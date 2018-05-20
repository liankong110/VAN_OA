using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{

    /// <summary>
    /// TB_HouseInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_HouseInfo
    {
        public TB_HouseInfo()
        { }
        public string CreateUserName { get; set; }

        #region Model
        private int _id;
        private string _houseno;
        private string _housename;
        private string _houseremark;
        private int _createuserid;
        private DateTime _createtime;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string houseNo
        {
            set { _houseno = value; }
            get { return _houseno; }
        }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string houseName
        {
            set { _housename = value; }
            get { return _housename; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string houseRemark
        {
            set { _houseremark = value; }
            get { return _houseremark; }
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
        #endregion Model

        public bool IfDefault { get; set; }

    }
}
