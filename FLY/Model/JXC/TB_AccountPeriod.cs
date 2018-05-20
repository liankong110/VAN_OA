using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    /// <summary>
    /// TB_AccountPeriod:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_AccountPeriod
    {
        public TB_AccountPeriod()
        { }
        #region Model
        private int _id;
        private decimal _accountname;
        private decimal _accountxishu;
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
        /// 
        /// </summary>
        public decimal AccountName
        {
            set { _accountname = value; }
            get { return _accountname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal AccountXiShu
        {
            set { _accountxishu = value; }
            get { return _accountxishu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}
