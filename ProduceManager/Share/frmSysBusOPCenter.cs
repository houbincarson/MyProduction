using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTab;

namespace ProduceManager
{
    public partial class frmSysBusOPCenter : frmEditorBase
    {
        #region private Params
        private bool blInitBound = false;
        private DataSet dsLoad = null;
        private DataTable dtConst = null;
        private DataTable dtShow = null;
        private DataTable dtBtns = null;
        private DataTable dtTabs = null;
        private DataTable dtGroupC = null;
        private DataTable dtSte = null;
        private DataTable dtContr = null;
        private DataTable dtBtnsM = null;
        private DataTable dtSp = null;
        private bool blSysProcess = false;
        private string strSpRptName = "Rpt_RS_DS";

        private DataSet dsFormAdt = null;
        private DataRow drMain = null;

        private string strSpName = string.Empty;
        private string strBusClassName = string.Empty;
        private string strAllowList = string.Empty;

        private List<BarButtonItem> lisBarItems = new List<BarButtonItem>();
        private Dictionary<string, List<SimpleButton>> lisBtns = new Dictionary<string, List<SimpleButton>>();//以编辑Tab对应的GridViewName为Key，保存所属的Btn
        private List<SimpleButton> lisBtnAlls = new List<SimpleButton>();//页面中所有的SimpleButton
        private List<GridView> GvNeedGCEdit = new List<GridView>();//所有需要编辑区的GridView，定制gridVInfo_FocusedRowChanged
        private Dictionary<string, GridView> gcItems = new Dictionary<string, GridView>();//所有子表的GridView集合，不包括gridVMain，key为GridViewName
        private List<RepositoryItemImageEdit> lisrepImg = new List<RepositoryItemImageEdit>();//所有子表GridView中要显示图片列的EditType
        private Dictionary<string, Control> lisGcQuerys = new Dictionary<string, Control>();//明细查询面板

        private Dictionary<string, GridCheckMarksSelection> gcGridCheckSels = new Dictionary<string, GridCheckMarksSelection>();
        #endregion

        public frmSysBusOPCenter()
        {
            InitializeComponent();
        }
        private void lookUpEdit_Properties_QueryPopUp(object sender, CancelEventArgs e)
        {
            if (sender is DevExpress.XtraEditors.LookUpEdit)
            {
                DevExpress.XtraEditors.LookUpEdit dpl = sender as DevExpress.XtraEditors.LookUpEdit;
                if (!dpl.Properties.DisplayMember.Equals("Number"))
                {
                    dpl.Properties.DisplayMember = "Number";
                }
            }
            else if (sender is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit dpl = sender as DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit;
                if (!dpl.DisplayMember.Equals("Number"))
                {
                    dpl.DisplayMember = "Number";
                }
            }
        }
        private void lookUpEdit_Properties_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if (sender is DevExpress.XtraEditors.LookUpEdit)
            {
                DevExpress.XtraEditors.LookUpEdit dpl = sender as DevExpress.XtraEditors.LookUpEdit;
                if (!dpl.Properties.DisplayMember.Equals("Name"))
                {
                    dpl.Properties.DisplayMember = "Name";
                }
            }
            else if (sender is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit dpl = sender as DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit;
                if (!dpl.DisplayMember.Equals("Name"))
                {
                    dpl.DisplayMember = "Name";
                }
            }
        }
        private void ucp_onClosePopUp(object sender, DataRow drReturn)
        {
        }

        private void frmSysBseManger_Load(object sender, EventArgs e)
        {
            ChkRight();

            xtabItemInfo_SelectedPageChanged(xtabItemInfo, new TabPageChangedEventArgs(null, xtabItemInfo.SelectedTabPage));
        }
        private void ChkRight()
        {
            //foreach (BarItem item in bar2.Manager.Items)
            //{
            //    if (strAllowList.IndexOf(item.Name + "=") >= 0)
            //    {
            //        item.Visibility = BarItemVisibility.Always;
            //    }
            //}
        }

