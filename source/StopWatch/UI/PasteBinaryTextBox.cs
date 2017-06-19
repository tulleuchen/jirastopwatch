using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StopWatch.UI
{
    class PasteBinaryTextBox : TextBox
    {
        public event EventHandler<EventArgs> OnPaste;

        private const int WM_PASTE = 0x0302;

        protected override void WndProc(ref Message m) {
            if (m.Msg == WM_PASTE && !Clipboard.ContainsText())
            {
                OnPaste?.Invoke(this, new EventArgs());
                return;
            }
            base.WndProc(ref m);
        }
    }
}
