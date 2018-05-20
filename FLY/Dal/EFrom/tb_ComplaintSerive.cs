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
    public class tb_ComplaintSerive
    {
        public bool updateTran(VAN_OA.Model.EFrom.tb_Complaint model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
        public int addTran(VAN_OA.Model.EFrom.tb_Complaint model, VAN_OA.Model.EFrom.tb_EForm eform)
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
                    string proNo = eformSer.GetAllE_No("tb_Complaint", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;


                    objCommand.Parameters.Clear();
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
        public int Add(VAN_OA.Model.EFrom.tb_Complaint model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.appUserId != null)
            {
                strSql1.Append("appUserId,");
                strSql2.Append("" + model.appUserId + ",");
            }
            if (model.datetime != null)
            {
                strSql1.Append("datetime,");
                strSql2.Append("'" + model.datetime + "',");
            }
            if (model.toPerId != null)
            {
                strSql1.Append("toPerId,");
                strSql2.Append("" + model.toPerId + ",");
            }
            if (model.ContentRemark != null)
            {
                strSql1.Append("ContentRemark,");
                strSql2.Append("'" + model.ContentRemark + "',");
            }
            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }

            strSql.Append("insert into tb_Complaint(");
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
        public void Update(VAN_OA.Model.EFrom.tb_Complaint model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Complaint set ");
            if (model.appUserId != null)
            {
                strSql.Append("appUserId=" + model.appUserId + ",");
            }
            else
            {
                strSql.Append("appUserId= null ,");
            }
            if (model.datetime != null)
            {
                strSql.Append("datetime='" + model.datetime + "',");
            }
            else
            {
                strSql.Append("datetime= null ,");
            }
            if (model.toPerId != null)
            {
                strSql.Append("toPerId=" + model.toPerId + ",");
            }
            else
            {
                strSql.Append("toPerId= null ,");
            }
            if (model.ContentRemark != null)
            {
                strSql.Append("ContentRemark='" + model.ContentRemark + "',");
            }
            else
            {
                strSql.Append("ContentRemark= null ,");
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
            strSql.Append("delete from tb_Complaint ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_Complaint GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" * ");
            strSql.Append(" from tb_ComplaintP_view ");
            strSql.Append(" where id=" + id + "");

            VAN_OA.Model.EFrom.tb_Complaint model = null;
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
        public List<VAN_OA.Model.EFrom.tb_Complaint> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM tb_ComplaintP_view ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.tb_Complaint> list = new List<VAN_OA.Model.EFrom.tb_Complaint>();

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
        public VAN_OA.Model.EFrom.tb_Complaint ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_Complaint model = new VAN_OA.Model.EFrom.tb_Complaint();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            ojb = dataReader["appUserId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.appUserId = (int)ojb;
            }
            ojb = dataReader["datetime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.datetime = (DateTime)ojb;
            }
            ojb = dataReader["toPerId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.toPerId = (int)ojb;
            }

            ojb = dataReader["AppName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppUserName = ojb.ToString();
            }

            ojb = dataReader["toPerName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ToPerName = ojb.ToString();
            }
            model.ContentRemark = dataReader["ContentRemark"].ToString();

            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }
            return model;
        }



    }
}
