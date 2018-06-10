using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using VAN_OA.Model.JXC;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.Dal.JXC
{
    public class JXC_REPORTService
    {
        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<Model.JXC.JXC_REPORT> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CG_POOrder.POName,JXC_REPORT.PONo,JXC_REPORT.ProNo,RuTime,Supplier,goodInfo,GoodNum,GoodSellPrice,goodSellTotal,GoodPrice,goodTotal,t_GoodNums,t_GoodTotalChas,maoli,FPTotal as FPNo ");
            strSql.Append(" FROM JXC_REPORT ");
            strSql.Append(" left join CG_POOrder on JXC_REPORT.PONo=CG_POOrder.PONo and CG_POOrder.IFZhui=0 and CG_POOrder.Status='通过'");

            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<Model.JXC.JXC_REPORT> list = new List<Model.JXC.JXC_REPORT>();
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
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<Model.JXC.JXC_REPORTTotal> Assessment_GetListArray_Total(string strWhere, string having, string fuhao, DateTime StartTime)
        {
            BaseKeyValue baseKeyModel = new BaseKeyValueService().GetModel(1);

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select  SumPOTotal,* from (");
            strSql.Append("select  MinOutDate,MaxDaoKuanDate,CG_POOrder.IsClose,CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate, CG_POOrder.GuestName,CG_POOrder.GuestType, CG_POOrder.GuestPro, ");
            strSql.Append(" sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal, ");
            strSql.Append(" isnull(sum(maoli),0) as maoliTotal,FPTotal,ZhangQiTotal, ");
            //strSql.Append(" ZhangQi as trueZhangQi,AE,INSIDE,AEPer as AEPer,INSIDEPer as INSIDEPer,isnull(avg(InvoTotal),0) as InvoTotal,avg(SellFPTotal) as SellFPTotal  from CG_POOrder ");
            strSql.Append(" AE,INSIDE,AEPer as AEPer,INSIDEPer as INSIDEPer,isnull(avg(InvoTotal),0) as InvoTotal,avg(SellFPTotal) as SellFPTotal,AVG(NeiInvoTotal) AS NeiInvoTotal  from CG_POOrder ");

            strSql.Append(" left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo ");
            strSql.Append(" left join (select max(DaoKuanDate)  as MaxDaoKuanDate,PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo");
            strSql.Append(@" left join (select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse left join Sell_OrderOutHouses
on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id where  Status='通过' group by PONO ) as SellOut on CG_POOrder.PONo=SellOut.PONO");

            strSql.Append(" left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) as ntb2 on CG_POOrder.PONo=ntb2.PONo");

            strSql.AppendFormat(@" left join ( select TB_ToInvoice.PONO,SUM(Total) as NeiInvoTotal  from  TB_ToInvoice left join 
(select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse where  Status='通过' group by PONO ) as MinOutPO on MinOutPO.PONo=TB_ToInvoice.PoNo 
left join CG_POOrder on CG_POOrder.PONo=TB_ToInvoice.PoNo  and CG_POOrder.IFZhui=0 
where  TB_ToInvoice.state='通过' and datediff(day,MinOutDate,TB_ToInvoice.DaoKuanDate)<={0} and CG_POOrder.PODate between '{1} 00:00:00'  and  '{2} 23:59:59' group by TB_ToInvoice.PoNo ) as ntb3 on CG_POOrder.PONo=ntb3.PONo", baseKeyModel.TypeValue, StartTime.ToString("yyyy-MM-dd"), StartTime.Year + "-12-31");


            strSql.Append(" where ifzhui=0  and CG_POOrder.Status='通过' ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }

            strSql.Append(" GROUP BY  CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate ,CG_POOrder.GuestName ,AE,INSIDE,FPTotal,AEPer,INSIDEPer,MinOutDate,MaxDaoKuanDate,ZhangQiTotal,CG_POOrder.IsClose,CG_POOrder.GuestType, CG_POOrder.GuestPro  ");

            if (having != "")
            {
                strSql.Append(having);
            }
            strSql.Append(" ) as allNewTb left join POTotal_SumView on  allNewTb.PONo=POTotal_SumView.pono " + fuhao);
            strSql.Append(" ORDER BY  allNewTb.PONo DESC ");
            List<Model.JXC.JXC_REPORTTotal> list = new List<Model.JXC.JXC_REPORTTotal>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        JXC_REPORTTotal model = new JXC_REPORTTotal();
                        object ojb;
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        ojb = dataReader["PODate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PODate = (DateTime)ojb;
                        }
                        ojb = dataReader["NeiInvoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ZhangNeiDaoTotal = (decimal)ojb;
                        }
                        else
                        {
                            model.isNeiDaoTotal = false;
                        }
                        model.GuestName = dataReader["GuestName"].ToString();
                        ojb = dataReader["goodSellTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.goodSellTotal = (decimal)ojb;
                        }
                        ojb = dataReader["goodTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.goodTotal = (decimal)ojb;
                        }
                        ojb = dataReader["maoliTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.maoliTotal = (decimal)ojb;
                        }
                        model.FPTotal = dataReader["FPTotal"].ToString();
                        ojb = dataReader["ZhangQiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ZhangQiTotal = (decimal)ojb;
                        }
                        //ojb = dataReader["trueZhangQi"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    //model.trueZhangQi =Convert.ToDecimal( ojb);
                        //}


                        model.AE = dataReader["AE"].ToString();
                        model.INSIDE = dataReader["INSIDE"].ToString();
                        ojb = dataReader["AEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AEPer = (decimal)ojb;
                        }
                        ojb = dataReader["INSIDEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.INSIDEPer = (decimal)ojb;
                        }

                        ojb = dataReader["InvoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvoiceTotal = (decimal)ojb;
                        }
                        ojb = dataReader["SellFPTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SellFPTotal = (decimal)ojb;
                        }
                        ojb = dataReader["IsClose"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsClose = (bool)ojb;
                        }
                        ojb = dataReader["GuestType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestType = ojb.ToString();
                            model.GuestType = model.GuestType.Replace("用户", "");
                        }

                        ojb = dataReader["GuestPro"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestProString = VAN_OA.BaseInfo.GuestProBaseInfoList.GetGestProInfo_1(ojb);

                        }
                        model.TrueLiRun = model.InvoiceTotal - model.goodTotal;

                        model.AETotal = model.AEPer * model.maoliTotal / 100;
                        model.InsidTotal = model.INSIDEPer * model.maoliTotal / 100;


                        ojb = dataReader["SumPOTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SumPOTotal = (decimal)ojb;
                        }

                        //===
                        var MaxDaoKuanDate = ojb = dataReader["MaxDaoKuanDate"];
                        var MinOutDate = ojb = dataReader["MinOutDate"];
                        if (MaxDaoKuanDate != null && MaxDaoKuanDate != DBNull.Value
                            && MinOutDate != null && MinOutDate != DBNull.Value)
                        {
                            TimeSpan ts = Convert.ToDateTime(MaxDaoKuanDate) - Convert.ToDateTime(MinOutDate);
                            model.trueZhangQi = ts.Days + 1;
                        }


                        if (MinOutDate != null && MinOutDate != DBNull.Value)
                        {
                            var minOutTime = Convert.ToDateTime(ojb);
                            TimeSpan ts = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(minOutTime.ToString("yyyy-MM-dd")));
                            model.trueZhangQi = ts.Days;
                        }

                        if (MaxDaoKuanDate != null && MaxDaoKuanDate != DBNull.Value
                            && MinOutDate != null && MinOutDate != DBNull.Value && model.SumPOTotal <= model.InvoiceTotal)
                        {
                            TimeSpan ts = Convert.ToDateTime(Convert.ToDateTime(MaxDaoKuanDate).ToString("yyyy-MM-dd")) - Convert.ToDateTime(Convert.ToDateTime(MinOutDate).ToString("yyyy-MM-dd"));
                            //model.trueZhangQi = ts.Days + 1;
                            model.trueZhangQi = ts.Days;
                        }

                        //在到款单列表（见第二画面），出库单已经开具的情况下，如果项目金额=0，则 不管到款金额合并 是否打勾，该项目的天数 一律显示0,表示不需要到款。
                        //这个逻辑 也需要在 销售业绩帐期考核、销售报表汇总、项目费用汇总统计  的 天数或实际到款期 中 同步修改
                        if (model.SumPOTotal == 0)
                        {
                            model.trueZhangQi = 0;
                        }

                        //===

                        if (model.SumPOTotal <= model.InvoiceTotal)
                        {
                            model.IsQuanDao = true;
                        }

                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<Model.JXC.JXC_REPORTTotal> GetSkill_Total(string strWhere, string having, string fuhao)
        {
            TB_BaseSkillService skillSer = new TB_BaseSkillService();
            var skillList = skillSer.GetListArray("");
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select  SumPOTotal,* from (");
            strSql.Append("select  MinOutDate,MaxDaoKuanDate,CG_POOrder.IsClose,CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate, CG_POOrder.GuestName,CG_POOrder.GuestType, CG_POOrder.GuestPro, ");
            strSql.Append(" sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal, ");
            strSql.Append(" isnull(sum(maoli),0) as maoliTotal,FPTotal,ZhangQiTotal, ");
            //strSql.Append(" ZhangQi as trueZhangQi,AE,INSIDE,AEPer as AEPer,INSIDEPer as INSIDEPer,isnull(avg(InvoTotal),0) as InvoTotal,avg(SellFPTotal) as SellFPTotal  from CG_POOrder ");
            strSql.Append(" AE,INSIDE,AEPer as AEPer,INSIDEPer as INSIDEPer,isnull(avg(InvoTotal),0) as InvoTotal,avg(SellFPTotal) as SellFPTotal,CG_POOrder.POType  from CG_POOrder ");

            strSql.Append(" left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo ");
            strSql.Append(" left join (select max(DaoKuanDate)  as MaxDaoKuanDate,PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo");
            strSql.Append(@" left join (select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse left join Sell_OrderOutHouses
on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id where  Status='通过' group by PONO ) as SellOut on CG_POOrder.PONo=SellOut.PONO");

            strSql.Append(" left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) as ntb2 on CG_POOrder.PONo=ntb2.PONo");
            strSql.Append(" where ifzhui=0  and CG_POOrder.Status='通过'");

            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }

            strSql.Append(" GROUP BY  CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate ,CG_POOrder.GuestName ,AE,INSIDE,FPTotal,AEPer,INSIDEPer,MinOutDate,MaxDaoKuanDate,ZhangQiTotal,CG_POOrder.IsClose,CG_POOrder.GuestType, CG_POOrder.GuestPro,CG_POOrder.POType  ");

            if (having != "")
            {
                strSql.Append(having);
            }
            strSql.Append(@" ) as allNewTb left join POTotal_SumView on  allNewTb.PONo=POTotal_SumView.pono 
left join 
(
 select tb_Dispatching.Id,MyPoNo,DisDate,tb_User.loginName,NiHours,MyValue,10*MyValue*MyXiShu AS allScore ,tb_Dispatching.OutDispater from tb_Dispatching
  left join tb_User on tb_User.ID=tb_Dispatching.OutDispater 
    where tb_Dispatching.Id in (
 SELECT allE_id FROM tb_EForm WHERE proId=1 AND state='通过')
) as PaiGong on PaiGong.MyPoNo=allNewTb.PONo " + fuhao);

            strSql.Append(" ORDER BY  allNewTb.PONo DESC ");
            List<Model.JXC.JXC_REPORTTotal> list = new List<Model.JXC.JXC_REPORTTotal>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        JXC_REPORTTotal model = new JXC_REPORTTotal();
                        object ojb;
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
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
                        model.GuestName = dataReader["GuestName"].ToString();
                        ojb = dataReader["goodSellTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.goodSellTotal = (decimal)ojb;
                        }
                        ojb = dataReader["goodTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.goodTotal = (decimal)ojb;
                        }
                        ojb = dataReader["maoliTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.maoliTotal = (decimal)ojb;
                        }
                        model.FPTotal = dataReader["FPTotal"].ToString();
                        ojb = dataReader["ZhangQiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ZhangQiTotal = (decimal)ojb;
                        }
                        //ojb = dataReader["trueZhangQi"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    //model.trueZhangQi =Convert.ToDecimal( ojb);
                        //}

                        //===

                        ojb = dataReader["SumPOTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SumPOTotal = (decimal)ojb;
                        }

                        var MaxDaoKuanDate = ojb = dataReader["MaxDaoKuanDate"];
                        var MinOutDate = ojb = dataReader["MinOutDate"];

                        if (MinOutDate != null && MinOutDate != DBNull.Value)
                        {
                            var minOutTime = Convert.ToDateTime(ojb);
                            TimeSpan ts = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(minOutTime.ToString("yyyy-MM-dd")));
                            model.trueZhangQi = ts.Days;
                        }
                        ojb = dataReader["InvoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvoiceTotal = (decimal)ojb;
                        }
                        if (MaxDaoKuanDate != null && MaxDaoKuanDate != DBNull.Value
                            && MinOutDate != null && MinOutDate != DBNull.Value && model.SumPOTotal <= model.InvoiceTotal)
                        {
                            TimeSpan ts = Convert.ToDateTime(Convert.ToDateTime(MaxDaoKuanDate).ToString("yyyy-MM-dd")) - Convert.ToDateTime(Convert.ToDateTime(MinOutDate).ToString("yyyy-MM-dd"));
                            //model.trueZhangQi = ts.Days + 1;
                            model.trueZhangQi = ts.Days;
                        }

                        //在到款单列表（见第二画面），出库单已经开具的情况下，如果项目金额=0，则 不管到款金额合并 是否打勾，该项目的天数 一律显示0,表示不需要到款。
                        //这个逻辑 也需要在 销售业绩帐期考核、销售报表汇总、项目费用汇总统计  的 天数或实际到款期 中 同步修改
                        if (model.SumPOTotal == 0)
                        {
                            model.trueZhangQi = 0;
                        }

                        //===
                        model.AE = dataReader["AE"].ToString();
                        model.INSIDE = dataReader["INSIDE"].ToString();
                        ojb = dataReader["AEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AEPer = (decimal)ojb;
                        }
                        ojb = dataReader["INSIDEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.INSIDEPer = (decimal)ojb;
                        }


                        ojb = dataReader["SellFPTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SellFPTotal = (decimal)ojb;
                        }
                        ojb = dataReader["IsClose"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsClose = (bool)ojb;
                        }
                        ojb = dataReader["GuestType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestType = ojb.ToString();
                            model.GuestType = model.GuestType.Replace("用户", "");
                        }


                        model.TrueLiRun = model.InvoiceTotal - model.goodTotal;
                        model.AETotal = model.AEPer * model.maoliTotal / 100;
                        model.InsidTotal = model.INSIDEPer * model.maoliTotal / 100;

                        if (model.SumPOTotal == model.InvoiceTotal)
                        {
                            model.IsQuanDao = true;
                        }

                        ojb = dataReader["DisDate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.DisDate = Convert.ToDateTime(ojb);
                        }
                        ojb = dataReader["loginName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.loginName = ojb.ToString();
                        }
                        ojb = dataReader["loginName"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.loginName = ojb.ToString();
                        }
                        ojb = dataReader["NiHours"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.NiHours = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["MyValue"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MyValue = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["allScore"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.allScore = Convert.ToDecimal(ojb);


                        }
                        if (model.allScore != 0)
                        {
                            var POType = Convert.ToDecimal(dataReader["POType"]);
                            if (POType == 1)
                            {
                                model.allScore = (skillList.Find(t => t.MyPoType == "零售") ?? new TB_BaseSkill()).XiShu * model.allScore;
                            }
                            else if (POType == 2)
                            {
                                model.allScore = (skillList.Find(t => t.MyPoType == "工程") ?? new TB_BaseSkill()).XiShu * model.allScore;
                            }
                            else if (POType == 3)
                            {
                                model.allScore = (skillList.Find(t => t.MyPoType == "系统") ?? new TB_BaseSkill()).XiShu * model.allScore;
                            }
                        }
                        ojb = dataReader["GuestPro"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestProString = VAN_OA.BaseInfo.GuestProBaseInfoList.GetGestProInfo_1(ojb);
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
        public List<Model.JXC.JXC_REPORTTotal> GetListArray_Total(string strWhere, string having, string fuhao)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select  SumPOTotal,* from (");
            strSql.Append("select  MinOutDate,MaxDaoKuanDate,CG_POOrder.IsClose,CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate, CG_POOrder.GuestName,CG_POOrder.GuestType, CG_POOrder.GuestPro, ");
            strSql.Append(" sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal, ");
            strSql.Append(" isnull(sum(maoli),0) as maoliTotal,FPTotal,ZhangQiTotal, ");
            //strSql.Append(" ZhangQi as trueZhangQi,AE,INSIDE,AEPer as AEPer,INSIDEPer as INSIDEPer,isnull(avg(InvoTotal),0) as InvoTotal,avg(SellFPTotal) as SellFPTotal  from CG_POOrder ");
            strSql.Append(" AE,INSIDE,AEPer as AEPer,INSIDEPer as INSIDEPer,isnull(avg(InvoTotal),0) as InvoTotal,avg(SellFPTotal) as SellFPTotal  from CG_POOrder ");

            strSql.Append(" left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo ");
            strSql.Append(" left join (select max(DaoKuanDate)  as MaxDaoKuanDate,PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo");
            strSql.Append(@" left join (select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse left join Sell_OrderOutHouses
on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id where  Status='通过' group by PONO ) as SellOut on CG_POOrder.PONo=SellOut.PONO");

            strSql.Append(" left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) as ntb2 on CG_POOrder.PONo=ntb2.PONo");
            strSql.Append(" where ifzhui=0  and CG_POOrder.Status='通过'");

            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }

            strSql.Append(" GROUP BY  CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate ,CG_POOrder.GuestName ,AE,INSIDE,FPTotal,AEPer,INSIDEPer,MinOutDate,MaxDaoKuanDate,ZhangQiTotal,CG_POOrder.IsClose,CG_POOrder.GuestType, CG_POOrder.GuestPro  ");

            if (having != "")
            {
                strSql.Append(having);
            }
            strSql.Append(" ) as allNewTb left join POTotal_SumView on  allNewTb.PONo=POTotal_SumView.pono " + fuhao);
            strSql.Append(" ORDER BY  allNewTb.PONo DESC ");
            List<Model.JXC.JXC_REPORTTotal> list = new List<Model.JXC.JXC_REPORTTotal>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        JXC_REPORTTotal model = new JXC_REPORTTotal();
                        object ojb;
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        ojb = dataReader["PODate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PODate = (DateTime)ojb;
                        }
                        model.GuestName = dataReader["GuestName"].ToString();
                        ojb = dataReader["goodSellTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.goodSellTotal = (decimal)ojb;
                        }
                        ojb = dataReader["goodTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.goodTotal = (decimal)ojb;
                        }
                        ojb = dataReader["maoliTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.maoliTotal = (decimal)ojb;
                        }
                        model.FPTotal = dataReader["FPTotal"].ToString();
                        ojb = dataReader["ZhangQiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ZhangQiTotal = (decimal)ojb;
                        }
                        //ojb = dataReader["trueZhangQi"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    //model.trueZhangQi =Convert.ToDecimal( ojb);
                        //}

                        //===

                        ojb = dataReader["SumPOTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SumPOTotal = (decimal)ojb;
                        }

                        var MaxDaoKuanDate = ojb = dataReader["MaxDaoKuanDate"];
                        var MinOutDate = ojb = dataReader["MinOutDate"];

                        if (MinOutDate != null && MinOutDate != DBNull.Value)
                        {
                            var minOutTime = Convert.ToDateTime(ojb);
                            TimeSpan ts = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(minOutTime.ToString("yyyy-MM-dd")));
                            model.trueZhangQi = ts.Days;
                        }
                        ojb = dataReader["InvoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvoiceTotal = (decimal)ojb;
                        }
                        if (MaxDaoKuanDate != null && MaxDaoKuanDate != DBNull.Value
                            && MinOutDate != null && MinOutDate != DBNull.Value && model.SumPOTotal <= model.InvoiceTotal)
                        {
                            TimeSpan ts = Convert.ToDateTime(Convert.ToDateTime(MaxDaoKuanDate).ToString("yyyy-MM-dd")) - Convert.ToDateTime(Convert.ToDateTime(MinOutDate).ToString("yyyy-MM-dd"));
                            //model.trueZhangQi = ts.Days + 1;
                            model.trueZhangQi = ts.Days;
                        }

                        //在到款单列表（见第二画面），出库单已经开具的情况下，如果项目金额=0，则 不管到款金额合并 是否打勾，该项目的天数 一律显示0,表示不需要到款。
                        //这个逻辑 也需要在 销售业绩帐期考核、销售报表汇总、项目费用汇总统计  的 天数或实际到款期 中 同步修改
                        if (model.SumPOTotal == 0)
                        {
                            model.trueZhangQi = 0;
                        }

                        //===
                        model.AE = dataReader["AE"].ToString();
                        model.INSIDE = dataReader["INSIDE"].ToString();
                        ojb = dataReader["AEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AEPer = (decimal)ojb;
                        }
                        ojb = dataReader["INSIDEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.INSIDEPer = (decimal)ojb;
                        }


                        ojb = dataReader["SellFPTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SellFPTotal = (decimal)ojb;
                        }
                        ojb = dataReader["IsClose"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsClose = (bool)ojb;
                        }
                        ojb = dataReader["GuestType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestType = ojb.ToString();
                            model.GuestType = model.GuestType.Replace("用户", "");
                        }

                        ojb = dataReader["GuestPro"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestProString = VAN_OA.BaseInfo.GuestProBaseInfoList.GetGestProInfo_1(ojb);

                        }
                        model.TrueLiRun = model.InvoiceTotal - model.goodTotal;

                        model.AETotal = model.AEPer * model.maoliTotal / 100;
                        model.InsidTotal = model.INSIDEPer * model.maoliTotal / 100;




                        //ojb = dataReader["allSellTotal"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.AllSellTotal = (decimal)ojb;
                        //}

                        if (model.SumPOTotal == model.InvoiceTotal)
                        {
                            model.IsQuanDao = true;
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
        public List<Model.JXC.JXC_REPORTTotal> NEW_GetListArray_Total(string strWhere, string having, string fuhao, DateTime StartTime, string compare, string fuhao_E,
            string KAO_POType, string NO_Kao_POType, string PoTypeList)
        {
            BaseKeyValue baseKeyModel = new BaseKeyValueService().GetModel(1);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select  SumPOTotal,*" + (compare == "" ? ",0 as KouInvoTotal" : ",WaiInvoTotal as KouInvoTotal") + " from (");
            strSql.Append("select  CG_POOrder.POType, MinOutDate,isnull(MaxDaoKuanDate,getdate()) as MaxDaoKuanDate,CG_POOrder.IsClose,CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate, CG_POOrder.GuestName,CG_POOrder.GuestType, CG_POOrder.GuestPro, ");
            strSql.Append(" sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal, ");
            strSql.Append(" isnull(sum(maoli),0) as maoliTotal,FPTotal,ZhangQiTotal, ");
            strSql.Append(" AE,INSIDE,AEPer as AEPer,INSIDEPer as INSIDEPer,isnull(avg(InvoTotal),0) as InvoTotal,avg(SellFPTotal) as SellFPTotal,avg(" + (compare == "" ? "0.05" : "WaiInvoTotal") + ") as WaiInvoTotal  from CG_POOrder ");

            strSql.Append(" left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo ");
            strSql.Append(" left join (select max(DaoKuanDate)  as MaxDaoKuanDate,PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo");
            strSql.Append(@" left join (select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse left join Sell_OrderOutHouses
on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id where  Status='通过' group by PONO ) as SellOut on CG_POOrder.PONo=SellOut.PONO");
            if (!string.IsNullOrEmpty(compare))
            {
                strSql.AppendFormat(@" left join ( select CG_POOrder.PONO,sum(case when datediff(day,MinOutDate,TB_ToInvoice.DaoKuanDate) {0} {1} then Total 
else 0 end)  as WaiInvoTotal   from CG_POOrder  left join TB_ToInvoice on CG_POOrder.PONo=TB_ToInvoice.PoNo  and TB_ToInvoice.state='通过' left join 
(select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse where  Status='通过' group by PONO ) as MinOutPO on MinOutPO.PONo=CG_POOrder.PoNo 
 where   CG_POOrder.IFZhui=0 and CG_POOrder.PODate between '{2} 00:00:00'  and  '{3} 23:59:59' {5}
 group by CG_POOrder.PoNo,POStatue4 having (POStatue4='已结清' and datediff(day,min(MinOutDate),isnull(max(TB_ToInvoice.DaoKuanDate),getdate())){4}{1})  or    
   (( POStatue4='' or POStatue4 is null)and datediff(day,min(MinOutDate),getdate()){4}{1})) as ntb3 on CG_POOrder.PONo=ntb3.PONo", compare, baseKeyModel.TypeValue, StartTime.ToString("yyyy-MM-dd"), DateTime.Now.Year + "-12-31", fuhao_E, KAO_POType);


            }
            strSql.Append(" left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) as ntb2 on CG_POOrder.PONo=ntb2.PONo");
            strSql.Append(" where ifzhui=0  and CG_POOrder.Status='通过'");

            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }

            strSql.Append(" GROUP BY  CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate ,CG_POOrder.GuestName ,AE,INSIDE,FPTotal,AEPer,INSIDEPer,MinOutDate,MaxDaoKuanDate,ZhangQiTotal,CG_POOrder.IsClose,CG_POOrder.GuestType, CG_POOrder.GuestPro,CG_POOrder.POType  ");

            if (having != "")
            {
                strSql.Append(having);
            }
            strSql.Append(" ) as allNewTb left join POTotal_SumView on  allNewTb.PONo=POTotal_SumView.pono " + fuhao);
            strSql.Append(" ORDER BY  allNewTb.PONo DESC ");
            List<Model.JXC.JXC_REPORTTotal> list = new List<Model.JXC.JXC_REPORTTotal>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        JXC_REPORTTotal model = new JXC_REPORTTotal();
                        model.PoTypeList = PoTypeList;
                        model.QueryDateTime = StartTime;
                        object ojb;
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        ojb = dataReader["PODate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PODate = (DateTime)ojb;
                        }
                        model.GuestName = dataReader["GuestName"].ToString();
                        ojb = dataReader["goodSellTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.goodSellTotal = (decimal)ojb;
                        }
                        ojb = dataReader["goodTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.goodTotal = (decimal)ojb;
                        }
                        ojb = dataReader["maoliTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.maoliTotal = (decimal)ojb;
                        }
                        model.FPTotal = dataReader["FPTotal"].ToString();
                        ojb = dataReader["ZhangQiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ZhangQiTotal = (decimal)ojb;
                        }
                        ojb = dataReader["InvoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvoiceTotal = (decimal)ojb;
                        }
                        ojb = dataReader["SumPOTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SumPOTotal = (decimal)ojb;
                        }
                        //ojb = dataReader["trueZhangQi"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    //model.trueZhangQi =Convert.ToDecimal( ojb);
                        //}

                        //===
                        var MaxDaoKuanDate = ojb = dataReader["MaxDaoKuanDate"];
                        var MinOutDate = ojb = dataReader["MinOutDate"];

                        if (MinOutDate != null && MinOutDate != DBNull.Value)
                        {
                            TimeSpan ts = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(Convert.ToDateTime(MinOutDate).ToString("yyyy-MM-dd")));

                            model.trueZhangQi = ts.Days;
                        }
                        //增加判断条件
                        if (MaxDaoKuanDate != null && MaxDaoKuanDate != DBNull.Value
                            && MinOutDate != null && MinOutDate != DBNull.Value
                            && model.SumPOTotal != 0 && model.SumPOTotal <= model.InvoiceTotal)
                        {
                            TimeSpan ts = Convert.ToDateTime(Convert.ToDateTime(MaxDaoKuanDate).ToString("yyyy-MM-dd")) - Convert.ToDateTime(Convert.ToDateTime(MinOutDate).ToString("yyyy-MM-dd"));

                            model.trueZhangQi = ts.Days;
                        }

                        //在到款单列表（见第二画面），出库单已经开具的情况下，如果项目金额=0，则 不管到款金额合并 是否打勾，该项目的天数 一律显示0,表示不需要到款。
                        //这个逻辑 也需要在 销售业绩帐期考核、销售报表汇总、项目费用汇总统计  的 天数或实际到款期 中 同步修改
                        if (model.SumPOTotal == 0)
                        {
                            model.trueZhangQi = 0;
                        }
                        if (model.SumPOTotal <= model.InvoiceTotal)
                        {
                            model.IsQuanDao = true;
                        }
                        //===
                        model.AE = dataReader["AE"].ToString();
                        model.INSIDE = dataReader["INSIDE"].ToString();
                        ojb = dataReader["AEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AEPer = (decimal)ojb;
                        }
                        ojb = dataReader["INSIDEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.INSIDEPer = (decimal)ojb;
                        }


                        ojb = dataReader["SellFPTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SellFPTotal = (decimal)ojb;
                        }
                        ojb = dataReader["IsClose"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsClose = (bool)ojb;
                        }
                        ojb = dataReader["GuestType"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestType = ojb.ToString();
                            model.GuestType = model.GuestType.Replace("用户", "");
                        }

                        ojb = dataReader["GuestPro"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.GuestProString = VAN_OA.BaseInfo.GuestProBaseInfoList.GetGestProInfo_1(ojb);

                        }
                        model.TrueLiRun = model.InvoiceTotal - model.goodTotal;

                        model.AETotal = model.AEPer * model.maoliTotal / 100;
                        model.InsidTotal = model.INSIDEPer * model.maoliTotal / 100;




                        ojb = dataReader["KouInvoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.WaiInvoTotal = Convert.ToDecimal(ojb);
                        }
                        model.potype = dataReader["POType"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        public List<Model.JXC.KPI_SellModel> KIP_SellReport(string strWhere, string having, string fuhao, DateTime StartTime, string compare, string fuhao_E,
          string KAO_POType, string NO_Kao_POType, string PoTypeList, string contactWhere)
        {
            BaseKeyValue baseKeyModel = new BaseKeyValueService().GetModel(1);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (");
            strSql.Append(@"select AE,SUM(POTotal_SumView.SumPOTotal) AS SumPOTotal,SUM(goodSellTotal) AS goodSellTotal,SUM(goodTotal) AS goodTotal,
SUM(maoliTotal) AS maoliTotal,SUM(InvoTotal) AS InvoTotal,SUM(SellFPTotal) AS SellFPTotal,SUM(KouInvoTotal) AS KouInvoTotal from (");
            strSql.Append(@"select *" + (compare == "" ? ",0 as KouInvoTotal" : ",WaiInvoTotal as KouInvoTotal") + @"  from (
select  CG_POOrder.PoNo,AE,sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal,  isnull(sum(maoli),0) as maoliTotal,
isnull(avg(InvoTotal),0) as InvoTotal,avg(SellFPTotal) as SellFPTotal,avg(" + (compare == "" ? "0.05" : "WaiInvoTotal") + @") as WaiInvoTotal  from CG_POOrder  
left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo  
left join (select max(DaoKuanDate)  as MaxDaoKuanDate,PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
left join (select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse 
left join Sell_OrderOutHouses on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id where  Status='通过' group by PONO ) as SellOut on CG_POOrder.PONo=SellOut.PONO ");

            if (!string.IsNullOrEmpty(compare))
            {
                strSql.AppendFormat(@" left join ( select CG_POOrder.PONO,sum(case when datediff(day,MinOutDate,TB_ToInvoice.DaoKuanDate) {0} {1} then Total 
else 0 end)  as WaiInvoTotal   from CG_POOrder  left join TB_ToInvoice on CG_POOrder.PONo=TB_ToInvoice.PoNo  and TB_ToInvoice.state='通过' left join 
(select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse where  Status='通过' group by PONO ) as MinOutPO on MinOutPO.PONo=CG_POOrder.PoNo 
 where   CG_POOrder.IFZhui=0 and CG_POOrder.PODate between '{2} 00:00:00'  and  '{3} 23:59:59' {5}
 group by CG_POOrder.PoNo,POStatue4 having (POStatue4='已结清' and datediff(day,min(MinOutDate),isnull(max(TB_ToInvoice.DaoKuanDate),getdate())){4}{1})  or    
   (( POStatue4='' or POStatue4 is null)and datediff(day,min(MinOutDate),getdate()){4}{1})) as ntb3 on CG_POOrder.PONo=ntb3.PONo", compare, baseKeyModel.TypeValue, StartTime.ToString("yyyy-MM-dd"), DateTime.Now.Year + "-12-31", fuhao_E, KAO_POType);
                
            }
            strSql.Append(@" left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) as ntb2 on CG_POOrder.PONo=ntb2.PONo
where ifzhui=0  and CG_POOrder.Status='通过' ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }

            strSql.Append(" GROUP BY  AE ,CG_POOrder.PoNo ");

            if (having != "")
            {
                strSql.Append(having);
            }

            strSql.AppendFormat(@" ) AS TEMPTB ) as NewTB 
LEFT JOIN POTotal_SumView on  NewTB.PONo=POTotal_SumView.pono  GROUP BY NewTB.AE )  as SUMPO 
LEFT JOIN
(
    select Name,count(*) AS ContactCount,sum(case when IsNewUnit=1 then 1 else 0 end) as NewCount  from tb_BusContact {0} group by Name
) AS contact on contact.Name=SUMPO.AE", contactWhere);
            strSql.Append(" ORDER BY AE ");
            List<Model.JXC.KPI_SellModel> list = new List<KPI_SellModel>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        KPI_SellModel model = new KPI_SellModel();
                        model.AE = dataReader["AE"].ToString();

                        object ojb;
                        int ContactCount = 0;
                        ojb = dataReader["ContactCount"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            ContactCount = (int)ojb;
                        }
                        ojb = dataReader["NewCount"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.NewContractCount = (int)ojb;
                        }
                        model.OldContractCount= ContactCount-model.NewContractCount;
                        ojb = dataReader["SumPOTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.POTotal = (decimal)ojb;
                        }

                        ojb = dataReader["goodSellTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SellTotal = (decimal)ojb;
                        }
                        ojb = dataReader["InvoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvoiceTotal = (decimal)ojb;
                        }
                        ojb = dataReader["maoliTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ProfitTotal = (decimal)ojb;
                        }
                        decimal goodTotal = 0;
                        ojb = dataReader["goodTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            goodTotal = (decimal)ojb;
                        }
                        model.LastProfitTotal = model.InvoiceTotal - goodTotal;
                        ojb = dataReader["SellFPTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            var SellFPTotal = (decimal)ojb;
                            if (model.POTotal!=0)
                            {
                                model.KP_Percent = SellFPTotal / model.POTotal*100;
                            }
                        }

                        ojb = dataReader["KouInvoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            var WaiInvoTotal = (decimal)ojb;
                            if (model.POTotal != 0)
                            {
                                model.DK_Percent = WaiInvoTotal / model.POTotal*100;
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
        public List<Model.JXC.JXC_REPORTTotal> GetListArray_Items_Total(string strWhere, string having, string tiaojian)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  SumPOTotal, MinOutDate,MaxDaoKuanDate,sumCost,IsClose,tb1.PONo,POName,PODate, GuestName,  ");
            strSql.Append("goodSellTotal,goodTotal, ");
            strSql.Append(" maoliTotal,FPTotal,ZhangQiTotal, ");
            strSql.Append(@" AE,INSIDE, AEPer,INSIDEPer,InvoTotal,SellFPTotal");
            strSql.Append(" ,ntb3.potype,itemTotal,HuiWuTotal,allItemTotal,MantTotal ");
            strSql.Append(" from (");
            strSql.Append("select  MinOutDate,MaxDaoKuanDate, CG_POOrder.IsClose,CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate, CG_POOrder.GuestName,  ");
            strSql.Append(" sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal, ");
            strSql.Append(" sum(maoli) as maoliTotal,FPTotal,ZhangQiTotal, ");
            strSql.Append(@" AE,INSIDE,AEPer as AEPer,INSIDEPer as INSIDEPer,avg(InvoTotal) as InvoTotal,avg(SellFPTotal) as SellFPTotal
            from CG_POOrder ");
            strSql.Append(" left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo ");
            strSql.Append(" left join (select max(DaoKuanDate)  as MaxDaoKuanDate,PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo");

            strSql.Append(@" left join (select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse left join Sell_OrderOutHouses
on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id where  Status='通过' group by PONO ) as SellOut on CG_POOrder.PONo=SellOut.PONO");


            strSql.Append(" left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) as ntb2 on CG_POOrder.PONo=ntb2.PONo");
            strSql.Append(" where ifzhui=0  and CG_POOrder.Status='通过'");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }

            strSql.Append(" GROUP BY  CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate ,CG_POOrder.GuestName ,AE,INSIDE,FPTotal,AEPer,INSIDEPer,ZhangQiTotal,CG_POOrder.IsClose,MinOutDate,MaxDaoKuanDate ");

            if (having != "")
            {
                strSql.Append(having);
            }
            strSql.Append(" ) as tb1 ");

            strSql.Append("  left join [POTotal_SumView] on [POTotal_SumView].PONo=tb1.PONo");
            strSql.AppendFormat(@" --费用类型 	费用金额 
left join 
(
select potype,pono,sum(total) as itemTotal from [JXC_Item_REPORT] {0} group by potype,pono
)
as ntb3 on tb1.PONo=ntb3.PONo 
--会务费用
left join 
(
select PONo,sum(HuiTotal) as HuiWuTotal
from tb_FundsUse where PONo is not null and HuiTotal is not null and state='通过' 
group by PONo
)
as ntb4 on tb1.PONo=ntb4.PONo 
--总费用
left join 
(
select PONo,sum(total) as allItemTotal
from JXC_Item_REPORT 
group by PONo
)
as ntb5 on tb1.PONo=ntb5.PONo 
left join 
(
SELECT pono,sum(OtherCost) as MantTotal FROM CG_POOrder LEFT JOIN CG_POOrders on CG_POOrder.id=CG_POOrders.Id where status='通过' and Profit is not null
group by pono
) as ntb6 on tb1.PONo=ntb6.PONo
left join TuiGuanLiTotal on tb1.pono=TuiGuanLiTotal.pono
", (tiaojian == "" ? "" : " where poType in ( " + tiaojian.Trim(',') + ")"));


            strSql.Append(" ORDER BY  tb1.PONo DESC ");
            List<Model.JXC.JXC_REPORTTotal> list = new List<Model.JXC.JXC_REPORTTotal>();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        JXC_REPORTTotal model = new JXC_REPORTTotal();
                        object ojb;
                        model.PONo = dataReader["PONo"].ToString();
                        model.POName = dataReader["POName"].ToString();
                        ojb = dataReader["PODate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.PODate = (DateTime)ojb;
                        }
                        model.GuestName = dataReader["GuestName"].ToString();
                        ojb = dataReader["goodSellTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.goodSellTotal = (decimal)ojb;
                        }
                        ojb = dataReader["goodTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.goodTotal = (decimal)ojb;
                        }
                        ojb = dataReader["maoliTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.maoliTotal = (decimal)ojb;
                        }
                        model.FPTotal = dataReader["FPTotal"].ToString();
                        ojb = dataReader["ZhangQiTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ZhangQiTotal = (decimal)ojb;
                        }

                        //===


                        //if (MaxDaoKuanDate != null && MaxDaoKuanDate != DBNull.Value
                        //    && MinOutDate != null && MinOutDate != DBNull.Value)
                        //{
                        //    TimeSpan ts = Convert.ToDateTime(MaxDaoKuanDate) - Convert.ToDateTime(MinOutDate);
                        //    model.trueZhangQi = ts.Days + 1;
                        //}
                        //在到款单列表（见第二画面），出库单已经开具的情况下，如果项目金额=0，则 不管到款金额合并 是否打勾，该项目的天数 一律显示0,表示不需要到款。
                        //这个逻辑 也需要在 销售业绩帐期考核、销售报表汇总、项目费用汇总统计  的 天数或实际到款期 中 同步修改
                        var SumPOTotal = Convert.ToDecimal(dataReader["SumPOTotal"]);
                        ojb = dataReader["InvoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvoiceTotal = (decimal)ojb;
                        }

                        var MinOutDate = ojb = dataReader["MinOutDate"];
                        if (MinOutDate != null && MinOutDate != DBNull.Value)
                        {
                            var minOutTime = Convert.ToDateTime(ojb);
                            TimeSpan ts = (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) - Convert.ToDateTime(minOutTime.ToString("yyyy-MM-dd")));
                            model.trueZhangQi = ts.Days;
                        }
                        var MaxDaoKuanDate = ojb = dataReader["MaxDaoKuanDate"];
                        if (MaxDaoKuanDate != null && MaxDaoKuanDate != DBNull.Value
                            && MinOutDate != null && MinOutDate != DBNull.Value && model.SumPOTotal <= model.InvoiceTotal)
                        {
                            TimeSpan ts = Convert.ToDateTime(Convert.ToDateTime(MaxDaoKuanDate).ToString("yyyy-MM-dd")) - Convert.ToDateTime(Convert.ToDateTime(MinOutDate).ToString("yyyy-MM-dd"));
                            //model.trueZhangQi = ts.Days + 1;
                            model.trueZhangQi = ts.Days;
                        }

                        if (SumPOTotal == 0)
                        {
                            model.trueZhangQi = 0;
                        }
                        //==
                        //ojb = dataReader["trueZhangQi"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    model.trueZhangQi = Convert.ToDecimal(ojb);
                        //}
                        model.AE = dataReader["AE"].ToString();
                        model.INSIDE = dataReader["INSIDE"].ToString();
                        ojb = dataReader["AEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AEPer = (decimal)ojb;
                        }
                        ojb = dataReader["INSIDEPer"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.INSIDEPer = (decimal)ojb;
                        }


                        ojb = dataReader["SellFPTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SellFPTotal = (decimal)ojb;
                        }
                        ojb = dataReader["IsClose"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.IsClose = (bool)ojb;
                        }

                        model.TrueLiRun = model.InvoiceTotal - model.goodTotal;

                        model.AETotal = model.AEPer * model.maoliTotal / 100;
                        model.InsidTotal = model.INSIDEPer * model.maoliTotal / 100;

                        ojb = dataReader["potype"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.potype = ojb.ToString();
                        }
                        ojb = dataReader["itemTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.itemTotal = Convert.ToDecimal(ojb);

                            if (model.potype == "管理费")
                            {
                                ojb = dataReader["sumCost"];
                                if (ojb != null && ojb != DBNull.Value)
                                {
                                    model.itemTotal -= Convert.ToDecimal(ojb);
                                }
                            }
                        }
                        ojb = dataReader["HuiWuTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.HuiWuTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["allItemTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.allItemTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["MantTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MantTotal = Convert.ToDecimal(ojb);
                        }

                        if (model.MantTotal > model.HuiWuTotal)
                        {
                            model.allItemTotal = model.allItemTotal - model.HuiWuTotal;
                        }
                        if (model.MantTotal <= model.HuiWuTotal)
                        {
                            model.allItemTotal = model.allItemTotal - model.MantTotal;
                        }
                        model.PONO_ProType = model.PONo + "," + model.potype;
                        if (SumPOTotal <= model.InvoiceTotal)
                        {
                            model.IsQuanDao = true;
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
        public Model.JXC.JXC_REPORT ReaderBind(IDataReader dataReader)
        {
            Model.JXC.JXC_REPORT model = new Model.JXC.JXC_REPORT();
            object ojb;
            model.PONo = dataReader["PONo"].ToString();
            model.ProNo = dataReader["ProNo"].ToString();
            ojb = dataReader["RuTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.RuTime = (DateTime)ojb;
            }
            model.Supplier = dataReader["Supplier"].ToString();
            model.goodInfo = dataReader["goodInfo"].ToString();
            ojb = dataReader["GoodNum"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodNum = (decimal)ojb;
            }
            ojb = dataReader["GoodSellPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodSellPrice = (decimal)ojb;
            }
            ojb = dataReader["goodSellTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.goodSellTotal = (decimal)ojb;
            }
            ojb = dataReader["GoodPrice"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GoodPrice = (decimal)ojb;
            }
            ojb = dataReader["goodTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.goodTotal = (decimal)ojb;
            }
            ojb = dataReader["t_GoodNums"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.t_GoodNums = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["t_GoodTotalChas"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.t_GoodTotalChas = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["maoli"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.maoli = Convert.ToDecimal(ojb);
            }
            ojb = dataReader["FPNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FPNo = ojb.ToString();
            }

            model.POName = dataReader["POName"].ToString();

            return model;
        }


        /// <summary>
        /// 销售结算明细表  -苏州万邦电脑系统有限公司 ,苏州源达万维智能科技有限公司 2加的销售数据
        /// </summary>
        /// <param name="fristTime"></param>
        /// <param name="lastTime"></param>
        /// <returns></returns>
        public List<JXC_Report_Detail> Get_Report(string fristTime, string lastTime, string comCode, string userId, string jieIsSelected)
        {
            string ponoSql = "";
            if (jieIsSelected != "-1")
            {
                ponoSql += " and JieIsSelected=" + jieIsSelected;
            }

            var list = new List<JXC_Report_Detail>();

            string aeSql = "";

            if (comCode != "")
            {
                comCode = "'" + comCode + "'";
                aeSql = string.Format(" and exists (select id from tb_User where  CompanyCode IN ({0}) and appName=id) ", comCode);
            }

            string sql = string.Format(@"select AE,sum(goodSellTotal) as goodSellTotal
from (  select CG_POOrder.AE,CG_POOrder.PONo,     
sum(goodSellTotal) as goodSellTotal  from CG_POOrder  
left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo    
where ifzhui=0  and CG_POOrder.Status='通过'  
and CG_POOrder.PODate>='{0} 00:00:00' and CG_POOrder.PODate<='{1} 23:59:59'
{2} and  
EXISTS (select ID from CG_POOrder where  PONO=JXC_REPORT.PONO    and IsSpecial=0
{3}) GROUP BY  CG_POOrder.PONo,AE ) as allNewTb group by AE; ", fristTime, lastTime, aeSql, ponoSql);

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = new JXC_Report_Detail();
                        model.SellTotal = (decimal)dataReader["goodSellTotal"];
                        model.AE = dataReader["AE"].ToString();
                        list.Add(model);
                    }
                }
            }

            return list;
        }

    }
}
