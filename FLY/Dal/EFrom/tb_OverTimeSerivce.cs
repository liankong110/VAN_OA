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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using VAN_OA.Model.EFrom;

namespace VAN_OA.Dal.EFrom
{
    public class tb_OverTimeSerivce
    {
        public bool updateTran(VAN_OA.Model.EFrom.tb_OverTime model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
        public int addTran(VAN_OA.Model.EFrom.tb_OverTime model, VAN_OA.Model.EFrom.tb_EForm eform)
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



                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();

                    string proNo = eformSer.GetAllE_No("tb_OverTime", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;
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
        public int Add(VAN_OA.Model.EFrom.tb_OverTime model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.appUseId != null)
            {
                strSql1.Append("appUseId,");
                strSql2.Append("" + model.appUseId + ",");
            }
            if (model.reason != null)
            {
                strSql1.Append("reason,");
                strSql2.Append("'" + model.reason + "',");
            }
            if (model.formTime != null)
            {
                strSql1.Append("formTime,");
                strSql2.Append("'" + model.formTime + "',");
            }
            if (model.toTime != null)
            {
                strSql1.Append("toTime,");
                strSql2.Append("'" + model.toTime + "',");
            }
            if (model.guestDai != null)
            {
                strSql1.Append("guestDai,");
                strSql2.Append("'" + model.guestDai + "',");
            }
            if (model.Address != null)
            {
                strSql1.Append("Address,");
                strSql2.Append("'" + model.Address + "',");
            }
            if (model.SuixingRen != null)
            {
                strSql1.Append("SuixingRen,");
                strSql2.Append("'" + model.SuixingRen + "',");
            }
            if (model.Reamrk != null)
            {
                strSql1.Append("Reamrk,");
                strSql2.Append("'" + model.Reamrk + "',");
            }
            if (model.Time != null)
            {
                strSql1.Append("Time,");
                strSql2.Append("'" + model.Time + "',");
            }
            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }

            if (model.OverTimeType != null)
            {
                strSql1.Append("OverTimeType,");
                strSql2.Append("'" + model.OverTimeType + "',");
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
            if (model.State != null)
            {
                strSql1.Append("State,");
                strSql2.Append("'" + model.State + "',");
            }
            strSql.Append("insert into tb_OverTime(");
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
        public void Update(VAN_OA.Model.EFrom.tb_OverTime model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_OverTime set ");
            if (model.appUseId != null)
            {
                strSql.Append("appUseId=" + model.appUseId + ",");
            }
            if (model.reason != null)
            {
                strSql.Append("reason='" + model.reason + "',");
            }
            else
            {
                strSql.Append("reason= null ,");
            }
            if (model.formTime != null)
            {
                strSql.Append("formTime='" + model.formTime + "',");
            }
            if (model.toTime != null)
            {
                strSql.Append("toTime='" + model.toTime + "',");
            }
            if (model.guestDai != null)
            {
                strSql.Append("guestDai='" + model.guestDai + "',");
            }
            else
            {
                strSql.Append("guestDai= null ,");
            }


            if (model.Address != null)
            {
                strSql.Append("Address='" + model.Address + "',");
            }
            else
            {
                strSql.Append("Address= null ,");
            }
            if (model.SuixingRen != null)
            {
                strSql.Append("SuixingRen='" + model.SuixingRen + "',");
            }
            else
            {
                strSql.Append("SuixingRen= null ,");
            }
            if (model.Reamrk != null)
            {
                strSql.Append("Reamrk='" + model.Reamrk + "',");
            }
            else
            {
                strSql.Append("Reamrk= null ,");
            }
            if (model.Time != null)
            {
                strSql.Append("Time='" + model.Time + "',");
            }
            else
            {
                strSql.Append("Time= null ,");
            }



            if (model.Total != null)
            {
                strSql.Append("Total=" + model.Total + ",");
            }
            else
            {
                strSql.Append("Total= null ,");
            }

            if (model.OverTimeType != null)
            {
                strSql.Append("OverTimeType='" + model.OverTimeType + "',");
            }
            else
            {
                strSql.Append("OverTimeType= null ,");
            }
            if (model.State != null)
            {
                strSql.Append("State='" + model.State + "',");
            }
            else
            {
                strSql.Append("State= null ,");
            }

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where id=" + model.id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_OverTime ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_OverTime GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" tb_OverTime.id,appUseId,reason,formTime,toTime,guestDai,loginName,loginIPosition,Address,SuixingRen,Reamrk,Time,proNo,Total,OverTimeType,POGuestName,PONo,POName,state ");
            strSql.Append(" from tb_OverTime left join tb_User on tb_User.id=tb_OverTime.appUseId");
            strSql.Append(" where tb_OverTime.id=" + id + "");

            VAN_OA.Model.EFrom.tb_OverTime model = null;
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
        public List<VAN_OA.Model.EFrom.tb_OverTime> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append("tb_OverTime.id,appUseId,reason,formTime,toTime,guestDai,loginName,loginIPosition,Address,SuixingRen,Reamrk,Time,tb_OverTime.proNo,Total,OverTimeType,POGuestName,tb_OverTime.PONo,tb_OverTime.POName,state ");
            strSql.Append(" from tb_OverTime left join tb_User on tb_User.id=tb_OverTime.appUseId");
            strSql.Append("  left join CG_POOrder on CG_POOrder.PONo=tb_OverTime.PONo AND IFZhui=0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append("order by formTime  desc");
            List<VAN_OA.Model.EFrom.tb_OverTime> list = new List<VAN_OA.Model.EFrom.tb_OverTime>();

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
        public VAN_OA.Model.EFrom.tb_OverTime ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_OverTime model = new VAN_OA.Model.EFrom.tb_OverTime();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            ojb = dataReader["appUseId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.appUseId = (int)ojb;
            }
            model.reason = dataReader["reason"].ToString();
            ojb = dataReader["formTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.formTime = (DateTime)ojb;
            }
            ojb = dataReader["toTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.toTime = (DateTime)ojb;
            }
            model.guestDai = dataReader["guestDai"].ToString();

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LoginName = ojb.ToString();
            }

            ojb = dataReader["OverTimeType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OverTimeType = ojb.ToString();
            }


            ojb = dataReader["loginIPosition"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DepartName = ojb.ToString();
            }


            model.Address = dataReader["Address"].ToString();
            model.SuixingRen = dataReader["SuixingRen"].ToString();
            model.Reamrk = dataReader["Reamrk"].ToString();
            ojb = dataReader["Time"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Time = (DateTime)ojb;
            }

            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Total =Convert.ToDecimal(ojb);
            }

            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }
            ojb = dataReader["PONo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PONo = ojb.ToString();
            }

            ojb = dataReader["POGuestName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POGuestName = ojb.ToString();
            }

            ojb = dataReader["POName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POName = ojb.ToString();
            }
           

            ojb = dataReader["State"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.State = ojb.ToString();
            }

            model.BetweenTime = model.formTime.ToShortTimeString()+"~"+model.toTime.ToShortTimeString();
            TimeSpan ts = Convert.ToDateTime(model.toTime) - Convert.ToDateTime(model.formTime);

            try
            {
                ////首先判断请假的天数
                //TimeSpan ts = Convert.ToDateTime(model.toTime) - Convert.ToDateTime(model.formTime);


                //DateTime fromTime = Convert.ToDateTime(model.formTime);
                //DateTime toTime = Convert.ToDateTime(model.toTime);

                //double lastfromTime = fromTime.TimeOfDay.Hours + Convert.ToDouble(fromTime.TimeOfDay.Minutes) / Convert.ToDouble(60);
                //double lasttoTime = toTime.TimeOfDay.Hours + Convert.ToDouble(toTime.TimeOfDay.Minutes) / Convert.ToDouble(60);


                //int days = ts.Days;
                //double hours = 0;
                //if ((ts.Hours + Convert.ToDouble(ts.Minutes) / Convert.ToDouble(60)) >= 8.25)
                //{
                //    days += 1;
                //}
                //else
                //{
                //    //计算小时数
                //    if ((lastfromTime >= 9 && lastfromTime <= 12) &&
                //        (lasttoTime >= 13.5 && lasttoTime <= 17.25))
                //    {
                //        hours = lasttoTime - lastfromTime - 1.5;
                //    }
                //    else if ((lastfromTime >= 9 && lastfromTime <= 12) &&
                //        (lasttoTime >= 9 && lasttoTime <= 12))
                //    {
                //        hours = lasttoTime - lastfromTime;
                //    }

                //    else if ((lastfromTime >= 13.5 && lastfromTime <= 17.25) &&
                //        (lasttoTime >= 13.5 && lasttoTime <= 17.25))
                //    {
                //        hours = lasttoTime - lastfromTime;
                //    }
                //    else
                //    {

                //    }

                //}

                model.BetweenHours = Convert.ToDecimal(string.Format("{0:N2}", ts.Days * 24 + ts.Hours + Convert.ToDecimal(ts.Minutes) / 60));
                //double HHH = days * 6.75 + hours;
                //model.BetweenHours = Convert.ToDecimal(string.Format("{0:N2}", HHH));
            }
            catch (Exception)
            {
                
                 
            }
            return model;
        }



    }
}
