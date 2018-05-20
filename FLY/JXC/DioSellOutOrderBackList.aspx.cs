using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.ReportForms;
using VAN_OA.Model.JXC;


namespace VAN_OA.JXC
{
    public partial class DioSellOutOrderBackList : BasePage
    {
        private Sell_OrderOutHouseService POSer = new Sell_OrderOutHouseService();
       

        private void Show()
        {
            string sql = " and Sell_OrderOutHouse.CreateUserId=" + Session["currentUserId"];
            sql = "";
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and Sell_OrderOutHouse.PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and Sell_OrderOutHouse.POName like '%{0}%'", ttxPOName.Text.Trim());
            }
            if (txtSellNo.Text.Trim() != "")
            {
                if (CheckProNo(txtSellNo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and Sell_OrderOutHouse.ProNo like '%{0}%'", txtSellNo.Text.Trim());
            }
            List<Sell_OrderOutHouse> cars = this.POSer.GetListArray_ToBack(sql);
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Session["OrderInHouseSession"] = null;
                List<Sell_OrderInHouseService> cars = new List<Sell_OrderInHouseService>();
                this.gvList.DataSource = cars;
                this.gvList.DataBind();
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {



        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                Session["OrderInHouseSession"] = e.CommandArgument;
                Response.Write("<script>window.close();window.opener=null;</script>"); 
            }
        }
    }
}
