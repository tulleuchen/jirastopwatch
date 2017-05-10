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
            this.SuspendLayout();
            // 
            // actbSearchProject
            // 
            this.actbSearchProject.Location = new System.Drawing.Point(12, 12);
            this.actbSearchProject.Name = "actbSearchProject";
            this.actbSearchProject.Size = new System.Drawing.Size(345, 20);
            this.actbSearchProject.TabIndex = 0;
            this.actbSearchProject.Values = null;
            // 
            // CreateIssueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 321);
            this.Controls.Add(this.actbSearchProject);
            this.Name = "CreateIssueForm";
            this.Text = "CreateIssueForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AutoCompleteTextBox actbSearchProject;
    }
}