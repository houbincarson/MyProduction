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
    public partial class frmSelectOperation : frmEditorBase
    {
        public frmSelectOperation()
        {
            InitializeComponent();
        }

        private void btnCost_Click(object sender, EventArgs e)
        {
            frmBseCostEdit frmCost = new frmBseCostEdit("Cost");
            frmCost.ShowDialog();
        }

        private void btnBasicFee_Click(object sender, EventArgs e)
        {
            frmBseCostEdit frmBasicFee = new frmBseCostEdit("BasicFee");
            frmBasicFee.ShowDialog();
        }

        private void btnLimits_Click(object sender, EventArgs e)
        {
            frmProdAuthorVisibleToCust cust = new frmProdAuthorVisibleToCust();
            cust.ShowDialog();
        }

        private void btnSaleFee_Click(object sender, EventArgs e)
        {
            frmSetSpecialFeeToCust fee = new frmSetSpecialFeeToCust();
            fee.ShowDialog();
        }
    }
}
