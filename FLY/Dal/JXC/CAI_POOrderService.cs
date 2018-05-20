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
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.JXC;
namespace VAN_OA.Dal.JXC
{
    public class CAI_POOrderService
    {
        public bool UpdataCai(List<CAI_POCai> Cais)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;                
                CAI_POCaiService CaiSer = new CAI_POCaiService();
                try
                {
                    objCommand.Parameters.Clear();  

                    #region 采购
                    for (int i = 0; i < Cais.Count; i++)
                    {
                      
                        if (Cais[i].IfUpdate == true && Cais[i].Ids != 0)
                        {

                            CaiSer.Update(Cais[i], objCommand);

                        }
                        else if (Cais[i].Ids == 0)
                        {
                            CaiSer.Add(Cais[i], objCommand);

                        }
                    }                  
                    #endregion


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

        public bool updateTran(VAN_OA.Model.JXC.CAI_POOrder model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms,List<CAI_POOrders> orders,string IDS,
            List<CAI_POCai> Cais, string CAI_IDS)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;

                CAI_POOrdersService OrdersSer = new CAI_POOrdersService();
                CAI_POCaiService CaiSer = new CAI_POCaiService();
                try
                {

                    objCommand.Parameters.Clear();
                    model.Status = eform.state;
                    Update(model, objCommand);
                    tb_EFormService eformSer = new tb_EFormService();
                    eformSer.Update(eform, objCommand);
                    tb_EFormsService eformsSer = new tb_EFormsService();
                    eformsSer.Add(forms, objCommand);


                    //for (int i = 0; i < orders.Count; i++)
                    //{
                    //    orders[i].Id = model.Id;
                    //    //if (orders[i].IfUpdate == true && orders[i].Ids != 0)
                    //    //{

                    //        OrdersSer.Update(orders[i], objCommand);
                            
                    //    //}
                    //    //else if (orders[i].Ids == 0)
                    //    //{
                    //    //    OrdersSer.Add(orders[i], objCommand);
                          
                    //    //}
                    //}
                    //if (IDS != "")
                    //{
                    //    IDS  = IDS.Substring(0, IDS.Length - 1);
                    //    OrdersSer.DeleteByIds(IDS, objCommand);
                    //}



                    #region 采购
                    for (int i = 0; i < Cais.Count; i++)
                    {
                        Cais[i].Id = model.Id;
                        if (Cais[i].IfUpdate == true && Cais[i].Ids != 0)
                        {

                            CaiSer.Update(Cais[i], objCommand);

                        }
                        else if (Cais[i].Ids == 0)
                        {
                            CaiSer.Add(Cais[i], objCommand);

                        }
                    }
                    //if (CAI_IDS != "")
                    //{
                    //    CAI_IDS = CAI_IDS.Substring(0, CAI_IDS.Length - 1);
                    //    CaiSer.DeleteByIds(CAI_IDS, objCommand);
                    //} 
                    #endregion


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
        public int addTran(VAN_OA.Model.JXC.CAI_POOrder model, VAN_OA.Model.EFrom.tb_EForm eform, List<CAI_POOrders> orders, List<CAI_POCai> caiOrders,out int MainId,bool isCopy=false)
        {
            CG_POCaiService poCaiSer = new CG_POCaiService();
            var caiList=poCaiSer.GetCaiList(model.CG_ProNo);
            int id = 0;
            MainId = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                CAI_POOrdersService OrdersSer = new CAI_POOrdersService();

                CAI_POCaiService caiSer = new CAI_POCaiService();
                try
                {

                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("CAI_POOrder", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;

                    model.Status = eform.state;
                    id = Add(model, objCommand);
                    MainId = id;
                 
                    eform.allE_id = id;
                    eformSer.Add(eform, objCommand);
                    for (int i = 0; i < orders.Count; i++)
                    {
                        orders[i].Id = id;
                         
                        OrdersSer.Add(orders[i], objCommand);

                        if (isCopy == false)
                        {
                            CAI_POCai cai = new CAI_POCai();
                            cai.GuestName = orders[i].GuestName;
                            cai.Num = orders[i].Num;
                            cai.InvName = orders[i].InvName;
                            cai.Id = id;
                            cai.GoodId = orders[i].GoodId;

                            var pocaiModel = caiList.Find(p => p.GoodId == orders[i].GoodId);
                            if (pocaiModel != null)
                            {
                                cai.FinPrice1 = pocaiModel.FinPrice1;
                                cai.FinPrice2 = pocaiModel.FinPrice2;
                                cai.FinPrice3 = pocaiModel.FinPrice3;
                                cai.SupperPrice = pocaiModel.SupperPrice;
                                cai.SupperPrice1 = pocaiModel.SupperPrice1;
                                cai.SupperPrice2 = pocaiModel.SupperPrice2;
                                cai.Supplier = pocaiModel.Supplier;
                                cai.Supplier1 = pocaiModel.Supplier1;
                                cai.Supplier2 = pocaiModel.Supplier2;
                            }
                            caiSer.Add(cai, objCommand);
                        }
                        else
                        {
                            var pocaiModel = caiOrders.Find(p => p.GoodId == orders[i].GoodId);
                            pocaiModel.GuestName = orders[i].GuestName;
                            pocaiModel.Num = orders[i].Num;
                            pocaiModel.InvName = orders[i].InvName;
                            pocaiModel.Id = id;
                            caiSer.Add(pocaiModel, objCommand);
                        }
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
        public int Add(VAN_OA.Model.JXC.CAI_POOrder model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.AppName != null)
            {
                strSql1.Append("AppName,");
                strSql2.Append("" + model.AppName + ",");
            }

            if (model.CaiGou != null)
            {
                strSql1.Append("CaiGou,");
                strSql2.Append("'" + model.CaiGou + "',");
            }
            if (model.cRemark != null)
            {
                strSql1.Append("cRemark,");
                strSql2.Append("'" + model.cRemark + "',");
            }
            if (model.fileName != null)
            {
                strSql1.Append("fileName,");
                strSql2.Append("'" + model.fileName + "',");
            }

            if (model.fileNo != null)
            {
                strSql1.Append("fileNo,");
                strSql2.Append("@fileNo,");

                SqlParameter pp = new System.Data.SqlClient.SqlParameter("@fileNo", System.Data.SqlDbType.Image, model.fileNo.Length);
                pp.Value = model.fileNo;
                objCommand.Parameters.Add(pp);
            }
            if (model.fileType != null)
            {
                strSql1.Append("fileType,");
                strSql2.Append("'" + model.fileType + "',");
            }


            if (model.BusType == "1")
            {

                //项目编号


                string MaxCardNo = "";
                string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(PONo),5))+1))),5) FROM  CAI_POOrder where PONo like 'KC{0}%'", DateTime.Now.Year);

                objCommand.CommandText = sql.ToString();
                object objMax = objCommand.ExecuteScalar();
                if (objMax != null && objMax.ToString() != "")
                {
                    MaxCardNo = "KC" + DateTime.Now.Year.ToString() + objMax.ToString();
                }
                else
                {
                    MaxCardNo = "KC" + DateTime.Now.Year.ToString() + "00001";
                }

                strSql1.Append("PONo,");
                strSql2.Append("'" + MaxCardNo + "',");
            }
            else
            {
                strSql1.Append("PONo,");
                strSql2.Append("'" + model.PONo + "',");               
            }

            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }


