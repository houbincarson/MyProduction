using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace ProduceManager
{
    public partial class frmBseUserEdit : frmEditorBase
    {
        #region 变量
        private DataSet dsLoad = null;
        private bool blInitBound = false;
        private string strSpName = "Bse_User_Add_Edit_Del";
        private string strKeyFiled = "User_Id";

        private DataTable dtConst = null;
        private DataTable dtShow = null;

        private string strNoEnableCtrIds = string.Empty;
        private Control CtrFirstEditContr = null;
        private string[] strFileds = null;
        private bool blSetDefault = false;
        List<CheckedComboBoxEdit> arrcklis = new List<CheckedComboBoxEdit>();
        #endregion

        private void Txt_Enter(object sender, EventArgs e)
        {
            strFocusedContrName = (sender as Control).Name;
            FocusedControl = sender as Control;
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
        public frmBseUserEdit()
        {
            InitializeComponent();
            InitContr();
        }
        private void InitContr()
        {
            if (dsLoad != null)
                return;

            dsLoad = this.GetFrmLoadDs(this.Name);
            dsLoad.AcceptChanges();
            dtShow = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];

            GridViewEdit = gridVMain;
            ParentControl = gcInfo;
            BtnEnterSave = btnSave;

            int igcHeight;
            Rectangle rect = SystemInformation.VirtualScreen;
            StaticFunctions.ShowGridControl(gridVMain, dtShow, dtConst);

            #region gcInfo
            List<Control> lisGcContrs = StaticFunctions.ShowGcContrs(gcInfo, rect.Width-50, dtShow, dtConst, true, 50, true, arrContrSeq, false
                , out blSetDefault, out strNoEnableCtrIds, out strFileds, out CtrFirstEditContr, out igcHeight);
            splitContainerControl2.SplitterPosition = (igcHeight > 100 ? igcHeight + 71 : 171);
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
                        arrcklis.Add(chkdpl);
                        break;
                    case "DevExpress.XtraEditors.DateEdit":
                        break;
                    default:
                        break;
                }
            }
            #endregion
        }
        public override void InitialByParam(string Mode, string strParam, DataTable dt)
        {
            base.InitialByParam(Mode, strParam, dt);
            RefreshItem();
            SetWMode("VIEW");
        }
        public override void RefreshItem()
        {
            GetCurrAllItem();
            rdState_CheckedChanged(null, null);
        }
        public override void GetCurrAllItem()
        {
            List<string> lisSpParmValue =new List<string>();
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                    "1"});
            DataSet ds = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            if (ds == null)
                return;

            frmDataTable = ds.Tables[0];
            frmDataTable.AcceptChanges();

            blInitBound = true;
            gridCMain.DataSource = frmDataTable.DefaultView;
            gridVMain.BestFitColumns();
            blInitBound = false;
            gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVMain.FocusedRowHandle));
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

            if (dr.RowState != DataRowState.Added && frmEditorMode != "VIEW")
            {
                SetWMode("VIEW");
            }
            StaticFunctions.SetControlBindings(gcInfo, gv.GridControl.DataSource as DataView);
            foreach (CheckedComboBoxEdit ckblis in arrcklis)
            {
                ckblis.EditValue = dr[ckblis.Tag.ToString()];
                ckblis.RefreshEditValue();
            }
        }
        public override void SetWMode(string strMode)
        {
            this.frmEditorMode = strMode;
            switch (strMode)
            {
                case "VIEW":
                    StaticFunctions.SetBtnEnabled(new Component[] { btnEdit, btnAdd, btnRole, btnSetPsw }, true);
                    StaticFunctions.SetBtnEnabled(new Component[] { btnCancel, btnSave }, false);
                    StaticFunctions.SetControlEnable(gcInfo, false, strNoEnableCtrIds);
                    break;

                case "ADD":
                    DataRow drNew = this.frmDataTable.NewRow();
                    if (blSetDefault)
                    {
                        StaticFunctions.SetContrDefaultValue(gcInfo, dtShow, drNew);
                    }
                    blInitBound = true;
                    frmDataTable.Rows.InsertAt(drNew, 0);//可能引发gridView1_FocusedRowChanged
                    gridVMain.MoveFirst();
                    blInitBound = false;
                    gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));

                    StaticFunctions.SetBtnEnabled(new Component[] { btnEdit, btnAdd, btnRole, btnSetPsw }, false);
                    StaticFunctions.SetBtnEnabled(new Component[] { btnCancel, btnSave }, true);
                    StaticFunctions.SetControlEnable(gcInfo, true, strNoEnableCtrIds);
                    StaticFunctions.SetFirstEditContrSelect(CtrFirstEditContr);
                    break;

                case "EDIT":
                    StaticFunctions.SetBtnEnabled(new Component[] { btnEdit, btnAdd, btnRole, btnSetPsw }, false);
                    StaticFunctions.SetBtnEnabled(new Component[] { btnCancel, btnSave }, true);
                    StaticFunctions.SetControlEnable(gcInfo, true, strNoEnableCtrIds);
                    StaticFunctions.SetFirstEditContrSelect(CtrFirstEditContr);
                    break;

                default:
                    break;
            }
        }
        private void btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;
            DataRow dr = gridVMain.GetFocusedDataRow();
            switch (btn.Name)
            {
                case "btnExcel":
                    this.Cursor = Cursors.WaitCursor;
                    StaticFunctions.GridViewExportToExcel(gridVMain, "系统用户信息", null);
                    this.Cursor = Cursors.Arrow;
                    break;

                case "btnAdd":
                    gridVMain.ClearColumnsFilter();
                    gridVMain.ClearSorting();
                    SetWMode("ADD");
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
                    gridVMain_FocusedRowChanged(gridVMain, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridVMain.FocusedRowHandle));
                    break;

                case "btnSave":
                    if (dr == null)
                    {
                        return;
                    }
                    DoSave();
                    break;

                case "btnSetPsw":
                    if (dr == null)
                    {
                        return;
                    }
                    if (MessageBox.Show("是否确认重置当前用户密码为 1？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                        return;

                    string[] strKey = "strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                    DataTable dtAdd = this.DataRequest_By_DataTable(strSpName,
                        strKey, new string[] { "PassWord='1'",
                         dr[strKeyFiled].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "3"});
                    if (dtAdd == null)
                    {
                        return;
                    }
                    MessageBox.Show("操作完成.");
                    break;

                case "btnRole":
                    if (dr == null)
                    {
                        return;
                    }
                    frmBseUserRoleEdit frm = new frmBseUserRoleEdit();
                    frm.UserId = dr["User_Id"].ToString();
                    frm.UserInfo = "姓名：" + dr["Name"].ToString() + " 卡号：" + dr["CardNub"].ToString() + " 编号：" + dr["Number"].ToString();
                    frm.ShowDialog();
                    break;

                case "btnCopy":
                    if (dr == null)
                    {
                        return;
                    }
                    if (txtSrcNumber.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("请输入来源员工工号.");
                        return;
                    }
                    List<string> lisSpParmValue = new List<string>();
                    string[] strKeyCopy = "Number,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                    lisSpParmValue.AddRange(new string[] { 
                         txtSrcNumber.Text.Trim(),
                         dr[strKeyFiled].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "21"});
                    DataTable dtCopy = this.DataRequest_By_DataTable(strSpName, strKeyCopy, lisSpParmValue.ToArray());
                    if (dtCopy == null)
                    {
                        return;
                    }
                    MessageBox.Show("操作完成.");
                    break;

                case "btnRight":
                    if (dr == null)
                    {
                        return;
                    }
                    frmBseUserRight frm2 = new frmBseUserRight();
                    frm2.UserId = dr["User_Id"].ToString();
                    frm2.UserName = dr["Number"].ToString() + "—" + dr["Name"].ToString();
                    frm2.ShowDialog();
                    break;

                default:
                    break;
            }
        }
        private void DoSave()
        {
            DataRow dr = gridVMain.GetFocusedDataRow();
            if (dr == null)
                return;

            if (!StaticFunctions.CheckSave(dr, gcInfo, dtShow))
                return;

            string strField = string.Empty;
            string strValues = string.Empty;
            btnSave.Enabled = false;

            if (dr[strKeyFiled].ToString() == string.Empty)
            {
                strValues = StaticFunctions.GetAddValues(dr, strFileds, out strField);

                string strSpParmName = string.Empty;
                List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(gcInfo, dtShow, out strSpParmName);

                if (strSpParmName != string.Empty)
                    strSpParmName += ",";
                string[] strKey = (strSpParmName + "strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                lisSpParmValue.AddRange(new string[] {
                    strField,
                    strValues,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                    "2"});
                DataSet dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
                if (dtAdd == null)
                {
                    btnSave.Enabled = true;
                    return;
                }
                DataRow drNew = dtAdd.Tables[0].Rows[0];
                //string[] strKeys = "Material_Dt".Split(",".ToCharArray());
                //foreach (string strK in strKeys)
                //{
                //    dr[strK] = drNew[strK];
                //}
                dr[strKeyFiled] = drNew[strKeyFiled];
            }
            else
            {
                strValues = StaticFunctions.GetUpdateValues(frmDataTable, dr, strFileds);
                if (strValues != string.Empty)
                {
                    string strSpParmName = string.Empty;
                    List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(gcInfo, dtShow, out strSpParmName);

                    if (strSpParmName != string.Empty)
                        strSpParmName += ",";
                    string[] strKey = (strSpParmName + "strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                    lisSpParmValue.AddRange(new string[] { 
                         strValues,
                         dr[strKeyFiled].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "3"});
                    DataTable dtAdd = this.DataRequest_By_DataTable(strSpName, strKey, lisSpParmValue.ToArray());
                    if (dtAdd == null)
                    {
                        btnSave.Enabled = true;
                        return;
                    }

                    DataRow drNew = dtAdd.Rows[0];
                }
            }
            dr.AcceptChanges();
            SetWMode("VIEW");
        }

        private void rdState_CheckedChanged(object sender, EventArgs e)
        {
            if (rdState.Checked)
            {
                frmDataTable.DefaultView.RowFilter = "State=1";
            }
            else if (rdNoState.Checked)
            {
                frmDataTable.DefaultView.RowFilter = "State=0";
            }
            else
            {
                frmDataTable.DefaultView.RowFilter = string.Empty;
            }
        }
    }
}