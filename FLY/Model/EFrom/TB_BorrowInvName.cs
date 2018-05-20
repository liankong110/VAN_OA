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
    public partial class TB_BorrowInvName : CommModel
    {
        public TB_BorrowInvName()
        { }
        private string loginName;
        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }
        #region Model
        private int _id;
        private int _appper;
        private DateTime _apptime;
        private string _invname;
        private string _reason;
        private DateTime? _borrowtime;
        private DateTime? _backtime;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 申请人
        /// </summary>
        public int AppPer
        {
            set { _appper = value; }
            get { return _appper; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime AppTime
        {
            set { _apptime = value; }
            get { return _apptime; }
        }
        /// <summary>
        /// 货物
        /// </summary>
        public string InvName
        {
            set { _invname = value; }
            get { return _invname; }
        }
        /// <summary>
        /// 原因
        /// </summary>
        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
        }
        /// <summary>
        /// 借出时间
        /// </summary>
        public DateTime? BorrowTime
        {
            set { _borrowtime = value; }
            get { return _borrowtime; }
        }
        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime? BackTime
        {
            set { _backtime = value; }
            get { return _backtime; }
        }
        #endregion Model

    }
}
