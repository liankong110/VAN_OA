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
using System.Collections;

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
        /// 被派员工 统计
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="having"></param>
        /// <param name="fuhao"></param>
        /// <returns></returns>
        public List<Model.JXC.SumSkillTotal> GetSumSkill_Total(string strWhere, string having, string fuhao, string paiSql)
        {
            TB_BaseSkillService skillSer = new TB_BaseSkillService();
            var skillList = skillSer.GetListArray("");
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@" select MyPoType,loginName,SUM(SumHours) as SumHours,sum(SumFK) as SumFK,avg(AvgFK) as AvgFK,sum(SumScore) as SumScore,avg(AvgScore) as AvgScore,sum(SumPOTotal) as SumPOTotal,sum(InvoTotal) as InvoTotal,sum(TypeCount) as TypeCount from 
 (
 select COUNT(*) AS TypeCount,MyPoNo,TB_BaseSkill.MyPoType,tb_User.loginName,sum(NiHours) SumHours,sum(MyValue) as SumFK,AVG(MyValue) as AvgFK,sum(10*MyValue*MyXiShu*XiShu) AS SumScore,AVG(10*MyValue*MyXiShu*XiShu) AS AvgScore from tb_Dispatching
  left join tb_User on tb_User.ID=tb_Dispatching.OutDispater 
  left join CG_POOrder on CG_POOrder.PONo=tb_Dispatching.MyPoNo and IFZhui=0
  left join TB_BaseSkill on TB_BaseSkill.Id=CG_POOrder.POType 
    where tb_Dispatching.Id in (
 SELECT allE_id FROM tb_EForm WHERE proId=1 AND state='通过') {3}
 group by MyPoNo,MyPoType,loginName
 ) as  Dispatching  
 inner join 
 (
 select allNewTb.PONo,SumPOTotal, InvoTotal 
 from (select  MinOutDate,MaxDaoKuanDate,CG_POOrder.IsClose,CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate, 
 CG_POOrder.GuestName,CG_POOrder.GuestType, CG_POOrder.GuestPro,  sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal, 
  isnull(sum(maoli),0) as maoliTotal,FPTotal,ZhangQiTotal,  AE,INSIDE,AEPer as AEPer,INSIDEPer as INSIDEPer,isnull(avg(InvoTotal),0) as InvoTotal,
  avg(SellFPTotal) as SellFPTotal,CG_POOrder.POType  from CG_POOrder  left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo  
  left join (select max(DaoKuanDate)  as MaxDaoKuanDate,PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice
    where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
	left join (select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse left join Sell_OrderOutHouses
on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id where  Status='通过' group by PONO ) as SellOut
 on CG_POOrder.PONo=SellOut.PONO 
 left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) as ntb2 on CG_POOrder.PONo=ntb2.PONo 
 where ifzhui=0  and CG_POOrder.Status='通过'  {0}
 GROUP BY  CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate ,CG_POOrder.GuestName ,AE,INSIDE,FPTotal,AEPer,INSIDEPer,MinOutDate,
 MaxDaoKuanDate,ZhangQiTotal,CG_POOrder.IsClose,CG_POOrder.GuestType, CG_POOrder.GuestPro,CG_POOrder.POType {1}) as allNewTb 
 left join POTotal_SumView on  allNewTb.PONo=POTotal_SumView.pono 
) as SumPorect  on Dispatching.MyPoNo=SumPorect.PONo {2}", strWhere, having, fuhao, paiSql);
            strSql.Append(" group by MyPoType,loginName");

            List<Model.JXC.SumSkillTotal_Detail> list = new List<Model.JXC.SumSkillTotal_Detail>();
            Hashtable hashtable = new Hashtable();
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql.ToString(), conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        SumSkillTotal_Detail model = new SumSkillTotal_Detail();
                        object ojb;
                        model.MyPoType = dataReader["MyPoType"].ToString();
                        model.loginName = dataReader["loginName"].ToString();
                        if (!hashtable.ContainsKey(model.loginName))
                        {
                            hashtable.Add(model.loginName, null);
                        }
                        ojb = dataReader["SumHours"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SumHours = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["SumFK"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SumFK = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["AvgFK"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AvgFK = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["SumScore"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SumScore = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["AvgScore"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.AvgScore = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["SumPOTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SumPOTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["InvoTotal"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.InvoTotal = Convert.ToDecimal(ojb);
                        }
                        ojb = dataReader["TypeCount"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.TypeCount = Convert.ToInt32(ojb);
                        }

                        list.Add(model);
                    }
                }
            }

            //逻辑处理
            List<SumSkillTotal> sumList = new List<SumSkillTotal>();
            decimal gongScore = list.Where(t => t.MyPoType == "工程").Sum(t => t.SumScore);
            decimal lingScore = list.Where(t => t.MyPoType == "零售").Sum(t => t.SumScore);
            decimal xiScore = list.Where(t => t.MyPoType == "系统").Sum(t => t.SumScore);

            foreach (string key in hashtable.Keys)
            {
                SumSkillTotal model = new SumSkillTotal();
                var tempList = list.FindAll(t => t.loginName == key);
                model.Name = key;

                model.Hours = tempList.Sum(t => t.SumHours);
                model.Gong_Value = tempList.Where(t => t.MyPoType == "工程").Sum(t => t.SumFK);
                model.Ling_Value = tempList.Where(t => t.MyPoType == "零售").Sum(t => t.SumFK);
                model.Xi_Value = tempList.Where(t => t.MyPoType == "系统").Sum(t => t.SumFK);

                model.SumValue = tempList.Sum(t => t.SumFK);
                var sumCount = tempList.Sum(t => t.TypeCount);
                if (sumCount != 0)
                {
                    model.AvgValue = model.SumValue / sumCount;
                }

                model.Gong_Score = tempList.Where(t => t.MyPoType == "工程").Sum(t => t.SumScore);
                model.Ling_Score = tempList.Where(t => t.MyPoType == "零售").Sum(t => t.SumScore);
                model.Xi_Score = tempList.Where(t => t.MyPoType == "系统").Sum(t => t.SumScore);
                if (gongScore != 0)
                {
                    model.Gong_Score_Per = model.Gong_Score / gongScore * 100;
                }
                if (lingScore != 0)
                {
                    model.Ling_Score_Per = model.Ling_Score / lingScore * 100;
                }
                if (xiScore != 0)
                {
                    model.Xi_Score_Per = model.Xi_Score / xiScore * 100;
                }

                model.SumScore = tempList.Sum(t => t.SumScore);
                if (sumCount != 0)
                {
                    model.AvgScore = model.SumScore / sumCount;
                }
                model.CompanyScore = gongScore + lingScore + xiScore;
                if (model.CompanyScore != 0)
                {
                    model.CompanyScore_Per = model.SumScore / model.CompanyScore * 100;
                }
                model.PoTotal = tempList.Sum(t => t.SumPOTotal);
                if (model.PoTotal != 0)
                {
                    model.DaoKuan_Per = tempList.Sum(t => t.InvoTotal) / model.PoTotal * 100;
                }
                sumList.Add(model);
            }

            return sumList;
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

        public List<InvoiceSimpDetail> GetInvoiceSimpDetailList()
        {
            List<InvoiceSimpDetail> list = new List<InvoiceSimpDetail>();
            string sql = string.Format(@"select DaoKuanDate,TB_ToInvoice.PONo from TB_ToInvoice left join
CG_POOrder on TB_ToInvoice.PoNo=CG_POOrder.PONo and IFZhui=0 where  Model='模型7' 
and BusType=0 and TB_ToInvoice.State='通过' order by DaoKuanDate");

            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var model = new InvoiceSimpDetail();
                        model.PONO = dataReader["PONo"].ToString();
                        model.DaoKuanDate = Convert.ToDateTime(dataReader["DaoKuanDate"]);
                        list.Add(model);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<Model.JXC.JXC_REPORTTotal> YuShouKuan_GetListArray_Total(string strWhere, string having, string fuhao)
        {
            List<InvoiceSimpDetail> invSimpDetailList = GetInvoiceSimpDetailList();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select  SumPOTotal,* from (");
            strSql.Append("select DaOKuanCount,MinDaoKuanDate, MinOutDate,MaxDaoKuanDate,CG_POOrder.IsClose,CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate, CG_POOrder.GuestName,CG_POOrder.GuestType, CG_POOrder.GuestPro, ");
            strSql.Append(" sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal, ");
            strSql.Append(" isnull(sum(maoli),0) as maoliTotal,FPTotal,ZhangQiTotal, ");
            //strSql.Append(" ZhangQi as trueZhangQi,AE,INSIDE,AEPer as AEPer,INSIDEPer as INSIDEPer,isnull(avg(InvoTotal),0) as InvoTotal,avg(SellFPTotal) as SellFPTotal  from CG_POOrder ");
            strSql.Append(" AE,INSIDE,AEPer as AEPer,INSIDEPer as INSIDEPer,isnull(avg(InvoTotal),0) as InvoTotal,avg(SellFPTotal) as SellFPTotal,Model  from CG_POOrder ");

            strSql.Append(" left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo ");
            strSql.Append(" left join (select max(DaoKuanDate)  as MaxDaoKuanDate,PoNo,SUM(Total) as InvoTotal,min(DaoKuanDate)  as MinDaoKuanDate,sum(case WHEN BUSTYPE=0 THEN 1 ELSE 0 end) AS DaOKuanCount from  TB_ToInvoice  where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo");
            strSql.Append(@" left join (select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse left join Sell_OrderOutHouses
on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id where  Status='通过' group by PONO ) as SellOut on CG_POOrder.PONo=SellOut.PONO");

            strSql.Append(" left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) as ntb2 on CG_POOrder.PONo=ntb2.PONo");
            strSql.Append(" where ifzhui=0  and CG_POOrder.Status='通过'");

            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }

            strSql.Append(" GROUP BY  CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate ,CG_POOrder.GuestName ,AE,INSIDE,FPTotal,AEPer,INSIDEPer,MinOutDate,MaxDaoKuanDate,ZhangQiTotal,CG_POOrder.IsClose,CG_POOrder.GuestType, CG_POOrder.GuestPro,Model ,MinDaoKuanDate,DaOKuanCount ");

            if (having != "")
            {
                strSql.Append(having);
            }
            strSql.Append(@" ) as allNewTb left join POTotal_SumView on  allNewTb.PONo=POTotal_SumView.pono 
left Join 
(
select PONo,max(BillDate) as BillDate
from [KingdeeInvoice].dbo.Invoice as inv left join Sell_OrderFP  on inv.InvoiceNumber=Sell_OrderFP.FPNo
where BillDate>'1900-1-1' and Status='通过' group by PONo
) as KisBill on KisBill.PONo=allNewTb.PONo
left join
(
 select PONo,count(*) AS BillCount FROM
 (
 select PONo,BillDate
from [KingdeeInvoice].dbo.Invoice as inv left join Sell_OrderFP  on inv.InvoiceNumber=Sell_OrderFP.FPNo
where BillDate>'1900-1-1' and Status='通过' group by PONo,BillDate
) as TB GROUP BY PONo
) as tb_BillCount on tb_BillCount.PONO=allNewTb.PONo
left join V_ModelZQ on V_ModelZQ.Model=allNewTb.model and V_ModelZQ.GuestName=allNewTb.GuestName
LEFT JOIN MODEL_ZQ ON allNewTb.PONO=MODEL_ZQ.PONO
" + fuhao);
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
                        var MinDaoKuanDate = ojb = dataReader["MinDaoKuanDate"];
                        var MaxDaoKuanDate = ojb = dataReader["MaxDaoKuanDate"];
                        var MinOutDate = ojb = dataReader["MinOutDate"];


                        if (MaxDaoKuanDate != null && MaxDaoKuanDate != DBNull.Value)
                        {
                            model.MaxDaoKuanDate = Convert.ToDateTime(MaxDaoKuanDate);
                        }
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
                        ojb = dataReader["MinDaoKuanTime_ZQ"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MinDaoKuanTime_ZQ = Convert.ToDateTime(ojb);
                        }
                        ojb = dataReader["MinBillDate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MinBillDate = Convert.ToDateTime(ojb);
                        }
                        ojb = dataReader["MinFPTime"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.MinFPTime = Convert.ToDateTime(ojb);
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

                        //预收账款系统 
                        //最近开票日
                        //计算开票日
                        //最近开票日按发票号取值金蝶发票清单中的新字段发票日期， 最近开票日如<=当月的25日，计算开票日=当月的30日（如2月份，就是最后一天）, 
                        //最近开票日如 > 当月的25日, 计算开票日 = 下个月的30日（如2月份，就是最后一天）, 如无最近开票日，计算开票日也空，最近开票日的右面增加一列计算开票日；
                        ojb = dataReader["BillDate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.BillDate = Convert.ToDateTime(ojb);

                            if (model.BillDate.Value.Day <= 25)
                            {
                                model.JSKaiPiaoDate = Convert.ToDateTime(model.BillDate.Value.Year + "-" + model.BillDate.Value.Month + "-1").AddMonths(1).AddDays(-1);
                            }
                            else
                            {
                                model.JSKaiPiaoDate = Convert.ToDateTime(model.BillDate.Value.Year + "-" + model.BillDate.Value.Month + "-1").AddMonths(2).AddDays(-1);
                            }
                        }

                        //经验账期
                        ojb = dataReader["AVG_ZQ"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Avg_ZQ = Convert.ToInt32(ojb);
                        }
                        ojb = dataReader["ZQ"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.ZQ = Convert.ToInt32(ojb);
                        }
                        ojb = dataReader["DaoKuanDate"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.SecondDaoKuanDate = Convert.ToDateTime(ojb);
                        }

                        ojb = dataReader["Model"];
                        if (ojb != null && ojb != DBNull.Value)
                        {
                            model.Model = Convert.ToString(ojb);
                        }
                        if (model.Model == "模型0" && model.BillDate != null && model.MinBillDate != null)
                        {
                            model.JSKaiPiaoDate = Convert.ToDateTime(model.BillDate.Value.Year + "-" + model.BillDate.Value.Month + "-1").AddMonths(1).AddDays(-1);
                            //model.MinBillDate = model.JSKaiPiaoDate;
                        }
                       

                        if (model.SellFPTotal - model.InvoiceTotal != 0)
                        {
                            if (model.Model == "模型0")
                            {
                                if (model.BillDate != null)
                                {
                                    model.YuGuDaoKuanDate = model.BillDate.Value.AddDays(model.Avg_ZQ);
                                }
                                model.DaoKuanNumber = 1;
                            }
                            //模型1 ,模型2,模型4 的计算如下
                            //预估到款日=计算开票日+经验账期（如尚无开票日，预估到款日也空白）；如发票额-实到账=0，预估到款日为空。
                            if (model.Model == "模型1" || model.Model == "模型2" || model.Model == "模型4")
                            {
                                if (model.BillDate != null && model.MinBillDate != null)
                                {
                                    model.YuGuDaoKuanDate = model.MinBillDate.Value.AddDays(model.Avg_ZQ);
                                    model.DaoKuanNumber = 1;
                                }
                            }

                            // 模型4：预估到款日=最近开票日+经验账期（如尚无最近开票日，预估到款日也空白）；如发票额-实到账=0，预估到款日为空。
                            if (model.Model == "模型4")
                            {
                                if (model.BillDate != null)
                                {
                                    model.YuGuDaoKuanDate = model.BillDate.Value.AddDays(model.Avg_ZQ);
                                }
                            }
                            // 这里的开票顺序即是预估到款顺序，只需判断当前开票是首次还是第几次，原则上，上一次开票是必须到款了我们才可能开下一次发票的。只需在金蝶发票清单中 判断下最近开票日是第几次的发票日期，就得出是第几次预估到款。
                            ojb = dataReader["BillCount"];
                            if ((model.Model == "模型2" || model.Model == "模型4") && model.BillDate != null && ojb != null && ojb != DBNull.Value)
                            {
                                model.DaoKuanNumber = Convert.ToInt32(ojb);
                            }
                        }
                        else
                        {
                            // 这里的开票顺序即是预估到款顺序，只需判断当前开票是首次还是第几次，原则上，上一次开票是必须到款了我们才可能开下一次发票的。只需在金蝶发票清单中 判断下最近开票日是第几次的发票日期，就得出是第几次预估到款。
                            ojb = dataReader["BillCount"];
                            if ((model.Model == "模型8") && model.BillDate != null && ojb != null && ojb != DBNull.Value)
                            {
                                model.DaoKuanNumber = Convert.ToInt32(ojb);
                            }
                        }
                        //模型3
                        //第一次预估到款日=项目日期+经验账期，
                        //第二次预估到款日 = 最近开票日 + 经验账期（如尚无最近开票日，预估到款日也空白），
                        //第三次预估到款日 = 最近的发票实际到款日 + 1年；如发票额 - 实到账 = 0，预估到款日为空;
                        //第一次预估到款日的判定条件是尚无到款记录。第二次预估到款日的判定条件是 有一次发票记录且5%< 到款总比例 <= 50 %。第三次预估到款日的判定条件是50 %< 到款总比例 <= 95 %。

                        if (model.Model == "模型3")
                        {
                            if (model.SellFPTotal - model.InvoiceTotal == 0 && model.SellFPTotal == 0)
                            {
                                model.YuGuDaoKuanDate = model.PODate.AddDays(model.Avg_ZQ);
                                model.DaoKuanNumber = 1;
                            }
                            else if (model.DaoKuanLv.Value > Convert.ToDecimal(0.05) && model.DaoKuanLv.Value <= Convert.ToDecimal(0.5))
                            {
                                if (model.SellFPTotal - model.InvoiceTotal != 0 && model.BillDate != null)
                                {
                                    model.YuGuDaoKuanDate = model.BillDate.Value.AddDays(model.Avg_ZQ);
                                    model.DaoKuanNumber = 2;
                                }
                            }
                            else if (model.DaoKuanLv.Value > Convert.ToDecimal(0.5) && model.DaoKuanLv.Value <= Convert.ToDecimal(0.95))
                            {
                                if (MaxDaoKuanDate != null && MaxDaoKuanDate != DBNull.Value)
                                {
                                    model.YuGuDaoKuanDate = Convert.ToDateTime(MaxDaoKuanDate).AddYears(1);
                                    model.DaoKuanNumber = 3;
                                }
                            }
                        }

                        //模型5
                        //第一次预估到款日=最近开票日+经验账期（如尚无最近开票日，预估到款日也空白），第二次预估到款日=最近的发票实际到款日+1年；如发票额-实到账=0，预估到款日为空
                        //第一次预估到款日的判定条件是尚无到款记录。第二次预估到款日的判定条件是有一次发票记录且70 %< 到款总比例 <= 95 %。
                        if (model.SellFPTotal - model.InvoiceTotal != 0 && model.Model == "模型5")
                        {
                            if (model.InvoiceTotal == 0 && model.BillDate != null)
                            {
                                model.YuGuDaoKuanDate = model.BillDate.Value.AddDays(model.Avg_ZQ);
                                model.DaoKuanNumber = 1;
                            }
                            else if (model.DaoKuanLv.Value > Convert.ToDecimal(0.7) && model.DaoKuanLv.Value <= Convert.ToDecimal(0.95))
                            {
                                if (MaxDaoKuanDate != null && MaxDaoKuanDate != DBNull.Value)
                                {
                                    model.YuGuDaoKuanDate = Convert.ToDateTime(MaxDaoKuanDate).AddYears(1);
                                    model.DaoKuanNumber = 2;
                                }
                            }
                        }

                        //模型6
                        //第一次预估到款日 = 项目日期 + 经验账期，
                        //第二次预估到款日 = 最近开票日 + 经验账期（如尚无最近开票日，预估到款日也空白），
                        //第三次预估到款日 = 第1次发票实际到款日 + 项目订单的计划完工天数 + 经验账期，
                        //第四次预估到款期 = 最近的发票实际到款日 + 半年，
                        //第五次预估到款期 = 最近的发票实际到款日 + 1年；如发票额 - 实到账 = 0，预估到款日为空
                        //第一次预估到款日的判定条件是尚无到款记录。
                        //第二次预估到款日的判定条件是有一次发票记录且5 %< 到款总比例 <= 25 %。
                        //第三次预估到款日的判定条件是有1次发票记录且 < 25 % 到款总比例 <= 50 %。
                        //第四次预估到款日的判定条件是有1次发票记录且50 %< 到款总比例 <= 70 %。
                        //第五次预估到款日的判定条件是仅有1次发票记录且70 %< 到款总比例 <= 90 %。
                        if (model.Model == "模型6")
                        {
                            if (model.InvoiceTotal == 0)
                            {
                                model.YuGuDaoKuanDate = model.PODate.AddDays(model.Avg_ZQ);
                                model.DaoKuanNumber = 1;
                            }
                            else
                            {
                                if (model.DaoKuanLv.Value > Convert.ToDecimal(0.05) && model.DaoKuanLv.Value <= Convert.ToDecimal(0.25))
                                {
                                    if (model.BillDate != null)
                                    {
                                        model.YuGuDaoKuanDate = model.BillDate.Value.AddDays(model.Avg_ZQ);
                                        model.DaoKuanNumber = 2;
                                    }
                                }
                                else if (model.DaoKuanLv.Value > Convert.ToDecimal(0.25) && model.DaoKuanLv.Value <= Convert.ToDecimal(0.5))
                                {
                                    //这里有问题 需要待商量
                                    //项目订单的计划完工天数 缺省你按 10
                                    if (MinDaoKuanDate != null && MinDaoKuanDate != DBNull.Value)
                                    {
                                        model.YuGuDaoKuanDate = Convert.ToDateTime(MinDaoKuanDate).AddDays(model.Avg_ZQ + 10);
                                        model.DaoKuanNumber = 3;
                                    }
                                }
                                else if (model.DaoKuanLv.Value > Convert.ToDecimal(0.5) && model.DaoKuanLv.Value <= Convert.ToDecimal(0.7))
                                {
                                    if (MaxDaoKuanDate != null && MaxDaoKuanDate != DBNull.Value)
                                    {
                                        model.YuGuDaoKuanDate = Convert.ToDateTime(MaxDaoKuanDate).AddMonths(6);
                                        model.DaoKuanNumber = 4;
                                    }
                                }
                                else if (model.DaoKuanLv.Value > Convert.ToDecimal(0.7) && model.DaoKuanLv.Value <= Convert.ToDecimal(0.9))
                                {
                                    if (MaxDaoKuanDate != null && MaxDaoKuanDate != DBNull.Value)
                                    {
                                        model.YuGuDaoKuanDate = Convert.ToDateTime(MaxDaoKuanDate).AddYears(1);
                                        model.DaoKuanNumber = 5;
                                    }
                                }

                            }
                        }

                        //模型7
                        //第一次预估到款日=最近开票日+经验账期（如尚无最近开票日，预估到款日也空白），
                        //第N次预估到款日 = 最近的发票实际到款日 + 1个月；如发票额 - 实到账 = 0，预估到款日为空
                        // 第一次预估到款日的判定条件是尚无到款记录。第N次预估到款日的判定条件是0 < 到款总比例 < 100 %。
                        if (model.SellFPTotal - model.InvoiceTotal != 0 && model.Model == "模型7")
                        {
                            if (model.InvoiceTotal == 0)
                            {
                                if (model.BillDate != null)
                                {
                                    model.YuGuDaoKuanDate = model.BillDate.Value.AddDays(model.Avg_ZQ);
                                    model.DaoKuanNumber = 1;
                                }
                            }
                            else if (model.DaoKuanLv.Value > 0 && model.DaoKuanLv.Value <= 1)
                            {
                                if (MaxDaoKuanDate != null && MaxDaoKuanDate != DBNull.Value)
                                {
                                    model.YuGuDaoKuanDate = Convert.ToDateTime(MaxDaoKuanDate).AddMonths(1);
                                    //这里只开1次发票，只需判断之前到款几次即可。                                     
                                    //如 到款单列表 没有首次发票到款日或 有首次发票实际到款日+30天后的第一次发票到款记录是否有，没有的话 当前就是第二次预估到款日；
                                    //有的话，这个第一次发票到款日+30天后的第一次发票到款是否有，没有话2+1；继续循环。

                                    if (model.MinDaoKuanTime_ZQ != null)
                                    {
                                        model.DaoKuanNumber = 2;
                                        var result = invSimpDetailList.FindAll(t => t.DaoKuanDate >= model.MinDaoKuanTime_ZQ.Value.AddDays(30));
                                        if (result.Count > 0)
                                        {
                                            var fristDate = result[0].DaoKuanDate;
                                            model.DaoKuanNumber = 3;
                                            while (result.Count > 0)
                                            {
                                                result = result.FindAll(t => t.DaoKuanDate >= fristDate.AddDays(30));
                                                if (result.Count > 0)
                                                {
                                                    fristDate = result[0].DaoKuanDate;
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }

                        //模型8
                        //第一次预估到款日=最近开票日+经验账期（如尚无开票日，预估到款日也空白），
                        //第2 - 4次预估到款日 = 最近开票日 + 经验账期（如尚无开票日，预估到款日也空白）；
                        //如发票额 - 实到账 = 0，预估到款日为空
                        if (model.Model == "模型8")
                        {
                            if (model.SellFPTotal - model.InvoiceTotal != 0 && model.BillDate != null)
                            {
                                model.YuGuDaoKuanDate = model.BillDate.Value.AddDays(model.Avg_ZQ);
                            }
                        }

                        //所有预付到款日期为空时，预付到款=0
                        if (model.YuGuDaoKuanDate != null)
                        {
                            model.YuGuDaoKuanTotal = model.SellFPTotal - model.InvoiceTotal;
                            model.YuGuDaoKuanDate = model.YuGuDaoKuanDate.Value.Date;
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
            string KAO_POType, string NO_Kao_POType, string PoTypeList, int zhangQI)
        {
            var poTypeList = new TB_BasePoTypeService().GetListArray("");
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
                        model.zhangQI = zhangQI;
                        model.BaseKeyValue = Convert.ToInt32(baseKeyModel.TypeValue);
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
                        int POType = Convert.ToInt32(dataReader["POType"]);
                        model.potypeString = poTypeList.Find(t => t.Id == POType).BasePoType;
                        model.potype = dataReader["POType"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        public List<Model.JXC.KPI_SellModel> KPI_AE()
        {
            List<Model.JXC.KPI_SellModel> AELIST = new List<KPI_SellModel>();
            string sql = "SELECT AE FROM CG_POOrder where AE IN (select loginName from tb_User where loginStatus='在职') GROUP BY AE ORDER BY AE";
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        KPI_SellModel model = new KPI_SellModel();
                        model.AE = dataReader["AE"].ToString();

                        AELIST.Add(model);
                    }
                }
            }
            return AELIST;
        }

        public List<KPI_SellModel> Kpi_ContactList(string contactWhere)
        {
            List<KPI_SellModel> contactList = new List<KPI_SellModel>();
            string sql = string.Format(" select Name,count(*) AS ContactCount,sum(case when IsNewUnit=1 then 1 else 0 end) as NewCount  from tb_BusContact {0} group by Name", contactWhere);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(sql, conn);
                using (SqlDataReader dataReader = objCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        KPI_SellModel model = new KPI_SellModel();
                        model.AE = dataReader["Name"].ToString();
                        int ContactCount = 0;
                        object ojb;
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
                        model.OldContractCount = ContactCount - model.NewContractCount;
                        contactList.Add(model);
                    }
                }
            }
            return contactList;
        }

        public List<Model.JXC.KPI_SellModel> KIP_SellReport(string strWhere, string having, string fuhao,
          string KAO_POType, string NO_Kao_POType, string PoTypeList, string contactWhere, string zhangQiWhere)
        {
            string compare = "";
            BaseKeyValue baseKeyModel = new BaseKeyValueService().GetModel(1);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (");
            strSql.Append(@"select AE,SUM(SumPOTotal) AS SumPOTotal,SUM(goodSellTotal) AS goodSellTotal,SUM(goodTotal) AS goodTotal,
SUM(maoliTotal) AS maoliTotal,SUM(InvoTotal) AS InvoTotal,SUM(SellFPTotal) AS SellFPTotal,SUM(KouInvoTotal) AS KouInvoTotal,count(*) AS PCount from (");


            strSql.Append(@"select * from (SELECT 
case
when isnull(POTotal_SumView.SumPOTotal,0)=0 then 0
when  MaxDaoKuanDate is not null and MinOutDate is not null and isnull(POTotal_SumView.SumPOTotal,0)<= isnull(InvoTotal,0) 
then datediff(day,CONVERT(varchar(100), MinOutDate, 23),CONVERT(varchar(100), MaxDaoKuanDate, 23))
when MinOutDate is not null then datediff(day,CONVERT(varchar(100), MinOutDate, 23), CONVERT(varchar(100), GETDATE(), 23))
else 0
end as ZhangQi,NewTB.*,POTotal_SumView.SumPOTotal
from (");

            strSql.Append(@"select *" + (compare == "" ? ",0 as KouInvoTotal" : ",WaiInvoTotal as KouInvoTotal") + @"  from (
select  MinOutDate,isnull(MaxDaoKuanDate,getdate()) as MaxDaoKuanDate,CG_POOrder.PoNo,AE,sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal,  isnull(sum(maoli),0) as maoliTotal,
isnull(avg(InvoTotal),0) as InvoTotal,avg(SellFPTotal) as SellFPTotal,avg(" + (compare == "" ? "0.05" : "WaiInvoTotal") + @") as WaiInvoTotal  from CG_POOrder  
left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo  
left join (select max(DaoKuanDate)  as MaxDaoKuanDate,PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
left join (select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse 
left join Sell_OrderOutHouses on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id where  Status='通过' group by PONO ) as SellOut on CG_POOrder.PONo=SellOut.PONO ");

            //            if (!string.IsNullOrEmpty(compare))
            //            {
            //                strSql.AppendFormat(@" left join ( select CG_POOrder.PONO,sum(case when datediff(day,MinOutDate,TB_ToInvoice.DaoKuanDate) {0} {1} then Total 
            //else 0 end)  as WaiInvoTotal   from CG_POOrder  left join TB_ToInvoice on CG_POOrder.PONo=TB_ToInvoice.PoNo  and TB_ToInvoice.state='通过' left join 
            //(select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse where  Status='通过' group by PONO ) as MinOutPO on MinOutPO.PONo=CG_POOrder.PoNo 
            // where   CG_POOrder.IFZhui=0 and CG_POOrder.PODate between '{2} 00:00:00'  and  '{3} 23:59:59' {5}
            // group by CG_POOrder.PoNo,POStatue4 having (POStatue4='已结清' and datediff(day,min(MinOutDate),isnull(max(TB_ToInvoice.DaoKuanDate),getdate())){4}{1})  or    
            //   (( POStatue4='' or POStatue4 is null)and datediff(day,min(MinOutDate),getdate()){4}{1})) as ntb3 on CG_POOrder.PONo=ntb3.PONo", compare, baseKeyModel.TypeValue, 
            //   StartTime.ToString("yyyy-MM-dd"), DateTime.Now.Year + "-12-31", fuhao_E, KAO_POType);

            //            }
            strSql.Append(@" left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) as ntb2 on CG_POOrder.PONo=ntb2.PONo
where ifzhui=0  and CG_POOrder.Status='通过' ");

            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }

            strSql.Append(" GROUP BY  AE ,CG_POOrder.PoNo,MaxDaoKuanDate,MinOutDate  ");

            if (having != "")
            {
                strSql.Append(having);
            }

            strSql.AppendFormat(@" ) AS TEMPTB ) as NewTB 
LEFT JOIN POTotal_SumView on  NewTB.PONo=POTotal_SumView.pono " + fuhao + " ) as AAAAA " + zhangQiWhere + @") AS BBBB GROUP BY BBBB.AE  )  as SUMPO 
", contactWhere);
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
                            if (model.POTotal != 0)
                            {
                                model.KP_Percent = SellFPTotal / model.POTotal * 100;
                            }
                        }
                        if (model.POTotal != 0)
                        {
                            model.DK_Percent = model.InvoiceTotal / model.POTotal * 100;
                        }


                        //ojb = dataReader["KouInvoTotal"];
                        //if (ojb != null && ojb != DBNull.Value)
                        //{
                        //    var WaiInvoTotal =Convert.ToDecimal(ojb);
                        //    if (model.POTotal != 0)
                        //    {
                        //        model.DK_Percent = WaiInvoTotal / model.POTotal*100;
                        //    }
                        //}
                        model.TimeOutCount = Convert.ToInt32(dataReader["PCount"]);
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
