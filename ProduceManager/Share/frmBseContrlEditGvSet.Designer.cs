namespace ProduceManager
{
    partial class frmBseContrlEditGvSet
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
            this.txtClassGv = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtSets = new DevExpress.XtraEditors.MemoEdit();
            this.txtGvName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtClassGv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSets.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGvName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtClassGv
            // 
            this.txtClassGv.Location = new System.Drawing.Point(333, 23);
            this.txtClassGv.Name = "txtClassGv";
            this.txtClassGv.Size = new System.Drawing.Size(216, 21);
            this.txtClassGv.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(283, 26);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 25;
            this.labelControl4.Text = "页面类名";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(440, 466);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(57, 23);
            this.btnClear.TabIndex = 24;
            this.btnClear.Text = "清空设置";
            this.btnClear.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(333, 466);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(57, 23);
            this.btnOk.TabIndex = 23;
            this.btnOk.Text = "确定完成";
            this.btnOk.Click += new System.EventHandler(this.btn_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(273, 81);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(270, 14);
            this.labelControl3.TabIndex = 22;
            this.labelControl3.Text = "一行输入一项，格式：绑定字段；中文名称(,，;；)";
            // 
            // txtSets
            // 
            this.txtSets.Location = new System.Drawing.Point(259, 101);
            this.txtSets.Name = "txtSets";
            this.txtSets.Size = new System.Drawing.Size(351, 354);
            this.txtSets.TabIndex = 21;
            // 
            // txtGvName
            // 
            this.txtGvName.Location = new System.Drawing.Point(333, 52);
            this.txtGvName.Name = "txtGvName";
            this.txtGvName.Size = new System.Drawing.Size(216, 21);
            this.txtGvName.TabIndex = 20;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(259, 55);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 19;
            this.labelControl2.Text = "GridView名称";
            // 
            // frmBseContrlEditGvSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 666);
            this.Controls.Add(this.txtClassGv);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtSets);
            this.Controls.Add(this.txtGvName);
            this.Controls.Add(this.labelControl2);
            this.Name = "frmBseContrlEditGvSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "维护grid显示列";
            this.Load += new System.EventHandler(this.frmBsuSetQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtClassGv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSets.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGvName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtClassGv;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.MemoEdit txtSets;
        private DevExpress.XtraEditors.TextEdit txtGvName;
        private DevExpress.XtraEditors.LabelControl labelControl2;



    }
}