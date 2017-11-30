namespace ProduceManager
{
    partial class frmBseRoleRightEdit
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.Menus_Name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Menu_Id = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gcMenu = new DevExpress.XtraEditors.GroupControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.ckbRight = new DevExpress.XtraEditors.CheckEdit();
            this.chkRightList = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.ckbAll = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMenu)).BeginInit();
            this.gcMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckbRight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRightList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeList1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gcMenu);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(782, 546);
            this.splitContainerControl1.SplitterPosition = 236;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.Menus_Name,
            this.Menu_Id});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.treeList1.Size = new System.Drawing.Size(236, 546);
            this.treeList1.TabIndex = 0;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            // 
            // Menus_Name
            // 
            this.Menus_Name.Caption = "菜单名称";
            this.Menus_Name.FieldName = "Menus_Name";
            this.Menus_Name.MinWidth = 38;
            this.Menus_Name.Name = "Menus_Name";
            this.Menus_Name.Visible = true;
            this.Menus_Name.VisibleIndex = 0;
            this.Menus_Name.Width = 168;
            // 
            // Menu_Id
            // 
            this.Menu_Id.Caption = "Menu_Id";
            this.Menu_Id.FieldName = "Menu_Id";
            this.Menu_Id.Name = "Menu_Id";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // gcMenu
            // 
            this.gcMenu.Controls.Add(this.ckbAll);
            this.gcMenu.Controls.Add(this.btnSave);
            this.gcMenu.Controls.Add(this.ckbRight);
            this.gcMenu.Controls.Add(this.chkRightList);
            this.gcMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMenu.Location = new System.Drawing.Point(0, 0);
            this.gcMenu.Name = "gcMenu";
            this.gcMenu.Size = new System.Drawing.Size(540, 546);
            this.gcMenu.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(461, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 20);
            this.btnSave.TabIndex = 107;
            this.btnSave.Text = "确定&S";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ckbRight
            // 
            this.ckbRight.Location = new System.Drawing.Point(378, 1);
            this.ckbRight.Name = "ckbRight";
            this.ckbRight.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.ckbRight.Properties.Appearance.Options.UseForeColor = true;
            this.ckbRight.Properties.AutoWidth = true;
            this.ckbRight.Properties.Caption = "允许访问&V";
            this.ckbRight.Size = new System.Drawing.Size(79, 19);
            this.ckbRight.TabIndex = 106;
            // 
            // chkRightList
            // 
            this.chkRightList.CheckOnClick = true;
            this.chkRightList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkRightList.Location = new System.Drawing.Point(2, 21);
            this.chkRightList.Name = "chkRightList";
            this.chkRightList.Size = new System.Drawing.Size(536, 523);
            this.chkRightList.TabIndex = 105;
            // 
            // ckbAll
            // 
            this.ckbAll.Location = new System.Drawing.Point(323, 1);
            this.ckbAll.Name = "ckbAll";
            this.ckbAll.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.ckbAll.Properties.Appearance.Options.UseForeColor = true;
            this.ckbAll.Properties.AutoWidth = true;
            this.ckbAll.Properties.Caption = "全选&A";
            this.ckbAll.Size = new System.Drawing.Size(55, 19);
            this.ckbAll.TabIndex = 108;
            this.ckbAll.CheckedChanged += new System.EventHandler(this.ckbAll_CheckedChanged);
            // 
            // frmBseRoleRightEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 546);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmBseRoleRightEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "给角色分配权限";
            this.Load += new System.EventHandler(this.frmBseRoleRightEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMenu)).EndInit();
            this.gcMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckbRight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRightList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbAll.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Menus_Name;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Menu_Id;
        private DevExpress.XtraEditors.GroupControl gcMenu;
        private DevExpress.XtraEditors.CheckedListBoxControl chkRightList;
        private DevExpress.XtraEditors.CheckEdit ckbRight;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.CheckEdit ckbAll;
    }
}