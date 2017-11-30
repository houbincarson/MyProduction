
using FrmBase;
namespace ExtendControl
{
  public class ExtRepositoryItemCheckedBox : 
    DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit, IExtendContrlBind
  {
    private ExtCheckedComboBoxEdit ownerEdit;
    #region IExtendContrlBind 成员

    public void DataBind(System.Data.DataTable _dataSource, string _displayMember, string _valueMember, string _dropDisplayName, 
                  string _sortMember, string[] _colFields, string[] _captions)
    {
      this.DataSource = _dataSource;
      this.DisplayMember = _displayMember;
      this.ValueMember = _valueMember;
      this.SelectAllItemCaption = "全选";
    }
    #endregion
    public override DevExpress.XtraEditors.BaseEdit CreateEditor()
    {
      ownerEdit = new ExtCheckedComboBoxEdit();
      return ownerEdit;
    }
    public void SetSefOwnerEdit(DevExpress.XtraEditors.BaseEdit edit)
    {
      this.SetOwnerEdit(edit);
    }
    public override string GetDisplayText(object editValue)
    {
      return base.GetDisplayText(editValue);
    }
    public override string GetDisplayText(DevExpress.Utils.FormatInfo format, object editValue)
    {
      string displayText = string.Empty;
      if (this.IsBoundMode)
      {
        displayText = this.GetDisplayText(editValue, true);
      }
      else 
      {
        displayText = this.GetDisplayText(editValue, false);
      }
      DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e = 
        new DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs(editValue, base.GetDisplayText(format, editValue));
      if (format != this.EditFormat)
      {
        this.RaiseCustomDisplayText(e);
      }

      return displayText;
    }

    private string GetDisplayText(object editValue, bool bound)
    {
      string str = string.Format("{0}", editValue);
      if ((str == string.Empty) || ((this.DisplayMember == string.Empty) && bound))
      {
        return NullText  ;
        //return str;
      }
      string[] strArray = str.Split(new char[] { this.SeparatorChar });
      string str2 = string.Empty;
      for (int i = 0; i < strArray.Length; i++)
      {
        string val = strArray[i];
        if (i > 0)
        {
          val = val.Trim();
        }
        str2 = str2 + string.Format("{0}{1} ", bound ? this.GetBoundDisplayTextByValue(val) : this.GetDisplayTextByValue(val), this.SeparatorChar);
      }
      if (str2.Length > 2)
      {
        str2 = str2.Substring(0, str2.Length - 2);
      }
      return str2;
    }

    private string GetDisplayTextByValue(string val)
    {
      for (int i = 0; i < this.Items.Count; i++)
      {
        if (val.Equals(string.Format("{0}", this.Items[i].Value)))
        {
          return string.Format("{0}", this.Items[i].Description);
        }
      }
      return val;
    }

    private string GetBoundDisplayTextByValue(string val)
    {
      for (int i = 0; i < this.DataAdapter.ListSource.Count; i++)
      {
        if (val.Equals(string.Format("{0}", this.DataAdapter.GetValueAtIndex(this.ValueViewMember, i))))
        {
          return string.Format("{0}", this.DataAdapter.GetValueAtIndex(this.DisplayMember, i));
        }
      }
      return val;
    }
  }
}
