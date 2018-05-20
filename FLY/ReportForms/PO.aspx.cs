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
    public partial class PO : System.Web.UI.Page
    {
        private TB_POService poSer = new TB_POService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {

                    TB_PO poModel = new TB_PO();
                    poModel.DataTime = Convert.ToDateTime(txtDataTime.Text);
                    poModel.InvName = txtInvName.Text;
                    poModel.Num = Convert.ToDecimal(txtNum.Text);
                    poModel.Price=Convert.ToDecimal(txtPrice.Text);                   
                    poModel.Unit=txtUnit.Text ;
                    poModel.UnitName=txtUnitName.Text;
                    poModel.CreateTime = DateTime.Now;
                    poModel.CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    poModel.Seller = txtSeller.Text;
                    if (this.poSer.Add(poModel) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                      
                        txtInvName.Text = "";
                        txtNum.Text = "";
                        txtPrice.Text = "";
                        txtUnit.Text = "";
                        txtSeller.Text = "";
                        this.txtDataTime.Focus();
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
                    TB_PO poModel = new TB_PO();
                    poModel.DataTime = Convert.ToDateTime(txtDataTime.Text);
                    poModel.InvName = txtInvName.Text;
                    poModel.Num = Convert.ToDecimal(txtNum.Text);
                    poModel.Price = Convert.ToDecimal(txtPrice.Text);
                    poModel.Unit = txtUnit.Text;
                    poModel.UnitName = txtUnitName.Text;
                    //poModel.CreateTime = DateTime.Now;
                    //poModel.CreateUser = Convert.ToInt32(Session["currentUserId"]);
                    poModel.Id = Convert.ToInt32(base.Request["Id"]);
                    if (this.poSer.Update(poModel))
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

            if (this.txtDataTime.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写日期！');</script>");
                this.txtDataTime.Focus();
                return false;
            }
            if (CommHelp.VerifesToDateTime(txtDataTime.Text.Trim()) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('日期 格式错误！');</script>");
                return false;
            }

            if (this.txtUnitName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写往来单位！');</script>");
                this.txtUnitName.Focus();
                return false;
            }

            if (this.txtInvName.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写商品名称！');</script>");
                this.txtInvName.Focus();
                return false;
            }


            if (this.txtNum.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写数量！');</script>");
                this.txtNum.Focus();
                return false;
            }


            if (this.txtPrice.Text.Trim() == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写单价！');</script>");
                this.txtPrice.Focus();
                return false;
            }
            try
            {
                Convert.ToDecimal(txtNum.Text);
                Convert.ToDecimal(txtPrice.Text);
            }
            catch (Exception)
            {
                
                  base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写的数字格式有误！');</script>");
                  txtNum.Focus();
                  return false;
            }
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
                    TB_PO poModel = this.poSer.GetModel(Convert.ToInt32(base.Request["Id"]));
                    txtDataTime.Text = poModel.DataTime.ToShortDateString();
                    txtInvName.Text = poModel.InvName;
                    txtNum.Text = poModel.Num.ToString();
                    txtPrice.Text = poModel.Price.ToString();
                    txtTotal.Text = (Convert.ToDecimal(txtNum.Text) * Convert.ToDecimal(txtPrice.Text)).ToString();
                    txtUnit.Text = poModel.Unit;
                    txtUnitName.Text = poModel.UnitName;
                    txtSeller.Text = poModel.Seller;
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }
            }
        }

        protected void txtNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPrice.Text != "" && txtNum.Text != "")
                {
                    txtTotal.Text = (Convert.ToDecimal(txtNum.Text) * Convert.ToDecimal(txtPrice.Text)).ToString();
                }
            }
            catch (Exception)
            {
                
               base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写数字格式不正确！');</script>");
            }
        }

        protected void txtPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPrice.Text != "" && txtNum.Text != "")
                {
                    txtTotal.Text = (Convert.ToDecimal(txtNum.Text) * Convert.ToDecimal(txtPrice.Text)).ToString();
                }
            }
            catch (Exception)
            {

                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('你填写数字格式不正确！');</script>");
            }
        }
    }
}
