using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class TB_BaseSkillService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.TB_BaseSkill model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.MyPoType != null)
            {
                strSql1.Append("MyPoType,");
                strSql2.Append("'" + model.MyPoType + "',");
            }
            if (model.XiShu != null)
            {
                strSql1.Append("XiShu,");
                strSql2.Append("" + model.XiShu + ",");
            }
            if (model.Val != null)
            {
                strSql1.Append("Val,");
                strSql2.Append("" + model.Val + ",");
            }
            strSql.Append("insert into TB_BaseSkill(");
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
        public bool Update(VAN_OA.Model.BaseInfo.TB_BaseSkill model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_BaseSkill set ");
            if (model.MyPoType != null)
            {
                strSql.Append("MyPoType='" + model.MyPoType + "',");
            }
            if (model.XiShu != null)
            {
                strSql.Append("XiShu=" + model.XiShu + ",");
            }
            if (model.Val != null)
            {
                strSql.Append("Val=" + model.Val + ",");
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
            strSql.Append("delete from TB_BaseSkill ");
            strSql.Append(" where Id=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.TB_BaseSkill GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,MyPoType,XiShu,Val ");
            strSql.Append(" FROM TB_BaseSkill ");
            strSql.Append(" where Id=" + ID + "");

            VAN_OA.Model.BaseInfo.TB_BaseSkill model = null;
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
        public List<VAN_OA.Model.BaseInfo.TB_BaseSkill> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,MyPoType,XiShu,Val ");
            strSql.Append(" FROM TB_BaseSkill ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id  ");
            List<VAN_OA.Model.BaseInfo.TB_BaseSkill> list = new List<VAN_OA.Model.BaseInfo.TB_BaseSkill>();

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
        public VAN_OA.Model.BaseInfo.TB_BaseSkill ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.TB_BaseSkill model = new VAN_OA.Model.BaseInfo.TB_BaseSkill();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.MyPoType = dataReader["MyPoType"].ToString();
            ojb = dataReader["XiShu"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.XiShu = (decimal)ojb;
            }
            ojb = dataReader["Val"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Val = (decimal)ojb;
            }
            return model;
        }
    }
}