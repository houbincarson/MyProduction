using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;

namespace ProduceManager
{
    public partial class frmSelectRptTicket : frmEditorBase
    {
        public DataTable DtBtnM
        {
            get;
            set;
        }
        public DataRow DrRet
        {
            get;
            set;
        }

        public frmSelectRptTicket()
        {
            InitializeComponent();
        }
        private void frmSelectRptTicket_Load(object sender, EventArgs e)
        {
            StaticFunctions.BindDplComboByTable(dplRpt, DtBtnM, "RptTicketTName", "BsuSetBtnM_Id",
                new string[] { "RptTicketTName", "RptTitle" },
                new string[] { "模板名称", "标题名称" }, true, "", "", false);
            dplRpt.ShowPopup();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            CApplication.App.CurrentSession.TimerId = 0;
            int k = msg.WParam.ToInt32();
            if (k == 27)//Esc
            {
                this.DialogResult = DialogResult.No;
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(dplRpt.EditValue) == string.Empty)
            {
                MessageBox.Show("请选择模板.");
                return;
            }
            DataRow[] drSels = DtBtnM.Select("BsuSetBtnM_Id=" + dplRpt.EditValue.ToString());
            DrRet = drSels[0];
            this.DialogResult = DialogResult.Yes;
        }
    }
}