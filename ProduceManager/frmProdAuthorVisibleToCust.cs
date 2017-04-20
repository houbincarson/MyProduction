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
    public partial class frmProdAuthorVisibleToCust : frmEditorBase
    { 
        string strSpName = "Bse_Prod_Author_Visible_Add_Edit_Del";
        string strKeyId = "-1";
        DataSet dsLoad = null;
        private string CustStr = string.Empty;
        public frmProdAuthorVisibleToCust()
        {
            InitializeComponent();
        }

        private void frmProdAuthorVisibleToCust_Load(object sender, EventArgs e)
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
        }

        private void createRepositoryItemImageComboBox()
        {
            RepositoryItemImageComboBox repositoryItemImageComboBox1 = new RepositoryItemImageComboBox();
            repositoryItemImageComboBox1.AutoHeight = false;
            repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(null, 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(null, 1, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(null, 2,2)});
            repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            repositoryItemImageComboBox1.SmallImages = imageCollection1;
            for (int i = 3; i < this.gridView1.Columns.Count; i++)
            {
                this.gridView1.Columns[i].ColumnEdit = repositoryItemImageComboBox1;
            }
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.RowHeight = 40;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
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
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,COL_SQL_NULL,COL_SQL,COL_SQL_Max,PNumber,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {strKeyId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     COL_SQL_Null.TrimEnd(','), 
                    COL_SQL.TrimEnd(','),
                    COL_SQL_Max.TrimEnd(','),
                    txtPNumber.Text.Trim(),
                     "3" };
            DataSet ds = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            this.gridView1.Columns.Clear();
            this.gridControl1.DataSource = ds.Tables[0];
            createRepositoryItemImageComboBox();
        }

        private void gridView1_EndSorting(object sender, EventArgs e)
        {
            string visibleIndex = this.gridView1.SortInfo[0].Column.VisibleIndex.ToString();
            string columnname = this.gridView1.SortInfo[0].Column.Name.Replace("col", "");
            GridColumn column = this.gridView1.SortInfo[0].Column;
            this.gridView1.ClearSorting();
            if (visibleIndex != "0" && visibleIndex != "1")
            {
                Point a = System.Windows.Forms.Form.MousePosition;
                int x = a.X;
                int y = a.Y;
                frmSelectLimits frmlimits = new frmSelectLimits(true, columnname, x, y);
                if (frmlimits.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        this.gridView1.SetRowCellValue(i, column, frmlimits.limit);
                    }
                }
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            string first = string.Empty;
            for (int j = 3; j < this.gridView1.Columns.Count; j++)
            {
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (i == gridView1.RowCount - 1)
                    {
                        first = first + "SELECT '" + gridView1.GetDataRow(i)[0].ToString() + "', '" + gridView1.GetDataRow(i)[j].ToString() + "','" + this.gridView1.Columns[j].FieldName + "','" + this.gridView1.GetDataRow(i)[1].ToString() + "','" + this.gridView1.GetDataRow(i)[2].ToString() + "'";
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
                        if (gridView1.GetDataRow(i)[j].ToString().Trim() != "0")
                        {

                            first = first + "SELECT '" + gridView1.GetDataRow(i)[0].ToString() + "', '" + gridView1.GetDataRow(i)[j].ToString() + "','" + this.gridView1.Columns[j].FieldName + "','" + this.gridView1.GetDataRow(i)[1].ToString() + "','" + this.gridView1.GetDataRow(i)[2].ToString() + "' UNION ALL ";
                        }
                    }
                }
            }
            MessageBox.Show("设置成功!");
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int visibleIndex = this.gridView1.FocusedColumn.VisibleIndex;
            string columnname = this.gridView1.FocusedColumn.Name.Replace("col", "");
            GridColumn column = this.gridView1.FocusedColumn;
            if (visibleIndex != 1 && visibleIndex != 0)
            {
                int i = this.gridView1.FocusedRowHandle;
                object a = this.gridView1.GetFocusedRowCellValue(columnname);
                if ((int)a == 0)
                {
                    this.gridView1.SetRowCellValue(i, this.gridView1.FocusedColumn, 1);
                }
                if ((int)a == 1)
                {
                    this.gridView1.SetRowCellValue(i, this.gridView1.FocusedColumn, 2);
                }
                if ((int)a == 2)
                {
                    this.gridView1.SetRowCellValue(i, this.gridView1.FocusedColumn, 0);
                }
            }
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
    }
}
