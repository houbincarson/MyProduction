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
    public partial class frmBseRoleUserEdit : frmEditorBase
    {
        private string strSpName = "Bse_User_Role_Add_Edit_Del";
        private DataTable dt;
        public string RoleId
        {
            get;
            set;
        }
        public string RoleName
        {
            get;
            set;
        }
        public frmBseRoleUserEdit()
        {
            InitializeComponent();
        }

        private void frmRoleEdit_Load(object sender, EventArgs e)
        {
            this.Text = "角色用户维护/" + RoleName;
            DataSet dtBasic = this.GetFrmLoadDs(this.Name);
            StaticFunctions.BindDplComboByTable(dplUserType, dtBasic.Tables[1], "Name", "SetValue", "", "", true);
            StaticFunctions.BindDplComboByTable(dplSsDep, dtBasic.Tables[0], "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "", true);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            btnQuery.Enabled = false;
            chkRoleList.Items.Clear();
            string[] strKey = "Role_Id,User_Type,Dept_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            dt = this.DataRequest_By_DataTable(strSpName,
                 strKey , new string[] {
                    RoleId,
                    dplUserType.EditValue == null || dplUserType.EditValue.ToString() == string.Empty || dplUserType.EditValue.ToString() =="-9999" ? "-1" : dplUserType.EditValue.ToString(),
                    dplSsDep.EditValue == null || dplSsDep.EditValue.ToString() == string.Empty || dplSsDep.EditValue.ToString() =="-9999" ? "-1" : dplSsDep.EditValue.ToString(),
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(),
                    "3"});
            btnQuery.Enabled = true; 
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
                    chkRoleList.Items.Add(dr["User_Id"].ToString(), dr["Name"].ToString(), CheckState.Checked, true);
                }
                else
                {
                    chkRoleList.Items.Add(dr["User_Id"].ToString(), dr["Name"].ToString(), CheckState.Unchecked, true);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {            
            string strCheckedValue ="";
            string strUnCheckedValue = "";
            foreach(CheckedListBoxItem item in chkRoleList.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    strCheckedValue =strCheckedValue+","+ item.Value;
                }
                else
                {
                    strUnCheckedValue = strUnCheckedValue+","+item.Value;
                }
            }
            this.btnSave.Enabled = false;
            string[] strKey = "Role_Id,AddRoleIds,DelRoleIds,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataTable dt = this.DataRequest_By_DataTable(strSpName, strKey,
                            new string[] { RoleId,strCheckedValue,strUnCheckedValue, 
                             CApplication.App.CurrentSession.UserId.ToString(),
                             CApplication.App.CurrentSession.DeptId.ToString(),
                             CApplication.App.CurrentSession.FyId.ToString(), 
                             "4" });
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