﻿using System;
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
using System.Collections.Generic;
using VAN_OA.Model.ReportForms;
using System.Data.SqlClient;

namespace VAN_OA
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.txtPwd.Text.Trim() == "") || (this.txtUserName.Text.Trim() == ""))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请将登录信息填写完整！');</script>");
                }
                else
                {
                    User _User = new User();
                    _User.LoginPwd = this.txtPwd.Text.Trim();
                    _User.LoginId = this.txtUserName.Text.Trim();
                    User u = new SysUserService().checkExist(_User);
                    if (u == null)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('用户账号或密码不存在！');</script>");
                    }
                    else if (u.LoginPwd != MD5Util.Encrypt(this.txtPwd.Text))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('用户账号或密码不存在！');</script>");
                    }
                    else
                    {
                        if (u != null)
                        {
                            Session["userInfo"] = u;
                            this.Session["currentUserId"] = u.Id;
                            this.Session["LoginName"] = u.LoginName;
                            this.Session["zhiwu"] = u.Zhiwu;
                            this.Session["CompanyCode"] = u.CompanyCode;
                            Session["IsSpecialUser"] = u.IsSpecialUser;
                            List<RoleUser> role_user = new RoleUserService().getRoleIdByUserId(u.Id);
                            if (u.LoginStatus == "离职")
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('用户已经禁用！');</script>");
                                return;
                            }
                        }
                        base.Response.Redirect("~/Main.htm");
                    }
                }
            }
            catch (Exception ex)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
            }
        }
        private int GetZhouQi(int month)
        {
            if (1 <= month && month <= 3)
            {
                return 1;
            }
            else if (4 <= month && month <= 6)
            {
                return 2;
            }
            else if (7 <= month && month <= 9)
            {
                return 3;
            }
            else if (10 <= month && month <= 12)
            {
                return 4;
            }
            return 0;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();

            


            string para = "R730 E5-2680*2/64G/128G固态+3'5  4T*6/RAID5/导轨/3年7×24";
            var a=HttpUtility.UrlEncode(para);
            var b = HttpUtility.UrlDecodeToBytes(para);
            var c = HttpUtility.UrlEncode(para);

            var d=Uri.EscapeDataString(para);
            var f = Uri.EscapeUriString(para);

        }
    }
}
