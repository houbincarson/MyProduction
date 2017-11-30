namespace ProduceManager
{
    partial class frmImportOrd
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
            this.txtOrdNum = new DevExpress.XtraEditors.TextEdit();
            this.lblOrd = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrdNum.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(188, 21);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 43);
            this.btnCancel.TabIndex = 99;
            this.btnCancel.Text = "取消复制&C";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(79, 21);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 43);
            this.btnOk.TabIndex = 98;
            this.btnOk.Text = "确定复制&S";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.txtOrdNum);
            this.splitContainerControl1.Panel1.Controls.Add(this.lblOrd);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.btnOk);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(383, 191);
            this.splitContainerControl1.SplitterPosition = 99;
            this.splitContainerControl1.TabIndex = 302;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // txtOrdNum
            // 
            this.txtOrdNum.Location = new System.Drawing.Point(127, 36);
            this.txtOrdNum.Name = "txtOrdNum";
            this.txtOrdNum.Size = new System.Drawing.Size(151, 21);
            this.txtOrdNum.TabIndex = 1;
            // 
            // lblOrd
            // 
            this.lblOrd.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblOrd.Appearance.Options.UseForeColor = true;
            this.lblOrd.Location = new System.Drawing.Point(79, 39);
            this.lblOrd.Name = "lblOrd";
            this.lblOrd.Size = new System.Drawing.Size(48, 14);
            this.lblOrd.TabIndex = 0;
            this.lblOrd.Text = "复制单号";
            // 
            // frmImportOrd
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 191);
            this.Controls.Add(this.splitContainerControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportOrd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "从现有单复制";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtOrdNum.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.LabelControl lblOrd;
        private DevExpress.XtraEditors.TextEdit txtOrdNum;
    }
}