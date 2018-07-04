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
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class Sell_OrderPFBackList : BasePage
    {
        Sell_OrderFPBackService POSer = new Sell_OrderFPBackService();
        Sell_OrderFPsService ordersSer = new Sell_OrderFPsService();

        protected string GetType(object type)
        {
            if (type.ToString() == "0")
            {
                return "收到";
            }
            if (type.ToString() == "2")
            {
                return "未开具发票";
            }

            return "未收到";
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

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
               

                //主单
                List<Sell_OrderFPBack> pOOrderList = new List<Sell_OrderFPBack>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //子单
                List<Sell_OrderFPBacks> orders = new List<Sell_OrderFPBacks>();
                gvList.DataSource = orders;
                gvList.DataBind();

                if (Session["currentUserId"] != null)
                {
//                    string sql =
//                        string.Format(
//                            @"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='查看所有'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='发票单签回列表') and sys_Object.AutoID is not null",
//                            Session["currentUserId"]);
                    if (QuanXian_ShowAll("发票单签回列表") == false)
                    {
                        ViewState["showAll"] = false;
                    }
                    else
                    {
                        user = userSer.getAllUserByPOList();
                    }

//                   string sql =
//                        string.Format(
//                            @"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='不能编辑'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='发票单签回列表') and sys_Object.AutoID is not null",
//                            Session["currentUserId"]);
//                    if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                    if (NewShowAll_textName("发票单签回列表", "不能编辑")==false)
                    {
                        gvMain.Columns[0].Visible = false;
                    }

                    user.Insert(0, new VAN_OA.Model.User() {LoginName = "全部", Id = -1});
                    ddlUser.DataSource = user;
                    ddlUser.DataBind();
                    ddlUser.DataTextField = "LoginName";
                    ddlUser.DataValueField = "Id";
                    //if (VAN_OA.JXC.SysObj.IfShowAll(SysObj.Sell_OrderPFBackList, Session["currentUserId"]) == false)
                    //{
                    //    ViewState["showAll"] = false;
                    //}

                    if (Request["PONo"] != null)
                    {
                        txtPONo.Text = Request["PONo"].ToString();
                        if (Request["Type"] != null)
                        {
                            ddlType.Text = Request["Type"].ToString();
                        }
                        cbIsSpecial.Checked = false;
                        Show();
                    }
                }

            }
        }


        private void Show()
        {
            string sql = "";

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
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
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('签回日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and FPBackTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('签回日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and FPBackTime<='{0} 23:59:59'", txtTo.Text);
            }


            if (ddlStatue.Text != "")
            {
                sql += string.Format(" and Sell_OrderFPBack.Status='{0}'", ddlStatue.Text);
            }
            //else
            //{
            //    sql += string.Format(" and Sell_OrderFPBack.Status<>'不通过'");
            //}

            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and Sell_OrderFPBack.ProNo like '%{0}%'", txtProNo.Text.Trim());
            }

            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and CG_POOrder.GuestName  like '%{0}%'", txtGuestName.Text.Trim());
            }
            if (ViewState["showAll"] != null)
            {
                sql += string.Format(" and Sell_OrderFP.CreateUserId={0}", Session["currentUserId"]);
            }
            else
            {
                if (ddlUser.Text != "-1")
                {
                    sql += string.Format(" and Sell_OrderFP.CreateUserId={0}", ddlUser.Text);
                }
                
            }

            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and Sell_OrderFP.CreateUserId IN(select id from tb_User where {0})", where);
            }
            if (txtFPNo.Text.Trim() != "")
            {
                sql += string.Format(" and Sell_OrderFP.FPNo like '%{0}%'", txtFPNo.Text.Trim());
            }

            if (ddlType.Text != "-1")
            {
                if (ddlType.Text == "0")
                {
                    sql += string.Format(" and FPBackType={0}", ddlType.Text);
                }
                if (ddlType.Text == "1")
                {
                    sql += string.Format(" and FPBackType={0} ", ddlType.Text);
                }
                if (ddlType.Text == "2")
                {
                    sql += string.Format(" and Sell_OrderFP.Id is null ");
                }
                if (ddlType.Text == "3")
                {
                    sql += string.Format(" and (Sell_OrderFP.Id is null or FPBackType=1 )");
                }
                
            }
            if (cbIsPoFax.Checked)
            {
                sql += string.Format(" and CG_POOrder.IsPoFax=0 ");
            }
            else
            {
                sql += string.Format(" and CG_POOrder.IsPoFax=1 ");
            }

            if (cbIsSpecial.Checked)
            {
                sql += string.Format(" and CG_POOrder.IsSpecial=0 ");
            }
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and CG_POOrder.Model='{0}'", ddlModel.Text);
            }
            List<Sell_OrderFPView> pOOrderList = this.POSer.GetListArrayAndFPInfo(sql);

            //decimal allTotal = 0;
            //foreach (var m in pOOrderList)
            //{
            //    allTotal += m.Total;
            //}
            //lblTotal.Text = allTotal.ToString();
            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();


            //if (pOOrderList.Count > 0)
            //{
            //    CG_POOrderService orderSer = new CG_POOrderService();
            //    var list = orderSer.GetOrder_ToInvoice_1(string.Format(" PONo='{0}'", pOOrderList[0].PONo));
            //    if (list.Count > 0)
            //    {

            //        lblPOTotal.Text = "项目(" + pOOrderList[0].PONo + ")---" + (list[0].POTotal - list[0].TuiTotal).ToString();
            //    }
            //    else
            //    {
            //        lblPOTotal.Text = "0";
            //    }


            //    //lblPOTotal.Text = "";
            //}
            //子单
            List<Sell_OrderFPBacks> orders = new List<Sell_OrderFPBacks>();
            gvList.DataSource = orders;
            gvList.DataBind();





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
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
                var model = e.Row.DataItem as Sell_OrderFPView;
                if (model != null)
                {
                    if (model.FPBackType == 1)
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;
                    }
                    if (model.FPBackType ==2)
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGreen;
                    }
                }
            }
        }

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                List<Sell_OrderFPs> orders = ordersSer.GetListArray(" 1=1 and Sell_OrderFPs.id=" + e.CommandArgument);

                //ViewState["Orders"] = orders;
                gvList.DataSource = orders;
                gvList.DataBind();
            }
            else if (e.CommandName == "ReEdit")
            {

                if (e.CommandArgument.ToString() == "0")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('发票签回单不存在！');</script>");
                    return;
                }
                //是否是此单据的申请人
                var model = POSer.GetModel(Convert.ToInt32(e.CommandArgument));

                //if (Session["currentUserId"].ToString() != model.CreateUserId.ToString())
                //{

                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('必须由原单据申请人 重新发起，其他人不能重新提交编辑！');</script>");
                //    return;
                //}

                //首先单子要先通过 
                if (model != null && model.Status == "通过")
                {

                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('此单据必须已经审批通过才能重新编辑！');</script>");
                    return;
                }


                string sql = "select pro_Id from A_ProInfo where pro_Type='发票单签回'";

                string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='发票单签回')", e.CommandArgument);
                string url = "~/JXC/WFSell_OrderFPBack.aspx?ProId=" + DBHelp.ExeScalar(sql) + "&allE_id=" + e.CommandArgument + "&EForm_Id=" + DBHelp.ExeScalar(efromId) + "&&ReAudit=true";
                Response.Redirect(url);


                //没有做过检验单


            }
        }



        Sell_OrderFPBacks SumOrders = new Sell_OrderFPBacks();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                Sell_OrderFPs model = e.Row.DataItem as Sell_OrderFPs;
                SumOrders.Total += model.Total;
            }

            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblGoodName") as Label, "合计");//合计                      
                setValue(e.Row.FindControl("lblTotal") as Label, SumOrders.Total.ToString());//成本总额    
            }

        }


        private void setValue(Label control, string value)
        {
            control.Text = value;
        }
    }
}
