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
using System.IO;
using Microsoft.Office.Interop.Word;

namespace VAN_OA.ReportForms
{
    public partial class WFQuotePriceList : BasePage
    {

        private tb_QuotePriceService qpSer = new tb_QuotePriceService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["POUrl"] = "~/ReportForms/WFQuotePriceList.aspx";
            base.Response.Redirect("~/EFrom/WFQuotePrice.aspx");
        }


        private void Show()
        {
            string sql = " 1=1 ";


            if (txtFrom.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtFrom.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('报价日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and QuoteDate>='{0} 00:00:00'", txtFrom.Text);
            }

            if (txtTo.Text != "")
            {
                if (CommHelp.VerifesToDateTime(txtTo.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('报价日期 格式错误！');</script>");
                    return;
                }
                sql += string.Format(" and QuoteDate<='{0} 23:59:59'", txtTo.Text);
            }
            if (txtQPNo.Text != "")
            {
                sql += string.Format(" and QuoteNo like '%{0}%'", txtQPNo.Text);
            }

            if (ddlGuestName.Text != "" && ddlGuestName.Text != "0")
            {
                sql += string.Format(" and GuestName='{0}'", ddlGuestName.SelectedValue);
            }


            List<tb_QuotePrice> pos = this.qpSer.GetListArray(sql);

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

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString());
            this.qpSer.Delete(id);
            tb_QuotePrice_InvsService invSer = new tb_QuotePrice_InvsService();
            invSer.DeleteById(id);

            tb_QuotePrice_InvDetailsService invDetailSer = new tb_QuotePrice_InvDetailsService();
            invDetailSer.DeleteById(id);

            Label lblQuoteNo = gvList.Rows[e.RowIndex].FindControl("lblQuoteNo") as Label;
            if (lblQuoteNo != null)
            {
                string url = System.Web.HttpContext.Current.Request.MapPath("PDFConverter/") + lblQuoteNo.Text + ".pdf";
                if (System.IO.File.Exists(url))
                {
                    try
                    {
                        System.IO.File.Delete(url);
                    }
                    catch (Exception)
                    {


                    }
                }
            }
            Show();

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Session["POUrl"] = "~/ReportForms/WFQuotePriceList.aspx";
            base.Response.Redirect("~/EFrom/WFQuotePrice.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {

                #region 是否有删除功能
                if (Session["currentUserId"] != null)
                {
                    VAN_OA.Dal.TB_AdminDeleteService deleteSer = new VAN_OA.Dal.TB_AdminDeleteService();
                    if (deleteSer.CheckIsExistByUserId(Convert.ToInt32(Session["currentUserId"])) == false)
                    {

                        gvList.Columns[2].Visible = false;
                        
                    }
                }
                #endregion

                Dal.ReportForms.TB_GuestTrackService guestSer = new VAN_OA.Dal.ReportForms.TB_GuestTrackService();
                List<Model.ReportForms.TB_GuestTrack> guestList = guestSer.GetGuestList("");
                guestList.Insert(0, new VAN_OA.Model.ReportForms.TB_GuestTrack());
                ddlGuestName.DataValueField = "GuestName";
                ddlGuestName.DataTextField = "GuestName";
                ddlGuestName.DataSource = guestList;
                ddlGuestName.DataBind();

                List<tb_QuotePrice> quotePriceList = new List<tb_QuotePrice>();
                this.gvList.DataSource = quotePriceList;
                this.gvList.DataBind();
            }
        }
        private void down1(string fileName, string url)
        {
            if (System.IO.File.Exists(url) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('文件不存在！');</script>");

                return;
            }
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
        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PDF")
            {

                List<VAN_OA.Model.EFrom.tb_QuotePrice> list = qpSer.GetListArray(string.Format(" tb_QuotePrice.Id=") + e.CommandArgument);
                if (list.Count > 0)
                {

                    string url = System.Web.HttpContext.Current.Request.MapPath("PDFConverter/") + list[0].QuoteNo + ".pdf";
                    if (System.IO.File.Exists(url))
                    {
                        try
                        {
                            down1( list[0].QuoteNo + ".pdf",url);
                            return;
                        }
                        catch (Exception)
                        {


                        }
                    }
                    else
                    {
                        Session["PDFId"] = e.CommandArgument;
                        url = string.Format("WPPrintPDF.aspx");
                        Page.RegisterStartupScript("ServiceManHistoryButtonClick", "<script>window.open('WPPrintPDF.aspx');</script>");
                    }




                }

            }
        }

        private void PrintPDF(int PDFId)
        {
            string modelWordUrl = System.Web.HttpContext.Current.Request.MapPath("WordModel/") + "QP.doc";
            if (!File.Exists(modelWordUrl))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('模版不存在！');</script>");
                return;
            }

            string wordUrl = System.Web.HttpContext.Current.Request.MapPath("PDFConverter/");
            string guidUrl = wordUrl + Guid.NewGuid() + ".doc";
            File.Copy(modelWordUrl, guidUrl);


            Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();

            object oMissing = System.Reflection.Missing.Value;
            //打开模板文档，并指定doc的文档类型 
            object objTemplate = guidUrl;
            //路径一定要正确            
            object objDocType = WdDocumentType.wdTypeDocument;
            object objfalse = false;
            object objtrue = true;
            object miss = System.Reflection.Missing.Value;
            object missingValue = Type.Missing;
            object doNotSaveChanges = WdSaveOptions.wdDoNotSaveChanges;
            Microsoft.Office.Interop.Word.Document doc = null;
            try
            {
                doc = (Document)appWord.Documents.Add(ref objTemplate, ref objfalse, ref objDocType, ref objtrue);

                //获取模板中所有的书签 
                Bookmarks odf = doc.Bookmarks;

                tb_QuotePriceService QuotePriSer = new tb_QuotePriceService();
                VAN_OA.Model.EFrom.tb_QuotePrice model = QuotePriSer.GetModel(PDFId);
                Dictionary<string, string> bookMarks = new Dictionary<string, string>();
                bookMarks.Add("GuestName", model.GuestName);
                bookMarks.Add("GuestNo", model.GuestNo);
                bookMarks.Add("PayStyle", model.PayStyle);
                bookMarks.Add("QuoteDate", model.QuoteDate.ToShortDateString());
                bookMarks.Add("QuoteNo", model.QuoteNo);
                bookMarks.Add("QuoteNo1", model.QuoteNo);
                bookMarks.Add("ResultGuestName", model.ResultGuestName);
                bookMarks.Add("ResultGuestNo", model.ResultGuestNo);


                bookMarks.Add("Address", model.Address);
                bookMarks.Add("AddressTofa", model.AddressTofa);
                bookMarks.Add("AddressToInv", model.AddressToInv);
                bookMarks.Add("brandNo", model.brandNo);
                bookMarks.Add("BuessEmail", model.BuessEmail);
                bookMarks.Add("BuessName", model.BuessName);
                bookMarks.Add("ComBrand", model.ComBrand);
                bookMarks.Add("ComChuanZhen", model.ComChuanZhen);
                bookMarks.Add("ComName", model.ComName);
                bookMarks.Add("ComTel", model.ComTel);
                bookMarks.Add("ContactPerTofa", model.ContactPerTofa);
                bookMarks.Add("ContactPerToInv", model.ContactPerToInv);
                bookMarks.Add("GuestNameTofa", model.GuestNameTofa);

                bookMarks.Add("GuestNameToInv", model.GuestNameToInv);
                bookMarks.Add("InvAddress", model.InvAddress);
                bookMarks.Add("InvContactPer", model.InvContactPer);
                bookMarks.Add("InvoHeader", model.InvoHeader);
                bookMarks.Add("InvTel", model.InvTel);
                bookMarks.Add("NaShuiNo", model.NaShuiNo);

                bookMarks.Add("NaShuiPer", model.NaShuiPer);
                bookMarks.Add("telTofa", model.telTofa);
                bookMarks.Add("telToInv", model.telToInv);



                foreach (var key in bookMarks.Keys)
                {
                    object objKey = key;
                    doc.Bookmarks.get_Item(ref objKey).Range.Text = bookMarks[key];
                }

                doc.SaveAs(ref objTemplate, ref miss, ref miss, ref miss, ref miss, ref miss,
                               ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
                doc.Close(ref doNotSaveChanges, ref missingValue, ref missingValue);
                appWord.Application.Quit(ref miss, ref miss, ref miss);
                doc = null;
                appWord = null;

                lblMess.Text = "文件生成成功";
            }
            catch
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('文件不存在！');</script>");
                doc.Close(ref doNotSaveChanges, ref missingValue, ref missingValue);
                appWord.Application.Quit(ref miss, ref miss, ref miss);
                return;
            }

        }

    }
}
