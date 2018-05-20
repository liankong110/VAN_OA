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
	/// Tb_ExpInv:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
    public partial class Tb_ExpInv
    {
        public Tb_ExpInv()
        { }
        #region Model
        private int _id;
        private int _createuserid;
        private DateTime _exptime;
        private string _prono;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 领用人
        /// </summary>
        public int CreateUserId
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 领用时间
        /// </summary>
        public DateTime ExpTime
        {
            set { _exptime = value; }
            get { return _exptime; }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string ProNo
        {
            set { _prono = value; }
            get { return _prono; }
        }
        #endregion Model

        private string _loginName;
        public string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }


        /// <summary>
        /// 部门
        /// </summary>
        public string DepartMent { get; set; }

        /// <summary>
        /// 是否发货
        /// </summary>
        public bool IfOutGoods { get; set; }

        /// <summary>
        /// 发货日期
        /// </summary>
        public DateTime? OutTime { get; set; }

        /// <summary>
        /// 事件号
        /// </summary>
        public string EventNo { get; set; }
    }
}
