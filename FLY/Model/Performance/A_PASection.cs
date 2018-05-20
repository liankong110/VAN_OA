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
    public partial class A_PASection
    {
        public A_PASection()
        { }
        #region Model
        private int _a_PASectionID;
        private string _a_PASectionName;
        private string[] _a_PASectionMember;
        private bool _a_ifedit;
        /// <summary>
        /// 
        /// </summary>
        public int A_PASectionID
        {
            set { _a_PASectionID = value; }
            get { return _a_PASectionID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string A_PASectionName
        {
            set { _a_PASectionName = value; }
            get { return _a_PASectionName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string[] A_PASectionMember
        {
            set { _a_PASectionMember = value; }
            get { return _a_PASectionMember; }
        }
        #endregion Model
    }
}
