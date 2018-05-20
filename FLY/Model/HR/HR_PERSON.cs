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
    /// 人员档案
    /// </summary>
    [Serializable]
    public partial class HR_PERSON
    {
        public HR_PERSON()
        { }
        #region Model
        private int _id;
        private string _Code;
        private string _Department;
        private string _Position;
        private string _Name;
        private DateTime? _Birthday;
        private string _Sex;
        private string _EducationLevel;
        private string _EducationSchool;
        private string _Major;
        private DateTime? _GraduationTime;
        private DateTime? _OnBoardTime;
        private DateTime? _BeNormalTime;
        private DateTime? _ContractTime;
        private DateTime? _ContractCloseTime;
        private string _HuKou;
        private string _Marriage;
        private string _IDCard;
        private string _MobilePhone;
        private string _HomePhone;
        private string _HomeAddress;
        private string _EmailAddress;
        private DateTime? _createtime;
        private int? _createperson;
        private DateTime? _updatetime;
        private int? _updateperson;
        private string _updatepersonName;
        private bool _quit;
        private string _quitStatusName;
        private DateTime? _quitTime;
        private string _quitReason;
        //Salary
        private decimal _BasicSalary;
        private decimal _GongLin;
        private decimal _MobileFee;
        private decimal _PositionFee;
        private decimal _YangLaoJin;
        private decimal _UnionFee;
        private decimal _DefaultWorkDays;
        private bool  _IsRetailed;
        private bool _IsQuit;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Code
        {
            set { _Code = value; }
            get { return _Code; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department
        {
            set { _Department = value; }
            get { return _Department; }
        }
        /// <summary>
        /// 岗位
        /// </summary>
        public string Position
        {
            set { _Position = value; }
            get { return _Position; }
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
        /// 出生年月日
        /// </summary>
        public DateTime? Birthday
        {
            set { _Birthday = value; }
            get { return _Birthday; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            set { _Sex = value; }
            get { return _Sex; }
        }
        /// <summary>
        /// 学历
        /// </summary>
        public string EducationLevel
        {
            set { _EducationLevel = value; }
            get { return _EducationLevel; }
        }
        /// <summary>
        /// 毕业学校
        /// </summary>
        public string EducationSchool
        {
            set { _EducationSchool = value; }
            get { return _EducationSchool; }
        }
        /// <summary>
        /// 专业
        /// </summary>
        public string Major
        {
            set { _Major = value; }
            get { return _Major; }
        }
        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? GraduationTime
        {
            set { _GraduationTime = value; }
            get { return _GraduationTime; }
        }
        /// <summary>
        /// 入司日期
        /// </summary>
        public DateTime? OnBoardTime
        {
            set { _OnBoardTime = value; }
            get { return _OnBoardTime; }
        }
        /// <summary>
        /// 约定转正     日期
        /// </summary>
        public DateTime? BeNormalTime
        {
            set { _BeNormalTime = value; }
            get { return _BeNormalTime; }
        }
        /// <summary>
        /// 签订合同 日期
        /// </summary>
        public DateTime? ContractTime
        {
            set { _ContractTime = value; }
            get { return _ContractTime; }
        }
        /// <summary>
        /// 签订合同结束日期
        /// </summary>
        public DateTime? ContractCloseTime
        {
            set { _ContractCloseTime = value; }
            get { return _ContractCloseTime; }
        }
        /// <summary>
        /// 户口所在地
        /// </summary>
        public string HuKou
        {
            set { _HuKou = value; }
            get { return _HuKou; }
        }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string Marriage
        {
            set { _Marriage = value; }
            get { return _Marriage; }
        }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDCard
        {
            set { _IDCard = value; }
            get { return _IDCard; }
        }
        /// <summary>
        /// 私人联系方式
        /// </summary>
        public string MobilePhone
        {
            set { _MobilePhone = value; }
            get { return _MobilePhone; }
        }
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string HomePhone
        {
            set { _HomePhone = value; }
            get { return _HomePhone; }
        }
        /// <summary>
        /// 家庭地址
        /// </summary>
        public string HomeAddress
        {
            set { _HomeAddress = value; }
            get { return _HomeAddress; }
        }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string EmailAddress
        {
            set { _EmailAddress = value; }
            get { return _EmailAddress; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int? CreatePerson
        {
            set { _createperson = value; }
            get { return _createperson; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>        
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 修改人
        /// </summary>
        public int? UpdatePerson
        {
            set { _updateperson = value; }
            get { return _updateperson; }
        }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string UpdatePersonName
        {
            set { _updatepersonName = value; }
            get { return _updatepersonName; }
        }
        /// <summary>
        /// 是否离职
        /// </summary>
        public bool Quit
        {
            set { _quit = value; }
            get { return _quit; }
        }
        /// <summary>
        /// 是否离职状态名称
        /// </summary>
        public string QuitStatusName
        {
            set { _quitStatusName = value; }
            get { return _quitStatusName; }
        }
        /// <summary>
        /// 离职时间
        /// </summary>
        public DateTime?  QuitTime
        {
            set { _quitTime = value; }
            get { return _quitTime; }
        }
        /// <summary>
        /// 离职原因
        /// </summary>
        public string  QuitReason
        {
            set { _quitReason = value; }
            get { return _quitReason; }
        }
        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal BasicSalary
        {
            set { _BasicSalary = value; }
            get { return _BasicSalary; }
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
        /// 通讯费
        /// </summary>
        public decimal MobileFee
        {
            set { _MobileFee = value; }
            get { return _MobileFee; }
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
        /// 养老金
        /// </summary>
        public decimal YangLaoJin
        {
            set { _YangLaoJin = value; }
            get { return _YangLaoJin; }
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
        /// 默认工作天数
        /// </summary>
        public decimal DefaultWorkDays
        {
            set { _DefaultWorkDays = value; }
            get { return _DefaultWorkDays; }
        }
        // <summary>
        /// 退休返聘
        /// </summary>
        public bool IsRetailed
        {
            set { _IsRetailed = value; }
            get { return _IsRetailed; }
        }
        // <summary>
        /// 离职人员
        /// </summary>
        public bool IsQuit
        {
            set { _IsQuit = value; }
            get { return _IsQuit; }
        }
        #endregion Model
    }
}
