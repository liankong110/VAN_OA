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
    }
}
