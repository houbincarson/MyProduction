using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace ProduceManager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            //RButtonMessageFilter Bfilt = new RButtonMessageFilter();
            //Application.AddMessageFilter(Bfilt);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (string.IsNullOrEmpty(CApplication.App.CurrentSession.Number))
            {
                //Application.Run(new frmSelectProdMgr());
                //return;
                using (frmLogin frm = new frmLogin())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new frmMainBase());
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
            else
            {
                Application.Run(new frmMainBase());
            }
        }
    }
}
