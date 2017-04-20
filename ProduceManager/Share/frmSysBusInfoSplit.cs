using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;

namespace ProduceManager
{
    public partial class frmSysBusInfoSplit : frmEditorBase
    {
        #region private Params
        private DataSet dsLoad = null;
        private DataTable dtConst = null;
        private DataTable dtShow = null;
        private string[] strFileds = null;
        private DataRow drBtn = null;
        private string strSplitFiled = string.Empty;
        public DataRow DrSplit
        {
            get;
            set;
        }
        public string[] StrFileds
        {
            get { return strFileds; }
            set { strFileds = value; }
        }
        public string StrOrdKeyId
        {
            get;
            set;
        }
        public string StrUpdSpName
        {
            get;
            set;
        }
        public DataSet DsRets
        {
            get;
            set;
        }
        #endregion

        public frmSysBusInfoSplit(DataRow drBtn)
        {
            InitializeComponent();

            this.drBtn = drBtn;
            strSplitFiled = drBtn["SplitFiled"].ToString();
            this.Text = drBtn["SplitClassText"].ToString();
            InitContr();
        }
        private void InitContr()
        {
            if (dsLoad != null)
                return;

            dsLoad = this.GetFrmLoadDs(drBtn["SplitClassName"].ToString());
            dsLoad.AcceptChanges();
            dtShow = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];

            if (drBtn["SplitClassShowIcon"].ToString() == "True")
            {
                GridColumn gridCol = new GridColumn();
                RepositoryItemImageEdit repImg = new RepositoryItemImageEdit();
                repImg.AutoHeight = false;
                repImg.Buttons.Clear();
                repImg.Name = "gridVInfo_repImg";
                repImg.PopupFormMinSize = new System.Drawing.Size(450, 350);
                repImg.ReadOnly = true;
                repImg.Popup += new System.EventHandler(this.repImg_Popup);

                gridCol.Caption = "图片";
                gridCol.ColumnEdit = repImg;
                gridCol.FieldName = "Icon";
                gridCol.Name = "gridVInfo_GCol";
                gridCol.OptionsColumn.AllowMove = false;
                gridCol.OptionsColumn.ReadOnly = true;
                gridCol.Visible = true;
                gridCol.VisibleIndex = 0;
                gridCol.Width = 50;

                this.gridCInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                    repImg});
                gridVInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridCol });
            }
            StaticFunctions.ShowGridControl(gridVInfo, dtShow, dtConst);
            StaticFunctions.ShowGridControl(gridVSplit, dtShow, dtConst, out strFileds);
        }
        private void frmSelectShop_Load(object sender, EventArgs e)
        {
            DataTable dtSplit = DrSplit.Table.Clone();
            DataRow drNew = dtSplit.NewRow();
            drNew.ItemArray = DrSplit.ItemArray;
            dtSplit.Rows.Add(drNew);

            gridCInfo.DataSource = dtSplit.DefaultView;
            gridVInfo.BestFitColumns();
        }
        private void btnSplit_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (int.TryParse(txtNum.Text, out i))
            {
                if (frmDataTable == null)
                    frmDataTable = DrSplit.Table.Clone();
                else
                    frmDataTable.Rows.Clear();

                for (int j = 0; j < i; j++)
                {
                    DataRow drNew = frmDataTable.NewRow();
                    drNew.ItemArray = DrSplit.ItemArray;
                    drNew[strSplitFiled] = DBNull.Value;
                    frmDataTable.Rows.Add(drNew);
                }
                gridCSplit.DataSource = frmDataTable.DefaultView;
                gridVSplit.BestFitColumns();
            }
            else
            {
                MessageBox.Show("请明确要拆分成几份.");
                txtNum.Focus();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (frmDataTable == null)
                return;

            frmDataTable.AcceptChanges();
            if (frmDataTable.Rows.Count == 0)
                return;

            double iA = 0;
            foreach (DataRow dr in frmDataTable.Rows)
            {
                iA += (dr[strSplitFiled].ToString() == string.Empty ? 0 : double.Parse(dr[strSplitFiled].ToString()));
            }
            if (iA != double.Parse(DrSplit[strSplitFiled].ToString()))
            {
                MessageBox.Show(drBtn["SplitErrorMsg"].ToString());
                return;
            }
            string strSplits = StaticFunctions.GetStringX(StrFileds, frmDataTable);
            if (strSplits == string.Empty)
            {
                MessageBox.Show("导入出错，未设置UpdatelFiled.");
                return;
            }
            string[] strKey = "Ord_Id,Key_Id,strSplits,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            string[] strVal = new string[] {
                StrOrdKeyId,
                DrSplit[drBtn["SplitGvKeyId"].ToString()].ToString(),
                strSplits,
                CApplication.App.CurrentSession.UserId.ToString(),
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString(),
                drBtn["SplitSpFlag"].ToString() };
            DataSet dsAdd = this.DataRequest_By_DataSet(StrUpdSpName, strKey, strVal);
            if (dsAdd == null)
            {
                return;
            }
            DsRets = dsAdd;
            this.DialogResult = DialogResult.Yes;
        }

        private void repImg_Popup(object sender, EventArgs e)
        {
            if (!(sender is DevExpress.XtraEditors.ImageEdit))
                return;

            DevExpress.XtraEditors.ImageEdit repImg = sender as DevExpress.XtraEditors.ImageEdit;
            DevExpress.XtraGrid.Views.Grid.GridView gv = (repImg.Parent as DevExpress.XtraGrid.GridControl).MainView as DevExpress.XtraGrid.Views.Grid.GridView;

            DataRow _tpDr = gv.GetDataRow(gv.FocusedRowHandle);

            if (_tpDr["Icon"].Equals(System.DBNull.Value))
            {
                byte[] _tpBytes = ServerRefManager.PicFileRead(_tpDr["StylePic"].ToString(), _tpDr["Pic_Version"].ToString());
                gv.FocusedColumn = gv.Columns["Icon"];
                gv.ShowEditor();
                if (gv.ActiveEditor is DevExpress.XtraEditors.ImageEdit)
                {
                    if (repImg.Properties.ShowPopupShadow == false)
                    {
                        repImg.ShowPopup();
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
                gv.RefreshRow(gv.FocusedRowHandle);
                repImg.ShowPopup();
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int k = msg.WParam.ToInt32();
            if (k == 27)//Esc
            {
                this.DialogResult = DialogResult.No;
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}