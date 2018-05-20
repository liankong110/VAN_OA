using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VAN_OA.Fin
{
    public partial class WFAll : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
//                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='公共费用'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目结算') and sys_Object.AutoID is not null", Session["currentUserId"]);
//                if (Convert.ToInt32(DBHelp.ExeScalar(sql))> 0)
                if (NewShowAll_textName("项目结算", "公共费用") == false)
                {
                    btnCommon.Visible = false;
                }
//                sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='个性费用'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目结算') and sys_Object.AutoID is not null", Session["currentUserId"]);
//                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                if (NewShowAll_textName("项目结算", "个性费用") == false)
                {
                    btnSpec.Visible = false;
                }
//                sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='销售结算明细表'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目结算') and sys_Object.AutoID is not null", Session["currentUserId"]);
//                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                if (NewShowAll_textName("项目结算", "销售结算明细表") == false)
                {
                    Button1.Visible = false;
                }
//                sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='自动选中'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目结算') and sys_Object.AutoID is not null", Session["currentUserId"]);
//                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) >0)
                if (NewShowAll_textName("项目结算", "自动选中") == false)
                {
                    btnSelected.Visible = false;
                }
            }
        }

        protected void btnCommon_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/CommCostList.aspx");
        }

        protected void btnSpec_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/SpecCostList.aspx");
        }

        protected void btnSellInfo_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/WFAllSellDetail.aspx");
        }

        protected void btnSellInfo1_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/NEW_WFAllSellDetail.aspx");
        }

        protected void btnSelected_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/SetPONoIsSelected.aspx");
        }

        protected void btnJieSuan_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/SetPONoIsSelected.aspx");

        }
    }
}