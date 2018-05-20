using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
namespace VAN_OA
{
   

    [Serializable]
    public class DBHelp
    {
        private static string providerName = "System.Data.SqlClient";
        private static string DBConn = ConfigurationManager.ConnectionStrings["DBConn"].ToString();
        private static DbProviderFactory provider = DbProviderFactories.GetFactory(providerName);


        public static DbProviderFactory GetProviderFactory()
        {
            return provider;
        }

        public static bool ExeCommand(string sql)
        {
            
            bool result = false;
            using (DbConnection objConnection = provider.CreateConnection())
            {
                objConnection.ConnectionString = DBConn;
                objConnection.Open();
                DbCommand objCommand = objConnection.CreateCommand();
                objCommand.CommandText = sql;
                if (objCommand.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
                objConnection.Close();
            }
            return result;
        }

        public static object ExeScalar(string sql)
        {
            object result = null;
            using (DbConnection objConnection = provider.CreateConnection())
            {
                objConnection.ConnectionString = DBConn;
                objConnection.Open();
                DbCommand objCommand = objConnection.CreateCommand();
                objCommand.CommandText = sql;
                result = objCommand.ExecuteScalar();
                objConnection.Close();
            }
            return result;
        }

        public static SqlConnection getConn()
        {
            return new SqlConnection(DBConn);
        }

        public static DataTable getDataTable(string sql)
        {
            DataTable dataTable = new DataTable();
            using (DbConnection objConnection = provider.CreateConnection())
            {
                objConnection.ConnectionString = DBConn;
                objConnection.Open();
                DbCommand objCommand = objConnection.CreateCommand();
                objCommand.CommandText = sql;
                DbDataAdapter objApater = provider.CreateDataAdapter();
                objApater.SelectCommand = objCommand;
                objApater.Fill(dataTable);
                objConnection.Close();
            }
            return dataTable;
        }

        public static DataSet getDataSet(string sql)
        {
            DataSet ds = new DataSet();
            using (DbConnection objConnection = provider.CreateConnection())
            {
                objConnection.ConnectionString = DBConn;
                objConnection.Open();
                DbCommand objCommand = objConnection.CreateCommand();
                objCommand.CommandText = sql;
                DbDataAdapter objApater = provider.CreateDataAdapter();
                objApater.SelectCommand = objCommand;
                objApater.Fill(ds);
                objConnection.Close();
            }
            return ds;
        }

        public static string GetPagerSql(PagerDomain domain, string sqlBody, string strWhere, string orderby)
        {
            #region 查询总条数
            var strSql = new StringBuilder();
            strSql.Append("select count(1) ");
            strSql.Append(sqlBody.Substring(sqlBody.ToUpper().IndexOf(" FROM ")));
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" where "+strWhere);
            }
            var allCount = ExeScalar(strSql.ToString());
            domain.TotalCount = Convert.ToInt32(allCount);
            #endregion

            #region 拼接好的分页SQL
            strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by " + orderby);
            }
            strSql.AppendFormat(")AS Row, {0} ", sqlBody.Substring(sqlBody.ToUpper().IndexOf("SELECT ") + 7));
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" where "+strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", (domain.CurrentPageIndex - 1) * domain.PageSize + 1, domain.CurrentPageIndex * domain.PageSize);
            #endregion

            return strSql.ToString();
        }
    }
}

