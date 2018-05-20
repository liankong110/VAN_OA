using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using VAN_OA.Model.JXC;

namespace VAN_OA.Dal.JXC
{
    public class Sell_TuiSunCha_ReportService
    {
        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<JXC_REPORT> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sell_TuiSunCha_Report.PONo,Sell_TuiSunCha_Report.ProNo,RuTime,Supplier,goodInfo,GoodNum,GoodSellPrice,goodSellTotal,GoodPrice,goodTotal,t_GoodNums,t_GoodTotalChas,maoli ");
            strSql.Append(" FROM Sell_TuiSunCha_Report ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<Model.JXC.JXC_REPORT> list = new List<Model.JXC.JXC_REPORT>();
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
        public List<Model.JXC.JXC_REPORTTotal> GetListArray_Total(string strWhere,string having)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  CG_POOrder.PONo,CG_POOrder.PODate, CG_POOrder.GuestName,  ");
            strSql.Append(" sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal, ");
            strSql.Append(" sum(maoli) as maoliTotal,FPTotal,AE,INSIDE,AEPer as AEPer,INSIDEPer as INSIDEPer");
            strSql.Append(" from CG_POOrder ");            
            strSql.Append(" left join Sell_TuiSunCha_Report on CG_POOrder.PONo=Sell_TuiSunCha_Report.PONo ");
            strSql.Append(" left join (select sum(ZhangQi) as zhangqi,PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo");
            strSql.Append(" left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) as ntb2 on CG_POOrder.PONo=ntb2.PONo");
            strSql.Append(" where ifzhui=0  and CG_POOrder.Status='通过'");

            if (strWhere.Trim() != "")
            {
                strSql.Append( strWhere);
            }
            strSql.Append(" GROUP BY  CG_POOrder.PONo,CG_POOrder.PODate ,CG_POOrder.GuestName ,AE,INSIDE,FPTotal,AEPer,INSIDEPer ");

            if (having != "")
            {
                strSql.Append(having);
            }
            List<Model.JXC.JXC_REPORTTotal> list = new List<Model.JXC.JXC_REPORTTotal>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        JXC_REPORTTotal model = new JXC_REPORTTotal();
                        object ojb;
                        model.PONo = dataReader["PONo"].ToString();
                        ojb = dataReader["PODate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PODate = (DateTime)ojb;
                        }
                        model.GuestName = dataReader["GuestName"].ToString();
                        ojb = dataReader["goodSellTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.goodSellTotal = (decimal)ojb;
                        }
                        ojb = dataReader["goodTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.goodTotal = (decimal)ojb;
                        }
                        ojb = dataReader["maoliTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.maoliTotal = (decimal)ojb;
                        }
                        
                        model.AE = dataReader["AE"].ToString();
                        model.INSIDE = dataReader["INSIDE"].ToString();
                        ojb = dataReader["AEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AEPer = (decimal)ojb;
                        }
                        ojb = dataReader["INSIDEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.INSIDEPer = (decimal)ojb;
                        }

                       
                        
                        model.TrueLiRun=model.InvoiceTotal-model.goodTotal;

                        model.AETotal = model.AEPer * model.maoliTotal / 100;
                        model.InsidTotal = model.INSIDEPer * model.maoliTotal / 100;
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public Model.JXC.JXC_REPORT ReaderBind(IDataReader dataReader)
        {
            Model.JXC.JXC_REPORT model = new Model.JXC.JXC_REPORT();
            object ojb;
            model.PONo = dataReader["PONo"].ToString();
            model.ProNo = dataReader["ProNo"].ToString();
            ojb = dataReader["RuTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RuTime = (DateTime)ojb;
            }
            model.Supplier = dataReader["Supplier"].ToString();
            model.goodInfo = dataReader["goodInfo"].ToString();
            ojb = dataReader["GoodNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNum = (decimal)ojb;
            }
            ojb = dataReader["GoodSellPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodSellPrice = (decimal)ojb;
            }
            ojb = dataReader["goodSellTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.goodSellTotal = (decimal)ojb;
            }
            ojb = dataReader["GoodPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodPrice = (decimal)ojb;
            }
            ojb = dataReader["goodTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.goodTotal = (decimal)ojb;
            }
            ojb = dataReader["t_GoodNums"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.t_GoodNums = Convert.ToDecimal( ojb);
            }
            ojb = dataReader["t_GoodTotalChas"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.t_GoodTotalChas =Convert.ToDecimal( ojb);
            }
            ojb = dataReader["maoli"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.maoli = Convert.ToDecimal( ojb);
            }
            
            
            return model;
        }

    }
}
