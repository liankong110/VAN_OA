using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;


namespace VAN_OA.JXC
{
    public partial class DioPOList : BasePage
    {
        private CG_POOrderService POSer = new CG_POOrderService();


        private void Show()
        {
            string sql = " 1=1 ";



            if (txtPONo.Text.Trim() != "")
            {
                if (CheckPoNO(txtPONo.Text.Trim()) == false)
                {
                    return;
                }
                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text.Trim());
            }


            if (ttxPOName.Text.Trim() != "")
            {
                sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text.Trim());
            }

            if (txtGuestName.Text.Trim() != "")
            {
                sql += string.Format(" and GuestName like '%{0}%'", txtGuestName.Text.Trim());
            }

            //sql += string.Format(" and AE='{0}'",Session["LoginName"]);

            if (ddlBusType.Text == "0")
            {
                sql += string.Format(" and (POStatue4<>'已结清' or POStatue4 is null)");
            }
            if (ddlBusType.Text == "1")
            {
                sql += string.Format(" and POStatue4='已结清'");
            }

            if (ddlUser.Text != "-1")
            {
                sql += string.Format(" and AE='{0}'", ddlUser.SelectedItem.Text);
            }
            List<CG_POOrder> cars = this.POSer.GetListArrayToQueryDio(sql);
            AspNetPager1.RecordCount = cars.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = cars;
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
                var model = e.Row.DataItem as CG_POOrder;
                if (model != null && model.POStatue4 == "已结清")
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                }
            }
        }





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Session["CGPONo"] = null;
                List<CG_POOrder> cars = new List<CG_POOrder>();
                this.gvList.DataSource = cars;
                this.gvList.DataBind();

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();

                user = userSer.getAllUserByPOList();
                user.Insert(0, new VAN_OA.Model.User() { LoginName = "全部", Id = -1 });
                ddlUser.DataSource = user;
                ddlUser.DataBind();
                ddlUser.DataTextField = "LoginName";
                ddlUser.DataValueField = "Id";
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {



        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                Session["CGPONo"] = e.CommandArgument;
                Response.Write("<script>window.close();window.opener=null;</script>");
            }
        }
    }
}
