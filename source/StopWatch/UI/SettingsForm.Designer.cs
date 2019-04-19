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
            this.lblJiraBaseUrl = new System.Windows.Forms.Label();
            this.tbJiraBaseUrl = new System.Windows.Forms.TextBox();
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
            this.lblDisplayOptions = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStartTransitions = new System.Windows.Forms.Label();
            this.tbStartTransitions = new System.Windows.Forms.TextBox();
            this.cbLoggingEnabbled = new System.Windows.Forms.CheckBox();
            this.lblOpenLogFolder = new System.Windows.Forms.LinkLabel();
            this.cbCheckForUpdate = new System.Windows.Forms.CheckBox();
            this.tbApiToken = new System.Windows.Forms.TextBox();
            this.lblApiToken = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.cbIncludeProjectName = new System.Windows.Forms.CheckBox();
            this.lblOpenAPITokensPage = new System.Windows.Forms.LinkLabel();
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
            this.tbJiraBaseUrl.Location = new System.Drawing.Point(122, 5);
            this.tbJiraBaseUrl.Margin = new System.Windows.Forms.Padding(2);
            this.tbJiraBaseUrl.Name = "tbJiraBaseUrl";
            this.tbJiraBaseUrl.Size = new System.Drawing.Size(259, 20);
            this.tbJiraBaseUrl.TabIndex = 1;
            // 
            // cbAlwaysOnTop
            // 
            this.cbAlwaysOnTop.AutoSize = true;
            this.cbAlwaysOnTop.Location = new System.Drawing.Point(122, 125);
            this.cbAlwaysOnTop.Margin = new System.Windows.Forms.Padding(2);
            this.cbAlwaysOnTop.Name = "cbAlwaysOnTop";
            this.cbAlwaysOnTop.Size = new System.Drawing.Size(158, 17);
            this.cbAlwaysOnTop.TabIndex = 10;
            this.cbAlwaysOnTop.Text = "Always keep window on top";
            this.cbAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(263, 464);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(56, 22);
            this.btnOk.TabIndex = 27;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(324, 464);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 22);
            this.btnCancel.TabIndex = 28;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblSaveTimerState
            // 
            this.lblSaveTimerState.Location = new System.Drawing.Point(9, 227);
            this.lblSaveTimerState.Name = "lblSaveTimerState";
            this.lblSaveTimerState.Size = new System.Drawing.Size(98, 38);
            this.lblSaveTimerState.TabIndex = 14;
            this.lblSaveTimerState.Text = "Save timer states on program exit";
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(11, 464);
            this.btnAbout.Margin = new System.Windows.Forms.Padding(2);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(56, 22);
            this.btnAbout.TabIndex = 26;
            this.btnAbout.Text = "About...";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // cbMinimizeToTray
            // 
            this.cbMinimizeToTray.AutoSize = true;
            this.cbMinimizeToTray.Location = new System.Drawing.Point(122, 146);
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
            this.cbSaveTimerState.Location = new System.Drawing.Point(122, 229);
            this.cbSaveTimerState.Name = "cbSaveTimerState";
            this.cbSaveTimerState.Size = new System.Drawing.Size(258, 21);
            this.cbSaveTimerState.TabIndex = 15;
            // 
            // lblPauseOnSessionLock
            // 
            this.lblPauseOnSessionLock.Location = new System.Drawing.Point(9, 265);
            this.lblPauseOnSessionLock.Name = "lblPauseOnSessionLock";
            this.lblPauseOnSessionLock.Size = new System.Drawing.Size(98, 38);
            this.lblPauseOnSessionLock.TabIndex = 16;
            this.lblPauseOnSessionLock.Text = "Pause timer on session lock";
            // 
            // cbPauseOnSessionLock
            // 
            this.cbPauseOnSessionLock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPauseOnSessionLock.FormattingEnabled = true;
            this.cbPauseOnSessionLock.Location = new System.Drawing.Point(122, 267);
            this.cbPauseOnSessionLock.Name = "cbPauseOnSessionLock";
            this.cbPauseOnSessionLock.Size = new System.Drawing.Size(176, 21);
            this.cbPauseOnSessionLock.TabIndex = 17;
            // 
            // splitter3
            // 
            this.splitter3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter3.Location = new System.Drawing.Point(12, 453);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(370, 2);
            this.splitter3.TabIndex = 25;
            // 
            // splitter2
            // 
            this.splitter2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter2.Location = new System.Drawing.Point(12, 215);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(370, 2);
            this.splitter2.TabIndex = 13;
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter1.Location = new System.Drawing.Point(12, 116);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(370, 2);
            this.splitter1.TabIndex = 8;
            // 
            // cbAllowMultipleTimers
            // 
            this.cbAllowMultipleTimers.AutoSize = true;
            this.cbAllowMultipleTimers.Location = new System.Drawing.Point(122, 305);
            this.cbAllowMultipleTimers.Margin = new System.Windows.Forms.Padding(2);
            this.cbAllowMultipleTimers.Name = "cbAllowMultipleTimers";
            this.cbAllowMultipleTimers.Size = new System.Drawing.Size(228, 17);
            this.cbAllowMultipleTimers.TabIndex = 18;
            this.cbAllowMultipleTimers.Text = "Allow running multiple timers simultaneously";
            this.cbAllowMultipleTimers.UseVisualStyleBackColor = true;
            // 
            // cbPostWorklogComment
            // 
            this.cbPostWorklogComment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPostWorklogComment.FormattingEnabled = true;
            this.cbPostWorklogComment.Location = new System.Drawing.Point(122, 334);
            this.cbPostWorklogComment.Name = "cbPostWorklogComment";
            this.cbPostWorklogComment.Size = new System.Drawing.Size(198, 21);
            this.cbPostWorklogComment.TabIndex = 20;
            // 
            // lblPostWorklogComment
            // 
            this.lblPostWorklogComment.Location = new System.Drawing.Point(9, 332);
            this.lblPostWorklogComment.Name = "lblPostWorklogComment";
            this.lblPostWorklogComment.Size = new System.Drawing.Size(98, 38);
            this.lblPostWorklogComment.TabIndex = 19;
            this.lblPostWorklogComment.Text = "How to post the worklog comment";
            // 
            // lblDisplayOptions
            // 
            this.lblDisplayOptions.Location = new System.Drawing.Point(9, 127);
            this.lblDisplayOptions.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDisplayOptions.Name = "lblDisplayOptions";
            this.lblDisplayOptions.Size = new System.Drawing.Size(107, 34);
            this.lblDisplayOptions.TabIndex = 9;
            this.lblDisplayOptions.Text = "General options";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(11, 372);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(370, 2);
            this.label1.TabIndex = 22;
            // 
            // lblStartTransitions
            // 
            this.lblStartTransitions.Location = new System.Drawing.Point(9, 386);
            this.lblStartTransitions.Name = "lblStartTransitions";
            this.lblStartTransitions.Size = new System.Drawing.Size(107, 52);
            this.lblStartTransitions.TabIndex = 23;
            this.lblStartTransitions.Text = "Possible state changes when pressing play (seperate by newline)";
            // 
            // tbStartTransitions
            // 
            this.tbStartTransitions.AcceptsReturn = true;
            this.tbStartTransitions.Location = new System.Drawing.Point(122, 386);
            this.tbStartTransitions.Multiline = true;
            this.tbStartTransitions.Name = "tbStartTransitions";
            this.tbStartTransitions.Size = new System.Drawing.Size(200, 52);
            this.tbStartTransitions.TabIndex = 24;
            // 
            // cbLoggingEnabbled
            // 
            this.cbLoggingEnabbled.AutoSize = true;
            this.cbLoggingEnabbled.Location = new System.Drawing.Point(122, 89);
            this.cbLoggingEnabbled.Margin = new System.Windows.Forms.Padding(2);
            this.cbLoggingEnabbled.Name = "cbLoggingEnabbled";
            this.cbLoggingEnabbled.Size = new System.Drawing.Size(132, 17);
            this.cbLoggingEnabbled.TabIndex = 6;
            this.cbLoggingEnabbled.Text = "Enable debug logging ";
            this.cbLoggingEnabbled.UseVisualStyleBackColor = true;
            // 
            // lblOpenLogFolder
            // 
            this.lblOpenLogFolder.AutoSize = true;
            this.lblOpenLogFolder.Location = new System.Drawing.Point(264, 90);
            this.lblOpenLogFolder.Name = "lblOpenLogFolder";
            this.lblOpenLogFolder.Size = new System.Drawing.Size(79, 13);
            this.lblOpenLogFolder.TabIndex = 7;
            this.lblOpenLogFolder.TabStop = true;
            this.lblOpenLogFolder.Text = "Open log folder";
            this.lblOpenLogFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblOpenLogFolder_LinkClicked);
            // 
            // cbCheckForUpdate
            // 
            this.cbCheckForUpdate.AutoSize = true;
            this.cbCheckForUpdate.Location = new System.Drawing.Point(122, 167);
            this.cbCheckForUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.cbCheckForUpdate.Name = "cbCheckForUpdate";
            this.cbCheckForUpdate.Size = new System.Drawing.Size(205, 17);
            this.cbCheckForUpdate.TabIndex = 12;
            this.cbCheckForUpdate.Text = "Check for updates on application start";
            this.cbCheckForUpdate.UseVisualStyleBackColor = true;
            // 
            // tbApiToken
            // 
            this.tbApiToken.Location = new System.Drawing.Point(122, 56);
            this.tbApiToken.Margin = new System.Windows.Forms.Padding(2);
            this.tbApiToken.Name = "tbApiToken";
            this.tbApiToken.PasswordChar = '*';
            this.tbApiToken.Size = new System.Drawing.Size(129, 20);
            this.tbApiToken.TabIndex = 5;
            // 
            // lblApiToken
            // 
            this.lblApiToken.AutoSize = true;
            this.lblApiToken.Location = new System.Drawing.Point(9, 59);
            this.lblApiToken.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblApiToken.Name = "lblApiToken";
            this.lblApiToken.Size = new System.Drawing.Size(58, 13);
            this.lblApiToken.TabIndex = 4;
            this.lblApiToken.Text = "API Token";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(122, 31);
            this.tbUsername.Margin = new System.Windows.Forms.Padding(2);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(129, 20);
            this.tbUsername.TabIndex = 3;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(9, 34);
            this.lblUsername.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Username";
            // 
            // cbIncludeProjectName
            // 
            this.cbIncludeProjectName.AutoSize = true;
            this.cbIncludeProjectName.Location = new System.Drawing.Point(122, 188);
            this.cbIncludeProjectName.Margin = new System.Windows.Forms.Padding(2);
            this.cbIncludeProjectName.Name = "cbIncludeProjectName";
            this.cbIncludeProjectName.Size = new System.Drawing.Size(207, 17);
            this.cbIncludeProjectName.TabIndex = 13;
            this.cbIncludeProjectName.Text = "Include project name in issue summary";
            this.cbIncludeProjectName.UseVisualStyleBackColor = true;
            // 
            // lblOpenAPITokensPage
            // 
            this.lblOpenAPITokensPage.AutoSize = true;
            this.lblOpenAPITokensPage.Location = new System.Drawing.Point(257, 62);
            this.lblOpenAPITokensPage.Name = "lblOpenAPITokensPage";
            this.lblOpenAPITokensPage.Size = new System.Drawing.Size(93, 13);
            this.lblOpenAPITokensPage.TabIndex = 29;
            this.lblOpenAPITokensPage.TabStop = true;
            this.lblOpenAPITokensPage.Text = "Get an API Token";
            this.lblOpenAPITokensPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblOpenAPITokensPage_LinkClicked);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(391, 494);
            this.Controls.Add(this.lblOpenAPITokensPage);
            this.Controls.Add(this.cbIncludeProjectName);
            this.Controls.Add(this.tbApiToken);
            this.Controls.Add(this.lblApiToken);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.cbCheckForUpdate);
            this.Controls.Add(this.lblOpenLogFolder);
            this.Controls.Add(this.cbLoggingEnabbled);
            this.Controls.Add(this.tbStartTransitions);
            this.Controls.Add(this.lblStartTransitions);
            this.Controls.Add(this.label1);
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
            this.Controls.Add(this.lblDisplayOptions);
            this.Controls.Add(this.tbJiraBaseUrl);
            this.Controls.Add(this.lblJiraBaseUrl);
            this.Icon = global::StopWatch.Properties.Resources.stopwatchicon;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StopWatch Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblJiraBaseUrl;
        private System.Windows.Forms.TextBox tbJiraBaseUrl;
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
        private System.Windows.Forms.Label lblDisplayOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStartTransitions;
        private System.Windows.Forms.TextBox tbStartTransitions;
        private System.Windows.Forms.CheckBox cbLoggingEnabbled;
        private System.Windows.Forms.LinkLabel lblOpenLogFolder;
        private System.Windows.Forms.CheckBox cbCheckForUpdate;
        private System.Windows.Forms.TextBox tbApiToken;
        private System.Windows.Forms.Label lblApiToken;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.CheckBox cbIncludeProjectName;
        private System.Windows.Forms.LinkLabel lblOpenAPITokensPage;
    }
}
