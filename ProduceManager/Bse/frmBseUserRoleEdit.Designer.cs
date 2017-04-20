namespace ProduceManager
{
    partial class frmBseUserRoleEdit
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
            this.labInfo = new DevExpress.XtraEditors.LabelControl();
            this.chkRoleList = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.chkRoleList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labInfo
            // 
            this.labInfo.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labInfo.Appearance.Options.UseForeColor = true;
            this.labInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labInfo.Location = new System.Drawing.Point(2, 2);
            this.labInfo.Name = "labInfo";
            this.labInfo.Size = new System.Drawing.Size(0, 14);
            this.labInfo.TabIndex = 103;
            // 
            // chkRoleList
            // 
            this.chkRoleList.CheckOnClick = true;
            this.chkRoleList.DisplayMember = "Role_Nme";
            this.chkRoleList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkRoleList.Location = new System.Drawing.Point(0, 22);
            this.chkRoleList.Name = "chkRoleList";
            this.chkRoleList.Size = new System.Drawing.Size(494, 312);
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
            this.panelControl1.Size = new System.Drawing.Size(494, 69);
            this.panelControl1.TabIndex = 108;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.labInfo);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(494, 22);
            this.panelControl2.TabIndex = 109;
            // 
            // frmBseUserRoleEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 403);
            this.Controls.Add(this.chkRoleList);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBseUserRoleEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.Load += new System.EventHandler(this.frmRoleEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkRoleList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labInfo;
        private DevExpress.XtraEditors.CheckedListBoxControl chkRoleList;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
    }
}