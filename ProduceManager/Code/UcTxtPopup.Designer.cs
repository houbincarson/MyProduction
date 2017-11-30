namespace ProduceManager
{
    partial class UcTxtPopup
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
            this.gridCCust = new DevExpress.XtraGrid.GridControl();
            this.gridVCust = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.UcPProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplCombox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCCust)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVCust)).BeginInit();
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
            this.dplCombox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.dplCombox.Size = new System.Drawing.Size(160, 21);
            this.dplCombox.TabIndex = 392;
            this.dplCombox.Popup += new System.EventHandler(this.dplCombox_Popup);
            this.dplCombox.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.dplCombox_Closed);
            this.dplCombox.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.dplCombox_EditValueChanging);
            this.dplCombox.Enter += new System.EventHandler(this.dplCombox_Enter);
            this.dplCombox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dplCombox_KeyDown);
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.gridCCust);
            this.popupContainerControl1.Location = new System.Drawing.Point(0, 21);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(450, 400);
            this.popupContainerControl1.TabIndex = 393;
            // 
            // gridCCust
            // 
            this.gridCCust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCCust.Location = new System.Drawing.Point(0, 0);
            this.gridCCust.MainView = this.gridVCust;
            this.gridCCust.Name = "gridCCust";
            this.gridCCust.Size = new System.Drawing.Size(450, 400);
            this.gridCCust.TabIndex = 295;
            this.gridCCust.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridVCust});
            // 
            // gridVCust
            // 
            this.gridVCust.GridControl = this.gridCCust;
            this.gridVCust.Name = "gridVCust";
            this.gridVCust.OptionsBehavior.Editable = false;
            this.gridVCust.OptionsView.ShowGroupPanel = false;
            this.gridVCust.OptionsView.ShowIndicator = false;
            this.gridVCust.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridVCust_KeyDown);
            this.gridVCust.Click += new System.EventHandler(this.gridVCust_Click);
            // 
            // UcTxtPopup
            // 
            this.Controls.Add(this.popupContainerControl1);
            this.Controls.Add(this.dplCombox);
            this.Size = new System.Drawing.Size(160, 21);
            this.Enter += new System.EventHandler(this.UcTxtPopup_Enter);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UcTxtPopup_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.UcTxtPopup_MouseDoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.UcPProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dplCombox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCCust)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVCust)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerEdit dplCombox;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraGrid.GridControl gridCCust;
        private DevExpress.XtraGrid.Views.Grid.GridView gridVCust;
        private DevExpress.XtraEditors.Repository.RepositoryItem UcPProperties;

    }
}
