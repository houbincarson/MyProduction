﻿namespace ProduceManager
{
    partial class frmSelectLimitsArea
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
            this.btnComfirm = new DevExpress.XtraEditors.SimpleButton();
            this.chkAllowPick = new System.Windows.Forms.RadioButton();
            this.chkNoSee = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnComfirm
            // 
            this.btnComfirm.Location = new System.Drawing.Point(75, 167);
            this.btnComfirm.Name = "btnComfirm";
            this.btnComfirm.Size = new System.Drawing.Size(75, 23);
            this.btnComfirm.TabIndex = 3;
            this.btnComfirm.Text = "确定";
            this.btnComfirm.Click += new System.EventHandler(this.btnComfirm_Click);
            // 
            // chkAllowPick
            // 
            this.chkAllowPick.AutoSize = true;
            this.chkAllowPick.Location = new System.Drawing.Point(104, 72);
            this.chkAllowPick.Name = "chkAllowPick";
            this.chkAllowPick.Size = new System.Drawing.Size(95, 16);
            this.chkAllowPick.TabIndex = 4;
            this.chkAllowPick.TabStop = true;
            this.chkAllowPick.Text = "区域全部可见";
            this.chkAllowPick.UseVisualStyleBackColor = true;
            // 
            // chkNoSee
            // 
            this.chkNoSee.AutoSize = true;
            this.chkNoSee.Location = new System.Drawing.Point(104, 113);
            this.chkNoSee.Name = "chkNoSee";
            this.chkNoSee.Size = new System.Drawing.Size(107, 16);
            this.chkNoSee.TabIndex = 6;
            this.chkNoSee.TabStop = true;
            this.chkNoSee.Text = "区域全部不可见";
            this.chkNoSee.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(31, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(156, 167);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmSelectLimitsArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 262);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkNoSee);
            this.Controls.Add(this.chkAllowPick);
            this.Controls.Add(this.btnComfirm);
            this.Name = "frmSelectLimitsArea";
            this.Text = "区域款式权限选择";
            this.Load += new System.EventHandler(this.frmSelectLimitsArea_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnComfirm;
        private System.Windows.Forms.RadioButton chkAllowPick;
        private System.Windows.Forms.RadioButton chkNoSee;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}