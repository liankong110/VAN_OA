﻿using System;
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
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.ReportForms
{
    public partial class OverTimeList : BasePage
    {
        private tb_OverTimeSerivce OverTimeSer = new tb_OverTimeSerivce();



        private void Show()
        {
            string sql = " 1=1 ";
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and (formTime>='{0} 00:00:00' or '{0} 00:00:00' between formTime and totime)", txtFrom.Text);

                //sql += string.Format(" and '{0} 23:59:59' between formTime and totime", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                //sql += string.Format(" and '{0} 23:59:59' between formTime and totime", txtTo.Text);

                sql += string.Format(" and ('{0} 23:59:59' >=totime or '{0} 23:59:59' between formTime and totime)", txtTo.Text);
            } 
            if (txtAppName.Text != "")
            {
                sql += string.Format(" and loginName like '%{0}%'", txtAppName.Text);
            }

       
            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and tb_OverTime.ProNo like '%{0}%'", txtProNo.Text.Trim());
            }

            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and tb_OverTime.PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and EXISTS (select ID from CG_POOrder where Model='{0}' AND PONO=tb_OverTime.PONO) ", ddlModel.Text);
            }

            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and exists(select id from CG_POOrder where  IFZhui=0 and CG_POOrder.PONo=tb_OverTime.PONo and AE IN(select LOGINNAME from tb_User where {0}))", where);
            }

            sql += string.Format(@" and tb_OverTime.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='加班单') and state='通过')");


            
            if (ddlUser.Text == "-1")
            {

            }
            else if (ddlUser.Text == "0")
            {
                var model = Session["userInfo"] as User;
                sql += string.Format(" and guestDai in (select loginName from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}'))", model.LoginIPosition);
            }
            else 
            {
                sql += string.Format(" and guestDai ='{0}'", ddlUser.SelectedItem.Text);
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
            List<tb_OverTime> OverTimeServices = this.OverTimeSer.GetListArray(sql);
            decimal totalHours = 0;
            decimal total = 0;

            foreach (var model in OverTimeServices)
            {
                totalHours += model.BetweenHours;
                if (model.Total != null)
                {
                    total += Convert.ToDecimal(model.Total);
                }
            }
            lbltotal.Text = total.ToString();
            lblTotalHour.Text = totalHours.ToString();

            AspNetPager1.RecordCount = OverTimeServices.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = OverTimeServices;
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

                List<tb_OverTime> tb_OverServices = new List<tb_OverTime>();
                this.gvList.DataSource = tb_OverServices;
                this.gvList.DataBind();


                //List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();

                //user = userSer.getAllUserByLoginName("");
                //user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });


                TB_CompanyService comSer = new TB_CompanyService();
                var comList = comSer.GetListArray("");
                foreach (var m in comList)
                {
                    m.ComSimpName += "," + m.Id + "," + m.ComCode;
                }
                comList.Insert(0, new VAN_OA.Model.BaseInfo.TB_Company() { ComSimpName = "-1", ComName = "全部" });
                ddlCompany.DataSource = comList;
                ddlCompany.DataBind();

                List<VAN_OA.Model.User> aeList = new List<VAN_OA.Model.User>();
                bool showAll = true;
                if (QuanXian_ShowAll("公司加班记录查询") == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }
                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll("公司加班记录查询", Session["currentUserId"], "WFScanDepartList") == true)
                {
                    ViewState["WFScanDepartList"] = false;
                    WFScanDepartList = false;
                }

                if (showAll == true)
                {
                    aeList = userSer.getAllUserByPOList();
                    aeList.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                }
                else if (WFScanDepartList == true)
                {
                    aeList = userSer.getAllUserByLoginName(string.Format(" and loginIPosition in (select loginIPosition from tb_User where id={0}) and loginIPosition<>''", Session["currentUserId"]));
                    aeList.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = 0 });
                }
                else
                {
                    var model = Session["userInfo"] as User;
                    aeList.Insert(0, model);
                }


                ddlUser.DataSource = aeList;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";

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
