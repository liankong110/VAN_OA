using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    [Serializable]
    public class FIN_Property
    {
        public FIN_Property()
        { }
        #region Model
        private int _id;
        private string _prono = "";
        private string _costtype = "";
        private string _myproperty = "";
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
        public string ProNo
        {
            set { _prono = value; }
            get { return _prono; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CostType
        {
            set { _costtype = value; }
            get { return _costtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MyProperty
        {
            set { _myproperty = value; }
            get { return _myproperty; }
        }
        #endregion Model

        public decimal Value { get; set; }
        public int UserId { get; set; }
        /// <summary>
        /// 系统值
        /// </summary>
        public decimal XiShu_Value { get; set; }

        /// <summary>
        /// 当月值
        /// </summary>
        public decimal CurrentMonth_Value { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code_Value { get; set; }

    }
}