namespace ProduceManager
{
    partial class frmBseContrlEditTabel
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.dplVisible = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnOkT = new DevExpress.XtraEditors.SimpleButton();
            this.dplTypeT = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtGroupNameT = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtClassT = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnQueryT = new DevExpress.XtraEditors.SimpleButton();
            this.txtNumberT = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.gridCTabel = new DevExpress.XtraGrid.GridControl();
            this.gridVTabel = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dplVisible.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplTypeT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupNameT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClassT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCTabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVTabel)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.CaptionLocation = DevExpress.Utils.Locations.Left;
            this.groupControl2.Controls.Add(this.dplVisible);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.btnOkT);
            this.groupControl2.Controls.Add(this.dplTypeT);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Controls.Add(this.txtGroupNameT);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.txtClassT);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.btnQueryT);
            this.groupControl2.Controls.Add(this.txtNumberT);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(962, 40);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "查询";
            // 
            // dplVisible
            // 
            this.dplVisible.EditValue = "0";
            this.dplVisible.Location = new System.Drawing.Point(268, 10);
            this.dplVisible.Name = "dplVisible";
            this.dplVisible.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dplVisible.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("是", "1", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("否", "0", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("所有", "-1", -1)});
            this.dplVisible.Size = new System.Drawing.Size(46, 21);
            this.dplVisible.TabIndex = 19;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(244, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "隐藏";
            // 
            // btnOkT
            // 
            this.btnOkT.Location = new System.Drawing.Point(896, 9);
            this.btnOkT.Name = "btnOkT";
            this.btnOkT.Size = new System.Drawing.Size(57, 23);
            this.btnOkT.TabIndex = 17;
            this.btnOkT.Text = "确定完成";
            this.btnOkT.Click += new System.EventHandler(this.btn_Click);
            // 
            // dplTypeT
            // 
            this.dplTypeT.Location = new System.Drawing.Point(810, 10);
            this.dplTypeT.Name = "dplTypeT";
            this.dplTypeT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dplTypeT.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("GridView", "1", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("编辑面板", "2", -1)});
            this.dplTypeT.Size = new System.Drawing.Size(80, 21);
            this.dplTypeT.TabIndex = 16;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl8.Appearance.Options.UseForeColor = true;
            this.labelControl8.Location = new System.Drawing.Point(786, 13);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(24, 14);
            this.labelControl8.TabIndex = 15;
            this.labelControl8.Text = "类型";
            // 
            // txtGroupNameT
            // 
            this.txtGroupNameT.Location = new System.Drawing.Point(672, 10);
            this.txtGroupNameT.Name = "txtGroupNameT";
            this.txtGroupNameT.Size = new System.Drawing.Size(109, 21);
            this.txtGroupNameT.TabIndex = 14;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl7.Appearance.Options.UseForeColor = true;
            this.labelControl7.Location = new System.Drawing.Point(624, 13);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(48, 14);
            this.labelControl7.TabIndex = 13;
            this.labelControl7.Text = "父控件名";
            // 
            // txtClassT
            // 
            this.txtClassT.Location = new System.Drawing.Point(458, 10);
            this.txtClassT.Name = "txtClassT";
            this.txtClassT.Size = new System.Drawing.Size(160, 21);
            this.txtClassT.TabIndex = 12;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Location = new System.Drawing.Point(410, 13);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "页面类名";
            // 
            // btnQueryT
            // 
            this.btnQueryT.Location = new System.Drawing.Point(320, 9);
            this.btnQueryT.Name = "btnQueryT";
            this.btnQueryT.Size = new System.Drawing.Size(40, 23);
            this.btnQueryT.TabIndex = 4;
            this.btnQueryT.Text = "查询";
            this.btnQueryT.Click += new System.EventHandler(this.btn_Click);
            // 
            // txtNumberT
            // 
            this.txtNumberT.Location = new System.Drawing.Point(71, 10);
            this.txtNumberT.Name = "txtNumberT";
            this.txtNumberT.Size = new System.Drawing.Size(168, 21);
            this.txtNumberT.TabIndex = 2;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(23, 13);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 1;
            this.labelControl5.Text = "模糊查询";
            // 
            // gridCTabel
            // 
            this.gridCTabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCTabel.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCTabel.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCTabel.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCTabel.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCTabel.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCTabel.Location = new System.Drawing.Point(0, 40);
            this.gridCTabel.MainView = this.gridVTabel;
            this.gridCTabel.Name = "gridCTabel";
            this.gridCTabel.Size = new System.Drawing.Size(962, 666);
            this.gridCTabel.TabIndex = 61;
            this.gridCTabel.UseEmbeddedNavigator = true;
            this.gridCTabel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVTabel});
            // 
            // gridVTabel
            // 
            this.gridVTabel.GridControl = this.gridCTabel;
            this.gridVTabel.Name = "gridVTabel";
            this.gridVTabel.OptionsDetail.ShowDetailTabs = false;
            this.gridVTabel.OptionsSelection.MultiSelect = true;
            this.gridVTabel.OptionsView.ColumnAutoWidth = false;
            this.gridVTabel.OptionsView.ShowAutoFilterRow = true;
            this.gridVTabel.OptionsView.ShowGroupPanel = false;
            // 
            // frmBseContrlEditTabel
            // 
            this.AcceptButton = this.btnQueryT;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 706);
            this.Controls.Add(this.gridCTabel);
            this.Controls.Add(this.groupControl2);
            this.Name = "frmBseContrlEditTabel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "根据表字段维护页面控件";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBsuSetQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dplVisible.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplTypeT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupNameT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClassT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumberT.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCTabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVTabel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnOkT;
        private DevExpress.XtraEditors.ImageComboBoxEdit dplTypeT;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtGroupNameT;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtClassT;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton btnQueryT;
        private DevExpress.XtraEditors.TextEdit txtNumberT;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraGrid.GridControl gridCTabel;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVTabel;
        private DevExpress.XtraEditors.ImageComboBoxEdit dplVisible;
        private DevExpress.XtraEditors.LabelControl labelControl1;



    }
}