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

namespace VAN_OA.Model.OA
{
    public class Directorys
    {

        private string direName;
        public string DireName
        {
            get { return direName; }
            set { direName = value; }
        }
        private string direUrl;
        public string DireUrl
        {
            get { return direUrl; }
            set { direUrl = value; }
        }
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int parentID;
        public int ParentID
        {
            get { return parentID; }
            set { parentID = value; }
        }

    }
}
