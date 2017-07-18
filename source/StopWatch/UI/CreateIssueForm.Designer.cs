using StopWatch.UI;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateIssueForm));
            this.lblChooseProject = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Label();
            this.tbSummary = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblSimilarIssues = new System.Windows.Forms.Label();
            this.lvRelatedIssues = new System.Windows.Forms.ListView();
            this.btnCreateIssue = new System.Windows.Forms.Button();
            this.lblAssignee = new System.Windows.Forms.Label();
            this.btnAssignToMe = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbAddRowAndPressStart = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblIssueType = new System.Windows.Forms.Label();
            this.cbIssueType = new System.Windows.Forms.ComboBox();
            this.actbAssignee = new StopWatch.AutoCompleteTextBox();
            this.tbDescription = new StopWatch.UI.PasteBinaryTextBox();
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
            this.lblChooseProject.TabIndex = 0;
            this.lblChooseProject.Text = "Choose &project";
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblSummary.Location = new System.Drawing.Point(12, 75);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(65, 16);
            this.lblSummary.TabIndex = 5;
            this.lblSummary.Text = "&Summary";
            // 
            // splitter1
            // 
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitter1.Location = new System.Drawing.Point(15, 65);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(498, 2);
            this.splitter1.TabIndex = 4;
            // 
            // tbSummary
            // 
            this.tbSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.tbSummary.Location = new System.Drawing.Point(15, 94);
            this.tbSummary.Name = "tbSummary";
            this.tbSummary.Size = new System.Drawing.Size(498, 27);
            this.tbSummary.TabIndex = 6;
            this.tbSummary.TextChanged += new System.EventHandler(this.tbSummary_TextChanged);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblDescription.Location = new System.Drawing.Point(13, 131);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(76, 16);
            this.lblDescription.TabIndex = 7;
            this.lblDescription.Text = "Description";
            // 
            // lblSimilarIssues
            // 
            this.lblSimilarIssues.AutoSize = true;
            this.lblSimilarIssues.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblSimilarIssues.Location = new System.Drawing.Point(535, 75);
            this.lblSimilarIssues.Name = "lblSimilarIssues";
            this.lblSimilarIssues.Size = new System.Drawing.Size(309, 16);
            this.lblSimilarIssues.TabIndex = 13;
            this.lblSimilarIssues.Text = "&Related issues (double-click to open, check to link)";
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
            this.lvRelatedIssues.TabIndex = 14;
            this.lvRelatedIssues.UseCompatibleStateImageBehavior = false;
            this.lvRelatedIssues.View = System.Windows.Forms.View.List;
            this.lvRelatedIssues.DoubleClick += new System.EventHandler(this.lvRelatedIssues_DoubleClick);
            // 
            // btnCreateIssue
            // 
            this.btnCreateIssue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreateIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCreateIssue.Location = new System.Drawing.Point(296, 446);
            this.btnCreateIssue.Name = "btnCreateIssue";
            this.btnCreateIssue.Size = new System.Drawing.Size(127, 27);
            this.btnCreateIssue.TabIndex = 16;
            this.btnCreateIssue.Text = "&Create issue";
            this.btnCreateIssue.UseVisualStyleBackColor = true;
            this.btnCreateIssue.Click += new System.EventHandler(this.btnCreateIssue_Click);
            // 
            // lblAssignee
            // 
            this.lblAssignee.AutoSize = true;
            this.lblAssignee.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblAssignee.Location = new System.Drawing.Point(13, 340);
            this.lblAssignee.Name = "lblAssignee";
            this.lblAssignee.Size = new System.Drawing.Size(92, 16);
            this.lblAssignee.TabIndex = 9;
            this.lblAssignee.Text = "Assign to &user";
            // 
            // btnAssignToMe
            // 
            this.btnAssignToMe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnAssignToMe.Location = new System.Drawing.Point(305, 359);
            this.btnAssignToMe.Name = "btnAssignToMe";
            this.btnAssignToMe.Size = new System.Drawing.Size(105, 27);
            this.btnAssignToMe.TabIndex = 11;
            this.btnAssignToMe.Text = "Assign to &me";
            this.btnAssignToMe.UseVisualStyleBackColor = true;
            this.btnAssignToMe.Click += new System.EventHandler(this.btnAssignToMe_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnCancel.Location = new System.Drawing.Point(438, 446);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cance&l";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cbAddRowAndPressStart
            // 
            this.cbAddRowAndPressStart.AutoSize = true;
            this.cbAddRowAndPressStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbAddRowAndPressStart.Location = new System.Drawing.Point(16, 402);
            this.cbAddRowAndPressStart.Name = "cbAddRowAndPressStart";
            this.cbAddRowAndPressStart.Size = new System.Drawing.Size(300, 20);
            this.cbAddRowAndPressStart.TabIndex = 12;
            this.cbAddRowAndPressStart.Text = "&Add this issue to StopWatch and start the timer";
            this.cbAddRowAndPressStart.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(15, 432);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(498, 2);
            this.label1.TabIndex = 15;
            // 
            // lblIssueType
            // 
            this.lblIssueType.AutoSize = true;
            this.lblIssueType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblIssueType.Location = new System.Drawing.Point(535, 9);
            this.lblIssueType.Name = "lblIssueType";
            this.lblIssueType.Size = new System.Drawing.Size(69, 16);
            this.lblIssueType.TabIndex = 2;
            this.lblIssueType.Text = "Issue &type";
            // 
            // cbIssueType
            // 
            this.cbIssueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIssueType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbIssueType.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.cbIssueType.FormattingEnabled = true;
            this.cbIssueType.Location = new System.Drawing.Point(538, 28);
            this.cbIssueType.Name = "cbIssueType";
            this.cbIssueType.Size = new System.Drawing.Size(183, 28);
            this.cbIssueType.TabIndex = 3;
            // 
            // actbAssignee
            // 
            this.actbAssignee.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.actbAssignee.Location = new System.Drawing.Point(16, 359);
            this.actbAssignee.Name = "actbAssignee";
            this.actbAssignee.Size = new System.Drawing.Size(283, 27);
            this.actbAssignee.TabIndex = 10;
            this.actbAssignee.WordWrap = false;
            this.actbAssignee.OnAutoComplete += new System.EventHandler<StopWatch.AutoCompleteEventArgs>(this.actbAssignee_OnAutoComplete);
            // 
            // tbDescription
            // 
            this.tbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.tbDescription.Location = new System.Drawing.Point(15, 150);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(498, 174);
            this.tbDescription.TabIndex = 8;
            this.tbDescription.OnPaste += new System.EventHandler<System.EventArgs>(this.tbDescription_OnPaste);
            // 
            // actbSearchProject
            // 
            this.actbSearchProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.actbSearchProject.Location = new System.Drawing.Point(15, 28);
            this.actbSearchProject.Name = "actbSearchProject";
            this.actbSearchProject.Size = new System.Drawing.Size(498, 27);
            this.actbSearchProject.TabIndex = 1;
            this.actbSearchProject.WordWrap = false;
            this.actbSearchProject.OnAutoComplete += new System.EventHandler<StopWatch.AutoCompleteEventArgs>(this.actbSearchProject_OnAutoComplete);
            this.actbSearchProject.SelectedValueChanged += new System.EventHandler(this.actbSearchProject_SelectedValueChanged);
            // 
            // CreateIssueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(856, 484);
            this.Controls.Add(this.cbIssueType);
            this.Controls.Add(this.lblIssueType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbAddRowAndPressStart);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAssignToMe);
            this.Controls.Add(this.lblAssignee);
            this.Controls.Add(this.actbAssignee);
            this.Controls.Add(this.btnCreateIssue);
            this.Controls.Add(this.lvRelatedIssues);
            this.Controls.Add(this.lblSimilarIssues);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.tbSummary);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.lblChooseProject);
            this.Controls.Add(this.actbSearchProject);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateIssueForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create new issue";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AutoCompleteTextBox actbSearchProject;
        private System.Windows.Forms.Label lblChooseProject;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Label splitter1;
        private System.Windows.Forms.TextBox tbSummary;
        private PasteBinaryTextBox tbDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblSimilarIssues;
        private System.Windows.Forms.ListView lvRelatedIssues;
        private System.Windows.Forms.Button btnCreateIssue;
        private AutoCompleteTextBox actbAssignee;
        private System.Windows.Forms.Label lblAssignee;
        private System.Windows.Forms.Button btnAssignToMe;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbAddRowAndPressStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblIssueType;
        private System.Windows.Forms.ComboBox cbIssueType;
    }
}