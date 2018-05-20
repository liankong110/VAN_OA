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
    public class TB_POService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.ReportForms.TB_PO model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.DataTime != null)
            {
                strSql1.Append("DataTime,");
                strSql2.Append("'" + model.DataTime + "',");
            }
            if (model.UnitName != null)
            {
                strSql1.Append("UnitName,");
                strSql2.Append("'" + model.UnitName + "',");
            }
            if (model.InvName != null)
            {
                strSql1.Append("InvName,");
                strSql2.Append("'" + model.InvName + "',");
            }
            if (model.Unit != null)
            {
                strSql1.Append("Unit,");
                strSql2.Append("'" + model.Unit + "',");
            }
            if (model.Num != null)
            {
                strSql1.Append("Num,");
                strSql2.Append("" + model.Num + ",");
            }
            if (model.Price != null)
            {
                strSql1.Append("Price,");
                strSql2.Append("" + model.Price + ",");
            }
            if (model.Seller != null)
            {
                strSql1.Append("Seller,");
                strSql2.Append("'" + model.Seller + "',");
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
            strSql.Append("insert into TB_PO(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            object obj =DBHelp.ExeScalar(strSql.ToString());
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
        public bool Update(VAN_OA.Model.ReportForms.TB_PO model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_PO set ");
            if (model.DataTime != null)
            {
                strSql.Append("DataTime='" + model.DataTime + "',");
            }
            if (model.UnitName != null)
            {
                strSql.Append("UnitName='" + model.UnitName + "',");
            }
            if (model.InvName != null)
            {
                strSql.Append("InvName='" + model.InvName + "',");
            }
            if (model.Unit != null)
            {
                strSql.Append("Unit='" + model.Unit + "',");
            }
            if (model.Num != null)
            {
                strSql.Append("Num=" + model.Num + ",");
            }
            if (model.Price != null)
            {
                strSql.Append("Price=" + model.Price + ",");
            }
            if (model.Seller != null)
            {
                strSql.Append("Seller='" + model.Seller + "',");
            }
            //if (model.CreateTime != null)
            //{
            //    strSql.Append("CreateTime='" + model.CreateTime + "',");
            //}
            //if (model.CreateUser != null)
            //{
            //    strSql.Append("CreateUser=" + model.CreateUser + ",");
            //}
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            return  DBHelp.ExeCommand(strSql.ToString());
            
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_PO ");
             strSql.Append(" where Id=" + Id + "");
           return DBHelp.ExeCommand(strSql.ToString());           
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.ReportForms.TB_PO GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" TB_PO.Id,DataTime,UnitName,InvName,Unit,Num,Price,Seller,CreateTime,CreateUser,loginName,Num*Price as total ");
            strSql.Append(" from TB_PO left join tb_User on TB_PO.CreateUser=tb_User.ID");
            strSql.Append(" where TB_PO.Id=" + Id + "");
         
          
            VAN_OA.Model.ReportForms.TB_PO model = null;
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
        public List<VAN_OA.Model.ReportForms.TB_PO> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select TB_PO.Id,DataTime,UnitName,InvName,Unit,Num,Price,Seller,CreateTime,CreateUser,loginName,Num*Price as total");
            strSql.Append(" FROM TB_PO left join tb_User on TB_PO.CreateUser=tb_User.ID");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by DataTime desc");
           List<VAN_OA.Model.ReportForms.TB_PO> list = new List<VAN_OA.Model.ReportForms.TB_PO>();
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
        public VAN_OA.Model.ReportForms.TB_PO ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.ReportForms.TB_PO model = new VAN_OA.Model.ReportForms.TB_PO();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["DataTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DataTime = (DateTime)ojb;
            }
            model.UnitName = dataReader["UnitName"].ToString();
            model.InvName = dataReader["InvName"].ToString();
            model.Unit = dataReader["Unit"].ToString();
            ojb = dataReader["Num"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Num = (decimal)ojb;
            }
            ojb = dataReader["Price"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Price = (decimal)ojb;
            }
            model.Seller = dataReader["Seller"].ToString();
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

            ojb = dataReader["total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Total =Convert.ToDecimal( ojb.ToString());
            }
            return model;
        }

    }
}
