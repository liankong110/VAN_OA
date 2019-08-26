using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VAN_OA
{
    public partial class WebAPI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["GuestName"]))
            {
                string sql = string.Format("select Model FROM CG_POOrder WHERE GuestName='{0}' and Model<>'' and status='通过' group by Model", Request["GuestName"]);

                Response.Write(string.Join(",", DBHelp.getDataTable(sql).Select().Select(t => t[0].ToString()).ToArray()));
                Response.End();
                return;
            }

        }
    }
}