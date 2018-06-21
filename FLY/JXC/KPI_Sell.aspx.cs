using System;
using System.Web.UI.WebControls;
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;
using VAN_OA.Model;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class KPI_Sell : BasePage
    {
        JXC_REPORTService POSer = new JXC_REPORTService();
        protected void cbKaoAll_CheckedChanged(object sender, EventArgs e)
        {

            if (cbKaoAll.Checked)
            {
                foreach (ListItem item in cbKaoList.Items)
                {
                    item.Selected = false;
                }
                cbKaoList.Enabled = false;
            }
            else
            {
                cbKaoList.Enabled = true;
            }
        }

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

                //txtStartTime.Text = (DateTime.Now.Year - 1) + "-1-1";

                txtStartTime.Text = DateTime.Now.AddYears(-1).AddDays(-150).ToString("yyyy-MM-dd");
                List<TB_BasePoType> basePoTypeList = new TB_BasePoTypeService().GetListArray("");
                List<TB_BasePoType> basePoTypeList1 = new TB_BasePoTypeService().GetListArray("");
                cbKaoList.DataSource = basePoTypeList1;
                cbKaoList.DataBind();
                cbKaoList.DataTextField = "BasePoType";
                cbKaoList.DataValueField = "Id";

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

                //主单
                List<JXC_REPORTTotal> pOOrderList = new List<JXC_REPORTTotal>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //if (VAN_OA.JXC.SysObj.IfShowAll(SysObj.JXC_REPORTTotalList, Session["currentUserId"]) == false)
                //{
                //    ViewState["showAll"] = false;
                //}


                bool showAll = true;
                if (QuanXian_ShowAll("销售KPI 量化指标") == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }



                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll("销售KPI 量化指标", Session["currentUserId"], "WFScanDepartList") == true)
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
                
                if (Request["PONo"] != null)
                {
                    txtPONo.Text = Request["PONo"].ToString();
                    Show();
                }
            }
        }

        

        private void Show()
        {
            string sql = " ";
            string contactWhere = " where 1=1 ";
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

             
            if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                return;
            }
            
            if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                return;
            }
            contactWhere += string.Format(" and [DateTime]>='{0} 00:00:00'", txtFrom.Text);
            contactWhere += string.Format(" and [DateTime]<='{0} 23:59:59'", txtTo.Text);

            sql += string.Format(" and ((CG_POOrder.PODate>='{0} 00:00:00' and CG_POOrder.PODate<='{1} 23:59:59') or (CG_POOrder.PODate>='{2} 00:00:00' and CG_POOrder.PODate<='{3} 23:59:59'))", 
                txtFrom.Text, txtTo.Text, Convert.ToDateTime(txtFrom.Text).AddDays(-120).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtTo.Text).AddDays(-120).ToString("yyyy-MM-dd"));
             
            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and CG_POOrder.GuestName  like '%{0}%'", txtGuestName.Text.Trim());
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
                     
                }
            }
            else if (ddlUser.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
               
                if (cbIsSpecial.Checked)
                {
                    sql += string.Format(" and CG_POOrder.AE in (select LOGINNAME from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') and IsSpecial=0 {1}",
                         model.LoginIPosition, isColse);
                   // sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') AND PONO=JXC_REPORT.PONO and IsSpecial=0 {1})", model.LoginIPosition, isColse);
                }
                else
                {
                    sql += string.Format(" and CG_POOrder.AE in (select LOGINNAME from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}')  {1}",
                        model.LoginIPosition, isColse);
                    //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') AND PONO=JXC_REPORT.PONO and IsSpecial=1 {1})", model.LoginIPosition, isColse);
                }
            }
            else
            {

                if (cbIsSpecial.Checked)
                {
                    sql += string.Format(" and CG_POOrder.AE='{0}' and IsSpecial=0 {1} ", ddlUser.SelectedItem.Text, isColse);
                    //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=JXC_REPORT.PONO  and IsSpecial=0 {1})", ddlUser.Text, isColse);
                }
                else
                {
                    sql += string.Format(" and CG_POOrder.AE='{0}'  {1} ", ddlUser.SelectedItem.Text, isColse);
                    //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=JXC_REPORT.PONO  and IsSpecial=1 {1})", ddlUser.Text, isColse);

                }
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=JXC_REPORT.PONO )", ddlUser.Text);
            }
             if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and exists (select id from tb_User where  IFZhui=0 and {0} and CG_POOrder.AE=LOGINNAME)", where);
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
            if (ddlPOFaTotal.Text != "-1")
            {
                fuhao += string.Format(" and SumPOTotal {0} isnull(SellFPTotal,0)", ddlPOFaTotal.Text);
            }
            if (ddlShiJiDaoKuan.Text != "-1")
            {
                fuhao += string.Format(" and SumPOTotal {0} isnull(InvoTotal,0)", ddlShiJiDaoKuan.Text);
            }
            if (ddlJingLiTotal.Text != "-1")
            {
                fuhao += string.Format(" and maoliTotal {0} (isnull(InvoTotal,0)-isnull(goodTotal,0))", ddlJingLiTotal.Text);
            }

            if (!string.IsNullOrEmpty(txtProProfit.Text) && ddlProProfit.Text!="-1")
            {
                if (CommHelp.VerifesToNum(txtProProfit.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目净利 格式错误！');</script>");
                    return;
                }
                fuhao += string.Format(" and maoliTotal {0} {1}", ddlProProfit.Text, txtProProfit.Text);
            }

            if (!string.IsNullOrEmpty(txtProTureProfit.Text)&&ddlProTureProfit.Text!="-1")
            {
                if (CommHelp.VerifesToNum(txtProTureProfit.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('实际净利 格式错误！');</script>");
                    return;
                }
                fuhao += string.Format(" and InvoTotal-goodTotal {0} {1}", ddlProTureProfit.Text, txtProTureProfit.Text);
            }
          
            string KAO_POType = "";
            string NO_Kao_POType = "";
            string PoTypeList = "";
            if (cbKaoAll.Checked == false)
            {
                string ids = "";
                foreach (ListItem item in cbKaoList.Items)
                {
                    if (item.Selected)
                    {
                        ids += item.Value + ",";
                    }
                }
                if (!string.IsNullOrEmpty(ids))
                {
                    PoTypeList = ids.Trim(',');
                    KAO_POType = " and CG_POOrder.POType in (" + ids.Trim(',') + ")";
                    NO_Kao_POType = " CG_POOrder.POType not in (" + ids.Trim(',') + ") or ";
                }
                else
                {
                    PoTypeList = "-10";
                    KAO_POType = " and CG_POOrder.POType=-10";
                    NO_Kao_POType = " 1=1 or ";
                }
            }
            else
            {
                PoTypeList = "-1";
            }

            if (!string.IsNullOrEmpty(txtStartTime.Text))
            {
                if (CommHelp.VerifesToDateTime(txtStartTime.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('启用时间 格式错误！');</script>");
                    return;
                }
            }
            var StartTime = Convert.ToDateTime(txtStartTime.Text);
               var fh = "";
               var fuhao_E = "";
            if (ddlZhangQI.Text != "-1")
            { 
                if (ddlZhangQI.Text == "1")
                {
                    fh = "<";
                    fuhao_E = ">=";
                }
                if (ddlZhangQI.Text == "2")
                {
                    fh = "<=";
                    fuhao_E = ">";
                }
            }
            string zhangQi = "where 1=1 ";

            if (ddlTrueZhangQI.Text != "-1")
            {
                if (ddlTrueZhangQI.Text == "1")
                {
                    zhangQi += string.Format(" and ZhangQi<=30"); 
                }
                if (ddlTrueZhangQI.Text == "2")
                {
                    zhangQi += string.Format(" and ZhangQi<=60 and ZhangQi>30");                    
                }
                if (ddlTrueZhangQI.Text == "3")
                {
                    zhangQi += string.Format(" and ZhangQi<=90 and ZhangQi>60");                   
                }
                if (ddlTrueZhangQI.Text == "4")
                {
                    zhangQi += string.Format(" and ZhangQi<=120 and ZhangQi>90");
                }
                if (ddlTrueZhangQI.Text == "5")
                {
                    zhangQi += string.Format(" and ZhangQi>120"); 
                }
            }


            List<KPI_SellModel> pOOrderList = this.POSer.KIP_SellReport(sql, having, fuhao, StartTime, fh, fuhao_E, KAO_POType, NO_Kao_POType, PoTypeList, contactWhere, zhangQi);
           
            //AspNetPager1.RecordCount = pOOrderList.Count;
            //this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind(); 
            
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (txtFrom.Text == ""|| txtTo.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('项目日期必填！');</script>"));
                return;
            }
            //AspNetPager1.CurrentPageIndex = 1;

            lblDateMess.Text = string.Format("项目日期：{0} 至 {1}",txtFrom.Text,txtTo.Text);

            string selectType = "";
            foreach (ListItem item in cbKaoList.Items)
            {
                if (item.Selected||cbKaoAll.Checked)
                {
                    selectType += item.Text + ",";
                }
            }
            
            selectType = selectType.Trim(',');
            lblProjectInfo.Text = string.Format("超期项目数是统计 项目类别为{0}的项目", selectType);
            Show();
        }

        public void BindData()
        {     
            Show();            
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
                 
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
        }






        private void setValue(Label control, string value)
        {
            control.Text = value;
        }

        protected void gvExvel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
    }
}
