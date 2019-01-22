using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.Fin;
using VAN_OA.Model.Fin;
using VAN_OA.Model.JXC;
using VAN_OA.Dal.JXC;

namespace VAN_OA.Fin
{
    public partial class SpecCostList : BasePage
    {
        JXC_REPORTService POSer = new JXC_REPORTService();
        protected List<FIN_Property> propertyList = new List<FIN_Property>();
        FIN_PropertyService FIN_PropertyService = new FIN_PropertyService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/WFAll.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (Check() == false)
            {
                return;
            }
            Show();
            btnSave.Enabled = true;
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        private bool Check()
        {
            if (CommHelp.VerifesToNum(txtZhangQi.Text) == false||Convert.ToDecimal(txtZhangQi.Text)<0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目财务成本账期 格式有问题！');</script>");
                return false;
            }
            if (CommHelp.VerifesToNum(txtCeSuanDian.Text) == false || Convert.ToDecimal(txtCeSuanDian.Text) < 0 || Convert.ToDecimal(txtCeSuanDian.Text) >1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('财务成本测算点 格式有问题！');</script>");
                return false;
            }
            if (CommHelp.VerifesToNum(txtMonthLiLv.Text) == false || Convert.ToDecimal(txtMonthLiLv.Text) < 0 || Convert.ToDecimal(txtMonthLiLv.Text) > 1)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('财务成本月利率 格式有问题！');</script>");
                return false;
            }
            return true;
        }
        private void Show()
        {
            var comModel = new Dal.BaseInfo.TB_CompanyService().GetModel(Convert.ToInt32(ddlCompany.Text));
            lblCompanyName.Text = comModel.ComName;
            lblSimpName.Text = comModel.ComSimpName;
            lblDate.Text = ddlYear.Text + ddlMonth.Text;
            lblAE.Text = ddlUser.SelectedItem.Text;
            propertyList = this.FIN_PropertyService.GetListArray_SpecCost(ddlYear.Text + ddlMonth.Text, ddlCompany.Text,ddlUser.Text);


            //DateTime fristDate = Convert.ToDateTime(ddlYear.Text+"-"+ddlMonth.Text+"-1");
            //DateTime fristDate_1 = Convert.ToDateTime(ddlYear.Text + "-1-1");
            //DateTime endDate = Convert.ToDateTime(ddlYear.Text + "-" + ddlMonth.Text + "-" + DateTime.DaysInMonth(fristDate.Year, fristDate.Month));
            //显示系统值
            var sql = string.Format(@"---显示=右面界面 加班记录管理中 日期从XX---YY(财年跨度) ，某一AE 的加班费 合计数
select  isnull(sum(Total),0) from tb_OverTime 
where   year(formTime)={0} 
and MONTH(formTime)<={1}
and guestDai ='{2}' and tb_OverTime.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='加班单') and state='通过')", ddlYear.Text, ddlMonth.Text, ddlUser.SelectedItem.Text);

            ViewState["OverTime"] = DBHelp.ExeScalar(sql);

