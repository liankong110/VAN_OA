using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAN_OA.Model.JXC
{
    [Serializable]
    public class GoodModel
    {

         /// <summary>
        /// 编码
        /// </summary>
        public string GoodNo { get; set; }
          /// <summary>
        /// 名称
        /// </summary>
        public string GoodName { get; set; }

         /// <summary>
        /// 小类
        /// </summary>
        public string GoodTypeSmName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string GoodSpec { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string Good_Model { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string GoodUnit { get; set; }
    }
}
