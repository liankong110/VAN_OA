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
using VAN_OA.Dal.HR;
using VAN_OA.Model.HR;
using System.Collections.Generic;
namespace VAN_OA.HR
{
    public partial class PaymentList : System.Web.UI.Page
    {
        private HR_PaymentService paymentSer = new HR_PaymentService();
        private SysUserService UserSer = new SysUserService();
        private DataTable MonthSum=new DataTable();
        private List<HR_Payment> payment = new List<HR_Payment>();

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string sql = "";
            if (ddlUser.SelectedValue != "")
            {
                sql += " P.ID='" + ddlUser.SelectedValue + "'";
                if (ddlYear.SelectedValue != "" && ddlMonth.SelectedValue != "")
                {
                    sql += " and H.YearMonth='" + ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "'";
                }
            }
            else
            {
                if (ddlYear.SelectedValue != "" && ddlMonth.SelectedValue != "")
                {
                    sql += " H.YearMonth='" + ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "'";
                }
            }
            if (ddlYear.SelectedValue != "" && ddlMonth.SelectedValue != "")
            {
                MonthSum=DBHelp.getDataTable("Select Sum(ActualPayment) as ActualPayment from HR_Payment where YearMonth='" + ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "'");
                lblMonthSummary.Text=MonthSum.Rows[0]["ActualPayment"].ToString();
            }
            payment = this.paymentSer.GetListArray(sql);
            this.gvList.DataSource = payment;
            this.gvList.DataBind();
            this.gvList_Temp.DataSource = payment;
            this.gvList_Temp.DataBind();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string sql;
            gvList.PageIndex = e.NewPageIndex;
            if (ddlYear.SelectedValue != "" && ddlMonth.SelectedValue != "")
            {
                sql = " H.YearMonth='" + ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "'";
            }
            else
            {
                sql = "";
            }
            payment = this.paymentSer.GetListArray(sql);
            this.gvList.DataSource = payment;
            this.gvList.DataBind();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                e.Row.Cells[7].ToolTip = payment[gvList.PageIndex * gvList.PageSize + e.Row.RowIndex].SpecialAwardNote.ToString();
                e.Row.Cells[17].ToolTip = payment[gvList.PageIndex * gvList.PageSize + e.Row.RowIndex].DeductionNote.ToString();
           }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            this.paymentSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            payment = this.paymentSer.GetListArray("");
            this.gvList.DataSource = payment;
            this.gvList.DataBind();
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/HR/PaymentInfo.aspx?Code=" + this.gvList.DataKeys[e.NewEditIndex]["ID"].ToString() + "&Yearmonth=" + this.gvList.DataKeys[e.NewEditIndex]["YearMonth"].ToString() + "&selectYearMonth=" + ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "&pageindex=" + gvList.PageIndex);
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string sql;
                ddlUser.DataSource = UserSer.getAllUserByLoginName(null);
                ddlUser.DataTextField = "loginName";
                ddlUser.DataValueField = "ID";
                ddlUser.DataBind();
                ddlYear.Items.Add(new ListItem("--选择--",""));
                for (int i = DateTime.Now.Year - 1; i < DateTime.Now.Year + 5; i++)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                if (base.Request["selectYearMonth"] == "" || base.Request["selectYearMonth"] == null)
                {
                    ddlYear.Text = DateTime.Now.ToString("yyyy");
                    ddlMonth.Text = DateTime.Now.ToString("MM");
                }
                else
                {
                    ddlYear.Text = base.Request["selectYearMonth"].Substring(0,4);
                    ddlMonth.Text = base.Request["selectYearMonth"].Substring(5, 2) ;
                }
                if (ddlYear.SelectedValue != "" && ddlMonth.SelectedValue != "")
                {
                    sql = " H.YearMonth='" + ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "'";

                }
                else
                {
                    sql = "";
                }
                if (base.Request["pageindex"] != "" && base.Request["pageindex"] != null)
                {
                    gvList.PageIndex = int.Parse(base.Request["pageindex"].ToString());
                }
                payment = this.paymentSer.GetListArray(sql);
                this.gvList.DataSource = payment;
                this.gvList.DataBind();
                this.gvList_Temp.DataSource = payment;
                this.gvList_Temp.DataBind();

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

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ToExcel("Gongzi.xls");
        }
        private void ToExcel(string FileName)
        { 
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName);
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文
            this.EnableViewState = false;
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvList_Temp.RenderControl(hw);
            Response.Write(sw.ToString());  
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlUser.SelectedValue != "")
            {
                base.Response.Redirect("~/HR/PaymentInfo.aspx?Code=" + ddlUser.SelectedValue + "&Yearmonth=");
            }
        }
    }
}
