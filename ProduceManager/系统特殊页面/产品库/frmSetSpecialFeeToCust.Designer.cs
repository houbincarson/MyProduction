namespace ProduceManager
{
    partial class frmSetSpecialFeeToCust
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetSpecialFeeToCust));
            this.btnSet = new DevExpress.XtraEditors.SimpleButton();
            this.lblName = new DevExpress.XtraEditors.LabelControl();
            this.lblPNumber = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridVMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridCMain = new DevExpress.XtraGrid.GridControl();
            this.splitContainerControl5 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl3 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnRestore = new DevExpress.XtraEditors.SimpleButton();
            this.txtPNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.gcNav = new System.Windows.Forms.GroupBox();
            this.extTreePc = new ExtendControl.ExtPopupTree();
            this.lblGoTo = new System.Windows.Forms.Label();
            this.btnSetFee = new DevExpress.XtraEditors.SimpleButton();
            this.cboUnitFee = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkComCust = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainerControl4 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl5)).BeginInit();
            this.splitContainerControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).BeginInit();
            this.splitContainerControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPNumber.Properties)).BeginInit();
            this.gcNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extTreePc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUnitFee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkComCust.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl4)).BeginInit();
            this.splitContainerControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(497, 4);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(88, 34);
            this.btnSet.TabIndex = 408;
            this.btnSet.Text = "保存设置";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(78, 304);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 14);
            this.lblName.TabIndex = 402;
            // 
            // lblPNumber
            // 
            this.lblPNumber.Location = new System.Drawing.Point(78, 273);
            this.lblPNumber.Name = "lblPNumber";
            this.lblPNumber.Size = new System.Drawing.Size(0, 14);
            this.lblPNumber.TabIndex = 401;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(10, 304);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(60, 14);
            this.labelControl5.TabIndex = 400;
            this.labelControl5.Text = "款式名称：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(10, 273);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 399;
            this.labelControl4.Text = "款式编号：";
            // 
            // picImage
            // 
            this.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.picImage.Location = new System.Drawing.Point(0, 0);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(258, 263);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.TabIndex = 398;
            this.picImage.TabStop = false;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 0)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemGridLookUpEdit1
            // 
            this.repositoryItemGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            this.repositoryItemGridLookUpEdit1.NullText = "";
            this.repositoryItemGridLookUpEdit1.View = this.gridView2;
            // 
            // gridVMain
            // 
            this.gridVMain.GridControl = this.gridCMain;
            this.gridVMain.Name = "gridVMain";
            this.gridVMain.OptionsCustomization.AllowFilter = false;
            this.gridVMain.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridVMain.OptionsView.ShowAutoFilterRow = true;
            this.gridVMain.OptionsView.ShowGroupPanel = false;
            this.gridVMain.DoubleClick += new System.EventHandler(this.gridVMain_DoubleClick);
            // 
            // gridCMain
            // 
            this.gridCMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCMain.Location = new System.Drawing.Point(0, 0);
            this.gridCMain.MainView = this.gridVMain;
            this.gridCMain.Name = "gridCMain";
            this.gridCMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemGridLookUpEdit1,
            this.repositoryItemImageComboBox1});
            this.gridCMain.Size = new System.Drawing.Size(975, 475);
            this.gridCMain.TabIndex = 1;
            this.gridCMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVMain});
            // 
            // splitContainerControl5
            // 
            this.splitContainerControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl5.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl5.Name = "splitContainerControl5";
            this.splitContainerControl5.Panel1.Controls.Add(this.gridCMain);
            this.splitContainerControl5.Panel1.Text = "Panel1";
            this.splitContainerControl5.Panel2.Controls.Add(this.lblName);
            this.splitContainerControl5.Panel2.Controls.Add(this.lblPNumber);
            this.splitContainerControl5.Panel2.Controls.Add(this.labelControl5);
            this.splitContainerControl5.Panel2.Controls.Add(this.labelControl4);
            this.splitContainerControl5.Panel2.Controls.Add(this.picImage);
            this.splitContainerControl5.Panel2.Text = "Panel2";
            this.splitContainerControl5.Size = new System.Drawing.Size(1239, 475);
            this.splitContainerControl5.SplitterPosition = 975;
            this.splitContainerControl5.TabIndex = 398;
            this.splitContainerControl5.Text = "splitContainerControl5";
            // 
            // splitContainerControl3
            // 
            this.splitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl3.Horizontal = false;
            this.splitContainerControl3.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl3.Name = "splitContainerControl3";
            this.splitContainerControl3.Panel1.Controls.Add(this.splitContainerControl5);
            this.splitContainerControl3.Panel1.Text = "Panel1";
            this.splitContainerControl3.Panel2.Controls.Add(this.btnRestore);
            this.splitContainerControl3.Panel2.Controls.Add(this.btnSet);
            this.splitContainerControl3.Panel2.Text = "Panel2";
            this.splitContainerControl3.Size = new System.Drawing.Size(1239, 523);
            this.splitContainerControl3.SplitterPosition = 475;
            this.splitContainerControl3.TabIndex = 404;
            this.splitContainerControl3.Text = "splitContainerControl3";
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(619, 4);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(88, 34);
            this.btnRestore.TabIndex = 409;
            this.btnRestore.Text = "撤销设置";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // txtPNumber
            // 
            this.txtPNumber.EditValue = "";
            this.txtPNumber.Location = new System.Drawing.Point(86, 15);
            this.txtPNumber.Name = "txtPNumber";
            this.txtPNumber.Size = new System.Drawing.Size(149, 21);
            this.txtPNumber.TabIndex = 394;
            this.txtPNumber.Tag = "OrdNumber";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(22, 19);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 393;
            this.labelControl2.Text = "款式编号：";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(645, 9);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(101, 27);
            this.btnOk.TabIndex = 178;
            this.btnOk.Text = "查询(&C)";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(239, 19);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 176;
            this.labelControl3.Text = "所属类别";
            // 
            // gcNav
            // 
            this.gcNav.Controls.Add(this.extTreePc);
            this.gcNav.Controls.Add(this.lblGoTo);
            this.gcNav.Controls.Add(this.btnSetFee);
            this.gcNav.Controls.Add(this.cboUnitFee);
            this.gcNav.Controls.Add(this.labelControl8);
            this.gcNav.Controls.Add(this.txtPNumber);
            this.gcNav.Controls.Add(this.labelControl2);
            this.gcNav.Controls.Add(this.btnOk);
            this.gcNav.Controls.Add(this.labelControl3);
            this.gcNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNav.Location = new System.Drawing.Point(0, 0);
            this.gcNav.Name = "gcNav";
            this.gcNav.Size = new System.Drawing.Size(1239, 46);
            this.gcNav.TabIndex = 396;
            this.gcNav.TabStop = false;
            this.gcNav.Text = "查询条件";
            // 
            // extTreePc
            // 
            this.extTreePc.DataSource = null;
            this.extTreePc.DisplayMember = null;
            this.extTreePc.Location = new System.Drawing.Point(294, 14);
            this.extTreePc.Name = "extTreePc";
            this.extTreePc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)}); 
            this.extTreePc.Properties.ShowPopupCloseButton = false;
            this.extTreePc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard; 
            this.extTreePc.Size = new System.Drawing.Size(145, 21);
            this.extTreePc.TabIndex = 399;
            this.extTreePc.ValueMember = null;
            // 
            // lblGoTo
            // 
            this.lblGoTo.AutoSize = true;
            this.lblGoTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Underline);
            this.lblGoTo.ForeColor = System.Drawing.Color.Blue;
            this.lblGoTo.Location = new System.Drawing.Point(859, 19);
            this.lblGoTo.Name = "lblGoTo";
            this.lblGoTo.Size = new System.Drawing.Size(367, 14);
            this.lblGoTo.TabIndex = 398;
            this.lblGoTo.Text = "设置完成后，点击此处可查看当前页面上的款式的特殊工费详细信息";
            this.lblGoTo.Click += new System.EventHandler(this.lblGoTo_Click);
            // 
            // btnSetFee
            // 
            this.btnSetFee.Location = new System.Drawing.Point(752, 9);
            this.btnSetFee.Name = "btnSetFee";
            this.btnSetFee.Size = new System.Drawing.Size(101, 27);
            this.btnSetFee.TabIndex = 397;
            this.btnSetFee.Text = "设置特殊工费";
            this.btnSetFee.Click += new System.EventHandler(this.btnSetFee_Click);
            // 
            // cboUnitFee
            // 
            this.cboUnitFee.Location = new System.Drawing.Point(535, 13);
            this.cboUnitFee.Name = "cboUnitFee";
            this.cboUnitFee.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboUnitFee.Properties.Items.AddRange(new object[] {
            "按克计",
            "按件计"});
            this.cboUnitFee.Size = new System.Drawing.Size(100, 21);
            this.cboUnitFee.TabIndex = 396;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(445, 17);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(84, 14);
            this.labelControl8.TabIndex = 395;
            this.labelControl8.Text = "工费计算方式：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(14, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 394;
            this.labelControl1.Text = "客户名称：";
            // 
            // chkComCust
            // 
            this.chkComCust.Location = new System.Drawing.Point(86, 18);
            this.chkComCust.Name = "chkComCust";
            this.chkComCust.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkComCust.Size = new System.Drawing.Size(946, 21);
            this.chkComCust.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.chkComCust);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1239, 47);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择要设置特殊工费的客户";
            // 
            // splitContainerControl4
            // 
            this.splitContainerControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl4.Horizontal = false;
            this.splitContainerControl4.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl4.Name = "splitContainerControl4";
            this.splitContainerControl4.Panel1.Controls.Add(this.groupBox1);
            this.splitContainerControl4.Panel1.Text = "Panel1";
            this.splitContainerControl4.Panel2.Controls.Add(this.gcNav);
            this.splitContainerControl4.Panel2.Text = "Panel2";
            this.splitContainerControl4.Size = new System.Drawing.Size(1239, 99);
            this.splitContainerControl4.SplitterPosition = 47;
            this.splitContainerControl4.TabIndex = 2;
            this.splitContainerControl4.Text = "splitContainerControl4";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.splitContainerControl4);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.splitContainerControl3);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1239, 628);
            this.splitContainerControl2.SplitterPosition = 99;
            this.splitContainerControl2.TabIndex = 71;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(48, 19);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "灰.png");
            this.imageCollection1.Images.SetKeyName(1, "灰黑.png");
            this.imageCollection1.Images.SetKeyName(2, "绿.png");
            // 
            // frmSetSpecialFeeToCust
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 628);
            this.Controls.Add(this.splitContainerControl2);
            this.Name = "frmSetSpecialFeeToCust";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "特殊工费设置";
            this.Load += new System.EventHandler(this.frmSetSpecialFeeToCust_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl5)).EndInit();
            this.splitContainerControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).EndInit();
            this.splitContainerControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPNumber.Properties)).EndInit();
            this.gcNav.ResumeLayout(false);
            this.gcNav.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.extTreePc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboUnitFee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkComCust.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl4)).EndInit();
            this.splitContainerControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private DevExpress.XtraEditors.SimpleButton btnSet;
        private DevExpress.XtraEditors.LabelControl lblName;
        private DevExpress.XtraEditors.LabelControl lblPNumber;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.PictureBox picImage;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVMain;
        private DevExpress.XtraGrid.GridControl gridCMain;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl5;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl3;
        private DevExpress.XtraEditors.SimpleButton btnRestore;
        private DevExpress.XtraEditors.TextEdit txtPNumber;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.GroupBox gcNav;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit chkComCust;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl4;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.ComboBoxEdit cboUnitFee;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.SimpleButton btnSetFee;
        private System.Windows.Forms.Label lblGoTo;
        private ExtendControl.ExtPopupTree extTreePc;


    }
}