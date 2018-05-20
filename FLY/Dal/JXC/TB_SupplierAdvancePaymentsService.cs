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
    public class TB_SupplierAdvancePaymentsService
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(VAN_OA.Model.JXC.TB_SupplierAdvancePayments model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            if (model.Id != null)
            {
                strSql1.Append("Id,");
                strSql2.Append("" + model.Id + ",");
            }
            if (model.CaiIds != null)
            {
                strSql1.Append("CaiIds,");
                strSql2.Append("" + model.CaiIds + ",");
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
            strSql1.Append("SupplierProNo,");
            strSql2.Append("'" + model.SupplierProNo + "',");           
            
            strSql.Append("insert into TB_SupplierAdvancePayments(");
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
        public void Update(VAN_OA.Model.JXC.TB_SupplierAdvancePayments model, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update TB_SupplierAdvancePayments set ");
            //if (model.Id != null)
            //{
            //    strSql.Append("Id=" + model.Id + ",");
            //}
            //if (model.CaiIds != null)
            //{
            //    strSql.Append("CaiIds=" + model.CaiIds + ",");
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
            strSql.Append("delete from TB_SupplierAdvancePayments ");
            strSql.Append(" where Ids=" + ids + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void DeleteByIds(string ids, SqlCommand objCommand)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_SupplierAdvancePayments ");
            strSql.Append(" where Ids in(" + ids + ")");
            objCommand.CommandText = strSql.ToString();
            objCommand.ExecuteNonQuery();
        }


        /// 删除一条数据
        /// </summary>
        public void DeleteById(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TB_SupplierAdvancePayments ");
            strSql.Append(" where id=" + id + "");
            DBHelp.ExeCommand(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
//        public VAN_OA.Model.JXC.SupplierToInvoiceView GetModel(int Ids)
//        {
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append(@"select TB_SupplierInfo.SupplierName,CAI_OrderInHouses.Ids,CAI_OrderInHouse.ProNo,RuTime,GuestName,houseName,PONo,POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,GoodNum,supplierTuiGoodNum,GoodPrice,DoPer,SupplierFPNo,ChcekProNo,CAI_OrderInHouse.Status,SupplierInvoiceStatues,SupplierInvoiceDate,SupplierInvoiceTotal
// from  TB_SupplierAdvancePayments  
//left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id
//left join CAI_OrderInHouses  on  TB_SupplierAdvancePayments.RuIds= CAI_OrderInHouses.Ids 
//left join CAI_OrderInHouse  on CAI_OrderInHouses.id=CAI_OrderInHouse.id 
//left join TB_HouseInfo on CAI_OrderInHouse.HouseID=TB_HouseInfo.id
//left join TB_Good on TB_Good.GoodId=CAI_OrderInHouses.GooId 
//left join (select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses group by OrderCheckIds )
//as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids left join TB_SupplierInfo on TB_SupplierInfo.SupplieSimpeName=lastSupplier ");
//            strSql.Append(" where TB_SupplierAdvancePayments.Ids=" + Ids + "");

//            VAN_OA.Model.JXC.SupplierToInvoiceView model = null;
//            using (SqlConnection conn = DBHelp.getConn())
//            {
//                conn.Open();
//                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
//                using (SqlDataReader dataReader = objCommand.ExecuteReader())
//                {
//                    if (dataReader.Read())
//                    {
//                        model = ReaderBind(dataReader);
//                    }
//                }
//            }
//            return model;
//        }




        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<VAN_OA.Model.JXC.SupplierToInvoiceView> GetListArray(string strWhere)
        {
            // LastTruePrice=lastPrice 修改
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select SumPOTotal,InvoiceTotal,TB_SupplierInfo.SupplierName,CAI_POCai.ids,CAI_POOrder.ProNo,lastSupplier,
CAI_POOrder.PONo,POName,GoodNo,GoodName,GoodTypeSmName,GoodSpec,GoodUnit,Num,
LastTruePrice as lastPrice,SupplierFPNo,CAI_POOrder.Status,SupplierInvoiceNum,SupplierInvoicePrice,
SupplierInvoiceDate,SupplierInvoiceTotal  ,TB_SupplierAdvancePayments.ids as PayIds,SupplierProNo,AE,CAI_POOrder.GuestName ,HadSupplierInvoiceTotal 
,IsHanShui from TB_SupplierAdvancePayment 
left join TB_SupplierAdvancePayments on  TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id 
left join CAI_POCai  on  TB_SupplierAdvancePayments.CaiIds=CAI_POCai.ids
left join CAI_POOrder  on   CAI_POCai.id=CAI_POOrder.id  
left join TB_Good on TB_Good.GoodId=CAI_POCai.GoodId 
left join 
(
select CaiIds,Sum(SupplierInvoiceTotal) as  HadSupplierInvoiceTotal from 
TB_SupplierAdvancePayments 
left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.id
where status='通过' 
group by CaiIds
) as tb2 on tb2.CaiIds=CAI_POCai.ids left join TB_SupplierInfo on TB_SupplierInfo.SupplieSimpeName=lastSupplier and TB_SupplierInfo.status='通过'
left join [PO_InvoiceTotal_SumView] on [PO_InvoiceTotal_SumView].PONo=CAI_POOrder.PONO");

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

                        decimal SumPOTotal = 0;
                        object ojb = dataReader["SumPOTotal"];
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
            model.GuestName = dataReader["lastSupplier"].ToString();
            model.SupplierAllName = dataReader["SupplierName"].ToString();
            
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
                model.SupplierInvoiceTotal = (decimal)ojb;
            }

            decimal GoodNum = model.GoodNum ?? 0;
            decimal supplierTuiGoodNum = model.supplierTuiGoodNum ?? 0;
            model.lastGoodNum = GoodNum - supplierTuiGoodNum;

            ojb = dataReader["lastPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodPrice = (decimal)ojb;
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

            ojb = dataReader["IsHanShui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsHanShui = Convert.ToInt32(ojb) == 0 ? false : true;
            }
            return model;
        }


    }
}
