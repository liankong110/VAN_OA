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
using VAN_OA.Model.EFrom;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;


namespace VAN_OA.Dal.EFrom
{
    public class tb_ConsignorService
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddSome(tb_Consignor model, List<int> allProIds)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {



                    objCommand.Parameters.Clear();
                    for (int i = 0; i < allProIds.Count; i++)
                    {
                        model.proId = allProIds[i];
                        Add(model, objCommand);
                    }



                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return 0;

                }
                return 1;
            }



          
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.EFrom.tb_Consignor model,SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.consignor != null)
            {
                strSql1.Append("consignor,");
                strSql2.Append("" + model.consignor + ",");
            }
            if (model.appPer != null)
            {
                strSql1.Append("appPer,");
                strSql2.Append("" + model.appPer + ",");
            }
            if (model.fromTime != null)
            {
                strSql1.Append("fromTime,");
                strSql2.Append("'" + model.fromTime + "',");
            }
            if (model.toTime != null)
            {
                strSql1.Append("toTime,");
                strSql2.Append("'" + model.toTime + "',");
            }
            if (model.ifYouXiao != null)
            {
                strSql1.Append("ifYouXiao,");
                strSql2.Append("" + (model.ifYouXiao ? 1 : 0) + ",");
            }
            if (model.conState != null)
            {
                strSql1.Append("conState,");
                strSql2.Append("'" + model.conState + "',");
            }
            if (model.proId != null)
            {
                strSql1.Append("proId,");
                strSql2.Append("" + model.proId + ",");
            }
            strSql.Append("insert into tb_Consignor(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");
            objCommand.CommandText = strSql.ToString();
            object obj = objCommand.ExecuteScalar();
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
        public int Add(VAN_OA.Model.EFrom.tb_Consignor model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.consignor != null)
            {
                strSql1.Append("consignor,");
                strSql2.Append("" + model.consignor + ",");
            }
            if (model.appPer != null)
            {
                strSql1.Append("appPer,");
                strSql2.Append("" + model.appPer + ",");
            }
            if (model.fromTime != null)
            {
                strSql1.Append("fromTime,");
                strSql2.Append("'" + model.fromTime + "',");
            }
            if (model.toTime != null)
            {
                strSql1.Append("toTime,");
                strSql2.Append("'" + model.toTime + "',");
            }
            if (model.ifYouXiao != null)
            {
                strSql1.Append("ifYouXiao,");
                strSql2.Append("" + (model.ifYouXiao ? 1 : 0) + ",");
            }
            if (model.conState != null)
            {
                strSql1.Append("conState,");
                strSql2.Append("'" + model.conState + "',");
            }
            if (model.proId != null)
            {
                strSql1.Append("proId,");
                strSql2.Append("" + model.proId + ",");
            }
            strSql.Append("insert into tb_Consignor(");
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
        public bool Update(VAN_OA.Model.EFrom.tb_Consignor model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Consignor set ");
            if (model.consignor != null)
            {
                strSql.Append("consignor=" + model.consignor + ",");
            }
            if (model.appPer != null)
            {
                strSql.Append("appPer=" + model.appPer + ",");
            }
            if (model.fromTime != null)
            {
                strSql.Append("fromTime='" + model.fromTime + "',");
            }
            else
            {
                strSql.Append("fromTime= null ,");
            }
            if (model.toTime != null)
            {
                strSql.Append("toTime='" + model.toTime + "',");
            }
            else
            {
                strSql.Append("toTime= null ,");
            }
            if (model.ifYouXiao != null)
            {
                strSql.Append("ifYouXiao=" + (model.ifYouXiao ? 1 : 0) + ",");
            }
            if (model.conState != null)
            {
                strSql.Append("conState='" + model.conState + "',");
            }
            if (model.proId != null)
            {
                strSql.Append("proId=" + model.proId + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where con_Id=" + model.con_Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
           
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int con_Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_Consignor ");
            strSql.Append(" where con_Id=" + con_Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_Consignor> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select con_Id,consignor,appPer,fromTime,toTime,ifYouXiao,conState,tb_Consignor_View.proId,pro_Type,consignor_Name,appPer_Name ");
            strSql.Append(" FROM tb_Consignor_View left join  A_ProInfo on A_ProInfo.pro_Id=tb_Consignor_View.proId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.tb_Consignor> list = new List<VAN_OA.Model.EFrom.tb_Consignor>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind(dataReader));
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_Consignor> GetListArray_BeiWeiTou(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select con_Id,consignor,appPer,fromTime,toTime,ifYouXiao,conState,tb_Consignor_View.proId,pro_Type,consignor_Name,appPer_Name  ");
            strSql.Append(" FROM tb_Consignor_View left join  A_ProInfo on A_ProInfo.pro_Id=tb_Consignor_View.proId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.tb_Consignor> list = new List<VAN_OA.Model.EFrom.tb_Consignor>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind(dataReader));
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.EFrom.tb_Consignor ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_Consignor model = new VAN_OA.Model.EFrom.tb_Consignor();
            object ojb;
            ojb = dataReader["con_Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.con_Id = (int)ojb;
            }
            model.consignor =Convert.ToInt32(dataReader["consignor"]);
            ojb = dataReader["consignor_Name"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Consignor_Name = ojb.ToString();
            }
            ojb = dataReader["appPer_Name"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Appper_Name = ojb.ToString();
            }
            model.appPer =Convert.ToInt32( dataReader["appPer"]);
            ojb = dataReader["fromTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.fromTime = (DateTime)ojb;
            }
            ojb = dataReader["toTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.toTime = (DateTime)ojb;
            }
            ojb = dataReader["ifYouXiao"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ifYouXiao = (bool)ojb;
            }
            model.conState = dataReader["conState"].ToString();
            ojb = dataReader["proId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.proId = (int)ojb;
            }

            model.ProType = dataReader["pro_Type"].ToString();

            if (model.ifYouXiao == true)
            {
                model.Youxiaoqi = "一直有效";
            }
            else
            {
                if (model.fromTime != null && model.toTime != null)
                {
                    model.Youxiaoqi = Convert.ToDateTime(model.fromTime).ToShortDateString() + "--" + Convert.ToDateTime(model.toTime).ToShortDateString();
                }
                else if (model.fromTime != null && model.toTime == null)
                {
                    model.Youxiaoqi ="生效日期："+ Convert.ToDateTime(model.fromTime).ToShortDateString() ;
                }
                else if (model.fromTime == null && model.toTime != null)
                {
                    model.Youxiaoqi = "终止日期：" + Convert.ToDateTime(model.toTime).ToShortDateString();
                }
            }
            return model;
        }



    }
}
