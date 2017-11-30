using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace ProduceManager
{
    public partial class frmBseUserRoleEdit : frmEditorBase
    {
        private string strSpName = "Bse_User_Role_Add_Edit_Del";
        private DataTable dt;
        public string UserId
        {
            get;
            set;
        }
        public string UserInfo
        {
            get;
            set;
        }
        public frmBseUserRoleEdit()
        {
            InitializeComponent();
        }

        private void frmRoleEdit_Load(object sender, EventArgs e)
        {
            labInfo.Text = UserInfo;
            string[] strKey = "User_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            dt = this.DataRequest_By_DataTable(strSpName, strKey,
                new string[] { UserId, 
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     "1" });
            if (dt == null)
                return;
            BoundItems();
        }

        private void BoundItems()
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Has_In"].ToString() == "1")
                {
                    chkRoleList.Items.Add(dr["Role_Id"].ToString(), dr["Role_Nme"].ToString(), CheckState.Checked, true);
                }
                else
                {
                    chkRoleList.Items.Add(dr["Role_Id"].ToString(), dr["Role_Nme"].ToString(), CheckState.Unchecked, true);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strCheckedValue = "";
            string strUnCheckedValue = "";
            foreach (CheckedListBoxItem item in chkRoleList.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    strCheckedValue = strCheckedValue + "," + item.Value;
                }
                else
                {
                    strUnCheckedValue = strUnCheckedValue + "," + item.Value;
                }
            }
            this.btnSave.Enabled = false;
            string[] strKey = "User_Id,AddRoleIds,DelRoleIds,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataTable dt = this.DataRequest_By_DataTable(strSpName, strKey,
                            new string[] { UserId,strCheckedValue,strUnCheckedValue, 
                             CApplication.App.CurrentSession.UserId.ToString(),
                             CApplication.App.CurrentSession.DeptId.ToString(),
                             CApplication.App.CurrentSession.FyId.ToString(), 
                             "2" });
            if (dt != null)
            {
                MessageBox.Show("保存成功");
            }
            btnSave.Enabled = true;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}