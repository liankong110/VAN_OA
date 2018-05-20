using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// CaiNotRuView:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CaiNotRuView
    {
        public CaiNotRuView()
        { }
        #region Model
        private string _prono;
        private string _pono;
        private string _poname;
        private DateTime? _podate;
        private decimal? _pototal;
        private string _guestname;
        private string _ae;
        private string _goodno;
        private string _goodname;
        private string _goodspec;
        private decimal _num;
        private decimal _totalordernum;
        private string _lastsupplier;
        private decimal? _lastprice;
        private decimal _pogoodsum;
        private DateTime? _mininhousedate;
        private decimal _inhousesum;
        private decimal _caigoodsum;
        private decimal _goodnum;
        private decimal? _goodavgprice;
        /// <summary>
        /// 
        /// </summary>
        public string ProNo
        {
            set { _prono = value; }
            get { return _prono; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PONo
        {
            set { _pono = value; }
            get { return _pono; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string POName
        {
            set { _poname = value; }
            get { return _poname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? PODate
        {
            set { _podate = value; }
            get { return _podate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? POTotal
        {
            set { _pototal = value; }
            get { return _pototal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GuestName
        {
            set { _guestname = value; }
            get { return _guestname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AE
        {
            set { _ae = value; }
            get { return _ae; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodNo
        {
            set { _goodno = value; }
            get { return _goodno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodName
        {
            set { _goodname = value; }
            get { return _goodname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodSpec
        {
            set { _goodspec = value; }
            get { return _goodspec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Num
        {
            set { _num = value; }
            get { return _num; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal totalOrderNum
        {
            set { _totalordernum = value; }
            get { return _totalordernum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lastSupplier
        {
            set { _lastsupplier = value; }
            get { return _lastsupplier; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? lastPrice
        {
            set { _lastprice = value; }
            get { return _lastprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal POGoodSum
        {
            set { _pogoodsum = value; }
            get { return _pogoodsum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? MinInHouseDate
        {
            set { _mininhousedate = value; }
            get { return _mininhousedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal InHouseSum
        {
            set { _inhousesum = value; }
            get { return _inhousesum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CaiGoodSum
        {
            set { _caigoodsum = value; }
            get { return _caigoodsum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal GoodNum
        {
            set { _goodnum = value; }
            get { return _goodnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? GoodAvgPrice
        {
            set { _goodavgprice = value; }
            get { return _goodavgprice; }
        }
        #endregion Model

        public int IsHanShui { get; set; }
        public string GoodAreaNumber { get; set; }

        public decimal SupplierInvoicePrice { get; set; }

        public decimal SupplierInvoiceTotal { get { return SupplierInvoicePrice *(CaiGoodSum); } }

       
    }
}
