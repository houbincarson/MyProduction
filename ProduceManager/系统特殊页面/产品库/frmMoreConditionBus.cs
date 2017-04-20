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
    public partial class frmMoreConditionBus : frmEditorBase
    {
        string strSpName = "Bse_Merge_PNumber_Rel_Add_Edit_Del";
        public DataSet dtCondition
        {
            get;
            set;
        }
        private string PNumber;
        private string StDay;
        private string EdDay;
        public frmMoreConditionBus(string Condition)
        {
            InitializeComponent();
            string[] condition = Condition.Split(',');
            PNumber = condition[0].ToString();
            StDay = condition[1].ToString();
            EdDay = condition[2].ToString();
        }

        private void frmMoreCondition_Load(object sender, EventArgs e)
        {

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            dtCondition = GetDate();
            this.DialogResult = DialogResult.OK;
        }

        private DataSet GetDate()
        {
            string[] strKey = "EUser_Id,EDept_Id,Fy_Id,Name,PNumber,Designer,PatentNumber,MarketTime,Help_Factory,Pc_Id,Material,Technology,Sex,DesignType,Material_Pc_Id,Compound_Type,SizeSmall,SizeLarger,WeightSmall,WeightLarger,flag".Split(",".ToCharArray());
            //,BasicTechnology,BasicTechnologySurface,BseShapeFlower,BseShapeSquare,BseRound,BseFaceWidth,BseFastener,BseStructure
            string[] strVal = new string[] {
                     CApplication.App.CurrentSession.UserId.ToString(),
                     CApplication.App.CurrentSession.DeptId.ToString(),
                     CApplication.App.CurrentSession.FyId.ToString(),
                        "",//Name
                        "",//PNumber
                        "",//Designer
                        "",//PatentNumber
                        "",//MarketTime
                        "",//Help_Factory
                        "",//Pc_Id
                        "",//Material
                        "-1",//Technology
                        "-1",//Sex
                        "-1",//DesignType
                        "",//Material_Pc_Id
                        "-1",//Compound_Type
                        txtSizeMin.Text.Trim(),//SizeSmall
                       txtSizeMax.Text.Trim(),//SizeLarger
                        "0",//WeightSmall
                        "0",//WeightLarger
                        //"-1",//BasicTechnology
                        //"-1",//BasicTechnologySurface
                        //"-1",//BseShapeFlower
                        //"-1",//BseShapeSquare
                        //"-1",//BseRound
                        //"-1",//BseFaceWidth
                        //"-1",//BseFastener
                        //"-1",//BseStructure 
                        "1" };
            DataSet ds = this.DataRequest_By_DataSet(strSpName, strKey, strVal);
            return ds;
        }

    }
}
