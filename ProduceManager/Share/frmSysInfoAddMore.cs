using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using System.IO;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;

namespace ProduceManager
{
    public partial class frmSysInfoAddMore : frmEditorBase
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
        private DataTable dtExcel = null;
        private bool blSysProcess = false;

        private DataSet dsFormAdt = null;
        private DataSet dsFormUkyndaAdt = null;
        private DataRow drMain = null;

        private string strShareSpName = "Share_Table_Op";
        private string strSpName = string.Empty;
        private string strKeyFiled = string.Empty;
        private string strKeyFiledOrd = string.Empty;
        private string strKeyIdOrd = string.Empty;
        private string strBusClassName = string.Empty;
        private string strEnterGc = string.Empty;
        private string strGetWFBalceCtrlIds = string.Empty;

        private List<string> lisOrdSpCtrlId = new List<string>();

        private Dictionary<string, GridView> gcItems = new Dictionary<string, GridView>();//所有子表的GridView集合，不包括gridVMain，key为GridViewName
        private Dictionary<string, UkyndaGcEdit> UkyndaGcItems = new Dictionary<string, UkyndaGcEdit>();//以编辑面板GroupBoxName为Key，保存UkyndaGcEdit

        private string CopyFields = string.Empty;
        private string CopyFieldsOrd = string.Empty;
        private bool blSetWeight = false;

        private DataRow drTabInfo = null;
        private DataSet dsSource = null;
        public DataRow DrOrd
        {
            get;
            set;
        }
        private string ClassNameParent = string.Empty;
        private string BusClassNameParent = string.Empty;
        #endregion

