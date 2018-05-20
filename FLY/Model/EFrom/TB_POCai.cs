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
    /// TB_POCai:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_POCai
    {

        /// <summary>
        /// 利润
        /// </summary>
        private decimal? caiLiRun;
        public decimal? CaiLiRun
        {
            get { return caiLiRun; }
            set { caiLiRun = value; }
        }

        private decimal? total1;
        public decimal? Total1
        {
            get { return total1; }
            set { total1 = value; }
        }
        private decimal? total2;
        public decimal? Total2
        {
            get { return total2; }
            set { total2 = value; }
        }
        private decimal? total3;
        public decimal? Total3
        {
            get { return total3; }
            set { total3 = value; }
        }


        private decimal? _finprice1;
        private decimal? _finprice2;
        private decimal? _finprice3;

        public TB_POCai()
        { }
        private string _guestname;
        private string _invname;
        private decimal? _num;

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
        public string InvName
        {
            set { _invname = value; }
            get { return _invname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Num
        {
            set { _num = value; }
            get { return _num; }
        }


        private string _supplier1;
        private decimal? _supperprice1;
        private string _supplier2;
        private decimal? _supperprice2;

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
        public decimal? SupperPrice1
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
        public decimal? SupperPrice2
        {
            set { _supperprice2 = value; }
            get { return _supperprice2; }
        }
        public bool IfUpdate { get; set; }
        #region Model
        private int _ids;
        private int? _id;
        private DateTime? _caitime;
        private string _supplier;
        private decimal? _supperprice;
        private string _updateuser;
        private string _idea;
        /// <summary>
        /// 
        /// </summary>
        public int Ids
        {
            set { _ids = value; }
            get { return _ids; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CaiTime
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
        public decimal? SupperPrice
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
        #endregion Model


        /// <summary>
        /// 
        /// </summary>
        public decimal? FinPrice1
        {
            set { _finprice1 = value; }
            get { return _finprice1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FinPrice2
        {
            set { _finprice2 = value; }
            get { return _finprice2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FinPrice3
        {
            set { _finprice3 = value; }
            get { return _finprice3; }
        }

    }
}
