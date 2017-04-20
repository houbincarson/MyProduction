using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ProduceManager
{
  /// <summary> 
  /// DbHelper 的摘要说明 
  /// </summary>
  /// 

  public class DbHelper
  {
    //private static string dbProviderName = ConfigurationSettings.AppSettings["DbHelper1Provider"];
    //private static string dbConnectionString = ConfigurationSettings.AppSettings["DbHelper1ConnectionString"];

    //数据类型
    private static string dbProviderName = "System.Data.SqlClient";
    //数据库连接字串
    private static string dbConnectionString = "";

    private DbConnection connection;

    //构造函数
    public DbHelper()
    {
      connection = CreateConnection(DbHelper.dbConnectionString);
    }
    public DbHelper(string connectionString, string dbProviderName)
    {
      DbHelper.dbProviderName = dbProviderName;
      connection = CreateConnection(connectionString);
    }
    //public DbHelper(string dbServer,string dbCatalog,string dbUID,string dbPWD)
    //{
    //    connection = CreateConnection("server=" + dbServer + ";database=" + dbCatalog + ";uid=" + dbUID + ";pwd=" + dbPWD);

    //}

    public static DbConnection CreateConnection()
    {
      DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
      DbConnection dbconn = dbfactory.CreateConnection();
      dbconn.ConnectionString = DbHelper.dbConnectionString;
      return dbconn;
    }
    public static DbConnection CreateConnection(string connectionString)
    {
      DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
      DbConnection dbconn = dbfactory.CreateConnection();
      dbconn.ConnectionString = connectionString;
      return dbconn;
    }

    /// <summary>
    /// 直接执行存储过程
    /// </summary>
    /// <param name="storedProcedure">存储过程名称</param>
    /// <returns>返回dbCommand对象</returns>
    public DbCommand GetStoredProcCommond(string storedProcedure)
    {
      DbCommand dbCommand = connection.CreateCommand();
      dbCommand.CommandText = storedProcedure;
      dbCommand.CommandType = CommandType.StoredProcedure;
      return dbCommand;
    }

    /// <summary>
    /// 直接执行sql语句,适应于数据表的增,删,改操作
    /// </summary>
    /// <param name="sqlQuery">SQL语句</param>
    /// <returns>返回dbCommand对象</returns>
    public DbCommand GetSqlStringCommond(string sqlQuery)
    {
      DbCommand dbCommand = connection.CreateCommand();
      dbCommand.CommandText = sqlQuery;
      dbCommand.CommandType = CommandType.Text;
      return dbCommand;
    }

    #region 增加参数
    public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
    {
      foreach (DbParameter dbParameter in dbParameterCollection)
      {
        cmd.Parameters.Add(dbParameter);
      }
    }

    /// <summary>
    /// 存储过程添加输出参数
    /// </summary>
    /// <param name="cmd">DbCommand对象</param>
    /// <param name="parameterName">参数名称</param>
    /// <param name="dbType">参数类型</param>
    /// <param name="size">参数大小</param>
    /// <returns>无返回结果</returns>
    public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
    {
      DbParameter dbParameter = cmd.CreateParameter();
      dbParameter.DbType = dbType;
      dbParameter.ParameterName = parameterName;
      dbParameter.Size = size;
      dbParameter.Direction = ParameterDirection.Output;
      cmd.Parameters.Add(dbParameter);
    }

    /// <summary>
    /// 存储过程添加输入参数
    /// </summary>
    /// <param name="cmd">DbCommand对象</param>
    /// <param name="parameterName">参数名称</param>
    /// <param name="dbType">参数类型</param>
    /// <param name="value">参数值</param>
    /// <returns>无返回结果</returns>
    public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
    {
      DbParameter dbParameter = cmd.CreateParameter();
      dbParameter.DbType = dbType;
      dbParameter.ParameterName = parameterName;
      dbParameter.Value = value;
      dbParameter.Direction = ParameterDirection.Input;
      cmd.Parameters.Add(dbParameter);
    }

    /// <summary>
    /// 存储过程添加返回参数
    /// </summary>
    /// <param name="cmd">DbCommand对象</param>
    /// <param name="parameterName">参数名称</param>
    /// <param name="dbType">参数类型</param>
    /// <returns>无返回结果</returns>
    public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
    {
      DbParameter dbParameter = cmd.CreateParameter();
      dbParameter.DbType = dbType;
      dbParameter.ParameterName = parameterName;
      dbParameter.Direction = ParameterDirection.ReturnValue;
      cmd.Parameters.Add(dbParameter);
    }

    /// <summary>
    /// 存储过程添加获得参数
    /// </summary>
    /// <param name="cmd">DbCommand对象</param>
    /// <param name="parameterName">参数名称</param>
    /// <returns>返回参数对象值</returns>
    public DbParameter GetParameter(DbCommand cmd, string parameterName)
    {
      return cmd.Parameters[parameterName];
    }
    #endregion

    #region 执行
    //返回DataSet对象
    public DataSet ExecuteDataSet(DbCommand cmd)
    {
      cmd.CommandTimeout = 100;
      DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
      DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
      dbDataAdapter.SelectCommand = cmd;
      DataSet ds = new DataSet();
      dbDataAdapter.Fill(ds);
      return ds;
    }
    public DataSet ExecuteDataSet(DbCommand cmd, string tableName)
    {
      cmd.CommandTimeout = 100;
      DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
      DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
      dbDataAdapter.SelectCommand = cmd;
      DataSet ds = new DataSet();
      dbDataAdapter.Fill(ds, tableName);
      return ds;
    }

    //返回DataTable对象
    public DataTable ExecuteDataTable(DbCommand cmd)
    {
      cmd.CommandTimeout = 100;
      DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
      DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
      dbDataAdapter.SelectCommand = cmd;
      DataTable dataTable = new DataTable();
      dbDataAdapter.Fill(dataTable);
      return dataTable;
    }
    //返回DataTable对象
    public List<Hashtable> ExecuteListHashtable(DbCommand cmd)
    {
      List<object[]> _listOjbs = new List<object[]>();

      cmd.CommandTimeout = 100;
      cmd.Connection.Open();
      DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
      object[] _fieldNames = new object[reader.FieldCount];
      for (int i = 0; i < reader.FieldCount; i++)
      {
        _fieldNames[i] = reader.GetName(i);
      }
      while (reader.Read())
      {
        object[] _objs = new object[reader.FieldCount];
        reader.GetValues(_objs);
        _listOjbs.Add(_objs);
      }
      reader.Close();
      cmd.Connection.Close();
      List<Hashtable> _listHts = new List<Hashtable>();
      foreach (object[] _objs in _listOjbs)
      {
        Hashtable _ht = new Hashtable();
        for (int i = 0; i < _fieldNames.Length; i++)
        {
          _ht.Add(_fieldNames[i], _objs[i]);
        }
        _listHts.Add(_ht);
      }
      return _listHts;
    }

    //返回DataTable对象
    public List<object[]> ExecuteListObject(DbCommand cmd)
    {
      List<object[]> _listOjbs = new List<object[]>();

      cmd.CommandTimeout = 100;
      cmd.Connection.Open();
      DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
      object[] _fieldNames = new object[reader.FieldCount];
      for (int i = 0; i < reader.FieldCount; i++)
      {
        _fieldNames[i] = reader.GetName(i);
      }
      while (reader.Read())
      {
        object[] _objs = new object[reader.FieldCount];
        reader.GetValues(_objs);
        _listOjbs.Add(_objs);
      }
      return _listOjbs;
    }

    //返回DataReader对象
    public DbDataReader ExecuteReader(DbCommand cmd)
    {
      cmd.CommandTimeout = 100;
      cmd.Connection.Open();
      DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
      return reader;
    }

    //无结果SQL操作,适用于数据表的增,删,改,存储过程操作
    public int ExecuteNonQuery(DbCommand cmd)
    {
      cmd.CommandTimeout = 100;
      cmd.Connection.Open();
      int ret = cmd.ExecuteNonQuery();
      cmd.Connection.Close();
      return ret;
    }
    public void ExecuteDataSet(DbCommand cmd, DataSet ds, string tableName)
    {
      cmd.CommandTimeout = 100;
      DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
      DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
      dbDataAdapter.SelectCommand = cmd;
      dbDataAdapter.Fill(ds, tableName);
    }

    //返回SQL影响的行数
    public object ExecuteScalar(DbCommand cmd)
    {
      cmd.CommandTimeout = 100;
      cmd.Connection.Open();
      object ret = cmd.ExecuteScalar();
      cmd.Connection.Close();
      return ret;
    }
    #endregion

    #region 执行事务
    public DataSet ExecuteDataSet(DbCommand cmd, Trans1 t)
    {
      cmd.Connection = t.DbConnection;
      cmd.Transaction = t.DbTrans;
      DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
      DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
      dbDataAdapter.SelectCommand = cmd;
      DataSet ds = new DataSet();
      dbDataAdapter.Fill(ds);
      return ds;
    }
    public DataTable ExecuteDataTable(DbCommand cmd, Trans1 t)
    {
      cmd.Connection = t.DbConnection;
      cmd.Transaction = t.DbTrans;
      DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
      DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
      dbDataAdapter.SelectCommand = cmd;
      DataTable dataTable = new DataTable();
      dbDataAdapter.Fill(dataTable);
      return dataTable;
    }
    public DbDataReader ExecuteReader(DbCommand cmd, Trans1 t)
    {
      cmd.Connection.Close();
      cmd.Connection = t.DbConnection;
      cmd.Transaction = t.DbTrans;
      DbDataReader reader = cmd.ExecuteReader();
      DataTable dt = new DataTable();
      return reader;
    }
    public int ExecuteNonQuery(DbCommand cmd, Trans1 t)
    {
      cmd.Connection.Close();
      cmd.Connection = t.DbConnection;
      cmd.Transaction = t.DbTrans;
      int ret = cmd.ExecuteNonQuery();
      return ret;
    }
    public object ExecuteScalar(DbCommand cmd, Trans1 t)
    {
      cmd.Connection.Close();
      cmd.Connection = t.DbConnection;
      cmd.Transaction = t.DbTrans;
      object ret = cmd.ExecuteScalar();
      return ret;
    }
    #endregion
  }

  //执行事务操作对象类
  public class Trans1 : IDisposable
  {
    private DbConnection conn;
    private DbTransaction dbTrans;
    public DbConnection DbConnection
    {
      get { return this.conn; }
    }
    public DbTransaction DbTrans
    {
      get { return this.dbTrans; }
    }
    public Trans1()
    {
      conn = DbHelper.CreateConnection();
      conn.Open();
      dbTrans = conn.BeginTransaction();
    }
    public Trans1(string connectionString)
    {
      conn = DbHelper.CreateConnection(connectionString);
      conn.Open();
      dbTrans = conn.BeginTransaction();
    }
    public void Commit()
    {
      dbTrans.Commit();
      this.Colse();
    }
    public void RollBack()
    {
      dbTrans.Rollback();
      this.Colse();
    }
    public void Dispose()
    {
      this.Colse();
    }
    public void Colse()
    {
      if (conn.State == System.Data.ConnectionState.Open)
      {
        conn.Close();
      }
    }

    #region MD5　32位加密

    /// <summary>
    /// MD5　32位加密
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string md5(string s)
    {
      System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
      byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
      bytes = md5.ComputeHash(bytes);
      md5.Clear();

      string ret = "";
      for (int i = 0; i < bytes.Length; i++)
      {
        ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
      }
      return ret.PadLeft(32, '0');
    }

    #endregion
  }
}
