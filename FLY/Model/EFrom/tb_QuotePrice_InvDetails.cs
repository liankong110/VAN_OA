using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.EFrom
{
    /// <summary>
    /// tb_QuotePrice_InvDetails:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tb_QuotePrice_InvDetails
    {
        public tb_QuotePrice_InvDetails()
        { }

        /// <summary>
        /// 产地
        /// </summary>
        public string Product { get; set; }

        private decimal total;
        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }
        private int no;

        public int No
        {
            get { return no; }
            set { no = value; }
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
        private string _invmodel;
        private string _invunit;
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
        /// 名称
        /// </summary>
        public string InvName
        {
            set { _invname = value; }
            get { return _invname; }
        }
        /// <summary>
        /// 型号
        /// </summary>
        public string InvModel
        {
            set { _invmodel = value; }
            get { return _invmodel; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string InvUnit
        {
            set { _invunit = value; }
            get { return _invunit; }
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
        #endregion Model

        public string GoodBrand { get; set; }

        public int InvGoodId { get; set; }

        public int UpdateIndex { get; set; }

        public string GoodNo { get; set; }

        public string GoodTypeSmName { get; set; }
        /// <summary>
        /// 概要
        /// </summary>
        public string InvRemark { get;set;}
    }
}
