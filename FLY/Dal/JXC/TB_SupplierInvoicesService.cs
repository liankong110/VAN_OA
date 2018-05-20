using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAN_OA.Model.JXC;
using System.Data.SqlClient;
 
using System.Text;
using System.Data;


namespace VAN_OA.Dal.JXC
{
    public class TB_SupplierInvoicesService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.TB_SupplierInvoices model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Id != null)
            {
                strSql1.Append("Id,");
                strSql2.Append("" + model.Id + ",");
            }
            if (model.RuIds != null)
            {
                strSql1.Append("RuIds,");
                strSql2.Append("" + model.RuIds + ",");
            }

            if (model.SupplierFPNo != null)
            {
                strSql1.Append("SupplierFPNo,");
                strSql2.Append("'" + model.SupplierFPNo + "',");
            }
           
            if (model.SupplierInvoiceDate != null)
            {
                strSql1.Append("SupplierInvoiceDate,");
                strSql2.Append("'" + model.SupplierInvoiceDate + "',");
            }
            if (model.SupplierInvoiceNum != null)
            {
                strSql1.Append("SupplierInvoiceNum,");
                strSql2.Append("" + model.SupplierInvoiceNum + ",");
            }
            if (model.SupplierInvoicePrice != null)
            {
                strSql1.Append("SupplierInvoicePrice,");
                strSql2.Append("" + model.SupplierInvoicePrice + ",");
            }
            if (model.SupplierInvoiceTotal != null)
            {
                strSql1.Append("SupplierInvoiceTotal,");
                strSql2.Append("" + model.SupplierInvoiceTotal + ",");
            }
            if (model.IsYuFu != null)
            {
                strSql1.Append("IsYuFu,");
                strSql2.Append("" + (model.IsYuFu ? 1 : 0) + ",");
            }
            strSql1.Append("SupplierProNo,");
            strSql2.Append("'" + model.SupplierProNo + "',");

            if (model.AllCaiIdsPay != null)
            {
                strSql1.Append("AllCaiIdsPay,");
                strSql2.Append("'" + model.AllCaiIdsPay + "',");
            }
            if (model.ActPay != null)
            {
                strSql1.Append("ActPay,");
                strSql2.Append("" + model.ActPay + ",");
            }
            if (model.RePayClear != null)
            {
                strSql1.Append("RePayClear,");
                strSql2.Append("" + model.RePayClear + ",");
            }
            if (model.FuShuTotal != null)
            {
                strSql1.Append("FuShuTotal,");
                strSql2.Append("" + model.FuShuTotal + ",");
            }
            if (model.IsPayStatus != null)
            {
                strSql1.Append("IsPayStatus,");
                strSql2.Append("" + model.IsPayStatus + ",");
            }
            if (model.IsHeBing != null)
            {
                strSql1.Append("IsHeBing,");
                strSql2.Append("" + model.IsHeBing + ",");
            }
            if (model.SupplierFpDate != null)
            {
                strSql1.Append("SupplierFpDate,");
                strSql2.Append("'" + model.SupplierFpDate + "',");
            }
            strSql.Append("insert into TB_SupplierInvoices(");
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
        public void Update(VAN_OA.Model.JXC.TB_SupplierInvoices model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_SupplierInvoices set ");
            //if (model.Id != null)
            //{
            //    strSql.Append("Id=" + model.Id + ",");
            //}
            //if (model.RuIds != null)
            //{
            //    strSql.Append("RuIds=" + model.RuIds + ",");
            //}
            if (model.SupplierFPNo != null)
            {
                strSql.Append("SupplierFPNo='" + model.SupplierFPNo + "',");
            }
            if (model.SupplierInvoiceDate != null)
            {
                strSql.Append("SupplierInvoiceDate='" + model.SupplierInvoiceDate + "',");
            }
            if (model.SupplierInvoiceNum != null)
            {
                strSql.Append("SupplierInvoiceNum=" + model.SupplierInvoiceNum + ",");
            }
            if (model.SupplierInvoicePrice != null)
            {
                strSql.Append("SupplierInvoicePrice=" + model.SupplierInvoicePrice + ",");
            }
            if (model.SupplierInvoiceTotal != null)
            {
                strSql.Append("SupplierInvoiceTotal=" + model.SupplierInvoiceTotal + ",");
            }
            if (model.SupplierProNo != null)
            {
                strSql.Append("SupplierProNo='" + model.SupplierProNo + "',");
            }

            if (model.SupplierFpDate != null)
            {
                strSql.Append("SupplierFpDate='" + model.SupplierFpDate + "',");
            }
            //if (model.IsYuFu != null)
            //{
            //    strSql.Append("IsYuFu=" + (model.IsYuFu ? 1 : 0) + ",");
            //}
            int n = strSql.ToString().LastIndexOf(",");
            strSql.Remove(n, 1);
            strSql.Append(" where Ids=" + model.Ids + "");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_SupplierInvoices ");
            strSql.Append(" where Ids=" + ids + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string ids, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_SupplierInvoices ");
            strSql.Append(" where Ids in(" + ids + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }
        /// 删除一条数据
        /// </summary>
        public void DeleteById(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_SupplierInvoices ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public VAN_OA.Model.JXC.SupplierToInvoiceView GetModel(int Ids)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select CAI_OrderInHouses.Ids,CAI_OrderInHouse.ProNo,RuTime,Supplier,houseName,PONo,POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum,GoodPrice,DoPer,SupplierFPNo,ChcekProNo,CAI_OrderInHouse.Status,SupplierInvoiceStatues,SupplierInvoiceDate,SupplierInvoiceTotal
 from  TB_SupplierInvoices  
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id
left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId 
left join (select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses group by OrderCheckIds )
as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids");
            strSql.Append(" where TB_SupplierInvoices.Ids=" + Ids + "");

            VAN_OA.Model.JXC.SupplierToInvoiceView model = null;
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
        public List<VAN_OA.Model.JXC.SupplierToInvoiceView> GetListArray_ToAdd(string strWhere)
        {

            var strSql = new StringBuilder();

            //CaiLastTruePrice = GoodPrice
            strSql.Append(@" select CaiNum,CAI_OrderInHouses.Ids,CAI_OrderInHouse.ProNo,RuTime,Supplier,houseName,CAI_OrderInHouse.PONo,CAI_OrderInHouse.POName,
GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum,CaiLastTruePrice as GoodPrice,DoPer,SupplierFPNo,ChcekProNo,
CAI_OrderInHouse.Status,SupplierInvoiceNum,SupplierInvoicePrice,SupplierInvoiceDate,SupplierInvoiceTotal,TB_SupplierInvoices.ids as PayIds,SupplierProNo
 ,tb3.AE,tb3.GuestName, HadSupplierInvoiceTotal,IsHanShui,ActPay,RePayClear ,FuShuTotal,IsPayStatus
from  TB_SupplierInvoices  
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id
left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId 
left join (SELECT CAI_OrderChecks.Ids as CheckIds,CAI_POCai.Num as CaiNum FROM CAI_OrderChecks left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
) as CheckOrders on CheckOrders.CheckIds=CAI_OrderInHouses.OrderCheckIds
left join 
(select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where status<>'不通过' group by OrderCheckIds )
as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids
--left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO and CG_POOrder.Status='通过' and IFZhui=0
left join 
(
select RuIds,Sum(SupplierInvoiceTotal) as  HadSupplierInvoiceTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where ((status='通过' and IsYuFu=0) or  (status<>'不通过' and IsYuFu=1)) and SupplierInvoiceTotal>0
group by RuIds
)
as tb2 on tb2.RuIds=CAI_OrderInHouses.ids
left join 
(
select AE,CAI_POOrder.GuestName,PONo,GoodId,min(IsHanShui) as IsHanShui from CAI_POOrder left join CAI_POCai on CAI_POOrder.id=CAI_POCai.id
where Status='通过'
group by PONo, GoodId,AE,CAI_POOrder.GuestName
)
as tb3  on tb3.PONO=CAI_OrderInHouse.PONO and tb3.GoodId=CAI_OrderInHouses.GooId 
");


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.SupplierToInvoiceView> list = new List<VAN_OA.Model.JXC.SupplierToInvoiceView>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        object ojb;
                        ojb = dataReader["HadSupplierInvoiceTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.HadSupplierInvoiceTotal =Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["IsHanShui"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsHanShui = Convert.ToInt32(ojb) == 0 ? false : true;
                        }

                        if (model.IsHanShui == false && model.SupplierFPNo == "")
                        {
                            model.SupplierFPNo = "-";
                        }
                        decimal CaiNum=0;
                        ojb = dataReader["CaiNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            CaiNum = Convert.ToDecimal(ojb);
                        }
                        model.ActPay = Convert.ToDecimal(dataReader["ActPay"]);
                        model.RePayClear = Convert.ToInt32(dataReader["RePayClear"]);
                        model.FuShuTotal = Convert.ToDecimal(dataReader["FuShuTotal"]);
                        model.IsPayStatus = Convert.ToInt32(dataReader["IsPayStatus"]);
                        //剩余金额 =实采单价×总采购数量-已经支付的金额+负数合计
                        model.ResultTotal = model.GoodPrice * CaiNum - model.HadSupplierInvoiceTotal + model.FuShuTotal;
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.SupplierToInvoiceView> GetListArray(string strWhere)
        {
            //CaiLastTruePrice = GoodPrice
            StringBuilder strSql = new StringBuilder();

            strSql.Append(@" select SumPOTotal,InvoiceTotal,CaiNum,TB_SupplierInvoices.SupplierFpDate,TB_SupplierInfo.SupplierName,CAI_OrderInHouses.Ids,CAI_OrderInHouse.ProNo,
RuTime,Supplier,houseName,CAI_OrderInHouse.PONo,CAI_OrderInHouse.POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,
supplierTuiGoodNum,CaiLastTruePrice as GoodPrice,DoPer,SupplierFPNo,ChcekProNo,CAI_OrderInHouse.Status,SupplierInvoiceNum,SupplierInvoicePrice,SupplierInvoiceDate,SupplierInvoiceTotal,TB_SupplierInvoices.ids as PayIds,SupplierProNo
 ,tb3.AE,tb3.GuestName, HadSupplierInvoiceTotal,IsHanShui,ActPay,RePayClear ,FuShuTotal,IsPayStatus,IsYuFu,HadFuShuTotal
from  TB_SupplierInvoices  
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id
left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId 
left join (SELECT CAI_POCai.Ids AS CAI_IDS,CAI_OrderChecks.Ids as CheckIds,CAI_POCai.Num as CaiNum FROM CAI_OrderChecks left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
) as CheckOrders on CheckOrders.CheckIds=CAI_OrderInHouses.OrderCheckIds
left join 
(select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where status<>'不通过' group by OrderCheckIds )
as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids
--left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO and CG_POOrder.Status='通过' and IFZhui=0
left join 
(
select  TB_SupplierInvoices.RuIds,Sum(SupplierInvoiceTotal) as  HadSupplierInvoiceTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where ((status='通过' and IsYuFu=0) or  (status<>'不通过' and IsYuFu=1)) and SupplierInvoiceTotal>0
group by TB_SupplierInvoices.RuIds
)
as tb2 on tb2.RuIds=CAI_OrderInHouses.ids
left join
(
select CAI_OrderInHouses.Ids,Sum(CAI_OrderOutHouses.GoodNum*CAI_OrderInHouses.CaiLastTruePrice) as  HadFuShuTotal from 
CAI_OrderOutHouse 
left join CAI_OrderOutHouses on CAI_OrderOutHouse.id=CAI_OrderOutHouses.id
left join CAI_OrderInHouses on CAI_OrderInHouses.Ids=CAI_OrderOutHouses.OrderCheckIds
where CAI_OrderOutHouse.status='通过' 
group by CAI_OrderInHouses.Ids
)
as temp3 on temp3.Ids=CAI_OrderInHouses.ids
left join 
(
select AE,CAI_POOrder.GuestName,PONo,GoodId,min(IsHanShui) as IsHanShui from CAI_POOrder left join CAI_POCai on CAI_POOrder.id=CAI_POCai.id
where Status='通过'
group by PONo, GoodId,AE,CAI_POOrder.GuestName
)
as tb3  on tb3.PONO=CAI_OrderInHouse.PONO and tb3.GoodId=CAI_OrderInHouses.GooId left join TB_SupplierInfo on TB_SupplierInfo.SupplieSimpeName=Supplier and TB_SupplierInfo.status='通过'
left join [PO_InvoiceTotal_SumView] on [PO_InvoiceTotal_SumView].PONo=CAI_OrderInHouse.PONO");


            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<VAN_OA.Model.JXC.SupplierToInvoiceView> list = new List<VAN_OA.Model.JXC.SupplierToInvoiceView>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        object ojb;
                        ojb = dataReader["HadSupplierInvoiceTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.HadSupplierInvoiceTotal =Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["IsHanShui"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsHanShui = Convert.ToInt32(ojb)==0?false:true;
                        }
                        ojb = dataReader["IsYuFu"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsYuFu = Convert.ToInt32(ojb) == 0 ? false : true;
                        }

                        
                        if (model.IsHanShui == false && model.SupplierFPNo == "")
                        {
                            model.SupplierFPNo = "-";
                        }
                        model.ActPay = Convert.ToDecimal(dataReader["ActPay"]);
                        model.RePayClear = Convert.ToInt32(dataReader["RePayClear"]);
                        model.FuShuTotal = Convert.ToDecimal(dataReader["FuShuTotal"]);
                        model.IsPayStatus = Convert.ToInt32(dataReader["IsPayStatus"]);
                        model.GoodNum = model.GoodNum ?? 0;

                        decimal CaiNum = 0;
                        ojb = dataReader["CaiNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            CaiNum = Convert.ToDecimal(ojb);
                        }
                        decimal HadFuShuTotal = 0;
                        ojb = dataReader["HadFuShuTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            HadFuShuTotal = Convert.ToDecimal(ojb);
                        }

                        //剩余金额 =实采单价×总采购数量-已经支付的金额+负数合计
                        model.ResultTotal = model.GoodPrice * CaiNum - model.HadSupplierInvoiceTotal -HadFuShuTotal;
                     
                        ojb = dataReader["SupplierName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SupplierAllName = ojb.ToString();
                        }
                        ojb = dataReader["SupplierFpDate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SupplierFpDate = (DateTime)ojb;
                        }
                        decimal SumPOTotal = 0;
                        ojb = dataReader["SumPOTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                           SumPOTotal = (decimal)ojb;
                        }
                        decimal InvoiceTotal = 0;
                        ojb = dataReader["InvoiceTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            InvoiceTotal = (decimal)ojb;
                        }
                        //回款率，=到款金额（全部通过的）/项目金额，如果项目金额=0，回款率=0（红色显示）
                        if (SumPOTotal != 0)
                        {
                            model.HuiKuanLiLv = InvoiceTotal / SumPOTotal;
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
        public SupplierToInvoiceView ReaderBind(IDataReader dataReader)
        {
            SupplierToInvoiceView model = new SupplierToInvoiceView();
            object ojb;
            model.ProNo = dataReader["ProNo"].ToString();
            ojb = dataReader["RuTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RuTime = (DateTime)ojb;
            }
            model.GuestName = dataReader["Supplier"].ToString();
            model.houseName = dataReader["houseName"].ToString();
            model.PONo = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
            model.GoodNo = dataReader["GoodNo"].ToString();
            model.GoodName = dataReader["GoodName"].ToString();
            model.GoodTypeSmName = dataReader["GoodTypeSmName"].ToString();
            model.GoodSpec = dataReader["GoodSpec"].ToString();
            model.GoodUnit = dataReader["GoodUnit"].ToString();
            ojb = dataReader["GoodNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNum = (decimal)ojb;
            }
            ojb = dataReader["Ids"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ids = (int)ojb;
            }
            ojb = dataReader["supplierTuiGoodNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.supplierTuiGoodNum = (decimal)ojb;
            }
            ojb = dataReader["SupplierInvoiceTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoiceTotal = (decimal)ojb;
            }

            decimal GoodNum = model.GoodNum ?? 0;
            decimal supplierTuiGoodNum = model.supplierTuiGoodNum ?? 0;
            model.lastGoodNum = GoodNum - supplierTuiGoodNum;

            ojb = dataReader["GoodPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodPrice = (decimal)ojb;
            }
            model.LastTotal = model.GoodPrice * model.lastGoodNum;

            model.DoPer = dataReader["DoPer"].ToString();
            model.SupplierFPNo = dataReader["SupplierFPNo"].ToString();
            model.ChcekProNo = dataReader["ChcekProNo"].ToString();
            model.Status = dataReader["Status"].ToString();
            //ojb = dataReader["SupplierInvoiceStatues"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.SupplierInvoiceStatues = (int)ojb;
            //}
            ojb = dataReader["SupplierInvoiceDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoiceDate = (DateTime)ojb;
            }
            ojb = dataReader["SupplierInvoiceNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoiceNum = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["SupplierInvoicePrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoicePrice = Convert.ToDecimal(ojb);
            }
            model.payIds = Convert.ToInt32(dataReader["PayIds"]);
            model.SupplierProNo = Convert.ToString(dataReader["SupplierProNo"]);

            //ojb = dataReader["PayStatus"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.SupplierInvoiceStatues = (int)ojb;
            //    if (model.SupplierInvoiceStatues.Value == 0)
            //    {
            //        model.isZhiFu = "未支付";
            //    }
            //    if (model.SupplierInvoiceStatues.Value == 1)
            //    {
            //        model.isZhiFu = "拟支付";
            //    }
            //    if (model.SupplierInvoiceStatues.Value == 2)
            //    {
            //        model.isZhiFu = "已支付";
            //    }
            //}
            //else
            //{
            //    model.isZhiFu = "未支付";
            //}

            ojb = dataReader["AE"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POAE = ojb.ToString();
            }
            ojb = dataReader["GuestName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POGuestName = ojb.ToString();
            }
            return model;
        }


    }
}
