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
    public partial class frmProdMegerPNumber : frmEditorBase
    { 
        public frmProdMegerPNumber()
        {
            InitializeComponent(); 
        }
        string strSpName = "Bse_Merge_PNumber_Rel_Add_Edit_Del";
        string strKeyId = "-1";
        DataSet dsLoad = null;
        GridCheckMarksSelection cbk = null;
        private void frmProdMegerPNumber_Load(object sender, EventArgs e)
        {
            dtStDay.EditValue = DateTime.Now.ToShortDateString();
            dtEndDay.EditValue = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (cbk == null)
            {
                cbk = new GridCheckMarksSelection(gridView1);
            }
            GetDate();
        }

        private void GetDate()
        {
            string PNmber = txtPNumber.Text.Trim();
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,Name,PNumber,Designer,PatentNumber,MarketTime,Help_Factory,Pc_Id,Material,Technology,Sex,DesignType,Material_Pc_Id,Compound_Type,SizeSmall,SizeLarger,WeightSmall,WeightLarger,flag".Split(",".ToCharArray());//BasicTechnology,BasicTechnologySurface,BseShapeFlower,BseShapeSquare,BseRound,BseFaceWidth,BseFastener,BseStructure,flag
            string[] strVal = new string[] {strKeyId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                        "",//Name
                      PNmber,//PNumber
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
                        //"-1",//BasicTechnology
                        //"-1",//BasicTechnologySurface
                        //"-1",//BseShapeFlower
                        //"-1",//BseShapeSquare
                        //"-1",//BseRound
                        //"-1",//BseFaceWidth
                        //"-1",//BseFastener
                        //"-1",//BseStructure 
                        "1" };
            dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            gridControl1.DataSource = dsLoad.Tables[0];
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (txtMergePNumber.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入合并后的款号");
                txtMergePNumber.Focus();
                return;
            }
            if (cbk.SelectedCount == 0)
            {
                MessageBox.Show("请选择要修改的款式");
                return;
            }
            string Pm_Ids = cbk.GetKeyIds("PM_Id");
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,MergePNumber,Key_Ids,flag".Split(",".ToCharArray());
            string[] strVal = new string[] { 
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     txtMergePNumber.Text.Trim(),
                     Pm_Ids,
                     "101" };
            DataSet ds = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            if (ds.Tables[0].Rows[0][0].ToString() == "OK")
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (cbk.IsRowSelected(i))
                    {
                        gridView1.SetRowCellValue(i, "MergePNumber", txtMergePNumber.Text.Trim());
                    }
                }
            }
            cbk.ClearSelection();
            MessageBox.Show("合并成功");
            txtMergePNumber.Text = string.Empty;
        }

        private void btnMoreCondition_Click(object sender, EventArgs e)
        {
            string condition = txtPNumber.Text.Trim() + "," + dtStDay.EditValue.ToString() + "," + dtEndDay.EditValue.ToString() + ",";
            frmMoreConditionBus frmMore = new frmMoreConditionBus(condition);
            if (frmMore.ShowDialog() == DialogResult.OK)
            {
                if (cbk == null)
                {
                    cbk = new GridCheckMarksSelection(gridView1);
                }
                this.gridControl1.DataSource = frmMore.dtCondition.Tables[0];
            }
        }
    }
}
