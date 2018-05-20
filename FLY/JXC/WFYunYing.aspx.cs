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
using VAN_OA.Dal.ReportForms;
using System.Collections.Generic;
using VAN_OA.Model.ReportForms;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using System.Data.SqlClient;
using VAN_OA.JXC;
using VAN_OA.Model;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class WFYunYing : BasePage
    {


        private CashFlowService cashFlowService = new CashFlowService();

        private void Show()
        {
            if (!string.IsNullOrEmpty(txtDaoKLvFrom.Text))
            {
                if (CommHelp.VerifesToNum(txtDaoKLvFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款率 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtDaoKLvTo.Text))
            {
                if (CommHelp.VerifesToNum(txtDaoKLvTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款率 格式错误！');</script>");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtYLFrom.Text))
            {
                if (CommHelp.VerifesToNum(txtYLFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('盈利能力 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtYLTo.Text))
            {
                if (CommHelp.VerifesToNum(txtYLTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('盈利能力 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtJCGTotal.Text))
            {
                if (CommHelp.VerifesToNum(txtJCGTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('净采购总额 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtJZFTotal.Text))
            {
                if (CommHelp.VerifesToNum(txtJZFTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('净支付总额 格式错误！');</script>");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtYFTotal.Text))
            {
                if (CommHelp.VerifesToNum(txtYFTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('应付总额 格式错误！');</script>");
                    return;
                }
            }

            if (!string.IsNullOrEmpty(txtYFKCTotal.Text))
            {
                if (CommHelp.VerifesToNum(txtYFKCTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('应付库存 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtYFWDKTotal.Text))
            {
                if (CommHelp.VerifesToNum(txtYFWDKTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('预付未到库 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtXMJLTotal.Text))
            {
                if (CommHelp.VerifesToNum(txtXMJLTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目净利 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtSJLRTotal.Text))
            {
                if (CommHelp.VerifesToNum(txtSJLRTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('实际利润 格式错误！');</script>");
                    return;
                }
            }
            string sql = "";

            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and exists (select id from tb_User where {0} and AE=loginName)", where);
            }

            if (txtPoDateFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtPoDateFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and PoDate>='{0} 00:00:00'", txtPoDateFrom.Text);
            }

            if (txtPoDateTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtPoDateTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and PoDate<='{0} 23:59:59'", txtPoDateTo.Text);
            }

            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }

            if (txtPONO.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONO.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and CG_POOrder.PoNo like '%{0}%'", txtPONO.Text.Trim());
            }

            if (txtPOName.Text.Trim() != "")
            {
                sql += string.Format(" and PoName like '%{0}%'", txtPOName.Text.Trim());
            }

            if (ddlFPState.Text == "1")//已开全票
            {
                sql += " and POStatue3='已开票' ";
            }
            if (ddlPOTyle.Text != "-1")
            {
                sql += " and CG_POOrder.POType=" + ddlPOTyle.Text;
            }
            else if (ddlFPState.Text == "2")//未开全票
            {
                sql += " and exists(select PONO from POTotal_View where CG_POOrder.PONO=POTotal_View.PONO AND POTotal>hadFpTotal)";

            }
            else if (ddlFPState.Text == "3")//未开票 (暂缓)
            {
                sql += " and (exists(select PONO from POTotal_View where CG_POOrder.PONO=POTotal_View.PONO AND ISNULL(hadFpTotal,0)=0 ) or CG_POOrder.Status='执行中')";
            }
            if (ddlGuestTypeList.SelectedValue != "全部")
            {
                sql += string.Format(" and GuestType='{0}'", ddlGuestTypeList.SelectedValue);
            }
            if (ddlGuestProList.SelectedValue != "-2")
            {
                sql += string.Format(" and GuestPro={0}", ddlGuestProList.SelectedValue);
            }

            if (ddlNoSpecial.Text != "-1")
            {
                sql += string.Format(" and IsSpecial=" + ddlNoSpecial.Text);
            }
            if (ddlIsPoFax.Text != "-1")
            {
                sql += string.Format(" and IsPoFax=" + ddlIsPoFax.Text);
            }
            if (cbHadJiaoFu.Checked)
            {
                sql += " and POStatue2='已交付' ";
            }

            //if (ddlFPState.Text == "2")//未开全票
            //{
            //    sql += " and (SellFPTotal<>0 and ALLPOTotal-isnull(TuiTotal,0)>SellFPTotal)  ";

            //}
            //if (ddlFPState.Text == "3")//未开票
            //{
            //    sql += " and SellFPTotal is null ";
            //}

            
            if (ddlPoClose.Text != "-1")
            {
                sql += " and IsClose=" + ddlPoClose.Text;
            }

            if (ddlIsSelect.Text != "-1")
            {
                sql += " and IsSelected=" + ddlIsSelect.Text;
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                sql += " and JieIsSelected=" + ddlJieIsSelected.Text;
            }
            if (ddlShui.Text != "-1")
            {
                sql += " and IsPoFax=" + ddlShui.Text;
            }
            if (ddlUser.Text == "-1")//显示所有用户
            {

            }
            else
            {
                if (ViewState["showAll"] != null)
                {
                    sql += string.Format(" and  AE='{0}'", ddlUser.SelectedItem.Text);
                }
                else
                {
                    sql += string.Format(" and  AE='{0}'", ddlUser.SelectedItem.Text);

                }
            }

         
            if (txtFPNo.Text.Trim() != "")
            {
                sql += string.Format(" and FPTOTAL like '%{0}%'", txtFPNo.Text.Trim());
            }


            if (txtPOTotal.Text != "")
            {
                try
                {
                    if (Convert.ToDecimal(txtPOTotal.Text) <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目金额必须大于0！');</script>");

                        return;
                    }
                    sql += string.Format(" and ALLPOTotal-isnull(TuiTotal,0) {0} {1}", ddlPrice.Text, txtPOTotal.Text);

                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目金额格式有误！');</script>");
                    return;
                }
            }


            if (txtDaoKuanTotal.Text != "")
            {
                try
                {
                    if (Convert.ToDecimal(txtDaoKuanTotal.Text) <= 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款金额必须大于0！');</script>");

                        return;
                    }

                    sql += string.Format(" and isnull(InvoiceTotal,0) {0} {1}", ddlDaoKuanTotal.Text, txtDaoKuanTotal.Text);

                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款金额格式有误！');</script>");
                    return;
                }
            }

            //if (txtKuCunWeiTotal.Text != "")
            //{
            //    try
            //    {
            //        if (Convert.ToDecimal(txtKuCunWeiTotal.Text) <= 0)
            //        {
            //            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('库存未付必须大于0！');</script>");

            //            return;
            //        }

            //        sql += string.Format(" and isnull(NoInvoice,0) {0} {1}", ddlKuCunWeiTotal.Text, txtKuCunWeiTotal.Text);

            //    }
            //    catch (Exception)
            //    {

            //        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('库存未付格式有误！');</script>");
            //        return;
            //    }
            //}

            if (ddlJinECha.Text != "-1")
            {
                sql += " and ALLPOTotal-isnull(TuiTotal,0)" + ddlJinECha.Text + "ISNULL(InvoiceTotal,0) ";
            }
            if (ddlDiffDays.Enabled && ddlDiffDays.Text != "-1" && ddlJinECha.Text == "<")
            {
                if (ddlDiffDays.Text == "1")
                {
                    sql += " and datediff(d,minOutTime,getdate())<=30 ";
                }
                else if (ddlDiffDays.Text == "2")
                {
                    sql += " and datediff(d,minOutTime,getdate())>30 and datediff(d,CG_POOrder.PODate,getdate())<=60";
                }
                else if (ddlDiffDays.Text == "3")
                {
                    sql += " and datediff(d,minOutTime,getdate())>60 and datediff(d,CG_POOrder.PODate,getdate())<=90";
                }
                else if (ddlDiffDays.Text == "4")
                {
                    sql += " and datediff(d,minOutTime,getdate())>90 and datediff(d,CG_POOrder.PODate,getdate())<=120";
                }
                else if (ddlDiffDays.Text == "5")
                {
                    sql += " and datediff(d,minOutTime,getdate())>90 ";
                }
                else if (ddlDiffDays.Text == "6")
                {
                    sql += " and datediff(d,minOutTime,getdate())>120 ";
                }
                else if (ddlDiffDays.Text == "7")
                {
                    sql += " and datediff(d,minOutTime,getdate())>180 ";
                }

            }
            if (ddlDiffDays.Enabled && ddlDiffDays.Text != "-1" && ddlJinECha.Text == ">=")
            {
                if (ddlDiffDays.Text == "1")
                {
                    sql += " and datediff(d,minOutTime,MaxDaoKuanDate)<=30 ";
                }
                else if (ddlDiffDays.Text == "2")
                {
                    sql += " and datediff(d,minOutTime,MaxDaoKuanDate)>30 and datediff(d,CG_POOrder.PODate,getdate())<=60";
                }
                else if (ddlDiffDays.Text == "3")
                {
                    sql += " and datediff(d,minOutTime,MaxDaoKuanDate)>60 and datediff(d,CG_POOrder.PODate,getdate())<=90";
                }
                else if (ddlDiffDays.Text == "4")
                {
                    sql += " and datediff(d,minOutTime,MaxDaoKuanDate)>90 and datediff(d,CG_POOrder.PODate,getdate())<=120";
                }
                else if (ddlDiffDays.Text == "5")
                {
                    sql += " and datediff(d,minOutTime,MaxDaoKuanDate)>90 ";
                }
                else if (ddlDiffDays.Text == "6")
                {
                    sql += " and datediff(d,minOutTime,MaxDaoKuanDate)>120 ";
                }
                else if (ddlDiffDays.Text == "7")
                {
                    sql += " and datediff(d,minOutTime,MaxDaoKuanDate)>180 ";
                }

            }

            if (cbPoNoZero.Checked)
            {
                sql += string.Format(" and ALLPOTotal-isnull(TuiTotal,0)<>0");              
            }
            if (dllCompareSell.Text != "-1")
            {
                sql += string.Format(" and ALLPOTotal-isnull(TuiTotal,0){0}isnull(GoodSellPriceTotal,0)-isnull(TuiTotal,0)", dllCompareSell.Text);    
            }
            if (dllCompareFP.Text != "-1")
            {
                sql += string.Format(" and ALLPOTotal-isnull(TuiTotal,0){0}isnull(SellFPTotal,0)", dllCompareFP.Text);
            }
            if (dllCompareInvoice.Text != "-1")
            {
                sql += string.Format(" and ALLPOTotal-isnull(TuiTotal,0){0}isnull(InvoiceTotal,0)", dllCompareInvoice.Text);
            }
          
            if (txtDaoKLvFrom.Text != "0" && !string.IsNullOrEmpty(txtDaoKLvFrom.Text))
            {
                sql += string.Format(" and (ALLPOTotal-isnull(TuiTotal,0)<>0 and isnull(InvoiceTotal,0)/(ALLPOTotal-isnull(TuiTotal,0))*100>={0})", txtDaoKLvFrom.Text);
            }

            if (txtDaoKLvTo.Text != "1" && !string.IsNullOrEmpty(txtDaoKLvTo.Text) && txtDaoKLvFrom.Text!="0")
            {
                sql += string.Format(" and (ALLPOTotal-isnull(TuiTotal,0)<>0 and isnull(InvoiceTotal,0)/(ALLPOTotal-isnull(TuiTotal,0))*100<={0})", txtDaoKLvTo.Text);
            }
            if (txtDaoKLvTo.Text != "1" && !string.IsNullOrEmpty(txtDaoKLvTo.Text) && txtDaoKLvFrom.Text == "0")
            {
                sql += string.Format(" and (ALLPOTotal-isnull(TuiTotal,0)=0 or (ALLPOTotal-isnull(TuiTotal,0)<>0 and isnull(InvoiceTotal,0)/(ALLPOTotal-isnull(TuiTotal,0))*100<={0}))", txtDaoKLvTo.Text);
            }

          

            List<CashFlow> cashFlowList = cashFlowService.GetYunYingList(sql);

            if (txtYLFrom.Text != "")
            {
                cashFlowList = cashFlowList.FindAll(t => t.YLNL >= Convert.ToDecimal(txtYLFrom.Text));
            }
            if (txtYLTo.Text != "")
            {
                cashFlowList = cashFlowList.FindAll(t => t.YLNL <= Convert.ToDecimal(txtYLTo.Text));
            }

            //=======================
            //净采购总额
            if (ddlJCGTotal.Text != "-1" && !string.IsNullOrEmpty(txtJCGTotal.Text))
            {
                if (ddlJCGTotal.Text == ">")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.LastCaiTotal > Convert.ToDecimal(txtJCGTotal.Text));
                }
                else if (ddlJCGTotal.Text == ">=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.LastCaiTotal >= Convert.ToDecimal(txtJCGTotal.Text));
                }
                else if (ddlJCGTotal.Text == "<")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.LastCaiTotal < Convert.ToDecimal(txtJCGTotal.Text));
                }
                else if (ddlJCGTotal.Text == "<=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.LastCaiTotal <= Convert.ToDecimal(txtJCGTotal.Text));
                }
                else if (ddlJCGTotal.Text == "=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.LastCaiTotal == Convert.ToDecimal(txtJCGTotal.Text));
                }
            }
            //净支付总额
            if (ddlJZFTotal.Text != "-1" && !string.IsNullOrEmpty(txtJZFTotal.Text))
            {
                if (ddlJZFTotal.Text == ">")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.SupplierTotal > Convert.ToDecimal(txtJZFTotal.Text));
                }
                else if (ddlJZFTotal.Text == ">=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.SupplierTotal >= Convert.ToDecimal(txtJZFTotal.Text));
                }
                else if (ddlJZFTotal.Text == "<")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.SupplierTotal < Convert.ToDecimal(txtJZFTotal.Text));
                }
                else if (ddlJZFTotal.Text == "<=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.SupplierTotal <= Convert.ToDecimal(txtJZFTotal.Text));
                }
                else if (ddlJZFTotal.Text == "=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.SupplierTotal == Convert.ToDecimal(txtJZFTotal.Text));
                }
            }
            //应付总额
            if (dllYFTotal.Text != "-1" && !string.IsNullOrEmpty(txtYFTotal.Text))
            {
                string fuhao = dllYFTotal.Text;
                decimal value = Convert.ToDecimal(txtYFTotal.Text);
                if (fuhao == ">")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YingFuTotal > value);
                }
                else if (fuhao == ">=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YingFuTotal >= value);
                }
                else if (fuhao == "<")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YingFuTotal < value);
                }
                else if (fuhao == "<=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YingFuTotal <= value);
                }
                else if (fuhao == "=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YingFuTotal == value);
                }
            }
            //应付库存
            if (ddlYFKCTotal.Text != "-1" && !string.IsNullOrEmpty(txtYFKCTotal.Text))
            {
                string fuhao = ddlYFKCTotal.Text;
                decimal value = Convert.ToDecimal(txtYFKCTotal.Text);
                if (fuhao == ">")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YingFuKuCun > value);
                }
                else if (fuhao == ">=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YingFuKuCun >= value);
                }
                else if (fuhao == "<")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YingFuKuCun < value);
                }
                else if (fuhao == "<=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YingFuKuCun <= value);
                }
                else if (fuhao == "=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YingFuKuCun == value);
                }
            }
            //预付未到库
            if (dllYFWDKTotal.Text != "-1" && !string.IsNullOrEmpty(txtYFWDKTotal.Text))
            {
                string fuhao = dllYFWDKTotal.Text;
                decimal value = Convert.ToDecimal(txtYFWDKTotal.Text);
                if (fuhao == ">")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YuFuWeiTotal > value);
                }
                else if (fuhao == ">=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YuFuWeiTotal >= value);
                }
                else if (fuhao == "<")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YuFuWeiTotal < value);
                }
                else if (fuhao == "<=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YuFuWeiTotal <= value);
                }
                else if (fuhao == "=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.YuFuWeiTotal == value);
                }
            }
            //项目净利
            if (ddlXMJLTotal.Text != "-1" && !string.IsNullOrEmpty(txtXMJLTotal.Text))
            {
                string fuhao = ddlXMJLTotal.Text;
                decimal value = Convert.ToDecimal(txtXMJLTotal.Text);
                if (fuhao == ">")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.MaoLiTotal > value);
                }
                else if (fuhao == ">=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.MaoLiTotal >= value);
                }
                else if (fuhao == "<")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.MaoLiTotal < value);
                }
                else if (fuhao == "<=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.MaoLiTotal <= value);
                }
                else if (fuhao == "=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.MaoLiTotal == value);
                }
            }
            //实际利润
            if (ddlSJLRTotal.Text != "-1" && !string.IsNullOrEmpty(txtSJLRTotal.Text))
            {
                string fuhao = ddlSJLRTotal.Text;
                decimal value = Convert.ToDecimal(txtSJLRTotal.Text);
                if (fuhao == ">")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.LiRunTotal > value);
                }
                else if (fuhao == ">=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.LiRunTotal >= value);
                }
                else if (fuhao == "<")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.LiRunTotal < value);
                }
                else if (fuhao == "<=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.LiRunTotal <= value);
                }
                else if (fuhao == "=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.LiRunTotal == value);
                }
            }

            CashFlow sumCashFlow = new CashFlow();
            sumCashFlow.POTotal = cashFlowList.Sum(t => t.POTotal);
            sumCashFlow.NotRuTotal = cashFlowList.Sum(t => t.NotRuTotal);
            sumCashFlow.NotRuSellTotal = cashFlowList.Sum(t => t.NotRuSellTotal);
            sumCashFlow.SellOutTotal = cashFlowList.Sum(t => t.SellOutTotal);
            sumCashFlow.LastCaiTotal = cashFlowList.Sum(t => t.LastCaiTotal);
            sumCashFlow.SupplierTotal = cashFlowList.Sum(t => t.SupplierTotal);
            sumCashFlow.ItemTotal = cashFlowList.Sum(t => t.ItemTotal);
            sumCashFlow.FPTotal = cashFlowList.Sum(t => t.FPTotal);
            sumCashFlow.InvoiceTotal = cashFlowList.Sum(t => t.InvoiceTotal);
            sumCashFlow.GoodTotal = cashFlowList.Sum(t => t.GoodTotal);

            sumCashFlow.NotKuCunTotal = cashFlowList.Sum(t => t.NotKuCunTotal);
            sumCashFlow.Profit = cashFlowList.Sum(t => t.Profit);
            sumCashFlow.GoodSellPriceTotal = cashFlowList.Sum(t => t.GoodSellPriceTotal);

            sumCashFlow.TrueNotKuCunTotal = cashFlowList.Sum(t => t.TrueNotKuCunTotal);
            sumCashFlow.YingFuKuCun = cashFlowList.Sum(t => t.YingFuKuCun);
            sumCashFlow.FaxPoTotal = cashFlowList.Sum(t => t.FaxPoTotal);
            sumCashFlow.YuFuWeiTotal = cashFlowList.Sum(t => t.YuFuWeiTotal);
            sumCashFlow.LastSupplierTotal = cashFlowList.Sum(t => t.LastSupplierTotal);

            lblXuJianTotal.Text = string.Format("{0:n2}", sumCashFlow.YuFuWeiTotal);
            lblYuQiYingShouTotal.Text = string.Format("{0:n2}", sumCashFlow.YuQiYingShou);
            lblYingFuKuCunTotal.Text = string.Format("{0:n2}", sumCashFlow.YingFuKuCun);

            sumCashFlow.MaoLiTotal = cashFlowList.Sum(t => t.MaoLiTotal);



            lblLastSupplierTotal.Text = string.Format("{0:n2}", sumCashFlow.LastSupplierTotal);

            lblAllPoTotal.Text = string.Format("{0:n2}",sumCashFlow.POTotal);
            lblFPTotal.Text = string.Format("{0:n2}",sumCashFlow.FPTotal);
            lblNoFpTotal.Text = string.Format("{0:n2}",sumCashFlow.NoFpTotal);
            lblInvoiceTotal.Text = string.Format("{0:n2}",sumCashFlow.InvoiceTotal);
            lblYingShouTotal.Text = string.Format("{0:n2}",sumCashFlow.YingShouTotal);

            lblLastCaiTotal.Text = string.Format("{0:n2}",sumCashFlow.LastCaiTotal);
            lblSupplierTotal.Text = string.Format("{0:n2}",sumCashFlow.SupplierTotal);
            //lblProfit.Text = string.Format("{0:n2}",sumCashFlow.Profit);

            lblMaoLi.Text = string.Format("{0:n2}", sumCashFlow.MaoLiTotal);
            lblLiRunTotal.Text = string.Format("{0:n2}",sumCashFlow.LiRunTotal);
            lblYingFuTotal.Text = string.Format("{0:n2}",sumCashFlow.YingFuTotal);

            lblInvoiceBiLiTotal.Text =string.Format("{0:n2}", sumCashFlow.InvoiceBiLiTotal);
            lblYLNL.Text =string.Format("{0:n2}", sumCashFlow.YLNL);

            //计算库存总额
            var kucunTotal = Convert.ToDecimal(DBHelp.ExeScalar("select isnull(sum(GoodNum*GoodAvgPrice),0) from TB_HouseGoods"));
            lblInvoiceTotal.Text = string.Format("{0:n2}",sumCashFlow.InvoiceTotal);

            var kcXuJianTotal = cashFlowService.GetKCXuJianTotal();
            lblKCXuJianTotal.Text = string.Format("{0:n2}", kcXuJianTotal);


            lblKuCunTotal.Text = string.Format("{0:n2}",kucunTotal);

            var KCWeiZhiFuTotal = cashFlowService.GetNoInvoiceTotal();//库存未支付合计
            lblKCWeiZhiFuTotal.Text = string.Format("{0:n2}", KCWeiZhiFuTotal);

            //运营总盘子=库存总金额+项目应收合计-项目应付合计+预付未到库合计+ KC预付未到库合计-库存未支付合计
            lblYingYunAllTotal.Text = string.Format("{0:n2}", (kucunTotal + sumCashFlow.YingYunAllTotal + sumCashFlow.YuFuWeiTotal + kcXuJianTotal - KCWeiZhiFuTotal));

            lblSellTotal.Text = string.Format("{0:n2}", sumCashFlow.GoodSellPriceTotal);




            AspNetPager1.RecordCount = cashFlowList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = cashFlowList;
            this.gvList.DataBind();
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        protected void btnSelect2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtYQYS.Text))
            {
                if (CommHelp.VerifesToNum(txtYQYS.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('预期应收 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtYFZE.Text))
            {
                if (CommHelp.VerifesToNum(txtYFZE.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('应付总额 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtYQYS1.Text))
            {
                if (CommHelp.VerifesToNum(txtYQYS1.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('预期应收 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtYFZE1.Text))
            {
                if (CommHelp.VerifesToNum(txtYFZE1.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('应付总额 格式错误！');</script>");
                    return;
                }
            }
            List<CashFlow> cashFlowList = new List<CashFlow>();// cashFlowService.GetYunYingList(sql);
            List<CashFlow> cashFlowList2 = new List<CashFlow>();
            var sql = " and (ALLPOTotal-isnull(TuiTotal,0)=0 or (ALLPOTotal-isnull(TuiTotal,0)<>0 and isnull(InvoiceTotal,0)/(ALLPOTotal-isnull(TuiTotal,0))*100<=100)) ";
            if (cbFeiTS.Checked)
            {
                sql += string.Format(" and IsSpecial=0");
                cashFlowList = cashFlowService.GetYunYingList(sql);
                if (ddlYQYS.Text != "-1" && !string.IsNullOrEmpty(txtYQYS.Text) && txtYQYS.Text.Trim() != "0")
                {
                    var fuhao = ddlYQYS.Text;
                    var value = Convert.ToDecimal(txtYQYS.Text);
                    if (fuhao == ">")
                    {
                        cashFlowList = cashFlowList.FindAll(t => t.YuQiYingShou > value);
                    }
                    if (fuhao == ">=")
                    {
                        cashFlowList = cashFlowList.FindAll(t => t.YuQiYingShou >= value);
                    }
                    if (fuhao == "<")
                    {
                        cashFlowList = cashFlowList.FindAll(t => t.YuQiYingShou < value);
                    }
                    if (fuhao == "<=")
                    {
                        cashFlowList = cashFlowList.FindAll(t => t.YuQiYingShou <= value);
                    }
                    if (fuhao == "=")
                    {
                        cashFlowList = cashFlowList.FindAll(t => t.YuQiYingShou ==value);
                    }
                }
                if (ddlYFZE.Text != "-1" && !string.IsNullOrEmpty(txtYFZE.Text) && txtYFZE.Text.Trim() != "0")
                {
                    var fuhao = ddlYFZE.Text;
                    var value = Convert.ToDecimal(txtYFZE.Text);
                    if (fuhao == ">")
                    {
                        cashFlowList = cashFlowList.FindAll(t => t.YingFuTotal > value);
                    }
                    if (fuhao == ">=")
                    {
                        cashFlowList = cashFlowList.FindAll(t => t.YingFuTotal >= value);
                    }
                    if (fuhao == "<")
                    {
                        cashFlowList = cashFlowList.FindAll(t => t.YingFuTotal < value);
                    }
                    if (fuhao == "<=")
                    {
                        cashFlowList = cashFlowList.FindAll(t => t.YingFuTotal <= value);
                    }
                    if (fuhao == "=")
                    {
                        cashFlowList = cashFlowList.FindAll(t => t.YingFuTotal == value);
                    }
                }
            }

            sql = " and (ALLPOTotal-isnull(TuiTotal,0)=0 or (ALLPOTotal-isnull(TuiTotal,0)<>0 and isnull(InvoiceTotal,0)/(ALLPOTotal-isnull(TuiTotal,0))*100<=100)) ";
            if (cbTS.Checked)
            {
                sql = string.Format("  and IsSpecial=1");
                cashFlowList2 = cashFlowService.GetYunYingList(sql);
                if (ddlYQYS1.Text != "-1" && !string.IsNullOrEmpty(txtYQYS1.Text) && txtYQYS1.Text.Trim()!="0")
                {
                    var fuhao = ddlYQYS1.Text;
                    var value = Convert.ToDecimal(txtYQYS1.Text);
                    if (fuhao == ">")
                    {
                        cashFlowList2 = cashFlowList2.FindAll(t => t.YuQiYingShou > value);
                    }
                    if (fuhao == ">=")
                    {
                        cashFlowList2 = cashFlowList2.FindAll(t => t.YuQiYingShou >= value);
                    }
                    if (fuhao == "<")
                    {
                        cashFlowList2 = cashFlowList2.FindAll(t => t.YuQiYingShou < value);
                    }
                    if (fuhao == "<=")
                    {
                        cashFlowList2 = cashFlowList2.FindAll(t => t.YuQiYingShou <= value);
                    }
                    if (fuhao == "=")
                    {
                        cashFlowList2 = cashFlowList2.FindAll(t => t.YuQiYingShou == value);
                    }
                }
                if (ddlYFZE1.Text != "-1" && !string.IsNullOrEmpty(txtYFZE1.Text) && txtYFZE1.Text.Trim() != "0")
                {
                    var fuhao = ddlYFZE1.Text;
                    var value = Convert.ToDecimal(txtYFZE1.Text);
                    if (fuhao == ">")
                    {
                        cashFlowList2 = cashFlowList2.FindAll(t => t.YingFuTotal > value);
                    }
                    if (fuhao == ">=")
                    {
                        cashFlowList2 = cashFlowList2.FindAll(t => t.YingFuTotal >= value);
                    }
                    if (fuhao == "<")
                    {
                        cashFlowList2 = cashFlowList2.FindAll(t => t.YingFuTotal < value);
                    }
                    if (fuhao == "<=")
                    {
                        cashFlowList2 = cashFlowList2.FindAll(t => t.YingFuTotal <= value);
                    }
                    if (fuhao == "=")
                    {
                        cashFlowList2 = cashFlowList2.FindAll(t => t.YingFuTotal == value);
                    }
                }
            }
            cashFlowList.AddRange(cashFlowList2);

            //CashFlow sumCashFlow = new CashFlow();
            //sumCashFlow.POTotal = cashFlowList.Sum(t => t.POTotal);
            //sumCashFlow.NotRuTotal = cashFlowList.Sum(t => t.NotRuTotal);
            //sumCashFlow.NotRuSellTotal = cashFlowList.Sum(t => t.NotRuSellTotal);
            //sumCashFlow.SellOutTotal = cashFlowList.Sum(t => t.SellOutTotal);
            //sumCashFlow.LastCaiTotal = cashFlowList.Sum(t => t.LastCaiTotal);
            //sumCashFlow.SupplierTotal = cashFlowList.Sum(t => t.SupplierTotal);
            //sumCashFlow.ItemTotal = cashFlowList.Sum(t => t.ItemTotal);
            //sumCashFlow.FPTotal = cashFlowList.Sum(t => t.FPTotal);
            //sumCashFlow.InvoiceTotal = cashFlowList.Sum(t => t.InvoiceTotal);
            //sumCashFlow.GoodTotal = cashFlowList.Sum(t => t.GoodTotal);

            //sumCashFlow.NotKuCunTotal = cashFlowList.Sum(t => t.NotKuCunTotal);
            //sumCashFlow.Profit = cashFlowList.Sum(t => t.Profit);
            //sumCashFlow.GoodSellPriceTotal = cashFlowList.Sum(t => t.GoodSellPriceTotal);

            //sumCashFlow.TrueNotKuCunTotal = cashFlowList.Sum(t => t.TrueNotKuCunTotal);
            //sumCashFlow.YingFuKuCun = cashFlowList.Sum(t => t.YingFuKuCun);
            //sumCashFlow.FaxPoTotal = cashFlowList.Sum(t => t.FaxPoTotal);
            //sumCashFlow.XuJianTotal = cashFlowList.Sum(t => t.XuJianTotal);

            //sumCashFlow.MaoLiTotal = cashFlowList.Sum(t => t.MaoLiTotal);

            ////计算库存总额
            //var kucunTotal = Convert.ToDecimal(DBHelp.ExeScalar("select isnull(sum(GoodNum*GoodAvgPrice),0) from TB_HouseGoods"));
            //var kcXuJianTotal = cashFlowService.GetKCXuJianTotal();
            //var KCWeiZhiFuTotal = cashFlowService.GetKCWeiZhiFuTotal();//库存未支付合计


            CashFlow sumCashFlow = new CashFlow();
            sumCashFlow.POTotal = cashFlowList.Sum(t => t.POTotal);
            sumCashFlow.NotRuTotal = cashFlowList.Sum(t => t.NotRuTotal);
            sumCashFlow.NotRuSellTotal = cashFlowList.Sum(t => t.NotRuSellTotal);
            sumCashFlow.SellOutTotal = cashFlowList.Sum(t => t.SellOutTotal);
            sumCashFlow.LastCaiTotal = cashFlowList.Sum(t => t.LastCaiTotal);
            sumCashFlow.SupplierTotal = cashFlowList.Sum(t => t.SupplierTotal);
            sumCashFlow.ItemTotal = cashFlowList.Sum(t => t.ItemTotal);
            sumCashFlow.FPTotal = cashFlowList.Sum(t => t.FPTotal);
            sumCashFlow.InvoiceTotal = cashFlowList.Sum(t => t.InvoiceTotal);
            sumCashFlow.GoodTotal = cashFlowList.Sum(t => t.GoodTotal);

            sumCashFlow.NotKuCunTotal = cashFlowList.Sum(t => t.NotKuCunTotal);
            sumCashFlow.Profit = cashFlowList.Sum(t => t.Profit);
            sumCashFlow.GoodSellPriceTotal = cashFlowList.Sum(t => t.GoodSellPriceTotal);

            sumCashFlow.TrueNotKuCunTotal = cashFlowList.Sum(t => t.TrueNotKuCunTotal);
            sumCashFlow.YingFuKuCun = cashFlowList.Sum(t => t.YingFuKuCun);
            sumCashFlow.FaxPoTotal = cashFlowList.Sum(t => t.FaxPoTotal);
            sumCashFlow.YuFuWeiTotal = cashFlowList.Sum(t => t.YuFuWeiTotal);
            sumCashFlow.LastSupplierTotal = cashFlowList.Sum(t => t.LastSupplierTotal);

            sumCashFlow.MaoLiTotal = cashFlowList.Sum(t => t.MaoLiTotal);
           

            //计算库存总额
            var kucunTotal = Convert.ToDecimal(DBHelp.ExeScalar("select isnull(sum(GoodNum*GoodAvgPrice),0) from TB_HouseGoods"));
           

            var kcXuJianTotal = cashFlowService.GetKCXuJianTotal();
            

            var KCWeiZhiFuTotal = cashFlowService.GetNoInvoiceTotal();//库存未支付合计
           
           
            //未来体系总盘子=库存总金额+预期项目应收合计-项目应付合计+预付未到库合计+ KC预付未到库合计-库存未支付合计
            WeilblYingYunAllTotal.Text = string.Format("{0:n2}", (kucunTotal + (sumCashFlow.YuQiYingShou - sumCashFlow.YingFuTotal) + sumCashFlow.YuFuWeiTotal +kcXuJianTotal - KCWeiZhiFuTotal));

        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                //CashFlow model = e.Row.DataItem as CashFlow;
                //cashFlowService.Update(model);
            }
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

                List<TB_BasePoType> basePoTypeList = new TB_BasePoTypeService().GetListArray("");
                basePoTypeList.Insert(0, new TB_BasePoType { BasePoType = "全部", Id = -1 });
                ddlPOTyle.DataSource = basePoTypeList;
                ddlPOTyle.DataBind();
                ddlPOTyle.DataTextField = "BasePoType";
                ddlPOTyle.DataValueField = "Id";

                GuestTypeBaseInfoService dal = new GuestTypeBaseInfoService();
                var dalList = dal.GetListArray("");
                dalList.Insert(0, new VAN_OA.Model.BaseInfo.GuestTypeBaseInfo { GuestType = "全部" });
                ddlGuestTypeList.DataSource = dalList;
                ddlGuestTypeList.DataBind();
                ddlGuestTypeList.DataTextField = "GuestType";
                ddlGuestTypeList.DataValueField = "GuestType";

                GuestProBaseInfoService guestProBaseInfodal = new GuestProBaseInfoService();
                var proList = guestProBaseInfodal.GetListArray("");
                proList.Insert(0, new VAN_OA.Model.BaseInfo.GuestProBaseInfo { GuestPro = -2 });
                ddlGuestProList.DataSource = proList;
                ddlGuestProList.DataBind();
                ddlGuestProList.DataTextField = "GuestProString";
                ddlGuestProList.DataValueField = "GuestPro";

                List<CashFlow> poseModels = new List<CashFlow>();
                this.gvList.DataSource = poseModels;
                this.gvList.DataBind();

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();                
                user = userSer.getAllUserByPOList();
                user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });               

                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";
                if (Request["PONo"] != null)
                {
                    txtPONO.Text = Request["PONo"].ToString();
                    Show();
                }
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void ddlJinECha_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlJinECha.Text == "<" || ddlJinECha.Text == ">=")
            {
                ddlDiffDays.Enabled = true;
            }
            else
            {
                ddlDiffDays.Enabled = false;
            }
        }






    }
}
