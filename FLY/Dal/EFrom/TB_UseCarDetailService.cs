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
    public class TB_UseCarDetailService
    {
        public bool updateTran(VAN_OA.Model.EFrom.TB_UseCarDetail model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {
                    model.State = eform.state;
                    objCommand.Parameters.Clear();
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


        public bool updateTran(VAN_OA.Model.EFrom.TB_UseCarDetail model, VAN_OA.Model.EFrom.tb_EForm eform)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {
                    model.State = eform.state;
                    objCommand.Parameters.Clear();
                    Update(model, objCommand);
                    string updateEform = string.Format("update  tb_EForm set toPer={0} where id={1}", eform.toPer, eform.id);
                    objCommand.CommandText = updateEform;
                    objCommand.ExecuteNonQuery();

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
        public int addTran(VAN_OA.Model.EFrom.TB_UseCarDetail model, VAN_OA.Model.EFrom.tb_EForm eform)
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

                    model.State = eform.state;
                    tb_EFormService eformSer = new tb_EFormService();
                    objCommand.Parameters.Clear();
                    string proNo = eformSer.GetAllE_No("TB_UseCarDetail", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;

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
        public int Add(VAN_OA.Model.EFrom.TB_UseCarDetail model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.AppUser != null)
            {
                strSql1.Append("AppUser,");
                strSql2.Append("" + model.AppUser + ",");
            }
            if (model.AppTime != null)
            {
                strSql1.Append("AppTime,");
                strSql2.Append("'" + model.AppTime + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.Type != null)
            {
                strSql1.Append("Type,");
                strSql2.Append("'" + model.Type + "',");
            }
            if (model.RoadLong != null)
            {
                strSql1.Append("RoadLong,");
                strSql2.Append("" + model.RoadLong + ",");
            }
            if (model.Area != null)
            {
                strSql1.Append("Area,");
                strSql2.Append("'" + model.Area + "',");
            }
            if (model.ByCarPers != null)
            {
                strSql1.Append("ByCarPers,");
                strSql2.Append("'" + model.ByCarPers + "',");
            }
            if (model.GoAddress != null)
            {
                strSql1.Append("GoAddress,");
                strSql2.Append("'" + model.GoAddress + "',");
            }
            if (model.ToAddress != null)
            {
                strSql1.Append("ToAddress,");
                strSql2.Append("'" + model.ToAddress + "',");
            }
            if (model.GoTime != null)
            {
                strSql1.Append("GoTime,");
                strSql2.Append("'" + model.GoTime + "',");
            }
            if (model.EndTime != null)
            {
                strSql1.Append("EndTime,");
                strSql2.Append("'" + model.EndTime + "',");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            if (model.CarNo != null)
            {
                strSql1.Append("CarNo,");
                strSql2.Append("'" + model.CarNo + "',");
            }
            if (model.Driver != null)
            {
                strSql1.Append("Driver,");
                strSql2.Append("'" + model.Driver + "',");
            }


            if (model.FromRoadLong != null)
            {
                strSql1.Append("FromRoadLong,");
                strSql2.Append("" + model.FromRoadLong + ",");
            }


            if (model.ToRoadLong != null)
            {
                strSql1.Append("ToRoadLong,");
                strSql2.Append("" + model.ToRoadLong + ",");
            }
            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
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

            if (model.UseDate != null)
            {
                strSql1.Append("UseDate,");
                strSql2.Append("'" + model.UseDate + "',");
            }


            strSql.Append("insert into TB_UseCarDetail(");
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
        public void Update(VAN_OA.Model.EFrom.TB_UseCarDetail model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_UseCarDetail set ");
            //if (model.AppUser != null)
            //{
            //    strSql.Append("AppUser=" + model.AppUser + ",");
            //}
            //if (model.AppTime != null)
            //{
            //    strSql.Append("AppTime='" + model.AppTime + "',");
            //}
            if (model.GuestName != null)
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            if (model.Type != null)
            {
                strSql.Append("Type='" + model.Type + "',");
            }
            if (model.RoadLong != null)
            {
                strSql.Append("RoadLong=" + model.RoadLong + ",");
            }
            if (model.Area != null)
            {
                strSql.Append("Area='" + model.Area + "',");
            }
            if (model.ByCarPers != null)
            {
                strSql.Append("ByCarPers='" + model.ByCarPers + "',");
            }
            if (model.GoAddress != null)
            {
                strSql.Append("GoAddress='" + model.GoAddress + "',");
            }
            if (model.ToAddress != null)
            {
                strSql.Append("ToAddress='" + model.ToAddress + "',");
            }
            if (model.GoTime != null)
            {
                strSql.Append("GoTime='" + model.GoTime + "',");
            }
            else
            {
                strSql.Append("GoTime= null ,");
            }
            if (model.EndTime != null)
            {
                strSql.Append("EndTime='" + model.EndTime + "',");
            }
            else
            {
                strSql.Append("EndTime= null ,");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }

            if (model.CarNo != null)
            {
                strSql.Append("CarNo='" + model.CarNo + "',");
            }
            else
            {
                strSql.Append("CarNo= null ,");
            }
            if (model.Driver != null)
            {
                strSql.Append("Driver='" + model.Driver + "',");
            }
            else
            {
                strSql.Append("Driver= null ,");
            }


            if (model.FromRoadLong != null)
            {
                strSql.Append("FromRoadLong=" + model.FromRoadLong + ",");
            }
            else
            {
                strSql.Append("FromRoadLong= null ,");
            }


            if (model.ToRoadLong != null)
            {
                strSql.Append("ToRoadLong=" + model.ToRoadLong + ",");
            }
            else
            {
                strSql.Append("ToRoadLong= null ,");
            }

            if (model.State != null)
            {
                strSql.Append("State='" + model.State + "',");
            }
            //else
            //{
            //    strSql.Append("State= null ,");
            //}
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");


            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(VAN_OA.Model.EFrom.TB_UseCarDetail model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_UseCarDetail set ");
            //if (model.AppUser != null)
            //{
            //    strSql.Append("AppUser=" + model.AppUser + ",");
            //}
            //if (model.AppTime != null)
            //{
            //    strSql.Append("AppTime='" + model.AppTime + "',");
            //}
            if (model.GuestName != null)
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            if (model.Type != null)
            {
                strSql.Append("Type='" + model.Type + "',");
            }
            if (model.RoadLong != null)
            {
                strSql.Append("RoadLong=" + model.RoadLong + ",");
            }
            if (model.Area != null)
            {
                strSql.Append("Area='" + model.Area + "',");
            }
            if (model.ByCarPers != null)
            {
                strSql.Append("ByCarPers='" + model.ByCarPers + "',");
            }
            if (model.GoAddress != null)
            {
                strSql.Append("GoAddress='" + model.GoAddress + "',");
            }
            if (model.ToAddress != null)
            {
                strSql.Append("ToAddress='" + model.ToAddress + "',");
            }
            if (model.GoTime != null)
            {
                strSql.Append("GoTime='" + model.GoTime + "',");
            }
            else
            {
                strSql.Append("GoTime= null ,");
            }
            if (model.EndTime != null)
            {
                strSql.Append("EndTime='" + model.EndTime + "',");
            }
            else
            {
                strSql.Append("EndTime= null ,");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }

            if (model.CarNo != null)
            {
                strSql.Append("CarNo='" + model.CarNo + "',");
            }
            else
            {
                strSql.Append("CarNo= null ,");
            }
            if (model.Driver != null)
            {
                strSql.Append("Driver='" + model.Driver + "',");
            }
            else
            {
                strSql.Append("Driver= null ,");
            }


            if (model.FromRoadLong != null)
            {
                strSql.Append("FromRoadLong=" + model.FromRoadLong + ",");
            }
            else
            {
                strSql.Append("FromRoadLong= null ,");
            }


            if (model.ToRoadLong != null)
            {
                strSql.Append("ToRoadLong=" + model.ToRoadLong + ",");
            }
            else
            {
                strSql.Append("ToRoadLong= null ,");
            }
            //if (model.State != null)
            //{
            //    strSql.Append("State='" + model.State + "',");
            //}
            //else
            //{
            //    strSql.Append("State= null ,");
            //}

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");

            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_UseCarDetail ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.TB_UseCarDetail GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" UseDate,TB_UseCarDetail.Id,AppUser,AppTime,GuestName,Type,RoadLong,Area,ByCarPers,GoAddress,ToAddress,GoTime,EndTime,Remark ,loginName,CarNo,Driver,FromRoadLong,ToRoadLong,DoPer,DoTime,proNo,PONo,POName,OilPrice,state");
            strSql.Append(" from TB_UseCarDetail  left join tb_User on TB_UseCarDetail.AppUser=tb_User.ID");
            strSql.Append(" where TB_UseCarDetail.Id=" + id + "");

            VAN_OA.Model.EFrom.TB_UseCarDetail model = null;
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
        public List<VAN_OA.Model.EFrom.TB_UseCarDetail> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UseDate,TB_UseCarDetail.Id,AppUser,AppTime,GuestName,Type,RoadLong,Area,ByCarPers,GoAddress,ToAddress,GoTime,EndTime,Remark,loginName,CarNo,Driver,FromRoadLong,ToRoadLong,DoPer,DoTime,proNo,PONo,POName,OilPrice,state ");
            strSql.Append(" FROM TB_UseCarDetail  left join tb_User on TB_UseCarDetail.AppUser=tb_User.Id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.TB_UseCarDetail> list = new List<VAN_OA.Model.EFrom.TB_UseCarDetail>();

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
        public List<VAN_OA.Model.EFrom.TB_UseCarDetail> GetListArrayReps(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UseDate,TB_UseCarDetail.Id,AppUser,AppTime,GuestName,Type,RoadLong,Area,ByCarPers,GoAddress,ToAddress,GoTime,EndTime,Remark,loginName,CarNo,Driver ,FromRoadLong,ToRoadLong,DoPer,DoTime,proNo,PONo,POName,OilPrice,state");
            strSql.Append(" FROM TB_UseCarDetail  left join tb_User on TB_UseCarDetail.AppUser=tb_User.Id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }



            strSql.Append(" order by AppTime");

            List<VAN_OA.Model.EFrom.TB_UseCarDetail> list = new List<VAN_OA.Model.EFrom.TB_UseCarDetail>();

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
        /// 销售结算明细表  -公司年度总用车里程数
        /// </summary>
        /// <param name="fristTime"></param>
        /// <param name="lastTime"></param>
        /// <returns></returns>
        public List<TB_UseCarDetailReport> Get_Report(string year, string comCode, string userId)
        {
            var list = new List<TB_UseCarDetailReport>();

            string aeSql = "";

            if (comCode != "")
            {
                comCode = "'" + comCode + "'";
                aeSql = string.Format(" and tb_User.CompanyCode IN ({0})  ", comCode);
            }

            string sql = string.Format(@"select sum(RoadLong) as RoadLong,AppUser,loginName FROM TB_UseCarDetail  left join tb_User on TB_UseCarDetail.AppUser=tb_User.Id
where  1=1  and PONo like '%{0}%' {1} and TB_UseCarDetail.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='用车明细表') and state='通过') group by AppUser,loginName", year, aeSql);

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = new TB_UseCarDetailReport();
                        model.RoadLong = (decimal)dataReader["RoadLong"];
                        model.UserId = (int)dataReader["AppUser"];
                        model.UserName = (string)dataReader["loginName"];
                        list.Add(model);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.EFrom.TB_UseCarDetail> GetListArrayReps_1(string strWhere, out decimal Total, out decimal TotalPrice)
        {
            TotalPrice = 0;
            Total = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AE,UseDate,TB_UseCarDetail.Id,AppUser,AppTime,TB_UseCarDetail.GuestName,Type,RoadLong,Area,ByCarPers,GoAddress,ToAddress,GoTime,EndTime,Remark,loginName,CarNo,Driver,FromRoadLong,ToRoadLong,DoPer,DoTime ,TB_UseCarDetail.proNo,TB_UseCarDetail.PONo,TB_UseCarDetail.POName,OilPrice,state");
            strSql.Append(" FROM TB_UseCarDetail  left join tb_User on TB_UseCarDetail.AppUser=tb_User.Id");
            strSql.Append("  left join CG_POOrder  on CG_POOrder.PONo=TB_UseCarDetail.PONo and CG_POOrder.IFZhui=0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }


            strSql.Append(" order by useDate desc");
            // strSql.Append(" order by AppTime desc");

            List<VAN_OA.Model.EFrom.TB_UseCarDetail> list = new List<VAN_OA.Model.EFrom.TB_UseCarDetail>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        TB_UseCarDetail dd = ReaderBind(dataReader);
                        object ojb;
                        ojb = dataReader["AE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            dd.AE = ojb.ToString();
                        }
                        Total += dd.RoadLong;
                        TotalPrice += dd.RoadLong * dd.OilPrice;
                        dd.TotalPrice = dd.RoadLong * dd.OilPrice;
                        list.Add(dd);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.EFrom.TB_UseCarDetail ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.TB_UseCarDetail model = new VAN_OA.Model.EFrom.TB_UseCarDetail();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["AppUser"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppUser = (int)ojb;
            }
            ojb = dataReader["AppTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppTime = (DateTime)ojb;
            }
            model.GuestName = dataReader["GuestName"].ToString();
            model.Type = dataReader["Type"].ToString();
            ojb = dataReader["RoadLong"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RoadLong = (decimal)ojb;
            }
            model.Area = dataReader["Area"].ToString();
            model.ByCarPers = dataReader["ByCarPers"].ToString();
            model.GoAddress = dataReader["GoAddress"].ToString();
            model.ToAddress = dataReader["ToAddress"].ToString();
            ojb = dataReader["GoTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoTime = (DateTime)ojb;
                model.GoEndTime = model.GoTime.Value.ToShortDateString() + " " + model.GoTime.Value.ToShortTimeString() + "~";
            }
            ojb = dataReader["EndTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.EndTime = (DateTime)ojb;
                model.GoEndTime += model.EndTime.Value.ToShortDateString() + " " + model.EndTime.Value.ToShortTimeString();
            }
            model.Remark = dataReader["Remark"].ToString();

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppUserName = ojb.ToString();
            }


            ojb = dataReader["FromRoadLong"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FromRoadLong = Convert.ToDecimal(ojb);
            }


            ojb = dataReader["ToRoadLong"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ToRoadLong = Convert.ToDecimal(ojb);
            }




            ojb = dataReader["DoPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DoPer = Convert.ToString(ojb);
            }

            ojb = dataReader["DoTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DoTime = Convert.ToDateTime(ojb);
            }


            model.CarNo = dataReader["CarNo"].ToString();
            model.Driver = dataReader["Driver"].ToString();

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

            ojb = dataReader["POName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POName = ojb.ToString();
            }
            ojb = dataReader["OilPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OilPrice = Convert.ToDecimal(ojb);
            }

            ojb = dataReader["State"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.State = ojb.ToString();
            }

            ojb = dataReader["UseDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UseDate = Convert.ToDateTime(ojb);
            }

            return model;
        }



    }
}
