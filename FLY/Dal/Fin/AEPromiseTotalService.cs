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

            strSql.Append(" order by TransactionDate desc");
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
            }
            model.AE = dataReader["AE"].ToString();
            model.YearNo = Convert.ToInt32(dataReader["YearNo"]);
            model.PromiseSellTotal = Convert.ToDecimal(dataReader["PromiseSellTotal"]);
            model.PromiseProfit = Convert.ToDecimal(dataReader["PromiseProfit"]);
            model.AddGuestProfit = Convert.ToDecimal(dataReader["AddGuestProfit"]);
            model.AddGuetSellTotal = Convert.ToDecimal(dataReader["AddGuetSellTotal"]);

            return model;
        }



    }
}
