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
using System.Data.SqlClient;

namespace VAN_OA.JXC
{
    public partial class Temp_JXC_YuShouKuanTotal : BasePage
    {
        JXC_REPORTService POSer = new JXC_REPORTService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TB_ModelService modelService = new TB_ModelService();

                this.gvModel.DataSource = modelService.GetListArray(""); ;
                this.gvModel.DataBind();

                txtYuGuDaoKuan_1.Text = DateTime.Now.ToString("yyyy-MM-dd");

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
                List<JXC_REPORTTotal> pOOrderList = new List<JXC_REPORTTotal>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //if (VAN_OA.JXC.SysObj.IfShowAll(SysObj.JXC_REPORTTotalList, Session["currentUserId"]) == false)
                //{
                //    ViewState["showAll"] = false;
                //}


                bool showAll = true;
                if (QuanXian_ShowAll("项目预收账款系统") == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }



                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll("项目预收账款系统", Session["currentUserId"], "WFScanDepartList") == true)
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

                if (NewShowAll_textName("项目预收账款系统", "可导出"))
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

                    ddlIsSpecial.Text = "-1";
                    Show();
                }
            }
        }



        private void Show()
        {
            string sql = " ";

            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and exists (select id from tb_User where IFZhui=0 and {0} and CG_POOrder.appName=id)", where);
            }
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and Temp_YuShouKuan.PONo like '%{0}%'", txtPONo.Text.Trim());
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and Temp_YuShouKuan.POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Temp_YuShouKuan.PODate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and Temp_YuShouKuan.PODate<='{0} 23:59:59'", txtTo.Text);
            }


            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and Temp_YuShouKuan.GuestName  like '%{0}%'", txtGuestName.Text.Trim());
            }
            var isColse = "";
            if (ddlIsClose.Text != "-1")
            {
                isColse += " and Temp_YuShouKuan.IsClose=" + ddlIsClose.Text;
            }

            if (ddlIsSelect.Text != "-1")
            {
                isColse += " and Temp_YuShouKuan.IsSelected=" + ddlIsSelect.Text;
            }
            if (ddlPOTyle.Text != "-1")
            {
                isColse += " and POType=" + ddlPOTyle.Text;
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
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and Temp_YuShouKuan.Model='{0}'", ddlModel.Text);
            }
            if (ddlGuestTypeList.SelectedValue != "全部")
            {
                sql += string.Format(" and Temp_YuShouKuan.GuestType='{0}'", ddlGuestTypeList.SelectedValue);
            }

            if (ddlGuestProList.SelectedValue != "-2")
            {
                sql += string.Format(" and Temp_YuShouKuan.GuestPro={0}", ddlGuestProList.SelectedValue);
            }



            if (ddlUser.Text == "-1")//显示所有用户
            {
                if (ddlIsSpecial.Text != "-1")
                {
                    sql += string.Format(" and IsSpecial={1} {0} ", isColse, ddlIsSpecial.Text);
                }

            }
            else if (ddlUser.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
                if (ddlIsSpecial.Text != "-1")
                {
                    sql += string.Format(" and CG_POOrder.AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') and IsSpecial={2} {1}",
                         model.LoginIPosition, isColse, ddlIsSpecial.Text);

                }
                else
                {
                    sql += string.Format(" and CG_POOrder.AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}')  {1}",
                        model.LoginIPosition, isColse);
                }
            }
            else
            {

                if (ddlIsSpecial.Text != "-1")
                {
                    sql += string.Format(" and CG_POOrder.AppName={0} and IsSpecial={2} {1} ", ddlUser.Text, isColse, ddlIsSpecial.Text);
                }
                else
                {
                    sql += string.Format(" and CG_POOrder.AppName={0}  {1} ", ddlUser.Text, isColse);

                }
            }

            if (CheckBox1.Checked || CheckBox2.Checked || CheckBox3.Checked || CheckBox4.Checked)
            {
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
            }



            if (CheckBox5.Checked && CheckBox6.Checked)
            {
                sql += string.Format("  (SellFPTotal<> goodSellTotal or  SellFPTotal is null)  and (InvoTotal<> goodSellTotal or InvoTotal is null)");
            }
            else if (CheckBox5.Checked || CheckBox6.Checked)
            {
                if (CheckBox5.Checked)
                {
                    sql += string.Format("  SellFPTotal<> goodSellTotal or  SellFPTotal is null ");
                }
                if (CheckBox6.Checked)
                {
                    sql += string.Format("  InvoTotal<> goodSellTotal or InvoTotal is null ");
                }
            }


            if (ddlFuHao.Text != "-1")
            {
                sql += string.Format(" and SumPOTotal {0} isnull(goodSellTotal,0)", ddlFuHao.Text);
            }
            if (ddlPOFaTotal.Text != "-1")
            {
                sql += string.Format(" and SumPOTotal {0} isnull(SellFPTotal,0)", ddlPOFaTotal.Text);
            }
            if (ddlShiJiDaoKuan.Text != "-1")
            {
                sql += string.Format(" and SumPOTotal {0} isnull(InvoTotal,0)", ddlShiJiDaoKuan.Text);
            }
            if (ddlEquPOTotal.Text != "-1")
            {
                if (CommHelp.VerifesToNum(txtEquTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目金额 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and SumPOTotal{0}{1}", ddlEquPOTotal.Text, txtEquTotal.Text);
            }



            if (ddlJingLiTotal.Text != "-1")
            {
                sql += string.Format(" and maoliTotal {0} (isnull(InvoTotal,0)-isnull(goodTotal,0))", ddlJingLiTotal.Text);
            }

            if (!string.IsNullOrEmpty(txtProProfit.Text) && ddlProProfit.Text != "-1")
            {
                if (CommHelp.VerifesToNum(txtProProfit.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目净利 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and maoliTotal {0} {1}", ddlProProfit.Text, txtProProfit.Text);
            }

            if (!string.IsNullOrEmpty(txtProTureProfit.Text) && ddlProTureProfit.Text != "-1")
            {
                if (CommHelp.VerifesToNum(txtProTureProfit.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('实际净利 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and InvoTotal-goodTotal {0} {1}", ddlProTureProfit.Text, txtProTureProfit.Text);
            }

            if (ddlJingLi.Text != "-1" && !string.IsNullOrEmpty(txtJingLi.Text))
            {
                if (CommHelp.VerifesToNum(txtJingLi.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('净利润率 格式错误！');</script>");
                    return;
                }

                sql += string.Format(" and JingLi {0} {1}", ddlJingLi.Text, txtJingLi.Text);
            }

            //预计到款
            if (txtYuDaoDate.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtYuDaoDate.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('预估到款日 格式错误！');</script>");
                    return;
                }
            }
            if (txtYuDaoDateTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtYuDaoDateTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('预估到款日 格式错误！');</script>");
                    return;
                }
            }
            if (txtYuGuDaoKuan_1.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtYuGuDaoKuan_1.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('预估到款日期 格式错误！');</script>");
                    return;
                }
            }
            //最近开票日
            if (txtBillDate.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtBillDate.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('最近开票日 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and BillDate>='{0} 00:00:00'", txtBillDate.Text);
            }
            if (txtBillDateTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtBillDateTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('最近开票日 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and BillDate<='{0} 23:59:59'", txtBillDateTo.Text);
            }
            if (cbWuKaiPiaoRi.Checked)
            {
                sql += string.Format(" and BillDate is null");
            }
            //计算开票日
            if (txtJSKaiPiaoDate.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtJSKaiPiaoDate.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('计算开票日 格式错误！');</script>");
                    return;
                }
            }
            if (txtJSKaiPiaoDateTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtJSKaiPiaoDateTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('计算开票日 格式错误！');</script>");
                    return;
                }
            }
            //实际到款日
            if (txtShiJiDate.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtShiJiDate.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('实际到款日 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and MaxDaoKuanDate>='{0} 00:00:00'", txtShiJiDate.Text);
            }
            if (txtShiJiDateTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtShiJiDateTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('实际到款日 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and MaxDaoKuanDate<='{0} 23:59:59'", txtShiJiDateTo.Text);
            }
            //经验账期
            if (ddlAvg_ZQ.Text != "-1" && ddlAvg_ZQ.Text != "无")
            {
                if (CommHelp.VerifesToNum(txtAvg_ZQ.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('经验账期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and isnull(AVG_ZQ,0){0}{1}", ddlAvg_ZQ.Text, txtAvg_ZQ.Text.Trim());
            }
            if (ddlAvg_ZQ.Text == "无")
            {
                sql += string.Format(" and isnull(AVG_ZQ,0)=0");               
            }

            //预估位序
            if (txtDaoKuanNumber.Text != "")
            {
                if (CommHelp.VerifesToNum(txtDaoKuanNumber.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('预估位序 格式错误！');</script>");
                    return;
                }              

                if (ddlDaoKuanNumber.Text == "无")
                { 
                    sql += string.Format(" and DaoKuanNumber is null");
                }
                else
                {
                    sql += string.Format(" and DaoKuanNumber{0}{1}", ddlDaoKuanNumber.Text, txtDaoKuanNumber.Text.Trim());
                }   
            }
            //预估到款
            if (txtYuGuDaoKuan.Text != "")
            {
                if (CommHelp.VerifesToNum(txtYuGuDaoKuan.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('预估到款 格式错误！');</script>");
                    return;
                }
                decimal jingLi = Convert.ToDecimal(txtYuGuDaoKuan.Text);
                if (ddlYuGuDaoKuan.Text == "无")
                {                     
                    sql += string.Format(" and YuGuDaoKuanTotal=0");
                }
                else
                {
                    if (ddlYuGuDaoKuan.Text != "-1")
                    {
                        sql += string.Format(" and YuGuDaoKuanTotal{0}{1}", ddlYuGuDaoKuan.Text, jingLi);
                    }
                }
            }

            if (ddlTrueZhangQI.Text != "-1")
            {
                if (ddlTrueZhangQI.Text == "1")
                {
                    sql += string.Format(" and trueZhangQi<=30");                   
                }
                if (ddlTrueZhangQI.Text == "2")
                {                    
                    sql += string.Format(" and trueZhangQi>30 and trueZhangQi<=60");
                }
                if (ddlTrueZhangQI.Text == "3")
                {
                    sql += string.Format(" and trueZhangQi>60 and trueZhangQi<=90"); 
                }
                if (ddlTrueZhangQI.Text == "4")
                {
                    sql += string.Format(" and trueZhangQi>90 and trueZhangQi<=120"); 
                }
                if (ddlTrueZhangQI.Text == "5")
                {
                    sql += string.Format(" and trueZhangQi>120 "); 
                }
            }
            //计算开票日
            if (txtJSKaiPiaoDate.Text != "")
            { 
                sql += string.Format(" and JSKaiPiaoDate>='{0} 00:00:00'", txtJSKaiPiaoDate.Text);
            }
            if (txtJSKaiPiaoDateTo.Text != "")
            {               
                sql += string.Format(" and JSKaiPiaoDate<='{0} 23:59:59'", txtJSKaiPiaoDateTo.Text);
            }
            if (ddlYuGuDaoKuan_1.Text != "-1" && !string.IsNullOrEmpty(txtYuGuDaoKuan_1.Text))
            {
                if (ddlYuGuDaoKuan_1.Text == "无")
                {
                    sql += string.Format(" and YuGuDaoKuanDate is null");
                }
                else
                {
                    sql += string.Format(" and YuGuDaoKuanDate{0}'{1}'", ddlYuGuDaoKuan_1.Text, txtYuGuDaoKuan_1.Text);
                }
            }

            //预计到款
            if (txtYuDaoDate.Text != "")
            {              
                sql += string.Format(" and YuGuDaoKuanDate>='{0} 00:00:00'", txtYuDaoDate.Text);
            }
            if (txtYuDaoDateTo.Text != "")
            { 
                sql += string.Format(" and YuGuDaoKuanDate<='{0} 23:59:59'", txtYuDaoDateTo.Text);
            }
            if (ddlDELAY.Text != "-1")
            {
                sql += string.Format(" and DELAY={0}", ddlDELAY.Text);
            }
            

            PagerDomain page = new PagerDomain();
            page.CurrentPageIndex = AspNetPager1.CurrentPageIndex;
            DataTable sumDT;
            if (isExcel)
            {
                page.PageSize = 100000;
            }

       
            List<JXC_REPORTTotal> pOOrderList = this.POSer.Temp_YuShouKuan_GetListArray_Total(sql, page, out sumDT);

           
           
        
            var getAllPONos = pOOrderList.Aggregate("", (current, m) => current + ("'" + m.PONo + "',")).Trim(',');
            lblVisAllPONO.Text = getAllPONos;
            decimal jlr = Convert.ToDecimal(sumDT.Rows[0]["maoliTotal"] == DBNull.Value ? 0 : sumDT.Rows[0]["maoliTotal"]);
            lblJLR.Text = string.Format("{0:n6}", jlr);

            var goodSellTotal = Convert.ToDecimal(sumDT.Rows[0]["goodSellTotal"] == DBNull.Value ? 0 : sumDT.Rows[0]["goodSellTotal"]);

            lblXSE.Text = string.Format("{0:n6}", goodSellTotal);


            lblYuGuDaoKuanTotal.Text = string.Format("{0:n6}", Convert.ToDecimal(sumDT.Rows[0]["YuGuDaoKuanTotal"] == DBNull.Value ? 0 : sumDT.Rows[0]["YuGuDaoKuanTotal"]));

            var InvoiceTotal = Convert.ToDecimal(sumDT.Rows[0]["InvoiceTotal"] == DBNull.Value ? 0 : sumDT.Rows[0]["InvoiceTotal"]);
            var goodTotal = Convert.ToDecimal(sumDT.Rows[0]["goodTotal"] == DBNull.Value ? 0 : sumDT.Rows[0]["goodTotal"]);
            var trueLiRun = InvoiceTotal- goodTotal;
            lblSJLR.Text = string.Format("{0:n6}", trueLiRun);

            lblFP.Text = string.Format("{0:n6}", Convert.ToDecimal(sumDT.Rows[0]["SellFPTotal"] == DBNull.Value ? 0 : sumDT.Rows[0]["SellFPTotal"]));
            // 项目总额：XXX      
            decimal allPoTotal = Convert.ToDecimal(sumDT.Rows[0]["SumPOTotal"] == DBNull.Value ? 0 : sumDT.Rows[0]["SumPOTotal"]);
            lblAllPoTotal.Text = string.Format("{0:n6}", allPoTotal);
            //项目总利润率：YYY = 项目净利合计/XXX 
            lblAllLRLv.Text = "0";
            if (allPoTotal > 0)
            {
                lblAllLRLv.Text = string.Format("{0:f2}", (jlr / allPoTotal * 100));
            }
            // 实际总利润率：ZZZ=  实际利润合计/XXX  
            lblAllTrunLv.Text = "0";
            if (allPoTotal > 0)
            {
                lblAllTrunLv.Text = string.Format("{0:f2}", (trueLiRun / allPoTotal * 100));
            }
            //项目到款总额：TTT= 实到帐的合计 
         
            lblAllDaoKuan.Text = string.Format("{0:n6}", InvoiceTotal);
            //到款率： VVV=TTT/XXX   
            lblAllDaoKuanLv.Text = "0";
            if (allPoTotal > 0)
            {
                lblAllDaoKuanLv.Text = string.Format("{0:f2}", (InvoiceTotal / allPoTotal * 100));
            }

            //开票率：LLL= 发票总额合计 /XXX
            var faPiaoTotal = Convert.ToDecimal(sumDT.Rows[0]["SellFPTotal"] == DBNull.Value ? 0 : sumDT.Rows[0]["SellFPTotal"]);
            lblAllFaPiaoLv.Text = "0";
            if (allPoTotal > 0)
            {
                lblAllFaPiaoLv.Text = string.Format("{0:f2}", (faPiaoTotal / allPoTotal * 100));
            }
           
            AspNetPager1.RecordCount = page.TotalCount; 

            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

        

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
                if (m.YuGuDaoKuanDate != null && m.YuGuDaoKuanDate < DateTime.Now && m.Model != "模型8")
                {
                    e.Row.BackColor = System.Drawing.Color.Khaki;
                }

                //在模型8中我们发现最近开票日之后的到款已完全到款，这时候预估到款=0，我们的DELAY判定需要修正一下，按如下：
                //预估到款日这列的日期，如 < 今天的日期且预估到款金额 > 0，说明这个项目付款有DELAY，请帮我把整个这一行的背景设置成土黄色。按此修正
                if (m.Model == "模型8" && m.YuGuDaoKuanDate != null && m.YuGuDaoKuanDate < DateTime.Now && m.YuGuDaoKuanTotal > 0)
                {
                    e.Row.BackColor = System.Drawing.Color.Khaki;
                }



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

        bool isExcel = false; 

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            isExcel = true;
            gvMain.AllowPaging = false;
            Show();
            toExcel();
            gvMain.AllowPaging = false;
            isExcel = false;
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

        protected void cbWuKaiPiaoRi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbWuKaiPiaoRi.Checked)
            {
                txtBillDate.Text = "";
                txtBillDateTo.Text = "";
                txtBillDate.Enabled = false;
                txtBillDateTo.Enabled = false;
                ImageButton4.Enabled = false;
                ImageButton5.Enabled = false;
            }
            else
            {
                txtBillDate.Enabled = true;
                txtBillDateTo.Enabled = true;
                ImageButton4.Enabled = true;
                ImageButton5.Enabled = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DBHelp.ExeCommand("truncate table Temp_YuShouKuan;");
            List<JXC_REPORTTotal> pOOrderList = this.POSer.YuShouKuan_GetListArray_Total("", "", "");

           

            DataTable dataTable = GetTableSchema();

            foreach (var model in pOOrderList)
            {
                var dataRow = dataTable.NewRow();
                dataRow[1] = model.SumPOTotal;
                dataRow[2] = 0;
                dataRow[3] = model.MinDaoKuanDate;
                dataRow[4] = model.MinOutDate;
                dataRow[5] = model.MaxDaoKuanDate;
                dataRow[6] = model.IsClose;
                dataRow[7] = model.PONo;
                dataRow[8] = model.POName;
                dataRow[9] = model.PODate;
                dataRow[10] = model.GuestName;
                dataRow[11] = model.GuestType;
                dataRow[12] = model.GuestPro;
                dataRow[13] = model.goodSellTotal;
                dataRow[14] = model.goodTotal;
                dataRow[15] = model.maoliTotal;
                dataRow[16] = model.FPTotal;
                dataRow[17] = model.ZhangQiTotal;
                dataRow[18] = model.AE;
                dataRow[19] = model.INSIDE;
                dataRow[20] = model.AEPer;
                dataRow[21] = model.INSIDEPer;
                dataRow[22] = model.InvoiceTotal;
                dataRow[23] = model.SellFPTotal;
                dataRow[24] = model.Model;
                dataRow[25] = model.BillDate;
                dataRow[26] = model.DaoKuanNumber;
                dataRow[27] = model.Avg_ZQ;
                dataRow[28] = model.ZQ;
                dataRow[29] = model.MinDaoKuanTime_ZQ;
                dataRow[30] = model.MinBillDate;
                dataRow[31] = model.MinFPTime;
                dataRow[32] = null;
                dataRow[33] = model.YuGuDaoKuanTotal;
                dataRow[34] = model.YuGuDaoKuanDate;
                dataRow[35] = model.DaoKuanNumber;
                dataRow[36] = model.JSKaiPiaoDate;
                dataRow[37] = model.trueZhangQi;
                dataRow[38] = model.JingLi;

                var m = model;

                if ((m.YuGuDaoKuanDate != null && m.YuGuDaoKuanDate < DateTime.Now && m.Model != "模型8")
                    || (m.Model == "模型8" && m.YuGuDaoKuanDate != null && m.YuGuDaoKuanDate < DateTime.Now && m.YuGuDaoKuanTotal > 0))
                {
                    dataRow[39] = 1;
                }
                if (!((m.YuGuDaoKuanDate != null && m.YuGuDaoKuanDate < DateTime.Now && m.Model != "模型8")
                     || (m.Model == "模型8" && m.YuGuDaoKuanDate != null && m.YuGuDaoKuanDate < DateTime.Now && m.YuGuDaoKuanTotal > 0)))
                {
                    dataRow[39] = 2;
                }

               
                dataTable.Rows.Add(dataRow);
            }

            if (dataTable.Rows.Count > 0)
            {
                BatchSaveData(dataTable, "Temp_YuShouKuan");
            }
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="sqlTableName"></param>
        public void BatchSaveData(DataTable dataTable, string sqlTableName)
        {
            using (var sqlBulkCopy = new SqlBulkCopy(DBHelp.getConn().ConnectionString))
            {
                sqlBulkCopy.DestinationTableName = "[" + sqlTableName + "]";
                sqlBulkCopy.BatchSize = dataTable.Rows.Count;
                if (dataTable != null && dataTable.Rows.Count != 0)
                {
                    sqlBulkCopy.WriteToServer(dataTable);
                }
                sqlBulkCopy.Close();
            }
            dataTable = null;
        }

        private DataTable GetTableSchema()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Id") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("SumPOTotal") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("DaoKuanCount") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("MinDaoKuanDate") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("MinOutDate") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("MaxDaoKuanDate") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("IsClose") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("PONo") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("POName") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("PODate") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("GuestName") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("GuestType") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("GuestPro") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("goodSellTotal") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("goodTotal") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("maoliTotal") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("FPTotal") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("ZhangQiTotal") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("AE") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("INSIDE") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("AEPer") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("INSIDEPer") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("InvoTotal") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("SellFPTotal") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("Model") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("BillDate") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("BillCount") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("AVG_ZQ") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("ZQ") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("MinDaoKuanTime_ZQ") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("MinBillDate") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("MinFPTime") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("DaoKuanDate") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("YuGuDaoKuanTotal") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("YuGuDaoKuanDate") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("DaoKuanNumber") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("JSKaiPiaoDate") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("trueZhangQi") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("JingLi") });
            dataTable.Columns.AddRange(new DataColumn[] { new DataColumn("DELAY") });
            return dataTable;
        }
    }
}
