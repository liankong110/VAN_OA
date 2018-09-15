using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace VAN_OA.Model.KingdeeInvoice
{

    /// <summary>
    /// Invoice:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Payable:Invoice
    {
        public Payable()
        { }
        public string SupplierName { get; set; }

        /// <summary>
        /// 0是未到账,1是全到帐 2是为未全到帐
        /// </summary>
        public override string IsAccountString
        {
            get
            {
                if (IsAccount == 0)
                {
                    return "未付款";
                }
                if (IsAccount == 1)
                {
                    return "全付款";
                }
                if (IsAccount == 2)
                {
                    return "未全付款";
                }
                return "";
            }
        }
    }
}
