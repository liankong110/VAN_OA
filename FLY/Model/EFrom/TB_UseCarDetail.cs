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
    /// <summary>
    /// TB_UseCarDetail:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_UseCarDetail:CommModel
    {
        public TB_UseCarDetail()
        { }
        #region Model
        private string _carno;
        private string _driver;

        /// <summary>
        /// 
        /// </summary>
        public string CarNo
        {
            set { _carno = value; }
            get { return _carno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Driver
        {
            set { _driver = value; }
            get { return _driver; }
        }

        private string goEndTime;
        public string GoEndTime
        {
            get { return goEndTime; }
            set { goEndTime = value; }
        }
        private string appUserName;
        public string AppUserName
        {
            get { return appUserName; }
            set { appUserName = value; }
        }
        private int _id;
        private int _appuser;
        private DateTime _apptime;
        private string _guestname;
        private string _type;
        private decimal _roadlong;
        private string _area;
        private string _bycarpers;
        private string _goaddress;
        private string _toaddress;
        private DateTime? _gotime;
        private DateTime? _endtime;
        private string _remark;
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
        public int AppUser
        {
            set { _appuser = value; }
            get { return _appuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AppTime
        {
            set { _apptime = value; }
            get { return _apptime; }
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
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal RoadLong
        {
            set { _roadlong = value; }
            get { return _roadlong; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Area
        {
            set { _area = value; }
            get { return _area; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ByCarPers
        {
            set { _bycarpers = value; }
            get { return _bycarpers; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoAddress
        {
            set { _goaddress = value; }
            get { return _goaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ToAddress
        {
            set { _toaddress = value; }
            get { return _toaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? GoTime
        {
            set { _gotime = value; }
            get { return _gotime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }


        private decimal? fromRoadLong;
        public decimal? FromRoadLong
        {
            get { return fromRoadLong; }
            set { fromRoadLong = value; }
        }


        private decimal? toRoadLong;
        public decimal? ToRoadLong
        {
            get { return toRoadLong; }
            set { toRoadLong = value; }
        }
        #endregion Model

        private string doPer;
        public string DoPer
        {
            get { return doPer; }
            set { doPer = value; }
        }
        private DateTime? doTime;
        public DateTime? DoTime
        {
            get { return doTime; }
            set { doTime = value; }
        }


        /// <summary>
        /// 项目编码
        /// </summary>
        public string PONo { get; set; }
        
        /// <summary>
        /// 项目名称
        /// </summary>
        public string POName { get; set; }

        public decimal OilPrice { get; set; }

        public static decimal CarOliPrice = 0;

        public string State { get; set; }

        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime UseDate { get; set; }

        public string AE { get; set; }
    }
}
