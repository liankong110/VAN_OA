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
using VAN_OA.Model.EFrom;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;


namespace VAN_OA.Dal.EFrom
{
    public class TB_POOrdersService
    {
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.EFrom.TB_POOrders model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Id != null)
            {
                strSql1.Append("Id,");
                strSql2.Append("" + model.Id + ",");
            }
            if (model.Time != null)
            {
                strSql1.Append("Time,");
                strSql2.Append("'" + model.Time + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.InvName != null)
            {
                strSql1.Append("InvName,");
                strSql2.Append("'" + model.InvName + "',");
            }
            if (model.Num != null)
            {
                strSql1.Append("Num,");
                strSql2.Append("" + model.Num + ",");
            }
            if (model.Unit != null)
            {
                strSql1.Append("Unit,");
                strSql2.Append("'" + model.Unit + "',");
            }
            if (model.CostPrice != null)
            {
                strSql1.Append("CostPrice,");
                strSql2.Append("" + model.CostPrice + ",");
            }
            if (model.SellPrice != null)
            {
                strSql1.Append("SellPrice,");
                strSql2.Append("" + model.SellPrice + ",");
            }
            if (model.OtherCost != null)
            {
                strSql1.Append("OtherCost,");
                strSql2.Append("" + model.OtherCost + ",");
            }
            if (model.ToTime != null)
            {
                strSql1.Append("ToTime,");
                strSql2.Append("'" + model.ToTime + "',");
            }
            if (model.Profit != null)
            {
                strSql1.Append("Profit,");
                strSql2.Append("" + model.Profit + ",");
            }
            strSql.Append("insert into TB_POOrders(");
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

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(VAN_OA.Model.EFrom.TB_POOrders model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_POOrders set ");
            //if (model.Id != null)
            //{
            //    strSql.Append("Id=" + model.Id + ",");
            //}
            if (model.Time != null)
            {
                strSql.Append("Time='" + model.Time + "',");
            }
            if (model.GuestName != null)
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            if (model.InvName != null)
            {
                strSql.Append("InvName='" + model.InvName + "',");
            }
            if (model.Num != null)
            {
                strSql.Append("Num=" + model.Num + ",");
            }
            if (model.Unit != null)
            {
                strSql.Append("Unit='" + model.Unit + "',");
            }
            if (model.CostPrice != null)
            {
                strSql.Append("CostPrice=" + model.CostPrice + ",");
            }
            if (model.SellPrice != null)
            {
                strSql.Append("SellPrice=" + model.SellPrice + ",");
            }
            if (model.OtherCost != null)
            {
                strSql.Append("OtherCost=" + model.OtherCost + ",");
            }
            if (model.ToTime != null)
            {
                strSql.Append("ToTime='" + model.ToTime + "',");
            }
            else
            {
                strSql.Append("ToTime= null ,");
            }
            if (model.Profit != null)
            {
                strSql.Append("Profit=" + model.Profit + ",");
            }
            else
            {
                strSql.Append("Profit= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Ids=" + model.Ids + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ids)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_POOrders ");
            strSql.Append(" where Ids=" + ids + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string ids ,SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_POOrders ");
            strSql.Append(" where Ids in(" + ids + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }
        /// 删除一条数据
        /// </summary>
        public void DeleteById(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_POOrders ");
            strSql.Append(" where Id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.TB_POOrders GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Ids,Id,Time,GuestName,InvName,Num,Unit,CostPrice,SellPrice,OtherCost,ToTime,Profit ");
            strSql.Append(" from TB_POOrders ");
            strSql.Append(" where Ids=" + Ids + "");
            VAN_OA.Model.EFrom.TB_POOrders model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind(dataReader);
                    }
                }
            }
            return model;
        }




        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.TB_POOrders> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Ids,Id,Time,GuestName,InvName,Num,Unit,CostPrice,SellPrice,OtherCost,ToTime,Profit ");
            strSql.Append(" FROM TB_POOrders ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.TB_POOrders> list = new List<VAN_OA.Model.EFrom.TB_POOrders>();

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
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.EFrom.TB_POOrders ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.TB_POOrders model = new VAN_OA.Model.EFrom.TB_POOrders();
            object ojb;
            ojb = dataReader["Ids"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ids = (int)ojb;
            }
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["Time"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Time = (DateTime)ojb;
            }
            model.GuestName = dataReader["GuestName"].ToString();
            model.InvName = dataReader["InvName"].ToString();
            ojb = dataReader["Num"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Num = (decimal)ojb;
            }
            model.Unit = dataReader["Unit"].ToString();
            ojb = dataReader["CostPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CostPrice = (decimal)ojb;
            }


            model.CostTotal = model.CostPrice * model.Num;
            ojb = dataReader["SellPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellPrice = (decimal)ojb;
            }

            model.SellTotal = model.SellPrice * model.Num;
            ojb = dataReader["OtherCost"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OtherCost = (decimal)ojb;
            }

            model.YiLiTotal = model.SellTotal-model.CostTotal - model.OtherCost;
           
            ojb = dataReader["ToTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ToTime = (DateTime)ojb;
            }
            ojb = dataReader["Profit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Profit = (decimal)ojb;
            }
            if (model.SellTotal != 0)
            {
                model.Profit = model.YiLiTotal / model.SellTotal * 100;
            }
            else if (model.YiLiTotal != 0)
            {
                model.Profit = -100;
            }
            else
            {
                model.Profit =0;
            }
            return model;
        }



    }
}
