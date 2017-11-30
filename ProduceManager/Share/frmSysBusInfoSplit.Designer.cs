namespace ProduceManager
{
    partial class frmSysBusInfoSplit
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnSplit = new DevExpress.XtraEditors.SimpleButton();
            this.txtNum = new DevExpress.XtraEditors.TextEdit();
            this.gridCInfo = new DevExpress.XtraGrid.GridControl();
            this.gridVInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridCSplit = new DevExpress.XtraGrid.GridControl();
            this.gridVSplit = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCSplit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVSplit)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(506, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 43);
            this.btnCancel.TabIndex = 99;
            this.btnCancel.Text = "取消&C";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(354, 13);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(120, 43);
            this.btnOk.TabIndex = 98;
            this.btnOk.Text = "确定拆分&S";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.btnSplit);
            this.splitContainerControl1.Panel1.Controls.Add(this.txtNum);
            this.splitContainerControl1.Panel1.Controls.Add(this.gridCInfo);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(972, 473);
            this.splitContainerControl1.SplitterPosition = 138;
            this.splitContainerControl1.TabIndex = 301;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // btnSplit
            // 
            this.btnSplit.Location = new System.Drawing.Point(446, 4);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(60, 22);
            this.btnSplit.TabIndex = 101;
            this.btnSplit.Text = "拆分";
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // txtNum
            // 
            this.txtNum.EditValue = "2";
            this.txtNum.Location = new System.Drawing.Point(399, 5);
            this.txtNum.Name = "txtNum";
            this.txtNum.Properties.Mask.EditMask = "d";
            this.txtNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtNum.Size = new System.Drawing.Size(41, 21);
            this.txtNum.TabIndex = 100;
            // 
            // gridCInfo
            // 
            this.gridCInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCInfo.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCInfo.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCInfo.Location = new System.Drawing.Point(0, 0);
            this.gridCInfo.MainView = this.gridVInfo;
            this.gridCInfo.Name = "gridCInfo";
            this.gridCInfo.Size = new System.Drawing.Size(972, 138);
            this.gridCInfo.TabIndex = 61;
            this.gridCInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVInfo});
            // 
            // gridVInfo
            // 
            this.gridVInfo.GridControl = this.gridCInfo;
            this.gridVInfo.Name = "gridVInfo";
            this.gridVInfo.OptionsSelection.MultiSelect = true;
            this.gridVInfo.OptionsView.ColumnAutoWidth = false;
            this.gridVInfo.OptionsView.ShowGroupPanel = false;
            this.gridVInfo.OptionsView.ShowViewCaption = true;
            this.gridVInfo.ViewCaption = " ";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.gridCSplit);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.btnCancel);
            this.splitContainerControl2.Panel2.Controls.Add(this.btnOk);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(972, 329);
            this.splitContainerControl2.SplitterPosition = 91;
            this.splitContainerControl2.TabIndex = 303;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // gridCSplit
            // 
            this.gridCSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCSplit.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCSplit.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCSplit.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCSplit.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCSplit.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCSplit.Location = new System.Drawing.Point(0, 0);
            this.gridCSplit.MainView = this.gridVSplit;
            this.gridCSplit.Name = "gridCSplit";
            this.gridCSplit.Size = new System.Drawing.Size(972, 232);
            this.gridCSplit.TabIndex = 59;
            this.gridCSplit.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVSplit});
            // 
            // gridVSplit
            // 
            this.gridVSplit.GridControl = this.gridCSplit;
            this.gridVSplit.Name = "gridVSplit";
            this.gridVSplit.OptionsSelection.MultiSelect = true;
            this.gridVSplit.OptionsView.ColumnAutoWidth = false;
            this.gridVSplit.OptionsView.ShowFooter = true;
            this.gridVSplit.OptionsView.ShowGroupPanel = false;
            // 
            // frmSysBusInfoSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 473);
            this.Controls.Add(this.splitContainerControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSysBusInfoSplit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "加工单拆分";
            this.Load += new System.EventHandler(this.frmSelectShop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCSplit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVSplit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gridCSplit;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVSplit;
        private DevExpress.XtraGrid.GridControl gridCInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVInfo;
        private DevExpress.XtraEditors.SimpleButton btnSplit;
        private DevExpress.XtraEditors.TextEdit txtNum;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
    }
}