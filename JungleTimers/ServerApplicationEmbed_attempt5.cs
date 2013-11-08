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
    public partial class ServerApplicationEmbed_attempt5 : Form
    {
        public ServerApplicationEmbed_attempt5()
        {
            InitializeComponent();
        }

        private void ServerApplicationEmbed_attempt5_Load(object sender, EventArgs e)
        {
            consoleControl1.StartProcess("ServerApplication.exe", null);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            consoleControl1.StopProcess();

            base.OnHandleDestroyed(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            

            base.OnFormClosing(e);
        }
    }
}
