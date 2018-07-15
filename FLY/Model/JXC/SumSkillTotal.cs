using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    public class SumSkillTotal
    {
        public string Name { get; set; }
        /// <summary>
        /// 派工总小时
        /// </summary>
        public decimal Hours { get; set; }
        /// <summary>
        /// 工程反馈得分
        /// </summary>
        public decimal Gong_Value { get; set; }
        /// <summary>
        /// 零售反馈得分
        /// </summary>
        public decimal Ling_Value { get; set; }
        /// <summary>
        /// 系统反馈得分
        /// </summary>
        public decimal Xi_Value { get; set; }
        /// <summary>
        /// 反馈均得分
        /// </summary>

        public decimal AvgValue { get; set; }
        /// <summary>
        /// 反馈总得分
        /// </summary>

        public decimal SumValue { get; set; }


        /// <summary>
        /// 工程综合得分
        /// </summary>
        public decimal Gong_Score { get; set; }

        /// <summary>
        /// 零售综合得分
        /// </summary>
        public decimal Ling_Score { get; set; }
        /// <summary>
        /// 系统综合得分
        /// </summary>
        public decimal Xi_Score { get; set; }

        /// <summary>
        /// 工程综合占比
        /// </summary>
        public decimal Gong_Score_Per { get; set; }

        /// <summary>
        /// 零售综合占比
        /// </summary>
        public decimal Ling_Score_Per { get; set; }
        /// <summary>
        /// 系统综合占比
        /// </summary>
        public decimal Xi_Score_Per { get; set; }
        /// <summary>
        /// 综合均分
        /// </summary>
        public decimal AvgScore { get; set; }
        /// <summary>
        /// 综合总得分
        /// </summary>
        public decimal SumScore { get; set; }

        /// <summary>
        /// 公司综合总得分
        /// </summary>
        public decimal CompanyScore { get; set; }
        /// <summary>
        /// 公司综合总得分
        /// </summary>
        public decimal CompanyScore_Per { get; set; }
        /// <summary>
        /// 项目总金额
        /// </summary>
        public decimal PoTotal { get; set; }
        /// <summary>
        /// 总到款率
        /// </summary>
        public decimal DaoKuan_Per { get; set; }
    }

    public class SumSkillTotal_Detail
    {
        /// <summary>
        /// 类型 工程，零售，系统
        /// </summary>
        public string MyPoType { get; set; }
        /// <summary>
        /// 派工人员
        /// </summary>
        public string loginName { get; set; }
        /// <summary>
        /// 总工时
        /// </summary>
        public decimal SumHours { get; set; }
        /// <summary>
        /// 反馈总得分
        /// </summary>
        public decimal SumFK { get; set; }
        /// <summary>
        /// 反馈均得分
        /// </summary>
        public decimal AvgFK { get; set; }
        /// <summary>
        /// 综合总得分
        /// </summary>
        public decimal SumScore { get; set; }
        /// <summary>
        /// 综合均分
        /// </summary>
        public decimal AvgScore { get; set; }
        /// <summary>
        /// 项目金额
        /// </summary>
        public decimal SumPOTotal { get; set; }
        /// <summary>
        /// 到款金额
        /// </summary>
        public decimal InvoTotal { get; set; }

    }
}