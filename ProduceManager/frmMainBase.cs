using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using System.Diagnostics;
using WcfSimpData;
using System.Configuration;
using System.IO.Ports;
using DevExpress.XtraBars.Docking;

namespace ProduceManager
{
    public partial class frmMainBase : frmEditorBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public frmMainBase()
        {
            InitializeComponent();
        }

        //菜单样式处理
        private Cursor currentCursor;
        private string skinMask = "样式: ";
        private bool skinProcessing = false;
        DevExpress.XtraBars.BarStaticItem Item12 = new BarStaticItem();
        private PrdManager.ShareWSSoapClient PrdManagerSoapClient = ServerRefManager.GetPrdManager();
        private readonly string BtProduceCS = ConfigurationManager.AppSettings["BtProduceCS"];
        private readonly string ProjectName = Convert.ToString(ConfigurationManager.AppSettings["ProjectName"]);
        private void InitMenu()
        {
            barManager1.GetController().Changed += new EventHandler(ChangedController);

            barManager1.GetController().AppearancesBar.SubMenu.Menu.Image = pictureBox1.Image;
            barManager1.GetController().AppearancesBar.SubMenu.Menu.BackColor = Color.FromArgb(50, Color.White);
            barManager1.GetController().AppearancesBar.SubMenu.SideStrip.BackColor = Color.FromArgb(90, SystemColors.Control);
            ips_Init();
            InitSkins();
        }

        #region Skins


        private void ips_Init()
        {
            BarItem item = null;
            for (int i = 0; i < barManager1.Items.Count; i++)
                if (barManager1.Items[i].Description == barManager1.GetController().PaintStyleName)
                    item = barManager1.Items[i];
            InitPaintStyle(item);
        }

        void InitSkins()
        {
            barManager1.ForceInitialize();
            if (barManager1.GetController().PaintStyleName == "Skin")
                iPaintStyle.Caption = skinMask + DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName;
            foreach (DevExpress.Skins.SkinContainer cnt in DevExpress.Skins.SkinManager.Default.Skins)
            {
                BarButtonItem item = new BarButtonItem(barManager1, skinMask + cnt.SkinName);
                iPaintStyle.AddItem(item);
                item.ItemClick += new ItemClickEventHandler(OnSkinClick);
            }
        }
        void OnSkinClick(object sender, ItemClickEventArgs e)
        {
            string skinName = e.Item.Caption.Replace(skinMask, "");
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
            barManager1.GetController().PaintStyleName = "Skin";
            iPaintStyle.Caption = e.Item.Caption;
            iPaintStyle.Hint = iPaintStyle.Caption;
            iPaintStyle.ImageIndex = -1;
        }

        #endregion

