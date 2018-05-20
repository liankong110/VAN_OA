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
using VAN_OA.Dal.JXC;
using VAN_OA.Model;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class CAI_OrderList : BasePage
    {
        CAI_POOrderService POSer = new CAI_POOrderService();
        CAI_POOrdersService ordersSer = new CAI_POOrdersService();
        CAI_POCaiService CaiSer = new CAI_POCaiService();

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
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();
                //主单
                List<CAI_POOrder> pOOrderList = new List<CAI_POOrder>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //子单
                List<CAI_POOrders> orders = new List<CAI_POOrders>();
                gvList.DataSource = orders;
                gvList.DataBind();



                List<CAI_POCai> caiList = new List<CAI_POCai>();
                gvCai.DataSource = caiList;
                gvCai.DataBind();

                //if (VAN_OA.JXC.SysObj.IfShowAll(SysObj.CAI_OrderList, Session["currentUserId"]) == false)
                //{
                //    ViewState["showAll"] = false;
                //}
                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                if (QuanXian_ShowAll("采购订单列表") == false)
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

                //                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='不能编辑'
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='采购订单列表') and sys_Object.AutoID is not null", Session["currentUserId"]);
                //                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                if (NewShowAll_textName("采购订单列表", "不能编辑")==false)
                {
                    gvMain.Columns[0].Visible = false;
                }

                //                 sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='复制'
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='采购订单列表') and sys_Object.AutoID is not null", Session["currentUserId"]);
                //                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                if (NewShowAll_textName("采购订单列表", "复制") == false)
                {
                    gvMain.Columns[2].Visible = false;
                }
                if (Session["PoNo"] != null)
                {
                    txtPONo.Text = Session["PoNo"].ToString();
                    Show();
                    Session["PoNo"] = null;
                }

            }
        }


        private void Show()
        {
            string sql = " 1=1 ";

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and PODate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and PODate<='{0} 23:59:59'", txtTo.Text);
            }
            if (!string.IsNullOrEmpty(txtAuditDate.Text))
            {
                if (CommHelp.VerifesToDateTime(txtAuditDate.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('审批日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and e_LastTime>='{0} 00:00:00'", txtAuditDate.Text);
                sql += string.Format(" and e_LastTime<='{0} 23:59:59'", txtAuditDate.Text);
            }

            if (ddlStatue.Text != "")
            {
                sql += string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }

            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and GuestName  like '%{0}%'", txtGuestName.Text.Trim());
            }


            if (ddlBusType.Text != "")
            {
                sql += string.Format(" and BusType='{0}'", ddlBusType.SelectedValue);
            }

            if (txtPoProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtPoProNo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and CG_ProNo  like '%{0}%'", txtPoProNo.Text.Trim());
            }

            if (txtCaiugou.Text != "")
            {
                sql += string.Format(" and CaiGou  like '%{0}%'", txtCaiugou.Text);
            }



            if (ddlUser.Text != "-1")
            {
                //sql += string.Format(" and (CAI_POOrder.AppName={0} or exists(select id from CG_POOrder where CG_POOrder.PONo=CAI_POOrder.PONo and AppName={0}))", ddlUser.Text);
				sql += string.Format(" and AE='{0}'", ddlUser.SelectedItem.Text);
			}

            if (txtCaiGouNo.Text.Trim() != "")
            {
                if (CheckProNo(txtCaiGouNo.Text)==false)
                {
                    return;
                }
                sql += string.Format(" and CAI_POOrder.proNo  like '%{0}%'", txtCaiGouNo.Text.Trim());
            }

            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and exists (select id from CG_POOrder where IFZhui=0 and CG_POOrder.PONo=CAI_POOrder.PONo and AppName in(select id from tb_User where {0}))", where);
            }

            if (ddlIsHanShui.Text == "1")
            {
                sql += string.Format(" and tb2.IsHanShui=tb2.allCount");
            }
            if (ddlIsHanShui.Text == "0")
            {
                sql += string.Format(" and tb2.IsHanShui<>tb2.allCount");
            }

            List<CAI_POOrder> pOOrderList = this.POSer.GetListArray(sql);
            foreach (var model in pOOrderList)
            {
                if (model.BusType == "0")
                {
                    model.BusType = "项目订单采购";
                }
                else if (model.BusType == "1")
                {
                    model.BusType = "库存采购";
                }
            }

            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

            //子单
            List<CAI_POOrders> orders = new List<CAI_POOrders>();
            gvList.DataSource = orders;
            gvList.DataBind();



            List<CAI_POCai> caiList = new List<CAI_POCai>();
            gvCai.DataSource = caiList;
            gvCai.DataBind();

        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
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
                CAI_POOrder model = e.Row.DataItem as CAI_POOrder;
                if (model.Count2 != model.Count1)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {

                List<CAI_POOrders> orders = ordersSer.GetListArray(" 1=1 and Cai_POOrders.id=" + e.CommandArgument);

                ViewState["Orders"] = orders;
                gvList.DataSource = orders;
                gvList.DataBind();



                List<CAI_POCai> caiList = CaiSer.GetListArray(" 1=1 and Cai_POCai.id=" + e.CommandArgument);
                gvCai.DataSource = caiList;
                gvCai.DataBind();
            }
            else if (e.CommandName == "Copy")//复制
            {
                //是否是此单据的申请人
                var model = POSer.GetModel(Convert.ToInt32(e.CommandArgument));
                if (model != null && model.Status == "不通过")
                {

                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据必须已经审批‘不通过’才能复制！');</script>");
                    return;
                }
                string url = "~/JXC/CAI_Order.aspx?ProId=20&Id=" + e.CommandArgument + "&&Copy=true";
                Response.Redirect(url);
            }
            else if (e.CommandName == "ReEdit")
            {
                //是否是此单据的申请人
                var model = POSer.GetModel(Convert.ToInt32(e.CommandArgument));

                if (Session["currentUserId"].ToString() != model.AppName.ToString())
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('必须由原单据申请人 重新发起，其他人不能重新提交编辑！');</script>");
                    return;
                }

                //首先单子要先通过               

                if (model != null && model.Status == "通过")
                {

                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据必须已经审批通过才能重新编辑！');</script>");
                    return;
                }

                string check = string.Format("select count(*) from CAI_OrderChecks left join CAI_OrderCheck on CAI_OrderChecks.CheckId=CAI_OrderCheck.Id where Status in ('通过','执行中') and caiProNo='{0}'  ", model.ProNo);
                if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据已经被检验单使用！');</script>");
                    return;
                }
                check = string.Format(@"select count(*) from TB_SupplierAdvancePayments left join TB_SupplierAdvancePayment on TB_SupplierAdvancePayments.Id=TB_SupplierAdvancePayment.Id
where caiIds in (select Ids from CAI_POCai where ID={0}) and Status<>'不通过'", model.Id);
                if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据已经被预付款单使用！');</script>");
                    return;
                }
                //判断当前采购信息有没有‘库存’的信息，按照 库存+项目编号+商品Id来判断 商品有没有入库 如果入库不可以编辑
                check = string.Format(@"select COUNT(*) from CAI_POOrder left join CAI_POCai on CAI_POOrder.Id=CAI_POCai.Id
where CAI_POCai.lastSupplier='库存' and GoodId in (select Sell_OrderOutHouses.GooId from Sell_OrderOutHouse left join Sell_OrderOutHouses 
on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id where Status<>'不通过' and POno='{0}')
and Status='通过' and CAI_POOrder.Id={1}", model.PONo, model.Id);
                if (Convert.ToInt32(DBHelp.ExeScalar(check)) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此订单已出库，无法重新编辑！');</script>");
                    return;
                }
                string sql = "select pro_Id from A_ProInfo where pro_Type='采购订单'";

                string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='采购订单')", e.CommandArgument);
                string url = "~/JXC/CAI_Order.aspx?ProId=" + DBHelp.ExeScalar(sql) + "&allE_id=" + e.CommandArgument + "&EForm_Id=" + DBHelp.ExeScalar(efromId) + "&&ReAudit=true";
                Response.Redirect(url);


                //没有做过检验单


            }
        }

        private void setValue(Label control, string value)
        {
            control.Text = value;
        }

        protected object ConvertToObj(object obj)
        {
            if (obj != null) return string.Format("{0:f2}", Convert.ToDecimal(obj));
            return 0;
        }

        CAI_POCai SumPOCai = new CAI_POCai();
        protected void gvCai_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                CAI_POCai model = e.Row.DataItem as CAI_POCai;

                if (model.IsHanShui == false)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
                if (model.Total1 != null)
                {
                    if (SumPOCai.Total1 == null) SumPOCai.Total1 = 0;
                    SumPOCai.Total1 += model.Total1;
                }
                if (model.Total2 != null)
                {
                    if (SumPOCai.Total2 == null) SumPOCai.Total2 = 0;
                    SumPOCai.Total2 += model.Total2;
                }
                if (model.Total3 != null)
                {
                    if (SumPOCai.Total3 == null) SumPOCai.Total3 = 0;
                    SumPOCai.Total3 += model.Total3;
                }

                if (model.Num != null)
                {
                    if (SumPOCai.Num == null) SumPOCai.Num = 0;
                    SumPOCai.Num += model.Num;
                }


            }
            Label lblPrice1 = e.Row.FindControl("lblSupperPrice") as Label;

            //Label lblFinPrice1 = e.Row.FindControl("lblFinPrice1") as Label;


            //if (lblPrice1 != null && lblFinPrice1 != null)
            //{
            //    if (lblPrice1.Text != "" && lblFinPrice1.Text != "")
            //    {
            //        if (Convert.ToDecimal(lblPrice1.Text) != Convert.ToDecimal(lblFinPrice1.Text))
            //        { 
            //            e.Row.Cells[6].ForeColor=System.Drawing.Color.Red;
            //        }
            //    }
            //}


            Label lblPrice2 = e.Row.FindControl("lblSupperPrice1") as Label;

            //Label lblFinPrice2 = e.Row.FindControl("FinPrice2") as Label;


            //if (lblPrice2 != null && lblFinPrice2 != null)
            //{
            //    if (lblPrice2.Text != "" && lblFinPrice2.Text != "")
            //    {
            //        if (Convert.ToDecimal(lblPrice2.Text) != Convert.ToDecimal(lblFinPrice2.Text))
            //        {
            //            e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
            //        }
            //    }
            //}




            Label lblPrice3 = e.Row.FindControl("lblSupperPrice2") as Label;

            //Label lblFinPrice3 = e.Row.FindControl("FinPrice3") as Label;


            //if (lblPrice3 != null && lblFinPrice3 != null)
            //{
            //    if (lblPrice3.Text != "" && lblFinPrice3.Text != "")
            //    {
            //        if (Convert.ToDecimal(lblPrice3.Text) != Convert.ToDecimal(lblFinPrice3.Text))
            //        {
            //            e.Row.Cells[14].ForeColor = System.Drawing.Color.Red;
            //        }
            //    }
            //}

            List<decimal> pricelMax = new List<decimal>();
            if (lblPrice1 != null && lblPrice1.Text != "")
            {
                pricelMax.Add(Convert.ToDecimal(lblPrice1.Text));
            }

            if (lblPrice2 != null && lblPrice2.Text != "")
            {
                pricelMax.Add(Convert.ToDecimal(lblPrice2.Text));
            }

            if (lblPrice3 != null && lblPrice3.Text != "")
            {
                pricelMax.Add(Convert.ToDecimal(lblPrice3.Text));
            }


            if (pricelMax.Count > 0)
            {
                decimal minPrice = pricelMax.Min();
                decimal lirun = 0;
                List<CAI_POOrders> POOrders = ViewState["Orders"] as List<CAI_POOrders>;

                Label lblGoodId = e.Row.FindControl("lblGoodId") as Label;

                CAI_POOrders po = null;
                if (POOrders != null && lblGoodId != null)
                {
                    po = POOrders.Find(p => p.GoodId.ToString() == lblGoodId.Text);
                    if (po != null && po.SellTotal != 0)
                    {
                        lirun = ((po.SellTotal - minPrice * po.Num - po.OtherCost) / po.SellTotal) * 100;
                    }

                    else if (po != null)
                    {
                        decimal yiLiTotal = po.SellTotal - minPrice * po.Num - po.OtherCost;

                        if (yiLiTotal != 0)
                        {
                            lirun = -100;
                        }



                    }
                }
                Label lblCaiLiRun = e.Row.FindControl("lblCaiLiRun") as Label;
                if (lblCaiLiRun != null)
                {
                    lblCaiLiRun.Text = string.Format("{0:n2}", lirun);
                    if (po != null && po.Profit != null && lirun < po.Profit.Value)
                    {
                        lblCaiLiRun.ForeColor = System.Drawing.Color.Red;
                    }
                }

            }
            //ImageButton btnEdit = e.Row.FindControl("lblFinPrice1") as ImageButton;
            //if (btnEdit != null)
            //{
            ////    string val = string.Format("javascript:window.showModalDialog('WFPOCai.aspx?indexcai={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex);
            ////    btnEdit.Attributes.Add("onclick", val);
            //}


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblNum") as Label, SumPOCai.Num == null ? "" : SumPOCai.Num.ToString());//数量
                setValue(e.Row.FindControl("lblTotal1") as Label, SumPOCai.Total1 == null ? "" : SumPOCai.Total1.ToString());//小计1
                setValue(e.Row.FindControl("lblTotal2") as Label, SumPOCai.Total2 == null ? "" : SumPOCai.Total2.ToString());//小计2
                setValue(e.Row.FindControl("lblTotal3") as Label, SumPOCai.Total3 == null ? "" : SumPOCai.Total3.ToString());//小计3


                List<decimal> totalMax = new List<decimal>();
                if (SumPOCai.Total1 != null)
                {
                    totalMax.Add(SumPOCai.Total1.Value);
                }

                if (SumPOCai.Total2 != null)
                {
                    totalMax.Add(SumPOCai.Total2.Value);
                }

                if (SumPOCai.Total3 != null)
                {
                    totalMax.Add(SumPOCai.Total3.Value);
                }
                if (totalMax.Count > 0)
                {
                    decimal minPrice = totalMax.Min();
                    decimal lirun = 0;
                    decimal sellTotal = 0;
                    decimal otherCost = 0;
                    List<CAI_POOrders> POOrders = ViewState["Orders"] as List<CAI_POOrders>;
                    foreach (var model in POOrders)
                    {
                        sellTotal += model.SellTotal;
                        otherCost += model.OtherCost;
                    }

                    if (sellTotal != 0)
                    {
                        lirun = ((sellTotal - minPrice - otherCost) / sellTotal) * 100;
                    }

                    else
                    {
                        decimal yiLiTotal = sellTotal - minPrice - otherCost;

                        if (yiLiTotal != 0)
                        {
                            lirun = -100;
                        }
                    }
                    setValue(e.Row.FindControl("lblCaiLiRun") as Label, ConvertToObj(lirun).ToString());//数量
                }
            }
        }


        CAI_POOrders SumOrders = new CAI_POOrders();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");


                CAI_POOrders model = e.Row.DataItem as CAI_POOrders;


                SumOrders.CostTotal += model.CostTotal;
                SumOrders.Num += model.Num;
                SumOrders.OtherCost += model.OtherCost;
                SumOrders.SellTotal += model.SellTotal;
                SumOrders.YiLiTotal += model.YiLiTotal;


            }
            ImageButton btnEdit = e.Row.FindControl("btnEdit") as ImageButton;
            if (btnEdit != null)
            {
                string val = string.Format("javascript:window.showModalDialog('CG_Orders.aspx?index={0}',null,'dialogWidth:500px;dialogHeight:450px;help:no;status:no')", e.Row.DataItemIndex);
                btnEdit.Attributes.Add("onclick", val);
            }


            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {







                setValue(e.Row.FindControl("lblGoodName") as Label, "合计");//合计
                setValue(e.Row.FindControl("lblNum") as Label, SumOrders.Num.ToString());//数量




                //e.Row.Cells[7].Text = ConvertToObj(SumOrders.Num != 0 ? SumOrders.CostTotal / SumOrders.Num : 0).ToString();//成本单价
                setValue(e.Row.FindControl("lblCostPrice") as Label, ConvertToObj(SumOrders.Num != 0 ? SumOrders.CostTotal / SumOrders.Num : 0).ToString());//成本单价

                // e.Row.Cells[8].Text = SumOrders.CostTotal.ToString();//成本总额
                setValue(e.Row.FindControl("lblCostTotal") as Label, SumOrders.CostTotal.ToString());//成本总额


                // e.Row.Cells[9].Text = ConvertToObj(SumOrders.Num != 0 ? SumOrders.SellTotal / SumOrders.Num : 0).ToString();//销售单价
                setValue(e.Row.FindControl("lblSellPrice") as Label, ConvertToObj(SumOrders.Num != 0 ? SumOrders.SellTotal / SumOrders.Num : 0).ToString());//销售单价


                //e.Row.Cells[10].Text = SumOrders.SellTotal.ToString();//销售总额
                setValue(e.Row.FindControl("lblSellTotal") as Label, SumOrders.SellTotal.ToString());//销售总额


                //e.Row.Cells[11].Text = SumOrders.OtherCost.ToString();//管理费
                setValue(e.Row.FindControl("lblOtherCost") as Label, SumOrders.OtherCost.ToString());//管理费


                // e.Row.Cells[12].Text = SumOrders.YiLiTotal.ToString();//管理费
                setValue(e.Row.FindControl("lblYiLiTotal") as Label, SumOrders.YiLiTotal.ToString());//盈利总额

                if (SumOrders.SellTotal != 0)
                {
                    SumOrders.Profit = SumOrders.YiLiTotal / SumOrders.SellTotal * 100;
                }
                else if (SumOrders.YiLiTotal != 0)
                {
                    SumOrders.Profit = -100;
                }
                else
                {
                    SumOrders.Profit = 0;
                }

                setValue(e.Row.FindControl("lblProfit") as Label, ConvertToObj(SumOrders.Profit).ToString());//利润


            }

        }

    }
}
