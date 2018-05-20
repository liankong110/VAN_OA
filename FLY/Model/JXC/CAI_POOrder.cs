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
using VAN_OA.Model.EFrom;

namespace VAN_OA.Model.JXC
{
    
        /// <summary>
        /// TB_POOrder:实体类(属性说明自动提取数据库字段的描述信息)
        /// </summary>
        [Serializable]
    public partial class CAI_POOrder : CommModel
        {
            public CAI_POOrder()
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

            private string _pono;
            private string _bustype;
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
            public string BusType
            {
                set { _bustype = value; }
                get { return _bustype; }
            }

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


            /// <summary>
            /// 客户ID
            /// </summary>
            public string GuestNo { get; set; }
            /// <summary>
            /// 客户名称
            /// </summary>
            public string GuestName { get; set; }
            /// <summary>
            /// AE
            /// </summary>
            public string AE { get; set; }
            /// <summary>
            /// INSIDE
            /// </summary>
            public string INSIDE { get; set; }
            
            /// <summary>
            /// 项目名称
            /// </summary>
            public string POName { get; set; }
            /// <summary>
            /// 项目日期
            /// </summary>
            public DateTime PODate { get; set; }

            public string POPayStype { get; set; }


            public decimal POTotal { get; set; }


            /// <summary>
            /// 状态
            /// </summary>
            public string Status { get; set; }


            public string CG_ProNo { get; set; }

            public int Count1 { get; set; }

            public int Count2 { get; set; }

            /// <summary>
            /// 审核时间
            /// </summary>
            public DateTime? LastTime { get; set; }
            public bool IsMingYiCaiGou { get; set; }
        }
    
}