            sql = string.Format(@"---显示=右面界面 加班记录管理中 日期从XX---YY(财年跨度) ，某一AE 的加班费 合计数
select  isnull(sum(Total),0) from tb_OverTime 
where   year(formTime)={0} 
and MONTH(formTime)={1}
and guestDai ='{2}' and tb_OverTime.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='加班单') and state='通过')", ddlYear.Text, ddlMonth.Text, ddlUser.SelectedItem.Text);

            ViewState["OverTimeMonth"] = DBHelp.ExeScalar(sql);
            GetGoodTotal();

            lblYear.Text = string.Format("{0}-01",ddlYear.Text);
            lblMonth.Text= string.Format("{0}-{1}", ddlYear.Text,ddlMonth.Text);
        }


        /// <summary>
        /// 获取项目总成本信息
        /// </summary>
        private void GetGoodTotal()
        {
            //string resultSql = "";
            //string sql = "where ifzhui=0  and CG_POOrder.Status='通过'";
            //if (cbAll.Checked == false)
            //{
            //    string ids = "";
            //    foreach (ListItem item in cbListPoType.Items)
            //    {
            //        if (item.Selected)
            //        {
            //            ids += item.Value + ",";
            //        }
            //    }
            //    if (!string.IsNullOrEmpty(ids))
            //    {
            //        sql += " and CG_POOrder.POType in (" + ids.Trim(',') + ")";
            //    }              
            //}
            //sql +=string.Format( " and CG_POOrder.AE='{0}' and IsSpecial=0  ",ddlUser.SelectedItem.Text);
            //sql += string.Format(" and year(CG_POOrder.PODate)={0}",ddlYear.Text);
            //resultSql = string.Format(" select  sum(goodTotal)+sum(t_goodTotalChas) as goodTotal from CG_POOrder left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo  {0};",sql);

            //sql += string.Format(" and month(CG_POOrder.PODate)={0}", ddlMonth.Text);
            //resultSql += string.Format(" select  sum(goodTotal)+sum(t_goodTotalChas) as goodTotal from CG_POOrder left join JXC_REPORT on CG_POOrder.PONo=JXC_REPORT.PONo  {0};", sql);

            //var ds= DBHelp.getDataSet(resultSql);
            //var year = ds.Tables[0];
            //var month = ds.Tables[1];

            var sql = string.Format("and year(CG_POOrder.PODate)={1} and IsSpecial=0 and CG_POOrder.AE='{0}' ",
                ddlUser.SelectedItem.Text, ddlYear.Text);
            List<JXC_REPORTTotal> pOOrderList = this.POSer.NEW_GetListArray_Total(sql, "", "where 1=1 ", DateTime.Now.Date, "", "", "", "", "-1", -1);


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
              
                if (cbAll.Checked)
                {
                    selectedPoType = "1,2,3";
                }
                foreach (var model in pOOrderList)
                {
                    decimal chengben = 0;
                    if ( !zeroList.ContainsKey(model.PONo) && model.MinOutDate != null && selectedPoType.Contains(model.potype) && model.ChengBenJiLiangString)
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
                                var X = (model.SumPOTotal * Convert.ToDecimal(P) - invoiceTotal) * R * (30 + (v1.Date - v).Days) / 30;
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
                    model.NewNo_KouLiRun = model.KPI_No_KouLiRun + model.CaiWuChengBen;
                }
            }

            ViewState["yearGoodTotal"] = string.Format("{0:n5}", pOOrderList.Sum(t => t.CaiWuChengBen));
            ViewState["monthGoodTotal"] = string.Format("{0:n5}", pOOrderList.Where(t=>t.PODate.Month==Convert.ToInt32(ddlMonth.Text)).Sum(t => t.CaiWuChengBen));
        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }


        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string where = "";
            if (ddlCompany.Text != "-1")
            {
                where = string.Format(" and tb_User.CompanyCode =(select ComCode from TB_Company where id={0})", ddlCompany.Text);
            }
            List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
            VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
            user = userSer.getAllUserByPOList(where);
            user.Insert(0, new VAN_OA.Model.User() { LoginName = "选择", Id = -1 });

            ddlUser.DataSource = user;
            ddlUser.DataBind();
            ddlUser.DataTextField = "LoginName";
            ddlUser.DataValueField = "Id";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {

                for (int i = 2012; i <= DateTime.Now.Year; i++)
                {
                    ddlYear.Items.Add(new ListItem { Value = i.ToString(), Text = i.ToString() });
                }
                ddlYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
                TB_CompanyService comSer = new TB_CompanyService();

                var comList = comSer.GetListArray("");
                comList.Insert(0, new TB_Company() { ComName = "选择", Id = -1 });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

//                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='个性费用-可修改保存'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目结算') and sys_Object.AutoID is not null", Session["currentUserId"]);
//                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                if (NewShowAll_textName("项目结算", "个性费用-可修改保存") == false)
                {
                    btnSave.Visible = false;
                }
                List<TB_BasePoType> basePoTypeList = new TB_BasePoTypeService().GetListArray("");
                cbListPoType.DataSource = basePoTypeList;
                cbListPoType.DataBind();
                cbListPoType.DataTextField = "BasePoType";
                cbListPoType.DataValueField = "Id";

                TB_ProjectCostService _projectCostService = new TB_ProjectCostService();
                var projectCosts=_projectCostService.GetListArray("");
                if (projectCosts.Count > 0)
                {
                    var model = projectCosts[0];
                    txtZhangQi.Text = model.ZhangQi.ToString();
                    txtCeSuanDian.Text = model.CeSuanDian.ToString();
                    txtMonthLiLv.Text = model.MonthLiLv.ToString();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/WFAll.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var propertyList = this.FIN_PropertyService.GetListArray(" MyProperty='个性'");
           List<FIN_SpecCost> commCostList = new List<FIN_SpecCost>();
           foreach (var m in propertyList)
           {
               if (!string.IsNullOrEmpty(Request["pro_" + m.Id]))
               {
                   var newM = new FIN_SpecCost();
                   newM.CaiYear = ddlYear.Text + ddlMonth.Text;
                   newM.CompId = Convert.ToInt32(ddlCompany.Text);
                   newM.CostTypeId = m.Id;

                    if (CommHelp.VerifesToNum(Request["pro_" + m.Id].ToString()) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('数值 格式错误！');</script>");
                        return;
                    }

                    newM.Total = Convert.ToDecimal(Request["pro_" + m.Id]);
                   newM.UserID =Convert.ToInt32(ddlUser.Text);
                   commCostList.Add(newM);
               }
           }
            FIN_SpecCostService commSer=new FIN_SpecCostService ();
            commSer.AddList(commCostList);

            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");

        }

        protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUser.Text != "-1")
            {
                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
            }
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
