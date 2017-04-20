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
using System.Threading;

namespace ProduceManager
{
    public partial class frmSysBusRpts : frmEditorBase
    {
        #region private Params
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
        private string strSpName = string.Empty;
        private string strQueryFlag = string.Empty;
        private string strBusClassName = string.Empty;
        private string strAllowList = string.Empty;

        private Dictionary<string, GridView> gcItems = new Dictionary<string, GridView>();
        private List<RepositoryItemImageEdit> lisrepImg = new List<RepositoryItemImageEdit>();
        #endregion

        public frmSysBusRpts()
        {
            InitializeComponent();
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
            strQueryFlag = drMain["QueryFlag"].ToString();

            dtBtns = dsLoad.Tables[3];
            dtTabs = dsLoad.Tables[4];
            dtGroupC = dsLoad.Tables[5];
            dtSte = dsLoad.Tables[6];
            dtContr = dsLoad.Tables[7];
            dtBtnsM = dsLoad.Tables[8];
            dtSp = dsLoad.Tables[9];

            ParentControl = gcQuery;
            int igcHeight;
            Rectangle rect = SystemInformation.VirtualScreen;
            List<Control> lisGcContrs = StaticFunctions.ShowGroupControl(gcQuery, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);

            List<BarButtonItem> lisBarItems = StaticFunctions.ShowBarButtonItem(dtBtns, bar2, "bar2", strAllowList, imageList1);
            foreach (BarButtonItem item in lisBarItems)
            {
                item.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            }
            gcItems = StaticFunctions.ShowTabItem(dtTabs, xtabItemInfo, "xtabItemInfo", strAllowList, lisrepImg);
            foreach (string strGv in gcItems.Keys)
            {
                StaticFunctions.ShowGridControl(gcItems[strGv], dtShow, dtConst);
                StaticFunctions.SaveOrLoadDelLayout(gcItems[strGv], this.strBusClassName + "_" + strGv, "LOAD");

                StaticFunctions.SetGridViewStyleFormatCondition(gcItems[strGv], dtBtnsM);
            }

            #region gcQuery
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
            #endregion
            StaticFunctions.SetBtnStyle(barManager1, null, drMain);

            if (drMain["HideQuery"].ToString() == "True")
            {
                gcQuery.Visible = false;
            }

            if (xtabItemInfo.TabPages.Count == 1)
            {
                xtabItemInfo.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            }
            else
            {
                xtabItemInfo.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
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
        }
        private void uct_onClosePopUp(object sender)
        {
        }

        private void frmSysBusQuery_Load(object sender, EventArgs e)
        {
            if (drMain["LoadQuery"].ToString() == "True")
            {
                btn_ItemClick(barManager1, new ItemClickEventArgs(bar2.Manager.Items["btnQuery"], null));
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
                    case "btnReLoad":
                        DoReLoad();
                        break;
                    case "btnExcel":
                        DoExcel();
                        break;
                    case "btnSaveLayOut":
                        foreach (string strGv in gcItems.Keys)
                        {
                            StaticFunctions.SaveOrLoadDelLayout(gcItems[strGv], this.strBusClassName + "_" + strGv, "SAVE");
                        }
                        MessageBox.Show("成功保存样式.");
                        break;
                    case "btnDeleteLayOut":
                        foreach (string strGv in gcItems.Keys)
                        {
                            StaticFunctions.SaveOrLoadDelLayout(gcItems[strGv], this.strBusClassName + "_" + strGv, "DELETE");
                        }
                        MessageBox.Show("成功删除样式.");
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

        private void DoReLoad()
        {
            bar2.Reset();
            gcItems.Clear();
            lisrepImg.Clear();
            gcQuery.Controls.Clear();
            xtabItemInfo.TabPages.Clear();
            arrContrSeq.Clear();
            dsLoad = null;
            InitContr();
            frmSysBusQuery_Load(null, null);
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
            StaticFunctions.GridViewExportToExcel(gv, xtabItemInfo.SelectedTabPage.Text, null);
            this.Cursor = Cursors.Arrow;
        }

        public override void RefreshItem()
        {
            btn_ItemClick(barManager1, new ItemClickEventArgs(bar2.Manager.Items["btnQuery"], null));
        }
        private void Query()
        {
            if (!StaticFunctions.CheckSave(gcQuery, dtShow))
                return;

            string strTabSel = xtabItemInfo.SelectedTabPage.Name;
            DataRow[] drTabs = dtTabs.Select("TabName='" + strTabSel + "'");
            if (drTabs.Length <= 0)
                return;

            DataRow drTab = drTabs[0];
            GridView gv = gcItems[drTab["GridViewName"].ToString()];
            gv.GridControl.Select();

            string strSpParmName = string.Empty;
            List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(gcQuery, dtShow, out strSpParmName);

            if (strSpParmName != string.Empty)
                strSpParmName += ",";
            string[] strKey = (strSpParmName + "EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     strQueryFlag});
            DataSet dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            if (dtAdd == null)
                return;

            BoundGridView(dtAdd);
        }
        private void BoundGridView(DataSet ds)
        {
            DataTable dt = null;
            foreach (string strGv in gcItems.Keys)
            {
                if (!ds.Tables.Contains(strGv))
                    continue;

                dt = ds.Tables[strGv];
                dt.AcceptChanges();

                gcItems[strGv].GridControl.DataSource = dt.DefaultView;
                gcItems[strGv].BestFitColumns();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int k = msg.WParam.ToInt32();
            if (k == 13)
            {
                btn_ItemClick(barManager1, new ItemClickEventArgs(bar2.Manager.Items["btnQuery"], null));
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}