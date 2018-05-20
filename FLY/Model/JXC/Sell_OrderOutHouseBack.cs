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
    public partial class Sell_OrderOutHouseBack
    {
        public Sell_OrderOutHouseBack()
        { }
        #region Model

        public string CreateName { get; set; }
        private int _id;
        private int _createuserid;
        private DateTime _createtime;
        private DateTime _rutime;
        private string _supplier;
        private int _doper;

        private string _sellProNo;
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
        /// 签回时间
        /// </summary>
        public DateTime BackTime
        {
            set { _rutime = value; }
            get { return _rutime; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string GuestName
        {
            set { _supplier = value; }
            get { return _supplier; }
        }
        /// <summary>
        /// 签回类型
        /// </summary>
        public int BackType
        {
            set { _doper = value; }
            get { return _doper; }
        }

        /// <summary>
        /// 销售单号
        /// </summary>
        public string SellProNo
        {
            set { _sellProNo = value; }
            get { return _sellProNo; }
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



        /// <summary>
        /// 总销售金额
        /// </summary>
        public decimal SellTotal { get; set; }

    }
}
