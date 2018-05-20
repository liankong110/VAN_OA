using System;
using System.Web.UI.WebControls;
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;
using VAN_OA.Model;

namespace VAN_OA.JXC
{
    public partial class Sell_TuiSunCha_ReportList : BasePage
    {
        readonly Sell_TuiSunCha_ReportService POSer = new Sell_TuiSunCha_ReportService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //主单
                List<JXC_REPORT> pOOrderList = new List<JXC_REPORT>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();
              
                if (Request["PONo"] != null)
                {                  
                    Show();
                }
            }
        }


        private void Show()
        {
            string sql = " 1=1 ";

            if (Request["PONo"] != null)
            {
                sql += string.Format(" and Sell_TuiSunCha_Report.PONo like '%{0}%'", Request["PONo"]);
            }

            List<JXC_REPORT> pOOrderList = this.POSer.GetListArray(sql);
            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMain.PageIndex = e.NewPageIndex;
            Show();
        }
        JXC_REPORT SumModel = new JXC_REPORT();
        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                if (SumModel.goodTotal == null) SumModel.goodTotal = 0;
                if (SumModel.maoli == null) SumModel.maoli = 0;
                if (SumModel.t_GoodTotalChas == null) SumModel.t_GoodTotalChas = 0;
                if (SumModel.goodSellTotal == null) SumModel.goodSellTotal = 0;

                JXC_REPORT model = e.Row.DataItem as JXC_REPORT;
                SumModel.goodTotal += model.goodTotal;
                SumModel.maoli += model.maoli;
                SumModel.t_GoodTotalChas += model.t_GoodTotalChas;
                SumModel.goodSellTotal += model.goodSellTotal;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("goodTotal") as Label, SumModel.goodTotal == null ? "" : SumModel.goodTotal.ToString());//数量
                setValue(e.Row.FindControl("maoli") as Label, SumModel.maoli == null ? "" : SumModel.maoli.ToString());//数量
                setValue(e.Row.FindControl("t_GoodTotalChas") as Label, SumModel.t_GoodTotalChas == null ? "" : SumModel.t_GoodTotalChas.ToString());//数量
                setValue(e.Row.FindControl("goodSellTotal") as Label, SumModel.goodSellTotal == null ? "" : SumModel.goodSellTotal.ToString());//数量

            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


        private void setValue(Label control, string value)
        {
            if (control != null)
                control.Text = value;
        }
    }
}
