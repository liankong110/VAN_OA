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
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace VAN_OA.Dal.BaseInfo
{
    public class GuestProBaseInfoService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.BaseInfo.GuestProBaseInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.GuestPro != null)
            {
                strSql1.Append("GuestPro,");
                strSql2.Append("" + model.GuestPro + ",");
            }
            if (model.JiLiXiShu != null)
            {
                strSql1.Append("JiLiXiShu,");
                strSql2.Append("" + model.JiLiXiShu + ",");
            }
            if (model.XiShu != null)
            {
                strSql1.Append("XiShu,");
                strSql2.Append("" + model.XiShu + ",");
            }
            strSql.Append("insert into GuestProBaseInfo(");
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
        public bool Update(VAN_OA.Model.BaseInfo.GuestProBaseInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update GuestProBaseInfo set ");
            if (model.GuestPro != null)
            {
                strSql.Append("GuestPro=" + model.GuestPro + ",");
            }
            if (model.JiLiXiShu != null)
            {
                strSql.Append("JiLiXiShu=" + model.JiLiXiShu + ",");
            }
            if (model.XiShu != null)
            {
                strSql.Append("XiShu=" + model.XiShu + ",");
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
            strSql.Append("delete from GuestProBaseInfo ");
            strSql.Append(" where Id=" + ID + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.BaseInfo.GuestProBaseInfo GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
           strSql.Append("select Id,GuestPro,JiLiXiShu,XiShu ");
			strSql.Append(" FROM GuestProBaseInfo ");
		 
            strSql.Append(" where ID=" + ID + "");

            VAN_OA.Model.BaseInfo.GuestProBaseInfo model = null;
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
        public List<VAN_OA.Model.BaseInfo.GuestProBaseInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,GuestPro,JiLiXiShu,XiShu ");
            strSql.Append(" FROM GuestProBaseInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by Id desc ");
            List<VAN_OA.Model.BaseInfo.GuestProBaseInfo> list = new List<VAN_OA.Model.BaseInfo.GuestProBaseInfo>();

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
        public VAN_OA.Model.BaseInfo.GuestProBaseInfo ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.BaseInfo.GuestProBaseInfo model = new VAN_OA.Model.BaseInfo.GuestProBaseInfo();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["GuestPro"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestPro = (int)ojb;
            }
            ojb = dataReader["JiLiXiShu"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.JiLiXiShu = (decimal)ojb;
            }
            ojb = dataReader["XiShu"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.XiShu = (int)ojb;
            }
            //if (model.GuestPro == 1)
            //{
            //    model.GuestProString = "";
            //}
            //else if (model.GuestPro == 0)
            //{ 
                
            //}
            return model;
        }
    }
}
