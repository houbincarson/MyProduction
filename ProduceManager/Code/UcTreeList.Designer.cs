namespace ProduceManager
{
    partial class UcTreeList
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.UcPProperties = new DevExpress.XtraEditors.Repository.RepositoryItem();
            this.dplCombox = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCol = new DevExpress.XtraEditors.SimpleButton();
            this.btnExp = new DevExpress.XtraEditors.SimpleButton();
            this.ckbSelect = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.UcPProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplCombox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckbSelect.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // UcPProperties
            // 
            this.UcPProperties.AutoHeight = false;
            this.UcPProperties.Name = "UcPProperties";
            // 
            // dplCombox
            // 
            this.dplCombox.Dock = System.Windows.Forms.DockStyle.Top;
            this.dplCombox.EditValue = "";
            this.dplCombox.Location = new System.Drawing.Point(0, 0);
            this.dplCombox.Name = "dplCombox";
            this.dplCombox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dplCombox.Properties.HideSelection = false;
            this.dplCombox.Properties.PopupControl = this.popupContainerControl1;
            this.dplCombox.Size = new System.Drawing.Size(160, 21);
            this.dplCombox.TabIndex = 392;
            this.dplCombox.Popup += new System.EventHandler(this.dplCombox_Popup);
            this.dplCombox.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.dplCombox_Closed);
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.treeList1);
            this.popupContainerControl1.Controls.Add(this.panel1);
            this.popupContainerControl1.Location = new System.Drawing.Point(0, 21);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(450, 400);
            this.popupContainerControl1.TabIndex = 393;
            // 
            // treeList1
            // 
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 33);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsView.AutoWidth = false;
            this.treeList1.OptionsView.ShowCheckBoxes = true;
            this.treeList1.Size = new System.Drawing.Size(450, 367);
            this.treeList1.TabIndex = 0;
            this.treeList1.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList1_AfterCheckNode);
            this.treeList1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dplCombox_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.btnCol);
            this.panel1.Controls.Add(this.btnExp);
            this.panel1.Controls.Add(this.ckbSelect);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 33);
            this.panel1.TabIndex = 100;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(215, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确认选中";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCol
            // 
            this.btnCol.Location = new System.Drawing.Point(145, 4);
            this.btnCol.Name = "btnCol";
            this.btnCol.Size = new System.Drawing.Size(65, 23);
            this.btnCol.TabIndex = 2;
            this.btnCol.Text = "收起所有";
            this.btnCol.Click += new System.EventHandler(this.btnCol_Click);
            // 
            // btnExp
            // 
            this.btnExp.Location = new System.Drawing.Point(75, 4);
            this.btnExp.Name = "btnExp";
            this.btnExp.Size = new System.Drawing.Size(65, 23);
            this.btnExp.TabIndex = 1;
            this.btnExp.Text = "展开所有";
            this.btnExp.Click += new System.EventHandler(this.btnExp_Click);
            // 
            // ckbSelect
            // 
            this.ckbSelect.Location = new System.Drawing.Point(25, 7);
            this.ckbSelect.Name = "ckbSelect";
            this.ckbSelect.Properties.AutoWidth = true;
            this.ckbSelect.Properties.Caption = "全选";
            this.ckbSelect.Size = new System.Drawing.Size(47, 19);
            this.ckbSelect.TabIndex = 10;
            this.ckbSelect.CheckedChanged += new System.EventHandler(this.ckbSelect_CheckedChanged);
            // 
            // UcTreeList
            // 
            this.Controls.Add(this.popupContainerControl1);
            this.Controls.Add(this.dplCombox);
            this.Name = "UcTreeList";
            this.Size = new System.Drawing.Size(160, 21);
            ((System.ComponentModel.ISupportInitialize)(this.UcPProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplCombox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckbSelect.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerEdit dplCombox;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItem UcPProperties;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.CheckEdit ckbSelect;
        private DevExpress.XtraEditors.SimpleButton btnExp;
        private DevExpress.XtraEditors.SimpleButton btnCol;
        private DevExpress.XtraEditors.SimpleButton btnOk;

    }
}
