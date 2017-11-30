namespace ProduceManager
{
    partial class frmBseUserEdit
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
            DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            this.gcState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridCMain = new DevExpress.XtraGrid.GridControl();
            this.gridVMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcQuery = new System.Windows.Forms.GroupBox();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.rdNoState = new System.Windows.Forms.RadioButton();
            this.rdState = new System.Windows.Forms.RadioButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.gcEdit = new System.Windows.Forms.GroupBox();
            this.pnlCopyRole = new DevExpress.XtraEditors.PanelControl();
            this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSrcNumber = new DevExpress.XtraEditors.TextEdit();
            this.btnRole = new DevExpress.XtraEditors.SimpleButton();
            this.btnSetPsw = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.gcInfo = new System.Windows.Forms.GroupBox();
            this.btnRight = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).BeginInit();
            this.gcQuery.SuspendLayout();
            this.gcEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCopyRole)).BeginInit();
            this.pnlCopyRole.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSrcNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcState
            // 
            this.gcState.Caption = "gridColumn1";
            this.gcState.FieldName = "State";
            this.gcState.Name = "gcState";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.splitContainerControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(803, 546);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "产品信息";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(2, 2);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.groupControl2);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.gcEdit);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(799, 542);
            this.splitContainerControl2.SplitterPosition = 171;
            this.splitContainerControl2.TabIndex = 1;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridCMain);
            this.groupControl2.Controls.Add(this.gcQuery);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(799, 365);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "产品信息";
            // 
            // gridCMain
            // 
            this.gridCMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCMain.Location = new System.Drawing.Point(2, 42);
            this.gridCMain.MainView = this.gridVMain;
            this.gridCMain.Name = "gridCMain";
            this.gridCMain.Size = new System.Drawing.Size(795, 321);
            this.gridCMain.TabIndex = 57;
            this.gridCMain.UseEmbeddedNavigator = true;
            this.gridCMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVMain});
            // 
            // gridVMain
            // 
            this.gridVMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcState});
            styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Red;
            styleFormatCondition1.Appearance.Options.UseForeColor = true;
            styleFormatCondition1.ApplyToRow = true;
            styleFormatCondition1.Column = this.gcState;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
            styleFormatCondition1.Value1 = false;
            this.gridVMain.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
            this.gridVMain.GridControl = this.gridCMain;
            this.gridVMain.Name = "gridVMain";
            this.gridVMain.OptionsSelection.MultiSelect = true;
            this.gridVMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridVMain.OptionsView.ColumnAutoWidth = false;
            this.gridVMain.OptionsView.ShowAutoFilterRow = true;
            this.gridVMain.OptionsView.ShowFooter = true;
            this.gridVMain.OptionsView.ShowGroupPanel = false;
            this.gridVMain.ViewCaption = " ";
            this.gridVMain.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridVMain_FocusedRowChanged);
            // 
            // gcQuery
            // 
            this.gcQuery.Controls.Add(this.rdAll);
            this.gcQuery.Controls.Add(this.rdNoState);
            this.gcQuery.Controls.Add(this.rdState);
            this.gcQuery.Controls.Add(this.btnExcel);
            this.gcQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcQuery.Location = new System.Drawing.Point(2, 2);
            this.gcQuery.Name = "gcQuery";
            this.gcQuery.Size = new System.Drawing.Size(795, 40);
            this.gcQuery.TabIndex = 111;
            this.gcQuery.TabStop = false;
            this.gcQuery.Text = "过滤条件";
            // 
            // rdAll
            // 
            this.rdAll.AutoSize = true;
            this.rdAll.Location = new System.Drawing.Point(181, 14);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(47, 16);
            this.rdAll.TabIndex = 12;
            this.rdAll.Text = "所有";
            this.rdAll.UseVisualStyleBackColor = true;
            this.rdAll.CheckedChanged += new System.EventHandler(this.rdState_CheckedChanged);
            // 
            // rdNoState
            // 
            this.rdNoState.AutoSize = true;
            this.rdNoState.Location = new System.Drawing.Point(126, 14);
            this.rdNoState.Name = "rdNoState";
            this.rdNoState.Size = new System.Drawing.Size(47, 16);
            this.rdNoState.TabIndex = 11;
            this.rdNoState.Text = "无效";
            this.rdNoState.UseVisualStyleBackColor = true;
            this.rdNoState.CheckedChanged += new System.EventHandler(this.rdState_CheckedChanged);
            // 
            // rdState
            // 
            this.rdState.AutoSize = true;
            this.rdState.Checked = true;
            this.rdState.Location = new System.Drawing.Point(71, 14);
            this.rdState.Name = "rdState";
            this.rdState.Size = new System.Drawing.Size(47, 16);
            this.rdState.TabIndex = 10;
            this.rdState.TabStop = true;
            this.rdState.Text = "有效";
            this.rdState.UseVisualStyleBackColor = true;
            this.rdState.CheckedChanged += new System.EventHandler(this.rdState_CheckedChanged);
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(236, 12);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(70, 22);
            this.btnExcel.TabIndex = 9;
            this.btnExcel.Text = "导出Excel";
            this.btnExcel.Click += new System.EventHandler(this.btn_Click);
            // 
            // gcEdit
            // 
            this.gcEdit.Controls.Add(this.btnRight);
            this.gcEdit.Controls.Add(this.pnlCopyRole);
            this.gcEdit.Controls.Add(this.btnRole);
            this.gcEdit.Controls.Add(this.btnSetPsw);
            this.gcEdit.Controls.Add(this.btnCancel);
            this.gcEdit.Controls.Add(this.btnEdit);
            this.gcEdit.Controls.Add(this.btnAdd);
            this.gcEdit.Controls.Add(this.btnSave);
            this.gcEdit.Controls.Add(this.gcInfo);
            this.gcEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcEdit.Location = new System.Drawing.Point(0, 0);
            this.gcEdit.Name = "gcEdit";
            this.gcEdit.Size = new System.Drawing.Size(799, 171);
            this.gcEdit.TabIndex = 108;
            this.gcEdit.TabStop = false;
            this.gcEdit.Text = "明细编辑   回车下移；新增“Alt+N”";
            // 
            // pnlCopyRole
            // 
            this.pnlCopyRole.Controls.Add(this.btnCopy);
            this.pnlCopyRole.Controls.Add(this.labelControl1);
            this.pnlCopyRole.Controls.Add(this.txtSrcNumber);
            this.pnlCopyRole.Location = new System.Drawing.Point(590, 1);
            this.pnlCopyRole.Name = "pnlCopyRole";
            this.pnlCopyRole.Size = new System.Drawing.Size(203, 23);
            this.pnlCopyRole.TabIndex = 1;
            this.pnlCopyRole.Visible = false;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(138, 0);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(65, 22);
            this.btnCopy.TabIndex = 66;
            this.btnCopy.Text = "拷贝角色";
            this.btnCopy.Click += new System.EventHandler(this.btn_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(2, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "来源工号";
            // 
            // txtSrcNumber
            // 
            this.txtSrcNumber.Location = new System.Drawing.Point(50, 0);
            this.txtSrcNumber.Name = "txtSrcNumber";
            this.txtSrcNumber.Size = new System.Drawing.Size(88, 21);
            this.txtSrcNumber.TabIndex = 0;
            // 
            // btnRole
            // 
            this.btnRole.Location = new System.Drawing.Point(460, 1);
            this.btnRole.Name = "btnRole";
            this.btnRole.Size = new System.Drawing.Size(65, 22);
            this.btnRole.TabIndex = 65;
            this.btnRole.Text = "维护角色";
            this.btnRole.Visible = false;
            this.btnRole.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnSetPsw
            // 
            this.btnSetPsw.Location = new System.Drawing.Point(395, 1);
            this.btnSetPsw.Name = "btnSetPsw";
            this.btnSetPsw.Size = new System.Drawing.Size(65, 22);
            this.btnSetPsw.TabIndex = 64;
            this.btnSetPsw.Text = "重置密码";
            this.btnSetPsw.Visible = false;
            this.btnSetPsw.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(345, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(50, 22);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "撤销&Z";
            this.btnCancel.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(245, 1);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(50, 22);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "修改&E";
            this.btnEdit.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(195, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(50, 22);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "新增&N";
            this.btnAdd.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(295, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(50, 22);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存&S";
            this.btnSave.Click += new System.EventHandler(this.btn_Click);
            // 
            // gcInfo
            // 
            this.gcInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcInfo.Location = new System.Drawing.Point(3, 18);
            this.gcInfo.Name = "gcInfo";
            this.gcInfo.Size = new System.Drawing.Size(793, 150);
            this.gcInfo.TabIndex = 63;
            this.gcInfo.TabStop = false;
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(525, 1);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(65, 22);
            this.btnRight.TabIndex = 69;
            this.btnRight.Text = "权限维护";
            this.btnRight.Visible = false;
            this.btnRight.Click += new System.EventHandler(this.btn_Click);
            // 
            // frmBseUserEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 546);
            this.Controls.Add(this.groupControl1);
            this.Name = "frmBseUserEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统用户维护";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVMain)).EndInit();
            this.gcQuery.ResumeLayout(false);
            this.gcQuery.PerformLayout();
            this.gcEdit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCopyRole)).EndInit();
            this.pnlCopyRole.ResumeLayout(false);
            this.pnlCopyRole.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSrcNumber.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridCMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVMain;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private System.Windows.Forms.GroupBox gcEdit;
        private System.Windows.Forms.GroupBox gcInfo;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraGrid.Columns.GridColumn gcState;
        private System.Windows.Forms.GroupBox gcQuery;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.RadioButton rdNoState;
        private System.Windows.Forms.RadioButton rdState;
        private DevExpress.XtraEditors.SimpleButton btnRole;
        private DevExpress.XtraEditors.SimpleButton btnSetPsw;
        private DevExpress.XtraEditors.PanelControl pnlCopyRole;
        private DevExpress.XtraEditors.SimpleButton btnCopy;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSrcNumber;
        private DevExpress.XtraEditors.SimpleButton btnRight;




    }
}