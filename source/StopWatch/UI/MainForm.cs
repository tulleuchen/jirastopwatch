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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            settings = Settings.Instance;
            settings.Load();

            Logger.Instance.LogfilePath = Path.Combine(Application.UserAppDataPath, "jirastopwatch.log");
            Logger.Instance.Enabled = settings.LoggingEnabled;

            restRequestFactory = new RestRequestFactory();
            jiraApiRequestFactory = new JiraApiRequestFactory(restRequestFactory);

            restClientFactory = new RestClientFactory();
            restClientFactory.BaseUrl = this.settings.JiraBaseUrl;

            jiraApiRequester = new JiraApiRequester(restClientFactory, jiraApiRequestFactory);

            jiraClient = new JiraClient(jiraApiRequestFactory, jiraApiRequester);

            InitializeComponent();

            Text = string.Format("{0} v. {1}", Application.ProductName, Application.ProductVersion);

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
            IssueControl senderCtrl = (IssueControl)sender;
            ChangeIssueState(senderCtrl.IssueKey);

            if (settings.AllowMultipleTimers)
                return;

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

            SaveSettingsAndIssueStates();

            if (firstTick)
                CheckForUpdates();
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
            SaveSettingsAndIssueStates();
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (this.settings.FirstRun)
            {
                this.settings.FirstRun = false;
                EditSettings();
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
                        issueControl.EstimateUpdateMethod = persistedIssue.EstimateUpdateMethod;
                        issueControl.EstimateUpdateValue = persistedIssue.EstimateUpdateValue;
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

        private void lblConnectionStatus_Click(object sender, EventArgs e)
        {
            if (jiraClient.SessionValid)
                return;

            string msg = string.Format("Jira StopWatch could not connect to your Jira server. Error returned:{0}{0}{1}", Environment.NewLine, jiraClient.ErrorMessage);
            MessageBox.Show(msg, "Connection error");
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

        private void pbAddIssue_Clicked(object sender, EventArgs e)
        {
            if (this.settings.IssueCount < maxIssues || this.issueControls.Count() < maxIssues)
            {
                this.settings.IssueCount++;
                this.InitializeIssueControls();
                IssueControl AddedIssue = this.issueControls.Last();
                AddedIssue.focusJiraField();
                this.pMain.ScrollControlIntoView(AddedIssue);
            }
        }

        private void pbAddIssue_MouseEnter(object sender, EventArgs e)
        {
            pbAddIssue.BackColor = SystemColors.GradientInactiveCaption;

        }

        private void pbAddIssue_MouseLeave(object sender, EventArgs e)
        {
            pbAddIssue.BackColor = SystemColors.GradientActiveCaption;
        }

        private void InitializeIssueControls()
        {
            this.SuspendLayout();

            if (this.settings.IssueCount >= maxIssues)
            {
                // Max reached.  Reset number in case it is larger 
                this.settings.IssueCount = maxIssues;

                // Update tooltip to reflect the fact that you can't add anymore
                // We don't disable the button since then the tooltip doesn't show but
                // the click won't do anything if we have too many issues
                this.ttMain.SetToolTip(this.pbAddIssue, string.Format("You have reached the max limit of {0} issues and cannot add another", maxIssues.ToString()));
                this.pbAddIssue.Cursor = System.Windows.Forms.Cursors.No;
            }
            else
            {
                if (this.settings.IssueCount < 1)
                    this.settings.IssueCount = 1;

                // Reset status 
                this.ttMain.SetToolTip(this.pbAddIssue, "Add another issue row");
                this.pbAddIssue.Cursor = System.Windows.Forms.Cursors.Hand;
            }
            
            // Remove IssueControl where user has clicked the remove button
            foreach (IssueControl issue in this.issueControls)
            {
                if (issue.MarkedForRemoval)
                    this.pMain.Controls.Remove(issue);
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

            // To make sure that pMain's scrollbar doesn't screw up, all IssueControls need to have
            // their position reset, before positioning them again
            foreach (IssueControl issue in this.issueControls)
            {
                issue.Left = 0;
                issue.Top = 0;
            }

            // Now position all issueControl controls
            int i = 0;
            bool EnableRemoveIssue = this.issueControls.Count() > 1;
            foreach (IssueControl issue in this.issueControls)
            {
                issue.ToggleRemoveIssueButton(EnableRemoveIssue);
                issue.Top = i * issue.Height;
                i++;
            }

            this.ClientSize = new Size(pBottom.Width, this.settings.IssueCount * issueControls.Last().Height + pMain.Top + pBottom.Height);

            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;

            if (this.Bottom > Screen.PrimaryScreen.WorkingArea.Height)
                this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            
            pMain.Height = ClientSize.Height - pTop.Height - pBottom.Height;
            pBottom.Top = ClientSize.Height - pBottom.Height;

            this.TopMost = this.settings.AlwaysOnTop;

            this.ResumeLayout(false);
            this.PerformLayout();
            UpdateIssuesOutput(true);
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
                        SetConnectionStatus(false);
                        return;
                    }

                    if (jiraClient.SessionValid || jiraClient.ValidateSession())
                    {
                        SetConnectionStatus(true);

                        this.InvokeIfRequired(
                            () =>
                            {
                                if (firstTick)
                                    LoadFilters();

                                UpdateIssuesOutput(firstTick);
                            }
                        );
                        return;
                    }

                    SetConnectionStatus(false);
                }
            );
        }


        private void SetConnectionStatus(bool connected)
        {
            this.InvokeIfRequired(
                () =>
                {
                    if (connected)
                    {
                        lblConnectionStatus.Text = "Connected";
                        lblConnectionStatus.ForeColor = Color.DarkGreen;
                        lblConnectionStatus.Font = new Font(lblConnectionStatus.Font, FontStyle.Regular);
                        lblConnectionStatus.Cursor = Cursors.Default;
                    }
                    else
                    {
                        lblConnectionStatus.Text = "Not connected";
                        lblConnectionStatus.ForeColor = Color.Tomato;
                        lblConnectionStatus.Font = new Font(lblConnectionStatus.Font, FontStyle.Regular | FontStyle.Underline);
                        lblConnectionStatus.Cursor = Cursors.Hand;
                    }
                }
            );
        }


        private void ChangeIssueState(string issueKey)
        {
            if (string.IsNullOrWhiteSpace(settings.StartTransitions))
                return;

            Task.Factory.StartNew(
                () =>
                {
                    var startTransitions = this.settings.StartTransitions
                        .Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
                        .Select(l => l.Trim().ToLower()).ToArray();

                    var availableTransitions = jiraClient.GetAvailableTransitions(issueKey);
                    if (availableTransitions == null || availableTransitions.Transitions.Count() == 0)
                        return;

                    foreach (var t in availableTransitions.Transitions)
                    {
                        if (startTransitions.Any(t.Name.ToLower().Contains))
                        {
                            jiraClient.DoTransition(issueKey, t.Id);
                            return;
                        }
                    }
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
                    Logging.Logger.Instance.Enabled = settings.LoggingEnabled;
                    if (IsJiraEnabled)
                        AuthenticateJira(this.settings.Username, this.settings.Password);
                    InitializeIssueControls();
                }
            }
        }


        private void SaveSettingsAndIssueStates()
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
                    Comment = issueControl.Comment,
                    EstimateUpdateMethod = issueControl.EstimateUpdateMethod,
                    EstimateUpdateValue = issueControl.EstimateUpdateValue
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


        private void CheckForUpdates()
        {
            if (!settings.CheckForUpdate)
                return;

            Task.Factory.StartNew(
                () =>
                {
                    GithubRelease latestRelease = ReleaseHelper.GetLatestVersion();
                    if (latestRelease == null)
                        return;

                    string currentVersion = Application.ProductVersion;
                    if (string.Compare(latestRelease.TagName, currentVersion) <= 0)
                        return;

                    this.InvokeIfRequired(
                        () =>
                        {
                            string msg = string.Format("There is a newer version available of Jira StopWatch.{0}{0}Latest release is {1}. You are running version {2}.{0}{0}Do you want to download latest release?",
                                Environment.NewLine,
                                latestRelease.TagName,
                                currentVersion);
                            if (MessageBox.Show(msg, "New version available", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                System.Diagnostics.Process.Start("https://github.com/carstengehling/jirastopwatch/releases/latest");
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
        private const int maxIssues = 20;
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
