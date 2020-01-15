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
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using VAN_OA.Model.ReportForms;


namespace VAN_OA.Dal.ReportForms
{
    public class TB_GuestTrackService
    {
        public bool updateTran(VAN_OA.Model.ReportForms.TB_GuestTrack model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
        public int addTran(VAN_OA.Model.ReportForms.TB_GuestTrack model, VAN_OA.Model.EFrom.tb_EForm eform)
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

                    string proNo = eformSer.GetAllE_No("TB_GuestTrack", objCommand);
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
        public string AddToString(TB_GuestTrack model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Time != null)
            {
                strSql1.Append("Time,");
                strSql2.Append("'" + model.Time + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.SimpGuestName != null)
            {
                strSql1.Append("SimpGuestName,");
                strSql2.Append("'" + model.SimpGuestName + "',");
            }
            if (model.Phone != null)
            {
                strSql1.Append("Phone,");
                strSql2.Append("'" + model.Phone + "',");
            }
            if (model.LikeMan != null)
            {
                strSql1.Append("LikeMan,");
                strSql2.Append("'" + model.LikeMan + "',");
            }
            if (model.Job != null)
            {
                strSql1.Append("Job,");
                strSql2.Append("'" + model.Job + "',");
            }
            if (model.FoxOrEmail != null)
            {
                strSql1.Append("FoxOrEmail,");
                strSql2.Append("'" + model.FoxOrEmail + "',");
            }
            if (model.IfSave != null)
            {
                strSql1.Append("IfSave,");
                strSql2.Append("" + (model.IfSave ? 1 : 0) + ",");
            }
            if (model.QQMsn != null)
            {
                strSql1.Append("QQMsn,");
                strSql2.Append("'" + model.QQMsn + "',");
            }
            if (model.FristMeet != null)
            {
                strSql1.Append("FristMeet,");
                strSql2.Append("'" + model.FristMeet + "',");
            }
            if (model.SecondMeet != null)
            {
                strSql1.Append("SecondMeet,");
                strSql2.Append("'" + model.SecondMeet + "',");
            }
            if (model.FaceMeet != null)
            {
                strSql1.Append("FaceMeet,");
                strSql2.Append("'" + model.FaceMeet + "',");
            }
            if (model.Price != null)
            {
                strSql1.Append("Price,");
                strSql2.Append("" + model.Price + ",");
            }
            if (model.IfSuccess != null)
            {
                strSql1.Append("IfSuccess,");
                strSql2.Append("" + (model.IfSuccess ? 1 : 0) + ",");
            }
            if (model.MyAppraise != null)
            {
                strSql1.Append("MyAppraise,");
                strSql2.Append("'" + model.MyAppraise + "',");
            }
            if (model.ManAppraise != null)
            {
                strSql1.Append("ManAppraise,");
                strSql2.Append("'" + model.ManAppraise + "',");
            }
            if (model.CreateUser != null)
            {
                strSql1.Append("CreateUser,");
                strSql2.Append("" + model.CreateUser + ",");
            }
            if (model.CreateTime != null)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("'" + model.CreateTime + "',");
            }
            if (model.GuestId != null)
            {
                strSql1.Append("GuestId,");
                strSql2.Append("'" + model.GuestId + "',");
            }
            if (model.GuestAddress != null)
            {
                strSql1.Append("GuestAddress,");
                strSql2.Append("'" + model.GuestAddress + "',");
            }
            if (model.GuestHttp != null)
            {
                strSql1.Append("GuestHttp,");
                strSql2.Append("'" + model.GuestHttp + "',");
            }
            if (model.GuestShui != null)
            {
                strSql1.Append("GuestShui,");
                strSql2.Append("'" + model.GuestShui + "',");
            }
            if (model.GuestGong != null)
            {
                strSql1.Append("GuestGong,");
                strSql2.Append("'" + model.GuestGong + "',");
            }
            if (model.GuestBrandNo != null)
            {
                strSql1.Append("GuestBrandNo,");
                strSql2.Append("'" + model.GuestBrandNo + "',");
            }
            if (model.GuestBrandName != null)
            {
                strSql1.Append("GuestBrandName,");
                strSql2.Append("'" + model.GuestBrandName + "',");
            }
            if (model.AE != null)
            {
                strSql1.Append("AE,");
                strSql2.Append("" + model.AE + ",");
            }
            if (model.INSIDE != null)
            {
                strSql1.Append("INSIDE,");
                strSql2.Append("" + model.INSIDE + ",");
            }
            if (model.GuestTotal != null)
            {
                strSql1.Append("GuestTotal,");
                strSql2.Append("" + model.GuestTotal + ",");
            }
            if (model.GuestLiRun != null)
            {
                strSql1.Append("GuestLiRun,");
                strSql2.Append("" + model.GuestLiRun + ",");
            }
            if (model.GuestDays != null)
            {
                strSql1.Append("GuestDays,");
                strSql2.Append("" + model.GuestDays + ",");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            if (model.INSIDERemark != null)
            {
                strSql1.Append("INSIDERemark,");
                strSql2.Append("'" + model.INSIDERemark + "',");
            }
            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }
            if (model.YearNo != null)
            {
                strSql1.Append("YearNo,");
                strSql2.Append("'" + model.YearNo + "',");
            }
            if (model.QuartNo != null)
            {
                strSql1.Append("QuartNo,");
                strSql2.Append("'" + model.QuartNo + "',");
            }
            if (model.AEPer != null)
            {
                strSql1.Append("AEPer,");
                strSql2.Append("" + model.AEPer + ",");
            }
            if (model.INSIDEPer != null)
            {
                strSql1.Append("INSIDEPer,");
                strSql2.Append("" + model.INSIDEPer + ",");
            }
            if (model.IsSpecial != null)
            {
                strSql1.Append("IsSpecial,");
                strSql2.Append("" + (model.IsSpecial ? 1 : 0) + ",");
            }

            if (model.MyGuestType != null)
            {
                strSql1.Append("MyGuestType,");
                strSql2.Append("'" + model.MyGuestType + "',");
            }
            if (model.MyGuestPro != null)
            {
                strSql1.Append("MyGuestPro,");
                strSql2.Append("" + model.MyGuestPro + ",");
            }
            strSql.Append("insert into TB_GuestTrack(");
            strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
            strSql.Append(")");
            return strSql.ToString();
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.ReportForms.TB_GuestTrack model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Time != null)
            {
                strSql1.Append("Time,");
                strSql2.Append("'" + model.Time + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.SimpGuestName != null)
            {
                strSql1.Append("SimpGuestName,");
                strSql2.Append("'" + model.SimpGuestName + "',");
            }
            if (model.Phone != null)
            {
                strSql1.Append("Phone,");
                strSql2.Append("'" + model.Phone + "',");
            }
            if (model.LikeMan != null)
            {
                strSql1.Append("LikeMan,");
                strSql2.Append("'" + model.LikeMan + "',");
            }
            if (model.Job != null)
            {
                strSql1.Append("Job,");
                strSql2.Append("'" + model.Job + "',");
            }
            if (model.FoxOrEmail != null)
            {
                strSql1.Append("FoxOrEmail,");
                strSql2.Append("'" + model.FoxOrEmail + "',");
            }
            if (model.IfSave != null)
            {
                strSql1.Append("IfSave,");
                strSql2.Append("" + (model.IfSave ? 1 : 0) + ",");
            }
            if (model.QQMsn != null)
            {
                strSql1.Append("QQMsn,");
                strSql2.Append("'" + model.QQMsn + "',");
            }
            if (model.FristMeet != null)
            {
                strSql1.Append("FristMeet,");
                strSql2.Append("'" + model.FristMeet + "',");
            }
            if (model.SecondMeet != null)
            {
                strSql1.Append("SecondMeet,");
                strSql2.Append("'" + model.SecondMeet + "',");
            }
            if (model.FaceMeet != null)
            {
                strSql1.Append("FaceMeet,");
                strSql2.Append("'" + model.FaceMeet + "',");
            }
            if (model.Price != null)
            {
                strSql1.Append("Price,");
                strSql2.Append("" + model.Price + ",");
            }
            if (model.IfSuccess != null)
            {
                strSql1.Append("IfSuccess,");
                strSql2.Append("" + (model.IfSuccess ? 1 : 0) + ",");
            }
            if (model.MyAppraise != null)
            {
                strSql1.Append("MyAppraise,");
                strSql2.Append("'" + model.MyAppraise + "',");
            }
            if (model.ManAppraise != null)
            {
                strSql1.Append("ManAppraise,");
                strSql2.Append("'" + model.ManAppraise + "',");
            }
            if (model.CreateUser != null)
            {
                strSql1.Append("CreateUser,");
                strSql2.Append("" + model.CreateUser + ",");
            }
            if (model.CreateTime != null)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("'" + model.CreateTime + "',");
            }


