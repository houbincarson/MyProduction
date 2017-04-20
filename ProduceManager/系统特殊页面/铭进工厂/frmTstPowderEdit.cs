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
    public partial class frmTstPowderEdit : frmEditorBase
    {
        private string strFocusedContrFlag = string.Empty;
        private List<string> arrContrSeqF = new List<string>(new string[] { "dplGoods_Type","txtUNumber", "txtAllWeight", "txtNetWeight"});
        private List<string> arrContrSeqT = new List<string>(new string[] { "txtUNumberD", "txtNetWeightD" });
        private DataTable dtDeli = null;
        private DataTable dtRece = null;
        private DataTable dtLoss = null;
        private DataRow drUDece = null;
        private DataRow drUDeli = null;
        private bool blInitBound = false;
        private DataSet dsLoad = null;
        private bool IsPrint = true;
        private bool blSetWeight = false;
        private DataTable dtUDeptLossType = null;

        public string strDeptNumber
        {
            get;
            set;
        }
        public DataTable dtDept
        {
            get;
            set;
        }
        public DataTable dtConst
        {
            get;
            set;
        }
        public frmTstPowderEdit()
        {
            InitializeComponent();
        }

        //电子称
        private void Txt_Enter(object sender, EventArgs e)
        {
            strFocusedContrName = (sender as Control).Name;
            FocusedControl = sender as Control;
            if (strFocusedContrFlag != "1")
            {
                strFocusedContrFlag = "1";
                ParentControl = gcRece;
                GridViewEdit = gridVRece;
                arrContrSeq = arrContrSeqF;

                gridVDeli.ClearSelection();
                gridVRece.ClearSelection();
                gridVLoss.ClearSelection();
                gridVRece.SelectRow(gridVRece.FocusedRowHandle);
            }
        }
        private void TxtT_Enter(object sender, EventArgs e)
        {
            strFocusedContrName = (sender as Control).Name;
            FocusedControl = sender as Control;

            if (strFocusedContrFlag != "2")
            {
                strFocusedContrFlag = "2";
                ParentControl = gcDeli;
                GridViewEdit = gridVDeli;
                arrContrSeq = arrContrSeqT;

                gridVRece.ClearSelection();
                gridVDeli.ClearSelection();
                gridVLoss.ClearSelection();
                gridVDeli.SelectRow(gridVDeli.FocusedRowHandle);
            }
        }
        private void TxtS_Enter(object sender, EventArgs e)
        {
            strFocusedContrName = (sender as Control).Name;
            FocusedControl = sender as Control;

            if (strFocusedContrFlag != "3")
            {
                strFocusedContrFlag = "3";
                ParentControl = gcRece;
                GridViewEdit = gridVLoss;
                arrContrSeq.Clear();

                gridVRece.ClearSelection();
                gridVDeli.ClearSelection();
                gridVLoss.ClearSelection();
                gridVLoss.SelectRow(gridVLoss.FocusedRowHandle);
            }
        }
        public override void SetText(string text)
        {
            TextEdit txt = FocusedControl as TextEdit;
            if (txt == null || txt.Properties.ReadOnly)
                return;

            double d = double.Parse(text);

            switch (strFocusedContrName)
            {
                case "txtNetWeight":
                    txtNetWeight.Text = StaticFunctions.Round(d, 2, 0.5).ToString();
                    break;
                case "txtNetWeightD":
                    txtNetWeightD.Text = StaticFunctions.Round(d, 2, 0.5).ToString();
                    break;

                case "txtUNumber":
                    if (dtAccountDt.Text != string.Empty)
                    {
                        if (!ckbChg.Checked)
                        {
                            MessageBox.Show("账目已完成盘点，不能新增.");
                            break;
                        }
                        string strMsg = "新增记录将作为 " + dtAccountDt.Text + " 的账目，是否继续？";
                        if (MessageBox.Show(strMsg, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            break;
                        }
                    }
                    txtUNumber.Text = text;
                    GetUser(txtUNumber.Text);
                    break;

                case "txtUNumberD":
                    if (dtAccountDt.Text != string.Empty)
                    {
                        if (!ckbChg.Checked)
                        {
                            MessageBox.Show("账目已完成盘点，不能新增.");
                            break;
                        }
                        string strMsg = "新增记录将作为 " + dtAccountDt.Text + " 的账目，是否继续？";
                        if (MessageBox.Show(strMsg, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            break;
                        }
                    }
                    txtUNumberD.Text = text;
                    GetUser(txtUNumberD.Text);
                    break;

                default:
                    break;
            }
        }

        private void GetReceDeli()
        {
            string[] strKey = "StartWT_Dt,EndWT_Dt,IsNotChk,EDept_Id,Fy_Id,PowderGType,flag".Split(",".ToCharArray());
            DataSet dsUReDl =this.DataRequest_By_DataSet("Tst_UserReceDeliChkPowder_Add_Edit_Del",
               strKey, new string[] { 
                    dtAccountDt.Text,dtAccountDt.Text, 
                    dtAccountDt.Text == string.Empty ? "1":"0",
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(),
                    Convert.ToString(dplGoods_Type.EditValue), "1" });
            dsUReDl.AcceptChanges();
            this.dtRece = dsUReDl.Tables[0];
            this.dtDeli = dsUReDl.Tables[1];
            this.dtLoss = dsUReDl.Tables[2];
            dtRece.Columns.Add("RationId", Type.GetType("System.Decimal"));
            dtLoss.Columns.Add("RationId", Type.GetType("System.Decimal"));
        }
        private void BoundRece()
        {
            foreach (DataRow dr in dtRece.Rows)
            {
                if (dr["UReceProd_Id_Root"].ToString() == string.Empty)
                {
                    dr["RationId"] = dr["UReceProd_Id"];
                }
                else
                {
                    dr["RationId"] = dr["UReceProd_Id_Root"];
                }
            }

            string strOldFilter = dtRece.DefaultView.RowFilter;

            dtRece.DefaultView.RowFilter = strOldFilter == string.Empty ? "UReceProd_Id_Root IS NULL" : strOldFilter+" and UReceProd_Id_Root IS NULL";
            DataTable dtParent = dtRece.Clone();
            dtParent.TableName = "Parent";
            foreach (DataRowView dr in dtRece.DefaultView)
            {
                dtParent.ImportRow(dr.Row);
            }

            DataTable dtChild = dtRece.Clone();
            dtChild.TableName = "Child";

            dtRece.DefaultView.RowFilter = "UReceProd_Id_Root IS NOT NULL";
            foreach (DataRowView dr in dtRece.DefaultView)
            {
                dtChild.ImportRow(dr.Row);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dtParent);
            ds.Tables.Add(dtChild);
            DataRelation dataRelation = new DataRelation("ChildGrid", ds.Tables["Parent"].Columns["RationId"], ds.Tables["Child"].Columns["RationId"], false);
            ds.Relations.Add(dataRelation);

            dtParent.DefaultView.Sort = "UReceProd_Id desc";
            gridCRece.DataSource = dtParent.DefaultView;
            gridVRece.BestFitColumns();

            for (int i = 0; i < gridVRece.RowCount; i++)
            {
                gridVRece.ExpandMasterRow(i);
            }

            dtRece.DefaultView.RowFilter = strOldFilter;
        }
        private void BoundLoss()
        {
            foreach (DataRow dr in dtLoss.Rows)
            {
                if (dr["UserChk_Id_Root"].ToString() == string.Empty)
                {
                    dr["RationId"] = dr["UserChk_Id"];
                }
                else
                {
                    dr["RationId"] = dr["UserChk_Id_Root"];
                }
            }

            dtLoss.DefaultView.RowFilter = "UserChk_Id_Root IS NULL";
            DataTable dtParent = dtLoss.Clone();
            dtParent.TableName = "Parent";
            foreach (DataRowView dr in dtLoss.DefaultView)
            {
                dtParent.ImportRow(dr.Row);
            }

            DataTable dtChild = dtLoss.Clone();
            dtChild.TableName = "Child";

            dtLoss.DefaultView.RowFilter = "UserChk_Id_Root IS NOT NULL";
            foreach (DataRowView dr in dtLoss.DefaultView)
            {
                dtChild.ImportRow(dr.Row);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dtParent);
            ds.Tables.Add(dtChild);
            DataRelation dataRelation = new DataRelation("ChildGrid", ds.Tables["Parent"].Columns["RationId"], ds.Tables["Child"].Columns["RationId"], false);
            ds.Relations.Add(dataRelation);

            dtParent.DefaultView.Sort = "UserChk_Id desc";
            gridCLoss.DataSource = dtParent.DefaultView;
            gridVLoss.BestFitColumns();

            for (int i = 0; i < gridVLoss.RowCount; i++)
            {
                gridVLoss.ExpandMasterRow(i);
            }
        }

        private void InitContr()
        {
            if (dsLoad != null)
                return;
            string[] strKey="Form,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
            dsLoad =this.DataRequest_By_DataSet("Get_Form_Load_Table", strKey,
                new string[] { this.Name
                    , CApplication.App.CurrentSession.UserId.ToString()
                    , CApplication.App.CurrentSession.DeptId.ToString()
                    , CApplication.App.CurrentSession.FyId.ToString() });
            dsLoad.AcceptChanges();
            dtUDeptLossType = dsLoad.Tables[1];
            dplGoods_Type.EditValueChanged -= dplGoods_Type_EditValueChanged;
            StaticFunctions.BindDplComboByTable(dplGoods_Type, dsLoad.Tables[0], "Name", "SetValue",
                new string[] { "Number", "Name" },
                new string[] { "编号", "名称" }, true, "", "SetKey='Goods_Type'", false);
            DataView dv = dplGoods_Type.Properties.DataSource as DataView;
            if (dv.Count == 1)
            {
                dplGoods_Type.EditValue = dv[0]["SetValue"].ToString();
                dplGoods_Type.Properties.ReadOnly = true;
            }
            dplGoods_Type.EditValueChanged += dplGoods_Type_EditValueChanged;

            DataRow[] drs = CApplication.App.DtAllowMenus.Select("Menus_Class='" + this.Name + "'");
            if (drs.Length >= 1 && drs[0]["Allowed_Operator"].ToString().IndexOf("pnlChgAccount=") != -1)
            {
                pnlChgAccount.Visible = true;
            }
            else
            {
                pnlChgAccount.Visible = false;
            }
        }
        private void frmTstPowderEdit_Load(object sender, EventArgs e)
        {
            InitContr();
            if (Convert.ToString(dplGoods_Type.EditValue) != string.Empty)
            {
                this.RefreshItem();
            }
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
        private void InitForm()
        {
            StaticFunctions.SetControlEmpty(gcRece, "dplGoods_Type");
            drUDece = null;
            StaticFunctions.SetControlEmpty(gcDeli);
            drUDeli = null;
            GetReceFilter(string.Empty);

            strFocusedContrFlag = "1";
            ParentControl = gcRece;
            GridViewEdit = gridVRece;
            arrContrSeq = arrContrSeqF;

            gridVDeli.ClearSelection();
            gridVRece.ClearSelection();
            gridVLoss.ClearSelection();
            gridVRece.SelectRow(gridVRece.FocusedRowHandle);
            txtUNumber.Focus();
            txtUNumber.SelectAll();
        }
        private void frmTstPowderEdit_Shown(object sender, EventArgs e)
        {
            strFocusedContrFlag = "1";
            ParentControl = gcRece;
            GridViewEdit = gridVRece;
            arrContrSeq = arrContrSeqF;

            gridVDeli.ClearSelection();
            gridVRece.ClearSelection();
            gridVLoss.ClearSelection();
            gridVRece.SelectRow(gridVRece.FocusedRowHandle);

            txtUNumber.Focus();
            txtUNumber.SelectAll();
        }

        private void GetUser(string strUNumber)
        {
            if (Convert.ToString(dplGoods_Type.EditValue) == string.Empty)
            {
                MessageBox.Show("请先选择 熔粉类型.");
                dplGoods_Type.Focus();
                dplGoods_Type.ShowPopup();
                return;
            }
            if (strUNumber == string.Empty)
                return;
            string[] strKey = "Number,Fy_Id".Split(",".ToCharArray());
            DataTable dtPm =this.DataRequest_By_DataTable("Get_Bse_User",strKey ,
                new string[] { strUNumber, CApplication.App.CurrentSession.FyId.ToString()});
            dtPm.AcceptChanges();
            if (dtPm.Rows.Count <= 0)
            {
                MessageBox.Show("不存在该工号的用户.");
                if (strFocusedContrFlag == "1")
                {
                    txtUNumber.Focus();
                    txtUNumber.SelectAll();
                }
                else
                {
                    txtUNumberD.Focus();
                    txtUNumberD.SelectAll();
                }
            }
            else
            {
                DataRow drPm = dtPm.Rows[0];
                if (dtPm.Rows[0]["IsNoPrint"].ToString() == "True")
                {
                    IsPrint = false;
                }
                else
                {
                    IsPrint = true;
                }
                if (drPm["Fy_Id"].ToString() != CApplication.App.CurrentSession.FyId.ToString())
                {
                    MessageBox.Show("本工厂不存在该工号的用户.");
                    if (strFocusedContrFlag == "1")
                    {
                        txtUNumber.Focus();
                        txtUNumber.SelectAll();
                    }
                    else
                    {
                        txtUNumberD.Focus();
                        txtUNumberD.SelectAll();
                    }

                    return;
                }
                if (strFocusedContrFlag == "1")
                {
                    txtUNumber.Text = drPm["Number"].ToString();
                    txtUName.Text = drPm["Name"].ToString();
                    txtUDept.Text = drPm["Dept_Txt"].ToString();
                    drUDece = drPm;

                    strFocusedContrName = "txtAllWeight";
                    FocusedControl = txtAllWeight;
                    txtAllWeight.Focus();
                    txtAllWeight.SelectAll();
                }
                else
                {
                    GetReceFilter(drPm["User_Id"].ToString());

                    txtUNumberD.Text = drPm["Number"].ToString();
                    txtUNameD.Text = drPm["Name"].ToString();
                    txtUDeptD.Text = drPm["Dept_Txt"].ToString();
                    drUDeli = drPm;

                    strFocusedContrName = "txtNetWeightD";
                    FocusedControl = txtNetWeightD;
                    this.txtNetWeightD.Focus();
                    txtNetWeightD.SelectAll();
                }
            }
        }
        private void GetReceFilter(string strUId)
        {
            gridVRece.ClearColumnsFilter();
            gridVDeli.ClearColumnsFilter();

            if (strUId != string.Empty)
            {
                dtRece.DefaultView.RowFilter = "UserId=" + strUId;
                dtDeli.DefaultView.RowFilter = "UserId=" + strUId;
                BoundRece();
            }
            else
            {
                dtRece.DefaultView.RowFilter = string.Empty;
                dtDeli.DefaultView.RowFilter = string.Empty;
                BoundRece();
            }
        }

        private void SaveRece()
        {
            if (drUDece == null || drUDece["User_Id"].ToString() == string.Empty)
            {
                MessageBox.Show("请输入员工.");
                txtUNumber.Focus();
                txtUNumber.SelectAll();
                return;
            }
            if (dplGoods_Type.EditValue == null || dplGoods_Type.EditValue.ToString() == string.Empty)
            {
                MessageBox.Show("请选择熔粉的粉类型.");
                dplGoods_Type.Focus();
                dplGoods_Type.ShowPopup();
                return;
            }
            double dnw = txtNetWeight.Text == string.Empty ? 0 : double.Parse(txtNetWeight.Text);
            if (dnw <= 0)
                return;

            string strField = string.Empty;
            string strValues = string.Empty;

            DataRow drNew = this.dtRece.NewRow();
            drNew["BseGTpyeId"] = dplGoods_Type.EditValue; 
            drNew["Goods_Type"] = dplGoods_Type.Text;
            double daw = txtAllWeight.Text == string.Empty ? 0 : double.Parse(txtAllWeight.Text);
            drNew["NetWeight"] = dnw - daw;
            drNew["UserId"] = drUDece["User_Id"];
            drNew["UDeptId"] = drUDece["Dept_Id"];
            DataRow[] drPowers = dtUDeptLossType.Select("Dept_Id=" + drNew["UDeptId"].ToString() + " AND BseGTpyeId=" + drNew["BseGTpyeId"].ToString());
            if (drPowers.Length != 1)
            {
                drPowers = dtUDeptLossType.Select("BseGTpyeId IS NULL AND Dept_Id=" + drNew["UDeptId"].ToString());
            } 
            if (drPowers.Length <=0)
            {
                MessageBox.Show("没有设置对应的熔粉损耗，无法熔粉.");
                return;
            }
            drNew["UDeptLossId"] = drPowers[0]["UDeptLossId"];
            drNew["ULossRepType"] = drPowers[0]["ULossRepType"];
            drNew["ProdLoss"] = drPowers[0]["ProdLoss"];
            drNew["Is_Powder"] = true;
            drNew["HostName"] = System.Net.Dns.GetHostName();
            if (dtAccountDt.Text != string.Empty)
                drNew["AccountDt"] = dtAccountDt.Text;

            string[] strFileds = new string[] { "HostName","UserId","UDeptId",
                    "BseGTpyeId","Goods_Type", "NetWeight","UDeptLossId","ULossRepType","ProdLoss", "Is_Powder","AccountDt"};
            strValues = StaticFunctions.GetAddValues(drNew, strFileds, out strField);

            string[] strKey = "StartWT_Dt,EndWT_Dt,IsNotChk,strFields,strFieldValues,UserId,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataSet dtAdd = this.DataRequest_By_DataSet("Tst_UserReceDeliChkPowder_Add_Edit_Del",
                strKey, new string[] {
                    dtAccountDt.Text,dtAccountDt.Text, 
                    dtAccountDt.Text == string.Empty ? "1":"0",
                    strField,
                    strValues,
                    drUDece["User_Id"].ToString(),
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                    "2"});
            if (dtAdd==null)
            {
                return;
            }
            DataRow drAdd = dtAdd.Tables[0].Rows[0];
            drNew["UReceProd_Id"] = drAdd["UReceProd_Id"].ToString();
            drNew["RIndex"] = drAdd["RIndex"].ToString();
            drNew["Rece_Dt"] = drAdd["Rece_Dt"].ToString();
            drNew["Dept_Id"] = CApplication.App.CurrentSession.DeptId;
            drNew["Fy_Id"] = CApplication.App.CurrentSession.FyId;
            drNew["CNumber"] = CApplication.App.CurrentSession.Number.ToString();
            drNew["CName"] = CApplication.App.CurrentSession.UserNme.ToString();
            drNew["UNumber"] = this.txtUNumber.Text;
            drNew["UName"] = txtUName.Text;

            gridVRece.ClearColumnsFilter();
            gridVRece.ClearSorting();
            dtRece.Rows.InsertAt(drNew, 0);
            dtRece.AcceptChanges();
            gridVRece.MoveFirst();
            if (IsPrint)
            {
                if (MessageBox.Show("成功保存，是否打印？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    PrintR(drNew);
                }
            }
            else
            {
                MessageBox.Show("成功保存");
            }

            StaticFunctions.SetControlEmpty(gcRece,"dplGoods_Type");
            drUDece = null;
            txtUNumber.Focus();
        }
        private void btnPowder_Click(object sender, EventArgs e)
        {
            if (drUDece == null || drUDece["User_Id"].ToString() == string.Empty)
            {
                MessageBox.Show("请输入员工.");
                txtUNumber.Focus();
                txtUNumber.SelectAll();

                return;
            }
            if (dplGoods_Type.EditValue == null || dplGoods_Type.EditValue.ToString() == string.Empty)
            {
                MessageBox.Show("请选择熔粉的粉类型.");
                dplGoods_Type.Focus();
                dplGoods_Type.ShowPopup();
                return;
            }
            if (MessageBox.Show(txtUName.Text + " 将代表熔粉，是否继续？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
            {
                return;
            }
            
            gridVRece.ClearColumnsFilter();
            if (gridVRece.RowCount == 0)
                return;

            string strField = string.Empty;
            string strValues = string.Empty;

            DataRow drNew = this.dtRece.NewRow();
            drNew["BseGTpyeId"] = dplGoods_Type.EditValue;
            drNew["Goods_Type"] = dplGoods_Type.Text;
            drNew["UserId"] = drUDece["User_Id"];
            drNew["UDeptId"] = drUDece["Dept_Id"];
            DataRow[] drPowers = dtUDeptLossType.Select("Dept_Id=" + drNew["UDeptId"].ToString() + " AND BseGTpyeId=" + drNew["BseGTpyeId"].ToString());
            if (drPowers.Length != 1)
            {
                drPowers = dtUDeptLossType.Select("BseGTpyeId IS NULL AND Dept_Id=" + drNew["UDeptId"].ToString());
            }
            if (drPowers.Length <= 0)
            {
                MessageBox.Show("没有设置对应的熔粉损耗，无法熔粉.");
                return;
            }
            drNew["UDeptLossId"] = drPowers[0]["UDeptLossId"];
            drNew["ULossRepType"] = drPowers[0]["ULossRepType"];
            drNew["ProdLoss"] = drPowers[0]["ProdLoss"];
            drNew["Is_Powder"] = true;
            drNew["Is_Powder_Root"] = true;
            drNew["HostName"] = System.Net.Dns.GetHostName();
            if (dtAccountDt.Text != string.Empty)
                drNew["AccountDt"] = dtAccountDt.Text;

            string[] strFileds = new string[] { "HostName","UserId","UDeptId",
                    "BseGTpyeId","Goods_Type", "UDeptLossId","ULossRepType","ProdLoss", "Is_Powder","Is_Powder_Root","AccountDt"};
            strValues = StaticFunctions.GetAddValues(drNew, strFileds, out strField);

            string[] strKey = "StartWT_Dt,EndWT_Dt,IsNotChk,strFields,strFieldValues,UserId,Is_Powder_Root,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataSet dtAdd = this.DataRequest_By_DataSet("Tst_UserReceDeliChkPowder_Add_Edit_Del",
                strKey, new string[] {
                    dtAccountDt.Text,dtAccountDt.Text, 
                    dtAccountDt.Text == string.Empty ? "1":"0",
                    strField,
                    strValues,
                    drUDece["User_Id"].ToString(),"1",
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                    "2"});
            if (dtAdd==null)
            {
                return;
            }
            DataRow drAdd = dtAdd.Tables[0].Rows[0];
            drNew["UReceProd_Id"] = drAdd["UReceProd_Id"].ToString();
            drNew["RIndex"] = drAdd["RIndex"].ToString();
            drNew["Rece_Dt"] = drAdd["Rece_Dt"].ToString();
            drNew["NetWeight"] = drAdd["NetWeight"];
            drNew["Dept_Id"] = CApplication.App.CurrentSession.DeptId;
            drNew["Fy_Id"] = CApplication.App.CurrentSession.FyId;
            drNew["CNumber"] = CApplication.App.CurrentSession.Number.ToString();
            drNew["CName"] = CApplication.App.CurrentSession.UserNme.ToString();
            drNew["UNumber"] = this.txtUNumber.Text;
            drNew["UName"] = txtUName.Text;

            if (MessageBox.Show("成功保存，是否打印？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                PrintR(drNew);
            }

            GetReceDeli();
            BoundRece();

            StaticFunctions.SetControlEmpty(gcRece, "dplGoods_Type");
            drUDece = null;
            txtUNumber.Focus();
        }

        private void PrintR(DataRow tp_Dr)
        {
            try
            {
                BarTender.Application btdb = new BarTender.Application();
                try
                {
                    PrintRItem(btdb, tp_Dr);
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
        private void PrintRItem(BarTender.Application btdb, DataRow dr)
        {
            string strPrintPath = Application.StartupPath + @"\打印模板" + @"\员工领货.btw";
            btdb.Formats.Open(strPrintPath, true, "");

            string strUDepTxt = string.Empty;
            DataRow[] drUs = dtDept.Select("Dept_Id=" + dr["UDeptId"].ToString());
            if (drUs.Length > 0)
            {
                strUDepTxt = drUs[0]["Name"].ToString();
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("GetGood", dr["UNumber"].ToString() + "-" + dr["UName"].ToString() + "-" + strUDepTxt);

            string strIndex = dr["RIndex"].ToString();
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Num", strIndex);

            strUDepTxt = string.Empty;
            drUs = dtDept.Select("Dept_Id=" + dr["Dept_Id"].ToString());
            if (drUs.Length > 0)
            {
                strUDepTxt = drUs[0]["Name"].ToString();
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("PutGood", dr["CNumber"].ToString() + "-" + dr["CName"].ToString() + "-" + strUDepTxt);
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Time", dr["Rece_Dt"].ToString());

            string strGood = dr["Goods_Type"].ToString() + ":";
            if (dr["NetWeight"] != DBNull.Value && decimal.Parse(dr["NetWeight"].ToString()) > 0)
            {
                strGood += "净重 " + dr["NetWeight"].ToString() + "[熔粉]";
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Good", strGood + "[领]");
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("HostName", dr["HostName"].ToString());

            object Zero = 0;
            btdb.Formats.Item(ref Zero).PrintOut(true, false);
        }


        private void SaveDeli()
        {
            if (drUDeli == null || drUDeli["User_Id"].ToString() == string.Empty)
            {
                MessageBox.Show("请输入员工.");
                txtUNumberD.Focus();
                txtUNumberD.SelectAll();
                return;
            }
            if (dplGoods_Type.EditValue == null || dplGoods_Type.EditValue.ToString() == string.Empty)
            {
                MessageBox.Show("请选择熔粉的粉类型.");
                dplGoods_Type.Focus();
                dplGoods_Type.ShowPopup();
                return;
            }
            double dnw = txtNetWeightD.Text == string.Empty ? 0 : double.Parse(txtNetWeightD.Text);
            if (dnw <= 0)
                return;

            string strField = string.Empty;
            string strValues = string.Empty;

            DataRow drNew = this.dtDeli.NewRow();
            drNew["BseGTpyeId"] = dplGoods_Type.EditValue;
            drNew["Goods_Type"] = dplGoods_Type.Text;
            drNew["DeliType"] = "1";
            drNew["NetWeight"] = dnw;
            drNew["UserId"] = drUDeli["User_Id"];
            drNew["UDeptId"] = drUDeli["Dept_Id"];
            DataRow[] drPowers = dtUDeptLossType.Select("Dept_Id=" + drNew["UDeptId"].ToString() + " AND BseGTpyeId=" + drNew["BseGTpyeId"].ToString());
            if (drPowers.Length != 1)
            {
                drPowers = dtUDeptLossType.Select("BseGTpyeId IS NULL AND Dept_Id=" + drNew["UDeptId"].ToString());
            }
            if (drPowers.Length <= 0)
            {
                MessageBox.Show("没有设置对应的熔粉损耗，无法熔粉.");
                return;
            }
            drNew["UDeptLossId"] = drPowers[0]["UDeptLossId"];
            drNew["ULossRepType"] = drPowers[0]["ULossRepType"];
            drNew["ProdLoss"] = drPowers[0]["ProdLoss"];
            drNew["Is_Powder"] = true;
            if (gridVRece.GetFocusedDataRow() != null)
                drNew["Is_Powder_Root"] = gridVRece.GetFocusedDataRow()["Is_Powder_Root"];
            drNew["HostName"] = System.Net.Dns.GetHostName();
            if (dtAccountDt.Text != string.Empty)
                drNew["AccountDt"] = dtAccountDt.Text;

            string[] strFileds = new string[] { "HostName","UserId","UDeptId",
                    "BseGTpyeId","Goods_Type","DeliType", "NetWeight","UDeptLossId","ULossRepType","ProdLoss","Is_Powder_Root", "Is_Powder","AccountDt"};
            strValues = StaticFunctions.GetAddValues(drNew, strFileds, out strField);

            string[] strKey = "StartWT_Dt,EndWT_Dt,IsNotChk,strFields,strFieldValues,UserId,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataSet dtAdd = this.DataRequest_By_DataSet("Tst_UserReceDeliChkPowder_Add_Edit_Del",
               strKey , new string[] {
                    dtAccountDt.Text,dtAccountDt.Text, 
                    dtAccountDt.Text == string.Empty ? "1":"0",
                    strField,
                    strValues,
                    drUDeli["User_Id"].ToString(),
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                    "4"});
            if (dtAdd==null)
            {
                return;
            }
            DataRow drAdd = dtAdd.Tables[0].Rows[0];
            drNew["UDeliProd_Id"] = drAdd["UDeliProd_Id"].ToString();
            drNew["DIndex"] = drAdd["DIndex"].ToString();
            drNew["Deli_Dt"] = drAdd["Deli_Dt"].ToString();
            drNew["Dept_Id"] = CApplication.App.CurrentSession.DeptId;
            drNew["Fy_Id"] = CApplication.App.CurrentSession.FyId;
            drNew["CNumber"] = CApplication.App.CurrentSession.Number.ToString();
            drNew["CName"] = CApplication.App.CurrentSession.UserNme.ToString();
            drNew["UNumber"] = this.txtUNumberD.Text;
            drNew["UName"] = txtUNameD.Text;

            gridVDeli.ClearColumnsFilter();
            gridVDeli.ClearSorting();
            dtDeli.Rows.InsertAt(drNew, 0);
            dtDeli.AcceptChanges();
            gridCDeli.DataSource = dtDeli.DefaultView;
            gridVDeli.BestFitColumns();
            gridVDeli.MoveFirst();

            if (IsPrint)
            {
                if (MessageBox.Show("成功保存，是否打印？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    PrintD(drNew);
                }
            }
            else
            {
                MessageBox.Show("成功保存");
            }

            if (MessageBox.Show("是否盘点,损耗：" + StaticFunctions.Round(double.Parse(gridVRece.Columns["NetWeight"].SummaryText) - double.Parse(gridVDeli.Columns["NetWeight"].SummaryText), 2, 0.5).ToString(), "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (ChkULoss())
                {
                    StaticFunctions.SetControlEmpty(gcDeli);
                    drUDeli = null;
                    GetReceFilter(string.Empty);
                    txtNetWeightD.Text = string.Empty;
                    txtUNumberD.Focus();
                }
            }
            else
            {
                StaticFunctions.SetControlEmpty(gcDeli);
                drUDeli = null;
                GetReceFilter(string.Empty);
                txtNetWeightD.Text = string.Empty;
                txtUNumberD.Focus();
            }
        }
        private void PrintD(DataRow tp_Dr)
        {
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
            string strPrintPath = Application.StartupPath + @"\打印模板" + @"\员工交货.btw";
            btdb.Formats.Open(strPrintPath, true, "");

            string strUDepTxt = string.Empty;
            DataRow[] drUs = dtDept.Select("Dept_Id=" + dr["UDeptId"].ToString());
            if (drUs.Length > 0)
            {
                strUDepTxt = drUs[0]["Name"].ToString();
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("GiveGood", dr["UNumber"].ToString() + "-" + dr["UName"].ToString() + "-" + strUDepTxt);
           
            string strIndex = dr["DIndex"].ToString();
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Num", strIndex);

            strUDepTxt = string.Empty;
            drUs = dtDept.Select("Dept_Id=" + dr["Dept_Id"].ToString());
            if (drUs.Length > 0)
            {
                strUDepTxt = drUs[0]["Name"].ToString();
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("RevGood", dr["CNumber"].ToString() + "-" + dr["CName"].ToString() + "-" + strUDepTxt);
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Time", dr["Deli_Dt"].ToString());

            string strGood = dr["Goods_Type"].ToString() + ":";
            if (dr["NetWeight"] != DBNull.Value && decimal.Parse(dr["NetWeight"].ToString()) > 0)
            {
                strGood += "净重 " + dr["NetWeight"].ToString() + "[熔粉]";
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Good", strGood + "[交]");
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("HostName", dr["HostName"].ToString());

            object Zero = 0;
            btdb.Formats.Item(ref Zero).PrintOut(true, false);
        }

        private bool ChkULoss()
        {
            if (drUDeli == null || drUDeli["User_Id"].ToString() == string.Empty)
            {
                MessageBox.Show("请输入员工.");
                txtUNumberD.Focus();
                txtUNumberD.SelectAll();
                return false;
            }
            if (dplGoods_Type.EditValue == null || dplGoods_Type.EditValue.ToString() == string.Empty)
            {
                MessageBox.Show("请选择熔粉的粉类型.");
                dplGoods_Type.Focus();
                dplGoods_Type.ShowPopup();
                return false;
            }
            gridVDeli.ClearColumnsFilter();
            gridVRece.ClearColumnsFilter();

            string strRPIds = string.Empty;
            string strDPIds = string.Empty;
            string strField = string.Empty;
            string strValues = string.Empty;
            string[] strFileds = null;

            for (int i = 0; i < gridVRece.RowCount; i++)
            {
                DataRow dr = gridVRece.GetDataRow(i);
                strRPIds += strRPIds == string.Empty ? dr["UReceProd_Id"].ToString() : "," + dr["UReceProd_Id"].ToString();

                if (dr["Is_Powder_Root"].ToString() == "True")
                {
                    DataRow[] drPows = dtRece.Select("UReceProd_Id_Root=" + dr["UReceProd_Id"].ToString());
                    foreach (DataRow drPow in drPows)
                    {
                        DataRow drNewDeli = this.dtDeli.NewRow();
                        drNewDeli["BseGTpyeId"] = dplGoods_Type.EditValue;
                        drNewDeli["Goods_Type"] = dplGoods_Type.Text;
                        drNewDeli["DeliType"] = "1";
                        drNewDeli["NetWeight"] = StaticFunctions.Round(double.Parse(drPow["NetWeight"].ToString()) / double.Parse(gridVRece.Columns["NetWeight"].SummaryText) * double.Parse(gridVDeli.Columns["NetWeight"].SummaryText), 2, 0.5);
                        drNewDeli["UserId"] = drPow["UserId"];
                        drNewDeli["UDeptId"] = drPow["UDeptId"];
                        DataRow[] drPowers = dtUDeptLossType.Select("Dept_Id=" + drNewDeli["UDeptId"].ToString() + " AND BseGTpyeId=" + drNewDeli["BseGTpyeId"].ToString());
                        if (drPowers.Length != 1)
                        {
                            drPowers = dtUDeptLossType.Select("BseGTpyeId IS NULL AND Dept_Id=" + drNewDeli["UDeptId"].ToString());
                        }
                        if (drPowers.Length <= 0)
                        {
                            MessageBox.Show("没有设置对应的熔粉损耗，无法熔粉.");
                            return false;
                        }
                        drNewDeli["UDeptLossId"] = drPowers[0]["UDeptLossId"];
                        drNewDeli["ULossRepType"] = drPowers[0]["ULossRepType"];
                        drNewDeli["ProdLoss"] = drPowers[0]["ProdLoss"];
                        drNewDeli["Is_Powder"] = true;
                        drNewDeli["Is_Powder_Root"] = false;
                        if (dtAccountDt.Text != string.Empty)
                            drNewDeli["AccountDt"] = dtAccountDt.Text;
                        DataRow drDeli = gridVDeli.GetFocusedDataRow();
                        if (drDeli != null)
                        {
                            drNewDeli["HostName"] = drDeli["HostName"];
                            drNewDeli["UDeliProd_Id_Root"] = drDeli["UDeliProd_Id"];
                        }

                        strFileds = new string[] { "HostName","AccountDt","UserId","UDeptId",
                            "BseGTpyeId","Goods_Type","DeliType","NetWeight","UDeptLossId","ULossRepType","ProdLoss","Is_Powder_Root","Is_Powder","UDeliProd_Id_Root"};
                        strValues = StaticFunctions.GetAddValues(drNewDeli, strFileds, out strField);

                        string[] strKey = "StartWT_Dt,EndWT_Dt,IsNotChk,strFields,strFieldValues,UserId,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                        DataSet dsAdd = this.DataRequest_By_DataSet("Tst_UserReceDeliChkPowder_Add_Edit_Del",
                            strKey, new string[] {
                            dtAccountDt.Text,dtAccountDt.Text, 
                            dtAccountDt.Text == string.Empty ? "1":"0",
                            strField,
                            strValues,
                            drPow["UserId"].ToString(),
                             CApplication.App.CurrentSession.UserId.ToString(),
                             CApplication.App.CurrentSession.DeptId.ToString(),
                             CApplication.App.CurrentSession.FyId.ToString(),
                            "4"});

                        if (dsAdd==null)
                        {
                            return false;
                        }
                    }
                }
            }

            for (int i = 0; i < gridVDeli.RowCount; i++)
            {
                DataRow dr = gridVDeli.GetDataRow(i);
                strDPIds += strDPIds == string.Empty ? dr["UDeliProd_Id"].ToString() : "," + dr["UDeliProd_Id"].ToString();
            }

            if (strRPIds == string.Empty && strDPIds == string.Empty)
                return false;

            DataRow drNew = dtLoss.NewRow();
            drNew["ChkUserId"] = drUDeli["User_Id"].ToString();
            drNew["BseGTpyeId"] = dplGoods_Type.EditValue;
            drNew["Goods_Type"] = dplGoods_Type.Text;
            drNew["D_RP_W"] = gridVRece.Columns["NetWeight"].SummaryText;
            drNew["D_DP_W"] = gridVDeli.Columns["NetWeight"].SummaryText;
            drNew["D_Qd_W"] = gridVDeli.Columns["NetWeight"].SummaryText;
            drNew["D_SP_W"] = gridVDeli.Columns["NetWeight"].SummaryText;
            drNew["D_Real_Loss"] = (double.Parse(gridVRece.Columns["NetWeight"].SummaryText) - double.Parse(gridVDeli.Columns["NetWeight"].SummaryText)).ToString();
            drNew["Is_Powder"] = true;
            if (gridVRece.GetFocusedDataRow() != null)
                drNew["Is_Powder_Root"] = gridVRece.GetFocusedDataRow()["Is_Powder_Root"];
            if (dtAccountDt.Text != string.Empty)
                drNew["AccountDt"] = dtAccountDt.Text;

            strFileds = new string[] { "ChkUserId", "BseGTpyeId","Goods_Type", "D_RP_W", "D_DP_W", "D_Qd_W", "D_SP_W", "D_Real_Loss", "Is_Powder", "Is_Powder_Root", "AccountDt" };
            strValues = StaticFunctions.GetAddValues(drNew, strFileds, out strField);
            string[] strKeys = "StartWT_Dt,EndWT_Dt,IsNotChk,strFields,strFieldValues,UserId,Is_Powder_Root,strRPIds,strDPIds,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataTable dtAdd = this.DataRequest_By_DataTable("Tst_UserReceDeliChkPowder_Add_Edit_Del",
               strKeys, new string[] { 
                    dtAccountDt.Text,dtAccountDt.Text, 
                    dtAccountDt.Text == string.Empty ? "1":"0",
                    strField,
                    strValues,drUDeli["User_Id"].ToString(),drNew["Is_Powder_Root"].ToString(),
                    strRPIds,strDPIds,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                    "8"});
            if (dtAdd==null)
            {
                return false;
            }

            foreach (DataRow drAdd in dtAdd.Rows)
            {
                dtLoss.ImportRow(drAdd);

                DataRow[] drs = dtRece.Select("UserId=" + drAdd["ChkUserId"].ToString());
                foreach (DataRow dr in drs)
                {
                    dr.Delete();
                }
                drs = dtDeli.Select("UserId=" + drAdd["ChkUserId"].ToString());
                foreach (DataRow dr in drs)
                {
                    dr.Delete();
                }
            }
            dtRece.AcceptChanges();
            dtDeli.AcceptChanges();
            dtLoss.AcceptChanges();
            BoundLoss();

            if (MessageBox.Show("成功保存，是否打印？", "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                try
                {
                    BarTender.Application btdb = new BarTender.Application();
                    try
                    {
                        DataRow[] drRoot = dtAdd.Select("Is_Powder_Root=1");
                        if (drRoot.Length == 0)
                        {
                            PrintULoss(btdb, dtAdd.Rows[0], null);
                        }
                        else
                        {
                            PrintULoss(btdb, drRoot[0], null);

                            foreach (DataRow drAdd in dtAdd.Select("Is_Powder_Root=0"))
                            {
                                PrintULoss(btdb, drAdd, drRoot[0]);
                            }
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
            strFocusedContrFlag = "2";
            ParentControl = gcDeli;
            GridViewEdit = gridVDeli;
            arrContrSeq = arrContrSeqT;

            gridVRece.ClearSelection();
            gridVDeli.ClearSelection();
            gridVLoss.ClearSelection();
            gridVDeli.SelectRow(gridVDeli.FocusedRowHandle);

            return true;
        }
        private void btnLossRept_Click(object sender, EventArgs e)
        {
            if (ChkULoss())
            {
                StaticFunctions.SetControlEmpty(gcDeli);
                drUDeli = null;
                GetReceFilter(string.Empty);
                txtUNumberD.Focus();
            }
        }

        private void PrintL(DataRow tp_Dr)
        {
            try
            {
                BarTender.Application btdb = new BarTender.Application();
                try
                {
                    PrintULoss(btdb, tp_Dr,null);
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
        private void PrintULoss(BarTender.Application btdb, DataRow dr, DataRow drRoot)
        {
            string strPrintPath = Application.StartupPath + @"\打印模板" + @"\员工损耗.btw";
            btdb.Formats.Open(strPrintPath, true, "");
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Time", dr["Chk_Dt"].ToString());

            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("UserTxt", dr["UNumber"].ToString() + "-" + dr["UName"].ToString() + "-" + dr["UDeptName"].ToString());

            string strUDepTxt = string.Empty;
            DataRow[] drUs = dtDept.Select("Dept_Id=" + dr["Dept_Id"].ToString());
            if (drUs.Length > 0)
            {
                strUDepTxt = drUs[0]["Name"].ToString();
            }
            btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("TstTxt", dr["CNumber"].ToString() + "-" + dr["CName"].ToString() + "-" + strUDepTxt);

            if (drRoot != null)
            {
                btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("SumRece", dr["D_RP_W"].ToString() + "[总领粉:" + drRoot["D_RP_W"].ToString() + "]");
                btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("SumDeli", dr["D_DP_W"].ToString() + "[总交粉:" + drRoot["D_DP_W"].ToString() + "]");
                btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("ULoss", dr["D_Real_Loss"].ToString() + "[熔粉总损耗:" + drRoot["D_Real_Loss"].ToString() + "]");
            }
            else
            {
                btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("SumRece", dr["D_RP_W"].ToString());
                btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("SumDeli", dr["D_DP_W"].ToString());
                if (dr["Is_Powder_Root"].ToString() == "True")
                {
                    btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("ULoss", dr["D_Real_Loss"].ToString() + "[总熔粉]");
                }
                else
                {
                    btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("ULoss", dr["D_Real_Loss"].ToString() + "[熔粉]");
                }
            }

            object Zero = 0;
            btdb.Formats.Item(ref Zero).PrintOut(true, false);
        }

        public override bool DeleteFocusedItem()
        {
            if (!base.DeleteFocusedItem())
            {
                return false;
            }
            DataRow dr = GridViewEdit.GetFocusedDataRow();
            if (dr["Is_Chk"].ToString() == "True")
            {
                if (!ckbChg.Checked)
                {
                    MessageBox.Show("记录已完成盘点，不能删除.");
                    return false;
                }
                else
                {
                    string strMsg1 = "将修改 " + dtAccountDt.Text + " 的账目，是否继续？";
                    if (MessageBox.Show(strMsg1, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return false;
                    }
                }
            }

            string strMsg = string.Empty;
            string strDelId = string.Empty;
            string strDelFlag = string.Empty;

            if (strFocusedContrFlag == "1")
            {
                strMsg = "将删除选中的领货记录，是否继续？";
                strDelId = dr["UReceProd_Id"].ToString();
                strDelFlag = "6";
            }
            else if (strFocusedContrFlag == "2")
            {
                strMsg = "将删除选中的交货记录，是否继续？";
                strDelId = dr["UDeliProd_Id"].ToString();
                strDelFlag = "7";
            }
            else
            {
                strMsg = "将删除选中的盘点记录，是否继续？";
                strDelId = dr["UserChk_Id"].ToString();
                strDelFlag = "11";
            }

            if (MessageBox.Show(strMsg, "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return false;
            string[] strKey = "DelId,UserChk_Id,Is_Powder_Root,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataTable dtAdd = this.DataRequest_By_DataTable("Tst_UserReceDeliChkPowder_Add_Edit_Del",
               strKey , new string[] {
                         strDelId,strDelId,dr["Is_Powder_Root"].ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         strDelFlag});

            if (dtAdd==null)
            {
                return false;
            }
            if (strFocusedContrFlag == "1")
            {
                DataRow[] drDeli = this.dtRece.Select("UReceProd_Id=" + strDelId);
                foreach (DataRow drDel in drDeli)
                {
                    drDel.Delete();
                }
                drDeli = this.dtRece.Select("UReceProd_Id_Root=" + strDelId);
                foreach (DataRow drDel in drDeli)
                {
                    drDel["UReceProd_Id_Root"] = DBNull.Value;
                    drDel["Is_Powder_Root"] = false;
                }
                dtRece.AcceptChanges();
                BoundRece();
            }
            else if (strFocusedContrFlag == "2")
            {
                DataRow[] drDeli = dtDeli.Select("UDeliProd_Id=" + strDelId);
                foreach (DataRow drDel in drDeli)
                {
                    drDel.Delete();
                }
                dtDeli.AcceptChanges();
            }
            else
            {
                frmTstPowderEdit_Load(null, null);
                InitForm();
            }
            return true;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            CApplication.App.CurrentSession.TimerId = 0;
            int k = msg.WParam.ToInt32();
            if (k == 9)//Tab
            {
                if (strFocusedContrFlag == "1")
                {
                    strFocusedContrFlag = "2";
                    ParentControl = gcDeli;
                    GridViewEdit = gridVDeli;
                    arrContrSeq = arrContrSeqT;

                    gridVRece.ClearSelection();
                    gridVDeli.ClearSelection();
                    gridVLoss.ClearSelection();
                    gridVDeli.SelectRow(gridVDeli.FocusedRowHandle);
                    txtUNumberD.Focus();
                    txtUNumberD.SelectAll();
                }
                else if (strFocusedContrFlag == "2")
                {
                    strFocusedContrFlag = "3";
                    ParentControl = gcRece;
                    GridViewEdit = gridVLoss;
                    arrContrSeq.Clear();

                    gridVRece.ClearSelection();
                    gridVDeli.ClearSelection();
                    gridVLoss.ClearSelection();
                    gridVLoss.SelectRow(gridVLoss.FocusedRowHandle);
                }
                else if (strFocusedContrFlag == "3")
                {
                    strFocusedContrFlag = "1";
                    ParentControl = gcRece;
                    GridViewEdit = gridVRece;
                    arrContrSeq = arrContrSeqF;

                    gridVDeli.ClearSelection();
                    gridVRece.ClearSelection();
                    gridVLoss.ClearSelection();
                    gridVRece.SelectRow(gridVRece.FocusedRowHandle);
                    txtUNumber.Focus();
                    txtUNumber.SelectAll();
                }
                return true;
            }
            else if (k == 13)
            {
                if (strFocusedContrName == "txtUNumber" || strFocusedContrName == "txtUNumberD")
                {
                    if (dtAccountDt.Text != string.Empty)
                    {
                        if (!ckbChg.Checked)
                        {
                            MessageBox.Show("账目已完成盘点，不能新增.");
                            return true;
                        }
                        string strMsg = "新增记录将作为 " + dtAccountDt.Text + " 的账目，是否继续？";
                        if (MessageBox.Show(strMsg, "操作确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return true;
                        }
                    }
                    GetUser((FocusedControl as BaseEdit).Text);
                    return true;
                }
                else if (strFocusedContrName == "txtNetWeight")
                {
                    txtUNumber.Focus();
                    SaveRece();
                    BoundRece();
                    return true;
                }
                else if (strFocusedContrName == "txtNetWeightD")
                {
                    txtUNumberD.Focus();
                    SaveDeli();
                    return true;
                }
            }
            else if (k == 122)//F11
            {
                if (strFocusedContrFlag == "1" && gridVRece.GetFocusedDataRow() != null)
                {
                    DataRow drRece = gridVRece.GetFocusedDataRow();
                    PrintR(drRece);
                    if (drRece["Is_Powder_Root"].ToString() == "True")
                    {
                        try
                        {
                            BarTender.Application btdb = new BarTender.Application();
                            try
                            {
                                foreach (DataRow drAdd in dtRece.Select("UDeliProd_Id_Root=" + drRece["UDeliProd_Id"].ToString()))
                                {
                                    PrintRItem(btdb, drAdd);
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
                }
                else if (strFocusedContrFlag == "2" && gridVDeli.GetFocusedDataRow() != null)
                {
                    PrintD(gridVDeli.GetFocusedDataRow());
                }
                else if (strFocusedContrFlag == "3" && gridVLoss.GetFocusedDataRow() != null)
                {
                    DataRow drLoss = gridVLoss.GetFocusedDataRow();

                    PrintL(drLoss);

                    if (drLoss["Is_Powder_Root"].ToString() == "True")
                    {
                        try
                        {
                            BarTender.Application btdb = new BarTender.Application();
                            try
                            {
                                foreach (DataRow drAdd in this.dtLoss.Select("UserChk_Id_Root=" + drLoss["UserChk_Id"].ToString()))
                                {
                                    PrintULoss(btdb, drAdd, drLoss);
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
                }
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dtAccountDt_EditValueChanged(object sender, EventArgs e)
        {
            RefreshItem();
        }
        private void txtWeight_Click(object sender, EventArgs e)
        {
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
            //if (txt.Text.IndexOf('.') == -1)
            //{
            //    blInitBound = true;
            //    txt.EditValue = decimal.Parse(txt.EditValue.ToString()) / 100;
            //    blInitBound = false;
            //}
        }

        public override void RefreshItem()
        {
            GetReceDeli();
            BoundRece();
            BoundLoss();
            gridCDeli.DataSource = dtDeli.DefaultView;
            gridVDeli.BestFitColumns();
        }
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
        private void dplGoods_Type_EditValueChanged(object sender, EventArgs e)
        {
            RefreshItem();
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!blSetWeight)
            {
                e.Handled = true;
            }
        }
    }
}