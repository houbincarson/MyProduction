namespace ProduceManager
{
    partial class frmProdPromotionMgrEdit
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridVCom = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridCMain = new DevExpress.XtraGrid.GridControl();
            this.gridVMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcNav = new System.Windows.Forms.GroupBox();
            this.txtFANumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelMeg = new DevExpress.XtraEditors.SimpleButton();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridVCom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.gcNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFANumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridVCom
            // 
            this.gridVCom.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn8,
            this.gridColumn5});
            this.gridVCom.GridControl = this.gridCMain;
            this.gridVCom.Name = "gridVCom";
            this.gridVCom.OptionsView.ShowGroupPanel = false;
            this.gridVCom.DoubleClick += new System.EventHandler(this.gridVCom_DoubleClick);
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "款式名称";
            this.gridColumn6.FieldName = "Name";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "款式编号";
            this.gridColumn8.FieldName = "PNumber";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "序号";
            this.gridColumn5.FieldName = "Bus_PM_Id";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            // 
            // gridCMain
            // 
            this.gridCMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridLevelNode2.LevelTemplate = this.gridVCom;
            gridLevelNode2.RelationName = "ChildGrid";
            this.gridCMain.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.gridCMain.Location = new System.Drawing.Point(0, 0);
            this.gridCMain.MainView = this.gridVMain;
            this.gridCMain.Name = "gridCMain";
            this.gridCMain.Size = new System.Drawing.Size(660, 384);
            this.gridCMain.TabIndex = 57;
            this.gridCMain.UseEmbeddedNavigator = true;
            this.gridCMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVMain,
            this.gridVCom});
            // 
            // gridVMain
            // 
            this.gridVMain.ChildGridLevelName = "ChildGrid";
            this.gridVMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.gridVMain.GridControl = this.gridCMain;
            this.gridVMain.Name = "gridVMain";
            this.gridVMain.OptionsDetail.ShowDetailTabs = false;
            this.gridVMain.OptionsView.ColumnAutoWidth = false;
            this.gridVMain.OptionsView.ShowAutoFilterRow = true;
            this.gridVMain.OptionsView.ShowFooter = true;
            this.gridVMain.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "方案号";
            this.gridColumn7.FieldName = "FANumber";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "状态";
            this.gridColumn9.FieldName = "State";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "方案名称";
            this.gridColumn10.FieldName = "Festival_Name";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "开始时间";
            this.gridColumn11.FieldName = "Festival_StDate";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 2;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "结束时间";
            this.gridColumn12.FieldName = "Festival_EdDate";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 3;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gcNav);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(660, 513);
            this.splitContainerControl1.SplitterPosition = 55;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gcNav
            // 
            this.gcNav.Controls.Add(this.txtFANumber);
            this.gcNav.Controls.Add(this.labelControl4);
            this.gcNav.Controls.Add(this.btnOk);
            this.gcNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcNav.Location = new System.Drawing.Point(0, 0);
            this.gcNav.Name = "gcNav";
            this.gcNav.Size = new System.Drawing.Size(660, 55);
            this.gcNav.TabIndex = 70;
            this.gcNav.TabStop = false;
            this.gcNav.Text = "查询条件";
            // 
            // txtFANumber
            // 
            this.txtFANumber.EditValue = "";
            this.txtFANumber.Location = new System.Drawing.Point(95, 17);
            this.txtFANumber.Name = "txtFANumber";
            this.txtFANumber.Size = new System.Drawing.Size(128, 21);
            this.txtFANumber.TabIndex = 426;
            this.txtFANumber.Tag = "";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(17, 24);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(72, 14);
            this.labelControl4.TabIndex = 425;
            this.labelControl4.Text = "营销方案号：";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(583, 19);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(58, 23);
            this.btnOk.TabIndex = 423;
            this.btnOk.Text = "查询&C";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.gridCMain);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.label1);
            this.splitContainerControl2.Panel2.Controls.Add(this.btnCancelMeg);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(660, 452);
            this.splitContainerControl2.SplitterPosition = 384;
            this.splitContainerControl2.TabIndex = 1;
            this.splitContainerControl2.Text = "splitContainerControl1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "选中自己发现有错误的方案号，点击";
            // 
            // btnCancelMeg
            // 
            this.btnCancelMeg.Location = new System.Drawing.Point(400, 10);
            this.btnCancelMeg.Name = "btnCancelMeg";
            this.btnCancelMeg.Size = new System.Drawing.Size(87, 50);
            this.btnCancelMeg.TabIndex = 0;
            this.btnCancelMeg.Text = "撤销发布";
            this.btnCancelMeg.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "款式编号";
            this.gridColumn3.FieldName = "PM_Id";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "修改记录";
            this.gridColumn4.FieldName = "Log_Rmk";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "款式ID";
            this.gridColumn1.FieldName = "PM_Id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "款式名称";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // frmProdPromotionMgrEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 513);
            this.Controls.Add(this.splitContainerControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmProdPromotionMgrEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "营销方案详细信息"; 
            ((System.ComponentModel.ISupportInitialize)(this.gridVCom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.gcNav.ResumeLayout(false);
            this.gcNav.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFANumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraGrid.GridControl gridCMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVCom;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVMain;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnCancelMeg;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.GroupBox gcNav;
        private DevExpress.XtraEditors.TextEdit txtFANumber;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;


    }
}