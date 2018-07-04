using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;
using VAN_OA.Model;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class JXCItems_REPORTTotalList : BasePage
    {

        protected string GetDate(object obj)
        {
            return string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(obj));
        }

        JXC_REPORTService POSer = new JXC_REPORTService();


        string tiaoJian = "";
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
                //主单
                List<JXC_REPORTTotal> pOOrderList = new List<JXC_REPORTTotal>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //if (VAN_OA.JXC.SysObj.IfShowAll(SysObj.JXC_REPORTTotalList, Session["currentUserId"]) == false)
                //{
                //    ViewState["showAll"] = false;
                //}
                //System.Wen.HttpUtility.HtmlEncode.

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                if (QuanXian_ShowAll("项目费用汇总统计") == false)
                {
                    ViewState["showAll"] = false;
                    var model = Session["userInfo"] as User;
                    user.Insert(0, model);
                }
                else
                {
                    user = userSer.getAllUserByPOList();
                    user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                }
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";




                if (Request["PONo"] != null)
                {
                    txtPONo.Text = Request["PONo"].ToString();
                    Show();
                }
            }
        }


        private void Show()
        {
            string sql = " ";

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and CG_POOrder.PONo like '%{0}%'", txtPONo.Text.Trim());
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and CG_POOrder.POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and CG_POOrder.PODate<='{0} 23:59:59'", txtTo.Text);
            }


            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and CG_POOrder.GuestName  like '%{0}%'", txtGuestName.Text.Trim());
            }
            var isColse = "";
            if (ddlIsClose.Text != "-1")
            {
                isColse = " and IsClose=" + ddlIsClose.Text;
            }

            if (ddlIsSelect.Text != "-1")
            {
                isColse = " and IsSelected=" + ddlIsSelect.Text;
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                isColse = " and JieIsSelected=" + ddlJieIsSelected.Text;
            }
            if (ddlModel.Text != "全部")
            {
                isColse += string.Format(" and Model='{0}'", ddlModel.Text);
            }

            //if (ViewState["showAll"] != null)
            //{
            //    sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=JXC_REPORT.PONO )", Session["currentUserId"]);
            //}


            if (ddlUser.Text == "-1")//显示所有用户
            {
                if (cbIsSpecial.Checked)
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where IsSpecial=0 AND PONO=JXC_REPORT.PONO {0})", isColse);
                }
                //else
                //{
                //    sql += string.Format(" and EXISTS (select ID from CG_POOrder where IsSpecial=1 AND PONO=JXC_REPORT.PONO {0})", isColse);
                //}
                //var model = Session["userInfo"] as User;
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职') AND PONO=JXC_REPORT.PONO )", Session["currentUserId"]);
            }
            //else if (ddlUser.Text == "0")//显示部门信息
            //{
            //    var model = Session["userInfo"] as User;
            //    //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') AND PONO=JXC_REPORT.PONO )", model.LoginIPosition);

            //    if (cbIsSpecial.Checked)
            //    {
            //        sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0}  AND PONO=JXC_REPORT.PONO and IsSpecial=0 {1})", model.Id, isColse);
            //    }
            //    else
            //    {
            //        sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=JXC_REPORT.PONO and IsSpecial=1 {1})", model.Id, isColse);
            //    }
            //}
            else
            {

                if (cbIsSpecial.Checked)
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where AE='{0}' AND PONO=JXC_REPORT.PONO  and IsSpecial=0 {1})", ddlUser.SelectedItem.Text, isColse);
                }
                else
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where AE='{0}' AND PONO=JXC_REPORT.PONO  and IsSpecial=1 {1})", ddlUser.SelectedItem.Text, isColse);

                }
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=JXC_REPORT.PONO )", ddlUser.Text);
            }

            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and EXISTS (select ID from CG_POOrder where  IFZhui=0 and AE IN(select LOGINNAME from tb_User where {0}) AND PONO=JXC_REPORT.PONO)", where);

            }
            if (CheckBox1.Checked || CheckBox2.Checked || CheckBox3.Checked || CheckBox4.Checked)
            {

                sql += " and exists ( select ID from CG_POOrder where PONO=JXC_REPORT.PONO";
                if (CheckBox1.Checked)
                {
                    sql += string.Format(" and POStatue='{0}'", CheckBox1.Text);
                }
                if (CheckBox2.Checked)
                {
                    sql += string.Format(" and POStatue2='{0}'", CheckBox2.Text);
                }
                if (CheckBox3.Checked)
                {
                    sql += string.Format(" and POStatue3='{0}'", CheckBox3.Text);
                }
                if (CheckBox4.Checked)
                {
                    sql += string.Format(" and POStatue4='{0}'", CheckBox4.Text);
                }

                sql += ")";
            }


            string having = " having ";
            if (CheckBox5.Checked && CheckBox6.Checked)
            {
                having += string.Format("  (avg(SellFPTotal)<> sum(goodSellTotal) or  avg(SellFPTotal) is null)  and (avg(InvoTotal)<> sum(goodSellTotal) or sum(InvoTotal) is null)");
            }
            else if (CheckBox5.Checked || CheckBox6.Checked)
            {
                if (CheckBox5.Checked)
                {
                    having += string.Format("  avg(SellFPTotal)<> sum(goodSellTotal) or  avg(SellFPTotal) is null ");
                }
                if (CheckBox6.Checked)
                {
                    having += string.Format("  avg(InvoTotal)<> sum(goodSellTotal) or sum(InvoTotal) is null ");
                }
            }
            else
            {
                having = "";
            }



            if (CheckBox8.Checked)
            {
                tiaoJian += "'" + CheckBox8.Text + "',";
            }
            if (CheckBox9.Checked)
            {
                tiaoJian += "'" + CheckBox9.Text + "',";
            }
            if (CheckBox10.Checked)
            {
                tiaoJian += "'" + CheckBox10.Text + "',";
            }
            if (CheckBox11.Checked)
            {
                tiaoJian += "'" + CheckBox11.Text + "',";
            }
            if (CheckBox12.Checked)
            {
                tiaoJian += "'" + CheckBox12.Text + "',";
            }
            if (CheckBox13.Checked)
            {
                tiaoJian += "'" + CheckBox13.Text + "',";
            }
            if (CheckBox14.Checked)
            {
                tiaoJian += "'" + CheckBox14.Text + "',";

            }
            if (CheckBox15.Checked)
            {
                tiaoJian += "'" + CheckBox15.Text + "',";
            }
            if (CheckBox16.Checked)
            {
                tiaoJian += "'" + CheckBox16.Text + "',";
            }
            if (CheckBox17.Checked)
            {
                tiaoJian += "'" + CheckBox17.Text + "',";
            }

            if (CheckBox7.Checked == false)
            {
                if (tiaoJian == "")
                {
                    tiaoJian = "''";
                }
            }
            List<JXC_REPORTTotal> pOOrderList = this.POSer.GetListArray_Items_Total(sql, having, tiaoJian);
            var getAllPONos = pOOrderList.Aggregate("", (current, m) => current + ("'" + m.PONo + "',")).Trim(',');
            lblVisAllPONO.Text = getAllPONos;
            decimal HuiWuTotal = 0;
            Hashtable hsPONo = new Hashtable();
            decimal allTotal = 0;
            decimal maoliTotal = 0;
            decimal goodSellTotal = 0;
            decimal TrueLiRun = 0;
            decimal SellFPTotal = 0;



            lblItemTotal.Text = pOOrderList.Sum(t => t.itemTotal).ToString();
            foreach (var m in pOOrderList)
            {
                if (!hsPONo.Contains(m.PONo))
                {
                    HuiWuTotal += m.HuiWuTotal;
                    allTotal += m.allItemTotal;
                    maoliTotal += m.maoliTotal;
                    goodSellTotal += m.goodSellTotal;
                    TrueLiRun += m.TrueLiRun;
                    SellFPTotal += m.SellFPTotal;
                    hsPONo.Add(m.PONo, null);
                }
            }

            lblJLR.Text = string.Format("{0:n2}", maoliTotal);
            lblXSE.Text = string.Format("{0:n2}", goodSellTotal);
            lblSJLR.Text = string.Format("{0:n2}", TrueLiRun);
            lblFP.Text = string.Format("{0:n2}", SellFPTotal);

            lblHuiWuTotal.Text = string.Format("{0:n2}", HuiWuTotal);
            lblAllItemTotal.Text = string.Format("{0:n2}", allTotal);

            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();

            lblCurrentPOTotal.Text = "0";
            //项目未到到款金额：XX=项目总金额-到款总额    
            lblPoWeiDaoTotal.Text = "0";
            //已开票未到款：YY=发票总额-到款总额   
            lblInvoiceNoDao.Text = "0";
            //未开票金额：ZZZ=项目总金额-发票总金额
            lblWeiKaiPiao.Text = "0";
            lblPP.Text = "0";
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMain.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                JXC_REPORTTotal m = e.Row.DataItem as JXC_REPORTTotal;
                if (m.IsClose)
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(204, 255, 204);
                }
                if (m.IsQuanDao)
                {
                    var lblDays = e.Row.FindControl("lblDays") as Label;
                    lblDays.Font.Underline = true;
                }
            }

        }

         
        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {

                Response.Redirect("~/JXC/JXC_REPORTList.aspx?PONo=" + e.CommandArgument + "&IsSpecial=" + cbIsSpecial.Checked);

            }
            if (e.CommandName == "selectPoNo")
            {
                string pono = e.CommandArgument.ToString().Split(',')[0];
                string poType = e.CommandArgument.ToString().Split(',')[1];
                gvList.DataSource = getDT(pono, poType);
                gvList.DataBind();
            }
            if (e.CommandName == "PoNo")
            {
                var m = e.CommandArgument.ToString().Split('_');

                var sql = string.Format(@"select max(DaoKuanDate) as MaxDaoKuanDate,SUM(Total) as InvoTotal from  TB_ToInvoice
where  TB_ToInvoice.state='通过' and pono='{0}';
select min(CreateTime) as MinOutDate from Sell_OrderOutHouse left join Sell_OrderOutHouses
on Sell_OrderOutHouse.id=Sell_OrderOutHouses.id where pono='{0}' and Status='通过';", m[0]);
                var dt = DBHelp.getDataSet(sql);

                lblDaoKuanTotal.Text = "";
                DateTime? MaxDaoKuanDate = null;
                var ojb = dt.Tables[0].Rows[0]["MaxDaoKuanDate"];
                if (ojb != null && ojb != DBNull.Value)
                {

                    lblDaoKuanDate.Text = string.Format("{0:yyyy-MM-dd}", ojb);
                    MaxDaoKuanDate = Convert.ToDateTime(ojb);
                }
                lblDaoKuanTotal.Text = "";
                ojb = dt.Tables[0].Rows[0]["InvoTotal"];
                if (ojb != null && ojb != DBNull.Value)
                {
                    lblDaoKuanTotal.Text = ojb.ToString();
                }
                ojb = dt.Tables[1].Rows[0]["MinOutDate"];
                if (MaxDaoKuanDate.HasValue && ojb != null && ojb != DBNull.Value)
                {
                    TimeSpan ts = MaxDaoKuanDate.Value - Convert.ToDateTime(ojb);
                    lblTrueDate.Text = (ts.Days+1).ToString();
                }
                else
                {
                    lblTrueDate.Text = "";
                }

                ShowPOInfo(m[0]);

                GetTotal(m[0], m[1],m[2],m[3]);
            }
        }


        CG_POOrderService orderSer = new CG_POOrderService();
        private void GetTotal(string poNo, string m, string TrueLiRun, string allItemTotal)
        {
            var list = orderSer.GetOrder_ToInvoice_1(string.Format(" PONo='{0}'", poNo));
            decimal total = 0;
            if (list.Count > 0)
            {
                total = list[0].POTotal - list[0].TuiTotal;
                lblCurrentPOTotal.Text = total.ToString();

            }
            if (string.IsNullOrEmpty(lblDaoKuanTotal.Text))
            {
                lblDaoKuanTotal.Text = "0";
            }
            var DaoKuanTotal = Convert.ToDecimal(lblDaoKuanTotal.Text);
            //项目未到到款金额：XX=项目总金额-到款总额    
            lblPoWeiDaoTotal.Text = (total - DaoKuanTotal).ToString();
            //已开票未到款：YY=发票总额-到款总额   
            lblInvoiceNoDao.Text = (Convert.ToDecimal(m) - DaoKuanTotal).ToString();
            //未开票金额：ZZZ=项目总金额-发票总金额
            lblWeiKaiPiao.Text = (total - Convert.ToDecimal(m)).ToString();
            //扣管理费后利润率 PP=（实际利润-管理费总额）/项目总金额 以百分数表示。
            if (total != 0)
            {
                lblPP.Text = string.Format("{0:f2}", (((Convert.ToDecimal(TrueLiRun) - Convert.ToDecimal(allItemTotal)) / total))*100)+"%";
            }
        }



        private void setValue(Label control, string value)
        {
            control.Text = value;
        }

        protected void gvExvel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[3].Attributes.Add("style", "vnd.ms-excel.numberformat:yyyy-MM-dd");
                e.Row.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat:yyyy-MM-dd");
            }
        }


        protected void btnExcel_Click(object sender, EventArgs e)
        {

            if (lblVisAllPONO.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('无信息可以导出！');</script>");

                return;
            }
            var strSql = new StringBuilder();
            strSql.Append("select JXC_REPORT.PONo as '项目编号',CG_POOrder.POName as '项目名称',JXC_REPORT.ProNo as '单号',PODate as '项目日期',Supplier as '客户名称',RuTime as '出库日期',");
            strSql.Append(" goodInfo as '销售内容',GoodNum as '数量',GoodSellPrice as '出货单价',goodSellTotal as '销售额',GoodPrice as '单价成本',");
            strSql.Append(" goodTotal as '总成本',t_GoodNums as '成本确认价',t_GoodTotalChas as '损失差额',maoli as '毛利润额',FPTotal as '发票号码' ");
            strSql.Append(" FROM JXC_REPORT ");
            strSql.AppendFormat(" left join CG_POOrder on JXC_REPORT.PONo=CG_POOrder.PONo and CG_POOrder.IFZhui=0 where JXC_REPORT.PONo in ({0}) order by JXC_REPORT.PONo ", lblVisAllPONO.Text);



            System.Data.DataTable dt = DBHelp.getDataTable(strSql.ToString());
            GridView gvOrders = new GridView();
            gvOrders.RowDataBound += gvExvel_RowDataBound;
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=SellInfo.xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");  //设置输出流为简体中文
            this.EnableViewState = false;
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            gvOrders.AllowPaging = false;
            gvOrders.AllowSorting = false;
            gvOrders.DataSource = dt;
            gvOrders.DataBind();
            gvOrders.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            Panel1.Enabled = !CheckBox7.Checked;

            CheckBox8.Checked = CheckBox7.Checked;
            CheckBox9.Checked = CheckBox7.Checked;
            CheckBox10.Checked = CheckBox7.Checked;
            CheckBox11.Checked = CheckBox7.Checked;
            CheckBox12.Checked = CheckBox7.Checked;
            CheckBox13.Checked = CheckBox7.Checked;
            CheckBox14.Checked = CheckBox7.Checked;
            CheckBox15.Checked = CheckBox7.Checked;
            CheckBox16.Checked = CheckBox7.Checked;
            CheckBox17.Checked = CheckBox7.Checked;


        }

        private DataTable getDT(string pono, string poType)
        {
            string sql = string.Format(@"select tb.*,AE,poName from (
--.管理费 
SELECT 19 as proId, CG_POOrder.id as allE_id,ProNo,loginName,createTime,'管理费' as poType,pono,isnull(OtherCost,0) as total FROM CG_POOrder 
LEFT JOIN CG_POOrders on CG_POOrder.id=CG_POOrders.Id 
left join tb_EForm on CG_POOrder.id=tb_EForm.allE_id and proId=19
left join tb_User on tb_User.id=tb_EForm.createPer
where status='通过' and pono='{0}' and OtherCost is not null and OtherCost<>0

union all
--非材料报销（邮寄费） 
select 12 as proId, Tb_DispatchList.id as allE_id,CardNo AS ProNo,loginName,createTime, '非材料报销（邮寄费）' as poType,pono,isnull(PostTotal,0)  as Total 
from Tb_DispatchList
left join tb_User on tb_User.id=Tb_DispatchList.userId 
 where  pono='{0}' and state='通过' and  PostTotal is not null

union all
--非材料报销（除邮寄费） 
select 12 as proId, Tb_DispatchList.id as allE_id,CardNo AS ProNo,loginName,createTime,'非材料报销（非邮寄费）' as poType, PONo,
isnull(BusTotal,0)+isnull(RepastTotal,0)+isnull(HotelTotal,0)+isnull(OilTotal,0)+isnull(GuoTotal,0) 
 +isnull(OtherTotal,0)  as Total 
from Tb_DispatchList left join tb_User on tb_User.id=Tb_DispatchList.userId  where pono='{0}'  and state='通过' and POTOTAL is  null 
union all
--公交车费
select -1 as proId, 0 as allE_id,'' as ProNo,loginName,createTime,'公交车费' as poType,PONo,useTotal as Total
from TB_BusCardUse left join tb_User on tb_User.id=TB_BusCardUse.createUserId   where pono='{0}'  
 
union all 

--私车油耗费(state)
select 8 as proId, tb_UseCar.id as allE_id,ProNo,loginName,dateTime as createTime,'私车油耗费' as poType,PONo, OilPrice*roadlong as Total
from tb_UseCar left join tb_User on tb_User.id=tb_UseCar.appName     where pono='{0}' and state='通过' 
union all 

--用车申请油耗费(state)
select 5 as proId, TB_UseCarDetail.id as allE_id,ProNo,loginName,appTime,'用车申请油耗费' as poType,PONo, OilPrice*roadlong as Total
from TB_UseCarDetail  left join tb_User on tb_User.id=TB_UseCarDetail.appUser  where pono='{0}' and state='通过' 
union all 
-- 行政采购金额(state)
select 9 as proId, tb_FundsUse.id as allE_id,ProNo,loginName,dateTiem as createTime,'行政采购金额' as poType,pono, XingCaiTotal as Total
from tb_FundsUse left join tb_User on tb_User.id=tb_FundsUse.appUserId where pono='{0}' and XingCaiTotal is not null and state='通过' 

 union all
 
--会务费用(state)
select  9 as proId, tb_FundsUse.id as allE_id,ProNo,loginName,dateTiem as createTime,'会务费' as poType,pono,HuiTotal as Total
from tb_FundsUse left join tb_User on tb_User.id=tb_FundsUse.appUserId  where pono='{0}' and HuiTotal is not null and state='通过'  

union all

--人工费(state) 
select  9 as proId, tb_FundsUse.id as allE_id,ProNo,loginName,dateTiem as createTime,'人工费' as poType,pono, RenTotal as Total
from tb_FundsUse left join tb_User on tb_User.id=tb_FundsUse.appUserId  where pono='{0}' and RenTotal is not null and state='通过'  

union all
--加班单
select  10 as proId, tb_OverTime.id as allE_id,ProNo,loginName,time as createTime,'加班单' as poType,PONo,Total
from tb_OverTime left join tb_User on tb_User.id=tb_OverTime.appUseId where pono='{0}' and state='通过'
) as tb left join CG_POOrder on CG_POOrder.pono=tb.pono and ifzhui=0 and Status='通过' WHERE tb.poType='{1}' ", pono, poType);

            if (poType == "管理费")
            {
                sql += string.Format(@" union all  select 25 as proId, Sell_OrderInHouse.Id as allE_id,ProNo,loginName,createTime,'管理费' as poType,Sell_OrderInHouse.PONo,-GoodNum*AVGOtherCost as total 
,AE,poName from Sell_OrderInHouse left join Sell_OrderInHouses 
on Sell_OrderInHouse.Id=Sell_OrderInHouses.id 
LEFT JOIN (
select PONo,AE, GoodId,SUM(OtherCost)/SUM(Num) AS AVGOtherCost from CG_POOrder 
left join CG_POOrders on CG_POOrder.Id=CG_POOrders.Id where Status='通过' and pono='{0}'
group by PONo,AE, GoodId
) AS TB1 on Sell_OrderInHouse.PONo=TB1.pono and Sell_OrderInHouses.GooId=TB1.GoodId  
left join tb_User on tb_User.id=Sell_OrderInHouse.CreateUserId 

where Status='通过' and AVGOtherCost>0 and Sell_OrderInHouse.pono='{0}'", pono);
            }
            return DBHelp.getDataTable(sql);
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                JXC_REPORTTotal m = e.Row.DataItem as JXC_REPORTTotal;
                if (m!=null&&m.IsQuanDao)
                {
                    var lblDays = e.Row.FindControl("lblDays") as Label;
                    lblDays.Font.Underline = true;
                }
            }
        }


        private void ShowPOInfo(string PONo)
        {
            var sql = string.Format(" and CG_POOrder.PONo='{0}'", PONo);
            List<JXC_REPORTTotal> pOOrderList = this.POSer.GetListArray_Items_Total(sql, "", "");
            decimal HuiWuTotal = 0;
            Hashtable hsPONo = new Hashtable();
            decimal allTotal = 0;
            decimal maoliTotal = 0;
            decimal goodSellTotal = 0;
            decimal TrueLiRun = 0;
            decimal SellFPTotal = 0;

            lblItemTotal.Text = pOOrderList.Sum(t => t.itemTotal).ToString();
            foreach (var m in pOOrderList)
            {
                if (!hsPONo.Contains(m.PONo))
                {
                    HuiWuTotal += m.HuiWuTotal;
                    allTotal += m.allItemTotal;
                    maoliTotal += m.maoliTotal;
                    goodSellTotal += m.goodSellTotal;
                    TrueLiRun += m.TrueLiRun;
                    SellFPTotal += m.SellFPTotal;
                    hsPONo.Add(m.PONo, null);
                }
            }

            lblJLR.Text = string.Format("{0:n2}", maoliTotal);
            lblXSE.Text = string.Format("{0:n2}", goodSellTotal);
            lblSJLR.Text = string.Format("{0:n2}", TrueLiRun);
            lblFP.Text = string.Format("{0:n2}", SellFPTotal);

            lblHuiWuTotal.Text = string.Format("{0:n2}", HuiWuTotal);
            lblAllItemTotal.Text = string.Format("{0:n2}", allTotal);


        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                tb_EFormService eformSer = new tb_EFormService();

                var param= e.CommandArgument.ToString().Split(',');
                var id = DBHelp.ExeScalar(string.Format("select top 1 id from tb_EForm where proId={0} and allE_id={1}", param[0], param[1])).ToString();
                Session["backurl"] = "/EFrom/MyEForms.aspx";

                tb_EForm eform = eformSer.GetModel(Convert.ToInt32(id));

                string type = eform.ProTyleName.ToString();
                string url = eformSer.getUrl(param[0], param[1], id, type);
                if (url != "")
                {                    
                    Response.Write("<script language='javascript'>window.open('"+url+"','_blank');</script>");                  
                }
            }
        }
    }
}
