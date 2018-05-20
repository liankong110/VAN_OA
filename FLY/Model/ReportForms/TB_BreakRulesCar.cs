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
    /// TB_BreakRulesCar:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_BreakRulesCar
    {
        public TB_BreakRulesCar()
        { }
        #region Model
        private int _id;
        private string _jiguan;
        private DateTime _breaktime;
        private string _address;
        private string _dothing;
        private decimal _total;
        private string _state;
        private string _carno;
        private string _remark;
        private string _driver;
        /// <summary>
        /// 
        /// </summary>
        public string Driver
        {
            set { _driver = value; }
            get { return _driver; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 发现机关
        /// </summary>
        public string JiGuan
        {
            set { _jiguan = value; }
            get { return _jiguan; }
        }
        /// <summary>
        /// 违章时间
        /// </summary>
        public DateTime BreakTime
        {
            set { _breaktime = value; }
            get { return _breaktime; }
        }
        /// <summary>
        /// 违章地点
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 违章行为
        /// </summary>
        public string Dothing
        {
            set { _dothing = value; }
            get { return _dothing; }
        }
        /// <summary>
        /// 罚款金额
        /// </summary>
        public decimal Total
        {
            set { _total = value; }
            get { return _total; }
        }
        /// <summary>
        /// 处理标志
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNo
        {
            set { _carno = value; }
            get { return _carno; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
        /// <summary>
        /// 分数
        /// </summary>
        public decimal Score { get; set; }

    }
}
