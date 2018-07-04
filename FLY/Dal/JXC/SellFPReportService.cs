using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using VAN_OA.Model.JXC;
using System.Data.SqlClient;
using System.Data;

namespace VAN_OA.Dal.JXC
{
    public class SellFPReportService
    {
        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<SellFPReport> GetListArray(string sql)
        {
//            StringBuilder strSql = new StringBuilder();
//            //strSql.Append("select PONo,AE,GuestName,GoodId,GoodNo,GoodName,GoodSpec,totalNum,avgSellPrice,avgLastPrice,POTotal,PODate,FPTotal,hadFpTotal,RuTime,GoodSellPrice,diffDate ");
//            //strSql.Append(" FROM SellFPReport ");
//            strSql.AppendFormat(@"select tb1.*, avgLastPrice,POTotal_View.POTotal,CG_POOrder.PODate,CG_POOrder.FPTotal,hadFpTotal,RuTime,GoodSellPrice,diffDate from (
//select  CG_POOrder.PONo,CG_POOrder.AE,CG_POOrder.GuestName,CG_POOrders.GoodId,TB_Good.GoodNo, TB_Good.GoodName,
//TB_Good.GoodSpec,sum(Num) as totalNum, avg(SellPrice) avgSellPrice,AppName  from CG_POOrder
//left join CG_POOrders on CG_POOrder.id=CG_POOrders.id
//left join TB_Good on TB_Good.GoodId=CG_POOrders.GoodId
//where  TB_Good.GoodName is not null and Status='通过' and (POStatue2='' or POStatue2 is null)  
//group by CG_POOrder.PONo,CG_POOrder.AE,CG_POOrder.GuestName,TB_Good.GoodNo, TB_Good.GoodName,
//TB_Good.GoodSpec,CG_POOrders.GoodId,AppName
//)as tb1-- 项目基本信息汇总
//left join --采购均价
//(
//select  PONo,GoodId,AVG(lastPrice) as avgLastPrice from CAI_POOrder
//left join CAI_POCai  on CAI_POOrder.Id=CAI_POCai.Id
//where Status='通过' and lastPrice is not null
//group by PONo,GoodId
//) as tb2-- 查询采购均价
//on tb1.PONo=tb2.PONo and tb1.GoodId=tb2.GoodId
//left join POTotal_View on POTotal_View.PONo=tb1.PONo--查询 项目金额和 已开发票金额
//left join CG_POOrder on CG_POOrder.PONo=tb1.PONo and CG_POOrder.IFZhui=0 and Status='通过' --找发票号和原始订单日期
//left join 
//(
//select PONo, RuTime,GooId ,GoodSellPrice, DATEDIFF(day,ruTime,getdate()) as diffDate FROM Sell_OrderOutHouse 
//left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id where Status='通过'
//)
//as tb3 on tb1.PONo=tb3.PONo and tb1.GoodId=tb3.GooId");
//            if (strWhere.Trim() != "")
//            {
//                strSql.Append(" where " + strWhere);
//            }
            List<SellFPReport> list = new List<SellFPReport>();      
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
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
        public SellFPReport ReaderBind(IDataReader dataReader)
        {
            SellFPReport model = new SellFPReport();
            object ojb;
            model.GoodTypeSmName = dataReader["GoodTypeSmName"].ToString();
            model.PONo = dataReader["PONo"].ToString();
            model.AE = dataReader["AE"].ToString();
            model.GuestName = dataReader["GuestName"].ToString();
            ojb = dataReader["GoodId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodId = (int)ojb;
            }
            model.GoodNo = dataReader["GoodNo"].ToString();
            model.GoodName = dataReader["GoodName"].ToString();
            model.GoodSpec = dataReader["GoodSpec"].ToString();
            ojb = dataReader["GoodNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.totalNum = (decimal)ojb;
            }
            
            ojb = dataReader["avgSellPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.avgSellPrice = (decimal)ojb;
            }
            ojb = dataReader["avgLastPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.avgLastPrice = (decimal)ojb;
            }
            ojb = dataReader["POTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POTotal = (decimal)ojb;
            }
            ojb = dataReader["PODate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PODate = (DateTime)ojb;
            }
            model.FPTotal = dataReader["FPTotal"].ToString();
            ojb = dataReader["hadFpTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.hadFpTotal = (decimal)ojb;
            }
            ojb = dataReader["RuTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RuTime = (DateTime)ojb;
            }
            ojb = dataReader["GoodSellPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodSellPrice = (decimal)ojb;
            }
            ojb = dataReader["diffDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.diffDate = (int)ojb;
            }

            ojb = dataReader["diffDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.diffDate = (int)ojb;
            }

            ojb = dataReader["outProNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OutProNo = ojb.ToString();
            }
            ojb = dataReader["POName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.POName = ojb.ToString();
            }
            ojb = dataReader["IsPoFax"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsPoFax = Convert.ToBoolean(ojb);
            }
            ojb = dataReader["Id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Id = Convert.ToInt32(ojb);
            }
            ojb = dataReader["SellInNums"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellInNums = Convert.ToDecimal(ojb);
            }
            model.GoodAreaNumber = dataReader["GoodAreaNumber"].ToString();

            model.TotalAvgPrice= model.avgLastPrice * model.totalNum;
            
            return model;
        }
    }
}
