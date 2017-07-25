/**************************************************************************
Copyright 2016 Carsten Gehling

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
**************************************************************************/
using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace StopWatch
{
    static class Program
    {

        static Mutex mutex = new Mutex(true, "{D5597999-20FE-430F-8E5D-8893EBED2599}");

        static string logPath = Path.Combine(Application.UserAppDataPath, "jirastopwatch.log");

        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);

                Application.Run(new MainForm());

                mutex.ReleaseMutex();
            }
            else {
                // Send Win32 message to make the currently running instance jump on top of all the other windows
                NativeMethods.PostMessage(
                    (IntPtr)NativeMethods.HWND_BROADCAST,
                    NativeMethods.WM_SHOWME,
                    IntPtr.Zero,
                    IntPtr.Zero);
            }
        }


        #region system eventhandlers
        static void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            MainForm form = (MainForm)Application.OpenForms[0];

            if (e.Reason == SessionSwitchReason.SessionLock)
                form.HandleSessionLock();
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
                form.HandleSessionUnlock();
        }


        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            WriteLog("Unhandled Thread Exception");
            WriteLog(e.Exception.Message);
            WriteLog(e.Exception.StackTrace);

            DisplayErrorHandled();
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            WriteLog("Unhandled UI Exception");
            WriteLog((e.ExceptionObject as Exception).Message);
            WriteLog((e.ExceptionObject as Exception).StackTrace);

            DisplayErrorHandled();
        }


        static void DisplayErrorHandled()
        {
            MessageBox.Show(string.Format("Jira StopWatch encountered an unhandled error. A logfile has been created. If the error continues to occur, please send the logfile content to carsten@sarum.dk.\n\nSee more details in the logfile:\n\n{0}", logPath), "Unhandled error occurred");
        }


        static void WriteLog(string message)
        {
            File.AppendAllText(logPath, string.Format("{0}: {1}\n", DateTime.Now, message));
        }
        #endregion
    }
}
