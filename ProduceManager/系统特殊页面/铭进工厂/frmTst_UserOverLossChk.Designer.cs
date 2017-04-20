namespace ProduceManager
{
    partial class frmTst_UserOverLossChk
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
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridCDeli = new DevExpress.XtraGrid.GridControl();
            this.gridVDeli = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn35 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCDeli)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVDeli)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.groupControl2);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            this.splitContainerControl2.Size = new System.Drawing.Size(782, 546);
            this.splitContainerControl2.SplitterPosition = 209;
            this.splitContainerControl2.TabIndex = 603;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridCDeli);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(782, 546);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "产品信息";
            // 
            // gridCDeli
            // 
            this.gridCDeli.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCDeli.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCDeli.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCDeli.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCDeli.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCDeli.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCDeli.Location = new System.Drawing.Point(2, 2);
            this.gridCDeli.MainView = this.gridVDeli;
            this.gridCDeli.Name = "gridCDeli";
            this.gridCDeli.Size = new System.Drawing.Size(778, 542);
            this.gridCDeli.TabIndex = 117;
            this.gridCDeli.UseEmbeddedNavigator = true;
            this.gridCDeli.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVDeli});
            // 
            // gridVDeli
            // 
            this.gridVDeli.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn10,
            this.gridColumn23,
            this.gridColumn35,
            this.gridColumn2,
            this.gridColumn4});
            this.gridVDeli.GridControl = this.gridCDeli;
            this.gridVDeli.Name = "gridVDeli";
            this.gridVDeli.OptionsView.ColumnAutoWidth = false;
            this.gridVDeli.OptionsView.ShowAutoFilterRow = true;
            this.gridVDeli.OptionsView.ShowFooter = true;
            this.gridVDeli.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "交货时间";
            this.gridColumn3.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn3.FieldName = "Deli_Dt";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 60;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "交货序号";
            this.gridColumn10.FieldName = "DIndex";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.ReadOnly = true;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 60;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "损耗分类";
            this.gridColumn23.FieldName = "ULossRepType";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.OptionsColumn.AllowEdit = false;
            this.gridColumn23.OptionsColumn.ReadOnly = true;
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 2;
            this.gridColumn23.Width = 60;
            // 
            // gridColumn35
            // 
            this.gridColumn35.Caption = "交货净重";
            this.gridColumn35.FieldName = "NetWeight";
            this.gridColumn35.Name = "gridColumn35";
            this.gridColumn35.OptionsColumn.ReadOnly = true;
            this.gridColumn35.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn35.Visible = true;
            this.gridColumn35.VisibleIndex = 4;
            this.gridColumn35.Width = 60;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "标准损耗率";
            this.gridColumn2.FieldName = "ProdLoss";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 3;
            this.gridColumn2.Width = 72;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "允许损耗";
            this.gridColumn4.FieldName = "AllLoss";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 60;
            // 
            // frmTst_UserOverLossChk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 546);
            this.Controls.Add(this.splitContainerControl2);
            this.Name = "frmTst_UserOverLossChk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "员工损耗对账";
            this.Load += new System.EventHandler(this.frmEmpEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCDeli)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVDeli)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridCDeli;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVDeli;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn35;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}