using System;
using System.ComponentModel;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using FrmBase;

namespace ExtendControl
{
  public class SingCelLookUpEdit :
    DevExpress.XtraEditors.Repository.RepositoryItemLookUpEditBase, IExtendContrlBind
  {
    private string[] colFields;
    private string[] captions;
    private string _ValueMenber = string.Empty;
    public SingCelLookUpEdit()
      : base()
    {
      this.NullText = string.Empty;
    }
    private LookUpEdit ownerEdit;
    public override DevExpress.XtraEditors.BaseEdit CreateEditor()
    {
      ownerEdit = new SingLookUpEdit();
      ownerEdit.Properties.DataSource = DataSource;
      ownerEdit.Properties.DisplayMember = DisplayMember;
      ownerEdit.Properties.ValueMember = ValueMember;
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
      return ownerEdit;
    }
    public void SetSefOwnerEdit(DevExpress.XtraEditors.BaseEdit edit)
    {
      this.SetOwnerEdit(edit);
    }

    public override string GetDisplayText(object editValue)
    {
      string _txt = base.GetDisplayText(editValue);
      if (_txt.Length == 0)
      {
        return string.Format("{0}", editValue);
      }
      return _txt;
    }
    public override string GetDisplayText(DevExpress.Utils.FormatInfo format, object editValue)
    {
      string _txt = format.GetDisplayText(editValue);
      if (_txt.Length == 0)
      {
        return string.Format("{0}", editValue);
      }
      return _txt;
    }
    #region IExtendContrlBind 成员

    public void DataBind(DataTable _dataSource, string _displayMember, string _valueMember, string _dropDisplayName, string _sortMember, string[] _colFields, string[] _captions)
    {
      DataSource = _dataSource;
      ValueMember = 
      DisplayMember = _displayMember;
      _ValueMenber=_valueMember;
      this.colFields = _colFields;
      this.captions = _captions;
    }
    public object EditValue
    {
      get
      {
        if (ownerEdit == null || ownerEdit.EditValue == null)
        {
          return null;
        }
        return ownerEdit.GetColumnValue(_ValueMenber);
        
      }
    }

    #endregion
  }
}
