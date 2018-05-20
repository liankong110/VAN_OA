using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.EFrom
{
    /// <summary>
    /// tb_QuotePrice_Invs:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tb_QuotePrice_Invs
    {
        public tb_QuotePrice_Invs()
        { }

        private decimal total;
        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        private bool ifUpdate;
        public bool IfUpdate
        {
            get { return ifUpdate; }
            set { ifUpdate = value; }
        }

        #region Model
        private int _id;
        private string _invname;
        private decimal _invnum;
        private decimal _invprice;
        private int _quoteid;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 品名
        /// </summary>
        public string InvName
        {
            set { _invname = value; }
            get { return _invname; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal InvNum
        {
            set { _invnum = value; }
            get { return _invnum; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal InvPrice
        {
            set { _invprice = value; }
            get { return _invprice; }
        }
        /// <summary>
        /// 主单Id
        /// </summary>
        public int QuoteId
        {
            set { _quoteid = value; }
            get { return _quoteid; }
        }

        public int InvGoodId { get; set; }
        #endregion Model

    }
}
