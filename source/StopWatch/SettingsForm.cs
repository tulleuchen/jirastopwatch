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
using System.Linq;

namespace StopWatch
{
    public enum SaveTimerSetting
    {
        NoSave,
        SavePause,
        SaveRunActive
    }


    public partial class SettingsForm : Form
    {
        #region public members
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
        #endregion


        public SaveTimerSetting SaveTimerState
        {
            get
            {
                RadioButton choice = gbSaveTimerState.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Checked);
                return (SaveTimerSetting)choice.Tag;
            }

            set
            {
                RadioButton choice = gbSaveTimerState.Controls.OfType<RadioButton>().FirstOrDefault(x => (SaveTimerSetting)x.Tag == value);
                choice.Checked = true;
            }
        }


        public SettingsForm()
        {
            InitializeComponent();

            rbNoSave.Tag = SaveTimerSetting.NoSave;
            rbSavePause.Tag = SaveTimerSetting.SavePause;
            rbSaveRunActive.Tag = SaveTimerSetting.SaveRunActive;
        }
    }
}
