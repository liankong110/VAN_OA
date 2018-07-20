using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// CAI_OrderInHouse:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Sell_OrderFP
    {
        public Sell_OrderFP()
        { }
        #region Model
        public decimal? Total1 { get; set; }
        public decimal Total { get; set; }
        public bool Isorder { get; set; }

        

        public string CreateName { get; set; }
        private int _id;
        private int _createuserid;
        private DateTime _createtime;
        private DateTime _rutime;
      
        private string _doper;
        
     
        private string _prono;
        private string _pono;
        private string _poname;
        private string _remark;

     
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 修改人
        /// </summary>
        public int CreateUserId
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        public DateTime? RuTime1 { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime RuTime
        {
            set { _rutime = value; }
            get { return _rutime; }
        }
        /// <summary>
        /// 客户
        /// </summary>
        public string GuestName { get; set; }
       
        /// <summary>
        /// 经手人
        /// </summary>
        public string DoPer
        {
            set { _doper = value; }
            get { return _doper; }
        }
       
     
        /// <summary>
        /// 单据号
        /// </summary>
        public string ProNo
        {
            set { _prono = value; }
            get { return _prono; }
        }
        /// <summary>
        /// 项目编码
        /// </summary>
        public string PONo
        {
            set { _pono = value; }
            get { return _pono; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string POName
        {
            set { _poname = value; }
            get { return _poname; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

        /// <summary>
        /// 以前发票号
        /// </summary>
        public string TopFPNo { get; set; }

        /// <summary>
        /// 以前金额
        /// </summary>
        public decimal TopTotal { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        private string _fpno;
     
        /// <summary>
        /// 发票号
        /// </summary>
        public string FPNo
        {
            set { _fpno = value; }
            get { return _fpno; }
        }
        /// <summary>
        /// 发票类型
        /// </summary>
        public string FPNoStyle{get;set;}


        /// <summary>
        /// 已经付款金额
        /// </summary>
        public decimal sumTotal { get; set; }

        /// <summary>
        /// 尚未付款金额
        /// </summary>
        public decimal chaTotals { get; set; }


        /// <summary>
        /// 是否退票
        /// </summary>
        public bool IsTuiHuo { get; set; }

        /// <summary>
        /// 项目金额
        /// </summary>
        public decimal AllPoTotal { get; set; }


        /// <summary>
        /// 发票修改 GUID
        /// </summary>
        public string NowGuid { get; set; }

        /// <summary>
        /// 发票修改到款单 GUID
        /// </summary>
        public string InvoiceNowGuid { get; set; }

        /// <summary>
        /// 预付款结转
        /// </summary>
        public decimal ZhuanPayTotal { get; set; }

        /// <summary>
        /// 项目日期
        /// </summary>
        public DateTime PODate { get; set; }
        /// <summary>
        /// 1 正 2 负数
        /// </summary>
        public int ZhengFu { get; set; }
    }
}
