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

namespace VAN_OA.Model.KingdeeInvoice
{
    [Serializable]
    public class AccountReport
    {
        public string All_InvoiceNo { get; set; }

        public string All_PONO { get; set; }

        public string All_OAGuestName { get; set; }

        /// <summary>
        /// 金蝶到款金额
        /// </summary>
        public decimal? All_AccountTotal { get; set; }

        /// <summary>
        /// OA到款金额
        /// </summary>
        public decimal? All_OATotal { get; set; }

        /// <summary>
        /// 发票金额
        /// </summary>
        public decimal? All_InvoiceTotal { get; set; }

        /// <summary>
        /// OA到款金额
        /// </summary>
        public decimal? All_WeiOATotal { get { return All_InvoiceTotal - All_OATotal; } }

        /// <summary>
        /// 发票金额
        /// </summary>
        public decimal All_WeiInvoiceTotal { get { return (All_InvoiceTotal??0) - (All_AccountTotal??0); } }


        public string Record { get; set; }

        public DateTime? MaxDate { get; set; }


        public string OA_InvoiceNo { get; set; }

        public string OA_GuestName { get; set; }

        public decimal? OA_InvoiceTotal { get; set; }

        public string OA_InvoiceDate { get; set; }

        public string OA_PONO { get; set; }

        public decimal? OA_AccountTotal { get; set; }

        public int OA_FPId { get; set; }

        public string All_OAAE { get; set; }

        public string Kingdee_InvoiceNo { get; set; }

        public string Kingdee_GuestName { get; set; }

        public decimal? Kingdee_InvoiceTotal { get; set; }

        public string Kingdee_InvoiceDate { get; set; }

        public decimal? Kingdee_AccountTotal { get; set; }

        public int Kingdee_Id { get; set; }
    }
}
