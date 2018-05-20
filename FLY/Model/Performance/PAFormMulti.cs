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
    public partial class PAFormMulti
    {
        public PAFormMulti()
        { }
        #region Model
        private List<int> _a_PAFormID;
        private List<int> _a_PAItem;
        private int _a_UserID;
        private List<decimal> _a_PAMultiReviewScore;
        private List<bool> _a_PAIsReview;
        private List<string> _a_PANote;
        /// <summary>
        /// 
        /// </summary>
        public List<int> PAFormID
        {
            set { _a_PAFormID = value; }
            get { return _a_PAFormID; }
        }
        /// <summary>
        /// 众评人ID
        /// </summary>
        public int UserID
        {
            set { _a_UserID = value; }
            get { return _a_UserID; }
        }
        /// <summary>
        /// 评分项ID
        /// </summary>
        public List<int> PAItem
        {
            set { _a_PAItem = value; }
            get { return _a_PAItem; }
        }
        /// <summary>
        /// 众评分数
        /// </summary>
        public List<decimal> PAMultiReviewScore
        {
            set { _a_PAMultiReviewScore = value; }
            get { return _a_PAMultiReviewScore; }
        }
        /// <summary>
        /// 是否众评
        /// </summary>
        public List<bool> PAIsReview
        {
            set { _a_PAIsReview = value; }
            get { return _a_PAIsReview; }
        }
        /// <summary>
        /// 众评注释
        /// </summary>
        public List<string> PANote
        {
            set { _a_PANote = value; }
            get { return _a_PANote; }
        }
        #endregion Model
    }
}
