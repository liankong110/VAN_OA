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
        /// TB_POOrder:实体类(属性说明自动提取数据库字段的描述信息)
        /// </summary>
        [Serializable]
    public partial class TB_POOrder : CommModel
        {
            public TB_POOrder()
            { }
            #region Model
            private string _cremark;
            /// <summary>
            /// 
            /// </summary>
            public string cRemark
            {
                set { _cremark = value; }
                get { return _cremark; }
            }
            private string caiGou;
            public string CaiGou
            {
                get { return caiGou; }
                set { caiGou = value; }
            }
            private int _id;
            private int _appname;
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
            public int AppName
            {
                set { _appname = value; }
                get { return _appname; }
            }
            #endregion Model

            private string loginName;

            public string LoginName
            {
                get { return loginName; }
                set { loginName = value; }
            }

            private string _filename;
            private byte[] _fileno;
            private string _filetype;

            /// <summary>
            /// 
            /// </summary>
            public string fileName
            {
                set { _filename = value; }
                get { return _filename; }
            }
            /// <summary>
            /// 
            /// </summary>
            public byte[] fileNo
            {
                set { _fileno = value; }
                get { return _fileno; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string fileType
            {
                set { _filetype = value; }
                get { return _filetype; }
            }

        }
    
}
