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
    public class Tb_ProjectInvService
    {

        public bool RestatePro(int e_Id, int proId,int doPer)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;


                try
                {
                    int audPer = 0;
                    int prosIds = 0;

                    //查询最后一次审批人的信息
                    string sql = string.Format("select max(doTime),audPer,consignor,prosIds from tb_EForms where e_Id={0} and idea<>'工程重新启动' group by audPer,consignor,prosIds", e_Id);
                    objCommand.CommandText = sql;
                    using (SqlDataReader dataReader = objCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            object ojb;
                            ojb = dataReader["audPer"];
                            if (ojb != null && ojb != DBNull.Value && ojb.ToString() != "0")
                            {
                                audPer = (int)ojb;
                            }

                            ojb = dataReader["consignor"];
                            if (ojb != null && ojb != DBNull.Value && ojb.ToString() != "0")
                            {
                                audPer = (int)ojb;
                            }

                            prosIds = Convert.ToInt32(dataReader["prosIds"]);


                        }

                        dataReader.Close();
                    }

                    if (audPer != 0 && prosIds != 0)
                    {
                        //将单子的状态改成‘未完工’
                        //将单子恢复，将单子的状态改成‘未完工’，在审批流程中，记录启动人的信息
                        sql = "update Tb_ProjectInv set state='未完工' where Id=" + proId;
                        sql += string.Format(";update tb_EForm set toPer={0} , toProsId={1} ,state='执行中' where id={2}", audPer, prosIds, e_Id);

                        sql += string.Format(";insert into tb_EForms values({0},{1},0,getdate(),'工程重新启动','通过',-1,'启动人')", e_Id, doPer);

                        objCommand.CommandText = sql;
                        objCommand.ExecuteNonQuery();

                    }
                    else
                    {
                        return false;
                    }
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
        public bool updateTran(VAN_OA.Model.EFrom.Tb_ProjectInv model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms, List<Tb_ProjectInvs> proInvs, string IDS)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;


                Tb_ProjectInvsService prosInvsSer = new Tb_ProjectInvsService();
                try
                {

                    objCommand.Parameters.Clear();
                    Update(model, objCommand);
                    tb_EFormService eformSer = new tb_EFormService();
                    eformSer.Update(eform, objCommand);
                    tb_EFormsService eformsSer = new tb_EFormsService();
                    eformsSer.Add(forms, objCommand);


                    for (int i = 0; i < proInvs.Count; i++)
                    {
                        proInvs[i].PId = model.Id;

                        if (proInvs[i].IfUpdate == true && proInvs[i].Id != 0)
                        {
                            prosInvsSer.Update(proInvs[i], objCommand);
                        }
                        else if (proInvs[i].Id == 0)
                        {
                            prosInvsSer.Add(proInvs[i], objCommand);

                        }
                    }
                        if (IDS != "")
                        {
                            IDS = IDS.Substring(0, IDS.Length - 1);
                            prosInvsSer.DeleteByIds(IDS, objCommand);
                        }                        
                     
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

        public bool updateTran(VAN_OA.Model.EFrom.Tb_ProjectInv model,List<Tb_ProjectInvs> proInvs, string IDS)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;


                Tb_ProjectInvsService prosInvsSer = new Tb_ProjectInvsService();
                try
                {

                    objCommand.Parameters.Clear();
                    Update(model, objCommand);



                    for (int i = 0; i < proInvs.Count; i++)
                    {
                        proInvs[i].PId = model.Id;

                        if (proInvs[i].IfUpdate == true && proInvs[i].Id != 0)
                        {
                            prosInvsSer.Update(proInvs[i], objCommand);
                        }
                        else if (proInvs[i].Id == 0)
                        {
                            prosInvsSer.Add(proInvs[i], objCommand);

                        }
                    }
                    if (IDS != "")
                    {
                        IDS = IDS.Substring(0, IDS.Length - 1);
                        prosInvsSer.DeleteByIds(IDS, objCommand);
                    }
                     
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

        public int addTran(VAN_OA.Model.EFrom.Tb_ProjectInv model, VAN_OA.Model.EFrom.tb_EForm eform, List<Tb_ProjectInvs> proInvs)
        {
            int id = 0;
            int MainId = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                Tb_ProjectInvsService proInvsSer = new Tb_ProjectInvsService();
                try
                {
                    tb_EFormService eformSer = new tb_EFormService();
                    objCommand.Parameters.Clear();
                    string proNo = eformSer.GetAllE_No("Tb_ProjectInv", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;

                    id = Add(model, objCommand);
                    MainId = id;
                   
                    eform.allE_id = id;
                    eformSer.Add(eform, objCommand);
                    for (int i = 0; i < proInvs.Count; i++)
                    {
                        proInvs[i].PId = id;
                        proInvsSer.Add(proInvs[i], objCommand);                     

                    }
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
        public int Add(VAN_OA.Model.EFrom.Tb_ProjectInv model, SqlCommand objCommand)
        {

            //string MaxCardNo = "";
            //string sql = "select  right('0000000000'+(convert(varchar,(convert(int,right(max(ProNo),4))+1))),4) FROM  Tb_ProjectInv;";
            //objCommand.CommandText = sql.ToString();
            //object objMax = objCommand.ExecuteScalar();
            //if (objMax != null && objMax.ToString() != "")
            //{
            //    MaxCardNo = DateTime.Now.Year.ToString() + objMax.ToString();
            //}
            //else
            //{
            //    MaxCardNo = DateTime.Now.Year.ToString() + "0001";
            //}


            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.UserId != null)
            {
                strSql1.Append("UserId,");
                strSql2.Append("" + model.UserId + ",");
            }

            if (model.CreateTime != null)
            {
                strSql1.Append("CreateTime,");
                strSql2.Append("'" + model.CreateTime + "',");
            }
            if (model.ProNo != "")
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }
            if (model.ProName != null)
            {
                strSql1.Append("ProName,");
                strSql2.Append("'" + model.ProName + "',");
            }

            strSql1.Append("State,");
            strSql2.Append("'" + model.State + "',");

            strSql1.Append("GuestId,");
            strSql2.Append("" + model.GuestId + ",");


            strSql.Append("insert into Tb_ProjectInv(");
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
        public void Update(VAN_OA.Model.EFrom.Tb_ProjectInv model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Tb_ProjectInv set ");
            
            if (model.CreateTime != null)
            {
                strSql.Append("CreateTime='" + model.CreateTime + "',");
            }
            else
            {
                strSql.Append("CreateTime= null ,");
            }
            
            if (model.ProName != null)
            {
                strSql.Append("ProName='" + model.ProName + "',");
            }
            else
            {
                strSql.Append("ProName= null ,");
            }

            strSql.Append("State='" + model.State + "',");
            strSql.Append("GuestId=" + model.GuestId + ",");

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
            strSql.Append("delete from Tb_ProjectInv ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.Tb_ProjectInv GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Tb_ProjectInv.Id,UserId,CreateTime,ProNo,ProName,loginName,State,GuestId ");
            strSql.Append(" from Tb_ProjectInv left join tb_User on tb_User.id=Tb_ProjectInv.UserId");
            strSql.Append(" where Tb_ProjectInv.Id=" + id + "");

            VAN_OA.Model.EFrom.Tb_ProjectInv model = null;
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
        public List<VAN_OA.Model.EFrom.Tb_ProjectInv> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Tb_ProjectInv.Id,UserId,CreateTime,ProNo,ProName,loginName,State,GuestId ");
            strSql.Append(" from Tb_ProjectInv left join tb_User on tb_User.id=Tb_ProjectInv.UserId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.Tb_ProjectInv> list = new List<VAN_OA.Model.EFrom.Tb_ProjectInv>();

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
        public VAN_OA.Model.EFrom.Tb_ProjectInv ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.Tb_ProjectInv model = new VAN_OA.Model.EFrom.Tb_ProjectInv();
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
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }
            model.ProNo = dataReader["ProNo"].ToString();
            model.ProName = dataReader["ProName"].ToString();

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LoginName = ojb.ToString();
            }
            ojb = dataReader["State"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.State = ojb.ToString();
            }

            ojb = dataReader["GuestId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestId =Convert.ToInt32(ojb);
            }   

            return model;
        }



    }
}
