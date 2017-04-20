using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WcfSimpData;

namespace ProduceManager
{
    public partial class frmSetSpecialFee : frmEditorBase
    {
        string strSpName = "Bse_SetSpecialFee_Add_Edit_Del";
        private string cust;
        private bool flag;
        private int x;
        private int y;
        public int operation
        {
            get;
            set;
        }

        public decimal setfee
        {
            get;
            set;
        }

        public frmSetSpecialFee(bool flag, string cust, int x, int y)
        {
            InitializeComponent();
            this.cust = cust;
            this.flag = flag;
            this.x = x;
            this.y = y;
        }
        private void btnComfirm_Click(object sender, EventArgs e)
        {
            if (lueOperation.EditValue == null || txtSetFee.Text.Trim().Length == 0)
            {
                MessageBox.Show("请选择操作方式或请填写设置金额");
                return;
            }
            this.DialogResult = DialogResult.OK;
            operation = int.Parse(lueOperation.EditValue.ToString());
            setfee = decimal.Parse(txtSetFee.Text.Trim());
        }

        private void frmSetSpecialFee_Load(object sender, EventArgs e)
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(), 
                     "6" };
            DataSet ds = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            StaticFunctions.BindDplComboByTable(lueOperation, ds.Tables[0], "SetText", "SetValue",
                new string[] { "SetValue", "SetText" },
                new string[] { "编号", "设置方式" }, true, "", "", false);

            this.label1.Text = "对选择的客户：" + cust + "设置特殊工费";

            this.Location = new Point(x + 20, y + 20); 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
