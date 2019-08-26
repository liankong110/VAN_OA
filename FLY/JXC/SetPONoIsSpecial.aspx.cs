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

namespace VAN_OA.JXC
{

    public partial class SetPONoIsSpecial : BasePage
    {

        CG_POOrderService POSer = new CG_POOrderService();
        TB_ModelService modelService = new TB_ModelService();
        GuestTypeBaseInfoService dal = new GuestTypeBaseInfoService();
        GuestProBaseInfoService guestProBaseInfodal = new GuestProBaseInfoService();

        List<FpTypeBaseInfo> gooQGooddList = new List<FpTypeBaseInfo>();
        List<TB_BasePoType> _basePoTypeList = new List<TB_BasePoType>();
        List<TB_Model> _modelList = new List<TB_Model>();

        List<GuestTypeBaseInfo> _guestTypeList = new List<GuestTypeBaseInfo>();
        List<GuestProBaseInfo> _guestProList = new List<GuestProBaseInfo>();

        protected bool IsChengBenJiLiang()
        {
            if (ViewState["isChengBenJiLiang"] != null)
            {
                return false;
            }
            return true;
        }

        protected bool IsPlanDays()
        {
            if (ViewState["isPlanDays"] != null)
            {
                return false;
            }
            return true;
        }
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

        protected bool IsModelEdit()
        {
            if (ViewState["isModelEdit"] != null)
            {
                return false;
            }
            return true;
        }
        protected bool IsPOType()
        {
            if (ViewState["isPOType"] != null)
            {
                return false;
            }
            return true;
        }
        protected bool IsGuestPro()
        {
            if (ViewState["isGuestPro"] != null)
            {
                return false;
            }
            return true;
        }

        protected bool IsGuestType()
        {
            if (ViewState["isGuestType"] != null)
            {
                return false;
            }
            return true;
        }

