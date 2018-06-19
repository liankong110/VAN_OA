using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    public class Pro_JSXDetailInfo
    {
        public string TypeName { get; set; }
        public DateTime RuTime { get; set; }
        public string allRemark { get; set; }
        public decimal GoodInNum { get; set; }
        public decimal GoodInTotal
        {
            get
            {
                return GoodInNum * Price;
            }
        }

        public decimal GoodOutNum { get; set; }
        public decimal GoodOutTotal
        {
            get
            {
                return GoodOutNum * TempHousePrice;
            }
        }
        public decimal GoodResultNum { get; set; }
        public string PONO { get; set; }
        public decimal Price { get; set; }
        public string ProNo { get; set; }

        public int GooId { get; set; }

        public int T { get; set; }

        public decimal? HadInvoice { get; set; }
        public decimal? NoInvoice { get; set; }

        public int RuId { get; set; }

        public decimal KuCunTotal { get; set; }

        public decimal TempHousePrice { get; set; }
    }
}
