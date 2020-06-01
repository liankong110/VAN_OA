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
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class WFPOAssess : BasePage
    {

        protected Hashtable noSellAndCaiGoodsList = new Hashtable();
        protected List<HashTableModel> resut_SellGoodsList = new List<HashTableModel>();

        protected DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TB_ModelService modelService = new TB_ModelService();
                var _modelList = modelService.GetListArray("");
                _modelList.Insert(0, new TB_Model { Id = -1, ModelName = "全部" });
                ddlModel.DataSource = _modelList;
                ddlModel.DataBind();
                ddlModel.DataTextField = "ModelName";
                ddlModel.DataValueField = "ModelName";

                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
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
                ds.Tables.Add(new DataTable());
                ds.Tables.Add(new DataTable());
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCKFPDays.Text)&&CommHelp.VerifesToNum(txtCKFPDays.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('出库发票缺票日 格式错误！');</script>");
                return;
            }
            if (!string.IsNullOrEmpty(txtYFWDTotal.Text) && CommHelp.VerifesToNum(txtYFWDTotal.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('预付未到票金额 格式错误！');</script>");
                return;
            }
            if (!string.IsNullOrEmpty(txtCZTXDays.Text) && CommHelp.VerifesToNum(txtCZTXDays.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('催账提醒未到日 格式错误！');</script>");
                return;
            }
            if (!string.IsNullOrEmpty(txtYSDKDays.Text) && CommHelp.VerifesToNum(txtYSDKDays.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('应收款未到日 格式错误！');</script>");
                return;
            }
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
            }

            

            ds = DBHelp.getDataSet(GetSql(ddlUser.Text,ddlUser.SelectedItem.Text).ToString());
            if (ddlUser.Text == "-1")
            {
                noSellAndCaiGoodsList = new RuSellReportService().getHT("",txtPONo.Text.Trim(),ddlCompany.Text,ddlSpecial.Text,ddlModel.Text);
            }
            else
            {
                noSellAndCaiGoodsList = new RuSellReportService().getHT(ddlUser.SelectedItem.Text, txtPONo.Text.Trim(), ddlCompany.Text, ddlSpecial.Text, ddlModel.Text);
            }
            resut_SellGoodsList = new Model.HashTableModel().HashTableToList(noSellAndCaiGoodsList);
            resut_SellGoodsList.Sort();


        }

        /// <summary>
        /// 发票漏填
        /// </summary>
        /// <param name="pono"></param>
        /// <param name="ae"></param>
        /// <returns></returns>
        public string GetEmptyFPNO(string pono, string ae)
        {
            int month = DateTime.Now.Month;
            string QuartNo = "";
            if (1 <= month && month <= 3)
            {
                QuartNo = "1";

            }
            else if (4 <= month && month <= 6)
            {
                QuartNo = "2";
            }
            else if (7 <= month && month <= 9)
            {
                QuartNo = "3";
            }
            else if (10 <= month && month <= 12)
            {
                QuartNo = "4";
            }

            string sql = string.Format(@"select InvoiceNumber,datediff(d,CreateDate,getdate()) as diffDays,loginName from 
 " + InvoiceService.InvoiceServer + @"KingdeeInvoice.dbo.Invoice_View as invoice 
 left join TB_GuestTrack on TB_GuestTrack.GuestName=invoice.GuestName and YearNo='{0}' and QuartNo={1}
 LEFT JOIN tb_User on tb_User.id=TB_GuestTrack.AE 
  where  Isorder is null and not exists(SELECT TB.FPNo AS InvoiceNumber FROM ( select FPNo,sum(Total) as Total,GuestNAME  from Sell_OrderFP where  Status='通过' group by FPNo,GuestNAME  ) AS TB 
INNER JOIN  " + InvoiceService.InvoiceServer + @"KingdeeInvoice.dbo.Invoice_View AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.Total=TB1.Total WHERE  invoice.InvoiceNumber=TB.FPNo)", DateTime.Now.Year, QuartNo);

            if (ae != "-1" && ae != "全部" && !string.IsNullOrEmpty(ae))
            {
                sql += string.Format(" and tb_User.loginName = '{0}'", ae);
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and {0}", where);
            }

            return sql+" order by InvoiceNumber desc ";
        }

        /// <summary>
        /// 预付未到票
        /// </summary>
        private string GetFPWei(string pono, string ae)
        {
            string sql = @"select CAI_POOrder.PONo,CAI_POOrder.AE,TB_SupplierInvoice.LastSupplier,sum(SupplierInvoiceTotal) as SupplierInvoiceTotal from  TB_SupplierInvoices  
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.Id
left join CAI_OrderInHouses  on  TB_SupplierInvoices.RuIds= CAI_OrderInHouses.Ids 
left join (select OrderCheckIds,sum(GoodNum) as supplierTuiGoodNum from CAI_OrderOutHouses left join CAI_OrderOutHouse on CAI_OrderOutHouses.id=CAI_OrderOutHouse.Id where status<>'不通过' group by OrderCheckIds )
as tb1 on tb1.OrderCheckIds=CAI_OrderInHouses.ids
left join CAI_OrderChecks on CAI_OrderChecks.Ids=CAI_OrderInHouses.OrderCheckIds
left join CAI_POCai on CAI_POCai.Ids=CAI_OrderChecks.CaiId
left join CAI_POOrder on CAI_POOrder.Id=CAI_POCai.Id
left join CG_POOrder on CG_POOrder.PONo=CAI_POOrder.PONo and CG_POOrder.IFZhui=0
where  IsYuFu=1  and CAI_OrderInHouses.Ids is not null  
and TB_SupplierInvoice.Status<>'不通过'
and IsHanShui=1 and SupplierFPNo=''  ";
            if (!string.IsNullOrEmpty(pono))
            {
                sql += string.Format(" and CAI_POOrder.pono like '%{0}%'", pono);
            }
            if (ae != "-1" && ae != "全部" && !string.IsNullOrEmpty(ae))
            {
                sql += string.Format(" and CAI_POOrder.ae = '{0}'", ae);
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and CAI_POOrder.ae IN(select loginName from tb_User where {0})", where);
            }
            if (ddlSpecial.Text != "-1")
            {
                sql += string.Format(" and CG_POOrder.IsSpecial = {0} ", ddlSpecial.Text);
            }
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and CG_POOrder.Model ='{0}' ", ddlModel.Text);
            }

            if (txtYFWDUnit.Text.Trim() != "")
            {
                sql += string.Format(" and TB_SupplierInvoice.lastSupplier like '%{0}%' ", txtYFWDUnit.Text.Trim());
            }
            sql= sql + "  group by  CAI_POOrder.PONo,CAI_POOrder.AE,TB_SupplierInvoice.LastSupplier ";
            if (ddlYFWDTotal.Text != "-1" && !string.IsNullOrEmpty(txtYFWDTotal.Text))
            {
                sql += string.Format(" having sum(SupplierInvoiceTotal){0}{1}", ddlYFWDTotal.Text, txtYFWDTotal.Text);
            }
            sql += " order by CAI_POOrder.PONo";
            return sql;
        }

//        private string GetFundWrong(string pono, string ae)
//        {
//            string where = "";
//            if (txtPONo.Text != "")
//            {
//                where += string.Format(" and CG_POOrder.PONO like '%{0}%'",txtPONo.Text);
//            }
//            string sql = string.Format(@"
//select proId,e_No,CG_POOrder.AE from
//tb_EForm left join tb_FundsUse  on tb_FundsUse.id=tb_EForm.allE_id
//left join CG_POOrder on CG_POOrder.pono=tb_FundsUse.PONo and Status='通过' and IFZhui=0 
//where proId=9 and tb_EForm.state='执行中'
//union all
//select proId,e_No,CG_POOrder.AE from
//tb_EForm
//left join Tb_DispatchList  on Tb_DispatchList.id=tb_EForm.allE_id
//left join CG_POOrder on CG_POOrder.pono=Tb_DispatchList.PONo and Status='通过' and IFZhui=0 
//where proId=12 and tb_EForm.state='执行中'
//union all
//select proId,e_No,CG_POOrder.AE from
//tb_EForm 
//left join tb_EForms  on tb_EForm.id=tb_EForms.e_Id
//left join tb_FundsUse  on tb_FundsUse.id=tb_EForm.allE_id
//left join CG_POOrder on CG_POOrder.pono=tb_FundsUse.PONo and Status='通过' and IFZhui=0 
//where proId=9 and tb_EForm.state='不通过' group by proId,e_No,CG_POOrder.AE having COUNT(*)>1
//union all
//select proId,e_No,CG_POOrder.AE from
//tb_EForm 
//left join tb_EForms  on tb_EForm.id=tb_EForms.e_Id
//left join Tb_DispatchList  on Tb_DispatchList.id=tb_EForm.allE_id
//left join CG_POOrder on CG_POOrder.pono=Tb_DispatchList.PONo and Status='通过' and IFZhui=0 
//where proId=12 and tb_EForm.state='不通过' group by proId,e_No,CG_POOrder.AE having COUNT(*)>1");
//            return sql;
//        }



        //发票有误
        private string GetFPNOWrong(string pono,string ae)
        {
            string specialSql = "";
            if (ddlSpecial.Text != "-1")
            {
                specialSql = " and IsSpecial="+ ddlSpecial.Text;
            }
            if (ddlModel.Text != "全部")
            {
                specialSql += string.Format(" and Model ='{0}' ", ddlModel.Text);
            }
            string sql= string.Format(@"select * from (
--1.   到款金额>项目金额  ；.不含特殊勾上 ；到款金额合并 勾上 ；.发票状态：所有  查询出来的项目 ------第一个画面
select newtable1.PONo,CG_POOrder.AE,5 as T
from(select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过' "+ specialSql + @" group by PONo ) as newtable1
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on newtable1.PONo= newtable2.PONo
left join (select pono ,sum(Total) as Total from  TB_ToInvoice 
where  1=1  and (State<>'不通过' or State is null) group by PONo )as newtable4 on newtable1.PONo= newtable4.PONo 
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0 
where  ISNULL(Total,0) >newtable1.POTotal-isnull(TuiTotal,0)--, ,CG_POOrder.AppName
UNION 
--2.   不含特殊 + 不含税 勾上；到款金额合并 勾上 ；.发票状态：已开全票 + 未开全票 查询出来的项目-----第二个画面
select  newtable1.PONo, CG_POOrder.AE,6 as T
from(select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过' and POStatue3='已开票'  " + specialSql + @" and IsPoFax=0  group by PONo ) as newtable1
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0 --,CG_POOrder.AppName
--2.1
UNION 
select newtable1.PONo, CG_POOrder.AE,6 as T
from(select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过' and POStatue3='已开票'  " + specialSql + @" and IsPoFax=0  group by PONo ) as newtable1
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on newtable1.PONo= newtable2.PONo
left join( select PONo, sum(Total) as hadFpTotal from Sell_OrderFP where Status='通过' group by PONo) as newtable3 on newtable1.PONo= newtable3.PONo
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0 
where (hadFpTotal<>0 and newtable1.POTotal-isnull(TuiTotal,0)>hadFpTotal) --,CG_POOrder.AppName
--3.  不含特殊 +含税 勾上；到款金额合并 勾上 ；项目金额<5；.发票状态：所有  查询出来的项目-----第三个画面
UNION 
 select newtable1.PONo, CG_POOrder.AE,7 as T
from(select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过' and POStatue3='已开票'  " + specialSql + @" and IsPoFax=1  group by PONo ) as newtable1
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on newtable1.PONo= newtable2.PONo
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0 
where newtable1.POTotal-isnull(TuiTotal,0) < 5 --,CG_POOrder.AppName
--4. 到款金额>= 项目金额，发票状态=未开票，CHECKBOX 含税勾上，不含特殊+到款单合并 勾上，的查询结果的项目
UNION 
select newtable1.PONo, CG_POOrder.AE,4 as T
from(select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过' " + specialSql + @" and IsPoFax=1 group by PONo ) as newtable1
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
left join CG_POOrder on CG_POOrder.PONo=Sell_OrderFP.PONo and CG_POOrder.IFZhui=0
where  1=1  and Sell_OrderFP.Status<>'不通过' and Isorder=0  and SumPoTotal-TuiTotal<sumTotal

--项目的金额必须>销售汇总的总金额项目编号 罗列在销售月考核中
UNION 
select  allNewTb.PONo,AE,2 as T from (select  CG_POOrder.PONo, sum(goodSellTotal) as goodSellTotal, AE from CG_POOrder  left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo  
 left join (select max(DaoKuanDate)  as MaxDaoKuanDate,PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  
 where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
 left join (select min(CreateTime) as MinOutDate,PONO from Sell_OrderOutHouse left join Sell_OrderOutHouses
on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id where  Status='通过' group by PONO ) as SellOut on CG_POOrder.PONo=SellOut.PONO
 left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) as ntb2 on CG_POOrder.PONo=ntb2.PONo 
 where ifzhui=0  and CG_POOrder.Status='通过'  
 GROUP BY  CG_POOrder.PONo,CG_POOrder.POName,CG_POOrder.PODate ,CG_POOrder.GuestName ,AE,INSIDE,FPTotal,AEPer,INSIDEPer,MinOutDate,
 MaxDaoKuanDate,ZhangQiTotal,CG_POOrder.IsClose,CG_POOrder.GuestType, CG_POOrder.GuestPro   ) as allNewTb left join POTotal_SumView on  allNewTb.PONo=POTotal_SumView.pono  where SumPOTotal <> goodSellTotal 
--发票对比
UNION 
select Sell_OrderFP.PONo,AE,3 as T  from Sell_OrderFP left join CG_POOrder on Sell_OrderFP.PONo=CG_POOrder.PONo and IFZhui=0
where Sell_OrderFP.Status='通过' and Isorder=0 and CG_POOrder.Status='通过'  
and not exists(SELECT TB.FPNo AS InvoiceNumber FROM ( select FPNo,sum(Total) as Total,GuestNAME  from Sell_OrderFP 
where  Status='通过' and Isorder=0 group by FPNo,GuestNAME  ) AS TB 
INNER JOIN  " + InvoiceService.InvoiceServer + @"KingdeeInvoice.dbo.Invoice_View AS TB1 ON TB.FPNo=TB1.InvoiceNumber AND TB.Total=TB1.Total 
WHERE Sell_OrderFP.FPNo=TB.FPNo ) 
) as tb where 1=1 ", (DateTime.Now.Year-1));
            
            if (ddlSpecial.Text != "-1")
            {
                sql += string.Format(" and exists(select c.id from CG_POOrder c where c.IFZhui=0 and c.PONo=tb.PONo and IsSpecial={0})", ddlSpecial.Text);
            }
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and exists(select c.id from CG_POOrder c where c.IFZhui=0 and c.PONo=tb.PONo and Model='{0}')", ddlModel.Text);               
            }
            if (!string.IsNullOrEmpty(pono))
            {
                sql += string.Format(" and pono like '%{0}%'",pono);
            }
            if (ae != "-1" &&ae!="全部"&&!string.IsNullOrEmpty(ae))
            {
                sql += string.Format(" and ae = '{0}'", ae);
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and ae IN(select loginName from tb_User where {0})", where);
            }
            if (ddlFPYW.Text != "-1")
            {
                sql += string.Format(" and t = {0}", ddlFPYW.Text);
            }
            return sql + "  order by PONo";
        }

        private StringBuilder GetSql(string selectUserId,string selectUserName)
        {
            txtPONo.Text = txtPONo.Text.Trim();
            string special = "1=1";
            if (ddlSpecial.Text != "-1")
            {
                special = "IsSpecial=" + ddlSpecial.Text;
            }
            if (ddlModel.Text != "全部")
            {
                special += string.Format(" and Model ='{0}' ", ddlModel.Text);
            }

            string t1 = "where 1=1 ";
            if (ddlSpecial.Text != "-1")
            {
                t1 = "where IsSpecial=" + ddlSpecial.Text;
            }
            if (ddlModel.Text != "全部")
            {
                t1 += string.Format(" and Model ='{0}' ", ddlModel.Text);
            }
            string t2 = "where 1=1 ";
           
            if (ddlSpecial.Text != "-1")
            {
                t2 = "where IsSpecial=" + ddlSpecial.Text;
            }
            if (ddlModel.Text != "全部")
            {
                t2 += string.Format(" and Model ='{0}' ", ddlModel.Text);
            }
            //string t3 = "";
            string t4 = "";
            string t5 = "where 1=1 ";

            if (ddlSpecial.Text != "-1")
            {
                t5 = "where IsSpecial=" + ddlSpecial.Text;
            }
            if (ddlModel.Text != "全部")
            {
                t5 += string.Format(" and Model ='{0}' ", ddlModel.Text);
            }
            string t6 = "";
            string t7 = "";
            string t8 = "";

            string t9 = "";
            string t10 = "";
            if (txtPONo.Text != "")
            {
                t1 += string.Format(" and CG_POOrder.PONO like '%{0}%'", txtPONo.Text);
                t2 += string.Format(" and CG_POOrder.PONO like '%{0}%'", txtPONo.Text);

                //if (selectUserId == "-1")
                //{
                //    t3 = string.Format(" where PONO like '%{0}%'", txtPONo.Text);
                //}
                //else  
                //{
                //    t3 = string.Format(" and PONO like '%{0}%'", txtPONo.Text);
                //}
                t4 = string.Format(" and tb1.PONO like '%{0}%'", txtPONo.Text);
                t5 += string.Format(" and newtable1.PONO like '%{0}%'", txtPONo.Text);
                t6 = string.Format(" and newtable1.PONO like '%{0}%'", txtPONo.Text);
                t7 = string.Format(" and newtable1.PONO like '%{0}%'", txtPONo.Text);
                t8 = string.Format(" and POFP_View.PONO like '%{0}%'", txtPONo.Text);
                t9 = string.Format(" and PONotCaiView.PONO like '%{0}%'", txtPONo.Text);
                t10 = string.Format(" and PONO like '%{0}%'", txtPONo.Text);
            }

            if (ddlCKFPDays.Text != "-1"&&!string.IsNullOrEmpty(txtCKFPDays.Text))
            {
                t4 += string.Format(@" and DATEDIFF(day,minRuTime,getdate()){1}{0}", txtCKFPDays.Text,ddlCKFPDays.Text);
            }
            var sqlInfo = new StringBuilder();
            //--出库单签回单-------需要显示 每个AE的未签回单  项目编号

            string Sell_OrderOutHouseBack = "";
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                Sell_OrderOutHouseBack=string.Format(" and Sell_OrderOutHouse.CreateUserId IN(select ID from tb_User where {0})", where);
            }
            sqlInfo.AppendFormat(
                @"SELECT CG_POOrder.PONO,CG_POOrder.AE FROM (select Sell_OrderOutHouse.PONo from Sell_OrderOutHouse  
left join Sell_OrderOutHouseBack on Sell_OrderOutHouse.ProNo=Sell_OrderOutHouseBack.SellProNo 
where  Sell_OrderOutHouse.Status='通过' and (Sell_OrderOutHouseBack.Status<>'不通过' or Sell_OrderOutHouseBack.Status is null)  and (BackType=1 or Sell_OrderOutHouseBack.Id is null)
{0} {1} group by Sell_OrderOutHouse.PONo) as newtable1
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0   "+t1+" ",
                                                                                       (selectUserId == "-1" ? "" : "and  Sell_OrderOutHouse.CreateUserId=" + selectUserId), Sell_OrderOutHouseBack);

          

            string Sell_OrderFPBack = "";
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                Sell_OrderFPBack=string.Format(" and Sell_OrderFP.CreateUserId IN(select ID from tb_User where {0})", where);
            }

            //-- 发票签回单 --------需要显示 每个AE 的未签回单  项目编号
            sqlInfo.AppendFormat(
                @"SELECT CG_POOrder.PONO,CG_POOrder.AE FROM (select Sell_OrderFP.PONo from Sell_OrderFP  left join Sell_OrderFPBack on Sell_OrderFP.ID=Sell_OrderFPBack.PID 
where Sell_OrderFP.Status='通过' and Sell_OrderFPBack.Status<>'不通过' and (FPBackType=1 or Sell_OrderFPBack.Id is null) {0} {1}
group by Sell_OrderFP.PONo ) as newtable1 
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0  " + t2 + " ", (selectUserId == "-1" ? "" : "and Sell_OrderFP.CreateUserId=" + selectUserId), Sell_OrderFPBack);

         
            //--入库未出清单------需要显示  每个AE的 列表 项目编号
            //sqlInfo.AppendFormat(
            //    @"select PONo,AE  from NoSellOutGoods_2  {0} " + t3 + " group by PONo,AE order by PONo;", selectUserId == "-1" ? "" : " where AppName=" + selectUserId);
            sqlInfo.AppendFormat(";select 1;");
            
            //--  出库未开发票和未开全发票 清单-----需要显示  每个AE的 列表 项目编号
            string POTotal_View = "";
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                POTotal_View = string.Format(" and IFZhui=0 and AppName IN(select ID from tb_User where {0})", where);
            }

            //出库发票清单
            sqlInfo.AppendFormat(@"select tb1.PONO, tb1.AE,POTotal_View.POTotal ,DATEDIFF(day,minRuTime,getdate()) as diffDate from (
select  CG_POOrder.PONo , CG_POOrder.AE  from CG_POOrder
where   Status='通过' and CG_POOrder.GuestName not like '本部门%' and " + special+ @" and IsPoFax=1 and (POStatue4='' or POStatue4 is null) {0} {1}
group by CG_POOrder.PONo , CG_POOrder.AE
) as tb1 
left join POTotal_View on POTotal_View.PONo=tb1.PONo--查询 项目金额和 已开发票金额
left join
(
select PoNo,SUM(Total) AS InvoiceTotal,MAX(DaoKuanDate) as MaxDaoKuanDate from TB_ToInvoice where State='通过' GROUP BY PoNo
) as Invoice on Invoice.PoNo=tb1.PONo
left join
(
select PONo, MIN(RuTime) as minRuTime from Sell_OrderOutHouse where Status='通过' group by PONo
) as FP on FP.PONo=tb1.PONo
where  hadFpTotal is  null and isnull(InvoiceTotal,0)<POTotal_View.POTotal and minRuTime is not null " + t4 + @"
union
select tb1.PONO, tb1.AE,POTotal_View.POTotal,DATEDIFF(day,minRuTime,getdate()) as diffDate from (
select  CG_POOrder.PONo , CG_POOrder.AE  from CG_POOrder 
where  Status='通过'   and CG_POOrder.GuestName not like '本部门%' and " + special + @" and IsPoFax=1 and POStatue3<>'已开票' and (POStatue4='' or POStatue4 is null) {0}
group by CG_POOrder.PONo , CG_POOrder.AE
)as tb1-- 项目基本信息汇总
left join POTotal_View on POTotal_View.PONo=tb1.PONo--查询 项目金额和 已开发票金额
left join
(
select PoNo,SUM(Total) AS InvoiceTotal,MAX(DaoKuanDate) as MaxDaoKuanDate from TB_ToInvoice where State='通过' GROUP BY PoNo
) as Invoice on Invoice.PoNo=tb1.PONo
left join
(
select PONo, MIN(RuTime) as minRuTime from Sell_OrderOutHouse where Status='通过' group by PONo
) as FP on FP.PONo=tb1.PONo
where  hadFpTotal is not null and hadFpTotal<POTotal_View.POTotal and isnull(InvoiceTotal,0)<POTotal_View.POTotal and minRuTime is not null " + t4 + "", (selectUserId == "-1" ? "" : "and AppName=" + selectUserId), POTotal_View);

       
            //--   采购未检验清单----需要显示每个AE 的列表 项目编号。
            sqlInfo.AppendFormat(@"SELECT newtable1.PONO,newtable1.AE FROM (select CAI_POOrder.PONo,CAI_POOrder.AE from CAI_POCai
left join CAI_POOrder on CAI_POCai.Id=CAI_POOrder.Id
left join
(
select  CaiId,SUM(CheckNum) as totalOrderNum from CAI_OrderChecks left join CAI_OrderCheck on  CAI_OrderCheck.id=CAI_OrderChecks.CheckId
where CaiId<>0  and CAI_OrderCheck.status<>'不通过'
group by CaiId
)
as newtable on CAI_POCai.Ids=newtable.CaiId 
where (CAI_POCai.Num>newtable.totalOrderNum or totalOrderNum is null)
and status='通过' and lastSupplier<>'库存' {0}
group by CAI_POOrder.PONo,CAI_POOrder.AE) as newtable1 left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0" + t5 + " ", selectUserId == "-1" ? "" : " and AE='" + selectUserName + "'");
            
            
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sqlInfo.AppendFormat(" and  newtable1.AE IN(select loginName from tb_User where {0})", where);
            }

            //应收款未到日
            sqlInfo.AppendFormat(@";select newtable1.PONo,CG_POOrder.AE, MaxDaoKuanDate,minOutTime,newtable1.POTotal-isnull(TuiTotal,0) as POTotal,isnull(Total,0) as Total from(
select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过' and " + special + @" group by PONo ) as newtable1
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on newtable1.PONo= newtable2.PONo 
left join (select pono ,sum(Total) as Total, max(DaoKuanDate) as MaxDaoKuanDate from  TB_ToInvoice where  1=1  and (State<>'不通过' or State is null) group by PONo )as newtable4 on newtable1.PONo= newtable4.PONo 
left join( select min(RuTime) as minOutTime, PONo from Sell_OrderOutHouse group by PONo) as newtable5 on newtable1.PONo= newtable5.PONo
left join CG_POOrder on CG_POOrder.pono=newtable1.PONo and Status='通过' and IFZhui=0 
 where 1=1  and ISNULL(Total,0) <newtable1.POTotal-isnull(TuiTotal,0) and datediff(d,minOutTime,getdate())>90 {0} " + t6 + "", selectUserId == "-1" ? "" : " and AppName=" + selectUserId + "");
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sqlInfo.AppendFormat(" and  IFZhui=0 and AppName IN(select ID from tb_User where {0})", where);
            }
            if (ddlYSDKDays.Text != "-1" && !string.IsNullOrEmpty(txtYSDKDays.Text))
            {
                sqlInfo.AppendFormat(@" and
( (isnull(Total,0)>=newtable1.POTotal-isnull(TuiTotal,0) and DATEDIFF(day,minOutTime,MaxDaoKuanDate){1}{0})
OR (isnull(Total,0)<newtable1.POTotal-isnull(TuiTotal,0) and DATEDIFF(day,minOutTime,getdate()){1}{0}))", txtYSDKDays.Text, ddlYSDKDays.Text);
            }
            //催账提醒未到日
            sqlInfo.AppendFormat(@" order by newtable1.PONo;select newtable1.PONo  ,AE,hadFpTotal ,POTotal-isnull(TuiTotal,0) as POTotal, datediff(dd, minOutTime,getdate() ) as chaDays,SimpGuestName from(
select PONo,sum(POTotal) AS POTotal from CG_POOrder where Status='通过'  and " + special + @"
group by PONo) as newtable1 
left join(select PONo ,sum(TuiTotal) as TuiTotal from Sell_OrderInHouse where Status='通过'  group by PONo) as newtable2 on newtable1.PONo= newtable2.PONo
left join( select PONo, sum(Total) as hadFpTotal from Sell_OrderFP where Status='通过' group by PONo)
as newtable3 on newtable1.PONo= newtable3.PONo
left join( select min(RuTime) as minOutTime,PONo from Sell_OrderOutHouse group by PONo) 
as newtable5 on newtable1.PONo= newtable5.PONo left join 
(select SimpGuestName,pono,AppName, case  when  isnumeric(POPayStype)=1 then Convert(decimal, POPayStype)  else 0 end
as sumJieSuan ,CG_POOrder.ae from CG_POOrder left join TB_GuestTrack on CG_POOrder.GuestId=TB_GuestTrack.Id where Status='通过' and IFZhui=0) as newtable6
on newtable6.pono=newtable1.PONo 
left join (select pono ,sum(Total) as Total
from  TB_ToInvoice where  1=1  and (State<>'不通过' or State is null) group by PONo )as newtable4 on newtable1.PONo= newtable4.PONo 
where getdate()>=dateadd(day,sumJieSuan-30 ,minOutTime) and ISNULL(Total,0) <newtable1.POTotal-isnull(TuiTotal,0)  {0}  " + t7 + " ", selectUserId == "-1" ? "" : " and AppName=" + selectUserId + "");
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sqlInfo.AppendFormat(" and  AppName IN(select ID from tb_User where {0})", where);
            }

            if (ddlCZTXDays.Text != "-1" && !string.IsNullOrEmpty(txtCZTXDays.Text))
            {
                sqlInfo.AppendFormat(@" and datediff(dd, minOutTime,getdate() ){1}{0}", txtCZTXDays.Text, ddlCZTXDays.Text);
            }

            sqlInfo.Append(" order by newtable1.PONo ;");
            //发票金额》项目金额
           // sqlInfo.AppendFormat("select CG_POOrder.PONo,CG_POOrder.AE from POFP_View left join CG_POOrder on CG_POOrder.pono=POFP_View.PONo and Status='通过' and IFZhui=0  where sumPOTOTAL-TUITOTAL<SUMTOTAL {0} " + t8 + ";", selectUserId == "-1" ? "" : "and CG_POOrder.AppName=" + selectUserId);
            sqlInfo.AppendFormat(GetFPNOWrong(txtPONo.Text, selectUserName));
            //项目未出清单
            sqlInfo.AppendFormat(" select PONo,AE from PONotCaiView  where lastNum>0  {0}  " + t10 + " and "+special, (selectUserId == "-1" ? "" : " and AppName=" + selectUserId + ""));
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sqlInfo.AppendFormat(" and  AppName IN(select ID from tb_User where {0})", where);
            }
            sqlInfo.Append(" group by PONo,AE;");
            sqlInfo.AppendFormat(GetFPWei(txtPONo.Text, selectUserName)+";");
            sqlInfo.AppendFormat(GetEmptyFPNO(txtPONo.Text,selectUserName));
            return sqlInfo;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            OutExcelToList("");
        }

        /// <summary>
        /// 导出售票明细报表
        /// 添加人：冯建
        /// 添加时间：2012-12-26
        /// </summary>
        /// <param name="ticketSalesRecordList">数据源</param>
        /// <param name="sheetName">EXCEL标题名称</param>
        private void OutExcelToList(string sheetName)
        {
            var hssfworkbook = new HSSFWorkbook();
            var dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI Team";
            hssfworkbook.DocumentSummaryInformation = dsi;
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "NPOI SDK Example";
            hssfworkbook.SummaryInformation = si;

            //超链接的单元格风格
            //超链接默认的是蓝色底边框
            var hlink_style = hssfworkbook.CreateCellStyle();
            var hlink_font = hssfworkbook.CreateFont();
            hlink_font.Color = HSSFColor.BLUE.index;
            hlink_font.Underline = 0;
            hlink_style.SetFont(hlink_font);

            //设置单元格式-居中
            var stylecenter = hssfworkbook.CreateCellStyle();
            stylecenter.Alignment = HorizontalAlignment.CENTER;
            stylecenter.VerticalAlignment = VerticalAlignment.CENTER;
            stylecenter.BorderTop = CellBorderType.THIN;
            stylecenter.BorderBottom = CellBorderType.THIN;
            stylecenter.BorderLeft = CellBorderType.THIN;
            stylecenter.BorderRight = CellBorderType.THIN;
            stylecenter.TopBorderColor = IndexedColors.BLACK.Index;
            stylecenter.BottomBorderColor = IndexedColors.BLACK.Index;
            stylecenter.LeftBorderColor = IndexedColors.BLACK.Index;
            stylecenter.RightBorderColor = IndexedColors.BLACK.Index;

            string url1 = Request.Url.Scheme + "://" + Request.Url.Authority + "/JXC/Sell_OrderOutHouseBackList.aspx?PONo=";
            string url2 = Request.Url.Scheme + "://" + Request.Url.Authority + "/JXC/Sell_OrderPFBackList.aspx?PONo=";
            string url3 = Request.Url.Scheme + "://" + Request.Url.Authority + "/JXC/NoSellAndCaiGoods.aspx?PONo1=";
            string url4 = Request.Url.Scheme + "://" + Request.Url.Authority + "/JXC/WFSellFPReport.aspx?PONo=";
            string url5 = Request.Url.Scheme + "://" + Request.Url.Authority + "/JXC/WFCaiNotRuReport.aspx?PONo=";
            string url6 = Request.Url.Scheme + "://" + Request.Url.Authority + "/ReportForms/WFToInvoiceList.aspx?PONo=";
            string url7 = Request.Url.Scheme + "://" + Request.Url.Authority + "/ReportForms/WFToInvoiceList.aspx?ishebing=1&PONo=";
            //string url8 = "";// Request.Url.Scheme + "://" + Request.Url.Authority + "/JXC/Sell_OrderPFList.aspx?PONo=";
            //url8 = Request.Url.Scheme + "://" + Request.Url.Authority + "/JXC/Sell_OrderPFList.aspx?PONo=";
            string url81 = Request.Url.Scheme + "://" + Request.Url.Authority + "/JXC/Sell_OrderPFList.aspx?PONo=";
            string url82 = Request.Url.Scheme + "://" + Request.Url.Authority + "/JXC/JXC_REPORTTotalList.aspx?PONo=";
            string url83 = Request.Url.Scheme + "://" + Request.Url.Authority + "/KingdeeInvoice/WFInvoiceCompare.aspx?PONo=";
            string url84 = Request.Url.Scheme + "://" + Request.Url.Authority + "/ReportForms/WFToInvoiceList.aspx?PONo=";
          
            string url9 = Request.Url.Scheme + "://" + Request.Url.Authority + "/JXC/WFPONotCaiReport.aspx?PONo=";
            string url10 = Request.Url.Scheme + "://" + Request.Url.Authority + "/JXC/WFSupplierInvoiceToAllFpList.aspx?PONo=";
            txtPONo.Text = txtPONo.Text.Trim();
            if (ddlUser.Text == "-1")
            {
                noSellAndCaiGoodsList = new RuSellReportService().getHT("", txtPONo.Text,ddlCompany.Text,ddlSpecial.Text, ddlModel.Text);
            }
            else
            {
                noSellAndCaiGoodsList = new RuSellReportService().getHT(ddlUser.SelectedItem.Text, txtPONo.Text, ddlCompany.Text, ddlSpecial.Text, ddlModel.Text);
            }

            var sellGoodsList = new Model.HashTableModel().HashTableToList(noSellAndCaiGoodsList);
            DataSet ds = new DataSet();
            using (DbConnection objConnection = DBHelp.getConn())
            {

                objConnection.Open();
                DbCommand objCommand = objConnection.CreateCommand();            
               

                for (int ii = 0; ii < ddlUser.Items.Count; ii++)
                {
                    var name = ddlUser.Items[ii].Text;
                    var userId = ddlUser.Items[ii].Value;
                    if(userId=="-1")
                    {
                        continue;
                    }
                    var user_SellGoodsList = sellGoodsList.FindAll(t=>t.Value==name);

                    //user_SellGoodsList.Sort();
                    #region 创建工作簿
                    var sheet = hssfworkbook.CreateSheet(name);
                    sheet.AddMergedRegion(new CellRangeAddress(0, 1, 0, 11));
                    sheet.CreateRow(0).CreateCell(0).SetCellValue(name + " 考核");
                    var cell = sheet.GetRow(0).GetCell(0);
                    cell.CellStyle = stylecenter;
                    var columns = new List<string>
                              {
                                  "出库签回单列表",
                                  "说明",
                                  "发票签回单列表",
                                  "说明",
                                   "项目未出清单",
                                  "说明",
                                  "采库未出清单",
                                  "说明",
                                  "出库发票清单",
                                  "说明",
                                  "采购未检验清单",     
                                  "说明",
                                  "应收款",     
                                  "说明",
                                  "催帐提醒",     
                                  "说明",
                                  "发票有误",
                                  "说明",
                                   "发票漏填",
                                  "说明",
                                   "预付未到票",
                                  "说明"
                              };
                    //添加列名
                    sheet.CreateRow(2);

                    objCommand.CommandText = GetSql(userId, name).ToString();
                    DbDataAdapter objApater = DBHelp.GetProviderFactory().CreateDataAdapter();
                    var myDS = new DataSet();
                    objApater.SelectCommand = objCommand;
                    objApater.Fill(myDS);

                    //var myDS = DBHelp.getDataSet(GetSql(userId, name).ToString());
                    var dataTable1 = myDS.Tables[0];
                    var dataTable2 = myDS.Tables[1];
                    //var dataTable3 = myDS.Tables[2];
                    var dataTable4 = myDS.Tables[3];
                    var dataTable5 = myDS.Tables[4];
                    var dataTable6 = myDS.Tables[5];
                    var dataTable7 = myDS.Tables[6];

                    var dataTable8 = myDS.Tables[7];
                    var dataTable9 = myDS.Tables[8];
                    var dataTable10 = myDS.Tables[9];
                    var dataTable11 = myDS.Tables[10];
                    int maxRows = dataTable1.Rows.Count;
                    if (dataTable2.Rows.Count > maxRows)
                    {
                        maxRows = dataTable2.Rows.Count;
                    }
                    if (noSellAndCaiGoodsList.Count > maxRows)
                    {
                        maxRows = user_SellGoodsList.Count;
                    }
                    if (dataTable4.Rows.Count > maxRows)
                    {
                        maxRows = dataTable4.Rows.Count;
                    }
                    if (dataTable5.Rows.Count > maxRows)
                    {
                        maxRows = dataTable5.Rows.Count;
                    }
                    if (dataTable6.Rows.Count > maxRows)
                    {
                        maxRows = dataTable6.Rows.Count;
                    }
                    if (dataTable7.Rows.Count > maxRows)
                    {
                        maxRows = dataTable7.Rows.Count;
                    }
                    if (dataTable8.Rows.Count > maxRows)
                    {
                        maxRows = dataTable8.Rows.Count;
                    }
                    if (dataTable9.Rows.Count > maxRows)
                    {
                        maxRows = dataTable9.Rows.Count;
                    }
                    if (dataTable10.Rows.Count > maxRows)
                    {
                        maxRows = dataTable10.Rows.Count;
                    }
                    if (dataTable11.Rows.Count > maxRows)
                    {
                        maxRows = dataTable11.Rows.Count;
                    }
                    var dt1Count = dataTable1.Rows.Count;
                    var dt2Count = dataTable2.Rows.Count;
                    var dt3Count = user_SellGoodsList.Count;
                    var dt4Count = dataTable4.Rows.Count;
                    var dt5Count = dataTable5.Rows.Count;
                    var dt6Count = dataTable6.Rows.Count;
                    var dt7Count = dataTable7.Rows.Count;

                    var dt8Count = dataTable8.Rows.Count;

                    var dt9Count = dataTable9.Rows.Count;
                    var dt10Count = dataTable10.Rows.Count;
                    var dt11Count = dataTable11.Rows.Count;
                    for (var i = 0; i < columns.Count; i++)
                    {
                        sheet.GetRow(2).CreateCell(i).SetCellValue(columns[i]);
                        cell = sheet.GetRow(2).GetCell(i);
                        cell.CellStyle = stylecenter;
                    }
                                    
                    for (var i = 0; i < maxRows; i++)
                    {
                        sheet.CreateRow(3 + i);
                        
                        var text1 = dt1Count > i ? dataTable1.Rows[i][0].ToString() : "";
                        var text2 = dt2Count > i ? dataTable2.Rows[i][0].ToString() : "";
                        var text3 = dt3Count > i ? user_SellGoodsList[i].Key : "";
                        var text4 = dt4Count > i ? dataTable4.Rows[i][0].ToString() : "";
                        var text5 = dt5Count > i ? dataTable5.Rows[i][0].ToString() : "";
                        var text6 = dt6Count > i ? dataTable6.Rows[i][0].ToString() : "";
                        var text7 = dt7Count > i ? dataTable7.Rows[i][0].ToString() : "";

                        var text8 = dt8Count > i ? dataTable8.Rows[i][0].ToString() : "";
                        var text81 = dt8Count > i ?  dataTable8.Rows[i][2].ToString():"";

                        var text9 = dt9Count > i ? dataTable9.Rows[i][0].ToString() : "";
                        var text10 = dt10Count > i ? dataTable10.Rows[i][0].ToString() : "";
                        var text11 = dt10Count > i ? dataTable11.Rows[i][0].ToString() : "";

                        var link1 = new HSSFHyperlink(HyperlinkType.URL) { Address = url1 + text1 };
                        var link2 = new HSSFHyperlink(HyperlinkType.URL) { Address = url2 + text2 };
                        var link3 = new HSSFHyperlink(HyperlinkType.URL) { Address = url3 + text3 };
                        var link4 = new HSSFHyperlink(HyperlinkType.URL) { Address = url4 + text4 };
                        var link5 = new HSSFHyperlink(HyperlinkType.URL) { Address = url5 + text5 };
                        var link6 = new HSSFHyperlink(HyperlinkType.URL) { Address = url6 + text6 };
                        var link7 = new HSSFHyperlink(HyperlinkType.URL) { Address = url7 + text7 };
                        var link81 = new HSSFHyperlink(HyperlinkType.URL) { Address = url81 + text8 };
                        var link82 = new HSSFHyperlink(HyperlinkType.URL) { Address = url82 + text8 };
                        var link83 = new HSSFHyperlink(HyperlinkType.URL) { Address = url83 + text8 };
                        var link84 = new HSSFHyperlink(HyperlinkType.URL) { Address = url84 + text8 };

                        var link9 = new HSSFHyperlink(HyperlinkType.URL) { Address = url9 + text9 };
                        var link10 = new HSSFHyperlink(HyperlinkType.URL) { Address = url10 + text10 };
                        //var link11= new HSSFHyperlink(HyperlinkType.URL) { Address = url11 + text11 };

                        sheet.GetRow(3 + i).CreateCell(0).SetCellValue(text1);
                        if (text1 != "")
                        {
                            sheet.GetRow(3 + i).GetCell(0).Hyperlink = link1;
                            sheet.GetRow(3 + i).GetCell(0).CellStyle = hlink_style;
                        }

                        sheet.GetRow(3 + i).CreateCell(1).SetCellValue("");
                        sheet.GetRow(3 + i).CreateCell(2).SetCellValue(text2);
                        if (text2 != "")
                        {
                            sheet.GetRow(3 + i).GetCell(2).Hyperlink = link2;
                            sheet.GetRow(3 + i).GetCell(2).CellStyle = hlink_style;

                        }

                        sheet.GetRow(3 + i).CreateCell(3).SetCellValue("");

                        //====
                        sheet.GetRow(3 + i).CreateCell(4).SetCellValue(text9);
                        if (text9 != "")
                        {
                            sheet.GetRow(3 + i).GetCell(4).Hyperlink = link9;
                            sheet.GetRow(3 + i).GetCell(4).CellStyle = hlink_style;
                        }

                        sheet.GetRow(3 + i).CreateCell(5).SetCellValue("");
                        //====

                        sheet.GetRow(3 + i).CreateCell(6).SetCellValue(text3);
                        if (text3 != "")
                        {
                            sheet.GetRow(3 + i).GetCell(6).Hyperlink = link3;
                            sheet.GetRow(3 + i).GetCell(6).CellStyle = hlink_style;
                        }

                        sheet.GetRow(3 + i).CreateCell(7).SetCellValue("");
                        sheet.GetRow(3 + i).CreateCell(8).SetCellValue(text4);
                        if (text4 != "")
                        {
                            sheet.GetRow(3 + i).GetCell(8).Hyperlink = link4;
                            sheet.GetRow(3 + i).GetCell(8).CellStyle = hlink_style;
                        }

                        sheet.GetRow(3 + i).CreateCell(9).SetCellValue("");
                        sheet.GetRow(3 + i).CreateCell(10).SetCellValue(text5);
                        if (text5 != "")
                        {
                            sheet.GetRow(3 + i).GetCell(10).Hyperlink = link5;
                            sheet.GetRow(3 + i).GetCell(10).CellStyle = hlink_style;
                        }

                        sheet.GetRow(3 + i).CreateCell(11).SetCellValue("");
                        sheet.GetRow(3 + i).CreateCell(12).SetCellValue(text6);
                        if (text6 != "")
                        {
                            sheet.GetRow(3 + i).GetCell(12).Hyperlink = link6;
                            sheet.GetRow(3 + i).GetCell(12).CellStyle = hlink_style;
                        }


                        sheet.GetRow(3 + i).CreateCell(13).SetCellValue("");
                        sheet.GetRow(3 + i).CreateCell(14).SetCellValue(text7);
                        if (text7 != "")
                        {
                            sheet.GetRow(3 + i).GetCell(14).Hyperlink = link7;
                            sheet.GetRow(3 + i).GetCell(14).CellStyle = hlink_style;
                        }



                        sheet.GetRow(3 + i).CreateCell(15).SetCellValue("");
                        sheet.GetRow(3 + i).CreateCell(16).SetCellValue(text8);
                        if (text8 != "")
                        {
                            if (text81 == "1")
                            {
                                sheet.GetRow(3 + i).GetCell(16).Hyperlink = link81;
                            }
                            else if (text81 == "2")
                            {
                                sheet.GetRow(3 + i).GetCell(16).Hyperlink = link82;
                            }
                            else if (text81 == "3")
                            {
                                sheet.GetRow(3 + i).GetCell(16).Hyperlink = link83;
                            }
                            else 
                            {
                                sheet.GetRow(3 + i).GetCell(16).Hyperlink = link84;
                            }
                            sheet.GetRow(3 + i).GetCell(16).CellStyle = hlink_style;
                        }

                        sheet.GetRow(3 + i).CreateCell(17).SetCellValue("");
                   
                        sheet.GetRow(3 + i).CreateCell(18).SetCellValue(text11);
                        if (text11 != "")
                        {
                            //sheet.GetRow(3 + i).GetCell(18).Hyperlink = link10;
                            sheet.GetRow(3 + i).GetCell(18).CellStyle = hlink_style;
                        }
                        sheet.GetRow(3 + i).CreateCell(19).SetCellValue("");

                        sheet.GetRow(3 + i).CreateCell(20).SetCellValue(text10);
                        if (text7 != "")
                        {
                            sheet.GetRow(3 + i).GetCell(20).Hyperlink = link10;
                            sheet.GetRow(3 + i).GetCell(20).CellStyle = hlink_style;
                        }
                        sheet.GetRow(3 + i).CreateCell(21).SetCellValue("");

                        for (var j = 0; j < columns.Count; j++)
                        {
                            cell = sheet.GetRow(3 + i).GetCell(j);
                            cell.CellStyle = stylecenter;
                        }
                    }
                   
                    for (var i = 0; i < columns.Count; i++)
                    {
                        
                        sheet.SetColumnWidth(i, 15 * 256);
                    }
                    sheet.ForceFormulaRecalculation = true;
                    #endregion
                }
                objConnection.Close();
            }
            WriteToFile(hssfworkbook, "销售考核");
        }

        /// <summary>
        /// 向页面输出excel文件
        /// 添加人：冯建
        /// 添加时间：2012-12-26
        /// </summary>
        /// <param name="hssfworkbook"></param>
        /// <param name="excelName">Excel的名称</param>
        private void WriteToFile(HSSFWorkbook hssfworkbook, string excelName)
        {
            var rootPath = Server.MapPath("~/UploadFiles/");
            var _title = "/" + excelName + ".xls";
            if (!string.IsNullOrEmpty(Request.Browser.Browser))
            {
                if (Request.Browser.Browser.ToLower().IndexOf("ie") >= 0)
                {
                    _title = Server.UrlEncode(_title);
                }
            }
            //Write the stream data of workbook to the root directory
            var fs = new FileStream(rootPath + "/" + excelName + ".xls", FileMode.Create, FileAccess.ReadWrite);
            hssfworkbook.Write(fs);
            hssfworkbook.Dispose();
            fs.Close();
            //把文件以流方式指定xls格式提供下载
            fs = System.IO.File.OpenRead(rootPath + "/" + excelName + ".xls");
            var FileArray = new byte[fs.Length];
            fs.Read(FileArray, 0, FileArray.Length);
            fs.Close();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + _title);
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("Content-Length", FileArray.Length.ToString());
            Response.BinaryWrite(FileArray);
            Response.Flush();
            Response.End();
            Response.Clear();
        }

    }
}
