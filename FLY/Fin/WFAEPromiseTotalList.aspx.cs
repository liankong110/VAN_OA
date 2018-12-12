using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.Fin;
using VAN_OA.Model;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Model.Fin;

namespace VAN_OA.Fin
{
    public partial class WFAEPromiseTotalList : BasePage
    {

        AEPromiseTotalService aEPromiseTotalService = new AEPromiseTotalService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/WFAEPromiseTotal.aspx");
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
            string sql = "1=1";
            string promiseSql = "1=1";

            if (ddlCompany.Text != "-1" && ddlCompany.Text != "" && ddlCompany.Text != "全部")
            {
                sql += string.Format("  and CompanyCode='{0}'", ddlCompany.Text.Trim());
            }

            if (ddlUser.Text != "-1" && ddlUser.Text != "" && ddlUser.Text != "全部")
            {
                sql += string.Format("  and tb.AE='{0}'", ddlUser.Text.Trim());
                promiseSql += string.Format("  and AE='{0}'", ddlUser.Text.Trim());
            }

            promiseSql += string.Format("  and YearNo={0} ", ddlYear.Text.Trim());
            List<AEPromiseTotal> aeProList = this.aEPromiseTotalService.GetListArrayReport(sql, ddlYear.Text.Trim(), promiseSql);
            AspNetPager1.RecordCount = aeProList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvList.DataSource = aeProList;
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
                var model = e.Row.DataItem as AEPromiseTotal;
                if (model.Id == 0)
                {
                    (e.Row.FindControl("btnEdit") as ImageButton).Visible = false;
                    (e.Row.FindControl("btnDel") as ImageButton).Visible = false;
                }
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //判断项目有没有使用

            this.aEPromiseTotalService.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));

            Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            base.Response.Redirect("~/Fin/WFAEPromiseTotal.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
               
                List<WFAEPromiseTotal> _list = new List<WFAEPromiseTotal>();
                this.gvList.DataSource = _list;
                this.gvList.DataBind();

                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");

                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComCode = "-1", ComName = "全部" });

                for (int i = 2012; i <= DateTime.Now.Year; i++)
                {
                    ddlYear.Items.Add(new ListItem { Value = i.ToString(), Text = i.ToString() });
                }
                ddlYear.SelectedValue = (DateTime.Now.Year).ToString();

                if (NewShowAll_textName("销售指标", "编辑")==false)
                {
                    gvList.Columns[1].Visible = false;
                    btnAdd.Visible = false;
                }
                if (NewShowAll_textName("销售指标", "删除") == false)
                {
                    gvList.Columns[2].Visible = false; 
                }
                bool showAll = true;

                if (NewShowAll_textName("销售指标", "查看所有"))
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                  
                }

                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll("销售指标", Session["currentUserId"], "WFScanDepartList") == true)
                {
                    ViewState["WFScanDepartList"] = false;
                    WFScanDepartList = false;
                }
                List<VAN_OA.Model.User> user = new List<User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                if (showAll == true)
                {
                    user = userSer.getAllUserByPOList();
                    user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                }
                else if (WFScanDepartList == true)
                {
                    user = userSer.getAllUserByLoginName(string.Format(" and loginIPosition in (select loginIPosition from tb_User where id={0}) and loginIPosition<>''", Session["currentUserId"]));
                    user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = 0 });
                }
                else
                {
                    var model = Session["userInfo"] as User;
                    user.Insert(0, model);
                    ddlCompany.Enabled = false;
                }

                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "LoginName";

                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();
            }
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string where = "";
            List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
            if (ddlCompany.Text != "-1")
            {                
                where = string.Format(" and tb_User.CompanyCode ='{0}'", ddlCompany.Text);
                if (ViewState["WFScanDepartList"] != null && Convert.ToBoolean(ViewState["WFScanDepartList"]) == true)
                {
                    where+=string.Format(" and loginIPosition in (select loginIPosition from tb_User where id={0}) and loginIPosition<>''", Session["currentUserId"]);                   
                }
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                user = userSer.getAllUserByPOList(where);
                user.Insert(0, new VAN_OA.Model.User() { LoginName = "", Id = -1 });
            }
            ddlUser.DataSource = user;
            ddlUser.DataBind();
            ddlUser.DataTextField = "LoginName";
            ddlUser.DataValueField = "LoginName";
        }
    }
}
