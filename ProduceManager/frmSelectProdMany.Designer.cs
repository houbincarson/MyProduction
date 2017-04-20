namespace ProduceManager
{
    partial class frmSelectProdMany
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 14);
            this.label1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(185, 110);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 27);
            this.btnClose.TabIndex = 405;
            this.btnClose.Text = "取消";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(103, 110);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(68, 27);
            this.btnOk.TabIndex = 404;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 14);
            this.label2.TabIndex = 406;
            // 
            // frmSelectProdMany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 164);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectProdMany";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量挑款";
            this.Load += new System.EventHandler(this.frmSelectProdMany_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private System.Windows.Forms.Label label2;
    }
}