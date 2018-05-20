using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace VAN_OA.Model.EFrom
{
    /// <summary>
    /// A_ProInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
   
        [Serializable]
        public partial class A_ProInfo 
        {
            public A_ProInfo()
            { }
            #region Model
            private int _pro_id;
            private string _pro_type;
            /// <summary>
            /// 
            /// </summary>
            public int pro_Id
            {
                set { _pro_id = value; }
                get { return _pro_id; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string pro_Type
            {
                set { _pro_type = value; }
                get { return _pro_type; }
            }


            private bool ifIDS;
            public bool IfIDS
            {
                get { return ifIDS; }
                set { ifIDS = value; }
            }
            #endregion Model

            public int RoleId { get; set; }
        }

     
}
