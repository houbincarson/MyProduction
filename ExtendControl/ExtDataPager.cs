using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;

namespace ExtendControl
{
  public class ExtDataPager : INavigatableControl
  {
    private int _Position;
    private int _PageSize;
    private int _PageCount;
    private int _RecordCount;

    public int Position
    {
      get { return _Position; }
    }
    public int RecordCount
    {
      get { return _PageCount; }
    }
    public int RowCount
    {
      get { return _RecordCount; }
    }
    private INavigatorOwner _NavigatorOwner;
    public delegate void DataPageChanged(int pageIdx, int pageSize);
    private DataPageChanged _DataPageChangedHandler = null;
    public event DataPageChanged DataPageChangedHandler
    {
      add
      {
        _DataPageChangedHandler = value;
      }
      remove
      {
        _DataPageChangedHandler = null;
      }
    }
    public ExtDataPager(int pageSize)
    {
      _PageSize = pageSize;
      _Position = 0;
    }
    public void SetPageSize(int pageSize)
    {
      _PageSize = pageSize;
      SetRecordCount(_RecordCount);
    }
    public void SetRecordCount(int recordCount)
    {
      _RecordCount = recordCount;
      _PageCount = recordCount == 0 ? 1 : (recordCount + _PageSize - 1) / _PageSize;
      _Position = 0;
      UpdateNavigator();
    }

    private void UpdateNavigator()
    {
      if (_NavigatorOwner.Visible)
      {
        _NavigatorOwner.Buttons.UpdateButtonsState();
        _NavigatorOwner.Buttons.LayoutChanged();
      }
    }
    #region INavigatableControl 成员

    public void AddNavigator(INavigatorOwner owner)
    {
      _NavigatorOwner = owner;
    }

    public void DoAction(NavigatorButtonType type)
    {
      switch (type)
      {
        case NavigatorButtonType.First:
          if (_Position == 0)
            return;
          _Position = 0;
          break;
        case NavigatorButtonType.PrevPage:
          if (_Position == 0)
            return;
          _Position -= 1;
          _Position = _Position < 0 ? 0 : _Position;
          break;
        case NavigatorButtonType.NextPage:
          if (_Position == _PageCount - 1)
            return;
          _Position += 1;
          _Position = _Position >= _PageCount ? _PageCount - 1 : _Position;
          break;
        case NavigatorButtonType.Last:
          if (_Position == _PageCount - 1)
            return;
          _Position = _PageCount - 1;
          break;
      }
      if (_DataPageChangedHandler != null)
      {
        _DataPageChangedHandler(_Position, _PageSize);
      }
      UpdateNavigator();
    }

    public bool IsActionEnabled(NavigatorButtonType type)
    {
      switch (type)
      {
        case NavigatorButtonType.First:
          if (_Position == 0)
            return false;
          break;
        case NavigatorButtonType.PrevPage:
          if (_Position == 0)
            return false;
          break;
        case NavigatorButtonType.NextPage:
          if (_PageCount - 1 < 0 || _Position == _PageCount - 1)
            return false;
          break;
        case NavigatorButtonType.Last:
          if (_PageCount - 1 < 0 || _Position == _PageCount - 1)
            return false;
          break;
      }
      return true;
    }

    public void RemoveNavigator(INavigatorOwner owner)
    {
      _NavigatorOwner = null;
    }

    #endregion
  }
}
