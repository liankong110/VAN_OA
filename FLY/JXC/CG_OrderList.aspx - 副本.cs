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
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.BaseInfo;
using System.Drawing;
using Newtonsoft.Json;

namespace VAN_OA.JXC
{
    public partial class CG_OrderList : BasePage
    {
        public string GetGestProInfo(object obj)
        {
            return VAN_OA.BaseInfo.GuestProBaseInfoList.GetGestProInfo(obj);
        }

        public string IsSpecial(object obj)
        {
            if (obj != null && Convert.ToBoolean(obj))
            {
                return "特";
            }
            return "";
        }

        public string GetStateType(object obj)
        {
            if (CG_POOrder.ConPOStatue5_1 == obj.ToString() || CG_POOrder.ConPOStatue5_1 == obj.ToString())
            {
                return "1";
            }
            return "0";
        }

        CG_POOrderService POSer = new CG_POOrderService();
        CG_POOrdersService ordersSer = new CG_POOrdersService();
        CG_POCaiService CaiSer = new CG_POCaiService();
        Sell_OrderInHousesService ordersSerTui = new Sell_OrderInHousesService();
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

                var GoodUnitList = new TB_GoodService().GetAllGoodUnits();
                ddlGoodUnit.Items.Add("全部");
                foreach (var unit in GoodUnitList)
                {
                    ddlGoodUnit.Items.Add(unit);
                }

                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

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

                //主单
                List<CG_POOrder> pOOrderList = new List<CG_POOrder>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //子单
                List<CG_POOrders> orders = new List<CG_POOrders>();
                gvList.DataSource = orders;
                gvList.DataBind();



                List<CG_POCai> caiList = new List<CG_POCai>();
                gvCai.DataSource = caiList;
                gvCai.DataBind();

                List<Sell_OrderInHouses> tuiOrders = new List<Sell_OrderInHouses>();
                gvTui.DataSource = tuiOrders;
                gvTui.DataBind();

                //if (VAN_OA.JXC.SysObj.IfShowAll(SysObj.CG_OrderList, Session["currentUserId"]) == false)
                //{
                //    ViewState["showAll"] = false;
                //}


                bool showAll = true;
                if (QuanXian_ShowAll(SysObj.CG_OrderList) == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }
                //                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='查看特殊客户'
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目订单列表') and sys_Object.AutoID is not null", Session["currentUserId"]);

                //                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                if (NewShowAll_textName("项目订单列表", "查看特殊客户"))
                {
                    ViewState["ScanSpecGuests"] = true;
                }
                else
                {
                    ViewState["ScanSpecGuests"] = false;
                }

                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll(SysObj.CG_OrderList, Session["currentUserId"], "WFScanDepartList") == true)
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


                List<TB_BasePoType> basePoTypeList = new TB_BasePoTypeService().GetListArray("");
                basePoTypeList.Insert(0, new TB_BasePoType { BasePoType = "全部", Id = -1 });
                ddlPOTyle.DataSource = basePoTypeList;
                ddlPOTyle.DataBind();
                ddlPOTyle.DataTextField = "BasePoType";
                ddlPOTyle.DataValueField = "Id";

