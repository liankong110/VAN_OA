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
    public class A_Role_User
    {
        public A_Role_User()
        { }
        #region Model

        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private int _id;
        private int? _a_roleid;
        private int? _user_id;
        /// <summary>
        /// 
        /// </summary>
        public int? A_RoleId
        {
            set { _a_roleid = value; }
            get { return _a_roleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? User_Id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        #endregion Model

        public int Role_User_Index { get; set; }

        public int RowState { get; set; }

        public string RowStateString
        {
            get
            {
                if (RowState == 1) return "有效"; return "无效";
            }
        }

        public string LoginName_ID { get; set; }
    }
}
