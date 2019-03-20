using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    [Serializable]
    public class PVReport
    {

        public int No { get; set; }
        /// <summary>
        /// 项目指标
        /// </summary>
        public string TargetName { get; set; }
        /// <summary>
        /// 开工日
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 今天日期
        /// </summary>
        public string CurrentDate { get; set; }
        /// <summary>
        /// 已开工天数
        /// </summary>
        public int HadDays { get; set; }

        /// <summary>
        /// 截止实际完工日开工天数
        /// 完整定义： 当项目未完工时 
        /// 出库金额《项目金额，截止实际完工日开工天数=今天日期-开工日 ；当项目已完工时（出库金额=项目金额 且不等于0），截止实际完工日开工天数=实际完工日-开工日；
        /// 当项目未关闭 同时 出库金额=项目金额 并且 等于0时，截止实际完工日开工天数=今天日期-开工日；
        /// 当项目关闭 同时 出库金额=项目金额 并且 等于0时，截止实际完工日开工天数=（项目首次建立日期中的年份的结算日）-开工日（见图1）。
        /// </summary>
        public int ActualHadDays { get; set; }

        /// <summary>
        /// 数值
        /// </summary>
        public decimal Values { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 调整措施
        /// </summary>
        public string CuoShi { get; set; }
        /// <summary>
        /// 计划完工天数
        /// </summary>
        public int PlanDays { get; set; }

        /// <summary>
        /// 计划完工日
        /// </summary>
        public string PlanDate { get; set; }
    }
}