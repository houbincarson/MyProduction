using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Nodes;

namespace ProduceManager
{
    public partial class UcTreeList : UserControl
    {
        private bool blInitBound = false;
        private bool blReGetNode = true;

        public UcTreeList()
        {
            InitializeComponent();
            onClosePopUp += new ClosePopUp(this._onClosePopUp);
        }
        protected virtual void _onClosePopUp(object sender)
        {
        }
        private string _TextData = string.Empty;
        private string _ValueData = string.Empty;
        private string _NullValue = string.Empty;
        public string NullValue
        {
            get { return _NullValue; }
            set { _NullValue = value; }
        }

        public delegate void ClosePopUp(object sender);
        public event ClosePopUp onClosePopUp;
        private void HadClosePopUp()
        {
            onClosePopUp(this);
        }

        [Category("Properties"), Browsable(true), Bindable(true)]
        public DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit Properties
        {
            get { return this.dplCombox.Properties; }
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
            get
            {
                return treeList1.KeyFieldName;
            }
            set
            {
                treeList1.KeyFieldName = value;
            }
        }
        /// <summary>
        /// 父主键字段
        /// </summary>
        [Category("Data"), Browsable(true), Bindable(true)]
        public string ParentValueFieldName
        {
            get
            {
                return treeList1.ParentFieldName;
            }
            set
            {
                treeList1.ParentFieldName = value;
            }
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
                if (dtCust != null && _ValueData != string.Empty)
                {
                    string strValues = string.Empty;
                    string strTxts = GetDisplayDataByValue(_ValueData, out strValues);
                    blInitBound = true;
                    this.dplCombox.EditValue = strTxts;
                    blInitBound = false;
                    this._TextData = strTxts;
                    this._ValueData = strValues;
                    return;
                }
                blInitBound = true;
                this.dplCombox.EditValue = string.Empty;
                blInitBound = false;
                this._TextData = string.Empty;
                this._ValueData = _NullValue;
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
            this.treeList1.Columns.Clear();
            for (int i = 0; i < this.strFieldCodeDiction.Length; i++)
            {
                DevExpress.XtraTreeList.Columns.TreeListColumn col = this.treeList1.Columns.Add();
                col.Visible = true;
                col.Caption = this.strFieldCaptionDiction[i];
                col.FieldName = this.strFieldCodeDiction[i];
                col.Name = this.strFieldCodeDiction[i];
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
        private string GetDisplayDataByValue(string strValueData, out string strValData)
        {
            if (dtCust == null)
            {
                strValData = string.Empty;
                return string.Empty;
            }

            string strTexts = string.Empty;
            strValData = string.Empty;
            string strFilterOld = dtCust.DefaultView.RowFilter;
            string strFilterCur = strValueData.IndexOf(',') != -1 ? ValueFieldName + " IN (" + strValueData + ")" : ValueFieldName + "=" + strValueData;
            dtCust.DefaultView.RowFilter = strFilterCur;
            foreach (DataRowView dr in dtCust.DefaultView)
            {
                strValData += strValData == string.Empty ? dr[ValueFieldName].ToString() : "," + dr[ValueFieldName].ToString();
                strTexts += strTexts == string.Empty ? dr[DisplayFieldName].ToString() : "," + dr[DisplayFieldName].ToString();
            }
            dtCust.DefaultView.RowFilter = strFilterOld;
            return strTexts;
        }
        private void DisplayData()
        {
            if (dtCust == null)
                return;

            string strFilterAll = GetCtrlRowFilter();
            dtCust.DefaultView.RowFilter = strFilterAll;
            this.treeList1.DataSource = dtCust.DefaultView;
            this.treeList1.Refresh();
            this.treeList1.BestFitColumns();
        }

        private void dplCombox_Popup(object sender, EventArgs e)
        {
            blInitBound = true;
            ckbSelect.Checked = false;
            blInitBound = false;

            DisplayData();
            SetTreeListChecked();
        }
        private void dplCombox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (IsPopupOpen)
                    this.dplCombox.ClosePopup();
            }
        }
        private void SetTreeListChecked()
        {
            if (_ValueData == _NullValue)
                return;

            if (dtCust == null)
                return;
            
            for (int i = 0; i < this.treeList1.Nodes.Count; i++)
            {
                TreeListNode node = treeList1.Nodes[i];
                node.CheckState = CheckState.Unchecked;
                SetCheckedChildNodes(node, CheckState.Unchecked);
            }
            Dictionary<string, TreeListNode> dicPNode = new Dictionary<string, TreeListNode>();
            string[] strKeyIds = _ValueData.Split(",".ToArray());
            foreach (string keyId in strKeyIds)
            {
                TreeListNode node = null;
                string strType = this.dtCust.Columns[ValueFieldName].DataType.ToString();
                if (strType == "System.String")
                {
                    node = treeList1.FindNodeByKeyID(keyId);
                }
                else if (strType == "System.Byte")
                {
                    node = treeList1.FindNodeByKeyID(Convert.ToByte(keyId));
                }
                else if (strType == "System.Int16")
                {
                    node = treeList1.FindNodeByKeyID(Convert.ToInt16(keyId));
                }
                else if (strType == "System.Int32")
                {
                    node = treeList1.FindNodeByKeyID(Convert.ToInt32(keyId));
                }
                else if (strType == "System.Int64")
                {
                    node = treeList1.FindNodeByKeyID(Convert.ToInt64(keyId));
                }
                else if (strType == "System.Decimal")
                {
                    node = treeList1.FindNodeByKeyID(Convert.ToDecimal(keyId));
                }
                else if (strType == "System.ToDouble")
                {
                    node = treeList1.FindNodeByKeyID(Convert.ToDouble(keyId));
                }
                if (node != null && !node.HasChildren)
                {
                    node.CheckState = CheckState.Checked;
                    DataRow dr = (treeList1.GetDataRecordByNode(node) as DataRowView).Row;
                    if (!dicPNode.Keys.Contains(dr[ParentValueFieldName].ToString()))
                    {
                        dicPNode.Add(dr[ParentValueFieldName].ToString(), node);
                    }
                }
            }
            foreach (TreeListNode node in dicPNode.Values)
            {
                SetCheckedParentNodes(node, CheckState.Checked);
            }
        }

