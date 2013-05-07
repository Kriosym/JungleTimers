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

                // Load Sound Options...
                radioButton1_Dies.Checked = true;
                textBox_WarningSeconds.Text = source.Configs["Sounds"].Get("WarningSeconds");
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
            source.Configs["Sounds"].Set("WarningSeconds", textBox_WarningSeconds.Text);
            if (radioButton1_Dies.Checked == true)
            {
                source.Configs["Sounds"].Set("PurpleLizardDead", textBox_PurpleLizardSounds.Text);
                source.Configs["Sounds"].Set("PurpleGolemDead", textBox_PurpleGolemSounds.Text);
                source.Configs["Sounds"].Set("BaronDead", textBox_BaronSounds.Text);
                source.Configs["Sounds"].Set("DragonDead", textBox_DragonSounds.Text);
                source.Configs["Sounds"].Set("BlueGolemDead", textBox_BlueGolemSounds.Text);
                source.Configs["Sounds"].Set("BlueLizardDead", textBox_BlueLizardSounds.Text);                
            }

            if (radioButton1_Warning.Checked == true)
            {
                source.Configs["Sounds"].Set("PurpleLizardWarning", textBox_PurpleLizardSounds.Text);
                source.Configs["Sounds"].Set("PurpleGolemWarning", textBox_PurpleGolemSounds.Text);
                source.Configs["Sounds"].Set("BaronWarning", textBox_BaronSounds.Text);
                source.Configs["Sounds"].Set("DragonWarning", textBox_DragonSounds.Text);
                source.Configs["Sounds"].Set("BlueGolemWarning", textBox_BlueGolemSounds.Text);
                source.Configs["Sounds"].Set("BlueLizardWarning", textBox_BlueLizardSounds.Text);                
            }

            if (radioButton1_Respawns.Checked == true)
            {
                source.Configs["Sounds"].Set("PurpleLizardAlive", textBox_PurpleLizardSounds.Text);
                source.Configs["Sounds"].Set("PurpleGolemAlive", textBox_PurpleGolemSounds.Text);
                source.Configs["Sounds"].Set("BaronAlive", textBox_BaronSounds.Text);
                source.Configs["Sounds"].Set("DragonAlive", textBox_DragonSounds.Text);
                source.Configs["Sounds"].Set("BlueGolemAlive", textBox_BlueGolemSounds.Text);
                source.Configs["Sounds"].Set("BlueLizardAlive", textBox_BlueLizardSounds.Text);                
            } 

            source.Save();
            this.Close();
        }

        private void comboBox1_Hotkey1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button_PurpleLizardSounds_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox_PurpleLizardSounds.Text = openFileDialog1.FileName;
                if (radioButton1_Dies.Checked == true) { source.Configs["Sounds"].Set("PurpleLizardDead", openFileDialog1.FileName); }
                if (radioButton1_Warning.Checked == true) { source.Configs["Sounds"].Set("PurpleLizardWarning", openFileDialog1.FileName); }
                if (radioButton1_Respawns.Checked == true) { source.Configs["Sounds"].Set("PurpleLizardAlive", openFileDialog1.FileName); }
            }
        }

        private void button_PurpleGolemSounds_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox_PurpleGolemSounds.Text = openFileDialog1.FileName;
                if (radioButton1_Dies.Checked == true) { source.Configs["Sounds"].Set("PurpleGolemDead", openFileDialog1.FileName); }
                if (radioButton1_Warning.Checked == true) { source.Configs["Sounds"].Set("PurpleGolemWarning", openFileDialog1.FileName); }
                if (radioButton1_Respawns.Checked == true) { source.Configs["Sounds"].Set("PurpleGolemAlive", openFileDialog1.FileName); }
            }
        }

        private void button_BaronSounds_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox_BaronSounds.Text = openFileDialog1.FileName;
                if (radioButton1_Dies.Checked == true) { source.Configs["Sounds"].Set("BaronDead", openFileDialog1.FileName); }
                if (radioButton1_Warning.Checked == true) { source.Configs["Sounds"].Set("BaronWarning", openFileDialog1.FileName); }
                if (radioButton1_Respawns.Checked == true) { source.Configs["Sounds"].Set("BaronAlive", openFileDialog1.FileName); }
            }
        }

        private void button_DragonSounds_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox_DragonSounds.Text = openFileDialog1.FileName;
                if (radioButton1_Dies.Checked == true) { source.Configs["Sounds"].Set("DragonDead", openFileDialog1.FileName); }
                if (radioButton1_Warning.Checked == true) { source.Configs["Sounds"].Set("DragonWarning", openFileDialog1.FileName); }
                if (radioButton1_Respawns.Checked == true) { source.Configs["Sounds"].Set("DragonAlive", openFileDialog1.FileName); }
            }
        }

        private void button_BlueGolemSounds_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox_BlueGolemSounds.Text = openFileDialog1.FileName;
                if (radioButton1_Dies.Checked == true) { source.Configs["Sounds"].Set("BlueGolemDead", openFileDialog1.FileName); }
                if (radioButton1_Warning.Checked == true) { source.Configs["Sounds"].Set("BlueGolemWarning", openFileDialog1.FileName); }
                if (radioButton1_Respawns.Checked == true) { source.Configs["Sounds"].Set("BlueGolemAlive", openFileDialog1.FileName); }
            }
        }

        private void button_BlueLizardSounds_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox_BlueLizardSounds.Text = openFileDialog1.FileName;
                if (radioButton1_Dies.Checked == true) { source.Configs["Sounds"].Set("BlueLizardDead", openFileDialog1.FileName); }
                if (radioButton1_Warning.Checked == true) { source.Configs["Sounds"].Set("BlueLizardWarning", openFileDialog1.FileName); }
                if (radioButton1_Respawns.Checked == true) { source.Configs["Sounds"].Set("BlueLizardAlive", openFileDialog1.FileName); }
            }
        } 

        // Radial Button For Monster Dies Sound Event...
        private void radioButton1_Dies_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1_Dies.Checked == true)
            {
                textBox_PurpleLizardSounds.Text = source.Configs["Sounds"].Get("PurpleLizardDead");
                textBox_PurpleGolemSounds.Text = source.Configs["Sounds"].Get("PurpleGolemDead");                
                textBox_BaronSounds.Text = source.Configs["Sounds"].Get("BaronDead");                
                textBox_DragonSounds.Text = source.Configs["Sounds"].Get("DragonDead");               
                textBox_BlueLizardSounds.Text = source.Configs["Sounds"].Get("BlueLizardDead");                
                textBox_BlueGolemSounds.Text = source.Configs["Sounds"].Get("BlueGolemDead");                  
            }
     
        }

        // Radial Button For Monster Warning (about to respawn) Sound Event...
        private void radioButton1_Warning_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1_Warning.Checked == true)
            {
                textBox_PurpleLizardSounds.Text = source.Configs["Sounds"].Get("PurpleLizardWarning");
                textBox_PurpleGolemSounds.Text = source.Configs["Sounds"].Get("PurpleGolemWarning");
                textBox_BaronSounds.Text = source.Configs["Sounds"].Get("BaronWarning");
                textBox_DragonSounds.Text = source.Configs["Sounds"].Get("DragonWarning");
                textBox_BlueLizardSounds.Text = source.Configs["Sounds"].Get("BlueLizardWarning");
                textBox_BlueGolemSounds.Text = source.Configs["Sounds"].Get("BlueGolemWarning");
            }
        }

        // Radial Button For Monster Respawns Sound Event...
        private void radioButton1_Respawns_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1_Respawns.Checked == true)
            {
                textBox_PurpleLizardSounds.Text = source.Configs["Sounds"].Get("PurpleLizardAlive");
                textBox_PurpleGolemSounds.Text = source.Configs["Sounds"].Get("PurpleGolemAlive");
                textBox_BaronSounds.Text = source.Configs["Sounds"].Get("BaronAlive");
                textBox_DragonSounds.Text = source.Configs["Sounds"].Get("DragonAlive");
                textBox_BlueLizardSounds.Text = source.Configs["Sounds"].Get("BlueLizardAlive");
                textBox_BlueGolemSounds.Text = source.Configs["Sounds"].Get("BlueGolemAlive");
            }
        }

        private void textBox_WarningSeconds_TextChanged(object sender, EventArgs e)
        {
            long testfornum;
            if (!long.TryParse(textBox_WarningSeconds.Text, out testfornum))
            {                
                textBox_WarningSeconds.Clear();                
            }
        } 
    }
}
