using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.Fin
{
    public class FIN_SpecCostService
    {
        public string GetAllE_No( SqlCommand objCommand)
        {
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(ProNo),4))+1))),4) FROM  FIN_SpecCost where ProNo like '{0}%';",
                DateTime.Now.Year);
            objCommand.CommandText = sql;
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

        public bool AddList(List<VAN_OA.Model.Fin.FIN_SpecCost> models)
        {
            if (models.Count > 0)
            {
                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlTransaction tan = conn.BeginTransaction();
                    SqlCommand objCommand = conn.CreateCommand();
                    objCommand.Transaction = tan;
                    try
                    {
                        objCommand.CommandText = string.Format("delete from FIN_SpecCost where CaiYear='{0}' and CompId={1} and UserId={2}", models[0].CaiYear, models[0].CompId, models[0].UserID);
                        objCommand.ExecuteNonQuery();
                        objCommand.Parameters.Clear();
                        foreach (var m in models)
                        {
                            Add(m, objCommand);
                        }
                        tan.Commit();
                    }
                    catch (Exception)
                    {
                        tan.Rollback();
                        return false;

                    }
                    conn.Close();
                }
            }
            return true;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.Fin.FIN_SpecCost model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FIN_SpecCost(");
            strSql.Append("ProNo,CostTypeId,Total,CaiYear,CompId,UserID");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("'" + GetAllE_No(objCommand) + "',");
            strSql.Append("" + model.CostTypeId + ",");
            strSql.Append("" + model.Total + ",");
            strSql.Append("'" + model.CaiYear + "',");
            strSql.Append("" + model.CompId + ",");
            strSql.Append("" + model.UserID + "");
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            objCommand.CommandText = strSql.ToString();
            int result;
            object obj = objCommand.ExecuteScalar();
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.Fin.FIN_SpecCost model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FIN_SpecCost(");
            strSql.Append("ProNo,CostTypeId,Total,CaiYear,CompId,UserID");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("'" + model.ProNo + "',");
            strSql.Append("" + model.CostTypeId + ",");
            strSql.Append("" + model.Total + ",");
            strSql.Append("'" + model.CaiYear + "',");
            strSql.Append("" + model.CompId + ",");
            strSql.Append("" + model.UserID + "");
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
        public bool Update(VAN_OA.Model.Fin.FIN_SpecCost model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FIN_SpecCost set ");
            strSql.Append("ProNo='" + model.ProNo + "',");
            strSql.Append("CostTypeId=" + model.CostTypeId + ",");
            strSql.Append("UserID=" + model.UserID + ",");
            strSql.Append("Total=" + model.Total + ",");
            strSql.Append("CaiYear='" + model.CaiYear + "',");
            strSql.Append("CompId=" + model.CompId + "");
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
            strSql.Append("delete from FIN_SpecCost ");
            strSql.Append(" where Id=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.Fin.FIN_SpecCost GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProNo,CostTypeId,Total,CaiYear,CompId,UserID ");
            strSql.Append(" FROM FIN_SpecCost ");
            strSql.Append(" where ID=" + ID + "");

            VAN_OA.Model.Fin.FIN_SpecCost model = null;
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
        public List<VAN_OA.Model.Fin.FIN_SpecCost> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProNo,CostTypeId,Total,CaiYear,CompId,UserID ");
            strSql.Append(" FROM FIN_SpecCost ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id desc ");
            List<VAN_OA.Model.Fin.FIN_SpecCost> list = new List<VAN_OA.Model.Fin.FIN_SpecCost>();

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
        public VAN_OA.Model.Fin.FIN_SpecCost ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.Fin.FIN_SpecCost model = new VAN_OA.Model.Fin.FIN_SpecCost();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.ProNo = dataReader["ProNo"].ToString();
            model.CostTypeId = Convert.ToInt32(dataReader["CostTypeId"]);
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Total = (decimal)ojb;
            }
            model.CaiYear = dataReader["CaiYear"].ToString();
            model.CompId =Convert.ToInt32(dataReader["CompId"]);
            model.UserID = Convert.ToInt32(dataReader["UserID"]);            
            return model;
        }
    }
}