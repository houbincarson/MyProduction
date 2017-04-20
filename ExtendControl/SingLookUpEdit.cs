using System;
using System.Data;
using DevExpress.XtraEditors;
using FrmBase;

namespace ExtendControl
{
  public class SingLookUpEdit : LookUpEdit
  {
    //protected override void CreateRepositoryItem()
    //{
    //  this.fProperties = new SingCelLookUpEdit();
    //  ((SingCelLookUpEdit)this.fProperties).SetSefOwnerEdit(this);
    //}
    protected override void PopupFormResultValueEntered()
    {
      base.PopupFormResultValueEntered();
    }
    public override object EditValue
    {
      get
      {
        return base.EditValue;
      }
      set
      {
        base.EditValue = value ;
      }
    }
  }
}
