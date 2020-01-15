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
    public class Tb_DispatchListService
    {
        public bool updateTran(VAN_OA.Model.EFrom.Tb_DispatchList model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
        public int addTran(VAN_OA.Model.EFrom.Tb_DispatchList model, VAN_OA.Model.EFrom.tb_EForm eform)
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


                    string MaxCardNo = "";
                    string sql = "select  right('0000000000'+(convert(varchar,(convert(int,right(max(CardNo),4))+1))),4) FROM  Tb_DispatchList where CardNo like '" + DateTime.Now.Year + "%';";
                    objCommand.CommandText = sql.ToString();
                    object objMax = objCommand.ExecuteScalar();
                    if (objMax != null && objMax.ToString() != "")
                    {
                        MaxCardNo = DateTime.Now.Year.ToString() + objMax.ToString();
                    }
                    else
                    {
                        MaxCardNo = DateTime.Now.Year.ToString() + "0001";
                    }

                    tb_EFormService eformSer = new tb_EFormService();
                    eform.E_No = MaxCardNo;
                    model.CardNo = MaxCardNo;
                    model.State = eform.state;
                    objCommand.Parameters.Clear();
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
        public int Add(VAN_OA.Model.EFrom.Tb_DispatchList model, SqlCommand objCommand)
        {
           

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.UserId != null)
            {
                strSql1.Append("UserId,");
                strSql2.Append("" + model.UserId + ",");
            }
            if (model.EvTime != null)
            {
                strSql1.Append("EvTime,");
                strSql2.Append("'" + model.EvTime + "',");
            }
            if (model.CreateTime != null)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("'" + model.CreateTime + "',");
            }
            if (model.CardNo != "")
            {
                strSql1.Append("CardNo,");
                strSql2.Append("'" + model.CardNo + "',");
            }
            if (model.BusFromAddress != null)
            {
                strSql1.Append("BusFromAddress,");
                strSql2.Append("'" + model.BusFromAddress + "',");
            }
            if (model.BusToAddress != null)
            {
                strSql1.Append("BusToAddress,");
                strSql2.Append("'" + model.BusToAddress + "',");
            }
            if (model.BusTotal != null)
            {
                strSql1.Append("BusTotal,");
                strSql2.Append("" + model.BusTotal + ",");
            }
            if (model.IfTexi != null)
            {
                strSql1.Append("IfTexi,");
                strSql2.Append("" + (model.IfTexi ? 1 : 0) + ",");
            }
            if (model.IfBus != null)
            {
                strSql1.Append("IfBus,");
                strSql2.Append("" + (model.IfBus ? 1 : 0) + ",");
            }
            if (model.BusFromTime != null)
            {
                strSql1.Append("BusFromTime,");
                strSql2.Append("'" + model.BusFromTime + "',");
            }
            if (model.BusToTime != null)
            {
                strSql1.Append("BusToTime,");
                strSql2.Append("'" + model.BusToTime + "',");
            }
            if (model.RepastAddress != null)
            {
                strSql1.Append("RepastAddress,");
                strSql2.Append("'" + model.RepastAddress + "',");
            }
            if (model.RepastTotal != null)
            {
                strSql1.Append("RepastTotal,");
                strSql2.Append("" + model.RepastTotal + ",");
            }
            if (model.RepastPerNum != null)
            {
                strSql1.Append("RepastPerNum,");
                strSql2.Append("" + model.RepastPerNum + ",");
            }
            if (model.RepastPers != null)
            {
                strSql1.Append("RepastPers,");
                strSql2.Append("'" + model.RepastPers + "',");
            }
            if (model.RepastType != null)
            {
                strSql1.Append("RepastType,");
                strSql2.Append("'" + model.RepastType + "',");
            }
            if (model.HotelAddress != null)
            {
                strSql1.Append("HotelAddress,");
                strSql2.Append("'" + model.HotelAddress + "',");
            }
            if (model.HotelName != null)
            {
                strSql1.Append("HotelName,");
                strSql2.Append("'" + model.HotelName + "',");
            }
            if (model.HotelTotal != null)
            {
                strSql1.Append("HotelTotal,");
                strSql2.Append("" + model.HotelTotal + ",");
            }
            if (model.HotelType != null)
            {
                strSql1.Append("HotelType,");
                strSql2.Append("'" + model.HotelType + "',");
            }
            if (model.OilFromAddress != null)
            {
                strSql1.Append("OilFromAddress,");
                strSql2.Append("'" + model.OilFromAddress + "',");
            }
            if (model.OilToAddress != null)
            {
                strSql1.Append("OilToAddress,");
                strSql2.Append("'" + model.OilToAddress + "',");
            }
            if (model.OilLiCheng != null)
            {
                strSql1.Append("OilLiCheng,");
                strSql2.Append("" + model.OilLiCheng + ",");
            }
            if (model.OilTotal != null)
            {
                strSql1.Append("OilTotal,");
                strSql2.Append("" + model.OilTotal + ",");
            }
            if (model.OilXiShu != null)
            {
                strSql1.Append("OilXiShu,");
                strSql2.Append("" + model.OilXiShu + ",");
            }
            if (model.GuoBeginAddress != null)
            {
                strSql1.Append("GuoBeginAddress,");
                strSql2.Append("'" + model.GuoBeginAddress + "',");
            }
            if (model.GuoToAddress != null)
            {
                strSql1.Append("GuoToAddress,");
                strSql2.Append("'" + model.GuoToAddress + "',");
            }
            if (model.GuoTotal != null)
            {
                strSql1.Append("GuoTotal,");
                strSql2.Append("" + model.GuoTotal + ",");
            }
            if (model.PostFrom != null)
            {
                strSql1.Append("PostFrom,");
                strSql2.Append("" + (model.PostFrom ? 1 : 0) + ",");
            }
            if (model.PostFromAddress != null)
            {
                strSql1.Append("PostFromAddress,");
                strSql2.Append("'" + model.PostFromAddress + "',");
            }
            if (model.PostTo != null)
            {
                strSql1.Append("PostTo,");
                strSql2.Append("" + (model.PostTo ? 1 : 0) + ",");
            }
            if (model.PostToAddress != null)
            {
                strSql1.Append("PostToAddress,");
                strSql2.Append("'" + model.PostToAddress + "',");
            }
            if (model.PostTotal != null)
            {
                strSql1.Append("PostTotal,");
                strSql2.Append("" + model.PostTotal + ",");
            }
            if (model.PoContext != null)
            {
                strSql1.Append("PoContext,");
                strSql2.Append("'" + model.PoContext + "',");
            }
            if (model.PoTotal != null)
            {
                strSql1.Append("PoTotal,");
                strSql2.Append("" + model.PoTotal + ",");
            }
            if (model.OtherContext != null)
            {
                strSql1.Append("OtherContext,");
                strSql2.Append("'" + model.OtherContext + "',");
            }
            if (model.OtherTotal != null)
            {
                strSql1.Append("OtherTotal,");
                strSql2.Append("" + model.OtherTotal + ",");
            }


            if (model.BusRemark != null)
            {
                strSql1.Append("BusRemark,");
                strSql2.Append("'" + model.BusRemark + "',");
            }
            if (model.RepastRemark != null)
            {
                strSql1.Append("RepastRemark,");
                strSql2.Append("'" + model.RepastRemark + "',");
            }
            if (model.HotelRemark != null)
            {
                strSql1.Append("HotelRemark,");
                strSql2.Append("'" + model.HotelRemark + "',");
            }
            if (model.OilRemark != null)
            {
                strSql1.Append("OilRemark,");
                strSql2.Append("'" + model.OilRemark + "',");
            }
            if (model.GuoRemark != null)
            {
                strSql1.Append("GuoRemark,");
                strSql2.Append("'" + model.GuoRemark + "',");
            }
            if (model.PostRemark != null)
            {
                strSql1.Append("PostRemark,");
                strSql2.Append("'" + model.PostRemark + "',");
            }
            if (model.PoRemark != null)
            {
                strSql1.Append("PoRemark,");
                strSql2.Append("'" + model.PoRemark + "',");
            }
            if (model.OtherRemark != null)
            {
                strSql1.Append("OtherRemark,");
                strSql2.Append("'" + model.OtherRemark + "',");
            }
            if (model.PostNo != null)
            {
                strSql1.Append("PostNo,");
                strSql2.Append("'" + model.PostNo + "',");
            }
            if (model.PostCompany != null)
            {
                strSql1.Append("PostCompany,");
                strSql2.Append("'" + model.PostCompany + "',");
            }
            if (model.PostContext != null)
            {
                strSql1.Append("PostContext,");
                strSql2.Append("'" + model.PostContext + "',");
            }
            if (model.PostToPer != null)
            {
                strSql1.Append("PostToPer,");
                strSql2.Append("'" + model.PostToPer + "',");
            }

            if (model.Post_Id != null)
            {
                strSql1.Append("Post_Id,");
                strSql2.Append("" + model.Post_Id + ",");
            }
            if (model.Post_No != null)
            {
                strSql1.Append("Post_No,");
                strSql2.Append("'" + model.Post_No + "',");
            }

            if (model.PoNo != null)
            {
                strSql1.Append("PoNo,");
                strSql2.Append("'" + model.PoNo + "',");
            }
            if (model.PoName != null)
            {
                strSql1.Append("PoName,");
                strSql2.Append("'" + model.PoName + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.CaiPoNo != null)
            {
                strSql1.Append("CaiPoNo,");
                strSql2.Append("'" + model.CaiPoNo + "',");
            }

            if (model.CaiId != null)
            {
                strSql1.Append("CaiId,");
                strSql2.Append("" + model.CaiId + ",");
            }
            if (model.State != null)
            {
                strSql1.Append("State,");
                strSql2.Append("'" + model.State + "',");
            }
            strSql1.Append("BusFPNO,");
            strSql2.Append("'" + model.BusFPNO + "',");
            strSql1.Append("RepastFPNO,");
            strSql2.Append("'" + model.RepastFPNO + "',");
            strSql1.Append("HotelFPNO,");
            strSql2.Append("'" + model.HotelFPNO + "',");
            strSql1.Append("OilFPNO,");
            strSql2.Append("'" + model.OilFPNO + "',");
            strSql1.Append("GuoBeginFPNO,");
            strSql2.Append("'" + model.GuoBeginFPNO + "',");
            strSql1.Append("PostFPNO,");
            strSql2.Append("'" + model.PostFPNO + "',");
            strSql1.Append("OtherFPNO,");
            strSql2.Append("'" + model.OtherFPNO + "',");
            strSql1.Append("CaiFPNO,");
            strSql2.Append("'" + model.CaiFPNO + "',");


            strSql.Append("insert into Tb_DispatchList(");
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
        public void Update(VAN_OA.Model.EFrom.Tb_DispatchList model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Tb_DispatchList set ");
           
            if (model.EvTime != null)
            {
                strSql.Append("EvTime='" + model.EvTime + "',");
            }
            if (model.CreateTime != null)
            {
                strSql.Append("CreateTime='" + model.CreateTime + "',");
            }
            if (model.CardNo != null)
            {
                strSql.Append("CardNo='" + model.CardNo + "',");
            }
            if (model.BusFromAddress != null)
            {
                strSql.Append("BusFromAddress='" + model.BusFromAddress + "',");
            }
            else
            {
                strSql.Append("BusFromAddress= null ,");
            }
            if (model.BusToAddress != null)
            {
                strSql.Append("BusToAddress='" + model.BusToAddress + "',");
            }
            else
            {
                strSql.Append("BusToAddress= null ,");
            }
            if (model.BusTotal != null)
            {
                strSql.Append("BusTotal=" + model.BusTotal + ",");
            }
            else
            {
                strSql.Append("BusTotal= null ,");
            }
            if (model.IfTexi != null)
            {
                strSql.Append("IfTexi=" + (model.IfTexi ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("IfTexi= null ,");
            }
            if (model.IfBus != null)
            {
                strSql.Append("IfBus=" + (model.IfBus ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("IfBus= null ,");
            }
            if (model.BusFromTime != null)
            {
                strSql.Append("BusFromTime='" + model.BusFromTime + "',");
            }
            else
            {
                strSql.Append("BusFromTime= null ,");
            }
            if (model.BusToTime != null)
            {
                strSql.Append("BusToTime='" + model.BusToTime + "',");
            }
            else
            {
                strSql.Append("BusToTime= null ,");
            }
            if (model.RepastAddress != null)
            {
                strSql.Append("RepastAddress='" + model.RepastAddress + "',");
            }
            else
            {
                strSql.Append("RepastAddress= null ,");
            }
            if (model.RepastTotal != null)
            {
                strSql.Append("RepastTotal=" + model.RepastTotal + ",");
            }
            else
            {
                strSql.Append("RepastTotal= null ,");
            }
            if (model.RepastPerNum != null)
            {
                strSql.Append("RepastPerNum=" + model.RepastPerNum + ",");
            }
            else
            {
                strSql.Append("RepastPerNum= null ,");
            }
            if (model.RepastPers != null)
            {
                strSql.Append("RepastPers='" + model.RepastPers + "',");
            }
            else
            {
                strSql.Append("RepastPers= null ,");
            }
            if (model.RepastType != null)
            {
                strSql.Append("RepastType='" + model.RepastType + "',");
            }
            else
            {
                strSql.Append("RepastType= null ,");
            }
            if (model.HotelAddress != null)
            {
                strSql.Append("HotelAddress='" + model.HotelAddress + "',");
            }
            else
            {
                strSql.Append("HotelAddress= null ,");
            }
            if (model.HotelName != null)
            {
                strSql.Append("HotelName='" + model.HotelName + "',");
            }
            else
            {
                strSql.Append("HotelName= null ,");
            }
            if (model.HotelTotal != null)
            {
                strSql.Append("HotelTotal=" + model.HotelTotal + ",");
            }
            else
            {
                strSql.Append("HotelTotal= null ,");
            }
            if (model.HotelType != null)
            {
                strSql.Append("HotelType='" + model.HotelType + "',");
            }
            else
            {
                strSql.Append("HotelType= null ,");
            }
            if (model.OilFromAddress != null)
            {
                strSql.Append("OilFromAddress='" + model.OilFromAddress + "',");
            }
            else
            {
                strSql.Append("OilFromAddress= null ,");
            }
            if (model.OilToAddress != null)
            {
                strSql.Append("OilToAddress='" + model.OilToAddress + "',");
            }
            else
            {
                strSql.Append("OilToAddress= null ,");
            }
            if (model.OilLiCheng != null)
            {
                strSql.Append("OilLiCheng=" + model.OilLiCheng + ",");
            }
            else
            {
                strSql.Append("OilLiCheng= null ,");
            }
            if (model.OilTotal != null)
            {
                strSql.Append("OilTotal=" + model.OilTotal + ",");
            }
            else
            {
                strSql.Append("OilTotal= null ,");
            }
            if (model.OilXiShu != null)
            {
                strSql.Append("OilXiShu=" + model.OilXiShu + ",");
            }
            else
            {
                strSql.Append("OilXiShu= null ,");
            }
            if (model.GuoBeginAddress != null)
            {
                strSql.Append("GuoBeginAddress='" + model.GuoBeginAddress + "',");
            }
            else
            {
                strSql.Append("GuoBeginAddress= null ,");
            }
            if (model.GuoToAddress != null)
            {
                strSql.Append("GuoToAddress='" + model.GuoToAddress + "',");
            }
            else
            {
                strSql.Append("GuoToAddress= null ,");
            }
            if (model.GuoTotal != null)
            {
                strSql.Append("GuoTotal=" + model.GuoTotal + ",");
            }
            else
            {
                strSql.Append("GuoTotal= null ,");
            }
            if (model.PostFrom != null)
            {
                strSql.Append("PostFrom=" + (model.PostFrom ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("PostFrom= null ,");
            }
            if (model.PostFromAddress != null)
            {
                strSql.Append("PostFromAddress='" + model.PostFromAddress + "',");
            }
            else
            {
                strSql.Append("PostFromAddress= null ,");
            }
            if (model.PostTo != null)
            {
                strSql.Append("PostTo=" + (model.PostTo ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("PostTo= null ,");
            }
            if (model.PostToAddress != null)
            {
                strSql.Append("PostToAddress='" + model.PostToAddress + "',");
            }
            else
            {
                strSql.Append("PostToAddress= null ,");
            }
            if (model.PostTotal != null)
            {
                strSql.Append("PostTotal=" + model.PostTotal + ",");
            }
            else
            {
                strSql.Append("PostTotal= null ,");
            }
            if (model.PoContext != null)
            {
                strSql.Append("PoContext='" + model.PoContext + "',");
            }
            else
            {
                strSql.Append("PoContext= null ,");
            }
            if (model.PoTotal != null)
            {
                strSql.Append("PoTotal=" + model.PoTotal + ",");
            }
            else
            {
                strSql.Append("PoTotal= null ,");
            }
            if (model.OtherContext != null)
            {
                strSql.Append("OtherContext='" + model.OtherContext + "',");
            }
            else
            {
                strSql.Append("OtherContext= null ,");
            }
            if (model.OtherTotal != null)
            {
                strSql.Append("OtherTotal=" + model.OtherTotal + ",");
            }
            else
            {
                strSql.Append("OtherTotal= null ,");
            }


            if (model.State != null)
            {
                strSql.Append("State='" + model.State + "',");
            }
            else
            {
                strSql.Append("State= null ,");
            }
            strSql.Append("BusFPNO='" + model.BusFPNO + "',");
            strSql.Append("RepastFPNO='" + model.RepastFPNO + "',");
            strSql.Append("HotelFPNO='" + model.HotelFPNO + "',");
            strSql.Append("OilFPNO='" + model.OilFPNO + "',");
            strSql.Append("GuoBeginFPNO='" + model.GuoBeginFPNO + "',");
            strSql.Append("PostFPNO='" + model.PostFPNO + "',");
            strSql.Append("OtherFPNO='" + model.OtherFPNO + "',");
            strSql.Append("CaiFPNO='" + model.CaiFPNO + "',");

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        public bool Update_FPNO(VAN_OA.Model.EFrom.Tb_DispatchList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Tb_DispatchList set ");
            strSql.Append("BusFPNO='" + model.BusFPNO + "',");
            strSql.Append("RepastFPNO='" + model.RepastFPNO + "',");
            strSql.Append("HotelFPNO='" + model.HotelFPNO + "',");
            strSql.Append("OilFPNO='" + model.OilFPNO + "',");
            strSql.Append("GuoBeginFPNO='" + model.GuoBeginFPNO + "',");
            strSql.Append("PostFPNO='" + model.PostFPNO + "',");
            strSql.Append("OtherFPNO='" + model.OtherFPNO + "',");
            strSql.Append("CaiFPNO='" + model.CaiFPNO + "',");
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        public bool Update(VAN_OA.Model.EFrom.Tb_DispatchList model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Tb_DispatchList set ");

            if (model.EvTime != null)
            {
                strSql.Append("EvTime='" + model.EvTime + "',");
            }
            if (model.CreateTime != null)
            {
                strSql.Append("CreateTime='" + model.CreateTime + "',");
            }
            if (model.CardNo != null)
            {
                strSql.Append("CardNo='" + model.CardNo + "',");
            }
            if (model.BusFromAddress != null)
            {
                strSql.Append("BusFromAddress='" + model.BusFromAddress + "',");
            }
            else
            {
                strSql.Append("BusFromAddress= null ,");
            }
            if (model.BusToAddress != null)
            {
                strSql.Append("BusToAddress='" + model.BusToAddress + "',");
            }
            else
            {
                strSql.Append("BusToAddress= null ,");
            }
            //if (model.BusTotal != null)
            //{
            //    strSql.Append("BusTotal=" + model.BusTotal + ",");
            //}
            //else
            //{
            //    strSql.Append("BusTotal= null ,");
            //}
            if (model.IfTexi != null)
            {
                strSql.Append("IfTexi=" + (model.IfTexi ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("IfTexi= null ,");
            }
            if (model.IfBus != null)
            {
                strSql.Append("IfBus=" + (model.IfBus ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("IfBus= null ,");
            }
            if (model.BusFromTime != null)
            {
                strSql.Append("BusFromTime='" + model.BusFromTime + "',");
            }
            else
            {
                strSql.Append("BusFromTime= null ,");
            }
            if (model.BusToTime != null)
            {
                strSql.Append("BusToTime='" + model.BusToTime + "',");
            }
            else
            {
                strSql.Append("BusToTime= null ,");
            }
            if (model.RepastAddress != null)
            {
                strSql.Append("RepastAddress='" + model.RepastAddress + "',");
            }
            else
            {
                strSql.Append("RepastAddress= null ,");
            }
            //if (model.RepastTotal != null)
            //{
            //    strSql.Append("RepastTotal=" + model.RepastTotal + ",");
            //}
            //else
            //{
            //    strSql.Append("RepastTotal= null ,");
            //}
            if (model.RepastPerNum != null)
            {
                strSql.Append("RepastPerNum=" + model.RepastPerNum + ",");
            }
            else
            {
                strSql.Append("RepastPerNum= null ,");
            }
            if (model.RepastPers != null)
            {
                strSql.Append("RepastPers='" + model.RepastPers + "',");
            }
            else
            {
                strSql.Append("RepastPers= null ,");
            }
            if (model.RepastType != null)
            {
                strSql.Append("RepastType='" + model.RepastType + "',");
            }
            else
            {
                strSql.Append("RepastType= null ,");
            }
            if (model.HotelAddress != null)
            {
                strSql.Append("HotelAddress='" + model.HotelAddress + "',");
            }
            else
            {
                strSql.Append("HotelAddress= null ,");
            }
            if (model.HotelName != null)
            {
                strSql.Append("HotelName='" + model.HotelName + "',");
            }
            else
            {
                strSql.Append("HotelName= null ,");
            }
            //if (model.HotelTotal != null)
            //{
            //    strSql.Append("HotelTotal=" + model.HotelTotal + ",");
            //}
            //else
            //{
            //    strSql.Append("HotelTotal= null ,");
            //}
            if (model.HotelType != null)
            {
                strSql.Append("HotelType='" + model.HotelType + "',");
            }
            else
            {
                strSql.Append("HotelType= null ,");
            }
            if (model.OilFromAddress != null)
            {
                strSql.Append("OilFromAddress='" + model.OilFromAddress + "',");
            }
            else
            {
                strSql.Append("OilFromAddress= null ,");
            }
            if (model.OilToAddress != null)
            {
                strSql.Append("OilToAddress='" + model.OilToAddress + "',");
            }
            else
            {
                strSql.Append("OilToAddress= null ,");
            }
            if (model.OilLiCheng != null)
            {
                strSql.Append("OilLiCheng=" + model.OilLiCheng + ",");
            }
            else
            {
                strSql.Append("OilLiCheng= null ,");
            }
            //if (model.OilTotal != null)
            //{
            //    strSql.Append("OilTotal=" + model.OilTotal + ",");
            //}
            //else
            //{
            //    strSql.Append("OilTotal= null ,");
            //}
            if (model.OilXiShu != null)
            {
                strSql.Append("OilXiShu=" + model.OilXiShu + ",");
            }
            else
            {
                strSql.Append("OilXiShu= null ,");
            }
            if (model.GuoBeginAddress != null)
            {
                strSql.Append("GuoBeginAddress='" + model.GuoBeginAddress + "',");
            }
            else
            {
                strSql.Append("GuoBeginAddress= null ,");
            }
            if (model.GuoToAddress != null)
            {
                strSql.Append("GuoToAddress='" + model.GuoToAddress + "',");
            }
            else
            {
                strSql.Append("GuoToAddress= null ,");
            }
            //if (model.GuoTotal != null)
            //{
            //    strSql.Append("GuoTotal=" + model.GuoTotal + ",");
            //}
            //else
            //{
            //    strSql.Append("GuoTotal= null ,");
            //}
            if (model.PostFrom != null)
            {
                strSql.Append("PostFrom=" + (model.PostFrom ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("PostFrom= null ,");
            }
            if (model.PostFromAddress != null)
            {
                strSql.Append("PostFromAddress='" + model.PostFromAddress + "',");
            }
            else
            {
                strSql.Append("PostFromAddress= null ,");
            }
            if (model.PostTo != null)
            {
                strSql.Append("PostTo=" + (model.PostTo ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("PostTo= null ,");
            }
            if (model.PostToAddress != null)
            {
                strSql.Append("PostToAddress='" + model.PostToAddress + "',");
            }
            else
            {
                strSql.Append("PostToAddress= null ,");
            }
            //if (model.PostTotal != null)
            //{
            //    strSql.Append("PostTotal=" + model.PostTotal + ",");
            //}
            //else
            //{
            //    strSql.Append("PostTotal= null ,");
            //}
            if (model.PoContext != null)
            {
                strSql.Append("PoContext='" + model.PoContext + "',");
            }
            else
            {
                strSql.Append("PoContext= null ,");
            }
            if (model.PoTotal != null)
            {
                strSql.Append("PoTotal=" + model.PoTotal + ",");
            }
            else
            {
                strSql.Append("PoTotal= null ,");
            }
            if (model.OtherContext != null)
            {
                strSql.Append("OtherContext='" + model.OtherContext + "',");
            }
            else
            {
                strSql.Append("OtherContext= null ,");
            }
            //if (model.OtherTotal != null)
            //{
            //    strSql.Append("OtherTotal=" + model.OtherTotal + ",");
            //}
            //else
            //{
            //    strSql.Append("OtherTotal= null ,");
            //}


            //if (model.State != null)
            //{
            //    strSql.Append("State='" + model.State + "',");
            //}
            //else
            //{
            //    strSql.Append("State= null ,");
            //}

            strSql.Append("BusRemark='" + model.BusRemark + "',");
            strSql.Append("RepastRemark='" + model.RepastRemark + "',");
            strSql.Append("HotelRemark='" + model.HotelRemark + "',");
            strSql.Append("OilRemark='" + model.OilRemark + "',");
            strSql.Append("GuoRemark='" + model.GuoRemark + "',");
            strSql.Append("PostRemark='" + model.PostRemark + "',");
            strSql.Append("PoRemark='" + model.PoRemark + "',");
            strSql.Append("OtherRemark='" + model.OtherRemark + "',");

            strSql.Append("BusFPNO='" + model.BusFPNO + "',");
            strSql.Append("RepastFPNO='" + model.RepastFPNO + "',");
            strSql.Append("HotelFPNO='" + model.HotelFPNO + "',");
            strSql.Append("OilFPNO='" + model.OilFPNO + "',");
            strSql.Append("GuoBeginFPNO='" + model.GuoBeginFPNO + "',");
            strSql.Append("PostFPNO='" + model.PostFPNO + "',");
            strSql.Append("OtherFPNO='" + model.OtherFPNO + "',");
            strSql.Append("CaiFPNO='" + model.CaiFPNO + "',");

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
            strSql.Append("delete from Tb_DispatchList ");
            strSql.Append(" where Id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.Tb_DispatchList GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");

            strSql.Append("BusFPNO,RepastFPNO,HotelFPNO,OilFPNO,GuoBeginFPNO,PostFPNO,OtherFPNO,CaiFPNO,loginName, Tb_DispatchList.Id,UserId,EvTime,CreateTime,CardNo,BusFromAddress,BusToAddress,BusTotal,IfTexi,IfBus,BusFromTime,BusToTime,RepastAddress,RepastTotal,RepastPerNum,RepastPers,RepastType,HotelAddress,HotelName,HotelTotal,HotelType,OilFromAddress,OilToAddress,OilLiCheng,OilTotal,OilXiShu,GuoBeginAddress,GuoToAddress,GuoTotal,PostFrom,PostFromAddress,PostTo,PostToAddress,PostTotal,PoContext,PoTotal,OtherContext,OtherTotal ,BusRemark,RepastRemark,HotelRemark,OilRemark,GuoRemark,PostRemark,PoRemark,OtherRemark,PostNo,PostCompany,PostContext,PostToPer,Post_Id,Post_No,state ");
            strSql.Append(" ,PoNo,PoName,GuestName,CaiPoNo,CaiId ");
            strSql.Append(" from Tb_DispatchList left join tb_User on tb_User.id=Tb_DispatchList.UserId");
            strSql.Append(" where Tb_DispatchList.Id=" + id + "");

            VAN_OA.Model.EFrom.Tb_DispatchList model = null;
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
        public List<VAN_OA.Model.EFrom.Tb_DispatchList> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append("loginName, Tb_DispatchList.Id,UserId,EvTime,CreateTime,CardNo,BusFromAddress,BusToAddress,BusTotal,IfTexi,IfBus,BusFromTime,BusToTime,RepastAddress,RepastTotal,RepastPerNum,RepastPers,RepastType,HotelAddress,HotelName,HotelTotal,HotelType,OilFromAddress,OilToAddress,OilLiCheng,OilTotal,OilXiShu,GuoBeginAddress,GuoToAddress,GuoTotal,PostFrom,PostFromAddress,PostTo,PostToAddress,PostTotal,PoContext,PoTotal,OtherContext,OtherTotal ,BusRemark,RepastRemark,HotelRemark,OilRemark,GuoRemark,PostRemark,PoRemark,OtherRemark,PostNo,PostCompany,PostContext,PostToPer,Post_Id,Post_No,state ");
            //strSql.Append(" ,BusPoNo,BusPoName,BusGuestName,RepastPoNo,RepastPoName,RepastGuestName,HotelPoNo,HotelPoName,HotelGuestName,OilPoNo,OilPoName,OilGuestName,GuoPoNo,GuoPoName,GuoGuestName,PostPoNo,PostPoName,PostGuestName,PoPoNo,PoPoName,PoGuestName,OtherPoNo,OtherName,OtherGuestName ");
            strSql.Append(" ,PoNo,PoName,GuestName,CaiPoNo,CaiId ");
            strSql.Append(" FROM Tb_DispatchList left join tb_User on tb_User.id=Tb_DispatchList.UserId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by EvTime desc");
            List<VAN_OA.Model.EFrom.Tb_DispatchList> list = new List<VAN_OA.Model.EFrom.Tb_DispatchList>();

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
        public Tb_DispatchList ReaderBind(IDataReader dataReader)
        {
           Tb_DispatchList model = new Tb_DispatchList();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["UserId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UserId = (int)ojb;
            }
            ojb = dataReader["EvTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.EvTime = (DateTime)ojb;
            }
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }
            model.CardNo = dataReader["CardNo"].ToString();
            model.BusFromAddress = dataReader["BusFromAddress"].ToString();
            model.BusToAddress = dataReader["BusToAddress"].ToString();
            ojb = dataReader["BusTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BusTotal = (decimal)ojb;
                model.Total += (decimal)ojb;
            }
            ojb = dataReader["IfTexi"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IfTexi = (bool)ojb;
            }
            ojb = dataReader["IfBus"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IfBus = (bool)ojb;
            }
            ojb = dataReader["BusFromTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BusFromTime = (DateTime)ojb;
            }
            ojb = dataReader["BusToTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BusToTime = (DateTime)ojb;
            }
            model.RepastAddress = dataReader["RepastAddress"].ToString();
            ojb = dataReader["RepastTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RepastTotal = (decimal)ojb;

                model.Total += (decimal)ojb;
            }
            ojb = dataReader["RepastPerNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RepastPerNum = (decimal)ojb;
            }
            model.RepastPers = dataReader["RepastPers"].ToString();
            model.RepastType = dataReader["RepastType"].ToString();
            model.HotelAddress = dataReader["HotelAddress"].ToString();
            model.HotelName = dataReader["HotelName"].ToString();
            ojb = dataReader["HotelTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HotelTotal = (decimal)ojb;
                model.Total += (decimal)ojb;
            }
            model.HotelType = dataReader["HotelType"].ToString();
            model.OilFromAddress = dataReader["OilFromAddress"].ToString();
            model.OilToAddress = dataReader["OilToAddress"].ToString();
            ojb = dataReader["OilLiCheng"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OilLiCheng = (decimal)ojb;
            }
            ojb = dataReader["OilTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OilTotal = (decimal)ojb;
                model.Total += (decimal)ojb;
            }
            ojb = dataReader["OilXiShu"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OilXiShu = (decimal)ojb;
            }
            model.GuoBeginAddress = dataReader["GuoBeginAddress"].ToString();
            model.GuoToAddress = dataReader["GuoToAddress"].ToString();
            ojb = dataReader["GuoTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuoTotal = (decimal)ojb;
                model.Total += (decimal)ojb;
            }
            ojb = dataReader["PostFrom"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PostFrom = (bool)ojb;
            }
            model.PostFromAddress = dataReader["PostFromAddress"].ToString();
            ojb = dataReader["PostTo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PostTo = (bool)ojb;
            }
            model.PostToAddress = dataReader["PostToAddress"].ToString();
            ojb = dataReader["PostTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PostTotal = (decimal)ojb;
                model.Total += (decimal)ojb;
            }
            model.PoContext = dataReader["PoContext"].ToString();
            ojb = dataReader["PoTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PoTotal = (decimal)ojb;
                model.Total += (decimal)ojb;
            }
            model.OtherContext = dataReader["OtherContext"].ToString();
            ojb = dataReader["OtherTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OtherTotal = (decimal)ojb;
                model.Total += (decimal)ojb;
            }

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UserName = ojb.ToString();
            }


            ojb = dataReader["BusRemark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BusRemark = ojb.ToString();
            }


            ojb = dataReader["RepastRemark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RepastRemark = ojb.ToString();
            }


            ojb = dataReader["HotelRemark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HotelRemark = ojb.ToString();
            }

            ojb = dataReader["OilRemark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OilRemark = ojb.ToString();
            }


            ojb = dataReader["GuoRemark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuoRemark = ojb.ToString();
            }
            ojb = dataReader["PostRemark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PostRemark = ojb.ToString();
            }
            ojb = dataReader["PoRemark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PoRemark = ojb.ToString();
            }

            ojb = dataReader["OtherRemark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OtherRemark = ojb.ToString();
            }
            ojb = dataReader["PostNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PostNo = ojb.ToString();
            }
            ojb = dataReader["PostCompany"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PostCompany = ojb.ToString();
            }
            ojb = dataReader["PostContext"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PostContext = ojb.ToString();
            }
            ojb = dataReader["PostToPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PostToPer = ojb.ToString();
            }
            
            model.PostToPer = dataReader["PostToPer"].ToString();


            ojb = dataReader["Post_Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Post_Id = (int)ojb;
            }
          

            ojb = dataReader["Post_No"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Post_No = ojb.ToString();
            }


           
            ojb = dataReader["PoNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PoNo = ojb.ToString();
            }


            
            ojb = dataReader["PoName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PoName = ojb.ToString();
            }


          
            ojb = dataReader["GuestName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestName = ojb.ToString();
            }



            ojb = dataReader["CaiPoNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CaiPoNo = ojb.ToString();
            }

            ojb = dataReader["CaiId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CaiId =Convert.ToInt32(ojb);
            }
            ojb = dataReader["State"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.State = ojb.ToString();
            }

            model.BusFPNO = dataReader["BusFPNO"].ToString();
            model.RepastFPNO = dataReader["RepastFPNO"].ToString();
            model.HotelFPNO = dataReader["HotelFPNO"].ToString();
            model.OilFPNO = dataReader["OilFPNO"].ToString();
            model.GuoBeginFPNO = dataReader["GuoBeginFPNO"].ToString();
            model.PostFPNO = dataReader["PostFPNO"].ToString();
            model.OtherFPNO = dataReader["OtherFPNO"].ToString();
            model.CaiFPNO = dataReader["CaiFPNO"].ToString(); 

            return model;
        }



        public List<Tb_DispatchList> GetListArrayReport(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select Tb_DispatchList.id,Tb_DispatchList.CardNo,Tb_DispatchList.CreateTime,Tb_DispatchList.PONo,Tb_DispatchList.POName,Tb_DispatchList.GuestName,AE,
TB_Company.ComName,BusTotal,RepastTotal,HotelTotal,OilTotal,GuoTotal,Tb_DispatchList.PostTotal,Tb_DispatchList.PoTotal,OtherTotal,Post_No,BusFPNO,RepastFPNO,HotelFPNO,OilFPNO,GuoBeginFPNO,PostFPNO,
OtherFPNO,CaiFPNO,PostNo
from Tb_DispatchList left join tb_User on tb_User.id=Tb_DispatchList.UserId
left join TB_Company on TB_Company.ComCode=tb_User.CompanyCode
left join CG_POOrder on CG_POOrder.PONo=Tb_DispatchList.PONo AND IFZhui=0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Tb_DispatchList.id desc");
            List<VAN_OA.Model.EFrom.Tb_DispatchList> list = new List<VAN_OA.Model.EFrom.Tb_DispatchList>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Tb_DispatchList model = new Tb_DispatchList();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        ojb = dataReader["AE"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AE = ojb.ToString();
                        }
                        ojb = dataReader["PostNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Post_No = ojb.ToString();
                        }
                        
                        ojb = dataReader["ComName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ComName = ojb.ToString();
                        }
                        ojb = dataReader["CreateTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreateTime = (DateTime)ojb;
                        }
                        model.CardNo = dataReader["CardNo"].ToString();
                        
                        ojb = dataReader["BusTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.BusTotal = (decimal)ojb;
                            model.Total += (decimal)ojb;

                            ojb = dataReader["BusFPNO"];
                            if (ojb != null && ojb != DBNull.Value&& ojb.ToString()!="")
                            {
                               
                                model.DispatchType += string.Format("公交费[<span style='color:Red;'>{0}</span>]{1}|", ojb,model.BusTotal);
                            }
                            else
                            {
                                model.DispatchType += string.Format("公交费{0}|", model.BusTotal);
                            }                                
                        }
                          
                      
                        ojb = dataReader["RepastTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.RepastTotal = (decimal)ojb;
                            model.Total += (decimal)ojb;
                            //model.DispatchType += string.Format("餐饮费{0}|", model.RepastTotal);

                            ojb = dataReader["RepastFPNO"];
                            if (ojb != null && ojb != DBNull.Value && ojb.ToString() != "")
                            {
                                model.DispatchType += string.Format("餐饮费[<span style='color:Red;'>{0}</span>]{1}|", ojb, model.RepastTotal);
                            }
                            else
                            {
                                model.DispatchType += string.Format("餐饮费{0}|", model.RepastTotal);
                            }
                        }
                       
                        ojb = dataReader["HotelTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.HotelTotal = (decimal)ojb;
                            model.Total += (decimal)ojb;
                            //model.DispatchType += string.Format("住宿费{0}|", model.HotelTotal);

                            ojb = dataReader["HotelFPNO"];
                            if (ojb != null && ojb != DBNull.Value && ojb.ToString() != "")
                            {
                                model.DispatchType += string.Format("住宿费[<span style='color:Red;'>{0}</span>]{1}|", ojb, model.HotelTotal);
                            }
                            else
                            {
                                model.DispatchType += string.Format("住宿费{0}|", model.HotelTotal);
                            }
                        }                   
                       
                        ojb = dataReader["OilTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.OilTotal = (decimal)ojb;
                            model.Total += (decimal)ojb;
                            //model.DispatchType += string.Format("汽油补贴{0}|", model.OilTotal);

                            ojb = dataReader["OilFPNO"];
                            if (ojb != null && ojb != DBNull.Value && ojb.ToString() != "")
                            {
                                model.DispatchType += string.Format("汽油补贴[<span style='color:Red;'>{0}</span>]{1}|", ojb, model.OilTotal);
                            }
                            else
                            {
                                model.DispatchType += string.Format("汽油补贴{0}|", model.OilTotal);
                            }

                        }
                        
                        ojb = dataReader["GuoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuoTotal = (decimal)ojb;
                            model.Total += (decimal)ojb;
                            //model.DispatchType += string.Format("过路费{0}|", model.GuoTotal);

                            ojb = dataReader["GuoBeginFPNO"];
                            if (ojb != null && ojb != DBNull.Value && ojb.ToString() != "")
                            {
                                model.DispatchType += string.Format("过路费[<span style='color:Red;'>{0}</span>]{1}|", ojb, model.GuoTotal);
                            }
                            else
                            {
                                model.DispatchType += string.Format("过路费{0}|", model.GuoTotal);
                            }
                        }                      
                        
                      
                        ojb = dataReader["PostTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PostTotal = (decimal)ojb;
                            model.Total += (decimal)ojb;
                            //model.DispatchType += string.Format("邮寄费{0}|", model.PostTotal);

                            ojb = dataReader["PostFPNO"];
                            if (ojb != null && ojb != DBNull.Value && ojb.ToString() != "")
                            {
                                model.DispatchType += string.Format("邮寄费[<span style='color:Red;'>{0}</span>]{1}|", ojb, model.PostTotal);
                            }
                            else
                            {
                                model.DispatchType += string.Format("邮寄费{0}|", model.PostTotal);
                            }
                        }
                       
                        ojb = dataReader["PoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PoTotal = (decimal)ojb;
                            model.Total += (decimal)ojb;
                            //model.DispatchType += string.Format("小额采购{0}|", model.PoTotal);

                            ojb = dataReader["CaiFPNO"];
                            if (ojb != null && ojb != DBNull.Value && ojb.ToString() != "")
                            {
                                model.DispatchType += string.Format("小额采购[<span style='color:Red;'>{0}</span>]{1}|", ojb, model.PoTotal);
                            }
                            else
                            {
                                model.DispatchType += string.Format("小额采购{0}|", model.PoTotal);
                            }
                        }
                     
                        ojb = dataReader["OtherTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.OtherTotal = (decimal)ojb;
                            model.Total += (decimal)ojb;
                            //model.DispatchType += string.Format("其他费用{0}|", model.OtherTotal);

                            ojb = dataReader["OtherFPNO"];
                            if (ojb != null && ojb != DBNull.Value && ojb.ToString() != "")
                            {
                                model.DispatchType += string.Format("其他费用[<span style='color:Red;'>{0}</span>]{1}|", ojb, model.OtherTotal);
                            }
                            else
                            {
                                model.DispatchType += string.Format("其他费用{0}|", model.OtherTotal);
                            }
                        }

                        if (model.DispatchType != null)
                        {
                            model.DispatchType = model.DispatchType.Trim('|');
                        }
                        ojb = dataReader["PoNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PoNo = ojb.ToString();
                        } 

                        ojb = dataReader["PoName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PoName = ojb.ToString();
                        } 

                        ojb = dataReader["GuestName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestName = ojb.ToString();
                        } 
                        list.Add(model);
                    }
                }
            }
            return list;
        }
    }
}
