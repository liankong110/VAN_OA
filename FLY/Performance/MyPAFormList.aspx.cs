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
using VAN_OA.Dal.Performance;
using VAN_OA.Model.Performance;
using VAN_OA.Dal;
using VAN_OA.Model;
using System.Collections.Generic;

namespace VAN_OA.Performance
{
    public partial class MyPAFormList : System.Web.UI.Page
    {
        private ApprovePAFormListService ApprovePAFormListSer = new ApprovePAFormListService();
        private A_PATemplateService PATemplateSer = new A_PATemplateService();
        private SysUserService UserSer = new SysUserService();
        
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.gvList.DataSource = ApprovePAFormListSer.GetMyPAFormList(Session["currentUserId"].ToString());
                this.gvList.DataBind();
                ddlYear.Items.Add(new ListItem("--选择--"));
                for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year  + 5; i++)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(),i.ToString()));
                }
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.gvList.DataSource = ApprovePAFormListSer.GetMyPAFormList(Session["currentUserId"].ToString(),ddlYear.SelectedValue+"-"+ddlMonth.SelectedValue );
            this.gvList.DataBind();
        }
    }
}
