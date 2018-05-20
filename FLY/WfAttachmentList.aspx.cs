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
namespace VAN_OA
{
    public partial class WfAttachmentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tb_AttachmentService attSer = new tb_AttachmentService();
                List<tb_Attachment> atts = attSer.GetListArray("");
                gvList.DataSource = atts;
                gvList.DataBind();


                tb_FolderService folderSer = new tb_FolderService();
                List<tb_Folder> allFolder = folderSer.GetListArray("");
                allFolder.Insert(0, new tb_Folder());
                ddlFolders.DataSource = allFolder;
                ddlFolders.DataBind();
                ddlFolders.DataTextField = "Folder_NAME";
                ddlFolders.DataValueField = "Folder_ID";

                #region 是否有删除功能
                if (Session["currentUserId"] != null)
                {
                    VAN_OA.Dal.TB_AdminDeleteService deleteSer = new VAN_OA.Dal.TB_AdminDeleteService();
                    if (deleteSer.CheckIsExistByUserId(Convert.ToInt32(Session["currentUserId"])) == false)
                    {
                        gvList.Columns[6].Visible = false;
                    }
                }
                #endregion


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {   
            Response.Redirect("~/CmdAttachment.aspx");
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            base.Response.Redirect("~/CmdAttachment.aspx?id=" + this.gvList.DataKeys[e.NewEditIndex].Value.ToString());
        }

        private void show()
        {
            try
            {
                string sql = " ";
                if (txtMainName.Text.Trim() != "")
                {
                    sql += string.Format(" and mainName like '%{0}%'", txtMainName.Text);
                }
                if (txtFrom.Text != "")
                {
                    if (CommHelp.VerifesToDateTime(txtFrom.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('创建时间 格式错误！');</script>");
                        return;
                    }
                    sql += string.Format(" and createTime>='{0} 00:00:00'", txtFrom.Text);
                }
                if (txtTo.Text != "")
                {
                    if (CommHelp.VerifesToDateTime(txtTo.Text) == false)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('创建时间 格式错误！');</script>");
                        return;
                    }
                    sql += string.Format(" and createTime<='{0} 23:59:59'", txtTo.Text);
                }

                if (ddlFolders.Text != "0")
                {
                    sql += string.Format(" and tb_Attachment.folder_Id={0}", ddlFolders.Text);
                }
                tb_AttachmentService attSer = new tb_AttachmentService();
                List<tb_Attachment> atts = attSer.GetListArray(sql);
                gvList.DataSource = atts;
                gvList.DataBind();
            }
            catch (Exception)
            {
                
               
            }
                

        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "down")
            {
                Response.Redirect("WFDownLoad.aspx?id="+e.CommandArgument);
            }
        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAF1FD',this.style.fontWeight='';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
            }
        }

        protected void gvList_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvList.PageIndex = e.NewPageIndex;
            show();
        }

        protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = this.gvList.DataKeys[e.RowIndex].Value.ToString();

            tb_AttachmentService attSer = new tb_AttachmentService();
            attSer.Delete(id.ToString());
            show();
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            show();
        }
    }
}