            if (model.GuestAddress != null)
            {
                strSql1.Append("GuestAddress,");
                strSql2.Append("'" + model.GuestAddress + "',");
            }
            if (model.GuestHttp != null)
            {
                strSql1.Append("GuestHttp,");
                strSql2.Append("'" + model.GuestHttp + "',");
            }
            if (model.GuestShui != null)
            {
                strSql1.Append("GuestShui,");
                strSql2.Append("'" + model.GuestShui + "',");
            }
            if (model.GuestGong != null)
            {
                strSql1.Append("GuestGong,");
                strSql2.Append("'" + model.GuestGong + "',");
            }
            if (model.GuestBrandNo != null)
            {
                strSql1.Append("GuestBrandNo,");
                strSql2.Append("'" + model.GuestBrandNo + "',");
            }
            if (model.GuestBrandName != null)
            {
                strSql1.Append("GuestBrandName,");
                strSql2.Append("'" + model.GuestBrandName + "',");
            }
            if (model.AE != null)
            {
                strSql1.Append("AE,");
                strSql2.Append("" + model.AE + ",");
            }
            if (model.INSIDE != null)
            {
                strSql1.Append("INSIDE,");
                strSql2.Append("" + model.INSIDE + ",");
            }
            if (model.GuestTotal != null)
            {
                strSql1.Append("GuestTotal,");
                strSql2.Append("" + model.GuestTotal + ",");
            }
            if (model.GuestLiRun != null)
            {
                strSql1.Append("GuestLiRun,");
                strSql2.Append("" + model.GuestLiRun + ",");
            }
            if (model.GuestDays != null)
            {
                strSql1.Append("GuestDays,");
                strSql2.Append("" + model.GuestDays + ",");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }

            if (model.INSIDERemark != null)
            {
                strSql1.Append("INSIDERemark,");
                strSql2.Append("'" + model.INSIDERemark + "',");
            }
            if (model.YearNo != null)
            {
                strSql1.Append("YearNo,");
                strSql2.Append("'" + model.YearNo + "',");
            }
            if (model.QuartNo != null)
            {
                strSql1.Append("QuartNo,");
                strSql2.Append("'" + model.QuartNo + "',");
            }

            if (model.AEPer != null)
            {
                strSql1.Append("AEPer,");
                strSql2.Append("" + model.AEPer + ",");
            }
            if (model.INSIDEPer != null)
            {
                strSql1.Append("INSIDEPer,");
                strSql2.Append("" + model.INSIDEPer + ",");
            }

            if (model.IsSpecial != null)
            {
                strSql1.Append("IsSpecial,");
                strSql2.Append("" + (model.IsSpecial ? 1 : 0) + ",");
            }
            if (model.MyGuestType != null)
            {
                strSql1.Append("MyGuestType,");
                strSql2.Append("'" + model.MyGuestType + "',");
            }
            if (model.MyGuestPro != null)
            {
                strSql1.Append("MyGuestPro,");
                strSql2.Append("" + model.MyGuestPro + ",");
            }

