using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// CAI_OrderInHouses:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Sell_OrderInHouses:GoodModel
    {
        public Sell_OrderInHouses()
        { }
        /// <summary>
        /// 原始销售价格（用来比较修改过的销售交个）
        /// </summary>
        public decimal OriSellPrice { get; set; }

        public decimal Total { get; set; }
        public decimal  GoodPrice { get; set; }
        #region Model
        private int _ids;
        private int _id;
        private int _gooid;
        private decimal _goodnum;
        private string _goodremark;
        /// <summary>
        /// 
        /// </summary>
        public int Ids
        {
            set { _ids = value; }
            get { return _ids; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GooId
        {
            set { _gooid = value; }
            get { return _gooid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal GoodNum
        {
            set { _goodnum = value; }
            get { return _goodnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodRemark
        {
            set { _goodremark = value; }
            get { return _goodremark; }
        }
        #endregion Model

        public decimal GoodSellPrice { get; set; }
        public decimal GoodSellPriceTotal { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public int HouseID { get; set; }

        public string HouseName { get; set; }

        /// <summary>
        /// 销售出库子单ID
        /// </summary>
        public int SellOutOrderId { get; set; }

        /// <summary>
        /// 成本再次确认价格
        /// </summary>
        public decimal GoodPriceSecond { get; set; }

        /// <summary>
        /// 损失差额
        /// </summary>
        public decimal GoodTotalCha { get; set; }

        public string Status { get; set; }

        public DateTime RuTime { get; set; }
        public string GoodAreaNumber { get; set; }
    }
}
