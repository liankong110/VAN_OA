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
    public partial class A_Role
    {
        public A_Role()
        { }
        #region Model
        private int _a_roleid;
        private string _a_rolename;
        private bool _a_ifedit;
        /// <summary>
        /// 
        /// </summary>
        public int A_RoleId
        {
            set { _a_roleid = value; }
            get { return _a_roleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string A_RoleName
        {
            set { _a_rolename = value; }
            get { return _a_rolename; }
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
