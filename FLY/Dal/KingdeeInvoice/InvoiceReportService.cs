using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Model.KingdeeInvoice;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections;

namespace VAN_OA.Dal.KingdeeInvoice
{
    public class InvoiceReportService
    {

        public List<InvoiceReport> GetAllInvoiceReports(string GuestName, string InvoiceNo, string InvTotal,
            string InvTotalEqu, string InvoFormDate, string InvToDate,
            string where, string compareType, bool isInvoTotalToge, string Isorder, bool cbZhengShu, string pono = "", string isXiaozhang="-1",string param="",
            string temp="",bool cbIsSpecial=false)
        {
            List<InvoiceReport> allInvoiceData = new List<InvoiceReport>();
            string strSql = "";
            if (compareType == "0")//不匹配
            {
                where = where.Replace(temp, "");
                string k_Sql = " where 1=1 ";

                if (GuestName != "")
                {
                    where += string.Format(" and Sell_OrderFP.GuestNAME like '%{0}%' ", GuestName);
                    k_Sql += string.Format(" and GuestNAME like '%{0}%'", GuestName);
                }

                if (InvoiceNo != "")
                {
                    where += string.Format(" and FPNo like '%{0}%' ", InvoiceNo);
                    k_Sql += string.Format(" and InvoiceNumber like '%{0}%'", InvoiceNo);
                }

                if (InvTotal != "")
                {
                    //where += string.Format(" and Sell_OrderFP.Total {0} {1} ", InvTotalEqu, InvTotal);
                    //k_Sql += string.Format(" and invoice.Total {0} {1}  ", InvTotalEqu, InvTotal);
                    if (cbZhengShu == false)
                    {
                        if (InvTotalEqu == "=")
                        {
                            where += string.Format(" and Sell_OrderFP.Total in ({0},{1})", Convert.ToDecimal(InvTotal), -Convert.ToDecimal(InvTotal));
                            k_Sql += string.Format(" and invoice.Total in ({0},{1})", Convert.ToDecimal(InvTotal), -Convert.ToDecimal(InvTotal));
                        }
                        else
                        {
                            where += string.Format(" and Sell_OrderFP.Total {0} {1}", InvTotalEqu, InvTotal);
                            k_Sql += string.Format(" and invoice.Total {0} {1}", InvTotalEqu, InvTotal);
                        }
                    }
                    else
                    {
                        if (InvTotalEqu == "=")
                        {
                            where += string.Format(" and Sell_OrderFP.Total={0} ", Convert.ToDecimal(InvTotal));
                            k_Sql += string.Format(" and invoice.Total={0}", Convert.ToDecimal(InvTotal));
                        }
                        else
                        {
                            where += string.Format(" and Sell_OrderFP.Total {0} {1}", InvTotalEqu, InvTotal);
                            k_Sql += string.Format(" and invoice.Total {0} {1}", InvTotalEqu, InvTotal);
                        }
                    }
                   
                }

                if (InvoFormDate != "")
                {
                    where += string.Format(" and RuTime>='{0} 00:00:00' ", InvoFormDate);
                    k_Sql += string.Format(" and CreateDate>='{0} 00:00:00' ", InvoFormDate);
                }

                if (InvToDate != "")
                {
                    where += string.Format(" and RuTime<='{0} 23:59:59' ", InvToDate);
                    k_Sql += string.Format(" and CreateDate<='{0} 23:59:59' ", InvToDate);
                }

                if (Isorder == "0")
                {
                    k_Sql += " and Isorder is null ";
                } 
                if (Isorder == "1")
                {
                    k_Sql += " and Isorder=1 ";
                }
                if (pono != "")
                {
                    k_Sql += " and 1=2 ";
                }

                if (isXiaozhang != "-1")
                {
                    where += string.Format(" and Isorder={0}", isXiaozhang);
                }
                strSql = string.Format(@" select  Sell_OrderFP.id as FPId,invoice.id,FPNo,Sell_OrderFP.GuestNAME as OA_GuestName ,Sell_OrderFP.Total as OA_Total,RuTime,Sell_OrderFP.PONo,
InvoiceNumber, invoice.GuestName,invoice.Total,CreateDate
 from (select *FROM  Sell_OrderFP WHERE Status='通过') AS Sell_OrderFP  full join 
(select id,InvoiceNumber,GuestName,Total,CreateDate from 
" + InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View as invoice {0}  AND InvoiceNumber<>'') as invoice 
on Sell_OrderFP.FPNo=Invoice.InvoiceNumber and (Sell_OrderFP.GuestNAME<>invoice.GuestName or (Sell_OrderFP.GuestNAME=invoice.GuestName and Sell_OrderFP.Total<>invoice.Total ))
left join  CG_POOrder on  CG_POOrder.PONO=Sell_OrderFP.PONO  and ifzhui=0 ",  k_Sql);

                if (isInvoTotalToge)
                {
                    strSql += string.Format(@" where not exists( 
SELECT TB.FPNo AS InvoiceNumber FROM ( select FPNo,sum(Total) as Total,GuestNAME  from Sell_OrderFP where  Status='通过' group by FPNo,GuestNAME  ) AS TB 
INNER JOIN "+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.Total=TB1.Total WHERE Sell_OrderFP.FPNo=TB.FPNo OR invoice.InvoiceNumber=TB.FPNo)");
                }
                else
                {

                    strSql += string.Format(@" where not exists( 
SELECT TB.FPNo AS InvoiceNumber FROM  Sell_OrderFP AS TB 
INNER JOIN "+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.Total=TB1.Total and TB.GuestNAME=TB1.GuestName
WHERE  Status='通过' and Sell_OrderFP.FPNo=TB.FPNo OR invoice.InvoiceNumber=TB.FPNo)");
          
                }
                strSql += string.Format("and (({0} and {1}) or InvoiceNumber is not null )", where.Replace(" where",""), param);
                if (cbIsSpecial == false)
                {

                }
                strSql += " order by Sell_OrderFP.FPNo desc,InvoiceNumber desc  ";
            }
            if (compareType == "1")//匹配
            {
                if (InvoFormDate != "")
                {
                    where += string.Format(" and RuTime>='{0} 00:00:00' ", InvoFormDate);
                }
                if (InvToDate != "")
                {
                    where += string.Format(" and RuTime<='{0} 23:59:59' ", InvToDate);
                }


                strSql = string.Format(@"SELECT TB.FPNo AS InvoiceNumber,TB.GuestNAME,TB.Total FROM (
 select FPNo,GuestNAME,sum(Total) as Total from Sell_OrderFP {0} group by FPNo,GuestNAME ) AS TB INNER JOIN "+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View
 AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.GuestNAME=TB1.GuestName AND TB.Total=TB1.Total where 1=1 ", where);

                if (InvoFormDate != "")
                {
                    strSql += string.Format(" and TB1.CreateDate>='{0} 00:00:00'  ", InvoFormDate);
                }
                if (InvToDate != "")
                {
                    strSql += string.Format(" and TB1.CreateDate<='{0} 23:59:59' ", InvToDate);
                }

                if (GuestName != "")
                {
                    strSql += string.Format(" and TB.GuestNAME like '%{0}%' ", GuestName);
                }
                if (InvoiceNo != "")
                {
                    strSql += string.Format(" and TB.FPNo like '%{0}%' ", InvoiceNo);
                }
                if (InvTotal != "")
                {
                    //strSql += string.Format(" and TB.Total {0} {1} ", InvTotalEqu, InvTotal);
                    if (cbZhengShu == false)
                    {
                        if (InvTotalEqu == "=")
                        {
                            strSql += string.Format(" and TB.Total in ({0},{1})", Convert.ToDecimal(InvTotal), -Convert.ToDecimal(InvTotal));

                        }
                        else
                        {
                            strSql += string.Format(" and TB.Total {0} {1}", InvTotalEqu, InvTotal);

                        }
                    }
                    else
                    {
                        if (InvTotalEqu == "=")
                        {
                            strSql += string.Format(" and TB.Total={0} ", Convert.ToDecimal(InvTotal));
                        }
                        else
                        {
                            strSql += string.Format(" and TB.Total {0} {1}", InvTotalEqu, InvTotal);
                        }
                    }
                   
                }
            
                if (Isorder == "0")
                {
                    strSql += " and TB1.Isorder is null ";
                }
                if (Isorder == "1")
                {
                    strSql += " and TB1.Isorder=1 ";
                }

                if (isXiaozhang != "-1")
                {
                    where += string.Format(" and Isorder={0}", isXiaozhang);
                }

                //if (isInvoTotalToge)
                //{

                    strSql += string.Format(@" UNION  SELECT TB.FPNo AS InvoiceNumber,TB.GuestNAME,TB.Total FROM (
 select FPNo,sum(Total) as Total,GuestNAME  from Sell_OrderFP {0} group by FPNo,GuestNAME  ) AS TB INNER JOIN "+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View
 AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.Total=TB1.Total where 1=1 ", where);
                //}


                if (InvoFormDate != "")
                {
                    strSql += string.Format(" and TB1.CreateDate>='{0} 00:00:00'  ", InvoFormDate);
                }
                if (InvToDate != "")
                {
                    strSql += string.Format(" and TB1.CreateDate<='{0} 23:59:59' ", InvToDate);
                }

                if (GuestName != "")
                {
                    strSql += string.Format(" and TB.GuestNAME like '%{0}%' ", GuestName);
                }
                if (InvoiceNo != "")
                {
                    strSql += string.Format(" and TB.FPNo like '%{0}%' ", InvoiceNo);
                }
                if (InvTotal != "")
                {
                   // strSql += string.Format(" and TB.Total {0} {1} ", InvTotalEqu, InvTotal);
                    if (cbZhengShu == false)
                    {
                        if (InvTotalEqu == "=")
                        {
                            strSql += string.Format(" and TB.Total in ({0},{1})", Convert.ToDecimal(InvTotal), -Convert.ToDecimal(InvTotal));

                        }
                        else
                        {
                            strSql += string.Format(" and TB.Total {0} {1}", InvTotalEqu, InvTotal);

                        }
                    }
                    else
                    {
                        if (InvTotalEqu == "=")
                        {
                            strSql += string.Format(" and TB.Total={0} ", Convert.ToDecimal(InvTotal));
                        }
                        else
                        {
                            strSql += string.Format(" and TB.Total {0} {1}", InvTotalEqu, InvTotal);
                        }
                    }
                   
                }

                if (Isorder == "0")
                {
                    strSql += " and TB1.Isorder is null ";
                }
                if (Isorder == "1")
                {
                    strSql += " and TB1.Isorder=1 ";
                }

                strSql = string.Format(" select * from ({0}) as t order by InvoiceNumber desc ", strSql);
            }
            if (strSql == "")
            {
                return allInvoiceData;
            }
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (compareType == "0")//不匹配
                    {
                    //    Hashtable hs = new Hashtable();
                    //    Hashtable hs1 = new Hashtable();
                        while (objReader.Read())
                        {
                            var model = ReaderBind_OA(objReader);
                            //if (model.Kingdee_Id != 0 || model.OA_FPId != 0)
                            //{
                                allInvoiceData.Add(model);
                            //}
                        }
                    }
                    if (compareType == "1")//匹配
                    {
                        while (objReader.Read())
                        {
                            allInvoiceData.Add(ReaderBind(objReader));
                        }
                    }
                    //if (compareType == "2")//全部
                    //{
                    //    while (objReader.Read())
                    //    {
                    //        allInvoiceData.Add(ReaderBind(objReader));
                    //    }
                    //}
                }
            }
            return allInvoiceData;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public InvoiceReport ReaderBind(IDataReader dataReader)
        {
            InvoiceReport model = new InvoiceReport();
            object ojb;
            ojb = dataReader["InvoiceNumber"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.All_InvoiceNo = ojb.ToString();
            }
            ojb = dataReader["GuestNAME"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.All_GuestName = ojb.ToString();
            }
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.All_InvoiceTotal = (decimal)ojb;
            }
            return model;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public InvoiceReport ReaderBind_OA(IDataReader dataReader)
        {
            InvoiceReport model = new InvoiceReport();
            object ojb;

            //OA
            ojb = dataReader["FPId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OA_FPId = Convert.ToInt32(ojb);
            }

            //金蝶
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Kingdee_Id = Convert.ToInt32(ojb);
            }
          
            //if (model.OA_FPId != 0 && !oa_hs.ContainsKey(model.OA_FPId))
            //{
            //    oa_hs.Add(model.OA_FPId, null);
                ojb = dataReader["FPNo"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.OA_InvoiceNo = ojb.ToString();
                }
                ojb = dataReader["OA_GuestName"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.OA_GuestName = ojb.ToString();
                }
                ojb = dataReader["OA_Total"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.OA_InvoiceTotal = (decimal)ojb;
                }

                ojb = dataReader["RuTime"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.OA_InvoiceDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ojb));
                }
                ojb = dataReader["PONo"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.OA_PONO = ojb.ToString();
                }
            //}
            //else
            //{
            //    model.OA_FPId = 0;
            //}

          
            //if (model.Kingdee_Id != 0 && !kingdee_hs.ContainsKey(model.Kingdee_Id))
            //{
            //    kingdee_hs.Add(model.Kingdee_Id, null);
                ojb = dataReader["InvoiceNumber"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.Kingdee_InvoiceNo = ojb.ToString();
                }
                ojb = dataReader["GuestNAME"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.Kingdee_GuestName = ojb.ToString();
                }
                ojb = dataReader["Total"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.Kingdee_InvoiceTotal = (decimal)ojb;
                }
                ojb = dataReader["CreateDate"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    model.Kingdee_InvoiceDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ojb));
                }
            //}
            //else
            //{
            //    model.Kingdee_Id = 0;
            //}
          

            return model;
        }




        public List<AccountReport> GetAllAccountReports(string GuestName, string InvoiceNo, string InvTotal,
          string InvTotalEqu, string InvoFormDate, string InvToDate,
          string where, string compareType, bool isInvoTotalToge, string Isorder, bool sameFP, string diffDate, bool cbZhengShu, string tiaojian = "-1",
          string ddlKISDaoKuanTotal="", string KISDaoKuanTotal="", string ddlOADaoKuanTotal="", string OADaoKuanTotal="")
        {
            // select * ,(SELECT GoodName_1 +',' FROM AAA FOR XML PATH('')) as models from A_Role
            List<AccountReport> allInvoiceData = new List<AccountReport>();
            string strSql = "";
            if (compareType == "0")//不匹配
            {
                string k_Sql = " where 1=1 ";

                if (GuestName != "")
                {
                    where += string.Format(" and Sell_OrderFP.GuestNAME like '%{0}%' ", GuestName);
                    k_Sql += string.Format(" and GuestNAME like '%{0}%'", GuestName);
                }

                if (InvoiceNo != "")
                {
                    where += string.Format(" and Sell_OrderFP.FPNo like '%{0}%' ", InvoiceNo);
                    k_Sql += string.Format(" and InvoiceNumber like '%{0}%'", InvoiceNo);
                }

                if (InvTotal != "")
                {
                    //where += string.Format(" and Sell_OrderFP.Total {0} {1} ", InvTotalEqu, InvTotal);
                    //k_Sql += string.Format(" and invoice.Total {0} {1}  ", InvTotalEqu, InvTotal);
                    if (cbZhengShu == false)
                    {
                        if (InvTotalEqu == "=")
                        {
                            where += string.Format(" and Sell_OrderFP.Total in ({0},{1})", Convert.ToDecimal(InvTotal), -Convert.ToDecimal(InvTotal));
                            k_Sql += string.Format(" and invoice.Total in ({0},{1})", Convert.ToDecimal(InvTotal), -Convert.ToDecimal(InvTotal));
                        }
                        else
                        {
                            where += string.Format(" and Sell_OrderFP.Total {0} {1}", InvTotalEqu, InvTotal);
                            k_Sql += string.Format(" and invoice.Total {0} {1}", InvTotalEqu, InvTotal);
                        }
                    }
                    else
                    {
                        if (InvTotalEqu == "=")
                        {
                            where += string.Format(" and Sell_OrderFP.Total={0} ", Convert.ToDecimal(InvTotal));
                            k_Sql += string.Format(" and invoice.Total={0}", Convert.ToDecimal(InvTotal));
                        }
                        else
                        {
                            where += string.Format(" and Sell_OrderFP.Total {0} {1}", InvTotalEqu, InvTotal);
                            k_Sql += string.Format(" and invoice.Total {0} {1}", InvTotalEqu, InvTotal);
                        }
                    }
                }

                if (InvoFormDate != "")
                {
                    where += string.Format(" and RuTime>='{0} 00:00:00' ", InvoFormDate);
                    k_Sql += string.Format(" and CreateDate>='{0} 00:00:00' ", InvoFormDate);
                }

                if (InvToDate != "")
                {
                    where += string.Format(" and RuTime<='{0} 23:59:59' ", InvToDate);
                    k_Sql += string.Format(" and CreateDate<='{0} 23:59:59' ", InvToDate);
                }

                if (Isorder == "0")
                {
                    k_Sql += " and Isorder is null ";
                }
                if (Isorder == "1")
                {
                    k_Sql += " and Isorder=1 ";
                }
                //金蝶到账金额
                if (KISDaoKuanTotal != "")
                {
                    //where += string.Format(" and 1=2", ddlKISDaoKuanTotal, KISDaoKuanTotal);
                    k_Sql += string.Format(" and Received{0}{1}", ddlKISDaoKuanTotal, KISDaoKuanTotal);
                }
                //OA到账金额
                if (OADaoKuanTotal != "")
                {
                    where += string.Format(" and AccountTotal{0}{1}", ddlOADaoKuanTotal, OADaoKuanTotal);
                    //k_Sql += string.Format(" and 1=2", ddlOADaoKuanTotal, OADaoKuanTotal);
                }

                strSql = string.Format(@" select  AccountTotal,Received,Sell_OrderFP.id as FPId,invoice.id,FPNo,Sell_OrderFP.GuestNAME as OA_GuestName ,Sell_OrderFP.Total as OA_Total,RuTime,PONo,
InvoiceNumber, invoice.GuestName,invoice.Total,CreateDate
 from 
(select id,Sell_OrderFP.FPNo,GuestNAME,Total,RuTime,PONo,AccountTotal from Sell_OrderFP Left join (select FPNo,sum(Total) as AccountTotal from TB_ToInvoice where State='通过' group by FPNo) as new_tb on Sell_OrderFP.FPNo=new_tb.FPNo {0} ) as Sell_OrderFP
full join 
(select id,InvoiceNumber,GuestName,Total,CreateDate,Received from 
"+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View as invoice {1} ) as invoice  on Sell_OrderFP.FPNo=Invoice.InvoiceNumber and (Sell_OrderFP.GuestNAME<>invoice.GuestName or (Sell_OrderFP.GuestNAME=invoice.GuestName and Sell_OrderFP.Total<>invoice.Total )) ", where, k_Sql);
               
                if (isInvoTotalToge)
                {
                    strSql += string.Format(@" where not exists( 
SELECT TB.FPNo AS InvoiceNumber FROM ( select FPNo,sum(Total) as Total,GuestNAME  from Sell_OrderFP where  Status='通过' group by FPNo,GuestNAME  ) AS TB 
INNER JOIN "+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.Total=TB1.Total WHERE Sell_OrderFP.FPNo=TB.FPNo OR invoice.InvoiceNumber=TB.FPNo)");
                }
                else
                {

                    strSql += string.Format(@" where not exists( 
SELECT TB.FPNo AS InvoiceNumber FROM  Sell_OrderFP AS TB 
INNER JOIN "+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.Total=TB1.Total and TB.GuestNAME=TB1.GuestName
WHERE  Status='通过' and Sell_OrderFP.FPNo=TB.FPNo OR invoice.InvoiceNumber=TB.FPNo)");

                }

                if (isInvoTotalToge)
                { 
                    strSql = string.Format(@" select SUM(AccountTotal) as AccountTotal,SUM(Received) as Received,0 as FPId,0 as id,FPNo,'' as OA_GuestName,SUM(OA_Total) as OA_Total
 ,MIN(RuTime) as RuTime,InvoiceNumber,GuestName,SUM(Total) AS Total,CreateDate,'' as PONo  from  ( {0} )as t group by InvoiceNumber,GuestName,CreateDate,FPNo {1} order by t.FPNo desc,InvoiceNumber desc  ", strSql,
                                                                                                                                                              (sameFP ? "having SUM(AccountTotal)<>SUM(Received)" : ""));
                }
                else
                {
                    strSql += " order by Sell_OrderFP.FPNo desc,InvoiceNumber desc  ";
                }
            }
            if (compareType == "1")//匹配
            {
                if (InvoFormDate != "")
                {
                    where += string.Format(" and RuTime>='{0} 00:00:00' ", InvoFormDate);
                }
                if (InvToDate != "")
                {
                    where += string.Format(" and RuTime<='{0} 23:59:59' ", InvToDate);
                }


                strSql = string.Format(@"SELECT TB.FPNo AS InvoiceNumber,TB.GuestNAME,TB.Total,AccountTotal,TB1.Received,TB.MaxRuTime FROM (
 select FPNo,GuestNAME,sum(Total) as Total,MAX(RuTime) as MaxRuTime from Sell_OrderFP {0} group by FPNo,GuestNAME ) AS TB INNER JOIN "+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View
 AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.GuestNAME=TB1.GuestName AND TB.Total=TB1.Total Left join (select FPNo,sum(Total) as AccountTotal from TB_ToInvoice where State='通过' group by FPNo) as new_tb on TB.FPNo=new_tb.FPNo where 1=1 ", where);

                if (InvoFormDate != "")
                {
                    strSql += string.Format(" and TB1.CreateDate>='{0} 00:00:00'  ", InvoFormDate);
                }
                if (InvToDate != "")
                {
                    strSql += string.Format(" and TB1.CreateDate<='{0} 23:59:59' ", InvToDate);
                }

                if (GuestName != "")
                {
                    strSql += string.Format(" and TB.GuestNAME like '%{0}%' ", GuestName);
                }
                if (InvoiceNo != "")
                {
                    strSql += string.Format(" and TB.FPNo like '%{0}%' ", InvoiceNo);
                }

                if (KISDaoKuanTotal != "")
                {
                    strSql += string.Format(" and Received{0}{1}",ddlKISDaoKuanTotal,KISDaoKuanTotal);
                }
                if (OADaoKuanTotal != "")
                {
                    strSql += string.Format(" and isnull(AccountTotal,0){0}{1}", ddlOADaoKuanTotal, OADaoKuanTotal);
                }
                if (InvTotal != "")
                {
                    //strSql += string.Format(" and TB.Total {0} {1} ", InvTotalEqu, InvTotal);
                    if (cbZhengShu == false)
                    {
                        if (InvTotalEqu == "=")
                        {
                            strSql += string.Format(" and TB.Total in ({0},{1})", Convert.ToDecimal(InvTotal), -Convert.ToDecimal(InvTotal));

                        }
                        else
                        {
                            strSql += string.Format(" and TB.Total {0} {1}", InvTotalEqu, InvTotal);

                        }
                    }
                    else
                    {
                        if (InvTotalEqu == "=")
                        {
                            strSql += string.Format(" and TB.Total={0} ", Convert.ToDecimal(InvTotal));
                        }
                        else
                        {
                            strSql += string.Format(" and TB.Total {0} {1}", InvTotalEqu, InvTotal);
                        }
                    }
                }

                if (Isorder == "0")
                {
                    strSql += " and TB1.Isorder is null ";
                }
                if (Isorder == "1")
                {
                    strSql += " and TB1.Isorder=1 ";
                }
                if (tiaojian == "0")
                {
                    strSql += "  and isnull(Received,0)<TB.Total and isnull(AccountTotal,0) <=TB.Total ";
                }
                //if (isInvoTotalToge)
                //{

                strSql += string.Format(@" UNION  SELECT TB.FPNo AS InvoiceNumber,TB.GuestNAME,TB.Total,AccountTotal,TB1.Received,TB.MaxRuTime FROM (
 select FPNo,sum(Total) as Total,GuestNAME,MAX(RuTime) as MaxRuTime from Sell_OrderFP {0} group by FPNo,GuestNAME  ) AS TB INNER JOIN "+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View
 AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.Total=TB1.Total Left join (select FPNo,sum(Total) as AccountTotal from TB_ToInvoice where State='通过' group by FPNo) as new_tb on TB.FPNo=new_tb.FPNo where 1=1 ", where);
                //}


                if (InvoFormDate != "")
                {
                    strSql += string.Format(" and TB1.CreateDate>='{0} 00:00:00'  ", InvoFormDate);
                }
                if (InvToDate != "")
                {
                    strSql += string.Format(" and TB1.CreateDate<='{0} 23:59:59' ", InvToDate);
                }

                if (GuestName != "")
                {
                    strSql += string.Format(" and TB.GuestNAME like '%{0}%' ", GuestName);
                }
                if (InvoiceNo != "")
                {
                    strSql += string.Format(" and TB.FPNo like '%{0}%' ", InvoiceNo);
                }
                if (KISDaoKuanTotal != "")
                {
                    strSql += string.Format(" and Received{0}{1}", ddlKISDaoKuanTotal, KISDaoKuanTotal);
                }
                if (OADaoKuanTotal != "")
                {
                    strSql += string.Format(" and isnull(AccountTotal,0){0}{1}", ddlOADaoKuanTotal, OADaoKuanTotal);
                }
                if (InvTotal != "")
                {
                    //strSql += string.Format(" and TB.Total {0} {1} ", InvTotalEqu, InvTotal);
                    if (cbZhengShu == false)
                    {
                        if (InvTotalEqu == "=")
                        {
                            strSql += string.Format(" and TB.Total in ({0},{1})", Convert.ToDecimal(InvTotal), -Convert.ToDecimal(InvTotal));

                        }
                        else
                        {
                            strSql += string.Format(" and TB.Total {0} {1}", InvTotalEqu, InvTotal);

                        }
                    }
                    else
                    {
                        if (InvTotalEqu == "=")
                        {
                            strSql += string.Format(" and TB.Total={0} ", Convert.ToDecimal(InvTotal));
                        }
                        else
                        {
                            strSql += string.Format(" and TB.Total {0} {1}", InvTotalEqu, InvTotal);
                        }
                    }
                }

                if (Isorder == "0")
                {
                    strSql += " and TB1.Isorder is null ";
                }
                if (Isorder == "1")
                {
                    strSql += " and TB1.Isorder=1 ";
                }
                if (tiaojian == "0")
                {
                    strSql += "  and isnull(Received,0)<TB.Total and isnull(AccountTotal,0) <=TB.Total ";
                }
                //if (isInvoTotalToge==false)
                //{
                //    strSql = string.Format(" select * from ({0}) as t order by InvoiceNumber desc ", strSql);
                //}
                //else
                //{
//                    strSql = string.Format(@" select InvoiceNumber,'' as GuestNAME,SUM(Total) as Total,SUM(AccountTotal) as AccountTotal,SUM(Received) as Received,
//(SELECT GuestNAME+'_'+PONo+',' FROM Sell_OrderFP AS A where Status='通过' 
//and T.InvoiceNumber=A.FPNo FOR XML PATH('')) as PONo from ({0}) as t  group by InvoiceNumber {1} order by InvoiceNumber desc ", strSql, (sameFP ? "having SUM(AccountTotal)<>SUM(Received)" : ""));
                string having = "";
                if (sameFP || diffDate != "")
                {
                    having = " having ";
                    if (sameFP)
                    {
                        having += "SUM(isnull(AccountTotal,0))<>SUM(Received)";
                        if (diffDate != "")
                        {
                            having += " and ";
                        }
                    }
                    if (diffDate != "")
                    {
                        having += diffDate;
                    }
                }
                strSql = string.Format(@" select InvoiceNumber,avg(Total) as Total,avg(AccountTotal) as AccountTotal,SUM(Received) as Received,max(MaxRuTime) as MaxRuTime from ({0}) as t  group by InvoiceNumber {1} order by InvoiceNumber desc ",
                    strSql, having);

                //}
            }
            if (strSql == "")
            {
                return allInvoiceData;
            }
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql, conn);
                objCommand.CommandTimeout = 500;
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (compareType == "0")//不匹配
                    {
                        //    Hashtable hs = new Hashtable();
                        //    Hashtable hs1 = new Hashtable();
                        while (objReader.Read())
                        {
                            var model = ReaderBind_OA_1(objReader);
                            //if (model.Kingdee_Id != 0 || model.OA_FPId != 0)
                            //{
                            allInvoiceData.Add(model);
                            //}
                        }
                    }
                    if (compareType == "1")//匹配
                    {
                        while (objReader.Read())
                        {
                            allInvoiceData.Add(ReaderBind_1(objReader));
                        }
                    }
                    //if (compareType == "2")//全部
                    //{
                    //    while (objReader.Read())
                    //    {
                    //        allInvoiceData.Add(ReaderBind(objReader));
                    //    }
                    //}
                }
            }
            return allInvoiceData;
        }


        public List<AccountReport> GetAllAccountReports(string GuestName, string InvoiceNo, string InvTotal,
       string InvTotalEqu, string InvoFormDate, string InvToDate,
       string where, string compareType, bool isInvoTotalToge, string Isorder, bool sameFP, string diffDate, string tiaojian = "-1",
       string ddlKISDaoKuanTotal = "", string KISDaoKuanTotal = "", string ddlOADaoKuanTotal = "", string OADaoKuanTotal = "")
        {
            // select * ,(SELECT GoodName_1 +',' FROM AAA FOR XML PATH('')) as models from A_Role
            List<AccountReport> allInvoiceData = new List<AccountReport>();
            string strSql = "";
            if (compareType == "0")//不匹配
            {
                string k_Sql = " where 1=1 ";

                if (GuestName != "")
                {
                    where += string.Format(" and Sell_OrderFP.GuestNAME like '%{0}%' ", GuestName);
                    k_Sql += string.Format(" and GuestNAME like '%{0}%'", GuestName);
                }

                if (InvoiceNo != "")
                {
                    where += string.Format(" and Sell_OrderFP.FPNo like '%{0}%' ", InvoiceNo);
                    k_Sql += string.Format(" and InvoiceNumber like '%{0}%'", InvoiceNo);
                }

                if (InvTotal != "")
                {
                    where += string.Format(" and Sell_OrderFP.Total {0} {1} ", InvTotalEqu, InvTotal);
                    k_Sql += string.Format(" and invoice.Total {0} {1}  ", InvTotalEqu, InvTotal);                   
                }

                if (InvoFormDate != "")
                {
                    where += string.Format(" and RuTime>='{0} 00:00:00' ", InvoFormDate);
                    k_Sql += string.Format(" and CreateDate>='{0} 00:00:00' ", InvoFormDate);
                }

                if (InvToDate != "")
                {
                    where += string.Format(" and RuTime<='{0} 23:59:59' ", InvToDate);
                    k_Sql += string.Format(" and CreateDate<='{0} 23:59:59' ", InvToDate);
                }

                if (Isorder == "0")
                {
                    k_Sql += " and Isorder is null ";
                }
                if (Isorder == "1")
                {
                    k_Sql += " and Isorder=1 ";
                }

                //金蝶到账金额
                if (KISDaoKuanTotal != "")
                {
                    //where += string.Format(" and 1=2", ddlKISDaoKuanTotal, KISDaoKuanTotal);
                    k_Sql += string.Format(" and Received{0}{1}", ddlKISDaoKuanTotal, KISDaoKuanTotal);
                }
                //OA到账金额
                if (OADaoKuanTotal != "")
                {
                    where += string.Format(" and AccountTotal{0}{1}", ddlOADaoKuanTotal, OADaoKuanTotal);
                    //k_Sql += string.Format(" and 1=2", ddlOADaoKuanTotal, OADaoKuanTotal);
                }

                strSql = string.Format(@" select  AccountTotal,Received,Sell_OrderFP.id as FPId,invoice.id,FPNo,Sell_OrderFP.GuestNAME as OA_GuestName ,Sell_OrderFP.Total as OA_Total,RuTime,PONo,
InvoiceNumber, invoice.GuestName,invoice.Total,CreateDate
 from 
(select id,Sell_OrderFP.FPNo,GuestNAME,Total,RuTime,PONo,AccountTotal from Sell_OrderFP Left join (select FPNo,sum(Total) as AccountTotal from TB_ToInvoice where State='通过' group by FPNo) as new_tb on Sell_OrderFP.FPNo=new_tb.FPNo {0} ) as Sell_OrderFP
full join 
(select id,InvoiceNumber,GuestName,Total,CreateDate,Received from 
"+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View as invoice {1} ) as invoice  on Sell_OrderFP.FPNo=Invoice.InvoiceNumber and (Sell_OrderFP.GuestNAME<>invoice.GuestName or (Sell_OrderFP.GuestNAME=invoice.GuestName and Sell_OrderFP.Total<>invoice.Total )) ", where, k_Sql);

                if (isInvoTotalToge)
                {
                    strSql += string.Format(@" where not exists( 
SELECT TB.FPNo AS InvoiceNumber FROM ( select FPNo,sum(Total) as Total,GuestNAME  from Sell_OrderFP where  Status='通过' group by FPNo,GuestNAME  ) AS TB 
INNER JOIN "+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.Total=TB1.Total WHERE Sell_OrderFP.FPNo=TB.FPNo OR invoice.InvoiceNumber=TB.FPNo)");
                }
                else
                {

                    strSql += string.Format(@" where not exists( 
SELECT TB.FPNo AS InvoiceNumber FROM  Sell_OrderFP AS TB 
INNER JOIN "+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.Total=TB1.Total and TB.GuestNAME=TB1.GuestName
WHERE  Status='通过' and Sell_OrderFP.FPNo=TB.FPNo OR invoice.InvoiceNumber=TB.FPNo)");

                }

                if (isInvoTotalToge)
                {
                    strSql = string.Format(@" select SUM(AccountTotal) as AccountTotal,SUM(Received) as Received,0 as FPId,0 as id,FPNo,'' as OA_GuestName,SUM(OA_Total) as OA_Total
 ,MIN(RuTime) as RuTime,InvoiceNumber,GuestName,SUM(Total) AS Total,CreateDate,'' as PONo  from  ( {0} )as t group by InvoiceNumber,GuestName,CreateDate,FPNo {1} order by t.FPNo desc,InvoiceNumber desc  ", strSql,
                                                                                                                                                              (sameFP ? "having SUM(AccountTotal)<>SUM(Received)" : ""));
                }
                else
                {
                    strSql += " order by Sell_OrderFP.FPNo desc,InvoiceNumber desc  ";
                }
            }
            if (compareType == "1")//匹配
            {
                if (InvoFormDate != "")
                {
                    where += string.Format(" and RuTime>='{0} 00:00:00' ", InvoFormDate);
                }
                if (InvToDate != "")
                {
                    where += string.Format(" and RuTime<='{0} 23:59:59' ", InvToDate);
                }


                strSql = string.Format(@"SELECT TB.FPNo AS InvoiceNumber,TB.GuestNAME,TB.Total,AccountTotal,TB1.Received,TB.MaxRuTime FROM (
 select FPNo,GuestNAME,sum(Total) as Total,MAX(RuTime) as MaxRuTime from Sell_OrderFP {0} group by FPNo,GuestNAME ) AS TB INNER JOIN "+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View
 AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.GuestNAME=TB1.GuestName AND TB.Total=TB1.Total Left join (select FPNo,sum(Total) as AccountTotal from TB_ToInvoice where State='通过' group by FPNo) as new_tb on TB.FPNo=new_tb.FPNo where 1=1 ", where);

                if (InvoFormDate != "")
                {
                    strSql += string.Format(" and TB1.CreateDate>='{0} 00:00:00'  ", InvoFormDate);
                }
                if (InvToDate != "")
                {
                    strSql += string.Format(" and TB1.CreateDate<='{0} 23:59:59' ", InvToDate);
                }

                if (GuestName != "")
                {
                    strSql += string.Format(" and TB.GuestNAME like '%{0}%' ", GuestName);
                }
                if (InvoiceNo != "")
                {
                    strSql += string.Format(" and TB.FPNo like '%{0}%' ", InvoiceNo);
                }
                if (InvTotal != "")
                {
                    strSql += string.Format(" and TB.Total {0} {1} ", InvTotalEqu, InvTotal);
                  
                }
                if (KISDaoKuanTotal != "")
                {
                    strSql += string.Format(" and Received{0}{1}", ddlKISDaoKuanTotal, KISDaoKuanTotal);
                }
                if (OADaoKuanTotal != "")
                {
                    strSql += string.Format(" and isnull(AccountTotal,0){0}{1}", ddlOADaoKuanTotal, OADaoKuanTotal);
                }
                if (Isorder == "0")
                {
                    strSql += " and TB1.Isorder is null ";
                }
                if (Isorder == "1")
                {
                    strSql += " and TB1.Isorder=1 ";
                }
                if (tiaojian == "0")
                {
                    strSql += "  and isnull(Received,0)<TB.Total and isnull(AccountTotal,0) <=TB.Total ";
                }
                //if (isInvoTotalToge)
                //{

                strSql += string.Format(@" UNION  SELECT TB.FPNo AS InvoiceNumber,TB.GuestNAME,TB.Total,AccountTotal,TB1.Received,TB.MaxRuTime FROM (
 select FPNo,sum(Total) as Total,GuestNAME,MAX(RuTime) as MaxRuTime from Sell_OrderFP {0} group by FPNo,GuestNAME  ) AS TB INNER JOIN "+InvoiceService.InvoiceServer+ @"KingdeeInvoice.dbo.Invoice_View
 AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.Total=TB1.Total Left join (select FPNo,sum(Total) as AccountTotal from TB_ToInvoice where State='通过' group by FPNo) as new_tb on TB.FPNo=new_tb.FPNo where 1=1 ", where);
                //}


                if (InvoFormDate != "")
                {
                    strSql += string.Format(" and TB1.CreateDate>='{0} 00:00:00'  ", InvoFormDate);
                }
                if (InvToDate != "")
                {
                    strSql += string.Format(" and TB1.CreateDate<='{0} 23:59:59' ", InvToDate);
                }

                if (GuestName != "")
                {
                    strSql += string.Format(" and TB.GuestNAME like '%{0}%' ", GuestName);
                }
                if (InvoiceNo != "")
                {
                    strSql += string.Format(" and TB.FPNo like '%{0}%' ", InvoiceNo);
                }
                if (InvTotal != "")
                {
                    strSql += string.Format(" and TB.Total {0} {1} ", InvTotalEqu, InvTotal);
                   
                }
                if (KISDaoKuanTotal != "")
                {
                    strSql += string.Format(" and Received{0}{1}", ddlKISDaoKuanTotal, KISDaoKuanTotal);
                }
                if (OADaoKuanTotal != "")
                {
                    strSql += string.Format(" and isnull(AccountTotal,0){0}{1}", ddlOADaoKuanTotal, OADaoKuanTotal);
                }
                if (Isorder == "0")
                {
                    strSql += " and TB1.Isorder is null ";
                }
                if (Isorder == "1")
                {
                    strSql += " and TB1.Isorder=1 ";
                }
                if (tiaojian == "0")
                {
                    strSql += "  and isnull(Received,0)<TB.Total and isnull(AccountTotal,0) <=TB.Total ";
                }
                //if (isInvoTotalToge==false)
                //{
                //    strSql = string.Format(" select * from ({0}) as t order by InvoiceNumber desc ", strSql);
                //}
                //else
                //{
                //                    strSql = string.Format(@" select InvoiceNumber,'' as GuestNAME,SUM(Total) as Total,SUM(AccountTotal) as AccountTotal,SUM(Received) as Received,
                //(SELECT GuestNAME+'_'+PONo+',' FROM Sell_OrderFP AS A where Status='通过' 
                //and T.InvoiceNumber=A.FPNo FOR XML PATH('')) as PONo from ({0}) as t  group by InvoiceNumber {1} order by InvoiceNumber desc ", strSql, (sameFP ? "having SUM(AccountTotal)<>SUM(Received)" : ""));
                string having = "";
                if (sameFP || diffDate != "")
                {
                    having = " having ";
                    if (sameFP)
                    {
                        having += "SUM(isnull(AccountTotal,0))<>SUM(Received)";
                        if (diffDate != "")
                        {
                            having += " and ";
                        }
                    }
                    if (diffDate != "")
                    {
                        having += diffDate;
                    }
                }
                strSql = string.Format(@" select InvoiceNumber,avg(Total) as Total,avg(AccountTotal) as AccountTotal,SUM(Received) as Received,max(MaxRuTime) as MaxRuTime from ({0}) as t  group by InvoiceNumber {1} order by InvoiceNumber desc ",
                    strSql, having);

                //}
            }
            if (strSql == "")
            {
                return allInvoiceData;
            }
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = new SqlCommand(strSql, conn);
                using (SqlDataReader objReader = objCommand.ExecuteReader())
                {
                    if (compareType == "0")//不匹配
                    {
                        //    Hashtable hs = new Hashtable();
                        //    Hashtable hs1 = new Hashtable();
                        while (objReader.Read())
                        {
                            var model = ReaderBind_OA_1(objReader);
                            //if (model.Kingdee_Id != 0 || model.OA_FPId != 0)
                            //{
                            allInvoiceData.Add(model);
                            //}
                        }
                    }
                    if (compareType == "1")//匹配
                    {
                        while (objReader.Read())
                        {
                            allInvoiceData.Add(ReaderBind_1(objReader));
                        }
                    }
                    //if (compareType == "2")//全部
                    //{
                    //    while (objReader.Read())
                    //    {
                    //        allInvoiceData.Add(ReaderBind(objReader));
                    //    }
                    //}
                }
            }
            return allInvoiceData;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public AccountReport ReaderBind_1(IDataReader dataReader)
        {
            AccountReport model = new AccountReport();
            object ojb;
            ojb = dataReader["InvoiceNumber"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.All_InvoiceNo = ojb.ToString();
            }
            //ojb = dataReader["GuestNAME"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.All_OAGuestName = ojb.ToString();
            //}

            //ojb = dataReader["PONo"];
            //if (ojb != null && ojb != DBNull.Value)
            //{
            //    model.All_PONO = ojb.ToString();
            //}
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.All_InvoiceTotal = (decimal)ojb;
            }
            else
            {
                model.All_InvoiceTotal = 0;
            }

            ojb = dataReader["AccountTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.All_OATotal = (decimal)ojb;
            }
            else
            {
                model.All_OATotal = 0;
            }

            ojb = dataReader["Received"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.All_AccountTotal = (decimal)ojb;
            }
            else
            {
                model.All_AccountTotal = 0;
            }
            ojb = dataReader["MaxRuTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.MaxDate = (DateTime)ojb;
            }
         
            
            return model;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public AccountReport ReaderBind_OA_1(IDataReader dataReader)
        {
            AccountReport model = new AccountReport();
            object ojb;

            //OA
            ojb = dataReader["FPId"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OA_FPId = Convert.ToInt32(ojb);
            }

            //金蝶
            ojb = dataReader["id"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Kingdee_Id = Convert.ToInt32(ojb);
            }

            //if (model.OA_FPId != 0 && !oa_hs.ContainsKey(model.OA_FPId))
            //{
            //    oa_hs.Add(model.OA_FPId, null);
            ojb = dataReader["FPNo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OA_InvoiceNo = ojb.ToString();
            }
            ojb = dataReader["OA_GuestName"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OA_GuestName = ojb.ToString();
            }
            ojb = dataReader["OA_Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OA_InvoiceTotal = (decimal)ojb;
            }
             ojb = dataReader["AccountTotal"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OA_AccountTotal = (decimal)ojb;
            }
            ojb = dataReader["Received"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Kingdee_AccountTotal = (decimal)ojb;
            }
             
            ojb = dataReader["RuTime"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OA_InvoiceDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ojb));
            }
            ojb = dataReader["PONo"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.OA_PONO = ojb.ToString();
            }
            //}
            //else
            //{
            //    model.OA_FPId = 0;
            //}


            //if (model.Kingdee_Id != 0 && !kingdee_hs.ContainsKey(model.Kingdee_Id))
            //{
            //    kingdee_hs.Add(model.Kingdee_Id, null);
            ojb = dataReader["InvoiceNumber"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Kingdee_InvoiceNo = ojb.ToString();
            }
            ojb = dataReader["GuestNAME"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Kingdee_GuestName = ojb.ToString();
            }
            ojb = dataReader["Total"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Kingdee_InvoiceTotal = (decimal)ojb;
            }
            ojb = dataReader["CreateDate"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.Kingdee_InvoiceDate = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ojb));
            }
            //}
            //else
            //{
            //    model.Kingdee_Id = 0;
            //}


            return model;
        }
    }
}
