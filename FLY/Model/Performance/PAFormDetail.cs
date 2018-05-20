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
using System.Collections.Generic;

namespace VAN_OA.Model.Performance
{
    [Serializable]
    public partial class PAFormDetail
    {
        public PAFormDetail()
        { }
        #region Model
        private int _a_PAFormID;
        private int _a_UserID;
        private string _a_Month;
        private string _a_UserName;
        private string _a_UserIPosition;
        private decimal _a_AttendDays;
        private decimal _a_LeaveDays;
        private decimal _a_FullAttendBonus;
        private int _a_Status;
        private List<int> _a_PAItem;
        private List<int> _a_PAFirstReviewUserID;
        private List<decimal> _a_PAFirstReviewScore;
        private List<int> _a_PASecondReviewUserID;
        private List<decimal> _a_PASecondReviewScore;
        private List<decimal> _a_PAAmount;
        private List<string> _a_PANote;
        /// <summary>
        /// 
        /// </summary>
        public int PAFormID
        {
            set { _a_PAFormID = value; }
            get { return _a_PAFormID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _a_UserID = value; }
            get { return _a_UserID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Month
        {
            set { _a_Month = value; }
            get { return _a_Month; }
        }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName
        {
            set { _a_UserName = value; }
            get { return _a_UserName; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string UserIPosition
        {
            set { _a_UserIPosition = value; }
            get { return _a_UserIPosition; }
        }
        /// <summary>
        /// 假期天数
        /// </summary>
        public decimal LeaveDays
        {
            set { _a_LeaveDays = value; }
            get { return _a_LeaveDays; }
        }
        /// <summary>
        /// 出勤天数
        /// </summary>
        public decimal AttendDays
        {
            set { _a_AttendDays = value; }
            get { return _a_AttendDays; }
        }
        /// <summary>
        /// 全勤奖金
        /// </summary>
        public decimal FullAttendBonus
        {
            set { _a_FullAttendBonus = value; }
            get { return _a_FullAttendBonus; }
        }
        /// <summary>
        /// 评估步骤；0=开始；1=初评结束；2=复评结束
        /// </summary>
        public int Status
        {
            set { _a_Status = value; }
            get { return _a_Status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<int> PAItem
        {
            set { _a_PAItem = value; }
            get { return _a_PAItem; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<int> PAFirstReviewUserID
        {
            set { _a_PAFirstReviewUserID = value; }
            get { return _a_PAFirstReviewUserID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<decimal> PAFirstReviewScore
        {
            set { _a_PAFirstReviewScore = value; }
            get { return _a_PAFirstReviewScore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<int> PASecondReviewUserID
        {
            set { _a_PASecondReviewUserID = value; }
            get { return _a_PASecondReviewUserID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<decimal> PASecondReviewScore
        {
            set { _a_PASecondReviewScore = value; }
            get { return _a_PASecondReviewScore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<decimal> PAAmount
        {
            set { _a_PAAmount = value; }
            get { return _a_PAAmount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<string> PANote
        {
            set { _a_PANote = value; }
            get { return _a_PANote; }
        }
        #endregion Model
    }
}
