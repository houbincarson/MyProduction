namespace ProduceManager
{
    partial class frmSelectOperation
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
            this.btnCost = new DevExpress.XtraEditors.SimpleButton();
            this.btnBasicFee = new DevExpress.XtraEditors.SimpleButton();
            this.btnLimits = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaleFee = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnCost
            // 
            this.btnCost.Location = new System.Drawing.Point(45, 42);
            this.btnCost.Name = "btnCost";
            this.btnCost.Size = new System.Drawing.Size(157, 122);
            this.btnCost.TabIndex = 0;
            this.btnCost.Text = "成本工费维护";
            this.btnCost.Click += new System.EventHandler(this.btnCost_Click);
            // 
            // btnBasicFee
            // 
            this.btnBasicFee.Location = new System.Drawing.Point(259, 42);
            this.btnBasicFee.Name = "btnBasicFee";
            this.btnBasicFee.Size = new System.Drawing.Size(157, 122);
            this.btnBasicFee.TabIndex = 1;
            this.btnBasicFee.Text = "基础销售工费维护";
            this.btnBasicFee.Click += new System.EventHandler(this.btnBasicFee_Click);
            // 
            // btnLimits
            // 
            this.btnLimits.Location = new System.Drawing.Point(45, 199);
            this.btnLimits.Name = "btnLimits";
            this.btnLimits.Size = new System.Drawing.Size(157, 122);
            this.btnLimits.TabIndex = 2;
            this.btnLimits.Text = "开放度维护";
            this.btnLimits.Click += new System.EventHandler(this.btnLimits_Click);
            // 
            // btnSaleFee
            // 
            this.btnSaleFee.Location = new System.Drawing.Point(259, 199);
            this.btnSaleFee.Name = "btnSaleFee";
            this.btnSaleFee.Size = new System.Drawing.Size(157, 122);
            this.btnSaleFee.TabIndex = 3;
            this.btnSaleFee.Text = "特殊销售工费维护";
            this.btnSaleFee.Click += new System.EventHandler(this.btnSaleFee_Click);
            // 
            // frmSelectOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 345);
            this.Controls.Add(this.btnSaleFee);
            this.Controls.Add(this.btnLimits);
            this.Controls.Add(this.btnBasicFee);
            this.Controls.Add(this.btnCost);
            this.Name = "frmSelectOperation";
            this.Text = "选择操作";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCost;
        private DevExpress.XtraEditors.SimpleButton btnBasicFee;
        private DevExpress.XtraEditors.SimpleButton btnLimits;
        private DevExpress.XtraEditors.SimpleButton btnSaleFee;

    }
}