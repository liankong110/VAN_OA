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


namespace VAN_OA
{
    public partial class WFDownLoad : System.Web.UI.Page
    {
        private void down1(string fileName, string url)
        {
            if (System.IO.File.Exists(url) == false)
            {
                try
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('文件不存在！');</script>");
                    return;

                }
                catch (Exception)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('文件不存在！');</script>");
                    return;
                }

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

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request["id"]!=null)
                {
                    tb_AttachmentService attSer = new tb_AttachmentService();
                    tb_Attachment atts = attSer.GetListArrayByParentId_Down( Request["id"]);
                    if (atts != null )
                    {
                        string qizhui = System.Web.HttpContext.Current.Request.MapPath("Attachment/") + Request["id"];

                        string fileExtension = atts.fileName.Substring(atts.fileName.LastIndexOf(".") + 1).ToLower();

                        string url = qizhui + "." + fileExtension;
                        down1(Request["id"]+"." + fileExtension, url);

                       

                      


                        //Response.Buffer = true;
                        //Page.Response.Clear();//清除缓冲区所有内容
                        //Page.Response.ContentType = "application/octet-stream";
                        //Page.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(atts.fileName));
                        //byte[] file;
                        //if (atts.fileNo == null)
                        //{
                        //    file = new byte[0];
                        //}
                        //else
                        //{
                        //    file = (Byte[])atts.fileNo;//读出数据
                        //}
                        //int a = file.Length;
                        //if (atts.fileNo != null)
                        //{
                        //    Response.BinaryWrite(file);
                        //}
                        //Response.Flush();
                        //Response.End();

                    }

                  // Response.Write("<script>window.close();window.opener=null;</script>"); 
                }
            }
        }
    }
}
