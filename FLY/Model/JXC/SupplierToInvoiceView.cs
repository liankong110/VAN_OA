using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// SupplierToInvoiceView:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SupplierToInvoiceView
    {
        public SupplierToInvoiceView()
        {
            IfCheck = true;

        }
        #region Model
        private string _prono;
        private DateTime _rutime;
        private string _guestname;
        private string _housename;
        private string _pono;
        private string _poname;
        private string _goodno;
        private string _goodname;
        private string _goodtypesmname;
        private string _goodspec;
        private string _goodunit;
        private decimal? _goodnum;
        private decimal? _suppliertuigoodnum;
        private decimal _lastgoodnum;
        private decimal _goodprice;
        private string _doper;
        private string _supplierfpno;
        private string _chcekprono;
        private string _status;
        private int? _supplierinvoicestatues;
        private DateTime? _supplierinvoicedate;

        public int Ids { get; set; }
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
        public string GuestName
        {
            set { _guestname = value; }
            get { return _guestname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string houseName
        {
            set { _housename = value; }
            get { return _housename; }
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
        public string GoodTypeSmName
        {
            set { _goodtypesmname = value; }
            get { return _goodtypesmname; }
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
        public string GoodUnit
        {
            set { _goodunit = value; }
            get { return _goodunit; }
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
        public decimal? supplierTuiGoodNum
        {
            set { _suppliertuigoodnum = value; }
            get { return _suppliertuigoodnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal lastGoodNum
        {
            set { _lastgoodnum = value; }
            get { return _lastgoodnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal GoodPrice
        {
            set { _goodprice = value; }
            get { return _goodprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DoPer
        {
            set { _doper = value; }
            get { return _doper; }
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
        public string ChcekProNo
        {
            set { _chcekprono = value; }
            get { return _chcekprono; }
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
        public int? SupplierInvoiceStatues
        {
            set { _supplierinvoicestatues = value; }
            get { return _supplierinvoicestatues; }
        }

        public string isZhiFu { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? SupplierInvoiceDate
        {
            set { _supplierinvoicedate = value; }
            get { return _supplierinvoicedate; }
        }

        public decimal SupplierInvoicePrice { get; set; }
        public decimal SupplierInvoiceNum { get; set; }

        public decimal LastTotal { get; set; }
        public decimal SupplierInvoiceTotal { get; set; }
        #endregion Model

        public string busType { get; set; }

        public int payId { get; set; }

        public int payIds { get; set; }

        public string InvProNo { get; set; }

        public DateTime CreteTime { get; set; }


        public string PayType_Id { get; set; }
        public bool IsYuFu { get; set; }

        public string SupplierProNo { get; set; }

        public decimal jianTotal { get; set; }

        public decimal zhengTotal { get; set; }

        public string POGuestName { get; set; }

        public string POAE { get; set; }


        public bool IfRuKu { get; set; }


        public decimal HadSupplierInvoiceTotal { get; set; }

        public bool IsHanShui { get; set; }


        /// <summary>
        /// 实际支付
        /// </summary>
        public decimal ActPay { get; set; }


        /// <summary>
        /// 是否结清
        /// 
        /// 任何记录的 结清 初始 都是 0
        ///事前退货的支付单 审批通过 结清 =1   就是6.A
        ///事后退货的支付单，审批通过 实际支付为负数 的 结清=2  就是6.B
        ///事后扣减成功 ，审批通过，这几条实际支付为负数的 记录 的结清=3  就是6.C
        ///
        ///邮件的正文已经修改正确了，见下面。
        ///结清=3的记录其实就是结清=2的记录 ，因此结清=3 的记录不用参加 1-4的逻辑状态运算。
        ///
        ///结清字段为1，就是已结清。
        ///结清字段为2.  就是未结清
        /// </summary>
        public int RePayClear { get; set; }

        public string RePayClearString { get; set; }


        /// <summary>
        /// 负数合计
        /// </summary>
        public decimal FuShuTotal { get; set; }

        /// <summary>
        /// 数据填写是否参加 逻辑判断检查 默认都需要
        /// </summary>
        private bool ifCheck;
        public bool IfCheck
        {
            get { return ifCheck; }
            set { ifCheck = value; }
        }

        /// <summary>
        /// 支付状态
        /// 0 未支付
        /// 1 未结清
        /// 2 已支付
        /// </summary>
        public decimal IsPayStatus { get; set; }

        public string CaiTuiProNo { get; set; }

        public string CaiFpType { get; set; }

        public int IsHeBing { get; set; }

        public string IsHeBingString { get; set; }
        public DateTime T { get; set; }
        /// <summary>
        /// 剩余金额
        /// </summary>
        public decimal ResultTotal { get; set; }

        public string SupplierAllName { get; set; }
        public int Good_IsHanShui { get; set; }

        /// <summary>
        /// 发票时间
        /// </summary>
        public DateTime? SupplierFpDate { get; set; }

        public bool IsTemp { get; set; }

        public bool IsShow { get; set; }

        /// <summary>
        /// 是否是后退款
        /// </summary>
        public bool IsHouTuiKui { get; set; }

        public decimal LastTruePrice { get; set; }

        public int GoodId { get; set; }

        public string CaiProNo { get; set; }

        /// <summary>
        /// 回款率
        /// </summary>
        public decimal HuiKuanLiLv { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        public decimal InHouseGoodNum { get; set; }

        /// <summary>
        /// 出价合计=实际支付×采购单价/实采单价
        /// </summary>
        public decimal LastPayTotal { get; set; }

        public string PORemark { get; set; }

        /// <summary>
        /// 剩余支价=剩余金额*采购单价/实采单价
        /// </summary>
        public decimal ShengYuZhiJia
        {
            get
            {
                if (LastTruePrice == 0)
                {
                    return 0;
                }
                return ResultTotal * GoodPrice / LastTruePrice;

            }
        }

        public string GuestType { get; set; }

        public int GuestPro { get; set; }
        public string Peculiarity { get; internal set; }

        public string Province { get; set; }
        public string City { get; set; }
    }
}
