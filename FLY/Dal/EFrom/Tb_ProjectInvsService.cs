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
    public class Tb_ProjectInvsService
    {  
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.EFrom.Tb_ProjectInvs model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.PId != null)
            {
                strSql1.Append("PId,");
                strSql2.Append("" + model.PId + ",");
            }
            if (model.BuyTime != null)
            {
                strSql1.Append("BuyTime,");
                strSql2.Append("'" + model.BuyTime + "',");
            }
            if (model.InvModel != null)
            {
                strSql1.Append("InvModel,");
                strSql2.Append("'" + model.InvModel + "',");
            }
            if (model.InvName != null)
            {
                strSql1.Append("InvName,");
                strSql2.Append("'" + model.InvName + "',");
            }
            if (model.InvUnit != null)
            {
                strSql1.Append("InvUnit,");
                strSql2.Append("'" + model.InvUnit + "',");
            }
            if (model.InvNum != null)
            {
                strSql1.Append("InvNum,");
                strSql2.Append("" + model.InvNum + ",");
            }
            if (model.InvPrice != null)
            {
                strSql1.Append("InvPrice,");
                strSql2.Append("" + model.InvPrice + ",");
            }
            if (model.InvCarPrice != null)
            {
                strSql1.Append("InvCarPrice,");
                strSql2.Append("" + model.InvCarPrice + ",");
            }
            if (model.InvTaskPrice != null)
            {
                strSql1.Append("InvTaskPrice,");
                strSql2.Append("" + model.InvTaskPrice + ",");
            }
            if (model.InvManPrice != null)
            {
                strSql1.Append("InvManPrice,");
                strSql2.Append("" + model.InvManPrice + ",");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            strSql.Append("insert into Tb_ProjectInvs(");
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
        public void Update(VAN_OA.Model.EFrom.Tb_ProjectInvs model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Tb_ProjectInvs set ");
            if (model.PId != null)
            {
                strSql.Append("PId=" + model.PId + ",");
            }
            if (model.BuyTime != null)
            {
                strSql.Append("BuyTime='" + model.BuyTime + "',");
            }
            if (model.InvModel != null)
            {
                strSql.Append("InvModel='" + model.InvModel + "',");
            }
            else
            {
                strSql.Append("InvModel= null ,");
            }
            if (model.InvName != null)
            {
                strSql.Append("InvName='" + model.InvName + "',");
            }
            if (model.InvUnit != null)
            {
                strSql.Append("InvUnit='" + model.InvUnit + "',");
            }
            else
            {
                strSql.Append("InvUnit= null ,");
            }
            if (model.InvNum != null)
            {
                strSql.Append("InvNum=" + model.InvNum + ",");
            }
            else
            {
                strSql.Append("InvNum= null ,");
            }
            if (model.InvPrice != null)
            {
                strSql.Append("InvPrice=" + model.InvPrice + ",");
            }
            else
            {
                strSql.Append("InvPrice= null ,");
            }
            if (model.InvCarPrice != null)
            {
                strSql.Append("InvCarPrice=" + model.InvCarPrice + ",");
            }
            else
            {
                strSql.Append("InvCarPrice= null ,");
            }
            if (model.InvTaskPrice != null)
            {
                strSql.Append("InvTaskPrice=" + model.InvTaskPrice + ",");
            }
            else
            {
                strSql.Append("InvTaskPrice= null ,");
            }
            if (model.InvManPrice != null)
            {
                strSql.Append("InvManPrice=" + model.InvManPrice + ",");
            }
            else
            {
                strSql.Append("InvManPrice= null ,");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
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
        public void Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tb_ProjectInvs ");
            strSql.Append(" where Id=" + Id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string Id, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tb_ProjectInvs ");
            strSql.Append(" where Id in(" + Id + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }
        /// 删除一条数据
        /// </summary>
        public void DeleteById(int pid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Tb_ProjectInvs ");
            strSql.Append(" where pid=" + pid + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.Tb_ProjectInvs GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Id,PId,BuyTime,InvModel,InvName,InvUnit,InvNum,InvPrice,InvCarPrice,InvTaskPrice,InvManPrice,Remark,isnull(InvPrice,0)+isnull(InvCarPrice,0)+isnull(InvTaskPrice,0)+isnull(InvManPrice,0) as Total ");
            strSql.Append(" from Tb_ProjectInvs ");
            strSql.Append(" where Ids=" + Ids + "");
            VAN_OA.Model.EFrom.Tb_ProjectInvs model = null;
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
        public List<VAN_OA.Model.EFrom.Tb_ProjectInvs> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,PId,BuyTime,InvModel,InvName,InvUnit,InvNum,InvPrice,InvCarPrice,InvTaskPrice,InvManPrice,Remark,isnull(InvPrice,0)+isnull(InvCarPrice,0)+isnull(InvTaskPrice,0)+isnull(InvManPrice,0) as Total ");
            strSql.Append(" FROM Tb_ProjectInvs ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.Tb_ProjectInvs> list = new List<VAN_OA.Model.EFrom.Tb_ProjectInvs>();

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
        public VAN_OA.Model.EFrom.Tb_ProjectInvs ReaderBind(IDataReader dataReader)
        {
            Model.EFrom.Tb_ProjectInvs model = new Model.EFrom.Tb_ProjectInvs();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["PId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PId = (int)ojb;
            }
            ojb = dataReader["BuyTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BuyTime = (DateTime)ojb;
            }
            model.InvModel = dataReader["InvModel"].ToString();
            model.InvName = dataReader["InvName"].ToString();
            model.InvUnit = dataReader["InvUnit"].ToString();
            ojb = dataReader["InvNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.InvNum = (decimal)ojb;
            }
            ojb = dataReader["InvPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.InvPrice = (decimal)ojb;
            }
            ojb = dataReader["InvCarPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.InvCarPrice = (decimal)ojb;
            }
            ojb = dataReader["InvTaskPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.InvTaskPrice = (decimal)ojb;
            }
            ojb = dataReader["InvManPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.InvManPrice = (decimal)ojb;
            }
            model.Remark = dataReader["Remark"].ToString();
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Total = Convert.ToDecimal(ojb);
            }    

            return model;
        }



        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.Tb_ProjectInvs> GetListArray_Rep(string strWhere)
        {
            


            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Tb_ProjectInv_View ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by ProNo desc ");
            List<VAN_OA.Model.EFrom.Tb_ProjectInvs> list = new List<VAN_OA.Model.EFrom.Tb_ProjectInvs>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        Model.EFrom.Tb_ProjectInvs model = new Model.EFrom.Tb_ProjectInvs();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        //ojb = dataReader["PId"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.PId = (int)ojb;
                        //}
                        ojb = dataReader["BuyTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.BuyTime = (DateTime)ojb;
                        }
                        model.InvModel = dataReader["InvModel"].ToString();
                        model.InvName = dataReader["InvName"].ToString();
                        model.InvUnit = dataReader["InvUnit"].ToString();
                        ojb = dataReader["InvNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvNum = (decimal)ojb;
                        }
                        ojb = dataReader["InvPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvPrice = (decimal)ojb;
                        }
                        ojb = dataReader["InvCarPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvCarPrice = (decimal)ojb;
                        }
                        ojb = dataReader["InvTaskPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvTaskPrice = (decimal)ojb;
                        }
                        ojb = dataReader["InvManPrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvManPrice = (decimal)ojb;
                        }
                        model.Remark = dataReader["Remark"].ToString();


                        ojb = dataReader["CreateTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreateTime = (DateTime)ojb;
                        }
                        model.ProNo = dataReader["ProNo"].ToString();
                        model.ProName = dataReader["ProName"].ToString();

                        ojb = dataReader["loginName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LoginName = ojb.ToString();
                        }
                        ojb = dataReader["State"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.State = ojb.ToString();
                        }

                        ojb = dataReader["Total"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Total =Convert.ToDecimal( ojb);
                        }

                        ojb = dataReader["guestName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestName = ojb.ToString();
                        }
                        ojb = dataReader["efState"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.EfState = ojb.ToString();
                        }    

                        
                        list.Add(model);
                    }
                }
            }
            return list;
        }


    }
}
