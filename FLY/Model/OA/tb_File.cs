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
    /// 实体类tb_File 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class tb_File
    {
        public bool ifCon(tb_File file)
        {
            return file.fileName.Contains(this.fileName);
        }
        public bool show(tb_File file)
        {
            return this.id ==file.id;
        }
        public tb_File()
        { }
        #region Model
        private int _id;
        private string _filename;
        private string _fileurl;
        private string _filefullname;
        private DateTime? _createtime;
        private string _createper;
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
        public string fileName
        {
            set { _filename = value; }
            get { return _filename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fileURL
        {
            set { _fileurl = value; }
            get { return _fileurl; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string fileFullName
        {
            set { _filefullname = value; }
            get { return _filefullname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string createPer
        {
            set { _createper = value; }
            get { return _createper; }
        }
        #endregion Model

    }
}
