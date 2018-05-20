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
    public partial class MyPAFormDetail : System.Web.UI.Page
    {
        private ApprovePAFormDetailService PAFormDetailSer = new ApprovePAFormDetailService();
        private PAFormDetail thisPAFormDetail = new PAFormDetail();
        decimal ScoreSum = 0;
        decimal FirstSum = 0;
        decimal SecondSum = 0;
        decimal AmountSum = 0;

        protected void btnClose_Click(object sender, EventArgs e)
        {
            if (base.Request["From"]=="All")
            { 
                base.Response.Redirect("~/Performance/PAFormList.aspx"); 
            }
            else
            {
                base.Response.Redirect("~/Performance/MyPAFormList.aspx");
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ScoreSum += decimal.Parse(e.Row.Cells[2].Text.ToString());
                FirstSum += decimal.Parse(e.Row.Cells[3].Text.ToString());
                SecondSum += decimal.Parse(e.Row.Cells[4].Text.ToString());
                AmountSum += decimal.Parse(e.Row.Cells[5].Text.ToString());
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "合计：";
                e.Row.Cells[2].Text = ScoreSum.ToString();
                e.Row.Cells[3].Text = FirstSum.ToString();
                e.Row.Cells[4].Text = SecondSum.ToString();
                e.Row.Cells[5].Text = AmountSum.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                thisPAFormDetail = PAFormDetailSer.GetPAFormHead(base.Request["PAFormId"]);
                lblStatus.Text = thisPAFormDetail.Status.ToString();
                lblUserName.Text = thisPAFormDetail.UserName.ToString();
                lblDepartment.Text = thisPAFormDetail.UserIPosition.ToString();
                lblMonth.Text = thisPAFormDetail.Month.ToString();
                lblAttendDays.Text = thisPAFormDetail.AttendDays.ToString(); 
                lblLeaveDays.Text = thisPAFormDetail.LeaveDays.ToString(); 
                lblFullAttendBonus.Text = thisPAFormDetail.AttendDays.ToString(); 
                gvList.DataSource = PAFormDetailSer.GetPAFormDetail(base.Request["PAFormId"]);
                gvList.DataBind(); 
            }
        }
    }
}
