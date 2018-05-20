using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.ReportForms
{
    /// <summary>
    /// TB_BusCardRecord:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_BusCardRecord
    {
        public TB_BusCardRecord()
        { }
        #region Model
        private int _id;
        private string _buscardno;
        private string _buscardper;
        private DateTime _buscarddate;
        private decimal _buscardtotal;
        private string _buscardremark;
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
        public string BusCardNo
        {
            set { _buscardno = value; }
            get { return _buscardno; }
        }
        /// <summary>
        /// 使用人
        /// </summary>
        public string BusCardPer
        {
            set { _buscardper = value; }
            get { return _buscardper; }
        }
        /// <summary>
        /// 充值日期
        /// </summary>
        public DateTime BusCardDate
        {
            set { _buscarddate = value; }
            get { return _buscarddate; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal BusCardTotal
        {
            set { _buscardtotal = value; }
            get { return _buscardtotal; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string BusCardRemark
        {
            set { _buscardremark = value; }
            get { return _buscardremark; }
        }
        #endregion Model

    }
}
