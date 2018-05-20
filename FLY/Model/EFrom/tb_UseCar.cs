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
    [Serializable]
    public partial class tb_UseCar : CommModel
    {
        public tb_UseCar()
        { }
        #region Model
        private int _id;
        private int _appname;
        private DateTime _datetime;
        private string _pers_car;
        private string _type;
        private string _usereason;
        private decimal? _roadlong;
        private string _deaddress;
        private string _goaddress;
        private string _toaddress;
        private DateTime? _gotime;
        private DateTime? _endtime;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int appName
        {
            set { _appname = value; }
            get { return _appname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime datetime
        {
            set { _datetime = value; }
            get { return _datetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string pers_car
        {
            set { _pers_car = value; }
            get { return _pers_car; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string useReason
        {
            set { _usereason = value; }
            get { return _usereason; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? roadLong
        {
            set { _roadlong = value; }
            get { return _roadlong; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string deAddress
        {
            set { _deaddress = value; }
            get { return _deaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string goAddress
        {
            set { _goaddress = value; }
            get { return _goaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string toAddress
        {
            set { _toaddress = value; }
            get { return _toaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? goTime
        {
            set { _gotime = value; }
            get { return _gotime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? endTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }

        private string loginName;

        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }


        private string goEndTime;
        public string GoEndTime
        {
            get { return goEndTime; }
            set { goEndTime = value; }
        }
        #endregion Model

        private string _poguestname;
        private string _pono;
        private string _poname;        
        /// <summary>
        /// 客户名称
        /// </summary>
        public string POGuestName
        {
            set { _poguestname = value; }
            get { return _poguestname; }
        }
        /// <summary>
        /// 项目编码
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

        public decimal OilPrice { get; set; }
        public static decimal CarOliPrice = 0;

        public string State { get; set; }
        public decimal TotalPrice { get; set; }
        public string AE { get; set; }
    }
}
