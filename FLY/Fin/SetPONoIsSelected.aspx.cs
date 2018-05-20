using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Model;
using System.Data;
using VAN_OA.Model.JXC;

namespace VAN_OA.JXC
{
    public partial class SetPONoIsSelected : BasePage
    {
        CG_POOrderService POSer = new CG_POOrderService();
        List<FpTypeBaseInfo> gooQGooddList = new List<FpTypeBaseInfo>();
        protected string GetType(object type)
        {
            if (type.ToString() == "0")
            {
                return "收到";
            }

            return "未收到";
        }

        protected bool IsSelectedEdit()
        {
            if (ViewState["cbIsSelected"] != null)
            {
                return false;
            }
            return true;
        }

        protected bool IsCloseEdist()
        {
            if (ViewState["isCloseEdist"] != null)
            {
                return false;
            }
            return true;
        }

        protected bool IsSpecialEdit()
        {
            if (ViewState["isSpecialEdit"] != null)
            {
                return false;
            }
            return true;
        }

        protected bool IsFaxEdist()
        {
            if (ViewState["isFaxEdist"] != null)
            {
                return false;
            }
            return true;
        }

        protected bool IsFPTypeEdist()
        {
            if (ViewState["isFPTypeEdist"] != null)
            {
                return false;
            }
            return true;
        }

        protected bool IsJieIsSelected()
        {
            if (ViewState["IsJieIsSelected"] != null)
            {
                return false;
            }
            return true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/Fin/WFAll.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComCode = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

                //主单
                var pOOrderList = new List<CG_POOrderService>();
                gvMain.DataSource = pOOrderList;
                gvMain.DataBind();

                //                string sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='自动选中-可选中'
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目结算') and sys_Object.AutoID is not null", Session["currentUserId"]);
                //                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                if (NewShowAll_textName("项目结算", "自动选中-可选中") == false)
                {
                    btnSelect.Visible = false;
                }
                //                sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='自动选中-可取消'
                //where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目结算') and sys_Object.AutoID is not null", Session["currentUserId"]);
                //                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                if (NewShowAll_textName("项目结算", "自动选中-可取消") == false)
                {
                    btnCancel.Visible = false;
                }
                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                user = userSer.getAllUserByPOList();
                user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";
            }
        }

        private List<string> allFpTypes = new List<string>();
        private void Show()
        {
            int doIt = Convert.ToInt32(ViewState["doIt"]);
            var fpTypeBaseInfoService = new FpTypeBaseInfoService();
            gooQGooddList = fpTypeBaseInfoService.GetListArray("");

            allFpTypes = gooQGooddList.Select(t => t.FpType).ToList();

            if (txtFrom.Text == "" || txtTo.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择日期！');</script>");
                return;
            }

            if (CommHelp.VerifesToDateTime(txtFrom.Text) == false || CommHelp.VerifesToDateTime(txtTo.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                return;
            }

            if (Convert.ToDateTime(txtFrom.Text) > Convert.ToDateTime(txtTo.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期错误，请重新填写！');</script>");
                return;
            }
            if (txtBai.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款金额大小不能为空！');</script>");
                return;
            }
            try
            {
                if (Convert.ToInt32(txtBai.Text) > 100 || (Convert.ToInt32(txtBai.Text) < 0))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款金额大小范围0%--100%！');</script>");
                    return;
                }
            }
            catch (Exception)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('到款金额大小范围0%--100%！');</script>");
                return;
            }
            var sql = "";
            if (txtFrom.Text != "")
            {
                sql += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                sql += string.Format(" and CG_POOrder.PODate<='{0} 23:59:59'", txtTo.Text);
            }

            if (ddlCompany.Text != "-1")
            {
                sql += string.Format(" and exists (select tb_User.id from tb_User where CompanyCode='{0}' and tb_User.loginName=CG_POOrder.ae)", ddlCompany.Text);
            }

            if (ddlPOTyle.Text != "-1")
            {
                sql += string.Format(" and CG_POOrder.POType={0} ", ddlPOTyle.Text);
            }
            if (ddlGuestTypeList.SelectedValue != "全部")
            {
                sql += string.Format(" and GuestType='{0}'", ddlGuestTypeList.SelectedValue);
            }
            if (ddlGuestProList.SelectedValue != "-2")
            {
                sql += string.Format(" and GuestPro={0}", ddlGuestProList.SelectedValue);
            }
            //查询
            if (doIt == 0)
            {

                if ((txtLeftPoTotal.Text.Trim() != "" && CommHelp.VerifesToNum(txtLeftPoTotal.Text) == false)
                    || (txtRightPoTotal.Text.Trim() != "" && CommHelp.VerifesToNum(txtRightPoTotal.Text) == false))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目金额 格式错误！');</script>");
                    return;
                }

                if ((txtLeftJingLi.Text.Trim() != "" && CommHelp.VerifesToNum(txtLeftJingLi.Text) == false)
                  || (txtRightJingLi.Text.Trim() != "" && CommHelp.VerifesToNum(txtRightJingLi.Text) == false))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目净利 格式错误！');</script>");
                    return;
                }

