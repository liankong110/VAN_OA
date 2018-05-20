using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.KingdeeInvoice;
using VAN_OA.Model.KingdeeInvoice;
using System.Data.SqlClient;
using System.Data;
using VAN_OA.Dal;
using VAN_OA.Model;



namespace VAN_OA.MyExcels
{
    public partial class WFExcelList : BasePage
    {


        MyExcelService myExcelSer = new MyExcelService();

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        private void Show()
        {

            string sql = " 1=1 ";

            if (txtContent.Text != "")
            {
                sql += string.Format(" and ExcelJosnContent like '%{0}%'", txtContent.Text);
            }
            PagerDomain page = new PagerDomain();
            page.PageSize = 50;
            page.CurrentPageIndex = AspNetPager1.CurrentPageIndex;
            List<MyExcel> invoiceList = this.myExcelSer.GetSonExcel(ddlExcel.Text, sql, page);
            AspNetPager1.RecordCount = page.TotalCount;
            //this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvList.DataSource = invoiceList;
            this.gvList.DataBind();

             

        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvList.PageIndex = e.NewPageIndex;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<MyExcel> invoiceList = new List<MyExcel>();
                this.gvList.DataSource = invoiceList;
                this.gvList.DataBind();

                MyExcelService dal = new MyExcelService();
                var dalList = dal.GetMainExcel(" UpState=3 ");

                ddlExcel.DataSource = dalList;
                ddlExcel.DataBind();
                ddlExcel.DataTextField = "Name";
                ddlExcel.DataValueField = "Table_Name";                
            }
        }
    }
}
