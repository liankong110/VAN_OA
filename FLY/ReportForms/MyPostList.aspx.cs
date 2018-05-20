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
using VAN_OA.Dal.EFrom;
using System.Collections.Generic;
using VAN_OA.Model.EFrom;

namespace VAN_OA.ReportForms
{
    public partial class MyPostList :BasePage
    {
        private tb_PostService postSer = new tb_PostService();

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
                sql += string.Format(" and AppTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and AppTime<='{0} 23:59:59'", txtTo.Text);
            }

            


            if (txtToPer.Text != "")
            {
                sql += string.Format(" and ToPer like '%{0}%'", txtToPer.Text);
            }

            if (txtWuLiu.Text != "")
            {
                sql += string.Format(" and WuliuName like '%{0}%'", txtWuLiu.Text);
            }

            if (txtProNo.Text.Trim() != "")
            {
                if (CheckProNo(txtProNo.Text) == false)
                {
                    return;
                }
                sql += string.Format(" and ProNo like '%{0}%'", txtProNo.Text.Trim());
            }
            if (txtAddress.Text != "")
            {
                sql += string.Format(" and PostAddress like '%{0}%'", txtAddress.Text);
            }

            sql += string.Format(" and AppName={0}", Session["currentUserId"].ToString());
            sql += string.Format(" and tb_Post.id in (select allE_id from tb_EForm where state='通过' and proid in (select pro_Id from A_ProInfo where pro_Type='邮寄文档快递表') )");
            List<tb_Post> cars = this.postSer.GetListArray(sql);
            decimal total = 0;
            foreach (var model in cars)
            {
                if (model.Total != null)
                {
                    total += model.Total.Value;
                }
            }

            lblTotal.Text = total.ToString();


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

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            Show();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
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
            //base.Response.Redirect("~/ReportForms/CarMaintenance.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<tb_Post> poseModels = new List<tb_Post>();
                this.gvList.DataSource = poseModels;
                this.gvList.DataBind();
            }
        }
    }
}
