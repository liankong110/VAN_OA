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

namespace VAN_OA.Model.EFrom
{
    public class Tb_DispatchList
    {
        public Tb_DispatchList()
        { }
        #region Model
        private int _id;
        private int _userid;
        private DateTime _evtime;
        private DateTime _createtime;
        private string _cardno;
        private string _busfromaddress;
        private string _bustoaddress;
        private decimal? _bustotal;
        private bool _iftexi;
        private bool _ifbus;
        private DateTime? _busfromtime;
        private DateTime? _bustotime;
        private string _repastaddress;
        private decimal? _repasttotal;
        private decimal? _repastpernum;
        private string _repastpers;
        private string _repasttype;
        private string _hoteladdress;
        private string _hotelname;
        private decimal? _hoteltotal;
        private string _hoteltype;
        private string _oilfromaddress;
        private string _oiltoaddress;
        private decimal? _oillicheng;
        private decimal? _oiltotal;
        private decimal? _oilxishu;
        private string _guobeginaddress;
        private string _guotoaddress;
        private decimal? _guototal;
        private bool _postfrom;
        private string _postfromaddress;
        private bool _postto;
        private string _posttoaddress;
        private decimal? _posttotal;
        private string _pocontext;
        private decimal? _pototal;
        private string _othercontext;
        private decimal? _othertotal;


        private string _busremark;
        private string _repastremark;
        private string _hotelremark;
        private string _oilremark;
        private string _guoremark;
        private string _postremark;
        private string _poremark;
        private string _otherremark;
        private string _postno;
        private string _postcompany;
        private string _postcontext;
        private string _posttoper;


        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 事件发生日期
        /// </summary>
        public DateTime EvTime
        {
            set { _evtime = value; }
            get { return _evtime; }
        }
        /// <summary>
        /// 填写日期
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 报销序号
        /// </summary>
        public string CardNo
        {
            set { _cardno = value; }
            get { return _cardno; }
        }
        /// <summary>
        /// 公交费 地点开始
        /// </summary>
        public string BusFromAddress
        {
            set { _busfromaddress = value; }
            get { return _busfromaddress; }
        }
        /// <summary>
        /// 公交费 地点结束
        /// </summary>
        public string BusToAddress
        {
            set { _bustoaddress = value; }
            get { return _bustoaddress; }
        }
        /// <summary>
        /// 公交费 金额
        /// </summary>
        public decimal? BusTotal
        {
            set { _bustotal = value; }
            get { return _bustotal; }
        }
        /// <summary>
        /// 公交费 是否打的
        /// </summary>
        public bool IfTexi
        {
            set { _iftexi = value; }
            get { return _iftexi; }
        }
        /// <summary>
        /// 公交费 是否公交
        /// </summary>
        public bool IfBus
        {
            set { _ifbus = value; }
            get { return _ifbus; }
        }
        /// <summary>
        /// 公交费 开始时间
        /// </summary>
        public DateTime? BusFromTime
        {
            set { _busfromtime = value; }
            get { return _busfromtime; }
        }
        /// <summary>
        /// 公交费 结束时间
        /// </summary>
        public DateTime? BusToTime
        {
            set { _bustotime = value; }
            get { return _bustotime; }
        }
        /// <summary>
        /// 餐饮费 地点
        /// </summary>
        public string RepastAddress
        {
            set { _repastaddress = value; }
            get { return _repastaddress; }
        }
        /// <summary>
        /// 餐饮费 金额
        /// </summary>
        public decimal? RepastTotal
        {
            set { _repasttotal = value; }
            get { return _repasttotal; }
        }
        /// <summary>
        /// 餐饮费 人数
        /// </summary>
        public decimal? RepastPerNum
        {
            set { _repastpernum = value; }
            get { return _repastpernum; }
        }
        /// <summary>
        /// 餐饮费 参与者
        /// </summary>
        public string RepastPers
        {
            set { _repastpers = value; }
            get { return _repastpers; }
        }
        /// <summary>
        /// 餐饮费 类型
        /// </summary>
        public string RepastType
        {
            set { _repasttype = value; }
            get { return _repasttype; }
        }
        /// <summary>
        /// 住宿费 地点
        /// </summary>
        public string HotelAddress
        {
            set { _hoteladdress = value; }
            get { return _hoteladdress; }
        }
        /// <summary>
        /// 住宿费 酒店名称
        /// </summary>
        public string HotelName
        {
            set { _hotelname = value; }
            get { return _hotelname; }
        }
        /// <summary>
        /// 住宿费 金额
        /// </summary>
        public decimal? HotelTotal
        {
            set { _hoteltotal = value; }
            get { return _hoteltotal; }
        }
        /// <summary>
        /// 住宿费 类型
        /// </summary>
        public string HotelType
        {
            set { _hoteltype = value; }
            get { return _hoteltype; }
        }
        /// <summary>
        /// 汽油补贴 开始地点
        /// </summary>
        public string OilFromAddress
        {
            set { _oilfromaddress = value; }
            get { return _oilfromaddress; }
        }
        /// <summary>
        /// 汽油补贴 结束地点
        /// </summary>
        public string OilToAddress
        {
            set { _oiltoaddress = value; }
            get { return _oiltoaddress; }
        }
        /// <summary>
        /// 汽油补贴 里程
        /// </summary>
        public decimal? OilLiCheng
        {
            set { _oillicheng = value; }
            get { return _oillicheng; }
        }
        /// <summary>
        /// 汽油补贴 金额
        /// </summary>
        public decimal? OilTotal
        {
            set { _oiltotal = value; }
            get { return _oiltotal; }
        }
        /// <summary>
        /// 汽油补贴 系数
        /// </summary>
        public decimal? OilXiShu
        {
            set { _oilxishu = value; }
            get { return _oilxishu; }
        }
        /// <summary>
        /// 过路费开始 地点
        /// </summary>
        public string GuoBeginAddress
        {
            set { _guobeginaddress = value; }
            get { return _guobeginaddress; }
        }
        /// <summary>
        /// 过路费  结束地点
        /// </summary>
        public string GuoToAddress
        {
            set { _guotoaddress = value; }
            get { return _guotoaddress; }
        }
        /// <summary>
        /// 过路费 金额
        /// </summary>
        public decimal? GuoTotal
        {
            set { _guototal = value; }
            get { return _guototal; }
        }
        /// <summary>
        /// 邮寄费 是否寄出
        /// </summary>
        public bool PostFrom
        {
            set { _postfrom = value; }
            get { return _postfrom; }
        }
        /// <summary>
        /// 邮寄费 寄出地点
        /// </summary>
        public string PostFromAddress
        {
            set { _postfromaddress = value; }
            get { return _postfromaddress; }
        }
        /// <summary>
        /// 邮寄费 是否到付
        /// </summary>
        public bool PostTo
        {
            set { _postto = value; }
            get { return _postto; }
        }
        /// <summary>
        /// 邮寄费 到付地点
        /// </summary>
        public string PostToAddress
        {
            set { _posttoaddress = value; }
            get { return _posttoaddress; }
        }
        /// <summary>
        /// 邮寄费 金额
        /// </summary>
        public decimal? PostTotal
        {
            set { _posttotal = value; }
            get { return _posttotal; }
        }
        /// <summary>
        /// 采购 内容
        /// </summary>
        public string PoContext
        {
            set { _pocontext = value; }
            get { return _pocontext; }
        }
        /// <summary>
        /// 采购 金额
        /// </summary>
        public decimal? PoTotal
        {
            set { _pototal = value; }
            get { return _pototal; }
        }
        /// <summary>
        ///  其它 内容
        /// </summary>
        public string OtherContext
        {
            set { _othercontext = value; }
            get { return _othercontext; }
        }
        /// <summary>
        ///  其它 金额
        /// </summary>
        public decimal? OtherTotal
        {
            set { _othertotal = value; }
            get { return _othertotal; }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private decimal total;
        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }


