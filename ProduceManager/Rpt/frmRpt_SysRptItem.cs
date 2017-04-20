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

namespace ProduceManager
{
    public partial class frmRpt_SysRptItem : frmEditorBase
    {
        #region private Params
        private DataSet dsLoad = null;
        private string strSpName = "Rpt_RS_DS";
        private DataTable dtConst = null;
        private DataTable dtShow = null;
        private int iPosxBtn = 0;
        private string strParas=string.Empty;
        private string strReportName = string.Empty;
        #endregion

        public string Rept_Key
        {
            get;
            set;
        }

        public frmRpt_SysRptItem(string strRptTitle, string strReportName, string strParas)
        {
            InitializeComponent();
            this.strParas = strParas;
            this.strReportName = strReportName;

            Rectangle rect = SystemInformation.VirtualScreen;
            iPosxBtn = rect.Width - 50 - btnOk.Width;
            btnOk.Location = new System.Drawing.Point(iPosxBtn, 15);
            InitContr();
            SetInial(strRptTitle, strReportName);
        }

        private void InitContr()
        {
            if (dsLoad != null)
                return;

            dsLoad = this.GetFrmLoadDs(this.Name, this.strReportName);
            dtShow = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];
        }

        private void SetInial(string strWinTitle, string strReportName)
        {
            this.Text = strWinTitle;
            this.Rept_Key = strReportName;
            string strFilePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\SysRpt\\" + strReportName + ".rdlc";
            if (!File.Exists(strFilePath))
            {
                throw new Exception("不存在报表文件：" + strReportName);
            }

            for (int i = gcQuery.Controls.Count - 1; i >= 0; i--)
            {
                Control ctrl = gcQuery.Controls[i];
                if (ctrl.Name != "btnOk")
                {
                    gcQuery.Controls.Remove(ctrl);
                }
            }
            int igcHeight;
            List<Control> lisGcContrs = StaticFunctions.ShowGroupControl(gcQuery, iPosxBtn - 50, dtShow, strReportName, dtConst, true, 60, false, null, true, out igcHeight);
            foreach (Control ctrl in lisGcContrs)
            {
                switch (ctrl.GetType().ToString())
                {
                    case "DevExpress.XtraEditors.SimpleButton":
                        break;

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

                    default:
                        break;
                }
            }

            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            reportViewer1.LocalReport.DisplayName = strWinTitle;
            reportViewer1.LocalReport.ReportPath = strFilePath;
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

        private void ShowReports(DataSet dsDataSource)
        {
            if (dsDataSource != null)
            {
                if (dsDataSource.Tables.Count > 0)
                {
                    if (dsDataSource.Tables[dsDataSource.Tables.Count - 1].Columns.IndexOf("RPTDSNME") != -1)
                    {
                        string strDsNme = dsDataSource.Tables[dsDataSource.Tables.Count - 1].Rows[0]["RPTDSNME"].ToString();
                        string[] strDsNmes = strDsNme.Split(",".ToCharArray());
                        for (int i = 0; i < strDsNmes.Length; i++)
                        {
                            dsDataSource.Tables[i].TableName = strDsNmes[i];
                        }
                    }
                }
                IList<string> lisDs = this.reportViewer1.LocalReport.GetDataSourceNames();
                foreach (string strds in lisDs)
                {
                    if (dsDataSource.Tables.Contains(strds))
                    {
                        if (reportViewer1.LocalReport.DataSources[strds] == null 
                            || reportViewer1.LocalReport.DataSources[strds].Value == null)
                        {
                            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource(strds, dsDataSource.Tables[strds]));
                        }
                        else
                        {
                            reportViewer1.LocalReport.DataSources[strds].Value = null;
                            reportViewer1.LocalReport.DataSources[strds].Value = dsDataSource.Tables[strds];
                        }
                    }
                }
            }
            this.reportViewer1.RefreshReport();
            //this.reportViewer1.LocalReport.Refresh();
        }

        private void frmRS_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer1.Dispose();
        }

        private void reportViewer1_Print(object sender, ReportPrintEventArgs e)
        {

        }

        private void reportViewer1_ReportExport(object sender, ReportExportEventArgs e)
        {

        }

        private void reportViewer1_RenderingBegin(object sender, CancelEventArgs e)
        {

        }

        private void reportViewer1_RenderingComplete(object sender, RenderingCompleteEventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!StaticFunctions.CheckSave(gcQuery, dtShow,strReportName))
                return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnOk.Enabled = false;

                string strSpParmName = string.Empty;
                List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(gcQuery, dtShow,strReportName, out strSpParmName);

                if (!string.IsNullOrEmpty(strParas))
                {
                    string[] strParaVals = strParas.Split("&".ToCharArray());
                    foreach (string strParVal in strParaVals)
                    {
                        string[] strPVal = strParVal.Split("=".ToCharArray());
                        if (strPVal.Length == 2)
                        {
                            strSpParmName += strSpParmName == string.Empty ? strPVal[0] : "," + strPVal[0];
                            lisSpParmValue.Add(strPVal[1]);
                        }
                    }
                }
                if (strSpParmName != string.Empty)
                    strSpParmName += ",";
                string[] strKey = (strSpParmName + "EUser_Id,EDept_Id,Fy_Id").Split(",".ToCharArray());
                lisSpParmValue.AddRange(new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     });
                DataSet dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
                if (dtAdd == null)
                    return;
                ShowReports(dtAdd);
            }
            catch (Exception err)
            {
                MessageBox.Show("错误:" + err.Message);
            }
            finally
            {
                btnOk.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }
    }
}
