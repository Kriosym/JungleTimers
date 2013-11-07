namespace JungleTimers
{
    partial class ServerApplicationForm3
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
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            button1_start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new System.Drawing.Point(12, 32);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new System.Drawing.Size(589, 395);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button1_start
            // 
            button1_start.Location = new System.Drawing.Point(11, 5);
            button1_start.Name = "button1_start";
            button1_start.Size = new System.Drawing.Size(75, 23);
            button1_start.TabIndex = 1;
            button1_start.Text = "Start";
            button1_start.UseVisualStyleBackColor = true;
            button1_start.Click += new System.EventHandler(button1_start_Click);
            // 
            // ServerApplicationForm3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 439);
            this.Controls.Add(button1_start);
            this.Controls.Add(richTextBox1);
            this.Name = "ServerApplicationForm3";
            this.Text = "ServerApplicationForm3";
            this.Load += new System.EventHandler(this.ServerApplicationForm3_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public static System.Windows.Forms.RichTextBox richTextBox1;
        public static System.Windows.Forms.Button button1_start;
    }
}