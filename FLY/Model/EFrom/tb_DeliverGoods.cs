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
    public partial class tb_DeliverGoods : CommModel
    {
        public tb_DeliverGoods()
        { }
        #region Model
        private int _id;
        private string _departname;
        private string _name;
        private DateTime _datetime;
        private string _songhuo;
        private string _compname;
        private DateTime? _gotime;
        private DateTime? _backtime;
        private string _invname;
        private string _address;
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
        public string DepartName
        {
            set { _departname = value; }
            get { return _departname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime dateTime
        {
            set { _datetime = value; }
            get { return _datetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SongHuo
        {
            set { _songhuo = value; }
            get { return _songhuo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CompName
        {
            set { _compname = value; }
            get { return _compname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? GoTime
        {
            set { _gotime = value; }
            get { return _gotime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? BackTime
        {
            set { _backtime = value; }
            get { return _backtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InvName
        {
            set { _invname = value; }
            get { return _invname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        #endregion Model

    }
}
