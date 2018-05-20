using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// CAR_YearCheck:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CAR_YearCheck
    {
        public CAR_YearCheck()
        { }
        #region Model
        private int _id;
        private string _carno = "";
        private DateTime _yeardate = DateTime.Now;
        private string _remark = "";
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
        public string CarNo
        {
            set { _carno = value; }
            get { return _carno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime YearDate
        {
            set { _yeardate = value; }
            get { return _yeardate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}