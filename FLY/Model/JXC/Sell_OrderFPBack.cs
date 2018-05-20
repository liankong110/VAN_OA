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
    public partial class Sell_OrderFPBack
    {
        public Sell_OrderFPBack()
        { }
        #region Model

        public decimal Total { get; set; }

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
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime FPBackTime
        {
            set { _rutime = value; }
            get { return _rutime; }
        }
        /// <summary>
        /// 客户
        /// </summary>
        public string GuestName { get; set; }
       
        /// <summary>
        /// 发票签回类型
        /// </summary>
        public int FPBackType{get;set;}      
       
     
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
        /// 已经付款金额
        /// </summary>
        public decimal sumTotal { get; set; }

        /// <summary>
        /// 尚未付款金额
        /// </summary>
        public decimal chaTotals { get; set; }

        /// <summary>
        /// 发票ID
        /// </summary>
        public int PId { get; set; }
       
    }
}
