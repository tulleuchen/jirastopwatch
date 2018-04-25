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

        public bool MarkedForRemoval
        {
            get
            {
                return _MarkedForRemoval;
            }
        }

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
        public EstimateUpdateMethods EstimateUpdateMethod { get; set; }
        public string EstimateUpdateValue { get; set; }

        public bool Current
        {
            get
            {
                return Current;
            }
            set
            {
                BackColor = value ? SystemColors.GradientInactiveCaption : SystemColors.Window;
            }
        }

        public event EventHandler RemoveMeTriggered;

        public event EventHandler Selected;

        public event EventHandler TimeEdited;
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

            cbJiraTbEvents = new ComboTextBoxEvents(cbJira);
            cbJiraTbEvents.Paste += cbJiraTbEvents_Paste;
            cbJiraTbEvents.MouseDown += CbJiraTbEvents_MouseDown;

            Comment = null;
            EstimateUpdateMethod = EstimateUpdateMethods.Auto;
            EstimateUpdateValue = null;

            this.settings = settings;

            this.jiraClient = jiraClient;
            this.WatchTimer = new WatchTimer();
        }

        private void CbJiraTbEvents_MouseDown(object sender, EventArgs e)
        {
            SetSelected();
        }

        public void ToggleRemoveIssueButton(bool Enable)
        {
            this.btnRemoveIssue.Enabled = Enable;
        }

        public bool focusJiraField()
        {
            return this.cbJira.Focus();
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
            btnPostAndReset.Enabled = WatchTimer.TimeElapsedNearestMinute.TotalMinutes >= 1;

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

        public void FocusKey()
        {
            cbJira.Focus();
        }
        #endregion


        #region private methods
        public void OpenJira()
        {
            if (cbJira.Text == "")
                return;

            OpenIssueInBrowser(cbJira.Text);
        }


        private void UpdateSummary()
        {

            if (cbJira.Text == "")
            {
                lblSummary.Text = "";
                return;
            }
            if (!jiraClient.SessionValid)
            {
                lblSummary.Text = "";
                return;
            }

            Task.Factory.StartNew(
                () => {
                    string key = "";
                    string summary = "";
                    this.InvokeIfRequired(
                        () => key = cbJira.Text
                    );
                    try
                    {
                        summary = jiraClient.GetIssueSummary(key, settings.IncludeProjectName);
                        this.InvokeIfRequired(
                            () => lblSummary.Text = summary
                        );
                    }
                    catch (RequestDeniedException)
                    {
                        // just leave the existing summary there when fetch fails
                    }
                }
            );
        }

        private void UpdateRemainingEstimate(WorklogForm  worklogForm)
        {
            RemainingEstimate = "";
            RemainingEstimateSeconds = -1;

            if (cbJira.Text == "")
                return;
            if (!jiraClient.SessionValid)
                return;

            Task.Factory.StartNew(
                () =>
                {
                    string key = "";
                    this.InvokeIfRequired(
                        () => key = cbJira.Text
                    );

                    TimetrackingFields timetracking = jiraClient.GetIssueTimetracking(key);
                    if (timetracking == null)
                        return;

                    this.InvokeIfRequired(
                        () => RemainingEstimate = timetracking.RemainingEstimate
                    );
                    this.InvokeIfRequired(
                        () => RemainingEstimateSeconds = timetracking.RemainingEstimateSeconds
                    );
                    if (worklogForm != null)
                    {
                        this.InvokeIfRequired(
                            () => worklogForm.RemainingEstimate = timetracking.RemainingEstimate
                        );
                        this.InvokeIfRequired(
                            () => worklogForm.RemainingEstimateSeconds = timetracking.RemainingEstimateSeconds
                        );                        
                    }
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
            this.btnRemoveIssue = new System.Windows.Forms.Button();
            this.btnPostAndReset = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbJira
            // 
            this.cbJira.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbJira.DisplayMember = "Key";
            this.cbJira.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cbJira.DropDownHeight = 90;
            this.cbJira.DropDownWidth = 488;
            this.cbJira.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.cbJira.IntegralHeight = false;
            this.cbJira.Location = new System.Drawing.Point(12, 5);
            this.cbJira.Name = "cbJira";
            this.cbJira.Size = new System.Drawing.Size(155, 28);
            this.cbJira.TabIndex = 0;
            this.cbJira.ValueMember = "Key";
            this.cbJira.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbJira_DrawItem);
            this.cbJira.DropDown += new System.EventHandler(this.cbJira_DropDown);
            this.cbJira.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cbJira_MeasureItem);
            this.cbJira.SelectionChangeCommitted += new System.EventHandler(this.cbJira_SelectionChangeCommitted);
            this.cbJira.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbJira_KeyDown);
            this.cbJira.Leave += new System.EventHandler(this.cbJira_Leave);
            // 
            // tbTime
            // 
            this.tbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.3F);
            this.tbTime.Location = new System.Drawing.Point(256, 5);
            this.tbTime.Name = "tbTime";
            this.tbTime.ReadOnly = true;
            this.tbTime.Size = new System.Drawing.Size(107, 28);
            this.tbTime.TabIndex = 3;
            this.tbTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTime_KeyDown);
            this.tbTime.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tbTime_MouseDoubleClick);
            this.tbTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbTime_MouseUp);
            // 
            // lblSummary
            // 
            this.lblSummary.AutoEllipsis = true;
            this.lblSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.Location = new System.Drawing.Point(11, 36);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(482, 17);
            this.lblSummary.TabIndex = 6;
            this.lblSummary.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblSummary_MouseUp);
            // 
            // btnRemoveIssue
            // 
            this.btnRemoveIssue.Enabled = false;
            this.btnRemoveIssue.Image = global::StopWatch.Properties.Resources.delete24;
            this.btnRemoveIssue.Location = new System.Drawing.Point(465, 3);
            this.btnRemoveIssue.Name = "btnRemoveIssue";
            this.btnRemoveIssue.Size = new System.Drawing.Size(32, 32);
            this.btnRemoveIssue.TabIndex = 7;
            this.ttIssue.SetToolTip(this.btnRemoveIssue, "Remove issue row (CTRL-DEL)");
            this.btnRemoveIssue.UseVisualStyleBackColor = true;
            this.btnRemoveIssue.Click += new System.EventHandler(this.btnRemoveIssue_Click);
            // 
            // btnPostAndReset
            // 
            this.btnPostAndReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPostAndReset.Image = global::StopWatch.Properties.Resources.posttime26;
            this.btnPostAndReset.Location = new System.Drawing.Point(369, 3);
            this.btnPostAndReset.Name = "btnPostAndReset";
            this.btnPostAndReset.Size = new System.Drawing.Size(32, 32);
            this.btnPostAndReset.TabIndex = 4;
            this.ttIssue.SetToolTip(this.btnPostAndReset, "Submit worklog to Jira and reset timer (CTRL-L)");
            this.btnPostAndReset.UseVisualStyleBackColor = true;
            this.btnPostAndReset.Click += new System.EventHandler(this.btnPostAndReset_Click);
            this.btnPostAndReset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPostAndReset_MouseUp);
            // 
            // btnReset
            // 
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.Image = global::StopWatch.Properties.Resources.reset24;
            this.btnReset.Location = new System.Drawing.Point(429, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(32, 32);
            this.btnReset.TabIndex = 5;
            this.ttIssue.SetToolTip(this.btnReset, "Reset timer (CTRL-R)");
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            this.btnReset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnReset_MouseUp);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartStop.Image = global::StopWatch.Properties.Resources.play26;
            this.btnStartStop.Location = new System.Drawing.Point(220, 3);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(32, 32);
            this.btnStartStop.TabIndex = 2;
            this.ttIssue.SetToolTip(this.btnStartStop, "Start/stop timer (CTRL-P)");
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            this.btnStartStop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnStartStop_MouseUp);
            // 
            // btnOpen
            // 
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpen.Image = global::StopWatch.Properties.Resources.openbrowser26;
            this.btnOpen.Location = new System.Drawing.Point(168, 3);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(32, 32);
            this.btnOpen.TabIndex = 1;
            this.ttIssue.SetToolTip(this.btnOpen, "Open issue in browser (CTRL-O)");
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            this.btnOpen.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnOpen_MouseUp);
            // 
            // IssueControl
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.btnRemoveIssue);
            this.Controls.Add(this.btnPostAndReset);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.tbTime);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.cbJira);
            this.Name = "IssueControl";
            this.Size = new System.Drawing.Size(517, 58);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.IssueControl_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        public void Reset()
        {
            Comment = null;
            EstimateUpdateMethod = EstimateUpdateMethods.Auto;
            EstimateUpdateValue = null;
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
            if (e.Index == 0)
                keyWidth = 0;
            CBIssueItem item = (CBIssueItem)cbJira.Items[e.Index];
            Font font = new Font(cbJira.Font.FontFamily, cbJira.Font.Size * 0.8f, cbJira.Font.Style);
            Size size = TextRenderer.MeasureText(e.Graphics, item.Key, font);
            e.ItemHeight = size.Height;
            if (keyWidth < size.Width)
                keyWidth = size.Width;
        }


        void cbJira_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the default background
            e.DrawBackground();

            CBIssueItem item = (CBIssueItem)cbJira.Items[e.Index];

            // Create rectangles for the columns to display
            Rectangle r1 = e.Bounds;
            Rectangle r2 = e.Bounds;

            r1.Width = keyWidth;

            r2.X = r1.Width + 5;
            r2.Width = 500 - keyWidth;

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


        private void cbJira_DropDown(object sender, EventArgs e)
        {
            LoadIssues();
        }


        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenJira();
        }


        private void cbJira_Leave(object sender, EventArgs e)
        {
            UpdateOutput(true);
        }


        private void cbJira_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.Enter)
                UpdateOutput(true);
        }


        private void cbJiraTbEvents_Paste(object sender, EventArgs e)
        {
            PasteKeyFromClipboard();
        }

        public void PasteKeyFromClipboard()
        {
            if (Clipboard.ContainsText())
            {
                cbJira.Text = JiraKeyHelpers.ParseUrlToKey(Clipboard.GetText());
                UpdateOutput(true);
            }
        }

        public void CopyKeyToClipboard()
        {
            if (string.IsNullOrEmpty(cbJira.Text))
                return;
            Clipboard.SetText(cbJira.Text);
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            StartStop();
        }

        public void StartStop()
        {
            if (WatchTimer.Running) {
                this.WatchTimer.Pause();
            }
            else {
                this.WatchTimer.Start();

                this.TimerStarted?.Invoke(this, new EventArgs());
            }
            UpdateOutput(true);
        }

        private void btnRemoveIssue_Click(object sender, EventArgs e)
        {
            Remove();
        }

        public void Remove()
        {
            this._MarkedForRemoval = true;
            RemoveMeTriggered?.Invoke(this, new EventArgs());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }


        private void btnPostAndReset_Click(object sender, EventArgs e)
        {
            PostAndReset();
        }

        public DialogResult PostAndReset()
        {
            using (var worklogForm = new WorklogForm(WatchTimer.GetInitialStartTime(), WatchTimer.TimeElapsedNearestMinute, Comment, EstimateUpdateMethod, EstimateUpdateValue))
            {
                UpdateRemainingEstimate(worklogForm);
                var formResult = worklogForm.ShowDialog(this);
                if (formResult == DialogResult.OK)
                {
                    Comment = worklogForm.Comment.Trim();
                    EstimateUpdateMethod = worklogForm.estimateUpdateMethod;
                    EstimateUpdateValue = worklogForm.EstimateValue;

                    PostAndReset(cbJira.Text, worklogForm.InitialStartTime, WatchTimer.TimeElapsedNearestMinute, Comment, EstimateUpdateMethod, EstimateUpdateValue);
                }
                else if (formResult == DialogResult.Yes)
                {
                    Comment = string.Format("{0}:{1}{2}", DateTime.Now.ToString("g"), Environment.NewLine, worklogForm.Comment.Trim());
                    EstimateUpdateMethod = worklogForm.estimateUpdateMethod;
                    EstimateUpdateValue = worklogForm.EstimateValue;
                    UpdateOutput();
                }
                return formResult;
            }
        }

        private void tbTime_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditTime();

        }


        private void tbTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyCode == Keys.Enter)
                EditTime();
        }
        #endregion


        #region private methods
        private void PostAndReset(string key, DateTimeOffset startTime, TimeSpan timeElapsed, string comment, EstimateUpdateMethods estimateUpdateMethod, string estimateUpdateValue)
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
                        postSuccesful = jiraClient.PostWorklog(key, startTime, timeElapsed, comment, estimateUpdateMethod, estimateUpdateValue);

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


        public void EditTime()
        {
            using (var editTimeForm = new EditTimeForm(WatchTimer.TimeElapsed))
            {
                if (editTimeForm.ShowDialog(this) == DialogResult.OK)
                {
                    WatchTimer.TimeElapsed = editTimeForm.Time;

                    UpdateOutput();

                    TimeEdited?.Invoke(this, new EventArgs());
                }
            }
        }


        public void OpenCombo()
        {
            cbJira.Focus();
            cbJira.DroppedDown = true;
        }


        private void SetSelected()
        {
            Selected?.Invoke(this, new EventArgs());
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
        private Button btnPostAndReset;

        private JiraClient jiraClient;

        private Settings settings;

        private int keyWidth;
        private string RemainingEstimate;
        private int RemainingEstimateSeconds;
        private Button btnRemoveIssue;
        private bool _MarkedForRemoval = false;

        private ComboTextBoxEvents cbJiraTbEvents;
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

        private void IssueControl_MouseUp(object sender, MouseEventArgs e)
        {
            SetSelected();
        }

        private void btnOpen_MouseUp(object sender, MouseEventArgs e)
        {
            SetSelected();
        }

        private void btnStartStop_MouseUp(object sender, MouseEventArgs e)
        {
            SetSelected();
        }

        private void tbTime_MouseUp(object sender, MouseEventArgs e)
        {
            SetSelected();
        }

        private void btnPostAndReset_MouseUp(object sender, MouseEventArgs e)
        {
            SetSelected();
        }

        private void btnReset_MouseUp(object sender, MouseEventArgs e)
        {
            SetSelected();
        }

        private void lblSummary_MouseUp(object sender, MouseEventArgs e)
        {
            SetSelected();
        }

        private void cbJira_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetSelected();
            UpdateOutput(true);
        }
    }
}
