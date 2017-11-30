using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using WcfSimpData;
using DevExpress.XtraBars;

namespace ProduceManager
{
    public class frmEditorBase : DevExpress.XtraEditors.XtraForm
    {
        private System.ComponentModel.Container components = null;
        private string mEditorMode;
        private string mfrmParam;
        private string _AllowOperatorList;
        private DataTable mDataTable;

        public readonly string frmReportServicesPath = System.Configuration.ConfigurationManager.AppSettings["ReportServices"];
        public readonly string frmImageFilePath = System.Configuration.ConfigurationManager.AppSettings["ImageFilePath"];
        public readonly string frmImageReadFilePath = System.Configuration.ConfigurationManager.AppSettings["ReadFile"];
        public readonly string BtProduceCS = System.Configuration.ConfigurationManager.AppSettings["BtProduceCS"];

        public DevExpress.XtraGrid.Views.Grid.GridView GridViewEdit
        {
            get;
            set;
        }

        public Control ParentControl
        {
            get;
            set;
        }

        public Control FocusedControl
        {
            get;
            set;
        }

        public bool blPrevFindControl
        {
            get;
            set;
        }

        public string strFocusedContrName
        {
            get;
            set;
        }

        public bool blNoEnterToSave
        {
            get;
            set;
        }

        public List<string> arrContrSeq
        {
            get;
            set;
        }

        public Component BtnEnterSave
        {
            get;
            set;
        }

        public string KeyFormName
        {
            get;
            set;
        }

        private static List<Form> arrOpenedForms = new List<Form>();


        public string frmAllowOperatorList
        {
            get { return _AllowOperatorList; }
            set { _AllowOperatorList = value; }
        }

        public string frmEditorMode
        {
            get
            {
                return mEditorMode;
            }
            set
            {
                mEditorMode = value == string.Empty ?  "VIEW" : value;
            }
        }

        public string frmParam
        {
            get
            {
                return mfrmParam;
            }
            set
            {
                mfrmParam = value;
            }
        }

        public DataTable frmDataTable
        {
            get
            {
                return mDataTable;
            }
            set
            {
                mDataTable = value;
            }
        }


        public frmEditorBase()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();
            frmEditorMode = "VIEW";

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //

            arrContrSeq = new List<string>();
        }

