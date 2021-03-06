﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProduceManager
{
    public partial class frmSelectProdMany : frmEditorBase
    {
        public bool IsSelect
        {
            get;
            set;
        }
        string strSpName = "Bse_ProductManager_Business_Add_Edit_Del";
        string strKeyId = "-1";
        private string PmIds = string.Empty;
        private DataTable dt = null;
        private string ids = string.Empty;
        public frmSelectProdMany(DataTable dt, string ids)
        {
            InitializeComponent();
            this.ids = ids;
            this.dt = dt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            IsSelect = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DataTable dtResult = GetMany();
            if (dtResult.Rows[0][0].ToString() == "OK")
            {
                MessageBox.Show("挑选成功");
            }
            else
            {
                MessageBox.Show("挑选失败");
            }
            IsSelect = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmSelectProdMany_Load(object sender, EventArgs e)
        {
            label1.Text = "您一共挑选了" + ids.TrimEnd(',').Split(',').Length.ToString() + "款";
           label2.Text = "我们将为您自动生成营销流水号"; 
        }

        private DataTable GetMany()
        {
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,Count,PmIds,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {strKeyId,
                                                                        CApplication.App.CurrentSession.UserId.ToString(),
                                                                        CApplication.App.CurrentSession.DeptId.ToString(),

                                                                        dt.Rows[0]["Fy_Id"].ToString()  ,  
                                                                         ids.TrimEnd(',').Split(',').Length.ToString() ,
                                                                   ids.TrimEnd(','),
                                                                        "4" };
            DataTable dtProd = this.DataRequest_By_DataTable(strSpName, strKey, strVal);

            return dtProd;
        }
    }
}
