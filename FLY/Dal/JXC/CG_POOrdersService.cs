using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Model.JXC;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;


namespace VAN_OA.Dal.JXC
{
    public class CG_POOrdersService
    {
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.CG_POOrders model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Id != null)
            {
                strSql1.Append("Id,");
                strSql2.Append("" + model.Id + ",");
            }
            //if (model.Time != null)
            //{
            //    strSql1.Append("Time,");
            //    strSql2.Append("'" + model.Time + "',");
            //}
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.InvName != null)
            {
                strSql1.Append("InvName,");
                strSql2.Append("'" + model.InvName + "',");
            }
            if (model.Num != null)
            {
                strSql1.Append("Num,");
                strSql2.Append("" + model.Num + ",");
            }
            if (model.Unit != null)
            {
                strSql1.Append("Unit,");
                strSql2.Append("'" + model.Unit + "',");
            }
            if (model.CostPrice != null)
            {
                strSql1.Append("CostPrice,");
                strSql2.Append("" + model.CostPrice + ",");
            }
            if (model.SellPrice != null)
            {
                strSql1.Append("SellPrice,");
                strSql2.Append("" + model.SellPrice + ",");
            }
            if (model.OtherCost != null)
            {
                strSql1.Append("OtherCost,");
                strSql2.Append("" + model.OtherCost + ",");
            }
            if (model.ToTime != null)
            {
                strSql1.Append("ToTime,");
                strSql2.Append("'" + model.ToTime + "',");
            }
            if (model.Profit != null)
            {
                strSql1.Append("Profit,");
                strSql2.Append("" + model.Profit + ",");
            }

            if (model.GoodId != null)
            {
                strSql1.Append("GoodId,");
                strSql2.Append("" + model.GoodId + ",");
            }
            if (model.DetailRemark != null)
            {
                strSql1.Append("DetailRemark,");
                strSql2.Append("'" + model.DetailRemark + "',");
            }
            
            strSql.Append("insert into CG_POOrders(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");

            int result;
            objCommand.CommandText = strSql.ToString();
            object obj = objCommand.ExecuteScalar();
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(VAN_OA.Model.JXC.CG_POOrders model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CG_POOrders set ");
            //if (model.Id != null)
            //{
            //    strSql.Append("Id=" + model.Id + ",");
            //}
            if (model.Time != null)
            {
                strSql.Append("Time='" + model.Time + "',");
            }
            if (model.GuestName != null)
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            if (model.InvName != null)
            {
                strSql.Append("InvName='" + model.InvName + "',");
            }
            if (model.Num != null)
            {
                strSql.Append("Num=" + model.Num + ",");
            }
            if (model.Unit != null)
            {
                strSql.Append("Unit='" + model.Unit + "',");
            }
            if (model.CostPrice != null)
            {
                strSql.Append("CostPrice=" + model.CostPrice + ",");
            }
            if (model.SellPrice != null)
            {
                strSql.Append("SellPrice=" + model.SellPrice + ",");
            }
            if (model.OtherCost != null)
            {
                strSql.Append("OtherCost=" + model.OtherCost + ",");
            }
            //if (model.ToTime != null)
            //{
            //    strSql.Append("ToTime='" + model.ToTime + "',");
            //}
            //else
            //{
            //    strSql.Append("ToTime= null ,");
            //}
            if (model.Profit != null)
            {
                strSql.Append("Profit=" + model.Profit + ",");
            }
            else
            {
                strSql.Append("Profit= null ,");
            }


            //if (model.GoodId != null)
            //{
            //    strSql.Append("GoodId=" + model.GoodId + ",");
            //}
            //else
            //{
            //    strSql.Append("GoodId= null ,");
            //}
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Ids=" + model.Ids + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        //public void Delete(int ids)
        //{

        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("delete from CG_POOrders ");
        //    strSql.Append(" where Ids=" + ids + "");
        //    DBHelp.ExeCommand(strSql.ToString());
        //}

        /// <summary>
        /// 删除一条数据
        /// </summary>
        //public void DeleteByIds(string ids ,SqlCommand objCommand)
        //{

        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("delete from CG_POOrders ");
        //    strSql.Append(" where Ids in(" + ids + ")");
        //    objCommand.CommandText = strSql.ToString();
        //    objCommand.ExecuteNonQuery();
        //}
        /// 删除一条数据
        /// </summary>
        //public void DeleteById(int id)
        //{

        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("delete from CG_POOrders ");
        //    strSql.Append(" where Id=" + id + "");
        //    DBHelp.ExeCommand(strSql.ToString());
        //}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.CG_POOrders GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Ids,CG_POOrders.Id,Time,GuestName,InvName,Num,Unit,CostPrice,SellPrice,OtherCost,ToTime,Profit,CG_POOrders.GoodId,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName");
            strSql.Append(" from CG_POOrders left join TB_Good on TB_Good.GoodId=CG_POOrders.GoodId  ");
            strSql.Append(" where Ids=" + Ids + "");
            VAN_OA.Model.JXC.CG_POOrders model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind(dataReader);
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 获取项目发票单剩余金额
        /// </summary>
        /// <returns></returns>
        public decimal GetPOOrder_FPTotal_Out(string PONo, object fpMailId)
        {
            string sql = string.Format(@"SELECT NEWTABLE1.POTotal- ISNULL(NEWTABLE2.fpTotal,0)-isnull(TuiTotal,0) AS WEITotals FROM (
select PONo, sum(SellTotal) AS POTotal from Sell_OrderOutHouse  where Status='通过' and PONo='{0}' group by PONo
) AS NEWTABLE1
LEFT JOIN
(
select PONo, sum(isnull(Total,0))  as fpTotal from Sell_OrderFP where Status<>'不通过' and PONo='{0}' and id<>{1}
group by PONo
)
AS NEWTABLE2
ON NEWTABLE1.PONo=NEWTABLE2.PONo 
left join 
(
select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  and PONo='{0}' 
group by PONo
)
as newtable3
on newtable1.PONo= newtable3.PONo", PONo, fpMailId);
            object obj = DBHelp.ExeScalar(sql);
            if (obj is DBNull)
            {
                return 0;
            }
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 获取项目发票单剩余金额
        /// </summary>
        /// <returns></returns>
        public decimal GetPOOrder_FPTotal(string PONo,object fpMailId)
        {
            string sql = string.Format(@"SELECT NEWTABLE1.POTotal- ISNULL(NEWTABLE2.fpTotal,0)-isnull(TuiTotal,0) AS WEITotals FROM (
select PONo, sum(POTotal) AS POTotal from CG_POOrder  where Status='通过' and PONo='{0}' group by PONo
) AS NEWTABLE1
LEFT JOIN
(
select PONo, sum(isnull(Total,0))  as fpTotal from Sell_OrderFP where Status<>'不通过' and PONo='{0}' and id<>{1}
group by PONo
)
AS NEWTABLE2
ON NEWTABLE1.PONo=NEWTABLE2.PONo 
left join 
(
select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  and PONo='{0}' 
group by PONo
)
as newtable3
on newtable1.PONo= newtable3.PONo",PONo,fpMailId);
            object obj = DBHelp.ExeScalar(sql);
            if (obj is DBNull)
            {
                return 0;
            }
            return Convert.ToDecimal(obj);
        }

        /// <summary>
        /// 获取所有尚未开发票的项目
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<VAN_OA.Model.JXC.CG_POOrdersFP> GetListArrayToFps_InSell(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT Sell_OrderInHouse.PONo,Sell_OrderInHouse.POName,Sell_OrderInHouse.GuestNAME as Supplier,
0 as POTotal, 0 as fpTotal,0 as WEITotals,Ids,
Sell_OrderInHouses.Id,RuTime,TB_Good.GoodName as InvName,GoodNum as Num,TB_Good.GoodUnit as Unit,
GoodPrice as CostPrice,GoodSellPrice as SellPrice,Sell_OrderInHouses.GooId,GoodNo,GoodName,GoodSpec,GoodModel,
GoodUnit,GoodTypeSmName from Sell_OrderInHouse  
left join Sell_OrderInHouses on Sell_OrderInHouses.Id=Sell_OrderInHouse.Id 
left join TB_Good on TB_Good.GoodId=Sell_OrderInHouses.GooId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Sell_OrderInHouse.PONo desc ");
            List<VAN_OA.Model.JXC.CG_POOrdersFP> list = new List<VAN_OA.Model.JXC.CG_POOrdersFP>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.CG_POOrdersFP model = new VAN_OA.Model.JXC.CG_POOrdersFP();
                        object ojb;
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();

                        ojb = dataReader["POTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["fpTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.fpTotal = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["WEITotals"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.WEITotals = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["Ids"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Ids = (int)ojb;
                        }
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        ojb = dataReader["RuTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Time = (DateTime)ojb;
                        }
                        model.GuestName = dataReader["Supplier"].ToString();
                        model.InvName = dataReader["InvName"].ToString();
                        ojb = dataReader["Num"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Num = (decimal)ojb;
                        }
                        model.Unit = dataReader["Unit"].ToString();
                        ojb = dataReader["CostPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CostPrice = (decimal)ojb;
                        }


                        model.CostTotal = model.CostPrice * model.Num;
                        ojb = dataReader["SellPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SellPrice = (decimal)ojb;
                        }

                        model.SellTotal = model.SellPrice * model.Num;
                        //ojb = dataReader["OtherCost"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.OtherCost = (decimal)ojb;
                        //}

                        //model.YiLiTotal = model.SellTotal - model.CostTotal - model.OtherCost;

                        //ojb = dataReader["ToTime"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.ToTime = (DateTime)ojb;
                        //}
                        //ojb = dataReader["Profit"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.Profit = (decimal)ojb;
                        //}
                        //if (model.SellTotal != 0)
                        //{
                        //    model.Profit = model.YiLiTotal / model.SellTotal * 100;
                        //}
                        //else if (model.YiLiTotal != 0)
                        //{
                        //    model.Profit = -100;
                        //}
                        //else
                        //{
                        //    model.Profit = 0;
                        //}


                        ojb = dataReader["GooId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodId = Convert.ToInt32(ojb);
                        }

                        ojb = dataReader["GoodNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNo = ojb.ToString();
                        }
                        ojb = dataReader["GoodName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodName = ojb.ToString();
                        }
                        ojb = dataReader["GoodSpec"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodSpec = ojb.ToString();
                        }

                        ojb = dataReader["GoodModel"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Good_Model = ojb.ToString();
                        }
                        ojb = dataReader["GoodUnit"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodUnit = ojb.ToString();
                        }

                        ojb = dataReader["GoodTypeSmName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodTypeSmName = ojb.ToString();
                        }



                        list.Add(model);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取所有尚未开发票的项目
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<VAN_OA.Model.JXC.CG_POOrdersFP> GetListArrayToFps_Out(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT SellFP_Out_View.PONo,Sell_OrderOutHouse.POName,Sell_OrderOutHouse.Supplier,
SellFP_Out_View.POTotal,SellFP_Out_View.fpTotal,SellFP_Out_View.WEITotals,Ids,
Sell_OrderOutHouses.Id,RuTime,TB_Good.GoodName as InvName,GoodNum as Num,TB_Good.GoodUnit as Unit,
GoodPrice as CostPrice,GoodSellPrice as SellPrice,Sell_OrderOutHouses.GooId,GoodNo,GoodName,GoodSpec,GoodModel,
GoodUnit,GoodTypeSmName from SellFP_Out_View 
left join Sell_OrderOutHouse on SellFP_Out_View.PONo=Sell_OrderOutHouse.PONo   
left join Sell_OrderOutHouses on Sell_OrderOutHouses.Id=Sell_OrderOutHouse.Id 
left join TB_Good on TB_Good.GoodId=Sell_OrderOutHouses.GooId  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Sell_OrderOutHouse.PONo desc ");
            List<VAN_OA.Model.JXC.CG_POOrdersFP> list = new List<VAN_OA.Model.JXC.CG_POOrdersFP>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.CG_POOrdersFP model = new VAN_OA.Model.JXC.CG_POOrdersFP();
                        object ojb;
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();

                        ojb = dataReader["POTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["fpTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.fpTotal = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["WEITotals"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.WEITotals = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["Ids"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Ids = (int)ojb;
                        }
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        ojb = dataReader["RuTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Time = (DateTime)ojb;
                        }
                        model.GuestName = dataReader["Supplier"].ToString();
                        model.InvName = dataReader["InvName"].ToString();
                        ojb = dataReader["Num"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Num = (decimal)ojb;
                        }
                        model.Unit = dataReader["Unit"].ToString();
                        ojb = dataReader["CostPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CostPrice = (decimal)ojb;
                        }


                        model.CostTotal = model.CostPrice * model.Num;
                        ojb = dataReader["SellPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SellPrice = (decimal)ojb;
                        }

                        model.SellTotal = model.SellPrice * model.Num;
                        //ojb = dataReader["OtherCost"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.OtherCost = (decimal)ojb;
                        //}

                        //model.YiLiTotal = model.SellTotal - model.CostTotal - model.OtherCost;

                        //ojb = dataReader["ToTime"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.ToTime = (DateTime)ojb;
                        //}
                        //ojb = dataReader["Profit"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.Profit = (decimal)ojb;
                        //}
                        //if (model.SellTotal != 0)
                        //{
                        //    model.Profit = model.YiLiTotal / model.SellTotal * 100;
                        //}
                        //else if (model.YiLiTotal != 0)
                        //{
                        //    model.Profit = -100;
                        //}
                        //else
                        //{
                        //    model.Profit = 0;
                        //}


                        ojb = dataReader["GooId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodId = Convert.ToInt32(ojb);
                        }

                        ojb = dataReader["GoodNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNo = ojb.ToString();
                        }
                        ojb = dataReader["GoodName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodName = ojb.ToString();
                        }
                        ojb = dataReader["GoodSpec"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodSpec = ojb.ToString();
                        }

                        ojb = dataReader["GoodModel"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Good_Model = ojb.ToString();
                        }
                        ojb = dataReader["GoodUnit"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodUnit = ojb.ToString();
                        }

                        ojb = dataReader["GoodTypeSmName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodTypeSmName = ojb.ToString();
                        }



                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有尚未开发票的项目
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<VAN_OA.Model.JXC.CG_POOrdersFP> GetListArrayToFps(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT SellFP_View.PONo,CG_POOrder.POName,CG_POOrder.GuestName,SellFP_View.POTotal,SellFP_View.fpTotal,SellFP_View.WEITotals,Ids,CG_POOrders.Id,Time,InvName,Num,Unit,CostPrice,SellPrice,OtherCost,ToTime,Profit,CG_POOrders.GoodId,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName");
            strSql.Append(" from SellFP_View left join CG_POOrder on SellFP_View.PONo=CG_POOrder.PONo  ");
            strSql.Append(" left join CG_POOrders on CG_POOrders.Id=CG_POOrder.Id left join TB_Good on TB_Good.GoodId=CG_POOrders.GoodId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CG_POOrder.PONo desc ");
            List<VAN_OA.Model.JXC.CG_POOrdersFP> list = new List<VAN_OA.Model.JXC.CG_POOrdersFP>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.CG_POOrdersFP model = new VAN_OA.Model.JXC.CG_POOrdersFP();
                        object ojb;
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();

                        ojb = dataReader["POTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal =Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["fpTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.fpTotal =Convert.ToDecimal( ojb);
                        }

                        ojb = dataReader["WEITotals"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.WEITotals = Convert.ToDecimal( ojb);
                        }

                        ojb = dataReader["Ids"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Ids = (int)ojb;
                        }
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        ojb = dataReader["Time"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Time = (DateTime)ojb;
                        }
                        model.GuestName = dataReader["GuestName"].ToString();
                        model.InvName = dataReader["InvName"].ToString();
                        ojb = dataReader["Num"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Num = (decimal)ojb;
                        }
                        model.Unit = dataReader["Unit"].ToString();
                        ojb = dataReader["CostPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CostPrice = (decimal)ojb;
                        }


                        model.CostTotal = model.CostPrice * model.Num;
                        ojb = dataReader["SellPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SellPrice = (decimal)ojb;
                        }

                        model.SellTotal = model.SellPrice * model.Num;
                        ojb = dataReader["OtherCost"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.OtherCost = (decimal)ojb;
                        }

                        model.YiLiTotal = model.SellTotal - model.CostTotal - model.OtherCost;

                        ojb = dataReader["ToTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ToTime = (DateTime)ojb;
                        }
                        ojb = dataReader["Profit"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Profit = (decimal)ojb;
                        }
                        if (model.SellTotal != 0)
                        {
                            model.Profit = model.YiLiTotal / model.SellTotal * 100;
                        }
                        else if (model.YiLiTotal != 0)
                        {
                            model.Profit = -100;
                        }
                        else
                        {
                            model.Profit = 0;
                        }


                        ojb = dataReader["GoodId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodId = Convert.ToInt32(ojb);
                        }

                        ojb = dataReader["GoodNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNo = ojb.ToString();
                        }
                        ojb = dataReader["GoodName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodName = ojb.ToString();
                        }
                        ojb = dataReader["GoodSpec"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodSpec = ojb.ToString();
                        }

                        ojb = dataReader["GoodModel"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Good_Model = ojb.ToString();
                        }
                        ojb = dataReader["GoodUnit"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodUnit = ojb.ToString();
                        }

                        ojb = dataReader["GoodTypeSmName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodTypeSmName = ojb.ToString();
                        }

                       

                        list.Add(model);
                    }
                }
            }
            return list;
        }


        public List<VAN_OA.Model.JXC.CG_POOrdersFP> GetListArrayToFpsAndUpdatePoStatue(string poNo,string state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT pono ");
            strSql.Append(" from SellFP_View_Count  ");
            strSql.AppendFormat(" where pono='{0}'" ,poNo);
            
            List<VAN_OA.Model.JXC.CG_POOrdersFP> list = new List<VAN_OA.Model.JXC.CG_POOrdersFP>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                object obj = objCommand.ExecuteScalar();
                if ((obj is DBNull) || obj == null)
                {
                    //CG_POOrderService.StaticUpdatePOStatus(poNo, objCommand, CG_POOrder.POStatue3);
                    string sql = string.Format("update CG_POOrder set POStatue3='{0}' where PONo='{1}'", CG_POOrder.ConPOStatue3, poNo);
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                }
                else
                {
                    string sql = string.Format("update CG_POOrder set POStatue3='{0}' where PONo='{1}'","", poNo);
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                }
//                if (state == "通过")
//                {
//                    string sql = string.Format(@"if exists(select * from Sell_OrderFPBack where Status='通过' and PONo='{1}')
//begin update CG_POOrder set POStatue6='{0}' where PONo='{1}' end", "", CG_POOrder.ConPOStatue6_1, poNo);
//                    objCommand.CommandText = sql;
//                    objCommand.ExecuteNonQuery();
//                }
                 
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.CG_POOrders> GetListArray(string strWhere)
        {
            
            //create view CG_POOrders_Cai_POOrders_View
            //as

            //select * from CG_POOrders 
            //left join 
            //(
            //select  CG_POOrdersId,SUM(Num) as totalOrderNum from CAI_POOrders 
            //where CG_POOrdersId<>0  and  id in (select allE_id from tb_EForm where proId in (
            //select pro_Id from A_ProInfo where pro_Type='采购订单') and state<>'不通过')
            //group by CG_POOrdersId
            //)
            //as newtable on CG_POOrders.Ids=newtable.CG_POOrdersId where (CG_POOrders.Num>newtable.totalOrderNum or totalOrderNum is null)
            //and id in (select allE_id from tb_EForm where proId in (
            //select pro_Id from A_ProInfo where pro_Type='项目订单') and state='通过')


            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select Ids,Id,Time,GuestName,InvName,Num,Unit,CostPrice,SellPrice,OtherCost,ToTime,Profit ");
            //strSql.Append(" FROM CG_POOrders ");
            strSql.Append(" SELECT DetailRemark,Ids,CG_POOrders.Id,Time,GuestName,InvName,Num,Unit,CostPrice,SellPrice,OtherCost,ToTime,Profit,CG_POOrders.GoodId,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName");
            strSql.Append(" from CG_POOrders left join TB_Good on TB_Good.GoodId=CG_POOrders.GoodId  ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CG_POOrders> list = new List<VAN_OA.Model.JXC.CG_POOrders>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                      
                        model.DetailRemark = dataReader["DetailRemark"].ToString();
                        
                        list.Add(model);
                    }
                }
            }
            return list;
        }



        public List<VAN_OA.Model.JXC.CG_POOrders> GetListArrayToList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT IFZhui,Ids,CG_POOrders.Id,Time,CG_POOrders.GuestName,InvName,Num,Unit,CostPrice,SellPrice,OtherCost,ToTime,Profit,CG_POOrders.GoodId,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,Status,PODate,ProNo,PORemark ");
            strSql.Append(" from CG_POOrders left join CG_POOrder on CG_POOrders.id=CG_POOrder.id left join TB_Good on TB_Good.GoodId=CG_POOrders.GoodId  ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by ProNo desc");
            List<VAN_OA.Model.JXC.CG_POOrders> list = new List<VAN_OA.Model.JXC.CG_POOrders>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        if (Convert.ToInt32(dataReader["IFZhui"]) == 0)
                        {
                            model.Type = "原";
                        }
                        else
                        {
                            model.Type = "追";
                        }
                        model.States = dataReader["Status"].ToString();

                        model.PODate = Convert.ToDateTime(dataReader["PODate"]);
                        model.MyProNo = dataReader["ProNo"].ToString();
                        var ojb = dataReader["PORemark"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PORemark =ojb.ToString();
                        }                     
                        
                        list.Add(model);
                    }
                }
            }
            return list;
        }




        /// <summary>
        /// 获取所有审批通过 而且未完成订单的所有项目订单
        /// </summary>
        public List<VAN_OA.Model.JXC.CG_POOrders> GetListCG_POOrders_Cai_POOrders_View(string strWhere)
        {

            //create view CG_POOrders_Cai_POOrders_View
            //as

            //select * from CG_POOrders 
            //left join 
            //(
            //select  CG_POOrdersId,SUM(Num) as totalOrderNum from CAI_POOrders 
            //where CG_POOrdersId<>0  and  id in (select allE_id from tb_EForm where proId in (
            //select pro_Id from A_ProInfo where pro_Type='采购订单') and state<>'不通过')
            //group by CG_POOrdersId
            //)
            //as newtable on CG_POOrders.Ids=newtable.CG_POOrdersId where (CG_POOrders.Num>newtable.totalOrderNum or totalOrderNum is null)
            //and id in (select allE_id from tb_EForm where proId in (
            //select pro_Id from A_ProInfo where pro_Type='项目订单') and state='通过')


            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select Ids,Id,Time,GuestName,InvName,Num,Unit,CostPrice,SellPrice,OtherCost,ToTime,Profit ");
            //strSql.Append(" FROM CG_POOrders ");
            strSql.Append(" SELECT GoodAreaNumber,Ids,CG_POOrders_Cai_POOrders_View.Id,Time,GuestName,InvName,Num,Unit,CostPrice,SellPrice,OtherCost,ToTime,Profit,CG_POOrders_Cai_POOrders_View.GoodId,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,totalOrderNum");
            strSql.Append(" from CG_POOrders_Cai_POOrders_View left join TB_Good on TB_Good.GoodId=CG_POOrders_Cai_POOrders_View.GoodId  ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CG_POOrders> list = new List<VAN_OA.Model.JXC.CG_POOrders>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.CG_POOrders model = new VAN_OA.Model.JXC.CG_POOrders();
                        object ojb;
                        ojb = dataReader["Ids"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Ids = (int)ojb;
                        }
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        ojb = dataReader["Time"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Time = (DateTime)ojb;
                        }
                        model.GuestName = dataReader["GuestName"].ToString();
                        model.InvName = dataReader["InvName"].ToString();
                        model.GoodAreaNumber = dataReader["GoodAreaNumber"].ToString();
                        ojb = dataReader["Num"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Num = (decimal)ojb;
                        }

                        object obj = dataReader["totalOrderNum"];
                        if (obj != null && obj != DBNull.Value)
                        {
                            model.HadTotalNum = (decimal)obj;
                        }

                        model.ResultTotalNum = model.Num - model.HadTotalNum;


                        model.Unit = dataReader["Unit"].ToString();
                        ojb = dataReader["CostPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CostPrice = (decimal)ojb;
                        }


                        model.CostTotal = model.CostPrice * model.ResultTotalNum;
                        ojb = dataReader["SellPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SellPrice = (decimal)ojb;
                        }

                        model.SellTotal = model.SellPrice * model.ResultTotalNum;
                        ojb = dataReader["OtherCost"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.OtherCost = (decimal)ojb;
                        }

                        model.YiLiTotal = model.SellTotal - model.CostTotal - model.OtherCost;

                        ojb = dataReader["ToTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ToTime = (DateTime)ojb;
                        }
                        ojb = dataReader["Profit"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Profit = (decimal)ojb;
                        }
                        if (model.SellTotal != 0)
                        {
                            model.Profit = model.YiLiTotal / model.SellTotal * 100;
                        }
                        else if (model.YiLiTotal != 0)
                        {
                            model.Profit = -100;
                        }
                        else
                        {
                            model.Profit = 0;
                        }


                        ojb = dataReader["GoodId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodId = Convert.ToInt32(ojb);
                        }

                        ojb = dataReader["GoodNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNo = ojb.ToString();
                        }
                        ojb = dataReader["GoodName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodName = ojb.ToString();
                        }
                        ojb = dataReader["GoodSpec"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodSpec = ojb.ToString();
                        }

                        ojb = dataReader["GoodModel"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Good_Model = ojb.ToString();
                        }
                        ojb = dataReader["GoodUnit"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodUnit = ojb.ToString();
                        }

                        ojb = dataReader["GoodTypeSmName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodTypeSmName = ojb.ToString();
                        }

                       
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.CG_POOrders ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.CG_POOrders model = new VAN_OA.Model.JXC.CG_POOrders();
            object ojb;
            ojb = dataReader["Ids"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ids = (int)ojb;
            }
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["Time"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Time = (DateTime)ojb;
            }
            model.GuestName = dataReader["GuestName"].ToString();
            model.InvName = dataReader["InvName"].ToString();
            ojb = dataReader["Num"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Num = (decimal)ojb;
            }
            model.Unit = dataReader["Unit"].ToString();
            ojb = dataReader["CostPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CostPrice = (decimal)ojb;
            }


            model.CostTotal = model.CostPrice * model.Num;
            ojb = dataReader["SellPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellPrice = (decimal)ojb;
            }

            model.SellTotal = model.SellPrice * model.Num;
            ojb = dataReader["OtherCost"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OtherCost = (decimal)ojb;
            }

            model.YiLiTotal = model.SellTotal-model.CostTotal - model.OtherCost;
           
            ojb = dataReader["ToTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ToTime = (DateTime)ojb;
            }
            ojb = dataReader["Profit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Profit = (decimal)ojb;
            }
            if (model.SellTotal != 0)
            {
                model.Profit = model.YiLiTotal / model.SellTotal * 100;
            }
            else if (model.YiLiTotal != 0)
            {
                model.Profit = -100;
            }
            else
            {
                model.Profit =0;
            }


            ojb = dataReader["GoodId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodId =Convert.ToInt32( ojb);
            }

            ojb = dataReader["GoodNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNo =  ojb.ToString();
            }
            ojb = dataReader["GoodName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodName = ojb.ToString();
            }
            ojb = dataReader["GoodSpec"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodSpec = ojb.ToString();
            }

            ojb = dataReader["GoodModel"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Good_Model = ojb.ToString();
            }
            ojb = dataReader["GoodUnit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodUnit = ojb.ToString();
            }

            ojb = dataReader["GoodTypeSmName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTypeSmName = ojb.ToString();
            }
            return model;
        }


        public Dictionary<int, decimal> GetPOGoodInfo(string poNo)
        {
            Dictionary<int, decimal> allGoods = new Dictionary<int, decimal>();
            string sql = string.Format("select GoodId,SellPrice from CG_POOrder left join  CG_POOrders on CG_POOrder.Id=CG_POOrders.Id where PONo='{0}' and STATUS='通过'",
                poNo);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int goodId = 0;
                        decimal SellPrice = 0;
                        object ojb;
                        ojb = dataReader["GoodId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            goodId = (int)ojb;
                        }

                        ojb = dataReader["SellPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            SellPrice = (decimal)ojb;
                        }
                        if (!allGoods.ContainsKey(goodId))
                        {
                            allGoods.Add(goodId, SellPrice);
                        }
                    }
                }
            }
            return allGoods;
        }

    }
}
