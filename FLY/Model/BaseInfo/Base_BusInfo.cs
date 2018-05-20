using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    [Serializable]
    public partial class Base_BusInfo
    {
        public Base_BusInfo()
        { }
        #region Model
        private int _id;
        private string _busno = "";
        private bool _isstop = false;
        private DateTime _createtime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 公交卡号
        /// </summary>
        public string BusNo
        {
            set { _busno = value; }
            get { return _busno; }
        }
        /// <summary>
        /// 是否停用
        /// </summary>
        public bool IsStop
        {
            set { _isstop = value; }
            get { return _isstop; }
        }
        /// <summary>
        /// 新建日期
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

    }
}