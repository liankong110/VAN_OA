using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// TB_SupplierInvoices:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_SupplierAdvancePayments
    {
        public TB_SupplierAdvancePayments()
        { }
        #region Model
        private int _ids;
        private int _id;
        private int _ruids;
        private string _supplierfpno;
        private int _supplierinvoicestatues;
        private DateTime _supplierinvoicedate;
        private decimal _supplierinvoicetotal;
        /// <summary>
        /// 
        /// </summary>
        public int Ids
        {
            set { _ids = value; }
            get { return _ids; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CaiIds
        {
            set { _ruids = value; }
            get { return _ruids; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SupplierFPNo
        {
            set { _supplierfpno = value; }
            get { return _supplierfpno; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime SupplierInvoiceDate
        {
            set { _supplierinvoicedate = value; }
            get { return _supplierinvoicedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SupplierInvoiceTotal
        {
            set { _supplierinvoicetotal = value; }
            get { return _supplierinvoicetotal; }
        }

        public decimal SupplierInvoiceNum { get; set; }
        public decimal SupplierInvoicePrice { get; set; }

        public string SupplierProNo { get; set; }
        #endregion Model

       
    }
}
