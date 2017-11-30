namespace ProduceManager
{
    partial class frmBseUserRight
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
            this.btnPreview = new DevExpress.XtraEditors.SimpleButton();
            this.btnColAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnExSel = new DevExpress.XtraEditors.SimpleButton();
            this.btnExAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.Menus_Name = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Menu_Id = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Operator = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.btnDelete);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnPreview);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnColAll);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnExSel);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnExAll);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnOk);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.treeList1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(832, 546);
            this.splitContainerControl1.SplitterPosition = 60;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(563, 8);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(70, 43);
            this.btnPreview.TabIndex = 104;
            this.btnPreview.Text = "已分配预览";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnColAll
            // 
            this.btnColAll.Location = new System.Drawing.Point(110, 8);
            this.btnColAll.Name = "btnColAll";
            this.btnColAll.Size = new System.Drawing.Size(72, 43);
            this.btnColAll.TabIndex = 103;
            this.btnColAll.Text = "收起所有";
            this.btnColAll.Click += new System.EventHandler(this.btnColAll_Click);
            // 
            // btnExSel
            // 
            this.btnExSel.Location = new System.Drawing.Point(188, 8);
            this.btnExSel.Name = "btnExSel";
            this.btnExSel.Size = new System.Drawing.Size(72, 43);
            this.btnExSel.TabIndex = 102;
            this.btnExSel.Text = "展开选中";
            this.btnExSel.Click += new System.EventHandler(this.btnExSel_Click);
            // 
            // btnExAll
            // 
            this.btnExAll.Location = new System.Drawing.Point(32, 8);
            this.btnExAll.Name = "btnExAll";
            this.btnExAll.Size = new System.Drawing.Size(72, 43);
            this.btnExAll.TabIndex = 101;
            this.btnExAll.Text = "展开所有";
            this.btnExAll.Click += new System.EventHandler(this.btnExAll_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(639, 8);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 43);
            this.btnOk.TabIndex = 100;
            this.btnOk.Text = "保存&S";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.Menus_Name,
            this.Menu_Id,
            this.Operator});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsView.AutoWidth = false;
            this.treeList1.OptionsView.ShowCheckBoxes = true;
            this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.treeList1.Size = new System.Drawing.Size(832, 480);
            this.treeList1.TabIndex = 1;
            this.treeList1.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.tlOffice_AfterCheckNode);
            // 
            // Menus_Name
            // 
            this.Menus_Name.Caption = "菜单操作";
            this.Menus_Name.FieldName = "Menus_Name";
            this.Menus_Name.MinWidth = 38;
            this.Menus_Name.Name = "Menus_Name";
            this.Menus_Name.Visible = true;
            this.Menus_Name.VisibleIndex = 0;
            this.Menus_Name.Width = 400;
            // 
            // Menu_Id
            // 
            this.Menu_Id.Caption = "Menu_Id";
            this.Menu_Id.FieldName = "Menu_Id";
            this.Menu_Id.Name = "Menu_Id";
            this.Menu_Id.Width = 91;
            // 
            // Operator
            // 
            this.Operator.Caption = "Operator";
            this.Operator.FieldName = "Operator";
            this.Operator.Name = "Operator";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(705, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(82, 43);
            this.btnDelete.TabIndex = 105;
            this.btnDelete.Text = "删除特殊权限";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmBseUserRight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 546);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmBseUserRight";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "给用户分配权限";
            this.Load += new System.EventHandler(this.frmBseRoleRightEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Menus_Name;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Menu_Id;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Operator;
        private DevExpress.XtraEditors.SimpleButton btnExSel;
        private DevExpress.XtraEditors.SimpleButton btnExAll;
        private DevExpress.XtraEditors.SimpleButton btnColAll;
        private DevExpress.XtraEditors.SimpleButton btnPreview;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
    }
}