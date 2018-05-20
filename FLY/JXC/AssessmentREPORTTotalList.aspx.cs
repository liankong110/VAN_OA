using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
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
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class AssessmentREPORTTotalList : BasePage
    {
        JXC_REPORTService POSer = new JXC_REPORTService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartTime.Text = (DateTime.Now.Year - 1) + "-1-1";
                List<TB_BasePoType> basePoTypeList = new TB_BasePoTypeService().GetListArray("");
                basePoTypeList.Add(new TB_BasePoType { BasePoType = "全部", Id = -1 });
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

                //主单
                List<JXC_REPORTTotal> pOOrderList = new List<JXC_REPORTTotal>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //if (VAN_OA.JXC.SysObj.IfShowAll(SysObj.JXC_REPORTTotalList, Session["currentUserId"]) == false)
                //{
                //    ViewState["showAll"] = false;
                //}


                bool showAll = true;
                if (QuanXian_ShowAll(SysObj.JXC_REPORTTotalList) == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }



                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll(SysObj.JXC_REPORTTotalList, Session["currentUserId"], "WFScanDepartList") == true)
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

                //                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='可导出'
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='销售报表汇总') and sys_Object.AutoID is not null", Session["currentUserId"]);
                //                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                if (NewShowAll_textName("销售报表汇总", "可导出"))
                {
                    btnExcel.Visible = true;
                }
                else
                {
                    btnExcel.Visible = false;
                }

                if (Request["PONo"] != null)
                {
                    txtPONo.Text = Request["PONo"].ToString();
                    Show();
                }
            }
        }



        private void Show()
        {
            if (string.IsNullOrEmpty(txtStartTime.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('启用时间不能为空！');</script>");
                return;
            }
            string sql = " ";

            if (txtPONo.Text != "")
            {
                sql += string.Format(" and CG_POOrder.PONo like '%{0}%'", txtPONo.Text);
            }


            if (ttxPOName.Text != "")
            {
                sql += string.Format(" and CG_POOrder.POName like '%{0}%'", ttxPOName.Text);
            }

            if (txtFrom.Text != "")
            {
                sql += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                sql += string.Format(" and CG_POOrder.PODate<='{0} 23:59:59'", txtTo.Text);
            }


            if (txtGuestName.Text != "")
            {
                sql += string.Format(" and CG_POOrder.GuestName  like '%{0}%'", txtGuestName.Text);
            }
            var isColse = "";
            if (ddlIsClose.Text != "-1")
            {
                isColse = " and IsClose=" + ddlIsClose.Text;
            }

            if (ddlIsSelect.Text != "-1")
            {
                isColse += " and IsSelected=" + ddlIsSelect.Text;
            }
            if (ddlPOTyle.Text != "-1")
            {
                isColse += " and CG_POOrder.POType=" + ddlPOTyle.Text;
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                isColse += " and JieIsSelected=" + ddlJieIsSelected.Text;
            }
            //if (ViewState["showAll"] != null)
            //{
            //    sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=JXC_REPORT.PONO )", Session["currentUserId"]);
            //}

            //if (ddlGuestTypeList.SelectedValue != "全部")
            //{
            //    sql += string.Format(" and EXISTS (select id from TB_GuestTrack where MyGuestType='{0}' and TB_GuestTrack.id=CG_POOrder.GuestId )",ddlGuestTypeList.SelectedValue);
            //}

            if (ddlGuestTypeList.SelectedValue != "全部")
            {
                sql += string.Format(" and GuestType='{0}'", ddlGuestTypeList.SelectedValue);
            }

            if (ddlGuestProList.SelectedValue != "-2")
            {
                sql += string.Format(" and GuestPro={0}", ddlGuestProList.SelectedValue);
            }



            if (ddlUser.Text == "-1")//显示所有用户
            {
                if (cbIsSpecial.Checked)
                {
                    sql += string.Format(" and IsSpecial=0 {0} ", isColse);
                    //sql += string.Format(" and EXISTS (select ID from CG_POOrder where IsSpecial=0 AND PONO=JXC_REPORT.PONO {0})", isColse);
                }
                //else
                //{
                //    sql += string.Format(" and EXISTS (select ID from CG_POOrder where IsSpecial=1 AND PONO=JXC_REPORT.PONO {0})", isColse);
                //}
                //var model = Session["userInfo"] as User;
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职') AND PONO=JXC_REPORT.PONO )", Session["currentUserId"]);
            }
            else if (ddlUser.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') AND PONO=JXC_REPORT.PONO )", model.LoginIPosition);

                if (cbIsSpecial.Checked)
                {
                    sql += string.Format(" and CG_POOrder.AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') and IsSpecial=0 {1}",
                         model.LoginIPosition, isColse);
                    // sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') AND PONO=JXC_REPORT.PONO and IsSpecial=0 {1})", model.LoginIPosition, isColse);
                }
                else
                {
                    sql += string.Format(" and CG_POOrder.AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}')  {1}",
                        model.LoginIPosition, isColse);
                    //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') AND PONO=JXC_REPORT.PONO and IsSpecial=1 {1})", model.LoginIPosition, isColse);
                }
            }
            else
            {

                if (cbIsSpecial.Checked)
                {
                    sql += string.Format(" and CG_POOrder.AppName={0} and IsSpecial=0 {1} ", ddlUser.Text, isColse);
                    //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=JXC_REPORT.PONO  and IsSpecial=0 {1})", ddlUser.Text, isColse);
                }
                else
                {
                    sql += string.Format(" and CG_POOrder.AppName={0}  {1} ", ddlUser.Text, isColse);
                    //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=JXC_REPORT.PONO  and IsSpecial=1 {1})", ddlUser.Text, isColse);

                }
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=JXC_REPORT.PONO )", ddlUser.Text);
            }

            if (CheckBox1.Checked || CheckBox2.Checked || CheckBox3.Checked || CheckBox4.Checked)
            {

                sql += " and exists ( select ID from CG_POOrder where PONO=JXC_REPORT.PONO";
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

                sql += ")";
            }


            string having = " having ";
            if (CheckBox5.Checked && CheckBox6.Checked)
            {
                having += string.Format("  (avg(SellFPTotal)<> sum(goodSellTotal) or  avg(SellFPTotal) is null)  and (avg(InvoTotal)<> sum(goodSellTotal) or sum(InvoTotal) is null)");
            }
            else if (CheckBox5.Checked || CheckBox6.Checked)
            {
                if (CheckBox5.Checked)
                {
                    having += string.Format("  avg(SellFPTotal)<> sum(goodSellTotal) or  avg(SellFPTotal) is null ");
                }
                if (CheckBox6.Checked)
                {
                    having += string.Format("  avg(InvoTotal)<> sum(goodSellTotal) or sum(InvoTotal) is null ");
                }
            }
            else
            {
                having = "";
            }


            string fuhao = " where 1=1 ";
            if (ddlFuHao.Text != "-1")
            {
                fuhao = string.Format(" where SumPOTotal {0} goodSellTotal", ddlFuHao.Text);
            }
            if (!string.IsNullOrEmpty(txtProProfit.Text) && ddlProProfit.Text != "-1")
            {
                fuhao += string.Format(" and maoliTotal {0} {1}", ddlProProfit.Text, txtProProfit.Text);
            }

            if (!string.IsNullOrEmpty(txtProTureProfit.Text) && ddlProTureProfit.Text != "-1")
            {

                fuhao += string.Format(" and InvoTotal-goodTotal {0} {1}", ddlProTureProfit.Text, txtProTureProfit.Text);
            }

            List<JXC_REPORTTotal> pOOrderList = this.POSer.Assessment_GetListArray_Total(sql, having, fuhao, Convert.ToDateTime(txtStartTime.Text));
            if (ddlZhangQI.Text == "1")//实际帐期<=帐期截止期
            {
                pOOrderList = pOOrderList.FindAll(t => t.trueZhangQi <= t.ZhangQiTotal);
            }
            if (ddlZhangQI.Text == "2")//实际帐期<帐期截止期
            {
                pOOrderList = pOOrderList.FindAll(t => t.trueZhangQi < t.ZhangQiTotal);
            }

            var getAllPONos = pOOrderList.Aggregate("", (current, m) => current + ("'" + m.PONo + "',")).Trim(',');
            lblVisAllPONO.Text = getAllPONos;
            decimal jlr = pOOrderList.Sum(t => t.maoliTotal);
            lblJLR.Text = string.Format("{0:n2}", jlr);
            lblXSE.Text = string.Format("{0:n2}", pOOrderList.Sum(t => t.goodSellTotal));
            lblSJLR.Text = string.Format("{0:n2}", pOOrderList.Sum(t => t.TrueLiRun));

            lblFP.Text = string.Format("{0:n2}", pOOrderList.Sum(t => t.SellFPTotal));
            decimal liRunKouChu = pOOrderList.Sum(t => t.LiRunKouChu);
            lblLiRunKouChu.Text = string.Format("{0:n2}", liRunKouChu);
            //项目纯利合计（项目净利合计-利润扣除合计）
            lblChunLiRunKouChu.Text = string.Format("{0:n2}", (jlr - liRunKouChu));
            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

            //pager.TotalCount = pOOrderList.Count;
            //PagerControl page= gvMain.BottomPagerRow.Cells[0].FindControl("myPage") as PagerControl;
            //page.TotalCount = pOOrderList.Count;
            //page.BindData();
            // AspNetPager1.CustomInfoHTML = "第<font color='red'><b>%currentPageIndex%</b></font>页，共%PageCount%页，每页显示%PageSize%条记录";

        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        public void BindData()
        {
            Show();
            //AspNetPager1.CustomInfoHTML = "Page  <font color=\"red\"><b>" + AspNetPager1.CurrentPageIndex + "</b></font> of  " + AspNetPager1.PageCount;
            //AspNetPager1.CustomInfoHTML += "  Orders " + AspNetPager1.StartRecordIndex + "-" + AspNetPager1.EndRecordIndex;
        }

        protected void AspNetPager1_PageChanged(object src, EventArgs e)
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

                JXC_REPORTTotal m = e.Row.DataItem as JXC_REPORTTotal;

                // m.GuestProString = m.GuestProString.Replace("用户", "");

                if (m.IsClose)
                {
                    e.Row.BackColor = System.Drawing.Color.FromArgb(204, 255, 204);
                }

                if (m.IsQuanDao)
                {
                    var lblDays = e.Row.FindControl("lblDays") as Label;
                    lblDays.Font.Underline = true;
                }
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {

                Response.Redirect("~/JXC/JXC_REPORTList.aspx?PONo=" + e.CommandArgument + "&IsSpecial=" + cbIsSpecial.Checked);

            }

        }






        private void setValue(Label control, string value)
        {
            control.Text = value;
        }

        protected void gvExvel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[3].Attributes.Add("style", "vnd.ms-excel.numberformat:yyyy-MM-dd");
                e.Row.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat:yyyy-MM-dd");
            }
        }


        protected void btnExcel_Click(object sender, EventArgs e)
        {

            if (lblVisAllPONO.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('无信息可以导出！');</script>");

                return;
            }
            var strSql = new StringBuilder();
            strSql.Append("select JXC_REPORT.PONo as '项目编号',CG_POOrder.POName as '项目名称',JXC_REPORT.ProNo as '单号',PODate as '项目日期',Supplier as '客户名称',RuTime as '出库日期',");
            strSql.Append(" goodInfo as '销售内容',GoodNum as '数量',GoodSellPrice as '出货单价',goodSellTotal as '销售额',GoodPrice as '单价成本',");
            strSql.Append(" goodTotal as '总成本',t_GoodNums as '成本确认价',t_GoodTotalChas as '损失差额',maoli as '毛利润额',FPTotal as '发票号码' ");
            strSql.Append(" FROM JXC_REPORT ");
            strSql.AppendFormat(" left join CG_POOrder on JXC_REPORT.PONo=CG_POOrder.PONo and CG_POOrder.IFZhui=0 where JXC_REPORT.PONo in ({0}) order by JXC_REPORT.PONo ", lblVisAllPONO.Text);



            System.Data.DataTable dt = DBHelp.getDataTable(strSql.ToString());
            GridView gvOrders = new GridView();
            gvOrders.RowDataBound += gvExvel_RowDataBound;
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "GB2312";
            Response.AppendHeader("Content-Disposition", "attachment;filename=SellInfo.xls");
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
