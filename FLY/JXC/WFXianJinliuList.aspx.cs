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
    public partial class WFXianJinliuList : BasePage
    {


        private CashFlowService cashFlowService = new CashFlowService();

        private void Show()
        {
            string sql = "";
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
            else if (ddlFPState.Text == "2")//未开全票
            {
                sql += " and exists(select PONO from POTotal_View where CG_POOrder.PONO=POTotal_View.PONO AND POTotal>hadFpTotal)";

            }
            else if (ddlFPState.Text == "3")//未开票 (暂缓)
            {
                sql += " and (exists(select PONO from POTotal_View where CG_POOrder.PONO=POTotal_View.PONO AND ISNULL(hadFpTotal,0)=0 ) or CG_POOrder.Status='执行中')";
            }
            if (ddlPOTyle.Text != "-1")
            {
                sql += " and CG_POOrder.POType=" + ddlPOTyle.Text;
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
            //if (ddlUser.Text == "-1")//显示所有用户
            //{

            //}
            //else
            //{
            //    if (ViewState["showAll"] != null)
            //    {
            //        sql += string.Format(" and  AE='{0}'", ddlUser.SelectedItem.Text);
            //    }
            //    else
            //    {
            //        sql += string.Format(" and  AE='{0}'", ddlUser.SelectedItem.Text);

            //    }
            //}


            if (ddlUser.Text == "-1")//显示所有用户
            {
                
            }
            else if (ddlUser.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
                sql += string.Format(" and (AE in (select loginName from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}'))", model.LoginIPosition);
            }
            else
            {
                sql += string.Format(" and (AE='{0}')", ddlUser.SelectedItem.Text);
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

                    sql += string.Format(" and InvoiceTotal {0} {1}", ddlDaoKuanTotal.Text, txtDaoKuanTotal.Text);

                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款金额格式有误！');</script>");
                    return;
                }
            }

            if (ddlJinECha.Text != "-1")
            {
                sql += " and ISNULL(InvoiceTotal,0) " + ddlJinECha.Text + "ALLPOTotal-isnull(TuiTotal,0)";
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
                if (CommHelp.VerifesToNum(txtDaoKLvFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款率 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and (ALLPOTotal-isnull(TuiTotal,0)<>0 and isnull(InvoiceTotal,0)/(ALLPOTotal-isnull(TuiTotal,0))*100>={0})", txtDaoKLvFrom.Text);
            }
            if (!string.IsNullOrEmpty(txtDaoKLvTo.Text))
            {
                if (CommHelp.VerifesToNum(txtDaoKLvTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款率 格式错误！');</script>");
                    return;
                }
            }
            if (txtDaoKLvTo.Text != "1" && !string.IsNullOrEmpty(txtDaoKLvTo.Text) && txtDaoKLvFrom.Text != "0")
            {
               
                sql += string.Format(" and (ALLPOTotal-isnull(TuiTotal,0)<>0 and isnull(InvoiceTotal,0)/(ALLPOTotal-isnull(TuiTotal,0))*100<={0})", txtDaoKLvTo.Text);
            }
            if (txtDaoKLvTo.Text != "1" && !string.IsNullOrEmpty(txtDaoKLvTo.Text) && txtDaoKLvFrom.Text == "0")
            {
                sql += string.Format(" and (ALLPOTotal-isnull(TuiTotal,0)=0 or (ALLPOTotal-isnull(TuiTotal,0)<>0 and isnull(InvoiceTotal,0)/(ALLPOTotal-isnull(TuiTotal,0))*100<={0}))", txtDaoKLvTo.Text);
            }

            //if (txtDaoKLvTo.Text != "1" && !string.IsNullOrEmpty(txtDaoKLvTo.Text))
            //{
            //    sql += string.Format(" and (ALLPOTotal-isnull(TuiTotal,0)<>0 and isnull(InvoiceTotal,0)/(ALLPOTotal-isnull(TuiTotal,0))*100<={0})", txtDaoKLvTo.Text);
            //}
           

            List<CashFlow> cashFlowList = cashFlowService.GetListArray(sql);

            if (txtYLFrom.Text != "")
            {
                if (CommHelp.VerifesToNum(txtYLFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('盈利能力 格式错误！');</script>");
                    return;
                }
                cashFlowList = cashFlowList.FindAll(t => t.YLNL >= Convert.ToDecimal(txtYLFrom.Text));
            }
            if (txtYLTo.Text != "")
            {
                if (CommHelp.VerifesToNum(txtYLTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('盈利能力 格式错误！');</script>");
                    return;
                }
                cashFlowList = cashFlowList.FindAll(t => t.YLNL <= Convert.ToDecimal(txtYLTo.Text));
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
            sumCashFlow.LastSupplierTotal = cashFlowList.Sum(t => t.LastSupplierTotal);

            lblLastSupplierTotal.Text = string.Format("{0:n2}", sumCashFlow.LastSupplierTotal);

            lblAllPoTotal.Text = sumCashFlow.POTotal.ToString();
            lblNotRuTotal.Text = sumCashFlow.NotRuTotal.ToString();
            lblNotRuSellTotal.Text = sumCashFlow.NotRuSellTotal.ToString();
            lblSellOutTotal.Text = sumCashFlow.SellOutTotal.ToString();
            lblLastCaiTotal.Text =sumCashFlow.LastCaiTotal.ToString();
            lblSupplierTotal.Text = sumCashFlow.SupplierTotal.ToString();
            lblItemTotal.Text = sumCashFlow.ItemTotal.ToString();
            lblFPTotal.Text = sumCashFlow.FPTotal.ToString();
            lblInvoiceTotal.Text = sumCashFlow.InvoiceTotal.ToString();
            lblLiRunTotal.Text = sumCashFlow.LiRunTotal.ToString();
            lblNotShouTotal.Text = sumCashFlow.NotShouTotal.ToString();
            lblInvoiceBiLiTotal.Text =  string.Format("{0:f2}",sumCashFlow.InvoiceBiLiTotal);
            lblSupplierBiliTotal.Text =  string.Format("{0:f2}",sumCashFlow.SupplierBiliTotal);

            lblZJHLV.Text = string.Format("{0:f2}",sumCashFlow.ZJHLV);
            lblZJZY.Text = string.Format("{0:f2}", sumCashFlow.ZJZY);
            lblYLNL.Text = string.Format("{0:f2}", sumCashFlow.YLNL);
            lblYLLV.Text = string.Format("{0:f2}", sumCashFlow.YLLV);

            lblKuCunTotal.Text = string.Format("{0:f2}", sumCashFlow.NotKuCunTotal);
            lblZiJinTotal.Text = string.Format("{0:f2}", sumCashFlow.ZJTouRu);
            if (sumCashFlow.POTotal != 0)
            {
                lblFeiYongTotal.Text = string.Format("{0:f2}", (cashFlowList.Where(t => t.POTotal != 0).Sum(t => t.ItemTotal) / sumCashFlow.POTotal * 100));
            }
            else
            {
                lblFeiYongTotal.Text = string.Format("{0:f2}", "0");
            }
         

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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text == "-")
            {
                int i = 18;
                gvList.Columns[i++].Visible = false;
                gvList.Columns[i++].Visible = false;
                gvList.Columns[i++].Visible = false;
                gvList.Columns[i++].Visible = false;
                gvList.Columns[i++].Visible = false;
                gvList.Columns[i++].Visible = false;
                gvList.Columns[i++].Visible = false;
                gvList.Columns[i++].Visible = false;
                gvList.Columns[i++].Visible = false;
                gvList.Columns[i++].Visible = false;
                btn.Text = "+";
            }
            else
            {
                int i = 18;
                btn.Text = "-";
                gvList.Columns[i++].Visible = true;
                gvList.Columns[i++].Visible = true;
                gvList.Columns[i++].Visible = true;
                gvList.Columns[i++].Visible = true;
                gvList.Columns[i++].Visible = true;
                gvList.Columns[i++].Visible = true;
                gvList.Columns[i++].Visible = true;
                gvList.Columns[i++].Visible = true;
                gvList.Columns[i++].Visible = true;
                gvList.Columns[i++].Visible = true;
               
            }
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
            if (e.Row.RowType == DataControlRowType.Footer)
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {


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

                bool showAll = true;
                if (QuanXian_ShowAll("现金流考核") == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }
                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll("现金流考核", Session["currentUserId"], "WFScanDepartList") == true)
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
