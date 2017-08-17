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
using System.Windows.Forms;
namespace StopWatch
{
    partial class WorklogForm
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
            this.lblComment = new System.Windows.Forms.Label();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.rdEstimateAdjustAuto = new System.Windows.Forms.RadioButton();
            this.gbRemainingEstimate = new System.Windows.Forms.GroupBox();
            this.tbReduceBy = new System.Windows.Forms.TextBox();
            this.tbSetTo = new System.Windows.Forms.TextBox();
            this.rdEstimateAdjustManualDecrease = new System.Windows.Forms.RadioButton();
            this.rdEstimateAdjustSetTo = new System.Windows.Forms.RadioButton();
            this.rdEstimateAdjustLeave = new System.Windows.Forms.RadioButton();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.startTimePicker = new System.Windows.Forms.DateTimePicker();
            this.gbRemainingEstimate.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(9, 7);
            this.lblComment.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(206, 13);
            this.lblComment.TabIndex = 0;
            this.lblComment.Text = "Add a Comment to your work log (optional)";
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(11, 24);
            this.tbComment.Margin = new System.Windows.Forms.Padding(2);
            this.tbComment.Multiline = true;
            this.tbComment.Name = "tbComment";
            this.tbComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbComment.Size = new System.Drawing.Size(302, 145);
            this.tbComment.TabIndex = 1;
            this.tbComment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbComment_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(254, 336);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(193, 336);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(56, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Su&bmit";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(11, 316);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(137, 13);
            this.lblInfo.TabIndex = 4;
            this.lblInfo.Text = "Press CTRL-Enter to submit";
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnSave.Location = new System.Drawing.Point(11, 336);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Sa&ve for later";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // rdEstimateAdjustAuto
            // 
            this.rdEstimateAdjustAuto.AutoSize = true;
            this.rdEstimateAdjustAuto.Checked = true;
            this.rdEstimateAdjustAuto.Location = new System.Drawing.Point(8, 15);
            this.rdEstimateAdjustAuto.Name = "rdEstimateAdjustAuto";
            this.rdEstimateAdjustAuto.Size = new System.Drawing.Size(119, 17);
            this.rdEstimateAdjustAuto.TabIndex = 2;
            this.rdEstimateAdjustAuto.TabStop = true;
            this.rdEstimateAdjustAuto.Text = "Adjust &Automatically";
            this.rdEstimateAdjustAuto.UseVisualStyleBackColor = true;
            this.rdEstimateAdjustAuto.CheckedChanged += new System.EventHandler(this.estimateUpdateMethod_changed);
            this.rdEstimateAdjustAuto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdEstimateAdjustAuto_KeyDown);
            // 
            // gbRemainingEstimate
            // 
            this.gbRemainingEstimate.Controls.Add(this.tbReduceBy);
            this.gbRemainingEstimate.Controls.Add(this.tbSetTo);
            this.gbRemainingEstimate.Controls.Add(this.rdEstimateAdjustManualDecrease);
            this.gbRemainingEstimate.Controls.Add(this.rdEstimateAdjustSetTo);
            this.gbRemainingEstimate.Controls.Add(this.rdEstimateAdjustLeave);
            this.gbRemainingEstimate.Controls.Add(this.rdEstimateAdjustAuto);
            this.gbRemainingEstimate.Location = new System.Drawing.Point(14, 208);
            this.gbRemainingEstimate.Name = "gbRemainingEstimate";
            this.gbRemainingEstimate.Size = new System.Drawing.Size(299, 102);
            this.gbRemainingEstimate.TabIndex = 2;
            this.gbRemainingEstimate.TabStop = false;
            this.gbRemainingEstimate.Text = "Remaining Estimate";
            // 
            // tbReduceBy
            // 
            this.tbReduceBy.Enabled = false;
            this.tbReduceBy.Location = new System.Drawing.Point(160, 75);
            this.tbReduceBy.Name = "tbReduceBy";
            this.tbReduceBy.Size = new System.Drawing.Size(133, 20);
            this.tbReduceBy.TabIndex = 7;
            this.tbReduceBy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbReduceBy_KeyDown);
            this.tbReduceBy.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbReduceBy_KeyUp);
            this.tbReduceBy.Validating += new System.ComponentModel.CancelEventHandler(this.tbReduceBy_Validating);
            // 
            // tbSetTo
            // 
            this.tbSetTo.Enabled = false;
            this.tbSetTo.Location = new System.Drawing.Point(160, 55);
            this.tbSetTo.Name = "tbSetTo";
            this.tbSetTo.Size = new System.Drawing.Size(133, 20);
            this.tbSetTo.TabIndex = 6;
            this.tbSetTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSetTo_KeyDown);
            this.tbSetTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSetTo_KeyUp);
            this.tbSetTo.Validating += new System.ComponentModel.CancelEventHandler(this.tbSetTo_Validating);
            // 
            // rdEstimateAdjustManualDecrease
            // 
            this.rdEstimateAdjustManualDecrease.AutoSize = true;
            this.rdEstimateAdjustManualDecrease.Location = new System.Drawing.Point(8, 76);
            this.rdEstimateAdjustManualDecrease.Name = "rdEstimateAdjustManualDecrease";
            this.rdEstimateAdjustManualDecrease.Size = new System.Drawing.Size(78, 17);
            this.rdEstimateAdjustManualDecrease.TabIndex = 5;
            this.rdEstimateAdjustManualDecrease.Text = "&Reduce By";
            this.rdEstimateAdjustManualDecrease.UseVisualStyleBackColor = true;
            this.rdEstimateAdjustManualDecrease.CheckedChanged += new System.EventHandler(this.estimateUpdateMethod_changed);
            this.rdEstimateAdjustManualDecrease.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdEstimateAdjustManualDecrease_KeyDown);
            // 
            // rdEstimateAdjustSetTo
            // 
            this.rdEstimateAdjustSetTo.AutoSize = true;
            this.rdEstimateAdjustSetTo.Location = new System.Drawing.Point(8, 56);
            this.rdEstimateAdjustSetTo.Name = "rdEstimateAdjustSetTo";
            this.rdEstimateAdjustSetTo.Size = new System.Drawing.Size(57, 17);
            this.rdEstimateAdjustSetTo.TabIndex = 4;
            this.rdEstimateAdjustSetTo.Text = "&Set To";
            this.rdEstimateAdjustSetTo.UseVisualStyleBackColor = true;
            this.rdEstimateAdjustSetTo.CheckedChanged += new System.EventHandler(this.estimateUpdateMethod_changed);
            this.rdEstimateAdjustSetTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdEstimateAdjustSetTo_KeyDown);
            // 
            // rdEstimateAdjustLeave
            // 
            this.rdEstimateAdjustLeave.AutoSize = true;
            this.rdEstimateAdjustLeave.Location = new System.Drawing.Point(8, 35);
            this.rdEstimateAdjustLeave.Name = "rdEstimateAdjustLeave";
            this.rdEstimateAdjustLeave.Size = new System.Drawing.Size(114, 17);
            this.rdEstimateAdjustLeave.TabIndex = 3;
            this.rdEstimateAdjustLeave.Text = "&Leave Unchanged";
            this.rdEstimateAdjustLeave.UseVisualStyleBackColor = true;
            this.rdEstimateAdjustLeave.CheckedChanged += new System.EventHandler(this.estimateUpdateMethod_changed);
            this.rdEstimateAdjustLeave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdEstimateAdjustLeave_KeyDown);
            // 
            // startDatePicker
            // 
            this.startDatePicker.AccessibleDescription = "Start Date";
            this.startDatePicker.AccessibleName = "StartDate";
            this.startDatePicker.Location = new System.Drawing.Point(124, 176);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.ShowUpDown = true;
            this.startDatePicker.Size = new System.Drawing.Size(115, 20);
            this.startDatePicker.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Start Time";
            // 
            // startTimePicker
            // 
            this.startTimePicker.AccessibleDescription = "Start Time";
            this.startTimePicker.AccessibleName = "StartTime";
            this.startTimePicker.CustomFormat = "HH:mm";
            this.startTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startTimePicker.Location = new System.Drawing.Point(254, 176);
            this.startTimePicker.Name = "startTimePicker";
            this.startTimePicker.ShowUpDown = true;
            this.startTimePicker.Size = new System.Drawing.Size(59, 20);
            this.startTimePicker.TabIndex = 13;
            // 
            // WorklogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(321, 367);
            this.Controls.Add(this.startTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startDatePicker);
            this.Controls.Add(this.gbRemainingEstimate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.lblComment);
            this.Icon = global::StopWatch.Properties.Resources.stopwatchicon;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "WorklogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Submit worklog";
            this.gbRemainingEstimate.ResumeLayout(false);
            this.gbRemainingEstimate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RadioButton rdEstimateAdjustAuto;
        private System.Windows.Forms.GroupBox gbRemainingEstimate;
        private RadioButton rdEstimateAdjustManualDecrease;
        private RadioButton rdEstimateAdjustSetTo;
        private TextBox tbSetTo;
        private TextBox tbReduceBy;
        public RadioButton rdEstimateAdjustLeave;
        private DateTimePicker startDatePicker;
        private Label label1;
        private DateTimePicker startTimePicker;
    }
}
