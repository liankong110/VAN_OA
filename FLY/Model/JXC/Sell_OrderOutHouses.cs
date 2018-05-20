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
    public partial class Sell_OrderOutHouses:GoodModel
    {
        public Sell_OrderOutHouses()
        { }
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

        public decimal PONum { get; set; }
        public decimal POGoodTotal { get; set; }
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
        /// 销售原始价格
        /// </summary>
        public decimal GoodOriSellPrice { get; set; }

        /// <summary>
        /// 需出库数量
        /// </summary>
        public decimal LastSellNum { get; set; }

        /// <summary>
        /// 采购需出库数量
        /// </summary>
        public decimal LastCaiNum { get; set; }
        public string GoodAreaNumber { get; set; }
    }
}