        private void ips_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barManager1.GetController().PaintStyleName = e.Item.Description;
            InitPaintStyle(e.Item);
            barManager1.GetController().ResetStyleDefaults();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetDefaultStyle();
        }

        private void InitPaintStyle(BarItem item)
        {
            if (item == null) return;
            iPaintStyle.ImageIndex = item.ImageIndex;
            iPaintStyle.Caption = item.Caption;
            iPaintStyle.Hint = item.Description;
        }

        protected void RefreshForm(bool b)
        {
            if (b)
            {
                currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                Refresh();
            }
            else
            {
                Cursor.Current = currentCursor;
            }
        }

        private void ChangedController(object sender, EventArgs e)
        {
            if (skinProcessing) return;
            string paintStyleName = barManager1.GetController().PaintStyleName;
            if ("Office2000OfficeXPWindowsXP".IndexOf(paintStyleName) >= 0)
                barManager1.Images = imageList2;
            else barManager1.Images = imageList1;
            if ("DefaultSkin".IndexOf(paintStyleName) >= 0)
                DevExpress.Skins.SkinManager.EnableFormSkins();
            else DevExpress.Skins.SkinManager.DisableFormSkins();
            skinProcessing = true;
            DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            skinProcessing = false;
        }

        private Rectangle CaptionTransform(Graphics g, Rectangle r)
        {
            g.RotateTransform(-90);
            r.X = r.X - r.Height + 5;
            r.Width = r.Height;
            return r;
        }

        private void item_PaintMenuBar(object sender, DevExpress.XtraBars.BarCustomDrawEventArgs e)
        {
            Rectangle r = e.Bounds;
            r.Inflate(1, 1);
            System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(r, Color.DarkBlue, Color.White, -90);
            e.Graphics.FillRectangle(brush, e.Bounds);
            r = CaptionTransform(e.Graphics, e.Bounds);
            Font f = new Font("Arial", 11, FontStyle.Bold);
            string caption = "XtraBars";
            e.Graphics.DrawString(caption, f, Brushes.Black, r);
            r.X -= 1; r.Y -= 1;
            e.Graphics.DrawString(caption, f, Brushes.White, r);
            e.Graphics.ResetTransform();
            e.Handled = true;
        }

        #region 动态生成菜单

        private string _MenuType = "";
        /// <summary>
        /// 菜单类型
        /// </summary>
        [Category("data"), Browsable(true), Bindable(true)]
        public string MenuType
        {
            get
            {
                return _MenuType;
            }
            set
            {
                _MenuType = value;
            }
        }

        private string _MenuSeq = "";
        /// <summary>
        /// 菜单分组标识
        /// </summary>
        [Category("data"), Browsable(true), Bindable(true)]
        public string MenuSeq
        {
            get
            {
                return _MenuSeq;
            }
            set
            {
                _MenuSeq = value;
            }
        }


        private bool blShowMenu = false;
        /// <summary>
        /// 生成固定菜单和主菜单项
        /// </summary>
        private void DisplayMenuDex()
        {
            blShowMenu = true;

            treeList1.BeginUnboundLoad();
            this.treeList1.ClearNodes();
            imageList1.Images.Clear();
            this.treeList1.BackgroundImage = this.pictureBox1.Image;
            this.treeList1.ColumnsImageList = imageList1;

            this.navBarControl1.Groups.Clear();
            this.navBarControl1.Items.Clear();
            this.navBarControl1.BackgroundImage = this.pictureBox1.Image;

            MethodRequest req = new MethodRequest();
            req.ParamKeys = null;
            req.ParamVals = null;
            req.ProceName = "Get_Bse_Menus";
            req.ProceDb = this.BtProduceCS;
            DataTable dtMenus = ServerRequestProcess.DbRequest.DataRequest_By_DataTable(req);
            dtMenus.AcceptChanges();

            string strType = string.Empty;
            int mendId = 1;
            foreach (DataRow dr in dtMenus.Rows)
            {
                if (dr["Menus_Type"].ToString().ToUpper() == "frmMainBase".ToUpper())  //主页面
                    continue;

                if (strType != dr["Menus_Type"].ToString())
                {
                    DevExpress.XtraBars.BarSubItem root_menu = null;
                    DevExpress.XtraNavBar.NavBarGroup navGrpTem = null;

                    bool blAdd = false;
                    DataRow[] drTypes = dtMenus.Select("Menus_Type = '" + dr["Menus_Type"].ToString() + "'");
                    foreach (DataRow drType in drTypes)
                    {
                        if (CApplication.App.DtAllowMenus.Select("Menu_Id = '" + drType["Menu_Id"].ToString() + "'").Length <= 0)
                            continue;

                        if (!blAdd)
                        {
                            navGrpTem = this.navBarControl1.Groups.Add(new DevExpress.XtraNavBar.NavBarGroup(dr["Menu_Type_Txt"].ToString().Trim()));

                            root_menu = new BarSubItem();
                            root_menu.Caption = dr["Menu_Type_Txt"].ToString().Trim();
                            root_menu.Tag = dr["Menus_Type"].ToString().Trim();
                            this.bar1.AddItem(root_menu);

                            blAdd = true;
                        }

                        DevExpress.XtraBars.BarButtonItem child_menu = new DevExpress.XtraBars.BarButtonItem();
                        child_menu.Caption = drType["Menus_Name"].ToString().Trim();
                        child_menu.Tag = drType["Menus_Info"].ToString().Trim();
                        child_menu.Name = drType["Menus_Class"].ToString().Trim();
                        child_menu.Id = mendId++;

                        child_menu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(subMenu_Click);
                        root_menu.ItemLinks.Add((BarItem)child_menu, drType["Is_Group"].ToString() == "True");


                        DevExpress.XtraNavBar.NavBarItem navItem = navGrpTem.AddItem().Item;
                        navItem.Caption = drType["Menus_Name"].ToString().Trim();
                        navItem.Tag = drType["Menus_Info"].ToString().Trim();
                        navItem.Name = drType["Menus_Class"].ToString().Trim();

                    }
                }
                strType = dr["Menus_Type"].ToString();
            }

            //CBizSysMenuConfig objSysMenuConfig = new CBizSysMenuConfig();
            //DataSet dsMenu = objSysMenuConfig.GetAllMenu();
            //DataRow[] root_menu_rows;
            ////SUB_SYSTEM_CID 菜单分类 : EMS箱管、FDS货代、FCS结算、SALES客服结算
            //root_menu_rows = dsMenu.Tables[0].Select("IS_ROOT = 'True' and MENU_TYPE =  '" + _MenuType + "' and SUB_SYSTEM_CID = '" + _MenuSeq + "'");

            //int memui = 1;
            //DevExpress.XtraTreeList.Nodes.TreeListNode NodeTem;
            //foreach (DataRow root_menu_row in root_menu_rows)
            //{
            //    DevExpress.XtraNavBar.NavBarGroup navGrpTem = this.navBarControl1.Groups.Add(new DevExpress.XtraNavBar.NavBarGroup(root_menu_row["MENU_NAME"].ToString().Trim()));

            //    NodeTem = treeList1.AppendNode(null, null);
            //    NodeTem.SetValue("MENU_NAME", root_menu_row["MENU_NAME"].ToString());
            //    NodeTem.SetValue("WEB_PAGE_NAME", root_menu_row["WEB_PAGE_NAME"].ToString());
            //    NodeTem.SetValue("MENU_NAME_IN_ENGLISH", root_menu_row["MENU_NAME_IN_ENGLISH"].ToString());
            //    if (root_menu_row["WEB_IMG_FILE_NAME"].ToString() != string.Empty && File.Exists(root_menu_row["WEB_IMG_FILE_NAME"].ToString()))
            //    {
            //        this.imageList1.Images.Add(new Bitmap(root_menu_row["WEB_IMG_FILE_NAME"].ToString()));
            //        NodeTem.StateImageIndex = this.imageList1.Images.Count - 1;
            //    }
            //    else
            //    {
            //        //this.imageList1.Images.Add(new Bitmap(".\\image\\csl_nrm.gif"));
            //        //NodeTem.StateImageIndex = this.imageList1.Images.Count - 1;
            //    }

            //    DevExpress.XtraBars.BarSubItem root_menu = new DevExpress.XtraBars.BarSubItem();
            //    root_menu.Caption = root_menu_row["MENU_NAME"].ToString().Trim() + "(&" + (char)((int)'A' + memui) + ")";
            //    root_menu.Tag = root_menu_row["WEB_PAGE_NAME"].ToString().Trim();
            //    root_menu.Name = root_menu_row["MENU_NAME_IN_ENGLISH"].ToString().Trim();
            //    if (root_menu_row["WEB_IMG_FILE_NAME"].ToString() != string.Empty)
            //    {
            //        root_menu.ImageIndex = this.imageList1.Images.Count - 1;
            //    }
            //    root_menu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(subMenu_Click);//menuStrip1_MenuItemClick

            //    this.bar1.AddItem(root_menu);
            //    //this.bar1.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(root_menu));
            //    this.GenerateMenu(root_menu, root_menu_row["MENU_CONFIG_ID"].ToString().Trim(), dsMenu, navGrpTem, NodeTem);

            //    memui++;
            //}

            //DevExpress.XtraBars.BarStaticItem Item11 = new BarStaticItem();
            //Item11.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            //Item11.Caption = "现在时间";
            //this.bar1.AddItem(Item11);
            ////DevExpress.XtraBars.BarStaticItem Item12 = new BarStaticItem();
            //Item12.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            //Item12.Caption = "";
            //this.bar1.AddItem(Item12);
            //UpdateShowMenuTo();
            treeList1.EndUnboundLoad();
            blShowMenu = false;
        }

        private void AddCustMenu()
        {
            //mWindows.Caption = "窗口(&Z)";
            iPaintStyle.Caption = "样式";
            //wHelp.Caption = "帮助(&X)";
            //this.bar1.AddItem(mWindows);
            this.bar1.AddItem(iPaintStyle);
            this.barSubSys.Caption = "系统";
            this.bar1.AddItem(barSubSys);
            //this.bar1.AddItem(wHelp);
            //BarItemLink blTem = this.bar1.AddItem(bar_);
            //blTem.BeginGroup = true;

            //增加当前操作界面的动态操作按纽
            //DevExpress.XtraNavBar.NavBarGroup navGrp = this.navBarControl1.Groups.Add(new DevExpress.XtraNavBar.NavBarGroup("快速操作"));
            //navGrp.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
            ////this.imageList1.Images.Add(new Bitmap(".\\image\\my_comp.gif"));
            //navGrp.SmallImageIndex = this.imageList1.Images.Count - 1;
            //navGrp.Name = "navGroupQuick";
            //navGrp.Visible = false;

            //DevExpress.XtraTreeList.Nodes.TreeListNode Node1 = treeList1.AppendNode(null, null);
            //Node1.SetValue("MENU_NAME", "快速操作");
            //Node1.SetValue("MENU_NAME_IN_ENGLISH", "TreeNodeQuick");
            //Node1.StateImageIndex = this.imageList1.Images.Count - 1;
            //Node1.Visible = false;
        }

        bool IsTabbedMdi { get { return biTabbedMDI.Down; } }

        void InitTabbedMDI()
        {
            if (AllowMerge.Checked)
                this.barManager1.MdiMenuMergeStyle = BarMdiMenuMergeStyle.Always;
            else
                this.barManager1.MdiMenuMergeStyle = BarMdiMenuMergeStyle.Never;

            xtraTabbedMdiManager1.MdiParent = IsTabbedMdi ? this : null;
            iCascade.Visibility = iTileHorizontal.Visibility = iTileVertical.Visibility = IsTabbedMdi ? BarItemVisibility.Never : BarItemVisibility.Always;

        }

        private void biTabbedMDI_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            InitTabbedMDI();
        }

        private void iCascade_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void iTileHorizontal_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void iTileVertical_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        /// <summary>
        /// 递归生成子菜单项
        /// </summary>
        /// <param name="parent_menu">主菜单项</param>
        /// <param name="parent_menu_id">主菜单项的id</param>
        /// <param name="ds_menu">主菜单项下包括其所有子项的数据集</param>
        private void GenerateMenu(DevExpress.XtraBars.BarSubItem parent_menu, string parent_menu_id, DataSet ds_menu, DevExpress.XtraNavBar.NavBarGroup navGrp, DevExpress.XtraTreeList.Nodes.TreeListNode NodeTemP)
        {
            DevExpress.XtraBars.BarSubItem ParentMenu = parent_menu;
            DataSet dsMenu = ds_menu;
            DataRow[] ItemRows = dsMenu.Tables[0].Select("PARENT_MENU_ID = '" + parent_menu_id + "'");
            DevExpress.XtraTreeList.Nodes.TreeListNode NodeTem;
            foreach (DataRow Item in ItemRows)
            {
                if (navGrp != null)
                {
                    //navGrp.AddItem().Item = navItem;
                    DevExpress.XtraNavBar.NavBarItem navItem = navGrp.AddItem().Item;
                    navItem.Caption = Item["MENU_NAME"].ToString().Trim();
                    navItem.Tag = Item["WEB_PAGE_NAME"].ToString().Trim();
                    navItem.Tag = navItem.Tag + "|" + Item["IS_OPEN"].ToString().Trim();
                    navItem.Name = Item["MENU_NAME_IN_ENGLISH"].ToString().Trim();
                    if (Item["WEB_IMG_FILE_NAME"].ToString() != string.Empty && File.Exists(Item["WEB_IMG_FILE_NAME"].ToString()))
                    {
                        this.imageList3.Images.Add(new Bitmap(Item["WEB_IMG_FILE_NAME"].ToString()));
                        navItem.LargeImageIndex = this.imageList3.Images.Count - 1;
                    }
                    else
                    {
                        //this.imageList3.Images.Add(new Bitmap(".\\image\\csl_nrm.gif"));
                        //navItem.LargeImageIndex = this.imageList3.Images.Count - 1;
                    }

                    navGrp.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsText;
                }




                //添加树型接点
                NodeTem = treeList1.AppendNode(null, NodeTemP);
                //NodeTem.Level = Item["COMPANY_CDE"].ToString();
                NodeTem.SetValue("MENU_NAME", Item["MENU_NAME"].ToString());
                NodeTem.SetValue("WEB_PAGE_NAME", Item["WEB_PAGE_NAME"].ToString());
                NodeTem.SetValue("MENU_NAME_IN_ENGLISH", Item["MENU_NAME_IN_ENGLISH"].ToString());
                if (Item["WEB_IMG_FILE_NAME"].ToString() != string.Empty && File.Exists(Item["WEB_IMG_FILE_NAME"].ToString()))
                {
                    this.imageList1.Images.Add(new Bitmap(Item["WEB_IMG_FILE_NAME"].ToString()));
                    NodeTem.StateImageIndex = this.imageList1.Images.Count - 1;
                }
                else
                {
                    //this.imageList1.Images.Add(new Bitmap(".\\image\\csl_nrm.gif"));
                    //NodeTem.StateImageIndex = this.imageList1.Images.Count - 1;
                }



                if (Item["MENU_CHILD"].ToString() == "True")
                {
                    DevExpress.XtraBars.BarSubItem child_item = new DevExpress.XtraBars.BarSubItem();
                    child_item.Caption = Item["MENU_NAME"].ToString().Trim();
                    child_item.Name = Item["MENU_NAME_IN_ENGLISH"].ToString().Trim();
                    if (Item["WEB_IMG_FILE_NAME"].ToString() != string.Empty)
                    {
                        child_item.ImageIndex = this.imageList1.Images.Count - 1;
                    }

                    BarItemLink blTem = parent_menu.AddItem(child_item);
                    //parent_menu.AddItem(child_item);
                    ///*如果要在某菜单项下面加Separator,在维护菜单时把该菜单项的
                    //  页面打开高度设置一个大于0的整数值 */
                    if (Item["WEB_PAGE_HEIGHT"].ToString().Trim() != string.Empty && int.Parse(Item["WEB_PAGE_HEIGHT"].ToString().Trim()) > 0)
                    {
                        blTem.GetBeginGroup();
                    }
                    //if (Item["WEB_PAGE_HEIGHT"].ToString().Trim() != string.Empty && int.Parse(Item["WEB_PAGE_HEIGHT"].ToString().Trim()) > 0)
                    //{
                    //    parent_menu.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(child_item, true));
                    //}
                    //else
                    //{
                    //    parent_menu.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(child_item));
                    //}
                    GenerateMenu(child_item, Item["MENU_CONFIG_ID"].ToString().Trim(), ds_menu, null, NodeTem);


                }
                else
                {
                    DevExpress.XtraBars.BarButtonItem child_item = new DevExpress.XtraBars.BarButtonItem();
                    child_item.Caption = Item["MENU_NAME"].ToString().Trim();
                    child_item.Tag = Item["WEB_PAGE_NAME"].ToString().Trim();
                    child_item.Tag = child_item.Tag + "|IS_OPEN=" + Item["IS_OPEN"].ToString().Trim();
                    child_item.Name = Item["MENU_NAME_IN_ENGLISH"].ToString().Trim();
                    if (Item["WEB_IMG_FILE_NAME"].ToString() != string.Empty)
                    {
                        child_item.ImageIndex = this.imageList1.Images.Count - 1;
                    }

                    child_item.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(subMenu_Click);//menuStrip1_MenuItemClick
                    parent_menu.AddItem(child_item);
                    //if (Item["WEB_PAGE_HEIGHT"].ToString().Trim() != string.Empty && int.Parse(Item["WEB_PAGE_HEIGHT"].ToString().Trim()) > 0)
                    //{
                    //    parent_menu.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(child_item, true));
                    //}
                    //else
                    //{
                    //    parent_menu.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(child_item));
                    //}
                }
            }
        }


        /// <summary>
        /// 菜单单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void subMenu_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                ClickMenu(e.Item.Caption, e.Item.Name, Convert.ToString(e.Item.Tag));
            }
            catch (Exception err)
            {
                MessageBox.Show("Error：" + err.Message);
            }
        }


        private string _ProjectNamespace = "";
        /// <summary>
        /// 窗口所在项目的命名空间,如 MC.Sys
        /// </summary>
        [Category("data"), Browsable(true), Bindable(true)]
        public string ProjectNamespace
        {
            get
            {
                return _ProjectNamespace;
            }
            set
            {
                _ProjectNamespace = value;
            }
        }

        private string _ApplicationMap = "";
        /// <summary>
        /// 当前操作截面的菜单路径说明，如“系统管理-常量字典”
        /// </summary>
        [Category("data"), Browsable(true), Bindable(true)]
        public string ApplicationMap
        {
            get
            {
                return _ApplicationMap;
            }
            set
            {
                _ApplicationMap = value;
                this.bsiApplicationMap.Caption = value;
            }
        }

        private string _HandleInfo = "";
        /// <summary>
        /// 当前界面的操作信息，如“货物已确认，修改货物资料”
        /// </summary>
        [Category("data"), Browsable(true), Bindable(true)]
        public string HandleInfo
        {
            get
            {
                return _HandleInfo;
            }
            set
            {
                _HandleInfo = value;
                bsiHandleInfo.Caption = value;
            }
        }
        public virtual bool ovClickMenu(string strFormCaption, string strFormClass, string strMenuParam)
        {
            return true;
        }

        public void ClickMenu(string strFormCaption, string strFormClass, string strMenuParam)
        {
            string formName = strFormClass;

            string strType = StaticFunctions.GetFrmParamValue(strMenuParam, "TYPE", new char[] { '|' });
            if (strType == "SRP")
            {
                StaticFunctions.ShowRptItem(strFormCaption, strFormClass, this);

                ////Form1|TYPE=SRP|RPU=BatarRpts/ProductRpts1|RPPARA=
                //string strRptUrl = StaticFunctions.GetFrmParamValue(strMenuParam, "RPU", new char[] { '|' });

                //frmReportServicePreview frmExist=StaticFunctions.GetExistedChildReptForm(this, strRptUrl);
                //if (frmExist != null)
                //{
                //    frmExist.Activate();
                //    return;
                //}

                ////Microsoft.Reporting.WinForms.ReportParameter[] RParameters;
                //string strReportPath = strRptUrl.StartsWith("/") ? strRptUrl : "/" + strRptUrl;
                //frmReportServicePreview objFrm = new frmReportServicePreview(strFormCaption, strReportPath, "", "");
                //objFrm.Rept_Key = strRptUrl;

                //Microsoft.Reporting.WinForms.ReportViewer reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();

                //reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
                //Microsoft.Reporting.WinForms.ServerReport SRPtem = reportViewer1.ServerReport;

                //SRPtem.ReportServerUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["ReportServices"]);//"http://172.20.28.17/ReportServer"
                //SRPtem.ReportPath = strReportPath;// "/fdcf/fVslApTruckChargeSheet";
                //Microsoft.Reporting.WinForms.ReportParameterInfoCollection RParameter = SRPtem.GetParameters();

                //string strRptParam = StaticFunctions.GetFrmParamValue(strMenuParam, "RPPARA", new char[] { '|' });
                //for (int r = 0; r < RParameter.Count; r++)
                //{
                //    try
                //    {
                //        string PRtemValue = StaticFunctions.GetFrmParamValue(strRptParam, RParameter[r].Name, new char[] { '&' });
                //        if (PRtemValue != string.Empty)
                //            objFrm.AddRpParameterItem(RParameter[r].Name, PRtemValue);
                //    }
                //    catch
                //    {
                //    }
                //}

                //objFrm.MdiParent = this;
                //objFrm.Show();
                //objFrm.InitialByParam("VIEW", strRptParam);
            }
            else if (strType == "FORM")
            {
                string strformName = StaticFunctions.GetFrmParamValue(strMenuParam, "FORMNME", new char[] { '|' });

                string strRptParam = StaticFunctions.GetFrmParamValue(strMenuParam, "RPPARA", new char[] { '|' });
                string strModeTem = StaticFunctions.GetFrmParamValue(strMenuParam, "MODE", null);


                if (strformName != string.Empty)
                    formName = strformName;

                if (strRptParam.IndexOf("BsuClassName=".ToUpper()) == -1)
                {
                    StaticFunctions.OpenChildEditorForm(true, "ProduceManager", this, strFormCaption, formName, strModeTem, strRptParam, null);
                }
                else
                {
                    string strBsuClassName = StaticFunctions.GetFrmParamValue(strRptParam, "BsuClassName", null);
                    StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", this, strFormCaption, strBsuClassName, formName, strModeTem, "BusClassName=" + formName, null);
                }
            }

            //try
            //{
            //    //tag属性在这里有用到。

            //    //if (!WFUtil.GetWebUiAction(this, strFormClass))
            //    //{
            //    //    MessageBox.Show(this, "对不起，你没有该权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    //    return;
            //    //}
            //    if(ovClickMenu(strFormCaption, strFormClass, strMenuParam))
            //        return ;

            //    string strType = WFUtil.GetFrmParamValue(Convert.ToString(strMenuParam), "TYPE", new char[] { '|' });
            //    if (strType == "SRP")
            //    {

            //        string strRptUrl = WFUtil.GetFrmParamValue(Convert.ToString(strMenuParam), "RPU", new char[] { '|' });

            //        //Microsoft.Reporting.WinForms.ReportParameter[] RParameters;
            //        string strReportPath = strRptUrl.StartsWith("/") ? strRptUrl : "/" + strRptUrl;
            //        frmReportServicePreview objFrm = new frmReportServicePreview(strFormCaption, strReportPath, "", "");

            //        Microsoft.Reporting.WinForms.ReportViewer reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();

            //        reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            //        Microsoft.Reporting.WinForms.ServerReport SRPtem = reportViewer1.ServerReport;

            //        SRPtem.ReportServerUrl = new Uri(System.Configuration.ConfigurationManager.AppSettings["ReportServices"]);//"http://172.20.28.17/ReportServer"
            //        SRPtem.ReportPath = strReportPath;// "/fdcf/fVslApTruckChargeSheet";
            //        Microsoft.Reporting.WinForms.ReportParameterInfoCollection RParameter = SRPtem.GetParameters();

            //        string strRptParam = WFUtil.GetFrmParamValue(Convert.ToString(strMenuParam), "RPPARA", new char[] { '|' });
            //        for (int r = 0; r < RParameter.Count; r++)
            //        {
            //            try
            //            {
            //                string PRtemValue = WFUtil.GetFrmParamValue(strRptParam, RParameter[r].Name, new char[] { '&' });
            //                if (PRtemValue != string.Empty)
            //                    objFrm.AddRpParameterItem(RParameter[r].Name, PRtemValue);
            //            }
            //            catch
            //            {
            //                if (RParameter[r].Name == "strUser")
            //                {
            //                    objFrm.AddRpParameterItem(RParameter[r].Name, CApplication.App.CurrentSession.UserId);
            //                }
            //                if (RParameter[r].Name == "strOfce")
            //                {
            //                    objFrm.AddRpParameterItem(RParameter[r].Name, CApplication.App.CurrentSession.UserOffice);
            //                }
            //                if (RParameter[r].Name == "strRpOfce")
            //                {
            //                    objFrm.AddRpParameterItem(RParameter[r].Name, CApplication.App.CurrentSession.UserOffice);
            //                }
            //            }
            //        }

            //        objFrm.MdiParent = this;
            //        objFrm.Show();
            //        objFrm.InitialByParam("VIEW", strRptParam);
            //        return;
            //    }
            //    if (strType == "WEB")
            //    {
            //        string strRptUrl = WFUtil.GetFrmParamValue(Convert.ToString(strMenuParam), "RPU", new char[] { '|' });
            //        string rptURL = strRptUrl.StartsWith("/") ? frmBase.UrlBase() + strRptUrl : frmEditorBase.UrlBase() + "/" + strRptUrl;

            //        if (rptURL.EndsWith("ID="))
            //        {
            //            if (CApplication.App.CurrentSession.SelectedSpmtIdS != string.Empty && CApplication.App.CurrentSession.SelectedSpmtIdS != null && CApplication.App.CurrentSession.SelectedSpmtIdS.Length > 16)
            //            {
            //                string[] strItem = CApplication.App.CurrentSession.SelectedSpmtIdS.Trim().Split(new char[] { '_' });
            //                if (strItem.Length >= 3)
            //                {
            //                    if (strItem[0].ToString() == "SALES")
            //                    {
            //                        rptURL += strItem[2].ToString();
            //                    }
            //                }
            //            }
            //        }

            //        //frmViewInternetPage objFrm = new frmViewInternetPage();
            //        //objFrm.Text = strFormCaption;
            //        //objFrm.ShowUrl(rptURL, "", "", this);
            //        return;
            //    }
            //    if (strType == "FORM")
            //    {

            //        string strformName = WFUtil.GetFrmParamValue(Convert.ToString(strMenuParam), "FORMNME", new char[] { '|' });
            //        string strRptParam = WFUtil.GetFrmParamValue(Convert.ToString(strMenuParam), "RPPARA", new char[] { '|' });
            //        string strModeTem = WFUtil.GetFrmParamValue(strRptParam, "MODE", null);
            //        string strPrPart = WFUtil.GetFrmParamValue(Convert.ToString(strMenuParam), "PRPART", new char[] { '|' });
            //        string strIsOpen = WFUtil.GetFrmParamValue(Convert.ToString(strMenuParam), "IS_OPEN", new char[] { '|' });
            //        strIsOpen = strIsOpen.ToUpper();

            //        if (strPrPart == string.Empty)
            //            strPrPart = _ProjectNamespace;
            //        if (strformName != string.Empty)
            //            strFormClass = strformName;

            //        if (!GetWebUiAction(strformName, string.Empty))
            //        {
            //            MessageBox.Show(this, "对不起，你没有该权限！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //            return;
            //        }
            //        //if ((CApplication.App.CurrentSession.strUserTypeSign == "OUTER_USER") || (CApplication.App.CurrentSession.strUserTypeSign == "") || (CApplication.App.CurrentSession.strUserTypeSign == null))
            //        //{
            //        //    MessageBox.Show("对不起，您没有该权限！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //        //    return;
            //        //}
            //        //if ((CApplication.App.CurrentSession.strUserTypeSign == "BROTHER_USER") && (strIsOpen == "" || strIsOpen=="FALSE"))
            //        //{
            //        //    MessageBox.Show("对不起，您没有该权限！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        //    return;
            //        //}
            //        WFUtil.OpenChildEditorForm(strPrPart, this, strFormCaption, strType, strFormClass, strModeTem, strRptParam, null);
            //    }
            //}
            //catch { }


            #region  点击当前菜单显示当前页面的导航信息

            //string[] strKey = "FormName".Split(",".ToCharArray());
            //string[] strVal = new string[] {
            //         formName };
            //DataSet dsLoad = this.DataRequest_By_DataSet("Bse_Navigation_Add_Edit_Del", strKey, strVal);
            //CreateLable(dsLoad);
            //CreatePictureForNav(formName);
            //oneinthree = (int)((this.dockPanel2.Height / 3) + 1);
            //if (SumHeight < oneinthree)
            //{
            //    this.groupControl2.Location = new Point(0, this.dockPanel2.Height - oneinthree);
            //    this.groupControl2.Size = new Size(this.groupControl2.Width, this.dockPanel2.Height - oneinthree * 2);
            //    this.groupControl1.Dock = DockStyle.Fill;

            //}
            //else if (SumHeight > oneinthree && SumHeight < oneinthree * 2)
            //{
            //    this.groupControl2.Location = new Point(0, this.dockPanel2.Height - SumHeight);
            //    this.groupControl2.Size = new Size(this.groupControl2.Width, SumHeight);
            //    this.groupControl1.Dock = DockStyle.Fill;
            //}
            //else
            //{
            //    this.groupControl2.Location = new Point(0, this.dockPanel2.Height - oneinthree * 2);
            //    this.groupControl2.Size = new Size(this.groupControl2.Width, oneinthree * 2);
            //    this.groupControl1.Dock = DockStyle.Fill;
            //}
            //SumHeight = 0;
            //group2Loc = this.groupControl2.Location;
            //group2Siz = this.groupControl2.Size;
            #endregion
        }

        #region 当前菜单显示当前页面的导航信息（先把#region里的全注释了140115）
        //int amountRow = 13;//在当前字体大小，GroupControl宽度的前提下一行摆放字符数
        //int heightCoumn = 16;//在当前字体大小下，一行的高度值
        //int lineHeight = 16;//两个System.Enviroment.NewLine的总高度 
        //int SumHeight = 0;
        //int oneinthree;
        //Point group2Loc = new Point();
        //Size group2Siz = new Size();
        private void groupControl2_Click(object sender, EventArgs e)
        {
        //    if (this.groupControl2.Size.Height < 100)
        //    {
        //        this.groupControl2.Location = group2Loc;
        //        this.groupControl2.Size = group2Siz;
        //    }
        //    else
        //    {
        //        Point group2NewLoc = new Point(0, this.dockPanel2.Height - 50);
        //        Size group2NewSize = new Size(this.groupControl1.Width, 50);
        //        this.groupControl2.Location = group2NewLoc;
        //        this.groupControl2.Size = group2NewSize; 
        //    }
        }
        //private void CreateLable(DataSet ds)
        //{
        //    if (ds.Tables.Count > 0 && ds != null)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        string Remark = string.Empty;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            string txt = dt.Rows[i]["CtlName"].ToString() + dt.Rows[i]["CtlRemark"].ToString();
        //            int qy = txt.Length % amountRow;//求余数
        //            if (qy > 0)
        //            {
        //                SumHeight = SumHeight + ((txt.Length / amountRow) + 1) * heightCoumn;
        //            }
        //            else
        //            {
        //                SumHeight = SumHeight + (txt.Length / amountRow) * heightCoumn;
        //            }
        //            Remark = Remark + txt + System.Environment.NewLine + System.Environment.NewLine;
        //        }
        //        memoEdit1.Text = Remark;
        //        memoEdit1.Font = new System.Drawing.Font("微软雅黑 ", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
        //        SumHeight = SumHeight + (dt.Rows.Count) * lineHeight + 27;
        //    }
        //}
        //private void CreatePictureForNav(string formName)
        //{
        //    string imgpath = System.Environment.CurrentDirectory + "\\NavigationImg\\" + formName + ".jpg";
        //    if (!File.Exists(imgpath))
        //    {
        //        //下载
        //    }
        //    this.Pic_Img.ImageLocation = imgpath;
        //}
        #endregion
        #endregion

        private void LoadOutLookSty()
        {
            foreach (object sty in navBarControl1.AvailableNavBarViews.ToArray(typeof(object)) as object[])
            {
                DevExpress.XtraNavBar.ViewInfo.BaseViewInfoRegistrator styTem = sty as DevExpress.XtraNavBar.ViewInfo.BaseViewInfoRegistrator;
                ToolStripItem menuStrTem = this.menuSelectSty.DropDown.Items.Add(styTem.ViewName);
                menuStrTem.Tag = sty;
                menuStrTem.Click += new EventHandler(menuStrTem_Click);
            }
        }

        void menuStrTem_Click(object sender, EventArgs e)
        {
            ToolStripItem menu = sender as ToolStripItem;
            this.navBarControl1.View = menu.Tag as DevExpress.XtraNavBar.ViewInfo.BaseViewInfoRegistrator;
            navBarControl1.ResetStyles();
        }

        private void frmMainBase_Load(object sender, EventArgs e)
        {
            this.dockPanel2.Visibility = DockVisibility.AutoHide;
            //DevExpress.Accessibility.AccLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressUtilsLocalizationCHS();
            //DevExpress.XtraBars.Localization.BarLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraBarsLocalizationCHS();
            //DevExpress.XtraCharts.Localization.ChartLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraChartsLocalizationCHS();
            //DevExpress.XtraEditors.Controls.Localizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraEditorsLocalizationCHS();
            //DevExpress.XtraGrid.Localization.GridLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraGridLocalizationCHS();
            //DevExpress.XtraLayout.Localization.LayoutLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraLayoutLocalizationCHS();
            //DevExpress.XtraNavBar.NavBarLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraNavBarLocalizationCHS();
            ////DevExpress.XtraPivotGrid.Localization.PivotGridLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraPivotGridLocalizationCHS();
            //DevExpress.XtraPrinting.Localization.PreviewLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraPrintingLocalizationCHS();
            ////DevExpress.XtraReports.Localization.ReportLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraReportsLocalizationCHS();
            ////DevExpress.XtraRichEdit.Localization.XtraRichEditLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraRichEditLocalizationCHS();
            ////DevExpress.XtraRichEdit.Localization.RichEditExtensionsLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraRichEditExtensionsLocalizationCHS();
            ////DevExpress.XtraScheduler.Localization.SchedulerLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraSchedulerLocalizationCHS();
            ////DevExpress.XtraScheduler.Localization.SchedulerExtensionsLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraSchedulerExtensionsLocalizationCHS();
            ////DevExpress.XtraSpellChecker.Localization.SpellCheckerLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraSpellCheckerLocalizationCHS();
            //DevExpress.XtraTreeList.Localization.TreeListLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraTreeListLocalizationCHS();
            ////DevExpress.XtraVerticalGrid.Localization.VGridLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraVerticalGridLocalizationCHS();
            ////DevExpress.XtraWizard.Localization.WizardLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraWizardLocalizationCHS();

            InitMenu();
            //InitTabbedMDI();
            InitForm();
            InitHomeOpLink();
            if (CApplication.Com_Prot == null)
            {
                CApplication.Com_Prot = new COM_ProtConnect();
                DataRow[] drCOM = CApplication.App.DtUserBasicSet.Select("SetKey='BalanceCOM'");
                if (drCOM.Length > 0)
                {
                    int baudRate = 0;
                    if (!int.TryParse(Convert.ToString(ConfigurationManager.AppSettings["BalancePort"]), out baudRate))
                    {
                        baudRate = 2400;
                    }

                    Parity par = Parity.None;
                    string strBalanceParity = Convert.ToString(ConfigurationManager.AppSettings["BalanceParity"]);
                    if (strBalanceParity != null)
                        par = (Parity)Enum.Parse(typeof(Parity), strBalanceParity);

                    StopBits stopB = StopBits.One;
                    string strBalanceStopBits = Convert.ToString(ConfigurationManager.AppSettings["BalanceStopBits"]);
                    if (strBalanceStopBits != null)
                        stopB = (StopBits)Enum.Parse(typeof(StopBits), strBalanceStopBits);

                    int dataBits = 0;
                    if (!int.TryParse(Convert.ToString(ConfigurationManager.AppSettings["BalancedataBits"]), out dataBits))
                    {
                        dataBits = 8;
                    }
                    int DobSeed = 0;
                    if (!int.TryParse(Convert.ToString(ConfigurationManager.AppSettings["DobSeed"]), out DobSeed))
                    {
                        DobSeed = 2;
                    }
                    CApplication.Com_Prot.DobSeed = DobSeed;
                    CApplication.Com_Prot.Init_BalancePort(baudRate, drCOM[0]["SetValue"].ToString(), par, stopB, dataBits);
                }

                drCOM = CApplication.App.DtUserBasicSet.Select("SetKey='CardCOM'");
                if (drCOM.Length > 0)
                {
                    CApplication.Com_Prot.Init_CodeCardPort(drCOM[0]["SetValue"].ToString());
                }
                txtComInfo.Caption = "串口:" + (CApplication.Com_Prot.IsPortOpen ? "开" : "关");
            }

            string strCust = System.Configuration.ConfigurationManager.AppSettings["SystemCustomer"];
            string strName = System.Configuration.ConfigurationManager.AppSettings["SystemName"];
            this.Text = strName + "--" + CApplication.App.CurrentSession.DeptNme;
            this.bsiApplicationMap.Caption = string.IsNullOrEmpty(strCust) ? "百泰首饰" : strCust;

            DataRow[] drSkin = CApplication.App.DtUserBasicSet.Select("SetKey='SkinName'");
            if (drSkin.Length > 0)
                InitDepSkin(drSkin[0]["SetValue"].ToString());

            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2007 Blue");
        }

        private void frmMainBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (CApplication.App.CurrentSession.HadOpenWindowsName.IndexOf("|" + this.Name + "|") != -1)
            //{
            //    CApplication.App.CurrentSession.HadOpenWindowsName = CApplication.App.CurrentSession.HadOpenWindowsName.Replace("|" + this.Name + "|", "");
            //}
        }

        private void menuSetTree_Click(object sender, EventArgs e)
        {
            this.menuSetTree.Checked = true;
            this.menuSetNav.Checked = false;
            this.treeList1.Visible = true;
            this.navBarControl1.Visible = false;

            this.menuSelectSty.Enabled = this.menuSetNav.Checked;
        }

        private void menuSetNav_Click(object sender, EventArgs e)
        {
            this.menuSetTree.Checked = false;
            this.menuSetNav.Checked = true;
            this.treeList1.Visible = false;
            this.navBarControl1.Visible = true;

            this.menuSelectSty.Enabled = this.menuSetNav.Checked;
        }

        private void InitForm()
        {
            AddCustMenu();

            DisplayMenuDex();
            this.dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            //this.dockPanel1.Controls.Add(this.treeList1);
            //this.treeList1.Dock = DockStyle.Fill;
            //this.menuSetTree.Checked = false;
            //this.menuSetNav.Checked = true;
            //this.treeList1.Visible = false;
            //this.navBarControl1.Visible = true;
            //this.treeList1.ExpandAll();

            //LoadOutLookSty();
            //this.menuSelectSty.Enabled = this.menuSetNav.Checked;

            this.barStaticItem7.Caption = CApplication.App.CurrentSession.Number + "(" + CApplication.App.CurrentSession.UserNme + ")";
            this.barStaticItem9.Caption = System.DateTime.Now.ToString();
            this.barDepartTxt.Caption = CApplication.App.CurrentSession.DeptNme;
            //UpdateShowSysLog();
        }

        private void UpdateShowMenuTo()
        {
            blShowMenu = true;
            for (int i = 0; i < this.navBarControl1.Groups.Count; i++)
            {
                if (this.navBarControl1.Groups[i].Name != "navGroupQuick")
                    this.navBarControl1.Groups[i].Visible = this.iShowMenuTo.Checked;
            }
            for (int i = 0; i < this.treeList1.Nodes.Count; i++)
            {
                if (this.treeList1.Nodes[i].RootNode["MENU_NAME_IN_ENGLISH"].ToString() != "TreeNodeQuick")
                    this.treeList1.Nodes[i].RootNode.Visible = this.iShowMenuTo.Checked;
            }
            blShowMenu = false;
        }

        private void navBarControl1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (blShowMenu)
                return;

            ClickMenu(e.Link.Item.Caption, e.Link.Item.Name, Convert.ToString(e.Link.Item.Tag));
        }

        private void bar3_LinkAdded(object sender, LinkEventArgs e)
        {
            //this.bar3.Visible = true;
        }

        private void bar3_LinkDeleted(object sender, LinkEventArgs e)
        {
            //this.bar3.Visible = false;
        }

        private void AllowMerge_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (AllowMerge.Checked)
            {
                this.biTabbedMDI.Checked = true;
                InitTabbedMDI();
            }
            else
            {
                InitTabbedMDI();
                this.LayoutMdi(MdiLayout.Cascade);
            }

        }

        private void frmMainBase_MdiChildActivate(object sender, EventArgs e)
        {
            //blShowMenu = true;
            //if (this.MdiChildren.Length <= 0)
            //{
            //    blShowMenu = false;
            //    return;
            //}

            //DevExpress.XtraNavBar.NavBarGroup navGroup = this.navBarControl1.Groups["navGroupQuick"];
            //navGroup.ItemLinks.Clear();
            //DevExpress.XtraTreeList.Nodes.TreeListNode NodePar = this.treeList1.FindNodeByFieldValue("MENU_NAME_IN_ENGLISH","TreeNodeQuick");
            //for(int r = 0;r < NodePar.Nodes.Count;r++)
            //{
            //    imageList1.Images.RemoveAt(imageList1.Images.Count - 1);
            //}
            //NodePar.Nodes.Clear();

            //frmBase frm = (frmBase)this.ActiveMdiChild;
            //if (frm == null || frm.arrCommand == null || frm.arrCommand.Length <= 0)
            //{
            //    navGroup.Visible = false;
            //    NodePar.Visible = false;
            //    this.treeList1.ExpandAll();
            //    blShowMenu = false;
            //    return;
            //}
            //DevExpress.XtraTreeList.Nodes.TreeListNode NodeTem;
            //foreach (CommandRec cRec in frm.arrCommand)
            //{
            //    //添加OutLook的快速操作动态按纽
            //    DevExpress.XtraNavBar.NavBarItem navItem = navGroup.AddItem().Item;
            //    navItem.Caption = cRec.CommText;
            //    navItem.Tag = cRec.CommParam;
            //    navItem.Name = cRec.CommNme;
            //    if (cRec.CommImage != null)
            //        navItem.LargeImage = cRec.CommImage;
            //    else
            //    {
            //        this.imageList3.Images.Add(new Bitmap(".\\image\\top_contacts_sel.gif"));
            //        navItem.LargeImageIndex = this.imageList3.Images.Count - 1;
            //    }

            //    //添加树型接点
            //    NodeTem = treeList1.AppendNode(new object[] { cRec.CommText, cRec.CommParam, cRec.CommNme }, NodePar);
            //    ////NodeTem.Level = Item["COMPANY_CDE"].ToString();
            //    //NodeTem.SetValue("MENU_NAME", cRec.CommText);
            //    //NodeTem.SetValue("WEB_PAGE_NAME", cRec.CommParam);
            //    //NodeTem.SetValue("MENU_NAME_IN_ENGLISH", cRec.CommNme);
            //    if (cRec.CommImage != null)
            //    {
            //        this.imageList1.Images.Add(cRec.CommImage);
            //        NodeTem.StateImageIndex = this.imageList1.Images.Count - 1;
            //    }
            //    else
            //    {
            //        this.imageList1.Images.Add(new Bitmap(".\\image\\top_contacts_sel.gif"));
            //        NodeTem.StateImageIndex = this.imageList1.Images.Count - 1;
            //    }

            //}
            //navGroup.Visible = true;
            //navGroup.Expanded = true;
            //NodePar.Visible = true;
            //this.treeList1.CollapseAll();
            //NodePar.ExpandAll();

            //blShowMenu = false;
        }

        private void iShowMenuTo_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            UpdateShowMenuTo();
        }

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            if (blShowMenu || this.treeList1.FocusedNode.HasChildren)
                return;

            //if (this.treeList1.FocusedNode.RootNode["MENU_NAME_IN_ENGLISH"].ToString() != "TreeNodeQuick")
            //{
            ClickMenu(this.treeList1.FocusedNode["MENU_NAME"].ToString(), this.treeList1.FocusedNode["MENU_NAME_IN_ENGLISH"].ToString(), this.treeList1.FocusedNode["WEB_PAGE_NAME"].ToString());
            //}
            //else
            //{
            //}
        }

        private void iHelp_ItemClick(object sender, ItemClickEventArgs e)
        {
            string strHelpFilePath = System.Configuration.ConfigurationManager.AppSettings["HelpFilePath"] + "\\Help\\用户手册.CHM";
            Help.ShowHelp(this, strHelpFilePath);
        }

        private void iAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            //MC.Shared.frmRegist frm = new MC.Shared.frmRegist();
            //frm.ShowDialog();
        }

        private void iReg_ItemClick(object sender, ItemClickEventArgs e)
        {
            //string strMsg = Cryptography.regIntgrity(true);
            //if (strMsg != string.Empty)
            //{
            //    MessageBox.Show("该软件已注册，使用权授予：\r\n　　" + strMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DataRow[] drSkin = CApplication.App.DtUserBasicSet.Select("SetKey='TimerId'");
            if (drSkin.Length <= 0)
                return;

            int timerCount = Convert.ToInt32(drSkin[0]["SetValue"].ToString());
            if (CApplication.App.CurrentSession.TimerId == timerCount)
            {
                timer1.Stop();

                Int64 UOld = CApplication.App.CurrentSession.UserId;
                Int64 deptOld = CApplication.App.CurrentSession.DeptId;
                frmLogin frmLg = new frmLogin();
                frmLg.IsLock = true;
                if (frmLg.ShowDialog() == DialogResult.OK &&
                    (CApplication.App.CurrentSession.UserId != UOld || CApplication.App.CurrentSession.DeptId != deptOld))
                {
                    Form[] charr = this.MdiChildren;
                    foreach (Form frm in charr)
                    {
                        frm.Close();
                    }
                    this.bar1.Reset();
                    frmMainBase_Load(null, null);
                }
                CApplication.App.CurrentSession.TimerId = 1;
                timer1.Start();
            }
            else
            {
                CApplication.TimerCount();
            }
        }

        private void UpdateShowSysLog()
        {
            this.barStaticItem1.Caption = "当前用户未订阅日asfsadasdsa志。。";
        }

        private void barButReLogin_ItemClick(object sender, ItemClickEventArgs e)
        {
            Int64 deptOld = CApplication.App.CurrentSession.UserId;
            frmLogin frmLg = new frmLogin();
            if (frmLg.ShowDialog() == DialogResult.OK)
            {
                Form[] charr = this.MdiChildren;
                foreach (Form frm in charr)
                {
                    frm.Close();
                }
                this.bar1.Reset();
                frmMainBase_Load(null, null);
            }
        }

        private void barBtnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            CApplication.ClearCSession();
            Application.Exit();
        }

        private void barEditPsw_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (string.IsNullOrEmpty(CApplication.App.CurrentSession.Number))
            {
                MessageBox.Show("请先登录.");
                frmLogin frm = new frmLogin();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    frmEditPsw frm1 = new frmEditPsw();
                    frm1.ShowDialog();
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                frmEditPsw frm = new frmEditPsw();
                frm.ShowDialog();
            }
        }

        private void barDownloadRpt_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string strUrl = ConfigurationManager.AppSettings["UpdateUrl"];
                string strFile = Application.StartupPath + "\\" + "SysRpt.zip";
                if (File.Exists(strFile))
                {
                    File.SetAttributes(strFile, FileAttributes.Normal);
                }
                File.WriteAllBytes(strFile, PrdManagerSoapClient.GetUpdateFileByte(strUrl, "SysRpt.zip"));


                //if (!FileZipUnZip.Exists())
                //{
                //    MessageBox.Show("未安装WinRAR,无法解压...");
                //    return;
                //}
                //if (!FileZipUnZip.UnRAR(Application.StartupPath, strFile, "SysRpt.rar"))
                //{
                //    MessageBox.Show("解压文件失败...");
                //    return;
                //}

                if (!FileZipUnZip.CompressFile(strFile, Application.StartupPath))
                {
                    MessageBox.Show(" 解压文件失败，按任意键退出..." + strFile);
                    return;
                }


                MessageBox.Show("成功下载文件...");
            }
            catch (Exception err)
            {
                MessageBox.Show("下载文件出错：" + err.Message);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        public void InitDepSkin(string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
            barManager1.GetController().PaintStyleName = "Skin";
            iPaintStyle.Caption = skinName;
            iPaintStyle.Hint = skinName;
            iPaintStyle.ImageIndex = -1;
        }
        public void SetDepartTxt(string Caption)
        {
            barDepartTxt.Caption = Caption;
        }
        public void SetRtDepart()
        {
            this.bar1.Reset();
            frmMainBase_Load(null, null);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            CApplication.App.CurrentSession.TimerId = 0;
            int k = msg.WParam.ToInt32();
            if (k == 121)//F10
            {
                timer1.Stop();
                barButReLogin_ItemClick(null, null);
                timer1.Start();
            }
            else if (k == 27)//Esc
            {
                Form frm = this.ActiveMdiChild;
                if (frm != null)
                {
                    frm.Close();
                    frm.Dispose();
                }
                else if (MessageBox.Show("将退出本系统，是否继续？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                ProjectSpecialSet.DoMainProcessCmdKey(ProjectName, ref msg, keyData, this, timer1);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void InitHomeOpLink()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LinkForm", Type.GetType("System.String"));
            dt.Columns.Add("Key", Type.GetType("System.String"));

            ProjectSpecialSet.InitHomeOpLink(ProjectName, dt);
            gridControl1.DataSource = dt.DefaultView;
        }

    }
}