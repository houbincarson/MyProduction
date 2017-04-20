using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrmBase;

namespace ExtendControl
{
  public class ExtCheckedComboBoxEdit : DevExpress.XtraEditors.CheckedComboBoxEdit, IExtendContrlBind
  {
    #region IExtendContrlBind 成员

    public void DataBind(System.Data.DataTable _dataSource, string _displayMember, string _valueMember, string _dropDisplayName, string _sortMember, string[] _colFields, string[] _captions)
    {
      this.Properties.DataSource = _dataSource;
      this.Properties.DisplayMember = _displayMember;
      this.Properties.ValueMember = _valueMember;
      this.Properties.SelectAllItemCaption = "全选";
      this.Properties.PopupFormSize =
      this.Properties.PopupFormSize = new System.Drawing.Size(100, 260);
      //this.Properties.SeparatorChar
    }
    protected override void CreateRepositoryItem()
    {
      this.fProperties = new ExtRepositoryItemCheckedBox();
      ((ExtRepositoryItemCheckedBox)this.fProperties).SetSefOwnerEdit(this);
    }
    public override object EditValue
    {
      get
      {
        return base.EditValue;
      }
      set
      {
        base.EditValue = value == null ? string.Empty : value.ToString().Replace(", ", ",");
      }
    }

    #endregion
  }
}
