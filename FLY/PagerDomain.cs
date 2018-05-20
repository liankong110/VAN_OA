using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA
{
    public class PagerDomain
    {
        public PagerDomain()
        {
            CurrentPageIndex = 1;
            PageSize = 10;
            MaxPagerCount = 10;
        }
        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 每页数据条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页码（从1开始）
        /// </summary>
        public int CurrentPageIndex { get; set; }
        /// <summary>
        /// 显示出来最多的页码数量，因为假设有100页，不可能把100页都显示到界面上
        /// </summary>
        public int MaxPagerCount { get; set; }
    }
}
