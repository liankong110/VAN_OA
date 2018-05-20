using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace VAN_OA
{
    public partial class WFUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            Response.Clear(); //清除所有之前生成的Response内容
           Response.Write(PicUpload("Excel"));
            Response.End(); //停止Response后续写入动作，保证Response内只有我们写入内容
        }

        
        /// <summary>
        /// 上传用户头像
        /// </summary>
        /// <param name="folderName">对应的文件夹</param>
        /// <returns></returns>
        public string PicUpload(string folderName)
        {
            const string defaulturl = "/Content/images/nopic.jpg";
            var urlpath = string.Format(@"/Attachment/{0}/{1}/", folderName, DateTime.Now.ToString("yyyyMM"));
            var dirpath = Server.MapPath(urlpath);
            if (!Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }
            var postedFile = System.Web.HttpContext.Current.Request.Files["Filedata"];
            if (postedFile == null)
            {
                return defaulturl;
            }
            var filename = GetFileName(postedFile.FileName.ToLower());
            postedFile.SaveAs(dirpath + filename);
            return urlpath + filename;
        }

        private static string GetFileName(string filename)
        {
            var random = new Random();
            var extension = ".jpg";
            if (filename.Contains("."))
            {
                extension = filename.Substring(filename.IndexOf('.')).Length > 0
                                ? filename.Substring(filename.IndexOf('.'))
                                : ".jpg";
            }
            return DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond + random.Next(10, 99) + extension;
        }

    }
}