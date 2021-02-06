using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    [Serializable]
    public class BI_Month
    {
        public string AE { get; set; }

        public int Month { get; set; }

        public decimal SumPOTotal { get; set; }

        public int Year { get; set; }
    }

    [Serializable]
    public class BI_Bar
    { 
        public string name { get; set; }
        public string type { get; set; }
        public object data { get; set; } 

    }

    [Serializable]
    public class BI_Pie
    {
        public string name { get; set; }
        public decimal value { get; set; } 
    }

}