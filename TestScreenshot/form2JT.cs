using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Resources;
using System.IO;
using Capture.Interface;
using Capture.Hook;
using Capture;
using EasyHook;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting;

namespace TestScreenshot
{
    public partial class form2JT : Form
    {
        public CaptureInterface CaptureInterface { get; set; }

        public BackgroundWorker b1 = new BackgroundWorker();
        delegate void SetTextCallback(Control ctrl, string text);          
            
        private void form2JT_Load(object sender, EventArgs e)
        {
            // Initialize the Background Worker...
            b1.WorkerReportsProgress = true;
            b1.DoWork += new DoWorkEventHandler(b1_DoWork);
            b1.ProgressChanged += new ProgressChangedEventHandler(b1_ProgressChanged);
            b1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(b1_RunWorkerCompleted);
            b1.WorkerSupportsCancellation = true;
        }
       
        public form2JT(CaptureInterface captureInterface)
        {
            this.CaptureInterface = captureInterface;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

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

        private void timer1_Tick(object sender, EventArgs e)
        {
                    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (buttonStartTimer.Text == "Start Timer")
            {
                buttonStartTimer.Text = "Stop Timer";
                b1.RunWorkerAsync();
            }
            else if (buttonStartTimer.Text == "Stop Timer")
            {
                b1.CancelAsync();
                buttonStartTimer.Text = "Start Timer";
            }
        }

        void b1_DoWork(object sender, DoWorkEventArgs b1)
        {
            BackgroundWorker worker = sender as BackgroundWorker;            
            for (int a = 299; a > -1; a--)
            {
                if (worker.CancellationPending != true)
                {
                    System.Threading.Thread.Sleep(1000);
                    worker.ReportProgress(a);
                }
            }
        }        

        void b1_ProgressChanged(object sender, ProgressChangedEventArgs b1)
        {
            TimeSpan t = TimeSpan.FromSeconds(b1.ProgressPercentage);
            string b1TimeFormatted = string.Format("{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
            SetText(labelTimeText, b1TimeFormatted.ToString());
            this.CaptureInterface.DisplayInGameText(b1TimeFormatted.ToString());
        }

        void b1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs b1)
        {
            labelTimeText.Text = "300";
        }     
        

    }
}
