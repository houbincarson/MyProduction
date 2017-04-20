namespace ProduceManager
{
    partial class frmProdMegerPNumber
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnMoreCondition = new DevExpress.XtraEditors.SimpleButton();
            this.dtEndDay = new DevExpress.XtraEditors.DateEdit();
            this.dtStDay = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtPNumber = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtMergePNumber = new DevExpress.XtraEditors.TextEdit();
            this.lblFee = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSet = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStDay.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMergePNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1041, 539);
            this.splitContainerControl1.SplitterPosition = 95;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.btnMoreCondition);
            this.groupBox1.Controls.Add(this.dtEndDay);
            this.groupBox1.Controls.Add(this.dtStDay);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.txtPNumber);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1041, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(515, 26);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(172, 14);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "注:可输入多个款号，用逗号隔开";
            // 
            // btnMoreCondition
            // 
            this.btnMoreCondition.Location = new System.Drawing.Point(772, 19);
            this.btnMoreCondition.Name = "btnMoreCondition";
            this.btnMoreCondition.Size = new System.Drawing.Size(107, 27);
            this.btnMoreCondition.TabIndex = 15;
            this.btnMoreCondition.Text = "更多筛选条件";
            this.btnMoreCondition.Click += new System.EventHandler(this.btnMoreCondition_Click);
            // 
            // dtEndDay
            // 
            this.dtEndDay.EditValue = null;
            this.dtEndDay.Location = new System.Drawing.Point(243, 54);
            this.dtEndDay.Name = "dtEndDay";
            this.dtEndDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtEndDay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtEndDay.Size = new System.Drawing.Size(117, 21);
            this.dtEndDay.TabIndex = 14;
            // 
            // dtStDay
            // 
            this.dtStDay.EditValue = null;
            this.dtStDay.Location = new System.Drawing.Point(112, 54);
            this.dtStDay.Name = "dtStDay";
            this.dtStDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStDay.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtStDay.Size = new System.Drawing.Size(117, 21);
            this.dtStDay.TabIndex = 13;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(14, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 14);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "建款起止日期：";
            // 
            // txtPNumber
            // 
            this.txtPNumber.Location = new System.Drawing.Point(112, 23);
            this.txtPNumber.Name = "txtPNumber";
            this.txtPNumber.Size = new System.Drawing.Size(397, 21);
            this.txtPNumber.TabIndex = 11;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(38, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "款式编号：";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(885, 18);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(87, 27);
            this.btnQuery.TabIndex = 9;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.gridControl1);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.txtMergePNumber);
            this.splitContainerControl2.Panel2.Controls.Add(this.lblFee);
            this.splitContainerControl2.Panel2.Controls.Add(this.btnCancel);
            this.splitContainerControl2.Panel2.Controls.Add(this.btnSet);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1041, 438);
            this.splitContainerControl2.SplitterPosition = 350;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1041, 350);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "序号";
            this.gridColumn4.FieldName = "PM_Id";
            this.gridColumn4.Name = "gridColumn4";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "款式名称";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "成色";
            this.gridColumn3.FieldName = "Material";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "克销售工费";
            this.gridColumn5.FieldName = "WeightFee";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 6;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "件销售工费";
            this.gridColumn6.FieldName = "AmoutFee";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 7;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "类别";
            this.gridColumn7.FieldName = "Pc_Id";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "工厂款号";
            this.gridColumn8.FieldName = "PNumber";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "营销款号";
            this.gridColumn9.FieldName = "Bus_PNumber";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 1;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "合并款号";
            this.gridColumn10.FieldName = "MergePNumber";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 2;
            // 
            // txtMergePNumber
            // 
            this.txtMergePNumber.Location = new System.Drawing.Point(503, 16);
            this.txtMergePNumber.Name = "txtMergePNumber";
            this.txtMergePNumber.Properties.Mask.EditMask = "\\d+(\\R.\\d{0,2})?";
            this.txtMergePNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtMergePNumber.Size = new System.Drawing.Size(139, 21);
            this.txtMergePNumber.TabIndex = 13;
            // 
            // lblFee
            // 
            this.lblFee.Location = new System.Drawing.Point(413, 19);
            this.lblFee.Name = "lblFee";
            this.lblFee.Size = new System.Drawing.Size(84, 14);
            this.lblFee.TabIndex = 12;
            this.lblFee.Text = "输入合并款号：";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(555, 48);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(413, 48);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(87, 27);
            this.btnSet.TabIndex = 0;
            this.btnSet.Text = "确定合并";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // frmProdMegerPNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 539);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmProdMegerPNumber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "合并款管理";
            this.Load += new System.EventHandler(this.frmProdMegerPNumber_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStDay.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMergePNumber.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSet;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnMoreCondition;
        private DevExpress.XtraEditors.DateEdit dtEndDay;
        private DevExpress.XtraEditors.DateEdit dtStDay;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtPNumber;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.TextEdit txtMergePNumber;
        private DevExpress.XtraEditors.LabelControl lblFee;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}