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
    public partial class A_PAItemCmd : System.Web.UI.Page
    {
        private A_PAItemService PAItemSer = new A_PAItemService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {

                    A_PAItem PAItem = new A_PAItem();
                    PAItem.A_PAItemName = this.txtPAItemName.Text.Trim();
                    PAItem.A_PAItemScore = decimal.Parse(this.txtPAItemScore.Text.Trim());
                    PAItem.A_PAItemAmount = decimal.Parse(this.txtPAItemAmount.Text.Trim());
                    if (this.PAItemSer.Add(PAItem) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");

                        this.txtPAItemName.Text = "";
                        this.txtPAItemScore.Text = "";
                        this.txtPAItemAmount.Text = "";
                        this.txtPAItemName.Focus();
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
            base.Response.Redirect("~/Performance/A_PAItemList.aspx");
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            this.txtPAItemName.Text = "";
            this.txtPAItemScore.Text = "";
            this.txtPAItemAmount.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    A_PAItem PAItem = new A_PAItem();
                    PAItem.A_PAItemName = this.txtPAItemName.Text.Trim();
                    PAItem.A_PAItemScore = decimal.Parse(this.txtPAItemScore.Text.Trim());
                    PAItem.A_PAItemAmount = decimal.Parse(this.txtPAItemAmount.Text.Trim());
                    PAItem.A_PAItemID = Convert.ToInt32(base.Request["PAItemId"]);
                    PAItemSer.Update(PAItem);
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
            if (this.txtPAItemName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写绩效考核项名称！');</script>");
                this.txtPAItemName.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(txtPAItemScore.Text))
            {
                if (CommHelp.VerifesToNum(txtPAItemScore.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('分值 格式错误！');</script>");
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(txtPAItemAmount.Text))
            {
                if (CommHelp.VerifesToNum(txtPAItemAmount.Text.Trim()) == false)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('奖罚金额 格式错误！');</script>");
                    return false;
                }
            }
            if (base.Request["PAItemId"] != null)
            {

                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from A_PAItem where A_PAItemName='{0}' and A_PAItemId<>{1}", this.txtPAItemName.Text.Trim(), base.Request["PAItemId"]))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('绩效考核项名称已经存在,请重新填写！');</script>");
                    this.txtPAItemName.Focus();
                    return false;
                }
            }
            else
            {
                if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from A_PAItem where A_PAItemName='{0}'", this.txtPAItemName.Text.Trim()))) > 0)
                {
                    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('绩效考核项名称已经存在,请重新填写！');</script>");
                    this.txtPAItemName.Focus();
                    return false;
                }
            }
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                if (base.Request["PAItemId"] != null)
                {
                    this.btnAdd.Visible = false;
                    A_PAItem PAItem = this.PAItemSer.GetModelList(" and A_PAItemID="+ base.Request["PAItemId"].ToString())[0];

                    this.txtPAItemName.Text = PAItem.A_PAItemName;
                    this.txtPAItemScore.Text = PAItem.A_PAItemScore.ToString() ;
                    this.txtPAItemAmount.Text = PAItem.A_PAItemAmount.ToString();
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }
    }
}
