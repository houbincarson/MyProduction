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
    public partial class frmProdFeeComfirm : frmEditorBase
    {
        string strSpName = "Bse_Prod_Fee_Comfirm_Add_Edit_Del";
        DataSet dsLoad;
        public frmProdFeeComfirm()
        {
            InitializeComponent();
        }
        //窗体载入
        private void frmProdFeeComfirm_Load(object sender, EventArgs e)
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                             //CApplication.App.CurrentSession.Company_Id.ToString(),
                                      CApplication.App.CurrentSession.FyId.ToString(),
                     "2" };
            dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);

            #region 绑定工厂
            StaticFunctions.BindDplComboByTable(dplFy, dsLoad.Tables[0], "Name", "Fy_Id",
           new string[] { "Fy_Id", "Name" },
           new string[] { "编号", "名称" }, true, "", "", false);
            #endregion

            #region 绑定类别
            StaticFunctions.BindDplComboByTable(extTreePc, dsLoad.Tables[1], "Kind_Name", "Kind_Id|Parent_Kind_Id", "Kind_Key=400", new string[] { "Kind_Key=120", "Kind_Name=200" }, new string[] { "拼音", "名称" }, "Kind_Id", "Level>0", "Kind_Key", "Kind_Id", "Parent_Kind_Id", "", true);
            #endregion
        }
        //查询
        private void btnOk_Click(object sender, EventArgs e)
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,Bus_Pc_Id,Bus_PNumber,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(), 
                     //CApplication.App.CurrentSession.Company_Id.ToString(),
                              CApplication.App.CurrentSession.FyId.ToString(),
                     extTreePc.Text.Trim()==string.Empty?"-1":    extTreePc.EditValue.ToString(),
                    txtBus_PNumber.Text.Trim(),
                     "1" };
            DataSet ds = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            this.gridCMain.DataSource = ds.Tables[0];
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
        //双击gridVMain
        private void gridVMain_DoubleClick(object sender, EventArgs e)
        {
            //string[] strKey = "EUser_Id,EDept_Id,Fy_Id,PNumber,Flag".Split(",".ToCharArray());
            //string[] strVal = new string[] {
            //         CApplication.App.CurrentSession.UserId.ToString(),
            //         CApplication.App.CurrentSession.DeptId.ToString(),
            //         CApplication.App.CurrentSession.FyId.ToString(),
            //         this.gridVMain.GetFocusedDataRow()["款式编号"].ToString(),
            //         "5" };
            //DataTable dt = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
            //string ImagePath = System.Environment.CurrentDirectory + "\\" + ConfigurationManager.AppSettings["ImageFilePath"] + "\\" + dt.Rows[0]["StylePic"].ToString() + "_ver1";
            //if (File.Exists(ImagePath) && dt.Rows[0]["StylePic"].ToString().Trim() != string.Empty)
            //{
            //    picImage.ImageLocation = ImagePath;
            //}
            //else
            //{
            //    StaticFunctions.GetImageByte(dt.Rows[0]["StylePic"].ToString());
            //    picImage.ImageLocation = ImagePath;
            //}

            //lblPNumber.Text = dt.Rows[0]["PNumber"].ToString();
            //lblName.Text = dt.Rows[0]["Name"].ToString();
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

        private void btnSynchronization_Click(object sender, EventArgs e)
        {
            if ( MessageBox.Show("您检查有调整幅度的工费了吗？你确定同步所有工费吗？同步之后我们将记录该操作","警告",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,Bus_Pc_Id,Bus_PNumber,Flag".Split(",".ToCharArray());
                string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(), 
                     //CApplication.App.CurrentSession.Company_Id.ToString(),
                              CApplication.App.CurrentSession.FyId.ToString(),
                     extTreePc.Text.Trim()==string.Empty?"-1":    extTreePc.EditValue.ToString(),
                    txtBus_PNumber.Text.Trim(),
                     "3" };
                DataTable dt = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
                if (dt!=null&&dt.Rows[0][0].ToString()=="OK")
                {
                    MessageBox.Show("同步成功"); 
                }
                else
                {
                    MessageBox.Show("同步失败");
                    return;
                }
            }
        }

        private void lblGoTo_Click(object sender, EventArgs e)
        {
            frmProdFeeComfirm_Log log = new frmProdFeeComfirm_Log();
            log.ShowDialog();
        }
    }
}
