namespace ProduceManager
{
    partial class frmProdAreaRelMgrEdit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lueArea = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnMoreCondition = new DevExpress.XtraEditors.SimpleButton();
            this.dtEndDay = new DevExpress.XtraEditors.DateEdit();
            this.dtStDay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtBus_PNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl3 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridCMain = new DevExpress.XtraGrid.GridControl();
            this.gridVMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckedComboBoxEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PicImg = new System.Windows.Forms.PictureBox();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSet = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStDay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBus_PNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).BeginInit();
            this.splitContainerControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicImg)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1030, 465);
            this.splitContainerControl1.SplitterPosition = 75;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lueArea);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.btnMoreCondition);
            this.groupBox1.Controls.Add(this.dtEndDay);
            this.groupBox1.Controls.Add(this.dtStDay);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.txtBus_PNumber);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1030, 75);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // lueArea
            // 
            this.lueArea.Location = new System.Drawing.Point(355, 43);
            this.lueArea.Name = "lueArea";
            this.lueArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueArea.Properties.DropDownRows = 12;
            this.lueArea.Properties.ImmediatePopup = true;
            this.lueArea.Properties.NullText = "";
            this.lueArea.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            this.lueArea.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lueArea.Size = new System.Drawing.Size(113, 21);
            this.lueArea.TabIndex = 428;
            this.lueArea.Tag = "To_Dept";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(317, 46);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 17;
            this.labelControl4.Text = "区域：";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(474, 21);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(172, 14);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "注:可输入多个款号，用逗号隔开";
            // 
            // btnMoreCondition
            // 
            this.btnMoreCondition.Location = new System.Drawing.Point(652, 12);
            this.btnMoreCondition.Name = "btnMoreCondition";
            this.btnMoreCondition.Size = new System.Drawing.Size(107, 27);
            this.btnMoreCondition.TabIndex = 15;
            this.btnMoreCondition.Text = "更多筛选条件";
            this.btnMoreCondition.Visible = false;
            this.btnMoreCondition.Click += new System.EventHandler(this.btnMoreCondition_Click);
            // 
            // dtEndDay
            // 
            this.dtEndDay.EditValue = null;
            this.dtEndDay.Location = new System.Drawing.Point(194, 43);
            this.dtEndDay.Name = "dtEndDay";
            this.dtEndDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEndDay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtEndDay.Size = new System.Drawing.Size(117, 21);
            this.dtEndDay.TabIndex = 14;
            // 
            // dtStDay
            // 
            this.dtStDay.EditValue = null;
            this.dtStDay.Location = new System.Drawing.Point(71, 45);
            this.dtStDay.Name = "dtStDay";
            this.dtStDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStDay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtStDay.Size = new System.Drawing.Size(117, 21);
            this.dtStDay.TabIndex = 13;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "建款日期：";
            // 
            // txtBus_PNumber
            // 
            this.txtBus_PNumber.Location = new System.Drawing.Point(71, 16);
            this.txtBus_PNumber.Name = "txtBus_PNumber";
            this.txtBus_PNumber.Size = new System.Drawing.Size(397, 21);
            this.txtBus_PNumber.TabIndex = 11;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "营销款号：";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(474, 38);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(87, 27);
            this.btnQuery.TabIndex = 9;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.splitContainerControl3);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.btnCancel);
            this.splitContainerControl2.Panel2.Controls.Add(this.btnSet);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1030, 384);
            this.splitContainerControl2.SplitterPosition = 328;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // splitContainerControl3
            // 
            this.splitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl3.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl3.Name = "splitContainerControl3";
            this.splitContainerControl3.Panel1.Controls.Add(this.gridCMain);
            this.splitContainerControl3.Panel1.Text = "Panel1";
            this.splitContainerControl3.Panel2.Controls.Add(this.PicImg);
            this.splitContainerControl3.Panel2.Text = "Panel2";
            this.splitContainerControl3.Size = new System.Drawing.Size(1030, 328);
            this.splitContainerControl3.SplitterPosition = 767;
            this.splitContainerControl3.TabIndex = 0;
            this.splitContainerControl3.Text = "splitContainerControl3";
            // 
            // gridCMain
            // 
            this.gridCMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCMain.Location = new System.Drawing.Point(0, 0);
            this.gridCMain.MainView = this.gridVMain;
            this.gridCMain.Name = "gridCMain";
            this.gridCMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemCheckedComboBoxEdit1});
            this.gridCMain.Size = new System.Drawing.Size(767, 328);
            this.gridCMain.TabIndex = 1;
            this.gridCMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVMain});
            // 
            // gridVMain
            // 
            this.gridVMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn1});
            this.gridVMain.GridControl = this.gridCMain;
            this.gridVMain.Name = "gridVMain";
            this.gridVMain.OptionsView.ShowGroupPanel = false;
            this.gridVMain.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "序号";
            this.gridColumn4.FieldName = "PM_Area_Id";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "款式名称";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "成色";
            this.gridColumn3.ColumnEdit = this.repositoryItemCheckedComboBoxEdit1;
            this.gridColumn3.FieldName = "Material";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // repositoryItemCheckedComboBoxEdit1
            // 
            this.repositoryItemCheckedComboBoxEdit1.AutoHeight = false;
            this.repositoryItemCheckedComboBoxEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCheckedComboBoxEdit1.Name = "repositoryItemCheckedComboBoxEdit1";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "克销售工费";
            this.gridColumn5.FieldName = "WeightFee";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "件销售工费";
            this.gridColumn6.FieldName = "AmoutFee";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 7;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "类别";
            this.gridColumn7.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.gridColumn7.FieldName = "Pc_Id";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.ReadOnly = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "工厂款号";
            this.gridColumn8.FieldName = "PNumber";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "营销款号";
            this.gridColumn9.FieldName = "Bus_PNumber";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 2;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "合并款号";
            this.gridColumn10.FieldName = "MergePNumber";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 3;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "所属区域";
            this.gridColumn1.FieldName = "AreaName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // PicImg
            // 
            this.PicImg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PicImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicImg.Location = new System.Drawing.Point(0, 0);
            this.PicImg.Name = "PicImg";
            this.PicImg.Size = new System.Drawing.Size(257, 328);
            this.PicImg.TabIndex = 0;
            this.PicImg.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(613, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "重置";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(418, 11);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(175, 27);
            this.btnSet.TabIndex = 0;
            this.btnSet.Text = "把选中款从区域款式中撤销";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // frmProdAreaRelMgrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1030, 465);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmProdAreaRelMgrEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "区域款式编辑";
            this.Load += new System.EventHandler(this.frmProdAreaRelMgrEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lueArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStDay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBus_PNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).EndInit();
            this.splitContainerControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckedComboBoxEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSet;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnMoreCondition;
        private DevExpress.XtraEditors.DateEdit dtEndDay;
        private DevExpress.XtraEditors.DateEdit dtStDay;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtBus_PNumber;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lueArea;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl3;
        private DevExpress.XtraGrid.GridControl gridCMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVMain;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private System.Windows.Forms.PictureBox PicImg;
    }
}