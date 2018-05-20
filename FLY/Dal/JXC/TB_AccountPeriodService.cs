using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using VAN_OA.Model.JXC;


namespace VAN_OA.Dal.JXC
{
    public class TB_AccountPeriodService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TB_AccountPeriod model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.AccountName != null)
            {
                strSql1.Append("AccountName,");
                strSql2.Append("" + model.AccountName + ",");
            }
            if (model.AccountXiShu != null)
            {
                strSql1.Append("AccountXiShu,");
                strSql2.Append("" + model.AccountXiShu + ",");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            strSql.Append("insert into TB_AccountPeriod(");
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
        public bool Update(TB_AccountPeriod model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_AccountPeriod set ");
            if (model.AccountName != null)
            {
                strSql.Append("AccountName=" + model.AccountName + ",");
            }
            if (model.AccountXiShu != null)
            {
                strSql.Append("AccountXiShu=" + model.AccountXiShu + ",");
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
            strSql.Append("delete from TB_AccountPeriod ");
            strSql.Append(" where ID=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public TB_AccountPeriod GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Id,AccountName,AccountXiShu,Remark ");
            strSql.Append(" from TB_AccountPeriod ");
            strSql.Append(" where Id=" + ID + "");

            TB_AccountPeriod model = null;
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
        public List<TB_AccountPeriod> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Id,AccountName,AccountXiShu,Remark ");
            strSql.Append(" from TB_AccountPeriod ");
            
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by AccountName ");
            List<TB_AccountPeriod> list = new List<TB_AccountPeriod>();

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
        public TB_AccountPeriod ReaderBind(IDataReader dataReader)
        {
            TB_AccountPeriod model = new TB_AccountPeriod();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["AccountName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AccountName = (decimal)ojb;
            }
            ojb = dataReader["AccountXiShu"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AccountXiShu = (decimal)ojb;
            }
            model.Remark = dataReader["Remark"].ToString();
            return model;
        }



    }
}
