using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.Fin;
using VAN_OA.Model.Fin;

namespace VAN_OA.Fin
{
    public partial class WFReport : System.Web.UI.Page
    {
        protected List<Report> reportList = new List<Report>();

        protected List<Report> topReportList = new List<Report>();

        protected List<Report> topMonthReportList = new List<Report>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int year = Convert.ToInt32(Request["year"]);
                int month = Convert.ToInt32(Request["month"]);
                DateTime currentDate = Convert.ToDateTime(year+"-"+ month + "-01");
                ViewState["CurrentDate"] = currentDate;
                reportList = new BankFlowService().Report(year, month);
                topReportList = new BankFlowService().Report(currentDate.AddYears(-1).Year, currentDate.Month);
                topMonthReportList = new BankFlowService().Report(currentDate.AddMonths(-1).Year, currentDate.AddMonths(-1).Month);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/WFBandFlow.aspx");
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //reportList = new BankFlowService().Report(DateTime.Now.Year, DateTime.Now.Month);
            //topReportList = new BankFlowService().Report(DateTime.Now.AddYears(-1).Year, DateTime.Now.Month);
            //topMonthReportList = new BankFlowService().Report(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month);
            int year = Convert.ToInt32(Request["year"]);
            int month = Convert.ToInt32(Request["month"]);
            DateTime currentDate = Convert.ToDateTime(year + "-" + month + "-01");
            reportList = new BankFlowService().Report(year, month);
            topReportList = new BankFlowService().Report(currentDate.AddYears(-1).Year, currentDate.Month);
            topMonthReportList = new BankFlowService().Report(currentDate.AddMonths(-1).Year, currentDate.AddMonths(-1).Month);

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            //Response.AppendHeader("Content-Disposition", string.Format("attachment;filename={0}银行往来月报表.XLS", DateTime.Now.ToString("yyyy/MM")));

            Response.AppendHeader("Content-Disposition",
               "attachment;filename=\"" + HttpUtility.UrlEncode(
                   string.Format("{0}银行往来月报表.XLS", currentDate.ToString("yyyy/MM"), System.Text.Encoding.UTF8)));

            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文
            this.EnableViewState = false;
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);            ;
            plReport.RenderControl(hw);            
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}