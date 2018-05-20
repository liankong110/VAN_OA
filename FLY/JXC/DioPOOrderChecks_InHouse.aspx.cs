using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;
using System.Collections.Generic;
using VAN_OA.Model.ReportForms;
using VAN_OA.Model.JXC;


namespace VAN_OA.JXC
{
    public partial class DioPOOrderChecks_InHouse : System.Web.UI.Page
    {
        private CAI_OrderChecksService POSer = new CAI_OrderChecksService();
       

        private void Show()
        {
            string sql = " 1=1 ";

            

            if (txtPONo.Text != "")
            {
                sql += string.Format(" and PONo like '%{0}%'", txtPONo.Text);
            }


            if (ttxPOName.Text != "")
            {
                sql += string.Format(" and POName like '%{0}%'", ttxPOName.Text);
            }






            List<CAI_OrderChecks> cars = this.POSer.GetListArrayCai_POOrderChecks_Cai_POOrderInHouse_View(sql);
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
            }
        }

     

       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Session["CAI_OrderInHousesNo_PoNo"] = null;
                List<CAI_OrderChecks> cars = new List<CAI_OrderChecks>();
                this.gvList.DataSource = cars;
                this.gvList.DataBind();
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {



        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CAI_OrderChecks model = new CAI_OrderChecks();
            Label lblCaiProNo = gvList.Rows[e.NewEditIndex].FindControl("CaiProNo") as Label;
            if (lblCaiProNo != null)
            {
                model.CaiProNo = lblCaiProNo.Text;

            }

            Label lblPONo = gvList.Rows[e.NewEditIndex].FindControl("PONo") as Label;
            if (lblPONo != null)
            {
                model.PONo = lblPONo.Text;
            }

            Label lblPOName = gvList.Rows[e.NewEditIndex].FindControl("POName") as Label;
            if (lblPOName != null)
            {
                model.POName = lblPOName.Text;
            }

            Label lblGuestName = gvList.Rows[e.NewEditIndex].FindControl("GuestName") as Label;
            if (lblGuestName != null)
            {
                model.GuestName = lblGuestName.Text;
            }

            Label lblSupplierName = gvList.Rows[e.NewEditIndex].FindControl("SupplierName") as Label;
            if (lblSupplierName != null)
            {
                model.SupplierName = lblSupplierName.Text;
            }
            Label CaiGouPer = gvList.Rows[e.NewEditIndex].FindControl("CaiGouPer") as Label;
            if (CaiGouPer != null)
            {
                model.CaiGouPer = CaiGouPer.Text;
            }



            
            Session["CAI_OrderInHousesNo_PoNo"] = model;
            Response.Write("<script>window.close();window.opener=null;</script>"); 
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        { 
        }
    }
}
