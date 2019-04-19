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
using StopWatch.Logging;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace StopWatch
{


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

            // Mono for MacOSX and Linux do not implement the notifyIcon
            // so ignore this feature if we are not running on Windows
            cbMinimizeToTray.Visible = CrossPlatformHelpers.IsWindowsEnvironment();

            tbJiraBaseUrl.Text = this.settings.JiraBaseUrl;
            tbUsername.Text = settings.Username;
            tbApiToken.Text = settings.ApiToken;

            cbAlwaysOnTop.Checked = this.settings.AlwaysOnTop;
            cbMinimizeToTray.Checked = this.settings.MinimizeToTray;
            cbAllowMultipleTimers.Checked = this.settings.AllowMultipleTimers;
            cbIncludeProjectName.Checked = this.settings.IncludeProjectName;

            cbSaveTimerState.DisplayMember = "Text";
            cbSaveTimerState.ValueMember = "Value";
            cbSaveTimerState.DataSource = new[]
            {
                new { Text = "Reset all timers on exit", Value = SaveTimerSetting.NoSave },
                new { Text = "Save current timetracking, pause active timer", Value = SaveTimerSetting.SavePause },
                new { Text = "Save current timetracking, active timer continues", Value = SaveTimerSetting.SaveRunActive }
            };
            cbSaveTimerState.SelectedValue = this.settings.SaveTimerState;

            cbPauseOnSessionLock.DisplayMember = "Text";
            cbPauseOnSessionLock.ValueMember = "Value";
            cbPauseOnSessionLock.DataSource = new[]
            {
                new { Text = "No pause", Value = PauseAndResumeSetting.NoPause },
                new { Text = "Pause active timer", Value = PauseAndResumeSetting.Pause },
                new { Text = "Pause and resume on unlock", Value = PauseAndResumeSetting.PauseAndResume }
            };
            cbPauseOnSessionLock.SelectedValue = this.settings.PauseOnSessionLock;

            cbPostWorklogComment.DisplayMember = "Text";
            cbPostWorklogComment.ValueMember = "Value";
            cbPostWorklogComment.DataSource = new[]
            {
                new { Text = "Post only as part of worklog", Value = WorklogCommentSetting.WorklogOnly },
                new { Text = "Post only as a comment", Value = WorklogCommentSetting.CommentOnly },
                new { Text = "Post as both worklog and comment", Value = WorklogCommentSetting.WorklogAndComment }
            };
            cbPostWorklogComment.SelectedValue = this.settings.PostWorklogComment;

            tbStartTransitions.Text = this.settings.StartTransitions;

            cbLoggingEnabbled.Checked = this.settings.LoggingEnabled;

            cbCheckForUpdate.Checked = settings.CheckForUpdate;
        }
        #endregion


        #region private eventhandlers
        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.settings.JiraBaseUrl = tbJiraBaseUrl.Text;
                settings.Username = tbUsername.Text;
                settings.ApiToken = tbApiToken.Text;

                this.settings.AlwaysOnTop = cbAlwaysOnTop.Checked;
                this.settings.MinimizeToTray = cbMinimizeToTray.Checked;
                this.settings.AllowMultipleTimers = cbAllowMultipleTimers.Checked;
                this.settings.IncludeProjectName = cbIncludeProjectName.Checked;

                this.settings.SaveTimerState = (SaveTimerSetting)cbSaveTimerState.SelectedValue;
                this.settings.PauseOnSessionLock = (PauseAndResumeSetting)cbPauseOnSessionLock.SelectedValue;
                this.settings.PostWorklogComment = (WorklogCommentSetting)cbPostWorklogComment.SelectedValue;

                this.settings.StartTransitions = tbStartTransitions.Text;

                this.settings.LoggingEnabled = cbLoggingEnabbled.Checked;
                settings.CheckForUpdate = cbCheckForUpdate.Checked;
            }
        }


        private void btnAbout_Click(object sender, System.EventArgs e)
        {
            using (var aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog();
            }
        }
        #endregion

        private void lblOpenLogFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Path.GetDirectoryName(Logger.Instance.LogfilePath));
        }

        private void lblOpenAPITokensPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://id.atlassian.com/manage/api-tokens");
        }
    }
}
