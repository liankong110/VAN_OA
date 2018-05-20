using System;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.BaseInfo;



namespace VAN_OA.BaseInfo
{
    public partial class WFInvoiceBillType : System.Web.UI.Page
    {
        Invoice_BillTypeService billService = new Invoice_BillTypeService();
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.FormCheck())
            {
                try
                {
                    string sqlCheck = string.Format("select count(*) from Invoice_BillType where BillName='{0}'",
                txtBillName.Text);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {
                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('票据名称[{0}]，已经存在！');</script>", txtBillName.Text));
                        return;
                    }
                    Invoice_BillType model = getModel();
                    if (this.billService.Add(model) > 0)
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
            base.Response.Redirect("~/BaseInfo/WFInvoiceBillTypeList.aspx");
        }

        private void Clear()
        {
            txtBillName.Text = "";
            txtBillType.Text = "";
            txtBillName.Focus();
        }


        public Invoice_BillType getModel()
        {
            VAN_OA.Model.BaseInfo.Invoice_BillType model = new VAN_OA.Model.BaseInfo.Invoice_BillType();
            model.BillName = txtBillName.Text;
            model.BillType = txtBillType.Text;
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
                    string sqlCheck = string.Format("select count(*) from Invoice_BillType where BillName='{0}' and id<>{1}",
                  txtBillName.Text, Request["Id"]);
                    if (Convert.ToInt32(DBHelp.ExeScalar(sqlCheck)) > 0)
                    {

                        base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('票据名称[{0}]，已经存在！');</script>", txtBillName.Text));
                        return;
                    }
                    Invoice_BillType model = getModel();
                    if (this.billService.Update(model))
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
            if (this.txtBillName.Text.Trim().Length == 0)
            {
                strErr += "票据名称不能为空！\\n";
            }
            if (this.txtBillType.Text.Trim().Length == 0)
            {
                strErr += "票据名称不能为空！\\n";
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
                    Invoice_BillType model = this.billService.GetModel(Convert.ToInt32(base.Request["Id"]));
                    this.txtBillName.Text = model.BillName;
                    this.txtBillType.Text = model.BillType;
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
