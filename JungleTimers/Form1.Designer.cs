using Glass;

namespace JungleTimers
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.baronlabel = new System.Windows.Forms.Label();
            this.dragonlabel = new System.Windows.Forms.Label();
            this.comboHostAddressBox = new System.Windows.Forms.TextBox();
            this.label_hostnameorip = new System.Windows.Forms.Label();
            this.button7connect = new Glass.GlassButton();
            this.button8_speaker = new System.Windows.Forms.Button();
            this.statusled = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonTest = new Glass.GlassButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label_Clients = new System.Windows.Forms.Label();
            this.flowLayoutPanel1_clients = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button_ServerGO = new Glass.GlassButton();
            this.button2 = new Glass.GlassButton();
            this.button3 = new Glass.GlassButton();
            this.button4 = new Glass.GlassButton();
            this.button1 = new Glass.GlassButton();
            this.button5 = new Glass.GlassButton();
            this.button6 = new Glass.GlassButton();
            ((System.ComponentModel.ISupportInitialize)(this.statusled)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // baronlabel
            // 
            this.baronlabel.AutoSize = true;
            this.baronlabel.BackColor = System.Drawing.Color.Transparent;
            this.baronlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.baronlabel.ForeColor = System.Drawing.Color.Lime;
            this.baronlabel.Location = new System.Drawing.Point(21, 166);
            this.baronlabel.Name = "baronlabel";
            this.baronlabel.Size = new System.Drawing.Size(77, 24);
            this.baronlabel.TabIndex = 10;
            this.baronlabel.Text = "BARON";
            // 
            // dragonlabel
            // 
            this.dragonlabel.AutoSize = true;
            this.dragonlabel.BackColor = System.Drawing.Color.Transparent;
            this.dragonlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.dragonlabel.ForeColor = System.Drawing.Color.Yellow;
            this.dragonlabel.Location = new System.Drawing.Point(131, 166);
            this.dragonlabel.Name = "dragonlabel";
            this.dragonlabel.Size = new System.Drawing.Size(92, 24);
            this.dragonlabel.TabIndex = 11;
            this.dragonlabel.Text = "DRAGON";
            // 
            // comboHostAddressBox
            // 
            this.comboHostAddressBox.BackColor = System.Drawing.Color.Gainsboro;
            this.comboHostAddressBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.comboHostAddressBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.comboHostAddressBox.ForeColor = System.Drawing.Color.Black;
            this.comboHostAddressBox.Location = new System.Drawing.Point(471, 420);
            this.comboHostAddressBox.Margin = new System.Windows.Forms.Padding(0);
            this.comboHostAddressBox.Name = "comboHostAddressBox";
            this.comboHostAddressBox.Size = new System.Drawing.Size(196, 20);
            this.comboHostAddressBox.TabIndex = 0;
            this.comboHostAddressBox.TabStop = false;
            this.comboHostAddressBox.Text = "KrioGamer.com";
            this.comboHostAddressBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.comboHostAddressBox.WordWrap = false;
            this.comboHostAddressBox.TextChanged += new System.EventHandler(this.comboHostAddressBox_TextChanged);
            // 
            // label_hostnameorip
            // 
            this.label_hostnameorip.AutoSize = true;
            this.label_hostnameorip.BackColor = System.Drawing.Color.Transparent;
            this.label_hostnameorip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label_hostnameorip.ForeColor = System.Drawing.Color.Gainsboro;
            this.label_hostnameorip.Location = new System.Drawing.Point(468, 403);
            this.label_hostnameorip.Name = "label_hostnameorip";
            this.label_hostnameorip.Size = new System.Drawing.Size(199, 17);
            this.label_hostnameorip.TabIndex = 0;
            this.label_hostnameorip.Text = "Enter Hostname or IP Address";
            // 
            // button7connect
            // 
            this.button7connect.ForeColor = System.Drawing.Color.Lime;
            this.button7connect.GlowColor = System.Drawing.Color.Lime;
            this.button7connect.InnerBorderColor = System.Drawing.Color.Transparent;
            this.button7connect.Location = new System.Drawing.Point(669, 419);
            this.button7connect.Name = "button7connect";
            this.button7connect.OuterBorderColor = System.Drawing.Color.Transparent;
            this.button7connect.ShineColor = System.Drawing.Color.Lime;
            this.button7connect.Size = new System.Drawing.Size(84, 22);
            this.button7connect.TabIndex = 0;
            this.button7connect.TabStop = false;
            this.button7connect.Text = "connect";
            this.button7connect.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button7connect.Click += new System.EventHandler(this.button7connect_Click);
            // 
            // button8_speaker
            // 
            this.button8_speaker.BackColor = System.Drawing.Color.Transparent;
            this.button8_speaker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button8_speaker.FlatAppearance.BorderSize = 0;
            this.button8_speaker.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button8_speaker.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button8_speaker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8_speaker.Font = new System.Drawing.Font("Impact", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8_speaker.ForeColor = System.Drawing.Color.Transparent;
            this.button8_speaker.Location = new System.Drawing.Point(676, 17);
            this.button8_speaker.Margin = new System.Windows.Forms.Padding(0);
            this.button8_speaker.Name = "button8_speaker";
            this.button8_speaker.Size = new System.Drawing.Size(84, 96);
            this.button8_speaker.TabIndex = 0;
            this.button8_speaker.TabStop = false;
            this.button8_speaker.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button8_speaker.UseVisualStyleBackColor = false;
            this.button8_speaker.Click += new System.EventHandler(this.button8_Click);
            // 
            // statusled
            // 
            this.statusled.BackColor = System.Drawing.Color.Transparent;
            this.statusled.Location = new System.Drawing.Point(290, 433);
            this.statusled.Name = "statusled";
            this.statusled.Size = new System.Drawing.Size(12, 10);
            this.statusled.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.statusled.TabIndex = 15;
            this.statusled.TabStop = false;
            this.statusled.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(720, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "v1.6b";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(126, 26);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(401, 2);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 23);
            this.buttonTest.TabIndex = 17;
            this.buttonTest.TabStop = false;
            this.buttonTest.Text = "buttonTest";
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(48, 310);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(139, 29);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(16, 8);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(205, 32);
            this.pictureBox2.TabIndex = 19;
            this.pictureBox2.TabStop = false;
            // 
            // label_Clients
            // 
            this.label_Clients.AutoSize = true;
            this.label_Clients.BackColor = System.Drawing.Color.Transparent;
            this.label_Clients.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Underline);
            this.label_Clients.ForeColor = System.Drawing.Color.Gold;
            this.label_Clients.Location = new System.Drawing.Point(698, 113);
            this.label_Clients.Name = "label_Clients";
            this.label_Clients.Size = new System.Drawing.Size(55, 20);
            this.label_Clients.TabIndex = 20;
            this.label_Clients.Text = "Clients";
            this.label_Clients.Visible = false;
            // 
            // flowLayoutPanel1_clients
            // 
            this.flowLayoutPanel1_clients.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1_clients.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1_clients.Location = new System.Drawing.Point(564, 133);
            this.flowLayoutPanel1_clients.Name = "flowLayoutPanel1_clients";
            this.flowLayoutPanel1_clients.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1_clients.Size = new System.Drawing.Size(192, 213);
            this.flowLayoutPanel1_clients.TabIndex = 21;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.LinkColor = System.Drawing.Color.Lime;
            this.linkLabel1.Location = new System.Drawing.Point(358, 7);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(31, 12);
            this.linkLabel1.TabIndex = 22;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Skype";
            this.linkLabel1.Visible = false;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // button_ServerGO
            // 
            this.button_ServerGO.Font = new System.Drawing.Font("Calibri", 11F, System.Drawing.FontStyle.Bold);
            this.button_ServerGO.ForeColor = System.Drawing.Color.Lime;
            this.button_ServerGO.Location = new System.Drawing.Point(669, 399);
            this.button_ServerGO.Name = "button_ServerGO";
            this.button_ServerGO.OuterBorderColor = System.Drawing.Color.Transparent;
            this.button_ServerGO.ShineColor = System.Drawing.Color.Gold;
            this.button_ServerGO.Size = new System.Drawing.Size(84, 19);
            this.button_ServerGO.TabIndex = 23;
            this.button_ServerGO.TabStop = false;
            this.button_ServerGO.Text = "Server GO!";
            this.button_ServerGO.UseCompatibleTextRendering = true;
            this.button_ServerGO.Click += new System.EventHandler(this.button_ServerGO_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F);
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button2.GlowColor = System.Drawing.Color.Blue;
            this.button2.Image = global::JungleTimers.Properties.Resources.PurpleAncientGolem;
            this.button2.InnerBorderColor = System.Drawing.Color.Blue;
            this.button2.Location = new System.Drawing.Point(130, 43);
            this.button2.Name = "button2";
            this.button2.OuterBorderColor = System.Drawing.Color.Transparent;
            this.button2.ShineColor = System.Drawing.Color.Navy;
            this.button2.Size = new System.Drawing.Size(96, 96);
            this.button2.TabIndex = 1;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F);
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button3.GlowColor = System.Drawing.Color.Blue;
            this.button3.Image = global::JungleTimers.Properties.Resources.BlueAncientGolem;
            this.button3.InnerBorderColor = System.Drawing.Color.Blue;
            this.button3.Location = new System.Drawing.Point(14, 343);
            this.button3.Name = "button3";
            this.button3.OuterBorderColor = System.Drawing.Color.Transparent;
            this.button3.ShineColor = System.Drawing.Color.Navy;
            this.button3.Size = new System.Drawing.Size(96, 96);
            this.button3.TabIndex = 2;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F);
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button4.GlowColor = System.Drawing.Color.Red;
            this.button4.Image = global::JungleTimers.Properties.Resources.BlueElderLizard;
            this.button4.InnerBorderColor = System.Drawing.Color.Red;
            this.button4.Location = new System.Drawing.Point(130, 343);
            this.button4.Name = "button4";
            this.button4.OuterBorderColor = System.Drawing.Color.Transparent;
            this.button4.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button4.Size = new System.Drawing.Size(96, 96);
            this.button4.TabIndex = 3;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.GlowColor = System.Drawing.Color.Red;
            this.button1.Image = global::JungleTimers.Properties.Resources.PurpleElderLizard;
            this.button1.InnerBorderColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(14, 43);
            this.button1.Name = "button1";
            this.button1.OuterBorderColor = System.Drawing.Color.Transparent;
            this.button1.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button1.Size = new System.Drawing.Size(96, 96);
            this.button1.TabIndex = 0;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F);
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button5.GlowColor = System.Drawing.Color.Lime;
            this.button5.Image = global::JungleTimers.Properties.Resources.TEHBARON;
            this.button5.InnerBorderColor = System.Drawing.Color.Lime;
            this.button5.Location = new System.Drawing.Point(14, 192);
            this.button5.Name = "button5";
            this.button5.OuterBorderColor = System.Drawing.Color.Transparent;
            this.button5.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button5.Size = new System.Drawing.Size(96, 96);
            this.button5.TabIndex = 4;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 21F);
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button6.GlowColor = System.Drawing.Color.Gold;
            this.button6.Image = global::JungleTimers.Properties.Resources.Dragon;
            this.button6.InnerBorderColor = System.Drawing.Color.Gold;
            this.button6.Location = new System.Drawing.Point(130, 192);
            this.button6.Name = "button6";
            this.button6.OuterBorderColor = System.Drawing.Color.Transparent;
            this.button6.ShineColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button6.Size = new System.Drawing.Size(96, 96);
            this.button6.TabIndex = 5;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::JungleTimers.Properties.Resources.rengar;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(763, 450);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.button_ServerGO);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label_Clients);
            this.Controls.Add(this.flowLayoutPanel1_clients);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button8_speaker);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusled);
            this.Controls.Add(this.button7connect);
            this.Controls.Add(this.label_hostnameorip);
            this.Controls.Add(this.comboHostAddressBox);
            this.Controls.Add(this.dragonlabel);
            this.Controls.Add(this.baronlabel);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jungle Timers by Kriosym";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.LocationChanged += new System.EventHandler(this.Form1_HasMoved);
            ((System.ComponentModel.ISupportInitialize)(this.statusled)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label baronlabel;
        private System.Windows.Forms.Label dragonlabel;
        private System.Windows.Forms.TextBox comboHostAddressBox;
        private System.Windows.Forms.Label label_hostnameorip;
        private GlassButton button7connect;
        private System.Windows.Forms.PictureBox statusled;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private GlassButton buttonTest;
        private GlassButton button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label_Clients;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1_clients;
        private System.Windows.Forms.Button button8_speaker;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private GlassButton button_ServerGO;
        private GlassButton button4;
        private GlassButton button5;
        private GlassButton button6;
        private GlassButton button2;
        private GlassButton button3;
    }
}

