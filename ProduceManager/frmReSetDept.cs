using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Diagnostics;
using WcfSimpData;

namespace ProduceManager
{
    public partial class frmReSetDept : Form
    {
        private readonly string BtProduceCS = System.Configuration.ConfigurationManager.AppSettings["BtProduceCS"];

        public string strDeptName
        {
            get;
            set;
        }

        public frmReSetDept()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string[] strKey = "Form,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
            string[] strVal = new string[] { this.Name
                    , CApplication.App.CurrentSession.UserId.ToString()
                    , CApplication.App.CurrentSession.DeptId.ToString()
                    , CApplication.App.CurrentSession.FyId.ToString() };
            MethodRequest req = new MethodRequest();
            req.ParamKeys = strKey;
            req.ParamVals = strVal;
            req.ProceName = "Get_Form_Load_Table";
            req.ProceDb = this.BtProduceCS;
            DataSet dsLoad = ServerRequestProcess.DbRequest.DataRequest_By_DataSet(req);

            DataTable dtDept = dsLoad.Tables[0];
            StaticFunctions.BindDplComboByTable(dplDeptId, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "", false);

            dplDeptId.EditValue = CApplication.App.CurrentSession.DeptId;
            dplDeptId.Focus();
            dplDeptId.SelectAll();
        }

        private void lookUpEdit_Properties_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if (sender is DevExpress.XtraEditors.LookUpEdit)
            {
                DevExpress.XtraEditors.LookUpEdit dpl = sender as DevExpress.XtraEditors.LookUpEdit;
                if (!dpl.Properties.DisplayMember.Equals("Name"))
                {
                    dpl.Properties.DisplayMember = "Name";
                }

                btnOk.Select();
            }
            else if (sender is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit dpl = sender as DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit;
                if (!dpl.DisplayMember.Equals("Name"))
                {
                    dpl.DisplayMember = "Name";
                }
            }
        }

        private void lookUpEdit_Properties_QueryPopUp(object sender, CancelEventArgs e)
        {
            if (sender is DevExpress.XtraEditors.LookUpEdit)
            {
                DevExpress.XtraEditors.LookUpEdit dpl = sender as DevExpress.XtraEditors.LookUpEdit;
                if (!dpl.Properties.DisplayMember.Equals("Number"))
                {
                    dpl.Properties.DisplayMember = "Number";
                }
            }
            else if (sender is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit dpl = sender as DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit;
                if (!dpl.DisplayMember.Equals("Number"))
                {
                    dpl.DisplayMember = "Number";
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dplDeptId.EditValue == null)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
            Int64 iDept = 0;
            if (Int64.TryParse(dplDeptId.EditValue.ToString(),out iDept))
            {
                string strSql = "Dept_Id=" + dplDeptId.EditValue.ToString();

                string[] strKey = "strTableName,strTableFieldKey,strTableFieldValue,strSql,UserId,ActFlag".Split(",".ToCharArray());
                string[] strVal = new string[] { "Bse_User", "User_Id", CApplication.App.CurrentSession.UserId.ToString()
                            , strSql, CApplication.App.CurrentSession.UserId.ToString(), "UpdateSubDept" };
                MethodRequest req = new MethodRequest();
                req.ParamKeys = strKey;
                req.ParamVals = strVal;
                req.ProceName = "Share_Update_Table_Value";
                req.ProceDb = this.BtProduceCS;
                DataTable dtRet = ServerRequestProcess.DbRequest.DataRequest_By_DataTable(req);
                if (dtRet.Rows[0][0].ToString() != "OK")
                {
                    MessageBox.Show("出错:" + dtRet.Rows[0][0]);
                    return;
                }

                CApplication.App.CurrentSession.DeptId = iDept;
                strDeptName = dplDeptId.GetColumnValue("Name").ToString();
                CApplication.App.CurrentSession.FyId = Int64.Parse(dplDeptId.GetColumnValue("Fy_Id").ToString());
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            this.DialogResult = DialogResult.Cancel;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
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
