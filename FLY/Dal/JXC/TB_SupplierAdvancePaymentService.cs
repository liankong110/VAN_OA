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
    public class TB_SupplierAdvancePaymentService
    {
        public bool updateTran(VAN_OA.Model.JXC.TB_SupplierAdvancePayment model, VAN_OA.Model.EFrom.tb_EForm eform, tb_EForms forms, List<SupplierToInvoiceView> orders, string IDS)
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

                    TB_SupplierAdvancePaymentsService OrdersSer = new TB_SupplierAdvancePaymentsService();
                    for (int i = 0; i < orders.Count; i++)
                    {
                        if (orders[i].IfCheck == false)
                        {
                            continue;
                        }
                        var modelSupplierInvoices = new TB_SupplierAdvancePayments();
                        modelSupplierInvoices.Ids = orders[i].payIds;
                        modelSupplierInvoices.Id = model.Id;
                        modelSupplierInvoices.CaiIds = orders[i].Ids;
                        modelSupplierInvoices.SupplierFPNo = orders[i].SupplierFPNo;
                        modelSupplierInvoices.SupplierInvoiceDate = orders[i].SupplierInvoiceDate.Value;
                        modelSupplierInvoices.SupplierInvoiceNum = orders[i].SupplierInvoiceNum;
                        modelSupplierInvoices.SupplierInvoicePrice = orders[i].SupplierInvoicePrice;
                        modelSupplierInvoices.SupplierInvoiceTotal = orders[i].SupplierInvoiceTotal;
                        if (eform.state == "通过" && (orders[i].SupplierProNo == null || orders[i].SupplierProNo == ""))
                        {
                            modelSupplierInvoices.SupplierProNo = GetAllE_No("TB_SupplierAdvancePayments", objCommand);
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

        public int addTran(VAN_OA.Model.JXC.TB_SupplierAdvancePayment model, VAN_OA.Model.EFrom.tb_EForm eform, List<SupplierToInvoiceView> orders, out int MainId)
        {
            int id = 0;
            MainId = 0;
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                TB_SupplierAdvancePaymentsService OrdersSer = new TB_SupplierAdvancePaymentsService();  
                try
                {
                    objCommand.Parameters.Clear();
                    tb_EFormService eformSer = new tb_EFormService();
                    string proNo = eformSer.GetAllE_No1("TB_SupplierAdvancePayment", objCommand);
                    model.ProNo = proNo;
                    eform.E_No = proNo;                     
                    model.Status = eform.state;
                    id = Add(model, objCommand);
                    MainId = id;
                    eform.allE_id = id;
                    eformSer.Add(eform, objCommand);    
                
                    for (int i = 0; i < orders.Count; i++)
                    {
                        if (orders[i].IfCheck == false)
                        {
                            continue;
                        }
                        var modelSupplierInvoices = new TB_SupplierAdvancePayments();
                        modelSupplierInvoices.Id = id;
                        modelSupplierInvoices.CaiIds = orders[i].Ids;
                        modelSupplierInvoices.SupplierFPNo = orders[i].SupplierFPNo;
                        modelSupplierInvoices.SupplierInvoiceDate = orders[i].SupplierInvoiceDate.Value;
                        modelSupplierInvoices.SupplierInvoiceNum = orders[i].SupplierInvoiceNum;
                        modelSupplierInvoices.SupplierInvoicePrice = orders[i].SupplierInvoicePrice;                        
                        modelSupplierInvoices.SupplierInvoiceTotal = orders[i].SupplierInvoiceTotal;
                        if (eform.state == "通过" && (orders[i].SupplierProNo == null || orders[i].SupplierProNo == ""))
                        {
                            modelSupplierInvoices.SupplierProNo = GetAllE_No("TB_SupplierAdvancePayments", objCommand);
                        }   
                        OrdersSer.Add(modelSupplierInvoices, objCommand); 
                    }
                    var otherOrders = orders.FindAll(t => t.IfCheck == false);
                    foreach (var otherOrder in otherOrders)
                    {
                        string insertSql = string.Format("insert into TB_TempSupplierInvoice values(0,{1},{0})", id, otherOrder.payIds);
                        objCommand.CommandText = insertSql;
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
        public int Add(VAN_OA.Model.JXC.TB_SupplierAdvancePayment model, SqlCommand objCommand)
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
            if (model.SumActPay != null)
            {
                strSql1.Append("SumActPay,");
                strSql2.Append("" + model.SumActPay + ",");
            }
            strSql1.Append("FristFPNo,");
            strSql2.Append("'" + model.FristFPNo + "',");

            strSql1.Append("SecondFPNo,");
            strSql2.Append("'" + model.SecondFPNo + "',");

            strSql.Append("insert into TB_SupplierAdvancePayment(");
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
        public void Update(VAN_OA.Model.JXC.TB_SupplierAdvancePayment model, SqlCommand objCommand)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_SupplierAdvancePayment set ");
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
                strSql.Append("SumActPay='" + model.SumActPay + "',");
            }
            strSql.Append("FristFPNo='" + model.FristFPNo + "',");
            strSql.Append("SecondFPNo='" + model.SecondFPNo + "',");
            strSql.Append("LastFPNo='" + (model.SecondFPNo != "" ? model.SecondFPNo : model.FristFPNo) + "',");
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
            strSql.Append("delete from TB_SupplierAdvancePayment ");
            strSql.Append(" where Id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.TB_SupplierAdvancePayment GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select   ");
            strSql.Append(" Id,ProNo,CreateName,CreteTime,Status,Remark,FristFPNo,SecondFPNo ");
            strSql.Append(" from TB_SupplierAdvancePayment ");
            strSql.Append(" where Id=" + id + "");
            VAN_OA.Model.JXC.TB_SupplierAdvancePayment model = null;
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
        public List<VAN_OA.Model.JXC.TB_SupplierAdvancePayment> GetListArray(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,ProNo,CreateName,CreteTime,Status,Remark ,FristFPNo,SecondFPNo ");
            strSql.Append(" FROM TB_SupplierAdvancePayment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.TB_SupplierAdvancePayment> list = new List<VAN_OA.Model.JXC.TB_SupplierAdvancePayment>();

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
        /// 获取供应商的名称
        /// </summary>
        /// <param name="sPay"></param>
        /// <param name="sInvoice"></param>
        /// <returns></returns>
        public List<SupplierInvoice_Name> GetSupplierName(string sPay, string sInvoice)
        {
            string sql = string.Format(@"select 0 as type,TB_SupplierAdvancePayments.Id,lastSupplier from TB_SupplierAdvancePayments
left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayment.Id=TB_SupplierAdvancePayments.Id
left join CAI_POCai  on  TB_SupplierAdvancePayments.CaiIds=CAI_POCai.ids
where TB_SupplierAdvancePayments.Id in ({0})
group by lastSupplier, TB_SupplierAdvancePayments.Id
union all
select 1 as type,TB_SupplierInvoice.Id,lastSupplier from TB_SupplierInvoice WHERE TB_SupplierInvoice.Id in ({1})", sPay,sInvoice);

            List<VAN_OA.Model.JXC.SupplierInvoice_Name> list = new List<VAN_OA.Model.JXC.SupplierInvoice_Name>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBind_1(dataReader));
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.SupplierInvoice_Name ReaderBind_1(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.SupplierInvoice_Name model = new VAN_OA.Model.JXC.SupplierInvoice_Name();
            object ojb;
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = (int)ojb;
            }
            model.LastSupplier = dataReader["lastSupplier"].ToString();
            model.Type = dataReader["type"].ToString();
           
            return model;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public VAN_OA.Model.JXC.TB_SupplierAdvancePayment ReaderBind(IDataReader dataReader)
        {
            VAN_OA.Model.JXC.TB_SupplierAdvancePayment model = new VAN_OA.Model.JXC.TB_SupplierAdvancePayment();
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

        /// <summary>
        /// 审批通过后，这些被拖出来的负数支付单的合并=0，结清=结清，支付状态=已支付。即可！
        /// </summary>
        /// <param name="POOrders"></param>
        public void SetActStatus(List<SupplierToInvoiceView> POOrders)
        {
            var fushuIds = POOrders.FindAll(t => t.IfCheck == false).Aggregate("", (current, m) => current + (m.payIds + ",")).Trim(',');
            var exeSql = string.Format("update TB_SupplierInvoices set  RePayClear=1,IsPayStatus=2,IsHeBing=0 where ids in ({0}) and SupplierInvoiceTotal<0;", fushuIds);
            DBHelp.ExeCommand(exeSql);
        }

        public void SetCaiPayStatus(List<SupplierToInvoiceView> POOrders)
        {
           
            var getAllRuIds = POOrders.Aggregate("", (current, m) => current + (m.Ids + ",")).Trim(',');
            //LastTruePrice=lastPrice
            var getDT = string.Format(@"select CAI_POCai.ids,Num,LastTruePrice as lastPrice ,isnull(SupplierInvoiceTotal,0) as allSupplierInvoiceTotal
from CAI_POCai
left join CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
left join 
(
select CaiIds,Sum(SupplierInvoiceTotal) as  SupplierInvoiceTotal from 
TB_SupplierAdvancePayments 
left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.id
where status='通过' and CaiIds in ({0})
group by CaiIds
)
as tb1 on CAI_POCai.IDs=tb1.CaiIds
where CAI_POOrder.status='通过' and CAI_POCai.ids in ({0}) ", getAllRuIds);

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
                    foreach (DataRow dr in dt.Rows)
                    {
                        string exeSql = "";
                        decimal resultTotal = Convert.ToDecimal(dr["lastPrice"]) * Convert.ToDecimal(dr["Num"]) - Convert.ToDecimal(dr["allSupplierInvoiceTotal"]);
                        if (Convert.ToDecimal(dr["allSupplierInvoiceTotal"]) == 0)//未支付
                        {
                            exeSql = " update CAI_POCai set PayStatus=0 where Ids=" + dr["ids"].ToString();
                        }
                        else if (resultTotal > 0)// 未付清（有过部分预付 或 部分支付，或2者兼有但没有付完，公式：（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额>0  ）
                        {
                            exeSql = " update CAI_POCai set PayStatus=1 where Ids=" + dr["ids"].ToString();
                        }
                        else if (resultTotal == 0)//已支付 （全部预付，或支付 完 ，公式：（入库数量）*入库单价-之前所有支付单金额+之前所有负数支付单金额-本次支付单金额=0  ）
                        {
                            exeSql = " update CAI_POCai set PayStatus=2 where Ids=" + dr["ids"].ToString();
                        }
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

//            using (SqlConnection conn = DBHelp.getConn())
//            {
//                conn.Open();
//                SqlCommand objCommand = conn.CreateCommand();
               
//                foreach (var model in POOrders)
//                {
//                    StringBuilder sb = new StringBuilder();
//                    sb.AppendFormat(@"if exists(select ids from TB_SupplierAdvancePayments left join TB_SupplierAdvancePayment  on 
//TB_SupplierAdvancePayments.id=TB_SupplierAdvancePayment.id
//where Status='通过' and CaiIds={0})
//begin
//   update CAI_POCai set PayStatus=2 where Ids={0}
//end
//else if exists(select ids from TB_SupplierInvoices left join TB_SupplierInvoice  on 
//TB_SupplierInvoices.id=TB_SupplierInvoice.id
//where Status='执行中' and RuIds={0})
//begin
//   update CAI_POCai set PayStatus=1 where Ids={0}
//end
//else
//begin
//   update CAI_POCai set PayStatus=0 where Ids={0}
//end", model.Ids);
//                    objCommand.CommandText = sb.ToString();
//                    objCommand.ExecuteNonQuery();
//                }

//                conn.Close();
//            }
        }

    }
}
