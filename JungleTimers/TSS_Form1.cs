using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Capture;
using Capture.Hook;
using Capture.Interface;
using EasyHook;

namespace JungleTimers
{
    public class TSS_Injector 
    {
        public void Inject()
        {
            if (_captureProcess == null)
            {
                //btnInject.Enabled = false;

                AttachProcess();
            }
            else
            {
                HookManager.RemoveHookedProcess(_captureProcess.Process.Id);
                _captureProcess.CaptureInterface.Disconnect();
                _captureProcess = null;
            }

            if (_captureProcess != null)
            {
                //btnInject.Text = "Detach";
                //btnInject.Enabled = true;
            }
            else
            {
                //btnInject.Text = "Inject";
                //btnInject.Enabled = true;
            }
        }

        int processId = 0;
        Process _process;
        CaptureProcess _captureProcess;
        private void AttachProcess()
        {
            string exeName = Path.GetFileNameWithoutExtension("mpc-hc");
            
            Process[] processes = Process.GetProcessesByName(exeName);
            foreach (Process process in processes)
            {
                // Simply attach to the first one found.

                // If the process doesn't have a mainwindowhandle yet, skip it (we need to be able to get the hwnd to set foreground etc)
                if (process.MainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }

                // Skip if the process is already hooked (and we want to hook multiple applications)
                if (HookManager.IsHooked(process.Id))
                {
                    continue;
                }


                processId = process.Id;
                CreateCaptureProcess(process);

                break;
            }
            Thread.Sleep(10);

            if (_captureProcess == null)
            {
                MessageBox.Show("No executable found matching: '" + exeName + "'");
            }
            else
            {
                //btnLoadTest.Enabled = true;
                //btnCapture.Enabled = true;
            }
        }

        private void CreateCaptureProcess(Process process)
        {
            _process = process;

            Direct3DVersion direct3DVersion = Direct3DVersion.Direct3D10;

            /*if (rbDirect3D11.Checked)
            {
                direct3DVersion = Direct3DVersion.Direct3D11;
            }
            else if (rbDirect3D10_1.Checked)
            {
                direct3DVersion = Direct3DVersion.Direct3D10_1;
            }
            else if (rbDirect3D10.Checked)
            {
                direct3DVersion = Direct3DVersion.Direct3D10;
            }
            else if (rbDirect3D9.Checked)
            {*/
                direct3DVersion = Direct3DVersion.Direct3D9;
            /*}
            else if (rbAutodetect.Checked)
            {
                direct3DVersion = Direct3DVersion.AutoDetect;
            }*/

            var cc = new CaptureConfig
                      {
                          Direct3DVersion = direct3DVersion,
                          ShowOverlay = true,
                          TestThisShit = 300
                      };

            var captureInterface = new CaptureInterface();

            if (frm1 != null)
            {
                frm1.Close();
                frm1.Dispose();
            }

            //frm1 = new form2JT(captureInterface);
            
            captureInterface.RemoteMessage += CaptureInterface_RemoteMessage;
            _captureProcess = new CaptureProcess(process, cc, captureInterface);
        }

        /// <summary>
        /// Display messages from the target process
        /// </summary>
        /// <param name="message"></param>
        void CaptureInterface_RemoteMessage(MessageReceivedEventArgs message)
        {
            
            /*txtDebugLog.Invoke(new MethodInvoker(delegate()
                {
                    txtDebugLog.Text = String.Format("{0}\r\n{1}", message, txtDebugLog.Text);
                })
            );*/
        }

        /// <summary>
        /// Display debug messages from the target process
        /// </summary>
        /// <param name="clientPID"></param>
        /// <param name="message"></param>
        void ScreenshotManager_OnScreenshotDebugMessage(int clientPID, string message)
        {
            /*txtDebugLog.Invoke(new MethodInvoker(delegate()
                {
                    txtDebugLog.Text = String.Format("{0}:{1}\r\n{2}", clientPID, message, txtDebugLog.Text);
                })
            );*/
        }

        DateTime start;
        DateTime end;
        private Form1 frm1;

        private void btnCapture_Click(object sender, EventArgs e)
        {
            /*start = DateTime.Now;
            progressBar1.Maximum = 1;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            DoRequest();*/
        }

        private void btnLoadTest_Click(object sender, EventArgs e)
        {
            // Note: we bring the target application into the foreground because
            //       windowed Direct3D applications have a lower framerate 
            //       if not the currently focused window
            _captureProcess.BringProcessWindowToFront();
            start = DateTime.Now;
            /*progressBar1.Maximum = Convert.ToInt32(txtNumber.Text);
            progressBar1.Minimum = 0;
            progressBar1.Step = 1;
            progressBar1.Value = 0;*/
            DoRequest();
        }

        /// <summary>
        /// Create the screen shot request
        /// </summary>
        void DoRequest()
        {
            /*progressBar1.Invoke(new MethodInvoker(delegate()
                {
                    if (progressBar1.Value < progressBar1.Maximum)
                    {
                        progressBar1.PerformStep();

                        _captureProcess.BringProcessWindowToFront();
                        // Initiate the screenshot of the CaptureInterface, the appropriate event handler within the target process will take care of the rest
                        _captureProcess.CaptureInterface.BeginGetScreenshot(new Rectangle(int.Parse(txtCaptureX.Text), int.Parse(txtCaptureY.Text), int.Parse(txtCaptureWidth.Text), int.Parse(txtCaptureHeight.Text)), new TimeSpan(0, 0, 2), Callback);
                    }
                    else
                    {
                        end = DateTime.Now;
                        txtDebugLog.Text = String.Format("Debug: {0}\r\n{1}", "Total Time: " + (end-start).ToString(), txtDebugLog.Text);
                    }
                })
            );*/
        }

        /// <summary>
        /// The callback for when the screenshot has been taken
        /// </summary>
        /// <param name="clientPID"></param>
        /// <param name="status"></param>
        /// <param name="screenshotResponse"></param>
        void Callback(IAsyncResult result)
        {
            Screenshot screenshot = _captureProcess.CaptureInterface.EndGetScreenshot(result);
            try
            {
                _captureProcess.CaptureInterface.DisplayInGameText("Screenshot captured...");
                if (screenshot != null && screenshot.CapturedBitmap != null)
                {
                    /*pictureBox1.Invoke(new MethodInvoker(delegate()
                    {
                        if (pictureBox1.Image != null)
                        {
                            pictureBox1.Image.Dispose();
                        }
                        pictureBox1.Image = screenshot.CapturedBitmap.ToBitmap();
                    })
                    );*/
                }

                Thread t = new Thread(new ThreadStart(DoRequest));
                t.Start();
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (frm1 == null)
            {
                MessageBox.Show("Inject First");
                return;
            }

            frm1.Show();
        }
    }
}
