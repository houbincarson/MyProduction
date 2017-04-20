using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using System.Configuration;
using System.IO;

namespace ProduceManager
{
    public partial class frmSetSpecialFeeToCust : frmEditorBase
    {

        string strSpName = "Bse_SetSpecialFee_Add_Edit_Del";
        DataSet dsLoad = null; 
        GridCheckMarksSelection cbk = null;
        string Pm_Ids = string.Empty;
        string Operate = string.Empty;
        string OperateSetFee = string.Empty;
        public frmSetSpecialFeeToCust()
        {
            InitializeComponent();
        }
        //窗体载入
        private void frmSetSpecialFeeToCust_Load(object sender, EventArgs e)
        {
            #region 绑定客户
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     "2" };
            dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            StaticFunctions.BindCheckedComboBoxEdit(chkComCust, dsLoad.Tables[0], "CustName", "CustId", "", "");
            foreach (CheckedListBoxItem item in chkComCust.Properties.Items)
            {
                item.CheckState = CheckState.Checked;
            }
            #endregion

            #region 绑定类别
            StaticFunctions.BindDplComboByTable(extTreePc, dsLoad.Tables[1], "Kind_Name", "Kind_Id|Parent_Kind_Id", "Kind_Key=400", new string[] { "Kind_Key=120", "Kind_Name=200" }, new string[] { "拼音", "名称" }, "Kind_Id", "Level>0", "Kind_Key", "Kind_Id", "Parent_Kind_Id", "", true);
            #endregion

