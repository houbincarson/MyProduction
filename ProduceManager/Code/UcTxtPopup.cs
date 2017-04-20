using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProduceManager
{
    public partial class UcTxtPopup : UserControl
    {
        private bool blInitBound = false;
        public UcTxtPopup()
        {
            InitializeComponent();
            onClosePopUp += new ClosePopUp(this._onClosePopUp);
        }
        protected virtual void _onClosePopUp(object sender, System.Data.DataRow drReturn)
        {
        }
        private DataRow RetrunRow;
        private string _TextData = string.Empty;
        private string _ValueData = string.Empty;
        private string _NullValue = "-9999";
        public string NullValue
        {
            get { return _NullValue; }
            set { _NullValue = value; }
        }

        public delegate void ClosePopUp(object sender, DataRow drReturn);
        public event ClosePopUp onClosePopUp;
        private void HadClosePopUp()
        {
            onClosePopUp(this, this.RetrunRow);
        }

        [Category("Properties"), Browsable(true), Bindable(true)]
        public DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit Properties
        {
            get { return this.dplCombox.Properties; }
        }
        /// <summary>
        /// 返回选择的记录
        /// </summary>
        [Category("Data"), Browsable(true), Bindable(false)]
        public DataRow SelectedRow
        {
            get { return this.RetrunRow; }
        }
        /// <summary>
        /// ReadOnly
        /// </summary>
        [Category("Data"), Browsable(true), Bindable(true)]
        public bool ReadOnly
        {
            get { return this.dplCombox.Properties.ReadOnly; }
            set
            {
                this.dplCombox.Properties.ReadOnly = value;
            }
        }
        /// <summary>
        /// 字段数组描述
        /// </summary>
        [Category("Data"), Browsable(true), Bindable(true)]
        public string[] strFieldCaptionDiction
        {
            get;
            set;
        }
        /// <summary>
        /// 字段数组代码
        /// </summary>
        [Category("Data"), Browsable(true), Bindable(true)]
        public string[] strFieldCodeDiction
        {
            get;
            set;
        }
        /// <summary>
        /// 过滤字段数组代码
        /// </summary>
        [Category("Data"), Browsable(true), Bindable(true)]
        public string[] strFieldFilterDiction
        {
            get;
            set;
        }
        /// <summary>
        /// 显示字段
        /// </summary>
        [Category("Data"), Browsable(true), Bindable(true)]
        public string DisplayFieldName
        {
            get;
            set;
        }
        /// <summary>
        /// 值字段
        /// </summary>
        [Category("Data"), Browsable(true), Bindable(true)]
        public string ValueFieldName
        {
            get;
            set;
        }
        public string TextData
        {
            get
            {
                return _TextData;
            }
        }
        /// <summary>
        /// 控件的返回值
        /// </summary>
        [Category("Data"), Browsable(true), Bindable(true)]
        public object EditValue
        {
            get
            {
                return _ValueData;
            }
            set
            {
                string strV = Convert.ToString(value);
                if (strV == _ValueData)
                    return;

                if (strV == string.Empty)
                    strV = _NullValue;
                if (strV == _ValueData)
                    return;

                _ValueData = strV;
                if (dtCust != null)
                {
                    DataRow[] drs = dtCust.Select(ValueFieldName + "='" + _ValueData + "'");
                    if (drs.Length > 0)
                    {
                        RetrunRow = drs[0];
                        blInitBound = true;
                        this.dplCombox.EditValue = RetrunRow[this.DisplayFieldName].ToString();
                        blInitBound = false;
                        this._TextData = RetrunRow[this.DisplayFieldName].ToString();
                        this._ValueData = RetrunRow[this.ValueFieldName].ToString();
                        //HadClosePopUp();
                        return;
                    }
                }
                RetrunRow = null;
                blInitBound = true;
                this.dplCombox.EditValue = string.Empty;
                blInitBound = false;
                this._TextData = string.Empty;
                this._ValueData = _NullValue;
                //HadClosePopUp();
            }
        }
        public Point popContrSize
        {
            set
            {
                popupContainerControl1.Size = new Size(value);
            }
        }
        public DataTable dtCust
        {
            get;
            set;
        }
        public bool IsPopupOpen
        {
            get
            {
                return dplCombox.IsPopupOpen;
            }
        }
        public bool IsAddNull
        {
            get;
            set;
        }

        public string FilterFieldsOrd
        {
            get;
            set;
        }
        public string FilterFieldsInfo
        {
            get;
            set;
        }
        public DataRow DrFilterFieldsOrd
        {
            get;
            set;
        }
        public DataRow DrFilterFieldsInfo
        {
            get;
            set;
        }

        private void InitGvColumns()
        {
            this.gridVCust.Columns.Clear();
            for (int i = 0; i < this.strFieldCodeDiction.Length; i++)
            {
                DevExpress.XtraGrid.Columns.GridColumn col = new DevExpress.XtraGrid.Columns.GridColumn();
                col = this.gridVCust.Columns.Add();
                col.FieldName = this.strFieldCodeDiction[i];
                col.Name = this.strFieldCodeDiction[i];
                col.Caption = this.strFieldCaptionDiction[i];
                col.VisibleIndex = i;
            }
        }
        private string GetCtrlRowFilter()
        {
            string strFilter = string.Empty;
            if (DrFilterFieldsOrd != null && FilterFieldsOrd.Trim() != string.Empty)
            {
                string[] strFields = FilterFieldsOrd.Trim().Split(",".ToCharArray());
                foreach (string strField in strFields)
                {
                    string strValue = DrFilterFieldsOrd[strField].ToString();
                    if (strValue == string.Empty)
                        continue;

                    int iV = 0;
                    if (int.TryParse(strValue, out iV))
                    {
                        if (iV < 0)
                            continue;
                    }
                    if (strFilter != string.Empty)
                    {
                        strFilter += " AND ";
                    }
                    string strType = dtCust.Columns[strField].DataType.ToString();
                    if (strType == "System.String")
                    {
                        strFilter += "(" + strField + "='" + strValue + "' OR " + strField + " like '%," + strValue + ",%'" + ")";
                    }
                    else
                    {
                        strFilter += strField + "=" + strValue;
                    }
                }
            }
            if (DrFilterFieldsInfo != null && FilterFieldsInfo.Trim() != string.Empty)
            {
                string[] strFields = FilterFieldsInfo.Trim().Split(",".ToCharArray());
                foreach (string strField in strFields)
                {
                    string strValue = DrFilterFieldsInfo[strField].ToString();
                    if (strValue == string.Empty)
                        continue;

                    int iV = 0;
                    if (int.TryParse(strValue, out iV))
                    {
                        if (iV < 0)
                            continue;
                    }
                    if (strFilter != string.Empty)
                    {
                        strFilter += " AND ";
                    }
                    string strType = dtCust.Columns[strField].DataType.ToString();
                    if (strType == "System.String")
                    {
                        strFilter += "(" + strField + "='" + strValue + "' OR " + strField + " like '%" + strValue + ",%'" + ")";
                    }
                    else
                    {
                        strFilter += strField + "=" + strValue;
                    }
                }
            }

            return strFilter;
        }
        private void DisplayData(string strFilterTxt)
        {
            string strFilter = strFilterTxt;
            if (strFilter != string.Empty)
                strFilter = "(" + strFilter + ")";
            string strFilterAll = GetCtrlRowFilter();
            if (strFilterAll != string.Empty && strFilter != string.Empty)
                strFilterAll += " AND ";

            string strFilterApply = strFilterAll + strFilter;
            if (strFilterApply == string.Empty)
            {
                dtCust.DefaultView.RowFilter = strFilterApply;
            }
            else
            {
                strFilterApply = "(" + strFilterApply + ")";
                dtCust.DefaultView.RowFilter = strFilterApply;
                if (dtCust.DefaultView.Count == 0 || strFilterTxt == string.Empty)
                {
                    dtCust.DefaultView.RowFilter = "(" + strFilterApply + " OR " + ValueFieldName + "='" + _NullValue + "')";
                }
            }
            this.gridCCust.DataSource = dtCust.DefaultView;
            this.gridCCust.Refresh();
            this.gridVCust.BestFitColumns();
        }
        private void DoFilterData(string strValue)
        {
            if (dtCust == null)
                return;

            if (strValue.Trim() == string.Empty)
            {
                DisplayData(string.Empty);
                return;
            }

            StringBuilder sbFilters = new StringBuilder();
            foreach (string strFiter in strFieldFilterDiction)
            {
                string strType = dtCust.Columns[strFiter].DataType.ToString();
                if (strType == "System.String")
                {
                    sbFilters.Append(" or " + "(" + strFiter + " like '%" + strValue + "%')");
                }
                else
                {
                    double dValue = 0;
                    if (double.TryParse(strValue, out dValue))
                    {
                        sbFilters.Append(" or " + "(" + strFiter + "=" + strValue + ")");
                    }
                }
            }
            if (sbFilters.Length == 0)
            {
                DisplayData(string.Empty);
            }
            else
            {
                DisplayData(sbFilters.ToString().Remove(0, 4));//移除前面" or "
            }
        }
        private void DoFilterDataByKey(string strKey)
        {
            if (dtCust == null)
                return;

            if (strKey != string.Empty)
            {
                DisplayData(ValueFieldName + "='" + strKey + "'");
            }
            else
            {
                DisplayData(string.Empty);
            }
        }
        private void MoveFocusedDataRowIndexToNone()
        {
            gridVCust.FocusedRowHandle = 0;
            DataRow drFoc = gridVCust.GetFocusedDataRow();
            if (drFoc != null && drFoc[this.ValueFieldName].ToString() == _NullValue)
                return;

            for (int k = 0; k < this.gridVCust.RowCount; k++)
            {
                if (this.gridVCust.GetDataRow(k)[this.ValueFieldName].ToString() == _NullValue)
                {
                    this.gridVCust.MoveBy(k);
                    return;
                }
            }
        }
        private void MoveFocusedDataRowIndexToKey(string strKey)
        {
            DataRow drFoc = gridVCust.GetFocusedDataRow();
            if (drFoc != null && drFoc[this.ValueFieldName].ToString() == strKey)
                return;

            gridVCust.FocusedRowHandle = 0;
            for (int k = 0; k < this.gridVCust.RowCount; k++)
            {
                if (this.gridVCust.GetDataRow(k)[this.ValueFieldName].ToString() == strKey)
                {
                    this.gridVCust.MoveBy(k);
                    return;
                }
            }
        }

        private void dplCombox_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (blInitBound)
                return;

            if (!dplCombox.IsPopupOpen)
            {
                blInitBound = true;
                dplCombox.ShowPopup();
                blInitBound = false;
            }
            string strValue = Convert.ToString(e.NewValue);
            DoFilterData(strValue);
            if (strValue == string.Empty)
            {
                MoveFocusedDataRowIndexToNone();
            }
        }
        private void dplCombox_Popup(object sender, EventArgs e)
        {
            this.dplCombox.Focus();
            if (!blInitBound)
                this.dplCombox.SelectAll();

            DoFilterDataByKey(string.Empty);
            if (_ValueData == string.Empty || _ValueData == _NullValue)
            {
                MoveFocusedDataRowIndexToNone();
            }
            else
            {
                MoveFocusedDataRowIndexToKey(_ValueData);
            }
        }
        private void dplCombox_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            SelectRow();
            HadClosePopUp();
        }
        private void SelectRow()
        {
            RetrunRow = gridVCust.GetFocusedDataRow();
            if (RetrunRow != null)
            {
                blInitBound = true;
                this.dplCombox.EditValue = RetrunRow[this.DisplayFieldName].ToString();
                blInitBound = false;
                this._TextData = RetrunRow[this.DisplayFieldName].ToString();
                this._ValueData = RetrunRow[this.ValueFieldName].ToString();
            }
            else
            {
                RetrunRow = null;
                blInitBound = true;
                this.dplCombox.EditValue = string.Empty;
                blInitBound = false;
                this._TextData = string.Empty;
                this._ValueData = string.Empty;
            }
        }

        private void dplCombox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (IsPopupOpen)
                    this.dplCombox.ClosePopup();
            }
            else if (e.KeyCode == Keys.Down)
            {
                this.gridVCust.MoveNext();
            }
            else if (e.KeyCode == Keys.Up)
            {
                this.gridVCust.MovePrev();
            }
        }
        private void UcTxtPopup_Enter(object sender, EventArgs e)
        {
            this.dplCombox.Focus();
            this.dplCombox.SelectAll();
        }
        private void UcTxtPopup_MouseClick(object sender, MouseEventArgs e)
        {
            this.dplCombox.Focus();
            this.dplCombox.SelectAll();
        }
        private void UcTxtPopup_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.dplCombox.Focus();
            this.dplCombox.SelectAll();
        }
        private void dplCombox_Enter(object sender, EventArgs e)
        {
            this.dplCombox.Focus();
            this.dplCombox.SelectAll();
        }
        private void gridVCust_Click(object sender, EventArgs e)
        {
            this.dplCombox.ClosePopup();
        }
        private void gridVCust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.dplCombox.ClosePopup();
        }

        public void BindDplComboByTable(DataTable dt,
            string strTxtField,
            string strValueField,
            string[] strfieldNames,
            string[] strHeadTexts,
            string[] strFilter,
            string strSort,
            string strNullValue,
            string strDataFilter,
            Point pSize,
            bool blAddNull)
        {
            ValueFieldName = strValueField;
            DisplayFieldName = strTxtField;
            strFieldFilterDiction = strFilter;
            strFieldCodeDiction = strfieldNames;
            strFieldCaptionDiction = strHeadTexts;
            NullValue = strNullValue;
            IsAddNull = blAddNull;
            popContrSize = pSize;
            InitGvColumns();

            string strFilterOld = dt.DefaultView.RowFilter;
            dt.DefaultView.RowFilter = strDataFilter;
            dtCust = dt.DefaultView.ToTable();
            if (blAddNull)
            {
                DataRow dr = dtCust.NewRow();
                dr[strTxtField] = "";
                dr[strValueField] = _NullValue;
                dtCust.Rows.InsertAt(dr,0);
            }
            dtCust.AcceptChanges();
            dtCust.DefaultView.Sort = strSort;
            dt.DefaultView.RowFilter = strFilterOld;

            DisplayData(string.Empty);
        }

        public void ShowPopup()
        {
            if (!dplCombox.IsPopupOpen)
            {
                dplCombox.ShowPopup();
            }
        }
        public void ClosePopup()
        {
            if (dplCombox.IsPopupOpen)
            {
                dplCombox.ClosePopup();
            }
        }
    }
}
