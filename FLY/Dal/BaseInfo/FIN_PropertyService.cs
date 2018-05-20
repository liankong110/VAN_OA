using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class FIN_PropertyService
    {
        public string GetAllE_No()
        {
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(ProNo),4))+1))),4) FROM  FIN_Property where ProNo like '{0}%';",
                DateTime.Now.Year);

            object objMax = DBHelp.ExeScalar(sql);
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

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.FIN_Property model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FIN_Property(");
            strSql.Append("ProNo,CostType,MyProperty");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("'" + GetAllE_No() + "',");
            strSql.Append("'" + model.CostType + "',");
            strSql.Append("'" + model.MyProperty + "'");
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
        public bool Update(VAN_OA.Model.BaseInfo.FIN_Property model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FIN_Property set ");
            strSql.Append("ProNo='" + model.ProNo + "',");
            strSql.Append("CostType='" + model.CostType + "',");
            strSql.Append("MyProperty='" + model.MyProperty + "'");
            strSql.Append(" where Id=" + model.Id + "");
            bool rowsAffected = DBHelp.ExeCommand(strSql.ToString());
            return rowsAffected;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from FIN_Property ");
            strSql.Append(" where Id=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.FIN_Property GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProNo,CostType,MyProperty ");
            strSql.Append(" FROM FIN_Property ");
            strSql.Append(" where ID=" + ID + "");

            VAN_OA.Model.BaseInfo.FIN_Property model = null;
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
        public List<VAN_OA.Model.BaseInfo.FIN_Property> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProNo,CostType,MyProperty ");
            strSql.Append(" FROM FIN_Property ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by MyProperty desc,id  ");
            List<VAN_OA.Model.BaseInfo.FIN_Property> list = new List<VAN_OA.Model.BaseInfo.FIN_Property>();

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
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.BaseInfo.FIN_Property> GetListArray_CommCost(string yeatMonth, string CompId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FIN_Property.Id,CostType,FIN_CommCost.Total ");
            strSql.AppendFormat(" FROM FIN_Property left join FIN_CommCost on FIN_CommCost.CostTypeId=FIN_Property.Id and CaiYear='{0}' and CompId in ({1})", yeatMonth, CompId);

            strSql.Append(" where MyProperty='公共' order by FIN_Property.Id  ");
            List<VAN_OA.Model.BaseInfo.FIN_Property> list = new List<VAN_OA.Model.BaseInfo.FIN_Property>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.BaseInfo.FIN_Property model = new VAN_OA.Model.BaseInfo.FIN_Property();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                       
                        model.CostType = dataReader["CostType"].ToString();
                        ojb = dataReader["Total"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Value = Convert.ToDecimal(dataReader["Total"]);
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.BaseInfo.FIN_Property> GetListArray_SpecCost(string yeatMonth, string CompId,string userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FIN_Property.Id,CostType,FIN_SpecCost.Total,FIN_SpecCost.UserId ");
            if (userId != "-1")
            {
                strSql.AppendFormat(" FROM FIN_Property left join FIN_SpecCost on FIN_SpecCost.CostTypeId=FIN_Property.Id and CaiYear='{0}' and CompId in ({1}) and userId={2}", yeatMonth, CompId, userId);
            }
            else
            {
                strSql.AppendFormat(" FROM FIN_Property left join FIN_SpecCost on FIN_SpecCost.CostTypeId=FIN_Property.Id and CaiYear='{0}' and CompId in ({1}) ", yeatMonth, CompId);
            }
            strSql.Append(" where MyProperty='个性' order by FIN_Property.Id  ");
            List<VAN_OA.Model.BaseInfo.FIN_Property> list = new List<VAN_OA.Model.BaseInfo.FIN_Property>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.BaseInfo.FIN_Property model = new VAN_OA.Model.BaseInfo.FIN_Property();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }

                        model.CostType = dataReader["CostType"].ToString();
                        ojb = dataReader["Total"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Value = Convert.ToDecimal(dataReader["Total"]);
                        }
                        ojb = dataReader["UserId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.UserId = Convert.ToInt32(dataReader["UserId"]);
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }



        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.BaseInfo.FIN_Property ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.FIN_Property model = new VAN_OA.Model.BaseInfo.FIN_Property();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.ProNo = dataReader["ProNo"].ToString();
            model.CostType = dataReader["CostType"].ToString();
            model.MyProperty = dataReader["MyProperty"].ToString();
            return model;
        }
    }
}