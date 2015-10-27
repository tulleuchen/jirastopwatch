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
            this.lblIssueCount = new System.Windows.Forms.Label();
            this.numIssueCount = new System.Windows.Forms.NumericUpDown();
            this.cbAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSaveTimerState = new System.Windows.Forms.Label();
            this.rbNoSave = new System.Windows.Forms.RadioButton();
            this.gbSaveTimerState = new System.Windows.Forms.GroupBox();
            this.rbSaveRunActive = new System.Windows.Forms.RadioButton();
            this.rbSavePause = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numIssueCount)).BeginInit();
            this.gbSaveTimerState.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblJiraBaseUrl
            // 
            this.lblJiraBaseUrl.AutoSize = true;
            this.lblJiraBaseUrl.Location = new System.Drawing.Point(12, 9);
            this.lblJiraBaseUrl.Name = "lblJiraBaseUrl";
            this.lblJiraBaseUrl.Size = new System.Drawing.Size(92, 17);
            this.lblJiraBaseUrl.TabIndex = 0;
            this.lblJiraBaseUrl.Text = "JIRA base url";
            // 
            // tbJiraBaseUrl
            // 
            this.tbJiraBaseUrl.Location = new System.Drawing.Point(145, 6);
            this.tbJiraBaseUrl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbJiraBaseUrl.Name = "tbJiraBaseUrl";
            this.tbJiraBaseUrl.Size = new System.Drawing.Size(353, 22);
            this.tbJiraBaseUrl.TabIndex = 1;
            // 
            // lblIssueCount
            // 
            this.lblIssueCount.Location = new System.Drawing.Point(12, 43);
            this.lblIssueCount.Name = "lblIssueCount";
            this.lblIssueCount.Size = new System.Drawing.Size(127, 39);
            this.lblIssueCount.TabIndex = 2;
            this.lblIssueCount.Text = "# issueControls displayed (max. 20)";
            // 
            // numIssueCount
            // 
            this.numIssueCount.Location = new System.Drawing.Point(145, 41);
            this.numIssueCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.numIssueCount.Size = new System.Drawing.Size(47, 22);
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
            this.cbAlwaysOnTop.Location = new System.Drawing.Point(145, 84);
            this.cbAlwaysOnTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbAlwaysOnTop.Name = "cbAlwaysOnTop";
            this.cbAlwaysOnTop.Size = new System.Drawing.Size(201, 21);
            this.cbAlwaysOnTop.TabIndex = 4;
            this.cbAlwaysOnTop.Text = "Always keep window on top";
            this.cbAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(344, 222);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 27);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(425, 222);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblSaveTimerState
            // 
            this.lblSaveTimerState.AutoSize = true;
            this.lblSaveTimerState.Location = new System.Drawing.Point(12, 121);
            this.lblSaveTimerState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSaveTimerState.Name = "lblSaveTimerState";
            this.lblSaveTimerState.Size = new System.Drawing.Size(117, 17);
            this.lblSaveTimerState.TabIndex = 7;
            this.lblSaveTimerState.Text = "Save timer states";
            // 
            // rbNoSave
            // 
            this.rbNoSave.AutoSize = true;
            this.rbNoSave.Location = new System.Drawing.Point(8, 10);
            this.rbNoSave.Margin = new System.Windows.Forms.Padding(4);
            this.rbNoSave.Name = "rbNoSave";
            this.rbNoSave.Size = new System.Drawing.Size(171, 21);
            this.rbNoSave.TabIndex = 8;
            this.rbNoSave.TabStop = true;
            this.rbNoSave.Text = "Reset all timers on exit";
            this.rbNoSave.UseVisualStyleBackColor = true;
            // 
            // gbSaveTimerState
            // 
            this.gbSaveTimerState.Controls.Add(this.rbSaveRunActive);
            this.gbSaveTimerState.Controls.Add(this.rbSavePause);
            this.gbSaveTimerState.Controls.Add(this.rbNoSave);
            this.gbSaveTimerState.Location = new System.Drawing.Point(137, 111);
            this.gbSaveTimerState.Margin = new System.Windows.Forms.Padding(4);
            this.gbSaveTimerState.Name = "gbSaveTimerState";
            this.gbSaveTimerState.Padding = new System.Windows.Forms.Padding(4);
            this.gbSaveTimerState.Size = new System.Drawing.Size(312, 92);
            this.gbSaveTimerState.TabIndex = 9;
            this.gbSaveTimerState.TabStop = false;
            // 
            // rbSaveRunActive
            // 
            this.rbSaveRunActive.AutoSize = true;
            this.rbSaveRunActive.Location = new System.Drawing.Point(8, 66);
            this.rbSaveRunActive.Margin = new System.Windows.Forms.Padding(4);
            this.rbSaveRunActive.Name = "rbSaveRunActive";
            this.rbSaveRunActive.Size = new System.Drawing.Size(292, 21);
            this.rbSaveRunActive.TabIndex = 10;
            this.rbSaveRunActive.TabStop = true;
            this.rbSaveRunActive.Text = "Save current times, active timer continues";
            this.rbSaveRunActive.UseVisualStyleBackColor = true;
            // 
            // rbSavePause
            // 
            this.rbSavePause.AutoSize = true;
            this.rbSavePause.Location = new System.Drawing.Point(8, 38);
            this.rbSavePause.Margin = new System.Windows.Forms.Padding(4);
            this.rbSavePause.Name = "rbSavePause";
            this.rbSavePause.Size = new System.Drawing.Size(270, 21);
            this.rbSavePause.TabIndex = 9;
            this.rbSavePause.TabStop = true;
            this.rbSavePause.Text = "Save current times, pause active timer";
            this.rbSavePause.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(511, 262);
            this.Controls.Add(this.gbSaveTimerState);
            this.Controls.Add(this.lblSaveTimerState);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbAlwaysOnTop);
            this.Controls.Add(this.numIssueCount);
            this.Controls.Add(this.lblIssueCount);
            this.Controls.Add(this.tbJiraBaseUrl);
            this.Controls.Add(this.lblJiraBaseUrl);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "StopWatch Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.numIssueCount)).EndInit();
            this.gbSaveTimerState.ResumeLayout(false);
            this.gbSaveTimerState.PerformLayout();
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
        private System.Windows.Forms.RadioButton rbNoSave;
        private System.Windows.Forms.GroupBox gbSaveTimerState;
        private System.Windows.Forms.RadioButton rbSaveRunActive;
        private System.Windows.Forms.RadioButton rbSavePause;
    }
}