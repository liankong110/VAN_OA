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
    public partial class tb_EForm
    {
        public tb_EForm()
        { }

        private string createPer_Name;
        public string CreatePer_Name
        {
            get { return createPer_Name; }
            set { createPer_Name = value; }
        }
        private string appPer_Name;
        public string AppPer_Name
        {
            get { return appPer_Name; }
            set { appPer_Name = value; }
        }
        private string toPer_Name;
        public string ToPer_Name
        {
            get { return toPer_Name; }
            set { toPer_Name = value; }
        }
        #region Model
        private int _id;
        private int _proid;
        private int _createper;
        private DateTime _createtime;
        private int _appper;
        private DateTime _apptime;
        private string _state;
        private int _alle_id;
        private int _toper;
        private int? _toprosid;
        private DateTime doTime;
        public DateTime DoTime
        {
            get { return doTime; }
            set { doTime = value; }
        }
        private string type1;
        public string Type1
        {
            get { return type1; }
            set { type1 = value; }
        }

        private string proTyleName;
        public string ProTyleName
        {
            get { return proTyleName; }
            set { proTyleName = value; }
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
        public int proId
        {
            set { _proid = value; }
            get { return _proid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int createPer
        {
            set { _createper = value; }
            get { return _createper; }
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
        public int appPer
        {
            set { _appper = value; }
            get { return _appper; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime appTime
        {
            set { _apptime = value; }
            get { return _apptime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string state
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int allE_id
        {
            set { _alle_id = value; }
            get { return _alle_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int toPer
        {
            set { _toper = value; }
            get { return _toper; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? toProsId
        {
            set { _toprosid = value; }
            get { return _toprosid; }
        }
        #endregion Model

        /// <summary>
        /// 表单单号
        /// </summary>
        private string e_No;
        public string E_No
        {
            get { return e_No; }
            set { e_No = value; }
        }

        /// <summary>
        /// 表单备注
        /// </summary>
        private string e_Remark;
        public string E_Remark
        {
            get { return e_Remark; }
            set { e_Remark = value; }
        }

        public string maxDoTime { get; set; }

        public string SupplierName { get; set; }

        public DateTime? E_LastTime { get; set; }
    }
}
