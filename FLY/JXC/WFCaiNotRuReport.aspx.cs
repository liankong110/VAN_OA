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
using VAN_OA.Model.BaseInfo;

namespace VAN_OA.JXC
{
    public partial class WFCaiNotRuReport : BasePage
    {
        CaiNotRuViewService _dal = new CaiNotRuViewService();
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

                if (Session["currentUserId"] != null)
                {
                    List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                    VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();
                    if (VAN_OA.JXC.SysObj.IfShowAll("采购未检验清单", Session["currentUserId"], "ShowAll") == false)
                    {
                        ViewState["showAll"] = false;
                        var model = Session["userInfo"] as User;
                        user.Insert(0, model);
                    }
                    else
                    {
                        user = userSer.getAllUserByPOList();
                        user.Insert(0, new VAN_OA.Model.User() {LoginName = "全部", Id = -1});
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
                        ddlSupplier.Text = "0";
                        ddlSupplier.Text = "2";
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

            string where = " 1=1 ";
            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text) == false)
                {
                    return;
                }

                where += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtPOName.Text.Trim()))
            {
                where += string.Format(" and PONAME like '%{0}%'", txtPOName.Text.Trim());
            }
            if (txtGuestName.Text.Trim() != "")
            {
                where += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }
            if (txtGoodNo.Text != "")
            {
                where += string.Format(" and GoodNo like '%{0}%'", txtGoodNo.Text);
            }

            if (ddlUser.Text == "-1")//显示所有用户
            {
                //var model = Session["userInfo"] as User;
                //sql += string.Format(" and EXISTS (select ID from CG_POOrder where AppName in (select ID from tb_User where 1=1 and loginName<>'admin' and loginStatus<>'离职') AND PONO=JXC_REPORT.PONO )", Session["currentUserId"]);
            }           
            else
            {
                where += string.Format(" and AE='{0}'",ddlUser.SelectedItem.Text);               
            }
            if (ddlCompany.Text != "-1")
            {
                string where1 = string.Format(" CompanyCode='{0}'", ddlCompany.Text.Split(',')[2]);
                where += string.Format(" and AE IN(select loginName from tb_User where {0})", where1);
            }
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('入库时间 格式错误！');</script>");
                    return;
                }
                where += string.Format(" and MinInHouseDate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('入库时间 格式错误！');</script>");
                    return;
                }
                where += string.Format(" and MinInHouseDate<='{0} 23:59:59'", txtTo.Text);
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

            if (ddlSupplier.Text == "1")
            {
                where += string.Format(" and lastSupplier='库存'");     
            }
            if (ddlSupplier.Text == "2")
            {
                where += string.Format(" and lastSupplier<>'库存'");     
            }
            if (ddlIsHanShui.Text != "-1")
            {
                where += string.Format(" and  IsHanShui={0} ", ddlIsHanShui.Text);
            }
            if (!string.IsNullOrEmpty(txtSupplierName.Text.Trim()))
            {
                if (cbPiPei.Checked == false)
                {
                    where += string.Format(" and lastSupplier like '%{0}%'", txtSupplierName.Text.Trim());
                }
                else
                {
                    where += string.Format(" and lastSupplier='{0}'", txtSupplierName.Text.Trim());
                }
            }

            if (ddlModel.Text != "全部")
            {
                where += string.Format(" and EXISTS (select ID from CG_POOrder where Model='{0}' AND PONO=CaiNotRuView.PONO) ", ddlModel.Text);
            }

            var list = _dal.GetCaiNotRuViewList(where); 
            AspNetPager1.RecordCount = list.Count;
            this.gvMain.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            //最终价格×需检数量 的 总合计。
            lblAllPoTotal.Text = list.Sum(t => t.CaiGoodSum * t.lastPrice).ToString();

            lblYuFuTotal.Text = list.Sum(t => t.CaiGoodSum * t.SupplierInvoicePrice).ToString();
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
                CaiNotRuView model = e.Row.DataItem as CaiNotRuView;
                System.Web.UI.WebControls.Label lblIsHanShui = e.Row.FindControl("lblIsHanShui") as System.Web.UI.WebControls.Label;
                if (lblIsHanShui != null)
                {
                    lblIsHanShui.Text = model.IsHanShui == 1 ? "含税" : "不含税";
                }
                if (model.IsHanShui == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }
    }
}
