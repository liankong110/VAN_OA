using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class BaseKeyValueService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.BaseKeyValue model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.TypeName != null)
            {
                strSql1.Append("TypeName,");
                strSql2.Append("'" + model.TypeName + "',");
            }
            if (model.TypeValue != null)
            {
                strSql1.Append("YearDate,");
                strSql2.Append("'" + model.TypeValue + "',");
            }            
            strSql.Append("insert into BaseKeyValue(");
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
        public bool Update(VAN_OA.Model.BaseInfo.BaseKeyValue model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BaseKeyValue set ");
            if (model.TypeName != null)
            {
                strSql.Append("TypeName='" + model.TypeName + "',");
            }
            if (model.TypeValue != null)
            {
                strSql.Append("TypeValue='" + model.TypeValue + "',");
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
            strSql.Append("delete from BaseKeyValue ");
            strSql.Append(" where Id=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.BaseKeyValue GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TypeName,TypeValue ");
            strSql.Append(" FROM BaseKeyValue ");
            strSql.Append(" where Id=" + ID + "");

            VAN_OA.Model.BaseInfo.BaseKeyValue model = null;
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
        public List<VAN_OA.Model.BaseInfo.BaseKeyValue> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,TypeName,TypeValue ");
            strSql.Append(" FROM BaseKeyValue ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id desc ");
            List<VAN_OA.Model.BaseInfo.BaseKeyValue> list = new List<VAN_OA.Model.BaseInfo.BaseKeyValue>();

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
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.BaseInfo.BaseKeyValue ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.BaseKeyValue model = new VAN_OA.Model.BaseInfo.BaseKeyValue();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.TypeValue = dataReader["TypeValue"].ToString();
            model.TypeName = dataReader["TypeName"].ToString();           
            return model;
        }
    }
}