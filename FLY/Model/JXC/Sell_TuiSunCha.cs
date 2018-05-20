using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// Sell_TuiSunCha:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Sell_TuiSunCha
    {
        public Sell_TuiSunCha()
        { }
        #region Model
        private int _id;
        private int _selltuiids;
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
        public int SellTuiIds
        {
            set { _selltuiids = value; }
            get { return _selltuiids; }
        }
        #endregion Model

    }
}
