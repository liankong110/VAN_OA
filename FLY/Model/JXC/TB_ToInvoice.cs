using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// TB_ToInvoice:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_ToInvoice
    {
        public TB_ToInvoice()
        { }
        #region Model

        public int FPId { get; set; }
        private int _id;
        private string _prono;
        private string _createuser;
        private DateTime _appledate;
        private DateTime _daokuandate;
        private decimal _total;
        private decimal _upaccount;
        private string _pono;
        private string _poname;
        private string _guestname;
        private string _remark;
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
        public string ProNo
        {
            set { _prono = value; }
            get { return _prono; }
        }
        /// <summary>
        /// 申请人
        /// </summary>
        public string CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime AppleDate
        {
            set { _appledate = value; }
            get { return _appledate; }
        }
        /// <summary>
        /// 到款日期
        /// </summary>
        public DateTime DaoKuanDate
        {
            set { _daokuandate = value; }
            get { return _daokuandate; }
        }
        public DateTime? DaoKuanDate1 { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Total
        {
            set { _total = value; }
            get { return _total; }
        }
        /// <summary>
        /// 上期账期系数
        /// </summary>
        public decimal UpAccount
        {
            set { _upaccount = value; }
            get { return _upaccount; }
        }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string PoNo
        {
            set { _pono = value; }
            get { return _pono; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string PoName
        {
            set { _poname = value; }
            get { return _poname; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string GuestName
        {
            set { _guestname = value; }
            get { return _guestname; }
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



        public string State { get; set; }

        /// <summary>
        /// 账期
        /// </summary>
        public decimal ZhangQi { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string FPNo { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public int BusType { get; set; }

        public string BusTypeStr { get; set; }


        /// <summary>
        /// 项目金额
        /// </summary>
        public decimal POTotal { get; set; }

        /// <summary>
        /// 已开发票金额
        /// </summary>
        public decimal HadFpTotal { get; set; }

        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime? MinOutTime { get; set; }

        /// <summary>
        /// 项目时间
        /// </summary>
        public DateTime MinPoDate { get; set; }

        public string PONo_Id { get; set; }

        public int? Days { get; set; }
        /// <summary>
        /// 是否全到款
        /// </summary>
        public bool IsQuanDao { get; set; }

        public bool IsPoFax { get; set; }

        /// <summary>
        /// 记录之前删除的数据
        /// </summary>
        public string TempGuid { get; set; }

        /// <summary>
        /// 剩余预付款 
        /// </summary>
        public decimal LastPayTotal { get; set; }

        public string AE { get; set; }
        /// <summary>
        /// 未到比例=（项目金额-到款金额）/项目金额*100，显示加上“%“
        /// </summary>
        public string WeiDaoTotal
        {
            get
            {
                if (POTotal == 0) { return "0%"; }

                return string.Format("{0:n2}%", (POTotal - Total) / POTotal * 100);

            }
        }

        public decimal SumWeiDaoTotal
        {
            get
            {
                return (POTotal - Total);
            }
        }
        public decimal SumWeiDaoTotal1
        {
            get
            {
                return (POTotal - Total1);
            }
        }
        /// <summary>
        /// 未到比例=（项目金额-到款金额）/项目金额*100，显示加上“%“
        /// </summary>
        public string WeiDaoTotal1
        {
            get
            {
                if (POTotal == 0) { return "0%"; }
                return string.Format("{0:n2}%", (POTotal - Total1) / POTotal * 100);
            }
        }
        public decimal Total1 { get; set; }
        /// <summary>
        /// 到款比例=（项目金额-到款金额）/项目金额*100，显示加上“%“
        /// </summary>
        public string DaoTotal
        {
            get
            {
                if (POTotal == 0) { return "0%"; }

                return string.Format("{0:n2}%", Total / POTotal * 100);

            }
        }

        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime? FPDate { get; set; }

        /// <summary>
        /// 开票天数
        /// </summary>
        public int? FPDays { get; set; }

        /// <summary>
        /// 未开票天数
        /// </summary>
        public int? WeiFPDays { get; set; }

        public string Model { get; set; }

        public string NewFPNo { get; set; }
    }
}
