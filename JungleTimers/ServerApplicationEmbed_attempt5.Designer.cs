namespace JungleTimers
{
    partial class ServerApplicationEmbed_attempt5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerApplicationEmbed_attempt5));
            this.consoleControl1 = new ConsoleControl.ConsoleControl();
            this.SuspendLayout();
            // 
            // consoleControl1
            // 
            this.consoleControl1.AutoScroll = true;
            this.consoleControl1.CausesValidation = false;
            this.consoleControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleControl1.IsInputEnabled = true;
            this.consoleControl1.Location = new System.Drawing.Point(0, 0);
            this.consoleControl1.Margin = new System.Windows.Forms.Padding(0);
            this.consoleControl1.Name = "consoleControl1";
            this.consoleControl1.SendKeyboardCommandsToProcess = false;
            this.consoleControl1.ShowDiagnostics = false;
            this.consoleControl1.Size = new System.Drawing.Size(375, 446);
            this.consoleControl1.TabIndex = 0;
            // 
            // ServerApplicationEmbed_attempt5
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(375, 446);
            this.Controls.Add(this.consoleControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ServerApplicationEmbed_attempt5";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "JT Server Module";
            this.Load += new System.EventHandler(this.ServerApplicationEmbed_attempt5_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ConsoleControl.ConsoleControl consoleControl1;
    }
}