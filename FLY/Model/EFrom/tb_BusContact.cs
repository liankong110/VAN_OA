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
    /// tb_BusContact:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tb_BusContact : CommModel
    {
        public tb_BusContact()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _departname;
        private DateTime? _datetime;
        private string _contactunit;
        private string _contacer;
        private string _tel;

        private DateTime? _gotime;
        private DateTime? _backtime;

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Gotime
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
        public int id
        {
            set { _id = value; }
            get { return _id; }
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
        public string DepartName
        {
            set { _departname = value; }
            get { return _departname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DateTime
        {
            set { _datetime = value; }
            get { return _datetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContactUnit
        {
            set { _contactunit = value; }
            get { return _contactunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Contacer
        {
            set { _contacer = value; }
            get { return _contacer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        #endregion Model

        public DateTime? AppDate {get;set;}

        public string fileName { get; set; }

        public string fileType { get; set; }

        /// <summary>
        /// 是否是新客户
        /// </summary>
        public bool IsNewUnit { get; set; }
    }
}