        private void TxtQ_Enter(object sender, EventArgs e)
        {
            strFocusedContrName = (sender as Control).Name;
            FocusedControl = sender as Control;
        }
        public override void InitialByParam(string Mode, string strParam, DataTable dt)
        {
            base.InitialByParam(Mode, strParam, dt);

            strBusClassName = StaticFunctions.GetFrmParamValue(strParam, "BusClassName", null);
            if (strBusClassName == string.Empty)
                return;

            DataRow[] drs = CApplication.App.DtAllowMenus.Select("Menus_Class='" + strBusClassName + "'");
            if (drs.Length <= 0)
                return;

            strAllowList = drs[0]["Allowed_Operator"].ToString();
            InitContr();
        }
        private void InitContr()
        {
            if (dsLoad != null)
                return;

            dsFormAdt = this.GetFrmLoadDsAdt(strBusClassName);
            dsFormAdt.AcceptChanges();
            dsLoad = this.GetFrmLoadDsNew(strBusClassName);
            dsLoad.AcceptChanges();
            dtShow = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];

            drMain = dsLoad.Tables[2].Rows[0];
            strSpName = drMain["SpName"].ToString();

            dtBtns = dsLoad.Tables[3];
            dtTabs = dsLoad.Tables[4];
            dtGroupC = dsLoad.Tables[5];
            dtSte = dsLoad.Tables[6];
            dtContr = dsLoad.Tables[7];
            dtBtnsM = dsLoad.Tables[8];
            dtSp = dsLoad.Tables[9];

            blInitBound = true;
            Rectangle rect = SystemInformation.VirtualScreen;
            lisBarItems = StaticFunctions.ShowBarButtonItem(dtBtns, bar2, "bar2", strAllowList, imageList1);
            foreach (BarButtonItem item in lisBarItems)
            {
                item.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            }
            gcItems = StaticFunctions.ShowTabItem(dtTabs, dtBtns, xtabItemInfo, "xtabItemInfo", strAllowList, lisrepImg, GvNeedGCEdit, lisBtnAlls, lisBtns, imageList1, true, lisGcQuerys);

