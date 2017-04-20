namespace ProduceManager
{
    partial class frmProdFeeComfirm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProdFeeComfirm));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcNav = new System.Windows.Forms.GroupBox();
            this.btnSynchronization = new DevExpress.XtraEditors.SimpleButton();
            this.extTreePc = new ExtendControl.ExtPopupTree();
            this.txtBus_PNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.gridCMain = new DevExpress.XtraGrid.GridControl();
            this.gridVMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repImg = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dplFy = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.lblGoTo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            this.gcNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extTreePc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBus_PNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplFy)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "up.png");
            this.imageCollection1.Images.SetKeyName(1, "down.png");
            this.imageCollection1.Images.SetKeyName(2, "no.png");
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.gcNav);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.gridCMain);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1207, 597);
            this.splitContainerControl2.SplitterPosition = 49;
            this.splitContainerControl2.TabIndex = 70;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // gcNav
            // 
            this.gcNav.Controls.Add(this.label2);
            this.gcNav.Controls.Add(this.label1);
            this.gcNav.Controls.Add(this.lblGoTo);
            this.gcNav.Controls.Add(this.btnSynchronization);
            this.gcNav.Controls.Add(this.extTreePc);
            this.gcNav.Controls.Add(this.txtBus_PNumber);
            this.gcNav.Controls.Add(this.labelControl2);
            this.gcNav.Controls.Add(this.btnOk);
            this.gcNav.Controls.Add(this.labelControl3);
            this.gcNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNav.Location = new System.Drawing.Point(0, 0);
            this.gcNav.Name = "gcNav";
            this.gcNav.Size = new System.Drawing.Size(1207, 49);
            this.gcNav.TabIndex = 397;
            this.gcNav.TabStop = false;
            this.gcNav.Text = "查询条件";
            // 
            // btnSynchronization
            // 
            this.btnSynchronization.Location = new System.Drawing.Point(575, 12);
            this.btnSynchronization.Name = "btnSynchronization";
            this.btnSynchronization.Size = new System.Drawing.Size(101, 27);
            this.btnSynchronization.TabIndex = 396;
            this.btnSynchronization.Text = "同步所有工费";
            this.btnSynchronization.Click += new System.EventHandler(this.btnSynchronization_Click);
            // 
            // extTreePc
            // 
            this.extTreePc.DataSource = null;
            this.extTreePc.DisplayMember = null;
            this.extTreePc.Location = new System.Drawing.Point(301, 14);
            this.extTreePc.Name = "extTreePc";
            this.extTreePc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)}); 
            this.extTreePc.Properties.ShowPopupCloseButton = false;
            this.extTreePc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard; 
            this.extTreePc.Size = new System.Drawing.Size(139, 21);
            this.extTreePc.TabIndex = 395;
            this.extTreePc.ValueMember = null;
            // 
            // txtBus_PNumber
            // 
            this.txtBus_PNumber.EditValue = "";
            this.txtBus_PNumber.Location = new System.Drawing.Point(96, 14);
            this.txtBus_PNumber.Name = "txtBus_PNumber";
            this.txtBus_PNumber.Size = new System.Drawing.Size(149, 21);
            this.txtBus_PNumber.TabIndex = 394;
            this.txtBus_PNumber.Tag = "";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 19);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 14);
            this.labelControl2.TabIndex = 393;
            this.labelControl2.Text = "营销款式编号：";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(468, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(101, 27);
            this.btnOk.TabIndex = 178;
            this.btnOk.Text = "查询(&C)";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(251, 19);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 176;
            this.labelControl3.Text = "所属类别";
            // 
            // gridCMain
            // 
            this.gridCMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCMain.Location = new System.Drawing.Point(0, 0);
            this.gridCMain.MainView = this.gridVMain;
            this.gridCMain.Name = "gridCMain";
            this.gridCMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repImg,
            this.dplFy});
            this.gridCMain.Size = new System.Drawing.Size(1207, 542);
            this.gridCMain.TabIndex = 3;
            this.gridCMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVMain});
            // 
            // gridVMain
            // 
            this.gridVMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.gridVMain.GridControl = this.gridCMain;
            this.gridVMain.Name = "gridVMain";
            this.gridVMain.OptionsBehavior.Editable = false;
            this.gridVMain.OptionsCustomization.AllowFilter = false;
            this.gridVMain.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridVMain.OptionsView.ShowAutoFilterRow = true;
            this.gridVMain.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "营销序号";
            this.gridColumn1.FieldName = "Bus_PM_Id";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "工厂序号";
            this.gridColumn2.FieldName = "PM_Id";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "名称";
            this.gridColumn3.FieldName = "Bus_Name";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "营销编号";
            this.gridColumn4.FieldName = "Bus_PNumber";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "工厂编号";
            this.gridColumn5.FieldName = "PNumber";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "当前工费";
            this.gridColumn6.FieldName = "Bus_ComfirmFee";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "调整后工费";
            this.gridColumn7.FieldName = "ResultFee";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "调整情况";
            this.gridColumn8.ColumnEdit = this.repImg;
            this.gridColumn8.FieldName = "Adjustment";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 5;
            // 
            // repImg
            // 
            this.repImg.AutoHeight = false;
            this.repImg.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repImg.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 2)});
            this.repImg.LargeImages = this.imageCollection1;
            this.repImg.Name = "repImg";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "调整幅度";
            this.gridColumn9.FieldName = "AdjustmentFee";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 6;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "生产工厂";
            this.gridColumn10.ColumnEdit = this.dplFy;
            this.gridColumn10.FieldName = "Fy_Id";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            // 
            // dplFy
            // 
            this.dplFy.AutoHeight = false;
            this.dplFy.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dplFy.Name = "dplFy";
            // 
            // lblGoTo
            // 
            this.lblGoTo.AutoSize = true;
            this.lblGoTo.Font = new System.Drawing.Font("宋体", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblGoTo.ForeColor = System.Drawing.Color.Red;
            this.lblGoTo.Location = new System.Drawing.Point(724, 21);
            this.lblGoTo.Name = "lblGoTo";
            this.lblGoTo.Size = new System.Drawing.Size(37, 14);
            this.lblGoTo.TabIndex = 397;
            this.lblGoTo.Text = "此处";
            this.lblGoTo.Click += new System.EventHandler(this.lblGoTo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(691, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 398;
            this.label1.Text = "点击";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(759, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 14);
            this.label2.TabIndex = 399;
            this.label2.Text = "查看历史操作记录";
            // 
            // frmProdFeeComfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 597);
            this.Controls.Add(this.splitContainerControl2);
            this.Name = "frmProdFeeComfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "工费查询确认";
            this.Load += new System.EventHandler(this.frmProdFeeComfirm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            this.gcNav.ResumeLayout(false);
            this.gcNav.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extTreePc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBus_PNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplFy)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private System.Windows.Forms.GroupBox gcNav;
        private ExtendControl.ExtPopupTree extTreePc;
        private DevExpress.XtraEditors.TextEdit txtBus_PNumber;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl gridCMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVMain;
        private DevExpress.XtraEditors.SimpleButton btnSynchronization;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repImg;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit dplFy;
        private System.Windows.Forms.Label lblGoTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

    }
}