        public frmEditorBase(string Mode, string strParam)
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            frmEditorMode = Mode;
            frmParam = strParam;
            arrContrSeq = new List<string>();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        public frmEditorBase(string Mode, string strParam,DataTable dt)
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            frmEditorMode = Mode;
            frmParam = strParam;
            frmDataTable = dt;
            arrContrSeq = new List<string>();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmEditorBase
            // 
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "frmEditorBase";
            this.Text = "frmEditorBase";
            this.Enter += new System.EventHandler(this.frmEditorBase_Enter);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmEditorBase_FormClosed);
            this.ResumeLayout(false);

        }
        #endregion

        public virtual void InitialByParam(string Mode, string strParam)
        {
            InitialByParam(Mode, strParam, null);
        }

        public virtual void InitialByParam(string Mode, string strParam, DataTable dt) 
        {
            frmEditorMode = Mode;
            frmParam = strParam;
            this.mDataTable = dt;
        }

        private void frmEditorBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (arrOpenedForms.Contains(this))
                arrOpenedForms.Remove(this);

            Form frmParent = this.ParentForm;
            if (frmParent == null)
                return;

            Control[] contr = frmParent.Controls.Find("gcHome", false);
            if (contr.Length <= 0)
                return;

            Form[] charr = frmParent.MdiChildren;
            if (charr.Length <= 1)
            {
                contr[0].Show();
            }
        }

        private void frmEditorBase_Enter(object sender, EventArgs e)
        {
            CApplication.CurrForm = this;
            CApplication.Com_Prot.OnPortDataReceived += SetText;

            if (!arrOpenedForms.Contains(this))
                arrOpenedForms.Add(this);

            Form frmParent = this.ParentForm;
            if (frmParent == null)
                return;

            Control[] contr = frmParent.Controls.Find("gcHome", false);
            if (contr.Length <= 0)
                return;

            Form[] charr = frmParent.MdiChildren;
            if (charr.Length <= 1)
            {
                contr[0].Hide();
            }
        }

        public virtual void SetText(string text)
        {
        }

        public virtual bool DeleteFocusedItem()
        {
            DataRow[] drs = CApplication.App.DtAllowMenus.Select("Menus_Class='" + this.Name + "'");
            if (drs.Length <= 0)
            {
                MessageBox.Show("你没有删除的权限.");
                return false;
            }
            else
            {
                string strAllow = drs[0]["Allowed_Operator"].ToString();
                if (strAllow.StartsWith("Delete=") || strAllow.IndexOf(";Delete=") != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public virtual void SetWMode(string strMode)
        {
        }

        public virtual void GetCurrAllItem()
        {
        }

        public virtual void RefreshItem()
        {
            if (frmDataTable == null)
                return;

            if (GridViewEdit == null)
                return;

            int index = GridViewEdit.FocusedRowHandle;//原来的FocusedRowHandle
            GetCurrAllItem();

            frmDataTable.AcceptChanges();
            GridViewEdit.GridControl.DataSource = frmDataTable.DefaultView;//可能引发gridView1_FocusedRowChanged
            GridViewEdit.BestFitColumns();

            SetWMode("VIEW");
            if (index == GridViewEdit.FocusedRowHandle || frmDataTable.DefaultView.Count == 0)
            {
                //如果原来的FocusedRowHandle=新的FocusedRowHandle,
                //因为如果不等，则gridControl1.DataSource = dtDep.DefaultView会自动引发gridView1_FocusedRowChanged
                StaticFunctions.SetControlBindings(ParentControl, frmDataTable.DefaultView);
            }
        }

        public virtual Control GetControlPrev(string strContrName)
        {
            int index = arrContrSeq.IndexOf(strContrName);
            if (index == -1)
            {
                return null;
            }

            int iFidx = index - 1 < 0 ? 0 : index - 1;
            Control[] contrs = FocusedControl.Parent.Controls.Find(arrContrSeq[iFidx], false);
            if (contrs.Length <= 0)
                return null;

            return contrs[0];
        }

        public virtual void SetContrMoveNext(string strContrName, bool blPrev)
        {
            int index = arrContrSeq.IndexOf(strContrName);
            if (index == -1)
            {
                return;
            }
            if (!blPrev && index == arrContrSeq.Count - 1 || blPrev && index == 0)
            {
                return;
            }
            if (FocusedControl.GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit")
            {
                DevExpress.XtraEditors.CheckedComboBoxEdit cklis = FocusedControl as DevExpress.XtraEditors.CheckedComboBoxEdit;
                string strText = string.Empty;
                string[] arrTexts = cklis.Text.Split(",，".ToCharArray());

                foreach(CheckedListBoxItem item in cklis.Properties.Items)
                {
                    foreach (string strT in arrTexts)
                    {
                        string strTxt = strT.Trim();
                        if (strTxt == string.Empty)
                            continue;

                        if (item.Value.ToString().ToLower() == strTxt.ToLower() ||
                           item.Description.ToLower() == strTxt.ToLower())
                        {
                            strText += strText == string.Empty ? item.Value.ToString() : "," + item.Value.ToString();
                        }
                    }
                }
                cklis.EditValue = strText;
                cklis.RefreshEditValue();
            }
            int iFidx = blPrev ? index - 1 : index + 1;

            Control[] contrs = FocusedControl.Parent.Controls.Find(arrContrSeq[iFidx], false);
            if (contrs.Length <= 0)
                return;

            Control contr = contrs[0];
            if (contr is BaseEdit)
            {
                BaseEdit bse = contr as BaseEdit;
                if (bse.Properties.ReadOnly || !bse.Visible)
                {
                    SetContrMoveNext(arrContrSeq[iFidx], blPrev);
                    return;
                }
            }
            else if (contr is ProduceManager.UcTxtPopup)
            {
                ProduceManager.UcTxtPopup bc = contr as ProduceManager.UcTxtPopup;
                if (bc.ReadOnly || !bc.Visible)
                {
                    SetContrMoveNext(arrContrSeq[iFidx], blPrev);
                    return;
                }
            }
            else if (contr is ProduceManager.UcTreeList)
            {
                ProduceManager.UcTreeList bc = contr as ProduceManager.UcTreeList;
                if (bc.ReadOnly || !bc.Visible)
                {
                    SetContrMoveNext(arrContrSeq[iFidx], blPrev);
                    return;
                }
            }
            switch (contr.GetType().ToString())
            {
                case "DevExpress.XtraEditors.TextEdit":
                    DevExpress.XtraEditors.TextEdit txt = contr as DevExpress.XtraEditors.TextEdit;
                    txt.Focus();
                    txt.SelectAll();
                    break;
                case "DevExpress.XtraEditors.MemoEdit":
                    DevExpress.XtraEditors.MemoEdit mTxt = contr as DevExpress.XtraEditors.MemoEdit;
                    mTxt.Focus();
                    mTxt.SelectAll();
                    break;
                case "DevExpress.XtraEditors.SimpleButton":
                    DevExpress.XtraEditors.SimpleButton btn = contr as DevExpress.XtraEditors.SimpleButton;
                    btn.Select();
                    break;
                case "DevExpress.XtraEditors.LookUpEdit":
                    DevExpress.XtraEditors.LookUpEdit dpl = contr as DevExpress.XtraEditors.LookUpEdit;
                    dpl.Focus();
                    dpl.ShowPopup();
                    break;
                case "DevExpress.XtraEditors.CheckedComboBoxEdit":
                    DevExpress.XtraEditors.CheckedComboBoxEdit ckcob = contr as DevExpress.XtraEditors.CheckedComboBoxEdit;
                    ckcob.Focus();
                    ckcob.SelectAll();
                    break;
                case "DevExpress.XtraEditors.DateEdit":
                    DevExpress.XtraEditors.DateEdit dt = contr as DevExpress.XtraEditors.DateEdit;
                    dt.Focus();
                    dt.Select();
                    break;
                case "ExtendControl.ExtPopupTree":
                    ExtendControl.ExtPopupTree ext = contr as ExtendControl.ExtPopupTree;
                    ext.Focus();
                    ext.ShowPopup();
                    break;
                case "ProduceManager.UcTxtPopup":
                    ProduceManager.UcTxtPopup ucp = contr as ProduceManager.UcTxtPopup;
                    ucp.Focus();
                    ucp.ShowPopup();
                    break;
                case "ProduceManager.UcTreeList":
                    ProduceManager.UcTreeList uct = contr as ProduceManager.UcTreeList;
                    uct.Focus();
                    uct.ShowPopup();
                    break;
                case "DevExpress.XtraEditors.ComboBoxEdit":
                    DevExpress.XtraEditors.ComboBoxEdit cob = contr as DevExpress.XtraEditors.ComboBoxEdit;
                    cob.Focus();
                    cob.ShowPopup(); 
                    break;
                default:
                    break;
            }
        }

        public virtual void SetContrMoveNext(string strContrName, bool blPrev,int iSeed)
        {
            int index = arrContrSeq.IndexOf(strContrName);
            if (index == -1)
            {
                return;
            }
            if (!blPrev && index == arrContrSeq.Count - 1 || blPrev && index == 0)
            {
                return;
            }
            if (FocusedControl.GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit")
            {
                DevExpress.XtraEditors.CheckedComboBoxEdit cklis = FocusedControl as DevExpress.XtraEditors.CheckedComboBoxEdit;
                string strText = string.Empty;
                string[] arrTexts = cklis.Text.Split(",，".ToCharArray());

                foreach (CheckedListBoxItem item in cklis.Properties.Items)
                {
                    foreach (string strT in arrTexts)
                    {
                        string strTxt = strT.Trim();
                        if (strTxt == string.Empty)
                            continue;

                        if (item.Value.ToString().ToLower() == strTxt.ToLower() ||
                           item.Description.ToLower() == strTxt.ToLower())
                        {
                            strText += strText == string.Empty ? item.Value.ToString() : "," + item.Value.ToString();
                        }
                    }
                }
                cklis.EditValue = strText;
                cklis.RefreshEditValue();
            }

            int iFidx = 0;
            if (blPrev)
            {
                iFidx = index - iSeed < 0 ? 0 : index - iSeed;
            }
            else
            {
                iFidx = index + iSeed > arrContrSeq.Count - 1 ? arrContrSeq.Count - 1 : index + iSeed;
            }

            Control[] contrs = FocusedControl.Parent.Controls.Find(arrContrSeq[iFidx], false);
            if (contrs.Length <= 0)
                return;

            Control contr = contrs[0];
            if (contr is BaseEdit)
            {
                BaseEdit bse = contr as BaseEdit;
                if (bse.Properties.ReadOnly || !bse.Visible)
                {
                    SetContrMoveNext(arrContrSeq[iFidx], blPrev, 1);
                    return;
                }
            }
            else if (contr is ProduceManager.UcTxtPopup)
            {
                ProduceManager.UcTxtPopup bc = contr as ProduceManager.UcTxtPopup;
                if (bc.ReadOnly || !bc.Visible)
                {
                    SetContrMoveNext(arrContrSeq[iFidx], blPrev, 1);
                    return;
                }
            }
            else if (contr is ProduceManager.UcTreeList)
            {
                ProduceManager.UcTreeList bc = contr as ProduceManager.UcTreeList;
                if (bc.ReadOnly || !bc.Visible)
                {
                    SetContrMoveNext(arrContrSeq[iFidx], blPrev, 1);
                    return;
                }
            }
            switch (contr.GetType().ToString())
            {
                case "DevExpress.XtraEditors.TextEdit":
                    DevExpress.XtraEditors.TextEdit txt = contr as DevExpress.XtraEditors.TextEdit;
                    txt.Focus();
                    txt.SelectAll();
                    break;
                case "DevExpress.XtraEditors.MemoEdit":
                    DevExpress.XtraEditors.MemoEdit mTxt = contr as DevExpress.XtraEditors.MemoEdit;
                    mTxt.Focus();
                    mTxt.SelectAll();
                    break;
                case "DevExpress.XtraEditors.SimpleButton":
                    DevExpress.XtraEditors.SimpleButton btn = contr as DevExpress.XtraEditors.SimpleButton;
                    btn.Select();
                    break;
                case "DevExpress.XtraEditors.LookUpEdit":
                    DevExpress.XtraEditors.LookUpEdit dpl = contr as DevExpress.XtraEditors.LookUpEdit;
                    dpl.Focus();
                    dpl.ShowPopup();
                    break;
                case "DevExpress.XtraEditors.CheckedComboBoxEdit":
                    DevExpress.XtraEditors.CheckedComboBoxEdit ckcob = contr as DevExpress.XtraEditors.CheckedComboBoxEdit;
                    ckcob.Focus();
                    ckcob.SelectAll();
                    break;
                case "DevExpress.XtraEditors.DateEdit":
                    DevExpress.XtraEditors.DateEdit dt = contr as DevExpress.XtraEditors.DateEdit;
                    dt.Focus();
                    dt.Select();
                    break;
                case "ExtendControl.ExtPopupTree":
                    ExtendControl.ExtPopupTree ext = contr as ExtendControl.ExtPopupTree;
                    ext.Focus();
                    ext.ShowPopup();
                    break;
                case "ProduceManager.UcTxtPopup":
                    ProduceManager.UcTxtPopup ucp = contr as ProduceManager.UcTxtPopup;
                    ucp.Focus();
                    ucp.ShowPopup();
                    break;
                case "ProduceManager.UcTreeList":
                    ProduceManager.UcTreeList uct = contr as ProduceManager.UcTreeList;
                    uct.Focus();
                    uct.ShowPopup();
                    break;
                case "DevExpress.XtraEditors.ComboBoxEdit":
                    DevExpress.XtraEditors.ComboBoxEdit cob = contr as DevExpress.XtraEditors.ComboBoxEdit;
                    cob.Focus();
                    cob.ShowPopup();
                    break;
                default:
                    break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            CApplication.App.CurrentSession.TimerId = 0;
            frmEditorBase frm = CApplication.CurrForm as frmEditorBase;
            if (frm == null)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            int k = msg.WParam.ToInt32();
            blPrevFindControl = false;
            if (k == 123) //F12
            {
                if (CApplication.Com_Prot != null)
                    CApplication.Com_Prot.port_Send();
            }
            else if (k == 27)//Esc
            {
                frm.Close();
                frm.Dispose();
            }
            else if (k == 13)//Enter
            {
                if (string.IsNullOrEmpty(strFocusedContrName) || FocusedControl == null)
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                if (!blNoEnterToSave)
                {
                    int index = arrContrSeq.IndexOf(strFocusedContrName);
                    if (index == arrContrSeq.Count - 1 && BtnEnterSave != null)
                    {
                        string strBtnType = BtnEnterSave.GetType().ToString();
                        if (strBtnType == "DevExpress.XtraEditors.SimpleButton")
                        {
                            (BtnEnterSave as SimpleButton).PerformClick();
                        }
                        else if (strBtnType == "DevExpress.XtraBars.BarButtonItem")
                        {
                            (BtnEnterSave as BarButtonItem).PerformClick();
                        }
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                }
                if (FocusedControl is BaseEdit)
                {
                    BaseEdit bc = FocusedControl as BaseEdit;
                    if (bc.Properties.ReadOnly)
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                }
                else if (FocusedControl is ProduceManager.UcTxtPopup)
                {
                    ProduceManager.UcTxtPopup bc = FocusedControl as ProduceManager.UcTxtPopup;
                    if (bc.ReadOnly)
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                }
                else if (FocusedControl is ProduceManager.UcTreeList)
                {
                    ProduceManager.UcTreeList bc = FocusedControl as ProduceManager.UcTreeList;
                    if (bc.ReadOnly)
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                }
                string strType = FocusedControl.GetType().ToString();
                if (strType == "DevExpress.XtraEditors.LookUpEdit")
                {
                    if (!(FocusedControl as DevExpress.XtraEditors.LookUpEdit).IsPopupOpen)
                        frm.SetContrMoveNext(strFocusedContrName, blPrevFindControl);
                }
                else if (strType == "ExtendControl.ExtPopupTree")
                {
                    if (!(FocusedControl as ExtendControl.ExtPopupTree).IsPopupOpen)
                        frm.SetContrMoveNext(strFocusedContrName, blPrevFindControl);
                }
                else if (strType == "ProduceManager.UcTxtPopup")
                {
                    if (!(FocusedControl as ProduceManager.UcTxtPopup).IsPopupOpen)
                        frm.SetContrMoveNext(strFocusedContrName, blPrevFindControl);
                }
                else if (strType == "ProduceManager.UcTreeList")
                {
                    if (!(FocusedControl as ProduceManager.UcTreeList).IsPopupOpen)
                        frm.SetContrMoveNext(strFocusedContrName, blPrevFindControl);
                }
                else
                {
                    frm.SetContrMoveNext(strFocusedContrName, blPrevFindControl);
                }
            }
            else if (k == 187 || k == 107 || k == 227) // +
            {
                if (keyData.ToString().ToUpper().IndexOf("CONTROL") != -1)
                {
                    if (string.IsNullOrEmpty(strFocusedContrName) || FocusedControl == null)
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                    if (FocusedControl is BaseEdit)
                    {
                        BaseEdit bc = FocusedControl as BaseEdit;
                        if (bc.Properties.ReadOnly)
                        {
                            return base.ProcessCmdKey(ref msg, keyData);
                        }
                    }
                    else if (FocusedControl is ProduceManager.UcTxtPopup)
                    {
                        ProduceManager.UcTxtPopup bc = FocusedControl as ProduceManager.UcTxtPopup;
                        if (bc.ReadOnly)
                        {
                            return base.ProcessCmdKey(ref msg, keyData);
                        }
                    }
                    else if (FocusedControl is ProduceManager.UcTreeList)
                    {
                        ProduceManager.UcTreeList bc = FocusedControl as ProduceManager.UcTreeList;
                        if (bc.ReadOnly)
                        {
                            return base.ProcessCmdKey(ref msg, keyData);
                        }
                    }

                    string strType = FocusedControl.GetType().ToString();
                    if (strType == "DevExpress.XtraEditors.LookUpEdit" &&
                        (FocusedControl as DevExpress.XtraEditors.LookUpEdit).IsPopupOpen)
                    {
                        blPrevFindControl = true;
                        (FocusedControl as DevExpress.XtraEditors.LookUpEdit).ClosePopup();
                        blPrevFindControl = false;
                    }
                    else if (strType == "ExtendControl.ExtPopupTree" &&
                        (FocusedControl as ExtendControl.ExtPopupTree).IsPopupOpen)
                    {
                        blPrevFindControl = true;
                        (FocusedControl as ExtendControl.ExtPopupTree).ClosePopup();
                        blPrevFindControl = false;
                    }
                    else if (strType == "ProduceManager.UcTxtPopup" &&
                        (FocusedControl as ProduceManager.UcTxtPopup).IsPopupOpen)
                    {
                        blPrevFindControl = true;
                        (FocusedControl as ProduceManager.UcTxtPopup).ClosePopup();
                        blPrevFindControl = false;
                    }
                    else if (strType == "ProduceManager.UcTreeList" &&
                        (FocusedControl as ProduceManager.UcTreeList).IsPopupOpen)
                    {
                        blPrevFindControl = true;
                        (FocusedControl as ProduceManager.UcTreeList).ClosePopup();
                        blPrevFindControl = false;
                    }
                    frm.SetContrMoveNext(strFocusedContrName, blPrevFindControl, 3);

                    return true;
                }
            }
            else if (k == 189 || k == 109 || k == 229) // - //36:Home
            {
                if (keyData.ToString().ToUpper().IndexOf("CONTROL") != -1)
                {
                    if (string.IsNullOrEmpty(strFocusedContrName) || FocusedControl == null)
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                    if (FocusedControl is BaseEdit)
                    {
                        BaseEdit bc = FocusedControl as BaseEdit;
                        if (bc.Properties.ReadOnly)
                        {
                            return base.ProcessCmdKey(ref msg, keyData);
                        }
                    }
                    else if (FocusedControl is ProduceManager.UcTxtPopup)
                    {
                        ProduceManager.UcTxtPopup bc = FocusedControl as ProduceManager.UcTxtPopup;
                        if (bc.ReadOnly)
                        {
                            return base.ProcessCmdKey(ref msg, keyData);
                        }
                    }
                    else if (FocusedControl is ProduceManager.UcTreeList)
                    {
                        ProduceManager.UcTreeList bc = FocusedControl as ProduceManager.UcTreeList;
                        if (bc.ReadOnly)
                        {
                            return base.ProcessCmdKey(ref msg, keyData);
                        }
                    }
                    blPrevFindControl = true;

                    string strType = FocusedControl.GetType().ToString();
                    if (strType == "DevExpress.XtraEditors.LookUpEdit" &&
                        (FocusedControl as DevExpress.XtraEditors.LookUpEdit).IsPopupOpen)
                    {
                        (FocusedControl as DevExpress.XtraEditors.LookUpEdit).ClosePopup();
                    }
                    else if (strType == "ExtendControl.ExtPopupTree" &&
                        (FocusedControl as ExtendControl.ExtPopupTree).IsPopupOpen)
                    {
                        (FocusedControl as ExtendControl.ExtPopupTree).ClosePopup();
                    }
                    else if (strType == "ProduceManager.UcTxtPopup" &&
                        (FocusedControl as ProduceManager.UcTxtPopup).IsPopupOpen)
                    {
                        (FocusedControl as ProduceManager.UcTxtPopup).ClosePopup();
                    }
                    else if (strType == "ProduceManager.UcTreeList" &&
                        (FocusedControl as ProduceManager.UcTreeList).IsPopupOpen)
                    {
                        (FocusedControl as ProduceManager.UcTreeList).ClosePopup();
                    }
                    frm.SetContrMoveNext(strFocusedContrName, blPrevFindControl);

                    return true;
                }
            }
            else if (k == 33)//PgUp
            {
                if (GridViewEdit != null)
                {
                    GridViewEdit.MovePrev();
                    return true;
                }
            }
            else if (k == 34)//PgDn
            {
                if (GridViewEdit != null)
                {
                    GridViewEdit.MoveNext();
                    return true;
                }
            }
            else if (k == 46)//Delete
            {
                frm.DeleteFocusedItem();
                return true;
                //if (GridViewEdit != null && GridViewEdit.GetFocusedDataRow() != null && frmEditorMode=="VIEW")
                //{
                //    frm.DeleteFocusedItem();
                //}
            }
            else if (k == 116)//F5
            {
                frm.RefreshItem();
                return true;
            }
            else if (k == 20)//CapsLk
            {
                if (keyData.ToString().ToUpper().IndexOf("ALT") != -1)
                {
                    int idex = arrOpenedForms.IndexOf(this);
                    if (idex == -1)
                        return true;

                    if (idex == 0)
                        idex = arrOpenedForms.Count - 1;
                    else
                        idex -= 1;


                    Form[] charr = this.ParentForm.MdiChildren;
                    foreach (Form chform in charr)
                    {
                        if (chform.Name.ToUpper() == arrOpenedForms[idex].Name.ToUpper())
                        {
                            chform.Show();
                            chform.Activate();

                            return true;
                        }
                    }
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public DataSet GetFrmLoadDs(string strFromNme)
        {
            string[] strKey = "Form,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
            string[] strVal = new string[] { strFromNme, 
                CApplication.App.CurrentSession.UserId.ToString(), 
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString()};
            MethodRequest req = new MethodRequest();
            req.ParamKeys = strKey;
            req.ParamVals = strVal;
            req.ProceName = "Get_Form_Load_Table";
            req.ProceDb = this.BtProduceCS;
            DataSet ds = ServerRequestProcess.DbRequest.DataRequest_By_DataSet(req);
            if (ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count >= 1 && ds.Tables[0].Columns.Contains("ERROR"))
            {
                MessageBox.Show("出错:" + ds.Tables[0].Rows[0]["ERROR"].ToString());
                return null;
            }
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count >= 1 && dt.Columns.Contains("ERROR"))
                {
                    MessageBox.Show("出错:" + dt.Rows[0]["ERROR"].ToString());
                    return null;
                }
            }
            ds.AcceptChanges();
            return ds;
        }
        public DataSet GetFrmLoadDs(string strFromNme, string strGpNme)
        {
            string[] strKey = "Form,GroupName,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
            string[] strVal = new string[] { strFromNme,strGpNme, 
                CApplication.App.CurrentSession.UserId.ToString(), 
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString()};
            MethodRequest req = new MethodRequest();
            req.ParamKeys = strKey;
            req.ParamVals = strVal;
            req.ProceName = "Get_Form_Load_Table";
            req.ProceDb = this.BtProduceCS;
            DataSet ds = ServerRequestProcess.DbRequest.DataRequest_By_DataSet(req);
            if (ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count >= 1 && ds.Tables[0].Columns.Contains("ERROR"))
            {
                MessageBox.Show("出错:" + ds.Tables[0].Rows[0]["ERROR"].ToString());
                return null;
            }
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count >= 1 && dt.Columns.Contains("ERROR"))
                {
                    MessageBox.Show("出错:" + dt.Rows[0]["ERROR"].ToString());
                    return null;
                }
            }
            ds.AcceptChanges();
            return ds;
        }
        public DataSet GetFrmLoadDsNew(string strFromNme)
        {
            string[] strKey = "Form,IsNew,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
            string[] strVal = new string[] { strFromNme,"1", 
                CApplication.App.CurrentSession.UserId.ToString(), 
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString()};
            MethodRequest req = new MethodRequest();
            req.ParamKeys = strKey;
            req.ParamVals = strVal;
            req.ProceName = "Get_Form_Load_Table";
            req.ProceDb = this.BtProduceCS;
            DataSet ds = ServerRequestProcess.DbRequest.DataRequest_By_DataSet(req);
            if (ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count >= 1 && ds.Tables[0].Columns.Contains("ERROR"))
            {
                MessageBox.Show("出错:" + ds.Tables[0].Rows[0]["ERROR"].ToString());
                return null;
            }
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count >= 1 && dt.Columns.Contains("ERROR"))
                {
                    MessageBox.Show("出错:" + dt.Rows[0]["ERROR"].ToString());
                    return null;
                }
            }
            ds.AcceptChanges();
            return ds;
        }
        public DataSet GetFrmLoadDsAdt(string strFromNme)
        {
            string[] strKey = "Form,IsAdt,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
            string[] strVal = new string[] { strFromNme,"1", 
                CApplication.App.CurrentSession.UserId.ToString(), 
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString()};
            MethodRequest req = new MethodRequest();
            req.ParamKeys = strKey;
            req.ParamVals = strVal;
            req.ProceName = "Get_Form_Load_Table";
            req.ProceDb = this.BtProduceCS;
            DataSet ds = ServerRequestProcess.DbRequest.DataRequest_By_DataSet(req);
            if (ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count >= 1 && ds.Tables[0].Columns.Contains("ERROR"))
            {
                MessageBox.Show("出错:" + ds.Tables[0].Rows[0]["ERROR"].ToString());
                return null;
            }
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count >= 1 && dt.Columns.Contains("ERROR"))
                {
                    MessageBox.Show("出错:" + dt.Rows[0]["ERROR"].ToString());
                    return null;
                }
            }
            StaticFunctions.SetDataSetTableName(ds);
            ds.AcceptChanges();
            return ds;
        }
        public DataSet GetFrmLoadUkyndaDsAdt(string strFromNme)
        {
            string[] strKey = "Form,EUser_Id,EDept_Id,EFy_Id".Split(",".ToCharArray());
            string[] strVal = new string[] { strFromNme, 
                CApplication.App.CurrentSession.UserId.ToString(), 
                CApplication.App.CurrentSession.DeptId.ToString(),
                CApplication.App.CurrentSession.FyId.ToString()};
            MethodRequest req = new MethodRequest();
            req.ParamKeys = strKey;
            req.ParamVals = strVal;
            req.ProceName = "Get_Form_Load_Table_UkyndaIsAdt";
            req.ProceDb = this.BtProduceCS;
            DataSet ds = ServerRequestProcess.DbRequest.DataRequest_By_DataSet(req);
            if (ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count >= 1 && ds.Tables[0].Columns.Contains("ERROR"))
            {
                MessageBox.Show("出错:" + ds.Tables[0].Rows[0]["ERROR"].ToString());
                return null;
            }
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count >= 1 && dt.Columns.Contains("ERROR"))
                {
                    MessageBox.Show("出错:" + dt.Rows[0]["ERROR"].ToString());
                    return null;
                }
            }
            StaticFunctions.SetDataSetTableName(ds);
            ds.AcceptChanges();
            return ds;
        }
        public DataSet DataRequest_By_DataSet(string strSpName, string[] strKey, string[] strVal)
        {
            MethodRequest req = new MethodRequest();
            req.ParamKeys = strKey;
            req.ParamVals = strVal;
            req.ProceName = strSpName;
            req.ProceDb = this.BtProduceCS;
            DataSet ds = ServerRequestProcess.DbRequest.DataRequest_By_DataSet(req);
            if (ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count >= 1 && ds.Tables[0].Columns.Contains("ERROR"))
            {
                MessageBox.Show("出错:" + ds.Tables[0].Rows[0]["ERROR"].ToString());
                return null;
            }
            foreach (DataTable dt in ds.Tables)
            {
                if (dt.Rows.Count >= 1 && dt.Columns.Contains("ERROR"))
                {
                    MessageBox.Show("出错:" + dt.Rows[0]["ERROR"].ToString());
                    return null;
                }
            }
            if (ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count >= 1 && ds.Tables[0].Columns.Contains("UkyndaMsg")
                && ds.Tables[0].Rows[0]["UkyndaMsg"].ToString() !=string.Empty)
            {
                MessageBox.Show("注意:" + ds.Tables[0].Rows[0]["UkyndaMsg"].ToString());
            }
            StaticFunctions.SetDataSetTableName(ds);
            StaticFunctions.SetDataSetNullRowToMinRowAmount(ds);
            ds.AcceptChanges();
            return ds;
        }
        public DataTable DataRequest_By_DataTable(string strSpName, string[] strKey, string[] strVal)
        {
            MethodRequest req = new MethodRequest();
            req.ParamKeys = strKey;
            req.ParamVals = strVal;
            req.ProceName = strSpName;
            req.ProceDb = this.BtProduceCS;
            DataTable dt = ServerRequestProcess.DbRequest.DataRequest_By_DataTable(req);
            if (dt.Rows.Count >= 1 && dt.Columns.Contains("ERROR"))
            {
                MessageBox.Show("出错:" + dt.Rows[0]["ERROR"].ToString());
                return null;
            }
            if (dt.Rows.Count >= 1 && dt.Columns.Contains("UkyndaMsg")
                && dt.Rows[0]["UkyndaMsg"].ToString() != string.Empty)
            {
                MessageBox.Show("注意:" + dt.Rows[0]["UkyndaMsg"].ToString());
            }
            StaticFunctions.SetDataSetNullRowToMinRowAmount(dt);
            dt.AcceptChanges();
            return dt;
        }
    }
}
