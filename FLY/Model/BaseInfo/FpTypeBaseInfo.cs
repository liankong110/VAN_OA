using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// FpTypeBaseInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class FpTypeBaseInfo
    {
        public FpTypeBaseInfo()
        { }
        #region Model
        private int _id;
        private string _fptype;
        private decimal _tax;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 发票类型
        /// </summary>
        public string FpType
        {
            set { _fptype = value; }
            get { return _fptype; }
        }
        /// <summary>
        /// 税率
        /// </summary>
        public decimal Tax
        {
            set { _tax = value; }
            get { return _tax; }
        }

        /// <summary>
        /// 发票长度
        /// </summary>
        public int FpLength { get; set; }

        #endregion Model

    }
}
