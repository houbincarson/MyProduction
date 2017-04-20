using System;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System.ComponentModel;
using System.Drawing.Design;
using System.Data;
using System.ComponentModel.Design;
using DataTableExtend;

namespace ExtendControl
{
  public class ExtendPopupGridEdit : DevExpress.XtraEditors.PopupContainerEdit
  {
    #region 变量区
    private string[] _Captions = null;
    private string[] _Fields = null;
    public DataTable DataSource
    {
      set
      {
        if (value == null)
        {
          return;
        }
        this._DataSource = value.Copy();
        if (this._DataSource.Columns.IndexOf("Is_Sel") == -1)
        {
          this._DataSource.Columns.Add("Is_Sel", typeof(Boolean));
        }
        if (string.Format("{0}", this.EditValues).Trim().Length > 0 && string.Format("{0}", ValueMember).Trim().Length > 0)
        {
          this._DataSource.DefaultView.RowFilter = string.Format("{0} IN ({1})", ValueMember, this.EditValues);
          DataTableHelper.UpDataTableColValue(this._DataSource.DefaultView, "Is_Sel", true, false);
          this._DataSource.DefaultView.RowFilter = string.Empty;
        }
        this._DataSource.AcceptChanges();
        this.gdCtlPop.DataSource = this._DataSource; 
      }
      get
      {
        return _DataSource;
      }
    }
    private DataTable _DataSource = null;

    private DevExpress.XtraEditors.PopupContainerControl popCrSend;
    private DevExpress.XtraGrid.GridControl gdCtlPop;
    private ExtendControl.ExtendGridView gdVwPop;
    private DevExpress.XtraEditors.PanelControl plCtlBottom;
    private DevExpress.XtraEditors.SimpleButton btnChg;
    private DevExpress.XtraEditors.SimpleButton btnOk;
    #endregion

