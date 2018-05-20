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
    public partial class A_ProInfoList : System.Web.UI.Page
    {

        private A_ProInfoService proSer = new A_ProInfoService();
     

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();            
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }
        private void Show()
        {
            string sql = " 1=1 ";
            if (txtName.Text.Trim() != "")
            {
                sql += string.Format(" and pro_Type like '%{0}%'", txtName.Text);
            }
            if (DropDownList1.Text != "")
            {
                if (DropDownList1.Text == "是")
                {
                    sql += string.Format(" and cou>0");
                }
                else
                {
                    sql += string.Format(" and (cou<=0 or cou is null)");
                }
            }

            List<A_ProInfo> pros = this.proSer.GetListArray(sql);
            AspNetPager1.RecordCount = pros.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvList.DataSource = pros;
            this.gvList.DataBind();
        }
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            string sql = " 1=1 ";
            if (txtName.Text.Trim() != "")
            {
                sql += string.Format(" and pro_Type like '%{0}%'", txtName.Text);
            }
            if (DropDownList1.Text != "")
            {
                if (DropDownList1.Text == "是")
                {
                    sql += string.Format(" and cou>0");
                }
                else
                {
                    sql += string.Format(" and (cou<=0 or cou is null)");
                }
            }

            List<A_ProInfo> pros = this.proSer.GetListArray(sql);
            this.gvList.DataSource = pros;
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

         

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/EFrom/A_ProInfoCmd.aspx?ProId=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                 string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='可编辑'
where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='审批流程设置') and sys_Object.AutoID is not null", Session["currentUserId"]);
                 if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                 {
                     gvList.Columns[0].Visible = false;
                 }
                 

            }
        }
    }
}
