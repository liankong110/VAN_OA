using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    [Serializable]
    public partial class Base_UseType
    {
        public Base_UseType()
        { }
        #region Model
        private int _id;
        private string _name = "";
       
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
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// 1 电子票据A和 2电子票据B中银行票据的用途种类
        /// </summary>
        public int Type { get; set; }

        public string  Type_String { get; set; }
        #endregion Model

    }
}