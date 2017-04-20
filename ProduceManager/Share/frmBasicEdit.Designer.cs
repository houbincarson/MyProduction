namespace ProduceManager
{
    partial class frmBasicEdit
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBasicEdit));
            this.gcState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcNav = new System.Windows.Forms.GroupBox();
            this.gridCMain = new DevExpress.XtraGrid.GridControl();
            this.gridVMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnReLoad = new DevExpress.XtraEditors.SimpleButton();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.xtabItemInfo = new DevExpress.XtraTab.XtraTabControl();
            this.xtabInfo = new DevExpress.XtraTab.XtraTabPage();
            this.gcOrdParent = new System.Windows.Forms.GroupBox();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.gcImgs = new DevExpress.XtraEditors.GroupControl();
            this.picEdit = new DevExpress.XtraEditors.PictureEdit();
            this.grAddImg = new DevExpress.XtraEditors.GroupControl();
            this.ckbDel = new DevExpress.XtraEditors.CheckEdit();
            this.btnFile = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.gcOrd = new System.Windows.Forms.GroupBox();
            this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.gcNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtabItemInfo)).BeginInit();
            this.xtabItemInfo.SuspendLayout();
            this.xtabInfo.SuspendLayout();
            this.gcOrdParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcImgs)).BeginInit();
            this.gcImgs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grAddImg)).BeginInit();
            this.grAddImg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckbDel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFile.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcState
            // 
            this.gcState.Caption = "gridColumn1";
            this.gcState.FieldName = "State";
            this.gcState.Name = "gcState";
            // 
            // gcNav
            // 
            this.gcNav.Controls.Add(this.gridCMain);
            this.gcNav.Controls.Add(this.btnReLoad);
            this.gcNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNav.Location = new System.Drawing.Point(0, 0);
            this.gcNav.Name = "gcNav";
            this.gcNav.Size = new System.Drawing.Size(324, 705);
            this.gcNav.TabIndex = 65;
            this.gcNav.TabStop = false;
            this.gcNav.Text = "PgUp上移；PgDn下移；Tab键切换";
            // 
            // gridCMain
            // 
            this.gridCMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCMain.Location = new System.Drawing.Point(3, 18);
            this.gridCMain.MainView = this.gridVMain;
            this.gridCMain.Name = "gridCMain";
            this.gridCMain.Size = new System.Drawing.Size(318, 684);
            this.gridCMain.TabIndex = 58;
            this.gridCMain.UseEmbeddedNavigator = true;
            this.gridCMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVMain});
            // 
            // gridVMain
            // 
            this.gridVMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcState});
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.DarkSalmon;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Column = this.gcState;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = false;
            this.gridVMain.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gridVMain.GridControl = this.gridCMain;
            this.gridVMain.Name = "gridVMain";
            this.gridVMain.OptionsSelection.MultiSelect = true;
            this.gridVMain.OptionsView.ColumnAutoWidth = false;
            this.gridVMain.OptionsView.ShowAutoFilterRow = true;
            this.gridVMain.OptionsView.ShowFooter = true;
            this.gridVMain.OptionsView.ShowGroupPanel = false;
            this.gridVMain.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridVMain_FocusedRowChanged);
            // 
            // btnReLoad
            // 
            this.btnReLoad.ImageIndex = 3;
            this.btnReLoad.ImageList = this.imageList2;
            this.btnReLoad.Location = new System.Drawing.Point(198, -1);
            this.btnReLoad.Name = "btnReLoad";
            this.btnReLoad.Size = new System.Drawing.Size(80, 20);
            this.btnReLoad.TabIndex = 165;
            this.btnReLoad.Text = "重新加载";
            this.btnReLoad.Click += new System.EventHandler(this.btn_Click);
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
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gcNav);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtabItemInfo);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(962, 705);
            this.splitContainerControl1.SplitterPosition = 324;
            this.splitContainerControl1.TabIndex = 66;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // xtabItemInfo
            // 
            this.xtabItemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtabItemInfo.Location = new System.Drawing.Point(0, 0);
            this.xtabItemInfo.Name = "xtabItemInfo";
            this.xtabItemInfo.SelectedTabPage = this.xtabInfo;
            this.xtabItemInfo.Size = new System.Drawing.Size(632, 705);
            this.xtabItemInfo.TabIndex = 609;
            this.xtabItemInfo.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtabInfo});
            // 
            // xtabInfo
            // 
            this.xtabInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.xtabInfo.Controls.Add(this.gcOrdParent);
            this.xtabInfo.Name = "xtabInfo";
            this.xtabInfo.Size = new System.Drawing.Size(623, 673);
            this.xtabInfo.Text = "款式抬头";
            // 
            // gcOrdParent
            // 
            this.gcOrdParent.Controls.Add(this.btnCopy);
            this.gcOrdParent.Controls.Add(this.btnAdd);
            this.gcOrdParent.Controls.Add(this.gcImgs);
            this.gcOrdParent.Controls.Add(this.btnDelete);
            this.gcOrdParent.Controls.Add(this.btnCancel);
            this.gcOrdParent.Controls.Add(this.btnEdit);
            this.gcOrdParent.Controls.Add(this.btnSave);
            this.gcOrdParent.Controls.Add(this.gcOrd);
            this.gcOrdParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcOrdParent.Location = new System.Drawing.Point(0, 0);
            this.gcOrdParent.Name = "gcOrdParent";
            this.gcOrdParent.Size = new System.Drawing.Size(623, 673);
            this.gcOrdParent.TabIndex = 605;
            this.gcOrdParent.TabStop = false;
            this.gcOrdParent.Text = "回车下移；-上移；新增“Alt+X”";
            // 
            // btnAdd
            // 
            this.btnAdd.ImageIndex = 21;
            this.btnAdd.ImageList = this.imageList2;
            this.btnAdd.Location = new System.Drawing.Point(242, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(62, 25);
            this.btnAdd.TabIndex = 163;
            this.btnAdd.Text = "新增&X";
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btn_Click);
            // 
            // gcImgs
            // 
            this.gcImgs.AppearanceCaption.ForeColor = System.Drawing.Color.Blue;
            this.gcImgs.AppearanceCaption.Options.UseForeColor = true;
            this.gcImgs.Controls.Add(this.picEdit);
            this.gcImgs.Controls.Add(this.grAddImg);
            this.gcImgs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcImgs.Location = new System.Drawing.Point(3, 127);
            this.gcImgs.Name = "gcImgs";
            this.gcImgs.ShowCaption = false;
            this.gcImgs.Size = new System.Drawing.Size(617, 543);
            this.gcImgs.TabIndex = 623;
            this.gcImgs.Text = "图片集";
            // 
            // picEdit
            // 
            this.picEdit.Location = new System.Drawing.Point(0, 43);
            this.picEdit.Name = "picEdit";
            this.picEdit.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
            this.picEdit.Properties.ReadOnly = true;
            this.picEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.picEdit.Size = new System.Drawing.Size(450, 350);
            this.picEdit.TabIndex = 12;
            // 
            // grAddImg
            // 
            this.grAddImg.Controls.Add(this.ckbDel);
            this.grAddImg.Controls.Add(this.btnFile);
            this.grAddImg.Controls.Add(this.labelControl1);
            this.grAddImg.Dock = System.Windows.Forms.DockStyle.Top;
            this.grAddImg.Location = new System.Drawing.Point(2, 2);
            this.grAddImg.Name = "grAddImg";
            this.grAddImg.ShowCaption = false;
            this.grAddImg.Size = new System.Drawing.Size(613, 40);
            this.grAddImg.TabIndex = 13;
            this.grAddImg.Text = "groupControl2";
            // 
            // ckbDel
            // 
            this.ckbDel.Location = new System.Drawing.Point(448, 10);
            this.ckbDel.Name = "ckbDel";
            this.ckbDel.Properties.AutoWidth = true;
            this.ckbDel.Properties.Caption = "上传后删除本地图片";
            this.ckbDel.Size = new System.Drawing.Size(131, 19);
            this.ckbDel.TabIndex = 161;
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(58, 9);
            this.btnFile.Name = "btnFile";
            this.btnFile.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnFile.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.btnFile.Properties.Click += new System.EventHandler(this.btnFile_Properties_Click);
            this.btnFile.Size = new System.Drawing.Size(390, 21);
            this.btnFile.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "图片路径";
            // 
            // btnDelete
            // 
            this.btnDelete.ImageIndex = 18;
            this.btnDelete.ImageList = this.imageList2;
            this.btnDelete.Location = new System.Drawing.Point(490, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(62, 25);
            this.btnDelete.TabIndex = 164;
            this.btnDelete.Text = "弃用&D";
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageIndex = 20;
            this.btnCancel.ImageList = this.imageList2;
            this.btnCancel.Location = new System.Drawing.Point(428, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 25);
            this.btnCancel.TabIndex = 162;
            this.btnCancel.Text = "撤销&B";
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.ImageIndex = 4;
            this.btnEdit.ImageList = this.imageList2;
            this.btnEdit.Location = new System.Drawing.Point(304, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(62, 25);
            this.btnEdit.TabIndex = 161;
            this.btnEdit.Text = "修改&C";
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 24;
            this.btnSave.ImageList = this.imageList2;
            this.btnSave.Location = new System.Drawing.Point(366, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 25);
            this.btnSave.TabIndex = 159;
            this.btnSave.Text = "保存&V";
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btn_Click);
            // 
            // gcOrd
            // 
            this.gcOrd.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcOrd.Location = new System.Drawing.Point(3, 18);
            this.gcOrd.Name = "gcOrd";
            this.gcOrd.Size = new System.Drawing.Size(617, 109);
            this.gcOrd.TabIndex = 63;
            this.gcOrd.TabStop = false;
            // 
            // btnCopy
            // 
            this.btnCopy.ImageIndex = 21;
            this.btnCopy.ImageList = this.imageList2;
            this.btnCopy.Location = new System.Drawing.Point(180, 0);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(62, 25);
            this.btnCopy.TabIndex = 624;
            this.btnCopy.Text = "复制&P";
            this.btnCopy.Visible = false;
            this.btnCopy.Click += new System.EventHandler(this.btn_Click);
            // 
            // frmBasicEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 705);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmBasicEdit";
            this.Text = "系统基础维护";
            this.Load += new System.EventHandler(this.frmBasicEdit_Load);
            this.gcNav.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtabItemInfo)).EndInit();
            this.xtabItemInfo.ResumeLayout(false);
            this.xtabInfo.ResumeLayout(false);
            this.gcOrdParent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcImgs)).EndInit();
            this.gcImgs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grAddImg)).EndInit();
            this.grAddImg.ResumeLayout(false);
            this.grAddImg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckbDel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFile.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gcNav;
        private DevExpress.XtraGrid.GridControl gridCMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVMain;
        private DevExpress.XtraGrid.Columns.GridColumn gcState;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTab.XtraTabControl xtabItemInfo;
        private DevExpress.XtraTab.XtraTabPage xtabInfo;
        private System.Windows.Forms.GroupBox gcOrdParent;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.GroupBox gcOrd;
        private DevExpress.XtraEditors.GroupControl gcImgs;
        private DevExpress.XtraEditors.PictureEdit picEdit;
        private DevExpress.XtraEditors.GroupControl grAddImg;
        private DevExpress.XtraEditors.CheckEdit ckbDel;
        private DevExpress.XtraEditors.ButtonEdit btnFile;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ImageList imageList2;
        private DevExpress.XtraEditors.SimpleButton btnReLoad;
        private DevExpress.XtraEditors.SimpleButton btnCopy;
    }
}