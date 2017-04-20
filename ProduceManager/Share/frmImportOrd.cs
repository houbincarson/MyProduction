using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;

namespace ProduceManager
{
    public partial class frmImportOrd : frmEditorBase
    {
        public string StrImpFlag
        {
            get;
            set;
        }
        public string StrUpdSpName
        {
            get;
            set;
        }
        public DataSet DsRets
        {
            get;
            set;
        }

        public frmImportOrd()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            CApplication.App.CurrentSession.TimerId = 0;
            int k = msg.WParam.ToInt32();
            if (k == 27)//Esc
            {
                this.DialogResult = DialogResult.No;
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtOrdNum.Text.Trim() == string.Empty)
            {
                txtOrdNum.Focus();
                return;
            }
            string[] strKey = "ImpNumber,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {txtOrdNum.Text.Trim(),
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                StrImpFlag };
            DataSet dsAdd = this.DataRequest_By_DataSet(StrUpdSpName, strKey, strVal);
            if (dsAdd == null)
            {
                return;
            }
            DsRets = dsAdd;
            this.DialogResult = DialogResult.Yes;
        }
    }
}