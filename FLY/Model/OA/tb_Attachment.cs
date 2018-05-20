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
    /// tb_Attachment:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tb_Attachment
    {

        private string parentd;
        public string Parentd
        {
            get { return parentd; }
            set { parentd = value; }
        }
        public tb_Attachment()
        { }

        private string _fileType;
        public string FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }

        #region Model
        private int _id;
        private int? _folder_Id;
        private string _filename;
        private byte[] _fileno;

        private DateTime _createtime;
        private string _username;
        private string _version;
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
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
        public int? Folder_Id
        {
            set { _folder_Id = value; }
            get { return _folder_Id; }
        }
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
        public DateTime createTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string version
        {
            set { _version = value; }
            get { return _version; }
        }

        private string mainName;
        public string MainName
        {
            get { return mainName; }
            set { mainName = value; }
        }

        private string folderName;
        public string FolderName
        {
            get { return folderName; }
            set { folderName = value; }
        }
        #endregion Model

    }
}
