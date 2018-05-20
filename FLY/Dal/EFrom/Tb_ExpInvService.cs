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
    public class Tb_ExpInvService
    {
        public bool updateTran(VAN_OA.Model.EFrom.Tb_ExpInv model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms, List<Tb_ExpInvs> proInvs, string IDS)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;


                Tb_ExpInvsService prosInvsSer = new Tb_ExpInvsService();
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
                        //else if (proInvs[i].Id == 0)
                        //{
                        //    prosInvsSer.Add(proInvs[i], objCommand);
                        //}
                       
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

        public bool updateTran(VAN_OA.Model.EFrom.Tb_ExpInv model, List<Tb_ExpInvs> proInvs, string IDS)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;


                Tb_ExpInvsService prosInvsSer = new Tb_ExpInvsService();
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

        public int addTran(VAN_OA.Model.EFrom.Tb_ExpInv model, VAN_OA.Model.EFrom.tb_EForm eform, List<Tb_ExpInvs> proInvs)
        {
            int id = 0;
            int MainId = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                Tb_ExpInvsService proInvsSer = new Tb_ExpInvsService();
                try
                {
                    tb_EFormService eformSer = new tb_EFormService();
                    objCommand.Parameters.Clear();
                    string proNo = eformSer.GetAllE_No("Tb_ExpInv", objCommand);
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

        public string GetAllE_No(SqlCommand objCommand)
        {
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(EventNo),5))+1))),5) FROM  Tb_ExpInv where EventNo like 'B{0}%';",
                 DateTime.Now.Year);
            objCommand.CommandText = sql.ToString();
            object objMax = objCommand.ExecuteScalar();
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = "B"+DateTime.Now.Year.ToString() + objMax.ToString();
            }
            else
            {
                MaxCardNo = "B"+DateTime.Now.Year.ToString() + "00001";
            }

            return MaxCardNo;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.EFrom.Tb_ExpInv model, SqlCommand objCommand)
        {




            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.CreateUserId != null)
            {
                strSql1.Append("CreateUserId,");
                strSql2.Append("" + model.CreateUserId + ",");
            }
            if (model.ExpTime != null)
            {
                strSql1.Append("ExpTime,");
                strSql2.Append("'" + model.ExpTime + "',");
            }
            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }

            strSql1.Append("IfOutGoods,");
            strSql2.Append("0,");

            strSql1.Append("EventNo,");
            strSql2.Append("'"+GetAllE_No(objCommand)+"',");


            strSql.Append("insert into Tb_ExpInv(");
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
        public void Update(VAN_OA.Model.EFrom.Tb_ExpInv model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Tb_ExpInv set ");
          
            if (model.ExpTime != null)
            {
                strSql.Append("ExpTime='" + model.ExpTime + "',");
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
            strSql.Append("delete from Tb_ExpInv ");
            strSql.Append(" where Id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.EFrom.Tb_ExpInv GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Tb_ExpInv.Id,CreateUserId,ExpTime,ProNo,loginName,loginIPosition,EventNo,IfOutGoods,OutTime ");
            strSql.Append(" from Tb_ExpInv left join tb_User on tb_User.id=Tb_ExpInv.CreateUserId");
            strSql.Append(" where Tb_ExpInv.Id=" + id + "");

            VAN_OA.Model.EFrom.Tb_ExpInv model = null;
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
        public List<VAN_OA.Model.EFrom.Tb_ExpInv> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Tb_ExpInv.Id,CreateUserId,ExpTime,ProNo,loginName,loginIPosition,EventNo,IfOutGoods,OutTime ");
            strSql.Append(" from Tb_ExpInv left join tb_User on tb_User.id=Tb_ExpInv.CreateUserId");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.EFrom.Tb_ExpInv> list = new List<VAN_OA.Model.EFrom.Tb_ExpInv>();

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
        public VAN_OA.Model.EFrom.Tb_ExpInv ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.EFrom.Tb_ExpInv model = new VAN_OA.Model.EFrom.Tb_ExpInv();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            ojb = dataReader["CreateUserId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUserId = (int)ojb;
            }
            ojb = dataReader["ExpTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ExpTime = (DateTime)ojb;
            }
            model.ProNo = dataReader["ProNo"].ToString();

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LoginName = ojb.ToString();
            }
            ojb = dataReader["loginIPosition"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DepartMent = ojb.ToString();
            }

            ojb = dataReader["IfOutGoods"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IfOutGoods =Convert.ToBoolean(ojb);
            }

            ojb = dataReader["OutTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OutTime =Convert.ToDateTime(ojb);
            }

            ojb = dataReader["EventNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.EventNo = ojb.ToString();
            }

           

            return model;
        }



    }
}
