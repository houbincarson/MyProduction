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
        private string type;
        public frmBseCostEdit(string type)
        {
            InitializeComponent();
            this.type = type;
        }
        string strSpName = "Bse_Prod_Model_Info_Add_Edit_Del";
        string strKeyId = "-1";
        DataSet dsLoad = null;
        GridCheckMarksSelection cbk = null;
        string alertmsg = string.Empty;
        private void frmBseCostEdit_Load(object sender, EventArgs e)
        {

            if (type == "Cost")
            {
                this.Text = "成本工费维护";
                lblFee.Text = "输入修改后的成本工费：";
                alertmsg = "请输入修改后的成本工费";
                labelControl3.Visible = false;
                comboBoxEdit1.Visible = false;
                this.gridColumn5.Visible = false;
                this.gridColumn6.Visible = false;
            }
            if (type == "BasicFee")
            {
                this.Text = "基础销售工费维护";
                lblFee.Text = "输入修改后的基础销售工费：";
                alertmsg = "输入修改后的基础销售工费";
                comboBoxEdit1.SelectedIndex = 0;
                this.gridColumn3.Visible = false;
            }

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
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,Name,PNumber,Designer,PatentNumber,MarketTime,Help_Factory,Pc_Id,Material,Technology,Sex,DesignType,Material_Pc_Id,Compound_Type,SizeSmall,SizeLarger,WeightSmall,WeightLarger,BasicTechnology,BasicTechnologySurface,BseShapeFlower,BseShapeSquare,BseRound,BseFaceWidth,BseFastener,BseStructure,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {strKeyId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                        "",//Name
                        "",//PNumber
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
            gridControl1.DataSource = dsLoad.Tables[0];
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (txtCost.Text.Trim().Length == 0)
            {
                MessageBox.Show(alertmsg);
                return;
            }
            if (cbk.SelectedCount == 0)
            {
                MessageBox.Show("请选择要修改的款式");
                return;
            }
            string Pm_Ids = cbk.GetKeyIds("PM_Id");



            if (type == "Cost")
            {
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
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        if (cbk.IsRowSelected(i))
                        {
                            gridView1.SetRowCellValue(i, "CostFee", txtCost.Text.Trim());
                        }
                    }
                }
            }

            if (type == "BasicFee")
            {
                string[] strKey1 = "EUser_Id,EDept_Id,Fy_Id,WeightFee,AmoutFee,UnitType,Key_Ids,flag".Split(",".ToCharArray());
                string[] strVal1 = new string[] { 
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     txtCost.Text.Trim(),
                     txtCost.Text.Trim(), 
                     comboBoxEdit1.SelectedIndex.ToString().Trim(),
                     Pm_Ids,
                     "202" };
                DataSet ds1 = this.DataRequest_By_DataSet(strSpName, strKey1, strVal1);
                if (ds1.Tables[0].Rows[0][0].ToString() == "OK")
                {
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        if (cbk.IsRowSelected(i))
                        {
                            if (comboBoxEdit1.SelectedIndex.ToString().Trim() == "0")
                            {
                                gridView1.SetRowCellValue(i, "WeightFee", txtCost.Text.Trim());
                            }
                            else
                            {
                                gridView1.SetRowCellValue(i, "AmountFee", txtCost.Text.Trim());
                            }
                        }
                    }
                }
            }


            cbk.ClearSelection();
            MessageBox.Show("设置成功");
            txtCost.Text = string.Empty;
        }

        private void btnMoreCondition_Click(object sender, EventArgs e)
        {
            string condition = txtPNumber.Text.Trim() + "," + dtStDay.EditValue.ToString() + "," + dtEndDay.EditValue.ToString() + ",";
            frmMoreCondition frmMore = new frmMoreCondition(condition);
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
