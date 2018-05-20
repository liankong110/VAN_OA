using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;
using System.Data;
using VAN_OA.Model;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.EFrom;


namespace VAN_OA.JXC
{
    public partial class WFSupplierInvoiceList : BasePage
    {

        public string GetGestProInfo(object obj)
        {
            return VAN_OA.BaseInfo.GuestProBaseInfoList.GetGestProInfo(obj);
        }

        System.Collections.Hashtable hs = new System.Collections.Hashtable();
        System.Collections.Hashtable DiXiaoHs = new System.Collections.Hashtable();
        SupplierToInvoiceViewService supplierToInvoiceSer = new SupplierToInvoiceViewService();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

                gvDiXiao.DataSource = new List<SupplierToInvoiceView>();
                gvDiXiao.DataBind();

                var fpTypeBaseInfoService = new FpTypeBaseInfoService();
                List<FpTypeBaseInfo> gooQGooddList = fpTypeBaseInfoService.GetListArray("");
                gooQGooddList.Insert(0, new FpTypeBaseInfo { Id = -1, FpType = "全部" });
                gooQGooddList.Add(new FpTypeBaseInfo { Id = -2, FpType = "不含税" });
                dllFPstye.DataSource = gooQGooddList;
                dllFPstye.DataBind();
                dllFPstye.DataTextField = "FpType";
                dllFPstye.DataValueField = "Id";

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();

                //                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='查看所有'
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='支付单列表') and sys_Object.AutoID is not null", Session["currentUserId"]);
                if (QuanXian_ShowAll("支付单列表") == false)
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
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='支付单列表') and sys_Object.AutoID is not null", Session["currentUserId"]);
                //                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                if (NewShowAll_textName("支付单列表", "不能编辑") == false)
                {
                    gvMain.Columns[0].Visible = false;
                }
                var sql = @"select CreateName from (
