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
    public class BusContactRep
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
        private string contactUnit;
        public string ContactUnit
        {
            get { return contactUnit; }
            set { contactUnit = value; }
        }
        private string contacer;
        public string Contacer
        {
            get { return contacer; }
            set { contacer = value; }
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
        private string appName;
        public string AppName
        {
            get { return appName; }
            set { appName = value; }
        }


        public string FileName { get; set; }

        public string HiddFileName { get; set; }
        
    }
}
