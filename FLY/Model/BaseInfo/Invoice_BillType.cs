using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// Invoice_BillType:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Invoice_BillType
    {
        public Invoice_BillType()
        { }
        #region Model
        private int _id;
        private string _billname = "";
        private string _billtype = "";
        private bool _issop = false;
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
        public string BillName
        {
            set { _billname = value; }
            get { return _billname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BillType
        {
            set { _billtype = value; }
            get { return _billtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsStop
        {
            set { _issop = value; }
            get { return _issop; }
        }
        #endregion Model

    }
}