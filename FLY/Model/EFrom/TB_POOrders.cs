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
    /// TB_POOrders:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_POOrders
    {
        public TB_POOrders()
        { }
        #region Model

        private bool ifUpdate;
        public bool IfUpdate
        {
            get { return ifUpdate; }
            set { ifUpdate = value; }
        }
        private int _ids;
        private int _id;
        private DateTime _time;
        private string _guestname;
        private string _invname;
        private decimal _num;
        private string _unit;
        private decimal _costprice;

        private decimal _costTotal;
        public decimal CostTotal
        {
            get { return _costTotal; }
            set { _costTotal = value; }
        }

        private decimal _sellTotal;
        public decimal SellTotal
        {
            get { return _sellTotal; }
            set { _sellTotal = value; }
        }

        private decimal yiLiTotal;
        public decimal YiLiTotal
        {
            get { return yiLiTotal; }
            set { yiLiTotal = value; }
        }
        private decimal _sellprice;
        private decimal _othercost;
        private DateTime? _totime;
        private decimal? _profit;
        /// <summary>
        /// 
        /// </summary>
        public int Ids
        {
            set { _ids = value; }
            get { return _ids; }
        }
        /// <summary>
        /// 父节点
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
        /// 产品
        /// </summary>
        public string InvName
        {
            set { _invname = value; }
            get { return _invname; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Num
        {
            set { _num = value; }
            get { return _num; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 成本单价
        /// </summary>
        public decimal CostPrice
        {
            set { _costprice = value; }
            get { return _costprice; }
        }
        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal SellPrice
        {
            set { _sellprice = value; }
            get { return _sellprice; }
        }
        /// <summary>
        /// 费用
        /// </summary>
        public decimal OtherCost
        {
            set { _othercost = value; }
            get { return _othercost; }
        }
        /// <summary>
        /// 到帐日期
        /// </summary>
        public DateTime? ToTime
        {
            set { _totime = value; }
            get { return _totime; }
        }
        /// <summary>
        /// 利润%
        /// </summary>
        public decimal? Profit
        {
            set { _profit = value; }
            get { return _profit; }
        }
        #endregion Model

    }
}
