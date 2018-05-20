using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAN_OA.Model.OA;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace VAN_OA.Dal.ReportForms
{
    public class TB_BusCardRecordService
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.ReportForms.TB_BusCardRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.BusCardNo != null)
            {
                strSql1.Append("BusCardNo,");
                strSql2.Append("'" + model.BusCardNo + "',");
            }
            if (model.BusCardPer != null)
            {
                strSql1.Append("BusCardPer,");
                strSql2.Append("'" + model.BusCardPer + "',");
            }
            if (model.BusCardDate != null)
            {
                strSql1.Append("BusCardDate,");
                strSql2.Append("'" + model.BusCardDate + "',");
            }
            if (model.BusCardTotal != null)
            {
                strSql1.Append("BusCardTotal,");
                strSql2.Append("" + model.BusCardTotal + ",");
            }
            if (model.BusCardRemark != null)
            {
                strSql1.Append("BusCardRemark,");
                strSql2.Append("'" + model.BusCardRemark + "',");
            }
            strSql.Append("insert into TB_BusCardRecord(");
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
        public bool Update(VAN_OA.Model.ReportForms.TB_BusCardRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_BusCardRecord set ");
            if (model.BusCardNo != null)
            {
                strSql.Append("BusCardNo='" + model.BusCardNo + "',");
            }
            if (model.BusCardPer != null)
            {
                strSql.Append("BusCardPer='" + model.BusCardPer + "',");
            }
            if (model.BusCardDate != null)
            {
                strSql.Append("BusCardDate='" + model.BusCardDate + "',");
            }
            if (model.BusCardTotal != null)
            {
                strSql.Append("BusCardTotal=" + model.BusCardTotal + ",");
            }
            if (model.BusCardRemark != null)
            {
                strSql.Append("BusCardRemark='" + model.BusCardRemark + "',");
            }
            else
            {
                strSql.Append("BusCardRemark= null ,");
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
            strSql.Append("delete from TB_BusCardRecord ");
            strSql.Append(" where Id=" + Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.ReportForms.TB_BusCardRecord> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,BusCardNo,BusCardPer,BusCardDate,BusCardTotal,BusCardRemark ");
            strSql.Append(" FROM TB_BusCardRecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by id desc " );

            List<VAN_OA.Model.ReportForms.TB_BusCardRecord> list = new List<VAN_OA.Model.ReportForms.TB_BusCardRecord>();
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
        public VAN_OA.Model.ReportForms.TB_BusCardRecord ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.ReportForms.TB_BusCardRecord model = new VAN_OA.Model.ReportForms.TB_BusCardRecord();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.BusCardNo = dataReader["BusCardNo"].ToString();
            model.BusCardPer = dataReader["BusCardPer"].ToString();
            ojb = dataReader["BusCardDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BusCardDate = (DateTime)ojb;
            }
            ojb = dataReader["BusCardTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BusCardTotal = (decimal)ojb;
            }
            model.BusCardRemark = dataReader["BusCardRemark"].ToString();
            return model;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.ReportForms.TB_BusCardRecord GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Id,BusCardNo,BusCardPer,BusCardDate,BusCardTotal,BusCardRemark ");
            strSql.Append(" from TB_BusCardRecord ");
            strSql.Append(" where Id=" + Id + "");
            VAN_OA.Model.ReportForms.TB_BusCardRecord model = new VAN_OA.Model.ReportForms.TB_BusCardRecord();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (objReader.Read())
                    {
                        model=ReaderBind(objReader);
                    }
                }
            }
            return model;
        }
    }
}
