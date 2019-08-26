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
using VAN_OA.Dal.ReportForms;
using System.Collections.Generic;
using VAN_OA.Model.ReportForms;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model;
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.ReportForms
{
    public partial class FundList : BasePage
    {


        private tb_FundsUseService FundSer = new tb_FundsUseService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["POUrl"] = "~/ReportForms/GusetInfoList.aspx";
            base.Response.Redirect("~/ReportForms/GusetInfo.aspx");
        }


        private void Show()
        {
            string sql = " 1=1 ";
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return ;
                }
                sql += string.Format(" and datetiem>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if ( CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and datetiem<='{0} 23:59:59'", txtTo.Text);
            }

            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and tb_FundsUse.GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }
            if (txtPOName.Text.Trim() != "")
            {
                sql += string.Format(" and tb_FundsUse.POName like '%{0}%'", txtPOName.Text.Trim());
            }
            if (txtPONO.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONO.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and tb_FundsUse.PONo like '%{0}%'", txtPONO.Text.Trim());
            }
            if (txtShuoMing.Text != "")
            {
                sql += string.Format(" and ShuoMing like '%{0}%'", txtShuoMing.Text);
            }
            if (txtYongTu.Text != "")
            {
                sql += string.Format(" and useTo like '%{0}%'", txtYongTu.Text);
            }

            if (ddlFuHao.Text != "-" && !string.IsNullOrEmpty(txtTotal.Text))
            {
                if (CommHelp.VerifesToNum(txtTotal.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请款金额 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and ISNULL(CaiTotal,0)+ISNULL(RenTotal,0)+ISNULL(HuiTotal,0)+ISNULL(XingCaiTotal,0) {0}{1}", ddlFuHao.Text, txtTotal.Text);
            }
            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and tb_FundsUse.ProNo like '%{0}%'", txtProNo.Text.Trim());
            }
            if (ddlFundType.Text != "-1")
            {
                sql += string.Format(" and FundType='{0}'", ddlFundType.Text);
            }
            if (ddlCompany.Text != "-1")
            {
                sql += string.Format(" and TB_Company.ComCode='{0}'", ddlCompany.Text.Split(',')[2]);                
            }

            if (ddlUser.Text != "-1")
            {
                sql += string.Format(" and AE='{0}'", ddlUser.SelectedItem.Text);
            }
            if (ddlState.Text != "全部")
            {
                sql += string.Format(" and tb_FundsUse.state='{0}'", ddlState.Text);
            }
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and Model='{0}'", ddlModel.Text);
            }

            //增加查询条件
            if (ddlGuestTypeList.SelectedValue != "全部")
            {
                sql += string.Format(" and GuestType='{0}'", ddlGuestTypeList.SelectedValue);
            }

            if (ddlGuestProList.SelectedValue != "-2")
            {
                sql += string.Format(" and GuestPro={0}", ddlGuestProList.SelectedValue);
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
            if (ddlIsSpecial.Text != "-1")
            {
                sql += string.Format(" and IsSpecial={0} ", ddlIsSpecial.Text);
            }
            List<tb_FundsUse> fundList = this.FundSer.GetFundList(sql);

            lblAllTotal.Text = fundList.Sum(t => t.AllTotal).ToString();
            lblAllTrueTotal.Text = fundList.Sum(t => t.AllTrueTotal).ToString();

            AspNetPager1.RecordCount = fundList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = fundList;
            this.gvList.DataBind();
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
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

           

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
           

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
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

                var user = new List<Model.User>();
                var userSer = new Dal.SysUserService();
                if (QuanXian_ShowAll("请款单列表") == false)
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

                if (!NewShowAll_textName("请款单列表", "编辑"))
                {
                    gvList.Columns[0].Visible = false;
                }
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";

                var fundList = new List<tb_FundsUse>();
                gvList.DataSource = fundList;
                gvList.DataBind();

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

            }
        }
    }
}
