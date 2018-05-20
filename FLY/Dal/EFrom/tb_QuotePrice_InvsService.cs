using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Model.EFrom;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;


namespace VAN_OA.Dal.EFrom
{
    public class tb_QuotePrice_InvsService
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.EFrom.tb_QuotePrice_Invs model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.InvName != null)
            {
                strSql1.Append("InvName,");
                strSql2.Append("'" + model.InvName + "',");
            }
            if (model.InvNum != null)
            {
                strSql1.Append("InvNum,");
                strSql2.Append("" + model.InvNum + ",");
            }
            if (model.InvPrice != null)
            {
                strSql1.Append("InvPrice,");
                strSql2.Append("" + model.InvPrice + ",");
            }
            if (model.QuoteId != null)
            {
                strSql1.Append("QuoteId,");
                strSql2.Append("" + model.QuoteId + ",");
            }
            strSql.Append("insert into tb_QuotePrice_Invs(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            int result;
            objCommand.CommandText = strSql.ToString();
            object obj = objCommand.ExecuteScalar();
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(VAN_OA.Model.EFrom.tb_QuotePrice_Invs model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_QuotePrice_Invs set ");
            if (model.InvName != null)
            {
                strSql.Append("InvName='" + model.InvName + "',");
            }
            if (model.InvNum != null)
            {
                strSql.Append("InvNum=" + model.InvNum + ",");
            }
            if (model.InvPrice != null)
            {
                strSql.Append("InvPrice=" + model.InvPrice + ",");
            }
            if (model.QuoteId != null)
            {
                strSql.Append("QuoteId=" + model.QuoteId + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ids)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_QuotePrice_Invs ");
            strSql.Append(" where Id=" + ids + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string ids, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_QuotePrice_Invs ");
            strSql.Append(" where Id in(" + ids + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }
        /// 删除一条数据
        /// </summary>
        public void DeleteById(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_QuotePrice_Invs ");
            strSql.Append(" where QuoteId=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_QuotePrice_Invs GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Id,InvName,InvNum,InvPrice,QuoteId ");
            strSql.Append(" from tb_QuotePrice_Invs ");
            strSql.Append(" where Id=" + Ids + "");
            VAN_OA.Model.EFrom.tb_QuotePrice_Invs model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind(dataReader);
                    }
                }
            }
            return model;
        }




        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_QuotePrice_Invs> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,InvName,InvNum,InvPrice,QuoteId ");
            strSql.Append(" FROM tb_QuotePrice_Invs ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.tb_QuotePrice_Invs> list = new List<VAN_OA.Model.EFrom.tb_QuotePrice_Invs>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind(dataReader));
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.EFrom.tb_QuotePrice_Invs ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_QuotePrice_Invs model = new VAN_OA.Model.EFrom.tb_QuotePrice_Invs();
            object ojb; 
			ojb = dataReader["Id"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.Id=(int)ojb;
			}
			model.InvName=dataReader["InvName"].ToString();
			ojb = dataReader["InvNum"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.InvNum=(decimal)ojb;
			}
			ojb = dataReader["InvPrice"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.InvPrice=(decimal)ojb;
			}
			ojb = dataReader["QuoteId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.QuoteId = (int)ojb;
            }

            model.Total = model.InvNum * model.InvPrice;
            return model;
        }
    }
}
