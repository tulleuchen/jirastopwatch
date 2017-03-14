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
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace StopWatch
{
    public class ComboTextBoxEvents : NativeWindow
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        public event EventHandler<EventArgs> Paste;

        public event EventHandler<EventArgs> MouseDown;

        public ComboTextBoxEvents(ComboBox comboBox)
        {
            if (comboBox == null) throw new ArgumentNullException("comboBox");

            _comboBox = comboBox;
            IntPtr lhWnd = FindWindowEx(_comboBox.Handle, IntPtr.Zero, "EDIT", null);
            AssignHandle(lhWnd);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PASTE)
            {
                if (Clipboard.ContainsText())
                    Paste?.Invoke(this, new EventArgs());
                return;
            }

            if (m.Msg == WM_LBUTTONDOWN)
                MouseDown?.Invoke(this, new EventArgs());

            base.WndProc(ref m);
        }

        private const int WM_PASTE = 0x0302;
        private const int WM_LBUTTONDOWN = 0x0201;

        private ComboBox _comboBox;
    }
}
