using System;
using System.Data;
using System.Web.UI.WebControls;
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.Fin;
using VAN_OA.Model.Fin;
using VAN_OA.Model;

namespace VAN_OA.JXC
{
    public partial class BI_WFAllSellTotal : BasePage
    {
        protected List<AllSellTotalModel> allList = new List<AllSellTotalModel>();
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



                GuestTypeBaseInfoService dal = new GuestTypeBaseInfoService();
                var dalList = dal.GetListArray("");
                dalList.Insert(0, new VAN_OA.Model.BaseInfo.GuestTypeBaseInfo { GuestType = "全部" });
                ddlGuestTypeList.DataSource = dalList;
                ddlGuestTypeList.DataBind();
                ddlGuestTypeList.DataTextField = "GuestType";
                ddlGuestTypeList.DataValueField = "GuestType";



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
                if (QuanXian_ShowAll("商业BI图表") == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                    ddlCompany.Enabled = false;
                }



                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll("商业BI图表", Session["currentUserId"], "WFScanDepartList") == true)
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

                GuestProBaseInfoService guestProBaseInfodal = new GuestProBaseInfoService();
                var proList = guestProBaseInfodal.GetListArray("");
                proList.Insert(0, new VAN_OA.Model.BaseInfo.GuestProBaseInfo { GuestPro = -2 });
                ddlGuestProList.DataSource = proList;
                ddlGuestProList.DataBind();
                ddlGuestProList.DataTextField = "GuestProString";
                ddlGuestProList.DataValueField = "GuestPro";


                List<TB_BasePoType> basePoTypeList = new TB_BasePoTypeService().GetListArray("");
                basePoTypeList.Insert(0, new TB_BasePoType { BasePoType = "全部", Id = -1 });
                ddlPOTyle.DataSource = basePoTypeList;
                ddlPOTyle.DataBind();
                ddlPOTyle.DataTextField = "BasePoType";
                ddlPOTyle.DataValueField = "Id";

