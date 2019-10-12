using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.BaseInfo
{
    [Serializable]
    public partial class Base_WarnDays
    {
        public Base_WarnDays()
        { }
        #region Model
        private int _id;
        private string _name = "";
        private float _warndays = 0;
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
        /// 
        /// </summary>
        public float WarnDays
        {
            set { _warndays = value; }
            get { return _warndays; }
        }
        #endregion Model

    }
}