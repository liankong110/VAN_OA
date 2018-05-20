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
using VAN_OA.Model.EFrom;

namespace VAN_OA.QuerySession
{
    public class QueryUseCarDetail:CommModel
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
        /// 司机
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string Apper { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNo { get; set; }
    }
}
