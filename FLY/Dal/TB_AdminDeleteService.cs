using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace VAN_OA.Dal
{
    public class TB_AdminDeleteService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.TB_AdminDelete model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.UserId != null)
            {
                strSql1.Append("UserId,");
                strSql2.Append("" + model.UserId + ",");
            }
            strSql.Append("insert into TB_AdminDelete(");
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
        public bool Update(VAN_OA.Model.TB_AdminDelete model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_AdminDelete set ");
            if (model.UserId != null)
            {
                strSql.Append("UserId=" + model.UserId + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            return DBHelp.ExeCommand(strSql.ToString());

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_AdminDelete ");
            strSql.Append(" where Id=" + Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }


        public bool CheckIsExistByUserId(int id)
        {
            if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from TB_AdminDelete where UserId='{0}'", id))) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.TB_AdminDelete> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TB_AdminDelete.Id,UserId,loginName ");
            strSql.Append(" FROM TB_AdminDelete left join tb_user on tb_user.ID=TB_AdminDelete.UserId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.TB_AdminDelete> list = new List<VAN_OA.Model.TB_AdminDelete>();

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
        public VAN_OA.Model.TB_AdminDelete ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.TB_AdminDelete model = new VAN_OA.Model.TB_AdminDelete();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["UserId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UserId = (int)ojb;
            }

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UserName = ojb.ToString();
            }
            return model;
        }

    }
}