    #region 属性
    [Description("设置下拉表样式.")]
    public GridView PopView
    {
      get
      {
        return gdVwPop;
      }
    }
    public event EventHandler BtnOkClick
    {

      add
      {
        _OnBtnOkClick += value;
      }
      remove
      {
        _OnBtnOkClick -= null;
      }
    }
    private EventHandler _OnBtnOkClick = null;
    public event EventHandler BtnChgClick
    {

      add
      {
        _OnBtnChgClick += value;
      }
      remove
      {
        _OnBtnChgClick -= null;
      }
    }
    private EventHandler _OnBtnChgClick = null;
    public string DisplayMember { get; set; }
    public string ValueMember { get; set; }
    #endregion
    #region 重载
    public object EditValues
    {
      get
      {
        return _EditValues;
      }
      set
      {
        _EditValues = value;
      }
    }
    private object _EditValues = null;
    private string _Text = string.Empty;
    public override void ClosePopup()
    {
      if (this._DataSource != null)
      {
        this._DataSource.RejectChanges();
      }
      base.ClosePopup();
    }
    #endregion
    #region 初始化函数
    public ExtendPopupGridEdit()
      : base()
    {
      IsInitPopView = false;
      InitializeComponent();
      RepositoryItemPopupContainerEdit _popContainer=(this.fProperties as RepositoryItemPopupContainerEdit);
      _popContainer.PopupControl = this.popCrSend;
      _popContainer.CloseOnLostFocus = true;
      _popContainer.ShowPopupCloseButton = false;
    }
    private void InitializeComponent()
    {
      this.popCrSend = new DevExpress.XtraEditors.PopupContainerControl();
      this.gdCtlPop = new DevExpress.XtraGrid.GridControl();
      this.gdVwPop = new ExtendControl.ExtendGridView();
      this.plCtlBottom = new DevExpress.XtraEditors.PanelControl();
      this.btnChg = new DevExpress.XtraEditors.SimpleButton();
      this.btnOk = new DevExpress.XtraEditors.SimpleButton();
      ((System.ComponentModel.ISupportInitialize)(this.popCrSend)).BeginInit();
      this.popCrSend.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gdCtlPop)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gdVwPop)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.plCtlBottom)).BeginInit();
      this.plCtlBottom.SuspendLayout();
      this.SuspendLayout();
      // 
      // popCrSend
      // 
      this.popCrSend.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
      this.popCrSend.Controls.Add(this.gdCtlPop);
      this.popCrSend.Controls.Add(this.plCtlBottom);
      this.popCrSend.Location = new System.Drawing.Point(0, 114);
      this.popCrSend.Name = "popCrSend";
      this.popCrSend.Size = new System.Drawing.Size(506, 230);
      this.popCrSend.TabIndex = 1;
      // 
      // gdCtlPop
      // 
      this.gdCtlPop.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gdCtlPop.Location = new System.Drawing.Point(2, 2);
      this.gdCtlPop.MainView = this.gdVwPop;
      this.gdCtlPop.Name = "gdCtlPop";
      this.gdCtlPop.Size = new System.Drawing.Size(502, 193);
      this.gdCtlPop.TabIndex = 4;
      this.gdCtlPop.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gdVwPop});
      // 
      // gdVwPop
      // 
      this.gdVwPop.GridControl = this.gdCtlPop;
      this.gdVwPop.IndicatorWidth = 32;
      this.gdVwPop.Name = "gdVwPop";
      this.gdVwPop.NoActionEvent = false;
      this.gdVwPop.OptionsSelection.MultiSelect = true;
      this.gdVwPop.OptionsView.ColumnAutoWidth = false;
      this.gdVwPop.OptionsView.ShowFooter = true;
      this.gdVwPop.OptionsView.ShowGroupPanel = false;
      // 
      // plCtlBottom
      // 
      this.plCtlBottom.Controls.Add(this.btnChg);
      this.plCtlBottom.Controls.Add(this.btnOk);
      this.plCtlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.plCtlBottom.Location = new System.Drawing.Point(2, 195);
      this.plCtlBottom.Name = "plCtlBottom";
      this.plCtlBottom.Size = new System.Drawing.Size(502, 33);
      this.plCtlBottom.TabIndex = 5;
      // 
      // btnChg
      // 
      this.btnChg.Location = new System.Drawing.Point(112, 7);
      this.btnChg.Name = "btnChg";
      this.btnChg.Size = new System.Drawing.Size(60, 23);
      this.btnChg.TabIndex = 1;
      this.btnChg.Text = "取消";
      this.btnChg.Click += new System.EventHandler(this.btnChg_Click);
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(23, 7);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(60, 23);
      this.btnOk.TabIndex = 0;
      this.btnOk.Text = "确定";
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      ((System.ComponentModel.ISupportInitialize)(this.popCrSend)).EndInit();
      this.popCrSend.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gdCtlPop)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gdVwPop)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.plCtlBottom)).EndInit();
      this.plCtlBottom.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    #endregion
    public bool IsInitPopView {get;set;}
    public void InitPopView(string[] _Captions, string[] _Fields, DataTable _DataSource)
    {
      if (_Captions != null && _Fields != null &&
        _Captions.Length > 0 && _Fields.Length > 0 && _Captions.Length == _Fields.Length)
      {
      this._Captions = _Captions;
      this._Fields = _Fields;
        int _i = 1, _iCnt = _Captions.Length + 1, _c = 0;
        DevExpress.XtraGrid.Columns.GridColumn[] _cols = new DevExpress.XtraGrid.Columns.GridColumn[_iCnt];

        _cols[0] = new DevExpress.XtraGrid.Columns.GridColumn();
        _cols[0].Width = 60;
        _cols[0].Caption = "选中";
        _cols[0].FieldName = "Is_Sel";
        _cols[0].Visible = true;
        _cols[0].VisibleIndex = 0;
        string[] _colInf = null;
        int _totalWidth = 100, _width = 0;
        for (; _i < _iCnt; _i++, _c++)
        {
          _colInf = _Fields[_c].Split('=');
          _cols[_i] = new DevExpress.XtraGrid.Columns.GridColumn();
          _cols[_i].Caption = _Captions[_c];
          _cols[_i].FieldName = _colInf[0];// _Fields[_c];
          if (_colInf.Length > 1)
          {
            int.TryParse(_colInf[1], out _width);
            _width = _width == 0 ? 70 : _width;
          }
          else
          {
            _width = 70;
          }
          _cols[_i].Width = _width; 
          _totalWidth += _width;
          _cols[_i].OptionsColumn.ReadOnly = true;
          _cols[_i].Visible = true;
          _cols[_i].VisibleIndex = _i;
        } 
        popCrSend.Width = _totalWidth;
        this.gdVwPop.Columns.AddRange(_cols);
        if (_DataSource != null)
        {
          this.DataSource = _DataSource;
        }
        IsInitPopView=true;
      }
    }
    public void SetFilter(string rowFilterString)
    {
      if (_DataSource != null)
        this._DataSource.DefaultView.RowFilter = rowFilterString;
    }
    #region 事件函数
    private void btnOk_Click(object sender, EventArgs e)
    {
      if (_DataSource != null &&
        !string.IsNullOrEmpty(ValueMember) &&
        !string.IsNullOrEmpty(DisplayMember))
      {
        string _filterString = this._DataSource.DefaultView.RowFilter;
        this._DataSource.DefaultView.RowFilter = _filterString.Length > 0 ?
          string.Format("{0}{1}", _filterString, " AND Is_Sel=1") : "Is_Sel=1";
        string _val = DataTableHelper.GetDataTableFieldVals(_DataSource.DefaultView, ValueMember),
        _txt = DataTableHelper.GetDataTableFieldVals(_DataSource.DefaultView, DisplayMember);
        this._DataSource.DefaultView.RowFilter = _filterString;
        _val = _val.Length > 0 ? _val : string.Empty;
        _txt = _val.Length > 0 ? _txt : string.Empty;
        this._EditValues = _val;
        this.EditValue = _txt;
        _DataSource.AcceptChanges();
      }
      if (_OnBtnOkClick != null)
      {
        _OnBtnOkClick(sender, e);
      }
      this.ClosePopup();
    }

    private void btnChg_Click(object sender, EventArgs e)
    {
      if (_OnBtnChgClick != null)
      {
        _OnBtnChgClick(sender, e);
      }
      this.ClosePopup();
    }
    #endregion
  }
}
