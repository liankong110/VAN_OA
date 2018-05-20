using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using VAN_OA.Model;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class WFPONotCaiReport : BasePage
    {
        CaiNotRuViewService _dal = new CaiNotRuViewService();
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
                if (Session["currentUserId"] != null)
                {
                    //List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                    //VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                    //if (VAN_OA.JXC.SysObj.IfShowAll("项目未采购清单", Session["currentUserId"], "ShowAll") == false)
                    //{
                    //    ViewState["showAll"] = false;
                    //    var model = Session["userInfo"] as User;
                    //    user.Insert(0, model);
                    //}
                    //else
                    //{
                    //    user = userSer.getAllUserByPOList();
                    //    user.Insert(0, new VAN_OA.Model.User() {LoginName = "全部", Id = -1});
                    //}

                    bool showAll = true;
                    if (QuanXian_ShowAll("项目未采购清单") == false)
                    {
                        ViewState["showAll"] = false;
                        showAll = false;
                    }
                    bool WFScanDepartList = true;
                    if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll("项目未采购清单", Session["currentUserId"], "WFScanDepartList") == true)
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

                    List<CaiNotRuView> list = new List<CaiNotRuView>();
                    gvMain.DataSource = list;
                    gvMain.DataBind();

                    if (Request["PONo"] != null)
                    {
                        txtPONo.Text = Request["PONo"].ToString();
                      
                        Show();
                    }
                }
            }
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
        private void Show()
        {
            string where = "1=1 ";
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }
                where += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (txtGuestName.Text.Trim() != "")
            {
                where += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }
            if (txtGoodNo.Text != "" || txtNameOrTypeOrSpec.Text != "" || txtNameOrTypeOrSpecTwo.Text != "")
            {
                if (txtGoodNo.Text != "")
                {
                    where += string.Format(" and GoodNo like '%{0}%'", txtGoodNo.Text);
                }
                if (txtNameOrTypeOrSpec.Text != "" && txtNameOrTypeOrSpecTwo.Text != "")
                {
                    where += string.Format(" and ((GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%') or (GoodTypeSmName like '%{1}%' or GoodName  like '%{1}%' or GoodSpec like '%{1}%'))",
                       txtNameOrTypeOrSpec.Text, txtNameOrTypeOrSpecTwo.Text);
                }
                else if (txtNameOrTypeOrSpec.Text != "" || txtNameOrTypeOrSpecTwo.Text != "")
                {
                    var NameOrTypeOrSpec = "";
                    if (txtNameOrTypeOrSpec.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpec.Text;
                    if (txtNameOrTypeOrSpecTwo.Text != "") NameOrTypeOrSpec = txtNameOrTypeOrSpecTwo.Text;

                    where += string.Format(" and (GoodTypeSmName like '%{0}%' or GoodName  like '%{0}%' or GoodSpec like '%{0}%')",
                       NameOrTypeOrSpec);
                }
            }           

            if (ddlUser.Text == "-1")//显示所有用户
            {

            }
            else if (ddlUser.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
                where += string.Format(" and (AE in (select loginName from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}'))", model.LoginIPosition);
            }
            else
            {
                where += string.Format(" and (AE='{0}')", ddlUser.SelectedItem.Text);
            }

            if (ddlCompany.Text != "-1")
            {
                string where1 = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                where += string.Format(" and AE IN(select loginName from tb_User where {0})", where1);
            }
            if (txtPOTimeFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtPOTimeFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单时间 格式错误！');</script>");
                    return;
                }
                where += string.Format(" and PODate>='{0} 00:00:00'", txtPOTimeFrom.Text);
            }
            if (txtPOTimeTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtPOTimeTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('订单时间 格式错误！');</script>");
                    return;
                }
                where += string.Format(" and PODate<='{0} 23:59:59'", txtPOTimeTo.Text);
            }
            if (txtSupplier.Text.Trim() != "")
            {
                if (cbPiPei.Checked)
                {
                    where += string.Format(" and Supplier='{0}'", txtSupplier.Text.Trim());
                }
                else
                {
                    where += string.Format(" and Supplier like '%{0}%'", txtSupplier.Text.Trim());
                }
            }
            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                where += string.Format(" and ProNo like '%{0}%'", txtProNo.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtPOName.Text.Trim()))
            {
                where += string.Format(" and PONAME like '%{0}%'", txtPOName.Text.Trim());
            }
            if (cbZero.Checked)
            {
                where += string.Format(" and lastNum>0 ");
            }
            var list = _dal.GetPONotCaiViewList(where); 
            AspNetPager1.RecordCount = list.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            gvMain.DataSource = list;
            gvMain.DataBind();
        }

        protected void gvMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMain.PageIndex = e.NewPageIndex;
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
    }
}
