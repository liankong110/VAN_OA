using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    public class Sell_OrderOutHouseView : Sell_OrderOutHouse
    {
        /// <summary>
        /// 签回类型
        /// </summary>
        public int BackType { get; set; }         

        /// <summary>
        /// 签回时间
        /// </summary>
        public DateTime? BackTime { get; set; }


        /// <summary>
        /// 签回时间
        /// </summary>
        private int backId ;
        public int BackId
        {
          get { return backId; }
          set { backId = value; }
        }

        /// <summary>
        /// 签回状态
        /// </summary>
        public string BackProNo { get; set; }
         
    }
}
