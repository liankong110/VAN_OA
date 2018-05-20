using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAN_OA.Model.JXC;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace VAN_OA.Dal.JXC
{
    public class SupplierToInvoiceViewService
    {
        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<SupplierToInvoiceView> GetListArray_New(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CAI_OrderInHouses.GoodNum,CaiNum,HadSupplierInvoiceTotal,HadFuShuTotal,CAI_OrderInHouses.CaiLastTruePrice,CAI_OrderInHouses.IsTemp,CAI_OrderInHouses.Ids,CAI_OrderInHouse.ProNo,RuTime,Supplier,houseName,PONo,POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum,GoodPrice,DoPer,ChcekProNo,CAI_OrderInHouse.Status,zhengTotal,jianTotal,PayStatus,0 as FuShuTotal ");
            strSql.Append(" from CAI_OrderInHouse ");
            strSql.Append(" left join CAI_OrderInHouses  on CAI_OrderInHouses.id=CAI_OrderInHouse.id ");
            strSql.Append(" left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id ");
            strSql.Append(" left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId ");
            strSql.Append(@" 
left join (SELECT CAI_POCai.Ids AS CAI_IDS,CAI_OrderChecks.Ids as CheckIds,CAI_POCai.Num as CaiNum FROM CAI_OrderChecks left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
) as CheckOrders on CheckOrders.CheckIds=CAI_OrderInHouses.OrderCheckIds
left join 
(
select TB_SupplierInvoices.RuIds,Sum(SupplierInvoiceTotal) as  HadSupplierInvoiceTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where ((status<>'不通过' and IsYuFu=0) or  (status<>'不通过' and IsYuFu=1)) and SupplierInvoiceTotal>0
group by TB_SupplierInvoices.RuIds
)
as HadSupplierInvoice on HadSupplierInvoice.RuIds=CAI_OrderInHouses.Ids
left join
(
select CAI_OrderInHouses.Ids,Sum(CAI_OrderOutHouses.GoodNum*CAI_OrderInHouses.CaiLastTruePrice) as  HadFuShuTotal from 
CAI_OrderOutHouse 
left join CAI_OrderOutHouses on CAI_OrderOutHouse.id=CAI_OrderOutHouses.id
left join CAI_OrderInHouses on CAI_OrderInHouses.Ids=CAI_OrderOutHouses.OrderCheckIds
where CAI_OrderOutHouse.status='通过' 
group by CAI_OrderInHouses.Ids
)
as temp3 on temp3.Ids=CAI_OrderInHouses.Ids
 
left join 
(
select sum(SupplierInvoiceTotal) as zhengTotal,ruIds
from  TB_SupplierInvoice 
left join TB_SupplierInvoices on  TB_SupplierInvoice.id=TB_SupplierInvoices.Id
where TB_SupplierInvoice.status<>'不通过' and SupplierInvoiceTotal>0
group by ruIds
)
as tb2 on tb2.ruIds=CAI_OrderInHouses.ids ");
            strSql.Append(@" left join 
(
select sum(SupplierInvoiceTotal) as jianTotal,ruIds
from  TB_SupplierInvoice 
left join TB_SupplierInvoices on  TB_SupplierInvoice.id=TB_SupplierInvoices.Id
where TB_SupplierInvoice.status<>'不通过' and SupplierInvoiceTotal<0
group by ruIds
)
as tb3 on tb3.ruIds=CAI_OrderInHouses.ids ");
            strSql.Append(" left join (");
            strSql.Append(" select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses group by OrderCheckIds ");
            strSql.Append("  )");
            strSql.Append(" as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append("  order by  CAI_OrderInHouses.Ids desc");
            List<SupplierToInvoiceView> list = new List<SupplierToInvoiceView>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        model.LastTruePrice = (decimal)dataReader["CaiLastTruePrice"];

                        object ojb;
                        decimal CaiNum = 0;
                        decimal HadSupplierInvoiceTotal = 0;
                        decimal HadFuShuTotal = 0;
                        decimal inGoodNum = 0;
                        ojb = dataReader["CaiNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            CaiNum = (decimal)ojb;
                        }
                        ojb = dataReader["GoodNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            inGoodNum = (decimal)ojb;
                        }
                        ojb = dataReader["HadSupplierInvoiceTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            HadSupplierInvoiceTotal = (decimal)ojb;
                        }
                        ojb = dataReader["HadFuShuTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            HadFuShuTotal = (decimal)ojb;
                        }

                        model.IsTemp = (bool)dataReader["IsTemp"];

                        //剩余金额 =实采单价×总采购数量-已经支付的金额+负数合计
                        var result = inGoodNum * model.LastTruePrice - HadSupplierInvoiceTotal - HadFuShuTotal;

                        //var result = CaiNum * model.LastTruePrice - model.SupplierInvoiceTotal;
                        if (result <= 0)
                        {
                            model.IsShow = false;
                        }
                        else
                        {
                            model.IsShow = true;
                        }
                        model.LastTotal = model.LastTruePrice * model.lastGoodNum;
                        model.ResultTotal = result;
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<SupplierToInvoiceView> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CAI_OrderInHouses.CaiLastTruePrice,CAI_OrderInHouses.IsTemp,CAI_OrderInHouses.Ids,CAI_OrderInHouse.ProNo,RuTime,Supplier,houseName,PONo,POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum,GoodPrice,DoPer,ChcekProNo,CAI_OrderInHouse.Status,zhengTotal,jianTotal,PayStatus,0 as FuShuTotal ");
            strSql.Append(" from CAI_OrderInHouse ");
            strSql.Append(" left join CAI_OrderInHouses  on CAI_OrderInHouses.id=CAI_OrderInHouse.id ");
            strSql.Append(" left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id ");
            strSql.Append(" left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId ");
            strSql.Append(@" left join 
(
select sum(SupplierInvoiceTotal) as zhengTotal,ruIds
from  TB_SupplierInvoice 
left join TB_SupplierInvoices on  TB_SupplierInvoice.id=TB_SupplierInvoices.Id
where TB_SupplierInvoice.status<>'不通过' and SupplierInvoiceTotal>0
group by ruIds
)
as tb2 on tb2.ruIds=CAI_OrderInHouses.ids ");
            strSql.Append(@" left join 
(
select sum(SupplierInvoiceTotal) as jianTotal,ruIds
from  TB_SupplierInvoice 
left join TB_SupplierInvoices on  TB_SupplierInvoice.id=TB_SupplierInvoices.Id
where TB_SupplierInvoice.status<>'不通过' and SupplierInvoiceTotal<0
group by ruIds
)
as tb3 on tb3.ruIds=CAI_OrderInHouses.ids ");
            strSql.Append(" left join (");
            strSql.Append(" select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses group by OrderCheckIds ");
            strSql.Append("  )");
            strSql.Append(" as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append("  order by  CAI_OrderInHouses.Ids desc");
            List<SupplierToInvoiceView> list = new List<SupplierToInvoiceView>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        model.LastTruePrice = (decimal)dataReader["CaiLastTruePrice"];

                        model.IsTemp = (bool)dataReader["IsTemp"];
                        var result = model.GoodNum * model.LastTruePrice - model.SupplierInvoiceTotal;
                        if (result <= 0)
                        {
                            model.IsShow = false;
                        }
                        else
                        {
                            model.IsShow = true;
                        }
                        model.LastTotal = model.LastTruePrice * model.lastGoodNum;
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 根据入库明细IDS 来获取对应项目的备注信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<PoRemarkInfo> GetPoRemark(string ids)
        {
            List<PoRemarkInfo> PoRemarkInfoList = new List<PoRemarkInfo>();
            string sql = string.Format(@"select PORemark,CAI_OrderInHouses.ids from CAI_OrderInHouses 
left join [dbo].[CAI_OrderChecks] on [dbo].[CAI_OrderChecks].Ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on [dbo].CAI_POCai.Ids=[CAI_OrderChecks].CaiId
left join [dbo].[CAI_POOrders] on [CAI_POOrders].Id=CAI_POCai.Id AND CAI_POCai.GoodId=[CAI_POOrders].GoodId
left join [dbo].[CG_POOrders]  on[dbo].[CG_POOrders].Ids=[CAI_POOrders].CG_POOrdersId
left join [CG_POOrder] on [CG_POOrder].Id=[CG_POOrders].Id where CAI_OrderInHouses.ids in ({0})", ids);

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = new PoRemarkInfo();
                        model.Ids = Convert.ToInt32(dataReader["ids"]);
                        object ojb;
                        ojb = dataReader["PORemark"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PORemark = Convert.ToString(ojb);
                        }
                        PoRemarkInfoList.Add(model);
                    }
                }
            }
            return PoRemarkInfoList;

        }

        /// <summary>
        /// 根据入库明细IDS 来获取对应项目的备注信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<PoRemarkInfo> GetPoRemarkByCaiIds(string ids)
        {
            List<PoRemarkInfo> PoRemarkInfoList = new List<PoRemarkInfo>();
            string sql = string.Format(@"select PORemark,CAI_POCai.ids from CAI_POCai 
left join [CAI_POOrders] ON [CAI_POOrders].Id=CAI_POCai.Id AND CAI_POCai.GoodId=[CAI_POOrders].GoodId
left join [dbo].[CG_POOrders]  on[dbo].[CG_POOrders].Ids=[CAI_POOrders].CG_POOrdersId
left join [CG_POOrder] on [CG_POOrder].Id=[CG_POOrders].Id
where CAI_POCai.ids in ({0})", ids);

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = new PoRemarkInfo();
                        model.Ids = Convert.ToInt32(dataReader["ids"]);
                        object ojb;
                        ojb = dataReader["PORemark"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PORemark = Convert.ToString(ojb);
                        }
                        PoRemarkInfoList.Add(model);
                    }
                }
            }
            return PoRemarkInfoList;

        }


        public List<SupplierToInvoiceView> GetListArrayToVerify(string strWhere, string ids)
        {
            //CaiLastTruePrice = GoodPrice
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CAI_OrderInHouses.GooId, CAI_OrderInHouses.Ids,CAI_OrderInHouse.ProNo,RuTime,Supplier,houseName,CAI_OrderInHouse.PONo,CAI_OrderInHouse.POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum,CaiLastTruePrice as GoodPrice,DoPer,'' as SupplierFPNo,ChcekProNo,CAI_OrderInHouse.Status,0 as SupplierInvoiceNum,0 as SupplierInvoicePrice,null as SupplierInvoiceDate,0 as SupplierInvoiceTotal,tb3.AE,tb3.GuestName, HadSupplierInvoiceTotal,IsHanShui,FuShuTotal ");
            strSql.Append(" from CAI_OrderInHouse ");
            strSql.Append(" left join CAI_OrderInHouses  on CAI_OrderInHouses.id=CAI_OrderInHouse.id ");
            strSql.Append(" left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id ");
            strSql.Append(" left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId ");
            strSql.Append(" left join (");
            strSql.Append(" select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where status<>'不通过' group by OrderCheckIds ");
            strSql.Append("  )");
            strSql.Append(" as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids ");
            //strSql.Append(" left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO and CG_POOrder.Status='通过' and IFZhui=0 ");

            strSql.Append(@" left join 
(
select AE,CAI_POOrder.GuestName,PONo,GoodId,min(IsHanShui) as IsHanShui from CAI_POOrder left join CAI_POCai on CAI_POOrder.id=CAI_POCai.id
where Status='通过'
group by PONo, GoodId,AE,CAI_POOrder.GuestName
)
as tb3  on tb3.PONO=CAI_OrderInHouse.PONO and tb3.GoodId=CAI_OrderInHouses.GooId ");
            strSql.AppendFormat(@"left join 
(
select RuIds,Sum(SupplierInvoiceTotal) as  HadSupplierInvoiceTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where  ((status='通过' and IsYuFu=0) or  (status<>'不通过' and IsYuFu=1)) and RuIds in ({0})  and SupplierInvoiceTotal>0
group by RuIds
)
as tb2 on tb2.RuIds=CAI_OrderInHouses.ids 
left join 
(
select RuIds,Sum(SupplierInvoiceTotal) as  FuShuTotal from 
TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where status='通过' and  SupplierInvoiceTotal<0 and RuIds in ({0}) 
group by RuIds
)
as tb4 on tb4.RuIds=CAI_OrderInHouses.ids 
", ids);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<SupplierToInvoiceView> list = new List<SupplierToInvoiceView>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBindToVerify(dataReader);
                        object ojb;

                        ojb = dataReader["GooId"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodId = Convert.ToInt32(ojb);
                        }
                        model.InHouseGoodNum = (model.GoodNum ?? 0);
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
            //model.SupplierFPNo = dataReader["SupplierFPNo"].ToString();
            model.ChcekProNo = dataReader["ChcekProNo"].ToString();
            model.Status = dataReader["Status"].ToString();
            ojb = dataReader["PayStatus"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoiceStatues = (int)ojb;
                if (model.SupplierInvoiceStatues.Value == 0)
                {
                    model.isZhiFu = "未支付";
                }
                if (model.SupplierInvoiceStatues.Value == 1)
                {
                    model.isZhiFu = "拟支付";
                }
                if (model.SupplierInvoiceStatues.Value == 2)
                {
                    model.isZhiFu = "已支付";
                }
            }
            else
            {
                model.isZhiFu = "未支付";
            }
            //ojb = dataReader["SupplierInvoiceDate"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.SupplierInvoiceDate = (DateTime)ojb;
            //}
            //ojb = dataReader["SupplierInvoiceNum"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.SupplierInvoiceNum = Convert.ToDecimal(ojb);
            //}
            //ojb = dataReader["SupplierInvoicePrice"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.SupplierInvoicePrice = Convert.ToDecimal(ojb);
            //}

            ojb = dataReader["zhengTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.zhengTotal = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["jianTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.jianTotal = Convert.ToDecimal(ojb);
            }

            model.SupplierInvoiceTotal = model.zhengTotal - model.jianTotal;

            ojb = dataReader["FuShuTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FuShuTotal = Convert.ToDecimal(ojb);
                model.ActPay = model.FuShuTotal;
            }

            return model;
        }

        public SupplierToInvoiceView ReaderBindToVerify(IDataReader dataReader)
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

            ojb = dataReader["HadSupplierInvoiceTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HadSupplierInvoiceTotal = Convert.ToDecimal(ojb);
            }


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

            decimal GoodNum = model.GoodNum ?? 0;
            decimal supplierTuiGoodNum = model.supplierTuiGoodNum ?? 0;
            model.lastGoodNum = GoodNum - supplierTuiGoodNum;

            ojb = dataReader["GoodPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodPrice = (decimal)ojb;
            }
            //强调 支付金额 按照入库数量*金额来计算 不要扣减 退款数量
            if (model.GoodNum != null) model.LastTotal = model.GoodPrice * model.GoodNum.Value;

            model.DoPer = dataReader["DoPer"].ToString();
            model.ChcekProNo = dataReader["ChcekProNo"].ToString();
            model.Status = dataReader["Status"].ToString();

            ojb = dataReader["SupplierInvoiceTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoiceTotal = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["IsHanShui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsHanShui = Convert.ToInt32(ojb) == 0 ? false : true;
            }

            try
            {
                model.FuShuTotal = Convert.ToDecimal(dataReader["FuShuTotal"]);
            }
            catch (Exception)
            {


            }

            //3.剩余金额=入库数量*实采单价-此次入库已经付款金额 （ 就是2的描述）
            //model.ResultTotal = model.GoodPrice * model.GoodNum.Value - model.HadSupplierInvoiceTotal + model.FuShuTotal;

            model.ResultTotal = model.GoodPrice * model.GoodNum.Value - model.SupplierInvoiceTotal;

            //model.ResultTotal = model.GoodPrice * model.GoodNum.Value - model.HadSupplierInvoiceTotal + model.FuShuTotal;

            return model;
        }

        #region 预付款

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<SupplierToInvoiceView> GetSupplierAdvancePaymentList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select CAI_POCai.IsCAITemp,CAI_POCai.Ids,CAI_POOrder.ProNo,lastSupplier,
PONo,POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,Num,
lastPrice,LastTruePrice,CAI_POOrder.Status,SupplierInvoiceTotal,PayStatus,CaiId  
from CAI_POOrder  
left join CAI_POCai  on CAI_POCai.id=CAI_POOrder.id  
left join TB_Good on TB_Good.GoodId=CAI_POCai.GoodId  left join 
(
select TB_SupplierAdvancePayments.CaiIds,sum(SupplierInvoiceTotal) as SupplierInvoiceTotal
from  TB_SupplierAdvancePayment 
left join TB_SupplierAdvancePayments on  TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id
where TB_SupplierAdvancePayment.status<>'不通过'
group by CaiIds
)
as tb2 on tb2.CaiIds=CAI_POCai.ids
left join 
(
	SELECT CaiId FROM CAI_OrderChecks left join CAI_OrderCheck on CAI_OrderChecks.CheckId=CAI_OrderCheck.Id
where Status in ('通过','执行中')
group by CaiId
)
as tb3 on tb3.CaiId=CAI_POCai.Ids ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CAI_POCai.Ids desc");
            List<SupplierToInvoiceView> list = new List<SupplierToInvoiceView>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBindSupplierAdvancePayment(dataReader);
                        object ojb = dataReader["IsCAITemp"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsTemp = (bool)ojb;
                        }

                        ojb = dataReader["LastTruePrice"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.LastTruePrice = (decimal)ojb;
                        }

                        model.LastTotal = model.LastTruePrice * model.lastGoodNum;
                        if (model.IfRuKu || (model.LastTotal - model.SupplierInvoiceTotal) <= 0)
                        {
                            model.IsShow = false;
                        }
                        else
                        {
                            model.IsShow = true;
                        }
                        //剩余可预付金额=数量*实采单价-已预付金额
                        model.ResultTotal = (model.GoodNum??0) * model.LastTruePrice - model.SupplierInvoiceTotal;
                        //剩余预价=剩余可预付金额*采购单价/实采单价

                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<SupplierToInvoiceView> GetSupplierAdvancePaymentListToLoadVerify(string strWhere, string ids)
        {
            // LastTruePrice=lastPrice 修改
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select CAI_POCai.Ids,CAI_POOrder.ProNo,lastSupplier,
PONo,POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,Num,
LastTruePrice as lastPrice,'' as SupplierFPNo,CAI_POOrder.Status,0 as SupplierInvoiceNum, 0 as SupplierInvoicePrice,
null as SupplierInvoiceDate,0 as SupplierInvoiceTotal,AE,CAI_POOrder.GuestName ,HadSupplierInvoiceTotal ,IsHanShui
from CAI_POOrder  
left join CAI_POCai  on CAI_POCai.id=CAI_POOrder.id  
left join TB_Good on TB_Good.GoodId=CAI_POCai.GoodId
left join 
(
select CaiIds,Sum(SupplierInvoiceTotal) as  HadSupplierInvoiceTotal from 
TB_SupplierAdvancePayments 
left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.id
where status='通过' and CaiIds in ({0})
group by CaiIds
) as tb2 on tb2.CaiIds=CAI_POCai.ids", ids);

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<SupplierToInvoiceView> list = new List<SupplierToInvoiceView>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBindSupplierAdvancePaymentToLoadVerify(dataReader));
                    }
                }
            }
            return list;
        }



        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public SupplierToInvoiceView ReaderBindSupplierAdvancePayment(IDataReader dataReader)
        {
            SupplierToInvoiceView model = new SupplierToInvoiceView();
            object ojb;
            model.ProNo = dataReader["ProNo"].ToString();
            model.GuestName = dataReader["lastSupplier"].ToString();
            model.PONo = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
            model.GoodNo = dataReader["GoodNo"].ToString();
            model.GoodName = dataReader["GoodName"].ToString();
            model.GoodTypeSmName = dataReader["GoodTypeSmName"].ToString();
            model.GoodSpec = dataReader["GoodSpec"].ToString();
            model.GoodUnit = dataReader["GoodUnit"].ToString();
            ojb = dataReader["Num"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNum = (decimal)ojb;
            }
            ojb = dataReader["Ids"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ids = (int)ojb;
            }

            ojb = dataReader["SupplierInvoiceTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoiceTotal = Convert.ToDecimal(ojb);
            }

            decimal GoodNum = model.GoodNum ?? 0;
            decimal supplierTuiGoodNum = model.supplierTuiGoodNum ?? 0;
            model.lastGoodNum = GoodNum - supplierTuiGoodNum;

            ojb = dataReader["lastPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodPrice = Convert.ToDecimal(ojb);
            }

            ojb = dataReader["CaiId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IfRuKu = true;
            }


            model.LastTotal = model.GoodPrice * model.lastGoodNum;

            //ojb = dataReader["SupplierFPNo"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.SupplierFPNo =ojb.ToString();
            //}


            //ojb = dataReader["SupplierInvoiceStatues"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.SupplierInvoiceStatues = (int)ojb;
            //    if (model.SupplierInvoiceStatues.Value == 1)
            //    {
            //        model.isZhiFu = "拟支付";
            //    }
            //    else
            //    {
            //        model.isZhiFu = "已支付";
            //    }
            //}
            //else
            //{
            //    model.isZhiFu = "未支付";
            //}
            //ojb = dataReader["SupplierInvoiceDate"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.SupplierInvoiceDate = (DateTime)ojb;
            //}
            //ojb = dataReader["SupplierInvoiceNum"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.SupplierInvoiceNum = Convert.ToDecimal(ojb);
            //}
            //ojb = dataReader["SupplierInvoicePrice"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.SupplierInvoicePrice = Convert.ToDecimal(ojb);
            //}
            ojb = dataReader["PayStatus"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoiceStatues = (int)ojb;
                if (model.SupplierInvoiceStatues.Value == 0)
                {
                    model.isZhiFu = "未支付";
                }
                if (model.SupplierInvoiceStatues.Value == 1)
                {
                    model.isZhiFu = "拟支付";
                }
                if (model.SupplierInvoiceStatues.Value == 2)
                {
                    model.isZhiFu = "已支付";
                }
            }
            else
            {
                model.isZhiFu = "未支付";
            }


            return model;
        }

        public SupplierToInvoiceView ReaderBindSupplierAdvancePaymentToLoadVerify(IDataReader dataReader)
        {
            SupplierToInvoiceView model = new SupplierToInvoiceView();
            object ojb;
            model.ProNo = dataReader["ProNo"].ToString();
            model.GuestName = dataReader["lastSupplier"].ToString();
            model.PONo = dataReader["PONo"].ToString();
            model.POName = dataReader["POName"].ToString();
            model.GoodNo = dataReader["GoodNo"].ToString();
            model.GoodName = dataReader["GoodName"].ToString();
            model.GoodTypeSmName = dataReader["GoodTypeSmName"].ToString();
            model.GoodSpec = dataReader["GoodSpec"].ToString();
            model.GoodUnit = dataReader["GoodUnit"].ToString();
            ojb = dataReader["Num"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNum = (decimal)ojb;
            }

            ojb = dataReader["HadSupplierInvoiceTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HadSupplierInvoiceTotal = (decimal)ojb;
            }

            ojb = dataReader["Ids"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Ids = (int)ojb;
            }

            ojb = dataReader["SupplierInvoiceTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoiceTotal = Convert.ToDecimal(ojb);
            }

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

            decimal GoodNum = model.GoodNum ?? 0;
            decimal supplierTuiGoodNum = model.supplierTuiGoodNum ?? 0;
            model.lastGoodNum = GoodNum - supplierTuiGoodNum;

            ojb = dataReader["lastPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodPrice = Convert.ToDecimal(ojb);
            }
            model.LastTotal = model.GoodPrice * model.lastGoodNum;

            ojb = dataReader["SupplierFPNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierFPNo = ojb.ToString();
            }


            //ojb = dataReader["SupplierInvoiceStatues"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.SupplierInvoiceStatues = (int)ojb;
            //    if (model.SupplierInvoiceStatues.Value == 1)
            //    {
            //        model.isZhiFu = "拟支付";
            //    }
            //    else
            //    {
            //        model.isZhiFu = "已支付";
            //    }
            //}
            //else
            //{
            //    model.isZhiFu = "未支付";
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

            ojb = dataReader["IsHanShui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsHanShui = Convert.ToInt32(ojb) == 0 ? false : true;
            }



            return model;
        }
        #endregion


        public List<SupplierToInvoiceView> GetSupplierInvoiceListToNoPay(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select IsHanShui,'支' as busType,TB_SupplierInvoice.ProNo as InvProNo, TB_SupplierInvoices.Id as payId,TB_SupplierInvoices.Ids as payIds,CAI_OrderInHouses.Ids,
CAI_OrderInHouse.ProNo,RuTime,CAI_OrderInHouse.Supplier,houseName,CAI_OrderInHouse.PONo,CAI_OrderInHouse.POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum
,GoodPrice,TB_SupplierInvoice.CreateName as DoPer,SupplierFPNo,ChcekProNo,TB_SupplierInvoice.Status,SupplierInvoiceNum,SupplierInvoicePrice,SupplierInvoiceDate,SupplierInvoiceTotal,TB_SupplierInvoice.CreteTime,IsYuFu,SupplierProNo,IsPayStatus as PayStatus,ActPay,FuShuTotal
 ,CG_POOrder.AE,CG_POOrder.GuestName,RePayClear,IsHeBing from  TB_SupplierInvoices  
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id
left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId 
left join (select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where status<>'不通过' group by OrderCheckIds )
as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids
left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO and CG_POOrder.Status='通过' and IFZhui=0
left join CAI_OrderChecks on CAI_OrderChecks.Ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by payIds desc ");
            List<SupplierToInvoiceView> list = new List<SupplierToInvoiceView>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBindSupplierInvoice(dataReader);
                        var ojb = dataReader["IsHeBing"]; ;

                        if (ojb != null && ojb != DBNull.Value)
                        {
                            if (Convert.ToInt32(ojb) == 0)
                            {
                                model.IsHeBingString = "不合并";
                            }
                            else if (Convert.ToInt32(ojb) == 1)
                            {
                                model.IsHeBingString = "合并";

                            }

                        }
                        ojb = dataReader["IsHanShui"]; ;

                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Good_IsHanShui = (int)ojb;
                        }

                        list.Add(model);
                    }
                }
            }
            return list;
        }
        public List<SupplierToInvoiceView> GetSupplierInvoiceList(string strWhere, string type)
        {

            StringBuilder strSql = new StringBuilder();
            if (type == "9")//已扣回款项的供应商预付款和供应商付款单
            {
                strSql.Append(@"select * from (
select BusType as CaiBusType,SupplierAdvanceId,TB_SupplierInvoice.CreateName, '支' as busType,
TB_SupplierInvoice.ProNo as InvProNo, 
TB_SupplierInvoice.Id as payId,TB_SupplierInvoices.Ids as payIds,CAI_OrderInHouses.Ids,
CAI_OrderInHouse.ProNo,'' as CaiProNo,RuTime,CAI_OrderInHouse.Supplier,houseName,CAI_OrderInHouse.PONo,
CAI_OrderInHouse.POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum
,GoodPrice,TB_SupplierInvoice.CreateName as DoPer,SupplierFPNo,ChcekProNo,TB_SupplierInvoice.Status,
SupplierInvoiceNum,SupplierInvoicePrice,SupplierInvoiceDate,SupplierInvoiceTotal,TB_SupplierInvoice.CreteTime,IsYuFu,SupplierProNo,IsPayStatus as PayStatus,ActPay,FuShuTotal
 ,CG_POOrder.AE,CG_POOrder.GuestName,RePayClear,CAI_POCai.CaiFpType,CAI_POCai.IsHanShui,
 IsHeBing,TB_SupplierInvoice.CreteTime AS T,SumActPay
  ,CAI_OrderInHouses.CaiLastTruePrice as LastPay,GuestType,GuestPro  from 
 TB_TempSupplierInvoice  
 left join TB_SupplierInvoices  ON TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds     
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_TempSupplierInvoice.SupplierInvoiceId
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id
left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId 
left join (select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where status<>'不通过' group by OrderCheckIds )
as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids
left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO and CG_POOrder.Status='通过' and IFZhui=0
left join CAI_OrderChecks on CAI_OrderChecks.Ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
left join CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
where SupplierAdvanceId=0
union all
select BusType as CaiBusType,SupplierAdvanceId,TB_SupplierAdvancePayment.CreateName, '预' as busType,
TB_SupplierAdvancePayment.ProNo as InvProNo, 
TB_SupplierAdvancePayment.Id as payId,TB_SupplierInvoices.Ids as payIds,CAI_OrderInHouses.Ids,
CAI_OrderInHouse.ProNo,'' as CaiProNo,RuTime,CAI_OrderInHouse.Supplier,houseName,CAI_OrderInHouse.PONo,
CAI_OrderInHouse.POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum
,GoodPrice,TB_SupplierAdvancePayment.CreateName as DoPer,SupplierFPNo,ChcekProNo,TB_SupplierAdvancePayment.Status,
SupplierInvoiceNum,SupplierInvoicePrice,SupplierInvoiceDate,SupplierInvoiceTotal,TB_SupplierAdvancePayment.CreteTime,IsYuFu,SupplierProNo,IsPayStatus as PayStatus,ActPay,FuShuTotal
 ,CG_POOrder.AE,CG_POOrder.GuestName,RePayClear,CAI_POCai.CaiFpType,CAI_POCai.IsHanShui,
 IsHeBing,TB_SupplierAdvancePayment.CreteTime AS T,SumActPay 
  ,CAI_OrderInHouses.CaiLastTruePrice as LastPay,GuestType,GuestPro from 
 TB_TempSupplierInvoice  
 left join TB_SupplierInvoices  ON TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds 
left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayment.id=TB_TempSupplierInvoice.SupplierAdvanceId
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id
left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId 
left join (select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where status<>'不通过' group by OrderCheckIds )
as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids
left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO and CG_POOrder.Status='通过' and IFZhui=0
left join CAI_OrderChecks on CAI_OrderChecks.Ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
left join CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
where SupplierInvoiceId=0
) as TB");
            }
            else if (type == "11")//实际提交已扣除的部分,全额采退+部分采退（不生成事后需要扣回的部分）
            {
                strSql.Append(@"select * from (
select BusType as CaiBusType,TB_SupplierInvoice.CreateName,'支' as busType,TB_SupplierInvoice.ProNo as InvProNo, TB_SupplierInvoices.Id as payId,TB_SupplierInvoices.Ids as payIds,CAI_OrderInHouses.Ids,
CAI_OrderInHouse.ProNo,'' as CaiProNo,RuTime,CAI_OrderInHouse.Supplier,houseName,CAI_OrderInHouse.PONo,CAI_OrderInHouse.POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum
,GoodPrice,TB_SupplierInvoice.CreateName as DoPer,SupplierFPNo,ChcekProNo,TB_SupplierInvoice.Status,SupplierInvoiceNum,SupplierInvoicePrice,SupplierInvoiceDate,SupplierInvoiceTotal,TB_SupplierInvoice.CreteTime,IsYuFu,SupplierProNo,IsPayStatus as PayStatus,ActPay,FuShuTotal
 ,CG_POOrder.AE,CG_POOrder.GuestName,RePayClear,CAI_POCai.CaiFpType,CAI_POCai.IsHanShui,IsHeBing,TB_SupplierInvoice.CreteTime AS T,SumActPay 
  ,CAI_OrderInHouses.CaiLastTruePrice as LastPay,GuestType,GuestPro  from  TB_SupplierInvoices  
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id
left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId 
left join (select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where status<>'不通过' group by OrderCheckIds )
as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids
left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO and CG_POOrder.Status='通过' and IFZhui=0
left join CAI_OrderChecks on CAI_OrderChecks.Ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
left join CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
) as TB");
            }
            else
            {
                strSql.Append(@"select * from (
select BusType as CaiBusType,TB_SupplierInvoice.CreateName,'支' as busType,TB_SupplierInvoice.ProNo as InvProNo, TB_SupplierInvoices.Id as payId,TB_SupplierInvoices.Ids as payIds,CAI_OrderInHouses.Ids,
CAI_OrderInHouse.ProNo,'' as CaiProNo,RuTime,CAI_OrderInHouse.Supplier,houseName,CAI_OrderInHouse.PONo,CAI_OrderInHouse.POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum
,GoodPrice,TB_SupplierInvoice.CreateName as DoPer,SupplierFPNo,ChcekProNo,TB_SupplierInvoice.Status,SupplierInvoiceNum,SupplierInvoicePrice,SupplierInvoiceDate,SupplierInvoiceTotal,TB_SupplierInvoice.CreteTime,IsYuFu,SupplierProNo,IsPayStatus as PayStatus,ActPay,FuShuTotal
 ,CG_POOrder.AE,CG_POOrder.GuestName,RePayClear,CAI_POCai.CaiFpType,CAI_POCai.IsHanShui,IsHeBing,TB_SupplierInvoice.CreteTime AS T,SumActPay 
  ,CAI_OrderInHouses.CaiLastTruePrice as LastPay,GuestType,GuestPro  from  TB_SupplierInvoices  
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id
left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId 
left join (select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where status<>'不通过' group by OrderCheckIds )
as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids
left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO and CG_POOrder.Status='通过' and IFZhui=0
left join CAI_OrderChecks on CAI_OrderChecks.Ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
left join CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
union all
select BusType as CaiBusType,TB_SupplierAdvancePayment.CreateName,'预' as busType,TB_SupplierAdvancePayment.ProNo as InvProNo,TB_SupplierAdvancePayments.Id as payId,TB_SupplierAdvancePayments.Ids as payIds,CAI_POCai.Ids,'' as ProNo,CAI_POOrder.ProNo as CaiProNo,null as RuTime ,lastSupplier as Supplier, '' as houseName,
CAI_POOrder.PONo,CAI_POOrder.POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,Num as GoodNum,0 as supplierTuiGoodNum,
lastPrice as GoodPrice,TB_SupplierAdvancePayment.CreateName as DoPer,SupplierFPNo,'' as ChcekProNo,TB_SupplierAdvancePayment.Status,SupplierInvoiceNum,SupplierInvoicePrice,
SupplierInvoiceDate,SupplierInvoiceTotal,TB_SupplierAdvancePayment.CreteTime,0 as IsYuFu,SupplierProNo,PayStatus,SupplierInvoiceTotal as ActPay,0 as FuShuTotal
,CAI_POOrder.AE,CAI_POOrder.GuestName,-1 as RePayClear,CAI_POCai.CaiFpType,CAI_POCai.IsHanShui,-1 as IsHeBing,TB_SupplierAdvancePayment.CreteTime AS T,SumActPay 
  ,CAI_POCai.LastTruePrice as LastPay,GuestType,GuestPro  from TB_SupplierAdvancePayment 
left join TB_SupplierAdvancePayments on  TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id 
left join CAI_POCai  on  TB_SupplierAdvancePayments.CaiIds=CAI_POCai.ids
left join CAI_POOrder  on   CAI_POCai.id=CAI_POOrder.id  
left join TB_Good on TB_Good.GoodId=CAI_POCai.GoodId 
left join CG_POOrder on CG_POOrder.PONO=CAI_POOrder.PONO and CG_POOrder.Status='通过' and IFZhui=0
) as TB  ");
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            //strSql.Append(" order by T desc ");
            List<SupplierToInvoiceView> list = new List<SupplierToInvoiceView>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBindSupplierInvoice(dataReader);
                        object ojb = dataReader["CaiFpType"]; ;

                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiFpType = ojb.ToString();
                        }
                        ojb = dataReader["IsHanShui"]; ;

                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsHanShui = Convert.ToInt32(ojb) == 0 ? false : true;
                        }

                        model.CaiProNo = dataReader["CaiProNo"].ToString();
                        ojb = dataReader["IsHeBing"];

                        model.T = Convert.ToDateTime(dataReader["T"]);

                        if (ojb != null && ojb != DBNull.Value)
                        {
                            if (Convert.ToInt32(ojb) == 0)
                            {
                                model.IsHeBingString = "不合并";
                            }
                            else if (Convert.ToInt32(ojb) == 1)
                            {
                                model.IsHeBingString = "合并";

                            }

                        }
                        ojb = dataReader["LastPay"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            var LastPay = Convert.ToDecimal(ojb);
                            if (LastPay != 0)
                            {
                                model.LastPayTotal = model.SupplierInvoiceTotal * model.GoodPrice / LastPay;
                            }
                        }

                        ojb = dataReader["GuestType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestType = ojb.ToString();
                        }
                        ojb = dataReader["GuestPro"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestPro =Convert.ToInt32(ojb);
                        }
                        list.Add(model);
                    }
                }
            }

            list.Sort(Compare);
            return list;
        }
        public int Compare(SupplierToInvoiceView x, SupplierToInvoiceView y)
        {
            return y.T.CompareTo(x.T);
        }
        public List<SupplierToInvoiceView> GetSupplierInvoiceListToDiXiao(int SupplierInvoiceId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select * from (
select '支' as busType,TB_SupplierInvoice.ProNo as InvProNo, TB_SupplierInvoices.Id as payId,TB_SupplierInvoices.Ids as payIds,CAI_OrderInHouses.Ids,
CAI_OrderInHouse.ProNo,RuTime,CAI_OrderInHouse.Supplier,houseName,CAI_OrderInHouse.PONo,CAI_OrderInHouse.POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum
,GoodPrice,TB_SupplierInvoice.CreateName as DoPer,SupplierFPNo,ChcekProNo,TB_SupplierInvoice.Status,SupplierInvoiceNum,SupplierInvoicePrice,SupplierInvoiceDate,SupplierInvoiceTotal,TB_SupplierInvoice.CreteTime,IsYuFu,SupplierProNo,IsPayStatus as PayStatus,ActPay,FuShuTotal
 ,CG_POOrder.AE,CG_POOrder.GuestName,RePayClear,CAI_POCai.CaiFpType,CAI_POCai.IsHanShui,IsHeBing,TB_SupplierInvoice.CreteTime AS T from  TB_SupplierInvoices  
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id
left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId 
left join (select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where status<>'不通过' group by OrderCheckIds )
as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids
left join CG_POOrder on CG_POOrder.PONO=CAI_OrderInHouse.PONO and CG_POOrder.Status='通过' and IFZhui=0
left join CAI_OrderChecks on CAI_OrderChecks.Ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
left join CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id 
where TB_SupplierInvoices.ids in (select SupplierInvoiceIds from TB_TempSupplierInvoice where SupplierInvoiceId={0})) as TB  ", SupplierInvoiceId);

            strSql.Append(" order by T desc ");
            List<SupplierToInvoiceView> list = new List<SupplierToInvoiceView>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBindSupplierInvoice(dataReader);
                        object ojb = dataReader["CaiFpType"]; ;

                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.CaiFpType = ojb.ToString();
                        }
                        ojb = dataReader["IsHanShui"]; ;

                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsHanShui = Convert.ToInt32(ojb) == 0 ? false : true;
                        }
                        ojb = dataReader["IsHeBing"]; ;

                        if (ojb != null && ojb != DBNull.Value)
                        {
                            if (Convert.ToInt32(ojb) == 0)
                            {
                                model.IsHeBingString = "不合并";
                            }
                            else if (Convert.ToInt32(ojb) == 1)
                            {
                                model.IsHeBingString = "合并";

                            }

                        }

                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<SupplierToInvoiceView> GetSupplierInvoiceListToAllFp(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            //            strSql.Append(@"select '支' as busType,TB_SupplierInvoice.ProNo as InvProNo, TB_SupplierInvoices.Id as payId,TB_SupplierInvoices.Ids as payIds,CAI_OrderInHouses.Ids,
            //CAI_OrderInHouse.ProNo,RuTime,Supplier,houseName,CAI_OrderInHouse.PONo,CAI_OrderInHouse.POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum
            //,GoodPrice,TB_SupplierInvoice.CreateName as DoPer,SupplierFPNo,ChcekProNo,TB_SupplierInvoice.Status,SupplierInvoiceNum,SupplierInvoicePrice,SupplierInvoiceDate,SupplierInvoiceTotal,TB_SupplierInvoice.CreteTime,IsYuFu,SupplierProNo,PayStatus
            // ,tb3.AE,tb3.GuestName,IsHanShui from  TB_SupplierInvoices  
            //left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.Id
            //left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
            //left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
            //left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id
            //left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId 
            //left join (select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where status<>'不通过' group by OrderCheckIds )
            //as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids
            //left join 
            //(
            //select AE,CAI_POOrder.GuestName,PONo,GoodId,min(IsHanShui) as IsHanShui from CAI_POOrder left join CAI_POCai on CAI_POOrder.id=CAI_POCai.id
            //where Status='通过'
            //group by PONo, GoodId,AE,CAI_POOrder.GuestName
            //)
            //as tb3  on tb3.PONO=CAI_OrderInHouse.PONO and tb3.GoodId=CAI_OrderInHouses.GooId ");
            strSql.Append(@"select '支' as busType,TB_SupplierInvoice.ProNo as InvProNo, TB_SupplierInvoices.Id as payId,TB_SupplierInvoices.Ids as payIds,CAI_OrderInHouses.Ids,
CAI_OrderInHouse.ProNo,RuTime,CAI_OrderInHouse.Supplier,houseName,CAI_OrderInHouse.PONo,CAI_OrderInHouse.POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum
,GoodPrice,TB_SupplierInvoice.CreateName as DoPer,SupplierFPNo,ChcekProNo,TB_SupplierInvoice.Status,SupplierInvoiceNum,SupplierInvoicePrice,SupplierInvoiceDate,SupplierInvoiceTotal,TB_SupplierInvoice.CreteTime,IsYuFu,SupplierProNo,CAI_OrderInHouses.PayStatus
,CAI_POOrder.AE,CAI_POOrder.GuestName
 ,IsHanShui from  TB_SupplierInvoices  
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id
left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId 
left join (select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where status<>'不通过' group by OrderCheckIds )
as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids
left join CAI_OrderChecks on CAI_OrderChecks.Ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
left join CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by payIds desc ");
            List<SupplierToInvoiceView> list = new List<SupplierToInvoiceView>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(ReaderBindSupplierInvoiceToAllFp(dataReader));
                    }
                }
            }
            return list;
        }

        public SupplierToInvoiceView ReaderBindSupplierInvoiceToAllFp(IDataReader dataReader)
        {
            SupplierToInvoiceView model = new SupplierToInvoiceView();
            object ojb;
            model.busType = Convert.ToString(dataReader["busType"]);
            model.payId = Convert.ToInt32(dataReader["payId"]);
            model.payIds = Convert.ToInt32(dataReader["payIds"]);
            model.InvProNo = Convert.ToString(dataReader["InvProNo"]);
            model.CreteTime = Convert.ToDateTime(dataReader["CreteTime"]);

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
            ojb = dataReader["PayStatus"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoiceStatues = (int)ojb;
                if (model.SupplierInvoiceStatues.Value == 0)
                {
                    model.isZhiFu = "未支付";
                }
                if (model.SupplierInvoiceStatues.Value == 1)
                {
                    model.isZhiFu = "拟支付";
                }
                if (model.SupplierInvoiceStatues.Value == 2)
                {
                    model.isZhiFu = "已支付";
                }
            }
            else
            {
                model.isZhiFu = "未支付";
            }
            ojb = dataReader["SupplierInvoiceDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoiceDate = (DateTime)ojb;
            }
            model.PayType_Id = model.busType + "_" + model.payId;
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
            model.IsYuFu = Convert.ToBoolean(dataReader["IsYuFu"]);
            model.SupplierProNo = Convert.ToString(dataReader["SupplierProNo"]);
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
            ojb = dataReader["IsHanShui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsHanShui = Convert.ToInt32(ojb) == 0 ? false : true;
            }
            return model;
        }
        public SupplierToInvoiceView ReaderBindSupplierInvoice(IDataReader dataReader)
        {
            SupplierToInvoiceView model = new SupplierToInvoiceView();
            object ojb;
            model.busType = Convert.ToString(dataReader["busType"]);
            model.payId = Convert.ToInt32(dataReader["payId"]);
            model.payIds = Convert.ToInt32(dataReader["payIds"]);
            model.InvProNo = Convert.ToString(dataReader["InvProNo"]);
            model.CreteTime = Convert.ToDateTime(dataReader["CreteTime"]);

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
            ojb = dataReader["PayStatus"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoiceStatues = (int)ojb;
                if (model.SupplierInvoiceStatues.Value == 0)
                {
                    model.isZhiFu = "未支付";
                }
                if (model.SupplierInvoiceStatues.Value == 1)
                {
                    model.isZhiFu = "未付清";
                }
                if (model.SupplierInvoiceStatues.Value == 2)
                {
                    model.isZhiFu = "已支付";
                }
            }
            else
            {
                model.isZhiFu = "未支付";
            }
            ojb = dataReader["SupplierInvoiceDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SupplierInvoiceDate = (DateTime)ojb;
            }
            model.PayType_Id = model.busType + "_" + model.payId;
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
            model.IsYuFu = Convert.ToBoolean(dataReader["IsYuFu"]);
            model.SupplierProNo = Convert.ToString(dataReader["SupplierProNo"]);
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
            ojb = dataReader["ActPay"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.ActPay = (decimal)ojb;
            }
            ojb = dataReader["FuShuTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FuShuTotal = (decimal)ojb;
            }
            ojb = dataReader["RePayClear"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RePayClear = (int)ojb;
                ///// 任何记录的 结清 初始 都是 0
                /////事前退货的支付单 审批通过 结清 =1   就是6.A
                /////事后退货的支付单，审批通过 实际支付为负数 的 结清=2  就是6.B
                /////事后扣减成功 ，审批通过，这几条实际支付为负数的 记录 的结清=3  就是6.C
                /////
                /////邮件的正文已经修改正确了，见下面。
                /////结清=3的记录其实就是结清=2的记录 ，因此结清=3 的记录不用参加 1-4的逻辑状态运算。
                if (model.RePayClear == 0)
                {
                    model.RePayClearString = "初始";
                }
                else if (model.RePayClear == 1)
                {
                    model.RePayClearString = "结清";
                }
                else if (model.RePayClear == 2)
                {
                    model.RePayClearString = "未结清";
                }
            }


            return model;
        }
    }
}