select CreateName from TB_SupplierAdvancePayment
union 
select CreateName from TB_SupplierInvoice) AS TB WHERE CreateName<>'ADMIN' ORDER BY CreateName ";
                List<ListItem> createList = new List<ListItem>();
                createList.Add(new ListItem { Text = "全部", Value = "全部" });
                createList.Add(new ListItem { Text = "不含Admin", Value = "不含Admin" });
                foreach (DataRow dr in DBHelp.getDataTable(sql).Rows)
                {
                    createList.Add(new ListItem { Text = dr[0].ToString(), Value = dr[0].ToString() });
                }
                ddlCreateName.DataSource = createList;
                ddlCreateName.DataBind();

                if (Request["Ids"] != null)
                {
                    ddlStatue.Text = "通过";
                    ddlType.Text = "0";
                    Show();
                }
                else
                {
                    if (Request["PayIds"] != null)
                    {
                        ddlStatue.Text = "通过";
                        ddlType.Text = "1";
                        Show();
                    }
                    else
                    {
                        //主单
                        List<SupplierToInvoiceView> pOOrderList = new List<SupplierToInvoiceView>();
                        this.gvMain.DataSource = pOOrderList;
                        this.gvMain.DataBind();
                    }
                }
            }
        }
        #region 入库后付款
        private void Show()
        {

            string sql = " 1=1 ";

            if (ddlRePayClear.Text != "-1")
            {
                sql += string.Format(" and RePayClear={0} ", ddlRePayClear.Text);

            }
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }

            if (Request["Ids"] != null)
            {
                sql += string.Format(" and ids={0} ", Request["Ids"]);
            }
            if (Request["PayIds"] != null)
            {
                sql += string.Format(" and ids={0} ", Request["PayIds"]);
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text.Trim());
            }


            if (ddlBusType.Text != "")
            {
                sql += string.Format(" and CaiBusType='{0}'", ddlBusType.SelectedValue);
            }

            if (ddlIsSpecial.Text != "-1")
            {
                sql += string.Format("and exists(select id from CG_POOrder where Status='通过' and IsSpecial={0} and CG_POOrder.PONO=TB.PONO ) ", ddlIsSpecial.Text);
            }
            if (ddlIsHanShui.Text == "1")
            {
                sql += string.Format("and exists(select id from CG_POOrder where Status='通过' and IsHanShui={0} and CG_POOrder.PONO=TB.PONO ) ", ddlIsHanShui.Text);
            }
            if (ddlIsHanShui.Text == "0")
            {
                sql += string.Format("and (exists(select id from CG_POOrder where Status='通过' and IsHanShui={0} and CG_POOrder.PONO=TB.PONO ) or PONo like 'KC%')", ddlIsHanShui.Text);
            }
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }

                sql += string.Format(" and SupplierInvoiceDate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and SupplierInvoiceDate<='{0} 23:59:59'", txtTo.Text);
            }


            if (ddlStatue.Text != "")
            {
                sql += string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }

            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and InvProNo like '%{0}%'", txtProNo.Text.Trim());
            }


            if (txtRuCaiProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtRuCaiProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and ProNo like '%{0}%'", txtRuCaiProNo.Text.Trim());
            }

            if (txtCaiProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtCaiProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and CaiProNo like '%{0}%'", txtCaiProNo.Text.Trim());
            }


            if (txtSupplier.Text.Trim() != "")
            {
                if (cbPiPei.Checked)
                {
                    sql += string.Format(" and Supplier='{0}'", txtSupplier.Text.Trim());
                }
                else
                {
                    sql += string.Format(" and Supplier  like '%{0}%'", txtSupplier.Text.Trim());
                }

            }


            if (txtGoodNo.Text != "")
            {
                sql += string.Format(" and GoodNo like '%{0}%'", txtGoodNo.Text);
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
                if (CommHelp.VerifesToNum(txtCaiNum.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('支付数量 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and SupplierInvoiceNum{0} {1}", ddlCaiNum.Text, txtCaiNum.Text.Trim());
            }
            if (txtCaiPrice.Text.Trim() != "")
            {
                if (CommHelp.VerifesToNum(txtCaiPrice.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('付款单价 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and SupplierInvoicePrice{0} {1}", ddlCaiPrice.Text, txtCaiPrice.Text.Trim());
            }

            if (ddlType.Text != "-1")
            {
                if (ddlType.Text == "0")
                {
                    sql += string.Format(" and busType='支'");
                }
                if (ddlType.Text == "1")
                {
                    sql += string.Format(" and busType='预'");
                }
            }

            if (ddlZhiFu.Text != "-1")
            {
                sql += string.Format(" and PayStatus=" + ddlZhiFu.Text);
            }
            if (txtActTotal.Text != "")
            {
                try
                {
                    if (Convert.ToDecimal(txtActTotal.Text) < 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('付款单价必须>0！');</script>");
                        return;
                    }
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('付款单价格式有误！');</script>");
                    return;
                }
                sql += string.Format(" and SupplierInvoicePrice" + ddlActJS.Text + txtActTotal.Text);
            }

            if (!string.IsNullOrEmpty(txtSumActPay.Text))
            {
                try
                {
                    if (Convert.ToDecimal(txtSumActPay.Text) < 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('总实际支付金额必须>0！');</script>");
                        return;
                    }
                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('总实际支付金额格式有误！');</script>");
                    return;
                }
                sql += string.Format(" and SumActPay" + ddlSumActPay.Text + txtSumActPay.Text);
            }
            //if (ViewState["showAll"] != null)
            //{
            if (ddlUser.Text != "-1")//显示所有用户
            {
                //sql += string.Format(" and (DoPer='{0}' or exists(select id from CG_POOrder where CG_POOrder.PONo=TB.PONo and AppName={1}))",
                //    Session["LoginName"], Session["currentUserId"]);

                sql += string.Format(" and (DoPer='{0}' or exists(select id from CG_POOrder where CG_POOrder.PONo=TB.PONo and AppName={1}))",
                    ddlUser.SelectedItem.Text, ddlUser.Text);

            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and (DoPer IN(select loginName from tb_User where {0}) or exists(select id from CG_POOrder where CG_POOrder.PONo=TB.PONo and AppName IN(select id from tb_User where {0})))", where);

            }
            //}
            if (dllFPstye.Text != "-1" && dllFPstye.Text != "-2")
            {
                sql += string.Format(" and CaiFpType='{0}' ", dllFPstye.SelectedItem.Text);
            }
            if (dllFPstye.Text == "-2")
            {
                sql += string.Format(" and IsHanShui=0 ");
            }

            if (ddlCreateName.Text != "全部")
            {
                if (ddlCreateName.Text == "不含Admin")
                {
                    sql += string.Format(" and CreateName<>'admin' ");
                }
                else
                {
                    sql += string.Format(" and CreateName='{0}' ", ddlCreateName.Text);
                }
            }
            //正常数据 含了1-4 的情况的数据，是真实来支付的数据
            if (ddlZhiFuType.Text == "1")
            {
                sql += string.Format(" and ( (ActPay>0 and CreateName<>'admin') ");
                sql += string.Format(" or ( ActPay<0 and CreateName='admin' and IsHeBing=1 and RePayClear=2 and PayStatus=0) ");
                sql += string.Format(" or ( ActPay<0 and CreateName<>'admin' and IsHeBing=0 and RePayClear=2 and PayStatus=0) ");
                sql += string.Format(" or ( ActPay>0 and CreateName<>'admin' and RuTime is null) ) ");

            }
            //过程数据  含了5-6的情况的数据
            if (ddlZhiFuType.Text == "2")
            {
                sql += string.Format(" and ( (ActPay>0 and CreateName='admin') ");
                sql += string.Format(" or (ActPay<0 and CreateName='admin' and IsHeBing=0 and RePayClear=1 and PayStatus=2) )");
            }
            //1.正数支付单  实际支付>0 制单人<>admin  是正在执行或完成的支付单据
            if (ddlZhiFuType.Text == "3")
            {
                sql += string.Format(" and ActPay>0 and CreateName<>'admin' ");
            }
            //2.     负数支付单  实际支付<0制单人=admin  合并=1 是采购退货需要扣减的负数支付单据（尚未扣减）
            if (ddlZhiFuType.Text == "4")
            {
                sql += string.Format(" and ActPay<0 and CreateName='admin' and IsHeBing=1 and RePayClear=2 and PayStatus=0 ");
            }
            //3.     负数支付单  实际支付<0制单人=admin  合并=0 结算=未结清  支付=未支付 
            //是采购退货时扣减的负数支付已经完成的记录显示
            if (ddlZhiFuType.Text == "5")
            {
                sql += string.Format(" and ActPay<0 and CreateName<>'admin' and IsHeBing=0 and RePayClear=2 and PayStatus=0");
            }
            //4.     正数预付款单 实际支付>0 制单人<>admin  入库时间=“0001-01-01” ，
            //是正在执行或完成的预付款支付单据
            if (ddlZhiFuType.Text == "6")
            {
                sql += string.Format(" and ActPay>0 and CreateName<>'admin' and RuTime is null");
            }
            //5.     正数支付单  实际支付>0 制单人=admin  是正在执行或完成的预付款转支付单据
            if (ddlZhiFuType.Text == "7")
            {
                sql += string.Format(" and ActPay>0 and CreateName='admin' ");
            }
            //6.     负数支付单  实际支付<0制单人=admin  合并=0 结算=结清  支付=已支付 
            //是采购退货时生成的一条无用数据，表明该金额已由其他记录的负数金额来扣减
            if (ddlZhiFuType.Text == "8")
            {
                sql += string.Format(" and ActPay<0 and CreateName='admin' and IsHeBing=0 and RePayClear=1 and PayStatus=2");
            }
            if (ddlZhiFuType.Text == "10")
            {
                sql += string.Format(" and IsHeBing=1 and ActPay<0 ");
            }
            //实际提交已扣除的部分,全额采退+部分采退（不生成事后需要扣回的部分）
            if (ddlZhiFuType.Text == "11")
            {
                sql += string.Format(@" and  TB.Ids  NOT IN ( SELECT TB_SupplierInvoices.RuIds FROM TB_TempSupplierInvoice 
left join TB_SupplierInvoices on TB_SupplierInvoices.Ids=TB_TempSupplierInvoice.SupplierInvoiceIds where TB_SupplierInvoices.Ids>0
union select RuIds from TB_SupplierInvoices where IsHeBing=1) and SupplierInvoicePrice<0 AND IsHeBing=0  ");
            }

            //实采单价{0}采购单价
            if (ddlComparePrice.Text != "-1")
            {
                sql += string.Format(" and LastPay{0}GoodPrice ", ddlComparePrice.Text);
            }

            if (txtFromDate.Text != "" || txtToDate.Text != "")
            {
                string where = "";
                if (txtFromDate.Text != "")
                {
                    if (CommHelp.VerifesToDateTime(txtFromDate.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                        return;
                    }
                    where += string.Format(" and PODate>='{0} 00:00:00'", txtFromDate.Text);
                }
                if (txtToDate.Text != "")
                {
                    if (CommHelp.VerifesToDateTime(txtToDate.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                        return;
                    }
                    where += string.Format(" and PODate<='{0} 23:59:59'", txtToDate.Text);
                }

              
              

                sql += string.Format(@" and (EXISTS(SELECT ID FROM CG_POOrder WHERE IFZhui=0 AND CG_POOrder.PONo=TB.PONo {0} ) 
OR EXISTS  (select id from CAI_POOrder where PONo like 'KC%' AND CAI_POOrder.PONo=TB.PONo {0} ))", where);
            }

            if (ddlGuestTypeList.SelectedValue != "全部")
            {
                sql += string.Format(" and GuestType='{0}'", ddlGuestTypeList.SelectedValue);
            }
            if (ddlGuestProList.SelectedValue != "-2")
            {
                sql += string.Format(" and GuestPro={0}", ddlGuestProList.SelectedValue);
            }
            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }
            List<SupplierToInvoiceView> pOOrderList = supplierToInvoiceSer.GetSupplierInvoiceList(sql, ddlZhiFuType.Text);


            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

            lblALLActTotal.Text = pOOrderList.Sum(t => t.ActPay).ToString();
            lblLastPayTotal.Text = string.Format("{0:n5}", pOOrderList.Sum(t => t.LastPayTotal));
            string ids = "";
            for (int i = 0; i < this.gvMain.Rows.Count; i++)
            {
                Label payId = (gvMain.Rows[i].FindControl("lblHiddPayId")) as Label;
                Label lblbusType = (gvMain.Rows[i].FindControl("lblbusType")) as Label;
                if (lblbusType.Text == "支" && !hs.Contains(payId.Text))
                {
                    hs.Add(payId.Text, null);
                    ids += payId.Text + ",";
                }
            }

            if (ids.Length > 0)
            {
                var dt = DBHelp.getDataTable(string.Format(@"select  SupplierInvoiceId from TB_TempSupplierInvoice where SupplierInvoiceId in ({0}) group by SupplierInvoiceId", ids.Trim(',')));
                foreach (DataRow dr in dt.Rows)
                {
                    DiXiaoHs.Add(dr[0].ToString(), null);
                }
            }

            for (int i = 0; i < this.gvMain.Rows.Count; i++)
            {
                Label payId = (gvMain.Rows[i].FindControl("lblHiddPayId")) as Label;
                Label lblbusType = (gvMain.Rows[i].FindControl("lblbusType")) as Label;
                if (lblbusType.Text == "支" && DiXiaoHs.Contains(payId.Text))
                {
                    gvMain.Rows[i].BackColor = System.Drawing.Color.Yellow;
                }
            }

        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
            gvDiXiao.DataSource = new List<SupplierToInvoiceView>();
            gvDiXiao.DataBind();
            lblAllTotal.Text = "0";
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
            gvDiXiao.DataSource = new List<SupplierToInvoiceView>();
            gvDiXiao.DataBind();
            lblAllTotal.Text = "0";
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
            }
        }
        protected void gvDiXiao_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

            }
        }



        #endregion

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                string ProId = "0";
                string allE_id = "0";
                string[] payType_Id = e.CommandArgument.ToString().Split('_');
                if (payType_Id[0] == "支")
                {
                    var obj = DBHelp.ExeScalar(string.Format(@"select SUM(SupplierInvoiceTotal) from TB_SupplierInvoices where Id={0} or ids in (
select SupplierInvoiceIds from TB_TempSupplierInvoice where SupplierInvoiceId={0});", payType_Id[1]));
                    if (obj != null)
                    {
                        lblAllTotal.Text = obj.ToString();
                    }
                    gvDiXiao.DataSource = new SupplierToInvoiceViewService().GetSupplierInvoiceListToDiXiao(Convert.ToInt32(payType_Id[1]));
                    gvDiXiao.DataBind();
                    allE_id = payType_Id[1];
                    ProId = "in (31,33)";
                }
                else
                {
                    if (payType_Id[0] == "预")
                    {
                        var obj = DBHelp.ExeScalar(string.Format(@"select SUM(SupplierInvoiceTotal) from TB_SupplierAdvancePayments 
where Id={0};", payType_Id[1]));
                        if (obj != null)
                        {
                            lblAllTotal.Text = obj.ToString();
                        }
                        ProId = "=32";
                        allE_id = payType_Id[1];
                    }
                    else
                    {
                        lblAllTotal.Text = "0";
                    }
                    gvDiXiao.DataSource = new List<SupplierToInvoiceView>();
                    gvDiXiao.DataBind();
                }
                if (allE_id != "0")
                {
                    //加载已经审批的数据
                    tb_EFormsService eformsSer = new tb_EFormsService();
                    List<VAN_OA.Model.EFrom.tb_EForms> eforms = eformsSer.GetListArray(string.Format(" e_Id in (select id from tb_EForm where proId {0} and allE_id={1})",
                        ProId, allE_id));
                    if (eforms.Count > 0)
                    {
                        string mess = @"<table cellpadding='0' cellspacing='0' width='100%' bordercolorlight='#999999' bordercolordark='#FFFFFF' border='1' ><tr><td colspan='6' style=' height:20px; background-color:#336699; color:White;'>流程图</td></tr>";


                        for (int i = 0; i < eforms.Count; i++)
                        {
                            string per = "";
                            if (eforms[i].consignor != null && eforms[i].consignor != 0)
                            {
                                per = eforms[i].Consignor_Name + "(委托人：" + eforms[i].Audper_Name + ")";
                            }
                            else
                            {
                                per = eforms[i].Audper_Name;
                            }
                            mess += string.Format("<tr><td align='center'>第{0}步</td><td>序号{0}：{1}</td><td><span style='color:red;'>{2}</span>[<span style='color:blue;'>{3} {4}</span>]<br/>意见：{5}</td></tr>",
                                i + 1, eforms[i].RoleName
    , per, eforms[i].resultState, eforms[i].doTime, eforms[i].idea);
                        }
                        mess += "</table>";
                        lblMess.Text = mess;
                    }
                }
            }
            if (e.CommandName == "Delete1")
            {
                //                string[] payType_Id = e.CommandArgument.ToString().Split('_');
                //                string sql = "";
                //                string type = "";
                //                string deleteSql = "";
                //                if (payType_Id[0] == "支")
                //                {
                //                    type = "供应商付款单";
                //                    var checkSql = string.Format(@"select count(*) from TB_SupplierInvoice
                //left join TB_SupplierInvoices on TB_SupplierInvoice.id=TB_SupplierInvoices.id
                //where status='通过'  and TB_SupplierInvoice.id={0} and SupplierInvoiceTotal<0", payType_Id[1]);
                //                    if (Convert.ToInt32(DBHelp.ExeScalar(checkSql)) > 0)
                //                    {
                //                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数据由退货单生成，无法修改！');</script>");
                //                        return;
                //                    }
                //                    deleteSql = string.Format("delete from TB_SupplierInvoice where id={0};delete from TB_SupplierInvoices where id={0};", payType_Id[1]);

                //                    sql = "select CreateName,Status,ProNo,TB_SupplierInvoices.Id,IsYuFu from TB_SupplierInvoice left join TB_SupplierInvoices on TB_SupplierInvoices.id=TB_SupplierInvoice.id where TB_SupplierInvoice.id=" + payType_Id[1];
                //                }
                //                if (payType_Id[0] == "预")
                //                {
                //                    type = "供应商预付款单";
                //                    //首先查询这个单子有没有 生成相应的支付单
                //                    var checksql = string.Format(@"select count(*) from CAI_OrderChecks left join CAI_OrderCheck on CAI_OrderChecks.CheckId=CAI_OrderCheck.Id
                //where Status<>'不通过' and  CaiId in (select  caiIds from TB_SupplierAdvancePayment left join TB_SupplierAdvancePayments
                //on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id
                //where status='通过' and TB_SupplierAdvancePayment.id={0} )", payType_Id[1]);
                //                    if (Convert.ToInt32(DBHelp.ExeScalar(checksql)) > 0)
                //                    {
                //                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数据已经存在入库数据，或正在入库的单子，无法修改！');</script>");
                //                        return;
                //                    }
                //                    deleteSql = string.Format("delete from TB_SupplierAdvancePayment where id={0};delete from TB_SupplierAdvancePayments where id={0};", payType_Id[1]);

                //                    sql = "select CreateName,Status,ProNo,Id from  TB_SupplierAdvancePayment where id=" + payType_Id[1];
                //                }

                //                DataTable tb = DBHelp.getDataTable(sql);
                //                if (tb.Rows.Count <= 0)
                //                {
                //                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('信息不存在！');</script>");
                //                    return;
                //                }
                //                string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='" + type + "')", payType_Id[1]);
                //                string DeleteAll = string.Format("delete from tb_EForms where e_Id={0};delete from tb_EForm where id={0};", DBHelp.ExeScalar(efromId));
                //                deleteSql = deleteSql + DeleteAll;
                //                DBHelp.ExeCommand(deleteSql);
                //                Show();
                //                //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除成功！');</script>");
                //                return;
            }
            if (e.CommandName == "ReEdit")
            {

                string[] payType_Id = e.CommandArgument.ToString().Split('_');
                string sql = "";
                string type = "";
                if (payType_Id[0] == "支")
                {
                    type = "供应商付款单";
                    //首先查询一下入库单有没有已经全部开票
                    //                     var checkSql = string.Format(@"select count(*) from TB_SupplierInvoice
                    //left join TB_SupplierInvoices on TB_SupplierInvoice.id=TB_SupplierInvoices.id
                    //left join CAI_OrderInHouses on CAI_OrderInHouses.ids=TB_SupplierInvoices.ruIds
                    //where status='通过' and PayStatus=2 and TB_SupplierInvoice.id={0}", payType_Id[1]);
                    var checkSql = string.Format(@"select count(*) from TB_SupplierInvoice
left join TB_SupplierInvoices on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where status='通过'  and TB_SupplierInvoice.id={0} and SupplierInvoiceTotal<0", payType_Id[1]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(checkSql)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数据由退货单生成，无法修改！');</script>");
                        return;
                    }
                    sql = "select CreateName,Status,ProNo,TB_SupplierInvoices.Id,IsYuFu,LastSupplier from TB_SupplierInvoice left join TB_SupplierInvoices on TB_SupplierInvoices.id=TB_SupplierInvoice.id where TB_SupplierInvoice.id=" + payType_Id[1];


                }
                if (payType_Id[0] == "预")
                {
                    type = "供应商预付款单";
                    //首先查询这个单子有没有 生成相应的支付单
                    var checksql = string.Format(@"select count(*) from CAI_OrderChecks left join CAI_OrderCheck on CAI_OrderChecks.CheckId=CAI_OrderCheck.Id
where Status<>'不通过' and  CaiId in (select  caiIds from TB_SupplierAdvancePayment left join TB_SupplierAdvancePayments
on TB_SupplierAdvancePayment.id=TB_SupplierAdvancePayments.Id
where status='通过' and TB_SupplierAdvancePayment.id={0} )", payType_Id[1]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(checksql)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数据已经存在入库数据，或正在入库的单子，无法修改！');</script>");
                        return;
                    }
                    sql = "select CreateName,Status,ProNo,Id from  TB_SupplierAdvancePayment where id=" + payType_Id[1];
                }

                DataTable tb = DBHelp.getDataTable(sql);
                if (tb.Rows.Count <= 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('信息不存在！');</script>");
                    return;
                }
                //是否是此单据的申请人


                //if (payType_Id[0] != "预"&&Session["LoginName"].ToString() != tb.Rows[0]["CreateName"].ToString())
                //{

                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('必须由原单据申请人 重新发起，其他人不能重新提交编辑！');</script>");
                //    return;
                //}

                //首先单子要先通过               

                if (tb.Rows[0]["Status"] != null && tb.Rows[0]["Status"].ToString() == "执行中")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据在执行中不能重新编辑！');</script>");
                    return;
                }
                else
                {
                    //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据必须已经审批通过才能重新编辑！');</script>");
                    //return;
                }

                if (payType_Id[0] == "支")
                {
                    if (Convert.ToBoolean(tb.Rows[0]["IsYuFu"]))
                    {
                        type = "供应商付款单（预付单转支付单）";
                    }

                    if (tb.Rows[0]["LastSupplier"] != null)
                    {
                        //判断改供应商是否有在支付中的单子
                        if (TB_SupplierInvoiceService.checkSupplierDoing(tb.Rows[0]["LastSupplier"].ToString()))
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有在执行中的支付单，请排队等候');</script>");
                            return;
                        }

                        //判断改供应商是否有在支付中的单子
                        if (TB_SupplierInvoiceService.CheckAdvanceAndSupplierInvoices(tb.Rows[0]["LastSupplier"].ToString(), 1))
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('该供应商有【预付款】在执行中的【抵扣支付单】，请排队等候');</script>");

                            return;
                        }
                    }
                }

                sql = "select pro_Id from A_ProInfo where pro_Type='" + type + "'";

                string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='" + type + "')", payType_Id[1]);

                string url = "";
                if (payType_Id[0] == "支")
                {
                    url = "~/JXC/WFSupplierInvoiceVerify.aspx?ProId=" + DBHelp.ExeScalar(sql) + "&allE_id=" + payType_Id[1] + "&EForm_Id=" + DBHelp.ExeScalar(efromId) + "&&ReAudit=true";
                }
                else
                {
                    url = "~/JXC/WFSupplierAdvancePaymentVerify.aspx?ProId=" + DBHelp.ExeScalar(sql) + "&allE_id=" + payType_Id[1] + "&EForm_Id=" + DBHelp.ExeScalar(efromId) + "&&ReAudit=true";

                }

                Response.Redirect(url);


                //没有做过检验单


            }
        }




    }
}
