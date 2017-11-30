using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors.Repository;

namespace ProduceManager
{
    public partial class frmRpt_SysRpt : frmEditorBase
    {
        private DataSet dsLoad = null;
        private DataTable dtConst = null;
        private DataTable dtShow = null;

        public frmRpt_SysRpt()
        {
            InitializeComponent();
            InitContr();
        }

        private void InitContr()
        {
            if (dsLoad != null)
                return;

            dsLoad = this.GetFrmLoadDs(this.Name);
            dtShow = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];

            DataRow[] drs = CApplication.App.DtAllowMenus.Select("Menus_Class = '" + this.Name + "'");
            if (drs.Length <= 0)
            {
                return;
            }
            string strAllow = drs[0]["Allowed_Operator"].ToString();
            for (int i = dtShow.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dtShow.Rows[i];
                if (dr["ControlType"].ToString() == "7" && strAllow.IndexOf(dr["ControlName"].ToString() + "=") == -1)
                {
                    dr.Delete();
                }
            }
            dtShow.AcceptChanges();

            Rectangle rect = SystemInformation.VirtualScreen;
            int igcHeight;
            List<Control> lisGcContrs = StaticFunctions.ShowGroupControl(gcRpt, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            foreach (Control ctrl in lisGcContrs)
            {
                switch (ctrl.GetType().ToString())
                {
                    case "DevExpress.XtraEditors.SimpleButton":
                        SimpleButton btn = ctrl as SimpleButton;
                        btn.Click += new EventHandler(btn_Click);
                        break;

                    default:
                        break;
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;
            DataRow[] drs = dtShow.Select("ControlName='" + btn.Name+"'");
            if (drs.Length <= 0)
                return;
            DataRow dr = drs[0];
            StaticFunctions.ShowRptItem(dr["ShowText"].ToString(), dr["ControlFiled"].ToString(), ParentForm, dr["PassSpParam"].ToString());
        }
    }
}