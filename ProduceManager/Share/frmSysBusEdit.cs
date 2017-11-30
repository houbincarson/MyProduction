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
using System.Threading;

namespace ProduceManager
{
    public partial class frmSysBusEdit : frmEditorBase
    {
        #region private Params
        private bool blInitBound = false;
        private bool blSetItemStatusByOrd = false;
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
        private DataTable dtExcel = null;
        private bool blSysProcess = false;

        private DataSet dsFormAdt = null;
        private DataSet dsFormUkyndaAdt = null;
        private DataRow drMain = null;

        private string strSpRptName = "Rpt_RS_DS";
        private string strShareSpName = "Share_Table_Op";
        private string strSpName = string.Empty;
        private string strQueryFlag = string.Empty;
        private string strGetInfoFlag = string.Empty;
        private string strAddFlag = string.Empty;
        private string strEditFlag = string.Empty;
        private string strKeyFiled = string.Empty;
        private string strBusClassName = string.Empty;
        private string strNoShowDefaultBarItems = string.Empty;
        private string strAllowList = string.Empty;
        private string strOrdState = string.Empty;
        private string strEnterGc = string.Empty;
        private string strGetWFBalceCtrlIds = string.Empty;

        private string[] strFilterFields = null;
        private List<string> lisOrdSpCtrlId = new List<string>();

        private Dictionary<string, GridView> gcItems = new Dictionary<string, GridView>();//所有子表的GridView集合，不包括gridVMain，key为GridViewName
        private List<RepositoryItemImageEdit> lisrepImg = new List<RepositoryItemImageEdit>();//所有子表GridView中要显示图片列的EditType
        private Dictionary<string, List<SimpleButton>> lisBtns = new Dictionary<string, List<SimpleButton>>();//以编辑Tab对应的GridViewName为Key，保存所属的Btn
        private List<SimpleButton> lisBtnAlls = new List<SimpleButton>();//页面中所有的SimpleButton
        private List<GridView> GvNeedGCEdit = new List<GridView>();//所有需要编辑区的GridView，定制gridVInfo_FocusedRowChanged
        private Dictionary<string, UkyndaGcEdit> UkyndaGcItems = new Dictionary<string, UkyndaGcEdit>();//以编辑面板GroupBoxName为Key，保存UkyndaGcEdit

        private bool blReAddInfo = false;
        private string CopyFields = string.Empty;
        private string CopyFieldsOrd = string.Empty;
        private Dictionary<string, Control> dicCtrlS = new Dictionary<string, Control>();
        private string strReBindCtrlIds = string.Empty;
        private bool blSetWeight = false;
        Thread tdCreateBarcodeImage = null;
        private DataRow drAddTemp = null;
        #endregion

        public frmSysBusEdit()
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

                if (strEnterGc == string.Empty)
                    return;

                if (UkyndaGcItems[strEnterGc].StrMode == "VIEW")
                    return;

                DataRow drInfo = GridViewEdit.GetFocusedDataRow();
                if (drInfo == null)
                    return;

