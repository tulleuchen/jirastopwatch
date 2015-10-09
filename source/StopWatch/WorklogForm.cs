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
    public partial class WorklogForm : Form
    {
        #region public members
        public string Comment
        {
            get
            {
                return tbComment.Text;
            }
        }
        #endregion


        #region public methods
        public WorklogForm()
        {
            InitializeComponent();
        }
        #endregion


        #region private eventhandlers
        private void tbComment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.Enter))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
        #endregion
    }
}
