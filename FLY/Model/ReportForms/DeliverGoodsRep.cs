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

namespace VAN_OA.Model.ReportForms
{
    public class DeliverGoodsRep
    {
        private string desc;
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }
        private string mouth;

        public string Mouth
        {
            get { return mouth; }
            set { mouth = value; }
        }
        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string invName;
        public string InvName
        {
            get { return invName; }
            set { invName = value; }
        }
        private string goTime;
        public string GoTime
        {
            get { return goTime; }
            set { goTime = value; }
        }
        string backTime;
        public string BackTime
        {
            get { return backTime; }
            set { backTime = value; }
        }
        private string waipairen;
        public string Waipairen
        {
            get { return waipairen; }
            set { waipairen = value; }
        }
        private string souhuoName;
        public string SouhuoName
        {
            get { return souhuoName; }
            set { souhuoName = value; }
        }
    }
}
