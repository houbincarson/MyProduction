using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProduceManager
{
    public partial class frmSelectProdOne : frmEditorBase
    {
        public bool IsSelect
        {
            get;
            set;
        }
        private string PmIds = string.Empty;
        private DataTable dt = null;
        private DataTable dtPc = null;

        string strSpName = "Bse_ProductManager_Business_Add_Edit_Del";
        string strKeyId = "-1";

        public frmSelectProdOne(DataTable dtPc, DataTable dt, string PmIds)
        {
            InitializeComponent();
            this.PmIds = PmIds;
            this.dt = dt;
            this.dtPc = dtPc;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            IsSelect = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
          
        
            //添加进营销数据库
            //工厂产品库记录谁挑选了
            DataTable dtResult = GetOne();
            if (dtResult.Rows[0][0].ToString() == "OK")
            {
                MessageBox.Show("挑款成功");
            }
            else
            {
                MessageBox.Show(dtResult.Rows[0][0].ToString());
                return;
            }
            IsSelect = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmSelectProdOne_Load(object sender, EventArgs e)
        {
            this.gridCMain.DataSource = dt;
            StaticFunctions.BindDplComboByTable(extPopupTree1, dtPc, "Kind_Name", "Kind_Id", "Kind_Key", new string[] { "Kind_Id", "Kind_Name" }, new string[] { "编号", "名称" }, "Kind_Id", "Kind_Id", "Kind_Key", "Kind_Id", "Parent_Kind_Id", "", true);
        }

        private DataTable GetOne()
        {
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,Bus_PNumber,Name,Pc_Id,Pm_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {strKeyId,
                                                                        CApplication.App.CurrentSession.UserId.ToString(),
                                                                        CApplication.App.CurrentSession.DeptId.ToString(),
                                                                        dt.Rows[0]["Fy_Id"].ToString()  ,
                                                                        txtPNumber.Text.Trim(),
                                                                        txtName.Text.Trim(),
                                                                    "1", //   ucTreeList1.EditValue.ToString(),
                                                                        dt.Rows[0]["PM_Id"].ToString(),
                                                                        "3" };
            DataTable dtProd = this.DataRequest_By_DataTable(strSpName, strKey, strVal);

            return dtProd;
        }
    }
}
