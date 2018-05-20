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
using VAN_OA.Dal.EFrom;

namespace VAN_OA.Model.EFrom
{
    /// <summary>
    /// tb_LeverInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tb_LeverInfo : CommModel
    {
        public tb_LeverInfo()
        { }
        #region Model
        private int _id;
        private string _depart;
        private string _zhiwu;
        private string _name;
        private string _levertype;
        private DateTime? _dateform;
        private DateTime? _dateto;
        private string _remark;

        private int zhuGuan;
        public int ZhuGuan
        {
            get { return zhuGuan; }
            set { zhuGuan = value; }
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
        public string depart
        {
            set { _depart = value; }
            get { return _depart; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string zhiwu
        {
            set { _zhiwu = value; }
            get { return _zhiwu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string leverType
        {
            set { _levertype = value; }
            get { return _levertype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dateForm
        {
            set { _dateform = value; }
            get { return _dateform; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dateTo
        {
            set { _dateto = value; }
            get { return _dateto; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
        public DateTime? AppDate { get; set; }
    }
}
