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
            this.txtReportLibrary = new System.Windows.Forms.TextBox();
            this.chkSharePointMode = new System.Windows.Forms.CheckBox();
            this.lblDomain = new System.Windows.Forms.Label();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkWindowsAuthentication = new System.Windows.Forms.CheckBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.serverSettingDTODataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverSettingDTODataGridView)).BeginInit();
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
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(727, 267);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lblReportLibrary
            // 
            this.lblReportLibrary.AutoSize = true;
            this.lblReportLibrary.Location = new System.Drawing.Point(303, 90);
            this.lblReportLibrary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReportLibrary.Name = "lblReportLibrary";
            this.lblReportLibrary.Size = new System.Drawing.Size(99, 17);
            this.lblReportLibrary.TabIndex = 31;
            this.lblReportLibrary.Text = "Report Library";
            // 
            // txtReportLibrary
            // 
            this.txtReportLibrary.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RSS_Report_Retrievers.Properties.Settings.Default, "ReportLibrary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtReportLibrary.Enabled = false;
            this.txtReportLibrary.Location = new System.Drawing.Point(408, 86);
            this.txtReportLibrary.Margin = new System.Windows.Forms.Padding(4);
            this.txtReportLibrary.Name = "txtReportLibrary";
            this.txtReportLibrary.Size = new System.Drawing.Size(293, 22);
            this.txtReportLibrary.TabIndex = 30;
            this.txtReportLibrary.Text = global::RSS_Report_Retrievers.Properties.Settings.Default.ReportLibrary;
            // 
            // chkSharePointMode
            // 
            this.chkSharePointMode.AutoSize = true;
            this.chkSharePointMode.Checked = global::RSS_Report_Retrievers.Properties.Settings.Default.SharePointMode;
            this.chkSharePointMode.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::RSS_Report_Retrievers.Properties.Settings.Default, "SharePointMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkSharePointMode.Location = new System.Drawing.Point(273, 58);
            this.chkSharePointMode.Margin = new System.Windows.Forms.Padding(4);
            this.chkSharePointMode.Name = "chkSharePointMode";
            this.chkSharePointMode.Size = new System.Drawing.Size(404, 21);
            this.chkSharePointMode.TabIndex = 29;
            this.chkSharePointMode.Text = "This instance is configured for SharePoint Integration Mode";
            this.chkSharePointMode.UseVisualStyleBackColor = true;
            this.chkSharePointMode.CheckedChanged += new System.EventHandler(this.chkSharePointMode_CheckedChanged);
            // 
            // lblDomain
            // 
            this.lblDomain.AutoSize = true;
            this.lblDomain.Location = new System.Drawing.Point(303, 146);
            this.lblDomain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(56, 17);
            this.lblDomain.TabIndex = 28;
            this.lblDomain.Text = "Domain";
            // 
            // txtDomain
            // 
            this.txtDomain.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RSS_Report_Retrievers.Properties.Settings.Default, "Domain", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDomain.Enabled = false;
            this.txtDomain.Location = new System.Drawing.Point(380, 143);
            this.txtDomain.Margin = new System.Windows.Forms.Padding(4);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(160, 22);
            this.txtDomain.TabIndex = 5;
            this.txtDomain.Text = global::RSS_Report_Retrievers.Properties.Settings.Default.Domain;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(301, 180);
            this.lblUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(73, 17);
            this.lblUsername.TabIndex = 27;
            this.lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RSS_Report_Retrievers.Properties.Settings.Default, "Username", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(380, 176);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(160, 22);
            this.txtUsername.TabIndex = 6;
            this.txtUsername.Text = global::RSS_Report_Retrievers.Properties.Settings.Default.Username;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(301, 212);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(69, 17);
            this.lblPassword.TabIndex = 26;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RSS_Report_Retrievers.Properties.Settings.Default, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(380, 208);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(160, 22);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.Text = global::RSS_Report_Retrievers.Properties.Settings.Default.Password;
            // 
            // chkWindowsAuthentication
            // 
            this.chkWindowsAuthentication.AutoSize = true;
            this.chkWindowsAuthentication.Checked = global::RSS_Report_Retrievers.Properties.Settings.Default.UseWindowsAuthentication;
            this.chkWindowsAuthentication.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWindowsAuthentication.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::RSS_Report_Retrievers.Properties.Settings.Default, "UseWindowsAuthentication", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkWindowsAuthentication.Location = new System.Drawing.Point(271, 121);
            this.chkWindowsAuthentication.Margin = new System.Windows.Forms.Padding(4);
            this.chkWindowsAuthentication.Name = "chkWindowsAuthentication";
            this.chkWindowsAuthentication.Size = new System.Drawing.Size(204, 21);
            this.chkWindowsAuthentication.TabIndex = 21;
            this.chkWindowsAuthentication.Text = "Use windows authentication";
            this.chkWindowsAuthentication.UseVisualStyleBackColor = true;
            this.chkWindowsAuthentication.CheckedChanged += new System.EventHandler(this.chkWindowsAuthentication_CheckedChanged);
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(20, 27);
            this.lblURL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(242, 17);
            this.lblURL.TabIndex = 25;
            this.lblURL.Text = "URL to  Reporting Server webservice";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(273, 23);
            this.txtURL.Margin = new System.Windows.Forms.Padding(4);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(428, 22);
            this.txtURL.TabIndex = 4;
            this.txtURL.Text = "http://localhost/ReportServer/ReportService.asmx";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(639, 281);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "OK";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(531, 281);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // serverSettingDTODataGridView
            // 
            this.serverSettingDTODataGridView.Location = new System.Drawing.Point(239, 301);
            this.serverSettingDTODataGridView.Name = "serverSettingDTODataGridView";
            this.serverSettingDTODataGridView.RowTemplate.Height = 24;
            this.serverSettingDTODataGridView.Size = new System.Drawing.Size(300, 220);
            this.serverSettingDTODataGridView.TabIndex = 4;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 604);
            this.Controls.Add(this.serverSettingDTODataGridView);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serverSettingDTODataGridView)).EndInit();
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
        private System.Windows.Forms.DataGridView serverSettingDTODataGridView;
    }
}