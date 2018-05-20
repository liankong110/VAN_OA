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
    public partial class A_PATemplate
    {
        public A_PATemplate()
        { }
        #region Model
        private int _a_PATemplateID;
        private string _a_PATemplateName;
        private List<int> _a_PATemplateItem;
        private List<int> _a_PAItemSequence;
        private List<int> _a_PATemplateSection;
        private List<decimal> _a_PATemplateScore;
        private List<decimal> _a_PATemplateAmount;
        private List<bool> _a_PATemplateIsFirstReview;
        private List<int> _a_PATemplateFirstReviewUserID;
        private List<bool> _a_PATemplateIsSecondReview;
        private List<int> _a_PATemplateSecondReviewUserID;
        private List<bool> _a_PATemplateIsMultiReview;
        private List<List<int>> _a_PATemplateMultiReviewUserID;

        /// <summary>
        /// 
        /// </summary>
        public int A_PATemplateID
        {
            set { _a_PATemplateID = value; }
            get { return _a_PATemplateID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<int> A_PAItemSequence
        {
            set { _a_PAItemSequence = value; }
            get { return _a_PAItemSequence; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string A_PATemplateName
        {
            set { _a_PATemplateName = value; }
            get { return _a_PATemplateName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<int> A_PATemplateItem
        {
            set { _a_PATemplateItem = value; }
            get { return _a_PATemplateItem; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<int> A_PATemplateSection
        {
            set { _a_PATemplateSection = value; }
            get { return _a_PATemplateSection; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<decimal> A_PATemplateScore
        {
            set { _a_PATemplateScore = value; }
            get { return _a_PATemplateScore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<decimal> A_PATemplateAmount
        {
            set { _a_PATemplateAmount = value; }
            get { return _a_PATemplateAmount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<bool> A_PATemplateIsFirstReview
        {
            set { _a_PATemplateIsFirstReview = value; }
            get { return _a_PATemplateIsFirstReview; }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<int> A_PATemplateFirstReviewUserID
        {
            set { _a_PATemplateFirstReviewUserID = value; }
            get { return _a_PATemplateFirstReviewUserID; }
        }
        /// <summary>
        /// 是否复评
        /// </summary>
        public List<bool> A_PATemplateIsSecondReview
        {
            set { _a_PATemplateIsSecondReview = value; }
            get { return _a_PATemplateIsSecondReview; }
        }
        /// <summary>
        /// 复评人
        /// </summary>
        public List<int> A_PATemplateSecondReviewUserID
        {
            set { _a_PATemplateSecondReviewUserID = value; }
            get { return _a_PATemplateSecondReviewUserID; }
        }
        /// <summary>
        /// 是否众评
        /// </summary>
        public List<bool> A_PATemplateIsMultiReview
        {
            set { _a_PATemplateIsMultiReview = value; }
            get { return _a_PATemplateIsMultiReview; }
        }
        /// 众评人
        /// </summary>
        public List<List<int>> A_PATemplateMultiReviewUserID
        {
            set { _a_PATemplateMultiReviewUserID = value; }
            get { return _a_PATemplateMultiReviewUserID; }
        }
        #endregion Model
    }
}
