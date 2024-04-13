using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Project_Server
{

    /// <summary>
    /// Protocol will be the following:
    /// username_code
    /// 
    /// e.g.
    /// user123_0
    /// is a login request.
    /// 
    /// user123_1
    /// is a request that will disconnect the user.
    /// 
    /// user123_2
    /// is a request that will drop the user from the game
    /// but not from the server.
    /// </summary>


    enum Message
    {
        LOGIN,
        LOGOFF,
        SPECTATE,
        ROCK_MESSAGE,
        PAPER_MESSAGE,
        SCISSORS_MESSAGE
    }

    /// <summary>
    /// After each cycle, 
    /// the game status of players will change.
    /// Initially, everybody is NONE. but when 
    /// the game actually starts, users will send messages that will represent their
    /// decision. These decisions will be held at the data structure up until the point where
    /// the countdown ends. 
    /// </summary>

    public partial class Form1 : Form
    {
        enum Game
        {
            NONE = 0,
            ROCK,
            PAPER,
            SCISSORS,
            SPECTATE
        }

        class PlayerAttributes
        {
            public Socket Socket;
            public int Points = 0;
            public Game game = Game.NONE;
            public int queueNumber;

            public PlayerAttributes(Socket socket, int queueNumber)
            {
                Socket = socket;
                this.queueNumber = queueNumber;
            }
        }

        /// <summary>
        /// Error codes:
        /// 0 : generic error.
        /// 1 : username already exists.
        /// 2 : 
        /// </summary>
        class clientException : Exception
        {
            public int errorNum = 0;

            public clientException(int err)
            {
                errorNum = err;
            }
        }

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Dictionary<string, PlayerAttributes> clients = new Dictionary<string, PlayerAttributes>();
        Dictionary<string, PlayerAttributes> queue = new Dictionary<string, PlayerAttributes>();

        Dictionary<string, int> leaderboard = new Dictionary<string, int>();

        // 1 indicates that username already exists on the server.
        private string errorMessage1 = "1";

        // 2 indicates that server was terminated.
        private string errorMessage2 = "2";

        Byte[] welcomeMessage = Encoding.Default.GetBytes("Welcome to the Rock-Paper-Scissors game server!\n");
        Byte[] queueMessage = Encoding.Default.GetBytes("Right now, you are on the queue.");

        int active_player = 0;
        int queue_player = 0;

        bool terminating = false;
        bool listening = false;

        bool active_game = false;
        bool connected = true;

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        // Checks name of the user.
        private bool check_name_queue(string name)
        {
            return queue.Keys.Any(p => p.Equals(name));
        }

        // Two methods in order to prevent redundant lookups.
        private bool check_name_client(string name)
        {
            return clients.Keys.Any(p => p.Equals(name));
        }

        private void ResetListBox()
        {
            listBox1.Items.Clear();

            var list = leaderboard.OrderBy(kvp => kvp.Value).ThenBy(kvp => kvp.Key).ToList();
            foreach (var item in list)
            {
               listBox1.Items.Add("     " + item.Key + "                                   " + item.Value);
            }
        }

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void sendRoomInfo()
        {
            if (active_player != 4 && !active_game)
            {
                foreach (var item in clients.Keys)
                {
                    clients[item].Socket.Send(Encoding.Default.GetBytes("Waiting for players: (" + active_player.ToString() + "/4)"));
                }
            }
        }

        // This is where the rodeo takes place.
        private void Receive(Socket thisClient)
        {
            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    thisClient.Receive(buffer);
                    String[] message;

                    if (buffer.Length > 0)
                    {
                        string incomingMessage = Encoding.Default.GetString(buffer);
                        incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                        message = incomingMessage.Split('_');

                        if (message.Length > 0)
                        {
                            if (Int32.TryParse(message[1], out int msg_type))
                            {
                                String usr = message[0];
                                Message msg = (Message)msg_type;

                                switch (msg)
                                {
                                    case Message.LOGIN:
                                        if (!check_name_client(usr) && !check_name_queue(usr))
                                        {
                                            if (active_player != 4)
                                            {
                                                active_player++;
                                                PlayerAttributes attr = new PlayerAttributes(thisClient, 0);
                                                clients.Add(usr, attr);
                                                leaderboard.Add(usr, 0);
                                                logs.AppendText(usr + " joined.\n");

                                                clients[usr].Socket.Send(welcomeMessage);
                                                sendRoomInfo();

                                                ResetListBox();

                                                if (active_player == 4)
                                                {
                                                    active_game = true;
                                                }
                                            }

                                            else
                                            {
                                                queue_player++;
                                                PlayerAttributes attr = new PlayerAttributes(thisClient, queue_player);
                                                queue.Add(usr, attr);
                                                thisClient.Send(welcomeMessage);
                                                thisClient.Send(queueMessage);
                                            }

                                            break;
                                        }

                                        else throw new clientException(1);
                                    
                                    // DEPRECATE
                                    case Message.LOGOFF:
                                        clients[usr].Socket.Send(Encoding.Default.GetBytes("Have a nice day! Thanks for playing Rock-Paper-Scissors!\n"));
                                        clients[usr].Socket.Close();
                                        connected = false;
                                        break;

                                    case Message.SPECTATE:
                                        clients[usr].Socket.Send(Encoding.Default.GetBytes("You are now in spectator mode. In the next game, you will be able to play.\n"));
                                        clients[usr].game = Game.SPECTATE;
                                        break;

                                    case Message.ROCK_MESSAGE:
                                        break;

                                    case Message.PAPER_MESSAGE:
                                        break;

                                    case Message.SCISSORS_MESSAGE:
                                        break;
                                }
                            }
                        }
                    }
                }

                catch (clientException ex)
                {
                    if (ex.errorNum == 1)
                    {
                        Byte[] bufferClient = Encoding.Default.GetBytes(errorMessage1);
                        thisClient.Send(bufferClient);
                        logs.AppendText("Username already exists!\n");
                        thisClient.Close();
                    }

                    connected = false;
                }
            }
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    logs.AppendText("A user is trying to connect to the server...\n");
                    Thread receiveThread = new Thread(() => Receive(newClient));
                    receiveThread.Start();
                }
                
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(textBox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_listen.Enabled = false;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                logs.AppendText("Please check port number \n");
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
