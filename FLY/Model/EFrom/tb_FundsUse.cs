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
    public partial class tb_FundsUse : CommModel
    {
        public tb_FundsUse()
        { }
        

        //public static string shuiNo1 = "含VAT税";
        //public static string shuiNo2 = "含LT税";
        //public static string shuiNo3 = "不含税a";
        //public static string shuiNo4 = "不含税b";
        //public static string shuiNo5 = "不含税c";

        //public static decimal shuiXS1 = 1;
        //public static decimal shuiXS2 =Convert.ToDecimal(1.02);
        //public static decimal shuiXS3 = Convert.ToDecimal(1.17);
        //public static decimal shuiXS4 = Convert.ToDecimal(1.33);
        //public static decimal shuiXS5 = Convert.ToDecimal(1.25);

        
        public static string GetShui(decimal xs )
        {
            object obj = DBHelp.ExeScalar(string.Format("select top 1 FpType from FpTaxInfo where TAX={0}", xs));
            if (obj != null && !(obj is DBNull))
            {
                return Convert.ToString(obj);
            }
            //if (xs == shuiXS1)
            //{
            //    return shuiNo1;
            //}
            //else if (xs == shuiXS2)
            //{
            //    return shuiNo2;
            //}
            //else if (xs == shuiXS3)
            //{
            //    return shuiNo3;
            //}
            //else if (xs == shuiXS4)
            //{
            //    return shuiNo4;
            //}
            //else if (xs == shuiXS5)
            //{
            //    return shuiNo5;
            //}
            return "";
        }

        public static decimal GetXiShuo(string shui)
        {
            object obj = DBHelp.ExeScalar(string.Format("select top 1 TAX from FpTaxInfo where FpType='{0}'",shui));
            if (obj != null&&!(obj  is DBNull))
            {
                return Convert.ToDecimal(obj);
            }
            //if (shui ==  shuiNo1)
            //{
            //    return shuiXS1;
            //}
            //else if (shui == shuiNo2)
            //{
            //    return shuiXS2;
            //}
            //else if (shui ==  shuiNo3)
            //{
            //    return shuiXS3;
            //}
            //else if (shui ==  shuiNo4)
            //{
            //    return shuiXS4;
            //}
            //else if (shui == shuiNo5)
            //{
            //    return shuiXS5;
            //}
            return 0;
        }


        public string State { get; set; }
        private string _expNo;
        public string ExpNo
        {
            get { return _expNo; }
            set { _expNo = value; }
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
        #region Model
        private int _id;
        private int _appuserid;
        private DateTime _datetiem;
        private string _useto;
        private string _type;
        private decimal _total;
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
        public int appUserId
        {
            set { _appuserid = value; }
            get { return _appuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime datetiem
        {
            set { _datetiem = value; }
            get { return _datetiem; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string useTo
        {
            set { _useto = value; }
            get { return _useto; }
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
        public decimal total
        {
            set { _total = value; }
            get { return _total; }
        }
        #endregion Model

        private string _invoce;
        private string _houseno;
        private string _idea;
        private string _filename;
        private byte[] _fileno;
        private string _filetype;

        /// <summary>
        /// 
        /// </summary>
        public string Invoce
        {
            set { _invoce = value; }
            get { return _invoce; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HouseNo
        {
            set { _houseno = value; }
            get { return _houseno; }
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
        private string _pono;
        private string _poname;
        private string _guestname;
        private decimal? _caitotal;
        private decimal? _caixs;
        private decimal? _hui;
        private decimal? _huixs;
        private decimal? _huitotal;
        private decimal? _ren;
        private decimal? _renxs;
        private decimal? _rentotal;
        private string _vat;
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
        public string GuestName
        {
            set { _guestname = value; }
            get { return _guestname; }
        }
        /// <summary>
		/// 
		/// </summary>
		public decimal? CaiTotal
		{
			set{ _caitotal=value;}
			get{return _caitotal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CAIXS
		{
			set{ _caixs=value;}
			get{return _caixs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Hui
		{
			set{ _hui=value;}
			get{return _hui;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? HuiXS
		{
			set{ _huixs=value;}
			get{return _huixs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? HuiTotal
		{
			set{ _huitotal=value;}
			get{return _huitotal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Ren
		{
			set{ _ren=value;}
			get{return _ren;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? RenXS
		{
			set{ _renxs=value;}
			get{return _renxs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? RenTotal
		{
			set{ _rentotal=value;}
			get{return _rentotal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string VAT
		{
			set{ _vat=value;}
			get{return _vat;}
		}

        public decimal? XingCai { get; set; }
        public decimal? XingCaiXS { get; set; }
        public decimal? XingCaiTotal { get; set; }
        /// <summary>
        /// 费用类别
        /// </summary>
        public string FundType { get; set; }

        public string ShuoMing { get; set; }

        public string AE { get; set; }

        public string ComName { get; set; }

        public decimal AllTotal { get; set; }
        public decimal AllTrueTotal { get; set; }

        public string XishuDes { get; set; }

        public string MyTeamLever { get; set; }
    }
}
