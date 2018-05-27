using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using VAN_OA.Model.EFrom;

namespace VAN_OA.Model.JXC
{

    /// <summary>
    /// TB_POOrder:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CG_POOrder : CommModel
    {
        public CG_POOrder()
        { }

        public string FPTotal { get; set; }
        #region Model
        private string _cremark;
        /// <summary>
        /// 
        /// </summary>
        public string cRemark
        {
            set { _cremark = value; }
            get { return _cremark; }
        }
        private string caiGou;
        public string CaiGou
        {
            get { return caiGou; }
            set { caiGou = value; }
        }
        private int _id;
        private int _appname;
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
        #endregion Model
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        private string loginName;

        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }

        private string _filename;
        private byte[] _fileno;
        private string _filetype;

        /// <summary>
        /// 
        /// </summary>
        public string fileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public byte[] fileNo
        {
            set { _fileno = value; }
            get { return _fileno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fileType
        {
            set { _filetype = value; }
            get { return _filetype; }
        }


        private int _guestid;
        private string _guestno;
        private string _guestname;
        private string _ae;
        private string _inside;
        private string _pono;
        private string _poname;
        private DateTime _podate;
        private decimal _pototal;
        private string _popaystype;

        /// <summary>
        /// 客户ID
        /// </summary>
        public int GuestId
        {
            set { _guestid = value; }
            get { return _guestid; }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public string GuestNo
        {
            set { _guestno = value; }
            get { return _guestno; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string GuestName
        {
            set { _guestname = value; }
            get { return _guestname; }
        }
        /// <summary>
        /// AE
        /// </summary>
        public string AE
        {
            set { _ae = value; }
            get { return _ae; }
        }
        /// <summary>
        /// INSIDE
        /// </summary>
        public string INSIDE
        {
            set { _inside = value; }
            get { return _inside; }
        }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string PONo
        {
            set { _pono = value; }
            get { return _pono; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string POName
        {
            set { _poname = value; }
            get { return _poname; }
        }
        /// <summary>
        /// 项目日期
        /// </summary>
        public DateTime PODate
        {
            set { _podate = value; }
            get { return _podate; }
        }
        /// <summary>
        /// 项目金额
        /// </summary>
        public decimal POTotal
        {
            set { _pototal = value; }
            get { return _pototal; }
        }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string POPayStype
        {
            set { _popaystype = value; }
            get { return _popaystype; }
        }


        /// <summary>
        /// 是否追加
        /// </summary>
        public int IFZhui { get; set; }

        /// <summary>
        /// 是否为特殊项目
        /// </summary>
        public bool IsSpecial { get; set; }

        public decimal AEPer { get; set; }
        public decimal INSIDEPer { get; set; }

        public decimal ZhangQiTotal { get; set; }




        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal TuiTotal { get; set; }

        /// <summary>
        /// 已经付款金额
        /// </summary>
        public decimal sumTotal { get; set; }


        /// <summary>
        /// 未付款金额
        /// </summary>
        public decimal WeiTotal { get; set; }


        /// <summary>
        /// 项目现在的状态
        /// </summary>
        public string POStatue { get; set; }


        /// <summary>
        /// 项目已交付的状态
        /// </summary>
        public string POStatue2 { get; set; }


        /// <summary>
        /// 项目已开票的状态
        /// </summary>
        public string POStatue3 { get; set; }


        /// <summary>
        /// 项目已结清的状态
        /// </summary>
        public string POStatue4 { get; set; }


        /// <summary>
        /// 项目出库单签回状态
        /// </summary>
        public string POStatue5 { get; set; }

        /// <summary>
        /// 项目发票单签回状态
        /// </summary>
        public string POStatue6 { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string PORemark { get; set; }

        /// <summary>
        /// 初始状态
        /// </summary>
        public const string ConPOStatue1 = "初始状态";
        /// <summary>
        /// 已交付
        /// </summary>
        public const string ConPOStatue2 = "已交付";
        /// <summary>
        /// 已开票
        /// </summary>
        public const string ConPOStatue3 = "已开票";
        /// <summary>
        /// 已结清
        /// </summary>
        public const string ConPOStatue4 = "已结清";

        /// <summary>
        /// 出库签回
        /// </summary>
        public const string ConPOStatue5 = "出库签回";

        /// <summary>
        /// 出库部分签回
        /// </summary>
        public const string ConPOStatue5_1 = "部分签回";

        /// <summary>
        /// 发票签回
        /// </summary>
        public const string ConPOStatue6 = "发票签回";

        /// <summary>
        /// 发票发票签回
        /// </summary>
        public const string ConPOStatue6_1 = "部分签回";





        /// <summary>
        /// 前台使用 是否销售已经完成
        /// </summary>
        public bool IfCanSell { get; set; }


        /// <summary>
        /// 判断一下：当时 项目原金额+项目追加金额-销售退货金额 的金额 如果=0
        /// </summary>
        public decimal ifZenoPoInfo { get; set; }

        /// <summary>
        /// 含税状态
        /// </summary>
        public bool IsPoFax { get; set; }

        /// <summary>
        /// 发票类型
        /// </summary>
        public string FpType { get; set; }

        /// <summary>
        /// 发票税率
        /// </summary>
        public decimal FpTax { get; set; }

        /// <summary>
        ///  关闭状态   缺省这个字段 是 未关闭。1是关闭，0 是未关闭 默认0
        /// </summary>
        public bool IsClose { get; set; }

        /// <summary>
        /// 客户类型
        /// </summary>
        public string GuestType { get; set; }

        /// <summary>
        /// 结算系数
        /// </summary>
        public decimal GuestXiShu { get; set; }

        /// <summary>
        /// 客户属性
        /// </summary>
        public int GuestPro { get; set; }

        /// <summary>
        /// 2：工程类   1.零售类
        /// </summary>
        public int POType { get; set; }
         
        public string GuestProString
        {
            get
            {                 
                return VAN_OA.BaseInfo.GuestProBaseInfoList.GetGestProInfo(GuestPro);

            }
        }

        public string POTypeString { get; set; }
       
        /// <summary>
        /// 激励系数
        /// </summary>
        public decimal JiLiXiShu { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected { get; set; }

        public bool JieIsSelected { get; set; }

        public decimal InvoiceTotal { get; set; }

        public decimal BILI { get; set; }
        /// <summary>
        /// 总成本
        /// </summary>
        public decimal GoodTotal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MaoliTotal { get; set; }

        /// <summary>
        /// 模型名称
        /// </summary>
        public string Model { get; set; }
    }

}
