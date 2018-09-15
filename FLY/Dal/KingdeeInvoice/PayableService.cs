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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace VAN_OA.Dal.KingdeeInvoice
{
    public class PayableService
    {
        public static string InvoiceServer = "";
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from "+InvoiceServer+ "[KingdeeInvoice].[dbo].[Payable] ");
            strSql.Append(" where Id=" + ID + ";");
            strSql.Append("update KIS.[KingdeeInvoice].[dbo].[Payable]  set IsDeleted=1 ");
            strSql.Append(" where Id=" + ID + ";");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool TrueDelete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + InvoiceServer + "[KingdeeInvoice].[dbo].[Payable] ");
            strSql.Append(" where Id=" + ID + ";");
            strSql.Append("delete KIS.[KingdeeInvoice].[dbo].[Payable] ");
            strSql.Append(" where Id=" + ID + ";");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 获得金蝶数据
        /// </summary>
        public List<VAN_OA.Model.KingdeeInvoice.Payable> GetListArray(string strWhere,string invoiceServer)
        {           
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Isorder,Id,SupplierName,InvoiceNumber,Total,CreateDate,IsAccount,Received,BillDate,CaiFpType ");
            strSql.Append(" FROM " + invoiceServer + "[KingdeeInvoice].[dbo].[Payable] ");
            strSql.Append(@" left join
(
selecT SupplierFPNo,CaiFpType FROM TB_SupplierInvoice
LEFT JOIN TB_SupplierInvoices ON TB_SupplierInvoice.Id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses on CAI_OrderInHouses.Ids=TB_SupplierInvoices.RuIds
left join CAI_OrderChecks on CAI_OrderChecks.ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
where Status='通过' and IsYuFu=0
UNION 
selecT SupplierFPNo,CaiFpType FROM TB_SupplierAdvancePayment
LEFT JOIN TB_SupplierAdvancePayments ON TB_SupplierAdvancePayment.Id=TB_SupplierAdvancePayments.Ids
left join CAI_POCai on CAI_POCai.Ids=TB_SupplierAdvancePayments.CaiIds
where Status='通过'
) AS TB ON TB.SupplierFPNo=[Payable].InvoiceNumber ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CreateDate desc,InvoiceNumber desc ");
            List<VAN_OA.Model.KingdeeInvoice.Payable> list = new List<VAN_OA.Model.KingdeeInvoice.Payable>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        var model=ReaderBind(objReader);
                        if (!string.IsNullOrEmpty(invoiceServer))
                        {
                            model.IsDeleted = true;
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
        public VAN_OA.Model.KingdeeInvoice.Payable ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.KingdeeInvoice.Payable model = new VAN_OA.Model.KingdeeInvoice.Payable();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.SupplierName = dataReader["SupplierName"].ToString();
            model.InvoiceNumber = dataReader["InvoiceNumber"].ToString();
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Total = (decimal)ojb;
            }
            ojb = dataReader["CreateDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateDate = (DateTime)ojb;
            }
            ojb = dataReader["IsAccount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsAccount = (int)ojb;
            }
            ojb = dataReader["Received"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Received = (decimal)ojb;
            }
            ojb = dataReader["Isorder"];
            if (ojb != null && ojb != DBNull.Value)
            {
                if (ojb.ToString() == "1")
                {
                    model.Isorder = true;
                }
               
            }
            ojb = dataReader["BillDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BillDate =Convert.ToDateTime( ojb);
            }
            ojb = dataReader["CaiFpType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPNoStyle = Convert.ToString(ojb);
            }
            
            return model;
        }
    }
}
