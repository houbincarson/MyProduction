using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;

namespace ProduceManager
{
    public partial class frmProdAuditMgrInfo : frmEditorBase
    {
        private DataTable dt;
        private string StrMode;
        public frmProdAuditMgrInfo()
        {
            InitializeComponent();
        }
        public frmProdAuditMgrInfo(DataTable dt)
        {
            InitializeComponent();
            this.dt = dt;
        }

        string strSpName = "Bse_ProductManager_Business_Add_Edit_Del";
        DataSet dsLoad;
        private void frmProdManagerInfo_Load(object sender, EventArgs e)
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     "8" };
            dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            StaticFunctions.BindDplComboByTable(dplfy, dsLoad.Tables[0], "Name", "Fy_Id",
                 new string[] { "Fy_Id", "Name" },
                 new string[] { "编号", "名称" }, true, "", "", true);

            #region 绑定成色
            StaticFunctions.BindCheckedComboBoxEdit(chkPqName, dsLoad.Tables[1], "Name", "Pq_Id", "", "");
            #endregion

            #region 绑定配件
            StaticFunctions.BindDplComboByTable(lookParts, dsLoad.Tables[15], "Name", "Part_Id",
                 new string[] { "Part_Id", "Name" },
                 new string[] { "编号", "名称" }, true, "", "", true);
            #endregion

            #region 绑定性别
            StaticFunctions.BindDplComboByTable(lookSex, dsLoad.Tables[3], "SetText", "SetValue",
                new string[] { "SetValue", "SetText" },
                new string[] { "序号", "性别" }, true, "", "", false);
            //lookSex.ItemIndex = -1;
            #endregion

            #region 绑定设计类型
            StaticFunctions.BindDplComboByTable(lookType, dsLoad.Tables[4], "SetText", "SetValue",
                new string[] { "SetValue", "SetText" },
                new string[] { "序号", "设计类型" }, true, "", "", false);
            //lookdesginType.ItemIndex = -1;
            #endregion

            #region 绑定组合方式
            StaticFunctions.BindDplComboByTable(lookZuHeType, dsLoad.Tables[5], "SetText", "SetValue",
                new string[] { "SetValue", "SetText" },
                new string[] { "序号", "组合类型" }, true, "", "", false);
            //lookZuHeType.ItemIndex = -1;
            #endregion

            #region 绑定类别
            StaticFunctions.BindDplComboByTable(extKindName, dsLoad.Tables[14], "Kind_Name", "Kind_Id|Parent_Kind_Id", "Kind_Key=400", new string[] { "Kind_Key=120", "Kind_Name=200" }, new string[] { "拼音", "名称" }, "Kind_Id", "Level>0", "Kind_Key", "Kind_Id", "Parent_Kind_Id", "", true);
            #endregion

            #region 绑定辅料大类
            StaticFunctions.BindDplComboByTable(extPMaPc, dsLoad.Tables[13], "Stones_Name", "Stones_Id|Parent_Stones_Id", "Stones_Key=400", new string[] { "Stones_Key=120", "Stones_Name=200" }, new string[] { "拼音", "名称" }, "Stones_Id", "Level>0", "Stones_Key", "Stones_Id", "Parent_Stones_Id", "", true);
            #endregion

            #region 绑定基础工艺
            StaticFunctions.BindDplComboByTable(extTreeTech, dsLoad.Tables[2], "Tec_Name", "Tec_Id|Parent_Tec_Id", "Tec_Key=400", new string[] { "Tec_Key=120", "Tec_Name=200" }, new string[] { "拼音", "名称" }, "Tec_Id", "Level>0", "Tec_Key", "Tec_Id", "Parent_Tec_Id", "", true);
            #endregion

            #region 绑定表面工艺
            StaticFunctions.BindDplComboByTable(extPThceSur, dsLoad.Tables[6], "Sur_Name", "Sur_Id|Parent_Sur_Id", "Sur_Key=400", new string[] { "Sur_Key=120", "Sur_Name=200" }, new string[] { "拼音", "名称" }, "Sur_Id", "Level>0", "Sur_Key", "Sur_Id", "Parent_Sur_Id", "", true);
            #endregion

            #region 绑定花型
            StaticFunctions.BindDplComboByTable(extPFlowerType, dsLoad.Tables[7], "Flow_Name", "Flow_Id|Parent_Flow_Id", "Flow_Key=400", new string[] { "Flow_Key=120", "Flow_Name=200" }, new string[] { "拼音", "名称" }, "Flow_Id", "Level>0", "Flow_Key", "Flow_Id", "Parent_Flow_Id", "", true);
            #endregion

