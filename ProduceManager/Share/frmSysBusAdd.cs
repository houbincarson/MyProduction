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
    public partial class frmSysBusAdd : frmEditorBase
    {
        #region private Params
        private bool blSysProcess = false;
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

        private DataSet dsFormAdt = null;
        private DataSet dsFormUkyndaAdt = null;
        private DataRow drMain = null;
        private DataTable dtInfo = null;

        private DataTable dtInfoInfo = null;
        private GridView gvChild = null;
        private string[] strFiledsInfo = null;
        private bool IsAddChildGv = false;

        private string strSpName = string.Empty;
        private string strQueryFlag = string.Empty;
        private string strAddFlag = string.Empty;
        private string strKeyFiled = string.Empty;
        private string strEnterGc = string.Empty;
        private string strGetWFBalceCtrlIds = string.Empty;
        private bool blSetWeight = false;
        private DataRow drAddTemp = null;

        private DataRow drBtn = null;
        public DataRow DrBtn
        {
            get { return drBtn; }
            set { drBtn = value; }
        }
        public string StrOrdKeyId
        {
            get;
            set;
        }
        private string[] strFileds = null;
        public string[] StrFileds
        {
            get { return strFileds; }
            set { strFileds = value; }
        }
        public string ClassNameParent
        {
            get;
            set;
        }
        public string BusClassNameParent
        {
            get;
            set;
        }

        private Dictionary<string, Control> GcOrdControls = new Dictionary<string, Control>();
        private Dictionary<string, List<string>> GcOrdarrContrSeq = new Dictionary<string, List<string>>();
        private List<string> lstFiledsOrd = new List<string>();
        Thread tdCreateBarcodeImage = null;
        #endregion

        public frmSysBusAdd(DataRow drBtn)
        {
            InitializeComponent();
            this.drBtn = drBtn;
            this.Text = drBtn["FrmClassText"].ToString();

            InitContr();
            blSetWeight = GetUserSetWeightfrmCalsses(drBtn["FrmClassName"].ToString());

            if (drBtn["FrmSaveFilter"].ToString() == string.Empty)
            {
                btnPreview.Enabled = false;
            }
            if (drBtn["SelUpdateFields"].ToString() == string.Empty || drBtn["NoSelUpdateFields"].ToString() == string.Empty)
            {
                this.btnChkItem.Enabled = false;
            }
        }
        private void InitContr()
        {
            if (dsLoad != null)
                return;

            string strBusClassName = drBtn["FrmClassName"].ToString();
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
            strAddFlag = drMain["AddFlag"].ToString();
            strKeyFiled = drMain["KeyIdFiled"].ToString();
            strGetWFBalceCtrlIds = drMain["GetWFBalceCtrlIds"].ToString();

            splitContainerControl1.SplitterPosition = int.Parse(drMain["MainSplitterPosition"].ToString());

            dtBtns = dsLoad.Tables[3];
            dtTabs = dsLoad.Tables[4];
            dtGroupC = dsLoad.Tables[5];
            dtSte = dsLoad.Tables[6];
            dtContr = dsLoad.Tables[7];
            dtBtnsM = dsLoad.Tables[8];
            dtSp = dsLoad.Tables[9];

            if (drBtn["FrmShowIcon"].ToString() == "True")
            {
                GridColumn gridCol = new GridColumn();
                RepositoryItemImageEdit repImg = new RepositoryItemImageEdit();
                repImg.AutoHeight = false;
                repImg.Buttons.Clear();
                repImg.Name = "gridVInfo_repImg";
                repImg.PopupFormMinSize = new System.Drawing.Size(450, 350);
                repImg.ReadOnly = true;
                repImg.Popup += new System.EventHandler(this.repImg_Popup);

                gridCol.Caption = "图片";
                gridCol.ColumnEdit = repImg;
                gridCol.FieldName = "Icon";
                gridCol.Name = "gridVInfo_GCol";
                gridCol.OptionsColumn.AllowMove = false;
                gridCol.OptionsColumn.ReadOnly = true;
                gridCol.OptionsColumn.FixedWidth = true;
                gridCol.Visible = true;
                gridCol.VisibleIndex = 0;
                gridCol.Width = 50;

                this.gridCInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                    repImg});
                gridVInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridCol });
            }
            else if (drBtn["FrmShowPicEdit"].ToString() == "True")
            {
                GridColumn gridCol = new GridColumn();
                RepositoryItemPictureEdit repPic = new RepositoryItemPictureEdit();
                repPic.Name = "gridVInfo_repPic";
                repPic.ReadOnly = true;
                repPic.ShowMenu = false;
                repPic.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
                repPic.MouseLeave += new System.EventHandler(this.repPic_MouseLeave);
                repPic.MouseHover += new EventHandler(repPic_MouseHover);

                gridCol.Caption = "图片";
                gridCol.ColumnEdit = repPic;
                gridCol.FieldName = "Icon";
                gridCol.Name = "gridVInfo_GCol";
                gridCol.OptionsColumn.AllowMove = false;
                gridCol.OptionsColumn.ReadOnly = true;
                gridCol.OptionsColumn.FixedWidth = true;
                gridCol.Visible = true;
                gridCol.VisibleIndex = 0;
                gridCol.Width = int.Parse(drBtn["FrmPicGvWidth"].ToString());

                this.gridCInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                    repPic});
                gridVInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridCol });

                this.gridVInfo.RowHeight = int.Parse(drBtn["FrmPicGvRowHeight"].ToString());
                this.gridVInfo.OptionsView.ShowAutoFilterRow = false;
                this.picEdit.Location = new System.Drawing.Point(int.Parse(drBtn["FrmPicX"].ToString()), int.Parse(drBtn["FrmPicY"].ToString()));
            }
            Rectangle rect = SystemInformation.VirtualScreen;
            StaticFunctions.ShowGridControl(gridVInfo, dtShow, dtConst, out strFileds);
            StaticFunctions.SetGridViewStyleFormatCondition(gridVInfo, dtBtnsM);
            IsAddChildGv = this.drBtn["IsAddChildGv"].ToString() == "True";
            if (IsAddChildGv)
            {
                gvChild = StaticFunctions.ShowGridVChildGv("gridVCom", gridCInfo, dtShow, dtConst, out strFiledsInfo);
                StaticFunctions.SetGridViewStyleFormatCondition(gvChild, dtBtnsM);
            }

            blInitBound = true;
            #region GroupC
            GridViewEdit = gridVMain;
            List<Control> lisGcContrs = new List<Control>();
            int iMaxHeight = StaticFunctions.ShowTabItemBusAdd(dtTabs, dtGroupC, xtabItemInfo, xtabItemInfo.Name, string.Empty
                , lisGcContrs, dtShow, dtConst, GcOrdControls, GcOrdarrContrSeq, lstFiledsOrd);
            if (lisGcContrs.Count == 0)
            {
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
            }
            else
            {
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Both;
                splitContainerControl1.SplitterPosition = iMaxHeight + 50;
            }
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
            blInitBound = false;
            xtabItemInfo_SelectedPageChanged(xtabItemInfo, new DevExpress.XtraTab.TabPageChangedEventArgs(null, xtabItemInfo.SelectedTabPage));
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

        private void gridCInfo_Enter(object sender, EventArgs e)
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
                if (strEnterGc != ctrParent.Name)
                {
                    strEnterGc = ctrParent.Name;
                    ParentControl = GcOrdControls[strEnterGc];
                    arrContrSeq = GcOrdarrContrSeq[strEnterGc];
                }
                if (FocusedControl.GetType().ToString() == "ProduceManager.UcTxtPopup")
                {
                    ProduceManager.UcTxtPopup ucp = FocusedControl as ProduceManager.UcTxtPopup;
                    ucp.DrFilterFieldsInfo = GridViewEdit.GetFocusedDataRow();
                }
                else if (FocusedControl.GetType().ToString() == "ProduceManager.UcTreeList")
                {
                    ProduceManager.UcTreeList ucp = FocusedControl as ProduceManager.UcTreeList;
                    ucp.DrFilterFieldsInfo = GridViewEdit.GetFocusedDataRow();
                }
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

                DataRow drInfo = GridViewEdit.GetFocusedDataRow();
                if (drInfo == null)
                    return;

                drInfo[dpl.Tag.ToString()] = Convert.ToString(dpl.EditValue) == string.Empty ? DBNull.Value : dpl.EditValue;
                DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, dpl.Name, dpl.Parent.Name);
                StaticFunctions.UpdateDataRowSynLookUpEdit(drInfo, dpl, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString());
                SetContrEditFromDpl(dpl.Parent, dpl.Name);
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

                DataRow drInfo = GridViewEdit.GetFocusedDataRow();
                if (drInfo == null)
                    return;

                ExtendControl.ExtPopupTree ept = sender as ExtendControl.ExtPopupTree;
                DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, ept.Name, ept.Parent.Name);
                StaticFunctions.UpdateDataRowSynExtPopupTree(drInfo, ept, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString());
                SetContrEditFromDpl(ept.Parent, ept.Name);
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

            DataRow drInfo = GridViewEdit.GetFocusedDataRow();
            if (drInfo == null)
                return;

            ProduceManager.UcTxtPopup ucp = sender as ProduceManager.UcTxtPopup;
            drInfo[ucp.Tag.ToString()] = Convert.ToString(ucp.EditValue) == string.Empty ? DBNull.Value : ucp.EditValue;
            DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, ucp.Name, ucp.Parent.Name);
            StaticFunctions.UpdateDataRowSynUcTxtPopup(drInfo, drReturn, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString(), ucp);
            SetContrEditFromDpl(ucp.Parent, ucp.Name);
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

        private void SetContrEditFromDpl(Control ctrlParent,string strEnterName)
        {
            string strFilter = "IsContrEditSet=1 AND BtnParent='" + ctrlParent.Name + "'";
            if (strEnterName != string.Empty)
                strFilter += " AND BtnName='" + strEnterName + "'";
            DataRow[] drContrEditSets = dtBtnsM.Select(strFilter);
            if (drContrEditSets.Length <= 0)
                return;

            DataRow drOrd = gridVMain.GetFocusedDataRow();

            foreach (DataRow dr in drContrEditSets)
            {
                if (!StaticFunctions.CheckKeyFields(dr["OrdKeyFields"].ToString(), drOrd, null))
                    continue;

                string[] strSets = dr["OrdKeyValues"].ToString().Split("|".ToCharArray());
                StaticFunctions.SetControlEdit(strSets[0], true, ctrlParent);
                StaticFunctions.SetControlEdit(strSets[1], false, ctrlParent);
            }
        }
        private void DoControlEvent(Control ctrl, string strValue)
        {
            if (Convert.ToString(ctrl.Tag) == string.Empty)
                return;
            if (strValue == string.Empty || strValue == "-9999")
                return;

            DataRow[] drControlSet = dtBtnsM.Select("IsControlSet=1 AND BtnName='" + ctrl.Name.ToString() + "'");
            if (drControlSet.Length <= 0)
                return;

            DataRow dr = GridViewEdit.GetFocusedDataRow();
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
        private void txt_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            DoControlEvent(sender as TextEdit, Convert.ToString(e.NewValue));
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!blSetWeight)
            {
                e.Handled = true;
            }
        }
        public override void SetText(string text)
        {
            if (strFocusedContrName == "gridCInfo")
            {
                if (gridVInfo.FocusedColumn == null)
                    return;

                if (gridVInfo.FocusedColumn.OptionsColumn.ReadOnly)
                    return;

                if (!gridVInfo.FocusedColumn.OptionsColumn.AllowEdit)
                    return;

                DataRow drFoc = gridVInfo.GetFocusedDataRow();
                if (drFoc == null)
                    return;

                string strFieldName = gridVInfo.FocusedColumn.FieldName;
                if ((strGetWFBalceCtrlIds + ",").IndexOf(strFieldName + ",") != -1)
                {
                    drFoc[strFieldName] = StaticFunctions.Round(double.Parse(text), 2, 0.5).ToString();
                    gridVInfo.UpdateCurrentRow();
                }
            }
            else
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
        private void repPic_MouseHover(object sender, EventArgs e)
        {
            DataRow dr = gridVInfo.GetFocusedDataRow();
            if (dr == null)
                return;

            picEdit.EditValue = dr["Icon"]; 
            picEdit.Visible = true;
        }
        private void repPic_MouseLeave(object sender, EventArgs e)
        {
            picEdit.Visible = false;
        }

        private void frmSysInfoAddItem_Load(object sender, EventArgs e)
        {
            DoQuery();
        }
        private void DoQuery()
        {
            List<string> lisSpParmValue = new List<string>();
            string[] strKey = "Key_Ids,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {StrOrdKeyId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     strQueryFlag});
            DataSet dsTemp = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            if (dsTemp == null)
                return;

            frmDataTable = dsTemp.Tables[0];
            frmDataTable.AcceptChanges();
            gridCMain.DataSource = frmDataTable.DefaultView;//可能引发gridView1_FocusedRowChanged
            foreach (Control ctrParent in GcOrdControls.Values)
            {
                StaticFunctions.SetControlBindings(ctrParent, frmDataTable.DefaultView, frmDataTable.Rows[0]);
            }
            dtInfo = dsTemp.Tables[1];
            DataColumn newColumn = dtInfo.Columns.Add("Icon", Type.GetType("System.Byte[]"));
            newColumn.AllowDBNull = true;
            if (drBtn["FrmShowPicEdit"].ToString() == "True")
            {
                foreach (DataRow dr in dtInfo.Rows)
                {
                    dr["Icon"] = ServerRefManager.PicFileRead(dr["StylePic"].ToString(), dr["Pic_Version"].ToString());
                }
            }
            if (IsAddChildGv && dsTemp.Tables.Count > 2)
            {
                dtInfoInfo = dsTemp.Tables[2];
                dtInfo.RowChanging += new DataRowChangeEventHandler(dtInfo_RowChanging);
                dtInfoInfo.RowChanging += new DataRowChangeEventHandler(dtInfoInfo_RowChanging);
                string strRelationsKeyId = drBtn["RelationsKeyId"].ToString();
                dsTemp.Relations.Add("gridCInfoChildGrid", dtInfo.Columns[strRelationsKeyId], dtInfoInfo.Columns[strRelationsKeyId]);
            }
            dtInfo.AcceptChanges();
            gridCInfo.DataSource = dtInfo.DefaultView;
            gridVInfo.BestFitColumns();

            blInitBound = true;
            btnChkItem.Checked = false;
            btnPreview.Checked = false;
            dtInfo.DefaultView.RowFilter = string.Empty;
            blInitBound = false;
        }

        private void dtInfo_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action != DataRowAction.Change)
                return;

            if (dtInfoInfo == null || dtInfoInfo.Rows.Count == 0)
                return;

            if (drBtn["UpdFields"].ToString() == string.Empty)
                return;

            string strRelationsKeyId = drBtn["RelationsKeyId"].ToString();
            string[] strFieldVs = drBtn["UpdFields"].ToString().Split(",".ToCharArray());
            DataRow dr = e.Row;
            DataRow[] drs = dtInfoInfo.Select(strRelationsKeyId + "=" + dr[strRelationsKeyId].ToString());
            if (drs.Length == 1)
            {
                dtInfoInfo.RowChanging -= new DataRowChangeEventHandler(dtInfoInfo_RowChanging);
                DataRow drDest = drs[0];
                for (int i = 0; i < strFieldVs.Length; i++)
                {
                    string strFieldV = strFieldVs[i];
                    if (dtInfoInfo.Columns.Contains(strFieldV))
                        drDest[strFieldV] = dr[strFieldV];
                }
                dtInfoInfo.RowChanging += new DataRowChangeEventHandler(dtInfoInfo_RowChanging);
            }
            else if (dtInfo.Columns.Contains("UkyndaFieldDest") 
                && dtInfo.Columns.Contains("UkyndaFieldSrc") 
                && dtInfo.Columns.Contains("UkyndaEqualFlag"))
            {
                if (dr["UkyndaEqualFlag"].ToString() != "1")
                    return;

                string strFieldDest = dr["UkyndaFieldDest"].ToString();
                string strFieldSrc = dr["UkyndaFieldSrc"].ToString();
                double dSrc = dr[strFieldSrc].ToString() == string.Empty ? 0 : StaticFunctions.Round(double.Parse(dr[strFieldSrc].ToString()), 4, 0.5);
                double dDest = dr[strFieldDest].ToString() == string.Empty ? 0 : StaticFunctions.Round(double.Parse(dr[strFieldDest].ToString()), 4, 0.5);
                bool blSetAll = ((int)(dSrc * 10000) <= (int)(dDest * 10000));

                List<string> lisFields = new List<string>();
                List<string> lisValues = new List<string>();
                string[] strFields = drBtn["SelUpdateFields"].ToString().Split(",".ToCharArray());
                foreach (string strSql in strFields)
                {
                    string[] strFieldVSrc = strSql.Split("=".ToCharArray());
                    lisFields.Add(strFieldVSrc[0]);
                    lisValues.Add(strFieldVSrc[1]);
                }
                dtInfoInfo.RowChanging -= new DataRowChangeEventHandler(dtInfoInfo_RowChanging);
                foreach (DataRow dri in drs)
                {
                    for (int i = 0; i < lisFields.Count; i++)
                    {
                        if (blSetAll)
                            dri[lisFields[i]] = dri[lisValues[i]];
                        else
                            dri[lisFields[i]] = DBNull.Value;
                    }
                }
                dtInfoInfo.RowChanging += new DataRowChangeEventHandler(dtInfoInfo_RowChanging);
            }
        }
        private void dtInfoInfo_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action != DataRowAction.Change)
                return;

            if (drBtn["UpdFields"].ToString() == string.Empty)
                return;

            string strRelationsKeyId = drBtn["RelationsKeyId"].ToString();
            string[] strFieldVs = drBtn["UpdFields"].ToString().Split(",".ToCharArray());
            DataRow dr = e.Row;

            Dictionary<string, int> dicV = new Dictionary<string, int>();
            DataRow[] drs = dtInfoInfo.Select(strRelationsKeyId + "=" + dr[strRelationsKeyId].ToString());
            foreach (DataRow drInfo in drs)
            {
                for (int i = 0; i < strFieldVs.Length; i++)
                {
                    string strFieldV = strFieldVs[i];
                    string strValue = drInfo[strFieldV].ToString();
                    if (strValue == string.Empty || strValue == "0")
                        continue;

                    object snw = new DataTable().Compute(strValue + "*10000", null);
                    int Val = (int)(double.Parse(snw.ToString()));
                    if (Val == 0)
                        continue;

                    if (dicV.ContainsKey(strFieldV))
                    {
                        dicV[strFieldV] += Val;
                    }
                    else
                    {
                        dicV.Add(strFieldV, Val);
                    }
                }
            }
            DataRow[] drPs = dtInfo.Select(strRelationsKeyId + "=" + dr[strRelationsKeyId].ToString());
            dtInfo.RowChanging -= new DataRowChangeEventHandler(dtInfo_RowChanging);
            foreach (DataRow drInfo in drPs)
            {
                for (int i = 0; i < strFieldVs.Length; i++)
                {
                    string strFieldV = strFieldVs[i];
                    if (dicV.ContainsKey(strFieldV))
                    {
                        drInfo[strFieldV] = dicV[strFieldV] / 10000.00;
                    }
                }
            }
            dtInfo.RowChanging += new DataRowChangeEventHandler(dtInfo_RowChanging);
        }

        private void xtabItemInfo_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (blInitBound)
                return;

            if (xtabItemInfo.SelectedTabPage == null)
                return;

            xtabItemInfo.Select();
            string strTabName = xtabItemInfo.SelectedTabPage.Name;
            DataRow[] drs = dtTabs.Select("TabName='" + strTabName + "'");
            if (drs.Length != 1)
                return;

            StaticFunctions.SetFirstEditContrSelect(GcOrdControls[drs[0]["PGCContrName"].ToString()], dtShow);
        }
        private void gridVInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (gridVInfo.FocusedColumn == null)
                return;

            if (gridVInfo.FocusedColumn.OptionsColumn.ReadOnly)
                return;

            if (!gridVInfo.FocusedColumn.OptionsColumn.AllowEdit)
                return;

            DataRow drFoc = gridVInfo.GetFocusedDataRow();
            if (drFoc == null)
                return;

            string strFieldName = gridVInfo.FocusedColumn.FieldName;
            if ((strGetWFBalceCtrlIds + ",").IndexOf(strFieldName + ",") != -1)
            {
                if (!blSetWeight)
                {
                    e.Handled = true;
                }
            }
        }

        private void DoPostEditor()
        {
            blInitBound = true;
            gridCInfo.EmbeddedNavigator.Buttons.DoClick(gridCInfo.EmbeddedNavigator.Buttons.EndEdit);
            blInitBound = false;
        }
        private void btnChkItem_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (blInitBound)
                return;

            if (dtInfo == null)
                return;

            if (dtInfo.Rows.Count == 0)
                return;

            DoChkItem();
        }
        private void DoChkItem()
        {
            DoPostEditor();

            List<string> lisFields = new List<string>();
            List<string> lisValues = new List<string>();
            string strFields = btnChkItem.Checked ? drBtn["SelUpdateFields"].ToString() : drBtn["NoSelUpdateFields"].ToString();
            string[] strFieldVs = strFields.Split(",".ToCharArray());
            foreach (string strSql in strFieldVs)
            {
                string[] strFieldVSrc = strSql.Split("=".ToCharArray());
                lisFields.Add(strFieldVSrc[0]);
                lisValues.Add(strFieldVSrc[1]);
            }
            for (int j = 0; j < gridVInfo.RowCount; j++)
            {
                DataRow dr = gridVInfo.GetDataRow(j);
                for (int i = 0; i < lisFields.Count; i++)
                {
                    dr[lisFields[i]] = dr[lisValues[i]];
                }
            }
            //foreach (DataRow dr in dtInfo.Rows)
            //{
            //    for (int i = 0; i < lisFields.Count; i++)
            //    {
            //        dr[lisFields[i]] = dr[lisValues[i]];
            //    }
            //}
            
        }
        private void btnPreview_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (blInitBound)
                return;

            if (dtInfo == null)
                return;

            DoPostEditor();

            if (btnPreview.Checked)
                dtInfo.DefaultView.RowFilter = drBtn["FrmSaveFilter"].ToString();
            else
                dtInfo.DefaultView.RowFilter = string.Empty;
        }

        private void btn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    case "btnRefresh":
                        DoQuery();
                        break;
                    case "btnQuit":
                        this.Close();
                        this.Dispose();
                        break;
                    case "btnSave":
                        DoOk();
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
                e.Item.Enabled = true;
                e.Item.Refresh();
                blSysProcess = false;
            }
        }
        private void DoOk()
        {
            DataRow dr = frmDataTable.Rows[0];
            foreach (Control ctrParent in GcOrdControls.Values)
            {
                if (!StaticFunctions.CheckSave(dr, ctrParent, dtShow))
                    return;
            }
            DoPostEditor();

            string strSplits = StaticFunctions.GetStringX(strFileds, dtInfo, drBtn["FrmSaveFilter"].ToString());
            if (dtInfo.Rows.Count > 0 && strSplits == string.Empty)
            {
                MessageBox.Show("没有可保存的明细记录.");
                return;
            }
            string strSplitsInfo = string.Empty;
            if (dtInfoInfo != null)
            {
                strSplitsInfo = StaticFunctions.GetStringX(strFiledsInfo, dtInfoInfo, drBtn["FrmSaveFilter"].ToString());
            }
            string strSplitsOrd = StaticFunctions.GetStringX(lstFiledsOrd.ToArray(), frmDataTable);
            string[] strKey = null;
            string[] strVal = null;
            if (strSplitsInfo == string.Empty)
            {
                strKey = "Key_Ids,strSplitOrd,strSplits,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                strVal = new string[] {StrOrdKeyId,strSplitsOrd,strSplits,
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(),
                    strAddFlag };
            }
            else
            {
                strKey = "Key_Ids,strSplitOrd,strSplits,strSplitsInfo,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                strVal = new string[] {StrOrdKeyId,strSplitsOrd,strSplits,strSplitsInfo,
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(),
                    strAddFlag };
            }
            DataSet dsAdd = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            if (dsAdd == null)
            {
                return;
            }
            MessageBox.Show("操作成功.");
            if (drBtn["SucceRefXtabNames"].ToString() != string.Empty)
            {
                Form frmExist = StaticFunctions.GetExistedBsuChildForm(this.ParentForm, ClassNameParent, BusClassNameParent);
                if (frmExist != null && frmExist is frmSysBusOPCenter)
                {
                    (frmExist as frmSysBusOPCenter).UpdateData(drBtn["SucceRefXtabNames"].ToString());
                }
            }

            if (drBtn["FormBsuClass"].ToString() != string.Empty && drBtn["FormMenus_Class"].ToString() != string.Empty)
            {
                StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", this.ParentForm, drBtn["FormCaption"].ToString(),
                    drBtn["FormBsuClass"].ToString(), drBtn["FormMenus_Class"].ToString(), "VIEW",
                    "KeyId=" + dsAdd.Tables[0].Rows[0][strKeyFiled].ToString() + "&BusClassName=" + drBtn["FormMenus_Class"].ToString(), null);
            }
            else if (drBtn["IsPrintTicket"].ToString() == "True")
            {
                DoPrintTicketInfoSpecial(drBtn, dsAdd.Tables[0]);
            }

            //生产加工单条码图片并上传
            if (dsAdd.Tables[0].Columns.IndexOf("BarcodeFileName") != -1
                && dsAdd.Tables[0].Columns.IndexOf("BarcodeNumber") != -1)
            {
                drAddTemp = dsAdd.Tables[0].Rows[0];
                tdCreateBarcodeImage = new Thread(new ThreadStart(CreateBarcodeImage));
                tdCreateBarcodeImage.Start();
            }
            this.Close();
            this.Dispose();
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
        private void DoPrintTicketInfoSpecial(DataRow drBtn, DataTable dtPrint)
        {
            try
            {
                BarTender.Application btdb = new BarTender.Application();
                string strPrintPath = Application.StartupPath + @"\打印模板\" + drBtn["TicketTempName"].ToString() + ".btw";
                try
                {
                    foreach (DataRow drPrint in dtPrint.Rows)
                    {
                        StaticFunctions.PrintItem(btdb, drPrint, strPrintPath, strKeyFiled, frmImageFilePath);
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
}