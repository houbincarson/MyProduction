namespace ProduceManager
{
    partial class frmBseRoleUserEdit
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
            this.chkRoleList = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.dplSsDep = new DevExpress.XtraEditors.LookUpEdit();
            this.dplUserType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.chkRoleList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dplSsDep.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplUserType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // chkRoleList
            // 
            this.chkRoleList.CheckOnClick = true;
            this.chkRoleList.DisplayMember = "Role_Nme";
            this.chkRoleList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkRoleList.Location = new System.Drawing.Point(0, 31);
            this.chkRoleList.Name = "chkRoleList";
            this.chkRoleList.Size = new System.Drawing.Size(617, 303);
            this.chkRoleList.TabIndex = 104;
            this.chkRoleList.ValueMember = "Role_Id";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(35, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(45, 20);
            this.btnSave.TabIndex = 105;
            this.btnSave.Text = "保存&S";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(108, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 20);
            this.btnClose.TabIndex = 107;
            this.btnClose.Text = "关闭&C";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 334);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(617, 69);
            this.panelControl1.TabIndex = 108;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.dplSsDep);
            this.panelControl2.Controls.Add(this.dplUserType);
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.btnQuery);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(617, 31);
            this.panelControl2.TabIndex = 109;
            // 
            // dplSsDep
            // 
            this.dplSsDep.Location = new System.Drawing.Point(180, 5);
            this.dplSsDep.Name = "dplSsDep";
            this.dplSsDep.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dplSsDep.Properties.NullText = "";
            this.dplSsDep.Size = new System.Drawing.Size(100, 21);
            this.dplSsDep.TabIndex = 173;
            // 
            // dplUserType
            // 
            this.dplUserType.EditValue = "-1";
            this.dplUserType.Location = new System.Drawing.Point(30, 5);
            this.dplUserType.Name = "dplUserType";
            this.dplUserType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dplUserType.Properties.DropDownRows = 12;
            this.dplUserType.Size = new System.Drawing.Size(98, 21);
            this.dplUserType.TabIndex = 167;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(132, 8);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 166;
            this.labelControl3.Text = "所属部门";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(285, 5);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(50, 20);
            this.btnQuery.TabIndex = 164;
            this.btnQuery.Text = "查询&F";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(5, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 163;
            this.labelControl2.Text = "类型";
            // 
            // frmBseRoleUserEdit
            // 
            this.AcceptButton = this.btnQuery;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 403);
            this.Controls.Add(this.chkRoleList);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBseRoleUserEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "角色用户维护";
            this.Load += new System.EventHandler(this.frmRoleEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkRoleList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dplSsDep.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplUserType.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckedListBoxControl chkRoleList;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.ImageComboBoxEdit dplUserType;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit dplSsDep;
    }
}