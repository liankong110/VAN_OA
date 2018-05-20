using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using Microsoft.Win32;
using VAN_OA.Model.ReportForms;


namespace VAN_OA.JXC
{
    public partial class WFCai_OrderPrint : System.Web.UI.Page
    {          
        protected decimal total = 0;
        protected string totalDa = "";
        protected int allRows = 0;
        protected int pageSize = 20;

        protected List<vAllCaiOrderList> modelList = new List<vAllCaiOrderList>();

        protected void btnClose_Click(object sender, EventArgs e)
        {


        }
        private void printnull()
        {
            RegistryKey pregkey;
            pregkey = Registry.CurrentUser.OpenSubKey("Software//Microsoft//Internet Explorer//PageSetup//", true);          
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            printnull();//隐藏ie打印的页眉和页脚
            if (!base.IsPostBack)
            {
                //请假单子
                if (Session["print"] != null)
                {                     
                    modelList = new CAI_POOrderService().GetListArrayAll(Session["print"].ToString());
                    foreach (var model in modelList)
                    {
                        if (model.BusType == "0")
                        {
                            model.BusType = "项采";
                        }
                        else if (model.BusType == "1")
                        {
                            model.BusType = "库采";
                        }
                    }
                    Session["print"] = null;
                }

            }
        }


     

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
