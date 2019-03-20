using System;
using System.Data;
using VAN_OA.Model.JXC;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.EFrom;
namespace VAN_OA.Dal.JXC
{

    public class CG_POOrderService
    {
        public bool updateTran(VAN_OA.Model.JXC.CG_POOrder model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms, List<CG_POOrders> orders, string IDS,
            List<CG_POCai> Cais, string CAI_IDS)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;

                CG_POOrdersService OrdersSer = new CG_POOrdersService();
                CG_POCaiService CaiSer = new CG_POCaiService();
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
                        //else if (Cais[i].Ids == 0)
                        //{
                        //    CaiSer.Add(Cais[i], objCommand);

                        //}
                    }
                    //if (CAI_IDS != "")
                    //{
                    //    CAI_IDS = CAI_IDS.Substring(0, CAI_IDS.Length - 1);
                    //    CaiSer.DeleteByIds(CAI_IDS, objCommand);
                    //} 
                    #endregion

                    if (eform.state == "通过")
                    {
                        UpdatePOStatus(model.PONo, objCommand, model.POTotal);
                    }
                    tan.Commit();

                    if (eform.state == "通过")
                    {
                        new CG_POOrderService().GetOrder_ToInvoiceAndUpdatePoStatus(model.PONo);
                        new CG_POOrdersService().GetListArrayToFpsAndUpdatePoStatue(model.PONo, eform.state);
                    }
                }
                catch (Exception)
                {
                    tan.Rollback();
                    return false;

                }

            }

            return true;
        }
        public int addTran(VAN_OA.Model.JXC.CG_POOrder model, VAN_OA.Model.EFrom.tb_EForm eform, List<CG_POOrders> orders, List<CG_POCai> caiOrders, out int MainId)
        {
            int id = 0;
            MainId = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                CG_POOrdersService OrdersSer = new CG_POOrdersService();

                CG_POCaiService caiSer = new CG_POCaiService();
                try
                {


                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No("CG_POOrder", objCommand);
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

                        CG_POCai cai = new CG_POCai();
                        cai.GuestName = orders[i].GuestName;
                        cai.Num = orders[i].Num;
                        cai.InvName = orders[i].InvName;
                        cai.Id = id;
                        cai.GoodId = orders[i].GoodId;
                        caiSer.Add(cai, objCommand);

                    }


                    //for (int i = 0; i < caiOrders.Count; i++)
                    //{
                    //    caiOrders[i].Id = id;

                    //    caiSer.Add(caiOrders[i], objCommand);


                    //}

                    //if(eform.state=="通过"&&model.IFZhui)

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
        /// 获取项目中商品的总数量
        /// </summary>
        /// <param name="poNo"></param>
        /// <param name="goodId"></param>
        /// <returns></returns>
        public decimal GetGoodTotalNum(string poNo, int goodId)
        {
            string sql = string.Format("select sum(Num) as totalNum from CG_POCai left join CG_POOrder on CG_POCai.id=CG_POOrder.id where Status='通过' and goodId={1} and  PoNo='{0}' ",
                poNo, goodId);
            object obj = DBHelp.ExeScalar(sql);
            if (obj is DBNull)
            {
                return 0;
            }
            return Convert.ToDecimal(obj);

        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.CG_POOrder model, SqlCommand objCommand)
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

            //if (model.fileNo != null)
            //{
            //    strSql1.Append("fileNo,");
            //    strSql2.Append("@fileNo,");

            //    SqlParameter pp = new System.Data.SqlClient.SqlParameter("@fileNo", System.Data.SqlDbType.Image, model.fileNo.Length);
            //    pp.Value = model.fileNo;
            //    objCommand.Parameters.Add(pp);
            //}
            if (model.fileType != null)
            {
                strSql1.Append("fileType,");
                strSql2.Append("'" + model.fileType + "',");
            }

            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }

            if (model.GuestId != null)
            {
                strSql1.Append("GuestId,");
                strSql2.Append("'" + model.GuestId + "',");
            }
            if (model.GuestNo != null)
            {
                strSql1.Append("GuestNo,");
                strSql2.Append("'" + model.GuestNo + "',");
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

            //项目编号

            if (model.PONo != null && model.PONo != "" && model.IFZhui == 1)
            {
                strSql1.Append("PONo,");
                strSql2.Append("'" + model.PONo + "',");
            }
            else if (model.PONo == "" && model.IFZhui == 0)
            {
                string MaxCardNo = "";
                string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(PONo),5))+1))),5) FROM  CG_POOrder where PONo like 'P{0}%'", DateTime.Now.Year);

                objCommand.CommandText = sql.ToString();
                object objMax = objCommand.ExecuteScalar();
                if (objMax != null && objMax.ToString() != "")
                {
                    MaxCardNo = "P" + DateTime.Now.Year.ToString() + objMax.ToString();
                }
                else
                {
                    MaxCardNo = "P" + DateTime.Now.Year.ToString() + "00001";
                }

                strSql1.Append("PONo,");
                strSql2.Append("'" + MaxCardNo + "',");
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
            if (model.POTotal != null)
            {
                strSql1.Append("POTotal,");
                strSql2.Append("" + model.POTotal + ",");
            }
            if (model.POPayStype != null)
            {
                strSql1.Append("POPayStype,");
                strSql2.Append("'" + model.POPayStype + "',");
            }

            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("'" + model.Status + "',");
            }

            if (model.IFZhui != null)
            {
                strSql1.Append("IFZhui,");
                strSql2.Append("" + model.IFZhui + ",");
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
            if (model.ZhangQiTotal != null)
            {
                strSql1.Append("ZhangQiTotal,");
                strSql2.Append("" + model.ZhangQiTotal + ",");
            }
            strSql1.Append("POStatue,");
            strSql2.Append("'" + CG_POOrder.ConPOStatue1 + "',");


            strSql1.Append("PORemark,");
            strSql2.Append("'" + model.PORemark + "',");

            strSql1.Append("Model,");
            strSql2.Append("'" + model.Model + "',");
            if (model.IsSpecial != null)
            {
                strSql1.Append("IsSpecial,");
                strSql2.Append("" + (model.IsSpecial ? 1 : 0) + ",");
            }

            if (model.IsPoFax != null)
            {
                strSql1.Append("IsPoFax,");
                strSql2.Append("" + (model.IsPoFax ? 1 : 0) + ",");
            }
            if (model.FpType != null)
            {
                strSql1.Append("FpType,");
                strSql2.Append("'" + model.FpType + "',");
            }
            if (model.FpTax != null)
            {
                strSql1.Append("FpTax,");
                strSql2.Append("" + model.FpTax + ",");
            }

            if (model.GuestXiShu != null)
            {
                strSql1.Append("GuestXiShu,");
                strSql2.Append("" + model.GuestXiShu + ",");
            }

            if (model.GuestType != null)
            {
                strSql1.Append("GuestType,");
                strSql2.Append("'" + model.GuestType + "',");
            }

            if (model.JiLiXiShu != null)
            {
                strSql1.Append("JiLiXiShu,");
                strSql2.Append("" + model.JiLiXiShu + ",");
            }

            if (model.GuestPro != null)
            {
                strSql1.Append("GuestPro,");
                strSql2.Append("" + model.GuestPro + ",");
            }

            if (model.IsSelected != null)
            {
                strSql1.Append("IsSelected,");
                strSql2.Append("" + (model.IsSelected ? 1 : 0) + ",");
            }
            strSql1.Append("POType,");
            strSql2.Append("" + model.POType + ",");
            strSql1.Append("PlanDays,");
            strSql2.Append("" + model.PlanDays + ",");
            //if (model.Status != null && model.Status != "执行中")
            //{
            //    strSql1.Append("TruePODate,");
            //    strSql2.Append("getdate(),");                
            //}
            strSql.Append("insert into CG_POOrder(");
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
        public void Update(VAN_OA.Model.JXC.CG_POOrder model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CG_POOrder set ");
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

            //if (model.POName != null)
            //{
            //    strSql.Append("POName='" + model.POName + "',");
            //}
            //if (model.PODate != null)
            //{
            //    strSql.Append("PODate='" + model.PODate + "',");
            //}
            //if (model.POTotal != null)
            //{
            //    strSql.Append("POTotal=" + model.POTotal + ",");
            //}
            if (model.POPayStype != null)
            {
                strSql.Append("POPayStype='" + model.POPayStype + "',");
            }

            if (model.Status != null)
            {
                strSql.Append("Status='" + model.Status + "',");
            }
            if (model.POType != null)
            {
                strSql.Append("POType=" + model.POType + ",");
            }
            if (model.Status != null && model.Status != "执行中")
            {
                if (model.IFZhui == 0 && !string.IsNullOrEmpty(model.PONo) && DateTime.Now.Year.ToString() != model.PONo.Substring(1, 4))
                {
                    strSql.AppendFormat("PODate='{0}-12-31 23:59:00',", model.PONo.Substring(1, 4));
                }
                else
                {
                    strSql.Append("PODate=getdate(),");
                }
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
            strSql.Append("delete from CG_POOrder ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.CG_POOrder GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Model,CG_POOrder.PoType,CG_POOrder.Id,AppName,loginName,CaiGou,cRemark ,fileName,fileType ,proNo,GuestId,GuestNo,GuestName,AE,INSIDE,PONo,POName,PODate,POTotal,POPayStype,Status,IFZhui,POStatue,PORemark,IsSpecial,IsPoFax,FpType,FpTax,PlanDays");
            strSql.Append(" from CG_POOrder left join tb_User on tb_User.id=CG_POOrder.AppName");
            strSql.Append(" where CG_POOrder.Id=" + id + "");

            VAN_OA.Model.JXC.CG_POOrder model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = ReaderBind(dataReader);
                        object ojb = dataReader["PORemark"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PORemark = ojb.ToString();
                        }

                        ojb = dataReader["IsSpecial"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsSpecial = Convert.ToBoolean(ojb);
                        }

                        model.IsPoFax = Convert.ToBoolean(dataReader["IsPoFax"]);
                        model.FpType = dataReader["FpType"].ToString();
                        model.FpTax = Convert.ToDecimal(dataReader["FpTax"]);
                        model.Model = dataReader["Model"].ToString();
                        model.PlanDays =Convert.ToInt32(dataReader["PlanDays"]);
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// 根据项目编码 来获取项目信息
        /// </summary>
        /// <param name="pono"></param>
        /// <returns></returns>
        public VAN_OA.Model.JXC.CG_POOrder GetModel(string pono)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" PONo,POName,GuestName,PODate,PlanDays");
            strSql.Append(" from CG_POOrder");
            strSql.Append(" where CG_POOrder.PONO='" + pono + "' AND IFZhui=0 AND Status='通过'");

            VAN_OA.Model.JXC.CG_POOrder model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = new CG_POOrder();
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        model.GuestName = dataReader["GuestName"].ToString();
                        model.PODate = Convert.ToDateTime(dataReader["PODate"]);
                        model.PlanDays = Convert.ToInt32(dataReader["PlanDays"]);
                    }
                }
            }
            return model;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.CG_POOrder GetModel_File(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" fileName,fileType,fileNo ");
            strSql.Append(" from CG_POOrder ");
            strSql.Append(" where CG_POOrder.Id=" + id + "");

            VAN_OA.Model.JXC.CG_POOrder model = null;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        model = new CG_POOrder();
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
        /// 根据付款款单修改项目订单状态
        /// </summary>
        /// <param name="poNo"></param>
        /// <returns></returns>
        public List<VAN_OA.Model.JXC.CG_POOrder> GetOrder_ToInvoiceAndUpdatePoStatus(string poNo)
        {

            StringBuilder strSql = new StringBuilder();

            strSql.Append(" SELECT [PONo]");
            strSql.Append(" from Order_ToInvoice_Count ");

            strSql.AppendFormat(" where PONo='{0}'", poNo);

            List<VAN_OA.Model.JXC.CG_POOrder> list = new List<VAN_OA.Model.JXC.CG_POOrder>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                object obj = objCommand.ExecuteScalar();
                if ((obj is DBNull) || obj == null)
                {
                    string sql = string.Format("update CG_POOrder set POStatue4='{0}' where PONo='{1}'", CG_POOrder.ConPOStatue4, poNo);
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                }
                else
                {
                    string sql = string.Format("update CG_POOrder set POStatue4='{0}' where PONo='{1}'", "", poNo);
                    objCommand.CommandText = sql;
                    objCommand.ExecuteNonQuery();
                }
            }
            return list;
        }


        public List<VAN_OA.Model.JXC.CG_POOrder> GetOrder_ToInvoice_1(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();

            strSql.Append(" SELECT [PONo],[POName],[GuestName],[POTotal],[TuiTotal],[sumTotal]");
            strSql.Append(" from Order_ToInvoice_1 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CG_POOrder> list = new List<VAN_OA.Model.JXC.CG_POOrder>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.CG_POOrder model = new VAN_OA.Model.JXC.CG_POOrder();
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        model.GuestName = dataReader["GuestName"].ToString();


                        object ojb;
                        ojb = dataReader["POTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["TuiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.TuiTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["sumTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.sumTotal = Convert.ToDecimal(ojb);
                        }
                        model.WeiTotal = model.POTotal - model.TuiTotal - model.sumTotal;

                        list.Add(model);
                    }
                }
            }
            return list;
        }



        public List<VAN_OA.Model.JXC.CG_POOrder> GetOrder_ToInvoice(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();

            strSql.Append(" SELECT [PONo],[POName],[GuestName],[POTotal],[TuiTotal],[sumTotal]");
            strSql.Append(" from Order_ToInvoice ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CG_POOrder> list = new List<VAN_OA.Model.JXC.CG_POOrder>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.CG_POOrder model = new VAN_OA.Model.JXC.CG_POOrder();
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        model.GuestName = dataReader["GuestName"].ToString();


                        object ojb;
                        ojb = dataReader["POTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["TuiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.TuiTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["sumTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.sumTotal = Convert.ToDecimal(ojb);
                        }
                        model.WeiTotal = model.POTotal - model.TuiTotal - model.sumTotal;
                        model.ifZenoPoInfo = model.POTotal - model.TuiTotal;
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<VAN_OA.Model.JXC.CG_POOrder> GetSellGoodsPoInfo(string strWhere, int AppId)
        {
            string sql = string.Format(@"select  CG_POOrder.Id,newtable1.PONo,POName, GuestId,GuestNo,GuestName,AE,INSIDE,PODate,newtable1.PONums,totalNums from 
(select sum(Num) as PONums,PONo from CG_POOrder left join CG_POOrders on CG_POOrder.id=CG_POOrders.id
where  Status='通过' and AppName={0}
group by PONo) as newtable1 left join (
select sum(GoodNum) as totalNums,Pono  from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id where Status<>'不通过' group by Pono) as newtable2 on newtable1.PONo=newtable2.PONo
left join CG_POOrder on newtable1.PONo=CG_POOrder.poNo where " + strWhere + " ", AppId);
            sql += " order by  CG_POOrder.Id desc";
            List<VAN_OA.Model.JXC.CG_POOrder> list = new List<VAN_OA.Model.JXC.CG_POOrder>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = new CG_POOrder();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        ojb = dataReader["PODate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PODate = (DateTime)ojb;
                        }
                        model.GuestNo = dataReader["GuestNo"].ToString();
                        model.GuestName = dataReader["GuestName"].ToString();
                        model.AE = dataReader["AE"].ToString();
                        model.INSIDE = dataReader["INSIDE"].ToString();
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();

                        decimal PONums = 0;
                        decimal totalNums = 0;
                        ojb = dataReader["PONums"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            PONums = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["totalNums"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            totalNums = Convert.ToDecimal(ojb);
                        }

                        if (PONums > totalNums)
                        {
                            model.IfCanSell = true;
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
        public List<VAN_OA.Model.JXC.CG_POOrder> GetSimpListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select * from (select PONo,POName,PODate,GuestNo,GuestName,AE,INSIDE from CG_POOrder where IFZhui=0 and Status='通过' {0}
union
select PONo,POName,PODate,GuestNo,GuestName,AE,INSIDE from CAI_POOrder where  Status='通过' and PONo like 'KC%' {0}) as temp order by PODate desc;", strWhere);

            List<VAN_OA.Model.JXC.CG_POOrder> list = new List<VAN_OA.Model.JXC.CG_POOrder>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        VAN_OA.Model.JXC.CG_POOrder model = new VAN_OA.Model.JXC.CG_POOrder();
                        object ojb;
                        model.GuestNo = dataReader["GuestNo"].ToString();
                        model.GuestName = dataReader["GuestName"].ToString();
                        model.AE = dataReader["AE"].ToString();
                        model.INSIDE = dataReader["INSIDE"].ToString();
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        ojb = dataReader["PODate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PODate = (DateTime)ojb;
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<VAN_OA.Model.JXC.CG_POOrder> GetSimpListArray(string strWhere, SqlCommand sqlCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select * from (select PONo,POName,PODate,GuestNo,GuestName,AE,INSIDE from CG_POOrder where IFZhui=0 and Status='通过' {0}
union
select PONo,POName,PODate,GuestNo,GuestName,AE,INSIDE from CAI_POOrder where  Status='通过' and PONo like 'KC%' {0}) as temp order by PODate desc;", strWhere);

            List<VAN_OA.Model.JXC.CG_POOrder> list = new List<VAN_OA.Model.JXC.CG_POOrder>();


            sqlCommand.CommandText = strSql.ToString();
            using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    VAN_OA.Model.JXC.CG_POOrder model = new VAN_OA.Model.JXC.CG_POOrder();
                    object ojb;
                    model.GuestNo = dataReader["GuestNo"].ToString();
                    model.GuestName = dataReader["GuestName"].ToString();
                    model.AE = dataReader["AE"].ToString();
                    model.INSIDE = dataReader["INSIDE"].ToString();
                    model.PONo = dataReader["PONo"].ToString();
                    model.POName = dataReader["POName"].ToString();
                    ojb = dataReader["PODate"];
                    if (ojb != null && ojb != DBNull.Value)
                    {
                        model.PODate = (DateTime)ojb;
                    }
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取项目订单明细 包含原+追加的订单明细
        /// </summary>
        public List<VAN_OA.Model.JXC.CG_POOrder> GetPOOrderDetailList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select POTotal,PODate,IFZhui,PlanDays,POStatue2 from CG_POOrder where Status='通过'");   
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" order by Id");
            List<CG_POOrder> list = new List<CG_POOrder>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = new CG_POOrder();                         
                        model.PODate =Convert.ToDateTime(dataReader["PODate"]);
                        model.IFZhui = Convert.ToInt32(dataReader["IFZhui"]);
                        model.PlanDays = Convert.ToInt32(dataReader["PlanDays"]);
                        model.POTotal = Convert.ToDecimal(dataReader["POTotal"]);
                        var ojb = dataReader["POStatue2"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POStatue2 = ojb.ToString();
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
        public List<VAN_OA.Model.JXC.CG_POOrder> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Model,Order_ToInvoice_1.[POTotal] as InvoTotal,[TuiTotal],TB_BasePoType.BasePoType,POType,GuestPro,GuestType,CG_POOrder.Id,AppName,loginName,CaiGou ,cRemark,fileName,fileType,proNo,GuestId,GuestNo,CG_POOrder.GuestName,AE,INSIDE,CG_POOrder.PONo,CG_POOrder.POName,PODate,CG_POOrder.POTotal,POPayStype ,Status,IFZhui,FPTotal,POStatue,POStatue2,POStatue3,POStatue4,POStatue5,POStatue6,PORemark,IsSpecial,IsPoFax ");
            strSql.Append(" from CG_POOrder left join tb_User on tb_User.id=CG_POOrder.AppName left join TB_BasePoType on TB_BasePoType.id=CG_POOrder.POType ");
            strSql.Append(" left join Order_ToInvoice_1 on Order_ToInvoice_1.PONO=CG_POOrder.PONO ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.Append(" order by  CG_POOrder.proNo desc");
            List<VAN_OA.Model.JXC.CG_POOrder> list = new List<VAN_OA.Model.JXC.CG_POOrder>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind_1(dataReader);
                        object ojb;
                        model.Model = dataReader["Model"].ToString();
                        ojb = dataReader["FPTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.FPTotal = ojb.ToString();
                        }

                        ojb = dataReader["GuestType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestType = ojb.ToString();
                        }
                        model.GuestPro = -1;
                        ojb = dataReader["GuestPro"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestPro = Convert.ToInt32(ojb);
                        }

                        ojb = dataReader["POStatue2"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            if (ojb.ToString() != "")
                                model.POStatue += "," + ojb.ToString();
                            model.POStatue2 = ojb.ToString();
                        }

                        ojb = dataReader["POStatue3"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            if (ojb.ToString() != "")
                                model.POStatue += "," + ojb.ToString();
                            model.POStatue3 = ojb.ToString();
                        }
                        ojb = dataReader["POStatue4"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            if (ojb.ToString() != "")
                                model.POStatue += "," + ojb.ToString();
                            //model.POStatue4 = ojb.ToString();
                        }


                        model.POStatue5 = CG_POOrder.ConPOStatue5_1;
                        model.POStatue6 = CG_POOrder.ConPOStatue6_1;
                        ojb = dataReader["POStatue5"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            if (ojb.ToString() != "")
                                model.POStatue5 = ojb.ToString();
                            //model.POStatue3 = ojb.ToString();
                        }
                        ojb = dataReader["POStatue6"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            if (ojb.ToString() != "")
                                model.POStatue6 = ojb.ToString();
                        }

                        ojb = dataReader["PORemark"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PORemark = ojb.ToString();
                        }

                        ojb = dataReader["IsSpecial"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsSpecial = Convert.ToBoolean(ojb);
                        }
                        ojb = dataReader["IsPoFax"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsPoFax = Convert.ToBoolean(ojb);
                        }
                        ojb = dataReader["BasePoType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTypeString = ojb.ToString();
                        }

                        model.POType = Convert.ToInt32(dataReader["POType"]);
                        decimal InvoTotal = 0;
                        decimal TuiTotal = 0;
                        ojb = dataReader["InvoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            InvoTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["TuiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            TuiTotal = Convert.ToDecimal(ojb);
                        }
                        model.POTotal = InvoTotal - TuiTotal;
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.CG_POOrder> GetListArrayToQuery(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append("  POType,CG_POOrder.Id,AppName,loginName,CaiGou ,cRemark,fileName,fileType,proNo,GuestId,GuestNo,GuestName,AE,INSIDE,PONo,POName,PODate,POTotal,POPayStype ,Status,IFZhui ,POStatue");
            strSql.Append(" from CG_POOrder left join tb_User on tb_User.id=CG_POOrder.AppName");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CG_POOrder> list = new List<VAN_OA.Model.JXC.CG_POOrder>();

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
        /// 设置项目为特殊项目
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<VAN_OA.Model.JXC.CG_POOrder> FIN_SetPoSpecial(string where)
        {
            string sql = @"select goodTotal,maoliTotal,TB_BasePoType.BasePoType,CG_POOrder.Id,CG_POOrder.ProNo,GuestName,CG_POOrder.PONo,POName,PODate,IsSpecial,AE,IsPoFax,FpType,IsClose,IsSelected,JieIsSelected,
newtable1.POTotal-isnull(TuiTotal,0) as POTotal,Total from CG_POOrder left join
(select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过' 
group by PONo) as newtable1 ON newtable1.PONo=CG_POOrder.PONo
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on CG_POOrder.PONo= newtable2.PONo
left join (select pono ,sum(Total) as Total,Min(ProNo) as minProNo,min(DaoKuanDate) as MinDaoKuanDate from  TB_ToInvoice
 where  (State='通过') group by PONo )as newtable4 on CG_POOrder.PONo= newtable4.PONo  left join TB_BasePoType on TB_BasePoType.id=CG_POOrder.POType
left join (select pono,isnull(sum(maoli),0) as maoliTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal from JXC_REPORT group by pono) as REPORT on REPORT.PONo=CG_POOrder.PONo";
            //sql += " , 1 as isCloseEdist,1 as isSpecialEdit, 1 as isFaxEdist, 1 as isFPTypeEdist from CG_POOrder";
            sql += " where IFZhui=0 and Status='通过'" + where;
            sql += " order by CG_POOrder.PONo desc ";
            List<VAN_OA.Model.JXC.CG_POOrder> list = new List<VAN_OA.Model.JXC.CG_POOrder>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        VAN_OA.Model.JXC.CG_POOrder model = new VAN_OA.Model.JXC.CG_POOrder();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }
                        ojb = dataReader["ProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ProNo = ojb.ToString();
                        }
                        model.GuestName = dataReader["GuestName"].ToString();
                        model.AE = dataReader["AE"].ToString();
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        ojb = dataReader["PODate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PODate = (DateTime)ojb;
                        }
                        ojb = dataReader["POTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal = (decimal)ojb;
                        }
                        ojb = dataReader["Total"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvoiceTotal = (decimal)ojb;
                        }
                        model.IsSpecial = (bool)dataReader["IsSpecial"];
                        model.IsPoFax = (bool)dataReader["IsPoFax"];
                        model.IsClose = (bool)dataReader["IsClose"];
                        model.IsSelected = (bool)dataReader["IsSelected"];
                        model.JieIsSelected = (bool)dataReader["JieIsSelected"];
                        model.FpType = dataReader["FpType"].ToString();
                        if (model.POTotal > 0)
                        {
                            model.BILI = model.InvoiceTotal / model.POTotal * 100;
                        }
                        ojb = dataReader["BasePoType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTypeString = ojb.ToString();
                        }
                        ojb = dataReader["goodTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodTotal = (decimal)ojb;
                        }
                        ojb = dataReader["maoliTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MaoliTotal = (decimal)ojb;
                        }
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 设置项目为特殊项目
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable SetPoSpecial(string where)
        {
            string sql = @"select ChengBenJiLiang,GuestPro,GuestType,SumPOTotal,maoliTotal,POType,Id,ProNo,GuestName,CG_POOrder.PONo,POName,PODate,IsSpecial,AE,IsPoFax,FpType,IsClose,IsSelected,JieIsSelected,Model  from CG_POOrder";
            //sql += " , 1 as isCloseEdist,1 as isSpecialEdit, 1 as isFaxEdist, 1 as isFPTypeEdist from CG_POOrder";
            sql += " left join (select pono,isnull(sum(maoli),0) as maoliTotal from JXC_REPORT group by pono) as REPORT on REPORT.PONo=CG_POOrder.PONo";
            sql += " left join POTotal_SumView on POTotal_SumView.PONO=CG_POOrder.PONO";
            sql += " where IFZhui=0 and Status='通过'" + where;
            sql += " order by CG_POOrder.PONo desc ";
            return DBHelp.getDataTable(sql);
        }
        public static void StaticUpdatePOStatus(string poNo, SqlCommand objCommend, string state)
        {
            string sql = string.Format("update CG_POOrder set POStatue='{0}' where PONo='{1}'", state, poNo);
            DBHelp.ExeCommand(sql);
        }
        public void UpdatePOStatus(string poNo, SqlCommand objCommend)
        {
            string sql = string.Format("update CG_POOrder set POStatue='{0}',POStatue2='',POStatue3='',POStatue4='' where PONo='{1}'", CG_POOrder.ConPOStatue1, poNo);
            objCommend.CommandText = sql;
            objCommend.ExecuteNonQuery();
        }

        public void UpdatePOStatus(string poNo, SqlCommand objCommend, decimal poTotal)
        {

            string sql = "";
            if (poTotal != 0)
            {
                sql = string.Format("update CG_POOrder set POStatue='{0}',POStatue2='',POStatue3='',POStatue4='' where PONo='{1}'", CG_POOrder.ConPOStatue1, poNo);
            }
            else
            {
                sql = string.Format("update CG_POOrder set POStatue='{0}',POStatue2='' where PONo='{1}'", CG_POOrder.ConPOStatue1, poNo);
            }
            objCommend.CommandText = sql;
            objCommend.ExecuteNonQuery();
        }


        /// <summary>
        /// 查询所有审批成功的但尚未全部用于采购订单的
        /// </summary>
        public List<VAN_OA.Model.JXC.CG_POOrder> GetListArrayToQueryDio(string strWhere)
        {
            //            create view CG_POOrder_Cai_POOrder_View
            //as
            //select CG_POOrder.Id,proNo,GuestId,GuestNo,GuestName,AE,INSIDE,PONo,POName,PODate,POTotal,POPayStype
            //from CG_POOrder 
            //where Id in( 
            //select Id from CG_POOrders 
            //left join 
            //(
            //select  CG_POOrdersId,SUM(Num) as totalOrderNum from CAI_POOrders 
            //where CG_POOrdersId<>0  and  id in (select allE_id from tb_EForm where proId in (
            //select pro_Id from A_ProInfo where pro_Type='采购订单') and state<>'不通过')
            //group by CG_POOrdersId
            //)
            //as newtable on CG_POOrders.Ids=newtable.CG_POOrdersId where (CG_POOrders.Num>newtable.totalOrderNum or totalOrderNum is null)
            //and id in (select allE_id from tb_EForm where proId in (
            //select pro_Id from A_ProInfo where pro_Type='项目订单') and state='通过')
            //) 

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" * ");
            strSql.Append(" from CG_POOrder_Cai_POOrder_View");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.CG_POOrder> list = new List<VAN_OA.Model.JXC.CG_POOrder>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        VAN_OA.Model.JXC.CG_POOrder model = new VAN_OA.Model.JXC.CG_POOrder();
                        object ojb;
                        ojb = dataReader["Id"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Id = (int)ojb;
                        }




                        ojb = dataReader["ProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ProNo = ojb.ToString();
                        }

                        ojb = dataReader["GuestId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestId = (int)ojb;
                        }
                        model.GuestNo = dataReader["GuestNo"].ToString();
                        model.GuestName = dataReader["GuestName"].ToString();
                        model.AE = dataReader["AE"].ToString();
                        model.INSIDE = dataReader["INSIDE"].ToString();
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        ojb = dataReader["PODate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PODate = (DateTime)ojb;
                        }
                        ojb = dataReader["POTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal = (decimal)ojb;
                        }
                        model.POPayStype = dataReader["POPayStype"].ToString();

                        ojb = dataReader["POStatue4"];
                        model.POStatue4 = "未结清";
                        if (ojb != null && ojb != DBNull.Value && ojb.ToString() != "")
                        {
                            model.POStatue4 = ojb.ToString();
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
        public VAN_OA.Model.JXC.CG_POOrder ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.CG_POOrder model = new VAN_OA.Model.JXC.CG_POOrder();
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

            ojb = dataReader["GuestId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestId = (int)ojb;
            }
            model.GuestNo = dataReader["GuestNo"].ToString();
            model.GuestName = dataReader["GuestName"].ToString();
            model.AE = dataReader["AE"].ToString();
            model.INSIDE = dataReader["INSIDE"].ToString();
            model.PONo = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
            ojb = dataReader["PODate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PODate = (DateTime)ojb;
            }
            ojb = dataReader["POTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POTotal = (decimal)ojb;
            }
            model.POPayStype = dataReader["POPayStype"].ToString();
            ojb = dataReader["Status"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Status = ojb.ToString();
            }

            ojb = dataReader["IFZhui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IFZhui = Convert.ToInt32(ojb);
            }

            try
            {
                ojb = dataReader["POStatue"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.POStatue = Convert.ToString(ojb);
                }
            }
            catch (Exception)
            {


            }
            model.POType = Convert.ToInt32(dataReader["POType"]);
            return model;
        }



        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.CG_POOrder ReaderBind_1(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.CG_POOrder model = new VAN_OA.Model.JXC.CG_POOrder();
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

            ojb = dataReader["GuestId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GuestId = (int)ojb;
            }
            model.GuestNo = dataReader["GuestNo"].ToString();
            model.GuestName = dataReader["GuestName"].ToString();
            model.AE = dataReader["AE"].ToString();
            model.INSIDE = dataReader["INSIDE"].ToString();
            model.PONo = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
            ojb = dataReader["PODate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PODate = (DateTime)ojb;
            }
            ojb = dataReader["POTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POTotal = (decimal)ojb;
            }
            model.POPayStype = dataReader["POPayStype"].ToString();
            ojb = dataReader["Status"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Status = ojb.ToString();
            }

            ojb = dataReader["IFZhui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IFZhui = Convert.ToInt32(ojb);
            }

            try
            {
                ojb = dataReader["POStatue"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.POStatue = Convert.ToString(ojb);
                }
            }
            catch (Exception)
            {


            }


            return model;
        }


        public bool ExistPONO(string pono)
        {
            if (pono == "") return true;
            string sql = string.Format("select COUNT(*) FROM CG_POOrder where PONO='{0}'", pono);
            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 项目是否关闭 关闭就不能在做单子啦
        /// </summary>
        /// <param name="pono"></param>
        /// <returns></returns>
        public static bool IsClosePONO(string pono)
        {
            if (pono == "") return true;
            string sql = string.Format("select IsClose FROM CG_POOrder where PONO='{0}' and IFZhui=0 ", pono);
            return Convert.ToBoolean(DBHelp.ExeScalar(sql));

        }

        /// <summary>
        /// 判断单据号对应的项目是否为追加信息
        /// </summary>
        /// <param name="proNo"></param>
        /// <returns></returns>
        public string IfZhuiByProNo(string proNo)
        {
            string sql = string.Format("select IFZhui FROM CG_POOrder where proNo='{0}'", proNo);
            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
            {
                return "追";
            }
            return "原";
        }

        /// <summary>
        /// 判断单据号对应的项目是否为追加信息
        /// </summary>
        /// <param name="proNo"></param>
        /// <returns></returns>
        public string IfSpecialByProNo(string proNo)
        {
            string sql = string.Format("select IsSpecial FROM CG_POOrder where proNo='{0}'", proNo);
            if (Convert.ToBoolean(DBHelp.ExeScalar(sql)))
            {
                return "特殊订单";
            }
            return "";
        }
    }
}
