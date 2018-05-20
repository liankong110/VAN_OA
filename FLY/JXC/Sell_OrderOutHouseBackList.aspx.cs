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
using Microsoft.Office.Interop.Excel;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class Sell_OrderOutHouseBackList : BasePage
    {
        Sell_OrderOutHouseBackService POSer = new Sell_OrderOutHouseBackService();
        Sell_OrderOutHousesService ordersSer = new Sell_OrderOutHousesService();

        protected string GetType(object type)
        {
            if (type.ToString() == "0")
            {
                return "收到";
            }
           
            return "未收到";
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

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
               
                
                //主单
                List<Sell_OrderOutHouseBack> pOOrderList = new List<Sell_OrderOutHouseBack>();
                this.gvMain.DataSource = pOOrderList;
                this.gvMain.DataBind();

                //子单
                List<Sell_OrderOutHouseBacks> orders = new List<Sell_OrderOutHouseBacks>();
                gvList.DataSource = orders;
                gvList.DataBind();

                if (Session["currentUserId"] != null)
                {

//                    string sql =
//                        string.Format(
//                            @"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='查看所有'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='出库单签回列表') and sys_Object.AutoID is not null",
//                            Session["currentUserId"]);
                    if (QuanXian_ShowAll("出库单签回列表") == false)   
                  
                    {
                        ViewState["showAll"] = false;
                    }
                    else
                    {
                        user = userSer.getAllUserByPOList();
                    }
//                    string sql =
//                        string.Format(
//                            @"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='不能编辑'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='出库单签回列表') and sys_Object.AutoID is not null",
//                            Session["currentUserId"]);
//                    if (Convert.ToInt32(DBHelp.ExeScalar(sql)) <= 0)
                    if (NewShowAll_textName("出库单签回列表", "不能编辑")==false)
                    {
                        gvMain.Columns[0].Visible = false;
                    }


                    user.Insert(0, new VAN_OA.Model.User() {LoginName = "全部", Id = -1});
                    ddlUser.DataSource = user;
                    ddlUser.DataBind();
                    ddlUser.DataTextField = "LoginName";
                    ddlUser.DataValueField = "Id";

                    //if (VAN_OA.JXC.SysObj.IfShowAll(SysObj.Sell_OrderOutHouseBackList, Session["currentUserId"]) == false)
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
            string sql = " 1=1 ";

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and Sell_OrderOutHouse.PONo like '%{0}%'", txtPONo.Text.Trim());
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and Sell_OrderOutHouse.POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('签回日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and BackTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('签回日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and BackTime<='{0} 23:59:59'", txtTo.Text);
            }


            if (ddlStatue.Text != "")
            {
                sql += string.Format(" and Sell_OrderOutHouse.Status='{0}'", ddlStatue.Text);
            }
            else
            {
                sql += string.Format(" and Sell_OrderOutHouse.Status<>'不通过'");
            }

            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and Sell_OrderOutHouseBack.ProNo like '%{0}%'", txtProNo.Text.Trim());
            }           

            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and Sell_OrderOutHouse.Supplier like '%{0}%'", txtGuestName.Text.Trim());
            }
         
            if (txtOutProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtOutProNo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and Sell_OrderOutHouse.ProNo like '%{0}%'", txtOutProNo.Text.Trim());
            }     
            
            if (ViewState["showAll"] != null)
            {
                sql += string.Format(" and Sell_OrderOutHouse.CreateUserId={0}", Session["currentUserId"]);
            }
            else
            {
                if (ddlUser.Text != "-1")
                {
                    sql += string.Format(" and Sell_OrderOutHouse.CreateUserId={0}", ddlUser.Text);
                }
            }

            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and Sell_OrderOutHouse.CreateUserId IN(select id from tb_User where {0})", where);
            }

            if (ddlType.Text != "-1")
            {
                if (ddlType.Text == "0")
                {
                    sql += string.Format(" and BackType={0}", ddlType.Text);
                }
                if (ddlType.Text == "1")
                {
                    sql += string.Format(" and (BackType={0} or Sell_OrderOutHouseBack.Id is null)", ddlType.Text);
                }
              
            }

            if (cbIsSpecial.Checked)
            {
                sql += string.Format(" and EXISTS (select ID from CG_POOrder where IsSpecial=0 AND PONO=Sell_OrderOutHouse.PONO) ");
            }
            //else
            //{
            //    sql += string.Format(" and EXISTS (select ID from CG_POOrder where IsSpecial=1 AND PONO=Sell_OrderOutHouse.PONO) ");
            //}
            List<Sell_OrderOutHouseView> pOOrderList = this.POSer.GetListArrayAndOutList(sql);
            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvMain.DataSource = pOOrderList;
            this.gvMain.DataBind();

            //子单
            List<Sell_OrderOutHouses> orders = new List<Sell_OrderOutHouses>();
            gvList.DataSource = orders;
            gvList.DataBind();          

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
                List<Sell_OrderOutHouses> orders = ordersSer.GetListArray(" 1=1 and Sell_OrderOutHouses.id=" + e.CommandArgument);               
               
                ViewState["Orders"] = orders;
                gvList.DataSource = orders;
                gvList.DataBind();            
            }
            else if (e.CommandName == "ReEdit")
            {

                if (e.CommandArgument.ToString() == "0")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('出库签回单不存在！');</script>");
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
                string sql = "select pro_Id from A_ProInfo where pro_Type='出库单签回'";
                string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='出库单签回')", e.CommandArgument);
                string url = "~/JXC/WFSell_OrderOutHouseBack.aspx?ProId=" + DBHelp.ExeScalar(sql) + "&allE_id=" + e.CommandArgument + "&EForm_Id=" + DBHelp.ExeScalar(efromId) + "&&ReAudit=true";
                Response.Redirect(url);
            }
            
        }



        Sell_OrderOutHouses SumOrders = new Sell_OrderOutHouses();
        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                Sell_OrderOutHouses model = e.Row.DataItem as Sell_OrderOutHouses;
                 
                //SumOrders.Total += model.Total;
                SumOrders.GoodSellPriceTotal += model.GoodSellPriceTotal;

            }
            


            // 合计
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                setValue(e.Row.FindControl("lblGoodName") as System.Web.UI.WebControls.Label, "合计");//合计                      
                setValue(e.Row.FindControl("lblTotal1") as System.Web.UI.WebControls.Label, SumOrders.GoodSellPriceTotal.ToString());//成本总额    
                //setValue(e.Row.FindControl("lblTotal1") as System.Web.UI.WebControls.Label, SumOrders.GoodSellPriceTotal.ToString());//成本总额    
            }

        }


        private void setValue(System.Web.UI.WebControls.Label control, string value)
        {
            control.Text = value;
        }

      
    }
}
