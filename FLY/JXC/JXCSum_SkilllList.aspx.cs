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
using System.IO;

namespace VAN_OA.JXC
{
    public partial class JXCSum_SkilllList : BasePage
    {
        JXC_REPORTService POSer = new JXC_REPORTService();
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

                //主单
                List<SumSkillTotal> sumSkillList = new List<SumSkillTotal>();
                this.gvMain.DataSource = sumSkillList;
                this.gvMain.DataBind();




                bool showAll = true;
                if (QuanXian_ShowAll("工程技术派工统计报表") == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }



                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll("工程技术派工统计报表", Session["currentUserId"], "WFScanDepartList") == true)
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

                //                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='可导出'
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='销售报表汇总') and sys_Object.AutoID is not null", Session["currentUserId"]);
                //                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                if (NewShowAll_textName("工程技术派工统计报表", "可导出"))
                {
                    btnExcel.Visible = true;
                }
                else
                {
                    btnExcel.Visible = false;
                }


                string sql = string.Format("select loginName,ID from tb_User where loginIPosition='技术部' AND loginStatus='在职' and loginName<>'';");

                List<User> getUsers = userSer.getUserReportTos(sql);
                getUsers = getUsers.FindAll(T => string.IsNullOrEmpty(T.LoginName) == false);
                getUsers.Insert(0, new Model.User { Id = -1, LoginName = "全部" });
                ddlPaiGong.DataSource = getUsers;
                ddlPaiGong.DataBind();

                ddlPaiGong.DataTextField = "LoginName";
                ddlPaiGong.DataValueField = "Id";

                if (Request["PONo"] != null)
                {
                    txtPONo.Text = Request["PONo"].ToString();

                    ddlIsSpecial.Text = "-1";
                    Show();
                }
            }
        }



        private void Show()
        {
            if (ddlFKDF.Text != "-1" && !string.IsNullOrEmpty(txtFKDF.Text))
            {
                if (CommHelp.VerifesToNum(txtFKDF.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('反馈得分 格式错误！');</script>");
                    return;
                }
            }
            if (ddlAllScore.Text != "-1" && !string.IsNullOrEmpty(txtAllScore.Text))
            {
                if (CommHelp.VerifesToNum(txtAllScore.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('综合得分 格式错误！');</script>");
                    return;
                }
            }
            string sql = " ";

            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and exists (select id from tb_User where IFZhui=0 and {0} and CG_POOrder.appName=id)", where);
            }
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
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
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
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and Model='{0}'", ddlModel.Text);
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
                if (ddlIsSpecial.Text != "-1")
                {
                    sql += string.Format(" and IsSpecial={1} {0} ", isColse, ddlIsSpecial.Text);
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

                if (ddlIsSpecial.Text != "-1")
                {
                    sql += string.Format(" and CG_POOrder.AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') and IsSpecial={2} {1}",
                         model.LoginIPosition, isColse, ddlIsSpecial.Text);
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

                if (ddlIsSpecial.Text != "-1")
                {
                    sql += string.Format(" and CG_POOrder.AppName={0} and IsSpecial={2} {1} ", ddlUser.Text, isColse, ddlIsSpecial.Text);
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

            if (ddlFKDF.Text != "-1" && !string.IsNullOrEmpty(txtFKDF.Text))
            {
                fuhao += string.Format(" and MyValue{0}{1}", ddlFKDF.Text, txtFKDF.Text);
            }
            if (ddlFuHao.Text != "-1")
            {
                fuhao = string.Format(" where SumPOTotal {0} isnull(goodSellTotal,0)", ddlFuHao.Text);
            }
            if (ddlPOFaTotal.Text != "-1")
            {
                fuhao += string.Format(" and SumPOTotal {0} isnull(SellFPTotal,0)", ddlPOFaTotal.Text);
            }
            if (ddlShiJiDaoKuan.Text != "-1")
            {
                fuhao += string.Format(" and SumPOTotal {0} isnull(InvoTotal,0)", ddlShiJiDaoKuan.Text);
            }
            if (ddlEquPOTotal.Text != "-1")
            {
                if (CommHelp.VerifesToNum(txtEquTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目金额 格式错误！');</script>");
                    return;
                }
                fuhao += string.Format(" and SumPOTotal{0}{1}", ddlEquPOTotal.Text, txtEquTotal.Text);
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
                    return;
                }
                fuhao += string.Format(" and maoliTotal {0} {1}", ddlProProfit.Text, txtProProfit.Text);
            }

            if (!string.IsNullOrEmpty(txtProTureProfit.Text) && ddlProTureProfit.Text != "-1")
            {
                if (CommHelp.VerifesToNum(txtProTureProfit.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('实际净利 格式错误！');</script>");
                    return;
                }
                fuhao += string.Format(" and InvoTotal-goodTotal {0} {1}", ddlProTureProfit.Text, txtProTureProfit.Text);
            }
            string paiSql = "";
            if (ddlPaiGong.Text != "-1")
            {
                paiSql += string.Format(" and OutDispater={0}", ddlPaiGong.Text);
            }
            string mess = "";
            if (txtPGFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtPGFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('派工时间 格式错误！');</script>");
                    return;
                }
                paiSql += string.Format(" and DisDate>='{0} 00:00:00'", txtPGFrom.Text);
                mess += txtPGFrom.Text+"~";
            }

            if (txtPGTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtPGTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('派工时间 格式错误！');</script>");
                    return;
                }
                paiSql += string.Format(" and DisDate<='{0} 23:59:59'", txtPGTo.Text);

                mess += txtPGTo.Text + "~";
            }
            lblPaiDateMess.Text = mess;

            if (ddlJingLi.Text != "-1" && !string.IsNullOrEmpty(txtJingLi.Text))
            {
                if (CommHelp.VerifesToNum(txtJingLi.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('净利润率 格式错误！');</script>");
                    return;
                }
            }
            List<SumSkillTotal> sumSkillTotals = this.POSer.GetSumSkill_Total(sql, having, fuhao, paiSql);
            //if (ddlTrueZhangQI.Text != "-1")
            //{
            //    if (ddlTrueZhangQI.Text == "1")
            //    {
            //        pOOrderList = pOOrderList.FindAll(T => T.trueZhangQi <= 30);
            //    }
            //    if (ddlTrueZhangQI.Text == "2")
            //    {
            //        pOOrderList = pOOrderList.FindAll(T => T.trueZhangQi > 30 && T.trueZhangQi <= 60);
            //    }
            //    if (ddlTrueZhangQI.Text == "3")
            //    {
            //        pOOrderList = pOOrderList.FindAll(T => T.trueZhangQi > 60 && T.trueZhangQi <= 90);
            //    }
            //    if (ddlTrueZhangQI.Text == "4")
            //    {
            //        pOOrderList = pOOrderList.FindAll(T => T.trueZhangQi > 90 && T.trueZhangQi <= 120);
            //    }
            //    if (ddlTrueZhangQI.Text == "5")
            //    {
            //        pOOrderList = pOOrderList.FindAll(T => T.trueZhangQi > 120);
            //    }
            //}


            //if (ddlJingLi.Text != "-1" && !string.IsNullOrEmpty(txtJingLi.Text))
            //{

            //    decimal jingLi = Convert.ToDecimal(txtJingLi.Text);
            //    if (ddlJingLi.Text == ">")
            //    {
            //        pOOrderList = pOOrderList.FindAll(t => t.JingLi > jingLi);
            //    }
            //    if (ddlJingLi.Text == "<")
            //    {
            //        pOOrderList = pOOrderList.FindAll(t => t.JingLi < jingLi);
            //    }
            //    if (ddlJingLi.Text == ">=")
            //    {
            //        pOOrderList = pOOrderList.FindAll(t => t.JingLi >= jingLi);
            //    }
            //    if (ddlJingLi.Text == "<=")
            //    {
            //        pOOrderList = pOOrderList.FindAll(t => t.JingLi <= jingLi);
            //    }
            //    if (ddlJingLi.Text == "=")
            //    {
            //        pOOrderList = pOOrderList.FindAll(t => t.JingLi == jingLi);
            //    }
            //}
            //反馈得分
            if (ddlFKDF.Text != "-1" && !string.IsNullOrEmpty(txtFKDF.Text))
            {
                if (CommHelp.VerifesToNum(txtFKDF.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('反馈得分 格式错误！');</script>");
                    return;
                }
                fuhao = ddlFKDF.Text;
                decimal allScore = Convert.ToDecimal(txtFKDF.Text);
                if (fuhao == ">")
                {
                    sumSkillTotals = sumSkillTotals.FindAll(t => t.SumValue > allScore);
                }
                if (fuhao == "<")
                {
                    sumSkillTotals = sumSkillTotals.FindAll(t => t.SumValue < allScore);
                }
                if (fuhao == ">=")
                {
                    sumSkillTotals = sumSkillTotals.FindAll(t => t.SumValue >= allScore);
                }
                if (fuhao == "<=")
                {
                    sumSkillTotals = sumSkillTotals.FindAll(t => t.SumValue <= allScore);
                }
                if (fuhao == "=")
                {
                    sumSkillTotals = sumSkillTotals.FindAll(t => t.SumValue == allScore);
                }
                if (fuhao == "<>")
                {
                    sumSkillTotals = sumSkillTotals.FindAll(t => t.SumValue == allScore);
                }
            }

            //综合得分
            if (ddlAllScore.Text != "-1" && !string.IsNullOrEmpty(txtAllScore.Text))
            {
                if (CommHelp.VerifesToNum(txtAllScore.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('综合得分 格式错误！');</script>");
                    return;
                }
                fuhao = ddlAllScore.Text;
                decimal allScore = Convert.ToDecimal(txtAllScore.Text);
                if (fuhao == ">")
                {
                    sumSkillTotals = sumSkillTotals.FindAll(t => t.SumScore > allScore);
                }
                if (fuhao == "<")
                {
                    sumSkillTotals = sumSkillTotals.FindAll(t => t.SumScore < allScore);
                }
                if (fuhao == ">=")
                {
                    sumSkillTotals = sumSkillTotals.FindAll(t => t.SumScore >= allScore);
                }
                if (fuhao == "<=")
                {
                    sumSkillTotals = sumSkillTotals.FindAll(t => t.SumScore <= allScore);
                }
                if (fuhao == "=")
                {
                    sumSkillTotals = sumSkillTotals.FindAll(t => t.SumScore == allScore);
                }
                if (fuhao == "<>")
                {
                    sumSkillTotals = sumSkillTotals.FindAll(t => t.SumScore == allScore);
                }
            }



            AspNetPager1.RecordCount = sumSkillTotals.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = sumSkillTotals;
            this.gvMain.DataBind();

            lblGong_Socre.Text = string.Format("{0:n2}", sumSkillTotals.Sum(t=>t.Gong_Score));
            lblLing_Score.Text = string.Format("{0:n2}", sumSkillTotals.Sum(t => t.Ling_Score));
            lblXi_Score.Text = string.Format("{0:n2}", sumSkillTotals.Sum(t => t.Xi_Score));
            lblAllScore.Text = string.Format("{0:n2}", sumSkillTotals.Sum(t => t.SumScore));
            lblFanKuiScore.Text = string.Format("{0:n2}", sumSkillTotals.Sum(t => t.SumValue));

            decimal AvgScore = 0;
            decimal AvgValue = 0;
            if (sumSkillTotals.Count > 0) {
                AvgScore = sumSkillTotals.Sum(t => t.SumScore) / sumSkillTotals.Count;
                AvgValue = sumSkillTotals.Sum(t => t.SumValue) / sumSkillTotals.Count;

            }
            lblAvgScore.Text = string.Format("{0:n2}", AvgScore);
            lblAvgFKscore.Text = string.Format("{0:n2}", AvgValue);

        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }
        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            var id = gvMain.DataKeys[e.NewEditIndex].Value;
            if (id != null && id.ToString() != "0")
            {
                string url = "/EFrom/Dispatching.aspx?ProId=1&Type=Edit&allE_id=" + id
                + "&EForm_Id=" + DBHelp.ExeScalar(string.Format("select id from tb_EForm WHERE allE_id={0} AND proId=1", id));


                Response.Write(string.Format("<script>window.open('{0}','_blank')</script>", url));
            }

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

            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                Response.Redirect("~/JXC/JXC_REPORTList.aspx?PONo=" + e.CommandArgument + "&IsSpecial=" + ddlIsSpecial.Text);
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
            gvMain.AllowPaging = false;
            Show();
            toExcel();
            gvMain.AllowPaging = false;
            Show();
        }


        void toExcel()
        {
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");

            string fileName = "export.xls";
            string style = @"<style> .text { mso-number-format:\@; } </script> ";
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.gvMain.RenderControl(htw);
            Response.Write(style);
            Response.Write(sw.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for

            //为了保险期间还可以在这里加入判断条件防止HTML中已经存在该ID
        }
    }
}
