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
    public partial class frmBseCostEdit : frmEditorBase
    {
        public frmBseCostEdit()
        {
            InitializeComponent();
        }
        string strSpName = "Bse_Prod_Model_Info_Add_Edit_Del";
        DataSet dsLoad;
        GridCheckMarksSelection cbk = null;
        //窗体载入
        private void frmBseCostEdit_Load(object sender, EventArgs e)
        {
            dtStDay.EditValue = DateTime.Now.ToShortDateString();
            dtEndDay.EditValue = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        }
        //查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (cbk == null)
            {
                cbk = new GridCheckMarksSelection(gridVMain);
            }
            GetDate();
        }
        private void GetDate()
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Name,PNumber,Designer,PatentNumber,MarketTime,Help_Factory,Pc_Id,Material,Technology,Sex,DesignType,Material_Pc_Id,Compound_Type,SizeSmall,SizeLarger,WeightSmall,WeightLarger,BasicTechnology,BasicTechnologySurface,BseShapeFlower,BseShapeSquare,BseRound,BseFaceWidth,BseFastener,BseStructure,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                        "",//Name
                        txtPNumber.Text.Trim(),//PNumber
                        "",//Designer
                        "",//PatentNumber
                        "",//MarketTime
                        "",//Help_Factory
                        "",//Pc_Id
                        "",//Material
                        "-1",//Technology
                        "-1",//Sex
                        "-1",//DesignType
                        "",//Material_Pc_Id
                        "-1",//Compound_Type
                        "0",//SizeSmall
                        "0",//SizeLarger
                        "0",//WeightSmall
                        "0",//WeightLarger
                        "-1",//BasicTechnology
                        "-1",//BasicTechnologySurface
                        "-1",//BseShapeFlower
                        "-1",//BseShapeSquare
                        "-1",//BseRound
                        "-1",//BseFaceWidth
                        "-1",//BseFastener
                        "-1",//BseStructure 
                        "1" };
            dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            gridCMain.DataSource = dsLoad.Tables[0];
        }
        //设置
        private void btnSet_Click(object sender, EventArgs e)
        {
            if (txtCost.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入修改后的成本工费");
                return;
            }
            if (cbk.SelectedCount == 0)
            {
                MessageBox.Show("请选择要修改的款式");
                return;
            }
            string Pm_Ids = cbk.GetKeyIds("PM_Id");
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,CostFee,Key_Ids,flag".Split(",".ToCharArray());
            string[] strVal = new string[] { 
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     txtCost.Text.Trim(),
                     Pm_Ids,
                     "201" };
            DataSet ds = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            if (ds.Tables[0].Rows[0][0].ToString() == "OK")
            {
                for (int i = 0; i < gridVMain.RowCount; i++)
                {
                    if (cbk.IsRowSelected(i))
                    {
                        gridVMain.SetRowCellValue(i, "CostFee", txtCost.Text.Trim());
                    }
                }
            }
            cbk.ClearSelection();
            MessageBox.Show("设置成功");
            txtCost.Text = string.Empty;
        }
        //更多筛选条件
        private void btnMoreCondition_Click(object sender, EventArgs e)
        {
            string Condition = txtPNumber.Text.Trim() + "," + dtStDay.EditValue.ToString() + "," + dtEndDay.EditValue.ToString() + ",";
            frmMoreCondition frmMore = new frmMoreCondition(Condition);
            if (frmMore.ShowDialog() == DialogResult.OK)
            {
                if (cbk == null)
                {
                    cbk = new GridCheckMarksSelection(gridVMain);
                }
                this.gridCMain.DataSource = frmMore.dtCondition.Tables[0];

            }
        }
        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
