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
    public class InvoiceReport
    {
        public string All_InvoiceNo { get; set; }

        public string All_GuestName { get; set; }

        public decimal? All_InvoiceTotal { get; set; }


        public string OA_InvoiceNo { get; set; }

        public string OA_GuestName { get; set; }

        public decimal? OA_InvoiceTotal { get; set; }

        public string OA_InvoiceDate { get; set; }

        public string OA_PONO { get; set; }

        public int OA_FPId { get; set; }

        public string OA_AE { get; set; }

        public string Kingdee_InvoiceNo { get; set; }

        public string Kingdee_GuestName { get; set; }

        public decimal? Kingdee_InvoiceTotal { get; set; }

        public string Kingdee_InvoiceDate { get; set; }

        public int Kingdee_Id { get; set; }
    }
}
