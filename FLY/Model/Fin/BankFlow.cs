using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.Fin
{
    public class BankFlow
    {
        public BankFlow()
        { }
        #region Model
        private int _id;
        private string _transactiontype = "";
        private string _businesstype = "";
        private string _payeraccountbank = "";
        private string _debitaccountno = "";
        private string _outpayername = "";
        private string _beneficiaryaccountbank = "";
        private string _payeeaccountnumber = "";
        private string _inpayeename = "";
        private DateTime _transactiondate ;
        private string _tradecurrency = "";
        private decimal _tradeamount = 0M;
        private decimal _aftertransactionbalance = 0M;
        private string _transactionreferencenumber = "";
        private string _vouchertype = "";
        private string _reference = "";
        private string _purpose = "";
        private string _remark = "";
        private string _remarks = "";
        private string _notes = "";
        private string _incometype = "";
        private string _paymenttype = "";
        private string _yuremarks = "";
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 交易类型
        /// </summary>
        public string TransactionType
        {
            set { _transactiontype = value; }
            get { return _transactiontype; }
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessType
        {
            set { _businesstype = value; }
            get { return _businesstype; }
        }
        /// <summary>
        /// 付款人开户行名
        /// </summary>
        public string PayerAccountBank
        {
            set { _payeraccountbank = value; }
            get { return _payeraccountbank; }
        }
        /// <summary>
        /// 付款人账号
        /// </summary>
        public string DebitAccountNo
        {
            set { _debitaccountno = value; }
            get { return _debitaccountno; }
        }
        /// <summary>
        /// 付款人名称
        /// </summary>
        public string OutPayerName
        {
            set { _outpayername = value; }
            get { return _outpayername; }
        }
        /// <summary>
        /// 收款人开户行名
        /// </summary>
        public string BeneficiaryAccountBank
        {
            set { _beneficiaryaccountbank = value; }
            get { return _beneficiaryaccountbank; }
        }
        /// <summary>
        /// 收款人账号
        /// </summary>
        public string PayeeAccountNumber
        {
            set { _payeeaccountnumber = value; }
            get { return _payeeaccountnumber; }
        }
        /// <summary>
        /// 收款人名称
        /// </summary>
        public string InPayeeName
        {
            set { _inpayeename = value; }
            get { return _inpayeename; }
        }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime TransactionDate
        {
            set { _transactiondate = value; }
            get { return _transactiondate; }
        }
        /// <summary>
        /// 交易货币
        /// </summary>
        public string TradeCurrency
        {
            set { _tradecurrency = value; }
            get { return _tradecurrency; }
        }
        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal TradeAmount
        {
            set { _tradeamount = value; }
            get { return _tradeamount; }
        }
        /// <summary>
        /// 交易后余额
        /// </summary>
        public decimal AfterTransactionBalance
        {
            set { _aftertransactionbalance = value; }
            get { return _aftertransactionbalance; }
        }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string TransactionReferenceNumber
        {
            set { _transactionreferencenumber = value; }
            get { return _transactionreferencenumber; }
        }
        /// <summary>
        /// 凭证类型
        /// </summary>
        public string VoucherType
        {
            set { _vouchertype = value; }
            get { return _vouchertype; }
        }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Reference
        {
            set { _reference = value; }
            get { return _reference; }
        }
        /// <summary>
        /// 用途
        /// </summary>
        public string Purpose
        {
            set { _purpose = value; }
            get { return _purpose; }
        }
        /// <summary>
        /// 交易附言
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        /// 注释
        /// </summary>
        public string Notes
        {
            set { _notes = value; }
            get { return _notes; }
        }
        /// <summary>
        /// 进账类型
        /// </summary>
        public string IncomeType
        {
            set { _incometype = value; }
            get { return _incometype; }
        }
        /// <summary>
        /// 付款类型
        /// </summary>
        public string PaymentType
        {
            set { _paymenttype = value; }
            get { return _paymenttype; }
        }
        /// <summary>
        /// 预留备注
        /// </summary>
        public string YuRemarks
        {
            set { _yuremarks = value; }
            get { return _yuremarks; }
        }
        #endregion Model

        public decimal SUMFPTotal { get; set; }
        public decimal SUMOutTotal { get; set; }
    }
}