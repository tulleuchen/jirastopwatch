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
namespace StopWatch
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.lblJiraBaseUrl = new System.Windows.Forms.Label();
            this.tbJiraBaseUrl = new System.Windows.Forms.TextBox();
            this.lblIssueCount = new System.Windows.Forms.Label();
            this.numIssueCount = new System.Windows.Forms.NumericUpDown();
            this.cbAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSaveTimerState = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.cbMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.cbSaveTimerState = new System.Windows.Forms.ComboBox();
            this.lblPauseOnSessionLock = new System.Windows.Forms.Label();
            this.cbPauseOnSessionLock = new System.Windows.Forms.ComboBox();
            this.splitter3 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Label();
            this.cbAllowMultipleTimers = new System.Windows.Forms.CheckBox();
            this.cbPostWorklogComment = new System.Windows.Forms.ComboBox();
            this.lblPostWorklogComment = new System.Windows.Forms.Label();
            this.cbAllowFlexibleIssueCount = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numIssueCount)).BeginInit();
            this.SuspendLayout();
            // 
            // lblJiraBaseUrl
            // 
            this.lblJiraBaseUrl.AutoSize = true;
            this.lblJiraBaseUrl.Location = new System.Drawing.Point(9, 7);
            this.lblJiraBaseUrl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblJiraBaseUrl.Name = "lblJiraBaseUrl";
            this.lblJiraBaseUrl.Size = new System.Drawing.Size(70, 13);
            this.lblJiraBaseUrl.TabIndex = 0;
            this.lblJiraBaseUrl.Text = "JIRA base url";
            // 
            // tbJiraBaseUrl
            // 
            this.tbJiraBaseUrl.Location = new System.Drawing.Point(113, 5);
            this.tbJiraBaseUrl.Margin = new System.Windows.Forms.Padding(2);
            this.tbJiraBaseUrl.Name = "tbJiraBaseUrl";
            this.tbJiraBaseUrl.Size = new System.Drawing.Size(259, 20);
            this.tbJiraBaseUrl.TabIndex = 1;
            // 
            // lblIssueCount
            // 
            this.lblIssueCount.Location = new System.Drawing.Point(9, 48);
            this.lblIssueCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIssueCount.Name = "lblIssueCount";
            this.lblIssueCount.Size = new System.Drawing.Size(107, 34);
            this.lblIssueCount.TabIndex = 2;
            this.lblIssueCount.Text = "# issueControls displayed (max. 20)";
            // 
            // numIssueCount
            // 
            this.numIssueCount.Location = new System.Drawing.Point(114, 48);
            this.numIssueCount.Margin = new System.Windows.Forms.Padding(2);
            this.numIssueCount.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numIssueCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIssueCount.Name = "numIssueCount";
            this.numIssueCount.Size = new System.Drawing.Size(35, 20);
            this.numIssueCount.TabIndex = 3;
            this.numIssueCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbAlwaysOnTop
            // 
            this.cbAlwaysOnTop.AutoSize = true;
            this.cbAlwaysOnTop.Location = new System.Drawing.Point(113, 84);
            this.cbAlwaysOnTop.Margin = new System.Windows.Forms.Padding(2);
            this.cbAlwaysOnTop.Name = "cbAlwaysOnTop";
            this.cbAlwaysOnTop.Size = new System.Drawing.Size(158, 17);
            this.cbAlwaysOnTop.TabIndex = 4;
            this.cbAlwaysOnTop.Text = "Always keep window on top";
            this.cbAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(255, 298);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(56, 22);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(316, 298);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 22);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblSaveTimerState
            // 
            this.lblSaveTimerState.Location = new System.Drawing.Point(9, 143);
            this.lblSaveTimerState.Name = "lblSaveTimerState";
            this.lblSaveTimerState.Size = new System.Drawing.Size(98, 38);
            this.lblSaveTimerState.TabIndex = 5;
            this.lblSaveTimerState.Text = "Save timer states on program exit";
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(11, 298);
            this.btnAbout.Margin = new System.Windows.Forms.Padding(2);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(56, 22);
            this.btnAbout.TabIndex = 10;
            this.btnAbout.Text = "About...";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // cbMinimizeToTray
            // 
            this.cbMinimizeToTray.AutoSize = true;
            this.cbMinimizeToTray.Location = new System.Drawing.Point(113, 105);
            this.cbMinimizeToTray.Margin = new System.Windows.Forms.Padding(2);
            this.cbMinimizeToTray.Name = "cbMinimizeToTray";
            this.cbMinimizeToTray.Size = new System.Drawing.Size(98, 17);
            this.cbMinimizeToTray.TabIndex = 11;
            this.cbMinimizeToTray.Text = "Minimize to tray";
            this.cbMinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // cbSaveTimerState
            // 
            this.cbSaveTimerState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSaveTimerState.FormattingEnabled = true;
            this.cbSaveTimerState.Location = new System.Drawing.Point(113, 145);
            this.cbSaveTimerState.Name = "cbSaveTimerState";
            this.cbSaveTimerState.Size = new System.Drawing.Size(258, 21);
            this.cbSaveTimerState.TabIndex = 13;
            // 
            // lblPauseOnSessionLock
            // 
            this.lblPauseOnSessionLock.Location = new System.Drawing.Point(9, 181);
            this.lblPauseOnSessionLock.Name = "lblPauseOnSessionLock";
            this.lblPauseOnSessionLock.Size = new System.Drawing.Size(98, 38);
            this.lblPauseOnSessionLock.TabIndex = 13;
            this.lblPauseOnSessionLock.Text = "Pause timer on session lock";
            // 
            // cbPauseOnSessionLock
            // 
            this.cbPauseOnSessionLock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPauseOnSessionLock.FormattingEnabled = true;
            this.cbPauseOnSessionLock.Location = new System.Drawing.Point(113, 183);
            this.cbPauseOnSessionLock.Name = "cbPauseOnSessionLock";
            this.cbPauseOnSessionLock.Size = new System.Drawing.Size(176, 21);
            this.cbPauseOnSessionLock.TabIndex = 14;
            // 
            // splitter3
            // 
            this.splitter3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter3.Location = new System.Drawing.Point(12, 287);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(360, 2);
            this.splitter3.TabIndex = 16;
            // 
            // splitter2
            // 
            this.splitter2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter2.Location = new System.Drawing.Point(12, 131);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(360, 2);
            this.splitter2.TabIndex = 15;
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter1.Location = new System.Drawing.Point(12, 37);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(360, 2);
            this.splitter1.TabIndex = 14;
            // 
            // cbAllowMultipleTimers
            // 
            this.cbAllowMultipleTimers.AutoSize = true;
            this.cbAllowMultipleTimers.Location = new System.Drawing.Point(114, 221);
            this.cbAllowMultipleTimers.Margin = new System.Windows.Forms.Padding(2);
            this.cbAllowMultipleTimers.Name = "cbAllowMultipleTimers";
            this.cbAllowMultipleTimers.Size = new System.Drawing.Size(228, 17);
            this.cbAllowMultipleTimers.TabIndex = 17;
            this.cbAllowMultipleTimers.Text = "Allow running multiple timers simultaneously";
            this.cbAllowMultipleTimers.UseVisualStyleBackColor = true;
            // 
            // cbPostWorklogComment
            // 
            this.cbPostWorklogComment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPostWorklogComment.FormattingEnabled = true;
            this.cbPostWorklogComment.Location = new System.Drawing.Point(113, 250);
            this.cbPostWorklogComment.Name = "cbPostWorklogComment";
            this.cbPostWorklogComment.Size = new System.Drawing.Size(198, 21);
            this.cbPostWorklogComment.TabIndex = 19;
            // 
            // lblPostWorklogComment
            // 
            this.lblPostWorklogComment.Location = new System.Drawing.Point(9, 248);
            this.lblPostWorklogComment.Name = "lblPostWorklogComment";
            this.lblPostWorklogComment.Size = new System.Drawing.Size(98, 38);
            this.lblPostWorklogComment.TabIndex = 18;
            this.lblPostWorklogComment.Text = "How to post the worklog comment";
            // 
            // cbAllowFlexibleIssueCount
            // 
            this.cbAllowFlexibleIssueCount.AutoSize = true;
            this.cbAllowFlexibleIssueCount.Location = new System.Drawing.Point(164, 48);
            this.cbAllowFlexibleIssueCount.Name = "cbAllowFlexibleIssueCount";
            this.cbAllowFlexibleIssueCount.Size = new System.Drawing.Size(185, 17);
            this.cbAllowFlexibleIssueCount.TabIndex = 21;
            this.cbAllowFlexibleIssueCount.Text = "Allow changing count dynamically";
            this.cbAllowFlexibleIssueCount.UseVisualStyleBackColor = true;            
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(383, 352);
            this.Controls.Add(this.cbAllowFlexibleIssueCount);
            this.Controls.Add(this.cbAllowManualEstimateAdjustments);
            this.Controls.Add(this.cbPostWorklogComment);
            this.Controls.Add(this.lblPostWorklogComment);
            this.Controls.Add(this.cbAllowMultipleTimers);
            this.Controls.Add(this.cbPauseOnSessionLock);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.cbSaveTimerState);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lblPauseOnSessionLock);
            this.Controls.Add(this.cbMinimizeToTray);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.lblSaveTimerState);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbAlwaysOnTop);
            this.Controls.Add(this.numIssueCount);
            this.Controls.Add(this.lblIssueCount);
            this.Controls.Add(this.tbJiraBaseUrl);
            this.Controls.Add(this.lblJiraBaseUrl);
            this.Icon = Properties.Resources.stopwatchicon;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StopWatch Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.numIssueCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblJiraBaseUrl;
        private System.Windows.Forms.TextBox tbJiraBaseUrl;
        private System.Windows.Forms.Label lblIssueCount;
        private System.Windows.Forms.NumericUpDown numIssueCount;
        private System.Windows.Forms.CheckBox cbAlwaysOnTop;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSaveTimerState;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.CheckBox cbMinimizeToTray;
        private System.Windows.Forms.ComboBox cbSaveTimerState;
        private System.Windows.Forms.ComboBox cbPauseOnSessionLock;
        private System.Windows.Forms.Label lblPauseOnSessionLock;
        private System.Windows.Forms.Label splitter3;
        private System.Windows.Forms.Label splitter2;
        private System.Windows.Forms.Label splitter1;
        private System.Windows.Forms.CheckBox cbAllowMultipleTimers;
        private System.Windows.Forms.ComboBox cbPostWorklogComment;
        private System.Windows.Forms.Label lblPostWorklogComment;
        private System.Windows.Forms.CheckBox cbAllowFlexibleIssueCount;
    }
}
