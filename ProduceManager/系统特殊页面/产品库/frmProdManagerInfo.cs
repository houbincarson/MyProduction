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
    public partial class frmProdManagerInfo : frmEditorBase
    {
        private DataTable dt;
        public frmProdManagerInfo()
        {
            InitializeComponent();
        }

        public frmProdManagerInfo(DataTable dt)
        {
            InitializeComponent();
            this.dt = dt;
        }

        string strSpName = "Bse_Prod_Model_Info_Add_Edit_Del_Qj";
        string strKeyId = "-1";
        DataSet dsLoad;

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void frmProdManagerInfo_Load(object sender, EventArgs e)
        {
            string[] strKey = "Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {strKeyId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     "2" };
            dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            StaticFunctions.BindDplComboByTable(dplfy, dsLoad.Tables[0], "Name", "Fy_Id",
                 new string[] { "Fy_Id", "Name" },
                 new string[] { "编号", "名称" }, true, "", "", true);
            dplfy.ItemIndex = -1;

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
                new string[] { "序号", "性别" }, true, "", "", true);
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

            LoadData(); 
        }
         
        public void SaveData()
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Name,PNumber,Designer,PatentNumber,MarketTime,Help_Factory,Pc_Id,Material,Sex,DesignType,Material_Pc_Id,Compound_Type,SizeSmall,SizeLarger,WeightSmall,WeightLarger,BasicTechnology,BasicTechnologySurface,BseShapeFlower,BseShapeSquare,BseRound,BseFaceWidth,BseFastener,BseStructure,State,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                        txtPName.Text.Trim(),//Name
                        txtPNumber.Text.Trim(),//PNumber
                        txtdesigner.Text.Trim(),//Designer
                        txtPatentNumber.Text.Trim(),//PatentNumber
                        dateEdit1.EditValue.ToString(),//MarketTime
                        dplfy.EditValue.ToString(),//Help_Factory
                        extKindName.EditValue.ToString(),//Pc_Id
                        chkPqName.EditValue.ToString(),// Material
                        lookSex.EditValue.ToString(),//Sex
                        txtdesigner.EditValue.ToString(),//DesignType
                        extPMaPc.EditValue.ToString(),//Material_Pc_Id 辅料大类
                        lookZuHeType.EditValue.ToString(),//Compound_Type
                        txtSmallSize.Text.Trim(),//SizeSmall
                        txtbigSize.Text.Trim(),//SizeLarger
                        txtSmallWeight.Text.Trim(),//WeightSmall
                        txtbigWeight.Text.Trim(),//WeightLarger
                        extTreeTech.EditValue.ToString(),//BasicTechnology
                        extPThceSur.EditValue.ToString(),//BasicTechnologySurface
                        extPFlowerType.EditValue.ToString(),//BseShapeFlower
                        extPSideType.EditValue.ToString(),//BseShapeSquare
                        extPRound.EditValue.ToString(),//BseRound
                        extPFaceWith.EditValue.ToString(),//BseFaceWidth
                        extPFastener.EditValue.ToString(),//BseFastener
                        extPStructure.EditValue.ToString(),//BseStructure 
                        checkState.Checked.ToString(),
                        "3" };
            dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            gridCMain.DataSource = dsLoad.Tables[0];
            gridVMain.BestFitColumns();
            //if (dsLoad.)
            //{
            //    MessageBox.Show("保存成功！");
            //}
            
        }
        public void UpdateData()
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Name,PNumber,Designer,PatentNumber,MarketTime,Help_Factory,Pc_Id,Material,Sex,DesignType,Material_Pc_Id,Compound_Type,SizeSmall,SizeLarger,WeightSmall,WeightLarger,BasicTechnology,BasicTechnologySurface,BseShapeFlower,BseShapeSquare,BseRound,BseFaceWidth,BseFastener,BseStructure,State,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                        txtPName.Text.Trim(),//Name
                        txtPNumber.Text.Trim(),//PNumber
                        txtdesigner.Text.Trim(),//Designer
                        txtPatentNumber.Text.Trim(),//PatentNumber
                        dateEdit1.EditValue.ToString(),//MarketTime
                        dplfy.EditValue.ToString(),//Help_Factory
                        extKindName.EditValue.ToString(),//Pc_Id
                        chkPqName.EditValue.ToString(),// Material
                        lookSex.EditValue.ToString(),//Sex
                        txtdesigner.EditValue.ToString(),//DesignType
                        extPMaPc.EditValue.ToString(),//Material_Pc_Id 辅料大类
                        lookZuHeType.EditValue.ToString(),//Compound_Type
                        txtSmallSize.Text.Trim(),//SizeSmall
                        txtbigSize.Text.Trim(),//SizeLarger
                        txtSmallWeight.Text.Trim(),//WeightSmall
                        txtbigWeight.Text.Trim(),//WeightLarger
                        extTreeTech.EditValue.ToString(),//BasicTechnology
                        extPThceSur.EditValue.ToString(),//BasicTechnologySurface
                        extPFlowerType.EditValue.ToString(),//BseShapeFlower
                        extPSideType.EditValue.ToString(),//BseShapeSquare
                        extPRound.EditValue.ToString(),//BseRound
                        extPFaceWith.EditValue.ToString(),//BseFaceWidth
                        extPFastener.EditValue.ToString(),//BseFastener
                        extPStructure.EditValue.ToString(),//BseStructure 
                        checkState.Checked.ToString(),
                        "4" };
            dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            gridCMain.DataSource = dsLoad.Tables[0];
            gridVMain.BestFitColumns();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        public void LoadData()
        {
            this.gridCMain.DataSource = dt;
        }

        private void gridVMain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridVMain.FocusedRowHandle>=0)
            { 
            DataRow dr = this.gridVMain.GetFocusedDataRow();
            StaticFunctions.SetDataRow2ControlValue(groupBox1, dr); 
            }
        }

    }
}
