namespace ProduceManager
{
    partial class frmProdAuditMgr
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcNav = new System.Windows.Forms.GroupBox();
            this.extTreePc = new ExtendControl.ExtPopupTree();
            this.btnLayout = new DevExpress.XtraEditors.SimpleButton();
            this.btnCardMode = new DevExpress.XtraEditors.SimpleButton();
            this.btnListMode = new DevExpress.XtraEditors.SimpleButton();
            this.dplFy = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.txtPNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.splitContainerControl3 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridCMain = new DevExpress.XtraGrid.GridControl();
            this.CarVMain = new DevExpress.XtraGrid.Views.Card.CardView();
            this.layoutViewColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutViewColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repPicture = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dxPager1 = new ProduceManager.DxPager();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            this.gcNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extTreePc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplFy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).BeginInit();
            this.splitContainerControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarVMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.Name = "gridView1";
            // 
            // gridView2
            // 
            this.gridView2.Name = "gridView2";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.gcNav);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.splitContainerControl3);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1247, 614);
            this.splitContainerControl2.SplitterPosition = 46;
            this.splitContainerControl2.TabIndex = 69;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // gcNav
            // 
            this.gcNav.Controls.Add(this.extTreePc);
            this.gcNav.Controls.Add(this.btnLayout);
            this.gcNav.Controls.Add(this.btnCardMode);
            this.gcNav.Controls.Add(this.btnListMode);
            this.gcNav.Controls.Add(this.dplFy);
            this.gcNav.Controls.Add(this.labelControl12);
            this.gcNav.Controls.Add(this.txtPNumber);
            this.gcNav.Controls.Add(this.labelControl4);
            this.gcNav.Controls.Add(this.btnOk);
            this.gcNav.Controls.Add(this.labelControl5);
            this.gcNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNav.Location = new System.Drawing.Point(0, 0);
            this.gcNav.Name = "gcNav";
            this.gcNav.Size = new System.Drawing.Size(1247, 46);
            this.gcNav.TabIndex = 70;
            this.gcNav.TabStop = false;
            this.gcNav.Text = "查询条件";
            // 
            // extTreePc
            // 
            this.extTreePc.DataSource = null;
            this.extTreePc.DisplayMember = null;
            this.extTreePc.Location = new System.Drawing.Point(447, 18);
            this.extTreePc.Name = "extTreePc";
            this.extTreePc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});  
            this.extTreePc.Properties.ShowPopupCloseButton = false;
            this.extTreePc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard; 
            this.extTreePc.Size = new System.Drawing.Size(134, 21);
            this.extTreePc.TabIndex = 433;
            this.extTreePc.ValueMember = null;
            // 
            // btnLayout
            // 
            this.btnLayout.Location = new System.Drawing.Point(655, 16);
            this.btnLayout.Name = "btnLayout";
            this.btnLayout.Size = new System.Drawing.Size(58, 23);
            this.btnLayout.TabIndex = 431;
            this.btnLayout.Text = "组合模式";
            this.btnLayout.Visible = false;
            this.btnLayout.Click += new System.EventHandler(this.btnLayoutMode_Click);
            // 
            // btnCardMode
            // 
            this.btnCardMode.Location = new System.Drawing.Point(783, 16);
            this.btnCardMode.Name = "btnCardMode";
            this.btnCardMode.Size = new System.Drawing.Size(58, 23);
            this.btnCardMode.TabIndex = 430;
            this.btnCardMode.Text = "卡片模式";
            this.btnCardMode.Visible = false;
            this.btnCardMode.Click += new System.EventHandler(this.btnCardMode_Click);
            // 
            // btnListMode
            // 
            this.btnListMode.Location = new System.Drawing.Point(719, 17);
            this.btnListMode.Name = "btnListMode";
            this.btnListMode.Size = new System.Drawing.Size(58, 23);
            this.btnListMode.TabIndex = 429;
            this.btnListMode.Text = "列表模式";
            this.btnListMode.Visible = false;
            this.btnListMode.Click += new System.EventHandler(this.btnListMode_Click);
            // 
            // dplFy
            // 
            this.dplFy.Location = new System.Drawing.Point(73, 19);
            this.dplFy.Name = "dplFy";
            this.dplFy.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dplFy.Properties.DropDownRows = 12;
            this.dplFy.Properties.ImmediatePopup = true;
            this.dplFy.Properties.NullText = "";
            this.dplFy.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            this.dplFy.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.dplFy.Size = new System.Drawing.Size(113, 21);
            this.dplFy.TabIndex = 427;
            this.dplFy.Tag = "To_Dept";
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl12.Appearance.Options.UseForeColor = true;
            this.labelControl12.Location = new System.Drawing.Point(16, 22);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(48, 14);
            this.labelControl12.TabIndex = 428;
            this.labelControl12.Text = "选择工厂";
            // 
            // txtPNumber
            // 
            this.txtPNumber.EditValue = "";
            this.txtPNumber.Location = new System.Drawing.Point(273, 19);
            this.txtPNumber.Name = "txtPNumber";
            this.txtPNumber.Size = new System.Drawing.Size(113, 21);
            this.txtPNumber.TabIndex = 426;
            this.txtPNumber.Tag = "OrdNumber";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(196, 22);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 425;
            this.labelControl4.Text = "款式编号：";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(587, 17);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(58, 23);
            this.btnOk.TabIndex = 423;
            this.btnOk.Text = "查询&C";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(392, 22);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 422;
            this.labelControl5.Text = "所属类别";
            // 
            // splitContainerControl3
            // 
            this.splitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl3.Horizontal = false;
            this.splitContainerControl3.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl3.Name = "splitContainerControl3";
            this.splitContainerControl3.Panel1.Controls.Add(this.gridCMain);
            this.splitContainerControl3.Panel1.Text = "Panel1";
            this.splitContainerControl3.Panel2.Controls.Add(this.dxPager1);
            this.splitContainerControl3.Panel2.Text = "Panel2";
            this.splitContainerControl3.Size = new System.Drawing.Size(1247, 562);
            this.splitContainerControl3.SplitterPosition = 512;
            this.splitContainerControl3.TabIndex = 404;
            this.splitContainerControl3.Text = "splitContainerControl3";
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
            this.gridCMain.MainView = this.CarVMain;
            this.gridCMain.Name = "gridCMain";
            this.gridCMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repPicture});
            this.gridCMain.Size = new System.Drawing.Size(1247, 512);
            this.gridCMain.TabIndex = 63;
            this.gridCMain.UseEmbeddedNavigator = true;
            this.gridCMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.CarVMain});
            // 
            // CarVMain
            // 
            this.CarVMain.CardInterval = 14;
            this.CarVMain.CardWidth = 230;
            this.CarVMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.layoutViewColumn2,
            this.layoutViewColumn3,
            this.gridColumn5,
            this.gridColumn1});
            this.CarVMain.DetailHeight = 300;
            this.CarVMain.FocusedCardTopFieldIndex = 0;
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.DarkSalmon;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = false;
            this.CarVMain.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.CarVMain.GridControl = this.gridCMain;
            this.CarVMain.MaximumCardColumns = 5;
            this.CarVMain.Name = "CarVMain";
            this.CarVMain.OptionsBehavior.FieldAutoHeight = true;
            this.CarVMain.OptionsSelection.MultiSelect = true;
            this.CarVMain.OptionsView.ShowCardCaption = false;
            this.CarVMain.OptionsView.ShowQuickCustomizeButton = false;
            // 
            // layoutViewColumn2
            // 
            this.layoutViewColumn2.Caption = "款式编号";
            this.layoutViewColumn2.FieldName = "PNumber";
            this.layoutViewColumn2.Name = "layoutViewColumn2";
            this.layoutViewColumn2.Visible = true;
            this.layoutViewColumn2.VisibleIndex = 0;
            // 
            // layoutViewColumn3
            // 
            this.layoutViewColumn3.Caption = "款式名称";
            this.layoutViewColumn3.FieldName = "Name";
            this.layoutViewColumn3.Name = "layoutViewColumn3";
            this.layoutViewColumn3.Visible = true;
            this.layoutViewColumn3.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "款式图片";
            this.gridColumn5.ColumnEdit = this.repPicture;
            this.gridColumn5.FieldName = "PhotoName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowSize = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // repPicture
            // 
            this.repPicture.CustomHeight = 150;
            this.repPicture.Name = "repPicture";
            this.repPicture.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.repPicture.DoubleClick += new System.EventHandler(this.repPicture_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "序号";
            this.gridColumn1.FieldName = "Bus_PM_Id";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // dxPager1
            // 
            this.dxPager1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dxPager1.CustPageSize = 10;
            this.dxPager1.Location = new System.Drawing.Point(0, 0);
            this.dxPager1.Name = "dxPager1";
            this.dxPager1.PageIndex = 1;
            this.dxPager1.RecordCount = 0;
            this.dxPager1.Size = new System.Drawing.Size(820, 32);
            this.dxPager1.TabIndex = 1;
            this.dxPager1.PageChanged += new ProduceManager.PageChangedEventHandler(this.dxPager1_PageChanged);
            // 
            // frmProdAuditMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 614);
            this.Controls.Add(this.splitContainerControl2);
            this.Name = "frmProdAuditMgr";
            this.Text = "款式修改";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmProdAuditMgr_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            this.gcNav.ResumeLayout(false);
            this.gcNav.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extTreePc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplFy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).EndInit();
            this.splitContainerControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarVMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private System.Windows.Forms.GroupBox gcNav;
        private ExtendControl.ExtPopupTree extTreePc;
        private DevExpress.XtraEditors.SimpleButton btnLayout;
        private DevExpress.XtraEditors.SimpleButton btnCardMode;
        private DevExpress.XtraEditors.SimpleButton btnListMode;
        private DevExpress.XtraEditors.LookUpEdit dplFy;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.TextEdit txtPNumber;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl3;
        private DxPager dxPager1;
        private DevExpress.XtraGrid.GridControl gridCMain;
        private DevExpress.XtraGrid.Views.Card.CardView CarVMain;
        private DevExpress.XtraGrid.Columns.GridColumn layoutViewColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn layoutViewColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repPicture;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;


    }
}