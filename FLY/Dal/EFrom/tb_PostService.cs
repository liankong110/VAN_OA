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
    public class tb_PostService
    {
        public bool updateTran(VAN_OA.Model.EFrom.tb_Post model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms)
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
        public int addTran(VAN_OA.Model.EFrom.tb_Post model, VAN_OA.Model.EFrom.tb_EForm eform)
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

                    string proNo = eformSer.GetAllE_No("tb_Post", objCommand);
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
        public int Add(VAN_OA.Model.EFrom.tb_Post model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.AppName != null)
            {
                strSql1.Append("AppName,");
                strSql2.Append("" + model.AppName + ",");
            }
            if (model.AppTime != null)
            {
                strSql1.Append("AppTime,");
                strSql2.Append("'" + model.AppTime + "',");
            }
            if (model.PostAddress != null)
            {
                strSql1.Append("PostAddress,");
                strSql2.Append("'" + model.PostAddress + "',");
            }
            if (model.ToPer != null)
            {
                strSql1.Append("ToPer,");
                strSql2.Append("'" + model.ToPer + "',");
            }
            if (model.Tel != null)
            {
                strSql1.Append("Tel,");
                strSql2.Append("'" + model.Tel + "',");
            }
            if (model.WuliuName != null)
            {
                strSql1.Append("WuliuName,");
                strSql2.Append("'" + model.WuliuName + "',");
            }
            if (model.PostCode != null)
            {
                strSql1.Append("PostCode,");
                strSql2.Append("'" + model.PostCode + "',");
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


            if (model.FromPer != null)
            {
                strSql1.Append("FromPer,");
                strSql2.Append("'" + model.FromPer + "',");
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

            strSql.Append("insert into tb_Post(");
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
        public void Update(VAN_OA.Model.EFrom.tb_Post model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Post set ");
            //if (model.AppName != null)
            //{
            //    strSql.Append("AppName=" + model.AppName + ",");
            //}
            //if (model.AppTime != null)
            //{
            //    strSql.Append("AppTime='" + model.AppTime + "',");
            //}
            if (model.PostAddress != null)
            {
                strSql.Append("PostAddress='" + model.PostAddress + "',");
            }
            if (model.ToPer != null)
            {
                strSql.Append("ToPer='" + model.ToPer + "',");
            }
            if (model.Tel != null)
            {
                strSql.Append("Tel='" + model.Tel + "',");
            }
            else
            {
                strSql.Append("Tel= null ,");
            }
            if (model.WuliuName != null)
            {
                strSql.Append("WuliuName='" + model.WuliuName + "',");
            }
            else
            {
                strSql.Append("WuliuName= null ,");
            }
            if (model.PostCode != null)
            {
                strSql.Append("PostCode='" + model.PostCode + "',");
            }
            else
            {
                strSql.Append("PostCode= null ,");
            }
            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }

            if (model.FromPer != null)
            {
                strSql.Append("FromPer='" + model.FromPer + "',");
            }
            else
            {
                strSql.Append("FromPer= null ,");
            }

            if (model.PostContext != null)
            {
                strSql.Append("PostContext='" + model.PostContext + "',");
            }
            else
            {
                strSql.Append("PostContext= null ,");
            }
            if (model.PostFrom != null)
            {
                strSql.Append("PostFrom=" + (Convert.ToBoolean(model.PostFrom) ? 1 : 0) + ",");
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
                strSql.Append("PostTo=" + (Convert.ToBoolean(model.PostTo) ? 1 : 0) + ",");
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
            if (model.PostRemark != null)
            {
                strSql.Append("PostRemark='" + model.PostRemark + "',");
            }
            else
            {
                strSql.Append("PostRemark= null ,");
            }


            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_Post ");
            strSql.Append(" where Id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.tb_Post GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" tb_Post.Id,AppName,AppTime,PostAddress,ToPer,Tel,WuliuName,PostCode,Remark,loginName,proNo ,FromPer,PostContext,PostFrom,PostFromAddress,PostTo,PostToAddress,PostTotal,PostRemark,POGuestName,PONo,POName ");
            strSql.Append(" from tb_Post left join tb_User on tb_Post.AppName=tb_User.Id");
            strSql.Append(" where tb_Post.Id=" + id + "");

            VAN_OA.Model.EFrom.tb_Post model = null;
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
        public List<VAN_OA.Model.EFrom.tb_Post> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AE,Tb_DispatchList.PostTotal as MyTotal,tb_Post.Id,AppName,AppTime,PostAddress,ToPer,Tel,WuliuName,PostCode,Remark ,loginName,proNo,FromPer,tb_Post.PostContext,tb_Post.PostFrom,tb_Post.PostFromAddress,tb_Post.PostTo,tb_Post.PostToAddress,tb_Post.PostTotal,tb_Post.PostRemark,tb_Post.POGuestName,tb_Post.PONo,tb_Post.POName ");
            strSql.Append(" FROM tb_Post join tb_User on tb_Post.AppName=tb_User.Id left join Tb_DispatchList ON tb_Post.ID=Tb_DispatchList.Post_Id AND Tb_DispatchList.state='通过'");
            strSql.Append("  LEFT JOIN ( SELECT PONo AS T_PONO,AE FROM CG_POOrder where IFZhui=0 ) AS T_POOrder on T_POOrder.T_PONO=tb_Post.PONo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by  AppTime desc");
            List<VAN_OA.Model.EFrom.tb_Post> list = new List<VAN_OA.Model.EFrom.tb_Post>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        tb_Post model = ReaderBind(dataReader);
                        object obj = dataReader["MyTotal"];
                        if (obj != null && obj != DBNull.Value)
                        {
                            model.Total = Convert.ToDecimal(obj);
                        }
                        obj = dataReader["AE"];
                        if (obj != null && obj != DBNull.Value)
                        {
                            model.AE = obj.ToString();
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }







        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.EFrom.tb_Post ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.tb_Post model = new VAN_OA.Model.EFrom.tb_Post();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["AppName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppName = (int)ojb;
            }
            ojb = dataReader["AppTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AppTime = (DateTime)ojb;
            }
            model.PostAddress = dataReader["PostAddress"].ToString();
            model.ToPer = dataReader["ToPer"].ToString();
            model.Tel = dataReader["Tel"].ToString();
            model.WuliuName = dataReader["WuliuName"].ToString();
            model.PostCode = dataReader["PostCode"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LoginName = ojb.ToString();
            }
            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }


            ojb = dataReader["FromPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FromPer = ojb.ToString();
            }

            ojb = dataReader["PostContext"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PostContext = ojb.ToString();
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



            ojb = dataReader["PostToAddress"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PostToAddress = ojb.ToString();
            }


            ojb = dataReader["PostTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PostTotal = (decimal)ojb;
            }


            ojb = dataReader["PostRemark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PostRemark = ojb.ToString();
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
            return model;
        }


    }
}
