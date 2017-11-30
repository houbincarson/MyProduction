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

namespace ProduceManager
{
    public partial class frmSelectProdMgr : frmEditorBase
    {
        public frmSelectProdMgr()
        {
            InitializeComponent();
        }
        string strSpName = "Bse_ProductManager_Business_Add_Edit_Del";
        string strKeyId = "-1";
        DataTable dt = null;
        DataTable dtProd = null;
        DataSet dsLoad = null;

        public static PrdManager.ShareWSSoapClient PrdManagerSoapClient = new PrdManager.ShareWSSoapClient();
        /// <summary>
        /// 图片存放路径，保存在App.config，只读
        /// </summary>
        public   string frmImageFilePath = System.Configuration.ConfigurationManager.AppSettings["ImageFilePath"];
        public   string frmImageReadFilePath = ConfigurationManager.AppSettings["ReadFile"];

        private void lookUpEdit_Properties_QueryPopUp(object sender, CancelEventArgs e)
        {

        }

        private void lookUpEdit_Properties_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {

        }

        //窗体载入
        private void frmSelectProdMgr_Load(object sender, EventArgs e)
        {
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {strKeyId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     "2" };
              dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);

            StaticFunctions.BindDplComboByTable(dplFy, dsLoad.Tables[0], "Name", "Fy_Id",
                 new string[] { "Fy_Id", "Name" },
                 new string[] { "编号", "名称" }, true, "", "", false);
              
            dplFy.ItemIndex = 0;
        }

