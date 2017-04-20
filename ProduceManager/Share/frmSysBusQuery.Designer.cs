namespace ProduceManager
{
    partial class frmSysBusQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSysBusQuery));
            this.gcQuery = new System.Windows.Forms.GroupBox();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnQuery = new DevExpress.XtraBars.BarButtonItem();
            this.btnMore = new DevExpress.XtraBars.BarButtonItem();
            this.btnReLoad = new DevExpress.XtraBars.BarButtonItem();
            this.btnExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnSaveLayOut = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteLayOut = new DevExpress.XtraBars.BarButtonItem();
            this.btnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnImport = new DevExpress.XtraBars.BarButtonItem();
            this.btnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContrMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridCMain = new DevExpress.XtraGrid.GridControl();
            this.gridVMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtabItemInfo = new DevExpress.XtraTab.XtraTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContrMain)).BeginInit();
            this.splitContrMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtabItemInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // gcQuery
            // 
            this.gcQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcQuery.Location = new System.Drawing.Point(0, 26);
            this.gcQuery.Name = "gcQuery";
            this.gcQuery.Size = new System.Drawing.Size(962, 55);
            this.gcQuery.TabIndex = 604;
            this.gcQuery.TabStop = false;
            this.gcQuery.Text = "查询条件";
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnQuery,
            this.btnMore,
            this.btnEdit,
            this.btnImport,
            this.btnAdd,
            this.btnSaveLayOut,
            this.btnDeleteLayOut,
            this.btnExcel,
            this.btnReLoad});
            this.barManager1.MaxItemId = 9;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnQuery),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMore),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnReLoad, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExcel, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveLayOut, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDeleteLayOut),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEdit, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnImport),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAdd, true)});
            this.bar2.OptionsBar.AllowCollapse = true;
            this.bar2.OptionsBar.AllowDelete = true;
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.AllowRename = true;
            this.bar2.OptionsBar.DisableClose = true;
            this.bar2.OptionsBar.DisableCustomization = true;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnQuery
            // 
            this.btnQuery.Caption = "查询&F";
            this.btnQuery.Id = 0;
            this.btnQuery.ImageIndex = 12;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnMore
            // 
            this.btnMore.Caption = "显示明细&M";
            this.btnMore.Id = 1;
            this.btnMore.ImageIndex = 6;
            this.btnMore.Name = "btnMore";
            this.btnMore.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnMore.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnReLoad
            // 
            this.btnReLoad.Caption = "重新加载&R";
            this.btnReLoad.Id = 8;
            this.btnReLoad.ImageIndex = 3;
            this.btnReLoad.Name = "btnReLoad";
            this.btnReLoad.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnReLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnExcel
            // 
            this.btnExcel.Caption = "导出Excel&X";
            this.btnExcel.Id = 7;
            this.btnExcel.ImageIndex = 13;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnSaveLayOut
            // 
            this.btnSaveLayOut.Caption = "保存样式";
            this.btnSaveLayOut.Id = 5;
            this.btnSaveLayOut.ImageIndex = 23;
            this.btnSaveLayOut.Name = "btnSaveLayOut";
            this.btnSaveLayOut.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSaveLayOut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnDeleteLayOut
            // 
            this.btnDeleteLayOut.Caption = "删除样式";
            this.btnDeleteLayOut.Id = 6;
            this.btnDeleteLayOut.ImageIndex = 22;
            this.btnDeleteLayOut.Name = "btnDeleteLayOut";
            this.btnDeleteLayOut.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnDeleteLayOut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnEdit
            // 
            this.btnEdit.Caption = "详细&E";
            this.btnEdit.Id = 2;
            this.btnEdit.ImageIndex = 4;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnImport
            // 
            this.btnImport.Caption = "导到维护&I";
            this.btnImport.Id = 3;
            this.btnImport.ImageIndex = 2;
            this.btnImport.Name = "btnImport";
            this.btnImport.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnImport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnImport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Caption = "新增&A";
            this.btnAdd.Id = 4;
            this.btnAdd.ImageIndex = 21;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "打开报表.png");
            this.imageList1.Images.SetKeyName(1, "加工拆单.png");
            this.imageList1.Images.SetKeyName(2, "导到维护.png");
            this.imageList1.Images.SetKeyName(3, "撤销完成.png");
            this.imageList1.Images.SetKeyName(4, "修改.png");
            this.imageList1.Images.SetKeyName(5, "启用.png");
            this.imageList1.Images.SetKeyName(6, "详细信息.png");
            this.imageList1.Images.SetKeyName(7, "反审.png");
            this.imageList1.Images.SetKeyName(8, "批量修改.png");
            this.imageList1.Images.SetKeyName(9, "批量新增.png");
            this.imageList1.Images.SetKeyName(10, "打印.png");
            this.imageList1.Images.SetKeyName(11, "退回.png");
            this.imageList1.Images.SetKeyName(12, "刷新.png");
            this.imageList1.Images.SetKeyName(13, "导出Excel.png");
            this.imageList1.Images.SetKeyName(14, "Excel导入.png");
            this.imageList1.Images.SetKeyName(15, "审核.png");
            this.imageList1.Images.SetKeyName(16, "删除.png");
            this.imageList1.Images.SetKeyName(17, "确认.png");
            this.imageList1.Images.SetKeyName(18, "作废.png");
            this.imageList1.Images.SetKeyName(19, "完成.png");
            this.imageList1.Images.SetKeyName(20, "撤销.png");
            this.imageList1.Images.SetKeyName(21, "新增.png");
            this.imageList1.Images.SetKeyName(22, "删除样式.png");
            this.imageList1.Images.SetKeyName(23, "保存样式.png");
            this.imageList1.Images.SetKeyName(24, "保存.png");
            // 
            // splitContrMain
            // 
            this.splitContrMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContrMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContrMain.Horizontal = false;
            this.splitContrMain.Location = new System.Drawing.Point(0, 81);
            this.splitContrMain.Name = "splitContrMain";
            this.splitContrMain.Panel1.Controls.Add(this.gridCMain);
            this.splitContrMain.Panel1.Text = "Panel1";
            this.splitContrMain.Panel2.Controls.Add(this.xtabItemInfo);
            this.splitContrMain.Panel2.Text = "Panel2";
            this.splitContrMain.Size = new System.Drawing.Size(962, 617);
            this.splitContrMain.SplitterPosition = 300;
            this.splitContrMain.TabIndex = 605;
            this.splitContrMain.Text = "splitContainerControl2";
            // 
            // gridCMain
            // 
            this.gridCMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCMain.Location = new System.Drawing.Point(0, 0);
            this.gridCMain.MainView = this.gridVMain;
            this.gridCMain.Name = "gridCMain";
            this.gridCMain.Size = new System.Drawing.Size(962, 311);
            this.gridCMain.TabIndex = 58;
            this.gridCMain.UseEmbeddedNavigator = true;
            this.gridCMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVMain});
            // 
            // gridVMain
            // 
            this.gridVMain.GridControl = this.gridCMain;
            this.gridVMain.Name = "gridVMain";
            this.gridVMain.OptionsDetail.ShowDetailTabs = false;
            this.gridVMain.OptionsSelection.MultiSelect = true;
            this.gridVMain.OptionsView.ColumnAutoWidth = false;
            this.gridVMain.OptionsView.ShowAutoFilterRow = true;
            this.gridVMain.OptionsView.ShowFooter = true;
            this.gridVMain.OptionsView.ShowGroupPanel = false;
            this.gridVMain.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridVMain_FocusedRowChanged);
            this.gridVMain.DoubleClick += new System.EventHandler(this.gridVMain_DoubleClick);
            // 
            // xtabItemInfo
            // 
            this.xtabItemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtabItemInfo.Location = new System.Drawing.Point(0, 0);
            this.xtabItemInfo.Name = "xtabItemInfo";
            this.xtabItemInfo.Size = new System.Drawing.Size(962, 300);
            this.xtabItemInfo.TabIndex = 0;
            // 
            // frmSysBusQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 698);
            this.Controls.Add(this.splitContrMain);
            this.Controls.Add(this.gcQuery);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmSysBusQuery";
            this.Text = "业务单据查询";
            this.Load += new System.EventHandler(this.frmSysBusQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContrMain)).EndInit();
            this.splitContrMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtabItemInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gcQuery;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnQuery;
        private DevExpress.XtraBars.BarButtonItem btnMore;
        private DevExpress.XtraEditors.SplitContainerControl splitContrMain;
        private DevExpress.XtraGrid.GridControl gridCMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVMain;
        private DevExpress.XtraTab.XtraTabControl xtabItemInfo;
        private DevExpress.XtraBars.BarButtonItem btnEdit;
        private DevExpress.XtraBars.BarButtonItem btnImport;
        private DevExpress.XtraBars.BarButtonItem btnAdd;
        private DevExpress.XtraBars.BarButtonItem btnSaveLayOut;
        private DevExpress.XtraBars.BarButtonItem btnDeleteLayOut;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarButtonItem btnExcel;
        private DevExpress.XtraBars.BarButtonItem btnReLoad;
    }
}