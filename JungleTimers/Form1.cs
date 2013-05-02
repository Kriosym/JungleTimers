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

namespace JungleTimers
{
    public partial class Form1 : Form
    {        
        // !! SET CODE REVISION !! 
        public static string versionIs = "1.3";        
        public bool FormCloseForUpdate;               
        
        ResourceManager resources = new ResourceManager(typeof(Form1));

        // Declare Background Workers and other variables...
        BackgroundWorker b1 = new BackgroundWorker();
        BackgroundWorker b2 = new BackgroundWorker();
        BackgroundWorker b3 = new BackgroundWorker();
        BackgroundWorker b4 = new BackgroundWorker();
        BackgroundWorker b5 = new BackgroundWorker();
        BackgroundWorker b6 = new BackgroundWorker();
        IPAddress ipaddr;
        KeyboardHookListener m_KeyboardHookManager = new KeyboardHookListener(new GlobalHooker());        
        string validAddress;
        int serverPort = 11000;
        string ConnectButtonState = "Connect";
        string StartPath = Application.StartupPath;
        bool SongBusy = false;
        
        int timerCurrent;
        int timerLength;
        
        // Inialize Notification MP3 Play History...
        bool b1_SongHasPlayed = false;
        bool b2_SongHasPlayed = false;
        bool b3_SongHasPlayed = false;
        bool b4_SongHasPlayed = false;
        bool b5_SongHasPlayed = false;
        bool b6_SongHasPlayed = false;        

        // Set Path to MP3 Files -                     
        string PurpleGolemDead = Application.StartupPath + "\\Resources\\PurpleGolemDead.mp3";
        string PurpleGolemAlive = Application.StartupPath + "\\Resources\\PurpleGolemAlive.mp3";

        string PurpleLizardDead = Application.StartupPath + "\\Resources\\PurpleLizardDead.mp3";
        string PurpleLizardAlive = Application.StartupPath + "\\Resources\\PurpleLizardAlive.mp3";

        string BaronDead = Application.StartupPath + "\\Resources\\BaronDead.mp3";
        string BaronAlive = Application.StartupPath + "\\Resources\\BaronAlive.mp3";

        string DragonDead = Application.StartupPath + "\\Resources\\DragonDead.mp3";
        string DragonAlive = Application.StartupPath + "\\Resources\\DragonAlive.mp3";

        string BlueGolemDead = Application.StartupPath + "\\Resources\\BlueGolemDead.mp3";
        string BlueGolemAlive = Application.StartupPath + "\\Resources\\BlueGolemAlive.mp3";

        string BlueLizardDead = Application.StartupPath + "\\Resources\\BlueLizardDead.mp3";
        string BlueLizardAlive = Application.StartupPath + "\\Resources\\BlueLizardAlive.mp3";
        
        // Import DLL for MP3 Playback...
        public const int MM_MCINOTIFY = 0x3B9;
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder returnString, int returnSize, IntPtr hwndCallback);

        // Declare Delegate to allow me to set button text without cross-thread errors...
        delegate void SetTextCallback(Control ctrl, string text);
        
        // Declare Delegate to allow me to set button enabled state without cross-thread errors...
        delegate void SetButtonStatus(Control ctrl, bool status);