        public frmSysInfoAddMore(DataRow drBtn, DataRow drOrd, string strSpName, string strKeyFiledOrd, string strClassNameParent, string strBusClassNameParent)
        {
            InitializeComponent();

            this.Text = drBtn["FrmClassText"].ToString();
            strBusClassName = drBtn["FrmClassName"].ToString();
            DrOrd = drOrd;
            this.strSpName = strSpName;
            this.strKeyFiledOrd = strKeyFiledOrd;
            strKeyIdOrd = drOrd[strKeyFiledOrd].ToString();
            ClassNameParent = strClassNameParent;
            BusClassNameParent = strBusClassNameParent;

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
            strKeyFiled = drMain["KeyIdFiled"].ToString();
            strGetWFBalceCtrlIds = drMain["GetWFBalceCtrlIds"].ToString();

            dtBtns = dsLoad.Tables[3];
            dtTabs = dsLoad.Tables[4];
            dtGroupC = dsLoad.Tables[5];
            dtSte = dsLoad.Tables[6];
            dtContr = dsLoad.Tables[7];
            dtBtnsM = dsLoad.Tables[8];
            dtSp = dsLoad.Tables[9];
            dtExcel = dsLoad.Tables[10];

            if (dtTabs.Rows.Count > 0)
            {
                drTabInfo = dtTabs.Rows[0];
                CopyFields = drTabInfo["BtnEnableIdEdit"].ToString();
                CopyFieldsOrd = drTabInfo["BtnEnableIdAdd"].ToString();
            }

            StaticFunctions.ShowGridControl(gridVInfo2, dtShow, dtConst);
            StaticFunctions.ShowGridControl(gridVInfo1, dtShow, dtConst);
            GridView gvChild = StaticFunctions.ShowGridVChildGv("gridVInfo1Com", gridCInfo1, dtShow, dtConst);

            ShowGcContrs();
        }
        private void ShowGcContrs()
        {
            List<Control> lisGcContrGcsRet = new List<Control>();

            string strNoEnableCtrIdsOrd = string.Empty;
            Control CtrFirstEditContrOrd = null;
            string[] strFiledsOrd = null;
            bool blSetDefaultOrd = false;
            List<string> arrContrSeqOrd = new List<string>();

            Rectangle rect = SystemInformation.VirtualScreen;
            int igcHeight;
            int iMaxHeight = 0;

            List<Control> lisGcContrGcs = StaticFunctions.ShowGcContrs(gcInfo1, rect.Width - 30, dtShow, dtConst, true, 30, true, arrContrSeqOrd, false
                , out blSetDefaultOrd, out strNoEnableCtrIdsOrd, out strFiledsOrd, out CtrFirstEditContrOrd, out igcHeight);
            lisGcContrGcsRet.AddRange(lisGcContrGcs);
            iMaxHeight += igcHeight + 30;
            splitContainerControl2.SplitterPosition = iMaxHeight;

            UkyndaGcEdit GcEdit = new UkyndaGcEdit();
            GcEdit.CtrParentControl = gcInfo1;
            GcEdit.GridViewName = "gridVInfo1";
            GcEdit.BtnEnterSaveBtnId = "btnSaveInfo1";
            GcEdit.SaveMoveToCtrId = StaticFunctions.GetLastEditContrId(gcInfo1, dtShow);
            GcEdit.BlSetDefault = blSetDefaultOrd;
            GcEdit.StrNoEnableCtrIds = strNoEnableCtrIdsOrd;
            GcEdit.StrNoEnableEditCtrIds = StaticFunctions.GetReadOnlyEditIds(dtShow, "gcInfo1");
            GcEdit.StrFileds = strFiledsOrd;
            GcEdit.CtrFirstAddFocusContr = CtrFirstEditContrOrd;
            GcEdit.CtrFirstEditFocusContr = StaticFunctions.GetFirstEditFocusContr(gcInfo1, dtShow);
            GcEdit.ArrContrSeq.AddRange(arrContrSeqOrd);
            GcEdit.BtnEnterSave = btnSaveInfo1;
            GcEdit.GridViewEdit = gridVInfo1;
            GcEdit.StrMode = "";
            UkyndaGcItems.Add("gcInfo1", GcEdit);

            int iMaxWidth = int.Parse(drTabInfo["PGCContrWidth"].ToString());
            if (iMaxWidth == 0)
                iMaxWidth = 600;
            splitContainerControl3.SplitterPosition = iMaxWidth;

            List<Control> lisGcContrGcs2 = StaticFunctions.ShowGcContrs(gcInfo2, iMaxWidth - 30, dtShow, dtConst, true, 30, true, arrContrSeqOrd, false
                , out blSetDefaultOrd, out strNoEnableCtrIdsOrd, out strFiledsOrd, out CtrFirstEditContrOrd, out igcHeight);
            lisGcContrGcsRet.AddRange(lisGcContrGcs2);
            iMaxHeight += igcHeight + 30;
            splitContainerControl1.SplitterPosition = iMaxHeight;

            UkyndaGcEdit GcEdit2 = new UkyndaGcEdit(); 
            GcEdit2.CtrParentControl = gcInfo2;
            GcEdit2.GridViewName = "gridVInfo2";
            GcEdit2.BtnEnterSaveBtnId = "btnSaveInfo2";
            GcEdit2.SaveMoveToCtrId = StaticFunctions.GetLastEditContrId(gcInfo2, dtShow);
            GcEdit2.BlSetDefault = blSetDefaultOrd;
            GcEdit2.StrNoEnableCtrIds = strNoEnableCtrIdsOrd;
            GcEdit2.StrNoEnableEditCtrIds = StaticFunctions.GetReadOnlyEditIds(dtShow, "gcInfo2"); ;
            GcEdit2.StrFileds = strFiledsOrd;
            GcEdit2.CtrFirstAddFocusContr = CtrFirstEditContrOrd;
            GcEdit2.CtrFirstEditFocusContr = StaticFunctions.GetFirstEditFocusContr(gcInfo2, dtShow);
            GcEdit2.ArrContrSeq.AddRange(arrContrSeqOrd);
            GcEdit2.BtnEnterSave = btnSaveInfo2;
            GcEdit2.GridViewEdit = gridVInfo2;
            GcEdit2.StrMode = "";
            UkyndaGcItems.Add("gcInfo2", GcEdit2);

            #region GroupC
            foreach (Control ctrl in lisGcContrGcsRet)
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
                        if (ucp.Parent != null && ucp.Parent.Name == "gcInfo1" && ucp.Tag != null && ucp.Tag.ToString() != string.Empty)
                        {
                            lisOrdSpCtrlId.Add(ucp.Tag.ToString());
                        }
                        break;
                    case "ProduceManager.UcTreeList":
                        ProduceManager.UcTreeList uct = ctrl as ProduceManager.UcTreeList;
                        uct.onClosePopUp += new UcTreeList.ClosePopUp(uct_onClosePopUp);
                        StaticFunctions.BoundSpicalContr(dtContr, dsFormAdt, dsFormUkyndaAdt, uct, dtShow);
                        if (uct.Parent != null && uct.Parent.Name == "gcInfo1" && uct.Tag != null && uct.Tag.ToString() != string.Empty)
                        {
                                lisOrdSpCtrlId.Add(uct.Tag.ToString());
                        }
                        break;
                    case "ExtendControl.ExtPopupTree":
                        ExtendControl.ExtPopupTree ept = ctrl as ExtendControl.ExtPopupTree;
                        ept.Properties.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.lookUpEdit_Properties_Closed);
                        StaticFunctions.BoundSpicalContr(dtContr, dsFormAdt, dsFormUkyndaAdt, ept, dtShow);
                        if (ept.Parent != null && ept.Parent.Name == "gcInfo1" && ept.Tag != null && ept.Tag.ToString() != string.Empty)
                        {
                                lisOrdSpCtrlId.Add(ept.Tag.ToString());
                        }
                        break;

