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

namespace VAN_OA.Model.Performance
{
    [Serializable]
    public partial class A_PAItem
    {
        public A_PAItem()
        { }
        #region Model
        private int _a_PAItemID;
        private string _a_PAItemName;
        private decimal _a_PAItemScore;
        private decimal _a_PAItemAmount;
        private bool _a_ifedit;
        /// <summary>
        /// 
        /// </summary>
        public int A_PAItemID
        {
            set { _a_PAItemID = value; }
            get { return _a_PAItemID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string A_PAItemName
        {
            set { _a_PAItemName = value; }
            get { return _a_PAItemName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal A_PAItemScore
        {
            set { _a_PAItemScore = value; }
            get { return _a_PAItemScore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal A_PAItemAmount
        {
            set { _a_PAItemAmount = value; }
            get { return _a_PAItemAmount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool A_IFEdit
        {
            set { _a_ifedit = value; }
            get { return _a_ifedit; }
        }
        #endregion Model

    }
}
