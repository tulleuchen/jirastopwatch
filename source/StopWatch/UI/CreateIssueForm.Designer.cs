namespace StopWatch
{
    partial class CreateIssueForm
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
            this.actbSearchProject = new StopWatch.AutoCompleteTextBox();
            this.lblChooseProject = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // actbSearchProject
            // 
            this.actbSearchProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actbSearchProject.Location = new System.Drawing.Point(15, 25);
            this.actbSearchProject.Name = "actbSearchProject";
            this.actbSearchProject.Size = new System.Drawing.Size(370, 27);
            this.actbSearchProject.TabIndex = 0;
            this.actbSearchProject.Values = null;
            this.actbSearchProject.WordWrap = false;
            // 
            // lblChooseProject
            // 
            this.lblChooseProject.AutoSize = true;
            this.lblChooseProject.Location = new System.Drawing.Point(12, 9);
            this.lblChooseProject.Name = "lblChooseProject";
            this.lblChooseProject.Size = new System.Drawing.Size(78, 13);
            this.lblChooseProject.TabIndex = 1;
            this.lblChooseProject.Text = "Choose project";
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Location = new System.Drawing.Point(12, 72);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(50, 13);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.Text = "Summary";
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter1.Location = new System.Drawing.Point(15, 62);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(370, 2);
            this.splitter1.TabIndex = 9;
            // 
            // CreateIssueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 321);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.lblChooseProject);
            this.Controls.Add(this.actbSearchProject);
            this.Name = "CreateIssueForm";
            this.Text = "CreateIssueForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AutoCompleteTextBox actbSearchProject;
        private System.Windows.Forms.Label lblChooseProject;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Label splitter1;
    }
}