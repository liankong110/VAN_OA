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
    public partial class A_PATemplateList : System.Web.UI.Page
    {
        private A_PATemplateService PATemplateSer = new A_PATemplateService();
        private SysUserService UserSer = new SysUserService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Performance/A_PATemplateCmd1.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim() != "")
            {
                List<A_PATemplate> PATemplate = this.PATemplateSer.GetModelList(string.Format(" A_PATemplateName like '%{0}%'", this.txtName.Text.Trim()));
                this.gvList.DataSource = PATemplate;
                this.gvList.DataBind();
            }
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            List<A_PATemplate> PATemplate = this.PATemplateSer.GetModelList(string.Format(" A_PATemplateName like '%{0}%'", this.txtName.Text.Trim()));
            this.gvList.DataSource = PATemplate;
            this.gvList.DataBind();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                DropDownList ddlUserID = e.Row.FindControl("ddlUserID") as DropDownList;
                if (ddlUserID != null)
                {
                    ddlUserID.DataSource = this.UserSer.getAllUserByLoginName(" and loginStatus='在职'");
                    ddlUserID.DataTextField = "LoginName";
                    ddlUserID.DataValueField = "Id";
                    ddlUserID.DataBind();
                }
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
                PATemplateSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
                List<A_PATemplate> PATemplate = this.PATemplateSer.GetModelList("");
                gvList.DataSource = PATemplate;
                gvList.DataBind();         
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/Performance/A_PATemplateCmd1.aspx?PATemplateId=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());
             
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<A_PATemplate> PATemplate = this.PATemplateSer.GetModelList("");
                this.gvList.DataSource = PATemplate;
                this.gvList.DataBind();      
            }
        }

        protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DropDownList ddlUserID = this.gvList.Rows[e.RowIndex].FindControl("ddlUserID") as DropDownList;
            if (ddlUserID.SelectedValue != "")
            {
                this.PATemplateSer.Copy(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()), int.Parse(ddlUserID.SelectedValue.ToString()));
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('复制成功！');</script>");
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请先选择用户！');</script>");
            }
        }

    }
}
