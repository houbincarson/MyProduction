using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using System.IO;
using DevExpress.Utils;
using System.Reflection;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Card;
using System.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using DevExpress.XtraEditors.Controls;

namespace ProduceManager
{
    public partial class frmProdAuditMgr : frmEditorBase
    {
        public frmProdAuditMgr()
        {
            InitializeComponent();
        }
        //图片存放路径，保存在App.config，只读 
        public string frmImageFilePath = ConfigurationManager.AppSettings["ImageFilePath"];
        public string frmImageReadFilePath = ConfigurationManager.AppSettings["ReadFile"];
        string strSpName = "Bse_ProductManager_Business_Add_Edit_Del";
        string PmIds = string.Empty;
        DataTable dt = null;
        DataTable dtProd = null;
        DataSet dsLoad = null; 
        //窗体载入
        private void frmProdAuditMgr_Load(object sender, EventArgs e)
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     "2" };
            dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            #region 绑定工厂
            StaticFunctions.BindDplComboByTable(dplFy, dsLoad.Tables[0], "Name", "Fy_Id",
                 new string[] { "Fy_Id", "Name" },
                 new string[] { "编号", "名称" }, true, "", "", false);
            //dplFy.ItemIndex = 0;
            #endregion

            #region 绑定类别
            StaticFunctions.BindDplComboByTable(extTreePc, dsLoad.Tables[1], "Kind_Name", "Kind_Id|Parent_Kind_Id", "Kind_Key=400", new string[] { "Kind_Key=120", "Kind_Name=200" }, new string[] { "拼音", "名称" }, "Kind_Id", "Level>0", "Kind_Key", "Kind_Id", "Parent_Kind_Id", "", true);
            #endregion
        } 
        //查询
        private void btnOk_Click(object sender, EventArgs e)
        {  
            gridCMain.DataSource = GetDataSet(); 
        }  
        //列表模式
        private void btnListMode_Click(object sender, EventArgs e)
        {
            StaticFunctions.ChangeView(gridCMain, gridCMain.LevelTree, "GridView", true);
        }
        //卡片模式
        private void btnCardMode_Click(object sender, EventArgs e)
        {
            StaticFunctions.ChangeView(gridCMain, gridCMain.LevelTree, "CardView", true);
        }
        //组合模式
        private void btnLayoutMode_Click(object sender, EventArgs e)
        {
            StaticFunctions.ChangeView(gridCMain, gridCMain.LevelTree, "LayoutView", true);
        }  
        //双击图片确定选择
        private void repPicture_DoubleClick(object sender, EventArgs e)
        { 
            DataRow dr = this.CarVMain.GetFocusedDataRow();
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Pm_Id,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     dr["Bus_PM_Id"].ToString(),
                     "7" };
           DataTable dtPmId = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
           frmProdAuditMgrInfo AuditInfo = new frmProdAuditMgrInfo(dtPmId);
           AuditInfo.ShowDialog();
        }  
        private void dxPager1_PageChanged(object sender, EventArgs e)
        { 
                gridCMain.DataSource = GetDataSet();  
        } 
        private DataTable GetDataSet()
        {
            this.dxPager1.PageSize = this.dxPager1.CustPageSize;
            string PNumber = txtPNumber.Text.Trim();
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,Name,PNumber,Pc_Id,PageNum,PageSize,Flag".Split(",".ToCharArray());//BasicTechnology,BasicTechnologySurface,BseShapeFlower,BseShapeSquare,BseRound,BseFaceWidth,BseFastener,BseStructure,Flag
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     dplFy.Text.Trim()==string.Empty?"":dplFy.EditValue.ToString(), 
                        //CApplication.App.CurrentSession.Company_Id.ToString(), 
                                 CApplication.App.CurrentSession.FyId.ToString(),
                        "",//Name
                        PNumber, 
                        "",//Pc_Id 
                        this.dxPager1.PageIndex.ToString(),
                        this.dxPager1.PageSize.ToString(),
                        "6" };
            DataSet dsPager = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            dt = dsPager.Tables[1];
            dtProd = new DataTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                dtProd.Columns.Add("PNumber", System.Type.GetType("System.String"));
                dtProd.Columns.Add("Name", System.Type.GetType("System.String"));
                dtProd.Columns.Add("Checked", System.Type.GetType("System.Boolean"));
                dtProd.Columns.Add("Bus_PM_Id", System.Type.GetType("System.Int32"));
                dtProd.Columns.Add("PhotoName", System.Type.GetType("System.Byte[]"));
                dtProd.Columns.Add("StylePic", System.Type.GetType("System.String"));
                dtProd.Columns.Add("MergePNumber", System.Type.GetType("System.String"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtProd.NewRow();
                    dr["PNumber"] = dt.Rows[i]["PNumber"].ToString();
                    dr["Name"] = dt.Rows[i]["Name"].ToString();
                    dr["Checked"] = false;
                    dr["Bus_PM_Id"] = dt.Rows[i]["Bus_PM_Id"].ToString();
                    dr["MergePNumber"] = dt.Rows[i]["MergePNumber"].ToString();
                    string ImagePath = System.Environment.CurrentDirectory + "\\" + ConfigurationManager.AppSettings["ImageFilePath"] + "\\" + dt.Rows[i]["StylePic"].ToString() + "_ver1";
                    if (File.Exists(ImagePath) && dt.Rows[i]["StylePic"].ToString().Trim() != string.Empty)
                    {
                        dr["PhotoName"] = StaticFunctions.ImgToByt(Image.FromFile(ImagePath));
                    }
                    else
                    {
                        dr["PhotoName"] = StaticFunctions.GetImageByte(dt.Rows[i]["StylePic"].ToString());
                    }
                    dr["StylePic"] = dt.Rows[i]["StylePic"].ToString();
                    dtProd.Rows.Add(dr);
                }
            }
            this.dxPager1.RecordCount = int.Parse(dsPager.Tables[0].Rows[0][0].ToString());
            this.dxPager1.InitPageInfo();
            return dtProd;
        } 
    }
}
