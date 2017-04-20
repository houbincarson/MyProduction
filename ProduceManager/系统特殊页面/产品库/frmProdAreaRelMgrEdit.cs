using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace ProduceManager
{
    public partial class frmProdAreaRelMgrEdit : frmEditorBase
    {
        public frmProdAreaRelMgrEdit()
        {
            InitializeComponent();
        }
        string strSpName = "Bse_Prod_Area_Mgr_Add_Edit_Del";
        DataSet dsLoad = null;
        GridCheckMarksSelection cbk = null;
        private void frmProdAreaRelMgrEdit_Load(object sender, EventArgs e)
        {
            dtStDay.EditValue = DateTime.Now.ToShortDateString();
            dtEndDay.EditValue = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                           CApplication.App.CurrentSession.FyId.ToString(),
                     "2" };
            dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            StaticFunctions.BindDplComboByTable(lueArea, dsLoad.Tables[2], "Area_Name", "Area_Id",
                 new string[] { "Area_Id", "Area_Name" },
                 new string[] { "编号", "名称" }, true, "", "", true);
            lueArea.ItemIndex = dsLoad.Tables[2].Rows.Count;
            StaticFunctions.BindDplComboByTable(repositoryItemLookUpEdit1, dsLoad.Tables[1], "Kind_Name", "Kind_Id",
                new string[] { "Kind_Id", "Kind_Name" },
                new string[] { "编号", "名称" }, true, "", "", true);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbk.ClearSelection();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (cbk == null)
            {
                cbk = new GridCheckMarksSelection(gridVMain);
            }
            GetDate();
        }

        private void GetDate()
        {
            string Bus_PNmber = txtBus_PNumber.Text.Trim();
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,Name,Bus_PNumber,PNumber,Designer,PatentNumber,MarketTime,Help_Factory,Pc_Id,Material,Technology,Sex,DesignType,Material_Pc_Id,Compound_Type,SizeSmall,SizeLarger,WeightSmall,WeightLarger,Area_Id,flag".Split(",".ToCharArray());//BasicTechnology,BasicTechnologySurface,BseShapeFlower,BseShapeSquare,BseRound,BseFaceWidth,BseFastener,BseStructure,flag
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                         //CApplication.App.CurrentSession.Company_Id.ToString(),
                                  CApplication.App.CurrentSession.FyId.ToString(),
                        "",//Name
                      Bus_PNmber,//PNumber
                      "",
                        "",//Designer
                        "",//PatentNumber
                        "",//MarketTime
                        "",//Help_Factory
                        "",//Pc_Id
                        "",//Material
                        "-1",//Technology
                        "-1",//Sex
                        "-1",//DesignType
                        "",//Material_Pc_Id
                        "-1",//Compound_Type
                        "0",//SizeSmall
                        "0",//SizeLarger
                        "0",//WeightSmall
                        "0",//WeightLarger
                        //"-1",//BasicTechnology
                        //"-1",//BasicTechnologySurface
                        //"-1",//BseShapeFlower
                        //"-1",//BseShapeSquare
                        //"-1",//BseRound
                        //"-1",//BseFaceWidth
                        //"-1",//BseFastener
                        //"-1",//BseStructure 
                        lueArea.EditValue.ToString(),
                        "101" };
            dsLoad = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            gridCMain.DataSource = dsLoad.Tables[0];
        }

        private void btnSet_Click(object sender, EventArgs e)
        {

            if (cbk.SelectedCount == 0)
            {
                MessageBox.Show("请选择要从区域款式中撤销的款式");
                return;
            }
            string Pm_Ids = cbk.GetKeyIds("PM_Area_Id");
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Company_Id,PmIds,flag".Split(",".ToCharArray());
            string[] strVal = new string[] { 
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(), 
                           //CApplication.App.CurrentSession.Company_Id.ToString(), 
                                    CApplication.App.CurrentSession.FyId.ToString(),
                     Pm_Ids,
                     "102" };
            DataSet ds = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            if (ds.Tables[0].Rows[0][0].ToString() == "OK")
            {
                string[] PM_Area_Ids = Pm_Ids.TrimEnd(',').Split(',');
                for (int i = 0; i < PM_Area_Ids.Length; i++)
                {
                    dsLoad.Tables[0].Rows.Remove(dsLoad.Tables[0].Select("PM_Area_Id = '" + PM_Area_Ids[i].ToString() + "' ")[0]);
                }
                gridCMain.DataSource = dsLoad.Tables[0];

            }
            cbk.ClearSelection();
            MessageBox.Show("移除成功！");
        }

        private void btnMoreCondition_Click(object sender, EventArgs e)
        {
            string condition = txtBus_PNumber.Text.Trim() + "," + dtStDay.EditValue.ToString() + "," + dtEndDay.EditValue.ToString() + ",";
            frmMoreConditionBus frmMore = new frmMoreConditionBus(condition);
            if (frmMore.ShowDialog() == DialogResult.OK)
            {
                if (cbk == null)
                {
                    cbk = new GridCheckMarksSelection(gridVMain);
                }
                this.gridCMain.DataSource = frmMore.dtCondition.Tables[0];
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //DataRow dr = this.gridVMain.GetFocusedDataRow();
            //string ImagePath = System.Environment.CurrentDirectory + "\\" + ConfigurationManager.AppSettings["ImageFilePath"] + "\\" + dr["StylePic"].ToString() + "_ver1";
            //PicImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            //if (File.Exists(ImagePath) && dr["StylePic"].ToString().Trim() != string.Empty)
            //{
            //    PicImg.ImageLocation = ImagePath;
            //}
            //else
            //{
            //    StaticFunctions.GetImageByte(dr["StylePic"].ToString());
            //    PicImg.ImageLocation = ImagePath;
            //}
        }
    }
}
