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

namespace ProduceManager
{
    public partial class frmSetSpecialFeeToCust : frmEditorBase
    {

        string strSpName = "Bse_SetSpecialFee_Add_Edit_Del";
        string strKeyId = "-1";
        DataSet dsLoad = null;
        private string CustStr = string.Empty;
        GridCheckMarksSelection cbk = null;
        string Pm_Ids = string.Empty;
        string operate = string.Empty;
        string operateSetFee = string.Empty;
        public frmSetSpecialFeeToCust()
        {
            InitializeComponent();
        }

        private void frmSetSpecialFeeToCust_Load(object sender, EventArgs e)
        {
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {strKeyId,
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

            comboBoxEdit1.SelectedIndex = 0;
        }



        private void btnOk_Click(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Text.Trim() == string.Empty)
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
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,COL_SQL_NULL,COL_SQL,COL_SQL_Max,PNumber,UnitType,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {strKeyId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     COL_SQL_Null.TrimEnd(','), 
                    COL_SQL.TrimEnd(','),
                    COL_SQL_Max.TrimEnd(','),
                    txtPNumber.Text.Trim(),
                    comboBoxEdit1.Text.Trim(),
                     "3" };
            DataSet ds = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            this.gridView1.Columns.Clear();
            this.gridControl1.DataSource = ds.Tables[0];
            //createRepositoryItemImageComboBox();

            if (cbk == null)
            {
                cbk = new GridCheckMarksSelection(gridView1);
            }
            this.gridView1.Columns[0].Visible = false;
        }
        private void gridView1_EndSorting(object sender, EventArgs e)
        {

        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (operateSetFee == string.Empty)
            {
                return;
            }
            string first = string.Empty;
            DataRowView dr = (DataRowView)cbk.GetSelectedRow(0);
            string unittype = decimal.Parse(dr["克工费"].ToString()) != 0 ? "weight" : "amount";
            for (int j = 5; j < this.gridView1.Columns.Count - 1; j++)
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (i == gridView1.RowCount - 1)
                    {
                        first = first + "SELECT '" + gridView1.GetDataRow(i)[0].ToString() + "', '" + operate + "','" + this.gridView1.Columns[j].FieldName + "','" + this.gridView1.GetDataRow(i)[1].ToString() + "','" + gridView1.GetDataRow(i)[2].ToString() + "', '" + gridView1.GetDataRow(i)[3].ToString() + "','" + gridView1.GetDataRow(i)[4].ToString() + "','" + operateSetFee + "','" + gridView1.GetDataRow(i)[5].ToString() + "'";

                        string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,COL_SQL_MAX,flag".Split(",".ToCharArray());
                        string[] strVal = new string[] {strKeyId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     first,
                     "4" };
                        DataTable dt = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
                        if (dt != null && dt.Rows[0][0].ToString() == "OK")
                        {
                            first = string.Empty;
                        }
                    }
                    else
                    {
                        if (cbk.IsRowSelected(i) == true)
                        {
                            first = first + "SELECT '" + gridView1.GetDataRow(i)[0].ToString() + "', '" + operate + "','" + this.gridView1.Columns[j].FieldName + "','" + this.gridView1.GetDataRow(i)[1].ToString() + "','" + gridView1.GetDataRow(i)[2].ToString() + "', '" + gridView1.GetDataRow(i)[3].ToString() + "','" + gridView1.GetDataRow(i)[4].ToString() + "','" + operateSetFee + "','" + gridView1.GetDataRow(i)[5].ToString() + "' UNION ALL ";
                        }
                    }
                }
            }
            MessageBox.Show("特殊工费设置成功");
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,PNumber,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {strKeyId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     this.gridView1.GetFocusedDataRow()["款式编号"].ToString(),
                     "5" };
            DataTable dt = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
            labelControl6.Text = dt.Rows[0]["PNumber"].ToString();
            labelControl7.Text = dt.Rows[0]["Name"].ToString();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            btnOk_Click(null, null);
        }

        private void btnSetFee_Click(object sender, EventArgs e)
        {
            Pm_Ids = cbk.GetKeyIds("款式序号");
            if (Pm_Ids.Trim() == string.Empty)
            {
                MessageBox.Show("请先选择要维护特殊工费的款式");
                return;
            }

            //string visibleIndex = this.gridView1.SortInfo[0].Column.VisibleIndex.ToString();
            //string columnname = this.gridView1.SortInfo[0].Column.Name.Replace("col", "");
            //GridColumn column = this.gridView1.SortInfo[0].Column;
            //this.gridView1.ClearSorting();
            //if (visibleIndex != "0" && visibleIndex != "1" && visibleIndex != "2" && visibleIndex != "3" && visibleIndex != "4")
            //{
            //Point a = System.Windows.Forms.Form.MousePosition;
            //int x = a.X;
            //int y = a.Y;

            DataRowView dr = (DataRowView)cbk.GetSelectedRow(0);
            string unittype1 = decimal.Parse(dr["克工费"].ToString()) != 0 ? "weight" : "amount";
            frmSetSpecialFee frmSpecialFee = new frmSetSpecialFee(true, this.chkComCust.Text, 100, 200);
            if (frmSpecialFee.ShowDialog() == DialogResult.OK)
            {
                operate = frmSpecialFee.operation.ToString();
                operateSetFee = frmSpecialFee.setfee.ToString();
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    string resultfee = string.Empty;
                    if (cbk.IsRowSelected(i) == true)
                    {
                        switch (frmSpecialFee.operation)
                        {
                            case 1:
                                resultfee = unittype1 == "weight" ? (decimal.Parse(this.gridView1.GetRowCellValue(i, "克工费").ToString()) + frmSpecialFee.setfee).ToString() : (decimal.Parse(this.gridView1.GetRowCellValue(i, "件工费").ToString()) + frmSpecialFee.setfee).ToString();
                                break;
                            case 2:
                                resultfee = unittype1 == "weight" ? (decimal.Parse(this.gridView1.GetRowCellValue(i, "克工费").ToString()) - frmSpecialFee.setfee).ToString() : (decimal.Parse(this.gridView1.GetRowCellValue(i, "件工费").ToString()) - frmSpecialFee.setfee).ToString();
                                break;
                            case 3:
                                resultfee = unittype1 == "weight" ? (decimal.Parse(this.gridView1.GetRowCellValue(i, "克工费").ToString()) * frmSpecialFee.setfee).ToString() : (decimal.Parse(this.gridView1.GetRowCellValue(i, "件工费").ToString()) * frmSpecialFee.setfee).ToString();
                                break;
                            case 4:
                                resultfee = (frmSpecialFee.setfee).ToString();
                                break;
                            default:
                                break;
                        }
                        for (int j = 5; j < gridView1.Columns.Count - 1; j++)
                        { 
                            this.gridView1.SetRowCellValue(i, gridView1.Columns[j], resultfee);
                        }
                    }
                }
            }
        }
    }
}
