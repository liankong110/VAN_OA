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

namespace VAN_OA.JXC
{
    public partial class WFUpdateGuestName : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {

            if (txtOldName.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写原有客户名称！');</script>");
                txtOldName.Focus();
                return ;
            }
            if (txtNewName.Text == "")
            {
                base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('请填写更新为客户名称！');</script>");
                txtNewName.Focus();
                return ;
            }
            string sql = string.Format(@" --21    采购订单检验
update CAI_OrderChecks  set GuestName='{1}' where GuestName='{0}'
--20    采购订单
update CAI_POOrder  set GuestName='{1}' where GuestName='{0}'
--19    项目订单
update CG_POOrder  set GuestName='{1}' where GuestName='{0}'
--26    销售发票
update Sell_OrderFP  set GuestName='{1}' where GuestName='{0}'
--29    发票单签回
update Sell_OrderFPBack  set GuestName='{1}' where GuestName='{0}'
--25    销售退货
update Sell_OrderInHouse  set GuestName='{1}' where GuestName='{0}'
--23    销售出库
update Sell_OrderOutHouse  set Supplier='{1}' where Supplier='{0}'
--28    出库单签回
update Sell_OrderOutHouseBack  set GuestName='{1}' where GuestName='{0}'
--27    到款单
update TB_ToInvoice set GuestName='{1}' where GuestName='{0}'
--申请请款单
update tb_FundsUse set GuestName='{1}' where GuestName='{0}'
--预期报销单
update Tb_DispatchList set GuestName='{1}' where GuestName='{0}'
--预期报销单(油费报销)
update Tb_DispatchList set GuestName='{1}' where GuestName='{0}'
update CAI_POOrder   set GuestName='{1}' where GuestName='{0}'
update CAI_OrderInHouse   set GuestName='{1}' where GuestName='{0}'
--私车公用申请单
update  tb_UseCar set POGuestName='{1}' where POGuestName='{0}'
--用车明细表
update TB_UseCarDetail set GuestName='{1}' where GuestName='{0}'
--加班单
update tb_OverTime set POGuestName='{1}' where POGuestName='{0}'
--客户联系跟踪表
update TB_GuestTrack set GuestName='{1}' where GuestName='{0}'
--公交车费用
update TB_BusCardUse set POGuestName='{1}' where POGuestName='{0}'", txtOldName.Text, txtNewName.Text);
           var result= DBHelp.ExeCommand(sql);
           if (result)
           {
               base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('更新成功！');</script>");

           }
           else
           {
               base.ClientScript.RegisterStartupScript(base.GetType(), null, "<script>alert('更新失败！');</script>");
         
           }
        }
    }
}
