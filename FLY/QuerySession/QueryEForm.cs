using System;
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

namespace VAN_OA.QuerySession
{
    /// <summary>
    /// 表单列表
    /// </summary>
    public class QueryEForm
    {
        /// <summary>
        /// 审批类型
        /// </summary>
        public int ProTypeId { get; set; }

        /// <summary>
        /// 开始申请时间
        /// </summary>
        public string FromTime { get; set; }

        /// <summary>
        /// 结束申请时间
        /// </summary>
        public string ToTime { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string Apper { get; set; }

        /// <summary>
        /// 委托人
        /// </summary>
        public string WeiTuo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string Auper { get; set; }

        /// <summary>
        /// 等待审批人
        /// </summary>
        public string WatingAuper { get; set; }


        /// <summary>
        /// 我要审批的类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 列表页数
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        public string E_No { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string PONO { get; set; }

        public string GuestName { get; set; }


        public string SPForm { get; set; }

        public string SPTo { get; set; }
    }
}
