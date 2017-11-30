using System;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace ProduceManager
{
    ///<summary>
    ///会话结构，用户登录系统后保存的用户登录信息
    ///</summary> 
    public struct CSession
    {
        public Int64 Company_Id;	
        public Int64 UserId;
        public Int64 DeptId;
        public Int64 FyId;	
        public string Number;
        public string CardNub;		
        public string UserNme;
        public string DeptNme;
        public string UserType;

        public int TimerId;
    }

    /// <summary>
    /// 应用程序管理类。
    /// </summary>
    /// <remarks>
    /// 应用程序管理类全局只能创建和使用一个
    /// 应用程序管理对象<c>CApplication.App</c>。
    /// <br/><hr/>
    /// </remarks>
    public class CApplication
    {
        /// <summary>
        /// 类的静态成员变量。用于存储全局唯一应用程序管理对象。
        /// </summary>
        public static COM_ProtConnect Com_Prot = null;
        public static System.Windows.Forms.Form CurrForm = null;
        private static CApplication m_App;
        public CSession CurrentSession;
        public DataTable DtAllowMenus;
        public DataTable DtUserBasicSet;


        /// <summary>
        /// 构造函数
        /// </summary>
        protected CApplication()
        {
            this.CurrentSession.UserId = -1;
            this.CurrentSession.DeptId = -1;
            this.CurrentSession.FyId = -1;
            this.CurrentSession.Number = "";
            this.CurrentSession.UserNme = "";
            this.CurrentSession.CardNub = "";
            this.CurrentSession.DeptNme = "";
            this.CurrentSession.UserType = "";
            this.CurrentSession.Company_Id = -1;
            this.CurrentSession.TimerId = 1;

            this.DtAllowMenus = new DataTable();
            
        }

        /// <summary>
        /// 用于创建<c>CApplication</c>对象实例。
        /// </summary>
        /// <returns></returns>
        private static CApplication CreateAppObj()
        {
            return new CApplication();
        }

        /// <summary>
        /// 全局唯一的<c>CApplication</c>对象实例
        /// </summary>
        public static CApplication App
        {
            get
            {
                lock (typeof(CApplication))
                {
                    if (CApplication.m_App == null)
                    {
                        m_App = CApplication.CreateAppObj();
                    }
                    return m_App;
                }
            }
        }

        public static void ClearCSession()
        {
            CApplication.App.CurrentSession.UserId = -1;
            CApplication.App.CurrentSession.DeptId = -1;
            CApplication.App.CurrentSession.FyId = -1;
            CApplication.App.CurrentSession.UserNme = "";
            CApplication.App.CurrentSession.Number = "";
            CApplication.App.CurrentSession.CardNub = "";
            CApplication.App.CurrentSession.DeptNme = "";
            CApplication.App.CurrentSession.UserType = "";

            CApplication.App.DtAllowMenus = null;
            CApplication.App.DtUserBasicSet = null;
        }

        public static void TimerCount()
        {
            CApplication.App.CurrentSession.TimerId++;
        }
    }
}

