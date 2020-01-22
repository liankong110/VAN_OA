using System;
using System.Collections.Generic;
using System.Data;
using VAN_OA.Model;
using  System.Text;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using System.Data.Common;
using VAN_OA.Dal.JXC;
using System.Collections;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.KingdeeInvoice;
using VAN_OA.Model.JXC;

namespace VAN_OA.JXC
{
    public partial class WFPOAssess_ProNo : BasePage
    {

        protected Hashtable noSellAndCaiGoodsList = new Hashtable();
        protected List<HashTableModel> resut_SellGoodsList = new List<HashTableModel>();

        protected DataSet ds;
        protected DataTable dt_Fund;
        public DataTable dt_GetSumPOTotal;
        public List<JXC_REPORTTotal> pOOrderList;

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string where = "";
            if (ddlCompany.Text != "-1")
            {
                where = string.Format(" and tb_User.CompanyCode ='{0}'", ddlCompany.Text.Split(',')[0]);
            }
            List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
            VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
            user = userSer.getAllUserByPOList(where);
            user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });

            ddlUser.DataSource = user;
            ddlUser.DataBind();
            ddlUser.DataTextField = "LoginName";
            ddlUser.DataValueField = "Id";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComCode += "," + m.ComSimpName;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComCode = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

                bool showAll = true;
                if (SysObj.IfShowAll("销售月考核", Session["currentUserId"], "ShowAll") == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }

                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll("销售月考核", Session["currentUserId"], "WFScanDepartList") == true)
                {
                    ViewState["WFScanDepartList"] = false;
                    WFScanDepartList = false;
                }

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                if (showAll == true)
                {
                    user = userSer.getAllUserByPOList();
                    user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                }
                else if (WFScanDepartList == true)
                {
                    user = userSer.getAllUserByLoginName(string.Format(" and loginIPosition in (select loginIPosition from tb_User where id={0}) and loginIPosition<>''", Session["currentUserId"]));
                    user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = 0 });
                }
                else
                {
                    var model = Session["userInfo"] as User;
                    user.Insert(0, model);
                }
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";

                ds=new DataSet();
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable()); 
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());

               
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Show();
        }


        private void Show()
        {
            string where = "";
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                where += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00'", txtFrom.Text);
            }
            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                where += string.Format(" and CG_POOrder.PODate<='{0} 23:59:59'", txtTo.Text);
            }
            if (ddlCompany.Text == "-1" && ddlUser.Text != "-1")
            {
                where += string.Format(" and exists (select id from tb_User where ID={0} and CG_POOrder.appName=id)", ddlUser.Text);
            }
            if (ddlCompany.Text != "-1")
            {
                string where1 = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[0]);

                if (ddlUser.Text != "-1")
                {
                    where1 += string.Format(" and ID={0} ", ddlUser.Text);
                }
                where += string.Format(" and exists (select id from tb_User where {0} and CG_POOrder.appName=id)", where1);
            }
            if (ddlIsClose.Text != "-1")
            {
                where += " and IsClose=" + ddlIsClose.Text;
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                where += " and JieIsSelected=" + ddlJieIsSelected.Text;
            }
            if (ddlSpecial.Text!="-1")
            {
                where += string.Format(" and CG_POOrder.IsSpecial={0}", ddlSpecial.Text);                
            }
            ds = DBHelp.getDataSet(GetSql("").ToString());
            noSellAndCaiGoodsList = new RuSellReportService().getHT(where);

            resut_SellGoodsList = new Model.HashTableModel().HashTableToList(noSellAndCaiGoodsList);
            resut_SellGoodsList.Sort();

            dt_Fund = DBHelp.getDataTable(GetFundWrong());
            GetSumPOTotal();
            //dt_GetSumPOTotal = DBHelp.getDataTable();
        }
        /// <summary>
        /// 罗列销售报表汇总中项目金额<>销售金额的项目编号
        /// </summary>
        /// <returns></returns>
        public void GetSumPOTotal()
        {
            string sql = " ";
         
            if (txtFrom.Text != "")
            {
                sql += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00'", txtFrom.Text);
            }
            if (txtTo.Text != "")
            {
                sql += string.Format(" and CG_POOrder.PODate<='{0} 23:59:59'", txtTo.Text);
            }
            if (ddlCompany.Text == "-1" && ddlUser.Text != "-1")
            {
                sql += string.Format(" and CG_POOrder.AppName={0}", ddlUser.Text);
            }

            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[0]);
                sql += string.Format(" and exists (select id from tb_User where IFZhui=0 and {0} and CG_POOrder.appName=id)", where);
            }
            
            if (ddlIsClose.Text != "-1")
            {
                sql += " and IsClose=" + ddlIsClose.Text;
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                sql += " and JieIsSelected=" + ddlJieIsSelected.Text;
            }
            if (ddlSpecial.Text != "-1")
            {
                sql += string.Format(" and CG_POOrder.IsSpecial={0}", ddlSpecial.Text);
            }
            pOOrderList = new JXC_REPORTService().GetListArray_Total(sql, "", " where SumPOTotal <> isnull(goodSellTotal,0)");

           
           
          
           
           
            //if (cbIsSpecial.Checked)
            //{
            //    where += " and IsSpecial=0";
            //}

           

