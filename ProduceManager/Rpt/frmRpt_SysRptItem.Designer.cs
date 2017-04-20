namespace ProduceManager
{
    partial class frmRpt_SysRptItem
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.gcQuery = new System.Windows.Forms.GroupBox();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.gcQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.EnableHyperlinks = true;
            this.reportViewer1.Location = new System.Drawing.Point(0, 60);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(972, 645);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.ReportExport += new Microsoft.Reporting.WinForms.ExportEventHandler(this.reportViewer1_ReportExport);
            this.reportViewer1.Print += new Microsoft.Reporting.WinForms.ReportPrintEventHandler(this.reportViewer1_Print);
            this.reportViewer1.RenderingComplete += new Microsoft.Reporting.WinForms.RenderingCompleteEventHandler(this.reportViewer1_RenderingComplete);
            this.reportViewer1.RenderingBegin += new System.ComponentModel.CancelEventHandler(this.reportViewer1_RenderingBegin);
            // 
            // gcQuery
            // 
            this.gcQuery.Controls.Add(this.btnOk);
            this.gcQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcQuery.Location = new System.Drawing.Point(0, 0);
            this.gcQuery.Name = "gcQuery";
            this.gcQuery.Size = new System.Drawing.Size(972, 60);
            this.gcQuery.TabIndex = 1;
            this.gcQuery.TabStop = false;
            this.gcQuery.Text = "报表查询条件,回车查看报表";
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.Location = new System.Drawing.Point(855, 15);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(95, 35);
            this.btnOk.TabIndex = 99;
            this.btnOk.Text = "查看报表&F";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmRpt_SysRptItem
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 705);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.gcQuery);
            this.Name = "frmRpt_SysRptItem";
            this.Text = "系统报表";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRS_FormClosing);
            this.Load += new System.EventHandler(this.frmRS_Load);
            this.gcQuery.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.GroupBox gcQuery;
        private DevExpress.XtraEditors.SimpleButton btnOk;
    }
}