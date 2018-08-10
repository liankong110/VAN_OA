using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace VAN_OA.Dal.JXC
{
    public class TB_HouseGoodsService
    {
        /// <summary>
        /// 判断是否为特殊商品 并查询库存是否有库存
        /// </summary>
        /// <returns></returns>
        public bool CheckGoodInHouse(int goodId)
        {

            string sql = string.Format("select count(*) from TB_HouseGoods  where goodId in (select GoodId from TB_Good where IfSpec=1 and GoodId={0})", goodId);
            if (Convert.ToInt32(DBHelp.ExeScalar(sql))>0)
            {
                return true;
            }
            return false;
        }

        public void InHouse(int houseId, int goodId, decimal num, decimal price, DbCommand objCommand)
        {
            string sql = string.Format(@"declare @IfExist int
            select @IfExist=isnull(count(*),0) from TB_HouseGoods where HouseId={0} and GoodId={1}
            if(@ifExist<=0)
            begin 
            	insert into TB_HouseGoods values({0},{1},{2},{3})
            end
            else
            begin
            	update TB_HouseGoods set GoodNum=GoodNum+{2},GoodAvgPrice=({2}*{3}+GoodNum*GoodAvgPrice)/({2}+abs(GoodNum))
                    where HouseId={0} and GoodId={1}
            end ", houseId, goodId, num, price);

            objCommand.CommandText = sql;
            objCommand.ExecuteNonQuery();
        }




        public void OutHouse(int houseId, int goodId, decimal num, decimal price, DbCommand objCommand)
        {
            string sql = string.Format(@"declare @Num decimal(18,6)
                     select @Num =GoodNum from TB_HouseGoods  where HouseId={0} and GoodId={1}
                    if(@Num>0)
                    begin
                        if(@Num-{2}=0)
                        delete from TB_HouseGoods where HouseId={0} and GoodId={1}
                        else
	                    update TB_HouseGoods set GoodNum=GoodNum-{2},GoodAvgPrice=(GoodNum*GoodAvgPrice-{2}*{3})/(abs(GoodNum)-{2})
                            where HouseId={0} and GoodId={1}
                    end
                    else
                    begin
                    update TB_HouseGoods set GoodNum={2},GoodAvgPrice={3}
                            where  HouseId={0} and GoodId={1}
                    end ", houseId, goodId, num, price);

            objCommand.CommandText = sql;
            objCommand.ExecuteNonQuery();
        }




        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.TB_HouseGoods> GetListArray_ToDio(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isnull(TB_HouseGoods.id,0) as id,isnull(HouseId,0) as HouseId,isnull(TB_Good.GoodId,0) as GoodId,isnull(GoodAvgPrice,0) as GoodAvgPrice, isnull(GoodNum,0) as GoodNum ,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,isnull(houseName,'') as houseName");
            strSql.Append(" FROM  TB_Good left join TB_HouseGoods on TB_Good.GoodId=TB_HouseGoods.GoodId  left join TB_HouseInfo on TB_HouseInfo.id=HouseId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.TB_HouseGoods> list = new List<VAN_OA.Model.JXC.TB_HouseGoods>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind(dataReader));
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.TB_HouseGoods> GetListArray_ToDio_1(string strWhere,string pono)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@" select GoodAreaNumber,isnull(TB_HouseGoods.id,0) as id,isnull(HouseId,0) as HouseId,isnull(TB_Good.GoodId,0) as GoodId,isnull(GoodAvgPrice,0) as GoodAvgPrice, 
 isnull(GoodNum,0) as GoodNum ,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,isnull(houseName,'') as houseName,
 TB1.PONUMS-ISNULL(TB2.OUTNUMS,0) as  LastNums, TB1.SellPrice
  FROM  TB_HouseGoods left join TB_Good on TB_Good.GoodId=TB_HouseGoods.GoodId  left join TB_HouseInfo on TB_HouseInfo.id=HouseId
  left join ( 
 SELECT GoodId,SellPrice,SUM(Num) AS PONUMS  FROM  CG_POOrder left join CG_POOrders on CG_POOrder.Id=CG_POOrders.Id 
  where   CG_POOrder.Status='通过'  AND CG_POOrder.PONo='{0}'
  GROUP BY GoodId,SellPrice ) as  TB1 ON TB1.GoodId=TB_HouseGoods.GoodId
  LEFT JOIN (  
  SELECT GooId,GoodSellPrice,SUM(GoodNum) as OUTNUMS FROM Sell_OrderOutHouse LEFT JOIN Sell_OrderOutHouses ON Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id
  WHERE Status<>'不通过' and PONo='{0}'
  group by GooId,GoodSellPrice ) AS TB2 ON TB1.GoodId=TB2.GooId and TB1.SellPrice=TB2.GoodSellPrice",pono);            
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where  TB1.PONUMS-ISNULL(TB2.OUTNUMS,0)>0 AND " + strWhere);
            }
            
            List<VAN_OA.Model.JXC.TB_HouseGoods> list = new List<VAN_OA.Model.JXC.TB_HouseGoods>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var m=ReaderBind(dataReader);
                        m.PONums = Convert.ToDecimal(dataReader["LastNums"]);
                        m.POGoodPrice = Convert.ToDecimal(dataReader["SellPrice"]);
                        m.GoodAreaNumber = dataReader["GoodAreaNumber"].ToString();
                        list.Add(m);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.TB_HouseGoods> GetListArrayByStocking(string strWhere, string fromDate, string endDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select GoodAreaNumber,TB_HouseGoods.id,HouseId,TB_HouseGoods.GoodId,GoodAvgPrice,GoodNum,
GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,houseName,SellOutNums as OutNums,CaiInNums as InNums,
isnull(CaiInNums1,0)- isnull(SellOutNums1,0)+ isnull(SellInNums1,0)-isnull(CaiOutNums1,0) as Nums ,SumKuXuCai
 FROM TB_Good left join TB_HouseGoods on TB_Good.GoodId=TB_HouseGoods.GoodId
  left join TB_HouseInfo on TB_HouseInfo.id=HouseId
  left join (
  select GooId,ISNULL(sum(GoodNum),0) as SellOutNums from Sell_OrderOutHouse
   left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id
  where Status='通过' and RuTime between '{0}'  and '{1}'   group by Sell_OrderOutHouses.GooId)
  as outTB on outTB.GooId=TB_Good.GoodId
  left join 
  (
    select GooId,ISNULL(sum(GoodNum),0)as CaiInNums from CAI_OrderInHouse
   left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.id
  where Status='通过' and RuTime between '{0}'  and '{1}'
  group by CAI_OrderInHouses.GooId) as inOut on inOut.GooId=TB_Good.GoodId


    left join (
  select GooId,ISNULL(sum(GoodNum),0) as SellOutNums1 from Sell_OrderOutHouse
   left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id
  where Status='通过' and RuTime < '{0}'   group by Sell_OrderOutHouses.GooId)
  as outTB1 on outTB1.GooId=TB_Good.GoodId
  left join 
  (
    select GooId,ISNULL(sum(GoodNum),0)as CaiInNums1 from CAI_OrderInHouse
   left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.id
  where Status='通过' and RuTime < '{0}'  
  group by CAI_OrderInHouses.GooId) as inOut1 on inOut1.GooId=TB_Good.GoodId
      left join (
  select GooId,ISNULL(sum(GoodNum),0) as SellInNums1 from Sell_OrderInHouse
   left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.id
  where Status='通过' and RuTime < '{0}'   group by Sell_OrderInHouses.GooId)
  as sellInTB on sellInTB.GooId=TB_Good.GoodId
  left join 
  (
    select GooId,ISNULL(sum(GoodNum),0)as CaiOutNums1 from CAI_OrderOutHouse
   left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.id
  where Status='通过' and RuTime<'{0}'  
  group by CAI_OrderOutHouses.GooId) as CaiOutTB on CaiOutTB.GooId=TB_Good.GoodId
left join CaiKuXuNumView on CaiKuXuNumView.GoodId=TB_Good.GoodId", fromDate,endDate);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            List<VAN_OA.Model.JXC.TB_HouseGoods> list = new List<VAN_OA.Model.JXC.TB_HouseGoods>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        model.GoodAreaNumber = dataReader["GoodAreaNumber"].ToString();
                        object ojb;
                        ojb = dataReader["Nums"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Nums = (decimal)ojb;
                        }
                        ojb = dataReader["InNums"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InNums = (decimal)ojb;
                        }
                        ojb = dataReader["OutNums"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.OutNums = (decimal)ojb;
                        }
                        ojb = dataReader["SumKuXuCai"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SumKuXuCai = Convert.ToDecimal(ojb);
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public DataTable GetListStocking(string strWhere, string fromDate, string endDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select GoodAreaNumber,TB_HouseGoods.id,HouseId,TB_HouseGoods.GoodId,GoodAvgPrice,GoodNum,
GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,houseName,SellOutNums as OutNums,CaiInNums as InNums,
isnull(CaiInNums1,0)- isnull(SellOutNums1,0)+ isnull(SellInNums1,0)-isnull(CaiOutNums1,0) as Nums
 FROM TB_Good left join TB_HouseGoods on TB_Good.GoodId=TB_HouseGoods.GoodId 
  left join TB_HouseInfo on TB_HouseInfo.id=HouseId
  left join (
  select GooId,ISNULL(sum(GoodNum),0) as SellOutNums from Sell_OrderOutHouse
   left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id
  where Status='通过' and RuTime between '{0}'  and '{1}'   group by Sell_OrderOutHouses.GooId)
  as outTB on outTB.GooId=TB_Good.GoodId
  left join 
  (
    select GooId,ISNULL(sum(GoodNum),0)as CaiInNums from CAI_OrderInHouse
   left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.id
  where Status='通过' and RuTime between '{0}'  and '{1}'
  group by CAI_OrderInHouses.GooId) as inOut on inOut.GooId=TB_Good.GoodId


    left join (
  select GooId,ISNULL(sum(GoodNum),0) as SellOutNums1 from Sell_OrderOutHouse
   left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id
  where Status='通过' and RuTime < '{0}'   group by Sell_OrderOutHouses.GooId)
  as outTB1 on outTB1.GooId=TB_Good.GoodId
  left join 
  (
    select GooId,ISNULL(sum(GoodNum),0)as CaiInNums1 from CAI_OrderInHouse
   left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.id
  where Status='通过' and RuTime < '{0}'  
  group by CAI_OrderInHouses.GooId) as inOut1 on inOut1.GooId=TB_Good.GoodId
      left join (
  select GooId,ISNULL(sum(GoodNum),0) as SellInNums1 from Sell_OrderInHouse
   left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.id
  where Status='通过' and RuTime < '{0}'   group by Sell_OrderInHouses.GooId)
  as sellInTB on sellInTB.GooId=TB_Good.GoodId
  left join 
  (
    select GooId,ISNULL(sum(GoodNum),0)as CaiOutNums1 from CAI_OrderOutHouse
   left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.id
  where Status='通过' and RuTime<'{0}'  
  group by CAI_OrderOutHouses.GooId) as CaiOutTB on CaiOutTB.GooId=TB_Good.GoodId", fromDate, endDate);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }

            return DBHelp.getDataTable(strSql.ToString()); ;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.TB_HouseGoods> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GoodAreaNumber,TB_HouseGoods.id,HouseId,TB_HouseGoods.GoodId,GoodAvgPrice,GoodNum,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,houseName");
            strSql.Append(" FROM TB_HouseGoods left join TB_Good on TB_Good.GoodId=TB_HouseGoods.GoodId  left join TB_HouseInfo on TB_HouseInfo.id=HouseId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.TB_HouseGoods> list = new List<VAN_OA.Model.JXC.TB_HouseGoods>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    int i = 1;
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        model.GoodAreaNumber = dataReader["GoodAreaNumber"].ToString();
                        model.No = i;
                        list.Add(model);
                        i++;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.TB_HouseGoods> GetListArrayToInvoice(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select AllOutTotal, HadInvoice,GoodTotal,GoodAreaNumber,TB_HouseGoods.id,HouseId,TB_HouseGoods.GoodId,GoodAvgPrice,GoodNum,GoodNo,GoodName,GoodSpec,GoodModel,
GoodUnit,GoodTypeSmName,houseName,SumKuXuCai FROM TB_HouseGoods left join TB_Good on TB_Good.GoodId=TB_HouseGoods.GoodId 
 left join TB_HouseInfo on TB_HouseInfo.id=HouseId left join (select GooId,--支付单价/实采金额 *采购单价
sum(SupplierInvoiceTotal) as HadInvoice, 
sum(GoodNum*GoodPrice) as GoodTotal ,-sum(OutTotal) as AllOutTotal
 from CAI_OrderInHouse 
left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id 
left join tb_User on tb_User.id=CAI_OrderInHouse.CreateUserId
left join 
(
--单支付的单子
select RuIds,--sum((SupplierInvoicePrice*SupplierInvoiceNum)/(GoodNum*CaiLastTruePrice)*GoodNum*GoodPrice) as SupplierInvoiceTotal
sum((GoodNum*GoodPrice)*SupplierInvoiceTotal/(GoodNum*CaiLastTruePrice))  as SupplierInvoiceTotal
from TB_SupplierInvoice
left join TB_SupplierInvoices on TB_SupplierInvoice.Id=TB_SupplierInvoices.id
left join CAI_OrderInHouses on CAI_OrderInHouses.Ids=TB_SupplierInvoices.RuIds
where (Status='通过' or (IsYuFu=1 and Status<>'不通过'))  and (
(ActPay>0 ) or
(IsHeBing=1 and SupplierInvoiceTotal<0)  
OR
 (exists (select SupplierInvoiceIds from TB_TempSupplierInvoice where   TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds ))
 ) group by RuIds
) as FuInvoice on FuInvoice.RuIds=CAI_OrderInHouses.Ids
left join 
(
select OrderCheckIds,SUM(GoodPrice*GoodNum) AS OutTotal from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.Id
where Status='通过' GROUP BY OrderCheckIds
) as caiOut ON caiOut.OrderCheckIds=CAI_OrderInHouses.Ids
where status='通过'
group by GooId) as Invoice on Invoice.GooId=TB_HouseGoods.GoodId 
left join CaiKuXuNumView on CaiKuXuNumView.GoodId=TB_HouseGoods.GoodId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.TB_HouseGoods> list = new List<VAN_OA.Model.JXC.TB_HouseGoods>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    int i = 1;
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        model.GoodAreaNumber = dataReader["GoodAreaNumber"].ToString();
                        model.No = i;

                        object ojb;
                        //总入库金额
                        decimal GoodTotal = 0;
                        ojb = dataReader["GoodTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            GoodTotal = Convert.ToDecimal(ojb);
                        }
                       
                        //支付的金额                       
                        ojb = dataReader["HadInvoice"];
                        if (GoodTotal>0&&ojb != null && ojb != DBNull.Value)
                        {
                            model.HadInvoice = Convert.ToDecimal(ojb);                          
                        }
                        //退货的金额
                        decimal AllOutTotal = 0;
                        ojb = dataReader["AllOutTotal"];
                        if (GoodTotal > 0 && ojb != null && ojb != DBNull.Value)
                        {
                            AllOutTotal = Convert.ToDecimal(ojb);
                        }
                         
                        //入库的未支付+退货的未支付 2个逻辑要分开处理
                        model.NoInvoice = GoodTotal + AllOutTotal - model.HadInvoice;

                        ojb = dataReader["SumKuXuCai"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SumKuXuCai = Convert.ToDecimal(ojb);
                        }

                        list.Add(model);
                        i++;
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.TB_HouseGoods> GetModelInfo(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TB_HouseGoods.id,HouseId,TB_HouseGoods.GoodId,GoodAvgPrice,GoodNum");
            strSql.Append(" FROM TB_HouseGoods ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.TB_HouseGoods> list = new List<VAN_OA.Model.JXC.TB_HouseGoods>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.TB_HouseGoods model = new VAN_OA.Model.JXC.TB_HouseGoods();
                        object ojb;
                        ojb = dataReader["id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.id = (int)ojb;
                        }
                        ojb = dataReader["HouseId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.HouseId = (int)ojb;
                        }
                        ojb = dataReader["GoodId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodId = (int)ojb;
                        }
                        ojb = dataReader["GoodAvgPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodAvgPrice = (decimal)ojb;
                        }
                        ojb = dataReader["GoodNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodNum = (decimal)ojb;
                        }                        
                        model.Total = model.GoodNum * model.GoodAvgPrice;

                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public decimal GetGoodNum(int houseId,int GoodId)
        {

            decimal goodNum = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GoodNum ");
            strSql.Append(string.Format(" FROM TB_HouseGoods where HouseId={0} and GoodId={1}",houseId,GoodId));

            object ob= DBHelp.ExeScalar(strSql.ToString());

            if (ob != null)
            {
                goodNum = Convert.ToDecimal(ob);
            }
            return goodNum;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public decimal GetGoodNum( int GoodId)
        {

            decimal goodNum = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GoodNum ");
            strSql.Append(string.Format(" FROM TB_HouseGoods where  GoodId={0}",  GoodId));

            object ob = DBHelp.ExeScalar(strSql.ToString());

            if (ob != null)
            {
                goodNum = Convert.ToDecimal(ob);
            }
            return goodNum;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.TB_HouseGoods ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.TB_HouseGoods model = new VAN_OA.Model.JXC.TB_HouseGoods();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            ojb = dataReader["HouseId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HouseId = (int)ojb;
            }
            ojb = dataReader["GoodId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodId = (int)ojb;
            }
            ojb = dataReader["GoodAvgPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodAvgPrice = (decimal)ojb;
            }
            ojb = dataReader["GoodNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNum = (decimal)ojb;
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

            ojb = dataReader["houseName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HouseName = ojb.ToString();
            }

            model.Total = model.GoodNum * model.GoodAvgPrice;
            return model;
        }

    }
}
