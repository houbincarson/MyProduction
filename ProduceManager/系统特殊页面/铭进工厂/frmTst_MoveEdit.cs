using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

namespace ProduceManager
{
    public partial class frmTst_MoveEdit : frmEditorBase
    {
        private DataSet dsLoad = null;
        private bool blInitBound = false;
        private DataTable dtDept = null;
        private DataTable dtConst = null;
        private bool blSetWeight = false;
        private DataRow drPOrdNumber = null;

        public frmTst_MoveEdit()
        {
            InitializeComponent();
        }

        //电子称
        private void lookUpEdit_Properties_QueryPopUp(object sender, CancelEventArgs e)
        {
            if (sender is DevExpress.XtraEditors.LookUpEdit)
            {
                DevExpress.XtraEditors.LookUpEdit dpl = sender as DevExpress.XtraEditors.LookUpEdit;
                if (!dpl.Properties.DisplayMember.Equals("Number"))
                {
                    dpl.Properties.DisplayMember = "Number";
                }
            }
            else if (sender is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit dpl = sender as DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit;
                if (!dpl.DisplayMember.Equals("Number"))
                {
                    dpl.DisplayMember = "Number";
                }
            }
        }
        private void lookUpEdit_Properties_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if (sender is DevExpress.XtraEditors.LookUpEdit)
            {
                DevExpress.XtraEditors.LookUpEdit dpl = sender as DevExpress.XtraEditors.LookUpEdit;
                if (!dpl.Properties.DisplayMember.Equals("Name"))
                {
                    dpl.Properties.DisplayMember = "Name";
                }

                DataRow dr = gridView1.GetFocusedDataRow();
                if (dr != null && Convert.ToString(dpl.Tag) != string.Empty && dr.Table.Columns.IndexOf(dpl.Tag.ToString()) != -1)
                {
                    dr[dpl.Tag.ToString()] = dpl.EditValue;
                }

                if (!blPrevFindControl)
                {
                    SetContrMoveNext(dpl.Name, false);
                }
            }
            else if (sender is DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit)
            {
                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit dpl = sender as DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit;
                if (!dpl.DisplayMember.Equals("Name"))
                {
                    dpl.DisplayMember = "Name";
                }
            }
        }
        private void ucGoods_Type_onClosePopUp(object sender, DataRow drReturn)
        {
            DataRow drInfo = GridViewEdit.GetFocusedDataRow();
            if (drInfo == null)
                return;

            ProduceManager.UcTxtPopup ucp = sender as ProduceManager.UcTxtPopup;
            drInfo["Goods_Type"] = drReturn["Name"];
            GridViewEdit.UpdateCurrentRow();
            if (!blPrevFindControl)
            {
                SetContrMoveNext(ucp.Name, false);
            }
        }
        private void Txt_Enter(object sender, EventArgs e)
        {
            strFocusedContrName = (sender as Control).Name;
            FocusedControl = sender as Control;
        }
        public override void SetText(string text)
        {
            TextEdit txt = FocusedControl as TextEdit;
            if (txt == null || txt.Properties.ReadOnly)
                return;

            switch (strFocusedContrName)
            {
                case "txtNetWeight":
                    txtNetWeight.Text = StaticFunctions.Round(double.Parse(text), 2, 0.5).ToString();
                    break;
                default:
                    break;
            }
        }

