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

namespace VAN_OA.Model.ReportForms
{
    /// <summary>
    /// 车辆保养记录表
    /// </summary>
    [Serializable]
    public partial class TB_CarMaintenance
    {
        public TB_CarMaintenance()
        { }
        #region Model
        private int _id;
        private string _cardno;
        private DateTime _maintenancetime;
        private decimal? _distance;
        private string _replaceremark;
        private string _replacestatus;
        private string _remark;
        private int _createuser;
        private DateTime _createtime;

        private string _usestate;
        /// <summary>
        /// 单号
        /// </summary>
        public string UseState
        {
            set { _usestate = value; }
            get { return _usestate; }
        }


        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CardNo
        {
            set { _cardno = value; }
            get { return _cardno; }
        }
        /// <summary>
        /// 保养时间点
        /// </summary>
        public DateTime MaintenanceTime
        {
            set { _maintenancetime = value; }
            get { return _maintenancetime; }
        }
        /// <summary>
        /// 公里数
        /// </summary>
        public decimal? Distance
        {
            set { _distance = value; }
            get { return _distance; }
        }
        /// <summary>
        /// 更换内容
        /// </summary>
        public string ReplaceRemark
        {
            set { _replaceremark = value; }
            get { return _replaceremark; }
        }
        /// <summary>
        /// 维修情况
        /// </summary>
        public string ReplaceStatus
        {
            set { _replacestatus = value; }
            get { return _replacestatus; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

        private decimal? _uptotal;
        private decimal? _oiltotal;
        private decimal? _addtotal;
        private decimal? _total;
        /// <summary>
		/// 
		/// </summary>
		public decimal? UpTotal
		{
			set{ _uptotal=value;}
			get{return _uptotal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? OilTotal
		{
			set{ _oiltotal=value;}
			get{return _oiltotal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? AddTotal
		{
			set{ _addtotal=value;}
			get{return _addtotal;}
		}
		/// <summary>
		/// 金额
		/// </summary>
		public decimal? Total
		{
			set{ _total=value;}
			get{return _total;}
		}
	 

    }
}
