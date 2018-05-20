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
    public class CAI_OrderOutHousesService
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.CAI_OrderOutHouses model, SqlCommand objCommand)
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

            if (model.OrderCheckIds != null)
            {
                strSql1.Append("OrderCheckIds,");
                strSql2.Append("'" + model.OrderCheckIds + "',");
            }

            if (model.QingGouPer != null)
            {
                strSql1.Append("QingGouPer,");
                strSql2.Append("'" + model.QingGouPer + "',");
            }
            
            
            strSql.Append("insert into CAI_OrderOutHouses(");
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
        public void Update(VAN_OA.Model.JXC.CAI_OrderOutHouses model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CAI_OrderOutHouses set ");
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

            if (model.GoodPrice != null)
            {
                strSql.Append("GoodPrice=" + model.GoodPrice + ",");
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
            strSql.Append("delete from CAI_OrderOutHouses ");
            strSql.Append(" where Ids=" + ids + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string ids, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CAI_OrderOutHouses ");
            strSql.Append(" where Ids in(" + ids + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }
        /// 删除一条数据
        /// </summary>
        public void DeleteById(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CAI_OrderOutHouses ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.CAI_OrderOutHouses GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Ids,CAI_OrderOutHouses.id,GooId,GoodNum,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,OrderCheckIds,QingGouPer");
            strSql.Append(" from CAI_OrderOutHouses left join TB_Good on TB_Good.GoodId=CAI_OrderOutHouses.GooId ");
            strSql.Append(" where Ids=" + Ids + "");
            VAN_OA.Model.JXC.CAI_OrderOutHouses model = null;
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
        public List<VAN_OA.Model.JXC.CAI_OrderOutHouses> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" GoodAreaNumber,CaiLastTruePrice,Ids,CAI_OrderOutHouses.id,GooId,GoodNum,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,OrderCheckIds,QingGouPer");
            strSql.Append(" from CAI_OrderOutHouses left join TB_Good on TB_Good.GoodId=CAI_OrderOutHouses.GooId  ");
            strSql.Append(" left join  (SELECT  IDS AS InId,CaiLastTruePrice FROM CAI_OrderInHouses ) AS TEMP on TEMP.InId=CAI_OrderOutHouses.OrderCheckIds");
           

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CAI_OrderOutHouses> list = new List<VAN_OA.Model.JXC.CAI_OrderOutHouses>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        object ojb;
                        ojb = dataReader["CaiLastTruePrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiLastTruePrice = Convert.ToDecimal(ojb);
                        }
                        model.GoodAreaNumber = dataReader["GoodAreaNumber"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        public List<VAN_OA.Model.JXC.CAI_OrderOutHouses> GetListArrayToPoNo(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" RuTime,Status,Ids,CAI_OrderOutHouses.id,GooId,GoodNum,GoodNum,GoodPrice,GoodRemark,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,OrderCheckIds,QingGouPer");
            strSql.Append(" from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.Id=CAI_OrderOutHouse.Id left join TB_Good on TB_Good.GoodId=CAI_OrderOutHouses.GooId  ");


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CAI_OrderOutHouses> list = new List<VAN_OA.Model.JXC.CAI_OrderOutHouses>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        object ojb;
                        ojb = dataReader["Status"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Status = ojb.ToString();
                        }
                        ojb = dataReader["RuTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.RuTime = Convert.ToDateTime(ojb);
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
        public VAN_OA.Model.JXC.CAI_OrderOutHouses ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.CAI_OrderOutHouses model = new VAN_OA.Model.JXC.CAI_OrderOutHouses();
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

            ojb = dataReader["OrderCheckIds"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OrderCheckIds =Convert.ToInt32( ojb);
            }
            ojb = dataReader["QingGouPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.QingGouPer = Convert.ToString(ojb);
            }

          
            
            return model;
        }



    }
}
