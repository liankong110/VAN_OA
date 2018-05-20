using System;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;

namespace VAN_OA.JXC
{
    public partial class WFTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new VAN_OA.Dal.JXC.CG_POOrdersService().GetListArrayToFpsAndUpdatePoStatue("P201500649", "通过");
            new VAN_OA.Dal.JXC.CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus("P201500649");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
 //           TB_SupplierInvoicesService ordersSer = new TB_SupplierInvoicesService();
 //           List<SupplierToInvoiceView> orders = ordersSer.GetListArray(" SupplierInvoiceTotal<0 and TB_SupplierInvoices.id in (select Id from TB_SupplierInvoice where Status='通过')");
 //           new TB_SupplierInvoiceService().SetRuKuPayStatus1(orders);
 ////更新供应商名称


            string sql = @"select CAI_OrderChecks.* from CAI_OrderChecks
where Ids in (
select   CAI_OrderChecks.ids  
from CAI_POCai
left join CAI_OrderChecks on CAI_OrderChecks.CaiId=CAI_POCai.Ids
left join CAI_OrderInHouses on CAI_OrderInHouses.OrderCheckIds=CAI_OrderChecks.Ids 
left join 
(
select CaiIds,Sum(SupplierInvoiceTotal) as  SupplierInvoiceTotal,min(SupplierInvoiceDate) as minSupplierInvoiceDate  from 
TB_SupplierAdvancePayments 
left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.id
where status='通过'  
group by CaiIds
)
as tb1 on CAI_POCai.IDs=tb1.CaiIds 
where SupplierInvoiceTotal is not null --and CAI_OrderInHouses.ids  is not null

AND CAI_OrderInHouses.ids NOT IN (select RuIds FROM TB_SupplierInvoices WHERE IsYuFu=1)
)
order by checkId";

            var dt = DBHelp.getDataTable(sql);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            List<CAI_OrderChecks> POOrders = new List<CAI_OrderChecks>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                if (!dic.ContainsKey(dr["CheckId"].ToString()))
                {
                    dic.Add(dr["CheckId"].ToString(), dr["SupplierName"].ToString());
                }
                CAI_OrderChecks model = new CAI_OrderChecks();
                model.Ids = Convert.ToInt32(dr["Ids"]);
                model.CaiId = Convert.ToInt32(dr["CaiId"]);
                model.CheckId = Convert.ToInt32(dr["CheckId"]);
                POOrders.Add(model);
            }

            foreach (string key in dic.Keys)
            {
                string checkIds = "";
                string caiIds = "";
                int id = Convert.ToInt32(key);
                foreach (var model in POOrders.FindAll(t => t.CheckId == id))
                {
                    checkIds += model.Ids + ",";
                    caiIds += model.CaiId + ",";
                }
                if (checkIds.Length > 0)
                {
                    checkIds = checkIds.Substring(0, checkIds.Length - 1);
                    caiIds = caiIds.Substring(0, caiIds.Length - 1);
                }
                new TB_SupplierInvoiceService().AddSupplierInvoice(checkIds, caiIds, Session["LoginName"].ToString(), Convert.ToInt32(Session["currentUserId"].ToString()), key);
            }


            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");

        }

       

      
    }
}
