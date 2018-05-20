using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.EFrom
{
  /// <summary>
	/// ExpInvInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
    public partial class ExpInvInfoView
    {
        public ExpInvInfoView()
        { }
        #region Model
        private string _eventno;
        private string _loginname;
        private DateTime? _exptime;
        private DateTime? _outtime;
        private string _invname;
        private string _expuse;
        private string _expstate;
        private DateTime? _returntime;
        private string _expremark;
        /// <summary>
        /// 
        /// </summary>
        public string EventNo
        {
            set { _eventno = value; }
            get { return _eventno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string loginName
        {
            set { _loginname = value; }
            get { return _loginname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ExpTime
        {
            set { _exptime = value; }
            get { return _exptime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OutTime
        {
            set { _outtime = value; }
            get { return _outtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InvName
        {
            set { _invname = value; }
            get { return _invname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExpUse
        {
            set { _expuse = value; }
            get { return _expuse; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExpState
        {
            set { _expstate = value; }
            get { return _expstate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ReturnTime
        {
            set { _returntime = value; }
            get { return _returntime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExpRemark
        {
            set { _expremark = value; }
            get { return _expremark; }
        }
        #endregion Model
    }
}
