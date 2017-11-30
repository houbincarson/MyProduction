using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using WcfSimpData;
using System.Configuration;

namespace ProduceManager
{
    public enum SysProjectEnum
    {
        Ukynda_Demo,//测试
        GC_ProduceManager,//工厂
        MJ_ProduceManager,//铭进工厂
        BTJ_ProduceManager,//百泰金
        YHSYB_ProduceManager,//银行事业部
        BTGL_ProduceManager,//百泰国礼
        Jew_ProduceManager,//钻饰
        XYL_ProduceManager,//信誉楼
        ProdManager//大产品库
    }

    public class ProjectSpecialSet
    {
        private static readonly string BtProduceCS = ConfigurationManager.AppSettings["BtProduceCS"];
        public static void InitHomeOpLink(string strProjectName, DataTable dt)
        {
            if (strProjectName == SysProjectEnum.MJ_ProduceManager.ToString())
            {
                ProjectSpecialSet.InitHomeOpLinkMJ(dt);
            }
            else
            {
                ProjectSpecialSet.InitHomeOpLinkNone(dt);
            }
        }
        public static void DoMainProcessCmdKey(string strProjectName, ref Message msg, Keys keyData, frmMainBase frmMain, Timer timer1)
        {
            int k = msg.WParam.ToInt32();
            if (strProjectName == SysProjectEnum.MJ_ProduceManager.ToString())
            {
                ProjectSpecialSet.DoMainProcessCmdKeyMJ(ref msg, keyData, frmMain, timer1);
            }
            if (strProjectName == SysProjectEnum.ProdManager.ToString())
            {
                ProjectSpecialSet.DoMainProcessCmdKeyProd(ref msg, keyData, frmMain, timer1);
            }
            else
            {
                ProjectSpecialSet.DoMainProcessCmdKeyNone(ref msg, keyData, frmMain, timer1);
            }
        }

    
        private static void InitHomeOpLinkMJ(DataTable dt)
        {
            dt.Rows.Add(new object[] { "员工操作", "F1" });
            dt.Rows.Add(new object[] { "收发交接维护", "F2" });
            dt.Rows.Add(new object[] { "进料入库维护", "F3" });
            dt.Rows.Add(new object[] { "出料出库维护", "F4" });
            dt.Rows.Add(new object[] { "页面刷新数据", "F5" });
            dt.Rows.Add(new object[] { "进饰入库维护", "F6" });
            dt.Rows.Add(new object[] { "实物盘点维护", "F7" });
            dt.Rows.Add(new object[] { "收发库存盘点", "F8" });
            dt.Rows.Add(new object[] { "切换所属部门", "F9" });
            dt.Rows.Add(new object[] { "切换登录用户", "F10" });
            dt.Rows.Add(new object[] { "员工操作中补打小票", "F11" });
            dt.Rows.Add(new object[] { "读电子称", "F12" });
            dt.Rows.Add(new object[] { "关闭页面", "Esc" });
            dt.Rows.Add(new object[] { "打开计算器", "~" });
            dt.Rows.Add(new object[] { "切换前一个页面", "Alt+CapsLock" });
            dt.Rows.Add(new object[] { "删除选中记录", "Delete" });
            dt.Rows.Add(new object[] { "选中记录上移/下移", "PgUp/PgDn" });
            dt.Rows.Add(new object[] { "对应的按钮", "Alt+?" });
            dt.Rows.Add(new object[] { "更多请阅读页面中蓝色标题", "更多请阅读页面中蓝色标题" });
        }
        private static void InitHomeOpLinkNone(DataTable dt)
        {
            dt.Rows.Add(new object[] { "刷新数据", "F5" });
            dt.Rows.Add(new object[] { "系统锁定", "F9" });
            dt.Rows.Add(new object[] { "切换用户", "F10" });
            dt.Rows.Add(new object[] { "关闭页面", "Esc" });
            dt.Rows.Add(new object[] { "删除选中记录", "Delete" });
            dt.Rows.Add(new object[] { "选中记录上移/下移", "PgUp/PgDn" });
            dt.Rows.Add(new object[] { "对应的按钮", "Alt+?" });
            dt.Rows.Add(new object[] { "操作面板光标下移", "Enter" });
            dt.Rows.Add(new object[] { "操作面板光标上移", "Ctrl + '-'" });
            dt.Rows.Add(new object[] { "操作面板光标下移3位", "Ctrl + '+'" });
            dt.Rows.Add(new object[] { "更多请阅读页面中蓝色标题", "更多请阅读页面中蓝色标题" });
        }


