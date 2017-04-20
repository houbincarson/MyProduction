using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;

namespace ProduceManager
{
    public partial class frmProdFeeComfirm_Log : frmEditorBase
    {
        private DataTable dt = null;
        string strSpName = "Bse_Prod_Fee_Comfirm_Add_Edit_Del";
        public frmProdFeeComfirm_Log()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            dt = GetDT();
            gridCMain.DataSource = dt;
        }

        private DataTable GetDT()
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,PNumber,StDay,EdDay,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     //CApplication.App.CurrentSession.Company_Id.ToString(),
                              CApplication.App.CurrentSession.FyId.ToString(),
                     txtPNumber.Text.Trim(),
                     dtStDay.EditValue.ToString(),
                     dtEdDay.EditValue.ToString(),
                     "4" };
            dt = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
            return dt;
        }

        private void frmProdFeeComfirm_Log_Load(object sender, EventArgs e)
        {
            dtEdDay.EditValue = dtStDay.EditValue = DateTime.Now.ToShortDateString();

        }
    }
}
