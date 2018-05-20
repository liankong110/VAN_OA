using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;
using VAN_OA.Model.EFrom;
using VAN_OA.Model.JXC;

namespace VAN_OA
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            List<int> ruids = new List<int>();
            ruids.Add(9917);
            //ruids.Add(14294);
            //ruids.Add(14312);
            using (SqlConnection conn = DBHelp.getConn())
            {
                conn.Open();

                foreach (var ids in ruids)
                {
                    var selectInfo = string.Format(@"select avg(SupplierInvoiceNum) as avgSupplierInvoiceNum, sum(SupplierInvoiceTotal) as sumSupplierInvoiceTotal from TB_SupplierInvoices 
left join TB_SupplierInvoice on TB_SupplierInvoice.id=TB_SupplierInvoices.id
where RuIds={0} and SupplierInvoiceTotal>0 and ((status='通过' and IsYuFu=0) or (Status<>'不通过' and IsYuFu=1)) ", ids);

                    SqlCommand objCommand = conn.CreateCommand();
                    objCommand.CommandText = selectInfo;
                    decimal avgSupplierInvoiceNum = 0;
                    decimal sumSupplierInvoiceTotal = 0;
                    using (var reader = objCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            object ojb;
                            ojb = reader["avgSupplierInvoiceNum"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                avgSupplierInvoiceNum = Convert.ToDecimal(ojb);
                            }
                            ojb = reader["sumSupplierInvoiceTotal"];
                            if (ojb != null && ojb != DBNull.Value)
                            {
                                sumSupplierInvoiceTotal = Convert.ToDecimal(ojb);
                            }
                        }
                        reader.Close();
                    }

                    TB_SupplierInvoice model = new TB_SupplierInvoice();
                    model.Status = "通过";
                    model.CreteTime = DateTime.Now;
                    model.CreateName = "姚世中";
                    model.CaiTuiProNo = "";
                    model.LastSupplier = "易佳通";
                    model.Remark = "全部退货";
                    var eform = new tb_EForm();
                    eform.appPer = 117;
                    eform.appTime = DateTime.Now;
                    eform.createPer = 117;
                    eform.createTime = DateTime.Now;
                    eform.proId = 31;
                    eform.state = "通过";
                    eform.toPer = 0;
                    eform.toProsId = 0;
                    var orders = new List<SupplierToInvoiceView>();
                    var invoiceModel = new SupplierToInvoiceView();
                    invoiceModel.Ids = ids;

                    //invoiceModel.IsYuFu = true;
                    invoiceModel.SupplierFPNo = "";
                    invoiceModel.SupplierInvoiceDate = DateTime.Now;
                    var AvgSupplierInvoicePrice = -(sumSupplierInvoiceTotal / avgSupplierInvoiceNum);
                    invoiceModel.SupplierInvoicePrice = AvgSupplierInvoicePrice;
                    invoiceModel.lastGoodNum = avgSupplierInvoiceNum;
                    invoiceModel.SupplierInvoiceTotal = -sumSupplierInvoiceTotal;

                    invoiceModel.FuShuTotal = invoiceModel.SupplierInvoiceTotal;
                    invoiceModel.ActPay = invoiceModel.SupplierInvoiceTotal;

                    invoiceModel.GuestName = "易佳通";
                    //invoiceModel.CaiTuiProNo = caiTuiProNo;
                    invoiceModel.RePayClear = 2;
                    invoiceModel.IsPayStatus = 0;//未支付
                    invoiceModel.IsHeBing = 1;
                    orders.Add(invoiceModel);
                    int mainId = 0;
                    new TB_SupplierInvoiceService().addTran(model, eform, orders, out mainId);
                }
            }
        }
    }
}