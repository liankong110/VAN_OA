using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;


namespace VAN_OA.Dal.BaseInfo
{
    public class TB_GoodsSmTypeService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.TB_GoodsSmType model)
        {           
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.GoodTypeName != null)
            {
                strSql1.Append("GoodTypeName,");
                strSql2.Append("'" + model.GoodTypeName + "',");
            }
            if (model.GoodTypeSmName != null)
            {
                strSql1.Append("GoodTypeSmName,");
                strSql2.Append("'" + model.GoodTypeSmName + "',");
            }
            strSql.Append("insert into TB_GoodsSmType(");
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
        public bool Update(VAN_OA.Model.BaseInfo.TB_GoodsSmType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_GoodsSmType set ");
            if (model.GoodTypeName != null)
            {
                strSql.Append("GoodTypeName='" + model.GoodTypeName + "',");
            }
            else
            {
                strSql.Append("GoodTypeId= null ,");
            }
            if (model.GoodTypeSmName != null)
            {
                strSql.Append("GoodTypeSmName='" + model.GoodTypeSmName + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            bool rowsAffected = DBHelp.ExeCommand(strSql.ToString());
            return rowsAffected;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_GoodsSmType ");
            strSql.Append(" where ID=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.TB_GoodsSmType GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TB_GoodsSmType.Id,GoodTypeId,GoodTypeSmName, GoodTypeName");
            strSql.Append(" FROM TB_GoodsSmType ");
            strSql.Append(" where TB_GoodsSmType.ID=" + ID + "");

            VAN_OA.Model.BaseInfo.TB_GoodsSmType model = null;
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
        public List<VAN_OA.Model.BaseInfo.TB_GoodsSmType> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TB_GoodsSmType.Id,GoodTypeSmName, GoodTypeName");
            strSql.Append(" FROM TB_GoodsSmType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by GoodTypeName,GoodTypeSmName ");
            List<VAN_OA.Model.BaseInfo.TB_GoodsSmType> list = new List<VAN_OA.Model.BaseInfo.TB_GoodsSmType>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        list.Add(ReaderBind(objReader));
                    }
                }
            }
            return list;
        }




        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.BaseInfo.TB_GoodsSmType> GetListArrayToddl(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GoodTypeSmName ");
            strSql.Append(" FROM TB_GoodsSmType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" group by GoodTypeSmName order by GoodTypeSmName ");
            List<VAN_OA.Model.BaseInfo.TB_GoodsSmType> list = new List<VAN_OA.Model.BaseInfo.TB_GoodsSmType>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        VAN_OA.Model.BaseInfo.TB_GoodsSmType model = new VAN_OA.Model.BaseInfo.TB_GoodsSmType();

                        model.GoodTypeSmName = objReader["GoodTypeSmName"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }


      


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.BaseInfo.TB_GoodsSmType ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.TB_GoodsSmType model = new VAN_OA.Model.BaseInfo.TB_GoodsSmType();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["GoodTypeName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTypeName = ojb.ToString();
            }
           
            model.GoodTypeSmName = dataReader["GoodTypeSmName"].ToString();


            return model;
        }



    }
}
