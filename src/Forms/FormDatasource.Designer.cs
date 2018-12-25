namespace ReportingServerManager.Forms
{
    partial class FormDatasource
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
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblConnStr = new System.Windows.Forms.Label();
            this.txtConnStr = new System.Windows.Forms.TextBox();
            this.cmbExtensions = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.radWindowsAuthentication = new System.Windows.Forms.RadioButton();
            this.radPrompt = new System.Windows.Forms.RadioButton();
            this.radStored = new System.Windows.Forms.RadioButton();
            this.radNone = new System.Windows.Forms.RadioButton();
            this.chkUsePromptedCredentialsAsWindowsCredentials = new System.Windows.Forms.CheckBox();
            this.txtCredentialsPrompt = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkExecutionContext = new System.Windows.Forms.CheckBox();
            this.chkUseStoredCredentialsAsWindowsCredentials = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(15, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(341, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblConnStr
            // 
            this.lblConnStr.AutoSize = true;
            this.lblConnStr.Location = new System.Drawing.Point(9, 99);
            this.lblConnStr.Name = "lblConnStr";
            this.lblConnStr.Size = new System.Drawing.Size(89, 13);
            this.lblConnStr.TabIndex = 2;
            this.lblConnStr.Text = "Connection string";
            // 
            // txtConnStr
            // 
            this.txtConnStr.Location = new System.Drawing.Point(12, 115);
            this.txtConnStr.Multiline = true;
            this.txtConnStr.Name = "txtConnStr";
            this.txtConnStr.Size = new System.Drawing.Size(344, 82);
            this.txtConnStr.TabIndex = 3;
            this.txtConnStr.Text = "Initial Catalog=<Database>;Data Source=<Server\\Instance>";
            // 
            // cmbExtensions
            // 
            this.cmbExtensions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExtensions.FormattingEnabled = true;
            this.cmbExtensions.Location = new System.Drawing.Point(15, 69);
            this.cmbExtensions.Name = "cmbExtensions";
            this.cmbExtensions.Size = new System.Drawing.Size(341, 21);
            this.cmbExtensions.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Data Source";
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Location = new System.Drawing.Point(12, 480);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkEnabled.TabIndex = 6;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(200, 507);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(281, 507);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // radWindowsAuthentication
            // 
            this.radWindowsAuthentication.AutoSize = true;
            this.radWindowsAuthentication.Checked = true;
            this.radWindowsAuthentication.Location = new System.Drawing.Point(12, 203);
            this.radWindowsAuthentication.Name = "radWindowsAuthentication";
            this.radWindowsAuthentication.Size = new System.Drawing.Size(139, 17);
            this.radWindowsAuthentication.TabIndex = 10;
            this.radWindowsAuthentication.TabStop = true;
            this.radWindowsAuthentication.Text = "Windows authentication";
            this.radWindowsAuthentication.UseVisualStyleBackColor = true;
            this.radWindowsAuthentication.CheckedChanged += new System.EventHandler(this.RadAuthenticationCheckedChanged);
            // 
            // radPrompt
            // 
            this.radPrompt.AutoSize = true;
            this.radPrompt.Location = new System.Drawing.Point(12, 226);
            this.radPrompt.Name = "radPrompt";
            this.radPrompt.Size = new System.Drawing.Size(127, 17);
            this.radPrompt.TabIndex = 11;
            this.radPrompt.Text = "Prompt for credentials";
            this.radPrompt.UseVisualStyleBackColor = true;
            this.radPrompt.CheckedChanged += new System.EventHandler(this.RadAuthenticationCheckedChanged);
            // 
            // radStored
            // 
            this.radStored.AutoSize = true;
            this.radStored.Location = new System.Drawing.Point(12, 299);
            this.radStored.Name = "radStored";
            this.radStored.Size = new System.Drawing.Size(110, 17);
            this.radStored.TabIndex = 12;
            this.radStored.Text = "Stored credentials";
            this.radStored.UseVisualStyleBackColor = true;
            this.radStored.CheckedChanged += new System.EventHandler(this.RadAuthenticationCheckedChanged);
            // 
            // radNone
            // 
            this.radNone.AutoSize = true;
            this.radNone.Location = new System.Drawing.Point(12, 445);
            this.radNone.Name = "radNone";
            this.radNone.Size = new System.Drawing.Size(93, 17);
            this.radNone.TabIndex = 13;
            this.radNone.Text = "No credentials";
            this.radNone.UseVisualStyleBackColor = true;
            this.radNone.CheckedChanged += new System.EventHandler(this.RadAuthenticationCheckedChanged);
            // 
            // chkUsePromptedCredentialsAsWindowsCredentials
            // 
            this.chkUsePromptedCredentialsAsWindowsCredentials.AutoSize = true;
            this.chkUsePromptedCredentialsAsWindowsCredentials.Enabled = false;
            this.chkUsePromptedCredentialsAsWindowsCredentials.Location = new System.Drawing.Point(33, 249);
            this.chkUsePromptedCredentialsAsWindowsCredentials.Name = "chkUsePromptedCredentialsAsWindowsCredentials";
            this.chkUsePromptedCredentialsAsWindowsCredentials.Size = new System.Drawing.Size(157, 17);
            this.chkUsePromptedCredentialsAsWindowsCredentials.TabIndex = 14;
            this.chkUsePromptedCredentialsAsWindowsCredentials.Text = "Use as windows credentials";
            this.chkUsePromptedCredentialsAsWindowsCredentials.UseVisualStyleBackColor = true;
            // 
            // txtCredentialsPrompt
            // 
            this.txtCredentialsPrompt.Enabled = false;
            this.txtCredentialsPrompt.Location = new System.Drawing.Point(33, 273);
            this.txtCredentialsPrompt.Name = "txtCredentialsPrompt";
            this.txtCredentialsPrompt.Size = new System.Drawing.Size(323, 20);
            this.txtCredentialsPrompt.TabIndex = 15;
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(33, 334);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(323, 20);
            this.txtUsername.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Username";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(33, 376);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(323, 20);
            this.txtPassword.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 360);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Password";
            // 
            // chkExecutionContext
            // 
            this.chkExecutionContext.AutoSize = true;
            this.chkExecutionContext.Enabled = false;
            this.chkExecutionContext.Location = new System.Drawing.Point(33, 422);
            this.chkExecutionContext.Name = "chkExecutionContext";
            this.chkExecutionContext.Size = new System.Drawing.Size(202, 17);
            this.chkExecutionContext.TabIndex = 21;
            this.chkExecutionContext.Text = "Set execution context to this account";
            this.chkExecutionContext.UseVisualStyleBackColor = true;
            // 
            // chkUseStoredCredentialsAsWindowsCredentials
            // 
            this.chkUseStoredCredentialsAsWindowsCredentials.AutoSize = true;
            this.chkUseStoredCredentialsAsWindowsCredentials.Enabled = false;
            this.chkUseStoredCredentialsAsWindowsCredentials.Location = new System.Drawing.Point(33, 399);
            this.chkUseStoredCredentialsAsWindowsCredentials.Name = "chkUseStoredCredentialsAsWindowsCredentials";
            this.chkUseStoredCredentialsAsWindowsCredentials.Size = new System.Drawing.Size(157, 17);
            this.chkUseStoredCredentialsAsWindowsCredentials.TabIndex = 20;
            this.chkUseStoredCredentialsAsWindowsCredentials.Text = "Use as windows credentials";
            this.chkUseStoredCredentialsAsWindowsCredentials.UseVisualStyleBackColor = true;
            // 
            // FormDatasource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 542);
            this.Controls.Add(this.chkExecutionContext);
            this.Controls.Add(this.chkUseStoredCredentialsAsWindowsCredentials);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCredentialsPrompt);
            this.Controls.Add(this.chkUsePromptedCredentialsAsWindowsCredentials);
            this.Controls.Add(this.radNone);
            this.Controls.Add(this.radStored);
            this.Controls.Add(this.radPrompt);
            this.Controls.Add(this.radWindowsAuthentication);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbExtensions);
            this.Controls.Add(this.txtConnStr);
            this.Controls.Add(this.lblConnStr);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDatasource";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create / Edit Datasource";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblConnStr;
        private System.Windows.Forms.TextBox txtConnStr;
        private System.Windows.Forms.ComboBox cmbExtensions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton radWindowsAuthentication;
        private System.Windows.Forms.RadioButton radPrompt;
        private System.Windows.Forms.RadioButton radStored;
        private System.Windows.Forms.RadioButton radNone;
        private System.Windows.Forms.CheckBox chkUsePromptedCredentialsAsWindowsCredentials;
        private System.Windows.Forms.TextBox txtCredentialsPrompt;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkExecutionContext;
        private System.Windows.Forms.CheckBox chkUseStoredCredentialsAsWindowsCredentials;

    }
}