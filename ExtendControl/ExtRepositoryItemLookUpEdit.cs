using System;
using System.ComponentModel;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using FrmBase;

namespace ExtendControl
{
  public class ExtRepositoryItemLookUpEdit : RepositoryItemLookUpEditBase, IExtendContrlBind
  {
    private LookUpEdit ownerEdit;
    private string dropDisplayName;
    private string displayMember;
    private string valueMember;
    private string sortMember;
    private string rowFilter;
    private string[] colFields;
    private string[] captions;
    private System.Data.DataTable dataSource;
    private static readonly object _sny = new object();
    public event ExtendControl.ExtRpyLookUpEdit.LokkUpEditValueChang OnLookUpEditValueChang
    {
      add
      {

        _onLookUpEditValueChang += value;
      }
      remove
      {

        _onLookUpEditValueChang = null;
      }
    }
    ExtendControl.ExtRpyLookUpEdit.LokkUpEditValueChang _onLookUpEditValueChang = null;
    [Browsable(false)]
    public new LookUpEdit OwnerEdit
    {
      get
      {
        return ownerEdit;
      }
    }
    public ExtRepositoryItemLookUpEdit()
      : base()
    {
      this.NullText = string.Empty;
    }
    public override BaseEdit CreateEditor()
    {
      this.QueryPopUp += new CancelEventHandler(ExtRepositoryItemLookUpEdit_QueryPopUp);
      this.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(ExtRepositoryItemLookUpEdit_Closed);
      if (this.dataSource == null)
      {
        if (((System.Data.DataView)(DataSource)) == null)
        {
        }
        else
        {
          dataSource = ((System.Data.DataView)(DataSource)).Table;
        }
      }
      if (this.dataSource != null)
      {
        this.dataSource.DefaultView.Sort = sortMember;
      }
      ownerEdit = new ExtRpyLookUpEdit();
      ownerEdit.Properties.DisplayMember = displayMember;
      ownerEdit.Properties.ValueMember = valueMember;
      if (colFields == null || captions == null || colFields.Length != captions.Length)
      {
#if　DEBUG
        throw new Exception("显示行数据字段和字段名不一至");
#else
#endif
      }
      else
      {
        int _totalWidth = 30, _width = 0;
        ownerEdit.Properties.Columns.Clear();
        for (int i = 0; i < colFields.Length; i++)
        {
          string[] _colInf = colFields[i].Split('=');
          if (_colInf.Length > 1)
          {
            _width = int.Parse(_colInf[1]);
          }
          else
          {
            _width = 70;
          }
          _totalWidth += _width;
          ownerEdit.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(_colInf[0], _width, captions[i]));
        }
        ownerEdit.Properties.PopupWidth = _totalWidth;
        this.PopupFormSize =
        this.PopupFormMinSize = new System.Drawing.Size(_totalWidth, 260);
      }
      ownerEdit.Properties.NullText = string.Empty;
      if (_onLookUpEditValueChang != null)
      {
        ((ExtRpyLookUpEdit)ownerEdit).OnLookUpEditValueChang += _onLookUpEditValueChang;
      }
      return ownerEdit;
    }

    void ExtRepositoryItemLookUpEdit_QueryPopUp(object sender, CancelEventArgs e)
    {
      if (!ownerEdit.Properties.DisplayMember.Equals(dropDisplayName))
      {
        ownerEdit.Properties.DisplayMember = dropDisplayName;
      } 
    }
    void ExtRepositoryItemLookUpEdit_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
    {
      if (!ownerEdit.Properties.DisplayMember.Equals(DisplayMember))
      {
        ownerEdit.Properties.DisplayMember = displayMember;
      }
    }
    public void DataBind(System.Data.DataTable _dataSource)
    {
      this.dataSource = _dataSource;
      this.dataSource.DefaultView.RowFilter = rowFilter;
      DataSource = this.dataSource.DefaultView;
    }
    public void SetFilter(string _filter)
    {
      rowFilter = _filter;
      if (this.dataSource == null)
      {
        return;
      }
      this.dataSource.DefaultView.RowFilter = rowFilter;
      DataSource = this.dataSource.DefaultView;
    }
    public static string GetDisplayText_By_Exp(System.Data.DataTable _dt, string _filterExp, string _displayMember)
    {
      DataRow _dr = GetDataRow_By_Exp(_dt, _filterExp);
      return _dr == null ? string.Empty : _dr[_displayMember].ToString();

    }
    public static DataRow GetDataRow_By_Exp(System.Data.DataTable _dt, string _filterExp)
    {
      lock (_sny)
      {
        System.Data.DataRow[] _drs = _dt.Select(_filterExp);
        if (_drs.Length > 0)
        {
          return _drs[0];
        }
      }
      return null;
    }
    public DataRow GetDataRow(object editValue)
    {
      try
      {
        if (editValue.Equals(string.Empty) || editValue == null || this.dataSource == null || editValue.Equals(System.DBNull.Value))
          return null;
        string _filterExp = string.Format("{0}='{1}'", valueMember, editValue);
        return GetDataRow_By_Exp(this.dataSource, _filterExp);
      }
      catch (Exception)
      {
        return null;
      }
    }
    public override string GetDisplayText(object editValue)
    {
      try
      {
        if (editValue.Equals(string.Empty) || editValue == null || this.dataSource == null || editValue.Equals(System.DBNull.Value))
        {
          ownerEdit.EditValue = null;
          return string.Empty;
        }
        string _filterExp = string.Format("{0}='{1}'", valueMember, editValue);
        return GetDisplayText_By_Exp(this.dataSource, _filterExp, displayMember);
      }
      catch (Exception)
      {
        return string.Empty;
      }
      //return base.GetDisplayText(editValue);
    }
    public override string GetDisplayText(DevExpress.Utils.FormatInfo format, object editValue)
    {
      return format.GetDisplayText(GetDisplayText(editValue));
    }
    public override DevExpress.XtraEditors.Controls.ShowDropDown ShowDropDown
    {
      get
      {
        return base.ShowDropDown;
      }
      set
      {
        base.ShowDropDown = value;
      }
    }


    #region IExtendContrlBind 成员

    public void DataBind(DataTable _dataSource, string _displayMember, string _valueMember, string _dropDisplayName, string _sortMember, string[] _colFields, string[] _captions)
    {
      this.dataSource = _dataSource;
      this.displayMember = _displayMember;
      this.valueMember = _valueMember;
      this.dropDisplayName = _dropDisplayName;
      this.colFields = _colFields;
      this.captions = _captions;
      this.sortMember = _sortMember;
      DisplayMember = _displayMember;
      ValueMember = _valueMember;
      if (this.dataSource != null)
      {
        this.dataSource.DefaultView.RowFilter = rowFilter;
        DataSource = this.dataSource.DefaultView;
      }
    }

    #endregion
  }
}
