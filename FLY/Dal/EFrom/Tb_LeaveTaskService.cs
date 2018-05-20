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
    public class Tb_LeaveTaskService
    {
        public bool updateTran(VAN_OA.Model.EFrom.Tb_LeaveTask model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
        public int addTran(VAN_OA.Model.EFrom.Tb_LeaveTask model, VAN_OA.Model.EFrom.tb_EForm eform)
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



                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();

                    string proNo = eformSer.GetAllE_No("Tb_LeaveTask", objCommand);
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
        public int Add(VAN_OA.Model.EFrom.Tb_LeaveTask model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.UserId != null)
            {
                strSql1.Append("UserId,");
                strSql2.Append("" + model.UserId + ",");
            }
            if (model.LeverType != null)
            {
                strSql1.Append("LeverType,");
                strSql2.Append("'" + model.LeverType + "',");
            }
            if (model.BeginTime != null)
            {
                strSql1.Append("BeginTime,");
                strSql2.Append("'" + model.BeginTime + "',");
            }
            if (model.ToTime != null)
            {
                strSql1.Append("ToTime,");
                strSql2.Append("'" + model.ToTime + "',");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }
            if (model.AppDate != null)
            {
                strSql1.Append("AppDate,");
                strSql2.Append("'" + model.AppDate + "',");
            }
            strSql.Append("insert into Tb_LeaveTask(");
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
        public void Update(VAN_OA.Model.EFrom.Tb_LeaveTask model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Tb_LeaveTask set ");
          
            if (model.LeverType != null)
            {
                strSql.Append("LeverType='" + model.LeverType + "',");
            }
            if (model.BeginTime != null)
            {
                strSql.Append("BeginTime='" + model.BeginTime + "',");
            }
            if (model.ToTime != null)
            {
                strSql.Append("ToTime='" + model.ToTime + "',");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
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
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tb_LeaveTask ");
            strSql.Append(" where Id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.Tb_LeaveTask GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Tb_LeaveTask.Id,UserId,LeverType,BeginTime,ToTime,Remark ,loginName,loginIPosition,zhiwu ,proNo,AppDate");
            strSql.Append(" from Tb_LeaveTask left join tb_User on tb_User.id=Tb_LeaveTask.UserId");
            strSql.Append(" where Tb_LeaveTask.Id=" + id + "");

            VAN_OA.Model.EFrom.Tb_LeaveTask model = null;
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
        public List<VAN_OA.Model.EFrom.Tb_LeaveTask> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Tb_LeaveTask.Id,UserId,LeverType,BeginTime,ToTime,Remark,loginName,loginIPosition,zhiwu ,proNo,AppDate");
            strSql.Append(" FROM Tb_LeaveTask left join tb_User on tb_User.id=Tb_LeaveTask.UserId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.Tb_LeaveTask> list = new List<VAN_OA.Model.EFrom.Tb_LeaveTask>();

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
        public Tb_LeaveTask ReaderBind(IDataReader dataReader)
        {
            Tb_LeaveTask model = new Tb_LeaveTask();
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
            model.LeverType = dataReader["LeverType"].ToString();
            ojb = dataReader["BeginTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BeginTime = (DateTime)ojb;
            }
            ojb = dataReader["ToTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ToTime = (DateTime)ojb;
            }

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UserName =ojb.ToString();
            }

            ojb = dataReader["loginIPosition"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Department =  ojb.ToString();
            }

            ojb = dataReader["zhiwu"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Job =ojb.ToString();
            }
            model.Remark = dataReader["Remark"].ToString();

            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }
            ojb = dataReader["AppDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppDate = (DateTime)ojb;
            }
            return model;
        }
    }
}
