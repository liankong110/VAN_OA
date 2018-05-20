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
using VAN_OA.Model;
using VAN_OA.Bll.OA;
namespace VAN_OA
{
    public partial class WFRoleAdd : System.Web.UI.Page
    {
        private RoleService roleSer = new RoleService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    Role role = new Role();
                    role.RoleName = this.txtRoleName.Text.Trim();
                    role.RoleCode = this.txtRoleCode.Text.Trim();
                    if (this.roleSer.addRole(role) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                        this.txtRoleCode.Text = "";
                        this.txtRoleName.Text = "";
                        this.txtRoleCode.Focus();
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加失败！');</script>");
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
            base.Response.Redirect("~/WFRole.aspx");
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            this.txtRoleCode.Text = "";
            this.txtRoleName.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    Role role = new Role();
                    role.RoleName = this.txtRoleName.Text.Trim();
                    role.RoleCode = this.txtRoleCode.Text.Trim();
                    role.RID = Convert.ToInt32(base.Request["RoleId"]);
                    if (this.roleSer.modityRole(role))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改失败！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        public bool FormCheck()
        {
            if (this.txtRoleCode.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写角色编码！');</script>");
                this.txtRoleCode.Focus();
                return false;
            }
            if (this.txtRoleName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写角色名称！');</script>");
                this.txtRoleName.Focus();
                return false;
            }
            if (base.Request["RoleId"] != null)
            {
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_Role where RoleCode='{0}' and RID<>{1}", this.txtRoleCode.Text.Trim(), base.Request["RoleId"]))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('角色编码已经存在,请重新填写！');</script>");
                    this.txtRoleCode.Focus();
                    return false;
                }
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_Role where RoleName='{0}' and RID<>{1}", this.txtRoleName.Text.Trim(), base.Request["RoleId"]))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('角色名称已经存在,请重新填写！');</script>");
                    this.txtRoleName.Focus();
                    return false;
                }
            }
            else
            {
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_Role where RoleCode='{0}'", this.txtRoleCode.Text.Trim()))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('角色编码已经存在,请重新填写！');</script>");
                    this.txtRoleCode.Focus();
                    return false;
                }
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_Role where RoleName='{0}'", this.txtRoleName.Text.Trim()))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('角色名称已经存在,请重新填写！');</script>");
                    this.txtRoleName.Focus();
                    return false;
                }
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (base.Request["RoleId"] != null)
                {
                    this.btnAdd.Visible = false;
                    Role ROLE = this.roleSer.getRoleById(Convert.ToInt32(base.Request["RoleId"]));
                    this.txtRoleCode.Text = ROLE.RoleCode;
                    this.txtRoleName.Text = ROLE.RoleName;
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }
    }
}
