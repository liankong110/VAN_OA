using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using VAN_OA.Model.JXC;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace VAN_OA.Dal.JXC
{
    public class RuSellReportService
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ponos"></param>
        /// <param name="goodIds"></param>
        /// <returns></returns>
        public List<CaiPoNo> GetCaiPoNo(string ponos, string goodIds)
        {
            List<CaiPoNo> list = new List<CaiPoNo>();
            if (string.IsNullOrEmpty(ponos) || string.IsNullOrEmpty(goodIds))
            {
                return list;
            }
            string sql = string.Format(@"select GoodId,PONo,lastSupplier from CAI_POOrder
left join  CAI_POCai on CAI_POOrder.id=CAI_POCai.id where  Status='通过' and pono in ({0}) and goodid in ({1}) "
                , ponos, goodIds);
          
           
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        CaiPoNo model = new CaiPoNo();
                        model.GoodId = dataReader["GoodId"].ToString();
                        model.PONo = dataReader["PONo"].ToString();
                        model.lastSupplier = dataReader["lastSupplier"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<RuSellReport> GetListArray(string ponoWhere, string userId,
            string goodNoWhere, string guestWhere, string ruTimeWhere, string poTimeWhere, string where)
        {
            List<RuSellReport> list = new List<RuSellReport>();
            StringBuilder strSql = new StringBuilder();
            //            strSql.Append("select tb1.PONo,AE,GuestName,GoodId,GoodNo,GoodName,GoodSpec,GoodAvgPrice,avgSellPrice,minRuTime,minPODate,LastNum,outNum,houseNums ");           

            //            strSql.AppendFormat(@"from 
            //(
            //select CAI_OrderInHouse.PONo,GooId,MIN(RuTime) as minRuTime,avg(NoSellOutGoods.LastNum) as LastNum, avg(outNum) as outNum
            //from CAI_OrderInHouse left join CAI_OrderInHouses on CAI_OrderInHouse.Id=CAI_OrderInHouses.id
            //left join NoSellOutGoods on NoSellOutGoods.PONo=CAI_OrderInHouse.PONo and NoSellOutGoods.GoodId=CAI_OrderInHouses.GooId
            //where Status='通过'  and NoSellOutGoods.pono is not null {3} {5}
            //group by CAI_OrderInHouse.PONo,GooId
            //) as tb1 
            //left join 
            //(
            //select  CG_POOrder.PONo,CG_POOrder.AE,CG_POOrder.GuestName,CG_POOrders.GoodId,TB_Good.GoodNo, TB_Good.GoodName,
            //TB_Good.GoodSpec, avg(SellPrice) avgSellPrice, min(CG_POOrder.PODate) as minPODate ,avg(GoodNum) as houseNums,avg(GoodAvgPrice) as GoodAvgPrice  from CG_POOrder
            //left join CG_POOrders on CG_POOrder.id=CG_POOrders.id
            //left join TB_Good on TB_Good.GoodId=CG_POOrders.GoodId
            //left join TB_HouseGoods on TB_HouseGoods.GoodId=CG_POOrders.GoodId
            //where  TB_Good.GoodName is not null and Status='通过' and (POStatue2='' or POStatue2 is null) {0} {1} {2} {4}
            //group by CG_POOrder.PONo,CG_POOrder.AE,CG_POOrder.GuestName,TB_Good.GoodNo, TB_Good.GoodName,
            //TB_Good.GoodSpec,CG_POOrders.GoodId
            //)as tb2
            //on tb1.PONo=tb2.PONo and tb1.GooId=tb2.GoodId
            //where tb2.PONo is not null ", userId == "" ? "" : string.Format(" and AppName={0}", userId), goodNoWhere,
            //                           guestWhere, ruTimeWhere, poTimeWhere, ponoWhere);
            strSql.AppendFormat(@"select GoodAreaNumber,IsHanShui,PONo,AE,GuestName,NoSellOutGoods_1.GooId,GoodNo,GoodName,GoodSpec,
GoodAvgPrice,avgSellPrice,minRuTime,minPODate,LastNum,outNum,GoodNum,ruChuNum
from 
[NoSellOutGoods_1]
left join TB_Good on TB_Good.GoodId=NoSellOutGoods_1.GooId
left join TB_HouseGoods on TB_HouseGoods.GoodId=NoSellOutGoods_1.GooId where 1=1 {0} {1} {2} {3} {4} {5} " + where,
                                                                                                     userId == "" ? "" : string.Format(" and AE='{0}'", userId)
                                                                                                    , goodNoWhere,
                         guestWhere, ruTimeWhere, poTimeWhere, ponoWhere);


            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBind(dataReader);
                        model.GoodAreaNumber = dataReader["GoodAreaNumber"].ToString();
                        list.Add(model);

                      
                    }
                }
            }
            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public RuSellReport ReaderBind(IDataReader dataReader)
        {
            RuSellReport model = new RuSellReport();
            object ojb;
            model.PONo = dataReader["PONo"].ToString();
            model.AE = dataReader["AE"].ToString();
            model.GuestName = dataReader["GuestName"].ToString();
            ojb = dataReader["GooId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodId = (int)ojb;
            }
            model.GoodNo = dataReader["GoodNo"].ToString();
            model.GoodName = dataReader["GoodName"].ToString();
            model.GoodSpec = dataReader["GoodSpec"].ToString();
            ojb = dataReader["LastNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.totalNum = (decimal)ojb;
            }
            model.avgGoodPrice = 0;
            ojb = dataReader["GoodAvgPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.avgGoodPrice = (decimal)ojb;
            }
            ojb = dataReader["avgSellPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.avgSellPrice = (decimal)ojb;
            }
            ojb = dataReader["minRuTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.minRuTime = (DateTime)ojb;
            }
            ojb = dataReader["minPODate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.minPODate = (DateTime)ojb;
            }

            ojb = dataReader["outNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OutNum = (decimal)ojb;
            }

            ojb = dataReader["GoodNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HouseNum = (decimal)ojb;

            }
            ojb = dataReader["RuChuNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RuChuNum = (decimal)ojb;
            }
            ojb = dataReader["IsHanShui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsHanShui = (int)ojb;
            }
            

            return model;
        }



        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<NoSellAndCaiGoods> GetListNoSellAndCaiGoods(string ponoWhere, string userId,
            string goodNoWhere, string guestWhere, string ruTimeWhere, string poTimeWhere, string where,string company,bool isJingjian,string poNoSql)
        {
            List<NoSellAndCaiGoods> list = new List<NoSellAndCaiGoods>();
            StringBuilder strSql = new StringBuilder();

            strSql.AppendFormat(@"select GoodAreaNumber,caiAvgPrice,hanShui,allCaiNum,kuCaiNum,waiCaiNum,PONo,AE,GuestName,[NoSellAndCaiGoods].GoodId,GoodNo,GoodName,GoodSpec,
GoodAvgPrice,avgSellPrice,minRuTime,minPODate,LastNum,outNum,GoodNum,CaiDate,sellTuiNum,caiTuiNum,POName
from 
NoSellAndCaiGoods
left join TB_Good on TB_Good.GoodId=[NoSellAndCaiGoods].GoodId
left join TB_HouseGoods on TB_HouseGoods.GoodId=[NoSellAndCaiGoods].GoodId  where 1=1 {0} {1} {2} {3} {4} {5} {6} {7} order by PONo desc " + where,
                                                                                                     userId == "" ? "" : string.Format(" and AE='{0}'", userId)
                                                                                                    , goodNoWhere,
                         guestWhere, ruTimeWhere, poTimeWhere, ponoWhere, company, poNoSql);

            Hashtable sh = new Hashtable();

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = ReaderBindNoSellAndCaiGoods(dataReader);
                        model.GoodAreaNumber = dataReader["GoodAreaNumber"].ToString();

                        if (isJingjian&&model.PONo.StartsWith("KC"))
                        {
                            if (!sh.ContainsKey(model.GoodId))

                            {
                                sh.Add(model.GoodId, null);
                            }
                            else
                            {
                                continue;
                            }
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
        public NoSellAndCaiGoods ReaderBindNoSellAndCaiGoods(IDataReader dataReader)
        {
            NoSellAndCaiGoods model = new NoSellAndCaiGoods();
            object ojb;
            model.PONo = dataReader["PONo"].ToString();
            model.POName= dataReader["POName"].ToString();
            model.AE = dataReader["AE"].ToString();
            model.GuestName = dataReader["GuestName"].ToString();
            ojb = dataReader["GoodId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodId = (int)ojb;
            }
            ojb = dataReader["hanShui"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.IsHanShui = (int)ojb;
            }
            model.GoodNo = dataReader["GoodNo"].ToString();
            model.GoodName = dataReader["GoodName"].ToString();
            model.GoodSpec = dataReader["GoodSpec"].ToString();
            ojb = dataReader["LastNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.totalNum = (decimal)ojb;
            }

            //这个采库需出的库存均价 是错误的，我的想法是如果没有库存，库存均价 就应该是采购单价，你需要在程序上区分一下，按此修改吧
            model.avgGoodPrice = 0;
            ojb = dataReader["GoodAvgPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.avgGoodPrice = (decimal)ojb;
            }
            else
            {
                ojb = dataReader["caiAvgPrice"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.avgGoodPrice = Convert.ToDecimal(dataReader["caiAvgPrice"]);
                }
            }
            ojb = dataReader["avgSellPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.avgSellPrice = (decimal)ojb;
            }
            ojb = dataReader["minRuTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.minRuTime = (DateTime)ojb;
            }
            ojb = dataReader["minPODate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.minPODate = (DateTime)ojb;
            }

            ojb = dataReader["outNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OutNum = (decimal)ojb;
            }

            ojb = dataReader["GoodNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.HouseNum = (decimal)ojb;

            }
            //ojb = dataReader["RuChuNum"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.RuChuNum = (decimal)ojb;
            //}
            ojb = dataReader["CaiDate"];
            model.CaiDate = (DateTime)ojb;
            ojb = dataReader["allCaiNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.AllCaiNum = (decimal)ojb;
            }
            ojb = dataReader["kuCaiNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.KuCaiNum = (decimal)ojb;
            }
            ojb = dataReader["waiCaiNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.WaiCaiNum = (decimal)ojb;
            }
            
            ojb = dataReader["sellTuiNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SellTuiNum = (decimal)ojb;
            }
            ojb = dataReader["caiTuiNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CaiTuiNum = (decimal)ojb;
            }
            if (model.WaiCaiNum == 0)
            {
                model.CaIKuNum = model.AllCaiNum - model.OutNum;
                model.RuChuNum = 0;
            }
            else
            {
                if (model.KuCaiNum == 0)
                {
                    if (model.SellTuiNum >= model.CaiTuiNum)
                    {
                        model.CaIKuNum = 0;
                        model.RuChuNum = model.WaiCaiNum - model.OutNum;
                    }
                    else
                    {
                        model.CaIKuNum = 0;
                        model.RuChuNum = model.WaiCaiNum - model.OutNum + model.SellTuiNum - model.CaiTuiNum;
                    }
                }
                else
                {
                    if (model.SellTuiNum >= model.CaiTuiNum)
                    {
                        //入库需出数量+采库需出数量=采购来自外部数量+采购来自库存数量-所有已出库数量

                        model.RuChuNum = model.WaiCaiNum + model.KuCaiNum - model.OutNum - model.CaIKuNum;
                    }
                    else
                    {
                        //入库需出数量+采库需出数量=采购来自外部数量+采购来自库存数量-所有已出库数量+所有销售退货数量-所有采购退货数量
                        model.CaiGouNum = model.WaiCaiNum + model.KuCaiNum - model.OutNum + model.SellTuiNum - model.CaiTuiNum;
                    }
                }
            }


            //这列采购需出=入库需出+采库需出（见下方原来邮件）
            model.CaiGouNum = model.RuChuNum + model.CaIKuNum;
            return model;
        }



        public Hashtable getHT(string AE, string poNo, string company,string special)
        {
            Hashtable hs = new Hashtable();
            var sql = string.Format("select NoSellAndCaiGoods.PONo,allCaiNum,kuCaiNum,waiCaiNum,NoSellAndCaiGoods.AE,LastNum,outNum,sellTuiNum,caiTuiNum from NoSellAndCaiGoods ");
            if (special != "-1")
            {
                sql += "left join CG_POOrder on CG_POOrder.PONo=NoSellAndCaiGoods.PONo and CG_POOrder.IFZhui=0";
            }
            sql += string.Format(" where NoSellAndCaiGoods.PONo like 'P%' {0} {1} "
                , (AE != "" ? " and NoSellAndCaiGoods.ae='" + AE + "'" : ""), (poNo != "" ? " and NoSellAndCaiGoods.PONo like '%" + poNo + "%'" : ""));
            if (company != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", company.Split(',')[2]);
                sql += string.Format(" and NoSellAndCaiGoods.ae IN(select loginName from tb_User where {0})", where);
            }
            if (special != "-1")
            {
                sql += string.Format(" and CG_POOrder.IsSpecial="+special);
            }
            sql += " order by NoSellAndCaiGoods.PONo";
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Get_YueKaohe(dataReader,hs);
                    }
                }
            }

            //增加 商品现在有库存的 并且 有AE的KC项目 ，全部列举
            sql = @"select  ROW_NUMBER() OVER(PARTITION BY GooId ORDER BY CreateTime DESC) as rowid,CAI_POOrder.PONo,CAI_POOrder.AE,TB_HouseGoods.GoodId,CAI_OrderInHouse.CreateTime from
TB_HouseGoods  
left join CAI_OrderInHouses on CAI_OrderInHouses.GooId=TB_HouseGoods.GoodId
left join CAI_OrderInHouse on CAI_OrderInHouse.Id=CAI_OrderInHouses.id
left join CAI_POOrder on CAI_POOrder.PONo=CAI_OrderInHouse.PONo
where CAI_OrderInHouse.Status='通过' 
AND CAI_POOrder.AE<>'' and CAI_OrderInHouse.PONo like 'KC%'";
            if (company != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", company.Split(',')[2]);
                sql += string.Format(" and CAI_POOrder.ae IN(select loginName from tb_User where {0})", where);
            }
            if (AE != "")
            {
                sql += string.Format(" and CAI_POOrder.AE='{0}'",AE);
            }
            if (poNo != "")
            {
                sql += string.Format(" and CAI_OrderInHouse.PONo like '%{0}%'", poNo);
            }

            sql = string.Format("select * from ({0}) as temp where rowid=1 ", sql);
            List<JXCDetail> allDetailList = new List<JXCDetail>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        JXCDetail model = new JXCDetail();                        
                     
                        model.PONo = dataReader["PONo"].ToString();
                        model.AE = dataReader["AE"].ToString();
                        model.GoodId = Convert.ToInt32(dataReader["GoodId"]);
                        model.CreateTime =Convert.ToDateTime(dataReader["CreateTime"]);

                        allDetailList.Add(model);

                    }
                }
            }
            CheckHouseNum(allDetailList,hs);

            return hs;
        }


        /// <summary>
        /// 检查出库情况
        /// </summary>
        public void CheckHouseNum(List<JXCDetail> allDetailList, Hashtable hs)
        {           

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand("Pro_JSXDetailInfo1", conn);
                objCommand.CommandType = CommandType.StoredProcedure;

                foreach (var detail in allDetailList)
                {
                    objCommand.Parameters.Clear();
                    SqlParameter[] paras = new SqlParameter[4];
                    paras[0] = new SqlParameter("@HouseId", 1);
                    paras[1] = new SqlParameter("@GoodId", detail.GoodId);
                    paras[2] = new SqlParameter("@FromDate", detail.CreateTime);
                    paras[3] = new SqlParameter("@ToDate",DateTime.Now);
                    objCommand.Parameters.AddRange(paras);
                    var result = true;
                    using (SqlDataReader dataReader = objCommand.ExecuteReader())
                    {
                         
                        decimal iniNum = 0;
                        while (dataReader.Read())
                        {                            
                            var GoodInNum = Convert.ToDecimal(dataReader["GoodInNum"]);
                            var GoodOutNum = Convert.ToDecimal(dataReader["GoodOutNum"]);                            
                            iniNum =iniNum+ GoodInNum - GoodOutNum;
                            if (iniNum == 0)
                            {
                                result = false;
                                break;
                            }
                        }
                    }
                    if (result)
                    {
                        if (!hs.ContainsKey(detail.PONo))
                        {
                            hs.Add(detail.PONo,detail.AE);
                        }        
                    }
                }
            }
            
        }

        public Hashtable getHT(string where)
        {
            Hashtable hs = new Hashtable();
            var sql = string.Format("select NoSellAndCaiGoods.PONo,allCaiNum,kuCaiNum,waiCaiNum,NoSellAndCaiGoods.AE,LastNum,outNum,sellTuiNum,caiTuiNum from NoSellAndCaiGoods left join CG_POOrder on CG_POOrder.pono=NoSellAndCaiGoods.PONo and Status='通过' and IFZhui=0  where NoSellAndCaiGoods.PONo like 'P%' {0}  order by NoSellAndCaiGoods.PONo "
                , where);

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Get_YueKaohe(dataReader, hs);
                    }
                }
            }
            return hs;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public void Get_YueKaohe(IDataReader dataReader,Hashtable hs)
        {
            NoSellAndCaiGoods model = new NoSellAndCaiGoods();
            object ojb;
            model.PONo = dataReader["PONo"].ToString();
            if (!hs.Contains(model.PONo))
            {
                model.AE = dataReader["AE"].ToString();
               
                ojb = dataReader["LastNum"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.totalNum = (decimal)ojb;
                }             
               

                ojb = dataReader["outNum"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.OutNum = (decimal)ojb;
                }

                //ojb = dataReader["GoodNum"];
                //if (ojb != null && ojb != DBNull.Value)
                //{
                //    model.HouseNum = (decimal)ojb;

                //}                
                ojb = dataReader["allCaiNum"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.AllCaiNum = (decimal)ojb;
                }
                ojb = dataReader["kuCaiNum"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.KuCaiNum = (decimal)ojb;
                }
                ojb = dataReader["waiCaiNum"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.WaiCaiNum = (decimal)ojb;
                }

                ojb = dataReader["sellTuiNum"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.SellTuiNum = (decimal)ojb;
                }
                ojb = dataReader["caiTuiNum"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.CaiTuiNum = (decimal)ojb;
                }
                if (model.WaiCaiNum == 0)
                {
                    model.CaIKuNum = model.AllCaiNum - model.OutNum;
                    model.RuChuNum = 0;
                }
                else
                {
                    if (model.KuCaiNum == 0)
                    {
                        if (model.SellTuiNum >= model.CaiTuiNum)
                        {
                            model.CaIKuNum = 0;
                            model.RuChuNum = model.WaiCaiNum - model.OutNum;
                        }
                        else
                        {
                            model.CaIKuNum = 0;
                            model.RuChuNum = model.WaiCaiNum - model.OutNum + model.SellTuiNum - model.CaiTuiNum;
                        }
                    }
                    else
                    {
                        if (model.SellTuiNum >= model.CaiTuiNum)
                        {

                            model.CaiGouNum = model.AllCaiNum - model.OutNum;
                        }
                        else
                        {
                            model.CaiGouNum = model.AllCaiNum - model.OutNum + model.SellTuiNum - model.CaiTuiNum;
                        }
                    }
                }

                if (model.RuChuNum != 0 || model.CaIKuNum != 0 || model.CaiGouNum != 0)
                {
                    hs.Add(model.PONo,model.AE);
                }
            }
        }
    }
}
