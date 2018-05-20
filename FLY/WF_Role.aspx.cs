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
using VAN_OA.Dal;
using VAN_OA.Model;
using System.Collections.Generic;

namespace VAN_OA
{
    public partial class WF_Role : BasePage
    {
        public Dictionary<int, List<SysForm>> formIndexs = null;
        private RoleService roleSer = new RoleService();
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ddlRoles.SelectedItem != null)
            {
                int role = Convert.ToInt32(this.ddlRoles.SelectedItem.Value);
                List<roleSysObject> roleObjects = new List<roleSysObject>();
                List<SysObject> sysobjs = new List<SysObject>();
                for (int i = 0; i < this.TreeView1.Nodes.Count; i++)
                {
                    TreeNodeCollection secondNodeColl = this.TreeView1.Nodes[i].ChildNodes;
                    for (int j = 0; j < secondNodeColl.Count; j++)
                    {
                        TreeNode second = secondNodeColl[j];
                        if (second.Checked)
                        {
                            roleSysObject roleObj = new roleSysObject();
                            roleObj.Role.RID = role;
                            roleObj._SysObject.FromId = Convert.ToInt32(second.Value);
                            roleObjects.Add(roleObj);
                        }
                        TreeNodeCollection ThiNodeColl = second.ChildNodes;
                        for (int k = 0; k < ThiNodeColl.Count; k++)
                        {
                            TreeNode third = ThiNodeColl[k];
                            if (!third.Checked)
                            {
                                SysObject sysobj = new SysObject();
                                sysobj.FromId = Convert.ToInt32(second.Value);
                                sysobj.TxtName = third.Text;
                                sysobj.Name = Convert.ToString(third.Value);
                                sysobjs.Add(sysobj);
                            }
                        }
                    }
                }
                SysObjectService objsMan = new SysObjectService();
                if (objsMan.addObject(roleObjects, sysobjs, role))
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, base.GetType(), "click", "alert('保存成功')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, base.GetType(), "click", "alert('保存失败')", true);
                }
            }
        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.setTabPage(Convert.ToInt32(this.ddlRoles.SelectedItem.Value));
        }

        private bool getEnable(List<SysObject> objs, string Name)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                if (objs[i].Name.Trim() == Name.Trim())
                {
                    return false;
                }
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                List<Role> roles = this.roleSer.getAllRoles("");
                this.ddlRoles.DataTextField = "RoleName";
                this.ddlRoles.DataValueField = "RID";
                this.ddlRoles.DataSource = roles;
                this.ddlRoles.DataBind();
                this.TreeView1.Attributes.Add("OnClick", "OnTreeNodeChecked()");
                if (roles.Count > 0)
                {
                    this.setTabPage(roles[0].RID);
                }
            }
        }

        public void setTabPage(int selectedIndex)
        {
            this.formIndexs = new Dictionary<int, List<SysForm>>();
            this.TreeView1.Nodes.Clear();
            sysMenuService menuMan = new sysMenuService();
            List<SysObject> sysobjects = new SysObjectService().getSomeObjects(selectedIndex);
            List<SysMenu> sysMenus = menuMan.getAllSysMenus();
            sysFormService form1 = new sysFormService();
            List<AllObjs> allobjs = new AllObjsService().getAllobjs();
            List<SysForm> sysForms = form1.getAllForms_1();
            foreach (SysMenu sysm in sysMenus)
            {
                int i;
                TreeNode fristNode = new TreeNode(sysm.DisplayName);
                SysForm form = new SysForm();
                form.UpperID = sysm.MenuID;
                List<SysForm> currentForms = sysForms.FindAll(new Predicate<SysForm>(form.getForms));
                this.formIndexs.Add(sysm.MenuIndex, currentForms);
                currentForms.Sort(delegate(SysForm from1, SysForm form2)
                {
                    return from1.FormIndex.CompareTo(form2.FormIndex);
                });
                foreach (SysForm currentForm in currentForms)
                {
                    TreeNode secondNode = new TreeNode(currentForm.DisplayName);
                    secondNode.Value = currentForm.FormID.ToString();
                    fristNode.ChildNodes.Add(secondNode);
                    AllObjs all = new AllObjs();
                    all.FormId = currentForm.FormID;
                    List<AllObjs> currentObjs = allobjs.FindAll(new Predicate<AllObjs>(all.getForms));
                    i = 0;
                    while (i < currentObjs.Count)
                    {
                        TreeNode tiridNode = new TreeNode(currentObjs[i].Chinese);
                        tiridNode.Value = currentObjs[i].English;
                        SysObject oo = new SysObject();
                        oo.FromId = currentForm.FormID;
                        List<SysObject> someObjs = sysobjects.FindAll(new Predicate<SysObject>(oo.getSomeObjct));
                        tiridNode.Checked = this.getEnable(someObjs, currentObjs[i].English);
                        secondNode.ChildNodes.Add(tiridNode);
                        i++;
                    }
                }
                List<roleSysform> rsf = new roleSysFormService().getRightsByRoleId_1(selectedIndex);
                for (int j = 0; j < rsf.Count; j++)
                {
                    for (i = 0; i < fristNode.ChildNodes.Count; i++)
                    {
                        int formId = Convert.ToInt32(fristNode.ChildNodes[i].Value);
                        try
                        {
                            if (formId == rsf[j].Sysform.FormID)
                            {
                                fristNode.ChildNodes[i].Checked = true;
                                fristNode.Checked = true;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                this.TreeView1.Nodes.Add(fristNode);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
        }

        protected void TreeView1_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
        }
    }
}
