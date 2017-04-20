namespace ProduceManager
{
    partial class frmReSetDept
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dplDeptId = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dplDeptId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dplDeptId
            // 
            this.dplDeptId.Location = new System.Drawing.Point(149, 52);
            this.dplDeptId.Name = "dplDeptId";
            this.dplDeptId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dplDeptId.Properties.DropDownRows = 12;
            this.dplDeptId.Properties.ImmediatePopup = true;
            this.dplDeptId.Properties.NullText = "";
            this.dplDeptId.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
            this.dplDeptId.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.dplDeptId.Properties.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.lookUpEdit_Properties_QueryPopUp);
            this.dplDeptId.Properties.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.lookUpEdit_Properties_Closed);
            this.dplDeptId.Size = new System.Drawing.Size(119, 21);
            this.dplDeptId.TabIndex = 174;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(75, 55);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 14);
            this.labelControl3.TabIndex = 173;
            this.labelControl3.Text = "切换所属部门";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(149, 86);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(50, 23);
            this.btnOk.TabIndex = 175;
            this.btnOk.Text = "确定&C";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmReSetDept
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 344);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.dplDeptId);
            this.Controls.Add(this.labelControl3);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(456, 371);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(456, 371);
            this.Name = "frmReSetDept";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "切换部门";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dplDeptId.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit dplDeptId;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnOk;


    }
}

