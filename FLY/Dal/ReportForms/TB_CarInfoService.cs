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
    public class TB_CarInfoService
    {/// <summary>
     /// 增加一条数据
     /// </summary>
        public int Add(VAN_OA.Model.ReportForms.TB_CarInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.CarNo != null)
            {
                strSql1.Append("CarNo,");
                strSql2.Append("'" + model.CarNo + "',");
            }
            if (model.Baoxian != null)
            {
                strSql1.Append("Baoxian,");
                strSql2.Append("'" + model.Baoxian + "',");
            }
            if (model.NianJian != null)
            {
                strSql1.Append("NianJian,");
                strSql2.Append("'" + model.NianJian + "',");
            }
            if (model.CarModel != null)
            {
                strSql1.Append("CarModel,");
                strSql2.Append("'" + model.CarModel + "',");
            }
            if (model.CarEngine != null)
            {
                strSql1.Append("CarEngine,");
                strSql2.Append("'" + model.CarEngine + "',");
            }
            if (model.CarJiaNo != null)
            {
                strSql1.Append("CarJiaNo,");
                strSql2.Append("'" + model.CarJiaNo + "',");
            }
            if (model.CaiXingShiNo != null)
            {
                strSql1.Append("CaiXingShiNo,");
                strSql2.Append("'" + model.CaiXingShiNo + "',");
            }
            if (model.CarShiBieNO != null)
            {
                strSql1.Append("CarShiBieNO,");
                strSql2.Append("'" + model.CarShiBieNO + "',");
            }
            if (model.OilNumber != null)
            {
                strSql1.Append("OilNumber,");
                strSql2.Append("" + model.OilNumber + ",");
            }
            strSql1.Append("IsStop,");
            strSql2.Append("" + (model.IsStop ? 1 : 0) + ",");
            strSql.Append("insert into TB_CarInfo(");
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
        public bool Update(VAN_OA.Model.ReportForms.TB_CarInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_CarInfo set ");
            //if (model.CarNo != null)
            //{
            //    strSql.Append("CarNo='" + model.CarNo + "',");
            //}
            if (model.Baoxian != null)
            {
                strSql.Append("Baoxian='" + model.Baoxian + "',");
            }
            else
            {
                strSql.Append("Baoxian= null ,");
            }
            if (model.NianJian != null)
            {
                strSql.Append("NianJian='" + model.NianJian + "',");
            }
            else
            {
                strSql.Append("NianJian= null ,");
            }

            if (!string.IsNullOrEmpty(model.CarModel))
            {
                strSql.Append("CarModel='" + model.CarModel + "',");
            }
            if (!string.IsNullOrEmpty(model.CarEngine))
            {
                strSql.Append("CarEngine='" + model.CarEngine + "',");
            }
            if (!string.IsNullOrEmpty(model.CarJiaNo))
            {
                strSql.Append("CarJiaNo='" + model.CarJiaNo + "',");
            }
            if (!string.IsNullOrEmpty(model.CaiXingShiNo))
            {
                strSql.Append("CaiXingShiNo='" + model.CaiXingShiNo + "',");
            }
            if (!string.IsNullOrEmpty(model.CarShiBieNO))
            {
                strSql.Append("CarShiBieNO='" + model.CarShiBieNO + "',");
            }
            strSql.Append("OilNumber='" + model.OilNumber + "',");
            strSql.Append("IsStop=" + (model.IsStop ? 1 : 0) + ",");
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where CarNo='" + model.CarNo + "'");
            return DBHelp.ExeCommand(strSql.ToString());
        }


        public bool UpdateDate(VAN_OA.Model.ReportForms.TB_CarInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_CarInfo set ");

            if (model.Baoxian != null)
            {
                strSql.Append("Baoxian='" + model.Baoxian + "',");
            }
            else
            {
                strSql.Append("Baoxian= null ,");
            }
            if (model.NianJian != null)
            {
                strSql.Append("NianJian='" + model.NianJian + "',");
            }
            else
            {
                strSql.Append("NianJian= null ,");
            }
            strSql.AppendFormat("IsStop= {0} ,", model.IsStop ? 1 : 0);

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where CarNo='" + model.CarNo + "'");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_CarInfo ");
            strSql.Append(" where id=" + id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.ReportForms.TB_CarInfo GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" IsStop,OilNumber,id,CarNo,Baoxian,NianJian,CarModel,CarEngine,CarJiaNo,CaiXingShiNo,CarShiBieNO ");
            strSql.Append(" from TB_CarInfo ");
            strSql.Append(" where id=" + id + "");

            VAN_OA.Model.ReportForms.TB_CarInfo model = null;
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
        public List<VAN_OA.Model.ReportForms.TB_CarInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select IsStop,OilNumber,id,CarNo,Baoxian,NianJian,CarModel,CarEngine,CarJiaNo,CaiXingShiNo,CarShiBieNO ");
            strSql.Append(" FROM TB_CarInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.ReportForms.TB_CarInfo> list = new List<VAN_OA.Model.ReportForms.TB_CarInfo>();

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
        public VAN_OA.Model.ReportForms.TB_CarInfo ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.ReportForms.TB_CarInfo model = new VAN_OA.Model.ReportForms.TB_CarInfo();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            model.CarNo = dataReader["CarNo"].ToString();
            ojb = dataReader["Baoxian"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Baoxian = (DateTime)ojb;
            }
            ojb = dataReader["NianJian"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.NianJian = (DateTime)ojb;
            }
            model.CarModel = dataReader["CarModel"].ToString();
            model.CarEngine = dataReader["CarEngine"].ToString();
            model.CarJiaNo = dataReader["CarJiaNo"].ToString();
            model.CaiXingShiNo = dataReader["CaiXingShiNo"].ToString();
            model.CarShiBieNO = dataReader["CarShiBieNO"].ToString();
            model.OilNumber = Convert.ToDecimal(dataReader["OilNumber"]);
            model.IsStop = Convert.ToBoolean(dataReader["IsStop"]);
            return model;
        }

    }
}
