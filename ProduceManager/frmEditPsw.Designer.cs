namespace ProduceManager
{
    partial class frmEditPsw
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
            this.txtNewPsw = new DevExpress.XtraEditors.TextEdit();
            this.txtNewPsw2 = new DevExpress.XtraEditors.TextEdit();
            this.txtOldPsw = new DevExpress.XtraEditors.TextEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPsw.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPsw2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOldPsw.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtNewPsw);
            this.groupControl1.Controls.Add(this.txtNewPsw2);
            this.groupControl1.Controls.Add(this.txtOldPsw);
            this.groupControl1.Controls.Add(this.btnCancel);
            this.groupControl1.Controls.Add(this.btnOk);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(403, 285);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "产品信息";
            // 
            // txtNewPsw
            // 
            this.txtNewPsw.Location = new System.Drawing.Point(135, 70);
            this.txtNewPsw.Name = "txtNewPsw";
            this.txtNewPsw.Properties.PasswordChar = '*';
            this.txtNewPsw.Size = new System.Drawing.Size(148, 21);
            this.txtNewPsw.TabIndex = 10;
            // 
            // txtNewPsw2
            // 
            this.txtNewPsw2.Location = new System.Drawing.Point(135, 106);
            this.txtNewPsw2.Name = "txtNewPsw2";
            this.txtNewPsw2.Properties.PasswordChar = '*';
            this.txtNewPsw2.Size = new System.Drawing.Size(148, 21);
            this.txtNewPsw2.TabIndex = 9;
            // 
            // txtOldPsw
            // 
            this.txtOldPsw.Location = new System.Drawing.Point(135, 34);
            this.txtOldPsw.Name = "txtOldPsw";
            this.txtOldPsw.Properties.PasswordChar = '*';
            this.txtOldPsw.Size = new System.Drawing.Size(148, 21);
            this.txtOldPsw.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(203, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(50, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(135, 152);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(50, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(71, 110);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "确认密码";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(83, 37);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(36, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "旧密码";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(83, 73);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "新密码";
            // 
            // frmEditPsw
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 285);
            this.Controls.Add(this.groupControl1);
            this.Name = "frmEditPsw";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "修改密码";
            this.Load += new System.EventHandler(this.frmCreateSigleProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPsw.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPsw2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOldPsw.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtNewPsw;
        private DevExpress.XtraEditors.TextEdit txtNewPsw2;
        private DevExpress.XtraEditors.TextEdit txtOldPsw;




    }
}