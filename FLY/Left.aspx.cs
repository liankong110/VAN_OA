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
    public partial class Left : System.Web.UI.Page
    {
        private List<SysForm> sysForms = null;
        private List<SysMenu> sysMenus = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.Session["currentUserId"] == null)
            {
                Response.Write("<script>top.location.href='login.aspx';</script>");  
            }
            else
            {
              
                this.fillMenu();
            }
        }

        private void fillMenu()
        {
            List<RoleUser> role_user = new RoleUserService().getRoleIdByUserId(Convert.ToInt32(base.Session["currentUserId"]));
            string roleStr = "";
            for (int i = 0; i < role_user.Count; i++)
            {
                roleStr = roleStr + "," + role_user[i].RoleId;
            }
            if (roleStr != "")
            {
                roleStr = roleStr.Substring(1, roleStr.Length - 1);
            }
            else
            {
                roleStr = "-1";
            }
            List<roleSysform> rsf = new roleSysFormService().getRightsByRoleId(roleStr);
            this.sysForms = new List<SysForm>();
            foreach (roleSysform r in rsf)
            {
                this.sysForms.Add(r.Sysform);
            }
            this.sysMenus = new sysMenuService().getAllSysMenus();
            this.sysForms.Sort(delegate(SysForm form1, SysForm form2)
            {
                return form1.FormIndex.CompareTo(form2.FormIndex);
            });
            List<ShowAccordion> accs = new List<ShowAccordion>();
            foreach (SysMenu sm in this.sysMenus)
            {
                string DisplayName = sm.DisplayName;
                ShowAccordion showOne = new ShowAccordion();
                showOne.HeardText = DisplayName;
                string body = "";
                foreach (SysForm sf in this.sysForms)
                {
                    if (sf.UpperID == sm.MenuID)
                    {
                        string url = base.ResolveUrl(sf.AssemblyPath);
                        body = body + string.Format("<a href='{1}' target='right'>{0}</a><br><div style='border-top:1px dashed #cccccc;height: 1px;overflow:hidden;margin-top:7px;margin-bottom:7px'></div>", sf.DisplayName, url);
                    }
                }
                if (body != "")
                {
                    showOne.Body = body;
                    accs.Add(showOne);
                }
            }
            this.sysMenus.Clear();
            this.sysForms.Clear();
            this.MyAccordion.DataSource = accs;
            this.MyAccordion.DataBind();
        }
    }
}
