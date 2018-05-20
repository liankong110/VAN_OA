using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal;
using VAN_OA.Model;
 

namespace VAN_OA
{
    public partial class WFTAdminDelete : BasePage
    {
        private TB_AdminDeleteService userSer = new TB_AdminDeleteService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/AdminDeleteCmd.aspx");
        }

      

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

         
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
                this.userSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
                List<TB_AdminDelete> users = this.userSer.GetListArray("");
                this.gvList.DataSource = users;
                this.gvList.DataBind();
           
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<TB_AdminDelete> users = this.userSer.GetListArray("");
                this.gvList.DataSource = users;
                this.gvList.DataBind();
            }
        }

        protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
