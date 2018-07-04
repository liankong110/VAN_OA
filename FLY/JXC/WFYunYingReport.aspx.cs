using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class WFYunYingReport : BasePage
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
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and Model='{0}'", ddlModel.Text);
            }
            if (ddlSpecial.Text != "-1")
            {
                sql += string.Format(" and IsSpecial=" + ddlSpecial.Text);
            }
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


            if (txtPOTotal.Text != "")
            {
                if (CommHelp.VerifesToNum(txtPOTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('差额 格式错误！');</script>");
                    return;
                }
            }

            List<CashFlowReport> cashFlowList = cashFlowService.GetYunYingListReport(sql, (ddlJinWei.Text=="1"));
            if (txtPOTotal.Text != "")
            {
                decimal diffTotal = Convert.ToDecimal(txtPOTotal.Text);
                if (ddlPrice.Text == ">=")
                {
                    cashFlowList = cashFlowList.FindAll(t=>t.DiffTotal>= diffTotal);
                }
                else if (ddlPrice.Text == "<")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.DiffTotal < diffTotal);
                }
                else if (ddlPrice.Text == ">")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.DiffTotal > diffTotal);
                }
                else if (ddlPrice.Text == "=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.DiffTotal == diffTotal);
                }
                else if (ddlPrice.Text == "<=")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.DiffTotal <= diffTotal);
                }
                else if (ddlPrice.Text == "<>")
                {
                    cashFlowList = cashFlowList.FindAll(t => t.DiffTotal != diffTotal);
                }

            }
            //if (ddlJinWei.Text == "1")
            //{
            //    cashFlowList = cashFlowList.FindAll(t => t.IsZhuan == true);
            //}
           
            CashFlowReport sumCashFlow = new CashFlowReport();
            sumCashFlow.POTotal = cashFlowList.Sum(t => t.POTotal);
            sumCashFlow.NotRuTotal = cashFlowList.Sum(t => t.NotRuTotal);
            sumCashFlow.NotRuSellTotal = cashFlowList.Sum(t => t.NotRuSellTotal);
            sumCashFlow.SellOutTotal = cashFlowList.Sum(t => t.SellOutTotal);
            sumCashFlow.LastCaiTotal = cashFlowList.Sum(t => t.LastCaiTotal);
            sumCashFlow.SupplierTotal = cashFlowList.Sum(t => t.SupplierTotal);
            sumCashFlow.FPTotal = cashFlowList.Sum(t => t.FPTotal);
            sumCashFlow.InvoiceTotal = cashFlowList.Sum(t => t.InvoiceTotal);
            sumCashFlow.Profit = cashFlowList.Sum(t => t.Profit);
            sumCashFlow.GoodSellPriceTotal = cashFlowList.Sum(t => t.GoodSellPriceTotal);
            sumCashFlow.TrueNotKuCunTotal = cashFlowList.Sum(t => t.TrueNotKuCunTotal);
            sumCashFlow.YingFuKuCun = cashFlowList.Sum(t => t.YingFuKuCun);
            sumCashFlow.FaxPoTotal = cashFlowList.Sum(t => t.FaxPoTotal);
            sumCashFlow.LastSupplierTotal = cashFlowList.Sum(t => t.LastSupplierTotal);
            lblYuQiYingShouTotal.Text = string.Format("{0:n6}", sumCashFlow.YuQiYingShou);
            lblYingFuKuCunTotal.Text = string.Format("{0:n6}", sumCashFlow.YingFuKuCun);
            lblLastSupplierTotal.Text = string.Format("{0:n6}", sumCashFlow.LastSupplierTotal);

            lblAllPoTotal.Text = string.Format("{0:n6}", sumCashFlow.POTotal);
            lblFPTotal.Text = string.Format("{0:n6}", sumCashFlow.FPTotal);
            lblNoFpTotal.Text = string.Format("{0:n6}", sumCashFlow.NoFpTotal);
            lblInvoiceTotal.Text = string.Format("{0:n6}", sumCashFlow.InvoiceTotal);
            lblYingShouTotal.Text = string.Format("{0:n6}", sumCashFlow.YingShouTotal);

            lblLastCaiTotal.Text = string.Format("{0:n6}", sumCashFlow.LastCaiTotal);
            lblSupplierTotal.Text = string.Format("{0:n6}", sumCashFlow.SupplierTotal);
            lblYingFuTotal.Text = string.Format("{0:n6}", sumCashFlow.YingFuTotal);

            //计算库存总额
            var kucunTotal = Convert.ToDecimal(DBHelp.ExeScalar("select isnull(sum(GoodNum*GoodAvgPrice),0) from TB_HouseGoods"));
            lblInvoiceTotal.Text = string.Format("{0:n6}", sumCashFlow.InvoiceTotal);

            var kcXuJianTotal = cashFlowService.GetKCXuJianTotal();
            lblKCXuJianTotal.Text = string.Format("{0:n6}", kcXuJianTotal);

            lblKuCunTotal.Text = string.Format("{0:n6}", kucunTotal);

            var KCWeiZhiFuTotal = cashFlowService.GetNoInvoiceTotal();//库存未支付合计
            lblKCWeiZhiFuTotal.Text = string.Format("{0:n6}", KCWeiZhiFuTotal);

            //运营总盘子=库存总金额+项目应收合计-项目应付合计+预付未到库合计+ KC预付未到库合计-库存未支付合计
            
            lblSellTotal.Text = string.Format("{0:n6}", sumCashFlow.GoodSellPriceTotal);

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
                CashFlowReport model = e.Row.DataItem as CashFlowReport;
                if (model.DiffTotal != 0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                }
                if (model.IsZhuan)
                {
                    var lastSupplierTotal=e.Row.FindControl("lblLastSupplierTotal") as Label;
                    lastSupplierTotal.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                TB_ModelService modelService = new TB_ModelService();
                var _modelList = modelService.GetListArray("");
                _modelList.Insert(0, new TB_Model { Id = -1, ModelName = "全部" });
                ddlModel.DataSource = _modelList;
                ddlModel.DataBind();
                ddlModel.DataTextField = "ModelName";
                ddlModel.DataValueField = "ModelName";

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

                List<CashFlowReport> poseModels = new List<CashFlowReport>();
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
    }
}
