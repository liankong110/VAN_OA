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

namespace VAN_OA.Model.JXC
{
    public class View_AllEform
    {
        public int myProId { get; set; }

        public int Id { get; set; }

        public string PONo { get; set; }
        public bool IsSpecial { get; set; }

        public string AE { get; set; }
    }
}
