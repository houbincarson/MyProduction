namespace ProduceManager
{
    partial class frmBseContrlEditSrc
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
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.btnOkS = new DevExpress.XtraEditors.SimpleButton();
            this.txtGroupNameS = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtClassS = new DevExpress.XtraEditors.TextEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.btnQueryS = new DevExpress.XtraEditors.SimpleButton();
            this.txtNumberS = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.gridCSrc = new DevExpress.XtraGrid.GridControl();
            this.gridVSrc = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupNameS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClassS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCSrc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVSrc)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl3
            // 
            this.groupControl3.CaptionLocation = DevExpress.Utils.Locations.Left;
            this.groupControl3.Controls.Add(this.btnOkS);
            this.groupControl3.Controls.Add(this.txtGroupNameS);
            this.groupControl3.Controls.Add(this.labelControl10);
            this.groupControl3.Controls.Add(this.txtClassS);
            this.groupControl3.Controls.Add(this.labelControl11);
            this.groupControl3.Controls.Add(this.btnQueryS);
            this.groupControl3.Controls.Add(this.txtNumberS);
            this.groupControl3.Controls.Add(this.labelControl12);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(962, 40);
            this.groupControl3.TabIndex = 3;
            this.groupControl3.Text = "查询";
            // 
            // btnOkS
            // 
            this.btnOkS.Location = new System.Drawing.Point(729, 9);
            this.btnOkS.Name = "btnOkS";
            this.btnOkS.Size = new System.Drawing.Size(57, 23);
            this.btnOkS.TabIndex = 17;
            this.btnOkS.Text = "确定完成";
            this.btnOkS.Click += new System.EventHandler(this.btn_Click);
            // 
            // txtGroupNameS
            // 
            this.txtGroupNameS.Location = new System.Drawing.Point(596, 10);
            this.txtGroupNameS.Name = "txtGroupNameS";
            this.txtGroupNameS.Size = new System.Drawing.Size(120, 21);
            this.txtGroupNameS.TabIndex = 14;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl10.Appearance.Options.UseForeColor = true;
            this.labelControl10.Location = new System.Drawing.Point(548, 13);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(48, 14);
            this.labelControl10.TabIndex = 13;
            this.labelControl10.Text = "父控件名";
            // 
            // txtClassS
            // 
            this.txtClassS.Location = new System.Drawing.Point(382, 10);
            this.txtClassS.Name = "txtClassS";
            this.txtClassS.Size = new System.Drawing.Size(160, 21);
            this.txtClassS.TabIndex = 12;
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl11.Appearance.Options.UseForeColor = true;
            this.labelControl11.Location = new System.Drawing.Point(309, 13);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(72, 14);
            this.labelControl11.TabIndex = 11;
            this.labelControl11.Text = "目标页面类名";
            // 
            // btnQueryS
            // 
            this.btnQueryS.Location = new System.Drawing.Point(244, 9);
            this.btnQueryS.Name = "btnQueryS";
            this.btnQueryS.Size = new System.Drawing.Size(40, 23);
            this.btnQueryS.TabIndex = 4;
            this.btnQueryS.Text = "查询";
            this.btnQueryS.Click += new System.EventHandler(this.btn_Click);
            // 
            // txtNumberS
            // 
            this.txtNumberS.Location = new System.Drawing.Point(71, 10);
            this.txtNumberS.Name = "txtNumberS";
            this.txtNumberS.Size = new System.Drawing.Size(168, 21);
            this.txtNumberS.TabIndex = 2;
            // 
            // labelControl12
            // 
            this.labelControl12.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl12.Appearance.Options.UseForeColor = true;
            this.labelControl12.Location = new System.Drawing.Point(23, 13);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(48, 14);
            this.labelControl12.TabIndex = 1;
            this.labelControl12.Text = "模糊查询";
            // 
            // gridCSrc
            // 
            this.gridCSrc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCSrc.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCSrc.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCSrc.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCSrc.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCSrc.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCSrc.Location = new System.Drawing.Point(0, 40);
            this.gridCSrc.MainView = this.gridVSrc;
            this.gridCSrc.Name = "gridCSrc";
            this.gridCSrc.Size = new System.Drawing.Size(962, 666);
            this.gridCSrc.TabIndex = 62;
            this.gridCSrc.UseEmbeddedNavigator = true;
            this.gridCSrc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVSrc});
            // 
            // gridVSrc
            // 
            this.gridVSrc.GridControl = this.gridCSrc;
            this.gridVSrc.Name = "gridVSrc";
            this.gridVSrc.OptionsDetail.ShowDetailTabs = false;
            this.gridVSrc.OptionsSelection.MultiSelect = true;
            this.gridVSrc.OptionsView.ColumnAutoWidth = false;
            this.gridVSrc.OptionsView.ShowAutoFilterRow = true;
            this.gridVSrc.OptionsView.ShowGroupPanel = false;
            // 
            // frmBseContrlEditSrc
            // 
            this.AcceptButton = this.btnQueryS;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 706);
            this.Controls.Add(this.gridCSrc);
            this.Controls.Add(this.groupControl3);
            this.Name = "frmBseContrlEditSrc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通过复制维护页面控件";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBsuSetQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupNameS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClassS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCSrc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVSrc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton btnOkS;
        private DevExpress.XtraEditors.TextEdit txtGroupNameS;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit txtClassS;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.SimpleButton btnQueryS;
        private DevExpress.XtraEditors.TextEdit txtNumberS;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraGrid.GridControl gridCSrc;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVSrc;




    }
}