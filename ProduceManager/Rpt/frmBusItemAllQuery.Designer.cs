namespace ProduceManager
{
    partial class frmBusItemAllQuery
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.gcTst = new System.Windows.Forms.GroupBox();
            this.gcQuery = new System.Windows.Forms.GroupBox();
            this.gridCInfo = new DevExpress.XtraGrid.GridControl();
            this.gridVInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repImg = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.gcTst.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repImg)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(382, 1);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(50, 23);
            this.btnExcel.TabIndex = 457;
            this.btnExcel.Text = "导出&E";
            this.btnExcel.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(332, 1);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(50, 23);
            this.btnQuery.TabIndex = 95;
            this.btnQuery.Text = "查询&F";
            this.btnQuery.Click += new System.EventHandler(this.btn_Click);
            // 
            // gcTst
            // 
            this.gcTst.Controls.Add(this.btnExcel);
            this.gcTst.Controls.Add(this.btnQuery);
            this.gcTst.Controls.Add(this.gcQuery);
            this.gcTst.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcTst.Location = new System.Drawing.Point(0, 0);
            this.gcTst.Name = "gcTst";
            this.gcTst.Size = new System.Drawing.Size(972, 71);
            this.gcTst.TabIndex = 605;
            this.gcTst.TabStop = false;
            this.gcTst.Text = "查询条件";
            // 
            // gcQuery
            // 
            this.gcQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcQuery.Location = new System.Drawing.Point(3, 18);
            this.gcQuery.Name = "gcQuery";
            this.gcQuery.Size = new System.Drawing.Size(966, 50);
            this.gcQuery.TabIndex = 606;
            this.gcQuery.TabStop = false;
            // 
            // gridCInfo
            // 
            this.gridCInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCInfo.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCInfo.Location = new System.Drawing.Point(0, 71);
            this.gridCInfo.MainView = this.gridVInfo;
            this.gridCInfo.Name = "gridCInfo";
            this.gridCInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repImg});
            this.gridCInfo.Size = new System.Drawing.Size(972, 495);
            this.gridCInfo.TabIndex = 606;
            this.gridCInfo.UseEmbeddedNavigator = true;
            this.gridCInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVInfo});
            // 
            // gridVInfo
            // 
            this.gridVInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridVInfo.GridControl = this.gridCInfo;
            this.gridVInfo.Name = "gridVInfo";
            this.gridVInfo.OptionsSelection.MultiSelect = true;
            this.gridVInfo.OptionsView.ColumnAutoWidth = false;
            this.gridVInfo.OptionsView.ShowAutoFilterRow = true;
            this.gridVInfo.OptionsView.ShowFooter = true;
            this.gridVInfo.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "图片";
            this.gridColumn1.ColumnEdit = this.repImg;
            this.gridColumn1.FieldName = "Icon";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowMove = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 50;
            // 
            // repImg
            // 
            this.repImg.AutoHeight = false;
            this.repImg.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null)});
            this.repImg.Name = "repImg";
            this.repImg.PopupFormMinSize = new System.Drawing.Size(450, 350);
            this.repImg.ReadOnly = true;
            this.repImg.Popup += new System.EventHandler(this.repImg_Popup);
            // 
            // frmBusItemAllQuery
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 566);
            this.Controls.Add(this.gridCInfo);
            this.Controls.Add(this.gcTst);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBusItemAllQuery";
            this.Text = "业务明细查询";
            this.gcTst.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private System.Windows.Forms.GroupBox gcTst;
        private System.Windows.Forms.GroupBox gcQuery;
        private DevExpress.XtraGrid.GridControl gridCInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit repImg;
    }
}