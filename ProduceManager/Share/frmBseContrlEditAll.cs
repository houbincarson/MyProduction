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
    public partial class frmBseContrlEditAll : frmEditorBase
    {
        private bool blInitBound = false;
        private DataSet dsLoad = null;
        private DataTable dtConst = null;
        private DataTable dtShow = null;

        private string strSpName = "Bse_Show_Add_Edit_Del";
        private string strBatchFormName = "frmSysBseShowEdit";
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
            public Control CtrParentControl
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

            public TabPageEdit(string strKeyId, string[] strFileds, GridView gv, Control gc, 
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

        public frmBseContrlEditAll()
        {
            InitializeComponent();

            InitContr();
        }
        private void InitContr()
        {
            if (dsLoad != null)
                return;

            dsLoad = this.GetFrmLoadDs("frmBseContrlEdit");
            dsLoad.AcceptChanges();
            dtShow = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];

            string[] strMainFileds;
            StaticFunctions.ShowGridControl(gridVMain, dtShow, dtConst, out strMainFileds);
            int igcHeight;
            Rectangle rect = SystemInformation.VirtualScreen;
            List<Control> lisGcContrs = StaticFunctions.ShowGroupControl(gcInfo, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);

            tabs.Add(0, new TabPageEdit("Show_Id", strMainFileds, gridVMain, gcInfo, "2", "3", "4"));
        }
        private void frmBsuSetQuery_Load(object sender, EventArgs e)
        {
            List<string> lisSpParmValue = new List<string>();
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {
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
                    case "btnAddIndex":
                        AddIndex();
                        break;
                    case "btnCopy":
                        Copy();
                        break;
                    case "btnEditBatch":
                        DoEditBatch();
                        break;
                    case "btnSave":
                        DoSave();
                        break;
                    case "btnDelete":
                        DoDelete();
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
        private void DoEditBatch()
        {
            if (this.gridVMain.SelectedRowsCount <= 0)
            {
                return;
            }
            DataTable dtInfo = (gridCMain.DataSource as DataView).Table;
            frmBatchEdit frm = new frmBatchEdit(strBatchFormName);
            frm.frmDataTable = dtInfo.Clone();
            if (frm.ShowDialog(this) != DialogResult.Yes)
                return;

            DataRow drEdit = frm.frmDataTable.Rows[0];
            List<DataRow> lisRows = new List<DataRow>();
            foreach (int i in gridVMain.GetSelectedRows())
            {
                DataRow dr = gridVMain.GetDataRow(i);
                lisRows.Add(dr);
            }
            foreach (DataRow dr in lisRows)
            {
                foreach (string strFiled in frm.strFileds)
                {
                    if (drEdit[strFiled] == DBNull.Value || drEdit[strFiled].ToString() == string.Empty)
                        continue;

                    dr[strFiled] = drEdit[strFiled];
                }
            }
        }
        private void AddIndex()
        {
            TabPageEdit tab = tabs[xtabItemInfo.SelectedTabPageIndex];
            if (tab.GridViewEdit.SelectedRowsCount <= 0)
                return;

            int idx = int.Parse(txtIdx.EditValue.ToString());
            foreach (int i in tab.GridViewEdit.GetSelectedRows())
            {
                DataRow dr = tab.GridViewEdit.GetDataRow(i);
                dr["ShowIndex"] = int.Parse(dr["ShowIndex"].ToString()) + idx;
            }
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
                    string[] strKey = "strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                    lisSpParmValue.AddRange(new string[] {
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

            StaticFunctions.SetControlBindings(tab.CtrParentControl, tab.GridViewEdit.GridControl.DataSource as DataView, dr);
        }
    }

}
