using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace ProduceManager
{
    public partial class frmSysRpts : frmEditorBase
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
        private bool blSysProcess = false;

        private DataSet dsFormAdt = null;
        private DataSet dsFormUkyndaAdt = null;
        private DataRow drMain = null;
        private string strSpName = string.Empty;
        private string strQueryFlag = string.Empty;
        private string strBusClassName = string.Empty;
        private string strAllowList = string.Empty;

        private Dictionary<string, ReportViewer> gcItems = new Dictionary<string, ReportViewer>();
        #endregion

        public frmSysRpts()
        {
            InitializeComponent();
        }
        private void frmRS_Load(object sender, EventArgs e)
        {
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

            int igcHeight;
            Rectangle rect = SystemInformation.VirtualScreen;
            List<Control> lisGcContrs = StaticFunctions.ShowGroupControl(gcQuery, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
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

            List<BarButtonItem> lisBarItems = StaticFunctions.ShowBarButtonItem(dtBtns, bar2, "bar2", strAllowList, imageList1);
            foreach (BarButtonItem item in lisBarItems)
            {
                item.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_ItemClick);
            }
            try
            {
                gcItems = StaticFunctions.ShowTabReportViewer(dtTabs, xtabItemInfo, "xtabItemInfo", strAllowList, this.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show("错误:" + err.Message);
                bar2.Manager.Items["btnQuery"].Enabled = false;
                return;
            }
            if (gcItems.Count == 1)
            {
                xtabItemInfo.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            }
            else
            {
                xtabItemInfo.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
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
                    default:
                        DoBtnSpecial(e.Item.Name);
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
            gcQuery.Controls.Clear();
            xtabItemInfo.TabPages.Clear();
            arrContrSeq.Clear();
            dsLoad = null;
            InitContr();
            frmRS_Load(null, null);
        }
        private void Query()
        {
            if (!StaticFunctions.CheckSave(gcQuery, dtShow))
                return;

            xtabItemInfo.Select();

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
            
            try
            {
                ShowReports(dtAdd);
            }
            catch (Exception err)
            {
                MessageBox.Show("错误:" + err.InnerException.Message);
            }

            ////打印Reporting service 有待完善，打印字体太大
            //string strFilePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\SysRpt\\" + "Rpt_Fy_ProductionDate" + ".rdlc";
            //using (ReportPrint rpt = new ReportPrint(strFilePath, dtAdd, "RptSto"))
            //{
            //    rpt.PrintRpt();
            //}
        }
        private void ShowReports(DataSet dsDataSource)
        {
            if (dsDataSource == null)
                return;

            foreach (string strKey in gcItems.Keys)
            {
                ReportViewer reportViewer1 = gcItems[strKey];
                IList<string> lisDs = reportViewer1.LocalReport.GetDataSourceNames();
                foreach (string strds in lisDs)
                {
                    if (!dsDataSource.Tables.Contains(strKey + "-" + strds))
                        continue;

                    if (reportViewer1.LocalReport.DataSources[strds] == null || reportViewer1.LocalReport.DataSources[strds].Value == null)
                    {
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource(strds, dsDataSource.Tables[strKey + "-" + strds]));
                    }
                    else
                    {
                        reportViewer1.LocalReport.DataSources[strds].Value = null;
                        reportViewer1.LocalReport.DataSources[strds].Value = dsDataSource.Tables[strKey + "-" + strds];
                    }
                }

                reportViewer1.RefreshReport();
                //reportViewer1.LocalReport.Refresh();
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

        #region 可能需要扩充的事件
        private void DoBtnSpecial(string strBtnName)
        {
            DataRow[] drBtns = dtBtns.Select("BtnName='" + strBtnName + "'");
            if (drBtns.Length <= 0)
                return;

            DataRow drBtn = drBtns[0];
            if (drBtn["IsRptBtn"].ToString() == "True")
            {
                StaticFunctions.DoShowRpt(drBtn, this.ParentForm);
            }
            else if (drBtn["IsFormLink"].ToString() == "True")
            {
                StaticFunctions.DoOpenLinkForm(drBtn,this.ParentForm,null);
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
        #endregion

    }
}
