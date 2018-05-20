using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{

    /// <summary>
    /// TB_CaiXiaoNo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_CaiXiaoNo
    {
        public TB_CaiXiaoNo()
        { }
        #region Model
        private int _id;
        private string _type;
        private string _lblno;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 单号
        /// </summary>
        public string lblNo
        {
            set { _lblno = value; }
            get { return _lblno; }
        }
        #endregion Model

    }
}