            #region 绑定边型
            StaticFunctions.BindDplComboByTable(extPSideType, dsLoad.Tables[8], "Squ_Name", "Squ_Id|Parent_Squ_Id", "Squ_Key=400", new string[] { "Squ_Key=120", "Squ_Name=200" }, new string[] { "拼音", "名称" }, "Squ_Id", "Level>0", "Squ_Key", "Squ_Id", "Parent_Squ_Id", "", true);
            #endregion

            #region 绑定圈口
            StaticFunctions.BindDplComboByTable(extPRound, dsLoad.Tables[9], "Rou_Name", "Rou_Id|Parent_Rou_Id", "Rou_Key=400", new string[] { "Rou_Key=120", "Rou_Name=200" }, new string[] { "拼音", "名称" }, "Rou_Id", "Level>0", "Rou_Key", "Rou_Id", "Parent_Rou_Id", "", true);
            #endregion

            #region 绑定面宽
            StaticFunctions.BindDplComboByTable(extPFaceWith, dsLoad.Tables[10], "Fac_Name", "Fac_Id|Parent_Fac_Id", "Fac_Key=400", new string[] { "Fac_Key=120", "Fac_Name=200" }, new string[] { "拼音", "名称" }, "Fac_Id", "Level>0", "Fac_Key", "Fac_Id", "Parent_Fac_Id", "", true);
            #endregion

            #region 绑定扣型
            StaticFunctions.BindDplComboByTable(extPFastener, dsLoad.Tables[11], "Fas_Name", "Fas_Id|Parent_Fas_Id", "Fas_Key=400", new string[] { "Fas_Key=120", "Fas_Name=200" }, new string[] { "拼音", "名称" }, "Fas_Id", "Level>0", "Fas_Key", "Fas_Id", "Parent_Fas_Id", "", true);
            #endregion

            #region 绑定结构
            StaticFunctions.BindDplComboByTable(extPStructure, dsLoad.Tables[12], "Str_Name", "Str_Id|Parent_Str_Id", "Str_Key=400", new string[] { "Str_Key=120", "Str_Name=200" }, new string[] { "拼音", "名称" }, "Str_Id", "Level>0", "Str_Key", "Str_Id", "Parent_Str_Id", "", true);
            #endregion

