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
    /// Tb_ProjectInv:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Tb_ProjectInv : CommModel
    {
        public Tb_ProjectInv()
        { }


        private string guestName;
        public string GuestName
        {
            get { return guestName; }
            set { guestName = value; }
        }


        private int _guestId;
        public int GuestId
        {
            get { return _guestId; }
            set { _guestId = value; }
        }


        private string state;
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        #region Model
        private int _id;
        private int? _userid;
        private DateTime? _createtime;
        private string _prono;
        private string _proname;
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
        public int? UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProNo
        {
            set { _prono = value; }
            get { return _prono; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProName
        {
            set { _proname = value; }
            get { return _proname; }
        }

        private string loginName;
        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }
        #endregion Model

    }
}
