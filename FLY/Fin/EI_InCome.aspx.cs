using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.BaseInfo;
using VAN_OA.Model.JXC;

namespace VAN_OA.Fin
{
    public partial class EI_InCome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ElectronicInvoice"] != null)
            {
                var model = Session["ElectronicInvoice"] as ElectronicInvoice;
                txtDate.Text = string.Format("{0:yyyy     MM     dd}", DateTime.Now); 
               
                txtSupplierName.Text = model.SupplierName;
                txtSupplierCardNo.Text = model.SupplierBrandNo;
                txtSupplierBrandName.Text = model.SupplierBrandName;
                txtNum.Text = "¥" + model.ActPay.ToString("f2").Replace(".", "");
                txtDaTotal.Text = RMBCapitalization.RMBAmount(Convert.ToDouble(model.ActPay.ToString("f2"))); 
                txtUse.Text = model.ProNo;

                TB_CompanyService companySer = new TB_CompanyService();
                var companyModel = companySer.GetListArray(string.Format(" ComName='{0}'", model.Company))[0];
                txtCompanyName.Text = model.Company;
                txtCompanyCardNo.Text = companyModel.KaHao;
                txtCompanyBrandName.Text = companyModel.KaiHuHang;

               
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
            model.SupplierName = txtSupplierName.Text;
            model.SupplierBrandNo = txtSupplierCardNo.Text;
            model.SupplierBrandName = txtSupplierBrandName.Text;
            model.ProNo = txtUse.Text;
            Session["ElectronicInvoice"] = model;
            base.Response.Redirect("~/Fin/EI_InComePrint.aspx");
        }
    }
}