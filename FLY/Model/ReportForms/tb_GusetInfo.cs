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

namespace VAN_OA.Model.ReportForms
{
    /// <summary>
    /// tb_GusetInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    
    public partial class tb_GusetInfo
    {
        public tb_GusetInfo()
        { }
        private string createUser_Name;
        public string CreateUser_Name
        {
            get { return createUser_Name; }
            set { createUser_Name = value; }
        }

        private DateTime _createtime;
        private int _createuser;
        #region Model
        private int _id;
        private string _guestname;
        private string _zhutel;
        private string _linkman;
        private string _tel1;
        private string _tel2;
        private decimal? _account;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GuestName
        {
            set { _guestname = value; }
            get { return _guestname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhuTel
        {
            set { _zhutel = value; }
            get { return _zhutel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LinkMan
        {
            set { _linkman = value; }
            get { return _linkman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tel1
        {
            set { _tel1 = value; }
            get { return _tel1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tel2
        {
            set { _tel2 = value; }
            get { return _tel2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Account
        {
            set { _account = value; }
            get { return _account; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        #endregion Model

    }
}
