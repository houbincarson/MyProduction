using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors.Controls;

namespace ProduceManager
{
    public partial class frmBseRoleRightEdit : frmEditorBase
    {
        private string strSpName = "Bse_Role_Right_Add_Edit_Del";
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

        private DataTable dtMenus = null;
        private DataRow drCurr = null;

        public frmBseRoleRightEdit()
        {
            InitializeComponent();
        }

        private void frmBseRoleRightEdit_Load(object sender, EventArgs e)
        {
            this.Text = "给角色分配权限—" + RoleName;
            string[] strKey = "RoleId,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            dtMenus = this.DataRequest_By_DataTable(strSpName, strKey,
                new string[] { RoleId, 
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(),
                    "1"});
            if (dtMenus == null)
                return;

            string strType = string.Empty;
            foreach (DataRow dr in dtMenus.Rows)
            {
                if (strType != dr["Menus_Type"].ToString())
                {
                    TreeListNode pN=treeList1.AppendNode(new object[] { dr["Menu_Type_Txt"].ToString(),string.Empty }, null);

                    DataRow[] drTypes = dtMenus.Select("Menus_Type = '" + dr["Menus_Type"].ToString() + "'");
                    foreach (DataRow drType in drTypes)
                    {
                        TreeListNode cN = treeList1.AppendNode(new object[] { drType["Menus_Name"].ToString(), drType["Menu_Id"].ToString() }, pN);
                    }
                }
                strType = dr["Menus_Type"].ToString();
            }
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string strMenuId = e.Node.GetValue("Menu_Id").ToString();
            if (strMenuId == string.Empty)
            {
                gcMenu.Text = "页面名称：";
                this.chkRightList.Items.Clear();
                ckbRight.Enabled = false;
                btnSave.Enabled = false;
                return;
            }

            DataRow[] drMenus = dtMenus.Select("Menu_Id=" + strMenuId);
            if (drMenus.Length == 0)
                return;

            drCurr = drMenus[0];
            gcMenu.Text = "页面名称："+drCurr["Menus_Name"].ToString();
            ckbRight.Checked = drCurr["Has_Account"].ToString() == "1";
            BoundItems(drCurr);

            ckbRight.Enabled = true;
            btnSave.Enabled = true;
            ckbAll.Checked = false;
        }

        private void BoundItems(DataRow dr)
        {
            this.chkRightList.Items.Clear();

            string strDef = dr["Definded_Operator"].ToString();
            string[] arrDef = strDef.Split(";".ToCharArray());

            foreach (string aDef in arrDef)
            {
                if(aDef == string.Empty)
                    continue;

                string[] DefItem = aDef.Split("=".ToCharArray());

                if (dr["Allowed_Operator"].ToString().IndexOf(DefItem[0] + "=") != -1)
                    this.chkRightList.Items.Add(DefItem[0], DefItem[1], CheckState.Checked, true);
                else
                    this.chkRightList.Items.Add(DefItem[0], DefItem[1], CheckState.Unchecked, true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ckbRight.Checked)
            {
                if (drCurr["Has_Account"].ToString() == "0")
                {
                    MessageBox.Show("没有要修改的权限.");
                    return;
                }
                string[] strKey = "MenuId,RoleId,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                DataTable dtEdit = this.DataRequest_By_DataTable(strSpName, strKey,
                    new string[] { drCurr["Menu_Id"].ToString(), RoleId, 
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(),
                    "2" });
                if (dtEdit!=null)
                {
                    MessageBox.Show("保存成功.");
                    foreach (CheckedListBoxItem item in chkRightList.Items)
                    {
                        item.CheckState = CheckState.Unchecked;
                    }
                    drCurr["Has_Account"] = "0";
                    drCurr["Allowed_Operator"] = string.Empty;
                    drCurr.AcceptChanges();
                }
            }
            else
            {
                string strAllowed_Operator = string.Empty;
                foreach (CheckedListBoxItem item in chkRightList.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        strAllowed_Operator += strAllowed_Operator == string.Empty ? (item.Value.ToString() + "=" + item.Description) : (";" + item.Value.ToString() + "=" + item.Description);
                    }
                }
                string[] strKey = "MenuId,RoleId,Allowed_Operator,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                DataTable dtEdit = this.DataRequest_By_DataTable(strSpName, strKey,
                    new string[] { drCurr["Menu_Id"].ToString(), RoleId, strAllowed_Operator,
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(), 
                    "3" });
                if (dtEdit!=null)
                {
                    MessageBox.Show("保存成功.");
                    drCurr["Has_Account"] = "1";
                    drCurr["Allowed_Operator"] = strAllowed_Operator;
                    drCurr.AcceptChanges();
                }
            }
        }

        private void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbAll.Checked)
            {
                ckbRight.Checked = true;
                foreach (CheckedListBoxItem item in chkRightList.Items)
                {
                    item.CheckState = CheckState.Checked;
                }
            }
            else
            {
                foreach (CheckedListBoxItem item in chkRightList.Items)
                {
                    item.CheckState = CheckState.Unchecked;
                }
            }
        }
    }
}
