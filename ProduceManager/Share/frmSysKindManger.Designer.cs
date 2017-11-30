namespace ProduceManager
{
    partial class frmSysKindManger
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
            DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSysKindManger));
            this.tcState = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcEdit = new System.Windows.Forms.GroupBox();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.btnAddSub = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.gcInfo = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            this.gcEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcState
            // 
            this.tcState.Caption = "State";
            this.tcState.FieldName = "State";
            this.tcState.Name = "tcState";
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tcState});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.DarkSalmon;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Column = this.tcState;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = false;
            this.treeList1.FormatConditions.AddRange(new DevExpress.XtraTreeList.StyleFormatConditions.StyleFormatCondition[] {
            styleFormatCondition1});
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsView.AutoWidth = false;
            this.treeList1.Size = new System.Drawing.Size(316, 705);
            this.treeList1.TabIndex = 0;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.treeList1);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.gcEdit);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(962, 705);
            this.splitContainerControl2.SplitterPosition = 316;
            this.splitContainerControl2.TabIndex = 2;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // gcEdit
            // 
            this.gcEdit.Controls.Add(this.btnAdd);
            this.gcEdit.Controls.Add(this.btnAddSub);
            this.gcEdit.Controls.Add(this.btnCancel);
            this.gcEdit.Controls.Add(this.btnEdit);
            this.gcEdit.Controls.Add(this.btnSave);
            this.gcEdit.Controls.Add(this.gcInfo);
            this.gcEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcEdit.Location = new System.Drawing.Point(0, 0);
            this.gcEdit.Name = "gcEdit";
            this.gcEdit.Size = new System.Drawing.Size(640, 705);
            this.gcEdit.TabIndex = 108;
            this.gcEdit.TabStop = false;
            this.gcEdit.Text = "回车下移；-上移；新增“Alt+Z”";
            // 
            // btnAdd
            // 
            this.btnAdd.ImageIndex = 21;
            this.btnAdd.ImageList = this.imageList2;
            this.btnAdd.Location = new System.Drawing.Point(174, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 25);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "新增同级&Z";
            this.btnAdd.Click += new System.EventHandler(this.btn_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "打开报表.png");
            this.imageList2.Images.SetKeyName(1, "加工拆单.png");
            this.imageList2.Images.SetKeyName(2, "导到维护.png");
            this.imageList2.Images.SetKeyName(3, "撤销完成.png");
            this.imageList2.Images.SetKeyName(4, "修改.png");
            this.imageList2.Images.SetKeyName(5, "启用.png");
            this.imageList2.Images.SetKeyName(6, "详细信息.png");
            this.imageList2.Images.SetKeyName(7, "反审.png");
            this.imageList2.Images.SetKeyName(8, "批量修改.png");
            this.imageList2.Images.SetKeyName(9, "批量新增.png");
            this.imageList2.Images.SetKeyName(10, "打印.png");
            this.imageList2.Images.SetKeyName(11, "退回.png");
            this.imageList2.Images.SetKeyName(12, "刷新.png");
            this.imageList2.Images.SetKeyName(13, "导出Excel.png");
            this.imageList2.Images.SetKeyName(14, "Excel导入.png");
            this.imageList2.Images.SetKeyName(15, "审核.png");
            this.imageList2.Images.SetKeyName(16, "删除.png");
            this.imageList2.Images.SetKeyName(17, "确认.png");
            this.imageList2.Images.SetKeyName(18, "作废.png");
            this.imageList2.Images.SetKeyName(19, "完成.png");
            this.imageList2.Images.SetKeyName(20, "撤销.png");
            this.imageList2.Images.SetKeyName(21, "新增.png");
            this.imageList2.Images.SetKeyName(22, "删除样式.png");
            this.imageList2.Images.SetKeyName(23, "保存样式.png");
            this.imageList2.Images.SetKeyName(24, "保存.png");
            // 
            // btnAddSub
            // 
            this.btnAddSub.ImageIndex = 21;
            this.btnAddSub.ImageList = this.imageList2;
            this.btnAddSub.Location = new System.Drawing.Point(259, 0);
            this.btnAddSub.Name = "btnAddSub";
            this.btnAddSub.Size = new System.Drawing.Size(85, 25);
            this.btnAddSub.TabIndex = 64;
            this.btnAddSub.Text = "新增下级&X";
            this.btnAddSub.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageIndex = 20;
            this.btnCancel.ImageList = this.imageList2;
            this.btnCancel.Location = new System.Drawing.Point(468, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "撤销&B";
            this.btnCancel.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.ImageIndex = 4;
            this.btnEdit.ImageList = this.imageList2;
            this.btnEdit.Location = new System.Drawing.Point(344, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(62, 25);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "修改&C";
            this.btnEdit.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 24;
            this.btnSave.ImageList = this.imageList2;
            this.btnSave.Location = new System.Drawing.Point(406, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存&V";
            this.btnSave.Click += new System.EventHandler(this.btn_Click);
            // 
            // gcInfo
            // 
            this.gcInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcInfo.Location = new System.Drawing.Point(3, 18);
            this.gcInfo.Name = "gcInfo";
            this.gcInfo.Size = new System.Drawing.Size(634, 684);
            this.gcInfo.TabIndex = 63;
            this.gcInfo.TabStop = false;
            // 
            // frmSysKindManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 705);
            this.Controls.Add(this.splitContainerControl2);
            this.Name = "frmSysKindManger";
            this.Text = "款式类型维护";
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            this.gcEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private System.Windows.Forms.GroupBox gcEdit;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.GroupBox gcInfo;
        private DevExpress.XtraEditors.SimpleButton btnAddSub;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tcState;
        private System.Windows.Forms.ImageList imageList2;
    }
}