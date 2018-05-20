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
    public partial class MyPAFormMulti : System.Web.UI.Page
    {
        private ApprovePAFormDetailService PAFormDetailSer = new ApprovePAFormDetailService();
        private ApprovePAFormMultiService PAFormMultiSer = new ApprovePAFormMultiService();
        private PAFormDetail thisPAFormDetail = new PAFormDetail();
        decimal ScoreSum = 0;

        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Performance/ApprovePAFormList.aspx");
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ScoreSum += decimal.Parse(e.Row.Cells[1].Text.ToString());
                if (decimal.Parse(e.Row.Cells[1].Text.ToString())==0 && e.Row.Cells[0].Text.Trim().ToString()!="")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow; 
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "合计：";
                e.Row.Cells[1].Text = ScoreSum.ToString();
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
                gvList.DataSource = PAFormMultiSer.GetPAFormMulti(base.Request["PAFormId"],base.Request["PAItemId"]);
                gvList.DataBind(); 
            }
        }
    }
}
