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

namespace ProduceManager
{
    public partial class frmSelectProdMgr : frmEditorBase
    {
        public frmSelectProdMgr()
        {
            InitializeComponent();
        }
        //图片存放路径，保存在App.config，只读 
        public string frmImageFilePath = System.Configuration.ConfigurationManager.AppSettings["ImageFilePath"].ToString();
        public string frmImageReadFilePath = ConfigurationManager.AppSettings["ReadFile"]; 
        string strSpName = "Bse_ProductManager_Business_Add_Edit_Del";
        string PmIds = string.Empty;
        DataTable dt = null;
        DataTable dtProd = null;
        DataSet dsLoad = null;

        //窗体载入
        private void frmSelectProdMgr_Load(object sender, EventArgs e)
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
            dplFy.ItemIndex = 0;
            #endregion

            #region 绑定类别
            StaticFunctions.BindDplComboByTable(extTreePc, dsLoad.Tables[1], "Kind_Name", "Kind_Id|Parent_Kind_Id", "Kind_Key=400", new string[] { "Kind_Key=120", "Kind_Name=200" }, new string[] { "拼音", "名称" }, "Kind_Id", "Level>0", "Kind_Key", "Kind_Id", "Parent_Kind_Id", "", true);
            #endregion
        }
        //查询
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.grpPic.Controls.Count > 0)
            {
                if (DialogResult.No == MessageBox.Show("您的图片区还有挑选的款未添加到产品库，是否添加？", "提示", MessageBoxButtons.YesNo))
                {
                    //gridCMain.DataSource = GetTable(); 
                    //this.groupControl1.Controls.Clear();
                    btnReSet_Click(null, null);
                }
                else
                {
                    btnComfirm_Click(null, null);
                }
            }
            else
            {
                gridCMain.DataSource = GetDataSet();//GetTable();
                this.grpPic.Controls.Clear();
            }
        }
        private DataTable GetTable()
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                    dplFy.EditValue.ToString(),
                 //CApplication.App.CurrentSession.Company_Id.ToString(),
                      CApplication.App.CurrentSession.FyId.ToString(),
                     "1" };
            dt = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
            dtProd = new DataTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                dtProd.Columns.Add("PNumber", System.Type.GetType("System.String"));
                dtProd.Columns.Add("Name", System.Type.GetType("System.String"));
                dtProd.Columns.Add("Checked", System.Type.GetType("System.Boolean"));
                dtProd.Columns.Add("PM_Id", System.Type.GetType("System.Int32"));
                dtProd.Columns.Add("PhotoName", System.Type.GetType("System.Byte[]"));
                dtProd.Columns.Add("StylePic", System.Type.GetType("System.String"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtProd.NewRow();
                    dr["PNumber"] = dt.Rows[i]["PNumber"].ToString();
                    dr["Name"] = dt.Rows[i]["Name"].ToString();
                    dr["Checked"] = false;
                    dr["PM_Id"] = dt.Rows[i]["PM_Id"].ToString();
                    string ImagePath = System.Environment.CurrentDirectory + "\\" + ConfigurationManager.AppSettings["ImageFilePath"] + "\\" + dt.Rows[i]["StylePic"].ToString() + "_ver1";
                    if (File.Exists(ImagePath) && dt.Rows[i]["StylePic"].ToString().Trim() != string.Empty)
                    {
                        dr["PhotoName"] = StaticFunctions.ImgToByt(Image.FromFile(ImagePath));
                    }
                    else
                    {
                        if (dt.Rows[i]["StylePic"].ToString().Trim() == string.Empty)
                        {
                            dr["PhotoName"] = null;
                        }
                        else
                        {
                            StaticFunctions.GetImageByte(dt.Rows[i]["StylePic"].ToString());
                            dr["PhotoName"] = StaticFunctions.ImgToByt(Image.FromFile(ImagePath));
                        }
                    }
                    dr["StylePic"] = dt.Rows[i]["StylePic"].ToString();
                    dtProd.Rows.Add(dr);
                }
            }
            return dtProd;
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
        //确定挑选
        private void btnComfirm_Click(object sender, EventArgs e)
        {
            PmIds = string.Empty;
            //BaseView ViewType = gridCMain.MainView;
            //if (ViewType.GetType().ToString() == "DevExpress.XtraGrid.Views.Grid.GridView")
            //{
            //    GridView grid = ViewType as GridView;
            //    for (int i = 0; i < grid.RowCount; i++)
            //    {
            //        if (grid.GetDataRow(i)["Checked"].ToString() == "True")
            //        {
            //            PmIds += grid.GetDataRow(i)["PM_Id"].ToString() + ",";
            //        }
            //    }
            //}

            //if (ViewType.GetType().ToString() == "DevExpress.XtraGrid.Views.Layout.LayoutView")
            //{
            //    LayoutView layout = ViewType as LayoutView;
            //    for (int i = 0; i < layout.RowCount; i++)
            //    {
            //        if (layout.GetDataRow(i)["Checked"].ToString() == "True")
            //        {
            //            PmIds += layout.GetDataRow(i)["PM_Id"].ToString() + ",";
            //        }
            //    }
            //}

            //if (ViewType.GetType().ToString() == "DevExpress.XtraGrid.Views.Card.CardView")
            //{
            //    CardView cardView1 = ViewType as CardView;
            //    for (int i = 0; i < cardView1.RowCount; i++)
            //    {
            //        if (cardView1.GetDataRow(i)["Checked"].ToString() == "True")
            //        {
            //            PmIds += cardView1.GetDataRow(i)["PM_Id"].ToString() + ",";
            //        }
            //    }
            //}

            foreach (Control item in this.grpPic.Controls)
            {
                if (item.Name.Contains("btn"))
                {
                    PmIds = PmIds + item.TabIndex.ToString().Trim() + ",";
                }
            } 
            //if (PmIds.TrimEnd(',').Split(',').Length == 1 && PmIds.Trim().Length > 0)
            //{
            //    DataTable dtOne = dt.Select("PM_Id = '" + PmIds.TrimEnd(',') + "' AND Fy_Id='" + dplFy.EditValue.ToString().Trim() + "' ").CopyToDataTable();
            //    frmSelectProdOne frmOne = new frmSelectProdOne(dsLoad.Tables[1], dtOne, PmIds.TrimEnd(','));
            //    if (frmOne.ShowDialog() == DialogResult.OK)
            //    {
            //        if (frmOne.IsSelect == true)
            //        {
            //            dtProd.Rows.Remove(dtProd.Select("PM_Id = '" + PmIds.TrimEnd(',') + "' ")[0]);
            //            gridCMain.DataSource = dtProd;
            //        }
            //    }
            //}
            //else
            //{
            //自动生成营销款号插入数据库
            if (PmIds == string.Empty)
            {
                MessageBox.Show("请先选择款式");
                return;
            }

            frmSelectProdMany frmMany = new frmSelectProdMany(dt, PmIds);
            if (frmMany.ShowDialog() == DialogResult.OK)
            {
                if (frmMany.IsSelect == true)
                {
                    string[] pmPmIds = PmIds.TrimEnd(',').Split(',');
                    for (int i = 0; i < pmPmIds.Length; i++)
                    {
                        dtProd.Rows.Remove(dtProd.Select("PM_Id = '" + pmPmIds[i].ToString() + "' ")[0]);
                    }
                    gridCMain.DataSource = dtProd;
                }

                this.grpPic.Controls.Clear();
                x = 10;
                y = 25;
                path = string.Empty;
            }
            //}
        }
        //撤销挑选
        private void btnReSet_Click(object sender, EventArgs e)
        {
            this.grpPic.Controls.Clear();
            for (int j = 0; j < LayVMain.RowCount; j++)
            {
                if (LayVMain.GetDataRow(j)["Checked"].ToString() == "True")
                {
                    LayVMain.GetDataRow(j)["Checked"] = false;
                }
            }
            i = 1;
            x = 10;
            y = 25;
        }
        //勾选图片，确定选择
        private void LayVMain_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            DataRow dr = this.LayVMain.GetFocusedDataRow();
            string stylepic = dr["StylePic"].ToString();
            string pm_id = dr["PM_Id"].ToString();
            if (dr["Checked"].ToString() == "False")
            {
                GenerateImg(stylepic, pm_id);
                dr["Checked"] = true;
                dtProd.AcceptChanges();
            }
            else
            {
                AddRemovePic("", pm_id);
                dr["Checked"] = false;
                dtProd.AcceptChanges();
            }
        }
        //双击图片，去除选择
        private void repPicture_DoubleClick(object sender, EventArgs e)
        {

            DataRow dr = this.LayVMain.GetFocusedDataRow();
            string stylepic = dr["StylePic"].ToString();
            string pm_id = dr["PM_Id"].ToString();
            if (Convert.ToBoolean(dr["Checked"].ToString()) == false)
            {
                GenerateImg(stylepic, pm_id);
                dr["Checked"] = true;
                dtProd.AcceptChanges();
            }
            else
            {
                AddRemovePic("", pm_id);
                dr["Checked"] = false;
                dtProd.AcceptChanges();
            }



        }

        #region   生成缩略图

        int i = 1;
        int x = 10;
        int y = 25;
        string path = string.Empty;

        private void GenerateImg(string StylePic, string PmId)
        {
            string ImagePath = System.Environment.CurrentDirectory + "\\" + ConfigurationManager.AppSettings["ImageFilePath"] + "\\" + StylePic + "_ver1";

            //byte[] bytes = StaticFunctions.ImgToByt(Image.FromFile(ImagePath));
            //if (bytes == null)
            //{
            //    MessageBox.Show("不存在图片，不能生成缩略图");
            //    return;
            //}

            int width = this.grpPic.Width;
            PictureBox pic = new PictureBox();
            pic.Size = new Size(120, 100);
            pic.Name = "picture" + i.ToString();
            pic.Tag = path;
            if (x + pic.Width > width)
            {
                x = 10;
                y += 165;
            }
            if (y > this.grpPic.Height)
            {
                this.grpPic.Height = y + 10;
            }
            Button btn = new Button();
            btn.Name = "btn" + i;
            btn.Tag = btn.Name + "#" + PmId;
            btn.Text = "删除";
            btn.TabIndex = int.Parse(PmId);
            btn.Click += new EventHandler(btnClick);
            Label lbl = new Label();
            lbl.Name = "lbl" + i;
            lbl.Text = "显示顺序";
            lbl.AutoSize = true;
            lbl.Visible = false;

            pic.Location = new System.Drawing.Point(x, y);
            btn.Location = new Point(x + 18, y + 110);// 130 
            lbl.Location = new Point(x + 5, y + 110);
            lbl.Tag = (x.ToString() + "," + y.ToString());//记录当前XY

            string[] imgName = path.Split('\\');
            string img = imgName[imgName.Length - 1].ToString();
            pic.Name = "picture" + i;
            // Bitmap bmp = (Bitmap)Image.FromFile(ImagePath);
            pic.ImageLocation = ImagePath;
            //pic.Image = StaticFunctions.GetImageByBytes(bytes);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            this.grpPic.Controls.Add(pic);
            this.grpPic.Controls.Add(btn);
            this.grpPic.Controls.Add(lbl);
            x += pic.Width + 10;
            i++;
        }

        protected void btnClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string name = btn.Name.ToString();
            string index = name.Replace("btn", "");
            string picname = "picture" + index.Trim();
            string lblname = "lbl" + index.Trim();
            Control pic = this.grpPic.Controls.Find(picname, true)[0];
            Control lbl = this.grpPic.Controls.Find(lblname, true)[0];
            int count = this.grpPic.Controls.Count;
            int maxindex = 0;
            if (count > 3)
            {
                foreach (Control item in this.grpPic.Controls)
                {
                    if (item.Name.Contains("lbl"))
                    {
                        maxindex = Convert.ToInt32(item.Name.Replace("lbl", "").Trim());
                    }
                }
            }

            //1，只剩下一个
            if (count == 3)
            {
                this.grpPic.Controls.Remove(btn);
                this.grpPic.Controls.Remove(pic);
                this.grpPic.Controls.Remove(lbl);
                i = 1;
                x = 10;
                y = 25;
            }
            //2，不止一个，但是删除的是最后一个
            if (count > 3 && index == maxindex.ToString())
            {
                this.grpPic.Controls.Remove(btn);
                this.grpPic.Controls.Remove(pic);
                this.grpPic.Controls.Remove(lbl);
                i = maxindex;
                string[] xy = lbl.Tag.ToString().Split(',');
                x = Convert.ToInt32(xy[0].Trim());
                y = Convert.ToInt32(xy[1].Trim());

            }

            //3,不止一个，删除的不是最后一个
            if (count > 3 && Convert.ToInt32(index) < maxindex)
            {
                i = maxindex;
                Control lblmax = this.grpPic.Controls.Find("lbl" + maxindex.ToString(), true)[0];
                string[] xy = lblmax.Tag.ToString().Split(',');
                x = Convert.ToInt32(xy[0].Trim());
                y = Convert.ToInt32(xy[1].Trim());

                for (int j = maxindex; j > Convert.ToInt32(index); j--)
                {
                    Control picMove = this.grpPic.Controls.Find("picture" + j.ToString(), true)[0];
                    Control lblMove = this.grpPic.Controls.Find("lbl" + j.ToString(), true)[0];
                    Control btnMove = this.grpPic.Controls.Find("btn" + j.ToString(), true)[0];

                    Control picPre = this.grpPic.Controls.Find("picture" + (j - 1).ToString(), true)[0];
                    Control lblPre = this.grpPic.Controls.Find("lbl" + (j - 1).ToString(), true)[0];
                    Control btnPre = this.grpPic.Controls.Find("btn" + (j - 1).ToString(), true)[0];

                    picMove.Location = picPre.Location;
                    picMove.Name = picPre.Name;
                    lblMove.Location = lblPre.Location;
                    lblMove.Name = lblPre.Name;
                    lblMove.Tag = lblPre.Tag;
                    btnMove.Location = btnPre.Location;
                    btnMove.Name = btnPre.Name;
                    btnMove.Tag = btnPre.Name + "#" + btnMove.Tag.ToString().Trim().Split('#')[1].ToString();
                }
                this.grpPic.Controls.Remove(btn);
                this.grpPic.Controls.Remove(pic);
                this.grpPic.Controls.Remove(lbl);
            }
            this.dtProd.Select("PM_Id =" + btn.Tag.ToString().Trim().Split('#')[1].ToString())[0]["Checked"] = false;
            this.dtProd.AcceptChanges();
        }
        #endregion

        private string GetDelBtnName(string pmid)
        {
            string name = string.Empty;
            foreach (Control item in this.grpPic.Controls)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.Button")
                {
                    if (item.Tag.ToString().Contains("#" + pmid))
                    {
                        name = item.Tag.ToString().Replace("#" + pmid, "").Trim();
                        break;
                    }
                }
            }
            return name;
        }

        private void AddRemovePic(string name, string pm_id)
        {
            int count = this.grpPic.Controls.Count;
            name = GetDelBtnName(pm_id);
            if (name == "")
            {
                return;
            }
            Control btn = this.grpPic.Controls.Find(name, true)[0];
            string index = name.Replace("btn", "");
            string picname = "picture" + index.Trim();
            string lblname = "lbl" + index.Trim();
            Control pic = this.grpPic.Controls.Find(picname, true)[0];
            Control lbl = this.grpPic.Controls.Find(lblname, true)[0];
            int maxindex = 0;
            if (count > 3)
            {
                foreach (Control item in this.grpPic.Controls)
                {
                    if (item.Name.Contains("lbl"))
                    {
                        maxindex = Convert.ToInt32(item.Name.Replace("lbl", "").Trim());
                    }
                }
            }

            //1，只剩下一个
            if (count == 3)
            {
                this.grpPic.Controls.Remove(btn);
                this.grpPic.Controls.Remove(pic);
                this.grpPic.Controls.Remove(lbl);
                i = 1;
                x = 10;
                y = 25;
            }
            //2，不止一个，但是删除的是最后一个
            if (count > 3 && index == maxindex.ToString())
            {
                this.grpPic.Controls.Remove(btn);
                this.grpPic.Controls.Remove(pic);
                this.grpPic.Controls.Remove(lbl);
                i = maxindex;
                string[] xy = lbl.Tag.ToString().Split(',');
                x = Convert.ToInt32(xy[0].Trim());
                y = Convert.ToInt32(xy[1].Trim());
            }

            //3,不止一个，删除的不是最后一个
            if (count > 3 && Convert.ToInt32(index) < maxindex)
            {
                i = maxindex;
                Control lblmax = this.grpPic.Controls.Find("lbl" + maxindex.ToString(), true)[0];
                string[] xy = lblmax.Tag.ToString().Split(',');
                x = Convert.ToInt32(xy[0].Trim());
                y = Convert.ToInt32(xy[1].Trim());

                for (int j = maxindex; j > Convert.ToInt32(index); j--)
                {
                    Control picMove = this.grpPic.Controls.Find("picture" + j.ToString(), true)[0];
                    Control lblMove = this.grpPic.Controls.Find("lbl" + j.ToString(), true)[0];
                    Control btnMove = this.grpPic.Controls.Find("btn" + j.ToString(), true)[0];

                    Control picPre = this.grpPic.Controls.Find("picture" + (j - 1).ToString(), true)[0];
                    Control lblPre = this.grpPic.Controls.Find("lbl" + (j - 1).ToString(), true)[0];
                    Control btnPre = this.grpPic.Controls.Find("btn" + (j - 1).ToString(), true)[0];

                    picMove.Location = picPre.Location;
                    picMove.Name = picPre.Name;
                    lblMove.Location = lblPre.Location;
                    lblMove.Name = lblPre.Name;
                    lblMove.Tag = lblPre.Tag;
                    btnMove.Location = btnPre.Location;
                    btnMove.Name = btnPre.Name;
                    btnMove.Tag = btnPre.Name + "#" + btnMove.Tag.ToString().Trim().Split('#')[1].ToString();
                }
                this.grpPic.Controls.Remove(btn);
                this.grpPic.Controls.Remove(pic);
                this.grpPic.Controls.Remove(lbl);
            }
        }

        private void dxPager1_PageChanged(object sender, EventArgs e)
        {
            if (this.grpPic.Controls.Count > 0)
            {
                if (DialogResult.No == MessageBox.Show("您的图片区还有挑选的款未添加到产品库，是否添加？", "提示", MessageBoxButtons.YesNo))
                { 
                    btnReSet_Click(null, null);
                }
                else
                {
                    btnComfirm_Click(null, null);
                }
            }
            else
            { 
                gridCMain.DataSource = GetDataSet();
                this.grpPic.Controls.Clear();
            }
        }

        private DataTable GetDataSet()
        {

            this.dxPager1.PageSize = this.dxPager1.CustPageSize;
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,PageSize,PageNum,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                    dplFy.EditValue.ToString(),
                      //CApplication.App.CurrentSession.Company_Id.ToString(),
                               CApplication.App.CurrentSession.FyId.ToString(),
                      dxPager1.PageSize.ToString(),
                      dxPager1.PageIndex.ToString(),
                     "5" };
            DataSet dsPager = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            dt = dsPager.Tables[1];
            dtProd = new DataTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                dtProd.Columns.Add("PNumber", System.Type.GetType("System.String"));
                dtProd.Columns.Add("Name", System.Type.GetType("System.String"));
                dtProd.Columns.Add("Checked", System.Type.GetType("System.Boolean"));
                dtProd.Columns.Add("PM_Id", System.Type.GetType("System.Int32"));
                dtProd.Columns.Add("PhotoName", System.Type.GetType("System.Byte[]"));
                dtProd.Columns.Add("StylePic", System.Type.GetType("System.String"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dtProd.NewRow();
                    dr["PNumber"] = dt.Rows[i]["PNumber"].ToString();
                    dr["Name"] = dt.Rows[i]["Name"].ToString();
                    dr["Checked"] = false;
                    dr["PM_Id"] = dt.Rows[i]["PM_Id"].ToString();
                    string ImagePath = System.Environment.CurrentDirectory + "\\" + ConfigurationManager.AppSettings["ImageFilePath"] + "\\" + dt.Rows[i]["StylePic"].ToString() + "_ver1";
                    if (File.Exists(ImagePath) && dt.Rows[i]["StylePic"].ToString().Trim() != string.Empty)
                    {
                        dr["PhotoName"] = StaticFunctions.ImgToByt(Image.FromFile(ImagePath));
                    }
                    else
                    {
                        if (dt.Rows[i]["StylePic"].ToString().Trim() == string.Empty)
                        {
                            dr["PhotoName"] = null;
                        }
                        else
                        {
                            dr["PhotoName"] = StaticFunctions.GetImageByte(dt.Rows[i]["StylePic"].ToString());
                        }
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