        // Load Actions...
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "v" + versionIs;
        }
        
        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            
            InitializeComponent();
            
            //Prevent Unhandled Exception incase of rogue packet reception...
            NetworkComms.IgnoreUnknownPacketTypes = true;

            // Trigger the methods when a packets are received from Server...    
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("Connected", Connected);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("TimerControl", TimerControl);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("version", VersionCheck);
                        
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

        // HOTKEY EVENTS...
        void m_KeyboardHookManager_KeyUp(object sender, KeyEventArgs e)
        {
            //Trace.WriteLine("Key: " + e.KeyCode);
            if (e.KeyCode == Keys.NumPad2)
            {
                button1_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad1)
            {
                button2_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad3)
            {
                button3_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad4)
            {
                button4_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad5)
            {
                button5_Click(null, null);
            }
            else if (e.KeyCode == Keys.NumPad6)
            {
                button6_Click(null, null);
            }
        }        

        // Play MP3s override...        
        protected override void DefWndProc(ref Message m)
       {
            base.DefWndProc(ref m);
            if (m.Msg == MM_MCINOTIFY)
            {
                SongBusy = false;
            }
        }

        // Cross-thread delegate to play MP3s using mciSendString...
        public delegate void delegatePlaySong(string file);
        public void PlaySong(string file)
        {   
            if (this.InvokeRequired)
            {
                this.Invoke(new delegatePlaySong(PlaySong), file);
            }
            else
            {
                SongBusy = true;
                mciSendString("close media", null, 0, IntPtr.Zero);                
                mciSendString("open \"" + file + "\" type mpegvideo alias media", null, 0, IntPtr.Zero);
                mciSendString("play media notify", null, 0, this.Handle);
            }
        }

        /* A little timer...
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timerCurrent < timerLength)
            {
                timerCurrent++;
            }
            else
            {             
                button1.Enabled = true;
                timer1.Stop();
            }
        }
         */

        // BUTTON CLICK EVENTS -
        private void button1_Click(object sender, EventArgs e)
        {                       
            // Start the Timer if it isn't already going, using Client/Server mechanism if connected...
            // note: button7connect.Text reads "Connect" prior to connecting, and reads "Disconnect" once connection has been established.
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
        }          

        private void button2_Click(object sender, EventArgs e)
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
        }

        private void button3_Click(object sender, EventArgs e)
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
        }

        private void button4_Click(object sender, EventArgs e)
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
        }

        private void button5_Click(object sender, EventArgs e)
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
        }

        private void button6_Click(object sender, EventArgs e)
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
        public void VersionCheck(PacketHeader header, Connection connection, string message)
        {
            if (message == versionIs)
            {
                MessageBox.Show("Jungle Timers is up to date (v" + message +").", "Version Check");
            }
            else
            {
                DialogResult result1 = MessageBox.Show("An updated installation package (v" + message + ") is available, Download now?", "Version Check", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    FormCloseForUpdate = true;                    
                    Process.Start("https://www.dropbox.com/s/76harst0u0g2iuq/JungleTimers.exe?dl=1");
                    // MessageBox.Show("Manually execute JungleTimers.exe installer once it finishes downloading", "NOTICE!");
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
            button1.Image = Properties.Resources.bluebutton_bw;                        
            PlaySong(PurpleGolemDead);
            for (int a = 299; a > -1; a--)
            {
                if (worker.CancellationPending != true)
                {
                    Thread.Sleep(1000);
                    worker.ReportProgress(a);
                }
            }
        }

        void b2_DoWork(object sender, DoWorkEventArgs b2)
        {            
            BackgroundWorker worker = sender as BackgroundWorker;
            button2.Image = Properties.Resources.redbutton_bw;                        
            PlaySong(PurpleLizardDead);
            for (int b = 299; b > -1; b--)
            {
                if (worker.CancellationPending != true)
                {
                    Thread.Sleep(1000);
                    worker.ReportProgress(b);
                }
            }
        }

        void b3_DoWork(object sender, DoWorkEventArgs b3)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            button3.Image = Properties.Resources.bluebutton_bw;
            PlaySong(BlueGolemDead);
            for (int c = 299; c > -1; c--)
            {
                if (worker.CancellationPending != true)
                {
                    Thread.Sleep(1000);
                    worker.ReportProgress(c);
                }
            }
        }

        void b4_DoWork(object sender, DoWorkEventArgs b4)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            button4.Image = Properties.Resources.redbutton_bw;
            PlaySong(BlueLizardDead);
            for (int d = 299; d > -1; d--)
            {
                if (worker.CancellationPending != true)
                {
                    Thread.Sleep(1000);
                    worker.ReportProgress(d);
                }
            }
        }

        void b5_DoWork(object sender, DoWorkEventArgs b5)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            button5.Image = Properties.Resources.baronbutton_bw;
            PlaySong(BaronDead);
            for (int e = 419; e > -1; e--)
            {
                if (worker.CancellationPending != true)
                {
                    Thread.Sleep(1000);
                    worker.ReportProgress(e);
                }
            }
        }

        void b6_DoWork(object sender, DoWorkEventArgs b6)
        {            
            BackgroundWorker worker = sender as BackgroundWorker;
            button6.Image = Properties.Resources.dragonbutton_bw;
            PlaySong(DragonDead);
            for (int f = 359; f > -1; f--)
            {
                if (worker.CancellationPending != true)
                {
                    Thread.Sleep(1000);
                    worker.ReportProgress(f);
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
            PlaySong(PurpleGolemAlive);            
            SetText(button1, "");
            button1.Image = Properties.Resources.bluebutton;
            SetText(button1, "");
        }

        void b2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs b2)
        {
            PlaySong(PurpleLizardAlive);
            SetText(button2, "");
            button2.Image = Properties.Resources.redbutton;            
            SetText(button2, "");
        }

        void b3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs b3)
        {
            PlaySong(BlueGolemAlive);
            SetText(button3, "");
            button3.Image = Properties.Resources.bluebutton;
            SetText(button3, "");
        }

        void b4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs b4)
        {
            PlaySong(BlueLizardAlive);
            SetText(button4, "");
            button4.Image = Properties.Resources.redbutton;
            SetText(button4, "");
        }

        void b5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs b5)
        {
            PlaySong(BaronAlive);
            SetText(button5, "");
            button5.Image = Properties.Resources.baronbutton;
            SetText(button5, "");
        }

        void b6_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs b6)
        {
            PlaySong(DragonAlive);
            SetText(button6, "");
            button6.Image = Properties.Resources.dragonbutton;
            SetText(button6, "");
        }

        // hidden test button...
        private void button8_Click(object sender, EventArgs e)
        {
            NetworkComms.SendObject("Disconnection", validAddress, serverPort, "Bye!");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close, or not...           
            if (button7connect.Text == "Disconnect" && FormCloseForUpdate != true)
            {
                DialogResult result2 = MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo);
                if (result2 == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    try
                    {
                        foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Disconnection", "Bye!");
                    }
                    finally
                    {

                    }
                }                    
            }
            else if (button7connect.Text == "Disconect" && FormCloseForUpdate == true)
            {
                foreach (var item in NetworkComms.GetExistingConnection()) item.SendObject("Disconnection", "Bye!");
            }                           
        }

        // Cleanup on Close...
        public static void OnApplicationExit(object sender, EventArgs e)
        {            
            NetworkComms.Shutdown();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void comboHostAddressBox_TextChanged(object sender, EventArgs e)
        {
            comboHostAddressBox.Enabled = true;
        }

    }
    
}