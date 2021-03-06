﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// TB_SupplierInvoice:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_SupplierAdvancePayment
    {
        public TB_SupplierAdvancePayment()
        { }
        #region Model
        private int _id;
        private string _prono;
        private string _createname;
        private DateTime _cretetime;
        private string _status;
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
        public string CreateName
        {
            set { _createname = value; }
            get { return _createname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreteTime
        {
            set { _cretetime = value; }
            get { return _cretetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

        public string Remark { get; set; }

        public decimal SumActPay { get; set; }

        public string FristFPNo { set; get; }

        public string SecondFPNo { get; set; }
        public string LastFPNo { get; set; }
    }
}
