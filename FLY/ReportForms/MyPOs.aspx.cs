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
    public partial class MyPOs : System.Web.UI.Page
    {
        private TB_POService poSer = new TB_POService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["POUrl"] = "~/ReportForms/MyPOs.aspx";
            base.Response.Redirect("~/ReportForms/PO.aspx");
        }


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
                sql += string.Format(" and DataTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and DataTime<='{0} 23:59:59'", txtTo.Text);
            }
            if (txtInvName.Text != "")
            {
                sql += string.Format(" and InvName like '%{0}%'", txtInvName.Text);
            }

            if (txtSeller.Text != "")
            {
                sql += string.Format(" and Seller like '%{0}%'", txtSeller.Text);
            }

            if (txtUnitName.Text != "")
            {
                sql += string.Format(" and UnitName like '%{0}%'", txtUnitName.Text);
            }

            sql += string.Format(" and CreateUser={0}", Session["currentUserId"].ToString());
            List<TB_PO> pos = this.poSer.GetListArray(sql);

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
                List<TB_PO> pos = new List<TB_PO>();
                this.gvList.DataSource = pos;
                this.gvList.DataBind();
            }
        }
    }
}
