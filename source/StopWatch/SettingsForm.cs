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
using System.Linq;
using System.Windows.Forms;

namespace StopWatch
{
    public enum SaveTimerSetting
    {
        NoSave,
        SavePause,
        SaveRunActive
    }


    internal partial class SettingsForm : Form
    {
        #region public members
        public Settings settings { get; private set; }
        #endregion


        #region public methods
        public SettingsForm(Settings settings)
        {
            this.settings = settings;

            InitializeComponent();

            rbNoSave.Tag = SaveTimerSetting.NoSave;
            rbSavePause.Tag = SaveTimerSetting.SavePause;
            rbSaveRunActive.Tag = SaveTimerSetting.SaveRunActive;

            tbJiraBaseUrl.Text = this.settings.JiraBaseUrl;
            numIssueCount.Value = this.settings.IssueCount;
            cbAlwaysOnTop.Checked = this.settings.AlwaysOnTop;
            cbTimerEditable.Checked = this.settings.TimerEditable;

            RadioButton choice = gbSaveTimerState.Controls.OfType<RadioButton>().FirstOrDefault(x => (SaveTimerSetting)x.Tag == settings.SaveTimerState);
            choice.Checked = true;
        }
        #endregion


        #region private eventhandlers
        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.settings.JiraBaseUrl = tbJiraBaseUrl.Text;
                this.settings.IssueCount = (int)numIssueCount.Value;
                this.settings.AlwaysOnTop = cbAlwaysOnTop.Checked;
                this.settings.TimerEditable = cbTimerEditable.Checked;

                RadioButton choice = gbSaveTimerState.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Checked);
                this.settings.SaveTimerState = (SaveTimerSetting)choice.Tag;
            }
        }
        #endregion

    }
}
