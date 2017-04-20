using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace ExtendControl
{
  public class ExtendGridView : GridView
  {
    private Dictionary<string, bool> _OrigColumnsReadOnly = new Dictionary<string, bool>();
    public void Set_EditTable(bool val)
    {
      string _field = string.Empty;
      for (int _i = 0, _iCnt = this.Columns.Count; _i < _iCnt; _i++)
      {
        _field = this.Columns[_i].FieldName;
        if (!_OrigColumnsReadOnly.ContainsKey(_field))
        {
          _OrigColumnsReadOnly[_field] = this.Columns[_i].OptionsColumn.ReadOnly;
        }
        if (val)//编辑状态
        {
          this.Columns[_i].OptionsColumn.ReadOnly = _OrigColumnsReadOnly[_field];
        }
        else
        {
          this.Columns[_i].OptionsColumn.ReadOnly = true;
        }
      }
      _EditTable = val;
    }
    public bool Get_EditTable(bool val){
        return _EditTable;
    }
    private bool _EditTable;
    public bool LockFocuseRow
    {
      get
      {
        return _LockFocuseRow;
      }
      set
      {
        if (value)
        {
          this.Appearance.FocusedCell.BackColor = this.Appearance.FocusedRow.BackColor = System.Drawing.Color.Red;
        }
        else
        {
          this.Appearance.FocusedCell.BackColor = this.Appearance.FocusedRow.BackColor = _FocuseRowPerBackColor;
        }
        _LockFocuseRow = value;
      }
    }
    private bool _LockFocuseRow = false;
    private System.Drawing.Color _FocuseRowPerBackColor { get; set; }
    private System.Drawing.Color _FocuseCellPerBackColor { get; set; }
    public static string GetDataTableFieldVals(GridView gdVw, string field)
    {
      if (gdVw == null)
        return string.Empty;
      StringBuilder _sb = new StringBuilder();
      DataRow _dr = null;
      HashSet<string> _hsahSet = new HashSet<string>();
      for (int _i = 0, _iCnt = gdVw.RowCount; _i < _iCnt; _i++)
      {
        _dr = gdVw.GetDataRow(_i);
        string _val = (_dr == null || _dr.RowState == DataRowState.Deleted || _dr.RowState == DataRowState.Detached) ? string.Empty : _dr[field].Equals(DBNull.Value) ? string.Empty : _dr[field].ToString().Trim();
        if (!_hsahSet.Contains(_val) && _val.Length != 0)
        {
          _hsahSet.Add(_val);
          if (_sb.Length != 0)
          {
            _sb.Append(",");
          }
          _sb.Append(_dr[field]);
        }

      }
      return _sb.ToString();
    }
    /// <summary>
    /// 添加新行的时候，是否自动定位到第一列
    /// </summary>
    public bool AutoNewRowFiristClomn
    {
      get
      {
        return _AutoNewRowFiristClomn;
      }
      set
      {
        _AutoNewRowFiristClomn = value;
      }
    }
    private bool _AutoNewRowFiristClomn = true;
    public event RefreshDataDelegate OnRefreshData
    {
      add
      {
        _onRefreshData = value;
      }
      remove
      {
        _onRefreshData = null;
      }
    }
    private event RefreshDataDelegate _onRefreshData;
    public delegate void RefreshDataDelegate(RefreshEventType EventType);


    public event System.EventHandler OnChangedColumns
    {
      add
      {
        _onChangedColumns = value;
      }
      remove
      {
        _onChangedColumns = null;
      }
    }
    private event System.EventHandler _onChangedColumns;

    public event ColumnActionDelegate OnColumnAction
    {
      add
      {
        _onColumnAction = value;
      }
      remove
      {
        _onColumnAction = null;
      }
    }
    private event ColumnActionDelegate _onColumnAction;
    public delegate void ColumnActionDelegate(GridView gdVw,GridColumn gdCl);
    public enum RefreshEventType
    {
      Refresh,
      UpdateCurrent,
      UpdateCurrentRowSelf
    }
    public bool NoActionEvent { get; set; }
    private DataTable ExportTableSchema
    {
      get
      {
        if (_ExportTableSchema == null)
        {
          _ExportTableSchema = new DataTable();
          string[] _fields = new string[Columns.Count];
          DevExpress.XtraEditors.Repository.RepositoryItemLookUpEditBase _repItem = null;
          for (int _i = 0, _iCnt = Columns.Count; _i < _iCnt; _i++)
          {
            _repItem = (Columns[_i].ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemLookUpEditBase);
            _fields[_i] = Columns[_i].FieldName;
            if (_repItem == null)
            {
              _ExportTableSchema.Columns.Add(_fields[_i], Columns[_i].ColumnType);
            }
            else
            {
              _ExportTableSchema.Columns.Add(_fields[_i], typeof(System.String));
            }
          }
        }
        return _ExportTableSchema.Clone();
      }
    }
    private DataTable _ExportTableSchema = null;
    #region 自定义函数
    public DataTable ExportToDataTable()
    {
      DataTable _dt = ExportTableSchema;

      object[] _itemArray = new object[Columns.Count];
      for (int _r = 0, _rCnt = this.RowCount; _r < _rCnt; _r++)
      {
        for (int _i = 0, _iCnt = Columns.Count; _i < _iCnt; _i++)
        {
          _itemArray[_i] = this.GetRowCellDisplayText(_r, Columns[_i]);
        }
        _dt.LoadDataRow(_itemArray, false);
      }
      return _dt;
    }
    public DataTable ExportFocRowToDataTable()
    {
      DataTable _dt = ExportTableSchema;

      object[] _itemArray = new object[Columns.Count];
      int _focIdx = this.FocusedRowHandle;
      for (int _i = 0, _iCnt = Columns.Count; _i < _iCnt; _i++)
      {
        _itemArray[_i] = this.GetRowCellDisplayText(_focIdx, Columns[_i]);
      }
      _dt.LoadDataRow(_itemArray, false);
      return _dt;
    }
    public void SetFocus(bool isLast)
    {
      this.Focus();
      if (isLast)
      {
        this.MoveTo(-2147483647);
      }
      else
      {
        this.MoveFirst();
      }
      this.NextFocusedColumn(0,false);

    }

    public int SetFirstGdVwIdxByCellValue(string col, string val)
    {
      for (int _i = 0, _rCnt = this.RowCount; _i < _rCnt; _i++)
      {
        if (this.GetRowCellValue(_i, col).ToString().Equals(val))
        {
          return SetGdVwIdx_Focuse(_i);
        }
      }
      return 0;
    }

    public int SetGdVwIdx_Focuse(int idx)
    {
      idx = idx < 0 ? 0 : idx;
      this.FocusedRowHandle = idx;//自动触发gdVwIdx_FocusedRowChanged事件
      return idx;
    }

    private bool NextFocusedColumn(int nextIndex)
    {
      return NextFocusedColumn(nextIndex, true);
    }

    private bool NextFocusedColumn(int nextIndex,bool isPastReadOnly)
    {
      for (int _i = nextIndex; _i < this.VisibleColumns.Count; _i++)
      {
        if (isPastReadOnly == false ||
          this.VisibleColumns[_i].ReadOnly == false && this.VisibleColumns[_i].OptionsColumn.AllowEdit == true)
        {
          this.FocusedColumn = this.VisibleColumns[_i];
          return true;
        }
      }
      return false;
    }

    private bool PreFocusedColumn(int preIndex)
    {
      return PreFocusedColumn(preIndex, true);
    }
    private bool PreFocusedColumn(int preIndex, bool isPastReadOnly)
    {
      for (int _i = preIndex; _i >= 0; _i--)
      {
        if (isPastReadOnly == false || 
          this.VisibleColumns[_i].ReadOnly == false && this.VisibleColumns[_i].OptionsColumn.AllowEdit == true)
        {
          this.FocusedColumn = this.VisibleColumns[_i];
          return true;
        }
      }
      return false;
    }

    public bool ShowGridViewRep()
    {
      this.ShowEditor();
      BaseEdit rep = this.ActiveEditor;
      if (rep == null)
        return false;
      switch (rep.ToString())
      {
        case "DevExpress.XtraEditors.ImageComboBoxEdit":
          rep.Show();
          if (!(rep as DevExpress.XtraEditors.ImageComboBoxEdit).IsPopupOpen)
          {
            (rep as DevExpress.XtraEditors.ImageComboBoxEdit).ShowPopup();
            if ((rep as DevExpress.XtraEditors.ImageComboBoxEdit).Properties.Items.Count > 0 &&
              (rep.EditValue == null || rep.EditValue.Equals(System.DBNull.Value)))
            {
              (rep as DevExpress.XtraEditors.ImageComboBoxEdit).SelectedIndex = 0;
            }
          }
          return true;
        case "DevExpress.XtraEditors.TextEdit":
        case "DevExpress.XtraEditors.CalcEdit":
          rep.Focus();
          rep.Select();
          rep.SelectAll();
          return true;
        case "DevExpress.XtraEditors.LookUpEdit":
        case "ExtendControl.ExtRpyLookUpEdit":
        case "DevExpress.XtraEditors.MemoExEdit":
        //case "DevExpress.XtraEditors.ImageEdit":
          rep.Show();
          rep.Select();
          if (!(rep as DevExpress.XtraEditors.PopupBaseEdit).IsPopupOpen)
          {
            (rep as DevExpress.XtraEditors.PopupBaseEdit).ShowPopup();
          }
          return true;
      }
      return false;
    }
    /// <summary>
    /// 更新View 激发OnRefreshData事件
    /// </summary>
    public void RefreshGridView()
    {
      base.RefreshData();
      ActionRefreshDataEvent(RefreshEventType.Refresh);
    }
    public bool UpdateCurrentRowSelf()
    {
      bool _bl = base.UpdateCurrentRow();
      ActionRefreshDataEvent(RefreshEventType.UpdateCurrentRowSelf);
      return _bl;
    }
    public void ResetGridColumns()
    {
      int _i = 0, _iCnt = this.Columns.Count;
      for (; _i < _iCnt; _i++)
      {
        this.Columns[_i].Visible = false;
      }
      for (_i = 0; _i < _iCnt; _i++)
      {
        this.Columns[_i].VisibleIndex = _i;
        this.Columns[_i].Visible = true;
      }
    }
    /// <summary>
    /// 激发OnRefreshData事件
    /// </summary>
    private void ActionRefreshDataEvent(RefreshEventType EventType)
    {
      if (_onRefreshData != null)
      {
        _onRefreshData(EventType);
      }
    }

    public void InvalidFocusedRowHandle()
    {
      this.FocusedRowHandle = InvalidRowHandle;
    }
    #endregion

    #region 自定义重载
    public override GridColumn FocusedColumn
    {
      get
      {
        return base.FocusedColumn;
      }
      set
      {
        base.FocusedColumn = value;
        if (_onColumnAction != null)
        {
          _onColumnAction(this, value);
        }
        ShowGridViewRep();
      }
    }

    protected override void OnEndInit()
    {
      base.OnEndInit();
      this.OptionsMenu.EnableColumnMenu =
      this.OptionsMenu.EnableFooterMenu =
      this.OptionsMenu.EnableGroupPanelMenu =
      this.OptionsMenu.ShowGroupSortSummaryItems =
      this.OptionsFilter.AllowFilterEditor =
      this.OptionsView.ShowGroupExpandCollapseButtons =
      this.OptionsView.ShowGroupPanel =
      this.OptionsCustomization.AllowFilter=
      this.OptionsView.ShowDetailButtons = false;
      this.KeyDown += new KeyEventHandler(ExtendGridView_KeyDown);
      for (int _i = 0; _i < this.Columns.Count; _i++)
      {
        if (this.Columns[_i].ColumnEdit != null)
        {
          this.Columns[_i].ColumnEdit.KeyDown += new KeyEventHandler(ColumnEdit_KeyDown);
        }
      }
      if (this.IndicatorWidth < 40)
      {
        this.IndicatorWidth = 40;
      }
      _FocuseRowPerBackColor = this.Appearance.FocusedRow.BackColor;
      _FocuseCellPerBackColor = this.Appearance.FocusedCell.BackColor;
    }

    protected override void RaiseCustomDrawRowIndicator(RowIndicatorCustomDrawEventArgs e)
    {
      base.RaiseCustomDrawRowIndicator(e);
      if (e.Info.IsRowIndicator && e.RowHandle >= 0)
      {
        e.Info.DisplayText = Convert.ToString(Convert.ToInt32(e.RowHandle.ToString()) + 1);
      }
    }

    protected override void OnActiveEditor_ValueChanged(object sender, EventArgs e)
    {
      base.OnActiveEditor_ValueChanged(sender, e);
      System.Data.DataView _dv = (this.DataSource as System.Data.DataView);
      if (_dv != null && (_dv.AllowNew == false || _dv.AllowEdit == false) && this.FocusedRowHandle == -2147483647)
      {
        return;
      }
      if (string.Format("{0}", this.FocusedColumn.Tag).Equals("UnSetValue"))
      {
        return;
      }
      string _sType=sender.GetType().ToString();
      switch (_sType)
      {
        case "DevExpress.XtraEditors.LookUpEdit":
        case "ExtendControl.ExtRpyLookUpEdit":
        case "ExtendControl.SingLookUpEdit":
          if (
          ((DevExpress.XtraEditors.LookUpEdit)(sender)).IsPopupOpen == false &&
          ((DevExpress.XtraEditors.LookUpEdit)(sender)).EditValue != null)
          {
            object _val = ((DevExpress.XtraEditors.LookUpEdit)(sender)).EditValue;
            SetFocusedValue(_val);
          }
          break;
        case "DevExpress.XtraEditors.CheckEdit":
          if (
          ((DevExpress.XtraEditors.CheckEdit)(sender)).EditValue != null)
          {
            object _val = ((DevExpress.XtraEditors.CheckEdit)sender).EditValue;
            SetFocusedValue(_val);
          }
          break;
        case "DevExpress.XtraEditors.CheckedComboBoxEdit":
        case "ExtendControl.ExtCheckedComboBoxEdit":
          if (
          ((DevExpress.XtraEditors.CheckedComboBoxEdit)(sender)).IsPopupOpen == false &&
          ((DevExpress.XtraEditors.CheckedComboBoxEdit)(sender)).EditValue != null)
          {
            object _val = ((DevExpress.XtraEditors.CheckedComboBoxEdit)sender).EditValue;
            SetFocusedValue(_val);
          }
          break;
        case "DevExpress.XtraEditors.ImageComboBoxEdit":
          DevExpress.XtraEditors.ImageComboBoxEdit _imgSender = sender as DevExpress.XtraEditors.ImageComboBoxEdit;
          if (_imgSender.IsPopupOpen == false && _imgSender.EditValue != null)
          {
            SetFocusedValue(_imgSender.EditValue);
          }
          break;
        case "DevExpress.XtraEditors.CalcEdit":
          DevExpress.XtraEditors.CalcEdit _clacSender = sender as DevExpress.XtraEditors.CalcEdit;
          if (_clacSender != null && _clacSender.EditValue != null  )
          {
            object _val = _clacSender.EditValue;
            int _selStart = _clacSender.SelectionStart;
            SetFocusedValue((_val.Equals(string.Empty) || _val.Equals(".")) ? 0 : _val);
            _clacSender.SelectionStart = _selStart;
          }
          break;
        case "DevExpress.XtraEditors.TextEdit":
          if (((DevExpress.XtraEditors.TextEdit)(sender)).EditValue != null)
          {
            object _val = ((DevExpress.XtraEditors.TextEdit)(sender)).EditValue;
            SetFocusedValue(_val);
          }
          break;
      }
      
    }

    /// <summary>
    /// 提交更改到DataSorce
    /// </summary>
    /// <returns></returns>
    public override bool UpdateCurrentRow()
    {
      bool _bl = base.UpdateCurrentRow();
      ActionRefreshDataEvent(RefreshEventType.UpdateCurrent);
      return _bl;
    }

    public override void DeleteRow(int rowHandle)
    {
      base.DeleteRow(rowHandle);
      RefreshGridView();
    }

    public override void DeleteSelectedRows()
    {
      base.DeleteSelectedRows();
      RefreshGridView();
    }

    protected override void DoChangeFocusedRow(int currentRowHandle, int newRowHandle, bool raiseUpdateCurrentRow)
    {
      if (!LockFocuseRow)
      {
        base.DoChangeFocusedRow(currentRowHandle, newRowHandle, raiseUpdateCurrentRow);
      }
    }

    protected override void FireChangedColumns()
    {
      base.FireChangedColumns();
      if (_onChangedColumns != null)
      {
        _onChangedColumns(this, null);
      }
    }
    #endregion

    #region 事件函数

    void ColumnEdit_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.SuppressKeyPress)
        return;
      this.ShowEditor();
      BaseEdit rep = this.ActiveEditor;

      if (e.KeyCode == Keys.Space && (rep is DevExpress.XtraEditors.PopupBaseEdit))
      {
        ShowGridViewRep();
        e.SuppressKeyPress = true;
      }
      if (e.KeyValue == 37 && rep != null && //右
        (rep is DevExpress.XtraEditors.PopupBaseEdit) &&
        (rep as DevExpress.XtraEditors.PopupBaseEdit).SelectionStart == 0)
      {
        PreFocusedColumn(this.FocusedColumn.VisibleIndex - 1);
        e.SuppressKeyPress = true;
      }
      if (e.KeyValue == 39)
      {
        if (rep != null && (rep is DevExpress.XtraEditors.PopupBaseEdit) &&
        (
          (rep.Controls.Count > 0 && rep.Controls[0].CanFocus == false) ||
          (rep as DevExpress.XtraEditors.PopupBaseEdit).SelectionStart == (rep as DevExpress.XtraEditors.PopupBaseEdit).Text.Length)
        )
        {
          if (!NextFocusedColumn(this.FocusedColumn.VisibleIndex + 1))
          {
            this.MoveNext();
            if (this.FocusedRowHandle < 0)
            {
              this.AddNewRow();
            }
            NextFocusedColumn(0);
          }
          e.SuppressKeyPress = true;
        }
      }
      //下
      if (e.KeyValue == 38 && rep != null && (rep is DevExpress.XtraEditors.LookUpEdit) &&
        (rep as DevExpress.XtraEditors.LookUpEdit).IsPopupOpen == false
        )
      {
        this.MovePrev();
        e.SuppressKeyPress = true;
      }
      //下
      if (e.KeyValue == 40 && rep != null && (rep is DevExpress.XtraEditors.LookUpEdit) &&
        (rep as DevExpress.XtraEditors.LookUpEdit).IsPopupOpen==false
        )
      {
        this.MoveNext();
        if (this.FocusedRowHandle < 0)
        {
          this.AddNewRow();
        }
        e.SuppressKeyPress = true;
      }
      if (e.KeyCode == Keys.Enter
        &&
        (rep is DevExpress.XtraEditors.LookUpEdit) &&
        (rep as DevExpress.XtraEditors.LookUpEdit).IsPopupOpen == false &&
        (
        (rep as DevExpress.XtraEditors.LookUpEdit).EditValue == null ||
        (rep as DevExpress.XtraEditors.LookUpEdit).EditValue.Equals(DBNull.Value)
        )
        )
      {
        ShowGridViewRep();
        e.SuppressKeyPress = true;
      }
    }
    void ExtendGridView_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.SuppressKeyPress)
        return;
      if (e.KeyCode == Keys.Enter)
      {
        this.ShowEditor();
        BaseEdit rep = this.ActiveEditor;
        if (rep != null && rep is DevExpress.XtraEditors.LookUpEdit && (rep as DevExpress.XtraEditors.LookUpEdit).IsPopupOpen)
        {
          return;
        }
        if ((rep is DevExpress.XtraEditors.LookUpEdit) &&
        (rep as DevExpress.XtraEditors.LookUpEdit).IsPopupOpen == false &&
        (
        (rep as DevExpress.XtraEditors.LookUpEdit).EditValue == null ||
        (rep as DevExpress.XtraEditors.LookUpEdit).EditValue.Equals(DBNull.Value)
        )
          )
        {
          ShowGridViewRep();
          e.SuppressKeyPress = true;
          return;
        }

        if (this.FocusedColumn != null && !NextFocusedColumn(this.FocusedColumn.VisibleIndex + 1))
        {
          this.MoveNext();
          //if (_gw.GetRow(_gw.FocusedRowHandle + 1) != null)
          //{
          //  _gw.FocusedRowHandle = (_gw.FocusedRowHandle + 1);
          //  break;
          //}
          if (this.FocusedRowHandle < 0)
          {
            this.AddNewRow();
            if (AutoNewRowFiristClomn)
            {
              NextFocusedColumn(0);
            }
          }
          else
          {
            NextFocusedColumn(0);
          }
        }
        e.SuppressKeyPress = true;
      }
    }
    #endregion
  }
}
