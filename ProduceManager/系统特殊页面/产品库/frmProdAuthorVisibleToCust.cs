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
using System.IO;
using System.Configuration;

namespace ProduceManager
{
    public partial class frmProdAuthorVisibleToCust : frmEditorBase
    {
        string strSpName = "Bse_Prod_Author_Visible_Add_Edit_Del";
        DataSet dsLoad;
        public frmProdAuthorVisibleToCust()
        {
            InitializeComponent();
        }
        //窗体载入
        private void frmProdAuthorVisibleToCust_Load(object sender, EventArgs e)
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
        }
        //查询
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
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,COL_SQL_NULL,COL_SQL,COL_SQL_Max,PNumber,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     COL_SQL_Null.TrimEnd(','), 
                    COL_SQL.TrimEnd(','),
                    COL_SQL_Max.TrimEnd(','),
                    txtPNumber.Text.Trim(),
                     "3" };
            DataSet ds = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            this.gridVMain.Columns.Clear();
            this.gridCMain.DataSource = ds.Tables[0];
            CreateRepositoryItemImageComboBox();
        }
        //点击列头
        private void gridVMain_EndSorting(object sender, EventArgs e)
        {
            string VisibleIdx = this.gridVMain.SortInfo[0].Column.VisibleIndex.ToString();
            string ColumnName = this.gridVMain.SortInfo[0].Column.Name.Replace("col", "");
            GridColumn Column = this.gridVMain.SortInfo[0].Column;
            this.gridVMain.ClearSorting();
            if (VisibleIdx != "0" && VisibleIdx != "1")
            {
                Point FocuseCellValue = System.Windows.Forms.Form.MousePosition;
                int x = FocuseCellValue.X;
                int y = FocuseCellValue.Y;
                frmSelectLimits frmlimits = new frmSelectLimits(true, ColumnName, x, y);
                if (frmlimits.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < gridVMain.RowCount; i++)
                    {
                        this.gridVMain.SetRowCellValue(i, Column, frmlimits.limit);
                    }
                }
            }
        }
        //点击单元格
        private void gridVMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            int VisibleIdx = this.gridVMain.FocusedColumn.VisibleIndex;
            string ColumnName = this.gridVMain.FocusedColumn.Name.Replace("col", "");
            GridColumn Column = this.gridVMain.FocusedColumn;
            if (VisibleIdx != 1 && VisibleIdx != 0)
            {
                int i = this.gridVMain.FocusedRowHandle;
                object FocuseCellValue = this.gridVMain.GetFocusedRowCellValue(ColumnName);
                if ((int)FocuseCellValue == 0)
                {
                    this.gridVMain.SetRowCellValue(i, this.gridVMain.FocusedColumn, 1);
                }
                if ((int)FocuseCellValue == 1)
                {
                    this.gridVMain.SetRowCellValue(i, this.gridVMain.FocusedColumn, 2);
                }
                if ((int)FocuseCellValue == 2)
                {
                    this.gridVMain.SetRowCellValue(i, this.gridVMain.FocusedColumn, 0);
                }
            }
        }
        //设置
        private void btnSet_Click(object sender, EventArgs e)
        {
            string SetInfoSql = string.Empty;
            for (int j = 3; j < this.gridVMain.Columns.Count; j++)
            {
                for (int i = 0; i < gridVMain.RowCount; i++)
                {
                    if (i == gridVMain.RowCount - 1)
                    {
                        SetInfoSql = SetInfoSql + "SELECT '" + gridVMain.GetDataRow(i)[0].ToString() + "', '" + gridVMain.GetDataRow(i)[j].ToString() + "','" + this.gridVMain.Columns[j].FieldName + "','" + this.gridVMain.GetDataRow(i)[1].ToString() + "','" + this.gridVMain.GetDataRow(i)[2].ToString() + "'";
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
                        if (gridVMain.GetDataRow(i)[j].ToString().Trim() != "0")
                        {

                            SetInfoSql = SetInfoSql + "SELECT '" + gridVMain.GetDataRow(i)[0].ToString() + "', '" + gridVMain.GetDataRow(i)[j].ToString() + "','" + this.gridVMain.Columns[j].FieldName + "','" + this.gridVMain.GetDataRow(i)[1].ToString() + "','" + this.gridVMain.GetDataRow(i)[2].ToString() + "' UNION ALL ";
                        }
                    }
                }
            }
            MessageBox.Show("设置成功!");
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
            string ImagePath = System.Environment.CurrentDirectory + "\\" + ConfigurationManager.AppSettings["ImageFilePath"] + "\\" + dt.Rows[0]["StylePic"].ToString() + "_ver1";
            if (File.Exists(ImagePath) && dt.Rows[0]["StylePic"].ToString().Trim() != string.Empty)
            {
                picImage.ImageLocation = ImagePath;
            }
            else
            {
                StaticFunctions.GetImageByte(dt.Rows[0]["StylePic"].ToString());
                picImage.ImageLocation = ImagePath;
            }

            lblPNumber.Text = dt.Rows[0]["PNumber"].ToString();
            lblName.Text = dt.Rows[0]["Name"].ToString();
        }
        //撤销
        private void btnRestore_Click(object sender, EventArgs e)
        {
            btnOk_Click(null, null);
        } 
        //动态为生成的列绑定
        private void CreateRepositoryItemImageComboBox()
        {
            RepositoryItemImageComboBox repImgCombox = new RepositoryItemImageComboBox();
            repImgCombox.AutoHeight = false;
            repImgCombox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            repImgCombox.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(null, 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(null, 1, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(null, 2,2)});
            repImgCombox.Name = "repImgCombox";
            repImgCombox.SmallImages = imageCollection1;
            for (int i = 3; i < this.gridVMain.Columns.Count; i++)
            {
                this.gridVMain.Columns[i].ColumnEdit = repImgCombox;
            }
            this.gridVMain.Columns[0].Visible = false;
            this.gridVMain.RowHeight = 40;
        }
    }
}