        //查询
        private void btnOk_Click(object sender, EventArgs e)
        { 
            gridCMain.DataSource = GetTable(); 
        }
        private DataTable GetTable()
        {
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,CustId,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {strKeyId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                    dplFy.EditValue.ToString(),"1",
                     "1" };
              dt = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
              dtProd = new DataTable();
            if (dt != null && dt.Rows.Count > 0)
            {

                dtProd.Columns.Add("PNumber", System.Type.GetType("System.String"));
                dtProd.Columns.Add("Name", System.Type.GetType("System.String"));
                dtProd.Columns.Add("Checked", System.Type.GetType("System.Boolean"));
                dtProd.Columns.Add("PM_Id", System.Type.GetType("System.Int32"));
             //   dtProd.Columns.Add("StylePic", System.Type.GetType("System.String"));
               dtProd.Columns.Add("PhotoName", System.Type.GetType("System.Byte[]"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtProd.NewRow();
                    dr["PNumber"] = dt.Rows[i]["PNumber"].ToString();
                    dr["Name"] = dt.Rows[i]["Name"].ToString();
                    dr["Checked"] = false;
                    dr["PM_Id"] = dt.Rows[i]["PM_Id"].ToString();
                    //dr["ImgPath"] = dt.Rows[i]["ImgPath"].ToString();
                    //string imagePath = dt.Rows[i]["ImgPath"].ToString();
                    dr["PhotoName"] = getImageByte(dt.Rows[i]["StylePic"].ToString());
                    dtProd.Rows.Add(dr);
                }
            }
            return dtProd;
        }

        //返回图片的字节流byte[]
        private byte[] getImageByte(string StylePic)
        {
            //FileStream files = new FileStream(imagePath, FileMode.Open);
            //byte[] imgByte = new byte[files.Length];
            //files.Read(imgByte, 0, imgByte.Length);
            //files.Close();
            //return imgByte;
            byte[] retBytes = PrdManagerSoapClient.FileRead(frmImageReadFilePath, StylePic);
            return retBytes;
        }


        //列表模式
        private void btnListMode_Click(object sender, EventArgs e)
        {
            ChangeView(gridCMain, gridCMain.LevelTree, "GridView", true);
        }
        //卡片模式
        private void btnCardMode_Click(object sender, EventArgs e)
        {

            ChangeView(gridCMain, gridCMain.LevelTree, "CardView", true);
        }
        //组合模式
        private void btnLayoutMode_Click(object sender, EventArgs e)
        {

            ChangeView(gridCMain, gridCMain.LevelTree, "LayoutView", true);
        }

        /// <summary>转换函数
        /// 
        /// </summary>
        /// <param name="Grid"></param>
        /// <param name="node"></param>
        /// <param name="viewName"></param>
        /// <param name="removeOld"></param>
        protected virtual void ChangeView(GridControl Grid, GridLevelNode node, string viewName, bool removeOld)
        {
            GridLevelNode levelNode = node;
            if (levelNode == null) return;
            BaseView view = Grid.CreateView(viewName);
            Grid.ViewCollection.Add(view);
            BaseView prev = levelNode.LevelTemplate;
            MemoryStream ms = null;
            if (prev != null)
            {
                ms = new MemoryStream();
                prev.SaveLayoutToStream(ms, OptionsLayoutBase.FullLayout);
            }

            if (node.IsRootLevel)
            {
                prev = Grid.MainView;
                Grid.MainView = view;
            }
            else
            {
                levelNode.LevelTemplate = view;
            }
            if (ms != null)
            {
                if (removeOld && prev != null) prev.Dispose();
                ms.Seek(0, SeekOrigin.Begin);
                view.RestoreLayoutFromStream(ms, OptionsLayoutBase.FullLayout);
                ms.Close();
                MethodInfo mi = view.GetType().GetMethod("DesignerMakeColumnsVisible", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance);
                if (mi != null) mi.Invoke(view, null);
                if (prev != null) AssignViewProperties(prev, view);

            }
            if (removeOld && prev != null) prev.Dispose();
        }

        private void AssignViewProperties(BaseView prev, BaseView view)
        {
            ColumnView cprev = prev as ColumnView, cview = view as ColumnView;
            if (cprev != null && cview != null)
            {
                cview.Images = cprev.Images;
            }
        }

        string ids = string.Empty;
        private void btnComfirm_Click(object sender, EventArgs e)
        {
            ids = string.Empty;
            BaseView ViewType = gridCMain.MainView;
            if (ViewType.GetType().ToString() == "DevExpress.XtraGrid.Views.Grid.GridView")
            {
                GridView grid = ViewType as GridView;
                for (int i = 0; i < grid.RowCount; i++)
                {
                    if (grid.GetDataRow(i)["Checked"].ToString() == "True")
                    {
                        ids += grid.GetDataRow(i)["PM_Id"].ToString() + ",";
                    }
                }
            }

            if (ViewType.GetType().ToString() == "DevExpress.XtraGrid.Views.Layout.LayoutView")
            {
                LayoutView layout = ViewType as LayoutView;
                for (int i = 0; i < layout.RowCount; i++)
                {
                    if (layout.GetDataRow(i)["Checked"].ToString() == "True")
                    {
                        ids += layout.GetDataRow(i)["PM_Id"].ToString() + ",";
                    }
                }
            }

            if (ViewType.GetType().ToString() == "DevExpress.XtraGrid.Views.Card.CardView")
            {
                CardView cardView1 = ViewType as CardView;
                for (int i = 0; i < cardView1.RowCount; i++)
                {
                    if (cardView1.GetDataRow(i)["Checked"].ToString() == "True")
                    {
                        ids += cardView1.GetDataRow(i)["PM_Id"].ToString() + ",";
                    }
                }
            }
            //   MessageBox.Show("选中了" + ids.TrimEnd(','));

            if (ids.TrimEnd(',').Split(',').Length == 1 && ids.Trim().Length>0)
            {
                DataTable dtOne=dt.Select("PM_Id = '"+ids.TrimEnd(',')+"' AND Fy_Id='"+dplFy.EditValue.ToString().Trim()+"' ").CopyToDataTable();
                frmSelectProdOne frmOne = new frmSelectProdOne(dsLoad.Tables[1],  dtOne, ids.TrimEnd(','));
                if (frmOne.ShowDialog() == DialogResult.OK)
                {
                    if (frmOne.IsSelect == true)
                    {
                        dtProd.Rows.Remove(dtProd.Select("PM_Id = '" + ids.TrimEnd(',') + "' ")[0]);
                        gridCMain.DataSource = dtProd;
                    }
                }
            }
            else
            {
                //自动生成营销款号插入数据库
                frmSelectProdMany frmMany = new frmSelectProdMany(dt,ids);
                if (frmMany.ShowDialog() == DialogResult.OK)
                {
                    if (frmMany.IsSelect == true)
                    {
                        string[] pmids = ids.TrimEnd(',').Split(',');
                        for (int i = 0; i < pmids.Length; i++)
                        { 
                            dtProd.Rows.Remove(dtProd.Select("PM_Id = '" + pmids[i].ToString() + "' ")[0]);
                        }
                        gridCMain.DataSource = dtProd; 
                    }
                }
            }
        }


    }
}
