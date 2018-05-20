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
    /// tb_Post:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tb_Post : CommModel
    {
        public tb_Post()
        { }
        #region Model

        private string loginName;
        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }
        private int _id;
        private int _appname;
        private DateTime _apptime;
        private string _postaddress;
        private string _toper;
        private string _tel;
        private string _wuliuname;
        private string _postcode;
        private string _remark;


        private string _postcontext;
        private bool? _postfrom;
        private string _postfromaddress;
        private bool? _postto;
        private string _posttoaddress;
        private decimal? _posttotal;
        private string _postremark;



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
        public int AppName
        {
            set { _appname = value; }
            get { return _appname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AppTime
        {
            set { _apptime = value; }
            get { return _apptime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostAddress
        {
            set { _postaddress = value; }
            get { return _postaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ToPer
        {
            set { _toper = value; }
            get { return _toper; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WuliuName
        {
            set { _wuliuname = value; }
            get { return _wuliuname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostCode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model


        public decimal? Total { get; set; }

        private string _fromPer;
        public string FromPer
        {
            get { return _fromPer; }
            set { _fromPer = value; }
        }



        /// <summary>
        /// 
        /// </summary>
        public string PostContext
        {
            set { _postcontext = value; }
            get { return _postcontext; }
        }
        /// <summary>
        /// 邮寄费 是否寄出
        /// </summary>
        public bool? PostFrom
        {
            set { _postfrom = value; }
            get { return _postfrom; }
        }
        /// <summary>
        /// 邮寄费 寄出地点
        /// </summary>
        public string PostFromAddress
        {
            set { _postfromaddress = value; }
            get { return _postfromaddress; }
        }
        /// <summary>
        /// 邮寄费 是否到付
        /// </summary>
        public bool? PostTo
        {
            set { _postto = value; }
            get { return _postto; }
        }
        /// <summary>
        /// 邮寄费 到付地点
        /// </summary>
        public string PostToAddress
        {
            set { _posttoaddress = value; }
            get { return _posttoaddress; }
        }
        /// <summary>
        /// 邮寄费 金额
        /// </summary>
        public decimal? PostTotal
        {
            set { _posttotal = value; }
            get { return _posttotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostRemark
        {
            set { _postremark = value; }
            get { return _postremark; }
        }

        private string _poguestname;
        private string _pono;
        private string _poname;
        /// <summary>
        /// 客户名称
        /// </summary>
        public string POGuestName
        {
            set { _poguestname = value; }
            get { return _poguestname; }
        }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string PONo
        {
            set { _pono = value; }
            get { return _pono; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string POName
        {
            set { _poname = value; }
            get { return _poname; }
        }

        public string AE { get; set; }

    }
}
