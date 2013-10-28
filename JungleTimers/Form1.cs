﻿using System;
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
using AnimatorNS;
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
    public partial class Form1 : Form
    {        
        // !! SET CODE REVISION !! 
        public static string versionIs = "1.5d";        

        // various other declarations...
        public bool FormCloseForUpdate;        
        bool backgroundhasplayed = false;
        public static HashSet<string> ConnectionsList = new HashSet<string>();        

        // Animation Time!
        Animator animator = new Animator();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags flags);
        [Flags]
        enum AnimateWindowFlags
        {
            AW_HOR_POSITIVE = 0x00000001,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_ACTIVATE = 0x00020000,
            AW_SLIDE = 0x00040000,
            AW_BLEND = 0x00080000
        }

        //Resource manager for accessing embedded files...
        ResourceManager resources = new ResourceManager(typeof(Form1));

        // Low level Keyboard Hook so that hotkeys work even while another fullscreen application has focus...
        KeyboardHookListener m_KeyboardHookManager = new KeyboardHookListener(new GlobalHooker());

        // Declare Background Workers and other variables...
        BackgroundWorker b1 = new BackgroundWorker();
        BackgroundWorker b2 = new BackgroundWorker();
        BackgroundWorker b3 = new BackgroundWorker();
        BackgroundWorker b4 = new BackgroundWorker();
        BackgroundWorker b5 = new BackgroundWorker();
        BackgroundWorker b6 = new BackgroundWorker();
        IPAddress ipaddr;        
        string validAddress;
        int serverPort = 11000;
        string ConnectButtonState = "Connect";
        string StartPath = Application.StartupPath;
        
        // Initialize Notification MP3 Play History...
        bool b1_SongHasPlayed = false;
        bool b2_SongHasPlayed = false;
        bool b3_SongHasPlayed = false;
        bool b4_SongHasPlayed = false;
        bool b5_SongHasPlayed = false;
        bool b6_SongHasPlayed = false;    
        bool SongBusy = false;
        bool Song2Busy = false;
        bool Song3Busy = false;
        bool Song4Busy = false;
        bool Song5Busy = false;
        bool Song6Busy = false;
        bool Song7Busy = false;
        
        // Hotkey strings...
        public string Hotkey1;
        public string Hotkey2;
        public string Hotkey3;
        public string Hotkey4;
        public string Hotkey5;
        public string Hotkey6;

        // Test timer...                
        public int timerCurrent = 0;
        public int timerLength = 5;

        // Sound Event Strings...
        public string WarningSecondsString;
        public int WarningSeconds;

        public string PurpleLizardDead;
        public string PurpleLizardWarning;
        public string PurpleLizardAlive;

        public string PurpleGolemDead;
        public string PurpleGolemWarning;
        public string PurpleGolemAlive;

        public string BaronDead;
        public string BaronWarning;
        public string BaronAlive;

        public string DragonDead;
        public string DragonWarning;
        public string DragonAlive;

        public string BlueGolemDead;
        public string BlueGolemWarning;
        public string BlueGolemAlive;

        public string BlueLizardDead;
        public string BlueLizardWarning;
        public string BlueLizardAlive;

        public string BackgroundMusic;
        
        // Import DLL for MP3 Playback...        
        public const int MM_MCINOTIFY = 0x3B9;        
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder returnString, int returnSize, IntPtr hwndCallback);

        // Declare Delegate to allow me to set button text without cross-thread errors...
        delegate void SetTextCallback(Control ctrl, string text);
        
        // Declare Delegate to allow me to set button enabled state without cross-thread errors...
        delegate void SetButtonStatus(Control ctrl, bool status);

        /* Testing to try and get rid of flicker...(disabled as doesn't work with AnimateWindowFlags)...
       *protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                //WS_EX_COMPOSITED. Prevents flickering.
                cp.ExStyle |= 0x00080000; //WS_EX_LAYERED. Transparency key
                
                return cp;
            }
        } */

        // Other Load Actions...
        private void Form1_Load(object sender, EventArgs e)
        {            
            
            // version...
            label1.Text = "v" + versionIs;            

            // Animate the Form as it Loads (doesn't work with cp.Exstyle protected override above)...
            // AnimateWindow(this.Handle, 1000, AnimateWindowFlags.AW_BLEND);
            
            // Pull Hotkey Config...
            IConfigSource source = new IniConfigSource("JTconfig.ini");
            Hotkey1 = source.Configs["Hotkeys"].Get("Hotkey1");
            Hotkey2 = source.Configs["Hotkeys"].Get("Hotkey2");
            Hotkey3 = source.Configs["Hotkeys"].Get("Hotkey3");
            Hotkey4 = source.Configs["Hotkeys"].Get("Hotkey4");
            Hotkey5 = source.Configs["Hotkeys"].Get("Hotkey5");
            Hotkey6 = source.Configs["Hotkeys"].Get("Hotkey6");

            // Pull Sound Events Config from JTconfig.ini...
            BackgroundMusic = source.Configs["Sounds"].Get("BackgroundMusic");
            if (BackgroundMusic == "Default") { BackgroundMusic = Application.StartupPath + "\\Resources\\oppagangamstyle.mp3"; }
            if (backgroundhasplayed == false)
            {
                PlaySong7(BackgroundMusic);
                backgroundhasplayed = true;
            }

            WarningSecondsString = source.Configs["Sounds"].Get("WarningSeconds");
            int.TryParse(WarningSecondsString, out WarningSeconds);

            PurpleLizardDead = source.Configs["Sounds"].Get("PurpleLizardDead");
            if (PurpleLizardDead == "Default") { PurpleLizardDead = Application.StartupPath + "\\Resources\\PurpleLizardDead.mp3"; }
            PurpleLizardWarning = source.Configs["Sounds"].Get("PurpleLizardWarning");
            if (PurpleLizardWarning == "Default") { PurpleLizardWarning = Application.StartupPath + "\\Resources\\PurpleLizardWarning.mp3"; }
            PurpleLizardAlive = source.Configs["Sounds"].Get("PurpleLizardAlive");
            if (PurpleLizardAlive == "Default") { PurpleLizardAlive = Application.StartupPath + "\\Resources\\PurpleLizardAlive.mp3"; }

            PurpleGolemDead = source.Configs["Sounds"].Get("PurpleGolemDead");
            if (PurpleGolemDead == "Default") { PurpleGolemDead = Application.StartupPath + "\\Resources\\PurpleGolemDead.mp3"; }
            PurpleGolemWarning = source.Configs["Sounds"].Get("PurpleGolemWarning");
            if (PurpleGolemWarning == "Default") { PurpleGolemWarning = Application.StartupPath + "\\Resources\\PurpleGolemWarning.mp3"; }
            PurpleGolemAlive = source.Configs["Sounds"].Get("PurpleGolemAlive");
            if (PurpleGolemAlive == "Default") { PurpleGolemAlive = Application.StartupPath + "\\Resources\\PurpleGolemAlive.mp3"; }

            BaronDead = source.Configs["Sounds"].Get("BaronDead");
            if (BaronDead == "Default") { BaronDead = Application.StartupPath + "\\Resources\\BaronDead.mp3"; }
            BaronWarning = source.Configs["Sounds"].Get("BaronWarning");
            if (BaronWarning == "Default") { BaronWarning = Application.StartupPath + "\\Resources\\BaronWarning.mp3"; }
            BaronAlive = source.Configs["Sounds"].Get("BaronAlive");
            if (BaronAlive == "Default") { BaronAlive = Application.StartupPath + "\\Resources\\BaronAlive.mp3"; }

            DragonDead = source.Configs["Sounds"].Get("DragonDead");
            if (DragonDead == "Default") { DragonDead = Application.StartupPath + "\\Resources\\DragonDead.mp3"; }
            DragonWarning = source.Configs["Sounds"].Get("DragonWarning");
            if (DragonWarning == "Default") { DragonWarning = Application.StartupPath + "\\Resources\\DragonWarning.mp3"; }
            DragonAlive = source.Configs["Sounds"].Get("DragonAlive");
            if (DragonAlive == "Default") { DragonAlive = Application.StartupPath + "\\Resources\\DragonAlive.mp3"; }

            BlueGolemDead = source.Configs["Sounds"].Get("BlueGolemDead");
            if (BlueGolemDead == "Default") { BlueGolemDead = Application.StartupPath + "\\Resources\\BlueGolemDead.mp3"; }
            BlueGolemWarning = source.Configs["Sounds"].Get("BlueGolemWarning");
            if (BlueGolemWarning == "Default") { BlueGolemWarning = Application.StartupPath + "\\Resources\\BlueGolemWarning.mp3"; }
            BlueGolemAlive = source.Configs["Sounds"].Get("BlueGolemAlive");
            if (BlueGolemAlive == "Default") { BlueGolemAlive = Application.StartupPath + "\\Resources\\BlueGolemAlive.mp3"; }

            BlueLizardDead = source.Configs["Sounds"].Get("BlueLizardDead");
            if (BlueLizardDead == "Default") { BlueLizardDead = Application.StartupPath + "\\Resources\\BlueLizardDead.mp3"; }
            BlueLizardWarning = source.Configs["Sounds"].Get("BlueLizardWarning");
            if (BlueLizardWarning == "Default") { BlueLizardWarning = Application.StartupPath + "\\Resources\\BlueLizardWarning.mp3"; }
            BlueLizardAlive = source.Configs["Sounds"].Get("BlueLizardAlive");
            if (BlueLizardAlive == "Default") { BlueLizardAlive = Application.StartupPath + "\\Resources\\BlueLizardAlive.mp3"; }

            //Prevent Unhandled Exception incase of rogue packet reception...
            NetworkComms.IgnoreUnknownPacketTypes = true;

            // Trigger these when a packets are received from Server...    
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Connected", Connected);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("TimerControl", TimerControl);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("version", VersionCheck);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Connection", AddToConnectionList);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Disconnection", RemoveFromConnectionList);
            
            
            // Keyboard Hook Initialize...
            m_KeyboardHookManager.KeyUp += m_KeyboardHookManager_KeyUp;
            m_KeyboardHookManager.Enabled = true;
            this.comboHostAddressBox.GotFocus += OnFocus;
            this.comboHostAddressBox.LostFocus += OnDefocus;

            // Initialize the Background Workers...
            b1.WorkerReportsProgress = true;
            b1.DoWork += new DoWorkEventHandler(b1_DoWork);
            b1.ProgressChanged += new ProgressChangedEventHandler(b1_ProgressChanged);
            b1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(b1_RunWorkerCompleted);
            b1.WorkerSupportsCancellation = true;

            b2.WorkerReportsProgress = true;
            b2.DoWork += new DoWorkEventHandler(b2_DoWork);
            b2.ProgressChanged += new ProgressChangedEventHandler(b2_ProgressChanged);
            b2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(b2_RunWorkerCompleted);
            b2.WorkerSupportsCancellation = true;

            b3.WorkerReportsProgress = true;
            b3.DoWork += new DoWorkEventHandler(b3_DoWork);
            b3.ProgressChanged += new ProgressChangedEventHandler(b3_ProgressChanged);
            b3.RunWorkerCompleted += new RunWorkerCompletedEventHandler(b3_RunWorkerCompleted);
            b3.WorkerSupportsCancellation = true;

            b4.WorkerReportsProgress = true;
            b4.DoWork += new DoWorkEventHandler(b4_DoWork);
            b4.ProgressChanged += new ProgressChangedEventHandler(b4_ProgressChanged);
            b4.RunWorkerCompleted += new RunWorkerCompletedEventHandler(b4_RunWorkerCompleted);
            b4.WorkerSupportsCancellation = true;

            b5.WorkerReportsProgress = true;
            b5.DoWork += new DoWorkEventHandler(b5_DoWork);
            b5.ProgressChanged += new ProgressChangedEventHandler(b5_ProgressChanged);
            b5.RunWorkerCompleted += new RunWorkerCompletedEventHandler(b5_RunWorkerCompleted);
            b5.WorkerSupportsCancellation = true;

            b6.WorkerReportsProgress = true;
            b6.DoWork += new DoWorkEventHandler(b6_DoWork);
            b6.ProgressChanged += new ProgressChangedEventHandler(b6_ProgressChanged);
            b6.RunWorkerCompleted += new RunWorkerCompletedEventHandler(b6_RunWorkerCompleted);
            b6.WorkerSupportsCancellation = true;
                        
            button1.Focus();
        }
        
        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            // Anti-Flicker parameters...
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            InitializeComponent();
            
            // test animation...           
            /* foreach (Control X in this.Controls)
            {
                animator.BeginUpdateSync(X, true);
                animator.Show(X, false, Animation.Particles);
                animator.EndUpdate(X);
            } */
        }

        // Disable Hotkeys if the Server/IP Textbox has focus...
        private void OnFocus(object sender, EventArgs e)
        {
            m_KeyboardHookManager.Enabled = false;
        }

        // Re-enable it afterwards...
        private void OnDefocus(object sender, EventArgs e)
        {
            m_KeyboardHookManager.Enabled = true;
        }

        // HOTKEY ASSIGNMENT...
        void m_KeyboardHookManager_KeyUp(object sender, KeyEventArgs e)
        {
            Trace.WriteLine("Key: " + e.KeyData.ToString());
            if (e.KeyData.ToString() == Hotkey1) // note: old code for keys directly was (e.KeyCode == Keys.NumPad2).
            {
                button1_Click(null, null);
            }
            else if (e.KeyData.ToString() == Hotkey2)
            {
                button2_Click(null, null);
            }
            else if (e.KeyData.ToString() == Hotkey3)
            {
                button3_Click(null, null);
            }
            else if (e.KeyData.ToString() == Hotkey4)
            {
                button4_Click(null, null);
            }
            else if (e.KeyData.ToString() == Hotkey5)
            {
                button5_Click(null, null);
            }
            else if (e.KeyData.ToString() == Hotkey6)
            {
                button6_Click(null, null);
            }
        }

        private void RefreshClientPanel()
        {
            // A new connection has happened, so clear then re-Populate client list in ClientPanel...
            this.Invoke((MethodInvoker)delegate { flowLayoutPanel1_clients.Controls.Clear(); });            
                        
            this.Invoke((MethodInvoker)delegate
            { 
                foreach (var item in ConnectionsList.Distinct())
                {                     
                   flowLayoutPanel1_clients.Controls.Add(new Label { Text = item, ForeColor = Color.Lime });    
                }                
            });

            // var clientPanelName = new Label { Text = item, ForeColor = Color.Lime };       
            // flowLayoutPanel1_clients.Controls.Add(clientPanelName);
        }

        private void AddToConnectionList(PacketHeader header, Connection connection, string Connection)
        {
            // Add incoming IP Address to HashSet list...            
            ConnectionsList.Add(Connection);

            // And refresh the list of clients shown in Client Panel...
            RefreshClientPanel();
        }

        private void RemoveFromConnectionList(PacketHeader header, Connection connection, string Disconnection)
        {
            ConnectionsList.Remove(Disconnection);
            
            // And refresh the list of clients shown in Client Panel...
            RefreshClientPanel();
        }

        // MP3 Finished return actions...(incomplete, need to figure out how to implement other song alias status checking (i.e. PlaySong2's media2 alias)...        
        protected override void DefWndProc(ref Message m)
       {
            base.DefWndProc(ref m);
            if (m.Msg == MM_MCINOTIFY)
            {
                SongBusy = false;
            }
        }

        // PlaySong starts alias media, via Cross-thread safe invoke delegate if needed...
        public delegate void delegatePlaySong(string file);
        public void PlaySong(string file)
        {   
            if (this.InvokeRequired)
            {
                this.Invoke(new delegatePlaySong(PlaySong), file);
            }
            else
            {                
                mciSendString("close media", null, 0, IntPtr.Zero);
                SongBusy = true;
                mciSendString("open \"" + file + "\" type mpegvideo alias media", null, 0, IntPtr.Zero);
                mciSendString("play media notify", null, 0, this.Handle);
            }
        }

        // PlaySong2 starts alias media2...
        public delegate void delegatePlaySong2(string file);
        public void PlaySong2(string file)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new delegatePlaySong2(PlaySong2), file);
            }
            else
            {
                mciSendString("close media2", null, 0, IntPtr.Zero);
                Song2Busy = true;
                mciSendString("open \"" + file + "\" type mpegvideo alias media2", null, 0, IntPtr.Zero);
                mciSendString("play media2 notify", null, 0, this.Handle);
            }
        }

        // PlaySong3 starts alias media3...
        public delegate void delegatePlaySong3(string file);
        public void PlaySong3(string file)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new delegatePlaySong3(PlaySong3), file);
            }
            else
            {
                mciSendString("close media3", null, 0, IntPtr.Zero);
                Song3Busy = true;
                mciSendString("open \"" + file + "\" type mpegvideo alias media3", null, 0, IntPtr.Zero);
                mciSendString("play media3 notify", null, 0, this.Handle);
            }
        }

        // PlaySong4 starts alias media4...
        public delegate void delegatePlaySong4(string file);
        public void PlaySong4(string file)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new delegatePlaySong4(PlaySong4), file);
            }
            else
            {
                mciSendString("close media4", null, 0, IntPtr.Zero);
                Song4Busy = true;
                mciSendString("open \"" + file + "\" type mpegvideo alias media4", null, 0, IntPtr.Zero);
                mciSendString("play media4 notify", null, 0, this.Handle);
            }
        }

        // PlaySong5 starts alias media5...
        public delegate void delegatePlaySong5(string file);
        public void PlaySong5(string file)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new delegatePlaySong5(PlaySong5), file);
            }
            else
            {
                mciSendString("close media5", null, 0, IntPtr.Zero);
                Song5Busy = true;
                mciSendString("open \"" + file + "\" type mpegvideo alias media5", null, 0, IntPtr.Zero);
                mciSendString("play media5 notify", null, 0, this.Handle);
            }
        }

        // PlaySong6 starts alias media6...
        public delegate void delegatePlaySong6(string file);
        public void PlaySong6(string file)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new delegatePlaySong6(PlaySong6), file);
            }
            else
            {
                mciSendString("close media6", null, 0, IntPtr.Zero);
                Song6Busy = true;
                mciSendString("open \"" + file + "\" type mpegvideo alias media6", null, 0, IntPtr.Zero);
                mciSendString("play media6 notify", null, 0, this.Handle);
            }
        }

        // PlaySong7 starts alias media7...
        public delegate void delegatePlaySong7(string file);
        public void PlaySong7(string file)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new delegatePlaySong7(PlaySong7), file);
            }
            else
            {
                mciSendString("close media7", null, 0, IntPtr.Zero);
                Song7Busy = true;
                mciSendString("open \"" + file + "\" type mpegvideo alias media7", null, 0, IntPtr.Zero);
                mciSendString("play media7 notify", null, 0, this.Handle);
            }
        }

        // BUTTON CLICK EVENTS -
        private void button1_Click(object sender, EventArgs e)
        {                       
            // Start the Timer if it isn't already going, using Client/Server mechanism if connected...
            // note: button7connect.Text reads "Connect" prior to connecting, and reads "Disconnect" once connection has been established.
            
            /* Spam Control Timer, works but blocks all button interaction for too long, better method will be to add anti-spam control to client preferences for incoming messages from a given client with option for each client to ignore audio portion or option for server to kick or ban.
            if (timer1.Enabled == false */
            {
                if (b1.IsBusy != true && button7connect.Text != "Connect")
                {
                    foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Message", "b1");
                }

                // ...or using local mechanism if not connected...
                else if (b1.IsBusy != true && button7connect.Text != "Disconnect")
                {
                    b1.RunWorkerAsync();
                }

                // Otherwise cancel Timer using Client/Server mechanism if connected...
                else if (b1.IsBusy != false && button7connect.Text != "Connect")
                {
                    foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Message", "b1STOP");
                }

                // ...or using local mechanism if not connected...
                else if (b1.IsBusy == true && button7connect.Text != "Disconnect")
                {
                    b1.CancelAsync();
                }
                // timer1.Enabled = true;
            }            
        }          

        private void button2_Click(object sender, EventArgs e)
        {
            // if (timer1.Enabled == false)
            {
                if (b2.IsBusy != true && button7connect.Text != "Connect")
                {
                    foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Message", "b2");
                }
                else if (b2.IsBusy != true && button7connect.Text != "Disconnect")
                {
                    b2.RunWorkerAsync();
                }
                else if (b2.IsBusy != false && button7connect.Text != "Connect")
                {
                    foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Message", "b2STOP");
                }
                else if (b2.IsBusy == true && button7connect.Text != "Disconnect")
                {
                    b2.CancelAsync();
                }
                // timer1.Enabled = true;
            }        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // if (timer1.Enabled == false)
            {
                if (b3.IsBusy != true && button7connect.Text != "Connect")
                {
                    foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Message", "b3");
                }
                else if (b3.IsBusy != true && button7connect.Text != "Disconnect")
                {
                    b3.RunWorkerAsync();
                }
                else if (b3.IsBusy != false && button7connect.Text != "Connect")
                {
                    foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Message", "b3STOP");
                }
                else if (b3.IsBusy != false && button7connect.Text != "Disconnect")
                {
                    b3.CancelAsync();
                }
               // timer1.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // if (timer1.Enabled == false)
            {
                if (b4.IsBusy != true && button7connect.Text != "Connect")
                {
                    foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Message", "b4");
                }
                else if (b4.IsBusy != true && button7connect.Text != "Disconnect")
                {
                    b4.RunWorkerAsync();
                }
                else if (b4.IsBusy != false && button7connect.Text != "Connect")
                {
                    foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Message", "b4STOP");
                }
                else if (b4.IsBusy != false && button7connect.Text != "Disconnect")
                {
                    b4.CancelAsync();
                }
                // timer1.Enabled = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // if (timer1.Enabled == false)
            {
                if (b5.IsBusy != true && button7connect.Text != "Connect")
                {
                    foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Message", "b5");
                }
                else if (b5.IsBusy != true && button7connect.Text != "Disconnect")
                {
                    b5.RunWorkerAsync();
                }
                else if (b5.IsBusy != false && button7connect.Text != "Connect")
                {
                    foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Message", "b5STOP");
                }
                else if (b5.IsBusy != false && button7connect.Text != "Disconnect")
                {
                    b5.CancelAsync();
                }
                // timer1.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // if (timer1.Enabled == false)
            {
                if (b6.IsBusy != true && button7connect.Text != "Connect")
                {
                    foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Message", "b6");
                }
                else if (b6.IsBusy != true && button7connect.Text != "Disconnect")
                {
                    b6.RunWorkerAsync();
                }
                else if (b6.IsBusy != false && button7connect.Text != "Connect")
                {
                    foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Message", "b6STOP");
                }
                else if (b6.IsBusy != false && button7connect.Text != "Disconnect")
                {
                    b6.CancelAsync();
                }
                // timer1.Enabled = true;
            }
        }

        // CONNECT/DISCONNECT BUTTON -
        public void button7connect_Click(object sender, EventArgs e)
        {
            if (button7connect.Text == "Disconnect")
            {
                try
                {
                    foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Disconnection", "Bye!");
                    NetworkComms.CloseAllConnections();
                    ConnectionsList.Clear();
                    RefreshClientPanel();
                    button7connect.Text = ConnectButtonState;
                    statusled.Image = Properties.Resources.reddot;
                }
                catch (DPSBase.ConnectionSetupException)
                {
                    return;
                }

            }
            else
            {
                var hostnameOrIP = comboHostAddressBox.Text;
                button7connect.Text = ConnectButtonState;
                // If valid IP...
                if (IPAddress.TryParse(hostnameOrIP, out ipaddr))
                {
                    validAddress = ipaddr.ToString();
                }

                // Else is host or invalid IP...
                else
                {
                    IPAddress[] currentIPHostEntry;
                    try
                    {
                        currentIPHostEntry = Dns.GetHostAddresses(hostnameOrIP);
                    }
                    catch (System.Net.Sockets.SocketException)
                    {
                        MessageBox.Show("Error getting address");
                        return;
                    }

                    ipaddr = currentIPHostEntry[0];
                    validAddress = ipaddr.ToString();
                }

                // Now that we have the IP and Port, connect to the Server...
                try
                {
                    if (NetworkComms.TotalNumConnections() > 0)
                    {
                        foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Connection", "Connected?");
                    }
                    else
                    {
                        NetworkComms.SendObject("Connection", validAddress, serverPort, "Connected?");
                    }
                }
                catch (DPSBase.ConnectionSetupException)
                {
                    MessageBox.Show("Server must be offline?", "Error Connecting");
                    return;
                }
            }

            /* Code for a ping test...
            System.Diagnostics.ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo();
            proc.FileName = @"C:\windows\system32\cmd.exe";
            proc.Arguments = " /c ping " + validAddress;
            System.Diagnostics.Process.Start(proc); 
            ...*/
            
        }       

        // Get Server Connection Response...
        public void Connected(PacketHeader header, Connection connection, string message)
        {
            // Set Form button and LED Image to Connected state...
            SetText(button7connect, "Disconnect");
            statusled.Image = Properties.Resources.greendot;

            // Report our version to server to check for update...
            foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("version", versionIs);            
        }

        // Get version response from server and prompt user for update if needed...
        public void VersionCheck(PacketHeader header, Connection connection, string version)
        {
            if (version == versionIs)
            {
                MessageBox.Show("Jungle Timers is up to date (v" + version +").", "Version Check");
            }
            else
            {
                DialogResult result1 = MessageBox.Show("An updated installation package (" + version + ") is available, Download now?", "Version Check", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    FormCloseForUpdate = true;                    
                    
                    /* Old download method...
                     * Process.Start("https://www.dropbox.com/s/76harst0u0g2iuq/JungleTimers.exe?dl=1"); */

                    Process.Start("Update_Downloader.exe");                    
                                     
                    this.Close();
                    if (this.InvokeRequired)
                        this.Invoke(new MethodInvoker(delegate { this.Close(); }), null);                                     
                }
            }            
        }

        // Delegate to allow me to set the Button text without cross-thread errors...
        private void SetText(Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.BeginInvoke(new SetTextCallback(SetText), ctrl, text);
            }
            else
            {
                ctrl.Text = text;
            }
        }               

        // Delegate to allow me to set Button Enabled Status without cross-thread errors...
        void SetButton(Control ctrl, bool status)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.BeginInvoke(new SetButtonStatus(SetButton), ctrl, status);                
            }
            else
            {
                ctrl.Enabled = status;
            }
            
        }

        // BEGIN TIMERS...        
        private void TimerControl(PacketHeader packetHeader, Connection connection, string message)
        {            
            if (message.ToString() == "b1")
            {
                b1_SongHasPlayed = false;
                b1.RunWorkerAsync();                
            }
            else if(message.ToString() == "b2")
            {
                b2_SongHasPlayed = false;
                b2.RunWorkerAsync();
            }
            else if(message.ToString() == "b3")
            {
                b3_SongHasPlayed = false;
                b3.RunWorkerAsync();
            }
            else if(message.ToString() == "b4")
            {
                b4_SongHasPlayed = false;
                b4.RunWorkerAsync();
            }
            else if(message.ToString() == "b5")
            {
                b5_SongHasPlayed = false;
                b5.RunWorkerAsync();
            }
            else if(message.ToString() == "b6")
            {
                b6_SongHasPlayed = false;
                b6.RunWorkerAsync();
            }
            else if (message.ToString() == "b1STOP")
            {
                b1_SongHasPlayed = false;
                b1.CancelAsync();           
            }
            else if (message.ToString() == "b2STOP")
            {
                b2_SongHasPlayed = false;
                b2.CancelAsync();
            }
            else if (message.ToString() == "b3STOP")
            {
                b3_SongHasPlayed = false;
                b3.CancelAsync();
            }
            else if (message.ToString() == "b4STOP")
            {
                b4_SongHasPlayed = false;
                b4.CancelAsync();
            }
            else if (message.ToString() == "b5STOP")
            {
                b5_SongHasPlayed = false;
                b5.CancelAsync();
            }
            else if (message.ToString() == "b6STOP")
            {
                b6_SongHasPlayed = false;
                b6.CancelAsync();
            }
        }
        
        // BACKGROUND WORKER TIMERS -
        void b1_DoWork(object sender, DoWorkEventArgs b1)
        {            
            BackgroundWorker worker = sender as BackgroundWorker;
            button1.Image = Properties.Resources.redbutton_bw;                        
            PlaySong(PurpleLizardDead);
            for (int a = 299; a > -1; a--)
            {
                if (worker.CancellationPending != true)
                {
                    if (a == WarningSeconds)
                    {
                        PlaySong(PurpleLizardWarning);
                    }
                    Thread.Sleep(1000);                    
                    worker.ReportProgress(a);
                }
            }
        }

        void b2_DoWork(object sender, DoWorkEventArgs b2)
        {            
            BackgroundWorker worker = sender as BackgroundWorker;
            button2.Image = Properties.Resources.bluebutton_bw;                        
            PlaySong2(PurpleGolemDead);
            for (int b = 299; b > -1; b--)
            {
                if (worker.CancellationPending != true)
                {
                    if (b == WarningSeconds)
                    {
                        PlaySong(PurpleGolemWarning);
                    }
                    Thread.Sleep(1000);
                    worker.ReportProgress(b);
                }
            }
        }

        void b3_DoWork(object sender, DoWorkEventArgs b3)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            button3.Image = Properties.Resources.bluebutton_bw;
            PlaySong3(BlueGolemDead);
            for (int c = 299; c > -1; c--)
            {
                if (worker.CancellationPending != true)
                {
                    if (c == WarningSeconds)
                    {
                        PlaySong(BlueGolemWarning);
                    }
                    Thread.Sleep(1000);
                    worker.ReportProgress(c);
                }
            }
        }

        void b4_DoWork(object sender, DoWorkEventArgs b4)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            button4.Image = Properties.Resources.redbutton_bw;
            PlaySong4(BlueLizardDead);
            for (int d = 299; d > -1; d--)
            {
                if (worker.CancellationPending != true)
                {
                    if (d == WarningSeconds)
                    {
                        PlaySong(BlueLizardWarning);
                    }
                    Thread.Sleep(1000);
                    worker.ReportProgress(d);
                }
            }
        }

        void b5_DoWork(object sender, DoWorkEventArgs b5)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            button5.Image = Properties.Resources.baronbutton_bw;
            PlaySong5(BaronDead);
            for (int e = 419; e > -1; e--)
            {
                if (worker.CancellationPending != true)
                {
                    if (e == WarningSeconds)
                    {
                        PlaySong(BaronWarning);
                    }
                    Thread.Sleep(1000);
                    worker.ReportProgress(e);
                }
            }
        }

        void b6_DoWork(object sender, DoWorkEventArgs b6)
        {            
            BackgroundWorker worker = sender as BackgroundWorker;
            button6.Image = Properties.Resources.dragonbutton_bw;
            PlaySong6(DragonDead);
            for (int f = 359; f > -1; f--)
            {
                if (worker.CancellationPending != true)
                {
                    if (f == WarningSeconds)
                    {
                        PlaySong(DragonWarning);
                    }
                    Thread.Sleep(1000);
                    worker.ReportProgress(f);
                }
            }
        }

        void b7_DoWork(object sender, DoWorkEventArgs b7)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            // button7.Image = Properties.Resources.dragonbutton_bw;
            PlaySong6(DragonDead);
            for (int g = 359; g > -1; g--)
            {
                if (worker.CancellationPending != true)
                {
                    if (g == WarningSeconds)
                    {
                        PlaySong(BackgroundMusic);
                    }
                    Thread.Sleep(1000);
                    worker.ReportProgress(g);
                }
            }
        }



        // BACKGROUND WORKER TIMERS PROGRESS - 
        void b1_ProgressChanged(object sender, ProgressChangedEventArgs b1)
        {
            SetText(button1, b1.ProgressPercentage.ToString());
        }

        void b2_ProgressChanged(object sender, ProgressChangedEventArgs b2)
        {
            SetText(button2, b2.ProgressPercentage.ToString());
        }

        void b3_ProgressChanged(object sender, ProgressChangedEventArgs b3)
        {
            SetText(button3, b3.ProgressPercentage.ToString());
        }

        void b4_ProgressChanged(object sender, ProgressChangedEventArgs b4)
        {
            SetText(button4, b4.ProgressPercentage.ToString());
        }

        void b5_ProgressChanged(object sender, ProgressChangedEventArgs b5)
        {
            SetText(button5, b5.ProgressPercentage.ToString());
        }

        void b6_ProgressChanged(object sender, ProgressChangedEventArgs b6)
        {
            SetText(button6, b6.ProgressPercentage.ToString()); 
        }


        // BACKGROUND WORKER TIMERS COMPLETE -
        void b1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs b1)
        {
            PlaySong(PurpleLizardAlive);            
            SetText(button1, "");
            button1.Image = Properties.Resources.redbutton;
            SetText(button1, "");
        }

        void b2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs b2)
        {
            PlaySong2(PurpleGolemAlive);
            SetText(button2, "");
            button2.Image = Properties.Resources.bluebutton;            
            SetText(button2, "");
        }

        void b3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs b3)
        {
            PlaySong3(BlueGolemAlive);
            SetText(button3, "");
            button3.Image = Properties.Resources.bluebutton;
            SetText(button3, "");
        }

        void b4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs b4)
        {
            PlaySong4(BlueLizardAlive);
            SetText(button4, "");
            button4.Image = Properties.Resources.redbutton;
            SetText(button4, "");
        }

        void b5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs b5)
        {
            PlaySong5(BaronAlive);
            SetText(button5, "");
            button5.Image = Properties.Resources.baronbutton;
            SetText(button5, "");
        }

        void b6_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs b6)
        {
            PlaySong6(DragonAlive);
            SetText(button6, "");
            button6.Image = Properties.Resources.dragonbutton;
            SetText(button6, "");
        }

        // hidden test button...
        private void button8_Click(object sender, EventArgs e)
        {
            if (Song7Busy == true)
            {
                // mciSendString("close media7", null, 0, IntPtr.Zero);
                mciSendString("pause media7", null, 0, IntPtr.Zero);
                Song7Busy = false;
                button8.Text = "No Beats";
                button8.ForeColor = Color.Red;               
            }
            else
            {
                // PlaySong7(BackgroundMusic);
                mciSendString("resume media7", null, 0, IntPtr.Zero);
                Song7Busy = true;
                button8.Text = "Gangnam Style!";
                button8.ForeColor = Color.Lime;
            }            
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Disconnection", "Bye!");
            
            // Animate window on closing...(doesn't work with ex.style override)       
            AnimateWindow(this.Handle, 1000, AnimateWindowFlags.AW_BLEND | AnimateWindowFlags.AW_HIDE);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            /* Confirm user wants to close, or not...
             * note: not sure why I put this code here over 5 months ago, could have been to avoid some bug, leaving commented out incase I need it in the future.
            if (button7connect.Text == "Disconnect" && FormCloseForUpdate != true)
            {
                DialogResult result2 = MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo);
                if (result2 == DialogResult.No) {e.Cancel = true;}
                else { try { foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Disconnection", "Bye!"); }
                    finally { } } }
            else if (button7connect.Text == "Disconect" && FormCloseForUpdate == true)
            { foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Disconnection", "Bye!"); } */
        }

        // Cleanup on Close...
        public static void OnApplicationExit(object sender, EventArgs e)
        {            
            NetworkComms.Shutdown();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        public void comboHostAddressBox_TextChanged(object sender, EventArgs e)
        {
            comboHostAddressBox.Enabled = true;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(this);
            frm.ShowDialog();            
            Form1_Load(null, null);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Kriosym/JungleTimers/commits?author=Kriosym");
        }

        // Test Button...
        private void buttonTest_Click(object sender, EventArgs e)
        {
            foreach (var item in ConnectionsList)
            MessageBox.Show(item);

            // if (flowLayoutPanel1_clients.Visible == false) { flowLayoutPanel1_clients.Visible = true; }
            // else { flowLayoutPanel1_clients.Visible = false; }

            /* this.flowLayoutPanel1_clients.Controls.Clear();
            var clientPanel = new Label { Text = "Clients", ForeColor = Color.Gold};
            clientPanel.Font = new Font("Impact", 10, FontStyle.Underline);
            this.flowLayoutPanel1_clients.Controls.Add(clientPanel);

            foreach (var item in ConnectionsList)
            {
                var clientPanelName = new Label { Text = item, ForeColor = Color.Lime };
                this.flowLayoutPanel1_clients.Controls.Add(clientPanelName);
            } */
        }

        private void label_client1_Click(object sender, EventArgs e)
        {

        }

        /* Dynamic Button (for above dynamic panel code)...
        void newButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I was clicked");
            var button = sender as Button;
            button.Click -= new EventHandler(newButton_Click);
            this.panel1.Controls.Remove(button);
        } */

        /* Spam Control Timer, works but blocks all button interaction for too long...
         * Better method will be to add anti-spam control to client preferences for incoming messages from a given client,
         * with option for each client to ignore audio portion or option for server to kick or ban.
        private void timer1_Tick_1(object sender, EventArgs e)
        {             
            if (timerCurrent < timerLength)
            {
                timerCurrent++;
            }
            else
            {   
                timer1.Stop();
                timerCurrent = 0;
            }                    
        }*/

    }
    
}