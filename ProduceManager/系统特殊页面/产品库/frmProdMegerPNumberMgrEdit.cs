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
    public partial class frmProdMegerPNumberMgrEdit : frmEditorBase
    {
        private DataSet ds = null;
        string strSpName = "Bse_Merge_PNumber_Rel_Mgr_Add_Edit_Del";
        public frmProdMegerPNumberMgrEdit(DataSet ds)
        {
            InitializeComponent();
            this.ds = ds;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            if (gridCMain.FocusedView.Name.ToString() != "gridVMain")
            {
                MessageBox.Show("请选中合并款号！");
                return;
            }

            string MergePNumber = this.gridVMain.GetFocusedDataRow()["MergePNumber"].ToString();
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,MergePNumber,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                           CApplication.App.CurrentSession.FyId.ToString(),
                MergePNumber ,
                     "102" };
            ds.Tables[0].Rows.Remove(ds.Tables[0].Select("MergePNumber = " + "'" + MergePNumber + "'")[0]);
            DataSet dsLog = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            if (dsLog.Tables[0].Rows[0][0].ToString() == "OK")
            {
                MessageBox.Show("撤销合并成功！");
            }
        }

        private void frmProductLog_Load(object sender, EventArgs e)
        {
            Bind();
        }

        private void Bind()
        {
            try
            {
                DataColumn KeyColumn = ds.Tables[0].Columns["MergePNumber"];
                DataColumn foreignKeyColumn = ds.Tables[1].Columns["MergePNumber"];
                ds.Relations.Add("ChildGrid", KeyColumn, foreignKeyColumn);
                gridCMain.DataSource = ds.Tables[0]; 
                //this.gridVCom.BestFitColumns();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ds = GetDs();
            Bind();
        }

        private DataSet GetDs()
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,MergePNumber,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     txtMegerPNumber.Text.Trim(),
                     "6" };
            ds = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            return ds;
        }

        private void gridVCom_DoubleClick(object sender, EventArgs e)
        { 
            int row = (gridCMain.MainView as ColumnView).FocusedRowHandle;
            ColumnView detailView = gridVMain.GetDetailView(row, 0) as ColumnView;
            DataRow dr = detailView.GetDataRow(detailView.FocusedRowHandle);
            int PM_Id = Convert.ToInt32(dr["Bus_PM_Id"].ToString());
            frmProdMegerInfo info = new frmProdMegerInfo(PM_Id, ds.Tables[1]);
            info.ShowDialog();
        }
         
    }
}
