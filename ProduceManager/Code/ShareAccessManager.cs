using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Text;

namespace ProduceManager
{
  public class ShareAccessManager
  {
    private DbHelper _dbComObj = null;
    public ShareAccessManager(string _dbPath)
    {
      //_dbComObj = new DbHelper(
      //  string.Format("Provider=Microsoft.Jet.OLEDB.12.0;Data Source={0};Persist Security Info=True;Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';", _dbPath),
      //  "System.Data.OleDb");
      _dbComObj = new DbHelper(
        string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True;Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';", _dbPath),
        "System.Data.OleDb");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="strSqlSPName">存储过程的名字</param>
    /// <param name="strParaKeys">参数列表，,隔开</param>
    /// <param name="strParaVals">参数值数组</param>
    /// <param name="strRetType">Table返回DataTable；String时返回string；Int时返回int</param>
    /// <returns></returns>
    public object ExecDbCommandAp(string _command, string[] _parakeys, string[] _paravals,
        string _rettype, CommandType _commandtype)
    {
      using (DbCommand cmd = _commandtype.Equals(CommandType.Text) ?
              _dbComObj.GetSqlStringCommond(_command) :
              _dbComObj.GetStoredProcCommond(_command))
      {
        if (_parakeys != null && _paravals != null & _parakeys.Length == _paravals.Length)
        {
          for (int i = 0; i < _parakeys.Length; i++)
          {
            _dbComObj.AddInParameter(cmd, "@" + _parakeys[i], DbType.String, _paravals[i]);
          }
        }
        if (_rettype == "Table")
          return _dbComObj.ExecuteDataTable(cmd);
        else if (_rettype == "String")
          return _dbComObj.ExecuteScalar(cmd).ToString();
        else if (_rettype == "Int")
          return Convert.ToInt32(_dbComObj.ExecuteScalar(cmd));
        else if (_rettype == "DataSet")
          return _dbComObj.ExecuteDataSet(cmd);
        else if (_rettype == "ListObj")
          return _dbComObj.ExecuteListObject(cmd);
        else if (_rettype == "ListHash")
          return _dbComObj.ExecuteListHashtable(cmd);

        return null;
      }
    }
    public object ExecDbCommand(string _command, string[] _parakeys, string[] _paravals, string _rettype)
    {
      return ExecDbCommandAp(_command, _parakeys, _paravals, _rettype, CommandType.Text);
    }
    public object ExecDbCommand(string _command, string _rettype)
    {
      return ExecDbCommandAp(_command, null, null, _rettype, CommandType.Text);
    }
    public object ExecDbCommand(string _command, string[] _parakeys, 
                                Hashtable _paravalht, string _rettype)
    {
      return ExecDbCommandByHash(_command, _parakeys, _paravalht, _rettype, CommandType.Text);
    }


    public object ExecDbStore(string _command, string[] _parakeys, string[] _paravals, string _rettype)
    {
      return ExecDbCommandAp(_command, _parakeys, _paravals, _rettype, CommandType.StoredProcedure);
    }
    public object ExecDbStore(string _command, string _rettype)
    {
      return ExecDbCommandAp(_command, null, null, _rettype, CommandType.StoredProcedure);
    }
    public object ExecDbStore(string _command, string[] _parakeys, 
                              Hashtable _paravalht, string _rettype)
    {
      return ExecDbCommandByHash(_command, _parakeys, _paravalht, _rettype, CommandType.StoredProcedure);
    }

    public object ExecDbCommandByHash(string _command, string[] _parakeys, Hashtable _paravalht,
                                      string _rettype, CommandType _commandtype)
    {
      string[] _paravals = new string[_parakeys.Length];
      int i = 0;
      foreach (string _key in _parakeys)
      {
        _paravals[i] = _paravalht[_key].ToString();
        i++;
      }
      return ExecDbCommandAp(_command, _parakeys, _paravals, _rettype, _commandtype);
    }

    public string GetSelectString(string _tablename, string[] _cols, string[] _qrycols)
    {
      StringBuilder _sb = new StringBuilder();
      _sb.Append("SELECT ");
      if (_cols == null || _cols.Length == 0)
      {
        _sb.Append("* ");
      }
      else
      {
        AppendColString(_cols, ref _sb, false);
      }
      _sb.Append(string.Format("FROM {0} ", _tablename));
      if (_qrycols == null || _qrycols.Length == 0)
      {
      }
      else
      {
        _sb.Append("WHERE ");
        AppendConditionString(_qrycols, ref _sb);
      }
      return _sb.ToString();
    }
    public string GetUpdateString(string _tablename, string[] _cols, string[] _qrycols)
    {
      StringBuilder _sb = new StringBuilder();
      _sb.Append(string.Format("UPDATE {0} SET ", _tablename));
      if (_cols == null || _cols.Length == 0)
      {
        throw (new Exception());
      }
      else
      {
        AppendSetString(_cols, ref _sb);
      }
      _sb.Append("WHERE ");
      AppendConditionString(_qrycols, ref _sb);
      return _sb.ToString();
    }
    public string GetInsertString(string _tablename, string[] _cols)
    {
      StringBuilder _sb = new StringBuilder();
      _sb.Append(string.Format("INSERT INTO  {0} (", _tablename));
      if (_cols == null || _cols.Length == 0)
      {
        throw (new Exception());
      }
      else
      {
        AppendColString(_cols, ref _sb,false);
      }
      _sb.Append(") VALUES (");
      AppendColString(_cols, ref _sb,true);
      _sb.Append(")");
      return _sb.ToString();
    }
    public string GetDeleteString(string _tablename, string[] _qrycols)
    {
      StringBuilder _sb = new StringBuilder();
      _sb.Append(string.Format("DELETE FROM {0} ", _tablename));
      _sb.Append("WHERE ");
      AppendConditionString(_qrycols, ref _sb);
      return _sb.ToString();
    }
    public void AppendColString(string[] _cols, ref StringBuilder _sb,bool _isparam)
    {
      foreach (string _col in _cols)
      {
        if (_isparam) _sb.Append("@");
        _sb.Append(_col);
        _sb.Append(",");
      }
      _sb.Replace(',', ' ', _sb.Length - 1, 1);
    }
    public void AppendSetString(string[] _cols, ref StringBuilder _sb)
    {
      foreach (string _col in _cols)
      {
        _sb.Append(string.Format("{0}= @{0},", _col));
      }
      _sb.Replace(',', ' ', _sb.Length - 1, 1);
    }
    public void AppendConditionString(string[] _qrycols, ref StringBuilder _sb)
    {
      foreach (string _col in _qrycols)
      {
        _sb.Append(string.Format("{0}= @{0} AND ", _col));
      }
      _sb.Remove(_sb.Length - "AND ".Length, "AND ".Length);
    }
  }
}
