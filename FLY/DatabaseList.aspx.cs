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
using System.Collections.Generic;
using VAN_OA.Model.OA;

using System.IO;

namespace VAN_OA
{
    public partial class DatabaseList : BasePage
    {

        private tb_FileService fileSer = new tb_FileService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/DatabaseCmd.aspx");
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim() != ""||txtURL.Text.Trim()!="")
            {

                List<tb_File> files = this.fileSer.GetListArray(string.Format(" and fileName like '%{0}%' and  fileURL like '%{1}%'", this.txtName.Text.Trim(), txtURL.Text));
                this.gvList.DataSource = files;
                this.gvList.DataBind();
            }
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvList.PageIndex = e.NewPageIndex;
            List<tb_File> files = this.fileSer.GetListArray(string.Format(" and fileName like '%{0}%' and  fileURL like '%{1}%'", this.txtName.Text.Trim(), txtURL.Text));
            this.gvList.DataSource = files;
            this.gvList.DataBind();
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

                this.fileSer.Delete(Convert.ToInt32(this.gvList.DataKeys[e.RowIndex].Value.ToString()));
                List<tb_File> files = this.fileSer.GetListArray(string.Format(" and fileName like '%{0}%' and  fileURL like '%{1}%'", this.txtName.Text.Trim(), txtURL.Text));
                this.gvList.DataSource = files;
                this.gvList.DataBind();
           
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
           
                base.Response.Redirect("~/DatabaseCmd.aspx?Id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<tb_File> files = this.fileSer.GetListArray("");
                this.gvList.DataSource = files;
                this.gvList.DataBind();
            }
        }

        private void down1(string fileName, string url)
        {
            if (File.Exists(url) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('文件不存在！');</script>");

                return;
            }
            string filePath = url;//路径

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            if (fileInfo.Exists == true)
            {
                const long ChunkSize = 102400;//100K 每次读取文件，只读取100Ｋ，这样可以缓解服务器的压力
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
        private void downLoad(string fileName,string url)
        {

            if (File.Exists(url) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('文件不存在！');</script>");
               
                return ;
            }
            //string fileName = "asd.txt";//客户端保存的文件名
            string filePath =url;//路径
            FileInfo fileInfo = new FileInfo(filePath);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.WriteFile(fileInfo.FullName);
            Response.Flush();
            Response.End();


        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Down")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    tb_File file = fileSer.GetModel(id);
                    down1(file.fileName + "." + file.fileFullName, file.fileURL);
                }
            }
            catch (Exception)
            {
                
                 base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('下载失败！');</script>");
            }
        }
    }
}
