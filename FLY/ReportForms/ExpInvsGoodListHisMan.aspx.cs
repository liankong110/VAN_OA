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
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;

namespace VAN_OA.ReportForms
{
    public partial class ExpInvsGoodListHisMan : System.Web.UI.Page
    {


        private Tb_ExpInvsService proInvSer = new Tb_ExpInvsService();


        private void Show()
        {
            string sql = "  1=1 ";
            sql += string.Format(" and InvId={0} ", Request["GoodId"]);
            List<ExpInvInfoView> pos = this.proInvSer.GetListExpInvInfo(sql);
            AspNetPager1.RecordCount = pos.Count;
            this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = pos;
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
            gvList.PageIndex = e.NewPageIndex;
            Show();
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");

                if (e.Row.Cells[0].Text.Contains("小计"))
                {
                    //e.Row.BackColor = System.Drawing.Color.FromName("#D7E8FF");
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
         

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                Show();
            }
        }
    }
}
