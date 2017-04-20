using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProduceManager
{
    public partial class frmProdSaleMasterEdit : frmEditorBase
    {
        #region 变量
        private string strkeyId = "-1";
        private string strDtlId = "-1";
        private int ordstate = 1;
        private DataTable _orddt = null;
        private DataSet _SaleUser = null;
        private DataSet _TicketUser = null;
        
        private string SpName = "Sys_ProdSale_Add_Edit_Del";
        private decimal BarWgt = 0;
        private int SaleRound = 0;
        private bool ElecState = true;
        private decimal SaleFeeQty = 0;
        private decimal SaleFeeWgt = 0;
        private int _SaleType = 1;
        private decimal _tmpexfee = 0;
        private decimal _lowgoldprice = 0;
        public frmProdSaleMasterEdit()
        {
            InitializeComponent();
            if (_orddt == null)
            {
                DataSet ds = GetOrdSetInfo();
                _orddt = ds.Tables[0];
            }
            BarWgt = decimal.Parse(_orddt.Rows[0]["BarWgtDiff"].ToString());
            SaleRound = int.Parse(_orddt.Rows[0]["SaleDplace"].ToString());
            if (_orddt.Rows[0]["ElectState"].ToString().Equals("1"))
            {
                ElecState = true;
            }
            else
            {
                ElecState = false;
            }
            if (ElecState )
            {
                lbmsg.Text = "本店已经启用电子称称重，请按电子称上右边的发送键或打印键";
            }
            else
            {
                lbmsg.Text = "";
            }
            ViewMode("ADD");
        }
        private DataSet GetOrdSetInfo()
        {
             string[] strKey = ("EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());

            List<string> lisSpParmValue = new List<string>();

            lisSpParmValue.AddRange(new string[] { 
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "9"});
            DataSet dtAdd = this.DataRequest_By_DataSet(SpName, strKey, lisSpParmValue.ToArray());
            return dtAdd;
        }
        public override void InitialByParam(string Mode, string strParam, DataTable dt)
        {
            base.InitialByParam(Mode, strParam, dt);
            if (_SaleUser == null)
            {
                _SaleUser = ReadUser();
            }
            if (_TicketUser == null)
            {
                _TicketUser = ReadUser();
            }

            chkSaleUser.Properties.DisplayMember = "Name";
            chkSaleUser.Properties.ValueMember = "User_Id";
            chkSaleUser.Properties.DataSource = _SaleUser.Tables[0].DefaultView;

            chkTicketUser.Properties.DisplayMember = "Name";
            chkTicketUser.Properties.ValueMember = "User_Id";
            chkTicketUser.Properties.DataSource = _TicketUser.Tables[0].DefaultView;
            chkSaleUser.EditValue = CApplication.App.CurrentSession.UserId.ToString();
            chkTicketUser.EditValue = CApplication.App.CurrentSession.UserId.ToString();

            if (strParam != string.Empty)
            {
                strkeyId = StaticFunctions.GetFrmParamValue(strParam, "KeyId", null);

                DataSet _dtOrd = ReadOrdInfo(strkeyId);//主表信息
                DataSet _dtDtl = ReadDtlInfo(strkeyId);//主表信息

                strkeyId = _dtOrd.Tables[0].Rows[0]["ProdSaleId"].ToString();
                ordstate = int.Parse(_dtOrd.Tables[0].Rows[0]["State"].ToString());
                txtPayOtherTotal.Text = _dtOrd.Tables[0].Rows[0]["PayOtherTotal"].ToString();
                txtPayXjTotal.Text = _dtOrd.Tables[0].Rows[0]["PayCashTotal"].ToString();
                txtPayXyCardTotal.Text = _dtOrd.Tables[0].Rows[0]["PayCardTotal"].ToString();
                txtPriceTotal.Text = _dtOrd.Tables[0].Rows[0]["PayCopeTotal"].ToString();

                this.txtTicketCode.Text = _dtOrd.Tables[0].Rows[0]["TicketCode"].ToString();
                this.txtCashCode.Text = _dtOrd.Tables[0].Rows[0]["CashCode"].ToString();

                chkSaleUser.EditValue = _dtOrd.Tables[0].Rows[0]["SaleUser"].ToString();
                chkTicketUser.EditValue = _dtOrd.Tables[0].Rows[0]["TicketUser"].ToString();

                gridCProd.DataSource = _dtDtl.Tables[0];
                gridCtlRec.DataSource = _dtDtl.Tables[1];
                ViewMode("VIEW");
                if (_dtOrd.Tables[0].Rows[0]["State"].ToString() == "1")
                {
                    ViewMode("THREE");
                }
                
               
            }
           
         
            
        }
        private DataSet ReadOrdInfo(string strKeyId)
        {
            string[] strKey = ("Key_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());

            List<string> lisSpParmValue = new List<string>();

            lisSpParmValue.AddRange(new string[] { 
                           strkeyId, 
                           
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "1"});
            DataSet dtAdd = this.DataRequest_By_DataSet(SpName, strKey, lisSpParmValue.ToArray());
            return dtAdd;
        }
        private DataSet ReadDtlInfo(string strKeyId)
        {
            string[] strKey = ("Key_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());

            List<string> lisSpParmValue = new List<string>();

            lisSpParmValue.AddRange(new string[] { 
                           strkeyId, 
                           
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "2"});
            DataSet dtAdd = this.DataRequest_By_DataSet(SpName, strKey, lisSpParmValue.ToArray());
            return dtAdd;
        }
        private DataSet ReadUser()
        {
            string[] strKey = ("Key_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());

            List<string> lisSpParmValue = new List<string>();

            lisSpParmValue.AddRange(new string[] { 
                           strkeyId, 
                           
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "91"});
            DataSet dtAdd = this.DataRequest_By_DataSet(SpName, strKey, lisSpParmValue.ToArray());
            return dtAdd;
        }
        #endregion
        #region 货品资料
        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != 13)
                return;
            ReadProdInfo();
            
        }
        private void ReadProdInfo()
        {
            if (ordstate == 10)
            {
                MessageBox.Show("本销售单已经完成销售，无法添加货品，如果需要销售，请点新增");

            }
            else
            {
                string strText = txtBarCode.Text.Trim();
                if (strText == string.Empty)
                {
                    MessageBox.Show("请先输入货品条码！");
                    txtBarCode.Focus();
                }
                else
                {

                    string[] strKey = ("Number,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());

                    List<string> lisSpParmValue = new List<string>();

                    lisSpParmValue.AddRange(new string[] {                        
                          strText,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "44"});
                    DataSet dtAdd = this.DataRequest_By_DataSet("FrmSelect", strKey, lisSpParmValue.ToArray());
                    if (dtAdd != null)
                    {
                        if (dtAdd.Tables[0].Rows.Count > 0)
                        {
                            txtProdName.Text = dtAdd.Tables[0].Rows[0]["ProdName"].ToString();
                            txtQty.Text = dtAdd.Tables[0].Rows[0]["Qty"].ToString();
                            txtCWgt.Text = dtAdd.Tables[0].Rows[0]["Wgt"].ToString();
                            txtSaleGoldPrice.Text = txtGoldPrice.Text = dtAdd.Tables[0].Rows[0]["SaleGoldPrice"].ToString();
                           // txtSalePrice.Text = dtAdd.Tables[0].Rows[0]["Price"].ToString();
                            txtSalePrice.Text = Math.Round(decimal.Parse(dtAdd.Tables[0].Rows[0]["Price"].ToString()), this.SaleRound).ToString();
                           // txtPrice.Text =  (dtAdd.Tables[0].Rows[0]["Price"].ToString();
                            txtPrice.Text = Math.Round(decimal.Parse(dtAdd.Tables[0].Rows[0]["Price"].ToString()), this.SaleRound).ToString();
                            txtFee.Text = dtAdd.Tables[0].Rows[0]["ExFee"].ToString();
                            txtCutFee.Text = dtAdd.Tables[0].Rows[0]["CutFee"].ToString();
                            _tmpexfee = decimal.Parse(dtAdd.Tables[0].Rows[0]["ExFee"].ToString());
                            _lowgoldprice = decimal.Parse(dtAdd.Tables[0].Rows[0]["LowGoldPrice"].ToString());

                            
                            txtSaleGoldPrice.Focus();
                            btnAddProd.Enabled = true;
                            btnCancelProd.Enabled = true;

                            if (dtAdd.Tables[0].Rows[0]["SaleType"].ToString().Equals("1"))
                            {

                                SaleFeeWgt = decimal.Parse(dtAdd.Tables[0].Rows[0]["ExFee"].ToString()) / decimal.Parse(dtAdd.Tables[0].Rows[0]["Wgt"].ToString());
                                _SaleType = 1;
                                ViewMode("GOLDTWO");
                            }
                            else
                            {
                                SaleFeeQty = decimal.Parse(dtAdd.Tables[0].Rows[0]["ExFee"].ToString());
                                _SaleType = 2;
                                ViewMode("JEWTWO");
                            }
                        }
                        else
                        {
                            MessageBox.Show("您输入的条码不正确或不在库存！请确认");
                            btnAddProd.Enabled = false;
                        }
                    }
                    else
                    {
                        btnAddProd.Enabled = false;
                    }
                }
            }
        }
        private void txtSaleGoldPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            
            if ((int)e.KeyChar != 13)
                return;
            string strText = txtSaleGoldPrice.Text.Trim();
            if (strText == string.Empty)
            {
                MessageBox.Show("请输入实收金价！");
                txtSaleGoldPrice.Focus();
            }
            else
            {
                //if (decimal.Parse(strText) < _lowgoldprice)
                //{
                //    MessageBox.Show("实收金价低于最低销售金价！");
                //    txtSaleGoldPrice.Focus();
                //}
                //else
                //{
                    CalcProdSalePrice(0);
                //}
            }
        }

        private void ClearControl()
        {
            txtBarCode.Text = "";
            txtFee.Text = "";
            txtGoldPrice.Text = "";
            txtPrice.Text = "";
            txtProdName.Text = "";
            txtQty.Text = "";
            txtSaleGoldPrice.Text = "";
            txtSalePrice.Text = "";
             
            txtWgt.Text = "0";
            txtCWgt.Text = "0";
            txtCutWgt.Text = "0";
            txtCutTotalFee.Text = "0";
            txtCutFee.Text = "0";
            txtPbPrice.Text = "0";
            btnAddProd.Enabled = false;
        }
        private void btnAddProd_Click(object sender, EventArgs e)
        {
            _tmpexfee = 0;
            if (_SaleType.ToString().Equals("1"))
            {
                if (ElecState)
                {
                    if (txtCWgt.Text == string.Empty || txtWgt.Text == string.Empty)
                    {
                        MessageBox.Show("对不起！实际金重没有获取到，无法开单！");
                        return;
                    }
                    decimal cwgt = decimal.Parse(txtCWgt.Text);
                    decimal pwgt = decimal.Parse(txtWgt.Text);
                    decimal cutwgt = decimal.Parse(txtCutWgt.Text);

                    if (Math.Abs(cwgt - cutwgt - pwgt) > BarWgt)
                    {
                        MessageBox.Show("对不起！实际金重和系统金重差异超过" + BarWgt.ToString() + "克，无法开单！");
                        return;
                    }
                }
            }
            string barcode = txtBarCode.Text;
            if (barcode == string.Empty && barcode=="")
            {
                MessageBox.Show("请先输入条码，然后回车");
                return;
            }
           
            string wgt = txtCWgt.Text;
            string fee = txtFee.Text;
            string salegoldprice = txtSaleGoldPrice.Text;
            string goldPrice = txtGoldPrice.Text;
            string price = txtPrice.Text;
            string saleprice = txtSalePrice.Text;
            string strField = string.Empty;
            string strValues = string.Empty;
            DataSet dtAdd = new DataSet();
            if (strDtlId == "-1")
            {
                strField = "Barcode,SaleGoldPrice,Price,SalePbPrice,Wgt,ExFee,GoldPrice,CutWgt,CutFee,CutTotalFee,PbPrice";
                strValues = "'" + barcode + "','" + salegoldprice + "','" + price + "','" + saleprice + "','" + wgt + "','";
                strValues = strValues + fee + "','" + goldPrice + "','" + txtCutWgt.Text + "','" + txtCutFee.Text + "','" + txtCutTotalFee.Text + "'";
                strValues = strValues + ",'" + txtPbPrice.Text + "'";

                string[] strKey = ("Ord_Id,Barcode,strFields,strFieldValues,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());

                List<string> lisSpParmValue = new List<string>();

                lisSpParmValue.AddRange(new string[] { 
                           strkeyId, barcode,
                          strField,strValues,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "31"});
                 dtAdd = this.DataRequest_By_DataSet(SpName, strKey, lisSpParmValue.ToArray());
            }
            else
            {
                strField = "Barcode='"+barcode+"',SaleGoldPrice="+salegoldprice+",Price="+price+",SalePbPrice="+saleprice+",Wgt="+wgt+",ExFee="+fee+",GoldPrice="+goldPrice;
                strField = strField + ",CutWgt='" + txtCutWgt.Text + "',CutTotalFee='" + txtCutTotalFee.Text + "'";
                strField = strField + ",PbPrice='"+txtPbPrice.Text.ToString()+"'";
                string[] strKey = ("Ord_Id,Key_Id,Barcode,strEditSql,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());

                List<string> lisSpParmValue = new List<string>();

                lisSpParmValue.AddRange(new string[] { 
                           strkeyId,strDtlId, barcode,
                          strField,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "32"});
                dtAdd = this.DataRequest_By_DataSet(SpName, strKey, lisSpParmValue.ToArray());
            }

            if (dtAdd != null)
            {

                if (dtAdd.Tables.Count == 2)
                {
                    strkeyId = dtAdd.Tables[0].Rows[0]["ProdSaleId"].ToString();
                    txtPayXjTotal.Text = txtPriceTotal.Text = dtAdd.Tables[0].Rows[0]["SaleTotalPrice"].ToString();
                    txtPayOtherTotal.Text = txtPayXyCardTotal.Text = "0";

                    DataTable _dt = dtAdd.Tables[1];
                    gridCProd.DataSource = _dt.DefaultView;
                    strDtlId = "-1";
                    ViewMode("THREE");

                    txtCutFee.Text = "0";
                    txtCutTotalFee.Text = "0";
                    SaleFeeWgt = 0;
                    SaleFeeQty = 0;
                }
            }
            
        }
        #endregion

        #region 付款信息
        private void txtPayXyCardTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }//支持退格Backspace键 
            if (e.KeyChar == (char)0x08)
            {
                e.Handled = false;
            }

            if ((int)e.KeyChar != 13)
                return;
            btnFinSave.Enabled = false;
            btnSaveSale.Enabled = true;
            decimal paytotal = decimal.Parse(txtPriceTotal.Text);
            decimal payxjtotal = decimal.Parse(txtPayXjTotal.Text);
            decimal payxyktotal = decimal.Parse(txtPayXyCardTotal.Text);
            decimal payothertotal = decimal.Parse(txtPayOtherTotal.Text);
            payxjtotal = paytotal - payxyktotal - payothertotal;
            if (payxyktotal > paytotal)
            {
                MessageBox.Show("您输入的金额超过总金额！");
                txtPayXjTotal.SelectAll();
                txtPayXjTotal.Focus();
                return;
            }
            if (payxjtotal < 0)
            {
                txtPayXjTotal.Text = "0";
                txtPayOtherTotal.Text = (paytotal - payxyktotal).ToString();
            }
            else
            {
                txtPayXjTotal.Text = payxjtotal.ToString();
            }
            btnSaveSale.Focus();
        }

        private void txtPayOtherTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }//支持退格Backspace键 
            if (e.KeyChar == (char)0x08)
            {
                e.Handled = false;
            }

            if ((int)e.KeyChar != 13)
                return;
            btnFinSave.Enabled = false;
            btnSaveSale.Enabled = true;
            decimal paytotal = decimal.Parse(txtPriceTotal.Text);
            decimal payxjtotal = decimal.Parse(txtPayXjTotal.Text);
            decimal payxyktotal = decimal.Parse(txtPayXyCardTotal.Text);
            decimal payothertotal = decimal.Parse(txtPayOtherTotal.Text);
            payxjtotal = paytotal - payxyktotal - payothertotal;
            if (payothertotal > paytotal)
            {
                MessageBox.Show("您输入的金额超过总金额！");
                txtPayOtherTotal.SelectAll();
                txtPayOtherTotal.Focus();
                return;
            }
            if (payxjtotal < 0)
            {
                txtPayXjTotal.Text = "0";
                txtPayXyCardTotal.Text = (paytotal - payothertotal).ToString();
            }
            else
            {
                txtPayXjTotal.Text = payxjtotal.ToString();
            }
            btnSaveSale.Focus();
        }
        #endregion
        #region 旧金回收
        private void btnRecOk_Click(object sender, EventArgs e)
        {
            if (strkeyId == "-1")
            {
                    MessageBox.Show("请先保存表头！");
                    return;
            }
            if (txtRecCode.Text == string.Empty)
            {
                MessageBox.Show("请输入旧金回收卡号！");
                txtRecCode.Focus();
                return;
            }
            if (cbRecType.EditValue == null)
            {
                MessageBox.Show("请选择兑换类型！");
                cbRecType.Focus();
                return;
            }
            decimal RecPreFee = decimal.Parse(txtRecPreExFee.Text);
            string RecCode = txtRecCode.Text;
            string RecType = cbRecType.EditValue.ToString();
            string RecOk = chkRec.EditValue.ToString().Equals("True")?"1":"0";
            string[] strKey = ("Ord_Id,Barcode,RecType,State,RecPreFee,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());

            List<string> lisSpParmValue = new List<string>();

            lisSpParmValue.AddRange(new string[] { 
                           strkeyId, RecCode,RecType,RecOk,RecPreFee.ToString(),
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "21"});
            DataSet dtAdd = this.DataRequest_By_DataSet(SpName, strKey, lisSpParmValue.ToArray());
            if (dtAdd != null)
            {
                 
                txtPayXjTotal.Text = txtPriceTotal.Text = dtAdd.Tables[0].Rows[0]["SalePrice"].ToString();
                txtPayOtherTotal.Text = txtPayXyCardTotal.Text = "0";
                gridCtlRec.DataSource = dtAdd.Tables[1];
                txtRecCode.Text = "";
                txtRecPreExFee.Text = "0";
                cbRecType.EditValue = "";
            }

        }
        private DateTime _dt = DateTime.Now;
        private void txtRecCode_KeyPress(object sender, KeyPressEventArgs e)
        {

            DateTime tempDt = DateTime.Now;
            TimeSpan ts = tempDt.Subtract(_dt);
            if (ts.Milliseconds > 50)
                txtRecCode.Text = "";
            _dt = tempDt;


            if ((int)e.KeyChar != 13)
                return;
                e.Handled = false;
                btnRecOk.Enabled = true;
                cbRecType.Focus();
            

          
        }

        private void cbRecType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != 13)
                return;
            btnRecOk.Enabled = true;
            btnRecOk.Focus();

        }
        #endregion
        #region 保存付款信息
        private void btnSaveSale_Click(object sender, EventArgs e)
        {
            if (strkeyId == "-1")
            {
                MessageBox.Show("请先保存表头！");
                return;
            }
            
            decimal paytotal =decimal.Parse(txtPriceTotal.Text);
            decimal payxjtotal = decimal.Parse(txtPayXjTotal.Text);
            decimal payxyktotal = decimal.Parse(txtPayXyCardTotal.Text);
            decimal payothertotal = decimal.Parse(txtPayOtherTotal.Text);
             string strField = string.Empty;
            if (paytotal != payxjtotal + payxyktotal + payothertotal)
            {
                MessageBox.Show("付款方式金额之和不等于应付金额！请确认");
                return;
            }

            string tickuser = chkTicketUser.EditValue.ToString();
            if (chkTicketUser.Text.ToString().Equals(""))
            {
                MessageBox.Show("请选择开票人");
                chkTicketUser.Focus();
                return;
            }
            if (tickuser.Equals("请选择") )
            {
                MessageBox.Show("请选择开票人");
                chkTicketUser.Focus();
                return;
            }
            strField = "TicketUser='" + tickuser + "', PayCopeTotal=" + paytotal.ToString() + ",PayCashTotal=" + payxjtotal.ToString() + ",PayCardTotal=" + payxyktotal.ToString() + ",PayOtherTotal=" + payothertotal.ToString();

            string[] strKey = ("Ord_Id,strEditSql,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());

            List<string> lisSpParmValue = new List<string>();
            lisSpParmValue.AddRange(new string[] { 
                           strkeyId, 
                          strField,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "4"});
            DataSet dtAdd = this.DataRequest_By_DataSet(SpName, strKey, lisSpParmValue.ToArray());
            if (dtAdd.Tables[0].Rows.Count == 1)
            {
                MessageBox.Show("保存付款信息成功！");
                btnSaveSale.Enabled = false;        
                btnFinSave.Enabled = true;
                txtTicketCode.Enabled = true;
                txtCashCode.Enabled = true;
                DoPrint();
            }
        }
        public DataSet  GetBtnMRpt()
        {
            string[] strKey = ("Key_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());

            List<string> lisSpParmValue = new List<string>();

            lisSpParmValue.AddRange(new string[] { 
                           strkeyId, 
                           
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "92"});
            DataSet dtAdd = this.DataRequest_By_DataSet(SpName, strKey, lisSpParmValue.ToArray());
            return dtAdd;

             
        }
        private void DoPrint()
        {


            string strRptName = string.Empty;
            string strSpFlag = string.Empty;
            string strTitle = string.Empty;
            DataSet _ds = GetBtnMRpt();
            if (_ds.Tables[0].Rows.Count > 0)
            {
                strRptName = _ds.Tables[0].Rows[0]["RptName"].ToString();
                strTitle = _ds.Tables[0].Rows[0]["RptTitle"].ToString();
                strSpFlag = _ds.Tables[0].Rows[0]["RptFlag"].ToString();

                string[] strParams = "OrdId,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                string[] strParaVals = new string[] { 
                strkeyId, 
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                strSpFlag};
                DataSet ds = this.DataRequest_By_DataSet("Rpt_RS_DS", strParams, strParaVals);
                StaticFunctions.ShowRptRS(strRptName, strTitle, this.ParentForm, null, null, ds);
            }
            else
            {
                MessageBox.Show("报表设置错误，无法打印");
            }
        }

        private void ViewMode(string Mode)
        {
            if (Mode == "SAVE")
            {
                
                txtPayOtherTotal.Enabled = false;
                txtPayXjTotal.Enabled = false;
                txtPayXyCardTotal.Enabled = false;
                txtPriceTotal.Enabled = false;
            }
            else if (Mode == "VIEWEDIT")
            {
                txtBarCode.Enabled = true;
                btnFinSave.Enabled = true;
                txtCashCode.Enabled = true;
                txtTicketCode.Enabled = true;
                btnSaveSale.Enabled = true;
                txtPayOtherTotal.Enabled = true;
                txtPayXyCardTotal.Enabled = true;
                chkTicketUser.Enabled = true;
                chkSaleUser.Enabled = true;
                txtPbPrice.Enabled = false;
            }
            else if (Mode == "VIEW")
            {
                txtBarCode.Enabled = false;
                txtPayOtherTotal.Enabled = false;
                txtPayXjTotal.Enabled = false;
                txtPayXyCardTotal.Enabled = false;
                txtPriceTotal.Enabled = false;
                btnAddProd.Enabled = false;
                btnRecOk.Enabled = false;
                btnSaveSale.Enabled = false;
                txtRecCode.Enabled = false;
                txtRecPreExFee.Enabled = false;
                cbRecType.Enabled = false;
                chkRec.Enabled = false;
                txtTicketCode.Enabled = false;
                txtCashCode.Enabled = false;
                btnFinSave.Enabled = false;
                btnCancelProd.Enabled = false;

                chkSaleUser.Enabled = false;
                chkTicketUser.Enabled = false;

                txtSaleGoldPrice.Enabled = false;
                txtFee.Enabled = false;
                txtSalePrice.Enabled = false;
                txtCutWgt.Enabled = false;
                txtPbPrice.Enabled = false;

            }
            else if (Mode == "Fin")
            {
                gridCProd.DataSource = null;
                gridCtlRec.DataSource = null;
                txtPayOtherTotal.Text = "";
                txtPayXjTotal.Text = "";
                txtPayXyCardTotal.Text = "";
                txtPriceTotal.Text = "";
                txtBarCode.Focus();
                txtPayOtherTotal.Enabled = true;
                txtPayXyCardTotal.Enabled = true;
                txtCashCode.Text = "";
                txtTicketCode.Text = "";
                strkeyId = "-1";
                strDtlId = "-1";
                txtPbPrice.Text = "0";

                chkSaleUser.Enabled = true;
                chkTicketUser.Enabled = false;
                chkSaleUser.EditValue = "请选择";
                chkTicketUser.EditValue = "请选择";
                ordstate = 1;
                txtSaleGoldPrice.Enabled = false;
                txtFee.Enabled = false;
                txtSalePrice.Enabled = false;
                txtCutWgt.Enabled = false;
                txtPbPrice.Enabled = false;
            }
            else if (Mode == "ADD")
            {
                txtBarCode.Enabled = true;

                gridCProd.DataSource = null;
                gridCtlRec.DataSource = null;
                txtPayOtherTotal.Text = "";
                txtPayXjTotal.Text = "";
                txtPayXyCardTotal.Text = "";
                txtPriceTotal.Text = "";
                txtBarCode.Focus();
                txtPayOtherTotal.Enabled = false;
                txtPayXyCardTotal.Enabled = false;
                txtCashCode.Text = "";
                txtTicketCode.Text = "";
                txtPbPrice.Text = "0";
                strkeyId = "-1";
                strDtlId = "-1";
                chkSaleUser.Enabled = false;
                chkTicketUser.Enabled = false;
                chkSaleUser.EditValue = "请选择";
                chkTicketUser.EditValue = "请选择";
                ordstate = 1;
                txtSaleGoldPrice.Enabled = false;
                txtFee.Enabled = false;
                txtSalePrice.Enabled = false;
                txtCutWgt.Enabled = false;
                txtWgt.Enabled = false;
                btnAddProd.Enabled = false;
                txtPbPrice.Enabled = false;
            }
            else if (Mode == "GOLDTWO")
            {
                txtCutWgt.Enabled = true;
                txtSaleGoldPrice.Enabled = true;
                txtFee.Enabled = true;
                txtSalePrice.Enabled = false;
                btnAddProd.Enabled = true;
                txtWgt.Enabled = false;
                txtPbPrice.Enabled = true;
            }
            else if (Mode == "JEWTWO")
            {
                txtCutWgt.Enabled = false;
                txtSaleGoldPrice.Enabled = false;
                txtFee.Enabled = false;
                txtSalePrice.Enabled = false;
                btnAddProd.Enabled = true;
                txtWgt.Enabled = false;
                txtPbPrice.Enabled = true;
            }
            else if (Mode == "THREE")
            {
                txtBarCode.Enabled = true;
                txtPayOtherTotal.Enabled = true;
                txtPayXyCardTotal.Enabled = true;
                btnSaveSale.Enabled = true;
                txtRecCode.Enabled = true;
                txtRecPreExFee.Enabled = true;
                cbRecType.Enabled = true;
                chkRec.Enabled = true;

                txtCashCode.Enabled = true;
                txtTicketCode.Enabled = true;
                chkSaleUser.Enabled = true;
                chkTicketUser.Enabled = true;
                btnFinSave.Enabled = true;
                ClearControl();
            }
        }
        #endregion

        #region 其他
        private void gridInfo_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            DataRow dr = this.gridInfo.GetFocusedDataRow();
        }
        /// <summary>
        /// 双击货品进行编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridInfo_DoubleClick(object sender, EventArgs e)
        {
            if (ordstate != 1)
            {
                MessageBox.Show("单据不在可编辑状态，无法编辑");
            }
            else
            {
                DataRow dr = this.gridInfo.GetFocusedDataRow();
                if (dr != null)
                {
                    strDtlId = dr["ProdSaleDtlId"].ToString();
                    txtBarCode.Text = dr["Barcode"].ToString();
                    txtProdName.Text = dr["ProdName"].ToString();
                    txtQty.Text = dr["Qty"].ToString();
                    if (ElecState)
                    {
                        txtWgt.Text = dr["Wgt"].ToString() ;
                        txtCWgt.Text = (decimal.Parse(dr["Wgt"].ToString()) + BarWgt.ToString()).ToString();
                    }
                    else
                    {
                        txtWgt.Text = "0";
                        txtCWgt.Text = dr["Wgt"].ToString();
                    }
                   
                    txtGoldPrice.Text = dr["GoldPrice"].ToString();
                    txtSaleGoldPrice.Text = dr["SaleGoldPrice"].ToString();
                    txtFee.Text = dr["ExFee"].ToString();
                    txtPrice.Text = dr["Price"].ToString();
                    txtSalePrice.Text = dr["SalePbPrice"].ToString();
                    txtCutFee.Text = dr["CutFee"].ToString();
                    txtCutWgt.Text = dr["CutWgt"].ToString();
                    txtCutTotalFee.Text = dr["CutTotalFee"].ToString();
                    txtPbPrice.Text = dr["PbPrice"].ToString();
                    btnAddProd.Enabled = true;
                    btnCancelProd.Enabled = true;

                    SaleFeeQty = decimal.Parse(dr["SalesFeeQty"].ToString());
                    SaleFeeWgt = decimal.Parse(dr["SalesFeeWgt"].ToString());

                    if (dr["SaleType"].ToString().Equals("1"))
                    {
                        txtCutWgt.Enabled = true;
                        txtSaleGoldPrice.Enabled = true;
                        txtFee.Enabled = true;
                        txtPbPrice.Enabled = true;
                    }
                    else
                    {
                        txtCutWgt.Enabled = false;
                        txtSaleGoldPrice.Enabled = false;
                        txtFee.Enabled = false;
                       
                    }
                }
            }
        }

        private void btnSaveFin_Click(object sender, EventArgs e)
        {
            if (strkeyId == "-1")
            {
                MessageBox.Show("请先保存表头！");
                return;
            }
          
            string saleuser = chkSaleUser.EditValue.ToString();
            
            if (saleuser.Equals("请选择"))
            {
                MessageBox.Show("请选择付货人");
                chkSaleUser.Focus();
                return;
            }
            if (chkSaleUser.Text.ToString().Equals(""))
            {
                MessageBox.Show("请选择付货人");
                chkSaleUser.Focus();
                return;
            }
            string strticketcode = txtTicketCode.Text;
            string strcashcode = txtCashCode.Text;
            if (strticketcode == string.Empty && strcashcode == string.Empty)
            {
                MessageBox.Show("请输入收银台号和小票号！");
            }
            else
            {
                
                string cashcode = txtCashCode.Text;
                string ticketcode = txtTicketCode.Text;
                
                string strField = string.Empty;


                strField = " CashCode='" + cashcode.ToString() + "',TicketCode='" + ticketcode.ToString() + "'";
                strField = strField + ",SaleUser='" + saleuser + "'";
                string[] strKey = ("Ord_Id,strEditSql,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());

                List<string> lisSpParmValue = new List<string>();

                lisSpParmValue.AddRange(new string[] { 
                           strkeyId, 
                          strField,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "55"});
                DataSet dtAdd = this.DataRequest_By_DataSet(SpName, strKey, lisSpParmValue.ToArray());
                if (dtAdd != null)
                {
                    if (dtAdd.Tables[0].Rows.Count == 1)
                    {
                        MessageBox.Show("销售完成！");
                       
                        ViewMode("Fin");
                    }
                }
            }
        }

        private void btnCancelProd_Click(object sender, EventArgs e)
        {
            ClearControl();
            if (strDtlId != "-1")
            {

                string[] strKey = ("Ord_Id,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());

                List<string> lisSpParmValue = new List<string>();

                lisSpParmValue.AddRange(new string[] { 
                           strkeyId, 
                          strDtlId,
                         CApplication.App.CurrentSession.UserId.ToString(),
                         CApplication.App.CurrentSession.DeptId.ToString(),
                         CApplication.App.CurrentSession.FyId.ToString(),
                         "33"});
                DataSet dtAdd = this.DataRequest_By_DataSet(SpName, strKey, lisSpParmValue.ToArray());
                if (dtAdd!= null)
                {
                    strDtlId = "-1";
                    RecBindData(dtAdd, gridCProd);
                }
            }
        }

        private void btnNewAdd_Click(object sender, EventArgs e)
        {
            ClearControl();
            ViewMode("ADD");
        }
        #endregion

        #region 删除回收
        public override bool DeleteFocusedItem()
        {
            DataRow drOrd =  gridRecInfo.GetFocusedDataRow();

            if (drOrd == null || drOrd["ProdSaleRecDtlId"].ToString() == string.Empty)
            {
                return false;
            }
             
            if (MessageBox.Show("确认删除回收明细", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                return false;

          
            string[] strKey = "Ord_Id,Key_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            DataSet dtAdd = this.DataRequest_By_DataSet(SpName,
                                strKey, new string[] {strkeyId,
                                 drOrd["ProdSaleRecDtlId"].ToString(),
                                 CApplication.App.CurrentSession.UserId.ToString(),
                                 CApplication.App.CurrentSession.DeptId.ToString(),
                                 CApplication.App.CurrentSession.FyId.ToString(),
                                 "23"});
            if(dtAdd!=null)
            RecBindData(dtAdd,gridCtlRec);
            return true;

             
        }
        private void RecBindData(DataSet _ds,DevExpress.XtraGrid.GridControl gridCtl)
        {
            txtPayXjTotal.Text = txtPriceTotal.Text = _ds.Tables[0].Rows[0]["PayCopeTotal"].ToString();
            txtPayOtherTotal.Text = txtPayXyCardTotal.Text = "0";
            gridCtl.DataSource = _ds.Tables[1];
            txtRecCode.Text = "";
            cbRecType.EditValue = "";
            txtRecPreExFee.Text = "0";
        }
        #endregion

        #region
        public override void SetText(string text)
        {
           
            
              txtWgt.Text = StaticFunctions.Round(double.Parse(text), 3, 0.5).ToString();
            
        }

        #endregion

        private void txtCutWgt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != 13)
                return;
            string strText = txtCutWgt.Text.Trim();
            if (strText == string.Empty && strText.Equals("0"))
            {
                MessageBox.Show("请输入截料重量！");
                txtCutWgt.Focus();
                
            }
            else
            {
                CalcProdSalePrice(0);
            }
        }
        private void txtFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != 13)
                return;
            string strText = txtFee.Text.Trim();
            if (strText == string.Empty)
            {
                MessageBox.Show("请输入实收工费总额！");
                txtFee.Focus();
            }
            else
            {
                if (_tmpexfee < decimal.Parse(strText))
                {
                    MessageBox.Show("您输入实收工费总额大于标准工费！");
                    txtFee.Focus();
                }
                else
                {
                    CalcProdSalePrice(decimal.Parse(strText.ToString()),true);
                }
            }
        }

        private void CalcProdSalePrice(decimal zfee,bool f)
        {
                decimal goldprice = 0;
                decimal Wgt = 0;
                decimal Fee = 0;
                decimal CutWgt = 0;
                decimal SalePrice = 0;
                decimal _CutTotalFee=0;
                decimal _CutFee=0;
                
                 
                    Wgt = decimal.Parse(txtCWgt.Text);
                
                _CutFee=decimal.Parse(txtCutFee.Text);
                goldprice = decimal.Parse(txtSaleGoldPrice.Text);
                CutWgt = decimal.Parse(txtCutWgt.Text);
                if (CutWgt > Wgt)
                {
                    MessageBox.Show("截料重量超过货品金重，无法继续");
                    txtCutWgt.Text = "0";
                    txtCutWgt.Focus();
                    btnAddProd.Enabled = false;
                    return;
                }
                
                 Fee = zfee;
                

                _CutTotalFee=CutWgt*_CutFee;
                SalePrice = (Wgt - CutWgt) * goldprice + Fee + _CutTotalFee;
                txtCutTotalFee.Text = Math.Round(_CutTotalFee, this.SaleRound, MidpointRounding.AwayFromZero).ToString();
                txtFee.Text = Math.Round(Fee, this.SaleRound, MidpointRounding.AwayFromZero).ToString(); 
                txtSalePrice.Text = Math.Round(SalePrice, this.SaleRound,MidpointRounding.AwayFromZero).ToString();
                btnAddProd.Enabled = true;
                btnAddProd.Focus();
        }
        private void CalcProdSalePrice(decimal zfee)
        {
            decimal goldprice = 0;
            decimal Wgt = 0;
            decimal Fee = 0;
            decimal CutWgt = 0;
            decimal SalePrice = 0;
            decimal _CutTotalFee = 0;
            decimal _CutFee = 0;


            Wgt = decimal.Parse(txtCWgt.Text);

            _CutFee = decimal.Parse(txtCutFee.Text);
            goldprice = decimal.Parse(txtSaleGoldPrice.Text);
            CutWgt = decimal.Parse(txtCutWgt.Text);
            if (CutWgt > Wgt)
            {
                MessageBox.Show("截料重量超过货品金重，无法继续");
                txtCutWgt.Text = "0";
                txtCutWgt.Focus();
                btnAddProd.Enabled = false;
                return;
            }
            
                Fee = (Wgt - CutWgt) * SaleFeeWgt + SaleFeeQty;
            

            _CutTotalFee = CutWgt * _CutFee;
            SalePrice = (Wgt - CutWgt) * goldprice + Fee + _CutTotalFee;
            txtCutTotalFee.Text = Math.Round(_CutTotalFee, this.SaleRound, MidpointRounding.AwayFromZero).ToString();
            txtFee.Text = Math.Round(Fee, this.SaleRound, MidpointRounding.AwayFromZero).ToString();
            txtSalePrice.Text = Math.Round(SalePrice, this.SaleRound, MidpointRounding.AwayFromZero).ToString();
            btnAddProd.Enabled = true;
            btnAddProd.Focus();
        }
        private void txtSalePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != 13)
                return;
            string strText = txtSalePrice.Text.Trim();
            if (strText == string.Empty)
            {
                MessageBox.Show("请输入实售价格！");
                txtSalePrice.Focus();
            }
            else
            {
             
                btnAddProd.Focus();
            }
        }

        private void txtSaleGoldPrice_Leave(object sender, EventArgs e)
        {
            string strText = txtSaleGoldPrice.Text.Trim();
            if (strText == string.Empty)
            {
                MessageBox.Show("请输入实收金价！");
                txtSaleGoldPrice.Focus();
            }
            else
            {
                //if (decimal.Parse(strText) < _lowgoldprice)
                //{
                //    MessageBox.Show("实收金价低于最低销售金价！");
                //    txtSaleGoldPrice.Focus();
                //}
                //else
                //{
                    CalcProdSalePrice(0);
                //}
            }
        }

        private void txtFee_Leave(object sender, EventArgs e)
        {
           string strText = txtFee.Text.Trim();
           if (strText == string.Empty)
           {
               MessageBox.Show("请输入实收工费总额！");
               txtFee.Focus();
           }
           else
           {

               CalcProdSalePrice(decimal.Parse(strText.ToString()),true);
           }
        }

        private void txtSalePrice_Leave(object sender, EventArgs e)
        {
            string strText = txtSalePrice.Text.Trim();
            if (strText == string.Empty)
            {
                MessageBox.Show("请输入实售价格！");
                txtSalePrice.Focus();
            }
            else
            {

                btnAddProd.Focus();
            }
        }

        private void txtCutWgt_Leave(object sender, EventArgs e)
        {
            string strText = txtCutWgt.Text.Trim();
            if (strText == string.Empty && strText.Equals("0"))
            {
                MessageBox.Show("请输入截料重量！");
                txtCutWgt.Focus();

            }
            else
            {
                CalcProdSalePrice(0);
            }
        }

        private void txtBarCode_Leave(object sender, EventArgs e)
        {
            if (!txtBarCode.Text.ToString().Equals(""))
            {
                ReadProdInfo();
            }
        }

        
    }
}
