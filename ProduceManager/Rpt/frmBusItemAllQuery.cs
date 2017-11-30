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

namespace ProduceManager
{
    public partial class frmBusItemAllQuery : frmEditorBase
    {
        private DataSet dsLoad = null;
        private DataTable dtConst = null;
        private DataTable dtShow = null;
        private string strSpName = "Rpt_BusItemAllQuery";

        public frmBusItemAllQuery()
        {
            InitializeComponent();
            InitContr();
        }
        private void InitContr()
        {
            if (dsLoad != null)
                return;

            dsLoad = this.GetFrmLoadDs(this.Name);
            dtShow = dsLoad.Tables[0];
            dtConst = dsLoad.Tables[1];

            Rectangle rect = SystemInformation.VirtualScreen;
            int igcHeight;
            List<Control> lisGcContrs = StaticFunctions.ShowGroupControl(gcQuery, rect.Width - 20, dtShow, dtConst, true, 50, false, null, true,out igcHeight);
            gcTst.Size = new Size(gcTst.Width, igcHeight > 50 ? igcHeight + 21 : 71); 
        }

        private void btn_Click(object sender, EventArgs e)
        {
            SimpleButton btn = sender as SimpleButton;
            switch (btn.Name)
            {
                case "btnQuery":
                    Query();
                    break;
                case "btnExcel":
                    DoPrintOrd();
                    break;

                default:
                    break;
            }
        }
        private void Query()
        {
            if(!StaticFunctions.CheckSave(gcQuery,dtShow))
                return;

            string strSpParmName = string.Empty;
            List<string> lisSpParmValue = StaticFunctions.GetPassSpParmValue(gcQuery, dtShow, out strSpParmName);

            if (strSpParmName != string.Empty)
                strSpParmName += ",";
            string[] strKey = (strSpParmName + "ClassName,EUser_Id,EDept_Id,Fy_Id").Split(",".ToCharArray());
            lisSpParmValue.AddRange(new string[] {this.Name,
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                     });
            DataSet ds = this.DataRequest_By_DataSet(strSpName, strKey, lisSpParmValue.ToArray());
            if (ds==null)
            {
                return;
            }
            DataTable dtShowExcel = ds.Tables[0];
            dtShowExcel.AcceptChanges();

            frmDataTable = ds.Tables[1];
            DataColumn newColumn = frmDataTable.Columns.Add("Icon", Type.GetType("System.Byte[]"));
            newColumn.AllowDBNull = true;
            frmDataTable.AcceptChanges();

            for (int i = gridVInfo.Columns.Count - 1; i >= 0; i--)
            {
                DevExpress.XtraGrid.Columns.GridColumn gc = gridVInfo.Columns[i];
                if (gc.FieldName == "Icon")
                    continue;

                gridVInfo.Columns.Remove(gc);
            }
            //gridVInfo.Columns.Clear();
            StaticFunctions.ShowExcelGridControl(gridVInfo, dtShowExcel.DefaultView);

            gridCInfo.DataSource = frmDataTable.DefaultView;
            gridVInfo.BestFitColumns();
        }
        private void DoPrintOrd()
        {
            if (gridVInfo.RowCount == 0)
                return;

            this.Cursor = Cursors.WaitCursor;
            StaticFunctions.GridViewExportToExcel(gridVInfo, this.Text, null);
            this.Cursor = Cursors.Arrow;
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
    }
}