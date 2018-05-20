using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAN_OA.Model.JXC;
using System.Data.SqlClient;
 
using System.Text;
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.EFrom;
using System.Data;



namespace VAN_OA.Dal.JXC
{
    public class Sell_OrderFPBackService
    {
        public bool updateTran(VAN_OA.Model.JXC.Sell_OrderFPBack model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms, List<Sell_OrderFPBacks> orders, string IDS)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {

                    decimal total = 0;
                    foreach (var m in orders)
                    {
                        total += m.GoodSellPriceTotal;
                    }
                    model.Total = total;
                    System.Collections.Hashtable hs = new System.Collections.Hashtable();
                    objCommand.Parameters.Clear();
                    model.Status = eform.state;
                    Update(model, objCommand);
                    tb_EFormService eformSer = new tb_EFormService();
                    eformSer.Update(eform, objCommand);
                    tb_EFormsService eformsSer = new tb_EFormsService();
                    eformsSer.Add(forms, objCommand);
                    //====删除所有子单据
                    string delete = "delete from Sell_OrderFPBacks where id="+model.Id;
                    objCommand.CommandText = delete;
                    objCommand.ExecuteNonQuery();
                    //====

                    Sell_OrderFPBacksService OrdersSer = new Sell_OrderFPBacksService();
                    for (int i = 0; i < orders.Count; i++)
                    {
                        orders[i].id = model.Id;
                        OrdersSer.Add(orders[i], objCommand);
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
        public int addTran(VAN_OA.Model.JXC.Sell_OrderFPBack model, VAN_OA.Model.EFrom.tb_EForm eform, List<Sell_OrderFPBacks> orders, out int MainId)
        {
            int id = 0;
            MainId = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                Sell_OrderFPBacksService OrdersSer = new Sell_OrderFPBacksService();
                
                
                try
                {


                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("Sell_OrderFPBack", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;

                    decimal total = 0;
                    foreach (var m in orders)
                    {
                        total += m.GoodSellPriceTotal;
                    }
                    model.Total = total;
                    model.Status = eform.state;
                    id = Add(model, objCommand);
                    MainId = id;

                    eform.allE_id = id;
                    eformSer.Add(eform, objCommand);

                    
                    for (int i = 0; i < orders.Count; i++)
                    {
                        orders[i].id = id;
                        OrdersSer.Add(orders[i], objCommand); 
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
        public int Add(VAN_OA.Model.JXC.Sell_OrderFPBack model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.CreateUserId != null)
            {
                strSql1.Append("CreateUserId,");
                strSql2.Append("" + model.CreateUserId + ",");
            }
            
            strSql1.Append("CreateTime,");
            strSql2.Append("getdate(),");
           
            if (model.FPBackTime != null)
            {
                strSql1.Append("FPBackTime,");
                strSql2.Append("'" + model.FPBackTime + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestNAME,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.FPBackType != null)
            {
                strSql1.Append("FPBackType,");
                strSql2.Append("" + model.FPBackType + ",");
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
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("'" + model.Status + "',");
            }
            if (model.FPNo != null)
            {
                strSql1.Append("FPNo,");
                strSql2.Append("'" + model.FPNo + "',");
            }
            
            strSql1.Append("Total,");
            strSql2.Append("" + model.Total + ",");

            strSql1.Append("PId,");
            strSql2.Append("" + model.PId + ",");
            
            strSql.Append("insert into Sell_OrderFPBack(");
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
        public void Update(VAN_OA.Model.JXC.Sell_OrderFPBack model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sell_OrderFPBack set ");

            if (model.Remark != null)
            {
                strSql.Append("Remark='" + model.Remark + "',");
            }
            else
            {
                strSql.Append("Remark= null ,");
            }
            if (model.Status != null)
            {
                strSql.Append("Status='" + model.Status + "',");
            }
            if (model.FPNo != null)
            {
                strSql.Append("FPNo='" + model.FPNo + "',");
            }
            else
            {
                strSql.Append("FPNo= null ,");
            }

            if (model.FPBackType != null)
            {
                strSql.Append("FPBackType=" + model.FPBackType + ",");
            }
            else
            {
                strSql.Append("FPBackType= null ,");
            }

            strSql.Append("FPBackTime='" + model.FPBackTime + "',");
            
            strSql.Append("Total=" + model.Total + ",");
            
            
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
            strSql.Append("delete from Sell_OrderFPBack ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.Sell_OrderFPBack GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" PId,Sell_OrderFPBack.Id,CreateUserId,CreateTime,FPBackTime,GuestNAME,FPBackType,ProNo,PONo,POName,Remark ,tb_User.loginName as CreateName,Status,FPNo,Total ");
            strSql.Append(" from Sell_OrderFPBack left join tb_User on tb_User.id=CreateUserId ");
            strSql.Append(" where Sell_OrderFPBack.Id=" + id + "");

            VAN_OA.Model.JXC.Sell_OrderFPBack model = null;
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
        public List<VAN_OA.Model.JXC.Sell_OrderFPBack> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append("PId, Sell_OrderFPBack.Id,CreateUserId,CreateTime,FPBackTime,GuestNAME,FPBackType,ProNo,PONo,POName,Remark ,tb_User.loginName as CreateName,Status,FPNo,Total ");
            strSql.Append(" from Sell_OrderFPBack left join tb_User on tb_User.id=CreateUserId "); 
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Sell_OrderFPBack.Id desc");
            List<VAN_OA.Model.JXC.Sell_OrderFPBack> list = new List<VAN_OA.Model.JXC.Sell_OrderFPBack>();

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


        public List<VAN_OA.Model.JXC.Sell_OrderFPView> GetListArrayAndFPInfo(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select PId,Sell_OrderFP.Id,Sell_OrderFP.CreateUserId,Sell_OrderFP.CreateTime,Sell_OrderFP.RuTime,");
            strSql.Append(" CG_POOrder.GuestNAME,Sell_OrderFP.DoPer,Sell_OrderFP.ProNo,CG_POOrder.PONo,CG_POOrder.POName");
            strSql.Append(" ,tb_User.loginName as CreateName,Sell_OrderFP.FPNo,Sell_OrderFP.FPNoStyle,Sell_OrderFP.Total");
            strSql.Append(" ,Sell_OrderFPBack.Id as BackId,FPBackTime,FPBackType,Sell_OrderFPBack.Status,Sell_OrderFPBack.ProNo as BackProNo");
            strSql.Append(" from CG_POOrder left join  ");
            strSql.Append(" Sell_OrderFP  on CG_POOrder.PoNo=Sell_OrderFP.PoNo and Sell_OrderFP.Status='通过' ");
            //strSql.Append(" left join Sell_OrderFPBack on Sell_OrderFP.FPNo=Sell_OrderFPBack.FPNo");
            strSql.Append(" left join Sell_OrderFPBack on Sell_OrderFP.ID=Sell_OrderFPBack.PID");
            strSql.Append(" left join tb_User on tb_User.id=Sell_OrderFP.CreateUserId ");


            strSql.Append(" where IFZhui=0 and CG_POOrder.Status='通过' " + strWhere);

            strSql.Append(" order by CG_POOrder.PONo desc,FPBackTime desc");
            List<VAN_OA.Model.JXC.Sell_OrderFPView> list = new List<VAN_OA.Model.JXC.Sell_OrderFPView>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBindBack(dataReader));
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.Sell_OrderFPView ReaderBindBack(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.Sell_OrderFPView model = new VAN_OA.Model.JXC.Sell_OrderFPView();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            else {
                model.FPBackType = 2;
            }
            ojb = dataReader["CreateUserId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateUserId = (int)ojb;
            }
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }
            ojb = dataReader["RuTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RuTime1 = (DateTime)ojb;
            }
            model.GuestName = dataReader["GuestNAME"].ToString();
          
            ojb = dataReader["DoPer"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.DoPer = ojb.ToString();
            }

            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }

           
            model.PONo = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
          
            ojb = dataReader["CreateName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateName = ojb.ToString();
            }

            ojb = dataReader["Status"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Status = ojb.ToString();
            }
            ojb = dataReader["FPNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPNo = ojb.ToString();
            }
            ojb = dataReader["FPNoStyle"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPNoStyle = ojb.ToString();
            }
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Total1 = Convert.ToDecimal(ojb);
            }

            ojb = dataReader["BackId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BackId = Convert.ToInt32(ojb);
            }

            ojb = dataReader["FPBackTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPBackTime = Convert.ToDateTime(ojb);
            }
           
            ojb = dataReader["FPBackType"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPBackType = Convert.ToInt32(ojb);
            }

            ojb = dataReader["BackProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.BackProNo = ojb.ToString();
            }
            ojb = dataReader["PID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PID =Convert.ToInt32(ojb);
            }

            
            return model;
        }


        public List<Sell_OrderFPBack> GetFPtoInvoiceView(string where)
        {
            string strSql = @"SELECT GuestNAME,[Id],[FPBackTime],[ProNo],[PONo],[POName],[FPNo],[FPNoStyle],[Total],[sumTotal],[chaTotals]   FROM [Fp_ToInvoice] where " + where;
            List<VAN_OA.Model.JXC.Sell_OrderFPBack> list = new List<VAN_OA.Model.JXC.Sell_OrderFPBack>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.Sell_OrderFPBack model = new VAN_OA.Model.JXC.Sell_OrderFPBack();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }

                        ojb = dataReader["FPBackTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FPBackTime =Convert.ToDateTime( ojb);
                        }
                        model.ProNo = dataReader["ProNo"].ToString();
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        ojb = dataReader["FPNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FPNo = ojb.ToString();
                        }

                        ojb = dataReader["sumTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.sumTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["chaTotals"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.chaTotals = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["FPNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FPNo = ojb.ToString();
                        }
                        model.GuestName = dataReader["GuestNAME"].ToString();

                        list.Add(model);
                    }
                }
            }
            return list;
        }


   




        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.Sell_OrderFPBack ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.Sell_OrderFPBack model = new VAN_OA.Model.JXC.Sell_OrderFPBack();
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
            ojb = dataReader["CreateTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateTime = (DateTime)ojb;
            }
            ojb = dataReader["FPBackTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPBackTime = (DateTime)ojb;
            }
            model.GuestName = dataReader["GuestNAME"].ToString();
            model.FPBackType =Convert.ToInt32( dataReader["FPBackType"]);
           
            
            model.ProNo = dataReader["ProNo"].ToString();
            model.PONo = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
            ojb = dataReader["Remark"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Remark = dataReader["Remark"].ToString();
                
            }
           
            ojb = dataReader["CreateName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CreateName = ojb.ToString();
            }

            ojb = dataReader["Status"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Status = ojb.ToString();
            }
            ojb = dataReader["FPNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPNo = ojb.ToString();
            }             
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Total =Convert.ToDecimal( ojb);
            }

            ojb = dataReader["PId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PId = Convert.ToInt32(ojb);
            }
            return model;
        }


        public void SellFPOrderBackUpdatePoStatus(string pono)
        {
            string sql = string.Format(@"select top 1 Sell_OrderFP.ID
 from Sell_OrderFP  left join Sell_OrderFPBack on  Sell_OrderFP.FPNo=Sell_OrderFPBack.FPNo  and Sell_OrderFPBack.Status='通过'  where 
Sell_OrderFP.Status='通过' and Sell_OrderFPBack.Id is null and Sell_OrderFP.PoNo='{0}';", pono);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.CommandText = sql;
                object obj = objCommand.ExecuteScalar();
                if ((obj is DBNull) || obj == null)
                {
                    sql = string.Format("update CG_POOrder set POStatue6='{0}' where PONo='{1}'", CG_POOrder.ConPOStatue6, pono);
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                }
                else
                {
                    sql = string.Format("update CG_POOrder set POStatue6='{0}' where PONo='{1}'", CG_POOrder.ConPOStatue6_1, pono);
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                }
            }

        }

    }
}
