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
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;

namespace VAN_OA.ReportForms
{
    public partial class LeverDispatching : System.Web.UI.Page
    {
        tb_LeverInfoService leverSer = new tb_LeverInfoService();
        tb_DispatchingService DispathchingSer = new tb_DispatchingService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string sql = " 1=1 ";
                sql += string.Format(" and (dateForm>='{0} 00:00:00' or '{0} 00:00:00' between dateForm and dateTo)", DateTime.Now.ToShortDateString());
                sql += string.Format(" and ('{0} 23:59:59' >=dateTo or '{0} 23:59:59' between dateForm and dateTo)", DateTime.Now.AddDays(2).ToShortDateString());
                sql += " and id in (select allE_Id from tb_EForm where proid=4 and state<>'不通过')";

                gvList.DataSource = leverSer.GetListArray_1(sql);
                gvList.DataBind();

                string sql1 = " 1=1 ";
                sql1 += " and tb_Dispatching.id in (select allE_Id from tb_EForm where proid=1 and state<>'不通过')";
                sql1 += string.Format(" and disDate between  '{0} 00:00:00' and '{1} 23:59:59'", DateTime.Now.ToShortDateString(), DateTime.Now.AddDays(2).ToShortDateString());


                gvDispatching.DataSource = DispathchingSer.GetListTwoDays(sql1);
                gvDispatching.DataBind();
                
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void gvDispathing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }
    }
}
