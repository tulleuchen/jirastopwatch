using Microsoft.Win32;
/**************************************************************************
Copyright 2015 Carsten Gehling

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
using System;
using System.Windows.Forms;

namespace StopWatch
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

            Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);

            Application.Run(new MainForm());
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
        #endregion
    }
}
