using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Data;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;
using DotNetSpeech;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;
using System.ComponentModel;
using System.Configuration;
using DevExpress.XtraBars;
using DevExpress.XtraTab;
using Microsoft.Reporting.WinForms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace ProduceManager
{
    public class StaticFunctions
    {
        public static string GetFrmParamValue(string strParam, string strParamName, char[] arPChar)
        {
            if (strParam != string.Empty && strParamName != string.Empty)
            {
                strParam = strParam.ToUpper();
                strParamName = strParamName.ToUpper();
                if (arPChar == null)
                    arPChar = new char[] { ';', ',', '&', '|' };
                string[] strArrParam = strParam.Split(arPChar);
                for (int i = 0; i < strArrParam.Length; i++)
                {
                    if (strArrParam[i].Length > 0)
                    {
                        int r = strArrParam[i].IndexOf(strParamName + "=");
                        if (r != -1)
                            return strArrParam[i].Substring(r + (strParamName + "=").Length);
                    }
                }
            }
            return string.Empty;
        }

        public static Form GetExistedChildForm(Form ParentForm, string sFormText)
        {
            Form[] charr = ParentForm.MdiChildren;
            Form FormTem = null;

            foreach (Form chform in charr)
            {
                if (chform.Name.ToUpper() == sFormText.ToUpper())
                {
                    if (!chform.Visible)
                        chform.Show();
                    chform.Activate();

                    if (((frmEditorBase)chform).frmEditorMode == "VIEW")
                    {
                        return chform;
                    }
                    else
                    {
                        FormTem = chform;
                    }
                }
            }
            return FormTem;
        }

        public static frmReportServicePreview GetExistedChildReptForm(Form ParentForm, string sFormText)
        {
            Form[] charr = ParentForm.MdiChildren;
            frmReportServicePreview FormTem = null;

            foreach (Form chform in charr)
            {
                if (chform is frmReportServicePreview)
                {
                    frmReportServicePreview frmRept = chform as frmReportServicePreview;
                    if (frmRept.Rept_Key.ToUpper() == sFormText.ToUpper())
                    {
                        return frmRept;
                    }
                }
            }
            return FormTem;
        }

        public static frmRS GetExistedChildRsForm(Form ParentForm, string sFormText)
        {
            Form[] charr = ParentForm.MdiChildren;
            frmRS FormTem = null;

            foreach (Form chform in charr)
            {
                if (chform is frmRS)
                {
                    frmRS frmRept = chform as frmRS;
                    if (frmRept.Rept_Key.ToUpper() == sFormText.ToUpper())
                    {
                        return frmRept;
                    }
                }
            }
            return FormTem;
        }

        public static frmRpt_SysRptItem GetExistedChildRptItemForm(Form ParentForm, string sFormText)
        {
            Form[] charr = ParentForm.MdiChildren;
            frmRpt_SysRptItem FormTem = null;

            foreach (Form chform in charr)
            {
                if (chform is frmRpt_SysRptItem)
                {
                    frmRpt_SysRptItem frmRept = chform as frmRpt_SysRptItem;
                    if (frmRept.Rept_Key.ToUpper() == sFormText.ToUpper())
                    {
                        return frmRept;
                    }
                }
            }
            return FormTem;
        }

        public static Form GetExistedChildForm(Form ParentForm, string sFormText, bool blShowForm)
        {
            Form[] charr = ParentForm.MdiChildren;
            Form FormTem = null;

            //for each child form set the window state to Maximized
            foreach (Form chform in charr)
            {
                if (chform.Name.ToUpper() == sFormText.ToUpper())
                {
                    if (!chform.Visible && blShowForm)
                    {
                        chform.Show();
                        chform.Activate();
                    }

                    if (((frmEditorBase)chform).frmEditorMode == "VIEW")
                    {
                        return chform;
                    }
                    else
                    {
                        FormTem = chform;
                    }
                }
            }
            return FormTem;
        }

        private static void OpenEditorForm(Form frmChildForm, string Mode, string strParam, Form ParentForm)
        {
            ((frmEditorBase)frmChildForm).MdiParent = ParentForm;
            try
            {
                ((frmEditorBase)frmChildForm).InitialByParam(Mode, strParam);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "窗口初始化错误提示");
            }
            ((frmEditorBase)frmChildForm).Show();
        }

        private static void OpenEditorForm(Form frmChildForm, string Mode, string strParam, Form ParentForm, DataTable dt)
        {
            ((frmEditorBase)frmChildForm).MdiParent = ParentForm;
            try
            {
                ((frmEditorBase)frmChildForm).InitialByParam(Mode, strParam, dt);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "窗口初始化错误提示");
            }
            ((frmEditorBase)frmChildForm).Show();
        }


        /// <summary>创建主页面的一个MdiChildForm，OpenChildEditorForm(false,"ProduceManager", this.ParentForm, "FormText", "Logistic_Package_Marking", "", "", "")
        /// 
        /// </summary>
        /// <param name="blRight">是否验证页面权限</param>
        /// <param name="strPrPart">程序集名:ProduceManager</param>
        /// <param name="ParentForm">主页面类,一般传this.ParentForm</param>
        /// <param name="strCaption">页面Text</param>
        /// <param name="formName">form的类名：Logistic_Package_Marking</param>
        /// <param name="strMode">设置页面frmEditorMode，一般VIEW</param>
        /// <param name="strFormParam">页面的frmParam,参数信息：MODE=VIEW</param>
        /// <returns></returns>
        public static frmEditorBase OpenChildEditorForm(bool blRight, string strPrPart, Form ParentForm, string strCaption, string formName, string strMode, string strFormParam)
        {
            return OpenChildEditorForm(blRight, strPrPart, ParentForm, strCaption, formName, strMode, strFormParam, null);
        }

        /// <summary>创建主页面的一个MdiChildForm，OpenChildEditorForm(false,"ProduceManager", this.ParentForm, "FormText", "Logistic_Package_Marking", "", "", "");
        /// 
        /// </summary>
        /// <param name="blRight">是否验证页面权限</param>
        /// <param name="strPrPart">程序集名:ProduceManager</param>
        /// <param name="ParentForm">主页面类,一般传this.ParentForm</param>
        /// <param name="strCaption">页面Text</param>
        /// <param name="formName">form的类名：Logistic_Package_Marking</param>
        /// <param name="strMode">设置页面frmEditorMode，一般VIEW</param>
        /// <param name="strFormParam">页面的frmParam，其他的参数信息：MODE=VIEW</param>
        /// <param name="dt">页面的frmDataTable</param>
        /// <returns></returns>
        public static frmEditorBase OpenChildEditorForm(bool blRight, string strPrPart, Form ParentForm, string strCaption, string formName, string strMode, string strFormParam, DataTable dt)
        {
            try
            {
                if (strPrPart == string.Empty)
                    return null;

                string strAllow = string.Empty;
                if (blRight)
                {
                    DataRow[] drs = CApplication.App.DtAllowMenus.Select("Menus_Class = '" + formName + "'");
                    if (drs.Length <= 0)
                    {
                        throw new Exception("你没有访问该页面的权限！");
                    }
                    else
                    {
                        strAllow = drs[0]["Allowed_Operator"].ToString();
                    }
                }

                Form[] charr = ParentForm.MdiChildren;
                Form frmChildForm;


                frmChildForm = GetExistedChildForm(ParentForm, formName);

                if (frmChildForm == null)
                {
                    //如果不存在则用反射创建form窗体实例。
                    Assembly asm = Assembly.Load(strPrPart);//程序集名"MC.Fds"
                    object frmObj = asm.CreateInstance(strPrPart + "." + formName, true);//程序集+form的类名。

                    frmEditorBase frms = (frmEditorBase)frmObj;
                    frms.frmAllowOperatorList = strAllow;

                    if (blRight)
                    {
                        string[] _allowOpList = strAllow.Split(';');
                        foreach (string _allowOp in _allowOpList)
                        {
                            if (!_allowOp.Trim().Equals(""))
                            {
                                Control[] tp_ctls = frms.Controls.Find(_allowOp.Split('=')[0], true);
                                foreach (Control tp_ctl in tp_ctls)
                                {
                                    tp_ctl.Visible = true;
                                }
                            }
                        }
                    }

                    if (strCaption != string.Empty)
                        frms.Text = strCaption;
                    if (dt != null)
                        OpenEditorForm(frms, strMode, strFormParam, ParentForm, dt);
                    else
                        OpenEditorForm(frms, strMode, strFormParam, ParentForm);

                    return frms;
                }
                else //if ((frmChildForm as frmEditorBase).frmEditorMode == "VIEW")
                {
                    if (dt != null)
                        ((frmEditorBase)frmChildForm).InitialByParam(strMode, strFormParam, dt);
                    else
                        ((frmEditorBase)frmChildForm).InitialByParam(strMode, strFormParam);

                    return (frmChildForm as frmEditorBase);
                }
                //else
                //{
                //    (frmChildForm as frmEditorBase).Show();
                //    return (frmChildForm as frmEditorBase);
                //}
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "错误提示");
                return null;
            }
        }

        /// <summary>创建一个DialogForm，OpenDialogForm("ProduceManager", "FormText", "Logistic_Package_Marking", "", "");
        /// 
        /// </summary>
        /// <param name="blRight">是否验证页面权限</param>
        /// <param name="strPrPart">程序集名:ProduceManager</param>
        /// <param name="strCaption">页面Text</param>
        /// <param name="formName">form的类名：Logistic_Package_Marking</param>
        /// <param name="strMode">设置页面frmEditorMode，一般VIEW</param>
        /// <param name="strFormParam">页面的frmParam，其他的参数信息：MODE=VIEW</param>
        /// <returns></returns>
        public static frmEditorBase OpenDialogForm(bool blRight, string strPrPart, string strCaption, string formName, string strMode, string strFormParam)
        {
            return OpenDialogForm(blRight, strPrPart, strCaption, formName, strMode, strFormParam, null);
        }

        /// <summary> 创建一个DialogForm，OpenDialogForm("ProduceManager", "FormText", "Logistic_Package_Marking", "", "");
        ///
        /// </summary>
        /// <param name="blRight">是否验证页面权限</param>
        /// <param name="strPrPart">程序集名:ProduceManager</param>
        /// <param name="strCaption">页面Text</param>
        /// <param name="formName">form的类名：Logistic_Package_Marking</param>
        /// <param name="strMode">设置页面frmEditorMode，一般VIEW</param>
        /// <param name="strFormParam">页面的frmParam，其他的参数信息：MODE=VIEW</param>
        /// <param name="dt">页面的frmDataTable</param>
        /// <returns></returns>
        public static frmEditorBase OpenDialogForm(bool blRight, string strPrPart, string strCaption, string formName, string strMode, string strFormParam, DataTable dt)
        {
            try
            {
                if (strPrPart == string.Empty)
                    return null;

                string strAllow = string.Empty;
                if (blRight)
                {
                    DataRow[] drs = CApplication.App.DtAllowMenus.Select("Menus_Class = '" + formName + "'");
                    if (drs.Length <= 0)
                    {
                        throw new Exception("你没有访问该页面的权限！");
                    }
                    else
                    {
                        strAllow = drs[0]["Allowed_Operator"].ToString();
                    }
                }

                Assembly asm = Assembly.Load(strPrPart);//程序集名"MC.Fds"
                object frmObj = asm.CreateInstance(strPrPart + "." + formName, true);//程序集+form的类名。

                frmEditorBase frms = (frmEditorBase)frmObj;
                frms.frmAllowOperatorList = strAllow;
                if (blRight)
                {
                    string[] _allowOpList = strAllow.Split(';');
                    foreach (string _allowOp in _allowOpList)
                    {
                        if (!_allowOp.Trim().Equals(""))
                        {
                            Control[] tp_ctls = frms.Controls.Find(_allowOp.Split('=')[0], true);
                            foreach (Control tp_ctl in tp_ctls)
                            {
                                tp_ctl.Visible = true;
                            }
                        }
                    }
                }
                if (strCaption != string.Empty)
                    frms.Text = strCaption;

                if (dt != null)
                    ((frmEditorBase)frms).InitialByParam(strMode, strFormParam, dt);
                else
                    ((frmEditorBase)frms).InitialByParam(strMode, strFormParam);

                return frms;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "错误提示");
                return null;
            }
        }

        /// <summary>绑定CheckedComboBoxEdit
        /// 
        /// </summary>
        /// <param name="ckb">CheckedComboBoxEdit</param>
        /// <param name="dt">数据源DataTable</param>
        /// <param name="strTxtFiled">显示值</param>
        /// <param name="strValueFiled">有效值</param>
        /// <param name="strSort">排序</param>
        /// <param name="strFilter">过滤</param>
        public static void BindCheckedComboBoxEdit(DevExpress.XtraEditors.CheckedComboBoxEdit ckb, DataTable dt, string strTxtFiled, string strValueFiled, string strSort, string strFilter)
        {
            ckb.Properties.Items.Clear();
            if (dt != null)
            {
                DataView dv = dt.Copy().DefaultView;
                dv.Sort = strSort;
                dv.RowFilter = strFilter;
                foreach (DataRowView row in dv)
                {
                    ckb.Properties.Items.Add(new DevExpress.XtraEditors.Controls.CheckedListBoxItem(row[strValueFiled].ToString().Trim(), row[strTxtFiled].ToString().Trim()));
                }
            }
        }

        public static void BindDplComboByTable(DevExpress.XtraEditors.ImageComboBoxEdit dplX,
            DataTable dt,
            string strTxtField,
            string strValueField,
            string strSort,
            string strFilter,
            bool blAddNone)
        {
            dplX.Properties.Items.Clear();
            if (blAddNone)
            {
                dplX.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ""));
            }
            string strFilterOld = dt.DefaultView.RowFilter;
            dt.DefaultView.RowFilter = strFilter;
            DataView dv = dt.DefaultView.ToTable().DefaultView;
            dv.Sort = strSort;
            foreach (DataRowView row in dv)
            {
                dplX.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(row[strTxtField].ToString().Trim(), row[strValueField].ToString()));
            }
            if (dplX.Properties.Items.Count > 0)
            {
                dplX.SelectedIndex = 0;
            }
            dt.DefaultView.RowFilter = strFilterOld;
        }

        public static void BindDplComboByTable(DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox dplX,
            DataTable dt,
            string strTxtField,
            string strValueField,
            string strSort,
            string strFilter,
            bool blAddNone)
        {
            dplX.Items.Clear();
            if (blAddNone)
            {
                dplX.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", ""));
            }
            string strFilterOld = dt.DefaultView.RowFilter;
            dt.DefaultView.RowFilter = strFilter;
            DataView dv = dt.DefaultView.ToTable().DefaultView;
            dv.Sort = strSort;
            foreach (DataRowView row in dv)
            {
                dplX.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(row[strTxtField].ToString().Trim(), row[strValueField].ToString()));
            }
            dt.DefaultView.RowFilter = strFilterOld;
        }

        public static void BindDplComboByTable(DevExpress.XtraEditors.ComboBoxEdit dplX,
            DataTable dt,
            string strValueField,
            string strSort,
            string strFilter)
        {
            dplX.Properties.Items.Clear();

            string strFilterOld = dt.DefaultView.RowFilter;
            dt.DefaultView.RowFilter = strFilter;
            DataView dv = dt.DefaultView.ToTable().DefaultView;
            dv.Sort = strSort;
            foreach (DataRowView drv in dv)
            {
                dplX.Properties.Items.Add(drv[strValueField].ToString());
            }
            dplX.EditValue = "";
            dt.DefaultView.RowFilter = strFilterOld;
        }

        public static void BindDplComboByTable(DevExpress.XtraEditors.LookUpEdit dplX,
            DataTable dt,
            string strTxtField,
            string strValueField,
            string[] strfieldNames,
            string[] strHeadTexts,
            bool blShowHeader,
            string strSort,
            string strFilter,
            bool blAddNone)
        {
            dplX.Properties.DataSource = null;
            dplX.Properties.Columns.Clear();

            int iW = 0;
            for (int i = 0; i < strfieldNames.Length; i++)
            {
                string[] strSplit = strfieldNames[i].Split("=".ToCharArray());
                if (strSplit.Length == 1)
                    dplX.Properties.Columns.Add(new LookUpColumnInfo(strfieldNames[i], strHeadTexts[i]));
                else
                {
                    int iTemp = int.Parse(strSplit[1]);
                    iW += iTemp;
                    dplX.Properties.Columns.Add(new LookUpColumnInfo(strSplit[0], iTemp, strHeadTexts[i]));
                }
            }
            dplX.Properties.PopupWidth = iW;
            dplX.Properties.ShowHeader = blShowHeader;
            dplX.Properties.ValueMember = strValueField;
            dplX.Properties.DisplayMember = strTxtField;

            string strFilterOld = dt.DefaultView.RowFilter;
            dt.DefaultView.RowFilter = strFilter;
            DataView dv = dt.DefaultView.ToTable().DefaultView;
            dv.Sort = strSort;
            if (blAddNone)
            {
                DataRowView dr = dv.AddNew();
                if (dv.Table.Columns.IndexOf("Number") != -1)
                {
                    dr["Number"] = " ";
                }
                dr[strTxtField] = "空";
                dr[strValueField] = "-9999";
            }

            dplX.Properties.DataSource = dv;
            dplX.EditValue = "-9999";
            dt.DefaultView.RowFilter = strFilterOld;
        }

        public static void BindDplComboByTable(ExtendControl.ExtPopupTree dplX,
            DataTable dt,
            string strTxtField,
            string strValueField,
            string strDorpTxtField,
            string[] strfieldNames,
            string[] strHeadTexts,
            string strSort,
            string strFilter,
            string strFilterField,
            string strKeyIdField,
            string strPKeyIdField,
            string strDataFilter,
            bool blAddNull)
        {
            string strFilterOld = dt.DefaultView.RowFilter;
            dt.DefaultView.RowFilter = strDataFilter;
            dt.DefaultView.Sort = strSort;
            DataTable dtCust = dt.DefaultView.ToTable();
            if (blAddNull)
            {
                DataRow dr = dtCust.NewRow();
                dr[strTxtField] = "";
                dr[strFilterField] = " ";
                dr[strKeyIdField] = "-9999";
                dr[strPKeyIdField] = "-9999";
                dtCust.Rows.InsertAt(dr, 0);
            }
            dtCust.AcceptChanges();
            dt.DefaultView.RowFilter = strFilterOld;
            dplX.DataBind(dtCust, strTxtField, strValueField, strDorpTxtField, strSort, strfieldNames, strHeadTexts);
        }

        public static void BindDplComboByTable(ExtendControl.ExtPopupTree dplX,
            DataTable dt,
            string strTxtField,
            string strValueField,
            string strDorpTxtField,
            string[] strfieldNames,
            string[] strHeadTexts,
            string strSort,
            string strFilter,
            string strFilterField,
            string strKeyIdField,
            string strPKeyIdField,
            string strDataFilter)
        {
            BindDplComboByTable(dplX, dt, strTxtField, strValueField, strDorpTxtField, strfieldNames, strHeadTexts,
                strSort, strFilter, strFilterField, strKeyIdField, strPKeyIdField, strDataFilter, true);
        }

        public static void BindDplComboByTable(ProduceManager.UcTxtPopup dplX,
            DataTable dt,
            string strTxtField,
            string strValueField,
            string[] strfieldNames,
            string[] strHeadTexts,
            string[] strFilter,
            string strSort,
            string strNullValue,
            string strDataFilter,
            Point pSize,
            bool blAddNull)
        {
            dplX.BindDplComboByTable(dt, strTxtField, strValueField, strfieldNames, strHeadTexts, strFilter, strSort, strNullValue, strDataFilter, pSize, blAddNull);
        }

        public static void BindDplComboByTable(ProduceManager.UcTxtPopup dplX,
            DataTable dt,
            string strTxtField,
            string strValueField,
            string[] strfieldNames,
            string[] strHeadTexts,
            string[] strFilter,
            string strSort,
            string strNullValue,
            string strDataFilter,
            Point pSize)
        {
            dplX.BindDplComboByTable(dt, strTxtField, strValueField, strfieldNames, strHeadTexts, strFilter, strSort, strNullValue, strDataFilter, pSize, true);
        }

        public static void BindDplComboByTable(ProduceManager.UcTreeList dplX,
            DataTable dt,
            string strTxtField,
            string strValueField,
            string[] strfieldNames,
            string[] strHeadTexts,
            string strDataFilter,
            Point pSize)
        {
            BindDplComboByTable(dplX,dt, strTxtField, strValueField, strfieldNames, strHeadTexts, string.Empty, strDataFilter, pSize);
        }

        public static void BindDplComboByTable(ProduceManager.UcTreeList dplX,
            DataTable dt,
            string strTxtField,
            string strValueField,
            string[] strfieldNames,
            string[] strHeadTexts,
            string strSort,
            string strDataFilter,
            Point pSize)
        {
          dplX.BindDplComboByTable(dt, strTxtField, strValueField, strfieldNames, strHeadTexts, strSort, strDataFilter, pSize);
        }

        public static void BindDplComboByTable(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit dplX,
            DataTable dt,
            string strTxtField,
            string strValueField,
            string[] strfieldNames,
            string[] strHeadTexts,
            bool blShowHeader,
            string strSort,
            string strFilter,
            bool blAddNone)
        {
            dplX.DataSource = null;
            dplX.Columns.Clear();
            for (int i = 0; i < strfieldNames.Length; i++)
            {
                dplX.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(strfieldNames[i], strHeadTexts[i]));
            }
            dplX.ShowHeader = blShowHeader;
            dplX.ValueMember = strValueField;
            dplX.DisplayMember = strTxtField;

            string strFilterOld = dt.DefaultView.RowFilter;
            dt.DefaultView.RowFilter = strFilter;
            DataView dv = dt.DefaultView.ToTable().DefaultView;
            dv.Sort = strSort;
            if (blAddNone)
            {
                DataRowView dr = dv.AddNew();
                if (dv.Table.Columns.IndexOf("Number") != -1)
                {
                    dr["Number"] = " ";
                }
                dr[strTxtField] = "空";
                dr[strValueField] = "-9999";
            }
            dplX.DataSource = dv;
            dt.DefaultView.RowFilter = strFilterOld;
        }

        public static void CopyDataRow(DataRow fromRow, DataRow toRow)
        {
            foreach (DataColumn dcl in fromRow.Table.Columns)
            {
                if (toRow.Table.Columns.IndexOf(dcl.ColumnName) < 0) continue;
                toRow[dcl.ColumnName] = fromRow[dcl.ColumnName];
            }
        }
        public static void CopyDataRow(DataRowView fromRow, DataRow toRow)
        {
            foreach (DataColumn dcl in fromRow.DataView.Table.Columns)
            {
                if (toRow.Table.Columns.IndexOf(dcl.ColumnName) < 0) continue;
                toRow[dcl.ColumnName] = fromRow[dcl.ColumnName];
            }
        }
        public static void CopyDataRow(DataRowView fromRow, DataRowView toRow)
        {
            foreach (DataColumn dcl in fromRow.DataView.Table.Columns)
            {
                if (toRow.DataView.Table.Columns.IndexOf(dcl.ColumnName) < 0) continue;
                toRow[dcl.ColumnName] = fromRow[dcl.ColumnName];
            }
        }
        public static void CopyDataRow(DataRow fromRow, DataRowView toRow)
        {
            foreach (DataColumn dcl in fromRow.Table.Columns)
            {
                if (toRow.DataView.Table.Columns.IndexOf(dcl.ColumnName) < 0) continue;
                toRow[dcl.ColumnName] = fromRow[dcl.ColumnName];
            }
        }

        public static void CopyDataTable(DataTable fromTable, DataTable toTable)
        {
            toTable.Clear();
            foreach (DataRow drw in fromTable.Rows)
            {
                DataRow drwTo = toTable.NewRow();
                foreach (DataColumn dcl in fromTable.Columns)
                {
                    if (toTable.Columns.IndexOf(dcl.ColumnName) < 0) continue;
                    drwTo[dcl.ColumnName] = drw[dcl.ColumnName];
                }
                toTable.Rows.Add(drwTo);
            }
            toTable.AcceptChanges();
        }

        public static string ConvertRMB(decimal num)
        {
            string strTmp = string.Empty;
            if (num < 0) strTmp = "负";
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾圆角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零圆整";
            }

            str5 = strTmp + str5;
            return str5;
        }

        /// <summary>
        /// 实现数据的四舍五入法
        /// </summary>
        /// <param name="v">要进行处理的数据</param>
        /// <param name="x">保留的小数位数</param>
        /// <param name="dSeed">逢dSeed进1,如果四舍五入则为0.5,逢8进1则为0.8</param>
        /// <returns>四舍五入后的结果</returns>
        public static double Round(double v, int x, double dSeed)
        {
            bool isNegative = false;
            //如果是负数
            if (v < 0)
            {
                isNegative = true;
                v = -v;
            }

            //int IValue = 1;
            //for (int i = 1; i <= x; i++)
            //{
            //    IValue = IValue * 10;
            //}
            //double Int = (double)Math.Round(v * IValue + (1 - dSeed), 0)

            int IValue = (int)(Math.Pow(10, x));
            double dValue = double.Parse((v * IValue + (1 - dSeed)).ToString());
            double Int = (double)((int)(dValue));
            v = Int / IValue;

            if (isNegative)
            {
                v = -v;
            }

            return v;
        }
        public static bool IsEquals(List<int> A, List<int> B)
        {
            if (A.Count != B.Count)
            {
                return false;
            }
            A.Sort();
            B.Sort();
            for (int i = 0; i < A.Count; i++)
            {
                if (!A[i].Equals(B[i]))
                    return false;
            }
            return true;
        }

        public static void CreateImageFile(string strPath, byte[] bytes)
        {
            if (bytes == null)
                return;

            FileStream fs = new FileStream(strPath, FileMode.Create, FileAccess.Write, FileShare.None);
            foreach (byte a in bytes)
            {
                fs.WriteByte(a);
            }
            fs.Close();
        }

        public static string GetStringX(string[] lisParentRow)
        {
            StringBuilder sbP = new StringBuilder(2000);
            sbP.Append("SELECT ");
            string strRow = string.Empty;
            foreach (string strVlaue in lisParentRow)
            {
                strRow += strRow == string.Empty ? "'" + strVlaue + "'" : ",'" + strVlaue + "'";
            }
            sbP.Append(strRow);
            return sbP.ToString();
        }

        public static string GetStringX(string[] dtFields, DataTable dt)
        {
            return GetStringX(dtFields, dt, string.Empty);
        }

        public static string GetStringX(string[] dtFields, DataTable dt, string strFilter)
        {
            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;

            string strFilterOld = dt.DefaultView.RowFilter;
            dt.DefaultView.RowFilter = strFilter;
            if (dt.DefaultView.Count == 0)
            {
                dt.DefaultView.RowFilter = strFilterOld;
                return string.Empty;
            }

            StringBuilder sbX = new StringBuilder(8000);
            foreach (DataRowView row in dt.DefaultView)
            {
                if (row.Row.RowState == DataRowState.Deleted)
                    continue;

                string strRow = string.Empty;
                foreach (string strField in dtFields)
                {
                    string strValue = row[strField] == DBNull.Value ? "" : row[strField].ToString();
                    strRow += strRow == string.Empty ? "SELECT '" + strValue + "'" : ",'" + strValue + "'";
                }
                sbX.Append(strRow);
                sbX.Append(" UNION ALL ");
            }
            sbX.Remove(sbX.Length - "UNION ALL ".Length, "UNION ALL ".Length);
            dt.DefaultView.RowFilter = strFilterOld;
            return sbX.ToString();
        }

        /// <summary>
        /// 清空面板中控件的值
        /// </summary>
        /// <param name="oContainerOfControls">承载控件的面板</param>
        public static void SetControlEmpty(Control oContainerOfControls)
        {
            SetControlEmpty(oContainerOfControls, string.Empty);
        }
        /// <summary>
        /// 清空面板中控件的值
        /// </summary>
        /// <param name="oContainerOfControls">承载控件的面板</param>
        /// <param name="strNoCluIds">控件ID列表，逗号分隔，此列表中的的对应ID的控件的值将不会被清空</param>
        public static void SetControlEmpty(Control oContainerOfControls, string strNoCluIds)
        {
            foreach (Control c in oContainerOfControls.Controls)
            {
                if (strNoCluIds != string.Empty && ("," + strNoCluIds + ",").IndexOf("," + c.Name + ",") != -1)
                    continue;

                switch (c.GetType().ToString())
                {
                    case "DevExpress.XtraEditors.LookUpEdit":
                        (c as DevExpress.XtraEditors.LookUpEdit).EditValue = null;
                        break;
                    case "DevExpress.XtraEditors.DateEdit":
                        (c as DevExpress.XtraEditors.DateEdit).EditValue = null;
                        break;
                    case "DevExpress.XtraEditors.TextEdit":
                        (c as DevExpress.XtraEditors.TextEdit).EditValue = string.Empty;
                        break;
                    case "DevExpress.XtraEditors.ImageComboBoxEdit":
                        (c as DevExpress.XtraEditors.ImageComboBoxEdit).EditValue = string.Empty;
                        break;
                    case "DevExpress.XtraEditors.CheckEdit":
                        (c as DevExpress.XtraEditors.CheckEdit).Checked = false;
                        break;
                    case "DevExpress.XtraEditors.MemoEdit":
                        (c as DevExpress.XtraEditors.MemoEdit).EditValue = string.Empty;
                        break;
                    case "DevExpress.XtraEditors.CheckedComboBoxEdit":
                        DevExpress.XtraEditors.CheckedComboBoxEdit chb = c as DevExpress.XtraEditors.CheckedComboBoxEdit;
                        chb.Text = string.Empty;
                        chb.EditValue = null;
                        chb.RefreshEditValue();
                        break;
                    case "ExtendControl.ExtPopupTree":
                        ExtendControl.ExtPopupTree ext = c as ExtendControl.ExtPopupTree;
                        ext.EditValue = null;
                        break;
                    case "ProduceManager.UcTxtPopup":
                        ProduceManager.UcTxtPopup ucp = c as ProduceManager.UcTxtPopup;
                        ucp.EditValue = string.Empty;
                        break;
                    case "ProduceManager.UcTreeList":
                        ProduceManager.UcTreeList uct = c as ProduceManager.UcTreeList;
                        uct.EditValue = string.Empty;
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 设置面板控件的可编辑性
        /// </summary>
        /// <param name="oContainerOfControls">承载控件的面板</param>
        /// <param name="blEnable">可编辑为true，只读为false</param>
        public static void SetControlEnable(Control oContainerOfControls, bool blEnable)
        {
            SetControlEnable(oContainerOfControls, blEnable, string.Empty);
        }
        /// <summary>
        /// 设置面板控件的可编辑性
        /// </summary>
        /// <param name="oContainerOfControls">承载控件的面板</param>
        /// <param name="blEnable">可编辑为true，只读为false</param>
        /// <param name="strNoCluIds">控件ID列表，逗号分隔，此列表中的的对应ID的控件的可编辑性将不会被设置</param>
        public static void SetControlEnable(Control oContainerOfControls, bool blEnable, string strNoCluIds)
        {
            foreach (Control c in oContainerOfControls.Controls)
            {
                if (strNoCluIds != string.Empty && ("," + strNoCluIds + ",").IndexOf("," + c.Name + ",") != -1)
                    continue;

                if (c is DevExpress.XtraEditors.BaseEdit)
                {
                    ((DevExpress.XtraEditors.BaseEdit)c).Properties.ReadOnly = !blEnable;
                }
                else if (c is ProduceManager.UcTxtPopup)
                {
                    (c as ProduceManager.UcTxtPopup).ReadOnly = !blEnable;
                }
                else if (c is ProduceManager.UcTreeList)
                {
                    (c as ProduceManager.UcTreeList).ReadOnly = !blEnable;
                }
            }
        }
        /// <summary>
        /// 设置面板控件的可编辑性
        /// </summary>
        /// <param name="oContainerOfControls">承载控件的面板</param>
        /// <param name="blEnable">可编辑为true，只读为false</param>
        /// <param name="strNoCluIds">设置的控件ID列表</param>
        public static void SetControlEnableByIds(Control oContainerOfControls, bool blEnable, string strNoCluIds)
        {
            foreach (Control c in oContainerOfControls.Controls)
            {
                if (("," + strNoCluIds + ",").IndexOf("," + c.Name + ",") == -1)
                    continue;

                if (c is DevExpress.XtraEditors.BaseEdit)
                {
                    ((DevExpress.XtraEditors.BaseEdit)c).Properties.ReadOnly = !blEnable;
                }
                else if (c is ProduceManager.UcTxtPopup)
                {
                    (c as ProduceManager.UcTxtPopup).ReadOnly = !blEnable;
                }
                else if (c is ProduceManager.UcTreeList)
                {
                    (c as ProduceManager.UcTreeList).ReadOnly = !blEnable;
                }
            }
        }
        /// <summary>
        /// 绑定控件的EditValue到一选中的Datarow
        /// </summary>
        /// <param name="oContainerOfControls">承载控件的面板</param>
        /// <param name="dr">选中Datarow的DataView</param>
        public static void SetControlBindings(Control oContainerOfControls, DataView dr)
        {
            SetControlBindings(oContainerOfControls, dr, null);
        }
        public static void SetControlBindings(Control oContainerOfControls, DataView dr, DataRow drFoc)
        {
            foreach (Control c in oContainerOfControls.Controls)
            {
                if (Convert.ToString(c.Tag) == string.Empty)
                    continue;

                c.DataBindings.Clear();
                c.DataBindings.Add("EditValue", dr, c.Tag.ToString(), false, DataSourceUpdateMode.OnPropertyChanged);
                switch (c.GetType().ToString())
                {
                    case "DevExpress.XtraEditors.TextEdit":
                        break;
                    case "DevExpress.XtraEditors.CheckEdit":
                        break;
                    case "DevExpress.XtraEditors.LookUpEdit":
                        break;
                    case "DevExpress.XtraEditors.CheckedComboBoxEdit":
                        CheckedComboBoxEdit ckblis = c as DevExpress.XtraEditors.CheckedComboBoxEdit;
                        if (drFoc != null)
                        {
                            ckblis.EditValue = drFoc[c.Tag.ToString()].ToString();
                        }
                        ckblis.RefreshEditValue();
                        break;
                    case "DevExpress.XtraEditors.DateEdit":
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 把控件的值设置到DataRow对应的字段
        /// </summary>
        /// <param name="oContainerOfControls">承载控件的面板</param>
        /// <param name="dr">被赋值的DataRow</param>
        /// <param name="blAdd">为true时，赋值所有控件中EditValue不为空的部分，为false时只赋值被修改的控件的部分</param>
        public static void SetControlValue2DataRow(Control oContainerOfControls, DataRow dr, bool blAdd)
        {
            SetControlValue2DataRow(oContainerOfControls, dr, blAdd, string.Empty);
        }
        /// <summary>
        /// 把控件的值设置到DataRow对应的字段
        /// </summary>
        /// <param name="oContainerOfControls">承载控件的面板</param>
        /// <param name="dr">被赋值的DataRow</param>
        /// <param name="blAdd">为true时，赋值所有控件中EditValue不为空的部分，为false时只赋值被修改的控件的部分</param>
        /// <param name="strNoCluIds">控件ID列表，逗号分隔，此列表中的的对应ID的控件将不会被赋值</param>
        public static void SetControlValue2DataRow(Control oContainerOfControls, DataRow dr, bool blAdd, string strNoCluIds)
        {
            foreach (Control c in oContainerOfControls.Controls)
            {
                if (strNoCluIds != string.Empty && (strNoCluIds + ",").IndexOf(c.Name + ",") != -1)
                    continue;

                if (Convert.ToString(c.Tag) == string.Empty)
                    continue;

                if (dr.Table.Columns.IndexOf(c.Tag.ToString()) == -1)
                    continue;

                string strFiled = c.Tag.ToString();
                string strValue = string.Empty;
                if (c is BaseEdit)
                {
                    strValue = Convert.ToString((c as BaseEdit).EditValue);
                }
                else if (c is ProduceManager.UcTxtPopup)
                {
                    strValue = Convert.ToString((c as ProduceManager.UcTxtPopup).EditValue);
                }
                else if (c is ProduceManager.UcTreeList)
                {
                    strValue = Convert.ToString((c as ProduceManager.UcTreeList).EditValue);
                }
                else
                {
                    continue;
                }

                if (blAdd)
                {
                    if (strValue != string.Empty && strValue != "-9999")
                        dr[strFiled] = strValue;
                }
                else
                {
                    if (dr[strFiled, DataRowVersion.Original].ToString() == strValue)
                        continue;

                    if (strValue == string.Empty)
                    {
                        dr[strFiled] = DBNull.Value;
                    }
                    else
                    {
                        dr[strFiled] = strValue;
                    }
                }
            }
        }

        /// <summary>
        /// 把Datarow的值赋给控件
        /// </summary>
        /// <param name="oContainerOfControls">承载控件的面板</param>
        /// <param name="dr">赋值的DataRow</param>
        public static void SetDataRow2ControlValue(Control oContainerOfControls, DataRow dr)
        {
            SetDataRow2ControlValue(oContainerOfControls, dr, string.Empty);
        }
        /// <summary>
        /// 把Datarow的值赋给控件
        /// </summary>
        /// <param name="oContainerOfControls">承载控件的面板</param>
        /// <param name="dr">赋值的DataRow</param>
        /// <param name="strNoCluIds">控件ID列表，逗号分隔，此列表中的的对应ID的控件将不会被赋值</param>
        public static void SetDataRow2ControlValue(Control oContainerOfControls, DataRow dr, string strNoCluIds)
        {
            foreach (Control c in oContainerOfControls.Controls)
            {
                if (strNoCluIds != string.Empty && ("," + strNoCluIds + ",").IndexOf("," + c.Name + ",") != -1)
                    continue;

                if (Convert.ToString(c.Tag) == string.Empty)
                    continue;

                if (dr.Table.Columns.IndexOf(c.Tag.ToString()) == -1)
                    continue; 
                if (c is BaseEdit)
                {
                    (c as BaseEdit).EditValue = dr[c.Tag.ToString()];
                }
                else if (c is ProduceManager.UcTxtPopup)
                {
                    (c as ProduceManager.UcTxtPopup).EditValue = dr[c.Tag.ToString()];
                }
                else if (c is ProduceManager.UcTreeList)
                {
                    (c as ProduceManager.UcTreeList).EditValue = dr[c.Tag.ToString()];
                }
            }
        }

        /// <summary>
        /// 获取新增一行要插入数据库的字段值
        /// </summary>
        /// <param name="dr">新增的Datarow</param>
        /// <param name="strFileds">要插入数据库的字段数组</param>
        /// <param name="strField">真正引发要插入数据库的字段列表</param>
        /// <returns></returns>
        public static string GetAddValues(DataRow dr, string[] strFileds, out string strField)
        {
            StringBuilder sbFileds = new StringBuilder();
            StringBuilder sbValues = new StringBuilder();

            foreach (string strFiled in strFileds)
            {
                if (dr[strFiled] == DBNull.Value || dr[strFiled].ToString().Trim() == string.Empty
                    || dr[strFiled].ToString() == "-9999"
                   )
                    continue;

                sbFileds.Append("," + strFiled);
                sbValues.Append(",'" + dr[strFiled].ToString().Replace("'", "''") + "'");
            }
            if (sbValues.Length > 0)
            {
                strField = sbFileds.ToString().Remove(0, 1);
                return sbValues.ToString().Remove(0, 1);
            }
            strField = string.Empty;
            return string.Empty;
        }
        /// <summary>
        /// 获取修改一行要update数据库的语句
        /// </summary>
        /// <param name="dt">修改行所属的DataTable</param>
        /// <param name="dr">已修改的行</param>
        /// <param name="strFileds">要update数据库的字段数组</param>
        /// <returns></returns>
        public static string GetUpdateValues(DataTable dt, DataRow dr, string[] strFileds)
        {
            return GetUpdateValues(dt, dr, strFileds, true);
        }
        /// <summary>
        /// 获取修改一行要update数据库的语句
        /// </summary>
        /// <param name="dt">修改行所属的DataTable</param>
        /// <param name="dr">已修改的行</param>
        /// <param name="strFileds">要update数据库的字段数组</param>
        /// <param name="blEditOnly">为true时仅update被修改的值</param>
        /// <returns></returns>
        public static string GetUpdateValues(DataTable dt, DataRow dr, string[] strFileds, bool blEditOnly)
        {
            StringBuilder sbValues = new StringBuilder();

            foreach (string strFiled in strFileds)
            {
                if (blEditOnly && dr[strFiled].ToString() == dr[strFiled, DataRowVersion.Original].ToString())
                    continue;

                string strType = dt.Columns[strFiled].DataType.ToString();

                if (dr[strFiled].ToString().Trim() == string.Empty
                    || dr[strFiled].ToString() == "-9999")
                {
                    sbValues.Append("," + strFiled + "=default");
                }
                else
                {
                    sbValues.Append("," + strFiled + "='" + dr[strFiled].ToString().Replace("'", "''") + "'");
                }
            }
            if (sbValues.Length > 0)
            {
                return sbValues.ToString().Remove(0, 1);
            }
            return string.Empty;
        }

        public static void GridViewExportToExcel(GridView _gw, string _title, string[] strNoExcelFileds)
        {
            if (_gw.RowCount <= 0)
                return;

            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = string.Format("导出Excel: {0}", _title);
                dlg.FileName = _title;
                dlg.Filter = "Excel文档(*.xls,*.xlsx,*.et)|*.xls;*.xlsx;*.et";//Image Files (*.bmp, *.jpg)|*.bmp;*.jpg
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string fileName = dlg.FileName;
                    if (!_gw.OptionsSelection.MultiSelect)
                    {
                        _gw.OptionsSelection.MultiSelect = true;
                    }
                    _gw.SelectAll();
                    _gw.CopyToClipboard();
                    File.WriteAllText(dlg.FileName, Clipboard.GetText(), Encoding.Unicode);

                    if (strNoExcelFileds != null)
                    {
                        foreach (string strF in strNoExcelFileds)
                        {
                            _gw.Columns[strF].Visible = false;
                        }
                    }
                    _gw.OptionsPrint.AutoWidth = false;
                    //_gw.ExportToXls(fileName, new DevExpress.XtraPrinting.XlsExportOptions(true, true, true, true, true));
                    //_gw.ExportToXls(fileName, new DevExpress.XtraPrinting.XlsExportOptions(DevExpress.XtraPrinting.TextExportMode.Text, true, true, true, true));
                    //客户机必须有DevExpress.XtraPrinting.v9.1.dll，否则无法导出，而方法又不需要该dll

                    if (MessageBox.Show("已成功导出到：" + fileName + ",是否打开？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        Microsoft.Office.Interop.Excel.Application xApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                        xApp.Visible = true;
                        xApp.Workbooks._Open(fileName, Missing.Value, Missing.Value, Missing.Value,
                            Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                             Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                        System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(); 
                        info.FileName = fileName; 
                        try
                        {
                            System.Diagnostics.Process.Start(info);
                        }
                        catch (System.ComponentModel.Win32Exception we)
                        {
                            throw we; 
                        } 
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("出错:" + err.Message);
            }
            finally
            {
                if (strNoExcelFileds != null)
                {
                    foreach (string strF in strNoExcelFileds)
                    {
                        _gw.Columns[strF].Visible = true;
                    }
                }
            }
        }

        //public static string GetBatchUpdateValues(DataTable dt, Control oContainerOfControls, List<BaseEdit> cUpdCtrs)
        //{
        //    StringBuilder sbValues = new StringBuilder();
        //    foreach (Control c in oContainerOfControls.Controls)
        //    {
        //        if (Convert.ToString(c.Tag) == string.Empty)
        //            continue;

        //        if (dt.Columns.IndexOf(c.Tag.ToString()) == -1)
        //            continue;

        //        BaseEdit bc = c as BaseEdit;
        //        if (bc == null)
        //            continue;

        //        if (Convert.ToString(bc.EditValue) == string.Empty)
        //            continue;

        //        if (bc is DevExpress.XtraEditors.LookUpEdit && Convert.ToString(bc.EditValue) == "-9999")
        //            continue;

        //        sbValues.Append("," + c.Tag.ToString() + "='" + bc.EditValue.ToString() + "'");

        //        cUpdCtrs.Add(bc);
        //    }

        //    if (sbValues.Length > 0)
        //    {
        //        return sbValues.ToString().Remove(0, 1);
        //    }

        //    return string.Empty;
        //}

        // 把阿拉伯数字的金额转换为中文大写数字 
        public static string ConvertToChinese(double x)
        {
            string s = x.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            string d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            return Regex.Replace(d, ".", delegate(Match m) { return "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟萬億兆京垓秭穰"[m.Value[0] - '-'].ToString(); });
        }

        /// <summary>
        /// 语音报读,在页面中调用该方法会出问题，而复制代码则正常
        /// </summary>
        /// <param name="strMsg"></param>
        public static void ReadVoice(string strMsg)
        {
            SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            SpVoice Voice = new SpVoice();
            Voice.Voice = Voice.GetVoices(string.Empty, string.Empty).Item(0);
            Voice.Speak(strMsg, SpFlags);
        }
        /// <summary>
        /// 根据条件动态生成Grid列
        /// </summary>
        /// <param name="gv">被操作的GridView</param>
        /// <param name="dtShow">包含GridView列的DataTable</param>
        /// <param name="dtConst">GridView列中需要绑定下拉款的数据源DataTable</param>
        public static void ShowGridControl(GridView gv, DataTable dtShow, DataTable dtConst)
        {
            GridControl gridC = gv.GridControl as GridControl;
            //DataRow[] drs = dtShow.Select("GroupName='" + gv.Name + "'");
            dtShow.DefaultView.RowFilter = "GroupName='" + gv.Name + "'";
            foreach (DataRowView dr in dtShow.DefaultView)
            {
                int iwd = int.Parse(dr["ControlWidth"].ToString());
                int iHd = int.Parse(dr["ControlHeight"].ToString());
                string strCName = dr["ControlName"].ToString();
                string strCFiled = dr["ControlFiled"].ToString();
                string strCKey = dr["SetKey"].ToString();
                string strTitle = dr["ShowText"].ToString();

                GridColumn gc = new GridColumn();
                gv.Columns.Add(gc);
                gc.Caption = strTitle;
                gc.FieldName = strCFiled;
                gc.Name = strCName;
                //gc.OptionsColumn.AllowMove = false;
                bool blROnly = bool.Parse(dr["IsReadOnly"].ToString());
                gc.OptionsColumn.ReadOnly = blROnly;
                gc.OptionsColumn.AllowEdit = bool.Parse(dr["gvAllowEdit"].ToString());
                gc.DisplayFormat.FormatString = dr["gvFormatString"].ToString();
                gc.DisplayFormat.FormatType = (DevExpress.Utils.FormatType)int.Parse(dr["gvFormatType"].ToString());
                gc.SummaryItem.SummaryType = (DevExpress.Data.SummaryItemType)int.Parse(dr["MaskType"].ToString());
                gc.SummaryItem.DisplayFormat = dr["EditMask"].ToString();
                gc.Visible = bool.Parse(dr["IsVisible"].ToString());

                string strType = dr["ControlType"].ToString();
                if (strType == "2")
                {
                    RepositoryItemLookUpEdit dpl = new RepositoryItemLookUpEdit();
                    gridC.RepositoryItems.Add(dpl);
                    gc.ColumnEdit = dpl;

                    dpl.NullText = "";
                    dpl.AutoHeight = false;
                    gc.OptionsColumn.AllowEdit = !blROnly;
                    dpl.Name = "Rep" + strCName;
                    if (strCKey != string.Empty)
                    {
                        StaticFunctions.BindDplComboByTable(dpl, dtConst, "Name", "SetValue",
                           new string[] { "Number", "Name" },
                           new string[] { "编号", "名称" }, true, "SetOrder", string.Format("SetKey='{0}'", strCKey), true);
                    }
                }
                else if (strType == "3")
                {
                    RepositoryItemCheckedComboBoxEdit dpl = new RepositoryItemCheckedComboBoxEdit();
                    gridC.RepositoryItems.Add(dpl);
                    gc.ColumnEdit = dpl;
                    gc.OptionsColumn.AllowEdit = false;

                    dpl.NullText = "";
                    dpl.AutoHeight = false;
                    dpl.DisplayMember = "Name";
                    dpl.Name = "Rep" + strCName;
                    dpl.ValueMember = "SetValue";

                    using (DataTable dtChkList = dtConst.Clone())
                    {
                        DataRow[] drLists = dtConst.Select(string.Format("SetKey='{0}'", strCKey));
                        foreach (DataRow drList in drLists)
                        {
                            dtChkList.ImportRow(drList);
                        }
                        dtChkList.AcceptChanges();
                        dpl.DataSource = dtChkList.DefaultView;
                    }
                }
            }

            StaticFunctions.InitGridViewStyle(gv);
        }
        /// <summary>
        /// 动态生成面板控件
        /// </summary>
        /// <param name="gc">承载控件的面板</param>
        /// <param name="igcW">面板最大的长度，排控件超出改值后自动换行添加</param>
        /// <param name="dtShow">包含控件的DataTable</param>
        /// <param name="dtConst">控件中需要绑定下拉款的数据源DataTable</param>
        /// <param name="blReHeightGc">新增控件完成后，是否自动调整面板高度</param>
        /// <param name="iMinGcHeight">面板的最小高度，如果控件排版完成后，面板高度小于改值，则设置面板的高度为该值</param>
        /// <param name="blEnterEdit">是否面板控件编辑按回车下移，只在编辑面板中传入</param>
        /// <param name="arrContrSeq">控件按回车移动的控件ID顺序列表，如果没有穿null</param>
        /// <param name="blSetDefaultValue">是否设置控件默认值</param>
        /// <param name="igcHeight">返回控件加载后占位置的总高度</param>
        /// <returns></returns>
        public static List<Control> ShowGroupControl(Control gc, int igcW, DataTable dtShow, DataTable dtConst,
            bool blReHeightGc, int iMinGcHeight, bool blEnterEdit, List<string> arrContrSeq, bool blSetDefaultValue, out int igcHeight)
        {
            return StaticFunctions.ShowGroupControl(gc, igcW, dtShow, gc.Name, dtConst, blReHeightGc, iMinGcHeight, blEnterEdit, arrContrSeq, blSetDefaultValue, out igcHeight);
        }
        /// <summary>
        /// 动态生成面板控件
        /// </summary>
        /// <param name="gc">承载控件的面板</param>
        /// <param name="igcW">面板最大的长度，排控件超出改值后自动换行添加</param>
        /// <param name="dtShow">包含控件的DataTable</param>
        /// <param name="strFilter">dtShow的过滤名称，不传即为面板的Name</param>
        /// <param name="dtConst">控件中需要绑定下拉款的数据源DataTable</param>
        /// <param name="blReHeightGc">新增控件完成后，是否自动调整面板高度</param>
        /// <param name="iMinGcHeight">面板的最小高度，如果控件排版完成后，面板高度小于改值，则设置面板的高度为该值</param>
        /// <param name="blEnterEdit">是否面板控件编辑按回车下移，只在编辑面板中传入</param>
        /// <param name="arrContrSeq">控件按回车移动的控件ID顺序列表，如果没有穿null</param>
        /// <param name="blSetDefaultValue">是否生成控件的同时设置默认值，一般只在查询面板中用</param>
        /// <param name="igcHeight">返回控件加载后占位置的总高度</param>
        /// <returns></returns>
        public static List<Control> ShowGroupControl(Control gc, int igcW, DataTable dtShow, string strFilter, DataTable dtConst,
            bool blReHeightGc, int iMinGcHeight, bool blEnterEdit, List<string> arrContrSeq, bool blSetDefaultValue, out int igcHeight)
        {
            List<Control> ListContr = new List<Control>();
            if (blEnterEdit)
                arrContrSeq.Clear();

            int x = 10, y = 10, seed = 10, hseed = 0, xSed = 0, ySed = 0;

            string strGCType = gc.GetType().ToString();
            if (strGCType == "DevExpress.XtraEditors.GroupControl")
            {
                GroupControl gc1 = gc as DevExpress.XtraEditors.GroupControl;
                if (gc1.ShowCaption)
                {
                    if (gc1.CaptionLocation == DevExpress.Utils.Locations.Left)
                    {
                        xSed = 20;
                    }
                    else if (gc1.CaptionLocation == DevExpress.Utils.Locations.Top
                        || gc1.CaptionLocation == DevExpress.Utils.Locations.Default)
                    {
                        ySed = 20;
                    }
                }
            }
            else if (strGCType == "System.Windows.Forms.GroupBox")
            {
                ySed = 10;
            }
            x += xSed;
            y += ySed;

            dtShow.DefaultView.RowFilter = "GroupName='" + strFilter + "'";
            //DataRow[] drs = dtShow.Select("GroupName='" + strFilter + "'");
            foreach (DataRowView dr in dtShow.DefaultView)
            {
                int iwd = int.Parse(dr["ControlWidth"].ToString());
                int iHd = int.Parse(dr["ControlHeight"].ToString());
                string strCName = dr["ControlName"].ToString();
                string strCFiled = dr["ControlFiled"].ToString();
                string strCKey = dr["SetKey"].ToString();
                string strTitle = dr["ShowText"].ToString();
                bool blROly = bool.Parse(dr["IsReadOnly"].ToString());
                string strDefaultVal = dr["DefaultValue"].ToString();
                string strValueType = dr["ValueType"].ToString();
                string strNewLine = dr["IsNewLine"].ToString();
                string strIsDplNull = dr["IsDplNull"].ToString();
                bool blVisible = dr["IsVisible"].ToString() == "True";
                int iLabelWidth = int.Parse(dr["LabelWidth"].ToString());
                int iLeftPexWidth = int.Parse(dr["LeftPexWidth"].ToString());
                string strFilterData = dr["FilterData"].ToString();
                string strFilterAll = strFilterData == string.Empty ? "SetKey='" + strCKey + "'" : "SetKey='" + strCKey + "' AND (" + strFilterData + ")";

                if (dr["IsAddLabel"].ToString() == "True" && blVisible)
                {
                    LabelControl lbl = new LabelControl();
                    gc.Controls.Add(lbl);
                    if (dr["IsMustValue"].ToString() == "True")
                    {
                        lbl.Appearance.ForeColor = System.Drawing.Color.Red;
                        lbl.Appearance.Options.UseForeColor = true;
                    }
                    lbl.Name = "lbl" + strCName;
                    lbl.Text = strTitle;
                    if (iLabelWidth == 0)
                    {
                        lbl.AutoSizeMode = LabelAutoSizeMode.Default;
                        lbl.Size = new System.Drawing.Size(3, 14);
                    }
                    else
                    {
                        lbl.AutoSizeMode = LabelAutoSizeMode.None;
                        lbl.Size = new System.Drawing.Size(iLabelWidth, 14);
                    }
                    if (x + lbl.Size.Width + iwd >= igcW || strNewLine == "True")
                    {
                        x = 10 + xSed;
                        y += seed + hseed;
                    }
                    lbl.Location = new System.Drawing.Point(x, y + 3);
                    x += lbl.Size.Width + iLeftPexWidth;
                }
                string strType = dr["ControlType"].ToString();
                if (strType == "1")
                {
                    TextEdit txt = new TextEdit();
                    ListContr.Add(txt);
                    gc.Controls.Add(txt);
                    txt.Name = strCName;
                    txt.Tag = strCFiled;
                    txt.Properties.ReadOnly = blROly;
                    txt.Properties.Mask.EditMask = dr["EditMask"].ToString();
                    txt.Properties.Mask.MaskType = (DevExpress.XtraEditors.Mask.MaskType)int.Parse(dr["MaskType"].ToString());
                    if (dr["MaskType"].ToString() == "1")
                    {
                        txt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        txt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        txt.Properties.DisplayFormat.FormatString = dr["EditMask"].ToString();
                        txt.Properties.EditFormat.FormatString = dr["EditMask"].ToString();
                    }
                    txt.Size = new System.Drawing.Size(iwd, iHd);
                    txt.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        txt.Location = new System.Drawing.Point(x, y);
                        x += txt.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        txt.Location = new System.Drawing.Point(x, y);
                    }

                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        if (strDefaultVal == "当前时间")
                            txt.EditValue = DateTime.Now.ToString();
                        else
                            txt.EditValue = strDefaultVal;
                    }

                }
                else if (strType == "6")
                {
                    MemoEdit txt = new MemoEdit();
                    ListContr.Add(txt);
                    gc.Controls.Add(txt);
                    txt.Name = strCName;
                    txt.Tag = strCFiled;
                    txt.Properties.ReadOnly = blROly;
                    txt.Size = new System.Drawing.Size(iwd, iHd);
                    txt.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        txt.Location = new System.Drawing.Point(x, y);
                        x += txt.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        txt.Location = new System.Drawing.Point(x, y);
                    }

                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        txt.EditValue = strDefaultVal;
                    }
                }
                else if (strType == "8")
                {
                    ComboBoxEdit txt = new ComboBoxEdit();
                    ListContr.Add(txt);
                    gc.Controls.Add(txt);
                    txt.Name = strCName;
                    txt.Tag = strCFiled;
                    txt.Properties.ReadOnly = blROly;
                    txt.Size = new System.Drawing.Size(iwd, iHd);
                    txt.Properties.DropDownRows = 15;
                    if (strCKey != string.Empty)
                    {
                        StaticFunctions.BindDplComboByTable(txt, dtConst, "Name", "SetOrder", strFilterAll);
                    }
                    txt.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        txt.Location = new System.Drawing.Point(x, y);
                        x += txt.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        txt.Location = new System.Drawing.Point(x, y);
                    }

                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        txt.EditValue = strDefaultVal;
                    }
                }
                else if (strType == "7")
                {
                    SimpleButton txt = new SimpleButton();
                    ListContr.Add(txt);
                    gc.Controls.Add(txt);
                    txt.Name = strCName;
                    txt.Text = strTitle;
                    txt.Tag = strCFiled;
                    txt.Enabled = !blROly;
                    txt.Size = new System.Drawing.Size(iwd, iHd);
                    txt.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        txt.Location = new System.Drawing.Point(x, y);
                        x += txt.Size.Width;
                        x += seed;
                    }
                    else
                    {
                        txt.Location = new System.Drawing.Point(x, y);
                    }
                }
                else if (strType == "2")
                {
                    LookUpEdit dpl = new LookUpEdit();
                    ListContr.Add(dpl);
                    gc.Controls.Add(dpl);
                    dpl.Name = strCName;
                    dpl.Tag = strCFiled;
                    dpl.Properties.ReadOnly = blROly;
                    dpl.Size = new System.Drawing.Size(iwd, iHd);
                    dpl.Properties.DropDownRows = 10;
                    dpl.Properties.ImmediatePopup = true;
                    dpl.Properties.NullText = "";
                    dpl.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
                    dpl.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

                    if (strCKey != string.Empty)
                    {
                        string[] strfieldNames;
                        string[] strHeadTexts;
                        if (dr["DplFieldNames"].ToString().Trim() == string.Empty)
                        {
                            strfieldNames = new string[] { "Number", "Name" };
                        }
                        else
                        {
                            strfieldNames = dr["DplFieldNames"].ToString().Split(",".ToCharArray());
                        }
                        if (dr["DplHeadTexts"].ToString().Trim() == string.Empty)
                        {
                            strHeadTexts = new string[] { "编号", "名称" };
                        }
                        else
                        {
                            strHeadTexts = dr["DplHeadTexts"].ToString().Split(",".ToCharArray());
                        }
                        StaticFunctions.BindDplComboByTable(dpl, dtConst, "Name", "SetValue", strfieldNames, strHeadTexts, true, "SetOrder", strFilterAll, strIsDplNull == "True");

                        DataRow[] drDpls = dtConst.Select(strFilterAll);
                        if (drDpls.Length == 0)
                        {
                            dr["IsReadOnly"] = true;
                            blROly = true;
                            dpl.Properties.ReadOnly = true;
                        }
                        else if (drDpls.Length == 1)
                        {
                            if (strIsDplNull == "False")
                            {
                                dr["DefaultValue"] = drDpls[0]["SetValue"].ToString();
                                strDefaultVal = drDpls[0]["SetValue"].ToString();
                                dr["IsReadOnly"] = true;
                                blROly = true;
                                dpl.Properties.ReadOnly = true;
                            }
                        }
                    }
                    dpl.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        dpl.Location = new System.Drawing.Point(x, y);
                        x += dpl.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        dpl.Location = new System.Drawing.Point(x, y);
                    }

                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        switch (strDefaultVal)
                        {
                            case "当前工厂":
                                if (dtConst.Select("SetKey='" + strCKey + "' AND SetValue=" + CApplication.App.CurrentSession.FyId.ToString()).Length > 0)
                                    dpl.EditValue = CApplication.App.CurrentSession.FyId.ToString();
                                break;
                            case "当前部门":
                                if (dtConst.Select("SetKey='" + strCKey + "' AND SetValue=" + CApplication.App.CurrentSession.DeptId.ToString()).Length > 0)
                                    dpl.EditValue = CApplication.App.CurrentSession.DeptId.ToString();
                                break;
                            case "当前用户":
                                if (dtConst.Select("SetKey='" + strCKey + "' AND SetValue=" + CApplication.App.CurrentSession.UserId.ToString()).Length > 0)
                                    dpl.EditValue = CApplication.App.CurrentSession.UserId.ToString();
                                break;
                            default:
                                dpl.EditValue = strDefaultVal;
                                break;
                        }
                    }
                }
                else if (strType == "3")
                {
                    CheckedComboBoxEdit dpl = new CheckedComboBoxEdit();
                    ListContr.Add(dpl);
                    gc.Controls.Add(dpl);
                    dpl.Name = strCName;
                    dpl.Tag = strCFiled;
                    dpl.Properties.ReadOnly = blROly;
                    dpl.Size = new System.Drawing.Size(iwd, iHd);
                    dpl.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
                    dpl.Properties.DisplayMember = "Name";
                    dpl.Properties.DropDownRows = 10;
                    dpl.Properties.SelectAllItemCaption = "全选";
                    dpl.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    dpl.Properties.ValidateOnEnterKey = true;
                    dpl.Properties.ValueMember = "SetValue";

                    DataTable dtChkList = dtConst.Clone();
                    DataRow[] drLists = dtConst.Select(strFilterAll);
                    foreach (DataRow drList in drLists)
                    {
                        dtChkList.ImportRow(drList);
                    }
                    dtChkList.AcceptChanges();
                    dpl.Properties.DataSource = dtChkList.DefaultView;
                    dpl.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        dpl.Location = new System.Drawing.Point(x, y);
                        x += dpl.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        dpl.Location = new System.Drawing.Point(x, y);
                    }
                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        dpl.EditValue = strDefaultVal;
                        dpl.RefreshEditValue();
                    }
                }
                else if (strType == "4")
                {
                    CheckEdit ckb = new CheckEdit();
                    ListContr.Add(ckb);
                    gc.Controls.Add(ckb);
                    ckb.Name = strCName;
                    ckb.Tag = strCFiled;
                    ckb.Properties.ReadOnly = blROly;
                    ckb.Properties.AutoWidth = true;
                    ckb.Properties.Caption = strTitle;
                    ckb.Size = new System.Drawing.Size(iwd, iHd);
                    ckb.Visible = blVisible;
                    if (dr["IsMustValue"].ToString() == "True")
                    {
                        ckb.ForeColor = System.Drawing.Color.Red;
                    }

                    if (blVisible)
                    {
                        if (x + iwd >= igcW || strNewLine == "True")
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        ckb.Location = new System.Drawing.Point(x, y + 2);
                        x += ckb.Size.Width;
                        x += seed;
                    }
                    else
                    {
                        ckb.Location = new System.Drawing.Point(x, y + 2);
                    }

                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        ckb.EditValue = bool.Parse(strDefaultVal);
                    }

                }
                else if (strType == "5")
                {
                    DateEdit ckb = new DateEdit();
                    ListContr.Add(ckb);
                    gc.Controls.Add(ckb);
                    ckb.Name = strCName;
                    ckb.Tag = strCFiled;
                    ckb.Properties.ReadOnly = blROly;
                    ckb.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                        new DevExpress.XtraEditors.Controls.EditorButton()});
                    ckb.Size = new System.Drawing.Size(iwd, iHd);
                    ckb.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        ckb.Location = new System.Drawing.Point(x, y);
                        x += ckb.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        ckb.Location = new System.Drawing.Point(x, y);
                    }

                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        switch (strDefaultVal)
                        {
                            case "当天":
                                ckb.EditValue = DateTime.Today.ToShortDateString();
                                break;
                            case "昨天":
                                ckb.EditValue = DateTime.Today.AddDays(-1).ToShortDateString();
                                break;
                            case "月初":
                                ckb.EditValue = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-1";
                                break;
                            case "年初":
                                ckb.EditValue = DateTime.Today.Year.ToString() + "-1-1";
                                break;
                            default:
                                DateTime dt;
                                if (DateTime.TryParse(strDefaultVal, out dt))
                                {
                                    ckb.EditValue = strDefaultVal;
                                }
                                else
                                {
                                    ckb.EditValue = null;
                                }
                                break;
                        }
                    }
                    else
                    {
                        ckb.EditValue = null;
                    }
                }
                else if (strType == "11")
                {
                    ExtendControl.ExtPopupTree dpl = new ExtendControl.ExtPopupTree();
                    ListContr.Add(dpl);
                    gc.Controls.Add(dpl);
                    dpl.Name = strCName;
                    dpl.Tag = strCFiled;
                    dpl.Properties.ReadOnly = blROly;
                    dpl.Size = new System.Drawing.Size(iwd, iHd);
                    dpl.Properties.NullText = "";
                    dpl.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    dpl.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        dpl.Location = new System.Drawing.Point(x, y);
                        x += dpl.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        dpl.Location = new System.Drawing.Point(x, y);
                    }
                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        switch (strValueType)
                        {
                            case "1":
                                dpl.EditValue = strDefaultVal;
                                break;
                            case "2":
                                dpl.EditValue = Int32.Parse(strDefaultVal);
                                break;
                            case "3":
                                dpl.EditValue = Int64.Parse(strDefaultVal);
                                break;
                            case "4":
                                dpl.EditValue = Decimal.Parse(strDefaultVal);
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if (strType == "12")
                {
                    ProduceManager.UcTxtPopup dpl = new ProduceManager.UcTxtPopup();
                    ListContr.Add(dpl);
                    gc.Controls.Add(dpl);
                    dpl.Name = strCName;
                    dpl.Tag = strCFiled;
                    dpl.Properties.ReadOnly = blROly;
                    dpl.Size = new System.Drawing.Size(iwd, iHd);
                    dpl.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        dpl.Location = new System.Drawing.Point(x, y);
                        x += dpl.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        dpl.Location = new System.Drawing.Point(x, y);
                    }
                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        dpl.EditValue = strDefaultVal;
                    }
                }
                else if (strType == "13")
                {
                    ProduceManager.UcTreeList dpl = new ProduceManager.UcTreeList();
                    ListContr.Add(dpl);
                    gc.Controls.Add(dpl);
                    dpl.Name = strCName;
                    dpl.Tag = strCFiled;
                    dpl.Properties.ReadOnly = blROly;
                    dpl.Size = new System.Drawing.Size(iwd, iHd);
                    dpl.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        dpl.Location = new System.Drawing.Point(x, y);
                        x += dpl.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        dpl.Location = new System.Drawing.Point(x, y);
                    }
                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        dpl.EditValue = strDefaultVal;
                    }
                }
                if (blVisible)
                    hseed = iHd;
            }

            if (blReHeightGc)
            {
                if (iMinGcHeight < y + seed + hseed)
                    gc.Size = new Size(gc.Size.Width, y + seed + hseed);
                else
                    gc.Size = new Size(gc.Size.Width, iMinGcHeight);
            }
            igcHeight = y + seed + hseed;
            return ListContr;
        }

        public static List<Control> ShowGroupControl(Control gc, int igcW, DataTable dtShow, DataTable dtConst,
            bool blReHeightGc, int iMinGcHeight, bool blEnterEdit, List<string> arrContrSeq, bool blSetDefaultValue)
        {
            return StaticFunctions.ShowGroupControl(gc, igcW, dtShow, gc.Name, dtConst, blReHeightGc, iMinGcHeight, blEnterEdit, arrContrSeq, blSetDefaultValue);
        }
        public static List<Control> ShowGroupControl(Control gc, int igcW, DataTable dtShow, string strFilter, DataTable dtConst,
            bool blReHeightGc, int iMinGcHeight, bool blEnterEdit, List<string> arrContrSeq, bool blSetDefaultValue)
        {
            List<Control> ListContr = new List<Control>();
            if (blEnterEdit)
                arrContrSeq.Clear();

            int x = 10, y = 10, seed = 10, hseed = 0, xSed = 0, ySed = 0;

            string strGCType = gc.GetType().ToString();
            if (strGCType == "DevExpress.XtraEditors.GroupControl")
            {
                GroupControl gc1 = gc as DevExpress.XtraEditors.GroupControl;
                if (gc1.ShowCaption)
                {
                    if (gc1.CaptionLocation == DevExpress.Utils.Locations.Left)
                    {
                        xSed = 20;
                    }
                    else if (gc1.CaptionLocation == DevExpress.Utils.Locations.Top
                        || gc1.CaptionLocation == DevExpress.Utils.Locations.Default)
                    {
                        ySed = 20;
                    }
                }
            }
            else if (strGCType == "System.Windows.Forms.GroupBox")
            {
                ySed = 10;
            }
            x += xSed;
            y += ySed;

            dtShow.DefaultView.RowFilter = "GroupName='" + strFilter + "'";
            //DataRow[] drs = dtShow.Select("GroupName='" + strFilter + "'");
            foreach (DataRowView dr in dtShow.DefaultView)
            {
                int iwd = int.Parse(dr["ControlWidth"].ToString());
                int iHd = int.Parse(dr["ControlHeight"].ToString());
                string strCName = dr["ControlName"].ToString();
                string strCFiled = dr["ControlFiled"].ToString();
                string strCKey = dr["SetKey"].ToString();
                string strTitle = dr["ShowText"].ToString();
                bool blROly = bool.Parse(dr["IsReadOnly"].ToString());
                string strDefaultVal = dr["DefaultValue"].ToString();
                string strValueType = dr["ValueType"].ToString();
                string strNewLine = dr["IsNewLine"].ToString();
                string strIsDplNull = dr["IsDplNull"].ToString();
                bool blVisible = dr["IsVisible"].ToString() == "True";
                int iLabelWidth = int.Parse(dr["LabelWidth"].ToString());
                int iLeftPexWidth = int.Parse(dr["LeftPexWidth"].ToString());
                string strFilterData = dr["FilterData"].ToString();
                string strFilterAll = strFilterData == string.Empty ? "SetKey='" + strCKey + "'" : "SetKey='" + strCKey + "' AND (" + strFilterData + ")";

                if (dr["IsAddLabel"].ToString() == "True" && blVisible)
                {
                    LabelControl lbl = new LabelControl();
                    gc.Controls.Add(lbl);
                    if (dr["IsMustValue"].ToString() == "True")
                    {
                        lbl.Appearance.ForeColor = System.Drawing.Color.Red;
                        lbl.Appearance.Options.UseForeColor = true;
                    }
                    lbl.Name = "lbl" + strCName;
                    lbl.Text = strTitle;
                    if (iLabelWidth == 0)
                    {
                        lbl.AutoSizeMode = LabelAutoSizeMode.Default;
                        lbl.Size = new System.Drawing.Size(3, 14);
                    }
                    else
                    {
                        lbl.AutoSizeMode = LabelAutoSizeMode.None;
                        lbl.Size = new System.Drawing.Size(iLabelWidth, 14);
                    }
                    if (x + lbl.Size.Width + iwd >= igcW || strNewLine == "True")
                    {
                        x = 10 + xSed;
                        y += seed + hseed;
                    }
                    lbl.Location = new System.Drawing.Point(x, y + 3);
                    x += lbl.Size.Width + iLeftPexWidth;
                }
                string strType = dr["ControlType"].ToString();
                if (strType == "1")
                {
                    TextEdit txt = new TextEdit();
                    ListContr.Add(txt);
                    gc.Controls.Add(txt);
                    txt.Name = strCName;
                    txt.Tag = strCFiled;
                    txt.Properties.ReadOnly = blROly;
                    txt.Properties.Mask.EditMask = dr["EditMask"].ToString();
                    txt.Properties.Mask.MaskType = (DevExpress.XtraEditors.Mask.MaskType)int.Parse(dr["MaskType"].ToString());
                    if (dr["MaskType"].ToString() == "1")
                    {
                        txt.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        txt.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        txt.Properties.DisplayFormat.FormatString = dr["EditMask"].ToString();
                        txt.Properties.EditFormat.FormatString = dr["EditMask"].ToString();
                    }
                    txt.Size = new System.Drawing.Size(iwd, iHd);
                    txt.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        txt.Location = new System.Drawing.Point(x, y);
                        x += txt.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        txt.Location = new System.Drawing.Point(x, y);
                    }

                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        if (strDefaultVal == "当前时间")
                            txt.EditValue = DateTime.Now.ToString();
                        else
                            txt.EditValue = strDefaultVal;
                    }

                }
                else if (strType == "6")
                {
                    MemoEdit txt = new MemoEdit();
                    ListContr.Add(txt);
                    gc.Controls.Add(txt);
                    txt.Name = strCName;
                    txt.Tag = strCFiled;
                    txt.Properties.ReadOnly = blROly;
                    txt.Size = new System.Drawing.Size(iwd, iHd);
                    txt.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        txt.Location = new System.Drawing.Point(x, y);
                        x += txt.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        txt.Location = new System.Drawing.Point(x, y);
                    }

                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        txt.EditValue = strDefaultVal;
                    }
                }
                else if (strType == "8")
                {
                    ComboBoxEdit txt = new ComboBoxEdit();
                    ListContr.Add(txt);
                    gc.Controls.Add(txt);
                    txt.Name = strCName;
                    txt.Tag = strCFiled;
                    txt.Properties.ReadOnly = blROly;
                    txt.Size = new System.Drawing.Size(iwd, iHd);
                    txt.Properties.DropDownRows = 15;
                    if (strCKey != string.Empty)
                    {
                        StaticFunctions.BindDplComboByTable(txt, dtConst, "Name", "SetOrder", strFilterAll);
                    }
                    txt.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        txt.Location = new System.Drawing.Point(x, y);
                        x += txt.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        txt.Location = new System.Drawing.Point(x, y);
                    }

                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        txt.EditValue = strDefaultVal;
                    }
                }
                else if (strType == "7")
                {
                    SimpleButton txt = new SimpleButton();
                    ListContr.Add(txt);
                    gc.Controls.Add(txt);
                    txt.Name = strCName;
                    txt.Text = strTitle;
                    txt.Tag = strCFiled;
                    txt.Enabled = !blROly;
                    txt.Size = new System.Drawing.Size(iwd, iHd);
                    txt.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        txt.Location = new System.Drawing.Point(x, y);
                        x += txt.Size.Width;
                        x += seed;
                    }
                    else
                    {
                        txt.Location = new System.Drawing.Point(x, y);
                    }
                }
                else if (strType == "2")
                {
                    LookUpEdit dpl = new LookUpEdit();
                    ListContr.Add(dpl);
                    gc.Controls.Add(dpl);
                    dpl.Name = strCName;
                    dpl.Tag = strCFiled;
                    dpl.Properties.ReadOnly = blROly;
                    dpl.Size = new System.Drawing.Size(iwd, iHd);
                    dpl.Properties.DropDownRows = 10;
                    dpl.Properties.ImmediatePopup = true;
                    dpl.Properties.NullText = "";
                    dpl.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.OnlyInPopup;
                    dpl.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

                    if (strCKey != string.Empty)
                    {
                        string[] strfieldNames;
                        string[] strHeadTexts;
                        if (dr["DplFieldNames"].ToString().Trim() == string.Empty)
                        {
                            strfieldNames = new string[] { "Number", "Name" };
                        }
                        else
                        {
                            strfieldNames = dr["DplFieldNames"].ToString().Split(",".ToCharArray());
                        }
                        if (dr["DplHeadTexts"].ToString().Trim() == string.Empty)
                        {
                            strHeadTexts = new string[] { "编号", "名称" };
                        }
                        else
                        {
                            strHeadTexts = dr["DplHeadTexts"].ToString().Split(",".ToCharArray());
                        }
                        StaticFunctions.BindDplComboByTable(dpl, dtConst, "Name", "SetValue", strfieldNames, strHeadTexts, true, "SetOrder", strFilterAll, strIsDplNull == "True");

                        DataRow[] drDpls = dtConst.Select(strFilterAll);
                        if (drDpls.Length == 0)
                        {
                            dr["IsReadOnly"] = true;
                            blROly = true;
                            dpl.Properties.ReadOnly = true;
                        }
                        else if (drDpls.Length == 1)
                        {
                            if (strIsDplNull == "False")
                            {
                                dr["DefaultValue"] = drDpls[0]["SetValue"].ToString();
                                strDefaultVal = drDpls[0]["SetValue"].ToString();
                                dr["IsReadOnly"] = true;
                                blROly = true;
                                dpl.Properties.ReadOnly = true;
                            }
                        }
                    }
                    dpl.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        dpl.Location = new System.Drawing.Point(x, y);
                        x += dpl.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        dpl.Location = new System.Drawing.Point(x, y);
                    }

                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        switch (strDefaultVal)
                        {
                            case "当前工厂":
                                if (dtConst.Select("SetKey='" + strCKey + "' AND SetValue=" + CApplication.App.CurrentSession.FyId.ToString()).Length > 0)
                                    dpl.EditValue = CApplication.App.CurrentSession.FyId.ToString();
                                break;
                            case "当前部门":
                                if (dtConst.Select("SetKey='" + strCKey + "' AND SetValue=" + CApplication.App.CurrentSession.DeptId.ToString()).Length > 0)
                                    dpl.EditValue = CApplication.App.CurrentSession.DeptId.ToString();
                                break;
                            case "当前用户":
                                if (dtConst.Select("SetKey='" + strCKey + "' AND SetValue=" + CApplication.App.CurrentSession.UserId.ToString()).Length > 0)
                                    dpl.EditValue = CApplication.App.CurrentSession.UserId.ToString();
                                break;
                            default:
                                dpl.EditValue = strDefaultVal;
                                break;
                        }
                    }
                }
                else if (strType == "3")
                {
                    CheckedComboBoxEdit dpl = new CheckedComboBoxEdit();
                    ListContr.Add(dpl);
                    gc.Controls.Add(dpl);
                    dpl.Name = strCName;
                    dpl.Tag = strCFiled;
                    dpl.Properties.ReadOnly = blROly;
                    dpl.Size = new System.Drawing.Size(iwd, iHd);
                    dpl.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
                    dpl.Properties.DisplayMember = "Name";
                    dpl.Properties.DropDownRows = 10;
                    dpl.Properties.SelectAllItemCaption = "全选";
                    dpl.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    dpl.Properties.ValidateOnEnterKey = true;
                    dpl.Properties.ValueMember = "SetValue";

                    DataTable dtChkList = dtConst.Clone();
                    DataRow[] drLists = dtConst.Select(strFilterAll);
                    foreach (DataRow drList in drLists)
                    {
                        dtChkList.ImportRow(drList);
                    }
                    dtChkList.AcceptChanges();
                    dpl.Properties.DataSource = dtChkList.DefaultView;
                    dpl.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        dpl.Location = new System.Drawing.Point(x, y);
                        x += dpl.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        dpl.Location = new System.Drawing.Point(x, y);
                    }
                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        dpl.EditValue = strDefaultVal;
                        dpl.RefreshEditValue();
                    }
                }
                else if (strType == "4")
                {
                    CheckEdit ckb = new CheckEdit();
                    ListContr.Add(ckb);
                    gc.Controls.Add(ckb);
                    ckb.Name = strCName;
                    ckb.Tag = strCFiled;
                    ckb.Properties.ReadOnly = blROly;
                    ckb.Properties.AutoWidth = true;
                    ckb.Properties.Caption = strTitle;
                    ckb.Size = new System.Drawing.Size(iwd, iHd);
                    ckb.Visible = blVisible;
                    if (dr["IsMustValue"].ToString() == "True")
                    {
                        ckb.ForeColor = System.Drawing.Color.Red;
                    }

                    if (blVisible)
                    {
                        if (x + iwd >= igcW || strNewLine == "True")
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        ckb.Location = new System.Drawing.Point(x, y + 2);
                        x += ckb.Size.Width;
                        x += seed;
                    }
                    else
                    {
                        ckb.Location = new System.Drawing.Point(x, y + 2);
                    }

                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        ckb.EditValue = bool.Parse(strDefaultVal);
                    }

                }
                else if (strType == "5")
                {
                    DateEdit ckb = new DateEdit();
                    ListContr.Add(ckb);
                    gc.Controls.Add(ckb);
                    ckb.Name = strCName;
                    ckb.Tag = strCFiled;
                    ckb.Properties.ReadOnly = blROly;
                    ckb.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                        new DevExpress.XtraEditors.Controls.EditorButton()});
                    ckb.Size = new System.Drawing.Size(iwd, iHd);
                    ckb.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        ckb.Location = new System.Drawing.Point(x, y);
                        x += ckb.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        ckb.Location = new System.Drawing.Point(x, y);
                    }

                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        switch (strDefaultVal)
                        {
                            case "当天":
                                ckb.EditValue = DateTime.Today.ToShortDateString();
                                break;
                            case "昨天":
                                ckb.EditValue = DateTime.Today.AddDays(-1).ToShortDateString();
                                break;
                            case "月初":
                                ckb.EditValue = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-1";
                                break;
                            case "年初":
                                ckb.EditValue = DateTime.Today.Year.ToString() + "-1-1";
                                break;
                            default:
                                DateTime dt;
                                if (DateTime.TryParse(strDefaultVal, out dt))
                                {
                                    ckb.EditValue = strDefaultVal;
                                }
                                else
                                {
                                    ckb.EditValue = null;
                                }
                                break;
                        }
                    }
                    else
                    {
                        ckb.EditValue = null;
                    }
                }
                else if (strType == "11")
                {
                    ExtendControl.ExtPopupTree dpl = new ExtendControl.ExtPopupTree();
                    ListContr.Add(dpl);
                    gc.Controls.Add(dpl);
                    dpl.Name = strCName;
                    dpl.Tag = strCFiled;
                    dpl.Properties.ReadOnly = blROly;
                    dpl.Size = new System.Drawing.Size(iwd, iHd);
                    dpl.Properties.NullText = "";
                    dpl.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
                    dpl.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        dpl.Location = new System.Drawing.Point(x, y);
                        x += dpl.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        dpl.Location = new System.Drawing.Point(x, y);
                    }
                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        switch (strValueType)
                        {
                            case "1":
                                dpl.EditValue = strDefaultVal;
                                break;
                            case "2":
                                dpl.EditValue = Int32.Parse(strDefaultVal);
                                break;
                            case "3":
                                dpl.EditValue = Int64.Parse(strDefaultVal);
                                break;
                            case "4":
                                dpl.EditValue = Decimal.Parse(strDefaultVal);
                                break;
                            default:
                                break;
                        }
                    }
                }
                else if (strType == "12")
                {
                    ProduceManager.UcTxtPopup dpl = new ProduceManager.UcTxtPopup();
                    ListContr.Add(dpl);
                    gc.Controls.Add(dpl);
                    dpl.Name = strCName;
                    dpl.Tag = strCFiled;
                    dpl.Properties.ReadOnly = blROly;
                    dpl.Size = new System.Drawing.Size(iwd, iHd);
                    dpl.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        dpl.Location = new System.Drawing.Point(x, y);
                        x += dpl.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        dpl.Location = new System.Drawing.Point(x, y);
                    }
                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        dpl.EditValue = strDefaultVal;
                    }
                }
                else if (strType == "13")
                {
                    ProduceManager.UcTreeList dpl = new ProduceManager.UcTreeList();
                    ListContr.Add(dpl);
                    gc.Controls.Add(dpl);
                    dpl.Name = strCName;
                    dpl.Tag = strCFiled;
                    dpl.Properties.ReadOnly = blROly;
                    dpl.Size = new System.Drawing.Size(iwd, iHd);
                    dpl.Visible = blVisible;

                    if (blVisible)
                    {
                        if (x + iwd >= igcW)
                        {
                            x = 10 + xSed;
                            y += seed + hseed;
                        }
                        dpl.Location = new System.Drawing.Point(x, y);
                        x += dpl.Size.Width;
                        x += seed;

                        if (blEnterEdit && !blROly)
                            arrContrSeq.Add(strCName);
                    }
                    else
                    {
                        dpl.Location = new System.Drawing.Point(x, y);
                    }
                    if (blSetDefaultValue && strDefaultVal != string.Empty)
                    {
                        dpl.EditValue = strDefaultVal;
                    }
                }
                if (blVisible)
                    hseed = iHd;
            }

            if (blReHeightGc)
            {
                if (iMinGcHeight < y + seed + hseed)
                    gc.Size = new Size(gc.Size.Width, y + seed + hseed);
                else
                    gc.Size = new Size(gc.Size.Width, iMinGcHeight);
            }
            return ListContr;
        }
        /// <summary>
        /// 检查是否输入了所有需要输入的值
        /// </summary>
        /// <param name="dr">被检查的DataRow</param>
        /// <param name="gc">面板</param>
        /// <param name="dtShow">面板控件的DataTable，检查哪些值必须</param>
        /// <returns></returns>
        public static bool CheckSave(DataRow dr, Control gc, DataTable dtShow)
        {
            DataRow[] drs = dtShow.Select("IsMustValue=1 AND GroupName='" + gc.Name + "'");
            foreach (DataRow drChk in drs)
            {
                string strField = drChk["ControlFiled"].ToString();
                if (!dr.Table.Columns.Contains(strField))
                    continue;

                if (dr[strField] == DBNull.Value || dr[strField].ToString().Trim() == string.Empty
                    || dr[strField].ToString() == "-9999")
                {
                    MessageBox.Show(drChk["ShowText"].ToString() + " 不能为空.");
                    Control[] ctrs = gc.Controls.Find(drChk["ControlName"].ToString(), true);
                    if (ctrs.Length > 0)
                    {
                        Control ctrl = ctrs[0];
                        ctrl.Select();
                        switch (ctrl.GetType().ToString())
                        {
                            case "DevExpress.XtraEditors.LookUpEdit":
                                LookUpEdit dpl = ctrl as LookUpEdit;
                                dpl.ShowPopup();
                                break;
                            case "ProduceManager.UcTxtPopup":
                                ProduceManager.UcTxtPopup ucp = ctrl as ProduceManager.UcTxtPopup;
                                ucp.Focus();
                                //ucp.ShowPopup();
                                break;
                            case "ProduceManager.UcTreeList":
                                ProduceManager.UcTreeList uct = ctrl as ProduceManager.UcTreeList;
                                uct.Focus();
                                //uct.ShowPopup();
                                break;

                            default:
                                break;
                        }
                    }
                    return false;
                }
            }
            drs = dtShow.Select("(IsEnterQuery=1 OR IsSpaceQuery=1) AND GroupName='" + gc.Name + "'");
            foreach (DataRow drChk in drs)
            {
                string strField = drChk["ControlFiled"].ToString();
                if (!dr.Table.Columns.Contains(strField))
                    continue;

                if (drChk["IsMustValue"].ToString() == "True")
                {
                    if (dr[strField] == DBNull.Value || dr[strField].ToString().Trim() == string.Empty
                        || dr[strField].ToString() == "-9999")
                    {
                        MessageBox.Show("请选择正确的 " + drChk["ShowText"].ToString());
                        return false;
                    }
                }
                else if (dr[strField].ToString().Trim() == string.Empty)
                {
                    string[] strDestFields = drChk["DestUpdatelFiled"].ToString().Split(",".ToCharArray());
                    foreach (string strF in strDestFields)
                    {
                        dr[strF] = DBNull.Value;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 检查是否输入了所有需要输入的值,比如查询页面中查询条件是否输入了必须值
        /// </summary>
        /// <param name="gc">面板</param>
        /// <param name="dtShow">面板控件的DataTable，检查哪些值必须</param>
        /// <returns></returns>
        public static bool CheckSave(Control gc, DataTable dtShow)
        {
            return StaticFunctions.CheckSave(gc, dtShow, gc.Name);
        }
        /// <summary>
        /// 检查是否输入了所有需要输入的值,比如查询页面中查询条件是否输入了必须值
        /// </summary>
        /// <param name="gc">面板</param>
        /// <param name="dtShow">面板控件的DataTable，检查哪些值必须</param>
        /// <param name="strFilter">dtShow的过滤名称，不传即为面板的Name</param>
        /// <returns></returns>
        public static bool CheckSave(Control gc, DataTable dtShow, string strFilter)
        {
            DataRow[] drs = dtShow.Select("IsMustValue=1 AND GroupName='" + strFilter + "'");
            foreach (DataRow drChk in drs)
            {
                Control[] ctrs = gc.Controls.Find(drChk["ControlName"].ToString(), true);
                if (ctrs.Length <= 0)
                    continue;

                Control ctrl = ctrs[0];
                string strValue = string.Empty;
                if (ctrl is BaseEdit)
                {
                    strValue = Convert.ToString((ctrl as BaseEdit).EditValue);
                }
                else if (ctrl is ProduceManager.UcTxtPopup)
                {
                    strValue = Convert.ToString((ctrl as ProduceManager.UcTxtPopup).EditValue);
                }
                else if (ctrl is ProduceManager.UcTreeList)
                {
                    strValue = Convert.ToString((ctrl as ProduceManager.UcTreeList).EditValue);
                }
                if (strValue == string.Empty || strValue == "-9999")
                {
                    MessageBox.Show(drChk["ShowText"].ToString() + " 不能为空.");
                    ctrl.Select();
                    switch (ctrl.GetType().ToString())
                    {
                        case "DevExpress.XtraEditors.LookUpEdit":
                            LookUpEdit dpl = ctrl as LookUpEdit;
                            dpl.ShowPopup();
                            break;
                        case "ProduceManager.UcTxtPopup":
                            ProduceManager.UcTxtPopup ucp = ctrl as ProduceManager.UcTxtPopup;
                            ucp.Focus();
                            //ucp.ShowPopup();
                            break;
                        case "ProduceManager.UcTreeList":
                            ProduceManager.UcTreeList uct = ctrl as ProduceManager.UcTreeList;
                            uct.Focus();
                            //uct.ShowPopup();
                            break;

                        default:
                            break;
                    }
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 获取面板只读控件的Id列表
        /// </summary>
        /// <param name="gc">面板</param>
        /// <param name="dtShow">面板控件的DataTable，检查哪些控件只读</param>
        /// <returns></returns>
        public static string GetReadOnlyIds(Control gc, DataTable dtShow)
        {
            string strId = string.Empty;
            DataRow[] drs = dtShow.Select("IsReadOnly=1 AND GroupName='" + gc.Name + "'");
            foreach (DataRow drChk in drs)
            {
                string strCName = drChk["ControlName"].ToString();
                strId += strId == string.Empty ? strCName : "," + strCName;
            }
            return strId;
        }
        /// <summary>
        /// 获取面板中第一个编辑的控件Id
        /// </summary>
        /// <param name="gc">面板</param>
        /// <param name="dtShow">面板控件的DataTable，检查哪些控件只读</param>
        /// <returns></returns>
        public static string GetFirstEditContrId(Control gc, DataTable dtShow)
        {
            dtShow.DefaultView.RowFilter = "IsVisible=1 AND IsReadOnly=0 AND GroupName='" + gc.Name + "'";
            //DataRow[] drs = dtShow.Select("IsVisible=1 AND IsReadOnly=0 AND GroupName='" + gc.Name + "'");
            if (dtShow.DefaultView.Count > 0)
                return dtShow.DefaultView[0]["ControlName"].ToString();

            return string.Empty;
        }
        /// <summary>
        /// 获取面板中最后一个编辑的控件Id
        /// </summary>
        /// <param name="gc">面板</param>
        /// <param name="dtShow">面板控件的DataTable，检查哪些控件只读</param>
        /// <returns></returns>
        public static string GetLastEditContrId(Control gc, DataTable dtShow)
        {
            dtShow.DefaultView.RowFilter = "IsVisible=1 AND IsReadOnly=0 AND GroupName='" + gc.Name + "'";
            if (dtShow.DefaultView.Count > 0)
                return dtShow.DefaultView[dtShow.DefaultView.Count - 1]["ControlName"].ToString();

            //DataRow[] drs = dtShow.Select("IsVisible=1 AND IsReadOnly=0 AND GroupName='" + gc.Name + "'");
            //if (drs.Length > 0)
            //    return drs[drs.Length - 1]["ControlName"].ToString();

            return string.Empty;
        }
        /// <summary>
        /// 根据索引获取面板中可编辑的控件Id
        /// </summary>
        /// <param name="gc">面板</param>
        /// <param name="dtShow">面板控件的DataTable，检查哪些控件只读</param>
        /// <param name="Index">所有可编辑控件的索引</param>
        /// <returns></returns>
        public static string GetEditContrIdByIndex(Control gc, DataTable dtShow, int Index)
        {
            dtShow.DefaultView.RowFilter = "IsVisible=1 AND IsReadOnly=0 AND GroupName='" + gc.Name + "'";
            if (dtShow.DefaultView.Count > Index)
                return dtShow.DefaultView[Index]["ControlName"].ToString();

            //DataRow[] drs = dtShow.Select("IsVisible=1 AND IsReadOnly=0 AND GroupName='" + gc.Name + "'");
            //if (drs.Length > Index)
            //{
            //    return drs[Index]["ControlName"].ToString();
            //}

            return string.Empty;
        }

        public static Control GetFirstEditContr(Control gc, DataTable dtShow)
        {
            string strFirstEditContrId = StaticFunctions.GetFirstEditContrId(gc, dtShow);
            if (strFirstEditContrId != string.Empty)
            {
                Control[] ctrs = gc.Controls.Find(strFirstEditContrId, true);
                if (ctrs != null && ctrs.Length > 0)
                {
                    return ctrs[0];
                }
            }

            return null;
        }

        public static Control GetLastEditContr(Control gc, DataTable dtShow)
        {
            string strFirstEditContrId = StaticFunctions.GetLastEditContrId(gc, dtShow);
            if (strFirstEditContrId != string.Empty)
            {
                Control[] ctrs = gc.Controls.Find(strFirstEditContrId, true);
                if (ctrs != null && ctrs.Length > 0)
                {
                    return ctrs[0];
                }
            }

            return null;
        }

        public static Control GetEditContrByIndex(Control gc, DataTable dtShow, int Index)
        {
            string strFirstEditContrId = StaticFunctions.GetEditContrIdByIndex(gc, dtShow, Index);
            if (strFirstEditContrId != string.Empty)
            {
                Control[] ctrs = gc.Controls.Find(strFirstEditContrId, true);
                if (ctrs != null && ctrs.Length > 0)
                {
                    return ctrs[0];
                }
            }

            return null;
        }

        /// <summary>
        /// 获取新增修改的要操作的数据库字段数组
        /// </summary>
        /// <param name="gc"></param>
        /// <param name="dtShow"></param>
        /// <returns></returns>
        public static string[] GetUpdateFields(Control gc, DataTable dtShow)
        {
            dtShow.DefaultView.RowFilter = "UpdatelFiled<>'' AND GroupName='" + gc.Name + "'";
            //DataRow[] drs = dtShow.Select("UpdatelFiled<>'' AND GroupName='" + gc.Name + "'");
            List<string> lisField = new List<string>();
            foreach (DataRowView drChk in dtShow.DefaultView)
            {
                lisField.Add(drChk["UpdatelFiled"].ToString());
            }

            return lisField.ToArray();
        }
        public static string[] GetUpdateFields(GridView gv, DataTable dtShow)
        {
            dtShow.DefaultView.RowFilter = "UpdatelFiled<>'' AND GroupName='" + gv.Name + "'";
            //DataRow[] drs = dtShow.Select("UpdatelFiled<>'' AND GroupName='" + gv.Name + "'");
            List<string> lisField = new List<string>();
            foreach (DataRowView drChk in dtShow.DefaultView)
            {
                lisField.Add(drChk["UpdatelFiled"].ToString());
            }

            return lisField.ToArray();
        }
        public static string[] GetUpdateFields(DataTable dtShow, string strFilter)
        {
            dtShow.DefaultView.RowFilter = strFilter + " AND UpdatelFiled<>''";
            List<string> lisField = new List<string>();
            foreach (DataRowView drChk in dtShow.DefaultView)
            {
                lisField.Add(drChk["UpdatelFiled"].ToString());
            }

            return lisField.ToArray();
        }
        /// <summary>
        /// 获取传递到存储过程的参数和参数值
        /// </summary>
        /// <param name="gc">面板</param>
        /// <param name="dtShow">面板控件的DataTable，检查要传递的参数</param>
        /// <param name="strSpParmName">获取的要传递的参数</param>
        /// <returns></returns>
        public static List<string> GetPassSpParmValue(Control gc, DataTable dtShow, out string strSpParmName)
        {
            return GetPassSpParmValue(gc, dtShow, gc.Name, out strSpParmName);
        }
        /// <summary>
        /// 获取传递到存储过程的参数和参数值
        /// </summary>
        /// <param name="gc">面板</param>
        /// <param name="dtShow">面板控件的DataTable，检查要传递的参数</param>
        /// <param name="strFilter">dtShow的过滤名称，不传即为面板的Name</param>
        /// <param name="strSpParmName">获取的要传递的参数</param>
        /// <returns></returns>
        public static List<string> GetPassSpParmValue(Control gc, DataTable dtShow, string strFilter, out string strSpParmName)
        {
            DataRow[] drs = dtShow.Select("PassSpParam<>'' AND GroupName='" + strFilter + "'");
            List<string> lisSpParmValue = new List<string>();
            strSpParmName = string.Empty;
            foreach (DataRow drChk in drs)
            {
                string strCtrName = drChk["ControlName"].ToString();
                Control[] ctrls = gc.Controls.Find(strCtrName, true);
                if (ctrls.Length > 0)
                {
                    Control ctrl = ctrls[0];
                    string strValue = string.Empty;
                    if (ctrl is BaseEdit)
                    {
                        strValue = Convert.ToString((ctrl as BaseEdit).EditValue);
                    }
                    else if (ctrl is ProduceManager.UcTxtPopup)
                    {
                        strValue = Convert.ToString((ctrl as ProduceManager.UcTxtPopup).EditValue);
                    }
                    else if (ctrl is ProduceManager.UcTreeList)
                    {
                        strValue = Convert.ToString((ctrl as ProduceManager.UcTreeList).EditValue);
                    }
                    lisSpParmValue.Add(strValue == "-9999" ? "-1" : strValue);
                }

                strSpParmName += strSpParmName == string.Empty ? drChk["PassSpParam"].ToString() : "," + drChk["PassSpParam"].ToString();
            }
            return lisSpParmValue;
        }

        public static void SetContrDefaultValue(Control gc, DataTable dtShow, DataRow drNew)
        {
            DataRow[] drs = dtShow.Select("DefaultValue<>'' AND GroupName='" + gc.Name + "'");
            foreach (DataRow drChk in drs)
            {
                string strDefaultVal = drChk["DefaultValue"].ToString();
                if (strDefaultVal.Trim() == string.Empty)
                    continue;

                string strFiled = drChk["ControlFiled"].ToString();
                switch (strDefaultVal)
                {
                    case "当前工厂":
                        drNew[strFiled] = CApplication.App.CurrentSession.FyId;
                        break;
                    case "当前部门":
                        drNew[strFiled] = CApplication.App.CurrentSession.DeptId;
                        break;
                    case "当前用户":
                        drNew[strFiled] = CApplication.App.CurrentSession.UserId;
                        break;

                    case "当前时间":
                        drNew[strFiled] = DateTime.Now.ToString();
                        break;
                    case "当天":
                        drNew[strFiled] = DateTime.Today.ToShortDateString();
                        break;
                    case "昨天":
                        drNew[strFiled] = DateTime.Today.AddDays(-1).ToShortDateString();
                        break;
                    case "月初":
                        drNew[strFiled] = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString() + "-1";
                        break;
                    case "年初":
                        drNew[strFiled] = DateTime.Today.Year.ToString() + "-1-1";
                        break;
                    default:
                        drNew[strFiled] = strDefaultVal;
                        break;
                }
            }
        }

        public static void ShowExcelGridControl(GridView gv, DataView dtShow)
        {
            GridControl gridC = gv.GridControl as GridControl;
            foreach (DataRowView drv in dtShow)
            {
                DataRow dr = drv.Row;
                string strCName = dr["Field"].ToString();
                string strCFiled = dr["Field"].ToString();
                string strTitle = dr["ExcelTitle"].ToString();

                if (dr["IsVisible"].ToString() == "False")
                    continue;

                GridColumn gc = new GridColumn();
                gv.Columns.Add(gc);
                gc.Caption = strTitle;
                gc.FieldName = strCFiled;
                gc.Name = strCName;
                //gc.OptionsColumn.AllowMove = false;
                gc.OptionsColumn.ReadOnly = true;
                gc.OptionsColumn.AllowEdit = bool.Parse(dr["IsEdit"].ToString());
                gc.Visible = true;
                gc.DisplayFormat.FormatString = dr["gvFormatString"].ToString();
                gc.DisplayFormat.FormatType = (DevExpress.Utils.FormatType)int.Parse(dr["gvFormatType"].ToString());
                gc.SummaryItem.SummaryType = (DevExpress.Data.SummaryItemType)int.Parse(dr["MaskType"].ToString());
                gc.SummaryItem.DisplayFormat = dr["EditMask"].ToString();
            }

            StaticFunctions.InitGridViewStyle(gv);
        }

        public static void SetBtnEnabled(Component[] ctrs, bool blEnabled)
        {
            foreach (Component ctr in ctrs)
            {
                switch (ctr.GetType().ToString())
                {
                    case "DevExpress.XtraBars.BarButtonItem":
                        (ctr as DevExpress.XtraBars.BarButtonItem).Enabled = blEnabled;
                        break;
                    case "DevExpress.XtraEditors.SimpleButton":
                        (ctr as DevExpress.XtraEditors.SimpleButton).Enabled = blEnabled;
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 生成编辑面板控件的集成方法
        /// </summary>
        /// <param name="gc">承载控件的面板</param>
        /// <param name="igcW">面板最大的长度，排控件超出改值后自动换行添加</param>
        /// <param name="dtShow">包含控件的DataTable</param>
        /// <param name="dtConst">控件中需要绑定下拉款的数据源DataTable</param>
        /// <param name="blReHeightGc">新增控件完成后，是否自动调整面板高度</param>
        /// <param name="iMinGcHeight">面板的最小高度，如果控件排版完成后，面板高度小于改值，则设置面板的高度为该值</param>
        /// <param name="blEnterEdit">是否面板控件编辑按回车下移，只在编辑面板中传入</param>
        /// <param name="arrContrSeq">控件按回车移动的控件ID顺序列表，如果没有穿null</param>
        /// <param name="blSetDefaultValue">是否生成控件的同时设置默认值，一般只在查询面板中用</param>
        /// <param name="blSetDefault">获取面板中是否存在设置控件默认值，如果为true则在新增记录的时候则设置默认值</param>
        /// <param name="strNoEnableCtrIds">获取面板中只读控件的Id，在新增修改的时候不改变该控件的可编辑性</param>
        /// <param name="strFileds">获取要Insert和Update的数据库字段数组，生成新增修改的数据库语句</param>
        /// <param name="CtrFirstEditContr">面板中第一个可编辑的控件对象</param>
        /// <returns></returns>
        public static List<Control> ShowGcContrs(Control gc, int igcW, DataTable dtShow, DataTable dtConst,
            bool blReHeightGc, int iMinGcHeight, bool blEnterEdit, List<string> arrContrSeq, bool blSetDefaultValue,
            out bool blSetDefault, out string strNoEnableCtrIds, out string[] strFileds, out Control CtrFirstEditContr)
        {
            int igcHeight;
            List<Control> lisGcContrsOrd = StaticFunctions.ShowGroupControl(gc, igcW,
                dtShow, dtConst, blReHeightGc, iMinGcHeight, blEnterEdit, arrContrSeq, blSetDefaultValue, out igcHeight);

            blSetDefault = dtShow.Select("DefaultValue<>'' and GroupName='" + gc.Name + "'").Length > 0;
            strNoEnableCtrIds = StaticFunctions.GetReadOnlyIds(gc, dtShow);
            strFileds = StaticFunctions.GetUpdateFields(gc, dtShow);
            CtrFirstEditContr = StaticFunctions.GetFirstEditContr(gc, dtShow);

            return lisGcContrsOrd;
        }
        /// <summary>
        /// 生成编辑面板控件的集成方法
        /// </summary>
        /// <param name="gc">承载控件的面板</param>
        /// <param name="igcW">面板最大的长度，排控件超出改值后自动换行添加</param>
        /// <param name="dtShow">包含控件的DataTable</param>
        /// <param name="dtConst">控件中需要绑定下拉款的数据源DataTable</param>
        /// <param name="blReHeightGc">新增控件完成后，是否自动调整面板高度</param>
        /// <param name="iMinGcHeight">面板的最小高度，如果控件排版完成后，面板高度小于改值，则设置面板的高度为该值</param>
        /// <param name="blEnterEdit">是否面板控件编辑按回车下移，只在编辑面板中传入</param>
        /// <param name="arrContrSeq">控件按回车移动的控件ID顺序列表，如果没有穿null</param>
        /// <param name="blSetDefaultValue">是否生成控件的同时设置默认值，一般只在查询面板中用</param>
        /// <param name="blSetDefault">获取面板中是否存在设置控件默认值，如果为true则在新增记录的时候则设置默认值</param>
        /// <param name="strNoEnableCtrIds">获取面板中只读控件的Id，在新增修改的时候不改变该控件的可编辑性</param>
        /// <param name="strFileds">获取要Insert和Update的数据库字段数组，生成新增修改的数据库语句</param>
        /// <param name="CtrFirstEditContr">面板中第一个可编辑的控件对象</param>
        /// <param name="igcHeight">返回控件加载后占位置的总高度</param>
        /// <returns></returns>
        public static List<Control> ShowGcContrs(Control gc, int igcW, DataTable dtShow, DataTable dtConst,
            bool blReHeightGc, int iMinGcHeight, bool blEnterEdit, List<string> arrContrSeq, bool blSetDefaultValue,
            out bool blSetDefault, out string strNoEnableCtrIds, out string[] strFileds, out Control CtrFirstEditContr, out int igcHeight)
        {
            List<Control> lisGcContrsOrd = StaticFunctions.ShowGroupControl(gc, igcW,
                dtShow, dtConst, blReHeightGc, iMinGcHeight, blEnterEdit, arrContrSeq, blSetDefaultValue, out igcHeight);

            blSetDefault = dtShow.Select("DefaultValue<>'' and GroupName='" + gc.Name + "'").Length > 0;
            strNoEnableCtrIds = StaticFunctions.GetReadOnlyIds(gc, dtShow);
            strFileds = StaticFunctions.GetUpdateFields(gc, dtShow);
            CtrFirstEditContr = StaticFunctions.GetFirstEditContr(gc, dtShow);

            return lisGcContrsOrd;
        }

        public static void ShowGridControl(GridView gv, DataTable dtShow, DataTable dtConst, out string[] strFileds)
        {
            ShowGridControl(gv, dtShow, dtConst);
            strFileds = StaticFunctions.GetUpdateFields(gv, dtShow);
        }
        /// <summary>
        /// 把输入焦点设置到控件
        /// </summary>
        /// <param name="CtrFirstEditContr"></param>
        public static void SetFirstEditContrSelect(Control CtrFirstEditContr)
        {
            if (CtrFirstEditContr == null)
                return;

            CtrFirstEditContr.Select();
            if (CtrFirstEditContr is BaseEdit)
            {
                BaseEdit txt = CtrFirstEditContr as BaseEdit;
                txt.SelectAll();
            }
            switch (CtrFirstEditContr.GetType().ToString())
            {
                case "DevExpress.XtraEditors.LookUpEdit":
                    LookUpEdit dpl = CtrFirstEditContr as LookUpEdit;
                    dpl.ShowPopup();
                    break;
                case "ExtendControl.ExtPopupTree":
                    ExtendControl.ExtPopupTree ext = CtrFirstEditContr as ExtendControl.ExtPopupTree;
                    ext.ShowPopup();
                    break;
                case "ProduceManager.UcTxtPopup":
                    ProduceManager.UcTxtPopup ucp = CtrFirstEditContr as ProduceManager.UcTxtPopup;
                    ucp.Focus();
                    //ucp.ShowPopup();
                    break;
                case "ProduceManager.UcTreeList":
                    ProduceManager.UcTreeList uct = CtrFirstEditContr as ProduceManager.UcTreeList;
                    uct.Focus();
                    //uct.ShowPopup();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 把输入焦点设置到控件
        /// </summary>
        /// <param name="gc">面板</param>
        /// <param name="strContrId">获取焦点的控件Id</param>
        public static void SetContrSelect(Control gc, string strContrId)
        {
            Control[] ctrs = gc.Controls.Find(strContrId, true);
            if (ctrs != null && ctrs.Length > 0)
            {
                Control CtrFirstEditContr = ctrs[0];
                CtrFirstEditContr.Select();
                switch (CtrFirstEditContr.GetType().ToString())
                {
                    case "DevExpress.XtraEditors.LookUpEdit":
                        LookUpEdit dpl = CtrFirstEditContr as LookUpEdit;
                        dpl.ShowPopup();
                        break;
                    case "ExtendControl.ExtPopupTree":
                        ExtendControl.ExtPopupTree ext = CtrFirstEditContr as ExtendControl.ExtPopupTree;
                        ext.ShowPopup();
                        break;
                    case "ProduceManager.UcTxtPopup":
                        ProduceManager.UcTxtPopup ucp = CtrFirstEditContr as ProduceManager.UcTxtPopup;
                        ucp.Focus();
                        //ucp.ShowPopup();
                        break;
                    case "ProduceManager.UcTreeList":
                        ProduceManager.UcTreeList uct = CtrFirstEditContr as ProduceManager.UcTreeList;
                        uct.Focus();
                        //uct.ShowPopup();
                        break;
                    default:
                        break;
                }
            }
        }

        public static void ShowRpt(string strRptSrc, string strRptTitle, Form ParentForm, string[] strParams, string[] strParaVals)
        {
            try
            {
                string strRptUrl = "BatarGold/" + strRptSrc;
                frmReportServicePreview frmExist = StaticFunctions.GetExistedChildReptForm(ParentForm, strRptUrl);
                if (frmExist != null)
                {
                    DataTable dtParas = new DataTable();
                    dtParas.Columns.Add("RP_KEY", Type.GetType("System.String"));
                    dtParas.Columns.Add("RP_VAL", Type.GetType("System.String"));
                    if (strParams != null)
                    {
                        for (int i = 0; i < strParams.Length; i++)
                        {
                            dtParas.Rows.Add(new object[] { strParams[i], strParaVals[i] });
                        }
                    }
                    frmExist.DtRpara = dtParas;
                    frmExist.ShowReports(strRptTitle, strRptUrl, null);
                    frmExist.Activate();
                    return;
                }

                string rptURL = ConfigurationManager.AppSettings["ReportServices"] + strRptUrl + "&rc:ToolBar=true&rc:Parameters=true";
                if (strParams != null)
                {
                    for (int i = 0; i < strParams.Length; i++)
                    {
                        rptURL += "&" + strParams[i] + "=" + strParaVals[i];
                    }
                }
                frmReportServicePreview objFrm = new frmReportServicePreview(strRptTitle, rptURL, "", "");
                objFrm.Rept_Key = strRptUrl;
                objFrm.MdiParent = ParentForm;
                objFrm.Show();
            }
            catch (Exception err)
            {
                MessageBox.Show("出错：" + err.InnerException.Message);
            }
        }
        public static void ShowRptRS(string strReportName, string strRptTitle, Form ParentForm, string[] strParams, string[] strParaVals, DataSet ds)
        {
            try
            {
                frmRS frmExist = StaticFunctions.GetExistedChildRsForm(ParentForm, strReportName);
                if (frmExist != null)
                {
                    frmExist.ShowReports(strRptTitle, strReportName, strParams, strParaVals, ds, false);
                    frmExist.Activate();
                    return;
                }
                frmRS objFrm = new frmRS();
                objFrm.ShowReports(strRptTitle, strReportName, strParams, strParaVals, ds, true);
                objFrm.MdiParent = ParentForm;
                objFrm.Show();
            }
            catch (Exception err)
            {
                MessageBox.Show("出错：" + err.InnerException.Message);
            }
        }

        public static void ShowRptItem(string strRptTitle, string strReportName, Form ParentForm)
        {
            ShowRptItem(strRptTitle, strReportName, ParentForm, string.Empty);
        }
        public static void ShowRptItem(string strRptTitle, string strReportName, Form ParentForm, string strParas)
        {
            try
            {
                frmRpt_SysRptItem frmExist = StaticFunctions.GetExistedChildRptItemForm(ParentForm, strReportName);
                if (frmExist != null)
                {
                    frmExist.Activate();
                    return;
                }
                frmRpt_SysRptItem objFrm = new frmRpt_SysRptItem(strRptTitle, strReportName, strParas);
                objFrm.MdiParent = ParentForm;
                objFrm.Show();
            }
            catch (Exception err)
            {
                MessageBox.Show("出错：" + err.InnerException.Message);
            }
        }

        public static Form GetExistedBsuChildForm(Form ParentForm, string sFormText, string KeyformName)
        {
            Form[] charr = ParentForm.MdiChildren;
            Form FormTem = null;

            foreach (Form chform in charr)
            {
                if (chform.Name.ToUpper() == sFormText.ToUpper())
                {
                    frmEditorBase frm = chform as frmEditorBase;
                    if (frm.KeyFormName.ToUpper() != KeyformName.ToUpper())
                        continue;

                    if (!frm.Visible)
                        frm.Show();
                    frm.Activate();

                    if (frm.frmEditorMode == "VIEW")
                    {
                        return chform;
                    }
                    else
                    {
                        FormTem = chform;
                    }
                }
            }
            return FormTem;
        }
        public static frmEditorBase OpenBsuChildEditorForm(bool blRight, string strPrPart, Form ParentForm, string strCaption,
            string formName, string KeyformName, string strMode, string strFormParam, DataTable dt)
        {
            try
            {
                if (strPrPart == string.Empty)
                    return null;

                string strAllow = string.Empty;
                if (blRight)
                {
                    DataRow[] drs = CApplication.App.DtAllowMenus.Select("Menus_Class = '" + KeyformName + "'");
                    if (drs.Length <= 0)
                    {
                        throw new Exception("你没有访问该页面的权限！");
                    }
                    else
                    {
                        strAllow = drs[0]["Allowed_Operator"].ToString();
                    }
                }

                Form[] charr = ParentForm.MdiChildren;
                Form frmChildForm;

                frmChildForm = GetExistedBsuChildForm(ParentForm, formName, KeyformName);
                if (frmChildForm == null)
                {
                    //如果不存在则用反射创建form窗体实例。
                    Assembly asm = Assembly.Load(strPrPart);//程序集名"MC.Fds"
                    object frmObj = asm.CreateInstance(strPrPart + "." + formName, true);//程序集+form的类名。

                    frmEditorBase frms = (frmEditorBase)frmObj;
                    frms.frmAllowOperatorList = strAllow;
                    frms.KeyFormName = KeyformName;

                    if (blRight)
                    {
                        string[] _allowOpList = strAllow.Split(';');
                        foreach (string _allowOp in _allowOpList)
                        {
                            if (!_allowOp.Trim().Equals(""))
                            {
                                Control[] tp_ctls = frms.Controls.Find(_allowOp.Split('=')[0], true);
                                foreach (Control tp_ctl in tp_ctls)
                                {
                                    tp_ctl.Visible = true;
                                }
                            }
                        }
                    }

                    if (strCaption != string.Empty)
                        frms.Text = strCaption;
                    if (dt != null)
                        OpenEditorForm(frms, strMode, strFormParam, ParentForm, dt);
                    else
                        OpenEditorForm(frms, strMode, strFormParam, ParentForm);

                    return frms;
                }
                else
                {
                    if (dt != null)
                        ((frmEditorBase)frmChildForm).InitialByParam(strMode, strFormParam, dt);
                    else
                        ((frmEditorBase)frmChildForm).InitialByParam(strMode, strFormParam);

                    return (frmChildForm as frmEditorBase);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "错误提示");
                return null;
            }
        }

        public static void SetMainGridView(GridView gvMain, DataRow dr, List<RepositoryItemImageEdit> lisImg)
        {
            gvMain.OptionsView.ShowAutoFilterRow = dr["MainGvShowFilter"].ToString() == "True";
            gvMain.OptionsView.ShowFooter = dr["MainGvShowFoot"].ToString() == "True";
            if (dr["MainGvShowIcon"].ToString() == "True")
            {
                GridColumn gridCol = new GridColumn();
                RepositoryItemImageEdit repImg = new RepositoryItemImageEdit();
                repImg.AutoHeight = false;
                repImg.Buttons.Clear();
                repImg.Name = gvMain.Name + "_repImg";
                repImg.PopupFormMinSize = new System.Drawing.Size(450, 350);
                repImg.ReadOnly = true;
                lisImg.Add(repImg);

                gridCol.Caption = "图片";
                gridCol.ColumnEdit = repImg;
                gridCol.FieldName = "Icon";
                gridCol.Name = gvMain.Name + "_GCol";
                gridCol.OptionsColumn.AllowMove = false;
                gridCol.OptionsColumn.ReadOnly = true;
                gridCol.OptionsColumn.FixedWidth = true;
                gridCol.Visible = true;
                gridCol.VisibleIndex = 0;
                gridCol.Width = 50;

                gvMain.GridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                    repImg});
                gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridCol });
            }
            else if (dr["MainGvShowPic"].ToString() == "True")
            {
                GridColumn gridCol = new GridColumn();
                RepositoryItemPictureEdit repPic = new RepositoryItemPictureEdit();
                repPic.Name = gvMain.Name + "_repPic";
                repPic.ReadOnly = true;
                repPic.ShowMenu = false;
                repPic.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;

                gridCol.Caption = "图片";
                gridCol.ColumnEdit = repPic;
                gridCol.FieldName = "Icon";
                gridCol.Name = gvMain.Name + "_GCol";
                gridCol.OptionsColumn.AllowMove = false;
                gridCol.OptionsColumn.ReadOnly = true;
                gridCol.OptionsColumn.FixedWidth = true;
                gridCol.Visible = true;
                gridCol.VisibleIndex = 0;
                gridCol.Width = int.Parse(dr["ControlFilterLocalPointX"].ToString());

                gvMain.GridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                    repPic});
                gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridCol });
                gvMain.RowHeight = int.Parse(dr["ControlFilterLocalPointY"].ToString());
            }
        }
        public static List<BarButtonItem> ShowBarButtonItem(DataTable dtBtns, Bar bar, string strFilter, string strAllowList, ImageList imageList1)
        {
            List<BarButtonItem> lisContr = new List<BarButtonItem>();
            dtBtns.DefaultView.RowFilter = "IsBarGroup=0 AND BarGroupName='' AND PContrName='" + strFilter + "'";
            foreach (DataRowView dr in dtBtns.DefaultView)
            {
                if (dr["IsChkRight"].ToString() == "True")
                {
                    if (strAllowList.IndexOf(dr["BtnName"].ToString() + "=") == -1)
                    {
                        continue;
                    }
                }

                BarButtonItem item = new BarButtonItem();
                item.Caption = dr["BtnTitle"].ToString();
                item.Id = int.Parse(dr["BsuSetBtn_Id"].ToString());
                item.Name = dr["BtnName"].ToString();
                item.ImageIndex = imageList1.Images.IndexOfKey(dr["ImgName"].ToString());
                if (dr["IsVisible"].ToString() == "True")
                {
                    item.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                item.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
                bar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                    new DevExpress.XtraBars.LinkPersistInfo(item, dr["IsGroup"].ToString() == "True")});
                bar.Manager.Items.AddRange(new DevExpress.XtraBars.BarItem[] { item });

                lisContr.Add(item);
            }
            dtBtns.DefaultView.RowFilter = "IsBarGroup=1 AND PContrName='" + strFilter + "'";
            foreach (DataRowView dr in dtBtns.DefaultView)
            {
                if (dr["IsChkRight"].ToString() == "True")
                {
                    if (strAllowList.IndexOf(dr["BtnName"].ToString() + "=") == -1)
                    {
                        continue;
                    }
                }
                BarSubItem item = new BarSubItem();
                item.Caption = dr["BtnTitle"].ToString();
                item.Id = int.Parse(dr["BsuSetBtn_Id"].ToString());
                item.Name = dr["BtnName"].ToString();
                item.ImageIndex = imageList1.Images.IndexOfKey(dr["ImgName"].ToString());
                if (dr["IsVisible"].ToString() == "True")
                {
                    item.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                item.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
                bar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                    new DevExpress.XtraBars.LinkPersistInfo(item, dr["IsGroup"].ToString() == "True")});
                bar.Manager.Items.AddRange(new DevExpress.XtraBars.BarItem[] { item });

                DataRow[] drItems = dtBtns.Select("IsBarGroup=0 AND PContrName='" + strFilter + "' AND BarGroupName='" + dr["BtnName"].ToString() + "'");
                foreach (DataRow drItem in drItems)
                {
                    if (drItem["IsChkRight"].ToString() == "True")
                    {
                        if (strAllowList.IndexOf(drItem["BtnName"].ToString() + "=") == -1)
                        {
                            continue;
                        }
                    }

                    BarButtonItem Bitem = new BarButtonItem();
                    Bitem.Caption = drItem["BtnTitle"].ToString();
                    Bitem.Id = int.Parse(drItem["BsuSetBtn_Id"].ToString());
                    Bitem.Name = drItem["BtnName"].ToString();
                    Bitem.ImageIndex = imageList1.Images.IndexOfKey(drItem["ImgName"].ToString());
                    if (drItem["IsVisible"].ToString() == "True")
                    {
                        Bitem.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        Bitem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    Bitem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
                    item.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
                    new DevExpress.XtraBars.LinkPersistInfo(Bitem, drItem["IsGroup"].ToString() == "True")});
                    bar.Manager.Items.AddRange(new DevExpress.XtraBars.BarItem[] { Bitem });

                    lisContr.Add(Bitem);
                }
            }
            return lisContr;
        }

        public static Dictionary<string, GridView> ShowTabItem(DataTable dtTabs, XtraTabControl tabContr, string strFilter, string strAllowList
            , List<RepositoryItemImageEdit> lisImg)
        {
            Dictionary<string, GridView> lisContr = new Dictionary<string, GridView>();
            dtTabs.DefaultView.RowFilter = "PContrName='" + strFilter + "'";
            foreach (DataRowView dr in dtTabs.DefaultView)
            {
                if (dr["IsChkRight"].ToString() == "True")
                {
                    if (strAllowList.IndexOf(dr["TabName"].ToString() + "=") == -1)
                    {
                        continue;
                    }
                }
                XtraTabPage tab = new DevExpress.XtraTab.XtraTabPage();
                tab.BorderStyle = System.Windows.Forms.BorderStyle.None;
                tab.Name = dr["TabName"].ToString();
                tab.Text = dr["TabTitle"].ToString();

                GridView GridV = new GridView();
                GridControl GridContr = new GridControl();

                GridV.GridControl = GridContr;
                GridV.Name = dr["GridViewName"].ToString();
                GridV.OptionsSelection.MultiSelect = true;
                GridV.OptionsView.ColumnAutoWidth = false;
                GridV.OptionsView.ShowAutoFilterRow = dr["IsShowFilter"].ToString() == "True";
                GridV.OptionsView.ShowFooter = dr["IsShowFoot"].ToString() == "True";
                GridV.OptionsView.ShowGroupPanel = false;
                GridV.OptionsDetail.ShowDetailTabs = false;

                if (dr["IsGvIcon"].ToString() == "True")
                {
                    GridColumn gridCol = new GridColumn();
                    RepositoryItemImageEdit repImg = new RepositoryItemImageEdit();
                    repImg.AutoHeight = false;
                    repImg.Buttons.Clear();
                    repImg.Name = dr["GridViewName"].ToString() + "_repImg";
                    repImg.PopupFormMinSize = new System.Drawing.Size(450, 350);
                    repImg.ReadOnly = true;
                    lisImg.Add(repImg);

                    gridCol.Caption = "图片";
                    gridCol.ColumnEdit = repImg;
                    gridCol.FieldName = "Icon";
                    gridCol.Name = dr["GridViewName"].ToString() + "_GCol";
                    gridCol.OptionsColumn.AllowMove = false;
                    gridCol.OptionsColumn.ReadOnly = true;
                    gridCol.OptionsColumn.FixedWidth = true;
                    gridCol.Visible = true;
                    gridCol.VisibleIndex = 0;
                    gridCol.Width = 50;

                    GridContr.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                        repImg});
                    GridV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridCol });
                }
                GridContr.Dock = System.Windows.Forms.DockStyle.Fill;
                GridContr.EmbeddedNavigator.Buttons.Append.Visible = false;
                GridContr.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
                GridContr.EmbeddedNavigator.Buttons.Edit.Visible = false;
                GridContr.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
                GridContr.EmbeddedNavigator.Buttons.Remove.Visible = false;
                GridContr.Location = new System.Drawing.Point(0, 0);
                GridContr.MainView = GridV;
                GridContr.Name = dr["GridContrName"].ToString();
                GridContr.UseEmbeddedNavigator = true;
                GridContr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                    GridV});

                tab.Controls.Add(GridContr);
                tabContr.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tab });

                lisContr.Add(dr["GridViewName"].ToString(), GridV);
            }
            return lisContr;
        }
        public static Dictionary<string, GridView> ShowTabItem(DataTable dtTabs, DataTable dtBtns, XtraTabControl tabContr, string strFilter, string strAllowList
            , List<RepositoryItemImageEdit> lisImg, List<GridView> GvNeedGCEdit, List<SimpleButton> lisBtnAlls, Dictionary<string, List<SimpleButton>> lisBtns, ImageList imageList1)
        {
            return ShowTabItem(dtTabs, dtBtns, tabContr, strFilter, strAllowList, lisImg, GvNeedGCEdit, lisBtnAlls, lisBtns, imageList1, false, null);
        }
        public static Dictionary<string, GridView> ShowTabItem(DataTable dtTabs, DataTable dtBtns, XtraTabControl tabContr, string strFilter, string strAllowList
            , List<RepositoryItemImageEdit> lisImg, List<GridView> GvNeedGCEdit, List<SimpleButton> lisBtnAlls, Dictionary<string, List<SimpleButton>> lisBtns, ImageList imageList1
            , bool blCreateQuery, Dictionary<string, Control> lisGcQuerys)
        {
            Dictionary<string, GridView> lisContr = new Dictionary<string, GridView>();
            dtTabs.DefaultView.RowFilter = "PContrName='" + strFilter + "'";
            foreach (DataRowView dr in dtTabs.DefaultView)
            {
                if (dr["IsChkRight"].ToString() == "True")
                {
                    if (strAllowList.IndexOf(dr["TabName"].ToString() + "=") == -1)
                    {
                        continue;
                    }
                }
                XtraTabPage tab = new DevExpress.XtraTab.XtraTabPage();
                tab.BorderStyle = System.Windows.Forms.BorderStyle.None;
                tab.Name = dr["TabName"].ToString();
                tab.Text = dr["TabTitle"].ToString();

                GridView GridV = new GridView();
                GridControl GridContr = new GridControl();

                CreateBtn(dtBtns, tab, strAllowList, imageList1, dr, lisBtnAlls, lisBtns, GridV);

                GridV.GridControl = GridContr;
                GridV.Name = dr["GridViewName"].ToString();
                GridV.OptionsSelection.MultiSelect = true;
                GridV.OptionsView.ColumnAutoWidth = false;
                GridV.OptionsView.ShowAutoFilterRow = dr["IsShowFilter"].ToString() == "True";
                GridV.OptionsView.ShowFooter = dr["IsShowFoot"].ToString() == "True";
                GridV.OptionsView.ShowGroupPanel = false;
                GridV.OptionsDetail.ShowDetailTabs = false;
                if (dr["IsShowViewCaption"].ToString() == "True")
                {
                    GridV.OptionsView.ShowViewCaption = true;
                    GridV.ViewCaption = dr["GridViewCaption"].ToString() + " ";
                    GridV.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Blue;
                    GridV.Appearance.ViewCaption.Options.UseForeColor = true;
                    GridV.Appearance.ViewCaption.Options.UseTextOptions = true;
                    GridV.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                }

                if (dr["IsGvIcon"].ToString() == "True")
                {
                    GridColumn gridCol = new GridColumn();
                    RepositoryItemImageEdit repImg = new RepositoryItemImageEdit();
                    repImg.AutoHeight = false;
                    repImg.Buttons.Clear();
                    repImg.Name = dr["GridViewName"].ToString() + "_repImg";
                    repImg.PopupFormMinSize = new System.Drawing.Size(450, 350);
                    repImg.ReadOnly = true;
                    lisImg.Add(repImg);

                    gridCol.Caption = "图片";
                    gridCol.ColumnEdit = repImg;
                    gridCol.FieldName = "Icon";
                    gridCol.Name = dr["GridViewName"].ToString() + "_GCol";
                    gridCol.OptionsColumn.AllowMove = false;
                    gridCol.OptionsColumn.ReadOnly = true;
                    gridCol.OptionsColumn.FixedWidth = true;
                    gridCol.Visible = true;
                    gridCol.VisibleIndex = 0;
                    gridCol.Width = 50;

                    GridContr.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                        repImg});
                    GridV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { gridCol });
                }
                GridContr.Dock = System.Windows.Forms.DockStyle.Fill;
                GridContr.EmbeddedNavigator.Buttons.Append.Visible = false;
                GridContr.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
                GridContr.EmbeddedNavigator.Buttons.Edit.Visible = false;
                GridContr.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
                GridContr.EmbeddedNavigator.Buttons.Remove.Visible = false;
                GridContr.Location = new System.Drawing.Point(0, 0);
                GridContr.MainView = GridV;
                GridContr.Name = dr["GridContrName"].ToString();
                GridContr.UseEmbeddedNavigator = true;
                GridContr.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
                    GridV});

                if (dr["IsNeedGCEdit"].ToString() == "True")
                {
                    SplitContainerControl splitCInfo = new SplitContainerControl();
                    splitCInfo.Dock = System.Windows.Forms.DockStyle.Fill;
                    splitCInfo.Horizontal = bool.Parse(dr["IsHorizontal"].ToString());
                    splitCInfo.SplitterPosition = int.Parse(dr["PGCContrWidth"].ToString());
                    splitCInfo.Location = new System.Drawing.Point(0, 0);
                    splitCInfo.Name = dr["TabName"].ToString() + "_splitCInfo";
                    splitCInfo.Panel1.Text = "Panel1";
                    splitCInfo.Panel2.Text = "Panel2";
                    splitCInfo.Text = "splitContainerControl1";

                    GroupBox gcInfoAll = new GroupBox();
                    gcInfoAll.Dock = System.Windows.Forms.DockStyle.Fill;
                    gcInfoAll.Location = new System.Drawing.Point(0, 0);
                    gcInfoAll.Name = dr["PGCContrName"].ToString();
                    gcInfoAll.Text = dr["PGCContrTitle"].ToString();

                    CreateBtn(dtBtns, gcInfoAll, strAllowList, imageList1, dr, lisBtnAlls, lisBtns, GridV);

                    if (dr["IsListEdit"].ToString() == "True")
                    {
                        splitCInfo.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
                        splitCInfo.Panel1.Controls.Add(GridContr);
                        splitCInfo.Panel2.Controls.Add(gcInfoAll);
                    }
                    else
                    {
                        splitCInfo.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel1;
                        splitCInfo.Panel1.Controls.Add(gcInfoAll);
                        splitCInfo.Panel2.Controls.Add(GridContr);
                    }
                    tab.Controls.Add(splitCInfo);
                    GvNeedGCEdit.Add(GridV);
                }
                else
                {
                    tab.Controls.Add(GridContr);
                }

                string strQueryName = dr["QueryContrName"].ToString();
                if (strQueryName != string.Empty && blCreateQuery)
                {
                    GroupBox gc = new GroupBox();
                    gc.Dock = System.Windows.Forms.DockStyle.Top;
                    gc.Name = strQueryName;
                    gc.Text = "查询条件";
                    lisGcQuerys.Add(strQueryName, gc);
                    tab.Controls.Add(gc);
                }
                tabContr.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tab });
                lisContr.Add(dr["GridViewName"].ToString(), GridV);
            }
            return lisContr;
        }

        public static void CreateBtn(DataTable dtBtns, Control gcInfoAll, string strAllowList, ImageList imageList1,
            DataRowView drTab, List<SimpleButton> lisBtnAlls, Dictionary<string, List<SimpleButton>> lisBtns, GridView GridV)
        {
            int iPContrBtnX = int.Parse(drTab["PGCContrBtnStPointX"].ToString());
            dtBtns.DefaultView.RowFilter = "PContrName='" + gcInfoAll.Name + "'";
            foreach (DataRowView drBtn in dtBtns.DefaultView)
            {
                if (drBtn["IsChkRight"].ToString() == "True")
                {
                    if (strAllowList.IndexOf(drBtn["BtnName"].ToString() + "=") == -1)
                    {
                        continue;
                    }
                }
                SimpleButton btn = new SimpleButton();
                btn.Name = drBtn["BtnName"].ToString();
                btn.Text = drBtn["BtnTitle"].ToString();
                btn.ImageList = imageList1;
                btn.ImageIndex = imageList1.Images.IndexOfKey(drBtn["ImgName"].ToString());
                btn.Visible = drBtn["IsVisible"].ToString() == "True";
                btn.Size = new System.Drawing.Size(int.Parse(drBtn["ControlWidth"].ToString()), int.Parse(drBtn["ControlHeight"].ToString()));
                iPContrBtnX += int.Parse(drBtn["LeftPix"].ToString());
                btn.Location = new System.Drawing.Point(iPContrBtnX, int.Parse(drTab["PGCContrBtnStPointY"].ToString()));
                iPContrBtnX += int.Parse(drBtn["ControlWidth"].ToString());

                gcInfoAll.Controls.Add(btn);
                lisBtnAlls.Add(btn);
                if (!lisBtns.Keys.Contains(GridV.Name))
                {
                    lisBtns.Add(GridV.Name, new List<SimpleButton>());
                }
                lisBtns[GridV.Name].Add(btn);
            }
        }

        public static DataRow GetContrRowValueById(DataTable dtShow, string strContrId, string strPContrId)
        {
            DataRow[] drs = dtShow.Select("ControlName='" + strContrId + "' AND GroupName='" + strPContrId + "'");
            if (drs.Length > 0)
            {
                return drs[0];
            }
            return null;
        }

        public static void UpdateDataRowSyn(DataRow drDest, DataRow drSrc, string strUpdateFields)
        {
            UpdateDataRowSyn(drDest, drSrc, strUpdateFields, string.Empty);
        }
        public static void UpdateDataRowSyn(DataRow drDest, DataRow drSrc, string strUpdateFields, string strUpdateSrcFields)
        {
            if (drDest == null || drSrc == null || strUpdateFields == string.Empty)
            {
                return;
            }
            string[] strFieldVs = strUpdateFields.Split(",".ToCharArray());
            string[] strFieldSrcVs = strUpdateSrcFields.Split(",".ToCharArray());
            for (int i = 0; i < strFieldVs.Length; i++)
            {
                string strFieldV = strFieldVs[i];
                string strstrFieldSrcV = strUpdateSrcFields == string.Empty ? strFieldV : strFieldSrcVs[i];
                drDest[strFieldV] = drSrc[strstrFieldSrcV];
            }
            drDest.EndEdit();
        }
        public static void UpdateDataRowSynUcTxtPopup(DataRow drDest, DataRow drSrc, string strUpdateFields, string strUpdateSrcFields, ProduceManager.UcTxtPopup ucp)
        {
            if (drDest == null || drSrc == null || strUpdateFields == string.Empty)
            {
                return;
            }
            drDest[ucp.Tag.ToString()] = Convert.ToString(ucp.EditValue) == string.Empty ? DBNull.Value : ucp.EditValue;
            string[] strFieldVs = strUpdateFields.Split(",".ToCharArray());
            string[] strFieldSrcVs = strUpdateSrcFields.Split(",".ToCharArray());
            for (int i = 0; i < strFieldVs.Length; i++)
            {
                string strFieldV = strFieldVs[i];
                string strstrFieldSrcV = strUpdateSrcFields == string.Empty ? strFieldV : strFieldSrcVs[i];
                drDest[strFieldV] = drSrc[strstrFieldSrcV];
            }
            drDest.EndEdit();
        }
        public static void UpdateDataRowSynUcTreeList(DataRow drDest, string strUpdateFields, ProduceManager.UcTreeList uct)
        {
            if (drDest == null || strUpdateFields == string.Empty)
            {
                return;
            }
            drDest[uct.Tag.ToString()] = Convert.ToString(uct.EditValue) == string.Empty ? DBNull.Value : uct.EditValue;
            drDest[strUpdateFields] = uct.TextData;
            drDest.EndEdit();
        }
        public static void UpdateDataRowSynExtPopupTree(DataRow drDest, ExtendControl.ExtPopupTree ept, string strUpdateFields, string strUpdateSrcFields)
        {
            if (drDest == null || ept == null || strUpdateFields == string.Empty)
            {
                return;
            }
            drDest[ept.Tag.ToString()] = Convert.ToString(ept.EditValue) == string.Empty ? DBNull.Value : ept.EditValue;
            string[] strFieldVs = strUpdateFields.Split(",".ToCharArray());
            string[] strFieldSrcVs = strUpdateSrcFields.Split(",".ToCharArray());
            for (int i = 0; i < strFieldVs.Length; i++)
            {
                string strFieldV = strFieldVs[i];
                string strstrFieldSrcV = strUpdateSrcFields == string.Empty ? strFieldV : strFieldSrcVs[i];
                if (Convert.ToString(ept.GetColumnValue(strstrFieldSrcV)) == string.Empty)
                {
                    drDest[strFieldV] = DBNull.Value;
                }
                else
                {
                    drDest[strFieldV] = ept.GetColumnValue(strstrFieldSrcV);
                }
            }
            drDest.EndEdit();
        }
        public static void UpdateDataRowSynLookUpEdit(DataRow drDest, LookUpEdit ept, string strUpdateFields, string strUpdateSrcFields)
        {
            if (drDest == null || ept == null || strUpdateFields == string.Empty)
            {
                return;
            }
            drDest[ept.Tag.ToString()] = Convert.ToString(ept.EditValue) == string.Empty ? DBNull.Value : ept.EditValue;
            string[] strFieldVs = strUpdateFields.Split(",".ToCharArray());
            string[] strFieldSrcVs = strUpdateSrcFields.Split(",".ToCharArray());
            for (int i = 0; i < strFieldVs.Length; i++)
            {
                string strFieldV = strFieldVs[i];
                string strstrFieldSrcV = strUpdateSrcFields == string.Empty ? strFieldV : strFieldSrcVs[i];
                if (Convert.ToString(ept.GetColumnValue(strstrFieldSrcV)) == string.Empty)
                {
                    drDest[strFieldV] = DBNull.Value;
                }
                else
                {
                    drDest[strFieldV] = ept.GetColumnValue(strstrFieldSrcV);
                }
            }
            drDest.EndEdit();
        }

        public static void SetBarItemStatu(Bar bar, string strItemEnableIds)
        {
            foreach (BarItem item in bar.Manager.Items)
            {
                if (item.GetType().ToString() == "DevExpress.XtraBars.BarButtonItem")
                {
                    if (("," + strItemEnableIds + ",").Replace(" ", "").IndexOf("," + item.Name + ",") >= 0)
                    {
                        item.Enabled = true;
                    }
                    else
                    {
                        item.Enabled = false;
                    }
                }
            }
        }
        public static void SetBtnStatu(List<SimpleButton> lisBtns, string strItemEnableIds)
        {
            foreach (SimpleButton btn in lisBtns)
            {
                if (("," + strItemEnableIds + ",").Replace(" ", "").IndexOf("," + btn.Name + ",") >= 0)
                {
                    btn.Enabled = true;
                }
                else
                {
                    btn.Enabled = false;
                }
            }
        }

        public static string GetReadOnlyEditIds(DataTable dtShow, string strFilter)
        {
            string strId = string.Empty;
            DataRow[] drs = dtShow.Select("(IsReadOnly=1 OR IsEdit=0) AND GroupName='" + strFilter + "'");
            foreach (DataRow drChk in drs)
            {
                string strCName = drChk["ControlName"].ToString();
                strId += strId == string.Empty ? strCName : "," + strCName;
            }
            return strId;
        }
        public static Control GetFirstEditFocusContr(Control gc, DataTable dtShow)
        {
            dtShow.DefaultView.RowFilter = "IsVisible=1 AND IsReadOnly=0 AND IsEdit=1 AND GroupName='" + gc.Name + "'";
            if (dtShow.DefaultView.Count <= 0)
                return null;

            string strFirstEditContrId = dtShow.DefaultView[0]["ControlName"].ToString();
            if (strFirstEditContrId != string.Empty)
            {
                Control[] ctrs = gc.Controls.Find(strFirstEditContrId, true);
                if (ctrs != null && ctrs.Length > 0)
                {
                    return ctrs[0];
                }
            }

            return null;
        }
        public static void GetGroupCUkynda(DataTable dtTabs, DataTable dtGroupC, Dictionary<string, UkyndaGcEdit> UkyndaGcItems, Form frm,
            string strAllowList, List<Control> lisGcContrs, DataTable dtShow, DataTable dtConst, Dictionary<string, GridView> gcItems, GridView GvMain, Bar bar, int iMaxWidth)
        {
            GetGroupCUkyndaXtra(dtTabs, dtGroupC, UkyndaGcItems, frm, strAllowList, lisGcContrs, dtShow, dtConst, gcItems, GvMain, bar, null, iMaxWidth);
        }
        public static void ShowGcContrsToUkynda(Control gc, GridView gv, SimpleButton btnSave, Dictionary<string, UkyndaGcEdit> UkyndaGcItems,
            List<Control> lisGcContrs, DataTable dtShow, DataTable dtConst,int iMaxWidth)
        {
            int igcHeight;

            string strNoEnableCtrIdsOrd = string.Empty;
            Control CtrFirstEditContrOrd = null;
            string[] strFiledsOrd = null;
            bool blSetDefaultOrd = false;
            List<string> arrContrSeqOrd = new List<string>();

            List<Control> lisGcContrGcs = StaticFunctions.ShowGcContrs(gc, iMaxWidth, dtShow, dtConst, true, 30, true, arrContrSeqOrd, false
                   , out blSetDefaultOrd, out strNoEnableCtrIdsOrd, out strFiledsOrd, out CtrFirstEditContrOrd, out igcHeight);
            lisGcContrs.AddRange(lisGcContrGcs);

            UkyndaGcEdit GcEdit = new UkyndaGcEdit();
            GcEdit.CtrParentControl = gc;
            GcEdit.GridViewName = gv.Name;
            GcEdit.GridViewEdit = gv;
            GcEdit.BtnEnterSaveBtnId = btnSave.Name;
            GcEdit.SaveMoveToCtrId = StaticFunctions.GetLastEditContrId(gc, dtShow);
            GcEdit.BtnEnterSave = btnSave;
            GcEdit.BlSetDefault = blSetDefaultOrd;
            GcEdit.StrNoEnableCtrIds = strNoEnableCtrIdsOrd;
            GcEdit.StrNoEnableEditCtrIds = GetReadOnlyEditIds(dtShow, gc.Name);
            GcEdit.StrFileds = strFiledsOrd;
            GcEdit.CtrFirstAddFocusContr = CtrFirstEditContrOrd;
            GcEdit.CtrFirstEditFocusContr = GetFirstEditFocusContr(gc, dtShow);
            GcEdit.ArrContrSeq.AddRange(arrContrSeqOrd);
            UkyndaGcItems.Add(gc.Name, GcEdit);
        }

        public static SimpleButton GetBtnById(Control gc, string strBtnId)
        {
            Control[] ctrs = gc.Controls.Find(strBtnId, true);
            if (ctrs != null && ctrs.Length > 0)
            {
                Control CtrFirstEditContr = ctrs[0];
                if (CtrFirstEditContr.GetType().ToString() == "DevExpress.XtraEditors.SimpleButton")
                {
                    return (CtrFirstEditContr as DevExpress.XtraEditors.SimpleButton);
                }
            }

            return null;
        }
        public static GridView ShowGridVChildGv(string strChildGvName, GridControl GridCPrent, DataTable dtShow, DataTable dtConst)
        {
            GridView gvChild = new GridView();
            gvChild.GridControl = GridCPrent;
            gvChild.Name = strChildGvName;
            gvChild.OptionsView.ColumnAutoWidth = false;
            gvChild.OptionsView.ShowAutoFilterRow = true;
            gvChild.OptionsView.ShowGroupPanel = false;
            gvChild.OptionsDetail.ShowDetailTabs = false;

            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            gridLevelNode1.LevelTemplate = gvChild;
            gridLevelNode1.RelationName = GridCPrent.Name + "ChildGrid";
            GridCPrent.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] { gridLevelNode1 });
            GridCPrent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvChild });
            //(GridCPrent.MainView as GridView).ChildGridLevelName = "ChildGrid";
            ShowGridControl(gvChild, dtShow, dtConst);

            return gvChild;
        }
        public static GridView ShowGridVChildGv(string strChildGvName, GridControl GridCPrent, DataTable dtShow, DataTable dtConst,out string[] strFields)
        {
            GridView gvChild = new GridView();
            gvChild.GridControl = GridCPrent;
            gvChild.Name = strChildGvName;
            gvChild.OptionsView.ColumnAutoWidth = false;
            gvChild.OptionsView.ShowAutoFilterRow = true;
            gvChild.OptionsView.ShowGroupPanel = false;
            gvChild.OptionsDetail.ShowDetailTabs = false;

            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            gridLevelNode1.LevelTemplate = gvChild;
            gridLevelNode1.RelationName = GridCPrent.Name + "ChildGrid";
            GridCPrent.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] { gridLevelNode1 });
            GridCPrent.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gvChild });
            //(GridCPrent.MainView as GridView).ChildGridLevelName = "ChildGrid";
            ShowGridControl(gvChild, dtShow, dtConst, out strFields);

            return gvChild;
        }

        public static void SetBtnStyle(BarManager bm, List<SimpleButton> lisBtnAlls, DataRow drMain)
        {
            if (bm != null && drMain["IsBarItemUseFont"].ToString() == "True")
            {
                foreach (BarItem item in bm.Items)
                {
                    item.Appearance.Font = new System.Drawing.Font(drMain["BarItemFontName"].ToString(), float.Parse(drMain["BarItemFontSize"].ToString()));
                    item.Appearance.Options.UseFont = true;
                }
            }
            if (lisBtnAlls != null && drMain["IsBtnItemUseFont"].ToString() == "True")
            {
                foreach (SimpleButton item in lisBtnAlls)
                {
                    item.Appearance.Font = new System.Drawing.Font(drMain["BtnItemFontName"].ToString(), float.Parse(drMain["BtnItemFontSize"].ToString()));
                    item.Appearance.Options.UseFont = true;
                }
            }
        }

        public static void SetDataSetTableName(DataSet ds)
        {
            if (ds.Tables.Count < 2)
                return;
            if (!ds.Tables[ds.Tables.Count - 1].Columns.Contains("DSNME"))
                return;

            string strDsNme = ds.Tables[ds.Tables.Count - 1].Rows[0]["DSNME"].ToString();
            string[] strDsNmes = strDsNme.Split(",".ToCharArray());
            for (int i = 0; i < strDsNmes.Length; i++)
            {
                ds.Tables[i].TableName = strDsNmes[i].Trim();
            }
        }
        public static void UpdateGridControlDataSouce(DataSet ds, string strUpdateFields, Dictionary<string, GridView> gcItems, DataTable dtTabs)
        {
            if (ds == null || strUpdateFields.Trim() == string.Empty)
            {
                return;
            }
            DataTable dt = null;
            DataColumn newColumn = null;
            string[] strFieldVs = strUpdateFields.Split(",".ToCharArray());
            foreach (string strGv in strFieldVs)
            {
                DataRow[] drTabs = dtTabs.Select("IsAddChildGv=1 AND GridViewName='" + strGv + "'");
                if (drTabs.Length == 1)
                {
                    if (!ds.Tables.Contains(strGv + "-Main") || !ds.Tables.Contains(strGv + "-Com"))
                    {
                        MessageBox.Show("存储过程没有返回嵌套表数据源：" + strGv);
                        return;
                    }
                    dt = ds.Tables[strGv + "-Main"];
                    string strRelationsKeyId = drTabs[0]["RelationsKeyId"].ToString();
                    ds.Relations.Add(gcItems[strGv].GridControl.Name + "ChildGrid", dt.Columns[strRelationsKeyId], ds.Tables[strGv + "-Com"].Columns[strRelationsKeyId]);
                }
                else
                {
                    if (!ds.Tables.Contains(strGv))
                    {
                        MessageBox.Show("存储过程没有返回表数据源：" + strGv);
                        return;
                    }
                    dt = ds.Tables[strGv];
                }

                newColumn = dt.Columns.Add("Icon", Type.GetType("System.Byte[]"));
                newColumn.AllowDBNull = true;
                dt.AcceptChanges();
                gcItems[strGv].GridControl.DataSource = dt.DefaultView;
                gcItems[strGv].BestFitColumns();
            }
        }

        public static void SaveOrLoadDelLayout(DevExpress.XtraGrid.Views.Base.BaseView MainView, string strFileName, string strSaveOrLoad)
        {
            string strLayoutPath = Application.StartupPath + @"\系统样式设置\" + CApplication.App.CurrentSession.Number + "\\" + strFileName + "_LayoutXml.xml";
            if (strSaveOrLoad == "SAVE")
            {
                string strLayoutFilePath = Application.StartupPath + @"\系统样式设置\" + CApplication.App.CurrentSession.Number + "\\";
                if (System.IO.Directory.Exists(strLayoutFilePath) == false)//如果要保存的文件不存在，创建文件路径
                {
                    System.IO.Directory.CreateDirectory(strLayoutFilePath);
                }
                MainView.SaveLayoutToXml(strLayoutPath);
            }
            if (strSaveOrLoad == "LOAD")
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(strLayoutPath);
                if (fi.Exists)
                {
                    MainView.RestoreLayoutFromXml(strLayoutPath);
                }
            }
            if (strSaveOrLoad == "DELETE")
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(strLayoutPath);
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }
        }
        public static void InitGridViewStyle(DevExpress.XtraGrid.Views.Grid.GridView gridVw)
        {
            gridVw.BeginInit();
            //gridVw.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(221, 236, 254);
            //gridVw.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(132, 171, 228);
            //gridVw.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(221, 236, 254);
            //gridVw.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            //gridVw.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            //gridVw.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            //gridVw.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            //gridVw.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            //gridVw.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(247, 251, 255);
            //gridVw.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(154, 190, 243);
            //gridVw.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(247, 251, 255);
            //gridVw.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            //gridVw.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            //gridVw.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            //gridVw.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            //gridVw.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            //gridVw.Appearance.Empty.BackColor = System.Drawing.Color.White;
            //gridVw.Appearance.Empty.Options.UseBackColor = true;
            //gridVw.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(225, 225, 245);
            //gridVw.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            //gridVw.Appearance.EvenRow.Options.UseBackColor = true;
            //gridVw.Appearance.EvenRow.Options.UseForeColor = true;
            //gridVw.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(221, 236, 254);
            //gridVw.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(132, 171, 228);
            //gridVw.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(221, 236, 254);
            //gridVw.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            //gridVw.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            //gridVw.Appearance.FilterCloseButton.Options.UseBackColor = true;
            //gridVw.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            //gridVw.Appearance.FilterCloseButton.Options.UseForeColor = true;
            //gridVw.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(62, 109, 185);
            //gridVw.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
            //gridVw.Appearance.FilterPanel.Options.UseBackColor = true;
            //gridVw.Appearance.FilterPanel.Options.UseForeColor = true;
            //gridVw.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(59, 97, 156);
            //gridVw.Appearance.FixedLine.Options.UseBackColor = true;
            gridVw.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            gridVw.Appearance.FocusedCell.Options.UseBackColor = true;
            gridVw.Appearance.FocusedCell.Options.UseForeColor = true;
            gridVw.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(129, 171, 177);//210, 222, 211);//200, 220, 222);//130, 171, 171
            //gridVw.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.FromArgb(193, 216, 247);//222, 223, 200);//130, 171, 171
            gridVw.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            gridVw.Appearance.FocusedRow.Options.UseBackColor = true;
            gridVw.Appearance.FocusedRow.Options.UseForeColor = true;
            gridVw.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gridVw.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(132, 171, 228);
            gridVw.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gridVw.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            gridVw.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Blue;
            gridVw.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            gridVw.Appearance.FooterPanel.Options.UseBackColor = true;
            gridVw.Appearance.FooterPanel.Options.UseBorderColor = true;
            gridVw.Appearance.FooterPanel.Options.UseForeColor = true;
            gridVw.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(193, 216, 247);
            gridVw.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(193, 216, 247);
            gridVw.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            gridVw.Appearance.GroupButton.Options.UseBackColor = true;
            gridVw.Appearance.GroupButton.Options.UseBorderColor = true;
            gridVw.Appearance.GroupButton.Options.UseForeColor = true;
            gridVw.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(193, 216, 247);
            gridVw.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(193, 216, 247);
            gridVw.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            gridVw.Appearance.GroupFooter.Options.UseBackColor = true;
            gridVw.Appearance.GroupFooter.Options.UseBorderColor = true;
            gridVw.Appearance.GroupFooter.Options.UseForeColor = true;
            gridVw.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(62, 109, 185);
            gridVw.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gridVw.Appearance.GroupPanel.Options.UseBackColor = true;
            gridVw.Appearance.GroupPanel.Options.UseForeColor = true;
            gridVw.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(193, 216, 247);
            gridVw.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(193, 216, 247);
            gridVw.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            gridVw.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            gridVw.Appearance.GroupRow.Options.UseBackColor = true;
            gridVw.Appearance.GroupRow.Options.UseBorderColor = true;
            gridVw.Appearance.GroupRow.Options.UseFont = true;
            gridVw.Appearance.GroupRow.Options.UseForeColor = true;
            gridVw.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gridVw.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(132, 171, 228);
            gridVw.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(221, 236, 254);
            gridVw.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            gridVw.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            gridVw.Appearance.HeaderPanel.Options.UseBackColor = true;
            gridVw.Appearance.HeaderPanel.Options.UseBorderColor = true;
            gridVw.Appearance.HeaderPanel.Options.UseForeColor = true;
            //gridVw.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(106, 153, 228);
            gridVw.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(129, 171, 177);//210, 222, 211);//200, 220, 222);//130, 171, 171
            gridVw.Appearance.HideSelectionRow.BackColor2 = System.Drawing.Color.FromArgb(193, 216, 247);//222, 223, 200);//130, 171, 171
            gridVw.Appearance.HideSelectionRow.Options.UseBackColor = true;
            gridVw.Appearance.HideSelectionRow.Options.UseForeColor = true;
            gridVw.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(242, 244, 236);
            gridVw.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(242, 244, 236);
            gridVw.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            gridVw.Appearance.OddRow.Options.UseBackColor = true;
            gridVw.Appearance.OddRow.Options.UseBorderColor = true;
            gridVw.Appearance.OddRow.Options.UseForeColor = true;
            gridVw.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(250, 250, 240);
            gridVw.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(104, 130, 134);
            gridVw.Appearance.Preview.Options.UseBackColor = true;
            gridVw.Appearance.Preview.Options.UseForeColor = true;
            gridVw.Appearance.Row.BackColor = System.Drawing.Color.White;//.FromArgb(242, 244, 236);
            gridVw.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            gridVw.Appearance.Row.Options.UseBackColor = true;
            gridVw.Appearance.Row.Options.UseForeColor = true;
            gridVw.Appearance.RowSeparator.BackColor = System.Drawing.Color.FromArgb(242, 244, 236);
            gridVw.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White;
            gridVw.Appearance.RowSeparator.Options.UseBackColor = true;
            gridVw.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(159, 201, 207);
            gridVw.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            gridVw.Appearance.SelectedRow.Options.UseBackColor = true;
            gridVw.Appearance.SelectedRow.Options.UseForeColor = true;
            gridVw.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(164, 164, 188);//61, 121, 196);
            gridVw.Appearance.HorzLine.Options.UseBackColor = true;
            gridVw.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(164, 164, 179);//0, 78, 196//99, 127, 196
            gridVw.Appearance.VertLine.Options.UseBackColor = true;

            gridVw.OptionsView.EnableAppearanceEvenRow = true;
            gridVw.OptionsView.EnableAppearanceOddRow = true;
            gridVw.EndInit();
        }

        public static void BoundSpicalContr(DataTable dtContr, DataSet dsFormAdt, DataSet dsUkyndaAdt, Control ctrl, DataTable dtShow)
        {
            DataRow[] drShows = dtShow.Select("ControlName='" + ctrl.Name + "' AND GroupName='" + ctrl.Parent.Name + "'");
            if (drShows.Length <= 0)
                return;

            DataRow drShowCtrl = drShows[0];
            bool blAddNull = (drShowCtrl["IsMustValue"].ToString() == "False");

            string strFilter = drShowCtrl["UkdCtrName"].ToString() == string.Empty ? ctrl.Name : drShowCtrl["UkdCtrName"].ToString();
            DataRow[] drs = dtContr.Select("ContrName='" + strFilter + "'");
            if (drs.Length <= 0)
                return;

            DataRow dr = drs[0];
            DataTable dtBound = null;
            if (dsFormAdt != null && dsFormAdt.Tables.Count > 0 && dsFormAdt.Tables.Contains(dr["BoundKey"].ToString()))
            {
                dtBound = dsFormAdt.Tables[dr["BoundKey"].ToString()];
            }
            else if (dsUkyndaAdt != null && dsUkyndaAdt.Tables.Count > 0 && dsUkyndaAdt.Tables.Contains(dr["BoundKey"].ToString()))
            {
                dtBound = dsUkyndaAdt.Tables[dr["BoundKey"].ToString()];
            }
            if (dtBound == null)
                return;

            string strFilterFieldsOrd = drShowCtrl["FilterFieldsOrd"].ToString() != string.Empty ? drShowCtrl["FilterFieldsOrd"].ToString() : dr["FilterFieldsOrd"].ToString();
            string strFilterFieldsInfo = drShowCtrl["FilterFieldsInfo"].ToString() != string.Empty ? drShowCtrl["FilterFieldsInfo"].ToString() : dr["FilterFieldsInfo"].ToString();
            switch (ctrl.GetType().ToString())
            {
                case "ExtendControl.ExtPopupTree":
                    ExtendControl.ExtPopupTree ept = ctrl as ExtendControl.ExtPopupTree;
                    StaticFunctions.BindDplComboByTable(ept, dtBound,
                        dr["TxtField"].ToString(), dr["ValueField"].ToString(), dr["DorpTxtField"].ToString(),
                        dr["FieldNames"].ToString().Split(",".ToCharArray()),
                        dr["HeadTexts"].ToString().Split(",".ToCharArray()),
                        dr["SortField"].ToString(), dr["FilterExt"].ToString(),
                        dr["FilterFieldExt"].ToString(), dr["KeyIdFieldExt"].ToString(),
                        dr["PKeyIdFieldExt"].ToString(), drShowCtrl["FilterData"].ToString(), blAddNull);
                    break;
                case "ProduceManager.UcTxtPopup":
                    ProduceManager.UcTxtPopup ucp = ctrl as ProduceManager.UcTxtPopup;
                    ucp.FilterFieldsOrd = strFilterFieldsOrd;
                    ucp.FilterFieldsInfo = strFilterFieldsInfo;
                    StaticFunctions.BindDplComboByTable(ucp, dtBound,
                        dr["TxtField"].ToString(), dr["ValueField"].ToString(),
                        dr["FieldNames"].ToString().Split(",".ToCharArray()),
                        dr["HeadTexts"].ToString().Split(",".ToCharArray()),
                        dr["FilterFields"].ToString().Split(",".ToCharArray()),
                        dr["SortField"].ToString(), dr["NullValue"].ToString(), drShowCtrl["FilterData"].ToString(),
                        new Point(int.Parse(dr["PSizeX"].ToString()), int.Parse(dr["PSizeY"].ToString())),
                        blAddNull);
                    break;
                case "ProduceManager.UcTreeList":
                    ProduceManager.UcTreeList uct = ctrl as ProduceManager.UcTreeList;
                    uct.FilterFieldsOrd = strFilterFieldsOrd;
                    uct.FilterFieldsInfo = strFilterFieldsInfo;
                    StaticFunctions.BindDplComboByTable(uct, dtBound,
                        dr["TxtField"].ToString(), dr["ValueField"].ToString(),
                        dr["FieldNames"].ToString().Split(",".ToCharArray()),
                        dr["HeadTexts"].ToString().Split(",".ToCharArray()), dr["SortField"].ToString(), drShowCtrl["FilterData"].ToString(),
                        new Point(int.Parse(dr["PSizeX"].ToString()), int.Parse(dr["PSizeY"].ToString()))
                        );
                    break;
                default:
                    break;
            }
        }
        public static void ReBoundSpicalContr(DataTable dtContr, DataSet dsFormAdt, string ctrlIds, DataTable dtShow, Dictionary<string, Control> dicCtrlS)
        {
            if (ctrlIds.Trim() == string.Empty)
                return;
            if (dsFormAdt == null || dsFormAdt.Tables.Count == 0)
                return;
            if (dicCtrlS == null || dicCtrlS.Count == 0)
                return;

            string[] strIds = ctrlIds.Split(",".ToCharArray());
            foreach (string strId in strIds)
            {
                if (!dicCtrlS.ContainsKey(strId))
                    continue;

                Control ctrl = dicCtrlS[strId];
                DataRow[] drShows = dtShow.Select("ControlName='" + strId + "' AND GroupName='" + ctrl.Parent.Name + "'");
                if (drShows.Length <= 0)
                    continue;

                DataRow drShowCtrl = drShows[0];
                bool blAddNull = (drShowCtrl["IsMustValue"].ToString() == "False");

                string strFilter = drShowCtrl["UkdCtrName"].ToString() == string.Empty ? strId : drShowCtrl["UkdCtrName"].ToString();
                DataRow[] drs = dtContr.Select("ContrName='" + strFilter + "'");
                if (drs.Length <= 0)
                    return;

                DataRow dr = drs[0];
                if (!dsFormAdt.Tables.Contains(dr["BoundKey"].ToString()))
                    continue;

                string strFilterFieldsOrd = drShowCtrl["FilterFieldsOrd"].ToString() != string.Empty ? drShowCtrl["FilterFieldsOrd"].ToString() : dr["FilterFieldsOrd"].ToString();
                string strFilterFieldsInfo = drShowCtrl["FilterFieldsInfo"].ToString() != string.Empty ? drShowCtrl["FilterFieldsInfo"].ToString() : dr["FilterFieldsInfo"].ToString();           
                
                switch (ctrl.GetType().ToString())
                {
                    case "ExtendControl.ExtPopupTree":
                        ExtendControl.ExtPopupTree ept = ctrl as ExtendControl.ExtPopupTree;
                        StaticFunctions.BindDplComboByTable(ept, dsFormAdt.Tables[dr["BoundKey"].ToString()],
                            dr["TxtField"].ToString(), dr["ValueField"].ToString(), dr["DorpTxtField"].ToString(),
                            dr["FieldNames"].ToString().Split(",".ToCharArray()),
                            dr["HeadTexts"].ToString().Split(",".ToCharArray()),
                            dr["SortField"].ToString(), dr["FilterExt"].ToString(),
                            dr["FilterFieldExt"].ToString(), dr["KeyIdFieldExt"].ToString(),
                            dr["PKeyIdFieldExt"].ToString(), drShowCtrl["FilterData"].ToString(), blAddNull);
                        break;
                    case "ProduceManager.UcTxtPopup":
                        ProduceManager.UcTxtPopup ucp = ctrl as ProduceManager.UcTxtPopup;
                        ucp.FilterFieldsOrd = strFilterFieldsOrd;
                        ucp.FilterFieldsInfo = strFilterFieldsInfo;
                        StaticFunctions.BindDplComboByTable(ucp, dsFormAdt.Tables[dr["BoundKey"].ToString()],
                            dr["TxtField"].ToString(), dr["ValueField"].ToString(),
                            dr["FieldNames"].ToString().Split(",".ToCharArray()),
                            dr["HeadTexts"].ToString().Split(",".ToCharArray()),
                            dr["FilterFields"].ToString().Split(",".ToCharArray()),
                            dr["SortField"].ToString(), dr["NullValue"].ToString(), drShowCtrl["FilterData"].ToString(),
                            new Point(int.Parse(dr["PSizeX"].ToString()), int.Parse(dr["PSizeY"].ToString())),
                            blAddNull);
                        break;
                    case "ProduceManager.UcTreeList":
                        ProduceManager.UcTreeList uct = ctrl as ProduceManager.UcTreeList;
                        uct.FilterFieldsOrd = strFilterFieldsOrd;
                        uct.FilterFieldsInfo = strFilterFieldsInfo;
                        StaticFunctions.BindDplComboByTable(uct, dsFormAdt.Tables[dr["BoundKey"].ToString()],
                            dr["TxtField"].ToString(), dr["ValueField"].ToString(),
                            dr["FieldNames"].ToString().Split(",".ToCharArray()),
                            dr["HeadTexts"].ToString().Split(",".ToCharArray()), dr["SortField"].ToString(), drShowCtrl["FilterData"].ToString(),
                            new Point(int.Parse(dr["PSizeX"].ToString()), int.Parse(dr["PSizeY"].ToString()))
                            );
                        break;
                    default:
                        break;
                }
            }
        }

        public static DataTable GetBtnMoreInfo(DataTable dtBtnM, DataRow drBtn, DataRow drOrd, string strKeyOrd)
        {
            DataTable lisDrs = dtBtnM.Clone();
            DataRow[] drSets = dtBtnM.Select("IsBtnSet=1 AND BtnName='" + drBtn["BtnName"].ToString() + "'");
            if (drSets.Length <= 0)
            {
                return lisDrs;
            }
            drSets = dtBtnM.Select("IsBtnSet=1 AND BtnName='" + drBtn["BtnName"].ToString() + "' AND OrdKeyFields<>''");
            foreach (DataRow dr in drSets)
            {
                string strFilter = string.Empty;
                if (drOrd != null && dr["OrdKeyFields"].ToString().Trim() != string.Empty)
                {
                    string[] strFields = dr["OrdKeyFields"].ToString().Trim().Split(",".ToCharArray());
                    string[] strFieldVs = dr["OrdKeyValues"].ToString().Trim().Split(",".ToCharArray());
                    for (int i = 0; i < strFields.Length; i++)
                    {
                        strFilter += " AND " + strFields[i] + "='" + strFieldVs[i] + "'";
                    }
                    if (drOrd.Table.Select(strKeyOrd + "='" + drOrd[strKeyOrd].ToString() + "'" + strFilter).Length == 1)
                    {
                        lisDrs.Rows.Add(dr.ItemArray);
                        return lisDrs;
                    }
                }
            }
            drSets = dtBtnM.Select("IsBtnSet=1 AND BtnName='" + drBtn["BtnName"].ToString() + "' AND OrdKeyFields=''");
            foreach (DataRow dr in drSets)
            {
                lisDrs.Rows.Add(dr.ItemArray);
            }
            return lisDrs;
        }
        public static bool GetBtnMRpt(DataTable dtBtnM, DataRow drBtn, DataRow drOrd, string strKeyOrd, Form frmParent
            , string strRptNameBtn, string strSpFlagBtn, string strTitleBtn
            , out string strRptName, out string strSpFlag, out string strTitle)
        {
            DataTable lisDrInfo = StaticFunctions.GetBtnMoreInfo(dtBtnM, drBtn, drOrd, strKeyOrd);
            if (lisDrInfo.Rows.Count == 0)
            {
                strSpFlag = strSpFlagBtn;
                strRptName = strRptNameBtn;
                strTitle = strTitleBtn;
            }
            else if (lisDrInfo.Rows.Count == 1)
            {
                strSpFlag = lisDrInfo.Rows[0]["SpFlag"].ToString();
                strRptName = lisDrInfo.Rows[0]["RptTicketTName"].ToString();
                strTitle = lisDrInfo.Rows[0]["RptTitle"].ToString();
            }
            else
            {
                frmSelectRptTicket frm = new frmSelectRptTicket();
                frm.DtBtnM = lisDrInfo;
                if (frm.ShowDialog(frmParent) != System.Windows.Forms.DialogResult.Yes)
                {
                    strSpFlag = string.Empty;
                    strRptName = string.Empty;
                    strTitle = string.Empty;
                    return false;
                }
                strSpFlag = frm.DrRet["SpFlag"].ToString();
                strRptName = frm.DrRet["RptTicketTName"].ToString();
                strTitle = frm.DrRet["RptTitle"].ToString();
            }
            return true;
        }

        public static string GetStylePicFileAllPath(string strStylePic, string strPic_Version, string frmImageFilePath)
        {
            string AppPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string AllPath = AppPath + frmImageFilePath;
            string PicFileName = string.Format("{0}\\{1}_ver{2}", AllPath, strStylePic, strPic_Version);

            if (!File.Exists(PicFileName))
            {
                ServerRefManager.PicFileRead(strStylePic, strPic_Version);
            }
            return PicFileName;
        }
        public static Dictionary<string, ReportViewer> ShowTabReportViewer(DataTable dtTabs, XtraTabControl tabContr, string strFilter, string strAllowList, string strTitle)
        {
            Dictionary<string, ReportViewer> lisContr = new Dictionary<string, ReportViewer>();
            dtTabs.DefaultView.RowFilter = "PContrName='" + strFilter + "'";
            bool blOneTab = false;
            if (dtTabs.DefaultView.Count == 1)
                blOneTab = true;

            foreach (DataRowView dr in dtTabs.DefaultView)
            {
                if (dr["IsChkRight"].ToString() == "True")
                {
                    if (strAllowList.IndexOf(dr["TabName"].ToString() + "=") == -1)
                    {
                        continue;
                    }
                }
                XtraTabPage tab = new DevExpress.XtraTab.XtraTabPage();
                tab.BorderStyle = System.Windows.Forms.BorderStyle.None;
                tab.Name = dr["TabName"].ToString();
                tab.Text = dr["TabTitle"].ToString();

                ReportViewer reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
                reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
                reportViewer1.Location = new System.Drawing.Point(0, 60);
                reportViewer1.Name = dr["GridContrName"].ToString();
                reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                reportViewer1.LocalReport.EnableExternalImages = true;
                reportViewer1.LocalReport.EnableHyperlinks = true;
                reportViewer1.LocalReport.DisplayName = blOneTab ? strTitle : strTitle + "-" + dr["TabTitle"].ToString();
                string strFilePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\SysRpt\\" + dr["GridViewName"].ToString() + ".rdlc";
                if (!File.Exists(strFilePath))
                {
                    throw new Exception("不存在报表文件：" + dr["GridViewName"].ToString());
                }
                reportViewer1.LocalReport.ReportPath = strFilePath;

                tab.Controls.Add(reportViewer1);
                tabContr.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tab });

                lisContr.Add(dr["GridContrName"].ToString(), reportViewer1);
            }
            return lisContr;
        }

        public static void SetGridViewStyleFormatCondition(GridView gv, DataTable dtBtnM)
        {
            DataRow[] drSets = dtBtnM.Select("IsGvSet=1 AND BtnName='" + gv.Name + "' AND OrdKeyFields<>''");
            foreach (DataRow dr in drSets)
            {
                DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();

                string[] strColor = dr["RptTicketTName"].ToString().Split("|".ToCharArray());
                Color color;
                if (strColor[0].StartsWith("#"))
                {
                    color = System.Drawing.ColorTranslator.FromHtml(strColor[0]);
                }
                else
                {
                    object obj = (new ColorConverter()).ConvertFrom(strColor[0]);
                    color = (Color)obj;
                }
                styleFormatCondition2.Appearance.BackColor = color;
                styleFormatCondition2.Appearance.Options.UseBackColor = true;
                styleFormatCondition2.ApplyToRow = strColor.Length > 1;
                styleFormatCondition2.Column = gv.Columns[dr["OrdKeyFields"].ToString()];
                styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
                styleFormatCondition2.Value1 = dr["OrdKeyValues"].ToString();
                gv.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
                    styleFormatCondition2});
            }
            if (drSets.Length > 0)
            {
                //使用匿名委托注册
                gv.FocusedRowChanged += delegate(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
                {
                    SetGridVFocuRowForeColor(gv, dtBtnM);
                };
            }
        }

        public static void ShowGridControl(TreeList gv, DataTable dtShow, DataTable dtConst)
        {
            //DataRow[] drs = dtShow.Select("GroupName='" + gv.Name + "'");
            dtShow.DefaultView.RowFilter = "GroupName='" + gv.Name + "'";
            foreach (DataRowView dr in dtShow.DefaultView)
            {
                int iwd = int.Parse(dr["ControlWidth"].ToString());
                int iHd = int.Parse(dr["ControlHeight"].ToString());
                string strCName = dr["ControlName"].ToString();
                string strCFiled = dr["ControlFiled"].ToString();
                string strTitle = dr["ShowText"].ToString();

                TreeListColumn treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
                treeListColumn1.Caption = strTitle;
                treeListColumn1.FieldName = strCFiled;
                treeListColumn1.Name = strCName;
                treeListColumn1.OptionsColumn.ReadOnly = true;
                treeListColumn1.Visible = bool.Parse(dr["IsVisible"].ToString());
                treeListColumn1.VisibleIndex = int.Parse(dr["ShowIndex"].ToString());
                treeListColumn1.Width = iwd;

                gv.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
                    treeListColumn1});
            }
        }

        public static int ShowTabItemBusAdd(DataTable dtTabs, DataTable dtGroupC, XtraTabControl tabContr, string strFilter, string strAllowList
            , List<Control> lisGcContrs, DataTable dtShow, DataTable dtConst
            , Dictionary<string, Control> GcOrdControls, Dictionary<string, List<string>> GcOrdarrContrSeq, List<string> lstFiledsOrd)
        {
            Rectangle rect = SystemInformation.VirtualScreen;
            int igcHeight;
            int iMaxHeight = 0;
            string strNoEnableCtrIdsOrd = string.Empty;
            Control CtrFirstEditContrOrd = null;
            string[] strFiledsOrd = null;
            bool blSetDefaultOrd = false;
            List<string> arrContrSeqOrd = new List<string>();

            dtTabs.DefaultView.RowFilter = "PContrName='" + strFilter + "'";
            foreach (DataRowView dr in dtTabs.DefaultView)
            {
                //if (dr["IsChkRight"].ToString() == "True")
                //{
                //    if (strAllowList.IndexOf(dr["TabName"].ToString() + "=") == -1)
                //    {
                //        continue;
                //    }
                //}
                XtraTabPage tab = new DevExpress.XtraTab.XtraTabPage();
                tab.BorderStyle = System.Windows.Forms.BorderStyle.None;
                tab.Name = dr["TabName"].ToString();
                tab.Text = dr["TabTitle"].ToString();

                GroupBox gcInfoAll = new GroupBox();
                gcInfoAll.Dock = System.Windows.Forms.DockStyle.Fill;
                gcInfoAll.Location = new System.Drawing.Point(0, 0);
                gcInfoAll.Name = dr["PGCContrName"].ToString();
                gcInfoAll.Text = dr["PGCContrTitle"].ToString();

                List<Control> lisGcContrGcs = StaticFunctions.ShowGcContrs(gcInfoAll, rect.Width - 50, dtShow, dtConst, true, 30, true, arrContrSeqOrd, false
                    , out blSetDefaultOrd, out strNoEnableCtrIdsOrd, out strFiledsOrd, out CtrFirstEditContrOrd, out igcHeight);
                lisGcContrs.AddRange(lisGcContrGcs);
                GcOrdControls.Add(gcInfoAll.Name, gcInfoAll);
                List<string> lisSql = new List<string>();
                lisSql.AddRange(arrContrSeqOrd);
                GcOrdarrContrSeq.Add(gcInfoAll.Name, lisSql);
                lstFiledsOrd.AddRange(strFiledsOrd);
                if (igcHeight > iMaxHeight)
                    iMaxHeight = igcHeight;

                tab.Controls.Add(gcInfoAll);
                tabContr.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] { tab });
            }

            return iMaxHeight;
        }

        public static void DoOpenLinkForm(DataRow drBtn, Form ParentForm, DataRow drEdit)
        {
            if (drBtn["FormBsuClass"].ToString() == string.Empty)
            {
                if(drBtn["FormMenus_Class"].ToString().ToUpper().StartsWith("HTTP://"))
                {
                    System.Diagnostics.Process.Start("iexplore.exe", drBtn["FormMenus_Class"].ToString());
                    return;
                }
                if (drBtn["IsAddFormLink"].ToString() == "True")
                {
                    StaticFunctions.OpenChildEditorForm(true, "ProduceManager", ParentForm, drBtn["FormCaption"].ToString(),
                        drBtn["FormMenus_Class"].ToString(), "ADD", string.Empty, null);
                }
                else if (drBtn["EditFormLinkKeyField"].ToString() != string.Empty)
                {
                    if (drEdit == null)
                        return;

                    string strKeyId = drEdit[drBtn["EditFormLinkKeyField"].ToString()].ToString();
                    if (strKeyId == string.Empty)
                        return;

                    StaticFunctions.OpenChildEditorForm(true, "ProduceManager", ParentForm, drBtn["FormCaption"].ToString(),
                        drBtn["FormMenus_Class"].ToString(), "VIEW", "KeyId=" + strKeyId, null);
                }
                else
                {
                    StaticFunctions.OpenChildEditorForm(true, "ProduceManager", ParentForm, drBtn["FormCaption"].ToString(),
                        drBtn["FormMenus_Class"].ToString(), "VIEW", string.Empty, null);
                }
            }
            else
            {
                if (drBtn["IsAddFormLink"].ToString() == "True")
                {
                    StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", ParentForm, drBtn["FormCaption"].ToString(),
                        drBtn["FormBsuClass"].ToString(), drBtn["FormMenus_Class"].ToString(), "ADD",
                        "BusClassName=" + drBtn["FormMenus_Class"].ToString(), null);
                }
                else if (drBtn["EditFormLinkKeyField"].ToString() != string.Empty)
                {
                    if (drEdit == null)
                        return;

                    string strKeyId = drEdit[drBtn["EditFormLinkKeyField"].ToString()].ToString();
                    if (strKeyId == string.Empty)
                        return;

                    StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", ParentForm, drBtn["FormCaption"].ToString(),
                        drBtn["FormBsuClass"].ToString(), drBtn["FormMenus_Class"].ToString(), "VIEW",
                        "KeyId=" + strKeyId + "&BusClassName=" + drBtn["FormMenus_Class"].ToString(), null);
                }
                else
                {
                    StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", ParentForm, drBtn["FormCaption"].ToString(),
                        drBtn["FormBsuClass"].ToString(), drBtn["FormMenus_Class"].ToString(), "VIEW",
                        "BusClassName=" + drBtn["FormMenus_Class"].ToString(), null);
                }
            }
        }
        public static void DoShowRpt(DataRow drBtn, Form ParentForm)
        {
            StaticFunctions.OpenBsuChildEditorForm(true, "ProduceManager", ParentForm, drBtn["RptTitle"].ToString(), "frmSysRpts"
                , drBtn["RptName"].ToString(), "VIEW", "BusClassName=" + drBtn["RptName"].ToString(), null);
        }

        public static void PrintItem(BarTender.Application btdb, DataRow dr, string strPrintPath, string strInfoKeyId, string frmImageFilePath)
        {
            btdb.Formats.Open(strPrintPath, true, "");
            bool blImg = false;
            foreach (DataColumn dc in dr.Table.Columns)
            {
                if (dc.ColumnName == strInfoKeyId || dc.ColumnName == "Pic_Version")
                    continue;

                if (dc.ColumnName == "StylePic")
                {
                    blImg = true;
                }
                else
                {
                    btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue(dc.ColumnName, dr[dc.ColumnName].ToString());
                }
            }
            if (blImg)
            {
                string PicFileName = StaticFunctions.GetStylePicFileAllPath(dr["StylePic"].ToString(), dr["Pic_Version"].ToString(), frmImageFilePath);
                btdb.Formats.Open(strPrintPath, true, "").SetNamedSubStringValue("Icon", PicFileName);
            }
            object Zero = 0;
            btdb.Formats.Item(ref Zero).PrintOut(true, false);
        }

        public static bool CheckKeyFields(string strKeyFields, DataRow drOrd, DataRow drInfo)
        {
            StringBuilder sbChkFilt = new StringBuilder();
            string[] strSets = strKeyFields.Split("|".ToCharArray());
            if (strSets.Length != 2)
                return false;

            if (strSets[0] != string.Empty && drOrd == null
                || strSets[1] != string.Empty && drInfo == null)
                return false;

            if (strSets[0] != string.Empty)
            {
                string[] strSetOrds = strSets[0].Split(",".ToCharArray());
                for (int i = 0; i < strSetOrds.Length; i++)
                {
                    if (i > 0)
                        sbChkFilt.Append(",");

                    string[] strSetOrdItems = strSetOrds[i].Split("=".ToCharArray());
                    sbChkFilt.Append(strSetOrdItems[0]);
                    sbChkFilt.Append("=");
                    sbChkFilt.Append(drOrd[strSetOrdItems[0]].ToString());
                }
            }
            sbChkFilt.Append("|");
            if (strSets[1] != string.Empty)
            {
                string[] strSetOrds = strSets[1].Split(",".ToCharArray());
                for (int i = 0; i < strSetOrds.Length; i++)
                {
                    if (i > 0)
                        sbChkFilt.Append(",");

                    string[] strSetOrdItems = strSetOrds[i].Split("=".ToCharArray());
                    sbChkFilt.Append(strSetOrdItems[0]);
                    sbChkFilt.Append("=");
                    sbChkFilt.Append(drInfo[strSetOrdItems[0]].ToString());
                }
            }

            if (sbChkFilt.ToString().ToUpper() == strKeyFields.ToUpper())
                return true;

            return false;
        }
        public static void SetControlEdit(string strIds, bool blEdit, Control ctrParent)
        {
            if (strIds == string.Empty)
                return;

            string[] strSets = strIds.Split(",".ToCharArray());
            foreach (string strId in strSets)
            {
                Control[] ctrs = ctrParent.Controls.Find(strId, true);
                if (ctrs == null || ctrs.Length <= 0)
                    continue;

                Control contr = ctrs[0];
                if (contr is BaseEdit)
                {
                    BaseEdit bse = contr as BaseEdit;
                    bse.Properties.ReadOnly = !blEdit;
                }
                else if (contr is ProduceManager.UcTxtPopup)
                {
                    ProduceManager.UcTxtPopup bc = contr as ProduceManager.UcTxtPopup;
                    bc.ReadOnly = !blEdit;
                }
                else if (contr is ProduceManager.UcTreeList)
                {
                    ProduceManager.UcTreeList bc = contr as ProduceManager.UcTreeList;
                    bc.ReadOnly = !blEdit;
                }
            }
        }

        public static void SetGridVFocuRowForeColor(GridView gv, DataTable dtBtnM)
        {
            if (dtBtnM == null)
                return;

            DataRow drFocu = gv.GetFocusedDataRow();
            if (drFocu == null)
                return;

            DataRow[] drSets = dtBtnM.Select("IsGvSet=1 AND BtnName='" + gv.Name + "' AND OrdKeyFields<>''");
            foreach (DataRow dr in drSets)
            {
                if (dr["OrdKeyValues"].ToString() == drFocu[dr["OrdKeyFields"].ToString()].ToString())
                {
                    string[] strColor = dr["RptTicketTName"].ToString().Split("|".ToCharArray());
                    Color color;
                    string strColorSet = strColor.Length == 1 ? strColor[0] : strColor[1];
                    if (strColorSet.StartsWith("#"))
                    {
                        color = System.Drawing.ColorTranslator.FromHtml(strColorSet);
                    }
                    else
                    {
                        object obj = (new ColorConverter()).ConvertFrom(strColorSet);
                        color = (Color)obj;
                    }
                    gv.Appearance.FocusedRow.BackColor = color;
                    return;
                }
            }
            gv.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(129, 171, 177);//InitGridViewStyle
            gv.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;//InitGridViewStyle
        }

        public static Control SetFirstEditContrSelect(Control gcParent, DataTable dtShow)
        {
            Control CtrFirstEditContr = GetFirstEditContr(gcParent, dtShow);
            if (CtrFirstEditContr == null)
                return null;

            CtrFirstEditContr.Select();
            if (CtrFirstEditContr is BaseEdit)
            {
                BaseEdit txt = CtrFirstEditContr as BaseEdit;
                txt.SelectAll();
            }
            switch (CtrFirstEditContr.GetType().ToString())
            {
                case "DevExpress.XtraEditors.LookUpEdit":
                    LookUpEdit dpl = CtrFirstEditContr as LookUpEdit;
                    dpl.ShowPopup();
                    break;
                case "ExtendControl.ExtPopupTree":
                    ExtendControl.ExtPopupTree ext = CtrFirstEditContr as ExtendControl.ExtPopupTree;
                    ext.ShowPopup();
                    break;
                case "ProduceManager.UcTxtPopup":
                    ProduceManager.UcTxtPopup ucp = CtrFirstEditContr as ProduceManager.UcTxtPopup;
                    ucp.Focus();
                    //ucp.ShowPopup();
                    break;
                case "ProduceManager.UcTreeList":
                    ProduceManager.UcTreeList uct = CtrFirstEditContr as ProduceManager.UcTreeList;
                    uct.Focus();
                    //uct.ShowPopup();
                    break;
                default:
                    break;
            }

            return CtrFirstEditContr;
        }

        public static void GetGroupCUkyndaXtra(DataTable dtTabs, DataTable dtGroupC, Dictionary<string, UkyndaGcEdit> UkyndaGcItems, Form frm,
            string strAllowList, List<Control> lisGcContrs, DataTable dtShow, DataTable dtConst, Dictionary<string, GridView> gcItems,
            GridView GvMain, Bar bar, XtraTabControl xtabOrdParent, int iMaxWidthF)
        {
            int igcHeight;
            int iMaxWidth = 0;

            string strNoEnableCtrIdsOrd = string.Empty;
            Control CtrFirstEditContrOrd = null;
            string[] strFiledsOrd = null;
            bool blSetDefaultOrd = false;
            List<string> arrContrSeqOrd = new List<string>();
            int iMaxHeight = 0;
            foreach (DataRowView dr in dtGroupC.DefaultView)
            {
                if (dr["IsChkRight"].ToString() == "True")
                {
                    if (strAllowList.IndexOf(dr["GroupCName"].ToString() + "=") == -1)
                    {
                        continue;
                    }
                }
                string strGvName = dr["GridViewName"].ToString();
                if (strGvName == "gridVMain")
                {
                    iMaxWidth = iMaxWidthF;
                }
                else
                {
                    DataRow[] drGvs = dtTabs.Select("GridViewName='" + strGvName + "'");
                    if (drGvs.Length != 1)
                        return;

                    if (drGvs[0]["IsListEdit"].ToString() == "True" && drGvs[0]["IsHorizontal"].ToString() == "True")
                    {
                        iMaxWidth = int.Parse(drGvs[0]["PGCContrWidth"].ToString());
                    }
                    else
                    {
                        iMaxWidth = iMaxWidthF;
                    }
                }
                GroupBox gc = new GroupBox();
                gc.Dock = System.Windows.Forms.DockStyle.Top;
                gc.Name = dr["GroupCName"].ToString();
                string strPContrName = dr["PContrName"].ToString();
                List<Control> lisGcContrGcs = StaticFunctions.ShowGcContrs(gc, iMaxWidth-30, dtShow, dtConst, true, 30, true, arrContrSeqOrd, false
                    , out blSetDefaultOrd, out strNoEnableCtrIdsOrd, out strFiledsOrd, out CtrFirstEditContrOrd, out igcHeight);
                lisGcContrs.AddRange(lisGcContrGcs);

                UkyndaGcEdit GcEdit = new UkyndaGcEdit();
                GcEdit.CtrParentControl = gc;
                GcEdit.GridViewName = strGvName;
                GcEdit.BtnEnterSaveBtnId = dr["BtnEnterSaveBtnId"].ToString();
                GcEdit.SaveMoveToCtrId = StaticFunctions.GetLastEditContrId(gc, dtShow);
                GcEdit.BlSetDefault = blSetDefaultOrd;
                GcEdit.StrNoEnableCtrIds = strNoEnableCtrIdsOrd;
                GcEdit.StrNoEnableEditCtrIds = GetReadOnlyEditIds(dtShow, gc.Name);
                GcEdit.StrFileds = strFiledsOrd;
                GcEdit.CtrFirstAddFocusContr = CtrFirstEditContrOrd;
                GcEdit.CtrFirstEditFocusContr = GetFirstEditFocusContr(gc, dtShow);
                GcEdit.ArrContrSeq.AddRange(arrContrSeqOrd);
                if (dr["BtnEnterSaveBtnId"].ToString() != string.Empty)
                {
                    if (strGvName == "gridVMain" && bar != null)
                    {
                        GcEdit.BtnEnterSave = bar.Manager.Items[dr["BtnEnterSaveBtnId"].ToString()];
                    }
                    else
                    {
                        GcEdit.BtnEnterSave = GetBtnById(frm, dr["BtnEnterSaveBtnId"].ToString());
                    }
                }
                if (strGvName == "gridVMain")
                {
                    GcEdit.GridViewEdit = GvMain;
                    if (igcHeight > iMaxHeight)
                        iMaxHeight = igcHeight;
                }
                else
                {
                    GcEdit.GridViewEdit = gcItems[dr["GridViewName"].ToString()];
                }
                UkyndaGcItems.Add(gc.Name, GcEdit);

                if (strPContrName == frm.Name)
                {
                    gc.Text = dr["GroupCTitle"].ToString();
                    frm.Controls.Add(gc);
                }
                else if (xtabOrdParent != null && strPContrName == xtabOrdParent.Name)
                {
                    XtraTabPage tab = new DevExpress.XtraTab.XtraTabPage();
                    tab.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    tab.Name = "tab_" + dr["GroupCName"].ToString();
                    tab.Text = dr["GroupCTitle"].ToString();

                    gc.Text = string.Empty;
                    tab.Controls.Add(gc);
                    xtabOrdParent.TabPages.Insert(0, tab);
                }
                else
                {
                    gc.Text = dr["GroupCTitle"].ToString();
                    Control[] gcPs = frm.Controls.Find(strPContrName, true);
                    if (gcPs != null && gcPs.Length > 0)
                    {
                        Control gcP = gcPs[0];
                        gcP.Controls.Add(gc);
                    }
                }
            }
            if (xtabOrdParent != null)
                xtabOrdParent.Size = new Size(xtabOrdParent.Size.Width, iMaxHeight + 40);
        }

        public static void CreateBarcodeImage(DataRow dr, string strFileNameField, string strBarcodeField)
        {
            CodeLib.Barcode b = new CodeLib.Barcode();
            int W = 250;
            int H = 60;
            CodeLib.AlignmentPositions Align = CodeLib.AlignmentPositions.CENTER;
            CodeLib.TYPE type = CodeLib.TYPE.CODE128;
            string strFileName = dr[strFileNameField].ToString();
            string strText = dr[strBarcodeField].ToString();

            b.IncludeLabel = true;
            b.Alignment = Align;
            b.LabelPosition = CodeLib.LabelPositions.BOTTOMCENTER;
            b.Encode(type, strText, Color.Black, Color.White, W, H);

            string AllPath = Application.StartupPath + "\\系统条码";
            if (!Directory.Exists(AllPath))
            {
                Directory.CreateDirectory(AllPath);
            }
            CodeLib.SaveTypes savetype = CodeLib.SaveTypes.JPG;
            b.SaveImage(AllPath + "\\" + strFileName, savetype);

            //保存图片
            ServerRefManager.FileSave(strFileName, AllPath + "\\" + strFileName, false);
        }

        public static void SetDataSetNullRowToMinRowAmount(DataSet ds)
        {
            foreach (DataTable dt in ds.Tables)
            {
                StaticFunctions.SetDataSetNullRowToMinRowAmount(dt);
            }
        }
        public static void SetDataSetNullRowToMinRowAmount(DataTable dt)
        {
            int iCur = dt.Rows.Count;
            if (dt == null || iCur <= 0)
                return;

            if (!dt.Columns.Contains("MinRowAmount"))
                return;

            int iMin = int.Parse(dt.Rows[0]["MinRowAmount"].ToString());
            if (iMin <= iCur)
                return;

            for (int i = 0; i < iMin - iCur; i++)
            {
                dt.Rows.Add();
            }
        }

        /// <summary>
        /// 根据条件动态生成GridBand列
        /// </summary>
        /// <param name="gv">被操作的GridView</param>
        /// <param name="dtShow">包含GridView列的DataTable</param>
        /// <param name="dtConst">GridView列中需要绑定下拉款的数据源DataTable</param>
        public static void ShowGridControlBand(BandedGridView gv, DataTable dtShow, DataTable dtConst)
        {
            GridControl gridC = gv.GridControl as GridControl;
            List<string> listBandNames = new List<string>();
            Dictionary<string, GridBand> listBand = new Dictionary<string, GridBand>();
            //DataRow[] drs = dtShow.Select("GroupName='" + gv.Name + "'");
            dtShow.DefaultView.RowFilter = "GroupName='" + gv.Name + "'";
            foreach (DataRowView dr in dtShow.DefaultView)
            {
                int iwd = int.Parse(dr["ControlWidth"].ToString());
                int iHd = int.Parse(dr["ControlHeight"].ToString());
                string strCName = dr["ControlName"].ToString();
                string strCFiled = dr["ControlFiled"].ToString();
                string strCKey = dr["SetKey"].ToString();
                string strTitle = dr["ShowText"].ToString();
                string ShowTextParent = dr["ShowTextParent"].ToString();
                if (ShowTextParent == string.Empty)
                    ShowTextParent = strTitle;

                if (!listBand.Keys.Contains(ShowTextParent))
                {
                    GridBand gb = new GridBand();
                    //gb.AppearanceHeader.Options.UseTextOptions = true;
                    //gb.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gb.Caption = ShowTextParent;
                    gb.Name = ShowTextParent;
                    gv.Bands.Add(gb);

                    listBand.Add(ShowTextParent, gb);
                }

                BandedGridColumn gc = new BandedGridColumn();
                gv.Columns.Add(gc);
                listBand[ShowTextParent].Columns.Add(gc);

                gc.Caption = strTitle;
                gc.FieldName = strCFiled;
                gc.Name = strCName;
                bool blROnly = bool.Parse(dr["IsReadOnly"].ToString());
                gc.OptionsColumn.ReadOnly = blROnly;
                gc.OptionsColumn.AllowEdit = bool.Parse(dr["gvAllowEdit"].ToString());
                gc.DisplayFormat.FormatString = dr["gvFormatString"].ToString();
                gc.DisplayFormat.FormatType = (DevExpress.Utils.FormatType)int.Parse(dr["gvFormatType"].ToString());
                gc.SummaryItem.SummaryType = (DevExpress.Data.SummaryItemType)int.Parse(dr["MaskType"].ToString());
                gc.SummaryItem.DisplayFormat = dr["EditMask"].ToString();
                gc.Visible = bool.Parse(dr["IsVisible"].ToString());

                string strType = dr["ControlType"].ToString();
                if (strType == "2")
                {
                    RepositoryItemLookUpEdit dpl = new RepositoryItemLookUpEdit();
                    gridC.RepositoryItems.Add(dpl);
                    gc.ColumnEdit = dpl;

                    dpl.NullText = "";
                    dpl.AutoHeight = false;
                    gc.OptionsColumn.AllowEdit = !blROnly;
                    dpl.Name = "Rep" + strCName;
                    if (strCKey != string.Empty)
                    {
                        StaticFunctions.BindDplComboByTable(dpl, dtConst, "Name", "SetValue",
                           new string[] { "Number", "Name" },
                           new string[] { "编号", "名称" }, true, "SetOrder", string.Format("SetKey='{0}'", strCKey), true);
                    }
                }
                else if (strType == "3")
                {
                    RepositoryItemCheckedComboBoxEdit dpl = new RepositoryItemCheckedComboBoxEdit();
                    gridC.RepositoryItems.Add(dpl);
                    gc.ColumnEdit = dpl;
                    gc.OptionsColumn.AllowEdit = false;

                    dpl.NullText = "";
                    dpl.AutoHeight = false;
                    dpl.DisplayMember = "Name";
                    dpl.Name = "Rep" + strCName;
                    dpl.ValueMember = "SetValue";

                    using (DataTable dtChkList = dtConst.Clone())
                    {
                        DataRow[] drLists = dtConst.Select(string.Format("SetKey='{0}'", strCKey));
                        foreach (DataRow drList in drLists)
                        {
                            dtChkList.ImportRow(drList);
                        }
                        dtChkList.AcceptChanges();
                        dpl.DataSource = dtChkList.DefaultView;
                    }
                }
            }

            StaticFunctions.InitGridViewStyle(gv);
        }

        #region 大产品库

        /// <summary>转换函数
        /// 
        /// </summary>
        /// <param name="Grid"></param>
        /// <param name="node"></param>
        /// <param name="viewName"></param>
        /// <param name="removeOld"></param>
        public static void ChangeView(GridControl Grid, GridLevelNode node, string viewName, bool removeOld)
        {
            GridLevelNode levelNode = node;
            if (levelNode == null) return;
            BaseView view = Grid.CreateView(viewName);
            Grid.ViewCollection.Add(view);
            BaseView prev = levelNode.LevelTemplate;
            MemoryStream ms = null;
            if (prev != null)
            {
                ms = new MemoryStream();
                prev.SaveLayoutToStream(ms, OptionsLayoutBase.FullLayout);
            }

            if (node.IsRootLevel)
            {
                prev = Grid.MainView;
                Grid.MainView = view;
            }
            else
            {
                levelNode.LevelTemplate = view;
            }
            if (ms != null)
            {
                if (removeOld && prev != null) prev.Dispose();
                ms.Seek(0, SeekOrigin.Begin);
                view.RestoreLayoutFromStream(ms, OptionsLayoutBase.FullLayout);
                ms.Close();
                MethodInfo mi = view.GetType().GetMethod("DesignerMakeColumnsVisible", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance);
                if (mi != null) mi.Invoke(view, null);
                if (prev != null) AssignViewProperties(prev, view);

            }
            if (removeOld && prev != null) prev.Dispose();
        }

        public static void AssignViewProperties(BaseView prev, BaseView view)
        {
            ColumnView cprev = prev as ColumnView, cview = view as ColumnView;
            if (cprev != null && cview != null)
            {
                cview.Images = cprev.Images;
            }
        }

        public static Image GetImageByBytes(byte[] bytes)
        {
            Image photo = null;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                ms.Write(bytes, 0, bytes.Length);
                photo = Image.FromStream(ms, true);
            }
            return photo;
        }

        public static byte[] ImgToByt(Image img)
        {
            MemoryStream ms = new MemoryStream();
            byte[] imagedata = null;
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            imagedata = ms.GetBuffer();
            ms.Dispose();
            return imagedata;
        }

        public static byte[] GetImageByte(string StylePic)
        {
            byte[] retBytes = ServerRefManager.PicFileRead(StylePic, "1");
            return retBytes;
        }

        #endregion


    }
}
