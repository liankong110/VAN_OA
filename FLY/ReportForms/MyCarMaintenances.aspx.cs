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


namespace VAN_OA.ReportForms
{
    public partial class MyCarMaintenances : BasePage
    {
        private TB_CarMaintenanceService CarSer = new TB_CarMaintenanceService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["POUrl"] = "~/ReportForms/MyCarMaintenances.aspx";
            base.Response.Redirect("~/ReportForms/CarMaintenance.aspx");
        }


        private void Show()
        {
            string sql = " 1=1 ";
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保养日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and MaintenanceTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保养日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and MaintenanceTime<='{0} 23:59:59'", txtTo.Text);
            }

            if (ddlCarNo.Text != "")
            {
                sql += string.Format(" and CardNo like '%{0}%'", ddlCarNo.Text);
            }

           
            sql += string.Format(" and CreateUser={0}", Session["currentUserId"].ToString());

            sql += string.Format(@" and TB_CarMaintenance.id in (select allE_id from tb_EForm where proId in (
select pro_Id from A_ProInfo where pro_Type='车辆保养申请表') and state='通过')");

            List<TB_CarMaintenance> cars = this.CarSer.GetListArray(sql);
            decimal total = 0;
            foreach (var model in cars)
            {

                if (model.Total != null)
                {
                    total += Convert.ToDecimal(model.Total);
                }
            }
            lbltotal.Text = total.ToString();
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

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            //this.CarSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            //Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Session["POUrl"] = "~/ReportForms/CarMaintenanceList.aspx";
            //base.Response.Redirect("~/ReportForms/MyCarMaintenances.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                //DataTable carInfos = DBHelp.getDataTable("select CarNO from TB_VAN_CarInfos");
                //ddlCarNo.DataSource = carInfos;
                //ddlCarNo.DataBind();
                //ddlCarNo.Text = "";

                //ddlOilCarNo.DataSource = carInfos;
                //ddlOilCarNo.DataBind();
                //ddlOilCarNo.Text = "";

                DataTable carInfos = DBHelp.getDataTable("select ''as CarNO union select CarNO from TB_CarInfo");
                ddlCarNo.DataSource = carInfos;
                ddlCarNo.DataBind();
                ddlCarNo.Text = "";

                ddlOilCarNo.DataSource = carInfos;
                ddlOilCarNo.DataBind();
                ddlOilCarNo.Text = "";


                List<TB_CarMaintenance> Cars = new List<TB_CarMaintenance>();
                this.gvList.DataSource = Cars;
                this.gvList.DataBind();

                List<TB_CarOilMaintenance> CarsOil = new List<TB_CarOilMaintenance>();
                this.gvOil.DataSource = CarsOil;
                this.gvOil.DataBind();

            }
        }

        private TB_CarOilMaintenanceService CarOilSer = new TB_CarOilMaintenanceService();
        protected void btnOilAdd_Click(object sender, EventArgs e)
        {
            Session["POUrl"] = "~/ReportForms/CarMaintenanceList.aspx";
            base.Response.Redirect("~/ReportForms/CarOilMaintenance.aspx");
        }


        private void ShowOil()
        {
            string sql = " 1=1 ";
            if (txtOilFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtOilFrom.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('加油日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and MaintenanceTime>='{0} 00:00:00'", txtOilFrom.Text);
            }

            if (txtOilTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtOilTo.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('加油日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and MaintenanceTime<='{0} 23:59:59'", txtOilTo.Text);
            }


            if (ddlOilCarNo.Text != "")
            {
                sql += string.Format(" and CardNo like '%{0}%'", ddlOilCarNo.Text);
            }

            if (txtFromChongZhi.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFromChongZhi.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('充值时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and ChongZhiDate>='{0} 00:00:00'", txtFromChongZhi.Text);
            }

            if (txtToChongZhi.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtToChongZhi.Text) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('充值时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and ChongZhiDate<='{0} 23:59:59'", txtToChongZhi.Text);
            }
            sql += string.Format(" and CreateUser={0}", Session["currentUserId"].ToString());
            List<TB_CarOilMaintenance> cars = this.CarOilSer.GetListArray(sql);
            this.gvOil.DataSource = cars;
            this.gvOil.DataBind();
        }
        protected void btnOilSelect_Click(object sender, EventArgs e)
        {
            ShowOil();
        }
        protected void gvOil_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

    }
}
