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
            this.cbTimerEditable = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numIssueCount)).BeginInit();
            this.gbSaveTimerState.SuspendLayout();
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
            this.lblIssueCount.Location = new System.Drawing.Point(9, 33);
            this.lblIssueCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIssueCount.Name = "lblIssueCount";
            this.lblIssueCount.Size = new System.Drawing.Size(107, 34);
            this.lblIssueCount.TabIndex = 2;
            this.lblIssueCount.Text = "# issueControls displayed (max. 20)";
            // 
            // numIssueCount
            // 
            this.numIssueCount.Location = new System.Drawing.Point(113, 36);
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
            this.cbAlwaysOnTop.Location = new System.Drawing.Point(113, 68);
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
            this.btnOk.Location = new System.Drawing.Point(255, 219);
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
            this.btnCancel.Location = new System.Drawing.Point(316, 219);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 22);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblSaveTimerState
            // 
            this.lblSaveTimerState.AutoSize = true;
            this.lblSaveTimerState.Location = new System.Drawing.Point(9, 100);
            this.lblSaveTimerState.Name = "lblSaveTimerState";
            this.lblSaveTimerState.Size = new System.Drawing.Size(88, 13);
            this.lblSaveTimerState.TabIndex = 5;
            this.lblSaveTimerState.Text = "Save timer states";
            // 
            // rbNoSave
            // 
            this.rbNoSave.AutoSize = true;
            this.rbNoSave.Location = new System.Drawing.Point(6, 8);
            this.rbNoSave.Name = "rbNoSave";
            this.rbNoSave.Size = new System.Drawing.Size(130, 17);
            this.rbNoSave.TabIndex = 0;
            this.rbNoSave.TabStop = true;
            this.rbNoSave.Text = "Reset all timers on exit";
            this.rbNoSave.UseVisualStyleBackColor = true;
            // 
            // gbSaveTimerState
            // 
            this.gbSaveTimerState.Controls.Add(this.rbSaveRunActive);
            this.gbSaveTimerState.Controls.Add(this.rbSavePause);
            this.gbSaveTimerState.Controls.Add(this.rbNoSave);
            this.gbSaveTimerState.Location = new System.Drawing.Point(113, 90);
            this.gbSaveTimerState.Name = "gbSaveTimerState";
            this.gbSaveTimerState.Size = new System.Drawing.Size(234, 75);
            this.gbSaveTimerState.TabIndex = 6;
            this.gbSaveTimerState.TabStop = false;
            // 
            // rbSaveRunActive
            // 
            this.rbSaveRunActive.AutoSize = true;
            this.rbSaveRunActive.Location = new System.Drawing.Point(6, 54);
            this.rbSaveRunActive.Name = "rbSaveRunActive";
            this.rbSaveRunActive.Size = new System.Drawing.Size(222, 17);
            this.rbSaveRunActive.TabIndex = 2;
            this.rbSaveRunActive.TabStop = true;
            this.rbSaveRunActive.Text = "Save current times, active timer continues";
            this.rbSaveRunActive.UseVisualStyleBackColor = true;
            // 
            // rbSavePause
            // 
            this.rbSavePause.AutoSize = true;
            this.rbSavePause.Location = new System.Drawing.Point(6, 31);
            this.rbSavePause.Name = "rbSavePause";
            this.rbSavePause.Size = new System.Drawing.Size(205, 17);
            this.rbSavePause.TabIndex = 1;
            this.rbSavePause.TabStop = true;
            this.rbSavePause.Text = "Save current times, pause active timer";
            this.rbSavePause.UseVisualStyleBackColor = true;
            // 
            // cbTimerEditable
            // 
            this.cbTimerEditable.AutoSize = true;
            this.cbTimerEditable.Location = new System.Drawing.Point(113, 179);
            this.cbTimerEditable.Margin = new System.Windows.Forms.Padding(2);
            this.cbTimerEditable.Name = "cbTimerEditable";
            this.cbTimerEditable.Size = new System.Drawing.Size(130, 17);
            this.cbTimerEditable.TabIndex = 9;
            this.cbTimerEditable.Text = "Enable editing of timer";
            this.cbTimerEditable.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(383, 252);
            this.Controls.Add(this.cbTimerEditable);
            this.Controls.Add(this.gbSaveTimerState);
            this.Controls.Add(this.lblSaveTimerState);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbAlwaysOnTop);
            this.Controls.Add(this.numIssueCount);
            this.Controls.Add(this.lblIssueCount);
            this.Controls.Add(this.tbJiraBaseUrl);
            this.Controls.Add(this.lblJiraBaseUrl);
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.CheckBox cbTimerEditable;
    }
}