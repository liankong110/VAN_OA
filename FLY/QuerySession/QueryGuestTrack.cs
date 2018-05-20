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
    public class QueryGuestTrack
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string FromTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string ToTime { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string GuestName { get; set; }

        /// <summary>
        /// 制单人
        /// </summary>
        public string Apper { get; set; }
    }
}
