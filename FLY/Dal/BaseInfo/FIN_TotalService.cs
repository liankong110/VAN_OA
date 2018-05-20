using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class FIN_TotalService
    {
        public string GetAllE_No()
        {
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(ProNo),4))+1))),4) FROM  FIN_Total where ProNo like '{0}%';",
                DateTime.Now.Year);

            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = DateTime.Now.Year.ToString() + objMax.ToString();
            }
            else
            {
                MaxCardNo = DateTime.Now.Year.ToString() + "0001";
            }

            return MaxCardNo;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.FIN_Total model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FIN_Total(");
            strSql.Append("ProNo,CaiYear,Total,CompCode");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("'" + GetAllE_No() + "',");
            strSql.Append("'" + model.CaiYear + "',");
            strSql.Append("" + model.Total + ",");
            strSql.Append("'" + model.CompCode + "'");
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
        public bool Update(VAN_OA.Model.BaseInfo.FIN_Total model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FIN_Total set ");
            strSql.Append("ProNo='" + model.ProNo + "',");
            strSql.Append("CaiYear='" + model.CaiYear + "',");
            strSql.Append("Total=" + model.Total + ",");
            strSql.Append("CompCode='" + model.CompCode + "'");
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
            strSql.Append("delete from FIN_Total ");
            strSql.Append(" where Id=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.FIN_Total GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProNo,CaiYear,Total,CompCode ");
            strSql.Append(" FROM FIN_Total ");
            strSql.Append(" where ID=" + ID + "");

            VAN_OA.Model.BaseInfo.FIN_Total model = null;
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
        public List<VAN_OA.Model.BaseInfo.FIN_Total> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProNo,CaiYear,Total,CompCode ");
            strSql.Append(" FROM FIN_Total ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id desc ");
            List<VAN_OA.Model.BaseInfo.FIN_Total> list = new List<VAN_OA.Model.BaseInfo.FIN_Total>();

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
        public VAN_OA.Model.BaseInfo.FIN_Total ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.FIN_Total model = new VAN_OA.Model.BaseInfo.FIN_Total();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.ProNo = dataReader["ProNo"].ToString();
            model.CaiYear = dataReader["CaiYear"].ToString();
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Total = (decimal)ojb;
            }
            model.CompCode = dataReader["CompCode"].ToString();
            return model;
        }
    }
}