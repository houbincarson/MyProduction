using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;

namespace ProduceManager
{
    public partial class frmBsuSetEdit : frmEditorBase
    {
        private bool blInitBound = false;
        private DataSet dsLoad = null;
        private DataTable dtConst = null;
        private DataTable dtShow = null;

        private string strSpName = "Bse_BsuSet_Add_Edit_Del";
        private string strClassType = "2";
        Dictionary<int, TabPageEdit> tabs = new Dictionary<int, TabPageEdit>();

        private class TabPageEdit
        {
            public string StrKeyId
            {
                get;
                set;
            }
            public string[] StrFileds
            {
                get;
                set;
            }
            public GridView GridViewEdit
            {
                get;
                set;
            }
            public Control[] CtrParentControl
            {
                get;
                set;
            }
            public string AddFlag
            {
                get;
                set;
            }
            public string EditFlag
            {
                get;
                set;
            }
            public string DeleteFlag
            {
                get;
                set;
            }

            public TabPageEdit(string strKeyId, string[] strFileds, GridView gv, Control[] gc,
                string strAddFlag, string strEditFlag, string strDeleteFlag)
            {
                StrKeyId = strKeyId;
                StrFileds = strFileds;
                GridViewEdit = gv;
                CtrParentControl = gc;
                AddFlag = strAddFlag;
                EditFlag = strEditFlag;
                DeleteFlag = strDeleteFlag;
            }
        }

        public frmBsuSetEdit()
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

