using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Diagnostics;
using WcfSimpData;
using System.Configuration;

namespace ProduceManager
{
    public partial class frmLogin : Form
    {
        private readonly string BtProduceCS = ConfigurationManager.AppSettings["BtProduceCS"];
        private readonly string strUrl = ConfigurationManager.AppSettings["UpdateUrl"];
        private PrdManager.ShareWSSoapClient PrdManagerSoapClient = ServerRefManager.GetPrdManager();
        private bool isExist = false;

        public bool IsLock
        {
            get;
            set;
        }

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (IsLock)
            {
                txtUserId.Text = CApplication.App.CurrentSession.Number;
                txtUserId.SelectAll();
            }
            else
            {
                txtUserId.Text = string.Empty;
                txtUserId.Focus();
            }
        }

        private void frmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                if (txtUserId.Text.Trim() == string.Empty)
                    txtUserId.Focus();
                else if (txtUserPwd.Text.Trim() == string.Empty)
                    txtUserPwd.Focus();
                else
                    picOk_Click(null, null);
            }
        }

        private void txtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                if (txtUserId.Text.Trim() == string.Empty)
                    txtUserId.Focus();
                else if (txtUserPwd.Text.Trim() == string.Empty)
                    txtUserPwd.Focus();
                else
                    picOk_Click(null, null);
            }
        }

        private void txtUserPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                if (txtUserId.Text.Trim() == string.Empty)
                    txtUserId.Focus();
                else if (txtUserPwd.Text.Trim() == string.Empty)
                    txtUserPwd.Focus();
                else
                    picOk_Click(null, null);
            }
        }

        private void picOk_Click(object sender, EventArgs e)
        {
            if (this.txtUserId.Text.Trim() == string.Empty)
            {
                MessageBox.Show("用户名不能为空.");
                txtUserId.Focus();
                return;
            }
            if (this.txtUserPwd.Text.Trim() == string.Empty)
            {
                MessageBox.Show("密码不能为空.");
                txtUserPwd.Focus();
                return;
            }

            string[] strKey = "Number,PassWord".Split(",".ToCharArray());
            string[] strVal = new string[] { this.txtUserId.Text, txtUserPwd.Text };
            MethodRequest req = new MethodRequest();
            req.ParamKeys = strKey;
            req.ParamVals = strVal;
            req.ProceName = "Get_Bse_User";
            req.ProceDb = this.BtProduceCS;
            DataTable dt = ServerRequestProcess.DbRequest.DataRequest_By_DataTable(req);
            dt.AcceptChanges();
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("密码或用户名不正确.");
                return;
            }
            else
            {
                DataRow dr = dt.Rows[0];

                strKey = "User_Id".Split(",".ToCharArray());
                strVal = new string[] { dr["User_id"].ToString() };
                req.ParamKeys = strKey;
                req.ParamVals = strVal;
                req.ProceName = "Get_Account_Menus_By_User";
                req.ProceDb = this.BtProduceCS;
                DataTable dtAccount = ServerRequestProcess.DbRequest.DataRequest_By_DataTable(req);
                dtAccount.AcceptChanges();
                DataTable dtAllow = dtAccount.Clone();
                foreach (DataRow drA in dtAccount.Rows)
                {
                    DataRow[] drs = dtAllow.Select("Menu_Id = '" + drA["Menu_Id"].ToString() + "'");
                    if (drs.Length == 0)
                    {
                        DataRow drNew = dtAllow.NewRow();
                        foreach (DataColumn dc in dtAccount.Columns)
                        {
                            drNew[dc.ColumnName] = drA[dc.ColumnName];
                        }
                        dtAllow.Rows.Add(drNew);
                    }
                    else
                    {
                        string strOps = drs[0]["Allowed_Operator"].ToString();
                        drs[0]["Allowed_Operator"] = strOps + ";" + drA["Allowed_Operator"].ToString();
                    }
                }

                if (dtAllow.Select("Menus_Class='frmMainBase'").Length == 0)
                {
                    MessageBox.Show("你没有登录该系统的权限,请与系统管理员联系.");
                    Application.Exit();
                    return;
                }

                CApplication.ClearCSession();
                CApplication.App.DtAllowMenus = dtAllow;
                CApplication.App.CurrentSession.UserId = Int64.Parse(dr["User_id"].ToString());
                CApplication.App.CurrentSession.UserNme = dr["Name"].ToString();
                CApplication.App.CurrentSession.Number = dr["Number"].ToString();
                CApplication.App.CurrentSession.DeptNme = dr["Dept_Txt"].ToString();
                Int64 iCompany_Id = -1;
                if (dt.Columns.Contains("Company_Id"))
                {
                    if (Int64.TryParse(dr["Company_Id"].ToString(), out iCompany_Id))
                    {
                        CApplication.App.CurrentSession.Company_Id = iCompany_Id;
                    } 
                }
                Int64 iDeptId = -1;
                if (Int64.TryParse(dr["Dept_Id"].ToString(), out iDeptId))
                {
                    CApplication.App.CurrentSession.DeptId = iDeptId;
                }
                if (Int64.TryParse(dr["Fy_Id"].ToString(), out iDeptId))
                {
                    CApplication.App.CurrentSession.FyId = iDeptId;
                }
                CApplication.App.CurrentSession.UserType = dr["User_Type"].ToString();
                CApplication.App.CurrentSession.CardNub = dr["CardNub"].ToString();



                strKey = "Form,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
                strVal = new string[] {  "frmLogin"
                    , CApplication.App.CurrentSession.UserId.ToString()
                    , CApplication.App.CurrentSession.DeptId.ToString()
                    , CApplication.App.CurrentSession.FyId.ToString()};
                req.ParamKeys = strKey;
                req.ParamVals = strVal;
                req.ProceName = "Get_Form_Load_Table";
                req.ProceDb = this.BtProduceCS;
                CApplication.App.DtUserBasicSet = ServerRequestProcess.DbRequest.DataRequest_By_DataTable(req);
                CApplication.App.DtUserBasicSet.AcceptChanges();
                //保存用户登录信息到UserInfo.xml
                if (dt.Columns.Contains("UserInfo") && dr["UserInfo"].ToString() == "1")
                {
                    string AppPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                    dt.WriteXml(AppPath + "\\UserInfo");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            string[] strKey = "Version".Split(",".ToCharArray());
            string[] strVal = new string[] { Application.ProductVersion };
            MethodRequest req = new MethodRequest();
            req.ParamKeys = strKey;
            req.ParamVals = strVal;
            req.ProceName = "Get_IsNeedVersionUpdate";
            req.ProceDb = this.BtProduceCS;
            DataTable dt = ServerRequestProcess.DbRequest.DataRequest_By_DataTable(req);
            if (dt.Rows.Count == 0)
                return;

            if (dt.Rows[0]["NeedUpdate"].ToString() == "0")
                return;

            if (MessageBox.Show(@"系统有新版本，是否更新,确定后请耐心等待文件下载？", @"更新确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (dt.Columns.Contains("FilenNmes"))
                {
                    string strFileUpd = dt.Rows[0]["FilenNmes"].ToString().Trim();
                    if (strFileUpd != string.Empty)
                    {
                        string[] FilenNmes = strFileUpd.Split(',');
                        if (FilenNmes.Length > 0)
                        {
                            for (int i = 0; i < FilenNmes.Length; i++)
                            {
                                File.WriteAllBytes(Application.StartupPath + "\\" + FilenNmes[i].ToString().Trim(), PrdManagerSoapClient.GetUpdateFileByte(strUrl, FilenNmes[i].ToString().Trim()));
                            }
                        }
                    }
                }

                string strUNet = System.Configuration.ConfigurationManager.AppSettings["UpdateNet"];
                if (string.IsNullOrEmpty(strUNet))
                {
                    MessageBox.Show("更新出错,请与系统人员联系.");
                    return;
                }
                string strFile = Application.StartupPath + "\\" + Application.ProductName + ".zip";
                if (File.Exists(strFile))
                {
                    File.SetAttributes(strFile, FileAttributes.Normal);
                }
                File.WriteAllBytes(strFile, PrdManagerSoapClient.GetUpdateFileByte(strUrl, Application.ProductName + strUNet + ".zip"));
                System.Diagnostics.Process.Start("AutoUpdate.exe", Application.ProductName);
                Application.Exit();
            }
            catch (Exception err)
            {
                MessageBox.Show(@"下载更新文件出错：" + err.Message);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int k = msg.WParam.ToInt32();
            if (k == 27)//Esc
            {
                if (IsLock)
                {
                    if (MessageBox.Show("必须重新登录，否则将退出系统，是否退出？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        isExist = true;
                        CApplication.ClearCSession();
                        Application.Exit();
                    }
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsLock && this.DialogResult != DialogResult.OK && !isExist)
            {
                if (MessageBox.Show("必须重新登录，否则将退出系统，是否退出？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    isExist = true;
                    CApplication.ClearCSession();
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
         
         
    }
}
