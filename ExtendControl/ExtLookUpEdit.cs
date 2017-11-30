using System;
using System.Data;
using DevExpress.XtraEditors;
using FrmBase;

namespace ExtendControl
{
  public class ExtLookUpEdit : LookUpEdit, IExtendContrlBind
  {
    private string dropDisplayName;
    private string displayMember;
    private string valueMember;
    private string sortMember;
    private string[] colFields;
    private string[] captions;
    public System.Data.DataTable DataSource
    {
      get
      {
        return dataSource;
      }
    }
    private System.Data.DataTable dataSource;
    protected override void OnCreateControl()
    {
      base.OnCreateControl();
      this.Properties.NullText = string.Empty;
    }
    public string RowFilter
    {
      set
      {
        if (dataSource != null)
        {
          dataSource.DefaultView.RowFilter = value;
        }
      }
    }
    protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
    {
      if (e.KeyChar == 13 &&
        this.dataSource.Select(string.Format("'{0}'={1}", this.EditValue, this.Properties.ValueMember)).Length == 0)
      {
        ShowPopup();
      }
      base.OnKeyPress(e);
    }
    void Properties_QueryPopUp(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (!this.Properties.DisplayMember.Equals(this.dropDisplayName))
      {
        this.Properties.DisplayMember = dropDisplayName;
      }
    }
    protected override void OnPopupClosed(PopupCloseMode closeMode)
    {
      if (!this.Properties.DisplayMember.Equals(this.displayMember))
      {
        this.Properties.DisplayMember = displayMember;
      }
      base.OnPopupClosed(closeMode);
    }


    public override object EditValue
    {
      get
      {
        return base.EditValue;
      }
      set
      {
        if (dataSource != null && value != null && !value.Equals(System.DBNull.Value) && !value.ToString().Trim().Equals(string.Empty))
        {
          System.Type _type = dataSource.Columns[valueMember].DataType;
          base.EditValue = System.Convert.ChangeType(value, _type);
        }
        else
        {
          base.EditValue = value;
        }
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


      if (dataSource == null) dataSource = ((System.Data.DataView)(this.Properties.DataSource)).Table;
      this.dataSource.DefaultView.Sort = sortMember;
      this.Properties.DisplayMember = displayMember;
      this.Properties.ValueMember = valueMember;
      this.Properties.DataSource = dataSource.DefaultView;
      if (colFields.Length != captions.Length)
        throw new Exception("显示行数据字段和字段名不一至");
      int _totalWidth = 30, _width = 0;
      this.Properties.Columns.Clear();
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
        this.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(_colInf[0], _width, captions[i]));
      }
      this.Properties.PopupWidth = _totalWidth;
      this.Properties.PopupFormSize =
      this.Properties.PopupFormMinSize = new System.Drawing.Size(_totalWidth, 260);
      this.Properties.QueryPopUp += new System.ComponentModel.CancelEventHandler(Properties_QueryPopUp);

    }

    #endregion
  }
}
