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

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// TB_Company:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_Company
    {
        public TB_Company()
        { }
        #region Model
        private int _id;
        private string _comid = "";
        private string _comcode = "";
        private string _comname = "";
        private string _comsimpname = "";
        private string _zhusuo = "";
        private string _leixing = "";
        private string _dianhua = "";
        private string _chuanzhen = "";
        private string _xinyongcode = "";
        private string _faren = "";
        private string _zhuceziben = "";
        private DateTime _createtime = DateTime.Now;
        private DateTime _starttime = DateTime.Now;
        private DateTime _endtime = DateTime.Now;
        private string _fanwei = "";
        private string _kaihuhang = "";
        private string _kahao = "";
        private string _email = "";
        private string _comurl = "";
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 流水号 
        /// </summary>
        public string ComId
        {
            set { _comid = value; }
            get { return _comid; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string ComCode
        {
            set { _comcode = value; }
            get { return _comcode; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string ComName
        {
            set { _comname = value; }
            get { return _comname; }
        }
        /// <summary>
        /// 公司简称
        /// </summary>
        public string ComSimpName
        {
            set { _comsimpname = value; }
            get { return _comsimpname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhuSuo
        {
            set { _zhusuo = value; }
            get { return _zhusuo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LeiXing
        {
            set { _leixing = value; }
            get { return _leixing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DianHua
        {
            set { _dianhua = value; }
            get { return _dianhua; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ChuanZhen
        {
            set { _chuanzhen = value; }
            get { return _chuanzhen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XinYongCode
        {
            set { _xinyongcode = value; }
            get { return _xinyongcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FaRen
        {
            set { _faren = value; }
            get { return _faren; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZhuCeZiBen
        {
            set { _zhuceziben = value; }
            get { return _zhuceziben; }
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
        public DateTime StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FanWei
        {
            set { _fanwei = value; }
            get { return _fanwei; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string KaiHuHang
        {
            set { _kaihuhang = value; }
            get { return _kaihuhang; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string KaHao
        {
            set { _kahao = value; }
            get { return _kahao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ComUrl
        {
            set { _comurl = value; }
            get { return _comurl; }
        }
        #endregion Model

        public int OrderByIndex { get; set; }
    }
}
