using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace ProduceManager
{
    public class ExeclImportHelper
    {
        private static ExeclImportHelper Instance = null;

        private ExeclImportHelper()
        {
        }

        public static ExeclImportHelper getInstance()
        {
            if (Instance == null)
            {
                Instance = new ExeclImportHelper();
            }
            return Instance;
        }

        public DataTable GetProdDataTable_Form_ImportExcel(DataView dtMap, Dictionary<string, DataTable> dtChg)
        {
            OpenFileDialog _openDlg = new OpenFileDialog();
            _openDlg.Title = "选择Excel文件";
            _openDlg.Filter = "Excel文档(*.xls,*.xlsx,*.et)|*.xls;*.xlsx;*.et";//Image Files (*.bmp, *.jpg)|*.bmp;*.jpg
            if (_openDlg.ShowDialog() == DialogResult.OK)
            {
                return GetProdDataTable_Form_ImportExcelByMap(_openDlg.FileName, dtMap, dtChg);
            }
            return null;
        }
        public DataTable GetProdDataTable_Form_ImportExcelByMap(string fileName, DataView dtMap, Dictionary<string, DataTable> dtChg)
        {
            try
            {
                ExcelHelper helper = new ExcelHelper(fileName);
                DataTable _dt = helper.ExportExcelToDataTable();
                helper = null;

                string _fieldName = string.Empty;
                string strDelField = string.Empty;
                foreach (DataRowView drv in dtMap)
                {
                    if (drv["IsNullDelete"].ToString() == "True")
                        strDelField = drv["Field"].ToString();

                    if (drv["IsVisible"].ToString() == "False")
                        continue;

                    _fieldName = drv["ExcelTitle"].ToString();
                    if (_dt.Columns.IndexOf(_fieldName) == -1)
                    {
                        MessageBox.Show(string.Format("Excel文件格式错误,缺少:{0}.", _fieldName));
                        return null;
                    }
                    else
                    {
                        _dt.Columns[_fieldName].ColumnName = drv["Field"].ToString();
                    }
                }
                if (strDelField != string.Empty)
                {
                    for (int i = _dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = _dt.Rows[i];
                        if (dr[strDelField] == DBNull.Value || dr[strDelField].ToString().Trim() == string.Empty)
                        {
                            dr.Delete();
                        }
                    }
                    _dt.AcceptChanges();
                }

                if (!DataValueChg(_dt, dtMap, dtChg))
                {
                    return null;
                }
                _dt.AcceptChanges();
                return _dt;
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("Excel文件读取错误.{0}", e));
                return null;
            }
        }
        private bool DataValueChg(DataTable _dt, DataView dtMap, Dictionary<string, DataTable> dtChg)
        {
            string _fieldName = string.Empty, _cellVal = string.Empty;
            decimal _decVal = 0;
            int _intVal = 0;
            DateTime _dtVal;
            DataRow _dr = null;
            for (int _r = 0, _rCnt = _dt.Rows.Count; _r < _rCnt; _r++)
            {
                _dr = _dt.Rows[_r];
                foreach (DataRowView row in dtMap)
                {
                    if (row["IsVisible"].ToString() == "False")
                        continue;

                    _fieldName = row["Field"].ToString();
                    _cellVal = _dr[_fieldName].ToString();
                    switch (row["Type"].ToString().ToLower())
                    {
                        case "decimal":
                            if (decimal.TryParse(_cellVal, out _decVal))
                            {
                                _dr[_fieldName] = _decVal;
                            }
                            else
                            {
                                MessageBox.Show("文件第 " + (_r + 2).ToString() + " 行“" + row["ExcelTitle"].ToString() + "” 格式不正确.");
                                return false;
                            }
                            break;

                        case "int":
                            if (int.TryParse(_cellVal, out _intVal))
                            {
                                _dr[_fieldName] = _intVal;
                            }
                            else
                            {
                                MessageBox.Show("文件第 " + (_r + 2).ToString() + " 行“" + row["ExcelTitle"].ToString() + "” 格式不正确.");
                                return false;
                            }
                            break;

                        case "datetime":
                            try
                            {
                                if (!DateTime.TryParse(_cellVal, out _dtVal))
                                {
                                    _dtVal = DateTime.FromOADate(double.Parse(_cellVal));
                                }
                                _dr[_fieldName] = _dtVal.ToString();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("文件第 " + (_r + 2).ToString() + " 行“" + row["ExcelTitle"].ToString() + "” 格式不正确.");
                                return false;
                            }
                            break;

                        default:
                            break;
                    }
                }

                foreach (DataRowView row in dtMap)
                {
                    string strKey = row["ChkKey"].ToString();
                    if (strKey == string.Empty)
                        continue;

                    string strChgFilter = string.Empty;
                    string strErrorTitle = string.Empty;
                    string[] strChgs = row["ChkFields"].ToString().Split(",".ToCharArray());
                    bool blNoChk = false;
                    foreach (string strChg in strChgs)
                    {
                        if (_dr[strChg].ToString() == string.Empty)
                        {
                            blNoChk = true;
                            break;
                        }

                        if (strChgFilter == string.Empty)
                        {
                            strChgFilter = strChg + "='" + _dr[strChg].ToString() + "'";
                        }
                        else
                        {
                            strChgFilter += " AND " + strChg + "='" + _dr[strChg].ToString() + "'";
                        }

                        DataRow[] drTitles = dtMap.Table.Select("Field='" + strChg + "'");
                        if (drTitles.Length > 0)
                        {
                            DataRow drTitle = drTitles[0];
                            if (strErrorTitle == string.Empty)
                            {
                                strErrorTitle = drTitle["ExcelTitle"].ToString();
                            }
                            else
                            {
                                strErrorTitle += "," + drTitle["ExcelTitle"].ToString();
                            }
                        }
                    }
                    if (blNoChk)
                    {
                        string strId = row["Field"].ToString();
                        if (!_dt.Columns.Contains(strId))
                        {
                            _dt.Columns.Add(strId, Type.GetType("System.String"));
                        }
                        _dr[strId] = DBNull.Value;
                        continue;
                    }

                    DataTable dtKey = dtChg[strKey];
                    DataRow[] drKeys = dtKey.Select(strChgFilter);
                    if (drKeys.Length != 1)
                    {
                        MessageBox.Show("数据检查不通过，第 " + (_r + 2).ToString() + " 行 “" + strErrorTitle + "” 数据有误.");
                        return false;
                    }
                    string strKeyId = row["Field"].ToString();
                    if (!_dt.Columns.Contains(strKeyId))
                    {
                        _dt.Columns.Add(strKeyId, Type.GetType("System.String"));
                    }
                    _dr[strKeyId] = drKeys[0][strKeyId];
                }
            }

            return true;
        }


        //public DataTable GetProdDataTable_Form_ImportExcel(DataView dtMap, Dictionary<string, DataTable> dtChg)
        //{
        //    OpenFileDialog _openDlg = new OpenFileDialog();
        //    _openDlg.Title = "选择Excel文件";
        //    _openDlg.Filter = "Excel文档(*.xls,*.xlsx)|*.xls;*.xlsx";//Image Files (*.bmp, *.jpg)|*.bmp;*.jpg
        //    if (_openDlg.ShowDialog() == DialogResult.OK)
        //    {
        //        return GetProdDataTable_Form_ImportExcelByMap(_openDlg.FileName, dtMap, dtChg);
        //    }
        //    return null;
        //}
        //public DataTable GetProdDataTable_Form_ImportExcelByMap(string fileName, DataView dtMap, Dictionary<string, DataTable> dtChg)
        //{
        //    try
        //    {
        //        string conn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Persist Security Info=True;Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';", fileName);
        //        OleDbConnection cnn = new OleDbConnection(conn);
        //        cnn.Open();
        //        DataTable dt = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null); //当excel没有打开的时候多一个_xlnm#_FilterDatabase，抽象呀
        //        string strSheetName = string.Empty;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            strSheetName = dt.Rows[i]["TABLE_NAME"].ToString();
        //            if (strSheetName.IndexOf("$") != -1)
        //            {
        //                break;
        //            }
        //        }
        //        cnn.Close();

        //        ShareAccessManager _sbmgr = new ShareAccessManager(fileName);
        //        DataTable _dt = (DataTable)_sbmgr.ExecDbCommand(
        //            "SELECT * FROM [" + strSheetName + "]",
        //            new string[] { },
        //            new string[] { },
        //            "Table");
        //        string _fieldName = string.Empty;
        //        string strDelField = string.Empty;
        //        foreach (DataRowView drv in dtMap)
        //        {
        //            if (drv["IsNullDelete"].ToString() == "True")
        //                strDelField = drv["Field"].ToString();

        //            if (drv["IsVisible"].ToString() == "False")
        //                continue;

        //            _fieldName = drv["ExcelTitle"].ToString();
        //            if (_dt.Columns.IndexOf(_fieldName) == -1)
        //            {
        //                MessageBox.Show(string.Format("Excel文件格式错误,缺少:{0}.", _fieldName));
        //                return null;
        //            }
        //            else
        //            {
        //                _dt.Columns[_fieldName].ColumnName = drv["Field"].ToString();
        //            }
        //        }
        //        if (strDelField != string.Empty)
        //        {
        //            for (int i = _dt.Rows.Count - 1; i >= 0; i--)
        //            {
        //                DataRow dr = _dt.Rows[i];
        //                if (dr[strDelField] == DBNull.Value || dr[strDelField].ToString().Trim() == string.Empty)
        //                {
        //                    dr.Delete();
        //                }
        //            }
        //            _dt.AcceptChanges();
        //        }

        //        if (!DataValueChg(_dt, dtMap, dtChg))
        //        {
        //            return null;
        //        }
        //        _dt.AcceptChanges();
        //        return _dt;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(string.Format("Excel文件读取错误.{0}", e));
        //        return null;
        //    }
        //}
        //private bool DataValueChg(DataTable _dt, DataView dtMap, Dictionary<string, DataTable> dtChg)
        //{
        //    string _fieldName = string.Empty, _cellVal = string.Empty;
        //    decimal _decVal = 0;
        //    int _intVal = 0;
        //    DateTime _dtVal;
        //    DataRow _dr = null;
        //    for (int _r = 0, _rCnt = _dt.Rows.Count; _r < _rCnt; _r++)
        //    {
        //        _dr = _dt.Rows[_r];
        //        foreach (DataRowView row in dtMap)
        //        {
        //            if (row["IsVisible"].ToString() == "False")
        //                continue;

        //            _fieldName = row["Field"].ToString();
        //            _cellVal = _dr[_fieldName].ToString();
        //            switch (row["Type"].ToString().ToLower())
        //            {
        //                case "decimal":
        //                    if (decimal.TryParse(_cellVal, out _decVal))
        //                    {
        //                        _dr[_fieldName] = _decVal;
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("文件第 " + (_r + 2).ToString() + " 行“" + row["ExcelTitle"].ToString() + "” 格式不正确.");
        //                        return false;
        //                    }
        //                    break;

        //                case "int":
        //                    if (int.TryParse(_cellVal, out _intVal))
        //                    {
        //                        _dr[_fieldName] = _intVal;
        //                    }
        //                    else
        //                    {
        //                        MessageBox.Show("文件第 " + (_r + 2).ToString() + " 行“" + row["ExcelTitle"].ToString() + "” 格式不正确.");
        //                        return false;
        //                    }
        //                    break;

        //                case "datetime":
        //                    try
        //                    {
        //                        if (!DateTime.TryParse(_cellVal, out _dtVal))
        //                        {
        //                            _dtVal = DateTime.FromOADate(double.Parse(_cellVal));
        //                        }
        //                        _dr[_fieldName] = _dtVal.ToString();
        //                    }
        //                    catch (Exception)
        //                    {
        //                        MessageBox.Show("文件第 " + (_r + 2).ToString() + " 行“" + row["ExcelTitle"].ToString() + "” 格式不正确.");
        //                        return false;
        //                    }
        //                    break;

        //                default:
        //                    break;
        //            }
        //        }

        //        foreach (DataRowView row in dtMap)
        //        {
        //            string strKey = row["ChkKey"].ToString();
        //            if (strKey == string.Empty)
        //                continue;

        //            string strChgFilter = string.Empty;
        //            string strErrorTitle = string.Empty;
        //            string[] strChgs = row["ChkFields"].ToString().Split(",".ToCharArray());
        //            bool blNoChk = false;
        //            foreach (string strChg in strChgs)
        //            {
        //                if (_dr[strChg].ToString() == string.Empty)
        //                {
        //                    blNoChk = true;
        //                    break;
        //                }

        //                if (strChgFilter == string.Empty)
        //                {
        //                    strChgFilter = strChg + "='" + _dr[strChg].ToString() + "'";
        //                }
        //                else
        //                {
        //                    strChgFilter += " AND " + strChg + "='" + _dr[strChg].ToString() + "'";
        //                }

        //                DataRow[] drTitles = dtMap.Table.Select("Field='" + strChg + "'");
        //                if (drTitles.Length > 0)
        //                {
        //                    DataRow drTitle = drTitles[0];
        //                    if (strErrorTitle == string.Empty)
        //                    {
        //                        strErrorTitle = drTitle["ExcelTitle"].ToString();
        //                    }
        //                    else
        //                    {
        //                        strErrorTitle += "," + drTitle["ExcelTitle"].ToString();
        //                    }
        //                }
        //            }
        //            if (blNoChk)
        //            {
        //                string strId = row["Field"].ToString();
        //                if (!_dt.Columns.Contains(strId))
        //                {
        //                    _dt.Columns.Add(strId, Type.GetType("System.String"));
        //                }
        //                _dr[strId] = DBNull.Value;
        //                continue;
        //            }

        //            DataTable dtKey = dtChg[strKey];
        //            DataRow[] drKeys = dtKey.Select(strChgFilter);
        //            if (drKeys.Length != 1)
        //            {
        //                MessageBox.Show("数据检查不通过，第 " + (_r + 2).ToString() + " 行 “" + strErrorTitle + "” 数据有误.");
        //                return false;
        //            }
        //            string strKeyId = row["Field"].ToString();
        //            if (!_dt.Columns.Contains(strKeyId))
        //            {
        //                _dt.Columns.Add(strKeyId, Type.GetType("System.String"));
        //            }
        //            _dr[strKeyId] = drKeys[0][strKeyId];
        //        }
        //    }

        //    return true;
        //}
    }
}
