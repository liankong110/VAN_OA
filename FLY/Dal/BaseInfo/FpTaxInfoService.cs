using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class FpTaxInfoService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.FpTaxInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.FpType != null)
            {
                strSql1.Append("FpType,");
                strSql2.Append("'" + model.FpType + "',");
            }
            if (model.Tax != null)
            {
                strSql1.Append("Tax,");
                strSql2.Append("" + model.Tax + ",");
            }
            

            strSql.Append("insert into FpTaxInfo(");
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
        public bool Update(VAN_OA.Model.BaseInfo.FpTaxInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FpTaxInfo set ");
            if (model.FpType != null)
            {
                strSql.Append("FpType='" + model.FpType + "',");
            }
            if (model.Tax != null)
            {
                strSql.Append("Tax=" + model.Tax + ",");
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
            strSql.Append("delete from FpTaxInfo ");
            strSql.Append(" where Id=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.FpTaxInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select Id,FpType,Tax ");
            strSql.Append(" FROM FpTaxInfo ");
            strSql.Append(" where ID=" + ID + "");

            VAN_OA.Model.BaseInfo.FpTaxInfo model = null;
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
        public List<VAN_OA.Model.BaseInfo.FpTaxInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,FpType,Tax ");
            strSql.Append(" FROM FpTaxInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id desc ");
            List<VAN_OA.Model.BaseInfo.FpTaxInfo> list = new List<VAN_OA.Model.BaseInfo.FpTaxInfo>();

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
        public VAN_OA.Model.BaseInfo.FpTaxInfo ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.FpTaxInfo model = new VAN_OA.Model.BaseInfo.FpTaxInfo();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.FpType = dataReader["FpType"].ToString();
            ojb = dataReader["Tax"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Tax = (decimal)ojb;
            }  
            return model;
        }
    }
}
