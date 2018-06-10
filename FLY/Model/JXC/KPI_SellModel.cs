using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    public class KPI_SellModel
    {
        /// <summary>
        /// AE
        /// </summary>
        public string AE { get; set; }
        /// <summary>
        /// 日期段显示项目时间输入框的起始日期--结束日期
        /// </summary>
        public string DataString { get; set; }
        /// <summary>
        /// 超期XX项目数，的条件是按如图一，XX是按考核项目类别打钩的类型，（如零售打钩和系统打钩，XX 就显示零售系统，超期零售系统项目数是统计 项目类别为零售和系统的项目，然后结合屏幕上其他条件）查询出来的项目总数 ，

        /// 由于我们考核账期的需要，项目日期其实是需要往前推4个月的，也就是这个超期查询的实际项目起始日期和结束日期 是按界面上填写的时间分别往前推4个月
        /// </summary>
        public int ProjectCount { get; set; }
        /// <summary>
        /// 销售新客户拜访数字
        /// </summary>
        public int NewContractCount { get; set; }
        /// <summary>
        /// 老客户走访次数
        /// </summary>
        public int OldContractCount { get; set; }
        /// <summary>
        /// 项目总金额
        /// </summary>
        public decimal POTotal { get; set; }
        /// <summary>
        /// 销售总金额
        /// </summary>
        public decimal SellTotal { get; set; }
        /// <summary>
        /// 到账总金额
        /// </summary>
        public decimal InvoiceTotal { get; set; }
        /// <summary>
        /// 项目总利润
        /// </summary>
        public decimal ProfitTotal { get; set; }
        /// <summary>
        /// 实际总利润
        /// </summary>
        public decimal LastProfitTotal { get; set; }
        /// <summary>
        /// 开票率=项目的发票额合计/项目金额合计
        /// </summary>
        public decimal KP_Percent { get; set; }
        /// <summary>
        ///  到款率=项目的账内到款合计/项目金额合计
        /// </summary>
        public decimal DK_Percent { get; set; }
    }
}