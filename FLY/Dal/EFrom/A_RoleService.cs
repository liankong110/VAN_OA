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
    public class A_RoleService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.EFrom.A_Role model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.A_RoleName != null)
            {
                strSql1.Append("A_RoleName,");
                strSql2.Append("'" + model.A_RoleName + "',");
            }
            if (model.A_IFEdit != null)
            {
                strSql1.Append("A_IFEdit,");
                strSql2.Append("" + (model.A_IFEdit ? 1 : 0) + ",");
            }
            strSql.Append("insert into A_Role(");
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
        public void Update(VAN_OA.Model.EFrom.A_Role model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update A_Role set ");
            if (model.A_RoleName != null)
            {
                strSql.Append("A_RoleName='" + model.A_RoleName + "',");
            }
            else
            {
                strSql.Append("A_RoleName= null ,");
            }
            if (model.A_IFEdit != null)
            {
                strSql.Append("A_IFEdit=" + (model.A_IFEdit ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("A_IFEdit= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where A_RoleId=" + model.A_RoleId + "");
            DBHelp.ExeCommand(strSql.ToString());
          
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int A_RoleId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from A_Role ");
            strSql.Append(" where A_RoleId=" + A_RoleId + "");
           DBHelp.ExeCommand (strSql.ToString());
           
        }		 
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public List<VAN_OA.Model.EFrom.A_Role> GetModelList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A_RoleId,A_RoleName,A_IFEdit ");
            strSql.Append(" FROM A_Role ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.A_Role> roles = new List<A_Role>();
             using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        
                        VAN_OA.Model.EFrom.A_Role model = new VAN_OA.Model.EFrom.A_Role();
                        object ojb;
                        ojb = dataReader["A_RoleId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.A_RoleId = (int)ojb;
                        }
                        model.A_RoleName = dataReader["A_RoleName"].ToString();
                        ojb = dataReader["A_IFEdit"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.A_IFEdit = (bool)ojb;
                        }
                        roles.Add(model);
                    }
                }
             }
             return roles;
               
          
        }
       
       
    }
}
