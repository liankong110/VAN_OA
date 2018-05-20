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
    public class tb_GusetInfoService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.ReportForms.tb_GusetInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.ZhuTel != null)
            {
                strSql1.Append("ZhuTel,");
                strSql2.Append("'" + model.ZhuTel + "',");
            }
            if (model.LinkMan != null)
            {
                strSql1.Append("LinkMan,");
                strSql2.Append("'" + model.LinkMan + "',");
            }
            if (model.tel1 != null)
            {
                strSql1.Append("tel1,");
                strSql2.Append("'" + model.tel1 + "',");
            }
            if (model.tel2 != null)
            {
                strSql1.Append("tel2,");
                strSql2.Append("'" + model.tel2 + "',");
            }
            if (model.Account != null)
            {
                strSql1.Append("Account,");
                strSql2.Append("" + model.Account + ",");
            }
            if (model.CreateTime != null)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("'" + model.CreateTime + "',");
            }
            if (model.CreateUser != null)
            {
                strSql1.Append("CreateUser,");
                strSql2.Append("" + model.CreateUser + ",");
            }
            strSql.Append("insert into tb_GusetInfo(");
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
        public bool Update(VAN_OA.Model.ReportForms.tb_GusetInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_GusetInfo set ");
            if (model.GuestName != null)
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            if (model.ZhuTel != null)
            {
                strSql.Append("ZhuTel='" + model.ZhuTel + "',");
            }
            else
            {
                strSql.Append("ZhuTel= null ,");
            }
            if (model.LinkMan != null)
            {
                strSql.Append("LinkMan='" + model.LinkMan + "',");
            }
            if (model.tel1 != null)
            {
                strSql.Append("tel1='" + model.tel1 + "',");
            }
            else
            {
                strSql.Append("tel1= null ,");
            }
            if (model.tel2 != null)
            {
                strSql.Append("tel2='" + model.tel2 + "',");
            }
            else
            {
                strSql.Append("tel2= null ,");
            }
            if (model.Account != null)
            {
                strSql.Append("Account=" + model.Account + ",");
            }
            else
            {
                strSql.Append("AccountTime= null ,");
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
            strSql.Append("delete from tb_GusetInfo ");
            strSql.Append(" where Id=" + Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.ReportForms.tb_GusetInfo GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" tb_GusetInfo.Id,GuestName,ZhuTel,LinkMan,tel1,tel2,Account,CreateTime,CreateUser,loginName ");
            strSql.Append(" from tb_GusetInfo left join tb_User on tb_GusetInfo.CreateUser=tb_User.ID");
            strSql.Append(" where tb_GusetInfo.Id=" + Id + "");
           
            VAN_OA.Model.ReportForms.tb_GusetInfo model = null;
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
        public List<VAN_OA.Model.ReportForms.tb_GusetInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select tb_GusetInfo.Id,GuestName,ZhuTel,LinkMan,tel1,tel2,Account,CreateTime,CreateUser,loginName ");
            strSql.Append(" FROM tb_GusetInfo left join tb_User on tb_GusetInfo.CreateUser=tb_User.ID");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by CreateTime desc ");
            List<VAN_OA.Model.ReportForms.tb_GusetInfo> list = new List<VAN_OA.Model.ReportForms.tb_GusetInfo>();
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
        public VAN_OA.Model.ReportForms.tb_GusetInfo ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.ReportForms.tb_GusetInfo model = new VAN_OA.Model.ReportForms.tb_GusetInfo();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.GuestName = dataReader["GuestName"].ToString();
            model.ZhuTel = dataReader["ZhuTel"].ToString();
            model.LinkMan = dataReader["LinkMan"].ToString();
            model.tel1 = dataReader["tel1"].ToString();
            model.tel2 = dataReader["tel2"].ToString();
            ojb = dataReader["Account"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Account =Convert.ToDecimal( ojb);
            }

            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }
            ojb = dataReader["CreateUser"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUser = (int)ojb;
            }

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUser_Name = ojb.ToString();
            }
            return model;
        }

    }
}
