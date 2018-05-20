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
    public partial class AttachmentTreeView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniData();
                UpdateLever();
            }

        }

        /// <summary>
        /// 将所有过期的 请假单 作废
        /// </summary>
        private void UpdateLever()
        {
//            string SQL = string.Format(@" update tb_EForm set state='不通过',toPer=0,toProsId=0
// where id in (select id from tb_EForm where proid in 
//(select pro_Id from A_ProInfo where pro_Type='员工请假单') 
//and state='执行中' and id not in (select e_Id from tb_EForms)
//and allE_id in (select id from tb_LeverInfo where DATEDIFF(day,getdate(),dateForm)<0))");
//            DBHelp.ExeCommand(SQL);

        }

        private void IniData()
        {
            tvMain.Nodes.Clear();
            TreeNode trNo = new TreeNode("内部文件");
            trNo.ImageUrl = "~/Image/Folder.ico";
            trNo.Target = "0";
            tb_FolderService folderSer = new tb_FolderService();
            List<tb_Folder> allFolder = folderSer.GetListArray("");


            tb_AttachmentService ATTSer = new tb_AttachmentService();
            List<tb_Attachment> atts = ATTSer.GetListArray_TV("");


            for (int i = 0; i < atts.Count; i++)
            {
                tb_Folder fole = new tb_Folder();
                fole.Type = "file";


                fole.ParentId =Convert.ToInt32( atts[i].Folder_Id);
                fole.Folder_NAME = atts[i].MainName + "(" + atts[i].createTime+ ")";
                fole.AttId1 =  atts[i].id.ToString();
                fole.Folder_ID = -1;
                allFolder.Add(fole);
               
            }
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

                    if (allFolder[i].Type != null && allFolder[i].Type == "file")
                    {
                        secondeTn.NavigateUrl = "~/preview.aspx?id=" + allFolder[i].AttId1;
                        secondeTn.ImageUrl = "~/Image/atchm.GIF";
                    }
                    else
                    {
                         secondeTn.ImageUrl = "~/Image/Folder.ico";
                    }
                    
                   
                    secondeTn.Target = allFolder[i].Folder_ID.ToString();
                    mainNode.ChildNodes.Add(secondeTn);
                    showFolder(allFolder, secondeTn);
                }
            }

         





        }

        protected void tvMain_SelectedNodeChanged(object sender, EventArgs e)
        {

        }

        protected void btnSea_Click(object sender, EventArgs e)
        {
            if (txtCon.Text.Trim() != "")
            {
                IniData();
                for (int i = 0; i < tvMain.Nodes.Count; i++)
                {
                    TreeNode fristNode = tvMain.Nodes[i];
                    if (fristNode.Text.Contains( txtCon.Text))
                    {
                        fristNode.Select();
                    }
                    searchFile(fristNode, txtCon.Text);
                }
            }
        }


        private void shoeSearchNode(TreeNode parentNode)
        {
            if (parentNode.Parent == null)
            {
                return;
            }
            TreeNode pareNode = parentNode.Parent;
            pareNode.Expanded = true;

            shoeSearchNode(pareNode);


        }
        private void searchFile(TreeNode node,string searchTxt)
        {
           
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                TreeNode  seTN= node.ChildNodes[i];
                if (seTN.Text.Contains(  searchTxt))
                {
                    seTN.Selected=true;                    
                    shoeSearchNode(seTN);
                    seTN.Text = string.Format("<div style='color:red'>{0}</div>",seTN.Text);
                    
                }
                searchFile(seTN, searchTxt);
            }
        }

    }
}
