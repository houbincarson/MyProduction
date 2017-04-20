using System;
using System.Data;
using System.Text;

namespace ExtendControl
{
  public class ExtPopupTree : DevExpress.XtraEditors.PopupContainerEdit
  {
    #region 变量区
    private string[] _Captions = null;
    private string[] _Fields = null;
    public DataTable DataSource
    {
      set
      {
        this.popTree.DataSource =
        Properties.DataSource = value;
      }
      get
      {
        return Properties.DataSource;
      }
    }
    private bool _IsMaskBoxKeyPress = false;
    private string _DropDisplayName;

    private DevExpress.XtraEditors.PopupContainerControl popCrSend;
    private DevExpress.XtraEditors.PanelControl plCtlBottom;
    private DevExpress.XtraEditors.SimpleButton btnChg;
    private DevExpress.XtraEditors.SimpleButton btnOk;
    private DevExpress.XtraTreeList.TreeList popTree;
    #endregion
    #region 属性
    public DevExpress.XtraTreeList.TreeList PopTree
    {
      get
      {
        return popTree;
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
    public string DisplayMember
    {
      get
      {
        return Properties.DisplayMember;
      }
      set
      {
        Properties.DisplayMember = value;
      }
    }
    public string ValueMember
    {
      get
      {
        return Properties.ValueMember;
      }
      set
      {
        Properties.ValueMember = value;
      }
    }
    #endregion

    #region 重载
    private new ExtRepositoryItemTree Properties
    {
      get
      {
        if (fProperties == null)
        {
          CreateRepositoryItem();
        }
        return ((ExtRepositoryItemTree)this.fProperties);
      }
    }
    public bool IsDisplayTextValid
    {
      get
      {
        return true;
      }
    }
    public override object EditValue
    {
      get
      {
        return base.EditValue;
      }
      set
      {
        //if (!_IsMaskBoxKeyPress && !string.Format("{0}", base.EditValue).Equals(string.Format("{0}", value)))
        if (!_IsMaskBoxKeyPress)//&& !string.Format("{0}", base.EditValue).Equals(string.Format("{0}", value))
        {
          base.EditValue = value;
          base.UpdateDisplayText();
          this.SelectAll();
          //SelectTreeNodeByText(this.MaskBox.EditText);
        }
        else
        {
          SelectTreeNodeByText(this.MaskBox.EditText);
          _IsMaskBoxKeyPress = false;
        }
      }
    }
    /// <summary>
    /// 在基类构造函数中会调用此方法，然后通过RepositoryItem显示信息
    /// </summary>
    protected override void CreateRepositoryItem()
    {
      if (fProperties == null)
      {
        this.fProperties = new ExtRepositoryItemTree();
        ((ExtRepositoryItemTree)this.fProperties).SetOwnerEdit(this);
      }
    }
    /// <summary>
    /// 在输入框中按键的时候 激发
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void OnMaskBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
    {
      if (e.KeyChar != 13)//Se
      {
        if (!this.IsPopupOpen)
        {
          this.ShowPopup();
        }
        _IsMaskBoxKeyPress = true;//放if (!this.IsPopupOpen)前会使 _IsMaskBoxKeyPress=false
        if (40 != e.KeyChar)//System.Windows.Forms.Keys.Down
        {
          ((DevExpress.XtraEditors.Mask.MaskBox)(sender)).Focus();
        }
      }
      this.OnKeyPress(e);

    }
    protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
    {
      switch (keyData)
      {
        case System.Windows.Forms.Keys.Enter:
          btnOk_Click(null, null);
          return base.ProcessCmdKey(ref msg, keyData);
        case System.Windows.Forms.Keys.Up:
          if (this.popTree.FocusedNode != null && this.popTree.FocusedNode.PrevVisibleNode != null)
          {
            this.popTree.SetFocusedNode(this.popTree.FocusedNode.PrevVisibleNode);
          }
          return true;
        case System.Windows.Forms.Keys.Down:
          if (!this.IsPopupOpen)
          {
            this.ShowPopup();
          }
          else
          {
            this.popTree.Focus();
            this.popTree.MoveNextVisible();
            //System.Windows.Forms.SendKeys.Send("{DOWN}");
          }
          return false;
        case System.Windows.Forms.Keys.Left:
        case System.Windows.Forms.Keys.Right:
          if (this.IsPopupOpen)
          {
            this.popTree.Focus();
            //System.Windows.Forms.SendKeys.Send(keyData == System.Windows.Forms.Keys.Right ? "{Right}" : "{Left}");
            if (this.popTree.FocusedNode != null)
            {
              this.popTree.FocusedNode.Expanded = keyData == System.Windows.Forms.Keys.Right;
            }
          }
          break;
      }
      return base.ProcessCmdKey(ref msg, keyData);
    }

    protected override void OnPopupShown()
    {
      _IsMaskBoxKeyPress = true;
      this.BeginUpdate();
      this.popTree.BeginUpdate();
      try
      {
        SetTreeNodesVisble(this.popTree.Nodes, true);
      }
      finally
      {
        this.popTree.EndUpdate();
        this.EndUpdate();
      }

      //popTree.FilterNodes();
      //if (_DropDisplayText.Length > 0)
      //{
      //  this.MaskBox.EditText = _DropDisplayText;
      //}
      this.SelectAll();
      SelectedNodeByEditValue(this.EditValue);
      _IsMaskBoxKeyPress = false;

      base.OnPopupShown();
    }
    private bool SelectedNodeByEditValue(object nodeValue)
    {
      object _keyValue = null;
      try
      {
        _keyValue = System.Convert.ChangeType(nodeValue, this.DataSource.Columns[this.ValueMember].DataType);
      }
      catch (Exception)
      {
      }
      if (_keyValue != null)
      {
        DevExpress.XtraTreeList.Nodes.TreeListNode node = this.popTree.FindNodeByKeyID(_keyValue);
        if (node != null)
        {
            ExpandedParentNode(node);
          //if (node.ParentNode != null && !node.ParentNode.Expanded)
          //{
          //  node.ParentNode.Expanded = node.ParentNode.Visible = true;
            //}
            this.popTree.FocusedNode = node;
          node.Selected = true;
          return true;
        }
      }
      return false;
    }
    #endregion
    private void ExpandedParentNode(DevExpress.XtraTreeList.Nodes.TreeListNode node)
    {
      if (node == null)
        return;
      DevExpress.XtraTreeList.Nodes.TreeListNode _parentNode = node.ParentNode;
      while (_parentNode != null)
      {
        _parentNode.Expanded = _parentNode.Visible = true;
        _parentNode = _parentNode.ParentNode;
      }
    }
    public void SetFilter(string rowFilterString)
    {
      if (Properties.DataSource != null)
        Properties.DataSource.DefaultView.RowFilter = rowFilterString;
    }
    private void SelectTreeNodeByText(object txt)
    {
      popTree.FilterNodes();

      if (string.Format("{0}", txt).Trim().Length == 0)
      {
        return;
      }
      string _txt = string.Format("{0}", txt).ToUpper();
      if (!SetTreeNode(this.popTree.Nodes, _txt, string.Format("{0}", this.EditValue)))
      {
        SetTreeNode(this.popTree.Nodes, _txt);
      }
    }
    private bool SetTreeNode(DevExpress.XtraTreeList.Nodes.TreeListNodes nodes, string filterTxt, string val)
    {
      for (int _i = 0, _iCnt = nodes.Count; _i < _iCnt; _i++)
      {
        string _key = string.Format("{0}", nodes[_i][_DropDisplayName]).ToUpper();
        string _val = string.Format("{0}", nodes[_i][ValueMember]);
        if ((filterTxt.Length > 2 ? _key.Contains(filterTxt) : _key.StartsWith(filterTxt)) && val.Equals(_val))
        {
          //base.EditValue = nodes[_i].GetValue(ValueMember);//选择的时候不设置，在btnOk_Click的时候设置EditValue ,
          this.popTree.FocusedNode = nodes[_i];//9.5版本必须用这个才能设置选中项
          nodes[_i].Selected = true;
          return true;
        }
        if (nodes[_i].Nodes.Count > 0)
        {
          if (SetTreeNode(nodes[_i].Nodes, filterTxt, val))
          {
            nodes[_i].Expanded = true;
            return true;
          }
        }
      }
      return false;
    }
    /// <summary>
    /// 选中根据文字查找到的第一个项
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="filterTxt"></param>
    /// <returns></returns>
    private bool SetTreeNode(DevExpress.XtraTreeList.Nodes.TreeListNodes nodes, string filterTxt)
    {
      for (int _i = 0, _iCnt = nodes.Count; _i < _iCnt; _i++)
      {
        string _key = string.Format("{0}", nodes[_i][_DropDisplayName]).ToUpper();
        if (filterTxt.Length > 2 ? _key.Contains(filterTxt) : _key.StartsWith(filterTxt))
        {
          //base.EditValue = nodes[_i].GetValue(ValueMember);//选择的时候不设置，在btnOk_Click的时候设置EditValue ,
          this.popTree.FocusedNode = nodes[_i];//9.5版本必须用这个才能设置选中项
          nodes[_i].Selected = true;
          return true;
        }
        if (nodes[_i].Nodes.Count > 0)
        {
          if (SetTreeNode(nodes[_i].Nodes, filterTxt))
          {
            nodes[_i].Expanded = true;
            return true;
          }
        }
      }
      return false;
    }
    protected void SetContainnerSize()
    {
      this.popCrSend.Size = new System.Drawing.Size(this.Width, 230);
      this.popTree.Size = new System.Drawing.Size(this.Width, 193);
      this.plCtlBottom.Size = new System.Drawing.Size(this.Width, 28);
    }
    public object GetColumnValue(string col)
    {
      if (popTree.Selection.Count > 0)
      {
        return popTree.Selection[0].GetValue(col);
      }
      return string.Empty;
    }

    private void SetTreeNodesVisble(DevExpress.XtraTreeList.Nodes.TreeListNodes nodes, bool visible)
    {
      for (int _i = 0, _iCnt = nodes.Count; _i < _iCnt; _i++)
      {
        DevExpress.XtraTreeList.Nodes.TreeListNode node = nodes[_i];
        node.Visible = visible;
        if (node.HasChildren)
        {
          node.Expanded = false;
          SetTreeNodesVisble(node.Nodes, visible);
        }
      }
    }
    #region 事件函数
    protected void PopTree_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case System.Windows.Forms.Keys.Enter:
          //btnOk_Click(null, null);
          this.Focus();
          System.Windows.Forms.SendKeys.Send("{Enter}");
          break;
        case System.Windows.Forms.Keys.Down:
        case System.Windows.Forms.Keys.Up:
          break;
        case System.Windows.Forms.Keys.Add:
        case System.Windows.Forms.Keys.Right:
          if (this.popTree.FocusedNode != null)
          {
            this.popTree.FocusedNode.Expanded = true;
          }
          break;
        case System.Windows.Forms.Keys.Subtract:
        case System.Windows.Forms.Keys.Left:
          if (this.popTree.FocusedNode != null)
          {
            if (this.popTree.FocusedNode.ParentNode != null)
            {
              this.popTree.FocusedNode.ParentNode.Expanded = false;
            }
            else
            {
              this.popTree.FocusedNode.Expanded = false;
            }
          }
          break;
        default:
          if (e.KeyValue > 47 && e.KeyValue < 91)
          {
            this.MaskBox.EditText = this.MaskBox.EditText.Substring(0, this.SelectionStart) + char.ToString((char)e.KeyValue);
            this.SelectionStart = this.MaskBox.EditText.Length;
            //this.MaskBox.SetEditValue(
            SelectTreeNodeByText(this.MaskBox.EditText);
          }
          this.Focus();
          break;
      }
    }
    private void popTree_FilterNode(object sender, DevExpress.XtraTreeList.FilterNodeEventArgs e)
    {
      string _key = string.Format("{0}", e.Node[_DropDisplayName]).ToUpper();
      string _filterTxt = this.Text.ToUpper();
      bool _isVisible = _filterTxt.Length > 2 ? _key.Contains(_filterTxt) : _key.StartsWith(_filterTxt);
      if (this.popTree.Selection.Count == 0 ||
        this.popTree.Selection[0].ParentNode == null ||
         this.popTree.Selection[0].ParentNode != e.Node)
      {
        e.Node.Visible = _isVisible;//父项设置不可见后，子项被选中项会被取消
      }
      if (_isVisible)
      {
          ExpandedParentNode(e.Node);
      }
      //if (e.Node.ParentNode != null)
      //{
      //  if (_isVisible &&
      //    (!e.Node.ParentNode.Visible || !e.Node.ParentNode.Expanded))
      //  {
      //    e.Node.ParentNode.Expanded =
      //    e.Node.ParentNode.Visible = true;
      //  }
      //}
      e.Handled = true;
    }
    /// <summary>
    /// 确定选中值，更新EditValue ,同时在EditValu中更新Text
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnOk_Click(object sender, EventArgs e)
    {
      if (Properties.DataSource != null &&
        !string.IsNullOrEmpty(ValueMember) &&
        !string.IsNullOrEmpty(DisplayMember))
      {
        StringBuilder _vals = new StringBuilder();
        StringBuilder _dropTxt = new StringBuilder();
        for (int _i = 0, _iCnt = popTree.Selection.Count; _i < _iCnt; _i++)
        {
          if (_vals.Length != 0)
          {
            _vals.Append(",");
          }
          _vals.Append(popTree.Selection[_i].GetValue(ValueMember));

          if (_dropTxt.Length != 0)
          {
            _dropTxt.Append(",");
          }
          _dropTxt.Append(popTree.Selection[_i].GetValue(_DropDisplayName));
        }
        _dropTxt = null;
        EditValue = _vals.ToString();
        _vals = null;
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

    #region IExtendContrlBind 成员
    public void DataBind(DataTable dataSource, string displayMember, string valueMember, string dropDisplayName, string sortMember, string[] colFields, string[] captions)
    {
      if (valueMember == null || valueMember.Length == 0)
      {
        return;
      }
      string[] _vals = valueMember.Split('|');
      if (_vals.Length < 2)
      {
        return;
      }
      string[] _dropSets = dropDisplayName.Split('=');
      int _dropHeight = 0;
      if (_dropSets.Length == 2)
      {
        dropDisplayName = _dropSets[0];
        int.TryParse(_dropSets[1], out _dropHeight);
      }
      else
      {
        _dropHeight = 260;
      }
      string key = _vals[0], parentKey = _vals[1];
      if (captions != null && colFields != null &&
        captions.Length > 0 && colFields.Length > 0 && captions.Length == colFields.Length)
      {
        this._Captions = captions;
        this._Fields = colFields;
        int _i = 0, _iCnt = captions.Length;
        DevExpress.XtraTreeList.Columns.TreeListColumn[] _cols = new DevExpress.XtraTreeList.Columns.TreeListColumn[_iCnt];
        string[] _colInf = null;
        int _totalWidth = 100, _width = 0;
        for (; _i < _iCnt; _i++)
        {
          _colInf = colFields[_i].Split('=');
          _cols[_i] = new DevExpress.XtraTreeList.Columns.TreeListColumn();
          _cols[_i].Caption = captions[_i];
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
        this.Properties.PopupFormSize =
        this.Properties.PopupFormMinSize = new System.Drawing.Size(_totalWidth, _dropHeight);
        this.popTree.Columns.Clear();
        this.popTree.Columns.AddRange(_cols);
        this.popTree.KeyFieldName = key;
        this.popTree.ParentFieldName = parentKey;
        if (dataSource != null)
        {
          this.DataSource = dataSource;
          this.DisplayMember = displayMember;
          this.ValueMember = key;
          this._DropDisplayName = dropDisplayName;
        }
      }
      SetContainnerSize();
    }

    #endregion

    #region 初始化函数
    public ExtPopupTree()
      : base()
    {
      InitializeComponent();
      Properties.PopupControl = this.popCrSend;
      Properties.CloseOnLostFocus = true;
      Properties.ShowPopupCloseButton = false;
      Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
    }
    private void InitializeComponent()
    {
      this.btnOk = new DevExpress.XtraEditors.SimpleButton();
      this.plCtlBottom = new DevExpress.XtraEditors.PanelControl();
      this.btnChg = new DevExpress.XtraEditors.SimpleButton();
      this.popTree = new DevExpress.XtraTreeList.TreeList();
      this.popCrSend = new DevExpress.XtraEditors.PopupContainerControl();
      ((System.ComponentModel.ISupportInitialize)(this.plCtlBottom)).BeginInit();
      this.plCtlBottom.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.popTree)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.popCrSend)).BeginInit();
      this.popCrSend.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(23, 2);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(60, 23);
      this.btnOk.Click += new EventHandler(btnOk_Click);
      this.btnOk.TabIndex = 0;
      this.btnOk.Text = "确定";
      // 
      // plCtlBottom
      // 
      this.plCtlBottom.Controls.Add(this.btnChg);
      this.plCtlBottom.Controls.Add(this.btnOk);
      this.plCtlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.plCtlBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.plCtlBottom.Location = new System.Drawing.Point(2, 195);
      this.plCtlBottom.Name = "plCtlBottom";
      this.plCtlBottom.TabIndex = 5;
      // 
      // btnChg
      // 
      this.btnChg.Location = new System.Drawing.Point(112, 2);
      this.btnChg.Name = "btnChg";
      this.btnChg.Size = new System.Drawing.Size(60, 23);
      this.btnChg.Click += new EventHandler(btnChg_Click);
      this.btnChg.TabIndex = 1;
      this.btnChg.Text = "取消";
      // 
      // popTreeList
      // 
      this.popTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.popTree.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.popTree.Location = new System.Drawing.Point(2, 2);
      this.popTree.OptionsSelection.EnableAppearanceFocusedCell = false;
      this.popTree.OptionsSelection.EnableAppearanceFocusedRow = true;
      this.popTree.OptionsSelection.MultiSelect = false;
      this.popTree.OptionsBehavior.Editable = false;
      //this.popTree.OptionsBehavior.AutoSelectAllInEditor = true;
      //this.popTree.OptionsBehavior.AllowIncrementalSearch = true;
      this.popTree.OptionsBehavior.EnableFiltering = true;
      //this.popTree.OptionsBehavior.ExpandNodesOnIncrementalSearch = true;
      this.popTree.Name = "popTreeList";
      this.popTree.TabIndex = 4;
      this.popTree.FilterNode += new DevExpress.XtraTreeList.FilterNodeEventHandler(popTree_FilterNode);
      this.popTree.KeyDown += new System.Windows.Forms.KeyEventHandler(PopTree_KeyDown);
      // 
      // popCrSend
      // 
      this.popCrSend.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
      this.popCrSend.Controls.Add(this.popTree);
      this.popCrSend.Controls.Add(this.plCtlBottom);
      this.popCrSend.Location = new System.Drawing.Point(139, 136);
      this.popCrSend.Name = "popCrSend";
      this.popCrSend.TabIndex = 2;
      ((System.ComponentModel.ISupportInitialize)(this.plCtlBottom)).EndInit();
      this.plCtlBottom.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.popTree)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.popCrSend)).EndInit();
      this.popCrSend.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion
  }
}
