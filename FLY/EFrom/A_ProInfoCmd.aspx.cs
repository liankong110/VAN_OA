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
using VAN_OA.Model.EFrom;
using VAN_OA.Dal.EFrom;

namespace VAN_OA.EFrom
{
    public partial class A_ProInfoCmd : System.Web.UI.Page
    {
        protected string Display = "block";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                A_RoleService roleSer = new A_RoleService();
                List<A_Role> roles = roleSer.GetModelList("");
                roles.Insert(0, new A_Role());
                this.ddlRoles.DataSource = roles;
                this.ddlRoles.DataBind();
                ddlRoles.DataTextField = "A_RoleName";
                ddlRoles.DataValueField = "A_RoleId";


             
                lblPareId.Visible = false;
                lblParent.Text = "&nbsp";
             
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                IniData();

               
            }
            if (Request["isScan"] != null)
            {
                Display = "none";
            }
        }

        private void IniData()
        {
            tvMain.Nodes.Clear();
            TreeNode trNo = new TreeNode("↓  开始");
            trNo.ImageUrl = "~/Image/per.ico";
            trNo.Value = "0";
            tvMain.Nodes.Add(trNo);
            List<A_ProInfos> getAllPros = new List<A_ProInfos>();
            A_ProInfosService prosSer=new A_ProInfosService ();
            if (Request["proId"] != null)
            {
                getAllPros = prosSer.GetListArray(string.Format(" pro_Id={0}", Request["proId"]));
                object obj = DBHelp.ExeScalar("select pro_Type from A_ProInfo where pro_Id=" + Request["proId"]);
                lblName.Text = obj.ToString();
            }

            for (int i = 0; i < getAllPros.Count; i++)
            {
                TreeNode maintrNo = new TreeNode("↓  "+getAllPros[i].RoleName);
                maintrNo.ImageUrl = "~/Image/per.ico";
                maintrNo.Value = getAllPros[i].ids.ToString();
                tvMain.Nodes.Add(maintrNo);
            }


            TreeNode trNoLast = new TreeNode("结束");
            trNoLast.ImageUrl = "~/Image/per.ico";
            trNoLast.Value ="-1";
            tvMain.Nodes.Add(trNoLast);
     

            tvMain.ExpandAll();
        }

     



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (tvMain.SelectedNode == null )
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请选择一个上级审批角色名称进行添加！');"), true);

                return;
            }

            if (tvMain.SelectedNode.Value == "0")
            {
                lblParent.Text = tvMain.SelectedNode.Text;
                lblPareId.Text = tvMain.SelectedNode.Value;
                ddlRoles.SelectedIndex = 0;
                ddlRoles.SelectedItem.Text = "";
            }
            else
            {
                if (tvMain.SelectedNode.Value != "-1")
                    ddlRoles.SelectedItem.Text = tvMain.SelectedNode.Text.Replace("↓  ", "");
                else
                {
                    ddlRoles.SelectedIndex = 0;
                    ddlRoles.SelectedItem.Text = "";
                }
                for (int i = 0; i < tvMain.Nodes.Count; i++)
                {
                    if (tvMain.Nodes[i].Value == tvMain.SelectedNode.Value && i != 0)
                    {
                        lblParent.Text = tvMain.Nodes[i - 1].Text;
                        lblPareId.Text = tvMain.Nodes[i - 1].Value;


                        break;
                    }
                }
            }

       
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            ViewState["state"] = "add";
           
            lblMess.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            if (tvMain.SelectedNode == null || (tvMain.SelectedNode.Value == "0"||tvMain.SelectedNode.Value == "-1"))
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请选择一个上级审批角色名称进行编辑！');"), true);
                // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择一个文件夹节点进行编辑！');</script>");
                return;
            }

            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            ViewState["state"] = "update";

            //lblParent.Text = tvMain.SelectedNode.Text;
            //lblPareId.Text = tvMain.SelectedNode.Value;



            ddlRoles.SelectedItem.Text = tvMain.SelectedNode.Text.Replace("↓  ", "");
            for (int i = 0; i < tvMain.Nodes.Count; i++)
            {
                if (tvMain.Nodes[i].Value == tvMain.SelectedNode.Value && i != 0)
                {
                    lblParent.Text = tvMain.Nodes[i - 1].Text;
                    lblPareId.Text = tvMain.Nodes[i - 1].Value;


                    break;
                }
            }


            ViewState["proId"] = tvMain.SelectedNode.Value;
            lblMess.Text = "";

        }

        protected void tvMain_SelectedNodeChanged(object sender, EventArgs e)
        {
                lblMess.Text = "";
                if (ViewState["state"] != null && ViewState["state"].ToString() == "add")
                {
                    //if (tvMain.SelectedNode.Value == "0" && tvMain.SelectedNode.Text=="结束")
                    //{
                    //    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('该节点无法编辑！');"), true);
                    //    return;
                    //}


                }
                else
                {
                    if (ViewState["state"] != null && ViewState["state"].ToString() == "update")
                    {
                        if (tvMain.SelectedNode.Value == "0" || tvMain.SelectedNode.Value == "-1")
                        {
                            ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('该节点无法编辑！');"), true);
                            return;
                        }
                    }



                }


                if (tvMain.SelectedNode.Value == "0")
                {
                    lblParent.Text = tvMain.SelectedNode.Text;
                    lblPareId.Text = tvMain.SelectedNode.Value;
                    ddlRoles.SelectedIndex = 0;
                    ddlRoles.SelectedItem.Text = "";
                }
                else
                {
                    if (tvMain.SelectedNode.Value != "-1")
                        ddlRoles.SelectedItem.Text = tvMain.SelectedNode.Text.Replace("↓  ", "");
                    else
                    {
                        ddlRoles.SelectedIndex = 0;
                        ddlRoles.SelectedItem.Text = "";

                    }
                    for (int i = 0; i < tvMain.Nodes.Count; i++)
                    {
                        if (tvMain.Nodes[i].Value == tvMain.SelectedNode.Value && i != 0)
                        {
                            lblParent.Text = tvMain.Nodes[i - 1].Text;
                            lblPareId.Text = tvMain.Nodes[i - 1].Value;
                            break;
                        }
                    }
                }

                ViewState["proId"] = tvMain.SelectedNode.Value;
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblPareId.Text.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请选择一个上级审批角色名称！');"), true);

                return;
            }

            if (ddlRoles.SelectedItem.Text == "")
            { 
                 

                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请选择审批角色名称！');"), true);

                return;
            }

            A_ProInfosService prosSer = new A_ProInfosService();




            A_ProInfos prosModel = new A_ProInfos();
            prosModel.a_Role_Id = Convert.ToInt32(ddlRoles.SelectedItem.Value);
            prosModel.pro_Id = Convert.ToInt32(Request["proId"]);
            prosModel.a_Index = 0;

            if (ViewState["state"].ToString() == "add")
            {
               


                List<A_ProInfos> getAllPros = new List<A_ProInfos>();
                if (Request["proId"] != null)
                {
                    getAllPros = prosSer.GetListArray(string.Format(" pro_Id={0}", Request["proId"]));

                }

                if (getAllPros.Count > 0)
                {

                    if (Convert.ToInt32(lblPareId.Text) == 0)
                    {
                        getAllPros.Insert(0, prosModel);
                    }
                    else
                    {
                        for (int i = 0; i < getAllPros.Count; i++)
                        {
                            if (getAllPros[i].ids == Convert.ToInt32(lblPareId.Text))
                            {

                                getAllPros.Insert(i + 1, prosModel);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    getAllPros.Insert(0, prosModel);
                }
                int ids=prosSer.addList(getAllPros);
                if (ids <= 0)
                {

                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('添加失败！');"), true);

                    return;
                }
                else
                {
                    lblPareId.Text = ids.ToString();
                    lblParent.Text = "↓  "+ddlRoles.SelectedItem.Text;
                }
              
            }
            if (ViewState["state"].ToString() == "update")
            {


                string sql = string.Format(@"select count(*) from tb_EForm where toProsId=" + Convert.ToInt32(ViewState["proId"]));
                if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
                {
                    ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('修改失败,该信息正在被使用！');"), true);
                   // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改失败,该信息正在被使用！');</script>");
                    return;
                }


                 sql = string.Format(" update A_ProInfos set a_Role_Id={0} where ids={1}", ddlRoles.SelectedItem.Value, ViewState["proId"]);
                DBHelp.ExeCommand(sql);

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
            if (tvMain.SelectedNode == null || (tvMain.SelectedNode.Value == "0" || tvMain.SelectedNode.Value == "-1"))
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('请选择一个上级审批角色名称进行删除！');"), true);
                // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请选择一个文件夹节点进行编辑！');</script>");
                return;
            }


            string sql = string.Format(@"select count(*) from tb_EForm where toProsId=" + Convert.ToInt32(ViewState["proId"]));
            if (Convert.ToInt32(DBHelp.ExeScalar(sql)) > 0)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "a", string.Format("alert('删除失败,该信息正在被使用！');"), true);
               // base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('删除失败,该信息正在被使用！');</script>");
                return;
            }


            A_ProInfosService ProSer = new A_ProInfosService();
            ProSer.deleCommand(Convert.ToInt32(ViewState["proId"]), Convert.ToInt32(Request["proId"]));
            IniData();
            
        }
    }
}
