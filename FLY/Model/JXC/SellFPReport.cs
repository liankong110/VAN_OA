using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// SellFPReport:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SellFPReport
    {
        public SellFPReport()
        { }
        #region Model
        private string _pono;
        private string _ae;
        private string _guestname;
        private int? _goodid;
        private string _goodno;
        private string _goodname;
        private string _goodspec;
        private decimal? _totalnum;
        private decimal? _avgsellprice;
        private decimal? _avglastprice;
        private decimal? _pototal;
        private DateTime? _podate;
        private string _fptotal;
        private decimal? _hadfptotal;
        private DateTime? _rutime;
        private decimal? _goodsellprice;
        private int? _diffdate;
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
        public string AE
        {
            set { _ae = value; }
            get { return _ae; }
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
        public int? GoodId
        {
            set { _goodid = value; }
            get { return _goodid; }
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
        public decimal? totalNum
        {
            set { _totalnum = value; }
            get { return _totalnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? avgSellPrice
        {
            set { _avgsellprice = value; }
            get { return _avgsellprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? avgLastPrice
        {
            set { _avglastprice = value; }
            get { return _avglastprice; }
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
        public DateTime? PODate
        {
            set { _podate = value; }
            get { return _podate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FPTotal
        {
            set { _fptotal = value; }
            get { return _fptotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? hadFpTotal
        {
            set { _hadfptotal = value; }
            get { return _hadfptotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? RuTime
        {
            set { _rutime = value; }
            get { return _rutime; }
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
        public int? diffDate
        {
            set { _diffdate = value; }
            get { return _diffdate; }
        }
        #endregion Model

        public string OutProNo { get; set; }

        public string POName { get; set; }

        public bool IsPoFax { get; set; }

        public int Id { get; set; }

        public decimal SellInNums { get; set; }
        /// <summary>
        /// 出库总价 销售数量×出库单价
        /// </summary>
        public decimal? SellOutTotal { get { return GoodSellPrice * totalNum; } }
        public string GoodAreaNumber { get; set; }


        public decimal? TotalAvgPrice
        {
            get; set;
        }
    }
}
