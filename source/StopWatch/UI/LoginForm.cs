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
using System.Windows.Forms;

namespace StopWatch
{
    public partial class LoginForm : Form
    {
        #region public members
        public string Username
        {
            get
            {
                return tbUsername.Text;
            }
            set
            {
                tbUsername.Text = value;
            }
        }

        public string Password
        {
            get
            {
                return tbPassword.Text;
            }
            set
            {
                tbPassword.Text = value;
            }
        }

        public bool Remember
        {
            get
            {
                return cbRemember.Checked;
            }
            set
            {
                cbRemember.Checked = value;
            }
        }
        #endregion


        #region public methods
        public LoginForm()
        {
            InitializeComponent();
        }
        #endregion
    }
}
