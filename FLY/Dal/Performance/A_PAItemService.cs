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
using VAN_OA.Model.Performance;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace VAN_OA.Dal.Performance
{
    public class A_PAItemService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(A_PAItem model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.A_PAItemName != null)
            {
                strSql1.Append("A_PAItemName,");
                strSql2.Append("'" + model.A_PAItemName + "',");
            }

                strSql1.Append("A_PAItemScore,");
                strSql2.Append("" + model.A_PAItemScore + ",");

                strSql1.Append("A_PAItemAmount,");
                strSql2.Append("" + model.A_PAItemAmount + ",");
            strSql.Append("insert into A_PAItem(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            object obj =DBHelp.ExeScalar(strSql.ToString());

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
        public void Update(A_PAItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update A_PAItem set ");
            if (model.A_PAItemName != null)
            {
                strSql.Append("A_PAItemName='" + model.A_PAItemName + "',");
            }
            else
            {
                strSql.Append("A_PAItemName= null ,");
            }
            if (model.A_PAItemScore != null)
            {
                strSql.Append("A_PAItemScore=" + model.A_PAItemScore + ",");
            }
            else
            {
                strSql.Append("A_PAItemScore= null ,");
            }
            if (model.A_PAItemAmount != null)
            {
                strSql.Append("A_PAItemAmount=" + model.A_PAItemAmount + ",");
            }
            else
            {
                strSql.Append("A_PAItemAmount= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where A_PAItemID=" + model.A_PAItemID + "");
            DBHelp.ExeCommand(strSql.ToString());
          
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int A_PAItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from A_PAItem ");
            strSql.Append(" where A_PAItemID=" + A_PAItemID + "");
           DBHelp.ExeCommand (strSql.ToString());
           
        }		 
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public List<A_PAItem> GetModelList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A_PAItemID,A_PAItemName,A_PAItemScore,A_PAItemAmount ");
            strSql.Append(" FROM A_PAItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            List<A_PAItem> PAItem = new List<A_PAItem>();
             using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        A_PAItem model = new A_PAItem();
                        object ojb;
                        ojb = dataReader["A_PAItemId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.A_PAItemID = (int)ojb;
                        }
                        model.A_PAItemName = dataReader["A_PAItemName"].ToString();
                        model.A_PAItemScore = decimal.Parse(dataReader["A_PAItemScore"].ToString());
                        model.A_PAItemAmount = decimal.Parse(dataReader["A_PAItemAmount"].ToString());
                        PAItem.Add(model);
                    }
                }
             }
             return PAItem; 
        }
        public DataTable GetTablePAItemList(string strWhere)
        {
            return DBHelp.getDataTable("select A_PAItemID,left(A_PAItemName,50) as A_PAItemName,A_PAItemScore,A_PAItemAmount FROM A_PAItem");
        }    
    }
}
