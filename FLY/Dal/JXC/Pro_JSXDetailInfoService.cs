using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using VAN_OA.Model.JXC;

namespace VAN_OA.Dal.JXC
{
    public class Pro_JSXDetailInfoService
    {

        public void ReSetPro_JSXDetailInfo(List<VAN_OA.Model.JXC.Pro_JSXDetailInfo> allInfo)
        {
            var tuiList = allInfo.FindAll(t => t.TypeName == "采购退货");
            if (tuiList.Count == 0)
            {
                return;
            }
            
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();

                foreach (var model in tuiList)
                {
                    if (model.ProNo == "20170035")
                    { 
                    
                    }
                    string sql = string.Format(@"select RuIds,OutTotal,SupplierInvoiceTotal*GoodPrice/CaiLastTruePrice as InvoiceTotal,GoodNum*GoodPrice as AllTotal,FuTotal from 
CAI_OrderInHouses left join 
(
SELECT OrderCheckIds,sum(GoodNum*GoodPrice) as OutTotal,SUM(SupplierInvoiceTotal) AS FuTotal 
FROM CAI_OrderOutHouse LEFT JOIN CAI_OrderOutHouseS ON CAI_OrderOutHouse.Id=CAI_OrderOutHouses.id
LEFT JOIN 
(
select CaiTuiProNo,CAI_OrderInHouses.GooId AS GID,SupplierInvoiceTotal
 from TB_SupplierInvoice left join TB_SupplierInvoices on TB_SupplierInvoice.Id=TB_SupplierInvoices.id
left join CAI_OrderInHouses on CAI_OrderInHouses.Ids=TB_SupplierInvoices.RuIds
where TB_SupplierInvoice.Status='通过'and IsHeBing=1
) as TB
 ON TB.CaiTuiProNo=CAI_OrderOutHouse.ProNo AND TB.GID= CAI_OrderOutHouses.GooId

where Status='通过' and RuTime<='{0}' and OrderCheckIds={1}
group by OrderCheckIds
) as TB_OUT on CAI_OrderInHouses.Ids=TB_OUT.OrderCheckIds
LEFT JOIN 
(
select RuIds,sum(SupplierInvoicePrice) as InvoicePrice,sum(SupplierInvoiceTotal) as SupplierInvoiceTotal  from TB_SupplierInvoice 
left join TB_SupplierInvoices on TB_SupplierInvoice.Id=TB_SupplierInvoices.id
where (TB_SupplierInvoice.Status='通过' or (IsYuFu=1))and SupplierInvoiceTotal>0 
group by RuIds) as TB_Invoice  on TB_Invoice.RuIds=TB_OUT.OrderCheckIds where CAI_OrderInHouses.Ids={1}
", model.RuTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),model.RuId);

                    SqlCommand objCommand = new SqlCommand(sql, conn);
                    decimal OutTotal = 0;
                    decimal InvoiceTotal = 0;
                    decimal AllTotal = 0;
                    decimal FuTotal = 0;
                    using (SqlDataReader dataReader = objCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            object ojb = null;

                            ojb = dataReader["OutTotal"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                OutTotal = Convert.ToDecimal(ojb);
                            }
                            ojb = dataReader["InvoiceTotal"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                InvoiceTotal = Convert.ToDecimal(ojb);
                            }
                            ojb = dataReader["AllTotal"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                AllTotal = Convert.ToDecimal(ojb);
                            }
                            ojb = dataReader["FuTotal"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                FuTotal = Convert.ToDecimal(ojb);
                            }
                          
                        }
                    }

                    var X = AllTotal - OutTotal - InvoiceTotal;
                    var Y = AllTotal - InvoiceTotal;
                    if (X > 0)
                    {
                        //如果>0,  则 采退的已支付=0，未支付=负数的( Y+合计通过的早于本采退单的采退单（含本身这条采退单的）的负数的未支付总值-X)

                        model.HadInvoice = 0;
                        //model.NoInvoice = -(Y+FuTotal-X);

                        model.NoInvoice = -(model.GoodOutNum * model.Price);
                    }
                    else
                    {
                        //如果<=0, 则  采退的已支付=X-（合计的对应该入库单号的通过的早于本采退单的采退单（含本身这条采退单的）的负数已支付 总值） ,未支付=0

                        //if (-model.GoodOutNum * model.Price > X)
                        //{
                        //    model.NoInvoice = 0;
                        //    model.HadInvoice = -model.GoodOutNum * model.Price;
                        //}
                        //else
                        //{ 

                        //}

                        // X和退货本身比较， 如果退货本身》=X  则 已支付等于 退货本身 ，未支付=0；
                        //如果退货本身《X 则 已支付等于 X， 未支付=退货本身-X
                        if (-model.GoodOutNum * model.Price >= X)
                        {
                            model.HadInvoice = -model.GoodOutNum * model.Price;
                            model.NoInvoice = 0;
                        }
                        else
                        {
                            model.HadInvoice = X;
                            model.NoInvoice =-model.GoodOutNum * model.Price- X;

                        }
                        //model.HadInvoice = (-model.GoodOutNum * model.Price >= X) ? (-model.GoodOutNum * model.Price) : X;

                        //model.NoInvoice = -(model.GoodOutNum * model.Price + model.HadInvoice);
                    }

                }
                conn.Close();
            }
        }
        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.Pro_JSXDetailInfo> GetListArray(int houseId, int GoodId, DateTime fromTime, DateTime toTime)
        {

            decimal iniNum = 0;
            List<VAN_OA.Model.JXC.Pro_JSXDetailInfo> list = new List<VAN_OA.Model.JXC.Pro_JSXDetailInfo>();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand("Pro_JSXDetailInfo", conn);
                objCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter[] paras = new SqlParameter[4];
                paras[0] = new SqlParameter("@HouseId", houseId);
                paras[1] = new SqlParameter("@GoodId", GoodId);
                paras[2] = new SqlParameter("@FromDate", fromTime);
                paras[3] = new SqlParameter("@ToDate", toTime);
                objCommand.Parameters.AddRange(paras);
                int i = 0;
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        var model = new Pro_JSXDetailInfo();
                        object ojb = null;

                        ojb = dataReader["TypeName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.TypeName = ojb.ToString();
                        }
                        ojb = dataReader["RuTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.RuTime = Convert.ToDateTime(ojb);
                        }
                        ojb = dataReader["allRemark"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.allRemark = ojb.ToString();
                        }

                        ojb = dataReader["GoodInNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodInNum = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["GoodOutNum"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GoodOutNum = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["ProNo"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ProNo = Convert.ToString(ojb);
                        }

                        ojb = dataReader["Price"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Price = Convert.ToDecimal(ojb);
                        }

                        ojb = dataReader["PONO"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PONO = Convert.ToString(ojb);
                        }

                        ojb = dataReader["HadInvoice"];
                        if (model.TypeName == "采购入库")
                        {
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.HadInvoice = Convert.ToDecimal(ojb);// *model.GoodInNum;
                            }
                            else
                            {
                                model.HadInvoice = 0;
                            }
                            model.NoInvoice = model.GoodInNum * model.Price - model.HadInvoice;
                        }
                        //else if (model.TypeName == "采购退货")
                        //{
                        //    if (ojb != null && ojb != DBNull.Value)
                        //    {
                        //        model.HadInvoice = -model.Price * model.GoodOutNum;
                        //    }
                        //    else
                        //    {
                        //        model.HadInvoice = 0;
                        //    }
                        //    model.NoInvoice = -model.GoodOutNum * model.Price - model.HadInvoice;
                        //}

