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
using VAN_OA.Dal.EFrom;

using VAN_OA.Model.EFrom;


namespace VAN_OA
{
    public partial class Top : System.Web.UI.Page
    {

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            base.Session.RemoveAll();
            //Request.Path
            Response.Write(string.Format("<script>top.location.href='{0}';</script>", "/login.aspx"));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.Session["currentUserId"] == null)
            {
                Response.Write(string.Format("<script>top.location.href='{0}';</script>", "/login.aspx"));
            }
            else
            {
                this.lblLoginName.Text = base.Session["LoginName"].ToString();

            }

            if (!IsPostBack)
            {
                GetTodoCount();
            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            GetTodoCount();
        }

        private void GetTodoCount()
        {
            string sql = string.Format(" 1=1 and state='执行中'");


            tb_EFormService eformSer = new tb_EFormService();
            int allEForms = eformSer.GetListArray_ToDo_Count(sql, Convert.ToInt32(Session["currentUserId"]));
            if (allEForms > 0)
            {
                lblMessTodo.Text = string.Format("你有 {0} 个文件要审批", allEForms);
            }
            else
            {
                lblMessTodo.Text = "";
            }

        }
    }
}
