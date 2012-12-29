using ReportingServerManager.Logic.Configuration;

namespace ReportingServerManager.Forms
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbServerVersion = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.bsServerSettings = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsServerSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbServerVersion);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtAlias);
            this.groupBox1.Controls.Add(this.label1);
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
            this.groupBox1.Location = new System.Drawing.Point(9, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 348);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmbServerVersion
            // 
            this.cmbServerVersion.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.bsServerSettings, "SQLServerVersion", true));
            this.cmbServerVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServerVersion.FormattingEnabled = true;
            this.cmbServerVersion.Items.AddRange(new object[] {
            "2000",
            "2005",
            "2008",
            "2008R2",
            "2012"});
            this.cmbServerVersion.Location = new System.Drawing.Point(138, 57);
            this.cmbServerVersion.Name = "cmbServerVersion";
            this.cmbServerVersion.Size = new System.Drawing.Size(121, 21);
            this.cmbServerVersion.TabIndex = 35;
            this.cmbServerVersion.SelectedIndexChanged += new System.EventHandler(this.CmbServerVersionSelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "SQL Server Version";
            // 
            // txtAlias
            // 
            this.txtAlias.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServerSettings, "Alias", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtAlias.Location = new System.Drawing.Point(17, 28);
            this.txtAlias.Margin = new System.Windows.Forms.Padding(2);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(330, 20);
            this.txtAlias.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Alias";
            // 
            // lblReportLibrary
            // 
            this.lblReportLibrary.AutoSize = true;
            this.lblReportLibrary.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportLibrary.Location = new System.Drawing.Point(40, 172);
            this.lblReportLibrary.Name = "lblReportLibrary";
            this.lblReportLibrary.Size = new System.Drawing.Size(87, 13);
            this.lblReportLibrary.TabIndex = 31;
            this.lblReportLibrary.Text = "Report Library";
            // 
            // txtReportLibrary
            // 
            this.txtReportLibrary.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServerSettings, "ReportLibrary", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtReportLibrary.Enabled = false;
            this.txtReportLibrary.Location = new System.Drawing.Point(138, 169);
            this.txtReportLibrary.Name = "txtReportLibrary";
            this.txtReportLibrary.Size = new System.Drawing.Size(209, 20);
            this.txtReportLibrary.TabIndex = 30;
            // 
            // chkSharePointMode
            // 
            this.chkSharePointMode.AutoSize = true;
            this.chkSharePointMode.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsServerSettings, "IsSharePointMode", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkSharePointMode.Location = new System.Drawing.Point(17, 142);
            this.chkSharePointMode.Name = "chkSharePointMode";
            this.chkSharePointMode.Size = new System.Drawing.Size(305, 17);
            this.chkSharePointMode.TabIndex = 29;
            this.chkSharePointMode.Text = "This instance is configured for SharePoint Integration Mode";
            this.chkSharePointMode.UseVisualStyleBackColor = true;
            this.chkSharePointMode.CheckedChanged += new System.EventHandler(this.ChkSharePointModeCheckedChanged);
            // 
            // lblDomain
            // 
            this.lblDomain.AutoSize = true;
            this.lblDomain.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDomain.Location = new System.Drawing.Point(41, 228);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(49, 13);
            this.lblDomain.TabIndex = 28;
            this.lblDomain.Text = "Domain";
            // 
            // txtDomain
            // 
            this.txtDomain.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServerSettings, "WindowsDomain", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtDomain.Enabled = false;
            this.txtDomain.Location = new System.Drawing.Point(138, 224);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(121, 20);
            this.txtDomain.TabIndex = 5;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(40, 255);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(63, 13);
            this.lblUsername.TabIndex = 27;
            this.lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServerSettings, "WindowsUsername", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(138, 250);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(121, 20);
            this.txtUsername.TabIndex = 6;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(40, 281);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(61, 13);
            this.lblPassword.TabIndex = 26;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServerSettings, "WindowsPwd", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(138, 276);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(121, 20);
            this.txtPassword.TabIndex = 7;
            // 
            // chkWindowsAuthentication
            // 
            this.chkWindowsAuthentication.AutoSize = true;
            this.chkWindowsAuthentication.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsServerSettings, "UseWindowsAuth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkWindowsAuthentication.Location = new System.Drawing.Point(17, 207);
            this.chkWindowsAuthentication.Name = "chkWindowsAuthentication";
            this.chkWindowsAuthentication.Size = new System.Drawing.Size(159, 17);
            this.chkWindowsAuthentication.TabIndex = 21;
            this.chkWindowsAuthentication.Text = "Use windows authentication";
            this.chkWindowsAuthentication.UseVisualStyleBackColor = true;
            this.chkWindowsAuthentication.CheckedChanged += new System.EventHandler(this.ChkWindowsAuthenticationCheckedChanged);
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblURL.Location = new System.Drawing.Point(15, 92);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(219, 13);
            this.lblURL.TabIndex = 25;
            this.lblURL.Text = "URL to  Reporting Server webservice";
            // 
            // txtURL
            // 
            this.txtURL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsServerSettings, "Url", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtURL.Location = new System.Drawing.Point(17, 107);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(330, 20);
            this.txtURL.TabIndex = 4;
            this.txtURL.Text = "http://localhost/ReportServer/ReportService2005.asmx";
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(208, 357);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(289, 357);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // bsServerSettings
            // 
            this.bsServerSettings.AllowNew = false;
            this.bsServerSettings.DataSource = typeof(ReportingServerManager.Logic.Configuration.ServerSettingsConfigElement);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 388);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FrmSettingsLoad);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsServerSettings)).EndInit();
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
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkSharePointMode;
        private System.Windows.Forms.Label lblReportLibrary;
        private System.Windows.Forms.TextBox txtReportLibrary;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.BindingSource bsServerSettings;
        private System.Windows.Forms.TextBox txtAlias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbServerVersion;
        private System.Windows.Forms.Label label2;
    }
}