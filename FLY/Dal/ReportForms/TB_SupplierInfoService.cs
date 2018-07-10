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
    public class TB_SupplierInfoService
    {
        public bool updateTran(VAN_OA.Model.ReportForms.TB_SupplierInfo model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
                    model.Status = eform.state;
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
        public int addTran(VAN_OA.Model.ReportForms.TB_SupplierInfo model, VAN_OA.Model.EFrom.tb_EForm eform)
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
                    model.Status = eform.state;
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("TB_SupplierInfo", objCommand);
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
        public string AddToString(TB_SupplierInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Time != null)
            {
                strSql1.Append("Time,");
                strSql2.Append("'" + model.Time + "',");
            }
            if (model.SupplierName != null)
            {
                strSql1.Append("SupplierName,");
                strSql2.Append("'" + model.SupplierName + "',");
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
            if (model.SupplierId != null)
            {
                strSql1.Append("SupplierId,");
                strSql2.Append("'" + model.SupplierId + "',");
            }
            if (model.SupplierAddress != null)
            {
                strSql1.Append("SupplierAddress,");
                strSql2.Append("'" + model.SupplierAddress + "',");
            }
            if (model.SupplierHttp != null)
            {
                strSql1.Append("SupplierHttp,");
                strSql2.Append("'" + model.SupplierHttp + "',");
            }
            if (model.SupplierShui != null)
            {
                strSql1.Append("SupplierShui,");
                strSql2.Append("'" + model.SupplierShui + "',");
            }
            if (model.SupplierGong != null)
            {
                strSql1.Append("SupplierGong,");
                strSql2.Append("'" + model.SupplierGong + "',");
            }
            if (model.SupplierBrandNo != null)
            {
                strSql1.Append("SupplierBrandNo,");
                strSql2.Append("'" + model.SupplierBrandNo + "',");
            }
            if (model.SupplierBrandName != null)
            {
                strSql1.Append("SupplierBrandName,");
                strSql2.Append("'" + model.SupplierBrandName + "',");
            }
           
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
           
            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }
          

            if (model.SupplieSimpeName != null)
            {
                strSql1.Append("SupplieSimpeName,");
                strSql2.Append("'" + model.SupplieSimpeName + "',");
            }

            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("'" + model.Status + "',");
            }

            if (model.MainRange != null)
            {
                strSql1.Append("MainRange,");
                strSql2.Append("'" + model.MainRange + "',");
            }

            if (model.ZhuJi != null)
            {
                strSql1.Append("ZhuJi,");
                strSql2.Append("'" + model.ZhuJi + "',");
            }
            if (model.IsSpecial != null)
            {
                strSql1.Append("IsSpecial,");
                strSql2.Append("" + (model.IsSpecial ? 1 : 0) + ",");
            }

            strSql1.Append("Province,");
            strSql2.Append("'" + model.Province  + "',");

            strSql1.Append("City,");
            strSql2.Append("'" + model.City + "',");

            strSql1.Append("Peculiarity,");
            strSql2.Append("'" + model.Peculiarity + "',");

            strSql.Append("insert into TB_SupplierInfo(");
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
        public int Add(VAN_OA.Model.ReportForms.TB_SupplierInfo model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Time != null)
            {
                strSql1.Append("Time,");
                strSql2.Append("'" + model.Time + "',");
            }
            if (model.SupplierName != null)
            {
                strSql1.Append("SupplierName,");
                strSql2.Append("'" + model.SupplierName + "',");
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


            if (model.SupplierAddress != null)
            {
                strSql1.Append("SupplierAddress,");
                strSql2.Append("'" + model.SupplierAddress + "',");
            }
            if (model.SupplierHttp != null)
            {
                strSql1.Append("SupplierHttp,");
                strSql2.Append("'" + model.SupplierHttp + "',");
            }
            if (model.SupplierShui != null)
            {
                strSql1.Append("SupplierShui,");
                strSql2.Append("'" + model.SupplierShui + "',");
            }
            if (model.SupplierGong != null)
            {
                strSql1.Append("SupplierGong,");
                strSql2.Append("'" + model.SupplierGong + "',");
            }
            if (model.SupplierBrandNo != null)
            {
                strSql1.Append("SupplierBrandNo,");
                strSql2.Append("'" + model.SupplierBrandNo + "',");
            }
            if (model.SupplierBrandName != null)
            {
                strSql1.Append("SupplierBrandName,");
                strSql2.Append("'" + model.SupplierBrandName + "',");
            }
            
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            if (model.MainRange != null)
            {
                strSql1.Append("MainRange,");
                strSql2.Append("'" + model.MainRange + "',");
            }
           
            if (model.SupplieSimpeName != null)
            {
                strSql1.Append("SupplieSimpeName,");
                strSql2.Append("'" + model.SupplieSimpeName + "',");
            }
            if (model.ZhuJi != null)
            {
                strSql1.Append("ZhuJi,");
                strSql2.Append("'" + model.ZhuJi + "',");
            }
          

            string MaxSupplierId = "";
            string sqlSupplierId = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(SupplierId),4))+1))),4) FROM TB_SupplierInfo;");

            object objMaxSupplier = DBHelp.ExeScalar(sqlSupplierId);
            if (objMaxSupplier != null && objMaxSupplier.ToString() != "")
            {
                MaxSupplierId = objMaxSupplier.ToString();
            }
            else
            {
                MaxSupplierId = "0001";
            }

            strSql1.Append("SupplierId,");
            strSql2.Append("'" + MaxSupplierId + "',");

            strSql1.Append("ProNo,");

          
            strSql2.Append("'" + model.ProNo + "',");

            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("'" + model.Status + "',");
            }
            if (model.IsUse != null)
            {
                strSql1.Append("IsUse,");
                strSql2.Append("" + (model.IsUse ? 1 : 0) + ",");
            }
            if (model.IsSpecial != null)
            {
                strSql1.Append("IsSpecial,");
                strSql2.Append("" + (model.IsSpecial ? 1 : 0) + ",");
            }

            strSql1.Append("Province,");
            strSql2.Append("'" + model.Province + "',");
            strSql1.Append("City,");
            strSql2.Append("'" + model.City + "',");

            strSql1.Append("Peculiarity,");
            strSql2.Append("'" + model.Peculiarity + "',");

            strSql.Append("insert into TB_SupplierInfo(");
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
        public int Add(VAN_OA.Model.ReportForms.TB_SupplierInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Time != null)
            {
                strSql1.Append("Time,");
                strSql2.Append("'" + model.Time + "',");
            }
            if (model.SupplierName != null)
            {
                strSql1.Append("SupplierName,");
                strSql2.Append("'" + model.SupplierName + "',");
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


            if (model.SupplierAddress != null)
            {
                strSql1.Append("SupplierAddress,");
                strSql2.Append("'" + model.SupplierAddress + "',");
            }
            if (model.SupplierHttp != null)
            {
                strSql1.Append("SupplierHttp,");
                strSql2.Append("'" + model.SupplierHttp + "',");
            }
            if (model.SupplierShui != null)
            {
                strSql1.Append("SupplierShui,");
                strSql2.Append("'" + model.SupplierShui + "',");
            }
            if (model.SupplierGong != null)
            {
                strSql1.Append("SupplierGong,");
                strSql2.Append("'" + model.SupplierGong + "',");
            }
            if (model.SupplierBrandNo != null)
            {
                strSql1.Append("SupplierBrandNo,");
                strSql2.Append("'" + model.SupplierBrandNo + "',");
            }
            if (model.SupplierBrandName != null)
            {
                strSql1.Append("SupplierBrandName,");
                strSql2.Append("'" + model.SupplierBrandName + "',");
            }
           
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }


            if (model.SupplieSimpeName != null)
            {
                strSql1.Append("SupplieSimpeName,");
                strSql2.Append("'" + model.SupplieSimpeName + "',");
            }

            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("'" + model.Status + "',");
            }

            strSql1.Append("SupplierId,");
            strSql2.Append("'" + model.SupplierId + "',");

            strSql1.Append("ProNo,");

            if (model.MainRange != null)
            {
                strSql1.Append("MainRange,");
                strSql2.Append("'" + model.MainRange + "',");
            }
            if (model.ZhuJi != null)
            {
                strSql1.Append("ZhuJi,");
                strSql2.Append("'" + model.ZhuJi + "',");
            }
            if (model.IsSpecial != null)
            {
                strSql1.Append("IsSpecial,");
                strSql2.Append("" + (model.IsSpecial ? 1 : 0) + ",");
            }
            strSql1.Append("Province,");
            strSql2.Append("'" + model.Province + "',");

            strSql1.Append("City,");
            strSql2.Append("'" + model.City + "',");

            strSql1.Append("Peculiarity,");
            strSql2.Append("'" + model.Peculiarity + "',");
            //
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(ProNo),4))+1))),4) FROM TB_SupplierInfo;");

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

            //
            strSql.Append("insert into TB_SupplierInfo(");
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
        public bool Update(VAN_OA.Model.ReportForms.TB_SupplierInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_SupplierInfo set ");
            if (model.Time != null)
            {
                strSql.Append("Time='" + model.Time + "',");
            }
            if (model.SupplierName != null)
            {
                strSql.Append("SupplierName='" + model.SupplierName + "',");
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

            if (model.SupplierId != null)
            {
                strSql.Append("SupplierId='" + model.SupplierId + "',");
            }
            else
            {
                strSql.Append("SupplierId= null ,");
            }
            if (model.SupplierAddress != null)
            {
                strSql.Append("SupplierAddress='" + model.SupplierAddress + "',");
            }
            else
            {
                strSql.Append("SupplierAddress= null ,");
            }
            if (model.SupplierHttp != null)
            {
                strSql.Append("SupplierHttp='" + model.SupplierHttp + "',");
            }
            else
            {
                strSql.Append("SupplierHttp= null ,");
            }
            if (model.SupplierShui != null)
            {
                strSql.Append("SupplierShui='" + model.SupplierShui + "',");
            }
            else
            {
                strSql.Append("SupplierShui= null ,");
            }
            if (model.SupplierGong != null)
            {
                strSql.Append("SupplierGong='" + model.SupplierGong + "',");
            }
            else
            {
                strSql.Append("SupplierGong= null ,");
            }
            if (model.SupplierBrandNo != null)
            {
                strSql.Append("SupplierBrandNo='" + model.SupplierBrandNo + "',");
            }
            else
            {
                strSql.Append("SupplierBrandNo= null ,");
            }
            if (model.SupplierBrandName != null)
            {
                strSql.Append("SupplierBrandName='" + model.SupplierBrandName + "',");
            }
            else
            {
                strSql.Append("SupplierBrandName= null ,");
            }
          
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }
          

            if (model.SupplieSimpeName != null)
            {
                strSql.Append("SupplieSimpeName='" + model.SupplieSimpeName + "',");
            }
            if (model.MainRange != null)
            {
                strSql.Append("MainRange='" + model.MainRange + "',");
            }
            if (model.ZhuJi != null)
            {
                strSql.Append("ZhuJi='" + model.ZhuJi + "',");
            }
            if (model.IsSpecial != null)
            {
                strSql.Append("IsSpecial=" + (model.IsSpecial ? 1 : 0) + ",");
            }
            strSql.Append("Province='" + model.Province + "',");
            strSql.Append("City='" + model.City + "',");
            strSql.Append("Peculiarity='" + model.Peculiarity + "',");
            // strSql.Append("Status='" + model.Status + "',");
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        public void Update(VAN_OA.Model.ReportForms.TB_SupplierInfo model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_SupplierInfo set ");
            if (model.Time != null)
            {
                strSql.Append("Time='" + model.Time + "',");
            }
            if (model.SupplierName != null)
            {
                strSql.Append("SupplierName='" + model.SupplierName + "',");
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
            
            if (model.SupplierId != null)
            {
                strSql.Append("SupplierId='" + model.SupplierId + "',");
            }
            else
            {
                strSql.Append("SupplierId= null ,");
            }
            if (model.SupplierAddress != null)
            {
                strSql.Append("SupplierAddress='" + model.SupplierAddress + "',");
            }
            else
            {
                strSql.Append("SupplierAddress= null ,");
            }
            if (model.SupplierHttp != null)
            {
                strSql.Append("SupplierHttp='" + model.SupplierHttp + "',");
            }
            else
            {
                strSql.Append("SupplierHttp= null ,");
            }
            if (model.SupplierShui != null)
            {
                strSql.Append("SupplierShui='" + model.SupplierShui + "',");
            }
            else
            {
                strSql.Append("SupplierShui= null ,");
            }
            if (model.SupplierGong != null)
            {
                strSql.Append("SupplierGong='" + model.SupplierGong + "',");
            }
            else
            {
                strSql.Append("SupplierGong= null ,");
            }
            if (model.SupplierBrandNo != null)
            {
                strSql.Append("SupplierBrandNo='" + model.SupplierBrandNo + "',");
            }
            else
            {
                strSql.Append("SupplierBrandNo= null ,");
            }
            if (model.SupplierBrandName != null)
            {
                strSql.Append("SupplierBrandName='" + model.SupplierBrandName + "',");
            }
            else
            {
                strSql.Append("SupplierBrandName= null ,");
            }
           
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }
          
            //if (model.SupplieSimpeName != null)
            //{
            //    strSql.Append("SupplieSimpeName='" + model.SupplieSimpeName + "',");
            //}

            if (model.MainRange != null)
            {
                strSql.Append("MainRange='" + model.MainRange + "',");
            }
            if (model.ZhuJi != null)
            {
                strSql.Append("ZhuJi='" + model.ZhuJi + "',");
            }
            strSql.Append("Status='" + model.Status + "',");
            if (model.IsUse != null)
            {
                strSql.Append("IsUse=" + (model.IsUse ? 1 : 0) + ",");
            }
            if (model.IsSpecial != null)
            {
                strSql.Append("IsSpecial=" + (model.IsSpecial ? 1 : 0) + ",");
            }
            strSql.Append("Province='" + model.Province + "',");
            strSql.Append("City='" + model.City + "',");
            strSql.Append("Peculiarity='" + model.Peculiarity + "',");
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }


      

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_SupplierInfo ");
            strSql.Append(" where Id=" + Id + "");
            return DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.ReportForms.TB_SupplierInfo GetModel(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Peculiarity,City,Province,TB_SupplierInfo.Id,Time,SupplierName,Phone,LikeMan,Job,FoxOrEmail,IfSave,QQMsn,FristMeet,SecondMeet,FaceMeet,Price,IfSuccess,MyAppraise,ManAppraise,CreateUser,CreateTime,tb_User.loginName,ZhuJi,IsSpecial");
            strSql.Append(",SupplierId,SupplierAddress,SupplierHttp,SupplierShui,SupplierGong,SupplierBrandNo,SupplierBrandName,Remark ,ProNo");
            strSql.Append(",SupplieSimpeName,Status,MainRange,IsUse");
            strSql.Append(" from TB_SupplierInfo left join tb_User on TB_SupplierInfo.CreateUser=tb_User.ID");          
            strSql.Append(" where TB_SupplierInfo.Id=" + Id + "");

            VAN_OA.Model.ReportForms.TB_SupplierInfo model = null;
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
        public List<VAN_OA.Model.ReportForms.TB_SupplierInfo> GetSupplierList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (select   ");
            strSql.Append(" SupplierName,SupplierId ");
            strSql.Append(" from TB_SupplierInfo ");
            strSql.Append(string.Format(@" where Status='通过' "));
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" GROUP BY  SupplierName,SupplierId) as newTable order by  SupplierName");
            List<VAN_OA.Model.ReportForms.TB_SupplierInfo> list = new List<VAN_OA.Model.ReportForms.TB_SupplierInfo>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {

                        VAN_OA.Model.ReportForms.TB_SupplierInfo model = new VAN_OA.Model.ReportForms.TB_SupplierInfo();
                        model.SupplierName = objReader["SupplierName"].ToString();
                        object ojb = objReader["SupplierId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SupplierId = ojb.ToString();
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }



        public List<VAN_OA.Model.ReportForms.TB_SupplierInfo> GetSupplierListToQuery(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  TB_SupplierInfo.Id，");
            strSql.Append(" SupplierName,SupplierId ");
            strSql.Append(" from TB_SupplierInfo ");
          

            strSql.Append(string.Format(@" where Status='通过' "));




            strSql.Append(strWhere);


            strSql.Append(" order by SupplierId, SupplierName");
            List<VAN_OA.Model.ReportForms.TB_SupplierInfo> list = new List<VAN_OA.Model.ReportForms.TB_SupplierInfo>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    while (objReader.Read())
                    {

                        VAN_OA.Model.ReportForms.TB_SupplierInfo model = new VAN_OA.Model.ReportForms.TB_SupplierInfo();
                        model.SupplierName = objReader["SupplierName"].ToString();
                        object ojb = objReader["SupplierId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SupplierId = ojb.ToString();
                        }

                      

                        ojb = objReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
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
        public List<VAN_OA.Model.ReportForms.TB_SupplierInfo> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            //strSql.Append("select TB_SupplierInfo.Id,Time,SupplierName,Phone,LikeMan,Job,FoxOrEmail,IfSave,QQMsn,FristMeet,SecondMeet,FaceMeet,Price,IfSuccess,MyAppraise,ManAppraise,CreateUser,CreateTime,loginName ");
            //strSql.Append(" FROM TB_SupplierInfo left join tb_User on TB_SupplierInfo.CreateUser=tb_User.ID");
            strSql.Append(" Peculiarity,City,Province,TB_SupplierInfo.Id,Time,SupplierName,Phone,LikeMan,Job,FoxOrEmail,IfSave,QQMsn,FristMeet,SecondMeet,FaceMeet,Price,IfSuccess,MyAppraise,ManAppraise,CreateUser,CreateTime,tb_User.loginName,ZhuJi,IsSpecial ");
            strSql.Append(",SupplierId,SupplierAddress,SupplierHttp,SupplierShui,SupplierGong,SupplierBrandNo,SupplierBrandName,Remark ,ProNo");
            strSql.Append(",SupplieSimpeName,Status,MainRange,IsUse");
            strSql.Append(" from TB_SupplierInfo left join tb_User on TB_SupplierInfo.CreateUser=tb_User.ID");
           
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by  TB_SupplierInfo.id desc");
            List<VAN_OA.Model.ReportForms.TB_SupplierInfo> list = new List<VAN_OA.Model.ReportForms.TB_SupplierInfo>();

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
        public VAN_OA.Model.ReportForms.TB_SupplierInfo ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.ReportForms.TB_SupplierInfo model = new VAN_OA.Model.ReportForms.TB_SupplierInfo();
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
            model.SupplierName = dataReader["SupplierName"].ToString();
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



            ojb = dataReader["SupplierId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierId = ojb.ToString();
            }


            ojb = dataReader["SupplierAddress"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierAddress = ojb.ToString();
            }

            ojb = dataReader["SupplierHttp"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierHttp = ojb.ToString();
            }

            ojb = dataReader["SupplierShui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierShui = ojb.ToString();
            }

            ojb = dataReader["SupplierGong"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierGong = ojb.ToString();
            }
            ojb = dataReader["SupplierBrandNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierBrandNo = ojb.ToString();
            }

            ojb = dataReader["SupplierBrandNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierBrandNo = ojb.ToString();
            }
            ojb = dataReader["SupplierBrandName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierBrandName = ojb.ToString();
            }
       
            ojb = dataReader["Remark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Remark = ojb.ToString();
            }
         
            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }
           
            ojb = dataReader["Status"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Status = ojb.ToString();
            }
            ojb = dataReader["SupplieSimpeName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplieSimpeName = ojb.ToString();
            }
            ojb = dataReader["MainRange"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MainRange = ojb.ToString();
            }
            ojb = dataReader["ZhuJi"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ZhuJi = ojb.ToString();
            }
            ojb = dataReader["IsUse"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsUse = (bool)ojb;
            }
            ojb = dataReader["IsSpecial"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsSpecial = (bool)ojb;
            }

            model.Province = dataReader["Province"].ToString();
            model.City = dataReader["City"].ToString();
            model.Peculiarity = dataReader["Peculiarity"].ToString();
            return model;
        }

      
    }
}
