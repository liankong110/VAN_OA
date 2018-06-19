using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// CAI_OrderInHouses:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CAI_OrderOutHouses:GoodModel
    {
        public CAI_OrderOutHouses()
        { }
        public decimal Total { get; set; }
        public decimal  GoodPrice { get; set; }
        public int OrderCheckIds { get; set; }
        #region Model
        private int _ids;
        private int _id;
        private int _gooid;
        private decimal _goodnum;
        private string _goodremark;
        /// <summary>
        /// 
        /// </summary>
        public int Ids
        {
            set { _ids = value; }
            get { return _ids; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GooId
        {
            set { _gooid = value; }
            get { return _gooid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal GoodNum
        {
            set { _goodnum = value; }
            get { return _goodnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GoodRemark
        {
            set { _goodremark = value; }
            get { return _goodremark; }
        }
        #endregion Model


        public string QingGouPer { get; set; }

        public string Status { get; set; }

        public DateTime RuTime { get; set; }

        public decimal CaiLastTruePrice { get; set; }
        #region 表头
        public string PONo { get; set; }
        public string POName { get; set; }
        public string ProNo { get; set; }

        public string Supplier { get; set; }

        #endregion

        public string GoodAreaNumber { get; set; }
        /// <summary>
        /// 临时库存
        /// </summary>
        public decimal TempHousePrice { get; set; }
    }
}
