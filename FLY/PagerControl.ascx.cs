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

namespace VAN_OA
{
    public partial class PagerControl : System.Web.UI.UserControl
    {
        public int TotalCount;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindData();
            }
        }

        public void BindData()
        {
            AspNetPager1.RecordCount = TotalCount;
            //GridView1.DataSource = SqlHelper.ExecuteReader(CommandType.StoredProcedure, "P_GetPagedOrders2005",
            //    new SqlParameter("@startIndex", AspNetPager1.StartRecordIndex),
            //    new SqlParameter("@endIndex", AspNetPager1.EndRecordIndex));
            //GridView1.DataBind();
            AspNetPager1.CustomInfoHTML = "Page  <font color=\"red\"><b>" + AspNetPager1.CurrentPageIndex + "</b></font> of  " + AspNetPager1.PageCount;
            AspNetPager1.CustomInfoHTML += "  Orders " + AspNetPager1.StartRecordIndex + "-" + AspNetPager1.EndRecordIndex;
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            BindData();
        }
       
     
    }
}