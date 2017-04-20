using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProduceManager
{
    public partial class frmProdMegerInfo : frmEditorBase
    {
        string strSpName = "Bse_Merge_PNumber_Rel_Mgr_Add_Edit_Del";
        private int PM_Id;
        private DataTable dt;
        private int CurrentPM_Id;
        public frmProdMegerInfo(int PM_Id, DataTable dt)
        {
            InitializeComponent();
            this.PM_Id = PM_Id;
            this.CurrentPM_Id = PM_Id;
            this.dt = dt;
        }

        private DataTable GetProdInfo(int Pm_Id)
        {
            DataTable dt = new DataTable();
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,PM_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     Pm_Id.ToString(),
                     "103" };
            dt = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
            CurrentPM_Id = Pm_Id;
            return dt;
        }

        private void frmMoreCondition_Load(object sender, EventArgs e)
        {
            DataTable dt = GetProdInfo(PM_Id);
            ShowInfo(dt);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //上一条
        private void btnPre_Click(object sender, EventArgs e)
        {
            string RowNum = this.dt.Select("PM_Id =" + CurrentPM_Id)[0]["RowNum"].ToString();
            if (Convert.ToInt32(RowNum) - 1 == 0)
            {
                MessageBox.Show("当前已经是第一条数据了。");
                return;
            }
            int CurrentRowNum = Convert.ToInt32(RowNum) - 1;
            int PM_Id = Convert.ToInt32(this.dt.Select("RowNum =" + CurrentRowNum)[0]["PM_Id"].ToString());
            DataTable dtPre = GetProdInfo(PM_Id);
            ShowInfo(dtPre);
        }
        //下一条
        private void btnNext_Click(object sender, EventArgs e)
        {

            string RowNum = this.dt.Select("PM_Id =" + CurrentPM_Id)[0]["RowNum"].ToString();
            int CurrentRowNum = 1 + Convert.ToInt32(RowNum);
            if (CurrentRowNum > dt.Rows.Count)
            {
                MessageBox.Show("当前已经是最后一条数据了。");
                return;
            }
            int PM_Id = Convert.ToInt32(this.dt.Select("RowNum =" + CurrentRowNum)[0]["PM_Id"].ToString());
            DataTable dtNext = GetProdInfo(PM_Id);
            ShowInfo(dtNext);
        }

        private void ShowInfo(DataTable dtInfo)
        {
            lblMegerPNumber.Text = dtInfo.Rows[0]["MergePNumber"].ToString();
            lblName.Text = dtInfo.Rows[0]["Name"].ToString();
            lblPNumber.Text = dtInfo.Rows[0]["PNumber"].ToString();
            lblSize.Text = dtInfo.Rows[0]["SizeSmall"].ToString() + "--" + dtInfo.Rows[0]["SizeLarger"].ToString();
            lblWeight.Text = dtInfo.Rows[0]["WeightSmall"].ToString() + "--" + dtInfo.Rows[0]["WeightLarger"].ToString();
            lblBus_PNumber.Text = dtInfo.Rows[0]["Bus_PNumber"].ToString();
            lblPC_Id.Text = dtInfo.Rows[0]["Kind_Name"].ToString();
        }

    }
}
