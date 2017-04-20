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

namespace ProduceManager
{
    public partial class frmBatchEdit : frmEditorBase
    {
        #region private Params
        private DataSet dsLoad = null;
        private DataSet dsFormAdt = null;
        private DataSet dsFormUkyndaAdt = null;
        private DataTable dtConst = null;
        private DataTable dtShow = null;
        private DataRow drSet = null;
        private DataTable dtContr = null;
        private string strFormName = string.Empty;
        private string strShareSpName = "Share_Table_Op";
        public string[] strFileds
        {
            get;
            set;
        }
        public string StrUpdKeyIds
        {
            get;
            set;
        }
        public string StrUpdSpName
        {
            get;
            set;
        }
        public DataRow DrBtn
        {
            get;
            set;
        }
        public DataTable DtRets
        {
            get;
            set;
        }
        public DataTable DtSps
        {
            get;
            set;
        }
        public List<DataRow> DrBatchs
        {
            get;
            set;
        }
        #endregion

        public frmBatchEdit(string strFormName)
        {
            InitializeComponent();
            this.strFormName = strFormName;
            InitContr();
        }
        private void InitContr()
        {
            if (dsLoad != null)
                return;

            dsFormUkyndaAdt = this.GetFrmLoadUkyndaDsAdt(strFormName);
            dsFormUkyndaAdt.AcceptChanges();
            dsFormAdt = this.GetFrmLoadDsAdt(strFormName);
            dsFormAdt.AcceptChanges();
            dsLoad = this.GetFrmLoadDsNew(strFormName);
            dsLoad.AcceptChanges();
            dtShow = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];
            dtContr = dsLoad.Tables[7];
            int iGcH;
            List<Control> lisGcContrs = StaticFunctions.ShowGroupControl(gcSet, this.Width - 30, dtShow, dtConst, false, 30, false, null, true, out iGcH);
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
                DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, dpl.Name, dpl.Parent.Name);
                StaticFunctions.UpdateDataRowSynLookUpEdit(drSet, dpl, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString());
            }
            else if (sender is ExtendControl.ExtPopupTree)
            {
                ExtendControl.ExtPopupTree ept = sender as ExtendControl.ExtPopupTree;
                DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, ept.Name, ept.Parent.Name);
                StaticFunctions.UpdateDataRowSynExtPopupTree(drSet, ept, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString());
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
            ProduceManager.UcTxtPopup ucp = sender as ProduceManager.UcTxtPopup;
            DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, ucp.Name, ucp.Parent.Name);
            StaticFunctions.UpdateDataRowSynUcTxtPopup(drSet, drReturn, drContrl["SetSynFields"].ToString(), drContrl["SetSynSrcFields"].ToString(), ucp);
        }
        private void uct_onClosePopUp(object sender)
        {
            ProduceManager.UcTreeList ucp = sender as ProduceManager.UcTreeList;
            DataRow drContrl = StaticFunctions.GetContrRowValueById(dtShow, ucp.Name, ucp.Parent.Name);
            StaticFunctions.UpdateDataRowSynUcTreeList(drSet, drContrl["SetSynFields"].ToString(), ucp);
        }

        private void frmSelectShop_Load(object sender, EventArgs e)
        {
            drSet = frmDataTable.Rows.Add();
            StaticFunctions.SetControlBindings(gcSet, this.frmDataTable.DefaultView as DataView, drSet);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!StaticFunctions.CheckSave(gcSet, dtShow))
                return;

            strFileds = StaticFunctions.GetUpdateFields(gcSet, dtShow);
            if (string.IsNullOrEmpty(StrUpdKeyIds))
            {
                this.DialogResult = DialogResult.Yes;
                return;
            }

            string strValues = string.Empty;
            DataRow drEdit = frmDataTable.Rows[0];
            foreach (string strFiled in strFileds)
            {
                if (drEdit[strFiled] == DBNull.Value
                    || drEdit[strFiled].ToString() == string.Empty
                    || drEdit[strFiled].ToString().Trim() == "-9999")
                    continue;

                strValues += strValues == string.Empty ? strFiled + "='" + drEdit[strFiled].ToString().Replace("'", "''") + "'" : "," + strFiled + "='" + drEdit[strFiled].ToString().Replace("'", "''") + "'";
            }
            if (strValues == string.Empty)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
                return;
            }
            DataTable dtAdd = null;
            List<string> lisSpParmValue = new List<string>();
            string strBatchEditSpFlag = DrBtn["BatchEditSpFlag"].ToString();
            DataRow[] drShares = DtSps.Select("SpName='" + StrUpdSpName + "' AND SpFlag='" + strBatchEditSpFlag + "'");
            if (drShares.Length == 0)
            {
                string[] strKey = "strEditSql,Key_Ids,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                lisSpParmValue.AddRange(new string[] { 
                    strValues,
                    StrUpdKeyIds,
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(),
                    strBatchEditSpFlag});
                dtAdd = this.DataRequest_By_DataTable(StrUpdSpName, strKey, lisSpParmValue.ToArray());
                if (dtAdd == null)
                {
                    return;
                }
            }
            else
            {
                string[] strKey = "BsuSetSp_Id,strEditSql,Key_Ids,EUser_Id,EDept_Id,Fy_Id".Split(",".ToCharArray());
                lisSpParmValue.AddRange(new string[] { drShares[0]["BsuSetSp_Id"].ToString(),
                    strValues,
                    StrUpdKeyIds,
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString()});
                dtAdd = this.DataRequest_By_DataTable(strShareSpName, strKey, lisSpParmValue.ToArray());
                if (dtAdd == null)
                {
                    return;
                }
            }
            DtRets = dtAdd;
            foreach (DataRow dr in DrBatchs)
            {
                foreach (DataColumn dc in frmDataTable.Columns)
                {
                    string strFiled = dc.ColumnName;
                    if (drEdit[strFiled] == DBNull.Value
                        || drEdit[strFiled].ToString() == string.Empty
                        || drEdit[strFiled].ToString().Trim() == "-9999")
                        continue;

                    dr[strFiled] = drEdit[strFiled];
                }
                if (dtAdd.Columns.Contains("UpdateFields"))
                {
                    DataRow drNew = dtAdd.Rows[0];
                    StaticFunctions.UpdateDataRowSyn(dr, drNew, drNew["UpdateFields"].ToString());
                }
            }
            this.DialogResult = DialogResult.Yes;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            CApplication.App.CurrentSession.TimerId = 0;
            int k = msg.WParam.ToInt32();
            if (k == 27)//Esc
            {
                this.DialogResult = DialogResult.No;
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}