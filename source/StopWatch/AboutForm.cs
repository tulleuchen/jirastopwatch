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
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            var v = Application.ProductVersion;
            v = v.Substring(0, v.LastIndexOf('.'));
            lblNameVersion.Text = string.Format("{0} v. {1}", Application.ProductName, v);
        }

        private void lblLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.apache.org/licenses/LICENSE-2.0");
        }

        private void lblHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://carstengehling.github.io/jirastopwatch");
        }
    }
}
