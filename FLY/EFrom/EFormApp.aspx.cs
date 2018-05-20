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
using VAN_OA.Dal.EFrom;
using System.Collections.Generic;
using VAN_OA.Model.EFrom;


namespace VAN_OA.EFrom
{
    public partial class EFormApp : System.Web.UI.Page
    {

        private A_ProInfoService proSer = new A_ProInfoService();


     

     
      

       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {

                string where = string.Format(" 1=1 and pro_Type not in ('项目订单','采购订单','采购订单检验','采购入库','销售出库','采购退货','销售退货','销售发票','到款单','出库单签回','发票单签回','供应商付款单','供应商预付款单')");
                List<A_ProInfo> roles = this.proSer.GetListArray(where);
                
                DataList1.DataSource = roles;
                DataList1.DataBind();


            }
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "shenqi")
            {
                string sql = string.Format("select pro_Type from A_ProInfo where pro_Id=" + e.CommandArgument);
                object obj = DBHelp.ExeScalar(sql);
                Session["backurl"] = null;
                if (obj != null)
                {
                    tb_EFormService eformSer = new tb_EFormService();

                     
                   
                    string type = obj.ToString();
                    string url = eformSer.getUrlToAdd(e.CommandArgument.ToString(), type);
                    if (url != "")
                    {
                        Response.Redirect(url);
                    }
                }

            }
        }
    }
}
