using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using VAN_OA.Model.JXC;

namespace VAN_OA.Dal.JXC
{
    public class Sell_TuiSunChaService
    {
        public int addTran(List<Sell_OrderInHouses> POOrders)
        {
            int id = 0;
        
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
            
                try
                {
                    foreach (var sellOrderInHousese in POOrders)
                    {
                        Sell_TuiSunCha model = new Sell_TuiSunCha();
                        model.SellTuiIds = sellOrderInHousese.Ids;
                        objCommand.Parameters.Clear();
                        id = Add(model, objCommand);
                    }
                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return 0;

                }
                return id;
            }
        }
   
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.Sell_TuiSunCha model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.SellTuiIds != null)
            {
                strSql1.Append("SellTuiIds,");
                strSql2.Append("" + model.SellTuiIds + ",");
            }
            strSql.Append("insert into Sell_TuiSunCha(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");

            int result;
            objCommand.CommandText = strSql.ToString();
            object obj = objCommand.ExecuteScalar();
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }
    }
}
