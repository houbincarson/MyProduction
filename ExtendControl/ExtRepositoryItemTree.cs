using System.Text;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;

namespace ExtendControl
{
  public class ExtRepositoryItemTree : RepositoryItemPopupContainerEdit
  {
    public System.Data.DataTable DataSource { get; set; }
    public string DisplayMember { get; set; }
    public string ValueMember { get; set; }

    #region 重载函数
    public new ExtPopupTree OwnerEdit
    {
      get
      {
        return CreateEditor() as ExtPopupTree;
      }
    }
    public new ExtRepositoryItemTree Properties
    {
      get
      {
        return this;
      }
    }
    public override BaseEdit CreateEditor()
    {//不重载的话base.OwnerEdit返回null,点击单元格后会直接显示EditValue
      if (base.OwnerEdit == null)
      {
        ExtPopupTree _bs = new ExtPopupTree();
        this.PopupControl = _bs.Properties.PopupControl;
        SetOwnerEdit(_bs);
      }
      return base.OwnerEdit;
    }
    public override string GetDisplayText(DevExpress.Utils.FormatInfo format, object editValue)
    {
      return format.GetDisplayText(GetDisplayText(editValue));
    }
    public override string GetDisplayText(object editValue)
    {
      string[] _ids = string.Format("{0}", editValue).Split(',');
      int _i = 0, _iCnt = _ids.Length;
      if (DataSource != null &&
        !string.IsNullOrEmpty(ValueMember) &&
        !string.IsNullOrEmpty(DisplayMember) &&
        _iCnt > 0)
      {
        StringBuilder _sb = new StringBuilder();
        for (; _i < _iCnt; _i++)
        {
          if (_sb.Length != 0)
          {
            _sb.Append(",");
          }
          _sb.Append(GetDisplayTextByValue(_ids[_i]));
        }
        return _sb.ToString();
      }
      return string.Empty;
    }
    #endregion
    public new void SetOwnerEdit(DevExpress.XtraEditors.BaseEdit edit)
    {
      base.SetOwnerEdit(edit);
    }
    private string GetDisplayTextByValue(string val)
    {
      for (int i = 0, _iCnt = this.DataSource.Rows.Count; i < _iCnt; i++)
      {
        if (val.Equals(string.Format("{0}", this.DataSource.Rows[i][ValueMember])))
        {
          return string.Format("{0}", this.DataSource.Rows[i][DisplayMember]);
        }
      }
      return string.Empty;
    }

    #region IExtendContrlBind 成员

    public void DataBind(System.Data.DataTable dataSource, string displayMember, string valueMember, string dropDisplayName, string sortMember, string[] colFields, string[] captions)
    {
      OwnerEdit.DataBind(dataSource, displayMember, valueMember, dropDisplayName, sortMember, colFields, captions);
    }

    #endregion
  }
}
