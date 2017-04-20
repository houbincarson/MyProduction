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
    public partial class frmBasicEdit : frmEditorBase
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
        private List<string> lisOrdSpCtrlId = new List<string>();

        private string strShareSpName = "Share_Table_Op";
        private string strSpName = string.Empty;
        private string strQueryFlag = string.Empty;
        private string strGetInfoFlag = string.Empty;
        private string strAddFlag = string.Empty;
        private string strEditFlag = string.Empty;
        private string strDeleteFlag = string.Empty;
        private string strKeyFiled = string.Empty;
        private string strBusClassName = string.Empty;
        private string strAllowList = string.Empty;
        private string strOrdState = string.Empty;
        private string strEnterGc = string.Empty;
        private bool blShowPicEdit = false;
        private DataRow drPicEditTemp = null;

        private Dictionary<string, GridView> gcItems = new Dictionary<string, GridView>();//所有子表的GridView集合，不包括gridVMain，key为GridViewName
        private List<RepositoryItemImageEdit> lisrepImg = new List<RepositoryItemImageEdit>();//所有子表GridView中要显示图片列的EditType
        private Dictionary<string, List<SimpleButton>> lisBtns = new Dictionary<string, List<SimpleButton>>();//以编辑Tab对应的GridViewName为Key，保存所属的Btn
        private List<SimpleButton> lisBtnAlls = new List<SimpleButton>();//页面中明细区域所有的SimpleButton
        private List<GridView> GvNeedGCEdit = new List<GridView>();//所有需要编辑区的GridView，定制gridVInfo_FocusedRowChanged
        private Dictionary<string, UkyndaGcEdit> UkyndaGcItems = new Dictionary<string, UkyndaGcEdit>();//以编辑面板GroupBoxName为Key，保存UkyndaGcEdit

        private bool blReAddInfo = false;
        private string CopyFields = string.Empty;
        private string CopyFieldsOrd = string.Empty;
        private Dictionary<string, Control> dicCtrlS = new Dictionary<string, Control>();
        private string strReBindCtrlIds = string.Empty;
        Thread tdCreateBarcodeImage = null;
        #endregion

        public frmBasicEdit()
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
                frmDataTable.AcceptChanges();
            }
            BoundGridContr(strKeyId);
            if (Mode == "ADD")
            {
                btn_Click(btnAdd, null);
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
            dtBtns = dsLoad.Tables[3];
            dtTabs = dsLoad.Tables[4];
            dtGroupC = dsLoad.Tables[5];
            dtSte = dsLoad.Tables[6];
            dtContr = dsLoad.Tables[7];
            dtBtnsM = dsLoad.Tables[8];
            dtSp = dsLoad.Tables[9];

            strSpName = drMain["SpName"].ToString();
            strQueryFlag = drMain["QueryFlag"].ToString();
            strGetInfoFlag = drMain["GetInfoFlag"].ToString();
            strAddFlag = drMain["AddFlag"].ToString();
            strEditFlag = drMain["EditFlag"].ToString();
            strDeleteFlag = drMain["DeleteFlag"].ToString();
            strKeyFiled = drMain["KeyIdFiled"].ToString();
            strOrdState = drMain["OrdStateFiled"].ToString();
            strReBindCtrlIds = drMain["ReBindCtrlIds"].ToString().Trim();
            blShowPicEdit = drMain["IsShowPicEdit"].ToString() == "True";
            gcImgs.Visible = blShowPicEdit;

            xtabInfo.Text = drMain["MainTabTitle"].ToString();
            if (xtabInfo.Text == string.Empty)
                xtabItemInfo.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            splitContainerControl1.Horizontal = drMain["IsHorizontal"].ToString() == "True";
            StaticFunctions.ShowGridControl(gridVMain, dtShow, dtConst);
            splitContainerControl1.SplitterPosition = int.Parse(drMain["MainSplitterPosition"].ToString());

            Rectangle rect = SystemInformation.VirtualScreen;
            int iGcW = rect.Width - 30 - splitContainerControl1.SplitterPosition;

            gcItems = StaticFunctions.ShowTabItem(dtTabs, dtBtns, xtabItemInfo, "xtabItemInfo", strAllowList, lisrepImg, GvNeedGCEdit, lisBtnAlls, lisBtns, imageList2);
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
            StaticFunctions.ShowGcContrsToUkynda(gcOrd, gridVMain, btnSave, UkyndaGcItems, lisGcContrs, dtShow, dtConst, iGcW);
            StaticFunctions.GetGroupCUkynda(dtTabs, dtGroupC, UkyndaGcItems, this, strAllowList, lisGcContrs, dtShow, dtConst, gcItems, gridVMain, null, rect.Width - splitContainerControl1.SplitterPosition);
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
                        if (ucp.Parent != null && ucp.Parent.Name == "gcOrd")
                        {
                            if (ucp.Tag != null && ucp.Tag.ToString() != string.Empty)
                                lisOrdSpCtrlId.Add(ucp.Tag.ToString());
                        }
                        break;
                    case "ProduceManager.UcTreeList":
                        ProduceManager.UcTreeList uct = ctrl as ProduceManager.UcTreeList;
                        uct.onClosePopUp += new UcTreeList.ClosePopUp(uct_onClosePopUp);
                        StaticFunctions.BoundSpicalContr(dtContr, dsFormAdt, dsFormUkyndaAdt, uct, dtShow);
                        if (uct.Parent != null && uct.Parent.Name == "gcOrd")
                        {
                            if (uct.Tag != null && uct.Tag.ToString() != string.Empty)
                                lisOrdSpCtrlId.Add(uct.Tag.ToString());
                        }
                        break;
                    case "ExtendControl.ExtPopupTree":
                        ExtendControl.ExtPopupTree ept = ctrl as ExtendControl.ExtPopupTree;
                        ept.Properties.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.lookUpEdit_Properties_Closed);
                        StaticFunctions.BoundSpicalContr(dtContr, dsFormAdt, dsFormUkyndaAdt, ept, dtShow);
                        if (ept.Parent != null && ept.Parent.Name == "gcOrd")
                        {
                            if (ept.Tag != null && ept.Tag.ToString() != string.Empty)
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
            lisBtnAlls.AddRange(new SimpleButton[] { btnCopy, btnAdd, btnEdit, btnSave, btnCancel, btnDelete, btnReLoad });
            StaticFunctions.SetBtnStyle(null, lisBtnAlls, drMain);
        }
        private void SetControlEvent(Control ctrl)
        {
            TextEdit txt = ctrl as TextEdit;
            if (Convert.ToString(ctrl.Tag) != string.Empty
                && dtBtnsM.Select("IsControlSet=1 AND BtnName='" + ctrl.Name.ToString() + "'").Length > 0)
            {
                txt.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(txt_EditValueChanging);
            }
        }
        private void frmBasicEdit_Load(object sender, EventArgs e)
        {
        }

        public override void GetCurrAllItem()
        {
            string[] strKey = "CDateSt,CDateEd,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string strDt = DateTime.Today.ToShortDateString();
            string[] strVal = new string[] {strDt,strDt,
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
                SetGroupCtrol(UkyndaGcItems["gcOrd"], "VIEW");
                SetBarItemByMode("VIEW");
            }
            StaticFunctions.SetControlBindings(gcOrd, gv.GridControl.DataSource as DataView, dr);
            btnDelete.Text = (dr[strOrdState].ToString() == "True" ? "弃用&D" : "启用&D");

            blSetItemStatusByOrd = true;
            BoundGridInfoData(dr[strKeyFiled].ToString());
            blSetItemStatusByOrd = false;

            foreach (string strKey in UkyndaGcItems.Keys)
            {
                if (UkyndaGcItems[strKey].GridViewName != "gridVMain")
                    SetGroupCtrol(UkyndaGcItems[strKey], "VIEW");
            }
            SetBtnState(dr[strOrdState].ToString());
            SetFocRowstyleFormat(dr);

            if (blShowPicEdit && frmDataTable.Columns.Contains("StylePic") && frmDataTable.Columns.Contains("Pic_Version"))
            {
                //绑定图片
                this.btnFile.Text = "";
                drPicEditTemp = dr;
                ShowPicture();
                //tdCreateBarcodeImage = new Thread(new ThreadStart(ShowPicture));
                //tdCreateBarcodeImage.Start();
            }
        }
        private void SetFocRowstyleFormat(DataRow dr)
        {
            if (dr[strOrdState].ToString() == "False")
            {
                gridVMain.Appearance.FocusedRow.BackColor = System.Drawing.Color.Coral;
            }
            else
            {
                gridVMain.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(129, 171, 177);//InitGridViewStyle
                gridVMain.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;//InitGridViewStyle
            }
        }
        private void ShowPicture()
        {
            DataRow dr = drPicEditTemp;
            string AppPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string AllPath = AppPath + frmImageFilePath;
            string PicFileName = string.Format("{0}\\{1}_ver{2}", AllPath, dr["StylePic"].ToString(), dr["Pic_Version"]);
            try
            {
                if (!File.Exists(PicFileName))
                    StaticFunctions.CreateImageFile(PicFileName, ServerRefManager.PicFileRead(dr["StylePic"].ToString(), dr["Pic_Version"].ToString()));

                picEdit.EditValue = ServerRefManager.GetbytesFromFile(PicFileName);
            }
            catch (Exception)
            {
                picEdit.EditValue = null;
            }
            finally
            {
                //tdCreateBarcodeImage.Abort();
                //tdCreateBarcodeImage = null;
                drPicEditTemp = null;
            }
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
            if (strMode == "VIEW")
            {
                StaticFunctions.SetBtnEnabled(new Component[] { btnCancel, btnSave, btnFile }, false);
                StaticFunctions.SetBtnEnabled(new Component[] { btnCopy, btnAdd, btnEdit, btnDelete, btnReLoad }, true);
            }
            else
            {
                StaticFunctions.SetBtnEnabled(new Component[] { btnCopy, btnAdd, btnEdit, btnDelete, btnReLoad }, false);
                StaticFunctions.SetBtnEnabled(new Component[] { btnCancel, btnSave, btnFile }, true);
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
                StaticFunctions.SetBtnStatu(lisBtnAlls, dr["BtnEnableId"].ToString() + ",btnReLoad");
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (blSysProcess)
                return;
            try
            {
                SimpleButton btn = sender as SimpleButton;
                blSysProcess = true;
                this.Cursor = Cursors.WaitCursor;
                switch (btn.Name)
                {
                    case "btnCopy":
                        DoCopyAdd();
                        break;
                    case "btnReLoad":
                        DoReLoad();
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
                    case "btnDelete":
                        SetState(btnDelete.Text == "弃用&D" ? "0" : "1");
                        break;
                    default:
                        DoBtnSpecial(btn.Name);
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
        private void DoCopyAdd()
        {
            DataRow drFoc = gridVMain.GetFocusedDataRow();

            UkyndaGcEdit GcOrd = UkyndaGcItems["gcOrd"];
            DataRow drNew = this.frmDataTable.NewRow();
            foreach (string strKey in lisOrdSpCtrlId)
            {
                if (frmDataTable.Columns.Contains(strKey))
                    drNew[strKey] = DBNull.Value;
            }
            if (drFoc != null)
            {
                string strCopyFields = drMain["CopyFields"].ToString();
                if (strCopyFields.Trim() != string.Empty)
                {
                    string[] arrFields = strCopyFields.Split(",".ToCharArray());
                    foreach (string strField in arrFields)
                    {
                        drNew[strField] = drFoc[strField];
                    }
                }
                else
                {
                    drNew.ItemArray = drFoc.ItemArray;
                }
            }
            drNew[strKeyFiled] = DBNull.Value;
            drNew[strOrdState] = true;
            if (frmDataTable.Columns.IndexOf("StylePic") != -1)
            {
                drNew["StylePic"] = DBNull.Value;
                drNew["Pic_Version"] = DBNull.Value;
                drNew["ExistFile"] = DBNull.Value;
            }

            blInitBound = true;
            frmDataTable.Rows.InsertAt(drNew, 0);//可能引发gridView1_FocusedRowChanged
            gridVMain.MoveFirst();
            blInitBound = false;
            gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            SetBarItemByMode("ADD");
            SetGroupCtrol(GcOrd, "ADD");
            StaticFunctions.SetFirstEditContrSelect(GcOrd.CtrFirstAddFocusContr);
        }
        private void DoReLoad()
        {
            gcItems.Clear();
            lisrepImg.Clear();
            lisBtns.Clear();
            lisBtnAlls.Clear();
            GvNeedGCEdit.Clear();
            UkyndaGcItems.Clear();
            lisOrdSpCtrlId.Clear();
            gridVMain.Columns.Clear();
            gcOrd.Controls.Clear();
            arrContrSeq.Clear();
            for (int i = xtabItemInfo.TabPages.Count - 1; i > 0; i--)
            {
                xtabItemInfo.TabPages.RemoveAt(i);
            }
            dsLoad = null;
            InitContr();

            gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVMain.FocusedRowHandle));
            gridVMain.BestFitColumns();
            frmBasicEdit_Load(null, null);
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
            string strCopyFields = drMain["CopyFields"].ToString();
            if (strCopyFields.Trim() != string.Empty && drFoc != null)
            {
                string[] arrFields = strCopyFields.Split(",".ToCharArray());
                foreach (string strField in arrFields)
                {
                    drNew[strField] = drFoc[strField];
                }
            }
            blInitBound = false;
            gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            SetBarItemByMode("ADD");
            SetGroupCtrol(GcOrd, "ADD");
            StaticFunctions.SetFirstEditContrSelect(GcOrd.CtrFirstAddFocusContr);
        }
        private void DoEdit()
        {
            DataRow dr = this.gridVMain.GetFocusedDataRow();
            if (dr == null)
                return;

            UkyndaGcEdit GcOrd = UkyndaGcItems["gcOrd"];
            SetBarItemByMode("EDIT");
            SetGroupCtrol(GcOrd, "EDIT");
            SetContrEditFromDpl(GcOrd, string.Empty);
            StaticFunctions.SetFirstEditContrSelect(GcOrd.CtrFirstEditFocusContr);
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

            UkyndaGcEdit GcEdit = UkyndaGcItems["gcOrd"];
            if (GcEdit.SaveMoveToCtrId != string.Empty)
            {
                StaticFunctions.SetContrSelect(GcEdit.CtrParentControl, GcEdit.SaveMoveToCtrId);
            }
            if (!StaticFunctions.CheckSave(dr, GcEdit.CtrParentControl, dtShow))
                return;

            if (blShowPicEdit)
            {
                if (btnFile.Text != string.Empty && !File.Exists(btnFile.Text))
                {
                    MessageBox.Show("指定路径下的图像文件不存在，请检查.");
                    return;
                }
            }
            if (!DoCheckSaveOrd())
                return;

            string strField = string.Empty;
            string strValues = string.Empty;
            btnSave.Enabled = false;
            try
            {
                if (dr[strKeyFiled].ToString() == string.Empty)
                {
                    strValues = StaticFunctions.GetAddValues(dr, GcEdit.StrFileds, out strField);

                    DataSet dtAdd = null;
                    string strSpParmName = string.Empty;
                    List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(GcEdit.CtrParentControl, dtShow, out strSpParmName);
                    if (strSpParmName != string.Empty)
                        strSpParmName += ",";

                    DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + strAddFlag + "'");
                    if (drShares.Length == 0)
                    {
                        string[] strKey = null;
                        if (blShowPicEdit)
                        {
                            strKey = (strSpParmName + "UImg,strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                            lisSpParmValue.AddRange(new string[] {
                                btnFile.Text.Trim()==string .Empty?"False":"True",
                                strField,
                                strValues,
                                CApplication.App.CurrentSession.UserId.ToString(),
                                CApplication.App.CurrentSession.DeptId.ToString(),
                                CApplication.App.CurrentSession.FyId.ToString(),
                                strAddFlag});
                        }
                        else
                        {
                            strKey = (strSpParmName + "strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                            lisSpParmValue.AddRange(new string[] {
                                strField,
                                strValues,
                                CApplication.App.CurrentSession.UserId.ToString(),
                                CApplication.App.CurrentSession.DeptId.ToString(),
                                CApplication.App.CurrentSession.FyId.ToString(),
                                strAddFlag});
                        }
                        dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
                    }
                    else
                    {
                        string[] strKey = (strSpParmName + "BsuSetSp_Id,UImg,strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id").Split(",".ToCharArray());
                        lisSpParmValue.AddRange(new string[] {drShares[0]["BsuSetSp_Id"].ToString(),
                            btnFile.Text.Trim()==string .Empty?"0":"1",
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
                }
                else
                {
                    strValues = StaticFunctions.GetUpdateValues(frmDataTable, dr, GcEdit.StrFileds);
                    if (strValues != string.Empty || blShowPicEdit && btnFile.Text != string.Empty)
                    {
                        DataTable dtAdd = null;
                        string strSpParmName = string.Empty;
                        List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(GcEdit.CtrParentControl, dtShow, out strSpParmName);
                        if (strSpParmName != string.Empty)
                            strSpParmName += ",";

                        DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + strEditFlag + "'");
                        if (drShares.Length == 0)
                        {
                            string[] strKey = null;
                            if (blShowPicEdit)
                            {
                                strKey = (strSpParmName + "UImg,strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                                lisSpParmValue.AddRange(new string[] { 
                                btnFile.Text.Trim()==string .Empty?"False":"True",
                                strValues,
                                dr[strKeyFiled].ToString(),
                                CApplication.App.CurrentSession.UserId.ToString(),
                                CApplication.App.CurrentSession.DeptId.ToString(),
                                CApplication.App.CurrentSession.FyId.ToString(),
                                strEditFlag});
                            }
                            else
                            {
                                strKey = (strSpParmName + "strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                                lisSpParmValue.AddRange(new string[] { 
                                 strValues,
                                 dr[strKeyFiled].ToString(),
                                 CApplication.App.CurrentSession.UserId.ToString(),
                                 CApplication.App.CurrentSession.DeptId.ToString(),
                                 CApplication.App.CurrentSession.FyId.ToString(),
                                 strEditFlag});
                            }
                            dtAdd = this.DataRequest_By_DataTable(strSpName, strKey, lisSpParmValue.ToArray());
                        }
                        else
                        {
                            string[] strKey = (strSpParmName + "BsuSetSp_Id,UImg,strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id").Split(",".ToCharArray());
                            lisSpParmValue.AddRange(new string[] {drShares[0]["BsuSetSp_Id"].ToString(),
                                 btnFile.Text.Trim()==string .Empty?"0":"1",
                                 strValues,
                                 dr[strKeyFiled].ToString(),
                                 CApplication.App.CurrentSession.UserId.ToString(),
                                 CApplication.App.CurrentSession.DeptId.ToString(),
                                 CApplication.App.CurrentSession.FyId.ToString()});
                            dtAdd = this.DataRequest_By_DataTable(strShareSpName, strKey, lisSpParmValue.ToArray());
                        }
                        if (dtAdd == null)
                        {
                            btnSave.Enabled = true;
                            return;
                        }
                        if (dtAdd.Rows.Count > 0 && dtAdd.Columns.Contains("UpdateFields"))
                        {
                            DataRow drNew = dtAdd.Rows[0];
                            StaticFunctions.UpdateDataRowSyn(dr, drNew, drNew["UpdateFields"].ToString());
                        }
                    }
                }
                dr.AcceptChanges();
                if (blShowPicEdit)
                {
                    //保存图片
                    if (picEdit.EditValue != null && picEdit.EditValue.ToString() != string.Empty && btnFile.Text != string.Empty)
                    {
                        try
                        {
                            ServerRefManager.FileSave(dr["StylePic"].ToString(), btnFile.Text, this.ckbDel.Checked);
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("上传图片失败:" + err.Message);
                            btnSave.Enabled = true;
                            return;
                        }
                        btnFile.Text = "";
                    }
                }
                SetGroupCtrol(GcEdit, "VIEW");
                SetBarItemByMode("VIEW");
            }
            catch (Exception ERR)
            {
                MessageBox.Show("保存出错:" + ERR.Message);
                btnSave.Enabled = true;
                return;
            }
        }
        private void SetState(string strState)
        {
            DataRow dr = gridVMain.GetFocusedDataRow();
            if (dr == null)
                return;

            DataTable dtAdd = null;
            DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + strDeleteFlag + "'");
            if (drShares.Length == 0)
            {
                string[] strKey = "Key_Id,State,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                string[] strVal = new string[] {
                    dr[strKeyFiled].ToString(),
                    strState,
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(),
                    strDeleteFlag};
                dtAdd = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
            }
            else
            {
                string[] strKey = "BsuSetSp_Id,Key_Id,EUser_Id,EDept_Id,Fy_Id".Split(",".ToCharArray());
                dtAdd = this.DataRequest_By_DataTable(strShareSpName,
                   strKey, new string[] {drShares[0]["BsuSetSp_Id"].ToString(),dr[strKeyFiled].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString()});
            }
            if (dtAdd == null)
            {
                return;
            }
            dr[strOrdState] = strState == "0" ? false : true;
            btnDelete.Text = (strState == "1" ? "弃用&D" : "启用&D");
            dr.AcceptChanges();
            MessageBox.Show("操作完成.");

            SetBtnState(dr[strOrdState].ToString());
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
            SetContrEditFromDpl(GcEdit, string.Empty);
            StaticFunctions.SetFirstEditContrSelect(GcEdit.CtrFirstEditFocusContr);

            blReAddInfo = false;
        }
        private void DoAddInfoSpecial(DataRow drBtn)
        {
            DataRow drOrd = gridVMain.GetFocusedDataRow();
            if (drOrd == null || drOrd[strKeyFiled].ToString() == string.Empty)
            {
                MessageBox.Show("请先保存主头.");
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
            }
            drNew[drs[0]["EditInfoKeyId"].ToString()] = DBNull.Value;

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

            DataRow dr = GcEdit.GridViewEdit.GetFocusedDataRow();
            if (dr == null)
                return;

            if (GcEdit.SaveMoveToCtrId != string.Empty)
            {
                StaticFunctions.SetContrSelect(GcEdit.CtrParentControl, GcEdit.SaveMoveToCtrId);
            }
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
            try
            {
                if (dr[strEditInfoKeyId].ToString() == string.Empty)
                {
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
                    DoAddInfo(GcEdit, CopyFields, drOrd, CopyFieldsOrd, false);
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
        private void btnFile_Properties_Click(object sender, EventArgs e)
        {
            if (!btnSave.Enabled)
                return;

            OpenFileDialog _svDlg = new OpenFileDialog();
            _svDlg.Filter = "图片文件|*.jpg|*.gif|*.bmp";
            _svDlg.ShowDialog();
            if (_svDlg.FileName.Length <= 0)
                return;

            if (!((_svDlg.FileName.ToLower().EndsWith(".jpg")) || (_svDlg.FileName.ToLower().EndsWith(".tif"))))
            {
                MessageBox.Show("请选择图像文件(.jpg 或 .tif)!");
                btnFile_Properties_Click(null, null);
            }
            else
            {
                try
                {
                    picEdit.EditValue = ServerRefManager.GetbytesFromFile(_svDlg.FileName);
                    btnFile.Text = _svDlg.FileName;
                }
                catch (Exception)
                {
                    MessageBox.Show("无法载入图像，请检查.");
                    btnFile_Properties_Click(null, null);
                }
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
            if (k == 32) //空格
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
                return base.ProcessCmdKey(ref msg, keyData);
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

        #region 可能需要扩充的事件
        private void DoBtnSpecial(string strBtnName)
        {
            DataRow[] drBtns = dtBtns.Select("BtnName='" + strBtnName + "'");
            if (drBtns.Length <= 0)
                return;

            DataRow drBtn = drBtns[0];
            if (drBtn["IsEditBtn"].ToString() == "True")
            {
                DoEditSpecial(drBtn);
            }
            else if (drBtn["IsBatchEditBtn"].ToString() == "True")
            {
                DoEditBatch(drBtn);
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
                case "MyBtn":
                    if (strBusClassName.ToUpper() == "frmSys_KpEdit".ToUpper())
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
