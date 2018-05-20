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
    public partial class DioPOOrderInHouse : BasePage
    {
        private CAI_OrderInHouseService POSer = new CAI_OrderInHouseService();


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






            List<CAI_OrderInHouse> cars = this.POSer.GetListArrayCai_POOrderInHouse_Cai_POOrderOutHouse_View(sql);
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
                Session["DioPOOrderInHouse"] = null;
                List<CAI_OrderInHouse> cars = new List<CAI_OrderInHouse>();
                this.gvList.DataSource = cars;
                this.gvList.DataBind();
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {



        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {


            CAI_OrderInHouse model = new CAI_OrderInHouse();
            Label Id = gvList.Rows[e.NewEditIndex].FindControl("Id") as Label;
            if (Id != null)
            {
                model.Id = Convert.ToInt32(Id.Text);

            }

            Label HouseID = gvList.Rows[e.NewEditIndex].FindControl("HouseID") as Label;
            if (HouseID != null)
            {
                model.HouseID = Convert.ToInt32(HouseID.Text);
            }

            Label lblPOName = gvList.Rows[e.NewEditIndex].FindControl("POName") as Label;
            if (lblPOName != null)
            {
                model.POName = lblPOName.Text;
            }

            Label PONo = gvList.Rows[e.NewEditIndex].FindControl("PONo") as Label;
            if (PONo != null)
            {
                model.PONo = PONo.Text;
            }


            Label Supplier = gvList.Rows[e.NewEditIndex].FindControl("Supplier") as Label;
            if (Supplier != null)
            {
                model.Supplier = Supplier.Text;
            }

            Label ProNo = gvList.Rows[e.NewEditIndex].FindControl("ProNo") as Label;
            if (ProNo != null)
            {
                model.ProNo = ProNo.Text;
            }

            Label DoPer = gvList.Rows[e.NewEditIndex].FindControl("DoPer") as Label;
            if (ProNo != null)
            {
                model.DoPer = DoPer.Text;
            }
            Session["DioPOOrderInHouse"] = model;
            Response.Write("<script>window.close();window.opener=null;</script>");
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
    }
}
