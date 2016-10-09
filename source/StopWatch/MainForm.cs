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
            settings = new Settings();
            settings.Load();

            restRequestFactory = new RestRequestFactory();
            jiraApiRequestFactory = new JiraApiRequestFactory(restRequestFactory);

            restClientFactory = new RestClientFactory();
            restClientFactory.BaseUrl = this.settings.JiraBaseUrl;

            jiraApiRequester = new JiraApiRequester(restClientFactory, jiraApiRequestFactory);

            jiraClient = new JiraClient(jiraApiRequestFactory, jiraApiRequester);

            InitializeComponent();

            cbFilters.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFilters.DisplayMember = "Name";

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
            if (settings.AllowMultipleTimers)
                return;

            IssueControl senderCtrl = (IssueControl)sender;

            foreach (var issue in this.issueControls)
                if (issue != senderCtrl)
                    issue.Pause();
        }


        void Issue_TimerReset(object sender, EventArgs e)
        {
            UpdateTotalTime();
        }


        void ticker_Tick(object sender, EventArgs e)
        {
            bool firstTick = ticker.Interval == firstDelay;

            ticker.Interval = defaultDelay;

            UpdateJiraRelatedData(firstTick);
            UpdateIssuesOutput(firstTick);

            SaceSettingsAndIssueStates();
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
            SaceSettingsAndIssueStates();
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


        private void cbFilters_DropDown(object sender, EventArgs e)
        {
            LoadFilters();
        }


        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (CBFilterItem)cbFilters.SelectedItem;
            this.settings.CurrentFilter = item.Id;
        }


        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Mono for MacOSX and Linux do not implement the notifyIcon
            // so ignore this feature if we are not running on Windows
            if (!CrossPlatformHelpers.IsWindowsEnvironment())
                return;

            if (!this.settings.MinimizeToTray)
                return;

            if (WindowState == FormWindowState.Minimized)
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
                        this.InvokeIfRequired(
                            () => UpdateIssuesOutput(true)
                        );

                    this.InvokeIfRequired(
                        () => UpdateJiraRelatedData(true)
                    );
                }
            );
        }

        private void issue_RemoveMeTriggered(object sender, EventArgs e)
        {
            if (this.settings.IssueCount > 1)
            {
                this.settings.IssueCount--;
            }
            this.InitializeIssueControls();
        }

        private void btnAddIssue_Clicked(object sender, EventArgs e)
        {
            this.settings.IssueCount++;
            this.InitializeIssueControls();
        }


        private void InitializeIssueControls()
        {
            this.SuspendLayout();
            this.btnAddIssue.Visible = this.settings.AllowFlexibleIssueCount;
            this.btnAddIssue.Enabled = this.settings.AllowFlexibleIssueCount;

            foreach (IssueControl issue in this.issueControls)
            {
                if (issue.MarkedForRemoval)
                {
                    this.pMain.Controls.Remove(issue);
                }
            }


            // If we have too many issueControl controls, compared to this.IssueCount
            // remove the ones not needed
            while (this.issueControls.Count() > this.settings.IssueCount)
            {
                var issue = this.issueControls.Last();
                this.pMain.Controls.Remove(issue);
            }

            // Create issueControl controls needed
            while (this.issueControls.Count() < this.settings.IssueCount)
            {
                var issue = new IssueControl(this.jiraClient, this.settings);
                issue.RemoveMeTriggered += new EventHandler(this.issue_RemoveMeTriggered);
                issue.TimerStarted += issue_TimerStarted;
                issue.TimerReset += Issue_TimerReset;
                this.pMain.Controls.Add(issue);
            }

            // Position all issueControl controls and set TimerEditable
            int i = 0;
            bool EnableRemoveIssue = settings.AllowFlexibleIssueCount && this.issueControls.Count() > 1;
            foreach (var issue in this.issueControls)
            {
                issue.ToggleRemoveIssueButton(settings.AllowFlexibleIssueCount, EnableRemoveIssue);
                issue.Left = 12;
                issue.Top = i * issue.Height + 12;
                i++;
            }

            pMain.Width = issueControls.Last().Width + 44;

            this.ClientSize = new Size(pBottom.Width, this.settings.IssueCount * issueControls.Last().Height + 50);

            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            if (this.Bottom > Screen.PrimaryScreen.WorkingArea.Height)
                this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;


            pMain.Height = ClientSize.Height - 34;
            pBottom.Top = ClientSize.Height - 34;

            this.TopMost = this.settings.AlwaysOnTop;

            this.ResumeLayout(false);
            this.PerformLayout();
        }


        private void UpdateIssuesOutput(bool updateSummary = false)
        {
            foreach (var issue in this.issueControls)
                issue.UpdateOutput(updateSummary);
            UpdateTotalTime();
        }


        private void UpdateTotalTime()
        {
            TimeSpan totalTime = new TimeSpan();
            foreach (var issue in this.issueControls)
                totalTime += issue.WatchTimer.TimeElapsed;
            tbTotalTime.Text = JiraTimeHelpers.TimeSpanToJiraTime(totalTime);
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
                                lblConnectionStatus.Text = "Not connected";
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

                                if (firstTick)
                                    LoadFilters();

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


        private void EditSettings()
        {
            using (var form = new SettingsForm(this.settings))
            {
                if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    restClientFactory.BaseUrl = this.settings.JiraBaseUrl;
                    InitializeIssueControls();
                }
            }
        }


        private void SaceSettingsAndIssueStates()
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

            this.settings.Save();
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


        private void LoadFilters()
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


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
                ShowOnTop();

            base.WndProc(ref m);
        }


        private void ShowOnTop()
        {
            if (WindowState == FormWindowState.Minimized) {
                Show();
                WindowState = FormWindowState.Normal;
                notifyIcon.Visible = false;
            }

            // get our current "TopMost" value (ours will always be false though)
            // make our form jump to the top of everything
            // set it back to whatever it was
            bool top = TopMost;
            TopMost = true;
            TopMost = top;
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
                return this.pMain.Controls.OfType<IssueControl>();
            }
        }

        private Timer ticker;

        private JiraApiRequestFactory jiraApiRequestFactory;
        private RestRequestFactory restRequestFactory;
        private JiraApiRequester jiraApiRequester;
        private RestClientFactory restClientFactory;
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
