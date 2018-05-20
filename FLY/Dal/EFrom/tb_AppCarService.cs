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
    public class tb_AppCarService
    {
        public bool updateTran(VAN_OA.Model.EFrom.tb_AppCar model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
        public int addTran(VAN_OA.Model.EFrom.tb_AppCar model, VAN_OA.Model.EFrom.tb_EForm eform)
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
                    string proNo = eformSer.GetAllE_No("tb_AppCar", objCommand);
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
        public int Add(VAN_OA.Model.EFrom.tb_AppCar model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.appUserId != null)
            {
                strSql1.Append("appUserId,");
                strSql2.Append("" + model.appUserId + ",");
            }
            if (model.datetiem != null)
            {
                strSql1.Append("datetiem,");
                strSql2.Append("'" + model.datetiem + "',");
            }
            if (model.type != null)
            {
                strSql1.Append("type,");
                strSql2.Append("'" + model.type + "',");
            }
            if (model.address != null)
            {
                strSql1.Append("address,");
                strSql2.Append("'" + model.address + "',");
            }
            if (model.compName != null)
            {
                strSql1.Append("compName,");
                strSql2.Append("'" + model.compName + "',");
            }
            if (model.invName != null)
            {
                strSql1.Append("invName,");
                strSql2.Append("'" + model.invName + "',");
            }
            if (model.suiChePer != null)
            {
                strSql1.Append("suiChePer,");
                strSql2.Append("'" + model.suiChePer + "',");
            }

            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }
            strSql.Append("insert into tb_AppCar(");
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
        public void Update(VAN_OA.Model.EFrom.tb_AppCar model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_AppCar set ");
            if (model.appUserId != null)
            {
                strSql.Append("appUserId=" + model.appUserId + ",");
            }
            else
            {
                strSql.Append("appUserId= null ,");
            }
            if (model.datetiem != null)
            {
                strSql.Append("datetiem='" + model.datetiem + "',");
            }
            else
            {
                strSql.Append("datetiem= null ,");
            }
            if (model.type != null)
            {
                strSql.Append("type='" + model.type + "',");
            }
            else
            {
                strSql.Append("type= null ,");
            }
            if (model.address != null)
            {
                strSql.Append("address='" + model.address + "',");
            }
            else
            {
                strSql.Append("address= null ,");
            }
            if (model.compName != null)
            {
                strSql.Append("compName='" + model.compName + "',");
            }
            else
            {
                strSql.Append("compName= null ,");
            }
            if (model.invName != null)
            {
                strSql.Append("invName='" + model.invName + "',");
            }
            else
            {
                strSql.Append("invName= null ,");
            }
            if (model.suiChePer != null)
            {
                strSql.Append("suiChePer='" + model.suiChePer + "',");
            }
            else
            {
                strSql.Append("suiChePer= null ,");
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
            strSql.Append("delete from tb_AppCar ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_AppCar GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" tb_AppCar.id,appUserId,datetiem,type,address,compName,invName,suiChePer,loginName,loginIPosition,proNo ");
            strSql.Append(" from tb_AppCar left join tb_User on tb_User.id=tb_AppCar.appUserId ");
            strSql.Append(" where tb_AppCar.id=" + id + "");

            VAN_OA.Model.EFrom.tb_AppCar model = null;
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
        public List<VAN_OA.Model.EFrom.tb_AppCar> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" tb_AppCar.id,appUserId,datetiem,type,address,compName,invName,suiChePer,loginName,loginIPosition,proNo ");
            strSql.Append(" from tb_AppCar left join tb_User on tb_User.id=tb_AppCar.appUserId ");
            strSql.Append(" FROM tb_AppCar ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.tb_AppCar> list = new List<VAN_OA.Model.EFrom.tb_AppCar>();

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
        public VAN_OA.Model.EFrom.tb_AppCar ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_AppCar model = new VAN_OA.Model.EFrom.tb_AppCar();
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
            ojb = dataReader["datetiem"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.datetiem = (DateTime)ojb;
            }

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LoginName = ojb.ToString();
            }


            ojb = dataReader["loginIPosition"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DepartName =ojb.ToString();
            }
            model.type = dataReader["type"].ToString();
            model.address = dataReader["address"].ToString();
            model.compName = dataReader["compName"].ToString();
            model.invName = dataReader["invName"].ToString();
            model.suiChePer = dataReader["suiChePer"].ToString();


            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }
            return model;
        }



    }
}