            if (model.BusType != null)
            {
                strSql1.Append("BusType,");
                strSql2.Append("'" + model.BusType + "',");
            }

            if (model.POName != null)
            {
                strSql1.Append("POName,");
                strSql2.Append("'" + model.POName + "',");
            }
            if (model.PODate != null)
            {
                strSql1.Append("PODate,");
                strSql2.Append("'" + model.PODate + "',");
            }


            if (model.POPayStype != null)
            {
                strSql1.Append("POPayStype,");
                strSql2.Append("'" + model.POPayStype + "',");
            }
            if (model.POTotal != null)
            {
                strSql1.Append("POTotal,");
                strSql2.Append("" + model.POTotal + ",");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestName,");
                strSql2.Append("'" + model.GuestName + "',");
            }

            if (model.AE != null)
            {
                strSql1.Append("AE,");
                strSql2.Append("'" + model.AE + "',");
            }

            if (model.INSIDE != null)
            {
                strSql1.Append("INSIDE,");
                strSql2.Append("'" + model.INSIDE + "',");
            }

            if (model.GuestNo != null)
            {
                strSql1.Append("GuestNo,");
                strSql2.Append("'" + model.GuestNo + "',");
            }
            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("'" + model.Status + "',");
            }

            if (model.CG_ProNo != null)
            {
                strSql1.Append("CG_ProNo,");
                strSql2.Append("'" + model.CG_ProNo + "',");
            }


            
                strSql1.Append("TrueCaiDate,");
                strSql2.Append("getdate(),");

                strSql1.Append("IsMingYiCaiGou,");
                strSql2.AppendFormat("{0},", (model.IsMingYiCaiGou?1:0));
            

            strSql.Append("insert into CAI_POOrder(");
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
        public void Update(VAN_OA.Model.JXC.CAI_POOrder model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CAI_POOrder set ");
            //if (model.AppName != null)
            //{
            //    strSql.Append("AppName=" + model.AppName + ",");
            //}

            if (model.CaiGou != null)
            {
                strSql.Append("CaiGou='" + model.CaiGou + "',");
            }

            if (model.cRemark != null)
            {
                strSql.Append("cRemark='" + model.cRemark + "',");
            }
            else
            {
                strSql.Append("cRemark= null ,");
            }
            if (model.Status != null)
            {
                strSql.Append("Status='" + model.Status + "',");
            }
            if (model.Status != null && model.Status != "执行中")
            {
                strSql.Append("TrueCaiDate=getdate(),");
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
            strSql.Append("delete from CAI_POOrder ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.CAI_POOrder GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" IsMingYiCaiGou,CAI_POOrder.Id,CAI_POOrder.AppName,loginName,CAI_POOrder.CaiGou,CAI_POOrder.cRemark ,CAI_POOrder.fileName,CAI_POOrder.fileType ,CAI_POOrder.proNo,CAI_POOrder.PONo,BusType,Status,CG_ProNo,");
            strSql.Append(" GuestNo,GuestName,AE,INSIDE,POName,PODate,POTotal,POPayStype");
            strSql.Append(" from CAI_POOrder left join tb_User on tb_User.id=CAI_POOrder.AppName ");
            strSql.Append(" where CAI_POOrder.Id=" + id + "");

            VAN_OA.Model.JXC.CAI_POOrder model = null;
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
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.CAI_POOrder GetModel_File(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" fileName,fileType,fileNo ");
            strSql.Append(" from CAI_POOrder ");
            strSql.Append(" where CAI_POOrder.Id=" + id + "");

            VAN_OA.Model.JXC.CAI_POOrder model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = new CAI_POOrder();
                        object ojb;
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

                        ojb = dataReader["fileNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.fileNo = (byte[])ojb;
                        }                       
                    }
                }
            }
            return model;
        }




        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.CAI_POOrder> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();           
            strSql.Append("select   ");
            strSql.Append(" IsMingYiCaiGou,e_LastTime,tb2.IsHanShui,tb2.allCount,CAI_POOrder.Id,CAI_POOrder.AppName,loginName,CAI_POOrder.CaiGou,CAI_POOrder.cRemark ,CAI_POOrder.fileName,CAI_POOrder.fileType ,CAI_POOrder.proNo,CAI_POOrder.PONo,BusType,Status,CG_ProNo,");
            strSql.Append(" GuestNo,GuestName,AE,INSIDE,POName,PODate,POTotal,POPayStype");
            strSql.Append(" from CAI_POOrder left join tb_User on tb_User.id=CAI_POOrder.AppName ");
            strSql.Append(@" left join (select id,sum(CAI_POCai.IsHanShui) as IsHanShui ,COUNT(*) as allCount from CAI_POCai group by Id) as tb2 on tb2.id=CAI_POOrder.Id  ");
            strSql.Append(" left join tb_EForm on tb_EForm.allE_id=CAI_POOrder.id and proId=20 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CAI_POOrder.Id desc");

            List<VAN_OA.Model.JXC.CAI_POOrder> list = new List<VAN_OA.Model.JXC.CAI_POOrder>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        var ojb = dataReader["IsHanShui"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Count1 = (int)ojb;
                        }
                        ojb = dataReader["allCount"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Count2 = (int)ojb;
                        }
                        ojb = dataReader["e_LastTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LastTime = (DateTime)ojb;
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
        public VAN_OA.Model.JXC.CAI_POOrder ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.CAI_POOrder model = new VAN_OA.Model.JXC.CAI_POOrder();
            object ojb;


            model.IsMingYiCaiGou = (bool)dataReader["IsMingYiCaiGou"];
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

            ojb = dataReader["loginName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.LoginName = ojb.ToString();
            }

            ojb = dataReader["CaiGou"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CaiGou = ojb.ToString();
            }
            try
            {
                model.cRemark = dataReader["cRemark"].ToString();
            }
            catch (Exception)
            {
                
                 
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

            ojb = dataReader["ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ProNo = ojb.ToString();
            }


            model.PONo = dataReader["PONo"].ToString();
            model.BusType = dataReader["BusType"].ToString();

            ojb = dataReader["GuestNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestNo = ojb.ToString();
            }

            ojb = dataReader["GuestName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestName = ojb.ToString();
            }

            ojb = dataReader["AE"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AE = ojb.ToString();
            }

            ojb = dataReader["INSIDE"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.INSIDE = ojb.ToString();
            }

            ojb = dataReader["POName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POName = ojb.ToString();
            }
            ojb = dataReader["PODate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PODate =Convert.ToDateTime(ojb);
            }

            ojb = dataReader["POTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POTotal = Convert.ToDecimal(ojb);
            }

            ojb = dataReader["POPayStype"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POPayStype =ojb.ToString();
            }

            ojb = dataReader["Status"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Status = ojb.ToString();
            }

            ojb = dataReader["CG_ProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CG_ProNo = ojb.ToString();
            }

            
            return model;
        }



        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.ReportForms.vAllCaiOrderList> GetListArrayAll(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select e_LastTime,CaiFpType,vAllCaiOrderList.Id,AppName,CaiGou,cRemark,ProNo,PONo,BusType,POName,PODate,POPayStype,POTotal,GuestName,AE,INSIDE,GuestNo,Status,CG_ProNo,Time,InvName,Num,Unit,CostPrice,SellPrice,OtherCost,ToTime,Profit,GoodId,CG_POOrdersId,CaiTime,Supplier,SupperPrice,UpdateUser,Idea,Supplier1,SupperPrice1,Supplier2,SupperPrice2,FinPrice1,FinPrice2,FinPrice3,cbifDefault1,cbifDefault2,cbifDefault3,lastSupplier,lastPrice,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,IsHanShui,ids");
            strSql.Append(" FROM vAllCaiOrderList ");
            strSql.Append(" left join tb_EForm on tb_EForm.allE_id=vAllCaiOrderList.id and proId=20 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by id desc");
            List<VAN_OA.Model.ReportForms.vAllCaiOrderList> list = new List<VAN_OA.Model.ReportForms.vAllCaiOrderList>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBindAll(dataReader);
                        object ojb;
                        ojb = dataReader["IsHanShui"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsHanShui = Convert.ToInt32(ojb) == 0 ? false : true ;
                        }
                        ojb = dataReader["ids"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ids = Convert.ToInt32(ojb);
                        }
                        ojb = dataReader["CaiFpType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiFpType = Convert.ToString(ojb);
                        }
                        ojb = dataReader["e_LastTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LastTime = (DateTime)ojb;
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        public List<VAN_OA.Model.ReportForms.vAllCaiOrderList> GetListArrayAll_Page(string strWhere, PagerDomain page,out decimal Total)
        {
            Total = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  sum(Num*lastPrice) as total ");
            strSql.Append(" FROM vAllCaiOrderList left join tb_EForm on tb_EForm.allE_id=vAllCaiOrderList.id and proId=20 ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            var total= DBHelp.ExeScalar(strSql.ToString());
            if (total != null && total != DBNull.Value)
            {
                Total = (decimal)total;
            }
            strSql = new StringBuilder();
            strSql.Append("select LastTruePrice,TruePrice1,TruePrice2,TruePrice3,CaiFpType,vAllCaiOrderList.Id,AppName,CaiGou,cRemark,ProNo,PONo,BusType,POName,PODate,POPayStype,POTotal,GuestName,AE,INSIDE,GuestNo,Status,CG_ProNo,Time,InvName,Num,Unit,CostPrice,SellPrice,OtherCost,ToTime,Profit,GoodId,CG_POOrdersId,CaiTime,Supplier,SupperPrice,UpdateUser,Idea,Supplier1,SupperPrice1,Supplier2,SupperPrice2,FinPrice1,FinPrice2,FinPrice3,cbifDefault1,cbifDefault2,cbifDefault3,lastSupplier,lastPrice,GoodNo,GoodName,GoodSpec,GoodModel,GoodUnit,GoodTypeSmName,IsHanShui,ids");
            strSql.Append(" FROM vAllCaiOrderList ");
            //strSql.Append(" left join tb_EForm on tb_EForm.allE_id=vAllCaiOrderList.id and proId=20 ");
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            //strSql.Append(" order by id desc");

            strSql = new StringBuilder(DBHelp.GetPagerSql(page, strSql.ToString(), strWhere, " vAllCaiOrderList.ids desc "));

            //SQL 优化 
            strSql = strSql.Replace("SELECT *", "SELECT *,e_LastTime ").Replace(" ) TT",") TT left join tb_EForm on tb_EForm.allE_id=TT.id and proId=20 ");
            strSql.Append(" order by TT.Row ");

            List<VAN_OA.Model.ReportForms.vAllCaiOrderList> list = new List<VAN_OA.Model.ReportForms.vAllCaiOrderList>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBindAll(dataReader);
                        object ojb;
                        ojb = dataReader["IsHanShui"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsHanShui = Convert.ToInt32(ojb) == 0 ? false : true;
                        }
                        ojb = dataReader["ids"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ids = Convert.ToInt32(ojb);
                        }
                        ojb = dataReader["CaiFpType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiFpType = Convert.ToString(ojb);
                        }
                        ojb = dataReader["e_LastTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LastTime = (DateTime)ojb;
                        }
                        ojb = dataReader["TruePrice1"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.TruePrice1 = (decimal)ojb;
                        }
                        ojb = dataReader["TruePrice2"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.TruePrice2 = (decimal)ojb;
                        }
                        ojb = dataReader["TruePrice3"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.TruePrice3 = (decimal)ojb;
                        }

                        ojb = dataReader["LastTruePrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LastTruePrice = (decimal)ojb;
                        }

                        model.LastTotal = model.lastPrice * model.Num;
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.ReportForms.vAllCaiOrderList ReaderBindAll(IDataReader dataReader)
        {
            VAN_OA.Model.ReportForms.vAllCaiOrderList model = new VAN_OA.Model.ReportForms.vAllCaiOrderList();
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
            model.CaiGou = dataReader["CaiGou"].ToString();
            model.cRemark = dataReader["cRemark"].ToString();
            model.ProNo = dataReader["ProNo"].ToString();
            model.PONo = dataReader["PONo"].ToString();
            model.BusType = dataReader["BusType"].ToString();
            model.POName = dataReader["POName"].ToString();
            ojb = dataReader["PODate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PODate = (DateTime)ojb;
            }
            model.POPayStype = dataReader["POPayStype"].ToString();
            ojb = dataReader["POTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POTotal = (decimal)ojb;
            }
            model.GuestName = dataReader["GuestName"].ToString();
            model.AE = dataReader["AE"].ToString();
            model.INSIDE = dataReader["INSIDE"].ToString();
            model.GuestNo = dataReader["GuestNo"].ToString();
            model.Status = dataReader["Status"].ToString();
            model.CG_ProNo = dataReader["CG_ProNo"].ToString();
            ojb = dataReader["Time"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Time = (DateTime)ojb;
            }
            model.InvName = dataReader["InvName"].ToString();
            ojb = dataReader["Num"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Num = (decimal)ojb;
            }
            model.Unit = dataReader["Unit"].ToString();
            ojb = dataReader["CostPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CostPrice = (decimal)ojb;
            }
            ojb = dataReader["SellPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellPrice = (decimal)ojb;
            }
            ojb = dataReader["OtherCost"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OtherCost = (decimal)ojb;
            }
            ojb = dataReader["ToTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ToTime = (DateTime)ojb;
            }
            ojb = dataReader["Profit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Profit = (decimal)ojb;
            }
            ojb = dataReader["GoodId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodId = (int)ojb;
            }
            ojb = dataReader["CG_POOrdersId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CG_POOrdersId = (int)ojb;
            }
            ojb = dataReader["CaiTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CaiTime = (DateTime)ojb;
            }
            model.Supplier = dataReader["Supplier"].ToString();
            ojb = dataReader["SupperPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupperPrice = (decimal)ojb;
            }
            model.UpdateUser = dataReader["UpdateUser"].ToString();
            model.Idea = dataReader["Idea"].ToString();
            model.Supplier1 = dataReader["Supplier1"].ToString();
            ojb = dataReader["SupperPrice1"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupperPrice1 = (decimal)ojb;
            }
            model.Supplier2 = dataReader["Supplier2"].ToString();
            ojb = dataReader["SupperPrice2"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupperPrice2 = (decimal)ojb;
            }
            ojb = dataReader["FinPrice1"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FinPrice1 = (decimal)ojb;
            }
            ojb = dataReader["FinPrice2"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FinPrice2 = (decimal)ojb;
            }
            ojb = dataReader["FinPrice3"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FinPrice3 = (decimal)ojb;
            }
            ojb = dataReader["cbifDefault1"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.cbifDefault1 = (bool)ojb;
            }
            ojb = dataReader["cbifDefault2"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.cbifDefault2 = (bool)ojb;
            }
            ojb = dataReader["cbifDefault3"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.cbifDefault3 = (bool)ojb;
            }
            model.lastSupplier = dataReader["lastSupplier"].ToString();
            ojb = dataReader["lastPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.lastPrice = (decimal)ojb;
            }
            ojb = dataReader["GoodNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNo = ojb.ToString();
            }
            ojb = dataReader["GoodName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodName = ojb.ToString();
            }
            ojb = dataReader["GoodSpec"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodSpec = ojb.ToString();
            }

            ojb = dataReader["GoodModel"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Good_Model = ojb.ToString();
            }
            ojb = dataReader["GoodUnit"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodUnit = ojb.ToString();
            }

            ojb = dataReader["GoodTypeSmName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodTypeSmName = ojb.ToString();
            }
            model.CostTotal = model.CostPrice * model.Num;
            model.SellTotal = model.SellPrice * model.Num;
            model.YiLiTotal = model.SellTotal - model.CostTotal - model.OtherCost;
            if (model.SellTotal != 0)
            {
                model.Profit = model.YiLiTotal / model.SellTotal * 100;
            }
            else if (model.YiLiTotal != 0)
            {
                model.Profit = -100;
            }
            else
            {
                model.Profit = 0;
            }

            //
            model.Total1 = model.SupperPrice * model.Num;
            model.Total2 = model.SupperPrice1 * model.Num;
            model.Total3 = model.SupperPrice2 * model.Num;
            if (model.FinPrice1!=0)
            model.Total1 = model.FinPrice1 * model.Num;
            if (model.FinPrice2!=0)
            model.Total2 = model.FinPrice2 * model.Num;
            if (model.FinPrice3!=0)
            model.Total3 = model.FinPrice3 * model.Num;
            return model;
        }


        /// <summary>
        /// 获取采购中项目商品的总数量
        /// </summary>
        /// <param name="poNo"></param>
        /// <param name="goodId"></param>
        /// <returns></returns>
        public decimal GetGoodTotalNum(string poNo, int goodId)
        {
            string sql = string.Format(@"select sum(Num) as totalNum from CAI_POOrder left join CAI_POCai on CAI_POCai.id=CAI_POOrder.id where Status='通过' and goodId={1} and  PoNo='{0}' ",
                poNo, goodId);
            object obj = DBHelp.ExeScalar(sql);
            if (obj is DBNull)
            {
                return 0;
            }
            return Convert.ToDecimal(obj);

        }

        /// <summary>
        /// 获取采购中项目商品的总数量
        /// </summary>
        /// <param name="poNo"></param>
        /// <param name="goodId"></param>
        /// <returns></returns>
        public decimal GetGoodTotalNum_New(string poNo, int goodId)
        {
            string sql = string.Format(@"select sum(caiNums) as caiNums from (
select GooId as GoodId,PoNo,sum(GoodNum) as caiNums from CAI_OrderInHouse 
left join CAI_OrderInHouses on CAI_OrderInHouse.id=CAI_OrderInHouses.id
where Status='通过' and Supplier<>'库存' and pono='{0}' and  GooId ={1} group by GooId,PoNo
union all
select GoodId,PoNo,sum(Num) as caiNums from CAI_POOrder 
left join CAI_POCai on CAI_POCai.id=CAI_POOrder.id
where Status='通过' and lastSupplier='库存' and pono='{0}' and  GoodId ={1} group by GoodId,PoNo) as A group by  GoodId,PoNo",
                poNo, goodId);
            object obj = DBHelp.ExeScalar(sql);
            if (obj is DBNull)
            {
                return 0;
            }
            return Convert.ToDecimal(obj);

        }

        /// <summary>
        /// 采购单的数量逻辑下面已经描述了。
        ///需采购的数量=原项目订单该商品的数量+所有该追加项目订单的该商品数量-（项目所有该商品的销售退货数量）-（项目所有该商品的采购退货数量）-该商品已经采购入库的数量
        ///但是 如果销售退货后 又做了采购退货，两部分的退货数量能够计一次（以销售退货的数量为准）。 
        /// </summary>
        /// <param name="goodId"></param>
        /// <param name="PoNo"></param>
        /// <returns></returns>
        public decimal GetActGoosTotalNum(int goodId,string PoNo ,int notCaiInId)
        {
            string sql = "";

//            if (notCaiInId != 0)
//            {
//                sql = string.Format(@"declare @AllTotalNum decimal(18,2);
//declare @TuiTotalNum decimal(18,2);
//declare @SellTuiTotalNum decimal(18,2);
//set @ALLTotalNum=0;
//set @TuiTotalNum=0;
//select @AllTotalNum=sum(Num) from CG_POOrder left join CG_POCai on CG_POOrder.id=CG_POCai.id where Status='通过' and goodId={1} and  PoNo='{0}';
//select @AllTotalNum=@AllTotalNum-isnull(sum(GoodNum),0) from CAI_OrderInHouse left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id where Status<>'不通过' and GooId={1} and  PoNo='{0}' and CAI_OrderInHouse.id<>{2};
//select @TuiTotalNum=isnull(sum(GoodNum),0) from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.ID  where Status<>'不通过' and GooId={1} and  PoNo='{0}';
//select @SellTuiTotalNum=isnull(sum(GoodNum),0) from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.ID  where Status<>'不通过' and GooId={1} and  PoNo='{0}';
//if(@SellTuiTotalNum<>0 and @SellTuiTotalNum>@TuiTotalNum)
//begin 
//set @TuiTotalNum=@SellTuiTotalNum
//end
//set @ALLTotalNum=@ALLTotalNum-@TuiTotalNum
//select @ALLTotalNum", PoNo, goodId, notCaiInId);
//            }
//            else
//            {
//                sql = string.Format(@"declare @AllTotalNum decimal(18,2);
//declare @TuiTotalNum decimal(18,2);
//declare @SellTuiTotalNum decimal(18,2);
//set @ALLTotalNum=0;
//set @TuiTotalNum=0;
//select @AllTotalNum=sum(Num) from CG_POOrder left join CG_POCai on CG_POOrder.id=CG_POCai.id where Status='通过' and goodId={1} and  PoNo='{0}';
//select @AllTotalNum=@AllTotalNum-isnull(sum(GoodNum),0) from CAI_OrderInHouse left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id where Status<>'不通过' and GooId={1} and  PoNo='{0}';
//select @TuiTotalNum=isnull(sum(GoodNum),0) from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.ID  where Status<>'不通过' and GooId={1} and  PoNo='{0}';
//select @SellTuiTotalNum=isnull(sum(GoodNum),0) from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.ID  where Status<>'不通过' and GooId={1} and  PoNo='{0}';
//if(@SellTuiTotalNum<>0 and @SellTuiTotalNum>@TuiTotalNum)
//begin 
//set @TuiTotalNum=@SellTuiTotalNum
//end
//set @ALLTotalNum=@ALLTotalNum-@TuiTotalNum
//select @ALLTotalNum", PoNo, goodId);
//            }

            if (notCaiInId != 0)
            {
                sql = string.Format(@"declare @AllTotalNum decimal(18,2);
declare @TuiTotalNum decimal(18,2);
declare @SellTuiTotalNum decimal(18,2);
set @ALLTotalNum=0;
set @TuiTotalNum=0;
select @AllTotalNum=sum(Num) from CG_POOrder left join CG_POCai on CG_POOrder.id=CG_POCai.id where Status='通过' and goodId={1} and  PoNo='{0}';
select @AllTotalNum=@AllTotalNum-isnull(sum(GoodNum),0) from CAI_OrderInHouse left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id where Status<>'不通过' and GooId={1} and  PoNo='{0}' and CAI_OrderInHouse.id<>{2};
select @ALLTotalNum", PoNo, goodId, notCaiInId);
            }
            else
            {
                sql = string.Format(@"declare @AllTotalNum decimal(18,2);
declare @TuiTotalNum decimal(18,2);
declare @SellTuiTotalNum decimal(18,2);
set @ALLTotalNum=0;
set @TuiTotalNum=0;
select @AllTotalNum=sum(Num) from CG_POOrder left join CG_POCai on CG_POOrder.id=CG_POCai.id where Status='通过' and goodId={1} and  PoNo='{0}';
select @AllTotalNum=@AllTotalNum-isnull(sum(GoodNum),0) from CAI_OrderInHouse left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id where Status<>'不通过' and GooId={1} and  PoNo='{0}';
select @ALLTotalNum", PoNo, goodId);
            }
            object obj = DBHelp.ExeScalar(sql);
            if (obj is DBNull)
            {
                return 0;
            }
            return Convert.ToDecimal(obj); 
        }
    }
}
