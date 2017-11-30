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
    public partial class frmBseContrlEditTabel : frmEditorBase
    {
        private DataSet dsLoad = null;
        private DataTable dtConst = null;
        private DataTable dtShow = null;
        private string strKeyFiled = "TableInfo_Id";
        private string strSpName = "Bse_Show_Add_Edit_Del";

        public string SetClass
        {
            set
            {
                this.txtClassT.Text = value;
            }
        }
        public frmEditorBase FrmEditorBaseP
        {
            get;
            set;
        }

        public frmBseContrlEditTabel()
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

            StaticFunctions.ShowGridControl(gridVTabel, dtShow, dtConst);
        }
        private void frmBsuSetQuery_Load(object sender, EventArgs e)
        {
            txtNumberT.Focus();
            txtNumberT.Select();
        }
        
        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SimpleButton btn = sender as SimpleButton;
                switch (btn.Name)
                {
                    case "btnQueryT":
                        DoQueryT();
                        break;
                    case "btnOkT":
                        DoOkT();
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

        private void DoQueryT()
        {
            List<string> lisSpParmValue = new List<string>();
            string[] strKey = "ClassName,Type,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {txtNumberT.Text.Trim(),dplVisible.EditValue.ToString(),
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                "111"});
            DataSet dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            if (dtAdd == null)
            {
                return;
            }
            gridCTabel.DataSource = dtAdd.Tables[0].DefaultView;
            gridVTabel.BestFitColumns();
        }
        private void DoOkT()
        {
            if (txtClassT.Text.Trim() == string.Empty)
                return;
            if (txtGroupNameT.Text.Trim() == string.Empty)
                return;

            if (Convert.ToString(dplTypeT.EditValue) == string.Empty)
            {
                dplTypeT.ShowPopup();
                return;
            }

            if (gridVTabel.SelectedRowsCount == 0)
            {
                MessageBox.Show("没有选中任何记录.");
                return;
            }
            string strKeyIds = string.Empty;
            foreach (int i in gridVTabel.GetSelectedRows())
            {
                DataRow dr = gridVTabel.GetDataRow(i);
                strKeyIds += strKeyIds == string.Empty ? dr[strKeyFiled].ToString() : "," + dr[strKeyFiled].ToString();
            }
            if (strKeyIds == string.Empty)
                return;

            List<string> lisSpParmValue = new List<string>();
            string[] strKey = "ClassName,GroupName,Type,strKeyIds,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {txtClassT.Text.Trim(),txtGroupNameT.Text.Trim(),
                dplTypeT.EditValue.ToString(),strKeyIds,
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                "112"});
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
