using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class FINPropertyList : BasePage
    {

        FIN_PropertyService FIN_PropertyService = new FIN_PropertyService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/FINProperty.aspx");
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

        private void Show()
        {

            string sql = "";
            if (txtCostType.Text != "")
            {
                sql += string.Format("  CostType like '%{0}%'", txtCostType.Text);

            }

            if (ddlPro.Text != "-1")
            {
                sql += string.Format("  MyProperty='{0}'", ddlPro.Text);

            }

            List<FIN_Property> gooQGooddList = this.FIN_PropertyService.GetListArray(sql);
            AspNetPager1.RecordCount = gooQGooddList.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = gooQGooddList;
            this.gvList.DataBind();
        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
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

            this.FIN_PropertyService.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));


            Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/BaseInfo/FINProperty.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<FIN_Property> goodList = new List<FIN_Property>();
                this.gvList.DataSource = goodList;
                this.gvList.DataBind();
            }
        }
    }
}
