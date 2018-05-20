using System;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class WFInvoicePerson : System.Web.UI.Page
    {
        Invoice_PersonService invoicePensonService = new Invoice_PersonService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from Invoice_Person where Name='{0}'",
                txtName.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('人员姓名[{0}]，已经存在！');</script>", txtName.Text));
                        return;
                    }
                    Invoice_Person model = getModel();
                    if (this.invoicePensonService.Add(model) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('添加成功！');</script>");
                        Clear();
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
            base.Response.Redirect("~/BaseInfo/WFInvoicePersonList.aspx");
        }

        private void Clear()
        {
            txtName.Text = "";
            txtCardNo.Text = "";
            txtPhone.Text = "";
            txtName.Focus();
        }


        public Invoice_Person getModel()
        {
            Invoice_Person model = new Invoice_Person();
            model.Name = txtName.Text;
            model.Phone = txtPhone.Text;
            model.CardNo = txtCardNo.Text;
            model.IsStop = ddlIsStop.Text == "0" ? false : true;
            if (Request["Id"] != null)
            {
                model.Id = Convert.ToInt32(Request["Id"]);
            }
            return model;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from Invoice_Person where Name='{0}' and id<>{1}",
                  txtName.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('人员姓名[{0}]，已经存在！');</script>", txtName.Text));
                        return;
                    }
                    Invoice_Person model = getModel();
                    if (this.invoicePensonService.Update(model))
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
            string strErr = "";
            if (this.txtName.Text.Trim().Length == 0)
            {
                strErr += "人员姓名不能为空！\\n";
            }
            if (this.txtCardNo.Text.Trim().Length == 0)
            {
                strErr += "身份证号不能为空！\\n";
            }
            if (this.txtPhone.Text.Trim().Length == 0)
            {
                strErr += "手机号不能为空！\\n";
            }
            if (strErr != "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('{0}');</script>", strErr));
                return false;
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
                    Invoice_Person model = this.invoicePensonService.GetModel(Convert.ToInt32(base.Request["Id"]));              
                    txtName.Text = model.Name;
                    txtCardNo.Text = model.CardNo;
                    txtPhone.Text = model.Phone;
                    this.ddlIsStop.Text = model.IsStop ? "1" : "0";
                }
                else
                {
                    this.btnUpdate.Visible = false;
                }


            }
        }


    }
}
