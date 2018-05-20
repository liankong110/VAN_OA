using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;

using VAN_OA.Model.ReportForms;
using VAN_OA.Model.JXC;


namespace VAN_OA.JXC
{
    public partial class DioCommPOListNO1 : BasePage
    {
        private CG_POOrderService POSer = new CG_POOrderService();

        private bool isSell = false;
        private void Show()
        {
            string sql = " 1=1 and IFZhui=0 and Status='通过' ";



            if (txtPONo.Text.Trim() != "")
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

            if (ddlUser.Text != "-1")
            {
                sql += string.Format(" and AppName={0} ", ddlUser.Text);
            }

            //if (Request["Type"] != null)
            //{
            //    sql += string.Format(" and PONo not in (select PoNo from TB_ToInvoice where State<>'不通过' )");
            //}

            //if (Request["CG_Order"] != null)
            //{
            //    sql += string.Format(" and AppName={0} ", Session["currentUserId"]);

            //}



            //if (Request["sell"] != null)
            //{
            //    sql += string.Format(" and AppName={0} ", Session["currentUserId"]);
            //    isSell = true;
            //}
            //    List<CG_POOrder> cars = this.POSer.GetSellGoodsPoInfo(sql, Convert.ToInt32(Session["currentUserId"]));
            //    this.gvList.DataSource = cars;
            //    this.gvList.DataBind();

            //}
            //else
            //{
            List<CG_POOrder> cars = this.POSer.GetListArray(sql);
            AspNetPager1.RecordCount = cars.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = cars;
            this.gvList.DataBind();
            //}



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
                //if (isSell)

                //{
                //    CG_POOrder model = e.Row.DataItem as CG_POOrder;
                //    if (model.POStatue2 == CG_POOrder.ConPOStatue2)
                //        e.Row.BackColor = System.Drawing.Color.Red;
                //}
            }
        }





        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Session["Comm_CGPONo"] = null;
                List<CG_POOrder> cars = new List<CG_POOrder>();
                this.gvList.DataSource = cars;
                this.gvList.DataBind();

                List<VAN_OA.Model.User> user = new List<VAN_OA.Model.User>();
                VAN_OA.Dal.SysUserService userSer = new VAN_OA.Dal.SysUserService();

                user = userSer.getAllUserByLoginName("");
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
                Session["Comm_CGPONo"] = e.CommandArgument;
                Response.Write("<script>window.close();window.opener=null;</script>");
            }
        }
    }
}
