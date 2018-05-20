using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.Fin
{
    public class In_BankFlowService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.Fin.In_BankFlow model, SqlCommand sqlCommand)
        {

            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(Number),6))+1))),6) FROM In_BankFlow where Number like '{0}%';", DateTime.Now.Year + DateTime.Now.Month.ToString("00"));
            sqlCommand.CommandText = sql.ToString();
            object objMax = sqlCommand.ExecuteScalar();
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + objMax.ToString();
            }
            else
            {
                MaxCardNo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + "000001";
            }
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (MaxCardNo != null)
            {
                strSql1.Append("Number,");
                strSql2.Append("'" + MaxCardNo + "',");
            }
            if (model.ReferenceNumber != null)
            {
                strSql1.Append("ReferenceNumber,");
                strSql2.Append("'" + model.ReferenceNumber + "',");
            }
            if (model.InType != null)
            {
                strSql1.Append("InType,");
                strSql2.Append("'" + model.InType + "',");
            }
            if (model.FPNo != null)
            {
                strSql1.Append("FPNo,");
                strSql2.Append("'" + model.FPNo + "',");
            }
            if (model.FPTotal != null)
            {
                strSql1.Append("FPTotal,");
                strSql2.Append("" + model.FPTotal + ",");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            strSql.Append("insert into In_BankFlow(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            sqlCommand.CommandText = strSql.ToString();

            object obj = sqlCommand.ExecuteScalar();
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
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.Fin.In_BankFlow model)
        {

            string MaxNumber = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(Number),6))+1))),6) FROM In_BankFlow where Number like '{0}%';", DateTime.Now.Year + DateTime.Now.Month.ToString("00"));

            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                MaxNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + objMax.ToString();
            }
            else
            {
                MaxNumber = DateTime.Now.Year.ToString()+ DateTime.Now.Month.ToString("00") + "000001";
            }

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (MaxNumber != null)
            {
                strSql1.Append("Number,");
                strSql2.Append("'" + MaxNumber + "',");
            }
            if (model.ReferenceNumber != null)
            {
                strSql1.Append("ReferenceNumber,");
                strSql2.Append("'" + model.ReferenceNumber + "',");
            }
            if (model.InType != null)
            {
                strSql1.Append("InType,");
                strSql2.Append("'" + model.InType + "',");
            }
            if (model.FPNo != null)
            {
                strSql1.Append("FPNo,");
                strSql2.Append("'" + model.FPNo + "',");
            }
            if (model.FPTotal != null)
            {
                strSql1.Append("FPTotal,");
                strSql2.Append("" + model.FPTotal + ",");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            strSql.Append("insert into In_BankFlow(");
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
        public bool Update(VAN_OA.Model.Fin.In_BankFlow model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update In_BankFlow set ");
            if (model.Number != null)
            {
                strSql.Append("Number='" + model.Number + "',");
            }
            if (model.ReferenceNumber != null)
            {
                strSql.Append("ReferenceNumber='" + model.ReferenceNumber + "',");
            }
            if (model.InType != null)
            {
                strSql.Append("InType='" + model.InType + "',");
            }
            if (model.FPNo != null)
            {
                strSql.Append("FPNo='" + model.FPNo + "',");
            }
            if (model.FPTotal != null)
            {
                strSql.Append("FPTotal=" + model.FPTotal + ",");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
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
            strSql.Append("delete from In_BankFlow ");
            strSql.Append(" where ID=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.Fin.In_BankFlow GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Number,ReferenceNumber,InType,FPNo,FPTotal,Remark ");
            strSql.Append(" FROM In_BankFlow ");
            strSql.Append(" where In_BankFlow.ID=" + ID + "");

            VAN_OA.Model.Fin.In_BankFlow model = null;
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
        public List<VAN_OA.Model.Fin.In_BankFlow> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Number,ReferenceNumber,InType,FPNo,FPTotal,Remark ");
            strSql.Append(" FROM In_BankFlow ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by id desc");
            List<VAN_OA.Model.Fin.In_BankFlow> list = new List<VAN_OA.Model.Fin.In_BankFlow>();

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
        public VAN_OA.Model.Fin.In_BankFlow ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.Fin.In_BankFlow model = new VAN_OA.Model.Fin.In_BankFlow();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.Number = dataReader["Number"].ToString();
            model.ReferenceNumber = dataReader["ReferenceNumber"].ToString();
            model.InType = dataReader["InType"].ToString();
            model.FPNo = dataReader["FPNo"].ToString();
            ojb = dataReader["FPTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPTotal = (decimal)ojb;
            }
            model.Remark = dataReader["Remark"].ToString();
            return model;
        }



    }
}