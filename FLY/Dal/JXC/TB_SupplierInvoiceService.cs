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
    public class TB_SupplierInvoiceService
    {
        /// <summary>
        /// 检查预付和支付单有么有在执行的扣款 在执行
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="tyle">1 预付款 2 支付</param>
        /// <returns></returns>
        public static bool CheckAdvanceAndSupplierInvoices(string supplier,int tyle)
        {
            string sql="";
            if (tyle == 1)
            {
                sql = string.Format(@"select count(*) from TB_TempSupplierInvoice 
left join TB_SupplierAdvancePayment on TB_TempSupplierInvoice.SupplierAdvanceId=TB_SupplierAdvancePayment.Id
 left join  TB_SupplierAdvancePayments on TB_SupplierAdvancePayment.Id=TB_SupplierAdvancePayments.id 
 left join CAI_POCai on CAI_POCai.Ids=TB_SupplierAdvancePayments.CaiIds 
  where lastSupplier='{0}' and status='执行中'", supplier);
            }
            if (tyle == 2)
            {
                sql = string.Format(@"  select count(*) from TB_TempSupplierInvoice
   left join TB_SupplierInvoice on TB_TempSupplierInvoice.SupplierInvoiceId=TB_SupplierInvoice.Id
  left join TB_SupplierInvoices on TB_SupplierInvoice.Id=TB_SupplierInvoices.id 
  where lastSupplier='{0}' and status='执行中' and IsYuFu=0", supplier);
            }

            return Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0;
        }

        public static bool checkSupplierDoing(string supplierName)
        {
            string sql = string.Format("select count(*) from TB_SupplierInvoice left join TB_SupplierInvoices on TB_SupplierInvoice.Id=TB_SupplierInvoices.id where lastSupplier='{0}' and status='执行中' and IsYuFu=0", supplierName);

//            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) == 0)
//            {

//                sql = string.Format(@"select count(*) from TB_SupplierAdvancePayment left join  TB_SupplierAdvancePayments on TB_SupplierAdvancePayment.Id=TB_SupplierAdvancePayments.id 
// left join CAI_POCai on CAI_POCai.Ids=TB_SupplierAdvancePayments.CaiIds where lastSupplier='{0}' and status='执行中' ", supplierName);
//                return Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0;
//            }
            return Convert.ToInt32(DBHelp.ExeScalar(sql))>0;
        }

        public tb_EForm GetToSupplierInvoice()
        {
            var eform = new tb_EForm();
          
            tb_EFormService eformSer = new tb_EFormService();
            if (eformSer.ifHasNodes(33))//33代表 供应商付款单（预付单转支付单）
            {
                //获取审批人                
                int ids = 0;
                List<A_Role_User> roleUserList = eformSer.getFristNodeUsers(1, 33, out ids);//33代表 供应商付款单（预付单转支付单）
                if (roleUserList.Count > 0)
                {
                    eform.appPer = 1;
                    eform.appTime = DateTime.Now;
                    eform.createPer = 1;
                    eform.createTime = DateTime.Now;
                    eform.proId = 33;//33代表 供应商付款单（预付单转支付单）

                    eform.state = "执行中";
                    eform.toPer = roleUserList[0].UserId;
                    eform.toProsId = ids;
                }

            }
            else
            {
                
                eform.appPer = 1;
                eform.appTime = DateTime.Now;
                eform.createPer = 1;
                eform.createTime = DateTime.Now;
                eform.proId = 33;//33代表 供应商付款单（预付单转支付单）
                eform.state = "通过";
                eform.toPer = 0;
                eform.toProsId = 0;
                
            }

            return eform;
        }

        public bool AddSupplierInvoice(string checkIds, string caiIds, string CreateName,int createId,string supplierName)
        {
            if (checkIds != "" && caiIds != "")
            {
                try
                {
                    Global.log.Info(string.Format("checkIds:{0},caiIds:{1}" ,checkIds,caiIds));
                }
                catch (Exception)
                {


                }
                var paySer=new SupplierAdvancePaymentsToPayService();
                var allList = paySer.GetListArray(checkIds, caiIds);
                if (allList.Count > 0)
                {
                    List<string>  fpList=paySer.GetFPInfo(caiIds);
                    tb_EForm eform = GetToSupplierInvoice();

                    TB_SupplierInvoice model = new TB_SupplierInvoice();
                    model.Status = eform.state;
                    model.CreteTime = DateTime.Now;
                    model.CreateName = "admin";
                    //model.Remark = "补单";
                    model.FristFPNo = fpList[0];
                    model.LastFPNo = fpList[1];
                    model.SecondFPNo = fpList[2];
                    try
                    {
                        Global.log.Info(string.Format("count:" + allList.Count));
                    }
                    catch (Exception)
                    {


                    }
                    List<SupplierToInvoiceView> orders = new List<SupplierToInvoiceView>();
                    using (SqlConnection conn = DBHelp.getConn())
                    {
                        conn.Open();
                        SqlCommand objCommand = conn.CreateCommand();
                        foreach (var supplierAdvancePayments in allList)
                        {
                            var invoiceModel = new SupplierToInvoiceView();
                            invoiceModel.Ids = supplierAdvancePayments.inHouseIds;
                            string sql = string.Format(@"declare @allFpNo varchar(500); 
SELECT @allFpNo=ISNULL(@allFpNo+',','')+SupplierFPNo
FROM (SELECT SupplierFPNo FROM TB_SupplierAdvancePayments where CaiIds={0} and SupplierFPNo<>'' )AS T;
SELECT @allFpNo", supplierAdvancePayments.ids);
                            objCommand.CommandText = sql;
                            object ojb = objCommand.ExecuteScalar();
                            string allFpNo = "";
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                allFpNo = ojb.ToString();
                            }
                            invoiceModel.IsYuFu = true;
                            invoiceModel.SupplierFPNo = allFpNo;
                            invoiceModel.SupplierInvoiceDate = supplierAdvancePayments.MinSupplierInvoiceDate;
                            var AvgSupplierInvoicePrice = supplierAdvancePayments.SupplierInvoiceTotal / supplierAdvancePayments.allNum;
                            invoiceModel.SupplierInvoicePrice = AvgSupplierInvoicePrice;
                            invoiceModel.lastGoodNum = supplierAdvancePayments.CheckNum;
                            invoiceModel.SupplierInvoiceTotal = AvgSupplierInvoicePrice * supplierAdvancePayments.CheckNum;
                            invoiceModel.GuestName = supplierName;
                            invoiceModel.ActPay = invoiceModel.SupplierInvoiceTotal;
                           
                            //                            sql = string.Format(@"select Sum(SupplierInvoiceTotal) as  FuShuTotal from 
                            //TB_SupplierInvoices 
                            //left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
                            //where status='通过' and  SupplierInvoiceTotal<0 and RuIds={0}
                            //group by RuIds", invoiceModel.Ids);
                            //                            object obj = DBHelp.ExeScalar(sql);
                            //                            if(obj!=null&&!(obj is DBNull))
                            //                            {
                            //                                invoiceModel.ActPay = Convert.ToDecimal(obj);
                            //                            }
                            //                            sql = string.Format(@"select Sum(SupplierInvoiceTotal) as  HadSupplierInvoiceTotal from 
                            //TB_SupplierInvoices 
                            //left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
                            //where status='通过' and RuIds={0}
                            //group by RuIds", invoiceModel.Ids);

                            //object obj = DBHelp.ExeScalar(sql);
                            //if (obj != null && !(obj is DBNull))
                            //{
                            //    invoiceModel.FuShuTotal = Convert.ToDecimal(obj);
                            //}
                            invoiceModel.FuShuTotal = 0;

                            //这里要重新 查找 未付清 或已支付状态
                            decimal resultTotal = supplierAdvancePayments.CheckNum * supplierAdvancePayments.lastPrice - supplierAdvancePayments.SupplierInvoiceTotal;
                            if (resultTotal > 0)
                            {
                                invoiceModel.RePayClear = 1;
                                invoiceModel.IsPayStatus = 1;
                                orders.Add(invoiceModel);
                            }
                            else if (resultTotal == 0)
                            {
                                invoiceModel.RePayClear = 1;
                                invoiceModel.IsPayStatus = 2;
                                orders.Add(invoiceModel);
                            }
                            else
                            {                               
                                //只要预付了 不管全部和部分都是 也是结清，和已支付
                                invoiceModel.RePayClear = 1;
                                invoiceModel.IsPayStatus = 2;
                                orders.Add(invoiceModel);
                            }

                            try
                            {
                                Global.log.Info(string.Format("num:{0},price:{1},total:{2},resultTotal:{3}",
                                    supplierAdvancePayments.CheckNum, supplierAdvancePayments.lastPrice, 
                                    supplierAdvancePayments.SupplierInvoiceTotal, resultTotal));
                            }
                            catch (Exception)
                            {
                                
                                 
                            }
                        }
                        conn.Close();
                    }
                    int mainId=0;
                    if (orders.Count > 0)
                    {
                        addTran(model, eform, orders, out mainId);
                    }
                    if(mainId>0)
                    {
                        //new TB_SupplierInvoiceService().SetRuKuPayStatus_ToAdd(orders);

                        //foreach (var m in orders)
                        //{
                        //    using (SqlConnection conn = DBHelp.getConn())
                        //    {
                        //        conn.Open();
                        //        SqlCommand objCommand = conn.CreateCommand();
                        //        objCommand.CommandText = string.Format("update CAI_OrderInHouses set PayStatus=2 where Ids={0} ",m.Ids);
                        //        objCommand.ExecuteNonQuery();
                        //        conn.Close();
                        //    }
                        //}                        
                    }
                }             
                

            }
            return true;
        }


        public bool AddSupplierInvoice(List<CAI_OrderOutHouses> allList, string CreateName,
            int createId, string supplierName,string caiTuiProNo)
        {
            if (allList.Count > 0)
            {
                TB_SupplierInvoice model = new TB_SupplierInvoice();
                model.Status = "通过";
                model.CreteTime = DateTime.Now;
                model.CreateName = CreateName;
                model.CaiTuiProNo = caiTuiProNo;
                model.LastSupplier = supplierName;
               
                tb_EForm eform = new tb_EForm();
                eform.appPer = createId;
                eform.appTime = DateTime.Now;
                eform.createPer = createId;
                eform.createTime = DateTime.Now;
                eform.proId = 31;
                eform.state = "通过";
                eform.toPer = 0;
                eform.toProsId = 0;

                List<int> toYYList = new List<int>();

                List<int> toAllTuiList = new List<int>();
                List<SupplierToInvoiceView> orders = new List<SupplierToInvoiceView>();
                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlCommand objCommand = conn.CreateCommand();
                    foreach (var supplierAdvancePayments in allList)
                    {
                        var invoiceModel = new SupplierToInvoiceView();
                        invoiceModel.Ids = supplierAdvancePayments.OrderCheckIds;
                        
                        //invoiceModel.IsYuFu = true;
                        invoiceModel.SupplierFPNo = "";
                        invoiceModel.SupplierInvoiceDate = DateTime.Now;
                        //var AvgSupplierInvoicePrice = -supplierAdvancePayments.GoodPrice;
                        var AvgSupplierInvoicePrice = -supplierAdvancePayments.CaiLastTruePrice;

                        invoiceModel.SupplierInvoicePrice = AvgSupplierInvoicePrice;
                        invoiceModel.lastGoodNum = supplierAdvancePayments.GoodNum;
                        invoiceModel.SupplierInvoiceTotal = AvgSupplierInvoicePrice * supplierAdvancePayments.GoodNum;
                        invoiceModel.GuestName = supplierName;
                        invoiceModel.CaiTuiProNo = caiTuiProNo;      
                        invoiceModel.FuShuTotal = invoiceModel.SupplierInvoiceTotal;
                        invoiceModel.ActPay = invoiceModel.SupplierInvoiceTotal;
                        invoiceModel.RePayClear = 2;
                        invoiceModel.IsPayStatus = 0;//未支付

                        //下面‘之前所有负数支付单金额’中的负数单是指 合并=0 并且是 未结清 未支付  的负数单 
                         
                        //必须判断： （入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额<=0
                        //CaiLastTruePrice=GoodPrice
                        var sql = string.Format(@"select CAI_OrderInHouses.ids,GoodNum,CaiLastTruePrice as GoodPrice,isnull(SupplierInvoiceTotal,0) as allSupplierInvoiceTotal,
isnull(FuShuSupplierInvoiceTotal,0) as FuShuSupplierInvoiceTotal
from CAI_OrderInHouses 
left join CAI_OrderInHouse on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id
left join 
(
select RuIds,Sum(SupplierInvoiceTotal) as  SupplierInvoiceTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where status<>'不通过' and SupplierInvoiceTotal>0 and RuIds ={0} 
group by RuIds
)
as tb1 on CAI_OrderInHouses.IDs=tb1.RuIds

left join 
(
select RuIds,Sum(SupplierInvoiceTotal) as  FuShuSupplierInvoiceTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where status='通过' and SupplierInvoiceTotal<0 and IsHeBing=0 and RuIds ={0}
group by RuIds
)
as tb2 on CAI_OrderInHouses.IDs=tb2.RuIds

where CAI_OrderInHouse.status='通过' and CAI_OrderInHouses.ids = {0}", supplierAdvancePayments.OrderCheckIds);

                        objCommand.CommandText = sql;

                        decimal resultTotal = 0;

                        decimal hadTotal = 0;
                        using (SqlDataReader dataReader = objCommand.ExecuteReader())
                        {
                            if (dataReader.Read())
                            {
                                hadTotal = Convert.ToDecimal(dataReader["allSupplierInvoiceTotal"]);
                                 resultTotal = Convert.ToDecimal(dataReader["GoodPrice"]) * Convert.ToDecimal(dataReader["GoodNum"]) - Convert.ToDecimal(dataReader["allSupplierInvoiceTotal"])+
                                    Convert.ToDecimal(dataReader["FuShuSupplierInvoiceTotal"]) + invoiceModel.SupplierInvoiceTotal;
                            }
                        }
                        //必须判断:（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额<0
                       
                        if (resultTotal < 0)
                        {
                            var checkAllTui = string.Format(@"select CAI_OrderInHouses.GoodNum- isnull(newtable.totalOrderNum,0) from CAI_OrderInHouses 
left join CAI_OrderInHouse on CAI_OrderInHouses.id=CAI_OrderInHouse.id
left join 
(
select  OrderCheckIds,SUM(GoodNum) as totalOrderNum from CAI_OrderOutHouses left join 
CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.id
where OrderCheckIds<>0  and status='通过'
group by OrderCheckIds
)
as newtable on CAI_OrderInHouses.Ids=newtable.OrderCheckIds 
where  status='通过' and CAI_OrderInHouses.ids={0};", supplierAdvancePayments.OrderCheckIds);
                            objCommand.CommandText = checkAllTui;

                            var objCount = objCommand.ExecuteScalar();
                            if (objCount != null && objCount != DBNull.Value && Convert.ToInt32(objCount) == 0)// 如果=就是全退货，审批通过 
                            {
                                //生成YY
                                toYYList.Add(supplierAdvancePayments.OrderCheckIds);
                                invoiceModel.IsHeBing = 0;
                            }
                            //如果累计采购退货数量（含本条退货）<入库数量时
                            //此条由采购退货产生的 系统生成的 负数的支付单的合并=1.
                            else
                            {
                                //YY=负数支付单的总金额=入库数量×实采单价-之前所有支付金额+之前所有负数支付单金额（含本条采退），这里的负数单的数量=1,单价=YY
                                invoiceModel.SupplierInvoicePrice = resultTotal;
                                invoiceModel.lastGoodNum = 1;
                                invoiceModel.SupplierInvoiceTotal = resultTotal;
                                invoiceModel.FuShuTotal = resultTotal;
                                invoiceModel.ActPay = resultTotal;

                                invoiceModel.IsHeBing = 1;
                            }
                            
                        }
                        //1.全退货，就是 之前所有支付单金额=0，所有负数支付单金额=入库数量*入库单价 
                        else if (resultTotal == 0 && hadTotal == 0)
                        {
                            //然后 在对同一入库ID 的负数支付单的状态 变成 已结清，已支付，不合并。
                            toAllTuiList.Add(supplierAdvancePayments.OrderCheckIds);
                            invoiceModel.IsHeBing = 0;
                        }
                        //（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额=0 算 部分退货，但之前所有支付单金额>0 ,
                        //（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额=0 ,这种情况 审批通过此条负数单的合并=1 
                        else if (resultTotal == 0 && hadTotal>0)
                        {
                            //YY=负数支付单的总金额=入库数量×实采单价-之前所有支付金额+之前所有负数支付单金额（含本条采退），这里的负数单的数量=1,单价=YY
                            invoiceModel.SupplierInvoicePrice = resultTotal;
                            invoiceModel.lastGoodNum = 1;
                            invoiceModel.SupplierInvoiceTotal = resultTotal;
                            invoiceModel.FuShuTotal = resultTotal;
                            invoiceModel.ActPay = resultTotal;

                            invoiceModel.IsHeBing = 1;
                        }                       
                        else
                        {
                            invoiceModel.IsHeBing = 0;
                        }
                        orders.Add(invoiceModel);
                    }
                    conn.Close();
                }
                int mainId = 0;
                addTran(model, eform, orders, out mainId);

                //先处理1.全退货，就是 之前所有支付单金额=0，所有负数支付单金额=入库数量*入库单价 
                //然后 在对同一入库ID 的负数支付单的状态 变成 已结清，已支付，不合并。
                string allTuiIds = "";
                foreach (var ids in toAllTuiList)
                {
                    allTuiIds +=ids+ ",";                   
                }
                if (allTuiIds != "")
                {
                    DBHelp.ExeCommand(string.Format("update TB_SupplierInvoices set  IsPayStatus=2,RePayClear=1,IsHeBing=0 where ruids in ({0}) and SupplierInvoiceTotal<0;",
                        allTuiIds.Trim(',')));
                }            


                //先算出 YY=同一个入库ID正数金额汇总+ 同一入库ID 负数单 （并且已经抵消）的金额 汇总
                //再将 同一入库ID 的负数支付单的状态 变成 已结清，已支付，不合并。
                //然后 把所有基于此入库ID 的正数的支付单 的金额 加上 审批中 或审批通过 的预付款转支付单 的金额 相加=YY ，
                //系统生成 一张负数的 支付单，金额=负数的YY，支付状态=未支付，结清标记=未结清。
                //按YY 的合并=1，
                 if(toYYList.Count>0)
                 {

                     using (SqlConnection conn = DBHelp.getConn())
                     {
                         conn.Open();
                         foreach (var ids in toYYList)
                         {
                             SqlCommand objCommand = conn.CreateCommand();


                             //先算出 YY=同一个入库ID正数金额汇总+ 同一入库ID 负数单 （并且已经抵消）的金额 汇总
                             var selectInfo = string.Format(@"select avg(SupplierInvoiceNum) as avgSupplierInvoiceNum, sum(SupplierInvoiceTotal) as sumSupplierInvoiceTotal from TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where RuIds={0} and SupplierInvoiceTotal>0 and ((status='通过' and IsYuFu=0) or (Status<>'不通过' and IsYuFu=1)) ", ids);


                             objCommand.CommandText = selectInfo;
                             decimal avgSupplierInvoiceNum = 0;
                             decimal sumSupplierInvoiceTotal = 0;
                             using (var reader = objCommand.ExecuteReader())
                             {
                                 if (reader.Read())
                                 {
                                     object ojb;
                                     ojb = reader["avgSupplierInvoiceNum"];
                                     if (ojb != null && ojb != DBNull.Value)
                                     {
                                         avgSupplierInvoiceNum = Convert.ToDecimal(ojb);
                                     }
                                     ojb = reader["sumSupplierInvoiceTotal"];
                                     if (ojb != null && ojb != DBNull.Value)
                                     {
                                         sumSupplierInvoiceTotal = Convert.ToDecimal(ojb);
                                     }
                                 }
                                 reader.Close();
                             }

                             objCommand.CommandText = string.Format(@"select  sum(SupplierInvoiceTotal) from TB_TempSupplierInvoice left join TB_SupplierInvoice 
on  TB_TempSupplierInvoice.SupplierInvoiceId=TB_SupplierInvoice.id 
left join TB_SupplierInvoices on  TB_TempSupplierInvoice.SupplierInvoiceIds=TB_SupplierInvoices.ids 
where status='通过' and ruids={0};",ids);
                               var objTotal = objCommand.ExecuteScalar();
                               if (objTotal != null && objTotal != DBNull.Value)
                               {
                                   sumSupplierInvoiceTotal = sumSupplierInvoiceTotal + Convert.ToDecimal(objTotal);
                               }

                             //同一入库ID 的负数支付单的状态 变成 已结清，已支付，不合并。
                             var updateSql = string.Format("update TB_SupplierInvoices set  IsPayStatus=2 ,RePayClear=1,IsHeBing=0 where ruids={0} and SupplierInvoiceTotal<0;", ids);
                             objCommand.CommandText = updateSql;
                             objCommand.ExecuteNonQuery();

                             if (sumSupplierInvoiceTotal > 0)
                             {
                                 model = new TB_SupplierInvoice();
                                 model.Status = "通过";
                                 model.CreteTime = DateTime.Now;
                                 model.CreateName = CreateName;
                                 model.CaiTuiProNo = caiTuiProNo;
                                 model.LastSupplier = supplierName;
                                 model.Remark = "全部退货";
                                 eform = new tb_EForm();
                                 eform.appPer = createId;
                                 eform.appTime = DateTime.Now;
                                 eform.createPer = createId;
                                 eform.createTime = DateTime.Now;
                                 eform.proId = 31;
                                 eform.state = "通过";
                                 eform.toPer = 0;
                                 eform.toProsId = 0;
                                 orders = new List<SupplierToInvoiceView>();
                                 var invoiceModel = new SupplierToInvoiceView();
                                 invoiceModel.Ids =ids;

                                 //invoiceModel.IsYuFu = true;
                                 invoiceModel.SupplierFPNo = "";
                                 invoiceModel.SupplierInvoiceDate = DateTime.Now;
                                 var AvgSupplierInvoicePrice = -(sumSupplierInvoiceTotal / avgSupplierInvoiceNum);
                                 invoiceModel.SupplierInvoicePrice = AvgSupplierInvoicePrice;
                                 invoiceModel.lastGoodNum = avgSupplierInvoiceNum;
                                 invoiceModel.SupplierInvoiceTotal = -sumSupplierInvoiceTotal;

                                 invoiceModel.FuShuTotal = invoiceModel.SupplierInvoiceTotal;
                                 invoiceModel.ActPay = invoiceModel.SupplierInvoiceTotal;

                                 invoiceModel.GuestName = supplierName;
                                 //invoiceModel.CaiTuiProNo = caiTuiProNo;
                                 invoiceModel.RePayClear = 2;
                                 invoiceModel.IsPayStatus = 0;//未支付
                                 invoiceModel.IsHeBing = 1;
                                 orders.Add(invoiceModel);
                                 addTran(model, eform, orders, out mainId);
                             }
                         }
                         conn.Close();
                     }
                        
                      
                }
                               
            }
            return true;
        }


        public bool AddSupplierInvoice_01(List<CAI_OrderOutHouses> allList, string CreateName,
        int createId, string supplierName, string caiTuiProNo)
        {
            if (allList.Count > 0)
            {
                TB_SupplierInvoice model = new TB_SupplierInvoice();
                model.Status = "通过";
                model.CreteTime = DateTime.Now;
                model.CreateName = CreateName;
                model.CaiTuiProNo = caiTuiProNo;
                model.LastSupplier = supplierName;

                tb_EForm eform = new tb_EForm();
                eform.appPer = createId;
                eform.appTime = DateTime.Now;
                eform.createPer = createId;
                eform.createTime = DateTime.Now;
                eform.proId = 31;
                eform.state = "通过";
                eform.toPer = 0;
                eform.toProsId = 0;

                List<int> toYYList = new List<int>();

                List<int> toAllTuiList = new List<int>();
                List<SupplierToInvoiceView> orders = new List<SupplierToInvoiceView>();
                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlCommand objCommand = conn.CreateCommand();
                    foreach (var supplierAdvancePayments in allList)
                    {
                        var invoiceModel = new SupplierToInvoiceView();
                        invoiceModel.Ids = supplierAdvancePayments.OrderCheckIds;

                        //invoiceModel.IsYuFu = true;
                        invoiceModel.SupplierFPNo = "";
                        invoiceModel.SupplierInvoiceDate = DateTime.Now;
                        //var AvgSupplierInvoicePrice = -supplierAdvancePayments.GoodPrice;
                        var AvgSupplierInvoicePrice = -supplierAdvancePayments.CaiLastTruePrice;

                        invoiceModel.SupplierInvoicePrice = AvgSupplierInvoicePrice;
                        invoiceModel.lastGoodNum = supplierAdvancePayments.GoodNum;
                        invoiceModel.SupplierInvoiceTotal = AvgSupplierInvoicePrice * supplierAdvancePayments.GoodNum;
                        invoiceModel.GuestName = supplierName;
                        invoiceModel.CaiTuiProNo = caiTuiProNo;
                        invoiceModel.FuShuTotal = invoiceModel.SupplierInvoiceTotal;
                        invoiceModel.ActPay = invoiceModel.SupplierInvoiceTotal;
                        invoiceModel.RePayClear = 2;
                        invoiceModel.IsPayStatus = 0;//未支付

                        //下面‘之前所有负数支付单金额’中的负数单是指 合并=0 并且是 未结清 未支付  的负数单 

                        //必须判断： （入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额<=0
                        //CaiLastTruePrice=GoodPrice
                        var sql = string.Format(@"select CAI_OrderInHouses.ids,GoodNum,CaiLastTruePrice as GoodPrice,isnull(SupplierInvoiceTotal,0) as allSupplierInvoiceTotal,
isnull(FuShuSupplierInvoiceTotal,0) as FuShuSupplierInvoiceTotal
from CAI_OrderInHouses 
left join CAI_OrderInHouse on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id
left join 
(
select RuIds,Sum(SupplierInvoiceTotal) as  SupplierInvoiceTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where status<>'不通过' and SupplierInvoiceTotal>0 and RuIds ={0} 
group by RuIds
)
as tb1 on CAI_OrderInHouses.IDs=tb1.RuIds

left join 
(
select RuIds,Sum(SupplierInvoiceTotal) as  FuShuSupplierInvoiceTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where status='通过' and SupplierInvoiceTotal<0 and IsHeBing=0 and RuIds ={0}
group by RuIds
)
as tb2 on CAI_OrderInHouses.IDs=tb2.RuIds

where CAI_OrderInHouse.status='通过' and CAI_OrderInHouses.ids = {0}", supplierAdvancePayments.OrderCheckIds);

                        objCommand.CommandText = sql;

                        decimal resultTotal = 0;

                        decimal hadTotal = 0;
                        using (SqlDataReader dataReader = objCommand.ExecuteReader())
                        {
                            if (dataReader.Read())
                            {
                                hadTotal = Convert.ToDecimal(dataReader["allSupplierInvoiceTotal"]);
                                resultTotal = Convert.ToDecimal(dataReader["GoodPrice"]) * Convert.ToDecimal(dataReader["GoodNum"]) - Convert.ToDecimal(dataReader["allSupplierInvoiceTotal"]) +
                                   Convert.ToDecimal(dataReader["FuShuSupplierInvoiceTotal"]) + invoiceModel.SupplierInvoiceTotal;
                            }
                        }
                        //必须判断:（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额<0

                        if (resultTotal < 0)
                        {
                            var checkAllTui = string.Format(@"select CAI_OrderInHouses.GoodNum- isnull(newtable.totalOrderNum,0) from CAI_OrderInHouses 
left join CAI_OrderInHouse on CAI_OrderInHouses.id=CAI_OrderInHouse.id
left join 
(
select  OrderCheckIds,SUM(GoodNum) as totalOrderNum from CAI_OrderOutHouses left join 
CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.id
where OrderCheckIds<>0  and status='通过'
group by OrderCheckIds
)
as newtable on CAI_OrderInHouses.Ids=newtable.OrderCheckIds 
where  status='通过' and CAI_OrderInHouses.ids={0};", supplierAdvancePayments.OrderCheckIds);
                            objCommand.CommandText = checkAllTui;

                            var objCount = objCommand.ExecuteScalar();
                            if (objCount != null && objCount != DBNull.Value && Convert.ToInt32(objCount) == 0)// 如果=就是全退货，审批通过 
                            {
                                //生成YY
                                toYYList.Add(supplierAdvancePayments.OrderCheckIds);
                                invoiceModel.IsHeBing = 0;
                            }
                            //如果累计采购退货数量（含本条退货）<入库数量时
                            //此条由采购退货产生的 系统生成的 负数的支付单的合并=1.
                            else
                            {
                                //YY=负数支付单的总金额=入库数量×实采单价-之前所有支付金额+之前所有负数支付单金额（含本条采退），这里的负数单的数量=1,单价=YY
                                invoiceModel.SupplierInvoicePrice = resultTotal;
                                invoiceModel.lastGoodNum = 1;
                                invoiceModel.SupplierInvoiceTotal = resultTotal;
                                invoiceModel.FuShuTotal = resultTotal;
                                invoiceModel.ActPay = resultTotal;

                                invoiceModel.IsHeBing = 1;
                            }

                        }
                        //1.全退货，就是 之前所有支付单金额=0，所有负数支付单金额=入库数量*入库单价 
                        else if (resultTotal == 0 && hadTotal == 0)
                        {
                            //然后 在对同一入库ID 的负数支付单的状态 变成 已结清，已支付，不合并。
                            toAllTuiList.Add(supplierAdvancePayments.OrderCheckIds);
                            invoiceModel.IsHeBing = 0;
                        }
                        //（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额=0 算 部分退货，但之前所有支付单金额>0 ,
                        //（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额=0 ,这种情况 审批通过此条负数单的合并=1 
                        else if (resultTotal == 0 && hadTotal > 0)
                        {
                            //YY=负数支付单的总金额=入库数量×实采单价-之前所有支付金额+之前所有负数支付单金额（含本条采退），这里的负数单的数量=1,单价=YY
                            invoiceModel.SupplierInvoicePrice = resultTotal;
                            invoiceModel.lastGoodNum = 1;
                            invoiceModel.SupplierInvoiceTotal = resultTotal;
                            invoiceModel.FuShuTotal = resultTotal;
                            invoiceModel.ActPay = resultTotal;

                            invoiceModel.IsHeBing = 1;
                        }
                        else
                        {
                            invoiceModel.IsHeBing = 0;
                        }
                        orders.Add(invoiceModel);
                    }
                    conn.Close();
                }
                int mainId = 0;
                addTran(model, eform, orders, out mainId);

                //先处理1.全退货，就是 之前所有支付单金额=0，所有负数支付单金额=入库数量*入库单价 
                //然后 在对同一入库ID 的负数支付单的状态 变成 已结清，已支付，不合并。
                string allTuiIds = "";
                foreach (var ids in toAllTuiList)
                {
                    allTuiIds += ids + ",";
                }
                if (allTuiIds != "")
                {
                    DBHelp.ExeCommand(string.Format("update TB_SupplierInvoices set  IsPayStatus=2,RePayClear=1,IsHeBing=0 where ruids in ({0}) and SupplierInvoiceTotal<0;",
                        allTuiIds.Trim(',')));
                }


                //先算出 YY=同一个入库ID正数金额汇总+ 同一入库ID 负数单 （并且已经抵消）的金额 汇总
                //再将 同一入库ID 的负数支付单的状态 变成 已结清，已支付，不合并。
                //然后 把所有基于此入库ID 的正数的支付单 的金额 加上 审批中 或审批通过 的预付款转支付单 的金额 相加=YY ，
                //系统生成 一张负数的 支付单，金额=负数的YY，支付状态=未支付，结清标记=未结清。
                //按YY 的合并=1，
              

            }
            return true;
        }


        public bool OrderOutHouse_ToInvoice(List<CAI_OrderOutHouses> allList, string CreateName, int createId, string supplierName, string caiTuiProNo)
        {

            return true;
        }
        public string GetAllE_No(string tableName, SqlCommand objCommand)
        {
            string MaxCardNo = "";
            string sql = string.Format("select  right('0000000000'+(convert(varchar,(convert(int,right(max(SupplierProNo),4))+1))),4) FROM  {0} where SupplierProNo like '{1}%';",
                tableName, DateTime.Now.Year);
            objCommand.CommandText = sql.ToString();
            object objMax = objCommand.ExecuteScalar();
            if (objMax != null && objMax.ToString() != "")
            {
                MaxCardNo = DateTime.Now.Year.ToString() + objMax.ToString();
            }
            else
            {
                MaxCardNo = DateTime.Now.Year.ToString() + "0001";
            }

            return MaxCardNo;
        }
        public bool updateTran(VAN_OA.Model.JXC.TB_SupplierInvoice model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms, List<SupplierToInvoiceView> orders, string IDS)
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

                    TB_SupplierInvoicesService OrdersSer = new TB_SupplierInvoicesService();
                    for (int i = 0; i < orders.Count; i++)
                    {
                        if(orders[i].IfCheck==false)
                        {
                            continue;
                        }
                        var modelSupplierInvoices = new TB_SupplierInvoices();
                        modelSupplierInvoices.Ids = orders[i].payIds;
                        modelSupplierInvoices.Id = model.Id;
                        modelSupplierInvoices.RuIds = orders[i].Ids;
                        modelSupplierInvoices.SupplierFPNo = orders[i].SupplierFPNo;
                        modelSupplierInvoices.SupplierInvoiceDate = orders[i].SupplierInvoiceDate.Value;
                        modelSupplierInvoices.SupplierInvoicePrice = orders[i].SupplierInvoicePrice;
                        modelSupplierInvoices.SupplierInvoiceNum = orders[i].lastGoodNum;
                        modelSupplierInvoices.SupplierInvoiceTotal = orders[i].SupplierInvoiceTotal;
                        modelSupplierInvoices.SupplierFpDate = orders[i].SupplierFpDate;

                        ////实际支付=付款金额+（负数合计）
                        //modelSupplierInvoices.ActPay = orders[i].FuShuTotal + orders[i].SupplierInvoiceTotal;
                        ////负数合计
                        //modelSupplierInvoices.FuShuTotal = orders[i].FuShuTotal;


                        if (eform.state == "通过" && (orders[i].SupplierProNo == null||orders[i].SupplierProNo == ""))
                        {
                            modelSupplierInvoices.SupplierProNo = GetAllE_No("TB_SupplierInvoices", objCommand);
                        }   
                        OrdersSer.Update(modelSupplierInvoices, objCommand);
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
        public int addTran(VAN_OA.Model.JXC.TB_SupplierInvoice model, VAN_OA.Model.EFrom.tb_EForm eform, List<SupplierToInvoiceView> orders, out int MainId,
            tb_EForms forms=null)
        {
            int id = 0;
            MainId = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                TB_SupplierInvoicesService OrdersSer = new TB_SupplierInvoicesService();  
                try
                {
                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No1("TB_SupplierInvoice", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;                     
                    model.Status = eform.state;
                    if (orders.Count>0)
                    {
                        model.LastSupplier = orders[0].GuestName;
                        model.CaiTuiProNo = orders[0].CaiTuiProNo;
                    }
                    id = Add(model, objCommand);
                    MainId = id;
                    eform.allE_id = id;
                    int e_Id= eformSer.Add(eform, objCommand);
                    if (forms != null)
                    {
                        forms.e_Id = e_Id;
                        tb_EFormsService eformsSer = new tb_EFormsService();
                        eformsSer.Add(forms, objCommand);
                    }
                    for (int i = 0; i < orders.Count; i++)
                    {
                        if(orders[i].IfCheck==false)
                        {
                            continue;
                        }
                        var modelSupplierInvoices = new TB_SupplierInvoices();
                        modelSupplierInvoices.Id = id;
                        modelSupplierInvoices.IsYuFu = orders[i].IsYuFu;
                        modelSupplierInvoices.RuIds = orders[i].Ids;
                        modelSupplierInvoices.SupplierFPNo = orders[i].SupplierFPNo;
                        modelSupplierInvoices.SupplierInvoiceDate = orders[i].SupplierInvoiceDate.Value;
                        modelSupplierInvoices.SupplierInvoicePrice = orders[i].SupplierInvoicePrice;
                        modelSupplierInvoices.SupplierInvoiceNum = orders[i].lastGoodNum;                        
                        modelSupplierInvoices.SupplierInvoiceTotal = orders[i].SupplierInvoiceTotal;

                        //实际支付=付款金额
                        
                        modelSupplierInvoices.ActPay = orders[i].ActPay;
                       

                       
                        //负数合计
                        modelSupplierInvoices.FuShuTotal = orders[i].FuShuTotal;
                        //modelSupplierInvoices.RePayClear = 0;
                        if (eform.state == "通过" && (orders[i].SupplierProNo == null || orders[i].SupplierProNo == ""))
                        {
                            modelSupplierInvoices.SupplierProNo = GetAllE_No("TB_SupplierInvoices", objCommand);
                        }
                        modelSupplierInvoices.RePayClear = orders[i].RePayClear;
                        modelSupplierInvoices.IsPayStatus = orders[i].IsPayStatus;
                        modelSupplierInvoices.IsHeBing = orders[i].IsHeBing;
                        modelSupplierInvoices.SupplierFpDate = orders[i].SupplierFpDate;
                        orders[i].payIds= OrdersSer.Add(modelSupplierInvoices, objCommand); 
                    }

                    var  otherOrders = orders.FindAll(t => t.IfCheck==false);
                    foreach (var otherOrder in otherOrders)
                    {
                        string insertSql = string.Format("insert into TB_TempSupplierInvoice values({0},{1},0)", id,otherOrder.payIds);
                        objCommand.CommandText = insertSql;
                        objCommand.ExecuteNonQuery();
                    }
                    tan.Commit();
                  
                }
                catch (Exception ex)
                {
                    tan.Rollback();
                    Global.log.Info("addTran系统异常:", ex);
                    throw new Exception("支付单保存到数据库失败，请联系管理员进行补单！");
                    //return 0;

                }
                return id;
            }
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.TB_SupplierInvoice model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.ProNo != null)
            {
                strSql1.Append("ProNo,");
                strSql2.Append("'" + model.ProNo + "',");
            }
            if (model.CreateName != null)
            {
                strSql1.Append("CreateName,");
                strSql2.Append("'" + model.CreateName + "',");
            }
            if (model.CreteTime != null)
            {
                strSql1.Append("CreteTime,");
                strSql2.Append("'" + model.CreteTime + "',");
            }
            if (model.Status != null)
            {
                strSql1.Append("Status,");
                strSql2.Append("'" + model.Status + "',");
            }
            if (model.Remark != null)
            {
                strSql1.Append("Remark,");
                strSql2.Append("'" + model.Remark + "',");
            }
            if (model.LastSupplier != null)
            {
                strSql1.Append("LastSupplier,");
                strSql2.Append("'" + model.LastSupplier + "',");
            }
            if (model.CaiTuiProNo != null)
            {
                strSql1.Append("CaiTuiProNo,");
                strSql2.Append("'" + model.CaiTuiProNo + "',");
            }
            if (model.SumActPay != null)
            {
                strSql1.Append("SumActPay,");
                strSql2.Append("" + model.SumActPay + ",");
            }
            strSql1.Append("FristFPNo,");
            strSql2.Append("'" + model.FristFPNo + "',");

            strSql1.Append("SecondFPNo,");
            strSql2.Append("'" + model.SecondFPNo + "',");

            strSql.Append("insert into TB_SupplierInvoice(");
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
        public void Update(VAN_OA.Model.JXC.TB_SupplierInvoice model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_SupplierInvoice set ");
            if (model.ProNo != null)
            {
                strSql.Append("ProNo='" + model.ProNo + "',");
            }
            if (model.CreateName != null)
            {
                strSql.Append("CreateName='" + model.CreateName + "',");
            }
            if (model.CreteTime != null)
            {
                strSql.Append("CreteTime='" + model.CreteTime + "',");
            }
            if (model.Status != null)
            {
                strSql.Append("Status='" + model.Status + "',");
            }
            if (model.SumActPay != null)
            {
                strSql.Append("SumActPay=" + model.SumActPay + ",");
            }
            strSql.Append("FristFPNo='" + model.FristFPNo + "',");
            strSql.Append("SecondFPNo='" + model.SecondFPNo + "',");
            strSql.Append("LastFPNo='" + (model.SecondFPNo!=""? model.SecondFPNo: model.FristFPNo) + "',");
             
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
            strSql.Append("delete from TB_SupplierInvoice ");
            strSql.Append(" where Id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.TB_SupplierInvoice GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Id,ProNo,CreateName,CreteTime,Status,Remark,FristFPNo,SecondFPNo ");
            strSql.Append(" from TB_SupplierInvoice ");
            strSql.Append(" where Id=" + id + "");
            VAN_OA.Model.JXC.TB_SupplierInvoice model = null;
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
        public List<VAN_OA.Model.JXC.TB_SupplierInvoice> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProNo,CreateName,CreteTime,Status,Remark,FristFPNo,SecondFPNo ");
            strSql.Append(" FROM TB_SupplierInvoice ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.TB_SupplierInvoice> list = new List<VAN_OA.Model.JXC.TB_SupplierInvoice>();

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
        public VAN_OA.Model.JXC.TB_SupplierInvoice ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.TB_SupplierInvoice model = new VAN_OA.Model.JXC.TB_SupplierInvoice();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.ProNo = dataReader["ProNo"].ToString();
            model.CreateName = dataReader["CreateName"].ToString();
            model.CreteTime =Convert.ToDateTime(dataReader["CreteTime"]);
            model.Status = dataReader["Status"].ToString();
            model.Remark = dataReader["Remark"].ToString();
            model.FristFPNo = dataReader["FristFPNo"].ToString();
            model.SecondFPNo = dataReader["SecondFPNo"].ToString();
            return model;
        }

         //审批完成后对 结清状态 和 支付状态进行更新
        public void SetRuKuPayStatus_YuFu(List<SupplierToInvoiceView> POOrders)
        {
            var getAllRuIds = POOrders.FindAll(t => t.IfCheck).Aggregate("", (current, m) => current + (m.Ids + ",")).Trim(',');
            if (getAllRuIds != "")
            {
                //CaiLastTruePrice=GoodPrice
                var getDT =
                    string.Format(@"select CAI_OrderInHouses.ids,GoodNum*CaiLastTruePrice as goodTotal 
from CAI_OrderInHouses 
left join CAI_OrderInHouse on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id 
where CAI_OrderInHouse.status='通过' and CAI_OrderInHouses.ids in ({0}) ", getAllRuIds);
                var dt = DBHelp.getDataTable(getDT);
                if (dt.Rows.Count <= 0) return;

                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlTransaction tan = conn.BeginTransaction();
                    SqlCommand objCommand = conn.CreateCommand();
                    objCommand.Transaction = tan;

                    try
                    {

                        //更新结算状态
                        foreach (DataRow dr in dt.Rows)
                        {
                            string exeSql = "";
                            int ids = Convert.ToInt32(dr["ids"]);
                            exeSql = string.Format(@"select sum(SupplierInvoiceTotal) as allSupplierInvoiceTotal from TB_SupplierAdvancePayments
left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayments.id=TB_SupplierAdvancePayment.id where CaiIds in (
select CaiId from CAI_OrderChecks 
where ids in (select OrderCheckIds from CAI_OrderInHouses where ids={0})
) and Status='通过'", ids);
                            decimal resultTotal = 0;
                            decimal goodTotal = Convert.ToDecimal(dr["goodTotal"]);
                           

                            objCommand.CommandText = exeSql;                           
                            var allSupplierInvoiceTotal = objCommand.ExecuteScalar();
                            if (!(allSupplierInvoiceTotal is DBNull))
                            {
                                resultTotal = goodTotal-Convert.ToDecimal(allSupplierInvoiceTotal);
                            }
                            else
                            {
                                resultTotal = goodTotal;
                            }         
                            int payIds = 0;
                            var findList = POOrders.Where(t => t.IfCheck == true && t.Ids == ids).ToList();
                            if (findList.Count() > 0)
                            {
                                payIds = findList[0].payIds;
                            }
                            exeSql = "";
                            //同一入库ID的商品的 （入库数量）*入库单价-之前所有支付单金额+所有负数支付单金额>0  
                            //就是还有未支付的金额，审批通过，此支付单的 支付状态 就是未付清，结清状态=1，即此支付单的 支付状态 
                            //变成 未付清，结清状态 变成 1。所有基于此ID 的负数支付单的支付状态变成已支付，结清状态 变成 1。
                            if (resultTotal > 0)
                            {
                                exeSql =
                                    string.Format(
                                        "update TB_SupplierInvoices set RePayClear=1,IsPayStatus=1 where ids={0};",
                                        payIds);                                
                            }
                            //就是没有未支付的金额，审批通过，此支付单的 支付状态 就是已支付。结清状态=1，
                            //即此支付单的 支付状态 变成已支付，结清状态 变成 1，所有基于此ID 的负数的支付单的
                            //支付状态 都变成 已支付，结清状态 变成 1。
                            else if (resultTotal == 0)
                            {
                                exeSql =
                                    string.Format(
                                        "update TB_SupplierInvoices set RePayClear=1,IsPayStatus=2 where ids={0};",
                                        payIds);                              
                            }
                            //else
                            //{
                            //    //事后退货的支付单，审批通过 实际支付为负数 的 结清=2  就是6.B
                            //    exeSql = string.Format("update TB_SupplierInvoices set RePayClear=2 where ids={0}", payIds);
                            //}
                            if (exeSql != "")
                            {
                                objCommand.CommandText = exeSql;
                                objCommand.ExecuteNonQuery();
                            }

                        }
                        tan.Commit();

                    }
                    catch (Exception)
                    {
                        tan.Rollback();
                    }

                    conn.Close();
                }
            }

        }
        //审批完成后对 结清状态 和 支付状态进行更新
        public void SetRuKuPayStatus(List<SupplierToInvoiceView> POOrders)
        {
            //           同一入库ID的商品的 （入库数量）*入库单价-之前所有支付单金额+所有负数支付单金额-本次支付单金额>0，就是还有未支付的金额，审批通过，此支付单的 支付状态 就是未付清，结清状态=1，即此支付单的 支付状态 变成 未付清，结清状态 变成 1。所有基于此ID 的负数支付单的支付状态变成已支付，结清状态 变成 1。
            // 同一入库ID的商品的 （入库数量）*入库单价-之前所有支付单金额+所有负数支付单金额-本次支付单金额=0，就是没有未支付的金额，审批通过，此支付单的 支付状态 就是已支付。结清状态=1，即此支付单的 支付状态 变成已支付，结清状态 变成 1，所有基于此ID 的负数的支付单的 支付状态 都变成 已支付，结清状态 变成 1。
            //当发生同一入库ID的商品的 （入库数量）*入库单价-之前所有支付单金额+所有负数支付单金额-本次支付单金额<0，就是本次支付超过了应付金额，这一步审批就不能通过，系统提示此支付单金额超过应付金额XXX了。并退出返回，此正数支付单就不能生成了。 XXX 就是 系统生成的应付金额=（入库数量）*入库单价-之前所有支付单金额+所有负数支付单金额，如果XXX是负数，就说明 采购退货产生的 负数值的记录一直未被抵消。


            var getAllRuIds = POOrders.FindAll(t=>t.IfCheck).Aggregate("", (current, m) => current + (m.Ids + ",")).Trim(',');
            var shthCount= POOrders.Where(t => t.IfCheck == false).Count();
            if (shthCount > 0)
            {
                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlTransaction tan = conn.BeginTransaction();
                    SqlCommand objCommand = conn.CreateCommand();
                    objCommand.Transaction = tan;

                    try
                    {

                        //就计算 实际支付 SUM
                        var sumActPay = POOrders.Sum(t => t.ActPay);

                        var zhengShuIds = POOrders.FindAll(t => t.IfCheck).Aggregate("", (current, m) => current + (m.payIds + ",")).Trim(',');
                        var fushuIds = POOrders.Aggregate("", (current, m) => current + (m.Ids + ",")).Trim(',');
                        string exeSql = "";
                        //   下面会有实际支付的合计，实际支付 合计>0 ,提交成功，最后审批结束 后，
                        //此正数支付单记录的支付状态 变成 未结清，结清状态=1.所有界面上的负数支付单 的支付状态 变成已支付，结清状态=1
                        if (sumActPay>0)
                        {
                            if (zhengShuIds!="")
                            exeSql = string.Format("update TB_SupplierInvoices set  RePayClear=1,IsPayStatus=1 where ids in ({0});", zhengShuIds);
                            if (fushuIds != "")
                                exeSql += string.Format("update TB_SupplierInvoices set  RePayClear=1,IsPayStatus=2,IsHeBing=0 where ruids in ({0}) and SupplierInvoiceTotal<0;", fushuIds);
                        }
                        //实际支付 合计=0 ,提交成功，最后审批结束 后，此正数支付单记录的支付状态 变成 已支付，结清状态=1.
                        //所有界面上的负数支付单 的支付状态 变成已支付，结清状态=1
                        else if(sumActPay==0)
                        {
                            if (zhengShuIds != "")
                            exeSql = string.Format("update TB_SupplierInvoices set  RePayClear=1,IsPayStatus=2 where ids in ({0});", zhengShuIds);
                            if (fushuIds != "")
                                exeSql += string.Format("update TB_SupplierInvoices set  RePayClear=1,IsPayStatus=2,IsHeBing=0 where ruids in ({0}) and SupplierInvoiceTotal<0;", fushuIds);
                        }

                        //实际支付 合计<0 ,提交不成功，说明还是不能支付，屏幕提示此支付单金额超过应付金额YYY了，并退出返回，
                        //此正数支付单就不能生成了。YYY 就是 
                        //系统生成的应付金额=（入库数量）*入库单价-之前所有支付单金额+所有负数支付单金额+界面上另外ID的几条负数支付单金额，
                        //如果YYY是负数，就说明 采购退货产生的 负数值的记录一直还是不能被抵消。只能等下一次再抵消了。


                        //更新结算状态 事后扣减成功
                        if (exeSql != "")
                        {
                            objCommand.CommandText = exeSql;
                            objCommand.ExecuteNonQuery();
                        }
                        tan.Commit();

                    }
                    catch (Exception)
                    {
                        tan.Rollback();
                    }

                    conn.Close();
                }
            }

            if (shthCount == 0)
            {
                //CaiLastTruePrice=GoodPrice
                var getDT =
                    string.Format(
                        @"select CAI_OrderInHouses.ids,GoodNum,CaiLastTruePrice AS GoodPrice,
isnull(SupplierInvoiceTotal,0) as allSupplierInvoiceTotal
from CAI_OrderInHouses 
left join CAI_OrderInHouse on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id
left join 
(
select RuIds,Sum(ABS(SupplierInvoiceTotal)) as  SupplierInvoiceTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where ((status='通过' and IsYuFu=0 ) or (Status<>'不通过' and IsYuFu=1))  and RuIds in ({0})
group by RuIds
)
as tb1 on CAI_OrderInHouses.IDs=tb1.RuIds
where CAI_OrderInHouse.status='通过' and CAI_OrderInHouses.ids in ({0}) ",
                        getAllRuIds);

                var dt = DBHelp.getDataTable(getDT);
                if (dt.Rows.Count <= 0) return;

                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlTransaction tan = conn.BeginTransaction();
                    SqlCommand objCommand = conn.CreateCommand();
                    objCommand.Transaction = tan;

                    try
                    {

                        //更新结算状态
                        foreach (DataRow dr in dt.Rows)
                        {
                            string exeSql = "";


                            decimal resultTotal = Convert.ToDecimal(dr["GoodPrice"])*Convert.ToDecimal(dr["GoodNum"]) -
                                                  Convert.ToDecimal(dr["allSupplierInvoiceTotal"]);
                            int ids = Convert.ToInt32(dr["ids"]);
                            int payIds = 0;
                            var findList = POOrders.Where(t => t.IfCheck == true && t.Ids == ids).ToList();
                            if (findList.Count() > 0)
                            {
                                payIds = findList[0].payIds;
                            }
                            //同一入库ID的商品的 （入库数量）*入库单价-之前所有支付单金额+所有负数支付单金额>0  
                            //就是还有未支付的金额，审批通过，此支付单的 支付状态 就是未付清，结清状态=1，即此支付单的 支付状态 
                            //变成 未付清，结清状态 变成 1。所有基于此ID 的负数支付单的支付状态变成已支付，结清状态 变成 1。
                            if (resultTotal > 0)
                            {
                                exeSql =
                                    string.Format(
                                        "update TB_SupplierInvoices set RePayClear=1,IsPayStatus=1 where ids={0};",
                                        payIds);
                                exeSql +=
                                    string.Format(
                                        "update  TB_SupplierInvoices set RePayClear=1,IsPayStatus=2 where SupplierInvoiceTotal<0 and RuIds=" +
                                        ids);
                            }
                                //就是没有未支付的金额，审批通过，此支付单的 支付状态 就是已支付。结清状态=1，
                                //即此支付单的 支付状态 变成已支付，结清状态 变成 1，所有基于此ID 的负数的支付单的
                                //支付状态 都变成 已支付，结清状态 变成 1。
                            else if (resultTotal == 0)
                            {
                                exeSql =
                                    string.Format(
                                        "update TB_SupplierInvoices set RePayClear=1,IsPayStatus=2 where ids={0};",
                                        payIds);
                                exeSql +=
                                    string.Format(
                                        "update  TB_SupplierInvoices set RePayClear=1,IsPayStatus=2 where SupplierInvoiceTotal<0 and RuIds=" +
                                        ids);
                            }
                            //else
                            //{
                            //    //事后退货的支付单，审批通过 实际支付为负数 的 结清=2  就是6.B
                            //    exeSql = string.Format("update TB_SupplierInvoices set RePayClear=2 where ids={0}", payIds);
                            //}
                            if (exeSql != "")
                            {
                                objCommand.CommandText = exeSql;
                                objCommand.ExecuteNonQuery();
                            }

                        }
                        tan.Commit();

                    }
                    catch (Exception)
                    {
                        tan.Rollback();
                    }

                    conn.Close();
                }

            }


            ////更新支付状态  只更新  未付清 已支付 这两种状态
            ////1.未付清（此入库ID的商品 有过部分预付 或 部分支付，或2者兼有但没有付完，此入库ID的记录按 公式：（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额>0  ），此支付单在审批通过 支付单状态 变成未付清。
            ////2.已支付 （此入库ID的商品 已全部预付，或支付 完 ，此入库ID的记录 公式：（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额=0 且 基于此入库ID 的之前所有支付单金额合计为正数 ），此条 支付单审批通过后，它的状态 就变成已支付。
            //using (SqlConnection conn = DBHelp.getConn())
            //{
            //    conn.Open();
            //    SqlTransaction tan = conn.BeginTransaction();
            //    SqlCommand objCommand = conn.CreateCommand();
            //    objCommand.Transaction = tan;

            //    try
            //    {
            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            string exeSql = "";
            //            decimal resultTotal = Convert.ToDecimal(dr["GoodPrice"]) * Convert.ToDecimal(dr["GoodNum"]) - Convert.ToDecimal(dr["allSupplierInvoiceTotal"]);
            //            if (resultTotal > 0)// 未付清（有过部分预付 或 部分支付，或2者兼有但没有付完，公式：（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额>0  ）
            //            {
            //                exeSql = " update CAI_OrderInHouses set PayStatus=1 where Ids=" + dr["ids"].ToString();
            //            }
            //            ////基于此入库ID 的之前所有支付单金额合计为正数 
            //            else if (resultTotal == 0 && Convert.ToDecimal(dr["allSupplierInvoiceTotal"])>=0)//已支付 （全部预付，或支付 完 ，公式：（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额=0  ）
            //            {
            //                exeSql = " update CAI_OrderInHouses set PayStatus=2 where Ids=" + dr["ids"].ToString();
            //            }
            //            objCommand.CommandText = exeSql;
            //            objCommand.ExecuteNonQuery();
            //        }
            //        tan.Commit();

            //    }
            //    catch (Exception)
            //    {
            //        tan.Rollback();
            //    }

            //    conn.Close();
            //}
        }

        //审批完成后对 结清状态 和 支付状态进行更新
        public void SetRuKuPayStatus1(List<SupplierToInvoiceView> POOrders)
        {
            //           同一入库ID的商品的 （入库数量）*入库单价-之前所有支付单金额+所有负数支付单金额-本次支付单金额>0，就是还有未支付的金额，审批通过，此支付单的 支付状态 就是未付清，结清状态=1，即此支付单的 支付状态 变成 未付清，结清状态 变成 1。所有基于此ID 的负数支付单的支付状态变成已支付，结清状态 变成 1。
            // 同一入库ID的商品的 （入库数量）*入库单价-之前所有支付单金额+所有负数支付单金额-本次支付单金额=0，就是没有未支付的金额，审批通过，此支付单的 支付状态 就是已支付。结清状态=1，即此支付单的 支付状态 变成已支付，结清状态 变成 1，所有基于此ID 的负数的支付单的 支付状态 都变成 已支付，结清状态 变成 1。
            //当发生同一入库ID的商品的 （入库数量）*入库单价-之前所有支付单金额+所有负数支付单金额-本次支付单金额<0，就是本次支付超过了应付金额，这一步审批就不能通过，系统提示此支付单金额超过应付金额XXX了。并退出返回，此正数支付单就不能生成了。 XXX 就是 系统生成的应付金额=（入库数量）*入库单价-之前所有支付单金额+所有负数支付单金额，如果XXX是负数，就说明 采购退货产生的 负数值的记录一直未被抵消。


            var getAllRuIds = POOrders.FindAll(t => t.IfCheck).Aggregate("", (current, m) => current + (m.Ids + ",")).Trim(',');
            var shthCount = POOrders.Where(t => t.IfCheck == false).Count();
           

            if (shthCount == 0)
            {
                //CaiLastTruePrice=GoodPrice
                var getDT =
                    string.Format(
                        @"select CAI_OrderInHouses.ids,GoodNum,CaiLastTruePrice AS GoodPrice,
isnull(SupplierInvoiceTotal,0) as allSupplierInvoiceTotal
from CAI_OrderInHouses 
left join CAI_OrderInHouse on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id
left join 
(
select RuIds,Sum(ABS(SupplierInvoiceTotal)) as  SupplierInvoiceTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where status='通过' and RuIds in ({0})
group by RuIds
)
as tb1 on CAI_OrderInHouses.IDs=tb1.RuIds
where CAI_OrderInHouse.status='通过' and CAI_OrderInHouses.ids in ({0}) ",
                        getAllRuIds);

                var dt = DBHelp.getDataTable(getDT);
                if (dt.Rows.Count <= 0) return;

                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlTransaction tan = conn.BeginTransaction();
                    SqlCommand objCommand = conn.CreateCommand();
                    objCommand.Transaction = tan;

                    try
                    {

                        //更新结算状态
                        foreach (DataRow dr in dt.Rows)
                        {
                            string exeSql = "";


                            decimal resultTotal = Convert.ToDecimal(dr["GoodPrice"]) * Convert.ToDecimal(dr["GoodNum"]) -
                                                  Convert.ToDecimal(dr["allSupplierInvoiceTotal"]);
                            int ids = Convert.ToInt32(dr["ids"]);
                            int payIds = 0;
                            var findList = POOrders.Where(t => t.IfCheck == true && t.Ids == ids).ToList();
                            if (findList.Count() > 0)
                            {
                                payIds = findList[0].payIds;
                            }
                            //同一入库ID的商品的 （入库数量）*入库单价-之前所有支付单金额+所有负数支付单金额>0  
                            //就是还有未支付的金额，审批通过，此支付单的 支付状态 就是未付清，结清状态=1，即此支付单的 支付状态 
                            //变成 未付清，结清状态 变成 1。所有基于此ID 的负数支付单的支付状态变成已支付，结清状态 变成 1。
                            if (resultTotal > 0)
                            {
                                
                                exeSql +=
                                    string.Format(
                                        "update  TB_SupplierInvoices set RePayClear=1,IsPayStatus=2 where SupplierInvoiceTotal<0 and RuIds=" +
                                        ids);
                            }
                            //就是没有未支付的金额，审批通过，此支付单的 支付状态 就是已支付。结清状态=1，
                            //即此支付单的 支付状态 变成已支付，结清状态 变成 1，所有基于此ID 的负数的支付单的
                            //支付状态 都变成 已支付，结清状态 变成 1。
                            else if (resultTotal == 0)
                            {
                                
                                exeSql +=
                                    string.Format(
                                        "update  TB_SupplierInvoices set RePayClear=1,IsPayStatus=2 where SupplierInvoiceTotal<0 and RuIds=" +
                                        ids);
                            }
                            //else
                            //{
                            //    //事后退货的支付单，审批通过 实际支付为负数 的 结清=2  就是6.B
                            //    exeSql = string.Format("update TB_SupplierInvoices set RePayClear=2 where ids={0}", payIds);
                            //}
                            if (exeSql != "")
                            {
                                objCommand.CommandText = exeSql;
                                objCommand.ExecuteNonQuery();
                            }

                        }
                        tan.Commit();

                    }
                    catch (Exception)
                    {
                        tan.Rollback();
                    }

                    conn.Close();
                }

            }


            ////更新支付状态  只更新  未付清 已支付 这两种状态
            ////1.未付清（此入库ID的商品 有过部分预付 或 部分支付，或2者兼有但没有付完，此入库ID的记录按 公式：（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额>0  ），此支付单在审批通过 支付单状态 变成未付清。
            ////2.已支付 （此入库ID的商品 已全部预付，或支付 完 ，此入库ID的记录 公式：（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额=0 且 基于此入库ID 的之前所有支付单金额合计为正数 ），此条 支付单审批通过后，它的状态 就变成已支付。
            //using (SqlConnection conn = DBHelp.getConn())
            //{
            //    conn.Open();
            //    SqlTransaction tan = conn.BeginTransaction();
            //    SqlCommand objCommand = conn.CreateCommand();
            //    objCommand.Transaction = tan;

            //    try
            //    {
            //        foreach (DataRow dr in dt.Rows)
            //        {
            //            string exeSql = "";
            //            decimal resultTotal = Convert.ToDecimal(dr["GoodPrice"]) * Convert.ToDecimal(dr["GoodNum"]) - Convert.ToDecimal(dr["allSupplierInvoiceTotal"]);
            //            if (resultTotal > 0)// 未付清（有过部分预付 或 部分支付，或2者兼有但没有付完，公式：（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额>0  ）
            //            {
            //                exeSql = " update CAI_OrderInHouses set PayStatus=1 where Ids=" + dr["ids"].ToString();
            //            }
            //            ////基于此入库ID 的之前所有支付单金额合计为正数 
            //            else if (resultTotal == 0 && Convert.ToDecimal(dr["allSupplierInvoiceTotal"])>=0)//已支付 （全部预付，或支付 完 ，公式：（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额=0  ）
            //            {
            //                exeSql = " update CAI_OrderInHouses set PayStatus=2 where Ids=" + dr["ids"].ToString();
            //            }
            //            objCommand.CommandText = exeSql;
            //            objCommand.ExecuteNonQuery();
            //        }
            //        tan.Commit();

            //    }
            //    catch (Exception)
            //    {
            //        tan.Rollback();
            //    }

            //    conn.Close();
            //}
        }


//        //审批提交时 对 未支付 和 勿支付 状态进行更新
//        public void SetRuKuPayStatus_ToAdd(List<SupplierToInvoiceView> POOrders)
//        {
         
//            var getAllRuIds = POOrders.FindAll(t => t.IfCheck).Aggregate("", (current, m) => current + (m.Ids + ",")).Trim(',');

//            var getDT = string.Format(@"select CAI_OrderInHouses.ids,GoodNum,GoodPrice,
//isnull(SupplierInvoiceTotal,0) as allSupplierInvoiceTotal
//from CAI_OrderInHouses 
//left join CAI_OrderInHouse on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id
//left join 
//(
//select RuIds,Sum(ABS(SupplierInvoiceTotal)) as  SupplierInvoiceTotal from 
//TB_SupplierInvoices 
//left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
//where status='通过' and RuIds in ({0})
//group by RuIds
//)
//as tb1 on CAI_OrderInHouses.IDs=tb1.RuIds
//where CAI_OrderInHouse.status='通过' and CAI_OrderInHouses.ids in ({0}) ", getAllRuIds);

//            var dt = DBHelp.getDataTable(getDT);
//            if (dt.Rows.Count <= 0) return;



//            //审批提交时 对 未支付 和 勿支付 状态进行更新
//            //关于未支付，我考虑了一下，还是正数的支付单，在小陆提交时 只要满足 此入库ID的（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额>0 ,就变成未支付。

//            //关于勿支付，我考虑了一下，勿支付 就是不需要支付，界定条件：此入库ID的商品 含有正数的支付单，和负数的支付单 ，此入库ID的记录 公式：（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额<=0，在小陆提交正数支付单时，状态就变成 勿支付。
//            using (SqlConnection conn = DBHelp.getConn())
//            {
//                conn.Open();
//                SqlTransaction tan = conn.BeginTransaction();
//                SqlCommand objCommand = conn.CreateCommand();
//                objCommand.Transaction = tan;

//                try
//                {
//                    foreach (DataRow dr in dt.Rows)
//                    {
//                        string exeSql = "";
//                        decimal resultTotal = Convert.ToDecimal(dr["GoodPrice"]) * Convert.ToDecimal(dr["GoodNum"]) - Convert.ToDecimal(dr["allSupplierInvoiceTotal"]);
//                        if (resultTotal > 0)// 未支付 
//                        {
//                            exeSql = " update CAI_OrderInHouses set PayStatus=0 where Ids=" + dr["ids"].ToString();
//                        }
//                        else if (resultTotal <= 0)//勿支付
//                        {
//                            //此入库ID的商品 含有正数的支付单，和负数的支付单

//                            exeSql = string.Format(@" declare @zhengshu int;
//                            declare @fushu int;
//                            set @zhengshu=0;
//                            set @fushu=0;
//                            select @zhengshu=count(*) from TB_SupplierInvoices left join TB_SupplierInvoice on TB_SupplierInvoice.Id=TB_SupplierInvoices.Id where Status<>'不通过' and SupplierInvoiceTotal<0 and ruIds={0}
//                            select @fushu=count(*) from TB_SupplierInvoices left join TB_SupplierInvoice on TB_SupplierInvoice.Id=TB_SupplierInvoices.Id where Status<>'不通过' and SupplierInvoiceTotal>0 and ruIds={0}
//                            if  (@zhengshu>0 and @fushu>0)
//                            begin
//                            update CAI_OrderInHouses set PayStatus=3 where Ids={0}
//                            end",  dr["ids"]);
//                        }
//                        objCommand.CommandText = exeSql;
//                        objCommand.ExecuteNonQuery();
//                    }
//                    tan.Commit();

//                }
//                catch (Exception)
//                {
//                    tan.Rollback();
//                }

//                conn.Close();
//            }
//        }

    }
}
