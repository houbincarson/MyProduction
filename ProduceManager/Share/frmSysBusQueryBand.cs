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
using System.Threading;

namespace ProduceManager
{
    public partial class frmSysBusQueryBand : frmEditorBase
    {
        #region private Params
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

        private DataSet dsFormAdt = null;
        private DataSet dsFormUkyndaAdt = null;
        private DataRow drMain = null;
        private string strSpRptName = "Rpt_RS_DS";
        private string strShareSpName = "Share_Table_Op";
        private string strSpName = string.Empty;
        private string strQueryFlag = string.Empty;
        private string strGetInfoFlag = string.Empty;
        private string strKeyFiled = string.Empty;
        private string strBusClassName = string.Empty;
        private string strAllowList = string.Empty;

        private string strMenus_ClassEdit = string.Empty;
        private string strMenus_ClassEditTitle = string.Empty;
        private string strEditBsuClass = string.Empty;

        private string strRelationsKeyId = string.Empty;
        private string strNoShowDefaultBarItems = string.Empty;

        private Dictionary<string, GridView> gcItems = new Dictionary<string, GridView>();
        private List<RepositoryItemImageEdit> lisrepImg = new List<RepositoryItemImageEdit>();
        Thread tdCreateBarcodeImage = null;
        #endregion

        public frmSysBusQueryBand()
        {
            InitializeComponent();
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

            dsFormUkyndaAdt = this.GetFrmLoadUkyndaDsAdt(strBusClassName);
            dsFormUkyndaAdt.AcceptChanges();
            dsFormAdt = this.GetFrmLoadDsAdt(strBusClassName);
            dsFormAdt.AcceptChanges();
            dsLoad = this.GetFrmLoadDsNew(strBusClassName);
            dsLoad.AcceptChanges();
            dtShow = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];

            drMain = dsLoad.Tables[2].Rows[0];
            strSpName = drMain["SpName"].ToString();
            strQueryFlag = drMain["QueryFlag"].ToString();
            strGetInfoFlag = drMain["GetInfoFlag"].ToString();
            strKeyFiled = drMain["KeyIdFiled"].ToString();
            strMenus_ClassEdit = drMain["Menus_ClassEdit"].ToString();
            strMenus_ClassEditTitle = drMain["Menus_ClassEditTitle"].ToString();
            strEditBsuClass = drMain["EditBsuClass"].ToString();
            strRelationsKeyId = drMain["RelationsKeyId"].ToString();
            strNoShowDefaultBarItems = drMain["NoShowDefaultBarItems"].ToString();

            dtBtns = dsLoad.Tables[3];
            dtTabs = dsLoad.Tables[4];
            dtGroupC = dsLoad.Tables[5];
            dtSte = dsLoad.Tables[6];
            dtContr = dsLoad.Tables[7];
            dtBtnsM = dsLoad.Tables[8];
            dtSp = dsLoad.Tables[9];

