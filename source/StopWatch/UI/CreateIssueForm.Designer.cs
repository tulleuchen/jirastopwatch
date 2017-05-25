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
            this.lblChooseProject = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Label();
            this.tbSummary = new System.Windows.Forms.TextBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblSimilarIssues = new System.Windows.Forms.Label();
            this.lvRelatedIssues = new System.Windows.Forms.ListView();
            this.actbSearchProject = new StopWatch.AutoCompleteTextBox();
            this.SuspendLayout();
            // 
            // lblChooseProject
            // 
            this.lblChooseProject.AutoSize = true;
            this.lblChooseProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblChooseProject.Location = new System.Drawing.Point(12, 9);
            this.lblChooseProject.Name = "lblChooseProject";
            this.lblChooseProject.Size = new System.Drawing.Size(99, 16);
            this.lblChooseProject.TabIndex = 1;
            this.lblChooseProject.Text = "Choose project";
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblSummary.Location = new System.Drawing.Point(12, 75);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(65, 16);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.Text = "Summary";
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter1.Location = new System.Drawing.Point(15, 65);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(498, 2);
            this.splitter1.TabIndex = 9;
            // 
            // tbSummary
            // 
            this.tbSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.tbSummary.Location = new System.Drawing.Point(15, 94);
            this.tbSummary.Name = "tbSummary";
            this.tbSummary.Size = new System.Drawing.Size(498, 27);
            this.tbSummary.TabIndex = 10;
            this.tbSummary.TextChanged += new System.EventHandler(this.tbSummary_TextChanged);
            // 
            // tbDescription
            // 
            this.tbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.tbDescription.Location = new System.Drawing.Point(15, 150);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(498, 174);
            this.tbDescription.TabIndex = 12;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblDescription.Location = new System.Drawing.Point(13, 131);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(76, 16);
            this.lblDescription.TabIndex = 11;
            this.lblDescription.Text = "Description";
            // 
            // lblSimilarIssues
            // 
            this.lblSimilarIssues.AutoSize = true;
            this.lblSimilarIssues.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblSimilarIssues.Location = new System.Drawing.Point(535, 75);
            this.lblSimilarIssues.Name = "lblSimilarIssues";
            this.lblSimilarIssues.Size = new System.Drawing.Size(309, 16);
            this.lblSimilarIssues.TabIndex = 14;
            this.lblSimilarIssues.Text = "Related issues (double-click to open, check to link)";
            // 
            // lvRelatedIssues
            // 
            this.lvRelatedIssues.CheckBoxes = true;
            this.lvRelatedIssues.FullRowSelect = true;
            this.lvRelatedIssues.GridLines = true;
            this.lvRelatedIssues.Location = new System.Drawing.Point(538, 94);
            this.lvRelatedIssues.MultiSelect = false;
            this.lvRelatedIssues.Name = "lvRelatedIssues";
            this.lvRelatedIssues.ShowItemToolTips = true;
            this.lvRelatedIssues.Size = new System.Drawing.Size(306, 230);
            this.lvRelatedIssues.TabIndex = 15;
            this.lvRelatedIssues.UseCompatibleStateImageBehavior = false;
            this.lvRelatedIssues.View = System.Windows.Forms.View.List;
            this.lvRelatedIssues.DoubleClick += new System.EventHandler(this.lvRelatedIssues_DoubleClick);
            // 
            // actbSearchProject
            // 
            this.actbSearchProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.actbSearchProject.Location = new System.Drawing.Point(15, 28);
            this.actbSearchProject.Name = "actbSearchProject";
            this.actbSearchProject.Size = new System.Drawing.Size(498, 27);
            this.actbSearchProject.TabIndex = 0;
            this.actbSearchProject.Values = null;
            this.actbSearchProject.WordWrap = false;
            // 
            // CreateIssueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 511);
            this.Controls.Add(this.lvRelatedIssues);
            this.Controls.Add(this.lblSimilarIssues);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.tbSummary);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.lblChooseProject);
            this.Controls.Add(this.actbSearchProject);
            this.Name = "CreateIssueForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CreateIssueForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AutoCompleteTextBox actbSearchProject;
        private System.Windows.Forms.Label lblChooseProject;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Label splitter1;
        private System.Windows.Forms.TextBox tbSummary;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblSimilarIssues;
        private System.Windows.Forms.ListView lvRelatedIssues;
    }
}