                    default:
                        break;
                }
            }

            #endregion
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

            DataRow drOrd = null;
            if (gc.GridViewName == "gridVInfo1")
            {
                drOrd = DrOrd;
            }
            else
            {
                drOrd = gridVInfo1.GetFocusedDataRow();
            }
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
                    if (gc.GridViewName == "gridVInfo1")
                    {
                        ucp.DrFilterFieldsOrd = DrOrd;
                    }
                    else
                    {
                        ucp.DrFilterFieldsOrd = gridVInfo1.GetFocusedDataRow();
                    }
                    ucp.DrFilterFieldsInfo = gc.GridViewEdit.GetFocusedDataRow();
                }
                else if (FocusedControl.GetType().ToString() == "ProduceManager.UcTreeList")
                {
                    ProduceManager.UcTreeList ucp = FocusedControl as ProduceManager.UcTreeList;
                    if (gc.GridViewName == "gridVInfo1")
                    {
                        ucp.DrFilterFieldsOrd = DrOrd;
                    }
                    else
                    {
                        ucp.DrFilterFieldsOrd = gridVInfo1.GetFocusedDataRow();
                    }
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

        private void frmSysInfoAddItem_Load(object sender, EventArgs e)
        {
            blSetWeight = GetUserSetWeightfrmCalsses(strBusClassName);
            DoQuery(); 
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

        public override void RefreshItem()
        {
            DoQuery();
        }
        private void DoQuery()
        {
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {strKeyIdOrd,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         drMain["QueryFlag"].ToString()};
            dsSource = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            dsSource.Relations.Add("gridCInfo1ChildGrid", dsSource.Tables[0].Columns[strKeyFiled], dsSource.Tables[1].Columns[strKeyFiled]);
            dsSource.AcceptChanges();

            blInitBound = true;
            gridCInfo1.DataSource = dsSource.Tables[0].DefaultView;
            gridVInfo1.BestFitColumns();
            gridCInfo2.DataSource = dsSource.Tables[1].DefaultView;
            gridVInfo2.BestFitColumns();
            blInitBound = false;

            gridVInfo1_FocusedRowChanged(gridVInfo1, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVInfo1.FocusedRowHandle));

            if (dsSource.Tables[0].Rows.Count == 0)
            {
                DoAddInfo1();
            }
        }
        private void gridVInfo1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1 || blInitBound)
                return;

            GridView gv = sender as GridView;
            DataRow drP = gv.GetDataRow(e.PrevFocusedRowHandle);
            if (drP != null && drP.RowState != DataRowState.Unchanged)
            {
                blInitBound = true;
                drP.RejectChanges();//引发gridView1_FocusedRowChanged
                dsSource.Tables[1].RejectChanges();
                blInitBound = false;
            }

            DataRow dr = gv.GetFocusedDataRow();
            blInitBound = true;
            string strFilter = string.Empty;
            if (dr == null)
            {
                strFilter = strKeyFiled + "=-9999";
                StaticFunctions.SetControlEmpty(gcInfo1);
            }
            else
            {
                strFilter = strKeyFiled + "=" + (dr[strKeyFiled].ToString() == string.Empty ? "-9999" : dr[strKeyFiled].ToString());
                StaticFunctions.SetControlBindings(gcInfo1, gv.GridControl.DataSource as DataView, dr);
            }
            dsSource.Tables[1].DefaultView.RowFilter = strFilter;
            blInitBound = false;

            SetBtnItemByMode(UkyndaGcItems["gcInfo1"], "VIEW");
            SetGroupCtrol(UkyndaGcItems["gcInfo1"], "VIEW");

            gridVInfo2_FocusedRowChanged(gridVInfo2, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-2, gridVInfo1.FocusedRowHandle));
            if (gridVInfo2.RowCount == 0)
            {
                SetBtnItemByMode(UkyndaGcItems["gcInfo2"], "VIEW");
                SetGroupCtrol(UkyndaGcItems["gcInfo2"], "VIEW");
            }
        }
        private void gridVInfo2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
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
            {
                StaticFunctions.SetControlEmpty(gcInfo2);
            }
            else
            {
                StaticFunctions.SetControlBindings(gcInfo2, gv.GridControl.DataSource as DataView, dr);
            }
            SetBtnItemByMode(UkyndaGcItems["gcInfo2"], "VIEW");
            SetGroupCtrol(UkyndaGcItems["gcInfo2"], "VIEW");
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
                switch (btn.Name)
                {
                    case "btnAddInfo1":
                        DoAddInfo1();
                        break;
                    case "btnEditInfo1":
                        DoEditInfo1();
                        break;
                    case "btnSaveInfo1":
                        DoSaveInfo1();
                        break;
                    case "btnCancelInfo1":
                        DoCancelInfo1();
                        break;

                    case "btnAddInfo2":
                        DoAddInfo2();
                        break;
                    case "btnEditInfo2":
                        DoEditInfo2();
                        break;
                    case "btnSaveInfo2":
                        DoSaveInfo2();
                        break;
                    case "btnCancelInfo2":
                        DoCancelInfo2();
                        break;
                    default:
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
        private void SetBtnItemByMode(UkyndaGcEdit gcEdit, string strMode)
        {
            if (gcEdit.StrMode == strMode)
                return;

            List<SimpleButton> lisBtns = new List<SimpleButton>();
            if (gcEdit.GridViewName == "gridVInfo1")
            {
                lisBtns.AddRange(new SimpleButton[] { btnAddInfo1, btnEditInfo1, btnSaveInfo1, btnCancelInfo1 });
            }
            else
            {
                lisBtns.AddRange(new SimpleButton[] { btnAddInfo2, btnEditInfo2, btnSaveInfo2, btnCancelInfo2 });
            }
            switch (strMode)
            {
                case "VIEW":
                    if (gcEdit.GridViewName == "gridVInfo1")
                    {
                        StaticFunctions.SetBtnStatu(lisBtns, "btnAddInfo1,btnEditInfo1");
                    }
                    else
                    {
                        StaticFunctions.SetBtnStatu(lisBtns, "btnAddInfo2,btnEditInfo2");
                    }
                    break;

                case "ADD":
                case "EDIT":
                    if (gcEdit.GridViewName == "gridVInfo1")
                    {
                        StaticFunctions.SetBtnStatu(lisBtns, "btnSaveInfo1,btnCancelInfo1");
                    }
                    else
                    {
                        StaticFunctions.SetBtnStatu(lisBtns, "btnSaveInfo2,btnCancelInfo2");
                    }
                    break;

                default:
                    break;
            }
        }
        private void DoAddInfo1()
        {
            DataRow drFoc = gridVInfo1.GetFocusedDataRow();
            if (drFoc != null && drFoc[strKeyFiled].ToString() == string.Empty)//已经在新增状态
                return;

            UkyndaGcEdit GcOrd = UkyndaGcItems["gcInfo1"];
            DataRow drNew = this.dsSource.Tables[0].NewRow();
            blInitBound = true;
            dsSource.Tables[0].Rows.InsertAt(drNew, 0);//可能引发gridView1_FocusedRowChanged
            this.gridVInfo1.MoveFirst();
            foreach (string strKey in lisOrdSpCtrlId)
            {
                if (dsSource.Tables[0].Columns.Contains(strKey))
                    drNew[strKey] = DBNull.Value;
            }
            if (GcOrd.BlSetDefault)
            {
                StaticFunctions.SetContrDefaultValue(GcOrd.CtrParentControl, dtShow, drNew);
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
            gridVInfo1_FocusedRowChanged(gridVInfo1, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVInfo1.FocusedRowHandle));

            SetBtnItemByMode(GcOrd, "ADD");
            SetGroupCtrol(GcOrd, "ADD");
            SetContrEditFromDpl(GcOrd, string.Empty);
            StaticFunctions.SetFirstEditContrSelect(GcOrd.CtrFirstAddFocusContr);
        }
        private void DoEditInfo1()
        {
            DataRow dr = this.gridVInfo1.GetFocusedDataRow();
            if (dr == null)
                return;

            UkyndaGcEdit GcEdit = UkyndaGcItems["gcInfo1"];
            SetBtnItemByMode(GcEdit, "EDIT");
            SetGroupCtrol(GcEdit, "EDIT");
            SetContrEditFromDpl(GcEdit, string.Empty);
            StaticFunctions.SetFirstEditContrSelect(GcEdit.CtrFirstEditFocusContr);
        }
        private void DoSaveInfo1()
        {
            DataRow dr = gridVInfo1.GetFocusedDataRow();
            if (dr == null)
                return;

            UkyndaGcEdit GcEdit = UkyndaGcItems["gcInfo1"];
            if (GcEdit.SaveMoveToCtrId != string.Empty)
            {
                StaticFunctions.SetContrSelect(GcEdit.CtrParentControl, GcEdit.SaveMoveToCtrId);
            }
            if (!StaticFunctions.CheckSave(dr, GcEdit.CtrParentControl, dtShow))
                return;

            DataSet dtAdd = null;
            string strField = string.Empty;
            string strValues = string.Empty;
            bool blAdd = false;
            try
            {
                if (dr[strKeyFiled].ToString() == string.Empty)
                {
                    blAdd = true;
                    dr[strKeyFiledOrd] = strKeyIdOrd;
                    strValues = StaticFunctions.GetAddValues(dr, GcEdit.StrFileds, out strField);

                    string strSpParmName = string.Empty;
                    List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(GcEdit.CtrParentControl, dtShow, out strSpParmName);
                    if (strSpParmName != string.Empty)
                        strSpParmName += ",";

                    DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + drMain["AddFlag"].ToString() + "'");
                    if (drShares.Length == 0)
                    {
                        string[] strKey = (strSpParmName + "strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                        lisSpParmValue.AddRange(new string[] {
                        strField,
                        strValues,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         drMain["AddFlag"].ToString()});
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
                    dr[strKeyFiled] = drNew[strKeyFiled];
                    dsSource.Tables[1].DefaultView.RowFilter = strKeyFiled + "=" + drNew[strKeyFiled].ToString();
                    if (dtAdd.Tables[0].Columns.Contains("UpdateFields"))
                    {
                        StaticFunctions.UpdateDataRowSyn(dr, drNew, drNew["UpdateFields"].ToString());
                    }
                }
                else
                {
                    strValues = StaticFunctions.GetUpdateValues(dsSource.Tables[0], dr, GcEdit.StrFileds);
                    if (strValues != string.Empty)
                    {
                        string strSpParmName = string.Empty;
                        List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(GcEdit.CtrParentControl, dtShow, out strSpParmName);
                        if (strSpParmName != string.Empty)
                            strSpParmName += ",";

                        DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + drMain["EditFlag"].ToString() + "'");
                        if (drShares.Length == 0)
                        {
                            string[] strKey = (strSpParmName + "strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                            lisSpParmValue.AddRange(new string[] { 
                             strValues,
                             dr[strKeyFiled].ToString(),
                             CApplication.App.CurrentSession.UserId.ToString(),
                             CApplication.App.CurrentSession.DeptId.ToString(),
                             CApplication.App.CurrentSession.FyId.ToString(),
                             drMain["EditFlag"].ToString()});
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
                            return;
                        }
                        if (dtAdd.Tables[0].Rows.Count > 0 && dtAdd.Tables[0].Columns.Contains("UpdateFields"))
                        {
                            DataRow drNew = dtAdd.Tables[0].Rows[0];
                            StaticFunctions.UpdateDataRowSyn(dr, drNew, drNew["UpdateFields"].ToString());
                        }
                    }
                }
                dr.AcceptChanges();
                SetBtnItemByMode(GcEdit, "VIEW");
                SetGroupCtrol(GcEdit, "VIEW");
                if (blAdd)//如果新增状态，则在保存1级后主动新增2级
                {
                    DoAddInfo2();
                }
            }
            catch (Exception ERR)
            {
                MessageBox.Show("保存出错:" + ERR.Message);
                return;
            }
        }
        private void DoCancelInfo1()
        {
            DataRow dr = this.gridVInfo1.GetFocusedDataRow();
            if (dr == null)
                return;

            blInitBound = true;
            dsSource.Tables[0].RejectChanges();
            dsSource.Tables[0].AcceptChanges();
            blInitBound = false;

            gridVInfo1_FocusedRowChanged(gridVInfo1, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVInfo1.FocusedRowHandle));
            if (gridVInfo1.RowCount == 0)
            {
                SetBtnItemByMode(UkyndaGcItems["gcInfo1"], "VIEW");
                SetGroupCtrol(UkyndaGcItems["gcInfo1"], "VIEW");
            }
        }

        private void DoAddInfo2()
        {
            DataRow drOrd = gridVInfo1.GetFocusedDataRow();
            if (drOrd == null || drOrd[strKeyFiled].ToString() == string.Empty)
            {
                MessageBox.Show("请先保存1级明细.");
                return;
            }
            string strKeyIdInfo = drTabInfo["EditInfoKeyId"].ToString();
            DataRow dr = gridVInfo2.GetFocusedDataRow();
            if (dr != null && dr[strKeyIdInfo].ToString() == string.Empty)//已经在新增状态
                return;

            UkyndaGcEdit GcEdit = UkyndaGcItems["gcInfo2"];
            GcEdit.GridViewEdit.ClearColumnsFilter();
            GcEdit.GridViewEdit.ClearSorting();

            DoAddInfo(GcEdit, CopyFields, drOrd, CopyFieldsOrd, true);
        }
        private void DoAddInfo(UkyndaGcEdit GcEdit, string strCopyFields, DataRow drOrd, string strCopyOrdFields, bool blFirst)
        {
            DataRow drFoc = gridVInfo2.GetFocusedDataRow();
            DataRow drNew = dsSource.Tables[1].NewRow();
            drNew[strKeyFiled] = drOrd[strKeyFiled];
            blInitBound = true;
            dsSource.Tables[1].Rows.InsertAt(drNew, gridVInfo2.RowCount);
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
            gridVInfo2_FocusedRowChanged(GcEdit.GridViewEdit, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, GcEdit.GridViewEdit.FocusedRowHandle));
            if (blFirst)
            {
                //SetBtnItemByMode(GcEdit, "ADD");
                //SetGroupCtrol(GcEdit, "ADD");
            }
            SetBtnItemByMode(GcEdit, "ADD");
            SetGroupCtrol(GcEdit, "ADD");
            SetContrEditFromDpl(GcEdit, string.Empty);
            StaticFunctions.SetFirstEditContrSelect(GcEdit.CtrFirstAddFocusContr);
        }
        private void DoEditInfo2()
        {
            DataRow dr = this.gridVInfo2.GetFocusedDataRow();
            if (dr == null)
                return;

            UkyndaGcEdit GcEdit = UkyndaGcItems["gcInfo2"];
            SetBtnItemByMode(GcEdit, "EDIT");
            SetGroupCtrol(GcEdit, "EDIT");
            SetContrEditFromDpl(GcEdit, string.Empty);
            StaticFunctions.SetFirstEditContrSelect(GcEdit.CtrFirstEditFocusContr);
        }
        private void DoSaveInfo2()
        {
            UkyndaGcEdit GcEdit = UkyndaGcItems["gcInfo2"];

            if (GcEdit.SaveMoveToCtrId != string.Empty)
            {
                StaticFunctions.SetContrSelect(GcEdit.CtrParentControl, GcEdit.SaveMoveToCtrId);
            }
            DataRow dr = GcEdit.GridViewEdit.GetFocusedDataRow();
            if (dr == null)
                return;

            if (!StaticFunctions.CheckSave(dr, GcEdit.CtrParentControl, dtShow))
                return;

            DataRow drOrd = gridVInfo1.GetFocusedDataRow();
            if (drOrd == null || drOrd[strKeyFiled].ToString() == string.Empty)
            {
                return;
            }

            string strEditInfoKeyId = drTabInfo["EditInfoKeyId"].ToString();
            string strField = string.Empty;
            string strValues = string.Empty;
            bool blAdd = false;
            try
            {
                if (dr[strEditInfoKeyId].ToString() == string.Empty)
                {
                    blAdd = true;
                    dr[strKeyFiled] = drOrd[strKeyFiled];
                    dr[strKeyFiledOrd] = strKeyIdOrd;
                    strValues = StaticFunctions.GetAddValues(dr, GcEdit.StrFileds, out strField);

                    DataSet dtAdd = null;
                    string strSpParmName = string.Empty;
                    List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(GcEdit.CtrParentControl, dtShow, out strSpParmName);
                    if (strSpParmName != string.Empty)
                        strSpParmName += ",";

                    DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + drTabInfo["AddFlag"].ToString() + "'");
                    if (drShares.Length == 0)
                    {
                        string[] strKey = (strSpParmName + "Ord_Id,strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                        lisSpParmValue.AddRange(new string[] {strKeyIdOrd,
                        strField,
                        strValues,
                        CApplication.App.CurrentSession.UserId.ToString(),
                        CApplication.App.CurrentSession.DeptId.ToString(),
                        CApplication.App.CurrentSession.FyId.ToString(),
                        drTabInfo["AddFlag"].ToString()});
                        dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
                    }
                    else
                    {
                        string[] strKey = (strSpParmName + "BsuSetSp_Id,Ord_Id,strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id").Split(",".ToCharArray());
                        lisSpParmValue.AddRange(new string[] {drShares[0]["BsuSetSp_Id"].ToString(),strKeyIdOrd,
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
                    strValues = StaticFunctions.GetUpdateValues(dsSource.Tables[1], dr, GcEdit.StrFileds);
                    if (strValues != string.Empty)
                    {
                        DataSet dsAdd = null;
                        string strSpParmName = string.Empty;
                        List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(GcEdit.CtrParentControl, dtShow, out strSpParmName);
                        if (strSpParmName != string.Empty)
                            strSpParmName += ",";

                        DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + drTabInfo["EditFlag"].ToString() + "'");
                        if (drShares.Length == 0)
                        {
                            string[] strKey = (strSpParmName + "Ord_Id,strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                            lisSpParmValue.AddRange(new string[] {strKeyIdOrd, 
                                 strValues,
                                 dr[strEditInfoKeyId].ToString(),
                                 CApplication.App.CurrentSession.UserId.ToString(),
                                 CApplication.App.CurrentSession.DeptId.ToString(),
                                 CApplication.App.CurrentSession.FyId.ToString(),
                                 drTabInfo["EditFlag"].ToString()});
                            dsAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
                        }
                        else
                        {
                            string[] strKey = (strSpParmName + "BsuSetSp_Id,Ord_Id,strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id").Split(",".ToCharArray());
                            lisSpParmValue.AddRange(new string[] {drShares[0]["BsuSetSp_Id"].ToString(),strKeyIdOrd,
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
                if (blAdd)
                {
                    DoAddInfo(GcEdit, CopyFields, drOrd, CopyFieldsOrd, false);
                }
                else
                {
                    SetBtnItemByMode(GcEdit, "VIEW");
                    SetGroupCtrol(GcEdit, "VIEW");
                }
            }
            catch (Exception ERR)
            {
                MessageBox.Show("保存出错:" + ERR.Message);
                return;
            }
        }
        private void DoCancelInfo2()
        {
            DataRow dr = this.gridVInfo2.GetFocusedDataRow();
            if (dr == null)
                return;

            blInitBound = true;
            dsSource.Tables[1].RejectChanges();//引发gridView1_FocusedRowChanged
            dsSource.Tables[1].AcceptChanges();
            blInitBound = false;

            gridVInfo2_FocusedRowChanged(gridVInfo2, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVInfo2.FocusedRowHandle));
            if (gridVInfo2.RowCount == 0)
            {
                SetBtnItemByMode(UkyndaGcItems["gcInfo2"], "VIEW");
                SetGroupCtrol(UkyndaGcItems["gcInfo2"], "VIEW");
            }
        }

        public override bool DeleteFocusedItem()
        {
            if (drTabInfo == null)
                return false;

            DataRow dr = gridVInfo2.GetFocusedDataRow();
            if (dr == null)
                return false;

            string strMsg = drTabInfo["DeleteMsg"].ToString();
            string strDelId = drTabInfo["EditInfoKeyId"].ToString();
            string strDelFlag = drTabInfo["DeleteFlag"].ToString();

            if (strDelId == string.Empty)
            {
                return false;
            }

            if (MessageBox.Show(strMsg, "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return false;

            if (dr[strDelId].ToString() == string.Empty)
            {
                dr.Delete();
                dr.Table.AcceptChanges();
                return true;
            }

            DataTable dtAdd = null;
            DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + strDelFlag + "'");
            if (drShares.Length == 0)
            {
                string[] strKey = "Ord_Id,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                dtAdd = this.DataRequest_By_DataTable(strSpName,
                   strKey, new string[] {strKeyIdOrd,
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
                   strKey, new string[] {drShares[0]["BsuSetSp_Id"].ToString(),strKeyIdOrd,
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

            return true;
        }

        private void frmSysInfoAddMore_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form frmExist = StaticFunctions.GetExistedBsuChildForm(this.ParentForm, ClassNameParent, BusClassNameParent);
            if (frmExist != null && frmExist is frmEditorBase)
            {
                (frmExist as frmEditorBase).RefreshItem();
            }
        }
    }
}