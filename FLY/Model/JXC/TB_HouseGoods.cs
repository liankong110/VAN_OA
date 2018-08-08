using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{

    /// <summary>
    /// TB_HouseGoods:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_HouseGoods : GoodModel
    {
        public TB_HouseGoods()
        { }
        #region Model
        private int _id;
        private int _houseid;
        private int _goodid;
        private decimal _goodavgprice;
        private decimal _goodnum;
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
        public int HouseId
        {
            set { _houseid = value; }
            get { return _houseid; }
        }

        public string HouseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int GoodId
        {
            set { _goodid = value; }
            get { return _goodid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal GoodAvgPrice
        {
            set { _goodavgprice = value; }
            get { return _goodavgprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal GoodNum
        {
            set { _goodnum = value; }
            get { return _goodnum; }
        }
        #endregion Model


        public decimal Total { get; set; }

        /// <summary>
        /// 需要出库数量
        /// </summary>
        public decimal LastNum { get; set; }

        /// <summary>
        /// 正在执行的数量
        /// </summary>
        public decimal DoingNum { get; set; }


        /// <summary>
        /// 采购需出
        /// </summary>
        public decimal CaiNum { get; set; }

        /// <summary>
        /// 原项目商品数量
        /// </summary>
        public decimal PONums { get; set; }

        /// <summary>
        /// 原项目商品价格
        /// </summary>
        public decimal POGoodPrice { get; set; }

        /// <summary>
        /// 仓位
        /// </summary>
        public string GoodAreaNumber { get; set; }


        #region   这里有个重要的问题是 商品期初如果没有，后来有入库，需要显示在内， 商品期初如果有库存，后来全出完了，也要显示在内，只有一种情况不用显示：期初为0,入库为0，出库为0，期末为0 ，这种情况才不用显示！！！！
        /// <summary>
        /// 期初数量为 盘点起始日期时的库存数量
        /// </summary>
        public decimal Nums { get; set; }
        /// <summary>
        /// 本期入库为 这段时间仓库里的该商品的入库数
        /// </summary>
        public decimal InNums { get; set; }
        /// <summary>
        /// 本期出库为这段时间仓库里的该商品的出库数
        /// </summary>
        public decimal OutNums { get; set; }
        #endregion


        public int No { get; set; }

        public decimal HadInvoice { get; set; }
        public decimal NoInvoice { get; set; }

        /// <summary>
        /// 采库需出数
        /// </summary>
        public decimal SumKuXuCai { get; set; }
        /// <summary>
        /// 滞留库存=库存数量-采库需出数
        /// </summary>
        public decimal ZhiLiuKuCun
        {
            get
            {
                return GoodNum - SumKuXuCai;
            }
        }

    }
}
