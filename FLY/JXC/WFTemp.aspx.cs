using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;

namespace VAN_OA.JXC
{
    public partial class WFTemp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["proId"] != null && Request["allE_id"] != null)
                {
                    tb_EFormService eformSer = new tb_EFormService();
                    
                    var id = DBHelp.ExeScalar(string.Format("select top 1 id from tb_EForm where proId={0} and allE_id={1}", Request["proId"], Request["allE_id"])).ToString();
                    Session["backurl"] = "/EFrom/MyEForms.aspx";

                    tb_EForm eform = eformSer.GetModel(Convert.ToInt32(id));

                    string type = eform.ProTyleName.ToString();
                    string url = eformSer.getUrl(Request["proId"], Request["allE_id"], id, type);
                    if (url != "")
                    {
                        Response.Redirect(url);                        
                    }
                }
            }
        }
    }
}
