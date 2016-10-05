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
                return cbJira.Text;
            }

            set
            {
                cbJira.Text = value;
                UpdateSummary();
            }
        }


        public WatchTimer WatchTimer { get; private set; }


        public IEnumerable<Issue> AvailableIssues
        {
            set
            {
                cbJira.Items.Clear();
                foreach (var issue in value)
                    cbJira.Items.Add(new CBIssueItem(issue.Key, issue.Fields.Summary));
            }
        }


        public string Comment { get; set; }
        #endregion


        #region public events
        public event EventHandler TimerStarted;

        public event EventHandler TimerReset;
        #endregion


        #region public methods
        public IssueControl(JiraClient jiraClient, Settings settings)
            : base()
        {
            InitializeComponent();

            Comment = null;

            this.settings = settings;

            this.jiraClient = jiraClient;
            this.WatchTimer = new WatchTimer();
        }


        public void UpdateOutput(bool updateSummary = false)
        {
            tbTime.Text = JiraTimeHelpers.TimeSpanToJiraTime(WatchTimer.TimeElapsed);

            if (WatchTimer.Running)
            {
                btnStartStop.Image = (System.Drawing.Image)(Properties.Resources.pause26);
                tbTime.BackColor = Color.PaleGreen;
            }
            else {
                btnStartStop.Image = (System.Drawing.Image)(Properties.Resources.play26);
                tbTime.BackColor = SystemColors.Control;
            }

            if (string.IsNullOrEmpty(Comment))
                btnPostAndReset.Image = (System.Drawing.Image)Properties.Resources.posttime26;
            else
                btnPostAndReset.Image = (System.Drawing.Image)Properties.Resources.posttimenote26;

            btnOpen.Enabled = cbJira.Text.Trim() != "";
            btnReset.Enabled = WatchTimer.Running || WatchTimer.TimeElapsed.Ticks > 0;
            btnPostAndReset.Enabled = WatchTimer.TimeElapsed.TotalMinutes >= 1;

            if (updateSummary)
                UpdateSummary();
        }


        public void Start()
        {
            WatchTimer.Start();
            UpdateOutput();
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
            if (cbJira.Text == "")
                return;

            OpenIssueInBrowser(cbJira.Text);
        }


        private void UpdateSummary()
        {
            lblSummary.Text = "";

            if (cbJira.Text == "")
                return;
            if (!jiraClient.SessionValid)
                return;

            Task.Factory.StartNew(
                () => {
                    string key = "";
                    string summary = "";
                    this.InvokeIfRequired(
                        () => key = cbJira.Text
                    );
                    summary = jiraClient.GetIssueSummary(key);
                    this.InvokeIfRequired(
                        () => lblSummary.Text = summary
                    );
                }
            );
        }


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbJira = new System.Windows.Forms.ComboBox();
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
            // cbJira
            // 
            this.cbJira.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbJira.DisplayMember = "Key";
            this.cbJira.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbJira.DropDownHeight = 90;
            this.cbJira.DropDownWidth = 500;
            this.cbJira.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.cbJira.IntegralHeight = false;
            this.cbJira.Location = new System.Drawing.Point(0, 2);
            this.cbJira.Name = "cbJira";
            this.cbJira.Size = new System.Drawing.Size(155, 28);
            this.cbJira.TabIndex = 0;
            this.cbJira.ValueMember = "Key";
            this.cbJira.DropDown += new System.EventHandler(this.cbJira_DropDown);
            this.cbJira.SelectedIndexChanged += new System.EventHandler(this.cbJira_SelectedIndexChanged);
            this.cbJira.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbJira_KeyDown);
            this.cbJira.Leave += new System.EventHandler(this.cbJira_Leave);
            this.cbJira.MeasureItem += this.cbJira_MeasureItem;
            this.cbJira.DrawItem += this.cbJira_DrawItem;
            // 
            // tbTime
            // 
            this.tbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.3F);
            this.tbTime.Location = new System.Drawing.Point(248, 2);
            this.tbTime.Name = "tbTime";
            this.tbTime.ReadOnly = true;
            this.tbTime.Size = new System.Drawing.Size(107, 28);
            this.tbTime.TabIndex = 3;
            this.tbTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTime_KeyDown);
            this.tbTime.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbTime_MouseDoubleClick);
            // 
            // lblSummary
            // 
            this.lblSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.Location = new System.Drawing.Point(0, 33);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(457, 17);
            this.lblSummary.TabIndex = 6;
            // 
            // btnPostAndReset
            // 
            this.btnPostAndReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPostAndReset.Image = global::StopWatch.Properties.Resources.posttime26;
            this.btnPostAndReset.Location = new System.Drawing.Point(361, 0);
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
            this.btnReset.Location = new System.Drawing.Point(414, 0);
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
            this.Controls.Add(this.cbJira);
            this.Name = "IssueControl";
            this.Size = new System.Drawing.Size(458, 58);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private void Reset()
        {
            Comment = null;
            this.WatchTimer.Reset();
            UpdateOutput();

            if (this.TimerReset != null)
                this.TimerReset(this, new EventArgs());
        }


        public void OpenIssueInBrowser(string key)
        {
            if (string.IsNullOrEmpty(this.settings.JiraBaseUrl))
                return;

            string url = this.settings.JiraBaseUrl;
            if (!url.EndsWith("/"))
                url += "/";
            url += "browse/";
            url += key.Trim();
            System.Diagnostics.Process.Start(url);
        }
        #endregion


        #region private eventhandlers
        void cbJira_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            Font font = new Font(cbJira.Font.FontFamily, cbJira.Font.Size * 0.8f, cbJira.Font.Style);
            Size size = TextRenderer.MeasureText(e.Graphics, "FOO", font);
            e.ItemHeight = size.Height;
        }


        void cbJira_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the default background
            e.DrawBackground();

            CBIssueItem item = (CBIssueItem)cbJira.Items[e.Index];

            // Create rectangles for the columns to display
            Rectangle r1 = e.Bounds;
            Rectangle r2 = e.Bounds;

            r1.Width = 100;

            r2.X = r1.Width;
            r2.Width = 400;

            Font font = new Font(e.Font.FontFamily, e.Font.Size * 0.8f, e.Font.Style);

            // Draw the text on the first column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
                e.Graphics.DrawString(item.Key, font, sb, r1);

            // Draw a line to isolate the columns 
            using (Pen p = new Pen(Color.Black))
                e.Graphics.DrawLine(p, r1.Right, 0, r1.Right, r1.Bottom);

            // Draw the text on the second column
            using (SolidBrush sb = new SolidBrush(e.ForeColor))
                e.Graphics.DrawString(item.Summary, font, sb, r2);

            // Draw a line to isolate the columns 
            using (Pen p = new Pen(Color.Black))
                e.Graphics.DrawLine(p, r1.Right, 0, r1.Right, 140);

        }


        void cbJira_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOutput(true);
        }


        private void cbJira_DropDown(object sender, EventArgs e)
        {
            LoadIssues();
        }


        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenJira(cbJira.Text);
        }


        private void cbJira_Leave(object sender, EventArgs e)
        {
            UpdateOutput(true);
        }


        private void cbJira_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                UpdateOutput(true);
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
            using (var worklogForm = new WorklogForm(Comment))
            {
                var formResult = worklogForm.ShowDialog(this);
                if (formResult == DialogResult.OK)
                {
                    Comment = worklogForm.Comment.Trim();

                    PostAndReset(cbJira.Text, WatchTimer.TimeElapsed, Comment);
                }
                else if (formResult == DialogResult.Yes)
                {
                    Comment = string.Format("{0}:{1}{2}", DateTime.Now.ToString("g"), Environment.NewLine, worklogForm.Comment.Trim());
                    UpdateOutput();
                }
            }
        }


        private void tbTime_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditTime();

        }


        private void tbTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                EditTime();
        }
        #endregion


        #region private methods
        private void PostAndReset(string key, TimeSpan timeElapsed, string comment)
        {
            Task.Factory.StartNew(
                () =>
                {
                    this.InvokeIfRequired(
                        () => {
                            btnPostAndReset.Enabled = false;
                            Cursor.Current = Cursors.WaitCursor;
                        }
                    );

                    bool postSuccesful = true;

                    // First post comment in Comment-track - and clear the comment string, if it should only be posted here
                    // Only actually post in Comment-track if text is not empty
                    if (settings.PostWorklogComment != WorklogCommentSetting.WorklogOnly && !string.IsNullOrEmpty(comment))
                    {
                        postSuccesful = jiraClient.PostComment(key, comment);
                        if (postSuccesful && settings.PostWorklogComment == WorklogCommentSetting.CommentOnly)
                            comment = "";
                    }

                    // Now post the WorkLog with timeElapsed - and comment unless it was reset
                    if (postSuccesful)
                        postSuccesful = jiraClient.PostWorklog(key, timeElapsed, comment);

                    if (postSuccesful)
                    {
                        this.InvokeIfRequired(
                            () => Reset()
                        );
                    }

                    this.InvokeIfRequired(
                        () => {
                            btnPostAndReset.Enabled = true;
                            Cursor.Current = DefaultCursor;
                        }
                    );
                }
            );
        }


        private void LoadIssues()
        {
            // TODO: This + the datasource for cbFilters should be moved into a controller layer
            var ctrlList = (Application.OpenForms[0] as MainForm).Controls.Find("cbFilters", true);
            if (ctrlList.Length == 0)
                return;

            var cbFilters = ctrlList[0] as ComboBox;
            if (cbFilters.SelectedIndex < 0)
                return;

            string jql = (cbFilters.SelectedItem as CBFilterItem).Jql;

            Task.Factory.StartNew(
                () =>
                {
                    List<Issue> availableIssues = jiraClient.GetIssuesByJQL(jql).Issues;

                    if (availableIssues == null)
                        return;

                    this.InvokeIfRequired(
                        () =>
                        {
                            AvailableIssues = availableIssues;
                            cbJira.DropDownHeight = 120;
                            cbJira.Invalidate();
                        }
                    );
                }
            );
        }


        private void EditTime()
        {
            using (var editTimeForm = new EditTimeForm(WatchTimer.TimeElapsed))
            {
                if (editTimeForm.ShowDialog(this) == DialogResult.OK)
                {
                    WatchTimer.TimeElapsed = editTimeForm.Time;

                    UpdateOutput();
                }
            }
        }
        #endregion


        #region private members
        private ComboBox cbJira;
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

        private Settings settings;
        #endregion


        #region private classes
        // content item for the combo box
        private class CBIssueItem {
            public string Key { get; set; }
            public string Summary { get; set; }

            public CBIssueItem(string key, string summary) {
                Key = key;
                Summary = summary;
            }
        }
        #endregion
    }
}
