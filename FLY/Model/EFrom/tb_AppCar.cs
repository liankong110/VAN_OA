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
    /// <summary>
    /// tb_AppCar:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tb_AppCar : CommModel
    {
        public tb_AppCar()
        { }

        private string loginName;
        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }
        private string departName;
        public string DepartName
        {
            get { return departName; }
            set { departName = value; }
        }
        #region Model
        private int _id;
        private int? _appuserid;
        private DateTime? _datetiem;
        private string _type;
        private string _address;
        private string _compname;
        private string _invname;
        private string _suicheper;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? appUserId
        {
            set { _appuserid = value; }
            get { return _appuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? datetiem
        {
            set { _datetiem = value; }
            get { return _datetiem; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string compName
        {
            set { _compname = value; }
            get { return _compname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string invName
        {
            set { _invname = value; }
            get { return _invname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string suiChePer
        {
            set { _suicheper = value; }
            get { return _suicheper; }
        }
        #endregion Model

    }
}
