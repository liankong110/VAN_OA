﻿using System;
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

        public List<KPI_SellModel> KPIList(int Type,out string ErrorMsg)
        {
            ErrorMsg = "";
            string sql = " ";
            string contactWhere = " where 1=1 ";
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    ErrorMsg = "1";
                    return new List<KPI_SellModel>();
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
                ErrorMsg = "1";
                return new List<KPI_SellModel>();
            }

            if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                ErrorMsg = "1";
                return new List<KPI_SellModel>();
            }
            contactWhere += string.Format(" and [DateTime]>='{0} 00:00:00'", txtFrom.Text);
            contactWhere += string.Format(" and [DateTime]<='{0} 23:59:59'", txtTo.Text);

            if (Type == 1)
            {
                sql += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00' and CG_POOrder.PODate<='{1} 23:59:59'",
                   txtFrom.Text, txtTo.Text);
            }
            if (Type == 2)
            {
                sql += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00' and CG_POOrder.PODate<='{1} 23:59:59'",
                  Convert.ToDateTime(txtFrom.Text).AddDays(-120).ToString("yyyy-MM-dd"), Convert.ToDateTime(txtTo.Text).AddDays(-120).ToString("yyyy-MM-dd"));
            }
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
                         model.LoginIPosition, isColse);      }
                else
                {
                    sql += string.Format(" and CG_POOrder.AE in (select LOGINNAME from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}')  {1}",
                        model.LoginIPosition, isColse);
                }
            }
            else
            {

                if (cbIsSpecial.Checked)
                {
                    sql += string.Format(" and CG_POOrder.AE='{0}' and IsSpecial=0 {1} ", ddlUser.SelectedItem.Text, isColse);
                }
                else
                {
                    sql += string.Format(" and CG_POOrder.AE='{0}'  {1} ", ddlUser.SelectedItem.Text, isColse);                    
                } 
                contactWhere += string.Format(" and Name='{0}'", ddlUser.SelectedItem.Text);
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

            if (!string.IsNullOrEmpty(txtProProfit.Text) && ddlProProfit.Text != "-1")
            {
                if (CommHelp.VerifesToNum(txtProProfit.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目净利 格式错误！');</script>");
                    ErrorMsg = "1";
                    return new List<KPI_SellModel>();
                }
                fuhao += string.Format(" and maoliTotal {0} {1}", ddlProProfit.Text, txtProProfit.Text);
            }

            if (!string.IsNullOrEmpty(txtProTureProfit.Text) && ddlProTureProfit.Text != "-1")
            {
                if (CommHelp.VerifesToNum(txtProTureProfit.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('实际净利 格式错误！');</script>");
                    ErrorMsg = "1";
                    return new List<KPI_SellModel>();
                }
                fuhao += string.Format(" and InvoTotal-goodTotal {0} {1}", ddlProTureProfit.Text, txtProTureProfit.Text);
            }

            string KAO_POType = "";
            string NO_Kao_POType = "";
            string PoTypeList = "";
            if (Type == 1)
            {
                //PoTypeList = "-10";
                //KAO_POType = " and CG_POOrder.POType=-10";
                //NO_Kao_POType = " 1=1 or ";
            }
            if (Type == 2)
            {
                if (cbKaoAll.Checked == false)
                {
                    string ids = "";
                    string notids = "";
                    foreach (ListItem item in cbKaoList.Items)
                    {
                        if (item.Selected)
                        {
                            ids += item.Value + ",";
                        }
                        else
                        {
                            notids += item.Value + ",";
                        }
                    }
                    if (!string.IsNullOrEmpty(ids))
                    { 
                        sql+= " and CG_POOrder.POType in (" + ids.Trim(',') + ")"; 
                    }
                    if (!string.IsNullOrEmpty(notids))
                    {
                        sql += " and CG_POOrder.POType not in (" + notids.Trim(',') + ")";
                    }

                }
                else
                {
                    //PoTypeList = "-1";
                }
            } 

            string zhangQi = "where 1=1 ";

            if (Type == 2)
            {
                if (ddlZhangQI.Text != "-1")
                {
                    if (ddlZhangQI.Text == "1")
                    {
                        zhangQi += string.Format(" and ZhangQi>=120");

                       
                    }
                    if (ddlZhangQI.Text == "2")
                    {
                        zhangQi += string.Format(" and ZhangQi>120");
                      
                    }
                }
            }
            

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
            return this.POSer.KIP_SellReport(sql, having, fuhao,  KAO_POType, NO_Kao_POType, PoTypeList, contactWhere, zhangQi);
        }

        private void Show()
        {

            string errorMsg = "";
            List<KPI_SellModel> kpiList = KPIList(1, out errorMsg);
            if (errorMsg == "-1")
            {
                return;
            }            
            List<KPI_SellModel> chaoQiList = KPIList(2, out errorMsg);
            foreach (var model in kpiList)
            {
                int chaoQiCount = 0;
                var fModel = chaoQiList.Find(t => t.AE == model.AE);
                if (fModel != null)
                {
                    model.TimeOutCount = fModel.TimeOutCount;
                }
                else
                {
                    model.TimeOutCount = chaoQiCount;
                }
            }
            string contactWhere = " where 1=1 ";

            contactWhere += string.Format(" and [DateTime]>='{0} 00:00:00'", txtFrom.Text);
            contactWhere += string.Format(" and [DateTime]<='{0} 23:59:59'", txtTo.Text);

            if (ddlUser.Text != "-1" && ddlUser.Text != "0")
            {
                contactWhere += string.Format(" and Name='{0}'", ddlUser.SelectedItem.Text);
            }
            var contactList = POSer.Kpi_ContactList(contactWhere);

            var aeList = POSer.KPI_AE();
            foreach(var aeModel in aeList)
            {
                var fModel = kpiList.Find(t => t.AE == aeModel.AE);
                if (fModel != null)
                {
                    aeModel.TimeOutCount = fModel.TimeOutCount;
                    
                    aeModel.POTotal = fModel.POTotal;
                    aeModel.SellTotal = fModel.SellTotal;
                    aeModel.InvoiceTotal = fModel.InvoiceTotal;
                    aeModel.ProfitTotal = fModel.ProfitTotal;
                    aeModel.LastProfitTotal = fModel.LastProfitTotal;
                    aeModel.KP_Percent = fModel.KP_Percent;
                    aeModel.DK_Percent = fModel.DK_Percent;                    
                }
                var cModel = contactList.Find(t => t.AE == aeModel.AE);
                if (cModel != null)
                {                    
                    aeModel.NewContractCount = cModel.NewContractCount;
                    aeModel.OldContractCount = cModel.OldContractCount; 
                }
            }
            this.gvMain.DataSource = aeList;
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

            lblDateMess.Text = string.Format("项目日期：{0} 至 {1}", Convert.ToDateTime(txtFrom.Text).AddDays(-120).ToString("yyyy-MM-dd"),
                Convert.ToDateTime(txtTo.Text).AddDays(-120).ToString("yyyy-MM-dd"));

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
