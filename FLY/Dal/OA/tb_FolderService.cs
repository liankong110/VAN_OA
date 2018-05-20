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

using VAN_OA;
using VAN_OA.Model.OA;
 
using System.Collections.Generic;
 
using System.Data.SqlClient;
using System.Text;

namespace VAN_OA.Dal.OA
{
    public class tb_FolderService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(tb_Folder model)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Folder_NAME != null)
            {
                strSql1.Append("Folder_NAME,");
                strSql2.Append("'" + model.Folder_NAME + "',");
            }
            if (model.State != null)
            {
                strSql1.Append("State,");
                strSql2.Append("" + (model.State ? 1 : 0) + ",");
            }
            if (model.ParentId != null)
            {
                strSql1.Append("ParentId,");
                strSql2.Append("" + model.ParentId + ",");
            }
            strSql.Append("insert into tb_Folder(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            
            int result;
            object obj = DBHelp.ExeScalar(strSql.ToString());
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(VAN_OA.Model.OA.tb_Folder model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Folder set ");
            if (model.Folder_NAME != null)
            {
                strSql.Append("Folder_NAME='" + model.Folder_NAME + "',");
            }
            if (model.State != null)
            {
                strSql.Append("State=" + (model.State ? 1 : 0) + ",");
            }
            if (model.ParentId != null)
            {
                strSql.Append("ParentId=" + model.ParentId + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Folder_ID=" + model.Folder_ID + " ");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Folder_ID)
        {
           
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_Folder ");
            strSql.Append(" where Folder_ID=" + Folder_ID + " ");
            DBHelp.ExeCommand(strSql.ToString());
        }



        public List<tb_Folder> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Folder_ID,Folder_NAME,State,ParentId ");
            strSql.Append(" FROM tb_Folder ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by ParentId");
            List<tb_Folder> list = new List<tb_Folder>();
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
        public tb_Folder ReaderBind(IDataReader dataReader)
        {
            tb_Folder model = new tb_Folder();
            object ojb;
            ojb = dataReader["Folder_ID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Folder_ID = (int)ojb;
            }
            model.Folder_NAME = dataReader["Folder_NAME"].ToString();
            ojb = dataReader["State"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.State = (bool)ojb;
            }
            ojb = dataReader["ParentId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ParentId = (int)ojb;
            }
            return model;
        }

    }
}
