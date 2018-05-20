using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class CAR_YearCheckService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.CAR_YearCheck model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.CarNo != null)
            {
                strSql1.Append("CarNo,");
                strSql2.Append("'" + model.CarNo + "',");
            }
            if (model.YearDate != null)
            {
                strSql1.Append("YearDate,");
                strSql2.Append("'" + model.YearDate + "',");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            strSql.Append("insert into CAR_YearCheck(");
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
        public bool Update(VAN_OA.Model.BaseInfo.CAR_YearCheck model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CAR_YearCheck set ");
            if (model.CarNo != null)
            {
                strSql.Append("CarNo='" + model.CarNo + "',");
            }
            if (model.YearDate != null)
            {
                strSql.Append("YearDate='" + model.YearDate + "',");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
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
            strSql.Append("delete from CAR_YearCheck ");
            strSql.Append(" where Id=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.CAR_YearCheck GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();          
            strSql.Append("select Id,CarNo,YearDate,Remark ");
            strSql.Append(" FROM CAR_YearCheck ");
            strSql.Append(" where Id=" + ID + "");

            VAN_OA.Model.BaseInfo.CAR_YearCheck model = null;
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
        public List<VAN_OA.Model.BaseInfo.CAR_YearCheck> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,CarNo,YearDate,Remark ");
            strSql.Append(" FROM CAR_YearCheck ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id desc ");
            List<VAN_OA.Model.BaseInfo.CAR_YearCheck> list = new List<VAN_OA.Model.BaseInfo.CAR_YearCheck>();

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
        public VAN_OA.Model.BaseInfo.CAR_YearCheck ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.CAR_YearCheck model = new VAN_OA.Model.BaseInfo.CAR_YearCheck();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.CarNo = dataReader["CarNo"].ToString();
            ojb = dataReader["YearDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.YearDate = (DateTime)ojb;
            }
            model.Remark = dataReader["Remark"].ToString();
            return model;
        }
    }
}