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
    public partial class AllPAFormDetail : System.Web.UI.Page
    {
        private A_PAUserTemplateService PAUserTemplateSer = new A_PAUserTemplateService();
        private ApprovePAFormDetailService PAFormDetailSer = new ApprovePAFormDetailService();
        private PAFormDetail PAFormDetailHead = new PAFormDetail();
        private DataTable PAItemDetail;
        private List<string> Section = new List<string>();
        private List<decimal> ScoreSum = new List<decimal>();
        private List<decimal> FirstSum = new List<decimal>();
        private List<decimal> SecondSum = new List<decimal>();
        private List<decimal> AmountSum = new List<decimal>();
        private decimal Avg = 0;

        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Performance/PAFormList.aspx?selectYearMonth=" + base.Request["selectYearMonth"] + "&pageindex=" + base.Request["pageindex"]); 
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < Section.Count; i++)
                {
                    if (Section[i] == PAItemDetail.Rows[e.Row.RowIndex]["PASectionName"].ToString())
                    {
                        ScoreSum[i] += decimal.Parse(e.Row.Cells[2].Text.ToString());
                        FirstSum[i] += decimal.Parse(e.Row.Cells[3].Text.ToString());
                        SecondSum[i] += decimal.Parse(e.Row.Cells[4].Text.ToString());
                        AmountSum[i] += decimal.Parse(e.Row.Cells[5].Text.ToString());
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "合计：";
                for (int i = 0; i < Section.Count; i++)
                {
                    e.Row.Cells[2].Text += Section[i] + ":" + ScoreSum[i].ToString() + "<br>";
                    e.Row.Cells[3].Text += Section[i] + ":" + FirstSum[i].ToString() + "<br>";
                    e.Row.Cells[4].Text += Section[i] + ":" + SecondSum[i].ToString() + "<br>";
                    e.Row.Cells[5].Text += Section[i] + ":" + AmountSum[i].ToString() + "<br>";
                    Avg = (FirstSum[i] + SecondSum[i]) / 2;
                    e.Row.Cells[8].Text = Avg.ToString();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Section = PAFormDetailSer.GetPAFormSection(base.Request["PAFormId"]);
                for (int i = 0; i < Section.Count; i++)
                {
                    ScoreSum.Add(0);
                    FirstSum.Add(0);
                    SecondSum.Add(0);
                    AmountSum.Add(0);
                }  
                PAFormDetailHead = PAFormDetailSer.GetPAFormHead(base.Request["PAFormId"]);
                PAItemDetail = PAFormDetailSer.GetPAFormDetail(base.Request["PAFormId"]);
                lblStatus.Text = PAFormDetailHead.Status.ToString();
                lblUserName.Text = PAFormDetailHead.UserName.ToString();
                lblDepartment.Text = PAFormDetailHead.UserIPosition.ToString();
                lblMonth.Text = PAFormDetailHead.Month.ToString();
                lblAttendDays.Text = PAFormDetailHead.AttendDays.ToString();
                lblLeaveDays.Text = PAFormDetailHead.LeaveDays.ToString();
                lblFullAttendBonus.Text = PAFormDetailHead.AttendDays.ToString();
                gvList.DataSource = PAItemDetail;
                gvList.DataBind(); 
            }
        }
    }
}