        private static void DoMainProcessCmdKeyMJ(ref Message msg, Keys keyData, frmMainBase frmMain, Timer timer1)
        {
            int k = msg.WParam.ToInt32();
            if (k == 112)//F1
            {
                StaticFunctions.OpenChildEditorForm(true, "ProduceManager", frmMain, "员工操作", "frmTst_UserReceDeliChkEdit", "VIEW", string.Empty);
            }
            else if (k == 113)//F2
            {
                StaticFunctions.OpenChildEditorForm(true, "ProduceManager", frmMain, "收发交接维护", "frmTst_MoveEdit", "VIEW", string.Empty);
            }
            else if (k == 114)//F3
            {
                StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", frmMain, "进料入库维护", "frmSysBusEdit", "frmTst_MaterialOrdEdit", "ADD", "BusClassName=frmTst_MaterialOrdEdit", null);
            }
            else if (k == 115)//F4
            {
                StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", frmMain, "出料出库维护", "frmSysBusEdit", "frmTst_MaterialOrdOutEdit", "ADD", "BusClassName=frmTst_MaterialOrdOutEdit", null);
            }
            else if (k == 117)//F6
            {
                StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", frmMain, "进饰入库维护", "frmSysBusEdit", "frmTst_RetOrdEdit", "ADD", "BusClassName=frmTst_RetOrdEdit", null);
            }
            else if (k == 118)//F7
            {
                StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", frmMain, "实物盘点维护", "frmSysBusEdit", "frmTst_ProdChkOrdEdit", "ADD", "BusClassName=frmTst_ProdChkOrdEdit", null);
            }
            else if (k == 119)//F8
            {
                Form frmEx = StaticFunctions.GetExistedChildForm(frmMain, "frmTst_StorageChkEdit", true);
                if (frmEx != null)
                {
                    (frmEx as frmTst_StorageChkEdit).ReGetData();
                    (frmEx as frmTst_StorageChkEdit).Activate();
                }
                else
                    StaticFunctions.OpenChildEditorForm(true, "ProduceManager", frmMain, "收发库存盘点", "frmTst_StorageChkEdit", "VIEW", string.Empty);
            }
            else if (k == 120)//F9
            {
                Int64 deptOld = CApplication.App.CurrentSession.DeptId;
                frmReSetDept frmLg = new frmReSetDept();
                timer1.Stop();
                if (frmLg.ShowDialog() == DialogResult.OK)
                {
                    if (CApplication.App.CurrentSession.DeptId != deptOld)
                    {
                        Form[] charr = frmMain.MdiChildren;
                        foreach (Form frm in charr)
                        {
                            frm.Close();
                        }
                        CApplication.App.CurrentSession.DeptNme = frmLg.strDeptName;
                        frmMain.SetDepartTxt(CApplication.App.CurrentSession.DeptNme);
                        string strName = System.Configuration.ConfigurationManager.AppSettings["SystemName"];
                        frmMain.Text = strName + "--" + CApplication.App.CurrentSession.DeptNme;

                        string[] strKey = "Form,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
                        string[] strVal = new string[] { "frmLogin"
                                        , CApplication.App.CurrentSession.UserId.ToString()
                                        , CApplication.App.CurrentSession.DeptId.ToString()
                                        , CApplication.App.CurrentSession.FyId.ToString() };
                        MethodRequest req = new MethodRequest();
                        req.ParamKeys = strKey;
                        req.ParamVals = strVal;
                        req.ProceName = "Get_Form_Load_Table";
                        req.ProceDb = ProjectSpecialSet.BtProduceCS;
                        CApplication.App.DtUserBasicSet = ServerRequestProcess.DbRequest.DataRequest_By_DataTable(req);
                        CApplication.App.DtUserBasicSet.AcceptChanges();

                        DataRow[] drSkin = CApplication.App.DtUserBasicSet.Select("SetKey='SkinName'");
                        if (drSkin.Length > 0)
                        {
                            frmMain.InitDepSkin(drSkin[0]["SetValue"].ToString());
                        }
                    }
                }
                timer1.Start();
            }
            else if (k == 192)//~
            {
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
                System.Diagnostics.Process proc = new System.Diagnostics.Process();

                //设置外部程序名(记事本用 notepad.exe 计算器用 calc.exe) 
                info.FileName = "calc.exe";
                //设置外部程序的启动参数 
                info.Arguments = "";
                //设置外部程序工作目录为c:\windows 
                info.WorkingDirectory = "c:/windows/";
                try
                {
                    proc = System.Diagnostics.Process.Start(info);
                }
                catch
                {
                    MessageBox.Show("系统找不到指定的程序文件", "错误提示！");
                }

            }
        }
        private static void DoMainProcessCmdKeyProd(ref Message msg, Keys keyData, frmMainBase frmMain, Timer timer1)
        {
            int k = msg.WParam.ToInt32();
            if (k == 120)//F9
            {
                Int64 deptOld = CApplication.App.CurrentSession.DeptId;
                frmReSetDept frmLg = new frmReSetDept();
                timer1.Stop();
                if (frmLg.ShowDialog() == DialogResult.OK)
                {
                    if (CApplication.App.CurrentSession.DeptId != deptOld)
                    {
                        Form[] charr = frmMain.MdiChildren;
                        foreach (Form frm in charr)
                        {
                            frm.Close();
                        }
                        CApplication.App.CurrentSession.DeptNme = frmLg.strDeptName;
                        frmMain.SetDepartTxt(CApplication.App.CurrentSession.DeptNme);
                        string strName = System.Configuration.ConfigurationManager.AppSettings["SystemName"];
                        frmMain.Text = strName + "--" + CApplication.App.CurrentSession.DeptNme;

                        string[] strKey = "Form,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
                        string[] strVal = new string[] { "frmLogin"
                                        , CApplication.App.CurrentSession.UserId.ToString()
                                        , CApplication.App.CurrentSession.DeptId.ToString()
                                        , CApplication.App.CurrentSession.FyId.ToString() };
                        MethodRequest req = new MethodRequest();
                        req.ParamKeys = strKey;
                        req.ParamVals = strVal;
                        req.ProceName = "Get_Form_Load_Table";
                        req.ProceDb = ProjectSpecialSet.BtProduceCS;
                        CApplication.App.DtUserBasicSet = ServerRequestProcess.DbRequest.DataRequest_By_DataTable(req);
                        CApplication.App.DtUserBasicSet.AcceptChanges();

                        DataRow[] drSkin = CApplication.App.DtUserBasicSet.Select("SetKey='SkinName'");
                        if (drSkin.Length > 0)
                        {
                            frmMain.InitDepSkin(drSkin[0]["SetValue"].ToString());
                        }
                    }
                }
                timer1.Start();
            }
        } 
        private static void DoMainProcessCmdKeyNone(ref Message msg, Keys keyData, frmMainBase frmMain, Timer timer1)
        {
            int k = msg.WParam.ToInt32();
            if (k == 120)//F9
            {
                timer1.Stop();
                Int64 deptOld = CApplication.App.CurrentSession.UserId;
                frmLogin frmLg = new frmLogin();
                frmLg.IsLock = true;
                if (frmLg.ShowDialog() == DialogResult.OK && CApplication.App.CurrentSession.UserId != deptOld)
                {
                    Form[] charr = frmMain.MdiChildren;
                    foreach (Form frm in charr)
                    {
                        frm.Close();
                    }
                    frmMain.SetRtDepart();
                }
                timer1.Start();
            }
        }
    }
}
