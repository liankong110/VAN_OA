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
using VAN_OA.Model.ReportForms;
using System.Collections.Generic;


namespace VAN_OA.ReportForms
{
    public partial class WFDeliverGoodsRep : System.Web.UI.Page
    {
        private tb_DeliverGoodsService deliverGoodsSer = new tb_DeliverGoodsService();
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            select();
        }
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            //AspNetPager1.CurrentPageIndex = 1;
            select();

        }
        private void select()
        {
            string sql = " 1=1 ";

           
            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and dateTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('时间 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and dateTime<='{0} 23:59:59'", txtTo.Text);
            }






            if (txtSong.Text != "")
            {
                sql += string.Format(" and SongHuo like '%{0}%'", txtSong.Text);
            }
            if (txtWai.Text != "")
            {
                sql += string.Format(" and Name like '%{0}%'", txtWai.Text);
            }

            List<DeliverGoodsRep> users = this.deliverGoodsSer.GetListArrayReport(sql);

            //AspNetPager1.RecordCount = users.Count;
            //this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = users;
            this.gvList.DataBind();
        }
        protected void gvList_DataBinding(object sender, EventArgs e)
        {

        }

        //protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{

        //    gvList.PageIndex = e.NewPageIndex;

        //    List<DeliverGoodsRep> users = this.deliverGoodsSer.GetListArrayReport("");
        //    this.gvList.DataSource = users;
        //    this.gvList.DataBind();

        //}

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");


                Label lbl = e.Row.FindControl("lblM") as Label;
                if (lbl.Text.Contains('-'))
                {
                    e.Row.Font.Bold = true;
                    e.Row.Font.Size = 12;
                }
                
            }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                gvList.ShowHeader = false;

                txtFrom.Text = DateTime.Now.AddDays(-2).ToShortDateString();
                txtTo.Text = DateTime.Now.ToShortDateString();
                select();
                //List<DeliverGoodsRep> users = this.deliverGoodsSer.GetListArrayReport("");
                //this.gvList.DataSource = users;
                //this.gvList.DataBind();
            }
        }
    }
}
