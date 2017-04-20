using System.Data;
using DevExpress.XtraEditors;

namespace ExtendControl
{
  public class ExtRpyLookUpEdit : LookUpEdit//ExtRepositoryItemLookUpEdit用
  {

    public event LokkUpEditValueChang OnLookUpEditValueChang
    {
      add
      {
        _onLookUpEditValueChang = value;
      }
      remove
      {
        _onLookUpEditValueChang = null;
      }
    }
    private event LokkUpEditValueChang _onLookUpEditValueChang;
    public delegate void LokkUpEditValueChang(string txt, DataRow val);
    /// <summary>
    /// 重载PopupFormResultValueEntered函数，用于实现，检测输入框输入内容，激发LokkUpEditValueChang事件
    /// 输入内容包含在选择项中，传递选择项数据
    /// 输入内容不包含在选择项中，传递输入内容
    /// </summary>
    protected override void PopupFormResultValueEntered()
    {
      if (_onLookUpEditValueChang != null)
      {
        string _newValue = this.PopupForm.ResultValue == null ? string.Empty : this.PopupForm.ResultValue.ToString(),
          _newTxt = this.Text.Trim();
        using (DataTable _dt = ((DataView)this.Properties.DataSource).ToTable())
        {
          DataRow[] _newValDrs = _newValue.Length == 0 ? null : _dt.Select(string.Format("{0}='{1}'", this.Properties.DisplayMember, _newValue));
          int _newTxtCnt = _newTxt.Length == 0 ? 0 : _dt.Select(string.Format("{0}='{1}'", this.Properties.DisplayMember, _newTxt)).Length;
          if (_newValDrs != null && _newValDrs.Length > 0)
          {
            if (_newTxtCnt > 0 || //输入文字和选择文字都能查询到
              (_newTxt.Length > 0 && _newValDrs[0][this.Properties.DisplayMember].ToString().ToUpper().IndexOf(_newTxt.ToUpper()) != -1)//输入文件包含在选择文件中
              )
            {
              _onLookUpEditValueChang(string.Empty, _newValDrs[0]);
              return;
            }
            else
            {
              _onLookUpEditValueChang(_newTxt, null);
              return;
            }
          }
          else//输入文字不在下拉列表中
          {
            if (_newTxt.Length > 0 && _newTxtCnt == 0)
            {
              _onLookUpEditValueChang(_newTxt, null);
              return;
            }
            else
            {
              _onLookUpEditValueChang(string.Empty, null);
              return;
            }
          }
        }
      }
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
        if (base.Properties.DataSource != null && value != null && !value.Equals(System.DBNull.Value) && !value.ToString().Trim().Equals(string.Empty))
        {
          System.Type _type = (base.Properties.DataSource as DataView).Table.Columns[base.Properties.ValueMember].DataType;
          try
          {
            base.EditValue = System.Convert.ChangeType(value, _type);
          }
          catch (System.Exception)
          {
          }
        }
        else
        {
          base.EditValue = value;
        }
      }
    }
  }
}
