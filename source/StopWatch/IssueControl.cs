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
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StopWatch
{
    internal class IssueControl : UserControl
    {
        #region public members
        public string IssueKey
        {
            get
            {
                return tbJira.Text;
            }

            set
            {
                tbJira.Text = value;
                UpdateSummary();
            }
        }


        public WatchTimer WatchTimer { get; private set; }
        #endregion


        #region public events
        public event EventHandler TimerStarted;
        #endregion


        #region public methods
        public IssueControl(JiraClient jiraClient)
            : base()
        {
            InitializeComponent();

            this.jiraClient = jiraClient;
            this.WatchTimer = new WatchTimer();
        }


        public void UpdateOutput()
        {
            tbTime.Text = JiraHelpers.TimeSpanToJiraTime(WatchTimer.TimeElapsed);

            if (WatchTimer.Running)
            {
                btnStartStop.Image = (System.Drawing.Image)(Properties.Resources.pause26);
                tbTime.BackColor = Color.PaleGreen;
            }
            else {
                btnStartStop.Image = (System.Drawing.Image)(Properties.Resources.play26);
                tbTime.BackColor = SystemColors.Control;
            }

            btnOpen.Enabled = tbJira.Text.Trim() != "";
            btnReset.Enabled = WatchTimer.Running || WatchTimer.TimeElapsed.Ticks > 0;
            btnPostAndReset.Enabled = WatchTimer.TimeElapsed.TotalMinutes >= 1;
        }


        public void Pause()
        {
            WatchTimer.Pause();
            UpdateOutput();
        }
        #endregion


        #region private methods
        private void OpenJira(string issue)
        {
            if (jiraClient == null)
                return;
            if (tbJira.Text == "")
                return;

            jiraClient.OpenIssueInBrowser(tbJira.Text);
        }


        private void UpdateSummary()
        {
            lblSummary.Text = "";

            if (jiraClient == null)
                return;
            if (tbJira.Text == "")
                return;

            Task.Factory.StartNew(
                () => {
                    string key = "";
                    string summary = "";
                    this.Invoke(new Action(
                        () => key = tbJira.Text
                    ));
                    summary = jiraClient.GetIssueSummary(tbJira.Text);
                    this.Invoke(new Action(
                        () => lblSummary.Text = summary
                    ));
                }
            );
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbJira = new System.Windows.Forms.TextBox();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.lblSummary = new System.Windows.Forms.Label();
            this.ttIssue = new System.Windows.Forms.ToolTip(this.components);
            this.btnPostAndReset = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lblSplitter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbJira
            // 
            this.tbJira.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbJira.Location = new System.Drawing.Point(0, 2);
            this.tbJira.Name = "tbJira";
            this.tbJira.Size = new System.Drawing.Size(155, 28);
            this.tbJira.TabIndex = 0;
            this.tbJira.Leave += new System.EventHandler(this.tbJira_Leave);
            // 
            // tbTime
            // 
            this.tbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTime.Location = new System.Drawing.Point(248, 2);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(95, 28);
            this.tbTime.TabIndex = 3;
            // 
            // lblSummary
            // 
            this.lblSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.Location = new System.Drawing.Point(0, 33);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(503, 17);
            this.lblSummary.TabIndex = 6;
            // 
            // btnPostAndReset
            // 
            this.btnPostAndReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPostAndReset.Image = global::StopWatch.Properties.Resources.posttime26;
            this.btnPostAndReset.Location = new System.Drawing.Point(350, 0);
            this.btnPostAndReset.Name = "btnPostAndReset";
            this.btnPostAndReset.Size = new System.Drawing.Size(32, 32);
            this.btnPostAndReset.TabIndex = 4;
            this.ttIssue.SetToolTip(this.btnPostAndReset, "Submit worklog to Jira and reset timer");
            this.btnPostAndReset.UseVisualStyleBackColor = true;
            this.btnPostAndReset.Click += new System.EventHandler(this.btnPostAndReset_Click);
            // 
            // btnReset
            // 
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.Image = global::StopWatch.Properties.Resources.reset24;
            this.btnReset.Location = new System.Drawing.Point(444, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(32, 32);
            this.btnReset.TabIndex = 5;
            this.ttIssue.SetToolTip(this.btnReset, "Reset timer");
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartStop.Image = global::StopWatch.Properties.Resources.play26;
            this.btnStartStop.Location = new System.Drawing.Point(212, 0);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(32, 32);
            this.btnStartStop.TabIndex = 2;
            this.ttIssue.SetToolTip(this.btnStartStop, "Start/stop timer");
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpen.Image = global::StopWatch.Properties.Resources.openbrowser26;
            this.btnOpen.Location = new System.Drawing.Point(156, 0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(32, 32);
            this.btnOpen.TabIndex = 1;
            this.ttIssue.SetToolTip(this.btnOpen, "Open issueControl in browser");
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // lblSplitter
            // 
            this.lblSplitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSplitter.Location = new System.Drawing.Point(0, 53);
            this.lblSplitter.Name = "lblSplitter";
            this.lblSplitter.Size = new System.Drawing.Size(500, 2);
            this.lblSplitter.TabIndex = 6;
            // 
            // IssueControl
            // 
            this.Controls.Add(this.btnPostAndReset);
            this.Controls.Add(this.lblSplitter);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.tbTime);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.tbJira);
            this.Name = "IssueControl";
            this.Size = new System.Drawing.Size(500, 58);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private void Reset()
        {
            this.WatchTimer.Reset();
            UpdateOutput();
        }
        #endregion


        #region private eventhandlers
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenJira(tbJira.Text);
        }


        private void tbJira_Leave(object sender, EventArgs e)
        {
            UpdateSummary();
        }


        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (WatchTimer.Running) {
                this.WatchTimer.Pause();
            }
            else {
                this.WatchTimer.Start();

                if (this.TimerStarted != null)
                    this.TimerStarted(this, e);
            }
            UpdateOutput();
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }


        private void btnPostAndReset_Click(object sender, EventArgs e)
        {
            if (jiraClient == null)
                return;

            using (var worklogForm = new WorklogForm())
            {
                if (worklogForm.ShowDialog(this) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    if (jiraClient.PostWorklog(tbJira.Text, WatchTimer.TimeElapsed, worklogForm.Comment))
                        Reset();
                    Cursor.Current = DefaultCursor;
                }
            }

        }
        #endregion


        #region private members
        private TextBox tbJira;
        private Button btnOpen;
        private TextBox tbTime;
        private Button btnStartStop;
        private Button btnReset;
        private Label lblSummary;

        private ToolTip ttIssue;
        private System.ComponentModel.IContainer components;
        private Label lblSplitter;
        private Button btnPostAndReset;

        private JiraClient jiraClient;
        #endregion
    }
}
