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

namespace VAN_OA.ReportForms
{
    public partial class WFToInvoiceList : BasePage
    {


        private TB_ToInvoiceService toInvoSerSer = new TB_ToInvoiceService();

        private void Show()
        {
            string sql = " 1=1 ";
            string sql2 = " ";
            bool mustContent = false;

            string s = " 1=1 ";
            string s1 = "";

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款时间 格式错误！');</script>");
                    return;
                }

                if (mustContent == false)
                {
                    sql += " and TB_ToInvoice.id is not null ";
                    mustContent = true;
                }
                sql += string.Format(" and DaoKuanDate>='{0} 00:00:00'", txtFrom.Text);
                s += string.Format(" and DaoKuanDate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款时间 格式错误！');</script>");
                    return;
                }
                if (mustContent == false)
                {
                    sql += " and TB_ToInvoice.id is not null ";
                    mustContent = true;
                }
                sql += string.Format(" and DaoKuanDate<='{0} 23:59:59'", txtTo.Text);
                s += string.Format(" and DaoKuanDate<='{0} 23:59:59'", txtTo.Text);
            }

            if (txtPoDateFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtPoDateFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and PoDate>='{0} 00:00:00'", txtPoDateFrom.Text);

                s1 += string.Format(" and PoDate>='{0} 00:00:00'", txtPoDateFrom.Text);
            }

            if (txtPoDateTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtPoDateTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and PoDate<='{0} 23:59:59'", txtPoDateTo.Text);
                s1 += string.Format(" and PoDate<='{0} 23:59:59'", txtPoDateTo.Text);
            }

            if (txtGuestName.Text.Trim() != "")
            {
                sql2 += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }


            if (txtPONO.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONO.Text.Trim()) == false)
                {
                    return;
                }
                sql2 += string.Format(" and CG_POOrder.PoNo like '%{0}%'", txtPONO.Text.Trim());
            }

            if (txtPOName.Text.Trim() != "")
            {
                sql2 += string.Format(" and PoName like '%{0}%'", txtPOName.Text.Trim());
            }
            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and TB_ToInvoice.ProNo like '%{0}%'", txtProNo.Text.Trim());
                s += string.Format(" and ProNo like '%{0}%'", txtProNo.Text.Trim());
                s1 += string.Format(" and Total is not null ", txtProNo.Text.Trim());
            }

            if (ddlState.Text != "全部")
            {
                if (mustContent == false)
                {
                    sql += " and TB_ToInvoice.id is not null ";
                    s += " and id is not null ";
                    mustContent = true;
                }
                sql += string.Format(" and State='{0}'", ddlState.Text);

                s += string.Format(" and State='{0}'", ddlState.Text);

            }
            else
            {
                //sql += string.Format(" and (State<>'不通过' or State is null)");
                //if (cbPOHeBing.Checked)
                //{
                //    s += string.Format(" and State='{0}'", "通过");
                //}

            }

            if (ddlFPState.Text == "1")//已开全票
            {
                sql2 += " and POStatue3='已开票' ";
            }

            if (ddlModel.Text != "全部")
            {
                sql2 += string.Format(" and CG_POOrder.Model='{0}'", ddlModel.Text);
            }

            if (ddlPOTyle.Text != "-1")
            {
                sql2 += " and CG_POOrder.POType=" + ddlPOTyle.Text;
            }
            if (ddlGuestTypeList.SelectedValue != "全部")
            {
                sql2 += string.Format(" and GuestType='{0}'", ddlGuestTypeList.SelectedValue);
            }
            if (ddlGuestProList.SelectedValue != "-2")
            {
                sql2 += string.Format(" and GuestPro={0}", ddlGuestProList.SelectedValue);
            }

            if (cbClose.Checked)
            {
                sql2 += " and IsClose=0 ";
            }

            if (ddlNoSpecial.Text != "-1")
            {
                sql2 += string.Format(" and IsSpecial=" + ddlNoSpecial.Text);
            }

            if (cbHadJiaoFu.Checked)
            {
                sql2 += " and POStatue2='已交付' ";
            }
            if (cbIsPoFax.Checked)
            {
                sql2 += " and IsPoFax=0 ";
            }
            if (cbHanShui.Checked)
            {
                sql2 += " and IsPoFax=1 ";
            }
            if (ddlJinECha.Text != "-1")
            {
                s1 += " and ISNULL(Total,0) " + ddlJinECha.Text + "newtable1.POTotal-isnull(TuiTotal,0)";
            }

            if (ddlFPState.Text == "2")//未开全票
            {
                sql += " and (hadFpTotal<>0 and newtable1.POTotal-isnull(TuiTotal,0)>hadFpTotal)  ";
                s1 += " and (hadFpTotal<>0 and newtable1.POTotal-isnull(TuiTotal,0)>hadFpTotal)  ";
            }
            if (ddlFPState.Text == "3")//未开票
            {
                sql += " and hadFpTotal is null ";
                s1 += " and hadFpTotal is null ";
            }
            //到款<=项目金额
            if (ddlDiffDays.Enabled && ddlDiffDays.Text != "-1" && (ddlJinECha.Text == "<=" || ddlJinECha.Text == "-1"))
            {
                if (ddlDiffDays.Text == "1")
                {
                    s1 += " and ( (( POStatue4='' or POStatue4 is null) and datediff(d,minOutTime,getdate())<=30) or (POStatue4='已结清' and datediff(d,minOutTime,MaxDaoKuanDate)<=30))";
                }
                else if (ddlDiffDays.Text == "2")
                {
                    s1 += " and ( ( POStatue4='' or POStatue4 is null) and (datediff(d,minOutTime,getdate())>30 and datediff(d,CG_POOrder.PODate,getdate())<=60) or (POStatue4='已结清' and datediff(d,minOutTime,MaxDaoKuanDate)>30 and datediff(d,CG_POOrder.PODate,getdate())<=60))";
                }
                else if (ddlDiffDays.Text == "3")
                {
                    s1 += " and ( ( POStatue4='' or POStatue4 is null) and (datediff(d,minOutTime,getdate())>60 and datediff(d,CG_POOrder.PODate,getdate())<=90) or (POStatue4='已结清' and datediff(d,minOutTime,MaxDaoKuanDate)>60 and datediff(d,CG_POOrder.PODate,getdate())<=90))";
                }
                else if (ddlDiffDays.Text == "4")
                {
                    s1 += " and ( ( POStatue4='' or POStatue4 is null) and (datediff(d,minOutTime,getdate())>90 and datediff(d,CG_POOrder.PODate,getdate())<=120) or (POStatue4='已结清' and datediff(d,minOutTime,MaxDaoKuanDate)>90 and datediff(d,CG_POOrder.PODate,getdate())<=120))";
                }
                else if (ddlDiffDays.Text == "5")
                {
                    s1 += " and ( (( POStatue4='' or POStatue4 is null) and datediff(d,minOutTime,getdate())>90) or (POStatue4='已结清' and datediff(d,minOutTime,MaxDaoKuanDate)>90))";
                }
                else if (ddlDiffDays.Text == "6")
                {
                    s1 += " and ( (( POStatue4='' or POStatue4 is null) and datediff(d,minOutTime,getdate())>120) or  (POStatue4='已结清' and datediff(d,minOutTime,MaxDaoKuanDate)>120 ))";
                }
                else if (ddlDiffDays.Text == "7")
                {
                    s1 += " and ( (( POStatue4='' or POStatue4 is null) and datediff(d,minOutTime,getdate())>180) or (POStatue4='已结清' and datediff(d,minOutTime,MaxDaoKuanDate)>180))";
                }

            }
            // 到款《项目金额
            if (ddlDiffDays.Enabled && ddlDiffDays.Text != "-1" && ddlJinECha.Text == "<")
            {
                if (ddlDiffDays.Text == "1")
                {
                    s1 += " and datediff(d,minOutTime,getdate())<=30 ";
                }
                else if (ddlDiffDays.Text == "2")
                {
                    s1 += " and datediff(d,minOutTime,getdate())>30 and datediff(d,CG_POOrder.PODate,getdate())<=60";
                }
                else if (ddlDiffDays.Text == "3")
                {
                    s1 += " and datediff(d,minOutTime,getdate())>60 and datediff(d,CG_POOrder.PODate,getdate())<=90";
                }
                else if (ddlDiffDays.Text == "4")
                {
                    s1 += " and datediff(d,minOutTime,getdate())>90 and datediff(d,CG_POOrder.PODate,getdate())<=120";
                }
                else if (ddlDiffDays.Text == "5")
                {
                    s1 += " and datediff(d,minOutTime,getdate())>90 ";
                }
                else if (ddlDiffDays.Text == "6")
                {
                    s1 += " and datediff(d,minOutTime,getdate())>120 ";
                }
                else if (ddlDiffDays.Text == "7")
                {
                    s1 += " and datediff(d,minOutTime,getdate())>180 ";
                }

            }
            //到款=项目金额 + 到款>=项目金额
            if (ddlDiffDays.Enabled && ddlDiffDays.Text != "-1" && (ddlJinECha.Text == ">=" || ddlJinECha.Text == "=" || ddlJinECha.Text == ">"))
            {
                if (ddlDiffDays.Text == "1")
                {
                    s1 += " and datediff(d,minOutTime,MaxDaoKuanDate)<=30 ";
                }
                else if (ddlDiffDays.Text == "2")
                {
                    s1 += " and datediff(d,minOutTime,MaxDaoKuanDate)>30 and datediff(d,CG_POOrder.PODate,getdate())<=60";
                }
                else if (ddlDiffDays.Text == "3")
                {
                    s1 += " and datediff(d,minOutTime,MaxDaoKuanDate)>60 and datediff(d,CG_POOrder.PODate,getdate())<=90";
                }
                else if (ddlDiffDays.Text == "4")
                {
                    s1 += " and datediff(d,minOutTime,MaxDaoKuanDate)>90 and datediff(d,CG_POOrder.PODate,getdate())<=120";
                }
                else if (ddlDiffDays.Text == "5")
                {
                    s1 += " and datediff(d,minOutTime,MaxDaoKuanDate)>90 ";
                }
                else if (ddlDiffDays.Text == "6")
                {
                    s1 += " and datediff(d,minOutTime,MaxDaoKuanDate)>120 ";
                }
                else if (ddlDiffDays.Text == "7")
                {
                    s1 += " and datediff(d,minOutTime,MaxDaoKuanDate)>180 ";
                }

            }
            if (cbPoNoZero.Checked)
            {
                sql += string.Format(" and newtable1.POTotal-isnull(TuiTotal,0)<>0");
                s1 += string.Format(" and newtable1.POTotal-isnull(TuiTotal,0)<>0");
            }
            string isClose = "";
            if (ddlPoClose.Text != "-1")
            {
                isClose += " and IsClose=" + ddlPoClose.Text;
            }


            if (ddlIsSelect.Text != "-1")
            {
                isClose += " and IsSelected=" + ddlIsSelect.Text;
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                isClose += " and JieIsSelected=" + ddlJieIsSelected.Text;
            }

            if (ddlUser.Text == "-1")//显示所有用户
            {
                //var model = Session["userInfo"] as User;
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职') AND PONO=JXC_REPORT.PONO )", Session["currentUserId"]);
            }
            else
            {
                if (ViewState["showAll"] != null)
                {
                    sql += string.Format(" and  AE='{0}'", ddlUser.SelectedItem.Text);
                    s1 += string.Format(" and AE='{0}'  ", ddlUser.SelectedItem.Text);

                }
                else
                {
                    sql += string.Format(" and  AE='{0}'", ddlUser.SelectedItem.Text);
                    s1 += string.Format(" and AE='{0}'  ", ddlUser.SelectedItem.Text);
                }
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and newtable1.AppName IN(select id from tb_User where {0})", where);
                s1 += string.Format(" and AE IN(select loginName from tb_User where {0})  ", where);
            }
            string FPTotal = "";
            if (txtFPNo.Text.Trim() != "")
            {
                if (mustContent == false)
                {
                    sql += " and TB_ToInvoice.id is not null ";

                    s += " and id is not null ";
                    mustContent = true;
                }
                sql += string.Format(" and FPNo like '%{0}%'", txtFPNo.Text.Trim());
                s += string.Format(" and FPNo like '%{0}%'", txtFPNo.Text.Trim());


                FPTotal = string.Format(" and FPTOTAL like '%{0}%'", txtFPNo.Text.Trim());
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
                    sql += string.Format(" and newtable1.POTotal-isnull(TuiTotal,0) {0} {1}", ddlPrice.Text, txtPOTotal.Text);
                    s1 += string.Format(" and newtable1.POTotal-isnull(TuiTotal,0) {0} {1}", ddlPrice.Text, txtPOTotal.Text);
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

                    sql += string.Format(" and Total {0} {1}", ddlDaoKuanTotal.Text, txtDaoKuanTotal.Text);
                    s1 += string.Format(" and Total {0} {1}", ddlDaoKuanTotal.Text, txtDaoKuanTotal.Text);
                }
                catch (Exception)
                {

                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款金额格式有误！');</script>");
                    return;
                }
            }
            List<TB_ToInvoice> cars = new List<TB_ToInvoice>();
            if (cbPOHeBing.Checked)
            {
                gvList.Columns[gvList.Columns.Count - 3].Visible = false;
                gvList.Columns[gvList.Columns.Count - 4].Visible = true;
                cars = this.toInvoSerSer.GetListArrayReport_HeBing(s, s1, sql2, FPTotal, isClose);
            }
            else
            {
                gvList.Columns[gvList.Columns.Count - 3].Visible = true;
                gvList.Columns[gvList.Columns.Count - 4].Visible = false;
                if (ddlBusType.Text != "-1")
                {
                    sql += string.Format(" and BusType=" + ddlBusType.Text);
                }
                cars = this.toInvoSerSer.GetListArrayReport(sql, sql2, isClose);

                if (cars.Count > 0)
                {                 

                    using (SqlConnection conn = DBHelp.getConn())
                    {
                        conn.Open();
                        for (int i = gvList.PageIndex * 10; i < ((gvList.PageIndex + 1) * 10); i++)
                        {
                            if (i < cars.Count)
                            {
                           
                                var model = cars[i];
                                string strSql = string.Format(@" select isnull(sum(Total), 0) from TB_ToInvoice where PoNo = '{0}' and DaoKuanDate <= '{1}'", 
                                    model.PoNo,model.DaoKuanDate1);
                                SqlCommand objCommand = new SqlCommand(strSql, conn);
                                var ojb = objCommand.ExecuteScalar();
                                if (ojb != null && ojb != DBNull.Value)
                                {
                                    model.Total1 = Convert.ToDecimal(ojb);
                                }
                            }
                        }
                        conn.Close();
                    }
                }

            }

            if (ddlFPDays.Text != "-1")
            {
                if (ddlFPDays.Text == "1")
                {
                    cars = cars.FindAll(t => t.FPDays <= 30);
                }
                else if (ddlFPDays.Text == "2")
                {
                    cars = cars.FindAll(t => t.FPDays <= 60 && t.FPDays > 30);
                }
                else if (ddlFPDays.Text == "3")
                {
                    cars = cars.FindAll(t => t.FPDays <= 90 && t.FPDays > 60);
                }
                else if (ddlFPDays.Text == "4")
                {
                    cars = cars.FindAll(t => t.FPDays <= 120 && t.FPDays > 90);
                }
                else if (ddlFPDays.Text == "5")
                {
                    cars = cars.FindAll(t => t.FPDays <= 180 && t.FPDays > 120);
                }
                else if (ddlFPDays.Text == "6")
                {
                    cars = cars.FindAll(t => t.FPDays > 180);
                }
            }

            if (ddlWeiFPDays.Text != "-1")
            {
                if (ddlWeiFPDays.Text == "1")
                {
                    cars = cars.FindAll(t => t.WeiFPDays <= 30);
                }
                else if (ddlWeiFPDays.Text == "2")
                {
                    cars = cars.FindAll(t => t.WeiFPDays <= 60 && t.WeiFPDays > 30);
                }
                else if (ddlWeiFPDays.Text == "3")
                {
                    cars = cars.FindAll(t => t.WeiFPDays <= 90 && t.WeiFPDays > 60);
                }
                else if (ddlWeiFPDays.Text == "4")
                {
                    cars = cars.FindAll(t => t.WeiFPDays <= 120 && t.WeiFPDays > 90);
                }
                else if (ddlWeiFPDays.Text == "5")
                {
                    cars = cars.FindAll(t => t.WeiFPDays <= 180 && t.WeiFPDays > 120);
                }
                else if (ddlWeiFPDays.Text == "6")
                {
                    cars = cars.FindAll(t => t.WeiFPDays > 180);
                }
            }
            decimal AllPOTotal = 0;
            Hashtable hs = new Hashtable();
            decimal AllInvoiceTotal = 0;
            decimal hsWKpTotal = 0;
            foreach (var model in cars)
            {
                if (!hs.Contains(model.PoNo))
                {
                    AllPOTotal += model.POTotal;
                    hs.Add(model.PoNo, null);

                    if (model.IsPoFax)
                    {
                        hsWKpTotal += model.POTotal - model.HadFpTotal;
                    }
                }
                AllInvoiceTotal += model.Total;
            }

            lblALLInvoiceTotal.Text = string.Format("{0:n2}", AllInvoiceTotal);
            lblAllPoTotal.Text = string.Format("{0:n2}", AllPOTotal);
            lblAllWeiTotal.Text = string.Format("{0:n2}", (AllPOTotal - AllInvoiceTotal));
            lblhsWKpTotal.Text = string.Format("{0:n2}", hsWKpTotal);
            //lblMess.Text = total.ToString();        

            AspNetPager1.RecordCount = cars.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = cars;
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
                TB_ToInvoice model = e.Row.DataItem as TB_ToInvoice;
                if (model != null)
                {
                    if (model.POTotal != 0 && model.HadFpTotal == model.POTotal)
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                    }
                    else if (model.HadFpTotal != 0)
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }
                }
                if (model.IsQuanDao)
                {
                    var lblDays = e.Row.FindControl("lblDays") as Label;
                    lblDays.Font.Underline = true;
                }
                if (model.POTotal == model.Total)
                {
                    var lblFPDays = e.Row.FindControl("lblFPDays") as Label;
                    lblFPDays.Font.Underline = true;
                }

                if (model.FPDate.HasValue)
                {
                    var lblWeiFPDays = e.Row.FindControl("lblWeiFPDays") as Label;
                    lblWeiFPDays.Font.Underline = true;
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {

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

                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

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

                List<TB_ToInvoice> poseModels = new List<TB_ToInvoice>();
                this.gvList.DataSource = poseModels;
                this.gvList.DataBind();

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();

                //                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='查看所有'
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='到款单列表') and sys_Object.AutoID is not null", Session["currentUserId"]);
                //                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                if (NewShowAll_textName("到款单列表", "查看所有") == false)
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

                //                sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='不能编辑'
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='到款单列表') and sys_Object.AutoID is not null", Session["currentUserId"]);
                //                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                if (NewShowAll_textName("到款单列表", "不能编辑") == false)
                {
                    gvList.Columns[0].Visible = false;
                }


                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";


                if (Request["ishebing"] != null)
                {
                    cbPOHeBing.Checked = true;
                }
                if (Request["PONo"] != null)
                {
                    txtPONO.Text = Request["PONo"].ToString();
                    ddlNoSpecial.Text = "-1";
                    Show();
                }

            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ReEdit")
            {
                //是否是此单据的申请人
                var model = toInvoSerSer.GetModel(Convert.ToInt32(e.CommandArgument));

                //首先单子要先通过               

                if (model != null && model.State == "通过")
                {

                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据必须已经审批通过才能重新编辑！');</script>");
                    return;
                }
                string sql = "select pro_Id from A_ProInfo where pro_Type='到款单'";

                string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='到款单')", e.CommandArgument);
                string url = "~/EFrom/WFToInvoice.aspx?ProId=" + DBHelp.ExeScalar(sql) + "&allE_id=" + e.CommandArgument + "&EForm_Id=" + DBHelp.ExeScalar(efromId) + "&&ReAudit=true";
                Response.Redirect(url);
                //没有做过检验单
            }
            if (e.CommandName == "select")
            {
                string[] PONo_Id = e.CommandArgument.ToString().Split('_');
                CG_POOrderService orderSer = new CG_POOrderService();
                var list = orderSer.GetOrder_ToInvoice_1(string.Format(" PONo='{0}'", PONo_Id[0]));
                if (list.Count > 0)
                {
                    lblPOTotal.Text = "项目(" + PONo_Id[0] + ")---" + string.Format("{0:n2}", list[0].POTotal - list[0].TuiTotal);
                    lblWeiTotal.Text = string.Format("{0:n2}", list[0].WeiTotal);
                    lblMess.Text = string.Format("{0:n2}", list[0].sumTotal);

                    string sqlKaiPiao = "select isnull(sum(total),0) from Sell_OrderFP where Status='通过' and pono='" + PONo_Id[0] + "'";
                    lblKaiPiaoTotal.Text = string.Format("{0:n2}", DBHelp.ExeScalar(sqlKaiPiao));

                    if (list[0].POTotal - list[0].TuiTotal != 0)
                    {
                        lblWeiTotalBiLi.Text = string.Format("{0:n2}", list[0].WeiTotal / (list[0].POTotal - list[0].TuiTotal) * 100);
                    }
                    else
                    {
                        lblWeiTotalBiLi.Text = "0";
                    }

                    string fpSQL = string.Format(" SELECT FPNo,RuTime,Total FROM Sell_OrderFP where Status='通过' and pono='{0}'", PONo_Id[0]);
                    var fpList = DBHelp.getDataTable(fpSQL);


                    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    //int i = 1;
                    //foreach (DataRow dr in fpList.Rows)
                    //{
                    //    sb.AppendFormat("{0} ：{1}      ", dr[0], dr[1]);
                    //    if (i % 5 == 0)
                    //    {
                    //        sb.Append(System.Environment.NewLine);
                    //    }
                    //    i++;
                    //}
                    //lblFPDetail.Text = sb.ToString();
                    ViewState["fpList"] = fpList;
                }
                else
                {
                    lblPOTotal.Text = "0";
                    lblWeiTotal.Text = "0";
                    lblMess.Text = "0";
                    lblKaiPiaoTotal.Text = "0";
                    lblWeiTotalBiLi.Text = "0";
                }
                string sql = "select PoNo,CreateUser,AppleDate,ZhangQi,Remark from TB_ToInvoice where id=" + PONo_Id[1];
                gvDetail.DataSource = DBHelp.getDataTable(sql);
                gvDetail.DataBind();
                //this.gvList.DataKeys[e.].Value.ToString()
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void ddlJinECha_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlJinECha.Text != "-1")
            //{
            ddlDiffDays.Enabled = true;
            //}
            //else
            //{
            //    ddlDiffDays.Enabled = false;
            //}

        }

        protected void cbPOHeBing_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPOHeBing.Checked)
            {
                Panel1.Enabled = true;
                ddlBusType.Enabled = false;
            }
            else
            {
                Panel1.Enabled = false;
                ddlBusType.Enabled = true;
            }
        }

        protected void cbHanShui_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHanShui.Checked)
            {
                cbIsPoFax.Enabled = false;
                cbIsPoFax.Checked = false;
            }
            else
            {
                cbIsPoFax.Enabled = true;
                cbHanShui.Enabled = true;
            }
        }

        protected void cbIsPoFax_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsPoFax.Checked)
            {
                cbHanShui.Enabled = false;
                cbHanShui.Checked = false;
            }
            else
            {
                cbIsPoFax.Enabled = true;
                cbHanShui.Enabled = true;
            }
        }
    }
}
