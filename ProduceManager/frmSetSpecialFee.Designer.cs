namespace ProduceManager
{
    partial class frmSetSpecialFee
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lueOperation = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSetFee = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.lueOperation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSetFee.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnComfirm
            // 
            this.btnComfirm.Location = new System.Drawing.Point(117, 202);
            this.btnComfirm.Name = "btnComfirm";
            this.btnComfirm.Size = new System.Drawing.Size(87, 27);
            this.btnComfirm.TabIndex = 3;
            this.btnComfirm.Text = "确定";
            this.btnComfirm.Click += new System.EventHandler(this.btnComfirm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(2, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 14);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(262, 202);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lueOperation
            // 
            this.lueOperation.Location = new System.Drawing.Point(233, 86);
            this.lueOperation.Name = "lueOperation";
            this.lueOperation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueOperation.Properties.NullText = "";
            this.lueOperation.Size = new System.Drawing.Size(117, 21);
            this.lueOperation.TabIndex = 9;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(120, 90);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "设置方式：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(120, 147);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 11;
            this.labelControl2.Text = "设置金额：";
            // 
            // txtSetFee
            // 
            this.txtSetFee.Location = new System.Drawing.Point(233, 147);
            this.txtSetFee.Name = "txtSetFee";
            this.txtSetFee.Properties.Mask.EditMask = "\\d+(\\R.\\d{0,2})?";
            this.txtSetFee.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtSetFee.Size = new System.Drawing.Size(117, 21);
            this.txtSetFee.TabIndex = 12;
            // 
            // frmSetSpecialFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 295);
            this.Controls.Add(this.txtSetFee);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lueOperation);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnComfirm);
            this.Name = "frmSetSpecialFee";
            this.Text = "设置特殊工费";
            this.Load += new System.EventHandler(this.frmSetSpecialFee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lueOperation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSetFee.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnComfirm;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LookUpEdit lueOperation;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtSetFee;
    }
}