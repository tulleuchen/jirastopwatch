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

            this.jiraClient = new JiraClient();

            //this.issueControls = new List<IssueControl>();

            LoadSettings();

            ticker = new Timer();
            ticker.Interval = 5000;
            ticker.Tick += ticker_Tick;
            ticker.Start();
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
            UpdateConnectionStatus();
            UpdateIssuesOutput();
        }


        private void pbSettings_Click(object sender, EventArgs e)
        {
            EditSettings();
        }


        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Copy issueControl keys and timer states to settings and save settings
            CollectIssueKeysAndStates();

            SaveSettings();
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (this.firstRun)
            {
                firstRun = false;

                EditSettings();
                JiraLogin();
            }

            if (this.username != "" && this.password != "")
                this.jiraClient.Authenticate(this.username, this.password);

            InitializeIssueControls();

            // Add issuekeys from settings to issueControl controls
            int i = 0;
            foreach (var issueControl in this.issueControls)
            {
                if (i < this.issues.Count)
                {
                    string[] issueState = this.issues[i].Split(';');
                    issueControl.IssueKey = issueState[0];

                    if (saveTimerState != SaveTimerSetting.NoSave)
                    {
                        TimerState timerState = new TimerState
                        {
                            Running = saveTimerState == SaveTimerSetting.SavePause ? false : bool.Parse(issueState[1]),
                            StartTime = DateTime.Parse(issueState[2]),
                            TotalTime = TimeSpan.Parse(issueState[3])
                        };
                        issueControl.WatchTimer.SetState(timerState);
                    }
                }
                i++;
            }

            UpdateConnectionStatus();
            UpdateIssuesOutput();
        }


        private void pbLogin_Click(object sender, EventArgs e)
        {
            JiraLogin();
        }
        #endregion


        #region private methods
        private void InitializeIssueControls()
        {
            this.SuspendLayout();

            // If we have too many issueControl controls, compared to this.issueCount
            // remove the ones not needed
            while (this.issueControls.Count() > this.issueCount)
            {
                var issue = this.issueControls.Last();
                this.Controls.Remove(issue);
            }

            // Create issueControl controls needed
            while (this.issueControls.Count() < this.issueCount)
            {
                var issue = new IssueControl(this.jiraClient);
                issue.TimerStarted += issue_TimerStarted;
                this.Controls.Add(issue);
            }

            // Position all issueControl controls
            int i = 0;
            foreach (var issue in this.issueControls)
            {
                issue.Left = 12;
                issue.Top = i * issue.Height + 12;
                i++;
            }

            // Resize form and reposition settings button
            this.ClientSize = new Size(issueControls.Last().Width + 24, this.issueCount * issueControls.Last().Height + 46);

            pbSettings.Left = this.ClientSize.Width - 30;
            pbSettings.Top = this.ClientSize.Height - 30;

            pbLogin.Left = 8;
            pbLogin.Top = this.ClientSize.Height - 30;

            lblConnectionHeader.Top = this.ClientSize.Height - 20;
            lblConnectionStatus.Top = this.ClientSize.Height - 20;

            this.TopMost = this.alwaysOnTop;

            this.ResumeLayout(false);
            this.PerformLayout();
        }


        private void UpdateIssuesOutput()
        {
            foreach (var issue in this.issueControls)
                issue.UpdateOutput();
        }


        private void UpdateConnectionStatus()
        {
            Task.Factory.StartNew(
                () =>
                {
                    if (jiraClient.SessionValid())
                    {
                        this.Invoke(new Action(
                            () =>
                            {
                                lblConnectionStatus.Text = "Connected";
                                lblConnectionStatus.ForeColor = Color.DarkGreen;
                            }
                        ));
                    }
                    else
                    {
                        this.Invoke(new Action(
                            () =>
                            {
                                lblConnectionStatus.Text = "Not connected";
                                lblConnectionStatus.ForeColor = SystemColors.ControlText;
                            }
                        ));
                    }
                }
            );
        }


        private void LoadSettings()
        {
            jiraClient.BaseUrl = Properties.Settings.Default.JiraBaseUrl ?? "";

            this.alwaysOnTop = Properties.Settings.Default.AlwaysOnTop;
            this.issueCount = Properties.Settings.Default.IssueCount;
            this.issues = Properties.Settings.Default.Issues ?? new System.Collections.Specialized.StringCollection();
            this.username = Properties.Settings.Default.Username;
            if (Properties.Settings.Default.Password != "")
                this.password = DPAPI.Decrypt(Properties.Settings.Default.Password);
            else
                this.password = "";
            this.rememberCredentials = Properties.Settings.Default.RememberCredentials;
            this.firstRun = Properties.Settings.Default.FirstRun;
            this.saveTimerState = (SaveTimerSetting)Properties.Settings.Default.SaveTimerState;
        }


        private void SaveSettings()
        {
            Properties.Settings.Default.JiraBaseUrl = jiraClient.BaseUrl;

            Properties.Settings.Default.AlwaysOnTop = this.alwaysOnTop;
            Properties.Settings.Default.IssueCount = this.issueCount;
            Properties.Settings.Default.Issues = this.issues;
            Properties.Settings.Default.Username = this.username;
            if (this.password != "")
                Properties.Settings.Default.Password = DPAPI.Encrypt(this.password);
            else
                Properties.Settings.Default.Password = "";
            Properties.Settings.Default.RememberCredentials = this.rememberCredentials;
            Properties.Settings.Default.FirstRun = this.firstRun;
            Properties.Settings.Default.SaveTimerState = (int)this.saveTimerState;

            Properties.Settings.Default.Save();
        }


        private void EditSettings()
        {
            using (var form = new SettingsForm())
            {
                form.JiraBaseUrl = jiraClient.BaseUrl;
                form.IssueCount = this.issueCount;
                form.AlwaysOnTop = this.alwaysOnTop;
                form.SaveTimerState = this.saveTimerState;

                if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    jiraClient.BaseUrl = form.JiraBaseUrl;
                    this.issueCount = form.IssueCount;
                    this.alwaysOnTop = form.AlwaysOnTop;
                    this.saveTimerState = form.SaveTimerState;

                    InitializeIssueControls();
                }
            }
        }


        private void CollectIssueKeysAndStates()
        {
            this.issues.Clear();

            foreach (var issueControl in this.issueControls)
            {
                TimerState timerState = issueControl.WatchTimer.GetState();

                // Simple serialize all issueControl runtime data into string
                string issueState = String.Format("{0};{1};{2};{3}",
                    issueControl.IssueKey,
                    timerState.Running,
                    timerState.StartTime,
                    timerState.TotalTime
                );

                this.issues.Add(issueState);
            }
        }


        private void JiraLogin()
        {
            using (var form = new LoginForm())
            {
                form.Username = this.username;
                form.Password = this.password;
                form.Remember = this.rememberCredentials;

                if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    this.jiraClient.Authenticate(form.Username, form.Password);

                    this.rememberCredentials = form.Remember;
                    if (this.rememberCredentials)
                    {
                        this.username = form.Username;
                        this.password = form.Password;
                    }
                    else
                    {
                        this.username = "";
                        this.password = "";
                    }
                }
            }

        }
        #endregion


        #region private members
        private IEnumerable<IssueControl> issueControls
        {
            get
            {
                return this.Controls.OfType<IssueControl>();
            }
        }

        private Timer ticker;

        private JiraClient jiraClient;

        private bool alwaysOnTop;
        private int issueCount;

        private SaveTimerSetting saveTimerState;

        private string username;
        private string password;
        private bool rememberCredentials;
        private bool firstRun;

        private System.Collections.Specialized.StringCollection issues;
        #endregion
    }
}
