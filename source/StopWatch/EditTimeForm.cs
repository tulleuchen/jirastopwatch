using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StopWatch
{
    public partial class EditTimeForm : Form
    {
        public TimeSpan Time { get; private set; }


        public EditTimeForm(TimeSpan time)
        {
            InitializeComponent();

            tbTime.BackColor = SystemColors.Window;

            Time = time;

            tbTime.Text = JiraTimeHelpers.TimeSpanToJiraTime(Time);
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateTimeInput())
            {
                DialogResult = DialogResult.None;
                tbTime.BackColor = Color.Tomato;
                return;
            }

            tbTime.BackColor = SystemColors.Window;
        }


        private bool ValidateTimeInput()
        {
            TimeSpan? time = JiraTimeHelpers.JiraTimeToTimeSpan(tbTime.Text);
            if (time == null)
                return false;

            Time = time.Value;

            return true;
        }

        private void tbTime_TextChanged(object sender, EventArgs e)
        {
            tbTime.BackColor = SystemColors.Window;
        }
    }
}
