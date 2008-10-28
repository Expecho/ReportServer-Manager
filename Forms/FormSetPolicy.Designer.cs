namespace RSS_Report_Retrievers
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
            this.txtUsername.Location = new System.Drawing.Point(15, 72);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(143, 22);
            this.txtUsername.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Domain\\Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(285, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Available Roles:";
            // 
            // listAvailableRoles
            // 
            this.listAvailableRoles.FormattingEnabled = true;
            this.listAvailableRoles.ItemHeight = 16;
            this.listAvailableRoles.Location = new System.Drawing.Point(288, 140);
            this.listAvailableRoles.Name = "listAvailableRoles";
            this.listAvailableRoles.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listAvailableRoles.Size = new System.Drawing.Size(120, 132);
            this.listAvailableRoles.TabIndex = 4;
            // 
            // listSelectedRoles
            // 
            this.listSelectedRoles.FormattingEnabled = true;
            this.listSelectedRoles.ItemHeight = 16;
            this.listSelectedRoles.Location = new System.Drawing.Point(15, 140);
            this.listSelectedRoles.Name = "listSelectedRoles";
            this.listSelectedRoles.Size = new System.Drawing.Size(120, 132);
            this.listSelectedRoles.TabIndex = 5;
            // 
            // btnAddToSelectedRoles
            // 
            this.btnAddToSelectedRoles.Location = new System.Drawing.Point(288, 291);
            this.btnAddToSelectedRoles.Name = "btnAddToSelectedRoles";
            this.btnAddToSelectedRoles.Size = new System.Drawing.Size(75, 23);
            this.btnAddToSelectedRoles.TabIndex = 6;
            this.btnAddToSelectedRoles.Text = "Select";
            this.btnAddToSelectedRoles.UseVisualStyleBackColor = true;
            this.btnAddToSelectedRoles.Click += new System.EventHandler(this.btnAddToSelectedRoles_Click);
            // 
            // btnRemoveFromSelectedRoles
            // 
            this.btnRemoveFromSelectedRoles.Location = new System.Drawing.Point(15, 291);
            this.btnRemoveFromSelectedRoles.Name = "btnRemoveFromSelectedRoles";
            this.btnRemoveFromSelectedRoles.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveFromSelectedRoles.TabIndex = 7;
            this.btnRemoveFromSelectedRoles.Text = "Remove";
            this.btnRemoveFromSelectedRoles.UseVisualStyleBackColor = true;
            this.btnRemoveFromSelectedRoles.Click += new System.EventHandler(this.btnRemoveFromSelectedRoles_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Selected Roles:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(364, 397);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 29);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(207, 397);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(119, 29);
            this.btnApply.TabIndex = 11;
            this.btnApply.Text = "ApplyAndClose";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
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
            this.groupBox.Location = new System.Drawing.Point(12, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(427, 332);
            this.groupBox.TabIndex = 12;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Setting security for ";
            // 
            // FormSetPolicy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 468);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Name = "FormSetPolicy";
            this.Text = "FormSetPolicy";
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