using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.Fin
{
    [Serializable]
    public partial class FIN_CommCost
    {
        public FIN_CommCost()
        { }
        #region Model
        private int _id;
        private string _prono = "";
        private int _costtypeId ;
        private decimal _total;
        private string _caiyear;
        private int _compcode;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProNo
        {
            set { _prono = value; }
            get { return _prono; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CostTypeId
        {
            set { _costtypeId = value; }
            get { return _costtypeId; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Total
        {
            set { _total = value; }
            get { return _total; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CaiYear
        {
            set { _caiyear = value; }
            get { return _caiyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CompId
        {
            set { _compcode = value; }
            get { return _compcode; }
        }
        #endregion Model

    }
}