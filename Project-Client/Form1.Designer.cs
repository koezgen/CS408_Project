namespace Project_Client
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_connect = new System.Windows.Forms.Button();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Username = new System.Windows.Forms.TextBox();
            this.button_rock = new System.Windows.Forms.Button();
            this.button_paper = new System.Windows.Forms.Button();
            this.button_scissors = new System.Windows.Forms.Button();
            this.button_leave_game = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(61, 204);
            this.button_connect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(247, 28);
            this.button_connect.TabIndex = 0;
            this.button_connect.Text = "Connect to Server";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(153, 70);
            this.textBox_IP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(153, 22);
            this.textBox_IP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server IP    :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 111);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port            : ";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(153, 107);
            this.textBox_Port.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(153, 22);
            this.textBox_Port.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 160);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Username  : ";
            // 
            // textBox_Username
            // 
            this.textBox_Username.Location = new System.Drawing.Point(153, 156);
            this.textBox_Username.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_Username.Name = "textBox_Username";
            this.textBox_Username.Size = new System.Drawing.Size(153, 22);
            this.textBox_Username.TabIndex = 6;
            // 
            // button_rock
            // 
            this.button_rock.Enabled = false;
            this.button_rock.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.button_rock.Location = new System.Drawing.Point(449, 295);
            this.button_rock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_rock.Name = "button_rock";
            this.button_rock.Size = new System.Drawing.Size(72, 58);
            this.button_rock.TabIndex = 8;
            this.button_rock.Text = "🪨";
            this.button_rock.UseVisualStyleBackColor = true;
            // 
            // button_paper
            // 
            this.button_paper.Enabled = false;
            this.button_paper.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.button_paper.Location = new System.Drawing.Point(553, 295);
            this.button_paper.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_paper.Name = "button_paper";
            this.button_paper.Size = new System.Drawing.Size(72, 58);
            this.button_paper.TabIndex = 9;
            this.button_paper.Text = "📃";
            this.button_paper.UseVisualStyleBackColor = true;
            // 
            // button_scissors
            // 
            this.button_scissors.Enabled = false;
            this.button_scissors.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.button_scissors.Location = new System.Drawing.Point(660, 298);
            this.button_scissors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_scissors.Name = "button_scissors";
            this.button_scissors.Size = new System.Drawing.Size(72, 55);
            this.button_scissors.TabIndex = 10;
            this.button_scissors.Text = "✂️";
            this.button_scissors.UseVisualStyleBackColor = true;
            // 
            // button_leave_game
            // 
            this.button_leave_game.Enabled = false;
            this.button_leave_game.Location = new System.Drawing.Point(905, 298);
            this.button_leave_game.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_leave_game.Name = "button_leave_game";
            this.button_leave_game.Size = new System.Drawing.Size(171, 55);
            this.button_leave_game.TabIndex = 12;
            this.button_leave_game.Text = "Leave Game";
            this.button_leave_game.UseVisualStyleBackColor = true;
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(404, 46);
            this.logs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(701, 223);
            this.logs.TabIndex = 13;
            this.logs.Text = "";
            this.logs.TextChanged += new System.EventHandler(this.logs_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 388);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_leave_game);
            this.Controls.Add(this.button_scissors);
            this.Controls.Add(this.button_paper);
            this.Controls.Add(this.button_rock);
            this.Controls.Add(this.textBox_Username);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.button_connect);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Username;
        private System.Windows.Forms.Button button_rock;
        private System.Windows.Forms.Button button_paper;
        private System.Windows.Forms.Button button_scissors;
        private System.Windows.Forms.Button button_leave_game;
        private System.Windows.Forms.RichTextBox logs;
    }
}

