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
    public partial class A_PAUserTemplateList : System.Web.UI.Page
    {
        private A_PAUserTemplateService PAUserTemplateSer = new A_PAUserTemplateService();
        private A_PATemplateService PATemplateSer = new A_PATemplateService();
        private SysUserService UserSer = new SysUserService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Performance/A_PATemplateCmd1.aspx");
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                DropDownList ddlTemplateID = e.Row.FindControl("ddlTemplateID") as DropDownList;
                if (ddlTemplateID != null)
                {
                    ddlTemplateID.DataSource = PATemplateSer.GetModelList("");
                    ddlTemplateID.DataTextField = "A_PATemplateName";
                    ddlTemplateID.DataValueField = "A_PATemplateID";
                    ddlTemplateID.DataBind();
                    ddlTemplateID.SelectedValue = gvList.DataKeys[e.Row.RowIndex]["PATemplateID"].ToString();
                }
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = string.Format(@"select count(*) from A_ProInfos where a_Section_Id=" + this.gvList.DataKeys[e.RowIndex].Value.ToString());
            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除失败,该信息已被其他信息引用！');</script>");
                return;
            }

            this.PAUserTemplateSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            //List<A_PATemplate> PATemplate = this.PATemplateSer.GetModelList(string.Format(" A_PATemplateName like '%{0}%'", this.txtName.Text.Trim()));
            //this.gvList.DataSource = PATemplate;
            //this.gvList.DataBind();
         
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/Performance/A_PAUserTemplateCmd1.aspx?UserID=" + this.gvList.DataKeys[e.NewEditIndex][0].ToString());
             
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.gvList.DataSource = PAUserTemplateSer.GetUserTemplateList("");
                this.gvList.DataBind();
                ddlYear.Items.Add(new ListItem("--选择--"));
                for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year  + 5; i++)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(),i.ToString()));
                }
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

        protected void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                CheckBox cbSelect = gvList.Rows[i].FindControl("cbSelect") as CheckBox;
                if (cbSelect.Enabled==true)
                {
                cbSelect.Checked = true;
                }
            }
        }

        protected void btnUnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                CheckBox cbSelect = gvList.Rows[i].FindControl("cbSelect") as CheckBox;
                if (cbSelect.Enabled == true)
                {
                    cbSelect.Checked = false;
                }
            }
        }

        protected void btnDisSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                CheckBox cbSelect = gvList.Rows[i].FindControl("cbSelect") as CheckBox;
                if (cbSelect.Enabled == true)
                {
                    if (cbSelect.Checked == true)
                    {
                        cbSelect.Checked = false;
                    }
                    else
                    {
                        cbSelect.Checked = true;
                    }
                }
            }
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            if (ddlYear.SelectedValue != "" && ddlMonth.SelectedValue != "")
            {
                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    CheckBox cbSelect = gvList.Rows[i].FindControl("cbSelect") as CheckBox;
                    if (cbSelect.Checked)
                    {
                        if (PAUserTemplateSer.Copy(gvList.DataKeys[i]["PATemplateID"].ToString(), gvList.DataKeys[i]["Id"].ToString(), ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue) == 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('复制成功！');</script>");
                        }
                        else
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('复制失败！');</script>");
                        }
                    }
                }
            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('复制年月不得为空！');</script>");
            }
        }
    }
}
