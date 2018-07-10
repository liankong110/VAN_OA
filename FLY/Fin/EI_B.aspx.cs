using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Dal.ReportForms;
using VAN_OA.Model.JXC;

namespace VAN_OA.Fin
{
    public partial class EI_B : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var billTypeList = new Invoice_BillTypeService().GetListArray(" IsStop=0");
                var personList = new Invoice_PersonService().GetListArray(" IsStop=0");
                dllBillType.DataSource = billTypeList;
                dllBillType.DataTextField = "BillName";
                dllBillType.DataValueField = "Id";
                dllBillType.DataBind();

                dllPerson.DataSource = personList;
                dllPerson.DataTextField = "Name";
                dllPerson.DataValueField = "Id";
                dllPerson.DataBind();
               
            }
        }

        private bool Check()
        {
            if (string.IsNullOrEmpty(txtSupplierName.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('供应商名称不能为空！');</script>"));
                return false;
            }
            if (string.IsNullOrEmpty(txtTotal.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('金额不能为空！');</script>"));
                return false;
            }
            if (CommHelp.VerifesToNum(txtTotal.Text) == false)
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('金额格式错误！');</script>"));
                return false;
            }

            if (string.IsNullOrEmpty(dllUse.Text))
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, string.Format("<script>alert('请选择用途！');</script>"));
                return false;
            }
            return true;
        }
        private ElectronicInvoice GetModel()
        {
            var model = new ElectronicInvoice();
            model.SupplierName = txtSupplierName.Text.Trim();
            model.SupplierBrandNo = txtBrandNo.Text;
            model.SupplierBrandName = txtBandName.Text;
            model.City = txtCity.Text;
            model.Province = txtProvice.Text;
            model.ActPay = Convert.ToDecimal(txtTotal.Text);
            model.BillType = dllBillType.SelectedItem.Text;
            model.Person = dllPerson.SelectedItem.Text;
            model.Use = dllUse.SelectedItem.Text;
            model.Company = dllCompany.SelectedItem.Text;
            Session["ElectronicInvoice"] = model;
            return model;
        }

        protected void btnYuLan_Click(object sender, EventArgs e)
        {
            if (Check() == false)
            {
                return;
            }
            var model = GetModel();
            if (model.BillType == "银行申请单")
            {
                Response.Write("<script>window.open('/Fin/EI_BankBill.aspx','_blank')</script>");
            }
            else if (model.BillType == "支票")
            {
                Response.Write("<script>window.open('/Fin/EI_BlankCheck.aspx','_blank')</script>");
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Check() == false)
            {
                return;
            }
            var model = GetModel();
            Response.Write("<script>window.open('/Fin/EI_InCome.aspx','_blank')</script>");
        }

        protected void txtSupplierName_TextChanged(object sender, EventArgs e)
        {
            if (txtSupplierName.Text != "")
            {
                TB_SupplierInfoService supplierSer = new TB_SupplierInfoService();

                var supplierList = supplierSer.GetListArray(string.Format(" SupplierName='{0}'", txtSupplierName.Text));
                if (supplierList.Count > 0)
                {
                    
                    var supplierModel = supplierList[0];
                    txtBandName.Text = supplierModel.SupplierBrandName;
                    txtBrandNo.Text = supplierModel.SupplierBrandNo;
                    txtCity.Text = supplierModel.City;
                    txtProvice.Text = supplierModel.Province;

                    if (supplierModel.City.Contains("苏州") || supplierModel.City.Contains("太仓") || supplierModel.City.Contains("相城")
                   || supplierModel.City.Contains("吴中") || supplierModel.City.Contains("张家港")
                   || supplierModel.City.Contains("常熟") || supplierModel.City.Contains("昆山"))
                    {
                        foreach (ListItem item in dllBillType.Items)
                        {
                            if (item.Text == "支票")
                            {
                                dllBillType.Text = item.Value;
                                break;
                            }
                        }

                    }
                    else
                    {
                        foreach (ListItem item in dllBillType.Items)
                        {
                            if (item.Text == "银行申请单")
                            {
                                dllBillType.Text = item.Value;
                                break;
                            }
                        }
                    }
                }
                
              
            }
        }
    }
}