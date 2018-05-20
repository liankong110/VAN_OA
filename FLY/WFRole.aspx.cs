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
using VAN_OA.Dal;
using System.Collections.Generic;
using VAN_OA.Model;
namespace VAN_OA
{
    public partial class WFRole : BasePage
    {
        private RoleService roleSer = new RoleService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/WFRoleAdd.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }
        private void Show()
        {
             
                List<Role> roles = this.roleSer.getAllRoles(string.Format(" and RoleName like '%{0}%'", this.txtName.Text.Trim()));
                AspNetPager1.RecordCount = roles.Count;
                this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

                this.gvList.DataSource = roles;
                this.gvList.DataBind();
            
        }
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            List<Role> roles = this.roleSer.getAllRoles(string.Format(" and RoleName like '%{0}%'", this.txtName.Text.Trim()));
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
            if (this.gvList.DataKeys[e.RowIndex].Value.ToString() != "1")
            {
                this.roleSer.deleteRoleByRoleId(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
                List<Role> roles = this.roleSer.getAllRoles(string.Format(" and RoleName like '%{0}%'", this.txtName.Text.Trim()));
                this.gvList.DataSource = roles;
                this.gvList.DataBind();
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('管理员不能被删除！');</script>");
            }
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.gvList.DataKeys[e.NewEditIndex].Value.ToString() != "1")
            {
                base.Response.Redirect("~/WFRoleAdd.aspx?RoleId=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('管理员不能被编辑！');</script>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<Role> roles = new List<Role> ();
                this.gvList.DataSource = roles;
                this.gvList.DataBind();
            }
        }
    }
}
