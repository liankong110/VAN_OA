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
 
namespace VAN_OA.EFrom
{
    public partial class A_RoleCmd : System.Web.UI.Page
    {
        private A_RoleService roleSer = new A_RoleService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {

                    A_Role role = new A_Role();
                    role.A_RoleName = this.txtRoleName.Text.Trim();
                    role.A_IFEdit = this.CbIfEdit.Checked;
                    if (this.roleSer.Add(role) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                       
                        this.txtRoleName.Text = "";
                        CbIfEdit.Checked = false;
                        this.txtRoleName.Focus();
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
            base.Response.Redirect("~/EFrom/A_RoleList.aspx");
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            this.txtRoleName.Text = "";
            CbIfEdit.Checked = false;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    A_Role role = new A_Role();
                    role.A_RoleName = this.txtRoleName.Text.Trim();
                    role.A_IFEdit = this.CbIfEdit.Checked;
                    role.A_RoleId = Convert.ToInt32(base.Request["RoleId"]);
                    roleSer.Update(role);
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");
                    
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        public bool FormCheck()
        {
          
            if (this.txtRoleName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写角色名称！');</script>");
                this.txtRoleName.Focus();
                return false;
            }
            if (base.Request["RoleId"] != null)
            {

                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from A_Role where A_RoleName='{0}' and A_RoleId<>{1}", this.txtRoleName.Text.Trim(), base.Request["RoleId"]))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('审批角色名称已经存在,请重新填写！');</script>");
                    this.txtRoleName.Focus();
                    return false;
                }
            }
            else
            {

                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from A_Role where A_RoleName='{0}'", this.txtRoleName.Text.Trim()))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('审批角色名称已经存在,请重新填写！');</script>");
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
                    A_Role ROLE = this.roleSer.GetModelList("A_RoleId="+base.Request["RoleId"])[0];
                  
                    this.txtRoleName.Text = ROLE.A_RoleName;
                    CbIfEdit.Checked=ROLE.A_IFEdit;
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }
    }
}
