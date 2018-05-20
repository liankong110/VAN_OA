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
using VAN_OA.Dal.Performance;
using VAN_OA.Model.Performance;

namespace VAN_OA.Performance
{
    public partial class A_PASectionCmd : System.Web.UI.Page
    {
        private A_PASectionService PASectionSer = new A_PASectionService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    A_PASection PASection = new A_PASection();
                    PASection.A_PASectionName = this.txtPASectionName.Text.Trim();
                    if (this.PASectionSer.Add(PASection) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");

                        this.txtPASectionName.Text = "";
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
            base.Response.Redirect("~/Performance/A_PASectionList.aspx");
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            this.txtPASectionName.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    A_PASection PASection = new A_PASection();
                    PASection.A_PASectionName = this.txtPASectionName.Text.Trim();
                    PASection.A_PASectionID = Convert.ToInt32(base.Request["PASectionId"]);
                    PASectionSer.Update(PASection);
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('修改成功！');</script>");
                }
                catch (Exception ex)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('" + ex.Message + "！');</script>");
                }
            }
        }

        public bool FormCheck()
        {        
            if (this.txtPASectionName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写绩效考核科目名称！');</script>");
                this.txtPASectionName.Focus();
                return false;
            }
            if (base.Request["PASectionId"] != null)
            {

                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from A_PASection where A_PASectionName='{0}' and A_PASectionId<>{1}", this.txtPASectionName.Text.Trim(), base.Request["PASectionId"]))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('绩效考核项名称已经存在,请重新填写！');</script>");
                    this.txtPASectionName.Focus();
                    return false;
                }
            }
            else
            {
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from A_PASection where A_PASectionName='{0}'", this.txtPASectionName.Text.Trim()))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('绩效考核科目名称已经存在,请重新填写！');</script>");
                    this.txtPASectionName.Focus();
                    return false;
                }
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (base.Request["PASectionId"] != null)
                {
                    this.btnAdd.Visible = false;
                    A_PASection PASection = this.PASectionSer.GetModelList("A_PASectionId=" + base.Request["PASectionId"])[0];

                    this.txtPASectionName.Text = PASection.A_PASectionName;
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }
    }
}
