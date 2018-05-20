using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;

namespace VAN_OA.JXC
{
    public partial class DeleteSellInInfo : BasePage
    {
        Sell_OrderInHouseService POSer = new Sell_OrderInHouseService();
        Sell_OrderInHousesService ordersSer = new Sell_OrderInHousesService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //主单
                List<Sell_OrderInHouse> pOOrderList = new List<Sell_OrderInHouse>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //子单
                List<Sell_OrderInHouses> orders = new List<Sell_OrderInHouses>();
                gvList.DataSource = orders;
                gvList.DataBind();
            }
        }

        protected void btnSub_Click(object sender, EventArgs e)
        {
            if (txtProNo.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写 销售退货单号！');</script>");
                return;
            }
            else
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
            }
            //查询项目信息是否存在
            string sql = string.Format("select Id,PoNo from Sell_OrderInHouse where ProNo='{0}' and status='通过'", txtProNo.Text.Trim());
            var obj = DBHelp.getDataTable(sql);
            if (obj.Rows.Count!=1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('销售退货信息不存在！');</script>");
                return;
            }
            Sell_OrderInHousesService ordersSer = new Sell_OrderInHousesService();
            TB_HouseGoodsService houseGoodsSer = new TB_HouseGoodsService();

            //查询销售退货信息
            List<Sell_OrderInHouses> orders = ordersSer.GetListArray(" 1=1 and Sell_OrderInHouses.id=" + obj.Rows[0]["Id"]);
            sql =string.Format( @"--销售退货还原--25	销售退货
delete from tb_EForm where e_No='{0}' and proId=25;
delete from tb_EForms where e_Id in (select id from tb_EForm where e_No='{0}' and proId=25);
delete from Sell_OrderInHouses where id = (select ID from Sell_OrderInHouse where ProNo='{0}');
delete from Sell_OrderInHouse where ProNo='{0}';",txtProNo.Text.Trim());
             
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {
                    for (int i = 0; i < orders.Count; i++)
                    {
                        if (orders[i].GoodPriceSecond != 0)
                        {
                            houseGoodsSer.OutHouse(orders[i].HouseID, orders[i].GooId, orders[i].GoodNum, orders[i].GoodPriceSecond, objCommand);
                        }
                        else
                        {
                            houseGoodsSer.OutHouse(orders[i].HouseID, orders[i].GooId, orders[i].GoodNum, orders[i].GoodPrice, objCommand);
                        }
                    }
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                    tan.Commit();

                }
                catch (Exception)
                {

                    tan.Rollback();
                    conn.Close();
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除失败!');</script>");
                    return;
                }
                conn.Close();
                string PONO = obj.Rows[0]["PoNo"].ToString();
                Sell_OrderOutHouseService SellOutSer = new Sell_OrderOutHouseService();
                SellOutSer.SellOrderUpdatePoStatus2(PONO);
                new CG_POOrdersService().GetListArrayToFpsAndUpdatePoStatue(PONO, "通过");
                new CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus(PONO);
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除成功!');</script>");
            }


        }

        private void Show()
        {
            if (txtProNo.Text.Trim()!=""&&CheckProNo(txtProNo.Text) == false)
            {
                return;
            }
            string  sql = string.Format(" Status='通过' and ProNo = '{0}'", txtProNo.Text.Trim());
            
            List<Sell_OrderInHouse> pOOrderList = this.POSer.GetListArray(sql);

            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

            if (pOOrderList.Count == 0)
            {
                //子单
                List<Sell_OrderInHouses> orders = new List<Sell_OrderInHouses>();
                gvList.DataSource = orders;
                gvList.DataBind();
            }
            else
            {
                List<Sell_OrderInHouses> orders = ordersSer.GetListArray(" 1=1 and Sell_OrderInHouses.id=" + pOOrderList[0].Id);

                ViewState["Orders"] = orders;
                gvList.DataSource = orders;
                gvList.DataBind();
            }





        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Show();
        }
        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }
        Sell_OrderInHouses SumOrders = new Sell_OrderInHouses();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                Sell_OrderInHouses model = e.Row.DataItem as Sell_OrderInHouses;

                SumOrders.Total += model.Total;
                SumOrders.GoodSellPriceTotal += model.GoodSellPriceTotal;

            }



            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblGoodName") as System.Web.UI.WebControls.Label, "合计");//合计                      
                setValue(e.Row.FindControl("lblTotal") as System.Web.UI.WebControls.Label, SumOrders.Total.ToString());//成本总额    
                setValue(e.Row.FindControl("lblTotal1") as System.Web.UI.WebControls.Label, SumOrders.GoodSellPriceTotal.ToString());//成本总额    
            }

        }


        private void setValue(System.Web.UI.WebControls.Label control, string value)
        {
            control.Text = value;
        }
    }
}
