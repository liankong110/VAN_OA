using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// TB_Model:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Fin_JieDate
    {
        public Fin_JieDate()
        { }
        #region Model
        private int _id;
        private int _JYear;
        private DateTime _JDate;
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
        public int JYear
        {
            set { _JYear = value; }
            get { return _JYear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime JDate
        {
            set { _JDate = value; }
            get { return _JDate; }
        }
        #endregion Model

    }
}