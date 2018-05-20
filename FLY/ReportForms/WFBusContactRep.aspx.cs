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
    public partial class WFBusContactRep : System.Web.UI.Page
    {
        private tb_BusContactService busContactSer = new tb_BusContactService();

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
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and DateTime>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and DateTime<='{0} 23:59:59'", txtTo.Text);
            }

            if (txtAppName.Text != "")
            {
                sql += string.Format(" and Name like '%{0}%'", txtAppName.Text);
            }


            if (txtUnit.Text != "")
            {
                sql += string.Format(" and ContactUnit like '%{0}%'", txtUnit.Text);
            }

            List<BusContactRep> users = this.busContactSer.GetListArrayReport(sql);

            //AspNetPager1.RecordCount = users.Count;
            //this.gvList.PageIndex = AspNetPager1.CurrentPageIndex - 1;
            this.gvList.DataSource = users;
            this.gvList.DataBind();
        }
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            select();
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
                //List<BusContactRep> users = this.busContactSer.GetListArrayReport("");
                //this.gvList.DataSource = users;
                //this.gvList.DataBind();
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SeeFile")
            {
                //System.Web.HttpContext.Current.PhysicalApplicationPath
                string url = Server.MapPath("~") + @"EFrom\BusContact\" + e.CommandArgument;
                var a = e.CommandSource as LinkButton;
                down1(a.Text, url);
            }
        }
        private void down1(string fileName, string url)
        {
            string filePath = url;//路径

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            if (fileInfo.Exists == true)
            {
                const long ChunkSize = 1024 * 500;//100K 每次读取文件，只读取100Ｋ，这样可以缓解服务器的压力
                byte[] buffer = new byte[ChunkSize];

                Response.Clear();
                System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
                while (dataLengthToRead > 0 && Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                    Response.OutputStream.Write(buffer, 0, lengthRead);
                    Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                Response.Close();
            }


        }
    }
}