        public override void GetCurrAllItem()
        {
            string[] strKey = "StartWT_Dt,EndWT_Dt,IsNotChk,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            frmDataTable = this.DataRequest_By_DataTable("Tst_Move_Add_Edit_Del", strKey,
                new string[] { dtAccountDt.Text,dtAccountDt.Text, 
                        dtAccountDt.Text == string.Empty ? "1":"0",
                        CApplication.App.CurrentSession.UserId.ToString(),
                        CApplication.App.CurrentSession.DeptId.ToString(),
                        CApplication.App.CurrentSession.FyId.ToString()
                        ,"1" });
            foreach (DataRow dr in frmDataTable.Rows)
            {
                SetRowWarmFlag(dr);
            }
            frmDataTable.AcceptChanges();
        }

        private void SetRowWarmFlag(DataRow dr)
        {
            if (dr["State"].ToString() == "0")
            {
                dr["WarmFlag"] = "已作废";
            }
            else if (dr["State"].ToString() == "5")
            {
                dr["WarmFlag"] = "登记";
            }
            else if (dr["State"].ToString() == "10")
            {
                if (dr["To_Dept"].ToString() == CApplication.App.CurrentSession.DeptId.ToString())
                {
                    dr["WarmFlag"] = "本收发未处理";//"本收发未处理的入库";
                }
                else if (dr["From_Dept"].ToString() == CApplication.App.CurrentSession.DeptId.ToString())
                {
                    dr["WarmFlag"] = "另收发未处理";//"目标收发未处理的入库";
                }
            }
            else if (dr["State"].ToString() == "15")
            {
                if (dr["To_Dept"].ToString() == CApplication.App.CurrentSession.DeptId.ToString())
                {
                    dr["WarmFlag"] = "另收发未处理";//"源收发未处理的退库";
                }
                else if (dr["From_Dept"].ToString() == CApplication.App.CurrentSession.DeptId.ToString())
                {
                    dr["WarmFlag"] = "本收发未处理";//"本收发未处理的退库";
                }
            }
            else if (dr["State"].ToString() == "20")
            {
                dr["WarmFlag"] = "已入库";
            }
        }

        public override void InitialByParam(string Mode, string strParam, DataTable dt)
        {
            base.InitialByParam(Mode, strParam, dt);
            InitContr();

            dtAccountDt.EditValue = null;
            ckbChg.Checked = false;

            if (this.frmDataTable == null)
            {
                GetCurrAllItem();
            }

            frmDataTable.AcceptChanges();
            blInitBound = true;
            gridControl1.DataSource = frmDataTable.DefaultView;//可能引发gridView1_FocusedRowChanged
            gridView1.BestFitColumns();
            blInitBound = false;

            string strIndex = StaticFunctions.GetFrmParamValue(strParam, "FocusedIndex", null);
            if (strIndex != string.Empty)
            {
                int idex = int.Parse(strIndex);
                if (idex <= frmDataTable.DefaultView.Count)
                {
                    if (gridView1.FocusedRowHandle != idex)
                        gridView1.FocusedRowHandle = idex;
                    else
                        gridView1_FocusedRowChanged(null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, idex));
                }
                else
                    gridView1_FocusedRowChanged(null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
            }
            else
                gridView1_FocusedRowChanged(null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));

            SetWMode(Mode);
            if (frmEditorMode == "VIEW")
            {
                DataRow dr = gridView1.GetFocusedDataRow();
                if (dr != null)
                {
                    SetStatus(dr["State"].ToString(), dr);
                }
            }   
        }

        private void InitContr()
        {
            if (dsLoad != null)
                return;

            GridViewEdit = gridView1;
            ParentControl = gcWInfo;
            arrContrSeq.AddRange(new string[] { "txtPONumber","dplTo_Dept", "ucGoods_Type", "txtNetWeight", "txtAmount", "txtF_Remark", "btnWSave" });  

            dsLoad = this.GetFrmLoadDs(this.Name);
            dsLoad.AcceptChanges();
            dtConst = dsLoad.Tables[1];
            StaticFunctions.BindDplComboByTable(ucGoods_Type, dsLoad.Tables[2], "Name", "BseGTpyeId"
                , new string[] { "Number", "Name", "PY_Name" }
                , new string[] { "编号", "名称", "拼音" }
                , new string[] { "Number", "Name", "PY_Name" }
                , string.Empty, "-9999", string.Empty, new Point(300, 350), false);

            StaticFunctions.BindDplComboByTable(dplState, dtConst, "Name", "SetValue", "", "SetKey='Move_State'", true);
            repState.Items.AddRange(dplState.Properties.Items);

            dtDept = dsLoad.Tables[0];

            StaticFunctions.BindDplComboByTable(dplFrom_Dept, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "", false);

            StaticFunctions.BindDplComboByTable(dplTo_Dept, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "", false);

            StaticFunctions.BindDplComboByTable(repDept, dtDept, "Name", "Dept_Id",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "", true);
        }

        private void frmEmpEdit_Load(object sender, EventArgs e)
        {
            InitContr();

            blSetWeight = GetUserSetWeightfrmCalsses(this.Name);
        }
        private bool GetUserSetWeightfrmCalsses(string strClassName)
        {
            string[] strKey = "User_Id".Split(",".ToCharArray());
            DataTable dtPm = this.DataRequest_By_DataTable("Get_Bse_User", strKey,
                new string[] { CApplication.App.CurrentSession.UserId.ToString() });
            if (dtPm == null || dtPm.Rows.Count == 0)
                return true;

            if (dtPm.Columns.IndexOf("IsSetWeight") == -1)
                return true;

            DataRow drU = dtPm.Rows[0];
            if (drU["IsSetWeight"].ToString() == "False")
            {
                return false;
            }
            else
            {
                string strSetWeightfrmCalsses = drU["frmCalsses"].ToString();
                if (strSetWeightfrmCalsses == string.Empty)
                    return true;

                return (strSetWeightfrmCalsses + ",").IndexOf(strClassName + ",") != -1;
            }
        }

        private void SetControlReadOnly()
        {
            dplFrom_Dept.Properties.ReadOnly = true;
            dplState.Properties.ReadOnly = true;
            dtFrom_Dt.Properties.ReadOnly = true;
            txtPOrdNumber.Properties.ReadOnly = true;
            this.txtPOrdNumberRkm.Properties.ReadOnly = true;
        }

        public override void SetWMode(string strMode)
        {
            this.frmEditorMode = strMode;
            gridView1.Columns["IsSel"].OptionsColumn.ReadOnly = (frmEditorMode != "VIEW");
            switch (strMode)
            {
                case "VIEW":
                    btnWEdit.Enabled = true;
                    btnWCancel.Enabled = false;
                    btnWAdd.Enabled = true;
                    btnWSave.Enabled = false;
                    btnSetOut.Enabled = true;
                    StaticFunctions.SetControlEnable(gcWInfo, false);
                    dtAccountDt.Properties.ReadOnly = false;
                    this.ckbChg.Properties.ReadOnly = false;
                    break;

                case "ADD":
                    DataRow drNew = this.frmDataTable.NewRow();
                    drNew["State"] = "5";
                    drNew["From_Dept"] = CApplication.App.CurrentSession.DeptId;
                    drNew["From_Dt"] = DateTime.Now.ToString();
                    if (dtAccountDt.Text != string.Empty)
                    {
                        drNew["AccountDt"] = dtAccountDt.Text;
                    }

                    DataRow dr = gridView1.GetFocusedDataRow();
                    if (dr != null)
                    {
                        drNew["Move_Id"] = decimal.Parse(frmDataTable.DefaultView[0]["Move_Id"].ToString()) + 1;
                    }
                    blInitBound = true;
                    frmDataTable.Rows.InsertAt(drNew, 0);//可能引发gridView1_FocusedRowChanged
                    gridView1.MoveFirst();
                    drNew["BseGTpyeId"] = DBNull.Value;
                    blInitBound = false;
                    gridView1_FocusedRowChanged(null, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));

                    btnWSave.Enabled = true;
                    btnWCancel.Enabled = true;
                    btnWEdit.Enabled = false;
                    btnWAdd.Enabled = false;
                    btnSetOut.Enabled = false;
                    btnIn.Enabled = false;
                    btnPrint.Enabled = false;
                    btnPrintT.Enabled = false;
                    btnRet.Enabled = false;
                    btnCancelIn.Enabled = false;
                    StaticFunctions.SetControlEnable(gcWInfo, true);
                    SetControlReadOnly();

                    this.txtPONumber.Focus();
                    break;

                case "EDIT":
                    btnWSave.Enabled = true;
                    btnWCancel.Enabled = true;
                    btnWEdit.Enabled = false;
                    btnWAdd.Enabled = false;
                    btnSetOut.Enabled = false;
                    btnIn.Enabled = false;
                    btnPrint.Enabled = false;
                    btnPrintT.Enabled = false;
                    btnRet.Enabled = false;
                    btnCancelIn.Enabled = false;
                    StaticFunctions.SetControlEnable(gcWInfo, true);
                    SetControlReadOnly();
                    dplTo_Dept.Focus();
                    dplTo_Dept.ShowPopup();
                    break;

                default:
                    break;
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle <= -1 || blInitBound)
                return;

            DataRow drP = gridView1.GetDataRow(e.PrevFocusedRowHandle);
            if (drP != null && drP.RowState != DataRowState.Unchanged)
            {
                blInitBound = true;
                drP.RejectChanges();//引发gridView1_FocusedRowChanged
                blInitBound = false;
            }

            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr == null)
                return;

            if (dr.RowState != DataRowState.Added && frmEditorMode != "VIEW")
            {
                SetWMode("VIEW");
            }
            DataView dv = gridControl1.DataSource as DataView;
            StaticFunctions.SetControlBindings(gcWInfo, dv, dr);

            if (frmEditorMode == "VIEW")
                SetStatus(dr["State"].ToString(), dr);
        }

        private void btnWAdd_Click(object sender, EventArgs e)
        {
            if (dtAccountDt.Text != string.Empty)
            {
                if (!ckbChg.Checked)
                {
                    MessageBox.Show("账目已完成盘点，不能新增.");
                    return;
                }
                string strMsg = "新增记录将作为 " + dtAccountDt.Text + " 的账目，是否继续？";
                if (MessageBox.Show(strMsg, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }
            gridView1.ClearColumnsFilter();
            gridView1.ClearSorting();
                    
            SetWMode("ADD");
        }

        private void btnWEdit_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr == null)
                return;

            if (dr["From_Dept"].ToString() != CApplication.App.CurrentSession.DeptId.ToString())
            {
                MessageBox.Show("操作员工不属于移出收发部门.");
                return;
            }

            SetWMode("EDIT");
        }

        private void btnWCancel_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr == null)
            {
                return;
            }
            blInitBound = true;
            dr.RejectChanges();//引发gridView1_FocusedRowChanged
            blInitBound = false;
            SetWMode("VIEW");

            dr = gridView1.GetFocusedDataRow();
            StaticFunctions.SetControlBindings(gcWInfo, this.frmDataTable.DefaultView, dr);
            if (dr != null)
            {
                SetStatus(dr["State"].ToString(), dr);
            }
        }
        private bool CheckWSave(DataRow dr)
        {
            if (dr["To_Dept"].ToString() == string.Empty)
            {
                MessageBox.Show("请选择目标收发.");
                this.dplTo_Dept.Focus();
                this.dplTo_Dept.ShowPopup();
                return false;
            }
            if (dr["From_Dept"].ToString() == dr["To_Dept"].ToString())
            {
                MessageBox.Show("请选择正确的目标收发.");
                this.dplTo_Dept.Focus();
                this.dplTo_Dept.ShowPopup();
                return false;
            }
            if (dr["Goods_Type"].ToString() == string.Empty)
            {
                MessageBox.Show("请选择货物类型.");
                this.ucGoods_Type.Focus();
                this.ucGoods_Type.ShowPopup();
                return false;
            }
            if (dr["NetWeight"] != DBNull.Value && decimal.Parse(dr["NetWeight"].ToString()) <= 0)
            {
                MessageBox.Show("请输入移出净重.");
                this.txtNetWeight.Focus();
                this.txtNetWeight.SelectAll();
                return false;
            }
            return true;
        }

        private void txtPONumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r' || txtPONumber.Properties.ReadOnly ||
                txtPONumber.Text == string.Empty)
                return;

            string[] strKey = "Number,flag".Split(",".ToCharArray());
            DataTable dtPm = this.DataRequest_By_DataTable("Tst_Move_Add_Edit_Del", strKey,
                new string[] { txtPONumber.Text.Trim(), "101" });
            if (dtPm == null)
            {
                drPOrdNumber = null;
                txtPOrdNumberRkm.Text = string.Empty;
                return;
            }
            if (dtPm.Rows.Count <= 0)
            {
                MessageBox.Show("不存在对应的加工单信息.");
                txtPONumber.Focus();
                txtPONumber.SelectAll();
            }
            else
            {
                drPOrdNumber = dtPm.Rows[0];
                txtPONumber.Text = drPOrdNumber["POrdNumber"].ToString();
                txtPOrdNumberRkm.Text = drPOrdNumber["POrdNumberRkm"].ToString();

                dplTo_Dept.Focus();
                dplTo_Dept.ShowPopup();
            }
        }
        private void btnWSave_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr == null)
                return;

            if (!CheckWSave(dr))
                return;

            string strField = string.Empty;
            string strValues = string.Empty;
            btnWSave.Enabled = false;
            if (drPOrdNumber != null)
            {
                dr["ProdOrdId"] = drPOrdNumber["ProdOrdId"];
                dr["POrdNumber"] = drPOrdNumber["POrdNumber"];
                dr["OrdRemark"] = drPOrdNumber["OrdRemark"];
                dr["IndetOrdNum"] = drPOrdNumber["IndetOrdNum"];
                dr["CustSignet"] = drPOrdNumber["CustSignet"];
            }

            if (this.frmEditorMode == "ADD")
            {
                dr["F_HostName"] = System.Net.Dns.GetHostName();
                string[] strFileds = new string[] { "F_HostName", "BseGTpyeId", "Goods_Type", "NetWeight", "Amount", "From_Dept", "To_Dept", "F_Remark", "State", "AccountDt", "ProdOrdId", "POrdNumber" };
                strValues = StaticFunctions.GetAddValues(dr, strFileds, out strField);
                string[] strKey = "strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                DataSet dtAdd = this.DataRequest_By_DataSet("Tst_Move_Add_Edit_Del",
                    strKey, new string[] { 
                    strField,
                    strValues,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                    "2"});
                if (dtAdd ==null)
                {
                    btnWSave.Enabled = true;
                    return;
                }
                DataRow drNew = dtAdd.Tables[0].Rows[0];
                dr["Move_Id"] = drNew["Move_Id"].ToString();
                dr["WarmFlag"] = "登记";
                dr["CNumber"] = CApplication.App.CurrentSession.Number.ToString();
                dr["CName"] = CApplication.App.CurrentSession.UserNme.ToString();
            }
            else
            {
                string[] strFileds = new string[] { "BseGTpyeId", "Goods_Type", "NetWeight", "Amount", "To_Dept", "F_Remark", "ProdOrdId", "POrdNumber" };
                strValues = StaticFunctions.GetUpdateValues(frmDataTable, dr, strFileds);
                if (strValues != string.Empty)
                {
                    if (strValues.IndexOf("NetWeight") != -1)
                    {
                        strValues += ",F_HostName='" + System.Net.Dns.GetHostName() + "'";
                    }
                    string[] strKey = "strEditSql,Move_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                    DataTable dtAdd = this.DataRequest_By_DataTable("Tst_Move_Add_Edit_Del",
                        strKey, new string[] { strValues,
                         dr["Move_Id"].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "3"});
                    if (dtAdd==null)
                    {
                        btnWSave.Enabled = true;
                        return;
                    }
                }
            }
            dr.AcceptChanges();
            SetWMode("VIEW");
            SetStatus(dr["State"].ToString(), dr);
        }


        private void btnSetOut_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr == null)
                return;

            if (dr["From_Dept"].ToString() != CApplication.App.CurrentSession.DeptId.ToString())
            {
                MessageBox.Show("操作员工不属于移出收发部门.");
                return;
            }

            if (MessageBox.Show("是否确定出库，出库后将不能修改移库记录.", "出库确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                return;

            string[] strKey = "Move_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataTable dtAdd = this.DataRequest_By_DataTable("Tst_Move_Add_Edit_Del",
                strKey, new string[] { 
                         dr["Move_Id"].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),"6"});
            if (dtAdd==null)
            {
                return;
            }
            DataRow drNew = dtAdd.Rows[0];
            dr["From_Dt"] = drNew["From_Dt"];
            string state = "10";
            dr["State"] = "10";
            dr["WarmFlag"] = "另收发未处理";
            dr.AcceptChanges();
            SetStatus(state,dr);

            if (MessageBox.Show("成功出库，是否打印？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                btnPrint_Click(null, null);
            }
        }

        private void btnRet_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr == null)
                return;

            if (MessageBox.Show("是否确定拒收，拒收后移出收发可以修改数据后再出库.", "拒收确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                return;

            dr["State"] = "15";

            string strField = string.Empty;
            string strValues = string.Empty;
            string[] strFileds = new string[] { "State" };
            strValues = StaticFunctions.GetUpdateValues(frmDataTable, dr, strFileds);
            if (strValues != string.Empty)
            {
                string[] strKey = "strEditSql,Move_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                DataTable dtAdd = this.DataRequest_By_DataTable("Tst_Move_Add_Edit_Del",
                    strKey, new string[] { strValues,
                         dr["Move_Id"].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),"7"});

                if (dtAdd==null)
                {
                    return;
                }
            }
            dr["To_Dt"] = DBNull.Value;
            dr["WarmFlag"] = "另收发未处理";
            dr.AcceptChanges();
            SetWMode("VIEW");
            SetStatus("15",dr);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr == null)
                return;

            if (MessageBox.Show("是否确定入库，入库后将不能修改入库数据.", "入库确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                return;

            dr["State"] = "20";
            dr["T_HostName"] = System.Net.Dns.GetHostName();

            string strField = string.Empty;
            string strValues = string.Empty;
            string[] strFileds = new string[] { "T_HostName", "State" };
            strValues = StaticFunctions.GetUpdateValues(frmDataTable, dr, strFileds);
            if (strValues != string.Empty)
            {
                string[] strKey = "strEditSql,Move_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                DataTable dtAdd = this.DataRequest_By_DataTable("Tst_Move_Add_Edit_Del",
                   strKey , new string[] { strValues,
                         dr["Move_Id"].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),"8"});

                if (dtAdd==null)
                {
                    return;
                }
                DataRow drNew = dtAdd.Rows[0];
                dr["To_Dt"] = drNew["To_Dt"];
            }
            dr["WarmFlag"] = "已入库";
            dr["ANumber"] = CApplication.App.CurrentSession.Number.ToString();
            dr["AName"] = CApplication.App.CurrentSession.UserNme.ToString();
            dr["NetNotEqual"] = "0";
            dr.AcceptChanges();
            SetWMode("VIEW");
            SetStatus("20",dr);

            if (MessageBox.Show("成功入库，是否打印？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                btnPrintT_Click(null, null);
            }
        }

        private void SetStatus(string state,DataRow dr)
        {
            switch (state)
            {
                case "0":
                    StaticFunctions.SetBtnEnabled(new Component[] { btnWAdd }, true);
                    StaticFunctions.SetBtnEnabled(new Component[] { btnWEdit, btnWSave, btnWCancel, btnSetOut, btnPrint, btnIn, btnRet, btnCancelIn, btnPrintT }, false);
                    break;
                case "5":
                    if (dr["From_Dept"].ToString() == CApplication.App.CurrentSession.DeptId.ToString())
                    {
                        StaticFunctions.SetBtnEnabled(new Component[] { btnWAdd, btnWEdit, btnSetOut }, true);
                    }
                    else
                    {
                        StaticFunctions.SetBtnEnabled(new Component[] { btnWAdd }, true);
                        StaticFunctions.SetBtnEnabled(new Component[] { btnWEdit, btnSetOut }, false);
                    }
                    StaticFunctions.SetBtnEnabled(new Component[] { btnWSave, btnWCancel, btnPrint, btnIn, btnRet, btnCancelIn, btnPrintT }, false);
                    break;
                case "10":
                    StaticFunctions.SetBtnEnabled(new Component[] { btnWAdd, btnPrint }, true);
                    if (dr["To_Dept"].ToString() == CApplication.App.CurrentSession.DeptId.ToString())
                    {
                        StaticFunctions.SetBtnEnabled(new Component[] { btnIn, btnRet }, true);
                    }
                    else
                    {
                        StaticFunctions.SetBtnEnabled(new Component[] { btnIn, btnRet }, false);
                    }
                    StaticFunctions.SetBtnEnabled(new Component[] { btnWEdit, btnWSave, btnWCancel, btnSetOut, btnCancelIn, btnPrintT }, false);
                    break;
                case "15":
                    if (dr["From_Dept"].ToString() == CApplication.App.CurrentSession.DeptId.ToString())
                    {
                        StaticFunctions.SetBtnEnabled(new Component[] { btnWAdd, btnWEdit, btnSetOut }, true);
                    }
                    else
                    {
                        StaticFunctions.SetBtnEnabled(new Component[] { btnWAdd }, true);
                        StaticFunctions.SetBtnEnabled(new Component[] { btnWEdit, btnSetOut }, false);
                    }
                    StaticFunctions.SetBtnEnabled(new Component[] { btnWSave, btnWCancel, btnPrint, btnIn, btnRet, btnCancelIn, btnPrintT }, false);
                    break;
                case "20":
                    StaticFunctions.SetBtnEnabled(new Component[] { btnWAdd, btnPrint, btnPrintT }, true);
                    if (dr["To_Dept"].ToString() == CApplication.App.CurrentSession.DeptId.ToString())
                    {
                        StaticFunctions.SetBtnEnabled(new Component[] { btnCancelIn }, true);
                    }
                    else
                    {
                        StaticFunctions.SetBtnEnabled(new Component[] { btnCancelIn }, false);
                    }
                    StaticFunctions.SetBtnEnabled(new Component[] { btnWEdit, btnWSave, btnWCancel, btnSetOut, btnIn, btnRet }, false);
                    break;
                default:
                    break;
            }
        }


        private void txtWeight_Click(object sender, EventArgs e)
        {
            if (frmEditorMode == "VIEW")
                return;

            DevExpress.XtraEditors.TextEdit txt = sender as DevExpress.XtraEditors.TextEdit;
            txt.SelectAll();
        }

        private void txt_EditValueChanged(object sender, EventArgs e)
        {
            if (blInitBound)
                return;

            DevExpress.XtraEditors.TextEdit txt = sender as DevExpress.XtraEditors.TextEdit;
            if (txt.Text == string.Empty)
            {
                blInitBound = true;
                txt.Text = "0";
                blInitBound = false;
            }
        }

        private void btnCancelIn_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr == null)
                return;

            if (dr["To_Dept"].ToString() != CApplication.App.CurrentSession.DeptId.ToString())
            {
                MessageBox.Show("操作员工不属于目标收发部门.");
                return;
            }
            if (dr["Is_Chk"].ToString() == "True")
            {
                if (!ckbChg.Checked)
                {
                    MessageBox.Show("记录已完成盘点，撤销失败.");
                    return;
                }
                else
                {
                    string strMsg = "将修改 " + dtAccountDt.Text + " 的账目，是否继续？";
                    if (MessageBox.Show(strMsg, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            if (MessageBox.Show("是否确定撤销入库，撤销后记录将变为“出库”状态.", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                return;

            string[] strKey = "Move_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataTable dtAdd = this.DataRequest_By_DataTable("Tst_Move_Add_Edit_Del",
               strKey , new string[] { 
                         dr["Move_Id"].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),"9"});

            if (dtAdd==null)
            {
                return;
            }
            MessageBox.Show("操作完成.");
            string state = "10";
            dr["State"] = "10";
            dplState.EditValue = "10";
            dr["WarmFlag"] = "本收发未处理";
            dr["NetNotEqual"] = "0";
            dr["To_Dt"] = DBNull.Value;
            dr.AcceptChanges();
            SetStatus(state,dr);
        }


        public override void RefreshItem()
        {
            base.RefreshItem();

            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr != null)
            {
                SetStatus(dr["State"].ToString(),dr);
            }
        }

        public override bool DeleteFocusedItem()
        {
            DataRow dr = GridViewEdit.GetFocusedDataRow();
            string strState = dr["State"].ToString();

            if (strState == "0")
            {
                DataRow[] drDel = frmDataTable.Select("Move_Id=" + dr["Move_Id"].ToString());
                if (drDel.Length > 0)
                {
                    drDel[0].Delete();
                    frmDataTable.AcceptChanges();
                }
                return false;
            }

            if (strState == "10" || strState == "20")
            {
                MessageBox.Show("记录在当前状态下不能删除，只能退回移出收发后删除.");
                return false;
            }
            if (dr["Is_Chk"].ToString() == "True")
            {
                if (!ckbChg.Checked)
                {
                    MessageBox.Show("记录已完成盘点，不能删除.");
                    return false;
                }
                else
                {
                    string strMsg = "将修改 " + dtAccountDt.Text + " 的账目，是否继续？";
                    if (MessageBox.Show(strMsg, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return false;
                    }
                }
            }

            if (MessageBox.Show("是否确认删除,出库净重：" + dr["NetWeight"].ToString(), "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return false;
            string[] strKey = "Move_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataTable dtAdd = this.DataRequest_By_DataTable("Tst_Move_Add_Edit_Del",
                strKey, new string[] { 
                         dr["Move_Id"].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),"5"});

            if (dtAdd==null)
            {
                return false;
            }

            DataRow[] drDels = frmDataTable.Select("Move_Id=" + dr["Move_Id"].ToString());
            if (drDels.Length > 0)
            {
                drDels[0].Delete();
                frmDataTable.AcceptChanges();
            }
            return true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            CApplication.App.CurrentSession.TimerId = 0;
            int k = msg.WParam.ToInt32();
            if (k == 107) //小键盘+
            {
                if (keyData.ToString().ToUpper().IndexOf("ALT") != -1)
                {
                    if (btnWSave.Enabled)
                    {
                        btnWSave.Select();
                        btnWSave_Click(null, null);
                        return true;
                    }
                }
            }
            else if (k == 96)//小键盘0
            {
                if (keyData.ToString().ToUpper().IndexOf("ALT") != -1)
                {
                    if (btnWAdd.Enabled)
                    {
                        btnWAdd_Click(null, null);
                        return true;
                    }
                }
            }
            else if (k == 97)//小键盘1
            {
                if (keyData.ToString().ToUpper().IndexOf("ALT") != -1)
                {
                    if (btnWEdit.Enabled)
                    {
                        btnWEdit_Click(null, null);
                        return true;
                    }
                }
            }
            else if (k == 99)//小键盘3
            {
                if (keyData.ToString().ToUpper().IndexOf("ALT") != -1)
                {
                    if (btnSetOut.Enabled)
                    {
                        btnSetOut_Click(null, null);
                        return true;
                    }
                }
            }
            else if (k == 101)//小键盘5
            {
                if (keyData.ToString().ToUpper().IndexOf("ALT") != -1)
                {
                    if (btnIn.Enabled)
                    {
                        btnIn.Select();
                        btnIn_Click(null, null);
                        return true;
                    }
                }
            }
            else if (k == 102)//小键盘6
            {
                if (keyData.ToString().ToUpper().IndexOf("ALT") != -1)
                {
                    if (btnRet.Enabled)
                    {
                        btnRet.Select();
                        btnRet_Click(null, null);
                        return true;
                    }
                }
            }
            else if (k == 122)//F11
            {
                double dW = 0;
                string strIds = string.Empty;
                DataRow[] drs = frmDataTable.Select("IsSel=1 AND State=10 and To_Dept=" + CApplication.App.CurrentSession.DeptId.ToString());
                if (drs.Length <= 0)
                {
                    MessageBox.Show("没有选中或没有要入库的记录或操作部门不对.");
                    return base.ProcessCmdKey(ref msg, keyData);
                }

                foreach (DataRow dr in drs)
                {
                    dW += StaticFunctions.Round(double.Parse(dr["NetWeight"].ToString()), 2, 0.5);
                    strIds += strIds == string.Empty ? dr["Move_Id"].ToString() : "," + dr["Move_Id"].ToString();
                }
                if (MessageBox.Show("所有入库：" + drs.Length.ToString() + " 笔 共" + dW.ToString() + " 克，是否确认入库？", "入库确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                else
                {
                    string[] strKey = "Move_Ids,EUser_Id,EDept_Id,Fy_Id,T_HostName,flag".Split(",".ToCharArray());
                    DataTable dtAdd =this.DataRequest_By_DataTable("Tst_Move_Add_Edit_Del",
                        strKey, new string[] { 
                         strIds,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         System.Net.Dns.GetHostName(),
                         "10"});
                    if (dtAdd==null)
                    {
                        return true;
                    }
                    foreach (DataRow dr in drs)
                    {
                        dr["State"] = "20";
                        dr["T_HostName"] = System.Net.Dns.GetHostName();
                        dr["To_Dt"] = DateTime.Now;
                        dr["WarmFlag"] = "已入库";
                    }
                    frmDataTable.AcceptChanges();
                    DataRow tp_Dr = this.gridView1.GetFocusedDataRow();
                    SetStatus("20", tp_Dr);
                    if (MessageBox.Show("操作完成，是否打印？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                    {
                        return true;
                    }
                    try
                    {
                        BarTender.Application btdb = new BarTender.Application();
                        try
                        {
                            foreach (DataRow dr in drs)
                            {
                                PrintDItem(btdb, dr);
                            }
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show("打印出错：" + err.Message);
                        }
                        finally
                        {
                            btdb.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("初始化打印出错，可能未安装BarTender.");
                    }
                }
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataRow tp_Dr = this.gridView1.GetFocusedDataRow();
            if (tp_Dr == null)
                return;

            try
            {
                BarTender.Application btdb = new BarTender.Application();
                try
                {
                    PrintDItem(btdb, tp_Dr);
                }
                catch (Exception err)
                {
                    MessageBox.Show("打印出错：" + err.Message);
                }
                finally
                {
                    btdb.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("初始化打印出错，可能未安装BarTender.");
            }
        }

        private void PrintDItem(BarTender.Application btdb, DataRow dr)
        {
            string strPrintPath = Application.StartupPath + @"\打印模板" + @"\移出库.btw";
            btdb.Formats.Open(strPrintPath, true, "");
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Time", dr["From_Dt"].ToString());

            string strUDepTxt = string.Empty;
            DataRow[] drUs = dtDept.Select("Dept_Id=" + dr["From_Dept"].ToString());
            if (drUs.Length > 0)
            {
                strUDepTxt = drUs[0]["Name"].ToString();
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("FDept", strUDepTxt + "-" + dr["CNumber"].ToString() + "-" + dr["CName"].ToString());

            drUs = dtDept.Select("Dept_Id=" + dr["To_Dept"].ToString());
            if (drUs.Length > 0)
            {
                strUDepTxt = drUs[0]["Name"].ToString();
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("TDept", strUDepTxt);

            string strGood = dr["Goods_Type"].ToString() + ":" + dr["NetWeight"].ToString();
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Goods", strGood);
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Amount", dr["Amount"].ToString());
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("HostName", dr["F_HostName"].ToString());
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("POrdNumber", dr["POrdNumber"].ToString());
            string strRmk = dr["F_Remark"].ToString();
            if (dr["OrdNumber"].ToString() != string.Empty)
            {
                strRmk += "[业务单号:" + dr["OrdNumber"].ToString() + "]";
            }
            if (dr["CustSignet"].ToString() != string.Empty)
            {
                strRmk += "[字印:" + dr["CustSignet"].ToString() + "]";
            }
            if (dr["IndetOrdNum"].ToString() != string.Empty)
            {
                strRmk += "[来源单号:" + dr["IndetOrdNum"].ToString() + "]";
            }
            if (dr["OrdRemark"].ToString() != string.Empty)
            {
                strRmk += "[订单说明:" + dr["OrdRemark"].ToString() + "]";
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Remark", strRmk);

            object Zero = 0;
            btdb.Formats.Item(ref Zero).PrintOut(true, false);
        }

        private void btnPrintT_Click(object sender, EventArgs e)
        {
            DataRow tp_Dr = this.gridView1.GetFocusedDataRow();
            if (tp_Dr == null)
                return;

            try
            {
                BarTender.Application btdb = new BarTender.Application();
                try
                {
                    PrintTItem(btdb, tp_Dr);
                }
                catch (Exception err)
                {
                    MessageBox.Show("打印出错：" + err.Message);
                }
                finally
                {
                    btdb.Quit(BarTender.BtSaveOptions.btDoNotSaveChanges);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("初始化打印出错，可能未安装BarTender.");
            }
        }

        private void PrintTItem(BarTender.Application btdb, DataRow dr)
        {
            string strPrintPath = Application.StartupPath + @"\打印模板" + @"\移入库.btw";
            btdb.Formats.Open(strPrintPath, true, "");
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Time", dr["To_Dt"].ToString());

            string strUDepTxt = string.Empty;
            DataRow[] drUs = dtDept.Select("Dept_Id=" + dr["From_Dept"].ToString());
            if (drUs.Length > 0)
            {
                strUDepTxt = drUs[0]["Name"].ToString();
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("FDept", strUDepTxt + "-" + dr["CNumber"].ToString() + "-" + dr["CName"].ToString());

            drUs = dtDept.Select("Dept_Id=" + dr["To_Dept"].ToString());
            if (drUs.Length > 0)
            {
                strUDepTxt = drUs[0]["Name"].ToString();
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("TDept", strUDepTxt + "-" + dr["ANumber"].ToString() + "-" + dr["AName"].ToString());

            string strGood = dr["Goods_Type"].ToString() + ":" + dr["NetWeight"].ToString();
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Goods", strGood);
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Amount", dr["Amount"].ToString());
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("HostName", dr["T_HostName"].ToString());
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("POrdNumber", dr["POrdNumber"].ToString());

            string strRmk = dr["F_Remark"].ToString();
            if (dr["OrdNumber"].ToString() != string.Empty)
            {
                strRmk += "[业务单号:" + dr["OrdNumber"].ToString() + "]";
            }
            if (dr["CustSignet"].ToString() != string.Empty)
            {
                strRmk += "[字印:" + dr["CustSignet"].ToString() + "]";
            }
            if (dr["IndetOrdNum"].ToString() != string.Empty)
            {
                strRmk += "[来源单号:" + dr["IndetOrdNum"].ToString() + "]";
            }
            if (dr["OrdRemark"].ToString() != string.Empty)
            {
                strRmk += "[订单说明:" + dr["OrdRemark"].ToString() + "]";
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Remark", strRmk);

            object Zero = 0;
            btdb.Formats.Item(ref Zero).PrintOut(true, false);
        }

        private void dtAccountDt_EditValueChanged(object sender, EventArgs e)
        {
            RefreshItem();
        }

        private void repSel_CheckedChanged(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr != null)
            {
                dr["IsSel"] = repSel.ValueChecked;
                dr.AcceptChanges();
            }
        }

        private void txtNetWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!blSetWeight)
            {
                e.Handled = true;
            }
        }
    }
}