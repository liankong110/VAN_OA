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
using VAN_OA.Bll.OA;



namespace VAN_OA
{
    public partial class DatabaseCmd : System.Web.UI.Page
    {
        private tb_FileService fileSer = new tb_FileService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    tb_File file = new tb_File();
                    file.fileName = txtFileName.Text;
                    file.fileURL = txtUrl.Text;



                    file.fileFullName =txtUrl.Text.Trim().Substring( txtUrl.Text.LastIndexOf('.')+1); ;
                    file.createPer = base.Session["LoginName"].ToString();
                    file.createTime = DateTime.Now;

                    if (this.fileSer.Add(file) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");

                        this.txtUrl.Text = "";

                        this.txtFileName.Text = "";
                        this.txtFileName.Focus();
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加失败！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("~/DatabaseList.aspx");
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            this.txtUrl.Text = "";
            this.txtFileName.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    tb_File file = new tb_File();
                    file.fileName = txtFileName.Text;
                    file.fileURL = txtUrl.Text;
                      
                    
                    file.fileFullName = txtUrl.Text.Trim().Substring(txtUrl.Text.LastIndexOf('.') + 1); ;


                    file.createPer = base.Session["LoginName"].ToString();
                    file.createTime = DateTime.Now;

                    file.id = Convert.ToInt32(base.Request["Id"]);
                    if (this.fileSer.Update(file))
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");
                    }
                    else
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改失败！');</script>");
                    }
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        public bool FormCheck()
        {

            if (this.txtFileName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写文件名称！');</script>");
                this.txtFileName.Focus();
                return false;
            }

            if (this.txtUrl.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写文件路径！');</script>");
                this.txtUrl.Focus();
                return false;
            }
            if (System.IO.File.Exists(txtUrl.Text.Trim())==false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('文件路径有无，文件不存在！');</script>");
                this.txtUrl.Focus();
                return false;
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (base.Request["Id"] != null)
                {
                    this.btnAdd.Visible = false;
                    tb_File file = fileSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    
                    this.txtFileName.Text = file.fileName;
                    this.txtUrl.Text = file.fileURL;
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }
    }
}
