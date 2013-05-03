namespace JungleTimers
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.button1_save = new System.Windows.Forms.Button();
            this.comboBox1_Hotkey1 = new System.Windows.Forms.ComboBox();
            this.comboBox2_Hotkey2 = new System.Windows.Forms.ComboBox();
            this.comboBox3_Hotkey3 = new System.Windows.Forms.ComboBox();
            this.comboBox4_Hotkey4 = new System.Windows.Forms.ComboBox();
            this.comboBox5_Hotkey5 = new System.Windows.Forms.ComboBox();
            this.comboBox6_Hotkey6 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1_save
            // 
            this.button1_save.Location = new System.Drawing.Point(133, 309);
            this.button1_save.Name = "button1_save";
            this.button1_save.Size = new System.Drawing.Size(75, 23);
            this.button1_save.TabIndex = 0;
            this.button1_save.Text = "Save";
            this.button1_save.UseVisualStyleBackColor = true;
            this.button1_save.Click += new System.EventHandler(this.button1_save_Click);
            // 
            // comboBox1_Hotkey1
            // 
            this.comboBox1_Hotkey1.FormattingEnabled = true;
            this.comboBox1_Hotkey1.Location = new System.Drawing.Point(89, 44);
            this.comboBox1_Hotkey1.Name = "comboBox1_Hotkey1";
            this.comboBox1_Hotkey1.Size = new System.Drawing.Size(163, 21);
            this.comboBox1_Hotkey1.TabIndex = 1;
            this.comboBox1_Hotkey1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_Hotkey1_SelectedIndexChanged);
            // 
            // comboBox2_Hotkey2
            // 
            this.comboBox2_Hotkey2.FormattingEnabled = true;
            this.comboBox2_Hotkey2.Location = new System.Drawing.Point(89, 86);
            this.comboBox2_Hotkey2.Name = "comboBox2_Hotkey2";
            this.comboBox2_Hotkey2.Size = new System.Drawing.Size(163, 21);
            this.comboBox2_Hotkey2.TabIndex = 2;
            // 
            // comboBox3_Hotkey3
            // 
            this.comboBox3_Hotkey3.FormattingEnabled = true;
            this.comboBox3_Hotkey3.Location = new System.Drawing.Point(89, 128);
            this.comboBox3_Hotkey3.Name = "comboBox3_Hotkey3";
            this.comboBox3_Hotkey3.Size = new System.Drawing.Size(163, 21);
            this.comboBox3_Hotkey3.TabIndex = 3;
            // 
            // comboBox4_Hotkey4
            // 
            this.comboBox4_Hotkey4.FormattingEnabled = true;
            this.comboBox4_Hotkey4.Location = new System.Drawing.Point(89, 170);
            this.comboBox4_Hotkey4.Name = "comboBox4_Hotkey4";
            this.comboBox4_Hotkey4.Size = new System.Drawing.Size(163, 21);
            this.comboBox4_Hotkey4.TabIndex = 4;
            // 
            // comboBox5_Hotkey5
            // 
            this.comboBox5_Hotkey5.FormattingEnabled = true;
            this.comboBox5_Hotkey5.Location = new System.Drawing.Point(89, 212);
            this.comboBox5_Hotkey5.Name = "comboBox5_Hotkey5";
            this.comboBox5_Hotkey5.Size = new System.Drawing.Size(163, 21);
            this.comboBox5_Hotkey5.TabIndex = 5;
            // 
            // comboBox6_Hotkey6
            // 
            this.comboBox6_Hotkey6.FormattingEnabled = true;
            this.comboBox6_Hotkey6.Location = new System.Drawing.Point(89, 254);
            this.comboBox6_Hotkey6.Name = "comboBox6_Hotkey6";
            this.comboBox6_Hotkey6.Size = new System.Drawing.Size(163, 21);
            this.comboBox6_Hotkey6.TabIndex = 6;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 364);
            this.Controls.Add(this.comboBox6_Hotkey6);
            this.Controls.Add(this.comboBox5_Hotkey5);
            this.Controls.Add(this.comboBox4_Hotkey4);
            this.Controls.Add(this.comboBox3_Hotkey3);
            this.Controls.Add(this.comboBox2_Hotkey2);
            this.Controls.Add(this.comboBox1_Hotkey1);
            this.Controls.Add(this.button1_save);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "Options";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1_save;
        private System.Windows.Forms.ComboBox comboBox1_Hotkey1;
        private System.Windows.Forms.ComboBox comboBox2_Hotkey2;
        private System.Windows.Forms.ComboBox comboBox3_Hotkey3;
        private System.Windows.Forms.ComboBox comboBox4_Hotkey4;
        private System.Windows.Forms.ComboBox comboBox5_Hotkey5;
        private System.Windows.Forms.ComboBox comboBox6_Hotkey6;
    }
}