            string[] strMainFileds;
            string[] strTabFileds;
            string[] strBtnFileds;
            string[] strGcFileds;
            string[] strSteFileds;
            string[] strCtrlFileds;
            string[] strBtnMFileds;
            string[] strSpFileds;
            string[] strExlFileds;
            StaticFunctions.ShowGridControl(gridVMain, dtShow, dtConst, out strMainFileds);
            StaticFunctions.ShowGridControl(gridVTab, dtShow, dtConst, out strTabFileds);
            StaticFunctions.ShowGridControl(gridVBtn, dtShow, dtConst, out strBtnFileds);
            StaticFunctions.ShowGridControl(gridVGroupC, dtShow, dtConst, out strGcFileds);
            StaticFunctions.ShowGridControl(gridVSte, dtShow, dtConst, out strSteFileds);
            StaticFunctions.ShowGridControl(gridVCContl, dtShow, dtConst, out strCtrlFileds);
            StaticFunctions.ShowGridControl(gridVBtnM, dtShow, dtConst, out strBtnMFileds);
            StaticFunctions.ShowGridControl(gridVSp, dtShow, dtConst, out strSpFileds);
            StaticFunctions.ShowGridControl(gridVExcel, dtShow, dtConst, out strExlFileds);
            int igcHeight;
            Rectangle rect = SystemInformation.VirtualScreen;
            List<Control> lisGcContrs = StaticFunctions.ShowGroupControl(gcMain, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            lisGcContrs = StaticFunctions.ShowGroupControl(gcTab, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            lisGcContrs = StaticFunctions.ShowGroupControl(gcGroupC, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            lisGcContrs = StaticFunctions.ShowGroupControl(gcSte, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            lisGcContrs = StaticFunctions.ShowGroupControl(gcBtn, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            lisGcContrs = StaticFunctions.ShowGroupControl(gcBtnYB, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            lisGcContrs = StaticFunctions.ShowGroupControl(gcBtnBatchAdd, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            lisGcContrs = StaticFunctions.ShowGroupControl(gcBtnBatchEdit, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            lisGcContrs = StaticFunctions.ShowGroupControl(gcBtnExcel, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            lisGcContrs = StaticFunctions.ShowGroupControl(gcBtnSplit, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            lisGcContrs = StaticFunctions.ShowGroupControl(gcCContl, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            lisGcContrs = StaticFunctions.ShowGroupControl(gcBtnM, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            lisGcContrs = StaticFunctions.ShowGroupControl(gcSp, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            lisGcContrs = StaticFunctions.ShowGroupControl(gcExcel, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);

            tabs.Add(0, new TabPageEdit("BsuSetMain_Id", strMainFileds, gridVMain, new Control[] { gcMain }, "2", "3", "4"));
            tabs.Add(1, new TabPageEdit("BsuSetTab_Id", strTabFileds, gridVTab, new Control[] { gcTab }, "22", "23", "24"));
            tabs.Add(2, new TabPageEdit("BsuSetBtn_Id", strBtnFileds, gridVBtn, new Control[] { gcBtn, gcBtnYB, gcBtnBatchAdd, gcBtnBatchEdit, gcBtnExcel, gcBtnSplit }, "32", "33", "34"));
            tabs.Add(3, new TabPageEdit("BsuSetGroupC_Id", strGcFileds, gridVGroupC, new Control[] { gcGroupC }, "42", "43", "44"));
            tabs.Add(4, new TabPageEdit("BsuSetSte_Id", strSteFileds, gridVSte, new Control[] { gcSte }, "52", "53", "54"));
            tabs.Add(5, new TabPageEdit("BsuSetCtrl_Id", strCtrlFileds, gridVCContl, new Control[] { gcCContl }, "62", "63", "64"));
            tabs.Add(6, new TabPageEdit("BsuSetBtnM_Id", strBtnMFileds, gridVBtnM, new Control[] { gcBtnM }, "72", "73", "74"));
            tabs.Add(7, new TabPageEdit("BsuSetSp_Id", strSpFileds, gridVSp, new Control[] { gcSp }, "82", "83", "84"));
            tabs.Add(8, new TabPageEdit("Exc_Id", strExlFileds, gridVExcel, new Control[] { gcExcel }, "92", "93", "94"));
        }
        private void frmBsuSetQuery_Load(object sender, EventArgs e)
        {
            List<string> lisSpParmValue = new List<string>();
            string[] strKey = "ClassType,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {
                strClassType,
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                "1"});
            DataSet dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            if (dtAdd == null)
            {
                return;
            }
            blInitBound = true;
            gridCMain.DataSource = dtAdd.Tables[0].DefaultView;
            gridVMain.BestFitColumns();
            gridCTab.DataSource = dtAdd.Tables[1].DefaultView;
            gridVTab.BestFitColumns();
            gridCBtn.DataSource = dtAdd.Tables[2].DefaultView;
            gridVBtn.BestFitColumns();
            gridCGroupC.DataSource = dtAdd.Tables[3].DefaultView;
            gridVGroupC.BestFitColumns();
            gridCSte.DataSource = dtAdd.Tables[4].DefaultView;
            gridVSte.BestFitColumns();
            this.gridCContl.DataSource = dtAdd.Tables[5].DefaultView;
            gridVCContl.BestFitColumns();
            this.gridCBtnM.DataSource = dtAdd.Tables[6].DefaultView;
            gridVBtnM.BestFitColumns();
            this.gridCSp.DataSource = dtAdd.Tables[7].DefaultView;
            gridVSp.BestFitColumns();
            this.gridCExcel.DataSource = dtAdd.Tables[8].DefaultView;
            gridVExcel.BestFitColumns();
            blInitBound = false;

            TabPageEdit tab = tabs[xtabItemInfo.SelectedTabPageIndex];
            gridView_FocusedRowChanged(tab.GridViewEdit, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, tab.GridViewEdit.FocusedRowHandle));
        }
        public override void RefreshItem()
        {
            frmBsuSetQuery_Load(null, null);
        }
        private void btn_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                switch (e.Item.Name)
                {
                    case "btnQuery":
                        RefreshItem();
                        break;
                    case "btnAdd":
                        DoAdd();
                        break;
                    case "btnCopy":
                        Copy();
                        break;
                    case "btnSave":
                        DoSave();
                        break;
                    case "btnDelete":
                        DoDelete();
                        break;
                    case "btnFastSet":
                        DoFastSet();
                        break;
                    case "btnAddInfo":
                        DoFastAddInfo();
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
            }
        }
        private void DoAdd()
        {
            TabPageEdit tab = tabs[xtabItemInfo.SelectedTabPageIndex];
            tab.GridViewEdit.ClearColumnsFilter();
            tab.GridViewEdit.ClearSorting();

            DataTable dtInfo = (tab.GridViewEdit.GridControl.DataSource as DataView).Table;
            DataRow drNew = dtInfo.NewRow();
            foreach (Control ctrParent in tab.CtrParentControl)
            {
                StaticFunctions.SetContrDefaultValue(ctrParent, dtShow, drNew);
            }

            blInitBound = true;
            dtInfo.Rows.Add(drNew);//可能引发gridView1_FocusedRowChanged
            tab.GridViewEdit.MoveLast();
            blInitBound = false;
            gridView_FocusedRowChanged(tab.GridViewEdit, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, tab.GridViewEdit.FocusedRowHandle));
        }
        private void Copy()
        {
            TabPageEdit tab = tabs[xtabItemInfo.SelectedTabPageIndex];
            if (tab.GridViewEdit.SelectedRowsCount <= 0)
                return;

            DataTable dtInfo = (tab.GridViewEdit.GridControl.DataSource as DataView).Table;
            blInitBound = true;
            foreach (int i in tab.GridViewEdit.GetSelectedRows())
            {
                DataRow dr = tab.GridViewEdit.GetDataRow(i);
                DataRow drNew = dtInfo.NewRow();
                drNew.ItemArray = dr.ItemArray;
                drNew[tab.StrKeyId] = DBNull.Value;
                dtInfo.Rows.Add(drNew);
            }
            tab.GridViewEdit.MoveLast();
            blInitBound = false;
            gridView_FocusedRowChanged(tab.GridViewEdit, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, tab.GridViewEdit.FocusedRowHandle));
        }
        private void DoSave()
        {
            string strField = string.Empty;
            string strValues = string.Empty;
            TabPageEdit tab = tabs[xtabItemInfo.SelectedTabPageIndex];
            int iFocu = tab.GridViewEdit.FocusedRowHandle;
            tab.GridViewEdit.FocusedRowHandle = -1;
            tab.GridViewEdit.FocusedRowHandle = iFocu;
            DataTable dtInfo = (tab.GridViewEdit.GridControl.DataSource as DataView).Table;
            if (dtInfo.GetChanges() == null)
                return;
            foreach (DataRow dr in dtInfo.Rows)
            {
                if (dr.RowState == DataRowState.Added)
                {
                    strValues = StaticFunctions.GetAddValues(dr, tab.StrFileds, out strField);
                    List<string> lisSpParmValue = new List<string>();
                    string[] strKey = "ClassType,strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                    lisSpParmValue.AddRange(new string[] {strClassType,
                                strField,
                                strValues,
                                CApplication.App.CurrentSession.UserId.ToString(),
                                CApplication.App.CurrentSession.DeptId.ToString(),
                                CApplication.App.CurrentSession.FyId.ToString(),
                                tab.AddFlag});
                    DataSet dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
                    if (dtAdd == null)
                    {
                        return;
                    }
                    DataRow drNew = dtAdd.Tables[0].Rows[0];
                    dr[tab.StrKeyId] = drNew[tab.StrKeyId];
                }
                else if (dr.RowState == DataRowState.Modified)
                {
                    strValues = StaticFunctions.GetUpdateValues(dtInfo, dr, tab.StrFileds);
                    if (strValues == string.Empty)
                        continue;

                    List<string> lisSpParmValue = new List<string>();
                    string[] strKey = "strEditSql,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                    lisSpParmValue.AddRange(new string[] { 
                                     strValues,
                                     dr[tab.StrKeyId].ToString(),
                                     CApplication.App.CurrentSession.UserId.ToString(),
                                     CApplication.App.CurrentSession.DeptId.ToString(),
                                     CApplication.App.CurrentSession.FyId.ToString(),
                                     tab.EditFlag});
                    DataTable dtAdd = this.DataRequest_By_DataTable(strSpName, strKey, lisSpParmValue.ToArray());
                    if (dtAdd == null)
                    {
                        return;
                    }
                }
            }
            dtInfo.AcceptChanges();
            MessageBox.Show("操作完成.");
        }
        private void DoDelete()
        {
            TabPageEdit tab = tabs[xtabItemInfo.SelectedTabPageIndex];
            if (tab.GridViewEdit.SelectedRowsCount <= 0)
                return;

            string strKeyIds = string.Empty;
            foreach (int i in tab.GridViewEdit.GetSelectedRows())
            {
                DataRow dr = tab.GridViewEdit.GetDataRow(i);
                if (dr.RowState == DataRowState.Added)
                    continue;
                strKeyIds += strKeyIds == string.Empty ? dr[tab.StrKeyId].ToString() : "," + dr[tab.StrKeyId].ToString();
            }
            if (strKeyIds == string.Empty)
            {
                return;
            }
            if (MessageBox.Show("是否删除", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;
            List<string> lisSpParmValue = new List<string>();
            string[] strKey = "strKeyIds,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {
                strKeyIds,
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                tab.DeleteFlag});
            DataSet dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            if (dtAdd == null)
            {
                return;
            }
            blInitBound = true;
            tab.GridViewEdit.DeleteSelectedRows();
            DataTable dtInfo = (tab.GridViewEdit.GridControl.DataSource as DataView).Table;
            dtInfo.AcceptChanges();
            blInitBound = false;
            gridView_FocusedRowChanged(tab.GridViewEdit, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, tab.GridViewEdit.FocusedRowHandle));
            MessageBox.Show("操作完成.");
        }
        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1 || blInitBound)
                return;

