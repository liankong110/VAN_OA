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
    public partial class ApprovePAFormList : System.Web.UI.Page
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

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/Performance/ApprovePAFormDetail.aspx?PAFormId=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.gvList.DataSource = ApprovePAFormListSer.GetMyApprovePAFormList(Session["currentUserId"].ToString(),"",DateTime.Today.AddMonths(-1).ToString("yyyy-MM"));
                this.gvList.DataBind();
                ddlUser.DataSource = UserSer.getAllUserByLoginName(null);
                ddlUser.DataTextField = "loginName";
                ddlUser.DataValueField = "ID";
                ddlUser.DataBind();
                for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year  + 5; i++)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(),i.ToString()));
                }
            }
        }

        protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DropDownList ddlUserID = this.gvList.Rows[e.RowIndex].FindControl("ddlUserID") as DropDownList;
            if (ddlUserID.SelectedValue != "")
            {
                string sql = string.Format(@"select count(*) from tb_UserPAForm where UserID=" + ddlUserID.SelectedValue);
                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('复制失败,该用户已有绩效考核模版！');</script>");
                    return;
                }
                //this.PAUserTemplateSer.Copy(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()), int.Parse(ddlUserID.SelectedValue.ToString()));
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('复制成功！');</script>");
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请先选择用户！');</script>");
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (ddlYear.SelectedValue == "" || ddlMonth.SelectedValue == "" || Chk_All.Checked)
            {
                this.gvList.DataSource = ApprovePAFormListSer.GetMyApprovePAFormList(Session["currentUserId"].ToString(),ddlUser.SelectedValue, "");
            }
            else
            {
                this.gvList.DataSource = ApprovePAFormListSer.GetMyApprovePAFormList(Session["currentUserId"].ToString(),ddlUser.SelectedValue, ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue);
            }
            this.gvList.DataBind();
        }
    }
}
