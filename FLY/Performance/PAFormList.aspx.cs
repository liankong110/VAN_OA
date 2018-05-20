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
using VAN_OA.Dal.Performance;
using VAN_OA.Model.Performance;
using VAN_OA.Dal;
using VAN_OA.Model;
using System.Collections.Generic;

namespace VAN_OA.Performance
{
    public partial class PAFormList : System.Web.UI.Page
    {
        private ApprovePAFormListService ApprovePAFormListSer = new ApprovePAFormListService();
        private A_PATemplateService PATemplateSer = new A_PATemplateService();
        private SysUserService UserSer = new SysUserService();
        
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                if (e.Row.Cells[8].Text.ToString() == "0.00")
                {
                    e.Row.Cells[8].Attributes.Add("bgColor","yellow"); 
                }
                if (e.Row.Cells[9].Text.ToString() == "0.00")
                {
                    e.Row.Cells[9].Attributes.Add("bgColor", "yellow");
                }
            }
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/Performance/AllPAFormDetail.aspx?PAFormId=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString() + "&selectYearMonth=" + ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue+"&pageindex=" + gvList.PageIndex);           
        }

        protected void cbIsDeleted_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb1 = sender as CheckBox;
            for (int i = 0; i < this.gvList.Rows.Count; i++)
            {
                CheckBox cb = (gvList.Rows[i].FindControl("cbIsDeleted")) as CheckBox;
                cb.Checked = cb1.Checked;
            }
        }
        protected bool IsDeletedEdit()
        {
            if (ViewState["cbIsDeleted"] != null)
            {
                return false;
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {

                //if (VAN_OA.JXC.SysObj.NewShowAll_textName("所有绩效考核表", Session["currentUserId"], "删除标记") == false)
                //{
                //    ViewState["cbIsDeleted"] = false;
                //    btnDeleted.Visible = false;
                //}

                ddlUser.DataSource = UserSer.getAllUserByLoginName(null);
                ddlUser.DataTextField = "loginName";
                ddlUser.DataValueField = "ID";
                ddlUser.DataBind();
                //ddlYear.Items.Add(new ListItem("--选择--"));
                for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year  + 5; i++)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(),i.ToString()));
                }

                //if (base.Request["pageindex"] != "" && base.Request["pageindex"] != null)
                //{
                //    gvList.PageIndex = int.Parse(base.Request["pageindex"].ToString());
                //}
                //AspNetPager1.CurrentPageIndex = 1;
                //PagerDomain page = new PagerDomain();
                //page.CurrentPageIndex = AspNetPager1.CurrentPageIndex;


                if (base.Request["selectYearMonth"] == "" || base.Request["selectYearMonth"] == "-" || base.Request["selectYearMonth"] == null)
                {
                    ddlMonth.Text = DateTime.Today.AddMonths(-1).Month.ToString("00");
                    ddlYear.Text = DateTime.Today.AddMonths(-1).Year.ToString();
                    //this.gvList.DataSource = ApprovePAFormListSer.GetAllPAFormList("",DateTime.Today.AddMonths(-1).ToString("yyyy-MM"),page);
                }
                else
                {
                    ddlYear.Text = base.Request["selectYearMonth"].Substring(0, 4);
                    ddlMonth.Text = base.Request["selectYearMonth"].Substring(5, 2);
                    //this.gvList.DataSource = ApprovePAFormListSer.GetAllPAFormList("", ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue, page);
                }
                //AspNetPager1.RecordCount = page.TotalCount;
                //this.gvList.DataBind();

                AspNetPager1.CurrentPageIndex = 1;
                select();
            }
        }

        protected void btnDeleted_Click(object sender, EventArgs e)
        {

            if (ViewState["cbIsDeleted"] == null)
            {
                string where = "";
                for (int i = 0; i < this.gvList.Rows.Count; i++)
                {
                    CheckBox cb = (gvList.Rows[i].FindControl("cbIsDeleted")) as CheckBox;
                    if (cb.Checked)
                    {                      
                        where += this.gvList.DataKeys[i].Value.ToString() + ",";
                    }                    
                }

                if (where != "")
                {
                    where = where.Substring(0, where.Length - 1);
                    ApprovePAFormListSer.BatchDelete(where);
                }
                else
                {
                    return;
                }

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                select();
            }
        }

        protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DropDownList ddlUserID = this.gvList.Rows[e.RowIndex].FindControl("ddlUserID") as DropDownList;
            if (ddlUserID.SelectedValue != "")
            {
                string sql = string.Format(@"select count(*) from tb_UserPAForm where UserID=" + ddlUserID.SelectedValue);
                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('复制失败,该用户已有绩效考核模版！');</script>");
                    return;
                }
                //this.PAUserTemplateSer.Copy(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()), int.Parse(ddlUserID.SelectedValue.ToString()));
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('复制成功！');</script>");
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请先选择用户！');</script>");
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            select();
        }

        private void select()
        {
            PagerDomain page = new PagerDomain();
            page.CurrentPageIndex = AspNetPager1.CurrentPageIndex;

            if (ddlYear.SelectedValue == "" || ddlMonth.SelectedValue == "" || Chk_All.Checked)
            {
                this.gvList.DataSource = ApprovePAFormListSer.GetAllPAFormList(ddlUser.SelectedValue, "", page);
            }
            else
            {
                this.gvList.DataSource = ApprovePAFormListSer.GetAllPAFormList(ddlUser.SelectedValue, ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue, page);
            }
            AspNetPager1.RecordCount = page.TotalCount;
            this.gvList.DataBind();
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            select();
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ApprovePAFormListSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            select();
            //this.gvList.DataSource = ApprovePAFormListSer.GetAllPAFormList();
            //this.gvList.DataBind(); 
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //this.gvList.PageIndex = e.NewPageIndex;
            //if (Chk_All.Checked)
            //{
            //    this.gvList.DataSource = ApprovePAFormListSer.GetAllPAFormList();
            //}
            //else
            //{
            //    if (ddlYear.SelectedValue != "" && ddlMonth.SelectedValue != "")
            //    {
            //        this.gvList.DataSource = ApprovePAFormListSer.GetAllPAFormList("", ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue);
            //    }
            //    else
            //    {
            //        this.gvList.DataSource = ApprovePAFormListSer.GetAllPAFormList("", DateTime.Today.AddMonths(-1).ToString("yyyy-MM"));
            //    }
            //}
            //this.gvList.DataBind(); 
        }
    }
}
