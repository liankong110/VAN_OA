using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using VAN_OA.Model.JXC;
using System.Data;

namespace VAN_OA.Dal.JXC
{
    public class CaiNotRuViewService
    {
        public List<VAN_OA.Model.JXC.CaiNotRuView> GetCaiNotRuViewList(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SupplierInvoicePrice,GoodAreaNumber,IsHanShui,ProNo,PONo,POName,PODate,POTotal,GuestName,AE,GoodNo,GoodName,GoodSpec,Num,totalOrderNum,lastSupplier,lastPrice,POGoodSum,MinInHouseDate,InHouseSum,CaiGoodSum,GoodNum,GoodAvgPrice ");
            strSql.Append(" FROM CaiNotRuView ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by ProNo desc,GoodNo ");
            List<VAN_OA.Model.JXC.CaiNotRuView> list = new List<VAN_OA.Model.JXC.CaiNotRuView>();

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
                        object ojb = dataReader["SupplierInvoicePrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SupplierInvoicePrice = (decimal)ojb;
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
        public CaiNotRuView ReaderBind(IDataReader dataReader)
        {
            CaiNotRuView model = new CaiNotRuView();
            object ojb;
            model.ProNo = dataReader["ProNo"].ToString();
            model.PONo = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
            ojb = dataReader["PODate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PODate = (DateTime)ojb;
            }
            ojb = dataReader["POTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POTotal = (decimal)ojb;
            }
            ojb = dataReader["IsHanShui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsHanShui = (int)ojb;
            }
            model.GuestName = dataReader["GuestName"].ToString();
            model.AE = dataReader["AE"].ToString();
            model.GoodNo = dataReader["GoodNo"].ToString();
            model.GoodName = dataReader["GoodName"].ToString();
            model.GoodSpec = dataReader["GoodSpec"].ToString();
            ojb = dataReader["Num"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Num = (decimal)ojb;
            }
            ojb = dataReader["totalOrderNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.totalOrderNum = (decimal)ojb;
            }
            model.lastSupplier = dataReader["lastSupplier"].ToString();
            ojb = dataReader["lastPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.lastPrice = (decimal)ojb;
            }
            ojb = dataReader["POGoodSum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POGoodSum = (decimal)ojb;
            }
            ojb = dataReader["MinInHouseDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MinInHouseDate = (DateTime)ojb;
            }
            ojb = dataReader["InHouseSum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.InHouseSum = (decimal)ojb;
            }
            ojb = dataReader["CaiGoodSum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CaiGoodSum = (decimal)ojb;
            }
            ojb = dataReader["GoodNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNum = (decimal)ojb;
            }
            ojb = dataReader["GoodAvgPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodAvgPrice = (decimal)ojb;
            }
            //decimal OrderCaiNum = 0;
            //ojb = dataReader["OrderCaiNum"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    OrderCaiNum = (decimal)ojb;
            //}
            if (model.POGoodSum != 0)
                model.POGoodSum = model.POGoodSum - model.Num;
            return model;
        }




        public List<VAN_OA.Model.JXC.PONotCaiView> GetPONotCaiViewList(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT ProNo,[PONo],[POName],[PODate],[POTotal],[GuestName],[AE],[GoodNo],[GoodName],[GoodSpec],[Num],[caiNums],[lastNum],[Supplier],[SupperPrice],GoodTypeSmName from [PONotCaiView] ");
           
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.PONotCaiView> list = new List<VAN_OA.Model.JXC.PONotCaiView>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind_PONotCaiView(dataReader));
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public PONotCaiView ReaderBind_PONotCaiView(IDataReader dataReader)
        {
            PONotCaiView model = new PONotCaiView();
            object ojb;
           
            model.PONo = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
            ojb = dataReader["PODate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PODate = (DateTime)ojb;
            }
            ojb = dataReader["POTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POTotal = (decimal)ojb;
            }
            model.GuestName = dataReader["GuestName"].ToString();
            model.AE = dataReader["AE"].ToString();
            model.GoodNo = dataReader["GoodNo"].ToString();
            model.GoodName = dataReader["GoodName"].ToString();
            model.GoodSpec = dataReader["GoodSpec"].ToString();
            model.GoodTypeSmName = dataReader["GoodTypeSmName"].ToString();
            
            ojb = dataReader["Num"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Num = (decimal)ojb;
            }
            ojb = dataReader["caiNums"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CaiGoodSum = (decimal)ojb;
            }
            ojb = dataReader["lastNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.lastNum = (decimal)ojb;
            }
            model.lastSupplier = dataReader["Supplier"].ToString();
            ojb = dataReader["SupperPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.lastPrice = (decimal)ojb;
            }
            model.ProNo = dataReader["ProNo"].ToString();
            return model;
        }

    }
}
