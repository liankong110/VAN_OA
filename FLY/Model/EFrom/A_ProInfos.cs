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
    public partial class A_ProInfos
    {
        public A_ProInfos()
        { }
        #region Model
        private int _ids;
        private int _pro_id;
        private int _a_role_id;
        private int _a_index;

        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ids
        {
            set { _ids = value; }
            get { return _ids; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int pro_Id
        {
            set { _pro_id = value; }
            get { return _pro_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int a_Role_Id
        {
            set { _a_role_id = value; }
            get { return _a_role_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int a_Index
        {
            set { _a_index = value; }
            get { return _a_index; }
        }
        #endregion Model

    }
}
