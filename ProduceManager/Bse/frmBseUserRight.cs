﻿using System;
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
    public partial class frmBseUserRight : frmEditorBase
    {
        private string strSpName = "Bse_Role_Right_Add_Edit_Del";
        public string UserId
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }

        private DataTable dtMenus = null;

        public frmBseUserRight()
        {
            InitializeComponent();
        }

        private void frmBseRoleRightEdit_Load(object sender, EventArgs e)
        {
            this.Text = "给用户分配权限：" + UserName;
            string[] strKey = "UserId,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataSet dsMenusTemp = this.DataRequest_By_DataSet(strSpName, strKey,
                new string[] { UserId, 
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(),
                    "11"});
            if (dsMenusTemp == null)
                return;

            SetDtMenus(dsMenusTemp);
            BoundTreeList(false);
            treeList1.Select();
        }
        private void SetDtMenus(DataSet dsMenusTemp)
        {
            dtMenus = dsMenusTemp.Tables[0];
            DataTable dtTemp = dsMenusTemp.Tables[1];
            foreach (DataRow drMenu in dtMenus.Rows)
            {
                DataRow[] drs = dtTemp.Select("Menu_Id=" + drMenu["Menu_Id"].ToString());
                if (drs.Length > 0)
                {
                    drMenu["Has_Account"] = 1;
                }
                List<string> sbAllow = new List<string>();
                StringBuilder slls = new StringBuilder();
                foreach (DataRow dr in drs)
                {
                    if (dr["Allowed_Operator"].ToString() == string.Empty)
                        continue;

                    string[] strAllows = dr["Allowed_Operator"].ToString().Split(";".ToCharArray());
                    foreach (string strAllow in strAllows)
                    {
                        if (sbAllow.Contains(strAllow))
                            continue;

                        sbAllow.Add(strAllow);
                        if (slls.Length > 0)
                            slls.Append(";");
                        slls.Append(strAllow);
                    }
                }
                if (slls.Length > 0)
                {
                    drMenu["Allowed_Operator"] = slls.ToString();
                }
            }
            dtMenus.AcceptChanges();
        }
        private void BoundTreeList(bool blOnlyHas)
        {
            treeList1.Nodes.Clear();

            string strType = string.Empty;
            string strFilter = blOnlyHas ? "Has_Account=1" : string.Empty;
            dtMenus.DefaultView.RowFilter = strFilter;
            foreach (DataRowView dr in dtMenus.DefaultView)
            {
                if (strType != dr["Menus_Type"].ToString())
                {
                    TreeListNode pN = treeList1.AppendNode(new object[] { dr["Menu_Type_Txt"].ToString(), string.Empty, string.Empty }, null, CheckState.Unchecked);

                    DataView dv = dtMenus.DefaultView.ToTable().DefaultView;
                    dv.RowFilter = (blOnlyHas ? "Has_Account=1 AND " : string.Empty) + "Menus_Type = '" + dr["Menus_Type"].ToString() + "'";
                    bool blChkType = true;
                    foreach (DataRowView drType in dv)
                    {
                        TreeListNode cN = treeList1.AppendNode(new object[] { drType["Menus_Name"].ToString(), drType["Menu_Id"].ToString(), string.Empty }, pN, CheckState.Unchecked);
                        TreeListNode cA;
                        bool blChk = true;
                        bool blChkItem = true;
                        if (drType["Has_Account"].ToString() == "1")
                        {
                            cA = treeList1.AppendNode(new object[] { "访问", string.Empty, "AllowIn" }, cN, CheckState.Checked);
                        }
                        else
                        {
                            blChk = false;
                            cA = treeList1.AppendNode(new object[] { "访问", string.Empty, "AllowIn" }, cN, CheckState.Unchecked);
                        }

                        if (drType["Definded_Operator"].ToString() != string.Empty)
                        {
                            AppendNodeItem(cN, drType["Definded_Operator"].ToString(), drType["Allowed_Operator"].ToString(), out blChkItem);
                        }

                        if (blChk && blChkItem)
                            cN.Checked = true;
                        else
                            blChkType = false;
                    }
                    pN.Checked = blChkType;
                }
                strType = dr["Menus_Type"].ToString();
            }

        }
        private void AppendNodeItem(TreeListNode PNode, string strOperator, string strHas,out bool blChk)
        {
            blChk = true;
            string[] arrDef = strOperator.Split(";".ToCharArray());
            for (int i = 0; i < arrDef.Length; i++)
            {
                string aDef = arrDef[i];
                if (aDef == string.Empty)
                    continue;

                string[] DefItem = aDef.Split("=".ToCharArray());
                if (DefItem.Length != 2)
                    continue;

                if (strHas.IndexOf(DefItem[0] + "=") != -1)
                {
                    TreeListNode cN = treeList1.AppendNode(new object[] { DefItem[1], string.Empty, aDef }, PNode, CheckState.Checked);
                }
                else
                {
                    blChk = false;
                    TreeListNode cN = treeList1.AppendNode(new object[] { DefItem[1], string.Empty, aDef }, PNode, CheckState.Unchecked);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            UpdateChgToDt();
            DataTable dtChg = dtMenus.GetChanges();
            if (dtChg == null)
            {
                MessageBox.Show("没有修改任何项.");
                return;
            }
            string[] dtFields = new string[] { "Menu_Id", "Has_Account", "Allowed_Operator" };
            string strSplits = StaticFunctions.GetStringX(dtFields, dtChg,"Has_Chg=1");
            if (strSplits == string.Empty)
            {
                MessageBox.Show("没有修改任何项.");
                return;
            }

            string[] strKey = "strSplits,UserId,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataTable dtEdit = this.DataRequest_By_DataTable(strSpName, strKey,
                new string[] { strSplits, UserId, 
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(),
                    "12" });
            if (dtEdit == null)
                return;

            MessageBox.Show("成功保存.");
            dtMenus.AcceptChanges();
        }
        private void UpdateChgToDt()
        {
            foreach (TreeListNode node in treeList1.Nodes)
            {
                foreach (TreeListNode item in node.Nodes)
                {
                    string strId = item.GetValue("Menu_Id").ToString();
                    if (strId == string.Empty)
                        continue;

                    SetValueToDr(item, strId);
                }
            }
        }
        private void SetValueToDr(TreeListNode node, string strId)
        {
            DataRow[] drs = dtMenus.Select("Menu_Id=" + strId);
            if (drs.Length == 0)
                return;

            DataRow dr = drs[0];
            StringBuilder sbOp = new StringBuilder();
            foreach (TreeListNode cn in node.Nodes)
            {
                string strOperator = cn.GetValue("Operator").ToString();
                if (strOperator == "AllowIn")
                {
                    dr["Has_Account"] = cn.Checked ? 1 : 0;
                }
                else if (cn.Checked)
                {
                    if (sbOp.Length > 0)
                        sbOp.Append(";");
                    sbOp.Append(strOperator);
                }
            }
            dr["Allowed_Operator"] = sbOp.ToString();

            if (dr["Allowed_Operator"].ToString() != dr["Allowed_Operator", DataRowVersion.Original].ToString()
                || dr["Has_Account"].ToString() != dr["Has_Account", DataRowVersion.Original].ToString())
            {
                dr["Has_Chg"] = 1;
            }
            else
            {
                dr["Has_Chg"] = 0;
            }
        }

        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }
        private void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                CheckState parentCheckState = node.ParentNode.CheckState;
                CheckState nodeCheckState;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    nodeCheckState = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(nodeCheckState))
                    {
                        parentCheckState = CheckState.Unchecked; 
                        break;
                    } 
                    parentCheckState = check;
                } 
                node.ParentNode.CheckState = parentCheckState; 
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }
        private void tlOffice_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);

            string strOperator = e.Node.GetValue("Operator").ToString();
            if (strOperator == string.Empty)
                return;

            if (strOperator == "AllowIn")
            {
                if (!e.Node.Checked)
                {
                    foreach (TreeListNode node in e.Node.ParentNode.Nodes)
                    {
                        if (node != e.Node)
                            node.Checked = false;
                    }
                }
            }
            else
            {
                if (e.Node.Checked)
                {
                    e.Node.ParentNode.FirstNode.Checked = true;
                }
            }
        }

        private void btnExAll_Click(object sender, EventArgs e)
        {
            treeList1.ExpandAll();
            treeList1.Select();
        }
        private void btnExSel_Click(object sender, EventArgs e)
        {
            TreeListNode SelNode = treeList1.FocusedNode;
            if (SelNode == null)
                return;

            SelNode.ExpandAll();
            treeList1.Select();
        }
        private void btnColAll_Click(object sender, EventArgs e)
        {
            treeList1.CollapseAll();
            treeList1.Select();
        }
        private void btnPreview_Click(object sender, EventArgs e)
        {
            UpdateChgToDt();
            if (btnPreview.Text == "已分配预览")
            {
                BoundTreeList(true);
                btnExAll_Click(null, null);
                btnPreview.Text = "继续分配";
            }
            else
            {
                BoundTreeList(false);
                btnColAll_Click(null, null);
                btnPreview.Text = "已分配预览";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strMsg = "是否确认删除该用户的所有特殊权限？\r\n确认删除后，该用户的权限将全部来源于他所属的角色";
            if (MessageBox.Show(strMsg, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                return;

            string[] strKey = "UserId,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataTable dtTemp = this.DataRequest_By_DataTable(strSpName, strKey,
                new string[] { UserId, 
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(),
                    "13"});
            if (dtTemp == null)
                return;

            frmBseRoleRightEdit_Load(null, null);
            btnPreview.Text = "已分配预览";
        }
    }
}
