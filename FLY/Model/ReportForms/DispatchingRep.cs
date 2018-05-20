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
    public class DispatchingRep
    {
        private string _outdispaterName;
        public string OutdispaterName
        {
            get { return _outdispaterName; }
            set { _outdispaterName = value; }
        }

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
        public string GuestAddress
        {
            get { return address; }
            set { address = value; }
        }
        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; }
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
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string outDispater;
        public string OutDispater
        {
            get { return outDispater; }
            set { outDispater = value; }
        }

        public string FileName { get; set; }

        public string HiddFileName { get; set; }

        public string SuiTongRen { get; set; }
    }
}
