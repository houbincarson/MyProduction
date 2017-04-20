using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System.Configuration;
using System.IO;

namespace ProduceManager
{
    public partial class frmProdManager : frmEditorBase
    {
        public frmProdManager()
        {
            InitializeComponent();
        }
        string strSpName = "Bse_Prod_Model_Info_Add_Edit_Del_Qj"; 
        DataSet dsLoad;
        DataTable dt = null; 
        public void GetData()
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
                        dateEdit1.Text.Trim()==string.Empty?"":dateEdit1.EditValue.ToString(),//MarketTime
                        dplfy.Text.Trim()==string.Empty?"":dplfy.EditValue.ToString(),//Help_Factory
                        extPKindName.Text.Trim()==string.Empty?"-1":extPKindName.EditValue.ToString(),//Pc_Id
                        chkPqName.Text.Trim()==string.Empty?"":chkPqName.EditValue.ToString(),// Material
                        lookSex.Text.Trim()==string.Empty?"-1":lookSex.EditValue.ToString(),//Sex
                        lookdesginType.Text.Trim()==string.Empty?"-1":lookdesginType.EditValue.ToString(),//DesignType
                        extPMaPc.Text.Trim()==string.Empty?"-1":extPMaPc.EditValue.ToString(),//Material_Pc_Id 辅料大类
                        lookZuHeType.Text.Trim()==string.Empty?"-1":lookZuHeType.EditValue.ToString(),//Compound_Type
                        txtSmallSize.Text.Trim(),//SizeSmall
                        txtbigSize.Text.Trim(),//SizeLarger
                        txtSmallWeight.Text.Trim(),//WeightSmall
                        txtbigWeight.Text.Trim(),//WeightLarger
                        extTreeTech.Text.Trim()==string.Empty?"-1":extTreeTech.EditValue.ToString(),//BasicTechnology
                        extPThceSur.Text.Trim()==string.Empty?"-1":extPThceSur.EditValue.ToString(),//BasicTechnologySurface
                        extPFlowerType.Text.Trim()==string.Empty?"-1":extPFlowerType.EditValue.ToString(),//BseShapeFlower
                        extPSideType.Text.Trim()==string.Empty?"-1":extPSideType.EditValue.ToString(),//BseShapeSquare
                        extPRound.Text.Trim()==string.Empty?"-1":extPRound.EditValue.ToString(),//BseRound
                        extPFaceWith.Text.Trim()==string.Empty?"-1":extPFaceWith.EditValue.ToString(),//BseFaceWidth
                        extPFastener.Text.Trim()==string.Empty?"-1":extPFastener.EditValue.ToString(),//BseFastener
                        extPStructure.Text.Trim()==string.Empty?"-1":extPStructure.EditValue.ToString(),//BseStructure 
                        checkState.Checked.ToString(),
                        "1" };
            dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);

            DataColumn newColumn = dsLoad.Tables[0].Columns.Add("Icon", Type.GetType("System.Byte[]"));
            newColumn.AllowDBNull = true;
            gridCMain.DataSource = dsLoad.Tables[0];
            gridVMain.BestFitColumns();
        }

        private void frmProdManager_Load(object sender, EventArgs e)
        {

            gridVMain.BestFitColumns();
            //dateEdit1.EditValue = DateTime.Now.ToShortDateString();
            //绑定协作工厂
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     "2" };
            dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            StaticFunctions.BindDplComboByTable(dplfy, dsLoad.Tables[0], "Name", "Fy_Id",
                 new string[] { "Fy_Id", "Name" },
                 new string[] { "编号", "名称" }, true, "", "", true);
            dplfy.ItemIndex = -1;

            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit1, dsLoad.Tables[0], "Name", "Fy_Id",//生产工厂，协作工厂
                new string[] { "Fy_Id", "Name" },
                new string[] { "编号", "名称" }, true, "", "", true);
            #endregion
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit2, dsLoad.Tables[3], "SetText", "SetValue",//性别
                new string[] { "SetValue", "SetText" },
                new string[] { "编号", "名称" }, true, "", "", true);
            #endregion
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit3, dsLoad.Tables[4], "SetText", "SetValue",//设计类型
                new string[] { "SetValue", "SetText" },
                new string[] { "序号", "设计类型" }, true, "", "", true);
            #endregion
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit4, dsLoad.Tables[5], "SetText", "SetValue",//组合方式
                new string[] { "SetValue", "SetText" },
                new string[] { "序号", "组合类型" }, true, "", "", true);
            #endregion
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit5, dsLoad.Tables[1], "Name", "Pq_Id", //成色
                new string []{"Pq_Id","Name"},
                new string []{"序号","成色名称"},true,"","",true);
            #endregion
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit6, dsLoad.Tables[2], "Tec_Name", "Tec_Id", //基础工艺
                new string[] { "Tec_Id", "Tec_Name" },
                new string[] { "序号", "基础工艺名称" }, true, "", "", true);
            #endregion
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit7, dsLoad.Tables[6], "Sur_Name", "Sur_Id", //表面工艺
                new string[] { "Sur_Id", "Sur_Name" },
                new string[] { "序号", "表面工艺名称" }, true, "", "", true);
            #endregion
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit8, dsLoad.Tables[7], "Flow_Name", "Flow_Id", //花型
                new string[] { "Flow_Id", "Flow_Name" },
                new string[] { "序号", "花型名称" }, true, "", "", true);
            #endregion 
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit9, dsLoad.Tables[8], "Squ_Name", "Squ_Id", //边型
                new string[] { "Squ_Id", "Squ_Name" },
                new string[] { "序号", "边型名称" }, true, "", "", true);
            #endregion 
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit10, dsLoad.Tables[9], "Rou_Name", "Rou_Id", //圈口
                new string[] { "Rou_Id", "Rou_Name" },
                new string[] { "序号", "圈口名称" }, true, "", "", true);
            #endregion 
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit11, dsLoad.Tables[10], "Fac_Name", "Fac_Id", //面宽
                new string[] { "Fac_Id", "Fac_Name" },
                new string[] { "序号", "面宽名称" }, true, "", "", true);
            #endregion 
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit12 ,dsLoad.Tables[11], "Fas_Name", "Fas_Id", //扣型
                new string[] { "Fas_Id", "Fas_Name" },
                new string[] { "序号", "扣型名称" }, true, "", "", true);
            #endregion 
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit13, dsLoad.Tables[12], "Str_Name", "Str_Id", //结构
                new string[] { "Str_Id", "Str_Name" },
                new string[] { "序号", "扣型名称" }, true, "", "", true);
            #endregion 
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit14, dsLoad.Tables[13], "Stones_Name", "Stones_Id", //辅料大类
                new string[] { "Stones_Id", "Stones_Name" },
                new string[] { "序号", "辅料大类名称" }, true, "", "", true);
            #endregion 
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit15, dsLoad.Tables[14], "Kind_Name", "Kind_Id", //类别名称
                new string[] { "Kind_Id", "Kind_Name" },
                new string[] { "序号", "类别名称" }, true, "", "", true);
            #endregion 
            #region
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit16, dsLoad.Tables[15], "Name", "Part_Id",//配件
                new string[] { "Part_Id", "Name" },
                new string[] { "编号", "名称" }, true, "", "", true);
            #endregion
            #region 绑定成色
            StaticFunctions.BindCheckedComboBoxEdit(chkPqName, dsLoad.Tables[1], "Name", "Pq_Id", "", "");

            //foreach (CheckedListBoxItem item in chkPqName.Properties.Items)
            //{
            //    item.CheckState = CheckState.Checked;
            //}
            #endregion 
            #region 绑定基础工艺
            StaticFunctions.BindDplComboByTable(extTreeTech, dsLoad.Tables[2], "Tec_Name", "Tec_Id|Parent_Tec_Id", "Tec_Key=400", new string[] { "Tec_Key=120", "Tec_Name=200" }, new string[] { "拼音", "名称" }, "Tec_Id", "Level>0", "Tec_Key", "Tec_Id", "Parent_Tec_Id", "", true);
            #endregion 
            #region 绑定性别
            StaticFunctions.BindDplComboByTable(lookSex, dsLoad.Tables[3], "SetText", "SetValue",
                new string[] { "SetValue", "SetText" },
                new string[] { "序号", "性别" }, true, "", "", false);
            //lookSex.ItemIndex = -1;
            #endregion 
            #region 绑定设计类型
            StaticFunctions.BindDplComboByTable(lookdesginType, dsLoad.Tables[4], "SetText", "SetValue",
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

            #region 绑定辅料大类
            StaticFunctions.BindDplComboByTable(extPMaPc, dsLoad.Tables[13], "Stones_Name", "Stones_Id|Parent_Stones_Id", "Stones_Key=400", new string[] { "Stones_Key=120", "Stones_Name=200" }, new string[] { "拼音", "名称" }, "Stones_Id", "Level>0", "Stones_Key", "Stones_Id", "Parent_Stones_Id", "", true);
            #endregion

            #region 绑定类别
            StaticFunctions.BindDplComboByTable(extPKindName, dsLoad.Tables[14], "Kind_Name", "Kind_Id|Parent_Kind_Id", "Kind_Key=400", new string[] { "Kind_Key=120", "Kind_Name=200" }, new string[] { "拼音", "名称" }, "Kind_Id", "Level>0", "Kind_Key", "Kind_Id", "Parent_Kind_Id", "", true);
            #endregion
            
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (gridVMain.RowCount == 0)
                return; 
            this.Cursor = Cursors.WaitCursor;
            StaticFunctions.GridViewExportToExcel(gridVMain, this.Text, null);
            this.Cursor = Cursors.Arrow;
        }
         
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProdManagerInfo frmInfo = new frmProdManagerInfo();
            frmInfo.Show();
        }

        private void btnMaintain_Click(object sender, EventArgs e)
        {   
            //导到维护 多行数据
            DataTable dtMany = (gridCMain.DataSource) as DataTable;
            frmProdManagerInfo AuditInfo = new frmProdManagerInfo(dtMany);
            AuditInfo.ShowDialog();
        }
          
        private void gridVMain_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = this.gridVMain.GetFocusedDataRow();
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Pm_Id,Flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     dr["PM_Id"].ToString(),
                     "7" };
            DataTable dtPmId = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
            frmProdManagerInfo AuditInfo = new frmProdManagerInfo(dtPmId);
            AuditInfo.ShowDialog();
        }



        private void repImg_Popup(object sender, EventArgs e)
        {
            DataRow _tpDr = gridVMain.GetDataRow(gridVMain.FocusedRowHandle);
            if (_tpDr["Icon"].Equals(System.DBNull.Value))
            {
                byte[] _tpBytes = ServerRefManager.PicFileRead(_tpDr["StylePic"].ToString(), _tpDr["Pic_Version"].ToString());
                gridVMain.FocusedColumn = gridVMain.Columns["Icon"];
                gridVMain.ShowEditor();
                if (gridVMain.ActiveEditor is DevExpress.XtraEditors.ImageEdit)
                {
                    if ((gridVMain.ActiveEditor as DevExpress.XtraEditors.ImageEdit).Properties.ShowPopupShadow == false)
                    {
                        (gridVMain.ActiveEditor as DevExpress.XtraEditors.ImageEdit).ShowPopup();
                    }
                }
                if (_tpBytes == null)
                {
                    _tpDr["Icon"] = new byte[1];
                }
                else
                {
                    _tpDr["Icon"] = _tpBytes;
                }
                gridVMain.RefreshRow(gridVMain.FocusedRowHandle);
                (gridVMain.ActiveEditor as DevExpress.XtraEditors.ImageEdit).ShowPopup();
            } 
        }

        private void gridVMain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            picImage.Visible = true;
            DataRow _tpDr = gridVMain.GetDataRow(gridVMain.FocusedRowHandle);
            string ImagePath = System.Environment.CurrentDirectory + "\\" + ConfigurationManager.AppSettings["ImageFilePath"] + "\\" + _tpDr["StylePic"].ToString() + "_ver1";
            if (File.Exists(ImagePath) && _tpDr["StylePic"].ToString().Trim() != string.Empty)
            {
                picImage.ImageLocation = ImagePath;
            }
            else
            {
                StaticFunctions.GetImageByte(_tpDr["StylePic"].ToString());
                picImage.ImageLocation = ImagePath;
            }
            picImage.ImageLocation = ImagePath; 
        }
    }
}
