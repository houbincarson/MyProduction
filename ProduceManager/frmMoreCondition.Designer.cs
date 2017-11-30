namespace ProduceManager
{
    partial class frmMoreCondition
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
            this.txtSizeMax = new DevExpress.XtraEditors.TextEdit();
            this.txtSizeMin = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSizeMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSizeMin.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.txtSizeMax);
            this.splitContainerControl1.Panel1.Controls.Add(this.txtSizeMin);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl2);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnOK);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(437, 371);
            this.splitContainerControl1.SplitterPosition = 248;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // txtSizeMax
            // 
            this.txtSizeMax.Location = new System.Drawing.Point(257, 26);
            this.txtSizeMax.Name = "txtSizeMax";
            this.txtSizeMax.Properties.Mask.EditMask = "\\d+(\\R.\\d{0,2})?";
            this.txtSizeMax.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtSizeMax.Size = new System.Drawing.Size(121, 21);
            this.txtSizeMax.TabIndex = 21;
            // 
            // txtSizeMin
            // 
            this.txtSizeMin.Location = new System.Drawing.Point(90, 26);
            this.txtSizeMin.Name = "txtSizeMin";
            this.txtSizeMin.Properties.Mask.EditMask = "\\d+(\\R.\\d{0,2})?";
            this.txtSizeMin.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtSizeMin.Size = new System.Drawing.Size(121, 21);
            this.txtSizeMin.TabIndex = 20;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(218, 29);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(8, 14);
            this.labelControl2.TabIndex = 19;
            this.labelControl2.Text = "--";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(28, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "尺寸范围：";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(246, 47);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 31);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(71, 47);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(101, 31);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmMoreCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 371);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmMoreCondition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更多查询条件";
            this.Load += new System.EventHandler(this.frmMoreCondition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSizeMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSizeMin.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.TextEdit txtSizeMax;
        private DevExpress.XtraEditors.TextEdit txtSizeMin;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;

    }
}