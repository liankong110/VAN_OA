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
using VAN_OA.Model.OA;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.EFrom;


namespace VAN_OA.Dal.ReportForms
{
    public class TB_CarMaintenanceService
    {


        public bool updateTran(VAN_OA.Model.ReportForms.TB_CarMaintenance model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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


        public string GetAllE_No(string tableName, SqlCommand objCommand)
        {
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(UseState),4))+1))),4) FROM  {0};", tableName);
            objCommand.CommandText = sql.ToString();
            object objMax = objCommand.ExecuteScalar();
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = DateTime.Now.Year.ToString() + objMax.ToString();
            }
            else
            {
                MaxCardNo = DateTime.Now.Year.ToString() + "0001";
            }

            return MaxCardNo;
        }



        public int addTran(VAN_OA.Model.ReportForms.TB_CarMaintenance model, VAN_OA.Model.EFrom.tb_EForm eform)
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

                    string proNo = GetAllE_No("TB_CarMaintenance", objCommand);
                    model.UseState = proNo;
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
        public int Add(VAN_OA.Model.ReportForms.TB_CarMaintenance model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.CardNo != null)
            {
                strSql1.Append("CardNo,");
                strSql2.Append("'" + model.CardNo + "',");
            }
            if (model.MaintenanceTime != null)
            {
                strSql1.Append("MaintenanceTime,");
                strSql2.Append("'" + model.MaintenanceTime + "',");
            }
            if (model.Distance != null)
            {
                strSql1.Append("Distance,");
                strSql2.Append("" + model.Distance + ",");
            }
            if (model.ReplaceRemark != null)
            {
                strSql1.Append("ReplaceRemark,");
                strSql2.Append("'" + model.ReplaceRemark + "',");
            }
            if (model.ReplaceStatus != null)
            {
                strSql1.Append("ReplaceStatus,");
                strSql2.Append("'" + model.ReplaceStatus + "',");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            if (model.CreateUser != null)
            {
                strSql1.Append("CreateUser,");
                strSql2.Append("" + model.CreateUser + ",");
            }
            if (model.CreateTime != null)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("'" + model.CreateTime + "',");
            }

            if (model.UpTotal != null)
            {
                strSql1.Append("UpTotal,");
                strSql2.Append("" + model.UpTotal + ",");
            }
            if (model.OilTotal != null)
            {
                strSql1.Append("OilTotal,");
                strSql2.Append("" + model.OilTotal + ",");
            }
            if (model.AddTotal != null)
            {
                strSql1.Append("AddTotal,");
                strSql2.Append("" + model.AddTotal + ",");
            }
            if (model.Total != null)
            {
                strSql1.Append("Total,");
                strSql2.Append("" + model.Total + ",");
            }

            if (model.UseState != null)
            {
                strSql1.Append("UseState,");
                strSql2.Append("'" + model.UseState + "',");
            }



            strSql.Append("insert into TB_CarMaintenance(");
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
        public void Update(VAN_OA.Model.ReportForms.TB_CarMaintenance model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_CarMaintenance set ");
            if (model.CardNo != null)
            {
                strSql.Append("CardNo='" + model.CardNo + "',");
            }
            if (model.MaintenanceTime != null)
            {
                strSql.Append("MaintenanceTime='" + model.MaintenanceTime + "',");
            }
            if (model.Distance != null)
            {
                strSql.Append("Distance=" + model.Distance + ",");
            }
            if (model.ReplaceRemark != null)
            {
                strSql.Append("ReplaceRemark='" + model.ReplaceRemark + "',");
            }
            else
            {
                strSql.Append("ReplaceRemark= null ,");
            }
            if (model.ReplaceStatus != null)
            {
                strSql.Append("ReplaceStatus='" + model.ReplaceStatus + "',");
            }
            else
            {
                strSql.Append("ReplaceStatus= null ,");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }
          
            if (model.UpTotal != null)
            {
                strSql.Append("UpTotal=" + model.UpTotal + ",");
            }
            else
            {
                strSql.Append("UpTotal= null ,");
            }
            if (model.OilTotal != null)
            {
                strSql.Append("OilTotal=" + model.OilTotal + ",");
            }
            else
            {
                strSql.Append("OilTotal= null ,");
            }
            if (model.AddTotal != null)
            {
                strSql.Append("AddTotal=" + model.AddTotal + ",");
            }
            else
            {
                strSql.Append("AddTotal= null ,");
            }
            if (model.Total != null)
            {
                strSql.Append("Total=" + model.Total + ",");
            }
            else
            {
                strSql.Append("Total= null ,");
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
        public bool Update(VAN_OA.Model.ReportForms.TB_CarMaintenance model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_CarMaintenance set ");
            if (model.CardNo != null)
            {
                strSql.Append("CardNo='" + model.CardNo + "',");
            }
            if (model.MaintenanceTime != null)
            {
                strSql.Append("MaintenanceTime='" + model.MaintenanceTime + "',");
            }
            if (model.Distance != null)
            {
                strSql.Append("Distance=" + model.Distance + ",");
            }
            if (model.ReplaceRemark != null)
            {
                strSql.Append("ReplaceRemark='" + model.ReplaceRemark + "',");
            }
            else
            {
                strSql.Append("ReplaceRemark= null ,");
            }
            if (model.ReplaceStatus != null)
            {
                strSql.Append("ReplaceStatus='" + model.ReplaceStatus + "',");
            }
            else
            {
                strSql.Append("ReplaceStatus= null ,");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }

            if (model.UpTotal != null)
            {
                strSql.Append("UpTotal=" + model.UpTotal + ",");
            }
            else
            {
                strSql.Append("UpTotal= null ,");
            }
            if (model.OilTotal != null)
            {
                strSql.Append("OilTotal=" + model.OilTotal + ",");
            }
            else
            {
                strSql.Append("OilTotal= null ,");
            }
            if (model.AddTotal != null)
            {
                strSql.Append("AddTotal=" + model.AddTotal + ",");
            }
            else
            {
                strSql.Append("AddTotal= null ,");
            }
            if (model.Total != null)
            {
                strSql.Append("Total=" + model.Total + ",");
            }
            else
            {
                strSql.Append("Total= null ,");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            return Convert.ToBoolean(DBHelp.ExeCommand(strSql.ToString())); 
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_CarMaintenance ");
            strSql.Append(" where Id=" + Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.ReportForms.TB_CarMaintenance GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" TB_CarMaintenance.Id,CardNo,MaintenanceTime,Distance,ReplaceRemark,ReplaceStatus,Remark,CreateUser,CreateTime,loginName,UpTotal,OilTotal,AddTotal,Total,UseState  ");
            strSql.Append(" from TB_CarMaintenance left join tb_User on TB_CarMaintenance.CreateUser=tb_User.ID");
            strSql.Append(" where TB_CarMaintenance.Id=" + Id + "");
            
            
            VAN_OA.Model.ReportForms.TB_CarMaintenance model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (objReader.Read())
                    {
                        model = ReaderBind(objReader);
                    }
                }
            }
            return model;
        }
        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.ReportForms.TB_CarMaintenance> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TB_CarMaintenance.Id,CardNo,MaintenanceTime,Distance,ReplaceRemark,ReplaceStatus,Remark,CreateUser,CreateTime ,loginName,UpTotal,OilTotal,AddTotal,Total,UseState ");
            strSql.Append(" FROM TB_CarMaintenance left join tb_User on TB_CarMaintenance.CreateUser=tb_User.ID");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by  MaintenanceTime desc " );
            List<VAN_OA.Model.ReportForms.TB_CarMaintenance> list = new List<VAN_OA.Model.ReportForms.TB_CarMaintenance>();
            
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
        public VAN_OA.Model.ReportForms.TB_CarMaintenance ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.ReportForms.TB_CarMaintenance model = new VAN_OA.Model.ReportForms.TB_CarMaintenance();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.CardNo = dataReader["CardNo"].ToString();
            ojb = dataReader["MaintenanceTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MaintenanceTime = (DateTime)ojb;
            }
            ojb = dataReader["Distance"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Distance = (decimal)ojb;
            }
            model.ReplaceRemark = dataReader["ReplaceRemark"].ToString();
            model.ReplaceStatus = dataReader["ReplaceStatus"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            ojb = dataReader["CreateUser"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUser = (int)ojb;
            }
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UserName =ojb.ToString();
            }


            ojb = dataReader["UpTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UpTotal = (decimal)ojb;
            }
            ojb = dataReader["OilTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OilTotal = (decimal)ojb;
            }
            ojb = dataReader["AddTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AddTotal = (decimal)ojb;
            }
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Total = (decimal)ojb;
            }

            ojb = dataReader["UseState"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UseState = ojb.ToString();
            }


            return model;
        }
    }
}
