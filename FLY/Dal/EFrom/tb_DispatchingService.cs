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
    public class tb_DispatchingService
    {
        public bool updateTran(VAN_OA.Model.EFrom.tb_Dispatching model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
                    Update(model, objCommand, eform.state);


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
        public int addTran(VAN_OA.Model.EFrom.tb_Dispatching model, VAN_OA.Model.EFrom.tb_EForm eform)
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
                    string proNo = eformSer.GetAllE_No("tb_Dispatching", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;

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
        public int Add(VAN_OA.Model.EFrom.tb_Dispatching model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Dispatcher != null)
            {
                strSql1.Append("Dispatcher,");
                strSql2.Append("'" + model.Dispatcher + "',");
            }
            if (model.DisDate != null)
            {
                strSql1.Append("DisDate,");
                strSql2.Append("'" + model.DisDate + "',");
            }

            if (model.NiDate != null)
            {
                strSql1.Append("NiDate,");
                strSql2.Append("'" + model.NiDate + "',");
            }
            if (model.NiHours != null)
            {
                strSql1.Append("NiHours,");
                strSql2.Append("'" + model.NiHours + "',");
            }

            if (model.OutDispater != null)
            {
                strSql1.Append("OutDispater,");
                strSql2.Append("" + model.OutDispater + ",");
            }
            if (model.GueName != null)
            {
                strSql1.Append("GueName,");
                strSql2.Append("'" + model.GueName + "',");
            }
            if (model.Tel != null)
            {
                strSql1.Append("Tel,");
                strSql2.Append("'" + model.Tel + "',");
            }
            if (model.Address != null)
            {
                strSql1.Append("Address,");
                strSql2.Append("'" + model.Address + "',");
            }
            if (model.Contacter != null)
            {
                strSql1.Append("Contacter,");
                strSql2.Append("'" + model.Contacter + "',");
            }
            if (model.GoDate != null)
            {
                strSql1.Append("GoDate,");
                strSql2.Append("'" + model.GoDate + "',");
            }
            if (model.BackDate != null)
            {
                strSql1.Append("BackDate,");
                strSql2.Append("'" + model.BackDate + "',");
            }
            if (model.Question != null)
            {
                strSql1.Append("Question,");
                strSql2.Append("'" + model.Question + "',");
            }
            if (model.QuestionRemark != null)
            {
                strSql1.Append("QuestionRemark,");
                strSql2.Append("'" + model.QuestionRemark + "',");
            }

            if (model.SuiTongRen != null)
            {
                strSql1.Append("SuiTongRen,");
                strSql2.Append("'" + model.SuiTongRen + "',");
            }

            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }

            if (model.fileName != null)
            {
                strSql1.Append("fileName,");
                strSql2.Append("'" + model.fileName + "',");
            }

            if (model.fileType != null)
            {
                strSql1.Append("fileType,");
                strSql2.Append("'" + model.fileType + "',");
            }

            if (model.MyPoNo != null)
            {
                strSql1.Append("MyPoNo,");
                strSql2.Append("'" + model.MyPoNo + "',");
            }
            if (model.MyXiShu != null)
            {
                strSql1.Append("MyXiShu,");
                strSql2.Append("" + model.MyXiShu + ",");
            }
            strSql.Append("insert into tb_Dispatching(");
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
        public void Update(VAN_OA.Model.EFrom.tb_Dispatching model, SqlCommand objCommand,string state)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Dispatching set ");
            if (model.Dispatcher != null)
            {
                strSql.Append("Dispatcher='" + model.Dispatcher + "',");
            }
            else
            {
                strSql.Append("Dispatcher= null ,");
            }
            if (model.DisDate != null)
            {
                strSql.Append("DisDate='" + model.DisDate + "',");
            }
            else
            {
                strSql.Append("DisDate= null ,");
            }
            if (model.OutDispater != null)
            {
                strSql.Append("OutDispater=" + model.OutDispater + ",");
            }
            else
            {
                strSql.Append("OutDispater= null ,");
            }
            if (model.GueName != null)
            {
                strSql.Append("GueName='" + model.GueName + "',");
            }
            else
            {
                strSql.Append("GueName= null ,");
            }
            if (model.Tel != null)
            {
                strSql.Append("Tel='" + model.Tel + "',");
            }
            else
            {
                strSql.Append("Tel= null ,");
            }
            if (model.Address != null)
            {
                strSql.Append("Address='" + model.Address + "',");
            }
            else
            {
                strSql.Append("Address= null ,");
            }
            if (model.Contacter != null)
            {
                strSql.Append("Contacter='" + model.Contacter + "',");
            }
            else
            {
                strSql.Append("Contacter= null ,");
            }
            if (model.GoDate != null)
            {
                strSql.Append("GoDate='" + model.GoDate + "',");
            }
            else
            {
                strSql.Append("GoDate= null ,");
            }
            if (model.BackDate != null)
            {
                strSql.Append("BackDate='" + model.BackDate + "',");
            }
            else
            {
                strSql.Append("BackDate= null ,");
            }
            if (model.Question != null)
            {
                strSql.Append("Question='" + model.Question + "',");
            }
            else
            {
                strSql.Append("Question= null ,");
            }
            if (model.QuestionRemark != null)
            {
                strSql.Append("QuestionRemark='" + model.QuestionRemark + "',");
            }
            else
            {
                strSql.Append("QuestionRemark= null ,");
            }
            if (model.SuiTongRen != null)
            {
                strSql.Append("SuiTongRen='" + model.SuiTongRen + "',");
            }

            if (!string.IsNullOrEmpty( model.fileName))
            {
                strSql.Append("fileName='" + model.fileName + "',");
            }
            if (!string.IsNullOrEmpty(model.fileType))
            {
                strSql.Append("fileType='" + model.fileType + "',");
            }

            if (model.NiHours != null)
            {
                strSql.Append("NiHours='" + model.NiHours + "',");
            }
            else
            {
                strSql.Append("NiHours= null ,");
            }

            if (model.NiDate != null)
            {
                strSql.Append("NiDate='" + model.NiDate + "',");
            }
            else
            {
                strSql.Append("NiDate= null ,");
            }
            //strSql.Append("MyXiShu=" + model.MyXiShu + ",");
            if (state == "通过")
            {
                strSql.Append("MyValue=" + model.MyValue + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }




        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(VAN_OA.Model.EFrom.tb_Dispatching model,string state)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Dispatching set ");
            if (model.Dispatcher != null)
            {
                strSql.Append("Dispatcher='" + model.Dispatcher + "',");
            }
            else
            {
                strSql.Append("Dispatcher= null ,");
            }
            if (model.DisDate != null)
            {
                strSql.Append("DisDate='" + model.DisDate + "',");
            }
            else
            {
                strSql.Append("DisDate= null ,");
            }
            if (model.OutDispater != null)
            {
                strSql.Append("OutDispater=" + model.OutDispater + ",");
            }
            else
            {
                strSql.Append("OutDispater= null ,");
            }
            if (model.GueName != null)
            {
                strSql.Append("GueName='" + model.GueName + "',");
            }
            else
            {
                strSql.Append("GueName= null ,");
            }
            if (model.Tel != null)
            {
                strSql.Append("Tel='" + model.Tel + "',");
            }
            else
            {
                strSql.Append("Tel= null ,");
            }
            if (model.Address != null)
            {
                strSql.Append("Address='" + model.Address + "',");
            }
            else
            {
                strSql.Append("Address= null ,");
            }
            if (model.Contacter != null)
            {
                strSql.Append("Contacter='" + model.Contacter + "',");
            }
            else
            {
                strSql.Append("Contacter= null ,");
            }
            if (model.GoDate != null)
            {
                strSql.Append("GoDate='" + model.GoDate + "',");
            }
            else
            {
                strSql.Append("GoDate= null ,");
            }
            if (model.BackDate != null)
            {
                strSql.Append("BackDate='" + model.BackDate + "',");
            }
            else
            {
                strSql.Append("BackDate= null ,");
            }
            if (model.Question != null)
            {
                strSql.Append("Question='" + model.Question + "',");
            }
            else
            {
                strSql.Append("Question= null ,");
            }
            if (model.QuestionRemark != null)
            {
                strSql.Append("QuestionRemark='" + model.QuestionRemark + "',");
            }
            else
            {
                strSql.Append("QuestionRemark= null ,");
            }

            if (model.SuiTongRen != null)
            {
                strSql.Append("SuiTongRen='" + model.SuiTongRen + "',");
            }
            if (!string.IsNullOrEmpty(model.fileName))
            {
                strSql.Append("fileName='" + model.fileName + "',");
            }
            if (!string.IsNullOrEmpty(model.fileType))
            {
                strSql.Append("fileType='" + model.fileType + "',");
            }
            if (model.NiHours != null)
            {
                strSql.Append("NiHours='" + model.NiHours + "',");
            }
            else
            {
                strSql.Append("NiHours= null ,");
            }

            if (model.NiDate != null)
            {
                strSql.Append("NiDate='" + model.NiDate + "',");
            }
            else
            {
                strSql.Append("NiDate= null ,");
            }
            if (state == "通过")
            {
                strSql.Append("MyValue=" + model.MyValue + ",");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            DBHelp.ExeCommand(strSql.ToString());
         
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_Dispatching ");
            strSql.Append(" where Id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_Dispatching GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1  ");
            strSql.Append(" MyXiShu,MyPoNo,MyValue,NiDate,NiHours,fileName,fileType,Id,Dispatcher,DisDate,OutDispater,GueName,Tel,Address,Contacter,GoDate,BackDate,Question,QuestionRemark,SuiTongRen ,proNo");
            strSql.Append(" from tb_Dispatching ");
            strSql.Append(" where Id=" + id + "");

            VAN_OA.Model.EFrom.tb_Dispatching model = null;
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
        public List<VAN_OA.Model.EFrom.tb_Dispatching> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MyXiShu,MyPoNo,MyValue,NiDate,NiHours,fileName,fileType,Id,Dispatcher,DisDate,OutDispater,GueName,Tel,Address,Contacter,GoDate,BackDate,Question,QuestionRemark,SuiTongRen,proNo ");
            strSql.Append(" FROM tb_Dispatching ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.tb_Dispatching> list = new List<VAN_OA.Model.EFrom.tb_Dispatching>();

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
        public VAN_OA.Model.EFrom.tb_Dispatching ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_Dispatching model = new VAN_OA.Model.EFrom.tb_Dispatching();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.Dispatcher = dataReader["Dispatcher"].ToString();
            ojb = dataReader["DisDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DisDate = (DateTime)ojb;
            }
            ojb = dataReader["OutDispater"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OutDispater = Convert.ToInt32(dataReader["OutDispater"]);
            }
            model.GueName = dataReader["GueName"].ToString();
            model.Tel = dataReader["Tel"].ToString();
            model.Address = dataReader["Address"].ToString();
            model.Contacter = dataReader["Contacter"].ToString();
            ojb = dataReader["GoDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoDate = (DateTime)ojb;
            }
            ojb = dataReader["BackDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BackDate = (DateTime)ojb;
            }

            ojb = dataReader["SuiTongRen"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SuiTongRen = ojb.ToString();
            }
            model.Question = dataReader["Question"].ToString();
            model.QuestionRemark = dataReader["QuestionRemark"].ToString();
            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }
            model.fileName = dataReader["fileName"].ToString();
            model.fileType = dataReader["fileType"].ToString();
            ojb = dataReader["NiDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.NiDate = Convert.ToDateTime(ojb);
            }
            ojb = dataReader["NiHours"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.NiHours = Convert.ToInt32(ojb);
            }
            model.MyPoNo = dataReader["MyPoNo"].ToString();
            model.MyValue =Convert.ToDecimal(dataReader["MyValue"]);
            model.MyXiShu = Convert.ToDecimal(dataReader["MyXiShu"]);            
            return model;
        }

        /// <summary>
        /// 提前两天的信息
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<tb_Dispatching> GetListTwoDays(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"  select tb_Dispatching.*,loginName from tb_Dispatching left join tb_User on tb_User.ID=tb_Dispatching.OutDispater");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by DisDate ");
            List<tb_Dispatching> list = new List<tb_Dispatching>();

            int result = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.EFrom.tb_Dispatching model = new VAN_OA.Model.EFrom.tb_Dispatching();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        model.Dispatcher = dataReader["Dispatcher"].ToString();
                        ojb = dataReader["DisDate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.DisDate = (DateTime)ojb;
                        }
                        ojb = dataReader["OutDispater"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.OutDispater = Convert.ToInt32(dataReader["OutDispater"]);
                        }
                        model.GueName = dataReader["GueName"].ToString();
                        model.Tel = dataReader["Tel"].ToString();
                        model.Address = dataReader["Address"].ToString();
                        model.Contacter = dataReader["Contacter"].ToString();
                        ojb = dataReader["GoDate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoDate = (DateTime)ojb;
                        }
                        ojb = dataReader["BackDate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.BackDate = (DateTime)ojb;
                        }

                        ojb = dataReader["SuiTongRen"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SuiTongRen = ojb.ToString();
                        }
                        model.Question = dataReader["Question"].ToString();
                        model.QuestionRemark = dataReader["QuestionRemark"].ToString();

                        ojb = dataReader["loginName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.OutdispaterName = ojb.ToString();
                        }
                        ojb = dataReader["ProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ProNo = ojb.ToString();
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
        public List<VAN_OA.Model.ReportForms.DispatchingRep> GetListArrayReport(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"  select tb_Dispatching.*,loginName from tb_Dispatching left join tb_User on tb_User.ID=tb_Dispatching.OutDispater ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by DisDate desc");
            List<VAN_OA.Model.ReportForms.DispatchingRep> list = new List<VAN_OA.Model.ReportForms.DispatchingRep>();

            int result = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        object ojb;
                        VAN_OA.Model.ReportForms.DispatchingRep model = new VAN_OA.Model.ReportForms.DispatchingRep();
                        if (list.Count == 0)
                        {
                            result = 0;
                        }
                        else
                        {
                            ojb = dataReader["DisDate"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                DateTime dateTime = Convert.ToDateTime(ojb);
                                model.Desc = dateTime.Year + "-" + dateTime.Month;
                                Model.ReportForms.DispatchingRep rep = list[list.Count - 1];
                                if (rep.Desc.Contains('-') && rep.Desc != model.Desc)
                                {
                                    result = 0;
                                }
                                else
                                {
                                    result = 1;
                                }
                            }

                        }




                        if (result != 0)
                        {


                            model.GuestAddress = dataReader["Address"].ToString();

                            ojb = dataReader["BackDate"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.BackTime = Convert.ToDateTime(ojb).ToShortDateString()+" "+ Convert.ToDateTime(ojb).ToShortTimeString();
                            }
                            ojb = dataReader["GoDate"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.GoTime = Convert.ToDateTime(ojb).ToShortDateString() + " " + Convert.ToDateTime(ojb).ToShortTimeString();
                            }
                            model.Content = dataReader["Question"].ToString();


                            ojb = dataReader["loginName"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.OutDispater = Convert.ToString(dataReader["loginName"]);
                            }
                             
                            model.Name = dataReader["Dispatcher"].ToString();

                            ojb = dataReader["DisDate"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                DateTime dateTime = Convert.ToDateTime(ojb);
                                model.Mouth = dateTime.Day.ToString();
                                model.Desc = dateTime.Year + "-" + dateTime.Month;
                            }
                            ojb = dataReader["SuiTongRen"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.SuiTongRen = Convert.ToString(ojb);
                            }
                            ojb = dataReader["Id"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                string id = Convert.ToString(dataReader["id"]);

                                ojb = dataReader["fileName"];
                                if (ojb != null && ojb != DBNull.Value)
                                {
                                    string fileName = Convert.ToString(dataReader["fileName"]);
                                    model.FileName = fileName;
                                    if (!string.IsNullOrEmpty(fileName))
                                    {
                                        model.HiddFileName=fileName.Substring(0, fileName.LastIndexOf('.')) + "_" + id + fileName.Substring(fileName.LastIndexOf('.'));
                                    }
                                }

                            }

                        }
                        else
                        {

                            VAN_OA.Model.ReportForms.DispatchingRep model1 = new VAN_OA.Model.ReportForms.DispatchingRep();

                            model1.GuestAddress = "客户地址";
                            model1.BackTime = "回来时间";
                            model1.GoTime = "外出时间";
                            model1.Content = "工作内容";
                            model1.Name = "派工人";
                            model1.OutDispater = "被派工人";
                            model1.SuiTongRen = "随同人";
                            model1.FileName = "附件";
                            ojb = dataReader["DisDate"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                DateTime dateTime = Convert.ToDateTime(ojb);
                                model1.Mouth = dateTime.Year + "-" + dateTime.Month;
                            }

                            list.Add(model1);
                            model.GuestAddress = dataReader["Address"].ToString();

                            ojb = dataReader["BackDate"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.BackTime = Convert.ToDateTime(ojb).ToShortDateString() + " " + Convert.ToDateTime(ojb).ToShortTimeString();
                            }
                            ojb = dataReader["GoDate"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.GoTime = Convert.ToDateTime(ojb).ToShortDateString() + " " + Convert.ToDateTime(ojb).ToShortTimeString();
                            }
                            model.Content = dataReader["Question"].ToString();
                            ojb = dataReader["loginName"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.OutDispater = Convert.ToString(dataReader["loginName"]);
                            }
                            model.Name = dataReader["Dispatcher"].ToString();

                            ojb = dataReader["DisDate"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                DateTime dateTime = Convert.ToDateTime(ojb);
                                model.Mouth = dateTime.Day.ToString();
                                model.Desc = dateTime.Year + "-" + dateTime.Month;
                            }

                            ojb = dataReader["SuiTongRen"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.SuiTongRen = Convert.ToString(ojb); 
                            }

                            ojb = dataReader["Id"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                string id = Convert.ToString(dataReader["id"]);

                                ojb = dataReader["fileName"];
                                if (ojb != null && ojb != DBNull.Value)
                                {
                                    string fileName = Convert.ToString(dataReader["fileName"]);
                                    model.FileName = fileName;
                                    if (!string.IsNullOrEmpty(fileName))
                                    {
                                        model.HiddFileName = fileName.Substring(0, fileName.LastIndexOf('.')) + "_" + id + fileName.Substring(fileName.LastIndexOf('.'));
                                    }
                                }

                            }
                        }


                        list.Add(model);
                    }
                }
            }
            return list;
        }
    }
}
