using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// CAI_OrderChecks:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CAI_OrderChecks : GoodModel
    {
        public CAI_OrderChecks()
        { }
        #region Model
        private int _ids;
        private int _checkid;
        private string _pono;
        private string _suppliername;
        private int _checkgoodid;
        private decimal _checknum;
        private decimal _checkprice;

        public DateTime CheckTime { get; set; }
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
        public int CheckId
        {
            set { _checkid = value; }
            get { return _checkid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PONo
        {
            set { _pono = value; }
            get { return _pono; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CheckGoodId
        {
            set { _checkgoodid = value; }
            get { return _checkgoodid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CheckNum
        {
            set { _checknum = value; }
            get { return _checknum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CheckPrice
        {
            set { _checkprice = value; }
            get { return _checkprice; }
        }
        #endregion Model




        public decimal Total { get; set; }

        public string POName { get; set; }

        public string GuestName { get; set; }

        public int CaiId { get; set; }


        public string CaiProNo { get; set; }


        
        public string QingGou { get; set; }
        /// <summary>
        /// 采购人
        /// </summary>
        public string CaiGouPer { get; set; }
        //下面为主表准备的字段
        public string ProNo { get; set; }
        public string CheckRemark { get; set; }
        public string Status { get; set; }
        public string CheckPer { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateName { get; set; }
        public string CheckUserName { get; set; }
        public decimal CheckLastTruePrice { get; set; }
        public string GoodAreaNumber { get; set; }

    }
}
