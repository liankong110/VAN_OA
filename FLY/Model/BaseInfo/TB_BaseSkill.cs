using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// TB_BaseSkill:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_BaseSkill
    {
        public TB_BaseSkill()
        { }
        #region Model
        private int _id;
        private string _mypotype = "";
        private decimal _xishu;
        private decimal _val = 0M;
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
        public string MyPoType
        {
            set { _mypotype = value; }
            get { return _mypotype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal XiShu
        {
            set { _xishu = value; }
            get { return _xishu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Val
        {
            set { _val = value; }
            get { return _val; }
        }
        #endregion Model

    }
}