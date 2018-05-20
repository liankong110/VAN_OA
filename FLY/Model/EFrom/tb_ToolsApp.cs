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
    [Serializable]
    public partial class tb_ToolsApp : CommModel
    {
        public tb_ToolsApp()
        { }
        #region Model
        private int _id;
        private string _departname;
        private string _appname;
        private DateTime? _datetime;
        private string _toolname;
        private string _toolyong;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string departName
        {
            set { _departname = value; }
            get { return _departname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string appName
        {
            set { _appname = value; }
            get { return _appname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? dateTime
        {
            set { _datetime = value; }
            get { return _datetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string toolName
        {
            set { _toolname = value; }
            get { return _toolname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string toolYong
        {
            set { _toolyong = value; }
            get { return _toolyong; }
        }
        #endregion Model

    }
}
