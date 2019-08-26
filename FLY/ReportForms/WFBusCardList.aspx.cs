using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.ReportForms;

using VAN_OA.Model.ReportForms;
using System.Data;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model;
using VAN_OA.Dal.BaseInfo;

namespace VAN_OA.ReportForms
{
    public partial class WFBusCardList : BasePage
    {
        private TB_BusCardRecordService BusCardRecordSer = new TB_BusCardRecordService();

        private TB_BusCardUseService BusCardUseSer = new TB_BusCardUseService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["backurl"] = "~/ReportForms/WFBusCardList.aspx";
            base.Response.Redirect("~/ReportForms/WFBusCardRecord.aspx");
        }

        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            Session["backurl"] = "~/ReportForms/WFBusCardList.aspx";
            base.Response.Redirect("~/ReportForms/WFBusCardUse.aspx");
        }


        private void Show()
        {
            string sql = " 1=1 ";


            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and BusCardDate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and BusCardDate<='{0} 23:59:59'", txtTo.Text);
            }


            if (ddlCardNo.Text != "")
            {
                sql += string.Format(" and BusCardNo like '%{0}%'", ddlCardNo.Text);
            }
            List<TB_BusCardRecord> pos = this.BusCardRecordSer.GetListArray(sql);
            AspNetPager2.RecordCount = pos.Count;
            this.gvCardRecordList.PageIndex = AspNetPager2.CurrentPageIndex - 1;
            this.gvCardRecordList.DataSource = pos;
            this.gvCardRecordList.DataBind();
        }         

        private void Show1()
        {
            string sql = " 1=1 ";


            if (txtFromUse.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFromUse.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return ;
                }
                sql += string.Format(" and BusCardDate>='{0} 00:00:00'", txtFromUse.Text);
            }

            if (txtToUse.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtToUse.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and BusCardDate<='{0} 23:59:59'", txtToUse.Text);
            }


            if (ddlCardNo1.Text != "")
            {
                sql += string.Format(" and BusCardNo like '%{0}%'", ddlCardNo1.Text);
            }
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }

                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and POGuestName like '%{0}%'", txtGuestName.Text.Trim());
            }
            sql += string.Format(@" and Status='通过' ");

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


            List<TB_BusCardUse> pos = this.BusCardUseSer.GetListArray(sql);
            lblTotal.Text = pos.Sum(t => t.UseTotal).ToString();
            AspNetPager1.RecordCount = pos.Count;
            this.gvCardNOUse.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvCardNOUse.DataSource = pos;
            this.gvCardNOUse.DataBind();
        }
        protected void btnDo_Click(object sender, EventArgs e)
        {
            ShowAll();
        }

        private void ShowAll()
        {
            List<TB_BusCardUse> pos = this.BusCardUseSer.GetListArray("");
            foreach (var m in pos)
            {
                string sql = "select pro_Id from A_ProInfo where pro_Type='公交卡使用'";

                string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='公交卡使用')", m.Id);
                object eformIdObj = DBHelp.ExeScalar(efromId);

                object proId = DBHelp.ExeScalar(sql);
                if ((eformIdObj is DBNull) || eformIdObj == null)
                {
                    sql = "select ProNo from TB_BusCardUse where id=" + m.Id;
                    var proNo = DBHelp.ExeScalar(sql);
                    string strProNo = "";
                    if (proNo is DBNull || proNo == null || proNo.ToString() == "")
                    {
                        strProNo = new tb_EFormService().GetAllE_No("TB_BusCardUse");
                        DBHelp.ExeCommand(string.Format(" update TB_BusCardUse set ProNo='{0}',STATUS='通过' where id={1}", strProNo, m.Id));
                    }
                    else
                    {
                        strProNo = proNo.ToString();
                    }
                    string insertEform = string.Format("insert into tb_EForm values ({0},1,getdate(),1,getdate(),'通过',{1},0,0,'{2}','',GETDATE())", proId,
                         m.Id, strProNo);
                    DBHelp.ExeCommand(insertEform);
                    efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='公交卡使用')", m.Id);
                    eformIdObj = DBHelp.ExeScalar(efromId);
                }

            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            AspNetPager2.CurrentPageIndex = 1;
            Show();
        }

        protected void btnSelect1_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
            Show1();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            Show1();
        }
        protected void AspNetPager2_PageChanged(object src, EventArgs e)
        {
            Show();
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCardRecordList.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvCardNOUse_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvCardNOUse.PageIndex = e.NewPageIndex;
            Show1();
        }


        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void gvCardNOUse_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            this.BusCardRecordSer.Delete(Convert.ToInt32(this.gvCardRecordList.DataKeys[e.RowIndex].Value.ToString()));
            Show();

        }

        protected void gvCardNOUse_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string sql = string.Format("delete from tb_EForms where e_Id=( select id from tb_EForm where proId=35 and allE_id={0});delete from tb_EForm where proId=35 and allE_id={0};",gvCardNOUse.DataKeys[e.RowIndex].Value);
            if (DBHelp.ExeCommand(sql))
            {
                this.BusCardUseSer.Delete(Convert.ToInt32(this.gvCardNOUse.DataKeys[e.RowIndex].Value.ToString()));
                Show1();
            }
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["BusCard"] = "~/ReportForms/WFBusCardList.aspx";
            base.Response.Redirect("~/ReportForms/WFBusCardRecord.aspx?Id=" + this.gvCardRecordList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void gvCardNOUse_RowEditing(object sender, GridViewEditEventArgs e)
        {

            string sql = "select pro_Id from A_ProInfo where pro_Type='公交卡使用'";

            string efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='公交卡使用')", this.gvCardNOUse.DataKeys[e.NewEditIndex].Value);
            object eformIdObj = DBHelp.ExeScalar(efromId);

            object proId = DBHelp.ExeScalar(sql);
            if ((eformIdObj is DBNull) || eformIdObj == null)
            {
                sql = "select ProNo from TB_BusCardUse where id=" + gvCardNOUse.DataKeys[e.NewEditIndex].Value;
                var proNo = DBHelp.ExeScalar(sql);
                string strProNo = "";
                if (proNo is DBNull || proNo == null || proNo.ToString() == "")
                {
                    strProNo = new tb_EFormService().GetAllE_No("TB_BusCardUse");
                    DBHelp.ExeCommand(string.Format(" update TB_BusCardUse set ProNo='{0}',STATUS='通过' where id={1}", strProNo, gvCardNOUse.DataKeys[e.NewEditIndex].Value));
                }
                else
                {
                    strProNo = proNo.ToString();
                }
                string insertEform = string.Format("insert into tb_EForm values ({0},1,getdate(),1,getdate(),'通过',{1},0,0,'{2}','',GETDATE())", proId,
                    gvCardNOUse.DataKeys[e.NewEditIndex].Value, strProNo);
                DBHelp.ExeCommand(insertEform);
                efromId = string.Format("select id from tb_EForm where alle_id={0} and proId=(select pro_Id from A_ProInfo where pro_Type='公交卡使用')", this.gvCardNOUse.DataKeys[e.NewEditIndex].Value);
                eformIdObj = DBHelp.ExeScalar(efromId);
            }

            string url = "~/ReportForms/WFBusCardUse.aspx?ProId=" + proId + "&allE_id=" + this.gvCardNOUse.DataKeys[e.NewEditIndex].Value + "&EForm_Id=" + eformIdObj + "&&ReAudit=true";
            Response.Redirect(url);

            //Session["BusCard"] = "~/ReportForms/WFBusCardList.aspx";
            //base.Response.Redirect("~/ReportForms/WFBusCardUse.aspx?Id=" + this.gvCardNOUse.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {

                DataTable cardInfo = DBHelp.getDataTable("select '' as CardNo union select BusNo as CardNo from Base_BusInfo");
              
                ddlCardNo.DataSource = cardInfo;
                ddlCardNo.DataBind();

                ddlCardNo1.DataSource = cardInfo;
                ddlCardNo1.DataBind();

                List<TB_BusCardRecord> BusCardRecordList = new List<TB_BusCardRecord>();
                this.gvCardRecordList.DataSource = BusCardRecordList;
                this.gvCardRecordList.DataBind();


                List<TB_BusCardUse> BusCardUseList = new List<TB_BusCardUse>();
                this.gvCardNOUse.DataSource = BusCardUseList;
                this.gvCardNOUse.DataBind();

                List<VAN_OA.Model.User> aeList = new List<VAN_OA.Model.User>();
                bool showAll = true;
                if (QuanXian_ShowAll("公交卡充值/使用记录") == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }
                bool WFScanDepartList = true;
                if (showAll == false && VAN_OA.JXC.SysObj.IfShowAll("公交卡充值/使用记录", Session["currentUserId"], "WFScanDepartList") == true)
                {
                    ViewState["WFScanDepartList"] = false;
                    WFScanDepartList = false;
                }
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
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
                ddlAE.DataSource = aeList;
                ddlAE.DataBind();
                ddlAE.DataTextField = "LoginName";
                ddlAE.DataValueField = "Id";
                #region 是否有删除功能
                if (Session["currentUserId"] != null)
                {
                    VAN_OA.Dal.TB_AdminDeleteService deleteSer = new VAN_OA.Dal.TB_AdminDeleteService();
                    if (deleteSer.CheckIsExistByUserId(Convert.ToInt32(Session["currentUserId"])) == false)
                    {
                        gvCardRecordList.Columns[1].Visible = false;
                        gvCardNOUse.Columns[1].Visible = false;
                    }
                }
                if (NewShowAll_textName("公交卡充值/使用记录", "编辑") == false)
                {
                    gvCardNOUse.Columns[0].Visible = false;
                    gvCardRecordList.Columns[0].Visible = false;
                }
                if (NewShowAll_textName("公交卡充值/使用记录", "删除") == false)
                {
                    gvCardNOUse.Columns[1].Visible = false;
                    gvCardRecordList.Columns[1].Visible = false;
                }
                #endregion

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
