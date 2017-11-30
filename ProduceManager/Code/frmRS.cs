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

namespace ProduceManager
{
    public partial class frmRS : frmEditorBase
    {
        private DataTable dtRpara = null;
        public string Rept_Key
        {
            get;
            set;
        }

        public frmRS()
        {
            InitializeComponent();
        }

        private void frmRS_Load(object sender, EventArgs e)
        {
        }

        private void AddRpParameterItem(string strName, string strValue)
        {
            if (string.IsNullOrEmpty(strName))
                return;

            if (dtRpara == null)
            {
                dtRpara = new DataTable();
                dtRpara.Columns.Add(new DataColumn("RP_KEY", System.Type.GetType("System.String")));
                dtRpara.Columns.Add(new DataColumn("RP_VAL", System.Type.GetType("System.String")));
            }
            DataRow[] drw = dtRpara.Select("RP_KEY = '" + strName + "'");
            if (drw.Length > 0)
            {
                drw[0]["RP_VAL"] = strValue;
            }
            else
            {
                DataRow drNew = dtRpara.NewRow();
                drNew["RP_KEY"] = strName;
                drNew["RP_VAL"] = strValue;
                dtRpara.Rows.Add(drNew);
            }
        }

        private void AddRpParameterItem(string[] strNames, string[] strValues)
        {
            if (strNames == null || strNames.Length <= 0)
                return;

            if (dtRpara == null)
            {
                dtRpara = new DataTable();
                dtRpara.Columns.Add(new DataColumn("RP_KEY", System.Type.GetType("System.String")));
                dtRpara.Columns.Add(new DataColumn("RP_VAL", System.Type.GetType("System.String")));
            }
            for (int i = 0; i < strNames.Length; i++)
            {
                DataRow[] drw = dtRpara.Select("RP_KEY = '" + strNames[i] + "'");
                if (drw.Length > 0)
                {
                    drw[0]["RP_VAL"] = strValues[i];
                }
                else
                {
                    DataRow drNew = dtRpara.NewRow();
                    drNew["RP_KEY"] = strNames[i];
                    drNew["RP_VAL"] = strValues[i];
                    dtRpara.Rows.Add(drNew);
                }
            }
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
            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            reportViewer1.LocalReport.DisplayName = strWinTitle;
            reportViewer1.LocalReport.ReportPath = strFilePath;
            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.LocalReport.EnableHyperlinks = true;

            Microsoft.Reporting.WinForms.ReportParameterInfoCollection RParameter = this.reportViewer1.LocalReport.GetParameters();
            for (int r = 0; r < RParameter.Count; r++)
            {
                if (RParameter[r].Name == "strUser")
                {
                    this.AddRpParameterItem(RParameter[r].Name, CApplication.App.CurrentSession.UserId.ToString());
                }
                else if (RParameter[r].Name == "strDept")
                {
                    this.AddRpParameterItem(RParameter[r].Name, CApplication.App.CurrentSession.DeptId.ToString());
                }
                else if (RParameter[r].Name == "strFy")
                {
                    this.AddRpParameterItem(RParameter[r].Name, CApplication.App.CurrentSession.FyId.ToString());
                }
            }
        }

        public void ShowReports(string strWinTitle, string strReportName, string[] strParams, string[] strParaVals, DataSet dsDataSource, bool blSetInit)
        {
            if (blSetInit || string.IsNullOrEmpty(reportViewer1.LocalReport.ReportPath))
                SetInial(strWinTitle, strReportName);

            AddRpParameterItem(strParams, strParaVals);

            if (dtRpara != null && dtRpara.Rows.Count > 0)
            {
                Microsoft.Reporting.WinForms.ReportParameter[] RParameters = new Microsoft.Reporting.WinForms.ReportParameter[dtRpara.Rows.Count];
                for (int i = 0; i < dtRpara.Rows.Count; i++)
                {
                    RParameters[i] = new Microsoft.Reporting.WinForms.ReportParameter();
                    Microsoft.Reporting.WinForms.ReportParameter RParameterItem = new Microsoft.Reporting.WinForms.ReportParameter();
                    RParameterItem.Name = dtRpara.Rows[i]["RP_KEY"].ToString();
                    RParameterItem.Values.Add(dtRpara.Rows[i]["RP_VAL"].ToString());
                    RParameters[i] = RParameterItem;
                }
                this.reportViewer1.LocalReport.SetParameters(RParameters);
            }
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            CApplication.App.CurrentSession.TimerId = 0;
            int k = msg.WParam.ToInt32();
            if (k == 27)//Esc
            {
                this.Close();
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
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
    }
}
