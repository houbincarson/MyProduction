using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace ProduceManager
{
    public partial class frmBseContrlEditSrc : frmEditorBase
    {
        private DataSet dsLoad = null;
        private DataTable dtConst = null;
        private DataTable dtShow = null;
        private string strKeyFiled = "Show_Id";
        private string strSpName = "Bse_Show_Add_Edit_Del";

        public frmEditorBase FrmEditorBaseP
        {
            get;
            set;
        }
        public string SetClass
        {
            set
            {
                this.txtClassS.Text = value;
            }
        }
        public string SetClassSrc
        {
            set
            {
                this.txtNumberS.Text = value;
            }
        }

        public frmBseContrlEditSrc()
        {
            InitializeComponent();

            InitContr();
        }
        private void InitContr()
        {
            if (dsLoad != null)
                return;

            dsLoad = this.GetFrmLoadDs(this.Name);
            dsLoad.AcceptChanges();
            dtShow = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];

            StaticFunctions.ShowGridControl(gridVSrc, dtShow, dtConst);
        }
        private void frmBsuSetQuery_Load(object sender, EventArgs e)
        {
            txtNumberS.Focus();
            txtNumberS.Select();

            if (txtNumberS.Text.Trim() != string.Empty)
            {
                DoQueryS();
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SimpleButton btn = sender as SimpleButton;
                switch (btn.Name)
                {
                    case "btnQueryS":
                        DoQueryS();
                        break;
                    case "btnOkS":
                        DoOkS();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("错误:" + err.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void DoQueryS()
        {
            List<string> lisSpParmValue = new List<string>();
            string[] strKey = "ClassName,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {txtNumberS.Text.Trim(),
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                "121"});
            DataSet dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            if (dtAdd == null)
            {
                return;
            }
            gridCSrc.DataSource = dtAdd.Tables[0].DefaultView;
            gridVSrc.BestFitColumns();
        }
        private void DoOkS()
        {
            if (txtClassS.Text.Trim() == string.Empty)
                return;

            if (gridVSrc.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中任何记录.");
                return;
            }
            string strKeyIds = string.Empty;
            foreach (int i in gridVSrc.GetSelectedRows())
            {
                DataRow dr = gridVSrc.GetDataRow(i);
                strKeyIds += strKeyIds == string.Empty ? dr[strKeyFiled].ToString() : "," + dr[strKeyFiled].ToString();
            }
            if (strKeyIds == string.Empty)
                return;

            List<string> lisSpParmValue = new List<string>();
            string[] strKey = "ClassName,GroupName,strKeyIds,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {txtClassS.Text.Trim(),txtGroupNameS.Text.Trim(),
                strKeyIds,
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                "122"});
            DataSet dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            if (dtAdd == null)
            {
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            if (FrmEditorBaseP != null)
                FrmEditorBaseP.RefreshItem();
            this.Close();
            this.Dispose();
        }
    }

}