                for (var i = 2012; i <= DateTime.Now.Year; i++)
                {
                    ddlYear.Items.Add(new ListItem { Value = i.ToString(), Text = i.ToString() });

                    ddlNextYear.Items.Add(new ListItem { Value = i.ToString(), Text = i.ToString() });
                }
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }

        public void show()
        {

            string allSimpNames = "";
            string aeSql = "";
            if (ddlCompany.Text == "-1" && ddlUser.Text != "-1")
            {
                aeSql = string.Format(" and exists (select id from tb_User where ID={0} and appName=id)", ddlUser.Text);
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[0]);

                if (ddlUser.Text != "-1")
                {
                    where += string.Format(" and ID={0} ", ddlUser.Text);
                }
                allSimpNames += ddlCompany.Text.Split(',')[1];
                aeSql = string.Format(" and exists (select id from tb_User where {0} and appName=id)", where);

            }
            else
            {
                foreach (ListItem m in ddlCompany.Items)
                {
                    if (m.Value != "-1")
                    {
                        allSimpNames += m.Value.Split(',')[1] + ",";
                    }
                }
            }
            if (ddlIsClose.Text != "-1")
            {
                aeSql += " and IsClose=" + ddlIsClose.Text;
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                aeSql += " and JieIsSelected=" + ddlJieIsSelected.Text;
            }


            if (ddlGuestProList.SelectedValue != "-2")
            {
                aeSql += string.Format(" and GuestPro={0}", ddlGuestProList.SelectedValue);
            }
            if (ddlPOTyle.Text != "-1")
            {
                aeSql += string.Format(" and POType={0} ", ddlPOTyle.Text);
            }
            //==== 一下是新增
            if (ddlIsSpecial.Text != "-1")
            {
                aeSql += " and IsSpecial=" + ddlIsSpecial.Text;
            }
            if (ddlFax.Text != "-1")
            {
                aeSql += string.Format(" and IsPoFax={0}", ddlFax.Text);
            }
            if (ddlModel.Text != "全部")
            {
                aeSql += string.Format(" and Model='{0}'", ddlModel.Text);
            }
            if (ddlGuestTypeList.SelectedValue != "全部")
            {
                aeSql += string.Format(" and GuestType='{0}'", ddlGuestTypeList.SelectedValue);
            }
            if (ddlIsSelect.Text != "-1")
            {
                aeSql += " and IsSelected=" + ddlIsSelect.Text;
            }


            string havingSQL = " having 1=1 ";
            if (ddlFuHao.Text != "-1")
            {
                havingSQL += string.Format(" and sum(SumPOTotal){0}sum(goodSellTotal)", ddlFuHao.Text);
            }
            if (ddlPOFaTotal.Text != "-1")
            {
                havingSQL += string.Format(" and sum(SumPOTotal){0}sum(SellFPTotal)", ddlPOFaTotal.Text);
            }
            if (ddlShiJiDaoKuan.Text != "-1")
            {
                havingSQL += string.Format(" and sum(SumPOTotal){0}sum(InvoTotal)", ddlShiJiDaoKuan.Text);
            }
            if (ddlEquPOTotal.Text != "-1")
            {
                if (CommHelp.VerifesToNum(txtEquTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目金额 格式错误！');</script>");
                    return;
                }
                havingSQL += string.Format(" and sum(SumPOTotal){0}{1}", ddlEquPOTotal.Text, Convert.ToDecimal(txtEquTotal.Text));
            }
            if (ddlJingLiTotal.Text != "-1")
            {
                havingSQL += string.Format(" and sum(maoliTotal) {0} (sum(InvoTotal)-sum(goodTotal))", ddlJingLiTotal.Text);
            }

            if (!string.IsNullOrEmpty(txtProProfit.Text) && ddlProProfit.Text != "-1")
            {
                if (CommHelp.VerifesToNum(txtProProfit.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目净利 格式错误！');</script>");
                    return;
                }
                havingSQL += string.Format(" and sum(maoliTotal) {0} {1}", ddlProProfit.Text, txtProProfit.Text);
            }

            if (!string.IsNullOrEmpty(txtProTureProfit.Text) && ddlProTureProfit.Text != "-1")
            {
                if (CommHelp.VerifesToNum(txtProTureProfit.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('实际净利 格式错误！');</script>");
                    return;
                }
                havingSQL += string.Format(" and sum(InvoTotal)-sum(goodTotal)  {0} {1}", ddlProTureProfit.Text, txtProTureProfit.Text);
            }

            if (ddlJingLi.Text != "-1" && !string.IsNullOrEmpty(txtJingLi.Text))
            {
                if (CommHelp.VerifesToNum(txtJingLi.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('净利润率 格式错误！');</script>");
                    return;
                }

                string jinLiSql = "and 1=1";
                if (txtJingLi.Text == "0")
                {
                    decimal jingLi = Convert.ToDecimal(txtJingLi.Text);
                    if (ddlJingLi.Text == ">")
                    {
                        jinLiSql += string.Format(" and sum(goodSellTotal)!=0 AND sum(maoliTotal)/sum(goodSellTotal)>0 ");
                    }
                    if (ddlJingLi.Text == "<")
                    {
                        jinLiSql += string.Format(" and sum(goodSellTotal)!=0 AND sum(maoliTotal)/sum(goodSellTotal)<0 ");
                    }
                    if (ddlJingLi.Text == ">=")
                    {
                        jinLiSql += string.Format(" and (sum(goodSellTotal)=0 or sum(maoliTotal)/sum(goodSellTotal)>=0) ");
                    }
                    if (ddlJingLi.Text == "<=")
                    {
                        jinLiSql += string.Format(" and (sum(goodSellTotal)=0 or sum(maoliTotal)/sum(goodSellTotal)<=0) ");
                    }
                    if (ddlJingLi.Text == "=")
                    {
                        jinLiSql += string.Format(" and sum(goodSellTotal)=0 ");
                    }
                }
                else
                {
                     jinLiSql += string.Format(" and sum(goodSellTotal)!=0 AND sum(maoliTotal)/sum(goodSellTotal){0}{1} ", ddlJingLi.Text, txtJingLi.Text);                    
                }

                havingSQL += jinLiSql;
            }

            if (havingSQL == " having 1=1 ")
            {
                havingSQL = "";
            }

            lblSimpName.Text = allSimpNames.Trim(',') + "-" + ddlYear.Text + "年";
            if (cbCompare.Checked)
            {
                lblSimpName.Text = allSimpNames.Trim(',') + "-" + ddlNextYear.Text + "~" + ddlYear.Text + "年项目总额";
            }
            lblOtherName.Text = allSimpNames.Trim(',') + "-" + ddlYear.Text + "年";
            var allDs = DBHelp.getDataSet(string.Format(@"select AE,sum(goodSellTotal) as goodSellTotal,sum(maoliTotal) as maoliTotal,sum(InvoTotal)-sum(goodTotal) as TrueLiRun,sum(SellFPTotal) as SellFPTotal,sum(SumPOTotal) as SumPOTotal from (
select CG_POOrder.PONo,  
 sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal,
  sum(maoli) as maoliTotal, AE,
isnull(avg(InvoTotal),0) as InvoTotal,isnull(avg(SellFPTotal),0) as SellFPTotal  
from CG_POOrder  left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo 
 left join (select PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  
where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) 
as ntb2 on CG_POOrder.PONo=ntb2.PONo where ifzhui=0  and CG_POOrder.Status='通过' 
 and year(CG_POOrder.PODate)={0} {1}  GROUP BY  CG_POOrder.PONo,AE ) as allNewTb left join POTotal_SumView on  allNewTb.PONo=POTotal_SumView.pono
group by AE  {2};
select AE,sum(goodSellTotal) as goodSellTotal,sum(maoliTotal) as maoliTotal,sum(InvoTotal)-sum(goodTotal) as TrueLiRun,sum(SellFPTotal) as SellFPTotal,sum(SumPOTotal) as SumPOTotal  from (
select CG_POOrder.PONo,  
 sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal,
  sum(maoli) as maoliTotal, AE,
isnull(avg(InvoTotal),0) as InvoTotal,isnull(avg(SellFPTotal),0) as SellFPTotal  
from CG_POOrder  left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo 
 left join (select PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  
where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) 
as ntb2 on CG_POOrder.PONo=ntb2.PONo where ifzhui=0  and CG_POOrder.Status='通过' 
 and year(CG_POOrder.PODate)={0} {1}  and GuestType='企业用户'   GROUP BY  CG_POOrder.PONo,AE ) as allNewTb left join POTotal_SumView on  allNewTb.PONo=POTotal_SumView.pono
group by AE {2};
select AE,sum(goodSellTotal) as goodSellTotal,sum(maoliTotal) as maoliTotal,sum(InvoTotal)-sum(goodTotal) as TrueLiRun,sum(SellFPTotal) as SellFPTotal,sum(SumPOTotal) as SumPOTotal  from (
select CG_POOrder.PONo,  
 sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal,
  sum(maoli) as maoliTotal, AE,
isnull(avg(InvoTotal),0) as InvoTotal,isnull(avg(SellFPTotal),0) as SellFPTotal  
from CG_POOrder  left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo 
 left join (select PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  
where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) 
as ntb2 on CG_POOrder.PONo=ntb2.PONo where ifzhui=0  and CG_POOrder.Status='通过' 
 and year(CG_POOrder.PODate)='{0}' {1}  and GuestType='政府部门'  GROUP BY  CG_POOrder.PONo,AE ) as allNewTb  left join POTotal_SumView on  allNewTb.PONo=POTotal_SumView.pono
group by AE {2};", ddlYear.Text, aeSql,havingSQL));


            DataTable dt1 = allDs.Tables[0];
            allList = new List<AllSellTotalModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                allList.Add(new AllSellTotalModel { AE = dr[0].ToString(), PoTotal = Convert.ToDecimal(dr[1]), PoLiRunTotal = Convert.ToDecimal(dr[2]), SumPOTotal = Convert.ToDecimal(dr[5]) });
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
                model.SumPOTotal_QZ = Convert.ToDecimal(dr[5]);

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
                model.SumPOTotal_ZZ = Convert.ToDecimal(dr[5]);
            }


            List<AEPromiseTotal> aeProList = new AEPromiseTotalService().GetListArray(string.Format(" YearNo={0} ", ddlYear.Text.Trim()));
            //DateTime.Now.DayOfYear
            var year = Convert.ToInt32(ddlYear.Text);
            var ts = Convert.ToDateTime(year + "-1-1") - Convert.ToDateTime(year + 1 + "-1-1");
            var yearDays = ts.Days;//一年有多少天
            decimal pv = 1;
            if (DateTime.Now.Year == year)
            {
                pv = DateTime.Now.DayOfYear / yearDays;
            }
            foreach (var m in allList)
            {
                var aePro = aeProList.Find(t => t.AE == m.AE);
                if (aePro != null && aePro.Id > 0)
                {
                    m.PV = aePro.PromiseSellTotal * pv;
                }
                if (m.PV != 0)
                    m.SPI = m.PoTotal / m.PV;
            }


            string yearSQL = "";
            if (cbCompare.Checked)
            {
                yearSQL = string.Format("in ({0},{1})", ddlYear.Text, ddlNextYear.Text);
            }
            else
            {
                yearSQL = string.Format("={0}", ddlYear.Text);
            }


            string monthPoSumSql = string.Format(@"Select  AE,sum(SumPOTotal) as SumPOTotal,MONTH(PODate) AS MON,Year(PODate) as YY from (
select CG_POOrder.PONo,  CG_POOrder.PODate,
 sum(goodSellTotal) as goodSellTotal,sum(goodTotal)+sum(t_goodTotalChas) as goodTotal,
  sum(maoli) as maoliTotal, AE,
isnull(avg(InvoTotal),0) as InvoTotal,isnull(avg(SellFPTotal),0) as SellFPTotal  
from CG_POOrder  left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo 
 left join (select PoNo,SUM(Total) as InvoTotal from  TB_ToInvoice  
where  TB_ToInvoice.state='通过' group by PoNo) as newtable1 on CG_POOrder.PONo=newtable1.PONo 
left join (select SUM(total) as SellFPTotal,PONo from Sell_OrderFP where Status='通过' group by PONo) 
as ntb2 on CG_POOrder.PONo=ntb2.PONo where ifzhui=0  and CG_POOrder.Status='通过' 
 and year(CG_POOrder.PODate) {0} {1} GROUP BY  CG_POOrder.PONo,AE,CG_POOrder.PODate ) as allNewTb left join POTotal_SumView on  allNewTb.PONo=POTotal_SumView.pono
group by AE,MONTH(PODate),Year(PODate); ", yearSQL, aeSql);

            var monthPoSumDT = DBHelp.getDataTable(monthPoSumSql);
            List<BI_Month> months = new List<BI_Month>();
            foreach (DataRow dr in monthPoSumDT.Rows)
            {
                months.Add(new BI_Month
                {
                    Year = Convert.ToInt32(dr["YY"]),
                    AE = dr["AE"].ToString(),
                    Month = Convert.ToInt32(dr["MON"]),
                    SumPOTotal = Convert.ToDecimal(dr["SumPOTotal"])
                });
            }

            ViewState["BI_MonthList"] = months;

            //在销售总额的右面新增2列，项目计划PV=该销售该年承诺销售额*项目天数/该年全年天数，
            //（项目天数指 1.如选今年之前的年度 项目天数 = 所选年度的总天数 或  2.如选今年， 项目天数 = 今年的1月1日到今天的天数（包含两边）），
            //项目进度SPI = 销售总额 / 项目计划PV。该销售该年承诺销售额就是系统中销售指标模块中该销售该年的承诺的总销售额指标。

            //DataTable dt4 = allDs.Tables[3];
            //foreach (DataRow dr in dt4.Rows)
            //{
            //    string ae = dr[0].ToString();
            //    var goodSellTotal = Convert.ToDecimal(dr[1]);
            //    var maoLiTotal = Convert.ToDecimal(dr[2]);
            //    var tureLiRun = Convert.ToDecimal(dr[3]);
            //    var sellFpTotal = Convert.ToDecimal(dr[4]);

            //    var model = allList.Find(t => t.AE == ae);
            //    model.TureliRun_QXZ = tureLiRun;
            //    model.MaoLi_QXZ = maoLiTotal;
            //    model.SellTotal_QXZ = goodSellTotal;
            //    model.sellFPTotal_QXZ = sellFpTotal;

            //}

            //DataTable dt5 = allDs.Tables[4];
            //foreach (DataRow dr in dt5.Rows)
            //{
            //    string ae = dr[0].ToString();
            //    var goodSellTotal = Convert.ToDecimal(dr[1]);
            //    var maoLiTotal = Convert.ToDecimal(dr[2]);
            //    var tureLiRun = Convert.ToDecimal(dr[3]);
            //    var sellFpTotal = Convert.ToDecimal(dr[4]);

            //    var model = allList.Find(t => t.AE == ae);
            //    model.TureliRun_ZXZ = tureLiRun;
            //    model.MaoLi_ZXZ = maoLiTotal;
            //    model.SellTotal_ZXZ = goodSellTotal;
            //    model.sellFPTotal_ZXZ = sellFpTotal;
            //}
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {


            show();
        }
        public string xlfile = "Sell.xls";
        protected void btnExcel_Click(object sender, EventArgs e)
        {



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
            Panel1.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

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

        protected void cbCompare_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCompare.Checked)
            {
                ddlNextYear.Enabled = true;
            }
            else
            {
                ddlNextYear.Enabled = false;
            }

            //string where = "";
            //List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
            //if (ddlCompany.Text != "-1")
            //{
            //    where = string.Format(" and tb_User.CompanyCode ='{0}'", ddlCompany.Text.Split(',')[2]);
            //    VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
            //    user = userSer.getAllUserByPOList(where);
            //    user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
            //}


            //ddlUser.DataSource = user;
            //ddlUser.DataBind();
            //ddlUser.DataTextField = "LoginName";
            //ddlUser.DataValueField = "Id";
        }
    }
}
