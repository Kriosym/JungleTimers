using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JungleTimers
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            
        }

        private Form1 mainForm = null;
        public Form2(Form callingForm)
        {
            mainForm = callingForm as Form1;
            InitializeComponent();
        }

        private static void Form2_Load(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Keys key in Enum.GetValues(typeof(System.Windows.Forms.Keys)))
            {
                Form2.comboBox1.Items.Add(new { Value = key, Description = GetDescription(key) });
            }
            Form2.comboBox1.DisplayMember = "Description";
        }

        private string GetDescription(System.Windows.Forms.Keys key)
        {
            switch (key)
            {
                case Keys.OemPipe:
                    return "Better oem pipe description";

                case Keys.HanjaMode:
                    return "Ninja mode";

                default:
                    return key.ToString(); // default name
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
