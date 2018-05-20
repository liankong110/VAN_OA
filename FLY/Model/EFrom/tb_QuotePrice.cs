using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.EFrom
{

    /// <summary>
    /// tb_QuotePrice:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tb_QuotePrice
    {
        public tb_QuotePrice()
        { }
        #region Model
        private int _id;
        private string _quoteno;
        private string _guestname;
        private string _guestno;
        private DateTime _quotedate;
        private string _resultguestname;
        private string _resultguestno;
        private string _paystyle;
        private string _guestnametoinv;
        private string _contactpertoinv;
        private string _teltoinv;
        private string _addresstoinv;
        private string _invoheader;
        private string _invcontactper;
        private string _invaddress;
        private string _invtel;
        private string _nashuiper;
        private string _brandno;
        private string _guestnametofa;
        private string _contactpertofa;
        private string _teltofa;
        private string _addresstofa;
        private string _buessname;
        private string _buessemail;
        private string _comtel;
        private string _comchuanzhen;
        private string _combustel;
        private string _comname;
        private string _nashuino;
        private string _address;
        private string _combrand;
        private int _createuser;
        private DateTime _createtime;

        public string CreateUserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 报价单
        /// </summary>
        public string QuoteNo
        {
            set { _quoteno = value; }
            get { return _quoteno; }
        }
        /// <summary>
        /// 买方名称
        /// </summary>
        public string GuestName
        {
            set { _guestname = value; }
            get { return _guestname; }
        }
        /// <summary>
        /// 买方编码
        /// </summary>
        public string GuestNo
        {
            set { _guestno = value; }
            get { return _guestno; }
        }
        /// <summary>
        /// 报价单日期
        /// </summary>
        public DateTime QuoteDate
        {
            set { _quotedate = value; }
            get { return _quotedate; }
        }
        /// <summary>
        /// 最终用户名称
        /// </summary>
        public string ResultGuestName
        {
            set { _resultguestname = value; }
            get { return _resultguestname; }
        }
        /// <summary>
        /// 最终用户编码
        /// </summary>
        public string ResultGuestNo
        {
            set { _resultguestno = value; }
            get { return _resultguestno; }
        }
        /// <summary>
        /// 付款方式
        /// </summary>
        public string PayStyle
        {
            set { _paystyle = value; }
            get { return _paystyle; }
        }
        /// <summary>
        /// 买方名称-收获地址
        /// </summary>
        public string GuestNameToInv
        {
            set { _guestnametoinv = value; }
            get { return _guestnametoinv; }
        }
        /// <summary>
        /// 联系人-收获地址
        /// </summary>
        public string ContactPerToInv
        {
            set { _contactpertoinv = value; }
            get { return _contactpertoinv; }
        }
        /// <summary>
        /// 电话-收获地址
        /// </summary>
        public string telToInv
        {
            set { _teltoinv = value; }
            get { return _teltoinv; }
        }
        /// <summary>
        /// 地址-收获地址
        /// </summary>
        public string AddressToInv
        {
            set { _addresstoinv = value; }
            get { return _addresstoinv; }
        }
        /// <summary>
        /// 开票抬头
        /// </summary>
        public string InvoHeader
        {
            set { _invoheader = value; }
            get { return _invoheader; }
        }
        /// <summary>
        /// 联系人-开票信息
        /// </summary>
        public string InvContactPer
        {
            set { _invcontactper = value; }
            get { return _invcontactper; }
        }
        /// <summary>
        /// 注册地址-开票信息
        /// </summary>
        public string InvAddress
        {
            set { _invaddress = value; }
            get { return _invaddress; }
        }
        /// <summary>
        /// 注册电话-开票信息
        /// </summary>
        public string InvTel
        {
            set { _invtel = value; }
            get { return _invtel; }
        }
        /// <summary>
        /// 纳税人登记号
        /// </summary>
        public string NaShuiPer
        {
            set { _nashuiper = value; }
            get { return _nashuiper; }
        }
        /// <summary>
        /// 开户行/帐号
        /// </summary>
        public string brandNo
        {
            set { _brandno = value; }
            get { return _brandno; }
        }
        /// <summary>
        /// 买方名称-发票邮件地址
        /// </summary>
        public string GuestNameTofa
        {
            set { _guestnametofa = value; }
            get { return _guestnametofa; }
        }
        /// <summary>
        /// 联系人-发票邮件地址
        /// </summary>
        public string ContactPerTofa
        {
            set { _contactpertofa = value; }
            get { return _contactpertofa; }
        }
        /// <summary>
        /// 电话-发票邮件地址
        /// </summary>
        public string telTofa
        {
            set { _teltofa = value; }
            get { return _teltofa; }
        }
        /// <summary>
        /// 地址-发票邮件地址
        /// </summary>
        public string AddressTofa
        {
            set { _addresstofa = value; }
            get { return _addresstofa; }
        }
        /// <summary>
        /// 业务代表
        /// </summary>
        public string BuessName
        {
            set { _buessname = value; }
            get { return _buessname; }
        }
        /// <summary>
        /// 业务代表电邮
        /// </summary>
        public string BuessEmail
        {
            set { _buessemail = value; }
            get { return _buessemail; }
        }
        /// <summary>
        /// 公司电话
        /// </summary>
        public string ComTel
        {
            set { _comtel = value; }
            get { return _comtel; }
        }
        /// <summary>
        /// 公司传真
        /// </summary>
        public string ComChuanZhen
        {
            set { _comchuanzhen = value; }
            get { return _comchuanzhen; }
        }
        /// <summary>
        /// 公司传真
        /// </summary>
        public string ComBusTel
        {
            set { _combustel = value; }
            get { return _combustel; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string ComName
        {
            set { _comname = value; }
            get { return _comname; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string NaShuiNo
        {
            set { _nashuino = value; }
            get { return _nashuino; }
        }
        /// <summary>
        /// 地址/电话
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 地址/电话
        /// </summary>
        public string ComBrand
        {
            set { _combrand = value; }
            get { return _combrand; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

        /// <summary>
        /// 总价
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// 质量保证
        /// </summary>
        public string ZLBZ { get; set; }
        /// <summary>
        /// 以上报价
        /// </summary>
        public string YSBJ { get; set; }
        /// <summary>
        /// 服务保修等级
        /// </summary>
        public string FWBXDJ { get; set; }
        /// <summary>
        /// 交付期
        /// </summary>
        public string JFQ { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public int GuestId { get; set; }

        /// <summary>
        /// 概要
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 人工费用
        /// </summary>
        public decimal LaborCost { get; set; }
        /// <summary>
        /// 工程税
        /// </summary>
        public decimal EngineeringTax { get; set; }
        /// <summary>
        /// 类型 1.含税格式  缺省  2.不含税格式  3.工程格式 
        /// </summary>
        public int QPType { get; set; }
        public string QPTypeString
        {
            get
            {
                if (QPType == 1)
                {
                    return "含税";
                }
                if (QPType == 2)
                {
                    return "不含税";
                }

                return "工程";
            }
        }
        /// <summary>
        /// 含品牌
        /// </summary>
        public bool IsBrand { get; set; }
        /// <summary>
        /// 含产地
        /// </summary>
        public bool IsProduct { get; set; }
        /// <summary>
        /// 含备注
        /// </summary>
        public bool IsRemark { get; set; }
        /// <summary>
        /// 利率
        /// </summary>
        public decimal LIlv { get; set; }

        /// <summary>
        /// 最终优惠价
        /// </summary>
        public decimal LastYH { get; set; }

        /// <summary>
        /// 是否有优惠价
        /// </summary>
        public bool IsYH { get; set; }

        public string AllName { get; set; }
        /// <summary>
        /// 是否打印水印
        /// </summary>
        public bool IsShuiYin { get; set; }
        /// <summary>
        /// 是否打印盖章
        /// </summary>
        public bool IsGaiZhang { get; set; }
    }
}
