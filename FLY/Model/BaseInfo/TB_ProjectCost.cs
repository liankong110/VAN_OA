using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// 项目财务
    /// </summary>
    public class TB_ProjectCost
    {
        public int Id { get; set; }
        /// <summary>
        ///  项目财务成本账期
        /// </summary>
        public float ZhangQi { get; set; }
        /// <summary>
        /// 项目财务成本测算点
        /// </summary>
        public float CeSuanDian { get; set; }
        /// <summary>
        /// 项目财务成本月利率
        /// </summary>
        public float MonthLiLv { get; set; }
    }
}