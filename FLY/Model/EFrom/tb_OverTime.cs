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
    public partial class tb_OverTime : CommModel
    {
        public tb_OverTime()
        { }
        #region Model


        private string overTimeType;
        public string OverTimeType
        {
            get { return overTimeType; }
            set { overTimeType = value; }
        }

        private string loginName;
        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }
        private string departName;
        public string DepartName
        {
            get { return departName; }
            set { departName = value; }
        }
        private int _id;
        private int _appuseid;
        private string _reason;
        private DateTime _formtime;
        private DateTime _totime;
        private string _guestdai;

        private string _address;
        private string _suixingren;
        private string _reamrk;
        private DateTime? _time;



        private string betweenTime;
        public string BetweenTime
        {
            get { return betweenTime; }
            set { betweenTime = value; }
        }

        private decimal betweenHours;
        public decimal BetweenHours
        {
            get { return betweenHours; }
            set { betweenHours = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SuixingRen
        {
            set { _suixingren = value; }
            get { return _suixingren; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Reamrk
        {
            set { _reamrk = value; }
            get { return _reamrk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Time
        {
            set { _time = value; }
            get { return _time; }
        }
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
        public int appUseId
        {
            set { _appuseid = value; }
            get { return _appuseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string reason
        {
            set { _reason = value; }
            get { return _reason; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime formTime
        {
            set { _formtime = value; }
            get { return _formtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime toTime
        {
            set { _totime = value; }
            get { return _totime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string guestDai
        {
            set { _guestdai = value; }
            get { return _guestdai; }
        }
        #endregion Model

        private decimal? total;
        public decimal? Total
        {
            get { return total; }
            set { total = value; }
        }

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


        public string State { get; set; }

    }
}
