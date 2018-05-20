using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    public class Sell_Cai_OrderInHouseListModel : GoodModel
    {
        /// <summary>
        /// 原始销售价格（用来比较修改过的销售交个）
        /// </summary>
        public decimal OriSellPrice { get; set; }
        public decimal Total { get; set; }
        public decimal GoodPrice { get; set; }
        #region Model
      
        private int _gooid;
        private decimal _goodnum;
        public DateTime RuTime { get; set; }
     
        /// <summary>
        /// 
        /// </summary>
        public int GooId
        {
            set { _gooid = value; }
            get { return _gooid; }
        }
        /// <summary>
        /// 销售退货数量
        /// </summary>
        public decimal GoodNum
        {
            set { _goodnum = value; }
            get { return _goodnum; }
        }
       
        #endregion Model

        public decimal GoodSellPrice { get; set; }
        public decimal GoodSellPriceTotal { get; set; }

        /// <summary>
        /// 成本再次确认价格
        /// </summary>
        public decimal GoodPriceSecond { get; set; }

        /// <summary>
        /// 损失差额
        /// </summary>
        public decimal GoodTotalCha { get; set; }

        public string PONo { get; set; }
        public string POName { get; set; }
        public string GuestName { get; set; }
        /// <summary>
        /// 采购退货数量
        /// </summary>
        public decimal? CaiGoodNum { get; set; }
        public DateTime? CAIRuTime { get; set; }
        public decimal? CAIGoodPrice { get; set; }
        public decimal? CAIGoodPriceTotal { get; set; }
        public string CreateName { get; set; }
        /// <summary>
        /// 项目数量 
        /// </summary>
        public decimal? PONums { get; set; }
        /// <summary>
        /// 这里的数量都是审批单通过的 数量
        /// 需补采退数=（销售退货合计-采购退货合计）×2-（项目中该商品数量合计-采购退货合计）
        /// </summary>
        public decimal NeedNums { get { return ((GoodNum - (CaiGoodNum ?? 0)) * 2 - (PONums - (CaiGoodNum ?? 0))) ?? 0; } }

        /// <summary>
        /// 库存
        /// </summary>
        public decimal HouseGoodNum { get; set; }

        public decimal CaiNums { get; set; }

        public System.Drawing.Color MyColor { get; set; }

        /// <summary>
        /// 销售退货状态是否正常 默认0
        /// </summary>
        public bool IsNormal { get; set; }

        /// <summary>
        /// 仓位
        /// </summary>
        public string GoodAreaNumber { get; set; } 

    }
}