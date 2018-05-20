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

namespace VAN_OA.EFrom
{
    public partial class UseCarDetailManList : BasePage
    {
        private TB_UseCarDetailService useCarSer = new TB_UseCarDetailService();

        public string Query = "QueryUseCarDetail";

        private void Show()
        {
            string sql = " 1=1 ";
            QuerySession.QueryUseCarDetail QueryUserDetail = new QuerySession.QueryUseCarDetail();

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and AppTime>='{0} 00:00:00'", txtFrom.Text);
                QueryUserDetail.FromTime = txtFrom.Text;
               
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and AppTime<='{0} 23:59:59'", txtTo.Text);
                QueryUserDetail.ToTime = txtTo.Text;
            }

            if (txtGuestName.Text != "")
            {
                sql += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text);
                QueryUserDetail.Company = txtGuestName.Text;
            }


            if (txtDriver.Text != "")
            {
                sql += string.Format(" and Driver like '%{0}%'", txtDriver.Text);
                QueryUserDetail.Driver = txtDriver.Text;
            }

            if (txtAppName.Text != "")
            {
                sql += string.Format(" and LoginName like '%{0}%'", txtAppName.Text);
                QueryUserDetail.Apper = txtAppName.Text;
            }


            if (txtCarNo.Text != "")
            {
                sql += string.Format(" and CarNo like '%{0}%'", txtCarNo.Text);
                QueryUserDetail.CarNo = txtCarNo.Text;
            }


            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and TB_UseCarDetail.ProNo like '%{0}%'", txtProNo.Text.Trim());
                QueryUserDetail.ProNo = txtProNo.Text.Trim();
            }


            sql += string.Format(@" and TB_UseCarDetail.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='用车明细表') and state='执行中')");
            decimal Total = 0;
                 decimal Total1 = 0;
                 List<TB_UseCarDetail> UseCarServices = this.useCarSer.GetListArrayReps_1(sql, out Total, out Total1);


            Session[Query] = QueryUserDetail;
            // lblTotal.Text = Total.ToString();
            this.gvList.DataSource = UseCarServices;
            this.gvList.DataBind();
        }
        protected void btnSelect_Click(object sender, EventArgs e)
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

            Session["backurl"] = "/EFrom/UseCarDetailManList.aspx";

            string type = "用车明细表";

            tb_EFormService eformSer = new tb_EFormService();
            tb_EForm eform = eformSer.GetModel(Convert.ToInt32(this.gvList.DataKeys[e.NewEditIndex].Value.ToString()), type);
            if (eform != null)
            {
                string url = "~/EFrom/UseCarDetailMan.aspx?ProId=" + eform.proId + "&allE_id=" + eform.allE_id + "&EForm_Id=" + eform.id;
                base.Response.Redirect(url);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                WebQuerySessin Sess = new WebQuerySessin(Query);


                  //加载SESSION中的数据
                if (Session[Query] != null)
                {
                    //QueryEForms
                    QuerySession.QueryUseCarDetail QUseCarDetail = Session[Query] as QuerySession.QueryUseCarDetail;
                    if (QUseCarDetail == null)
                    {
                        return;
                    }
                    if (QUseCarDetail.FromTime != "")
                    {
                        txtFrom.Text = QUseCarDetail.FromTime;
                    }

                    if (QUseCarDetail.ToTime != "")
                    {
                        txtTo.Text = QUseCarDetail.ToTime;
                    }

                    if (QUseCarDetail.Company != "")
                    {
                        txtGuestName.Text = QUseCarDetail.Company;
                    }


                    if (QUseCarDetail.Driver != "")
                    {
                        txtDriver.Text = QUseCarDetail.Driver;
                    }

                    if (QUseCarDetail.Apper != "")
                    {
                        txtAppName.Text = QUseCarDetail.Apper;
                    }


                    if (QUseCarDetail.CarNo != "")
                    {
                        txtCarNo.Text = QUseCarDetail.CarNo;
                    }
                    
                    if (QUseCarDetail.ProNo != "")
                    {
                        txtProNo.Text = QUseCarDetail.ProNo;
                    }
                    Show();
                }
                else
                {

                    List<TB_UseCarDetail> UseCarServices = new List<TB_UseCarDetail>();
                    this.gvList.DataSource = UseCarServices;
                    this.gvList.DataBind();
                }
            }
        }


    }
}
