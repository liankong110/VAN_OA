using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal;
using VAN_OA.Model;



namespace VAN_OA.MyExcels
{
    public partial class MainExcelList : BasePage
    {

        MyExcelService myExcelSer = new MyExcelService();

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/MyExcels/AddExcel.aspx");
        }

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
            string sql = "";
            if (txtName.Text != "")
            {
                sql += string.Format("  Name like '%{0}%' OR SheetName LIKE '%{0}%'", txtName.Text);
            }

            List<TB_EXCEL> excelList = this.myExcelSer.QueryMainExcel(sql);
            AspNetPager1.RecordCount = excelList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvList.DataSource = excelList;
            this.gvList.DataBind();
        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            Show();
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
            this.myExcelSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            Show();
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除成功！');</script>");
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/BaseInfo/WFBasePoType.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<TB_EXCEL> excelList = new List<TB_EXCEL>();
                this.gvList.DataSource = excelList;
                this.gvList.DataBind();

                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='删除'
where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='Excel管理') and sys_Object.AutoID is not null", Session["currentUserId"]);
                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                {
                    gvList.Columns[0].Visible = true;
                }
                else
                {
                    gvList.Columns[0].Visible = false;
                }

                 sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='添加'
where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='Excel管理') and sys_Object.AutoID is not null", Session["currentUserId"]);
                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                {

                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                }
            }
        }
    }
}
