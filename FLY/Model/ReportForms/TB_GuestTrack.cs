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

namespace VAN_OA.Model.ReportForms
{
    /// <summary>
    /// 客户联系跟踪表
    /// </summary>
    [Serializable]
    public partial class TB_GuestTrack
    {

        public TB_GuestTrack()
        { }
        #region Model

        private string _prono;
        /// <summary>
        /// 单据号
        /// </summary>
        public string ProNo
        {
            set { _prono = value; }
            get { return _prono; }
        }
        private int _id;
        private DateTime _time;
        private string _guestname;
        private string _phone;
        private string _likeman;
        private string _job;
        private string _foxoremail;
        private bool _ifsave;
        private string _qqmsn;
        private string _fristmeet;
        private string _secondmeet;
        private string _facemeet;
        private decimal? _price;
        private bool _ifsuccess;
        private string _myappraise;
        private string _manappraise;
        private int _createuser;
        private DateTime _createtime;


        public string Save_Name
        {
            get
            {
                if (IfSave)
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
        }

        public string Success_Name
        {
            get
            {
                if (IfSuccess)
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
        }
        /// <summary>
        /// 主键
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Time
        {
            set { _time = value; }
            get { return _time; }
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
        /// 电话/手机
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string LikeMan
        {
            set { _likeman = value; }
            get { return _likeman; }
        }
        /// <summary>
        /// 职务
        /// </summary>
        public string Job
        {
            set { _job = value; }
            get { return _job; }
        }
        /// <summary>
        /// 传真或邮箱
        /// </summary>
        public string FoxOrEmail
        {
            set { _foxoremail = value; }
            get { return _foxoremail; }
        }
        /// <summary>
        /// 是否留资料
        /// </summary>
        public bool IfSave
        {
            set { _ifsave = value; }
            get { return _ifsave; }
        }
        /// <summary>
        /// QQ/MSN联系
        /// </summary>
        public string QQMsn
        {
            set { _qqmsn = value; }
            get { return _qqmsn; }
        }
        /// <summary>
        /// 1次约见
        /// </summary>
        public string FristMeet
        {
            set { _fristmeet = value; }
            get { return _fristmeet; }
        }
        /// <summary>
        /// 2次约见
        /// </summary>
        public string SecondMeet
        {
            set { _secondmeet = value; }
            get { return _secondmeet; }
        }
        /// <summary>
        /// 询价
        /// </summary>
        public string FaceMeet
        {
            set { _facemeet = value; }
            get { return _facemeet; }
        }
        /// <summary>
        /// 询价
        /// </summary>
        public decimal? Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 成单
        /// </summary>
        public bool IfSuccess
        {
            set { _ifsuccess = value; }
            get { return _ifsuccess; }
        }
        /// <summary>
        /// 自我评价
        /// </summary>
        public string MyAppraise
        {
            set { _myappraise = value; }
            get { return _myappraise; }
        }
        /// <summary>
        /// 主管评价
        /// </summary>
        public string ManAppraise
        {
            set { _manappraise = value; }
            get { return _manappraise; }
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

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        #endregion Model
        private decimal? _aeper;
        private decimal? _insideper;
        /// <summary>
        /// 
        /// </summary>
        public decimal? AEPer
        {
            set { _aeper = value; }
            get { return _aeper; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? INSIDEPer
        {
            set { _insideper = value; }
            get { return _insideper; }
        }

        /// <summary>
        /// 客户类型
        /// </summary>
        public string MyGuestType { get; set; }

        /// <summary>
        /// 客户属性
        /// </summary>
        public int MyGuestPro { get; set; }

        public string MyGuestProString
        {
            get
            {               
                return VAN_OA.BaseInfo.GuestProBaseInfoList.GetGestProInfo(MyGuestPro);

            }
        }

        private string _guestid;
        private string _guestaddress;
        private string _guesthttp;
        private string _guestshui;
        private string _guestgong;
        private string _guestbrandno;
        private string _guestbrandname;
        private int? _ae;
        private int? _inside;
        private decimal? _guesttotal;
        private decimal? _guestlirun;
        private decimal? _guestdays;
        private string _remark;
        private string _insideremark;
        /// <summary>
        /// INSIDE备注
        /// </summary>
        public string INSIDERemark
        {
            set { _insideremark = value; }
            get { return _insideremark; }
        }

        /// <summary>
        /// AE
        /// </summary>
        public string AEName { get; set; }
        /// <summary>
        /// INSIDE
        /// </summary>
        public string INSIDEName { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public string GuestId
        {
            set { _guestid = value; }
            get { return _guestid; }
        }
        /// <summary>
        /// 联系人地址
        /// </summary>
        public string GuestAddress
        {
            set { _guestaddress = value; }
            get { return _guestaddress; }
        }
        /// <summary>
        /// 联系人网址
        /// </summary>
        public string GuestHttp
        {
            set { _guesthttp = value; }
            get { return _guesthttp; }
        }
        /// <summary>
        /// 客户税务登记号
        /// </summary>
        public string GuestShui
        {
            set { _guestshui = value; }
            get { return _guestshui; }
        }
        /// <summary>
        /// 客户工商注册号
        /// </summary>
        public string GuestGong
        {
            set { _guestgong = value; }
            get { return _guestgong; }
        }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string GuestBrandNo
        {
            set { _guestbrandno = value; }
            get { return _guestbrandno; }
        }
        /// <summary>
        /// 开户行
        /// </summary>
        public string GuestBrandName
        {
            set { _guestbrandname = value; }
            get { return _guestbrandname; }
        }
        /// <summary>
        /// AE
        /// </summary>
        public int? AE
        {
            set { _ae = value; }
            get { return _ae; }
        }
        /// <summary>
        /// INSIDE
        /// </summary>
        public int? INSIDE
        {
            set { _inside = value; }
            get { return _inside; }
        }
        /// <summary>
        /// 销售额
        /// </summary>
        public decimal? GuestTotal
        {
            set { _guesttotal = value; }
            get { return _guesttotal; }
        }
        /// <summary>
        /// 利润
        /// </summary>
        public decimal? GuestLiRun
        {
            set { _guestlirun = value; }
            get { return _guestlirun; }
        }
        /// <summary>
        /// 收款期/天
        /// </summary>
        public decimal? GuestDays
        {
            set { _guestdays = value; }
            get { return _guestdays; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        private string _yearno;
        private string _quartno;
        /// <summary>
        /// 年
        /// </summary>
        public string YearNo
        {
            set { _yearno = value; }
            get { return _yearno; }
        }
        /// <summary>
        /// 季度
        /// </summary>
        public string QuartNo
        {
            set { _quartno = value; }
            get { return _quartno; }
        }


        public bool IsSpecial { get; set; }

        public string YearAndMonth { get; set; }

        /// <summary>
        /// 客户简称
        /// </summary>
        public string SimpGuestName { get; set; }
    }
}
