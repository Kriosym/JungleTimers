using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JungleTimers
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += Form1.OnApplicationExit;
            try
            {
                Application.Run(new Form1());
            }
            catch (InvalidOperationException exception)
            {
                Trace.WriteLine(exception);
            }
        }
    }
}
