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
using System.Data;
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Dal.BaseInfo;


namespace VAN_OA.JXC
{
    public partial class WFAllSellTotal : System.Web.UI.Page
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
                    m.ComCode += "," + m.ComSimpName;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComCode = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();
                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                user = userSer.getAllUserByLoginName("");
                user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });

                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";
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
            
            lblSimpName.Text = allSimpNames.Trim(',');
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
 and CG_POOrder.PODate<='{0} 23:59:59' and CG_POOrder.PODate>='{2} 00:00:00' {1} and 
 EXISTS (select ID from CG_POOrder where
PONO=JXC_REPORT.PONO  
and IsSpecial=0  and IsClose=0) GROUP BY  CG_POOrder.PONo,AE ) as allNewTb 
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
and IsSpecial=0  and IsClose=0) GROUP BY  CG_POOrder.PONo,AE ) as allNewTb 
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
 and CG_POOrder.PODate<='{0} 23:59:59' and CG_POOrder.PODate>='{2} 00:00:00' {1}  and GuestType='政府部门' and
 EXISTS (select ID from CG_POOrder where --AppName=9 AND 
PONO=JXC_REPORT.PONO  
and IsSpecial=0  and IsClose=0) GROUP BY  CG_POOrder.PONo,AE ) as allNewTb 
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
and IsSpecial=0  and IsClose=0 and IsSelected=1) GROUP BY  CG_POOrder.PONo,AE) as allNewTb 
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
 and CG_POOrder.PODate<='{0} 23:59:59' and CG_POOrder.PODate>='{2} 00:00:00' {1}  and GuestType='政府部门' and
 EXISTS (select ID from CG_POOrder where --AppName=9 AND 
PONO=JXC_REPORT.PONO  
and IsSpecial=0  and IsClose=0 and IsSelected=1) GROUP BY  CG_POOrder.PONo,AE) as allNewTb 
group by AE;
", txtTo.Text, aeSql, txtFrom.Text));


            DataTable dt1 = allDs.Tables[0];
            allList = new List<AllSellTotalModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                allList.Add(new AllSellTotalModel { AE = dr[0].ToString(), PoTotal = Convert.ToDecimal(dr[1]), PoLiRunTotal = Convert.ToDecimal(dr[2]) });
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
            if (txtFrom.Text == "" || txtTo.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择日期！');</script>");
                return;
            }
            try
            {
                if (Convert.ToDateTime(txtFrom.Text) > Convert.ToDateTime(txtTo.Text))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期错误，请重新填写！');</script>");
                    return;
                }
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期错误，请重新填写！');</script>");
                return;
            }

            show();
        }
        public string xlfile = "Sell.xls";
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (txtFrom.Text == "" || txtTo.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择日期！');</script>");
                return;
            }
            try
            {
                if (Convert.ToDateTime(txtFrom.Text) > Convert.ToDateTime(txtTo.Text))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期错误，请重新填写！');</script>");
                    return;
                }
            }
            catch (Exception)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期错误，请重新填写！');</script>");
                return;
            }


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
            user = userSer.getAllUserByLoginName(where);
            user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });

            ddlUser.DataSource = user;
            ddlUser.DataBind();
            ddlUser.DataTextField = "LoginName";
            ddlUser.DataValueField = "Id";
        }
    }
}