                        //if (i == 0)
                        //{
                        //    iniNum = model.GoodInNum - model.GoodOutNum;
                        //    i++;
                        //}
                        //else
                        //{
                        //    iniNum = iniNum + model.GoodInNum - model.GoodOutNum;
                        //}

                        model.GoodResultNum = iniNum;

                        model.RuId = Convert.ToInt32(dataReader["RuId"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<Sell_Cai_OrderInHouseListModel> GetListArray_New(List<Sell_Cai_OrderInHouseListModel> pOOrderList)
        {
            List<Sell_Cai_OrderInHouseListModel> pOOrderList_New = new List<Sell_Cai_OrderInHouseListModel>();
            decimal iniNum = 0;

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand("Pro_JSXDetailInfo_New", conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                foreach (var m in pOOrderList)
                {
                    objCommand.Parameters.Clear();
                    SqlParameter[] paras = new SqlParameter[4];
                    paras[0] = new SqlParameter("@HouseId", 1);
                    paras[1] = new SqlParameter("@GoodId", m.GooId);
                    paras[2] = new SqlParameter("@FromDate", m.RuTime);
                    paras[3] = new SqlParameter("@ToDate", DateTime.Now.ToString("yyyy-MM-dd"));
                    objCommand.Parameters.AddRange(paras);
                    int i = 0;
                    using (SqlDataReader dataReader = objCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var model = new Pro_JSXDetailInfo();
                            object ojb = null;

                            ojb = dataReader["GoodInNum"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.GoodInNum = Convert.ToDecimal(ojb);
                            }
                            ojb = dataReader["GoodOutNum"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.GoodOutNum = Convert.ToDecimal(ojb);
                            }
                            var t = Convert.ToInt32(dataReader["T"]);

                            if (i == 0)
                            {
                                iniNum = model.GoodInNum - model.GoodOutNum;
                                //i++;
                            }
                            else
                            {
                                iniNum = iniNum + model.GoodInNum - model.GoodOutNum;
                            }
                            //这个销售退货时间点后，只要有一次采购退货或销售出库后 的库存数量=0，我们就认为是正常了
                            if ((t == 2 || t == 3) && iniNum == 0)
                            {
                                m.MyColor = System.Drawing.Color.Yellow;
                                pOOrderList_New.Add(m);
                                break;
                            }

                            i++;
                        }
                    }
                }
            }
            return pOOrderList_New;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<Sell_Cai_OrderInHouseListModel> GetListArray_New_Good(List<Sell_Cai_OrderInHouseListModel> pOOrderList)
        {
            List<Sell_Cai_OrderInHouseListModel> pOOrderList_New = new List<Sell_Cai_OrderInHouseListModel>();
            List<Pro_JSXDetailInfo> allProDetail = new List<Pro_JSXDetailInfo>();
            var l = pOOrderList.Select(t => t.GooId.ToString()).ToArray();
            string goodIds = string.Join(",", l);

            string sql = string.Format(@"select * from (
select  1 as T,RuTime, GoodNum as GoodInNum,0 as GoodOutNum,GooId  from CAI_OrderInHouse left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id 

where status='通过' and HouseID=1 and GooId in ({0}) 

union  all

select 2 as T,RuTime, 0 as GoodInNum,GoodNum as GoodOutNum,GooId from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.Id 

where status='通过' and HouseID=1 and GooId in ({0}) 

union  all

select 3 as T,RuTime,0 as GoodInNum,GoodNum as GoodOutNum,GooId  from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id 

where status='通过' and HouseID=1 and GooId in ({0}) 
union  all

select 4 as T,RuTime, GoodNum as GoodInNum,0 as GoodOutNum,GooId from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.Id 

where status='通过' and HouseID=1 and GooId in ({0}) 
) as tb order by RuTime ;", goodIds);

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);


                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = new Pro_JSXDetailInfo();
                        object ojb = null;

                        ojb = dataReader["GoodInNum"];
                        model.GoodInNum = Convert.ToDecimal(ojb);

                        ojb = dataReader["GoodOutNum"];
                        model.GoodOutNum = Convert.ToDecimal(ojb);


                        ojb = dataReader["RuTime"];
                        model.RuTime = Convert.ToDateTime(ojb);

                        ojb = dataReader["GooId"];
                        model.GooId = Convert.ToInt32(ojb);

                        model.T = Convert.ToInt32(dataReader["T"]);

                        allProDetail.Add(model);

                    }
                }

            }

            foreach (var m in pOOrderList)
            {
                var goodList = allProDetail.FindAll(t => m.GooId == t.GooId);

                var count_List = goodList.FindAll(t => t.RuTime <= m.RuTime);

                var other_List = goodList.FindAll(t => t.RuTime > m.RuTime && (t.T == 2 || t.T == 3));

                var total = count_List.Sum(t => t.GoodInNum - t.GoodOutNum);

                for (int i = 0; i < other_List.Count; i++)
                {
                    count_List = goodList.FindAll(t => t.RuTime > m.RuTime && t.RuTime <= other_List[i].RuTime);
                    if (total + count_List.Sum(t => t.GoodInNum - t.GoodOutNum) == 0)
                    {
                        m.MyColor = System.Drawing.Color.Yellow;
                        pOOrderList_New.Add(m);
                        break;
                    }
                }

            }
            return pOOrderList_New;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<Sell_Cai_OrderInHouseListModel> GetListArray_New_Good_Out(List<Sell_Cai_OrderInHouseListModel> pOOrderList)
        {
            List<Sell_Cai_OrderInHouseListModel> pOOrderList_New = new List<Sell_Cai_OrderInHouseListModel>();
            List<Pro_JSXDetailInfo> allProDetail = new List<Pro_JSXDetailInfo>();
            var l = pOOrderList.Select(t => t.GooId.ToString()).ToArray();
            string goodIds = string.Join(",", l);

            string sql = string.Format(@"select * from (
select  1 as T,RuTime, GoodNum as GoodInNum,0 as GoodOutNum,GooId  from CAI_OrderInHouse left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.Id 

where status='通过' and HouseID=1 and GooId in ({0}) 

union  all

select 2 as T,RuTime, 0 as GoodInNum,GoodNum as GoodOutNum,GooId from CAI_OrderOutHouse left join CAI_OrderOutHouses on CAI_OrderOutHouse.Id=CAI_OrderOutHouses.Id 

where status='通过' and HouseID=1 and GooId in ({0}) 

union  all

select 3 as T,RuTime,0 as GoodInNum,GoodNum as GoodOutNum,GooId  from Sell_OrderOutHouse left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.Id 

where status='通过' and HouseID=1 and GooId in ({0}) 
union  all

select 4 as T,RuTime, GoodNum as GoodInNum,0 as GoodOutNum,GooId from Sell_OrderInHouse left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.Id 

where status='通过' and HouseID=1 and GooId in ({0}) 
) as tb order by RuTime ;", goodIds);

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);


                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = new Pro_JSXDetailInfo();
                        object ojb = null;

                        ojb = dataReader["GoodInNum"];
                        model.GoodInNum = Convert.ToDecimal(ojb);

                        ojb = dataReader["GoodOutNum"];
                        model.GoodOutNum = Convert.ToDecimal(ojb);


                        ojb = dataReader["RuTime"];
                        model.RuTime = Convert.ToDateTime(ojb);

                        ojb = dataReader["GooId"];
                        model.GooId = Convert.ToInt32(ojb);

                        model.T = Convert.ToInt32(dataReader["T"]);

                        allProDetail.Add(model);

                    }
                }

            }

            foreach (var m in pOOrderList)
            {
                var goodList = allProDetail.FindAll(t => m.GooId == t.GooId);

                var count_List = goodList.FindAll(t => t.RuTime <= m.RuTime);

                var other_List = goodList.FindAll(t => t.RuTime > m.RuTime && (t.T == 2 || t.T == 3));

                var total = count_List.Sum(t => t.GoodInNum - t.GoodOutNum);
                var result = false;
                for (int i = 0; i < other_List.Count; i++)
                {
                    count_List = goodList.FindAll(t => t.RuTime > m.RuTime && t.RuTime <= other_List[i].RuTime);
                    if (total + count_List.Sum(t => t.GoodInNum - t.GoodOutNum) == 0)
                    {
                        result = true;
                        break;
                    }
                }
                if (result == false)
                {
                    m.MyColor = System.Drawing.Color.Red;
                    pOOrderList_New.Add(m);
                }

            }
            return pOOrderList_New;
        }


        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<Sell_Cai_OrderInHouseListModel> GetListArray_New_Out(List<Sell_Cai_OrderInHouseListModel> pOOrderList)
        {
            List<Sell_Cai_OrderInHouseListModel> pOOrderList_New = new List<Sell_Cai_OrderInHouseListModel>();
            decimal iniNum = 0;

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand("Pro_JSXDetailInfo_New", conn);
                objCommand.CommandType = CommandType.StoredProcedure;
                foreach (var m in pOOrderList)
                {
                    objCommand.Parameters.Clear();
                    SqlParameter[] paras = new SqlParameter[4];
                    paras[0] = new SqlParameter("@HouseId", 1);
                    paras[1] = new SqlParameter("@GoodId", m.GooId);
                    paras[2] = new SqlParameter("@FromDate", m.RuTime);
                    paras[3] = new SqlParameter("@ToDate", DateTime.Now.ToString("yyyy-MM-dd"));
                    objCommand.Parameters.AddRange(paras);
                    bool result = false;
                    int i = 0;
                    using (SqlDataReader dataReader = objCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var model = new Pro_JSXDetailInfo();
                            object ojb = null;

                            ojb = dataReader["GoodInNum"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.GoodInNum = Convert.ToDecimal(ojb);
                            }
                            ojb = dataReader["GoodOutNum"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                model.GoodOutNum = Convert.ToDecimal(ojb);
                            }
                            var t = Convert.ToInt32(dataReader["T"]);

                            if (i == 0)
                            {
                                iniNum = model.GoodInNum - model.GoodOutNum;
                                //i++;
                            }
                            else
                            {
                                iniNum = iniNum + model.GoodInNum - model.GoodOutNum;
                            }
                            //这个销售退货时间点后，只要有一次采购退货或销售出库后 的库存数量=0，我们就认为是正常了
                            if ((t == 2 || t == 3) && iniNum == 0)
                            {
                                result = true;

                                break;
                            }
                            i++;
                        }

                    }
                    if (result == false)
                    {
                        m.MyColor = System.Drawing.Color.Red;
                        pOOrderList_New.Add(m);
                    }
                }
            }
            return pOOrderList_New;
        }

    }
}
