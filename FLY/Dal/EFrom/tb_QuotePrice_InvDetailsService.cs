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
    public class tb_QuotePrice_InvDetailsService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.EFrom.tb_QuotePrice_InvDetails model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            
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
            if (model.InvGoodId != null)
            {
                strSql1.Append("InvGoodId,");
                strSql2.Append("" + model.InvGoodId + ",");
            }
            if (model.InvName != null)
            {
                strSql1.Append("InvName,");
                strSql2.Append("'" + model.InvName + "',");
            }
            if (model.InvModel != null)
            {
                strSql1.Append("InvModel,");
                strSql2.Append("@InvModel,");
                //strSql2.Append("'" + model.InvModel + "',");
            }
            if (model.InvUnit != null)
            {
                strSql1.Append("InvUnit,");
                strSql2.Append("'" + model.InvUnit + "',");
            }
            if (model.Product != null)
            {
                strSql1.Append("Product,");
                strSql2.Append("'" + model.Product + "',");
            }
            if (model.GoodBrand != null)
            {
                strSql1.Append("InvBrand,");
                strSql2.Append("'" + model.GoodBrand + "',");
            }
            if (model.InvRemark != null)
            {
                strSql1.Append("InvRemark,");
                strSql2.Append("'" + model.InvRemark + "',");
            }
            strSql.Append("insert into tb_QuotePrice_InvDetails(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");

            int result;
            objCommand.CommandText = strSql.ToString();
            objCommand.Parameters.Add(new SqlParameter { ParameterName= "@InvModel", Value = model.InvModel });
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
        public void Update(VAN_OA.Model.EFrom.tb_QuotePrice_InvDetails model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_QuotePrice_InvDetails set ");
           
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
            if (model.InvGoodId != null)
            {
                strSql.Append("InvGoodId=" + model.InvGoodId + ",");
            }
            if (model.InvName != null)
            {
                strSql.Append("InvName='" + model.InvName + "',");
            }
            if (model.InvModel != null)
            {
                strSql.Append("InvModel=@InvModel,");
            }
            if (model.InvUnit != null)
            {
                strSql.Append("InvUnit='" + model.InvUnit + "',");
            }
            if (model.Product != null)
            {
                strSql.Append("Product='" + model.Product + "',");
            }
            if (model.GoodBrand != null)
            {
                strSql.Append("InvBrand='" + model.GoodBrand + "',");
            }
            if (model.InvRemark != null)
            {
                strSql.Append("InvRemark='" + model.InvRemark + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.Parameters.Add(new SqlParameter { ParameterName = "@InvModel", Value = model.InvModel });
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ids)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_QuotePrice_InvDetails ");
            strSql.Append(" where Id=" + ids + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string ids, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_QuotePrice_InvDetails ");
            strSql.Append(" where id in(" + ids + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }
        /// 删除一条数据
        /// </summary>
        public void DeleteById(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_QuotePrice_InvDetails ");
            strSql.Append(" where QuoteId=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_QuotePrice_InvDetails GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append("select InvRemark,Id,QuoteId,InvGoodId,InvNum,InvPrice,InvName,InvModel,InvUnit,Product,InvBrand ");
            strSql.Append(" from tb_QuotePrice_InvDetails ");
            strSql.Append(" where Id=" + Ids + "");
            VAN_OA.Model.EFrom.tb_QuotePrice_InvDetails model = null;
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
        public List<VAN_OA.Model.EFrom.tb_QuotePrice_InvDetails> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select InvRemark,Id,QuoteId,InvGoodId,InvNum,InvPrice,InvName,InvModel,InvUnit,Product,InvBrand ");
            strSql.Append(" FROM tb_QuotePrice_InvDetails");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.tb_QuotePrice_InvDetails> list = new List<VAN_OA.Model.EFrom.tb_QuotePrice_InvDetails>();

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
        public VAN_OA.Model.EFrom.tb_QuotePrice_InvDetails ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_QuotePrice_InvDetails model = new VAN_OA.Model.EFrom.tb_QuotePrice_InvDetails();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.InvName = dataReader["InvName"].ToString();
            model.InvModel = dataReader["InvModel"].ToString();
            model.InvUnit = dataReader["InvUnit"].ToString();
            model.InvGoodId = Convert.ToInt32(dataReader["InvGoodId"]);
            ojb = dataReader["InvNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.InvNum = (decimal)ojb;
            }
            ojb = dataReader["InvPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.InvPrice = (decimal)ojb;
            }
            ojb = dataReader["QuoteId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.QuoteId = (int)ojb;
            }

            ojb = dataReader["InvBrand"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodBrand =  ojb.ToString();
            }
            model.Total = model.InvNum * model.InvPrice;
            model.Product = dataReader["Product"].ToString();
            model.InvRemark = dataReader["InvRemark"].ToString(); 
            return model;
        }
    }
}
