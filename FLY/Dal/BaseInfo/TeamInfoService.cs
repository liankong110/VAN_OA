using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class TeamInfoService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.TeamInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.TeamLever != null)
            {
                strSql1.Append("TeamLever,");
                strSql2.Append("'" + model.TeamLever + "',");
            }
            if (model.Sex != null)
            {
                strSql1.Append("Sex,");
                strSql2.Append("'" + (model.Sex) + "',");
            }
            if (model.CardNo != null)
            {
                strSql1.Append("CardNo,");
                strSql2.Append("'" + model.CardNo + "',");
            }
            if (model.BrithdayYear != null)
            {
                strSql1.Append("BrithdayYear,");
                strSql2.Append("" + model.BrithdayYear + ",");
            }
            if (model.BrithdayMonth != null)
            {
                strSql1.Append("BrithdayMonth,");
                strSql2.Append("" + model.BrithdayMonth + ",");
            }
            if (model.ContractStartTime != null)
            {
                strSql1.Append("ContractStartTime,");
                strSql2.Append("'" + model.ContractStartTime + "',");
            }
            if (model.TeamPersonCount != null)
            {
                strSql1.Append("TeamPersonCount,");
                strSql2.Append("" + model.TeamPersonCount + ",");
            }

            if (model.Phone != null)
            {
                strSql1.Append("Phone,");
                strSql2.Append("'" + model.Phone + "',");
            }

            strSql.Append("insert into TeamInfo(");
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
        public bool Update(VAN_OA.Model.BaseInfo.TeamInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TeamInfo set ");
            if (model.TeamLever != null)
            {
                strSql.Append("TeamLever='" + model.TeamLever + "',");
            }
            if (model.Sex != null)
            {
                strSql.Append("Sex='" + (model.Sex) + "',");
            }
            if (model.CardNo != null)
            {
                strSql.Append("CardNo='" + model.CardNo + "',");
            }
            if (model.BrithdayYear != null)
            {
                strSql.Append("BrithdayYear=" + model.BrithdayYear + ",");
            }
            if (model.BrithdayMonth != null)
            {
                strSql.Append("BrithdayMonth=" + model.BrithdayMonth + ",");
            }
            if (model.ContractStartTime != null)
            {
                strSql.Append("ContractStartTime='" + model.ContractStartTime + "',");
            }
            if (model.TeamPersonCount != null)
            {
                strSql.Append("TeamPersonCount=" + model.TeamPersonCount + ",");
            }
            if (model.Phone != null)
            {
                strSql.Append("Phone='" + model.Phone + "',");
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
            strSql.Append("delete from TeamInfo ");
            strSql.Append(" where ID=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.TeamInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" Select Id,TeamLever,Sex,CardNo,BrithdayYear,BrithdayMonth,ContractStartTime,TeamPersonCount,Phone ");
            strSql.Append(" from TeamInfo ");
            strSql.Append(" where ID=" + ID + "");

            VAN_OA.Model.BaseInfo.TeamInfo model = null;
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
        public List<VAN_OA.Model.BaseInfo.TeamInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" Select Id,TeamLever,Sex,CardNo,BrithdayYear,BrithdayMonth,ContractStartTime,TeamPersonCount,Phone ");
            strSql.Append(" from TeamInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id desc ");
            List<VAN_OA.Model.BaseInfo.TeamInfo> list = new List<VAN_OA.Model.BaseInfo.TeamInfo>();

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
        public VAN_OA.Model.BaseInfo.TeamInfo ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.TeamInfo model = new VAN_OA.Model.BaseInfo.TeamInfo();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.TeamLever = dataReader["TeamLever"].ToString();
            ojb = dataReader["Sex"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Sex = ojb.ToString();
            }
            model.CardNo = dataReader["CardNo"].ToString();
            ojb = dataReader["BrithdayYear"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BrithdayYear = (int)ojb;
            }
            ojb = dataReader["BrithdayMonth"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BrithdayMonth = (int)ojb;
            }
            ojb = dataReader["ContractStartTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ContractStartTime = (DateTime)ojb;
            }
            ojb = dataReader["TeamPersonCount"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.TeamPersonCount = (int)ojb;
            }
            ojb = dataReader["Phone"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Phone =ojb.ToString();
            }
            
            return model;
        }



    }
}