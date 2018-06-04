using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace VAN_OA
{
    public class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 判断项目编码信息
        /// </summary>
        /// <returns></returns>
        public bool CheckPoNO(string PONO)
        {
            PONO = PONO.Trim();           
            if (PONO.ToLower().Contains("kc"))
            {
                if (PONO.Length > 11)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('KC项目编码长度为12位，请重新填写！');</script>"));
                    return false;
                }
                if (PONO != "")
                {
                    string head = PONO.Substring(0, 2);
                    if (head.ToLower() == "kc")
                    {

                        if (PONO.Length > 2 && CommHelp.VerifesToNum(PONO.Substring(2)) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('项目编号的格式为KC+4位年份(大于2012年)+5位数字，请重新填写！');</script>"));
                            return false;
                        }
                    }
                    else
                    {
                        if (CommHelp.VerifesToNum(PONO.Substring(0)) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('项目编号的格式为KC+4位年份(大于2012年)+5位数字，请重新填写！');</script>"));
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (PONO.Length > 10)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('项目编码长度为10位，请重新填写！');</script>"));
                    return false;
                }
                if (PONO != "")
                {
                    string head = PONO.Substring(0, 1);
                    if (head.ToLower() == "p")
                    {

                        if (PONO.Length > 1 && CommHelp.VerifesToNum(PONO.Substring(1)) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('项目编号的格式为P+4位年份(大于2012年)+5位数字，请重新填写！');</script>"));
                            return false;
                        }
                    }
                    else
                    {
                        if (CommHelp.VerifesToNum(PONO.Substring(0)) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('项目编号的格式为P+4位年份(大于2012年)+5位数字，请重新填写！');</script>"));
                            return false;
                        }
                    }
                }
            }     
            return true;
        }
        /// <summary>
        /// 判断单据号信息
        /// </summary>
        /// <returns></returns>
        public bool CheckProNo(string ProNo)
        {
            ProNo = ProNo.Trim();
            if (string.IsNullOrEmpty(ProNo))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('单据号不能为空！');</script>"));
                return false;
            }
            if (ProNo.Length > 8)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('单据号长度为8位，请重新填写！');</script>"));
                return false;
            }
           
            if ( CommHelp.VerifesToNum(ProNo) == false )
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('单据号的格式为4位年份+4位数字，请重新填写！');</script>"));
                return false;
            } 
            return true;
        }
        protected override void OnInit(EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.AbsolutePath;
            if (Session["currentUserId"] != null)
            {
                string sql = string.Format(@"DECLARE @FROMID INT; SET  @FROMID=0;
SELECT @FROMID=FORMID FROM SYS_FORM  WHERE ASSEMBLYPATH='{0}'
IF(@FROMID<>0)
BEGIN
SELECT COUNT(*) AS COU FROM ROLE_SYS_FORM WHERE ROLE_ID IN ( SELECT ROLEID FROM ROLE_USER WHERE USERID={1})
AND SYS_FORM_ID=@FROMID
END
ELSE
SELECT -1 AS COU
", url.Substring(1), Session["currentUserId"]);
                if ((int)DBHelp.ExeScalar(sql) == 0)
                {
                    base.Response.Redirect("~/WFNull.aspx");
                }
            }
            //base.OnLoad(e);
        }



        /// <summary>
        /// 在权限中  有没有 存在没有打钩的权限
        /// </summary>
        /// <param name="displayName">窗体名称</param>
        /// <param name="textName">权限名称</param>        
        /// <returns></returns>
        protected bool QuanXian(string displayName, string textName)
        {
            string sql = string.Format(@"--存在没有打钩的权限  
IF EXISTS(select sys_Object.AutoID from role_sys_form 
left join sys_Object
on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='{2}'
where  role_Id in (select roleId from Role_User where userId={0}) 
and sys_form_Id in(select formID from sys_form where displayName='{1}') 
and sys_Object.AutoID is null)
BEGIN
SELECT 1   --有
END
ELSE
BEGIN 
SELECT 0   --没有
END",Session["currentUserId"],displayName,textName);  
            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 在权限中  有没有 存在没有打钩的权限
        /// 已经重新修改
        /// </summary>
        /// <param name="displayName">窗体名称</param>            
        /// <returns></returns>
        protected bool QuanXian_ShowAll(string displayName)
        {
            //return QuanXian(displayName, "查看所有");

            return NewShowAll_textName(displayName, "查看所有");
        }

        /// <summary>
        /// 是否是AE or 特殊用户
        /// </summary>
        /// <returns></returns>
        protected bool IsSpecialUser { get { return (bool)Session["IsSpecialUser"]; } }

        /// <summary>
        /// 对于'不能编辑' 来说 如果返回为false 说明 不能编辑  反正 可以
        /// 对于'查看所有' 来说 如果返回为false 说明 不能查看所有  反正 能查看所有
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="displayName"></param>
        /// <param name="textName"></param>
        /// <returns></returns>
        protected bool NewShowAll_Name(string displayName, string objName)
        {
            string sql = string.Format(@"declare @result int;
set @result=0;
select @result=COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and Name='{2}'
where  role_Id in (select roleId from Role_User where userId={0}) 
and sys_form_Id in(select formID from sys_form where displayName='{1}')
and sys_Object.AutoID is not null;
select @result=COUNT(*)-@result from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and Name='{2}'
where  role_Id in (select roleId from Role_User where userId={0}) 
and sys_form_Id in(select formID from sys_form where displayName='{1}')
select @result;
", Session["currentUserId"], displayName, objName);

            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) == 0)
            {
                return false;
            }
            return true;

        }
        /// <summary>
        /// 对于'不能编辑' 来说 如果返回为false 说明 不能编辑  反正 可以
        /// 对于'查看所有' 来说 如果返回为false 说明 不能查看所有  反正 能查看所有
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <param name="displayName"></param>
        /// <param name="textName"></param>
        /// <returns></returns>
        protected bool NewShowAll_textName(string displayName, string textName)
        {
            string sql = "";
            if (textName != "不能编辑" && textName != "禁止含税设置")
            {
                sql = string.Format(@"declare @result int;
set @result=0;
select @result=COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='{2}'
where  role_Id in (select roleId from Role_User where userId={0}) 
and sys_form_Id in(select formID from sys_form where displayName='{1}')
and sys_Object.AutoID is not null;
select @result=COUNT(*)-@result from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='{2}'
where  role_Id in (select roleId from Role_User where userId={0}) 
and sys_form_Id in(select formID from sys_form where displayName='{1}')
select @result;
", Session["currentUserId"], displayName, textName);
            }
            else
            {
                sql = string.Format(@"declare @result int;
set @result=0;
select @result=COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='{2}'
where  role_Id in (select roleId from Role_User where userId={0}) 
and sys_form_Id in(select formID from sys_form where displayName='{1}')
and sys_Object.AutoID is not null;
select @result;
", Session["currentUserId"], displayName, textName);
            }
            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) == 0)
            {
                return false;
            }
            return true;
        }
       
    }
}
