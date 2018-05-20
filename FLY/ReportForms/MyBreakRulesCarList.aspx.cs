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
namespace VAN_OA.ReportForms
{
    public partial class MyBreakRulesCarList : BasePage
    {
        private TB_BreakRulesCarService braRuleCarSer = new TB_BreakRulesCarService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Session["POUrl"] = "~/ReportForms/BreakRulesCarList.aspx";
            //base.Response.Redirect("~/ReportForms/BreakRulesCar.aspx");
        }

        private void Show()
        {
            string sql = " 1=1 ";
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('违章时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and BreakTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('违章时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and BreakTime<='{0} 23:59:59'", txtTo.Text);
            }

            if (ddlCarNo.Text != "")
            {
                sql += string.Format(" and CarNo like '%{0}%'", ddlCarNo.Text);
            }


            if (txtDriver.Text != "")
            {
                sql += string.Format(" and Driver like '%{0}%'", txtDriver.Text);
            }

            if (ddlState.Text != "")
            {
                sql += string.Format(" and state like '%{0}%'", ddlState.Text);
            }




            List<TB_BreakRulesCar> cars = this.braRuleCarSer.GetListArray(sql);

            AspNetPager1.RecordCount = cars.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = cars;
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

            //this.braRuleCarSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            //Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Session["POUrl"] = "~/ReportForms/BreakRulesCarList.aspx";
            //base.Response.Redirect("~/ReportForms/BreakRulesCar.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                TB_CarInfoService carInfoSer = new TB_CarInfoService();
                System.Collections.Generic.List<TB_CarInfo> carInfos = carInfoSer.GetListArray("");
                if (carInfos.Count > 0)
                {
                    lblMess.Text += "<table cellpadding='0' cellspacing='0'  border='1'  width=50%'>";
                    lblMess.Text += "<tr><td style='color:black'><font>车牌</font></td><td style='color:black'>年检时间</td><td style='color:black'>保险时间</td></tr>";
                    for (int i = 0; i < carInfos.Count; i++)
                    {
                        lblMess.Text += string.Format("<tr><td> {0}</td>  <td>{1}</td><td>{2}</td></tr>", carInfos[i].CarNo, carInfos[i].NianJian, carInfos[i].Baoxian);
                    }
                    lblMess.Text += "</table>";
                }
                carInfos.Insert(0, new TB_CarInfo());
                ddlCarNo.DataSource = carInfos;
                ddlCarNo.DataBind();
                ddlCarNo.DataTextField = "CarNo";
                ddlCarNo.DataValueField = "CarNo";
                List<TB_BreakRulesCar> poseModels = new List<TB_BreakRulesCar>();
                this.gvList.DataSource = poseModels;
                this.gvList.DataBind();
            }
        }
    }
}