            foreach (string strGv in gcItems.Keys)
            {
                StaticFunctions.ShowGridControl(gcItems[strGv], dtShow, dtConst);

                DataRow[] drTabs = dtTabs.Select("IsAddChildGv=1 AND GridViewName='" + strGv + "'");
                if (drTabs.Length == 1)
                {
                    GridView gvChild = StaticFunctions.ShowGridVChildGv(strGv + "Com", gcItems[strGv].GridControl, dtShow, dtConst);
                    StaticFunctions.SetGridViewStyleFormatCondition(gvChild, dtBtnsM);
                }
                StaticFunctions.SetGridViewStyleFormatCondition(gcItems[strGv], dtBtnsM);

                drTabs = dtTabs.Select("ShowGridChkSel=1 AND GridViewName='" + strGv + "'");
                if (drTabs.Length == 1)
                {
                    GridCheckMarksSelection gc = new GridCheckMarksSelection(gcItems[strGv]);
                    gc.StrGridKeyField = drTabs[0]["EditInfoKeyId"].ToString();
                    gcGridCheckSels.Add(strGv, gc);
                }
            }
            foreach (RepositoryItemImageEdit rep in lisrepImg)
            {
                rep.Popup += new System.EventHandler(this.repImg_Popup);
            }
            #region gcQuery
            int igcHeight;
            foreach (Control ctrlQuery in lisGcQuerys.Values)
            {
                List<Control> lisGcContrsQuery = StaticFunctions.ShowGroupControl(ctrlQuery, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
                foreach (Control ctrl in lisGcContrsQuery)
                {
                    ctrl.Enter += new System.EventHandler(this.TxtQ_Enter);
                    switch (ctrl.GetType().ToString())
                    {
                        case "DevExpress.XtraEditors.TextEdit":
                            break;

                        case "DevExpress.XtraEditors.CheckEdit":
                            break;

                        case "DevExpress.XtraEditors.LookUpEdit":
                            LookUpEdit dpl = ctrl as LookUpEdit;
                            dpl.Properties.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.lookUpEdit_Properties_QueryPopUp);
                            dpl.Properties.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.lookUpEdit_Properties_Closed);
                            break;

                        case "DevExpress.XtraEditors.CheckedComboBoxEdit":
                            CheckedComboBoxEdit chkdpl = ctrl as CheckedComboBoxEdit;
                            chkdpl.Properties.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.lookUpEdit_Properties_Closed);
                            break;

                        case "DevExpress.XtraEditors.DateEdit":
                            break;

                        case "ProduceManager.UcTxtPopup":
                            ProduceManager.UcTxtPopup ucp = ctrl as ProduceManager.UcTxtPopup;
                            ucp.onClosePopUp += new UcTxtPopup.ClosePopUp(ucp_onClosePopUp);
                            break;

                        default:
                            break;
                    }
                }
            }
            #endregion
            StaticFunctions.SetBtnStyle(barManager1, lisBtnAlls, drMain);

            this.txtOrdFilter.Location = new System.Drawing.Point(int.Parse(drMain["ControlFilterLocalPointX"].ToString()), int.Parse(drMain["ControlFilterLocalPointY"].ToString()));

            foreach (XtraTabPage tap in xtabItemInfo.TabPages)
            {
                if (dtTabs.Select("LoadQuery=1 AND TabName='" + tap.Name + "'").Length > 0)
                {
                    Query(tap.Name);
                }
            }
            blInitBound = false;
        }

        private void btn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (blSysProcess)
                return;
            try
            {
                blSysProcess = true;
                e.Item.Enabled = false;
                e.Item.Refresh();
                this.Cursor = Cursors.WaitCursor;
                switch (e.Item.Name)
                {
                    case "btnQuery":
                        Query(xtabItemInfo.SelectedTabPage.Name);
                        break;
                    case "btnQueryAll":
                        foreach (XtraTabPage tab in xtabItemInfo.TabPages)
                        {
                            Query(tab.Name);
                        }
                        break;
                    default:
                        DoBtnSpecial(e.Item.Name);
                        break;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("错误:" + err.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                e.Item.Enabled = true;
                e.Item.Refresh();
                blSysProcess = false;
            }
        }
        private void repImg_Popup(object sender, EventArgs e)
        {
            if (!(sender is DevExpress.XtraEditors.ImageEdit))
                return;

            DevExpress.XtraEditors.ImageEdit repImg = sender as DevExpress.XtraEditors.ImageEdit;
            DevExpress.XtraGrid.Views.Grid.GridView gv = (repImg.Parent as DevExpress.XtraGrid.GridControl).MainView as DevExpress.XtraGrid.Views.Grid.GridView;

            DataRow _tpDr = gv.GetDataRow(gv.FocusedRowHandle);

            if (_tpDr["Icon"].Equals(System.DBNull.Value))
            {
                byte[] _tpBytes = ServerRefManager.PicFileRead(_tpDr["StylePic"].ToString(), _tpDr["Pic_Version"].ToString());
                gv.FocusedColumn = gv.Columns["Icon"];
                gv.ShowEditor();
                if (gv.ActiveEditor is DevExpress.XtraEditors.ImageEdit)
                {
                    if (repImg.Properties.ShowPopupShadow == false)
                    {
                        repImg.ShowPopup();
                    }
                }
                if (_tpBytes == null)
                {
                    _tpDr["Icon"] = new byte[1];
                }
                else
                {
                    _tpDr["Icon"] = _tpBytes;
                }
                gv.RefreshRow(gv.FocusedRowHandle);
                repImg.ShowPopup();
            }
        }

        private void Query(string strSelTabName)
        {
            DataRow[] drTabs = dtTabs.Select("TabName='" + strSelTabName + "'");
            if (drTabs.Length <= 0)
                return;

            DataRow drTab = drTabs[0];
            GridView gv = gcItems[drTab["GridViewName"].ToString()];
            gv.GridControl.Select();

            string strSpParmName = string.Empty;
            List<string> lisSpParmValue = new List<string>();
            if (drTab["QueryContrName"].ToString() != string.Empty)
            {
                lisSpParmValue = StaticFunctions.GetPassSpParmValue(lisGcQuerys[drTab["QueryContrName"].ToString()], dtShow, out strSpParmName);
            }
            if (strSpParmName != string.Empty)
                strSpParmName += ",";
            string[] strKey = (strSpParmName + "EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     drTab["QueryFlag"].ToString()});
            DataSet dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            if (dtAdd == null)
                return;

            dtAdd.AcceptChanges();
            blInitBound = true;
            gv.GridControl.DataSource = dtAdd.Tables[0].DefaultView;//可能引发gridView1_FocusedRowChanged
            gv.BestFitColumns();

            blInitBound = false;
            gcItems[drTab["GridViewName"].ToString()].GridControl.Select();

            if (gcGridCheckSels.ContainsKey(gv.Name))
            {
                gcGridCheckSels[gv.Name].ClearSelection();
            }
        }
        private void DoOpenLinkForm(DataRow drBtn)
        {
            DataRow drEdit = null;
            if (drBtn["IsAddFormLink"].ToString() == "False" && drBtn["EditFormLinkKeyField"].ToString() != string.Empty)
            {
                string strTabSel = xtabItemInfo.SelectedTabPage.Name;
                DataRow[] drTabs = dtTabs.Select("TabName='" + strTabSel + "'");
                if (drTabs.Length <= 0)
                    return;

                GridView gv = gcItems[drTabs[0]["GridViewName"].ToString()];
                if (gv.GridControl.DataSource == null)
                    return;

                drEdit = gv.GetFocusedDataRow();
                if (drEdit == null)
                {
                    return;
                }
            }
            StaticFunctions.DoOpenLinkForm(drBtn, this.ParentForm, drEdit);
        }
        private void DoPrint(DataRow drBtn)
        {
            DataRow[] drTabs = dtTabs.Select("TabName='" + xtabItemInfo.SelectedTabPage.Name + "'");
            if (drTabs.Length <= 0)
                return;

            DataRow drTab = drTabs[0];
            GridView gridVMain = gcItems[drTab["GridViewName"].ToString()];
            DataRow dr = gridVMain.GetFocusedDataRow();
            if (dr == null)
                return;

            string strKeyFiled = drBtn["RptKeyField"].ToString();
            if (strKeyFiled == string.Empty)
                strKeyFiled = drTab["EditInfoKeyId"].ToString();

            string strRptName = string.Empty;
            string strSpFlag = string.Empty;
            string strTitle = string.Empty;
            if (!StaticFunctions.GetBtnMRpt(dtBtnsM, drBtn, dr, strKeyFiled, this
                , drBtn["RptName"].ToString(), drBtn["RptFlag"].ToString(), drBtn["RptTitle"].ToString()
                , out strRptName, out strSpFlag, out strTitle))
            {
                return;
            }
            string strKeyIds = string.Empty;
            if (drBtn["IsPrintBatch"].ToString() == "True")
            {
                if (gcGridCheckSels.ContainsKey(gridVMain.Name))
                {
                    strKeyIds = gcGridCheckSels[gridVMain.Name].GetKeyIds(strKeyFiled);
                }
                if (strKeyIds == string.Empty && gridVMain.SelectedRowsCount > 1)
                {
                    foreach (int i in gridVMain.GetSelectedRows())
                    {
                        DataRow drSel = gridVMain.GetDataRow(i);
                        strKeyIds += strKeyIds == string.Empty ? drSel[strKeyFiled].ToString() : "," + drSel[strKeyFiled].ToString();
                    }
                }
            }
            if (strKeyIds == string.Empty)
            {
                strKeyIds = dr[strKeyFiled].ToString();
            }
            string[] strParams = "OrdId,OrdIds,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strParaVals = new string[] { 
                dr[strKeyFiled].ToString(), strKeyIds, 
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                strSpFlag};
            DataSet ds = this.DataRequest_By_DataSet(strSpRptName, strParams, strParaVals);
            StaticFunctions.ShowRptRS(strRptName, strTitle, this.ParentForm, null, null, ds);
        }
        private void DoPrintTicketInfoSpecial(DataRow drBtn)
        {
            DataRow[] drTabs = dtTabs.Select("TabName='" + xtabItemInfo.SelectedTabPage.Name + "'");
            if (drTabs.Length <= 0)
                return;

            DataRow drTab = drTabs[0];
            GridView gridVMain = gcItems[drTab["GridViewName"].ToString()];
            if (gridVMain.SelectedRowsCount <= 0)
            {
                MessageBox.Show("请选择要打印的明细记录.");
                return;
            }
            string strIds = string.Empty;
            List<string> lisIds = new List<string>();
            string strKeyFiled = drTab["EditInfoKeyId"].ToString();
            string strRptName = drBtn["TicketTempName"].ToString();
            string strSpFlag = drBtn["TicketSpFlag"].ToString();
            foreach (int i in gridVMain.GetSelectedRows())
            {
                DataRow dr = gridVMain.GetDataRow(i);
                lisIds.Add(dr[strKeyFiled].ToString());
                strIds += strIds == string.Empty ? dr[strKeyFiled].ToString() : "," + dr[strKeyFiled].ToString();
            }

            List<string> lisSpParmValue = new List<string>();
            string[] strKeyP = "Key_Ids,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] { 
                                     strIds,
                                     CApplication.App.CurrentSession.UserId.ToString(),
                                     CApplication.App.CurrentSession.DeptId.ToString(),
                                     CApplication.App.CurrentSession.FyId.ToString(),
                                     strSpFlag});
            DataTable dtAdd = this.DataRequest_By_DataTable(strSpName, strKeyP, lisSpParmValue.ToArray());
            if (dtAdd == null)
            {
                return;
            }
            try
            {
                BarTender.Application btdb = new BarTender.Application();
                string strPrintPath = Application.StartupPath + @"\打印模板\" + strRptName + ".btw";
                try
                {
                    foreach (string strId in lisIds)//保证按客户端顺序打印
                    {
                        DataRow[] drPrints = dtAdd.Select(strKeyFiled + "=" + strId);
                        {
                            if (drPrints.Length > 0)
                            {
                                StaticFunctions.PrintItem(btdb, drPrints[0], strPrintPath, strKeyFiled, frmImageFilePath);
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("打印出错：" + err.Message);
                }
                finally
                {
                    btdb.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("初始化打印出错，可能未安装BarTender.");
            }
        }

        private void xtabItemInfo_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (blInitBound)
                return;

            string strTabSel = xtabItemInfo.SelectedTabPage.Name;
            foreach (BarButtonItem item in lisBarItems)
            {
                DataRow[] drBtns = dtBtns.Select("BtnName='" + item.Name + "'");
                if (drBtns.Length <= 0)
                    continue;

                string strFName = drBtns[0]["InfoFilterName"].ToString();
                if (strFName == string.Empty)
                {
                    item.Visibility = BarItemVisibility.Always;
                }
                else if (("," + strFName + ",").IndexOf("," + strTabSel+",") != -1)
                {
                    item.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    item.Visibility = BarItemVisibility.Never;
                }
            }

            DataRow[] drTabs = dtTabs.Select("TabName='" + xtabItemInfo.SelectedTabPage.Name + "'");
            if (drTabs.Length <= 0)
            {
                return;
            }
            DataRow drTab = drTabs[0];
            gcItems[drTab["GridViewName"].ToString()].GridControl.Select();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int k = msg.WParam.ToInt32();
            if (k == 9)//Tab
            {
                if (xtabItemInfo.TabPages.Count <= 1)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else
                {
                    blInitBound = true;
                    if (xtabItemInfo.SelectedTabPageIndex == xtabItemInfo.TabPages.Count - 1)
                    {
                        xtabItemInfo.SelectedTabPageIndex = 0;
                    }
                    else
                    {
                        xtabItemInfo.SelectedTabPageIndex++;
                    }
                    blInitBound = false;
                    xtabItemInfo_SelectedPageChanged(xtabItemInfo, new TabPageChangedEventArgs(null, xtabItemInfo.SelectedTabPage));
                    return true;
                }
            }
            if (k == 13) //回车
            {
                DevExpress.XtraEditors.TextEdit txt = FocusedControl as DevExpress.XtraEditors.TextEdit;
                if (txt == null)
                    return base.ProcessCmdKey(ref msg, keyData);
                if (txt.Properties.ReadOnly)
                    return base.ProcessCmdKey(ref msg, keyData);
                Control ctrlP = txt.Parent;
                if (ctrlP != null && lisGcQuerys.ContainsKey(ctrlP.Name))
                {
                    btn_ItemClick(barManager1, new ItemClickEventArgs(bar2.Manager.Items["btnQuery"], null));
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public override void RefreshItem()
        {
            btn_ItemClick(barManager1, new ItemClickEventArgs(bar2.Manager.Items["btnQuery"], null));
        }

        private string GetKeyIds(DataRow drBtn)
        {
            string strTabSel = xtabItemInfo.SelectedTabPage.Name;
            DataRow[] drTabs = dtTabs.Select("TabName='" + strTabSel + "'");
            if (drTabs.Length <= 0)
                return string.Empty;

            GridView gv = gcItems[drTabs[0]["GridViewName"].ToString()];
            if (gv.GridControl.DataSource == null)
                return string.Empty;

            if (!gcGridCheckSels.ContainsKey(gv.Name))
                return string.Empty;

            string strKeyF = drBtn["FrmInfoKeyId"].ToString();
            if (strKeyF == string.Empty)
                return string.Empty;

            return gcGridCheckSels[gv.Name].GetKeyIds(strKeyF);
        }
        private void DoAddFromOrd(DataRow drBtn)
        {
            string strKeyIds = GetKeyIds(drBtn);
            if (strKeyIds == string.Empty)
            {
                MessageBox.Show("请选择操作单据.");
                return;
            }
            if (strKeyIds.IndexOf(',') != -1 && drBtn["IsMutiSelect"].ToString() == "False")
            {
                MessageBox.Show("只能选择一条操作单据.");
                return;
            }
            string strFrmInfoClass = drBtn["FrmInfoClass"].ToString();
            if (strFrmInfoClass.ToUpper() == "frmSysBusAdd".ToUpper())
            {
                Form frmExist = StaticFunctions.GetExistedChildForm(this.ParentForm, "frmSysBusAdd");
                if (frmExist != null)
                {
                    frmExist.Close();
                    frmExist.Dispose();
                }
                frmSysBusAdd frm = new frmSysBusAdd(drBtn);
                frm.StrOrdKeyId = strKeyIds;
                frm.ClassNameParent = this.Name;
                frm.BusClassNameParent = strBusClassName;
                frm.MdiParent = this.ParentForm;
                frm.Show();
            }
        }

        private void txtOrdFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != 13)
                return;

            blInitBound = true;
            string strText = txtOrdFilter.Text.Trim();
            if (strText == string.Empty)
            {
                return;
            }

            DataRow[] drTabs = dtTabs.Select("TabName='" + xtabItemInfo.SelectedTabPage.Name + "'");
            if (drTabs.Length <= 0)
            {
                return;
            }
            DataRow drTab = drTabs[0];

            string strFilterF = drTab["FilterSelKeyField"].ToString();
            if (strFilterF == string.Empty)
                return;

            GridView gv = gcItems[drTab["GridViewName"].ToString()];
            if (gv.GridControl.DataSource == null)
            {
                return;
            }

            if (!gcGridCheckSels.ContainsKey(gv.Name))
            {
                return;
            }

            DataTable dtSource = (gv.GridControl.DataSource as DataView).Table;
            DataRow[] drSels = dtSource.Select(strFilterF + " like '%" + strText + "'");
            if (drSels.Length == 0)
            {
                MessageBox.Show("没有找到记录.");
                txtOrdFilter.SelectAll();
                return;
            }
            gcGridCheckSels[gv.Name].SelectRow(drSels, false);

            blInitBound = true;
            txtOrdFilter.EditValue = string.Empty;
            blInitBound = false;
        }

        public void UpdateData(string strSelTabName)
        {
            foreach (XtraTabPage tap in xtabItemInfo.TabPages)
            {
                if ((strSelTabName + ",").IndexOf(tap.Name + ",") != -1)
                {
                    Query(tap.Name);
                }
            }
        }

        #region 可能需要扩充的事件
        private void DoPostEditorGv()
        {
            string strTabSel = xtabItemInfo.SelectedTabPage.Name;
            DataRow[] drTabs = dtTabs.Select("TabName='" + strTabSel + "'");
            if (drTabs.Length <= 0)
                return;

            GridView gv = gcItems[drTabs[0]["GridViewName"].ToString()];
            if (gv.GridControl.DataSource == null)
                return;

            blInitBound = true;
            int iF = gv.FocusedRowHandle;
            gv.FocusedRowHandle = -1;
            gv.FocusedRowHandle = iF;
            blInitBound = false;
        }
        private void DoBtnSpecial(string strBtnName)
        {

            DataRow[] drBtns = dtBtns.Select("BtnName='" + strBtnName + "'");
            if (drBtns.Length <= 0)
                return;

            DoPostEditorGv();
            DataRow drBtn = drBtns[0];
            if (drBtn["IsAddInfoFrmBtn"].ToString() == "True")
            {
                DoAddFromOrd(drBtn);
            }
            else if (drBtn["IsFormLink"].ToString() == "True")
            {
                DoOpenLinkForm(drBtn);
            }
            else if (drBtn["IsRptBtn"].ToString() == "True")
            {
                StaticFunctions.DoShowRpt(drBtn, this.ParentForm);
            }
            else if (drBtn["IsPrintBtn"].ToString() == "True")
            {
                DoPrint(drBtn);
            }
            else if (drBtn["IsPrintTicket"].ToString() == "True")
            {
                DoPrintTicketInfoSpecial(drBtn);
            }
            else
            {
                DoMyBtn(strBtnName);
            }
        }
        private void DoMyBtn(string strBtnName)
        {
        }
        #endregion

    }
}