            string MaxGuestId = "";
            string sqlGuestId = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(GuestId),4))+1))),4) FROM TB_GuestTrack;");

            object objMaxGuest = DBHelp.ExeScalar(sqlGuestId);
            if (objMaxGuest != null && objMaxGuest.ToString() != "")
            {
                MaxGuestId = objMaxGuest.ToString();
            }
            else
            {
                MaxGuestId = "0001";
            }

            strSql1.Append("GuestId,");
            strSql2.Append("'" + MaxGuestId + "',");

            strSql1.Append("ProNo,");

            //
            //string MaxCardNo = "";
            //string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(ProNo),4))+1))),4) FROM TB_GuestTrack;");

            //object objMax = DBHelp.ExeScalar(sql);
            //if (objMax != null && objMax.ToString() != "")
            //{
            //    MaxCardNo = DateTime.Now.Year.ToString() + objMax.ToString();
            //}
            //else
            //{
            //    MaxCardNo = DateTime.Now.Year.ToString() + "0001";
            //}
            strSql2.Append("'" + model.ProNo + "',");

            //
            strSql.Append("insert into TB_GuestTrack(");
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
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.ReportForms.TB_GuestTrack model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Time != null)
            {
                strSql1.Append("Time,");
                strSql2.Append("'" + model.Time + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.SimpGuestName != null)
            {
                strSql1.Append("SimpGuestName,");
                strSql2.Append("'" + model.SimpGuestName + "',");
            }
            if (model.Phone != null)
            {
                strSql1.Append("Phone,");
                strSql2.Append("'" + model.Phone + "',");
            }
            if (model.LikeMan != null)
            {
                strSql1.Append("LikeMan,");
                strSql2.Append("'" + model.LikeMan + "',");
            }
            if (model.Job != null)
            {
                strSql1.Append("Job,");
                strSql2.Append("'" + model.Job + "',");
            }
            if (model.FoxOrEmail != null)
            {
                strSql1.Append("FoxOrEmail,");
                strSql2.Append("'" + model.FoxOrEmail + "',");
            }
            if (model.IfSave != null)
            {
                strSql1.Append("IfSave,");
                strSql2.Append("" + (model.IfSave ? 1 : 0) + ",");
            }
            if (model.QQMsn != null)
            {
                strSql1.Append("QQMsn,");
                strSql2.Append("'" + model.QQMsn + "',");
            }
            if (model.FristMeet != null)
            {
                strSql1.Append("FristMeet,");
                strSql2.Append("'" + model.FristMeet + "',");
            }
            if (model.SecondMeet != null)
            {
                strSql1.Append("SecondMeet,");
                strSql2.Append("'" + model.SecondMeet + "',");
            }
            if (model.FaceMeet != null)
            {
                strSql1.Append("FaceMeet,");
                strSql2.Append("'" + model.FaceMeet + "',");
            }
            if (model.Price != null)
            {
                strSql1.Append("Price,");
                strSql2.Append("" + model.Price + ",");
            }
            if (model.IfSuccess != null)
            {
                strSql1.Append("IfSuccess,");
                strSql2.Append("" + (model.IfSuccess ? 1 : 0) + ",");
            }
            if (model.MyAppraise != null)
            {
                strSql1.Append("MyAppraise,");
                strSql2.Append("'" + model.MyAppraise + "',");
            }
            if (model.ManAppraise != null)
            {
                strSql1.Append("ManAppraise,");
                strSql2.Append("'" + model.ManAppraise + "',");
            }
            if (model.CreateUser != null)
            {
                strSql1.Append("CreateUser,");
                strSql2.Append("" + model.CreateUser + ",");
            }
            if (model.CreateTime != null)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("'" + model.CreateTime + "',");
            }


            if (model.GuestAddress != null)
            {
                strSql1.Append("GuestAddress,");
                strSql2.Append("'" + model.GuestAddress + "',");
            }
            if (model.GuestHttp != null)
            {
                strSql1.Append("GuestHttp,");
                strSql2.Append("'" + model.GuestHttp + "',");
            }
            if (model.GuestShui != null)
            {
                strSql1.Append("GuestShui,");
                strSql2.Append("'" + model.GuestShui + "',");
            }
            if (model.GuestGong != null)
            {
                strSql1.Append("GuestGong,");
                strSql2.Append("'" + model.GuestGong + "',");
            }
            if (model.GuestBrandNo != null)
            {
                strSql1.Append("GuestBrandNo,");
                strSql2.Append("'" + model.GuestBrandNo + "',");
            }
            if (model.GuestBrandName != null)
            {
                strSql1.Append("GuestBrandName,");
                strSql2.Append("'" + model.GuestBrandName + "',");
            }
            if (model.AE != null)
            {
                strSql1.Append("AE,");
                strSql2.Append("" + model.AE + ",");
            }
            if (model.INSIDE != null)
            {
                strSql1.Append("INSIDE,");
                strSql2.Append("" + model.INSIDE + ",");
            }
            if (model.GuestTotal != null)
            {
                strSql1.Append("GuestTotal,");
                strSql2.Append("" + model.GuestTotal + ",");
            }
            if (model.GuestLiRun != null)
            {
                strSql1.Append("GuestLiRun,");
                strSql2.Append("" + model.GuestLiRun + ",");
            }
            if (model.GuestDays != null)
            {
                strSql1.Append("GuestDays,");
                strSql2.Append("" + model.GuestDays + ",");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }

            if (model.INSIDERemark != null)
            {
                strSql1.Append("INSIDERemark,");
                strSql2.Append("'" + model.INSIDERemark + "',");
            }
            if (model.YearNo != null)
            {
                strSql1.Append("YearNo,");
                strSql2.Append("'" + model.YearNo + "',");
            }
            if (model.QuartNo != null)
            {
                strSql1.Append("QuartNo,");
                strSql2.Append("'" + model.QuartNo + "',");
            }

            if (model.AEPer != null)
            {
                strSql1.Append("AEPer,");
                strSql2.Append("" + model.AEPer + ",");
            }
            if (model.INSIDEPer != null)
            {
                strSql1.Append("INSIDEPer,");
                strSql2.Append("" + model.INSIDEPer + ",");
            }

            if (model.IsSpecial != null)
            {
                strSql1.Append("IsSpecial,");
                strSql2.Append("" + (model.IsSpecial ? 1 : 0) + ",");
            }

            //string MaxGuestId = "";
            //string sqlGuestId = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(GuestId),4))+1))),4) FROM TB_GuestTrack;");

            //object objMaxGuest = DBHelp.ExeScalar(sqlGuestId);
            //if (objMaxGuest != null && objMaxGuest.ToString() != "")
            //{
            //    MaxGuestId = objMaxGuest.ToString();
            //}
            //else
            //{
            //    MaxGuestId = "0001";
            //}

            strSql1.Append("GuestId,");
            strSql2.Append("'" + model.GuestId + "',");

            strSql1.Append("ProNo,");

            //
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(ProNo),4))+1))),4) FROM TB_GuestTrack;");

            object objMax = DBHelp.ExeScalar(sql);
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = DateTime.Now.Year.ToString() + objMax.ToString();
            }
            else
            {
                MaxCardNo = DateTime.Now.Year.ToString() + "0001";
            }
            strSql2.Append("'" + MaxCardNo + "',");


            if (model.MyGuestType != null)
            {
                strSql1.Append("MyGuestType,");
                strSql2.Append("'" + model.MyGuestType + "',");
            }
            if (model.MyGuestPro != null)
            {
                strSql1.Append("MyGuestPro,");
                strSql2.Append("" + model.MyGuestPro + ",");
            }

            //
            strSql.Append("insert into TB_GuestTrack(");
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
        public bool Update(VAN_OA.Model.ReportForms.TB_GuestTrack model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_GuestTrack set ");
            if (model.Time != null)
            {
                strSql.Append("Time='" + model.Time + "',");
            }
            if (model.GuestName != null)
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            if (model.SimpGuestName != null)
            {
                strSql.Append("SimpGuestName='" + model.SimpGuestName + "',");
            }
            if (model.Phone != null)
            {
                strSql.Append("Phone='" + model.Phone + "',");
            }
            if (model.LikeMan != null)
            {
                strSql.Append("LikeMan='" + model.LikeMan + "',");
            }
            if (model.Job != null)
            {
                strSql.Append("Job='" + model.Job + "',");
            }
            if (model.FoxOrEmail != null)
            {
                strSql.Append("FoxOrEmail='" + model.FoxOrEmail + "',");
            }
            if (model.IfSave != null)
            {
                strSql.Append("IfSave=" + (model.IfSave ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("IfSave= null ,");
            }
            if (model.QQMsn != null)
            {
                strSql.Append("QQMsn='" + model.QQMsn + "',");
            }
            else
            {
                strSql.Append("QQMsn= null ,");
            }
            //if (model.FristMeet != null)
            //{
            //    strSql.Append("FristMeet='" + model.FristMeet + "',");
            //}
            //else
            //{
            //    strSql.Append("FristMeet= null ,");
            //}
            //if (model.SecondMeet != null)
            //{
            //    strSql.Append("SecondMeet='" + model.SecondMeet + "',");
            //}
            //else
            //{
            //    strSql.Append("SecondMeet= null ,");
            //}
            //if (model.FaceMeet != null)
            //{
            //    strSql.Append("FaceMeet='" + model.FaceMeet + "',");
            //}
            //else
            //{
            //    strSql.Append("FaceMeet= null ,");
            //}
            //if (model.Price != null)
            //{
            //    strSql.Append("Price=" + model.Price + ",");
            //}
            //else
            //{
            //    strSql.Append("Price= null ,");
            //}
            //if (model.IfSuccess != null)
            //{
            //    strSql.Append("IfSuccess=" + (model.IfSuccess ? 1 : 0) + ",");
            //}
            //else
            //{
            //    strSql.Append("IfSuccess= null ,");
            //}
            //if (model.MyAppraise != null)
            //{
            //    strSql.Append("MyAppraise='" + model.MyAppraise + "',");
            //}
            //else
            //{
            //    strSql.Append("MyAppraise= null ,");
            //}
            //if (model.ManAppraise != null)
            //{
            //    strSql.Append("ManAppraise='" + model.ManAppraise + "',");
            //}
            //else
            //{
            //    strSql.Append("ManAppraise= null ,");
            //}
            //if (model.CreateUser != null)
            //{
            //    strSql.Append("CreateUser=" + model.CreateUser + ",");
            //}
            //if (model.CreateTime != null)
            //{
            //    strSql.Append("CreateTime='" + model.CreateTime + "',");
            //}

            if (model.GuestId != null)
            {
                strSql.Append("GuestId='" + model.GuestId + "',");
            }
            else
            {
                strSql.Append("GuestId= null ,");
            }
            if (model.GuestAddress != null)
            {
                strSql.Append("GuestAddress='" + model.GuestAddress + "',");
            }
            else
            {
                strSql.Append("GuestAddress= null ,");
            }
            if (model.GuestHttp != null)
            {
                strSql.Append("GuestHttp='" + model.GuestHttp + "',");
            }
            else
            {
                strSql.Append("GuestHttp= null ,");
            }
            if (model.GuestShui != null)
            {
                strSql.Append("GuestShui='" + model.GuestShui + "',");
            }
            else
            {
                strSql.Append("GuestShui= null ,");
            }
            if (model.GuestGong != null)
            {
                strSql.Append("GuestGong='" + model.GuestGong + "',");
            }
            else
            {
                strSql.Append("GuestGong= null ,");
            }
            if (model.GuestBrandNo != null)
            {
                strSql.Append("GuestBrandNo='" + model.GuestBrandNo + "',");
            }
            else
            {
                strSql.Append("GuestBrandNo= null ,");
            }
            if (model.GuestBrandName != null)
            {
                strSql.Append("GuestBrandName='" + model.GuestBrandName + "',");
            }
            else
            {
                strSql.Append("GuestBrandName= null ,");
            }
            if (model.AE != null)
            {
                strSql.Append("AE=" + model.AE + ",");
            }
            else
            {
                strSql.Append("AE= null ,");
            }
            if (model.INSIDE != null)
            {
                strSql.Append("INSIDE=" + model.INSIDE + ",");
            }
            else
            {
                strSql.Append("INSIDE= null ,");
            }
            if (model.GuestTotal != null)
            {
                strSql.Append("GuestTotal=" + model.GuestTotal + ",");
            }
            else
            {
                strSql.Append("GuestTotal= null ,");
            }
            if (model.GuestLiRun != null)
            {
                strSql.Append("GuestLiRun=" + model.GuestLiRun + ",");
            }
            else
            {
                strSql.Append("GuestLiRun= null ,");
            }
            if (model.GuestDays != null)
            {
                strSql.Append("GuestDays=" + model.GuestDays + ",");
            }
            else
            {
                strSql.Append("GuestDays= null ,");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }
            if (model.INSIDERemark != null)
            {
                strSql.Append("INSIDERemark='" + model.INSIDERemark + "',");
            }
            else
            {
                strSql.Append("INSIDERemark= null ,");
            }

            if (model.YearNo != null)
            {
                strSql.Append("YearNo='" + model.YearNo + "',");
            }
            else
            {
                strSql.Append("YearNo= null ,");
            }
            if (model.QuartNo != null)
            {
                strSql.Append("QuartNo='" + model.QuartNo + "',");
            }
            else
            {
                strSql.Append("QuartNo= null ,");
            }

            if (model.AEPer != null)
            {
                strSql.Append("AEPer=" + model.AEPer + ",");
            }
            else
            {
                strSql.Append("AEPer= null ,");
            }
            if (model.INSIDEPer != null)
            {
                strSql.Append("INSIDEPer=" + model.INSIDEPer + ",");
            }
            else
            {
                strSql.Append("INSIDEPer= null ,");
            }

            if (model.MyGuestType != null)
            {
                strSql.Append("MyGuestType='" + model.MyGuestType + "',");
            }
            if (model.MyGuestPro != null)
            {
                strSql.Append("MyGuestPro=" + model.MyGuestPro + ",");
            }

            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        public string UpdateToString(VAN_OA.Model.ReportForms.TB_GuestTrack model, string guestId, string yearNo, string QuartNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_GuestTrack set ");
            //if (model.Time != null)
            //{
            //    strSql.Append("Time='" + model.Time + "',");
            //}
            if (model.GuestName != null)
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            if (model.SimpGuestName != null)
            {
                strSql.Append("SimpGuestName='" + model.SimpGuestName + "',");
            }
            if (model.Phone != null)
            {
                strSql.Append("Phone='" + model.Phone + "',");
            }
            if (model.LikeMan != null)
            {
                strSql.Append("LikeMan='" + model.LikeMan + "',");
            }
            if (model.Job != null)
            {
                strSql.Append("Job='" + model.Job + "',");
            }
            if (model.FoxOrEmail != null)
            {
                strSql.Append("FoxOrEmail='" + model.FoxOrEmail + "',");
            }
            if (model.IfSave != null)
            {
                strSql.Append("IfSave=" + (model.IfSave ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("IfSave= null ,");
            }
            if (model.QQMsn != null)
            {
                strSql.Append("QQMsn='" + model.QQMsn + "',");
            }
            else
            {
                strSql.Append("QQMsn= null ,");
            }
            //if (model.FristMeet != null)
            //{
            //    strSql.Append("FristMeet='" + model.FristMeet + "',");
            //}
            //else
            //{
            //    strSql.Append("FristMeet= null ,");
            //}
            //if (model.SecondMeet != null)
            //{
            //    strSql.Append("SecondMeet='" + model.SecondMeet + "',");
            //}
            //else
            //{
            //    strSql.Append("SecondMeet= null ,");
            //}
            //if (model.FaceMeet != null)
            //{
            //    strSql.Append("FaceMeet='" + model.FaceMeet + "',");
            //}
            //else
            //{
            //    strSql.Append("FaceMeet= null ,");
            //}
            //if (model.Price != null)
            //{
            //    strSql.Append("Price=" + model.Price + ",");
            //}
            //else
            //{
            //    strSql.Append("Price= null ,");
            //}
            //if (model.IfSuccess != null)
            //{
            //    strSql.Append("IfSuccess=" + (model.IfSuccess ? 1 : 0) + ",");
            //}
            //else
            //{
            //    strSql.Append("IfSuccess= null ,");
            //}
            //if (model.MyAppraise != null)
            //{
            //    strSql.Append("MyAppraise='" + model.MyAppraise + "',");
            //}
            //else
            //{
            //    strSql.Append("MyAppraise= null ,");
            //}
            //if (model.ManAppraise != null)
            //{
            //    strSql.Append("ManAppraise='" + model.ManAppraise + "',");
            //}
            //else
            //{
            //    strSql.Append("ManAppraise= null ,");
            //}
            //if (model.CreateUser != null)
            //{
            //    strSql.Append("CreateUser=" + model.CreateUser + ",");
            //}
            //if (model.CreateTime != null)
            //{
            //    strSql.Append("CreateTime='" + model.CreateTime + "',");
            //}

            //if (model.GuestId != null)
            //{
            //    strSql.Append("GuestId='" + model.GuestId + "',");
            //}
            //else
            //{
            //    strSql.Append("GuestId= null ,");
            //}
            if (model.GuestAddress != null)
            {
                strSql.Append("GuestAddress='" + model.GuestAddress + "',");
            }
            else
            {
                strSql.Append("GuestAddress= null ,");
            }
            if (model.GuestHttp != null)
            {
                strSql.Append("GuestHttp='" + model.GuestHttp + "',");
            }
            else
            {
                strSql.Append("GuestHttp= null ,");
            }
            if (model.GuestShui != null)
            {
                strSql.Append("GuestShui='" + model.GuestShui + "',");
            }
            else
            {
                strSql.Append("GuestShui= null ,");
            }
            if (model.GuestGong != null)
            {
                strSql.Append("GuestGong='" + model.GuestGong + "',");
            }
            else
            {
                strSql.Append("GuestGong= null ,");
            }
            if (model.GuestBrandNo != null)
            {
                strSql.Append("GuestBrandNo='" + model.GuestBrandNo + "',");
            }
            else
            {
                strSql.Append("GuestBrandNo= null ,");
            }
            if (model.GuestBrandName != null)
            {
                strSql.Append("GuestBrandName='" + model.GuestBrandName + "',");
            }
            else
            {
                strSql.Append("GuestBrandName= null ,");
            }
            if (model.AE != null)
            {
                strSql.Append("AE=" + model.AE + ",");
            }
            else
            {
                strSql.Append("AE= null ,");
            }
            if (model.INSIDE != null)
            {
                strSql.Append("INSIDE=" + model.INSIDE + ",");
            }
            else
            {
                strSql.Append("INSIDE= null ,");
            }
            if (model.GuestTotal != null)
            {
                strSql.Append("GuestTotal=" + model.GuestTotal + ",");
            }
            else
            {
                strSql.Append("GuestTotal= null ,");
            }
            if (model.GuestLiRun != null)
            {
                strSql.Append("GuestLiRun=" + model.GuestLiRun + ",");
            }
            else
            {
                strSql.Append("GuestLiRun= null ,");
            }
            if (model.GuestDays != null)
            {
                strSql.Append("GuestDays=" + model.GuestDays + ",");
            }
            else
            {
                strSql.Append("GuestDays= null ,");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }
            if (model.INSIDERemark != null)
            {
                strSql.Append("INSIDERemark='" + model.INSIDERemark + "',");
            }
            else
            {
                strSql.Append("INSIDERemark= null ,");
            }

            //if (model.YearNo != null)
            //{
            //    strSql.Append("YearNo='" + model.YearNo + "',");
            //}
            //else
            //{
            //    strSql.Append("YearNo= null ,");
            //}
            //if (model.QuartNo != null)
            //{
            //    strSql.Append("QuartNo='" + model.QuartNo + "',");
            //}
            //else
            //{
            //    strSql.Append("QuartNo= null ,");
            //}

            if (model.AEPer != null)
            {
                strSql.Append("AEPer=" + model.AEPer + ",");
            }
            else
            {
                strSql.Append("AEPer= null ,");
            }
            if (model.INSIDEPer != null)
            {
                strSql.Append("INSIDEPer=" + model.INSIDEPer + ",");
            }
            else
            {
                strSql.Append("INSIDEPer= null ,");
            }
            if (model.IsSpecial != null)
            {
                strSql.Append("IsSpecial=" + (model.IsSpecial ? 1 : 0) + ",");
            }
            if (model.MyGuestType != null)
            {
                strSql.Append("MyGuestType='" + model.MyGuestType + "',");
            }
            if (model.MyGuestPro != null)
            {
                strSql.Append("MyGuestPro=" + model.MyGuestPro + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.AppendFormat(" where  guestId='{0}' and yearNo='{1}' and QuartNo='{2}'", guestId, yearNo, QuartNo);
            return strSql.ToString();
        }

        public void Update(VAN_OA.Model.ReportForms.TB_GuestTrack model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_GuestTrack set ");
            if (model.Time != null)
            {
                strSql.Append("Time='" + model.Time + "',");
            }
            if (model.GuestName != null)
            {
                strSql.Append("GuestName='" + model.GuestName + "',");
            }
            if (model.SimpGuestName != null)
            {
                strSql.Append("SimpGuestName='" + model.SimpGuestName + "',");
            }
            if (model.Phone != null)
            {
                strSql.Append("Phone='" + model.Phone + "',");
            }
            if (model.LikeMan != null)
            {
                strSql.Append("LikeMan='" + model.LikeMan + "',");
            }
            if (model.Job != null)
            {
                strSql.Append("Job='" + model.Job + "',");
            }
            if (model.FoxOrEmail != null)
            {
                strSql.Append("FoxOrEmail='" + model.FoxOrEmail + "',");
            }
            if (model.IfSave != null)
            {
                strSql.Append("IfSave=" + (model.IfSave ? 1 : 0) + ",");
            }
            else
            {
                strSql.Append("IfSave= null ,");
            }
            if (model.QQMsn != null)
            {
                strSql.Append("QQMsn='" + model.QQMsn + "',");
            }
            else
            {
                strSql.Append("QQMsn= null ,");
            }
            //if (model.FristMeet != null)
            //{
            //    strSql.Append("FristMeet='" + model.FristMeet + "',");
            //}
            //else
            //{
            //    strSql.Append("FristMeet= null ,");
            //}
            //if (model.SecondMeet != null)
            //{
            //    strSql.Append("SecondMeet='" + model.SecondMeet + "',");
            //}
            //else
            //{
            //    strSql.Append("SecondMeet= null ,");
            //}
            //if (model.FaceMeet != null)
            //{
            //    strSql.Append("FaceMeet='" + model.FaceMeet + "',");
            //}
            //else
            //{
            //    strSql.Append("FaceMeet= null ,");
            //}
            //if (model.Price != null)
            //{
            //    strSql.Append("Price=" + model.Price + ",");
            //}
            //else
            //{
            //    strSql.Append("Price= null ,");
            //}
            //if (model.IfSuccess != null)
            //{
            //    strSql.Append("IfSuccess=" + (model.IfSuccess ? 1 : 0) + ",");
            //}
            //else
            //{
            //    strSql.Append("IfSuccess= null ,");
            //}
            //if (model.MyAppraise != null)
            //{
            //    strSql.Append("MyAppraise='" + model.MyAppraise + "',");
            //}
            //else
            //{
            //    strSql.Append("MyAppraise= null ,");
            //}
            //if (model.ManAppraise != null)
            //{
            //    strSql.Append("ManAppraise='" + model.ManAppraise + "',");
            //}
            //else
            //{
            //    strSql.Append("ManAppraise= null ,");
            //}
            //if (model.CreateUser != null)
            //{
            //    strSql.Append("CreateUser=" + model.CreateUser + ",");
            //}
            //if (model.CreateTime != null)
            //{
            //    strSql.Append("CreateTime='" + model.CreateTime + "',");
            //}

            if (model.GuestId != null)
            {
                strSql.Append("GuestId='" + model.GuestId + "',");
            }
            else
            {
                strSql.Append("GuestId= null ,");
            }
            if (model.GuestAddress != null)
            {
                strSql.Append("GuestAddress='" + model.GuestAddress + "',");
            }
            else
            {
                strSql.Append("GuestAddress= null ,");
            }
            if (model.GuestHttp != null)
            {
                strSql.Append("GuestHttp='" + model.GuestHttp + "',");
            }
            else
            {
                strSql.Append("GuestHttp= null ,");
            }
            if (model.GuestShui != null)
            {
                strSql.Append("GuestShui='" + model.GuestShui + "',");
            }
            else
            {
                strSql.Append("GuestShui= null ,");
            }
            if (model.GuestGong != null)
            {
                strSql.Append("GuestGong='" + model.GuestGong + "',");
            }
            else
            {
                strSql.Append("GuestGong= null ,");
            }
            if (model.GuestBrandNo != null)
            {
                strSql.Append("GuestBrandNo='" + model.GuestBrandNo + "',");
            }
            else
            {
                strSql.Append("GuestBrandNo= null ,");
            }
            if (model.GuestBrandName != null)
            {
                strSql.Append("GuestBrandName='" + model.GuestBrandName + "',");
            }
            else
            {
                strSql.Append("GuestBrandName= null ,");
            }
            if (model.AE != null)
            {
                strSql.Append("AE=" + model.AE + ",");
            }
            else
            {
                strSql.Append("AE= null ,");
            }
            if (model.INSIDE != null)
            {
                strSql.Append("INSIDE=" + model.INSIDE + ",");
            }
            else
            {
                strSql.Append("INSIDE= null ,");
            }
            if (model.GuestTotal != null)
            {
                strSql.Append("GuestTotal=" + model.GuestTotal + ",");
            }
            else
            {
                strSql.Append("GuestTotal= null ,");
            }
            if (model.GuestLiRun != null)
            {
                strSql.Append("GuestLiRun=" + model.GuestLiRun + ",");
            }
            else
            {
                strSql.Append("GuestLiRun= null ,");
            }
            if (model.GuestDays != null)
            {
                strSql.Append("GuestDays=" + model.GuestDays + ",");
            }
            else
            {
                strSql.Append("GuestDays= null ,");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }
            if (model.INSIDERemark != null)
            {
                strSql.Append("INSIDERemark='" + model.INSIDERemark + "',");
            }
            else
            {
                strSql.Append("INSIDERemark= null ,");
            }

            if (model.YearNo != null)
            {
                strSql.Append("YearNo='" + model.YearNo + "',");
            }
            else
            {
                strSql.Append("YearNo= null ,");
            }
            if (model.QuartNo != null)
            {
                strSql.Append("QuartNo='" + model.QuartNo + "',");
            }
            else
            {
                strSql.Append("QuartNo= null ,");
            }

            if (model.AEPer != null)
            {
                strSql.Append("AEPer=" + model.AEPer + ",");
            }
            else
            {
                strSql.Append("AEPer= null ,");
            }
            if (model.INSIDEPer != null)
            {
                strSql.Append("INSIDEPer=" + model.INSIDEPer + ",");
            }
            else
            {
                strSql.Append("INSIDEPer= null ,");
            }
            if (model.IsSpecial != null)
            {
                strSql.Append("IsSpecial=" + (model.IsSpecial ? 1 : 0) + ",");
            }
            if (model.MyGuestType != null)
            {
                strSql.Append("MyGuestType='" + model.MyGuestType + "',");
            }
            if (model.MyGuestPro != null)
            {
                strSql.Append("MyGuestPro=" + model.MyGuestPro + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }


        /// 更新一条数据
        /// </summary>
        public bool UpdateINSIDE(VAN_OA.Model.ReportForms.TB_GuestTrack model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_GuestTrack set ");


            if (model.GuestTotal != null)
            {
                strSql.Append("GuestTotal=" + model.GuestTotal + ",");
            }
            else
            {
                strSql.Append("GuestTotal= null ,");
            }
            if (model.GuestLiRun != null)
            {
                strSql.Append("GuestLiRun=" + model.GuestLiRun + ",");
            }
            else
            {
                strSql.Append("GuestLiRun= null ,");
            }
            if (model.GuestDays != null)
            {
                strSql.Append("GuestDays=" + model.GuestDays + ",");
            }
            else
            {
                strSql.Append("GuestDays= null ,");
            }

            if (model.INSIDERemark != null)
            {
                strSql.Append("INSIDERemark='" + model.INSIDERemark + "',");
            }
            else
            {
                strSql.Append("INSIDERemark= null ,");
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
            strSql.Append("delete from TB_GuestTrack ");
            strSql.Append(" where Id=" + Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.ReportForms.TB_GuestTrack GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" SimpGuestName,MyGuestPro,MyGuestType,TB_GuestTrack.Id,Time,GuestName,Phone,LikeMan,Job,FoxOrEmail,IfSave,QQMsn,FristMeet,SecondMeet,FaceMeet,Price,IfSuccess,MyAppraise,ManAppraise,CreateUser,CreateTime,tb_User.loginName,YearNo,QuartNo,AEPer,INSIDEPer,IsSpecial ");
            strSql.Append(",GuestId,GuestAddress,GuestHttp,GuestShui,GuestGong,GuestBrandNo,GuestBrandName,AE,INSIDE,GuestTotal,GuestLiRun,GuestDays,Remark ,ProNo");
            strSql.Append(",AEUser.loginName as AEloginName,INSIDEUser.loginName as INSIDEloginName,INSIDERemark");
            strSql.Append(" from TB_GuestTrack left join tb_User on TB_GuestTrack.CreateUser=tb_User.ID");
            strSql.Append(" left join tb_User as AEUser on TB_GuestTrack.AE=AEUser.ID");
            strSql.Append(" left join tb_User as INSIDEUser on TB_GuestTrack.INSIDE=INSIDEUser.ID");
            strSql.Append(" where TB_GuestTrack.Id=" + Id + "");

            VAN_OA.Model.ReportForms.TB_GuestTrack model = null;
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
        public List<VAN_OA.Model.ReportForms.TB_GuestTrack> GetGuestList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (select   ");
            strSql.Append(" GuestName,GuestId ");
            strSql.Append(" from TB_GuestTrack ");
            strSql.Append(string.Format(@" where (TB_GuestTrack.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') and state='通过') or TB_GuestTrack.id not in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') ))"));
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" GROUP BY  GuestName,GuestId) as newTable order by  GuestName");
            List<VAN_OA.Model.ReportForms.TB_GuestTrack> list = new List<VAN_OA.Model.ReportForms.TB_GuestTrack>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {

                        VAN_OA.Model.ReportForms.TB_GuestTrack model = new VAN_OA.Model.ReportForms.TB_GuestTrack();
                        model.GuestName = objReader["GuestName"].ToString();
                        object ojb = objReader["GuestId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestId = ojb.ToString();
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public bool GuestIsSpecial(string strWhere)
        {
            bool isSpecial = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  TB_GuestTrack.IsSpecial ");

            strSql.Append(" from TB_GuestTrack ");
            strSql.Append(" left join tb_User as AEUser on TB_GuestTrack.AE=AEUser.ID");
            strSql.Append(" left join tb_User as INSIDEUser on TB_GuestTrack.INSIDE=INSIDEUser.ID");

            strSql.Append(string.Format(@" where (TB_GuestTrack.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') and state='通过') or TB_GuestTrack.id not in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') ))"));


            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            if (1 <= month && month <= 3)
            {
                strSql.Append(string.Format(" and QuartNo='1' and YearNo='{0}' ", year));
            }
            else if (4 <= month && month <= 6)
            {
                strSql.Append(string.Format(" and QuartNo='2' and YearNo='{0}' ", year));
            }
            else if (7 <= month && month <= 9)
            {
                strSql.Append(string.Format(" and QuartNo='3' and YearNo='{0}' ", year));
            }
            else if (10 <= month && month <= 12)
            {
                strSql.Append(string.Format(" and QuartNo='4' and YearNo='{0}' ", year));
            }

            strSql.Append(strWhere);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (objReader.Read())
                    {
                        if (Convert.ToBoolean(objReader["IsSpecial"]) == true)
                        {
                            isSpecial = true;
                        }
                    }
                }
            }
            return isSpecial;
        }




        public string GetGuestSimpName(string strWhere)
        {
            string SimpGuestName = "";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SimpGuestName");
            strSql.Append(" from TB_GuestTrack ");
            strSql.Append(" left join tb_User as AEUser on TB_GuestTrack.AE=AEUser.ID");
            strSql.Append(" left join tb_User as INSIDEUser on TB_GuestTrack.INSIDE=INSIDEUser.ID");

            strSql.Append(string.Format(@" where (TB_GuestTrack.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') and state='通过') or TB_GuestTrack.id not in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') ))"));


            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            if (1 <= month && month <= 3)
            {
                strSql.Append(string.Format(" and QuartNo='1' and YearNo='{0}' ", year));
            }
            else if (4 <= month && month <= 6)
            {
                strSql.Append(string.Format(" and QuartNo='2' and YearNo='{0}' ", year));
            }
            else if (7 <= month && month <= 9)
            {
                strSql.Append(string.Format(" and QuartNo='3' and YearNo='{0}' ", year));
            }
            else if (10 <= month && month <= 12)
            {
                strSql.Append(string.Format(" and QuartNo='4' and YearNo='{0}' ", year));
            }
            strSql.Append(strWhere);

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        SimpGuestName = objReader["SimpGuestName"].ToString();

                    }
                }
            }
            return SimpGuestName;
        }

        public List<VAN_OA.Model.ReportForms.TB_GuestTrack> GetGuestListToQuery(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MyGuestPro,MyGuestType,TB_GuestTrack.Id,AEUser.loginName as AEloginName,INSIDEUser.loginName as INSIDEloginName,");
            strSql.Append(" GuestName,GuestId,AEPer,INSIDEPer,GuestAddress ");
            strSql.Append(" from TB_GuestTrack ");
            strSql.Append(" left join tb_User as AEUser on TB_GuestTrack.AE=AEUser.ID");
            strSql.Append(" left join tb_User as INSIDEUser on TB_GuestTrack.INSIDE=INSIDEUser.ID");

            strSql.Append(string.Format(@" where (TB_GuestTrack.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') and state='通过') or TB_GuestTrack.id not in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='客户联系跟踪表') ))"));


            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            if (1 <= month && month <= 3)
            {
                strSql.Append(string.Format(" and QuartNo='1' and YearNo='{0}' ", year));
            }
            else if (4 <= month && month <= 6)
            {
                strSql.Append(string.Format(" and QuartNo='2' and YearNo='{0}' ", year));
            }
            else if (7 <= month && month <= 9)
            {
                strSql.Append(string.Format(" and QuartNo='3' and YearNo='{0}' ", year));
            }
            else if (10 <= month && month <= 12)
            {
                strSql.Append(string.Format(" and QuartNo='4' and YearNo='{0}' ", year));
            }


            strSql.Append(strWhere);


            strSql.Append(" order by GuestId, GuestName");
            List<VAN_OA.Model.ReportForms.TB_GuestTrack> list = new List<VAN_OA.Model.ReportForms.TB_GuestTrack>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {

                        VAN_OA.Model.ReportForms.TB_GuestTrack model = new VAN_OA.Model.ReportForms.TB_GuestTrack();
                        model.GuestName = objReader["GuestName"].ToString();
                        object ojb = objReader["GuestId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestId = ojb.ToString();
                        }

                        ojb = objReader["AEloginName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AEName = ojb.ToString();
                        }

                        ojb = objReader["INSIDEloginName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.INSIDEName = ojb.ToString();
                        }

                        ojb = objReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }

                        ojb = objReader["AEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AEPer = Convert.ToDecimal(ojb);
                        }

                        ojb = objReader["INSIDEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.INSIDEPer = Convert.ToDecimal(ojb);
                        }

                        ojb = objReader["MyGuestType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MyGuestType = Convert.ToString(ojb);
                        }
                        model.MyGuestPro = -1;
                        ojb = objReader["MyGuestPro"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MyGuestPro = Convert.ToInt32(ojb);
                        }
                        ojb = objReader["GuestAddress"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestAddress = ojb.ToString();
                        }

                        list.Add(model);
                    }
                }
            }
            return list;
        }



        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.ReportForms.TB_GuestTrack> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            //strSql.Append("select TB_GuestTrack.Id,Time,GuestName,Phone,LikeMan,Job,FoxOrEmail,IfSave,QQMsn,FristMeet,SecondMeet,FaceMeet,Price,IfSuccess,MyAppraise,ManAppraise,CreateUser,CreateTime,loginName ");
            //strSql.Append(" FROM TB_GuestTrack left join tb_User on TB_GuestTrack.CreateUser=tb_User.ID");
            strSql.Append(" SimpGuestName,MyGuestPro,MyGuestType,TB_GuestTrack.Id,Time,GuestName,Phone,LikeMan,Job,FoxOrEmail,IfSave,QQMsn,FristMeet,SecondMeet,FaceMeet,Price,IfSuccess,MyAppraise,ManAppraise,CreateUser,CreateTime,tb_User.loginName,YearNo,QuartNo,AEPer,INSIDEPer,IsSpecial ");
            strSql.Append(",GuestId,GuestAddress,GuestHttp,GuestShui,GuestGong,GuestBrandNo,GuestBrandName,AE,INSIDE,GuestTotal,GuestLiRun,GuestDays,Remark ,ProNo");
            strSql.Append(",AEUser.loginName as AEloginName,INSIDEUser.loginName as INSIDEloginName,INSIDERemark");
            strSql.Append(" from TB_GuestTrack left join tb_User on TB_GuestTrack.CreateUser=tb_User.ID");
            strSql.Append(" left join tb_User as AEUser on TB_GuestTrack.AE=AEUser.ID");
            strSql.Append(" left join tb_User as INSIDEUser on TB_GuestTrack.INSIDE=INSIDEUser.ID");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by  Time desc");
            List<VAN_OA.Model.ReportForms.TB_GuestTrack> list = new List<VAN_OA.Model.ReportForms.TB_GuestTrack>();

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


        public List<int> GetCurrentZhangQi(string ddlSelectYears, string ddlJidu)
        {
            int year = Convert.ToInt32(ddlSelectYears);
            int jidu = Convert.ToInt32(ddlJidu);


            List<int> keyValues = new List<int>();
            

            if (jidu == 1)
            {
                keyValues.Add(1);
                keyValues.Add(year);

                keyValues.Add(4);
                keyValues.Add(year - 1);
            }
            else if (jidu==2)
            {
                keyValues.Add(2);
                keyValues.Add(year);
                keyValues.Add(1);
                keyValues.Add(year);
            }
            else if (jidu==3)
            {
                keyValues.Add(3);
                keyValues.Add(year);
                keyValues.Add(2);
                keyValues.Add(year);
            }
            else if (jidu==4)
            {
                keyValues.Add(4);
                keyValues.Add(year);
                keyValues.Add(3);
                keyValues.Add(year);
            }

            return keyValues;
        }

        public List<VAN_OA.Model.ReportForms.TB_GuestTrack> GetListArrayGuilei(string strWhere, string dllAddGuest, string ddlDiffMyGuestType, string dllDiffMyGuestPro
            , string ddlSelectYears, string ddlJidu)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            //strSql.Append("select TB_GuestTrack.Id,Time,GuestName,Phone,LikeMan,Job,FoxOrEmail,IfSave,QQMsn,FristMeet,SecondMeet,FaceMeet,Price,IfSuccess,MyAppraise,ManAppraise,CreateUser,CreateTime,loginName ");
            //strSql.Append(" FROM TB_GuestTrack left join tb_User on TB_GuestTrack.CreateUser=tb_User.ID");
            strSql.Append(" SimpGuestName,MyGuestPro,MyGuestType,TB_GuestTrack.Id,Time,GuestName,Phone,LikeMan,Job,FoxOrEmail,IfSave,QQMsn,FristMeet,SecondMeet,FaceMeet,Price,IfSuccess,MyAppraise,ManAppraise,CreateUser,CreateTime,tb_User.loginName,YearNo,QuartNo,AEPer,INSIDEPer,IsSpecial,AA.ID as AddId ");
            strSql.Append(",GuestId,GuestAddress,GuestHttp,GuestShui,GuestGong,GuestBrandNo,GuestBrandName,AE,INSIDE,GuestTotal,GuestLiRun,GuestDays,Remark ,ProNo");
            strSql.Append(",AEUser.loginName as AEloginName,INSIDEUser.loginName as INSIDEloginName,INSIDERemark,TopId");
            strSql.Append(" from TB_GuestTrack left join tb_User on TB_GuestTrack.CreateUser=tb_User.ID");
            strSql.Append(" left join tb_User as AEUser on TB_GuestTrack.AE=AEUser.ID");
            strSql.Append(" left join tb_User as INSIDEUser on TB_GuestTrack.INSIDE=INSIDEUser.ID");
            List<int> zhangqi = GetCurrentZhangQi(ddlSelectYears, ddlJidu);
            strSql.AppendFormat(@" left join (
select A.Id,b.Id as TopId from 
(SELECT ID,GuestName FROM TB_GuestTrack where QuartNo='{0}' and YearNo='{1}') AS A 
LEFT JOIN (SELECT ID,GuestName FROM TB_GuestTrack where QuartNo='{2}' and YearNo='{3}') AS B on A.GuestName=B.GuestName
) AS AA on AA.ID=TB_GuestTrack.ID", zhangqi[0], zhangqi[1], zhangqi[2], zhangqi[3]);

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //选 新增时，当前季度新增的客户（上季度没有的）将被筛选出来；
            if (dllAddGuest == "1")
            {
                strSql.AppendFormat(@" and  AA.TopId is null");
            }
            //选原有，本季度有的客户，上季度也有的客户将被筛选出来；
            if (dllAddGuest == "2")
            {
                strSql.AppendFormat(@" and  AA.TopId>0");                
            }
            //当前季度客户类型 和 上季度 不同的，将被筛选出来
            if (ddlDiffMyGuestType == "1")
            {
                strSql.AppendFormat(@" and TB_GuestTrack.ID in (select A.ID from 
(SELECT ID,SimpGuestName,MyGuestType FROM TB_GuestTrack where  QuartNo='{0}' and YearNo='{1}') AS A 
left JOIN (SELECT ID,SimpGuestName,MyGuestType FROM TB_GuestTrack where QuartNo='{2}' and YearNo='{3}') AS B on A.SimpGuestName=B.SimpGuestName
where A.MyGuestType<> isnull(b.MyGuestType,''))"
, zhangqi[0], zhangqi[1], zhangqi[2], zhangqi[3]);
            }
            //选不变时，当前季度客户类型 和 上季度 相同的，将被筛选出来
            if (ddlDiffMyGuestType == "2")
            {
                strSql.AppendFormat(@" and TB_GuestTrack.ID in (select A.ID from 
(SELECT ID,SimpGuestName,MyGuestType FROM TB_GuestTrack where  QuartNo='{0}' and YearNo='{1}') AS A 
left JOIN (SELECT ID,SimpGuestName,MyGuestType FROM TB_GuestTrack where QuartNo='{2}' and YearNo='{3}') AS B on A.SimpGuestName=B.SimpGuestName
where A.MyGuestType=b.MyGuestType)"
, zhangqi[0], zhangqi[1], zhangqi[2], zhangqi[3]);
            }

            //当前季度客户属性 和 上季度 不同的，将被筛选出来
            if (dllDiffMyGuestPro == "1")
            {
                strSql.AppendFormat(@" and TB_GuestTrack.ID in (select A.ID from 
(SELECT ID,SimpGuestName,MyGuestPro FROM TB_GuestTrack where  QuartNo='{0}' and YearNo='{1}') AS A 
left JOIN (SELECT ID,SimpGuestName,MyGuestPro FROM TB_GuestTrack where QuartNo='{2}' and YearNo='{3}') AS B on A.SimpGuestName=B.SimpGuestName
where A.MyGuestPro<> isnull(b.MyGuestPro,''))"
, zhangqi[0], zhangqi[1], zhangqi[2], zhangqi[3]);
            }
            //选不变时，当前季度客户属性 和 上季度 相同的，将被筛选出来
            if (dllDiffMyGuestPro == "2")
            {
                strSql.AppendFormat(@" and TB_GuestTrack.ID in (select A.ID from 
(SELECT ID,SimpGuestName,MyGuestPro FROM TB_GuestTrack where  QuartNo='{0}' and YearNo='{1}') AS A 
left JOIN (SELECT ID,SimpGuestName,MyGuestPro FROM TB_GuestTrack where QuartNo='{2}' and YearNo='{3}') AS B on A.SimpGuestName=B.SimpGuestName
where A.MyGuestPro=b.MyGuestPro)"
, zhangqi[0], zhangqi[1], zhangqi[2], zhangqi[3]);
            }

            strSql.Append(" order by  Time desc");
            List<VAN_OA.Model.ReportForms.TB_GuestTrack> list = new List<VAN_OA.Model.ReportForms.TB_GuestTrack>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        var model = ReaderBind(objReader);
                        var ojb = objReader["TopId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsAddGuest = "否";
                        }
                        else
                        {
                            model.IsAddGuest = "是";
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.ReportForms.TB_GuestTrack> GetListArrayToPage(string strWhere, PagerDomain page)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  AEUser.loginName as AEloginName,
INSIDEUser.loginName as INSIDEloginName,Time,YearNo,QuartNo,GuestName,SimpGuestName,MyGuestType,MyGuestPro from TB_GuestTrack 
left join tb_User on TB_GuestTrack.CreateUser=tb_User.ID 
left join tb_User as AEUser on TB_GuestTrack.AE=AEUser.ID left join tb_User as INSIDEUser on TB_GuestTrack.INSIDE=INSIDEUser.ID 
where TB_GuestTrack.id in (select allE_id from tb_EForm where proId =17 and state='通过') ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }

            strSql.Append(@" union select  AEUser.loginName as AEloginName,
INSIDEUser.loginName as INSIDEloginName,Time,YearNo,QuartNo,GuestName,SimpGuestName,MyGuestType,MyGuestPro from TB_GuestTrack 
left join tb_User on TB_GuestTrack.CreateUser=tb_User.ID 
left join tb_User as AEUser on TB_GuestTrack.AE=AEUser.ID left join tb_User as INSIDEUser on TB_GuestTrack.INSIDE=INSIDEUser.ID 
where TB_GuestTrack.id not in (select allE_id from tb_EForm where proId =17) ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            //strSql.Append(" order by TB_GuestTrack.id desc");

            //strSql = new StringBuilder(DBHelp.GetPagerSql(page, strSql.ToString(), strWhere, " TB_GuestTrack.id desc "));

            List<VAN_OA.Model.ReportForms.TB_GuestTrack> list = new List<VAN_OA.Model.ReportForms.TB_GuestTrack>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.ReportForms.TB_GuestTrack model = new VAN_OA.Model.ReportForms.TB_GuestTrack();
                        object ojb;

                        ojb = dataReader["Time"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Time = (DateTime)ojb;
                        }
                        model.GuestName = dataReader["GuestName"].ToString();
                        model.SimpGuestName = dataReader["SimpGuestName"].ToString();
                        ojb = dataReader["AEloginName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AEName = ojb.ToString();
                        }

                        ojb = dataReader["INSIDEloginName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.INSIDEName = ojb.ToString();
                        }



                        try
                        {

                            ojb = dataReader["YearNo"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.YearNo = ojb.ToString();
                            }

                            ojb = dataReader["QuartNo"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.QuartNo = ojb.ToString();
                            }
                            model.YearAndMonth = model.YearNo + "-" + model.QuartNo + " 季度";
                        }
                        catch (Exception)
                        {


                        }



                        ojb = dataReader["MyGuestType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MyGuestType = ojb.ToString();
                        }
                        model.MyGuestPro = -1;
                        ojb = dataReader["MyGuestPro"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MyGuestPro = Convert.ToInt32(ojb);
                        }


                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.ReportForms.TB_GuestTrack> GetListArrayOld(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            //strSql.Append("select TB_GuestTrack.Id,Time,GuestName,Phone,LikeMan,Job,FoxOrEmail,IfSave,QQMsn,FristMeet,SecondMeet,FaceMeet,Price,IfSuccess,MyAppraise,ManAppraise,CreateUser,CreateTime,loginName ");
            //strSql.Append(" FROM TB_GuestTrack left join tb_User on TB_GuestTrack.CreateUser=tb_User.ID");
            strSql.Append(" TB_GuestTrackOld.Id,Time,GuestName,Phone,LikeMan,Job,FoxOrEmail,IfSave,QQMsn,FristMeet,SecondMeet,FaceMeet,Price,IfSuccess,MyAppraise,ManAppraise,CreateUser,CreateTime,tb_User.loginName,AEPer,INSIDEPer ");
            strSql.Append(",GuestId,GuestAddress,GuestHttp,GuestShui,GuestGong,GuestBrandNo,GuestBrandName,AE,INSIDE,GuestTotal,GuestLiRun,GuestDays,Remark ,ProNo");
            strSql.Append(",AEUser.loginName as AEloginName,INSIDEUser.loginName as INSIDEloginName,INSIDERemark");
            strSql.Append(" from TB_GuestTrackOld left join tb_User on TB_GuestTrackOld.CreateUser=tb_User.ID");
            strSql.Append(" left join tb_User as AEUser on TB_GuestTrackOld.AE=AEUser.ID");
            strSql.Append(" left join tb_User as INSIDEUser on TB_GuestTrackOld.INSIDE=INSIDEUser.ID");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by  Time desc");
            List<VAN_OA.Model.ReportForms.TB_GuestTrack> list = new List<VAN_OA.Model.ReportForms.TB_GuestTrack>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {
                        list.Add(ReaderBind_1(objReader));
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.ReportForms.TB_GuestTrack ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.ReportForms.TB_GuestTrack model = new VAN_OA.Model.ReportForms.TB_GuestTrack();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["Time"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Time = (DateTime)ojb;
            }
            model.GuestName = dataReader["GuestName"].ToString();
            model.SimpGuestName = dataReader["SimpGuestName"].ToString();
            model.Phone = dataReader["Phone"].ToString();
            model.LikeMan = dataReader["LikeMan"].ToString();
            model.Job = dataReader["Job"].ToString();
            model.FoxOrEmail = dataReader["FoxOrEmail"].ToString();
            ojb = dataReader["IfSave"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IfSave = (bool)ojb;
            }
            model.QQMsn = dataReader["QQMsn"].ToString();
            model.FristMeet = dataReader["FristMeet"].ToString();
            model.SecondMeet = dataReader["SecondMeet"].ToString();
            model.FaceMeet = dataReader["FaceMeet"].ToString();
            ojb = dataReader["Price"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Price = (decimal)ojb;
            }
            ojb = dataReader["IfSuccess"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IfSuccess = (bool)ojb;
            }
            model.MyAppraise = dataReader["MyAppraise"].ToString();
            model.ManAppraise = dataReader["ManAppraise"].ToString();
            ojb = dataReader["CreateUser"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUser = (int)ojb;
            }
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UserName = ojb.ToString();
            }



            ojb = dataReader["GuestId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestId = ojb.ToString();
            }


            ojb = dataReader["GuestAddress"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestAddress = ojb.ToString();
            }

            ojb = dataReader["GuestHttp"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestHttp = ojb.ToString();
            }

            ojb = dataReader["GuestShui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestShui = ojb.ToString();
            }

            ojb = dataReader["GuestGong"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestGong = ojb.ToString();
            }
            ojb = dataReader["GuestBrandNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestBrandNo = ojb.ToString();
            }

            ojb = dataReader["GuestBrandNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestBrandNo = ojb.ToString();
            }
            ojb = dataReader["GuestBrandName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestBrandName = ojb.ToString();
            }






            ojb = dataReader["AE"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AE = (int)ojb;
            }
            ojb = dataReader["INSIDE"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.INSIDE = (int)ojb;
            }
            ojb = dataReader["GuestTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestTotal = (decimal)ojb;
            }
            ojb = dataReader["GuestLiRun"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestLiRun = (decimal)ojb;
            }
            ojb = dataReader["GuestDays"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestDays = (decimal)ojb;
            }

            ojb = dataReader["Remark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Remark = ojb.ToString();
            }

            ojb = dataReader["AEloginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AEName = ojb.ToString();
            }

            ojb = dataReader["INSIDEloginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.INSIDEName = ojb.ToString();
            }

            ojb = dataReader["INSIDERemark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.INSIDERemark = ojb.ToString();
            }
            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }

            ojb = dataReader["AEPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AEPer = (decimal)ojb;
            }

            ojb = dataReader["INSIDEPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.INSIDEPer = (decimal)ojb;
            }


            try
            {

                ojb = dataReader["YearNo"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.YearNo = ojb.ToString();
                }

                ojb = dataReader["QuartNo"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.QuartNo = ojb.ToString();
                }
                model.YearAndMonth = model.YearNo + "-" + model.QuartNo + " 季度";
            }
            catch (Exception)
            {


            }

            ojb = dataReader["IsSpecial"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsSpecial = (bool)ojb;
            }

            ojb = dataReader["MyGuestType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MyGuestType = ojb.ToString();
            }
            model.MyGuestPro = -1;
            ojb = dataReader["MyGuestPro"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MyGuestPro = Convert.ToInt32(ojb);
            }
            return model;
        }

        public VAN_OA.Model.ReportForms.TB_GuestTrack ReaderBind_1(IDataReader dataReader)
        {
            VAN_OA.Model.ReportForms.TB_GuestTrack model = new VAN_OA.Model.ReportForms.TB_GuestTrack();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["Time"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Time = (DateTime)ojb;
            }
            model.GuestName = dataReader["GuestName"].ToString();
            model.Phone = dataReader["Phone"].ToString();
            model.LikeMan = dataReader["LikeMan"].ToString();
            model.Job = dataReader["Job"].ToString();
            model.FoxOrEmail = dataReader["FoxOrEmail"].ToString();
            ojb = dataReader["IfSave"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IfSave = (bool)ojb;
            }
            model.QQMsn = dataReader["QQMsn"].ToString();
            model.FristMeet = dataReader["FristMeet"].ToString();
            model.SecondMeet = dataReader["SecondMeet"].ToString();
            model.FaceMeet = dataReader["FaceMeet"].ToString();
            ojb = dataReader["Price"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Price = (decimal)ojb;
            }
            ojb = dataReader["IfSuccess"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IfSuccess = (bool)ojb;
            }
            model.MyAppraise = dataReader["MyAppraise"].ToString();
            model.ManAppraise = dataReader["ManAppraise"].ToString();
            ojb = dataReader["CreateUser"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUser = (int)ojb;
            }
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.UserName = ojb.ToString();
            }



            ojb = dataReader["GuestId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestId = ojb.ToString();
            }


            ojb = dataReader["GuestAddress"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestAddress = ojb.ToString();
            }

            ojb = dataReader["GuestHttp"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestHttp = ojb.ToString();
            }

            ojb = dataReader["GuestShui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestShui = ojb.ToString();
            }

            ojb = dataReader["GuestGong"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestGong = ojb.ToString();
            }
            ojb = dataReader["GuestBrandNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestBrandNo = ojb.ToString();
            }

            ojb = dataReader["GuestBrandNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestBrandNo = ojb.ToString();
            }
            ojb = dataReader["GuestBrandName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestBrandName = ojb.ToString();
            }






            ojb = dataReader["AE"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AE = (int)ojb;
            }
            ojb = dataReader["INSIDE"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.INSIDE = (int)ojb;
            }
            ojb = dataReader["GuestTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestTotal = (decimal)ojb;
            }
            ojb = dataReader["GuestLiRun"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestLiRun = (decimal)ojb;
            }
            ojb = dataReader["GuestDays"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestDays = (decimal)ojb;
            }

            ojb = dataReader["Remark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Remark = ojb.ToString();
            }

            ojb = dataReader["AEloginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AEName = ojb.ToString();
            }

            ojb = dataReader["INSIDEloginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.INSIDEName = ojb.ToString();
            }

            ojb = dataReader["INSIDERemark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.INSIDERemark = ojb.ToString();
            }
            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }

            ojb = dataReader["AEPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AEPer = (decimal)ojb;
            }

            ojb = dataReader["INSIDEPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.INSIDEPer = (decimal)ojb;
            }

            //ojb = dataReader["MyGuestType"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.MyGuestType = ojb.ToString();
            //}
            return model;
        }
    }
}
