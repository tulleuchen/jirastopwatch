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


        public bool TimerEditable
        {
            set
            {
                tbTime.ReadOnly = !value;
            }
        }

        public string Comment { get; set; }
        #endregion


        #region public events
        public event EventHandler TimerStarted;
        #endregion


        #region public methods
        public IssueControl(JiraClient jiraClient)
            : base()
        {
            InitializeComponent();

            cbJira.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbJira.DropDownStyle = ComboBoxStyle.DropDown;
            cbJira.DrawMode = DrawMode.OwnerDrawVariable;
            cbJira.DrawItem += cbJira_DrawItem;
            cbJira.MeasureItem += cbJira_MeasureItem;
            cbJira.SelectedIndexChanged += cbJira_SelectedIndexChanged;
            cbJira.DisplayMember = "Key";
            cbJira.ValueMember = "Key";

            ignoreTextChange = false;

            Comment = null;

            this.jiraClient = jiraClient;
            this.WatchTimer = new WatchTimer();
        }


        public void UpdateOutput(bool updateSummary = false)
        {
            ignoreTextChange = true;
            tbTime.Text = JiraHelpers.TimeSpanToJiraTime(WatchTimer.TimeElapsed);
            ignoreTextChange = false;

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

            jiraClient.OpenIssueInBrowser(cbJira.Text);
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
            this.cbJira.DropDownWidth = 500;
            this.cbJira.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.cbJira.Location = new System.Drawing.Point(0, 2);
            this.cbJira.Name = "cbJira";
            this.cbJira.Size = new System.Drawing.Size(155, 28);
            this.cbJira.TabIndex = 0;
            this.cbJira.Leave += new System.EventHandler(this.cbJira_Leave);
            // 
            // tbTime
            // 
            this.tbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.3F);
            this.tbTime.Location = new System.Drawing.Point(248, 2);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(95, 28);
            this.tbTime.TabIndex = 3;
            this.tbTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTime.TextChanged += new System.EventHandler(this.tbTime_TextChanged);
            // 
            // lblSummary
            // 
            this.lblSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.Controls.Add(this.cbJira);
            this.Name = "IssueControl";
            this.Size = new System.Drawing.Size(500, 58);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private void Reset()
        {
            Comment = null;
            this.WatchTimer.Reset();
            UpdateOutput();
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
        }


        void cbJira_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSummary();
        }


        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenJira(cbJira.Text);
        }


        private void cbJira_Leave(object sender, EventArgs e)
        {
            UpdateOutput();
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
                    Cursor.Current = Cursors.WaitCursor;
                    if (jiraClient.PostWorklog(cbJira.Text, WatchTimer.TimeElapsed, Comment))
                        Reset();
                    Cursor.Current = DefaultCursor;
                }
                else if (formResult == DialogResult.Yes)
                {
                    Comment = string.Format("{0}:{1}{2}", DateTime.Now.ToString("g"), Environment.NewLine, worklogForm.Comment.Trim());
                    UpdateOutput();
                }
            }

        }


        private void tbTime_TextChanged(object sender, EventArgs e)
        {
            // Ignore if programatically changing value (from timer)
            if (ignoreTextChange)
                return;

            // Validate time input
            TimeSpan time = JiraHelpers.JiraTimeToTimeSpan(tbTime.Text);
            if (time.TotalMilliseconds == 0)
                return;

            TimerState state = WatchTimer.GetState();
            state.TotalTime = time;
            state.StartTime = DateTime.Now;
            WatchTimer.SetState(state);

            UpdateOutput();
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

        private bool ignoreTextChange;
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
