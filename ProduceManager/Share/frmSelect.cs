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
    public partial class frmSelect : frmEditorBase
    {
        private string strFocuName = "txtNumName";
        private string strSpNmae = "frmSelect";
        public string SelectFlag
        {
            get;
            set;
        }
        public DataRow SelectRow
        {
            get;
            set;
        }
        public string StrParas
        {
            get;
            set;
        }
        public string[] StrParaVals
        {
            get;
            set;
        }

        public frmSelect()
        {
            InitializeComponent();
            StaticFunctions.InitGridViewStyle(gridView1);
        }

        private void frmSelectShop_Load(object sender, EventArgs e)
        {
        }

        private void Txt_Enter(object sender, EventArgs e)
        {
            strFocuName = (sender as Control).Name;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            CApplication.App.CurrentSession.TimerId = 0;
            int k = msg.WParam.ToInt32();
            if (k == 27)//Esc
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return true;
            }
            else if (k ==70)//F
            {
                if (keyData.ToString().ToUpper().IndexOf("CONTROL") != -1)
                {
                    txtNumName.Focus();
                    txtNumName.SelectAll();
                }
            }
            else if (k == 13)
            {
                if (strFocuName == "txtNumName")
                {
                    string strKeys = string.IsNullOrEmpty(StrParas) ? string.Empty : StrParas + ",";
                    string[] strKey = (strKeys + "Number,Fy_Id,EDept_Id,EUser_Id,flag").Split(",".ToCharArray());

                    List<string> lisSpParmValue = new List<string>();
                    if (StrParaVals != null && StrParaVals.Length > 0)
                        lisSpParmValue.AddRange(StrParaVals);
                    lisSpParmValue.AddRange(new string[] {
                        txtNumName.Text.Trim(),                  
                        CApplication.App.CurrentSession.FyId.ToString(),
                        CApplication.App.CurrentSession.DeptId.ToString(),
                        CApplication.App.CurrentSession.UserId.ToString(),
                        SelectFlag });
                    frmDataTable = this.DataRequest_By_DataTable(strSpNmae, strKey, lisSpParmValue.ToArray());
                    frmDataTable.AcceptChanges();

                    gridControl1.DataSource = frmDataTable.DefaultView;
                    gridView1.BestFitColumns();
                    gridControl1.Select();
                }
                else
                {
                    gridControl1_DoubleClick(gridControl1, null);
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            SelectRow = gridView1.GetFocusedDataRow();
            if (SelectRow == null)
                return;

            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }
    }
}