        private void dplCombox_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            if (blReGetNode)
            {
                GetTreeListChecked();
                blReGetNode = false;
            }
            HadClosePopUp();
        }
        private void GetTreeListChecked()
        {
            string strValData = string.Empty;
            string strTexts = string.Empty;

            for (int i = 0; i < this.treeList1.Nodes.Count; i++)
            {
                TreeListNode node = treeList1.Nodes[i];
                GetTreeListNodeChecked(node, ref strValData, ref strTexts);
            }

            blInitBound = true;
            this.dplCombox.EditValue = strTexts;
            blInitBound = false;
            this._TextData = strTexts;
            this._ValueData = strValData;
        }
        private void GetTreeListNodeChecked(TreeListNode node, ref string strValData, ref string strTexts)
        {
            if (node.HasChildren)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    GetTreeListNodeChecked(node.Nodes[i], ref strValData, ref strTexts);
                }
            }
            else if (node.CheckState == CheckState.Checked)
            {
                DataRow dr = (treeList1.GetDataRecordByNode(node) as DataRowView).Row;
                strValData += strValData == string.Empty ? dr[ValueFieldName].ToString() : "," + dr[ValueFieldName].ToString();
                strTexts += strTexts == string.Empty ? dr[DisplayFieldName].ToString() : "," + dr[DisplayFieldName].ToString();
            }
        }

        private void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            blReGetNode = true;
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }
        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }
        private void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode == null)
                return;

            CheckState parentCheckState = node.ParentNode.CheckState;
            CheckState nodeCheckState;
            for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
            {
                nodeCheckState = (CheckState)node.ParentNode.Nodes[i].CheckState;
                if (!check.Equals(nodeCheckState))
                {
                    parentCheckState = CheckState.Unchecked;
                    break;
                }
                parentCheckState = check;
            }
            node.ParentNode.CheckState = parentCheckState;
            SetCheckedParentNodes(node.ParentNode, check);
        }

        private void ckbSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (blInitBound)
                return;

            for (int i = 0; i < this.treeList1.Nodes.Count; i++)
            {
                TreeListNode node = treeList1.Nodes[i];
                if (ckbSelect.Checked)
                {
                    node.CheckState = CheckState.Checked;
                    SetCheckedChildNodes(node, CheckState.Checked);
                }
                else
                {
                    node.CheckState = CheckState.Unchecked;
                    SetCheckedChildNodes(node, CheckState.Unchecked);
                }
            }
            blReGetNode = true;
        }
        private void btnExp_Click(object sender, EventArgs e)
        {
            treeList1.ExpandAll();
            treeList1.Select();
        }
        private void btnCol_Click(object sender, EventArgs e)
        {
            treeList1.CollapseAll();
            treeList1.Select();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dplCombox.IsPopupOpen)
            {
                dplCombox.ClosePopup();
            }
        }

        public void BindDplComboByTable(DataTable dt,
            string strTxtField,
            string strValueField,
            string[] strfieldNames,
            string[] strHeadTexts,
            string strDataFilter,
            Point pSize)
        {
            this.BindDplComboByTable(dt, strTxtField, strValueField, strfieldNames, strHeadTexts, string.Empty, strDataFilter, pSize);
        }

        public void BindDplComboByTable(DataTable dt,
            string strTxtField,
            string strValueField,
            string[] strfieldNames,
            string[] strHeadTexts,
            string strSort,
            string strDataFilter,
            Point pSize)
        {
            string[] strVals = strValueField.Split("|".ToArray());
            if (strVals.Length != 2)
            {
                throw new Exception("绑定值设置错误，必须为格式：Kind_Id|Parent_Kind_Id");
            }
            ValueFieldName = strVals[0];
            ParentValueFieldName = strVals[1];
            DisplayFieldName = strTxtField;
            strFieldCodeDiction = strfieldNames;
            strFieldCaptionDiction = strHeadTexts;
            popContrSize = pSize;
            InitGvColumns();

            string strFilterOld = dt.DefaultView.RowFilter;
            dt.DefaultView.RowFilter = strDataFilter;
            dt.DefaultView.Sort = strSort;
            dtCust = dt.DefaultView.ToTable();
            dtCust.AcceptChanges();
            dt.DefaultView.RowFilter = strFilterOld;
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
