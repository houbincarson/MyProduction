using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;

namespace ProduceManager
{
    public partial class frmSysKindManger : frmEditorBase
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

        private DataSet dsFormAdt = null;
        private DataSet dsFormUkyndaAdt = null;
        private DataRow drMain = null;

        private string strShareSpName = "Share_Table_Op";
        private string strSpName = string.Empty;
        private string strKeyFiled = string.Empty;
        private string strParentKeyFiled = string.Empty;
        private string strKind_Level = string.Empty;
        private string strBusClassName = string.Empty;
        private string strAllowList = string.Empty;

        private string strNoEnableCtrIds = string.Empty;
        private string StrNoEnableEditCtrIds = string.Empty;
        private Control CtrFirstEditContr = null;
        private Control CtrFirstEditFocusContr = null;
        private string[] strFileds = null;
        private bool blSetDefault = false;
        #endregion

        public frmSysKindManger()
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
                if (!blPrevFindControl)
                {
                    SetContrMoveNext(dpl.Name, false);
                }

                if (this.frmEditorMode == "VIEW")
                    return;

                TreeListNode nodeSel = treeList1.FocusedNode;
                if (nodeSel == null)
                    return;

                DataRow drInfo = (treeList1.GetDataRecordByNode(treeList1.FocusedNode) as DataRowView).Row;
                DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, dpl.Name, dpl.Parent.Name);
                StaticFunctions.UpdateDataRowSynLookUpEdit(drInfo, dpl, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString());
            }
            else if (sender is ExtendControl.ExtPopupTree)
            {
                if (this.frmEditorMode == "VIEW")
                    return;

                TreeListNode nodeSel = treeList1.FocusedNode;
                if (nodeSel == null)
                    return;

                DataRow drInfo = (treeList1.GetDataRecordByNode(treeList1.FocusedNode) as DataRowView).Row;

                ExtendControl.ExtPopupTree ept = sender as ExtendControl.ExtPopupTree;
                DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, ept.Name, ept.Parent.Name);
                StaticFunctions.UpdateDataRowSynExtPopupTree(drInfo, ept, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString());
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
            if (this.frmEditorMode == "VIEW")
                return;

            TreeListNode nodeSel = treeList1.FocusedNode;
            if (nodeSel == null)
                return;

            DataRow drInfo = (treeList1.GetDataRecordByNode(treeList1.FocusedNode) as DataRowView).Row;

            ProduceManager.UcTxtPopup ucp = sender as ProduceManager.UcTxtPopup;
            if (!blPrevFindControl)
            {
                SetContrMoveNext(ucp.Name, false);
            }
            DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, ucp.Name, ucp.Parent.Name);
            StaticFunctions.UpdateDataRowSynUcTxtPopup(drInfo, drReturn, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString(), ucp);
        }
        private void uct_onClosePopUp(object sender)
        {
            if (this.frmEditorMode == "VIEW")
                return;

            TreeListNode nodeSel = treeList1.FocusedNode;
            if (nodeSel == null)
                return;

            DataRow drInfo = (treeList1.GetDataRecordByNode(treeList1.FocusedNode) as DataRowView).Row;

            ProduceManager.UcTreeList ucp = sender as ProduceManager.UcTreeList;
            if (!blPrevFindControl)
            {
                SetContrMoveNext(ucp.Name, false);
            }
            DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, ucp.Name, ucp.Parent.Name);
            StaticFunctions.UpdateDataRowSynUcTreeList(drInfo, drContrl["SetSynFields"].ToString(), ucp);
        }
        private void Txt_Enter(object sender, EventArgs e)
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

            RefreshItem();
            SetWMode("VIEW");
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

            StaticFunctions.ShowGridControl(treeList1, dtShow, dtConst);
            string[] strKeyIdFileds = drMain["KeyIdFiled"].ToString().Split("|".ToCharArray());
            if (strKeyIdFileds.Length != 3)
            {
                MessageBox.Show("主表主键必须以下列方式设置：Kind_Id|Parent_Kind_Id|Kind_Level");
                return;
            }
            this.treeList1.KeyFieldName = strKeyIdFileds[0];
            this.treeList1.ParentFieldName = strKeyIdFileds[1];
            strKeyFiled = this.treeList1.KeyFieldName;
            strParentKeyFiled = this.treeList1.ParentFieldName;
            strKind_Level = strKeyIdFileds[2];
            strSpName = drMain["SpName"].ToString();

            splitContainerControl2.SplitterPosition = int.Parse(drMain["MainSplitterPosition"].ToString());

            ParentControl = gcInfo;
            BtnEnterSave = btnSave;
            Rectangle rect = SystemInformation.VirtualScreen;
            int iGcW = rect.Width - 30 - splitContainerControl2.SplitterPosition;
            #region gcInfo
            List<Control> lisGcContrs = StaticFunctions.ShowGcContrs(gcInfo, iGcW, dtShow, dtConst, true, 50, true, arrContrSeq, false
                , out blSetDefault, out strNoEnableCtrIds, out strFileds, out CtrFirstEditContr);
            StrNoEnableEditCtrIds = StaticFunctions.GetReadOnlyEditIds(dtShow, gcInfo.Name);
            CtrFirstEditFocusContr = StaticFunctions.GetFirstEditFocusContr(gcInfo, dtShow);
            foreach (Control ctrl in lisGcContrs)
            {
                ctrl.Enter += new System.EventHandler(this.Txt_Enter);
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
        }
        public override void RefreshItem()
        {
            GetCurrAllItem();
        }
        public override void GetCurrAllItem()
        {
            DataSet dtAdd = null;
            DataRow[] drShares = dtSp.Select("SpName='" + strSpName + "' AND SpFlag='" + drMain["QueryFlag"].ToString() + "'");
            List<string> lisSpParmValue = new List<string>();
            if (drShares.Length == 0)
            {
                string[] strKey = "EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                lisSpParmValue.AddRange(new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     drMain["QueryFlag"].ToString()});
                dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            }
            else
            {
                string[] strKey = "BsuSetSp_Id,EUser_Id,EDept_Id,Fy_Id".Split(",".ToCharArray());
                lisSpParmValue.AddRange(new string[] {drShares[0]["BsuSetSp_Id"].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString()});
                dtAdd = this.DataRequest_By_DataSet(strShareSpName, strKey, lisSpParmValue.ToArray());
            }
            if (dtAdd == null)
                return;

            frmDataTable = dtAdd.Tables[0];
            frmDataTable.AcceptChanges();
            blInitBound = true;
            treeList1.DataSource = frmDataTable.DefaultView;
            blInitBound = false;
            treeList1_FocusedNodeChanged(treeList1, new DevExpress.XtraTreeList.FocusedNodeChangedEventArgs(null, treeList1.FocusedNode));
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (blInitBound)
                return;

            if (e.OldNode != null)
            {
                DataRow drP = (treeList1.GetDataRecordByNode(e.OldNode) as DataRowView).Row;
                if (drP != null && drP.RowState != DataRowState.Unchanged)
                {
                    blInitBound = true;
                    drP.RejectChanges();//引发gridView1_FocusedRowChanged
                    blInitBound = false;
                }
            }
            if (e.Node != null)
            {
                DataRow dr = (treeList1.GetDataRecordByNode(e.Node) as DataRowView).Row;
                if (dr == null)
                    return;

                if (dr.RowState != DataRowState.Added && frmEditorMode != "VIEW")
                {
                    SetWMode("VIEW");
                }
                StaticFunctions.SetControlBindings(gcInfo, treeList1.DataSource as DataView, dr);
                SetFocRowstyleFormat(dr);
            }
        }
        private void SetFocRowstyleFormat(DataRow dr)
        {
            if (dr.Table.Columns.Contains("State") && dr["State"].ToString() == "False")
            {
                treeList1.Appearance.FocusedCell.BackColor = System.Drawing.Color.Coral;
                treeList1.Appearance.FocusedCell.Options.UseBackColor = true;
                treeList1.Appearance.FocusedRow.BackColor = System.Drawing.Color.Coral;
                treeList1.Appearance.FocusedRow.Options.UseBackColor = true;
            }
            else
            {
                treeList1.Appearance.FocusedCell.Options.UseBackColor = false;
                treeList1.Appearance.FocusedRow.Options.UseBackColor = false;
            }
        }
        public override void SetWMode(string strMode)
        {
            this.frmEditorMode = strMode;
            switch (strMode)
            {
                case "VIEW":
                    StaticFunctions.SetBtnEnabled(new Component[] { btnEdit, btnAdd, btnAddSub }, true);
                    StaticFunctions.SetBtnEnabled(new Component[] { btnCancel, btnSave }, false);
                    StaticFunctions.SetControlEnable(gcInfo, false);
                    break;

                case "ADD":
                    StaticFunctions.SetBtnEnabled(new Component[] { btnEdit, btnAdd, btnAddSub }, false);
                    StaticFunctions.SetBtnEnabled(new Component[] { btnCancel, btnSave }, true);
                    StaticFunctions.SetControlEnable(gcInfo, true, strNoEnableCtrIds);
                    StaticFunctions.SetFirstEditContrSelect(CtrFirstEditContr);
                    break;

                case "EDIT":
                    StaticFunctions.SetBtnEnabled(new Component[] { btnEdit, btnAdd, btnAddSub }, false);
                    StaticFunctions.SetBtnEnabled(new Component[] { btnCancel, btnSave }, true);
                    StaticFunctions.SetControlEnable(gcInfo, true, StrNoEnableEditCtrIds);
                    StaticFunctions.SetFirstEditContrSelect(CtrFirstEditFocusContr);
                    break;

                default:
                    break;
            }
        }
        private void btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;
            TreeListNode nodeSel = treeList1.FocusedNode;
            DataRow dr = null;
            if (nodeSel != null)
            {
                dr = (treeList1.GetDataRecordByNode(treeList1.FocusedNode) as DataRowView).Row;
            }
            switch (btn.Name)
            {
                case "btnAdd":
                    SetWMode("ADD");
                    DataRow drNew = this.frmDataTable.NewRow();
                    if (blSetDefault)
                    {
                        StaticFunctions.SetContrDefaultValue(gcInfo, dtShow, drNew);
                    }
                    drNew[strKeyFiled] = -1;
                    if (dr == null)
                    {
                        drNew[strKind_Level] = 1;
                        drNew[strParentKeyFiled] = 0;
                    }
                    else
                    {
                        drNew[strKind_Level] = dr[strKind_Level];
                        drNew[strParentKeyFiled] = dr[strParentKeyFiled];
                    }

                    blInitBound = true;
                    TreeListNode nodeA;
                    if (dr == null)
                    {
                        nodeA = treeList1.AppendNode(drNew, 0);
                    }
                    else
                    {
                        nodeA = treeList1.AppendNode(drNew, nodeSel.ParentNode);
                    }
                    treeList1.SetFocusedNode(nodeA);
                    blInitBound = false;
                    treeList1_FocusedNodeChanged(treeList1, new DevExpress.XtraTreeList.FocusedNodeChangedEventArgs(null, treeList1.FocusedNode));
                    break;

                case "btnAddSub":
                    if (dr == null)
                        return;

                    SetWMode("ADD");
                    DataRow drNewS = this.frmDataTable.NewRow();
                    if (blSetDefault)
                    {
                        StaticFunctions.SetContrDefaultValue(gcInfo, dtShow, drNewS);
                    }
                    drNewS[strKeyFiled] = -1;
                    drNewS[strKind_Level] = int.Parse(dr[strKind_Level].ToString()) + 1;
                    drNewS[strParentKeyFiled] = dr[strKeyFiled];

                    blInitBound = true;
                    TreeListNode nodeAS = treeList1.AppendNode(drNewS, nodeSel);
                    nodeSel.ExpandAll();
                    treeList1.SetFocusedNode(nodeAS);
                    blInitBound = false;
                    treeList1_FocusedNodeChanged(treeList1, new DevExpress.XtraTreeList.FocusedNodeChangedEventArgs(null, treeList1.FocusedNode));
                    break;

                case "btnEdit":
                    if (dr == null)
                    {
                        return;
                    }
                    SetWMode("EDIT");
                    break;

                case "btnCancel":
                    if (dr == null)
                    {
                        return;
                    }
                    blInitBound = true;
                    frmDataTable.RejectChanges();//引发gridView1_FocusedRowChanged
                    frmDataTable.AcceptChanges();
                    blInitBound = false;
                    SetWMode("VIEW");
                    treeList1_FocusedNodeChanged(treeList1, new DevExpress.XtraTreeList.FocusedNodeChangedEventArgs(null, treeList1.FocusedNode));
                    break;

                case "btnSave":
                    if (dr == null)
                    {
                        return;
                    }
                    DoSave();
                    break;

                default:
                    break;
            }
        }
        private void DoSave()
        {
            DataRow dr = (treeList1.GetDataRecordByNode(treeList1.FocusedNode) as DataRowView).Row;
            if (dr == null)
                return;

            if (!StaticFunctions.CheckSave(dr, gcInfo, dtShow))
                return;

            string strField = string.Empty;
            string strValues = string.Empty;
            btnSave.Enabled = false;
            bool blChgState = false;

            try
            {
                if (dr[strKeyFiled].ToString() == string.Empty || dr[strKeyFiled].ToString() == "-1")
                {
                    strValues = StaticFunctions.GetAddValues(dr, strFileds, out strField);

                    DataSet dtAdd = null;
                    string strSpParmName = string.Empty;
                    List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(gcInfo, dtShow, out strSpParmName);
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
                    strValues = StaticFunctions.GetUpdateValues(frmDataTable, dr, strFileds);
                    if (strValues != string.Empty)
                    {
                        DataSet dsAdd = null;
                        string strSpParmName = string.Empty;
                        List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(gcInfo, dtShow, out strSpParmName);
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
                            dsAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
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
                            dsAdd = this.DataRequest_By_DataSet(strShareSpName, strKey, lisSpParmValue.ToArray());
                        }
                        if (dsAdd == null)
                        {
                            btnSave.Enabled = true;
                            return;
                        }
                        if (dsAdd.Tables.Count > 0 && dsAdd.Tables[0].Columns.Contains("UpdateFields"))
                        {
                            DataRow drNew = dsAdd.Tables[0].Rows[0];
                            StaticFunctions.UpdateDataRowSyn(dr, drNew, drNew["UpdateFields"].ToString());
                        }
                        if (dr.Table.Columns.Contains("State") && dr["State"].ToString() != dr["State", DataRowVersion.Original].ToString())
                        {
                            blChgState = true;
                        }
                    }
                }
                dr.AcceptChanges();
                SetWMode("VIEW");
                if (blChgState)
                    SetFocRowstyleFormat(dr);
            }
            catch (Exception err)
            {
                MessageBox.Show("错误:" + err.Message);
                btnSave.Enabled = true;
            }
        }
    }
}
