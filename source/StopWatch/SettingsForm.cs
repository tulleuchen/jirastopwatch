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
using System.Windows.Forms;

namespace StopWatch
{
    public partial class SettingsForm : Form
    {
        public string JiraBaseUrl
        {
            get
            {
                return tbJiraBaseUrl.Text;
            }
            set
            {
                tbJiraBaseUrl.Text = value;
            }
        }
                
        public int IssueCount
        {
            get
            {
                return (int)numIssueCount.Value;
            }
            set
            {
                numIssueCount.Value = value;
            }
        }
                
        public bool AlwaysOnTop
        {
            get
            {
                return cbAlwaysOnTop.Checked;
            }
            set
            {
                cbAlwaysOnTop.Checked = value;
            }
        }
                

        public SettingsForm()
        {
            InitializeComponent();
        }
    }
}