            TabPageEdit tab = tabs[xtabItemInfo.SelectedTabPageIndex];
            DataRow dr = tab.GridViewEdit.GetFocusedDataRow();
            if (dr == null)
                return;

            foreach (Control ctrParent in tab.CtrParentControl)
            {
                StaticFunctions.SetControlBindings(ctrParent, tab.GridViewEdit.GridControl.DataSource as DataView, dr);
            }
        }

        private void xtabItemInfo_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            TabPageEdit tab = tabs[xtabItemInfo.SelectedTabPageIndex];
            gridView_FocusedRowChanged(tab.GridViewEdit, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, tab.GridViewEdit.FocusedRowHandle));
        }

        private void DoFastSet()
        {
            Form frmEx = StaticFunctions.GetExistedChildForm(this.ParentForm, "frmBsuSetQueryFastAddInfo");
            if (frmEx != null)
            {
                frmEx.Close();
                frmEx.Dispose();
            }
            frmBsuSetQueryFastAddInfo frm = new frmBsuSetQueryFastAddInfo();
            frm.Q_OR_E = "E";
            frm.StrType = "1";
            frm.MdiParent = this.ParentForm;
            frm.FrmEditorBaseP = this;
            frm.Show();
        }
        private void DoFastAddInfo()
        {
            TabPageEdit tab = tabs[xtabItemInfo.SelectedTabPageIndex];
            DataRow drFoc = tab.GridViewEdit.GetFocusedDataRow();
            if (drFoc == null)
                return;

            Form frmEx = StaticFunctions.GetExistedChildForm(this.ParentForm, "frmBsuSetQueryFastAddInfo");
            if (frmEx != null)
            {
                frmEx.Close();
                frmEx.Dispose();
            }
            frmBsuSetQueryFastAddInfo frm = new frmBsuSetQueryFastAddInfo();
            frm.Q_OR_E = "E";
            frm.SetClass = drFoc["Menus_Class"].ToString();
            frm.StrType = "2";
            frm.MdiParent = this.ParentForm;
            frm.FrmEditorBaseP = this;
            frm.Show();
        }
    }
}
