using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// RuSellReport:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class NoSellAndCaiGoods
    {
        public NoSellAndCaiGoods()
        { }
        #region Model
        public int Id { get; set; }
        private string _pono;
        private string _ae;
        private string _guestname;
        private int? _goodid;
        private string _goodno;
        private string _goodname;
        private string _goodspec;
        private decimal? _totalnum;
        private decimal? _avggoodprice;
        private decimal? _avgsellprice;
        private DateTime? _minrutime;
        private DateTime? _minpodate;
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
        public decimal? avgGoodPrice
        {
            set { _avggoodprice = value; }
            get { return _avggoodprice; }
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
        public DateTime? minRuTime
        {
            set { _minrutime = value; }
            get { return _minrutime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? minPODate
        {
            set { _minpodate = value; }
            get { return _minpodate; }
        }
        #endregion Model

        public decimal HouseNum { get; set; }
        public decimal OutNum { get; set; }

        public decimal RuChuNum { get; set; }


        public DateTime CaiDate { get; set; }

        public decimal CaIKuNum { get; set; }

        public decimal CaiGouNum { get; set; }



        public decimal AllCaiNum { get; set; }

        public decimal KuCaiNum { get; set; }

        public decimal WaiCaiNum { get; set; }

        public decimal SellTuiNum { get; set; }

        public decimal CaiTuiNum { get; set; }


        public int IsHanShui { get; set; }
        public string GoodAreaNumber { get; set; }

        public string Supplier { get; set; }
        public string POName { get; internal set; }
        public string GoodTypeSmName { get; internal set; }
        /// <summary>
        /// 如该项目该商品的采购检验单 中的备注 含有直发 两个字（去空格），这列就显示 直发，不含有直发 两个字（去空格），这列就不显示
        /// </summary>
        public string ZHIFA { get; set; }
    }
}
