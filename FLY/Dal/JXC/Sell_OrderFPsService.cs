using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAN_OA.Model.JXC;
using System.Data.SqlClient;
 
using System.Text;
using System.Data;


namespace VAN_OA.Dal.JXC
{
    public class Sell_OrderFPsService
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.Sell_OrderFPs model, SqlCommand objCommand)
        {


            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.id != null)
            {
                strSql1.Append("id,");
                strSql2.Append("" + model.id + ",");
            }
            if (model.GooId != null)
            {
                strSql1.Append("GooId,");
                strSql2.Append("" + model.GooId + ",");
            }
            if (model.GoodNum != null)
            {
                strSql1.Append("GoodNum,");
                strSql2.Append("" + model.GoodNum + ",");
            }

            if (model.GoodPrice != null)
            {
                strSql1.Append("GoodPrice,");
                strSql2.Append("" + model.GoodPrice + ",");
            }
            if (model.GoodRemark != null)
            {
                strSql1.Append("GoodRemark,");
                strSql2.Append("'" + model.GoodRemark + "',");
            }

            if (model.GoodSellPrice != null)
            {
                strSql1.Append("GoodSellPrice,");
                strSql2.Append("" + model.GoodSellPrice + ",");
            }

            if (model.SellOutPONO != null)
            {
                strSql1.Append("SellOutPONO,");
                strSql2.Append("'" + model.SellOutPONO + "',");
            }


            if (model.SellOutOrderId != null)
            {
                strSql1.Append("SellOutOrderId,");
                strSql2.Append("" + model.SellOutOrderId + ",");
            }


            
            strSql.Append("insert into Sell_OrderFPs(");
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
        public void Update(VAN_OA.Model.JXC.Sell_OrderFPs model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sell_OrderFPs set ");
            if (model.id != null)
            {
                strSql.Append("id=" + model.id + ",");
            }
            if (model.GooId != null)
            {
                strSql.Append("GooId=" + model.GooId + ",");
            }
            if (model.GoodNum != null)
            {
                strSql.Append("GoodNum=" + model.GoodNum + ",");
            }

            if (model.GoodSellPrice != null)
            {
                strSql.Append("GoodSellPrice=" + model.GoodSellPrice + ",");
            }

         

            if (model.GoodRemark != null)
            {
                strSql.Append("GoodRemark='" + model.GoodRemark + "',");
            }
            else
            {
                strSql.Append("GoodRemark= null ,");
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
            strSql.Append("delete from Sell_OrderFPs ");
            strSql.Append(" where Ids=" + ids + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string ids, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Sell_OrderFPs ");
            strSql.Append(" where Ids in(" + ids + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }
        /// 删除一条数据
        /// </summary>
        public void DeleteById(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Sell_OrderFPs ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.Sell_OrderFPs GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Ids,Sell_OrderFPs.id,GooId,GoodNum,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,GoodSellPrice,SellOutPONO,SellOutOrderId");
            strSql.Append(" from Sell_OrderFPs left join TB_Good on TB_Good.GoodId=Sell_OrderFPs.GooId  ");
            strSql.Append(" where Ids=" + Ids + "");
            VAN_OA.Model.JXC.Sell_OrderFPs model = null;
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
        public List<VAN_OA.Model.JXC.Sell_OrderFPs> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Ids,Sell_OrderFPs.id,GooId,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,GoodSellPrice,SellOutPONO,SellOutOrderId");
            strSql.Append(" from Sell_OrderFPs left join TB_Good on TB_Good.GoodId=Sell_OrderFPs.GooId  ");
           

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.Sell_OrderFPs> list = new List<VAN_OA.Model.JXC.Sell_OrderFPs>();

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
        public VAN_OA.Model.JXC.Sell_OrderFPs ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.Sell_OrderFPs model = new VAN_OA.Model.JXC.Sell_OrderFPs();
            object ojb;
            ojb = dataReader["Ids"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ids = (int)ojb;
            }
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            ojb = dataReader["GooId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GooId = (int)ojb;
            }
            ojb = dataReader["GoodNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNum = (decimal)ojb;
            }

            ojb = dataReader["GoodPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodPrice = (decimal)ojb;
            }

            model.GoodRemark = dataReader["GoodRemark"].ToString();
            model.Total = model.GoodNum * model.GoodPrice;            

            ojb = dataReader["GoodNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNo = ojb.ToString();
            }
            ojb = dataReader["GoodName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodName = ojb.ToString();
            }
            ojb = dataReader["GoodSpec"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodSpec = ojb.ToString();
            }

            ojb = dataReader["GoodModel"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Good_Model = ojb.ToString();
            }
            ojb = dataReader["GoodUnit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodUnit = ojb.ToString();
            }

            ojb = dataReader["GoodTypeSmName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTypeSmName = ojb.ToString();
            }
            ojb = dataReader["GoodSellPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodSellPrice =Convert.ToDecimal( ojb);
            }


            model.GoodSellPriceTotal = model.GoodSellPrice * model.GoodNum;
          
            model.SellOutPONO = ojb.ToString();
            ojb = dataReader["SellOutPONO"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellOutPONO = ojb.ToString();
            }

            ojb = dataReader["SellOutOrderId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellOutOrderId = (int)ojb;
            }


            return model;
        }



    }
}
