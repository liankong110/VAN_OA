using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    public class JobInfo
    {
        public int id { get; set; }
        /// <summary>
        /// 1 客户季度同步
        /// </summary>
        public int JobType { get; set; }
        /// <summary>
        /// 执行的时间
        /// </summary>
        public DateTime JobTime { get; set; }
    }
}