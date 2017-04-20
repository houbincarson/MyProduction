namespace ProduceManager
{
    partial class frmSysBseManger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSysBseManger));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnQuery = new DevExpress.XtraBars.BarButtonItem();
            this.btnReLoad = new DevExpress.XtraBars.BarButtonItem();
            this.btnExcel = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xtabItemInfo = new DevExpress.XtraTab.XtraTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtabItemInfo)).BeginInit();
            this.SuspendLayout();
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
            new DevExpress.XtraBars.LinkPersistInfo(this.btnReLoad, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExcel, true)});
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
            // btnReLoad
            // 
            this.btnReLoad.Caption = "重新加载";
            this.btnReLoad.Id = 8;
            this.btnReLoad.ImageIndex = 3;
            this.btnReLoad.Name = "btnReLoad";
            this.btnReLoad.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnReLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnExcel
            // 
            this.btnExcel.Caption = "导出Excel";
            this.btnExcel.Id = 7;
            this.btnExcel.ImageIndex = 13;
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
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
            // xtabItemInfo
            // 
            this.xtabItemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtabItemInfo.Location = new System.Drawing.Point(0, 26);
            this.xtabItemInfo.Name = "xtabItemInfo";
            this.xtabItemInfo.Size = new System.Drawing.Size(962, 672);
            this.xtabItemInfo.TabIndex = 609;
            // 
            // frmSysBseManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 698);
            this.Controls.Add(this.xtabItemInfo);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmSysBseManger";
            this.Text = "基础维护模板";
            this.Load += new System.EventHandler(this.frmSysBseManger_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtabItemInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnQuery;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarButtonItem btnExcel;
        private DevExpress.XtraTab.XtraTabControl xtabItemInfo;
        private DevExpress.XtraBars.BarButtonItem btnReLoad;
    }
}