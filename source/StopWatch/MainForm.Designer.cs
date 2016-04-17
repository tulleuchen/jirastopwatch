namespace StopWatch
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pbLogin = new System.Windows.Forms.PictureBox();
            this.pbSettings = new System.Windows.Forms.PictureBox();
            this.lblConnectionHeader = new System.Windows.Forms.Label();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.cbFilters = new System.Windows.Forms.ComboBox();
            this.lblActiveFilter = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.tbTotalTime = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLogin
            // 
            this.pbLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbLogin.Image = global::StopWatch.Properties.Resources.login22;
            this.pbLogin.Location = new System.Drawing.Point(9, 92);
            this.pbLogin.Margin = new System.Windows.Forms.Padding(2);
            this.pbLogin.Name = "pbLogin";
            this.pbLogin.Size = new System.Drawing.Size(22, 22);
            this.pbLogin.TabIndex = 1;
            this.pbLogin.TabStop = false;
            this.pbLogin.Click += new System.EventHandler(this.pbLogin_Click);
            // 
            // pbSettings
            // 
            this.pbSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSettings.Image = global::StopWatch.Properties.Resources.settings22;
            this.pbSettings.Location = new System.Drawing.Point(442, 92);
            this.pbSettings.Margin = new System.Windows.Forms.Padding(2);
            this.pbSettings.Name = "pbSettings";
            this.pbSettings.Size = new System.Drawing.Size(22, 22);
            this.pbSettings.TabIndex = 0;
            this.pbSettings.TabStop = false;
            this.pbSettings.Click += new System.EventHandler(this.pbSettings_Click);
            // 
            // lblConnectionHeader
            // 
            this.lblConnectionHeader.AutoSize = true;
            this.lblConnectionHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblConnectionHeader.Location = new System.Drawing.Point(28, 98);
            this.lblConnectionHeader.Name = "lblConnectionHeader";
            this.lblConnectionHeader.Size = new System.Drawing.Size(30, 15);
            this.lblConnectionHeader.TabIndex = 2;
            this.lblConnectionHeader.Text = "Jira:";
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblConnectionStatus.Location = new System.Drawing.Point(53, 98);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(13, 15);
            this.lblConnectionStatus.TabIndex = 3;
            this.lblConnectionStatus.Text = "x";
            // 
            // cbFilters
            // 
            this.cbFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbFilters.FormattingEnabled = true;
            this.cbFilters.Location = new System.Drawing.Point(164, 92);
            this.cbFilters.Margin = new System.Windows.Forms.Padding(2);
            this.cbFilters.Name = "cbFilters";
            this.cbFilters.Size = new System.Drawing.Size(151, 24);
            this.cbFilters.TabIndex = 4;
            this.cbFilters.DropDown += new System.EventHandler(this.cbFilters_DropDown);
            this.cbFilters.SelectedIndexChanged += new System.EventHandler(this.cbFilters_SelectedIndexChanged);
            // 
            // lblActiveFilter
            // 
            this.lblActiveFilter.AutoSize = true;
            this.lblActiveFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblActiveFilter.Location = new System.Drawing.Point(104, 94);
            this.lblActiveFilter.Name = "lblActiveFilter";
            this.lblActiveFilter.Size = new System.Drawing.Size(39, 17);
            this.lblActiveFilter.TabIndex = 5;
            this.lblActiveFilter.Text = "Filter";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "JIRA StopWatch";
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblTotalTime.Location = new System.Drawing.Point(320, 93);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(40, 17);
            this.lblTotalTime.TabIndex = 6;
            this.lblTotalTime.Text = "Total";
            // 
            // tbTotalTime
            // 
            this.tbTotalTime.BackColor = System.Drawing.SystemColors.Window;
            this.tbTotalTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTotalTime.Location = new System.Drawing.Point(366, 91);
            this.tbTotalTime.Name = "tbTotalTime";
            this.tbTotalTime.ReadOnly = true;
            this.tbTotalTime.Size = new System.Drawing.Size(64, 22);
            this.tbTotalTime.TabIndex = 8;
            this.tbTotalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 119);
            this.Controls.Add(this.tbTotalTime);
            this.Controls.Add(this.lblTotalTime);
            this.Controls.Add(this.lblActiveFilter);
            this.Controls.Add(this.cbFilters);
            this.Controls.Add(this.lblConnectionStatus);
            this.Controls.Add(this.lblConnectionHeader);
            this.Controls.Add(this.pbLogin);
            this.Controls.Add(this.pbSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "JIRA StopWatch";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSettings;
        private System.Windows.Forms.PictureBox pbLogin;
        private System.Windows.Forms.Label lblConnectionHeader;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.ComboBox cbFilters;
        private System.Windows.Forms.Label lblActiveFilter;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.TextBox tbTotalTime;
    }
}

