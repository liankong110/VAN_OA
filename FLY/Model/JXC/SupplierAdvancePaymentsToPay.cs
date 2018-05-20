using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    [Serializable]
    public class SupplierAdvancePaymentsToPay
    {
        #region Model
        private int _ids;
        private int _checkids;
        private int _inhouseids;
        private decimal _allnum;
        private decimal _lastprice;
        private decimal _checknum;
        private decimal _supplierinvoicetotal;
        /// <summary>
        /// 
        /// </summary>
        public int ids
        {
            set { _ids = value; }
            get { return _ids; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int checkIds
        {
            set { _checkids = value; }
            get { return _checkids; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int inHouseIds
        {
            set { _inhouseids = value; }
            get { return _inhouseids; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal allNum
        {
            set { _allnum = value; }
            get { return _allnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal lastPrice
        {
            set { _lastprice = value; }
            get { return _lastprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CheckNum
        {
            set { _checknum = value; }
            get { return _checknum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SupplierInvoiceTotal
        {
            set { _supplierinvoicetotal = value; }
            get { return _supplierinvoicetotal; }
        }

        public DateTime  MinSupplierInvoiceDate { get; set; }
        #endregion Model

    }
}
