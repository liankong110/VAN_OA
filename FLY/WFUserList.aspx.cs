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
using System.Collections.Generic;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA
{
    public partial class WFUserList : BasePage
    {
        private SysUserService userSer = new SysUserService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/WFUserCmd.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        private  void Show()
        {
            string sql = "select * ,user1.loginName AS ReportToName,tb_User.CompanyCode from tb_User left join tb_user as user1 on tb_User.reportTo=user1.id where 1=1 ";
            if (txtName.Text != "")
            {
                sql += string.Format(" and tb_User.loginName like '%{0}%'", txtName.Text);
            }

            if (txtLoginId.Text != "")
            {
                sql += string.Format(" and tb_User.LoginId like '%{0}%'", txtLoginId.Text);
            }

            if (txtLoginUserNO.Text != "")
            {
                sql += string.Format(" and tb_User.LoginUserNO like '%{0}%'", txtLoginUserNO.Text);
            }
            if(ddlCompany.Text!="全部")
            {  
                sql += string.Format(" and tb_User.CompanyCode ='{0}'", ddlCompany.Text);                
            }
            if (ddlDepartment.Text != "全部")
            {
                sql += string.Format(" and tb_User.loginIPosition ='{0}'", ddlDepartment.Text);
            }
            if (ddlState.Text != "全部")
            {
                sql += string.Format(" and tb_User.LoginStatus ='{0}'", ddlState.Text);
            }
            if (ddlSepcList.Text != "-1")
            {
                sql += string.Format(" and tb_User.IsSpecialUser ={0}", ddlSepcList.Text);
            }

            List<User> users = this.userSer.getUserBySQL1(sql + " order by tb_User.LoginUserNO ");
            AspNetPager1.RecordCount = users.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvList.DataSource = users;
            this.gvList.DataBind();
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            gvList.PageIndex = e.NewPageIndex;
            string sql = "select * ,user1.loginName AS ReportToName,tb_User.CompanyCode from tb_User left join tb_user as user1 on tb_User.reportTo=user1.id";
            if (txtName.Text != "")
            {
                sql += string.Format(" where tb_User.loginName like '%{0}%'",txtName.Text);
            }
            List<User> users = this.userSer.getUserBySQL1(sql);
            this.gvList.DataSource = users;
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
            if (this.gvList.DataKeys[e.RowIndex].Value.ToString() != "1")
            {
                this.userSer.deleteUserByUserId(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
                DBHelp.ExeCommand(string.Format("DELETE from A_Role_User where A_RoleId IN (28,32) AND User_Id ={0};", this.gvList.DataKeys[e.RowIndex].Value.ToString()));
                Show();
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('管理员不能被删除！');</script>");
            }
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            if (this.gvList.DataKeys[e.NewEditIndex].Value.ToString() != "1")
            {
                base.Response.Redirect("~/WFUserCmd.aspx?UserId=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('管理员不能被编辑！');</script>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<User> users = new List<User>();
                this.gvList.DataSource = users;
                this.gvList.DataBind();

                TB_CompanyService comSer = new TB_CompanyService();

                var comList = comSer.GetListArray("");
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComCode = "全部", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();


                var dt= DBHelp.getDataTable("select loginIPosition from tb_User group by loginIPosition ");
                ddlDepartment.Items.Add(new ListItem ("全部"));
                foreach (DataRow dr in dt.Rows)
                { 
                    ddlDepartment.Items.Add(new ListItem (dr[0].ToString()));
                }

            }
        }

        protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
