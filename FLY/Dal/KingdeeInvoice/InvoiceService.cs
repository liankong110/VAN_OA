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
    public class InvoiceService
    {
        public static string InvoiceServer = "";
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from "+InvoiceServer+"[KingdeeInvoice].[dbo].[Invoice] ");
            strSql.Append(" where Id=" + ID + ";");
            strSql.Append("update KIS.[KingdeeInvoice].[dbo].[Invoice]  set IsDeleted=1 ");
            strSql.Append(" where Id=" + ID + ";");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool TrueDelete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + InvoiceServer + "[KingdeeInvoice].[dbo].[Invoice] ");
            strSql.Append(" where Id=" + ID + ";");
            //strSql.Append("delete KIS.[KingdeeInvoice].[dbo].[Invoice] ");
            //strSql.Append(" where Id=" + ID + ";");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 获得金蝶数据
        /// </summary>
        public List<VAN_OA.Model.KingdeeInvoice.Invoice> GetListArray(string strWhere,string invoiceServer)
        {           
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select IsDeleted,Isorder,Id,GuestName,InvoiceNumber,Total,CreateDate,IsAccount,Received,BillDate,FPNoStyle ");
            strSql.Append(" FROM " + invoiceServer + "[KingdeeInvoice].[dbo].[Invoice] ");
            strSql.Append(@" left join
(
select *from (
select *,
ROW_NUMBER() over(partition by FPNo order by FPNoStyle desc) rowNum from
(
select FPNo,FPNoStyle from Sell_OrderFP where status='通过' group By FPNo,FPNoStyle
) as FP ) as FP where rowNum=1
) AS TB ON TB.FPNo=[Invoice].InvoiceNumber ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CreateDate desc,InvoiceNumber desc ");
            List<VAN_OA.Model.KingdeeInvoice.Invoice> list = new List<VAN_OA.Model.KingdeeInvoice.Invoice>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        var model=ReaderBind(objReader);
                        //if (!string.IsNullOrEmpty(invoiceServer))
                        //{
                        //    model.IsDeleted = true;
                        //}
                       
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.KingdeeInvoice.Invoice ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.KingdeeInvoice.Invoice model = new VAN_OA.Model.KingdeeInvoice.Invoice();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.GuestName = dataReader["GuestName"].ToString();
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
            ojb = dataReader["IsDeleted"];
            if (ojb != null && ojb != DBNull.Value)
            {
                if (ojb.ToString() == "1")
                {
                    model.IsDeleted = true;
                }

            }
          

            ojb = dataReader["BillDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BillDate =Convert.ToDateTime( ojb);
            }
            ojb = dataReader["FPNoStyle"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPNoStyle = Convert.ToString(ojb);
            }
            
            return model;
        }
    }
}
