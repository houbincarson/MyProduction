namespace ProduceManager
{
    partial class frmBsuSetQueryFastAddInfo
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
            this.gcSet = new System.Windows.Forms.GroupBox();
            this.txtClassGv = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuit = new DevExpress.XtraEditors.SimpleButton();
            this.dplClass = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.gcMain = new System.Windows.Forms.GroupBox();
            this.gcSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClassGv.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplClass.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcSet
            // 
            this.gcSet.Controls.Add(this.txtClassGv);
            this.gcSet.Controls.Add(this.labelControl4);
            this.gcSet.Controls.Add(this.btnQuit);
            this.gcSet.Controls.Add(this.dplClass);
            this.gcSet.Controls.Add(this.labelControl1);
            this.gcSet.Controls.Add(this.btnOk);
            this.gcSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcSet.Location = new System.Drawing.Point(0, 0);
            this.gcSet.Name = "gcSet";
            this.gcSet.Size = new System.Drawing.Size(892, 64);
            this.gcSet.TabIndex = 25;
            this.gcSet.TabStop = false;
            this.gcSet.Text = "设置选项";
            // 
            // txtClassGv
            // 
            this.txtClassGv.Location = new System.Drawing.Point(367, 21);
            this.txtClassGv.Name = "txtClassGv";
            this.txtClassGv.Size = new System.Drawing.Size(184, 21);
            this.txtClassGv.TabIndex = 26;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(319, 24);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 27;
            this.labelControl4.Text = "页面类名";
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(639, 21);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(57, 23);
            this.btnQuit.TabIndex = 23;
            this.btnQuit.Text = "关闭返回";
            this.btnQuit.Click += new System.EventHandler(this.btn_Click);
            // 
            // dplClass
            // 
            this.dplClass.EditValue = "0";
            this.dplClass.Location = new System.Drawing.Point(145, 21);
            this.dplClass.Name = "dplClass";
            this.dplClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dplClass.Size = new System.Drawing.Size(170, 21);
            this.dplClass.TabIndex = 22;
            this.dplClass.SelectedValueChanged += new System.EventHandler(this.dplClass_SelectedValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(97, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "业务模板";
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(564, 21);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(57, 23);
            this.btnOk.TabIndex = 20;
            this.btnOk.Text = "确定完成";
            this.btnOk.Click += new System.EventHandler(this.btn_Click);
            // 
            // gcMain
            // 
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 64);
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(892, 602);
            this.gcMain.TabIndex = 26;
            this.gcMain.TabStop = false;
            this.gcMain.Text = "快速设置项目";
            // 
            // frmBsuSetQueryFastAddInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 666);
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.gcSet);
            this.Name = "frmBsuSetQueryFastAddInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查询页面快速设置";
            this.Load += new System.EventHandler(this.frmBsuSetQuery_Load);
            this.gcSet.ResumeLayout(false);
            this.gcSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtClassGv.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplClass.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gcSet;
        private System.Windows.Forms.GroupBox gcMain;
        private DevExpress.XtraEditors.SimpleButton btnQuit;
        private DevExpress.XtraEditors.ImageComboBoxEdit dplClass;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.TextEdit txtClassGv;
        private DevExpress.XtraEditors.LabelControl labelControl4;




    }
}