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
    public class Sell_OrderFPService
    {
        public bool updateTran_BakDown(string NowGuid, int id)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;

                Sell_OrderFP model = new Sell_OrderFP();
                List<Sell_OrderFPs> orders = new List<Sell_OrderFPs>();


                BackUpFPInfoService backUpSer = new BackUpFPInfoService();
                backUpSer.BackDown(NowGuid, id, objCommand, out model, out orders);


                if (model.Id > 0)
                {
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

                        Update(model, objCommand);


                        Sell_OrderFPsService OrdersSer = new Sell_OrderFPsService();

                        //删除之前的数据
                        objCommand.CommandText = string.Format("delete from Sell_OrderFPs where id=" + model.Id);
                        objCommand.ExecuteNonQuery();

                        for (int i = 0; i < orders.Count; i++)
                        {
                            orders[i].id = model.Id;
                            OrdersSer.Add(orders[i], objCommand);

                            if (model.Status == "通过")
                            {
                                if (!hs.Contains(orders[i].SellOutPONO))
                                {
                                    hs.Add(orders[i].SellOutPONO, null);
                                }
                            }
                        }



                        foreach (var key in hs.Keys)
                        {
                            //更改销售订单的发票号
                            string sql = string.Format("update Sell_OrderOutHouse set FPNo=FPNo+'{0}/' where ProNo='{1}'", model.FPNo, key);
                            objCommand.CommandText = sql;
                            objCommand.ExecuteNonQuery();


                            //更改项目订单的发票号
                            sql = string.Format("update CG_POOrder set FPTotal=isnull(FPTotal,'')+'{0}/' where PONo='{1}' and ifzhui=0 ", model.FPNo, model.PONo);
                            objCommand.CommandText = sql;
                            objCommand.ExecuteNonQuery();
                        }

                        tan.Commit();

                        if (model.Status == "通过")
                        {
                            new Sell_OrderFPBackService().SellFPOrderBackUpdatePoStatus(model.PONo);
                            new CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus(model.PONo);

                            new CG_POOrdersService().GetListArrayToFpsAndUpdatePoStatue(model.PONo, model.Status);
                            new Sell_OrderFPBackService().SellFPOrderBackUpdatePoStatus(model.PONo);
                        }

                    }
                    catch (Exception)
                    {
                        tan.Rollback();
                        return false;

                    }

                }
                //else
                //{
                //    tan.Rollback();
                //}
            }
            return true;
        }


        public bool updateTran(VAN_OA.Model.JXC.Sell_OrderFP model, VAN_OA.Model.EFrom.tb_EForm eform,
            tb_EForms forms, List<Sell_OrderFPs> orders, string IDS, bool isBackUp, bool isBackUpInvoice)
        {
            //判断是否是删除 -销售发票删除
            if (eform.proId == 37)
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
                        if (eform.state == "不通过")
                        {
                            model.Status = "通过";
                        }
                        UpdateToDelete(model, objCommand);
                        tb_EFormService eformSer = new tb_EFormService();
                        eformSer.Update(eform, objCommand, isBackUp);
                        tb_EFormsService eformsSer = new tb_EFormsService();
                        eformsSer.Add(forms, objCommand);

                        if (eform.state == "通过")
                        {
                            //删除发票签回单（如果有）
                            string deleteFPBack = string.Format("delete tb_EForms where e_Id in (select id from tb_EForm where proId=29 and allE_id in (select id from Sell_OrderFPBack where PId={0}));", model.Id);
                            deleteFPBack += string.Format("delete tb_EForm where proId=29 and allE_id in (select id from Sell_OrderFPBack where PId={0});", model.Id);
                            deleteFPBack += string.Format("delete Sell_OrderFPBacks  where Id in (select id from Sell_OrderFPBack where PId={0});delete Sell_OrderFPBack  where PId={0};", model.Id);

                            objCommand.CommandText = deleteFPBack;
                            objCommand.ExecuteNonQuery();

                            //删除发票删除单
                            string deleteFPDelete = string.Format("delete tb_EForms where e_Id in (select id from tb_EForm where proId in (26,34,37) and allE_id={0});", model.Id);
                            deleteFPDelete += string.Format("delete tb_EForm where proId in (26,34,37) and allE_id={0};", model.Id);
                            objCommand.CommandText = deleteFPDelete;
                            objCommand.ExecuteNonQuery();

                            //删除发票单
                            string DeleteAll = string.Format(@"declare @oldFPNo  varchar(500);declare @oldPONo  varchar(500);
select top 1  @oldFPNo=FPNo,@oldPONo=PONo from Sell_OrderFP where id={0}
update  CG_POOrder set FPTotal=replace( FPTotal, @oldFPNo+'/','')
where PONo  in (select PONo from Sell_OrderFP where id={0}) and ifzhui=0;", model.Id);

                            Dal.EFrom.tb_EFormService efromSer = new VAN_OA.Dal.EFrom.tb_EFormService();
                            //var efromModel = efromSer.GetModel(Convert.ToInt32(model.Id));
                            //if (efromModel.state == "通过")
                            //{
                                DeleteAll += "update CG_POOrder set POStatue3='' where PONo=@oldPONo;";
                            //}

                            DeleteAll += string.Format("delete from Sell_OrderFP where id={0};delete from Sell_OrderFPs where id={0}; ", model.Id);

                            objCommand.CommandText = DeleteAll;
                            objCommand.ExecuteNonQuery();

                           
                        }
                        tan.Commit();
                   
                    }
                    catch (Exception)
                    {
                        tan.Rollback();
                        return false;
                    }
                }
                if (eform.state == "通过")
                {
                    new Sell_OrderFPBackService().SellFPOrderBackUpdatePoStatus(model.PONo);
                    new VAN_OA.Dal.JXC.CG_POOrdersService().GetListArrayToFpsAndUpdatePoStatue(model.PONo, "通过");
                }
            }
            else
            {
                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlTransaction tan = conn.BeginTransaction();
                    SqlCommand objCommand = conn.CreateCommand();
                    objCommand.Transaction = tan;

                    string backUpPoNos = "";
                    if (isBackUp)
                    {
                        BackUpFPInfoService backUpSer = new BackUpFPInfoService();
                        backUpPoNos = backUpSer.BackUp(model.Id, objCommand);
                    }
                    //最后进行 删除 到款单 ，以及发票签回单& 备份 
                    if (isBackUpInvoice && eform.state == "通过")
                    {
                        BackUpFPInfoService backUpSer = new BackUpFPInfoService();
                        backUpPoNos = backUpSer.BackUpOthers(model.Id, objCommand, model.InvoiceNowGuid);
                    }
                    //CG_POOrdersService OrdersSer = new CG_POOrdersService();
                    //CG_POCaiService CaiSer = new CG_POCaiService();
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
                        eformSer.Update(eform, objCommand, isBackUp);
                        tb_EFormsService eformsSer = new tb_EFormsService();
                        eformsSer.Add(forms, objCommand);
                        TB_HouseGoodsService houseGoodsSer = new TB_HouseGoodsService();
                        Sell_OrderFPsService OrdersSer = new Sell_OrderFPsService();
                        if (isBackUp)
                        {
                            //删除之前的数据
                            objCommand.CommandText = string.Format("delete from Sell_OrderFPs where id=" + model.Id);
                            objCommand.ExecuteNonQuery();

                            for (int i = 0; i < orders.Count; i++)
                            {
                                orders[i].id = model.Id;
                                OrdersSer.Add(orders[i], objCommand);

                                if (eform.state == "通过")
                                {
                                    if (!hs.Contains(orders[i].SellOutOrderId))
                                    {
                                        hs.Add(orders[i].SellOutOrderId, null);
                                    }
                                }
                            }
                        }


                        //foreach (var key in hs.Keys)
                        //{
                        //    //更改销售订单的发票号
                        //    string sql = string.Format("update Sell_OrderOutHouse set FPNo=FPNo+'{0}/' where ProNo='{1}'", model.FPNo, key);
                        //    objCommand.CommandText = sql;
                        //    objCommand.ExecuteNonQuery();
                        if (eform.state == "通过")
                        {
                            //更改项目订单的发票号
                            string sql = string.Format("update CG_POOrder set FPTotal=isnull(FPTotal,'')+'{0}/' where PONo='{1}' and ifzhui=0 ", model.FPNo, model.PONo);
                            objCommand.CommandText = sql;
                            objCommand.ExecuteNonQuery();
                        }
                        //}

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


                        tan.Commit();


                        if (backUpPoNos != "")
                        {
                            foreach (string pono in backUpPoNos.Split(','))
                            {
                                if (!string.IsNullOrEmpty(pono))
                                {
                                    new Sell_OrderFPBackService().SellFPOrderBackUpdatePoStatus(pono);
                                    new CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus(pono);
                                }
                            }
                        }

                    }
                    catch (Exception)
                    {
                        tan.Rollback();
                        return false;

                    }

                }
            }
            return true;
        }
        public int addTran(VAN_OA.Model.JXC.Sell_OrderFP model, VAN_OA.Model.EFrom.tb_EForm eform, List<Sell_OrderFPs> orders, out int MainId)
        {
            int id = 0;
            MainId = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                Sell_OrderFPsService OrdersSer = new Sell_OrderFPsService();


                try
                {


                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("Sell_OrderFP", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;

                    decimal total = 0;
                    foreach (var m in orders)
                    {
                        total += m.GoodSellPriceTotal;
                    }
                    model.Total = total;
                    model.Status = eform.state;

                    //
                    TB_ToInvoiceService invoiceSer = new TB_ToInvoiceService();
                    model.ZhuanPayTotal = invoiceSer.GetPayTotal(model.PONo, model.Total);


                    id = Add(model, objCommand);
                    MainId = id;

                    eform.allE_id = id;
                    eformSer.Add(eform, objCommand);

                    System.Collections.Hashtable hs = new System.Collections.Hashtable();
                    for (int i = 0; i < orders.Count; i++)
                    {
                        orders[i].id = id;
                        OrdersSer.Add(orders[i], objCommand);
                        if (eform.state == "通过")
                        {
                            if (!hs.Contains(orders[i].SellOutPONO))
                            {
                                hs.Add(orders[i].SellOutPONO, null);
                            }

                        }

                    }


                    foreach (var key in hs.Keys)
                    {
                        //更改销售订单的发票号
                        //string sql = string.Format("update Sell_OrderOutHouse set FPNo=FPNo+'{0}/' where ProNo='{1}'", model.FPNo, key);
                        //objCommand.CommandText = sql;
                        //objCommand.ExecuteNonQuery();


                        //更改项目订单的发票号
                        string sql = string.Format("update CG_POOrder set FPTotal=isnull(FPTotal,'')+'{0}/' where PONo='{1}' and ifzhui=0 ", model.FPNo, model.PONo);
                        objCommand.CommandText = sql;
                        objCommand.ExecuteNonQuery();
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
        public int Add(VAN_OA.Model.JXC.Sell_OrderFP model, SqlCommand objCommand)
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

            if (model.RuTime != null)
            {
                strSql1.Append("RuTime,");
                strSql2.Append("'" + model.RuTime + "',");
            }
            if (model.GuestName != null)
            {
                strSql1.Append("GuestNAME,");
                strSql2.Append("'" + model.GuestName + "',");
            }
            if (model.DoPer != null)
            {
                strSql1.Append("DoPer,");
                strSql2.Append("'" + model.DoPer + "',");
            }

            if (model.FPNoStyle != null)
            {
                strSql1.Append("FPNoStyle,");
                strSql2.Append("'" + model.FPNoStyle + "',");
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

            strSql1.Append("ZhuanPayTotal,");
            strSql2.Append("" + model.ZhuanPayTotal + ",");

            strSql1.Append("ZhengFu,");
            strSql2.Append("" + model.ZhengFu + ",");

            strSql.Append("insert into Sell_OrderFP(");
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
        public void Update(VAN_OA.Model.JXC.Sell_OrderFP model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sell_OrderFP set ");


            if (model.RuTime != null)
            {
                strSql.Append("RuTime='" + model.RuTime + "',");
            }
            if (model.GuestName != null)
            {
                strSql.Append("GuestNAME='" + model.GuestName + "',");
            }

            if (model.PONo != null)
            {
                strSql.Append("PONo='" + model.PONo + "',");
            }
            if (model.POName != null)
            {
                strSql.Append("POName='" + model.POName + "',");
            }

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

            if (model.DoPer != null)
            {
                strSql.Append("DoPer='" + model.DoPer + "',");
            }
            else
            {
                strSql.Append("DoPer= null ,");
            }

            if (model.FPNoStyle != null)
            {
                strSql.Append("FPNoStyle='" + model.FPNoStyle + "',");
            }
            else
            {
                strSql.Append("FPNoStyle= null ,");
            }
            strSql.Append("Total=" + model.Total + ",");

            if (model.TopFPNo != null && model.TopFPNo != "")
            {
                strSql.Append("TopFPNo='" + model.TopFPNo + "',");
            }
            if (model.TopTotal != null && model.TopTotal != 0)
            {
                strSql.Append("TopTotal=" + model.TopTotal + ",");
            }



            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            int i = objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateToDelete(VAN_OA.Model.JXC.Sell_OrderFP model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Sell_OrderFP set ");
            if (model.Status != null)
            {
                strSql.Append("Status='" + model.Status + "',");
            }
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Id=" + model.Id + "");
            objCommand.CommandText = strSql.ToString();
            int i = objCommand.ExecuteNonQuery();
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Sell_OrderFP ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.Sell_OrderFP GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" ZhengFu,ZhuanPayTotal,NowGuid,InvoiceNowGuid,Sell_OrderFP.Id,CreateUserId,CreateTime,RuTime,GuestNAME,DoPer,ProNo,PONo,POName,Remark ,tb_User.loginName as CreateName,Status,FPNo,FPNoStyle,Total,TopFPNo,TopTotal ");
            strSql.Append(" from Sell_OrderFP left join tb_User on tb_User.id=CreateUserId ");
            strSql.Append(" where Sell_OrderFP.Id=" + id + "");

            VAN_OA.Model.JXC.Sell_OrderFP model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind(dataReader);
                        model.NowGuid = dataReader["NowGuid"].ToString();
                        model.InvoiceNowGuid = dataReader["InvoiceNowGuid"].ToString();
                        model.ZhuanPayTotal = Convert.ToDecimal(dataReader["ZhuanPayTotal"]);
                        model.ZhengFu = Convert.ToInt32(dataReader["ZhengFu"]);
                    }
                }
            }
            return model;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.Sell_OrderFP> GetListArray_Back(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Sell_OrderFP.Id,Sell_OrderFP.CreateTime,Sell_OrderFP.RuTime,Sell_OrderFP.GuestNAME,Sell_OrderFP.ProNo,Sell_OrderFP.PONo,Sell_OrderFP.POName,Sell_OrderFP.FPNo,Sell_OrderFP.FPNoStyle,Sell_OrderFP.Total ");
            // strSql.Append(" from Sell_OrderFP  left join Sell_OrderFPBack on  Sell_OrderFP.FPNo=Sell_OrderFPBack.FPNo  and Sell_OrderFPBack.Status<>'不通过'  ");
            strSql.Append(" from Sell_OrderFP  left join Sell_OrderFPBack on  Sell_OrderFP.ID=Sell_OrderFPBack.PID  and Sell_OrderFPBack.Status<>'不通过'  ");

            strSql.Append(" where Sell_OrderFP.Status='通过' and Sell_OrderFPBack.Id is null " + strWhere);

            strSql.Append(" order by Sell_OrderFP.Id desc");
            List<VAN_OA.Model.JXC.Sell_OrderFP> list = new List<VAN_OA.Model.JXC.Sell_OrderFP>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.Sell_OrderFP model = new VAN_OA.Model.JXC.Sell_OrderFP();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }

                        ojb = dataReader["CreateTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreateTime = (DateTime)ojb;
                        }
                        ojb = dataReader["RuTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.RuTime = (DateTime)ojb;
                        }
                        model.GuestName = dataReader["GuestNAME"].ToString();
                        model.ProNo = dataReader["ProNo"].ToString();
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
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
                            model.Total = Convert.ToDecimal(ojb);
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 对账使用
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Sell_OrderFP> GetOAInvoiceList(string where)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select Sell_OrderFP.PONAME,CG_POOrder.PODate,Isorder,Sell_OrderFP.Id,Sell_OrderFP.GuestNAME,FPNo,Total,CreateTime, Sell_OrderFP.PONo,loginName from Sell_OrderFP left join tb_User on tb_User.id=CreateUserId
left join CG_POOrder on CG_POOrder.PONo=Sell_OrderFP.PONo and IFZhui=0    where Sell_OrderFP.Status='通过'");
            if (where != "")
            {
                strSql.Append(where);
            }
            List<Sell_OrderFP> list = new List<Sell_OrderFP>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.Sell_OrderFP model = new VAN_OA.Model.JXC.Sell_OrderFP();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }

                        ojb = dataReader["CreateTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreateTime = (DateTime)ojb;
                        }

                        model.GuestName = dataReader["GuestNAME"].ToString();

                        model.POName = dataReader["PONAME"].ToString();
                        model.PODate = Convert.ToDateTime(dataReader["PODate"]);

                        model.PONo = dataReader["PONo"].ToString();

                        ojb = dataReader["loginName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CreateName = ojb.ToString();
                        }


                        ojb = dataReader["FPNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FPNo = ojb.ToString();
                        }
                        ojb = dataReader["Total"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Total = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["Total"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Total = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["Isorder"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            if (ojb.ToString() == "1")
                            {
                                model.Isorder = true;
                            }
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
        public List<VAN_OA.Model.JXC.Sell_OrderFP> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" SumPoTotal-TuiTotal as AllPoTotal,Sell_OrderFP.Id,CreateUserId,CreateTime,RuTime,GuestNAME,DoPer,ProNo,Sell_OrderFP.PONo,POName,Remark ,tb_User.loginName as CreateName,Status,FPNo,FPNoStyle,Total,TopFPNo,TopTotal,IsTuiHuo ");
            strSql.Append(" from Sell_OrderFP left join tb_User on tb_User.id=CreateUserId left join POFP_View on POFP_View.pono=Sell_OrderFP.pono ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by Sell_OrderFP.Id desc");
            List<Sell_OrderFP> list = new List<Sell_OrderFP>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);

                        model.IsTuiHuo = Convert.ToBoolean(dataReader["IsTuiHuo"]);
                        model.AllPoTotal = Convert.ToDecimal(dataReader["AllPoTotal"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<string> GetFPtoInvoiceView_Doing(string where)
        {
            var list = new List<string>();
            var sql = @" SELECT [PONo] FROM Sell_OrderFP
left join 
(
SELECT FPId,sum(Total) as sumTotal FROM TB_ToInvoice WHERE State='执行中'
GROUP BY FPId
) as newtable 
on Sell_OrderFP.Id=newtable.FPId
where Sell_OrderFP.Status='通过' and (Total>newtable.sumTotal or newtable.sumTotal is null)  " + where;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(dataReader["PONo"].ToString());
                    }
                }
            }
            return list;

        }

        public List<Sell_OrderFP> GetFPtoInvoiceView_AccountCom(string where)
        {
            string strSql = @"SELECT GuestNAME,
       [Id]     
      ,[RuTime]      
      ,[ProNo]
      ,[PONo]
      ,[POName]     
      ,[FPNo]
      ,[FPNoStyle]
      ,[Total]
      ,newtable.sumTotal
      ,Total-ISNULL(newtable.sumTotal,0) as chaTotals      
       FROM Sell_OrderFP
left join 
(
SELECT FPId,sum(Total) as sumTotal FROM TB_ToInvoice WHERE State='通过'
GROUP BY FPId
) as newtable 
on Sell_OrderFP.Id=newtable.FPId
where Sell_OrderFP.Status='通过' and (Total>newtable.sumTotal or newtable.sumTotal is null) and " + where;
            List<VAN_OA.Model.JXC.Sell_OrderFP> list = new List<VAN_OA.Model.JXC.Sell_OrderFP>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.Sell_OrderFP model = new VAN_OA.Model.JXC.Sell_OrderFP();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }

                        ojb = dataReader["RuTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.RuTime = Convert.ToDateTime(ojb);
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
                        model.FPNoStyle = dataReader["FPNoStyle"].ToString();

                        list.Add(model);
                    }
                }
            }
            return list;
        }


        public List<Sell_OrderFP> GetFPtoInvoiceView(string where)
        {
            string strSql = @"SELECT GuestNAME,[Id],[RuTime],[ProNo],[PONo],[POName],[FPNo],[FPNoStyle],[Total],[sumTotal],[chaTotals]   FROM [Fp_ToInvoice] where " + where;
            List<VAN_OA.Model.JXC.Sell_OrderFP> list = new List<VAN_OA.Model.JXC.Sell_OrderFP>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.Sell_OrderFP model = new VAN_OA.Model.JXC.Sell_OrderFP();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }

                        ojb = dataReader["RuTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.RuTime = Convert.ToDateTime(ojb);
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
                        model.FPNoStyle = dataReader["FPNoStyle"].ToString();

                        list.Add(model);
                    }
                }
            }
            return list;
        }


        public List<Sell_OrderFP> GetFPtoInvoice(string where)
        {
            string strSql = @"select [Sell_OrderFP].[Id]  ,[Sell_OrderFP].[GuestNAME],[Sell_OrderFP].[ProNo] ,[Sell_OrderFP].[PONo],[Sell_OrderFP].[POName],[Sell_OrderFP].[FPNo] from Sell_OrderFP
left join TB_ToInvoice on TB_ToInvoice.FPID=Sell_OrderFP.id  and TB_ToInvoice.State='通过'  where Sell_OrderFP.status='通过' 
and TB_ToInvoice.id is null " + where;
            List<VAN_OA.Model.JXC.Sell_OrderFP> list = new List<VAN_OA.Model.JXC.Sell_OrderFP>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.Sell_OrderFP model = new VAN_OA.Model.JXC.Sell_OrderFP();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }


                        model.ProNo = dataReader["ProNo"].ToString();
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();



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
        public VAN_OA.Model.JXC.Sell_OrderFP ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.Sell_OrderFP model = new VAN_OA.Model.JXC.Sell_OrderFP();
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
            ojb = dataReader["RuTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RuTime = (DateTime)ojb;
            }
            model.GuestName = dataReader["GuestNAME"].ToString();
            model.DoPer = dataReader["DoPer"].ToString();


            model.ProNo = dataReader["ProNo"].ToString();
            model.PONo = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
            model.Remark = dataReader["Remark"].ToString();
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
                model.Total = Convert.ToDecimal(ojb);
            }
            model.TopFPNo = dataReader["TopFPNo"].ToString();
            model.TopTotal = Convert.ToDecimal(dataReader["TopTotal"]);

            return model;
        }



    }
}
