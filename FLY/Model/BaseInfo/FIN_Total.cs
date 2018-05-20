using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// FIN_Total:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class FIN_Total
    {
        public FIN_Total()
        { }
        #region Model
        private int _id;
        private string _prono = "";
        private string _caiyear;
        private decimal _total;
        private string _compcode;
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
        public string CaiYear
        {
            set { _caiyear = value; }
            get { return _caiyear; }
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
        public string CompCode
        {
            set { _compcode = value; }
            get { return _compcode; }
        }
        #endregion Model

    }
}