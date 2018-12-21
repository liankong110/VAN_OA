using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class TB_ProjectCostService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.TB_ProjectCost model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();

            strSql1.Append("ZhangQi,");
            strSql2.Append("" + model.ZhangQi + ",");
            strSql1.Append("CeSuanDian,");
            strSql2.Append("" + model.CeSuanDian + ",");
            strSql1.Append("MonthLiLv,");
            strSql2.Append("" + model.MonthLiLv + ",");

            strSql.Append("insert into TB_ProjectCost(");
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
        public bool Update(VAN_OA.Model.BaseInfo.TB_ProjectCost model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_ProjectCost set ");
          
             strSql.Append("ZhangQi=" + model.ZhangQi + ",");
            strSql.Append("CeSuanDian=" + model.CeSuanDian + ",");
            strSql.Append("MonthLiLv=" + model.MonthLiLv + ",");

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
            strSql.Append("delete from TB_ProjectCost ");
            strSql.Append(" where Id=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.TB_ProjectCost GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Id,ZhangQi,CeSuanDian,MonthLiLv ");
            strSql.Append(" from TB_ProjectCost ");
            strSql.Append(" where Id=" + ID + "");

            VAN_OA.Model.BaseInfo.TB_ProjectCost model = null;
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
        public List<VAN_OA.Model.BaseInfo.TB_ProjectCost> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Id,ZhangQi,CeSuanDian,MonthLiLv ");
            strSql.Append(" FROM TB_ProjectCost ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id  ");
            List<VAN_OA.Model.BaseInfo.TB_ProjectCost> list = new List<VAN_OA.Model.BaseInfo.TB_ProjectCost>();

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
        public VAN_OA.Model.BaseInfo.TB_ProjectCost ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.TB_ProjectCost model = new VAN_OA.Model.BaseInfo.TB_ProjectCost();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.ZhangQi = Convert.ToSingle(dataReader["ZhangQi"]);
            model.CeSuanDian = Convert.ToSingle(dataReader["CeSuanDian"]);
            model.MonthLiLv = Convert.ToSingle(dataReader["MonthLiLv"]);
            return model;
        }
    }
}