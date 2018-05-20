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
    public class tb_UseCarService
    {
        public bool updateTran(VAN_OA.Model.EFrom.tb_UseCar model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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

                    model.State = eform.state;
                    Update(model, objCommand);


                    tb_EFormService eformSer = new tb_EFormService();

                    eformSer.Update(eform, objCommand);


                    tb_EFormsService eformsSer = new tb_EFormsService();
                    eformsSer.Add(forms, objCommand);

                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return false;

                }

            }

            return true;
        }
        public int addTran(VAN_OA.Model.EFrom.tb_UseCar model, VAN_OA.Model.EFrom.tb_EForm eform)
        {
            int id = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {


                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("tb_UseCar", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;
                    objCommand.Parameters.Clear();
                    model.State = eform.state;
                    id = Add(model, objCommand);

                   
                    eform.allE_id = id;
                    eformSer.Add(eform, objCommand);

                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return 0;

                }
                return id;
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.EFrom.tb_UseCar model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.appName != null)
            {
                strSql1.Append("appName,");
                strSql2.Append("" + model.appName + ",");
            }
            if (model.datetime != null)
            {
                strSql1.Append("datetime,");
                strSql2.Append("'" + model.datetime + "',");
            }
            if (model.pers_car != null)
            {
                strSql1.Append("pers_car,");
                strSql2.Append("'" + model.pers_car + "',");
            }
            if (model.type != null)
            {
                strSql1.Append("type,");
                strSql2.Append("'" + model.type + "',");
            }
            if (model.useReason != null)
            {
                strSql1.Append("useReason,");
                strSql2.Append("'" + model.useReason + "',");
            }
            if (model.roadLong != null)
            {
                strSql1.Append("roadLong,");
                strSql2.Append("" + model.roadLong + ",");
            }
            if (model.deAddress != null)
            {
                strSql1.Append("deAddress,");
                strSql2.Append("'" + model.deAddress + "',");
            }
            if (model.goAddress != null)
            {
                strSql1.Append("goAddress,");
                strSql2.Append("'" + model.goAddress + "',");
            }
            if (model.toAddress != null)
            {
                strSql1.Append("toAddress,");
                strSql2.Append("'" + model.toAddress + "',");
            }
            if (model.goTime != null)
            {
                strSql1.Append("goTime,");
                strSql2.Append("'" + model.goTime + "',");
            }
            if (model.endTime != null)
            {
                strSql1.Append("endTime,");
                strSql2.Append("'" + model.endTime + "',");
            }
            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }
            if (model.POGuestName != null)
            {
                strSql1.Append("POGuestName,");
                strSql2.Append("'" + model.POGuestName + "',");
            }
            if (model.PONo != null)
            {
                strSql1.Append("PONo,");
                strSql2.Append("'" + model.PONo + "',");
            }
            if (model.POName != null)
            {
                strSql1.Append("POName,");
                strSql2.Append("'" + model.POName + "',");
            }

            if (model.OilPrice != null)
            {
                strSql1.Append("OilPrice,");
                strSql2.Append("" + model.OilPrice + ",");
            }

            if (model.State != null)
            {
                strSql1.Append("State,");
                strSql2.Append("'" + model.State + "',");
            }
            strSql.Append("insert into tb_UseCar(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            strSql.Append(";select @@IDENTITY");


            int result;
            objCommand.CommandText = strSql.ToString();
            object obj = objCommand.ExecuteScalar();
            if (!int.TryParse(obj.ToString(), out result))
            {
                return 0;
            }
            return result;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(VAN_OA.Model.EFrom.tb_UseCar model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_UseCar set ");
            //if (model.appName != null)
            //{
            //    strSql.Append("appName='" + model.appName + "',");
            //}
            //if (model.datetime != null)
            //{
            //    strSql.Append("datetime='" + model.datetime + "',");
            //}
            if (model.pers_car != null)
            {
                strSql.Append("pers_car='" + model.pers_car + "',");
            }
            else
            {
                strSql.Append("pers_car= null ,");
            }
            if (model.type != null)
            {
                strSql.Append("type='" + model.type + "',");
            }
            if (model.useReason != null)
            {
                strSql.Append("useReason='" + model.useReason + "',");
            }
            else
            {
                strSql.Append("useReason= null ,");
            }
            if (model.roadLong != null)
            {
                strSql.Append("roadLong=" + model.roadLong + ",");
            }
            else
            {
                strSql.Append("roadLong= null ,");
            }
            if (model.deAddress != null)
            {
                strSql.Append("deAddress='" + model.deAddress + "',");
            }
            else
            {
                strSql.Append("deAddress= null ,");
            }
            if (model.goAddress != null)
            {
                strSql.Append("goAddress='" + model.goAddress + "',");
            }
            else
            {
                strSql.Append("goAddress= null ,");
            }
            if (model.toAddress != null)
            {
                strSql.Append("toAddress='" + model.toAddress + "',");
            }
            else
            {
                strSql.Append("toAddress= null ,");
            }
            if (model.goTime != null)
            {
                strSql.Append("goTime='" + model.goTime + "',");
            }
            else
            {
                strSql.Append("goTime= null ,");
            }
            if (model.endTime != null)
            {
                strSql.Append("endTime='" + model.endTime + "',");
            }
            else
            {
                strSql.Append("endTime= null ,");
            }

            //if (model.POGuestName != null)
            //{
            //    strSql.Append("POGuestName='" + model.POGuestName + "',");
            //}
            //else
            //{
            //    strSql.Append("POGuestName= null ,");
            //}
            //if (model.PONo != null)
            //{
            //    strSql.Append("PONo='" + model.PONo + "',");
            //}
            //else
            //{
            //    strSql.Append("PONo= null ,");
            //}
            //if (model.POName != null)
            //{
            //    strSql.Append("POName='" + model.POName + "',");
            //}
            //else
            //{
            //    strSql.Append("POName= null ,");
            //}

            if (model.State != null)
            {
                strSql.Append("State='" + model.State + "',");
            }
            else
            {
                strSql.Append("State= null ,");
            }


            if (model.OilPrice != null)
            {
                strSql.Append("OilPrice=" + model.OilPrice + ",");
            }
            else
            {
                strSql.Append("OilPrice= null ,");
            }

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where id=" + model.id + "");

         
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(VAN_OA.Model.EFrom.tb_UseCar model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_UseCar set ");
            //if (model.appName != null)
            //{
            //    strSql.Append("appName='" + model.appName + "',");
            //}
            //if (model.datetime != null)
            //{
            //    strSql.Append("datetime='" + model.datetime + "',");
            //}
            if (model.pers_car != null)
            {
                strSql.Append("pers_car='" + model.pers_car + "',");
            }
            else
            {
                strSql.Append("pers_car= null ,");
            }
            if (model.type != null)
            {
                strSql.Append("type='" + model.type + "',");
            }
            if (model.useReason != null)
            {
                strSql.Append("useReason='" + model.useReason + "',");
            }
            else
            {
                strSql.Append("useReason= null ,");
            }
            if (model.roadLong != null)
            {
                strSql.Append("roadLong=" + model.roadLong + ",");
            }
            else
            {
                strSql.Append("roadLong= null ,");
            }
            if (model.deAddress != null)
            {
                strSql.Append("deAddress='" + model.deAddress + "',");
            }
            else
            {
                strSql.Append("deAddress= null ,");
            }
            if (model.goAddress != null)
            {
                strSql.Append("goAddress='" + model.goAddress + "',");
            }
            else
            {
                strSql.Append("goAddress= null ,");
            }
            if (model.toAddress != null)
            {
                strSql.Append("toAddress='" + model.toAddress + "',");
            }
            else
            {
                strSql.Append("toAddress= null ,");
            }
            if (model.goTime != null)
            {
                strSql.Append("goTime='" + model.goTime + "',");
            }
            else
            {
                strSql.Append("goTime= null ,");
            }
            if (model.endTime != null)
            {
                strSql.Append("endTime='" + model.endTime + "',");
            }
            else
            {
                strSql.Append("endTime= null ,");
            }


            //if (model.POGuestName != null)
            //{
            //    strSql.Append("POGuestName='" + model.POGuestName + "',");
            //}
            //else
            //{
            //    strSql.Append("POGuestName= null ,");
            //}
            //if (model.PONo != null)
            //{
            //    strSql.Append("PONo='" + model.PONo + "',");
            //}
            //else
            //{
            //    strSql.Append("PONo= null ,");
            //}
            //if (model.POName != null)
            //{
            //    strSql.Append("POName='" + model.POName + "',");
            //}
            //else
            //{
            //    strSql.Append("POName= null ,");
            //}
          

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where id=" + model.id + "");


            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_UseCar ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_UseCar GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" tb_UseCar.id,appName,datetime,pers_car,type,useReason,roadLong,deAddress,goAddress,toAddress,goTime,endTime,loginName ,proNo,POGuestName,PONo,POName,OilPrice,state");
            strSql.Append(" from tb_UseCar left join tb_User on tb_UseCar.appName=tb_User.Id");
            strSql.Append(" where tb_UseCar.id=" + id + "");
            VAN_OA.Model.EFrom.tb_UseCar model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind(dataReader);
                    }
                }
            }
            return model;
        }




        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.tb_UseCar> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select tb_UseCar.id,appName,datetime,pers_car,type,useReason,roadLong,deAddress,goAddress,toAddress,goTime,endTime,loginName,proNo,POGuestName,PONo,POName,OilPrice,state ");
            strSql.Append(" FROM tb_UseCar  left join tb_User on tb_UseCar.appName=tb_User.Id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.tb_UseCar> list = new List<VAN_OA.Model.EFrom.tb_UseCar>();

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
        public List<VAN_OA.Model.EFrom.tb_UseCar> GetListArray_Req(string strWhere, out decimal Total, out decimal Total1)
        {
            Total = 0;
            Total1 = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AE,tb_UseCar.id,appName,datetime,pers_car,type,useReason,roadLong,deAddress,goAddress,toAddress,goTime,endTime,loginName,proNo,POGuestName,PONo,POName,OilPrice,state");
            strSql.Append(" FROM tb_UseCar  left join tb_User on tb_UseCar.appName=tb_User.Id ");
            strSql.Append("  LEFT JOIN ( SELECT PONo AS T_PONO,AE FROM CG_POOrder where IFZhui=0 ) AS T_POOrder on T_POOrder.T_PONO=tb_UseCar.PONo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by datetime desc");
            List<VAN_OA.Model.EFrom.tb_UseCar> list = new List<VAN_OA.Model.EFrom.tb_UseCar>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        tb_UseCar model=ReaderBind(dataReader);
                        object ojb;
                        ojb = dataReader["AE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AE = ojb.ToString();
                        }
                        if (model.roadLong!=null)
                        Total += model.roadLong.Value;
                        model.TotalPrice= model.roadLong.Value*model.OilPrice;

                        Total1 += model.TotalPrice;
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.EFrom.tb_UseCar ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_UseCar model = new VAN_OA.Model.EFrom.tb_UseCar();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            model.appName =Convert.ToInt32( dataReader["appName"].ToString());
            model.datetime =Convert.ToDateTime( dataReader["datetime"].ToString());
            model.pers_car = dataReader["pers_car"].ToString();
            model.type = dataReader["type"].ToString();
            model.useReason = dataReader["useReason"].ToString();
            ojb = dataReader["roadLong"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.roadLong = (decimal)ojb;
            }
            model.deAddress = dataReader["deAddress"].ToString();
            model.goAddress = dataReader["goAddress"].ToString();
            model.toAddress = dataReader["toAddress"].ToString();
            ojb = dataReader["goTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.goTime = (DateTime)ojb;
                model.GoEndTime = model.goTime.Value.ToShortDateString()+" " + model.goTime.Value.ToShortTimeString() + "~";
            }
            ojb = dataReader["endTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.endTime = (DateTime)ojb;
                model.GoEndTime += model.endTime.Value.ToShortDateString() + " " + model.endTime.Value.ToShortTimeString();
            }

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LoginName =ojb.ToString();
            }
            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }
            ojb = dataReader["POGuestName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POGuestName = ojb.ToString();
            }

            ojb = dataReader["PONo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PONo = ojb.ToString();
            }

            ojb = dataReader["POName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POName = ojb.ToString();
            }
            ojb = dataReader["OilPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OilPrice =Convert.ToDecimal(ojb);
            }

            ojb = dataReader["State"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.State = ojb.ToString();
            }
            
            
            return model;
        }



    }
}
