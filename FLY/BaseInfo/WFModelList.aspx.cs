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
    public partial class WFModelList : BasePage
    {

        TB_ModelService modelService = new TB_ModelService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFModel.aspx");
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
            if (txtName.Text != "")
            {
                sql += string.Format("  ModelName like '%{0}%'", txtName.Text);
            }
            List<TB_Model> list = this.modelService.GetListArray(sql);
            AspNetPager1.RecordCount = list.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;

            this.gvList.DataSource = list;
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
            //判断项目有没有使用
            this.modelService.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
            Show();
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/BaseInfo/WFModel.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<TB_Model> list = new List<TB_Model>();
                this.gvList.DataSource = list;
                this.gvList.DataBind();
            }
        }
    }
}
