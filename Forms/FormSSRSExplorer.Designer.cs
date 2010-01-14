namespace RSS_Report_Retrievers
{
    partial class FormSSRSExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSSRSExplorer));
            this.tvReportServer = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDatasourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDatasourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setItemSecurityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPermissionsForUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOK = new System.Windows.Forms.Button();
            this.lvItems = new System.Windows.Forms.ListView();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.contextMenuStrip.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvReportServer
            // 
            this.tvReportServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvReportServer.ImageIndex = 2;
            this.tvReportServer.ImageList = this.imageList;
            this.tvReportServer.Location = new System.Drawing.Point(3, 3);
            this.tvReportServer.Name = "tvReportServer";
            this.tvReportServer.SelectedImageIndex = 3;
            this.tvReportServer.Size = new System.Drawing.Size(252, 405);
            this.tvReportServer.TabIndex = 0;
            this.tvReportServer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvReportServer_AfterSelect);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            this.imageList.Images.SetKeyName(6, "model.gif");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.moveToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.setDatasourceToolStripMenuItem,
            this.editToolStripMenuItem,
            this.createDatasourceToolStripMenuItem,
            this.setItemSecurityToolStripMenuItem,
            this.newFolderToolStripMenuItem,
            this.toolStripMenuItemSep1,
            this.uploadToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.replaceModelToolStripMenuItem,
            this.toolStripMenuItemSep2,
            this.propertiesToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.ShowImageMargin = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(167, 304);
            this.contextMenuStrip.Text = "With selected items:";
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // moveToolStripMenuItem
            // 
            this.moveToolStripMenuItem.Name = "moveToolStripMenuItem";
            this.moveToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.moveToolStripMenuItem.Text = "&Move";
            this.moveToolStripMenuItem.Click += new System.EventHandler(this.moveToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.renameToolStripMenuItem.Text = "&Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // setDatasourceToolStripMenuItem
            // 
            this.setDatasourceToolStripMenuItem.Name = "setDatasourceToolStripMenuItem";
            this.setDatasourceToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.setDatasourceToolStripMenuItem.Text = "S&et Datasource";
            this.setDatasourceToolStripMenuItem.Click += new System.EventHandler(this.setDatasourceToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.editToolStripMenuItem.Text = "&Edit Datasource";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // createDatasourceToolStripMenuItem
            // 
            this.createDatasourceToolStripMenuItem.Name = "createDatasourceToolStripMenuItem";
            this.createDatasourceToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.createDatasourceToolStripMenuItem.Text = "&Create Datasource";
            this.createDatasourceToolStripMenuItem.Click += new System.EventHandler(this.createDatasourceToolStripMenuItem_Click);
            // 
            // setItemSecurityToolStripMenuItem
            // 
            this.setItemSecurityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPermissionsForUserToolStripMenuItem});
            this.setItemSecurityToolStripMenuItem.Name = "setItemSecurityToolStripMenuItem";
            this.setItemSecurityToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.setItemSecurityToolStripMenuItem.Text = "Security";
            // 
            // addPermissionsForUserToolStripMenuItem
            // 
            this.addPermissionsForUserToolStripMenuItem.Name = "addPermissionsForUserToolStripMenuItem";
            this.addPermissionsForUserToolStripMenuItem.Size = new System.Drawing.Size(332, 24);
            this.addPermissionsForUserToolStripMenuItem.Text = "Add permissions for \"My Reports\"-folders";
            this.addPermissionsForUserToolStripMenuItem.Click += new System.EventHandler(this.addPermissionsForUserToolStripMenuItem_Click);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.newFolderToolStripMenuItem.Text = "&New Folder";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSep1
            // 
            this.toolStripMenuItemSep1.Name = "toolStripMenuItemSep1";
            this.toolStripMenuItemSep1.Size = new System.Drawing.Size(163, 6);
            // 
            // uploadToolStripMenuItem
            // 
            this.uploadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem,
            this.folderToolStripMenuItem});
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            this.uploadToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.uploadToolStripMenuItem.Text = "&Upload";
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(116, 24);
            this.filesToolStripMenuItem.Text = "File(s)";
            this.filesToolStripMenuItem.Click += new System.EventHandler(this.filesToolStripMenuItem_Click);
            // 
            // folderToolStripMenuItem
            // 
            this.folderToolStripMenuItem.Name = "folderToolStripMenuItem";
            this.folderToolStripMenuItem.Size = new System.Drawing.Size(116, 24);
            this.folderToolStripMenuItem.Text = "Folder";
            this.folderToolStripMenuItem.Click += new System.EventHandler(this.folderToolStripMenuItem_Click);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.downloadToolStripMenuItem.Text = "Do&wnload";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // replaceModelToolStripMenuItem
            // 
            this.replaceModelToolStripMenuItem.Name = "replaceModelToolStripMenuItem";
            this.replaceModelToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.replaceModelToolStripMenuItem.Text = "Replace model";
            this.replaceModelToolStripMenuItem.Click += new System.EventHandler(this.replaceModelToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSep2
            // 
            this.toolStripMenuItemSep2.Name = "toolStripMenuItemSep2";
            this.toolStripMenuItemSep2.Size = new System.Drawing.Size(163, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(166, 24);
            this.propertiesToolStripMenuItem.Text = "&Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(694, 428);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lvItems
            // 
            this.lvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvItems.ContextMenuStrip = this.contextMenuStrip;
            this.lvItems.Location = new System.Drawing.Point(3, 3);
            this.lvItems.Name = "lvItems";
            this.lvItems.ShowItemToolTips = true;
            this.lvItems.Size = new System.Drawing.Size(514, 405);
            this.lvItems.SmallImageList = this.imageList;
            this.lvItems.TabIndex = 2;
            this.lvItems.UseCompatibleStateImageBehavior = false;
            this.lvItems.View = System.Windows.Forms.View.List;
            this.lvItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvItems_MouseDoubleClick);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.tvReportServer);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.lvItems);
            this.splitContainer.Size = new System.Drawing.Size(781, 410);
            this.splitContainer.SplitterDistance = 257;
            this.splitContainer.TabIndex = 3;
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(781, 24);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.AutoToolTip = true;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(99, 19);
            this.toolStripStatusLabel.Text = "Not connected";
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "rdl";
            this.openFileDialog.Filter = "Reports|*.rdl";
            this.openFileDialog.InitialDirectory = "C:\\";
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.ShowReadOnly = true;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.helpToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(781, 27);
            this.menuStripMain.TabIndex = 4;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem6,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(89, 23);
            this.toolStripMenuItem5.Text = "&Application";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.connectToolStripMenuItem.Text = "&Connect";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.settingsToolStripMenuItem.Text = "&Servers...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 24);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.BottomToolStripPanel
            // 
            this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(781, 410);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.Size = new System.Drawing.Size(781, 461);
            this.toolStripContainer.TabIndex = 4;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStripMain);
            // 
            // FormSSRSExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 461);
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.btnOK);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormSSRSExplorer";
            this.Text = "SSRS Explorer ";
            this.Shown += new System.EventHandler(this.frmReportSelector_Shown);
            this.contextMenuStrip.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvReportServer;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem moveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSep1;
        private System.Windows.Forms.ToolStripMenuItem uploadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem folderToolStripMenuItem;
        private System.Windows.Forms.ListView lvItems;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDatasourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSep2;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDatasourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setItemSecurityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPermissionsForUserToolStripMenuItem;
    }
}