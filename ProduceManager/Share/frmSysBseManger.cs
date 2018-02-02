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
    public partial class frmSysBseManger : frmEditorBase
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
        private bool blSysProcess = false;

        private DataSet dsFormAdt = null;
        private DataSet dsFormUkyndaAdt = null;
        private DataRow drMain = null;

        private string strShareSpName = "Share_Table_Op";
        private string strSpName = string.Empty;
        private string strBusClassName = string.Empty;
        private string strNoShowDefaultBarItems = string.Empty;
        private string strAllowList = string.Empty;
        private string strOrdState = string.Empty;
        private string strEnterGc = string.Empty;

        private Dictionary<string, GridView> gcItems = new Dictionary<string, GridView>();//所有子表的GridView集合，不包括gridVMain，key为GridViewName
        private List<RepositoryItemImageEdit> lisrepImg = new List<RepositoryItemImageEdit>();//所有子表GridView中要显示图片列的EditType
        private Dictionary<string, List<SimpleButton>> lisBtns = new Dictionary<string, List<SimpleButton>>();//以编辑Tab对应的GridViewName为Key，保存所属的Btn
        private List<SimpleButton> lisBtnAlls = new List<SimpleButton>();//页面中所有的SimpleButton
        private List<GridView> GvNeedGCEdit = new List<GridView>();//所有需要编辑区的GridView，定制gridVInfo_FocusedRowChanged
        private Dictionary<string, UkyndaGcEdit> UkyndaGcItems = new Dictionary<string, UkyndaGcEdit>();//以编辑面板GroupBoxName为Key，保存UkyndaGcEdit
        private Dictionary<string, Control> lisGcQuerys = new Dictionary<string, Control>();//明细查询面板

        private bool blReAddInfo = false;
        private string CopyFields = string.Empty;
        private bool blSetWeight = false;
        private string strGetWFBalceCtrlIds = string.Empty;
        #endregion

        public frmSysBseManger()
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
                DoControlEvent(dpl, Convert.ToString(dpl.EditValue));
                SetContrEditFromDpl(UkyndaGcItems[strEnterGc], dpl.Name);
                if (!blPrevFindControl)
                {
                    SetContrMoveNext(dpl.Name, false);
                }
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
        private void DoControlEvent(Control ctrl, string strValue)
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
        private void SetContrEditFromDpl(UkyndaGcEdit gc, string strEnterName)
        {
            if (gc.StrMode == "VIEW")
                return;

            string strFilter = "IsContrEditSet=1 AND BtnParent='" + gc.CtrParentControl.Name + "'";
            if (strEnterName != string.Empty)
                strFilter += " AND BtnName='" + strEnterName + "'";
            DataRow[] drContrEditSets = dtBtnsM.Select(strFilter);
            if (drContrEditSets.Length <= 0)
                return;

            DataRow drInfo = gc.GridViewEdit.GetFocusedDataRow();

            foreach (DataRow dr in drContrEditSets)
            {
                if (!StaticFunctions.CheckKeyFields(dr["OrdKeyFields"].ToString(), null, drInfo))
                    continue;

                string[] strSets = dr["OrdKeyValues"].ToString().Split("|".ToCharArray());
                StaticFunctions.SetControlEdit(strSets[0], true, gc.CtrParentControl);
                StaticFunctions.SetControlEdit(strSets[1], false, gc.CtrParentControl);
            }
        }

        private void frmSysBseManger_Load(object sender, EventArgs e)
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

        private void TxtQ_Enter(object sender, EventArgs e)
        {
            strFocusedContrName = (sender as Control).Name;
            FocusedControl = sender as Control;
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
                    ucp.DrFilterFieldsInfo = gc.GridViewEdit.GetFocusedDataRow();
                }
                else if (FocusedControl.GetType().ToString() == "ProduceManager.UcTreeList")
                {
                    ProduceManager.UcTreeList ucp = FocusedControl as ProduceManager.UcTreeList;
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
            strNoShowDefaultBarItems = drMain["NoShowDefaultBarItems"].ToString();
            strGetWFBalceCtrlIds = drMain["GetWFBalceCtrlIds"].ToString();

            dtBtns = dsLoad.Tables[3];
            dtTabs = dsLoad.Tables[4];
            dtGroupC = dsLoad.Tables[5];
            dtSte = dsLoad.Tables[6];
            dtContr = dsLoad.Tables[7];
            dtBtnsM = dsLoad.Tables[8];
            dtSp = dsLoad.Tables[9];

            Rectangle rect = SystemInformation.VirtualScreen;
            List<BarButtonItem> lisBarItems = StaticFunctions.ShowBarButtonItem(dtBtns, bar2, "bar2", strAllowList, imageList1);
            foreach (BarButtonItem item in lisBarItems)
            {
                item.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            }
            gcItems = StaticFunctions.ShowTabItem(dtTabs, dtBtns, xtabItemInfo, "xtabItemInfo", strAllowList, lisrepImg, GvNeedGCEdit, lisBtnAlls, lisBtns, imageList1, true, lisGcQuerys);
            foreach (string strGv in gcItems.Keys)
            {
                StaticFunctions.ShowGridControl(gcItems[strGv], dtShow, dtConst);
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
                            break;

                        default:
                            break;
                    }
                }
            }
            #endregion

            #region GroupC
            List<Control> lisGcContrs = new List<Control>();
            StaticFunctions.GetGroupCUkynda(dtTabs, dtGroupC, UkyndaGcItems, this, strAllowList, lisGcContrs, dtShow, dtConst, gcItems, null, bar2, rect.Width);
            foreach (Control ctrl in lisGcContrs)
            {
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

            foreach (SimpleButton btn in lisBtnAlls)
            {
                btn.Click += new System.EventHandler(this.btn_Click);
            }
            StaticFunctions.SetBtnStyle(barManager1, lisBtnAlls, drMain);

            foreach (string strGv in gcItems.Keys)
            {
                GridView gv = gcItems[strGv];
                if (!GvNeedGCEdit.Contains(gv))
                    continue;

                SetBtnItemByMode(gv.Name, "VIEW");
                foreach (string strKey in UkyndaGcItems.Keys)
                {
                    if (UkyndaGcItems[strKey].GridViewName == gv.Name)
                        SetGroupCtrol(UkyndaGcItems[strKey], "VIEW");
                }
            }
            if (xtabItemInfo.TabPages.Count == 1 && xtabItemInfo.TabPages[0].Text == string.Empty)
            {
                xtabItemInfo.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            }
            else
            {
                xtabItemInfo.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            }
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
                    case "btnExcel":
                        DoExcel();
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
            lisBtns.Clear();
            lisBtnAlls.Clear();
            GvNeedGCEdit.Clear();
            UkyndaGcItems.Clear();
            lisGcQuerys.Clear();
            arrContrSeq.Clear();
            xtabItemInfo.TabPages.Clear();
            dsLoad = null;
            InitContr();
            frmSysBseManger_Load(null, null);
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
        private void SetBtnItemByMode(string strGridViewName, string strMode)
        {
            string strBtnIdsOrdSte = string.Empty;
            if (strMode == "VIEW")
            {
                if (dtSte.Rows.Count > 0)
                {
                    strBtnIdsOrdSte = dtSte.Rows[0]["BtnEnableId"].ToString();
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

        private void Query()
        {
            string strTabSel = xtabItemInfo.SelectedTabPage.Name;
            DataRow[] drTabs = dtTabs.Select("TabName='" + strTabSel + "'");
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

            DataSet dtAdd = null;
            DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + drTab["QueryFlag"].ToString() + "'");
            if (drShares.Length == 0)
            {
                string[] strKey = (strSpParmName + "EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                lisSpParmValue.AddRange(new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     drTab["QueryFlag"].ToString()});
                dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            }
            else
            {
                string[] strKey = (strSpParmName + "BsuSetSp_Id,EUser_Id,EDept_Id,Fy_Id").Split(",".ToCharArray());
                lisSpParmValue.AddRange(new string[] {drShares[0]["BsuSetSp_Id"].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString()});
                dtAdd = this.DataRequest_By_DataSet(strShareSpName, strKey, lisSpParmValue.ToArray());
            }
            if (dtAdd == null)
                return;

            dtAdd.AcceptChanges();
            blInitBound = true;
            gv.GridControl.DataSource = dtAdd.Tables[0].DefaultView;//可能引发gridView1_FocusedRowChanged
            gv.BestFitColumns();
            blInitBound = false;
            gridVInfo_FocusedRowChanged(gv, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gv.FocusedRowHandle));
        }
        private void DoExcel()
        {
            string strTabSel = xtabItemInfo.SelectedTabPage.Name;
            DataRow[] drTabs = dtTabs.Select("TabName='" + strTabSel + "'");
            if (drTabs.Length <= 0)
                return;

            DataRow drTab = drTabs[0];
            GridView gv = gcItems[drTab["GridViewName"].ToString()];
            if (gv.RowCount == 0)
                return;

            this.Cursor = Cursors.WaitCursor;
            StaticFunctions.GridViewExportToExcel(gv, xtabItemInfo.SelectedTabPage.Text == string.Empty ? this.Text : xtabItemInfo.SelectedTabPage.Text, null);
            this.Cursor = Cursors.Arrow;
        }

        private void DoEditSpecial(DataRow drBtn)
        {
            UkyndaGcEdit GcEdit = UkyndaGcItems[drBtn["EditGroupCName"].ToString()];
            DataRow dr = GcEdit.GridViewEdit.GetFocusedDataRow();
            if (dr == null)
                return;

            SetBtnItemByMode(GcEdit.GridViewName, "EDIT");
            SetGroupCtrol(GcEdit, "EDIT");
            SetContrEditFromDpl(GcEdit, string.Empty);
            StaticFunctions.SetFirstEditContrSelect(GcEdit.CtrFirstEditFocusContr);

            blReAddInfo = false;
        }
        private void DoAddInfoSpecial(DataRow drBtn)
        {
            UkyndaGcEdit GcEdit = UkyndaGcItems[drBtn["EditGroupCName"].ToString()];
            if (GcEdit.GridViewEdit.GridControl.DataSource == null){
                Query();
            }
            GcEdit.GridViewEdit.ClearColumnsFilter();
            GcEdit.GridViewEdit.ClearSorting();

            blReAddInfo = (drBtn["IsReAddInfoBtn"].ToString() == "True");
            CopyFields = drBtn["CopyFields"].ToString();
            DoAddInfo(GcEdit, CopyFields, true);
        }
        private void DoAddInfo(UkyndaGcEdit GcEdit, string strCopyFields, bool blFirst)
        {
            DataTable dtInfo = (GcEdit.GridViewEdit.GridControl.DataSource as DataView).Table;
            DataRow drNew = dtInfo.NewRow();
            if (GcEdit.BlSetDefault)
            {
                StaticFunctions.SetContrDefaultValue(GcEdit.CtrParentControl, dtShow, drNew);
            }
            if (strCopyFields.Trim() != string.Empty)
            {
                DataRow drFoc = GcEdit.GridViewEdit.GetFocusedDataRow();
                if (drFoc != null)
                {
                    string[] arrFields = strCopyFields.Split(",".ToCharArray());
                    foreach (string strField in arrFields)
                    {
                        drNew[strField] = drFoc[strField];
                    }
                }
            }
            blInitBound = true;
            dtInfo.Rows.InsertAt(drNew, dtInfo.Rows.Count);//可能引发gridView1_FocusedRowChanged
            GcEdit.GridViewEdit.MoveLast();
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
            UkyndaGcEdit GcEdit = UkyndaGcItems[drBtn["EditGroupCName"].ToString()];
            DataRow[] drs = dtTabs.Select("IsNeedGCEdit=1 AND GridViewName='" + GcEdit.GridViewEdit.Name + "'");
            if (drs.Length <= 0)
            {
                return;
            }
            if (GcEdit.GridViewEdit.GridControl.DataSource == null)
            {
                Query();
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

            if (!DoCheckSaveInfo())
                return;

            string strEditInfoKeyId = drInfoEdit["EditInfoKeyId"].ToString();
            string strField = string.Empty;
            string strValues = string.Empty;
            try
            {
                if (dr[strEditInfoKeyId].ToString() == string.Empty)
                {
                    strValues = StaticFunctions.GetAddValues(dr, GcEdit.StrFileds, out strField);

                    DataSet dtAdd = null;
                    string strSpParmName = string.Empty;
                    List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(GcEdit.CtrParentControl, dtShow, out strSpParmName);
                    if (strSpParmName != string.Empty)
                        strSpParmName += ",";

                    DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + drInfoEdit["AddFlag"].ToString() + "'");
                    if (drShares.Length == 0)
                    {
                        string[] strKey = (strSpParmName + "strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                        lisSpParmValue.AddRange(new string[] {
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
                        return;
                    }
                    DataRow drNew = dtAdd.Tables[0].Rows[0];
                    dr[strEditInfoKeyId] = drNew[strEditInfoKeyId];
                    if (dtAdd.Tables[0].Columns.Contains("UpdateFields"))
                    {
                        StaticFunctions.UpdateDataRowSyn(dr, drNew, drNew["UpdateFields"].ToString());
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
                            string[] strKey = (strSpParmName + "strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                            lisSpParmValue.AddRange(new string[] {
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
                            string[] strKey = (strSpParmName + "BsuSetSp_Id,strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id").Split(",".ToCharArray());
                            lisSpParmValue.AddRange(new string[] {drShares[0]["BsuSetSp_Id"].ToString(),
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
                    }
                }
                dr.AcceptChanges();
                if (blReAddInfo)
                {
                    DoAddInfo(GcEdit, CopyFields, false);
                }
                else
                {
                    SetBtnItemByMode(strSaveCancelGvName, "VIEW");
                    SetGroupCtrol(GcEdit, "VIEW");
                }
            }
            catch (Exception ERR)
            {
                MessageBox.Show("保存出错:" + ERR.Message);
                return;
            }
        }
        public override bool DeleteFocusedItem()
        {
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
                string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                dtAdd = this.DataRequest_By_DataTable(strSpName,
                   strKey, new string[] {
                         dr[strDelId].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         strDelFlag});
            }
            else
            {
                string[] strKey = "BsuSetSp_Id,Key_Id,EUser_Id,EDept_Id,Fy_Id".Split(",".ToCharArray());
                dtAdd = this.DataRequest_By_DataTable(strShareSpName,
                   strKey, new string[] {drShares[0]["BsuSetSp_Id"].ToString(),
                         dr[strDelId].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString()});
            }
            if (dtAdd == null)
            {
                return false;
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
                string[] strKey = "Key_Ids,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                dtAdd = this.DataRequest_By_DataTable(strSpName,
                   strKey, new string[] {
                         strDelIds,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         strDelFlag});
            }
            else
            {
                string[] strKey = "BsuSetSp_Id,Key_Ids,EUser_Id,EDept_Id,Fy_Id".Split(",".ToCharArray());
                dtAdd = this.DataRequest_By_DataTable(strShareSpName,
                   strKey, new string[] {drShares[0]["BsuSetSp_Id"].ToString(),
                         strDelIds,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString()});
            }
            if (dtAdd == null)
            {
                return;
            }
            blInitBound = true;
            gvDel.DeleteSelectedRows();
            blInitBound = false;
            if (GvNeedGCEdit.Contains(gvDel))
                gridVInfo_FocusedRowChanged(gvDel, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gvDel.FocusedRowHandle));
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
                Control ctrlP = txt.Parent;
                if (ctrlP != null && lisGcQuerys.ContainsKey(ctrlP.Name))
                {
                    btn_ItemClick(barManager1, new ItemClickEventArgs(bar2.Manager.Items["btnQuery"], null));
                    return true;
                }
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
        public override void RefreshItem()
        {
            btn_ItemClick(barManager1, new ItemClickEventArgs(bar2.Manager.Items["btnQuery"], null));
        }

        #region 可能需要扩充的事件
        private void DoBtnSpecial(string strBtnName)
        {
            DataRow[] drBtns = dtBtns.Select("BtnName='" + strBtnName + "'");
            if (drBtns.Length <= 0)
                return;

            DataRow drBtn = drBtns[0];
            if (drBtn["IsBatchEditBtn"].ToString() == "True")
            {
                DoEditBatch(drBtn);
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
            else if (drBtn["IsBatchDelBtn"].ToString() == "True")
            {
                DoDeletes(drBtn);
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