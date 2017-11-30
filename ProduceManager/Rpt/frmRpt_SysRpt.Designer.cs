namespace ProduceManager
{
    partial class frmRpt_SysRpt
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
            this.gcRpt = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // gcRpt
            // 
            this.gcRpt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRpt.Location = new System.Drawing.Point(0, 0);
            this.gcRpt.Name = "gcRpt";
            this.gcRpt.Size = new System.Drawing.Size(972, 746);
            this.gcRpt.TabIndex = 0;
            this.gcRpt.TabStop = false;
            this.gcRpt.Text = "明细报表，点击打开对应报表";
            // 
            // frmRpt_SysRpt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 746);
            this.Controls.Add(this.gcRpt);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRpt_SysRpt";
            this.Text = "系统报表";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gcRpt;
    }
}