        /// <summary>
        /// 公交费 备注
        /// </summary>
        public string BusRemark
        {
            set { _busremark = value; }
            get { return _busremark; }
        }
        /// <summary>
        /// 餐饮费 备注
        /// </summary>
        public string RepastRemark
        {
            set { _repastremark = value; }
            get { return _repastremark; }
        }
        /// <summary>
        /// 住宿费 备注
        /// </summary>
        public string HotelRemark
        {
            set { _hotelremark = value; }
            get { return _hotelremark; }
        }
        /// <summary>
        /// 汽油补贴 备注
        /// </summary>
        public string OilRemark
        {
            set { _oilremark = value; }
            get { return _oilremark; }
        }
        /// <summary>
        /// 过路费 备注
        /// </summary>
        public string GuoRemark
        {
            set { _guoremark = value; }
            get { return _guoremark; }
        }
        /// <summary>
        /// 邮寄费 备注
        /// </summary>
        public string PostRemark
        {
            set { _postremark = value; }
            get { return _postremark; }
        }
        /// <summary>
        /// 小额采购 备注
        /// </summary>
        public string PoRemark
        {
            set { _poremark = value; }
            get { return _poremark; }
        }
        /// <summary>
        /// 其它费用 备注
        /// </summary>
        public string OtherRemark
        {
            set { _otherremark = value; }
            get { return _otherremark; }
        }
        /// <summary>
        /// 邮寄编号
        /// </summary>
        public string PostNo
        {
            set { _postno = value; }
            get { return _postno; }
        }
        /// <summary>
        /// 快递公司
        /// </summary>
        public string PostCompany
        {
            set { _postcompany = value; }
            get { return _postcompany; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string PostContext
        {
            set { _postcontext = value; }
            get { return _postcontext; }
        }
        /// <summary>
        /// 收件人姓名
        /// </summary>
        public string PostToPer
        {
            set { _posttoper = value; }
            get { return _posttoper; }
        }
        #endregion Model


        private int? _post_id;
        private string _post_no;

        /// <summary>
        /// 
        /// </summary>
        public int? Post_Id
        {
            set { _post_id = value; }
            get { return _post_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Post_No
        {
            set { _post_no = value; }
            get { return _post_no; }
        }

        private string _buspono;
        public string PoNo
        {
            get { return _buspono; }
            set { _buspono = value; }
        }
        private string _busponame;
        private string _busguestname;
        private string _repastpono;
        private string _repastponame;
        private string _repastguestname;
        private string _hotelpono;
        private string _hotelponame;
        private string _hotelguestname;
        private string _oilpono;
        private string _oilponame;
        private string _oilguestname;
        private string _guopono;
        private string _guoponame;
        private string _guoguestname;
        private string _postpono;
        private string _postponame;
        private string _postguestname;
        private string _popono;
        private string _poponame;
        private string _poguestname;
        private string _otherpono;
        private string _othername;
        private string _otherguestname;
        /// <summary>
		/// 
		/// </summary>
		public string PoName
		{
			set{ _busponame=value;}
			get{return _busponame;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GuestName
		{
			set{ _busguestname=value;}
			get{return _busguestname;}
		}


        public string CaiPoNo { get; set; }

        public int CaiId { get; set; }
        public string State { get; set; }
        public string AE { get; set; }
        public string ComName { get; set; }

        public string DispatchType { get; set; }
    }
}
