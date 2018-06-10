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
    public partial class EI_BankBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ElectronicInvoice"] != null)
                {
                    var model = Session["ElectronicInvoice"] as ElectronicInvoice;
                    txtCompanyName.Text = model.Company;
                    txtRightCompanyName.Text = model.Company;

                    txtSupplierName.Text = model.SupplierName;
                    txtRightSupplierName.Text = model.SupplierName;
                    txtSupplierCardNo.Text = model.SupplierBrandNo;
                    txtRightSupplierCardNo.Text = model.SupplierBrandNo;
                    txtBrandName.Text = model.SupplierBrandName;
                    txtRightBrandName.Text = model.SupplierBrandName;
                    txtNum.Text = "¥" + model.ActPay.ToString("f2").Replace(".", "");
                    txtDaTotal.Text = RMBCapitalization.RMBAmount(Convert.ToDouble(model.ActPay)); 
                    txtUse.Text = model.ProNo;
                    txtTotal.Text = "¥"+string.Format("{0:n2}", model.ActPay);

                    TB_CompanyService companySer = new TB_CompanyService();
                    var companyModel = companySer.GetListArray(string.Format(" ComName='{0}'", model.Company))[0];
                    txtCardNo.Text = companyModel.KaHao;
                    txtRightCompanyCardNo.Text = companyModel.KaHao;

                    TB_SupplierInfoService supplierSer = new TB_SupplierInfoService();
                    var list = supplierSer.GetListArray(string.Format(" SupplierName='{0}'", model.SupplierName));
                    if (list.Count > 0)
                    {
                        var supplierModel = list[0];
                        txtBrandAddress.Text = (string.IsNullOrEmpty(supplierModel.Province) ? "        " : supplierModel.Province)
                            + "  " + supplierModel.City;
                        txtRightBrandAddress.Text = (string.IsNullOrEmpty(supplierModel.Province) ? "        " : supplierModel.Province)
                            + "  " + supplierModel.City;                       
                    }
                    else
                    {
                        txtBrandAddress.Text = "";
                        txtRightBrandAddress.Text = "";
                    }

                    if (!string.IsNullOrEmpty(model.Person))
                    {
                        var person = new Invoice_PersonService().GetListArray(string.Format(" name='{0}'", model.Person))[0];
                        txtId.Text = person.CardNo;
                        txtRightPhone.Text = person.Phone;
                        txtPhone.Text = person.Phone;
                    }
                }
            }
        }

        public string ConvertNum(string Num)
        {
            string result = "";
            int i = 0;
            foreach (char c in Num)
            {
                if (i > 0 && Convert.ToInt32(Num) > 9)
                {

                }
                string da = "";
                if (c.ToString() == "0")
                {
                    da = "零";
                }
                else if (c.ToString() == "1")
                {
                    da = "壹";
                }
                else if (c.ToString() == "2")
                {
                    da = "贰";
                }
                else if (c.ToString() == "3")
                {
                    da = "叁";
                }
                else if (c.ToString() == "4")
                {
                    da = "肆";
                }
                else if (c.ToString() == "5")
                {
                    da = "伍";
                }
                else if (c.ToString() == "6")
                {
                    da = "陆";
                }
                else if (c.ToString() == "7")
                {
                    da = "柒";
                }
                else if (c.ToString() == "8")
                {
                    da = "捌";
                }
                else if (c.ToString() == "9")
                {
                    da = "玖";
                }


                result += da;
                i++;

            }
            return result;
        }
        

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            var model = Session["ElectronicInvoice"] as ElectronicInvoice;
            model.Company = txtCompanyName.Text;
            model.SupplierName = txtSupplierName.Text;
            model.SupplierBrandNo = txtSupplierCardNo.Text;
            model.SupplierBrandName = txtBrandName.Text;
            model.ProNo = txtUse.Text;
            Session["ElectronicInvoice"] = model;
            base.Response.Redirect("~/Fin/EI_BankBillPrint.aspx");
        }
    }
}