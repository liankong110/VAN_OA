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
using VAN_OA.Dal.OA;
using VAN_OA.Model.OA;
using System.Collections.Generic;
using System.IO;

using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Office;



namespace VAN_OA
{
    public partial class preview : System.Web.UI.Page
    {
        /// <summary> 
        /// word转成html 
        /// </summary> 
        /// <param name="wordFileName"></param> 
        private string WordToHtml(object wordFileName, string htmlFileName)
        {
            object saveFileName = "";
            try
            {
               

                //在此处放置用户代码以初始化页面 
                Word.ApplicationClass word = new Word.ApplicationClass();
                if (word == null)
                {
                    ServiceAppSetting.LoggerHander.Invoke("word is null", "Error");
                    
                    return "";
                }
                Type wordType = word.GetType();
                Word.Documents docs = word.Documents;

                if (docs == null)
                {
                    ServiceAppSetting.LoggerHander.Invoke("docs is null", "Error"); 
                    return "";
                }
                //打开文件 
                Type docsType = docs.GetType();
                Word.Document doc = (Word.Document)docsType.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { wordFileName, true, true });

                if (doc == null)
                {
                    ServiceAppSetting.LoggerHander.Invoke("doc is null", "Error"); 
                    return "";
                }
                //转换格式，另存为 
                Type docType = doc.GetType();
                string wordSaveFileName = wordFileName.ToString();


                string strSaveFileName = htmlFileName;
                saveFileName = (object)strSaveFileName;
                docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { saveFileName, Word.WdSaveFormat.wdFormatFilteredHTML });
                docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod, null, doc, null);
                //退出 Word 
                wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, word, null);
            }
            catch (Exception ex)
            {
                ServiceAppSetting.LoggerHander.Invoke(string.Format("<script>alert('{0}！');</script>", ex.Message), "Error"); 
            }

            return saveFileName.ToString();
        }

        protected void ExcelConvertToHtml(string xlsPath, string htmlPath)
        {


            Excel.Application app = new Excel.Application();
            app.Visible = false;
            Object o = Missing.Value;

            /**/
            /// _Workbook xls=app.Workbooks.Open(xlsPath,ref o,ref o,ref o,ref o,ref o,ref o,ref o,ref o,ref o,ref o,ref o,ref o,ref o,ref o); 

            Excel.Workbook xls = app.Workbooks.Open(xlsPath, Missing.Value, Missing.Value, Missing.Value,
           Missing.Value, Missing.Value, Missing.Value, Missing.Value,
           Missing.Value, Missing.Value, Missing.Value, Missing.Value,
           Missing.Value, Missing.Value, Missing.Value);
            object fileName = htmlPath;
            object format = Excel.XlFileFormat.xlHtml;//Html 

            // xls.SaveAs(ref fileName,ref format,ref o,ref o,ref o,ref o,ref o,ref o,ref o,ref o,ref o,ref o); 
            //xls.SaveAs(fileName, format, o, o, o, o, Excel.XlSaveAsAccessMode.xlExclusive, o, o, o, o);
            xls.SaveAs(fileName, format, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            //xls.SaveAs(
            object t = true;
            app.Quit();

            Process[] myProcesses = Process.GetProcessesByName("EXCEL");
            foreach (Process myProcess in myProcesses)
            {
                myProcess.Kill();
            }


        }
        protected string url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                // string url="<script>window.open('55.htm', '','fullscreen=yes')</script>";

                // Response.Write(url);
                //if (1 == 1)
                //{
                //    return;
                //}

                if (Request["id"] != null)
                {
                    tb_AttachmentService attSer = new tb_AttachmentService();
                    tb_Attachment atts = attSer.GetListArrayByParentId_Pre(Request["id"]);
                    if (atts != null)
                    {

                        string qizhui = System.Web.HttpContext.Current.Request.MapPath("Attachment/") + Request["id"];

                        string fileExtension = atts.fileName.Substring(atts.fileName.LastIndexOf(".") + 1).ToLower();




                        if (fileExtension == "doc" || fileExtension == "docx")
                        {
                            if (File.Exists(qizhui + "." + "htm") == false)
                            {
                                WordToHtml(qizhui + "." + fileExtension, qizhui + ".htm");
                            }
                            // url = "/Attachment/" + Request["id"] + ".htm";
                            Response.Redirect("~/Attachment/" + Request["id"] + ".htm");
                        }

                        else if (fileExtension == "xlsx" || fileExtension == "xls")
                        {
                            if (File.Exists(qizhui + "." + "htm") == false)
                            {
                                ExcelConvertToHtml(qizhui + "." + fileExtension, qizhui + ".htm");
                            }
                            //  url = "/Attachment/" + Request["id"] + ".htm";
                            Response.Redirect("~/Attachment/" + Request["id"] + ".htm");
                        }
                        else
                        {

                            Response.Redirect("~/Attachment/" + Request["id"] + "." + fileExtension);


                            //Response.ContentType = atts.FileType;
                            //Response.BinaryWrite((byte[])atts.fileNo);
                            //Response.Write(atts.fileName.ToString());
                        }


                    }

                    // Response.Write("<script>window.close();window.opener=null;</script>"); 
                }
            }

        }
    }
}
