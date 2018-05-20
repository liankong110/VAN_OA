using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using VAN_OA.Model;
using System.Text;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class WFRuSellReport : BasePage
    {
        RuSellReportService _dal = new RuSellReportService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["currentUserId"] != null)
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

                    List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                    VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                    if (VAN_OA.JXC.SysObj.IfShowAll("入库未出清单", Session["currentUserId"], "ShowAll") == false)
                    {
                        ViewState["showAll"] = false;
                        var model = Session["userInfo"] as User;
                        user.Insert(0, model);
                    }
                    else
                    {
                        user = userSer.getAllUserByPOList();
                        user.Insert(0, new VAN_OA.Model.User() {LoginName = "全部", Id = -1});
                    }
                    ddlUser.DataSource = user;
                    ddlUser.DataBind();
                    ddlUser.DataTextField = "LoginName";
                    ddlUser.DataValueField = "Id";

                    List<RuSellReport> list = new List<RuSellReport>();
                    gvMain.DataSource = list;
                    gvMain.DataBind();


                    if (Request["PONo"] != null)
                    {
                        txtPONo.Text = Request["PONo"].ToString();
                        ddlPoType.Text = "2";
                        cbRuZero.Checked = true;
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

            string userId = "", goodNoWhere = "", guestWhere = "", ruTimeWhere = "", poTimeWhere = "", ponoWhere = "";

            string where = "";
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
                //ponoWhere = string.Format(" and CAI_OrderInHouse.PONo like '%{0}%'", txtPONo.Text);
                ponoWhere = string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (ddlPoType.Text=="0")
            {
                ponoWhere += " and PONo like 'P%'";
            }
            if (ddlPoType.Text == "1")
            {
                ponoWhere += " and PONo like 'KC%'";
            }
            
            if (!string.IsNullOrEmpty(txtPOName.Text.Trim()))
            {
                ponoWhere +=string.Format( " and PONAME like '%{0}%'",txtPOName.Text.Trim());
            }
            if (txtGuestName.Text.Trim() != "")
            {
                //guestWhere = string.Format(" and CG_POOrder.GuestName like '%{0}%'", txtGuestName.Text);
                guestWhere = string.Format(" and GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }
            if (txtGoodNo.Text != "")
            {
                goodNoWhere = string.Format(" and TB_Good.GoodNo like '%{0}%'", txtGoodNo.Text);
            }

            if (ddlUser.Text == "-1")//显示所有用户
            {
                //var model = Session["userInfo"] as User;
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职') AND PONO=JXC_REPORT.PONO )", Session["currentUserId"]);
            }
            //else if (ddlUser.Text == "0")//显示部门信息
            //{
            //    var model = Session["userInfo"] as User;
            //    AE = string.Format(" and AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}')", model.LoginIPosition);
            //}
            else
            {
                userId =  ddlUser.SelectedItem.Text;
            }

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('入库时间 格式错误！');</script>");
                    return;
                }
                ruTimeWhere += string.Format(" and minRuTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('入库时间 格式错误！');</script>");
                    return;
                }
                ruTimeWhere += string.Format(" and minRuTime<='{0} 23:59:59'", txtTo.Text);
            }


            if (txtPOTimeFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtPOTimeFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单时间 格式错误！');</script>");
                    return;
                }
                //poTimeWhere += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00'", txtPOTimeFrom.Text);
                poTimeWhere += string.Format(" and minPODate>='{0} 00:00:00'", txtPOTimeFrom.Text);
            }


            if (txtPOTimeTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtPOTimeTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单时间 格式错误！');</script>");
                    return;
                }
                //poTimeWhere += string.Format(" and CG_POOrder.PODate<='{0} 23:59:59'", txtPOTimeTo.Text);
                poTimeWhere += string.Format(" and minPODate<='{0} 23:59:59'", txtPOTimeTo.Text);
            }
           
           
            if (ddlWeiType.Text == "0")//未开具出库单
            {
                where += " and NoOutNum is null ";
            }
            else if (ddlWeiType.Text == "1")//出库单执行中
            {
                where += " and doingOutNum=0 ";
            }
            else if (ddlWeiType.Text == "2")//已出又销退
            {
                where += " and sellTuiNum>0";
            }
            else if (ddlWeiType.Text == "3")//出库单未通过
            {
                where += " and WeiOutNum=0 ";
            }
            if (cbZero.Checked)
            {
                where += " and LastNum>0 ";
            }
            if (cbRuZero.Checked)
            {
                where += " and ruChuNum>0";
            }
            if (ddlIsHanShui.Text != "-1")
            {
                where += string.Format(" and  IsHanShui={0} ", ddlIsHanShui.Text);
            }

            if (ddlCompany.Text != "-1")
            {
                string where1 = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                where += string.Format(" and AE IN(select loginName from tb_User where {0})", where1);
            }

            var list = _dal.GetListArray(ponoWhere, userId, goodNoWhere, guestWhere, ruTimeWhere, poTimeWhere, where);
            AspNetPager1.RecordCount = list.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            gvMain.DataSource = list;
            gvMain.DataBind();
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
                RuSellReport model = e.Row.DataItem as RuSellReport;
                System.Web.UI.WebControls.Label lblIsHanShui = e.Row.FindControl("lblIsHanShui") as System.Web.UI.WebControls.Label;
                if (lblIsHanShui != null)
                {
                    lblIsHanShui.Text = model.IsHanShui == 1 ? "含税" : "不含税";
                }
                if (model.IsHanShui == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string userId = "", goodNoWhere = "", guestWhere = "", ruTimeWhere = "", poTimeWhere = "", ponoWhere = "";

            string where = "";
            if (txtPONo.Text != "")
            {
                //ponoWhere = string.Format(" and CAI_OrderInHouse.PONo like '%{0}%'", txtPONo.Text);
                ponoWhere = string.Format(" and PONo like '%{0}%'", txtPONo.Text);
            }
            if (txtGuestName.Text != "")
            {
                //guestWhere = string.Format(" and CG_POOrder.GuestName like '%{0}%'", txtGuestName.Text);
                guestWhere = string.Format(" and GuestName like '%{0}%'", txtGuestName.Text);
            }
            if (txtGoodNo.Text != "")
            {
                goodNoWhere = string.Format(" and TB_Good.GoodNo like '%{0}%'", txtGoodNo.Text);
            }

            if (ddlUser.Text == "-1")//显示所有用户
            {
                //var model = Session["userInfo"] as User;
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职') AND PONO=JXC_REPORT.PONO )", Session["currentUserId"]);
            }
            //else if (ddlUser.Text == "0")//显示部门信息
            //{
            //    var model = Session["userInfo"] as User;
            //    AE = string.Format(" and AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}')", model.LoginIPosition);
            //}
            else
            {
                userId = ddlUser.Text;
            }

            if (txtFrom.Text != "")
            {
                ruTimeWhere += string.Format(" and minRuTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                ruTimeWhere += string.Format(" and minRuTime<='{0} 23:59:59'", txtTo.Text);
            }


            if (txtPOTimeFrom.Text != "")
            {
                //poTimeWhere += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00'", txtPOTimeFrom.Text);
                poTimeWhere += string.Format(" and minPODate>='{0} 00:00:00'", txtPOTimeFrom.Text);
            }


            if (txtPOTimeTo.Text != "")
            {
                //poTimeWhere += string.Format(" and CG_POOrder.PODate<='{0} 23:59:59'", txtPOTimeTo.Text);
                poTimeWhere += string.Format(" and minPODate<='{0} 23:59:59'", txtPOTimeTo.Text);
            }

            if (ddlWeiType.Text == "0")//未开具出库单
            {
                where += " and NoOutNum is null ";
            }
            else if (ddlWeiType.Text == "1")//出库单执行中
            {
                where += " and doingOutNum=0 ";
            }
            else if (ddlWeiType.Text == "2")//已出又销退
            {
                where += " and sellTuiNum>0";
            }
            else if (ddlWeiType.Text == "3")//出库单未通过
            {
                where += " and WeiOutNum=0 ";
            }

            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat(@"select PONo as '项目编号',AE as 'AE',GuestName AS '客户名称',GoodNo AS '商品编号',GoodName AS '名称',GoodSpec as '规格',
outNum '已出数量',LastNum as '项目需出',ruChuNum as '入库需出', GoodNum as '库存数量',GoodAvgPrice  as '库存均价',avgSellPrice  as '销售单价',minRuTime  as '入库时间',minPODate  as '订单时间'
from [NoSellOutGoods_1] left join TB_Good on TB_Good.GoodId=NoSellOutGoods_1.GooId
left join TB_HouseGoods on TB_HouseGoods.GoodId=NoSellOutGoods_1.GooId where 1=1 {0} {1} {2} {3} {4} {5} " + where,
                                                                                                   userId == "" ? "" : string.Format(" and AppName={0}", userId)
                                                                                                  , goodNoWhere,
                       guestWhere, ruTimeWhere, poTimeWhere, ponoWhere);
            System.Data.DataTable dt = DBHelp.getDataTable(strSql.ToString());
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
    }
}
