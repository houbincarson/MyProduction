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
    public partial class frmTst_StorageChkEdit : frmEditorBase
    {
        #region private params
        private bool blInit = false;
        private bool blUChk = false;
        private DataTable dtSys = null;
        private DataTable dtDept = null;
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
        private DataTable dtULoss = null;
        private DataTable dtURece = null;
        private DataTable dtUDeli = null;
        private DataTable dtTstSend = null;
        private DataTable dtTstRece = null;
        private DataTable dtUChkLoss = null;
        private DataTable dtALoss = null;

        private DataTable dtWBasic = null;
        private DataSet dsLoad = null;
        private DataTable dtUChk = null;
        private string strSpName = "Tst_StorageChk_Add_Edit_Del";

        public string strAccount
        {
            get;
            set;
        }
        public string strReChkDept
        {
            get;
            set;
        }
        public string strReChkId
        {
            get;
            set;
        }
        public DataRow DrReChk
        {
            get;
            set;
        }
        #endregion

        public frmTst_StorageChkEdit()
        {
            InitializeComponent();
        }
        private void InitContr()
        {
            if (dsLoad != null)
                return;
            string[] strKey="Form,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
            dsLoad = this.DataRequest_By_DataSet("Get_Form_Load_Table", strKey,
                new string[] { this.Name, CApplication.App.CurrentSession.UserId.ToString(), CApplication.App.CurrentSession.DeptId.ToString()
                    , CApplication.App.CurrentSession.FyId.ToString()  });
            dsLoad.AcceptChanges();
            dtDept = dsLoad.Tables[0];
            dtSys = dsLoad.Tables[1];
            dtWBasic = dsLoad.Tables[2];

            InitContrInit();
        }
        private void InitContrInit()
        {
            blInit = true;
            StaticFunctions.BindDplComboByTable(dplPCType, dtSys, "Name", "SetValue",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "SetKey='Goods_PCType'", false);
            dplPCType.EditValue = "1";
            blInit = false;

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

        private void InitDataSet()
        {
            dtPProd = null;
            dtCProd = null;
            dtInMat = null;
            dtOutMat = null;
            dtInProd = null;
            dtOutProd = null;
            dtMoveIn = null;
            dtMoveOut = null;
            dtSumIn = null;
            dtSumOut = null;
            dtULoss = null;
            dtURece = null;
            dtUDeli = null;
            dtTstSend = null;
            dtTstRece = null;
            dtUChkLoss = null;
            dtALoss = null;

            gridVCProd.ClearColumnsFilter();
            gridVPProd.ClearColumnsFilter();
            gridVSumIn.ClearColumnsFilter();
            gridVSumOut.ClearColumnsFilter();
            gridVULoss.ClearColumnsFilter();
            gridVALoss.ClearColumnsFilter();
        }
        private void frmTst_StorageChk_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                btnChk.Enabled = false;
                InitContr();
                InitDataSet();

                if(!string.IsNullOrEmpty(strReChkId))
                {
                    dtAccountDt.EditValue = strAccount;
                    dtAccountDt.Enabled = false;
                    blInit = true;
                    dplPCType.EditValue = DrReChk["Goods_PCType"].ToString();
                    dplPCType.Enabled = false;
                    blInit = false;
                    btnChk.Visible = true;
                    btnUChk.Visible = false;
                }
                else
                {
                    SetUChk();
                }
                GetChkData();
                if (!btnChk.Visible)
                {
                    gridVInMat.OptionsView.ShowFooter = false;
                    gridVOutMat.OptionsView.ShowFooter = false;
                    gridVInProd.OptionsView.ShowFooter = false;
                    gridVOutProd.OptionsView.ShowFooter = false;
                    this.gridVMoveIn.OptionsView.ShowFooter = false;
                    gridVMoveOut.OptionsView.ShowFooter = false;

                    gcSumInfo.Visible = false;
                }
                btnChk.Enabled = true;
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

        private void SetUChk()
        {
            string[] strKey = "StrPcType,Fy_Id,Dept_Id,EUser_Id,EDept_Id,flag".Split(",".ToCharArray());
            dtUChk = this.DataRequest_By_DataTable(strSpName, strKey,
                new string[] { dplPCType.EditValue.ToString(), 
                            CApplication.App.CurrentSession.FyId.ToString(), 
                            CApplication.App.CurrentSession.DeptId.ToString(), 
                            CApplication.App.CurrentSession.UserId.ToString(),
                            CApplication.App.CurrentSession.DeptId.ToString(),"0"   
                        });
            if (dtUChk.Rows.Count > 0)
            {
                blUChk = true;
                DataRow drUChk = dtUChk.Rows[0];
                btnUChk.Text = "重置员工账&F";
                dtAccountDt.EditValue = DateTime.Parse(drUChk["AccountDt"].ToString()).ToShortDateString();
                dtAccountDt.Enabled = false;
            }
            else
            {
                blUChk = false;
                btnUChk.Text = "盘点员工账&F";
                dtAccountDt.EditValue = null;
                dtAccountDt.Enabled = true;
            }
            btnUChk.Visible = true;
            btnChk.Visible = (frmAllowOperatorList.IndexOf("btnChk=") != -1);
        }

        public void ReGetData()
        {
            string[] strKey="Form,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
            dsLoad =this.DataRequest_By_DataSet("Get_Form_Load_Table",strKey,
                new string[] { this.Name, CApplication.App.CurrentSession.UserId.ToString(), CApplication.App.CurrentSession.DeptId.ToString()
                    , CApplication.App.CurrentSession.FyId.ToString()  });
            dsLoad.AcceptChanges();
            frmTst_StorageChk_Load(null, null);
        }

        private void GetChkData()
        {
            string strDept = string.IsNullOrEmpty(strAccount) ? CApplication.App.CurrentSession.DeptId.ToString() : strReChkDept;
            string strStorageChk_Id = string.IsNullOrEmpty(strAccount) ? "-1" : strReChkId;
            string strReChk = string.IsNullOrEmpty(strAccount) ? "0" : "1";
            string strblUChk = blUChk ? "1" : "0";
            string strType = dplPCType.EditValue.ToString();
            string strFy = CApplication.App.CurrentSession.FyId.ToString();
            string strUId = CApplication.App.CurrentSession.UserId.ToString();
            string strUDId = CApplication.App.CurrentSession.DeptId.ToString();

            string[] strKey = "StrPcType,StartWT_Dt,StorageChk_Id,ReChk,blUChk,Fy_Id,Dept_Id,EUser_Id,EDept_Id,blChk,flag,Adtflag".Split(",".ToCharArray());
            DataSet dsLoad = this.DataRequest_By_DataSet(strSpName,
               strKey, new string[] {strType,
                   dtAccountDt.Text
                   ,strStorageChk_Id
                   ,strReChk
                   ,strblUChk
                    ,strFy
                    ,strDept
                    ,strUId
                    ,strUDId
                    ,btnChk.Visible?"1":"0"
                    ,"1","1"
                   });
            if (dsLoad == null)
                return;
            dsLoad.AcceptChanges();
            dtPProd = dsLoad.Tables[0];
            dtCProd = dsLoad.Tables[1];
            dtALoss = dsLoad.Tables[2];

            DataTable dtMat = dsLoad.Tables[3];
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
            DataTable dtProd = dsLoad.Tables[4];
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
            dsLoad = null;
            dsLoad = this.DataRequest_By_DataSet(strSpName,
               strKey, new string[] {strType,
                   dtAccountDt.Text
                   ,strStorageChk_Id
                   ,strReChk
                   ,strblUChk
                    ,strFy
                    ,strDept
                    ,strUId
                    ,strUDId
                    ,btnChk.Visible?"1":"0"
                    ,"1","2"
                   });
            if (dsLoad == null)
                return;
            dsLoad.AcceptChanges();
            dtUChkLoss = dsLoad.Tables[0];

            dsLoad = null;
            dsLoad = this.DataRequest_By_DataSet(strSpName,
               strKey, new string[] {strType,
                   dtAccountDt.Text
                   ,strStorageChk_Id
                   ,strReChk
                   ,strblUChk
                    ,strFy
                    ,strDept
                    ,strUId
                    ,strUDId
                    ,btnChk.Visible?"1":"0"
                    ,"1","3"
                   });
            if (dsLoad == null)
                return;
            dsLoad.AcceptChanges();
            DataTable dtMove = dsLoad.Tables[0];
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
            DataTable dtSum = dsLoad.Tables[1];
            dtSumIn = dtSum.Clone();
            dtSumOut = dtSum.Clone();
            foreach (DataRow drv in dtSum.Rows)
            {
                if (drv["SUM_FLAG"].ToString() == "1")
                {
                    dtSumIn.ImportRow(drv);
                }
                else
                {
                    dtSumOut.ImportRow(drv);
                }
            }
            dtULoss = dsLoad.Tables[2];
            DataTable dtReces = dsLoad.Tables[3];
            dtURece = dtReces.Clone();
            dtTstSend = dtReces.Clone();
            foreach (DataRow drv in dtReces.Rows)
            {
                if (drv["Transit_DeptId"].ToString() == strDept)
                {
                    dtURece.ImportRow(drv);
                }
                if (drv["Dept_Id"].ToString() == strDept)
                {
                    dtTstSend.ImportRow(drv);
                }
            }
            DataTable dtDelis = dsLoad.Tables[4];
            dtUDeli = dtDelis.Clone();
            dtTstRece = dtDelis.Clone();
            foreach (DataRow drv in dtDelis.Rows)
            {
                if (drv["Transit_DeptId"].ToString() == strDept)
                {
                    dtUDeli.ImportRow(drv);
                }
                if (drv["Dept_Id"].ToString() == strDept)
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

            this.dtUChkLoss.DefaultView.RowFilter = "Is_Powder_Root=0";
            this.gridCUChk.DataSource = this.dtUChkLoss.DefaultView;
            gridVUChk.BestFitColumns();

            this.gridCALoss.DataSource = this.dtALoss.DefaultView;
            gridVALoss.BestFitColumns();

            if (btnChk.Visible)
            {
                BoundSum();
            }
        }

        private void BoundSum()
        {
            txtC_Prod_W.Text = gridVCProd.Columns["NetWeight"].SummaryText;
            txtP_Prod_W.Text = gridVPProd.Columns["NetWeight"].SummaryText;
            txtIn_Prod_W.Text = gridVSumIn.Columns["NetWeight"].SummaryText;
            txtOut_Prod_W.Text = gridVSumOut.Columns["NetWeight"].SummaryText;
            txtRC_Prod_W.Text = (decimal.Parse(txtP_Prod_W.Text) + decimal.Parse(txtIn_Prod_W.Text) - decimal.Parse(txtOut_Prod_W.Text)).ToString();
            txtU_Loss_W.Text = gridVULoss.Columns["NetWeight"].SummaryText;
            txtA_Loss_W.Text = this.gridVALoss.Columns["NetWeight"].SummaryText;
            txtDept_Loss_W.Text = (decimal.Parse(txtRC_Prod_W.Text) - decimal.Parse(txtC_Prod_W.Text) - decimal.Parse(txtU_Loss_W.Text) - decimal.Parse(txtA_Loss_W.Text)).ToString();
        }

        private void btnChk_Click(object sender, EventArgs e)
        {
            if (dtAccountDt.Text == string.Empty)
            {
                MessageBox.Show("请选择账目日期.");
                return;
            }
            if (MessageBox.Show("确认前请检查数据是否正确,是否确认完成盘点?", "盘点确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                return;

            if (MessageBox.Show("账目日期已选择：" + dtAccountDt.Text + " ，账目日期将不能修改，是否确认盘点？", "盘点确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                return;

            btnChk.Enabled = false;

            string strField = string.Empty;
            string strValues = string.Empty;

            DataRow drNew = dtWBasic.NewRow();
            StaticFunctions.SetControlValue2DataRow(gcSumInfo, drNew, true);
            if (string.IsNullOrEmpty(strAccount))
            {
                drNew["AccountDt"] = dtAccountDt.Text;
                drNew["Goods_PCType"] = dplPCType.EditValue.ToString();
                drNew["Fy_Id"] = CApplication.App.CurrentSession.FyId.ToString();
                drNew["Dept_Id"] = CApplication.App.CurrentSession.DeptId.ToString();
            }
            dtWBasic.Rows.Add(drNew);
            if (string.IsNullOrEmpty(strAccount))
            {
                string[] strFileds = new string[] { "C_Prod_W", "P_Prod_W","In_Prod_W", "Out_Prod_W","RC_Prod_W"
                    ,"U_Loss_W","A_Loss_W", "Dept_Loss_W","AccountDt","Goods_PCType","Fy_Id","Dept_Id"};
                strValues = StaticFunctions.GetAddValues(drNew, strFileds, out strField);
            }
            else
            {
                string[] strFileds = new string[] { "C_Prod_W", "P_Prod_W","In_Prod_W", "Out_Prod_W","RC_Prod_W"
                    ,"U_Loss_W","A_Loss_W", "Dept_Loss_W"};
                strValues = StaticFunctions.GetUpdateValues(dtWBasic,drNew, strFileds, false);
            }

            string[] strKeys = "StrPcType,StartWT_Dt,StorageChk_Id,ReChk,blUChk,Dept_Id,strFields,strFieldValues,Fy_Id,EUser_Id,EDept_Id,flag".Split(",".ToCharArray());
            DataTable dtAdd = this.DataRequest_By_DataTable(strSpName,
               strKeys, new string[] {dplPCType.EditValue.ToString(),
                   dtAccountDt.Text
                   ,string.IsNullOrEmpty(strAccount) ? "-1":strReChkId
                   ,string.IsNullOrEmpty(strAccount) ? "0":"1",
                   blUChk ? "1":"0",
                   string.IsNullOrEmpty(strAccount) ? CApplication.App.CurrentSession.DeptId.ToString() : strReChkDept
                   ,strField,strValues,
                   CApplication.App.CurrentSession.FyId.ToString(),
                   CApplication.App.CurrentSession.UserId.ToString(),
                   CApplication.App.CurrentSession.DeptId.ToString(),
                   "2"
                   });
            if (dtAdd == null)
            {
                btnChk.Enabled = true;
                return;
            }
            MessageBox.Show("保存成功.");
            btnChk.Enabled = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            CApplication.App.CurrentSession.TimerId = 0;
            int k = msg.WParam.ToInt32();
            if (k == 9)//Tab
            {
                if (xtraTabControl1.SelectedTabPageIndex == xtraTabControl1.TabPages.Count - 1)
                {
                    xtraTabControl1.SelectedTabPageIndex = 0;
                }
                else
                {
                    xtraTabControl1.SelectedTabPageIndex += 1;
                }
                foreach (Control ctr in xtraTabControl1.SelectedTabPage.Controls)
                {
                    if (ctr.GetType().ToString() == "DevExpress.XtraGrid.GridControl")
                    {
                        GridViewEdit = ((ctr as DevExpress.XtraGrid.GridControl).MainView as DevExpress.XtraGrid.Views.Grid.GridView);
                        break;
                    }
                }

                return true;
            }
            else if (k == 116)//F5
            {
                ReGetData();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            foreach (Control ctr in xtraTabControl1.SelectedTabPage.Controls)
            {
                if (ctr.GetType().ToString() == "DevExpress.XtraGrid.GridControl")
                {
                    GridViewEdit = ((ctr as DevExpress.XtraGrid.GridControl).MainView as DevExpress.XtraGrid.Views.Grid.GridView);
                    break;
                }
            }
        }

        private void btnUChk_Click(object sender, EventArgs e)
        {
            if (btnUChk.Text == "盘点员工账&F")
            {
                if (dtAccountDt.Text == string.Empty)
                {
                    MessageBox.Show("请选择账目日期.");
                    return;
                }

                if (MessageBox.Show("员工账被设置为 "+dtAccountDt.Text+" ,是否确认?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                    return;

                string[] strKeys = "StrPcType,StartWT_Dt,Dept_Id,DelUChk,Fy_Id,EUser_Id,EDept_Id,flag".Split(",".ToCharArray());
                DataTable dtAdd = this.DataRequest_By_DataTable(strSpName,
                   strKeys, new string[] {dplPCType.EditValue.ToString(),
                   dtAccountDt.Text,
                   CApplication.App.CurrentSession.DeptId.ToString(),
                   "0",
                   CApplication.App.CurrentSession.FyId.ToString(),
                   CApplication.App.CurrentSession.UserId.ToString(),
                   CApplication.App.CurrentSession.DeptId.ToString(),
                   "6"
                   });
                if (dtAdd==null)
                {
                    return;
                }
                MessageBox.Show("保存成功.");
                frmTst_StorageChk_Load(null, null);
            }
            else
            {
                if (MessageBox.Show("员工账将被重置为未盘点，请确保盘员工账后员工未领交货,是否确认?", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                    return;

                string[] strKeys = "StrPcType,StartWT_Dt,Dept_Id,DelUChk,Fy_Id,EUser_Id,EDept_Id,flag".Split(",".ToCharArray());
                DataTable dtAdd = this.DataRequest_By_DataTable(strSpName,
                   strKeys, new string[] {dplPCType.EditValue.ToString(),
                   dtAccountDt.Text,
                   CApplication.App.CurrentSession.DeptId.ToString(),
                   "1",
                   CApplication.App.CurrentSession.FyId.ToString(),
                   CApplication.App.CurrentSession.UserId.ToString(),
                   CApplication.App.CurrentSession.DeptId.ToString(),
                   "6"
                   });
                if (dtAdd==null)
                {
                    return;
                }
                MessageBox.Show("保存成功.");
                frmTst_StorageChk_Load(null, null);
            }
        }

        private void dplPCType_EditValueChanged(object sender, EventArgs e)
        {
            if (blInit)
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnChk.Enabled = false;
                SetUChk();
                GetChkData();
                btnChk.Enabled = true;
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
    }
}
