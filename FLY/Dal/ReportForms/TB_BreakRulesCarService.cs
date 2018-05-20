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
    public class TB_BreakRulesCarService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.ReportForms.TB_BreakRulesCar model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.JiGuan != null)
            {
                strSql1.Append("JiGuan,");
                strSql2.Append("'" + model.JiGuan + "',");
            }
            if (model.BreakTime != null)
            {
                strSql1.Append("BreakTime,");
                strSql2.Append("'" + model.BreakTime + "',");
            }
            if (model.Address != null)
            {
                strSql1.Append("Address,");
                strSql2.Append("'" + model.Address + "',");
            }
            if (model.Dothing != null)
            {
                strSql1.Append("Dothing,");
                strSql2.Append("'" + model.Dothing + "',");
            }
            if (model.Total != null)
            {
                strSql1.Append("Total,");
                strSql2.Append("" + model.Total + ",");
            }
            if (model.State != null)
            {
                strSql1.Append("State,");
                strSql2.Append("'" + model.State + "',");
            }
            if (model.CarNo != null)
            {
                strSql1.Append("CarNo,");
                strSql2.Append("'" + model.CarNo + "',");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }

            if (model.Driver != null)
            {
                strSql1.Append("Driver,");
                strSql2.Append("'" + model.Driver + "',");
            }
            strSql1.Append("Score,");
            strSql2.Append("" + model.Score + ",");
            
            strSql.Append("insert into TB_BreakRulesCar(");
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
        public bool Update(VAN_OA.Model.ReportForms.TB_BreakRulesCar model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_BreakRulesCar set ");
            if (model.JiGuan != null)
            {
                strSql.Append("JiGuan='" + model.JiGuan + "',");
            }
            if (model.BreakTime != null)
            {
                strSql.Append("BreakTime='" + model.BreakTime + "',");
            }
            if (model.Address != null)
            {
                strSql.Append("Address='" + model.Address + "',");
            }
            if (model.Dothing != null)
            {
                strSql.Append("Dothing='" + model.Dothing + "',");
            }
            if (model.Total != null)
            {
                strSql.Append("Total=" + model.Total + ",");
            }
            if (model.State != null)
            {
                strSql.Append("State='" + model.State + "',");
            }
            if (model.CarNo != null)
            {
                strSql.Append("CarNo='" + model.CarNo + "',");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }

            if (model.Driver != null)
            {
                strSql.Append("Driver='" + model.Driver + "',");
            }
            else
            {
                strSql.Append("Driver= null ,");
            }
            strSql.Append("Score=" + model.Score + ",");

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
            strSql.Append("delete from TB_BreakRulesCar ");
            strSql.Append(" where Id=" + Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.ReportForms.TB_BreakRulesCar GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Id,JiGuan,BreakTime,Address,Dothing,Total,State,CarNo,Remark ,Driver,Score ");
            strSql.Append(" from TB_BreakRulesCar ");
            strSql.Append(" where Id=" + Id + "");
          
            VAN_OA.Model.ReportForms.TB_BreakRulesCar model = null;
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
        public List<VAN_OA.Model.ReportForms.TB_BreakRulesCar> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,JiGuan,BreakTime,Address,Dothing,Total,State,CarNo,Remark,Driver,Score  ");
            strSql.Append(" FROM TB_BreakRulesCar ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by  BreakTime desc" );
            List<VAN_OA.Model.ReportForms.TB_BreakRulesCar> list = new List<VAN_OA.Model.ReportForms.TB_BreakRulesCar>();
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
        public VAN_OA.Model.ReportForms.TB_BreakRulesCar ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.ReportForms.TB_BreakRulesCar model = new VAN_OA.Model.ReportForms.TB_BreakRulesCar();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.JiGuan = dataReader["JiGuan"].ToString();
            ojb = dataReader["BreakTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BreakTime = (DateTime)ojb;
            }
            model.Address = dataReader["Address"].ToString();
            model.Dothing = dataReader["Dothing"].ToString();
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Total = (decimal)ojb;
            }
            model.State = dataReader["State"].ToString();
            model.CarNo = dataReader["CarNo"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            model.Driver = dataReader["Driver"].ToString();
            model.Score =Convert.ToInt32(dataReader["Score"]);
            return model;
        }

    }
}
