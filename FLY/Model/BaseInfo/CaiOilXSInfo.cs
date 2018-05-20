using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{

    /// <summary>
    /// CaiOilXSInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CaiOilXSInfo
    {
        public CaiOilXSInfo()
        { }
        #region Model
        private int _id;
        private string _carno;
        private decimal _oilxs;
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
        public string CarNo
        {
            set { _carno = value; }
            get { return _carno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OilXs
        {
            set { _oilxs = value; }
            get { return _oilxs; }
        }
        #endregion Model

    }
}
