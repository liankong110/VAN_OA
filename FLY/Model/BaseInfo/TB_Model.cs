using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    /// <summary>
    /// TB_Model:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TB_Model
    {
        public TB_Model()
        { }
        #region Model
        private int _id;
        private string _modelname;
        private string _modelremark;
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
        public string ModelName
        {
            set { _modelname = value; }
            get { return _modelname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModelRemark
        {
            set { _modelremark = value; }
            get { return _modelremark; }
        }
        #endregion Model

    }
}