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
    public partial class A_RoleList : System.Web.UI.Page
    {
        protected List<A_ProInfo> proInfoList;
        private A_RoleService roleSer = new A_RoleService();

        protected string GetProListByRoleId(object roleId)
        {
            string str = "";
            var resultList = proInfoList.FindAll(t=>t.RoleId==(int)roleId);
            foreach (var result in resultList)
            { 
                str+=string.Format(@" <a href='/EFrom/A_ProInfoCmd.aspx?ProId={0}&isScan=true' target='_blank'>{1}</a><br/>",result.pro_Id,result.pro_Type);
                               
            }
            return str;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/EFrom/A_RoleCmd.aspx");
        }

        private void select()
        {
            List<A_Role> roles = this.roleSer.GetModelList(string.Format("  A_RoleName like '%{0}%'", this.txtName.Text.Trim()));

            AspNetPager1.RecordCount = roles.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            string roleIds = "";
            for (int i = gvList.PageIndex * 10; i < (gvList.PageIndex + 1) * 10; i++)
            {
                if (roles.Count - 1 < i)
                {
                    break;
                }
                roleIds += roles[i].A_RoleId + ",";
            }
            roleIds = roleIds.Trim(',');
            A_ProInfoService proSer = new A_ProInfoService();
            proInfoList = proSer.GetListArrayByRoleIds(roleIds);

            
            this.gvList.DataSource = roles;
            this.gvList.DataBind();
            
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            select();

        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            select();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            select();
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
            string sql = string.Format(@"select count(*) from A_ProInfos where a_Role_Id=" + this.gvList.DataKeys[e.RowIndex].Value.ToString());
            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除失败,该信息已被其他信息引用！');</script>");
                return;
            }

            this.roleSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            List<A_Role> roles = this.roleSer.GetModelList(string.Format("  A_RoleName like '%{0}%'", this.txtName.Text.Trim()));
            this.gvList.DataSource = roles;
            this.gvList.DataBind();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/EFrom/A_RoleCmd.aspx?RoleId=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='可编辑删除'
where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='审批角色权限管理') and sys_Object.AutoID is not null", Session["currentUserId"]);
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
