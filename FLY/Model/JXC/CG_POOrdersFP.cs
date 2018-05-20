using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    public class CG_POOrdersFP : CG_POOrders
    {
        public string PONo { get; set; }
        public string POName { get; set; }
        public decimal POTotal { get; set; }
        public decimal fpTotal { get; set; }
        public decimal WEITotals { get; set; }

         

    }
}
