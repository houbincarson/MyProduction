namespace ProduceManager
{
    partial class frmExcelShow
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.gridCExcel = new DevExpress.XtraGrid.GridControl();
            this.gridVExcel = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridCExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(502, 21);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 43);
            this.btnCancel.TabIndex = 99;
            this.btnCancel.Text = "取消导入&C";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(300, 21);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(120, 43);
            this.btnOk.TabIndex = 98;
            this.btnOk.Text = "确定导入&S";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gridCExcel
            // 
            this.gridCExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCExcel.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCExcel.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCExcel.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCExcel.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCExcel.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCExcel.Location = new System.Drawing.Point(0, 0);
            this.gridCExcel.MainView = this.gridVExcel;
            this.gridCExcel.Name = "gridCExcel";
            this.gridCExcel.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridCExcel.Size = new System.Drawing.Size(962, 429);
            this.gridCExcel.TabIndex = 301;
            this.gridCExcel.UseEmbeddedNavigator = true;
            this.gridCExcel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVExcel});
            // 
            // gridVExcel
            // 
            this.gridVExcel.GridControl = this.gridCExcel;
            this.gridVExcel.Name = "gridVExcel";
            this.gridVExcel.OptionsBehavior.Editable = false;
            this.gridVExcel.OptionsSelection.MultiSelect = true;
            this.gridVExcel.OptionsView.ColumnAutoWidth = false;
            this.gridVExcel.OptionsView.ShowFooter = true;
            this.gridVExcel.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridCExcel);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.btnOk);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(962, 566);
            this.splitContainerControl1.SplitterPosition = 131;
            this.splitContainerControl1.TabIndex = 302;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // frmExcelShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 566);
            this.Controls.Add(this.splitContainerControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExcelShow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel确定导入";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSelectShop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridCExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridCExcel;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVExcel;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    }
}