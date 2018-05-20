using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.Fin
{
    /// <summary>
    /// Out_BankFlow:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Out_BankFlow
    {
        public Out_BankFlow()
        { }
        #region Model
        private int _id;
        private string _number = "";
        private string _referencenumber = "";
        private string _outtype = "";
        private string _prono = "";
        private decimal _outtotal = 0M;
        private string _remark = "";
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 交易流水号（和银行流水记账表的字段一样）
        /// </summary>
        public string ReferenceNumber
        {
            set { _referencenumber = value; }
            get { return _referencenumber; }
        }
        /// <summary>
        /// 进账类型 
        /// </summary>
        public string OutType
        {
            set { _outtype = value; }
            get { return _outtype; }
        }
        /// <summary>
        /// 发票号码
        /// </summary>
        public string ProNo
        {
            set { _prono = value; }
            get { return _prono; }
        }
        /// <summary>
        /// 进账金额
        /// </summary>
        public decimal OutTotal
        {
            set { _outtotal = value; }
            get { return _outtotal; }
        }
        /// <summary>
        /// 注释
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        public DateTime Time { get; set; }
        #endregion Model

    }
}