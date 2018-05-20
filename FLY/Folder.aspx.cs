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

namespace VAN_OA
{
    public partial class Folder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblSonId.Visible = false;
                lblPareId.Visible = false;
                lblParent.Text = "&nbsp";
                txtFolder.Text = "";
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                IniData();
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

            tvMain.ExpandAll();
        }

        private void showFolder(List<tb_Folder> allFolder,TreeNode mainNode)
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
            if (tvMain.SelectedNode == null || tvMain.SelectedNode.Depth <= 0)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请选择一个上级文件夹节点进行添加！');"), true);
                // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择一个文件夹节点进行编辑！');</script>");
                return;
            }

            if (tvMain.SelectedNode.Depth == 0)
            {
                lblParent.Text = tvMain.SelectedNode.Text;
                lblPareId.Text = tvMain.SelectedNode.Target;

            }
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            ViewState["state"] = "add";
            txtFolder.Text = "";
            lblMess.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            if (tvMain.SelectedNode==null||tvMain.SelectedNode.Depth <= 0)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请选择一个文件夹节点进行编辑！');"), true);
               // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择一个文件夹节点进行编辑！');</script>");
                return;
            }

            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            ViewState["state"] = "update";

            lblParent.Text = tvMain.SelectedNode.Parent.Text;
            lblPareId.Text = tvMain.SelectedNode.Parent.Target;
            txtFolder.Text = tvMain.SelectedNode.Text;
            ViewState["folder_Id"] = tvMain.SelectedNode.Target;
            lblMess.Text = "";

        }

        protected void tvMain_SelectedNodeChanged(object sender, EventArgs e)
        {
            lblMess.Text = "";
            if (ViewState["state"] != null && ViewState["state"].ToString() == "add")
            {
                if (tvMain.SelectedNode.Parent != null)
                {
                    lblParent.Text = tvMain.SelectedNode.Parent.Text;
                    lblPareId.Text = tvMain.SelectedNode.Parent.Target;

                    if (tvMain.SelectedNode.Depth == 0)
                    {
                        lblParent.Text = tvMain.SelectedNode.Text;
                        lblPareId.Text = tvMain.SelectedNode.Target;

                    }
                   
                        lblParent.Text = tvMain.SelectedNode.Text;
                        lblPareId.Text = tvMain.SelectedNode.Target;
                   
                }
                else
                {
                    lblParent.Text = "内部文件";
                    lblPareId.Text = "0";
                }
               
                //txtFolder.Text = tvMain.SelectedNode.Text;
            }
            else
            {
                if (ViewState["state"] != null && ViewState["state"].ToString() == "update")
                {
                    if (tvMain.SelectedNode.Depth == 0)
                    {
                        ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('改文件夹节点无法编辑！');"), true);
                        return;
                    }
                }
                if (tvMain.SelectedNode.Parent != null)
                {
                    lblParent.Text = tvMain.SelectedNode.Parent.Text;
                    lblPareId.Text = tvMain.SelectedNode.Parent.Target;


                }
                else
                {
                    lblParent.Text ="";
                    lblPareId.Text = "0";
                }
                txtFolder.Text = tvMain.SelectedNode.Text;
                ViewState["folder_Id"] = tvMain.SelectedNode.Target;
            }
            if (tvMain.SelectedNode.Depth != 0)
            {

                lblSonId.Text = tvMain.SelectedNode.Target;
            }
            else
            {
                lblSonId.Text = "";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblPareId.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请选择一个上级文件夹节点！');"), true);
               
                return;
            }

            if (txtFolder.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请填写文件夹名称！');"), true);
              
                return;
            }

            tb_Folder folder = new tb_Folder();
            folder.Folder_NAME = txtFolder.Text;
            folder.ParentId =Convert.ToInt32( lblPareId.Text);

            tb_FolderService folderSer = new tb_FolderService();
          
            if (ViewState["state"].ToString() == "add")
            {
             
                folderSer.Add(folder);
                txtFolder.Text = "";
                txtFolder.Focus();
            }
            if (ViewState["state"].ToString() == "update")
            {
                folder.Folder_ID = Convert.ToInt32(ViewState["folder_Id"]);
                folderSer.Update(folder);
            }
            IniData();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            ViewState["state"] = null;
            ViewState["folder_Id"] = null;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (tvMain.SelectedNode == null || tvMain.SelectedNode.Depth <= 0)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请选择一个文件夹节点进行删除！');"), true);              
                return;
            }
            if (tvMain.SelectedNode.Depth == 0)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('该文件夹节点无法删除！');"), true);
                return;
            }

            string sql = "Folder_ID in (";
            sql += string.Format("{0},",tvMain.SelectedNode.Target);
            for (int i = 0; i < tvMain.SelectedNode.ChildNodes.Count; i++)
            {
                sql += string.Format("{0},", tvMain.SelectedNode.ChildNodes[i].Target);
            }

            sql = sql.Substring(0, sql.Length-1);
            sql += ")";
            lblMess.Text = "";
            //检测文件夹中是否有文件
            string checkF = string.Format("select mainName,Folder_NAME from tb_Attachment left join tb_Folder on tb_Folder.Folder_ID=tb_Attachment.folder_Id where tb_Attachment." + sql);
            DataTable dt = DBHelp.getDataTable(checkF);
            if (dt.Rows.Count > 0)
            {
                string message = "<br/>提示：<br/>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (tvMain.SelectedNode.Text == dt.Rows[i]["Folder_NAME"].ToString())
                    {
                        message += string.Format("文件夹：{1},存在文件：{0}<br/>", dt.Rows[i]["mainName"].ToString(), dt.Rows[i]["Folder_NAME"].ToString());
                    }
                    else
                    {
                        message += string.Format("子文件夹：{1},存在文件：{0}<br/>", dt.Rows[i]["mainName"].ToString(), dt.Rows[i]["Folder_NAME"].ToString());
                    }
                }
                message += string.Format("上述原因导致改文件夹（{0}）不能被删除",tvMain.SelectedNode.Text);
                lblMess.Text = message;
                //  ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('{0}！');",message), true);
                return;
            }
            else
            {
                string delete = "delete from tb_Folder where " + sql;
                DBHelp.ExeCommand(delete);

                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('删除成功！');"), true);
                lblSonId.Visible = false;
                lblPareId.Visible = false;
                lblParent.Text = "&nbsp";
                txtFolder.Text = "";
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                IniData();
            }
        }
    }
}
