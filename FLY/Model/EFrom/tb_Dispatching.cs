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
    public partial class tb_Dispatching : CommModel
    {
        public tb_Dispatching()
        { }
        #region Model



        private string suiTongRen;
        public string SuiTongRen
        {
            get { return suiTongRen; }
            set { suiTongRen = value; }
        }

        private string _outdispaterName;
        public string OutdispaterName
        {
            get { return _outdispaterName; }
            set { _outdispaterName = value; }
        }
        private int _id;
        private string _dispatcher;
        private DateTime? _disdate;
        private int _outdispater;
        private string _guename;
        private string _tel;
        private string _address;
        private string _contacter;
        private DateTime? _godate;
        private DateTime? _backdate;
        private string _question;
        private string _questionremark;
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
        public string Dispatcher
        {
            set { _dispatcher = value; }
            get { return _dispatcher; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DisDate
        {
            set { _disdate = value; }
            get { return _disdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OutDispater
        {
            set { _outdispater = value; }
            get { return _outdispater; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GueName
        {
            set { _guename = value; }
            get { return _guename; }
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
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Contacter
        {
            set { _contacter = value; }
            get { return _contacter; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? GoDate
        {
            set { _godate = value; }
            get { return _godate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? BackDate
        {
            set { _backdate = value; }
            get { return _backdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Question
        {
            set { _question = value; }
            get { return _question; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QuestionRemark
        {
            set { _questionremark = value; }
            get { return _questionremark; }
        }
        #endregion Model

        public string fileName { get; set; }

        public string fileType { get; set; }
        /// <summary>
        /// 拟派工日期
        /// </summary>
        public DateTime? NiDate { get; set; }
        /// <summary>
        /// 拟派工时间
        /// </summary>
        public decimal? NiHours { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string MyPoNo { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public decimal MyValue { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string MyPoName { get; set; }

        /// <summary>
        /// 项目类别 1.零售 2.工程 3.系统
        /// </summary>
        public string MyPoClass{ get; set; }

        /// <summary>
        /// 客户类型
        /// </summary>
        public string MyGuestType { get; set; }

        /// <summary>
        /// 客户属性
        /// </summary>
        public string MyGuestPro { get; set; }

        public decimal MyXiShu { get; set; }


    }
}