            GridViewEdit = gridVMain;
            ParentControl = gcQuery;
            int igcHeight;
            Rectangle rect = SystemInformation.VirtualScreen;
            List<Control> lisGcContrs = StaticFunctions.ShowGroupControl(gcQuery, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            //StaticFunctions.SetMainGridView(gridVMain, drMain, lisrepImg);
            //StaticFunctions.ShowGridControl(gridVMain, dtShow, dtConst);
            StaticFunctions.ShowGridControlBand(gridVMain, dtShow, dtConst);
            StaticFunctions.SaveOrLoadDelLayout(gridVMain, this.strBusClassName + "_gridVMain", "LOAD");
            StaticFunctions.SetGridViewStyleFormatCondition(gridVMain, dtBtnsM);

            List<BarButtonItem> lisBarItems = StaticFunctions.ShowBarButtonItem(dtBtns, bar2, "bar2", strAllowList, imageList1);
            foreach (BarButtonItem item in lisBarItems)
            {
                item.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            }
            gcItems = StaticFunctions.ShowTabItem(dtTabs, xtabItemInfo, "xtabItemInfo", strAllowList, lisrepImg);
            foreach (string strGv in gcItems.Keys)
            {
                StaticFunctions.ShowGridControl(gcItems[strGv], dtShow, dtConst);
                StaticFunctions.SaveOrLoadDelLayout(gcItems[strGv], this.strBusClassName + "_" + strGv, "LOAD");

                DataRow[] drTabs = dtTabs.Select("IsAddChildGv=1 AND GridViewName='" + strGv + "'");
                if (drTabs.Length == 1)
                {
                    GridView gvChild = StaticFunctions.ShowGridVChildGv(strGv + "Com", gcItems[strGv].GridControl, dtShow, dtConst);
                    StaticFunctions.SetGridViewStyleFormatCondition(gvChild, dtBtnsM);
                }

                StaticFunctions.SetGridViewStyleFormatCondition(gcItems[strGv], dtBtnsM);
            }
            foreach (RepositoryItemImageEdit rep in lisrepImg)
            {
                rep.Popup += new System.EventHandler(this.repImg_Popup);
            }

            #region gcQuery
            foreach (Control ctrl in lisGcContrs)
            {
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
                        StaticFunctions.BoundSpicalContr(dtContr, dsFormAdt, dsFormUkyndaAdt, ucp, dtShow);
                        break;
                    case "ProduceManager.UcTreeList":
                        ProduceManager.UcTreeList uct = ctrl as ProduceManager.UcTreeList;
                        uct.onClosePopUp += new UcTreeList.ClosePopUp(uct_onClosePopUp);
                        StaticFunctions.BoundSpicalContr(dtContr, dsFormAdt, dsFormUkyndaAdt, uct, dtShow);
                        break;
                    case "ExtendControl.ExtPopupTree":
                        ExtendControl.ExtPopupTree ept = ctrl as ExtendControl.ExtPopupTree;
                        ept.Properties.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.lookUpEdit_Properties_Closed);
                        StaticFunctions.BoundSpicalContr(dtContr, dsFormAdt, dsFormUkyndaAdt, ept, dtShow);
                        break;

                    default:
                        break;
                }
            }
            #endregion
            if (drMain["IsAddChildGv"].ToString() == "True")
            {
                GridView gvChild = StaticFunctions.ShowGridVChildGv("gridVCom", gridCMain, dtShow, dtConst);
                StaticFunctions.SetGridViewStyleFormatCondition(gvChild, dtBtnsM);
            }
            StaticFunctions.SetBtnStyle(barManager1, null, drMain);

            if (drMain["HideQuery"].ToString() == "True")
            {
                gcQuery.Visible = false;
            }
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
        private void uct_onClosePopUp(object sender)
        {
        }

