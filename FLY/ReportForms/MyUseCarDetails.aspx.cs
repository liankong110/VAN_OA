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
using VAN_OA.Dal.ReportForms;
using System.Collections.Generic;
using VAN_OA.Model.ReportForms;
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.EFrom;


namespace VAN_OA.ReportForms
{
    public partial class MyUseCarDetails : System.Web.UI.Page
    {
        private TB_UseCarDetailService useCarSer = new TB_UseCarDetailService();

        private tb_UseCarService useCarSer_Si = new tb_UseCarService();


        private void Show()
        {
            string sql = " 1=1 ";
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and AppTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and AppTime<='{0} 23:59:59'", txtTo.Text);
            }

            if (txtGuestName.Text != "")
            {
                sql += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text);
            }


            if (txtDriver.Text != "")
            {
                sql += string.Format(" and Driver like '%{0}%'", txtDriver.Text);
            }



            if (ddlCarNo.Text != "")
            {
                sql += string.Format(" and CarNo like '%{0}%'", ddlCarNo.Text);
            }



            if (txtProNo.Text != "")
            {
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text);
            }


            if (txtUseFromTime.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtUseFromTime.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('使用日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and useDate>='{0} 00:00:00'", txtUseFromTime.Text);
            }

            if (txtUseToTime.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtUseToTime.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('使用日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and useDate<='{0} 23:59:59'", txtUseToTime.Text);
            } 
            

            sql += string.Format(@" and AppUser={0} and TB_UseCarDetail.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='用车明细表') and state='通过')",Session["currentUserId"].ToString() );

           // List<TB_UseCarDetail> UseCarServices = this.useCarSer.GetListArrayReps(sql);

            decimal Total = 0;
            decimal Total1 = 0;
            List<TB_UseCarDetail> UseCarServices = this.useCarSer.GetListArrayReps_1(sql, out Total,out Total1);
            lblTotal.Text = Total.ToString();

            AspNetPager1.RecordCount = UseCarServices.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = UseCarServices;
            this.gvList.DataBind();
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
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
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



        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                DataTable carInfos = DBHelp.getDataTable("select ''as CarNO union select CarNO from TB_CarInfo");
                ddlCarNo.DataSource = carInfos;
                ddlCarNo.DataBind();
                ddlCarNo.Text = "";

                List<TB_UseCarDetail> UseCarServices = new List<TB_UseCarDetail>();
                this.gvList.DataSource = UseCarServices;
                this.gvList.DataBind();


                List<tb_UseCar> UseCarServices_Si = new List<tb_UseCar>();
                this.GvSi.DataSource = UseCarServices_Si;
                this.GvSi.DataBind();
            }
        }

        protected void GvSi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GvSi.PageIndex = e.NewPageIndex;
            showSi();
        }

        protected void GvSi_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }

        }

        protected void btnSiSelect_Click(object sender, EventArgs e)
        {
            showSi();
        }

        private void showSi()
        {
            string sql = " 1=1 ";
            if (txtSiFrom.Text != "")
            {
                sql += string.Format(" and datetime>='{0} 00:00:00'", txtSiFrom.Text);
            }

            if (txtSiTo.Text != "")
            {
                sql += string.Format(" and datetime<='{0} 23:59:59'", txtSiTo.Text);
            }


            //if (txtSiApper.Text != "")
            //{
            //    sql += string.Format(" and loginName like '%{0}%'", txtSiApper.Text);
            //}
            if (txtProNo1.Text != "")
            {
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo1.Text);
            }


            sql += string.Format(@" and appName={0}  and tb_UseCar.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='私车公用申请单') and state='通过')", Session["currentUserId"].ToString());
            decimal Total = 0;
            decimal Total1 = 0;
            List<tb_UseCar> UseCarServices = this.useCarSer_Si.GetListArray_Req(sql, out Total, out Total1);

            lblSiTotal.Text = Total.ToString();

            this.GvSi.DataSource = UseCarServices;
            this.GvSi.DataBind();
        }
    }
}
