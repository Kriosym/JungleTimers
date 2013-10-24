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
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.TeamPurplzor = new System.Windows.Forms.GroupBox();
            this.TeamBlue = new System.Windows.Forms.GroupBox();
            this.baronlabel = new System.Windows.Forms.Label();
            this.dragonlabel = new System.Windows.Forms.Label();
            this.comboHostAddressBox = new System.Windows.Forms.TextBox();
            this.labelhostnameorip = new System.Windows.Forms.Label();
            this.button7connect = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.statusled = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TeamPurplzor.SuspendLayout();
            this.TeamBlue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusled)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button6
            // 
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.button6.ForeColor = System.Drawing.Color.Lime;
            this.button6.Image = global::JungleTimers.Properties.Resources.dragonbutton;
            this.button6.Location = new System.Drawing.Point(143, 197);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(96, 96);
            this.button6.TabIndex = 5;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.button5.ForeColor = System.Drawing.Color.Lime;
            this.button5.Image = global::JungleTimers.Properties.Resources.baronbutton;
            this.button5.Location = new System.Drawing.Point(27, 197);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 96);
            this.button5.TabIndex = 4;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Red;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.button4.ForeColor = System.Drawing.Color.Lime;
            this.button4.Image = global::JungleTimers.Properties.Resources.redbutton;
            this.button4.Location = new System.Drawing.Point(131, 37);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 96);
            this.button4.TabIndex = 7;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Blue;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.button3.ForeColor = System.Drawing.Color.Lime;
            this.button3.Image = global::JungleTimers.Properties.Resources.bluebutton;
            this.button3.Location = new System.Drawing.Point(15, 37);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 96);
            this.button3.TabIndex = 6;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.AutoEllipsis = true;
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.button1.ForeColor = System.Drawing.Color.Lime;
            this.button1.Image = global::JungleTimers.Properties.Resources.redbutton;
            this.button1.Location = new System.Drawing.Point(15, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 96);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Blue;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.button2.ForeColor = System.Drawing.Color.Lime;
            this.button2.Image = global::JungleTimers.Properties.Resources.bluebutton;
            this.button2.Location = new System.Drawing.Point(131, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 96);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TeamPurplzor
            // 
            this.TeamPurplzor.BackColor = System.Drawing.Color.Transparent;
            this.TeamPurplzor.Controls.Add(this.button2);
            this.TeamPurplzor.Controls.Add(this.button1);
            this.TeamPurplzor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TeamPurplzor.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamPurplzor.ForeColor = System.Drawing.Color.Purple;
            this.TeamPurplzor.Location = new System.Drawing.Point(12, 12);
            this.TeamPurplzor.Name = "TeamPurplzor";
            this.TeamPurplzor.Size = new System.Drawing.Size(241, 143);
            this.TeamPurplzor.TabIndex = 7;
            this.TeamPurplzor.TabStop = false;
            this.TeamPurplzor.Text = "Team Purple";
            // 
            // TeamBlue
            // 
            this.TeamBlue.BackColor = System.Drawing.Color.Transparent;
            this.TeamBlue.Controls.Add(this.button3);
            this.TeamBlue.Controls.Add(this.button4);
            this.TeamBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeamBlue.ForeColor = System.Drawing.Color.Blue;
            this.TeamBlue.Location = new System.Drawing.Point(12, 309);
            this.TeamBlue.Name = "TeamBlue";
            this.TeamBlue.Size = new System.Drawing.Size(241, 143);
            this.TeamBlue.TabIndex = 8;
            this.TeamBlue.TabStop = false;
            this.TeamBlue.Text = "Team Blue";
            // 
            // baronlabel
            // 
            this.baronlabel.AutoSize = true;
            this.baronlabel.BackColor = System.Drawing.Color.Transparent;
            this.baronlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baronlabel.ForeColor = System.Drawing.Color.White;
            this.baronlabel.Location = new System.Drawing.Point(41, 171);
            this.baronlabel.Name = "baronlabel";
            this.baronlabel.Size = new System.Drawing.Size(69, 25);
            this.baronlabel.TabIndex = 10;
            this.baronlabel.Text = "Baron";
            // 
            // dragonlabel
            // 
            this.dragonlabel.AutoSize = true;
            this.dragonlabel.BackColor = System.Drawing.Color.Transparent;
            this.dragonlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dragonlabel.ForeColor = System.Drawing.Color.White;
            this.dragonlabel.Location = new System.Drawing.Point(150, 171);
            this.dragonlabel.Name = "dragonlabel";
            this.dragonlabel.Size = new System.Drawing.Size(82, 25);
            this.dragonlabel.TabIndex = 11;
            this.dragonlabel.Text = "Dragon";
            // 
            // comboHostAddressBox
            // 
            this.comboHostAddressBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.comboHostAddressBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboHostAddressBox.Location = new System.Drawing.Point(527, 425);
            this.comboHostAddressBox.Name = "comboHostAddressBox";
            this.comboHostAddressBox.Size = new System.Drawing.Size(222, 26);
            this.comboHostAddressBox.TabIndex = 1;
            this.comboHostAddressBox.Text = "krio.game-host.org";
            this.comboHostAddressBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.comboHostAddressBox.WordWrap = false;
            this.comboHostAddressBox.TextChanged += new System.EventHandler(this.comboHostAddressBox_TextChanged);
            // 
            // labelhostnameorip
            // 
            this.labelhostnameorip.AutoSize = true;
            this.labelhostnameorip.BackColor = System.Drawing.Color.Transparent;
            this.labelhostnameorip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelhostnameorip.ForeColor = System.Drawing.Color.White;
            this.labelhostnameorip.Location = new System.Drawing.Point(523, 402);
            this.labelhostnameorip.Name = "labelhostnameorip";
            this.labelhostnameorip.Size = new System.Drawing.Size(226, 20);
            this.labelhostnameorip.TabIndex = 13;
            this.labelhostnameorip.Text = "Enter Hostname or IP Address";
            // 
            // button7connect
            // 
            this.button7connect.Location = new System.Drawing.Point(448, 425);
            this.button7connect.Name = "button7connect";
            this.button7connect.Size = new System.Drawing.Size(73, 27);
            this.button7connect.TabIndex = 0;
            this.button7connect.Text = "Connect";
            this.button7connect.UseVisualStyleBackColor = true;
            this.button7connect.Click += new System.EventHandler(this.button7connect_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(686, 48);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 14;
            this.button8.Text = "test";
            this.button8.UseCompatibleTextRendering = true;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Visible = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // statusled
            // 
            this.statusled.BackColor = System.Drawing.Color.Transparent;
            this.statusled.Image = global::JungleTimers.Properties.Resources.reddot;
            this.statusled.Location = new System.Drawing.Point(433, 432);
            this.statusled.Name = "statusled";
            this.statusled.Size = new System.Drawing.Size(12, 13);
            this.statusled.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.statusled.TabIndex = 15;
            this.statusled.TabStop = false;
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
            this.label1.Location = new System.Drawing.Point(718, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "vx.x";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::JungleTimers.Properties.Resources.rengar;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(773, 464);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusled);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7connect);
            this.Controls.Add(this.labelhostnameorip);
            this.Controls.Add(this.comboHostAddressBox);
            this.Controls.Add(this.dragonlabel);
            this.Controls.Add(this.baronlabel);
            this.Controls.Add(this.TeamBlue);
            this.Controls.Add(this.TeamPurplzor);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Jungle Timer by Kriosym";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TeamPurplzor.ResumeLayout(false);
            this.TeamBlue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusled)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox TeamPurplzor;
        private System.Windows.Forms.GroupBox TeamBlue;
        private System.Windows.Forms.Label baronlabel;
        private System.Windows.Forms.Label dragonlabel;
        private System.Windows.Forms.TextBox comboHostAddressBox;
        private System.Windows.Forms.Label labelhostnameorip;
        private System.Windows.Forms.Button button7connect;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.PictureBox statusled;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    }
}

