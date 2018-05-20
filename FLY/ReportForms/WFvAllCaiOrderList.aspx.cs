using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Model.ReportForms;
using VAN_OA.Dal.JXC;
using VAN_OA.JXC;
using VAN_OA.Model;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using System.Data.SqlClient;

namespace VAN_OA.ReportForms
{
    public partial class WFvAllCaiOrderList : BasePage
    {
        List<FpTypeBaseInfo> gooQGooddList = new List<FpTypeBaseInfo>();
        List<FpTypeBaseInfo> gooQGooddList_1 = new List<FpTypeBaseInfo>();
        CAI_POOrderService POSer = new CAI_POOrderService();
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

                var fpTypeBaseInfoService = new FpTypeBaseInfoService();
                gooQGooddList_1 = fpTypeBaseInfoService.GetListArray("");
                gooQGooddList_1.Insert(0, new FpTypeBaseInfo() { FpType = "全部" });
             
                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService(); 
                if (NewShowAll_textName("采购订单列表2", "查看所有")==false)
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
                if (NewShowAll_textName("采购订单列表2", "禁止含税设置")==false)
                {
                    gvMain.Columns[0].Visible = false;
                    btnSave.Visible = false;
                }
                else
                {
                    gvMain.Columns[1].Visible = false;
                }



