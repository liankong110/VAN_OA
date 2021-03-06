﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.BaseInfo
{
    public class Invoice_PersonService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.Invoice_Person model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Name != null)
            {
                strSql1.Append("Name,");
                strSql2.Append("'" + model.Name + "',");
            }
            if (model.CardNo != null)
            {
                strSql1.Append("CardNo,");
                strSql2.Append("'" + model.CardNo + "',");
            }
            if (model.Phone != null)
            {
                strSql1.Append("Phone,");
                strSql2.Append("'" + model.Phone + "',");
            }
            if (model.IsStop != null)
            {
                strSql1.Append("IsStop,");
                strSql2.Append("" + (model.IsStop ? 1 : 0) + ",");
            }

            strSql.Append("insert into Invoice_Person(");
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
        public bool Update(VAN_OA.Model.BaseInfo.Invoice_Person model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Invoice_Person set ");
            if (model.Name != null)
            {
                strSql.Append("Name='" + model.Name + "',");
            }
            if (model.CardNo != null)
            {
                strSql.Append("CardNo='" + model.CardNo + "',");
            }
            if (model.Phone != null)
            {
                strSql.Append("Phone='" + model.Phone + "',");
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
            strSql.Append("delete from Invoice_Person ");
            strSql.Append(" where Id=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.Invoice_Person GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select Id,Name,CardNo,Phone,IsStop ");
            strSql.Append(" FROM Invoice_Person ");
            strSql.Append(" where ID=" + ID + "");

            VAN_OA.Model.BaseInfo.Invoice_Person model = null;
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
        public List<VAN_OA.Model.BaseInfo.Invoice_Person> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Name,CardNo,Phone,IsStop ");
            strSql.Append(" FROM Invoice_Person ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id desc ");
            List<VAN_OA.Model.BaseInfo.Invoice_Person> list = new List<VAN_OA.Model.BaseInfo.Invoice_Person>();

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
        public VAN_OA.Model.BaseInfo.Invoice_Person ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.Invoice_Person model = new VAN_OA.Model.BaseInfo.Invoice_Person();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.Name = dataReader["Name"].ToString();
            model.CardNo = dataReader["CardNo"].ToString();
            model.Phone = dataReader["Phone"].ToString();
            ojb = dataReader["IsStop"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsStop = (bool)ojb;
            }
            return model;
        }
    }
}