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
    public class A_PASectionService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(A_PASection model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.A_PASectionName != null)
            {
                strSql1.Append("A_PASectionName,");
                strSql2.Append("'" + model.A_PASectionName + "',");
            }
            strSql.Append("insert into A_PASection(");
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
        public void Update(A_PASection model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update A_PASection set ");
            if (model.A_PASectionName != null)
            {
                strSql.Append("A_PASectionName='" + model.A_PASectionName + "',");
            }
            else
            {
                strSql.Append("A_PASectionName= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where A_PASectionID=" + model.A_PASectionID + "");
            DBHelp.ExeCommand(strSql.ToString());
          
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int A_PAItemID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from A_PASection ");
            strSql.Append(" where A_PASectionID=" + A_PAItemID + "");
           DBHelp.ExeCommand (strSql.ToString());
           
        }		 
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public List<A_PASection> GetModelList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A_PASectionID,A_PASectionName ");
            strSql.Append(" FROM A_PASection ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<A_PASection> PASection = new List<A_PASection>();
             using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        A_PASection model = new A_PASection();
                        object ojb;
                        ojb = dataReader["A_PASectionId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.A_PASectionID = (int)ojb;
                        }
                        model.A_PASectionName = dataReader["A_PASectionName"].ToString();
                        PASection.Add(model);
                    }
                }
             }
             return PASection; 
        }    
    }
}
