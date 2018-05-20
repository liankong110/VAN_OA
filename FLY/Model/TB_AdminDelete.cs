using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model
{
    /// <summary>
    /// TB_AdminDelete:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_AdminDelete
    {
        public TB_AdminDelete()
        { }
        #region Model
        private int _id;
        private int _userid;
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
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }

        public string UserName { get; set; }
        #endregion Model

    }
}
