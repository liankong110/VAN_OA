using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Office.Interop.Word;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using VAN_OA.Bll;
//using PDFConverter;

 
namespace VAN_OA.ReportForms
{
    public partial class WPPrintPDF : System.Web.UI.Page
    {
        private RemoteConverter converter;
        //private string storefolder = @"D:\PDFConverter\";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    converter = new RemoteConverter();
                    if (Request["type"] != "word")
                    {
                        try
                        {
                            //try to get the remoting-object


                            if (!converter.WordIsAvailable())
                            {
                                lblMess.Text = "Word 2007 not available on server!";
                                return;
                            }

                        }
                        catch
                        {
                            //Remoteserver not available
                            lblMess.Text = "PDFConverter is not running!";
                            return;
                        }
                    }

                    if (Session["PDFId"] != null)
                    {

                        #region 获取URL名称
                        tb_QuotePriceService QuotePriSer = new tb_QuotePriceService();
                        VAN_OA.Model.EFrom.tb_QuotePrice model = QuotePriSer.GetModel(Convert.ToInt32(Session["PDFId"]));
                        //文件名由 “苏州万邦电脑系统有限公司”（报价AE所属单位全称）+“-”+“20161018“（第一次生成的日期）+“天华网络”（客户简称）+“-”+“报价单”
                        VAN_OA.Dal.ReportForms.TB_GuestTrackService guestTrackSer = new VAN_OA.Dal.ReportForms.TB_GuestTrackService();
                        var simpGuestName = guestTrackSer.GetGuestSimpName(string.Format(" and GuestName='{0}'", model.GuestName));


                        #endregion

                        string wordUrl = System.Web.HttpContext.Current.Request.MapPath("PDFConverter/");
                       
                        string otherName = model.ComName + "-" + model.CreateTime.ToString("yyyyMMdd") + "-" + simpGuestName + "" + "-报价单"+model.QuoteNo.Substring(12);
                        string urlName = wordUrl + otherName;

                        try
                        {
                            if (File.Exists(urlName+".pdf"))
                            {
                                File.Delete(urlName + ".pdf");
                            }
                        }
                        catch (Exception)
                        {


                        }
                        try
                        {
                            if (File.Exists(urlName + ".doc"))
                            {
                                File.Delete(urlName + ".doc");
                            }
                        }
                        catch (Exception)
                        {


                        }
                        var modelPath = System.Web.HttpContext.Current.Request.MapPath("WordModel/");
                        if (model.ComName != "苏州万邦电脑系统有限公司")
                        {
                            modelPath += model.ComName + @"/";
                        }


                        string modelWordUrl = modelPath +"QP.doc";
                        if (Request["type"] != "word")
                        {
                            if (model.IsShuiYin&&model.IsGaiZhang)//QP2
                            {
                                modelWordUrl = modelPath + "QP1.doc";
                            }
                            if (model.IsGaiZhang == false && model.IsShuiYin)//QP1
                            {
                                modelWordUrl = modelPath + "QP2.doc";
                            }
                            if (model.IsGaiZhang && model.IsShuiYin == false)//QP1
                            {
                                modelWordUrl = modelPath + "QP3.doc";
                            }
                        }
                        string QPNO = "";
                        string GuidNO = "";

                        //string url = @"D:\Project\万邦OA\FLY\ReportForms\PDFConverter\201702080285-01-word\201702080285-01.doc"; //
                        string url = converter.PrintPDF(Convert.ToInt32(Session["PDFId"]), out QPNO, out GuidNO, modelWordUrl, wordUrl);
                        if (url != "" && Request["type"] != "word")
                        {
                            //convertPDF(url, QPNO, GuidNO);
                            string sourcefile = url;
                            string outputfile = url.Replace(".doc", ".pdf");
                           
                            //call the converter method
                            converter.convert(sourcefile, outputfile);       

                         
                            urlName += ".pdf";
                            if (File.Exists(outputfile))
                            {
                                File.Copy(outputfile, urlName);
                            }
                            List<string> listFJ = new List<string>();//保存附件路径
                            List<string> listFJName = new List<string>();//保存附件名字
                            listFJ.Add(outputfile);
                            listFJ.Add(urlName);
                            listFJName.Add(GuidNO + ".pdf");
                            listFJName.Add(otherName + ".pdf");

                            string time = GuidNO + "-pdf";
                            var path = Server.MapPath("PDFConverter/" + time + ".rar");

                            ZipFileMain(listFJ.ToArray(), listFJName.ToArray(), path, 9);//压缩文件
                            //DownloadFile(Server.UrlEncode(time + ".rar"), path);//下载文件
                            try
                            {
                                File.Delete(url);
                            }
                            catch (Exception)
                            {
                                
                                
                            }
                            //
                            foreach (var fj in listFJ)
                            {
                                try
                                {
                                    File.Delete(fj);//删除已下载文件
                                }
                                catch (Exception)
                                {

                                }
                            }

                            convertWord(path, listFJ);
                        }
                        else
                        {                          

                            //创建另一个文件
                            string guidUrl = wordUrl + GuidNO + ".doc";
                            urlName += ".doc";
                            if (File.Exists(guidUrl))
                            {
                                File.Copy(guidUrl, urlName, true);
                            }

                         

                            List<string> listFJ = new List<string>();//保存附件路径
                            List<string> listFJName = new List<string>();//保存附件名字
                            listFJ.Add(url);
                            listFJ.Add(urlName);
                            listFJName.Add(GuidNO + ".doc");
                            listFJName.Add(otherName + ".doc");

                            string time = GuidNO+"-word";
                            var path = Server.MapPath("PDFConverter/" + time + ".rar");

                            ZipFileMain(listFJ.ToArray(), listFJName.ToArray(),path , 9);//压缩文件
                            //DownloadFile(Server.UrlEncode(time + ".zip"), path);//下载文件

                            foreach (var fj in listFJ)
                            {
                                try
                                {
                                    File.Delete(fj);//删除已下载文件
                                }
                                catch (Exception)
                                {

                                }
                            }

                            convertWord(path, listFJ);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (converter == null)
                    {
                        lblMess.Text += "conver -null";
                    }
                    if (Session["PDFId"] == null)
                    {
                        lblMess.Text += "pdfid-null";
                    }
                    lblMess.Text += "A:" + ex.ToString()+"111";
                    ServiceAppSetting.LoggerHander.Invoke(ex.ToString(), "Error");
                }
               

            }
        }


