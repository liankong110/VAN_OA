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
using VAN_OA.Model.OA;
using VAN_OA.Dal.OA;

using System.Collections.Generic;
using VAN_OA.Bll.OA;

using System.IO;

using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Office;
namespace VAN_OA
{
    public partial class CmdAttachment : System.Web.UI.Page
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
                Type wordType = word.GetType();
                Word.Documents docs = word.Documents;
                //打开文件 
                Type docsType = docs.GetType();
                Word.Document doc = (Word.Document)docsType.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { wordFileName, true, true });
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
                
               base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", ex.Message.ToString()));
            }
            return saveFileName.ToString();
        }

        protected void ExcelConvertToHtml(string xlsPath, string htmlPath)
        {

            try
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
            catch (Exception ex)
            { 
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", ex.Message.ToString()));
                // MessageBox.Show(ex.Message);
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniData();
                lblParent.Text = "";
                lblFolderId.Visible = false;
                tb_AttachmentService attSer = new tb_AttachmentService();
                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;

                    tb_Attachment attachment = attSer.GetListArrayByParentId(Convert.ToInt32(base.Request["Id"]));
                    txtAttName.Text = attachment.MainName;
                    txtRemark.Text = attachment.Remark;
                    txtVersion.Text = attachment.version;
                    lblFolderId.Text = attachment.Folder_Id.ToString();
                    lblParent.Text = attachment.FolderName;
                    lblAttName.Text = attachment.fileName;
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }
        private void IniData()
        {
            tvMain.Nodes.Clear();
            TreeNode trNo = new TreeNode("内部文件");
            trNo.ImageUrl = "~/Image/Folder.ico";
            trNo.Target = "0";
            tb_FolderService folderSer = new tb_FolderService();
            List<tb_Folder> allFolder = folderSer.GetListArray("");
            showFolder(allFolder, trNo);
            tvMain.Nodes.Add(trNo);
            tvMain.CollapseAll();
        }
        private void showFolder(List<tb_Folder> allFolder, TreeNode mainNode)
        {

            for (int i = 0; i < allFolder.Count; i++)
            {
                if (allFolder[i].ParentId.ToString() == mainNode.Target)
                {
                    TreeNode secondeTn = new TreeNode(allFolder[i].Folder_NAME);
                    secondeTn.ImageUrl = "~/Image/Folder.ico";
                    secondeTn.Target = allFolder[i].Folder_ID.ToString();
                    mainNode.ChildNodes.Add(secondeTn);

                    showFolder(allFolder, secondeTn);
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAttName.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写文件名称！');</script>");
                    return;
                }

                if (lblParent.Text == "" || lblParent.Text == "内部文件")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择一个文件夹！');</script>");
                    return;
                }
                HttpFileCollection files = HttpContext.Current.Request.Files;

                if (files.Count <= 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择一个文件！');</script>");
                    return;
                }




                tb_Attachment att = new tb_Attachment();
                att.createTime = DateTime.Now;
                att.MainName = txtAttName.Text;
                att.Folder_Id = Convert.ToInt32(lblFolderId.Text);
                att.Remark = txtRemark.Text;
                att.userName = Session["LoginName"].ToString();
                att.version = txtVersion.Text;
                string fileName, fileExtension;
                fileExtension = "";
                HttpPostedFile postedFile = null;
                for (int iFile = 0; iFile < files.Count; iFile++)
                {


                    ///'检查文件扩展名字
                    postedFile = files[iFile];

                    fileName = System.IO.Path.GetFileName(postedFile.FileName);
                    if (fileName != "")
                    {
                        att.fileName = fileName;
                        fileExtension = System.IO.Path.GetExtension(fileName);
                        string fileType = postedFile.ContentType.ToString();//文件类型
                        att.FileType = fileType;
                        System.IO.Stream streamFile = postedFile.InputStream;//建立数据流对象
                        int fileLength = postedFile.ContentLength;//文件长度以字节为单位
                        byte[] fileData = new Byte[fileLength];//新建一个数组
                        streamFile.Read(fileData, 0, fileLength);//将这个数据流读取到数组中
                        //att.fileNo = fileData;



                    }
                }

                tb_AttachmentManager attMan = new tb_AttachmentManager();
                int id = attMan.AddSomAttachments(att);
                if (id > 0)
                {
                   // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                    if ( fileExtension != "")
                    {
                        string qizhui = System.Web.HttpContext.Current.Request.MapPath("Attachment/") + id;
                        postedFile.SaveAs(qizhui + fileExtension);

                        fileExtension = fileExtension.Substring(1, fileExtension.Length - 1).ToLower();

                        if (fileExtension == "doc" || fileExtension == "docx")
                        {
                            WordToHtml(qizhui + "." + fileExtension, qizhui + ".htm");
                            //ToHtml.HowToHtml h = new ToHtml.HowToHtml();
                            //h.WordToHtml(qizhui + "." + fileExtension, qizhui + ".htm");

                        }

                        else if (fileExtension == "xlsx" || fileExtension == "xls")
                        {
                            ExcelConvertToHtml(qizhui + "." + fileExtension, qizhui + ".htm");
                            //ToHtml.HowToHtml h = new ToHtml.HowToHtml();
                            //h.ExcelConvertToHtml(qizhui + "." + fileExtension, qizhui + ".htm");
                        }


                    }
                    Response.Redirect("~/CmdAttachment.aspx");
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加失败！');</script>");
                    // Response.Redirect("~/WfAttachmentList.aspx");
                }
            }
            catch (Exception EX)
            {
                 base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>",EX.Message.ToString()));
               
            }

            


          
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAttName.Text.Trim() == "")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写文件名称！');</script>");
                    return;
                }

                if (lblParent.Text == "" || lblParent.Text == "内部文件")
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择一个文件！');</script>");
                    return;
                }
                tb_Attachment att = new tb_Attachment();
                att.createTime = DateTime.Now;
                att.MainName = txtAttName.Text;
                att.Folder_Id = Convert.ToInt32(lblFolderId.Text);
                att.Remark = txtRemark.Text;
                att.userName = Session["LoginName"].ToString();
                att.version = txtVersion.Text;

                att.id = Convert.ToInt32(base.Request["Id"]);
                HttpFileCollection files = HttpContext.Current.Request.Files;

                string fileName, fileExtension;
                fileExtension = "";
                HttpPostedFile postedFile = null;
                for (int iFile = 0; iFile < files.Count; iFile++)
                {


                    ///'检查文件扩展名字
                    postedFile = files[iFile];
                    //string fileName, fileExtension;
                    fileName = System.IO.Path.GetFileName(postedFile.FileName);
                    if (fileName != "")
                    {
                        att.fileName = fileName;
                        fileExtension = System.IO.Path.GetExtension(fileName);
                        string fileType = postedFile.ContentType.ToString();//文件类型
                        att.FileType = fileType;
                        System.IO.Stream streamFile = postedFile.InputStream;//建立数据流对象
                        int fileLength = postedFile.ContentLength;//文件长度以字节为单位
                        byte[] fileData = new Byte[fileLength];//新建一个数组
                        streamFile.Read(fileData, 0, fileLength);//将这个数据流读取到数组中
                        att.fileNo = fileData;



                    }
                }

                tb_AttachmentManager attMan = new tb_AttachmentManager();
                if (attMan.UpdateSomAttachments(att))
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存成功！');</script>");
                    if (att.fileNo != null && fileExtension != "")
                    {

                        if (lblAttName.Text != "")
                        {
                            string attName = System.Web.HttpContext.Current.Request.MapPath("Attachment/") + att.id + lblAttName.Text.Substring(lblAttName.Text.LastIndexOf("."));
                            string ban = System.Web.HttpContext.Current.Request.MapPath("Attachment/") + att.id;
                            if (File.Exists(attName))
                            {
                                try
                                {
                                    File.Delete(attName);
                                    File.Delete(ban + ".htm");
                                    File.Delete(ban + ".files");
                                }
                                catch (Exception)
                                {


                                }
                            }

                        }


                        string qizhui = System.Web.HttpContext.Current.Request.MapPath("Attachment/") + att.id;
                        postedFile.SaveAs(qizhui + fileExtension);
                        fileExtension = fileExtension.Substring(1, fileExtension.Length - 1).ToLower();

                        if (fileExtension == "doc" || fileExtension == "docx")
                        {

                            WordToHtml(qizhui + "." + fileExtension, qizhui + ".htm");
                        }

                        else if (fileExtension == "xlsx" || fileExtension == "xls")
                        {

                            ExcelConvertToHtml(qizhui + "." + fileExtension, qizhui + ".htm");
                        }


                    }
                    Response.Redirect("~/WfAttachmentList.aspx");
                }
                else
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('保存失败！');</script>");
                    // Response.Redirect("~/WfAttachmentList.aspx");
                }
            }
            catch (Exception EX)
            {
                
                 base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>",EX.Message.ToString()));
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WfAttachmentList.aspx");
        }

        protected void tvMain_SelectedNodeChanged(object sender, EventArgs e)
        {
            lblFolderId.Text= tvMain.SelectedNode.Target;
            lblParent.Text = tvMain.SelectedNode.Text;
        }
    }
}
