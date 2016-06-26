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
        public WorklogForm(string comment)
        {
            InitializeComponent();

            if (!String.IsNullOrEmpty(comment))
            {
                tbComment.Text = String.Format("{0}{0}{1}", Environment.NewLine, comment);
                tbComment.SelectionStart = 0;
            }
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
