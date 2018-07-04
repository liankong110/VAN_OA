using System;
using System.Linq;
using System.Web.UI.WebControls;
using VAN_OA.Model.JXC;
using System.Collections.Generic;
using VAN_OA.Dal.JXC;
using VAN_OA.Model;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class Sell_TuiSunCha_ReportTotalList : BasePage
    {
        readonly Sell_TuiSunCha_ReportService POSer = new Sell_TuiSunCha_ReportService();

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

                //主单
                var pOOrderList = new List<JXC_REPORTTotal>();
                gvMain.DataSource = pOOrderList;
                gvMain.DataBind();

                bool showAll = true;
                if (SysObj.IfShowAll("销售退货损差表", Session["currentUserId"], "") == false)
                {
                    ViewState["showAll"] = false;
                    showAll = false;
                }



                bool WFScanDepartList = true;
                if (showAll == false && SysObj.IfShowAll("销售退货损差表", Session["currentUserId"], "WFScanDepartList") == true)
                {
                    ViewState["WFScanDepartList"] = false;
                    WFScanDepartList = false;
                }

                var user = new List<User>();
                var userSer = new Dal.SysUserService();
                if (showAll)
                {
                    user = userSer.getAllUserByPOList();
                    user.Insert(0, new User() { LoginName = "全部", Id = -1 });
                }
                else if (WFScanDepartList)
                {
                    user = userSer.getAllUserByLoginName(string.Format(" and loginIPosition in (select loginIPosition from tb_User where id={0}) and loginIPosition<>''", Session["currentUserId"]));
                    user.Insert(0, new User() { LoginName = "全部", Id = 0 });
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
                if (Request["PONo"] != null)
                {
                    txtPONo.Text = Request["PONo"];
                    Show();
                }
            }
        }


        private void Show()
        {
            string sql = " ";

            if (txtPONo.Text.Trim()!= "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
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
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and CG_POOrder.PODate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('项目时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and CG_POOrder.PODate<='{0} 23:59:59'", txtTo.Text);
            }


            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and CG_POOrder.GuestName  like '%{0}%'", txtGuestName.Text.Trim());
            }



            var isColse = "";
            if (ddlUser.Text == "-1")//显示所有用户
            {
                if (cbIsSpecial.Checked)
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where IsSpecial=0 AND PONO=Sell_TuiSunCha_Report.PONO {0})", isColse);
                }
                else
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where IsSpecial=1 AND PONO=Sell_TuiSunCha_Report.PONO {0})", isColse);
                }
            }
            else if (ddlUser.Text == "0")//显示部门信息
            {
                var model = Session["userInfo"] as User;
                if (cbIsSpecial.Checked)
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') AND PONO=Sell_TuiSunCha_Report.PONO and IsSpecial=0 {1})", model.LoginIPosition, isColse);
                }
                else
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职' and loginIPosition<>'' and loginIPosition='{0}') AND PONO=Sell_TuiSunCha_Report.PONO and IsSpecial=1 {1})", model.LoginIPosition, isColse);
                }
            }
            else
            {

                if (cbIsSpecial.Checked)
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=Sell_TuiSunCha_Report.PONO  and IsSpecial=0 {1})", ddlUser.Text, isColse);
                }
                else
                {
                    sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName={0} AND PONO=Sell_TuiSunCha_Report.PONO  and IsSpecial=1 {1})", ddlUser.Text, isColse);

                }
            }
            if (ddlModel.Text != "全部")
            {
                sql += string.Format(" and EXISTS (select ID from CG_POOrder where Model='{0}' AND PONO=Sell_TuiSunCha_Report.PONO) ", ddlModel.Text);
            }
            if (ddlCompany.Text != "-1")
            {
                string where = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                sql += string.Format(" and EXISTS (select ID from CG_POOrder where IFZhui=0 and AppName IN(select id from tb_User where {0}) AND PONO=Sell_TuiSunCha_Report.PONO)", where);

            } 
            
            
            List<JXC_REPORTTotal> pOOrderList = POSer.GetListArray_Total(sql, "");
            decimal JLR = 0;
            lblJLR.Text = pOOrderList.Sum(t => t.maoliTotal).ToString();
            lblXSE.Text = pOOrderList.Sum(t => t.goodSellTotal).ToString();
            lblSJLR.Text = pOOrderList.Sum(t => t.TrueLiRun).ToString();

            lblFP.Text = pOOrderList.Sum(t => t.SellFPTotal).ToString();

            AspNetPager1.RecordCount = pOOrderList.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            gvMain.DataSource = pOOrderList;
            gvMain.DataBind();

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

        protected void gvMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }






        private void setValue(Label control, string value)
        {
            control.Text = value;
        }
    }
}
