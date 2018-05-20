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
    /// tb_Complaint:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tb_Complaint : CommModel
    {
        public tb_Complaint()
        { }
        #region Model
        private int _id;
        private int? _appuserid;
        private string appUserName;
        public string AppUserName
        {
            get { return appUserName; }
            set { appUserName = value; }
        }
        private string toPerName;
        public string ToPerName
        {
            get { return toPerName; }
            set { toPerName = value; }
        }
        private DateTime? _datetime;
        private int? _toperid;
        private string _contentremark;
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
        public DateTime? datetime
        {
            set { _datetime = value; }
            get { return _datetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? toPerId
        {
            set { _toperid = value; }
            get { return _toperid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContentRemark
        {
            set { _contentremark = value; }
            get { return _contentremark; }
        }
        #endregion Model

    }
}