                if (Request["PONo"] != null)
                {
                    txtPONo.Text = Request["PONo"].ToString();
                    Show();

                    GetData(Request["PONo"].ToString());
                }

            }
        }


        private void Show()
        {
            string sql = " 1=1 and IFZhui=0 ";

            if (ddlFPState.Text == "1")
            {
                sql += " and POStatue3='已开票'";
            }
            else if (ddlFPState.Text == "2")//未开全票
            {
                sql += " and exists(select PONO from POTotal_View where CG_POOrder.PONO=POTotal_View.PONO AND POTotal>hadFpTotal)";

            }
            else if (ddlFPState.Text == "3")//未开票 (暂缓)
            {
                sql += " and (exists(select PONO from POTotal_View where CG_POOrder.PONO=POTotal_View.PONO AND ISNULL(hadFpTotal,0)=0 ) or CG_POOrder.Status='执行中')";
            }

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and CG_POOrder.PONo like '%{0}%'", txtPONo.Text.Trim());
            }

            if (ddlGuestTypeList.SelectedValue != "全部")
            {
                sql += string.Format(" and GuestType='{0}'", ddlGuestTypeList.SelectedValue);
            }
            if (ddlGuestProList.SelectedValue != "-2")
            {
                sql += string.Format(" and GuestPro={0}", ddlGuestProList.SelectedValue);
            }
            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and CG_POOrder.POName like '%{0}%'", ttxPOName.Text.Trim());
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
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and PODate<='{0} 23:59:59'", txtTo.Text);
            }


            if (ddlStatue.Text != "通过+执行")
            {
                sql += string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }

            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and CG_POOrder.GuestName  like '%{0}%'", txtGuestName.Text.Trim());
            }

            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and Model='{0}'", ddlModel.Text);
            }
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

            if (CheckBox5.Checked)
            {
                sql += string.Format(" and POStatue5='{0}'", CheckBox5.Text);
            }

            if (CheckBox6.Checked)
            {
                sql += string.Format(" and POStatue6='{0}'", CheckBox6.Text);
            }
            if (ddlClose.Text != "-1")
            {
                sql += string.Format(" and IsClose={0} ", ddlClose.Text);
            }
            if (ddlIsSelect.Text != "-1")
            {
                sql += string.Format(" and IsSelected={0} ", ddlIsSelect.Text);
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                sql += string.Format(" and JieIsSelected={0} ", ddlJieIsSelected.Text);
            }
            if (ddlPOTyle.Text != "-1")
            {
                sql += string.Format(" and POType={0} ", ddlPOTyle.Text);
            }
            if (ddlZhangqi.Text != "-1")
            {
                //实际帐期=该项目最后一笔到款单日期-该项目第一笔出库单日期
                sql += string.Format(" and exists(select [ZhangQiView1].PONO from [ZhangQiView1] where CG_POOrder.PONo=[ZhangQiView1].PONo and [ZhangQiView1].ZhangQie{0}{1})", ddlZhangqi.Text, ViewState["KeyValue"]);
            }
            //if (ViewState["showAll"] != null)
            //{
            //    sql += string.Format(" and AppName={0}", Session["currentUserId"]);
            //}
            if (ddlUser.Text == "-1")//显示所有用户
            {
                sql += " and ( 1=1 ";
            }
            else if (ddlUser.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
                sql += string.Format(" and (AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}')", model.LoginIPosition);
            }
            else
            {
                //sql += string.Format(" and (AE='{0}' OR exists( select TB_GuestTrack.Id from TB_GuestTrack where TB_GuestTrack.IsSpecial=1 and CG_POOrder.GuestNo=TB_GuestTrack.GuestId {1}) )", ddlUser.SelectedItem.Text, strSql.ToString());
                sql += string.Format(" and (AE='{0}'", ddlUser.SelectedItem.Text);
            }

            if ((ddlUser.Text == "-1" || ddlUser.Items.Count == 1) && (bool)ViewState["ScanSpecGuests"])
            {
                var strSql = new System.Text.StringBuilder();
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                if (1 <= month && month <= 3)
                {
                    strSql.Append(string.Format(" and QuartNo='1' and YearNo='{0}' ", year));
                }
                else if (4 <= month && month <= 6)
                {
                    strSql.Append(string.Format(" and QuartNo='2' and YearNo='{0}' ", year));
                }
                else if (7 <= month && month <= 9)
                {
                    strSql.Append(string.Format(" and QuartNo='3' and YearNo='{0}' ", year));
                }
                else if (10 <= month && month <= 12)
                {
                    strSql.Append(string.Format(" and QuartNo='4' and YearNo='{0}' ", year));
                }

                sql += string.Format("  or  exists( select TB_GuestTrack.Id from TB_GuestTrack where TB_GuestTrack.IsSpecial=1 and CG_POOrder.GuestNo=TB_GuestTrack.GuestId {0}) ", strSql.ToString());
            }
            sql += " )";

            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and exists (select id from tb_User where {0} and CG_POOrder.AE=LoginName and IFZhui=0)", where);
            }

            if (ddlIsSpecial.Text != "-1")
            {
                sql += string.Format(" and IsSpecial={0} ", ddlIsSpecial.Text);
            }
            if (cbIsPoFax.Checked)
            {
                sql += string.Format(" and IsPoFax=0");
            }
            if (txtGoodNo.Text != "" || txtNameOrTypeOrSpec.Text != "" || txtNameOrTypeOrSpecTwo.Text != "")
            {
                string goodInfo = " and CG_POOrder.PONO IN (select PONO from CG_POOrders left join CG_POOrder on CG_POOrders.Id=CG_POOrder.Id left join TB_Good on CG_POOrders.GoodId=TB_Good.GoodId where 1=1 ";
                if (txtGoodNo.Text != "")
                {
                    goodInfo += string.Format(" and TB_Good.GoodNo like '%{0}%'", txtGoodNo.Text);
                }
                if (txtNameOrTypeOrSpec.Text != "" && txtNameOrTypeOrSpecTwo.Text != "")
                {
                    goodInfo += string.Format(" and ((GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%') or (GoodTypeSmName like '%{1}%' or GoodName  like '%{1}%' or GoodSpec like '%{1}%'))",
                       txtNameOrTypeOrSpec.Text, txtNameOrTypeOrSpecTwo.Text);
                }
                else if (txtNameOrTypeOrSpec.Text != "" || txtNameOrTypeOrSpecTwo.Text != "")
                {
                    var NameOrTypeOrSpec = "";
                    if (txtNameOrTypeOrSpec.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpec.Text;
                    if (txtNameOrTypeOrSpecTwo.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpecTwo.Text;

                    goodInfo += string.Format(" and (GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%')",
                       NameOrTypeOrSpec);
                }
                sql += goodInfo + ")";
            }

            List<CG_POOrder> pOOrderList = this.POSer.GetListArray(sql);

            var list = orderSer.GetOrder_ToInvoice_1(string.Format(" exists(select ID from CG_POOrder where CG_POOrder.PONo=Order_ToInvoice_1.PONo and {0})", sql));



            lblAllTotal.Text = (list.Sum(t => t.POTotal) - list.Sum(t => t.TuiTotal)).ToString();

            AspNetPager6.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager6.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

            //子单
            List<CG_POOrders> orders = new List<CG_POOrders>();
            gvList.DataSource = orders;
            gvList.DataBind();



            List<CG_POCai> caiList = new List<CG_POCai>();
            gvCai.DataSource = caiList;
            gvCai.DataBind();

            List<Sell_OrderInHouses> ordersTui = new List<Sell_OrderInHouses>();
            gvTui.DataSource = ordersTui;
            gvTui.DataBind();

            AspNetPager1.RecordCount = 0;
            AspNetPager2.RecordCount = 0;
            AspNetPager3.RecordCount = 0;
            AspNetPager4.RecordCount = 0;
            AspNetPager5.RecordCount = 0;

            lblTotal.Text = "0";

        }

        private int Value = 0;
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            var invSer = new BaseKeyValueService();
            var model = invSer.GetModel(1);
            if (model != null)
            {
                try
                {
                    Value = Convert.ToInt32(model.TypeValue);
                    ViewState["KeyValue"] = Value;
                }
                catch (Exception)
                {
                }
            }
            AspNetPager6.CurrentPageIndex = 1;
            Show();
        }

        protected void AspNetPager6_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMain.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            //this.gvList.PageIndex = e.NewPageIndex;
            //XiaoShouChange(AspNetPager1.CurrentPageIndex);

            gvList.PageIndex = AspNetPager1.CurrentPageIndex;
            gvAllPoRemark.PageIndex = AspNetPager1.CurrentPageIndex;
            gvCai.PageIndex = AspNetPager1.CurrentPageIndex;

            GetData();

            TabContainer1.ActiveTabIndex = 0;
            TabContainer1.TabIndex = 0;
        }


        private void XiaoShouChange(int pageIndex)
        {
            string sql = "";
            if (ddlStatue.Text != "通过+执行")
            {
                sql = string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }
            List<CG_POOrders> orders = ordersSer.GetListArrayToList(" 1=1 and CG_POOrder.PONo='" + ViewState["selPONo"] + "' " + sql);
            AspNetPager1.RecordCount = orders.Count;


            this.gvList.PageIndex = pageIndex - 1;
            this.gvAllPoRemark.PageIndex = pageIndex - 1;


            ViewState["Orders"] = orders;
            gvList.DataSource = orders;
            gvList.DataBind();


            gvAllPoRemark.DataSource = orders;
            gvAllPoRemark.DataBind();

            //采购信息====
            sql = "";
            if (ddlStatue.Text != "通过+执行")
            {
                sql = string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }
            List<CG_POCai> caiList = CaiSer.GetListArrayToList(" 1=1 and CG_POOrder.PONo='" + ViewState["selPONo"] + "'" + sql);

            this.gvCai.PageIndex = pageIndex - 1;
            gvCai.DataSource = caiList;
            gvCai.DataBind();

            //======
            AspNetPager2.RecordCount = caiList.Count;
            AspNetPager3.RecordCount = orders.Count;
            AspNetPager1.RecordCount = orders.Count;
            //if (TabContainer1.ActiveTabIndex == 0)
            //{

            this.AspNetPager3.CurrentPageIndex = pageIndex;
            //}
            //  if (TabContainer1.ActiveTabIndex == 2)

            //{
            this.AspNetPager1.CurrentPageIndex = pageIndex;
            //}
            this.AspNetPager2.CurrentPageIndex = pageIndex;

        }
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            string sql = "";
            if (ddlStatue.Text != "通过+执行")
            {
                sql = string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }
            List<CG_POOrders> orders = ordersSer.GetListArrayToList(" 1=1 and CG_POOrder.PONo='" + ViewState["selPONo"] + "' " + sql);

            ViewState["Orders"] = orders;
            gvList.DataSource = orders;
            gvList.DataBind();
            TabContainer1.ActiveTabIndex = 0;
            TabContainer1.TabIndex = 0;
        }

        protected void AspNetPager2_PageChanged(object src, EventArgs e)
        {
            gvList.PageIndex = AspNetPager2.CurrentPageIndex;
            gvAllPoRemark.PageIndex = AspNetPager2.CurrentPageIndex;
            gvCai.PageIndex = AspNetPager2.CurrentPageIndex;

            GetData();

            //XiaoShouChange(AspNetPager2.CurrentPageIndex);
            //RemarkChange();
            TabContainer1.ActiveTabIndex = 1;
            TabContainer1.TabIndex = 1;

            ////this.gvCai.PageIndex = e.NewPageIndex;
            //string sql = "";
            //if (ddlStatue.Text != "通过+执行")
            //{
            //    sql = string.Format(" and Status='{0}'", ddlStatue.Text);
            //}
            //else
            //{
            //    sql += string.Format(" and Status<>'不通过'");
            //}
            //List<CG_POCai> caiList = CaiSer.GetListArrayToList(" 1=1 and CG_POOrder.PONo='" + ViewState["selPONo"] + "'" + sql);
            //AspNetPager2.RecordCount = caiList.Count;
            //this.gvCai.PageIndex = AspNetPager2.CurrentPageIndex - 1;
            //gvCai.DataSource = caiList;
            //gvCai.DataBind();
            //TabContainer1.ActiveTabIndex = 1;
            //TabContainer1.TabIndex = 1;
        }

        protected void gvCai_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCai.PageIndex = e.NewPageIndex;
            string sql = "";
            if (ddlStatue.Text != "通过+执行")
            {
                sql = string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }
            List<CG_POCai> caiList = CaiSer.GetListArrayToList(" 1=1 and CG_POOrder.PONo='" + ViewState["selPONo"] + "'" + sql);
            gvCai.DataSource = caiList;
            gvCai.DataBind();
            TabContainer1.ActiveTabIndex = 1;
            TabContainer1.TabIndex = 1;
        }

        protected void AspNetPager4_PageChanged(object src, EventArgs e)
        {
            string sql = "";
            if (ddlStatue.Text != "通过+执行")
            {
                sql = string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }

            List<Sell_OrderInHouses> ordersTui = ordersSerTui.GetListArrayToList(" 1=1 and Sell_OrderInHouse.PONo='" + ViewState["selPONo"] + "' and status<>'不通过'");
            AspNetPager4.RecordCount = ordersTui.Count;
            this.gvTui.PageIndex = AspNetPager4.CurrentPageIndex - 1;

            gvTui.DataSource = ordersTui;
            gvTui.DataBind();
            TabContainer2.ActiveTabIndex = 0;
            TabContainer2.TabIndex = 0;
        }

        protected void gvTui_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvTui.PageIndex = e.NewPageIndex;
            string sql = "";
            if (ddlStatue.Text != "通过+执行")
            {
                sql = string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }

            List<Sell_OrderInHouses> ordersTui = ordersSerTui.GetListArrayToList(" 1=1 and Sell_OrderInHouse.PONo='" + ViewState["selPONo"] + "' and status<>'不通过'");
            gvTui.DataSource = ordersTui;
            gvTui.DataBind();
            TabContainer2.ActiveTabIndex = 0;
            TabContainer2.TabIndex = 0;
        }

        protected void AspNetPager5_PageChanged(object src, EventArgs e)
        {
            string sql = "";
            if (ddlStatue.Text != "通过+执行")
            {
                sql = string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }

            CAI_OrderOutHousesService caiOutHouseSER = new CAI_OrderOutHousesService();
            List<CAI_OrderOutHouses> CaiInHouse = caiOutHouseSER.GetListArrayToPoNo(" 1=1 and CAI_OrderOutHouse.PONo='" + ViewState["selPONo"] + "' and status<>'不通过'");
            AspNetPager5.RecordCount = CaiInHouse.Count;
            this.gvCaiOut.PageIndex = AspNetPager5.CurrentPageIndex - 1;

            gvCaiOut.DataSource = CaiInHouse;
            gvCaiOut.DataBind();
            TabContainer2.ActiveTabIndex = 1;
            TabContainer2.TabIndex = 1;
        }

        protected void gvCaiOut_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCaiOut.PageIndex = e.NewPageIndex;
            string sql = "";
            if (ddlStatue.Text != "通过+执行")
            {
                sql = string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }

            CAI_OrderOutHousesService caiOutHouseSER = new CAI_OrderOutHousesService();
            List<CAI_OrderOutHouses> CaiInHouse = caiOutHouseSER.GetListArrayToPoNo(" 1=1 and CAI_OrderOutHouse.PONo='" + ViewState["selPONo"] + "' and status<>'不通过'");
            gvCaiOut.DataSource = CaiInHouse;
            gvCaiOut.DataBind();
            TabContainer2.ActiveTabIndex = 1;
            TabContainer2.TabIndex = 1;
        }


        private void RemarkChange()
        {
            string sql = "";
            if (ddlStatue.Text != "通过+执行")
            {
                sql = string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }

            string allRemark = string.Format("select PORemark, ProNo from CG_POOrder where PONO='{0}' {1} order by ProNo desc",
               ViewState["selPONo"], sql);
            var dt = DBHelp.getDataTable(allRemark);
            AspNetPager3.RecordCount = dt.Rows.Count;
            this.gvAllPoRemark.PageIndex = AspNetPager3.CurrentPageIndex - 1;

            gvAllPoRemark.DataSource = dt;
            gvAllPoRemark.DataBind();
        }
        protected void AspNetPager3_PageChanged(object src, EventArgs e)
        {
            gvList.PageIndex = AspNetPager3.CurrentPageIndex;
            gvAllPoRemark.PageIndex = AspNetPager3.CurrentPageIndex;
            gvCai.PageIndex = AspNetPager3.CurrentPageIndex;

            GetData();
            //XiaoShouChange(AspNetPager3.CurrentPageIndex);
            //RemarkChange();
            TabContainer1.ActiveTabIndex = 2;
            TabContainer1.TabIndex = 2;
        }

        protected void gvAllPoRemark_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAllPoRemark.PageIndex = e.NewPageIndex;

            string sql = "";
            if (ddlStatue.Text != "通过+执行")
            {
                sql = string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }

            string allRemark = string.Format("select PORemark, ProNo from CG_POOrder where PONO='{0}' {1}",
               ViewState["selPONo"], sql);
            gvAllPoRemark.DataSource = DBHelp.getDataTable(allRemark);
            gvAllPoRemark.DataBind();

            TabContainer1.ActiveTabIndex = 2;
            TabContainer1.TabIndex = 2;
        }


        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                CG_POOrder model = e.Row.DataItem as CG_POOrder;
                if (model.POStatue3 == CG_POOrder.ConPOStatue3)
                    e.Row.BackColor = System.Drawing.Color.Red;



                if (model.IsSpecial)
                {
                    (e.Row.FindControl("lblSpecial") as Label).BackColor = System.Drawing.Color.Blue;
                }
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                GetData(e.CommandArgument.ToString());
            }
        }

        public void GetData(string pono)
        {
            string sql = "";
            if (ddlStatue.Text != "通过+执行")
            {
                sql = string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }

            //string allRemark =string.Format( "select PORemark, ProNo from CG_POOrder where PONO='{0}' {1}",
            //    e.CommandArgument,sql);

            //var dt = DBHelp.getDataTable(allRemark);


            GetTotal(pono);

            ViewState["selPONo"] = pono;
            List<CG_POOrders> orders = ordersSer.GetListArrayToList(" 1=1 and CG_POOrder.PONo='" + pono + "' " + sql);

            ViewState["Orders"] = orders;

            AspNetPager1.RecordCount = orders.Count;
            AspNetPager1.CurrentPageIndex = 1;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            gvList.DataSource = orders;
            gvList.DataBind();


            AspNetPager3.RecordCount = orders.Count;
            AspNetPager3.CurrentPageIndex = 1;
            this.gvAllPoRemark.PageIndex = AspNetPager3.CurrentPageIndex - 1;
            gvAllPoRemark.DataSource = orders;
            gvAllPoRemark.DataBind();


            List<CG_POCai> caiList = CaiSer.GetListArrayToList(" 1=1 and CG_POOrder.PONo='" + pono + "'" + sql);
            AspNetPager2.RecordCount = caiList.Count;
            AspNetPager2.CurrentPageIndex = 1;
            this.gvCai.PageIndex = AspNetPager2.CurrentPageIndex - 1;

            gvCai.DataSource = caiList;
            gvCai.DataBind();


            List<Sell_OrderInHouses> ordersTui = ordersSerTui.GetListArrayToList(" 1=1 and Sell_OrderInHouse.PONo='" + pono + "' and status<>'不通过'");
            AspNetPager4.RecordCount = ordersTui.Count;
            AspNetPager4.CurrentPageIndex = 1;
            this.gvTui.PageIndex = AspNetPager4.CurrentPageIndex - 1;
            gvTui.DataSource = ordersTui;
            gvTui.DataBind();

            CAI_OrderOutHousesService caiOutHouseSER = new CAI_OrderOutHousesService();
            List<CAI_OrderOutHouses> CaiInHouse = caiOutHouseSER.GetListArrayToPoNo(" 1=1 and CAI_OrderOutHouse.PONo='" + pono + "' and status<>'不通过'");
            AspNetPager5.RecordCount = CaiInHouse.Count;
            AspNetPager5.CurrentPageIndex = 1;
            this.gvCaiOut.PageIndex = AspNetPager5.CurrentPageIndex - 1;
            gvCaiOut.DataSource = CaiInHouse;
            gvCaiOut.DataBind();

            //这里增加一个表格来展示数据信息


            ViewState["PVReport"] = GetPVReport(pono);

            ViewState["PVChats"] = JsonConvert.SerializeObject(Chat(pono));

        }


        public List<PVReport> GetPVReport(string pono)
        {
            CG_POOrderService cG_POOrderService = new CG_POOrderService();
            var model = cG_POOrderService.GetModel(pono);
            List<PVReport> pVReports = new List<PVReport>();
            var jxc = new JXC_REPORTService().GetListArray_Total(string.Format(" and CG_POOrder.pono='{0}'", pono), "", "");
            if (jxc.Count > 0)
            {

                PVReport AC = new PVReport
                {
                    No = 1,
                    CurrentDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    TargetName = "AC(实际成本)",
                    StartDate = model.PODate.ToString("yyyy-MM-dd"),//开工日=项目建立的日期
                    PlanDate = model.PODate.AddDays(model.PlanDays).ToString("yyyy-MM-dd"),//计划完工日=开工日+计划完工天数；
                    Values = jxc[0].goodTotal,//截止到今天的 AC(实现的出库的总的成本价之和)
                    HadDays = (DateTime.Now - model.PODate).Days + 1,
                    PlanDays = model.PlanDays
                };
                pVReports.Add(AC);
                PVReport PV = new PVReport
                {
                    No = 2,
                    CurrentDate = AC.CurrentDate,
                    TargetName = "PV（计划预算）",
                    StartDate = AC.StartDate,
                    PlanDate = AC.PlanDate,
                    HadDays = AC.HadDays,
                    PlanDays = AC.PlanDays,
                    //PV (PV=（今天-项目建立日期）*每天平均PV; 项目总价/计划完工天数= 每天平均PV) 
                    Values = model.PlanDays > 0 ? (jxc[0].SumPOTotal / model.PlanDays * ((DateTime.Now - model.PODate).Days+1)) : 0
                };
                pVReports.Add(PV);
                PVReport EV = new PVReport
                {
                    No = 3,
                    CurrentDate = AC.CurrentDate,
                    TargetName = "EV（实际预算）",
                    StartDate = AC.StartDate,
                    PlanDate = AC.PlanDate,
                    HadDays = AC.HadDays,
                    PlanDays = AC.PlanDays,
                    //EV(实现总的销售额=实现的出库的总的销售额)
                    Values = jxc[0].goodSellTotal
                };
                pVReports.Add(EV);
                PVReport CV = new PVReport
                {
                    No = 4,
                    CurrentDate = AC.CurrentDate,
                    TargetName = "CV成本偏差(EV-AC)",
                    StartDate = AC.StartDate,
                    PlanDate = AC.PlanDate,
                    HadDays = AC.HadDays,
                    PlanDays = AC.PlanDays,
                    //CV成本偏差(EV-AC)
                    Values = EV.Values - AC.Values
                };
                pVReports.Add(CV);
                PVReport SV = new PVReport
                {
                    No = 5,
                    CurrentDate = AC.CurrentDate,
                    TargetName = "SV进度偏差(EV-PV)",
                    StartDate = AC.StartDate,
                    PlanDate = AC.PlanDate,
                    HadDays = AC.HadDays,
                    PlanDays = AC.PlanDays,
                    //SV进度偏差(EV-PV)
                    Values = EV.Values - PV.Values
                };
                pVReports.Add(SV);
                PVReport CPI = new PVReport
                {
                    No = 6,
                    CurrentDate = AC.CurrentDate,
                    TargetName = "CPI成本效能指数(EV/AC)",
                    StartDate = AC.StartDate,
                    PlanDate = AC.PlanDate,
                    HadDays = AC.HadDays,
                    PlanDays = AC.PlanDays,
                    //CPI成本效能指数(EV/AC)
                    Values = AC.Values > 0 ? (EV.Values / AC.Values) : 0
                };
                pVReports.Add(CPI);
                PVReport SPI = new PVReport
                {
                    No = 7,
                    CurrentDate = AC.CurrentDate,
                    TargetName = "SPI进度效率指数(EV / PV)",
                    StartDate = AC.StartDate,
                    PlanDate = AC.PlanDate,
                    HadDays = AC.HadDays,
                    PlanDays = AC.PlanDays,
                    //SPI进度效率指数(EV / PV)
                    Values = PV.Values > 0 ? (EV.Values / PV.Values) : 0
                };
                pVReports.Add(SPI);
                PVReport BAC = new PVReport
                {
                    No = 8,
                    CurrentDate = AC.CurrentDate,
                    TargetName = "BAC计划总额",
                    StartDate = AC.StartDate,
                    PlanDate = AC.PlanDate,
                    HadDays = AC.HadDays,
                    PlanDays = AC.PlanDays,
                    //BAC=项目金额
                    Values = jxc[0].SumPOTotal
                };
                pVReports.Add(BAC);
                PVReport ETC = new PVReport
                {
                    No = 9,
                    CurrentDate = AC.CurrentDate,
                    TargetName = "ETC完工尚需成本",
                    StartDate = AC.StartDate,
                    PlanDate = AC.PlanDate,
                    HadDays = AC.HadDays,
                    PlanDays = AC.PlanDays,
                    //ETC=（BAC-EV）
                    Values = BAC.Values - EV.Values
                };
                pVReports.Add(ETC);
                PVReport EAC = new PVReport
                {
                    No = 10,
                    CurrentDate = AC.CurrentDate,
                    TargetName = "EAC完工估算",
                    StartDate = AC.StartDate,
                    PlanDate = AC.PlanDate,
                    HadDays = AC.HadDays,
                    PlanDays = AC.PlanDays,
                    //EAC= ETC+AC; 
                    Values = ETC.Values + AC.Values
                };
                pVReports.Add(EAC);
                string remark = "";
                string cuoshi = "";
                GetRemark(AC.Values, PV.Values, EV.Values, out remark, out cuoshi);
                foreach (var m in pVReports)
                {
                    m.CuoShi = cuoshi;
                    m.Remark = remark;
                }
            }

            return pVReports;
        }

        /// <summary>
        /// 图表
        /// </summary>
        public List<PVChat> Chat(string pono)
        {
            List<PVChat> series = new List<PVChat>();
            var poDetail = POSer.GetPOOrderDetailList(string.Format(" and PONo='{0}'", pono));
            if (poDetail.Count > 0)
            {
                var model = poDetail[0];
                if (model.PlanDays > 0)
                {
                    string sql = string.Format("  JXC_REPORT.PONo='{0}'", pono);
                    List<JXC_REPORT> pOOrderList = new JXC_REPORTService().GetListArray(sql);


                    PVChat pV = new PVChat();
                    pV.name = "PV";
                    pV.type = "line";

                    PVChat aC = new PVChat();
                    aC.name = "AC";
                    aC.type = "line";

                    PVChat eV = new PVChat();
                    eV.name = "EV";
                    eV.type = "line";
                    var tuiDeltail = pOOrderList.FindAll(t => t.PoType == "2");//销售退货 明细
                    for (int i = 0; i < model.PlanDays; i++)
                    {
                        if (model.PODate.AddDays(i) > DateTime.Now)
                        {
                            var sum = 0;
                            var sumTui = 0;
                            //PV
                            List<decimal?> pvPoint = new List<decimal?>();
                            pvPoint.Add(i + 1);//X
                            pvPoint.Add(null);
                            pV.data.Add(pvPoint);
                            //AC
                            List<decimal?> acPoint = new List<decimal?>();
                            acPoint.Add(i + 1);//X
                            acPoint.Add(null);//AC 截止到今天的 AC(实现的出库的总的成本价之和)
                            aC.data.Add(acPoint);

                            //EV (实现总的销售额=实现的出库的总的销售额)
                            List<decimal?> evPoint = new List<decimal?>();
                            evPoint.Add(i + 1);//X
                            evPoint.Add(null);//(实现总的销售额=实现的出库的总的销售额)
                            eV.data.Add(evPoint);

                        }
                        else
                        {
                            var sum = poDetail.FindAll(t => t.PODate <= model.PODate.AddDays(i)).Sum(t => t.POTotal);
                            var sumTui = tuiDeltail.FindAll(t => t.RuTime <= model.PODate.AddDays(i)).Sum(t => t.goodSellTotal).Value;
                            //PV
                            List<decimal?> pvPoint = new List<decimal?>();
                            pvPoint.Add(i + 1);//X
                            if (model.PlanDays != 0)
                            {
                                pvPoint.Add((sum - sumTui) / model.PlanDays * (i + 1));//Y (PV=（今天-项目建立日期）*每天平均PV; 项目总价/计划完工天数= 每天平均PV) 
                            }
                            else
                            {
                                pvPoint.Add(0);
                            }
                            pV.data.Add(pvPoint);
                            //AC
                            List<decimal?> acPoint = new List<decimal?>();
                            acPoint.Add(i + 1);//X
                            acPoint.Add(pOOrderList.FindAll(t => t.RuTime <= model.PODate.AddDays(i)).Sum(t => t.goodTotal).Value);//AC 截止到今天的 AC(实现的出库的总的成本价之和)
                            aC.data.Add(acPoint);

                            //EV (实现总的销售额=实现的出库的总的销售额)
                            List<decimal?> evPoint = new List<decimal?>();
                            evPoint.Add(i + 1);//X
                            evPoint.Add(pOOrderList.FindAll(t => t.RuTime <= model.PODate.AddDays(i)).Sum(t => t.goodSellTotal).Value);//(实现总的销售额=实现的出库的总的销售额)
                            eV.data.Add(evPoint);
                        }
                    }

                    series.Add(pV);
                    series.Add(aC);
                    series.Add(eV);


                    var halfDays = Convert.ToInt32(string.Format("{0:n0}", model.PlanDays / 2.0));
                    PVChat sV = new PVChat();
                    sV.name = "SV";
                    sV.type = "bar";
                    sV.barHeight = "10";
                    //4.PV 和EV 的差额 就是进度偏差 SV （对同一测量时间） ，图上显示，你按1/2工期这个时间点来取值和显示 进度偏差SV
                    if (pV.data.Count >=halfDays)
                    {
                        sV.data.Add(new List<decimal?> { halfDays, pV.data[halfDays - 1][1] - eV.data[halfDays - 1][1] });
                    }
                    PVChat cV = new PVChat();
                    cV.name = "CV";
                    cV.type = "bar";
                    cV.barHeight = "10";
                    //5.实际成本AC和挣值EV 的差额 就是成本偏差 CV （对同一测量时间） ，图上显示，你按1/2工期这个时间点来取值和显示成本偏差CV
                    if (aC.data.Count >= halfDays)
                    {
                        cV.data.Add(new List<decimal?> { halfDays, aC.data[halfDays - 1][1] - eV.data[halfDays - 1][1] });
                    }
                    series.Add(sV);
                    series.Add(cV);
                }
            }
            return series;
        }


        public void GetRemark(decimal ac, decimal pv, decimal ev, out string remark, out string cuoshi)
        {
            remark = "";
            cuoshi = "";
            if (ac > pv && pv > ev)
            {
                remark = "偏差指标：CV=EV-AC<0,说明投资超前;SV=EV-PV<0,说明进度拖延;";
                remark += "绩效指标：CPI=EV/AC<1,说明资金使用效率低;SPI=EV/PV<1,说明进度效率低;";
                cuoshi = "加大成本监控，并行工作，提高工作效率";
                return;
            }
            if (pv > ac && ac >= ev)
            {
                remark = "偏差指标：CV=EV-AC<=0,说明成本支出适当;SV=EV-PV<0,说明进度拖延;";
                remark += "绩效指标：CPI=EV/AC<=1,说明资金使用效率一般;SPI=EV/PV<1,说明进度效率低;";
                cuoshi = "加大成本投入来提高进度效率;赶工，并行工作以追赶进度;增加高效人员投放";
                return;
            }

            if (ac >= ev && ev > pv)
            {
                remark = "偏差指标：CV=EV-AC<=0,说明成本支出适当;SV=EV-PV>0,说明进度提前;";
                remark += "绩效指标：CPI=EV/AC<=1,说明资金使用效率一般;SPI=EV/PV>1,说明进度效率高;";
                cuoshi = "加大成本投入来进一步提高整体效率，加强人员培训和质量控制;";
                return;
            }
            if (ev > pv && pv > ac)
            {
                remark = "偏差指标：CV=EV-AC>0,说明资金投入延后;SV=EV-PV>0,说明进度提前;";
                remark += "绩效指标：CPI=EV/AC>1,说明资金使用效率高;SPI=EV/PV>1,说明进度效率高;";
                cuoshi = "加强质量控制，密切监控项目;";
                return;
            }
        }
        private void setValue(Label control, string value)
        {
            control.Text = value;
        }

        protected object ConvertToObj(object obj)
        {
            return obj;
            //if (obj != null) return string.Format("{0:f2}", Convert.ToDecimal(obj));
            //return 0;
        }
        protected object ConvertToObj1(object obj)
        {
            if (obj != null) return string.Format("{0:f2}", Convert.ToDecimal(obj));
            return 0;
        }

        protected void gvAllPoRemark_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }
        CG_POCai SumPOCai = new CG_POCai();
        decimal IniProfit = 0;
        decimal SumSellTotal = 0;
        protected void gvCai_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                CG_POCai model = e.Row.DataItem as CG_POCai;
                //获取商品的销售信息
                List<CG_POOrders> POOrders = ViewState["Orders"] as List<CG_POOrders>;
                if (POOrders != null)
                {
                    model.SellPrice = POOrders.Find(p => p.GoodId == model.GoodId).SellPrice;
                    (e.Row.FindControl("lblSellPrice") as Label).Text = model.SellPrice.ToString();
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

                //小计1，小计2，小计3 ，我想 还是在每一行的 最便宜的小计的 询价和小计 这两列 帮我 背景变成 淡蓝色 ，这样就能看清楚价格优势
                List<decimal?> totals = new List<decimal?>();
                totals.Add(model.Total1);
                totals.Add(model.Total2);
                totals.Add(model.Total3);
                var minTotal = totals.Min();

                if (model.Total1 == minTotal)
                {
                    model.CheapPrice = model.SupperPrice ?? 0;// (minTotal ?? 0);
                                                              //(e.Row.FindControl("lblSupperPrice") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                                                              //(e.Row.FindControl("lblTotal1") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[9].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[10].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                }
                else if (model.Total2 == minTotal)
                {
                    model.CheapPrice = model.SupperPrice1 ?? 0;
                    //(e.Row.FindControl("lblSupperPrice1") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    //(e.Row.FindControl("lblTotal2") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[12].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[13].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                }
                else if (model.Total3 == minTotal)
                {
                    model.CheapPrice = model.SupperPrice2 ?? 0;
                    //(e.Row.FindControl("lblSupperPrice2") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    //(e.Row.FindControl("lblTotal3") as Label).BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[15].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                    e.Row.Cells[16].BackColor = ColorTranslator.FromHtml("#D7E8FF");
                }

                //利润%放在最右面背景为淡绿色=初步利润/（销售单价*数量）
                Label lblCaiLiRun = e.Row.FindControl("lblCaiLiRun") as Label;
                if (lblCaiLiRun != null && model.SellPrice != 0)
                {
                    lblCaiLiRun.Text = string.Format("{0:n2}", (model.IniProfit / (model.SellPrice * model.Num) * 100));
                }
                else
                {
                    lblCaiLiRun.Text = "0";
                }

                IniProfit = IniProfit + (model.IniProfit ?? 0);
                SumSellTotal = SumSellTotal + (model.Num ?? 0) * model.SellPrice;

                setValue(e.Row.FindControl("lblIniProfit") as Label, NumHelp.FormatFour(model.IniProfit).ToString());//数量

            }


            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblNum") as Label, SumPOCai.Num == null ? "" : SumPOCai.Num.ToString());//数量
                setValue(e.Row.FindControl("lblTotal1") as Label, NumHelp.FormatFour(SumPOCai.Total1 == null ? 0 : SumPOCai.Total1));//小计1
                setValue(e.Row.FindControl("lblTotal2") as Label, NumHelp.FormatFour(SumPOCai.Total2 == null ? 0 : SumPOCai.Total2));//小计2
                setValue(e.Row.FindControl("lblTotal3") as Label, NumHelp.FormatFour(SumPOCai.Total3 == null ? 0 : SumPOCai.Total3));//小计3

                setValue(e.Row.FindControl("lblIniProfit") as Label, NumHelp.FormatFour(IniProfit).ToString());//数量

                if (SumSellTotal != 0)
                {
                    setValue(e.Row.FindControl("lblCaiLiRun") as Label, NumHelp.FormatFour(IniProfit / SumSellTotal * 100).ToString());//数量
                }
                else
                {
                    setValue(e.Row.FindControl("lblCaiLiRun") as Label, NumHelp.FormatFour(0).ToString());//数量
                }

                //setValue(e.Row.FindControl("lblNum") as Label, SumPOCai.Num == null ? "" : SumPOCai.Num.ToString());//数量
                //setValue(e.Row.FindControl("lblTotal1") as Label,NumHelp.FormatFour( SumPOCai.Total1 == null ? 0 : SumPOCai.Total1));//小计1
                //setValue(e.Row.FindControl("lblTotal2") as Label, NumHelp.FormatFour(SumPOCai.Total2 == null ? 0 : SumPOCai.Total2));//小计2
                //setValue(e.Row.FindControl("lblTotal3") as Label, NumHelp.FormatFour(SumPOCai.Total3 == null ? 0 : SumPOCai.Total3));//小计3


                //List<decimal> totalMax = new List<decimal>();
                //if (SumPOCai.Total1 != null)
                //{
                //    totalMax.Add(SumPOCai.Total1.Value);
                //}

                //if (SumPOCai.Total2 != null)
                //{
                //    totalMax.Add(SumPOCai.Total2.Value);
                //}

                //if (SumPOCai.Total3 != null)
                //{
                //    totalMax.Add(SumPOCai.Total3.Value);
                //}
                //if (totalMax.Count > 0)
                //{
                //    decimal minPrice = totalMax.Min();
                //    decimal lirun = 0;
                //    decimal sellTotal = 0;
                //    decimal otherCost = 0;
                //    List<CG_POOrders> POOrders = ViewState["Orders"] as List<CG_POOrders>;
                //    foreach (var model in POOrders)
                //    {
                //        sellTotal += model.SellTotal;
                //        otherCost += model.OtherCost;
                //    }

                //    if (sellTotal != 0)
                //    {
                //        lirun = ((sellTotal - minPrice - otherCost) / sellTotal) * 100;
                //    }

                //    else
                //    {
                //        decimal yiLiTotal = sellTotal - minPrice - otherCost;

                //        if (yiLiTotal != 0)
                //        {
                //            lirun = -100;
                //        }
                //    }
                //    setValue(e.Row.FindControl("lblCaiLiRun") as Label, ConvertToObj1(lirun).ToString());//数量
                //}
            }
        }


        CG_POOrders SumOrders = new CG_POOrders();

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                CG_POOrders model = e.Row.DataItem as CG_POOrders;
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
                //setValue(e.Row.FindControl("lblCostPrice") as Label, ConvertToObj(SumOrders.Num != 0 ? SumOrders.CostTotal / SumOrders.Num : 0).ToString());//成本单价

                // e.Row.Cells[8].Text = SumOrders.CostTotal.ToString();//成本总额
                setValue(e.Row.FindControl("lblCostTotal") as Label, NumHelp.FormatTwo(SumOrders.CostTotal));//成本总额


                // e.Row.Cells[9].Text = ConvertToObj(SumOrders.Num != 0 ? SumOrders.SellTotal / SumOrders.Num : 0).ToString();//销售单价
                //setValue(e.Row.FindControl("lblSellPrice") as Label, ConvertToObj(SumOrders.Num != 0 ? SumOrders.SellTotal / SumOrders.Num : 0).ToString());//销售单价


                //e.Row.Cells[10].Text = SumOrders.SellTotal.ToString();//销售总额
                setValue(e.Row.FindControl("lblSellTotal") as Label, NumHelp.FormatTwo(SumOrders.SellTotal));//销售总额


                //e.Row.Cells[11].Text = SumOrders.OtherCost.ToString();//管理费
                setValue(e.Row.FindControl("lblOtherCost") as Label, SumOrders.OtherCost.ToString());//管理费


                // e.Row.Cells[12].Text = SumOrders.YiLiTotal.ToString();//管理费
                setValue(e.Row.FindControl("lblYiLiTotal") as Label, NumHelp.FormatTwo(SumOrders.YiLiTotal));//盈利总额

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

                setValue(e.Row.FindControl("lblProfit") as Label, NumHelp.FormatFour(SumOrders.Profit));//利润


            }

        }
        Sell_OrderInHouses SumOrdersTui = new Sell_OrderInHouses();
        protected void gvTui_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                Sell_OrderInHouses model = e.Row.DataItem as Sell_OrderInHouses;

                SumOrdersTui.Total += model.Total;
                SumOrdersTui.GoodSellPriceTotal += model.GoodSellPriceTotal;
            }

            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblGoodName") as Label, "合计");//合计                      
                setValue(e.Row.FindControl("lblTotal") as Label, NumHelp.FormatFour(SumOrdersTui.Total));//成本总额    
                setValue(e.Row.FindControl("lblTotal1") as Label, NumHelp.FormatFour(SumOrdersTui.GoodSellPriceTotal));//成本总额    
            }

        }
        CG_POOrderService orderSer = new CG_POOrderService();
        private void GetTotal(string poNo)
        {
            var list = orderSer.GetOrder_ToInvoice_1(string.Format(" PONo='{0}'", poNo));
            if (list.Count > 0)
            {
                lblTotal.Text = (list[0].POTotal - list[0].TuiTotal).ToString();
            }
        }

        CAI_OrderInHouses CaiSumOrders = new CAI_OrderInHouses();
        protected void gvCaiOut_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                CAI_OrderOutHouses model = e.Row.DataItem as CAI_OrderOutHouses;
                CaiSumOrders.Total += model.Total;

            }

            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblGoodName") as Label, "合计");//合计

                setValue(e.Row.FindControl("lblTotal") as Label, NumHelp.FormatFour(CaiSumOrders.Total));//成本总额    
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            AspNetPager2.CurrentPageIndex = 1;
            AspNetPager3.CurrentPageIndex = 1;

            GetData();
        }
        public void GetData()
        {
            if (ViewState["selPONo"] == null)
            {
                return;
            }
            string pono = ViewState["selPONo"].ToString();
            string sql = "";
            if (ddlStatue.Text != "通过+执行")
            {
                sql = string.Format(" and Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Status<>'不通过'");
            }

            if (txtGoodNo1.Text != "" || txtNameOrTypeOrSpec1.Text != "" || txtNameOrTypeOrSpecTwo1.Text != "" || ddlGoodUnit.Text != "全部")
            {
                string goodInfo = "";
                if (txtGoodNo1.Text != "")
                {
                    goodInfo += string.Format(" and TB_Good.GoodNo like '%{0}%'", txtGoodNo1.Text);
                }
                if (txtNameOrTypeOrSpec1.Text != "" && txtNameOrTypeOrSpecTwo1.Text != "")
                {
                    goodInfo += string.Format(" and ((GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%') or (GoodTypeSmName like '%{1}%' or GoodName  like '%{1}%' or GoodSpec like '%{1}%'))",
                       txtNameOrTypeOrSpec1.Text, txtNameOrTypeOrSpecTwo1.Text);
                }
                else if (txtNameOrTypeOrSpec1.Text != "" || txtNameOrTypeOrSpecTwo1.Text != "")
                {
                    var NameOrTypeOrSpec = "";
                    if (txtNameOrTypeOrSpec1.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpec1.Text;
                    if (txtNameOrTypeOrSpecTwo1.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpecTwo1.Text;

                    goodInfo += string.Format(" and (GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%')",
                       NameOrTypeOrSpec);
                }
                if (ddlGoodUnit.Text != "全部")
                {
                    goodInfo += string.Format(" and GoodUnit='{0}'", ddlGoodUnit.Text);
                }
                sql += goodInfo;
            }


            if (!string.IsNullOrEmpty(txtGoodNum.Text))
            {
                sql += string.Format(" and Num{0}{1}", ddlFuHao.Text, txtGoodNum.Text);
            }
            string chenben = "";
            string caiChenben = "";
            if (!string.IsNullOrEmpty(txtChenBen.Text))
            {
                chenben = string.Format(" and CostPrice{0}{1}", ddlFuHao1.Text, txtChenBen.Text);
                caiChenben = string.Format(" and (SupperPrice{0}{1} or SupperPrice1{0}{1} or SupperPrice2{0}{1})", ddlFuHao1.Text, txtChenBen.Text);
            }
            ViewState["selPONo"] = pono;

            if (!string.IsNullOrEmpty(txtRemark.Text))
            {
                sql += string.Format(" and CG_POOrder.PORemark like '%{0}%'", txtRemark.Text);
            }
            List<CG_POOrders> orders = ordersSer.GetListArrayToList(" 1=1 and CG_POOrder.PONo='" + pono + "' " + sql + chenben);

            ViewState["Orders"] = orders;

            AspNetPager1.RecordCount = orders.Count;
            //AspNetPager1.CurrentPageIndex = 1;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            gvList.DataSource = orders;
            gvList.DataBind();


            AspNetPager3.RecordCount = orders.Count;
            //AspNetPager3.CurrentPageIndex = 1;
            this.gvAllPoRemark.PageIndex = AspNetPager3.CurrentPageIndex - 1;
            gvAllPoRemark.DataSource = orders;
            gvAllPoRemark.DataBind();


            List<CG_POCai> caiList = CaiSer.GetListArrayToList(" 1=1 and CG_POOrder.PONo='" + pono + "'" + sql + caiChenben);
            AspNetPager2.RecordCount = caiList.Count;
            //AspNetPager2.CurrentPageIndex = 1;
            this.gvCai.PageIndex = AspNetPager2.CurrentPageIndex - 1;

            gvCai.DataSource = caiList;
            gvCai.DataBind();
        }
    }
}
