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

namespace VAN_OA.JXC
{
    public partial class WFUpdatePOName : BasePage
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
                //主单
                List<CG_POOrder> pOOrderList = new List<CG_POOrder>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();


                bool showAll = true;
                if (QuanXian_ShowAll(SysObj.CG_OrderList) == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }

                if (Request["PONo"] != null)
                {
                    txtPONo.Text = Request["PONo"].ToString();
                    Show();
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
                    user = userSer.getAllUserByLoginName("");
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

            }
        }


        private void Show()
        {
            string sql = " 1=1 and IFZhui=0 ";

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
                sql += string.Format(" and PODate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and PODate<='{0} 23:59:59'", txtTo.Text);
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
                sql += string.Format(" and CG_POOrder.GuestName  like '%{0}%'", txtGuestName.Text.Trim());
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

            //if (ViewState["showAll"] != null)
            //{
            //    sql += string.Format(" and AppName={0}", Session["currentUserId"]);
            //}


            if (ddlUser.Text == "-1")//显示所有用户
            {
                //var model = Session["userInfo"] as User;
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职') AND PONO=JXC_REPORT.PONO )", Session["currentUserId"]);
            }
            else if (ddlUser.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
                sql += string.Format(" and AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}')", model.LoginIPosition);
            }
            else
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

                sql += string.Format(" and (AE='{0}' OR exists( select TB_GuestTrack.Id from TB_GuestTrack where TB_GuestTrack.IsSpecial=1 and CG_POOrder.GuestNo=TB_GuestTrack.GuestId {1}) )", ddlUser.SelectedItem.Text, strSql.ToString());
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
                string goodInfo = " and exists (select Ids from CG_POOrders left join TB_Good on CG_POOrders.GoodId=TB_Good.GoodId where CG_POOrders.id=CG_POOrder.id ";
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



            AspNetPager6.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager6.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
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
                CG_POOrder model = new CG_POOrderService().GetModel(Convert.ToInt32(e.CommandArgument));
                if (model != null)
                {
                    txtPONo.Text = model.PONo;
                    ttxPOName.Text = model.POName;
                }
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sql = string.Format(@" 
update CAI_OrderOutHouse  set POName='{1}' where POName='{0}';
--21    采购订单检验
update CAI_OrderChecks  set POName='{1}' where POName='{0}'
--20    采购订单
update CAI_POOrder  set POName='{1}' where POName='{0}'
--19    项目订单
update CG_POOrder  set POName='{1}' where POName='{0}'
--26    销售发票
update Sell_OrderFP  set POName='{1}' where POName='{0}'
--29    发票单签回
update Sell_OrderFPBack  set POName='{1}' where POName='{0}'
--25    销售退货
update Sell_OrderInHouse  set POName='{1}' where POName='{0}'
--23    销售出库
update Sell_OrderOutHouse  set POName='{1}' where POName='{0}'
--28    出库单签回
update Sell_OrderOutHouseBack  set POName='{1}' where POName='{0}'
--27    到款单
update TB_ToInvoice set POName='{1}' where POName='{0}'
--申请请款单
update tb_FundsUse set POName='{1}' where POName='{0}'
--预期报销单
update Tb_DispatchList set POName='{1}' where POName='{0}'
--预期报销单(油费报销)
update Tb_DispatchList set POName='{1}' where POName='{0}'
update CAI_POOrder   set POName='{1}' where POName='{0}'
update CAI_OrderInHouse   set POName='{1}' where POName='{0}'
--私车公用申请单
update  tb_UseCar set POGuestName='{1}' where POGuestName='{0}'
--用车明细表
update TB_UseCarDetail set POName='{1}' where POName='{0}'
--加班单
update tb_OverTime set POName='{1}' where POName='{0}'
--公交车费用
update TB_BusCardUse set POName='{1}' where POName='{0}'
update tb_Post set POName='{1}' where POName='{0}'", ttxPOName.Text, txtNewPOName.Text);
            var result = DBHelp.ExeCommand(sql);
            if (result)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('更新成功！');</script>");

            }
            else
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('更新失败！');</script>");

            }
        }











    }
}
