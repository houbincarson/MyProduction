namespace ProduceManager
{
    partial class frmSysInfoAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSysInfoAdd));
            this.btnQueryAll = new DevExpress.XtraBars.BarButtonItem();
            this.btnQuery = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnRemove = new DevExpress.XtraBars.BarButtonItem();
            this.btnRemoveAll = new DevExpress.XtraBars.BarButtonItem();
            this.btnChkItem = new DevExpress.XtraBars.BarCheckItem();
            this.btnPreview = new DevExpress.XtraBars.BarCheckItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnQuit = new DevExpress.XtraBars.BarButtonItem();
            this.btnHide = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gcQuery = new System.Windows.Forms.GroupBox();
            this.gridCInfo = new DevExpress.XtraGrid.GridControl();
            this.gridVInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.picEdit = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQueryAll
            // 
            this.btnQueryAll.Caption = "ˢ������&F";
            this.btnQueryAll.Id = 17;
            this.btnQueryAll.ImageIndex = 12;
            this.btnQueryAll.Name = "btnQueryAll";
            this.btnQueryAll.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnQueryAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnQuery
            // 
            this.btnQuery.Caption = "ˢ�µ�ǰ(F5)";
            this.btnQuery.Id = 0;
            this.btnQuery.ImageIndex = 12;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
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
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnChkItem,
            this.btnPreview,
            this.btnSave,
            this.btnQuit,
            this.btnRemove,
            this.btnRemoveAll,
            this.btnHide});
            this.barManager1.MaxItemId = 8;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRemove, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnRemoveAll),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnChkItem, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPreview, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSave, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnQuit, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnHide, true)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnRemove
            // 
            this.btnRemove.Caption = "�Ƴ�ѡ��&F";
            this.btnRemove.Id = 5;
            this.btnRemove.ImageIndex = 7;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Caption = "�Ƴ�����&D";
            this.btnRemoveAll.Id = 6;
            this.btnRemoveAll.ImageIndex = 9;
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnRemoveAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnChkItem
            // 
            this.btnChkItem.Caption = "ȫѡ��ϸ&A";
            this.btnChkItem.Id = 1;
            this.btnChkItem.ImageIndex = 6;
            this.btnChkItem.Name = "btnChkItem";
            this.btnChkItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnChkItem.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.btnChkItem_CheckedChanged);
            // 
            // btnPreview
            // 
            this.btnPreview.Caption = "Ԥ������&P";
            this.btnPreview.Id = 2;
            this.btnPreview.ImageIndex = 0;
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnPreview.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPreview_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "ȷ�ϱ���&S";
            this.btnSave.Id = 3;
            this.btnSave.ImageIndex = 24;
            this.btnSave.Name = "btnSave";
            this.btnSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnQuit
            // 
            this.btnQuit.Caption = "�رշ���&C";
            this.btnQuit.Id = 4;
            this.btnQuit.ImageIndex = 20;
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnQuit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // btnHide
            // 
            this.btnHide.Caption = "����ͼƬ���&H";
            this.btnHide.Id = 7;
            this.btnHide.ImageIndex = 16;
            this.btnHide.Name = "btnHide";
            this.btnHide.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnHide.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "�򿪱���.png");
            this.imageList1.Images.SetKeyName(1, "�ӹ���.png");
            this.imageList1.Images.SetKeyName(2, "����ά��.png");
            this.imageList1.Images.SetKeyName(3, "�������.png");
            this.imageList1.Images.SetKeyName(4, "�޸�.png");
            this.imageList1.Images.SetKeyName(5, "����.png");
            this.imageList1.Images.SetKeyName(6, "��ϸ��Ϣ.png");
            this.imageList1.Images.SetKeyName(7, "����.png");
            this.imageList1.Images.SetKeyName(8, "�����޸�.png");
            this.imageList1.Images.SetKeyName(9, "��������.png");
            this.imageList1.Images.SetKeyName(10, "��ӡ.png");
            this.imageList1.Images.SetKeyName(11, "�˻�.png");
            this.imageList1.Images.SetKeyName(12, "ˢ��.png");
            this.imageList1.Images.SetKeyName(13, "����Excel.png");
            this.imageList1.Images.SetKeyName(14, "Excel����.png");
            this.imageList1.Images.SetKeyName(15, "���.png");
            this.imageList1.Images.SetKeyName(16, "ɾ��.png");
            this.imageList1.Images.SetKeyName(17, "ȷ��.png");
            this.imageList1.Images.SetKeyName(18, "����.png");
            this.imageList1.Images.SetKeyName(19, "���.png");
            this.imageList1.Images.SetKeyName(20, "����.png");
            this.imageList1.Images.SetKeyName(21, "����.png");
            this.imageList1.Images.SetKeyName(22, "ɾ����ʽ.png");
            this.imageList1.Images.SetKeyName(23, "������ʽ.png");
            this.imageList1.Images.SetKeyName(24, "����.png");
            // 
            // gcQuery
            // 
            this.gcQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcQuery.Location = new System.Drawing.Point(0, 26);
            this.gcQuery.Name = "gcQuery";
            this.gcQuery.Size = new System.Drawing.Size(972, 61);
            this.gcQuery.TabIndex = 606;
            this.gcQuery.TabStop = false;
            this.gcQuery.Text = "��ѯ���������س�����";
            // 
            // gridCInfo
            // 
            this.gridCInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCInfo.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCInfo.Location = new System.Drawing.Point(0, 87);
            this.gridCInfo.MainView = this.gridVInfo;
            this.gridCInfo.Name = "gridCInfo";
            this.gridCInfo.Size = new System.Drawing.Size(972, 618);
            this.gridCInfo.TabIndex = 607;
            this.gridCInfo.UseEmbeddedNavigator = true;
            this.gridCInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVInfo});
            // 
            // gridVInfo
            // 
            this.gridVInfo.GridControl = this.gridCInfo;
            this.gridVInfo.Name = "gridVInfo";
            this.gridVInfo.OptionsDetail.ShowDetailTabs = false;
            this.gridVInfo.OptionsSelection.MultiSelect = true;
            this.gridVInfo.OptionsView.ColumnAutoWidth = false;
            this.gridVInfo.OptionsView.ShowAutoFilterRow = true;
            this.gridVInfo.OptionsView.ShowFooter = true;
            this.gridVInfo.OptionsView.ShowGroupPanel = false;
            // 
            // picEdit
            // 
            this.picEdit.Location = new System.Drawing.Point(522, 87);
            this.picEdit.Name = "picEdit";
            this.picEdit.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
            this.picEdit.Properties.ReadOnly = true;
            this.picEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.picEdit.Size = new System.Drawing.Size(450, 350);
            this.picEdit.TabIndex = 608;
            this.picEdit.Visible = false;
            // 
            // frmSysInfoAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 705);
            this.Controls.Add(this.picEdit);
            this.Controls.Add(this.gridCInfo);
            this.Controls.Add(this.gcQuery);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControl1);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSysInfoAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "��������ģ��";
            this.Load += new System.EventHandler(this.frmSysInfoAddItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarButtonItem btnQueryAll;
        private DevExpress.XtraBars.BarButtonItem btnQuery;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarCheckItem btnChkItem;
        private DevExpress.XtraBars.BarCheckItem btnPreview;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnQuit;
        private DevExpress.XtraBars.BarButtonItem btnRemove;
        private System.Windows.Forms.GroupBox gcQuery;
        private DevExpress.XtraGrid.GridControl gridCInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVInfo;
        private DevExpress.XtraEditors.PictureEdit picEdit;
        private DevExpress.XtraBars.BarButtonItem btnRemoveAll;
        private DevExpress.XtraBars.BarButtonItem btnHide;
    }
}