using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProduceManager
{
    public partial class frmTst_UserOverLossChk : frmEditorBase
    {
        public string strUId
        {
            get;
            set;
        }
        public string strAccountDt
        {
            get;
            set;
        }

        public DataTable dtConst
        {
            get;
            set;
        }

        public frmTst_UserOverLossChk()
        {
            InitializeComponent();
        }

        private void frmEmpEdit_Load(object sender, EventArgs e)
        {
            string[] strKey = "StartWT_Dt,EndWT_Dt,IsNotChk,UserId,Fy_Id,flag".Split(",".ToCharArray());
            DataSet dsUReDl = this.DataRequest_By_DataSet("Tst_UserReceDeliChkEdit_Add_Edit_Del",strKey
                , new string[] { strAccountDt,strAccountDt, 
                        strAccountDt == string.Empty ? "1":"0",strUId,
                            CApplication.App.CurrentSession.FyId.ToString(), "13" });
            dsUReDl.AcceptChanges();
            gridCDeli.DataSource = dsUReDl.Tables[0].DefaultView;
            gridVDeli.BestFitColumns();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            CApplication.App.CurrentSession.TimerId = 0;
            int k = msg.WParam.ToInt32();
            if (k == 27)//Esc
            {
                this.DialogResult = DialogResult.Cancel;
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}