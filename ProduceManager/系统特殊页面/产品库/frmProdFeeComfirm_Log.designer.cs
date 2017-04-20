namespace ProduceManager
{
    partial class frmProdFeeComfirm_Log
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
            this.gcNav = new System.Windows.Forms.GroupBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dtEdDay = new DevExpress.XtraEditors.DateEdit();
            this.dtStDay = new DevExpress.XtraEditors.DateEdit();
            this.txtPNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.gridCMain = new DevExpress.XtraGrid.GridControl();
            this.gridVMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.gcNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtEdDay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEdDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStDay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gcNav);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridCMain);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(660, 566);
            this.splitContainerControl1.SplitterPosition = 55;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // gcNav
            // 
            this.gcNav.Controls.Add(this.labelControl1);
            this.gcNav.Controls.Add(this.dtEdDay);
            this.gcNav.Controls.Add(this.dtStDay);
            this.gcNav.Controls.Add(this.txtPNumber);
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
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(213, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 429;
            this.labelControl1.Text = "操作日期：";
            // 
            // dtEdDay
            // 
            this.dtEdDay.EditValue = null;
            this.dtEdDay.Location = new System.Drawing.Point(385, 21);
            this.dtEdDay.Name = "dtEdDay";
            this.dtEdDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEdDay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtEdDay.Size = new System.Drawing.Size(100, 21);
            this.dtEdDay.TabIndex = 428;
            // 
            // dtStDay
            // 
            this.dtStDay.EditValue = null;
            this.dtStDay.Location = new System.Drawing.Point(279, 21);
            this.dtStDay.Name = "dtStDay";
            this.dtStDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStDay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtStDay.Size = new System.Drawing.Size(100, 21);
            this.dtStDay.TabIndex = 427;
            // 
            // txtPNumber
            // 
            this.txtPNumber.EditValue = "";
            this.txtPNumber.Location = new System.Drawing.Point(74, 20);
            this.txtPNumber.Name = "txtPNumber";
            this.txtPNumber.Size = new System.Drawing.Size(128, 21);
            this.txtPNumber.TabIndex = 426;
            this.txtPNumber.Tag = "";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(17, 24);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 425;
            this.labelControl4.Text = "款式编号：";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(512, 18);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(58, 23);
            this.btnOk.TabIndex = 423;
            this.btnOk.Text = "查询&C";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
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
            this.gridCMain.Size = new System.Drawing.Size(660, 505);
            this.gridCMain.TabIndex = 58;
            this.gridCMain.UseEmbeddedNavigator = true;
            this.gridCMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVMain});
            // 
            // gridVMain
            // 
            this.gridVMain.ChildGridLevelName = "ChildGrid";
            this.gridVMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn8,
            this.gridColumn9});
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
            this.gridColumn7.Caption = "款号";
            this.gridColumn7.FieldName = "PNumber";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 0;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "款名";
            this.gridColumn5.FieldName = "Bus_Name";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "同步前工费";
            this.gridColumn6.FieldName = "Bus_ComfirmFee";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "同步后工费";
            this.gridColumn8.FieldName = "ResultFee";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "操作人";
            this.gridColumn9.FieldName = "ComfirmUserName";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 4;
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
            // frmProdFeeComfirm_Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 566);
            this.Controls.Add(this.splitContainerControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmProdFeeComfirm_Log";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "操作历史记录";
            this.Load += new System.EventHandler(this.frmProdFeeComfirm_Log_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.gcNav.ResumeLayout(false);
            this.gcNav.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtEdDay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEdDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStDay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.GroupBox gcNav;
        private DevExpress.XtraEditors.TextEdit txtPNumber;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraGrid.GridControl gridCMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVMain;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.DateEdit dtEdDay;
        private DevExpress.XtraEditors.DateEdit dtStDay;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;


    }
}