            string[] strKeyBus = "EUser_Id,EDept_Id,Fy_Id,Company_Id,Flag".Split(",".ToCharArray());
            string[] strValBus = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     //CApplication.App.CurrentSession.Company_Id.ToString(),
                              CApplication.App.CurrentSession.FyId.ToString(),
                     "9" };
            DataSet dsLoadBus = this.DataRequest_By_DataSet(strSpName, strKeyBus, strValBus);

            #region 绑定营销类别
            StaticFunctions.BindDplComboByTable(extBus_Pc_Id, dsLoadBus.Tables[0], "Kind_Name", "Kind_Id|Parent_Kind_Id", "Kind_Key=400", new string[] { "Kind_Key=120", "Kind_Name=200" }, new string[] { "拼音", "名称" }, "Kind_Id", "Level>0", "Kind_Key", "Kind_Id", "Parent_Kind_Id", "", true);
            #endregion
            #region 绑定营销属性1
            StaticFunctions.BindDplComboByTable(extBusiness_Type1, dsLoadBus.Tables[1], "Attr_Name", "Attr_Id|Parent_Attr_Id", "Attr_Key=400", new string[] { "Attr_Key=120", "Attr_Name=200" }, new string[] { "拼音", "名称" }, "Attr_Id", "Level>0", "Attr_Key", "Attr_Id", "Parent_Attr_Id", "", true);
            #endregion
            #region 绑定营销属性2
            StaticFunctions.BindDplComboByTable(extBusiness_Type2, dsLoadBus.Tables[2], "Attr_Name", "Attr_Id|Parent_Attr_Id", "Attr_Key=400", new string[] { "Attr_Key=120", "Attr_Name=200" }, new string[] { "拼音", "名称" }, "Attr_Id", "Level>0", "Attr_Key", "Attr_Id", "Parent_Attr_Id", "", true);
            #endregion
            #region 绑定营销属性3
            StaticFunctions.BindDplComboByTable(extBusiness_Type3, dsLoadBus.Tables[3], "Attr_Name", "Attr_Id|Parent_Attr_Id", "Attr_Key=400", new string[] { "Attr_Key=120", "Attr_Name=200" }, new string[] { "拼音", "名称" }, "Attr_Id", "Level>0", "Attr_Key", "Attr_Id", "Parent_Attr_Id", "", true);
            #endregion
            #region 绑定产品类型
            StaticFunctions.BindDplComboByTable(dplBus_Bind, dsLoadBus.Tables[4], "SetText", "SetValue",
                new string[] { "SetValue", "SetText" },
                new string[] { "序号", "产品类型" }, true, "", "", false);
            //lookSex.ItemIndex = -1;
            #endregion


            #region 绑定柜台
            StaticFunctions.BindDplComboByTable(dplBus_Counter, dsLoadBus.Tables[5], "Name", "Counter_Id",
                new string[] { "Counter_Id", "Name" },
                new string[] { "序号", "名称" }, true, "", "", false);
            #endregion

            #region 绑定系列
            StaticFunctions.BindDplComboByTable(dplseries, dsLoadBus.Tables[6], "Name", "Series_Id",
                new string[] { "Series_Id", "Name" },
                new string[] { "序号", "名称" }, true, "", "", false);
            #endregion

            LoadData();
            SetBtnTxt(dt.Rows[0]["StateUndetermined"].ToString());
            StrMode = "View";
            SetMode(StrMode);
        }

        private void SetBtnTxt(string StateUndetermined)
        {
            if (StateUndetermined == "True")
            {
                btnPrecate.Text = "撤销发布";
            }
            else
            {
                btnPrecate.Text = "发布到订购网";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            decimal Bus_AmoutFee;
            bool Bus_AmoutFeeFlag = decimal.TryParse(txtBus_AmoutFee.Text.Trim(), out Bus_AmoutFee);
            decimal Bus_WeightFee;
            bool Bus_WeightFeeFlag = decimal.TryParse(txtBus_WeightFee.Text.Trim(), out Bus_WeightFee);
            if (Bus_AmoutFeeFlag == true && Bus_WeightFeeFlag == true && Bus_AmoutFee > 0 && Bus_WeightFee > 0)
            {
                MessageBox.Show("按克销售工费与按件销售工费只能输入一个！");
                return;
            }
            if (Bus_AmoutFeeFlag == true && Bus_WeightFeeFlag == true && Bus_AmoutFee <= 0 && Bus_WeightFee <= 0)
            {
                MessageBox.Show("请您需输入一个按克销售工费或按件销售工费！");
                return;
            }



            decimal Bus_AmoutSaleFee;
            bool Bus_AmoutSaleFeeFlag = decimal.TryParse(txtBus_AmoutSaleFee.Text.Trim(), out Bus_AmoutSaleFee);
            decimal Bus_WeightSaleFee;
            bool Bus_WeightSaleFeeFlag = decimal.TryParse(txtBus_WeightSaleFee.Text.Trim(), out Bus_WeightSaleFee);
            if (Bus_AmoutSaleFeeFlag == true && Bus_WeightSaleFeeFlag == true && Bus_AmoutSaleFee > 0 && Bus_WeightSaleFee > 0)
            {
                MessageBox.Show("按克零售工费与按件零售工费只能输入一个！");
                return;
            }

            if (Bus_AmoutSaleFeeFlag == true && Bus_WeightSaleFeeFlag == true && Bus_AmoutSaleFee <= 0 && Bus_WeightSaleFee <= 0)
            {
                MessageBox.Show("请您需输入一个按克零售工费或按件零售工费！");
                return;
            }

            SaveOneData();
            StrMode = "View";
            SetMode(StrMode);
        }

        //单件修改
        public void SaveOneData()
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,PM_Id,Bus_Pc_Id,Business_Type1,Business_Type2,Business_Type3,Bus_WeightFee,Bus_AmoutFee,StateUndetermined,Bus_Bind,Bus_Series,Bus_Designer,Bus_Counter,Bus_PatentNumber,Bus_Description,Bus_WeightSaleFee,Bus_AmoutSaleFee,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                        CApplication.App.CurrentSession.UserId.ToString(),
                        CApplication.App.CurrentSession.DeptId.ToString(),
                        CApplication.App.CurrentSession.FyId.ToString(), 
                        //CApplication.App.CurrentSession.Company_Id.ToString(), 
                                 CApplication.App.CurrentSession.FyId.ToString(),
                        dt.Rows[0]["Bus_PM_Id"].ToString().Trim(),
                        extBus_Pc_Id.EditValue.ToString(),
                        extBusiness_Type1.EditValue.ToString(),
                        extBusiness_Type2.EditValue.ToString(),
                        extBusiness_Type3.EditValue.ToString(),
                        txtBus_WeightFee.Text.Trim()==string.Empty?"0": txtBus_WeightFee.Text.Trim(),
                        txtBus_AmoutFee.Text.Trim()==string.Empty?"0": txtBus_AmoutFee.Text.Trim(), 
                        chkStateUndetermined.Checked.ToString()=="True"?"1":"0",
                        dplBus_Bind.Text.Trim()==string.Empty?"-1":dplBus_Bind.EditValue.ToString(),
                        dplseries.Text.Trim()==string.Empty?"":dplseries.EditValue.ToString(),
                        txtBus_Designer.Text.Trim(),
                        dplBus_Counter.Text.Trim()==string.Empty?"-1":dplBus_Counter.EditValue.ToString(),
                        txtBus_PatentNumber.Text.Trim(),
                        txtBus_Description.Text.Trim(),
                           txtBus_WeightSaleFee.Text.Trim()==string.Empty?"0": txtBus_WeightSaleFee.Text.Trim(),
                        txtBus_AmoutSaleFee.Text.Trim()==string.Empty?"0": txtBus_AmoutSaleFee.Text.Trim(), 
                        "12" };
            DataTable dtReuslt = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
            if (dtReuslt != null && dtReuslt.Rows[0][0].ToString() == "OK")
            {
                MessageBox.Show("修改成功");
            }
            else
            {
                MessageBox.Show("修改失败");
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            StrMode = "Edit";
            SetMode(StrMode);
        }

        public void LoadData()
        {
            DataRow dr = dt.Rows[0];
            StaticFunctions.SetDataRow2ControlValue(groupBox1, dr);
            StaticFunctions.SetDataRow2ControlValue(groupBox2, dr);
            string ImagePath = System.Environment.CurrentDirectory + "\\" + ConfigurationManager.AppSettings["ImageFilePath"] + "\\" + dr["StylePic"].ToString() + "_ver1";
            if (File.Exists(ImagePath) && dr["StylePic"].ToString().Trim() != string.Empty)
            {
                picImg.ImageLocation = ImagePath;
            }
            else
            {
                StaticFunctions.GetImageByte(dr["StylePic"].ToString());
                picImg.ImageLocation = ImagePath;
            }
        }

        private void btnBackout_Click(object sender, EventArgs e)
        {
            StrMode = "View";
            SetMode(StrMode);
            LoadData();
        }

        private void btnPrecate_Click(object sender, EventArgs e)
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,PM_Id,StateUndetermined,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                        CApplication.App.CurrentSession.UserId.ToString(),
                        CApplication.App.CurrentSession.DeptId.ToString(),
                        CApplication.App.CurrentSession.FyId.ToString(), 
                        //CApplication.App.CurrentSession.Company_Id.ToString(), 
                                 CApplication.App.CurrentSession.FyId.ToString(),
                        dt.Rows[0]["Bus_PM_Id"].ToString().Trim(), 
                        btnPrecate.Text=="发布到订购网"?"1":"0", 
                        "10" };
            DataTable dtReuslt = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
            if (dtReuslt != null && dtReuslt.Rows[0][0].ToString() == "OK")
            {
                MessageBox.Show("修改成功");
                if (dt.Rows[0]["StateUndetermined"].ToString() == "False")
                {
                    SetBtnTxt("True");
                    chkStateUndetermined.Checked = true;
                }
                else
                {
                    SetBtnTxt("False");
                    chkStateUndetermined.Checked = false;
                }
            }
            else
            {
                MessageBox.Show("修改失败");
                return;
            }
        }

        private void SetMode(string Mode)
        {
            switch (Mode)
            {
                case "Edit":
                    txtBus_AmoutFee.Enabled = txtBus_WeightFee.Enabled = extBus_Pc_Id.Enabled = extBusiness_Type1.Enabled = extBusiness_Type2.Enabled = dplBus_Bind.Enabled = extBusiness_Type3.Enabled = chkStateUndetermined.Enabled = dplseries.Enabled = dplBus_Counter.Enabled = txtBus_WeightSaleFee.Enabled = txtBus_AmoutSaleFee.Enabled = txtBus_Description.Enabled = txtBus_Designer.Enabled =txtBus_PatentNumber.Enabled= true;
                    btnSave.Enabled = btnBackout.Enabled = btnPrecate.Enabled = true;
                    btnUpdate.Enabled = false;
                    break;
                case "View":
                    txtBus_AmoutFee.Enabled = txtBus_WeightFee.Enabled = extBus_Pc_Id.Enabled = extBusiness_Type1.Enabled = extBusiness_Type2.Enabled = extBusiness_Type3.Enabled = chkStateUndetermined.Enabled = dplBus_Bind.Enabled = dplseries.Enabled = dplBus_Counter.Enabled = txtBus_WeightSaleFee.Enabled = txtBus_AmoutSaleFee.Enabled = txtBus_Description.Enabled = txtBus_Designer.Enabled = txtBus_PatentNumber.Enabled = false;
                    btnSave.Enabled = btnBackout.Enabled = false;
                    btnUpdate.Enabled = btnPrecate.Enabled = true;
                    break;
                default:
                    break;
            }
        }
    }
}
