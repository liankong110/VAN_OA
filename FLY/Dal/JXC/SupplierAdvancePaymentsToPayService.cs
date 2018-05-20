using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAN_OA.Model.JXC;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace VAN_OA.Dal.JXC
{
    public class SupplierAdvancePaymentsToPayService
    {
        public List<SupplierAdvancePaymentsToPay> GetListArray(string checkIds, string caiIds)
        {
            //LastTruePrice=lastPrice
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select CAI_POCai.ids, CAI_OrderChecks.ids as checkIds,CAI_OrderInHouses.ids as inHouseIds, Num as allNum,CAI_POCai.LastTruePrice as lastPrice,CheckNum ,SupplierInvoiceTotal,minSupplierInvoiceDate
from CAI_POCai
left join CAI_OrderChecks on CAI_OrderChecks.CaiId=CAI_POCai.Ids
left join CAI_OrderInHouses on CAI_OrderInHouses.OrderCheckIds=CAI_OrderChecks.Ids 
left join 
(
select CaiIds,Sum(SupplierInvoiceTotal) as  SupplierInvoiceTotal,min(SupplierInvoiceDate) as minSupplierInvoiceDate  from 
TB_SupplierAdvancePayments 
left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.id
where status='通过' and CaiIds in ({0})
group by CaiIds
)
as tb1 on CAI_POCai.IDs=tb1.CaiIds 
where SupplierInvoiceTotal is not null
and CAI_POCai.ids in ({0})
and CAI_OrderChecks.Ids in ({1}) ", caiIds,checkIds);
            
            
            
            List<SupplierAdvancePaymentsToPay> list = new List<SupplierAdvancePaymentsToPay>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        list.Add(ReaderBind(objReader));
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public SupplierAdvancePaymentsToPay ReaderBind(IDataReader dataReader)
        {
            SupplierAdvancePaymentsToPay model = new SupplierAdvancePaymentsToPay();
            object ojb;
            ojb = dataReader["ids"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ids = (int)ojb;
            }
            ojb = dataReader["checkIds"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.checkIds = (int)ojb;
            }
            ojb = dataReader["inHouseIds"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.inHouseIds = (int)ojb;
            }
            ojb = dataReader["allNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.allNum = (decimal)ojb;
            }
            ojb = dataReader["lastPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.lastPrice = (decimal)ojb;
            }
            ojb = dataReader["CheckNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CheckNum = (decimal)ojb;
            }
            ojb = dataReader["SupplierInvoiceTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoiceTotal = (decimal)ojb;
            }
            ojb = dataReader["minSupplierInvoiceDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MinSupplierInvoiceDate = Convert.ToDateTime(ojb);
            }
            return model;
        }
    }
}
