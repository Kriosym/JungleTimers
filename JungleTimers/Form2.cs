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
            }
            
        }
        private string GetDescription(System.Windows.Forms.Keys key)
        {
            return key.ToString(); // default name            
        }

        private void button1_save_Click(object sender, EventArgs e)
        {
            //Object selectedItem = comboBox1_Hotkey1.SelectedItem;
            //MessageBox.Show("Selected Item Text: " + selectedItem.ToString());
            source.Configs["Hotkeys"].Set("Hotkey1", comboBox1_Hotkey1.SelectedItem);
            source.Configs["Hotkeys"].Set("Hotkey2", comboBox2_Hotkey2.SelectedItem);
            source.Configs["Hotkeys"].Set("Hotkey3", comboBox3_Hotkey3.SelectedItem);
            source.Configs["Hotkeys"].Set("Hotkey4", comboBox4_Hotkey4.SelectedItem);
            source.Configs["Hotkeys"].Set("Hotkey5", comboBox5_Hotkey5.SelectedItem);
            source.Configs["Hotkeys"].Set("Hotkey6", comboBox6_Hotkey6.SelectedItem);
            source.Save();
            this.Close();
        }

        private void comboBox1_Hotkey1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
