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
    public partial class frmProdPromotionMgrEdit : frmEditorBase
    {
        private DataSet ds = null;
        string strSpName = "Bse_Prod_Festival_Rel_Mgr_Add_Edit_Del";
        public frmProdPromotionMgrEdit()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (gridCMain.FocusedView.Name.ToString() != "gridVMain")
            {
                MessageBox.Show("请选中方案号！");
                return;
            }

            string Festival_Id = this.gridVMain.GetFocusedDataRow()["Festival_Id"].ToString();
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,Festival_Id,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                                 //CApplication.App.CurrentSession.Company_Id.ToString(),
                                          CApplication.App.CurrentSession.FyId.ToString(),
                Festival_Id ,
                     "102" };
            ds.Tables[0].Rows.Remove(ds.Tables[0].Select("Festival_Id = " + "'" + Festival_Id + "'")[0]);
            DataSet dsLog = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            if (dsLog.Tables[0].Rows[0][0].ToString() == "OK")
            {
                MessageBox.Show("撤销合并成功！");
            }
        }
         

        private void Bind()
        {
            try
            {
                DataColumn KeyColumn = ds.Tables[0].Columns["Festival_Id"];
                DataColumn foreignKeyColumn = ds.Tables[1].Columns["Festival_Id"];
                ds.Relations.Add("ChildGrid", KeyColumn, foreignKeyColumn);
                gridCMain.DataSource = ds.Tables[0]; 
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
             string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,FANumber,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                           CApplication.App.CurrentSession.FyId.ToString(),
                     txtFANumber.Text.Trim(),
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
