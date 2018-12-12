using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.Fin
{
    public class AEPromiseTotal
    {

        public int NO { get; set; }
        public int Id { get; set; }

        public int YearNo { get; set; }

        public string AE { get; set; }
        public string Department { get; set; }

        public string Company { get; set; }
        public decimal PromiseSellTotal { get; set; }
        public decimal PromiseProfit { get; set; }
        public decimal AddGuetSellTotal { get; set; }
        public decimal AddGuestProfit { get; set; }


        public decimal ActualPromiseSellTotal { get; set; }
        public decimal ActualPromiseProfit { get; set; }
        public decimal ActualAddGuetSellTotal { get; set; }
        public decimal ActualAddGuestProfit { get; set; }


        public decimal BaiPromiseSellTotal
        {
            get
            {
                if (PromiseSellTotal != 0)
                {
                    return ActualPromiseSellTotal / PromiseSellTotal;
                }
                return 0;
            }
        }

        public decimal BaiPromiseProfit
        {
            get
            {
                if (PromiseProfit != 0)
                {
                    return ActualPromiseProfit / PromiseProfit;
                }
                return 0;
            }
        }

    }
}