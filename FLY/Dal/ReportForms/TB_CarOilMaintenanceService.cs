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
namespace VAN_OA.Dal.ReportForms
{
    public class TB_CarOilMaintenanceService
    {
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.ReportForms.TB_CarOilMaintenance model)
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

            if (model.ChongZhiDate != null)
            {
                strSql1.Append("ChongZhiDate,");
                strSql2.Append("'" + model.ChongZhiDate + "',");
            }

           

            strSql.Append("insert into TB_CarOilMaintenance(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            object obj = DBHelp.ExeScalar(strSql.ToString());
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
        public bool Update(VAN_OA.Model.ReportForms.TB_CarOilMaintenance model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_CarOilMaintenance set ");
            if (model.CardNo != null)
            {
                strSql.Append("CardNo='" + model.CardNo + "',");
            }
            if (model.MaintenanceTime != null)
            {
                strSql.Append("MaintenanceTime='" + model.MaintenanceTime + "',");
            }
            
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }
            //if (model.CreateUser != null)
            //{
            //    strSql.Append("CreateUser=" + model.CreateUser + ",");
            //}
            //if (model.CreateTime != null)
            //{
            //    strSql.Append("CreateTime='" + model.CreateTime + "',");
            //}

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

            if (model.ChongZhiDate != null)
            {
                strSql.Append("ChongZhiDate='" + model.ChongZhiDate + "',");
            }
            else
            {
                strSql.Append("ChongZhiDate= null ,");
            }
            

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_CarOilMaintenance ");
            strSql.Append(" where Id=" + Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.ReportForms.TB_CarOilMaintenance GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" TB_CarOilMaintenance.Id,CardNo,MaintenanceTime,Remark,CreateUser,CreateTime,loginName,UpTotal,OilTotal,AddTotal,Total ,ChongZhiDate ");
            strSql.Append(" from TB_CarOilMaintenance left join tb_User on TB_CarOilMaintenance.CreateUser=tb_User.ID");
            strSql.Append(" where TB_CarOilMaintenance.Id=" + Id + "");


            VAN_OA.Model.ReportForms.TB_CarOilMaintenance model = null;
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
        public List<VAN_OA.Model.ReportForms.TB_CarOilMaintenance> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TB_CarOilMaintenance.Id,CardNo,MaintenanceTime,Remark,CreateUser,CreateTime ,loginName,UpTotal,OilTotal,AddTotal,Total,ChongZhiDate ");
            strSql.Append(" FROM TB_CarOilMaintenance left join tb_User on TB_CarOilMaintenance.CreateUser=tb_User.ID");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by  MaintenanceTime desc");
            List<VAN_OA.Model.ReportForms.TB_CarOilMaintenance> list = new List<VAN_OA.Model.ReportForms.TB_CarOilMaintenance>();

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
        public VAN_OA.Model.ReportForms.TB_CarOilMaintenance ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.ReportForms.TB_CarOilMaintenance model = new VAN_OA.Model.ReportForms.TB_CarOilMaintenance();
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
                model.UserName = ojb.ToString();
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

            ojb = dataReader["ChongZhiDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ChongZhiDate = (DateTime)ojb;
            }

            
            return model;
        }
    }
}
