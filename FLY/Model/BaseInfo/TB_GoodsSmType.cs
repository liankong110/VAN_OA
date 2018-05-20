using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// TB_GoodsSmType:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_GoodsSmType
    {
        public TB_GoodsSmType()
        { }
        #region Model
        private int _id;
        private int? _goodtypeid;
        private string _goodtypesmname;
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
        public string GoodTypeSmName
        {
            set { _goodtypesmname = value; }
            get { return _goodtypesmname; }
        }
        #endregion Model


        public string GoodTypeName { get; set; }
    }
}
