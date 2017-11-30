using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProduceManager
{
    public partial class frmSelectLimits : Form
    {

        private string cust;
        private bool flag;
        private int x;
        private int y;
        public int limit
        {
            get;
            set;
        }
        public frmSelectLimits( bool flag,string cust,int x,int y)
        {
            InitializeComponent();
            this.cust = cust;
            this.flag = flag;
            this.x = x;
            this.y = y;  
        }
        private void btnComfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void frmSelectLimits_Load(object sender, EventArgs e)
        {
            this.label1.Text="对客户："+cust+"设置权限";
            
            this.Location = new Point(x + 20, y + 20);
            if (this.flag==true)
            {
                this.chkAllowPick.Text = "全部可见可选";
                this.chkNoSee.Text = "全部不可见不可选";
                this.chkSee.Text = "全部可见不可选";
            } 

            this.chkAllowPick.CheckedChanged += new EventHandler(this.radioBtn_CheckedChange);
            this.chkSee.CheckedChanged += new EventHandler(this.radioBtn_CheckedChange);
            this.chkNoSee.CheckedChanged += new EventHandler(this.radioBtn_CheckedChange);
            chkAllowPick.Checked = true;
        }


        //RadioButton新事件
        public void radioBtn_CheckedChange(object sender, EventArgs e)
        {
            if (!((RadioButton)sender).Checked)
            {
                return;
            }
            string rechargeMoney = string.Empty;
            switch (((RadioButton)sender).Text.ToString())
            { 
                case "可见可选": 
                    this.limit =  2 ; 
                    break;
                case "可见不可选": 
                    this.limit = 1 ; 
                    break;
                case "不可见不可选": 
                    this.limit = 0  ; 
                    break;
                case "全部可见可选":
                    this.limit =  2 ; 
                    break;
                case "全部可见不可选":
                    this.limit =  1 ; 
                    break;
                case "全部不可见不可选":
                    this.limit =  0 ; 
                    break; 
                default:
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
