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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StopWatch
{
    public partial class MainForm : Form
    {
        #region public methods
        public MainForm()
        {
            InitializeComponent();

            settings = new Settings();
            jiraClient = new JiraClient(new RestClientFactory(), new RestRequestFactory());

            cbFilters.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFilters.DisplayMember = "Name";

            LoadSettings();

            ticker = new Timer();
            // First run should be almost immediately after start
            ticker.Interval = firstDelay;
            ticker.Tick += ticker_Tick;
        }


        public void HandleSessionLock()
        {
            if (settings.PauseOnSessionLock == PauseAndResumeSetting.NoPause)
                return;

            foreach (var issue in issueControls)
            {
                if (issue.WatchTimer.Running)
                {
                    lastRunningIssue = issue;
                    issue.InvokeIfRequired(
                        () => issue.Pause()
                    );
                    return;
                }
            }
        }


        public void HandleSessionUnlock()
        {
            if (settings.PauseOnSessionLock != PauseAndResumeSetting.PauseAndResume)
                return;

            if (lastRunningIssue != null)
            {
                lastRunningIssue.InvokeIfRequired(
                    () => lastRunningIssue.Start()
                );
                lastRunningIssue = null;
            }
        }
        #endregion


        #region private eventhandlers
        void issue_TimerStarted(object sender, EventArgs e)
        {
            IssueControl senderCtrl = (IssueControl)sender;

            foreach (var issue in this.issueControls)
                if (issue != senderCtrl)
                    issue.Pause();
        }


        void ticker_Tick(object sender, EventArgs e)
        {
            bool firstTick = ticker.Interval == firstDelay;

            ticker.Interval = defaultDelay;

            UpdateJiraRelatedData(firstTick);
            UpdateIssuesOutput(firstTick);
        }


        private void pbSettings_Click(object sender, EventArgs e)
        {
            if (settings.AlwaysOnTop)
                this.TopMost = false;
            EditSettings();
            if (settings.AlwaysOnTop)
                this.TopMost = settings.AlwaysOnTop;
        }


        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Copy issueControl keys and timer states to settings and save settings
            CollectIssueKeysAndStates();

            SaveSettings();
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (this.settings.FirstRun)
            {
                this.settings.FirstRun = false;

                EditSettings();
                JiraLogin();
            }
            else
            {
                if (IsJiraEnabled)
                    AuthenticateJira(this.settings.Username, this.settings.Password);
            }

            InitializeIssueControls();

            // Add issuekeys from settings to issueControl controls
            int i = 0;
            foreach (var issueControl in this.issueControls)
            {
                if (i < settings.PersistedIssues.Count)
                {
                    var persistedIssue = settings.PersistedIssues[i];
                    issueControl.IssueKey = persistedIssue.Key;

                    if (this.settings.SaveTimerState != SaveTimerSetting.NoSave)
                    {
                        TimerState timerState = new TimerState
                        {
                            Running = this.settings.SaveTimerState == SaveTimerSetting.SavePause ? false : persistedIssue.TimerRunning,
                            StartTime = persistedIssue.StartTime,
                            TotalTime = persistedIssue.TotalTime
                        };
                        issueControl.WatchTimer.SetState(timerState);
                        issueControl.Comment = persistedIssue.Comment;
                    }
                }
                i++;
            }

            ticker.Start();
        }


        private void pbLogin_Click(object sender, EventArgs e)
        {
            if (settings.AlwaysOnTop)
                this.TopMost = false;
            JiraLogin();
            if (settings.AlwaysOnTop)
                this.TopMost = settings.AlwaysOnTop;
        }


        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (CBFilterItem)cbFilters.SelectedItem;
            this.settings.CurrentFilter = item.Id;

            string jql = item.Jql;

            Task.Factory.StartNew(
                () =>
                {
                    List<Issue> availableIssues = jiraClient.GetIssuesByJQL(jql);

                    if (availableIssues == null)
                        return;

                    this.InvokeIfRequired(
                        () =>
                        {
                            foreach (var issueControl in this.issueControls)
                                issueControl.AvailableIssues = availableIssues;
                        }
                    );
                }
            );
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Mono for MacOSX and Linux do not implement the notifyIcon
            // so ignore this feature if we are not running on Windows
            if (!CrossPlatformHelpers.IsWindowsEnvironment())
                return;

            if (this.settings.MinimizeToTray && WindowState == FormWindowState.Minimized)
            {
                this.notifyIcon.Visible = true;
                this.Hide();
            }
            else if (WindowState == FormWindowState.Normal)
            {
                this.notifyIcon.Visible = false;
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        #endregion


        #region private methods
        private void AuthenticateJira(string username, string password)
        {
            Task.Factory.StartNew(
                () =>
                {
                    this.InvokeIfRequired(
                        () =>
                        {
                            lblConnectionStatus.Text = "Connecting...";
                            lblConnectionStatus.ForeColor = SystemColors.ControlText;
                        }
                    );

                    if (jiraClient.Authenticate(username, password))
                        UpdateIssuesOutput(true);
                    UpdateJiraRelatedData(true);
                }
            );
        }


        private void InitializeIssueControls()
        {
            this.SuspendLayout();

            // If we have too many issueControl controls, compared to this.IssueCount
            // remove the ones not needed
            while (this.issueControls.Count() > this.settings.IssueCount)
            {
                var issue = this.issueControls.Last();
                this.Controls.Remove(issue);
            }

            // Create issueControl controls needed
            while (this.issueControls.Count() < this.settings.IssueCount)
            {
                var issue = new IssueControl(this.jiraClient);
                issue.TimerStarted += issue_TimerStarted;
                this.Controls.Add(issue);
            }

            // Position all issueControl controls and set TimerEditable
            int i = 0;
            foreach (var issue in this.issueControls)
            {
                issue.Left = 12;
                issue.Top = i * issue.Height + 12;
                issue.TimerEditable = this.settings.TimerEditable;
                i++;
            }

            // Resize form and reposition settings button
            this.ClientSize = new Size(issueControls.Last().Width + 24, this.settings.IssueCount * issueControls.Last().Height + 46);

            pbSettings.Left = this.ClientSize.Width - 28;
            pbSettings.Top = this.ClientSize.Height - 28;

            pbLogin.Left = 8;
            pbLogin.Top = this.ClientSize.Height - 28;

            lblConnectionHeader.Top = this.ClientSize.Height - 22;
            lblConnectionStatus.Top = this.ClientSize.Height - 22;

            lblActiveFilter.Left = this.ClientSize.Width - 320;
            lblActiveFilter.Top = this.ClientSize.Height - 24;

            cbFilters.Left = this.ClientSize.Width - 280;
            cbFilters.Top = this.ClientSize.Height - 28;

            this.TopMost = this.settings.AlwaysOnTop;

            this.ResumeLayout(false);
            this.PerformLayout();
        }


        private void UpdateIssuesOutput(bool updateSummary = false)
        {
            foreach (var issue in this.issueControls)
                issue.UpdateOutput(updateSummary);
        }


        private void UpdateJiraRelatedData(bool firstTick)
        {
            Task.Factory.StartNew(
                () =>
                {
                    if (!IsJiraEnabled)
                    {
                        this.InvokeIfRequired(
                            () =>
                            {
                                lblConnectionStatus.Text = "Not setup yet";
                                lblConnectionStatus.ForeColor = SystemColors.ControlText;
                            }
                        );
                        return;
                    }

                    if (jiraClient.SessionValid || jiraClient.ValidateSession())
                    {
                        this.InvokeIfRequired(
                            () =>
                            {
                                lblConnectionStatus.Text = "Connected";
                                lblConnectionStatus.ForeColor = Color.DarkGreen;

                                LoadFilterList();

                                UpdateIssuesOutput(firstTick);
                            }
                        );
                        return;
                    }

                    this.InvokeIfRequired(
                        () =>
                        {
                            lblConnectionStatus.Text = "Not connected";
                            lblConnectionStatus.ForeColor = SystemColors.ControlText;
                        }
                    );
                }
            );
        }


        private void LoadSettings()
        {
            this.settings.Load();
            jiraClient.BaseUrl = this.settings.JiraBaseUrl;
        }


        private void SaveSettings()
        {
            this.settings.Save();
        }


        private void EditSettings()
        {
            using (var form = new SettingsForm(this.settings))
            {
                if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    jiraClient.BaseUrl = this.settings.JiraBaseUrl;
                    InitializeIssueControls();
                }
            }
        }


        private void CollectIssueKeysAndStates()
        {
            settings.PersistedIssues.Clear();

            foreach (var issueControl in this.issueControls)
            {
                TimerState timerState = issueControl.WatchTimer.GetState();

                var persistedIssue = new PersistedIssue
                {
                    Key = issueControl.IssueKey,
                    TimerRunning = timerState.Running,
                    StartTime = timerState.StartTime,
                    TotalTime = timerState.TotalTime,
                    Comment = issueControl.Comment
                };

                settings.PersistedIssues.Add(persistedIssue);
            }
        }


        private void JiraLogin()
        {
            using (var form = new LoginForm())
            {
                form.Username = this.settings.Username;
                form.Password = this.settings.Password;
                form.Remember = this.settings.RememberCredentials;

                if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    this.settings.RememberCredentials = form.Remember;
                    this.settings.Username = form.Username;
                    this.settings.Password = form.Password;

                    if (IsJiraEnabled)
                        AuthenticateJira(this.settings.Username, this.settings.Password);
                }
            }
        }


        private void LoadFilterList()
        {
            Task.Factory.StartNew(
                () =>
                {
                    List<Filter> filters = jiraClient.GetFavoriteFilters();
                    if (filters == null)
                        return;

                    this.InvokeIfRequired(
                        () =>
                        {
                            CBFilterItem currentItem = null;

                            cbFilters.Items.Clear();
                            foreach (var filter in filters)
                            {
                                var item = new CBFilterItem(filter.Id, filter.Name, filter.Jql);
                                cbFilters.Items.Add(item);
                                if (item.Id == this.settings.CurrentFilter)
                                   currentItem = item;
                            }

                            if (currentItem != null)
                                cbFilters.SelectedItem = currentItem;
                        }
                    );
                }
            );
        }
        #endregion


        #region private members
        private bool IsJiraEnabled
        {
            get
            {
                return !(
                    string.IsNullOrWhiteSpace(settings.JiraBaseUrl) ||
                    string.IsNullOrWhiteSpace(settings.Username) ||
                    string.IsNullOrWhiteSpace(settings.Password)
                );
            }
        }

        private IEnumerable<IssueControl> issueControls
        {
            get
            {
                return this.Controls.OfType<IssueControl>();
            }
        }

        private Timer ticker;

        private JiraClient jiraClient;

        private Settings settings;

        private IssueControl lastRunningIssue = null;
        #endregion


        #region private consts
        private const int firstDelay = 500;
        private const int defaultDelay = 30000;
        #endregion
    }

    // content item for the combo box
    public class CBFilterItem {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Jql { get; set; }

        public CBFilterItem(int id, string name, string jql) {
            Id = id;
            Name = name;
            Jql = jql;
        }
    }
}
