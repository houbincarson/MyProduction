using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using System.ComponentModel;

namespace ProduceManager
{
    public class UkyndaGcEdit
    {
        public string StrNoEnableCtrIds
        {
            get;
            set;
        }
        public string StrNoEnableEditCtrIds
        {
            get;
            set;
        }
        public Control CtrFirstAddFocusContr
        {
            get;
            set;
        }
        public Control CtrFirstEditFocusContr
        {
            get;
            set;
        }
        public string[] StrFileds
        {
            get;
            set;
        }
        public bool BlSetDefault
        {
            get;
            set;
        }
        public List<string> ArrContrSeq
        {
            get;
            set;
        }
        public Control CtrParentControl
        {
            get;
            set;
        }
        public string GridViewName
        {
            get;
            set;
        }
        public string BtnEnterSaveBtnId
        {
            get;
            set;
        }
        public string SaveMoveToCtrId
        {
            get;
            set;
        }
        public GridView GridViewEdit
        {
            get;
            set;
        }
        public Component BtnEnterSave
        {
            get;
            set;
        }
        public string StrMode
        {
            get;
            set;
        }

        public UkyndaGcEdit()
        {
            ArrContrSeq = new List<string>();
        }
    }
}
