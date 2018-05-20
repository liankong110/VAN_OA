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
    public class tb_ToolsAppService
    {
        public bool updateTran(VAN_OA.Model.EFrom.tb_ToolsApp model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {

                    objCommand.Parameters.Clear();
                    Update(model, objCommand);


                    tb_EFormService eformSer = new tb_EFormService();

                    eformSer.Update(eform, objCommand);


                    tb_EFormsService eformsSer = new tb_EFormsService();
                    eformsSer.Add(forms, objCommand);

                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return false;

                }

            }

            return true;
        }
        public int addTran(VAN_OA.Model.EFrom.tb_ToolsApp model, VAN_OA.Model.EFrom.tb_EForm eform)
        {
            int id = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {


                    tb_EFormService eformSer = new tb_EFormService();
                    objCommand.Parameters.Clear();

                    string proNo = eformSer.GetAllE_No("tb_ToolsApp", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;
 

                    id = Add(model, objCommand);

                 
                    eform.allE_id = id;
                    eformSer.Add(eform, objCommand);

                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return 0;

                }
                return id;
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.EFrom.tb_ToolsApp model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.departName != null)
            {
                strSql1.Append("departName,");
                strSql2.Append("'" + model.departName + "',");
            }
            if (model.appName != null)
            {
                strSql1.Append("appName,");
                strSql2.Append("'" + model.appName + "',");
            }
            if (model.dateTime != null)
            {
                strSql1.Append("dateTime,");
                strSql2.Append("'" + model.dateTime + "',");
            }
            if (model.toolName != null)
            {
                strSql1.Append("toolName,");
                strSql2.Append("'" + model.toolName + "',");
            }
            if (model.toolYong != null)
            {
                strSql1.Append("toolYong,");
                strSql2.Append("'" + model.toolYong + "',");
            }

            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }

            strSql.Append("insert into tb_ToolsApp(");
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
        public void Update(VAN_OA.Model.EFrom.tb_ToolsApp model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_ToolsApp set ");
            if (model.departName != null)
            {
                strSql.Append("departName='" + model.departName + "',");
            }
            else
            {
                strSql.Append("departName= null ,");
            }
            if (model.appName != null)
            {
                strSql.Append("appName='" + model.appName + "',");
            }
            else
            {
                strSql.Append("appName= null ,");
            }
            if (model.dateTime != null)
            {
                strSql.Append("dateTime='" + model.dateTime + "',");
            }
            else
            {
                strSql.Append("dateTime= null ,");
            }
            if (model.toolName != null)
            {
                strSql.Append("toolName='" + model.toolName + "',");
            }
            else
            {
                strSql.Append("toolName= null ,");
            }
            if (model.toolYong != null)
            {
                strSql.Append("toolYong='" + model.toolYong + "',");
            }
            else
            {
                strSql.Append("toolYong= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where id=" + model.id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_ToolsApp ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_ToolsApp GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" id,departName,appName,dateTime,toolName,toolYong ,proNo");
            strSql.Append(" from tb_ToolsApp ");
            strSql.Append(" where id=" + id + "");

            VAN_OA.Model.EFrom.tb_ToolsApp model = null;
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
        public List<VAN_OA.Model.EFrom.tb_ToolsApp> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,departName,appName,dateTime,toolName,toolYong,proNo ");
            strSql.Append(" FROM tb_ToolsApp ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.tb_ToolsApp> list = new List<VAN_OA.Model.EFrom.tb_ToolsApp>();

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
        public VAN_OA.Model.EFrom.tb_ToolsApp ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_ToolsApp model = new VAN_OA.Model.EFrom.tb_ToolsApp();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            model.departName = dataReader["departName"].ToString();
            model.appName = dataReader["appName"].ToString();
            ojb = dataReader["dateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.dateTime = (DateTime)ojb;
            }
            model.toolName = dataReader["toolName"].ToString();
            model.toolYong = dataReader["toolYong"].ToString();
            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }

            return model;
        }


    }
}
