using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// JXC_REPORT:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class JXC_REPORT
    {
        public JXC_REPORT()
        { }
        #region Model
        private string _pono;
        private string _prono;
        private DateTime _rutime;
        private string _supplier;
        private string _goodinfo;
        private decimal? _goodnum;
        private decimal? _goodsellprice;
        private decimal? _goodselltotal;
        private decimal? _goodprice;
        private decimal? _goodtotal;
        private decimal? _t_goodnums;
        private decimal? _t_goodtotalchas;
        private decimal? _maoli;
        private string _fpno;
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
        public string ProNo
        {
            set { _prono = value; }
            get { return _prono; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime RuTime
        {
            set { _rutime = value; }
            get { return _rutime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string goodInfo
        {
            set { _goodinfo = value; }
            get { return _goodinfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? GoodNum
        {
            set { _goodnum = value; }
            get { return _goodnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? GoodSellPrice
        {
            set { _goodsellprice = value; }
            get { return _goodsellprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? goodSellTotal
        {
            set { _goodselltotal = value; }
            get { return _goodselltotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? GoodPrice
        {
            set { _goodprice = value; }
            get { return _goodprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? goodTotal
        {
            set { _goodtotal = value; }
            get { return _goodtotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? t_GoodNums
        {
            set { _t_goodnums = value; }
            get { return _t_goodnums; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? t_GoodTotalChas
        {
            set { _t_goodtotalchas = value; }
            get { return _t_goodtotalchas; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? maoli
        {
            set { _maoli = value; }
            get { return _maoli; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FPNo
        {
            set { _fpno = value; }
            get { return _fpno; }
        }

        public string POName { get; set; }
        #endregion Model

        public string PoType { get; set; }

    }
}
