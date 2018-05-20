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
using VAN_OA.Dal.HR;
using VAN_OA.Model.HR;
using System.Collections.Generic;
namespace VAN_OA.HR
{
    public partial class PersonList : System.Web.UI.Page
    {
        private HR_PERSONService perSon = new HR_PERSONService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/HR/Person.aspx?pageindex = "+gvList.PageIndex);
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string sql = "";
            if (txtName.Text != "")
            {
                sql += string.Format(" Name like '%{0}%'", txtName.Text);
                if (chkQuit.Checked == false)
                {
                    sql += " and QuitStatus=0";
                }
            }
            else
            {
                if (chkQuit.Checked == false)
                {
                    sql += " QuitStatus=0";
                }
            }

            List<HR_PERSON> pers = this.perSon.GetListArray(sql);
            this.gvList.DataSource = pers;
            this.gvList.DataBind();
        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;

            string sql = "";
            if (txtName.Text != "")
            {
                sql += string.Format(" Name like '%{0}%'", txtName.Text);
                if (chkQuit.Checked == false)
                {
                    sql += " and QuitStatus=0";
                }
            }
            else
            {
                if (chkQuit.Checked == false)
                {
                    sql += " QuitStatus=0";
                }
            }  

            List<HR_PERSON> pers = this.perSon.GetListArray(sql);
            this.gvList.DataSource = pers;
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
          
                this.perSon.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
                List<HR_PERSON> pers = this.perSon.GetListArray("");
                this.gvList.DataSource = pers;
                this.gvList.DataBind();
          
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/HR/Person.aspx?Code=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString() + "&pageindex="+gvList.PageIndex);
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<HR_PERSON> pers;
                string sql = "";
                if (chkQuit.Checked == false)
                {
                    sql += " QuitStatus=0";
                }
                if (base.Request["pageindex"] != "" && base.Request["pageindex"]!=null)
                {
                    gvList.PageIndex = int.Parse (base.Request["pageindex"].ToString());
                }
                if (base.Request["ContractRemind"] == "1")
                {
                    pers = this.perSon.GetListArray(" ContractCloseTime=convert(varchar(50),getdate(),23)"); 
                }
                else
                {
                    pers = this.perSon.GetListArray(sql);
                }
                this.gvList.DataSource = pers;
                this.gvList.DataBind();
                #region 是否有删除功能
                //if (Session["currentUserId"] != null)
                //{
                //    VAN_OA.Dal.TB_AdminDeleteService deleteSer = new VAN_OA.Dal.TB_AdminDeleteService();
                //    if (deleteSer.CheckIsExistByUserId(Convert.ToInt32(Session["currentUserId"])) == false)
                //    {
                //        gvList.Columns[1].Visible = false;
                //    }
                //}
                #endregion
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
                if (e.CommandName == "Salary")
                {
                    base.Response.Redirect("~/HR/SalaryInfo.aspx?Code=" + e.CommandArgument+"&pageindex="+gvList.PageIndex);
                }
        }
    }
}
