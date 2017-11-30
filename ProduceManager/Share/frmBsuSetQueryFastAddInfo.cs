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
    public partial class frmBsuSetQueryFastAddInfo : frmEditorBase
    {
        private DataSet dsLoad = null;
        private DataTable dtConst = null;
        private DataTable dtShow = null;
        private string strSpName = "Bse_BsuSet_Add_Edit_Del_More";
        private string[] strMainFileds;

        public frmEditorBase FrmEditorBaseP
        {
            get;
            set;
        }
        public string SetClass
        {
            set
            {
                txtClassGv.Text = value;
            }
        }
        public string StrType
        {
            get;
            set;
        }
        public string Q_OR_E
        {
            get;
            set;
        }

        public frmBsuSetQueryFastAddInfo()
        {
            InitializeComponent();
        }
        private void frmBsuSetQuery_Load(object sender, EventArgs e)
        {
            dsLoad = this.GetFrmLoadDs(this.Name);
            dsLoad.AcceptChanges();
            dtShow = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];

            List<string> lisSpParmValue = new List<string>();
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                "1"});
            DataSet dtAdd = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            if (dtAdd == null)
            {
                return;
            }
            frmDataTable = dtAdd.Tables[0];

            if (Q_OR_E == "Q")
            {
                this.Text = "查询页面快速配置";
                this.dplClass.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("标准查询", "frmSysBusQuery", -1),
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("统计查询", "frmSysBusRpts", -1),
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("操作中心", "frmSysBusOPCenter", -1),
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("报表模板", "frmSysRpts", -1)});
            }
            else
            {
                this.Text = "编辑页面快速配置";
                if (StrType == "1")
                {
                    this.dplClass.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("树形维护", "frmSysKindManger", -1),
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("单表维护", "frmSysBseManger", -1),
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("基础维护", "frmBasicEdit", -1),
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("业务编辑", "frmSysBusEdit", -1)});
                }
                else
                {
                    this.dplClass.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("单表维护", "frmSysBseManger", -1),
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("基础维护", "frmBasicEdit", -1),
                    new DevExpress.XtraEditors.Controls.ImageComboBoxItem("业务编辑", "frmSysBusEdit", -1)});
                }
            }
        }
        private void InitContr()
        {
            int igcHeight;
            Rectangle rect = SystemInformation.VirtualScreen;
            List<Control> lisGcContrs = StaticFunctions.ShowGroupControl(gcMain, 870, dtShow, dplClass.EditValue.ToString() + "_" + Q_OR_E + StrType, dtConst, true, 30, false, null, true, out igcHeight);
            strMainFileds = null;
            strMainFileds = StaticFunctions.GetUpdateFields(dtShow, "GroupName='" + dplClass.EditValue.ToString() + "_" + Q_OR_E + StrType + "'");
        }
        
        private void btn_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                SimpleButton btn = sender as SimpleButton;
                switch (btn.Name)
                {
                    case "btnQuit":
                        this.DialogResult = System.Windows.Forms.DialogResult.No;
                        this.Close();
                        this.Dispose();
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

            frmDataTable.AcceptChanges();
            string strField = string.Empty;
            string strValues = string.Empty;

            DataRow dr = frmDataTable.Rows[0];
            strValues = StaticFunctions.GetAddValues(dr, strMainFileds, out strField);
            List<string> lisSpParmValue = new List<string>();
            string[] strKey = "BusClass,DstMenuClass,ClassType,strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {dplClass.EditValue.ToString(),txtClassGv.Text,StrType,
                                strField,
                                strValues,
                                CApplication.App.CurrentSession.UserId.ToString(),
                                CApplication.App.CurrentSession.DeptId.ToString(),
                                CApplication.App.CurrentSession.FyId.ToString(),
                                Q_OR_E=="Q"?"2":"3"});
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
        private void dplClass_SelectedValueChanged(object sender, EventArgs e)
        {
            gcMain.Controls.Clear();
            InitContr();

            DataRow drSet = frmDataTable.Rows.Add();
            frmDataTable.AcceptChanges();
            StaticFunctions.SetControlBindings(gcMain, this.frmDataTable.DefaultView as DataView, drSet);
            txtClassGv.Focus();
            btnOk.Enabled = true;
        }
    }

}
