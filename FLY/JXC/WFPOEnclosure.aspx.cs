using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.EFrom;
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.JXC;
using System.IO;

namespace VAN_OA.JXC
{
    public partial class WFPOEnclosure : BasePage
    {
         
        private void Show()
        {
            string sql = string.Format(@" select ProNo,fileName,Convert(varchar(50),ID)+'_'+fileName as File_Id from CG_POOrder where fileName is not null and poNo='{0}' and 
Status<>'不通过'",Request["PONo"]);
            System.Data.DataTable poFiles = DBHelp.getDataTable(sql);
        
            this.gvList.DataSource = poFiles;
            this.gvList.DataBind();

            sql = string.Format(@" select ProNo from CG_POOrder where poNo='{0}' and Status='通过'", Request["PONo"]);

            System.Data.DataTable poProList = DBHelp.getDataTable(sql);

            this.ddlProList.DataSource = poProList;
            this.ddlProList.DataBind();
            ddlProList.DataTextField = "ProNo";
            ddlProList.DataValueField = "ProNo";
        }
        protected void btnSelect_Click(object sender, EventArgs e)
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

                
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                var getPONO = string.Format("select AE from CG_POOrder where pono='{0}' and IFZhui=0 ", Request["PONo"]);
                var appName = Convert.ToString(DBHelp.ExeScalar(getPONO));
                if (appName != Session["LoginName"].ToString())
                {
                    plAddFile.Visible = false;
                }
                else
                {
//                    var sql = string.Format(@"select COUNT(*) from role_sys_form left join sys_Object on sys_Object.FormID=role_sys_form.sys_form_Id and sys_Object.roleId=role_sys_form.role_Id and textName='补充附件'
//where  role_Id in (select roleId from Role_User where userId={0}) and sys_form_Id in(select formID from sys_form where displayName='项目订单列表') and sys_Object.AutoID is not null", Session["currentUserId"]);
//                    if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                    if (NewShowAll_textName("项目订单列表", "补充附件")==false)
                    {
                        plAddFile.Visible = false;
                    }
                }
                Show();
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "down")
            {               
                string id= e.CommandArgument.ToString().Substring(0,e.CommandArgument.ToString().IndexOf('_'));
                string fileName = e.CommandArgument.ToString().Substring(e.CommandArgument.ToString().IndexOf('_')+1);
                string url = System.Web.HttpContext.Current.Request.MapPath("PO/") +
                  fileName.Substring(0, fileName.LastIndexOf('.')) + "_" + id + fileName.Substring(fileName.LastIndexOf('.')); ;
                down1(fileName, url);
            }
        }

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

        protected void Button1_Click(object sender, EventArgs e)
        {            
            HttpFileCollection files = HttpContext.Current.Request.Files;
            //查找是否有文件
            string fileName="",fileType="";          
            HttpPostedFile postedFile = null;
            string file = "";
            for (int iFile = 0; iFile < files.Count; iFile++)
            {
                ///'检查文件扩展名字
                postedFile = files[iFile];

                fileName = System.IO.Path.GetFileName(postedFile.FileName);
                if (fileName != "")
                {
                    //order.fileName = fileName;
                    //fileExtension = System.IO.Path.GetExtension(fileName);
                    fileType = postedFile.ContentType.ToString();//文件类型
                    //order.fileType = fileType;
                    System.IO.Stream streamFile = postedFile.InputStream;//建立数据流对象
                    //int fileLength = postedFile.ContentLength;//文件长度以字节为单位
                    //byte[] fileData = new Byte[fileLength];//新建一个数组
                    //streamFile.Read(fileData, 0, fileLength);//将这个数据流读取到数组中
                    //order.fileNo = fileData;
                    //file = System.IO.Path.GetFileNameWithoutExtension(fileName);
                }
            }

            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(fileType))
            {
                string sql = string.Format("update CG_POOrder set fileName='{0}',fileType='{1}' where prono='{2}'", fileName,fileType,ddlProList.SelectedItem.Value);
                DBHelp.ExeCommand(sql);
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('提交成功！');</script>");
            }
            Show();
        }
    }
}
