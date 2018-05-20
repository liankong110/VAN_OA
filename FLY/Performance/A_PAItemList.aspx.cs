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
using System.Collections.Generic;
using VAN_OA.Model.Performance;

namespace VAN_OA.Performance
{
    public partial class A_PAItemList : System.Web.UI.Page
    {
        private A_PAItemService PAItemSer = new A_PAItemService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Performance/A_PAItemCmd.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim() != "")
            {
                List<A_PAItem> PAItem = this.PAItemSer.GetModelList(string.Format(" and A_PAItemName like '%{0}%'", this.txtName.Text.Trim()));
                this.gvList.DataSource = PAItem;
                this.gvList.DataBind();
            }
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            List<A_PAItem> roles = this.PAItemSer.GetModelList(string.Format(" and A_PAItemName like '%{0}%'", this.txtName.Text.Trim()));
            this.gvList.DataSource = roles;
            this.gvList.DataBind();
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
            PAItemSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
                List<A_PAItem> PAItem = this.PAItemSer.GetModelList("");
                this.gvList.DataSource = PAItem;
                this.gvList.DataBind();   
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            base.Response.Redirect("~/Performance/A_PAItemCmd.aspx?PAItemId=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());   
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<A_PAItem> PAItem = this.PAItemSer.GetModelList("");
                this.gvList.DataSource = PAItem;
                this.gvList.DataBind();      
            }
        }
    }
}
