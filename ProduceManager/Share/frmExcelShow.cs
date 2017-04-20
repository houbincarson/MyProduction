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
    public partial class frmExcelShow : frmEditorBase
    {
        public string StrOrdKeyId
        {
            get;
            set;
        }
        public string StrUpdSpName
        {
            get;
            set;
        }
        public DataRow DrBtn
        {
            get;
            set;
        }
        public DataTable DtExcel
        {
            get;
            set;
        }
        public DataSet DsRets
        {
            get;
            set;
        }

        public frmExcelShow(DataView dvExcel)
        {
            InitializeComponent();
            StaticFunctions.ShowExcelGridControl(gridVExcel, dvExcel);
        }
        private void frmSelectShop_Load(object sender, EventArgs e)
        {
            gridCExcel.DataSource = frmDataTable.DefaultView;
            gridVExcel.BestFitColumns();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(StrOrdKeyId))
            {
                this.DialogResult = DialogResult.Yes;
                return;
            }
            string[] dtFields = StaticFunctions.GetUpdateFields(DtExcel, DtExcel.DefaultView.RowFilter);
            if (dtFields.Length == 0)
            {
                MessageBox.Show("导入设置出错，未设置UpdatelFiled.");
                return;
            }
            string strSplits = StaticFunctions.GetStringX(dtFields, frmDataTable, DrBtn["FilterGetStringX"].ToString());
            if (strSplits == string.Empty)
            {
                MessageBox.Show("Excel数据不完整，没有可以导入的数据行.");
                return;
            }

            string[] strKey = "Ord_Id,strSplits,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {StrOrdKeyId,
                strSplits,
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                DrBtn["ImportSpFlag"].ToString() };
            DataSet dsAdd = this.DataRequest_By_DataSet(StrUpdSpName, strKey, strVal);
            if (dsAdd == null)
            {
                return;
            }
            DsRets = dsAdd;
            this.DialogResult = DialogResult.Yes;
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
    }
}