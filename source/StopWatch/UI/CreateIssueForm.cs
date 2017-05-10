using System;
using System.Windows.Forms;

namespace StopWatch
{
    public partial class CreateIssueForm : Form
    {
        private readonly String[] _values = {
            "using System.Collections.Generic",
            "using System.ComponentModel",
            "using System.Data",
            "using System.Drawing",
            "using System.Linq",
            "using System.Text",
            "using System.Threading.Tasks"
        };

        public CreateIssueForm()
        {
            InitializeComponent();

            actbSearchProject.Values = _values;
        }
    }
}
