using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using VAN_OA.Model.JXC;

namespace VAN_OA.Dal.JXC
{
    public class CashFlowService
    {
        public void Update(CashFlow model)
        {
            bool isUpdate = false;
            decimal TopZJHLV = 0;
            decimal TopZJZY = 0;
            decimal TopYLNL = 0;
            decimal TopYLLV = 0;

            if (model.TopZJHLV < model.ZJHLV)
            {
                TopZJHLV = model.ZJHLV;
                isUpdate = true;
            }
            else
            {
                TopZJHLV = model.TopZJHLV;
            }
            if (model.TopZJZY < model.ZJZY)
            {
                TopZJZY = model.ZJZY;
                isUpdate = true;
            }
            else
            {
                TopZJZY = model.TopZJZY;
            }
            if (model.TopYLNL < model.YLNL)
            {
                TopYLNL = model.YLNL;
                isUpdate = true;
            }
            else
            {
                TopYLNL = model.TopYLNL;
            }
            if (model.TopYLLV < model.YLLV)
            {
                TopYLLV = model.YLLV;
                isUpdate = true;
            }
            else
            {
                TopYLLV = model.TopYLLV;
            }
            if (isUpdate)
            {
                string sql = string.Format(@"if exists(select Ids from  Ex_PO_POOrder where PPoNo='{4}')
begin
update Ex_PO_POOrder set TopZJHLV={0},TopZJZY={1},TopYLNL={2},TopYLLV={3} where PPoNo='{4}'
end
else
begin
insert into Ex_PO_POOrder values({0},{1},{2},{3},'{4}')
end", TopZJHLV, TopZJZY, TopYLNL, TopYLLV, model.PONO);
                DBHelp.ExeCommand(sql);
            }
        }


        public void Update(CashFlow model, SqlCommand comm)
        {
            bool isUpdate = false;
            decimal TopZJHLV = 0;
            decimal TopZJZY = 0;
            decimal TopYLNL = 0;
            decimal TopYLLV = 0;

            if (model.TopZJHLV < model.ZJHLV || model.IsNullTopZJHLV)
            {
                TopZJHLV = model.ZJHLV;
                isUpdate = true;
            }
            else
            {
                TopZJHLV = model.TopZJHLV;
            }
            if (model.TopZJZY < model.ZJZY || model.IsNullTopZJZY)
            {
                TopZJZY = model.ZJZY;
                isUpdate = true;
            }
            else
            {
                TopZJZY = model.TopZJZY;
            }
            if (model.TopYLNL < model.YLNL || model.IsNullTopYLNL)
            {
                TopYLNL = model.YLNL;
                isUpdate = true;
            }
            else
            {
                TopYLNL = model.TopYLNL;
            }
            if (model.TopYLLV < model.YLLV || model.IsNullTopYLLV)
            {
                TopYLLV = model.YLLV;
                isUpdate = true;
            }
            else
            {
                TopYLLV = model.TopYLLV;
            }
            if (isUpdate)
            {
                string sql = string.Format(@"if exists(select Ids from  Ex_PO_POOrder where PPoNo='{4}')
begin
update Ex_PO_POOrder set TopZJHLV={0},TopZJZY={1},TopYLNL={2},TopYLLV={3} where PPoNo='{4}'
end
else
begin
insert into Ex_PO_POOrder values({0},{1},{2},{3},'{4}')
end", TopZJHLV, TopZJZY, TopYLNL, TopYLLV, model.PONO);
                comm.CommandText = sql;
                comm.ExecuteNonQuery();
            }
        }
        public List<Model.JXC.CashFlow> GetListArray(string sql)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select LastSupplierTotal,GoodSellPriceTotal-isnull(TuiTotal,0) as GoodSellPriceTotal,SellInTotal,KuCunTotal,caiOutTotal,TopZJHLV,TopZJZY,TopYLNL,TopYLLV,NotKuCunTotal,CG_POOrder.FPTotal,SellFPTotal,POOrder.PONo,POName,AE,IsPoFax,ALLPOTotal,TuiTotal,InvoiceTotal,CostPrice,OtherCostm,
Profit,NotRuTotal,notRuSellTotal,SellOutTotal,lastCaiTotal,SupplierTotal,ItemTotal,SellFPTotal,goodTotal
from 
(
select PONo,sum(Num*CostPrice) as CostPrice,sum(OtherCost) as OtherCostm,
sum((SellPrice-CostPrice)* Num-OtherCost) as Profit from CG_POOrder left join CG_POOrders on CG_POOrder.Id=CG_POOrders.Id
where Status='通过'  group by PONo
) as POOrder
left join 
(
	select PONo,sum(POTotal) as ALLPOTotal from CG_POOrder where Status='通过'  group by PONo
) as SumPoOrder on SumPoOrder.PONo=POOrder.PONo
--采购单
left join 
(
select PONo ,sum(Num*lastPrice) as lastCaiTotal
,sum((case when lastSupplier<>'库存' then Num*lastPrice else 0 end)) as NotKuCunTotal from CAI_POOrder
left join CAI_POCai on CAI_POOrder.Id=CAI_POCai.id where Status='通过'  
group by PONo
)
as CAIPOOrder on POOrder.PONo= CAIPOOrder.PONo
--采购单退货
left join 
(
select PONo ,sum(GoodNum*GoodPrice) as caiOutTotal from CAI_OrderOutHouse
left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.id where Status='通过'  
group by PONo
)
as CAIOutPOOrder on POOrder.PONo= CAIOutPOOrder.PONo
--销售出库
left join 
(
select PONo ,sum(GoodNum*GoodPrice) as SellOutTotal,min(RuTime) as minOutTime,sum(GoodNum*GoodSellPrice) as GoodSellPriceTotal from Sell_OrderOutHouse
left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id where Status='通过'  
group by PONo
)
as SellOutHouse on POOrder.PONo= SellOutHouse.PONo
--销售退库
left join 
(
select PONo ,sum(GoodNum*GoodSellPrice) as TuiTotal,sum(GoodNum*GoodPrice) as SellInTotal from Sell_OrderInHouse left join  Sell_OrderInHouses
on Sell_OrderInHouse.Id=Sell_OrderInHouses.id where Status='通过'  group by PONo
)
as SellInHouse on POOrder.PONo= SellInHouse.PONo

left join (
SELECT PONO,sum(SupplierTotal) as SupplierTotal,Sum(LastSupplierTotal) as LastSupplierTotal FROM (
select PONo,SupplierInvoiceTotal as SupplierTotal,SupplierInvoiceTotal*CAI_POCai.lastPrice/CAI_POCai.LastTruePrice  as LastSupplierTotal from TB_SupplierAdvancePayment
 left join TB_SupplierAdvancePayments on TB_SupplierAdvancePayment.Id=TB_SupplierAdvancePayments.Id
 left join CAI_POCai on CAI_POCai.Ids=TB_SupplierAdvancePayments.CaiIds
 LEFT JOIN CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
where TB_SupplierAdvancePayment.Status='通过'
UNION ALL
select PONO,ActPay as SupplierTotal,ActPay*CAI_OrderInHouses.GoodPrice/CAI_OrderInHouses.CaiLastTruePrice as LastSupplierTotal from TB_SupplierInvoice 
left join TB_SupplierInvoices on TB_SupplierInvoice.Id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
where TB_SupplierInvoice.Status='通过' and IsYuFu=0 and ((IsHeBing=1 and SupplierInvoiceTotal<0) or (ActPay>0 and CreateName<>'admin') OR (exists (select SupplierInvoiceIds from TB_TempSupplierInvoice where   TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds )))
) AS SupplierInovice group by PONo
) as SupplierInovice on POOrder.PONo=SupplierInovice.PONo
left join 
(SELECT PONO,SUM(total) as ItemTotal FROM(
--非材料报销（邮寄费） 
select pono,isnull(PostTotal,0)  as Total 
from Tb_DispatchList where PONo is not null  and state='通过' and  PostTotal is not null
union all
--非材料报销（除邮寄费） 
select  PONo,
isnull(BusTotal,0)+isnull(RepastTotal,0)+isnull(HotelTotal,0)+isnull(OilTotal,0)+isnull(GuoTotal,0) 
 +isnull(OtherTotal,0)  as Total 
from Tb_DispatchList where PONo is not null  and state='通过' and POTotal is  null 
union all
--公交车费
select PONo,useTotal as Total
from TB_BusCardUse where PONo is not null   
union all 
--私车油耗费(state)
select PONo, OilPrice*roadlong as Total
from tb_UseCar   where PONo is not null and state='通过' 
union all 
--用车申请油耗费(state)
select PONo, OilPrice*roadlong as Total
from TB_UseCarDetail   where PONo is not null and state='通过' 
union all 
-- 行政采购金额(state)
select pono, XingCaiTotal as Total
from tb_FundsUse where PONo is not null and XingCaiTotal is not null and state='通过' 
union all 
--会务费用(state)
select pono,HuiTotal as Total
from tb_FundsUse where PONo is not null and HuiTotal is not null and state='通过'
union all
--人工费(state) 
select pono, RenTotal as Total
from tb_FundsUse where PONo is not null and RenTotal is not null and state='通过'
union all
--加班单
select PONo,Total
from tb_OverTime where PONo is not null and state='通过'
) AS A GROUP BY PONO 
) 
AS TempTotal on POOrder.PONo=TempTotal.PONo

--开票总额
left join 
(
select PONo ,sum(Total) as SellFPTotal from Sell_OrderFP where Status='通过'  
group by PONo
)
as SellOrderFP on POOrder.PONo= SellOrderFP.PONo
--到款
left join 
(
SELECT PONo,sum(Total) as InvoiceTotal,max(DaoKuanDate) as MaxDaoKuanDate FROM TB_ToInvoice WHERE State<>'不通过'
GROUP BY PONo
) as Inovice
on POOrder.PONo= Inovice.PONo
left join
(select CAI_POOrder.PONo,sum(([Num]-isnull(checkNum,0))*lastPrice) as NotRuTotal
from CAI_POCai 
left join CAI_POOrder on CAI_POCai.Id=CAI_POOrder.Id 
left join 
(
select  CaiId,SUM(CheckNum) as checkNum from CAI_OrderChecks left join CAI_OrderCheck on  CAI_OrderCheck.id=CAI_OrderChecks.CheckId
where CaiId<>0  and CAI_OrderCheck.status<>'不通过' 
group by CaiId
)
as newtable on CAI_POCai.Ids=newtable.CaiId 
where status='通过' and lastSupplier<>'库存' group by CAI_POOrder.PONo) as NotRuCai

on POOrder.PONo= NotRuCai.PONo

left join 
(
select newTbCai.pono,
sum(
case  
when isnull(WaiCaiNum,0)=0 then allCaiNum-ISNULL(outNum,0) 
when isnull(WaiCaiNum,0)>0 and ISNULL(sellTuiNum,0)>=ISNULL(caiTuiNum,0) then ISNULL(waiCaiNum,0)+isnull(kuCaiNum,0)-ISNULL(outNum,0) 
when isnull(WaiCaiNum,0)>0 and ISNULL(sellTuiNum,0)<ISNULL(caiTuiNum,0) then  ISNULL(waiCaiNum,0)+isnull(kuCaiNum,0)-ISNULL(outNum,0)+ISNULL(sellTuiNum,0)-ISNULL(caiTuiNum,0)
else 0 end 
* (case when GoodAvgPrice is null then caiAvgPrice else GoodAvgPrice end)
)  as notRuSellTotal
from 
(select CAI_POOrder.PONo,GoodId, sum(Num) as allCaiNum,
sum((case when  lastSupplier='库存' then num end)) as kuCaiNum,
sum((case when  lastSupplier<>'库存' then num end)) as waiCaiNum,sum( lastPrice*Num)/sum(Num) as caiAvgPrice  from CAI_POOrder
left join  CAI_POCai on CAI_POOrder.id=CAI_POCai.id 
where  Status='通过' 
and ( lastSupplier='库存' or
exists( select id from CAI_OrderCheck left join CAI_OrderChecks on CAI_OrderCheck.Id=CAI_OrderChecks.CheckId where
CAI_OrderCheck.Status='通过' and CAI_POCai.Ids=CAI_OrderChecks.CaiId ))
group by CAI_POOrder.PONo,GoodId )
as newTbCai
left join
(
select PoNo,GooId,sum(GoodNum) as outNum from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id 
where Status='通过'  group by GooId,PoNo
) as newTb2 on newTbCai.GoodId=newTb2.GooId  and  newTbCai.Pono=newTb2.Pono  left join 
(
select PoNo,GooId,sum(GoodNum)  as caiTuiNum from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.ID 
where Status='通过'   group by GooId ,PoNo
) as newTb3 on newTbCai.GoodId=newTb3.GooId and newTbCai.Pono=newTb3.Pono  left join 
(
select PoNo,GooId,sum(GoodNum) as sellTuiNum from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.ID  
where  Status='通过'  group by GooId,PoNo
) newTb4 on newTbCai.GoodId=newTb4.GooId and newTbCai.POno=newTb4.pono 
left join TB_HouseGoods on TB_HouseGoods.GoodId=newTbCai.GoodId
group by newTbCai.PONo

) as NotRuSell on POOrder.PONo= NotRuSell.PONo
left join CG_POOrder on  POOrder.PONo= CG_POOrder.PONo and CG_POOrder.IFZhui=0 and CG_POOrder.Status='通过'
left join (select pono,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal from JXC_REPORT group by pono) as JxcReport on CG_POOrder.PONo=JxcReport.PONo
left join Ex_PO_POOrder on Ex_PO_POOrder.PPoNo=CG_POOrder.PONo

left join (
select PONo,SUM(KuCunTotal) as KuCunTotal  from (select TB_Cai.PONo,
 --如果采购有来自 库存的商品，且没有销售退货，
 --A.该商品出库数量>= 采购来自库存的数量 ，净库存出库数量=采购来自库存的数量  
 --B. 该商品出库数量< 采购来自库存的数量 ，净库存出库数量=该商品出库数量
(case 
when isnull(SellGoodNums,0)>=CaiNums then CaiNums 
when  isnull(SellGoodNums,0)<CaiNums then SellGoodNums 
end -
case
 --如果采购有来自 库存的商品
--A.销售退货的数量 >=采购来自库存的数量 ,扣除数量=采购来自库存的数量 ；
--B.销售退货的数量 <采购来自库存的数量 ,扣除数量=销售退货的数量
when SellInGoodNums is not null and isnull(SellInGoodNums,0)>=CaiNums then CaiNums 
when SellInGoodNums is not null and isnull(SellInGoodNums,0)<CaiNums then SellInGoodNums
else 0
end)* isnull(avgGoodPrice,0) as KuCunTotal from 
(select PONo ,sum(Num) as CaiNums,GoodId from CAI_POOrder
left join CAI_POCai on CAI_POOrder.Id=CAI_POCai.id where Status='通过' and lastSupplier='库存'
group by PONo,GoodId 
) AS TB_Cai 
left join 
(
select PONo,GooId,sum(GoodNum) as SellGoodNums,avg(GoodPrice) as avgGoodPrice from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id
group by PONo,GooId 
) as TB_SellOut on  TB_SellOut.PONo=TB_Cai.PONo and TB_SellOut.GooId=TB_Cai.GoodId
left join  
(
select PONo,GooId,sum(GoodNum) as SellInGoodNums from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.id
group by PONo,GooId 
) as TB_SellIn 
on TB_SellIn.PONo=TB_Cai.PONo and TB_SellIn.GooId=TB_Cai.GoodId) as TB 
GROUP BY PONo) as TB_OutSum on TB_OutSum.PONO=CG_POOrder.PONo");
            if (sql.Trim() != "")
            {
                strSql.Append(" where 1=1 " + sql);
            }
            strSql.Append(" order by POOrder.PONo desc");
            List<Model.JXC.CashFlow> list = new List<Model.JXC.CashFlow>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model=ReaderBind(dataReader);
                        object obj= dataReader["LastSupplierTotal"];
                        if (obj != null && obj != DBNull.Value)
                        {
                            model.LastSupplierTotal= Convert.ToDecimal(obj);
                        }
                        list.Add(model);
                       
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有KC 需要检验*单价 的总金额
        /// </summary>
        /// <returns></returns>
        public decimal GetKCXuJianTotal()
        {
            string sql = string.Format(@"select SUM(YuFuWeiTotal) as XuJianTotal
FROM XuJianView  where  1=1 and PONo like 'KC%' ");
            var ojb = DBHelp.ExeScalar(sql);
            if (ojb != null && ojb != DBNull.Value)
            {
                return Convert.ToDecimal(ojb);
            }
            return 0;
        }
        /// <summary>
        /// 是 所有项目的 库存未付 合并相同商品 后的 未支付的合计值。 意思就是 各项目的采购来自库存的商品编码 经 同类合并 后的 进销存的  截止今日为止的 未支付总值的 合计值。
        /// </summary>
        /// <returns></returns>
        public decimal GetNoInvoiceTotal()
        {

            string sql = @"SELECT SUM(NoInvoice) FROM (
select PONo,--支付单价/实采金额 *采购单价
sum(GoodNum*GoodPrice)-isnull(sum(OutTotal),0)-isnull(sum(SupplierInvoiceTotal),0) as NoInvoice
 from CAI_OrderInHouse 
left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id 
left join tb_User on tb_User.id=CAI_OrderInHouse.CreateUserId
left join 
(
--单支付的单子
select RuIds,
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
where status='通过' and CAI_OrderInHouse.pono like 'KC%'
group by PONo) 
AS Invoice";
            decimal total = 0;
            var ojb = DBHelp.ExeScalar(sql);
            if (ojb != null && ojb != DBNull.Value)
            {
                total = Convert.ToDecimal(ojb);
            }
            return total;
        }
        /// <summary>
        /// 关于应付库存中未支付部分涉及了 应付合计，运营总盘子，我考虑了一下：
        ///所有采购来自库存的部分，是KC 采购完成，这些KC 采购进来的商品，涉及预付和支付
        ///2块，由于我和彩蓉对2012-2016年的未支付已作清理，也就是系统中基本上未支付的都
        ///是真实的，可以这样解决：
        ///目前库存表中未支付的部分，就是需要补充的金额，这样我们只需要对当前库存中未支
        ///付的部分进行界定即可.        
        /// </summary>
        /// <returns></returns>

        public decimal GetKCWeiZhiFuTotal()
        {
            //支付
            string sql = @"SELECT sum(YuFuTotal)FROM (
select CAI_OrderInHouse.PONo,GoodNum*CaiLastTruePrice-isnull(HadSupplierInvoiceTotal,0)-isnull(HadFuShuTotal,0) AS YuFuTotal  from CAI_OrderInHouse 
 left join CAI_OrderInHouses  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join 
(
select TB_SupplierInvoices.RuIds,Sum(SupplierInvoiceTotal) as  HadSupplierInvoiceTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where ((status<>'不通过' and IsYuFu=0) or  (status<>'不通过' and IsYuFu=1)) and SupplierInvoiceTotal>0
group by TB_SupplierInvoices.RuIds
)
as HadSupplierInvoice on HadSupplierInvoice.RuIds=CAI_OrderInHouses.Ids
left join
(
select CAI_OrderInHouses.Ids,Sum(CAI_OrderOutHouses.GoodNum*CAI_OrderInHouses.CaiLastTruePrice) as  HadFuShuTotal from 
CAI_OrderOutHouse 
left join CAI_OrderOutHouses on CAI_OrderOutHouse.id=CAI_OrderOutHouses.id
left join CAI_OrderInHouses on CAI_OrderInHouses.Ids=CAI_OrderOutHouses.OrderCheckIds
where CAI_OrderOutHouse.status='通过' 
group by CAI_OrderInHouses.Ids
)
as temp3 on temp3.Ids=CAI_OrderInHouses.Ids
where  1=1  and CAI_OrderInHouse.Status='通过' and CAI_OrderInHouse.PONo like 'KC%') AS A WHERE YuFuTotal>0";
            decimal total = 0;
            var ojb = DBHelp.ExeScalar(sql);
            if (ojb != null && ojb != DBNull.Value)
            {
                total = Convert.ToDecimal(ojb);
            }
            //预付
            sql = @"select sum(isnull(Num,0)*LastTruePrice-isnull(SupplierInvoiceTotal,0))  
from CAI_POOrder  
left join CAI_POCai  on CAI_POCai.id=CAI_POOrder.id  
left join TB_Good on TB_Good.GoodId=CAI_POCai.GoodId  left join 
(
select TB_SupplierAdvancePayments.CaiIds,sum(SupplierInvoiceTotal) as SupplierInvoiceTotal
from  TB_SupplierAdvancePayment 
left join TB_SupplierAdvancePayments on  TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id
where TB_SupplierAdvancePayment.status<>'不通过'
group by CaiIds
)
as tb2 on tb2.CaiIds=CAI_POCai.ids
left join 
(SELECT CaiId FROM CAI_OrderChecks left join CAI_OrderCheck on CAI_OrderChecks.CheckId=CAI_OrderCheck.Id
where Status in ('通过','执行中')
group by CaiId)
as tb3 on tb3.CaiId=CAI_POCai.Ids  where   CAI_POOrder.Status='通过'  and PONo like '%KC%' 
and (CaiId is null and (isnull(Num,0)*LastTruePrice-isnull(SupplierInvoiceTotal,0))>0)";

            ojb = DBHelp.ExeScalar(sql);
            if (ojb != null && ojb != DBNull.Value)
            {
                total += Convert.ToDecimal(ojb);
            }
            return total;
        }
        /// <summary>
        /// 运营
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<Model.JXC.CashFlow> GetYunYingList(string sql)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select YuFuWeiTotal,LastSupplierTotal,NoInvoice,XuJianTotal,KuCunTotal,FaxTuiTotal,FaxPoTotal,CaiOutTotal,POOrder.PONo,POName,AE,IsPoFax,ALLPOTotal,GoodSellPriceTotal-isnull(TuiTotal,0) as GoodSellPriceTotal,SellFPTotal, 0 as 未开票额,InvoiceTotal,
0 as 项目应收,LastCaiTotal,SupplierTotal,0 as 应付总额,isnull(maoli,0) as maoliTotal,  InvoiceTotal,GoodTotal,NotKuCunTotal,TuiTotal,ItemTotal,SellOutTotal-isnull(TuiTotal,0) as SellOutTotal
from
(
	select PONo,sum(POTotal) as ALLPOTotal,sum(case when IsPoFax=1 then POTotal else 0 end) as FaxPoTotal from CG_POOrder where Status='通过'  group by PONo
) as POOrder 
--采购单
left join 
(
select PONo ,sum(Num*lastPrice) as lastCaiTotal
,sum((case when lastSupplier<>'库存' then Num*lastPrice else 0 end)) as NotKuCunTotal from CAI_POOrder
left join CAI_POCai on CAI_POOrder.Id=CAI_POCai.id where Status='通过'  
group by PONo
)
as CAIPOOrder on POOrder.PONo= CAIPOOrder.PONo
--采购单退货
left join 
(
select PONo ,sum(GoodNum*GoodPrice) as CaiOutTotal from CAI_OrderOutHouse
left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.id where Status='通过'  
group by PONo
)
as caiOut on POOrder.PONo= caiOut.PONo
--销售出库
left join 
(
select PONo,sum(GoodNum*GoodPrice) as SellOutTotal,min(RuTime) as minOutTime ,sum(GoodNum*GoodSellPrice) as GoodSellPriceTotal from Sell_OrderOutHouse
left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id where Status='通过'  
group by PONo
)
as SellOutHouse on POOrder.PONo= SellOutHouse.PONo
--销售退库
left join 
(
select Sell_OrderInHouse.PONo ,sum(TuiTotal) as TuiTotal
,sum(case when IsPoFax=1 then TuiTotal else 0 end) as FaxTuiTotal  from Sell_OrderInHouse
left join CG_POOrder on CG_POOrder.PONo=Sell_OrderInHouse.PONo and CG_POOrder.IFZhui=0
where Sell_OrderInHouse.Status='通过'
group by Sell_OrderInHouse.PONo
)
as SellInHouse on POOrder.PONo= SellInHouse.PONo

left join (
SELECT PONO,sum(SupplierTotal) as SupplierTotal,Sum(LastSupplierTotal) as LastSupplierTotal FROM (
select PONo,SupplierInvoiceTotal as SupplierTotal,SupplierInvoiceTotal*CAI_POCai.lastPrice/CAI_POCai.LastTruePrice  as LastSupplierTotal from TB_SupplierAdvancePayment
 left join TB_SupplierAdvancePayments on TB_SupplierAdvancePayment.Id=TB_SupplierAdvancePayments.Id
 left join CAI_POCai on CAI_POCai.Ids=TB_SupplierAdvancePayments.CaiIds
 LEFT JOIN CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
where TB_SupplierAdvancePayment.Status='通过'
UNION ALL
select PONO,ActPay as SupplierTotal,ActPay*CAI_OrderInHouses.GoodPrice/CAI_OrderInHouses.CaiLastTruePrice as LastSupplierTotal from TB_SupplierInvoice 
left join TB_SupplierInvoices on TB_SupplierInvoice.Id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
where TB_SupplierInvoice.Status='通过' and IsYuFu=0 and ((IsHeBing=1 and SupplierInvoiceTotal<0) or (ActPay>0 and CreateName<>'admin') OR (exists (select SupplierInvoiceIds from TB_TempSupplierInvoice where   TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds)))
) AS SupplierInovice group by PONo
) as SupplierInovice on POOrder.PONo=SupplierInovice.PONo
left join 
(SELECT PONO,SUM(total) as ItemTotal FROM(
--非材料报销（邮寄费） 
select pono,isnull(PostTotal,0)  as Total 
from Tb_DispatchList where PONo is not null  and state='通过' and  PostTotal is not null
union all
--非材料报销（除邮寄费） 
select  PONo,
isnull(BusTotal,0)+isnull(RepastTotal,0)+isnull(HotelTotal,0)+isnull(OilTotal,0)+isnull(GuoTotal,0) 
 +isnull(OtherTotal,0)  as Total 
from Tb_DispatchList where PONo is not null  and state='通过' and POTotal is  null 
union all
--公交车费
select PONo,useTotal as Total
from TB_BusCardUse where PONo is not null   
union all 
--私车油耗费(state)
select PONo, OilPrice*roadlong as Total
from tb_UseCar   where PONo is not null and state='通过' 
union all 
--用车申请油耗费(state)
select PONo, OilPrice*roadlong as Total
from TB_UseCarDetail   where PONo is not null and state='通过' 
union all 
-- 行政采购金额(state)
select pono, XingCaiTotal as Total
from tb_FundsUse where PONo is not null and XingCaiTotal is not null and state='通过' 
union all 
--会务费用(state)
select pono,HuiTotal as Total
from tb_FundsUse where PONo is not null and HuiTotal is not null and state='通过'
union all
--人工费(state) 
select pono, RenTotal as Total
from tb_FundsUse where PONo is not null and RenTotal is not null and state='通过'
union all
--加班单
select PONo,Total
from tb_OverTime where PONo is not null and state='通过'
) AS A GROUP BY PONO 
) 
AS TempTotal on POOrder.PONo=TempTotal.PONo

--开票总额
left join 
(
select PONo ,sum(Total) as SellFPTotal from Sell_OrderFP where Status='通过'  
group by PONo
)
as SellOrderFP on POOrder.PONo= SellOrderFP.PONo
--到款
left join 
(
SELECT PONo,sum(Total) as InvoiceTotal,max(DaoKuanDate) as MaxDaoKuanDate FROM TB_ToInvoice WHERE State<>'不通过'
GROUP BY PONo
) as Inovice
on POOrder.PONo= Inovice.PONo
left join CG_POOrder on  POOrder.PONo= CG_POOrder.PONo and CG_POOrder.IFZhui=0 and CG_POOrder.Status='通过'
left join (select pono,sum(goodTotal)+sum(t_goodTotalChas)as goodTotal,isnull(sum(maoli),0) as maoli from JXC_REPORT group by pono) as JxcReport on CG_POOrder.PONo=JxcReport.PONo
left join (
select PONo,SUM(KuCunTotal) as KuCunTotal  from (select TB_Cai.PONo,
 --如果采购有来自 库存的商品，且没有销售退货，
 --A.该商品出库数量>= 采购来自库存的数量 ，净库存出库数量=采购来自库存的数量  
 --B. 该商品出库数量< 采购来自库存的数量 ，净库存出库数量=该商品出库数量
(case 
when isnull(SellGoodNums,0)>=CaiNums then CaiNums 
when  isnull(SellGoodNums,0)<CaiNums then SellGoodNums 
end -
case
 --如果采购有来自 库存的商品
--A.销售退货的数量 >=采购来自库存的数量 ,扣除数量=采购来自库存的数量 ；
--B.销售退货的数量 <采购来自库存的数量 ,扣除数量=销售退货的数量
when SellInGoodNums is not null and isnull(SellInGoodNums,0)>=CaiNums then CaiNums 
when SellInGoodNums is not null and isnull(SellInGoodNums,0)<CaiNums then SellInGoodNums
else 0
end)* isnull(avgGoodPrice,0) as KuCunTotal from 
(select PONo ,sum(Num) as CaiNums,GoodId from CAI_POOrder
left join CAI_POCai on CAI_POOrder.Id=CAI_POCai.id where Status='通过' and lastSupplier='库存'
group by PONo,GoodId 
) AS TB_Cai 
left join 
(
select PONo,GooId,sum(GoodNum) as SellGoodNums,avg(GoodPrice) as avgGoodPrice from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id
group by PONo,GooId 
) as TB_SellOut on  TB_SellOut.PONo=TB_Cai.PONo and TB_SellOut.GooId=TB_Cai.GoodId
left join  
(
select PONo,GooId,sum(GoodNum) as SellInGoodNums from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.id
group by PONo,GooId 
) as TB_SellIn 
on TB_SellIn.PONo=TB_Cai.PONo and TB_SellIn.GooId=TB_Cai.GoodId) as TB 
GROUP BY PONo) as TB_OutSum on TB_OutSum.PONO=CG_POOrder.PONo
left join XuJianView on  XuJianView.PONO=POOrder.PONo
left join 
(
select PONo,--支付单价/实采金额 *采购单价
sum(GoodNum*GoodPrice)-isnull(sum(OutTotal),0)-isnull(sum(SupplierInvoiceTotal),0) as NoInvoice
 from CAI_OrderInHouse 
left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id 
left join tb_User on tb_User.id=CAI_OrderInHouse.CreateUserId
left join 
(
--单支付的单子
select RuIds,
--sum((GoodNum*GoodPrice)*SupplierInvoiceTotal/(GoodNum*CaiLastTruePrice))  as SupplierInvoiceTotal
sum(SupplierInvoiceTotal*GoodPrice/CaiLastTruePrice)  as SupplierInvoiceTotal
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
where status='通过' and CAI_OrderInHouse.Supplier='库存'
group by PONo
) AS Invoice  on  XuJianView.PONO=Invoice.PONo");
            if (sql.Trim() != "")
            {
                strSql.Append(" where 1=1 " + sql);
            }
            strSql.Append(" order by POOrder.PONo desc");
            List<Model.JXC.CashFlow> list = new List<Model.JXC.CashFlow>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Model.JXC.CashFlow model = new Model.JXC.CashFlow();
                        object ojb;
                        model.PONO = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        model.AE = dataReader["AE"].ToString();
                        model.POTotal = Convert.ToDecimal(dataReader["ALLPOTotal"]);

                        model.FaxPoTotal = Convert.ToDecimal(dataReader["FaxPoTotal"]);

                        model.IsHanShui = Convert.ToBoolean(dataReader["IsPoFax"]);


                        ojb = dataReader["TuiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal -= Convert.ToDecimal(ojb);
                        }
                        //含税项目减去含税退货金额
                        ojb = dataReader["FaxTuiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FaxPoTotal -= Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["SellOutTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SellOutTotal = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["maoliTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MaoLiTotal = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["GoodSellPriceTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodSellPriceTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["LastCaiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LastCaiTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["LastSupplierTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LastSupplierTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["SupplierTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SupplierTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["ItemTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ItemTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["SellFPTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FPTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["InvoiceTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvoiceTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["goodTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodTotal = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["NotKuCunTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            //model.NotKuCunTotal = Convert.ToDecimal(ojb);

                            //总采购金额-非库存采购金额
                            model.YingFuKuCun = model.LastCaiTotal - Convert.ToDecimal(ojb);

                            //库存出库金额 =（总出库金额-外采数量×采购金额）
                            //model.NotKuCunTotal = model.SellOutTotal - model.NotKuCunTotal;
                        }

                        // 如果采购 没有来自 库存 的商品，净库存出库成本金额=0
                        ojb = dataReader["KuCunTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.NotKuCunTotal = Convert.ToDecimal(ojb);
                        }

                        //由于系统有采购退货一项，在采购总额这一列，需要修改下， 
                        //采购总额=该项目 所有采购的总金额-所有采购退货的总金额
                        ojb = dataReader["CaiOutTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiOutTotal = Convert.ToDecimal(ojb);
                            model.LastCaiTotal -= model.CaiOutTotal;
                        }

                        ojb = dataReader["XuJianTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.XuJianTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["NoInvoice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.NoInvoice = Convert.ToDecimal(ojb);
                        } ojb = dataReader["YuFuWeiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.YuFuWeiTotal = Convert.ToDecimal(ojb);
                        }


                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<Model.JXC.CashFlowReport> GetYunYingListReport(string sql,bool isJinWei)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select CheckIngTotal,ZhiTotal,ZhiTotalIng,YuTotal,YuTotalIng,ToSupplierTotal,NotKuCunTotal,LastSupplierTotal,FaxTuiTotal,FaxPoTotal,CaiOutTotal,POOrder.PONo,POName,AE,IsPoFax,ALLPOTotal,
GoodSellPriceTotal-isnull(TuiTotal,0) as GoodSellPriceTotal,SellFPTotal,InvoiceTotal,LastCaiTotal,SupplierTotal,InvoiceTotal,TuiTotal,SellOutTotal-isnull(TuiTotal,0) as SellOutTotal
from
(
select PONo,sum(POTotal) as ALLPOTotal,sum(case when IsPoFax=1 then POTotal else 0 end) as FaxPoTotal from CG_POOrder where Status='通过'  group by PONo
) as POOrder 
--采购单
left join 
(
select PONo ,sum(Num*lastPrice) as lastCaiTotal,sum((case when lastSupplier<>'库存' then Num*lastPrice else 0 end)) as NotKuCunTotal from CAI_POOrder
left join CAI_POCai on CAI_POOrder.Id=CAI_POCai.id where Status='通过'  
group by PONo
)
as CAIPOOrder on POOrder.PONo= CAIPOOrder.PONo
--采购单退货
left join 
(
select PONo ,sum(GoodNum*GoodPrice) as CaiOutTotal from CAI_OrderOutHouse
left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.id where Status='通过'  
group by PONo
)
as caiOut on POOrder.PONo= caiOut.PONo
--销售出库
left join 
(
select PONo,sum(GoodNum*GoodPrice) as SellOutTotal,min(RuTime) as minOutTime ,sum(GoodNum*GoodSellPrice) as GoodSellPriceTotal from Sell_OrderOutHouse
left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id where Status='通过'  
group by PONo
)
as SellOutHouse on POOrder.PONo= SellOutHouse.PONo
--销售退库
left join 
(
select Sell_OrderInHouse.PONo ,sum(TuiTotal) as TuiTotal
,sum(case when IsPoFax=1 then TuiTotal else 0 end) as FaxTuiTotal  from Sell_OrderInHouse
left join CG_POOrder on CG_POOrder.PONo=Sell_OrderInHouse.PONo and CG_POOrder.IFZhui=0
where Sell_OrderInHouse.Status='通过'
group by Sell_OrderInHouse.PONo
)
as SellInHouse on POOrder.PONo= SellInHouse.PONo

left join (
SELECT PONO,sum(SupplierTotal) as SupplierTotal,Sum(LastSupplierTotal) as LastSupplierTotal FROM (
select PONo,SupplierInvoiceTotal as SupplierTotal,SupplierInvoiceTotal*CAI_POCai.lastPrice/CAI_POCai.LastTruePrice  as LastSupplierTotal from TB_SupplierAdvancePayment
 left join TB_SupplierAdvancePayments on TB_SupplierAdvancePayment.Id=TB_SupplierAdvancePayments.Id
 left join CAI_POCai on CAI_POCai.Ids=TB_SupplierAdvancePayments.CaiIds
 LEFT JOIN CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
where TB_SupplierAdvancePayment.Status='通过'
UNION ALL
select PONO,ActPay as SupplierTotal,ActPay*CAI_OrderInHouses.GoodPrice/CAI_OrderInHouses.CaiLastTruePrice as LastSupplierTotal from TB_SupplierInvoice 
left join TB_SupplierInvoices on TB_SupplierInvoice.Id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
where TB_SupplierInvoice.Status='通过' and IsYuFu=0 and ((IsHeBing=1 and SupplierInvoiceTotal<0) or (ActPay>0 and CreateName<>'admin') OR (exists (select SupplierInvoiceIds from TB_TempSupplierInvoice where   TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds)))
) AS SupplierInovice group by PONo
) as SupplierInovice on POOrder.PONo=SupplierInovice.PONo

--开票总额
left join 
(
select PONo ,sum(Total) as SellFPTotal from Sell_OrderFP where Status='通过'  
group by PONo
)
as SellOrderFP on POOrder.PONo= SellOrderFP.PONo
--到款
left join 
(
SELECT PONo,sum(Total) as InvoiceTotal,max(DaoKuanDate) as MaxDaoKuanDate FROM TB_ToInvoice WHERE State<>'不通过'
GROUP BY PONo
) as Inovice
on POOrder.PONo= Inovice.PONo
left join CG_POOrder on  POOrder.PONo= CG_POOrder.PONo and CG_POOrder.IFZhui=0 and CG_POOrder.Status='通过'
LEFT JOIN SupplierInVoiceToZhi ON SupplierInVoiceToZhi.PONO=POOrder.PONo
LEFT JOIN SupplierInVoiceToYu ON SupplierInVoiceToYu.PONO=POOrder.PONo
LEFT JOIN TB_ToSupplierTotal ON TB_ToSupplierTotal.PONO=POOrder.PONo
left join 
(--正在执行的支付单
select CAI_OrderInHouse.PONo,Sum(ActPay*CAI_OrderInHouses.GoodPrice/CAI_OrderInHouses.CaiLastTruePrice) as  ZhiTotalIng from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
left join CAI_OrderInHouses on CAI_OrderInHouses.Ids=TB_SupplierInvoices.RuIds
left join CAI_OrderInHouse on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id
where TB_SupplierInvoice.status='执行中' and SupplierInvoiceTotal>0 and IsYuFu=0
group by CAI_OrderInHouse.PONo
)
as HadSupplierInvoice on HadSupplierInvoice.PONo=POOrder.PONo

left join 
(
select PONo,sum(SupplierInvoiceTotal*CAI_POCai.lastPrice/CAI_POCai.LastTruePrice)  as YuTotalIng from TB_SupplierAdvancePayment
 left join TB_SupplierAdvancePayments on TB_SupplierAdvancePayment.Id=TB_SupplierAdvancePayments.Id
 left join CAI_POCai on CAI_POCai.Ids=TB_SupplierAdvancePayments.CaiIds
 LEFT JOIN CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
where TB_SupplierAdvancePayment.status='执行中'
group by PONo
) AS SupplierAdvancePaymentTotal on SupplierAdvancePaymentTotal.PONo=POOrder.PONo
left join
(--采购检验中可支付金额=采购检验中数量*（最终价格-预付单价）
select
CAI_POOrder.PONo,
sum(
checkNumsIng*(
--预付单价= SUM(预付款) ×采购单价/实采单价/已采数量
lastPrice-isnull(SupplierInvoiceTotal*CAI_POCai.lastPrice/CAI_POCai.LastTruePrice/CAI_POCai.Num,0)
)) as CheckIngTotal

from CAI_POCai 
left join CAI_POOrder on CAI_POCai.Id=CAI_POOrder.Id
left join 
(
select  CaiId,SUM(CheckNum) as checkNumsIng from CAI_OrderChecks left join CAI_OrderCheck on  CAI_OrderCheck.id=CAI_OrderChecks.CheckId
where CAI_OrderCheck.status='执行中' 
group by CaiId
)
as newtable on CAI_POCai.Ids=newtable.CaiId 
left join 
(
select CaiIds,sum(SupplierInvoicePrice) as SupplierInvoicePrice ,sum(SupplierInvoiceTotal) as SupplierInvoiceTotal from TB_SupplierAdvancePayment left join TB_SupplierAdvancePayments on 
TB_SupplierAdvancePayment.Id=TB_SupplierAdvancePayments.Id
where Status='通过' group by CaiIds
) as Invoice on Invoice.CaiIds=CAI_POCai.Ids

where checkNumsIng>0 and status='通过'
group by CAI_POOrder.PONo
) as CheckIng on CheckIng.pono=POOrder.PONo");
            if (sql.Trim() != "")
            {
                strSql.Append(" where 1=1 " + sql);
            }
            strSql.Append(" order by POOrder.PONo desc");
            List<Model.JXC.CashFlowReport> list = new List<Model.JXC.CashFlowReport>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Model.JXC.CashFlowReport model = new Model.JXC.CashFlowReport();
                        object ojb;
                        model.PONO = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        model.AE = dataReader["AE"].ToString();
                        model.POTotal = Convert.ToDecimal(dataReader["ALLPOTotal"]);

                        model.FaxPoTotal = Convert.ToDecimal(dataReader["FaxPoTotal"]);

                        model.IsHanShui = Convert.ToBoolean(dataReader["IsPoFax"]);


                        ojb = dataReader["TuiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal -= Convert.ToDecimal(ojb);
                        }
                        //含税项目减去含税退货金额
                        ojb = dataReader["FaxTuiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FaxPoTotal -= Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["SellOutTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SellOutTotal = Convert.ToDecimal(ojb);
                        }

                      

                        ojb = dataReader["GoodSellPriceTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodSellPriceTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["LastCaiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LastCaiTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["LastSupplierTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LastSupplierTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["SupplierTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SupplierTotal = Convert.ToDecimal(ojb);
                        }
                      
                        ojb = dataReader["SellFPTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FPTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["InvoiceTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvoiceTotal = Convert.ToDecimal(ojb);
                        }
                       
                        ojb = dataReader["NotKuCunTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            //model.NotKuCunTotal = Convert.ToDecimal(ojb);

                            //总采购金额-非库存采购金额
                            model.YingFuKuCun = model.LastCaiTotal - Convert.ToDecimal(ojb);

                            //库存出库金额 =（总出库金额-外采数量×采购金额）
                            //model.NotKuCunTotal = model.SellOutTotal - model.NotKuCunTotal;
                        }                       

                        //由于系统有采购退货一项，在采购总额这一列，需要修改下， 
                        //采购总额=该项目 所有采购的总金额-所有采购退货的总金额
                        ojb = dataReader["CaiOutTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiOutTotal = Convert.ToDecimal(ojb);
                            model.LastCaiTotal -= model.CaiOutTotal;
                        }

                        ojb = dataReader["ZhiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ZhiTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["ZhiTotalIng"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ZhiTotalIng = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["YuTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.YuTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["YuTotalIng"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.YuTotalIng = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["ToSupplierTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            //剩余支付-可支付=未检验可支付
                            model.ToSupplierTotal = Convert.ToDecimal(ojb);// - model.ZhiTotal-model.ZhiTotalIng;
                        }
                        ojb = dataReader["CheckIngTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CheckIngTotal = Convert.ToDecimal(ojb);
                        }
                        

                        //1.如果 供应商可支付，已有入库未检可支付，供应商可预付，支付进行款，预付进行款 这5个值 均为 0 的情况，
                        // 屏幕上净支价总额显示请帮我按 四舍五入到小数点2位，这样这个值就准确了,
                        //应付总额也按这个四舍五入的净支价总额来计算(理论上应该是0)

                        //2.如果 供应商可支付，已有入库未检可支付，供应商可预付，支付进行款，
                        //预付进行款 这5个值 其中有不等于 0 的情况，屏幕上净支价总额显示不变！

                        if (isJinWei&&model.ZhiTotal == 0 && model.ToSupplierTotal == 0 
                            && model.YuTotal == 0 && model.ZhiTotalIng == 0 && model.YuTotalIng == 0)
                        {
                            model.LastSupplierTotal =Convert.ToDecimal(string.Format("{0:n4}", model.LastSupplierTotal));
                            model.IsZhuan = true;
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
        public Model.JXC.CashFlow ReaderBind(IDataReader dataReader)
        {
            Model.JXC.CashFlow model = new Model.JXC.CashFlow();
            object ojb;
            model.PONO = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
            model.AE = dataReader["AE"].ToString();
            model.POTotal = Convert.ToDecimal(dataReader["ALLPOTotal"]);
            model.IsHanShui = Convert.ToBoolean(dataReader["IsPoFax"]);

            ojb = dataReader["FPTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPNoTOTAL = Convert.ToString(ojb);
            }
            ojb = dataReader["TuiTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TuiTotal = Convert.ToDecimal(ojb);
                model.POTotal -= Convert.ToDecimal(ojb);
            }
            ojb = dataReader["CostPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CostPrice = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["Profit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Profit = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["NotRuTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.NotRuTotal = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["NotRuSellTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.NotRuSellTotal = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["SellOutTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellOutTotal = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["LastCaiTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LastCaiTotal = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["SupplierTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierTotal = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["ItemTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ItemTotal = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["SellFPTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPTotal = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["InvoiceTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.InvoiceTotal = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["goodTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTotal = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["OtherCostm"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OtherCostm = Convert.ToDecimal(ojb);
            }
            //非库存采购金额
            //decimal NotKuCunTotal = 0;
            //ojb = dataReader["NotKuCunTotal"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    NotKuCunTotal = Convert.ToDecimal(ojb);

            //}

            // 如果采购 没有来自 库存 的商品，净库存出库成本金额=0
            ojb = dataReader["KuCunTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.NotKuCunTotal = Convert.ToDecimal(ojb);
            }


            ojb = dataReader["TopZJHLV"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TopZJHLV = Convert.ToDecimal(ojb);
            }
            else
            {
                model.IsNullTopZJHLV = true;
            }
            ojb = dataReader["TopZJZY"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TopZJZY = Convert.ToDecimal(ojb);
            }
            else
            {
                model.IsNullTopZJZY = true;
            }
            ojb = dataReader["TopYLNL"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TopYLNL = Convert.ToDecimal(ojb);
            }
            else
            {
                model.IsNullTopYLNL = true;
            }
            ojb = dataReader["TopYLLV"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TopYLLV = Convert.ToDecimal(ojb);
            }
            else
            {
                model.IsNullTopYLLV = true;
            }

            ojb = dataReader["SellInTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                //出库成本，需要扣减销售退货引起的出库成本部分
                model.SellOutTotal -= Convert.ToDecimal(ojb);
                //预估利润=原预估利润-（销售退货总销售额-销售退货总成本）
                model.CostPrice = model.CostPrice - Convert.ToDecimal(ojb);

                model.SellInTotal = Convert.ToDecimal(ojb);
            }

            ojb = dataReader["caiOutTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                //采购总额，需要扣减采购退货产生的金额
                model.LastCaiTotal -= Convert.ToDecimal(ojb);
            }


            ojb = dataReader["GoodSellPriceTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodSellPriceTotal = Convert.ToDecimal(ojb);
            }

            model.Profit = model.Profit - (model.TuiTotal - model.SellInTotal);


            return model;
        }

    }
}