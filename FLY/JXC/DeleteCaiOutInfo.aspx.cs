using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using System.Data;

namespace VAN_OA.JXC
{
    public partial class DeleteCaiOutInfo : BasePage
    {
        CAI_OrderOutHouseService POSer = new CAI_OrderOutHouseService();
        CAI_OrderOutHousesService ordersSer = new CAI_OrderOutHousesService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //主单
                List<CAI_OrderOutHouse> pOOrderList = new List<CAI_OrderOutHouse>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //子单
                List<CAI_OrderOutHouses> orders = new List<CAI_OrderOutHouses>();
                gvList.DataSource = orders;
                gvList.DataBind();
            }
        }

        protected void btnSub_Click(object sender, EventArgs e)
        {
            if (txtProNo.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写 采购退货单号！');</script>");
                return;
            }
            //查询项目信息是否存在
            string sql = string.Format("select Id,PoNo,HouseID from CAI_OrderOutHouse where ProNo='{0}' and status='通过'", txtProNo.Text);
            var obj = DBHelp.getDataTable(sql);
            if (obj.Rows.Count != 1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('采购退货信息不存在！');</script>");
                return;
            }
            sql = string.Format("SELECT count(id) FROM TB_SupplierInvoice where CaiTuiProNo='{0}' and status='通过'", txtProNo.Text);
            var obj1 = DBHelp.ExeScalar(sql);
            if (Convert.ToInt32(obj1)!=1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('支付负数单不存在！');</script>");
                return;
            }

            // 如果此采购退货单生成的负数的 支付单的支付状态为已支付，结清状态为已结清

            //说明有3种可能 1.事后退货的已抵消 2.此采退是全退货单  3.此采退单导致了全部退货

            //1.修改此采购退货单生成的负数的支付单的状态 为未支付，结清状态为 未结清。

            //2-3 删除基于此入库ID 的所有负数支付单（支付状态为未支付和结清状态为 未结清），修改此采购退货单生成的负数的支付单的状态 为未支付，结清状态为 未结清。

            // 你要区分这两种情况！！

            //4.如果此采购退货单生成的负数的 支付单的支付状态为未支付，结清状态为未结清，由此采购退货单生成的负数的支付单删除即可
            sql = string.Format(@"select TB_SupplierInvoice.Id,Ids,RuIds,RePayClear,IsPayStatus from TB_SupplierInvoices
left join TB_SupplierInvoice on TB_SupplierInvoices.Id=TB_SupplierInvoice.Id where CAITUIPRONO='{0}' and Status='通过'", txtProNo.Text);
            var dt = DBHelp.getDataTable(sql);

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = conn.CreateCommand();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["RePayClear"].ToString() == "1" && dr["IsPayStatus"].ToString() == "2")//已支付  已结清
                    {
                        //检查是否是事后退货的已抵消
                        sql = string.Format(@"select COUNT(*) from TB_TempSupplierInvoice
left join TB_SupplierInvoice on TB_SupplierInvoice.Id=TB_TempSupplierInvoice.SupplierInvoiceId 
where SupplierInvoiceIds={0}  and Status='通过' ", dr["Ids"]);
                        objCommand.CommandText = sql;
                        var result = objCommand.ExecuteScalar();
                        if (Convert.ToInt32(result) > 0)
                        {
                            //修改此采购退货单生成的负数的支付单的状态 为未支付，结清状态为 未结清。
                            sql = "update TB_SupplierInvoices set RePayClear=2,IsPayStatus=0 where ids=" + dr["Ids"];
                            objCommand.CommandText = sql;
                            objCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            //删除基于此入库ID 的所有负数支付单（支付状态为未支付和结清状态为 未结清），
                            //修改此采购退货单生成的负数的支付单的状态 为未支付，结清状态为 未结清
                            sql = "delete from TB_SupplierInvoices where RuIds=" + dr["RuIds"] + " and SupplierInvoiceTotal<0 and RePayClear=2 and IsPayStatus=0";
                            objCommand.CommandText = sql;
                            objCommand.ExecuteNonQuery();

                            sql = "update TB_SupplierInvoices set RePayClear=2,IsPayStatus=0 where ids=" + dr["Ids"];
                            objCommand.CommandText = sql;
                            objCommand.ExecuteNonQuery();
                        }

                    }
                    else if (dr["RePayClear"].ToString() == "2" && dr["IsPayStatus"].ToString() == "0")//未支付  未结清
                    {
                        //如果此采购退货单生成的负数的 支付单的支付状态为未支付，结清状态为未结清，由此采购退货单生成的负数的支付单删除即可
                        sql = "delete from TB_SupplierInvoices where Ids=" + dr["Ids"];
                        objCommand.CommandText = sql;
                        objCommand.ExecuteNonQuery();
                    }
                }
                conn.Close();
            }


            TB_HouseGoodsService houseGoodsSer = new TB_HouseGoodsService();
            CAI_OrderOutHousesService ordersSer = new CAI_OrderOutHousesService();
            List<CAI_OrderOutHouses> orders = ordersSer.GetListArray(" 1=1 and CAI_OrderOutHouses.id=" + obj.Rows[0]["Id"]);

            sql = string.Format(@"delete from tb_EForm where e_No='{0}' and proId=24;
delete from tb_EForms where e_Id in (select id from tb_EForm where e_No='{0}' and proId=24);
delete from CAI_OrderOutHouses where id = (select ID from CAI_OrderOutHouse where ProNo='{0}');
delete from CAI_OrderOutHouse where ProNo='{0}';", txtProNo.Text);

            using (var conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                var objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {
                    for (int i = 0; i < orders.Count; i++)
                    {
                        houseGoodsSer.InHouse(Convert.ToInt32(obj.Rows[0]["HouseID"]), orders[i].GooId, orders[i].GoodNum, orders[i].GoodPrice, objCommand);
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

            }
            //删除没有表体的数据
            sql = "select ProNo from TB_SupplierInvoice where not exists(select Id from TB_SupplierInvoices where TB_SupplierInvoices.Id=TB_SupplierInvoice.Id)";
            var db = DBHelp.getDataTable(sql);

            if (db.Rows.Count > 0)
            {
                using (var conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlTransaction tan = conn.BeginTransaction();
                    var objCommand = conn.CreateCommand();
                    objCommand.Transaction = tan;
                    try
                    {

                        foreach (DataRow dr in db.Rows)
                        {
                            sql = string.Format(@" delete from tb_EForm where e_No='{0}' and proId=31;
delete from tb_EForms where e_Id in (select id from tb_EForm where e_No='{0}' and proId=31);
delete from TB_SupplierInvoice where ProNo='{0}';
delete from TB_SupplierInvoices where Id = (select Id from TB_SupplierInvoice where ProNo='{0}');", dr["ProNo"]);
                            objCommand.CommandText = sql;
                            objCommand.ExecuteNonQuery();
                        }
                        tan.Commit();
                        conn.Close();
                    }
                    catch (Exception)
                    {

                        tan.Rollback();
                        conn.Close();
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除失败!');</script>");
                        return;
                    }

                }
            }

            string PONO = obj.Rows[0]["PoNo"].ToString();
            Sell_OrderOutHouseService SellOutSer = new Sell_OrderOutHouseService();
            SellOutSer.SellOrderUpdatePoStatus2(PONO);
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除成功!');</script>");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Show();
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMain.PageIndex = e.NewPageIndex;
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

        CAI_OrderInHouses SumOrders = new CAI_OrderInHouses();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                CAI_OrderOutHouses model = e.Row.DataItem as CAI_OrderOutHouses;

                SumOrders.Total += model.Total;


            }



            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblGoodName") as Label, "合计");//合计

                setValue(e.Row.FindControl("lblTotal") as Label, SumOrders.Total.ToString());//成本总额    
            }

        }

        private void setValue(Label control, string value)
        {
            control.Text = value;
        }


        private void Show()
        {
            string sql = string.Format(" Status='通过' and ProNo = '{0}'", txtProNo.Text);
          
            List<CAI_OrderOutHouse> pOOrderList = this.POSer.GetListArray(sql);

            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();


            if (pOOrderList.Count == 0)
            {
                //子单
                List<CAI_OrderOutHouses> orders = new List<CAI_OrderOutHouses>();
                gvList.DataSource = orders;
                gvList.DataBind();
            }
            else
            {
                List<CAI_OrderOutHouses> orders = ordersSer.GetListArray(" CAI_OrderOutHouses.id=" + pOOrderList[0].Id);
                ViewState["Orders"] = orders;
                gvList.DataSource = orders;
                gvList.DataBind();

            }


        }

    }
}
