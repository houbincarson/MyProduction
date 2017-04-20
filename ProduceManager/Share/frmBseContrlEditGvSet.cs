using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace ProduceManager
{
    public partial class frmBseContrlEditGvSet : frmEditorBase
    {
        private string strSpName = "Bse_Show_Add_Edit_Del";

        public string SetClass
        {
            set
            {
                txtClassGv.Text = value;
            }
        }
        public frmEditorBase FrmEditorBaseP
        {
            get;
            set;
        }

        public frmBseContrlEditGvSet()
        {
            InitializeComponent();
        }
        private void frmBsuSetQuery_Load(object sender, EventArgs e)
        {
        }
        
        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SimpleButton btn = sender as SimpleButton;
                switch (btn.Name)
                {
                    case "btnClear":
                        txtSets.Text = string.Empty;
                        break;
                    case "btnOk":
                        DoOkSets();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("错误:" + err.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private void DoOkSets()
        {
            if (txtClassGv.Text.Trim() == string.Empty)
                return;
            if (txtGvName.Text.Trim() == string.Empty)
                return;
            if (txtSets.Text.Trim() == string.Empty)
                return;

            List<string> lisSpParmValue = new List<string>();
            string[] strKey = "ClassName,GroupName,strEditSql,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {txtClassGv.Text.Trim(),txtGvName.Text.Trim(),
                txtSets.Text.Trim().Replace("\r\n","|").Replace("，",",").Replace(";",",").Replace("；",","),
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                "101"});
            DataSet dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            if (dtAdd == null)
            {
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            if (FrmEditorBaseP != null)
                FrmEditorBaseP.RefreshItem();
            this.Close();
            this.Dispose();
        }
    }

}
