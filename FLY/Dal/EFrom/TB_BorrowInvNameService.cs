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
    public class TB_BorrowInvNameService
    {
        public bool updateTran(VAN_OA.Model.EFrom.TB_BorrowInvName model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
        public int addTran(VAN_OA.Model.EFrom.TB_BorrowInvName model, VAN_OA.Model.EFrom.tb_EForm eform)
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
                    string proNo = eformSer.GetAllE_No("TB_BorrowInvName", objCommand);
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
        public int Add(VAN_OA.Model.EFrom.TB_BorrowInvName model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.AppPer != null)
            {
                strSql1.Append("AppPer,");
                strSql2.Append("" + model.AppPer + ",");
            }
            if (model.AppTime != null)
            {
                strSql1.Append("AppTime,");
                strSql2.Append("'" + model.AppTime + "',");
            }
            if (model.InvName != null)
            {
                strSql1.Append("InvName,");
                strSql2.Append("'" + model.InvName + "',");
            }
            if (model.Reason != null)
            {
                strSql1.Append("Reason,");
                strSql2.Append("'" + model.Reason + "',");
            }
            if (model.BorrowTime != null)
            {
                strSql1.Append("BorrowTime,");
                strSql2.Append("'" + model.BorrowTime + "',");
            }
            if (model.BackTime != null)
            {
                strSql1.Append("BackTime,");
                strSql2.Append("'" + model.BackTime + "',");
            }
            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }

            strSql.Append("insert into TB_BorrowInvName(");
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
        public void Update(VAN_OA.Model.EFrom.TB_BorrowInvName model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_BorrowInvName set ");
            //if (model.AppPer != null)
            //{
            //    strSql.Append("AppPer=" + model.AppPer + ",");
            //}
            //if (model.AppTime != null)
            //{
            //    strSql.Append("AppTime='" + model.AppTime + "',");
            //}
            if (model.InvName != null)
            {
                strSql.Append("InvName='" + model.InvName + "',");
            }
            if (model.Reason != null)
            {
                strSql.Append("Reason='" + model.Reason + "',");
            }
            else
            {
                strSql.Append("Reason= null ,");
            }
            if (model.BorrowTime != null)
            {
                strSql.Append("BorrowTime='" + model.BorrowTime + "',");
            }
            else
            {
                strSql.Append("BorrowTime= null ,");
            }
            if (model.BackTime != null)
            {
                strSql.Append("BackTime='" + model.BackTime + "',");
            }
            else
            {
                strSql.Append("BackTime= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }



        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(VAN_OA.Model.EFrom.TB_BorrowInvName model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_BorrowInvName set ");
            //if (model.AppPer != null)
            //{
            //    strSql.Append("AppPer=" + model.AppPer + ",");
            //}
            //if (model.AppTime != null)
            //{
            //    strSql.Append("AppTime='" + model.AppTime + "',");
            //}
            if (model.InvName != null)
            {
                strSql.Append("InvName='" + model.InvName + "',");
            }
            if (model.Reason != null)
            {
                strSql.Append("Reason='" + model.Reason + "',");
            }
            else
            {
                strSql.Append("Reason= null ,");
            }
            if (model.BorrowTime != null)
            {
                strSql.Append("BorrowTime='" + model.BorrowTime + "',");
            }
            else
            {
                strSql.Append("BorrowTime= null ,");
            }
            if (model.BackTime != null)
            {
                strSql.Append("BackTime='" + model.BackTime + "',");
            }
            else
            {
                strSql.Append("BackTime= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_BorrowInvName ");
            strSql.Append(" where Id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.TB_BorrowInvName GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" TB_BorrowInvName.Id,AppPer,AppTime,InvName,Reason,BorrowTime,BackTime,loginName ,proNo");
            strSql.Append(" from TB_BorrowInvName left join tb_User on tb_User.id=TB_BorrowInvName.AppPer");
            strSql.Append(" where TB_BorrowInvName.Id=" + id + "");

            VAN_OA.Model.EFrom.TB_BorrowInvName model = null;
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
        public List<VAN_OA.Model.EFrom.TB_BorrowInvName> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TB_BorrowInvName.Id,AppPer,AppTime,InvName,Reason,BorrowTime,BackTime,loginName ,proNo");
            strSql.Append(" FROM TB_BorrowInvName left join tb_User on tb_User.id=TB_BorrowInvName.AppPer");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.TB_BorrowInvName> list = new List<VAN_OA.Model.EFrom.TB_BorrowInvName>();

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
        public VAN_OA.Model.EFrom.TB_BorrowInvName ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.TB_BorrowInvName model = new VAN_OA.Model.EFrom.TB_BorrowInvName();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["AppPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppPer = (int)ojb;
            }
            ojb = dataReader["AppTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppTime = (DateTime)ojb;
            }
            model.InvName = dataReader["InvName"].ToString();
            model.Reason = dataReader["Reason"].ToString();
            ojb = dataReader["BorrowTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BorrowTime = (DateTime)ojb;
            }
            ojb = dataReader["BackTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BackTime = (DateTime)ojb;
            }

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LoginName = ojb.ToString();
            }
            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }

            return model;
        }



    }
}
