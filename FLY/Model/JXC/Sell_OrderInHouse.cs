﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// CAI_OrderInHouse:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Sell_OrderInHouse
    {
        public Sell_OrderInHouse()
        { }
        #region Model

        public string CreateName { get; set; }
        private int _id;
        private int _createuserid;
        private DateTime _createtime;
        private DateTime _rutime;
        private string _supplier;
        private string _doper;
        
        private string _chcekprono;
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
        public DateTime RuTime
        {
            set { _rutime = value; }
            get { return _rutime; }
        }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
        /// <summary>
        /// 经手人
        /// </summary>
        public string DoPer
        {
            set { _doper = value; }
            get { return _doper; }
        }
       
        /// <summary>
        /// 检验单号
        /// </summary>
        public string ChcekProNo
        {
            set { _chcekprono = value; }
            get { return _chcekprono; }
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
        /// 状态
        /// </summary>
        public string Status { get; set; }

        private string _fpno;
        private string _daili;
        /// <summary>
        /// 发票号
        /// </summary>
        public string FPNo
        {
            set { _fpno = value; }
            get { return _fpno; }
        }
        /// <summary>
        /// 代理人
        /// </summary>
        public string DaiLi
        {
            set { _daili = value; }
            get { return _daili; }
        }

        /// <summary>
        /// 总退货销售金额
        /// </summary>
        public decimal TuiTotal { get; set; }

        /// <summary>
        /// 退货类别-1.空白—缺省   2.更换供应商或换货并可以退给供应商   3.调整销售价格或客户退货了我们无法退给供应商  
        /// </summary>
        public int SellInType { get; set; }
    }
}
