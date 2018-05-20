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
    public partial class WFFpTaxInfoList : BasePage
    {

        FpTaxInfoService fpTypeBaseInfoService = new FpTaxInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/BaseInfo/WFFpTaxInfo.aspx");
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
            if (txtFpType.Text != "")
            {
                sql += string.Format("  FpType like '%{0}%'", txtFpType.Text);

            }

            List<FpTaxInfo> gooQGooddList = this.fpTypeBaseInfoService.GetListArray(sql);
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

            string sql = string.Format(@"
declare @allCount int;
set @allCount=0;
select @allCount=@allCount+count(*) from FpTaxInfo where 
exists (select id from tb_FundsUse where CAIXS=TAX) and id={0};
select @allCount=@allCount+count(*) from FpTaxInfo where 
exists (select id from tb_FundsUse where RenXS=TAX) and id={0};
select @allCount=@allCount+count(*) from FpTaxInfo where 
exists (select id from tb_FundsUse where XingCaiXS=TAX) and id={0};
select @allCount=@allCount+count(*) from FpTaxInfo where 
exists (select id from tb_FundsUse where HuiXS=TAX) and id={0};
select @allCount", gvList.DataKeys[e.RowIndex].Value);
            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('信息已经被使用,无法删除！');</script>");               
                return;
            }
            this.fpTypeBaseInfoService.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));


            Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

            base.Response.Redirect("~/BaseInfo/WFFpTaxInfo.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<FpTaxInfo> goodList = new List<FpTaxInfo>();
                this.gvList.DataSource = goodList;
                this.gvList.DataBind();
            }
        }
    }
}
