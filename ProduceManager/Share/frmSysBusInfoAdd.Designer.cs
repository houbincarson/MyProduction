namespace ProduceManager
{
    partial class frmSysBusInfoAdd
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnSetAll = new DevExpress.XtraEditors.SimpleButton();
            this.gridCInfo = new DevExpress.XtraGrid.GridControl();
            this.gridVInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.picEdit = new DevExpress.XtraEditors.PictureEdit();
            this.gcQuery = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Blue;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.btnCancel);
            this.groupControl1.Controls.Add(this.btnOk);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.Location = new System.Drawing.Point(0, 394);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(972, 172);
            this.groupControl1.TabIndex = 300;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(498, 26);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 43);
            this.btnCancel.TabIndex = 99;
            this.btnCancel.Text = "取消&C";
            this.btnCancel.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(328, 26);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(120, 43);
            this.btnOk.TabIndex = 98;
            this.btnOk.Text = "确定&S";
            this.btnOk.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(663, 61);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(70, 25);
            this.btnClearAll.TabIndex = 308;
            this.btnClearAll.Text = "重置数量";
            this.btnClearAll.Visible = false;
            this.btnClearAll.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnSetAll
            // 
            this.btnSetAll.Location = new System.Drawing.Point(498, 61);
            this.btnSetAll.Name = "btnSetAll";
            this.btnSetAll.Size = new System.Drawing.Size(70, 25);
            this.btnSetAll.TabIndex = 307;
            this.btnSetAll.Text = "一次发齐";
            this.btnSetAll.Visible = false;
            this.btnSetAll.Click += new System.EventHandler(this.btn_Click);
            // 
            // gridCInfo
            // 
            this.gridCInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCInfo.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCInfo.Location = new System.Drawing.Point(0, 61);
            this.gridCInfo.MainView = this.gridVInfo;
            this.gridCInfo.Name = "gridCInfo";
            this.gridCInfo.Size = new System.Drawing.Size(972, 333);
            this.gridCInfo.TabIndex = 301;
            this.gridCInfo.UseEmbeddedNavigator = true;
            this.gridCInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVInfo});
            // 
            // gridVInfo
            // 
            this.gridVInfo.GridControl = this.gridCInfo;
            this.gridVInfo.Name = "gridVInfo";
            this.gridVInfo.OptionsSelection.MultiSelect = true;
            this.gridVInfo.OptionsView.ColumnAutoWidth = false;
            this.gridVInfo.OptionsView.ShowAutoFilterRow = true;
            this.gridVInfo.OptionsView.ShowFooter = true;
            this.gridVInfo.OptionsView.ShowGroupPanel = false;
            this.gridVInfo.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridVInfo_CellValueChanging);
            // 
            // picEdit
            // 
            this.picEdit.Location = new System.Drawing.Point(385, 12);
            this.picEdit.Name = "picEdit";
            this.picEdit.Properties.PictureStoreMode = DevExpress.XtraEditors.Controls.PictureStoreMode.ByteArray;
            this.picEdit.Properties.ReadOnly = true;
            this.picEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.picEdit.Size = new System.Drawing.Size(450, 350);
            this.picEdit.TabIndex = 303;
            this.picEdit.Visible = false;
            // 
            // gcQuery
            // 
            this.gcQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcQuery.Location = new System.Drawing.Point(0, 0);
            this.gcQuery.Name = "gcQuery";
            this.gcQuery.Size = new System.Drawing.Size(972, 61);
            this.gcQuery.TabIndex = 605;
            this.gcQuery.TabStop = false;
            this.gcQuery.Text = "查询条件";
            // 
            // frmSysBusInfoAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 566);
            this.Controls.Add(this.btnSetAll);
            this.Controls.Add(this.btnClearAll);
            this.Controls.Add(this.picEdit);
            this.Controls.Add(this.gridCInfo);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.gcQuery);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSysBusInfoAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增套装产品";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSelectShop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraGrid.GridControl gridCInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVInfo;
        private DevExpress.XtraEditors.PictureEdit picEdit;
        private DevExpress.XtraEditors.SimpleButton btnClearAll;
        private DevExpress.XtraEditors.SimpleButton btnSetAll;
        private System.Windows.Forms.GroupBox gcQuery;
    }
}