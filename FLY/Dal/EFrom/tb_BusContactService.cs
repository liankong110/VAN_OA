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
    public class tb_BusContactService
    {
        public bool updateTran(VAN_OA.Model.EFrom.tb_BusContact model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
        public int addTran(VAN_OA.Model.EFrom.tb_BusContact model, VAN_OA.Model.EFrom.tb_EForm eform)
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
                    string proNo = eformSer.GetAllE_No("tb_BusContact", objCommand);
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
        public int Add(VAN_OA.Model.EFrom.tb_BusContact model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Name != null)
            {
                strSql1.Append("Name,");
                strSql2.Append("'" + model.Name + "',");
            }
            if (model.DepartName != null)
            {
                strSql1.Append("DepartName,");
                strSql2.Append("'" + model.DepartName + "',");
            }
            if (model.DateTime != null)
            {
                strSql1.Append("DateTime,");
                strSql2.Append("'" + model.DateTime + "',");
            }
            if (model.ContactUnit != null)
            {
                strSql1.Append("ContactUnit,");
                strSql2.Append("'" + model.ContactUnit + "',");
            }
            if (model.Contacer != null)
            {
                strSql1.Append("Contacer,");
                strSql2.Append("'" + model.Contacer + "',");
            }
            if (model.Tel != null)
            {
                strSql1.Append("Tel,");
                strSql2.Append("'" + model.Tel + "',");
            }

            if (model.Gotime != null)
            {
                strSql1.Append("Gotime,");
                strSql2.Append("'" + model.Gotime + "',");
            }
            if (model.BackTime != null)
            {
                strSql1.Append("BackTime,");
                strSql2.Append("'" + model.BackTime + "',");
            }
            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }
            if (model.AppDate != null)
            {
                strSql1.Append("AppDate,");
                strSql2.Append("'" + model.AppDate + "',");
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
            
            strSql.Append("insert into tb_BusContact(");
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
        public void Update(VAN_OA.Model.EFrom.tb_BusContact model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_BusContact set ");
            if (model.Name != null)
            {
                strSql.Append("Name='" + model.Name + "',");
            }
            else
            {
                strSql.Append("Name= null ,");
            }
            if (model.DepartName != null)
            {
                strSql.Append("DepartName='" + model.DepartName + "',");
            }
            else
            {
                strSql.Append("DepartName= null ,");
            }
            if (model.DateTime != null)
            {
                strSql.Append("DateTime='" + model.DateTime + "',");
            }
            else
            {
                strSql.Append("DateTime= null ,");
            }
            if (model.ContactUnit != null)
            {
                strSql.Append("ContactUnit='" + model.ContactUnit + "',");
            }
            else
            {
                strSql.Append("ContactUnit= null ,");
            }
            if (model.Contacer != null)
            {
                strSql.Append("Contacer='" + model.Contacer + "',");
            }
            else
            {
                strSql.Append("Contacer= null ,");
            }
            if (model.Tel != null)
            {
                strSql.Append("Tel='" + model.Tel + "',");
            }
            else
            {
                strSql.Append("Tel= null ,");
            }
            if (model.Gotime != null)
            {
                strSql.Append("Gotime='" + model.Gotime + "',");
            }
            else
            {
                strSql.Append("Gotime= null ,");
            }
            if (model.BackTime != null)
            {
                strSql.Append("BackTime='" + model.BackTime + "',");
            }
            else
            {
                strSql.Append("BackTime= null ,");
            }
            if (!string.IsNullOrEmpty(model.fileName))
            {
                strSql.Append("fileName='" + model.fileName + "',");
            }
            if (!string.IsNullOrEmpty(model.fileType))
            {
                strSql.Append("fileType='" + model.fileType + "',");
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
            strSql.Append("delete from tb_BusContact ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_BusContact GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" fileName,fileType,id,Name,DepartName,DateTime,ContactUnit,Contacer,Tel,Gotime,BackTime,proNo,AppDate ");
            strSql.Append(" from tb_BusContact ");
            strSql.Append(" where id=" + id + "");

            VAN_OA.Model.EFrom.tb_BusContact model = null;
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
        public List<VAN_OA.Model.EFrom.tb_BusContact> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select fileName,fileType,id,Name,DepartName,DateTime,ContactUnit,Contacer,Tel,Gotime,BackTime,proNo,AppDate ");
            strSql.Append(" FROM tb_BusContact ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.tb_BusContact> list = new List<VAN_OA.Model.EFrom.tb_BusContact>();

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
        public VAN_OA.Model.EFrom.tb_BusContact ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_BusContact model = new VAN_OA.Model.EFrom.tb_BusContact();
            object ojb;
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.id = (int)ojb;
            }
            model.Name = dataReader["Name"].ToString();
            model.DepartName = dataReader["DepartName"].ToString();
            ojb = dataReader["DateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DateTime = (DateTime)ojb;
            }
            model.ContactUnit = dataReader["ContactUnit"].ToString();
            model.Contacer = dataReader["Contacer"].ToString();
            model.Tel = dataReader["Tel"].ToString();
            ojb = dataReader["Gotime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Gotime = (DateTime)ojb;
            }
            ojb = dataReader["BackTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BackTime = (DateTime)ojb;
            }
            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }
            ojb = dataReader["AppDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppDate =Convert.ToDateTime(ojb);
            }

            ojb = dataReader["fileName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.fileName = ojb.ToString();
            }

            ojb = dataReader["fileType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.fileType = ojb.ToString();
            }
            return model;
        }
        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.ReportForms.BusContactRep> GetListArrayReport(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"  select * from tb_BusContact");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by DateTime desc");
            List<VAN_OA.Model.ReportForms.BusContactRep> list = new List<VAN_OA.Model.ReportForms.BusContactRep>();

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
                        VAN_OA.Model.ReportForms.BusContactRep model = new VAN_OA.Model.ReportForms.BusContactRep();
                        if (list.Count == 0)
                        {
                            result = 0;
                        }
                        else
                        {
                            ojb = dataReader["DateTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                DateTime dateTime = Convert.ToDateTime(ojb);
                                model.Desc = dateTime.Year + "-" + dateTime.Month;
                                Model.ReportForms.BusContactRep rep = list[list.Count - 1];
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


                            model.AppName = dataReader["Name"].ToString();

                            ojb = dataReader["backTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.BackTime = Convert.ToDateTime(ojb).ToLongTimeString();
                            }
                            ojb = dataReader["GoTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.GoTime = Convert.ToDateTime(ojb).ToLongTimeString();
                            }
                            model.ContactUnit = dataReader["ContactUnit"].ToString();
                            model.Contacer = dataReader["Contacer"].ToString();
                            

                            ojb = dataReader["DateTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                DateTime dateTime = Convert.ToDateTime(ojb);
                                model.Mouth = dateTime.Day.ToString();
                                model.Desc = dateTime.Year + "-" + dateTime.Month;
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
                        else
                        {

                            VAN_OA.Model.ReportForms.BusContactRep model1 = new VAN_OA.Model.ReportForms.BusContactRep();

                            model1.AppName = "申请人";
                            model1.BackTime = "回来时间";
                            model1.GoTime = "外出时间";
                            model1.Contacer = "联系人";
                            model1.ContactUnit = "外出联系单位";
                            model1.FileName = "附件";
                            ojb = dataReader["dateTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                DateTime dateTime = Convert.ToDateTime(ojb);
                                model1.Mouth = dateTime.Year + "-" + dateTime.Month;
                            }

                            list.Add(model1);
                            model.AppName = dataReader["Name"].ToString();

                            ojb = dataReader["backTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.BackTime = Convert.ToDateTime(ojb).ToLongTimeString();
                            }
                            ojb = dataReader["GoTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.GoTime = Convert.ToDateTime(ojb).ToLongTimeString();
                            }
                            model.ContactUnit = dataReader["ContactUnit"].ToString();
                            model.Contacer = dataReader["Contacer"].ToString();


                            ojb = dataReader["DateTime"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                DateTime dateTime = Convert.ToDateTime(ojb);
                                model.Mouth = dateTime.Day.ToString();
                                model.Desc = dateTime.Year + "-" + dateTime.Month;
                            } ojb = dataReader["Id"];
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