        private void frmSysBusQuery_Load(object sender, EventArgs e)
        {
            ChkRight();
            SetMoreInfo(false); 

            if (drMain["LoadQuery"].ToString() == "True")
            {
                btn_ItemClick(barManager1, new ItemClickEventArgs(bar2.Manager.Items["btnQuery"], null));
            }
        }
        private void ChkRight()
        {
            foreach (BarItem item in bar2.Manager.Items)
            {
                if (("," + strNoShowDefaultBarItems + ",").Replace(" ", "").IndexOf(item.Name + ",") >= 0)
                {
                    item.Visibility = BarItemVisibility.Never;
                }
                else if (strAllowList.IndexOf(item.Name + "=") >= 0)
                {
                    item.Visibility = BarItemVisibility.Always;
                }
            }
        }
        private void gridVMain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1)
                return;

            SetMoreInfo(false);
        }
        private void gridVMain_DoubleClick(object sender, EventArgs e)
        {
            if (bar2.Manager.Items["btnEdit"].Visibility == BarItemVisibility.Always)
            {
                btn_ItemClick(barManager1, new ItemClickEventArgs(bar2.Manager.Items["btnEdit"], null));
            }
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
                        Query();
                        break;
                    case "btnMore":
                        DoMore();
                        break;
                    case "btnReLoad":
                        DoReLoad();
                        break;
                    case "btnEdit":
                        DoEdit();
                        break;
                    case "btnAdd":
                        DoAdd();
                        break;
                    case "btnExcel":
                        DoExcel();
                        break;
                    case "btnImport":
                        DoImport();
                        break;
                    case "btnSaveLayOut":
                        StaticFunctions.SaveOrLoadDelLayout(gridVMain, this.strBusClassName + "_gridVMain", "SAVE");
                        foreach (string strGv in gcItems.Keys)
                        {
                            StaticFunctions.SaveOrLoadDelLayout(gcItems[strGv], this.strBusClassName + "_" + strGv, "SAVE");
                        }
                        MessageBox.Show("成功保存样式.");
                        break;
                    case "btnDeleteLayOut":
                        StaticFunctions.SaveOrLoadDelLayout(gridVMain, this.strBusClassName + "_gridVMain", "DELETE");
                        foreach (string strGv in gcItems.Keys)
                        {
                            StaticFunctions.SaveOrLoadDelLayout(gcItems[strGv], this.strBusClassName + "_" + strGv, "DELETE");
                        }
                        MessageBox.Show("成功删除样式.");
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

        private void DoReLoad()
        {
            bar2.Reset();
            gcItems.Clear();
            lisrepImg.Clear();
            gcQuery.Controls.Clear();
            gridVMain.Bands.Clear();
            gridVMain.Columns.Clear();
            xtabItemInfo.TabPages.Clear();
            arrContrSeq.Clear();
            dsLoad = null;
            InitContr();
            gridVMain.BestFitColumns();
            frmSysBusQuery_Load(null, null);
        }
        private void DoMore()
        {
            if (btnMore.Caption == "显示明细&M")
            {
                SetMoreInfo(true);
            }
            else
            {
                SetMoreInfo(false);
            }
        }
        private void DoEdit()
        {
            DataRow dr = this.gridVMain.GetFocusedDataRow();
            if (dr == null)
                return;
            if (strEditBsuClass != string.Empty)
            {
                StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", this.ParentForm, strMenus_ClassEditTitle,
                    strEditBsuClass, strMenus_ClassEdit, "VIEW", "KeyId=" + dr[strKeyFiled].ToString() + "&BusClassName=" + strMenus_ClassEdit, null);
            }
            else
            {
                StaticFunctions.OpenChildEditorForm(true, "ProduceManager", ParentForm, strMenus_ClassEditTitle,
                    strMenus_ClassEdit, "VIEW", "KeyId=" + dr[strKeyFiled].ToString(), null);
            }
        }
        private void DoAdd()
        {
            if (strEditBsuClass != string.Empty)
            {
                StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", this.ParentForm, strMenus_ClassEditTitle,
                    strEditBsuClass, strMenus_ClassEdit, "ADD", "BusClassName=" + strMenus_ClassEdit, null);
            }
            else
            {
                StaticFunctions.OpenChildEditorForm(true, "ProduceManager", ParentForm, strMenus_ClassEditTitle,
                    strMenus_ClassEdit, "ADD", string.Empty, null);
            }
        }
        private void DoImport()
        {
            if (gridVMain.RowCount <= 0)
                return;

            DataTable dt = (gridCMain.DataSource as DataView).Table.Clone();
            for (int i = 0; i < gridVMain.RowCount; i++)
            {
                dt.ImportRow(gridVMain.GetDataRow(i));
            }
            StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", this.ParentForm, strMenus_ClassEditTitle,
                strEditBsuClass, strMenus_ClassEdit, "VIEW", "BusClassName=" + strMenus_ClassEdit, dt);
        }
        private void DoPrint(DataRow drBtn)
        {
            DataRow dr = gridVMain.GetFocusedDataRow();
            if (dr == null)
                return;

            string strKeyFiledRpt = drBtn["RptKeyField"].ToString();
            if (strKeyFiledRpt == string.Empty)
                strKeyFiledRpt = strKeyFiled;

            string strRptName = string.Empty;
            string strSpFlag = string.Empty;
            string strTitle = string.Empty;
            if (!StaticFunctions.GetBtnMRpt(dtBtnsM, drBtn, dr, strKeyFiledRpt, this
                , drBtn["RptName"].ToString(), drBtn["RptFlag"].ToString(), drBtn["RptTitle"].ToString()
                , out strRptName, out strSpFlag, out strTitle))
            {
                return;
            }
            string strKeyIds = dr[strKeyFiledRpt].ToString();
            if (drBtn["IsPrintBatch"].ToString() == "True" && gridVMain.SelectedRowsCount > 1)
            {
                strKeyIds = string.Empty;
                foreach (int i in gridVMain.GetSelectedRows())
                {
                    DataRow drSel = gridVMain.GetDataRow(i);
                    strKeyIds += strKeyIds == string.Empty ? drSel[strKeyFiledRpt].ToString() : "," + drSel[strKeyFiledRpt].ToString();
                }
            }
            string[] strParams = "OrdId,OrdIds,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strParaVals = new string[] { 
                dr[strKeyFiledRpt].ToString(), strKeyIds, 
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                strSpFlag};
            DataSet ds = this.DataRequest_By_DataSet(strSpRptName, strParams, strParaVals);
            StaticFunctions.ShowRptRS(strRptName, strTitle, this.ParentForm, null, null, ds);
        }
        private void DoExcel()
        {
            if (gridVMain.RowCount == 0)
                return;

            this.Cursor = Cursors.WaitCursor;
            StaticFunctions.GridViewExportToExcel(gridVMain, this.Text, null);
            this.Cursor = Cursors.Arrow;
        }
        private void DoPrintTicketInfoSpecial(DataRow drBtn)
        {
            string strIds = string.Empty;
            List<string> lisIds = new List<string>();
            if (drBtn["IsPrintTicketAll"].ToString() == "True")
            {
                for (int i = 0; i < gridVMain.RowCount; i++)
                {
                    DataRow dr = gridVMain.GetDataRow(i);
                    lisIds.Add(dr[strKeyFiled].ToString());
                    strIds += strIds == string.Empty ? dr[strKeyFiled].ToString() : "," + dr[strKeyFiled].ToString();
                }
            }
            else
            {
                if (gridVMain.SelectedRowsCount <= 0)
                {
                    MessageBox.Show("请选择要打印的明细记录.");
                    return;
                }
                foreach (int i in gridVMain.GetSelectedRows())
                {
                    DataRow dr = gridVMain.GetDataRow(i);
                    lisIds.Add(dr[strKeyFiled].ToString());
                    strIds += strIds == string.Empty ? dr[strKeyFiled].ToString() : "," + dr[strKeyFiled].ToString();
                }
            }

            string strRptName = string.Empty;
            string strSpFlag = string.Empty;
            string strTitle = string.Empty;
            if (!StaticFunctions.GetBtnMRpt(dtBtnsM, drBtn, gridVMain.GetFocusedDataRow(), strKeyFiled, this
                , drBtn["TicketTempName"].ToString(), drBtn["TicketSpFlag"].ToString(), ""
                , out strRptName, out strSpFlag, out strTitle))
            {
                return;
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

        private void DoUpdateOrdState(DataRow drBtn)
        {
            List<DataRow> selRows = new List<DataRow>();
            string strIds = string.Empty;
            foreach (int i in gridVMain.GetSelectedRows())
            {
                DataRow drSel = gridVMain.GetDataRow(i);
                selRows.Add(drSel);
                strIds += strIds == string.Empty ? drSel[strKeyFiled].ToString() : "," + drSel[strKeyFiled].ToString();
            }
            if (strIds == string.Empty)
            {
                DataRow dr = gridVMain.GetFocusedDataRow();
                if (dr != null)
                {
                    strIds = dr[strKeyFiled].ToString();
                    selRows.Add(dr);
                }
            }
            if (strIds == string.Empty)
            {
                return;
            }
            string strMsg = drBtn["UpdateOrdStateComfMsg"].ToString();
            if (strMsg != string.Empty)
            {
                if (MessageBox.Show(strMsg, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                    return;
            }

            DataSet dsAdd = null;
            List<string> lisSpParmValue = new List<string>();
            if (drBtn["UpdateOrdFrmClass"].ToString() == string.Empty)
            {
                DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + drBtn["UpdateOrdStateFlag"].ToString() + "'");
                if (drShares.Length == 0)
                {
                    string[] strKey = "Key_Ids,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                    lisSpParmValue.AddRange(new string[] { 
                         strIds,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         drBtn["UpdateOrdStateFlag"].ToString()});
                    dsAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
                }
                else
                {
                    string[] strKey = "BsuSetSp_Id,Key_Ids,EUser_Id,EDept_Id,Fy_Id".Split(",".ToCharArray());
                    lisSpParmValue.AddRange(new string[] {drShares[0]["BsuSetSp_Id"].ToString(),
                         strIds,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString()});
                    dsAdd = this.DataRequest_By_DataSet(strShareSpName, strKey, lisSpParmValue.ToArray());
                }
                if (dsAdd == null)
                {
                    return;
                }
                MessageBox.Show("操作完成.");
            }
            else
            {
                DataTable dtInfo = (gridCMain.DataSource as DataView).Table;
                frmUpdateConfirm frm = new frmUpdateConfirm(drBtn["UpdateOrdFrmClass"].ToString());
                frm.frmDataTable = dtInfo.Clone();
                frm.DrBtn = drBtn;
                frm.DrOrd = gridVMain.GetFocusedDataRow();
                frm.StrUpdKeyIds = strIds;
                frm.StrUpdSpName = strSpName;
                frm.DtSp = dtSp;
                if (frm.ShowDialog(this) != DialogResult.Yes)
                    return;

                dsAdd = frm.DtRets;
            }
            if (dsAdd.Tables.Count > 0 && dsAdd.Tables[0].Columns.Contains("UpdateFields"))
            {
                DataRow drNew = dsAdd.Tables[0].Rows[0];
                foreach (DataRow dr in selRows)
                {
                    StaticFunctions.UpdateDataRowSyn(dr, drNew, drNew["UpdateFields"].ToString());
                    dr.AcceptChanges();
                }
            }
        }
        private void DoEditBatch(DataRow drBtn)
        {
            if (this.gridVMain.SelectedRowsCount <= 0)
            {
                MessageBox.Show("请先选择要批量修改的记录.");
                return;
            }
            string strKeyFiled = drBtn["BatchUpdateKeyId"].ToString();
            List<DataRow> lisRows = new List<DataRow>();
            string strKeyIds = string.Empty;
            foreach (int i in gridVMain.GetSelectedRows())
            {
                DataRow dr = gridVMain.GetDataRow(i);
                strKeyIds += strKeyIds == string.Empty ? dr[strKeyFiled].ToString() : "," + dr[strKeyFiled].ToString();
                lisRows.Add(dr);
            }
            if (strKeyIds == string.Empty)
                return;

            DataTable dtInfo = (gridCMain.DataSource as DataView).Table;
            frmBatchEdit frm = new frmBatchEdit(drBtn["BatchFormName"].ToString());
            frm.frmDataTable = dtInfo.Clone();
            frm.DrBtn = drBtn;
            frm.StrUpdKeyIds = strKeyIds;
            frm.StrUpdSpName = strSpName;
            frm.DrBatchs = lisRows;
            frm.DtSps = dtSp;
            if (frm.ShowDialog(this) != DialogResult.Yes)
                return;

            dtInfo.AcceptChanges();
        }

        public override void RefreshItem()
        {
            btn_ItemClick(barManager1, new ItemClickEventArgs(bar2.Manager.Items["btnQuery"], null));
        }
        private void Query()
        {
            if (!StaticFunctions.CheckSave(gcQuery, dtShow))
                return;

            gridCMain.Select();

            string strSpParmName = string.Empty;
            List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(gcQuery, dtShow, out strSpParmName);

            if (strSpParmName != string.Empty)
                strSpParmName += ",";
            string[] strKey = (strSpParmName + "EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     strQueryFlag});
            DataSet dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            if (dtAdd == null)
                return;
            frmDataTable = dtAdd.Tables[0];
            if (drMain["MainGvShowIcon"].ToString() == "True")
            {
                DataColumn newColumn = frmDataTable.Columns.Add("Icon", Type.GetType("System.Byte[]"));
                newColumn.AllowDBNull = true;
            }
            else if (drMain["MainGvShowPic"].ToString() == "True")
            {
                DataColumn newColumn = frmDataTable.Columns.Add("Icon", Type.GetType("System.Byte[]"));
                newColumn.AllowDBNull = true;

                foreach (DataRow dr in frmDataTable.Rows)
                {
                    dr["Icon"] = ServerRefManager.PicFileRead(dr["StylePic"].ToString(), dr["Pic_Version"].ToString());
                }
            }
            if (drMain["IsAddChildGv"].ToString() == "True" && strRelationsKeyId != string.Empty && dtAdd.Tables.Count >= 2)
            {
                dtAdd.Relations.Add("gridCMainChildGrid", frmDataTable.Columns[strRelationsKeyId], dtAdd.Tables[1].Columns[strRelationsKeyId]);
            }
            dtAdd.AcceptChanges();
            gridCMain.DataSource = frmDataTable.DefaultView;//可能引发gridView1_FocusedRowChanged
            gridVMain.BestFitColumns();

            SetMoreInfo(false);
        }
        private void SetMoreInfo(bool blShow)
        {
            if (blShow)
            {
                ShowMore();
                splitContrMain.PanelVisibility = SplitPanelVisibility.Both;
                btnMore.Caption = "隐藏明细&M";
            }
            else
            {
                splitContrMain.PanelVisibility = SplitPanelVisibility.Panel1;
                btnMore.Caption = "显示明细&M";
            }
        }
        private void ShowMore()
        {
            DataRow dr = this.gridVMain.GetFocusedDataRow();
            if (dr == null)
                return;

            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                         dr[strKeyFiled].ToString() == string.Empty ? "-1":dr[strKeyFiled].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         strGetInfoFlag};
            DataSet dtSet = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            dtSet.AcceptChanges();
            BoundGridView(dtSet);
        }
        private void BoundGridView(DataSet ds)
        {
            DataTable dt = null;
            DataColumn newColumn = null;
            foreach (string strGv in gcItems.Keys)
            {
                DataRow[] drTabs = dtTabs.Select("IsAddChildGv=1 AND GridViewName='" + strGv + "'");
                if (drTabs.Length == 1)
                {
                    if (!ds.Tables.Contains(strGv + "-Main") || !ds.Tables.Contains(strGv + "-Com"))
                    {
                        MessageBox.Show("存储过程没有返回嵌套表数据源：" + strGv);
                        return;
                    }
                    dt = ds.Tables[strGv + "-Main"];
                    string strRelationsKeyId = drTabs[0]["RelationsKeyId"].ToString();
                    ds.Relations.Add(gcItems[strGv].GridControl.Name + "ChildGrid", dt.Columns[strRelationsKeyId], ds.Tables[strGv + "-Com"].Columns[strRelationsKeyId]);
                }
                else
                {
                    if (!ds.Tables.Contains(strGv))
                    {
                        MessageBox.Show("存储过程没有返回表数据源：" + strGv);
                        return;
                    }
                    dt = ds.Tables[strGv];
                }
                newColumn = dt.Columns.Add("Icon", Type.GetType("System.Byte[]"));
                newColumn.AllowDBNull = true;
                dt.AcceptChanges();

                gcItems[strGv].GridControl.DataSource = dt.DefaultView;
                gcItems[strGv].BestFitColumns();
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int k = msg.WParam.ToInt32();
            if (k == 13)
            {
                btn_ItemClick(barManager1, new ItemClickEventArgs(bar2.Manager.Items["btnQuery"], null));
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region 可能需要扩充的事件
        private void DoBtnSpecial(string strBtnName)
        {
            DataRow[] drBtns = dtBtns.Select("BtnName='" + strBtnName + "'");
            if (drBtns.Length <= 0)
                return;

            DataRow drBtn = drBtns[0];
            if (drBtn["IsPrintBtn"].ToString() == "True")
            {
                DoPrint(drBtn);
            }
            else if (drBtn["IsRptBtn"].ToString() == "True")
            {
                StaticFunctions.DoShowRpt(drBtn, this.ParentForm);
            }
            else if (drBtn["IsBatchEditBtn"].ToString() == "True")
            {
                DoEditBatch(drBtn);
            }
            else if (drBtn["IsFormLink"].ToString() == "True")
            {
                StaticFunctions.DoOpenLinkForm(drBtn, this.ParentForm, gridVMain.GetFocusedDataRow());
            }
            else if (drBtn["IsUpdateOrdStatetn"].ToString() == "True")
            {
                DoUpdateOrdState(drBtn);
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
            DataRow dr = gridVMain.GetFocusedDataRow();
            switch (strBtnName)
            {
                case "btnCreateBarcodeImage":
                    if (gridVMain.SelectedRowsCount <= 0)
                        return;

                    tdCreateBarcodeImage = new Thread(new ThreadStart(CreateBarcodeImage));
                    tdCreateBarcodeImage.Start();
                    break;
                default:
                    break;
            }
        }
        private void CreateBarcodeImage()
        {
            try
            {
                foreach (int i in gridVMain.GetSelectedRows())
                {
                    DataRow dr = gridVMain.GetDataRow(i);
                    StaticFunctions.CreateBarcodeImage(dr, "BarcodeFileName", "BarcodeNumber");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成条码图像出错:" + ex.Message);
            }
            finally
            {
                tdCreateBarcodeImage.Abort();
                tdCreateBarcodeImage = null;
            }
        }
        #endregion
    }
}