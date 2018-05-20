using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.ReportForms;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class DioSellOutOrderList : BasePage
    {
        private Sell_OrderOutHouseService POSer = new Sell_OrderOutHouseService();
       

        private void Show()
        {
            string sql = " 1=1 and (CreateUserId=" + Session["currentUserId"];

            sql += string.Format("  or  exists( select TB_GuestTrack.Id from TB_GuestTrack where TB_GuestTrack.IsSpecial=1 and Sell_OrderOutHouse_Sell_OrderInHouse_View.Supplier=TB_GuestTrack.GuestName)) ");
       

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text.Trim());
            }



            if (txtGoodNo1.Text != "" || txtNameOrTypeOrSpec1.Text != "" || txtNameOrTypeOrSpecTwo1.Text != "" || ddlGoodUnit.Text != "全部"||
              txtGoodNum.Text!=""|| txtChenBen.Text!=""|| txtsmallChenBen.Text!=""|| txtBigSellPrice.Text!=""|| txtSmallSellPrice.Text!="")
            {
                string goodInfo = " and ID in (select id from Sell_OrderOutHouses left join TB_Good on Sell_OrderOutHouses.GooId=TB_Good.GoodId where 1=1 ";
                if (txtGoodNo1.Text != "")
                {
                    goodInfo += string.Format(" and TB_Good.GoodNo like '%{0}%'", txtGoodNo1.Text);
                }
                if (txtNameOrTypeOrSpec1.Text != "" && txtNameOrTypeOrSpecTwo1.Text != "")
                {
                    goodInfo += string.Format(" and ((GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%') or (GoodTypeSmName like '%{1}%' or GoodName  like '%{1}%' or GoodSpec like '%{1}%'))",
                       txtNameOrTypeOrSpec1.Text, txtNameOrTypeOrSpecTwo1.Text);
                }
                else if (txtNameOrTypeOrSpec1.Text != "" || txtNameOrTypeOrSpecTwo1.Text != "")
                {
                    var NameOrTypeOrSpec = "";
                    if (txtNameOrTypeOrSpec1.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpec1.Text;
                    if (txtNameOrTypeOrSpecTwo1.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpecTwo1.Text;

                    goodInfo += string.Format(" and (GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%')",
                       NameOrTypeOrSpec);
                }
                if (ddlGoodUnit.Text != "全部")
                {
                    goodInfo += string.Format(" and GoodUnit='{0}'", ddlGoodUnit.Text);
                }

                if (!string.IsNullOrEmpty(txtGoodNum.Text))
                {
                    goodInfo += string.Format(" and GoodNum{0}{1}", ddlFuHao.Text, txtGoodNum.Text);
                }
              
                if (!string.IsNullOrEmpty(txtChenBen.Text))
                {
                    if (CommHelp.VerifesToNum(txtChenBen.Text) == false)
                    {
                        return;
                    }
                    goodInfo += string.Format(" and GoodPrice{0}{1}", ddlFuHao1.Text, txtChenBen.Text);                    
                }
                
                if (!string.IsNullOrEmpty(txtsmallChenBen.Text))
                {
                    if (CommHelp.VerifesToNum(txtsmallChenBen.Text) == false)
                    {
                        return;
                    }
                    goodInfo += string.Format(" and {1}{0}GoodPrice", ddlSmallChenBen.Text, txtsmallChenBen.Text);
                }

                if (!string.IsNullOrEmpty(txtBigSellPrice.Text))
                {
                    if (CommHelp.VerifesToNum(txtBigSellPrice.Text) == false)
                    {
                        return;
                    }
                    goodInfo += string.Format(" and GoodSellPrice{0}{1}", ddlBigSellPrice.Text, txtBigSellPrice.Text);
                }

                if (!string.IsNullOrEmpty(txtSmallSellPrice.Text))
                {
                    if (CommHelp.VerifesToNum(txtSmallSellPrice.Text) == false)
                    {
                        return;
                    }
                    goodInfo += string.Format(" and {1}{0}GoodSellPrice", ddlSmallSellPrice.Text, txtSmallSellPrice.Text);
                }
                goodInfo += ")";
                sql += goodInfo;
            }


            


            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and RuTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and RuTime<='{0} 23:59:59'", txtTo.Text);
            }
            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text.Trim());
            }

            List<Sell_OrderOutHouse> cars = this.POSer.GetListSell_OrderOutHouse_Sell_OrderInHouse_View(sql);
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

                var GoodUnitList = new TB_GoodService().GetAllGoodUnits();
                ddlGoodUnit.Items.Add("全部");
                foreach (var unit in GoodUnitList)
                {
                    ddlGoodUnit.Items.Add(unit);
                }

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
