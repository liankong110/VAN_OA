using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAN_OA.Dal.JXC;

namespace VAN_OA.JXC
{
    public partial class UpdatePONO : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            CG_POOrderService POOrderSer = new CG_POOrderService();
            System.Data.DataTable dt = DBHelp.getDataTable("select pono from CG_POOrder where IFZhui=0");
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                POOrderSer.GetOrder_ToInvoiceAndUpdatePoStatus(dr[0].ToString());
                //POOrderSer.SellOrderUpdatePoStatus2(dr[0].ToString());
            }
            base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('更新成功！');</script>");
        }
    }
}
