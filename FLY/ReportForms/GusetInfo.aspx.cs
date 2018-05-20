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
using VAN_OA.Dal.ReportForms;
using VAN_OA.Model.ReportForms;


namespace VAN_OA.ReportForms
{
    public partial class GusetInfo : System.Web.UI.Page
    {
        private tb_GusetInfoService guestSer = new tb_GusetInfoService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {

                    tb_GusetInfo model = new tb_GusetInfo();
                    model.ZhuTel = txtZhuTel.Text;
                    model.tel2 = txttel2.Text;
                    model.tel1 = txttel1.Text;
                    model.LinkMan = txtLinkMan.Text;
                    model.GuestName = txtGuestName.Text;

                    if (txtAccount.Text != "")
                    {
                        model.Account = Convert.ToDecimal(txtAccount.Text);
                    }
                    model.CreateTime = DateTime.Now;
                    model.CreateUser = Convert.ToInt32(Session["currentUserId"]);

                    if (this.guestSer.Add(model) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");

                        txtAccount.Text = "";
                        txtGuestName.Text = "";
                        txtLinkMan.Text = "";
                        txttel1.Text = "";
                        txttel2.Text = "";
                        txtZhuTel.Text = "";
                        this.txtGuestName.Focus();
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
            base.Response.Redirect(Session["POUrl"].ToString());
        }



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {

                    tb_GusetInfo model = new tb_GusetInfo();
                    model.ZhuTel = txtZhuTel.Text;
                    model.tel2 = txttel2.Text;
                    model.tel1 = txttel1.Text;
                    model.LinkMan = txtLinkMan.Text;
                    model.GuestName = txtGuestName.Text;
                    if (txtAccount.Text != "")
                    {
                        model.Account = Convert.ToDecimal(txtAccount.Text);
                    }
                    model.CreateTime = DateTime.Now;
                    model.CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    model.Id = Convert.ToInt32(base.Request["Id"]);
                    if (this.guestSer.Update(model))
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

            if (this.txtGuestName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写客户名称！');</script>");
                this.txtGuestName.Focus();
                return false;
            }

            if (this.txtLinkMan.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写联系人！');</script>");
                this.txtLinkMan.Focus();
                return false;
            }
            if (txtAccount.Text != "")
            {
                try
                {
                    Convert.ToDecimal(txtAccount.Text);
                }
                catch (Exception)
                {
                    
                   base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的账期格式有误！');</script>");
                   txtAccount.Focus();
                    return false;
                }
            }
            //if (this.txtInvName.Text.Trim() == "")
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写商品名称！');</script>");
            //    this.txtInvName.Focus();
            //    return false;
            //}


            //if (this.txtNum.Text.Trim() == "")
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写数量！');</script>");
            //    this.txtNum.Focus();
            //    return false;
            //}


            //if (this.txtPrice.Text.Trim() == "")
            //{
            //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写单价！');</script>");
            //    this.txtPrice.Focus();
            //    return false;
            //}

            if (base.Request["Id"] != null)
            {
                //if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_Role where RoleCode='{0}' and RID<>{1}", this.txtRoleCode.Text.Trim(), base.Request["RoleId"]))) > 0)
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('角色编码已经存在,请重新填写！');</script>");
                //    this.txtRoleCode.Focus();
                //    return false;
                //}
                //if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_Role where RoleName='{0}' and RID<>{1}", this.txtRoleName.Text.Trim(), base.Request["RoleId"]))) > 0)
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('角色名称已经存在,请重新填写！');</script>");
                //    this.txtRoleName.Focus();
                //    return false;
                //}
            }
            else
            {
                //if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from TB_PO where RoleCode='{0}'", this.txtRoleCode.Text.Trim()))) > 0)
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('角色编码已经存在,请重新填写！');</script>");
                //    this.txtRoleCode.Focus();
                //    return false;
                //}
                //if (Convert.ToInt32(DBHelp.ExeScalar(string.Format("select count(*) from tb_Role where RoleName='{0}'", this.txtRoleName.Text.Trim()))) > 0)
                //{
                //    base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('角色名称已经存在,请重新填写！');</script>");
                //    this.txtRoleName.Focus();
                //    return false;
                //}
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
                    tb_GusetInfo guestModel = this.guestSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    if (guestModel.Account != null)
                    {
                        txtAccount.Text = guestModel.Account.Value.ToString();
                    }
                    txtGuestName.Text = guestModel.GuestName;
                    txtLinkMan.Text = guestModel.LinkMan;
                    txttel1.Text = guestModel.tel1;
                    txttel2.Text = guestModel.tel2;
                    txtZhuTel.Text = guestModel.ZhuTel;                    
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }

       
    }
}
