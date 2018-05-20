using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VAN_OA.Model.JXC;

namespace VAN_OA.Model.ReportForms
{
    /// <summary>
    /// vAllCaiOrderList:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class vAllCaiOrderList : GoodModel
    {
        public vAllCaiOrderList()
        { }
        #region Model
        private int _id;
        private int _appname;
        private string _caigou;
        private string _cremark;
        private string _prono;
        private string _pono;
        private string _bustype;
        private string _poname;
        private DateTime _podate;
        private string _popaystype;
        private decimal _pototal;
        private string _guestname;
        private string _ae;
        private string _inside;
        private string _guestno;
        private string _status;
        private string _cg_prono;
        private DateTime _time;
        private string _invname;
        private decimal _num;
        private string _unit;
        private decimal _costprice;
        private decimal _sellprice;
        private decimal _othercost;
        private DateTime? _totime;
        private decimal _profit;
        private int _goodid;
        private int _cg_poordersid;
        private DateTime _caitime;
        private string _supplier;
        private decimal _supperprice;
        private string _updateuser;
        private string _idea;
        private string _supplier1;
        private decimal _supperprice1;
        private string _supplier2;
        private decimal _supperprice2;
        private decimal _finprice1;
        private decimal _finprice2;
        private decimal _finprice3;
        private bool _cbifdefault1;
        private bool _cbifdefault2;
        private bool _cbifdefault3;
        private string _lastsupplier;
        private decimal _lastprice;
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
        public int AppName
        {
            set { _appname = value; }
            get { return _appname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CaiGou
        {
            set { _caigou = value; }
            get { return _caigou; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string cRemark
        {
            set { _cremark = value; }
            get { return _cremark; }
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
        public string PONo
        {
            set { _pono = value; }
            get { return _pono; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BusType
        {
            set { _bustype = value; }
            get { return _bustype; }
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
        public DateTime PODate
        {
            set { _podate = value; }
            get { return _podate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string POPayStype
        {
            set { _popaystype = value; }
            get { return _popaystype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal POTotal
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
        public string INSIDE
        {
            set { _inside = value; }
            get { return _inside; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GuestNo
        {
            set { _guestno = value; }
            get { return _guestno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CG_ProNo
        {
            set { _cg_prono = value; }
            get { return _cg_prono; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time
        {
            set { _time = value; }
            get { return _time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InvName
        {
            set { _invname = value; }
            get { return _invname; }
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
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CostPrice
        {
            set { _costprice = value; }
            get { return _costprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SellPrice
        {
            set { _sellprice = value; }
            get { return _sellprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal OtherCost
        {
            set { _othercost = value; }
            get { return _othercost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ToTime
        {
            set { _totime = value; }
            get { return _totime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Profit
        {
            set { _profit = value; }
            get { return _profit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GoodId
        {
            set { _goodid = value; }
            get { return _goodid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CG_POOrdersId
        {
            set { _cg_poordersid = value; }
            get { return _cg_poordersid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CaiTime
        {
            set { _caitime = value; }
            get { return _caitime; }
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
        public decimal SupperPrice
        {
            set { _supperprice = value; }
            get { return _supperprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UpdateUser
        {
            set { _updateuser = value; }
            get { return _updateuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Idea
        {
            set { _idea = value; }
            get { return _idea; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Supplier1
        {
            set { _supplier1 = value; }
            get { return _supplier1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SupperPrice1
        {
            set { _supperprice1 = value; }
            get { return _supperprice1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Supplier2
        {
            set { _supplier2 = value; }
            get { return _supplier2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SupperPrice2
        {
            set { _supperprice2 = value; }
            get { return _supperprice2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal FinPrice1
        {
            set { _finprice1 = value; }
            get { return _finprice1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal FinPrice2
        {
            set { _finprice2 = value; }
            get { return _finprice2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal FinPrice3
        {
            set { _finprice3 = value; }
            get { return _finprice3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool cbifDefault1
        {
            set { _cbifdefault1 = value; }
            get { return _cbifdefault1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool cbifDefault2
        {
            set { _cbifdefault2 = value; }
            get { return _cbifdefault2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool cbifDefault3
        {
            set { _cbifdefault3 = value; }
            get { return _cbifdefault3; }
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
        public decimal lastPrice
        {
            set { _lastprice = value; }
            get { return _lastprice; }
        }
        #endregion Model

        public decimal SellTotal { get; set; }
        public decimal YiLiTotal { get; set; }
        public decimal CostTotal { get; set; }
        public decimal Total1 { get; set; }
        public decimal Total2 { get; set; }
        public decimal Total3 { get; set; }
        /// <summary>
        /// 利润
        /// </summary>
        private decimal? caiLiRun;
        public decimal? CaiLiRun
        {
            get { return caiLiRun; }
            set { caiLiRun = value; }
        }

        public bool IsHanShui { get; set; }
        public int ids { get; set; }


        public string CaiFpType { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? LastTime { get; set; }

        public decimal? TruePrice1 { get; set; }
        public decimal? TruePrice2 { get; set; }
        public decimal? TruePrice3 { get; set; }

        public decimal? LastTruePrice { get; set; }

        private decimal? _lastTrueTotal;
        /// <summary>
        /// 最总实际总价合计
        /// </summary>
        public decimal? LastTotal { get { return _lastTrueTotal; } set { _lastTrueTotal = value; } }
    }
}
