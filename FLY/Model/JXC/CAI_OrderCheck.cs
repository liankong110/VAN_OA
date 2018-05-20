using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// CAI_OrderCheck:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CAI_OrderCheck
    {
        public CAI_OrderCheck()
        { }
        public string AE { get;set;}
        public string CreateName { get; set; }
        public string CheckUserName { get; set; }
        public string ProNo { get; set; }
        #region Model
        private int _id;
        private int _checkper;
        private DateTime _checktime;
        private int _createper;
        private DateTime _createtime;
        private string _checkremark;
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
        public int CheckPer
        {
            set { _checkper = value; }
            get { return _checkper; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CheckTime
        {
            set { _checktime = value; }
            get { return _checktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CreatePer
        {
            set { _createper = value; }
            get { return _createper; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CheckRemark
        {
            set { _checkremark = value; }
            get { return _checkremark; }
        }
        #endregion Model

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        public string PONo { get; set; }
        public string POName { get; set; }
        public string GUESTName { get; set; }

        public string SupplierName { get; set; }

        public int IsHanShui { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? LastTime { get; set; }
    }
}
