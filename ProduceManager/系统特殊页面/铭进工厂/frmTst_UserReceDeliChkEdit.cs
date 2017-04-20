using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace ProduceManager
{
    public partial class frmTst_UserReceDeliChkEdit : frmEditorBase
    {
        private DataSet dsLoad = null;
        private DataTable dtDept = null;
        private DataTable dtConst = null;
        private bool blInitBound = false;
        private string strDeptNumber = string.Empty;

        private string strFocusedContrFlag = string.Empty;
        private List<string> arrContrSeqF = new List<string>(new string[] { "txtNetWeight", "txtAmount", "ucGoods_TypeR", "txtRemark", "txtRNetWExpre", "btnWSave" });
        private List<string> arrContrSeqT = new List<string>(new string[] { "txtDRIndex", "txtDNetWeight", "txtDAmount", "ucGoods_TypeD", "dplDeliType", "ucULossRepTypeD", "txtDRemark", "txtDNetWExpre", "btnDSave" });
        private List<string> arrContrSeqS = new List<string>(new string[] { "txtSRIndex", "btnFilter" });

        private DataTable dtDeli = null;
        private DataTable dtRece = null;
        private DataTable dtSLoss = null;
        private string strDMode = "VIEW";
        private string strRMode = "VIEW";
        private string strUserId = string.Empty;
        private DataRow drReceDeliUser = null;
        private DataRow drPOrdNumber = null;
        private DataTable dtURDSetTst = null;
        private bool IsPrint = true;
        private bool blSetWeight = false;

        public frmTst_UserReceDeliChkEdit()
        {
            InitializeComponent();
        }
        //电子称
        private void Txt_Enter(object sender, EventArgs e)
        {
            strFocusedContrName = (sender as Control).Name;
            FocusedControl = sender as Control;
            if (strFocusedContrFlag != "1")
            {
                strFocusedContrFlag = "1";
                ParentControl = gcRece;
                GridViewEdit = gridVRece;
                arrContrSeq = arrContrSeqF;

                gridVDeli.ClearSelection();
                gridVSLoss.ClearSelection();
                gridVRece.ClearSelection();
                gridVRece.SelectRow(gridVRece.FocusedRowHandle);
            }
            if (strDMode != "VIEW")
            {
                btnDCancel_Click(null, null);
            }
        }
        private void TxtT_Enter(object sender, EventArgs e)
        {
            strFocusedContrName = (sender as Control).Name;
            FocusedControl = sender as Control;

            if (strFocusedContrFlag != "2")
            {
                strFocusedContrFlag = "2";
                ParentControl = gcDeli;
                GridViewEdit = gridVDeli;
                arrContrSeq = arrContrSeqT;

                gridVRece.ClearSelection();
                gridVSLoss.ClearSelection();
                gridVDeli.ClearSelection();
                gridVDeli.SelectRow(gridVDeli.FocusedRowHandle);
            }
            if (strRMode != "VIEW")
            {
                btnWCancel_Click(null, null);
            }
        }
        private void TxtS_Enter(object sender, EventArgs e)
        {
            strFocusedContrName = (sender as Control).Name;
            FocusedControl = sender as Control;

            if (strFocusedContrFlag != "3")
            {
                strFocusedContrFlag = "3";
                ParentControl = gcSum;
                GridViewEdit = gridVSLoss;
                arrContrSeq = arrContrSeqS;

                gridVRece.ClearSelection();
                gridVDeli.ClearSelection();
                gridVSLoss.ClearSelection();
                gridVSLoss.SelectRow(gridVSLoss.FocusedRowHandle);
            }
            if (strDMode != "VIEW")
            {
                btnDCancel_Click(null, null);
            }
            if (strRMode != "VIEW")
            {
                btnWCancel_Click(null, null);
            }
        }
        public override void SetText(string text)
        {
            TextEdit txt = FocusedControl as TextEdit;
            if (txt == null || txt.Properties.ReadOnly)
                return;

            double d = double.Parse(text);
            switch (strFocusedContrName)
            {
                case "txtNetWeight":
                    txtNetWeight.Text = StaticFunctions.Round(d, 2, 0.3).ToString();
                    break;
                case "txtRNetWExpre":
                    txtRNetWExpre.Text = StaticFunctions.Round(d, 2, 0.3).ToString();
                    break;

                case "txtDNetWeight":
                    txtDNetWeight.Text = StaticFunctions.Round(d, 2, 0.8).ToString();
                    break;
                case "txtDNetWExpre":
                    txtDNetWExpre.Text = StaticFunctions.Round(d, 2, 0.8).ToString();
                    break;

                case "txtUNumber":
                    txtUNumber.Text = text;
                    txtUNumber_KeyPress(null, new KeyPressEventArgs('\r'));
                    break;

                default:
                    break;
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

                if (!blPrevFindControl)
                {
                    SetContrMoveNext(dpl.Name, false);
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
        private void ucULossRepTypeD_onClosePopUp(object sender, DataRow drReturn)
        {
            ProduceManager.UcTxtPopup ucp = sender as ProduceManager.UcTxtPopup;
            txtProdLossD.Text = drReturn["ProdLoss"].ToString();
            if (!blPrevFindControl)
            {
                SetContrMoveNext(ucp.Name, false);
            }
        }
        private void ucGoods_TypeR_onClosePopUp(object sender, DataRow drReturn)
        {
            ProduceManager.UcTxtPopup ucp = sender as ProduceManager.UcTxtPopup;
            if (!blPrevFindControl)
            {
                SetContrMoveNext(ucp.Name, false);
            }
        }
        private void ucGoods_TypeD_onClosePopUp(object sender, DataRow drReturn)
        {
            if (drReturn["Is_Powder"].ToString() == "True")
                dplDeliType.EditValue = "3";
            else
                dplDeliType.EditValue = "1";

            ProduceManager.UcTxtPopup ucp = sender as ProduceManager.UcTxtPopup;
            if (!blPrevFindControl)
            {
                SetContrMoveNext(ucp.Name, false);
            }
        }

        private void frmTst_UserReceDeliChkEdit_Load(object sender, EventArgs e)
        {
            InitContr();
            SetContrVisible();

            blSetWeight = GetUserSetWeightfrmCalsses(this.Name);
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

        private void InitContr()
        {
            if (dsLoad != null)
                return;
            string[] strKey="Form,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
            dsLoad = this.DataRequest_By_DataSet("Get_Form_Load_Table",strKey ,
                new string[] { this.Name
                    , CApplication.App.CurrentSession.UserId.ToString()
                    , CApplication.App.CurrentSession.DeptId.ToString()
                    , CApplication.App.CurrentSession.FyId.ToString() });
            dsLoad.AcceptChanges();
            this.dtDept = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];

            //StaticFunctions.BindDplComboByTable(extPopupTree1, dtKind, "Kind_Name", "Kind_Id|Parent_Kind_Id", "Kind_Key=400",
            //  new string[] { "Kind_Key=60", "Kind_Name=160" },
            //  new string[] { "编号", "名称" }, "Kind_Id", "Kind_Level>1", "Kind_Key", "Kind_Id", "Parent_Kind_Id");

            StaticFunctions.BindDplComboByTable(ucGoods_TypeR, dsLoad.Tables[2], "Name", "BseGTpyeId"
                , new string[] { "Number", "Name", "PY_Name" }
                , new string[] { "编号", "名称", "拼音" }
                , new string[] { "Number", "Name", "PY_Name" }
                , string.Empty, "-9999", string.Empty, new Point(240, 180), false);

            StaticFunctions.BindDplComboByTable(ucGoods_TypeD, dsLoad.Tables[3], "Name", "BseGTpyeId"
                , new string[] { "Number", "Name", "PY_Name" }
                , new string[] { "编号", "名称", "拼音" }
                , new string[] { "Number", "Name", "PY_Name" }
                , string.Empty, "-9999", string.Empty, new Point(240, 180), false);

            StaticFunctions.BindDplComboByTable(ucULossRepTypeD, dsLoad.Tables[4], "ULossName", "UDeptLossId"
                , new string[] { "ULossName", "Py_Name", "ProdLoss" }
                , new string[] { "名称", "拼音", "损耗率" }
                , new string[] { "ULossName", "Py_Name", "ProdLoss" }
                , string.Empty, "-9999", string.Empty, new Point(240, 180), true);

            StaticFunctions.BindDplComboByTable(dplRDept, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=2", false);

            StaticFunctions.BindDplComboByTable(repRTst, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=2", true);

            StaticFunctions.BindDplComboByTable(repDDept, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=2", true);

            StaticFunctions.BindDplComboByTable(repChkTst, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=2", true);

            StaticFunctions.BindDplComboByTable(dplDeliType, dtConst, "Name", "SetValue",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "SetKey='DeliType'", false);

            StaticFunctions.BindDplComboByTable(repDeliType, dtConst, "Name", "SetValue",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "SetKey='DeliType'", true);

            DataRow[] drs = dtDept.Select("Dept_Id=" + CApplication.App.CurrentSession.DeptId);
            if (drs.Length > 0)
                strDeptNumber = drs[0]["Number"].ToString();           
        }
        private void SetContrVisible()
        {
            DataRow[] drNoVisIds = dtConst.Select("SetKey='UserReceDeli_NoVisIds'");
            if (drNoVisIds.Length <= 0)
                return;

            string[] strIds = drNoVisIds[0]["SetValue"].ToString().Split(",".ToCharArray());
            foreach (string strId in strIds)
            {
                Control[] ctrs = this.Controls.Find(strId, true);
                if (ctrs.Length > 0)
                {
                    ctrs[0].Visible = false;
                }
            }
        }

        public override void InitialByParam(string Mode, string strParam, DataTable dt)
        {
            base.InitialByParam(Mode, strParam, dt);
            InitContr();

            dtAccountDt.EditValue = null;
            ckbChg.Checked = false;

            dtDeli = null;
            dtRece = null;
            dtSLoss = null;
            gridCRece.DataSource = null;
            gridCDeli.DataSource = null;
            gridCSLoss.DataSource = null;
            gridVRece.FocusedRowHandle = -1;
            gridVDeli.FocusedRowHandle = -1;
            gridVSLoss.FocusedRowHandle = -1;
            strUserId = string.Empty;
            drReceDeliUser = null;
            txtUNumber.Text = string.Empty;
            txtUName.Text = string.Empty;
            txtUDept.Text = string.Empty;
            drPOrdNumber = null;
            txtPONumber.Text = string.Empty;
            txtPOrdNumberRkm.Text = string.Empty;

            SetRMode("VIEW");
            SetDMode("VIEW");

            StaticFunctions.SetControlEmpty(gcRece);
            StaticFunctions.SetControlEmpty(gcDeli);
            StaticFunctions.SetControlEmpty(gcSum);

            strFocusedContrFlag = "1";
            ParentControl = gcRece;
            GridViewEdit = gridVRece;
            arrContrSeq = arrContrSeqF;

            txtUNumber.Properties.ReadOnly = false;
            txtUNumber.Focus();
            this.txtPONumber.Properties.ReadOnly = false;        
        }
        private void btnReSet_Click(object sender, EventArgs e)
        {
            drPOrdNumber = null;
            txtPONumber.Text = string.Empty;
            txtPOrdNumberRkm.Text = string.Empty;
            txtPONumber.Properties.ReadOnly = false;
            txtPONumber.Select();
        }

        private void SetRMode(string strMode)
        {
            this.frmEditorMode = strMode;
            this.strRMode = strMode;
            switch (strMode)
            {
                case "VIEW":
                    btnWAdd.Enabled = true;
                    btnWSave.Enabled = false;
                    StaticFunctions.SetControlEnable(gcRece, false);
                    dtAccountDt.Properties.ReadOnly = false;
                    this.ckbChg.Properties.ReadOnly = false;
                    break;

                case "ADD":
                    StaticFunctions.SetControlEmpty(gcRece);

                    dplRDept.EditValue = CApplication.App.CurrentSession.DeptId;
                    btnWAdd.Enabled = false;
                    btnWSave.Enabled = true;
                    StaticFunctions.SetControlEnable(gcRece, true);
                    dplRDept.Properties.ReadOnly = true;
                    txtRIndex.Properties.ReadOnly = true;

                    txtNetWeight.Focus();
                    break;

                case "EDIT":
                    break;

                default:
                    break;
            }
        }
        private void SetDMode(string strMode)
        {
            this.frmEditorMode = strMode;
            this.strDMode = strMode;
            switch (strMode)
            {
                case "VIEW":
                    btnDAdd.Enabled = true;
                    btnDSave.Enabled = false;
                    StaticFunctions.SetControlEnable(gcDeli, false);
                    dtAccountDt.Properties.ReadOnly = false;
                    this.ckbChg.Properties.ReadOnly = false;
                    break;

                case "ADD":
                    btnDAdd.Enabled = false;
                    btnDSave.Enabled = true;
                    StaticFunctions.SetControlEmpty(gcDeli);
                    StaticFunctions.SetControlEnable(gcDeli, true);
                    txtDIndex.Properties.ReadOnly = true;
                    txtProdLossD.Properties.ReadOnly = true;
                    txtProdLossD.Text = "0";
                    ucULossRepTypeD.EditValue = null;
                    ucGoods_TypeD.EditValue = null;

                    this.txtDRIndex.Focus();
                    txtDRIndex.SelectAll();
                    break;

                case "EDIT":
                    break;

                default:
                    break;
            }           
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1 || blInitBound)
                return;

            if (strRMode != "VIEW")
                SetRMode("VIEW");

            DataRow dr = gridVRece.GetFocusedDataRow();
            if (dr != null)
            {
                StaticFunctions.SetDataRow2ControlValue(gcRece, dr);
            }
            else
            {
                StaticFunctions.SetControlEmpty(gcRece);
            }
        }
        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1 || blInitBound)
                return;

            if (strDMode != "VIEW")
                SetDMode("VIEW");

            DataRow dr = gridVDeli.GetFocusedDataRow();
            if (dr != null)
            {
                StaticFunctions.SetDataRow2ControlValue(gcDeli, dr);
            }
            else
            {
                StaticFunctions.SetControlEmpty(gcDeli);
            }
        }

        private void txtUNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r' || txtUNumber.Properties.ReadOnly ||
                txtUNumber.Text == string.Empty)
                return;

            string[] strKey="Number,Fy_Id".Split(",".ToCharArray());
            DataTable dtPm = this.DataRequest_By_DataTable("Get_Bse_User",strKey ,
                new string[] { txtUNumber.Text.Trim(), CApplication.App.CurrentSession.FyId.ToString() });
            dtPm.AcceptChanges();
            if (dtPm.Rows.Count <= 0)
            {
                MessageBox.Show("不存在该工号的用户.");
                txtUNumber.Focus();
                txtUNumber.SelectAll();
            }
            else
            {
                DataRow drPm = dtPm.Rows[0];

                if (dtPm.Rows[0]["IsNoPrint"].ToString() == "True")
                {
                    IsPrint = false;
                }
                else
                {
                    IsPrint = true;
                }

                if (drPm["Fy_Id"].ToString() != CApplication.App.CurrentSession.FyId.ToString())
                {
                    MessageBox.Show("本工厂不存在该工号的用户.");
                    txtUNumber.Focus();
                    txtUNumber.SelectAll();

                    return;
                }
                drReceDeliUser = drPm;
                strUserId = drPm["User_Id"].ToString();
                txtUNumber.Text = drPm["Number"].ToString();
                txtUName.Text = drPm["Name"].ToString();
                txtUDept.Text = drPm["Dept_Txt"].ToString();

                txtUNumber.Properties.ReadOnly = true;

                GetCurrAllItem();

                gridCRece.DataSource = dtRece.DefaultView;
                gridVRece.BestFitColumns();

                gridCDeli.DataSource = dtDeli.DefaultView;
                gridVDeli.BestFitColumns();

                gridCSLoss.DataSource = dtSLoss.DefaultView;
                gridVSLoss.BestFitColumns();

                SetUSumInfo();

                strFocusedContrFlag = "1";
                ParentControl = gcRece;
                GridViewEdit = gridVRece;
                arrContrSeq = arrContrSeqF;

                gridVDeli.ClearSelection();
                gridVSLoss.ClearSelection();
                gridVRece.ClearSelection();
                gridVRece.SelectRow(gridVRece.FocusedRowHandle);

                this.txtPONumber.Select();
                txtPONumber.Focus();
                ucULossRepTypeD.DrFilterFieldsOrd = drReceDeliUser;
                ucULossRepTypeD.FilterFieldsOrd = "Dept_Id";
            }
        }
        private void txtPONumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r' || txtPONumber.Properties.ReadOnly ||
                txtPONumber.Text == string.Empty)
                return;

            string[] strKey = "Number,flag".Split(",".ToCharArray());
            DataTable dtPm = this.DataRequest_By_DataTable("Tst_UserReceDeliChkEdit_Add_Edit_Del", strKey,
                new string[] { txtPONumber.Text.Trim(), "101" });
            if (dtPm == null)
            {
                drPOrdNumber = null;
                txtPOrdNumberRkm.Text = string.Empty;
                return;
            }
            if (dtPm.Rows.Count <= 0)
            {
                MessageBox.Show("不存在对应的加工单信息.");
                txtPONumber.Focus();
                txtPONumber.SelectAll();
            }
            else
            {
                drPOrdNumber = dtPm.Rows[0];
                txtPONumber.Text = drPOrdNumber["POrdNumber"].ToString();
                txtPOrdNumberRkm.Text = drPOrdNumber["POrdNumberRkm"].ToString();
                txtPONumber.Properties.ReadOnly = true;
            }
        }

        public override void GetCurrAllItem()
        {
            this.dtRece = null;
            this.dtDeli = null;
            this.dtSLoss = null;
            string[] strKey = "StartWT_Dt,EndWT_Dt,IsNotChk,UserId,UDept_Id,Fy_Id,EUser_Id,EDept_Id".Split(",".ToCharArray());
            DataSet dsUReDl = this.DataRequest_By_DataSet("Tst_UserReceDeliChkEdit_Add_Edit_Del",
               strKey , new string[] { 
                    dtAccountDt.Text,dtAccountDt.Text, 
                    dtAccountDt.Text == string.Empty ? "1":"0",
                    strUserId
                    ,drReceDeliUser["Dept_Id"].ToString()
                    ,CApplication.App.CurrentSession.FyId.ToString()
                    ,CApplication.App.CurrentSession.UserId.ToString()
                    ,CApplication.App.CurrentSession.DeptId.ToString() });
            dsUReDl.AcceptChanges();
            this.dtRece = dsUReDl.Tables[0];
            this.dtDeli = dsUReDl.Tables[1];
            this.dtSLoss = dsUReDl.Tables[2];
            DataTable dtOverL = dsUReDl.Tables[3];
            dtURDSetTst = dsUReDl.Tables[4];

            dtRece.AcceptChanges();
            dtDeli.AcceptChanges();
            dtSLoss.AcceptChanges();

            if (dtOverL.Rows.Count > 0)
            {
                gcSum.Text = string.Format("账汇总[总损:{0};超损:{1}]", dtOverL.Rows[0]["RealLoss"].ToString(), dtOverL.Rows[0]["OverLoss"].ToString());
            }
            else
            {
                gcSum.Text = "账汇总";
            }

            BoundRDept();
        }
        private void BoundRDept()
        {
            //DataTable dtULossRep = dtConst.Clone();
            //foreach (DataRow dr in dtRece.Rows)
            //{
            //    string strType = dr["UDeptLossId"].ToString();
            //    if (strType == string.Empty)
            //        continue;

            //    if (dtULossRep.Select("SetValue='" + strType + "'").Length > 0)
            //        continue;

            //    dtULossRep.Rows.Add(new object[] { "ULossRepType", strType, strType, dr["ULossRepType"].ToString() });
            //}
            //foreach (DataRow dr in this.dtDeli.Rows)
            //{
            //    string strType = dr["UDeptLossId"].ToString();
            //    if (strType == string.Empty)
            //        continue;

            //    if (dtULossRep.Select("SetValue='" + strType + "'").Length > 0)
            //        continue;

            //    dtULossRep.Rows.Add(new object[] { "ULossRepType", strType, strType, dr["ULossRepType"].ToString() });
            //}
            //StaticFunctions.BindDplComboByTable(dplULossRepType, dtULossRep, "Name", "SetValue",
            //    new string[] { "Number", "Name" },
            //    new string[] { "编号", "名称" }, true, "", "", false);
            //dplULossRepType.Properties.BestFit();
            //if (dtULossRep.Rows.Count == 1)
            //{
            //    dplULossRepType.EditValue = dtULossRep.Rows[0]["SetValue"].ToString();
            //}
        }
        public override void RefreshItem()
        {
            if (strUserId == string.Empty)
            {
                MessageBox.Show("请选输入员工.");
                txtUNumber.Focus();
                txtUNumber.SelectAll();
                return;
            }
            GetCurrAllItem();

            blInitBound = true;
            gridCRece.DataSource = dtRece.DefaultView;
            gridVRece.BestFitColumns();
            blInitBound = false;

            if (strRMode != "VIEW")
                SetRMode("VIEW");

            DataRow drRece = gridVRece.GetFocusedDataRow();
            if (drRece != null)
            {
                StaticFunctions.SetDataRow2ControlValue(gcRece, drRece);
            }
            else
            {
                StaticFunctions.SetControlEmpty(gcRece);
            }

            blInitBound = true;
            gridCDeli.DataSource = dtDeli.DefaultView;
            gridVDeli.BestFitColumns();
            blInitBound = false;

            if (strDMode != "VIEW")
                SetDMode("VIEW");

            DataRow drDeil = gridVDeli.GetFocusedDataRow();
            if (drDeil != null)
            {
                StaticFunctions.SetDataRow2ControlValue(gcDeli, drDeil);
            }
            else
            {
                StaticFunctions.SetControlEmpty(gcDeli);
            }

            gridCSLoss.DataSource = dtSLoss.DefaultView;
            gridVSLoss.BestFitColumns();

            SetUSumInfo();

            strFocusedContrFlag = "1";
            ParentControl = gcRece;
            GridViewEdit = gridVRece;
            arrContrSeq = arrContrSeqF;

            gridVDeli.ClearSelection();
            gridVSLoss.ClearSelection();
            gridVRece.ClearSelection();
            gridVRece.SelectRow(gridVRece.FocusedRowHandle);

            this.txtNetWeight.Focus();
        }

        private void btnWAdd_Click(object sender, EventArgs e)
        {
            if (strUserId == string.Empty)
            {
                MessageBox.Show("请选输入员工.");
                txtUNumber.Focus();
                txtUNumber.SelectAll();
                return;
            }
            string strReceIds = drReceDeliUser["Rece_DeptIds"].ToString();
            if (strReceIds == string.Empty)
            {
                MessageBox.Show("该员工不能在本收发领货");
                return;
            }
            string[] arrIds = strReceIds.Split(",".ToCharArray());
            bool blE = false;
            foreach (string strId in arrIds)
            {
                if (strId.Trim() == string.Empty)
                    continue;

                if (strId.Trim() == CApplication.App.CurrentSession.DeptId.ToString())
                {
                    blE = true;
                    break;
                }
            }

            if (!blE)
            {
                MessageBox.Show("该员工不能在本收发领货");
                return;
            }

            if(dtURDSetTst.Rows.Count >0)
            {
                DataRow[] drSets = dtURDSetTst.Select("ReceDeliType=1 AND Tst_User_Id=" + CApplication.App.CurrentSession.UserId.ToString());
                if (drSets.Length <= 0)
                {
                    MessageBox.Show("本收发不能给该员工发货");
                    return;
                }
            }
            if (dtAccountDt.Text != string.Empty)
            {
                if (!ckbChg.Checked)
                {
                    MessageBox.Show("账目已完成盘点，不能新增.");
                    return;
                }
                string strMsg = "新增记录将作为 " + dtAccountDt.Text + " 的账目，是否继续？";
                if (MessageBox.Show(strMsg, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }

            if (strDMode != "VIEW")
            {
                blInitBound = true;
                btnDCancel_Click(null, null);
                blInitBound = false;
            }

            gridVRece.ClearColumnsFilter();

            SetRMode("ADD");
        }
        private void btnWCancel_Click(object sender, EventArgs e)
        {
            if (strRMode != "VIEW")
                SetRMode("VIEW");
            DataRow dr = gridVRece.GetFocusedDataRow();
            if (dr != null)
            {
                StaticFunctions.SetDataRow2ControlValue(gcRece, dr);
            }
            else
            {
                StaticFunctions.SetControlEmpty(gcRece);
            }
        }
        private bool CheckRSave()
        {
            string strChkV = Convert.ToString(ucGoods_TypeR.EditValue);
            if (strChkV == string.Empty || strChkV == "-9999")
            {
                MessageBox.Show("料类型不能都为空.");
                ucGoods_TypeR.Focus();
                ucGoods_TypeR.ShowPopup();
                return false;
            }
            //double dNW = txtNetWeight.Text == string.Empty ? 0 : double.Parse(txtNetWeight.Text);
            //if (dNW <= 0)
            //{
            //    MessageBox.Show("重量不能都为空.");
            //    txtNetWeight.Focus();
            //    txtNetWeight.SelectAll();
            //    return false;
            //}
            return true;
        }
        private void btnWSave_Click(object sender, EventArgs e)
        {
            if (!CheckRSave())
                return;

            DataRow drNew = this.dtRece.NewRow();
            StaticFunctions.SetControlValue2DataRow(gcRece, drNew, true);
            drNew["UserId"] = strUserId;
            drNew["UDeptId"] = drReceDeliUser["Dept_Id"];
            if (drPOrdNumber != null)
            {
                drNew["ProdOrdId"] = drPOrdNumber["ProdOrdId"];
                drNew["POrdNumber"] = drPOrdNumber["POrdNumber"];
            }
            drNew["Goods_Type"] = ucGoods_TypeR.TextData;
            drNew["HostName"] = System.Net.Dns.GetHostName();
            if (dtAccountDt.Text != string.Empty)
                drNew["AccountDt"] = dtAccountDt.Text;

            btnWSave.Enabled = false;
            string strField = string.Empty;
            string strValues = string.Empty;

            bool blPrint = false;
            if (strRMode == "ADD")
            {
                string[] strFileds = new string[] { "HostName","UserId","UDeptId","BseGTpyeId","Goods_Type",
                    "ProdOrdId","POrdNumber", "NetWeight", "Amount","Remark","AccountDt","NetWExpre"};
                strValues = StaticFunctions.GetAddValues(drNew, strFileds, out strField);

                string[] strKey = "StartWT_Dt,EndWT_Dt,IsNotChk,strFields,strFieldValues,UserId,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                DataSet dtAdd =this.DataRequest_By_DataSet("Tst_UserReceDeliChkEdit_Add_Edit_Del",
                   strKey , new string[] {
                    dtAccountDt.Text,dtAccountDt.Text, 
                    dtAccountDt.Text == string.Empty ? "1":"0",
                    strField,
                    strValues,
                    strUserId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                    "2"});
                if (dtAdd ==null)
                {
                    btnWSave.Enabled = true;
                    return;
                }
                DataRow drAdd = dtAdd.Tables[0].Rows[0];
                drNew["UReceProd_Id"] = drAdd["UReceProd_Id"].ToString();
                drNew["RIndex"] = drAdd["RIndex"].ToString();
                drNew["BarCodeNumber"] = drAdd["BarCodeNumber"].ToString();
                drNew["Rece_Dt"] = drAdd["Rece_Dt"].ToString();
                drNew["Dept_Id"] = CApplication.App.CurrentSession.DeptId.ToString();
                drNew["CNumber"] = CApplication.App.CurrentSession.Number.ToString();
                drNew["CName"] = CApplication.App.CurrentSession.UserNme.ToString();
                drNew["CustName"] = drAdd["CustName"].ToString();
                drNew["CustSignet"] = drAdd["CustSignet"].ToString();
                dtRece.Rows.InsertAt(drNew,0);

                blPrint = true;
            }
            dtRece.AcceptChanges();
            gridVRece.ClearSelection();
            gridVRece.MoveFirst();
            SetRMode("VIEW");

            if (blPrint)
            {
                if (IsPrint)
                {
                    if (MessageBox.Show("成功保存，是否打印？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        PrintR(drNew);
                    }
                }
            }

            if (drReceDeliUser["Is_ReSet"].ToString() == "True")
            {
                BoundRDept();
                SetUSumInfo();
                gridVRece.BestFitColumns();
            }
            else
            {
                dtAccountDt.EditValueChanged -= dtAccountDt_EditValueChanged;
                txtUNumber.Text = string.Empty;
                strUserId = string.Empty;
                this.InitialByParam("VIEW", string.Empty, null);
                dtAccountDt.EditValueChanged += dtAccountDt_EditValueChanged;
            }
        }

        private void PrintR(DataRow tp_Dr)
        {
            try
            {
                BarTender.Application btdb = new BarTender.Application();
                try
                {
                    PrintRItem(btdb, tp_Dr);
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
        private void PrintRItem(BarTender.Application btdb, DataRow dr)
        {
            string strPrintPath = string.Empty;
            strPrintPath = Application.StartupPath + @"\打印模板" + @"\员工领货.btw";
            btdb.Formats.Open(strPrintPath, true, "");
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("GetGood", txtUNumber.Text + "-" + txtUName.Text + "-" + txtUDept.Text);

            string strIndex = dr["RIndex"].ToString();
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Num", strIndex);

            string strUDepTxt = string.Empty;
            DataRow[] drUs = dtDept.Select("Dept_Id=" + dr["Dept_Id"].ToString());
            if (drUs.Length > 0)
            {
                strUDepTxt = drUs[0]["Name"].ToString();
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("PutGood", dr["CNumber"].ToString() + "-" + dr["CName"].ToString() + "-" + strUDepTxt);
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Time", dr["Rece_Dt"].ToString());

            string strGood = dr["Goods_Type"].ToString();
            strGood += ";净重" + dr["NetWeight"].ToString();
            if (dr["Amount"] != DBNull.Value && decimal.Parse(dr["Amount"].ToString()) > 0)
            {
                strGood += "数量" + dr["Amount"].ToString();
            }
            strGood += "[领]";

            if (dr["Remark"].ToString() != string.Empty)
            {
                strGood += "[" + dr["Remark"].ToString() + "]";
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Good", strGood);
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("HostName", dr["HostName"].ToString());
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Barcode", dr["BarCodeNumber"].ToString());
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("CustName", dr["CustName"].ToString());
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("CustSignet", dr["CustSignet"].ToString());

            object Zero = 0;
            btdb.Formats.Item(ref Zero).PrintOut(true, false);
        }

        private void btnDAdd_Click(object sender, EventArgs e)
        {
            if (strUserId == string.Empty)
            {
                MessageBox.Show("请选输入员工.");
                txtUNumber.Focus();
                txtUNumber.SelectAll();
                return;
            }
            string strReceIds = drReceDeliUser["Deli_DeptIds"].ToString();
            if (strReceIds == string.Empty)
            {
                MessageBox.Show("该员工不能在本收发交货");
                return;
            }
            string[] arrIds = strReceIds.Split(",".ToCharArray());
            bool blE = false;
            foreach (string strId in arrIds)
            {
                if (strId.Trim() == string.Empty)
                    continue;

                if (strId.Trim() == CApplication.App.CurrentSession.DeptId.ToString())
                {
                    blE = true;
                    break;
                }
            }

            if (!blE)
            {
                MessageBox.Show("该员工不能在本收发交货");
                return;
            }

            if (dtURDSetTst.Rows.Count > 0)
            {
                DataRow[] drSets = dtURDSetTst.Select("ReceDeliType=2 AND Tst_User_Id=" + CApplication.App.CurrentSession.UserId.ToString());
                if (drSets.Length <= 0)
                {
                    MessageBox.Show("本收发不能给该员工收货");
                    return;
                }
            }
            if (dtAccountDt.Text != string.Empty)
            {
                if (!ckbChg.Checked)
                {
                    MessageBox.Show("账目已完成盘点，不能新增.");
                    return;
                }
                string strMsg = "新增记录将作为 " + dtAccountDt.Text + " 的账目，是否继续？";
                if (MessageBox.Show(strMsg, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }

            if (strRMode != "VIEW")
            {
                blInitBound = true;
                btnWCancel_Click(null, null);
                blInitBound = false;
            }

            gridVRece.ClearColumnsFilter();
            gridVDeli.ClearColumnsFilter();

            blInitBound = true;
            dtRece.DefaultView.RowFilter = string.Empty;
            dtDeli.DefaultView.RowFilter = string.Empty;
            blInitBound = false;

            SetDMode("ADD");
        }
        private void btnDCancel_Click(object sender, EventArgs e)
        {
            if (strDMode != "VIEW")
                SetDMode("VIEW");

            DataRow dr = gridVDeli.GetFocusedDataRow();
            if (dr != null)
            {
                StaticFunctions.SetDataRow2ControlValue(gcDeli, dr);
            }
            else
            {
                StaticFunctions.SetControlEmpty(gcDeli);
            }
        }
        private bool CheckDSave()
        {
            string strChkV = Convert.ToString(ucGoods_TypeD.EditValue);
            if (strChkV == string.Empty || strChkV == "-9999")
            {
                MessageBox.Show("料类型不能都为空.");
                ucGoods_TypeD.Focus();
                ucGoods_TypeD.ShowPopup();
                return false;
            }
            if (dplDeliType.EditValue == null || dplDeliType.EditValue.ToString() == string.Empty || dplDeliType.EditValue.ToString() =="-9999")
            {
                MessageBox.Show("交类型不能为空.");
                dplDeliType.Focus();
                dplDeliType.ShowPopup();
                return false;
            }
            //double dNW = txtDNetWeight.Text == string.Empty ? 0 : double.Parse(txtDNetWeight.Text);
            //if (dNW <= 0)
            //{
            //    MessageBox.Show("重量不能都为空.");
            //    txtDNetWeight.Focus();
            //    txtDNetWeight.SelectAll();
            //    return false;
            //}
            return true;
        }
        private void btnDSave_Click(object sender, EventArgs e)
        {
            if (!CheckDSave())
                return;

            DataRow drNew = this.dtDeli.NewRow();
            StaticFunctions.SetControlValue2DataRow(gcDeli, drNew, true);
            if (drPOrdNumber != null)
            {
                drNew["ProdOrdId"] = drPOrdNumber["ProdOrdId"];
                drNew["POrdNumber"] = drPOrdNumber["POrdNumber"];
            }
            drNew["UserId"] = strUserId;
            drNew["UDeptId"] = drReceDeliUser["Dept_Id"];
            drNew["Goods_Type"] = ucGoods_TypeD.TextData;
            drNew["ULossRepType"] = ucULossRepTypeD.TextData;
            drNew["Goods_Type"] = ucGoods_TypeD.TextData;
            drNew["HostName"] = System.Net.Dns.GetHostName();
            if (dtAccountDt.Text != string.Empty)
                drNew["AccountDt"] = dtAccountDt.Text;

            btnDSave.Enabled = false;

            string strField = string.Empty;
            string strValues = string.Empty;

            bool blPrint = false;
            if (this.strDMode == "ADD")
            {
                string[] strFileds = new string[] { "HostName","RIndex","UserId","UDeptId","BseGTpyeId","Goods_Type","ProdOrdId","POrdNumber",
                    "DeliType","NetWeight", "Amount","Remark","AccountDt","UDeptLossId","ULossRepType","ProdLoss","NetWExpre"};
                strValues = StaticFunctions.GetAddValues(drNew, strFileds, out strField);

                string[] strKey = "StartWT_Dt,EndWT_Dt,IsNotChk,strFields,strFieldValues,UserId,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                DataSet dtAdd = this.DataRequest_By_DataSet("Tst_UserReceDeliChkEdit_Add_Edit_Del",
                   strKey, new string[] {
                    dtAccountDt.Text,dtAccountDt.Text, 
                    dtAccountDt.Text == string.Empty ? "1":"0",
                    strField,
                    strValues,
                    strUserId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                    "4"});

                if (dtAdd == null)
                {
                    btnDSave.Enabled = true;
                    return;
                }
                DataRow drAdd = dtAdd.Tables[0].Rows[0];
                drNew["UDeliProd_Id"] = drAdd["UDeliProd_Id"].ToString();
                drNew["DIndex"] = drAdd["DIndex"].ToString();
                drNew["Deli_Dt"] = drAdd["Deli_Dt"].ToString();
                drNew["Dept_Id"] = CApplication.App.CurrentSession.DeptId;
                drNew["CNumber"] = CApplication.App.CurrentSession.Number.ToString();
                drNew["CName"] = CApplication.App.CurrentSession.UserNme.ToString();
                drNew["BarCodeNumber"] = drAdd["BarCodeNumber"].ToString();
                drNew["CustName"] = drAdd["CustName"].ToString();
                drNew["CustSignet"] = drAdd["CustSignet"].ToString();
                dtDeli.Rows.InsertAt(drNew, 0);

                blPrint = true;
            }

            dtDeli.AcceptChanges();
            gridVDeli.ClearSelection();
            gridVDeli.MoveFirst();
            SetDMode("VIEW");
          
            if (blPrint)
            {
                if (IsPrint)
                {
                    if (MessageBox.Show("成功保存，是否打印？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        PrintD(drNew);
                    }

                    if (txtDRIndex.Text.Trim() != string.Empty)
                    {
                        SetUSumInfo();
                        double dLoss = 0;
                        if (double.TryParse(txtD_Real_Loss.Text, out dLoss) && Math.Abs(dLoss) < 0.5)
                        {
                            if (MessageBox.Show("是否盘点：领序号 " + txtDRIndex.Text.Trim() + " ;损耗 " + txtD_Real_Loss.Text, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                txtSRIndex.Text = txtDRIndex.Text.Trim();
                                DataRow drLoss = null;
                                if (ChkULoss(false, out drLoss))
                                {
                                    if (MessageBox.Show("成功盘点，是否打印？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        PrintU(drLoss);
                                    }
                                    this.RefreshItem();
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            if (drReceDeliUser["Is_ReSet"].ToString() == "True")
            {
                BoundRDept();
                SetUSumInfo();
                gridVDeli.BestFitColumns();
            }
            else
            {
                dtAccountDt.EditValueChanged -= dtAccountDt_EditValueChanged;
                txtUNumber.Text = string.Empty;
                strUserId = string.Empty;
                this.InitialByParam("VIEW", string.Empty, null);
                dtAccountDt.EditValueChanged += dtAccountDt_EditValueChanged;
            }
        }

        private void PrintD(DataRow tp_Dr)
        {
            try
            {
                BarTender.Application btdb = new BarTender.Application();
                try
                {
                    PrintDItem(btdb, tp_Dr);
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
        private void PrintDItem(BarTender.Application btdb, DataRow dr)
        {
            string strPrintPath = Application.StartupPath + @"\打印模板" + @"\员工交货.btw";
            btdb.Formats.Open(strPrintPath, true, "");
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("GiveGood", txtUNumber.Text + "-" + txtUName.Text + "-" + txtUDept.Text);

            string strIndex = dr["DIndex"].ToString();
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Num", strIndex);

            string strUDepTxt = string.Empty;
            DataRow[] drUs = dtDept.Select("Dept_Id=" + dr["Dept_Id"].ToString());
            if (drUs.Length > 0)
            {
                strUDepTxt = drUs[0]["Name"].ToString();
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("RevGood", dr["CNumber"].ToString() + "-" + dr["CName"].ToString() + "-" + strUDepTxt);
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Time", dr["Deli_Dt"].ToString());
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("ULoss", ucULossRepTypeD.TextData + "-" + txtProdLossD.Text);

            string strGood = this.ucGoods_TypeD.TextData;
            DataRow[] drss = this.dtConst.Select("SetKey='DeliType' and SetValue='" + dr["DeliType"].ToString() + "'");
            if (drss.Length > 0)
            {
                strGood += "["+drss[0]["Name"].ToString()+"]";
            }
            strGood += "净重" + dr["NetWeight"].ToString();
            if (dr["Amount"] != DBNull.Value && decimal.Parse(dr["Amount"].ToString()) > 0)
            {
                strGood += "数量" + dr["Amount"].ToString();
            }
            strGood += "[交]";
            if (dr["Remark"].ToString() != string.Empty)
            {
                strGood += "[" + dr["Remark"].ToString() + "]";
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Good", strGood);
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("HostName", dr["HostName"].ToString());
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Barcode", dr["BarCodeNumber"].ToString());
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("CustName", dr["CustName"].ToString());
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("CustSignet", dr["CustSignet"].ToString());

            object Zero = 0;
            btdb.Formats.Item(ref Zero).PrintOut(true, false);
        }

        private void SetUSumInfo()
        {
            double dQd = 0;
            double dUd = 0;
            double dRet = 0;
            for (int i = 0; i < gridVDeli.RowCount; i++)
            {
                DataRow dr = gridVDeli.GetDataRow(i);
                double dnw = dr["NetWeight"] == DBNull.Value ? 0 : double.Parse(dr["NetWeight"].ToString());

                string strType = dr["DeliType"].ToString();
                if (strType == "1")
                {
                    dQd += dnw * 100;
                }
                else if (strType == "2")
                {
                    dUd += dnw * 100;
                }
                else if (strType == "3")
                {
                    dRet += dnw * 100;
                }
            }
            txtD_Qd_W.Text = (dQd / 100).ToString();
            txtD_Ud_W.Text = (dUd / 100).ToString();
            txtD_SP_W.Text = ((dQd + dUd) / 100).ToString();
            txtD_Ret_W.Text = (dRet / 100).ToString();

            decimal dRPN = decimal.Parse(this.gridVRece.Columns["NetWeight"].SummaryText);
            decimal dRPAN = 0;// decimal.Parse(this.gridVRece.Columns["AllWeight"].SummaryText);
            txtD_RP_W.Text = (dRPN + dRPAN).ToString();

            decimal dDPN = decimal.Parse(this.gridVDeli.Columns["NetWeight"].SummaryText);
            decimal dDPAN = 0;//  decimal.Parse(this.gridVDeli.Columns["AllWeight"].SummaryText);
            txtD_DP_W.Text = (dDPN + dDPAN).ToString();

            txtD_Real_Loss.Text = ((dRPN * 100 + dRPAN * 100 - dDPN * 100 - dDPAN * 100) / 100).ToString();
        }

        private void btnChk_Click(object sender, EventArgs e)
        {
            if (strUserId == string.Empty)
            {
                MessageBox.Show("请选输入员工.");
                txtUNumber.Focus();
                txtUNumber.SelectAll();
                return;
            }

            if (drReceDeliUser["Sub_Tst"].ToString() != CApplication.App.CurrentSession.DeptId.ToString())
            {
                MessageBox.Show("员工不属于本收发，不能盘点.");
                return;
            }

            if (MessageBox.Show("是否确认员工盘点,损耗：" + txtD_Real_Loss.Text, "盘点确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                return;
            DataRow drLoss = null;
            if (ChkULoss(true,out drLoss))
            {
                if (drReceDeliUser["Is_ReSet"].ToString() == "True")
                {
                    this.RefreshItem();
                }
                else
                {
                    dtAccountDt.EditValueChanged -= dtAccountDt_EditValueChanged;
                    txtUNumber.Text = string.Empty;
                    strUserId = string.Empty;
                    this.InitialByParam("VIEW", string.Empty, null);
                    dtAccountDt.EditValueChanged += dtAccountDt_EditValueChanged;
                }
            }
        }
        private bool ChkULoss(bool blPrint,out DataRow drLoss)
        {
            gridVDeli.ClearColumnsFilter();
            gridVRece.ClearColumnsFilter();

            string strRPIds = string.Empty;
            string strDPIds = string.Empty;
            drLoss = null;

            for (int i = 0; i < gridVRece.RowCount; i++)
            {
                DataRow dr = gridVRece.GetDataRow(i);
                strRPIds += strRPIds == string.Empty ? dr["UReceProd_Id"].ToString() : "," + dr["UReceProd_Id"].ToString();
            }

            for (int i = 0; i < gridVDeli.RowCount; i++)
            {
                DataRow dr = gridVDeli.GetDataRow(i);
                strDPIds += strDPIds == string.Empty ? dr["UDeliProd_Id"].ToString() : "," + dr["UDeliProd_Id"].ToString();
            }

            if (strRPIds == string.Empty && strDPIds == string.Empty)
                return false;

            DataRow drNew = dtSLoss.NewRow();
            StaticFunctions.SetControlValue2DataRow(gcSum, drNew, true);
            drNew["ChkUserId"] = strUserId;
            if (dtAccountDt.Text != string.Empty)
                drNew["AccountDt"] = dtAccountDt.Text;

            string strField = string.Empty;
            string strValues = string.Empty;
            string[] strFileds = new string[] { "ChkUserId", "D_RP_W", "D_DP_W","D_Ret_W", "D_Qd_W", 
                    "D_Ud_W","D_SP_W", "D_Real_Loss","RIndex","AccountDt"};
            strValues = StaticFunctions.GetAddValues(drNew, strFileds, out strField);

            string[] strKey = "StartWT_Dt,EndWT_Dt,IsNotChk,strFields,strFieldValues,UserId,strRPIds,strDPIds,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataSet dtAdd =this.DataRequest_By_DataSet("Tst_UserReceDeliChkEdit_Add_Edit_Del",
              strKey , new string[] { 
                    dtAccountDt.Text,dtAccountDt.Text, 
                    dtAccountDt.Text == string.Empty ? "1":"0",
                    strField,
                    strValues,strUserId,
                    strRPIds,strDPIds,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                    "8"});
            if (dtAdd==null)
            {
                btnChk.Enabled = true;
                return false;
            }
            drLoss = dtAdd.Tables[0].Rows[0];
            dtSLoss.Rows.Add(drNew);

            if (blPrint)
            {
                if (MessageBox.Show("成功盘点，是否打印？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    PrintU(dtAdd.Tables[0].Rows[0]);
                }
            }

            return true;
        }

        private void PrintU(DataRow tp_Dr)
        {
            try
            {
                BarTender.Application btdb = new BarTender.Application();
                try
                {
                    PrintULoss(btdb, tp_Dr);
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
        private void PrintULoss(BarTender.Application btdb, DataRow dr)
        {
            string strPrintPath = Application.StartupPath + @"\打印模板" + @"\员工损耗.btw";
            btdb.Formats.Open(strPrintPath, true, "");
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Time", dr["Chk_Dt"].ToString());

            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("UserTxt", txtUNumber.Text + "-" + txtUName.Text + "-" + txtUDept.Text);

            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("TstTxt", CApplication.App.CurrentSession.Number
                + "-" + CApplication.App.CurrentSession.UserNme + "-" + CApplication.App.CurrentSession.DeptNme);

            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("SumRece", txtD_RP_W.Text);
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("SumDeli", txtD_DP_W.Text);
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("ULoss", this.txtD_Real_Loss.Text);

            object Zero = 0;
            btdb.Formats.Item(ref Zero).PrintOut(true, false);
        }
        private void PrintUSum()
        {
            if (strUserId == string.Empty)
            {
                return;
            }
            try
            {
                BarTender.Application btdb = new BarTender.Application();
                try
                {
                    gridVRece.ClearColumnsFilter();
                    gridVDeli.ClearColumnsFilter();

                    blInitBound = true;
                    dtRece.DefaultView.RowFilter = string.Empty;
                    dtDeli.DefaultView.RowFilter = string.Empty;
                    blInitBound = false;

                    this.SetUSumInfo();

                    string strPrintPath = Application.StartupPath + @"\打印模板" + @"\员工汇总.btw";
                    btdb.Formats.Open(strPrintPath, true, "");
                    btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Time", DateTime.Now.ToString());

                    btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("UserTxt", txtUNumber.Text + "-" + txtUName.Text + "-" + txtUDept.Text);
                    btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("TstTxt", CApplication.App.CurrentSession.Number
                        + "-" + CApplication.App.CurrentSession.UserNme + "-" + CApplication.App.CurrentSession.DeptNme);

                    btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("SumRece", (double.Parse(txtD_RP_W.Text) + double.Parse(this.gridVSLoss.Columns["D_RP_W"].SummaryText)).ToString());
                    btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("SumDeli", (double.Parse(txtD_DP_W.Text) + double.Parse(this.gridVSLoss.Columns["D_DP_W"].SummaryText)).ToString());
                    btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("SumProd", (double.Parse(txtD_Qd_W.Text) + double.Parse(this.gridVSLoss.Columns["D_Qd_W"].SummaryText)).ToString());
                    btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("SumNoDeli", this.txtD_Real_Loss.Text);
                    btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("HaveLoss", double.Parse(this.gridVSLoss.Columns["D_Real_Loss"].SummaryText).ToString());

                    string[] strKey = "StartWT_Dt,EndWT_Dt,IsNotChk,UserId,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                    DataTable dtUSum = this.DataRequest_By_DataTable("Tst_UserReceDeliChkEdit_Add_Edit_Del",
                       strKey , new string[] { 
                            dtAccountDt.Text,
                            dtAccountDt.Text, 
                            dtAccountDt.Text == string.Empty ? "1":"0",
                            strUserId,
                            CApplication.App.CurrentSession.UserId.ToString(),
                            CApplication.App.CurrentSession.DeptId.ToString(),
                            CApplication.App.CurrentSession.FyId.ToString(),"14"});
                    dtUSum.AcceptChanges();
                    StringBuilder sbLoss = new StringBuilder();
                    foreach (DataRow dr in dtUSum.Rows)
                    {
                        if (sbLoss.Length == 0)
                            sbLoss.Append(dr["SetText"].ToString() + ":" + dr["NetWeight"].ToString()+"g");
                        else
                            sbLoss.Append("\r\n" + dr["SetText"].ToString() + ":" + dr["NetWeight"].ToString() + "g");
                    }
                    btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("StandLoss", sbLoss.ToString());

                    object Zero = 0;
                    btdb.Formats.Item(ref Zero).PrintOut(true, false);
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

        private void txtWeight_Click(object sender, EventArgs e)
        {
            if (strRMode == "VIEW" && strDMode == "VIEW")
            {
                return;
            }

            DevExpress.XtraEditors.TextEdit txt = sender as DevExpress.XtraEditors.TextEdit;
            txt.SelectAll();
        }
        private void txt_EditValueChanged(object sender, EventArgs e)
        {
            if (frmEditorMode == "VIEW" || blInitBound)
                return;

            DevExpress.XtraEditors.TextEdit txt = sender as DevExpress.XtraEditors.TextEdit;
            if (txt.Text == string.Empty && txt.Name != "txtDRIndex")
            {
                blInitBound = true;
                txt.Text = "0";
                blInitBound = false;
            }
            //if (txt.Text.IndexOf('.') == -1 &&
            //    (txt.Name == "txtNetWeight" || txt.Name == "txtAllWeight" || txt.Name == "txtDNetWeight" || txt.Name == "txtDAllWeight"))
            //{
            //    blInitBound = true;
            //    txt.EditValue = decimal.Parse(txt.EditValue.ToString()) / 100;
            //    blInitBound = false;
            //}

            if (txt.Name == "txtDRIndex")
            {
                SetFilter();
            }

            if (txt.Name == "txtDNetWeight")
            {
                FindEqualWRecord();
            }
        }

        private void FindEqualWRecord()
        {
            if (strDMode == "VIEW")
                return;

            if (txtDRIndex.Text.Trim() != string.Empty)
                return;

            double dNw = txtDNetWeight.Text == string.Empty ? 0 : double.Parse(txtDNetWeight.Text);

            if (dNw == 0)
                return;

            dtRece.DefaultView.Sort = "NetWeight ASC";
            double dAbs = 99999;
            for (int i = 0; i < gridVRece.RowCount; i++)
            {
                DataRow dr = gridVRece.GetDataRow(i);
                double dnw = dr["NetWeight"] == DBNull.Value ? 0 : StaticFunctions.Round(double.Parse(dr["NetWeight"].ToString()), 2, 0.5);

                double d = Math.Abs(dnw - dNw);
                if (d == 0)
                {
                    gridVRece.FocusedRowHandle = i;
                    return;
                }
                else if (d < dAbs)
                {
                    dAbs = d;
                }
                else if (d > dAbs)
                {
                    gridVRece.FocusedRowHandle = i - 1;
                    return;
                }
            }
        }
        private void SetFilter()
        {
            if (strDMode == "VIEW")
                return;

            string strFilter = string.Empty;
            if (txtDRIndex.Text.Trim() != string.Empty)
            {
                string[] strItems = txtDRIndex.Text.Trim().Split(",， .".ToCharArray());
                string strFilteR = string.Empty;
                string strFilteD = string.Empty;
                foreach (string strItem in strItems)
                {
                    int iRidx = 0;
                    if (int.TryParse(strItem, out iRidx))
                    {
                        strFilteR += strFilteR == string.Empty ? iRidx.ToString() : "," + iRidx.ToString();
                        strFilteD += strFilteD == string.Empty ? "( ','+RIndex+',' LIKE '%," + iRidx.ToString() + ",%' " : "OR ','+RIndex+',' LIKE '%," + iRidx.ToString() + ",%' ";
                    }
                }
                blInitBound = true;
                if (strFilteR != string.Empty)
                {
                    if (strFilter != string.Empty)
                        strFilter += " and ";

                    dtRece.DefaultView.RowFilter = strFilter + "RIndex IN (" + strFilteR + ")";
                }
                else
                {
                    dtRece.DefaultView.RowFilter = strFilter;
                }
                if (strFilteD != string.Empty)
                {
                    dtDeli.DefaultView.RowFilter = strFilter + strFilteD + ")";
                }
                else
                {
                    dtDeli.DefaultView.RowFilter = strFilter;
                }
                blInitBound = false;
            }
            else
            {
                blInitBound = true;
                dtRece.DefaultView.RowFilter = strFilter;
                dtDeli.DefaultView.RowFilter = strFilter;
                blInitBound = false;
            }

            DataRow dr = gridVRece.GetFocusedDataRow();
            if (dr != null)
            {
                StaticFunctions.SetDataRow2ControlValue(gcRece, dr);

            }
            else
            {
                StaticFunctions.SetControlEmpty(gcRece);
            }
            this.SetUSumInfo();
        }

        public override bool DeleteFocusedItem()
        {
            if (!base.DeleteFocusedItem())
            {
                return false;
            }
            DataRow dr = GridViewEdit.GetFocusedDataRow();
            if (dr["Is_Chk"].ToString() == "True")
            {
                if (!ckbChg.Checked)
                {
                    MessageBox.Show("记录已完成盘点，不能删除.");
                    return false;
                }
                else
                {
                    string strMsg1 = "将修改 " + dtAccountDt.Text + " 的账目，是否继续？";
                    if (MessageBox.Show(strMsg1, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return false;
                    }
                }
            }

            string strMsg = string.Empty;
            string strDelId = string.Empty;
            string strDelFlag = string.Empty;

            if (strFocusedContrFlag == "1")
            {
                strMsg = "将删除选中的领货记录，是否继续？";
                strDelId = dr["UReceProd_Id"].ToString();
                strDelFlag = "6";
            }
            else if (strFocusedContrFlag == "2")
            {
                strMsg = "将删除选中的交货记录，是否继续？";
                strDelId = dr["UDeliProd_Id"].ToString();
                strDelFlag = "7";
            }
            else
            {
                strMsg = "将删除选中的盘点记录，是否继续？";
                strDelId = dr["UserChk_Id"].ToString();
                strDelFlag = "11";
            }

            if (MessageBox.Show(strMsg, "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return false;
            string[] strKey = "DelId,UserChk_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataTable dtAdd = this.DataRequest_By_DataTable("Tst_UserReceDeliChkEdit_Add_Edit_Del",
               strKey , new string[] {
                         strDelId,strDelId,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         strDelFlag});

            if (dtAdd==null)
            {
                return false;
            }
            this.RefreshItem();
            return true;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            CApplication.App.CurrentSession.TimerId = 0;
            int k = msg.WParam.ToInt32();
            if (k == 9)//Tab
            {
                if (strFocusedContrFlag == "1")
                {
                    strFocusedContrFlag = "2";
                    ParentControl = gcDeli;
                    GridViewEdit = gridVDeli;
                    arrContrSeq = arrContrSeqT;

                    gridVRece.ClearSelection();
                    gridVSLoss.ClearSelection();
                    gridVDeli.ClearSelection();
                    gridVDeli.SelectRow(gridVDeli.FocusedRowHandle);

                    txtDRIndex.Focus();
                    txtDRIndex.SelectAll();
                }
                else if (strFocusedContrFlag == "2")
                {
                    strFocusedContrFlag = "3";
                    ParentControl = gcSum;
                    GridViewEdit = gridVSLoss;
                    arrContrSeq = arrContrSeqS;


                    gridVRece.ClearSelection();
                    gridVDeli.ClearSelection();
                    gridVSLoss.ClearSelection();
                    gridVSLoss.SelectRow(gridVSLoss.FocusedRowHandle);

                    this.txtSRIndex.Focus();

                }
                else if (strFocusedContrFlag == "3")
                {
                    strFocusedContrFlag = "1";
                    ParentControl = gcRece;
                    GridViewEdit = gridVRece;
                    arrContrSeq = arrContrSeqF;

                    gridVDeli.ClearSelection();
                    gridVSLoss.ClearSelection();
                    gridVRece.ClearSelection();
                    gridVRece.SelectRow(gridVRece.FocusedRowHandle);

                    this.txtNetWeight.Focus();
                }
                return true;
            }
            else if (k == 122)//F11
            {
                if (strFocusedContrFlag == "1")
                {
                    if (strRMode == "VIEW" && gridVRece.GetFocusedDataRow() != null)
                        PrintR(gridVRece.GetFocusedDataRow());
                }
                else if (strFocusedContrFlag == "2" && gridVDeli.GetFocusedDataRow() != null)
                {
                    if (strDMode == "VIEW")
                        PrintD(gridVDeli.GetFocusedDataRow());
                }
                else if (strFocusedContrFlag == "3" && this.gridVSLoss.GetFocusedDataRow() != null)
                {
                    PrintU(gridVSLoss.GetFocusedDataRow());
                }
                return true;
            }
            else if (k == 117)//F6
            {
                PrintUSum();
                return true;
            }
            else if (k == 187 || k == 107 || k == 189 || k == 109) // + -
            {
                if (strFocusedContrName == "txtRNetWExpre" && strRMode == "ADD"
                   || strFocusedContrName == "txtDNetWExpre" && strDMode == "ADD")
                {
                    return false;
                }
                else if (k == 107) //小键盘+
                {
                    if (keyData.ToString().ToUpper().IndexOf("ALT") != -1)
                    {
                        if (btnDSave.Enabled)
                        {
                            btnDSave.Select();
                            btnDSave_Click(null, null);
                            return true;
                        }
                    }
                }
                else if (k == 109)//小键盘-
                {
                    if (keyData.ToString().ToUpper().IndexOf("ALT") != -1)
                    {
                        if (btnWSave.Enabled)
                        {
                            btnWSave.Select();
                            btnWSave_Click(null, null);
                            return true;
                        }
                    }
                }
            }
            else if (k == 96)//小键盘0
            {
                if (keyData.ToString().ToUpper().IndexOf("ALT") != -1)
                {
                    if (btnDAdd.Enabled)
                    {
                        btnDAdd_Click(null, null);
                        return true;
                    }
                }
            }
            else if (k == 100)//小键盘4
            {
                if (keyData.ToString().ToUpper().IndexOf("ALT") != -1)
                {
                    if (btnChk.Enabled)
                    {
                        btnChk.Select();
                        btnChk_Click(null, null);
                        return true;
                    }
                }
            }
            else if (k == 103)//小键盘7
            {
                if (keyData.ToString().ToUpper().IndexOf("ALT") != -1)
                {
                    if (btnWAdd.Enabled)
                    {
                        btnWAdd_Click(null, null);
                        return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (strUserId == string.Empty)
                return;

            string strFilter = string.Empty;
            if (txtSRIndex.Text.Trim() != string.Empty)
            {

                string[] strItems = txtSRIndex.Text.Split(",， .".ToCharArray());
                string strFilteR = string.Empty;
                string strFilteD = string.Empty;
                foreach (string strItem in strItems)
                {
                    int iRidx = 0;
                    if (int.TryParse(strItem, out iRidx))
                    {
                        strFilteR += strFilteR == string.Empty ? iRidx.ToString() : "," + iRidx.ToString();
                        strFilteD += strFilteD == string.Empty ? "( ','+RIndex+',' LIKE '%," + iRidx.ToString() + ",%' " : "OR ','+RIndex+',' LIKE '%," + iRidx.ToString() + ",%' ";
                    }
                }
                if (strFilteR != string.Empty)
                {
                    if (strFilter != string.Empty)
                        strFilter += " and ";
                    dtRece.DefaultView.RowFilter = strFilter + "RIndex IN (" + strFilteR + ")";
                }
                else
                {
                    dtRece.DefaultView.RowFilter = strFilter;
                }
                if (strFilteD != string.Empty)
                {
                    dtDeli.DefaultView.RowFilter = strFilter + strFilteD + ")";
                }
                else
                {
                    dtDeli.DefaultView.RowFilter = strFilter;
                }
            }
            else
            {
                dtRece.DefaultView.RowFilter = strFilter;
                dtDeli.DefaultView.RowFilter = strFilter;
            }

            DataRow dr = gridVRece.GetFocusedDataRow();
            if (dr != null)
            {
                StaticFunctions.SetDataRow2ControlValue(gcRece, dr);
            }
            else
            {
                StaticFunctions.SetControlEmpty(gcRece);
            }

            dr = this.gridVDeli.GetFocusedDataRow();
            if (dr != null)
            {
                StaticFunctions.SetDataRow2ControlValue(gcDeli, dr);
            }
            else
            {
                StaticFunctions.SetControlEmpty(gcDeli);
            }

            this.SetUSumInfo();
        }

        private void btnPowder_Click(object sender, EventArgs e)
        {
            Form frmEx = StaticFunctions.GetExistedChildForm(this.ParentForm, "frmTstPowderEdit");
            if (frmEx != null)
                frmEx.Close();

            frmTstPowderEdit frm = new frmTstPowderEdit();
            frm.strDeptNumber = this.strDeptNumber;
            frm.dtConst = this.dtConst;
            frm.dtDept = this.dtDept;
            frm.MdiParent = this.ParentForm;
            frm.Show();
        }

        private void btnDInfo_Click(object sender, EventArgs e)
        {
            StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", this.ParentForm, "员工计件", "frmSysBseManger", "frmTst_UserDeliFeeEditNew", "ADD", "BusClassName=frmTst_UserDeliFeeEditNew", null);
        }
        private void btnOver_Click(object sender, EventArgs e)
        {
            frmTst_UserOverLossChk frm = new frmTst_UserOverLossChk();
            frm.strAccountDt = dtAccountDt.Text;
            frm.strUId = strUserId;
            frm.dtConst = this.dtConst;
            frm.ShowDialog();
        }

        private void dtAccountDt_EditValueChanged(object sender, EventArgs e)
        {
            RefreshItem();
        }
        private void txtNWexpre_EditValueChanged(object sender, EventArgs e)
        {
            if (frmEditorMode == "VIEW" || blInitBound)
                return;
            try
            {
                DevExpress.XtraEditors.TextEdit txt = sender as DevExpress.XtraEditors.TextEdit;
                object snw = new DataTable().Compute(txt.Text.Trim(), null);
                double dnw = 0;
                if (double.TryParse(snw.ToString(), out dnw))
                {
                    if (txt.Name == "txtRNetWExpre")
                    {
                        txtNetWeight.EditValue = dnw;
                    }
                    else
                    {
                        txtDNetWeight.EditValue = dnw;
                    }
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
    }
}