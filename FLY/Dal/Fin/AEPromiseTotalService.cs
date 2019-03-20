using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using VAN_OA.Model.Fin;

namespace VAN_OA.Dal.Fin
{
    public class AEPromiseTotalService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.Fin.AEPromiseTotal model, SqlCommand sqlCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.YearNo != null)
            {
                strSql1.Append("YearNo,");
                strSql2.Append("" + model.YearNo + ",");
            }
            if (model.AE != null)
            {
                strSql1.Append("AE,");
                strSql2.Append("'" + model.AE + "',");
            }
            if (model.Department != null)
            {
                strSql1.Append("Department,");
                strSql2.Append("'" + model.Department + "',");
            }
            if (model.PromiseSellTotal != null)
            {
                strSql1.Append("PromiseSellTotal,");
                strSql2.Append("" + model.PromiseSellTotal + ",");
            }
            if (model.PromiseProfit != null)
            {
                strSql1.Append("PromiseProfit,");
                strSql2.Append("" + model.PromiseProfit + ",");
            }
            if (model.AddGuetSellTotal != null)
            {
                strSql1.Append("AddGuetSellTotal,");
                strSql2.Append("" + model.AddGuetSellTotal + ",");
            }
            if (model.AddGuestProfit != null)
            {
                strSql1.Append("AddGuestProfit,");
                strSql2.Append("'" + model.AddGuestProfit + "',");
            }

            strSql.Append("insert into AEPromiseTotal(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            sqlCommand.CommandText = strSql.ToString();

            object obj = sqlCommand.ExecuteScalar();
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.Fin.AEPromiseTotal model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.YearNo != null)
            {
                strSql1.Append("YearNo,");
                strSql2.Append("" + model.YearNo + ",");
            }
            if (model.AE != null)
            {
                strSql1.Append("AE,");
                strSql2.Append("'" + model.AE + "',");
            }
            if (model.Department != null)
            {
                strSql1.Append("Department,");
                strSql2.Append("'" + model.Department + "',");
            }
            if (model.PromiseSellTotal != null)
            {
                strSql1.Append("PromiseSellTotal,");
                strSql2.Append("" + model.PromiseSellTotal + ",");
            }
            if (model.PromiseProfit != null)
            {
                strSql1.Append("PromiseProfit,");
                strSql2.Append("" + model.PromiseProfit + ",");
            }
            if (model.AddGuetSellTotal != null)
            {
                strSql1.Append("AddGuetSellTotal,");
                strSql2.Append("" + model.AddGuetSellTotal + ",");
            }
            if (model.AddGuestProfit != null)
            {
                strSql1.Append("AddGuestProfit,");
                strSql2.Append("'" + model.AddGuestProfit + "',");
            }
            strSql.Append("insert into AEPromiseTotal(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            object obj = DBHelp.ExeScalar(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(VAN_OA.Model.Fin.AEPromiseTotal model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AEPromiseTotal set ");
            if (model.YearNo != null)
            {
                strSql.Append("YearNo=" + model.YearNo + ",");
            }
            if (model.AE != null)
            {
                strSql.Append("AE='" + model.AE + "',");
            }
            if (model.PromiseSellTotal != null)
            {
                strSql.Append("PromiseSellTotal=" + model.PromiseSellTotal + ",");
            }
            if (model.PromiseProfit != null)
            {
                strSql.Append("PromiseProfit=" + model.PromiseProfit + ",");
            }
            if (model.AddGuetSellTotal != null)
            {
                strSql.Append("AddGuetSellTotal=" + model.AddGuetSellTotal + ",");
            }
            if (model.AddGuestProfit != null)
            {
                strSql.Append("AddGuestProfit=" + model.AddGuestProfit + ",");
            }

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            bool rowsAffected = DBHelp.ExeCommand(strSql.ToString());
            return rowsAffected;
        }

        public bool Update(VAN_OA.Model.Fin.AEPromiseTotal model, SqlCommand sqlCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AEPromiseTotal set ");
            if (model.YearNo != null)
            {
                strSql.Append("YearNo=" + model.YearNo + ",");
            }
            if (model.AE != null)
            {
                strSql.Append("AE='" + model.AE + "',");
            }
            if (model.PromiseSellTotal != null)
            {
                strSql.Append("PromiseSellTotal=" + model.PromiseSellTotal + ",");
            }
            if (model.PromiseProfit != null)
            {
                strSql.Append("PromiseProfit=" + model.PromiseProfit + ",");
            }
            if (model.AddGuetSellTotal != null)
            {
                strSql.Append("AddGuetSellTotal=" + model.AddGuetSellTotal + ",");
            }
            if (model.AddGuestProfit != null)
            {
                strSql.Append("AddGuestProfit=" + model.AddGuestProfit + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            sqlCommand.CommandText = strSql.ToString();
            bool rowsAffected = sqlCommand.ExecuteNonQuery() > 0;
            return rowsAffected;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AEPromiseTotal ");
            strSql.Append(" where ID=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.Fin.AEPromiseTotal GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Id,YearNo,AE,Department,PromiseSellTotal,PromiseProfit,AddGuetSellTotal,AddGuestProfit");
            strSql.Append(" from AEPromiseTotal ");
            strSql.Append(" where AEPromiseTotal.ID=" + ID + "");

            VAN_OA.Model.Fin.AEPromiseTotal model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (objReader.Read())
                    {
                        model = ReaderBind(objReader);
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.Fin.AEPromiseTotal> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Id,YearNo,AE,Department,PromiseSellTotal,PromiseProfit,AddGuetSellTotal,AddGuestProfit");
            strSql.Append(" from AEPromiseTotal ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id desc");
            List<VAN_OA.Model.Fin.AEPromiseTotal> list = new List<VAN_OA.Model.Fin.AEPromiseTotal>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        var model = ReaderBind(objReader);

                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.Fin.AEPromiseTotal ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.Fin.AEPromiseTotal model = new VAN_OA.Model.Fin.AEPromiseTotal();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
                model.AE = dataReader["AE"].ToString();
                model.YearNo = Convert.ToInt32(dataReader["YearNo"]);
                model.PromiseSellTotal = Convert.ToDecimal(dataReader["PromiseSellTotal"]);
                model.PromiseProfit = Convert.ToDecimal(dataReader["PromiseProfit"]);
                model.AddGuestProfit = Convert.ToDecimal(dataReader["AddGuestProfit"]);
                model.AddGuetSellTotal = Convert.ToDecimal(dataReader["AddGuetSellTotal"]);
            }
          

            return model;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.Fin.AEPromiseTotal> GetListArrayReport(string strWhere,string year,string promiseSql)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@" select comname, AEPromiseTotal.Id,YearNo,TB.AE,Department,PromiseSellTotal,PromiseProfit,AddGuetSellTotal,AddGuestProfit,goodSellTotal,InvoTotal,goodTotal,
new_goodSellTotal, new_InvoTotal, new_goodTotal from(
select AE,SUM(goodSellTotal) AS goodSellTotal,SUM(InvoTotal) AS InvoTotal,SUM(goodTotal) AS goodTotal from (
select  sum(goodSellTotal) as goodSellTotal, isnull(avg(InvoTotal), 0) as InvoTotal, sum(goodTotal) + sum(t_goodTotalChas) as goodTotal, AE
from CG_POOrder left join JXC_REPORT on CG_POOrder.PONo = JXC_REPORT.PONo
left
join (select PoNo, SUM(Total) as InvoTotal from TB_ToInvoice  where TB_ToInvoice.state = '通过' group by PoNo) as newtable1
on CG_POOrder.PONo = newtable1.PONo
where ifzhui = 0  and CG_POOrder.Status = '通过'  and year(CG_POOrder.PODate)={0} and IsSpecial = 0
 GROUP BY  AE ,CG_POOrder.PONo) AS ALLTB GROUP BY AE ) AS TB
  LEFT JOIN
  (
select AE,SUM(new_goodSellTotal) AS new_goodSellTotal,SUM(new_InvoTotal) AS new_InvoTotal,SUM(new_goodTotal) AS new_goodTotal from (
select sum(goodSellTotal) as new_goodSellTotal,isnull(avg(InvoTotal), 0) as new_InvoTotal,sum(goodTotal) + sum(t_goodTotalChas) as new_goodTotal, AE
  from CG_POOrder left join JXC_REPORT on CG_POOrder.PONo = JXC_REPORT.PONo
left join(select PoNo, SUM(Total) as InvoTotal from TB_ToInvoice  where TB_ToInvoice.state = '通过' group by PoNo) as newtable1
on CG_POOrder.PONo = newtable1.PONo
where ifzhui = 0  and CG_POOrder.Status = '通过'  and year(CG_POOrder.PODate)={0} and IsSpecial = 0
and GuestPro = 1
  GROUP BY  AE,CG_POOrder.PONo ) AS NEWTB GROUP BY AE  ) AS TB2 ON TB.AE = TB2.AE
  left join (select *from AEPromiseTotal where {1}) as AEPromiseTotal on AEPromiseTotal.AE = TB.AE 
  left join tb_User on tb_User.loginName=TB.AE 
left join TB_Company on TB_Company.ComCode=tb_User.CompanyCode", year, promiseSql);
           
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by TB.AE desc");
            List<VAN_OA.Model.Fin.AEPromiseTotal> list = new List<VAN_OA.Model.Fin.AEPromiseTotal>();

            int i = 1;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        var model = ReaderBind(objReader);

                        object ojb;
                        
                        decimal invoTotal = 0;
                        decimal goodTotal = 0;
                       
                        decimal new_invoTotal = 0;
                        decimal new_goodTotal = 0;

                        model.AE = objReader["AE"].ToString();
                        ojb = objReader["goodSellTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ActualPromiseSellTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = objReader["invoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            invoTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = objReader["goodTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            goodTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = objReader["new_goodSellTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ActualAddGuetSellTotal = Convert.ToDecimal(ojb);
                        }
                       
                        ojb = objReader["new_goodTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            new_goodTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = objReader["new_InvoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            new_invoTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = objReader["comname"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Company = Convert.ToString(ojb);
                        }
                        model.ActualPromiseProfit = invoTotal - goodTotal;
                        model.ActualAddGuestProfit = new_invoTotal - new_goodTotal;
                        model.NO = i++;
                        model.YearNo = Convert.ToInt32(year);
                        list.Add(model);
                    }
                }
            }
            return list;
        } 

    }
}