            cboUnitFee.SelectedIndex = 0;
        }
        //查询
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cboUnitFee.Text.Trim() == string.Empty)
            {
                MessageBox.Show("为了避免误操作，请选择工费计算方式！");
                return;
            }
            cbk = null;
            string COL_SQL = string.Empty;
            string COL_SQL_Null = string.Empty;
            string COL_SQL_Max = string.Empty;
            string[] strAry = this.chkComCust.Text.Split(',');
            for (int i = 0; i < strAry.Length; i++)
            {
                COL_SQL = COL_SQL + "[" + strAry[i].ToString().Trim() + "],";
                COL_SQL_Null = COL_SQL_Null + "[" + strAry[i].ToString().Trim() + "]= ISNULL(" + "[" + strAry[i].ToString().Trim() + "],0),";
                COL_SQL_Max = COL_SQL_Max + "[" + strAry[i].ToString().Trim() + "]= Max(" + strAry[i].ToString().Trim() + "),";
            }
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,COL_SQL_NULL,COL_SQL,COL_SQL_Max,PNumber,UnitType,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     COL_SQL_Null.TrimEnd(','), 
                    COL_SQL.TrimEnd(','),
                    COL_SQL_Max.TrimEnd(','),
                    txtPNumber.Text.Trim(),
                    cboUnitFee.Text.Trim(),
                     "3" };
            DataSet ds = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            this.gridVMain.Columns.Clear();
            this.gridCMain.DataSource = ds.Tables[0];
            if (cbk == null)
            {
                cbk = new GridCheckMarksSelection(gridVMain);
            }
            this.gridVMain.Columns[0].Visible = false;
            if (cboUnitFee.Text.Trim() == "按件计")
            {
                this.gridVMain.Columns[3].Visible = false;
            }
            else
            {
                this.gridVMain.Columns[4].Visible = false;
            }
        }
        //保存设置
        private void btnSet_Click(object sender, EventArgs e)
        {
            if (OperateSetFee == string.Empty)
            {
                return;
            }
            string SetInfoSql = string.Empty;
            DataRowView dr = (DataRowView)cbk.GetSelectedRow(0);
            string UnitType = decimal.Parse(dr["克工费"].ToString()) != 0 ? "Weight" : "Amount";
            for (int j = 5; j < this.gridVMain.Columns.Count - 1; j++)
            {
                for (int i = 0; i < gridVMain.RowCount; i++)
                {
                    if (i == gridVMain.RowCount - 1)
                    {
                        SetInfoSql = SetInfoSql + "SELECT '" + gridVMain.GetDataRow(i)[0].ToString() + "', '" + Operate + "','" + this.gridVMain.Columns[j].FieldName + "','" + this.gridVMain.GetDataRow(i)[1].ToString() + "','" + gridVMain.GetDataRow(i)[2].ToString() + "', '" + gridVMain.GetDataRow(i)[3].ToString() + "','" + gridVMain.GetDataRow(i)[4].ToString() + "','" + OperateSetFee + "','" + gridVMain.GetDataRow(i)[5].ToString() + "'";

                        string[] strKey = "EUser_Id,EDept_Id,Fy_Id,COL_SQL_MAX,Flag".Split(",".ToCharArray());
                        string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     SetInfoSql,
                     "4" };
                        DataTable dt = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
                        if (dt != null && dt.Rows[0][0].ToString() == "OK")
                        {
                            SetInfoSql = string.Empty;
                        }
                    }
                    else
                    {
                        if (cbk.IsRowSelected(i) == true)
                        {
                            SetInfoSql = SetInfoSql + "SELECT '" + gridVMain.GetDataRow(i)[0].ToString() + "', '" + Operate + "','" + this.gridVMain.Columns[j].FieldName + "','" + this.gridVMain.GetDataRow(i)[1].ToString() + "','" + gridVMain.GetDataRow(i)[2].ToString() + "', '" + gridVMain.GetDataRow(i)[3].ToString() + "','" + gridVMain.GetDataRow(i)[4].ToString() + "','" + OperateSetFee + "','" + gridVMain.GetDataRow(i)[5].ToString() + "' UNION ALL ";
                        }
                    }
                }
            }
            MessageBox.Show("特殊工费设置成功");
        }
        //双击gridVMain
        private void gridVMain_DoubleClick(object sender, EventArgs e)
        { 
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,PNumber,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     this.gridVMain.GetFocusedDataRow()["款式编号"].ToString(),
                     "5" };
            DataTable dt = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
            string imagepath = System.Environment.CurrentDirectory + "\\" + ConfigurationManager.AppSettings["ImageFilePath"] + "\\" + dt.Rows[0]["StylePic"].ToString() + "_ver1";
            if (File.Exists(imagepath) == false || dt.Rows[0]["StylePic"].ToString().Trim() == string.Empty)
            {
                StaticFunctions.GetImageByte(dt.Rows[0]["StylePic"].ToString());
            }
            picImage.ImageLocation = imagepath; 
            lblPNumber.Text = dt.Rows[0]["PNumber"].ToString();
            lblName.Text = dt.Rows[0]["Name"].ToString();
        }
        //撤销
        private void btnRestore_Click(object sender, EventArgs e)
        {
            btnOk_Click(null, null);
        }
        //设置特殊工费
        private void btnSetFee_Click(object sender, EventArgs e)
        {
            Pm_Ids = cbk.GetKeyIds("款式序号");
            if (Pm_Ids.Trim() == string.Empty)
            {
                MessageBox.Show("请先选择要维护特殊工费的款式");
                return;
            }
            //string visibleIndex = this.gridVMain.SortInfo[0].Column.VisibleIndex.ToString();
            //string columnname = this.gridVMain.SortInfo[0].Column.Name.Replace("col", "");
            //GridColumn column = this.gridVMain.SortInfo[0].Column;
            //this.gridVMain.ClearSorting();
            //if (visibleIndex != "0" && visibleIndex != "1" && visibleIndex != "2" && visibleIndex != "3" && visibleIndex != "4")
            //{
            //Point a = System.Windows.Forms.Form.MousePosition;
            //int x = a.X;
            //int y = a.Y; 
            DataRowView dr = (DataRowView)cbk.GetSelectedRow(0);
            string UintFeeType = decimal.Parse(dr["克工费"].ToString()) != 0 ? "Weight" : "Amount";
            frmSetSpecialFee frmSpecialFee = new frmSetSpecialFee(true, this.chkComCust.Text, 100, 200);
            if (frmSpecialFee.ShowDialog() == DialogResult.OK)
            {
                Operate = frmSpecialFee.Operation.ToString();
                OperateSetFee = frmSpecialFee.setfee.ToString();
                for (int i = 0; i < gridVMain.RowCount; i++)
                {
                    string ResultFee = string.Empty;
                    if (cbk.IsRowSelected(i) == true)
                    {
                        switch (frmSpecialFee.Operation)
                        {
                            case 1:
                                ResultFee = UintFeeType == "Weight" ? (decimal.Parse(this.gridVMain.GetRowCellValue(i, "克工费").ToString()) + frmSpecialFee.setfee).ToString() : (decimal.Parse(this.gridVMain.GetRowCellValue(i, "件工费").ToString()) + frmSpecialFee.setfee).ToString();
                                break;
                            case 2:
                                ResultFee = UintFeeType == "Weight" ? (decimal.Parse(this.gridVMain.GetRowCellValue(i, "克工费").ToString()) - frmSpecialFee.setfee).ToString() : (decimal.Parse(this.gridVMain.GetRowCellValue(i, "件工费").ToString()) - frmSpecialFee.setfee).ToString();
                                break;
                            case 3:
                                ResultFee = UintFeeType == "Weight" ? (decimal.Parse(this.gridVMain.GetRowCellValue(i, "克工费").ToString()) * frmSpecialFee.setfee).ToString() : (decimal.Parse(this.gridVMain.GetRowCellValue(i, "件工费").ToString()) * frmSpecialFee.setfee).ToString();
                                break;
                            case 4:
                                ResultFee = (frmSpecialFee.setfee).ToString();
                                break;
                            default:
                                break;
                        }
                        for (int j = 5; j < gridVMain.Columns.Count - 1; j++)
                        {
                            this.gridVMain.SetRowCellValue(i, gridVMain.Columns[j], ResultFee);
                        }
                    }
                }
            }
        }
        //跳转
        private void lblGoTo_Click(object sender, EventArgs e)
        {
            frmSysBusQuery frm = new frmSysBusQuery();
            frm.SetInit("frmProd_Special_FeeQuery");
            frm.Show();
        }
    }
}
