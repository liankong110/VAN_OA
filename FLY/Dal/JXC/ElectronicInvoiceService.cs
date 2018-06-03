using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using VAN_OA.Model.JXC;

namespace VAN_OA.Dal.JXC
{
    public class ElectronicInvoiceService
    {

        public List<ElectronicInvoice> GetReport(string Where,string sumWhere)
        {
            List<ElectronicInvoice> list = new List<ElectronicInvoice>();
            string sql= string.Format(@"select  PONo,ProNo,busType,SupplierName,SupplierBrandName,SupplierBrandNo,Province,City
,sum(ActPay) as  SumActPay,PODate,AE,[SupplieSimpeName] from (
select
CAI_OrderInHouse.PONo,TB_SupplierInvoice.ProNo,'支' as busType,ActPay,
TB_SupplierInfo.SupplierName,SupplierBrandName,SupplierBrandNo,Province,City,TB_SupplierInvoice.Status,CreateName,PODate,AE,[SupplieSimpeName]
  from  TB_SupplierInvoices  
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join TB_SupplierInfo on TB_SupplierInfo.SupplieSimpeName=TB_SupplierInvoice.LastSupplier  and TB_SupplierInfo.Status='通过'
left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO and CG_POOrder.Status='通过' and IFZhui=0
union all
select CAI_POOrder.PONo,TB_SupplierAdvancePayment.ProNo as InvProNo,'预' as busType,SupplierInvoiceTotal as ActPay,
SupplierName,SupplierBrandName,SupplierBrandNo,Province,City,TB_SupplierAdvancePayment.Status,CreateName,CG_POOrder.PODate,CG_POOrder.AE,[SupplieSimpeName]
 from TB_SupplierAdvancePayment 
left join TB_SupplierAdvancePayments on  TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id 
left join CAI_POCai  on  TB_SupplierAdvancePayments.CaiIds=CAI_POCai.ids
left join CAI_POOrder  on   CAI_POCai.id=CAI_POOrder.id   
left join TB_SupplierInfo on TB_SupplierInfo.SupplieSimpeName=CAI_POCai.lastSupplier and TB_SupplierInfo.Status='通过'
left join CG_POOrder on CG_POOrder.PONO=CAI_POOrder.PONO and CG_POOrder.Status='通过' and IFZhui=0
) as TB   where  1=1  and Status='通过' and ActPay>0 and CreateName<>'admin' {0}
group by PONo,ProNo,busType,SupplierName,SupplierBrandName,SupplierBrandNo,Province,City,PODate,AE,[SupplieSimpeName]
{1}
order by ProNo desc", Where, sumWhere);
            list.AddRange(Data(sql));
            sql = string.Format(@"select  PONo,ProNo,busType,SupplierName,SupplierBrandName,SupplierBrandNo,Province,City
,sum(ActPay) as  SumActPay,PODate,AE,[SupplieSimpeName] from (
select CAI_OrderInHouse.PONo,TB_SupplierInvoice.ProNo,'支' as busType,ActPay,
TB_SupplierInfo.SupplierName,SupplierBrandName,SupplierBrandNo,Province,City,TB_SupplierInvoice.Status,CreateName,CG_POOrder.PODate,CG_POOrder.AE,[SupplieSimpeName]
from  TB_TempSupplierInvoice  
 left join TB_SupplierInvoices  ON TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds     
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_TempSupplierInvoice.SupplierInvoiceId
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO and CG_POOrder.Status='通过' and IFZhui=0
left join CAI_OrderChecks on CAI_OrderChecks.Ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
left join CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
left join TB_SupplierInfo on TB_SupplierInfo.SupplieSimpeName=TB_SupplierInvoice.LastSupplier  and TB_SupplierInfo.Status='通过'
where SupplierAdvanceId=0
union all
select CAI_POOrder.PONo,TB_SupplierAdvancePayment.ProNo as InvProNo,'预' as busType,SupplierInvoiceTotal as ActPay,
TB_SupplierInfo.SupplierName,SupplierBrandName,SupplierBrandNo,Province,City,TB_SupplierAdvancePayment.Status,CreateName,CG_POOrder.PODate,CG_POOrder.AE,[SupplieSimpeName] from 
 TB_TempSupplierInvoice  
 left join TB_SupplierInvoices  ON TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds 
left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayment.id=TB_TempSupplierInvoice.SupplierAdvanceId
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id  
left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO and CG_POOrder.Status='通过' and IFZhui=0
left join CAI_OrderChecks on CAI_OrderChecks.Ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
left join TB_SupplierInfo on TB_SupplierInfo.SupplieSimpeName=CAI_POCai.lastSupplier and TB_SupplierInfo.Status='通过'
left join CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
where SupplierInvoiceId=0
) as TB where  1=1  and Status='通过' {0}
group by PONo,ProNo,busType,SupplierName,SupplierBrandName,SupplierBrandNo,Province,City,PODate,AE,[SupplieSimpeName]
{1}
order by ProNo desc", Where, sumWhere);
            list.AddRange(Data(sql));
            return list;
        }

        private List<ElectronicInvoice> Data(string sql)
        {
            List<ElectronicInvoice> list = new List<ElectronicInvoice>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        var model = new ElectronicInvoice();
                        model.PONo = objReader["PONo"].ToString();
                        model.ProNo = objReader["ProNo"].ToString();
                        model.busType = objReader["busType"].ToString();
                        model.ActPay = Convert.ToDecimal(objReader["SumActPay"]);
                        object ojb;
                        ojb = objReader["SupplierName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SupplierName = ojb.ToString();
                        }
                        ojb = objReader["SupplierBrandName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SupplierBrandName = ojb.ToString();
                        }
                        ojb = objReader["SupplierBrandNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SupplierBrandNo = ojb.ToString();
                        }
                        ojb = objReader["Province"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Province = ojb.ToString();
                        }
                        ojb = objReader["City"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.City = ojb.ToString();
                        }
                        ojb = objReader["SupplieSimpeName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SupplieSimpeName = ojb.ToString();
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }

    }
}