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
namespace VAN_OA
{
    public partial class UpdatePwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (txtOrgPwd.Text.Trim() == "")
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), null, "<script>alert('密码不能为空');</script>");
                txtNewPwd.Focus();
                return;
                
            }
            if (txtNewPwd.Text.Trim() == "")
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), null, "<script>alert('新密码不能为空');</script>");
                txtNewPwd.Focus();
                return;
            }
            if (txtNewPwd.Text != txtReNewPwd.Text)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), null, "<script>alert('新密码和确认密码两个输入不一致');</script>");
                txtNewPwd.Focus();
                return;
            }


           User _User = new User();
           _User.LoginPwd = this.txtOrgPwd.Text.Trim();
           _User.LoginName = Convert.ToString(Session["LoginName"]);


            User u = new SysUserService().checkExist_1(_User);
            if (u == null)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('密码不存在！');</script>");
                return;
            }
            else if (u.LoginPwd !=MD5Util.Encrypt(this.txtOrgPwd.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('密码不存在！');</script>");
                return;
            }
            else
            { 
                
            }
            string sql = string.Format("update tb_User set loginPwd='{0}' where loginName='{1}'",MD5Util.Encrypt( txtNewPwd.Text), Session["LoginName"].ToString());
            DBHelp.ExeCommand(sql);
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");

        }
    }
}
