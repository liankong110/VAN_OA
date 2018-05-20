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

namespace VAN_OA.Model.OA
{
    /// <summary>
    /// 实体类tb_Folder 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class tb_Folder
    {
        private string AttId;
        public string AttId1
        {
            get { return AttId; }
            set { AttId = value; }
        }
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public tb_Folder()
        { }
        #region Model
        private int _folder_id;
        private string _folder_name;
        private bool _state;
        private int? _parentid;
        /// <summary>
        /// 
        /// </summary>
        public int Folder_ID
        {
            set { _folder_id = value; }
            get { return _folder_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Folder_NAME
        {
            set { _folder_name = value; }
            get { return _folder_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        #endregion Model

    }
}
