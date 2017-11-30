namespace ProduceManager
{
    partial class frmSysBusEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSysBusEdit));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnReLoad = new DevExpress.XtraBars.BarButtonItem();
            this.btnCopy = new DevExpress.XtraBars.BarButtonItem();
            this.btnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.btnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnCancel = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gridCMain = new DevExpress.XtraGrid.GridControl();
            this.gridVMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtabItemInfo = new DevExpress.XtraTab.XtraTabControl();
            this.controlNavigator1 = new DevExpress.XtraEditors.ControlNavigator();
            this.txtOrdFilter = new DevExpress.XtraEditors.TextEdit();
            this.xtabOrdParent = new DevExpress.XtraTab.XtraTabControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtabItemInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrdFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtabOrdParent)).BeginInit();
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
            this.btnEdit,
            this.btnAdd,
            this.btnSave,
            this.btnCancel,
            this.btnReLoad,
            this.btnCopy});
            this.barManager1.MaxItemId = 11;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnReLoad),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCopy, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnEdit, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSave, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCancel)});
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
            // btnReLoad
            // 
            this.btnReLoad.Caption = "重新加载";
            this.btnReLoad.Id = 8;
            this.btnReLoad.ImageIndex = 3;
            this.btnReLoad.Name = "btnReLoad";
            this.btnReLoad.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnReLoad.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnCopy
            // 
            this.btnCopy.Caption = "复制&I";
            this.btnCopy.Id = 10;
            this.btnCopy.ImageIndex = 21;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnCopy.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
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
            // btnEdit
            // 
            this.btnEdit.Caption = "修改&E";
            this.btnEdit.Id = 2;
            this.btnEdit.ImageIndex = 4;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "保存&S";
            this.btnSave.Id = 5;
            this.btnSave.ImageIndex = 24;
            this.btnSave.Name = "btnSave";
            this.btnSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Caption = "撤销&Z";
            this.btnCancel.Id = 6;
            this.btnCancel.ImageIndex = 20;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnCancel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
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
            // gridCMain
            // 
            this.gridCMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCMain.Location = new System.Drawing.Point(458, 33);
            this.gridCMain.MainView = this.gridVMain;
            this.gridCMain.Name = "gridCMain";
            this.gridCMain.Size = new System.Drawing.Size(345, 161);
            this.gridCMain.TabIndex = 58;
            this.gridCMain.UseEmbeddedNavigator = true;
            this.gridCMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVMain});
            this.gridCMain.Visible = false;
            // 
            // gridVMain
            // 
            this.gridVMain.GridControl = this.gridCMain;
            this.gridVMain.Name = "gridVMain";
            this.gridVMain.OptionsView.ColumnAutoWidth = false;
            this.gridVMain.OptionsView.ShowGroupPanel = false;
            this.gridVMain.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridVMain_FocusedRowChanged);
            // 
            // xtabItemInfo
            // 
            this.xtabItemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtabItemInfo.Location = new System.Drawing.Point(0, 203);
            this.xtabItemInfo.Name = "xtabItemInfo";
            this.xtabItemInfo.Size = new System.Drawing.Size(962, 495);
            this.xtabItemInfo.TabIndex = 608;
            // 
            // controlNavigator1
            // 
            this.controlNavigator1.Buttons.Append.Visible = false;
            this.controlNavigator1.Buttons.CancelEdit.Visible = false;
            this.controlNavigator1.Buttons.Edit.Visible = false;
            this.controlNavigator1.Buttons.EndEdit.Visible = false;
            this.controlNavigator1.Buttons.NextPage.Visible = false;
            this.controlNavigator1.Buttons.PrevPage.Visible = false;
            this.controlNavigator1.Buttons.Remove.Visible = false;
            this.controlNavigator1.Location = new System.Drawing.Point(742, 1);
            this.controlNavigator1.Name = "controlNavigator1";
            this.controlNavigator1.NavigatableControl = this.gridCMain;
            this.controlNavigator1.Size = new System.Drawing.Size(90, 22);
            this.controlNavigator1.TabIndex = 609;
            this.controlNavigator1.Text = "controlNavigator1";
            // 
            // txtOrdFilter
            // 
            this.txtOrdFilter.Location = new System.Drawing.Point(582, 2);
            this.txtOrdFilter.Name = "txtOrdFilter";
            this.txtOrdFilter.Size = new System.Drawing.Size(120, 21);
            this.txtOrdFilter.TabIndex = 610;
            this.txtOrdFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrdFilter_KeyPress);
            // 
            // xtabOrdParent
            // 
            this.xtabOrdParent.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtabOrdParent.Location = new System.Drawing.Point(0, 26);
            this.xtabOrdParent.Name = "xtabOrdParent";
            this.xtabOrdParent.Size = new System.Drawing.Size(962, 177);
            this.xtabOrdParent.TabIndex = 611;
            this.xtabOrdParent.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtabOrdParent_SelectedPageChanged);
            // 
            // frmSysBusEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 698);
            this.Controls.Add(this.txtOrdFilter);
            this.Controls.Add(this.controlNavigator1);
            this.Controls.Add(this.gridCMain);
            this.Controls.Add(this.xtabItemInfo);
            this.Controls.Add(this.xtabOrdParent);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmSysBusEdit";
            this.Text = "业务单据编辑";
            this.Load += new System.EventHandler(this.frmSysBusEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtabItemInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrdFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtabOrdParent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnEdit;
        private DevExpress.XtraBars.BarButtonItem btnAdd;
        private DevExpress.XtraGrid.GridControl gridCMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVMain;
        private DevExpress.XtraTab.XtraTabControl xtabItemInfo;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnCancel;
        private DevExpress.XtraEditors.ControlNavigator controlNavigator1;
        private DevExpress.XtraBars.BarButtonItem btnReLoad;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.TextEdit txtOrdFilter;
        private DevExpress.XtraBars.BarButtonItem btnCopy;
        private DevExpress.XtraTab.XtraTabControl xtabOrdParent;
    }
}