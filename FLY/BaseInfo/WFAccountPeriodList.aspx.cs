using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;


namespace VAN_OA.BaseInfo
{
    public partial class WFAccountPeriodList : BasePage
    {

        private TB_AccountPeriodService accSer = new TB_AccountPeriodService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFAccountPeriod.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            show();
        }

        private void show()
        {
            string sql = "";
            if (txtName.Text != "")
            {
                sql += string.Format("  AccountName={0}", txtName.Text);
            }
            List<TB_AccountPeriod> pers = this.accSer.GetListArray(sql);
            this.gvList.DataSource = pers;
            this.gvList.DataBind();
        }

        protected void gvList_DataBinding(object sender, EventArgs e)
        {
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            show();
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
           
            this.accSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));


            List<TB_AccountPeriod> pers = this.accSer.GetListArray("");
            this.gvList.DataSource = pers;
            this.gvList.DataBind();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/BaseInfo/WFAccountPeriod.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<TB_AccountPeriod> pers = new List<TB_AccountPeriod>();
                this.gvList.DataSource = pers;
                this.gvList.DataBind();

                

            }
        }


 

 

    }
}
