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
    /// 工作事务型请假单
    /// </summary>
    public class Tb_LeaveTask : CommModel
    {
        public Tb_LeaveTask()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _levertype;
        private DateTime _begintime;
        private DateTime _totime;
        private string _remark;

        private string department;
        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        private string job;
        public string Job
        {
            get { return job; }
            set { job = value; }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        
        /// <summary>
        /// 标示
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 请假类型
        /// </summary>
        public string LeverType
        {
            set { _levertype = value; }
            get { return _levertype; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime ToTime
        {
            set { _totime = value; }
            get { return _totime; }
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
        public DateTime? AppDate { get; set; }
    }
}

