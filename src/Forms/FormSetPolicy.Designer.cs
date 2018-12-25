namespace ReportingServerManager.Forms
{
    partial class FormSetPolicy
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
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listAvailableRoles = new System.Windows.Forms.ListBox();
            this.listSelectedRoles = new System.Windows.Forms.ListBox();
            this.btnAddToSelectedRoles = new System.Windows.Forms.Button();
            this.btnRemoveFromSelectedRoles = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(11, 58);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(296, 20);
            this.txtUsername.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Domain\\Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(214, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Available Roles:";
            // 
            // listAvailableRoles
            // 
            this.listAvailableRoles.FormattingEnabled = true;
            this.listAvailableRoles.Location = new System.Drawing.Point(162, 113);
            this.listAvailableRoles.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listAvailableRoles.Name = "listAvailableRoles";
            this.listAvailableRoles.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listAvailableRoles.Size = new System.Drawing.Size(145, 108);
            this.listAvailableRoles.TabIndex = 4;
            // 
            // listSelectedRoles
            // 
            this.listSelectedRoles.FormattingEnabled = true;
            this.listSelectedRoles.Location = new System.Drawing.Point(11, 113);
            this.listSelectedRoles.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listSelectedRoles.Name = "listSelectedRoles";
            this.listSelectedRoles.Size = new System.Drawing.Size(145, 108);
            this.listSelectedRoles.TabIndex = 5;
            // 
            // btnAddToSelectedRoles
            // 
            this.btnAddToSelectedRoles.Location = new System.Drawing.Point(162, 225);
            this.btnAddToSelectedRoles.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddToSelectedRoles.Name = "btnAddToSelectedRoles";
            this.btnAddToSelectedRoles.Size = new System.Drawing.Size(56, 19);
            this.btnAddToSelectedRoles.TabIndex = 6;
            this.btnAddToSelectedRoles.Text = "Select";
            this.btnAddToSelectedRoles.UseVisualStyleBackColor = true;
            this.btnAddToSelectedRoles.Click += new System.EventHandler(this.BtnAddToSelectedRolesClick);
            // 
            // btnRemoveFromSelectedRoles
            // 
            this.btnRemoveFromSelectedRoles.Location = new System.Drawing.Point(11, 225);
            this.btnRemoveFromSelectedRoles.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRemoveFromSelectedRoles.Name = "btnRemoveFromSelectedRoles";
            this.btnRemoveFromSelectedRoles.Size = new System.Drawing.Size(56, 19);
            this.btnRemoveFromSelectedRoles.TabIndex = 7;
            this.btnRemoveFromSelectedRoles.Text = "Remove";
            this.btnRemoveFromSelectedRoles.UseVisualStyleBackColor = true;
            this.btnRemoveFromSelectedRoles.Click += new System.EventHandler(this.BtnRemoveFromSelectedRolesClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Selected Roles:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(273, 271);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 24);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(180, 271);
            this.btnApply.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(89, 24);
            this.btnApply.TabIndex = 11;
            this.btnApply.Text = "Apply && Close";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.BtnApplyClick);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.btnRemoveFromSelectedRoles);
            this.groupBox.Controls.Add(this.txtUsername);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.listAvailableRoles);
            this.groupBox.Controls.Add(this.btnAddToSelectedRoles);
            this.groupBox.Controls.Add(this.listSelectedRoles);
            this.groupBox.Location = new System.Drawing.Point(9, 10);
            this.groupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox.Name = "groupBox";
            this.groupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox.Size = new System.Drawing.Size(320, 257);
            this.groupBox.TabIndex = 12;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Set security for ";
            // 
            // FormSetPolicy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 304);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetPolicy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Set Security";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listAvailableRoles;
        private System.Windows.Forms.ListBox listSelectedRoles;
        private System.Windows.Forms.Button btnAddToSelectedRoles;
        private System.Windows.Forms.Button btnRemoveFromSelectedRoles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox groupBox;
    }
}