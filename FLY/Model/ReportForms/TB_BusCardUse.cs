using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.ReportForms
{
    /// <summary>
    /// TB_BusCardUse:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_BusCardUse
    {
        public TB_BusCardUse()
        { }
        #region Model
        private int _id;
        private string _buscardno;
        private string _buscardper;
        private DateTime _buscarddate;
        private string _address;
        private string _guestname;
        private decimal _usetotal;
        private string _bususeremark;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公交卡号
        /// </summary>
        public string BusCardNo
        {
            set { _buscardno = value; }
            get { return _buscardno; }
        }
        /// <summary>
        /// 使用人
        /// </summary>
        public string BusCardPer
        {
            set { _buscardper = value; }
            get { return _buscardper; }
        }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime BusCardDate
        {
            set { _buscarddate = value; }
            get { return _buscarddate; }
        }
        /// <summary>
        /// 地点
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 客户代表
        /// </summary>
        public string GuestName
        {
            set { _guestname = value; }
            get { return _guestname; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal UseTotal
        {
            set { _usetotal = value; }
            get { return _usetotal; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BusUseRemark
        {
            set { _bususeremark = value; }
            get { return _bususeremark; }
        }
        #endregion Model

        public string Status { get; set; }
        public string ProNo { get; set; }      

        public int CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
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

        /// <summary>
        /// 使用人
        /// </summary>
        public string UseName { get; set; }
    }
}