        private void convertWord(string GuestUrl, List<string> listFJ)
        {
            System.IO.FileInfo downloadFile = new System.IO.FileInfo(GuestUrl);

            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition",
                                                   string.Format("attachment; filename={0}",
                                                                 downloadFile.Name));
            HttpContext.Current.Response.AddHeader("Content-Length", downloadFile.Length.ToString());
            HttpContext.Current.Response.WriteFile(downloadFile.FullName);
            HttpContext.Current.Response.Flush();

            HttpContext.Current.ApplicationInstance.CompleteRequest();
           
            HttpContext.Current.Response.Close();

           
        }

        //private void convertPDF(string url, string No, string GuidNO)
        //{
        //    string sourcefile = url;
        //    string outputfile = url.Replace(".doc", ".pdf");   
        //    //call the converter method
        //    converter.convert(sourcefile, outputfile);         


        //    //Send back a downloadable File
        //    System.IO.FileInfo downloadFile = new System.IO.FileInfo(outputfile);
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.ContentType = "application/pdf";
        //    HttpContext.Current.Response.AddHeader("Content-Disposition",
        //                                           string.Format("attachment; filename={0}",
        //                                                         downloadFile.Name));
        //    HttpContext.Current.Response.AddHeader("Content-Length", downloadFile.Length.ToString());
        //    HttpContext.Current.Response.WriteFile(downloadFile.FullName);
        //    HttpContext.Current.Response.End();
        //    HttpContext.Current.Response.Close();

        //    //be warned: at this point, the pdf-file is still existing in the storefolder.
        //    //as we don't know how long it takes for the user, to download the file, we can not
        //    //delete the pdf file at this point.
        //}



        //private void DownloadFile(string fileName, string filePath)
        //{
        //    FileInfo fileInfo = new FileInfo(filePath);
        //    Response.Clear();
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
        //    Response.AddHeader("Content-Length", fileInfo.Length.ToString());
        //    Response.AddHeader("Content-Transfer-Encoding", "binary");
        //    Response.ContentType = "application/octet-stream";
        //    Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        //    Response.WriteFile(fileInfo.FullName);
        //    Response.Flush();
        //    //File.Delete(filePath);//删除已下载文件
        //    Response.End();            
        //}

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileName">要压缩的所有文件（完全路径)</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="name">压缩后文件路径</param>
        /// <param name="Level">压缩级别</param>
        public void ZipFileMain(string[] filenames, string[] fileName, string name, int Level)
        {
            ZipOutputStream s = new ZipOutputStream(File.Create(name));
            Crc32 crc = new Crc32();
            //压缩级别
            s.SetLevel(Level); // 0 - store only to 9 - means best compression
            try
            {
                int m = 0;
                foreach (string file in filenames)
                {
                    //打开压缩文件
                    FileStream fs = File.OpenRead(file);//文件地址
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    //建立压缩实体
                    ZipEntry entry = new ZipEntry(fileName[m].ToString());//原文件名
                    //时间
                    entry.DateTime = DateTime.Now;
                    //空间大小
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                    m++;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                s.Finish();
                s.Close();
            }
        }

    }
}
