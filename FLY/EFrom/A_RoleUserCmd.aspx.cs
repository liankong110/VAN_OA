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
using System.Collections.Generic;


namespace VAN_OA.EFrom
{
    public partial class A_RoleUserCmd : System.Web.UI.Page
    {
        private A_Role_UserService roleUserSer = new A_Role_UserService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            A_Role_User roleUser = null;
            roleUser = FormCheck();
            if (roleUser != null)
            {
                try
                {

                    if (cbAll.Checked == true)
                    {
                        string sql = string.Format(@"
insert into A_Role_User 
select {0},ID,0,1 from tb_User where id not in (select  User_Id from A_Role_User where A_RoleId={0})", ddlRoles.SelectedItem.Value);
                        DBHelp.ExeCommand(sql);
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");

                    }
                    else
                    {

                        if (this.roleUserSer.Add(roleUser) > 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");

                        }
                        else
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加失败！');</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/EFrom/A_RoleUserList.aspx");
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            A_Role_User roleUser = null;
            roleUser = FormCheck();
            if (roleUser != null)
            {
                try
                {

                    roleUserSer.Update(roleUser);
                    //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");
                    base.Response.Redirect("~/EFrom/A_RoleUserList.aspx?RoleId=" + roleUser.A_RoleId);

                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        public A_Role_User FormCheck()
        {

            if (this.ddlRoles.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择角色名称！');</script>");
                this.ddlRoles.Focus();
                return null;
            }
            A_Role_User roleuser = new A_Role_User();
            if (cbAll.Checked == false)
            {
                if (ddlUser.SelectedItem == null)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择用户名！');</script>");
                    this.ddlUser.Focus();
                    return null;
                }





                if (base.Request["ID"] != null)
                {
                    roleuser.ID = Convert.ToInt32(base.Request["ID"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from A_Role_User_View where loginName='{0}' AND A_RoleName='{1}'and id<>{2}",
                      ddlUser.SelectedItem.Text, ddlRoles.SelectedItem.Text, base.Request["ID"]))) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('审批角色{0},用户名称{1}已经存在,请重新填写！');</script>", ddlRoles.SelectedItem.Text, ddlUser.SelectedItem.Text));

                        this.ddlUser.Focus();
                        return null;
                    }
                }
                else
                {

                    if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from A_Role_User_View where loginName='{0}' AND A_RoleName='{1}'",
                     ddlUser.SelectedItem.Text, ddlRoles.SelectedItem.Text))) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('审批角色{0},用户名称{1}已经存在,请重新填写！');</script>", ddlRoles.SelectedItem.Text, ddlUser.SelectedItem.Text));
                        this.ddlUser.Focus();
                        return null;
                    }
                }
            }


            roleuser.A_RoleId = Convert.ToInt32(ddlRoles.SelectedValue);
            roleuser.User_Id = Convert.ToInt32(ddlUser.SelectedItem.Value);

            roleuser.Role_User_Index = Convert.ToInt32(txtRole_User_Index.Text);
            roleuser.RowState = Convert.ToInt32(ddlRowState.Text);
            return roleuser;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                List<VAN_OA.Model.User> user = userSer.getAllUserByLoginName("");
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName_ID";
                ddlUser.DataValueField = "Id";



                A_RoleService roleSer = new A_RoleService();
                List<A_Role> roles = roleSer.GetModelList("");

                this.ddlRoles.DataSource = roles;
                this.ddlRoles.DataBind();
                ddlRoles.DataTextField = "A_RoleName";
                ddlRoles.DataValueField = "A_RoleId";
                if (base.Request["ID"] != null)
                {
                    this.btnAdd.Visible = false;

                    A_Role_User roleUser = roleUserSer.GetModelGetModel(Convert.ToInt32(base.Request["ID"]));
                    foreach (ListItem item in ddlRoles.Items)
                    {
                        if (item.Text == roleUser.RoleName)
                        {
                            item.Selected = true;
                            break;
                        }

                    }
                    //ddlRoles.SelectedItem.Text = roleUser.RoleName;

                    ddlUser.Text = roleUser.UserId.ToString();

                    cbAll.Visible = false;
                    txtRole_User_Index.Text = roleUser.Role_User_Index.ToString();
                    ddlRowState.Text = roleUser.RowState.ToString();
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }

        protected void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAll.Checked)
            {
                ddlUser.Enabled = false;
            }
            else
            {
                ddlUser.Enabled = true;
            }
        }
    }
}