                drInfo[dpl.Tag.ToString()] = Convert.ToString(dpl.EditValue) == string.Empty ? DBNull.Value : dpl.EditValue;
                DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, dpl.Name, dpl.Parent.Name);
                StaticFunctions.UpdateDataRowSynLookUpEdit(drInfo, dpl, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString());
                SetContrEditFromDpl(UkyndaGcItems[strEnterGc], dpl.Name);
                if (!blPrevFindControl)
                {
                    SetContrMoveNext(dpl.Name, false);
                }
                DoControlEvent(dpl, Convert.ToString(dpl.EditValue));
            }
            else if (sender is ExtendControl.ExtPopupTree)
            {
                if (strEnterGc == string.Empty)
                    return;

                if (UkyndaGcItems[strEnterGc].StrMode == "VIEW")
                    return;

                DataRow drInfo = GridViewEdit.GetFocusedDataRow();
                if (drInfo == null)
                    return;

                ExtendControl.ExtPopupTree ept = sender as ExtendControl.ExtPopupTree;
                DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, ept.Name, ept.Parent.Name);
                StaticFunctions.UpdateDataRowSynExtPopupTree(drInfo, ept, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString());
                SetContrEditFromDpl(UkyndaGcItems[strEnterGc], ept.Name);
                DoControlEvent(ept, Convert.ToString(ept.EditValue));
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
            if (strEnterGc == string.Empty)
                return;

            if (UkyndaGcItems[strEnterGc].StrMode == "VIEW")
                return;

            DataRow drInfo = GridViewEdit.GetFocusedDataRow();
            if (drInfo == null)
                return;

            ProduceManager.UcTxtPopup ucp = sender as ProduceManager.UcTxtPopup;
            drInfo[ucp.Tag.ToString()] = Convert.ToString(ucp.EditValue) == string.Empty ? DBNull.Value : ucp.EditValue;
            DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, ucp.Name, ucp.Parent.Name);
            StaticFunctions.UpdateDataRowSynUcTxtPopup(drInfo, drReturn, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString(), ucp);
            SetContrEditFromDpl(UkyndaGcItems[strEnterGc], ucp.Name);
            if (!blPrevFindControl)
            {
                SetContrMoveNext(ucp.Name, false);
            }

            DoControlEvent(ucp, Convert.ToString(ucp.EditValue));
        }
        private void uct_onClosePopUp(object sender)
        {
            if (strEnterGc == string.Empty)
                return;

            if (UkyndaGcItems[strEnterGc].StrMode == "VIEW")
                return;

            DataRow drInfo = GridViewEdit.GetFocusedDataRow();
            if (drInfo == null)
                return;

            ProduceManager.UcTreeList ucp = sender as ProduceManager.UcTreeList;
            if (!blPrevFindControl)
            {
                SetContrMoveNext(ucp.Name, false);
            }
            DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, ucp.Name, ucp.Parent.Name);
            StaticFunctions.UpdateDataRowSynUcTreeList(drInfo, drContrl["SetSynFields"].ToString(), ucp);
        }
        private void txt_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            DoControlEvent(sender as TextEdit, Convert.ToString(e.NewValue));
        }
        private void DoControlEvent(Control ctrl,string strValue)
        {
            if (blInitBound)
                return;

            if (Convert.ToString(ctrl.Tag) == string.Empty)
                return;
            if (strValue == string.Empty || strValue == "-9999")
                return;

            DataRow[] drControlSet = dtBtnsM.Select("IsControlSet=1 AND BtnName='" + ctrl.Name.ToString() + "'");
            if (drControlSet.Length <= 0)
                return;

            Control ctrParent = ctrl.Parent;
            UkyndaGcEdit gc = UkyndaGcItems[ctrParent.Name];
            if (gc.StrMode == "VIEW")
                return;

            DataRow dr = gc.GridViewEdit.GetFocusedDataRow();
            if (dr == null)
                return;

            DataRow drContr = drControlSet[0];
            try
            {
                string[] strValuesArr = drContr["OrdKeyValues"].ToString().Split(",".ToCharArray());
                foreach (string strValuesItem in strValuesArr)
                {
                    string[] strValues = strValuesItem.Split("=".ToCharArray());
                    string strCompute = strValues[1];
                    string[] strTags = drContr["OrdKeyFields"].ToString().Split(",".ToCharArray());
                    foreach (string strTag in strTags)
                    {
                        if (strTag == ctrl.Tag.ToString())
                        {
                            strCompute = strCompute.Replace(strTag, strValue);
                        }
                        else
                        {
                            strCompute = strCompute.Replace(strTag, dr[strTag].ToString());
                        }
                    }
                    object snw = new DataTable().Compute(strCompute, null);
                    string strKeyF = strValues[0];
                    dr[strKeyF] = snw;
                    dr.EndEdit();
                }
            }
            catch (Exception)
            {
            }
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!blSetWeight)
            {
                e.Handled = true;
            }
        }

        private void frmSysBusEdit_Load(object sender, EventArgs e)
        {
            ChkRight();

            blSetWeight = GetUserSetWeightfrmCalsses(strBusClassName);
        }
        private bool GetUserSetWeightfrmCalsses(string strClassName)
        {
            string[] strKey = "User_Id".Split(",".ToCharArray());
            DataTable dtPm = this.DataRequest_By_DataTable("Get_Bse_User", strKey,
                new string[] { CApplication.App.CurrentSession.UserId.ToString() });
            if (dtPm == null || dtPm.Rows.Count == 0)
                return true;

            if (dtPm.Columns.IndexOf("IsSetWeight") == -1)
                return true;

            DataRow drU = dtPm.Rows[0];
            if (drU["IsSetWeight"].ToString() == "False")
            {
                return false;
            }
            else
            {
                string strSetWeightfrmCalsses = drU["frmCalsses"].ToString();
                if (strSetWeightfrmCalsses == string.Empty)
                    return true;

                return (strSetWeightfrmCalsses + ",").IndexOf(strClassName + ",") != -1;
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

        private void SetContrEditFromDpl(UkyndaGcEdit gc,string strEnterName)
        {
            if (gc.StrMode == "VIEW")
                return;

            string strFilter = "IsContrEditSet=1 AND BtnParent='" + gc.CtrParentControl.Name + "'";
            if (strEnterName != string.Empty)
                strFilter += " AND BtnName='" + strEnterName + "'";
            DataRow[] drContrEditSets = dtBtnsM.Select(strFilter);
            if (drContrEditSets.Length <= 0)
                return;

            DataRow drOrd = gridVMain.GetFocusedDataRow();
            DataRow drInfo = gc.GridViewEdit.GetFocusedDataRow();

            foreach (DataRow dr in drContrEditSets)
            {
                if (!StaticFunctions.CheckKeyFields(dr["OrdKeyFields"].ToString(), drOrd, drInfo))
                    continue;

                string[] strSets = dr["OrdKeyValues"].ToString().Split("|".ToCharArray());
                StaticFunctions.SetControlEdit(strSets[0], true, gc.CtrParentControl);
                StaticFunctions.SetControlEdit(strSets[1], false, gc.CtrParentControl);
            }
        }

        private void Txt_Enter(object sender, EventArgs e)
        {
            strFocusedContrName = (sender as Control).Name;
            FocusedControl = sender as Control;

            Control ctrParent = FocusedControl.Parent;
            if (ctrParent != null)
            {
                UkyndaGcEdit gc = UkyndaGcItems[ctrParent.Name];
                if (strEnterGc != ctrParent.Name)
                {
                    strEnterGc = ctrParent.Name;
                    ParentControl = gc.CtrParentControl;
                    arrContrSeq = gc.ArrContrSeq;
                    GridViewEdit = gc.GridViewEdit;
                    BtnEnterSave = gc.BtnEnterSave;
                }
                if (FocusedControl.GetType().ToString() == "ProduceManager.UcTxtPopup")
                {
                    ProduceManager.UcTxtPopup ucp = FocusedControl as ProduceManager.UcTxtPopup;
                    ucp.DrFilterFieldsOrd = gridVMain.GetFocusedDataRow();
                    ucp.DrFilterFieldsInfo = gc.GridViewEdit.GetFocusedDataRow();
                }
                else if (FocusedControl.GetType().ToString() == "ProduceManager.UcTreeList")
                {
                    ProduceManager.UcTreeList ucp = FocusedControl as ProduceManager.UcTreeList;
                    ucp.DrFilterFieldsOrd = gridVMain.GetFocusedDataRow();
                    ucp.DrFilterFieldsInfo = gc.GridViewEdit.GetFocusedDataRow();
                }
            }
        }
        public override void SetText(string text)
        {
            if (strGetWFBalceCtrlIds == string.Empty)
                return;
            TextEdit txt = FocusedControl as TextEdit;
            if (txt == null || txt.Properties.ReadOnly)
                return;

            string strName = txt.Name;
            if ((strGetWFBalceCtrlIds + ",").IndexOf(strName + ",") != -1)
            {
                txt.Text = StaticFunctions.Round(double.Parse(text), 2, 0.5).ToString();
            }
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
            string strKeyId = StaticFunctions.GetFrmParamValue(strParam, "KeyId", null);

            if (frmDataTable == null)
            {
                if (gridCMain.DataSource != null)
                {
                    frmDataTable = (gridCMain.DataSource as DataView).Table.Copy();
                }
                if (strKeyId != string.Empty)
                {
                    if (frmDataTable == null || frmDataTable.Select(strKeyFiled + "=" + strKeyId).Length <= 0)
                    {
                        DataTable dtTemp = GetDataById(strKeyId);
                        if (frmDataTable == null)
                        {
                            frmDataTable = dtTemp;
                        }
                        else
                        {
                            foreach (DataRow dr in dtTemp.Rows)
                            {
                                frmDataTable.ImportRow(dr);
                            }
                        }
                    }
                }
                if (frmDataTable == null && Mode == "ADD")
                {
                    frmDataTable = GetDataById("-999");
                    //GetCurrAllItem();
                }
            }
            BoundGridContr(strKeyId);
            if (Mode == "ADD")
            {
                btn_ItemClick(barManager1, new ItemClickEventArgs(bar2.Manager.Items["btnAdd"], null));
            }
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
            strAddFlag = drMain["AddFlag"].ToString();
            strEditFlag = drMain["EditFlag"].ToString();
            strKeyFiled = drMain["KeyIdFiled"].ToString();
            strOrdState = drMain["OrdStateFiled"].ToString();
            strNoShowDefaultBarItems = drMain["NoShowDefaultBarItems"].ToString();
            strGetWFBalceCtrlIds = drMain["GetWFBalceCtrlIds"].ToString();
            strReBindCtrlIds = drMain["ReBindCtrlIds"].ToString().Trim();

            dtBtns = dsLoad.Tables[3];
            dtTabs = dsLoad.Tables[4];
            dtGroupC = dsLoad.Tables[5];
            dtSte = dsLoad.Tables[6];
            dtContr = dsLoad.Tables[7];
            dtBtnsM = dsLoad.Tables[8];
            dtSp = dsLoad.Tables[9];
            dtExcel = dsLoad.Tables[10];

            Rectangle rect = SystemInformation.VirtualScreen;
            List<BarButtonItem> lisBarItems = StaticFunctions.ShowBarButtonItem(dtBtns, bar2, "bar2", strAllowList, imageList1);
            foreach (BarButtonItem item in lisBarItems)
            {
                item.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            }
            gcItems = StaticFunctions.ShowTabItem(dtTabs, dtBtns, xtabItemInfo, "xtabItemInfo", strAllowList, lisrepImg, GvNeedGCEdit, lisBtnAlls, lisBtns, imageList1);
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
            }
            foreach (GridView gv in GvNeedGCEdit)
            {
                gv.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridVInfo_FocusedRowChanged);
            }
            foreach (RepositoryItemImageEdit rep in lisrepImg)
            {
                rep.Popup += new System.EventHandler(this.repImg_Popup);
            }

            #region GroupC
            List<Control> lisGcContrs = new List<Control>();
            blInitBound = true;
            StaticFunctions.GetGroupCUkyndaXtra(dtTabs, dtGroupC, UkyndaGcItems, this, strAllowList, lisGcContrs, dtShow, dtConst, gcItems, gridVMain, bar2, xtabOrdParent, rect.Width);
            if (xtabOrdParent.TabPages.Count > 0)
            {
                xtabOrdParent.Visible = true;
                xtabOrdParent.SelectedTabPageIndex = 0;
            }
            else
            {
                xtabOrdParent.Visible = false;
            }
            blInitBound = false;
            foreach (Control ctrl in lisGcContrs)
            {
                if (("," + strReBindCtrlIds + ",").IndexOf("," + ctrl.Name + ",") != -1)
                {
                    dicCtrlS.Add(ctrl.Name, ctrl);
                }
                ctrl.Enter += new System.EventHandler(this.Txt_Enter);
                switch (ctrl.GetType().ToString())
                {
                    case "DevExpress.XtraEditors.TextEdit":
                        SetControlEvent(ctrl);
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
                        if (ucp.Parent != null && UkyndaGcItems.ContainsKey(ucp.Parent.Name))
                        {
                            UkyndaGcEdit uky = UkyndaGcItems[ucp.Parent.Name];
                            if (uky.GridViewName == "gridVMain" && ucp.Tag != null && ucp.Tag.ToString() != string.Empty)
                                lisOrdSpCtrlId.Add(ucp.Tag.ToString());
                        }
                        break;
                    case "ProduceManager.UcTreeList":
                        ProduceManager.UcTreeList uct = ctrl as ProduceManager.UcTreeList;
                        uct.onClosePopUp += new UcTreeList.ClosePopUp(uct_onClosePopUp);
                        StaticFunctions.BoundSpicalContr(dtContr, dsFormAdt, dsFormUkyndaAdt, uct, dtShow);
                        if (uct.Parent != null && UkyndaGcItems.ContainsKey(uct.Parent.Name))
                        {
                            UkyndaGcEdit uky = UkyndaGcItems[uct.Parent.Name];
                            if (uky.GridViewName == "gridVMain" && uct.Tag != null && uct.Tag.ToString() != string.Empty)
                                lisOrdSpCtrlId.Add(uct.Tag.ToString());
                        }
                        break;
                    case "ExtendControl.ExtPopupTree":
                        ExtendControl.ExtPopupTree ept = ctrl as ExtendControl.ExtPopupTree;
                        ept.Properties.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.lookUpEdit_Properties_Closed);
                        StaticFunctions.BoundSpicalContr(dtContr, dsFormAdt, dsFormUkyndaAdt, ept, dtShow);
                        if (ept.Parent != null && UkyndaGcItems.ContainsKey(ept.Parent.Name))
                        {
                            UkyndaGcEdit uky = UkyndaGcItems[ept.Parent.Name];
                            if (uky.GridViewName == "gridVMain" && ept.Tag != null && ept.Tag.ToString() != string.Empty)
                                lisOrdSpCtrlId.Add(ept.Tag.ToString());
                        }
                        break;

                    default:
                        break;
                }
            }

            #endregion

            foreach (SimpleButton btn in lisBtnAlls)
            {
                btn.Click += new System.EventHandler(this.btn_Click);
            }
            this.controlNavigator1.Location = new System.Drawing.Point(int.Parse(drMain["ControlNavLocalPointX"].ToString()), int.Parse(drMain["ControlNavLocalPointY"].ToString()));
            if (this.controlNavigator1.Location.X == 0 && this.controlNavigator1.Location.Y == 0)
            {
                this.controlNavigator1.Visible = false;
            }
            else
            {
                this.controlNavigator1.Visible = true;
            }

            string strlistFilterFields = drMain["FilterFields"].ToString();
            if (strlistFilterFields == string.Empty)
            {
                this.txtOrdFilter.Visible = false;
            }
            else
            {
                this.txtOrdFilter.Visible = true;
                this.txtOrdFilter.Location = new System.Drawing.Point(int.Parse(drMain["ControlFilterLocalPointX"].ToString()), int.Parse(drMain["ControlFilterLocalPointY"].ToString()));
                strFilterFields = strlistFilterFields.Split(",".ToCharArray());
            }
            StaticFunctions.SetBtnStyle(barManager1, lisBtnAlls, drMain);
        }
        private void SetControlEvent(Control ctrl)
        {
            TextEdit txt = ctrl as TextEdit;
            if (Convert.ToString(ctrl.Tag) != string.Empty
                && dtBtnsM.Select("IsControlSet=1 AND BtnName='" + ctrl.Name.ToString() + "'").Length > 0)
            {
                txt.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(txt_EditValueChanging);
            }
            else if ((strGetWFBalceCtrlIds + ",").IndexOf(ctrl.Name + ",") != -1)
            {
                txt.KeyPress += new KeyPressEventHandler(txt_KeyPress);
            }            
        }

        public override void GetCurrAllItem()
        {
            string[] strKey = "CDateSt,CDateEd,CUserId,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string strDt = DateTime.Today.ToShortDateString();
            string[] strVal = new string[] {strDt,strDt,
                 CApplication.App.CurrentSession.UserId.ToString(),
                 CApplication.App.CurrentSession.UserId.ToString(),
                 CApplication.App.CurrentSession.DeptId.ToString(),
                 CApplication.App.CurrentSession.FyId.ToString(),
                 strQueryFlag };
            frmDataTable = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
            frmDataTable.AcceptChanges();
        }
        public override void RefreshItem()
        {
            DataRow dr = gridVMain.GetFocusedDataRow();
            if (dr == null)
                return;

            string strKeyId = dr[strKeyFiled].ToString();
            if (strKeyId == string.Empty)
                return;

            DataTable dtTemp = GetDataById(strKeyId);
            if (dtTemp == null)
            {
                return;
            }
            if (dtTemp.Rows.Count == 0)
            {
                MessageBox.Show("出错:无法读取单据信息.");
                return;
            }
            dr.ItemArray = dtTemp.Rows[0].ItemArray;
            frmDataTable.AcceptChanges();

            BoundGridContr(string.Empty);
        }
        private DataTable GetDataById(string strKeyId)
        {
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] { strKeyId, 
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),strQueryFlag };
            DataTable dtTemp = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
            if (dtTemp == null)
            {
                return null;
            }
            return dtTemp;
        }
        private void BoundGridContr(string strKeyId)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                frmDataTable.AcceptChanges();

                blInitBound = true;
                gridCMain.DataSource = frmDataTable.DefaultView;//可能引发gridView1_FocusedRowChanged
                gridVMain.BestFitColumns();
                blInitBound = false;
                if (strKeyId == string.Empty)
                {
                    gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVMain.FocusedRowHandle));
                }
                else
                {
                    for (int i = 0; i < gridVMain.RowCount; i++)
                    {
                        if (gridVMain.GetDataRow(i)[strKeyFiled].ToString() == strKeyId)
                        {
                            blInitBound = true;
                            gridVMain.FocusedRowHandle = i;
                            gridVMain.ClearSelection();
                            gridVMain.SelectRow(i);
                            blInitBound = false;
                            gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVMain.FocusedRowHandle));
                            break;
                        }
                    }
                }
                if (frmDataTable.Rows.Count == 0)
                {
                    foreach (string strKey in UkyndaGcItems.Keys)
                    {
                        StaticFunctions.SetControlEmpty(UkyndaGcItems[strKey].CtrParentControl);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("错误:" + err.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void gridVMain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1 || blInitBound)
                return;

            GridView gv = sender as GridView;
            DataRow drP = gv.GetDataRow(e.PrevFocusedRowHandle);
            if (drP != null && drP.RowState != DataRowState.Unchanged)
            {
                blInitBound = true;
                drP.RejectChanges();//引发gridView1_FocusedRowChanged
                blInitBound = false;
            }
            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null)
                return;

            if (dr.RowState != DataRowState.Added)
            {
                foreach (string strKey in UkyndaGcItems.Keys)
                {
                    if (UkyndaGcItems[strKey].GridViewName == "gridVMain")
                        SetGroupCtrol(UkyndaGcItems[strKey], "VIEW");
                }
            }
            foreach (string strKey in UkyndaGcItems.Keys)
            {
                if (UkyndaGcItems[strKey].GridViewName == "gridVMain")
                    StaticFunctions.SetControlBindings(UkyndaGcItems[strKey].CtrParentControl, gv.GridControl.DataSource as DataView, dr);
            }
            blSetItemStatusByOrd = true;
            BoundGridInfoData(dr[strKeyFiled].ToString());
            blSetItemStatusByOrd = false;

            foreach (string strKey in UkyndaGcItems.Keys)
            {
                if (UkyndaGcItems[strKey].GridViewName != "gridVMain")
                    SetGroupCtrol(UkyndaGcItems[strKey], "VIEW");
            }
            SetBtnState(dr[strOrdState].ToString());
        }
        private void BoundGridInfoData(string strKeyId)
        {
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {strKeyId==string.Empty?"-1":strKeyId,
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

                blInitBound = true;
                gcItems[strGv].GridControl.DataSource = dt.DefaultView;
                GridView gv = gcItems[strGv];
                gv.BestFitColumns();
                blInitBound = false;
                if (GvNeedGCEdit.Contains(gv))
                    gridVInfo_FocusedRowChanged(gv, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gv.FocusedRowHandle));
            }
            StaticFunctions.ReBoundSpicalContr(dtContr, ds, strReBindCtrlIds, dtShow, dicCtrlS);
        }
        private void gridVInfo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1 || blInitBound)
                return;

            GridView gv = sender as GridView;
            DataRow drP = gv.GetDataRow(e.PrevFocusedRowHandle);
            if (drP != null && drP.RowState != DataRowState.Unchanged)
            {
                blInitBound = true;
                drP.RejectChanges();//引发gridView1_FocusedRowChanged
                blInitBound = false;
            }
            DataRow dr = gv.GetFocusedDataRow();
            if (dr == null)
                return;

            if (dr.RowState != DataRowState.Added)
            {
                if (!blSetItemStatusByOrd)
                {
                    SetBtnItemByMode(gv.Name, "VIEW");
                    foreach (string strKey in UkyndaGcItems.Keys)
                    {
                        if (UkyndaGcItems[strKey].GridViewName == gv.Name)
                            SetGroupCtrol(UkyndaGcItems[strKey], "VIEW");
                    }
                }
            }
            foreach (string strKey in UkyndaGcItems.Keys)
            {
                if (UkyndaGcItems[strKey].GridViewName == gv.Name)
                    StaticFunctions.SetControlBindings(UkyndaGcItems[strKey].CtrParentControl, gv.GridControl.DataSource as DataView, dr);
            }
        }

        private void SetBarItemByMode(string strMode)
        {
            if (strMode != "VIEW")
            {
                StaticFunctions.SetBarItemStatu(bar2, "btnSave,btnCancel");
            }
        }
        private void SetBtnItemByMode(string strGridViewName, string strMode)
        {
            string strBtnIdsOrdSte = string.Empty;
            if (strMode == "VIEW")
            {
                DataRow drOrd = this.gridVMain.GetFocusedDataRow();
                DataRow[] drSte = dtSte.Select("OrdState='" + drOrd[strOrdState].ToString() + "'");
                if (drSte.Length > 0)
                {
                    strBtnIdsOrdSte = drSte[0]["BtnEnableId"].ToString();
                }
            }
            DataRow[] drTabs = dtTabs.Select("IsNeedGCEdit=1 AND GridViewName='" + strGridViewName + "'");
            foreach (DataRow dr in drTabs)
            {
                switch (strMode)
                {
                    case "VIEW":
                        foreach (SimpleButton btn in lisBtns[strGridViewName])
                        {
                            if (("," + strBtnIdsOrdSte + ",").Replace(" ", "").IndexOf(btn.Name + ",") >= 0)
                            {
                                btn.Enabled = true;
                            }
                            else
                            {
                                btn.Enabled = false;
                            }
                        }
                        break;

                    case "ADD":
                        StaticFunctions.SetBtnStatu(lisBtns[strGridViewName], dr["BtnEnableIdAdd"].ToString());
                        break;

                    case "EDIT":
                        StaticFunctions.SetBtnStatu(lisBtns[strGridViewName], dr["BtnEnableIdEdit"].ToString());
                        break;

                    default:
                        break;
                }
            }
        }
        private void SetGroupCtrol(UkyndaGcEdit gcEdit, string strMode)
        {
            if (gcEdit.StrMode == strMode)
                return;

            gcEdit.StrMode = strMode;
            switch (strMode)
            {
                case "VIEW":
                    StaticFunctions.SetControlEnable(gcEdit.CtrParentControl, false);
                    break;

                case "ADD":
                    StaticFunctions.SetControlEnable(gcEdit.CtrParentControl, true, gcEdit.StrNoEnableCtrIds);
                    break;

                case "EDIT":
                    StaticFunctions.SetControlEnable(gcEdit.CtrParentControl, true, gcEdit.StrNoEnableEditCtrIds);
                    break;

                default:
                    break;
            }
        }

        private void SetBtnState(string strState)
        {
            DataRow[] drSte = dtSte.Select("OrdState='" + strState + "'");
            foreach (DataRow dr in drSte)
            {
                StaticFunctions.SetBarItemStatu(bar2, dr["BarItemEnableId"].ToString() + ",btnReLoad");
                StaticFunctions.SetBtnStatu(lisBtnAlls, dr["BtnEnableId"].ToString());
            }
        }

        private void btn_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (blSysProcess)
                return;
            try
            {
                blSysProcess = true;
                this.Cursor = Cursors.WaitCursor;
                switch (e.Item.Name)
                {
                    case "btnCopy":
                        DoCopyAdd();
                        break;
                    case "btnAdd":
                        DoAdd();
                        break;
                    case "btnEdit":
                        DoEdit();
                        break;
                    case "btnCancel":
                        DoCancel();
                        break;
                    case "btnSave":
                        DoSaveOrd();
                        break;
                    case "btnReLoad":
                        DoReLoad();
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
                blSysProcess = false;
            }
        }
        private void btn_Click(object sender, EventArgs e)
        {
            if (blSysProcess)
                return;
            try
            {
                blSysProcess = true;
                this.Cursor = Cursors.WaitCursor;
                SimpleButton btn = sender as SimpleButton;
                DoBtnSpecial(btn.Name);
            }
            catch (Exception err)
            {
                MessageBox.Show("错误:" + err.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                blSysProcess = false;
            }
        }

        private void DoReLoad()
        {
            bar2.Reset();
            gcItems.Clear();
            lisrepImg.Clear();
            lisBtns.Clear();
            lisBtnAlls.Clear();
            GvNeedGCEdit.Clear();
            lisOrdSpCtrlId.Clear();
            xtabItemInfo.TabPages.Clear();
            this.xtabOrdParent.TabPages.Clear();
            arrContrSeq.Clear();
            dicCtrlS.Clear();
            foreach (string strkey in UkyndaGcItems.Keys)
            {
                UkyndaGcEdit ugc = UkyndaGcItems[strkey];
                if (ugc.GridViewName == "gridVMain")
                {
                    this.Controls.Remove(ugc.CtrParentControl);
                }
            }
            UkyndaGcItems.Clear();
            dsLoad = null;
            InitContr();

            gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVMain.FocusedRowHandle));
            frmSysBusEdit_Load(null, null);
        }
        private void DoCopyAdd()
        {
            frmImportOrd frm = new frmImportOrd();
            frm.StrUpdSpName = strSpName;
            frm.StrImpFlag = drMain["ImpFlag"].ToString();
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.Yes)
            {
                DataRow drNew = this.frmDataTable.NewRow();
                drNew.ItemArray = frm.DsRets.Tables[0].Rows[0].ItemArray;
                blInitBound = true;
                frmDataTable.Rows.InsertAt(drNew, 0);//可能引发gridView1_FocusedRowChanged
                frmDataTable.AcceptChanges();
                gridVMain.MoveFirst();
                blInitBound = false;
                gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));

                MessageBox.Show("成功复制，请核对修改相应信息.");
            }
        }
        private void DoAdd()
        {
            DataRow drFoc = this.gridVMain.GetFocusedDataRow();
            UkyndaGcEdit GcOrd = UkyndaGcItems["gcOrd"];
            DataRow drNew = this.frmDataTable.NewRow();
            blInitBound = true;
            frmDataTable.Rows.InsertAt(drNew, 0);//可能引发gridView1_FocusedRowChanged
            gridVMain.MoveFirst();
            foreach (string strKey in lisOrdSpCtrlId)
            {
                if (frmDataTable.Columns.Contains(strKey))
                    drNew[strKey] = DBNull.Value;
            }
            if (GcOrd.BlSetDefault)
            {
                StaticFunctions.SetContrDefaultValue(GcOrd.CtrParentControl, dtShow, drNew);
            }
            string strOrdEnableId = string.Empty;
            DataRow[] drSte = dtSte.Select("OrdState='" + drNew[strOrdState].ToString() + "'");
            if (drSte.Length > 0)
            {
                strOrdEnableId = drSte[0]["OrdEnableId"].ToString();
            }
            if (strOrdEnableId != string.Empty)
            {
                foreach (string strKey in UkyndaGcItems.Keys)
                {
                    UkyndaGcEdit GcItem = UkyndaGcItems[strKey];
                    if (GcItem.GridViewName == "gridVMain" && strKey != "gcOrd" && GcItem.BlSetDefault &&
                        ("," + strOrdEnableId + ",").Replace(" ", "").IndexOf(strKey + ",") >= 0)
                    {
                        StaticFunctions.SetContrDefaultValue(GcItem.CtrParentControl, dtShow, drNew);
                    }
                }
            }
            string strCopyFields = drMain["CopyFields"].ToString();
            List<string> lisSetV = new List<string>();
            if (strCopyFields.Trim() != string.Empty && drFoc != null)
            {
                string[] arrFields = strCopyFields.Split(",".ToCharArray());
                foreach (string strField in arrFields)
                {
                    lisSetV.Add(strField);
                    drNew[strField] = drFoc[strField];
                }
            }
            blInitBound = false;
            gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            SetBarItemByMode("ADD");
            SetGroupCtrol(GcOrd, "ADD");

            foreach (string strKey in UkyndaGcItems.Keys)
            {
                UkyndaGcEdit GcItem = UkyndaGcItems[strKey];
                if (GcItem.GridViewName == "gridVMain" && strKey != "gcOrd")
                {
                    if (("," + strOrdEnableId + ",").Replace(" ", "").IndexOf(strKey + ",") >= 0)
                    {
                        SetGroupCtrol(UkyndaGcItems[strKey], "ADD");
                    }
                    else
                    {
                        SetGroupCtrol(UkyndaGcItems[strKey], "VIEW");
                    }
                }
            }
            if (xtabOrdParent.TabPages.Count == 0)
            {
                StaticFunctions.SetFirstEditContrSelect(GcOrd.CtrFirstAddFocusContr);
            }
            else
            {
                blInitBound = true;
                xtabOrdParent.SelectedTabPageIndex = 0;
                blInitBound = false;
                xtabOrdParent_SelectedPageChanged(null, new TabPageChangedEventArgs(null, null));
            }
        }
        private void DoEdit()
        {
            DataRow dr = this.gridVMain.GetFocusedDataRow();
            if (dr == null)
                return;

            SetBarItemByMode("EDIT");

            string strOrdEnableId = string.Empty;
            DataRow[] drSte = dtSte.Select("OrdState='" + dr[strOrdState].ToString() + "'");
            if (drSte.Length > 0)
            {
                strOrdEnableId = drSte[0]["OrdEnableId"].ToString();
            }
            if (strOrdEnableId == string.Empty)
            {
                strOrdEnableId = "gcOrd";
            }
            foreach (string strKey in UkyndaGcItems.Keys)
            {
                UkyndaGcEdit GcItem = UkyndaGcItems[strKey];
                if (GcItem.GridViewName == "gridVMain" &&
                    ("," + strOrdEnableId + ",").Replace(" ", "").IndexOf(strKey + ",") >= 0)
                {
                    SetGroupCtrol(GcItem, "EDIT");
                    SetContrEditFromDpl(GcItem, string.Empty);
                }
            }
            if (xtabOrdParent.TabPages.Count == 0)
            {
                UkyndaGcEdit GcOrd = UkyndaGcItems["gcOrd"];
                StaticFunctions.SetFirstEditContrSelect(GcOrd.CtrFirstEditFocusContr);
            }
            else
            {
                xtabOrdParent_SelectedPageChanged(null, new TabPageChangedEventArgs(null, null));
            }
        }
        private void DoCancel()
        {
            DataRow dr = this.gridVMain.GetFocusedDataRow();
            if (dr == null)
            {
                return;
            }
            blInitBound = true;
            frmDataTable.RejectChanges();//引发gridView1_FocusedRowChanged
            frmDataTable.AcceptChanges();
            blInitBound = false;

            if (frmDataTable.Rows.Count == 0)
            {
                this.Close();
                this.Dispose();
            }
            else
            {
                gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVMain.FocusedRowHandle));
            }
        }
        private void DoSaveOrd()
        {
            DataRow dr = gridVMain.GetFocusedDataRow();
            if (dr == null)
                return;

            List<UkyndaGcEdit> listGcEdit = new List<UkyndaGcEdit>();
            List<string> listFileds = new List<string>();
            string strSpParmName = string.Empty;
            List<string> lisSpParmValue = new List<string>();
            foreach (string strKey in UkyndaGcItems.Keys)
            {
                UkyndaGcEdit GcItem = UkyndaGcItems[strKey];
                if (GcItem.GridViewName == "gridVMain" && GcItem.StrMode != "VIEW")
                {
                    listGcEdit.Add(GcItem);
                    listFileds.AddRange(GcItem.StrFileds);

                    string strSpParmNameTemp = string.Empty;
                    List<string> lisSpParmValueTemp = StaticFunctions.GetPassSpParmValue(GcItem.CtrParentControl, dtShow, out strSpParmNameTemp);
                    lisSpParmValue.AddRange(lisSpParmValueTemp);
                    if (strSpParmName != string.Empty)
                        strSpParmName += ",";
                    strSpParmName += strSpParmNameTemp;
                }
            }
            if (strSpParmName != string.Empty)
                strSpParmName += ",";
            if (listGcEdit.Count == 0)
                return;

            foreach (UkyndaGcEdit GcEdit in listGcEdit)
            {
                if (GcEdit.SaveMoveToCtrId != string.Empty)
                {
                    StaticFunctions.SetContrSelect(GcEdit.CtrParentControl, GcEdit.SaveMoveToCtrId);
                }
                if (!StaticFunctions.CheckSave(dr, GcEdit.CtrParentControl, dtShow))
                    return;
            }

            if (!DoCheckSaveOrd())
                return;
            DataSet dtAdd = null;
            string strField = string.Empty;
            string strValues = string.Empty;
            btnSave.Enabled = false;
            bool blAdd = false;
            try
            {
                if (dr[strKeyFiled].ToString() == string.Empty)
                {
                    blAdd = true;
                    strValues = StaticFunctions.GetAddValues(dr, listFileds.ToArray(), out strField);
                    

                    DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + strAddFlag + "'");
                    if (drShares.Length == 0)
                    {
                        string[] strKey = (strSpParmName + "strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                        lisSpParmValue.AddRange(new string[] {
                        strField,
                        strValues,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         strAddFlag});
                        dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
                    }
                    else
                    {
                        string[] strKey = (strSpParmName + "BsuSetSp_Id,strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id").Split(",".ToCharArray());
                        lisSpParmValue.AddRange(new string[] {drShares[0]["BsuSetSp_Id"].ToString(),
                        strField,
                        strValues,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString()});
                        dtAdd = this.DataRequest_By_DataSet(strShareSpName, strKey, lisSpParmValue.ToArray());
                    }
                    if (dtAdd == null)
                    {
                        btnSave.Enabled = true;
                        return;
                    }
                    DataRow drNew = dtAdd.Tables[0].Rows[0];
                    dr[strKeyFiled] = drNew[strKeyFiled];
                    if (dtAdd.Tables[0].Columns.Contains("UpdateFields"))
                    {
                        StaticFunctions.UpdateDataRowSyn(dr, drNew, drNew["UpdateFields"].ToString());
                    }
                    blInitBound = true;
                    StaticFunctions.UpdateGridControlDataSouce(dtAdd, drMain["AddUpdateGvName"].ToString(), gcItems, dtTabs);
                    StaticFunctions.ReBoundSpicalContr(dtContr, dtAdd, strReBindCtrlIds, dtShow, dicCtrlS);
                    blInitBound = false;
                    SetUpdateGvFocusedRowChanged(drMain["AddUpdateGvName"].ToString());

                    //生产加工单条码图片并上传
                    if (dtAdd.Tables[0].Columns.IndexOf("BarcodeFileName") != -1 
                        && dtAdd.Tables[0].Columns.IndexOf("BarcodeNumber") != -1)
                    {
                        drAddTemp = drNew;
                        tdCreateBarcodeImage = new Thread(new ThreadStart(CreateBarcodeImage));
                        tdCreateBarcodeImage.Start();
                    }
                }
                else
                {
                    strValues = StaticFunctions.GetUpdateValues(frmDataTable, dr, listFileds.ToArray());
                    if (strValues != string.Empty)
                    {
                       

                        DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + strEditFlag + "'");
                        if (drShares.Length == 0)
                        {
                            string[] strKey = (strSpParmName + "strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                            lisSpParmValue.AddRange(new string[] { 
                             strValues,
                             dr[strKeyFiled].ToString(),
                             CApplication.App.CurrentSession.UserId.ToString(),
                             CApplication.App.CurrentSession.DeptId.ToString(),
                             CApplication.App.CurrentSession.FyId.ToString(),
                             strEditFlag});
                            dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
                        }
                        else
                        {
                            string[] strKey = (strSpParmName + "BsuSetSp_Id,strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id").Split(",".ToCharArray());
                            lisSpParmValue.AddRange(new string[] {drShares[0]["BsuSetSp_Id"].ToString(),
                                 strValues,
                                 dr[strKeyFiled].ToString(),
                                 CApplication.App.CurrentSession.UserId.ToString(),
                                 CApplication.App.CurrentSession.DeptId.ToString(),
                                 CApplication.App.CurrentSession.FyId.ToString()});
                            dtAdd = this.DataRequest_By_DataSet(strShareSpName, strKey, lisSpParmValue.ToArray());
                        }
                        if (dtAdd == null)
                        {
                            btnSave.Enabled = true;
                            return;
                        }
                        if (dtAdd.Tables[0].Rows.Count > 0 && dtAdd.Tables[0].Columns.Contains("UpdateFields"))
                        {
                            DataRow drNew = dtAdd.Tables[0].Rows[0];
                            StaticFunctions.UpdateDataRowSyn(dr, drNew, drNew["UpdateFields"].ToString());
                        }
                        blInitBound = true;
                        StaticFunctions.UpdateGridControlDataSouce(dtAdd, drMain["EditUpdateGvName"].ToString(), gcItems, dtTabs);
                        StaticFunctions.ReBoundSpicalContr(dtContr, dtAdd, strReBindCtrlIds, dtShow, dicCtrlS);
                        blInitBound = false;
                        SetUpdateGvFocusedRowChanged(drMain["EditUpdateGvName"].ToString());
                    }
                }
                dr.AcceptChanges();
                SetBtnState(dr[strOrdState].ToString()); 
                foreach (UkyndaGcEdit GcEdit in listGcEdit)
                {
                    SetGroupCtrol(GcEdit, "VIEW");
                }
                if (dtAdd != null && !dtAdd.Tables[0].Columns.Contains("Ct"))//碰到如果包含CT字段就不继续，否则继续
                {
                    DoSaveSynBtnEvents(drMain, blAdd);
                }
            }
            catch (Exception ERR)
            {
                MessageBox.Show("保存出错:" + ERR.Message);
                btnSave.Enabled = true;
                return;
            }
        }
        private void CreateBarcodeImage()
        {
            try
            {
                StaticFunctions.CreateBarcodeImage(drAddTemp, "BarcodeFileName", "BarcodeNumber");
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成条码图像出错:" + ex.Message);
            }
            finally
            {
                tdCreateBarcodeImage.Abort();
                tdCreateBarcodeImage = null;
                drAddTemp = null;
            }
        }
        private void DoUpdateOrdState(DataRow drBtn)
        {
            DataRow dr = gridVMain.GetFocusedDataRow();
            if (dr == null || dr[strKeyFiled].ToString() == string.Empty)
                return;

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
                    string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                    lisSpParmValue.AddRange(new string[] { 
                         dr[strKeyFiled].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         drBtn["UpdateOrdStateFlag"].ToString()});
                    dsAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
                }
                else
                {
                    string[] strKey = "BsuSetSp_Id,Key_Id,EUser_Id,EDept_Id,Fy_Id".Split(",".ToCharArray());
                    lisSpParmValue.AddRange(new string[] {drShares[0]["BsuSetSp_Id"].ToString(),
                         dr[strKeyFiled].ToString(),
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
                frm.DrOrd = dr;
                frm.StrUpdKeyIds = dr[strKeyFiled].ToString();
                frm.StrUpdSpName = strSpName;
                frm.DtSp = dtSp;
                if (frm.ShowDialog(this) != DialogResult.Yes)
                    return;

                dsAdd = frm.DtRets;
            }
            DataRow drNew = dsAdd.Tables[0].Rows[0];
            string strState = drNew[strOrdState].ToString();
            dr[strOrdState] = strState;
            if (dsAdd.Tables[0].Columns.Contains("UpdateFields"))
            {
                StaticFunctions.UpdateDataRowSyn(dr, drNew, drNew["UpdateFields"].ToString());
            }
            dr.AcceptChanges();
            if (drBtn["IsSuccessRefresh"].ToString() == "True")
            {
                gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVMain.FocusedRowHandle));
                return;
            }
            blInitBound = true;
            StaticFunctions.UpdateGridControlDataSouce(dsAdd, drBtn["SynUpdateGvName"].ToString(), gcItems, dtTabs);
            StaticFunctions.ReBoundSpicalContr(dtContr, dsAdd, strReBindCtrlIds, dtShow, dicCtrlS);
            blInitBound = false;
            SetUpdateGvFocusedRowChanged(drBtn["SynUpdateGvName"].ToString());
            foreach (string strGcKey in UkyndaGcItems.Keys)
            {
                SetGroupCtrol(UkyndaGcItems[strGcKey], "VIEW");
            }
            SetBtnState(strState);
        }
        private void DoPrint(DataRow drBtn)
        {
            DataRow dr = gridVMain.GetFocusedDataRow();
            if (dr == null)
                return;

            string strRptName = string.Empty;
            string strSpFlag = string.Empty;
            string strTitle = string.Empty;
            if (!StaticFunctions.GetBtnMRpt(dtBtnsM, drBtn, dr, strKeyFiled, this
                , drBtn["RptName"].ToString(), drBtn["RptFlag"].ToString(), drBtn["RptTitle"].ToString()
                , out strRptName, out strSpFlag, out strTitle))
            {
                return;
            }

            string[] strParams = "OrdId,OrdIds,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strParaVals = new string[] { 
                dr[strKeyFiled].ToString(), dr[strKeyFiled].ToString(), 
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                strSpFlag};
            DataSet ds = this.DataRequest_By_DataSet(strSpRptName, strParams, strParaVals);
            StaticFunctions.ShowRptRS(strRptName, strTitle, this.ParentForm, null, null, ds);
        }

        private void DoEditSpecial(DataRow drBtn)
        {
            UkyndaGcEdit GcEdit = UkyndaGcItems[drBtn["EditGroupCName"].ToString()];
            DataRow dr = GcEdit.GridViewEdit.GetFocusedDataRow();
            if (dr == null)
                return;

            if (GcEdit.GridViewName == "gridVMain")
            {
                SetBarItemByMode("EDIT");
            }
            else
            {
                SetBtnItemByMode(GcEdit.GridViewName, "EDIT");
            }
            SetGroupCtrol(GcEdit, "EDIT");
            SetContrEditFromDpl(GcEdit,string.Empty);
            StaticFunctions.SetFirstEditContrSelect(GcEdit.CtrFirstEditFocusContr);

            blReAddInfo = false;
        }
        private void DoAddInfoSpecial(DataRow drBtn)
        {
            DataRow drOrd = gridVMain.GetFocusedDataRow();
            if (drOrd == null || drOrd[strKeyFiled].ToString() == string.Empty)
            {
                MessageBox.Show("请先保存单据表头.");
                return;
            }
            UkyndaGcEdit GcEdit = UkyndaGcItems[drBtn["EditGroupCName"].ToString()];
            GcEdit.GridViewEdit.ClearColumnsFilter();
            GcEdit.GridViewEdit.ClearSorting();

            blReAddInfo = (drBtn["IsReAddInfoBtn"].ToString() == "True");
            CopyFields = drBtn["CopyFields"].ToString();
            CopyFieldsOrd = drBtn["CopyOrdFields"].ToString();
            DoAddInfo(GcEdit, CopyFields, drOrd, CopyFieldsOrd, true);
        }
        private void DoAddInfo(UkyndaGcEdit GcEdit, string strCopyFields, DataRow drOrd, string strCopyOrdFields, bool blFirst)
        {
            DataRow drFoc = GcEdit.GridViewEdit.GetFocusedDataRow();
            DataTable dtInfo = (GcEdit.GridViewEdit.GridControl.DataSource as DataView).Table;
            DataRow drNew = dtInfo.NewRow();
            blInitBound = true;
            dtInfo.Rows.InsertAt(drNew, dtInfo.Rows.Count);//可能引发gridView1_FocusedRowChanged
            GcEdit.GridViewEdit.MoveLast();
            if (GcEdit.BlSetDefault)
            {
                StaticFunctions.SetContrDefaultValue(GcEdit.CtrParentControl, dtShow, drNew);
            }
            if (strCopyOrdFields.Trim() != string.Empty)
            {
                string[] arrFields = strCopyOrdFields.Split(",".ToCharArray());
                foreach (string strField in arrFields)
                {
                    drNew[strField] = drOrd[strField];
                }
            }
            if (strCopyFields.Trim() != string.Empty && drFoc != null)
            {
                string[] arrFields = strCopyFields.Split(",".ToCharArray());
                foreach (string strField in arrFields)
                {
                    drNew[strField] = drFoc[strField];
                }
            }
            blInitBound = false;
            gridVInfo_FocusedRowChanged(GcEdit.GridViewEdit, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, GcEdit.GridViewEdit.FocusedRowHandle));
            if (blFirst)
            {
                SetBtnItemByMode(GcEdit.GridViewName, "ADD");
                SetGroupCtrol(GcEdit, "ADD");
            }
            SetContrEditFromDpl(GcEdit, string.Empty);
            StaticFunctions.SetFirstEditContrSelect(GcEdit.CtrFirstAddFocusContr);
        }
        private void DoCopyAddInfoSpecial(DataRow drBtn)
        {
            DataRow drOrd = gridVMain.GetFocusedDataRow();
            if (drOrd == null || drOrd[strKeyFiled].ToString() == string.Empty)
            {
                MessageBox.Show("请先保存单据表头.");
                return;
            }
            UkyndaGcEdit GcEdit = UkyndaGcItems[drBtn["EditGroupCName"].ToString()];
            DataRow[] drs = dtTabs.Select("IsNeedGCEdit=1 AND GridViewName='" + GcEdit.GridViewEdit.Name + "'");
            if (drs.Length <= 0)
            {
                return;
            }
            GcEdit.GridViewEdit.ClearColumnsFilter();
            GcEdit.GridViewEdit.ClearSorting();

            DataTable dtInfo = (GcEdit.GridViewEdit.GridControl.DataSource as DataView).Table;
            DataRow drNew = dtInfo.NewRow();
            DataRow drFoc = GcEdit.GridViewEdit.GetFocusedDataRow();
            if (drFoc != null)
            {
                drNew.ItemArray = drFoc.ItemArray;
                drNew[drs[0]["EditInfoKeyId"].ToString()] = DBNull.Value;
            }

            blInitBound = true;
            dtInfo.Rows.InsertAt(drNew, dtInfo.Rows.Count);//可能引发gridView1_FocusedRowChanged
            GcEdit.GridViewEdit.MoveLast();
            blInitBound = false;
            gridVInfo_FocusedRowChanged(GcEdit.GridViewEdit, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, GcEdit.GridViewEdit.FocusedRowHandle));
            SetBtnItemByMode(GcEdit.GridViewName, "ADD");
            SetGroupCtrol(GcEdit, "ADD");
            StaticFunctions.SetFirstEditContrSelect(GcEdit.CtrFirstAddFocusContr);

            blReAddInfo = false;
        }
        private void DoCancelInfoSpecial(DataRow drBtn)
        {
            string strSaveCancelGvName = drBtn["SaveCancelGvName"].ToString();
            UkyndaGcEdit GcEdit = null;
            foreach (string strKey in UkyndaGcItems.Keys)
            {
                if (UkyndaGcItems[strKey].GridViewName == strSaveCancelGvName && UkyndaGcItems[strKey].StrMode != "VIEW")
                {
                    GcEdit = UkyndaGcItems[strKey];
                    break;
                }
            }
            if (GcEdit == null)
                return;

            DataRow dr = GcEdit.GridViewEdit.GetFocusedDataRow();
            if (dr == null)
                return;

            DataTable dtInfo = (GcEdit.GridViewEdit.GridControl.DataSource as DataView).Table;
            blInitBound = true;
            dtInfo.RejectChanges();//引发gridView1_FocusedRowChanged
            dtInfo.AcceptChanges();
            blInitBound = false;
            blSetItemStatusByOrd = true;
            gridVInfo_FocusedRowChanged(GcEdit.GridViewEdit, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, GcEdit.GridViewEdit.FocusedRowHandle));
            blSetItemStatusByOrd = false;

            SetBtnItemByMode(GcEdit.GridViewName, "VIEW");
            foreach (string strKey in UkyndaGcItems.Keys)
            {
                if (UkyndaGcItems[strKey].GridViewName == GcEdit.GridViewName)
                    SetGroupCtrol(UkyndaGcItems[strKey], "VIEW");
            }
            SetGroupCtrol(GcEdit, "VIEW");
        }
        private void DoSaveInfoSpecial(DataRow drBtn)
        {
            string strSaveCancelGvName = drBtn["SaveCancelGvName"].ToString();
            UkyndaGcEdit GcEdit = null;
            foreach (string strKey in UkyndaGcItems.Keys)
            {
                if (UkyndaGcItems[strKey].GridViewName == strSaveCancelGvName && UkyndaGcItems[strKey].StrMode != "VIEW")
                {
                    GcEdit = UkyndaGcItems[strKey];
                    break;
                }
            }
            if (GcEdit == null)
                return;

            if (GcEdit.SaveMoveToCtrId != string.Empty)
            {
                StaticFunctions.SetContrSelect(GcEdit.CtrParentControl, GcEdit.SaveMoveToCtrId);
            }
            DataRow dr = GcEdit.GridViewEdit.GetFocusedDataRow();
            if (dr == null)
                return;

            if (!StaticFunctions.CheckSave(dr, GcEdit.CtrParentControl, dtShow))
                return;

            DataRow drInfoEdit = null;
            DataRow[] drs = dtTabs.Select("IsNeedGCEdit=1 AND GridViewName='" + strSaveCancelGvName + "'");
            if (drs.Length > 0)
            {
                drInfoEdit = drs[0];
            }
            if (drInfoEdit == null)
                return;

            DataRow drOrd = gridVMain.GetFocusedDataRow();
            if (drOrd == null || drOrd[strKeyFiled].ToString() == string.Empty)
            {
                return;
            }
            if (!DoCheckSaveInfo())
                return;

            string strEditInfoKeyId = drInfoEdit["EditInfoKeyId"].ToString();
            string strField = string.Empty;
            string strValues = string.Empty;
            bool blAdd = false;
            try
            {
                if (dr[strEditInfoKeyId].ToString() == string.Empty)
                {
                    blAdd = true;
                    dr[strKeyFiled] = drOrd[strKeyFiled];
                    strValues = StaticFunctions.GetAddValues(dr, GcEdit.StrFileds, out strField);

                    DataSet dtAdd = null;
                    string strSpParmName = string.Empty;
                    List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(GcEdit.CtrParentControl, dtShow, out strSpParmName);
                    if (strSpParmName != string.Empty)
                        strSpParmName += ",";

                    DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + drInfoEdit["AddFlag"].ToString() + "'");
                    if (drShares.Length == 0)
                    {
                        string[] strKey = (strSpParmName + "Ord_Id,strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                        lisSpParmValue.AddRange(new string[] {dr[strKeyFiled].ToString(),
                        strField,
                        strValues,
                        CApplication.App.CurrentSession.UserId.ToString(),
                        CApplication.App.CurrentSession.DeptId.ToString(),
                        CApplication.App.CurrentSession.FyId.ToString(),
                        drInfoEdit["AddFlag"].ToString()});
                        dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
                    }
                    else
                    {
                        string[] strKey = (strSpParmName + "BsuSetSp_Id,Ord_Id,strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id").Split(",".ToCharArray());
                        lisSpParmValue.AddRange(new string[] {drShares[0]["BsuSetSp_Id"].ToString(),dr[strKeyFiled].ToString(),
                        strField,
                        strValues,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString()});
                        dtAdd = this.DataRequest_By_DataSet(strShareSpName, strKey, lisSpParmValue.ToArray());
                    }
                    if (dtAdd == null)
                    {
                        return;
                    }
                    DataRow drNew = dtAdd.Tables[0].Rows[0];
                    dr[strEditInfoKeyId] = drNew[strEditInfoKeyId];
                    if (dtAdd.Tables[0].Columns.Contains("UpdateFields"))
                    {
                        StaticFunctions.UpdateDataRowSyn(dr, drNew, drNew["UpdateFields"].ToString());
                    }
                    if (dtAdd.Tables.Count > 1 && dtAdd.Tables[1].Columns.Contains("UpdateFieldsOrd"))
                    {
                        DataRow drOrdUpdate = dtAdd.Tables[1].Rows[0];
                        StaticFunctions.UpdateDataRowSyn(drOrd, drOrdUpdate, drOrdUpdate["UpdateFieldsOrd"].ToString());
                    }
                }
                else
                {
                    DataTable dtInfo = (GcEdit.GridViewEdit.GridControl.DataSource as DataView).Table;
                    strValues = StaticFunctions.GetUpdateValues(dtInfo, dr, GcEdit.StrFileds);
                    if (strValues != string.Empty)
                    {
                        DataSet dsAdd = null;
                        string strSpParmName = string.Empty;
                        List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(GcEdit.CtrParentControl, dtShow, out strSpParmName);
                        if (strSpParmName != string.Empty)
                            strSpParmName += ",";

                        DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + drInfoEdit["EditFlag"].ToString() + "'");
                        if (drShares.Length == 0)
                        {
                            string[] strKey = (strSpParmName + "Ord_Id,strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                            lisSpParmValue.AddRange(new string[] {dr[strKeyFiled].ToString(), 
                                 strValues,
                                 dr[strEditInfoKeyId].ToString(),
                                 CApplication.App.CurrentSession.UserId.ToString(),
                                 CApplication.App.CurrentSession.DeptId.ToString(),
                                 CApplication.App.CurrentSession.FyId.ToString(),
                                 drInfoEdit["EditFlag"].ToString()});
                            dsAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
                        }
                        else
                        {
                            string[] strKey = (strSpParmName + "BsuSetSp_Id,Ord_Id,strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id").Split(",".ToCharArray());
                            lisSpParmValue.AddRange(new string[] {drShares[0]["BsuSetSp_Id"].ToString(),dr[strKeyFiled].ToString(),
                                 strValues,
                                 dr[strEditInfoKeyId].ToString(),
                                 CApplication.App.CurrentSession.UserId.ToString(),
                                 CApplication.App.CurrentSession.DeptId.ToString(),
                                 CApplication.App.CurrentSession.FyId.ToString()});
                            dsAdd = this.DataRequest_By_DataSet(strShareSpName, strKey, lisSpParmValue.ToArray());
                        }
                        if (dsAdd == null)
                        {
                            return;
                        }
                        if (dsAdd.Tables.Count > 0 && dsAdd.Tables[0].Columns.Contains("UpdateFields"))
                        {
                            DataRow drNew = dsAdd.Tables[0].Rows[0];
                            StaticFunctions.UpdateDataRowSyn(dr, drNew, drNew["UpdateFields"].ToString());
                        }
                        if (dsAdd.Tables.Count > 1 && dsAdd.Tables[1].Columns.Contains("UpdateFieldsOrd"))
                        {
                            DataRow drOrdUpdate = dsAdd.Tables[1].Rows[0];
                            StaticFunctions.UpdateDataRowSyn(drOrd, drOrdUpdate, drOrdUpdate["UpdateFieldsOrd"].ToString());
                        }
                    }
                }
                dr.AcceptChanges();
                if (blReAddInfo)
                {
                    DoSaveSynBtnEvents(drBtn, blAdd);
                    DoAddInfo(GcEdit, CopyFields, drOrd, CopyFieldsOrd, false);
                }
                else
                {
                    SetBtnItemByMode(strSaveCancelGvName, "VIEW");
                    SetGroupCtrol(GcEdit, "VIEW");
                    DoSaveSynBtnEvents(drBtn, blAdd);
                }
            }
            catch (Exception ERR)
            {
                MessageBox.Show("保存出错:" + ERR.Message);
                return;
            }
        }
        private void DoSaveSynBtnEvents(DataRow drBtn,bool blAdd)
        {
            string strField = (blAdd ? "AddSynBtnEvents" : "EditSynBtnEvents");
            string strSaveSynBtnEvents = drBtn[strField].ToString();
            if (strSaveSynBtnEvents == string.Empty)
                return;

            string[] strBtns = strSaveSynBtnEvents.Split(",".ToCharArray());
            foreach (string strBtn in strBtns)
            {
                DoBtnSpecial(strBtn);
            }

        }
        public override bool DeleteFocusedItem()
        {
            DataRow drOrd = gridVMain.GetFocusedDataRow();
            if (drOrd == null || drOrd[strKeyFiled].ToString() == string.Empty)
            {
                return false;
            }
            XtraTabPage tab = xtabItemInfo.SelectedTabPage;
            if (tab == null)
                return false;

            DataRow[] drs = dtTabs.Select("IsCanDelete=1 AND TabName='" + tab.Name + "'");
            if (drs.Length <= 0)
            {
                return false;
            }
            DataRow drInfoEdit = drs[0];

            string strChkRightName = drInfoEdit["DeleteChkRightName"].ToString();
            if (strChkRightName != string.Empty)
            {
                if ((";" + strAllowList + ";").IndexOf(";" + strChkRightName + "=") == -1)
                {
                    MessageBox.Show("你没有删除的权限.");
                    return false;
                }
                DataRow[] drSte = dtSte.Select("OrdState='" + drOrd[strOrdState].ToString() + "'");
                if (drSte.Length > 0 &&
                    ("," + drSte[0]["BtnEnableId"].ToString() + ",").IndexOf("," + strChkRightName + ",") == -1)
                {
                    MessageBox.Show("单据当前状态下不能删除.");
                    return false;
                }
            }
            GridView gvDel = gcItems[drInfoEdit["GridViewName"].ToString()];
            DataRow dr = gvDel.GetFocusedDataRow();
            if (dr == null)
                return false;

            string strMsg = drInfoEdit["DeleteMsg"].ToString();
            string strDelId = drInfoEdit["EditInfoKeyId"].ToString();
            string strDelFlag = drInfoEdit["DeleteFlag"].ToString();

            if (strDelId == string.Empty)
                return false;
            if (MessageBox.Show(strMsg, "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return false;

            DataTable dtAdd = null;
            DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + strDelFlag + "'");
            if (drShares.Length == 0)
            {
                string[] strKey = "Ord_Id,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                dtAdd = this.DataRequest_By_DataTable(strSpName,
                   strKey, new string[] {dr[strKeyFiled].ToString(),
                         dr[strDelId].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         strDelFlag});
            }
            else
            {
                string[] strKey = "BsuSetSp_Id,Ord_Id,Key_Id,EUser_Id,EDept_Id,Fy_Id".Split(",".ToCharArray());
                dtAdd = this.DataRequest_By_DataTable(strShareSpName,
                   strKey, new string[] {drShares[0]["BsuSetSp_Id"].ToString(),dr[strKeyFiled].ToString(),
                         dr[strDelId].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString()});
            }
            if (dtAdd == null)
            {
                return false;
            }
            if (dtAdd.Rows.Count > 0)
            {
                DataRow drUpdate = dtAdd.Rows[0];
                if (dtAdd.Columns.Contains("UpdateFieldsOrd"))
                {
                    StaticFunctions.UpdateDataRowSyn(drOrd, drUpdate, drUpdate["UpdateFieldsOrd"].ToString());
                }
                if (dtAdd.Columns.Contains("NeedRefresh") && drUpdate["NeedRefresh"].ToString() == "1")
                {
                    gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVMain.FocusedRowHandle));
                    return true;
                }
            }
            blInitBound = true;
            dr.Delete();
            dr.Table.AcceptChanges();
            blInitBound = false;
            if (GvNeedGCEdit.Contains(gvDel))
                gridVInfo_FocusedRowChanged(gvDel, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gvDel.FocusedRowHandle));

            return true;
        }
        private void DoDeletes(DataRow drBtn)
        {
            DataRow drOrd = gridVMain.GetFocusedDataRow();
            if (drOrd == null || drOrd[strKeyFiled].ToString() == string.Empty)
            {
                return;
            }
            XtraTabPage tab = xtabItemInfo.SelectedTabPage;
            if (tab == null)
                return;

            DataRow[] drs = dtTabs.Select("IsCanDelete=1 AND TabName='" + tab.Name + "'");
            if (drs.Length <= 0)
            {
                return;
            }
            DataRow drInfoEdit = drs[0];
            string strMsg = drInfoEdit["DeleteMsg"].ToString();
            string strDelId = drInfoEdit["EditInfoKeyId"].ToString();
            string strDelFlag = drBtn["BatchDelBtnFlag"].ToString();
            if (MessageBox.Show(strMsg, "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return;

            GridView gvDel = gcItems[drInfoEdit["GridViewName"].ToString()];
            string strDelIds = string.Empty;
            foreach (int i in gvDel.GetSelectedRows())
            {
                DataRow drFoc = gvDel.GetDataRow(i);
                strDelIds += strDelIds == string.Empty ? drFoc[strDelId].ToString() : "," + drFoc[strDelId].ToString();
            }
            if (strDelIds == string.Empty)
                return;
            
            DataTable dtAdd = null;
            DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + strDelFlag + "'");
            if (drShares.Length == 0)
            {
                string[] strKey = "Ord_Id,Key_Ids,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                dtAdd = this.DataRequest_By_DataTable(strSpName,
                   strKey, new string[] {drOrd[strKeyFiled].ToString(),
                         strDelIds,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         strDelFlag});
            }
            else
            {
                string[] strKey = "BsuSetSp_Id,Ord_Id,Key_Ids,EUser_Id,EDept_Id,Fy_Id".Split(",".ToCharArray());
                dtAdd = this.DataRequest_By_DataTable(strShareSpName,
                   strKey, new string[] {drShares[0]["BsuSetSp_Id"].ToString(),drOrd[strKeyFiled].ToString(),
                         strDelIds,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString()});
            }
            if (dtAdd == null)
            {
                return;
            }
            if (dtAdd.Rows.Count > 0)
            {
                DataRow drUpdate = dtAdd.Rows[0];
                if (dtAdd.Columns.Contains("UpdateFieldsOrd"))
                {
                    StaticFunctions.UpdateDataRowSyn(drOrd, drUpdate, drUpdate["UpdateFieldsOrd"].ToString());
                }
                if (dtAdd.Columns.Contains("NeedRefresh") && drUpdate["NeedRefresh"].ToString() == "1")
                {
                    gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVMain.FocusedRowHandle));
                    return;
                }
            }
            blInitBound = true;
            gvDel.DeleteSelectedRows();
            blInitBound = false;
            if (GvNeedGCEdit.Contains(gvDel))
                gridVInfo_FocusedRowChanged(gvDel, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gvDel.FocusedRowHandle));
        }
        private void DoPrintTicketInfoSpecial(DataRow drBtn)
        {
            string strSaveCancelGvName = drBtn["TicketGvName"].ToString();
            if (strSaveCancelGvName == "gridVMain")
            {
                DataRow drOrd = gridVMain.GetFocusedDataRow();

                string strRptName = string.Empty;
                string strSpFlag = string.Empty;
                string strTitle = string.Empty;
                if (!StaticFunctions.GetBtnMRpt(dtBtnsM, drBtn, drOrd, strKeyFiled, this
                    , drBtn["TicketTempName"].ToString(), drBtn["TicketSpFlag"].ToString(), ""
                    , out strRptName, out strSpFlag, out strTitle))
                {
                    return;
                }

                List<string> lisSpParmValue = new List<string>();
                string[] strKeyP = "Key_Id,Key_Ids,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                lisSpParmValue.AddRange(new string[] {drOrd[strKeyFiled].ToString(), 
                                     drOrd[strKeyFiled].ToString(),
                                     CApplication.App.CurrentSession.UserId.ToString(),
                                     CApplication.App.CurrentSession.DeptId.ToString(),
                                     CApplication.App.CurrentSession.FyId.ToString(),
                                     strSpFlag});
                DataTable dtAdd = this.DataRequest_By_DataTable(strSpName, strKeyP, lisSpParmValue.ToArray());
                if (dtAdd == null)
                {
                    return;
                }
                DataRow drPrint = dtAdd.Rows[0];
                try
                {
                    BarTender.Application btdb = new BarTender.Application();
                    string strPrintPath = Application.StartupPath + @"\打印模板\" + strRptName + ".btw";
                    try
                    {
                        StaticFunctions.PrintItem(btdb, drPrint, strPrintPath, strKeyFiled, frmImageFilePath);
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
                return;
            }
            else
            {
                UkyndaGcEdit GcEdit = null;
                foreach (string strKey in UkyndaGcItems.Keys)
                {
                    if (UkyndaGcItems[strKey].GridViewName == strSaveCancelGvName)
                    {
                        GcEdit = UkyndaGcItems[strKey];
                        break;
                    }
                }
                if (GcEdit == null)
                    return;

                DataRow drInfoEdit = null;
                DataRow[] drs = dtTabs.Select("GridViewName='" + strSaveCancelGvName + "'");
                if (drs.Length > 0)
                {
                    drInfoEdit = drs[0];
                }
                if (drInfoEdit == null)
                    return;

                string strIds = string.Empty;
                string strEditInfoKeyId = drInfoEdit["EditInfoKeyId"].ToString();
                List<string> lisIds = new List<string>();
                if (drBtn["IsPrintTicketAll"].ToString() == "True")
                {
                    for (int i = 0; i < GcEdit.GridViewEdit.RowCount; i++)
                    {
                        DataRow dr = GcEdit.GridViewEdit.GetDataRow(i);
                        lisIds.Add(dr[strEditInfoKeyId].ToString());
                        strIds += strIds == string.Empty ? dr[strEditInfoKeyId].ToString() : "," + dr[strEditInfoKeyId].ToString();
                    }
                }
                else
                {
                    if (GcEdit.GridViewEdit.SelectedRowsCount <= 0)
                    {
                        MessageBox.Show("请选择要打印的明细记录.");
                        return;
                    }
                    foreach (int i in GcEdit.GridViewEdit.GetSelectedRows())
                    {
                        DataRow dr = GcEdit.GridViewEdit.GetDataRow(i);
                        lisIds.Add(dr[strEditInfoKeyId].ToString());
                        strIds += strIds == string.Empty ? dr[strEditInfoKeyId].ToString() : "," + dr[strEditInfoKeyId].ToString();
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
                            DataRow[] drPrints = dtAdd.Select(strEditInfoKeyId + "=" + strId);
                            {
                                if (drPrints.Length > 0)
                                {
                                    StaticFunctions.PrintItem(btdb, drPrints[0], strPrintPath, strEditInfoKeyId, frmImageFilePath);
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
        }

        private void DoExcel(DataRow drBtn)
        {
            string strSaveCancelGvName = drBtn["ExcelGvName"].ToString();
            UkyndaGcEdit GcEdit = null;
            foreach (string strKey in UkyndaGcItems.Keys)
            {
                if (UkyndaGcItems[strKey].GridViewName == strSaveCancelGvName)
                {
                    GcEdit = UkyndaGcItems[strKey];
                    break;
                }
            }
            if (GcEdit == null)
                return;

            if (GcEdit.GridViewEdit.RowCount == 0)
                return;

            this.Cursor = Cursors.WaitCursor;
            StaticFunctions.GridViewExportToExcel(GcEdit.GridViewEdit, drBtn["ExcelTitle"].ToString(), null);
            this.Cursor = Cursors.Arrow;
        }

        private void DoEditBatch(DataRow drBtn)
        {
            string strGvInfoName = drBtn["BatchGvInfoName"].ToString();
            if (strGvInfoName == string.Empty)
                return;

            GridView gvInfo = gcItems[strGvInfoName];
            if (gvInfo.SelectedRowsCount <= 0)
            {
                MessageBox.Show("请先选择要批量修改的记录.");
                return;
            }
            string strKeyFiled = drBtn["BatchUpdateKeyId"].ToString();
            List<DataRow> lisRows = new List<DataRow>();
            string strKeyIds = string.Empty;
            foreach (int i in gvInfo.GetSelectedRows())
            {
                DataRow dr = gvInfo.GetDataRow(i);
                strKeyIds += strKeyIds == string.Empty ? dr[strKeyFiled].ToString() : "," + dr[strKeyFiled].ToString();
                lisRows.Add(dr);
            }
            if (strKeyIds == string.Empty)
                return;

            DataTable dtInfo = (gvInfo.GridControl.DataSource as DataView).Table;
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
        private void DoImportInfoSpecial(DataRow drBtn)
        {
            DataRow drOrd = gridVMain.GetFocusedDataRow();
            if (drOrd == null || drOrd[strKeyFiled].ToString() == string.Empty)
            {
                MessageBox.Show("请先保存单据表头.");
                return;
            }
            Dictionary<string, DataTable> dtChg = null;
            if (drBtn["ImportChkKeys"].ToString() != string.Empty)
            {
                dtChg = new Dictionary<string, DataTable>();
                string[] strFieldVs = drBtn["ImportChkKeys"].ToString().Split(",".ToCharArray());
                foreach (string strFieldV in strFieldVs)
                {
                    dtChg.Add(strFieldV, dsFormAdt.Tables[strFieldV]);
                }
            }
            //DataTable dtExcel = dsFormAdt.Tables[drBtn["ImportDsName"].ToString()];
            string strFilterGroup = "GroupName='" + drBtn["FilterGroupName"].ToString() + "'";
            dtExcel.DefaultView.RowFilter = strFilterGroup;
            DataTable dtEx = ExeclImportHelper.getInstance().GetProdDataTable_Form_ImportExcel(dtExcel.DefaultView, dtChg);
            if (dtEx == null)
                return;

            frmExcelShow frm = new frmExcelShow(dtExcel.DefaultView);
            frm.frmDataTable = dtEx;
            frm.DrBtn = drBtn;
            frm.StrOrdKeyId = drOrd[strKeyFiled].ToString();
            frm.StrUpdSpName = strSpName;
            frm.DtExcel = dtExcel;
            if (frm.ShowDialog(this) != DialogResult.Yes)
                return;

            DataSet dsAdd = frm.DsRets;
            if (dsAdd.Tables.Contains("gridVMain"))
            {
                DataRow drNew = dsAdd.Tables["gridVMain"].Rows[0];
                StaticFunctions.UpdateDataRowSyn(drOrd, drNew, drNew["UpdateFieldsOrd"].ToString());
            }
            blInitBound = true;
            StaticFunctions.UpdateGridControlDataSouce(dsAdd, drBtn["ImportUpdateGvName"].ToString(), gcItems, dtTabs);
            StaticFunctions.ReBoundSpicalContr(dtContr, dsAdd, strReBindCtrlIds, dtShow, dicCtrlS);
            blInitBound = false;
            SetUpdateGvFocusedRowChanged(drBtn["ImportUpdateGvName"].ToString());
        }
        private void DoSplitInfo(DataRow drBtn)
        {
            DataRow drOrd = gridVMain.GetFocusedDataRow();
            if (drOrd == null)
                return;

            GridView gvSplit = gcItems[drBtn["SplitGvName"].ToString()];
            DataRow drInfo = gvSplit.GetFocusedDataRow();
            if (drInfo == null)
                return;

            frmSysBusInfoSplit frm = new frmSysBusInfoSplit(drBtn);
            frm.DrSplit = drInfo;
            frm.StrOrdKeyId = drOrd[strKeyFiled].ToString();
            frm.StrUpdSpName = strSpName;
            if (frm.ShowDialog(this) != DialogResult.Yes)
                return;

            DataSet dsAdd = frm.DsRets;
            if (dsAdd.Tables.Contains("gridVMain"))
            {
                DataRow drNew = dsAdd.Tables["gridVMain"].Rows[0];
                StaticFunctions.UpdateDataRowSyn(drOrd, drNew, drNew["UpdateFieldsOrd"].ToString());
            }
            blInitBound = true;
            StaticFunctions.UpdateGridControlDataSouce(dsAdd, drBtn["SplitUpdateGvName"].ToString(), gcItems, dtTabs);
            StaticFunctions.ReBoundSpicalContr(dtContr, dsAdd, strReBindCtrlIds, dtShow, dicCtrlS);
            blInitBound = false;
            SetUpdateGvFocusedRowChanged(drBtn["SplitUpdateGvName"].ToString());
        }
        private void DoAddFromOrd(DataRow drBtn)
        {
            DataRow drOrd = gridVMain.GetFocusedDataRow();
            if (drOrd == null || drOrd[strKeyFiled].ToString() == string.Empty)
            {
                MessageBox.Show("请先保存单据表头.");
                return;
            }

            string strFrmInfoClass = drBtn["FrmInfoClass"].ToString();
            if (strFrmInfoClass == "frmSysInfoAdd")
            {
                frmSysInfoAdd frm = new frmSysInfoAdd(drBtn);
                frm.StrOrdKeyId = drOrd[strKeyFiled].ToString();
                frm.StrUpdSpName = strSpName;
                if (frm.ShowDialog() != DialogResult.Yes)
                {
                    CApplication.CurrForm = this;
                    CApplication.Com_Prot.OnPortDataReceived += SetText;
                    return;
                }

                CApplication.CurrForm = this;
                CApplication.Com_Prot.OnPortDataReceived += SetText;
                DataSet dsAdd = frm.DsRets;
                if (dsAdd.Tables.Contains("gridVMain"))
                {
                    DataRow drNew = dsAdd.Tables["gridVMain"].Rows[0];
                    StaticFunctions.UpdateDataRowSyn(drOrd, drNew, drNew["UpdateFieldsOrd"].ToString());
                }
                blInitBound = true;
                StaticFunctions.UpdateGridControlDataSouce(dsAdd, drBtn["FrmUpdateGvName"].ToString(), gcItems, dtTabs);
                StaticFunctions.ReBoundSpicalContr(dtContr, dsAdd, strReBindCtrlIds, dtShow, dicCtrlS);
                blInitBound = false;
                SetUpdateGvFocusedRowChanged(drBtn["FrmUpdateGvName"].ToString());
            }
            else if (strFrmInfoClass == "frmSysInfoAddItem")
            {
                frmSysInfoAddItem frm = new frmSysInfoAddItem(drBtn);
                frm.StrOrdKeyId = drOrd[strKeyFiled].ToString();
                frm.StrUpdSpName = strSpName;
                if (frm.ShowDialog() != DialogResult.Yes)
                {
                    CApplication.CurrForm = this;
                    CApplication.Com_Prot.OnPortDataReceived += SetText;
                    return;
                }

                CApplication.CurrForm = this;
                CApplication.Com_Prot.OnPortDataReceived += SetText;
                DataSet dsAdd = frm.DsRets;
                if (dsAdd.Tables.Contains("gridVMain"))
                {
                    DataRow drNew = dsAdd.Tables["gridVMain"].Rows[0];
                    StaticFunctions.UpdateDataRowSyn(drOrd, drNew, drNew["UpdateFieldsOrd"].ToString());
                }
                blInitBound = true;
                StaticFunctions.UpdateGridControlDataSouce(dsAdd, drBtn["FrmUpdateGvName"].ToString(), gcItems, dtTabs);
                StaticFunctions.ReBoundSpicalContr(dtContr, dsAdd, strReBindCtrlIds, dtShow, dicCtrlS);
                blInitBound = false;
                SetUpdateGvFocusedRowChanged(drBtn["FrmUpdateGvName"].ToString());
            }
            else if (strFrmInfoClass == "frmSysInfoAddMore")
            {
                Form frmEx = StaticFunctions.GetExistedChildForm(this.ParentForm, "frmSysInfoAddMore");
                if (frmEx != null)
                {
                    frmEx.Close();
                    frmEx.Dispose();
                }

                frmSysInfoAddMore frm = new frmSysInfoAddMore(drBtn, drOrd, strSpName, strKeyFiled, this.Name, strBusClassName);
                frm.MdiParent = this.ParentForm;
                frm.Show();
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
            if (k == 9)//Tab
            {
                if (xtabItemInfo.TabPages.Count <= 1)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else
                {
                    if (xtabItemInfo.SelectedTabPageIndex == xtabItemInfo.TabPages.Count - 1)
                    {
                        xtabItemInfo.SelectedTabPageIndex = 0;
                    }
                    else
                    {
                        xtabItemInfo.SelectedTabPageIndex++;
                    }
                    return true;
                }
            }
            else if (k == 70) //Ctrl+F
            {
                if (keyData.ToString().ToUpper().IndexOf("CONTROL") != -1)
                {
                    txtOrdFilter.Focus();
                    txtOrdFilter.SelectAll();
                    return true;
                }
            }
            else if (k == 32) //空格
            {
                DevExpress.XtraEditors.TextEdit txt = FocusedControl as DevExpress.XtraEditors.TextEdit;
                if (txt == null)
                    return true;
                if (txt.Properties.ReadOnly)
                    return true;
                DataRow dr = GridViewEdit.GetFocusedDataRow();
                if (dr == null)
                    return true;

                DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, txt.Name, txt.Parent.Name);
                if (drContrl == null)
                    return true;
                if (drContrl["IsSpaceQuery"].ToString() == "True")
                {
                    frmSelect frm = new frmSelect();
                    frm.SelectFlag = drContrl["SelectFlag"].ToString();
                    frm.StrParas = drContrl["SelectParas"].ToString();
                    if (drContrl["SelectParasFieldIds"].ToString() != string.Empty)
                    {
                        List<string> arrValue = new List<string>();
                        string[] strFieldVs = drContrl["SelectParasFieldIds"].ToString().Split(",".ToCharArray());
                        foreach (string strFieldV in strFieldVs)
                        {
                            arrValue.Add(dr[strFieldV].ToString());
                        }
                        frm.StrParaVals = arrValue.ToArray();
                    }
                    if (frm.ShowDialog(this) == DialogResult.OK)
                    {
                        DataRow drSel = frm.SelectRow;
                        StaticFunctions.UpdateDataRowSyn(dr, drSel, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString());
                    }
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
                DataRow dr = GridViewEdit.GetFocusedDataRow();
                if (dr == null)
                    return base.ProcessCmdKey(ref msg, keyData);

                DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, txt.Name, txt.Parent.Name);
                if (drContrl == null)
                    return base.ProcessCmdKey(ref msg, keyData);
                if (drContrl["IsEnterQuery"].ToString() == "True")
                {
                    string StrParas = drContrl["SelectParas"].ToString();
                    string strKeys = string.IsNullOrEmpty(StrParas) ? string.Empty : StrParas + ",";
                    string[] strKey = (strKeys + "Number,Fy_Id,EDept_Id,EUser_Id,flag").Split(",".ToCharArray());

                    List<string> arrValue = new List<string>();
                    if (drContrl["SelectParasFieldIds"].ToString() != string.Empty)
                    {
                        string[] strFieldVs = drContrl["SelectParasFieldIds"].ToString().Split(",".ToCharArray());
                        foreach (string strFieldV in strFieldVs)
                        {
                            arrValue.Add(dr[strFieldV].ToString());
                        }
                    }
                    List<string> lisSpParmValue = new List<string>();
                    if (arrValue.Count > 0)
                        lisSpParmValue.AddRange(arrValue.ToArray());
                    lisSpParmValue.AddRange(new string[] {
                        txt.Text.Trim(),                  
                        CApplication.App.CurrentSession.FyId.ToString(),
                        CApplication.App.CurrentSession.DeptId.ToString(),
                        CApplication.App.CurrentSession.UserId.ToString(),
                        drContrl["SelectFlag"].ToString() });
                    DataTable dtTemp = this.DataRequest_By_DataTable("frmSelect", strKey, lisSpParmValue.ToArray());
                    if (dtTemp == null)
                    {
                        txt.SelectAll();
                        return true;
                    }
                    if (dtTemp.Rows.Count <= 0)
                    {
                        MessageBox.Show("没有检索到记录.");
                        txt.SelectAll();
                        return true;
                    }
                    DataRow drSel = dtTemp.Rows[0];
                    StaticFunctions.UpdateDataRowSyn(dr, drSel, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString());
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void txtOrdFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != 13)
                return;

            blInitBound = true;
            string strText = txtOrdFilter.Text.Trim();
            if (strText == string.Empty)
            {
                frmDataTable.DefaultView.RowFilter = "";
            }
            else
            {
                string strFilter = string.Empty;
                foreach (string strFiter in strFilterFields)
                {
                    string strType = frmDataTable.Columns[strFiter].DataType.ToString();
                    if (strType == "System.String")
                    {
                        strFilter += strFilter == string.Empty ? "(" + strFiter + " like '%" + strText + "%')" : " or (" + strFiter + " like '%" + strText + "%')";
                    }
                    else
                    {
                        double dValue = 0;
                        if (double.TryParse(strText, out dValue))
                        {
                            strFilter += strFilter == string.Empty ? "(" + strFiter + "=" + strText + ")" : " or (" + strFiter + "=" + strText + ")";
                        }
                    }
                }
                frmDataTable.DefaultView.RowFilter = strFilter;
            }
            blInitBound = false;
            gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVMain.FocusedRowHandle));
        }

        private void SetUpdateGvFocusedRowChanged(string strUpdateFields)
        {
            if (strUpdateFields.Trim() == string.Empty)
            {
                return;
            }
            string[] strFieldVs = strUpdateFields.Split(",".ToCharArray());
            foreach (string strFieldV in strFieldVs)
            {
                GridView gv = gcItems[strFieldV];
                if (GvNeedGCEdit.Contains(gv))
                    gridVInfo_FocusedRowChanged(gv, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gv.FocusedRowHandle));
            }
        }
        private void xtabOrdParent_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (blInitBound)
                return;

            if (xtabOrdParent.TabPages.Count == 0)
                return;

            Control gc = xtabOrdParent.SelectedTabPage.Controls[0];
            if (gc.GetType().ToString() != "System.Windows.Forms.GroupBox")
                return;

            foreach (string strKey in UkyndaGcItems.Keys)
            {
                UkyndaGcEdit GcItem = UkyndaGcItems[strKey];
                if (GcItem.GridViewName == "gridVMain" && strKey == gc.Name)
                {
                    if (GcItem.StrMode == "ADD")
                        StaticFunctions.SetFirstEditContrSelect(GcItem.CtrFirstAddFocusContr);
                    else if (GcItem.StrMode == "EDIT")
                        StaticFunctions.SetFirstEditContrSelect(GcItem.CtrFirstEditFocusContr);

                    break;
                }
            }
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
            else if (drBtn["IsUpdateOrdStatetn"].ToString() == "True")
            {
                DoUpdateOrdState(drBtn);
            }
            else if (drBtn["IsEditBtn"].ToString() == "True")
            {
                DoEditSpecial(drBtn);
            }
            else if (drBtn["IsAddInfoBtn"].ToString() == "True")
            {
                DoAddInfoSpecial(drBtn);
            }
            else if (drBtn["IsCopyAddInfoBtn"].ToString() == "True")
            {
                DoCopyAddInfoSpecial(drBtn);
            }
            else if (drBtn["IsCancelInfoBtn"].ToString() == "True")
            {
                DoCancelInfoSpecial(drBtn);
            }
            else if (drBtn["IsSaveInfoBtn"].ToString() == "True")
            {
                DoSaveInfoSpecial(drBtn);
            }
            else if (drBtn["IsImportFExcel"].ToString() == "True")
            {
                DoImportInfoSpecial(drBtn);
            }
            else if (drBtn["IsSplitBtn"].ToString() == "True")
            {
                DoSplitInfo(drBtn);
            }
            else if (drBtn["IsPrintTicket"].ToString() == "True")
            {
                DoPrintTicketInfoSpecial(drBtn);
            }
            else if (drBtn["IsAddInfoFrmBtn"].ToString() == "True")
            {
                DoAddFromOrd(drBtn);
            }
            else if (drBtn["IsBatchDelBtn"].ToString() == "True")
            {
                DoDeletes(drBtn);
            }
            else if (drBtn["IsFormLink"].ToString() == "True")
            {
                StaticFunctions.DoOpenLinkForm(drBtn, this.ParentForm, null);
            }
            else if (drBtn["IsExcel"].ToString() == "True")
            {
                DoExcel(drBtn);
            }
            else
            {
                DoMyBtn(strBtnName);
            }
        }
        private void DoMyBtn(string strBtnName)
        {
            switch (strBtnName)
            {
                case "btnAddMore":
                    if (strBusClassName.ToUpper() == "frmTst_ProductJKOrdEdit".ToUpper())
                    {
                    }
                    break;
                case "btnDelFromOrd":
                    if (strBusClassName.ToUpper() == "frmTst_ProductJKOrdEdit".ToUpper())
                    {
                    }
                    break;
                default:
                    break;
            }
        }
        private bool DoCheckSaveOrd()
        {
            if (strBusClassName.ToUpper() == "frmSys_KpEdit".ToUpper())
            {
            }
            return true;
        }
        private bool DoCheckSaveInfo()
        {
            if (strBusClassName.ToUpper() == "frmSys_KpEdit".ToUpper())
            {
            }
            return true;
        }
        #endregion

    }
}