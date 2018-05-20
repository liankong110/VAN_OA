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
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.EFrom;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model;

namespace VAN_OA.ReportForms
{
    public partial class UseCarDetailList : BasePage
    {
        private  TB_UseCarDetailService useCarSer= new TB_UseCarDetailService();

        private tb_UseCarService useCarSer_Si = new tb_UseCarService();

        private void Show()
        {
            string sql = " 1=1 ";
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('申请日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and AppTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('申请日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and AppTime<='{0} 23:59:59'", txtTo.Text);
            }


            if (txtUseFromTime.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtUseFromTime.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('使用日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and useDate>='{0} 00:00:00'", txtUseFromTime.Text);
            }

            if (txtUseToTime.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtUseToTime.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('使用日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and useDate<='{0} 23:59:59'", txtUseToTime.Text);
            } 
            
            
            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and TB_UseCarDetail.GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }


            if (txtDriver.Text != "")
            {
                sql += string.Format(" and Driver like '%{0}%'", txtDriver.Text);
            }

            if (txtAppName.Text != "")
            {
                sql += string.Format(" and LoginName like '%{0}%'", txtAppName.Text);
            }

            if (txtPONo.Text .Trim()!= "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and TB_UseCarDetail.PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and exists(select id from CG_POOrder where  IFZhui=0 and CG_POOrder.PONo=TB_UseCarDetail.PONo and AE IN(select LOGINNAME from tb_User where {0}))", where);
            }

            if (ddlCarNo.Text != "")
            {
                sql += string.Format(" and CarNo like '%{0}%'", ddlCarNo.Text);
            }
            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and TB_UseCarDetail.ProNo like '%{0}%'", txtProNo.Text.Trim());
            }


             
            if (ddlIsSpecial.Text != "-1")
            {
                sql += " and IsSpecial=" + ddlIsSpecial.Text;
            }

            if (ddlClose.Text != "-1")
            {
                sql += " and IsClose=" + ddlClose.Text;
            }

            if (ddlIsSelect.Text != "-1")
            {
                sql += " and IsSelected=" + ddlIsSelect.Text;
            }
            if (ddlGuestTypeList.Text != "全部")
            {
                sql += string.Format(" and CG_POOrder.GuestType='{0}'" , ddlGuestTypeList.Text);
            }
            if (ddlGuestProList.Text != "-2")
            {
                sql += " and CG_POOrder.GuestPro=" + ddlGuestProList.Text;
            }
            if (ddlJieIsSelected.Text != "-1")
            {
                sql += " and JieIsSelected=" + ddlJieIsSelected.Text;
            }
           
            
            if (ddlUser.Text == "-1")//显示所有用户
            {
                 
            }
            else if (ddlUser.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
                sql += string.Format(" and CG_POOrder.AE in (select loginName from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}')", model.LoginIPosition);
            }
            else
            {
                sql += string.Format(" and CG_POOrder.AE='{0}'", ddlUser.SelectedItem.Text);
            }

            sql += string.Format(@" and TB_UseCarDetail.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='用车明细表') and state='通过')");
            decimal Total = 0;
            decimal TotalPrice;
            List<TB_UseCarDetail> UseCarServices = this.useCarSer.GetListArrayReps_1(sql, out Total,out TotalPrice);
            lblTotal.Text = Total.ToString();
            lblTotalPrice.Text = TotalPrice.ToString();
            AspNetPager1.RecordCount = UseCarServices.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = UseCarServices;
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
        protected void AspNetPager2_PageChanged(object src, EventArgs e)
        {
            showSi();          
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
                DataTable carInfos = DBHelp.getDataTable("select ''as CarNO union select CarNO from TB_CarInfo");
                ddlCarNo.DataSource = carInfos;
                ddlCarNo.DataBind();
                ddlCarNo.Text = "";

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

                //List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                //VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                //user = userSer.getAllUserByPOList();
                //user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                //ddlUser.DataSource = user;
                //ddlUser.DataBind();
                //ddlUser.DataTextField = "LoginName";
                //ddlUser.DataValueField = "Id";
                bool showAll = true;
                if (QuanXian_ShowAll("用车统计") == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }
                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll("用车统计", Session["currentUserId"], "WFScanDepartList") == true)
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

                ddlAE.DataSource = user;
                ddlAE.DataBind();
                ddlAE.DataTextField = "LoginName";
                ddlAE.DataValueField = "Id";

                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();


                dllCompayList.DataSource = comList;
                dllCompayList.DataBind();


                List<TB_UseCarDetail> UseCarServices = new List<TB_UseCarDetail>();
                this.gvList.DataSource = UseCarServices;
                this.gvList.DataBind();


                List<tb_UseCar> UseCarServices_Si = new List<tb_UseCar>();
                this.GvSi.DataSource = UseCarServices_Si;
                this.GvSi.DataBind();
            }
        }

        protected void GvSi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GvSi.PageIndex = e.NewPageIndex;
            showSi();
        }

        protected void GvSi_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }

        }

        protected void btnSiSelect_Click(object sender, EventArgs e)
        {
            AspNetPager2.CurrentPageIndex = 1;
            showSi();
        }

        private void showSi()
        {
            string sql = " 1=1 ";
            if (txtSiFrom.Text != "")
            {
                sql += string.Format(" and datetime>='{0} 00:00:00'",txtSiFrom.Text);
            }

            if (txtSiTo.Text != "")
            {
                sql += string.Format(" and datetime<='{0} 23:59:59'",txtSiTo.Text);
            }


            if (txtSiApper.Text != "")
            {
                sql += string.Format(" and loginName like '%{0}%'", txtSiApper.Text);
            }


            if (txtProNo1.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo1.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo1.Text.Trim());
            }

            if (txtSecondPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtSecondPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and PONO like '%{0}%'", txtSecondPONo.Text.Trim());
            }

            if (dllCompayList.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", dllCompayList.Text.Split(',')[2]);
                sql += string.Format(" and exists(select id from CG_POOrder where  IFZhui=0 and CG_POOrder.PONo=tb_UseCar.PONo and AE IN(select LOGINNAME from tb_User where {0}))", where);
            }
            
            if (ddlAE.Text == "-1")//显示所有用户
            {

            }
            else if (ddlAE.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
                sql += string.Format(" and AE in (select loginName from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}')", model.LoginIPosition);
            }
            else
            {
                sql += string.Format(" and AE='{0}'", ddlAE.SelectedItem.Text);
            }
            sql += string.Format(@" and tb_UseCar.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='私车公用申请单') and state='通过')");
            decimal Total = 0;
            decimal Total1 = 0;
            List<tb_UseCar> UseCarServices = this.useCarSer_Si.GetListArray_Req(sql, out Total, out Total1);
            AspNetPager2.RecordCount = UseCarServices.Count;
            this.GvSi.PageIndex = AspNetPager2.CurrentPageIndex - 1;
            lblSiTotal.Text = Total.ToString();
            lblSiTotalPrice.Text = Total1.ToString();
            this.GvSi.DataSource = UseCarServices;
            this.GvSi.DataBind();
        }
    }
}
