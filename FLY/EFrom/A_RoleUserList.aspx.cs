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
using System.Collections.Generic;
using VAN_OA.Model.EFrom;

namespace VAN_OA.EFrom
{
    public partial class A_RoleUserList : System.Web.UI.Page
    {

        private A_Role_UserService roleUserSer = new A_Role_UserService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/EFrom/A_RoleUserCmd.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;

            Show();
        }

        private void Show()
        {

            string sql = " 1=1 ";
            if (txtName.Text.Trim() != "")
            {
                sql += string.Format(" and loginName like '%{0}%'", txtName.Text);
            }

            if (ddlRoles.SelectedItem.Text.Trim() != "")
            {
                sql += string.Format(" and A_RoleName='{0}'", ddlRoles.SelectedItem.Text.Trim());
            }
            if (ddlRowState.Text != "-1")
            {
                sql += string.Format(" and RowState={0} ", ddlRowState.Text);
            }

            List<A_Role_User> roles = this.roleUserSer.GetListArray(sql,0);
            AspNetPager1.RecordCount = roles.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = roles;
            this.gvList.DataBind();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            string sql = " 1=1 ";
            if (txtName.Text.Trim() != "")
            {
                sql += string.Format(" and loginName like '%{0}%'", txtName.Text);
            }

            if (ddlRoles.SelectedItem.Text.Trim() != "")
            {
                sql += string.Format(" and A_RoleName='{0}'", ddlRoles.SelectedItem.Text.Trim());
            }

            List<A_Role_User> roles = this.roleUserSer.GetListArray(sql);
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

            //string sql = string.Format(@"select count(*) from A_ProInfos where a_Role_Id=" + this.gvList.DataKeys[e.RowIndex].Value.ToString());
            //if (Convert.ToInt32(DBHelp.ExeScalar(sql))>0)
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除失败,该信息已被其他信息引用！');</script>");
            //    return;
            //}

            this.roleUserSer.DeleteGetModel(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
             string   sql = " 1=1 ";
            if (txtName.Text.Trim() != "")
            {
                sql += string.Format(" and loginName like '%{0}%'",txtName.Text);
            }

            if (ddlRoles.SelectedItem.Text.Trim() != "")
            {
                sql += string.Format(" and A_RoleName='{0}'", ddlRoles.SelectedItem.Text.Trim());
            }

            List<A_Role_User> roles = this.roleUserSer.GetListArray(sql);
            this.gvList.DataSource = roles;
            this.gvList.DataBind();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/EFrom/A_RoleUserCmd.aspx?ID=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {

                A_RoleService roleSer = new A_RoleService();
                List<A_Role> roles = roleSer.GetModelList("");
                roles.Insert(0, new A_Role());
                this.ddlRoles.DataSource = roles;
                this.ddlRoles.DataBind();
                ddlRoles.DataTextField = "A_RoleName";
                ddlRoles.DataValueField = "A_RoleId";

                if (Request["RoleId"] != null)
                {
                    ddlRoles.Text = Request["RoleId"];
                    Show();
                }

                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='可编辑删除'
where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='审批角色组信息') and sys_Object.AutoID is not null", Session["currentUserId"]);
                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)               
                {
                    gvList.Columns[0].Visible = false;
                    gvList.Columns[1].Visible = false;
                    btnAdd.Visible = false;
                }
            }
        }
    }
}
