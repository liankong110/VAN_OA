using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Model.JXC;
using VAN_OA.Model;

namespace VAN_OA.JXC
{
    public partial class WFSellFPReport : BasePage
    {
        SellFPReportService _dal = new SellFPReportService();
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

                if (Session["currentUserId"] != null)
                {
                    List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                    VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                    if (VAN_OA.JXC.SysObj.IfShowAll("出库发票清单", Session["currentUserId"], "ShowAll") == false)
                    {
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


                    List<SellFPReport> list = new List<SellFPReport>();
                    gvMain.DataSource = list;
                    gvMain.DataBind();

                    var fpTypeBaseInfoService = new FpTypeBaseInfoService();
                    List<FpTypeBaseInfo> gooQGooddList = fpTypeBaseInfoService.GetListArray("");
                    gooQGooddList.Insert(0, new FpTypeBaseInfo { FpType = "全部", Id = 0 });
                    dllFPstye.DataSource = gooQGooddList;
                    dllFPstye.DataBind();
                    dllFPstye.DataTextField = "FpType";
                    dllFPstye.DataValueField = "Id";
                    //                    string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='可导出'
                    //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='出库发票清单') and sys_Object.AutoID is not null", Session["currentUserId"]);
                    //                    if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                    if (NewShowAll_textName("出库发票清单", "可导出"))
                    {
                        Button1.Visible = true;
                    }
                    else
                    {
                        Button1.Visible = false;
                    }
                    if (Request["PONo"] != null)
                    {
                        txtPONo.Text = Request["PONo"].ToString();
                        cbIsSpecial.Checked = false;
                        Show();
                    }
                }
            }
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
        private void Show()
        {
            if (!string.IsNullOrEmpty(txtPOTimeFrom.Text))
            {
                if (CommHelp.VerifesToDateTime(txtPOTimeFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单时间 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtPOTimeTo.Text))
            {
                if (CommHelp.VerifesToDateTime(txtPOTimeTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单时间 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtOutFrom.Text))
            {
                if (CommHelp.VerifesToDateTime(txtOutFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('出库时间 格式错误！');</script>");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(txtOutTo.Text))
            {
                if (CommHelp.VerifesToDateTime(txtOutTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('出库时间 格式错误！');</script>");
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
            List<SellFPReport> myList = GetListArray();
            System.Collections.Hashtable hs = new System.Collections.Hashtable();
            System.Collections.Hashtable hs1 = new System.Collections.Hashtable();
            decimal HSTotal = 0;
            decimal KPTotal = 0;
            decimal WHPTotal = 0;
            decimal NoHSTotal = 0;
            decimal TotalAvgPrice = 0;
            foreach (var m in myList)
            {
                if (m.POTotal.HasValue && !hs.Contains(m.Id))
                {
                    hs.Add(m.Id, null);
                    if (m.IsPoFax)
                    {
                        HSTotal += m.POTotal.Value;
                    }
                    else
                    {
                        NoHSTotal += m.POTotal.Value;
                    }
                }

                if (m.IsPoFax && m.hadFpTotal.HasValue && !hs1.Contains(m.PONo))
                {
                    hs1.Add(m.PONo, null);
                    KPTotal += m.hadFpTotal.Value;
                }
                if (m.avgLastPrice !=null&&m.totalNum != null)
                {
                    TotalAvgPrice += m.avgLastPrice.Value * m.totalNum.Value;
                }
            }

            lblHSTotal.Text = HSTotal.ToString();
            lblKPTotal.Text = KPTotal.ToString();
            lblNoHSTotal.Text = NoHSTotal.ToString();
            lblWHPTotal.Text = (HSTotal - KPTotal).ToString();
            lblAvgPriceTotal.Text = TotalAvgPrice.ToString() ;
            AspNetPager1.RecordCount = myList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            gvMain.DataSource = myList;
            gvMain.DataBind();
        }

        public List<SellFPReport> GetListArray()
        {
            string pono = "";
            string guestName = "";
            string AE = "";
            string DiffDate = "";
            string JiaoFu = "";
            string FPState = "";

            if (txtPONo.Text.Trim() != "")
            {
                pono = string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtPOName.Text.Trim()))
            {
                pono += string.Format(" and PONAME like '%{0}%'", txtPOName.Text.Trim());
            }
            if (txtGuestName.Text.Trim() != "")
            {
                guestName = string.Format(" and CG_POOrder.GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }

            if (ddlGuestType.Text == "2")//本部门
            {
                guestName += string.Format(" and CG_POOrder.GuestName like '本部门%'");
                //guestName += string.Format(" and exists(select id from TB_GuestTrack where CG_POOrder.GuestName like '本部门%' and CG_POOrder.GuestId=TB_GuestTrack.ID)");

            }

            if (ddlGuestType.Text == "1")//不含本部门  
            {
                guestName += string.Format(" and CG_POOrder.GuestName not like '本部门%'");
                //guestName += string.Format(" and exists(select id from TB_GuestTrack where CG_POOrder.GuestName not like '本部门%' and CG_POOrder.GuestId=TB_GuestTrack.ID)");
            }

            if (ddlDiffDate.Text == "1")
            {
                DiffDate = " and DATEDIFF(day,RuTime,getdate()) <=30";
            }

            else if (ddlDiffDate.Text == "2")
            {
                DiffDate = " and 30<DATEDIFF(day,RuTime,getdate()) and DATEDIFF(day,RuTime,getdate())<=60";
            }
            else if (ddlDiffDate.Text == "3")
            {
                DiffDate = " and 60<DATEDIFF(day,RuTime,getdate()) and DATEDIFF(day,RuTime,getdate())<=90";
            }
            else if (ddlDiffDate.Text == "4")
            {
                DiffDate = " and DATEDIFF(day,RuTime,getdate())>90 ";
            }
            else if (ddlDiffDate.Text == "5")
            {
                DiffDate = " and DATEDIFF(day,RuTime,getdate())>30 ";
            }

            if (ddlFPState.Text == "1")
            {
                FPState = " and POStatue3='已开票'";
            }
            else if (ddlFPState.Text == "2")
            {
                FPState = " and POStatue3<>'已开票'";
                DiffDate += " and hadFpTotal is not null ";
            }
            else if (ddlFPState.Text == "3")//未开票 (暂缓)
            {
                DiffDate += " and hadFpTotal is null ";
            }
            else if (ddlFPState.Text == "4")
            {
                FPState = " and POStatue3<>'已开票'";
            }

            if (ddlJiaoFu.Text == "1")
            {
                FPState = " and POStatue2='已交付'";
            }
            else if (ddlJiaoFu.Text == "2")
            {
                FPState = " and (POStatue2='' or POStatue2 is null) ";
            }


            if (ddlUser.Text == "-1")//显示所有用户
            {
                //var model = Session["userInfo"] as User;
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职') AND PONO=JXC_REPORT.PONO )", Session["currentUserId"]);
            }
            else if (ddlUser.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
                AE = string.Format(" and AE in (select loginName from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}')", model.LoginIPosition);
            }
            else
            {
                AE = string.Format(" and AE='{0}' ", ddlUser.SelectedItem.Text);
            }

            if (ddlCompany.Text != "-1")
            {
                string where1 = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                AE += string.Format(" and IFZhui=0 and  AE IN(select loginName from tb_User where {0})", where1);
            }
            string poTimeWhere = "";
            if (txtPOTimeFrom.Text != "")
            {
                poTimeWhere += string.Format(" and PODate>='{0} 00:00:00'", txtPOTimeFrom.Text);
            }


            if (txtPOTimeTo.Text != "")
            {
                poTimeWhere += string.Format(" and PODate<='{0} 23:59:59'", txtPOTimeTo.Text);
            }

            string IsSpecial = "";
            if (cbIsSpecial.Checked)
            {
                IsSpecial = " and IsSpecial=0 ";
            }
            //else
            //{
            //    IsSpecial = " and IsSpecial=1 ";
            //}

            string isFax = "";
            if (cbIsPoFax.Checked)
            {
                isFax = " and IsPoFax=1 ";
                if (dllFPstye.Text != "0")
                {
                    isFax += string.Format(" and FpType='{0}'", dllFPstye.SelectedItem.Text);
                }
            }
            else
            {
                isFax = " and IsPoFax=0 ";
            }

            string outTime = "";
            if (txtOutFrom.Text != "")
            {
                outTime += string.Format(" and ruTime>='{0} 00:00:00' ", txtOutFrom.Text);
            }


            if (txtOutTo.Text != "")
            {
                outTime += string.Format(" and ruTime<='{0} 23:59:59' ", txtOutTo.Text);
            }

            //string outCon = "";
            if (txtOutFrom.Text != "" || txtOutTo.Text != "")
            {
                DiffDate += " and outProNo is not null ";
            }
            string isClear = "";
            if (cbNoClear.Checked == false)
            {
                isClear = " and POStatue4='已结清'";
            }
            else
            {
                isClear = " and (POStatue4='' or POStatue4 is null) ";
            }
            if (cbPOWCG.Checked)
            {
                DiffDate += " and avgLastPrice is null ";
                DiffDate += " and GoodNum is null ";
            }
            if (cbCGWC.Checked)
            {
                DiffDate += " and avgLastPrice is not null ";
                DiffDate += " and GoodNum is null ";
            }

            if (cbNumZero.Checked)
            {
                DiffDate += " and ((avgLastPrice is null and GoodNum is null) ";

                DiffDate += " or (avgLastPrice is not null and GoodNum is null)) ";
            }
            string sql = string.Format(@"select  GoodAreaNumber,SellInNums,CG_POOrder.id,CG_POOrder.IsPoFax,CG_POOrder.POName,tb1.PONO,TB1.AE, TB1.GuestName,TB1.GoodId,TB1.GoodNo,TB1.GoodName,TB1.GoodSpec,TB1.avgSellPrice,TB1.AppName, avgLastPrice,POTotal_View.POTotal,CG_POOrder.PODate,CG_POOrder.FPTotal,hadFpTotal,RuTime,GoodSellPrice, DATEDIFF(day,ruTime,getdate()) as diffDate,outProNo,GoodNum from (
select  GoodAreaNumber,CG_POOrder.PONo,CG_POOrder.AE,CG_POOrder.GuestName,CG_POOrders.GoodId,TB_Good.GoodNo, TB_Good.GoodName,
TB_Good.GoodSpec,avg(SellPrice) avgSellPrice,AppName  from CG_POOrder
left join CG_POOrders on CG_POOrder.id=CG_POOrders.id
left join TB_Good on TB_Good.GoodId=CG_POOrders.GoodId
where  TB_Good.GoodName is not null and Status='通过' " + pono + guestName + FPState + JiaoFu + AE + poTimeWhere + IsSpecial + isFax + isClear + @"  
group by CG_POOrder.PONo,CG_POOrder.AE,CG_POOrder.GuestName,TB_Good.GoodNo, TB_Good.GoodName,
TB_Good.GoodSpec,CG_POOrders.GoodId,AppName,GoodAreaNumber
)as tb1-- 项目基本信息汇总

left join POTotal_View on POTotal_View.PONo=tb1.PONo--查询 项目金额和 已开发票金额
left join CG_POOrder on CG_POOrder.PONo=tb1.PONo and CG_POOrder.IFZhui=0 and Status='通过' --找发票号和原始订单日期
left join 
(
select GoodNum,proNo as outProNo,PONo, RuTime,GooId ,GoodSellPrice,Sell_OrderOutHouses.ids,GoodPrice as avgLastPrice FROM Sell_OrderOutHouse 
left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id where Status='通过' " + outTime + @"
)
as tb3 on tb1.PONo=tb3.PONo and tb1.GoodId=tb3.GooId 
left join 
(
select  sum(GoodNum) as SellInNums,SellOutOrderId  FROM Sell_OrderInHouse 
left join Sell_OrderInHouses on Sell_OrderInHouse.Id=Sell_OrderInHouses.id where Status='通过' 
group by SellOutOrderId 
) as tb4 on tb3.Ids=tb4.SellOutOrderId  where   (GoodNum is null or isnull(SellInNums,0) <> GoodNum)
" + (DiffDate == "" ? "" : DiffDate));

            sql += " order by tb1.PONO desc";
            return _dal.GetListArray(sql);
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                SellFPReport model = e.Row.DataItem as SellFPReport;
                if (model.hadFpTotal.HasValue && model.POTotal != 0 && model.POTotal == model.hadFpTotal)
                    e.Row.BackColor = System.Drawing.Color.Red;

                if (model.hadFpTotal.HasValue && model.POTotal != 0 && model.POTotal > model.hadFpTotal)
                    e.Row.BackColor = System.Drawing.Color.DarkOrange;





            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string pono = "";
            string guestName = "";
            string AE = "";
            string DiffDate = "";
            string JiaoFu = "";
            string FPState = "";

            if (txtPONo.Text != "")
            {
                pono = string.Format(" and PONo like '%{0}%'", txtPONo.Text);
            }
            if (txtGuestName.Text != "")
            {
                guestName = string.Format(" and CG_POOrder.GuestName like '%{0}%'", txtGuestName.Text);
            }

            if (ddlDiffDate.Text == "1")
            {
                DiffDate = " and diffDate <=30";
            }

            else if (ddlDiffDate.Text == "2")
            {
                DiffDate = " and 30<diffDate and diffDate<=60";
            }
            else if (ddlDiffDate.Text == "3")
            {
                DiffDate = " and 60<diffDate and diffDate<=90";
            }
            else if (ddlDiffDate.Text == "4")
            {
                DiffDate = " and diffDate>90 ";
            }
            else if (ddlDiffDate.Text == "5")
            {
                DiffDate = " and diffDate>30 ";
            }

            if (ddlFPState.Text == "1")
            {
                FPState = " and POStatue3='已开票'";
            }
            else if (ddlFPState.Text == "2")
            {
                FPState = " and POStatue3<>'已开票'";
                DiffDate += " and hadFpTotal is not null ";
            }
            else if (ddlFPState.Text == "3")//未开票 (暂缓)
            {
                DiffDate += " and hadFpTotal is null ";
            }
            else if (ddlFPState.Text == "4")
            {
                FPState = " and POStatue3<>'已开票'";
            }

            if (ddlJiaoFu.Text == "1")
            {
                FPState = " and POStatue2='已交付'";
            }
            else if (ddlJiaoFu.Text == "2")
            {
                FPState = " and (POStatue2='' or POStatue2 is null) ";
            }


            if (ddlUser.Text == "-1")//显示所有用户
            {
                //var model = Session["userInfo"] as User;
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职') AND PONO=JXC_REPORT.PONO )", Session["currentUserId"]);
            }
            else if (ddlUser.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
                AE = string.Format(" and AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}')", model.LoginIPosition);
            }
            else
            {
                AE = string.Format(" and AppName={0} ", ddlUser.Text);
            }


            string poTimeWhere = "";
            if (txtPOTimeFrom.Text != "")
            {
                poTimeWhere += string.Format(" and PODate>='{0} 00:00:00'", txtPOTimeFrom.Text);
            }


            if (txtPOTimeTo.Text != "")
            {
                poTimeWhere += string.Format(" and PODate<='{0} 23:59:59'", txtPOTimeTo.Text);
            }

            string IsSpecial = "";
            if (cbIsSpecial.Checked)
            {
                IsSpecial = " and IsSpecial=0 ";
            }
            else
            {
                IsSpecial = " and IsSpecial=1 ";
            }



            string isFax = "";
            if (cbIsPoFax.Checked)
            {
                isFax = " and IsPoFax=1 ";
                if (dllFPstye.Text != "0")
                {
                    isFax += string.Format(" and FpType='{0}'", dllFPstye.SelectedItem.Text);
                }
            }
            else
            {
                isFax = " and IsPoFax=0 ";
            }

            string outTime = "";
            if (txtOutFrom.Text != "")
            {
                outTime += string.Format(" and ruTime>='{0} 00:00:00' ", txtOutFrom.Text);
            }


            if (txtOutTo.Text != "")
            {
                outTime += string.Format(" and ruTime<='{0} 23:59:59' ", txtOutTo.Text);
            }

            //string outCon = "";
            if (txtOutFrom.Text != "" || txtOutTo.Text != "")
            {
                DiffDate += " and outProNo is not null ";
            }
            string isClear = "";
            if (cbNoClear.Checked == false)
            {
                isClear = " and POStatue4='已结清'";
            }
            else
            {
                isClear = " and (POStatue4='' or POStatue4 is null) ";
            }
            string sql = string.Format(@"select tb1.PONo as '项目编号', tb1.AE as 'AE',tb1.GuestName as '客户名称',tb1.GoodNo as '商品编号',tb1.GoodName as '名称',
tb1.GoodSpec as '规格',GoodNum as '数量',avgLastPrice as '采购均价',OutProNo as '出库单号',tb1.avgSellPrice as '销售单价',
GoodSellPrice as '出库单价',RuTime as '出库时间',CG_POOrder.PODate as '订单时间', CG_POOrder.FPTotal as '发票编号'
,POTotal_View.POTotal as '项目金额',hadFpTotal as '开票金额',diffDate as '缺票天数' 

from (
select  CG_POOrder.PONo,CG_POOrder.AE,CG_POOrder.GuestName,CG_POOrders.GoodId,TB_Good.GoodNo, TB_Good.GoodName,
TB_Good.GoodSpec,avg(SellPrice) avgSellPrice,AppName  from CG_POOrder
left join CG_POOrders on CG_POOrder.id=CG_POOrders.id
left join TB_Good on TB_Good.GoodId=CG_POOrders.GoodId
where  TB_Good.GoodName is not null and Status='通过' " + pono + guestName + FPState + JiaoFu + AE + poTimeWhere + IsSpecial + isFax + isClear + @"  
group by CG_POOrder.PONo,CG_POOrder.AE,CG_POOrder.GuestName,TB_Good.GoodNo, TB_Good.GoodName,
TB_Good.GoodSpec,CG_POOrders.GoodId,AppName
)as tb1-- 项目基本信息汇总
left join POTotal_View on POTotal_View.PONo=tb1.PONo--查询 项目金额和 已开发票金额
left join CG_POOrder on CG_POOrder.PONo=tb1.PONo and CG_POOrder.IFZhui=0 and Status='通过' --找发票号和原始订单日期
left join 
(
select GoodNum,proNo as outProNo,PONo, RuTime,GooId ,GoodSellPrice, DATEDIFF(day,ruTime,getdate()) as diffDate,GoodPrice as avgLastPrice FROM Sell_OrderOutHouse 
left join Sell_OrderOutHouses on Sell_OrderOutHouse.Id=Sell_OrderOutHouses.id where Status='通过' " + outTime + @"
)
as tb3 on tb1.PONo=tb3.PONo and tb1.GoodId=tb3.GooId " + (DiffDate == "" ? "" : " where 1=1 " + DiffDate));
            System.Data.DataTable dt = DBHelp.getDataTable(sql);
            GridView gvOrders = new GridView();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=SellFPReport.xls");
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

        protected void cbIsPoFax_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsPoFax.Checked)
            {
                dllFPstye.Enabled = true;
            }
            else
            {
                dllFPstye.Enabled = false;
            }
        }

        protected void cbPOWCG_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPOWCG.Checked)
            {
                cbNumZero.Checked = false;
                cbNumZero.Enabled = false;

                cbCGWC.Checked = false;
                cbCGWC.Enabled = false;
            }
            else
            {
                cbCGWC.Enabled = true;
                cbNumZero.Enabled = true;
            }
        }

        protected void cbCGWC_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCGWC.Checked)
            {
                cbNumZero.Checked = false;
                cbNumZero.Enabled = false;

                cbPOWCG.Checked = false;
                cbPOWCG.Enabled = false;
            }
            else
            {
                cbPOWCG.Enabled = true;
                cbNumZero.Enabled = true;
            }

        }

        protected void cbNumZero_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNumZero.Checked)
            {
                cbPOWCG.Checked = false;
                cbPOWCG.Enabled = false;

                cbCGWC.Checked = false;
                cbCGWC.Enabled = false;
            }
            else
            {
                cbCGWC.Enabled = true;
                cbPOWCG.Enabled = true;
            }
        }
    }
}