                //主单
                List<vAllCaiOrderList> pOOrderList = new List<vAllCaiOrderList>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //发票类型
                dllSelectFPstye.DataSource = gooQGooddList_1;
                dllSelectFPstye.DataTextField = "FpType";
                dllSelectFPstye.DataValueField = "FpType";
                dllSelectFPstye.DataBind();
                //
                
            }
        }

        private string GetSql()
        {
            var fpTypeBaseInfoService = new FpTypeBaseInfoService();
            gooQGooddList = fpTypeBaseInfoService.GetListArray("");

            allFpTypes = gooQGooddList.Select(t => t.FpType).ToList();

            string sql = " 1=1 ";

            if (txtPONo.Text.Trim() != "")
            {
                 
                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            //if (txtFrom.Text != "")
            //{
            //    sql += string.Format(" and PODate>='{0} 00:00:00'", txtFrom.Text);
            //}

            //if (txtTo.Text != "")
            //{
            //    sql += string.Format(" and PODate<='{0} 23:59:59'", txtTo.Text);
            //}


            if (txtFrom.Text != "" || txtTo.Text != "")
            {
                string where = "";
                if (txtFrom.Text != "")
                { 
                    where += string.Format(" and PODate>='{0} 00:00:00'", txtFrom.Text);
                }
                if (txtTo.Text != "")
                {
                    where += string.Format(" and PODate<='{0} 23:59:59'", txtTo.Text);
                }
                sql += string.Format(@" and (EXISTS(SELECT ID FROM CG_POOrder WHERE IFZhui=0 AND CG_POOrder.PONo=vAllCaiOrderList.PONo {0} ) 
OR EXISTS  (select id from CAI_POOrder where PONo like 'KC%' AND CAI_POOrder.PONo=vAllCaiOrderList.PONo {0} ))", where);
            } 

            if (!string.IsNullOrEmpty(txtAuditDate.Text))
            {
                sql += " and exists( select id from tb_EForm where tb_EForm.allE_id=vAllCaiOrderList.id and proId=20 ";
                sql += string.Format(" and e_LastTime>='{0} 00:00:00'", txtAuditDate.Text);
                sql += string.Format(" and e_LastTime<='{0} 23:59:59'", txtAuditDate.Text);
                sql += " )";
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
                sql += string.Format(" and CG_ProNo  like '%{0}%'", txtPoProNo.Text.Trim());
            }

            if (txtCaiugou.Text != "")
            {
                sql += string.Format(" and CaiGou  like '%{0}%'", txtCaiugou.Text);
            }

          
            if (ddlUser.Text != "-1")
            {
                //sql += string.Format(" and (AppName={0} or exists(select id from CG_POOrder where CG_POOrder.PONo=vAllCaiOrderList.PONo and AppName={0}))", ddlUser.Text);

                sql += string.Format(" and AE='{0}'", ddlUser.SelectedItem.Text);
            }

            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and exists (select id from CG_POOrder where IFZhui=0 and CG_POOrder.PONo=vAllCaiOrderList.PONo and AppName in(select id from tb_User where {0}))", where);
            }
            if (txtGoodNo.Text != "")
            {
                sql += string.Format(" and GoodNo  like '%{0}%'", txtGoodNo.Text);
            }

            if (txtLastSupplier.Text.Trim() != "")
            {
                if (cbPiPei.Checked)
                {
                    sql += string.Format(" and lastSupplier='{0}'", txtLastSupplier.Text.Trim());
                }
                else
                {
                    sql += string.Format(" and lastSupplier like '%{0}%'", txtLastSupplier.Text.Trim());
                }
            }
            if (ddlCaiGou.Text == "1")
            {
                sql += string.Format(" and lastSupplier<>'库存'");                
            }
            if (ddlCaiGou.Text == "0")
            {
                sql += string.Format(" and lastSupplier='库存'");
            }
            if (ddlHanShui.Text != "-1")
            {
                sql += string.Format(" and IsHanShui={0} ", ddlHanShui.Text);
            }

            if (dllSelectFPstye.Text != "全部")
            {
                sql += string.Format(" and CaiFpType='{0}'", dllSelectFPstye.Text);
            }
            if (txtNameOrTypeOrSpec.Text != "" || txtNameOrTypeOrSpecTwo.Text != "")
            {
                if (txtNameOrTypeOrSpec.Text != "" && txtNameOrTypeOrSpecTwo.Text != "")
                {
                    sql += string.Format(" and ((GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%') or (GoodTypeSmName like '%{1}%' or GoodName  like '%{1}%' or GoodSpec like '%{1}%'))",
                       txtNameOrTypeOrSpec.Text, txtNameOrTypeOrSpecTwo.Text);
                }
                else if (txtNameOrTypeOrSpec.Text != "" || txtNameOrTypeOrSpecTwo.Text != "")
                {
                    var NameOrTypeOrSpec = "";
                    if (txtNameOrTypeOrSpec.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpec.Text;
                    if (txtNameOrTypeOrSpecTwo.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpecTwo.Text;

                    sql += string.Format(" and (GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%')",
                       NameOrTypeOrSpec);
                }
            }
            if (txtCaiNum.Text.Trim() != "")
            {
                sql += string.Format(" and Num{0} {1}", ddlCaiNum.Text, txtCaiNum.Text.Trim());
            }
            if (txtCaiPrice.Text.Trim() != "")
            {
                sql += string.Format(" and lastPrice{0} {1}", ddlCaiPrice.Text, txtCaiPrice.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtProNo.Text.Trim()))
            {                
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text.Trim());
            }
            if (ddlIsSpecial.Text != "-1")
            {
                sql += string.Format("and exists(select id from CG_POOrder where Status='通过' and IsSpecial={0} and CG_POOrder.PONO=vAllCaiOrderList.PONO ) ", ddlIsSpecial.Text);
            }
            return sql;
        }
        private void Show()
        {

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }

            }
            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
            }

            if (txtCaiNum.Text != "")
            {
                if (CommHelp.VerifesToNum(txtCaiNum.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数量 格式错误！');</script>");
                    return;
                }
            }

            if (txtCaiPrice.Text != "")
            {
                if (CommHelp.VerifesToNum(txtCaiPrice.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('最终采购单价 格式错误！');</script>");
                    return;
                }
            }

            if (txtAuditDate.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtAuditDate.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('审批日期 格式错误！');</script>");
                    return;
                }
            }
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
            }
            if (txtPoProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtPoProNo.Text.Trim()) == false)
                {
                    return;
                }

            }
                PagerDomain page = new PagerDomain();
            page.CurrentPageIndex = AspNetPager1.CurrentPageIndex;
            decimal Total=0;
            List<vAllCaiOrderList> pOOrderList = this.POSer.GetListArrayAll_Page(GetSql(), page, out Total); 
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
            lblTotal.Text = Total.ToString();// pOOrderList.Sum(t => (t.lastPrice * t.Num)).ToString();
            AspNetPager1.RecordCount = page.TotalCount;
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
        }
        private void setValue(Label control, string value)
        {
            control.Text = value;
        }
        vAllCaiOrderList SumPOCai = new vAllCaiOrderList();
        private List<string> allFpTypes = new List<string>();
        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                vAllCaiOrderList model = e.Row.DataItem as vAllCaiOrderList;

                DropDownList drp = (DropDownList)e.Row.FindControl("dllFPstye");
                if (ViewState["isFPTypeEdist"] != null)
                {
                    drp.Enabled = false;
                }
                drp.DataSource = gooQGooddList;
                drp.DataTextField = "FpType";
                drp.DataValueField = "FpType";
                drp.DataBind();
                //  选中 DropDownList
                try
                {
                    var hidTxt = ((HiddenField)e.Row.FindControl("hidtxt")).Value;
                    if (hidTxt == "")
                    {
                        drp.SelectedIndex = allFpTypes.IndexOf("增值税发票");
                    }
                    else
                    {
                        drp.SelectedIndex = allFpTypes.IndexOf(hidTxt);
                    }
                    if (gvMain.Columns[0].Visible == false)
                    {
                        drp.Enabled = false;
                    }
                }
                catch (Exception)
                {


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

                if (model.SellTotal != null)
                {
                    if (SumPOCai.SellTotal == null) SumPOCai.SellTotal = 0;
                    SumPOCai.SellTotal += model.SellTotal;
                }
                if (model.OtherCost != null)
                {
                    if (SumPOCai.OtherCost == null) SumPOCai.OtherCost = 0;
                    SumPOCai.OtherCost += model.OtherCost;
                }
                SumPOCai.CostTotal += model.CostTotal;


                SumPOCai.YiLiTotal += model.YiLiTotal;

               
                if (!SumPOCai.LastTotal.HasValue)
                {
                    SumPOCai.LastTotal = 0;
                }
                SumPOCai.LastTotal += model.lastPrice * model.Num;
                
                List<decimal> pricelMax = new List<decimal>();
                pricelMax.Add(model.SupperPrice);
                pricelMax.Add(model.SupperPrice1);
                pricelMax.Add(model.SupperPrice2);
                decimal minPrice = pricelMax.Min();
                decimal lirun = 0;
                if (model.SellTotal != 0)
                {
                    lirun = ((model.SellTotal - minPrice * model.Num - model.OtherCost) / model.SellTotal) * 100;
                }
                else
                {
                    decimal yiLiTotal = model.SellTotal - minPrice * model.Num - model.OtherCost;

                    if (yiLiTotal != 0)
                    {
                        lirun = -100;
                    }
                }
                Label lblCaiLiRun = e.Row.FindControl("lblCaiLiRun") as Label;
                lblCaiLiRun.Text = string.Format("{0:n2}", lirun);
                if (lirun < model.Profit)
                {
                    lblCaiLiRun.ForeColor = System.Drawing.Color.Red;
                }

            }



            if (e.Row.RowType == DataControlRowType.Footer)
            {


                setValue(e.Row.FindControl("lblGoodName") as Label, "合计");//合计
                setValue(e.Row.FindControl("lblNum") as Label, SumPOCai.Num.ToString());//数量




                //e.Row.Cells[7].Text = ConvertToObj(SumPOCai.Num != 0 ? SumPOCai.CostTotal / SumPOCai.Num : 0).ToString();//成本单价
                setValue(e.Row.FindControl("lblCostPrice") as Label, ConvertToObj(SumPOCai.Num != 0 ? SumPOCai.CostTotal / SumPOCai.Num : 0).ToString());//成本单价

                // e.Row.Cells[8].Text = SumPOCai.CostTotal.ToString();//成本总额
                setValue(e.Row.FindControl("lblCostTotal") as Label, SumPOCai.CostTotal.ToString());//成本总额


                // e.Row.Cells[9].Text = ConvertToObj(SumPOCai.Num != 0 ? SumPOCai.SellTotal / SumPOCai.Num : 0).ToString();//销售单价
                setValue(e.Row.FindControl("lblSellPrice") as Label, ConvertToObj(SumPOCai.Num != 0 ? SumPOCai.SellTotal / SumPOCai.Num : 0).ToString());//销售单价


                //e.Row.Cells[10].Text = SumPOCai.SellTotal.ToString();//销售总额
                setValue(e.Row.FindControl("lblSellTotal") as Label, SumPOCai.SellTotal.ToString());//销售总额


                //e.Row.Cells[11].Text = SumPOCai.OtherCost.ToString();//管理费
                setValue(e.Row.FindControl("lblOtherCost") as Label, SumPOCai.OtherCost.ToString());//管理费


                // e.Row.Cells[12].Text = SumPOCai.YiLiTotal.ToString();//管理费
                setValue(e.Row.FindControl("lblYiLiTotal") as Label, SumPOCai.YiLiTotal.ToString());//盈利总额

                if (SumPOCai.SellTotal != 0)
                {
                    SumPOCai.Profit = SumPOCai.YiLiTotal / SumPOCai.SellTotal * 100;
                }
                else if (SumPOCai.YiLiTotal != 0)
                {
                    SumPOCai.Profit = -100;
                }
                else
                {
                    SumPOCai.Profit = 0;
                }

                setValue(e.Row.FindControl("lblProfit") as Label, ConvertToObj(SumPOCai.Profit).ToString());//利润


                setValue(e.Row.FindControl("lblNum") as Label, SumPOCai.Num == null ? "" : SumPOCai.Num.ToString());//数量
                setValue(e.Row.FindControl("lblTotal1") as Label, SumPOCai.Total1 == null ? "" : SumPOCai.Total1.ToString());//小计1
                setValue(e.Row.FindControl("lblTotal2") as Label, SumPOCai.Total2 == null ? "" : SumPOCai.Total2.ToString());//小计2
                setValue(e.Row.FindControl("lblTotal3") as Label, SumPOCai.Total3 == null ? "" : SumPOCai.Total3.ToString());//小计3

                setValue(e.Row.FindControl("lblLastTrueTotal") as Label, SumPOCai.LastTotal == null ? "" : SumPOCai.LastTotal.ToString());//小计3

                List<decimal> totalMax = new List<decimal>();
                if (SumPOCai.Total1 != null)
                {
                    totalMax.Add(SumPOCai.Total1);
                }

                if (SumPOCai.Total2 != null)
                {
                    totalMax.Add(SumPOCai.Total2);
                }

                if (SumPOCai.Total3 != null)
                {
                    totalMax.Add(SumPOCai.Total3);
                }
                if (totalMax.Count > 0)
                {
                    decimal minPrice = totalMax.Min();
                    decimal lirun = 0;
                    decimal sellTotal = 0;
                    decimal otherCost = 0;

                    sellTotal = SumPOCai.SellTotal;
                    otherCost = SumPOCai.OtherCost;


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
        protected object ConvertToObj(object obj)
        {
            if (obj != null) return string.Format("{0:f2}", Convert.ToDecimal(obj));
            return 0;
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMain.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            string where = " IDs  in (";
            string expWhere = " IDs  in (";
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlCommand objCommand = conn.CreateCommand();
                for (int i = 0; i < this.gvMain.Rows.Count; i++)
                {
                    CheckBox cb = (gvMain.Rows[i].FindControl("cbIsHanShui")) as CheckBox;
                    if (cb.Checked)
                    {
                        Label lblIds = (gvMain.Rows[i].FindControl("myId")) as Label;
                        // where += "'" + lblIds.Text + "',";


                        DropDownList drp = ((DropDownList)gvMain.Rows[i].FindControl("dllFPstye"));
                        objCommand.CommandText = string.Format("update CAI_POCai set IsHanShui=1, CaiFpType='{1}' where Ids='{0}'",
                            lblIds.Text, drp.Text);
                        objCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        Label lblIds = (gvMain.Rows[i].FindControl("myId")) as Label;
                        expWhere += "'" + lblIds.Text + "',";
                    }
                }
                conn.Close();
            }

            //if (where != " IDs  in (")
            //{
            //    where = where.Substring(0, where.Length - 1) + ")";
            //    var sql = "update CAI_POCai set IsHanShui=1 where " + where;
            //    DBHelp.ExeCommand(sql);
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            //}

            if (expWhere != " IDs  in (")
            {
                expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                var sql = "update CAI_POCai set IsHanShui=0 where " + expWhere;
                DBHelp.ExeCommand(sql);
              
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
        }
       
        protected void btnPrint_Click(object sender, EventArgs e)
        { 
            Session["print"] = GetSql();
            Response.Write("<script>window.open('../JXC/WFCai_OrderPrint.aspx','_blank')</script>");
        }

        protected void ddlBusType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBusType.Text != "0")
            {
                ddlIsSpecial.Text = "-1";
                ddlIsSpecial.Enabled = false;
            }
            else
            {
                ddlIsSpecial.Text = "0";
                ddlIsSpecial.Enabled = true;
            }
        }
    }
}
