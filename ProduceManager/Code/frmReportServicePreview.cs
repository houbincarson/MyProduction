using System;
using System.Drawing;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Configuration;

namespace ProduceManager
{
    /// <summary>
    /// frmReportServicePreview 的摘要说明。
    /// </summary>
    public class frmReportServicePreview : frmEditorBase
    {
        private string _strUrl = string.Empty;
        private string _strOpenType = "OLD";
        Microsoft.Reporting.WinForms.ReportParameter[] RParameters;
        private DataTable dtRpara;

        public DataTable DtRpara
        {
            get { return dtRpara; }
            set { dtRpara = value; }
        }
        public bool blIsPrint = false;
        private string strUserId = string.Empty;
        private string strUserPass = string.Empty;
        private string strFileType = string.Empty;
        private string _strWinTitle = string.Empty;
        private string _strReportPath = string.Empty;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        public string Rept_Key
        {
            get;
            set;
        }

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public frmReportServicePreview()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        public frmReportServicePreview(string OpenType, string strWinTitle, string strReportPath, string strUser, string strPass)
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();
            _strOpenType = OpenType;

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
            this.Text = strWinTitle;
            strUserId = strUser;
            strUserPass = strPass;
            _strWinTitle = strWinTitle;
            _strReportPath = strReportPath;
            //ShowUrl(strWinTitle, strReportPath, null);
        }

