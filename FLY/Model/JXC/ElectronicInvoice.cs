using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    [Serializable]
    public class ElectronicInvoice
    {
        public string PONo { get; set; }

        public string ProNo { get; set; }
        public string busType { get; set; }
        public decimal ActPay { get; set; }
        public string SupplierName { get; set; }
        public string SupplierBrandName { get; set; }
        public string SupplierBrandNo { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
    
        public string BillType { get; set; }
        public string Person { get; set; }
        public string Use { get; set; }
        public string SupplieSimpeName { get;  set; }
        public string Company { get;  set; }
    }
}