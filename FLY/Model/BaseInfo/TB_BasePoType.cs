using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    [Serializable]
    public class TB_BasePoType
    {
         public int Id { get; set; }

         public string BasePoType { get; set; }

         public decimal Reward { get; set; }

         public int Year { get; set; }
    }
}