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
    /// Tb_ProjectInvs:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Tb_ProjectInvs
    {
        public Tb_ProjectInvs()
        { }

        public bool IfUpdate { get; set; }
        #region Model
        private int _id;
        private int _pid;
        private DateTime _buytime;
        private string _invmodel;
        private string _invname;
        private string _invunit;
        private decimal? _invnum;
        private decimal? _invprice;
        private decimal? _invcarprice;
        private decimal? _invtaskprice;
        private decimal? _invmanprice;
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
        public int PId
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime BuyTime
        {
            set { _buytime = value; }
            get { return _buytime; }
        }
        /// <summary>
        /// 材料型号
        /// </summary>
        public string InvModel
        {
            set { _invmodel = value; }
            get { return _invmodel; }
        }
        /// <summary>
        /// 材料名称
        /// </summary>
        public string InvName
        {
            set { _invname = value; }
            get { return _invname; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string InvUnit
        {
            set { _invunit = value; }
            get { return _invunit; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? InvNum
        {
            set { _invnum = value; }
            get { return _invnum; }
        }
        /// <summary>
        /// 材料费
        /// </summary>
        public decimal? InvPrice
        {
            set { _invprice = value; }
            get { return _invprice; }
        }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal? InvCarPrice
        {
            set { _invcarprice = value; }
            get { return _invcarprice; }
        }
        /// <summary>
        /// 会务费
        /// </summary>
        public decimal? InvTaskPrice
        {
            set { _invtaskprice = value; }
            get { return _invtaskprice; }
        }
        /// <summary>
        /// 管理费
        /// </summary>
        public decimal? InvManPrice
        {
            set { _invmanprice = value; }
            get { return _invmanprice; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

        private string guestName;
        public string GuestName
        {
            get { return guestName; }
            set { guestName = value; }
        }


        private string efState;
        public string EfState
        {
            get { return efState; }
            set { efState = value; }
        }

        private string state;
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        #region Model
        
        private DateTime? _createtime;
        private string _prono;
        private string _proname;
         

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProNo
        {
            set { _prono = value; }
            get { return _prono; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProName
        {
            set { _proname = value; }
            get { return _proname; }
        }

        private string loginName;
        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }

        private decimal? _total;
        public decimal? Total
        {
            get { return _total; }
            set { _total = value; }
        }
        #endregion Model

    }
}
