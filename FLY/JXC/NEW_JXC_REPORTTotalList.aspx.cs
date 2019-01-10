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
    public partial class NEW_JXC_REPORTTotalList : BasePage
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
                if (QuanXian_ShowAll("销售业绩帐期考核") == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }



                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll("销售业绩帐期考核", Session["currentUserId"], "WFScanDepartList") == true)
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
                if (NewShowAll_textName("销售业绩帐期考核", "可导出"))
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

                TB_ProjectCostService _projectCostService = new TB_ProjectCostService();
                var projectCosts = _projectCostService.GetListArray("");
                if (projectCosts.Count > 0)
                {
                    var model = projectCosts[0];
                    txtZhangQi.Text = model.ZhangQi.ToString();
                    txtCeSuanDian.Text = model.CeSuanDian.ToString();
                    txtMonthLiLv.Text = model.MonthLiLv.ToString();
                }




                List<TB_BasePoType> new_basePoTypeList = new TB_BasePoTypeService().GetListArray("");
                cbListPoType.DataSource = new_basePoTypeList;
                cbListPoType.DataBind();
                cbListPoType.DataTextField = "BasePoType";
                cbListPoType.DataValueField = "Id";
            }
        }



        private void Show()
        {
            if (CommHelp.VerifesToNum(txtZhangQi.Text) == false || Convert.ToDecimal(txtZhangQi.Text) < 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目财务成本账期 格式有问题！');</script>");
                return;
            }
            if (CommHelp.VerifesToNum(txtCeSuanDian.Text) == false || Convert.ToDecimal(txtCeSuanDian.Text) < 0 || Convert.ToDecimal(txtCeSuanDian.Text) > 1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('财务成本测算点 格式有问题！');</script>");
                return;
            }
            if (CommHelp.VerifesToNum(txtMonthLiLv.Text) == false || Convert.ToDecimal(txtMonthLiLv.Text) < 0 || Convert.ToDecimal(txtMonthLiLv.Text) > 1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('财务成本月利率 格式有问题！');</script>");
                return;
            }

            string sql = " ";

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

            if (ddlJiLiang.Text != "-1")
            {
                isColse += string.Format(" and CG_POOrder.ChengBenJiLiang=" + ddlJiLiang.Text);
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
            List<JXC_REPORTTotal> pOOrderList = this.POSer.NEW_GetListArray_Total(sql, having, fuhao, StartTime, fh, fuhao_E, KAO_POType, NO_Kao_POType, PoTypeList, Convert.ToInt32(ddlZhangQI.Text));
            if (ddlTrueZhangQI.Text != "-1")
            {
                if (ddlTrueZhangQI.Text == "1")
                {
                    pOOrderList = pOOrderList.FindAll(T => T.trueZhangQi <= 30);
                }
                if (ddlTrueZhangQI.Text == "2")
                {
                    pOOrderList = pOOrderList.FindAll(T => T.trueZhangQi > 30 && T.trueZhangQi <= 60);
                }
                if (ddlTrueZhangQI.Text == "3")
                {
                    pOOrderList = pOOrderList.FindAll(T => T.trueZhangQi > 60 && T.trueZhangQi <= 90);
                }
                if (ddlTrueZhangQI.Text == "4")
                {
                    pOOrderList = pOOrderList.FindAll(T => T.trueZhangQi > 90 && T.trueZhangQi <= 120);
                }
                if (ddlTrueZhangQI.Text == "5")
                {
                    pOOrderList = pOOrderList.FindAll(T => T.trueZhangQi > 120);
                }
            }

            if (pOOrderList.Count > 0)
            {
                var D = Convert.ToInt32(txtZhangQi.Text);
                var P = Convert.ToSingle(txtCeSuanDian.Text);
                var R = Convert.ToDecimal(txtMonthLiLv.Text);


                //计算财务成本为0 的项目
                var zeroList = POSer.GetPoNoList(D, P);
                //财务成本不为0的 项目
                var invoiceList = POSer.GetInvoiceList(D, P);
                // 计算财务成本，按首次出库单开具日期 + D + 1这一天开始计算，这天定义为T
                //IF T > 今日的日期,财务成本 = 0
                // ELSE
                //     IF T的到款金额 >= 项目金额 * P ，财务成本 = 0
                //       ELSE
                //        财务成本 = 0
                //        FOR I = 0 TO 100
                //        v = T + 30 * i
                //           IF v> 今日的日期  v = 今日的日期；
                //X = IIF(（项目金额 * P - 截止v的到款总金额）*R > 0, （项目金额 * P - 截止v的到款总金额）*R * IIF(该项目类别属于勾选的财务成本考核项目类别, 1, 0), 0)； 财务成本 = 财务成本 + X；EXIT
                //           
                //X = IIF(（项目金额 * P - 截止v的到款总金额）*R > 0, （项目金额 * P - 截止v的到款总金额）*R * IIF(该项目类别属于勾选的财务成本考核项目类别, 1, 0), 0)
                //        财务成本 = 财务成本 + X
                //        NEXT I
                //     ENDIF

                //获取勾选财务成本考核项的信息


                string selectedPoType = "";
                foreach (ListItem item in cbListPoType.Items)
                {
                    if (item.Selected)
                    {
                        selectedPoType += item.Value + ",";
                    }
                }
                selectedPoType = selectedPoType.Trim(',');
                //if (selectedPoType == ""&& cbAll.Checked==false)
                //{
                //    selectedPoType = "1,2,3";
                //}
                if ( cbAll.Checked )
                {
                    selectedPoType = "1,2,3";
                }
                foreach (var model in pOOrderList)
                {
                    decimal chengben = 0;
                    if (!zeroList.ContainsKey(model.PONo) && model.MinOutDate != null && selectedPoType.Contains(model.potype))
                    {
                        var T = model.MinOutDate.Value.AddDays(D + 1);

                        for (int i = 1; i <= 100; i++)
                        {
                            var v = T.AddDays(30 * i).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                            if (v > DateTime.Now)
                            {

                                //IF v> 今日的日期  v1 = 今日的日期；X = IIF(（项目金额 * P - 截止v1的到款
                                //总金额）*R > 0, （项目金额 * P - 截止v1的到款总金额）*R *（30 - v + v1）/ 30 * IIF(该项
                                //目类别属于勾选的财务成本考核项目类别, 1, 0), 0)； 财务成本 = 财务成本 + X；EXIT

                                var v1 = DateTime.Now;
                                //v = DateTime.Now;
                                var invoiceTotal = invoiceList.FindAll(t => t.DaoKuanDate <= v1 && t.PoNo == model.PONo).Sum(t => t.Total);
                                var X = (model.SumPOTotal * Convert.ToDecimal(P) - invoiceTotal) * R*(30 + (v1.Date - v).Days)/30;
                                if (X > 0)
                                {
                                    chengben = chengben + X;
                                }
                                break;
                            }
                            else
                            {
                                var result = invoiceList.FindAll(t => t.DaoKuanDate <= v && t.PoNo == model.PONo);
                                var invoiceTotal = result.Sum(t => t.Total);
                                if (model.SumPOTotal * Convert.ToDecimal(P) <= invoiceTotal)
                                {
                                    if (result.Count > 0)
                                    {
                                        var v2 = result.Max(t => t.DaoKuanDate).AddDays(-1);
                                        var v2_Total = invoiceList.FindAll(t => t.DaoKuanDate <= v2 && t.PoNo == model.PONo).Sum(t => t.Total);
                                      
                                        var X2 = (model.SumPOTotal * Convert.ToDecimal(P) - v2_Total) * R * (30 + (v2.Date - v).Days) / 30;
                                        if (X2 > 0)
                                        {
                                            chengben = chengben + X2;
                                        }
                                        
                                    }
                                    break;
                                }
                                var X = (model.SumPOTotal * Convert.ToDecimal(P) - invoiceTotal) * R;
                                if (X > 0)
                                {
                                    chengben = chengben + X;
                                }
                            }
                        }
                    }
                    model.CaiWuChengBen = chengben;
                    model.NewKouLiRun = model.KouLiRun + model.CaiWuChengBen;
                    model.NewNo_KouLiRun = model.No_KouLiRun + model.CaiWuChengBen;
                }
            }
            //var a=pOOrderList.FindAll(T => T.SumPOTotal == 0);
            var getAllPONos = pOOrderList.Aggregate("", (current, m) => current + ("'" + m.PONo + "',")).Trim(',');
            lblVisAllPONO.Text = getAllPONos;
            //pOOrderList = pOOrderList.FindAll(t=>t.maoliTotal>0);

            decimal jlr = pOOrderList.Sum(t => t.maoliTotal);
            lblJLR.Text = string.Format("{0:n6}", jlr);

            //利润扣除合计
            decimal liRunKouTotal = pOOrderList.Sum(t => t.KouLiRun);
            lblLiRunKouTotal.Text = string.Format("{0:n5}", liRunKouTotal);
            //项目纯利合计（项目净利合计-利润扣除合计）
            lblChunLiTotal.Text = string.Format("{0:n6}", (jlr - liRunKouTotal));

            decimal no_liRunKouTotal = pOOrderList.Sum(t => t.No_KouLiRun);
            Label8.Text = string.Format("{0:n5}", no_liRunKouTotal);



            lblXSE.Text = string.Format("{0:n6}", pOOrderList.Sum(t => t.goodSellTotal));
            var trueLiRun = pOOrderList.Sum(t => t.TrueLiRun);
            lblSJLR.Text = string.Format("{0:n6}", trueLiRun);

            lblFP.Text = string.Format("{0:n6}", pOOrderList.Sum(t => t.SellFPTotal));
            // 项目总额：XXX      
            decimal allPoTotal = pOOrderList.Sum(t => t.SumPOTotal);
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
            var allDaoKuan = pOOrderList.Sum(t => t.InvoiceTotal);
            lblAllDaoKuan.Text = string.Format("{0:n2}", allDaoKuan);
            //到款率： VVV=TTT/XXX   
            lblAllDaoKuanLv.Text = "0";
            if (allPoTotal > 0)
            {
                lblAllDaoKuanLv.Text = string.Format("{0:f2}", (allDaoKuan / allPoTotal * 100));
            }

            //开票率：LLL= 发票总额合计 /XXX
            var faPiaoTotal = pOOrderList.Sum(t => t.SellFPTotal);
            lblAllFaPiaoLv.Text = "0";
            if (allPoTotal > 0)
            {
                lblAllFaPiaoLv.Text = string.Format("{0:f2}", (faPiaoTotal / allPoTotal * 100));
            }

            lblCaiWuChengBen.Text = string.Format("{0:n5}", pOOrderList.Sum(t => t.CaiWuChengBen));
            lblNewKouLiRun.Text = string.Format("{0:n5}", pOOrderList.Sum(t => t.NewKouLiRun));
            lblNewNo_KouLiRun.Text = string.Format("{0:n5}", pOOrderList.Sum(t => t.NewNo_KouLiRun));

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

        protected void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAll.Checked)
            {
                foreach (ListItem item in cbListPoType.Items)
                {
                    item.Selected = false;
                }
                cbListPoType.Enabled = false;
            }
            else
            {
                cbListPoType.Enabled = true;
            }
        }
    }
}