        protected string GetNum(string num)
        {
            return string.Format("{0:n2}", num);
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

        protected void cbIsPoFax_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                _modelList = modelService.GetListArray("");
                _modelList.Insert(0, new TB_Model { Id = -1, ModelName = "全部" });
                ddlModel.DataSource = _modelList;
                ddlModel.DataBind();
                ddlModel.DataTextField = "ModelName";
                ddlModel.DataValueField = "ModelName";

                this.gvModel.DataSource = modelService.GetListArray(""); ;
                this.gvModel.DataBind();

                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();
                if (NewShowAll_textName("项目归类", "关闭可编辑") == false)
                {
                    ViewState["isCloseEdist"] = false;
                    btnSaveIsClose.Visible = false;
                }
                if (NewShowAll_textName("项目归类", "特殊可编辑") == false)
                {
                    ViewState["isSpecialEdit"] = false;
                }
                if (NewShowAll_textName("项目归类", "项目模型可编辑") == false)
                {
                    ViewState["isModelEdit"] = false;
                }
                if (NewShowAll_textName("项目归类", "项目类型可编辑") == false)
                {
                    ViewState["isPOType"] = false;
                }
                if (NewShowAll_textName("项目归类", "含税可编辑") == false)
                {
                    ViewState["isFaxEdist"] = false;
                }
                if (NewShowAll_textName("项目归类", "发票类型可编辑") == false)
                {
                    ViewState["isFPTypeEdist"] = false;
                }
                if (NewShowAll_textName("项目归类", "选中可编辑") == false)
                {
                    ViewState["cbIsSelected"] = false;
                    btnIsSelected.Visible = false;
                }
                if (NewShowAll_textName("项目归类", "结算选中可编辑") == false)
                {
                    ViewState["IsJieIsSelected"] = false;
                    btnJieIsSelected.Visible = false;
                }

                if (NewShowAll_textName("项目归类", "财务成本计量可编辑") == false)
                {
                    ViewState["isChengBenJiLiang"] = false;
                }
                if (NewShowAll_textName("项目归类", "计划完工天数可编辑") == false)
                {
                    ViewState["isPlanDays"] = false;
                }
                if (NewShowAll_textName("项目归类", "客户类型可编辑") == false)
                {
                    ViewState["isGuestType"] = false;
                    btnGuestType.Visible = false;
                }
                if (NewShowAll_textName("项目归类", "客户属性可编辑") == false)
                {
                    ViewState["isGuestPro"] = false;
                    btnGuestPro.Visible = false;
                }
                var user = new List<Model.User>();
                var userSer = new Dal.SysUserService();

                //主单
                var pOOrderList = new List<CG_POOrderService>();
                gvMain.DataSource = pOOrderList;
                gvMain.DataBind();

                if (QuanXian_ShowAll("项目归类") == false)
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
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";

                var fpTypeBaseInfoService = new FpTypeBaseInfoService();
                List<FpTypeBaseInfo> fpTypeList = fpTypeBaseInfoService.GetListArray("");
                fpTypeList.Insert(0, new FpTypeBaseInfo { FpType = "全部", Id = -1 });
                fpTypeList.Add(new FpTypeBaseInfo { Id = 999, FpType = "" });
                dllFPstye.DataSource = fpTypeList;
                dllFPstye.DataBind();
                dllFPstye.DataTextField = "FpType";
                dllFPstye.DataValueField = "Id";

                dllFPstye.Items[fpTypeList.Count - 1].Attributes.Add("style", "background-color: red");

                List<FpTypeBaseInfo> newFpTypeList = new List<FpTypeBaseInfo>();
                foreach (var fp in fpTypeList)
                {
                    newFpTypeList.Add(new FpTypeBaseInfo { Id = fp.Id, FpType = fp.FpType });
                }

                newFpTypeList.Add(new FpTypeBaseInfo { Id = 1000, FpType = "其他" });
                ddlFPType.DataSource = newFpTypeList;
                ddlFPType.DataBind();
                ddlFPType.DataTextField = "FpType";
                ddlFPType.DataValueField = "Id";

                ddlFPType.Items[newFpTypeList.Count - 2].Attributes.Add("style", "background-color: red");


                List<TB_BasePoType> basePoTypeList = new TB_BasePoTypeService().GetListArray("");
                basePoTypeList.Insert(0, new TB_BasePoType { BasePoType = "全部", Id = -1 });
                ddlPOTyle.DataSource = basePoTypeList;
                ddlPOTyle.DataBind();
                ddlPOTyle.DataTextField = "BasePoType";
                ddlPOTyle.DataValueField = "Id";

                GuestTypeBaseInfoService dal = new GuestTypeBaseInfoService();
                var dalList = dal.GetListArray("");
                dalList.Insert(0, new VAN_OA.Model.BaseInfo.GuestTypeBaseInfo { GuestType = "" });
                dalList.Insert(0, new VAN_OA.Model.BaseInfo.GuestTypeBaseInfo { GuestType = "全部" });

                ddlGuestTypeList.DataSource = dalList;
                ddlGuestTypeList.DataBind();
                ddlGuestTypeList.DataTextField = "GuestType";
                ddlGuestTypeList.DataValueField = "GuestType";

                GuestProBaseInfoService guestProBaseInfodal = new GuestProBaseInfoService();
                var proList = guestProBaseInfodal.GetListArray("");
                proList.Insert(0, new VAN_OA.Model.BaseInfo.GuestProBaseInfo { GuestPro = -1 });
                proList.Insert(0, new VAN_OA.Model.BaseInfo.GuestProBaseInfo { GuestPro = -2, });

                ddlGuestProList.DataSource = proList;
                ddlGuestProList.DataBind();
                ddlGuestProList.DataTextField = "GuestProString";
                ddlGuestProList.DataValueField = "GuestPro";
            }
        }