//            string sql = string.Format(@"SELECT tb.PONo,CG_POOrder.AE FROM 
//( select PONo,sum(goodSellTotal) as goodSellTotal from JXC_REPORT group by PONo) as tb
//left join POTotal_SumView on POTotal_SumView.PONo=tb.PONo
//left join CG_POOrder on CG_POOrder.PONo=tb.PONo and ifzhui=0  and CG_POOrder.Status='通过' 
//where SumPOTotal <> goodSellTotal {0}
//ORDER BY  tb.PONo", where);
//            return sql;
        }

        /// <summary>
        /// 申请请款单 和 报销单 信息单号
        /// </summary>
        /// <param name="pono"></param>
        /// <param name="ae"></param>
        /// <returns></returns>
        private string GetFundWrong()
        {
            string where = "";
            if (txtFrom.Text != "")
            {
                where += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00'", txtFrom.Text);
            }
            if (txtTo.Text != "")
            {
                where += string.Format(" and CG_POOrder.PODate<='{0} 23:59:59'", txtTo.Text);
            }
            if (ddlCompany.Text == "-1" && ddlUser.Text != "-1")
            {
                where += string.Format(" and exists (select id from tb_User where ID={0} and CG_POOrder.appName=id)", ddlUser.Text);
            }
            if (ddlCompany.Text != "-1")
            {
                string where1 = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[0]);

                if (ddlUser.Text != "-1")
                {
                    where1 += string.Format(" and ID={0} ", ddlUser.Text);
                }
                where += string.Format(" and exists (select id from tb_User where {0} and CG_POOrder.appName=id)", where1);
            }
            if (ddlIsClose.Text != "-1")
            {
                where += " and IsClose=" + ddlIsClose.Text;
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                where += " and JieIsSelected=" + ddlJieIsSelected.Text;
            }
            //if (cbIsSpecial.Checked)
            //{
            //    where += " and IsSpecial=0";
            //}
            if (ddlSpecial.Text != "-1")
            {
                where += string.Format(" and CG_POOrder.IsSpecial={0}", ddlSpecial.Text);
            }
            string sql = string.Format(@"select * from (
select proId,CG_POOrder.AE,tb_FundsUse.PONo,allE_id,tb_EForm.id from
tb_EForm left join tb_FundsUse  on tb_FundsUse.id=tb_EForm.allE_id
left join CG_POOrder on CG_POOrder.pono=tb_FundsUse.PONo and Status='通过' and IFZhui=0 
where proId=9 and tb_EForm.state='执行中' {0}
union all
select proId,CG_POOrder.AE,Tb_DispatchList.PONo,allE_id,tb_EForm.id from
tb_EForm
left join Tb_DispatchList  on Tb_DispatchList.id=tb_EForm.allE_id
left join CG_POOrder on CG_POOrder.pono=Tb_DispatchList.PONo and Status='通过' and IFZhui=0 
where proId=12 and tb_EForm.state='执行中' {0}
union all
select proId,CG_POOrder.AE,tb_FundsUse.PONo,allE_id,tb_EForm.id from
tb_EForm 
left join tb_EForms  on tb_EForm.id=tb_EForms.e_Id
left join tb_FundsUse  on tb_FundsUse.id=tb_EForm.allE_id
left join CG_POOrder on CG_POOrder.pono=tb_FundsUse.PONo and Status='通过' and IFZhui=0 
where proId=9 and tb_EForm.state='不通过'  --and  roleName='财务'
{0}
union all
select proId,CG_POOrder.AE,Tb_DispatchList.PONo,allE_id,tb_EForm.id from
tb_EForm 
left join tb_EForms  on tb_EForm.id=tb_EForms.e_Id
left join Tb_DispatchList  on Tb_DispatchList.id=tb_EForm.allE_id
left join CG_POOrder on CG_POOrder.pono=Tb_DispatchList.PONo and Status='通过' and IFZhui=0 
where proId=12 and tb_EForm.state='不通过' --and  roleName='财务' 
{0}) as t order by PONO  ", where);
            return sql;
        }


        //发票有误- 单号
        private string GetFPNOWrong(string where)
        {
            string DateWhere = "";
            if (txtFrom.Text != "")
            {
                DateWhere += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00'", txtFrom.Text);
            }
            if (txtTo.Text != "")
            {
                DateWhere += string.Format(" and CG_POOrder.PODate<='{0} 23:59:59'", txtTo.Text);
            }
            if (ddlIsClose.Text != "-1")
            {
                where += " and IsClose=" + ddlIsClose.Text;
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                where += " and JieIsSelected=" + ddlJieIsSelected.Text;
            }
            //if (cbIsSpecial.Checked)
            //{
            //    where += " and IsSpecial=0";
            //}
            if (ddlSpecial.Text != "-1")
            {
                where += string.Format(" and CG_POOrder.IsSpecial={0}", ddlSpecial.Text);
            }
            string sql = string.Format(@"select * from (
--1.   到款金额>项目金额  ；.不含特殊勾上 ；到款金额合并 勾上 ；.发票状态：所有  查询出来的项目 ------第一个画面
select newtable1.PONo,CG_POOrder.AE,5 as T
from(select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过'   and IsSpecial=0  group by PONo ) as newtable1
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on newtable1.PONo= newtable2.PONo
left join (select pono ,sum(Total) as Total from  TB_ToInvoice 
where  1=1  and (State<>'不通过' or State is null) group by PONo )as newtable4 on newtable1.PONo= newtable4.PONo 
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0 
where  ISNULL(Total,0) >newtable1.POTotal-isnull(TuiTotal,0)--, ,CG_POOrder.AppName
UNION 
--2.   不含特殊 + 不含税 勾上；到款金额合并 勾上 ；.发票状态：已开全票 + 未开全票 查询出来的项目-----第二个画面
select  newtable1.PONo, CG_POOrder.AE,6 as T
from(select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过' and POStatue3='已开票'  and IsSpecial=0 and IsPoFax=0  group by PONo ) as newtable1
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0 --,CG_POOrder.AppName
--2.1
UNION 
select newtable1.PONo, CG_POOrder.AE,6 as T
from(select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过' and POStatue3='已开票'  and IsSpecial=0 and IsPoFax=0  group by PONo ) as newtable1
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on newtable1.PONo= newtable2.PONo
left join( select PONo, sum(Total) as hadFpTotal from Sell_OrderFP where Status='通过' group by PONo) as newtable3 on newtable1.PONo= newtable3.PONo
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0 
where (hadFpTotal<>0 and newtable1.POTotal-isnull(TuiTotal,0)>hadFpTotal) --,CG_POOrder.AppName
--3.  不含特殊 +含税 勾上；到款金额合并 勾上 ；项目金额<5；.发票状态：所有  查询出来的项目-----第三个画面
UNION 
 select newtable1.PONo, CG_POOrder.AE,7 as T
from(select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过' and POStatue3='已开票'  and   IsSpecial=0 and IsPoFax=1  group by PONo ) as newtable1
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on newtable1.PONo= newtable2.PONo
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0 
where newtable1.POTotal-isnull(TuiTotal,0) < 5 --,CG_POOrder.AppName
--4. 到款金额>= 项目金额，发票状态=未开票，CHECKBOX 含税勾上，不含特殊+到款单合并 勾上，的查询结果的项目
UNION 
select newtable1.PONo, CG_POOrder.AE,4 as T
from(select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过'   and IsSpecial=0 and IsPoFax=1 group by PONo ) as newtable1
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on newtable1.PONo= newtable2.PONo
left join( select PONo, sum(Total) as hadFpTotal from Sell_OrderFP where Status='通过' group by PONo) as newtable3 on newtable1.PONo= newtable3.PONo
left join (select pono ,sum(Total) as Total from  TB_ToInvoice where  1=1  and (State<>'不通过' or State is null) group by PONo )as newtable4 on newtable1.PONo= newtable4.PONo 
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0 
where  ISNULL(Total,0) >=newtable1.POTotal-isnull(TuiTotal,0) and hadFpTotal is null 
--,CG_POOrder.AppName

--就是开具发票的金额>项目总金额 的项目
UNION 
select Sell_OrderFP.PONo,tb_User.loginName as AE,1 as T
from Sell_OrderFP left join tb_User on tb_User.id=CreateUserId left join POFP_View on POFP_View.pono=Sell_OrderFP.pono 
where  1=1  and Status<>'不通过'  and SumPoTotal-TuiTotal<sumTotal

--项目的金额必须>销售汇总的总金额项目编号 罗列在销售月考核中
UNION 
select  allNewTb.PONo,AE,2 as T from (select  CG_POOrder.PONo, sum(goodSellTotal) as goodSellTotal, AE from CG_POOrder  left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo  
 left join (select max(DaoKuanDate)  as MaxDaoKuanDate,PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  
 where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
 left join (select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse left join Sell_OrderOutHouses
on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id where  Status='通过' group by PONO ) as SellOut on CG_POOrder.PONo=SellOut.PONO
 left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) as ntb2 on CG_POOrder.PONo=ntb2.PONo 
 where ifzhui=0  and CG_POOrder.Status='通过'  {0} 
 and EXISTS (select ID from CG_POOrder where --AppName=5 AND 
 PONO=JXC_REPORT.PONO  and IsSpecial=0 ) 
 GROUP BY  CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate ,CG_POOrder.GuestName ,AE,INSIDE,FPTotal,AEPer,INSIDEPer,MinOutDate,
 MaxDaoKuanDate,ZhangQiTotal,CG_POOrder.IsClose,CG_POOrder.GuestType, CG_POOrder.GuestPro   ) as allNewTb left join POTotal_SumView on  allNewTb.PONo=POTotal_SumView.pono  where SumPOTotal <> goodSellTotal 
--发票对比
UNION 
select tb.PONo,AE,3 as T from ( select  PONo from 
(select id,FPNo,GuestNAME,Total,RuTime,PONo from Sell_OrderFP  where Status='通过' 
 and exists(select id from CG_POOrder where Status='通过' and IsSpecial=0  
 {0} and  CG_POOrder.PONO=Sell_OrderFP.PONO  and ifzhui=0) ) as Sell_OrderFP full join 
(select id,InvoiceNumber,GuestName,Total,CreateDate from 
 " + InvoiceService.InvoiceServer + @"KingdeeInvoice.dbo.Invoice as invoice  where 1=1  and Isorder is null  ) as invoice  
on Sell_OrderFP.FPNo=Invoice.InvoiceNumber and (Sell_OrderFP.GuestNAME<>invoice.GuestName 
or (Sell_OrderFP.GuestNAME=invoice.GuestName and Sell_OrderFP.Total<>invoice.Total )) where not exists( 
SELECT TB.FPNo AS InvoiceNumber FROM ( select FPNo,sum(Total) as Total,GuestNAME  from Sell_OrderFP where  Status='通过' group by FPNo,GuestNAME  ) AS TB 
INNER JOIN  " + InvoiceService.InvoiceServer + @"KingdeeInvoice.dbo.Invoice AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.Total=TB1.Total 
WHERE Sell_OrderFP.FPNo=TB.FPNo OR invoice.InvoiceNumber=TB.FPNo) 
) as tb left join CG_POOrder on tb.PONo=CG_POOrder.PONo and IFZhui=0 where tb.PONo is not null
) as tb where 1=1 ", DateWhere);
            if (ddlCompany.Text == "-1" && ddlUser.Text != "-1")
            {
                sql += string.Format(" and exists (select id from tb_User where ID={0} and AE=loginName)", ddlUser.Text);
            }
            if (ddlCompany.Text != "-1")
            {
                string where1 = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[0]);

                if (ddlUser.Text != "-1")
                {
                    where1 += string.Format(" and ID={0} ", ddlUser.Text);
                }
                sql += string.Format(" and exists (select id from tb_User where {0} and AE=loginName)", where1);
            }


            return sql + "  order by PONo";
        }

        private StringBuilder GetSql(string userId)
        {
            string where = "";
            if (txtFrom.Text != "")
            {
                where += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00'", txtFrom.Text);
            }
            if (txtTo.Text != "")
            {
                where += string.Format(" and CG_POOrder.PODate<='{0} 23:59:59'", txtTo.Text);
            }
            if (ddlIsClose.Text != "-1")
            {
                where += " and IsClose=" + ddlIsClose.Text;
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                where += " and JieIsSelected=" + ddlJieIsSelected.Text;
            }
            //if (cbIsSpecial.Checked)
            //{
            //    where += " and IsSpecial=0";
            //}
            if (ddlSpecial.Text != "-1")
            {
                where += string.Format(" and CG_POOrder.IsSpecial={0}", ddlSpecial.Text);
            }
            if (ddlCompany.Text == "-1" && ddlUser.Text != "-1")
            {
                where += string.Format(" and exists (select id from tb_User where ID={0} and CG_POOrder.appName=id)", ddlUser.Text);
            }
            if (ddlCompany.Text != "-1")
            {
                string where1 = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[0]);

                if (ddlUser.Text != "-1")
                {
                    where1 += string.Format(" and ID={0} ", ddlUser.Text);
                }
                where += string.Format(" and exists (select id from tb_User where {0} and CG_POOrder.appName=id)", where1);              
            }

            if (userId != "")
            {
                where += " and CG_POOrder.appName=" + userId;
            }
           
            var sqlInfo = new StringBuilder();
            //--出库单签回单-------需要显示 每个AE的未签回单  项目编号
            sqlInfo.AppendFormat(
                @"SELECT CG_POOrder.PONO,CG_POOrder.AE FROM (select Sell_OrderOutHouse.PONo from Sell_OrderOutHouse  
left join Sell_OrderOutHouseBack on Sell_OrderOutHouse.ProNo=Sell_OrderOutHouseBack.SellProNo 
where  Sell_OrderOutHouse.Status='通过' and (Sell_OrderOutHouseBack.Status<>'不通过' or Sell_OrderOutHouseBack.Status is null)  and (BackType=1 or Sell_OrderOutHouseBack.Id is null)
 group by Sell_OrderOutHouse.PONo) as newtable1
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0  where 1=1 " + where + ";");

            //-- 发票签回单 --------需要显示 每个AE 的未签回单  项目编号
            sqlInfo.AppendFormat(
                @"SELECT CG_POOrder.PONO,CG_POOrder.AE FROM (select Sell_OrderFP.PONo from Sell_OrderFP  left join Sell_OrderFPBack on Sell_OrderFP.ID=Sell_OrderFPBack.PID 
where Sell_OrderFP.Status='通过' and Sell_OrderFPBack.Status<>'不通过' and (FPBackType=1 or Sell_OrderFPBack.Id is null)
group by Sell_OrderFP.PONo ) as newtable1 
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0  where IsSpecial=0 " + where + ";");

            //--入库未出清单------需要显示  每个AE的 列表 项目编号
            //sqlInfo.AppendFormat(
            //    @"select PONo,AE  from NoSellOutGoods_2  {0} " + t3 + " group by PONo,AE order by PONo;", selectUserId == "-1" ? "" : " where AppName=" + selectUserId);
            sqlInfo.AppendFormat("select 1;");
            
            //--  出库未开发票和未开全发票 清单-----需要显示  每个AE的 列表 项目编号
            sqlInfo.AppendFormat(@"select tb1.PONO, tb1.AE  from (
select  CG_POOrder.PONo , CG_POOrder.AE  from CG_POOrder
where   Status='通过' and CG_POOrder.GuestName not like '本部门%' and IsSpecial=0  and IsPoFax=1 and (POStatue4='' or POStatue4 is null) {0}
group by CG_POOrder.PONo , CG_POOrder.AE
) as tb1 
left join POTotal_View on POTotal_View.PONo=tb1.PONo--查询 项目金额和 已开发票金额
where  hadFpTotal is  null 
union
select tb1.PONO, tb1.AE  from (
select  CG_POOrder.PONo , CG_POOrder.AE  from CG_POOrder 
where  Status='通过'   and CG_POOrder.GuestName not like '本部门%' and IsSpecial=0  and IsPoFax=1 and POStatue3<>'已开票' and (POStatue4='' or POStatue4 is null) {0}
group by CG_POOrder.PONo , CG_POOrder.AE
)as tb1-- 项目基本信息汇总
left join POTotal_View on POTotal_View.PONo=tb1.PONo--查询 项目金额和 已开发票金额
where  hadFpTotal is not null ;", where);

            //--   采购未检验清单----需要显示每个AE 的列表 项目编号。
            sqlInfo.AppendFormat(@"SELECT newtable1.PONO,CG_POOrder.AE FROM (select CAI_POOrder.PONo from CAI_POCai
left join CAI_POOrder on CAI_POCai.Id=CAI_POOrder.Id
left join
(
select  CaiId,SUM(CheckNum) as totalOrderNum from CAI_OrderChecks left join CAI_OrderCheck on  CAI_OrderCheck.id=CAI_OrderChecks.CheckId
where CaiId<>0  and CAI_OrderCheck.status<>'不通过'
group by CaiId
)
as newtable on CAI_POCai.Ids=newtable.CaiId 
where (CAI_POCai.Num>newtable.totalOrderNum or totalOrderNum is null)
and status='通过' and lastSupplier<>'库存'
group by CAI_POOrder.PONo) as newtable1 left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0 where 1=1" + where + ";");
            //到款单
            sqlInfo.AppendFormat(@"select newtable1.PONo,CG_POOrder.AE from(
select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过' and IsSpecial=0  group by PONo ) as newtable1
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on newtable1.PONo= newtable2.PONo 
left join (select pono ,sum(Total) as Total from  TB_ToInvoice where  1=1  and (State<>'不通过' or State is null) group by PONo )as newtable4 on newtable1.PONo= newtable4.PONo 
left join( select min(RuTime) as minOutTime, PONo from Sell_OrderOutHouse group by PONo) as newtable5 on newtable1.PONo= newtable5.PONo
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0 
 where 1=1  and ISNULL(Total,0) <newtable1.POTotal-isnull(TuiTotal,0) and datediff(d,minOutTime,getdate())>90  " + where + ";");

            //到款单
            sqlInfo.AppendFormat(@"select newtable1.PONo  ,AE,hadFpTotal ,POTotal-isnull(TuiTotal,0) as POTotal, datediff(dd, dateadd(day,sumJieSuan-30 ,minOutTime),getdate() ) as chaDays from(
select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过'  and IsSpecial=0 {0}
group by PONo) as newtable1 
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on newtable1.PONo= newtable2.PONo
left join( select PONo, sum(Total) as hadFpTotal from Sell_OrderFP where Status='通过' group by PONo)
as newtable3 on newtable1.PONo= newtable3.PONo
left join( select min(RuTime) as minOutTime,PONo from Sell_OrderOutHouse group by PONo) 
as newtable5 on newtable1.PONo= newtable5.PONo left join 
(select pono,AppName, case  when  isnumeric(POPayStype)=1 then Convert(decimal, POPayStype)  else 0 end
as sumJieSuan ,ae from CG_POOrder where Status='通过' and IFZhui=0) as newtable6
on newtable6.pono=newtable1.PONo 
left join (select pono ,sum(Total) as Total
from  TB_ToInvoice where  1=1  and (State<>'不通过' or State is null) group by PONo )as newtable4 on newtable1.PONo= newtable4.PONo 
where getdate()>=dateadd(day,sumJieSuan-30 ,minOutTime) and ISNULL(Total,0) <newtable1.POTotal-isnull(TuiTotal,0)  order by newtable1.PONo ;",where);
            
            //发票金额》项目金额
           // sqlInfo.AppendFormat("select CG_POOrder.PONo,CG_POOrder.AE from POFP_View left join CG_POOrder on CG_POOrder.pono=POFP_View.PONo and Status='通过' and IFZhui=0  where sumPOTOTAL-TUITOTAL<SUMTOTAL {0} " + t8 + ";", selectUserId == "-1" ? "" : "and CG_POOrder.AppName=" + selectUserId);
            sqlInfo.AppendFormat(GetFPNOWrong(where));
            //项目未出清单
            sqlInfo.AppendFormat(" select PONotCaiView.PONo,PONotCaiView.AE from PONotCaiView left join CG_POOrder on PONotCaiView.PONo=CG_POOrder.PONo and IFZhui=0   where lastNum>0 " + where + ";");
            return sqlInfo;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=结算预审.xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文
            this.EnableViewState = false;
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            Show();
            Panel2.RenderControl(hw);
         
            Response.Write(sw.ToString());
            Response.End();
        }

         

    }
}
