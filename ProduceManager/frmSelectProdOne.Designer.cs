namespace ProduceManager
{
    partial class frmSelectProdOne
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition3 = new DevExpress.XtraGrid.StyleFormatCondition();
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.splitContainerControl3 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridCMain = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutViewColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutViewColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.extPopupTree1 = new ExtendControl.ExtPopupTree();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtPNumber = new DevExpress.XtraEditors.TextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).BeginInit();
            this.splitContainerControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.extPopupTree1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl3
            // 
            this.splitContainerControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl3.Horizontal = false;
            this.splitContainerControl3.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl3.Name = "splitContainerControl3";
            this.splitContainerControl3.Panel1.Controls.Add(this.gridCMain);
            this.splitContainerControl3.Panel1.Text = "Panel1";
            this.splitContainerControl3.Panel2.Controls.Add(this.extPopupTree1);
            this.splitContainerControl3.Panel2.Controls.Add(this.labelControl4);
            this.splitContainerControl3.Panel2.Controls.Add(this.txtName);
            this.splitContainerControl3.Panel2.Controls.Add(this.labelControl3);
            this.splitContainerControl3.Panel2.Controls.Add(this.labelControl1);
            this.splitContainerControl3.Panel2.Controls.Add(this.labelControl2);
            this.splitContainerControl3.Panel2.Controls.Add(this.txtPNumber);
            this.splitContainerControl3.Panel2.Controls.Add(this.btnClose);
            this.splitContainerControl3.Panel2.Controls.Add(this.btnOk);
            this.splitContainerControl3.Panel2.Text = "Panel2";
            this.splitContainerControl3.Size = new System.Drawing.Size(667, 303);
            this.splitContainerControl3.SplitterPosition = 156;
            this.splitContainerControl3.TabIndex = 406;
            this.splitContainerControl3.Text = "splitContainerControl3";
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
            this.gridCMain.MainView = this.gridView1;
            this.gridCMain.Name = "gridCMain";
            this.gridCMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.gridCMain.Size = new System.Drawing.Size(667, 156);
            this.gridCMain.TabIndex = 62;
            this.gridCMain.UseEmbeddedNavigator = true;
            this.gridCMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.cardView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.layoutViewColumn2,
            this.layoutViewColumn3});
            styleFormatCondition3.Appearance.BackColor = System.Drawing.Color.DarkSalmon;
            styleFormatCondition3.Appearance.Options.UseBackColor = true;
            styleFormatCondition3.ApplyToRow = true;
            styleFormatCondition3.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition3.Value1 = false;
            this.gridView1.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition3});
            this.gridView1.GridControl = this.gridCMain;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never;
            // 
            // layoutViewColumn2
            // 
            this.layoutViewColumn2.Caption = "款式编号";
            this.layoutViewColumn2.FieldName = "PNumber";
            this.layoutViewColumn2.Name = "layoutViewColumn2";
            this.layoutViewColumn2.Visible = true;
            this.layoutViewColumn2.VisibleIndex = 0;
            // 
            // layoutViewColumn3
            // 
            this.layoutViewColumn3.Caption = "款式名称";
            this.layoutViewColumn3.FieldName = "Name";
            this.layoutViewColumn3.Name = "layoutViewColumn3";
            this.layoutViewColumn3.Visible = true;
            this.layoutViewColumn3.VisibleIndex = 1;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // cardView1
            // 
            this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn4});
            this.cardView1.FocusedCardTopFieldIndex = 0;
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.DarkSalmon;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = false;
            this.cardView1.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.cardView1.GridControl = this.gridCMain;
            this.cardView1.Name = "cardView1";
            this.cardView1.OptionsBehavior.FieldAutoHeight = true;
            this.cardView1.OptionsSelection.MultiSelect = true;
            this.cardView1.OptionsView.ShowCardCaption = false;
            this.cardView1.OptionsView.ShowQuickCustomizeButton = false;
            this.cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "选择";
            this.gridColumn3.ColumnEdit = this.repositoryItemCheckEdit2;
            this.gridColumn3.FieldName = "Checked";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "款式编号";
            this.gridColumn1.FieldName = "PNumber";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "款式名称";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "序号";
            this.gridColumn4.FieldName = "PM_Id";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // extPopupTree1
            // 
            this.extPopupTree1.DataSource = null;
            this.extPopupTree1.DisplayMember = null;
            this.extPopupTree1.Location = new System.Drawing.Point(277, 61);
            this.extPopupTree1.Name = "extPopupTree1";
            this.extPopupTree1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)}); 
            this.extPopupTree1.Properties.ShowPopupCloseButton = false;
            this.extPopupTree1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard; 
            this.extPopupTree1.Size = new System.Drawing.Size(149, 21);
            this.extPopupTree1.TabIndex = 411;
            this.extPopupTree1.ValueMember = null;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(200, 64);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 409;
            this.labelControl4.Text = "产品类别：";
            // 
            // txtName
            // 
            this.txtName.EditValue = "";
            this.txtName.Location = new System.Drawing.Point(277, 22);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(149, 21);
            this.txtName.TabIndex = 408;
            this.txtName.Tag = "OrdNumber";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(200, 26);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 407;
            this.labelControl3.Text = "款式名称：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(441, 50);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(187, 14);
            this.labelControl1.TabIndex = 406;
            this.labelControl1.Text = "*营销款号如不输入系统将自动生成";
            this.labelControl1.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(441, 26);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 405;
            this.labelControl2.Text = "营销款号：";
            this.labelControl2.Visible = false;
            // 
            // txtPNumber
            // 
            this.txtPNumber.EditValue = "";
            this.txtPNumber.Location = new System.Drawing.Point(518, 23);
            this.txtPNumber.Name = "txtPNumber";
            this.txtPNumber.Size = new System.Drawing.Size(137, 21);
            this.txtPNumber.TabIndex = 404;
            this.txtPNumber.Tag = "OrdNumber";
            this.txtPNumber.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(359, 101);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 27);
            this.btnClose.TabIndex = 403;
            this.btnClose.Text = "取消";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(277, 101);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(68, 27);
            this.btnOk.TabIndex = 402;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmSelectProdOne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 303);
            this.Controls.Add(this.splitContainerControl3);
            this.Name = "frmSelectProdOne";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "单件挑款";
            this.Load += new System.EventHandler(this.frmSelectProdOne_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl3)).EndInit();
            this.splitContainerControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.extPopupTree1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPNumber.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtPNumber;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraGrid.GridControl gridCMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn layoutViewColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn layoutViewColumn3;
        private DevExpress.XtraGrid.Views.Card.CardView cardView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private ExtendControl.ExtPopupTree extPopupTree1;

    }
}