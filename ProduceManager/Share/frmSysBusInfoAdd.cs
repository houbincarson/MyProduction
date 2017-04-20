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
    public partial class frmSysBusInfoAdd : frmEditorBase
    {
        #region private Params
        private DataSet dsLoad = null;
        private DataTable dtConst = null;
        private DataTable dtShow = null;
        private bool blInitBound = false;
        private string strSpName = string.Empty;
        private string strKeyId = string.Empty;
        private DataRow drBtn = null;
        private string[] strFileds = null;
        public string[] StrFileds
        {
            get { return strFileds; }
            set { strFileds = value; }
        }
        #endregion

        public frmSysBusInfoAdd(DataRow drBtn,string strKeyId)
        {
            InitializeComponent();
            this.strKeyId = strKeyId == string.Empty ? "-1" : strKeyId;
            this.drBtn = drBtn;
            strSpName = drBtn["AddFromOrdSpName"].ToString();
            this.Text = drBtn["AddFromOrdClassText"].ToString();

            InitContr();
        }
        private void InitContr()
        {
            if (dsLoad != null)
                return;

            dsLoad = this.GetFrmLoadDs(drBtn["AddFromOrdClassName"].ToString());
            dsLoad.AcceptChanges();
            dtShow = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];

            if (drBtn["AddFromOrdShowIcon"].ToString() == "True")
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
                gridCol.OptionsColumn.FixedWidth = true;
                gridCol.Visible = true;
                gridCol.VisibleIndex = 0;
                gridCol.Width = 50;

                this.gridCInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                    repImg});
                gridVInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridCol });
            }
            else if (drBtn["AddFromOrdShowPic"].ToString() == "True")
            {
                GridColumn gridCol = new GridColumn();
                RepositoryItemPictureEdit repPic = new RepositoryItemPictureEdit();
                repPic.Name = "gridVInfo_repPic";
                repPic.ReadOnly = true;
                repPic.ShowMenu = false;
                repPic.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
                repPic.MouseEnter += new System.EventHandler(this.repPic_MouseEnter);
                repPic.MouseLeave += new System.EventHandler(this.repPic_MouseLeave);
                repPic.MouseHover += new EventHandler(repPic_MouseHover);

                gridCol.Caption = "图片";
                gridCol.ColumnEdit = repPic;
                gridCol.FieldName = "Icon";
                gridCol.Name = "gridVInfo_GCol";
                gridCol.OptionsColumn.AllowMove = false;
                gridCol.OptionsColumn.ReadOnly = true;
                gridCol.OptionsColumn.FixedWidth = true;
                gridCol.Visible = true;
                gridCol.VisibleIndex = 0;
                gridCol.Width = int.Parse(drBtn["AddFromOrdClassPicColWidth"].ToString());

                this.gridCInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                    repPic});
                gridVInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridCol });

                this.gridVInfo.RowHeight = int.Parse(drBtn["AddFromOrdClassGvRowHeight"].ToString());
            }
            this.gridVInfo.OptionsView.ShowFooter = drBtn["AddFromOrdClassGvShowFoot"].ToString() == "True";
            this.gridVInfo.OptionsView.ShowAutoFilterRow = drBtn["AddFromOrdClassGvShowFilter"].ToString() == "True";
            this.gridVInfo.OptionsView.ShowViewCaption = drBtn["AddFromOrdClassGvShowCaption"].ToString() == "True";
            this.gridVInfo.ViewCaption = drBtn["AddFromOrdViewCaption"].ToString() + " ";

            this.picEdit.Location = new System.Drawing.Point(int.Parse(drBtn["AddFromOrdClassPicX"].ToString()), int.Parse(drBtn["AddFromOrdClassPicY"].ToString()));
            this.picEdit.Visible = drBtn["ShowPicEditAllow"].ToString() == "True";
            StaticFunctions.ShowGridControl(gridVInfo, dtShow, dtConst, out strFileds);

            if (drBtn["AddFromOrdShowSetBtn"].ToString() == "True")
            {
                btnSetAll.Visible = true;
                btnSetAll.Text = drBtn["AddFromOrdClassSBtnText"].ToString();
                btnSetAll.Location = new System.Drawing.Point(int.Parse(drBtn["AddFromOrdClassSBtnPX"].ToString()), int.Parse(drBtn["AddFromOrdClassSBtnPY"].ToString()));
                btnSetAll.Size = new System.Drawing.Size(int.Parse(drBtn["AddFromOrdClassSBtnSW"].ToString()), int.Parse(drBtn["AddFromOrdClassSBtnSH"].ToString()));

                btnClearAll.Visible = true;
                btnClearAll.Text = drBtn["AddFromOrdClassCBtnText"].ToString();
                btnClearAll.Location = new System.Drawing.Point(int.Parse(drBtn["AddFromOrdClassCBtnPX"].ToString()), int.Parse(drBtn["AddFromOrdClassCBtnPY"].ToString()));
                btnClearAll.Size = new System.Drawing.Size(int.Parse(drBtn["AddFromOrdClassCBtnSW"].ToString()), int.Parse(drBtn["AddFromOrdClassCBtnSH"].ToString()));
            }

            int igcHeight;
            Rectangle rect = SystemInformation.VirtualScreen;
            List<Control> lisGcContrs = StaticFunctions.ShowGroupControl(gcQuery, rect.Width - 50, dtShow, dtConst, true, 30, false, null, true, out igcHeight);
            if (lisGcContrs.Count == 0)
            {
                gcQuery.Visible = false;
            }
            else
            {
                gcQuery.Visible = true;
            }
        }

        private void frmSelectShop_Load(object sender, EventArgs e)
        {
            if (!gcQuery.Visible)
                DoQuery();
        }
        private void DoQuery()
        {
            if (gcQuery.Visible)
            {
                string strSpParmName = string.Empty;
                List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(gcQuery, dtShow, out strSpParmName);

                if (strSpParmName != string.Empty)
                    strSpParmName += ",";
                string[] strKey = (strSpParmName + "Ord_Id,EUser_Id,EDept_Id,Fy_Id,flag").Split(",".ToCharArray());
                lisSpParmValue.AddRange(new string[] {strKeyId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     drBtn["AddFromOrdSpLoadFlag"].ToString()});
                DataTable dtTemp = this.DataRequest_By_DataTable(strSpName, strKey, lisSpParmValue.ToArray());
                if (dtTemp == null)
                    return;

                if (frmDataTable == null)
                {
                    frmDataTable = dtTemp;
                }
                else
                {
                    foreach (DataRow drTemp in dtTemp.Rows)
                    {
                        DataRow drNew = frmDataTable.NewRow();
                        drNew.ItemArray = drTemp.ItemArray;
                        frmDataTable.Rows.InsertAt(drNew, 0);
                    }
                }
                if (!frmDataTable.Columns.Contains("Icon"))
                {
                    DataColumn newColumn = frmDataTable.Columns.Add("Icon", Type.GetType("System.Byte[]"));
                    newColumn.AllowDBNull = true;
                }
                if (drBtn["AddFromOrdShowPic"].ToString() == "True")
                {
                    foreach (DataRow dr in frmDataTable.Rows)
                    {
                        dr["Icon"] = ServerRefManager.PicFileRead(dr["StylePic"].ToString(), dr["Pic_Version"].ToString());
                    }
                }
                blInitBound = true;
                frmDataTable.AcceptChanges();
                gridCInfo.DataSource = frmDataTable.DefaultView;
                gridVInfo.BestFitColumns();
                blInitBound = false;

                if (drBtn["ShowPicEditAllow"].ToString() == "True")
                {
                    DataRow dr = frmDataTable.Rows[0];
                    picEdit.EditValue = ServerRefManager.PicFileRead(dr["StylePic"].ToString(), dr["Pic_Version"].ToString());
                }
            }
            else
            {
                string[] strKey = "Ord_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                string[] strVal = new string[] {strKeyId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     drBtn["AddFromOrdSpLoadFlag"].ToString() };
                frmDataTable = this.DataRequest_By_DataTable(strSpName, strKey, strVal);
                DataColumn newColumn = frmDataTable.Columns.Add("Icon", Type.GetType("System.Byte[]"));
                newColumn.AllowDBNull = true;

                if (drBtn["AddFromOrdShowPic"].ToString() == "True")
                {
                    foreach (DataRow dr in frmDataTable.Rows)
                    {
                        dr["Icon"] = ServerRefManager.PicFileRead(dr["StylePic"].ToString(), dr["Pic_Version"].ToString());
                    }
                }
                blInitBound = true;
                frmDataTable.AcceptChanges();
                gridCInfo.DataSource = frmDataTable.DefaultView;
                gridVInfo.BestFitColumns();
                blInitBound = false;

                gridCInfo.Select();
                gridVInfo.FocusedColumn = gridVInfo.Columns["AmountWait"];
                gridVInfo.ShowEditor();
            }
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
        private void repPic_MouseHover(object sender, EventArgs e)
        {
            DataRow dr = gridVInfo.GetFocusedDataRow();
            if (dr == null)
                return;

            picEdit.EditValue = dr["Icon"];
            picEdit.Visible = true;
        }
        private void repPic_MouseLeave(object sender, EventArgs e)
        {
            picEdit.Visible = false;
        }
        private void repPic_MouseEnter(object sender, EventArgs e)
        {
            DataRow dr = gridVInfo.GetFocusedDataRow();
            if (dr == null)
                return;

            picEdit.EditValue = dr["Icon"];
            picEdit.Visible = true;
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
            else if (k == 13)
            {
                if (gcQuery.Visible)
                {
                    DoQuery();
                }
                else
                {
                    gridVInfo.MoveNext();
                    gridVInfo.FocusedColumn = gridVInfo.Columns["AmountWait"];
                    gridVInfo.ShowEditor();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;
            switch (btn.Name)
            {
                case "btnCancel":
                    this.DialogResult = DialogResult.No;
                    break;
                case "btnOk":
                    DoOk();
                    break;
                case "btnSetAll":
                    SetOrClear("Set");
                    break;
                case "btnClearAll":
                    SetOrClear("Clear");
                    break;

                default:
                    break;
            }
        }
        private void SetOrClear(string strFlag)
        {
            string strAddFromOrdClassSetDestFields = drBtn["AddFromOrdClassSetDestFields"].ToString();
            if (strAddFromOrdClassSetDestFields == string.Empty)
                return;

            string[] strFieldDests = strAddFromOrdClassSetDestFields.Split(",".ToCharArray());
            blInitBound = true;
            for (int i = 0; i < gridVInfo.RowCount; i++)
            {
                DataRow dr = gridVInfo.GetDataRow(i);
                if (strFlag == "Set")
                {
                    string[] strFieldSrcs = drBtn["AddFromOrdClassSetSrcFields"].ToString().Split(",".ToCharArray());
                    for (int j = 0; j < strFieldDests.Length; j++)
                    {
                        dr[strFieldDests[j]] = dr[strFieldSrcs[j]];
                    }
                }
                else if (strFlag == "Clear")
                {
                    foreach (string strFieldV in strFieldDests)
                    {
                        dr[strFieldV] = DBNull.Value;
                    }
                }
            }
            blInitBound = false;
        }
        private void DoOk()
        {
            if (frmDataTable == null)
                return;

            frmDataTable.AcceptChanges();
            if (frmDataTable.Rows.Count == 0)
                return;

            if (!DoCheck())
                return;

            this.DialogResult = DialogResult.Yes;
        }
        private void gridVInfo_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0 || blInitBound)
                return;

            DataRow dr = gridVInfo.GetFocusedDataRow();
            if (dr == null)
                return;

            if (!frmDataTable.Columns.Contains("WeightWait") || !frmDataTable.Columns.Contains("Weight"))
                return;

            if (e.Column.FieldName == "AmountWait")
            {
                string strA = Convert.ToString(e.Value) == string.Empty ? "0" : e.Value.ToString();
                int iA = 0;
                if (int.TryParse(strA, out iA))
                {
                    dr["WeightWait"] = iA * int.Parse(dr["Weight"].ToString());
                    dr.EndEdit();
                }
            }
        }

        #region 可能需要扩充的事件
        private bool DoCheck()
        {
            string strBusClassName = drBtn["AddFromOrdClassName"].ToString();
            if (strBusClassName.ToUpper() == "frmSys_OutCpOrdersInfoAdd".ToUpper())
            {
                return DoCheck_frmSys_OutCpOrdersInfoAdd();
            }
            else if (strBusClassName.ToUpper() == "frmSys_OutOrdersInfoAdd".ToUpper())
            {
                return DoCheck_frmSys_OutOrdersInfoAdd();
            }
            return true;
        }
        private bool DoCheck_frmSys_OutCpOrdersInfoAdd()
        {
            foreach (DataRow dr in frmDataTable.Rows)
            {
                int iw = dr["AmountWait"].ToString() == string.Empty ? 0 : int.Parse(dr["AmountWait"].ToString());
                int iO = dr["Total_AmountCpNotIn"].ToString() == string.Empty ? 0 : int.Parse(dr["Total_AmountCpNotIn"].ToString());

                if (iw != 0 && iw > iO)
                {
                    MessageBox.Show("收货数不能大于订购数.");
                    return false;
                }
            }
            return true;
        }
        private bool DoCheck_frmSys_OutOrdersInfoAdd()
        {
            foreach (DataRow dr in frmDataTable.Rows)
            {
                int iw = dr["AmountWait"].ToString() == string.Empty ? 0 : int.Parse(dr["AmountWait"].ToString());
                int iO = dr["NoOutAmount"].ToString() == string.Empty ? 0 : int.Parse(dr["NoOutAmount"].ToString());
                if (iw != 0)
                {
                    if (iw > iO)
                    {
                        MessageBox.Show("出货数不能大于订购数.");
                        return false;
                    }
                    if (dr["CertificateTag"].ToString() == string.Empty)
                    {
                        MessageBox.Show("证书标志不能为空，请先在实物金模块里维护对应.");
                        return false;
                    }
                    string strCertificateStInt = dr["CertificateStInt"].ToString();
                    if (strCertificateStInt == string.Empty)
                    {
                        MessageBox.Show("起始证书编号不能为空.");
                        return false;
                    }
                    if (strCertificateStInt.Length != 8)
                    {
                        MessageBox.Show("证书编号位数只能输入8位.");
                        return false;
                    }
                    Int64 iSt = 0;
                    if (!Int64.TryParse(strCertificateStInt, out iSt))
                    {
                        MessageBox.Show("证书编号必须为整数.");
                        return false;
                    }
                    if (iSt <= 0 || iSt + iw > 99999999)
                    {
                        MessageBox.Show("证书编号输入超出范围.");
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
    }
}