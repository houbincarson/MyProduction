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
    public partial class frmSelectLimitsArea : Form
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
        public frmSelectLimitsArea(bool flag, string cust, int x, int y)
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

        private void frmSelectLimitsArea_Load(object sender, EventArgs e)
        {
            this.label1.Text="对区域："+cust+"设置权限";
            
            this.Location = new Point(x + 20, y + 20);
            if (this.flag==true)
            {
                this.chkAllowPick.Text = "区域全部可见";
                this.chkNoSee.Text = "区域全部不可见"; 
            } 

            this.chkAllowPick.CheckedChanged += new EventHandler(this.radioBtn_CheckedChange); 
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
                case "区域全部可见":
                    this.limit =  1 ; 
                    break;
                case "区域全部不可见":
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
