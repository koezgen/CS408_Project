using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Client
{
    public partial class Form1 : Form
    {
        bool terminating = false;
        bool connected = false;
        Socket clientSocket;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    int receivedBytes = clientSocket.Receive(buffer);
                    if (receivedBytes > 0)
                    {
                        string incomingMessage = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                        int nullIndex = incomingMessage.IndexOf('\0');
                        if (nullIndex != -1)
                        {
                            incomingMessage = incomingMessage.Substring(0, nullIndex);
                        }

                        Int32.TryParse(incomingMessage, out int messageCode);

                        if (messageCode == 2)
                        {
                            logs.AppendText("Server was terminated.\n");
                            button_connect.Enabled = true;
                            connected = false;
                        }

                        else
                        {
                            logs.Invoke(new Action(() => {
                                logs.AppendText(incomingMessage + "\n");
                            }));
                        }
                    }
                }
                catch (SocketException sockEx)
                {
                    logs.Invoke(new Action(() => {
                        logs.AppendText("Socket exception: " + sockEx.Message + "\n");
                    }));
                    break; 
                }
                catch (Exception ex)
                {
                    logs.Invoke(new Action(() => {
                        logs.AppendText("General exception: " + ex.ToString() + "\n");
                    }));
                    continue;
                }
            }

            if (!connected)
            {
                clientSocket.Close();
            }
        }


        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_IP.Text;
            string username = textBox_Username.Text;

            int portNum;
            if (Int32.TryParse(textBox_Port.Text, out portNum))
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    clientSocket.Send(Encoding.Default.GetBytes(username + "_0"));

                    Byte[] buf = new Byte[64];
                    clientSocket.Receive(buf);

                    string incomingMessage = Encoding.Default.GetString(buf).Trim();

                    if (int.TryParse(incomingMessage, out int messageCode))
                    {
                        if (messageCode == 1)
                        {
                            logs.AppendText("Username " + username + " is already taken.\n");
                            clientSocket.Close();
                        }
                    }

                    else
                    {
                        button_connect.Enabled = false;
                        connected = true;

                        Thread receiveThread = new Thread(Receive);
                        receiveThread.Start();
                    }
                }

                catch (Exception ex) 
                { 
                    logs.AppendText(ex.ToString());
                }
            }

            else
            {
                logs.AppendText("Check the port\n");
            }
        }
    }
}
