using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    public class CAI_POCaiView : GoodModel
    {
        public string loginName { get; set; }

        public string CaiGou { get; set; }
        private string _pono;
        private string _bustype;
        /// <summary>
        /// 
        /// </summary>
        public string PONo
        {
            set { _pono = value; }
            get { return _pono; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BusType
        {
            set { _bustype = value; }
            get { return _bustype; }
        }
        

       
        /// <summary>
        /// 客户ID
        /// </summary>
        public string GuestNo { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string GuestName { get; set; }
        /// <summary>
        /// AE
        /// </summary>
        public string AE { get; set; }
        /// <summary>
        /// INSIDE
        /// </summary>
        public string INSIDE { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string POName { get; set; }
        /// <summary>
        /// 项目日期
        /// </summary>
        public DateTime PODate { get; set; }

        public string POPayStype { get; set; }


        public decimal POTotal { get; set; }

        public decimal Num { get; set; }
        public decimal totalOrderNum { get; set; }

        public decimal ResultNum { get; set; }

        public decimal Price { get; set; }

        public decimal Total { get; set; } 

        public int POCaiId { get; set; }

        public string ProNo { get; set; }

        public string Supplier { get; set; }


        public int GoodId { get; set; }

        public decimal LastTruePrice { get; set; }
        public string GoodAreaNumber { get; set; }

    }
}
