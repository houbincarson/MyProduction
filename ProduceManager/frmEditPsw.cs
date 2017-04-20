using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ProduceManager
{
    public partial class frmEditPsw : frmEditorBase
    {
        public frmEditPsw()
        {
            InitializeComponent();
        }

        private void frmCreateSigleProduct_Load(object sender, EventArgs e)
        {
            txtOldPsw.Select();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtOldPsw.Text == string.Empty)
            {
                MessageBox.Show("请输入旧密码.");
                txtOldPsw.Select();
                return;
            }
            if (this.txtNewPsw.Text == string.Empty)
            {
                MessageBox.Show("请输入新密码.");
                txtNewPsw.Select();
                return;
            }
            if (txtNewPsw2.Text == string.Empty)
            {
                MessageBox.Show("请输入确认密码.");
                txtNewPsw2.Select();
                return;
            }
            if (txtNewPsw2.Text != this.txtNewPsw.Text)
            {
                MessageBox.Show("两次输入密码不相等.");
                txtNewPsw2.Select();
                return;
            }
            try
            {
                string[] strKey = "Number,PassWord".Split(",".ToCharArray());
                string[] strVal = new string[] { CApplication.App.CurrentSession.Number, txtOldPsw.Text };
                DataTable dt = this.DataRequest_By_DataTable("Get_Bse_User", strKey, strVal);
                dt.AcceptChanges();
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("旧密码不正确.");
                    return;
                }
                else
                {
                    string strSql = "PassWord='" + this.txtNewPsw.Text + "'";
                    strKey = "strTableName,strTableFieldKey,strTableFieldValue,strSql,UserId,ActFlag".Split(",".ToCharArray());
                    strVal = new string[] { "Bse_User", "User_Id", CApplication.App.CurrentSession.UserId.ToString()
                            , strSql, CApplication.App.CurrentSession.UserId.ToString(), "EditPsd" };
                    DataTable dtRet = this.DataRequest_By_DataTable("Share_Update_Table_Value", strKey, strVal);

                    if (dtRet.Rows[0][0].ToString() == "OK")
                    {
                        MessageBox.Show("密码修改成功.");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("修改密码失败，请重新操作.");
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
