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
    public partial class tb_Consignor
    {
        public tb_Consignor()
        { }
        #region Model
        private int _con_id;
        private int _consignor;
        private string _consignor_Name;
        public string Consignor_Name
        {
            get { return _consignor_Name; }
            set { _consignor_Name = value; }
        }
        private int _appper;
        private string _appper_Name;
        public string Appper_Name
        {
            get { return _appper_Name; }
            set { _appper_Name = value; }
        }
        private DateTime? _fromtime;
        private DateTime? _totime;
        private bool _ifyouxiao;
        private string _constate;
        private int _proid;


        private string youxiaoqi;
        public string Youxiaoqi
        {
            get { return youxiaoqi; }
            set { youxiaoqi = value; }
        }
        private string proType;
        public string ProType
        {
            get { return proType; }
            set { proType = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int con_Id
        {
            set { _con_id = value; }
            get { return _con_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int consignor
        {
            set { _consignor = value; }
            get { return _consignor; }
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
        public DateTime? fromTime
        {
            set { _fromtime = value; }
            get { return _fromtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? toTime
        {
            set { _totime = value; }
            get { return _totime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ifYouXiao
        {
            set { _ifyouxiao = value; }
            get { return _ifyouxiao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string conState
        {
            set { _constate = value; }
            get { return _constate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int proId
        {
            set { _proid = value; }
            get { return _proid; }
        }
        #endregion Model

    }
}
