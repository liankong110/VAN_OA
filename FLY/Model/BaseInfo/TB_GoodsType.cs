using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// TB_GoodsType:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_GoodsType
    {
        public TB_GoodsType()
        { }
        #region Model
        private int _id;
        private string _goodtypename;
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
        public string GoodTypeName
        {
            set { _goodtypename = value; }
            get { return _goodtypename; }
        }
        #endregion Model

    }
}
