using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Model;
using VAN_OA.Model.Fin;
using VAN_OA.Dal.Fin;
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.EFrom;
using VAN_OA.Dal.JXC;



namespace VAN_OA.JXC
{
    public partial class WFAllSellDetail : BasePage
    {
        protected List<AllSellTotalModel> allList = new List<AllSellTotalModel>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });

                for (int i = 2012; i <= DateTime.Now.Year; i++)
                {
                    ddlYear.Items.Add(new ListItem { Value = i.ToString(), Text = i.ToString() });
                }
                ddlYear.SelectedValue = (DateTime.Now.Year - 1).ToString();

//                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='结算明细表-可导出'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目结算') and sys_Object.AutoID is not null", Session["currentUserId"]);
//                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                if (NewShowAll_textName("项目结算", "结算明细表-可导出") == false)
                {
                    btnExcel.Visible = false;
                    Button2.Visible = false;
                }
//                sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='结算明细表-查询全部'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目结算') and sys_Object.AutoID is not null", Session["currentUserId"]);
//                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                if (NewShowAll_textName("项目结算", "结算明细表-查询全部") == false)
                {
                    btnExcel.Visible = false;
                    var model = Session["userInfo"] as User;
                    var m = comList.Find(t => t.ComCode == model.CompanyCode);
                    comList = new List<TB_Company>();
                    comList.Add(m);

                    user.Insert(0, model);
                    ddlUser.DataSource = user;
                    ddlUser.DataBind();
                    ddlUser.DataTextField = "LoginName";
                    ddlUser.DataValueField = "Id";
                }
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/WFAll.aspx");
        }

        FIN_PropertyService FIN_PropertyService = new FIN_PropertyService();
        FIN_CommCostService CommService = new FIN_CommCostService();
        FIN_SpecCostService SpecService = new FIN_SpecCostService();
        TB_UseCarDetailService useCarSer = new TB_UseCarDetailService();
        JXC_REPORTService reportSer = new JXC_REPORTService();

        protected int allCount = 0;//所有列
        protected List<FIN_CommCost> comm_propertyList = new List<FIN_CommCost>();
        protected List<FIN_SpecCost> spec_propertyList = new List<FIN_SpecCost>();
        protected List<FIN_Property> all_propertyList = new List<FIN_Property>();
        protected List<User> user = new List<User>();
        protected List<TB_UseCarDetailReport> UseCarDetailList = new List<TB_UseCarDetailReport>();//公里数
        //总公里数
        protected decimal RoadLongTotal = 0;

        protected List<JXC_Report_Detail> reportDetailList = new List<JXC_Report_Detail>();//销售总金额
        /// <summary>
        /// 销售总金额
        /// </summary>
        protected decimal SellTotal = 0;

        public void show()
        {
            #region 表1
            string companyName = "";
            string companyIds = "";
            string companyCode = "";
            if (ddlCompany.Text == "-1")
            {
                foreach (ListItem item in ddlCompany.Items)
                {
                    if (item.Value != "-1")
                    {
                        companyName += "+" + item.Value.Split(',')[0];
                        companyIds += "," + item.Value.Split(',')[1];
                    }
                }
            }
            else
            {
                companyName = ddlCompany.Text.Split(',')[0];
                companyIds = ddlCompany.Text.Split(',')[1];
                companyCode = ddlCompany.Text.Split(',')[2];
            }
            companyIds = companyIds.Trim(',');
            lblSimpName1.Text = companyName.Trim('+');
            lblSimpName.Text = companyName.Trim('+');
            lblYearMonth.Text = ddlYear.Text + ddlMonth.Text;
            lblYearMonth1.Text = ddlYear.Text + ddlMonth.Text;
            //var com= ddlCompany.SelectedItem. as TB_Company;
            //查询所有费用
            //公共
            comm_propertyList = CommService.GetListArray(string.Format(" CaiYear='{0}' and CompId in ({1})", ddlYear.Text + ddlMonth.Text, companyIds)); //this.FIN_PropertyService.GetListArray_CommCost(ddlYear.Text + ddlMonth.Text, companyIds);

            //个性
            spec_propertyList = SpecService.GetListArray(string.Format(" CaiYear='{0}' and CompId in ({1})",
                ddlYear.Text + ddlMonth.Text, companyIds));
            // this.FIN_PropertyService.GetListArray_SpecCost(ddlYear.Text + ddlMonth.Text, companyIds, "-1");

            //所有
            all_propertyList = this.FIN_PropertyService.GetListArray("");

            allCount = all_propertyList.Count + 4;



            DateTime fristDate = Convert.ToDateTime(ddlYear.Text + "-1-1");
            DateTime endDate = Convert.ToDateTime(ddlYear.Text + "-" + ddlMonth.Text + "-" + DateTime.DaysInMonth(fristDate.Year, Convert.ToInt32(ddlMonth.Text)));
            //公里数
            UseCarDetailList = useCarSer.Get_Report(ddlYear.Text, companyCode, ddlUser.Text);
            //RoadLongTotal=UseCarDetailList.Sum(t => t.RoadLong);
            //lblCarTotal.Text = RoadLongTotal.ToString();
            //lblSumCarTotal.Text = lblCarTotal.Text;

            //销售总金额
            reportDetailList = reportSer.Get_Report(fristDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), companyCode, ddlUser.Text,ddlJieIsSelected.Text);
            SellTotal = reportDetailList.Sum(t => t.SellTotal);
            lblSellTotal.Text = SellTotal.ToString();


            //根据用户来计算总公里数
            RoadLongTotal = 0;
            foreach (var m in reportDetailList)
            {
                RoadLongTotal += UseCarDetailList.Where(t => t.UserName == m.AE).Sum(t => t.RoadLong);
            }
            //RoadLongTotal = UseCarDetailList.Sum(t => t.RoadLong);

            lblCarTotal.Text = RoadLongTotal.ToString();
            lblSumCarTotal.Text = lblCarTotal.Text;

            #endregion



            //string allSimpNames = "";
            string aeSql = "";
            if (ddlCompany.Text != "-1" && ddlUser.Text != "-1")
            {
                aeSql = string.Format(" and exists (select id from tb_User where ID={0} and appName=id)", ddlUser.Text);
            }
            else if (ddlCompany.Text != "-1" && ddlUser.Text == "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                aeSql = string.Format(" and exists (select id from tb_User where {0} and appName=id)", where);
            }
            if (ddlIsClose.Text != "-1")
            {
                aeSql += " and IsClose=" + ddlIsClose.Text;
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                aeSql += " and JieIsSelected=" + ddlJieIsSelected.Text;
            }
            


            var allDs = DBHelp.getDataSet(string.Format(@"select AE,sum(goodSellTotal) as goodSellTotal,sum(maoliTotal) as maoliTotal,sum(InvoTotal)-sum(goodTotal) as TrueLiRun,sum(SellFPTotal) as SellFPTotal from (
select CG_POOrder.PONo,  
 sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal,
  sum(maoli) as maoliTotal, AE,
isnull(avg(InvoTotal),0) as InvoTotal,isnull(avg(SellFPTotal),0) as SellFPTotal  
from CG_POOrder  left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo 
 left join (select PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  
where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) 
as ntb2 on CG_POOrder.PONo=ntb2.PONo where ifzhui=0  and CG_POOrder.Status='通过' 
 and CG_POOrder.PODate<='{0} 23:59:59'  and CG_POOrder.PODate>='{2} 00:00:00' {1} and 
 EXISTS (select ID from CG_POOrder where
PONO=JXC_REPORT.PONO  
and IsSpecial=0  and JieIsSelected=1) GROUP BY  CG_POOrder.PONo,AE ) as allNewTb 
group by AE;
select AE,sum(goodSellTotal) as goodSellTotal,sum(maoliTotal) as maoliTotal,sum(InvoTotal)-sum(goodTotal) as TrueLiRun,sum(SellFPTotal) as SellFPTotal from (
select CG_POOrder.PONo,  
 sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal,
  sum(maoli) as maoliTotal, AE,
isnull(avg(InvoTotal),0) as InvoTotal,isnull(avg(SellFPTotal),0) as SellFPTotal  
from CG_POOrder  left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo 
 left join (select PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  
where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) 
as ntb2 on CG_POOrder.PONo=ntb2.PONo where ifzhui=0  and CG_POOrder.Status='通过' 
 and CG_POOrder.PODate<='{0} 23:59:59' and CG_POOrder.PODate>='{2} 00:00:00'  {1}  and GuestType='企业用户' and
 EXISTS (select ID from CG_POOrder where --AppName=9 AND 
PONO=JXC_REPORT.PONO  
and IsSpecial=0  and JieIsSelected=1) GROUP BY  CG_POOrder.PONo,AE ) as allNewTb 
group by AE;
select AE,sum(goodSellTotal) as goodSellTotal,sum(maoliTotal) as maoliTotal,sum(InvoTotal)-sum(goodTotal) as TrueLiRun,sum(SellFPTotal) as SellFPTotal from (
select CG_POOrder.PONo,  
 sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal,
  sum(maoli) as maoliTotal, AE,
isnull(avg(InvoTotal),0) as InvoTotal,isnull(avg(SellFPTotal),0) as SellFPTotal  
from CG_POOrder  left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo 
 left join (select PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  
where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) 
as ntb2 on CG_POOrder.PONo=ntb2.PONo where ifzhui=0  and CG_POOrder.Status='通过' 
 and CG_POOrder.PODate<='{0} 23:59:59' and CG_POOrder.PODate>='{2} 00:00:00'  {1}  and GuestType='政府部门' and
 EXISTS (select ID from CG_POOrder where --AppName=9 AND 
PONO=JXC_REPORT.PONO  
and IsSpecial=0 and JieIsSelected=1) GROUP BY  CG_POOrder.PONo,AE ) as allNewTb 
group by AE;
select AE,sum(goodSellTotal) as goodSellTotal,sum(maoliTotal) as maoliTotal,sum(InvoTotal)-sum(goodTotal) as TrueLiRun,sum(SellFPTotal) as SellFPTotal from (
select CG_POOrder.PONo,  
 sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal,
  sum(maoli) as maoliTotal, AE,
isnull(avg(InvoTotal),0) as InvoTotal,isnull(avg(SellFPTotal),0) as SellFPTotal  
from CG_POOrder  left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo 
 left join (select PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  
where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) 
as ntb2 on CG_POOrder.PONo=ntb2.PONo where ifzhui=0  and CG_POOrder.Status='通过' 
 and CG_POOrder.PODate<='{0} 23:59:59' and CG_POOrder.PODate>='{2} 00:00:00' {1}  and GuestType='企业用户' and
 EXISTS (select ID from CG_POOrder where --AppName=9 AND 
PONO=JXC_REPORT.PONO  
and IsSpecial=0  and IsSelected=1 and JieIsSelected=1) GROUP BY  CG_POOrder.PONo,AE) as allNewTb 
group by AE;
select AE,sum(goodSellTotal) as goodSellTotal,sum(maoliTotal) as maoliTotal,sum(InvoTotal)-sum(goodTotal) as TrueLiRun,sum(SellFPTotal) as SellFPTotal from (
select CG_POOrder.PONo,  
 sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal,
  sum(maoli) as maoliTotal, AE,
isnull(avg(InvoTotal),0) as InvoTotal,isnull(avg(SellFPTotal),0) as SellFPTotal  
from CG_POOrder  left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo 
 left join (select PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  
where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) 
as ntb2 on CG_POOrder.PONo=ntb2.PONo where ifzhui=0  and CG_POOrder.Status='通过' 
 and CG_POOrder.PODate<='{0} 23:59:59'  and CG_POOrder.PODate>='{2} 00:00:00' {1}  and GuestType='政府部门' and
 EXISTS (select ID from CG_POOrder where --AppName=9 AND 
PONO=JXC_REPORT.PONO  
and IsSpecial=0 and IsSelected=1 and JieIsSelected=1) GROUP BY  CG_POOrder.PONo,AE) as allNewTb 
group by AE;
", endDate.ToString("yyyy-MM-dd"), aeSql, fristDate.ToString("yyyy-MM-dd")));

            string whereAE = "";

            DataTable dt1 = allDs.Tables[0];
            if (dt1.Rows.Count > 0)
            {
                whereAE = " and LoginName in (";
            }
            allList = new List<AllSellTotalModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                allList.Add(new AllSellTotalModel { AE = dr[0].ToString(), PoTotal = Convert.ToDecimal(dr[1]), PoLiRunTotal = Convert.ToDecimal(dr[2]) });
                whereAE += string.Format("'{0}',", dr[0].ToString());
            }
            whereAE = whereAE.Trim(',');
            if (whereAE != "")
            {
                whereAE += ")";
            }
            if (whereAE != "")
            {
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                user = userSer.getAllUserByLoginName_Report(whereAE);
            }
            else
            {
                user = new List<User>();
            }



            DataTable dt2 = allDs.Tables[1];
            foreach (DataRow dr in dt2.Rows)
            {
                string ae = dr[0].ToString();
                var goodSellTotal = Convert.ToDecimal(dr[1]);
                var maoLiTotal = Convert.ToDecimal(dr[2]);
                var tureLiRun = Convert.ToDecimal(dr[3]);
                var sellFpTotal = Convert.ToDecimal(dr[4]);

                var model = allList.Find(t => t.AE == ae);
                model.TureliRun_QZ = tureLiRun;
                model.MaoLi_QZ = maoLiTotal;
                model.SellTotal_QZ = goodSellTotal;
                model.sellFPTotal_QZ = sellFpTotal;

            }

            DataTable dt3 = allDs.Tables[2];
            foreach (DataRow dr in dt3.Rows)
            {
                string ae = dr[0].ToString();
                var goodSellTotal = Convert.ToDecimal(dr[1]);
                var maoLiTotal = Convert.ToDecimal(dr[2]);
                var tureLiRun = Convert.ToDecimal(dr[3]);
                var sellFpTotal = Convert.ToDecimal(dr[4]);

                var model = allList.Find(t => t.AE == ae);
                model.TureliRun_ZZ = tureLiRun;
                model.MaoLi_ZZ = maoLiTotal;
                model.SellTotal_ZZ = goodSellTotal;
                model.sellFPTotal_ZZ = sellFpTotal;

            }

            DataTable dt4 = allDs.Tables[3];
            foreach (DataRow dr in dt4.Rows)
            {
                string ae = dr[0].ToString();
                var goodSellTotal = Convert.ToDecimal(dr[1]);
                var maoLiTotal = Convert.ToDecimal(dr[2]);
                var tureLiRun = Convert.ToDecimal(dr[3]);
                var sellFpTotal = Convert.ToDecimal(dr[4]);

                var model = allList.Find(t => t.AE == ae);
                model.TureliRun_QXZ = tureLiRun;
                model.MaoLi_QXZ = maoLiTotal;
                model.SellTotal_QXZ = goodSellTotal;
                model.sellFPTotal_QXZ = sellFpTotal;

            }

            DataTable dt5 = allDs.Tables[4];
            foreach (DataRow dr in dt5.Rows)
            {
                string ae = dr[0].ToString();
                var goodSellTotal = Convert.ToDecimal(dr[1]);
                var maoLiTotal = Convert.ToDecimal(dr[2]);
                var tureLiRun = Convert.ToDecimal(dr[3]);
                var sellFpTotal = Convert.ToDecimal(dr[4]);

                var model = allList.Find(t => t.AE == ae);
                model.TureliRun_ZXZ = tureLiRun;
                model.MaoLi_ZXZ = maoLiTotal;
                model.SellTotal_ZXZ = goodSellTotal;
                model.sellFPTotal_ZXZ = sellFpTotal;

            }
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            //if (txtFrom.Text == "")
            //{
            //    return;
            //}

            show();
        }
        public string xlfile = "Sell.xls";
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //if (txtFrom.Text == "")
            //{
            //    return;
            //}

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + xlfile);
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文
            this.EnableViewState = false;
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            show();
            Panel2.RenderControl(hw);
            Panel1.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string where = "";
            List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
            if (ddlCompany.Text != "-1")
            {
                where = string.Format(" and tb_User.CompanyCode ='{0}'", ddlCompany.Text.Split(',')[2]);
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                user = userSer.getAllUserByPOList(where);
                user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
            }


            ddlUser.DataSource = user;
            ddlUser.DataBind();
            ddlUser.DataTextField = "LoginName";
            ddlUser.DataValueField = "Id";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {


            //            Response.Clear();
            //            Response.ContentType = "application/vnd.ms-excel";
            //            Response.Charset = "GB2312";
            //            Response.AppendHeader("Content-Disposition", "attachment;filename=" + xlfile);
            //            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文
            //            this.EnableViewState = false;
            //            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            //            Response.Write("<?xml version='1.0'?><?mso-application progid='Excel.Sheet'?>");
            //            Response.Write(@"<Workbook xmlns='urn:schemas-microsoft-com:office:spreadsheet'  
            //      xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel'  
            //      xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet' xmlns:html='http://www.w3.org/TR/REC-html40'>");  
            //            System.IO.StringWriter sw = new System.IO.StringWriter();
            //            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            //            show();
            //            for (int i = 0; i < 10; i++)
            //            {
            //                Response.Write("<Worksheet ss:Name='Sheet" + (i + 1) + "'>");
            //                Panel2.RenderControl(hw);
            //                Panel1.RenderControl(hw);
            //                Response.Write(sw.ToString());
            //                Response.Write("</Worksheet>");
            //                Response.Flush(); 
            //            }
            //            Response.Write("</Workbook>");  
            //            Response.End();


            //Response.Clear();
            Response.ClearContent();
            Response.BufferOutput = true;
            Response.Charset = "utf-8";
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            //Response.AppendHeader("Content-Disposition", "attachment;filename="+Server.UrlEncode("孟宪会Excel表格测试")+".xls");  
            // 采用下面的格式，将兼容 Excel 2003,Excel 2007, Excel 2010。  
            this.EnableViewState = false;
            String FileName = "销售结算明细表";
            if (!String.IsNullOrEmpty(Request.UserAgent))
            {
                // firefox 里面文件名无需编码。  
                if (!(Request.UserAgent.IndexOf("Firefox") > -1 && Request.UserAgent.IndexOf("Gecko") > -1))
                {
                    FileName = Server.UrlEncode(FileName);
                }
            }
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName + ".xls");

            Response.Write("<?xml version='1.0'?><?mso-application progid='Excel.Sheet'?>");
            //Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            Response.Write(@"<Workbook xmlns='urn:schemas-microsoft-com:office:spreadsheet'  
      xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:x='urn:schemas-microsoft-com:office:excel'  
      xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet' xmlns:html='http://www.w3.org/TR/REC-html40'>");

            Response.Write(@"<Styles><Style ss:ID='Default' ss:Name='Normal'><Alignment ss:Vertical='Center'/>  
      <Borders/><Font ss:FontName='宋体' x:CharSet='134' ss:Size='12'/><Interior/><NumberFormat/><Protection/></Style>");
            //定义标题样式      
            Response.Write(@"<Style ss:ID='Header'><Borders><Border ss:Position='Bottom' ss:LineStyle='Continuous' ss:Weight='1'/>  
       <Border ss:Position='Left' ss:LineStyle='Continuous' ss:Weight='1'/>  
       <Border ss:Position='Right' ss:LineStyle='Continuous' ss:Weight='1'/>  
       <Border ss:Position='Top' ss:LineStyle='Continuous' ss:Weight='1'/></Borders>  
       <Font ss:FontName='宋体' x:CharSet='134' ss:Size='18' ss:Color='#FF0000' ss:Bold='1'/></Style>");

            //定义边框  
            Response.Write(@"<Style ss:ID='border'><NumberFormat ss:Format='@'/><Borders>  
      <Border ss:Position='Bottom' ss:LineStyle='Continuous' ss:Weight='1'/>  
      <Border ss:Position='Left' ss:LineStyle='Continuous' ss:Weight='1'/>  
      <Border ss:Position='Right' ss:LineStyle='Continuous' ss:Weight='1'/>  
      <Border ss:Position='Top' ss:LineStyle='Continuous' ss:Weight='1'/></Borders></Style>");


            Response.Write(@"<Style ss:ID='s82'><Borders>
    <Border ss:Position='Bottom' ss:LineStyle='Continuous' ss:Weight='1'/>
    <Border ss:Position='Left' ss:LineStyle='Continuous' ss:Weight='1'/>
    <Border ss:Position='Right' ss:LineStyle='Continuous' ss:Weight='1'/>
    <Border ss:Position='Top' ss:LineStyle='Continuous' ss:Weight='1'/>
   </Borders><Font ss:FontName='宋体' x:CharSet='134' ss:Size='12' ss:Color='#FFFFFF'/>
   <Interior ss:Color='#366092' ss:Pattern='Solid'/>
   <NumberFormat ss:Format='@'/></Style>");

            Response.Write("</Styles>");




            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            show();

            foreach (var u in user)
            {
                int sumCount = all_propertyList.Count + 6;
                if (sumCount % 2 == 0)
                {
                    sumCount = sumCount / 2;
                }
                else
                {
                    sumCount = sumCount / 2;
                }
                Response.Write("<Worksheet ss:Name='" + u.LoginName + "'>");

                
                Response.Write(@"<Table x:FullColumns='1' x:FullRows='1' ss:DefaultColumnWidth='73'>");
                string report1_String = "<Row ss:AutoFitHeight='1' ss:style='height: 20px; background-color: #336699; color: White;'>";
       
                Response.Write(string.Format("<Row ss:AutoFitHeight='1' ss:style='height: 20px; background-color: #336699; color: White;'>"));
                Response.Write(string.Format("<Cell ss:StyleID='s82' ss:MergeAcross='{4}'  ><Data ss:Type='String'>公司名称:{0}     财年年月:{1} 公司年度总销售额:{2}     公司年度总用车里程数:{3}</Data></Cell>",
                    lblSimpName1.Text, lblYearMonth.Text, lblSellTotal.Text, lblCarTotal.Text, (sumCount - 1)));
                Response.Write("</Row>");

                //body
                Response.Write(string.Format("<Row ss:AutoFitHeight='1'>"));

                GetHtml_S82("费用类型");
               
                for (int i = 0; i < all_propertyList.Count; i++)
                {
                    if (i < sumCount-1)
                    {
                        GetHtml_S82(all_propertyList[i].CostType);
                    }
                    else
                    {
                        report1_String += GetHtml_S82_String(all_propertyList[i].CostType);
                    }

                }

                report1_String += GetHtml_S82_String("总计");
                report1_String += GetHtml_S82_String("用车公里数");
                report1_String += GetHtml_S82_String("用车百分比");
                report1_String += GetHtml_S82_String("总销售额");
                report1_String += GetHtml_S82_String("销售额百分比");
                report1_String += "</Row>";
                Response.Write("</Row>");

                //具体数据
                decimal sum_AllTotal = 0;
                decimal sum_RoadLong = 0;
                System.Collections.Generic.Dictionary<int, decimal> tsTbComm = new System.Collections.Generic.Dictionary<int, decimal>();
                System.Collections.Generic.Dictionary<int, decimal> tsTbSpec = new System.Collections.Generic.Dictionary<int, decimal>();

                decimal RoadLong = 0;
                var carM = UseCarDetailList.Find(t => t.UserId == u.Id);
                if (carM != null)
                {
                    RoadLong = carM.RoadLong;
                }
                decimal sellT = 0;
                var sellM = reportDetailList.Find(t => t.AE == u.LoginName);
                if (sellM != null)
                {
                    sellT = sellM.SellTotal;
                }
                string baifenbi = "0%";

                //所有费用汇总
                decimal allTotal = 0;
                var user_spec_propertyList = spec_propertyList.FindAll(uu => uu.UserID == u.Id);


                if (RoadLongTotal == 0 && SellTotal == 0 && user_spec_propertyList.Sum(t => t.Total) == 0)
                {
                    continue;
                }
                Response.Write(string.Format("<Row ss:AutoFitHeight='1'>"));
                report1_String += "<Row ss:AutoFitHeight='1'>";
                GetHtml(u.LoginName);

                int index = 0;
                foreach (var m in all_propertyList)
                {

                    if (m.MyProperty == "公共")
                    {
                        decimal m_value = 0;
                        var com_m = comm_propertyList.Find(t => t.CostTypeId == m.Id && t.CompId == u.CompanyId);

                        if (com_m != null)
                        {
                            if (m.CostType == "汽油费补差" || m.CostType == "汽车维修费" || m.CostType == "汽车保险")
                            {
                                if (RoadLongTotal != 0)
                                {
                                    m_value = (RoadLong / RoadLongTotal) * com_m.Total;
                                }

                            }
                            else
                            {
                                if (SellTotal != 0)
                                {
                                    m_value = (sellT / SellTotal) * com_m.Total;
                                    baifenbi = ((sellT / SellTotal) * 100).ToString("n2") + "%";
                                }

                            }
                        }
                        if (!tsTbComm.ContainsKey(m.Id))
                        {
                            tsTbComm.Add(m.Id, m_value);
                        }
                        else
                        {
                            tsTbComm[m.Id] = tsTbComm[m.Id] + m_value;
                        }
                        allTotal += m_value;

                        if (index < sumCount-1)
                        {
                            GetNumber(m_value.ToString("n2"));
                        }
                        else
                        {
                            report1_String += GetNumber_String(m_value.ToString("n2"));
                        }

                       
                    }
                    if (m.MyProperty == "个性")
                    {
                        decimal m_value = 0;
                        var spec_m = user_spec_propertyList.Find(t => t.CostTypeId == m.Id && t.CompId == u.CompanyId);
                        if (spec_m != null)
                        {
                            m_value = spec_m.Total;
                        }
                        if (!tsTbSpec.ContainsKey(m.Id))
                        {
                            tsTbSpec.Add(m.Id, m_value);
                        }
                        else
                        {
                            tsTbSpec[m.Id] = tsTbSpec[m.Id] + m_value;
                        }
                        allTotal += m_value;
                        //GetNumber(m_value.ToString());

                        if (index < sumCount-1)
                        {
                            GetNumber(m_value.ToString("n2"));
                        }
                        else
                        {
                            report1_String += GetNumber_String(m_value.ToString("n2"));
                        }
                    }
                    index++;
                }
                u.Total = allTotal;
                sum_RoadLong += RoadLong;

                report1_String += GetNumber_String(allTotal.ToString("n2"));
                report1_String += GetNumber_String(RoadLong.ToString());
                if (Convert.ToDecimal(lblCarTotal.Text) == 0)
                {
                    report1_String += GetNumber_String("0");
                }
                else
                {
                    report1_String += GetNumber_String(((RoadLong / Convert.ToDecimal(lblCarTotal.Text)) * 100).ToString("n2") + "%");
                }
                report1_String += GetNumber_String(sellT.ToString("n2"));
                report1_String += GetNumber_String(baifenbi.ToString());
                report1_String += "</Row>";
                Response.Write("</Row>");
                Response.Write(report1_String);
                sum_AllTotal += allTotal;

                //Response.Write("</Table>");

                ////第二个表
                //Response.Write(@"<Table x:FullColumns='1' x:FullRows='1'>");

                Response.Write(string.Format("<Row ss:AutoFitHeight='1' ss:colspan='21' ss:style='height: 20px; background-color: #336699; color: White;'>"));

                Response.Write(string.Format("<Cell ss:StyleID='s82'  ss:MergeAcross='10' ><Data ss:Type='String'>公司：{0}</Data></Cell>", lblSimpName.Text));


                Response.Write("</Row>");

                string report2_String = "<Row ss:AutoFitHeight='1'><Cell ss:StyleID='s82'><Data ss:Type='String'> </Data></Cell><Cell ss:StyleID='s82' ss:MergeAcross='9'><Data ss:Type='String'>选中合计</Data></Cell></Row>";
                Response.Write(string.Format("<Row ss:AutoFitHeight='1'>"));
                Response.Write(string.Format("<Cell ss:StyleID='s82'><Data ss:Type='String'> </Data></Cell>", lblYearMonth1.Text));

                Response.Write(string.Format("<Cell ss:StyleID='s82' ss:MergeAcross='9'><Data ss:Type='String'>全年合计</Data></Cell>"));

                //Response.Write(string.Format("<Cell ss:StyleID='s82' ss:MergeAcross='9'><Data ss:Type='String'>选中合计</Data></Cell>"));


                Response.Write("</Row>");
                //body

            
                Response.Write(string.Format("<Row ss:AutoFitHeight='1'>"));
                Response.Write(string.Format("<Cell ss:StyleID='s82'><Data ss:Type='String'>财年：{0}</Data></Cell>", lblYearMonth1.Text));
                GetHtml_S82("总销售额");
                GetHtml_S82("企业销售额");
                GetHtml_S82("企业利润");
                GetHtml_S82("政府销售额");
                GetHtml_S82("政府利润");
                GetHtml_S82("总成本");
                GetHtml_S82("企业成本");
                GetHtml_S82("政府成本");
                GetHtml_S82("企业净利");
                GetHtml_S82("政府净利");

                report2_String += "<Row ss:AutoFitHeight='1'>";
                report2_String += GetHtml_S82_String("");
                report2_String+=GetHtml_S82_String("总销售额");
                report2_String += GetHtml_S82_String("企业销售额");
                report2_String += GetHtml_S82_String("企业利润");
                report2_String += GetHtml_S82_String("政府销售额");
                report2_String += GetHtml_S82_String("政府利润");
                report2_String += GetHtml_S82_String("总成本");
                report2_String += GetHtml_S82_String("企业成本");
                report2_String += GetHtml_S82_String("政府成本");
                report2_String += GetHtml_S82_String("企业净利");
                report2_String += GetHtml_S82_String("政府净利");
                report2_String += "</Row>";
                 Response.Write(string.Format("</Row>"));
                decimal SUM_X1 = 0;
                decimal SUM_X6 = 0;
                decimal SUM_X7 = 0;
                decimal SUM_X8 = 0;
                decimal SUM_X9 = 0;
                decimal SUM_X10 = 0;
                decimal SUM_X11 = 0;
                decimal SUM_X16 = 0;
                decimal SUM_X17 = 0;
                decimal SUM_X18 = 0;
                decimal SUM_X19 = 0;
                decimal SUM_X20 = 0;

                decimal AllTotal = 0;
                var model = allList.Find(t=>t.AE==u.LoginName);
                //foreach (var model in allList)
                //{
                    decimal X1 = model.SellTotal_QZ + model.SellTotal_ZZ;
                    //var u = user.Find(t => t.LoginName == model.AE);
                    decimal X6 = u != null ? u.Total : 0;
                    decimal X7 = X1 != 0 ? model.SellTotal_QZ / X1 * X6 : 0;
                    decimal X8 = X1 != 0 ? model.SellTotal_ZZ / X1 * X6 : 0;
                    decimal X9 = model.MaoLi_QZ - X7;
                    decimal X10 = model.MaoLi_ZZ - X8;
                    decimal X11 = model.SellTotal_QXZ + model.SellTotal_ZXZ;
                    decimal X16 = X1 != 0 ? X11 / X1 * X6 : 0;
                    decimal X17 = X11 != 0 ? model.SellTotal_QXZ / X11 * X16 : 0;
                    decimal X18 = X11 != 0 ? model.SellTotal_ZXZ / X11 * X16 : 0;
                    decimal X19 = model.MaoLi_QXZ - X17;
                    decimal X20 = model.MaoLi_ZXZ - X18;
                    SUM_X1 += X1;
                    SUM_X6 += X6;
                    SUM_X7 += X7;
                    SUM_X8 += X8;
                    SUM_X9 += X9;
                    SUM_X10 += X10;
                    SUM_X11 += X11;
                    SUM_X16 += X16;
                    SUM_X17 += X17;
                    SUM_X18 += X18;
                    SUM_X19 += X19;
                    SUM_X20 += X20;
                    Response.Write(string.Format("<Row ss:AutoFitHeight='1'>"));

                    GetHtml(model.AE);
                    GetNumber(X1.ToString());
                    GetNumber(model.SellTotal_QZ);
                    GetNumber(model.MaoLi_QZ);
                    GetNumber(model.SellTotal_ZZ);
                    GetNumber(model.MaoLi_ZZ);
                    GetNumber(X6.ToString("n4"));
                    GetNumber(X7.ToString("n4"));
                    GetNumber(X8.ToString("n4"));
                    GetNumber(X9.ToString("n4"));
                    GetNumber(X10.ToString("n4"));

                    report2_String += "<Row ss:AutoFitHeight='1'>";
                    report2_String += GetNumber_String(" ");
                    report2_String += GetNumber_String(X11.ToString("n4"));
                    report2_String += GetNumber_String(model.SellTotal_QXZ);
                    report2_String += GetNumber_String(model.MaoLi_QXZ);
                    report2_String += GetNumber_String(model.SellTotal_ZXZ);
                    report2_String += GetNumber_String(model.MaoLi_ZXZ);
                    report2_String += GetNumber_String(X16.ToString("n4"));
                    report2_String += GetNumber_String(X17.ToString("n4"));
                    report2_String += GetNumber_String(X18.ToString("n4"));
                    report2_String += GetNumber_String(X19.ToString("n4"));

                    report2_String += GetNumber_String(X20.ToString("n4"));
                    report2_String += "</Row>";

                    Response.Write("</Row>");
                //}

                Response.Write(report2_String);
                Response.Write("</Table>");

                Response.Write("</Worksheet>");
                Response.Flush();
            }
            Response.Write("</Workbook>");
            Response.End();



        }

         
            
       

        private void GetHtml(object value)
        {
            Response.Write(string.Format("<Cell ss:StyleID='border'><Data ss:Type='String'>{0}</Data></Cell>", value.ToString()));
        }

        private void GetHtml_S82(object value)
        {
            Response.Write(string.Format("<Cell ss:StyleID='s82'><Data ss:Type='String'>{0}</Data></Cell>", value.ToString()));
        }

        private string GetHtml_S82_String(object value)
        {
            return string.Format("<Cell ss:StyleID='s82'><Data ss:Type='String'>{0}</Data></Cell>", value.ToString());
        }

        private void GetNumber(object value)
        {
            Response.Write(string.Format("<Cell ss:StyleID='border'><Data ss:Type='String'>{0}</Data></Cell>", value));
        }
        private string GetNumber_String(object value)
        {
          return string.Format("<Cell ss:StyleID='border'><Data ss:Type='String'>{0}</Data></Cell>", value);
        }

        private void StringBu_2()
        { 
            
        }
    }
}
