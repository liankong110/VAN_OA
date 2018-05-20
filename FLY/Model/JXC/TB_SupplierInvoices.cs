using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// TB_SupplierInvoices:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_SupplierInvoices
    {
        public TB_SupplierInvoices()
        { }
        #region Model
        private int _ids;
        private int _id;
        private int _ruids;
        private string _supplierfpno;
        private int _supplierinvoicestatues;
        private DateTime _supplierinvoicedate;
        private decimal _supplierinvoicetotal;
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
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int RuIds
        {
            set { _ruids = value; }
            get { return _ruids; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SupplierFPNo
        {
            set { _supplierfpno = value; }
            get { return _supplierfpno; }
        }
       
        /// <summary>
        /// 
        /// </summary>
        public DateTime SupplierInvoiceDate
        {
            set { _supplierinvoicedate = value; }
            get { return _supplierinvoicedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SupplierInvoiceTotal
        {
            set { _supplierinvoicetotal = value; }
            get { return _supplierinvoicetotal; }
        }

        public decimal SupplierInvoiceNum { get; set; }
        public decimal SupplierInvoicePrice { get; set; }
        public bool IsYuFu { get; set; }
        public string SupplierProNo { get; set; }
        #endregion Model

        public string AllCaiIdsPay { get; set; }

        /// <summary>
        /// 实际支付
        /// </summary>
        public decimal ActPay { get; set; }


        /// <summary>
        /// 是否结清
        /// 任何记录的 结清 初始 都是 0
        ///事前退货的支付单 审批通过 结清 =1   就是6.A
        ///事后退货的支付单，审批通过 实际支付为负数 的 结清=2  就是6.B
        ///事后扣减成功 ，审批通过，这几条实际支付为负数的 记录 的结清=3  就是6.C
        ///
        ///邮件的正文已经修改正确了，见下面。
        ///结清=3的记录其实就是结清=2的记录 ，因此结清=3 的记录不用参加 1-4的逻辑状态运算。
        /// </summary>
        public decimal RePayClear { get; set; }

        /// <summary>
        /// 负数合计
        /// </summary>
        public decimal FuShuTotal { get; set; }

        /// <summary>
        /// 支付状态
        /// 0 未支付
        /// 1 未结清
        /// 2 已支付
        /// </summary>
        public decimal IsPayStatus { get; set; }

        /// <summary>
        /// 是否合并 默认都是0
        /// </summary>
        public int IsHeBing { get; set; }

        public DateTime? SupplierFpDate { get; set; }

    }
}