        public frmReportServicePreview(string strWinTitle, string strReportPath, string strUser, string strPass)
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
            _strUrl = strReportPath;
            this.Text = strWinTitle;
            strUserId = strUser;
            strUserPass = strPass;
            _strWinTitle = strWinTitle;
            _strReportPath = strReportPath;
            //ShowUrl(strWinTitle, strReportPath, null);
        }

        private void InitReportByUrl(string strRpUrl)
        {
            if (strRpUrl == string.Empty)
                return;

            int intS = strRpUrl.IndexOf("?/") + 2;
            int intE = strRpUrl.IndexOf("&");
            if (intS == 1)
                intS = strRpUrl.ToUpper().IndexOf("ReportServer".ToUpper()) + 12;
            if (intS == 11)
                intS = 0;
            if (intE == -1)
                intE = strRpUrl.Length;
            string strRpPath = strRpUrl.Substring(intS, intE - intS);

            _strReportPath = strRpPath.StartsWith("/") ? strRpPath : "/" + strRpPath;
            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.reportViewer1.ServerReport.DisplayName = _strWinTitle;
            this.reportViewer1.ServerReport.ReportServerUrl = new Uri(this.frmReportServicesPath);// (System.Configuration.ConfigurationManager.AppSettings["ReportServices"]);//"http://172.20.28.17/ReportServer"
            this.reportViewer1.ServerReport.ReportPath = _strReportPath;// "/fdcf/fVslApTruckChargeSheet";
            //string strUser = ConfigurationManager.AppSettings["MyReportViewerUser"];
            //string strPsw = ConfigurationManager.AppSettings["MyReportViewerPassword"];
            //string domain = ConfigurationManager.AppSettings["MyReportViewerDomain"];
            //reportViewer1.ServerReport.ReportServerCredentials.SetFormsCredentials(null, strUser, strPsw, domain);
            Microsoft.Reporting.WinForms.ReportParameterInfoCollection RParameter = this.reportViewer1.ServerReport.GetParameters();
            for (int r = 0; r < RParameter.Count; r++)
            {
                try
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
                    else
                    {
                        string PRtemValue = StaticFunctions.GetFrmParamValue(strRpUrl, RParameter[r].Name, new char[] { '&' });
                        if (PRtemValue != string.Empty)
                            this.AddRpParameterItem(RParameter[r].Name, PRtemValue);
                    }
                }
                catch
                {
                }
            }


            if (dtRpara != null && dtRpara.Rows.Count > 0)
            {
                RParameters = new Microsoft.Reporting.WinForms.ReportParameter[dtRpara.Rows.Count];
                for (int i = 0; i < dtRpara.Rows.Count; i++)
                {
                    RParameters[i] = new Microsoft.Reporting.WinForms.ReportParameter();
                    Microsoft.Reporting.WinForms.ReportParameter RParameterItem = new Microsoft.Reporting.WinForms.ReportParameter();
                    RParameterItem.Name = dtRpara.Rows[i]["RP_KEY"].ToString();
                    RParameterItem.Values.Add(dtRpara.Rows[i]["RP_VAL"].ToString());
                    RParameters[i] = RParameterItem;
                }
                this.reportViewer1.ServerReport.SetParameters(RParameters);
            }
            this.reportViewer1.ShowParameterPrompts = true;
            this.reportViewer1.RefreshReport();
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.ReportServerUrl = new System.Uri("http://172.20.28.17/reportserver", System.UriKind.Absolute);
            this.reportViewer1.Size = new System.Drawing.Size(892, 626);
            this.reportViewer1.TabIndex = 0;
            // 
            // frmReportServicePreview
            // 
            this.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.Appearance.Options.UseFont = true;
            this.ClientSize = new System.Drawing.Size(892, 626);
            this.Controls.Add(this.reportViewer1);
            this.MinimizeBox = false;
            this.Name = "frmReportServicePreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmReportServicePreview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.frmReportServicePreview_Shown);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmReportServicePreview_Closing);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// 添加参数，注意大小写
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        public void AddRpParameterItem(string strName, string strValue)
        {
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
        /// <summary>
        /// 打开一WEB页面
        /// </summary>
        /// <param name="strWinTitle">窗口标题</param>
        /// <param name="strUrl">页面地址，如“http://www.123.com”</param>
        /// <param name="postData">向页面传递POST参数</param>
        public void ShowReports(string strWinTitle, string strReportPath, Object postData)
        {
            //AddRpParameterItem("strUser", "zhangbanghua");
            //AddRpParameterItem("strVsl", "minwu");
            strReportPath = strReportPath.StartsWith("/") ? strReportPath : "/" + strReportPath;
            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.reportViewer1.ServerReport.DisplayName = strWinTitle;
            this.reportViewer1.ServerReport.ReportServerUrl = new Uri(this.frmReportServicesPath);// (System.Configuration.ConfigurationManager.AppSettings["ReportServices"]);//"http://172.20.28.17/ReportServer"
            this.reportViewer1.ServerReport.ReportPath = strReportPath;// "/fdcf/fVslApTruckChargeSheet";
            //string strUser = ConfigurationManager.AppSettings["MyReportViewerUser"];
            //string strPsw = ConfigurationManager.AppSettings["MyReportViewerPassword"];
            //string domain = ConfigurationManager.AppSettings["MyReportViewerDomain"];
            //reportViewer1.ServerReport.ReportServerCredentials.SetFormsCredentials(null, strUser, strPsw, domain);
            Microsoft.Reporting.WinForms.ReportParameterInfoCollection RParameter = this.reportViewer1.ServerReport.GetParameters();

            if (dtRpara != null && dtRpara.Rows.Count > 0)
            {
                RParameters = new Microsoft.Reporting.WinForms.ReportParameter[dtRpara.Rows.Count];
                for (int i = 0; i < dtRpara.Rows.Count; i++)
                {
                    RParameters[i] = new Microsoft.Reporting.WinForms.ReportParameter();
                    Microsoft.Reporting.WinForms.ReportParameter RParameterItem = new Microsoft.Reporting.WinForms.ReportParameter();
                    RParameterItem.Name = dtRpara.Rows[i]["RP_KEY"].ToString();
                    RParameterItem.Values.Add(dtRpara.Rows[i]["RP_VAL"].ToString());
                    RParameters[i] = RParameterItem;
                }
                this.reportViewer1.ServerReport.SetParameters(RParameters);
            }
            this.reportViewer1.ShowParameterPrompts = true;
            this.reportViewer1.RefreshReport();


            //            // Create a new webrequest to the mentioned URL.
            ////			System.Net.WebRequest myWebRequest = System.Net.WebRequest.Create(strUrl);
            ////			myWebRequest.PreAuthenticate=true;
            ////			System.Net.NetworkCredential networkCredential = new System.Net.NetworkCredential(strUserId,strUserPass);
            ////			myWebRequest.Credentials = networkCredential;
            ////			System.Net.WebResponse myWebResponse = myWebRequest.GetResponse();

            //            this.Text = strWinTitle;
            //            Object optional = System.Reflection.Missing.Value;
            //            if(postData == null)
            //                postData = System.Reflection.Missing.Value;
            //            this.axWebBrowser1.Navigate(strUrl,ref optional,ref optional,ref optional,ref optional);
            ////			this.axWebBrowser1.Refresh2(0);

            //            this.strFileType = "URL";	
        }

        //        /// <summary>
        //        /// 打开并编辑一个EXCEL文件
        //        /// </summary>
        //        /// <param name="strFileName">Excel文件的位置，如 @"c:\a.xls"</param>
        //        public void ShowExcel(string strFileName )
        //        {
        //            Object refmissing = System.Reflection.Missing.Value;
        //            axWebBrowser1.Navigate(strFileName, ref refmissing , ref refmissing , ref refmissing , ref refmissing);

        ////			axWebBrowser1.ExecWB(SHDocVw.OLECMDID.OLECMDID_HIDETOOLBARS, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER,ref refmissing , ref refmissing);

        //            this.strFileType = "EXCEL";	

        //        }

        //        public void axWebBrowser1_NavigateComplete2(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
        //        {
        //            if(this.strFileType == "EXCEL")
        //            {
        ////				Object o = e.pDisp;
        ////				Object oDocument = o.GetType().InvokeMember("Document",BindingFlags.GetProperty,null,o,null);
        ////				Object oApplication = o.GetType().InvokeMember("Application",BindingFlags.GetProperty,null,oDocument,null);
        ////				//Object oName = o.GetType().InvokeMember("Name",BindingFlags.GetProperty ,null,oApplication,null);
        ////				//由于打开的是excel文件，所以这里的oApplication 其实就是Excel.Application
        ////				Excel.Application eApp =(Excel.Application)oApplication;
        //            }
        //        }

        private void frmReportServicePreview_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.reportViewer1.Dispose();
            //GC.Collect();
        }

        //
        //
        //
        //		private void ExcelExit()
        //		{
        //			NAR(oSheet);
        //			oBook.Close(False);
        //			NAR(oBook);
        //			NAR(oBooks);
        //			oApp.Quit();
        //			NAR(oApp);
        //			Debug.WriteLine("Sleeping...");
        //			System.Threading.Thread.Sleep(5000);
        //			Debug.WriteLine("End Excel");
        //		}
        //private void NAR(Object o)
        //{
        //    try{System.Runtime.InteropServices.Marshal.ReleaseComObject(o);}
        //    catch{}
        //    finally{o = null;}
        //}

        //private void frmReportServicePreview_Load(object sender, EventArgs e)
        //{

        //    this.reportViewer1.RefreshReport();
        //}

        private void frmReportServicePreview_Shown(object sender, EventArgs e)
        {
            try
            {
                if (_strOpenType == "NEW")
                    this.ShowReports(this._strWinTitle, this._strReportPath, null);
                else
                    this.InitReportByUrl(_strUrl);
            }
            catch (Exception err)
            {
                MessageBox.Show("Error:" + err.Message);
            }
        }

        private void reportViewer1_Print(object sender, CancelEventArgs e)
        {
            blIsPrint = true;
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
    }
}