        private List<string> allFpTypes = new List<string>();
        private void Show()
        {

            ddlFPType.Items[ddlFPType.Items.Count - 2].Attributes.Add("style", "background-color: red");

            _basePoTypeList = new TB_BasePoTypeService().GetListArray("");
            _basePoTypeList.Insert(0, new TB_BasePoType { BasePoType = "", Id = -1 });

            _modelList = modelService.GetListArray("");
            _modelList.Insert(0, new TB_Model { Id = -1, ModelName = "" });


            _guestTypeList = dal.GetListArray("");
            _guestTypeList.Insert(0, new VAN_OA.Model.BaseInfo.GuestTypeBaseInfo { GuestType = "" });


            _guestProList = guestProBaseInfodal.GetListArray("");
            _guestProList.Insert(0, new VAN_OA.Model.BaseInfo.GuestProBaseInfo { GuestPro = -1 });

            var fpTypeBaseInfoService = new FpTypeBaseInfoService();
            gooQGooddList = fpTypeBaseInfoService.GetListArray("");
            gooQGooddList.Add(new FpTypeBaseInfo { FpType = "" });
            allFpTypes = gooQGooddList.Select(t => t.FpType).ToList();
            var sql = "";

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and CG_POOrder.PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtPOName.Text))
            {
                sql += string.Format(" and PONAME like '%{0}%'", txtPOName.Text.Trim());
            }

            if (txtPlanDayForm.Text != "")
            {
                if (CommHelp.VerifesToNum(txtPlanDayForm.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('计划完工天数 格式错误！');</script>");
                    return;
                }

                sql += string.Format(" and {1}{0}PlanDays", ddlPlanDayForm.Text, txtPlanDayForm.Text);
            }

            if (txtPlanDayTo.Text != "")
            {
                if (CommHelp.VerifesToNum(txtPlanDayTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('计划完工天数 格式错误！');</script>");
                    return;
                }

                sql += string.Format(" and PlanDays{0}{1}", ddlPlanDayTo.Text, txtPlanDayTo.Text);
            }

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and PODate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and PODate<='{0} 23:59:59'", txtTo.Text);
            }

            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text.Trim());
            }

            if (ddlUser.Text != "-1")
            {
                var strSql = new System.Text.StringBuilder();
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                if (1 <= month && month <= 3)
                {
                    strSql.Append(string.Format(" and QuartNo='1' and YearNo='{0}' ", year));
                }
                else if (4 <= month && month <= 6)
                {
                    strSql.Append(string.Format(" and QuartNo='2' and YearNo='{0}' ", year));
                }
                else if (7 <= month && month <= 9)
                {
                    strSql.Append(string.Format(" and QuartNo='3' and YearNo='{0}' ", year));
                }
                else if (10 <= month && month <= 12)
                {
                    strSql.Append(string.Format(" and QuartNo='4' and YearNo='{0}' ", year));

                }
                // sql += string.Format(" and AppName={0}", ddlUser.Text);
                sql += string.Format(" and AE='{0}'", ddlUser.SelectedItem.Text);
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and AE IN(select loginName from tb_User where {0})", where);
            }

            if (ddlSpecial.Text != "-1")
            {
                sql += string.Format(" and IsSpecial={0}", ddlSpecial.Text);
            }
            if (ddlNoSelected.Text != "-1")
            {
                sql += string.Format(" and IsSelected=" + ddlNoSelected.Text);
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                sql += string.Format(" and JieIsSelected=" + ddlJieIsSelected.Text);
            }
            if (ddlColse.Text != "-1")
            {
                sql += string.Format(" and IsClose={0}", ddlColse.Text);
            }

            if (ddlIsPoFax.Text != "-1")
            {
                sql += string.Format(" and IsPoFax={0}", ddlIsPoFax.Text);

                if (ddlIsPoFax.Text == "1" && dllFPstye.Text != "-1")
                {
                    sql += string.Format(" and FpType='" + dllFPstye.SelectedItem.Text + "'");
                }
            }
            if (ddlPOTyle.Text != "-1")
            {
                sql += string.Format(" and POType=" + ddlPOTyle.Text);
            }
            if (ddlJiLiang.Text != "-1")
            {
                sql += string.Format(" and ChengBenJiLiang=" + ddlJiLiang.Text);
            }


            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and Model='{0}'", ddlModel.Text);
            }

            if (!string.IsNullOrEmpty(txtPoTotal.Text))
            {
                if (CommHelp.VerifesToNum(txtPoTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目金额 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and SumPOTotal{0}{1}", ddlFuHao.Text, txtPoTotal.Text);
            }

            if (!string.IsNullOrEmpty(txtGuestName.Text.Trim()))
            {
                sql += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }
            if (ddlFPType.Text != "-1")
            {
                if (ddlFPType.SelectedItem.Text != "其他")
                {
                    sql += string.Format(" and FPType='{0}'", ddlFPType.SelectedItem.Text);
                }
                else
                {
                    sql += string.Format(" and FPType not in (select FpType from FpTypeBaseInfo) and FPType<>''");
                }
            }
            if (txtEque1.Text != "")
            {
                if (CommHelp.VerifesToNum(txtEque1.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目金额 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and {1}{0}SumPOTotal", ddlEque1.Text, txtEque1.Text);
            }
            if (txtEque2.Text != "")
            {
                if (CommHelp.VerifesToNum(txtEque2.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目金额 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and SumPOTotal{0}{1}", ddlEque2.Text, txtEque2.Text);
            }

            if (ddlGuestTypeList.SelectedValue != "全部")
            {
                sql += string.Format(" and GuestType='{0}'", ddlGuestTypeList.SelectedValue);
            }
            if (ddlGuestProList.SelectedValue != "-2")
            {
                if (ddlGuestProList.SelectedValue == "-1")
                {
                    sql += string.Format(" and GuestPro not in (0,1,2)");
                }
                else
                {
                    sql += string.Format(" and GuestPro={0}", ddlGuestProList.SelectedValue);
                }
            }

            var dt = this.POSer.SetPoSpecial(sql);

            //foreach (DataRow dr in dt.Rows)
            //{
            //    if (ViewState["isCloseEdist"] != null)
            //        dr["isCloseEdist"] = false;
            //    else
            //    {
            //        dr["isCloseEdist"] = true;
            //    }

            //}
            AspNetPager1.RecordCount = dt.Rows.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = dt;
            this.gvMain.DataBind();


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

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvMain.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                var dr = e.Row.DataItem as DataRowView;
                var goodTotal = dr["goodTotal"];
                var sumPOTotal = dr["SumPOTotal"];
                if (goodTotal != DBNull.Value && sumPOTotal != DBNull.Value)
                {
                    //如果项目金额 < 成本 并且是特殊订单，帮我单据号，项目编码，成本 三列的格子背景帮我显示粉红色
                    if (Convert.ToDecimal(sumPOTotal) < Convert.ToDecimal(goodTotal) && Convert.ToBoolean(dr["IsSpecial"]))
                    {
                        e.Row.Cells[1].BackColor = System.Drawing.Color.Pink;
                        e.Row.Cells[2].BackColor = System.Drawing.Color.Pink;
                        e.Row.Cells[4].BackColor = System.Drawing.Color.Pink;

                    }
                }
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                DropDownList drp = (DropDownList)e.Row.FindControl("dllFPstye");
                if (ViewState["isFPTypeEdist"] != null)
                {
                    drp.Enabled = false;
                }
                drp.DataSource = gooQGooddList;
                drp.DataTextField = "FpType";
                drp.DataValueField = "FpType";
                drp.DataBind();

                drp.Items[gooQGooddList.Count - 1].Attributes.Add("style", "background-color: red");
                //  选中 DropDownList
                try
                {
                    var hidTxt = ((HiddenField)e.Row.FindControl("hidtxt")).Value;
                    //if (hidTxt == "")
                    //{
                    //    drp.SelectedIndex = allFpTypes.IndexOf("");
                    //}
                    //else
                    //{
                    drp.SelectedIndex = allFpTypes.IndexOf(hidTxt);
                    //}

                }
                catch (Exception)
                {


                }

                try
                {
                    DropDownList dllPOType = (DropDownList)e.Row.FindControl("dllPOType");
                    dllPOType.DataSource = _basePoTypeList;
                    dllPOType.DataBind();
                    dllPOType.DataTextField = "BasePoType";
                    dllPOType.DataValueField = "Id";

                    var hidPOTypetxt = ((HiddenField)e.Row.FindControl("hidPOTypetxt")).Value;
                    if (hidPOTypetxt != "-1")
                    {
                        dllPOType.SelectedIndex = _basePoTypeList.FindIndex(t => t.Id.ToString() == hidPOTypetxt);
                    }

                }
                catch (Exception)
                {


                }

                try
                {
                    DropDownList ddlModel = (DropDownList)e.Row.FindControl("ddlModel");
                    ddlModel.DataSource = _modelList;
                    ddlModel.DataBind();
                    ddlModel.DataTextField = "ModelName";
                    ddlModel.DataValueField = "ModelName";

                    var hidModeltxt = ((HiddenField)e.Row.FindControl("hidModeltxt")).Value;
                    if (hidModeltxt != "-1")
                    {
                        ddlModel.SelectedIndex = _modelList.FindIndex(t => t.ModelName == hidModeltxt);
                    }

                }
                catch (Exception)
                {


                }

                try
                {
                    DropDownList dllGuestPro = (DropDownList)e.Row.FindControl("dllGuestPro");
                    dllGuestPro.DataSource = _guestProList;
                    dllGuestPro.DataBind();
                    dllGuestPro.DataTextField = "GuestProString";
                    dllGuestPro.DataValueField = "GuestPro";

                    var hidModeltxt = ((HiddenField)e.Row.FindControl("hidGuestProtxt")).Value;
                    if (hidModeltxt != "-1")
                    {
                        dllGuestPro.SelectedIndex = _guestProList.FindIndex(t => t.GuestPro.ToString() == hidModeltxt);
                    }

                }
                catch (Exception)
                {


                }

                try
                {
                    DropDownList dllGuestType = (DropDownList)e.Row.FindControl("dllGuestType");
                    dllGuestType.DataSource = _guestTypeList;
                    dllGuestType.DataBind();
                    dllGuestType.DataTextField = "GuestType";
                    dllGuestType.DataValueField = "GuestType";

                    var hidGuestTypetxt = ((HiddenField)e.Row.FindControl("hidGuestTypetxt")).Value;
                    if (hidGuestTypetxt != "-1")
                    {
                        dllGuestType.SelectedIndex = _guestTypeList.FindIndex(t => t.GuestType == hidGuestTypetxt);
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

        private bool CheckIsSpecial()
        {
            using (SqlConnection sqlconn = DBHelp.getConn())
            {
                sqlconn.Open();
                SqlCommand sqlComm = sqlconn.CreateCommand();
                for (int i = 0; i < this.gvMain.Rows.Count; i++)
                {
                    CheckBox cb = (gvMain.Rows[i].FindControl("cbIsSpecial")) as CheckBox;
                    if (cb.Checked)
                    {
                        Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                        sqlComm.CommandText = string.Format(@"declare  @AllCount  int=0 ;
select @AllCount=COUNT(*)  from TB_BusCardUse where PONo ='{0}';
select @AllCount+=COUNT(*) from tb_UseCar   where PONo ='{0}';
select @AllCount+=COUNT(*) from TB_UseCarDetail where  PONo ='{0}';
select @AllCount+=COUNT(*) from Tb_DispatchList where  PONo ='{0}'; 
select @AllCount+=COUNT(*) from tb_OverTime where  PONo ='{0}';
select @AllCount+=COUNT(*) from tb_FundsUse where PONo ='{0}';
SELECT @AllCount;", lblIds.Text);
                        if (Convert.ToInt32(sqlComm.ExecuteScalar()) > 0)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null,
                                string.Format("<script>alert('项目{0},有费用，不能定义为特殊订单！');</script>", lblIds.Text));
                            return false;
                        }
                    }
                }
                sqlconn.Close();
            }
            return true;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string where = " PONo  in (";
            string expWhere = " PONo  in (";
            if (ViewState["isSpecialEdit"] == null)
            {
                if (CheckIsSpecial() == false)
                {
                    return;
                }
                for (int i = 0; i < this.gvMain.Rows.Count; i++)
                {
                    CheckBox cb = (gvMain.Rows[i].FindControl("cbIsSpecial")) as CheckBox;
                    if (cb.Checked)
                    {
                        Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                        where += "'" + lblIds.Text + "',";
                    }
                    else
                    {
                        Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                        expWhere += "'" + lblIds.Text + "',";
                    }
                }

                if (where != " PONo  in (")
                {
                    where = where.Substring(0, where.Length - 1) + ")";
                    var sql = "update CG_POOrder set IsSpecial=1 where " + where;
                    DBHelp.ExeCommand(sql);
                    //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                }

                if (expWhere != " PONo  in (")
                {
                    expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                    var sql = "update CG_POOrder set IsSpecial=0 where " + expWhere;
                    DBHelp.ExeCommand(sql);
                    // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                }
            }

            if (ViewState["isChengBenJiLiang"] == null)
            {
                where = " PONo  in (";
                expWhere = " PONo  in (";
                for (int i = 0; i < this.gvMain.Rows.Count; i++)
                {
                    CheckBox cb = (gvMain.Rows[i].FindControl("cbChengBenJiLiang")) as CheckBox;
                    if (cb.Checked)
                    {
                        Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                        where += "'" + lblIds.Text + "',";
                    }
                    else
                    {
                        Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                        expWhere += "'" + lblIds.Text + "',";
                    }
                }

                if (where != " PONo  in (")
                {
                    where = where.Substring(0, where.Length - 1) + ")";
                    var sql = "update CG_POOrder set ChengBenJiLiang=1 where " + where;
                    DBHelp.ExeCommand(sql);
                    //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                }

                if (expWhere != " PONo  in (")
                {
                    expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                    var sql = "update CG_POOrder set ChengBenJiLiang=0 where " + expWhere;
                    DBHelp.ExeCommand(sql);
                    // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                }
            }

            if (ViewState["isPOType"] == null)
            {

                //保存含税信息               
                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlCommand objCommand = conn.CreateCommand();

                    for (int i = 0; i < this.gvMain.Rows.Count; i++)
                    {
                        Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                        DropDownList drp = ((DropDownList)gvMain.Rows[i].FindControl("dllPOType"));
                        objCommand.CommandText = string.Format("update CG_POOrder set POType={1} where PONO='{0}'",
                            lblIds.Text, drp.Text);
                        objCommand.ExecuteNonQuery();

                    }
                    conn.Close();
                }

                ////保存含税信息
                //where = " PONo  in (";
                //expWhere = " POType  in (";


                //    for (int i = 0; i < this.gvMain.Rows.Count; i++)
                //    {
                //        Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                //        DropDownList drp = ((DropDownList)gvMain.Rows[i].FindControl("dllPOType"));

                //        //if (drp.Text == "1")
                //        //{
                //            where += "'" + lblIds.Text + "',";
                //            expWhere += "" + drp.Text + ",";
                //        //}
                //        //if (drp.Text == "2")
                //        //{
                //        //    expWhere += "'" + lblIds.Text + "',";
                //        //}       
                //    }

                //    if (where != " PONo  in (")
                //    {
                //        where = where.Substring(0, where.Length - 1) + ")";
                //        var sql = "update CG_POOrder set POType=1 where " + where;
                //        DBHelp.ExeCommand(sql);
                //    }
                //if (expWhere != " PONo  in (")
                //{
                //    expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                //    var sql = "update CG_POOrder set POType=2 where " + expWhere;
                //    DBHelp.ExeCommand(sql);
                //}
            }

            //项目模型
            if (ViewState["isModelEdit"] == null)
            {
                //保存项目模型信息               
                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlCommand objCommand = conn.CreateCommand();

                    for (int i = 0; i < this.gvMain.Rows.Count; i++)
                    {
                        Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                        DropDownList drp = ((DropDownList)gvMain.Rows[i].FindControl("ddlModel"));
                        objCommand.CommandText = string.Format("update CG_POOrder set Model='{1}' where PONO='{0}'",
                            lblIds.Text, drp.Text);
                        objCommand.ExecuteNonQuery();

                    }
                    conn.Close();
                }
            }

            if (ViewState["isFaxEdist"] == null)
            {
                //保存含税信息
                expWhere = " PONo  in (";
                var fpTypeBaseInfoService = new FpTypeBaseInfoService();
                gooQGooddList = fpTypeBaseInfoService.GetListArray("");
                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlCommand objCommand = conn.CreateCommand();

                    for (int i = 0; i < this.gvMain.Rows.Count; i++)
                    {
                        CheckBox cb = (gvMain.Rows[i].FindControl("cbIsPoFax")) as CheckBox;
                        if (cb.Checked)
                        {
                            Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                            //where += "'" + lblIds.Text + "',";
                            DropDownList drp = ((DropDownList)gvMain.Rows[i].FindControl("dllFPstye"));
                            objCommand.CommandText = string.Format("update CG_POOrder set IsPoFax=1, FpType='{1}',FpTax={2} where PONO='{0}'",
                                lblIds.Text, drp.Text, gooQGooddList.Find(p => p.FpType == drp.Text).Tax);
                            objCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                            expWhere += "'" + lblIds.Text + "',";
                        }
                    }
                    conn.Close();
                }



                if (expWhere != " PONo  in (")
                {
                    expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                    var sql = "update CG_POOrder set IsPoFax=0,FpType='',FpTax=0 where " + expWhere;
                    DBHelp.ExeCommand(sql);

                }
            }
            if (ViewState["isPlanDays"] == null)
            {
                //保存含税信息
                expWhere = " PONo  in (";

                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlCommand objCommand = conn.CreateCommand();

                    for (int i = 0; i < this.gvMain.Rows.Count; i++)
                    {
                        TextBox txtPlanDayForm = (gvMain.Rows[i].FindControl("txtPlanDays")) as TextBox;
                        if (CommHelp.VerifesToNum(txtPlanDayForm.Text) == false)
                        {
                            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('计划完工天数 格式错误！');</script>");
                            return;
                        }

                        Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                        objCommand.CommandText = string.Format("update CG_POOrder set PlanDays={1} where PONO='{0}'",
                            lblIds.Text, txtPlanDayForm.Text);
                        objCommand.ExecuteNonQuery();
                    }
                    conn.Close();
                }                 
            }

            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            //AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        protected void btnSaveIsClose_Click(object sender, EventArgs e)
        {
            if (ViewState["isCloseEdist"] == null)
            {
                string where = " PONo  in (";
                string expWhere = " PONo  in (";

                string whereEpec = " PONo  in (";
                string expWhereEpec = " PONo  in (";

                for (int i = 0; i < this.gvMain.Rows.Count; i++)
                {
                    CheckBox cb = (gvMain.Rows[i].FindControl("cbIsClose")) as CheckBox;
                    Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                    if (cb.Checked)
                    {
                        where += "'" + lblIds.Text + "',";
                    }
                    else
                    {
                        expWhere += "'" + lblIds.Text + "',";
                    }
                    decimal poTotal = Convert.ToDecimal(gvMain.Rows[i].Cells[3].Text);
                    var pp = gvMain.Rows[i].Cells[4].Text;
                    decimal maoliTotal = Convert.ToDecimal((pp == "" ? "0" : pp));
                    //项目金额=0，项目净利=0
                    if (poTotal == 0 && maoliTotal == 0)
                    {
                        whereEpec += "'" + lblIds.Text + "',";
                    }
                    else
                    {
                        expWhereEpec += "'" + lblIds.Text + "',";
                    }
                }

                if (where != " PONo  in (")
                {
                    where = where.Substring(0, where.Length - 1) + ")";
                    var sql = "update CG_POOrder set IsClose=1 where " + where;
                    DBHelp.ExeCommand(sql);
                    //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                }

                if (expWhere != " PONo  in (")
                {
                    expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                    var sql = "update CG_POOrder set IsClose=0 where " + expWhere;
                    DBHelp.ExeCommand(sql);
                    // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                }

                if (whereEpec != " PONo  in (")
                {
                    whereEpec = whereEpec.Substring(0, whereEpec.Length - 1) + ")";
                    var sql = "update CG_POOrder set IsSpecial=1 where " + whereEpec;
                    DBHelp.ExeCommand(sql);
                }

                //if (expWhereEpec != " PONo  in (")
                //{
                //    expWhereEpec = expWhereEpec.Substring(0, expWhereEpec.Length - 1) + ")";
                //    var sql = "update CG_POOrder set IsSpecial=0 where " + expWhereEpec;
                //    DBHelp.ExeCommand(sql);                    
                //}
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }
            Show();
        }

        protected void btnJieIsSelected_Click(object sender, EventArgs e)
        {

            string where = " PONo  in (";
            string expWhere = " PONo  in (";
            for (int i = 0; i < this.gvMain.Rows.Count; i++)
            {
                CheckBox cb = (gvMain.Rows[i].FindControl("cbJieIsSelected")) as CheckBox;
                if (cb.Checked)
                {
                    Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                    where += "'" + lblIds.Text + "',";
                }
                else
                {
                    Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                    expWhere += "'" + lblIds.Text + "',";
                }
            }

            if (where != " PONo  in (")
            {
                where = where.Substring(0, where.Length - 1) + ")";
                var sql = "update CG_POOrder set JieIsSelected=1 where " + where;
                DBHelp.ExeCommand(sql);
                //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }

            if (expWhere != " PONo  in (")
            {
                expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                var sql = "update CG_POOrder set JieIsSelected=0 where " + expWhere;
                DBHelp.ExeCommand(sql);
                // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");

            // AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        protected void cbHanShui_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb1 = sender as CheckBox;
            for (int i = 0; i < this.gvMain.Rows.Count; i++)
            {
                CheckBox cb = (gvMain.Rows[i].FindControl("cbIsClose")) as CheckBox;
                cb.Checked = cb1.Checked;
            }
        }
        protected void cbJieIsSelected_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb1 = sender as CheckBox;
            for (int i = 0; i < this.gvMain.Rows.Count; i++)
            {
                CheckBox cb = (gvMain.Rows[i].FindControl("cbJieIsSelected")) as CheckBox;
                cb.Checked = cb1.Checked;
            }
        }

        protected void cbIsSelected_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb1 = sender as CheckBox;
            for (int i = 0; i < this.gvMain.Rows.Count; i++)
            {
                CheckBox cb = (gvMain.Rows[i].FindControl("cbIsSelected")) as CheckBox;
                cb.Checked = cb1.Checked;
            }
        }

        protected void btnIsSelected_Click(object sender, EventArgs e)
        {
            if (ViewState["cbIsSelected"] == null)
            {
                string where = " PONo  in (";
                string expWhere = " PONo  in (";
                for (int i = 0; i < this.gvMain.Rows.Count; i++)
                {
                    CheckBox cb = (gvMain.Rows[i].FindControl("cbIsSelected")) as CheckBox;
                    if (cb.Checked)
                    {
                        Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                        where += "'" + lblIds.Text + "',";
                    }
                    else
                    {
                        Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                        expWhere += "'" + lblIds.Text + "',";
                    }
                }

                if (where != " PONo  in (")
                {
                    where = where.Substring(0, where.Length - 1) + ")";
                    var sql = "update CG_POOrder set IsSelected=1 where " + where;
                    DBHelp.ExeCommand(sql);
                    //base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                }

                if (expWhere != " PONo  in (")
                {
                    expWhere = expWhere.Substring(0, expWhere.Length - 1) + ")";
                    var sql = "update CG_POOrder set IsSelected=0 where " + expWhere;
                    DBHelp.ExeCommand(sql);
                    // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                }
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }
            //AspNetPager1.CurrentPageIndex = 1;
            Show();
        }

        protected void ddlIsPoFax_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlIsPoFax.Text == "1")
            {
                dllFPstye.Enabled = true;
            }
            else
            {
                dllFPstye.Enabled = false;
            }
        }

        protected void btnGuestType_Click(object sender, EventArgs e)
        {
            if (ViewState["isGuestType"] == null)
            {
                //保存客户类型信息               
                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlCommand objCommand = conn.CreateCommand();

                    for (int i = 0; i < this.gvMain.Rows.Count; i++)
                    {
                        Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                        DropDownList drp = ((DropDownList)gvMain.Rows[i].FindControl("dllGuestType"));
                        objCommand.CommandText = string.Format("update CG_POOrder set GuestType='{1}' where PONO='{0}'",
                            lblIds.Text, drp.Text);
                        objCommand.ExecuteNonQuery();

                    }
                    conn.Close();
                }
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }
            //Show();
        }

        protected void btnGuestPro_Click(object sender, EventArgs e)
        {
            if (ViewState["isGuestPro"] == null)
            {
                //保存客户属性信息               
                using (SqlConnection conn = DBHelp.getConn())
                {
                    conn.Open();
                    SqlCommand objCommand = conn.CreateCommand();

                    for (int i = 0; i < this.gvMain.Rows.Count; i++)
                    {
                        Label lblIds = (gvMain.Rows[i].FindControl("PONo")) as Label;
                        DropDownList drp = ((DropDownList)gvMain.Rows[i].FindControl("dllGuestPro"));
                        objCommand.CommandText = string.Format("update CG_POOrder set GuestPro={1} where PONO='{0}'",
                            lblIds.Text, drp.Text);
                        objCommand.ExecuteNonQuery();

                    }
                    conn.Close();
                }
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
            }

            //Show();
        }
    }
}
