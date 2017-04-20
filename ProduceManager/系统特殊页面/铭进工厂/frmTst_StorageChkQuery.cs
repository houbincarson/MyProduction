using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProduceManager
{
    public partial class frmTst_StorageChkQuery : frmEditorBase
    {
        #region private params
        private DataTable dt = null;
        private DataTable dtDept = null;
        private DataTable dtSys = null;

        private DataTable dtPProd = null;
        private DataTable dtCProd = null;
        private DataTable dtInMat = null;
        private DataTable dtOutMat = null;
        private DataTable dtInProd = null;
        private DataTable dtOutProd = null;
        private DataTable dtMoveIn = null;
        private DataTable dtMoveOut = null;
        private DataTable dtSumIn = null;
        private DataTable dtSumOut = null;
        private DataTable dtURece = null;
        private DataTable dtUDeli = null;
        private DataTable dtULoss = null;
        private DataTable dtTstSend = null;
        private DataTable dtTstRece = null;
        private DataTable dtUChkLoss = null;
        private DataTable dtALoss = null;

        #endregion

        public frmTst_StorageChkQuery()
        {
            InitializeComponent();
        }

        private void InitContr()
        {
            StaticFunctions.BindDplComboByTable(repGoods_PCType, dtSys, "Name", "SetValue",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "SetKey='Goods_PCType'", false);

            StaticFunctions.BindDplComboByTable(repCSto_Class, dtSys, "Name", "SetValue",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "SetKey='Sto_Class'", true);
            StaticFunctions.BindDplComboByTable(repPSto_Class, dtSys, "Name", "SetValue",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "SetKey='Sto_Class'", true);

            StaticFunctions.BindDplComboByTable(repUDeptULoss, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=1", true);

            StaticFunctions.BindDplComboByTable(repFDept, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=2", true);

            StaticFunctions.BindDplComboByTable(repTDept, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=2", true);

            StaticFunctions.BindDplComboByTable(repUDeptURece, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=1", true);

            StaticFunctions.BindDplComboByTable(repUDeptUDeli, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=1", true);

            StaticFunctions.BindDplComboByTable(repRDept, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=2", true);

            StaticFunctions.BindDplComboByTable(repDDept, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=2", true);

            StaticFunctions.BindDplComboByTable(repUDeptTstSend, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=1", true);

            StaticFunctions.BindDplComboByTable(repUDeptTstRece, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=1", true);

            StaticFunctions.BindDplComboByTable(repDDeptTstRece, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=2", true);

            StaticFunctions.BindDplComboByTable(repDeptTstSend, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=2", true);

            StaticFunctions.BindDplComboByTable(repUDeptUChk, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=1", true);

            StaticFunctions.BindDplComboByTable(repDeptUChk, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=2", true);

            StaticFunctions.BindDplComboByTable(repDeliType, dtSys, "Name", "SetValue",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "SetKey='DeliType'", true);
            StaticFunctions.BindDplComboByTable(repTstDeliType, dtSys, "Name", "SetValue",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "SetKey='DeliType'", true);
            StaticFunctions.BindDplComboByTable(repChkItem_TypeCProd, dtSys, "Name", "SetValue",
               new string[] { "Number", "Name" },
               new string[] { "编号", "名称" }, true, "", "SetKey='ChkItem_Type'", true);
            StaticFunctions.BindDplComboByTable(repChkItem_TypePProd, dtSys, "Name", "SetValue",
               new string[] { "Number", "Name" },
               new string[] { "编号", "名称" }, true, "", "SetKey='ChkItem_Type'", true);
        }

        private void frmEmpEdit_Load(object sender, EventArgs e)
        {
            string[] strKey="Form,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
            DataSet dsLoad =this.DataRequest_By_DataSet("Get_Form_Load_Table", strKey,
                new string[] { this.Name
                    , CApplication.App.CurrentSession.UserId.ToString()
                    , CApplication.App.CurrentSession.DeptId.ToString()
                    , CApplication.App.CurrentSession.FyId.ToString() });
            dsLoad.AcceptChanges();
            dtDept = dsLoad.Tables[0];
            dtSys = dsLoad.Tables[1];

            StaticFunctions.BindDplComboByTable(repTst, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "Dept_Type=2", true);

            InitContr();

            dtCreate_DtSt.EditValue = DateTime.Now.AddDays(-1).ToShortDateString();
            dtCreate_DtEd.EditValue = DateTime.Now.AddDays(-1).ToShortDateString();

            ckbShowDetail_CheckedChanged(null, null);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int index = gridView3.FocusedRowHandle;//原来的FocusedRowHandle

                string[] strKey = "StartWT_Dt,EndWT_Dt,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                dt = this.DataRequest_By_DataTable("Tst_StorageChk_Add_Edit_Del",strKey
                   , new string[] {
                    dtCreate_DtSt.Text == string.Empty ? "" : dtCreate_DtSt.EditValue.ToString()
                    ,dtCreate_DtEd.Text == string.Empty ? "" : dtCreate_DtEd.EditValue.ToString() 
                    , CApplication.App.CurrentSession.UserId.ToString()
                    , CApplication.App.CurrentSession.DeptId.ToString()
                    , CApplication.App.CurrentSession.FyId.ToString(),"4"});
                dt.AcceptChanges();
                gridControl3.DataSource = dt.DefaultView;
                gridView3.BestFitColumns();

                if (index == gridView3.FocusedRowHandle)
                {
                    //如果原来的FocusedRowHandle=新的FocusedRowHandle,
                    //因为如果不等，则gridControl1.DataSource = dtDep.DefaultView会自动引发
                    gridView3_FocusedRowChanged(null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridView3.FocusedRowHandle));
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

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView3.GetFocusedDataRow();
            if (dr == null)
                return;

            ckbShowDetail.Checked = false;
        }

        private void GetChkData()
        {
            DataRow dr = gridView3.GetFocusedDataRow();
            if (dr == null)
                return;
            string[] strKey = "Dept_Id,StartWT_Dt,StorageChk_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataSet dsLoad =this.DataRequest_By_DataSet("Tst_StorageChk_Add_Edit_Del",strKey
              , new string[] {
                   dr["Dept_Id"].ToString(),
                   dr["AccountDt"].ToString(),
                   dr["StorageChk_Id"].ToString(),
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                    "5"
                   });
            dsLoad.AcceptChanges();
            DataTable dtProdCk = dsLoad.Tables[0];
            dtPProd = dtProdCk.Clone();
            dtCProd = dtProdCk.Clone();
            foreach (DataRow drv in dtProdCk.Rows)
            {
                if (drv["ProChkFlag"].ToString() == "1")
                {
                    dtPProd.ImportRow(drv);
                }
                else
                {
                    dtCProd.ImportRow(drv);
                }
            }
            DataTable dtMat = dsLoad.Tables[1];
            dtInMat = dtMat.Clone();
            dtOutMat = dtMat.Clone();
            foreach (DataRow drv in dtMat.Rows)
            {
                if (drv["IN_OUT_TYPE"].ToString() == "1")
                {
                    dtInMat.ImportRow(drv);
                }
                else
                {
                    dtOutMat.ImportRow(drv);
                }
            }

            DataTable dtProd = dsLoad.Tables[2];
            dtInProd = dtProd.Clone();
            dtOutProd = dtProd.Clone();
            foreach (DataRow drv in dtProd.Rows)
            {
                if (drv["IN_OUT_TYPE"].ToString() == "1")
                {
                    dtInProd.ImportRow(drv);
                }
                else
                {
                    dtOutProd.ImportRow(drv);
                }
            }

            DataTable dtMove = dsLoad.Tables[3];
            dtMoveIn = dtMove.Clone();
            dtMoveOut = dtMove.Clone();
            foreach (DataRow drv in dtMove.Rows)
            {
                if (drv["MOVE_FLAG"].ToString() == "1")
                {
                    dtMoveIn.ImportRow(drv);
                }
                else
                {
                    dtMoveOut.ImportRow(drv);
                }
            }

            dtSumIn = dsLoad.Tables[4];
            dtSumOut = dsLoad.Tables[5];
            dtULoss = dsLoad.Tables[6];
            dtUChkLoss = dsLoad.Tables[7];

            DataTable dtReces = dsLoad.Tables[8];
            dtURece = dtReces.Clone();
            dtTstSend = dtReces.Clone();
            foreach (DataRow drv in dtReces.Rows)
            {
                if (drv["URECE"].ToString() == "1")
                {
                    dtURece.ImportRow(drv);
                }
                if (drv["TSEND"].ToString() == "1")
                {
                    dtTstSend.ImportRow(drv);
                }
            }

            DataTable dtDelis = dsLoad.Tables[9];
            dtUDeli = dtDelis.Clone();
            dtTstRece = dtDelis.Clone();
            foreach (DataRow drv in dtDelis.Rows)
            {
                if (drv["URECE"].ToString() == "1")
                {
                    dtUDeli.ImportRow(drv);
                }
                if (drv["TSEND"].ToString() == "1")
                {
                    dtTstRece.ImportRow(drv);
                }
            }

            #region  昨日实物
            gridCPProd.DataSource = dtPProd.DefaultView;
            gridVPProd.BestFitColumns();

            #endregion

            #region 今日实物
            gridCCProd.DataSource = dtCProd.DefaultView;
            gridVCProd.BestFitColumns();

            #endregion

            #region 进料
            gridCInMat.DataSource = dtInMat.DefaultView;
            gridVInMat.BestFitColumns();

            #endregion

            #region 出料
            gridCOutMat.DataSource = dtOutMat.DefaultView;
            gridVOutMat.BestFitColumns();

            #endregion

            #region 进饰

            gridCInProd.DataSource = dtInProd.DefaultView;
            gridVInProd.BestFitColumns();

            #endregion

            #region 出饰

            gridCOutProd.DataSource = dtOutProd.DefaultView;
            gridVOutProd.BestFitColumns();

            #endregion

            #region 移入
            gridCMoveIn.DataSource = dtMoveIn.DefaultView;
            gridVMoveIn.BestFitColumns();

            #endregion

            #region 移出
            gridCMoveOut.DataSource = dtMoveOut.DefaultView;
            gridVMoveOut.BestFitColumns();

            #endregion

            #region 总入库
            gridCSumIn.DataSource = dtSumIn.DefaultView;
            gridVSumIn.BestFitColumns();

            #endregion

            #region 总出库
            gridCSumOut.DataSource = dtSumOut.DefaultView;
            gridVSumOut.BestFitColumns();

            #endregion

            #region 员工损耗
            gridCULoss.DataSource = dtULoss.DefaultView;
            gridVULoss.BestFitColumns();

            #endregion

            this.gridCURece.DataSource = this.dtURece.DefaultView;
            gridVURece.BestFitColumns();

            gridCUDeli.DataSource = dtUDeli.DefaultView;
            gridVUDeli.BestFitColumns();


            this.gridCTstSend.DataSource = this.dtTstSend.DefaultView;
            gridVTstSend.BestFitColumns();

            gridCTstRece.DataSource = dtTstRece.DefaultView;
            gridVTstRece.BestFitColumns();

            this.gridCUChk.DataSource = this.dtUChkLoss.DefaultView;
            gridVUChk.BestFitColumns();

            dtALoss = dsLoad.Tables[10];
            this.gridCALoss.DataSource = this.dtALoss.DefaultView;
            gridVALoss.BestFitColumns();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView3.GetFocusedDataRow();
            if (dr == null)
                return;

            string strDt = DateTime.Parse(dr["AccountDt"].ToString()).ToShortDateString();
            string strDept = dr["Dept_Id"].ToString();
            string strMsg = "是否删除选中的账目记录：";
            DataRow[] drs = dtDept.Select("Dept_Id=" + strDept);
            if (drs.Length > 0)
            {
                strMsg += drs[0]["Name"].ToString();
            }
            strMsg += "-" + strDt;
            if (MessageBox.Show(strMsg, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            string[] strKey = "StartWT_Dt,StorageChk_Id,Dept_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataTable dtAdd =this.DataRequest_By_DataTable("Tst_StorageChk_Add_Edit_Del",strKey
              , new string[] {
                   dr["AccountDt"].ToString(),
                   dr["StorageChk_Id"].ToString(),
                   dr["Dept_Id"].ToString(),
                   CApplication.App.CurrentSession.UserId.ToString(),
                   CApplication.App.CurrentSession.DeptId.ToString(),
                   CApplication.App.CurrentSession.FyId.ToString(),
                   "3"
                   });

            if (dtAdd == null)
            {
                return;
            }
            MessageBox.Show("删除成功.");

            gridView3.DeleteSelectedRows();
            gridView3_FocusedRowChanged(null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, gridView3.FocusedRowHandle));
        }

        private void btnReChk_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView3.GetFocusedDataRow();
            if (dr == null)
                return;

            string strDt = DateTime.Parse(dr["AccountDt"].ToString()).ToShortDateString();
            string strDept = dr["Dept_Id"].ToString();
            string strMsg = "是否重新盘点选中的账目记录：";
            DataRow[] drs = dtDept.Select("Dept_Id=" + strDept);
            if (drs.Length > 0)
            {
                strMsg += drs[0]["Name"].ToString();
            }
            strMsg += "-" + strDt;
            if (MessageBox.Show(strMsg, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
            {
                return;
            }

            frmTst_StorageChkEdit frm = new frmTst_StorageChkEdit();
            frm.strAccount = strDt;
            frm.strReChkDept = strDept;
            frm.strReChkId = dr["StorageChk_Id"].ToString();
            frm.DrReChk = dr;
            frm.ShowDialog();
        }

        private void ckbShowDetail_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbShowDetail.Checked)
            {
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Both;

                this.Cursor = Cursors.WaitCursor;
                GetChkData();
                this.Cursor = Cursors.Default;
            }
            else
            {
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel1;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView3.GetFocusedDataRow();
            if (dr == null)
                return;

            if (dtAccountDtNew.EditValue == null)
                return;

            string strDt = DateTime.Parse(dr["AccountDt"].ToString()).ToShortDateString();
            string strDtNew = DateTime.Parse(dtAccountDtNew.EditValue.ToString()).ToShortDateString();
            string strDept = dr["Dept_Id"].ToString();
            string strMsg = "是否修改选中账目的日期,";
            DataRow[] drs = dtDept.Select("Dept_Id=" + strDept);
            if (drs.Length > 0)
            {
                strMsg += drs[0]["Name"].ToString();
            }
            strMsg += ":" + strDt + "—>" + strDtNew;
            if (MessageBox.Show(strMsg, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }
            string[] strKey = "StartWT_Dt,EndWT_Dt,StorageChk_Id,Dept_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataTable dtAdd = this.DataRequest_By_DataTable("Tst_StorageChk_Add_Edit_Del", strKey
              , new string[] {
                   strDt,strDtNew,
                   dr["StorageChk_Id"].ToString(),
                   strDept,
                   CApplication.App.CurrentSession.UserId.ToString(),
                   CApplication.App.CurrentSession.DeptId.ToString(),
                   CApplication.App.CurrentSession.FyId.ToString(),
                   "21"
                   });

            if (dtAdd == null)
            {
                return;
            }
            MessageBox.Show("成功修改.");
            dr["AccountDt"] = strDtNew;
            dr.AcceptChanges();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (gridView3.RowCount == 0)
                return;
            this.Cursor = Cursors.WaitCursor;
            StaticFunctions.GridViewExportToExcel(gridView3, this.Text, null);
            this.Cursor = Cursors.Arrow;
        }
    }
}
