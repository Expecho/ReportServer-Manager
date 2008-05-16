namespace RSS_Report_Retrievers
{
    partial class FormSettings
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblReportLibrary = new System.Windows.Forms.Label();
            this.lblDomain = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblURL = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtReportLibrary = new System.Windows.Forms.TextBox();
            this.chkSharePointMode = new System.Windows.Forms.CheckBox();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkWindowsAuthentication = new System.Windows.Forms.CheckBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblReportLibrary);
            this.groupBox1.Controls.Add(this.txtReportLibrary);
            this.groupBox1.Controls.Add(this.chkSharePointMode);
            this.groupBox1.Controls.Add(this.lblDomain);
            this.groupBox1.Controls.Add(this.txtDomain);
            this.groupBox1.Controls.Add(this.lblUsername);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.chkWindowsAuthentication);
            this.groupBox1.Controls.Add(this.lblURL);
            this.groupBox1.Controls.Add(this.txtURL);
            this.groupBox1.Location = new System.Drawing.Point(9, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(545, 217);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblReportLibrary
            // 
            this.lblReportLibrary.AutoSize = true;
            this.lblReportLibrary.Location = new System.Drawing.Point(227, 73);
            this.lblReportLibrary.Name = "lblReportLibrary";
            this.lblReportLibrary.Size = new System.Drawing.Size(73, 13);
            this.lblReportLibrary.TabIndex = 31;
            this.lblReportLibrary.Text = "Report Library";
            // 
            // lblDomain
            // 
            this.lblDomain.AutoSize = true;
            this.lblDomain.Location = new System.Drawing.Point(227, 119);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(43, 13);
            this.lblDomain.TabIndex = 28;
            this.lblDomain.Text = "Domain";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(226, 146);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 27;
            this.lblUsername.Text = "Username";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(226, 172);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 26;
            this.lblPassword.Text = "Password";
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(15, 22);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(184, 13);
            this.lblURL.TabIndex = 25;
            this.lblURL.Text = "URL to  Reporting Server webservice";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(479, 228);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "OK";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtReportLibrary
            // 
            this.txtReportLibrary.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RSS_Report_Retrievers.Properties.Settings.Default, "ReportLibrary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtReportLibrary.Enabled = false;
            this.txtReportLibrary.Location = new System.Drawing.Point(306, 70);
            this.txtReportLibrary.Name = "txtReportLibrary";
            this.txtReportLibrary.Size = new System.Drawing.Size(221, 20);
            this.txtReportLibrary.TabIndex = 30;
            this.txtReportLibrary.Text = global::RSS_Report_Retrievers.Properties.Settings.Default.ReportLibrary;
            // 
            // chkSharePointMode
            // 
            this.chkSharePointMode.AutoSize = true;
            this.chkSharePointMode.Checked = global::RSS_Report_Retrievers.Properties.Settings.Default.SharePointMode;
            this.chkSharePointMode.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::RSS_Report_Retrievers.Properties.Settings.Default, "SharePointMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkSharePointMode.Location = new System.Drawing.Point(205, 47);
            this.chkSharePointMode.Name = "chkSharePointMode";
            this.chkSharePointMode.Size = new System.Drawing.Size(305, 17);
            this.chkSharePointMode.TabIndex = 29;
            this.chkSharePointMode.Text = "This instance is configured for SharePoint Integration Mode";
            this.chkSharePointMode.UseVisualStyleBackColor = true;
            this.chkSharePointMode.CheckedChanged += new System.EventHandler(this.chkSharePointMode_CheckedChanged);
            // 
            // txtDomain
            // 
            this.txtDomain.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RSS_Report_Retrievers.Properties.Settings.Default, "Domain", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDomain.Enabled = false;
            this.txtDomain.Location = new System.Drawing.Point(285, 116);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(121, 20);
            this.txtDomain.TabIndex = 5;
            this.txtDomain.Text = global::RSS_Report_Retrievers.Properties.Settings.Default.Domain;
            // 
            // txtUsername
            // 
            this.txtUsername.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RSS_Report_Retrievers.Properties.Settings.Default, "Username", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(285, 143);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(121, 20);
            this.txtUsername.TabIndex = 6;
            this.txtUsername.Text = global::RSS_Report_Retrievers.Properties.Settings.Default.Username;
            // 
            // txtPassword
            // 
            this.txtPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RSS_Report_Retrievers.Properties.Settings.Default, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(285, 169);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(121, 20);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.Text = global::RSS_Report_Retrievers.Properties.Settings.Default.Password;
            // 
            // chkWindowsAuthentication
            // 
            this.chkWindowsAuthentication.AutoSize = true;
            this.chkWindowsAuthentication.Checked = global::RSS_Report_Retrievers.Properties.Settings.Default.UseWindowsAuthentication;
            this.chkWindowsAuthentication.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWindowsAuthentication.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::RSS_Report_Retrievers.Properties.Settings.Default, "UseWindowsAuthentication", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkWindowsAuthentication.Location = new System.Drawing.Point(203, 98);
            this.chkWindowsAuthentication.Name = "chkWindowsAuthentication";
            this.chkWindowsAuthentication.Size = new System.Drawing.Size(159, 17);
            this.chkWindowsAuthentication.TabIndex = 21;
            this.chkWindowsAuthentication.Text = "Use windows authentication";
            this.chkWindowsAuthentication.UseVisualStyleBackColor = true;
            this.chkWindowsAuthentication.CheckedChanged += new System.EventHandler(this.chkWindowsAuthentication_CheckedChanged);
            // 
            // txtURL
            // 
            this.txtURL.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RSS_Report_Retrievers.Properties.Settings.Default, "RSS_Report_Retrievers_RSS_ReportingService", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtURL.Location = new System.Drawing.Point(205, 19);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(322, 20);
            this.txtURL.TabIndex = 4;
            this.txtURL.Text = global::RSS_Report_Retrievers.Properties.Settings.Default.RSS_Report_Retrievers_RSS_ReportingService;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(398, 228);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 263);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblDomain;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkWindowsAuthentication;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkSharePointMode;
        private System.Windows.Forms.Label lblReportLibrary;
        private System.Windows.Forms.TextBox txtReportLibrary;
        private System.Windows.Forms.Button btnCancel;
    }
}