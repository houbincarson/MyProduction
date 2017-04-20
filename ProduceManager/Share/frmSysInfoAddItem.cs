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
using System.IO;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;

namespace ProduceManager
{
    public partial class frmSysInfoAddItem : frmEditorBase
    {
        #region private Params
        private bool blSysProcess = false;
        private bool blInitBound = false;
        private DataSet dsLoad = null;
        private DataTable dtConst = null;
        private DataTable dtShow = null;
        private DataRow drBtn = null;
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
        private string[] strFileds = null;
        public string[] StrFileds
        {
            get { return strFileds; }
            set { strFileds = value; }
        }

        private DataTable dtInfo = null;
        private DataTable dtInfoInfo = null;
        private GridView gvChild = null;
        private string[] strFiledsInfo = null;
        private bool IsAddChildGv = false;

        #endregion

        public frmSysInfoAddItem(DataRow drBtn)
        {
            InitializeComponent();
            this.drBtn = drBtn;
            this.Text = drBtn["FrmClassText"].ToString();

            int iSizeX = int.Parse(drBtn["FrmSizeX"].ToString());
            if (iSizeX != 0)
            {
                this.Size = new Size(iSizeX, int.Parse(drBtn["FrmSizeY"].ToString()));
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }

            if (drBtn["FrmSaveFilter"].ToString() == string.Empty)
            {
                btnPreview.Enabled = false;
            }
            if (drBtn["SelUpdateFields"].ToString() == string.Empty || drBtn["NoSelUpdateFields"].ToString() == string.Empty)
            {
                this.btnChkItem.Enabled = false;
            }

            InitContr();
        }
        private void InitContr()
        {
            if (dsLoad != null)
                return;

            dsLoad = this.GetFrmLoadDs(drBtn["FrmClassName"].ToString());
            dsLoad.AcceptChanges();
            dtShow = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];
            if (drBtn["FrmShowIcon"].ToString() == "True")
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
            else if (drBtn["FrmShowPicEdit"].ToString() == "True")
            {
                GridColumn gridCol = new GridColumn();
                RepositoryItemPictureEdit repPic = new RepositoryItemPictureEdit();
                repPic.Name = "gridVInfo_repPic";
                repPic.ReadOnly = true;
                repPic.ShowMenu = false;
                repPic.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
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
                gridCol.Width = int.Parse(drBtn["FrmPicGvWidth"].ToString());

                this.gridCInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                    repPic});
                gridVInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridCol });

                this.gridVInfo.RowHeight = int.Parse(drBtn["FrmPicGvRowHeight"].ToString());
                this.gridVInfo.OptionsView.ShowAutoFilterRow = false;
                this.picEdit.Location = new System.Drawing.Point(int.Parse(drBtn["FrmPicX"].ToString()), int.Parse(drBtn["FrmPicY"].ToString()));
            }
            Rectangle rect = SystemInformation.VirtualScreen;
            StaticFunctions.ShowGridControl(gridVInfo, dtShow, dtConst, out strFileds);
            IsAddChildGv = this.drBtn["IsAddChildGv"].ToString() == "True";
            if (IsAddChildGv)
            {
                gvChild = StaticFunctions.ShowGridVChildGv("gridVCom", gridCInfo, dtShow, dtConst, out strFiledsInfo);
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int k = msg.WParam.ToInt32();
            if (k == 27)//Esc
            {
                this.DialogResult = DialogResult.No;
                this.Close();
                this.Dispose();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmSysInfoAddItem_Load(object sender, EventArgs e)
        {
            CApplication.CurrForm = this;
            CApplication.Com_Prot.OnPortDataReceived += SetText;
            DoQuery();
        }
        private void DoQuery()
        {
            List<string> lisSpParmValue = new List<string>();
            string[] strKey = "Ord_Id,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {StrOrdKeyId,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     drBtn["FrmQueryFlag"].ToString()});
            DataSet dsTemp = this.DataRequest_By_DataSet(StrUpdSpName, strKey, lisSpParmValue.ToArray());
            if (dsTemp == null)
                return;

            frmDataTable = dsTemp.Tables[0];
            dtInfo = frmDataTable;
            DataColumn newColumn = frmDataTable.Columns.Add("Icon", Type.GetType("System.Byte[]"));
            newColumn.AllowDBNull = true;
            if (drBtn["FrmShowPicEdit"].ToString() == "True")
            {
                foreach (DataRow dr in frmDataTable.Rows)
                {
                    dr["Icon"] = ServerRefManager.PicFileRead(dr["StylePic"].ToString(), dr["Pic_Version"].ToString());
                }
            }
            if (IsAddChildGv && dsTemp.Tables.Count == 2)
            {
                dtInfoInfo = dsTemp.Tables[1];
                dtInfo.RowChanging += new DataRowChangeEventHandler(dtInfo_RowChanging);
                dtInfoInfo.RowChanging += new DataRowChangeEventHandler(dtInfoInfo_RowChanging);
                string strRelationsKeyId = drBtn["RelationsKeyId"].ToString();
                dsTemp.Relations.Add("gridCInfoChildGrid", frmDataTable.Columns[strRelationsKeyId], dtInfoInfo.Columns[strRelationsKeyId]);
            }
            frmDataTable.AcceptChanges();
            gridCInfo.DataSource = frmDataTable.DefaultView;
            gridVInfo.BestFitColumns();

            blInitBound = true;
            btnChkItem.Checked = false;
            btnPreview.Checked = false;
            frmDataTable.DefaultView.RowFilter = string.Empty;
            blInitBound = false;
        }

        private void dtInfo_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action != DataRowAction.Change)
                return;

            if (dtInfoInfo == null || dtInfoInfo.Rows.Count == 0)
                return;

            if (drBtn["UpdFields"].ToString() == string.Empty)
                return;

            string strRelationsKeyId = drBtn["RelationsKeyId"].ToString();
            string[] strFieldVs = drBtn["UpdFields"].ToString().Split(",".ToCharArray());
            DataRow dr = e.Row;
            DataRow[] drs = dtInfoInfo.Select(strRelationsKeyId + "=" + dr[strRelationsKeyId].ToString());
            if (drs.Length == 1)
            {
                dtInfoInfo.RowChanging -= new DataRowChangeEventHandler(dtInfoInfo_RowChanging);
                DataRow drDest = drs[0];
                for (int i = 0; i < strFieldVs.Length; i++)
                {
                    string strFieldV = strFieldVs[i];
                    if (dtInfoInfo.Columns.Contains(strFieldV))
                        drDest[strFieldV] = dr[strFieldV];
                }
                dtInfoInfo.RowChanging += new DataRowChangeEventHandler(dtInfoInfo_RowChanging);
            }
            else if (dtInfo.Columns.Contains("UkyndaFieldDest")
                && dtInfo.Columns.Contains("UkyndaFieldSrc")
                && dtInfo.Columns.Contains("UkyndaEqualFlag"))
            {
                if (dr["UkyndaEqualFlag"].ToString() != "1")
                    return;

                string strFieldDest = dr["UkyndaFieldDest"].ToString();
                string strFieldSrc = dr["UkyndaFieldSrc"].ToString();
                double dSrc = dr[strFieldSrc].ToString() == string.Empty ? 0 : StaticFunctions.Round(double.Parse(dr[strFieldSrc].ToString()), 4, 0.5);
                double dDest = dr[strFieldDest].ToString() == string.Empty ? 0 : StaticFunctions.Round(double.Parse(dr[strFieldDest].ToString()), 4, 0.5);
                bool blSetAll = ((int)(dSrc * 10000) <= (int)(dDest * 10000));

                List<string> lisFields = new List<string>();
                List<string> lisValues = new List<string>();
                string[] strFields = drBtn["SelUpdateFields"].ToString().Split(",".ToCharArray());
                foreach (string strSql in strFields)
                {
                    string[] strFieldVSrc = strSql.Split("=".ToCharArray());
                    lisFields.Add(strFieldVSrc[0]);
                    lisValues.Add(strFieldVSrc[1]);
                }
                dtInfoInfo.RowChanging -= new DataRowChangeEventHandler(dtInfoInfo_RowChanging);
                foreach (DataRow dri in drs)
                {
                    for (int i = 0; i < lisFields.Count; i++)
                    {
                        if (blSetAll)
                            dri[lisFields[i]] = dri[lisValues[i]];
                        else
                            dri[lisFields[i]] = DBNull.Value;
                    }
                }
                dtInfoInfo.RowChanging += new DataRowChangeEventHandler(dtInfoInfo_RowChanging);
            }
        }
        private void dtInfoInfo_RowChanging(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action != DataRowAction.Change)
                return;

            if (drBtn["UpdFields"].ToString() == string.Empty)
                return;

            string strRelationsKeyId = drBtn["RelationsKeyId"].ToString();
            string[] strFieldVs = drBtn["UpdFields"].ToString().Split(",".ToCharArray());
            DataRow dr = e.Row;

            Dictionary<string, int> dicV = new Dictionary<string, int>();
            DataRow[] drs = dtInfoInfo.Select(strRelationsKeyId + "=" + dr[strRelationsKeyId].ToString());
            foreach (DataRow drInfo in drs)
            {
                for (int i = 0; i < strFieldVs.Length; i++)
                {
                    string strFieldV = strFieldVs[i];
                    string strValue = drInfo[strFieldV].ToString();
                    if (strValue == string.Empty || strValue == "0")
                        continue;

                    object snw = new DataTable().Compute(strValue + "*10000", null);
                    int Val = (int)(double.Parse(snw.ToString()));
                    if (Val == 0)
                        continue;

                    if (dicV.ContainsKey(strFieldV))
                    {
                        dicV[strFieldV] += Val;
                    }
                    else
                    {
                        dicV.Add(strFieldV, Val);
                    }
                }
            }
            DataRow[] drPs = dtInfo.Select(strRelationsKeyId + "=" + dr[strRelationsKeyId].ToString());
            dtInfo.RowChanging -= new DataRowChangeEventHandler(dtInfo_RowChanging);
            foreach (DataRow drInfo in drPs)
            {
                for (int i = 0; i < strFieldVs.Length; i++)
                {
                    string strFieldV = strFieldVs[i];
                    if (dicV.ContainsKey(strFieldV))
                    {
                        drInfo[strFieldV] = dicV[strFieldV] / 10000.00;
                    }
                }
            }
            dtInfo.RowChanging += new DataRowChangeEventHandler(dtInfo_RowChanging);
        }

        private void DoPostEditor()
        {
            blInitBound = true;
            gridCInfo.EmbeddedNavigator.Buttons.DoClick(gridCInfo.EmbeddedNavigator.Buttons.EndEdit);
            blInitBound = false;
        }
        private void btnChkItem_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (blInitBound)
                return;

            if (frmDataTable == null)
                return;

            if (frmDataTable.Rows.Count == 0)
                return;

            DoChkItem();
        }
        private void DoChkItem()
        {
            DoPostEditor();
            List<string> lisFields = new List<string>();
            List<string> lisValues = new List<string>();
            string strFields = btnChkItem.Checked ? drBtn["SelUpdateFields"].ToString() : drBtn["NoSelUpdateFields"].ToString();
            string[] strFieldVs = strFields.Split(",".ToCharArray());
            foreach (string strSql in strFieldVs)
            {
                string[] strFieldVSrc = strSql.Split("=".ToCharArray());
                lisFields.Add(strFieldVSrc[0]);
                lisValues.Add(strFieldVSrc[1]);
            }
            for (int j = 0; j < gridVInfo.RowCount; j++)
            {
                DataRow dr = gridVInfo.GetDataRow(j);
                for (int i = 0; i < lisFields.Count; i++)
                {
                    dr[lisFields[i]] = dr[lisValues[i]];
                }
            }
            //foreach (DataRow dr in frmDataTable.Rows)
            //{
            //    for (int i = 0; i < lisFields.Count; i++)
            //    {
            //        dr[lisFields[i]] = dr[lisValues[i]];
            //    }
            //}
        }
        private void btnPreview_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (blInitBound)
                return;

            if (frmDataTable == null)
                return;

            DoPostEditor();

            if (btnPreview.Checked)
                frmDataTable.DefaultView.RowFilter = drBtn["FrmSaveFilter"].ToString();
            else
                frmDataTable.DefaultView.RowFilter = string.Empty;
        }

        private void btn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (blSysProcess)
                return;
            try
            {
                blSysProcess = true;
                e.Item.Enabled = false;
                e.Item.Refresh();
                this.Cursor = Cursors.WaitCursor;
                switch (e.Item.Name)
                {
                    case "btnRefresh":
                        DoQuery();
                        break;
                    case "btnQuit":
                        this.Close();
                        this.Dispose();
                        break;
                    case "btnSave":
                        DoOk();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("错误:" + err.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                e.Item.Enabled = true;
                e.Item.Refresh();
                blSysProcess = false;
            }
        }
        private void DoOk()
        {
            if (frmDataTable == null)
                return;

            frmDataTable.AcceptChanges();
            if (frmDataTable.Rows.Count == 0)
                return;

            DoPostEditor();

            string strKeyIds = string.Empty;
            string strSFt = drBtn["FrmSaveFilter"].ToString();
            if (strFileds != null && strFileds.Length > 0)
            {
                strKeyIds = StaticFunctions.GetStringX(strFileds, frmDataTable, strSFt);
            }
            else if (drBtn["FrmInfoKeyId"].ToString() != string.Empty)
            {
                string strKeyF = drBtn["FrmInfoKeyId"].ToString();

                string strSFtOld = frmDataTable.DefaultView.RowFilter;
                frmDataTable.DefaultView.RowFilter = strSFt;
                foreach (DataRowView dr in frmDataTable.DefaultView)
                {
                    strKeyIds += strKeyIds == string.Empty ? dr[strKeyF].ToString() : "," + dr[strKeyF].ToString();
                }
                frmDataTable.DefaultView.RowFilter = strSFtOld;
            }
            if (strKeyIds == string.Empty)
            {
                MessageBox.Show("没有可保存的明细记录.");
                return;
            }
            string strSplitsInfo = string.Empty;
            if (dtInfoInfo != null)
            {
                strSplitsInfo = StaticFunctions.GetStringX(strFiledsInfo, dtInfoInfo, drBtn["FrmSaveFilter"].ToString());
            }
            string[] strKey = null;
            string[] strVal = null;
            if (strSplitsInfo == string.Empty)
            {
                strKey = "Ord_Id,strSplits,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                strVal = new string[] {StrOrdKeyId,strKeyIds,
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(),
                    drBtn["FrmConfirmFlag"].ToString() };
            }
            else
            {
                strKey = "Ord_Id,strSplits,strSplitsInfo,EUser_Id,EDept_Id,Fy_Id,flag".Split(",".ToCharArray());
                strVal = new string[] {StrOrdKeyId,strKeyIds,strSplitsInfo,
                    CApplication.App.CurrentSession.UserId.ToString(),
                    CApplication.App.CurrentSession.DeptId.ToString(),
                    CApplication.App.CurrentSession.FyId.ToString(),
                    drBtn["FrmConfirmFlag"].ToString() };
            }
            DataSet dsAdd = this.DataRequest_By_DataSet(StrUpdSpName, strKey, strVal);
            if (dsAdd == null)
            {
                return;
            }
            DsRets = dsAdd;
            this.DialogResult = DialogResult.Yes;
            this.Close();
            this.Dispose();
        }
        public override void SetText(string text)
        {
            if (gridVInfo.FocusedColumn == null)
                return;

            if (gridVInfo.FocusedColumn.OptionsColumn.ReadOnly)
                return;

            if (!gridVInfo.FocusedColumn.OptionsColumn.AllowEdit)
                return;

            DataRow drFoc = gridVInfo.GetFocusedDataRow();
            if (drFoc == null)
                return;

            drFoc[gridVInfo.FocusedColumn.FieldName] = StaticFunctions.Round(double.Parse(text), 2, 0.5).ToString();
            gridVInfo.UpdateCurrentRow();
        }
    }
}