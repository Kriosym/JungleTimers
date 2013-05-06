using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using NetworkCommsDotNet;
using System.Runtime.InteropServices;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System.Diagnostics;
using System.Resources;
using System.IO;
using Nini.Config;

namespace JungleTimers
{
    public partial class Form2 : Form
    {
        IConfigSource source = new IniConfigSource("JTconfig.ini");
        public Form2()
        {
            
        }        
        private Form1 mainForm = null;

        public Form2(Form callingForm)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            mainForm = callingForm as Form1;
            InitializeComponent();
            Form2_Load(null, null);
        }

        private void Form2_Load(object sender, EventArgs e)
        {            
            foreach (System.Windows.Forms.Keys key in Enum.GetValues(typeof(System.Windows.Forms.Keys)))
            {   
                // Load all possible Keyboard keys into dropdown selections...
                comboBox1_Hotkey1.Items.Add(key);
                comboBox2_Hotkey2.Items.Add(key);
                comboBox3_Hotkey3.Items.Add(key);
                comboBox4_Hotkey4.Items.Add(key);
                comboBox5_Hotkey5.Items.Add(key);
                comboBox6_Hotkey6.Items.Add(key);

                // Show current hotkey configuration in each dropdown box...
                comboBox1_Hotkey1.Text = source.Configs["Hotkeys"].Get("Hotkey1");
                comboBox2_Hotkey2.Text = source.Configs["Hotkeys"].Get("Hotkey2");
                comboBox3_Hotkey3.Text = source.Configs["Hotkeys"].Get("Hotkey3");
                comboBox4_Hotkey4.Text = source.Configs["Hotkeys"].Get("Hotkey4");
                comboBox5_Hotkey5.Text = source.Configs["Hotkeys"].Get("Hotkey5");
                comboBox6_Hotkey6.Text = source.Configs["Hotkeys"].Get("Hotkey6");

                // Show current Sounds configuration  in each dropdown box...
                textBox_PurpleLizardDead.Text = source.Configs["Sounds"].Get("PurpleLizardDead");
                textBox_PurpleLizardAlive.Text = source.Configs["Sounds"].Get("PurpleLizardAlive");
                textBox_PurpleGolemDead.Text = source.Configs["Sounds"].Get("PurpleGolemDead");
                textBox_PurpleGolemAlive.Text = source.Configs["Sounds"].Get("PurpleGolemAlive");
                textBox_BaronDead.Text = source.Configs["Sounds"].Get("BaronDead"); 
                textBox_BaronAlive.Text = source.Configs["Sounds"].Get("BaronAlive");
                textBox_DragonDead.Text = source.Configs["Sounds"].Get("DragonDead");
                textBox_DragonAlive.Text = source.Configs["Sounds"].Get("DragonAlive");
                textBox_BlueLizardDead.Text = source.Configs["Sounds"].Get("BlueLizardDead");
                textBox_BlueLizardAlive.Text = source.Configs["Sounds"].Get("BlueLizardAlive");
                textBox_BlueGolemDead.Text = source.Configs["Sounds"].Get("BlueGolemDead");
                textBox_BlueGolemAlive.Text = source.Configs["Sounds"].Get("BlueGolemAlive");      
            }
            
        }
        private string GetDescription(System.Windows.Forms.Keys key)
        {
            return key.ToString(); // default name            
        }

        // Save Configuration...
        private void button1_save_Click(object sender, EventArgs e)
        {                        
            // Hotkeys -
            source.Configs["Hotkeys"].Set("Hotkey1", comboBox1_Hotkey1.Text);
            source.Configs["Hotkeys"].Set("Hotkey2", comboBox2_Hotkey2.Text);
            source.Configs["Hotkeys"].Set("Hotkey3", comboBox3_Hotkey3.Text);
            source.Configs["Hotkeys"].Set("Hotkey4", comboBox4_Hotkey4.Text);
            source.Configs["Hotkeys"].Set("Hotkey5", comboBox5_Hotkey5.Text);
            source.Configs["Hotkeys"].Set("Hotkey6", comboBox6_Hotkey6.Text);

            // Sounds - 
            source.Configs["Sounds"].Set("PurpleLizardDead", textBox_PurpleLizardDead.Text);           
            source.Configs["Sounds"].Set("PurpleLizardAlive", textBox_PurpleLizardAlive.Text);
            source.Configs["Sounds"].Set("PurpleGolemDead", textBox_PurpleGolemDead.Text);
            source.Configs["Sounds"].Set("PurpleGolemAlive", textBox_PurpleGolemAlive.Text);
            source.Configs["Sounds"].Set("BaronDead", textBox_BaronDead.Text);
            source.Configs["Sounds"].Set("BaronAlive", textBox_BaronAlive.Text);
            source.Configs["Sounds"].Set("DragonDead", textBox_DragonDead.Text);
            source.Configs["Sounds"].Set("DragonAlive", textBox_DragonAlive.Text);
            source.Configs["Sounds"].Set("BlueLizardDead", textBox_BlueLizardDead.Text);
            source.Configs["Sounds"].Set("BlueLizardAlive", textBox_BlueLizardAlive.Text);
            source.Configs["Sounds"].Set("BlueGolemDead", textBox_BlueGolemDead.Text);
            source.Configs["Sounds"].Set("BlueGolemAlive", textBox_BlueGolemAlive.Text);
            
            source.Save();
            this.Close();
        }

        private void comboBox1_Hotkey1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button_PurpleLizardDead_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox_PurpleLizardDead.Text = openFileDialog1.FileName;
            }
        }

        private void button_PurpleLizardAlive_Click(object sender, EventArgs e)
        {

        }

        private void button_PurpleGolemDead_Click(object sender, EventArgs e)
        {

        }

        private void button_PurpleGolemAlive_Click(object sender, EventArgs e)
        {

        }

        private void button_BaronDead_Click(object sender, EventArgs e)
        {

        }

        private void button_BaronAlive_Click(object sender, EventArgs e)
        {

        }

        private void button_DragonDead_Click(object sender, EventArgs e)
        {

        }

        private void button_DragonAlive_Click(object sender, EventArgs e)
        {

        }

        private void button_BlueGolemDead_Click(object sender, EventArgs e)
        {

        }

        private void button_BlueGolemAlive_Click(object sender, EventArgs e)
        {

        }

        private void button_BlueLizardDead_Click(object sender, EventArgs e)
        {

        }

        private void button_BlueLizardAlive_Click(object sender, EventArgs e)
        {

        }
    }
}
