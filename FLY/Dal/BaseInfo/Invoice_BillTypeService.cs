using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class Invoice_BillTypeService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.Invoice_BillType model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();

            if (model.BillName != null)
            {
                strSql1.Append("BillName,");
                strSql2.Append("'" + model.BillName + "',");
            }
            if (model.BillType != null)
            {
                strSql1.Append("BillType,");
                strSql2.Append("'" + model.BillType + "',");
            }
            if (model.IsStop != null)
            {
                strSql1.Append("IsStop,");
                strSql2.Append("" + (model.IsStop ? 1 : 0) + ",");
            }

            strSql.Append("insert into Invoice_BillType(");
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
        public bool Update(VAN_OA.Model.BaseInfo.Invoice_BillType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Invoice_BillType set ");
            if (model.BillName != null)
            {
                strSql.Append("BillName='" + model.BillName + "',");
            }
            if (model.BillType != null)
            {
                strSql.Append("BillType='" + model.BillType + "',");
            }
            if (model.IsStop != null)
            {
                strSql.Append("IsStop=" + (model.IsStop ? 1 : 0) + ",");
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
            strSql.Append("delete from Invoice_BillType ");
            strSql.Append(" where Id=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.Invoice_BillType GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select   ");
            strSql.Append(" Id,BillName,BillType,IsStop ");
            strSql.Append(" from Invoice_BillType ");
            strSql.Append(" where ID=" + ID + "");

            VAN_OA.Model.BaseInfo.Invoice_BillType model = null;
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
        public List<VAN_OA.Model.BaseInfo.Invoice_BillType> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Id,BillName,BillType,IsStop ");
            strSql.Append(" from Invoice_BillType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id desc ");
            List<VAN_OA.Model.BaseInfo.Invoice_BillType> list = new List<VAN_OA.Model.BaseInfo.Invoice_BillType>();

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
        public VAN_OA.Model.BaseInfo.Invoice_BillType ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.Invoice_BillType model = new VAN_OA.Model.BaseInfo.Invoice_BillType();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.BillName = dataReader["BillName"].ToString();
            model.BillType = dataReader["BillType"].ToString();
            ojb = dataReader["IsStop"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsStop = (bool)ojb;
            }
            return model;
        }
    }
}