                if (txtLeftJingLi.Text.Trim() != "")
                {
                    sql += string.Format(" and {0}{1}isnull(maoliTotal,0)", txtLeftJingLi.Text, ddlLeftJingLi.Text);
                }
                
                if (txtRightJingLi.Text.Trim() != "")
                {
                    sql += string.Format(" and isnull(maoliTotal,0){0}{1}", ddlRightJingLi.Text, txtRightJingLi.Text);
                }

                if (txtLeftPoTotal.Text.Trim() != "")
                {
                    sql += string.Format(" and {0}{1}(newtable1.POTotal-isnull(TuiTotal,0))", txtLeftPoTotal.Text, ddlLeftPoTotal.Text);
                }

                if (txtRightPoTotal.Text.Trim() != "")
                {
                    sql += string.Format(" and (newtable1.POTotal-isnull(TuiTotal,0)){0}{1}", ddlRightPoTotal.Text, txtRightPoTotal.Text);
                }

                if (ddlIsSpecial.Text != "-1")
                {
                    sql += string.Format(" and IsSpecial={0} ", ddlIsSpecial.Text);
                }

                if (ddlClose.Text != "-1")
                {
                    sql += string.Format(" and IsClose={0} ", ddlClose.Text);
                }
                if (ddlIsSelect.Text != "-1")
                {
                    sql += string.Format(" and IsSelected={0} ", ddlIsSelect.Text);
                }
                if (ddlJieIsSelected.Text != "-1")
                {
                    sql += string.Format(" and JieIsSelected={0} ", ddlJieIsSelected.Text);
                }
                if (ddlHanShui.Text != "-1")
                {
                    sql += string.Format(" and IsPoFax={0} ", ddlHanShui.Text);
                }
                if (txtPONo.Text.Trim() != "")
                {
                    if (CheckPoNO(txtPONo.Text) == false)
                    {
                        return;
                    }
                    txtPONo.Text = txtPONo.Text.Trim();
                    sql += string.Format(" and CG_POOrder.PONo like '%{0}%'", txtPONo.Text.Trim());
                }
                if (ttxPOName.Text.Trim() != "")
                {
                    sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text.Trim());
                }
                if (txtGuestName.Text.Trim() != "")
                {
                    sql += string.Format(" and GuestName  like '%{0}%'", txtGuestName.Text.Trim());
                }
                if (ddlUser.Text != "-1")//显示部门信息
                {
                    sql += string.Format(" and AE = '{0}'", ddlUser.SelectedItem.Text);
                }
            }
            //执行命令
            if (doIt == 1)
            {
                sql += string.Format(" and IsSpecial=0 ");
            }
            var dt = this.POSer.FIN_SetPoSpecial(sql);
            if (doIt == 0)
            {
                if (ddlFuHao.Text == ">=")
                {
                    dt = dt.FindAll(t => t.BILI >= Convert.ToDecimal(txtBai.Text));
                }
                if (ddlFuHao.Text == ">")
                {
                    dt = dt.FindAll(t => t.BILI > Convert.ToDecimal(txtBai.Text));
                }
                if (ddlFuHao.Text == "<")
                {
                    dt = dt.FindAll(t => t.BILI < Convert.ToDecimal(txtBai.Text));
                }
                if (ddlFuHao.Text == "<=")
                {
                    dt = dt.FindAll(t => t.BILI <= Convert.ToDecimal(txtBai.Text));
                }
                if (ddlFuHao.Text == "=")
                {
                    dt = dt.FindAll(t => t.BILI == Convert.ToDecimal(txtBai.Text));
                }
            }
            if (dt.Count > 0 && doIt == 1)//自动选中
            {
                Do(dt, Convert.ToDecimal(txtBai.Text), ddlFuHao.Text);
                dt = this.POSer.FIN_SetPoSpecial(sql);
            }
            if (dt.Count > 0 && doIt == 2)//取消选中
            {
                Do_Cancel(dt);
                dt = this.POSer.FIN_SetPoSpecial(sql);
            }

            if (dt.Count == 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('没有找到数据！');</script>");
            }
            AspNetPager1.RecordCount = dt.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = dt;
            this.gvMain.DataBind();

        }

        private void Do(List<VAN_OA.Model.JXC.CG_POOrder> list, decimal bili, string fuhao)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {

                    objCommand.Parameters.Clear();
                    foreach (var m in list)
                    {



                        var sql = "";
                        // a.凡是到款金额>=90%项目金额的项目，在项目归类 中的选中属性打上勾，
                        //c.凡是项目金额=0的项目，在项目归类 中的选中属性打上勾。
                        //if (m.BILI >= bili || m.POTotal == 0)
                        //{
                        //     sql = string.Format("update CG_POOrder set IsSelected=1 where PONo='{0}'; ",m.PONo);
                        //}

                        //自动选中，增加一个 项目金额=0，净利=0 的判断，判断次序变为 
                        //1.先判断 同时为0的情况，只设置特殊属性，选中和结算选中 不勾选，
                        //2. 不同时为0 还是原来的自动选中的逻辑

                        //如果项目金额=0，项目净利=0，则该项目的特殊属性自动打勾
                        if (m.POTotal == 0 && m.MaoliTotal != null && m.MaoliTotal == 0)
                        {
                            sql += string.Format("update CG_POOrder set IsSpecial=1,IsSelected=0,JieIsSelected=0 where pono='{0}';", m.PONo);
                        }
                        else
                        {
                            if (fuhao == ">=" && m.BILI >= bili)
                            {
                                sql = string.Format("update CG_POOrder set IsSelected=1 where PONo='{0}'; ", m.PONo);
                            }
                            else if (fuhao == ">" && m.BILI > bili)
                            {
                                sql = string.Format("update CG_POOrder set IsSelected=1 where PONo='{0}'; ", m.PONo);
                            }
                            else if (fuhao == "<" && m.BILI < bili)
                            {
                                sql = string.Format("update CG_POOrder set IsSelected=1 where PONo='{0}'; ", m.PONo);
                            }
                            else if (fuhao == "<=" && m.BILI <= bili)
                            {
                                sql = string.Format("update CG_POOrder set IsSelected=1 where PONo='{0}'; ", m.PONo);
                            }
                            else if (fuhao == "=" && m.BILI == bili)
                            {
                                sql = string.Format("update CG_POOrder set IsSelected=1 where PONo='{0}'; ", m.PONo);
                            }
                            if (m.POTotal == 0)
                            {
                                sql = string.Format("update CG_POOrder set IsSelected=1 where PONo='{0}'; ", m.PONo);
                            }
                            //d. 凡是项目总成本>=项目金额 的项目，在项目归类 中的选中属性打上勾
                            if (m.GoodTotal >= m.POTotal)
                            {
                                sql = string.Format("update CG_POOrder set IsSelected=1 where PONo='{0}'; ", m.PONo);
                            }
                            // b.凡是项目日期区间的项目，项目归类中的结算选中的属性均打上勾。
                            sql += string.Format("update CG_POOrder set JieIsSelected=1 where pono='{0}';", m.PONo);
                        }

                        objCommand.CommandText = sql;
                        objCommand.ExecuteNonQuery();

                    }

                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");
                    return;
                }
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
        }

        private void Do_Cancel(List<VAN_OA.Model.JXC.CG_POOrder> list)
        {
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();
                SqlTransaction tan = conn.BeginTransaction();
                SqlCommand objCommand = conn.CreateCommand();
                objCommand.Transaction = tan;
                try
                {

                    objCommand.Parameters.Clear();
                    foreach (var m in list)
                    {
                        var sql = string.Format("update CG_POOrder set IsSelected=0,JieIsSelected=0 where PONo='{0}'; ", m.PONo);
                        objCommand.CommandText = sql;
                        objCommand.ExecuteNonQuery();
                    }

                    tan.Commit();
                }
                catch (Exception)
                {
                    tan.Rollback();
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");
                    return;
                }
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            ViewState["doIt"] = 0;
            Show();
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            ViewState["doIt"] = 1;
            AspNetPager1.CurrentPageIndex = 1;
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
                if (model.MaoliTotal < 0)
                {
                    e.Row.Cells[14].BackColor= System.Drawing.Color.Khaki;
                }
                DropDownList drp = (DropDownList)e.Row.FindControl("dllFPstye");
                if (ViewState["isFPTypeEdist"] != null)
                {
                    drp.Enabled = false;
                }
                drp.DataSource = gooQGooddList;
                drp.DataTextField = "FpType";
                drp.DataValueField = "FpType";
                drp.DataBind();
                //  选中 DropDownList
                try
                {
                    var hidTxt = ((HiddenField)e.Row.FindControl("hidtxt")).Value;
                    if (hidTxt == "")
                    {
                        drp.SelectedIndex = allFpTypes.IndexOf("增值税发票");
                    }
                    else
                    {
                        drp.SelectedIndex = allFpTypes.IndexOf(hidTxt);
                    }

                }
                catch (Exception)
                {


                }

            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


        private void setValue(Label control, string value)
        {
            control.Text = value;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["doIt"] = 2;
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            ViewState["doIt"] = 0;
            AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

    }
}
