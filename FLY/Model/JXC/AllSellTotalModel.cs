﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace VAN_OA.Model.JXC
{
    public class AllSellTotalModel
    {


        /// <summary>
        ///  净利润率=项目净利/销售额 ，如果销售额=0 ，净利润率=0 
        /// </summary>
        public decimal JingLi
        {
            get
            {

                if (PoTotal == 0)
                {
                    return 0;
                }
                return PoLiRunTotal / PoTotal;
            }
        }


        public decimal PV { get; set; }
        public decimal SPI { get; set; }
        public decimal SumPOTotal { get; set; }
        public decimal SumPOTotal_QZ { get; set; }
        public decimal SumPOTotal_ZZ { get; set; }
        public decimal SumPOTotal_QXZ { get; set; }
        public decimal SumPOTotal_ZXZ { get; set; }

        public string AE { get; set; }

        public decimal PoTotal { get; set; }

        public decimal PoLiRunTotal { get; set; }

        public decimal MaoLi_QZ { get; set; }

        public decimal TureliRun_QZ { get; set; }

        public decimal SellTotal_QZ { get; set; }

        public decimal sellFPTotal_QZ { get; set; }

        public decimal KouTotal_QZ { get; set; }





        public decimal MaoLi_ZZ { get; set; }

        public decimal TureliRun_ZZ { get; set; }

        public decimal SellTotal_ZZ { get; set; }

        public decimal sellFPTotal_ZZ { get; set; }

        public decimal KouTotal_ZZ { get; set; }




        public decimal MaoLi_QXZ { get; set; }

        public decimal TureliRun_QXZ { get; set; }

        public decimal SellTotal_QXZ { get; set; }

        public decimal sellFPTotal_QXZ { get; set; }

        public decimal KouTotal_QXZ { get; set; }




        public decimal MaoLi_ZXZ { get; set; }

        public decimal TureliRun_ZXZ { get; set; }

        public decimal SellTotal_ZXZ { get; set; }

        public decimal sellFPTotal_ZXZ { get; set; }

        public decimal KouTotal_ZXZ { get; set; }
		 

	}
}
