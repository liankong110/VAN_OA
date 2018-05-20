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

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// GuestTypeBaseInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class GuestTypeBaseInfo
    {
        public GuestTypeBaseInfo()
        { }
        #region Model
        private int _id;
        private string _guesttype;
        private decimal _payxishu;
        private int _xishu = 0;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 客户类型
        /// </summary>
        public string GuestType
        {
            set { _guesttype = value; }
            get { return _guesttype; }
        }
        /// <summary>
        /// 结算系数（数值0-1，小数点3位）
        /// </summary>
        public decimal PayXiShu
        {
            set { _payxishu = value; }
            get { return _payxishu; }
        }
        /// <summary>
        ///  系数可变（1：可变  0 不可变）
        /// </summary>
        public int XiShu
        {
            set { _xishu = value; }
            get { return _xishu; }
        }
        #endregion Model

    }
}
