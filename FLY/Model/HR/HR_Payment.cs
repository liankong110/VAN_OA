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

namespace VAN_OA.Model.HR
{
    /// <summary>
    /// 人员工资信息
    /// </summary>
    [Serializable]
    public partial class HR_Payment
    {
        public HR_Payment()
        { }
        #region Model
        private int _id;
        private string _Name;
        private string _Position;
        private string _Yearmonth;
        //Payment
        private decimal _BasicSalary;
        private decimal _FullAttendence;
        private decimal _MobileFee;
        private decimal _SpecialAward;
        private string _SpecialAwardNote;
        private decimal _GongLin;
        private decimal _PositionPerformance;
        private decimal _PositionFee;
        private decimal _WorkPerformance;
        private decimal _FullPayment;
        private decimal _DefaultWorkDays;
        private decimal _WorkDays;
        private decimal _ShouldPayment;
        private decimal _UnionFee;
        private decimal _Deduction;
        private string _DeductionNote;
        private decimal _YangLaoJin;
        private decimal _ActualPayment;
        private DateTime? _UpdateTime;
        private int? _UpdatePerson;
        private string _UpdatePersonName;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            set { _Name = value; }
            get { return _Name; }
        }
        /// <summary>
        /// 职位
        /// </summary>
        public string Position
        {
            set { _Position = value; }
            get { return _Position; }
        }
        /// <summary>
        /// 工资年月
        /// </summary>
        public string  YearMonth
        {
            set { _Yearmonth = value; }
            get { return _Yearmonth; }
        }
        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal BasicSalary
        {
            set { _BasicSalary = value; }
            get { return _BasicSalary; }
        }
        /// <summary>
        /// 全勤奖
        /// </summary>
        public decimal FullAttendence
        {
            set { _FullAttendence = value; }
            get { return _FullAttendence; }
        }
        // <summary>
        /// 通讯费
        /// </summary>
        public decimal MobileFee
        {
            set { _MobileFee = value; }
            get { return _MobileFee; }
        }
        /// <summary>
        /// 特殊奖励
        /// </summary>
        public decimal SpecialAward
        {
            set { _SpecialAward = value; }
            get { return _SpecialAward; }
        }
        /// <summary>
        /// 特殊奖励注释
        /// </summary>
        public string SpecialAwardNote
        {
            set { _SpecialAwardNote = value; }
            get { return _SpecialAwardNote; }
        }
        // <summary>
        /// 工龄工资
        /// </summary>
        public decimal GongLin
        {
            set { _GongLin = value; }
            get { return _GongLin; }
        }
        // <summary>
        /// 岗位考核
        /// </summary>
        public decimal PositionPerformance
        {
            set { _PositionPerformance = value; }
            get { return _PositionPerformance; }
        }
        // <summary>
        /// 职务津贴
        /// </summary>
        public decimal PositionFee
        {
            set { _PositionFee = value; }
            get { return _PositionFee; }
        }
        // <summary>
        /// 工作绩效
        /// </summary>
        public decimal WorkPerformance
        {
            set { _WorkPerformance = value; }
            get { return _WorkPerformance; }
        }
        // <summary>
        /// 工资合计
        /// </summary>
        public decimal FullPayment
        {
            set { _FullPayment = value; }
            get { return _FullPayment; }
        }
        // <summary>
        /// 默认出勤天数
        /// </summary>
        public decimal DefaultWorkDays
        {
            set { _DefaultWorkDays = value; }
            get { return _DefaultWorkDays; }
        }
        // <summary>
        /// 出勤天数
        /// </summary>
        public decimal WorkDays
        {
            set { _WorkDays = value; }
            get { return _WorkDays; }
        }
        // <summary>
        /// 应发工资
        /// </summary>
        public decimal ShouldPayment
        {
            set { _ShouldPayment = value; }
            get { return _ShouldPayment; }
        }
        // <summary>
        /// 工会费
        /// </summary>
        public decimal UnionFee
        {
            set { _UnionFee = value; }
            get { return _UnionFee; }
        }
        // <summary>
        /// 扣款
        /// </summary>
        public decimal Deduction
        {
            set { _Deduction = value; }
            get { return _Deduction; }
        }
        // <summary>
        /// 扣款注释
        /// </summary>
        public string DeductionNote
        {
            set { _DeductionNote = value; }
            get { return _DeductionNote; }
        }
        // <summary>
        /// 养老金
        /// </summary>
        public decimal YangLaoJin
        {
            set { _YangLaoJin = value; }
            get { return _YangLaoJin; }
        }
        // <summary>
        /// 实发工资
        /// </summary>
        public decimal ActualPayment
        {
            set { _ActualPayment = value; }
            get { return _ActualPayment; }
        }
        // <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _UpdateTime = value; }
            get { return _UpdateTime; }
        }
        // <summary>
        /// 更新人
        /// </summary>
        public int? UpdatePerson
        {
            set { _UpdatePerson = value; }
            get { return _UpdatePerson; }
        }
        // <summary>
        /// 更新人名称
        /// </summary>
        public string UpdatePersonName
        {
            set { _UpdatePersonName = value; }
            get { return _UpdatePersonName; }
        }
        #endregion Model

    }
}
